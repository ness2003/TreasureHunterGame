using Controller.Menu;
using ViewConsole.Menu;
using Controller.Game;
using Controller.Rules;
using Controller.TableOfRecords;
using Model.Menu;
using Controller.PauseMenu;
using Controller.GameResult;

namespace ControllerConsole.Menu
{
  /// <summary>
  /// Контроллер главного меню для консольного интерфейса.
  /// Отвечает за отображение главного меню, обработку пользовательского ввода и переход к выбранным разделам.
  /// </summary>
  public class ControllerMenuMainConsole : ControllerMenuMain, IConsoleController
  {
    /// <summary>
    /// Инициализирует новый экземпляр контроллера главного меню для консоли.
    /// </summary>
    /// <param name="parMenuMain">Модель главного меню.</param>
    /// <param name="parControllerGame">Контроллер игры.</param>
    /// <param name="parControllerRules">Контроллер правил игры.</param>
    /// <param name="parTableOfRecordsController">Контроллер таблицы рекордов.</param>
    /// <param name="parControllerMenuPause">Контроллер паузы.</param>
    /// <param name="parControllerGameResult">Контроллер результатов игры.</param>
    public ControllerMenuMainConsole(
      MenuMain parMenuMain,
      ControllerGame parControllerGame,
      ControllerRules parControllerRules,
      TableOfRecordsController parTableOfRecordsController,
      ControllerMenuPause parControllerMenuPause,
      ControllerGameResult parControllerGameResult
    ) : base(parMenuMain, parControllerGame, parControllerRules, parTableOfRecordsController, parControllerMenuPause, parControllerGameResult)
    {
    }

    /// <summary>
    /// Запускает контроллер главного меню.
    /// Отображает главное меню и начинает обработку пользовательского ввода.
    /// </summary>
    public override void Start()
    {
      base.Start();
      // Создаём представление меню для консоли
      _menuMainView = new ViewMenuConsole(_menuMain);
      // Запускаем обработку ввода пользователя
      ProcessKeyPress();
    }

    /// <summary>
    /// Очищает консольное окно.
    /// </summary>
    public override void Clear()
    {
      Console.Clear();
    }

    /// <summary>
    /// Обрабатывает нажатия клавиш пользователя.
    /// Пользователь может перемещаться по пунктам меню с помощью стрелок и выбирать пункт клавишей Enter.
    /// </summary>
    public void ProcessKeyPress()
    {
      var needExit = false; // Флаг для завершения обработки ввода

      while (!needExit)
      {
        if (Console.KeyAvailable) // Проверяем, была ли нажата клавиша
        {
          ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Читаем клавишу, не отображая её на экране

          switch (keyInfo.Key)
          {
            case ConsoleKey.UpArrow:
              // Переключение на предыдущий пункт меню
              _menuMain.SelectPrevItem();
              break;

            case ConsoleKey.DownArrow:
              // Переключение на следующий пункт меню
              _menuMain.SelectNextItem();
              break;

            case ConsoleKey.Enter:
              // Подтверждение выбора пункта меню
              _menuMain.EnterSelectedItem();
              needExit = true; // Выход из цикла после выбора
              break;
          }
        }
      }
    }
  }
}
