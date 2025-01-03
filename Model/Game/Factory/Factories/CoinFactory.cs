using Model.Game.GameObjects;
using Model.Game.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.Новая_папка.Factories
{
  /// <summary>
  /// Фабрика для создания объектов типа "монета" (Coin).
  /// </summary>
  public class CoinFactory : GameObjectFactory
  {
    /// <summary>
    /// Создаёт объект типа GameObject, представляющий монету (Coin), с заданными параметрами.
    /// </summary>
    /// <param name="parX">Координата X начальной позиции объекта.</param>
    /// <param name="parY">Координата Y начальной позиции объекта.</param>
    /// <param name="parSpeed">Скорость объекта.</param>
    /// <param name="parRadius">Радиус объекта.</param>
    /// <param name="parGameFieldHeight">Высота игрового поля.</param>
    /// <param name="parGameFieldWidth">Ширина игрового поля.</param>
    /// <returns>Созданный объект типа GameObject, представляющий монету.</returns>
    public override GameObject CreateGameObject(int parX, int parY, int parSpeed, int parRadius, int parGameFieldHeight, int parGameFieldWidth)
    {
      // Генерация случайного типа объекта из перечисления ObjectType
      ObjectType randomType = (ObjectType)new Random().Next((int)ObjectType.GoldCell, (int)ObjectType.BronzeCell + 1);

      // Возвращаем новый объект GameObject с соответствующими параметрами
      return new GameObject(parX, parY, parSpeed, randomType, parRadius, parGameFieldHeight, parGameFieldWidth, new MoveDownStrategy());
    }
  }
}
