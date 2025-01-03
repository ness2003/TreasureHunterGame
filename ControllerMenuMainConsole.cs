using System;

/// <summary>
/// Контроллер главного меню
/// </summary>
public class ControllerMenuMainConsole : ControllerMenuMain, IConsoleController
{
  /// <summary>
  /// Конструктор
  /// </summary>
  public ControllerMenuMainConsole()
  {
    _menuMainView = new ViewConsole.Menu.ViewMenuConsole(_menuMain);
    _menuMain[(int)Model.Menu.MenuMain.MenuIds.Exit].Enter += () => { Environment.Exit(0); };
  }

  /// <summary>
  /// Запустить контроллер
  /// </summary>
  public override void Start()
  {
    _menuMainView = new ViewConsole.Menu.ViewMenuConsole(_menuMain);
    ProcessKeyPress();
  }

  public override void Stop()
  {
    _menuMain.UnsubscribeAll();
  }

  /// <summary>
  /// Запустить просмотр правил
  /// </summary>
  protected override void CallRules()
  {
    Stop();
    new ControllerRulesConsole(this).Start();
  }
  /// <summary>
  /// Запустить просмотр рекордов
  /// </summary>
  protected override void CallRecords()
  {
    //ControllerRecordsConsole controller = new ControllerRecordsConsole(this);
    //controller.ProcessMessages();
    //_menuMainView.Draw();

  }
  /// <summary>
  /// Запустить новую игру
  /// </summary>
  protected override void CallGame()
  {
    //GameControllerConsole controller = new GameControllerConsole();
    //controller.ProcessMessages();
    //_menuMainView.Draw();
  }
  /// <summary>
  /// Запустить контроллер
  /// </summary>
  public void ProcessKeyPress()
  {
    do
    {
      ConsoleKeyInfo keyInfo = Console.ReadKey();

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

    } while (true);
  }
}
