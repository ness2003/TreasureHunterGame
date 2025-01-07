using Controller.GameResult;
using Model.GameResult;
using Model.TableOfRecords;
using System.Windows.Controls;
using System.Windows.Input;
using ViewWPF;
using ViewWPF.GameResult;

namespace ControllerWPF.GameResult
{
  /// <summary>
  /// Контроллер для результата игры в WPF
  /// </summary>
  public class ControllerGameResultWPF : ControllerGameResult, IWPFController
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModelGameResult">Модель результата игры</param>
    public ControllerGameResultWPF(ModelGameResult parModelGameResult) : base(parModelGameResult)
    {
    }

    /// <summary>
    /// Обработчик нажатия клавиш
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Аргументы события</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Enter:
          Stop(); 
          break;
      }
    }

    /// <summary>
    /// Запуск контроллера
    /// </summary>
    public override void Start()
    {
      MainScreen.GetInstance().StackPanel.Children.Clear();
      _viewGameResult = new ViewGameResultWPF(_modelGameResult);
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler;
    }

    /// <summary>
    /// Остановка контроллера
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler; 

      TextBox name = ((ViewGameResultWPF)_viewGameResult).Name;
      ModelTableOfRecords.Instance.Add(new Record(name.Text, _modelGameResult.Score, _modelGameResult.Level)); // Добавление записи в таблицу рекордов
      GoBackCall(); // Возврат на предыдущий экран
    }
  }
}
