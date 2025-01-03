using Controller;
using Controller.GameResult;
using Controller.Rules;
using Model.GameResult;
using Model.TableOfRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using View.GameResult;
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
          Stop(); // Остановить при нажатии Enter
          break;
      }
    }

    /// <summary>
    /// Запуск контроллера
    /// </summary>
    public override void Start()
    {
      MainScreen.GetInstance().StackPanel.Children.Clear(); // Очистка экрана
      _viewGameResult = new ViewGameResultWPF(_modelGameResult); // Создание представления результата игры
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler; // Подключение обработчика клавиш
    }

    /// <summary>
    /// Остановка контроллера
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler; // Отключение обработчика клавиш

      TextBox name = ((ViewGameResultWPF)_viewGameResult).Name; // Получение имени игрока
      ModelTableOfRecords.Instance.Add(new Record(name.Text, _modelGameResult.Score, _modelGameResult.Level)); // Добавление записи в таблицу рекордов
      GoBackCall(); // Возврат на предыдущий экран
    }
  }
}
