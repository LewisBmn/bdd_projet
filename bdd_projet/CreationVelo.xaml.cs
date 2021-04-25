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

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour CreationVelo.xaml
    /// </summary>
    public partial class CreationVelo : Page
    {
        private bool numDel = false;
        private bool nomDel = false;
        private bool grandeurDel = false;
        private bool prixDel = false;
        private bool typeDel = false;
        private bool introDel = false;
        private bool discDel = false;

        private MySqlConnection maConnexion;

        private Frame frame;

        public CreationVelo(MySqlConnection maConnexion, Frame frame)
        {
            InitializeComponent();
            this.maConnexion = maConnexion;
            this.frame = frame;
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

        private void grandeur_GotFocus(object sender, RoutedEventArgs e)
        {
            if (grandeurDel == false)
            {
                grandeur.Text = "";
                grandeur.TextAlignment = TextAlignment.Left;
                grandeur.BorderBrush = Brushes.Black;
                grandeurDel = true;
                grandeur.Foreground = Brushes.Black;
            }
        }

        private void prix_GotFocus(object sender, RoutedEventArgs e)
        {
            if (prixDel == false)
            {
                prix.Text = "";
                prix.TextAlignment = TextAlignment.Left;
                prix.BorderBrush = Brushes.Black;
                prixDel = true;
                prix.Foreground = Brushes.Black;
            }
        }

        private void type_GotFocus(object sender, RoutedEventArgs e)
        {
            if (typeDel == false)
            {
                type.Text = "";
                type.TextAlignment = TextAlignment.Left;
                type.BorderBrush = Brushes.Black;
                typeDel = true;
                type.Foreground = Brushes.Black;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool canSubmit = true;
            int val = 0;
            int cost = 0;
            DateTime dtIn = new DateTime();
            DateTime dtDc = new DateTime();

            bool valDc = false;

            if (string.IsNullOrWhiteSpace(num.Text) == true || int.TryParse(num.Text, out val) == false)
            {
                canSubmit = false;
                num.Text = "Invalid argument";
                num.TextAlignment = TextAlignment.Center;
                num.BorderBrush = Brushes.Red;
                num.Foreground = Brushes.Red;
                numDel = false;
            }
            if (string.IsNullOrWhiteSpace(nom.Text) == true || int.TryParse(nom.Text, out int i) == true || nom.Text == "Nom du produit")
            {
                canSubmit = false;
                nom.Text = "Invalid argument";
                nom.TextAlignment = TextAlignment.Center;
                nom.BorderBrush = Brushes.Red;
                nom.Foreground = Brushes.Red;
                nomDel = false;
            }
            if (grandeur.Text.ToLower() != "adultes" && grandeur.Text.ToLower() != "jeunes" && grandeur.Text.ToLower() != "hommes"
               && grandeur.Text.ToLower() != "dames" && grandeur.Text.ToLower() != "filles" && grandeur.Text.ToLower() != "garçons")
            {
                canSubmit = false;
                grandeur.Text = "Invalid argument";
                grandeur.TextAlignment = TextAlignment.Center;
                grandeur.BorderBrush = Brushes.Red;
                grandeur.Foreground = Brushes.Red;
                grandeurDel = false;
            }
            if (int.TryParse(prix.Text, out cost) == false || cost < 0)
            {
                canSubmit = false;
                prix.Text = "Invalid argument";
                prix.TextAlignment = TextAlignment.Center;
                prix.BorderBrush = Brushes.Red;
                prix.Foreground = Brushes.Red;
                prixDel = false;
            }
            if (type.Text.ToUpper() != "VTT" && type.Text.ToLower() != "vélo de course" && type.Text.ToLower() != "classique" && type.Text.ToUpper() != "BMX")
            {
                canSubmit = false;
                type.Text = "Invalid argument";
                type.TextAlignment = TextAlignment.Center;
                type.BorderBrush = Brushes.Red;
                type.Foreground = Brushes.Red;
                typeDel = false;
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
            if (dateDisc.Text != "Date discontinuité" && string.IsNullOrWhiteSpace(dateDisc.Text) == false && DateTime.TryParse(dateDisc.Text, out dtDc) == false)
            {
                canSubmit = false;
                dateDisc.Text = "Invalid argument";
                dateDisc.TextAlignment = TextAlignment.Center;
                dateDisc.BorderBrush = Brushes.Red;
                dateDisc.Foreground = Brushes.Red;
                discDel = false;
            }
            if (dateDisc.Text != "Date discontinuité" && string.IsNullOrWhiteSpace(dateDisc.Text) == false)
            {
                valDc = true;
            }
            if (canSubmit)
            {
                string insertTable = "insert into velo values (@num, @nom, @grandeur, @cost, @type, @dtIn, @dtDc)";
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = insertTable;
                command.Parameters.Add("@num", MySqlDbType.Int32).Value = val;
                command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = GoodMaj(nom.Text);
                command.Parameters.Add("@grandeur", MySqlDbType.VarChar).Value = GoodMaj(grandeur.Text);
                command.Parameters.Add("@cost", MySqlDbType.Int32).Value = cost;
                command.Parameters.Add("@type", MySqlDbType.VarChar).Value = GoodMaj(type.Text);
                command.Parameters.Add("@dtIn", MySqlDbType.DateTime).Value = dtIn;
                if (valDc)
                {
                    command.Parameters.Add("@dtDc", MySqlDbType.DateTime).Value = dtDc;
                }
                else
                {
                    command.Parameters.Add("@dtDc", MySqlDbType.DateTime).Value = null;
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException er)
                {
                    if (er.Code == 0)
                    {
                        num.Text = "Le n° doit être unique";
                        num.FontSize = 10;
                        num.TextAlignment = TextAlignment.Center;
                        num.BorderBrush = Brushes.Red;
                        num.Foreground = Brushes.Red;
                        numDel = false;
                    }
                    return;
                }

                command.Dispose();
                if (numDel == true)
                {
                    frame.NavigationService.Navigate(new Velo(frame, maConnexion));
                }
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

        private void nom_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nom.Text))
            {
                nom.Text = "Nom du produit";
                nom.Foreground = Brushes.DarkGray;
                nom.TextAlignment = TextAlignment.Center;
                nomDel = false;
            }
        }

        private void grandeur_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(grandeur.Text))
            {
                grandeur.Text = "Grandeur";
                grandeur.Foreground = Brushes.DarkGray;
                grandeur.TextAlignment = TextAlignment.Center;
                grandeurDel = false;
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

        private void type_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(type.Text))
            {
                type.Text = "Type";
                type.Foreground = Brushes.DarkGray;
                type.TextAlignment = TextAlignment.Center;
                typeDel = false;
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
        private string GoodMaj(string str)
        {
            if(string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            if(str.Length>1 && str.ToLower() != "vtt" && str.ToLower()!="bmx")
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return str.ToUpper();
        }
    }
}
