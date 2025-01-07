using Model.Game;
using Model.Game.GameObjects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using View.Game;

namespace ViewWPF.Game
{
  /// <summary>
  /// WPF-отображение игры
  /// </summary>
  public class ViewGameWPF : ViewGame
  {
    /// <summary>
    /// Окно игры, в котором отображается все содержимое
    /// </summary>
    private readonly Window _gameWindow;

    /// <summary>
    /// Основной контейнер, в котором размещены все элементы игры
    /// </summary>
    private readonly Grid _mainGrid;

    /// <summary>
    /// Холст для отображения игрового поля и объектов
    /// </summary>
    private readonly Canvas _gameCanvas;

    /// <summary>
    /// Холст для отображения панели с информацией (время, счет, уровень)
    /// </summary>
    private readonly Canvas _infoCanvas;

    /// <summary>
    /// Холст для отображения целей уровня (монеты и требуемый счет)
    /// </summary>
    private readonly Canvas _goalCanvas;

    /// <summary>
    /// Объект для фона игры, который используется для настройки фона на холсте
    /// </summary>
    private readonly ImageBrush _backgroundBrush;

    /// <summary>
    /// Текстовый блок для отображения оставшегося времени игры
    /// </summary>
    private TextBlock _timeTextBlock;

    /// <summary>
    /// Текстовый блок для отображения текущего счета игрока
    /// </summary>
    private TextBlock _scoreTextBlock;

    /// <summary>
    /// Текстовый блок для отображения текущего уровня игры
    /// </summary>
    private TextBlock _levelTextBlock;

    /// <summary>
    /// Текстовый блок для отображения количества золотых монет, необходимых для выполнения цели
    /// </summary>
    private TextBlock _goldCoinTextBlock;

    /// <summary>
    /// Текстовый блок для отображения количества серебряных монет, необходимых для выполнения цели
    /// </summary>
    private TextBlock _silverCoinTextBlock;

    /// <summary>
    /// Текстовый блок для отображения количества бронзовых монет, необходимых для выполнения цели
    /// </summary>
    private TextBlock _bronzeCoinTextBlock;


    /// <summary>
    /// Конструктор для инициализации представления игры.
    /// </summary>
    /// <param name="parModelGame">Модель игры, которая передается в представление.</param>
    public ViewGameWPF(ModelGame parModelGame) : base(parModelGame)
    {
      _modelGame = parModelGame;
      _gameWindow = MainScreen.GetInstance().Window;

      // Основной контейнер
      _mainGrid = new Grid();
      _gameWindow.Content = _mainGrid;

      // Создаем строки для Grid
      _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Для информационной панели    
      _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Для целей уровня
      _mainGrid.RowDefinitions.Add(new RowDefinition()); // Для игрового поля

      // Информационная панель (Canvas)
      _infoCanvas = new Canvas { Height = 50, Background = Brushes.Black };
      Grid.SetRow(_infoCanvas, 0);
      _mainGrid.Children.Add(_infoCanvas);

      // Игровое поле (Canvas)
      _gameCanvas = new Canvas { Width = 1000, Height = 500 };
      Grid.SetRow(_gameCanvas, 2);
      _mainGrid.Children.Add(_gameCanvas);

      // Canvas для целей уровня
      _goalCanvas = new Canvas { Width = 1000, Height = 150, Background = Brushes.BurlyWood };
      Grid.SetRow(_goalCanvas, 1);
      _mainGrid.Children.Add(_goalCanvas);

      // Устанавливаем фон
      _backgroundBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/ViewWPF;component/Assets/background.png")))
      {
        Stretch = Stretch.Fill
      };

      _gameCanvas.Background = _backgroundBrush;

      Application.Current.Dispatcher.Invoke(InitializeScreen);
      _modelGame.NeedRedraw += OnCanRender;
    }

    /// <summary>
    /// Создает панель целей уровня, которая отображает информацию о монетах и требуемом счете.
    /// </summary>
    private void CreateGoalPanel()
    {
      var goal = _modelGame.CurrentGoal;

      // Заголовок "Цели уровня"
      var titleTextBlock = CreateTextBlock("Цели уровня", Brushes.White);
      titleTextBlock.FontSize = 20;
      titleTextBlock.Margin = new Thickness(0, 10, 0, 10);
      _goalCanvas.Children.Add(titleTextBlock);

      // Центрируем заголовок
      titleTextBlock.SizeChanged += (parSender, parE) =>
      {
        Canvas.SetLeft(titleTextBlock, (_goalCanvas.Width - titleTextBlock.ActualWidth) / 2);
        Canvas.SetTop(titleTextBlock, 10);
      };

      // Создаём монеты
      CreateGoalCoin(ObjectType.GoldCell, goal.CountGoldCoins, _goalCanvas.Width / 2 - 150, 50);
      CreateGoalCoin(ObjectType.SilverCell, goal.CountSilverCoins, _goalCanvas.Width / 2, 50);
      CreateGoalCoin(ObjectType.BronzeCell, goal.CountBronzeCoins, _goalCanvas.Width / 2 + 150, 50);

      // Требуемый счёт
      var scoreTextBlock = new TextBlock
      {
        Text = $"Требуемый счет: {goal.ScoreTarget}",
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = Brushes.Black,
        Margin = new Thickness(10)
      };
      _goalCanvas.Children.Add(scoreTextBlock);

      // Центрируем текст с требуемым счётом
      scoreTextBlock.SizeChanged += (parSender, parE) =>
      {
        Canvas.SetLeft(scoreTextBlock, (_goalCanvas.Width - scoreTextBlock.ActualWidth) / 2);
        Canvas.SetTop(scoreTextBlock, 100);
      };
    }

    /// <summary>
    /// Обновляет панель целей уровня, чтобы отобразить актуальную информацию о монетах и требуемом счете.
    /// </summary>
    private void UpdateGoalPanel()
    {
      var goal = _modelGame.CurrentGoal;

      // Обновляем текст с количеством монет
      UpdateCoinText(ObjectType.GoldCell, goal.CountGoldCoins);
      UpdateCoinText(ObjectType.SilverCell, goal.CountSilverCoins);
      UpdateCoinText(ObjectType.BronzeCell, goal.CountBronzeCoins);

      // Обновляем требуемый счёт
      var scoreTextBlock = _goalCanvas.Children.OfType<TextBlock>().FirstOrDefault(parT => parT.Text.StartsWith("Требуемый"));
      if (scoreTextBlock != null)
      {
        scoreTextBlock.Text = $"Требуемый счет: {goal.ScoreTarget}";
      }
    }

    /// <summary>
    /// Обновляет текст с количеством монет для конкретного типа монеты.
    /// </summary>
    /// <param name="parCoinType">Тип монеты (золотая, серебряная, бронзовая)</param>
    /// <param name="parCoinCount">Количество монет данного типа</param>
    private void UpdateCoinText(ObjectType parCoinType, int parCoinCount)
    {
      // Обновляем соответствующий TextBlock
      if (parCoinType == ObjectType.GoldCell && _goldCoinTextBlock != null)
        _goldCoinTextBlock.Text = parCoinCount.ToString();
      else if (parCoinType == ObjectType.SilverCell && _silverCoinTextBlock != null)
        _silverCoinTextBlock.Text = parCoinCount.ToString();
      else if (parCoinType == ObjectType.BronzeCell && _bronzeCoinTextBlock != null)
        _bronzeCoinTextBlock.Text = parCoinCount.ToString();
    }

    /// <summary>
    /// Создает отображение монеты на панели целей уровня.
    /// </summary>
    /// <param name="parCoinType">Тип монеты</param>
    /// <param name="parCoinCount">Количество монет</param>
    /// <param name="parXPosition">Позиция X для размещения монеты</param>
    /// <param name="parYPosition">Позиция Y для размещения монеты</param>
    private void CreateGoalCoin(ObjectType parCoinType, int parCoinCount, double parXPosition, double parYPosition)
    {
      var coinImage = new ImageBrush(new BitmapImage(new Uri(GetImagePath(parCoinType))));

      var coinEllipse = new Ellipse
      {
        Width = 40,
        Height = 40,
        Fill = coinImage
      };
      Canvas.SetLeft(coinEllipse, parXPosition);
      Canvas.SetTop(coinEllipse, parYPosition);
      _goalCanvas.Children.Add(coinEllipse);

      // Текст с количеством монет
      var countTextBlock = new TextBlock
      {
        Text = parCoinCount.ToString(),
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = Brushes.Black,
        Margin = new Thickness(5)
      };
      Canvas.SetLeft(countTextBlock, parXPosition + 10);
      Canvas.SetTop(countTextBlock, parYPosition + 40);
      _goalCanvas.Children.Add(countTextBlock);

      // Сохраняем ссылку на TextBlock в зависимости от типа монеты
      if (parCoinType == ObjectType.GoldCell)
        _goldCoinTextBlock = countTextBlock;
      else if (parCoinType == ObjectType.SilverCell)
        _silverCoinTextBlock = countTextBlock;
      else if (parCoinType == ObjectType.BronzeCell)
        _bronzeCoinTextBlock = countTextBlock;
    }

    /// <summary>
    /// Обработчик события, когда необходимо перерисовать экран.
    /// </summary>
    private void OnCanRender()
    {
      Application.Current.Dispatcher.Invoke(Render);
    }

    /// <summary>
    /// Инициализация экрана игры: создание панелей и объектов.
    /// </summary>
    public void InitializeScreen()
    {
      CreateInfoPanel();
      CreateGoalPanel();
    }

    /// <summary>
    /// Создает информационную панель, отображающую время, счет и уровень.
    /// </summary>
    private void CreateInfoPanel()
    {
      _timeTextBlock = CreateTextBlock("⏱️ Время: 0 сек", Brushes.LightGreen);
      _scoreTextBlock = CreateTextBlock("🏆 Счет: 0", Brushes.Gold);
      _levelTextBlock = CreateTextBlock("📊 Уровень: 1", Brushes.Cyan);

      var panel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)),
        Margin = new Thickness(5)
      };

      panel.Children.Add(_timeTextBlock);
      panel.Children.Add(_scoreTextBlock);
      panel.Children.Add(_levelTextBlock);

      _infoCanvas.Children.Add(panel);
    }

    /// <summary>
    /// Создает текстовый блок с заданным текстом и цветом.
    /// </summary>
    /// <param name="parText">Текст, который будет отображаться</param>
    /// <param name="parColor">Цвет текста</param>
    private TextBlock CreateTextBlock(string parText, Brush parColor)
    {
      return new TextBlock
      {
        Text = parText,
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = parColor,
        Margin = new Thickness(5)
      };
    }

    /// <summary>
    /// Обновляет информацию о времени, счете и уровне игры.
    /// </summary>
    private void UpdateGameInfo()
    {
      if (_timeTextBlock != null && _scoreTextBlock != null && _levelTextBlock != null)
      {
        var gameState = _modelGame;
        _timeTextBlock.Text = $"⏱️ Время: {gameState.RemainingTime:F1} сек";
        _scoreTextBlock.Text = $"🏆 Счет: {gameState.Score}";
        _levelTextBlock.Text = $"📊 Уровень: {gameState.Level}";
      }
    }

    /// <summary>
    /// Рисует все элементы на экране: игровое поле, игрока, объекты игры.
    /// </summary>
    public override void Draw()
    {
      _gameCanvas.Children.Clear();
      DrawGameField();
      DrawPlayer();
      DrawGameObjects(_modelGame.Coins);
      DrawGameObjects(_modelGame.Bonuses);
      DrawGameObjects(_modelGame.BadObjects);

      UpdateGameInfo();
      UpdateGoalPanel();  // Обновляем цели уровня
    }

    /// <summary>
    /// Рисует игровое поле.
    /// </summary>
    private void DrawGameField()
    {
      Rectangle gameField = new Rectangle
      {
        Width = _modelGame.Width,
        Height = _modelGame.Height,
        Stroke = Brushes.Black,
        StrokeThickness = 5
      };
      Canvas.SetLeft(gameField, 0);
      Canvas.SetTop(gameField, 0);
      _gameCanvas.Children.Add(gameField);
    }

    /// <summary>
    /// Рисует игрока на игровом поле.
    /// </summary>
    private void DrawPlayer()
    {
      var player = _modelGame.Player;
      Rectangle playerRect = new Rectangle
      {
        Width = player.Width,
        Height = player.Height,
        Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/ViewWPF;component/Assets/player.png")))
      };
      Canvas.SetLeft(playerRect, player.X);
      Canvas.SetTop(playerRect, player.Y);
      _gameCanvas.Children.Add(playerRect);
    }

    /// <summary>
    /// Рисует объекты игры (монеты, бонусы, плохие объекты).
    /// </summary>
    /// <param name="parGameObjects">Объекты игры для отображения (монеты, бонусы, плохие объекты)</param>
    private void DrawGameObjects(IEnumerable<GameObject> parGameObjects)
    {
      foreach (var obj in parGameObjects)
      {
        ImageBrush objectBrush = new ImageBrush(new BitmapImage(new Uri(GetImagePath(obj.ObjectType))));

        Ellipse objectEllipse = new Ellipse
        {
          Width = obj.Radius * 2,
          Height = obj.Radius * 2,
          Fill = objectBrush
        };

        Canvas.SetLeft(objectEllipse, obj.X);
        Canvas.SetTop(objectEllipse, obj.Y);
        _gameCanvas.Children.Add(objectEllipse);
      }
    }

    /// <summary>
    /// Получает путь к изображению для заданного типа объекта.
    /// </summary>
    /// <param name="parObjectType">Тип объекта (монета, бонус, плохой объект)</param>
    private string GetImagePath(ObjectType parObjectType)
    {
      return parObjectType switch
      {
        ObjectType.GoldCell => "pack://application:,,,/ViewWPF;component/Assets/gold_coin.png",
        ObjectType.SilverCell => "pack://application:,,,/ViewWPF;component/Assets/silver_coin.png",
        ObjectType.BronzeCell => "pack://application:,,,/ViewWPF;component/Assets/bronze_coin.png",
        ObjectType.Magnet => "pack://application:,,,/ViewWPF;component/Assets/magnet.png",
        ObjectType.Timer => "pack://application:,,,/ViewWPF;component/Assets/timer.png",
        ObjectType.Mul => "pack://application:,,,/ViewWPF;component/Assets/mul.png",
        ObjectType.Thief => "pack://application:,,,/ViewWPF;component/Assets/thief.png",
        ObjectType.Meteorite => "pack://application:,,,/ViewWPF;component/Assets/meteorite.png",
        _ => "pack://application:,,,/ViewWPF;component/Assets/default.png"
      };
    }

    /// <summary>
    /// Обновляет отображение на экране.
    /// </summary>
    private void Render()
    {
      Draw();
    }

    /// <summary>
    /// Обрабатывает начало игры и рисует начальное состояние.
    /// </summary>
    public override void OnStartGame()
    {
      Draw();
    }
  }
}
