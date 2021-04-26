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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Threading;

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour Velo.xaml
    /// </summary>
    public partial class Velo : Page
    {
        List<int> listeNum = new List<int> { };

        private bool numDel = false;

        public Velo()
        {
            InitializeComponent();
        }

        private void Creation_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(730, 0, 0, 0), new TimeSpan(0, 0, 0, 1, 0));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, db);
            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            Loading(0);
        }

        DispatcherTimer timer = new DispatcherTimer();

        private void timer_tick0(object sender, EventArgs e)
        {
            timer.Stop();
            MainWindow.Accueil.NavigationService.Navigate(new CreationVelo());
        }
        private void timer_tick1(object sender, EventArgs e)
        {
            timer.Stop();
            MainWindow.Accueil.NavigationService.Navigate(new ModifVelo(listeNum));
        }
        void Loading(int val) //0 pour CreationVelo, 1 pour ModifVelo
        {
            if (val == 0)
            {
                timer.Tick += timer_tick0;
                timer.Interval = TimeSpan.FromSeconds(0.35);
                timer.Start();
            }
            if(val==1)
            {
                timer.Tick += timer_tick1;
                timer.Interval = TimeSpan.FromSeconds(0.35);
                timer.Start();
            }
        }
        private void Velo_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "Select * from velo";
            MySqlCommand command = MainWindow.maConnexion.CreateCommand();
            command.CommandText = query;

            MySqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGrid1.ItemsSource = dt.DefaultView;

            command.CommandText = "SELECT DISTINCT numProduit FROM velo";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                listeNum.Add(Int32.Parse(reader.GetString(0)));
            }
            command.Dispose();

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 2, 0));
            doubleAnimation.EasingFunction = new ExponentialEase();

            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 20, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
            marginAn.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);
            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, marginAn);
        }

        private void Suppr_Click(object sender, RoutedEventArgs e)
        {
            num.IsEnabled = true;
            num.Visibility = Visibility.Visible;
            del.Visibility = Visibility.Visible;
            del.IsEnabled = true;

            Submission.Opacity = 1;

            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(-220, 0, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 500));
            marginAn.EasingFunction = new QuadraticEase();


            Submission.BeginAnimation(Control.MarginProperty, marginAn);
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            bool canSubmit = true;
            int val = 0;

            if (string.IsNullOrWhiteSpace(num.Text) == true || int.TryParse(num.Text, out val) == false)
            {
                canSubmit = false;
                num.Text = "Invalid argument";
                num.TextAlignment = TextAlignment.Center;
                num.BorderBrush = Brushes.Red;
                num.Foreground = Brushes.Red;
                numDel = false;
            }
            if(canSubmit)
            {
                string delTable = "DELETE FROM velo WHERE numProduit = @num";

                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = delTable;
                command.Parameters.Add("@num", MySqlDbType.Int32).Value = val;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return;
                }

                command.Dispose();
                if (listeNum.Contains(val)==false)
                {
                    num.Text = "Le n° doit exister";
                    num.FontSize = 10;
                    num.TextAlignment = TextAlignment.Center;
                    num.BorderBrush = Brushes.Red;
                    num.Foreground = Brushes.Red;
                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope(num), null);
                    Keyboard.ClearFocus();
                    numDel = false;
                }
                if (numDel == true)
                {
                    MainWindow.Accueil.NavigationService.Navigate(new Velo());
                }
            }
        }

        private void num_GotFocus(object sender, RoutedEventArgs e)
        {
            if (numDel == false)
            {
                num.Text = "";
                num.FontSize = 12;
                num.TextAlignment = TextAlignment.Left;
                num.BorderBrush = Brushes.Black;
                numDel = true;
                num.Foreground = Brushes.Black;
            }
        }

        private void num_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(num.Text))
            {
                num.Text = "N° du produit";
                num.Foreground = Brushes.DarkGray;
                num.TextAlignment = TextAlignment.Center;
                numDel = false;
            }
        }

        private void num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                del_Click(sender, e);
            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(730, 0, 0, 0), new TimeSpan(0, 0, 0, 1, 0));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, db);
            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            Loading(1);
        }
    }
}
