using System.Collections.ObjectModel;
using System.Windows.Input;
using CardsClient.Services;
using Common.Models;
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.IO;

namespace CardsClient.ViewModels
{
    public class MainVM : BaseVM
    {
        private readonly ApiController _apiService;
        private string _description;
        private string _imgPath;
        private string _initImgPath;
        private ICommand LoadDataCommand { get; } 
        public ObservableCollection<Card> CardsCollection { get; private set; }
        public ICommand SubmitCommand { get; set; }
        
        public string InputDescription
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string SelectedImgPath
        {
            get
            {
                return _imgPath;
            }
            set
            {
                _imgPath = value;
                OnPropertyChanged();
            }
        }
        public MainVM() 
        {
            _apiService = new ApiController();
            _initImgPath = "\\Icons\\InitIcon.png";
            CardsCollection = new ObservableCollection<Card>();
            SubmitCommand = new AsyncRelayCommand(Submit);
            LoadDataCommand = new AsyncRelayCommand(LoadData);
            LoadDataCommand.Execute(this);
        }
        private async Task LoadData()
        {
            try
            {
                var list = await _apiService.GetCardsAsync();
                foreach (var item in list)
                {
                    CardsCollection.Add(new Card(item.Description, File.Exists(item.ImgPath) ? item.ImgPath : _initImgPath));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task Submit()
        {
            try
            {
                var item = new Card(InputDescription, SelectedImgPath);
                await _apiService.PostCardAsync(item);
                CardsCollection.Clear();
                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}