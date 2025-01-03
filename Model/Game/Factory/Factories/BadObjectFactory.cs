using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Game.Strategy;

namespace Model.Game.Новая_папка.Factories
{
  /// <summary>
  /// Фабрика для создания объектов типа "плохой объект" (Bad Object).
  /// </summary>
  public class BadObjectFactory : GameObjectFactory
  {
    /// <summary>
    /// Создаёт объект типа GameObject, представляющий плохой объект (Bad Object), с заданными параметрами.
    /// </summary>
    /// <param name="parX">Координата X начальной позиции объекта.</param>
    /// <param name="parY">Координата Y начальной позиции объекта.</param>
    /// <param name="parSpeed">Скорость объекта.</param>
    /// <param name="parRadius">Радиус объекта.</param>
    /// <param name="parGameFieldHeight">Высота игрового поля.</param>
    /// <param name="parGameFieldWidth">Ширина игрового поля.</param>
    /// <returns>Созданный объект типа GameObject, представляющий плохой объект.</returns>
    public override GameObject CreateGameObject(int parX, int parY, int parSpeed, int parRadius, int parGameFieldHeight, int parGameFieldWidth)
    {
      // Генерация случайного типа плохого объекта из перечисления ObjectType
      ObjectType randomType = (ObjectType)new Random().Next((int)ObjectType.Thief, (int)ObjectType.Meteorite + 1);

      // Возвращаем новый объект GameObject с соответствующими параметрами
      return new GameObject(parX, parY, parSpeed, randomType, parRadius, parGameFieldHeight, parGameFieldWidth, new MoveDownStrategy());
    }
  }
}
