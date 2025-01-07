using System.IO;
using System.Text.Json;

namespace Model.TableOfRecords
{
  /// <summary>
  /// Класс для управления таблицей рекордов.
  /// </summary>
  public class ModelTableOfRecords
  {
    /// <summary>
    /// Максимальное количество хранимых рекордов.
    /// </summary>
    public const int SIZE = 10;

    /// <summary>
    /// Путь к файлу, где сохраняются рекорды.
    /// </summary>
    private const string PATH = "ScoreList.json";

    /// <summary>
    /// Экземпляр класса (Singleton).
    /// </summary>
    private static ModelTableOfRecords? _instance;

    /// <summary>
    /// Коллекция рекордов.
    /// </summary>
    private SortedSet<Record>? _recordsValues;

    /// <summary>
    /// Коллекция рекордов (только для чтения).
    /// </summary>
    public SortedSet<Record>? RecordsValues { get => _recordsValues; private set => _recordsValues = value; }


    /// <summary>
    /// Компаратор для сортировки рекордов по очкам и имени.
    /// </summary>
    private static readonly Comparer<Record> COMPARER = Comparer<Record>.Create((r1, r2) =>
    {
      int scoreComparison = r2.Score.CompareTo(r1.Score);
      return scoreComparison != 0 ? scoreComparison : r1.Name.CompareTo(r2.Name);
    });

    /// <summary>
    /// Приватный конструктор
    /// </summary>
    private ModelTableOfRecords()
    {
      RecordsValues = new SortedSet<Record>(COMPARER);
    }

    /// <summary>
    /// Единственный экземпляр класса (Singleton).
    /// </summary>
    public static ModelTableOfRecords Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new ModelTableOfRecords();
          _instance.Load();
        }
        return _instance;
      }
    }

    /// <summary>
    /// Удаляет лишние записи, чтобы соответствовать лимиту.
    /// </summary>
    private void Update()
    {
      while (RecordsValues?.Count > SIZE)
      {
        RecordsValues.Remove(RecordsValues.Last());
      }
    }

    /// <summary>
    /// Добавляет новый рекорд в таблицу.
    /// </summary>
    /// <param name="parRecord">Новый рекорд.</param>
    public void Add(Record parRecord)
    {
      if (parRecord == null)
        throw new ArgumentNullException(nameof(parRecord));

      RecordsValues?.Add(parRecord);
      Update();
      Save();
    }

    /// <summary>
    /// Проверяет, является ли результат новым рекордом.
    /// </summary>
    /// <param name="parScore">Количество очков.</param>
    /// <returns>True, если это новый рекорд.</returns>
    public bool IsNewRecord(int parScore)
    {
      return RecordsValues?.Count < SIZE || parScore > RecordsValues?.Last().Score;
    }

    /// <summary>
    /// Сохраняет рекорды в файл.
    /// </summary>
    public void Save()
    {
      try
      {
        string jsonString = JsonSerializer.Serialize(RecordsValues, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(PATH, jsonString);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка сохранения рекордов: {ex.Message}");
      }
    }

    /// <summary>
    /// Загружает рекорды из файла.
    /// </summary>
    public void Load()
    {
      if (File.Exists(PATH))
      {
        try
        {
          string jsonString = File.ReadAllText(PATH);
          var loadData = JsonSerializer.Deserialize<List<Record>>(jsonString);
          if (loadData != null)
          {
            RecordsValues = new SortedSet<Record>(loadData, COMPARER);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Ошибка загрузки рекордов: {ex.Message}");
          RecordsValues = new SortedSet<Record>(COMPARER);
        }
      }
      else
      {
        RecordsValues = new SortedSet<Record>(COMPARER);
      }
    }
  }
}
