using CardsClient.Services;
using CardsClient.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using CommonFiles.Services;

namespace CardsClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }
        void App_Startup(object sender, StartupEventArgs e)
        {
            var serviceProvider = new ServiceCollection()
                .AddHttpClient<IHttpClientService, HttpClientService>()
                .Services
                .AddSingleton<MainVM>()
                .AddSingleton<IConfigDataService, ConfigDataService>()
                .AddSingleton<IHttpClientService, HttpClientService>()
                .BuildServiceProvider();
            var mainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetService<MainVM>()
            };
            mainWindow.Show();
        }
    }
}
