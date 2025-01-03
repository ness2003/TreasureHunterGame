using System;
using Model.TableOfRecords;

namespace ViewConsole.TableOfRecords
{
  /// <summary>
  /// Консольное представление таблицы рекордов
  /// </summary>
  public class ViewTableOfRecordsConsole : View.TableOfRecords.ViewTableOfRecords
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ViewTableOfRecordsConsole(ModelTableOfRecords parModelTableOfRecords) : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Нарисовать таблицу рекордов в консоли
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      Console.WriteLine("=========================");
      Console.WriteLine("       ТАБЛИЦА РЕКОРДОВ       ");
      Console.WriteLine("=========================");

      foreach (var record in _modelTableOfRecords.RecordsValues)
      {
        Console.WriteLine($"{record.Name} - score: {record.Score} level: {record.Level}");
      }
    }
  }
}
