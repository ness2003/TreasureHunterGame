
namespace ViewConsole.Menu
{
  /// <summary>
  /// Представление элимента меню
  /// </summary>
  public class ViewMenuItemConsole : View.Menu.ViewMenuItemBase
  {

    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase"></param>
    public ViewMenuItemConsole(Model.Menu.MenuItem parMenuItemBase) : base(parMenuItemBase)
    {
      if (parMenuItemBase != null)
      {
        Height = 2;
        Width = parMenuItemBase.Name.Length;
      }
      parMenuItemBase.NeedRedraw += Draw;
    }

    /// <summary>
    /// Нарисовать представление элмента меню
    /// </summary>
    public override void Draw()
    {
      Console.CursorLeft = X;
      Console.CursorTop = Y;
      ConsoleColor savColor = Console.ForegroundColor;
      switch (this.MenuItem.Status)
      {
        case Model.Menu.MenuItem.Statuses.Disabled:
          Console.ForegroundColor = ConsoleColor.Gray;
          break;
        case Model.Menu.MenuItem.Statuses.Focused:
          Console.ForegroundColor = ConsoleColor.DarkYellow;
          break;
        case Model.Menu.MenuItem.Statuses.Selected:
          Console.ForegroundColor = ConsoleColor.Green;
          break;
        case Model.Menu.MenuItem.Statuses.None:
          Console.ForegroundColor = ConsoleColor.Black;
          break;
      }
      Console.Write(MenuItem.Name);
    }
  }
}
