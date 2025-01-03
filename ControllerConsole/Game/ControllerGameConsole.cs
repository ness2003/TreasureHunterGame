using Controller;
using Controller.Game;
using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Rules;
using ViewConsole.Game;
using ViewConsole.Rules;

namespace ControllerConsole.Game
{
  public class ControllerGameConsole : ControllerGame, IConsoleController
  {
    /// <summary>
    /// Делегат для обработки движения игрока.
    /// </summary>
    public delegate void PlayerAction();

    /// <summary>
    /// Событие для движения игрока влево.
    /// </summary>
    public event PlayerAction? MovePlayerLeft;

    /// <summary>
    /// Событие для движения игрока вправо.
    /// </summary>
    public event PlayerAction? MovePlayerRight;

    /// <summary>
    /// Флаг для завершения игрового цикла.
    /// </summary>
    private bool _isGameRunning = true;

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

      ProcessKeyPress();
    }

    /// <summary>
    /// Подписка на события
    /// </summary>
    protected override void SubscribeToEvents()
    {
      base.SubscribeToEvents();
      // Подписываемся на событие обновления игрока
      PlayerUpdateHandlerEvent += PlayerUpdateHandler;

      // Подписываемся на события движения игрока
      MovePlayerLeft += _modelGame.MovePlayerLeft;
      MovePlayerRight += _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Отписка от событий
    /// </summary>
    protected override void UnsubscribeFromEvents()
    {
      base.UnsubscribeFromEvents();
      PlayerUpdateHandlerEvent -= PlayerUpdateHandler;
      MovePlayerLeft -= _modelGame.MovePlayerLeft;
      MovePlayerRight -= _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Обработчик событий управления игроком.
    /// </summary>
    protected override void PlayerUpdateHandler()
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
            _isGameRunning = false;
            GameInterruptCall();
            ClearGameField();
            GoBackCall();
            break;
          case ConsoleKey.Spacebar:
            ClearGameField();
            PauseGameCall();
            break;
        }
      }

    }

    /// <summary>
    /// Обработчик ввода с клавиатуры.
    /// </summary>
    public void ProcessKeyPress()
    {
      PlayerUpdateHandler();
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
