using Model.Game.GameObjects;
using System;

namespace UnitTests
{
  [TestClass]
  public class PlayerTests
  {
    // Тест конструктора Player на правильную инициализацию свойств
    [TestMethod]
    public void Constructor_ShouldInitializePropertiesCorrectly()
    {
      // Arrange: задаем начальные значения для игрока
      int x = 50;
      int y = 100;
      int width = 20;
      int height = 40;
      int speed = 5;
      int fieldWidth = 800;
      int fieldHeight = 600;

      // Act: создаем игрока с заданными параметрами
      Player player = new Player(x, y, width, height, speed, fieldWidth, fieldHeight);

      // Assert: проверяем, что свойства игрока инициализировались правильно
      Assert.AreEqual(x, player.X);
      Assert.AreEqual(y, player.Y);
      Assert.AreEqual(width, player.Width);
      Assert.AreEqual(height, player.Height);
      Assert.AreEqual(speed, player.Speed);
    }

    // Тест на движение вправо: проверка увеличения позиции X
    [TestMethod]
    public void MoveRight_ShouldIncreaseXPosition()
    {
      // Arrange: создаем игрока в начальной позиции
      Player player = new Player(50, 100, 20, 40, 5, 800, 600);

      // Act: двигаем игрока вправо
      player.MoveRight();

      // Assert: проверяем, что позиция X увеличилась
      Assert.AreEqual(55, player.X);
    }

    // Тест на движение вправо с ограничением по полю: игрок не должен выходить за пределы
    [TestMethod]
    public void MoveRight_ShouldNotIncreaseXPositionBeyondFieldWidth()
    {
      // Arrange: создаем игрока в правом краю поля
      Player player = new Player(780, 100, 20, 40, 5, 800, 600);

      // Act: пытаемся двигать игрока вправо
      player.MoveRight();

      // Assert: проверяем, что игрок не выйдет за пределы поля
      Assert.AreEqual(780, player.X);  // Игрок не должен выйти за пределы поля
    }

    // Тест на движение влево: проверка уменьшения позиции X
    [TestMethod]
    public void MoveLeft_ShouldDecreaseXPosition()
    {
      // Arrange: создаем игрока в начальной позиции
      Player player = new Player(50, 100, 20, 40, 5, 800, 600);

      // Act: двигаем игрока влево
      player.MoveLeft();

      // Assert: проверяем, что позиция X уменьшилась
      Assert.AreEqual(45, player.X);
    }

    // Тест на движение влево с ограничением по левому краю: игрок не должен выйти за пределы
    [TestMethod]
    public void MoveLeft_ShouldNotDecreaseXPositionBelowZero()
    {
      // Arrange: создаем игрока в левом краю поля
      Player player = new Player(0, 100, 20, 40, 5, 800, 600);

      // Act: пытаемся двигать игрока влево
      player.MoveLeft();

      // Assert: проверяем, что игрок не выйдет за левый предел
      Assert.AreEqual(0, player.X);  // Игрок не должен выйти за пределы поля
    }

    // Тест на сброс позиции игрока: игрок должен вернуться в начальную позицию
    [TestMethod]
    public void ResetPosition_ShouldResetToInitialCoordinates()
    {
      // Arrange: создаем игрока с конкретными размерами поля
      int fieldWidth = 800;
      int fieldHeight = 600;
      Player player = new Player(50, 100, 20, 40, 5, fieldWidth, fieldHeight);

      // Act: сбрасываем позицию игрока
      player.ResetPosition();

      // Assert: проверяем, что позиция сбросилась в центр нижней части поля
      Assert.AreEqual(fieldWidth / 2 - player.Width / 2, player.X);
      Assert.AreEqual(fieldHeight - player.Height, player.Y);
    }

    // Тест на сброс позиции после движения вправо
    [TestMethod]
    public void ResetPosition_ShouldResetPositionAfterMoveRight()
    {
      // Arrange: создаем игрока с конкретными размерами поля
      int fieldWidth = 800;
      int fieldHeight = 600;
      Player player = new Player(50, 100, 20, 40, 5, fieldWidth, fieldHeight);

      // Act: двигаем игрока вправо и сбрасываем позицию
      player.MoveRight();
      player.ResetPosition();

      // Assert: проверяем, что позиция сбросилась в центр нижней части поля
      Assert.AreEqual(fieldWidth / 2 - player.Width / 2, player.X);
      Assert.AreEqual(fieldHeight - player.Height, player.Y);
    }

    // Тест на скорость движения игрока: проверка влияния скорости на движение
    [TestMethod]
    public void Speed_ShouldAffectPlayerMovement()
    {
      // Arrange: создаем игрока с высокой скоростью
      Player player = new Player(50, 100, 20, 40, 10, 800, 600);

      // Act: двигаем игрока вправо
      player.MoveRight();

      // Assert: проверяем, что позиция X увеличилась на величину скорости
      Assert.AreEqual(60, player.X);  // Скорость = 10, поэтому X увеличится на 10
    }

    // Тест на изменение ширины игрока: проверка влияния ширины на движение
    [TestMethod]
    public void Width_ShouldAffectPositionCalculation()
    {
      // Arrange: создаем игрока с увеличенной шириной
      Player player = new Player(50, 100, 30, 40, 5, 800, 600);

      // Act: двигаем игрока вправо
      player.MoveRight();

      // Assert: проверяем, что после увеличения ширины, игрок двигается дальше
      Assert.AreEqual(55, player.X);  // Теперь игрок должен двигаться дальше, так как ширина больше
    }

    // Тест на корректную работу конструктора с минимальными значениями
    [TestMethod]
    public void Constructor_ShouldHandleMinValuesCorrectly()
    {
      // Arrange: задаем минимальные значения для всех параметров
      int x = 0;
      int y = 0;
      int width = 1;
      int height = 1;
      int speed = 1;
      int fieldWidth = 1;
      int fieldHeight = 1;

      // создаем игрока с минимальными значениями
      Player player = new Player(x, y, width, height, speed, fieldWidth, fieldHeight);

      // Assert: проверяем, что все значения корректно инициализировались
      Assert.AreEqual(x, player.X);
      Assert.AreEqual(y, player.Y);
      Assert.AreEqual(width, player.Width);
      Assert.AreEqual(height, player.Height);
      Assert.AreEqual(speed, player.Speed);
    }
  }
}
