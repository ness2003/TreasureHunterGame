using Microsoft.Win32.SafeHandles;
using Model.Game;
using Model.Game.GameObjects;
using View.Game;
using ViewsConsole;

namespace ViewConsole.Game
{
  public class ViewGameConsole : ViewGame
  {
    /// <summary>
    /// Ширина игрового окна консоли
    /// </summary>
    public const int GAME_CONSOLE_WIDTH = 90;
    /// <summary>
    /// Высота игрового окна консоли
    /// </summary>
    public const int GAME_CONSOLE_HEIGHT = 30;

    /// <summary>
    /// Длина буфера для консоли
    /// </summary>
    private const int CONSOLE_BUFFER_LENGTH = GAME_CONSOLE_WIDTH * GAME_CONSOLE_HEIGHT;

    /// <summary>
    /// Смещение для битового сдвига влево, чтобы настроить цвет фона консоли
    /// </summary>
    private const int LEFT_BYTE_SHIFT_OFFSET_FOR_BACKGROUND_COLOR = 4;

    /// <summary>
    /// Дескриптор вывода консоли
    /// </summary>
    private readonly SafeFileHandle _handleConsoleOut;

    /// <summary>
    /// Буфер для содержимого консоли
    /// </summary>
    private readonly ConsoleHelperUtilite.CharInfo[] _consoleBuffer;

    /// <summary>
    /// Генератор случайных чисел
    /// </summary>
    private readonly Random _random = new();

    private bool IsAcceptDraw = false;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModelGame">Экземпляр модели игры</param>
    public ViewGameConsole(ModelGame parModelGame) : base(parModelGame)
    {
      _modelGame = parModelGame;
      _handleConsoleOut = ConsoleHelperUtilite.GetConsoleOutputHandle();
      _consoleBuffer = new ConsoleHelperUtilite.CharInfo[CONSOLE_BUFFER_LENGTH];
      _modelGame.NeedRedraw += AcceptDraw;
    }

    /// <summary>
    /// Вывод символа в буфер консоли на заданные координаты с указанным цветом.
    /// </summary>
    /// <param name="parX">Позиция по горизонтали.</param>
    /// <param name="parY">Позиция по вертикали.</param>
    /// <param name="parColor">Цвет символа.</param>
    /// <param name="parChar">Символ для вывода.</param>
    private void PlaceCharToBuffer(int parX, int parY, ConsoleColor parColor, char parChar)
    {
      if (parX < 0 || parX >= GAME_CONSOLE_WIDTH || parY < 0 || parY >= GAME_CONSOLE_HEIGHT)
        return;

      int offset = parY * GAME_CONSOLE_WIDTH + parX;
      _consoleBuffer[offset].Attributes = (short)(((short)parColor) | ((short)ConsoleColor.White) << LEFT_BYTE_SHIFT_OFFSET_FOR_BACKGROUND_COLOR);
      _consoleBuffer[offset].Char.UnicodeChar = parChar;
    }

    /// <summary>
    /// Отрисовка игрока в виде прямоугольника на консоли.
    /// </summary>
    /// <param name="parPlayer">Объект игрока, содержащий его координаты и размеры.</param>
    /// <param name="parColor">Цвет символов для отрисовки.</param>
    /// <param name="parChar">Символ для отрисовки.</param>
    private void DrawPlayer(Player parPlayer, ConsoleColor parColor, char parChar)
    {
      int playerX = parPlayer.X;
      int playerY = parPlayer.Y;

      int left = playerX + 1;
      int right = playerX + parPlayer.Width - 1;
      int top = playerY + 1;
      int bottom = playerY + parPlayer.Height - 1;

      for (int x = left; x <= right; x++)
      {
        PlaceCharToBuffer(x, top, parColor, '*');
        PlaceCharToBuffer(x, bottom, parColor, '*');
      }

      for (int y = top; y <= bottom; y++)
      {
        PlaceCharToBuffer(left, y, parColor, '*');
        PlaceCharToBuffer(right, y, parColor, '*');
      }
    }

    /// <summary>
    /// Отрисовка заполненной окружности с заданным радиусом, центром и символом на консоли.
    /// </summary>
    /// <param name="parObject">Объект, содержащий радиус и координаты центра окружности.</param>
    /// <param name="parColor">Цвет символов для отрисовки.</param>
    /// <param name="parChar">Символ для отрисовки.</param>
    private void DrawCircle(GameObject parObject, ConsoleColor parColor, char parChar)
    {
      int radius = parObject.Radius;
      int centerX = parObject.X;
      int centerY = parObject.Y;

      int x = 0;
      int y = radius;
      int d = 3 - 2 * radius;

      /// <summary>
      /// Отрисовка горизонтальной линии между двумя точками.
      /// </summary>
      void DrawHorizontalLine(int cx, int cy, int dx)
      {
        for (int i = -dx; i <= dx; i++)
        {
          PlaceCharToBuffer(cx + i, cy, parColor, parChar);
        }
      }

      /// <summary>
      /// Заполнение окружности горизонтальными линиями.
      /// </summary>
      void FillCircle(int cx, int cy, int dx, int dy)
      {
        DrawHorizontalLine(cx, cy + dy, dx); // Верхняя симметрия
        DrawHorizontalLine(cx, cy - dy, dx); // Нижняя симметрия
        DrawHorizontalLine(cx, cy + dx, dy); // Левая вертикальная симметрия
        DrawHorizontalLine(cx, cy - dx, dy); // Правая вертикальная симметрия
      }

      while (x <= y)
      {
        FillCircle(centerX, centerY, x, y);

        if (d < 0)
        {
          d += 4 * x + 6;
        }
        else
        {
          d += 4 * (x - y) + 10;
          y--;
        }
        x++;
      }
    }



    /// <summary>
    /// Рисование объектов на поле
    /// </summary>
    private void DrawObjects()
    {
      List<GameObject> drawed = _modelGame.Coins;
      foreach (GameObject elObjects in drawed)
      {
        ConsoleColor color;
        switch (elObjects.ObjectType)
        {
          case ObjectType.GoldCell:
            color = ConsoleColor.DarkYellow;
            DrawCircle(elObjects, color, 'o');
            break;
          case ObjectType.SilverCell:
            color = ConsoleColor.DarkGray;
            DrawCircle(elObjects, color, 'o');
            break;
          case ObjectType.BronzeCell:
            color = ConsoleColor.DarkMagenta;
            DrawCircle(elObjects, color, 'o');
            break;
          default:
            color = ConsoleColor.Red;
            break;
        }
      }

      List<GameObject> drawed2 = _modelGame.Bonuses;
      foreach (GameObject elObjects in drawed2)
      {
        ConsoleColor color;
        switch (elObjects.ObjectType)
        {
          case ObjectType.Magnet:
            color = ConsoleColor.DarkBlue;
            DrawCircle(elObjects, color, 'U');
            break;
          case ObjectType.Timer:
            color = ConsoleColor.Green;
            DrawCircle(elObjects, color, 't');
            break;
          case ObjectType.Mul:
            color = ConsoleColor.Cyan;
            DrawCircle(elObjects, color, '2');
            break;
          default:
            color = ConsoleColor.Red;
            break;
        }
      }

      List<GameObject> drawed3 = _modelGame.BadObjects;
      foreach (GameObject elObjects in drawed3)
      {
        ConsoleColor color;
        switch (elObjects.ObjectType)
        {
          case ObjectType.Meteorite:
            color = ConsoleColor.Red;
            DrawCircle(elObjects, color, '*');
            break;
          case ObjectType.Thief:
            color = ConsoleColor.DarkRed;
            DrawCircle(elObjects, color, '$');
            break;
          default:
            color = ConsoleColor.Red;
            break;
        }
      }
    }

    /// <summary>
    /// Рисование границ игрового поля
    /// < /summary>
    private void DrawGameFieldBorders()
    {
      int fieldWidth = _modelGame.Width - 1;
      int fieldHeight = _modelGame.Height - 1;

      int xLeft = 0;
      int xRight = fieldWidth;
      int yUp = 0;
      int yDown = fieldHeight;

      for (int x = xLeft; x <= xRight; x++)
      {
        PlaceCharToBuffer(x, yUp, ConsoleColor.Black, '\xC4');
        PlaceCharToBuffer(x, yDown, ConsoleColor.Black, '\xC4');
      }

      for (int y = yUp; y <= yDown; y++)
      {
        PlaceCharToBuffer(xLeft, y, ConsoleColor.Black, '\xB3');
        PlaceCharToBuffer(xRight, y, ConsoleColor.Black, '\xB3');
      }

      PlaceCharToBuffer(xLeft, yUp, ConsoleColor.Black, '\xDA');
      PlaceCharToBuffer(xLeft, yDown, ConsoleColor.Black, '\xC0');
      PlaceCharToBuffer(xRight, yUp, ConsoleColor.Black, '\xBF');
      PlaceCharToBuffer(xRight, yDown, ConsoleColor.Black, '\xD9');
    }


    /// <summary>
    /// Очистка буфера консоли
    /// </summary>
    private void ClearConsoleBuffer()
    {
      Array.Fill(_consoleBuffer, new() { Char = new() { UnicodeChar = ' ' }, Attributes = (short)ConsoleColor.White << LEFT_BYTE_SHIFT_OFFSET_FOR_BACKGROUND_COLOR });
    }

    /// <summary>
    /// Функция отрисовки
    /// </summary>
    public override void Draw()
    {
      if (IsAcceptDraw)
      {
        ClearConsoleBuffer();
        DrawGameFieldBorders();
        DrawObjects();
        DrawPlayer(_modelGame.Player, ConsoleColor.Green, '.');
        DrawGameStatistic();
        ConsoleHelperUtilite.PrintToConsoleFast(_handleConsoleOut, _consoleBuffer, GAME_CONSOLE_WIDTH, GAME_CONSOLE_HEIGHT);
        IsAcceptDraw = false;
      }
    }

    public void AcceptDraw()
    {
      IsAcceptDraw = true;
    }

    /// <summary>
    /// Вывод строки в консоль на указанной позиции
    /// </summary>
    /// <param name="parString">Строка для вывода</param>
    /// <param name="parColor">Цвет текста</param>
    /// <param name="parX">Начальная позиция по горизонтали</param>
    /// <param name="parY">Начальная позиция по вертикали</param>
    private void PrintString(string parString, ConsoleColor parColor, int parX, int parY)
    {
      int parLength = parString.Length;
      for (int parIndex = 0; parIndex < parLength; ++parIndex)
      {
        PlaceCharToBuffer(parX++, parY, parColor, parString[parIndex]);
      }
    }


    /// <summary>
    /// Отображение статистики
    /// </summary>
    protected void DrawGameStatistic()
    {
      int x = 1;
      int y = 1;
      PrintString($"Score: {(int)_modelGame.Score}", ConsoleColor.Black, x, y++);
      PrintString($"Level: {(int)_modelGame.Level}", ConsoleColor.Black, x, y++);
      PrintString($"Remaining Time: {(int)_modelGame.RemainingTime}", ConsoleColor.Black, x, y++);
      x = 20;
      y = 1;
      PrintString($"===GOAL FOR LEVEL===", ConsoleColor.DarkBlue, x, y++);
      PrintString($"GOLD COINS: {(int)_modelGame.CurrentGoal.CountGoldCoins}", ConsoleColor.DarkYellow, x, y++);
      PrintString($"SILVER COINS: {(int)_modelGame.CurrentGoal.CountSilverCoins}", ConsoleColor.DarkGray, x, y++);
      PrintString($"BRONZE COINS: {(int)_modelGame.CurrentGoal.CountBronzeCoins}", ConsoleColor.DarkMagenta, x, y++);

      PrintString($"SCORE: {(int)_modelGame.CurrentGoal.ScoreTarget}", ConsoleColor.Blue, x, y++);
    }

    /// <summary>
    /// Настройка экрана при запуске игры
    /// </summary>
    public override void OnStartGame()
    {
      if (OperatingSystem.IsWindows())
      {
        Console.WindowWidth = GAME_CONSOLE_WIDTH;
        Console.WindowHeight = GAME_CONSOLE_HEIGHT;
        Console.BufferWidth = GAME_CONSOLE_WIDTH;
        Console.BufferHeight = GAME_CONSOLE_HEIGHT;
      }
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Draw();
    }
  }
}
