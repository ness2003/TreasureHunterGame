using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Model.Rules;

namespace View.Rules
{
  /// <summary>
  /// Представление правил
  /// </summary>
  public abstract class ViewRules : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Координата по вертикали
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Ширина представления
    /// </summary>
    public int Width { get; protected set; }
    /// <summary>
    /// Высота представления
    /// </summary>
    public int Height { get; protected set; }
    /// <summary>
    /// Элемент меню
    /// </summary>
    /// Элемент меню
    /// </summary>
    protected ModelRules _modelRules = new ModelRules();
    public ViewRules(ModelRules parModelRules)
    {
      _modelRules = parModelRules;
      Init();
      Draw();
    }

    public abstract void Init();


  }
}
