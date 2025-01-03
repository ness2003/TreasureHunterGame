using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.Strategy
{
  /// <summary>
  /// Стратегия для перемещения объекта к заданной цели.
  /// </summary>
  public class MoveToGoalStrategy : IMoveStrategy
  {
    private float goalX;
    private float goalY;

    /// <summary>
    /// Инициализирует стратегию перемещения объекта к цели.
    /// </summary>
    /// <param name="parGoalX">Координата X цели.</param>
    /// <param name="parGoalY">Координата Y цели.</param>
    public MoveToGoalStrategy(float parGoalX, float parGoalY)
    {
      this.goalX = parGoalX;
      this.goalY = parGoalY;
    }

    /// <summary>
    /// Выполняет движение объекта к цели.
    /// </summary>
    /// <param name="parGameObject">Объект игры, который должен двигаться.</param>
    public void Move(GameObject parGameObject)
    {
      // Вычисляем вектор направления от объекта к цели
      float offsetX = goalX - parGameObject.X;
      float offsetY = goalY - parGameObject.Y;

      // Находим расстояние (или длину вектора)
      float distance = (float)Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

      // Если объект близок к цели, останавливаем его движение
      if (distance < 5f)
      {
        parGameObject.X = (int)goalX;
        parGameObject.Y = (int)goalY;
      }
      else
      {
        // Нормализуем вектор (чтобы длина была равна 1)
        offsetX /= distance;
        offsetY /= distance;

        // Перемещаем объект к цели с фиксированной скоростью
        float speed = 10f;
        parGameObject.X += (int)(offsetX * speed);
        parGameObject.Y += (int)(offsetY * speed);
      }
    }
  }
}
