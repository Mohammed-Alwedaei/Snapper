using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Snapper.Client.Models;

namespace Snapper.Client.Services;

public class ProductsService : IProductsService
{
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public List<ProductModel> Products { get; private set; }

    public List<string> Categories { get; private set; }

    public ProductsService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,

            WriteIndented = true
        };
    }

    public async Task<List<ProductModel>> GetProductsAsync()
    {
        Products = new List<ProductModel>();

        var uri = new Uri("https://fakestoreapi.com/products");

        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Products = JsonSerializer.Deserialize<List<ProductModel>>(content, _serializerOptions);
            }
        }
        catch (Exception exception)
        {
            await Application.Current.MainPage.DisplayAlert("Error while getting products",
                "Unable to fetch the data due to logic error", "Ok");
        }

        return Products;
    }

    public async Task<List<string>> GetProductsCategoriesAsync()
    {
        Categories = new List<string>();

        var uri = new Uri("https://fakestoreapi.com/products/categories");

        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<string>>(content, _serializerOptions);
            }
        }
        catch (Exception exception)
        {
            await Application.Current.MainPage.DisplayAlert("Error while getting products",
                "Unable to fetch the data due to logic error", "Ok");
        }

        return Categories;
    }
}