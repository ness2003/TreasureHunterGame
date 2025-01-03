using Model.TableOfRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.TableOfRecords
{
  /// <summary>
  /// Представление таблицы рекордов
  /// </summary>
  public abstract class ViewTableOfRecords : ViewBase
  {
    /// <summary>
    /// Рекорды
    /// </summary>
    protected ModelTableOfRecords _modelTableOfRecords;
   
    /// <summary>
    /// Конструктор
    /// </summary>
    public ViewTableOfRecords(ModelTableOfRecords parModelTableOfRecords)
    {
      _modelTableOfRecords = parModelTableOfRecords;
    }
  }
}
