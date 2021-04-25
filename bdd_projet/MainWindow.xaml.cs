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

namespace bdd_projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Accueil.NavigationService.Navigate(new Home());
        }

        private void Velos_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Velo(Accueil));
            TglButton.IsChecked = false;
        }
        private void Pieces_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Pieces());
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

        private void TglButton_Unchecked(object sender, RoutedEventArgs e)
        {
            //Accueil.Opacity = 1;
        }

        private void TglButton_Checked(object sender, RoutedEventArgs e)
        {
            //Accueil.Opacity = 0.3;
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
