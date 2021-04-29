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
        private bool siretDel = false;

        private List<string[]> listeNumSiret = new List<string[]> { };

        public Pieces()
        {
            InitializeComponent();
        }

        #region Click and Focus
        private void Click()
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(730, 0, 0, 0), new TimeSpan(0, 0, 0, 1, 0));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, db);
            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);
        }
        private void Creation_Click(object sender, RoutedEventArgs e)
        {
            Click();
            timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
            {
                timer.Stop();
                MainWindow.Accueil.NavigationService.Navigate(new CreationPiece(listeNumSiret, 0));
            });
            timer.Interval = TimeSpan.FromSeconds(0.35);
            timer.Start();
        }
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            Click();
            timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
            {
                timer.Stop();
                MainWindow.Accueil.NavigationService.Navigate(new CreationPiece(listeNumSiret, 1));
            });
            timer.Interval = TimeSpan.FromSeconds(0.35);
            timer.Start();
        }
        private void Suppr_Click(object sender, RoutedEventArgs e)
        {
            num.IsEnabled = true;
            siret.IsEnabled = true;
            num.Visibility = Visibility.Visible;
            del.Visibility = Visibility.Visible;
            siret.Visibility = Visibility.Visible;
            del.IsEnabled = true;

            Submission.Opacity = 1;

            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(-220, 0, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 500));
            marginAn.EasingFunction = new QuadraticEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 500));
            ExponentialEase be = new ExponentialEase(); be.EasingMode = EasingMode.EaseIn;
            doubleAnimation.EasingFunction = be;

            Submission.BeginAnimation(Control.MarginProperty, marginAn);
            Submission.BeginAnimation(Control.OpacityProperty, doubleAnimation);
        }
        private void del_Click(object sender, RoutedEventArgs e)
        {
            bool canSubmit = true;
            int val = 0;

            if (string.IsNullOrWhiteSpace(num.Text) == true || num.Text == "N° du produit")
            {
                canSubmit = false;
                num.Text = "Argument invalide";
                num.TextAlignment = TextAlignment.Center;
                num.BorderBrush = Brushes.Red;
                num.Foreground = Brushes.Red;
                numDel = false;
            }
            if (string.IsNullOrWhiteSpace(siret.Text) == true || int.TryParse(siret.Text, out val) == false)
            {
                canSubmit = false;
                siret.Text = "Argument invalide";
                siret.TextAlignment = TextAlignment.Center;
                siret.BorderBrush = Brushes.Red;
                siret.Foreground = Brushes.Red;
                siretDel = false;
            }
            if (canSubmit)
            {
                string delTable = "DELETE FROM pieces WHERE numPiece = @num AND Siret = @siret";

                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = delTable;
                command.Parameters.Add("@num", MySqlDbType.String).Value = num.Text.ToLower();
                command.Parameters.Add("@siret", MySqlDbType.String).Value = val;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return;
                }

                command.Dispose();

                if (!AssociationNumSiretExiste(num.Text, val))
                {
                    num.Text = "Le n° doit exister";
                    num.TextAlignment = TextAlignment.Center;
                    num.BorderBrush = Brushes.Red;
                    num.Foreground = Brushes.Red;
                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope(num), null);
                    numDel = false;

                    siret.Text = "Le n° doit exister";
                    siret.TextAlignment = TextAlignment.Center;
                    siret.BorderBrush = Brushes.Red;
                    siret.Foreground = Brushes.Red;
                    FocusManager.SetFocusedElement(FocusManager.GetFocusScope(siret), null);
                    siretDel = false;

                    Keyboard.ClearFocus();
                }
                else
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

            command.CommandText = "SELECT numPiece, Siret FROM pieces";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                listeNumSiret.Add(new string[] { reader.GetString(0).ToLower(), reader.GetString(1) });
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
        private bool AssociationNumSiretExiste(string num, int siret)
        {
            for(int i = 0; i<listeNumSiret.Count; i++)
            {
                if(listeNumSiret[i][0] == num && int.Parse(listeNumSiret[i][1]) == siret) { return true; }
            }
            return false;
        }
        private void siret_GotFocus(object sender, RoutedEventArgs e)
        {
            if (siretDel == false)
            {
                siret.Text = "";
                siret.FontSize = 12;
                siret.TextAlignment = TextAlignment.Left;
                siret.BorderBrush = Brushes.Black;
                siretDel = true;
                siret.Foreground = Brushes.Black;
            }
        }

        private void siret_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(siret.Text))
            {
                siret.Text = "Siret";
                siret.Foreground = Brushes.DarkGray;
                siret.TextAlignment = TextAlignment.Center;
                siretDel = false;
            }
        }

        private void siret_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                del_Click(sender, e);
            }
        }
    }
}
