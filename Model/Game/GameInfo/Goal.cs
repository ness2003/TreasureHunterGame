using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Game.GameInfo
{
  /// <summary>
  /// Класс, который описывает цель игры. Цель включает в себя количество монет разных типов и целевой счет.
  /// </summary>
  public class Goal
  {    /// <summary>
       /// Единственный экземпляр класса Goal (Singleton).
       /// </summary>
    private static Goal? _instance;

    /// <summary>
    /// Количество золотых монет, которые необходимо собрать для достижения цели.
    /// </summary>
    private int _countGoldCoins;

    /// <summary>
    /// Количество серебряных монет, которые необходимо собрать для достижения цели.
    /// </summary>
    private int _countSilverCoins;

    /// <summary>
    /// Количество бронзовых монет, которые необходимо собрать для достижения цели.
    /// </summary>
    private int _countBronzeCoins;

    /// <summary>
    /// Целевой счет, который необходимо набрать для завершения уровня.
    /// </summary>
    private int _scoreTarget;

    /// <summary>
    /// Событие, которое срабатывает при достижении цели.
    /// </summary>
    public event Action? GoalCompleted;

    /// <summary>
    /// Количество золотых монет, которые необходимо собрать для достижения цели.
    /// </summary>
    public int CountGoldCoins { get => _countGoldCoins; private set => _countGoldCoins = value; }

    /// <summary>
    /// Количество серебряных монет, которые необходимо собрать для достижения цели.
    /// </summary>
    public int CountSilverCoins { get => _countSilverCoins; private set => _countSilverCoins = value; }

    /// <summary>
    /// Количество бронзовых монет, которые необходимо собрать для достижения цели.
    /// </summary>
    public int CountBronzeCoins { get => _countBronzeCoins; private set => _countBronzeCoins = value; }

    /// <summary>
    /// Целевой счет, который необходимо набрать для завершения игры.
    /// </summary>
    public int ScoreTarget { get => _scoreTarget; private set => _scoreTarget = value; }

    /// <summary>
    /// Флаг, который указывает, выполнена ли цель по набранному счету.
    /// </summary>
    public bool ScoreTargetGoalPassed { get; set; }

    /// <summary>
    /// Получить единственный экземпляр класса Goal.
    /// </summary>
    /// <returns>Экземпляр класса Goal.</returns>
    public static Goal GetInstance()
    {
      if (_instance == null)
      {
        _instance = new Goal();
        _instance.GenerateGoal(1);
        
      }
      return _instance;
    }

    /// <summary>
    /// Генерирует цель игры на основе уровня.
    /// </summary>
    /// <param name="parLevel">Уровень игры, на котором устанавливается цель.</param>
    public void GenerateGoal(int parLevel)
    {
      CountGoldCoins = 2 + parLevel * 2;
      CountSilverCoins = 4 + parLevel;
      CountBronzeCoins = 5 + parLevel / 2;
      ScoreTarget = (int)((CountGoldCoins * 3 + CountSilverCoins * 2 + CountBronzeCoins) * 10);
    }

    /// <summary>
    /// Обрабатывает пойманную монету и обновляет цель.
    /// </summary>
    /// <param name="parCoinType">Тип монеты, которую поймал игрок.</param>
    /// <param name="parScore">Текущий счет игрока.</param>
    public void ProcessCoin(ObjectType parCoinType, int parScore)
    {
      switch (parCoinType)
      {
        case ObjectType.GoldCell:
          if (CountGoldCoins > 0) CountGoldCoins--;
          break;
        case ObjectType.SilverCell:
          if (CountSilverCoins > 0) CountSilverCoins--;
          break;
        case ObjectType.BronzeCell:
          if (CountBronzeCoins > 0) CountBronzeCoins--;
          break;
      }

      // Проверяем, достиг ли игрок цели
      if (CountGoldCoins <= 0 && CountSilverCoins <= 0 && CountBronzeCoins <= 0 && parScore >= ScoreTarget)
      {
        GoalCompleted?.Invoke();
      }
    }
  }
}
