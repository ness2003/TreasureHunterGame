namespace Model.Game.GameObjects
{
  /// <summary>
  /// Класс, представляющий игрока в игре.
  /// </summary>
  public class Player
  {
    /// <summary>
    /// Ширина игрового поля
    /// </summary>
    private int _fieldWidth;

    /// <summary>
    /// Высота игрового поля
    /// </summary>
    private int _fieldHeight;

    /// <summary>
    /// Позиция игрока по оси X.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Позиция игрока по оси Y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Ширина игрока.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Высота игрока.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Скорость игрока.
    /// </summary>
    public int Speed { get; set; }

    // Конструктор
    /// <summary>
    /// Конструктор класса Player.
    /// </summary>
    /// <param name="parX">Позиция игрока по оси X.</param>
    /// <param name="parY">Позиция игрока по оси Y.</param>
    /// <param name="parWidth">Ширина игрока.</param>
    /// <param name="parHeight">Высота игрока.</param>
    /// <param name="parSpeed">Скорость игрока.</param>
    /// <param name="parFieldWidth">Ширина игрового поля.</param>
    /// <param name="parFieldHeight">Высота игрового поля.</param>
    public Player(int parX, int parY, int parWidth, int parHeight, int parSpeed, int parFieldWidth, int parFieldHeight)
    {
      X = parX;
      Y = parY;
      Width = parWidth;
      Height = parHeight;
      Speed = parSpeed;

      _fieldWidth = parFieldWidth;
      _fieldHeight = parFieldHeight;
    }

    /// <summary>
    /// Двигает игрока вправо.
    /// </summary>
    public void MoveRight()
    {
      if (X + Width + Speed <= _fieldWidth)
      {
        X += Speed;
      }
    }

    /// <summary>
    /// Сбрасывает позицию игрока к начальным координатам.
    /// </summary>
    public void ResetPosition()
    {
      X = _fieldWidth / 2 - Width / 2; 
      Y = _fieldHeight - Height;       
    }


    /// <summary>
    /// Двигает игрока влево.
    /// </summary>
    public void MoveLeft()
    {
      // Проверяем, не выходит ли игрок за левую границу игрового поля
      if (X - Speed >= 0)
      {
        X -= Speed;
      }
    }
  }
}
