using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model.Menu;
using ViewWPF.Menu;
using ViewWPF;
using Controller.Menu;
using ControllerWPF.Rules;
using ControllerWPF.Game;
using ControllerWPF.TableOfRecords;
using View.Menu;
using Controller.Game;
using Controller.Rules;
using Controller.TableOfRecords;
using Controller.PauseMenu;
using Controller.GameResult;

namespace ControllerWPF.Menu
{
  /// <summary>
  /// Контроллер меню для WPF
  /// </summary>
  public class ControllerMenuMainWPF : ControllerMenuMain, IWPFController
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerMenuMainWPF(MenuMain parMenuMain, ControllerGame parControllerGame, ControllerRules parControllerRules, TableOfRecordsController parTableOfRecordsController, ControllerMenuPause parControllerMenuPause, ControllerGameResult parControllerGameResult)
        : base(parMenuMain, parControllerGame, parControllerRules, parTableOfRecordsController, parControllerMenuPause, parControllerGameResult)
    {
    }

    /// <summary>
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      base.Start(); // Вызов базового метода Start
      _menuMainView = new ViewMenuWPF(_menuMain); // Инициализация представления
      _menuMainView.Draw(); // Отображение меню
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler; // Подключение обработчика событий нажатия клавиш
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
          _menuMain.SelectPrevItem(); // Выбор предыдущего пункта меню
          break;
        case Key.Down:
          _menuMain.SelectNextItem(); // Выбор следующего пункта меню
          break;
        case Key.Enter:
          _menuMain.EnterSelectedItem(); // Ввод выбранного пункта меню
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
