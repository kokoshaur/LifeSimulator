using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int speed
        {
            get { if ((int)GetValue(speedProperty) != 0)
                    return (100 / (int)GetValue(speedProperty));
                else return 25;
            }
            set {SetValue(speedProperty, value); }
        }
        public static readonly DependencyProperty speedProperty =
            DependencyProperty.Register("speed", typeof(int), typeof(ViewModelControl), new PropertyMetadata(null));
        public ViewModelControl()
        {

        }
    }
}
