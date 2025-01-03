using System;
using Controller.Menu;
using View.Menu;
using ViewConsole.Menu;
using ControllerConsole.Rules;
using ControllerConsole.Game;
using Controller.Game;
using Controller.Rules;
using Controller.TableOfRecords;
using Model.Menu;
using Controller.PauseMenu;
using Controller.GameResult;
using System.Windows.Input;

namespace ControllerConsole.Menu
{
  public class ControllerMenuMainConsole : ControllerMenuMain, IConsoleController
  {
    /// <summary>
    /// Флаг продолжения нахождения в меню
    /// </summary>
    private volatile bool _needExit = false;

    /// <summary>
    /// Конструктор
    /// </summary>
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
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      base.Start();
      _menuMainView = new ViewMenuConsole(_menuMain);
      _menuMainView.Draw();
      _needExit = false;
      ProcessKeyPress();
    }

    /// <summary>
    /// Остановить контроллер
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      _needExit = true;
    }

    /// <summary>
    /// Очистить консольное меню
    /// </summary>
    public override void Clear()
    {
      Console.Clear();
    }

    /// <summary>
    /// Обработка нажатий клавиш
    /// </summary>
    public void ProcessKeyPress()
    {
      while (!_needExit)
      {
        if (Console.KeyAvailable)
        {
          ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

          switch (keyInfo.Key)
          {
            case ConsoleKey.UpArrow:
              _menuMain.SelectPrevItem();
              break;

            case ConsoleKey.DownArrow:
              _menuMain.SelectNextItem();
              break;

            case ConsoleKey.Enter:
              _menuMain.EnterSelectedItem();
              break;

          }
        }
      }
    }
  }
}
