using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Menu
{
  /// <summary>
  /// Класс, представляющий меню, состоящее из элементов, которые могут быть выбраны или активированы пользователем.
  /// </summary>
  public class Menu
  {
    /// <summary>
    /// Родительское меню, к которому принадлежит данное меню.
    /// </summary>
    private Menu _parent;

    /// <summary>
    /// Словарь элементов меню, где ключ — уникальный идентификатор элемента, а значение — сам элемент.
    /// </summary>
    private Dictionary<int, MenuItem> _items = [];

    /// <summary>
    /// Упорядоченные элементы меню для последовательного выбора.
    /// </summary>
    private Dictionary<int, MenuItem> _orderedItems = [];

    /// <summary>
    /// Последний выбранный элемент меню.
    /// </summary>
    private MenuItem? _lastSelectedItem = null;

    /// <summary>
    /// Индекс текущего выбранного элемента в упорядоченном списке.
    /// </summary>
    private int _selectedItemIndex = -1;

    /// <summary>
    /// Название меню.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Возвращает массив элементов меню, отсортированных по их ключам.
    /// </summary>
    public MenuItem[] Items
    {
      get => _items.OrderBy(item => item.Key).Select(item => item.Value).ToArray();
    }

    /// <summary>
    /// Индексатор для доступа к элементу меню по его идентификатору.
    /// </summary>
    /// <param name="parId">Идентификатор элемента меню.</param>
    /// <returns>Элемент меню с указанным идентификатором.</returns>
    public MenuItem this[int parId]
    {
      get => _items[parId];
    }

    /// <summary>
    /// Конструктор для создания меню.
    /// </summary>
    /// <param name="parName">Имя меню.</param>
    /// <param name="parParent">Родительское меню (если есть).</param>
    public Menu(string parName, Menu parParent = null)
    {
      Name = parName;
      _parent = parParent;
    }

    /// <summary>
    /// Добавляет элемент в меню.
    /// </summary>
    /// <param name="parMenuItem">Элемент меню для добавления.</param>
    public void AddMenuItem(MenuItem parMenuItem)
    {
      _items.Add(parMenuItem.Id, parMenuItem);
      _orderedItems.Add(_orderedItems.Count, parMenuItem);
    }

    /// <summary>
    /// Устанавливает статус элемента меню.
    /// </summary>
    /// <param name="parId">Идентификатор элемента.</param>
    /// <param name="parStatus">Новый статус элемента.</param>
    public void SetMenuItemStatus(int parId, MenuItem.Statuses parStatus)
    {
      MenuItem menuItem = _items[parId];
      if (parStatus == MenuItem.Statuses.Focused || parStatus == MenuItem.Statuses.Selected)
      {
        if (_lastSelectedItem != null)
          _lastSelectedItem.Status = MenuItem.Statuses.None;
        _lastSelectedItem = menuItem;
      }
      menuItem.Status = parStatus;
    }

    /// <summary>
    /// Устанавливает текущий выбранный элемент как активный.
    /// </summary>
    public void EnterSelectedItem()
    {
      if (_selectedItemIndex == -1)
        return;

      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItem.Statuses.Selected);
    }

    /// <summary>
    /// Выбирает следующий элемент в упорядоченном списке.
    /// </summary>
    public void SelectNextItem()
    {
      if (_selectedItemIndex == -1 || _selectedItemIndex == _orderedItems.Count - 1)
        _selectedItemIndex = 0;
      else
        _selectedItemIndex++;

      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItem.Statuses.Focused);
    }

    /// <summary>
    /// Удаляет последний элемент из меню.
    /// </summary>
    public void RemoveLastItem()
    {
      if (_orderedItems.Count == 0)
        return;

      // Получаем последний элемент из упорядоченного словаря
      int lastIndex = _orderedItems.Keys.Max();
      MenuItem lastItem = _orderedItems[lastIndex];

      // Удаляем элемент из обоих словарей
      _items.Remove(lastItem.Id);
      _orderedItems.Remove(lastIndex);

      // Сбрасываем индекс выбранного элемента, если он был последним
      if (_selectedItemIndex == lastIndex)
      {
        _selectedItemIndex = 0;
        _lastSelectedItem = null;
      }
    }


    /// <summary>
    /// Выбирает предыдущий элемент в упорядоченном списке.
    /// </summary>
    public void SelectPrevItem()
    {
      if (_selectedItemIndex == -1 || _selectedItemIndex == 0)
        _selectedItemIndex = _orderedItems.Count - 1;
      else
        _selectedItemIndex--;

      SetMenuItemStatus(_orderedItems[_selectedItemIndex].Id, MenuItem.Statuses.Focused);
    }

    /// <summary>
    /// Отписывает все элементы меню от событий.
    /// </summary>
    public void UnsubscribeAll()
    {
      foreach (MenuItem elMenuItem in Items)
      {
        elMenuItem.Unsubscribe();
      }
    }
  }
}
