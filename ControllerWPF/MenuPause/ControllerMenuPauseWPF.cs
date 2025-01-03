using System.Windows.Input;
using Model.Menu;
using ViewWPF.Menu;
using ViewWPF;
using Controller.PauseMenu;

namespace ControllerWPF.Menu
{
  /// <summary>
  /// Контроллер меню для WPF
  /// </summary>
  public class ControllerMenuPauseWPF : ControllerMenuPause, IWPFController
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerMenuPauseWPF(MenuPause parMenuPause) : base(parMenuPause)
    {
    }

    /// <summary>
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      base.Start(); // Вызов базового метода Start
      _pauseMenuView = new ViewMenuWPF(_menuPause); // Инициализация представления для паузы
      _pauseMenuView.Draw(); // Отображение меню паузы
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler; // Подключение обработчика событий клавиш
    }

    /// <summary>
    /// Остановить контроллер
    /// </summary>
    public override void Stop()
    {
      base.Stop(); // Вызов базового метода Stop
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler; // Отключение обработчика событий
    }

    /// <summary>
    /// Обработчик нажатия кнопок
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Аргументы события</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Up:
          _menuPause.SelectPrevItem(); // Выбор предыдущего пункта меню паузы
          break;
        case Key.Down:
          _menuPause.SelectNextItem(); // Выбор следующего пункта меню паузы
          break;
        case Key.Enter:
          _menuPause.EnterSelectedItem(); // Ввод выбранного пункта меню паузы
          break;
      }
    }

    /// <summary>
    /// Очистить экран
    /// </summary>
    public override void Clear()
    {
      MainScreen.GetInstance().StackPanel.Children.Clear(); // Очистка содержимого экрана
    }
  }
}
