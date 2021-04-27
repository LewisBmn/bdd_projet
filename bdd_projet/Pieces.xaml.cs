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
using System.Data;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace bdd_projet
{
    public partial class Pieces : Page
    {
        private bool numDel = false;
        private List<string> listeNum = new List<string> { };

        public Pieces()
        {
            InitializeComponent();
        }

        #region Click and Focus
        private void Click(int val)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(730, 0, 0, 0), new TimeSpan(0, 0, 0, 1, 0));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, db);
            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            Loading(val);
        }
        private void Creation_Click(object sender, RoutedEventArgs e)
        {
            Click(0);
        }
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Click(1);
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

            if (string.IsNullOrWhiteSpace(num.Text) == true)
            {
                canSubmit = false;
                num.Text = "Invalid argument";
                num.TextAlignment = TextAlignment.Center;
                num.BorderBrush = Brushes.Red;
                num.Foreground = Brushes.Red;
                numDel = false;
            }
            if (canSubmit)
            {
                string delTable = "DELETE FROM pieces WHERE numPiece = @num";

                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = delTable;
                command.Parameters.Add("@num", MySqlDbType.String).Value = num.Text.ToLower();

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return;
                }

                command.Dispose();
                if (listeNum.Contains(num.Text) == false)
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
                    MainWindow.Accueil.NavigationService.Navigate(new Pieces());
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
        #endregion

        private void Pieces_Loaded(object sender, RoutedEventArgs e)
        {
            string query = "Select * from pieces";
            MySqlCommand command = MainWindow.maConnexion.CreateCommand();
            command.CommandText = query;

            MySqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGrid1.ItemsSource = dt.DefaultView;

            command.CommandText = "SELECT DISTINCT numPiece FROM pieces";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                listeNum.Add(reader.GetString(0).ToLower());
            }
            command.Dispose();

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 2, 0));
            doubleAnimation.EasingFunction = new ExponentialEase();

            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(0, 20, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
            marginAn.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);
            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, marginAn);
        }
        DispatcherTimer timer = new DispatcherTimer();
        void Loading(int val) //0 pour CreationPiece, 1 pour ModifPiece
        {
            timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
            {
                timer.Stop();
                MainWindow.Accueil.NavigationService.Navigate(new CreationPiece(listeNum, val));
            });
            timer.Interval = TimeSpan.FromSeconds(0.35);
            timer.Start();
        }
    }
}
