using Controller.PauseMenu;
using ViewConsole.Menu;

namespace ControllerConsole.MenuPause
{
  /// <summary>
  /// Контроллер меню паузы для консольной версии.
  /// </summary>
  public class ControllerMenuPauseConsole : ControllerMenuPause
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuPause">Модель меню паузы</param>
    public ControllerMenuPauseConsole(Model.Menu.MenuPause parMenuPause) : base(parMenuPause)
    {
    }

    /// <summary>
    /// Запуск контроллера.
    /// </summary>
    public override void Start()
    {
      base.Start();
      _pauseMenuView = new ViewMenuConsole(_menuPause);
      ProcessKeyPress();
    }

    /// <summary>
    /// Обработка нажатия клавиш.
    /// </summary>
    public void ProcessKeyPress()
    {
      var needExit = false;
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            _menuPause.SelectPrevItem();
            break;
          case ConsoleKey.DownArrow:
            _menuPause.SelectNextItem();
            break;
          case ConsoleKey.Enter:
            _menuPause.EnterSelectedItem();
            needExit = true;
            break;
          case ConsoleKey.Escape:
            needExit = true;
            break;
        }

      } while (!needExit);
    }

    /// <summary>
    /// Очистка консольного экрана.
    /// </summary>
    public override void Clear()
    {
      Console.Clear(); // Очищаем экран консоли
    }
  }
}
