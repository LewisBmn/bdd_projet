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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection maConnexion;

        public MainWindow()
        {
            InitializeComponent();
            Accueil.NavigationService.Navigate(new Home());
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

        private void Velos_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Velo(Accueil, maConnexion));
            TglButton.IsChecked = false;
        }
        private void Pieces_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Pieces(Accueil, maConnexion));
            TglButton.IsChecked = false;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(TglButton.IsChecked==true)
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
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Home());
            TglButton.IsChecked = false;
        }
    }
}
