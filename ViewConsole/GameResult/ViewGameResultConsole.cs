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
    public ViewGameResultConsole(ModelGameResult parModelGameResult) : base(parModelGameResult)
    {
    }

    /// <summary>
    /// Нарисовать результат игры в консоли
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("        КОНЕЦ ИГРЫ       ");
      Console.WriteLine("=========================");
      Console.WriteLine($"Score: {_modelGameResult.Score}");
      Console.WriteLine($"Level: {_modelGameResult.Level}");
      Console.WriteLine("=========================");

      Console.WriteLine("Новый Рекорд! Введите ваше имя:");
      string playerName = Console.ReadLine() ?? "Player";
      _modelGameResult.Name = playerName;

      Console.WriteLine($"Спасибо, {playerName}! Ваш результат сохранён.");
      Console.WriteLine("Нажмите любую клавишу для продолжения...");
      Console.ReadKey();
    }
  }
}
