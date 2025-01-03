using Microsoft.Win32.SafeHandles;
using Model.Game;
using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Media.Media3D;
using View.Game;
using ViewsConsole;

namespace ViewConsole.Game
{
  public class ViewGameConsole : ViewGame
  {
    /// <summary>
    /// Ширина игрового окна консоли
    /// </summary>
    public const int GAME_CONSOLE_WIDTH = 80;
    /// <summary>
    /// Высота игрового окна консоли
    /// </summary>
    public const int GAME_CONSOLE_HEIGHT = 35;

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


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModelGame">Экземпляр модели игры</param>
    public ViewGameConsole(ModelGame parModelGame) : base(parModelGame)
    {
      _modelGame = parModelGame;
      _modelGame.Width = GAME_CONSOLE_WIDTH;
      _modelGame.Height = GAME_CONSOLE_HEIGHT;
      _handleConsoleOut = ConsoleHelperUtilite.GetConsoleOutputHandle();
      _consoleBuffer = new ConsoleHelperUtilite.CharInfo[CONSOLE_BUFFER_LENGTH]; 
      _modelGame.NeedRedraw += Draw;
    }


    /// <summary>
    /// Вывод символа в буфер консоли
    /// </summary>
    /// <param name="parX">Позиция по горизонтали</param>
    /// <param name="parY">Позиция по вертикали</param>
    /// <param name="parColor">Цвет</param>
    /// <param name="parChar">Символ</param>
    private void PlaceCharToBuffer(int parX, int parY, ConsoleColor parColor, char parChar)
    {
      if (parX < 0 || parX >= GAME_CONSOLE_WIDTH || parY < 0 || parY >= GAME_CONSOLE_HEIGHT)
        return;

      int offset = parY * GAME_CONSOLE_WIDTH + parX;
      _consoleBuffer[offset].Attributes = (short)(((short)parColor) | ((short)ConsoleColor.Black) << LEFT_BYTE_SHIFT_OFFSET_FOR_BACKGROUND_COLOR);
      _consoleBuffer[offset].Char.UnicodeChar = parChar;
    }

    /// <summary>
    /// Отрисовка игрока
    /// </summary>
    /// <param name="parPlayer">Объект игрока</param>
    /// <param name="parColor">Цвет</param>
    /// <param name="parChar">Символ</param>
    private void DrawPlayer(Player parPlayer, ConsoleColor parColor, char parChar)
    {
      // Размеры объекта
      Vector2 carSize = new(parPlayer.Height, parPlayer.Width);

      // Положение объекта
      Vector2 carLeftUp = new Vector2(parPlayer.X, parPlayer.Y) - carSize / 2;
      Vector2 carRightDown = carLeftUp + carSize;

      // Здесь нужно привести координаты объекта к координатам экрана
      // Преобразуем объектные координаты в координаты экрана
      Vector2 leftUpOnScreen = new Vector2(carLeftUp.X, carLeftUp.Y);
      Vector2 rightDownOnScreen = new Vector2(carRightDown.X, carRightDown.Y);

      // Рисуем верхнюю и нижнюю стороны (горизонтальные линии)
      for (int x = (int)leftUpOnScreen.X; x <= rightDownOnScreen.X; x++)
      {
        PlaceCharToBuffer(x, (int)leftUpOnScreen.Y, parColor, parChar);  // Верхняя сторона
        PlaceCharToBuffer(x, (int)rightDownOnScreen.Y, parColor, parChar); // Нижняя сторона
      }

      // Рисуем левую и правую стороны (вертикальные линии)
      for (int y = (int)leftUpOnScreen.Y; y <= rightDownOnScreen.Y; y++)
      {
        PlaceCharToBuffer((int)leftUpOnScreen.X, y, parColor, parChar);  // Левая сторона
        PlaceCharToBuffer((int)rightDownOnScreen.X, y, parColor, parChar); // Правая сторона
      }
    }


    private void DrawCircle(GameObject parObject, ConsoleColor parColor, char parChar)
    {
      // Радиус окружности
      int radius = parObject.Radius;

      // Центр окружности (с учетом координат объекта)
      int centerX = parObject.X;
      int centerY = parObject.Y;

      // Алгоритм Брезенхема для рисования окружности
      int x = radius;
      int y = 0;
      int err = 0;

      while (x >= y)
      {
        // Рисуем 8 симметричных точек окружности
        PlaceCharToBuffer(centerX + x, centerY + y, parColor, parChar); // Вверх справа
        PlaceCharToBuffer(centerX + y, centerY + x, parColor, parChar); // Вправо сверху
        PlaceCharToBuffer(centerX - y, centerY + x, parColor, parChar); // Вправо снизу
        PlaceCharToBuffer(centerX - x, centerY + y, parColor, parChar); // Вниз справа
        PlaceCharToBuffer(centerX - x, centerY - y, parColor, parChar); // Вниз слева
        PlaceCharToBuffer(centerX - y, centerY - x, parColor, parChar); // Влево снизу
        PlaceCharToBuffer(centerX + y, centerY - x, parColor, parChar); // Влево сверху
        PlaceCharToBuffer(centerX + x, centerY - y, parColor, parChar); // Вверх слева

        // Обновляем параметры для следующей точки на окружности
        y += 1;
        err += 2 * y + 1;
        if (2 * err >= 2 * x + 1)
        {
          x -= 1;
          err -= 2 * x + 1;
        }
      }
    }


    /// <summary>
    /// Рисование машин
    /// </summary>
    private void DrawObjects()
    {
      List<GameObject> drawed = _modelGame.Coins;
      foreach (GameObject elObjects in drawed)
        DrawCircle(elObjects, ConsoleColor.Yellow, '.');

      List<GameObject> drawed2 = _modelGame.Bonuses;
      foreach (GameObject elObjects in drawed2)
        DrawCircle(elObjects, ConsoleColor.Cyan, '.');

      List<GameObject> drawed3 = _modelGame.BadObjects;
      foreach (GameObject elObjects in drawed3)
        DrawCircle(elObjects, ConsoleColor.Red, '.');
    }



    /// <summary>
    /// Рисование границ игрового поля
    /// </summary>
    private void DrawGameFieldBorders()
    {
      // Размеры игрового поля
      int fieldWidth = _modelGame.Width;
      int fieldHeight = _modelGame.Height;

      // Позиции углов
      int xLeft = 0;
      int xRight = fieldWidth;
      int yUp = 0;
      int yDown = fieldHeight;

      // Рисуем верхнюю и нижнюю горизонтальные границы
      for (int x = xLeft; x <= xRight; x++)
      {
        // Верхняя граница
        PlaceCharToBuffer(x, yUp, ConsoleColor.Green, '\xC4');
        // Нижняя граница
        PlaceCharToBuffer(x, yDown, ConsoleColor.Green, '\xC4');
      }

      // Рисуем левую и правую вертикальные границы
      for (int y = yUp; y <= yDown; y++)
      {
        // Левая граница
        PlaceCharToBuffer(xLeft, y, ConsoleColor.Green, '\xB3');
        // Правая граница
        PlaceCharToBuffer(xRight, y, ConsoleColor.Green, '\xB3');
      }

      // Угловые символы
      PlaceCharToBuffer(xLeft, yUp, ConsoleColor.Green, '\xDA');   // Верхний левый угол
      PlaceCharToBuffer(xLeft, yDown, ConsoleColor.Green, '\xC0'); // Нижний левый угол
      PlaceCharToBuffer(xRight, yUp, ConsoleColor.Green, '\xBF');  // Верхний правый угол
      PlaceCharToBuffer(xRight, yDown, ConsoleColor.Green, '\xD9'); // Нижний правый угол
    }


    /// <summary>
    /// Очистка буфера консоли
    /// </summary>
    private void ClearConsoleBuffer()
    {
      Array.Fill(_consoleBuffer, new() { Char = new() { UnicodeChar = ' ' }, Attributes = (short)ConsoleColor.Black << LEFT_BYTE_SHIFT_OFFSET_FOR_BACKGROUND_COLOR });
    }

    private void OnCanRender()
    {
      Application.Current.Dispatcher.Invoke(Render);
    }

    private void Render()
    {
      Draw();
    }

    /// <summary>
    /// Функция отрисовки
    /// </summary>
    public override void Draw()
    {
      ClearConsoleBuffer();
      DrawGameFieldBorders();
      DrawObjects();
      DrawPlayer(_modelGame.Player, ConsoleColor.Green, '.');
      DrawGameStatistic();
      ConsoleHelperUtilite.PrintToConsoleFast(_handleConsoleOut, _consoleBuffer, GAME_CONSOLE_WIDTH, GAME_CONSOLE_HEIGHT);
    }

    /// <summary>
    /// Получение случайного цвета переднего плана, не совпадающего с <paramref name="parBackgroundColor"/>
    /// </summary>
    /// <param name="parBackgroundColor">Цвет заднего плана</param>
    /// <returns></returns>
    private ConsoleColor GetRandomCarColor(ConsoleColor parBackgroundColor)
    {
      const int COLORS_COUNT = 3;
      return _random.Next(COLORS_COUNT) switch
      {
        0 => ConsoleColor.Red,
        1 => ConsoleColor.Green,
        2 => ConsoleColor.DarkYellow,
        _ => ConsoleColor.Red
      };
    }

    /// <summary>
    /// Выводит строку <paramref name="parString"/> с началом в точке (<paramref name="parX"/>, <paramref name="parY"/>)
    /// цветом <paramref name="parColor"/>. Без переносов, при нехватке места строка отсечётся
    /// </summary>
    /// <param name="parString"></param>
    /// <param name="parColor"></param>
    /// <param name="parX"></param>
    /// <param name="parY"></param>
    private void PrintString(string parString, ConsoleColor parColor, int parX, int parY)
    {
      int l = parString.Length;
      for (int i = 0; i < l; ++i)
        PlaceCharToBuffer(parX++, parY, parColor, parString[i]);
    }

    /// <summary>
    /// Отображение таблицы лидеров
    /// </summary>
    protected void DrawGameStatistic()
    {
      //GameField gameField = GameInstance.GameField;
      int x = 10;
      int y = 0;
      PrintString($"Score: {(int)_modelGame.Score}", ConsoleColor.White, x, ++y);
      PrintString($"Level: {(int)_modelGame.Level}", ConsoleColor.White, x, ++y);
      PrintString($"Remaining Time: {(int)_modelGame.RemainingTime}", ConsoleColor.White, x, ++y);
      //PrintString($"Distance: {(int)(gameField.Height - gameField.Player.Car.Position.Y - GameField.ROAD_START_OFFSET)} m", ViewsProperties.GAME_TEXT_COLOR, x, ++y);
      //PrintString($"Danger overtakings: {(int)gameField.OvertakingCount}", ViewsProperties.GAME_TEXT_COLOR, x, ++y);
      //PrintString($"Time: {(int)gameField.ElapsedTime} s", ViewsProperties.GAME_TEXT_COLOR, x, ++y);
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
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.White;
      Draw();
    }

  }
}
