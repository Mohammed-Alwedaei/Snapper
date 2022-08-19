using Snapper.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snapper.Client.Services;

public interface IProductsService
{
    Task<List<ProductModel>> GetProductsAsync();

    Task<List<string>> GetProductsCategoriesAsync();
}