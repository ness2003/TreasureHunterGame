using Microsoft.Win32.SafeHandles;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace ViewsConsole
{
  /// <summary>
  /// Вспомогательный класс-прослойка между вызовами WINAPI
  /// </summary>
  public static class ConsoleHelperUtilite
  {
    /// <summary>
    /// Флаг "Не изменять размеры окна", сохраняет автонастройку
    /// </summary>
    private const uint SWP_NOSIZE = 0x0001;

    /// <summary>
    /// Координаты
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
      /// <summary>
      /// X
      /// </summary>
      public short X;
      /// <summary>
      /// Y
      /// </summary>
      public short Y;

      /// <summary>
      /// Инициализация
      /// </summary>
      /// <param name="X"></param>
      /// <param name="Y"></param>
      public Coord(short X, short Y)
      {
        this.X = X;
        this.Y = Y;
      }
    };

    /// <summary>
    /// Представление символа
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
      /// <summary>
      /// Поле для доступа как к символу Unicode
      /// </summary>
      [FieldOffset(0)] public char UnicodeChar;
      /// <summary>
      /// Поле для доступа как к символу Ascii
      /// </summary>
      [FieldOffset(0)] public byte AsciiChar;
    }

    /// <summary>
    /// Информация о символе
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
      /// <summary>
      /// Символ
      /// </summary>
      [FieldOffset(0)] public CharUnion Char;
      /// <summary>
      /// Атрибуты
      /// </summary>
      [FieldOffset(2)] public short Attributes;
    }

    /// <summary>
    /// Прямоугольник
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
      /// <summary>
      /// Левая сторона
      /// </summary>
      public short Left;
      /// <summary>
      /// Верхняя сторона
      /// </summary>
      public short Top;
      /// <summary>
      /// Правая сторона
      /// </summary>
      public short Right;
      /// <summary>
      /// Нижняя сторона
      /// </summary>
      public short Bottom;
    }

    /// <summary>
    /// Точка
    /// </summary>
    public struct Point
    {
      /// <summary>
      /// X
      /// </summary>
      public int X;
      /// <summary>
      /// Y
      /// </summary>
      public int Y;
    }


    /// <summary>
    /// Поиск дескриптора окна по заголовку
    /// </summary>
    /// <param name="ZeroOnly">Класс окна в более общей функции. Здесь же - всегда должно быть 0</param>
    /// <param name="lpWindowName">Точный заголовок окна</param>
    /// <returns>Дескриптор окна или ZeroPtr при отсутствии</returns>
    [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

    /// <summary>
    /// Создание файла
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileAccess"></param>
    /// <param name="fileShare"></param>
    /// <param name="securityAttributes"></param>
    /// <param name="creationDisposition"></param>
    /// <param name="flags"></param>
    /// <param name="template"></param>
    /// <returns></returns>
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    /// <summary>
    /// Вывод в консоль массива <paramref name="lpBuffer"/>
    /// </summary>
    /// <param name="hConsoleOutput"></param>
    /// <param name="lpBuffer"></param>
    /// <param name="dwBufferSize"></param>
    /// <param name="dwBufferCoord"></param>
    /// <param name="lpWriteRegion"></param>
    /// <returns></returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool WriteConsoleOutput(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);

    /// <summary>
    /// Получение прямоугольника окна консоли
    /// </summary>
    /// <param name="hwnd">Дескриптор окна консоли</param>
    /// <param name="rect">Прямоугольник</param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);

    /// <summary>
    /// Получение дескриптора окна консоли
    /// </summary>
    /// <param name="parWindowName">Имя окна консоли</param>
    /// <returns></returns>
    public static IntPtr GetConsoleWindowHandle(string parWindowName)
    {
      return FindWindowByCaption(IntPtr.Zero, parWindowName);
    }

    /// <summary>
    /// Получение дескриптора вывода консоли
    /// </summary>
    /// <returns></returns>
    public static SafeFileHandle GetConsoleOutputHandle()
    {
      return CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
    }

    /// <summary>
    /// Быстрый вывод в консоль
    /// </summary>
    /// <param name="parHandle">Дескриптор окна консоли</param>
    /// <param name="parBuffer">Буфер с данными</param>
    /// <param name="parWidth">Ширина буфера</param>
    /// <param name="parHeight">Высота буфера</param>
    public static void PrintToConsoleFast(SafeFileHandle parHandle, CharInfo[] parBuffer, int parWidth, int parHeight)
    {
      SmallRect rect = new() { Left = 0, Top = 0, Right = (short)parWidth, Bottom = (short)parHeight };
      WriteConsoleOutput(parHandle, parBuffer,
              new Coord() { X = (short)parWidth, Y = (short)parHeight },
              new Coord() { X = 0, Y = 0 },
              ref rect);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


    /// <summary>
    /// Устанавливает фиксированный размер консольного окна и предотвращает его изменение.
    /// </summary>
    /// <param name="width">Ширина окна консоли (в символах)</param>
    /// <param name="height">Высота окна консоли (в символах)</param>
    public static void SetFixedConsoleWindowSize(int width, int height)
    {
      // Устанавливаем размеры консоли
      Console.SetWindowSize(width, height);
      Console.SetBufferSize(width, height);

      // Получаем дескриптор консольного окна
      IntPtr consoleWindow = GetConsoleWindowHandle(Console.Title);
      if (consoleWindow == IntPtr.Zero)
      {
        throw new Exception("Не удалось получить дескриптор консольного окна.");
      }

      const int GWL_STYLE = -16;
      const int WS_SIZEBOX = 0x00040000;    // Возможность изменять размер окна
      const int WS_MAXIMIZEBOX = 0x00010000; // Кнопка максимизации окна

      // Убираем возможность изменять размер и максимизировать окно
      int style = GetWindowLong(consoleWindow, GWL_STYLE);
      style &= ~WS_SIZEBOX;       // Отключаем изменение размера
      style &= ~WS_MAXIMIZEBOX;   // Отключаем кнопку максимизации

      SetWindowLong(consoleWindow, GWL_STYLE, style);

      Console.Title = "TREASURE HUNTER";
    }

  }
}
