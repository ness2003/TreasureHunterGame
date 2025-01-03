using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.Новая_папка
{
  /// <summary>
  /// Абстрактный класс фабрики для создания игровых объектов.
  /// </summary>
  public abstract class GameObjectFactory
  {
    /// <summary>
    /// Абстрактный метод для создания игрового объекта.
    /// </summary>
    /// <param name="parX">Координата X начальной позиции объекта.</param>
    /// <param name="parY">Координата Y начальной позиции объекта.</param>
    /// <param name="parSpeed">Скорость объекта.</param>
    /// <param name="parRadius">Радиус объекта.</param>
    /// <param name="parGameFieldHeight">Высота игрового поля.</param>
    /// <param name="parGameFieldWidth">Ширина игрового поля.</param>
    /// <returns>Созданный объект типа GameObject.</returns>
    public abstract GameObject CreateGameObject(int parX, int parY, int parSpeed, int parRadius, int parGameFieldHeight, int parGameFieldWidth);
  }
}
