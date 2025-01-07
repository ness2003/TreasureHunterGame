using Model.Game.GameObjects;
using Model.Game.Новая_папка;

namespace UnitTests
{
  [TestClass]
  public class GameObjectFactoryTests
  {
    // Тест для создания случайной монеты с правильным типом
    [TestMethod]
    public void CreateRandomCoin_ShouldCreateCoinWithValidType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайную монету
      GameObject coin = factory.CreateRandomCoin();

      // Assert: проверяем, что тип монеты корректен
      Assert.IsTrue(coin.ObjectType == ObjectType.GoldCell || coin.ObjectType == ObjectType.SilverCell || coin.ObjectType == ObjectType.BronzeCell);
      // Проверяем корректность других свойств
      Assert.AreEqual(defaultY, coin.Y);
      Assert.AreEqual(defaultSpeed, coin.Speed);
      Assert.AreEqual(defaultRadius, coin.Radius);
      Assert.AreEqual(fieldHeight, coin.GameFieldHeight);
      Assert.AreEqual(fieldWidth, coin.GameFieldWidth);
    }

    // Тест для создания случайного плохого объекта с правильным типом
    [TestMethod]
    public void CreateRandomBadObject_ShouldCreateBadObjectWithValidType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный плохой объект
      GameObject badObject = factory.CreateRandomBadObject();

      // Assert: проверяем, что тип плохого объекта корректен
      Assert.IsTrue(badObject.ObjectType == ObjectType.Thief || badObject.ObjectType == ObjectType.Meteorite);
      // Проверяем корректность других свойств
      Assert.AreEqual(defaultY, badObject.Y);
      Assert.AreEqual(defaultSpeed, badObject.Speed);
      Assert.AreEqual(defaultRadius, badObject.Radius);
      Assert.AreEqual(fieldHeight, badObject.GameFieldHeight);
      Assert.AreEqual(fieldWidth, badObject.GameFieldWidth);
    }

    // Тест для создания случайного бонуса с правильным типом
    [TestMethod]
    public void CreateRandomBonus_ShouldCreateBonusWithValidType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный бонус
      GameObject bonus = factory.CreateRandomBonus();

      // Assert: проверяем, что тип бонуса корректен
      Assert.IsTrue(bonus.ObjectType == ObjectType.Magnet || bonus.ObjectType == ObjectType.Timer || bonus.ObjectType == ObjectType.Mul);
      // Проверяем корректность других свойств
      Assert.AreEqual(defaultY, bonus.Y);
      Assert.AreEqual(defaultSpeed, bonus.Speed);
      Assert.AreEqual(defaultRadius, bonus.Radius);
      Assert.AreEqual(fieldHeight, bonus.GameFieldHeight);
      Assert.AreEqual(fieldWidth, bonus.GameFieldWidth);
    }

    // Тест на создание монеты с заданной скоростью и позицией
    [TestMethod]
    public void CreateRandomCoin_ShouldSetCorrectSpeedAndPosition()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 100;
      int defaultY = 100;
      int defaultSpeed = 10;
      int defaultRadius = 15;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайную монету
      GameObject coin = factory.CreateRandomCoin();

      // Assert: проверяем, что скорость и позиция монеты заданы корректно
      Assert.AreEqual(defaultSpeed, coin.Speed);
      Assert.AreEqual(defaultY, coin.Y);
    }

    // Тест на создание случайного плохого объекта с заданной скоростью и позицией
    [TestMethod]
    public void CreateRandomBadObject_ShouldSetCorrectSpeedAndPosition()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 100;
      int defaultY = 100;
      int defaultSpeed = 15;
      int defaultRadius = 20;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный плохой объект
      GameObject badObject = factory.CreateRandomBadObject();

      // Assert: проверяем, что скорость и позиция плохого объекта заданы корректно
      Assert.AreEqual(defaultSpeed, badObject.Speed);
      Assert.AreEqual(defaultY, badObject.Y);
    }

    // Тест на создание случайного бонуса с заданной скоростью и позицией
    [TestMethod]
    public void CreateRandomBonus_ShouldSetCorrectSpeedAndPosition()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 150;
      int defaultY = 150;
      int defaultSpeed = 8;
      int defaultRadius = 12;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный бонус
      GameObject bonus = factory.CreateRandomBonus();

      // Assert: проверяем, что скорость и позиция бонуса заданы корректно
      Assert.AreEqual(defaultSpeed, bonus.Speed);
      Assert.AreEqual(defaultY, bonus.Y);
    }

    // Тест на случайный выбор типов объектов для монет
    [TestMethod]
    public void CreateRandomCoin_ShouldChooseValidRandomType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайную монету
      GameObject coin = factory.CreateRandomCoin();

      // Assert: проверяем, что тип монеты был выбран случайным образом и корректен
      Assert.IsTrue(coin.ObjectType == ObjectType.GoldCell || coin.ObjectType == ObjectType.SilverCell || coin.ObjectType == ObjectType.BronzeCell);
    }

    // Тест на случайный выбор типов плохих объектов
    [TestMethod]
    public void CreateRandomBadObject_ShouldChooseValidRandomType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный плохой объект
      GameObject badObject = factory.CreateRandomBadObject();

      // Assert: проверяем, что тип плохого объекта был выбран случайным образом и корректен
      Assert.IsTrue(badObject.ObjectType == ObjectType.Thief || badObject.ObjectType == ObjectType.Meteorite);
    }

    // Тест на случайный выбор типов бонусов
    [TestMethod]
    public void CreateRandomBonus_ShouldChooseValidRandomType()
    {
      // Arrange: задаем параметры поля и объекта
      int fieldHeight = 500;
      int fieldWidth = 500;
      int defaultX = 50;
      int defaultY = 50;
      int defaultSpeed = 5;
      int defaultRadius = 10;
      GameObjectFactory factory = new GameObjectFactory(fieldHeight, fieldWidth, defaultX, defaultY, defaultSpeed, defaultRadius);

      // Act: создаем случайный бонус
      GameObject bonus = factory.CreateRandomBonus();

      // Assert: проверяем, что тип бонуса был выбран случайным образом и корректен
      Assert.IsTrue(bonus.ObjectType == ObjectType.Magnet || bonus.ObjectType == ObjectType.Timer || bonus.ObjectType == ObjectType.Mul);
    }
  }
}
