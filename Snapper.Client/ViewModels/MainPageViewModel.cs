using System.Collections.ObjectModel;
using Snapper.Client.Models;
using Snapper.Client.Services;
using CommunityToolkit.Mvvm.Input;

namespace Snapper.Client.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    private readonly IProductsService _productsService;

    public ObservableCollection<ProductModel> Products { get; } = new();

    public ObservableCollection<CategoryModel> Categories { get; } = new();

    public MainPageViewModel(IProductsService productsService)
    {
        Title = "Home";
        _productsService = productsService;
    }

    [RelayCommand]
    async Task GetProductsAsync()
    {
        var products = new List<ProductModel>();

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            products = await _productsService.GetProductsAsync();

            if (Products.Count != 0)
                Products.Clear();

            foreach (var product in products)
                Products.Add(product);
        }
        catch (Exception exception)
        {
            await Shell.Current.DisplayAlert("Could not get data", "Please try again we are unable to get your data",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GetCategoriesAsync()
    {
        var categories = new List<CategoryModel>()
        {
            new ()
            {
                Id = 1,
                Category = "Men"
            },
            new ()
            {
                Id = 2,
                Category = "Women"
            },
            new ()
            {
                Id = 3,
                Category = "Kids"
            },
            new ()
            {
                Id = 4,
                Category = "Elderly"
            },
        };

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            if (Categories.Count != 0)
                Categories.Clear();

            foreach (var category in categories)
                Categories.Add(category);
        }
        catch (Exception exception)
        {
            await Shell.Current.DisplayAlert("Could not get data", "Please try again we are unable to get your data",
                "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}