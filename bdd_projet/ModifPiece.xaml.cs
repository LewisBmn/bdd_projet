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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour ModifPiece.xaml
    /// </summary>
    public partial class ModifPiece : Page
    {
        private List<string> listeNum = new List<string> { };

        bool numDel = false;
        bool nomDel = false;
        bool introDel = false;
        bool discDel = false;
        bool descDel = false;
        bool numfournDel = false;
        bool approDel = false;
        bool prixDel = false;
        bool unable = false;

        public ModifPiece(List<string> listeNum)
        {
            InitializeComponent();
            this.listeNum = listeNum;
            unable = true;
        }

        #region GotFocus
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
        private void nom_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nomDel == false)
            {
                nom.Text = "";
                nom.TextAlignment = TextAlignment.Left;
                nom.BorderBrush = Brushes.Black;
                nomDel = true;
                nom.Foreground = Brushes.Black;
            }
        }
        private void dateIntro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (introDel == false)
            {
                dateIntro.Text = "";
                dateIntro.TextAlignment = TextAlignment.Left;
                dateIntro.BorderBrush = Brushes.Black;
                introDel = true;
                dateIntro.Foreground = Brushes.Black;
            }
        }
        private void dateDisc_GotFocus(object sender, RoutedEventArgs e)
        {
            if (discDel == false)
            {
                dateDisc.Text = "";
                dateDisc.TextAlignment = TextAlignment.Left;
                dateDisc.BorderBrush = Brushes.Black;
                discDel = true;
                dateDisc.Foreground = Brushes.Black;
            }
        }
        private void desc_GotFocus(object sender, RoutedEventArgs e)
        {
            if (descDel == false)
            {
                desc.Text = "";
                desc.FontSize = 12;
                desc.TextAlignment = TextAlignment.Left;
                desc.BorderBrush = Brushes.Black;
                descDel = true;
                desc.Foreground = Brushes.Black;
            }
        }
        private void numfourn_GotFocus(object sender, RoutedEventArgs e)
        {
            if (numfournDel == false)
            {
                numfourn.Text = "";
                numfourn.FontSize = 12;
                numfourn.TextAlignment = TextAlignment.Left;
                numfourn.BorderBrush = Brushes.Black;
                numfournDel = true;
                numfourn.Foreground = Brushes.Black;
            }
        }
        private void appro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (approDel == false)
            {
                appro.Text = "";
                appro.FontSize = 12;
                appro.TextAlignment = TextAlignment.Left;
                appro.BorderBrush = Brushes.Black;
                approDel = true;
                appro.Foreground = Brushes.Black;
            }
        }
        private void prix_GotFocus(object sender, RoutedEventArgs e)
        {
            if (prixDel == false)
            {
                prix.Text = "";
                prix.FontSize = 12;
                prix.TextAlignment = TextAlignment.Left;
                prix.BorderBrush = Brushes.Black;
                prixDel = true;
                prix.Foreground = Brushes.Black;
            }
        }
        #endregion
        #region LostFocus
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
        private void desc_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(desc.Text))
            {
                desc.Text = "Description";
                desc.Foreground = Brushes.DarkGray;
                desc.TextAlignment = TextAlignment.Center;
                descDel = false;
            }
        }
        private void nom_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nom.Text))
            {
                nom.Text = "Fournisseur";
                nom.Foreground = Brushes.DarkGray;
                nom.TextAlignment = TextAlignment.Center;
                nomDel = false;
            }
        }
        private void numfourn_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(numfourn.Text))
            {
                numfourn.Text = "n° produit fournisseur";
                numfourn.FontSize = 11;
                numfourn.Foreground = Brushes.DarkGray;
                numfourn.TextAlignment = TextAlignment.Center;
                numfournDel = false;
            }
        }
        private void dateIntro_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dateIntro.Text))
            {
                dateIntro.Text = "Date d'introduction";
                dateIntro.Foreground = Brushes.DarkGray;
                dateIntro.TextAlignment = TextAlignment.Center;
                introDel = false;
            }
        }
        private void appro_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(appro.Text))
            {
                appro.Text = "Délai d'approvisionnement";
                appro.FontSize = 9;
                appro.Foreground = Brushes.DarkGray;
                appro.TextAlignment = TextAlignment.Center;
                approDel = false;
            }
        }
        private void prix_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(prix.Text))
            {
                prix.Text = "Prix";
                prix.Foreground = Brushes.DarkGray;
                prix.TextAlignment = TextAlignment.Center;
                prixDel = false;
            }
        }
        #endregion

        private string GoodMaj(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            if (str.Length > 1 && str.ToLower() != "vtt" && str.ToLower() != "bmx")
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return str.ToUpper();
        }
        private void Unaccessible(TextBox tb)
        {
            tb.IsEnabled = false;
            tb.Foreground = Brushes.DarkGray;
            tb.TextAlignment = TextAlignment.Center;
        }
        private void Reaccessible(TextBox tb)
        {
            tb.IsEnabled = true;
            tb.FontSize = 12;
            tb.TextAlignment = TextAlignment.Left;
            tb.BorderBrush = Brushes.Black;
            tb.Foreground = Brushes.Black;
        }

        public void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(num.Text) == false)
            {
                bool canSubmit = true;
                int delay = 0;
                int cost = 0;
                DateTime dtIn = new DateTime();
                DateTime dtDc = new DateTime();

                bool valDc = false;
                if (string.IsNullOrWhiteSpace(nom.Text) == true || int.TryParse(nom.Text, out int i) == true || nom.Text == "Invalid argument")
                {
                    canSubmit = false;
                    nom.Text = "Invalid argument";
                    nom.TextAlignment = TextAlignment.Center;
                    nom.BorderBrush = Brushes.Red;
                    nom.Foreground = Brushes.Red;
                    nomDel = false;
                }
                if (string.IsNullOrWhiteSpace(desc.Text) || desc.Text == "Description" || desc.Text == "Invalid argument")
                {
                    canSubmit = false;
                    desc.Text = "Invalid argument";
                    desc.TextAlignment = TextAlignment.Center;
                    desc.BorderBrush = Brushes.Red;
                    desc.Foreground = Brushes.Red;
                    descDel = false;
                }
                if (string.IsNullOrWhiteSpace(numfourn.Text) == true || numfourn.Text == "n° produit fournisseur" || numfourn.Text == "Invalid argument")
                {
                    canSubmit = false;
                    numfourn.Text = "Invalid argument";
                    numfourn.FontSize = 12;
                    numfourn.TextAlignment = TextAlignment.Center;
                    numfourn.BorderBrush = Brushes.Red;
                    numfourn.Foreground = Brushes.Red;
                    numfournDel = false;
                }
                if (string.IsNullOrWhiteSpace(prix.Text) == true || int.TryParse(prix.Text, out cost) == false)
                {
                    canSubmit = false;
                    prix.Text = "Invalid argument";
                    prix.TextAlignment = TextAlignment.Center;
                    prix.BorderBrush = Brushes.Red;
                    prix.Foreground = Brushes.Red;
                    prixDel = false;
                }
                if (string.IsNullOrWhiteSpace(appro.Text) == true || int.TryParse(appro.Text, out delay) == false)
                {
                    canSubmit = false;
                    appro.Text = "Invalid argument";
                    appro.TextAlignment = TextAlignment.Center;
                    appro.BorderBrush = Brushes.Red;
                    appro.Foreground = Brushes.Red;
                    appro.FontSize = 12;
                    approDel = false;
                }
                if (DateTime.TryParse(dateIntro.Text, out dtIn) == false)
                {
                    canSubmit = false;
                    dateIntro.Text = "Invalid argument";
                    dateIntro.TextAlignment = TextAlignment.Center;
                    dateIntro.BorderBrush = Brushes.Red;
                    dateIntro.Foreground = Brushes.Red;
                    introDel = false;
                }
                if (dateDisc.Text != "Date discontinuité" && string.IsNullOrWhiteSpace(dateDisc.Text) == false
                    && DateTime.TryParse(dateDisc.Text, out dtDc) == false)
                {
                    canSubmit = false;
                    dateDisc.Text = "Invalid argument";
                    dateDisc.TextAlignment = TextAlignment.Center;
                    dateDisc.BorderBrush = Brushes.Red;
                    dateDisc.Foreground = Brushes.Red;
                    discDel = false;
                }
                if (dateDisc.Text != "Date discontinuité" && string.IsNullOrWhiteSpace(dateDisc.Text) == false && dateDisc.Text != "Invalid argument")
                {
                    valDc = true;
                }
                if (canSubmit)
                {
                    string query = "update pieces set descr=@description, nomFourn=@nom, numProdCat=@numfourn, prix=@prix" +
                        ", dateIntro=@dtIn, dateDiscont=@dtDc, delaiAppr=@delai WHERE numPiece=@num";
                    MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                    command.CommandText = query;
                    command.Parameters.Add("@num", MySqlDbType.VarChar).Value = num.Text.ToUpper();
                    command.Parameters.Add("@description", MySqlDbType.VarChar).Value = GoodMaj(desc.Text);
                    command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = GoodMaj(nom.Text);
                    command.Parameters.Add("@numfourn", MySqlDbType.VarChar).Value = GoodMaj(numfourn.Text);
                    command.Parameters.Add("@prix", MySqlDbType.Int32).Value = cost;
                    command.Parameters.Add("@dtIn", MySqlDbType.DateTime).Value = dtIn;
                    if (valDc)
                    {
                        command.Parameters.Add("@dtDc", MySqlDbType.DateTime).Value = dtDc;
                    }
                    else
                    {
                        command.Parameters.Add("@dtDc", MySqlDbType.DateTime).Value = null;
                    }
                    command.Parameters.Add("@delai", MySqlDbType.Int32).Value = delay;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("Something went wrong...");
                        return;
                    }

                    command.Dispose();
                    if (numDel == true)
                    {
                        MainWindow.Accueil.NavigationService.Navigate(new Pieces());
                    }
                }
                if (canSubmit == false)
                {
                    ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(-10, 0, 0, 0), new Thickness(10, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
                    marginAn.EasingFunction = new BounceEase();

                    MainWindow.Accueil.BeginAnimation(Control.MarginProperty, marginAn);
                    Loading();
                }
            }
        }
        DispatcherTimer timer = new DispatcherTimer();
        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            ThicknessAnimation marginAn = new ThicknessAnimation(new Thickness(10, 0, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 1));
            marginAn.EasingFunction = new QuadraticEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, marginAn);
        }
        void Loading() //0 pour CreationVelo, 1 pour ModifVelo
        {
            timer.Tick += timer_tick;
            timer.Interval = TimeSpan.FromSeconds(0.29);
            timer.Start();
        }

        private void num_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (listeNum.Contains(num.Text.ToLower()))
            {
                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                string request = "SELECT * FROM pieces WHERE numPiece=@num";
                command.CommandText = request;
                command.Parameters.Add("@num", MySqlDbType.VarChar).Value = num.Text;
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    desc.Text = reader.GetString(1);
                    descDel = true;
                    Reaccessible(desc);
                    nom.Text = reader.GetString(2);
                    nomDel = true;
                    Reaccessible(nom);
                    numfourn.Text = reader.GetString(3);
                    numfournDel = true;
                    Reaccessible(numfourn);
                    prix.Text = reader.GetString(4);
                    prixDel = true;
                    Reaccessible(prix);
                    dateIntro.Text = Convert.ToDateTime(reader.GetString(5)).ToString("yyyy-MM-dd");
                    introDel = true;
                    Reaccessible(dateIntro);
                    if (reader.IsDBNull(6))
                    {
                        dateDisc.Text = "";
                    }
                    else
                    {
                        dateDisc.Text = Convert.ToDateTime(reader.GetString(6)).ToString("yyyy-MM-dd");
                    }
                    discDel = true;
                    Reaccessible(dateDisc);
                    appro.Text = reader.GetString(7);
                    approDel = true;
                    Reaccessible(appro);
                }

                command.Dispose();
                Submit.IsEnabled = true;
            }
            else if (unable)
            {
                Unaccessible(desc);
                desc.Text = "Description";
                Unaccessible(nom);
                nom.Text = "Fournisseur";
                Unaccessible(numfourn);
                numfourn.Text = "n° produit fournisseur";
                numfourn.FontSize = 11;
                Unaccessible(prix);
                prix.Text = "Prix";
                Unaccessible(dateIntro);
                dateIntro.Text = "Date d'introduction";
                Unaccessible(dateDisc);
                dateDisc.Text = "Date discontinuité";
                Unaccessible(appro);
                appro.Text = "Délai d'approvisionnement";
                Submit.IsEnabled = false;
            }
        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit_Click(sender, e);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(desc), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(nom), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(numfourn), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(prix), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(dateIntro), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(dateDisc), null);
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(appro), null);
                Keyboard.ClearFocus();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 750, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 800));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.2));
            doubleAnimation.EasingFunction = new ExponentialEase();

            MainWindow.Accueil.BeginAnimation(Control.MarginProperty, db);
            MainWindow.Accueil.BeginAnimation(Control.OpacityProperty, doubleAnimation);
        }
    }
}
