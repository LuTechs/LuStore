using System.Web.Http;
using LuStoreWebService.Models;
using LuStoreWebService.Repositories;

namespace LuStoreWebService.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: api/Products
        public IHttpActionResult Get(string description = "", string model = "", string brand = "")
        {
            var products = _productsRepository
                .GetProducts(description, model, brand);

            if (products.Count > 0)
                return Ok(products);
            return NotFound();
        }

        // GET: api/Products/1
        public IHttpActionResult Get(string id)
        {
            var product = _productsRepository.GetProduct(id);
            if (product != null)
                return Ok(product);
            return NotFound();
        }

        // POST: api/Products
        public IHttpActionResult Post(Product product)
        {
            var addedProduct = _productsRepository.AddProduct(product);
            return CreatedAtRoute("DefaultApi", new {product.Id}, addedProduct);
        }

        // PUT: api/Products/1
        public IHttpActionResult Put(string id, Product product)
        {
            var updateProduct = _productsRepository.UpdateProduct(id, product);
            if (updateProduct == null) return NotFound();
            return Ok(_productsRepository.UpdateProduct(id, product));
        }

        // DELETE: api/Products/1
        public IHttpActionResult Delete(string id)
        {
            if (_productsRepository.DeleteProduct(id))
                return Ok();
            return NotFound();
        }
    }
}