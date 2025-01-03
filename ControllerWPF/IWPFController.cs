using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControllerWPF
{
  /// <summary>
  /// Интерфейс для контроллеров WPF, обрабатывающих нажатия клавиш.
  /// </summary>
  public interface IWPFController
  {
    /// <summary>
    /// Обработчик нажатия клавиш.
    /// </summary>
    /// <param name="parSender">Источник события.</param>
    /// <param name="parArgs">Аргументы события.</param>
    void KeyEventHandler(object parSender, KeyEventArgs parArgs);
  }
}
