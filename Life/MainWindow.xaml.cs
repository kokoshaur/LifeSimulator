using System;
using System.Windows;
using System.Windows.Controls;

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
            game = new Game(this);
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, lifeSpeed);
        }
        private void StatrLife_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            timer.Start();
            Pause.IsEnabled = true;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            game.OneDrawing(frameSkip);
            Label.Content = (Convert.ToInt32(Label.Content) + frameSkip);
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
