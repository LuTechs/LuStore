using System.Collections.Generic;
using System.Linq;
using LuStoreWebService.Models;

namespace LuStoreWebService.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly List<Product> _products;

        public ProductsRepository()
        {
            _products = new List<Product>();
        }

        public Product GetProduct(string id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProducts(string description, string model, string brand)
        {
            var products = _products.Where(p =>
                (string.IsNullOrEmpty(description) || p.Description.ToLower().Contains(description.ToLower()))
                && ( string.IsNullOrEmpty(model)||p.Model.ToLower().Contains(model.ToLower()) )
                && ( string.IsNullOrEmpty(brand)|| p.Brand.ToLower().Contains(brand.ToLower()))
            ).ToList();

            return products;
        }

        public Product AddProduct(Product product)
        {
            _products.Add(product);
            return product;
        }

        public bool DeleteProduct(string id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                return true;
            }
            return false;
        }

        public Product UpdateProduct(string id, Product product)
        {
            var productInMemory = _products.FirstOrDefault(p => p.Id == id);
            if (productInMemory == null) return null;
            productInMemory.Brand = product.Brand;
            productInMemory.Model = product.Model;
            productInMemory.Description = product.Description;
            return productInMemory;
        }
    }
}