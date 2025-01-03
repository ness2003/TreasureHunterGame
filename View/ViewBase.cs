using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
  /// <summary>
  /// Базовый абстрактный класс для всех представлений, который содержит метод Draw для отрисовки.
  /// </summary>
  public abstract class ViewBase
  {
    /// <summary>
    /// Вызывает отрисовку
    /// </summary>
    public abstract void Draw();
  }
}
