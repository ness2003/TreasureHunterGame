using Controller.GameResult;
using Model.GameResult;
using Model.TableOfRecords;
using ViewConsole.GameResult;

namespace ControllerConsole.GameResult
{
  /// <summary>
  /// Контроллер результата игры для консольного интерфейса.  
  /// Отвечает за отображение результатов игры и взаимодействие с пользователем через консоль.
  /// </summary>
  public class ControllerGameResultConsole : ControllerGameResult, IConsoleController
  {
    /// <summary>
    /// Инициализирует новый экземпляр контроллера результата игры для консоли.
    /// </summary>
    /// <param name="parModelGameResult">Модель результата игры, содержащая информацию о счёте и уровне.</param>
    public ControllerGameResultConsole(ModelGameResult parModelGameResult)
        : base(parModelGameResult)
    {
    }

    /// <summary>
    /// Запускает контроллер результата игры.  
    /// Отображает результаты игры и начинает обработку ввода пользователя.
    /// </summary>
    public override void Start()
    {
      // Создание представления для консоли
      _viewGameResult = new ViewGameResultConsole(_modelGameResult);

      // Отображение результатов игры на консоли
      _viewGameResult.Draw();

      // Обработка ввода пользователя
      ProcessKeyPress();
    }

    /// <summary>
    /// Обрабатывает нажатия клавиш пользователя.  
    /// Считывает имя игрока, сохраняет результат в таблицу рекордов и завершает работу контроллера.
    /// </summary>
    public void ProcessKeyPress()
    {
      string playerName = Console.ReadLine() ?? "Player";

      _modelGameResult.Name = playerName;

      ModelTableOfRecords.Instance.Add(new Record(playerName, _modelGameResult.Score, _modelGameResult.Level));
      Stop();
      GoBackCall();
    }

    /// <summary>
    /// Останавливает контроллер результата игры.  
    /// Очищает имя игрока в модели результата игры.
    /// </summary>
    public override void Stop()
    {
      _modelGameResult.Name = null;
    }
  }
}
