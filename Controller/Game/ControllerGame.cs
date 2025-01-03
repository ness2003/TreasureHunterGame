using Model.Game;
using Model.GameResult;
using Model.TableOfRecords;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using View.Game;
using static Model.Menu.MenuItem;

namespace Controller.Game
{
  /// <summary>
  /// Контроллер игры, управляющий основными процессами игры.
  /// </summary>
  public abstract class ControllerGame : IController
  {
    /// <summary>
    /// Количество обновлений состояния игры в секунду.
    /// </summary>
    private const int GAME_UPDATES_PER_SECOND = 60;

    /// <summary>
    /// Период обновления состояния игры в секундах.
    /// </summary>
    private const float UPDATE_PERIOD_SECONDS = 1f / GAME_UPDATES_PER_SECOND;

    /// <summary>
    /// Максимально допустимое время одного кадра.
    /// </summary>
    private const float MAX_ELAPSED_TIME_BORDER = 1f;
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
    /// Подписка на события
    /// </summary>
    protected virtual void SubscribeToEvents()
    {
      GoToBack += Stop;
      OnGamePaused += Stop;
      _modelGame.OnTimesUp += GameEndCall;
      _modelGame.OnTimesUp += ProcessEndGame;
      _modelGame.OnTimesUp += ResetCall;
    }

    /// <summary>
    /// Отписка от событий.
    /// </summary>
    protected virtual void UnsubscribeFromEvents()
    {
      GoToBack -= Stop;
      OnGamePaused -= Stop;
      _modelGame.OnTimesUp -= GameEndCall;
      _modelGame.OnTimesUp -= ProcessEndGame;
      _modelGame.OnTimesUp -= ResetCall;
    }


    /// <summary>
    /// Обрабатывает завершение игры.
    /// </summary>
    public void ProcessEndGame()
    {
      int score = _modelGame.Score;
      int level = _modelGame.Level;
      if (ModelTableOfRecords.Instance.IsNewRecord(score))
      {
        ChangeScoreAndLevel?.Invoke(score, level);
        GoToGameResult?.Invoke();
        Stop();
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
    /// Вызывает событие прерывания игры.
    /// </summary>
    public void GameInterruptCall()
    {
      OnGameInterrupred?.Invoke();
    }

    /// <summary>
    /// Вызывает событие окончания игры.
    /// </summary>
    public void GameEndCall()
    {
      OnGameEnded?.Invoke();
    }
    /// <summary>
    /// Возвращает к предыдущему экрану.
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
    /// Обновляет состояние игрока.
    /// </summary>
    protected abstract void PlayerUpdateHandler();

    /// <summary>
    /// Запускает игровой цикл, обрабатывающий обновления игры.
    /// </summary>
    private void StartGameLoop()
    {
      _gameTicker = new Thread(() =>
      {
        Thread.CurrentThread.IsBackground = true;
        var timer = Stopwatch.StartNew();

        double previousTime = timer.Elapsed.TotalSeconds;
        double lagSeconds = 0;

        while (_gameTicker != null)
        {
          try
          {
            _gamePauseEvent.WaitOne();
          }
          catch (ThreadInterruptedException)
          {
            break;
          }
          double currentTime = timer.Elapsed.TotalSeconds;
          double elapsedSeconds = currentTime - previousTime;
          previousTime = currentTime;
          lagSeconds += elapsedSeconds;

          if (lagSeconds >= MAX_ELAPSED_TIME_BORDER)
            lagSeconds = 0;

          while (lagSeconds >= UPDATE_PERIOD_SECONDS)
          {
            _modelGame.Update((float)(UPDATE_PERIOD_SECONDS / lagSeconds), UPDATE_PERIOD_SECONDS);
            lagSeconds -= UPDATE_PERIOD_SECONDS;
          }

          PlayerUpdateHandlerEvent?.Invoke();
        }
      })
      {
        Name = "GAME_TICKER"
      };
      _gameTicker.Start();
    }
  }
}
