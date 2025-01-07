using Controller.Game;
using Model.Game;
using View.Game;
using ViewWPF.Game;
using System.Windows;
using System.Windows.Input;
using ViewWPF;

namespace ControllerWPF.Game
{
  /// <summary>
  /// Контроллер игры для WPF.
  /// Управляет игровым процессом, включая обработку событий и обновление состояния игрока.
  /// </summary>
  public class ControllerGameWPF : ControllerGame, IWPFController
  {
    /// <summary>
    /// Флаг, указывающий, нужно ли завершить игру
    /// </summary>
    private Boolean _exit = false;

    /// <summary>
    /// Делегат для обработки движения игрока влево.
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
    /// Конструктор контроллера игры для WPF.
    /// </summary>
    /// <param name="modelGame">Модель игры, содержащая состояние игры.</param>
    public ControllerGameWPF(ModelGame modelGame) : base(modelGame)
    {
    }

    /// <summary>
    /// Запускает игровой процесс, подписывается на события и инициирует обновление состояния.
    /// </summary>
    public override void Start()
    {
      // Инициализация представления игры для WPF
      _viewGame = new ViewGameWPF(_modelGame);
      base.Start();
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler;
    }

    /// <summary>
    /// Подписка на события движения игрока влево и вправо.
    /// </summary>
    protected override void SubscribeToEvents()
    {
      base.SubscribeToEvents();
      MovePlayerLeft += _modelGame.MovePlayerLeft;
      MovePlayerRight += _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Отписка от событий движения игрока влево и вправо.
    /// </summary>
    protected override void UnsubscribeFromEvents()
    {
      base.UnsubscribeFromEvents();
      MovePlayerLeft -= _modelGame.MovePlayerLeft;
      MovePlayerRight -= _modelGame.MovePlayerRight;
    }

    /// <summary>
    /// Очистка игрового поля в интерфейсе WPF.
    /// </summary>
    public override void ClearGameField()
    {
      MainScreen.GetInstance().Window.Content = MainScreen.GetInstance().StackPanel;
    }

    /// <summary>
    /// Обработка окончания игры.
    /// </summary>
    public override void ProcessEndGameCall()
    {
      Application.Current.Dispatcher.Invoke(ProcessEndGame);
    }

    /// <summary>
    /// Обработчик нажатий клавиш.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parArgs">Аргументы события (содержат информацию о нажатой клавише).</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Left:
          MovePlayerLeft?.Invoke();
          break;
        case Key.Right:
          MovePlayerRight?.Invoke();
          break;
        case Key.Escape:
          GameInterruptCall();
          Stop();
          GoBackCall();
          _exit = true;
          break;
        case Key.Space:
          Stop();
          PauseGameCall();
          _exit = true;
          break;
      }
    }

    /// <summary>
    /// Остановка контроллера игры.
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler;
    }
  }
}
