using System.Windows.Input;
using Controller.Rules;
using ViewWPF.Rules;
using ViewWPF;
using Model.Rules;

namespace ControllerWPF.Rules
{
  /// <summary>
  /// Контроллер для отображения правил игры в WPF.
  /// Управляет процессом отображения правил и обработкой пользовательских действий.
  /// </summary>
  public class ControllerRulesWPF : ControllerRules, IWPFController
  {
    /// <summary>
    /// Конструктор контроллера для просмотра правил игры.
    /// </summary>
    /// <param name="parModelRules">Модель правил игры.</param>
    public ControllerRulesWPF(ModelRules parModelRules) : base(parModelRules)
    {
    }

    /// <summary>
    /// Обработчик нажатий клавиш для управления меню правил.
    /// </summary>
    /// <param name="parSender">Источник события (окно).</param>
    /// <param name="parArgs">Аргументы события (информация о нажатой клавише).</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Escape:
          Stop();
          GoBackCall();
          break;
      }
    }

    /// <summary>
    /// Запускает контроллер и отображает правила игры.
    /// </summary>
    public override void Start()
    {
      _viewRules = new ViewRulesWPF(_modelRules);
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler;
    }

    /// <summary>
    /// Останавливает контроллер, очищает содержимое экрана и отписывается от событий клавиш.
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler;
      MainScreen.GetInstance().StackPanel.Children.Clear();
    }
  }
}
