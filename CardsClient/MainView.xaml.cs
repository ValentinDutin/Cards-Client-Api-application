using CardsClient.ViewModels;
using Microsoft.Win32;
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

namespace CardsClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
        private void SelectImg_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*jpeg;*png;*bmp"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var viewModel = (MainVM)DataContext;
                viewModel.SelectedImgPath = openFileDialog.FileName;
            }
        }
    }
}
