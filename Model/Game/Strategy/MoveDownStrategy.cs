using Model.Game.GameObjects;

namespace Model.Game.Strategy
{
  /// <summary>
  /// Стратегия для перемещения объекта вниз по экрану.
  /// </summary>
  public class MoveDownStrategy : IMoveStrategy
  {
    /// <summary>
    /// Выполняет движение объекта вниз по экрану.
    /// </summary>
    /// <param name="parGameObject">Объект игры, который должен быть перемещён.</param>
    public void Move(GameObject parGameObject)
    {
      // Проверка, достиг ли объект нижней границы игрового поля
      if (parGameObject.Y + 2 * parGameObject.Radius == parGameObject.GameFieldHeight)
      {
        parGameObject.TriggerFall();  // Вызов метода для события падения
      }
      else
      {
        parGameObject.Y++;
      }
    }
  }
}
