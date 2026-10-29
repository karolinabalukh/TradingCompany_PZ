using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using shopBL;
using shopDTO;
using System.Collections.ObjectModel;

namespace shopViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IProductService _productService;

        [ObservableProperty]
        private string _welcomeMessage = "Каталог товарів";
        public ObservableCollection<ProductDTO> Products { get; } = new ObservableCollection<ProductDTO>();

        public MainViewModel(IProductService productService)
        {
            _productService = productService;
            LoadProducts();
        }

        [RelayCommand]
        private void LoadProducts()
        {
            Products.Clear();
            var productsFromDb = _productService.GetAllProducts();
            foreach (var product in productsFromDb)
            {
                Products.Add(product);
            }
        }


        public Action OnOpenLogin { get; set; } = null!;

        [RelayCommand]
        private void OpenLogin()
        {
            OnOpenLogin?.Invoke();
        }
    }
}