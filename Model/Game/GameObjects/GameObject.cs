using Model.Game.Strategy;
using System;

namespace Model.Game.GameObjects
{
  /// <summary>
  /// Класс, представляющий объект игры.
  /// </summary>
  public class GameObject
  {
    // Поля класса
    /// <summary>
    /// Высота игрового поля.
    /// </summary>
    private int _gameFieldHeight;

    /// <summary>
    /// Ширина игрового поля.
    /// </summary>
    private int _gameFieldWidth;

    /// <summary>
    /// Стратегия движения объекта.
    /// </summary>
    private IMoveStrategy _moveStrategy;

    /// <summary>
    /// Экземпляр генератора случайных чисел.
    /// </summary>
    private Random _random = new();

    // Свойства класса
    /// <summary>
    /// Позиция объекта по оси X.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Позиция объекта по оси Y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Радиус объекта.
    /// </summary>
    public int Radius { get; set; }

    /// <summary>
    /// Скорость объекта.
    /// </summary>
    public int Speed { get; set; }

    /// <summary>
    /// Высота игрового поля.
    /// </summary>
    public int GameFieldHeight { get => _gameFieldHeight; set => _gameFieldHeight = value; }

    /// <summary>
    /// Ширина игрового поля.
    /// </summary>
    public int GameFieldWidth { get => _gameFieldWidth; set => _gameFieldWidth = value; }

    /// <summary>
    /// Тип объекта.
    /// </summary>
    public ObjectType ObjectType { get; set; }

    /// <summary>
    /// Событие, которое вызывается при падении объекта.
    /// </summary>
    public event Action<GameObject, ObjectType>? onFall;

    // Конструктор
    /// <summary>
    /// Конструктор для инициализации нового объекта игры.
    /// </summary>
    /// <param name="parX">Позиция объекта по оси X.</param>
    /// <param name="parY">Позиция объекта по оси Y.</param>
    /// <param name="parSpeed">Скорость объекта.</param>
    /// <param name="parObjectType">Тип объекта.</param>
    /// <param name="parRadius">Радиус объекта.</param>
    /// <param name="parGameFieldHeight">Высота игрового поля.</param>
    /// <param name="parGameFieldWidth">Ширина игрового поля.</param>
    /// <param name="parMoveStrategy">Стратегия движения объекта.</param>
    public GameObject(int parX, int parY, int parSpeed, ObjectType parObjectType, int parRadius, int parGameFieldHeight, int parGameFieldWidth, IMoveStrategy parMoveStrategy)
    {
      X = parX;
      Y = parY;
      Speed = parSpeed;
      ObjectType = parObjectType;
      Radius = parRadius;
      _gameFieldHeight = parGameFieldHeight;
      _gameFieldWidth = parGameFieldWidth;
      RandomOffsetHorizontal();
      _moveStrategy = parMoveStrategy;
    }

    // Методы
    /// <summary>
    /// Создает копию текущего объекта.
    /// </summary>
    /// <returns>Клонированный объект игры.</returns>
    public GameObject Clone()
    {
      return new GameObject(X, Y, Speed, ObjectType, Radius, _gameFieldHeight, _gameFieldWidth, _moveStrategy);
    }

    /// <summary>
    /// Устанавливает случайное горизонтальное смещение для объекта.
    /// </summary>
    public void RandomOffsetHorizontal()
    {
      X = _random.Next(Radius * 2, _gameFieldWidth - Radius * 2);
    }

    /// <summary>
    /// Выполняет движение объекта, делегируя задачу стратегии.
    /// </summary>
    public void Move()
    {
      _moveStrategy.Move(this);
    }

    /// <summary>
    /// Устанавливает новую стратегию для движения объекта.
    /// </summary>
    /// <param name="parNewStrategy">Новая стратегия движения.</param>
    public void SetMoveStrategy(IMoveStrategy parNewStrategy)
    {
      _moveStrategy = parNewStrategy;
    }

    /// <summary>
    /// Вызывает событие onFall, если объект падает.
    /// </summary>
    public void TriggerFall()
    {
      onFall?.Invoke(this, ObjectType);
    }
  }
}
