using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
  /// <summary>
  /// Интерфейс контроллера
  /// </summary>
  public interface IController
  {
    /// <summary>
    /// Событие, возникающее при возврате на предыдущий экран
    /// </summary>
    event Action? GoToBack;
    /// <summary>
    /// Запуск
    /// </summary>
    void Start();
    /// <summary>
    /// Остановка
    /// </summary>
    void Stop();
    /// <summary>
    /// Вызов события GoToBack
    /// </summary>
    void GoBackCall();
  }
}
