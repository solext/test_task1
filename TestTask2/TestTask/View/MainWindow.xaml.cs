﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestTask.ViewModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModelMain();
        }

        private void checkbutton_Click(object sender, RoutedEventArgs e)
        {
            if (checkbutton.Content.ToString() != "Check All")
            {
                checkbutton.Content = "Check All";
            }
            else
                checkbutton.Content = "Unheck All";
        }
    }
}
