using Model.Game.GameObjects;
using Model.Game.Strategy;

namespace Model.Game.Новая_папка
{
  /// <summary>
  /// Фабрика для создания игровых объектов с заранее заданными параметрами.
  /// </summary>
  public class GameObjectFactory
  {
    /// <summary>
    /// Высота игрового поля.
    /// </summary>
    private readonly int _gameFieldHeight;

    /// <summary>
    /// Ширина игрового поля.
    /// </summary>
    private readonly int _gameFieldWidth;

    /// <summary>
    /// Начальная координата X для игровых объектов.
    /// </summary>
    private readonly int _defaultX;

    /// <summary>
    /// Начальная координата Y для игровых объектов.
    /// </summary>
    private readonly int _defaultY;

    /// <summary>
    /// Скорость для игровых объектов.
    /// </summary>
    private readonly int _defaultSpeed;

    /// <summary>
    /// Радиус для игровых объектов.
    /// </summary>
    private readonly int _defaultRadius;

    /// <summary>
    /// Экземпляр случайного генератора для выбора случайных значений.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Инициализирует новый экземпляр фабрики объектов.
    /// </summary>
    /// <param name="parGameFieldHeight">Высота игрового поля.</param>
    /// <param name="parGameFieldWidth">Ширина игрового поля.</param>
    /// <param name="parDefaultX">Начальная координата X для игровых объектов.</param>
    /// <param name="parDefaultY">Начальная координата Y для игровых объектов.</param>
    /// <param name="parDefaultSpeed">Cкорость для игровых объектов.</param>
    /// <param name="parDefaultRadius">Радиус для игровых объектов.</param>
    public GameObjectFactory(
        int parGameFieldHeight,
        int parGameFieldWidth,
        int parDefaultX,
        int parDefaultY,
        int parDefaultSpeed,
        int parDefaultRadius)
    {
      _gameFieldHeight = parGameFieldHeight;
      _gameFieldWidth = parGameFieldWidth;
      _defaultX = parDefaultX;
      _defaultY = parDefaultY;
      _defaultSpeed = parDefaultSpeed;
      _defaultRadius = parDefaultRadius;
      _random = new Random();
    }

    /// <summary>
    /// Создаёт случайную монету (золотую, серебряную или бронзовую).
    /// </summary>
    /// <returns>Возвращает созданный объект монеты.</returns>
    public GameObject CreateRandomCoin()
    {
      var coinTypes = new List<ObjectType>
            {
                ObjectType.GoldCell,
                ObjectType.SilverCell,
                ObjectType.BronzeCell
            };
      var selectedType = coinTypes[_random.Next(coinTypes.Count)];
      return new GameObject(
          _defaultX,
          _defaultY,
          _defaultSpeed,
          selectedType,
          _defaultRadius,
          _gameFieldHeight,
          _gameFieldWidth,
          new MoveDownStrategy()
      );
    }

    /// <summary>
    /// Создаёт случайный плохой объект (вор или метеорит).
    /// </summary>
    /// <returns>Возвращает созданный объект плохого объекта.</returns>
    public GameObject CreateRandomBadObject()
    {
      var badObjectTypes = new List<ObjectType>
            {
                ObjectType.Thief,
                ObjectType.Meteorite
            };
      var selectedType = badObjectTypes[_random.Next(badObjectTypes.Count)];
      return new GameObject(
          _defaultX,
          _defaultY,
          _defaultSpeed,
          selectedType,
          _defaultRadius,
          _gameFieldHeight,
          _gameFieldWidth,
          new MoveDownStrategy()
      );
    }

    /// <summary>
    /// Создаёт случайный бонус (магнит, таймер, мультипликатор).
    /// </summary>
    /// <returns>Возвращает созданный объект бонуса.</returns>
    public GameObject CreateRandomBonus()
    {
      var bonusTypes = new List<ObjectType>
            {
                ObjectType.Magnet,
                ObjectType.Timer,
                ObjectType.Mul
            };

      var selectedType = bonusTypes[_random.Next(bonusTypes.Count)];
      return new GameObject(
          _defaultX,
          _defaultY,
          _defaultSpeed,
          selectedType,
          _defaultRadius,
          _gameFieldHeight,
          _gameFieldWidth,
          new MoveDownStrategy()
      );
    }
  }
}
