using Model.TableOfRecords;

namespace ViewConsole.TableOfRecords
{
  /// <summary>
  /// Консольное представление таблицы рекордов
  /// </summary>
  public class ViewTableOfRecordsConsole : View.TableOfRecords.ViewTableOfRecords
  {
    /// <summary>
    /// Конструктор консольного представления таблицы рекордов
    /// </summary>
    /// <param name="parModelTableOfRecords">Модель таблицы рекордов</param>
    public ViewTableOfRecordsConsole(ModelTableOfRecords parModelTableOfRecords)
        : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Центрирует текст в строке консоли с указанными цветами
    /// </summary>
    /// <param name="text">Текст для отображения</param>
    /// <param name="textColor">Цвет текста</param>
    /// <param name="bgColor">Цвет фона</param>
    private void WriteCentered(string text, ConsoleColor textColor, ConsoleColor bgColor)
    {
      Console.ForegroundColor = textColor;
      Console.BackgroundColor = bgColor;

      int screenWidth = Console.WindowWidth;
      int padding = (screenWidth - text.Length) / 2;

      Console.WriteLine(new string(' ', padding) + text + new string(' ', padding));
    }

    /// <summary>
    /// Отображает таблицу рекордов в консоли
    /// </summary>
    public override void Draw()
    {
      Console.Clear();

      Console.BackgroundColor = ConsoleColor.White;
      Console.Clear();

      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);
      WriteCentered("       ТАБЛИЦА РЕКОРДОВ       ", ConsoleColor.DarkGreen, ConsoleColor.White);
      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);

      foreach (var record in _modelTableOfRecords.RecordsValues)
      {
        string recordLine = $"{record.Name} - score: {record.Score} level: {record.Level}";
        WriteCentered(recordLine, ConsoleColor.Black, ConsoleColor.White);
      }

      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);
    }
  }
}
