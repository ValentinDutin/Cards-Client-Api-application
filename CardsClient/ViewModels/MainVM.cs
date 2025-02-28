using System.Collections.ObjectModel;
using System.Windows.Input;
using CardsClient.Services;
using Common.Models;
using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Linq;
using System.Windows.Navigation;
using System.Configuration;

namespace CardsClient.ViewModels
{
    public class MainVM : BaseVM
    {
        private readonly string _initImgPath;
        private readonly ApiController _apiService;
        private string _description;
        private string _imgPath;
        private Card _selectedItem;
        private ICommand LoadDataCommand { get; }
        public ObservableCollection<Card> CardsCollection { get; private set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand DeleteCardByIdCommand { get; set; }
        public ICommand DeleteAllCardsCommand { get; set; }

        public Card SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        public string InputDescription
        { 
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string SelectedImgPath
        { 
            get { return _imgPath; }
            set
            {
                _imgPath = value;
                OnPropertyChanged();
            }
        }
        public MainVM() 
        {
            _initImgPath = ConfigurationManager.AppSettings["InitImagePath"].ToString() ?? "\\Icons\\InitIcon.png";
            _apiService = new ApiController();
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
                var list = await _apiService.GetCardsAsync();
                foreach (var item in list)
                {
                    CardsCollection.Add(new Card(item.Id, item.Description, File.Exists(item.ImgPath) ? item.ImgPath : _initImgPath));
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
                if (String.IsNullOrEmpty(SelectedImgPath) && String.IsNullOrEmpty(InputDescription))
                    return;
                var item = Card.CreateCard(InputDescription, SelectedImgPath);
                await _apiService.PostCardAsync(item);
                CardsCollection.Clear();
                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task DeleteById()
        {
            try
            {                
                if(SelectedItem == null)
                {  return; }
                await _apiService.DeleteCardByIdAsync(SelectedItem.Id);
                CardsCollection.Clear();
                await LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task DeleteAllCards()
        {
            await _apiService.DeleteAllCards();
            CardsCollection.Clear();
        }
    }
}