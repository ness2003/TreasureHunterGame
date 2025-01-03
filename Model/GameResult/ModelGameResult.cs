using System;

namespace Model.GameResult
{
  /// <summary>
  /// Модель результата игры, содержащая данные об уровне, набранных очках и имени игрока.
  /// </summary>
  public class ModelGameResult
  {
    /// <summary>
    /// Уровень, достигнутый игроком.
    /// </summary>
    private int _level;

    /// <summary>
    /// Набранное количество очков.
    /// </summary>
    private int _score;

    /// <summary>
    /// Имя игрока для записи рекорда.
    /// </summary>
    private string? _name;

    /// <summary>
    /// Свойство для доступа к количеству набранных очков.
    /// </summary>
    public int Score
    {
      get => _score;
      set => _score = value;
    }

    /// <summary>
    /// Свойство для доступа к уровню игрока.
    /// </summary>
    public int Level
    {
      get => _level;
      set => _level = value;
    }

    /// <summary>
    /// Свойство для доступа к имени игрока.
    /// </summary>
    public string? Name
    {
      get => _name;
      set => _name = value;
    }

    /// <summary>
    /// Конструктор для инициализации данных результата игры.
    /// </summary>
    /// <param name="parScore">Количество очков, набранное игроком.</param>
    /// <param name="parLevel">Уровень, достигнутый игроком.</param>
    public ModelGameResult(int parScore, int parLevel)
    {
      _score = parScore;
      _level = parLevel;
    }
  }
}
