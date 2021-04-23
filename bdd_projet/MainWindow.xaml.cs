﻿using System;
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
        }

        private void Velos_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Velo());
        }
        private void Pieces_Click(object sender, RoutedEventArgs e)
        {
            Accueil.NavigationService.Navigate(new Pieces());
        }
    }
}
