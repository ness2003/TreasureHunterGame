using Model.Game.GameObjects;

namespace Model.Game.Strategy
{
  /// <summary>
  /// Стратегия для перемещения объекта к заданной цели.
  /// </summary>
  public class MoveToGoalStrategy : IMoveStrategy
  {
    /// <summary>
    /// Координата X цели
    /// </summary>
    private float _goalX;
    /// <summary>
    /// Координата Y цели
    /// </summary>
    private float _goalY;

    /// <summary>
    /// Инициализирует стратегию перемещения объекта к цели.
    /// </summary>
    /// <param name="parGoalX">Координата X цели.</param>
    /// <param name="parGoalY">Координата Y цели.</param>
    public MoveToGoalStrategy(float parGoalX, float parGoalY)
    {
      _goalX = parGoalX;
      _goalY = parGoalY;
    }

    /// <summary>
    /// Выполняет движение объекта к цели.
    /// </summary>
    /// <param name="parGameObject">Объект игры, который должен двигаться.</param>
    public void Move(GameObject parGameObject)
    {
      // Вычисляем вектор направления от объекта к цели
      float offsetX = _goalX - parGameObject.X;
      float offsetY = _goalY - parGameObject.Y;

      // Находим расстояние (или длину вектора)
      float distance = (float)Math.Sqrt(offsetX * offsetX + offsetY * offsetY);

      // Если объект близок к цели, останавливаем его движение
      if (distance < 5f)
      {
        parGameObject.X = (int)_goalX;
        parGameObject.Y = (int)_goalY;
      }
      else
      {
        // Нормализуем вектор (чтобы длина была равна 1)
        offsetX /= distance;
        offsetY /= distance;

        // Перемещаем объект к цели с фиксированной скоростью
        float speed = parGameObject.Speed;
        parGameObject.X += (int)(offsetX * speed);
        parGameObject.Y += (int)(offsetY * speed);
      }
    }
  }
}
