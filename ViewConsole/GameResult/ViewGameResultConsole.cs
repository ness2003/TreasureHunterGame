  using System;
  using Model.GameResult;

  namespace ViewConsole.GameResult
  {
    /// <summary>
    /// Консольное представление результата игры
    /// </summary>
    public class ViewGameResultConsole : View.GameResult.ViewGameResult
    {
      /// <summary>
      /// Конструктор
      /// </summary>
      /// <param name="parModelGameResult">Модель результата игры</param>
      public ViewGameResultConsole(ModelGameResult parModelGameResult)
          : base(parModelGameResult)
      {
      }

      /// <summary>
      /// Центрирует текст в строке консоли
      /// </summary>
      /// <param name="parText">Текст для вывода</param>
      /// <param name="parTextColor">Цвет текста</param>
      /// <param name="parBgColor">Цвет фона</param>
      private void WriteCentered(string parText, ConsoleColor parTextColor, ConsoleColor parBgColor)
      {
        Console.ForegroundColor = parTextColor;
        Console.BackgroundColor = parBgColor;

        int parScreenWidth = Console.WindowWidth;
        int parPadding = (parScreenWidth - parText.Length) / 2;

        Console.WriteLine(new string(' ', parPadding) + parText + new string(' ', parPadding));
      }

      /// <summary>
      /// Нарисовать результат игры в консоли
      /// </summary>
      public override void Draw()
      {
        Console.Clear();

        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();

        WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);
        WriteCentered("        КОНЕЦ ИГРЫ       ", ConsoleColor.DarkGreen, ConsoleColor.White);
        WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);

        WriteCentered($"Score: {_modelGameResult.Score}", ConsoleColor.Black, ConsoleColor.White);
        WriteCentered($"Level: {_modelGameResult.Level}", ConsoleColor.Black, ConsoleColor.White);

        WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);

        WriteCentered("Новый Рекорд! Введите ваше имя:", ConsoleColor.DarkRed, ConsoleColor.White);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;

        Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);

      }
    }
  }
