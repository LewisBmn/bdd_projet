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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MySqlConnection maConnexion;
        public static Frame Accueil;
        bool firstTime = true;

        public MainWindow()
        {
            InitializeComponent();

            MinimizeButton.Click += (s, e) => WindowState = WindowState.Minimized;
            CloseButton.Click += (s, e) => Close();

            Accueil = AccueilW;
            Accueil.NavigationService.Navigate(new Home());

            Loader.Visibility = Visibility.Hidden;
            Barre.Visibility = Visibility.Hidden;

            DoubleAnimation db = new DoubleAnimation(0, 450, TimeSpan.FromSeconds(2));
            CubicEase ease = new CubicEase();
            ease.EasingMode = EasingMode.EaseOut;
            db.EasingFunction = ease;

            this.BeginAnimation(Control.HeightProperty, db);
            Loading(-1);

            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;" +
                                         "DATABASE=velomax;" +
                                         "UID=root;PASSWORD=root";

                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(" ErreurConnexion : " + e.ToString());
                return;
            }
        }

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();

        #region Click/Transitions
        void Loading(int val) //1 = vélo ; 0 = home ; 2 = pieces ; -1 = start ; -2 affichage après start
        {
            if (val == -2)
            {
                timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer.Stop();
                    if (firstTime)
                    {
                        firstTime = false;
                        Start.Visibility = Visibility.Collapsed;

                        DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 1, 2));
                        doubleAnimation.EasingFunction = new ExponentialEase();

                        ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 300, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 1, 0));
                        marginAn.EasingFunction = new ExponentialEase();

                        Loader.BeginAnimation(Control.OpacityProperty, doubleAnimation);
                        Barre.BeginAnimation(Control.OpacityProperty, doubleAnimation);
                        Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);

                        Barre.BeginAnimation(Control.MarginProperty, marginAn);
                        Loader.BeginAnimation(Control.MarginProperty, marginAn);
                        Accueil.BeginAnimation(Control.MarginProperty, marginAn);
                    }
                });
                timer.Interval = TimeSpan.FromSeconds(0.300);
                timer.Start();
            }
            else if (val == -1)
            {
                timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer.Stop();
                    DoubleAnimation db = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 1, 0));
                    db.EasingFunction = new ExponentialEase();

                    Start.BeginAnimation(Control.OpacityProperty, db);
                });
                timer.Interval = TimeSpan.FromSeconds(0.4);
                timer.Start();

                timer2.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer2.Stop();
                    DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, new TimeSpan(0, 0, 0, 0, 200));
                    doubleAnimation.EasingFunction = new ExponentialEase();

                    ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 400), new TimeSpan(0, 0, 0, 1, 0));
                    marginAn.EasingFunction = new ExponentialEase();

                    Start.BeginAnimation(Control.OpacityProperty, doubleAnimation);
                    Start.BeginAnimation(Control.MarginProperty, marginAn);

                    Loader.Visibility = Visibility.Visible;
                    Barre.Visibility = Visibility.Visible;

                    Loading(-2);
                });
                timer2.Interval = TimeSpan.FromSeconds(3);
                timer2.Start();
            }
            else if (val == 0)
            {
                timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer.Stop();
                    Accueil.Visibility = Visibility.Visible;

                    Accueil.NavigationService.Navigate(new Home());
                    TglButton.IsChecked = false;

                    DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 2, 0));
                    doubleAnimation.EasingFunction = new ExponentialEase();

                    ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 200, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
                    marginAn.EasingFunction = new ExponentialEase();

                    Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);
                    Accueil.BeginAnimation(Control.MarginProperty, marginAn);
                });
                timer.Interval = TimeSpan.FromSeconds(0.15);
                timer.Start();
            }
            else if (val == 1)
            {
                timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer.Stop();
                    Accueil.Visibility = Visibility.Visible;

                    Accueil.NavigationService.Navigate(new Velo());
                    TglButton.IsChecked = false;
                });
                timer.Interval = TimeSpan.FromSeconds(0.15);
                timer.Start();
            }
            else if (val == 2)
            {
                timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
                {
                    timer.Stop();
                    Accueil.Visibility = Visibility.Visible;

                    Accueil.NavigationService.Navigate(new Pieces());
                    TglButton.IsChecked = false;
                });
                timer.Interval = TimeSpan.FromSeconds(0.15);
                timer.Start();
            }
        }
        private void Click(int val)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.15));
            doubleAnimation.EasingFunction = new ExponentialEase();

            ThicknessAnimation marginAn = new ThicknessAnimation();
            if (Accueil.NavigationService.Content.GetType().ToString() == "bdd_projet.Home")
            {
                marginAn = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 270), new TimeSpan(0, 0, 0, 0, 500));
            }
            else
            {
                marginAn = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 500));
            }
            marginAn.EasingFunction = new ExponentialEase();

            Accueil.Opacity = 1;

            Accueil.BeginAnimation(Control.MarginProperty, marginAn);
            Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            Loading(val);
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Click(0);
        }
        private void Velos_Click(object sender, RoutedEventArgs e)
        {
            Click(1);
        }
        private void Pieces_Click(object sender, RoutedEventArgs e)
        {
            Click(2);
        }
        #endregion

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (TglButton.IsChecked == true)
            {
                tt_velos.Visibility = Visibility.Collapsed;
                tt_pieces.Visibility = Visibility.Collapsed;
                tt_home.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_velos.Visibility = Visibility.Visible;
                tt_pieces.Visibility = Visibility.Visible;
                tt_home.Visibility = Visibility.Visible;
            }
        }
        private void Accueil_PreviwMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TglButton.IsChecked = false;
        }
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Deactivated += (senders, args) => { this.WindowState = WindowState.Minimized; };
        }
    }
}
