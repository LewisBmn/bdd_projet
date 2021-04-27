using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();

            DoubleAnimation db = new DoubleAnimation(100, 450, TimeSpan.FromSeconds(1.5));
            CubicEase ease = new CubicEase();
            ease.EasingMode = EasingMode.EaseOut;
            db.EasingFunction = ease;

            this.BeginAnimation(Control.HeightProperty, db);
            Loading(0);
            
        }
        DispatcherTimer timer = new DispatcherTimer();
        private void timer_tick0(object sender, EventArgs e)
        {
            timer.Stop();
            Transition();
        }
        private void timer_tick1(object sender, EventArgs e)
        {
            timer.Stop();
            Panel.SetZIndex(frame, 1);
            frame.NavigationService.Navigate(new MainWindow());
        }
        void Loading(int val) //1 = vélo ; 0 = home ; 2 = pieces
        {
            if (val == 0)
            {
                timer.Tick += timer_tick0;
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Start();
            }
            if (val == 1)
            {
                timer.Tick += timer_tick1;
                timer.Interval = TimeSpan.FromSeconds(0.15);
                timer.Start();
            }
        }
        private void Transition()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.15));
            doubleAnimation.EasingFunction = new ExponentialEase();

            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 270), new TimeSpan(0, 0, 0, 0, 500));
            marginAn.EasingFunction = new ExponentialEase();

            Grid.BeginAnimation(Control.MarginProperty, marginAn);
            Grid.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            Loading(1);
        }
    }
}
