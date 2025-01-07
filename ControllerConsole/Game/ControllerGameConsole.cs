using Controller.Game;
using Model.Game;
using ViewConsole.Game;

namespace ControllerConsole.Game
{
  /// <summary>
  /// Консольный контроллер игры
  /// </summary>
  public class ControllerGameConsole : ControllerGame, IConsoleController
  {
    /// <summary>
    /// Делегат для обработки движения игрока.
    /// </summary>
    public delegate void dPlayerAction();

    /// <summary>
    /// Событие для движения игрока влево.
    /// </summary>
    public event dPlayerAction? MovePlayerLeft;

    /// <summary>
    /// Событие для движения игрока вправо.
    /// </summary>
    public event dPlayerAction? MovePlayerRight;

    /// <summary>
    /// Флаг для завершения игрового цикла.
    /// </summary>
    private bool _isTimeEnd = false;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера игры для консоли.
    /// </summary>
    /// <param name="modelGame">Модель игры.</param>
    public ControllerGameConsole(ModelGame modelGame) : base(modelGame)
    {
    }

    /// <summary>
    /// Запускает игровой процесс, подписывается на события и инициирует обновление состояния.
    /// </summary>
    public override void Start()
    {
      base.Start();
      _viewGame = new ViewGameConsole(_modelGame);
      _isTimeEnd = false;
      ProcessKeyPress();
    }

    /// <summary>
    /// Обработка окончания игры
    /// </summary>
    public override void ProcessEndGameCall()
    {
      _isTimeEnd = true;
    }

    /// <summary>
    /// Подписка на события
    /// </summary>
    protected override void SubscribeToEvents()
    {
      base.SubscribeToEvents();
      MovePlayerLeft += _modelGame.MovePlayerLeft;
      MovePlayerRight += _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Отписка от событий
    /// </summary>
    protected override void UnsubscribeFromEvents()
    {
      base.UnsubscribeFromEvents();
      MovePlayerLeft -= _modelGame.MovePlayerLeft;
      MovePlayerRight -= _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Обработчик ввода с клавиатуры.
    /// </summary>
    public void ProcessKeyPress()
    {
      bool exitRequested = false;
      do
      {
        if (Console.KeyAvailable)
        {
          ConsoleKey key = Console.ReadKey(intercept: true).Key; // intercept=true, чтобы не выводить на консоль

          switch (key)
          {
            case ConsoleKey.RightArrow:
              MovePlayerRight?.Invoke();
              break;
            case ConsoleKey.LeftArrow:
              MovePlayerLeft?.Invoke();
              break;
            case ConsoleKey.Escape:
              GameInterruptCall();
              ClearGameField();
              Stop();
              GoBackCall();
              exitRequested = true;
              break;
            case ConsoleKey.Spacebar:
              ClearGameField();
              Stop();
              PauseGameCall();
              exitRequested = true;
              break;
          }
        }
        if (_isTimeEnd)
        {
          exitRequested = true;
          ProcessEndGame();
        }
        _viewGame.Draw();
      } while (!exitRequested);
    }

    /// <summary>
    /// Очищает игровое поле в консоли.
    /// </summary>
    public override void ClearGameField()
    {
      Console.Clear();
    }
  }
}
