namespace Model.TableOfRecords
{
  /// <summary>
  /// Класс, представляющий запись в таблице рекордов.
  /// </summary>
  public class Record
  {
    /// <summary>
    /// Имя игрока по умолчанию.
    /// </summary>
    public const string DEFAULT_NAME = "Unknown Player";
    /// <summary>
    /// Имя игрока.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Набранные очки.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Уровень, на котором был достигнут рекорд.
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Конструктор для создания новой записи рекорда.
    /// </summary>
    /// <param name="name">Имя игрока.</param>
    /// <param name="score">Количество очков.</param>
    /// <param name="level">Достигнутый уровень.</param>
    public Record(string name, int score, int level)
    {
      if (string.IsNullOrWhiteSpace(name))
        name = DEFAULT_NAME;
      if (score < 0)
        score = 0;

      if (level < 1)
        level = 0;

      Name = name;
      Score = score;
      Level = level;
    }
  }
}
