using System.Collections.ObjectModel;
using CommonFiles.Models;
using CommonFiles.Services;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Diagnostics;
using CardsClient.Services;

namespace CardsClient.ViewModels
{
    public class MainVM : BaseVM
    {
        private readonly string _initImgPath;
        private readonly IHttpClientService _httpClientService;
        private string? _description;
        private string? _imgPath;
        private Card? _selectedItem;
        private string _statusBar;
        private long _ping;
        private Stopwatch _pingTimer;
        private bool _pingCanChanged;
        private IAsyncRelayCommand LoadDataCommand { get; }
        public ObservableCollection<Card> CardsCollection { get; private set; }
        public IAsyncRelayCommand SubmitCommand { get; set; }
        public IAsyncRelayCommand DeleteCardByIdCommand { get; set; }
        public IAsyncRelayCommand DeleteAllCardsCommand { get; set; }
        public string StatusBar
        {
            get => _statusBar;
            set
            {
                _statusBar = value;
                OnPropertyChanged();
            }
        }
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
        public long Ping
        {
            get => _ping;
            set
            {
                _ping = value;
                OnPropertyChanged();
            }
        }
        public MainVM(IHttpClientService httpClientService, IConfigDataService configDataService)
        {
            try
            {
                _initImgPath = configDataService.GetData("InitIconRelativePath");
                _httpClientService = httpClientService;
                _statusBar = "App running successfully";
                _ping = 0;
                _pingTimer = new();
                _pingCanChanged = true;
                CardsCollection = [];
                SubmitCommand = new AsyncRelayCommand(Submit);
                LoadDataCommand = new AsyncRelayCommand(LoadData);
                DeleteCardByIdCommand = new AsyncRelayCommand(DeleteById);
                DeleteAllCardsCommand = new AsyncRelayCommand(DeleteAllCards);
                LoadDataCommand.Execute(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                PostExceptionInView(ex);
            }
        }
        private async Task LoadData()
        {
            try
            {
                if (_pingCanChanged)
                {
                    _pingTimer.Restart();
                }
                    var list = await _httpClientService.GetCardsAsync();
                if (_pingCanChanged)
                {
                    _pingTimer.Stop();
                    Ping = _pingTimer.ElapsedMilliseconds;
                }
                else
                    _pingCanChanged = true;
                foreach (var item in list)
                {
                    CardsCollection.Add(new Card(item.Id, item.Description, File.Exists(item.ImgPath) ? item.ImgPath : _initImgPath));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                PostExceptionInView(ex);
            }
        }
        private async Task Submit()
        {
            try
            {
                if (String.IsNullOrEmpty(SelectedImgPath) && String.IsNullOrEmpty(InputDescription))
                    return;
                var item = Card.CreateCard(InputDescription, SelectedImgPath);
                StatusBar = "Added card id : " + item.Id;
                _pingTimer.Restart();
                await _httpClientService.PostCardAsync(item);
                _pingTimer.Stop();
                Ping = _pingTimer.ElapsedMilliseconds;
                CardsCollection.Clear();
                _pingCanChanged = false;
                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                PostExceptionInView(ex);
            }
        }
        private async Task DeleteById()
        {
            try
            {
                if (SelectedItem == null)
                { return; }
                _pingTimer.Restart();
                await _httpClientService.DeleteCardByIdAsync(SelectedItem.Id);
                _pingTimer.Stop();
                Ping = _pingTimer.ElapsedMilliseconds;
                CardsCollection.Clear();
                _pingCanChanged = false;
                await LoadData();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                PostExceptionInView(ex);
            }
        }
        private async Task DeleteAllCards()
        {
            try
            {
                _pingTimer.Restart();
                await _httpClientService.DeleteAllCardsAsync();
                _pingTimer.Stop();
                Ping = _pingTimer.ElapsedMilliseconds;
                CardsCollection.Clear();
            }
            catch (Exception ex)
            {
                PostExceptionInView(ex);
            }
        }
        private void PostExceptionInView(Exception ex)
        {
            StatusBar = ex.Message;
        }
    }
}