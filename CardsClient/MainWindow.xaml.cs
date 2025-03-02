using CardsClient.ViewModels;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace CardsClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
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
