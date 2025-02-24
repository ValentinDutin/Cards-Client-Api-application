using System.Collections.ObjectModel;
using System.Windows.Input;
using CardsClient.Commands;
using CardsClient.Services;
using Common.Models;
using System;

namespace CardsClient.ViewModels
{
    public class MainVM : BaseVM
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Card> CardsCollection { get; private set; }
        public ICommand SubmitCommand { get; set; }
        private string _description;
        private string _imgPath;
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
            _apiService = new ApiService();
            CardsCollection = new ObservableCollection<Card>();
            SubmitCommand = new RelayCommand(o => Submit());
            LoadData();
        }
        private async void LoadData()
        {
            try
            {
                var list = await _apiService.GetCardsAsync();
                foreach (var item in list)
                {
                    CardsCollection.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void Submit()
        {
            try
            {
                var item = new Card() { Description = InputDescription, ImgPath = SelectedImgPath };
                await _apiService.PostCardAsync(item);
                CardsCollection.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}