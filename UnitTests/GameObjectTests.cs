

namespace UnitTests
{
  // Простая заглушка для IMoveStrategy, чтобы можно было протестировать движение
  public class TestMoveStrategy : Model.Game.Strategy.IMoveStrategy
  {
    public void Move(Model.Game.GameObjects.GameObject gameObject)
    {
      // Просто перемещаем объект на 1 по оси Y
      gameObject.Y += 1;
    }
  }

  [TestClass]
  public class GameObjectTests
  {
    [TestMethod]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
      int x = 100;
      int y = 150;
      int speed = 5;
      var objectType = Model.Game.GameObjects.ObjectType.Mul;
      int radius = 10;
      int gameFieldHeight = 500;
      int gameFieldWidth = 500;
      var moveStrategy = new TestMoveStrategy();

      var gameObject = new Model.Game.GameObjects.GameObject(x, y, speed, objectType, radius, gameFieldHeight, gameFieldWidth, moveStrategy);

      Assert.AreEqual(y, gameObject.Y);
      Assert.AreEqual(speed, gameObject.Speed);
      Assert.AreEqual(objectType, gameObject.ObjectType);
      Assert.AreEqual(radius, gameObject.Radius);
      Assert.AreEqual(gameFieldHeight, gameObject.GameFieldHeight);
      Assert.AreEqual(gameFieldWidth, gameObject.GameFieldWidth);
    }

    [TestMethod]
    public void RandomOffsetHorizontal_ShouldSetRandomXWithinBounds()
    {
      int radius = 10;
      int gameFieldWidth = 500;
      var gameObject = new Model.Game.GameObjects.GameObject(0, 0, 5, Model.Game.GameObjects.ObjectType.Timer, radius, 500, gameFieldWidth, new TestMoveStrategy());

      gameObject.RandomOffsetHorizontal();

      Assert.IsTrue(gameObject.X >= radius * 2 && gameObject.X <= gameFieldWidth - radius * 2);
    }

    [TestMethod]
    public void Move_ShouldUpdateYPositionWhenMoveIsCalled()
    {
      var gameObject = new Model.Game.GameObjects.GameObject(0, 0, 5, Model.Game.GameObjects.ObjectType.GoldCell, 10, 500, 500, new TestMoveStrategy());

      int initialY = gameObject.Y;

      gameObject.Move();

      Assert.AreEqual(initialY + 1, gameObject.Y);
    }


    [TestMethod]
    public void TriggerFall_ShouldInvokeOnFallEvent()
    {
      var gameObject = new Model.Game.GameObjects.GameObject(0, 0, 5, Model.Game.GameObjects.ObjectType.BronzeCell, 10, 500, 500, new TestMoveStrategy());

      bool eventTriggered = false;
      gameObject.OnFall += (sender, objectType) => eventTriggered = true;

      gameObject.TriggerFall();

      Assert.IsTrue(eventTriggered);
    }

    [TestMethod]
    public void GameFieldDimensions_ShouldBeSetAndGetCorrectly()
    {
      var gameObject = new Model.Game.GameObjects.GameObject(0, 0, 5, Model.Game.GameObjects.ObjectType.Timer, 10, 500, 500, new TestMoveStrategy());

      Assert.AreEqual(500, gameObject.GameFieldHeight);
      Assert.AreEqual(500, gameObject.GameFieldWidth);

      gameObject.GameFieldHeight = 600;
      gameObject.GameFieldWidth = 600;

      Assert.AreEqual(600, gameObject.GameFieldHeight);
      Assert.AreEqual(600, gameObject.GameFieldWidth);
    }
  }
}

