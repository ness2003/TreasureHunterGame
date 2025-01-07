using Model.Game;
using Model.TableOfRecords;
using System.Diagnostics;
using View.Game;

namespace Controller.Game
{
  /// <summary>
  /// Контроллер игры, управляющий основными процессами игры.
  /// </summary>
  public abstract class ControllerGame : IController
  {

    /// <summary>
    /// Максимально допустимое время одного кадра.
    /// </summary>
    private const float MAX_ELAPSED_TIME_BORDER = 1f;

    /// <summary>
    /// Количество обновлений состояния игры в секунду.
    /// </summary>
    private int _gameUpdatesPerSecond = 60;

    /// <summary>
    /// Период обновления состояния игры в секундах.
    /// </summary>
    private float _updatePeriodSeconds => 1f / _gameUpdatesPerSecond;

    /// <summary>
    /// Модель игры, содержащая игровую логику и данные.
    /// </summary>
    protected ModelGame _modelGame;

    /// <summary>
    /// Представление игры, отображающее состояние игры пользователю.
    /// </summary>
    protected ViewGame? _viewGame;

    /// <summary>
    /// Поток игрового цикла.
    /// </summary>
    private Thread? _gameTicker;

    /// <summary>
    /// Событие для возврата в предыдущее меню.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Событие при приостановке игры.
    /// </summary>
    public event Action? OnGamePaused;

    /// <summary>
    /// Событие при прерывании игры.
    /// </summary>
    public event Action? OnGameInterrupred;

    /// <summary>
    /// Событие при окончании игры.
    /// </summary>
    public event Action? OnGameEnded;

    /// <summary>
    /// Событие для обновления очков и уровня.
    /// </summary>
    public event Action<int, int>? ChangeScoreAndLevel;

    /// <summary>
    /// Событие для перехода к результату игры.
    /// </summary>
    public event Action? GoToGameResult;

    /// <summary>
    /// Событие для обновления состояния игрока.
    /// </summary>
    public event Action? PlayerUpdateHandlerEvent;

    /// <summary>
    /// Событие для удержания игры в приостановленном состоянии.
    /// </summary>
    private readonly ManualResetEvent _gamePauseEvent = new(false);

    /// <summary>
    /// Количество обновлений состояния игры в секунду.
    /// </summary>
    public int GameUpdatesPerSecond
    {
      get { return _gameUpdatesPerSecond; }
      set { _gameUpdatesPerSecond = value; }
    }

    /// <summary>
    /// Инициализирует новый экземпляр контроллера игры.
    /// </summary>
    /// <param name="parModelGame">Модель игры.</param>
    public ControllerGame(ModelGame parModelGame)
    {
      _modelGame = parModelGame;
    }

    /// <summary>
    /// Запускает игровой процесс.
    /// </summary>
    public virtual void Start()
    {
      _modelGame.StartGame();
      SubscribeToEvents();
      _gamePauseEvent.Set();
      if (_gameTicker == null)
      {
        StartGameLoop();
      }
    }

    /// <summary>
    /// Останавливает игровой процесс.
    /// </summary>
    public virtual void Stop()
    {
      UnsubscribeFromEvents();
      _gamePauseEvent.Reset();
      ClearGameField();
    }

    /// <summary>
    /// Подписка на события модели игры.
    /// </summary>
    protected virtual void SubscribeToEvents()
    {
      _modelGame.OnTimesUp += ProcessEndGameCall;
    }

    /// <summary>
    /// Отписка от событий модели игры.
    /// </summary>
    protected virtual void UnsubscribeFromEvents()
    {
      _modelGame.OnTimesUp -= ProcessEndGameCall;
    }

    /// <summary>
    /// Обрабатывает завершение игры.
    /// </summary>
    public abstract void ProcessEndGameCall();

    /// <summary>
    /// Завершает игру и проверяет рекорды.
    /// </summary>
    public void ProcessEndGame()
    {
      OnGameEnded?.Invoke();
      int score = _modelGame.Score;
      int level = _modelGame.Level;
      Stop();
      _modelGame?.Reset();
      if (ModelTableOfRecords.Instance.IsNewRecord(score))
      {
        ChangeScoreAndLevel?.Invoke(score, level);
        GoToGameResult?.Invoke();
      }
      else
      {
        GoToBack?.Invoke();
      }
    }

    /// <summary>
    /// Очищает игровое поле.
    /// </summary>
    public abstract void ClearGameField();

    /// <summary>
    /// Сбрасывает игровое состояние.
    /// </summary>
    public virtual void ResetCall()
    {
      _modelGame?.Reset();
    }

    /// <summary>
    /// Прерывает игру.
    /// </summary>
    public void GameInterruptCall()
    {
      OnGameInterrupred?.Invoke();
    }

    /// <summary>
    /// Возвращает в предыдущее меню.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }

    /// <summary>
    /// Приостанавливает игру.
    /// </summary>
    public void PauseGameCall()
    {
      OnGamePaused?.Invoke();
    }

    /// <summary>
    /// Запускает цикл обновления состояния игры.
    /// </summary>
    private void StartGameLoop()
    {
      _gameTicker = new Thread(() =>
      {
        var timer = Stopwatch.StartNew();
        double previousTime = timer.Elapsed.TotalSeconds;
        double lagSeconds = 0;

        while (_gameTicker != null)
        {
          _gamePauseEvent.WaitOne();
          double currentTime = timer.Elapsed.TotalSeconds;
          double elapsedSeconds = currentTime - previousTime;
          previousTime = currentTime;
          lagSeconds += elapsedSeconds;

          if (lagSeconds >= MAX_ELAPSED_TIME_BORDER)
            lagSeconds = 0;

          while (lagSeconds >= _updatePeriodSeconds)
          {
            _modelGame.Update((float)(_updatePeriodSeconds / lagSeconds), _updatePeriodSeconds);
            lagSeconds -= _updatePeriodSeconds;
          }
        }
      })
      {
        Name = "GAME_TICKER"
      };
      _gameTicker.Start();
    }
  }
}
