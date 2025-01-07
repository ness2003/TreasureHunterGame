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
  /// Контроллер главного меню для WPF.
  /// Управляет интерфейсом главного меню и взаимодействием с пользователем.
  /// </summary>
  public class ControllerMenuMainWPF : ControllerMenuMain, IWPFController
  {
    /// <summary>
    /// Конструктор контроллера главного меню для WPF.
    /// </summary>
    /// <param name="parMenuMain">Модель главного меню.</param>
    /// <param name="parControllerGame">Контроллер игры.</param>
    /// <param name="parControllerRules">Контроллер правил.</param>
    /// <param name="parTableOfRecordsController">Контроллер таблицы рекордов.</param>
    /// <param name="parControllerMenuPause">Контроллер меню паузы.</param>
    /// <param name="parControllerGameResult">Контроллер результата игры.</param>
    public ControllerMenuMainWPF(MenuMain parMenuMain, ControllerGame parControllerGame, ControllerRules parControllerRules, TableOfRecordsController parTableOfRecordsController, ControllerMenuPause parControllerMenuPause, ControllerGameResult parControllerGameResult)
        : base(parMenuMain, parControllerGame, parControllerRules, parTableOfRecordsController, parControllerMenuPause, parControllerGameResult)
    {
    }

    /// <summary>
    /// Запускает контроллер и отображает меню.
    /// </summary>
    public override void Start()
    {
      base.Start();
      _menuMainView = new ViewMenuWPF(_menuMain);
      _menuMainView.Draw();
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
    /// Обработчик нажатия клавиш для навигации по меню.
    /// </summary>
    /// <param name="parSender">Источник события (окно).</param>
    /// <param name="parArgs">Аргументы события (клавиша).</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      // Обработка нажатых клавиш для навигации по меню
      switch (parArgs.Key)
      {
        case Key.Up:
          _menuMain.SelectPrevItem();
          break;
        case Key.Down:
          _menuMain.SelectNextItem();
          break;
        case Key.Enter:
          _menuMain.EnterSelectedItem();
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
