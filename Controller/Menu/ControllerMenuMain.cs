using Controller.Game;
using Controller.GameResult;
using Controller.PauseMenu;
using Controller.Rules;
using Controller.TableOfRecords;
using Model.Menu;
using View.Menu;

namespace Controller.Menu
{
  /// <summary>
  /// Абстрактный класс основного контроллера меню.
  /// </summary>
  public abstract class ControllerMenuMain : IController
  {
    /// <summary>
    /// Модель главного меню.
    /// </summary>
    protected MenuMain _menuMain;

    /// <summary>
    /// Представление главного меню.
    /// </summary>
    protected ViewMenu? _menuMainView;

    /// <summary>
    /// Событие для возврата назад.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Событие для создания элемента "Продолжить игру".
    /// </summary>
    public event Action? CreateContinueGameElement;

    /// <summary>
    /// Событие для удаления элемента "Продолжить игру".
    /// </summary>
    public event Action? RemoveContinueGameElement;

    /// <summary>
    /// Контроллер игры.
    /// Управляет игровым процессом.
    /// </summary>
    public ControllerGame ControllerGame { get; set; }

    /// <summary>
    /// Контроллер правил.
    /// </summary>
    public ControllerRules ControllerRules { get; set; }

    /// <summary>
    /// Контроллер меню паузы.
    /// </summary>
    public ControllerMenuPause ControllerMenuPause { get; set; }

    /// <summary>
    /// Контроллер таблицы рекордов.
    /// </summary>
    public TableOfRecordsController TableOfRecordsController { get; set; }

    /// <summary>
    /// Контроллер записи результата игры.
    /// </summary>
    public ControllerGameResult ControllerGameResult { get; set; }

    /// <summary>
    /// Конструктор контроллера главного меню.
    /// </summary>
    /// <param name="parMenuMain">Экземпляр модели главного меню.</param>
    /// <param name="parControllerGame">Экземпляр контроллера игры.</param>
    /// <param name="parControllerRules">Экземпляр контроллера правил.</param>
    /// <param name="parTableOfRecordsController">Экземпляр контроллера таблицы рекордов.</param>
    /// <param name="parControllerMenuPause">Экземпляр контроллера меню паузы.</param>
    /// <param name="parControllerGameResult">Экземпляр контроллера результатов игры.</param>
    public ControllerMenuMain(MenuMain parMenuMain,
        ControllerGame parControllerGame,
        ControllerRules parControllerRules,
        TableOfRecordsController parTableOfRecordsController,
        ControllerMenuPause parControllerMenuPause,
        ControllerGameResult parControllerGameResult)
    {
      _menuMain = parMenuMain;
      ControllerGame = parControllerGame;
      ControllerRules = parControllerRules;
      TableOfRecordsController = parTableOfRecordsController;
      ControllerMenuPause = parControllerMenuPause;
      ControllerGameResult = parControllerGameResult;

      // Подписки на события
      ControllerGame.OnGameInterrupred += CreateContinueGameElementCall;
      ControllerGame.OnGameEnded += RemoveContinueGameElementCall;
      ControllerGame.GoToBack += Start;
      ControllerGame.ChangeScoreAndLevel += ControllerGameResult.ChangeScoreAndLevel;

      //ControllerGame.GoToGameResult += RemoveContinueGameElementCall;
      ControllerGame.GoToGameResult += ControllerGameResult.Start;
      ControllerGame.OnGamePaused += ControllerGame.Stop;
      ControllerGame.OnGamePaused += ControllerMenuPause.Start;
      ControllerGameResult.GoToBack += Start;
      ControllerRules.GoToBack += Start;
      ControllerMenuPause.GoToBack += ControllerGame.Start;
      ControllerMenuPause.GoToMainMenu += ControllerMenuPause.Stop;
      ControllerMenuPause.GoToMainMenu += CreateContinueGameElementCall;
      ControllerMenuPause.GoToMainMenu += Start;
      TableOfRecordsController.GoToBack += Start;

      // Настройка обработчиков для элементов меню
      _menuMain[(int)MenuMain.MenuIds.Rules].Enter += Stop;
      _menuMain[(int)MenuMain.MenuIds.Records].Enter += Stop;
      _menuMain[(int)MenuMain.MenuIds.New].Enter += Stop;
      _menuMain[(int)MenuMain.MenuIds.Exit].Enter += Stop;
      _menuMain[(int)MenuMain.MenuIds.Rules].Enter += ControllerRules.Start;
      _menuMain[(int)MenuMain.MenuIds.Records].Enter += TableOfRecordsController.Start;
      _menuMain[(int)MenuMain.MenuIds.New].Enter += ControllerGame.Start;
      _menuMain[(int)MenuMain.MenuIds.Exit].Enter += Exit;
      GoToBack += Stop;
    }

    /// <summary>
    /// Инициализация контроллера меню.
    /// Отображает главное меню и настраивает дополнительные элементы.
    /// </summary>
    public virtual void Start()
    {
      Clear();
    }

    /// <summary>
    /// Создание элемента меню "Продолжить игру"
    /// </summary>
    public void CreateContinueGameElementCall()
    {
      if (_menuMain.Items.Length == 4)
      {
        _menuMain[(int)MenuMain.MenuIds.New].Name = "Продолжить игру";
        _menuMain.AddMenuItemNewGame();
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter += Stop;
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter += ControllerGame.ResetCall;
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter += ControllerGame.Start;
      }
    }

    /// <summary>
    /// Удаление элемента меню "Продолжить игру"
    /// </summary>
    public void RemoveContinueGameElementCall()
    {
      if (_menuMain.Items.Length == 5)
      {
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter -= Stop;
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter -= ControllerGame.ResetCall;
        _menuMain[(int)MenuMain.MenuIds.NewGame].Enter -= ControllerGame.Start;
        _menuMain.RemoveLastItem();
        _menuMain[(int)MenuMain.MenuIds.New].Name = "Новая игра";
      }
    }

    /// <summary>
    /// Абстрактный метод для очистки ресурсов контроллера.
    /// </summary>
    public abstract void Clear();

    /// <summary>
    /// Остановка контроллера.
    /// </summary>
    public virtual void Stop()
    {
      _menuMain.UnsubscribeAll();
      Clear();
    }

    /// <summary>
    /// Остановка приложения.
    /// </summary>
    public void Exit()
    {
      Environment.Exit(0);
    }

    /// <summary>
    /// Вызов события возврата.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }
  }
}
