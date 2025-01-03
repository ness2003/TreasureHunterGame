using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.Strategy
{
  /// <summary>
  /// Интерфейс, определяющий стратегию движения для объектов игры.
  /// </summary>
  public interface IMoveStrategy
  {
    /// <summary>
    /// Метод для выполнения движения объекта в соответствии с заданной стратегией.
    /// </summary>
    /// <param name="parGameObject">Объект игры, который должен быть перемещён.</param>
    void Move(GameObject parGameObject);
  }
}
