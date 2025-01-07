using Model.Game.GameInfo;
using Model.Game.GameObjects;
using Model.Game;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Game.Новая_папка;

namespace UnitTests
{
  [TestClass]
  public class ModelGameTests
  {
    // Метод для создания новой модели игры с начальной конфигурацией
    private ModelGame CreateModelGame()
    {
      var width = 1000; // Ширина экрана игры
      var height = 500; // Высота экрана игры
      var player = new Player(width / 2, height - 100, 100, 100, 3, 1000, 500); // Создание игрока

      var score = 0;
      var level = 1;
      var coins = new List<GameObject>();
      var bonuses = new List<GameObject>();
      var badObjects = new List<GameObject>();
      var goal = Goal.GetInstance(); // Получение цели игры
      var random = new Random();
      var factory = new GameObjectFactory(height, width, 0, 0, 5, 20);

      // Возвращаем объект модели игры с нужными параметрами
      return new ModelGame(player, goal, coins, bonuses, badObjects, factory, random, width, height, level, score);
    }

    // Проверяем, что событие GameStarted вызывается при старте игры
    [TestMethod]
    public void StartGame_ShouldInvokeGameStartedEvent()
    {
      var modelGame = CreateModelGame();
      bool eventTriggered = false;
      modelGame.GameStarted += () => eventTriggered = true;

      modelGame.StartGame(); // Действие: запуск игры

      Assert.IsTrue(eventTriggered, "Событие GameStarted не было вызвано."); // Проверка, что событие произошло
    }

    // Проверяем, что событие GameFinished вызывается при завершении игры
    [TestMethod]
    public void FinishGame_ShouldInvokeGameFinishedEvent()
    {
      var modelGame = CreateModelGame();
      bool eventTriggered = false;
      modelGame.GameFinished += () => eventTriggered = true;

      modelGame.FinishGame(); // Действие: завершение игры

      Assert.IsTrue(eventTriggered, "Событие GameFinished не было вызвано."); // Проверка, что событие произошло
    }

    // Проверяем, что время игры уменьшается при обновлении
    [TestMethod]
    public void UpdateTime_ShouldReduceRemainingTime()
    {
      var modelGame = CreateModelGame();
      float initialRemainingTime = modelGame.RemainingTime;

      modelGame.Update(1.0f, 1.0f); // Действие: обновление игры с временем 1.0f

      Assert.IsTrue(modelGame.RemainingTime < initialRemainingTime, "Время не уменьшилось."); // Проверка уменьшения времени
    }

    // Проверяем, что событие OnTimesUp вызывается, когда время истекает
    [TestMethod]
    public void TimesUp_ShouldInvokeOnTimesUpEvent()
    {
      var modelGame = CreateModelGame();
      bool eventTriggered = false;
      modelGame.OnTimesUp += () => eventTriggered = true;

      modelGame.ElapsedTime = 120f; // Устанавливаем время истечения

      modelGame.Update(1.0f, 1.0f); // Действие: обновление игры

      Assert.IsTrue(eventTriggered, "Событие OnTimesUp не было вызвано."); // Проверка, что событие сработало
    }


    // Проверяем, что при генерации объекта он добавляется в соответствующий список
    [TestMethod]
    public void GenerateRandomObject_ShouldAddObjectToCorrectList()
    {
      var modelGame = CreateModelGame();
      int initialCoinCount = modelGame.Coins.Count;
      int initialBadObjectCount = modelGame.BadObjects.Count;
      int initialBonusCount = modelGame.Bonuses.Count;

      modelGame.GenerateRandomObject(); // Действие: генерация случайного объекта

      // Проверка, что хотя бы один список изменился
      Assert.IsTrue(modelGame.Coins.Count > initialCoinCount ||
                    modelGame.BadObjects.Count > initialBadObjectCount ||
                    modelGame.Bonuses.Count > initialBonusCount, "Объект не был добавлен в игру.");
    }

    // Проверяем, что при поимке золотой монеты увеличивается счет на 4
    [TestMethod]
    public void HandleCatch_GoldCoin_ShouldIncreaseScoreBy4()
    {
      var modelGame = CreateModelGame();
      var initialScore = modelGame.Score;
      var goldCoin = new GameObject(0, 0, 0, ObjectType.GoldCell, 10, 500, 1000, null);
      modelGame.Coins.Add(goldCoin); // Добавляем золотую монету в коллекцию монет

      modelGame.HandleCatch(ObjectType.GoldCell); // Действие: поимка золотой монеты

      Assert.AreEqual(initialScore + 4, modelGame.Score, "Счет не увеличился на 4 для GoldCell."); // Проверка увеличения счета
    }

    // Проверяем, что при поимке метеорита счет уменьшается на 10 на первом уровне
    [TestMethod]
    public void HandleCatch_Meteorite_ShouldDecreaseScoreForLevel1()
    {
      var modelGame = CreateModelGame();
      modelGame.Level = 1; // Уровень 1
      var initialScore = modelGame.Score;
      var meteorite = new GameObject(0, 0, 0, ObjectType.Meteorite, 10, 500, 1000, null);
      modelGame.BadObjects.Add(meteorite); // Добавляем метеорит в плохие объекты

      modelGame.HandleCatch(ObjectType.Meteorite); // Действие: поимка метеорита

      Assert.AreEqual(initialScore - 10, modelGame.Score, "Счет не уменьшился на 10 для Meteorite."); // Проверка уменьшения счета
    }

    // Проверяем, что при поимке вора все объекты очищаются
    [TestMethod]
    public void HandleCatch_Thief_ShouldClearAllObjects()
    {
      var modelGame = CreateModelGame();
      modelGame.Coins.Add(new GameObject(0, 0, 0, ObjectType.GoldCell, 10, 500, 1000, null));
      modelGame.Bonuses.Add(new GameObject(0, 0, 0, ObjectType.Timer, 10, 500, 1000, null));
      modelGame.BadObjects.Add(new GameObject(0, 0, 0, ObjectType.Thief, 10, 500, 1000, null));

      modelGame.HandleCatch(ObjectType.Thief); // Действие: поимка вора

      // Проверка, что все объекты очищены
      Assert.AreEqual(0, modelGame.Coins.Count, "Коллекция монет не была очищена.");
      Assert.AreEqual(0, modelGame.Bonuses.Count, "Коллекция бонусов не была очищена.");
      Assert.AreEqual(0, modelGame.BadObjects.Count, "Коллекция плохих объектов не была очищена.");
    }
  }
}
