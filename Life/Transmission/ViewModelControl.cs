using Life.Transmission;
using System.Windows;

namespace Life
{
    class ViewModelControl : DependencyObject
    {
        public int frame
        {
            get { return (int)GetValue(frameProperty); }
            set { SetValue(frameProperty, value); }
        }
        public static readonly DependencyProperty frameProperty =
            DependencyProperty.Register("frame", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int allTimePeacCounter
        {
            get { return (int)GetValue(allTimePeacCounterProperty); }
            set { SetValue(allTimePeacCounterProperty, value); }
        }
        public static readonly DependencyProperty allTimePeacCounterProperty =
            DependencyProperty.Register("allTimePeacCounter", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int allTimeEvilCounter
        {
            get { return (int)GetValue(allTimeEvilCounterProperty); }
            set { SetValue(allTimeEvilCounterProperty, value); }
        }
        public static readonly DependencyProperty allTimeEvilCounterProperty =
            DependencyProperty.Register("allTimeEvilCounter", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowFoodCounter
        {
            get { return (int)GetValue(nowFoodCounterProperty); }
            set
            {
                SetValue(nowFoodCounterProperty, value);
                SetValue(nowFoodCounterForGraphProperty, value * 4);
            }
        }
        public static readonly DependencyProperty nowFoodCounterProperty =
            DependencyProperty.Register("nowFoodCounter", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowFoodCounterForGraph
        {
            get { return (int)GetValue(nowFoodCounterForGraphProperty); }
        }
        public static readonly DependencyProperty nowFoodCounterForGraphProperty =
            DependencyProperty.Register("nowFoodCounterForGraph", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowPeacCounter
        {
            get { return (int)GetValue(nowPeacCounterProperty); }
            set { SetValue(nowPeacCounterProperty, value);
                SetValue(nowPeacCounterForGraphProperty, value * 4);}
        }
        public static readonly DependencyProperty nowPeacCounterProperty =
            DependencyProperty.Register("nowPeacCounter", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowPeacCounterForGraph
        {
            get { return (int)GetValue(nowPeacCounterForGraphProperty); }
        }
        public static readonly DependencyProperty nowPeacCounterForGraphProperty =
            DependencyProperty.Register("nowPeacCounterForGraph", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowEvilCounter
        {
            get { return (int)GetValue(nowEvilCounterProperty); }
            set { SetValue(nowEvilCounterProperty, value);
                SetValue(nowEvilCounterForGraphProperty, value * 4);}
        }
        public static readonly DependencyProperty nowEvilCounterProperty =
            DependencyProperty.Register("nowEvilCounter", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int nowEvilCounterForGraph
        {
            get { return (int)GetValue(nowEvilCounterForGraphProperty); }
        }
        public static readonly DependencyProperty nowEvilCounterForGraphProperty =
            DependencyProperty.Register("nowEvilCounterForGraph", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public int gameSpeed
        {
            get { if ((int)GetValue(gameSpeedProperty) != 0)
                    return (100 / (int)GetValue(gameSpeedProperty));
                return 25;
            }
            set {SetValue(gameSpeedProperty, value); }
        }
        public static readonly DependencyProperty gameSpeedProperty =
            DependencyProperty.Register("gameSpeed", typeof(int), typeof(ViewModelControl), new PropertyMetadata(4));
        public double maxSpeed
        {
            get { return (double)GetValue(maxSpeedProperty); }
            set
            {
                if (value > maxSpeed)
                    SetValue(maxSpeedProperty, value);
            }
        }
        public static readonly DependencyProperty maxSpeedProperty =
            DependencyProperty.Register("maxSpeed", typeof(double), typeof(ViewModelControl), new PropertyMetadata(Settings.bacteriaDefaultSpeed));
        public double maxRotationSpeed
        {
            get { return (double)GetValue(maxRotationSpeedProperty); }
            set
            {
                if (value > maxRotationSpeed)
                    SetValue(maxRotationSpeedProperty, value);
            }
        }
        public static readonly DependencyProperty maxRotationSpeedProperty =
            DependencyProperty.Register("maxRotationSpeed", typeof(double), typeof(ViewModelControl), new PropertyMetadata(Settings.bacteriaDefaultRotationSpeed));
        public int maxVision
        {
            get { return (int)GetValue(maxVisionProperty); }
            set
            {
                if (value > maxVision)
                    SetValue(maxVisionProperty, value);
            }
        }
        public static readonly DependencyProperty maxVisionProperty =
            DependencyProperty.Register("maxVision", typeof(int), typeof(ViewModelControl), new PropertyMetadata(Settings.bacteriaDefaultVision));
        public int maxMaxHeal
        {
            get { return (int)GetValue(maxMaxHealProperty); }
            set
            {
                if (value > maxMaxHeal)
                    SetValue(maxMaxHealProperty, value);
            }
        }
        public static readonly DependencyProperty maxMaxHealProperty =
            DependencyProperty.Register("maxMaxHeal", typeof(int), typeof(ViewModelControl), new PropertyMetadata(Settings.bacteriaDefaultMaxHeal));
        public int maxMaxAge
        {
            get { return (int)GetValue(maxMaxAgeProperty); }
            set
            {
                if (value > maxMaxAge)
                    SetValue(maxMaxAgeProperty, value);
            }
        }
        public static readonly DependencyProperty maxMaxAgeProperty =
            DependencyProperty.Register("maxMaxAge", typeof(int), typeof(ViewModelControl), new PropertyMetadata(Settings.bacteriaDefaultMaxAge));
        public ViewModelControl()
        {

        }
    }
}
