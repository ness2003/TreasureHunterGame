using Controller.Game;
using Model.Game;
using View.Game;
using ViewWPF.Game;
using System.Windows;
using System.Windows.Input;
using ViewWPF;
using System.Configuration;

namespace ControllerWPF.Game
{
  /// <summary>
  /// Контроллер игры для WPF.
  /// Управляет игровым процессом, включая обработку событий и обновление состояния игрока.
  /// </summary>
  public class ControllerGameWPF : ControllerGame
  {
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
    /// Конструктор
    /// </summary>
    /// <param name="modelGame">Модель игры.</param>
    public ControllerGameWPF(ModelGame modelGame) : base(modelGame)
    {

    }

    /// <summary>
    /// Запускает игровой процесс, подписывается на события и инициирует обновление состояния.
    /// </summary>
    public override void Start()
    {
      _viewGame = new ViewGameWPF(_modelGame);
      base.Start();
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
    /// Очистка игрового поля
    /// </summary>
    public override void ClearGameField()
    {
      MainScreen.GetInstance().Window.Content = MainScreen.GetInstance().StackPanel;
    }

    /// <summary>
    /// Обработчик события обновления состояния игрока.
    /// </summary>
    protected override void PlayerUpdateHandler()
    {
      Application.Current.Dispatcher.Invoke(() =>
      {
        // Проверяем состояние клавиш и вызываем соответствующие события
        if (Keyboard.IsKeyDown(Key.Left))
        {
          MovePlayerLeft?.Invoke();
        }
        if (Keyboard.IsKeyDown(Key.Right))
        {
          MovePlayerRight?.Invoke();
        }
        // Обрабатываем клавишу Escape для остановки игры
        if (Keyboard.IsKeyDown(Key.Escape))
        {
          GameInterruptCall();
          GoBackCall();
        }
        if (Keyboard.IsKeyDown(Key.Space))
        {
          PauseGameCall();
        }
      });
    }
  }
}

