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
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour AffichageDemo.xaml
    /// </summary>
    public partial class AffichageDemo : Page
    {
        private int value = 0;
        public AffichageDemo(int value, bool next)
        {
            InitializeComponent();

            before.IsEnabled = true;
            after.IsEnabled = true;

            ThicknessAnimation db = new ThicknessAnimation();
            if (next)
            {
                db = new ThicknessAnimation(new Thickness(730, 0, 0, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
            }
            else
            {
                db = new ThicknessAnimation(new Thickness(0, 0, 730, 0), new Thickness(0, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
            }
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.5));
            doubleAnimation.EasingFunction = new ExponentialEase();

            Anim.BeginAnimation(Control.MarginProperty, db);
            Anim.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            this.value = value;
            Process();
        }

        public void Process()
        {
            if (value == 0)
            {
                string query = "SELECT COUNT(DISTINCT No_client) FROM Clients;";
                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = query;

                MySqlDataReader reader = command.ExecuteReader();
                int nb = reader.Read() ? reader.GetInt32(0) : -1;
                infos.Visibility = Visibility.Visible;

                infos.Text = "Nombre de client dans la base de données : " + nb;
                command.Dispose();

                before.Visibility = Visibility.Hidden;
            }
            if (value == 1)
            {
                Title.Text = "Clients et cumul de leurs\n commandes en euros";
                string query = "SELECT numCommande as 'n° de commande', c.nom, c.prenom as 'prénom', prixTotal as 'prix total (en €)' " +
                                "from clients as c, " +
                                "commande, " +
                                    "(SELECT sum(prix) as prixTotal, " +
                                    "No_commande as numCommande " +
                                    "FROM( " +
                                        "SELECT p.prix, i.No_commande " +
                                        "FROM pieces as p, itemcommande as i " +
                                        "where p.numpiece = i.Ref_item " +
                                        "UNION ALL " +
                                        "SELECT v.prix, i.No_commande " +
                                        "FROM velo as v, itemcommande as i " +
                                        "where v.numProduit = i.Ref_item and i.Ref_item REGEXP '^-?[0-9]+$' " +
                                    ") as sub " +
                            "GROUP BY numCommande) as p " +
                        "WHERE numCommande = commande.No_commande and commande.No_client = c.No_client " +
                        "ORDER BY numCommande";

                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                dataGrid1.Visibility = Visibility.Visible;
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGrid1.ItemsSource = dt.DefaultView;

                before.Visibility = Visibility.Visible;
                infos.Visibility = Visibility.Hidden;
            }
            if (value == 2)
            {
                Title.Text = "Liste des pièces avec \nun faible stock";

                string query = "SELECT * from PIECES where quantite < 3";
                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = query;

                MySqlDataReader reader = command.ExecuteReader();
                dataGrid1.Visibility = Visibility.Visible;
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
            if (value == 3)
            {
                Title.Text = "Nombre de pièces fournies \npar fournisseur";

                string query = "SELECT numsiret as 'n° de siret', sum(quantite) as 'quantité' " +
                                "from fournisseur as f, " +
                                "( " +
                                    "SELECT f.siret as numsiret, p.quantite as quantite " +
                                    "from pieces as p, " +
                                    "fournisseur as f " +
                                    "where p.Siret = f.siret " +
                                ") " +
                                "as o " +
                                "WHERE numsiret = f.siret " +
                                "GROUP BY numsiret";

                MySqlCommand command = MainWindow.maConnexion.CreateCommand();
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                dataGrid1.Visibility = Visibility.Visible;
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGrid1.ItemsSource = dt.DefaultView;
            }
            if (value == 4)
            {
                Title.Text = "Export en xml de la table vélos";
                after.Visibility = Visibility.Hidden;
            }
        }
        DispatcherTimer timer = new DispatcherTimer();
        private void after_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 730, 0), new TimeSpan(0, 0, 0, 0, 400));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            Anim.BeginAnimation(Control.MarginProperty, db);
            Anim.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
            {
                timer.Stop();
                MainWindow.Accueil.NavigationService.Navigate(new AffichageDemo(value + 1, true));
            });
            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Start();
            before.IsEnabled = false;
            after.IsEnabled = false;
        }

        private void before_Click(object sender, RoutedEventArgs e)
        {
            ThicknessAnimation db = new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(730, 0, 0, 0), new TimeSpan(0, 0, 0, 0, 400));
            db.EasingFunction = new ExponentialEase();

            DoubleAnimation doubleAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.35));
            doubleAnimation.EasingFunction = new ExponentialEase();

            Anim.BeginAnimation(Control.MarginProperty, db);
            Anim.BeginAnimation(Control.OpacityProperty, doubleAnimation);

            timer.Tick += new EventHandler(delegate (Object o, EventArgs a)
            {
                timer.Stop();
                MainWindow.Accueil.NavigationService.Navigate(new AffichageDemo(value - 1, false));
            });
            timer.Interval = TimeSpan.FromSeconds(0.3);
            timer.Start();
            before.IsEnabled = false;
            after.IsEnabled = false;
        }

    }
}
