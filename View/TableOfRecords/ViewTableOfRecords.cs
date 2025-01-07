using Model.TableOfRecords;

namespace View.TableOfRecords
{
  /// <summary>
  /// Абстрактное представление таблицы рекордов.  
  /// Отвечает за отображение данных о лучших результатах игроков.
  /// </summary>
  public abstract class ViewTableOfRecords : ViewBase
  {
    /// <summary>
    /// Модель таблицы рекордов, содержащая данные о результатах игроков.
    /// </summary>
    protected ModelTableOfRecords _modelTableOfRecords;

    /// <summary>
    /// Конструктор класса
    /// Инициализирует модель таблицы рекордов.
    /// </summary>
    /// <param name="parModelTableOfRecords">Экземпляр модели таблицы рекордов.</param>
    public ViewTableOfRecords(ModelTableOfRecords parModelTableOfRecords)
    {
      _modelTableOfRecords = parModelTableOfRecords;
    }
  }
}
