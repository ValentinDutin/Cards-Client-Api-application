using System.Collections.ObjectModel;
using Common.Models;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Diagnostics;
using CardsClient.Services;
using System.Windows;

namespace CardsClient.ViewModels
{
    public class MainVM : BaseVM
    {
        private readonly string _initImgPath;
        private readonly IHttpClientService _httpClientService;
        private string? _description;
        private string? _imgPath;
        private Card? _selectedItem;
        private IAsyncRelayCommand LoadDataCommand { get; }
        public ObservableCollection<Card> CardsCollection { get; private set; }
        public IAsyncRelayCommand SubmitCommand { get; set; }
        public IAsyncRelayCommand DeleteCardByIdCommand { get; set; }
        public IAsyncRelayCommand DeleteAllCardsCommand { get; set; }

        public Card? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        public string? InputDescription
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string? SelectedImgPath
        {
            get => _imgPath;
            set
            {
                _imgPath = value;
                OnPropertyChanged();
            }
        }
        public MainVM(IHttpClientService httpClientService)
        {
            _initImgPath = ((App)Application.Current).ConfigData["InitIconRelativePath"] ?? "\\Icons\\InitIcon.png";
            _httpClientService = httpClientService;
            CardsCollection = new ObservableCollection<Card>();
            SubmitCommand = new AsyncRelayCommand(Submit);
            LoadDataCommand = new AsyncRelayCommand(LoadData);
            DeleteCardByIdCommand = new AsyncRelayCommand(DeleteById);
            DeleteAllCardsCommand = new AsyncRelayCommand(DeleteAllCards);
            LoadDataCommand.Execute(this);
        }
        private async Task LoadData()
        {
            try
            {
                var list = await _httpClientService.GetCardsAsync();
                foreach (var item in list)
                {
                    CardsCollection.Add(new Card(item.Id, item.Description, File.Exists(item.ImgPath) ? item.ImgPath : _initImgPath));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task Submit()
        {
            try
            {
                if (String.IsNullOrEmpty(SelectedImgPath) && String.IsNullOrEmpty(InputDescription))
                    return;
                var item = Card.CreateCard(InputDescription, SelectedImgPath);
                await _httpClientService.PostCardAsync(item);
                CardsCollection.Clear();
                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task DeleteById()
        {
            try
            {
                if (SelectedItem == null)
                { return; }
                await _httpClientService.DeleteCardByIdAsync(SelectedItem.Id);
                CardsCollection.Clear();
                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task DeleteAllCards()
        {
            await _httpClientService.DeleteAllCardsAsync();
            CardsCollection.Clear();
        }
    }
}