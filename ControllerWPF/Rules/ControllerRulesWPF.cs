using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Controller;
using Controller.Rules;
using ViewWPF.Rules;
using ViewWPF;
using Model.Rules;
using View.Rules;

namespace ControllerWPF.Rules
{
  /// <summary>
  /// Контроллер просмотра правил для WPF
  /// </summary>
  public class ControllerRulesWPF : ControllerRules, IWPFController
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerRulesWPF(ModelRules parModelRules) : base(parModelRules)
    {
    }

    /// <summary>
    /// Обработчик нажатий клавиш
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Аргументы события</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Escape:
          Stop(); // Остановка контроллера при нажатии Escape
          GoBackCall(); // Возврат в предыдущее меню
          break;
      }
    }

    /// <summary>
    /// Запуск контроллера
    /// </summary>
    public override void Start()
    {
      _viewRules = new ViewRulesWPF(_modelRules); // Инициализация представления правил
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler; // Подключение обработчика событий клавиш
    }

    /// <summary>
    /// Остановка контроллера
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler; // Отключение обработчика событий
      MainScreen.GetInstance().StackPanel.Children.Clear(); // Очистка содержимого экрана
    }
  }
}
