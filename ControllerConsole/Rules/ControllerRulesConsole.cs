using ViewConsole.Rules;
using Model.Rules;
using Controller.Rules;

namespace ControllerConsole.Rules
{
  /// <summary>
  /// Контроллер правил для консольного интерфейса.
  /// Отвечает за отображение правил игры и обработку пользовательского ввода.
  /// </summary>
  public class ControllerRulesConsole : ControllerRules, IConsoleController
  {
    /// <summary>
    /// Инициализирует новый экземпляр контроллера правил для консоли.
    /// </summary>
    /// <param name="parModelRules">Модель правил игры.</param>
    public ControllerRulesConsole(ModelRules parModelRules) : base(parModelRules)
    {
    }

    /// <summary>
    /// Запускает контроллер правил.
    /// Отображает правила игры и начинает обработку пользовательского ввода.
    /// </summary>
    public override void Start()
    {
      _viewRules = new ViewRulesConsole(_modelRules);
      ProcessKeyPress();
    }

    /// <summary>
    /// Обрабатывает нажатия клавиш пользователя.
    /// </summary>
    public void ProcessKeyPress()
    {
      bool exitRequested = false; // Флаг для выхода из режима просмотра правил

      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.Escape:
            // При нажатии Escape завершаем работу контроллера
            Stop();
            GoBackCall();
            exitRequested = true;
            break;
        }

      } while (!exitRequested);
    }

    /// <summary>
    /// Останавливает контроллер правил.
    /// Очищает консольный экран.
    /// </summary>
    public override void Stop()
    {
      Console.Clear();
    }
  }
}
