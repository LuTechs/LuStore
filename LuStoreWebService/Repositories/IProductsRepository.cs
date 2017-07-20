using System.Collections.Generic;
using LuStoreWebService.Models;

namespace LuStoreWebService.Repositories
{
    public interface IProductsRepository
    {
        Product GetProduct(string id);
        List<Product> GetProducts(string description,string model,string brand);
        Product AddProduct(Product product);
        bool DeleteProduct(string id);
        Product UpdateProduct(string id, Product product);
    }
}