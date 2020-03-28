using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Life
{
    public partial class MainWindow : Window
    {
        
        static int frameSkip = 1;    //Пропуск фреймов (при больших значениях повышается производительность)
        static int lifeSpeed = 25;  //Период обновления кадров (чем меньше, тем плавнее картинка)

        Game game;

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Refresh_Game();
            InitControl();
            DataContext = Game.Controler;
        }
        private void InitControl()
        {
            //Game.Controler.frame++;
        }
        private void Refresh_Game()
        {
            game = new Game(this);
            Height = MinHeight = Game.fieldHeight + 6 + 33;
            Width = MinWidth = Game.fieldWidth + 200 + 6 + 33;

            CanvasMap.Height = Game.fieldHeight;
            CanvasMap.Width = Game.fieldWidth;

            VerticalWall.Y2 = Game.fieldHeight;

            HorizontlWall.X2 = HorizontlWall.X1 = Game.fieldHeight + 1;

            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, lifeSpeed);
        }
        private void StartLife_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            timer.Start();
            Pause.IsEnabled = true;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            game.OneDrawing(frameSkip);
        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Pause.IsEnabled = false;
            Continue.IsEnabled = true;
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            Pause.IsEnabled = true;
            Continue.IsEnabled = false;
        }
    }
}
