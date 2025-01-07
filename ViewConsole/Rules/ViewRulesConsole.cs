using Model.Rules;

namespace ViewConsole.Rules
{
  /// <summary>
  /// Консольное представление правил
  /// </summary>
  public class ViewRulesConsole : View.Rules.ViewRules
  {
    /// <summary>
    /// Конструктор консольного представления правил
    /// </summary>
    /// <param name="parModelRules">Модель правил</param>
    public ViewRulesConsole(ModelRules parModelRules) : base(parModelRules)
    {
    }

    /// <summary>
    /// Инициализация представления (для консоли не требуется специфических параметров)
    /// </summary>
    public override void Init()
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
    /// Отображает правила и управление в консоли
    /// </summary>
    public override void Draw()
    {
      Console.BackgroundColor = ConsoleColor.White;
      Console.Clear();

      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);
      WriteCentered("         ПРАВИЛА         ", ConsoleColor.DarkGreen, ConsoleColor.White);
      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);

      foreach (var line in _modelRules.RulesText)
      {
        WriteCentered(line, ConsoleColor.Black, ConsoleColor.White);
      }

      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);
      WriteCentered("       УПРАВЛЕНИЕ        ", ConsoleColor.DarkGreen, ConsoleColor.White);
      WriteCentered("=========================", ConsoleColor.DarkBlue, ConsoleColor.White);

      foreach (var line in _modelRules.ControlsText)
      {
        WriteCentered(line, ConsoleColor.Black, ConsoleColor.White);
      }
    }
  }
}
