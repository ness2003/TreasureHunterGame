namespace ControllerConsole
{
  /// <summary>
  /// Интерфейс для контроллеров консольных приложений.
  /// Определяет методы для обработки ввода с клавиатуры.
  /// </summary>
  public interface IConsoleController
  {
    /// <summary>
    /// Метод для обработки нажатий клавиш в консольном приложении.
    /// </summary>
    void ProcessKeyPress();
  }
}
