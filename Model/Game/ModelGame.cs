using Model.Game.GameObjects;
using Model.Game.Новая_папка.Factories;
using Model.Game.Новая_папка;
using System;
using Model.Game.Strategy;
using System.Windows;
using Model.Game.GameInfo;


namespace Model.Game
{
  /// <summary>
  /// Представляет собой игровую модель, управляет состоянием игры и взаимодействием с объектами.
  /// </summary>
  public class ModelGame
  {

   /// <summary>
   /// Фабрика монет.
   /// </summary>
    private GameObjectFactory _coinFactory = new CoinFactory();
    /// <summary>
    /// Фабрика бонусов.
    /// </summary>
    private GameObjectFactory _bonusFactory = new BonusFactory();
    /// <summary>
    /// Фабрика плохих объектов.
    /// </summary>
    private GameObjectFactory _badObjectFactory = new BadObjectFactory();

    /// <summary>
    /// Длительность действия магнита (в секундах)
    /// </summary>
    private const float MAGNET_DURATION = 5f;
    /// <summary>
    /// Время на уровень (в секундах)
    /// </summary>
    private const float TIME_FOR_LEVEL = 60f;

    /// <summary>
    /// Время, прошедшее с последнего обновления
    /// </summary>
    private float deltaTime;
    /// <summary>
    /// Шаг обновления
    /// </summary>
    private float deltaUpdate = 0;
    /// <summary>
    /// Время с последнего спавна объектов
    /// </summary>
    private float _timeSinceLastSpawn = 0;  
    /// <summary>
    /// Время активации магнита
    /// </summary>
    private float magnetActivationTime = 0f;

    /// <summary>
    /// Цель текущего уровня.
    /// </summary>
    public Goal CurrentGoal { get; set; }

    /// <summary>
    /// Игрок.
    /// </summary>
    public Player Player { get; set; }

    /// <summary>
    /// Список монет.
    /// </summary>
    public List<GameObject>? Coins { get; set; }

    /// <summary>
    /// Список бонусов.
    /// </summary>
    public List<GameObject>? Bonuses { get; set; }

    /// <summary>
    /// Список плохих объектов.
    /// </summary>
    public List<GameObject>? BadObjects { get; set; }

    /// <summary>
    /// Ширина игрового поля.
    /// </summary>
    public int Width { get; set; } = 1000;

    /// <summary>
    /// Высота игрового поля.
    /// </summary>
    public int Height { get; set; } = 500;

    /// <summary>
    /// Радиус объектов.
    /// </summary>
    public int ObjectsRadius { get; set; } = 20;

    /// <summary>
    /// Уровень игры.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Очки игрока.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Время, прошедшее с начала игры.
    /// </summary>
    public float ElapsedTime { get; private set; } = 0;

    /// <summary>
    /// Оставшееся время.
    /// </summary>
    public float RemainingTime { get; private set; } = 120;

    /// <summary>
    /// Дополнительное время.
    /// </summary>
    public float AdditionTime { get; private set; } = 0;

    /// <summary>
    /// Флаг, указывающий, активен ли магнит.
    /// </summary>
    public bool IsMagnetActive { get; set; } = false;

    /// <summary>
    /// Генератор случайных чисел.
    /// </summary>
    public Random Random { get; set; }

    /// <summary>
    /// Событие, вызываемое при начале игры.
    /// </summary>
    public event Action? GameStarted;

    /// <summary>
    /// Событие, вызываемое при окончании игры.
    /// </summary>
    public event Action? GameFinished;

    /// <summary>
    /// Событие, вызываемое, когда время игры истекло.
    /// </summary>
    public event Action? OnTimesUp;

    /// <summary>
    /// Делегат, вызываемый для перерисовки экрана.
    /// </summary>
    public delegate void dNeedRedraw();

    /// <summary>
    /// Событие, вызываемое для перерисовки экрана.
    /// </summary>
    public event dNeedRedraw? NeedRedraw;


    /// <summary>
    /// Создаёт новый экземпляр игры с заданными параметрами.
    /// </summary>
    /// <param name="parPlayer">Игрок, управляемый пользователем.</param>
    /// <param name="parGoal">Цель уровня, которую необходимо достичь.</param>
    /// <param name="parCoins">Список игровых объектов, представляющих монеты.</param>
    /// <param name="parBonuses">Список игровых объектов, представляющих бонусы.</param>
    /// <param name="parBadObjects">Список игровых объектов, представляющих опасные препятствия.</param>
    /// <param name="parCoinFactory">Фабрика для создания объектов-монет.</param>
    /// <param name="parBonusFactory">Фабрика для создания объектов-бонусов.</param>
    /// <param name="parBadObjectFactory">Фабрика для создания опасных объектов.</param>
    /// <param name="parRandom">Генератор случайных чисел для случайного размещения объектов.</param>
    /// <param name="parWidth">Ширина игрового поля (по умолчанию 1000).</param>
    /// <param name="parHeight">Высота игрового поля (по умолчанию 500).</param>
    /// <param name="parLevel">Начальный уровень игры (по умолчанию 1).</param>
    /// <param name="parScore">Начальное количество очков (по умолчанию 0).</param>
    public ModelGame(
        Player parPlayer,
        Goal parGoal,
        List<GameObject> parCoins,
        List<GameObject> parBonuses,
        List<GameObject> parBadObjects,
        GameObjectFactory parCoinFactory,
        GameObjectFactory parBonusFactory,
        GameObjectFactory parBadObjectFactory,
        Random parRandom,
        int parWidth = 1000,
        int parHeight = 500,
        int parLevel = 1,
        int parScore = 0)
    {
      Player = parPlayer;
      CurrentGoal = parGoal;
      Coins = parCoins;
      Bonuses = parBonuses;
      BadObjects = parBadObjects;
      _coinFactory = parCoinFactory;
      _bonusFactory = parBonusFactory;
      _badObjectFactory = parBadObjectFactory;
      Random = parRandom;
      Width = parWidth;
      Height = parHeight;
      Level = parLevel;
      Score = parScore;

      // Подписка на событие завершения цели уровня.
      CurrentGoal.GoalCompleted += UpdateLevel;
    }

    /// <summary>
    /// Запуск игры.
    /// </summary>
    public void StartGame()
    {
      GameStarted?.Invoke();
    }

    /// <summary>
    /// Завершение игры.
    /// </summary>
    public void FinishGame()
    {
      GameFinished?.Invoke();
    }

    /// <summary>
    /// Обновляет состояние игры на каждом кадре.
    /// </summary>
    /// <param name="parDeltaUpdate">Шаг обновления логики.</param>
    /// <param name="parDeltaTime">Время, прошедшее с последнего кадра (временной шаг анимации).</param>
    public void Update(float parDeltaUpdate, float parDeltaTime)
    {
      deltaTime = parDeltaTime;
      deltaUpdate += parDeltaUpdate;

      UpdateTime(parDeltaTime);
      SpawnObjects(parDeltaTime);
      UpdateObjectMovements();
      UpdateMagnet(parDeltaTime);

      NeedRedraw?.Invoke();
    }

    /// <summary>
    /// Обновляет таймер игры и проверяет окончание уровня.
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего кадра.</param>
    private void UpdateTime(float parDeltaTime)
    {
      ElapsedTime += parDeltaTime;
      RemainingTime = TIME_FOR_LEVEL - (ElapsedTime - TIME_FOR_LEVEL * (Level - 1)) + AdditionTime;

      // Если время истекло, вызываем событие завершения уровня.
      if (RemainingTime <= 0)
      {
        Application.Current.Dispatcher.Invoke(TimesUp);
      }
    }

    /// <summary>
    /// Спавнит новые игровые объекты по истечению интервала времени.
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего кадра.</param>
    private void SpawnObjects(float parDeltaTime)
    {
      _timeSinceLastSpawn += parDeltaTime;

      // Если прошло достаточно времени, создаём новый объект.
      if (_timeSinceLastSpawn >= 1.0f)
      {
        GenerateRandomObject();
        _timeSinceLastSpawn = 0;
      }
    }

    /// <summary>
    /// Обновляет положение игровых объектов на экране.
    /// </summary>
    private void UpdateObjectMovements()
    {
      MoveObjects(Coins);
      MoveObjects(Bonuses);
      MoveObjects(BadObjects);
    }

    /// <summary>
    /// Обновляет состояние магнита, притягивающего объекты к игроку.
    /// </summary>
    /// <param name="parDeltaTime">Время, прошедшее с последнего кадра.</param>
    private void UpdateMagnet(float parDeltaTime)
    {
      if (!IsMagnetActive) return;

      // Притягиваем монеты и бонусы к игроку.
      AttractObjects(Coins);
      AttractObjects(Bonuses);

      // Обновляем таймер магнита.
      magnetActivationTime += parDeltaTime;
      if (magnetActivationTime >= MAGNET_DURATION)
      {
        IsMagnetActive = false;
        magnetActivationTime = 0f;
      }
    }



    /// <summary>
    /// Вызов события "Время вышло"
    /// </summary>
    public void TimesUp()
    {
      OnTimesUp?.Invoke();
    }
    /// <summary>
    /// Сбрасывает состояние игры до начального состояния.
    /// </summary>
    public void Reset()
    {
      ClearAllObjects();
      Level = 1;
      Score = 0;
      ElapsedTime = 0;
      RemainingTime = 12;
      AdditionTime = 0;
      IsMagnetActive = false;
      magnetActivationTime = 0f;
      Player.ResetPosition();
      CurrentGoal.GenerateGoal(parLevel: Level);
    }


    /// <summary>
    /// Перемещение объектов на экране.
    /// </summary>
    /// <param name="parObjects">Список объектов для перемещения.</param>
    private void MoveObjects(List<GameObject> parObjects)
    {
      foreach (var obj in parObjects.ToList())
      {
        obj.Move();  // Делегируем движение стратегии
        if (PlayerCollidesWithObject(parObj: obj))
        {
          HandleCatch(parValue: obj.ObjectType);
          obj.onFall -= HandleFall;
          parObjects.Remove(obj);
        }
      }
    }

    /// <summary>
    /// Притягивает объекты к игроку.
    /// </summary>
    private void AttractObjects(List<GameObject> objects)
    {
      foreach (var obj in objects)
      {
        obj.SetMoveStrategy(new MoveToGoalStrategy(Player.X + Player.Width / 2, Player.Y + Player.Height / 2));
      }
    }

    /// <summary>
    /// Генерация случайного объекта
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public void GenerateRandomObject()
    {
      int randomType = Random.Next(10); // 0-9 диапазон

      GameObject newObject = randomType switch
      {
        <= 6 => _coinFactory.CreateGameObject(0, 0, 2, ObjectsRadius, Height, Width), // 0-6: монеты (70%)
        <= 8 => _badObjectFactory.CreateGameObject(0, 0, 2, ObjectsRadius, Height, Width), // 7-8: плохие объекты (20%)
        9 => _bonusFactory.CreateGameObject(0, 0, 2, ObjectsRadius, Height, Width), // 9: бонусы (10%)
        _ => throw new ArgumentException("Unknown object type")
      };

      newObject.onFall += HandleFall;

      if (IsMagnetActive && randomType <= 6) // Магнит действует только на монеты
      {
        newObject.SetMoveStrategy(new MoveToGoalStrategy(Player.X + Player.Width / 2, Player.Y + Player.Height / 2));
      }
      else
      {
        newObject.SetMoveStrategy(new MoveDownStrategy());
      }

      switch (newObject.ObjectType)
      {
        case ObjectType.GoldCell:
        case ObjectType.SilverCell:
        case ObjectType.BronzeCell:
          Coins?.Add(newObject);
          break;
        case ObjectType.Magnet:
        case ObjectType.Timer:
        case ObjectType.Mul:
          Bonuses?.Add(newObject);
          break;
        case ObjectType.Thief:
        case ObjectType.Meteorite:
          BadObjects?.Add(newObject);
          break;
      }
    }

    /// <summary>
    /// Обновление уровня игры.
    /// </summary>
    public void UpdateLevel()
    {
      Level++;
      CurrentGoal.GenerateGoal(parLevel: Level);
    }

    /// <summary>
    /// Обработчик события падения объекта.
    /// </summary>
    /// <param name="parModel">Модель объекта.</param>
    /// <param name="parCellType">Тип объекта.</param>
    private void HandleFall(GameObject parModel, ObjectType parCellType)
    {
      switch (parCellType)
      {
        case ObjectType.GoldCell:
        case ObjectType.SilverCell:
        case ObjectType.BronzeCell:
          Coins?.Remove(parModel);
          break;

        case ObjectType.Magnet:
        case ObjectType.Timer:
        case ObjectType.Mul:
          Bonuses?.Remove(parModel);
          break;

        case ObjectType.Thief:
        case ObjectType.Meteorite:
          BadObjects?.Remove(parModel);
          break;
      }
      parModel.onFall -= HandleFall;
    }

    /// <summary>
    /// Проверка на столкновение игрока с объектом.
    /// </summary>
    /// <param name="parObj">Объект для проверки столкновения.</param>
    /// <returns>Возвращает true, если столкновение произошло.</returns>
    private bool PlayerCollidesWithObject(GameObject parObj)
    {
      int playerCenterX = Player.X + Player.Width / 2;
      int playerCenterY = Player.Y + Player.Height / 2;

      int closestX = Math.Clamp(parObj.X, Player.X, Player.X + Player.Width);
      int closestY = Math.Clamp(parObj.Y, Player.Y, Player.Y + Player.Height);

      int distanceX = parObj.X - closestX;
      int distanceY = parObj.Y - closestY;

      double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
      return distance < parObj.Radius;
    }

    /// <summary>
    /// Обработчик действий игрока, когда он поймал объект.
    /// </summary>
    /// <param name="parValue">Тип пойманного объекта.</param>
    public void HandleCatch(ObjectType parValue)
    {
      switch (parValue)
      {
        case ObjectType.GoldCell: Score += 4; break;
        case ObjectType.SilverCell: Score += 2; break;
        case ObjectType.BronzeCell: Score++; break;
        case ObjectType.Mul: Score *= 2; break;
        case ObjectType.Timer: AdditionTime += 10; break;
        case ObjectType.Magnet: IsMagnetActive = true; magnetActivationTime = 0f; break;
        case ObjectType.Meteorite: Score -= 10; break;
        case ObjectType.Thief: ApplyThiefEffect(); break;
      }
      CurrentGoal.ProcessCoin(parValue, Score);
    }

    /// <summary>
    /// Применение эффекта вора.
    /// </summary>
    private void ApplyThiefEffect()
    {
      ClearAllObjects();
    }

    /// <summary>
    /// Очистка всех объектов.
    /// </summary>
    private void ClearAllObjects()
    {
      for (int i = 0; i < Coins?.Count; i++)
      {
        GameObject? coin = Coins[i];
        coin.onFall -= HandleFall;
      }
      Coins?.Clear();

      for (int j = 0; j < Bonuses?.Count; j++)
      {
        GameObject? bonus = Bonuses[j];
        bonus.onFall -= HandleFall;
      }
      Bonuses?.Clear();

      for (int k = 0; k < BadObjects?.Count; k++)
      {
        GameObject? badObject = BadObjects[k];
        badObject.onFall -= HandleFall;
      }
      BadObjects?.Clear();
    }

    /// <summary>
    /// Перемещение игрока влево.
    /// </summary>
    public void MovePlayerLeft()
    {
      if (deltaUpdate >= 1)
      {
        Player.MoveLeft();
        deltaUpdate = 0;
      }
    }

    /// <summary>
    /// Перемещение игрока вправо.
    /// </summary>
    public void MovePlayerRight()
    {
      if (deltaUpdate >= 1)
      {
        Player.MoveRight();
        deltaUpdate = 0;
      }
    }
  }
}

