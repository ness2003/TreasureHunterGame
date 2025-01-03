using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.GameObjects
{
  /// <summary>
  /// Перечисление типов объектов, которые могут быть использованы в игре.
  /// </summary>
  public enum ObjectType
  {
    /// <summary>
    /// Золотая монета.
    /// </summary>
    GoldCell,

    /// <summary>
    /// Серебряная монета.
    /// </summary>
    SilverCell,

    /// <summary>
    /// Бронзовая монета.
    /// </summary>
    BronzeCell,

    /// <summary>
    /// Магнит, который может влиять на другие объекты.
    /// </summary>
    Magnet,

    /// <summary>
    /// Таймер, добавляющий время на выполнение задания.
    /// </summary>
    Timer,

    /// <summary>
    /// Умножитель, увеличивающий очки.
    /// </summary>
    Mul,

    /// <summary>
    /// Вор, который может воровать объекты.
    /// </summary>
    Thief,

    /// <summary>
    /// Метеорит, который может воровать очки.
    /// </summary>
    Meteorite
  }
}
