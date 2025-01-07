using System.Windows.Controls;
using System.Windows;
using Model.TableOfRecords;
using View.TableOfRecords;

namespace ViewWPF.TableOfRecords
{
  /// <summary>
  /// WPF отображение таблицы рекордов
  /// </summary>
  public class ViewTableOfRecordsWPF : ViewTableOfRecords
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModelTableOfRecords">Экземпляр модели таблицы рекордов</param>
    public ViewTableOfRecordsWPF(ModelTableOfRecords parModelTableOfRecords) : base(parModelTableOfRecords)
    {
      _modelTableOfRecords = parModelTableOfRecords;
      Draw();
    }

    /// <summary>
    /// Отрисовка представления таблицы рекордов
    /// </summary>
    public override void Draw()
    {
      StackPanel panel = MainScreen.GetInstance().StackPanel;

      panel.Children.Add(new TextBlock
      {
        FontSize = 25,
        Text = "Рекорды",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center
      });

      foreach (var record in _modelTableOfRecords.RecordsValues)
      {
        panel.Children.Add(new TextBlock
        {
          FontSize = 16,
          Text = record.Name + " - " + "score: " + record.Score + " level: " + record.Level,
          HorizontalAlignment = HorizontalAlignment.Center
        });
      }
    }
  }
}
