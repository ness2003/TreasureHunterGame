using System;
using Model.Rules;

namespace ViewConsole.Rules
{
  /// <summary>
  /// Консольное представление правил
  /// </summary>
  public class ViewRulesConsole : View.Rules.ViewRules
  {
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
    /// Отображение правил и управления в консоли
    /// </summary>
    public override void Draw()
    {

      // Заголовок "Правила"
      Console.WriteLine("=========================");
      Console.WriteLine("         ПРАВИЛА         ");
      Console.WriteLine("=========================");

      // Отображение правил
      foreach (var line in _modelRules.RulesText)
      {
        Console.WriteLine(line);
      }

      // Разделитель
      Console.WriteLine("=========================");

      // Заголовок "Управление"
      Console.WriteLine("       УПРАВЛЕНИЕ        ");
      Console.WriteLine("=========================");

      // Отображение управления
      foreach (var line in _modelRules.ControlsText)
      {
        Console.WriteLine(line);
      }
    }
  }
}
