using System.Windows.Input;
using Model.Menu;
using ViewWPF.Menu;
using ViewWPF;
using Controller.PauseMenu;

namespace ControllerWPF.Menu
{
  /// <summary>
  /// Контроллер меню паузы для WPF.
  /// Управляет отображением и взаимодействием с меню паузы.
  /// </summary>
  public class ControllerMenuPauseWPF : ControllerMenuPause, IWPFController
  {
    /// <summary>
    /// Конструктор контроллера меню паузы для WPF.
    /// </summary>
    /// <param name="parMenuPause">Модель меню паузы.</param>
    public ControllerMenuPauseWPF(MenuPause parMenuPause) : base(parMenuPause)
    {
    }

    /// <summary>
    /// Запускает контроллер и отображает меню паузы.
    /// </summary>
    public override void Start()
    {
      base.Start(); 
      _pauseMenuView = new ViewMenuWPF(_menuPause);
      _pauseMenuView.Draw();
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler; 
    }

    /// <summary>
    /// Останавливает контроллер и очищает подписку на события.
    /// </summary>
    public override void Stop()
    {
      base.Stop();
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler;
    }

    /// <summary>
    /// Обрабатывает нажатие клавиш для навигации по меню паузы.
    /// </summary>
    /// <param name="parSender">Источник события (окно).</param>
    /// <param name="parArgs">Аргументы события (клавиша).</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Up:
          _menuPause.SelectPrevItem();
          break;
        case Key.Down:
          _menuPause.SelectNextItem();
          break;
        case Key.Enter:
          _menuPause.EnterSelectedItem();
          break;
      }
    }

    /// <summary>
    /// Очищает экран от элементов управления и контента.
    /// </summary>
    public override void Clear()
    {
      MainScreen.GetInstance().StackPanel.Children.Clear();
    }
  }
}
