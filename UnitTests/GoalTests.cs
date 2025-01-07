using Model.Game.GameInfo;
using Model.Game.GameObjects;

namespace UnitTests
{
  [TestClass]
  public class GoalTests
  {
    // Тест на корректность работы паттерна Singleton для Goal
    [TestMethod]
    public void GetInstance_ShouldReturnSameInstance()
    {
      // Получаем два экземпляра Goal
      Goal goal1 = Goal.GetInstance();
      Goal goal2 = Goal.GetInstance();

      // Проверяем, что оба экземпляра указывают на одну и ту же ссылку
      Assert.AreSame(goal1, goal2);
    }

    // Тест на генерацию цели для уровня 1
    [TestMethod]
    public void GenerateGoal_ShouldGenerateCorrectGoalForLevel1()
    {
      // Создаем экземпляр Goal
      Goal goal = Goal.GetInstance();

      // Генерируем цель для уровня 1
      goal.GenerateGoal(1);

      // Проверяем, что цель для уровня 1 правильно сформирована
      Assert.AreEqual(4, goal.CountGoldCoins);  // Ожидается 4 золотых монеты
      Assert.AreEqual(5, goal.CountSilverCoins);  // Ожидается 5 серебряных монет
      Assert.AreEqual(5, goal.CountBronzeCoins);  // Ожидается 5 бронзовых монет
      Assert.AreEqual(270, goal.ScoreTarget);  // Ожидаемый целевой счет для уровня 1
    }

    // Тест на генерацию цели для уровня 5
    [TestMethod]
    public void GenerateGoal_ShouldGenerateCorrectGoalForLevel5()
    {
      // Создаем экземпляр Goal
      Goal goal = Goal.GetInstance();

      // Генерируем цель для уровня 5
      goal.GenerateGoal(5);

      // Проверяем правильность генерации цели для уровня 5
      Assert.AreEqual(12, goal.CountGoldCoins);  // Ожидается 12 золотых монет
      Assert.AreEqual(9, goal.CountSilverCoins);  // Ожидается 9 серебряных монет
      Assert.AreEqual(7, goal.CountBronzeCoins);  // Ожидается 7 бронзовых монет
      Assert.AreEqual(610, goal.ScoreTarget);  // Ожидаемый целевой счет для уровня 5
    }

    // Тест на уменьшение количества золотых монет при их сборе
    [TestMethod]
    public void ProcessCoin_ShouldDecreaseGoldCoinCount()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);
      int initialGoldCoins = goal.CountGoldCoins;

      // Обрабатываем золотую монету
      goal.ProcessCoin(ObjectType.GoldCell, 0);

      // Проверяем, что количество золотых монет уменьшилось на 1
      Assert.AreEqual(initialGoldCoins - 1, goal.CountGoldCoins);
    }

    // Тест на уменьшение количества серебряных монет при их сборе
    [TestMethod]
    public void ProcessCoin_ShouldDecreaseSilverCoinCount()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);
      int initialSilverCoins = goal.CountSilverCoins;

      // Обрабатываем серебряную монету
      goal.ProcessCoin(ObjectType.SilverCell, 0);

      // Проверяем, что количество серебряных монет уменьшилось на 1
      Assert.AreEqual(initialSilverCoins - 1, goal.CountSilverCoins);
    }

    // Тест на уменьшение количества бронзовых монет при их сборе
    [TestMethod]
    public void ProcessCoin_ShouldDecreaseBronzeCoinCount()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);
      int initialBronzeCoins = goal.CountBronzeCoins;

      // Обрабатываем бронзовую монету
      goal.ProcessCoin(ObjectType.BronzeCell, 0);

      // Проверяем, что количество бронзовых монет уменьшилось на 1
      Assert.AreEqual(initialBronzeCoins - 1, goal.CountBronzeCoins);
    }

    // Тест на срабатывание события OnGoalCompleted при достижении цели
    [TestMethod]
    public void ProcessCoin_ShouldTriggerOnGoalCompletedEvent_WhenGoalIsAchieved()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);
      int targetScore = goal.ScoreTarget; // Целевой счет
      int score = 0;

      // Подписываемся на событие OnGoalCompleted
      bool eventTriggered = false;
      goal.OnGoalCompleted += () => eventTriggered = true;

      // Собираем монеты, увеличивая счет
      for (int i = 0; i < goal.CountGoldCoins; i++)
      {
        goal.ProcessCoin(ObjectType.GoldCell, score);
        score += 4;
      }
      for (int i = 0; i < goal.CountSilverCoins; i++)
      {
        goal.ProcessCoin(ObjectType.SilverCell, score);
        score += 2;
      }
      for (int i = 0; i < goal.CountBronzeCoins; i++)
      {
        goal.ProcessCoin(ObjectType.BronzeCell, score);
        score += 1;
      }

      // Проверяем, что событие не сработало до достижения целевого счета
      Assert.IsFalse(eventTriggered, "Событие не должно быть вызвано, пока счет не достигает целевого.");

      // Увеличиваем счет до целевого значения
      goal.ProcessCoin(ObjectType.GoldCell, score);

      // Проверяем, что событие сработало после достижения цели
      Assert.IsTrue(eventTriggered, "Событие должно быть вызвано, когда цель достигнута.");
    }

    // Тест на отсутствие срабатывания события при недостаточном счете
    [TestMethod]
    public void ProcessCoin_ShouldNotTriggerOnGoalCompletedEvent_WhenScoreIsNotSufficient()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);
      int insufficientScore = goal.ScoreTarget - 1;  // Недостаточный счет

      // Подписываемся на событие OnGoalCompleted
      bool eventTriggered = false;
      goal.OnGoalCompleted += () => eventTriggered = true;

      // Обрабатываем монету с недостаточным счетом
      goal.ProcessCoin(ObjectType.GoldCell, insufficientScore);

      // Проверяем, что событие не было вызвано
      Assert.IsFalse(eventTriggered);
    }

    // Тест на отсутствие срабатывания события, если монеты собраны, но счет недостаточен
    [TestMethod]
    public void ProcessCoin_ShouldNotTriggerOnGoalCompletedEvent_WhenCoinsAreCollectedButScoreIsNotEnough()
    {
      // Создаем экземпляр Goal и генерируем цель для уровня 1
      Goal goal = Goal.GetInstance();
      goal.GenerateGoal(1);

      // Подписываемся на событие OnGoalCompleted
      bool eventTriggered = false;
      goal.OnGoalCompleted += () => eventTriggered = true;

      // Собираем монеты, но счет не достигает целевого
      goal.ProcessCoin(ObjectType.GoldCell, 0);
      goal.ProcessCoin(ObjectType.SilverCell, 0);
      goal.ProcessCoin(ObjectType.BronzeCell, 0);

      // Проверяем, что событие не было вызвано
      Assert.IsFalse(eventTriggered);
    }
  }
}
