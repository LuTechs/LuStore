using System.Collections.Generic;
using System.Web.Http.Results;
using LuStoreWebService.Controllers;
using LuStoreWebService.Models;
using LuStoreWebService.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuStoreWebService.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void GetShouldReturnAllProducts()
        {
            // Arrange
            /*should use Moq instead if the ProductRepository perform CRUD operation to Database. 
              However, ProductRepository is in-memory persistence so Moq is not important.
             */
            IProductsRepository productsRepository = new ProductsRepository();
            productsRepository.AddProduct(new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            });

            var controller = new ProductsController(productsRepository);

            // Act
            var actionResult = controller.Get(string.Empty, string.Empty, string.Empty);
            var response = actionResult as OkNegotiatedContentResult<List<Product>>;
            // Assert
            Assert.IsNotNull(response);
            var products = response.Content;
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void GetShouldReturnNotFoundWhenProductListEmpty()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();

            var controller = new ProductsController(productsRepository);

            // Act
            var actionResult = controller.Get(string.Empty, string.Empty, string.Empty);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void GetWithFiltersShouldReturnThatProducts()
        {
            // Arrange
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            var product2 = new Product
            {
                Id = "2",
                Brand = "Apple",
                Description = "Apple iPhone 7",
                Model = "Plus"
            };
            var product3 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple iPad",
                Model = "5"
            };
            var product4 = new Product
            {
                Id = "2",
                Brand = "Samsung",
                Description = "Galaxy Tab S2",
                Model = "Version 9.7"
            };
            IProductsRepository productsRepository = new ProductsRepository();
            productsRepository.AddProduct(product1);
            productsRepository.AddProduct(product2);
            productsRepository.AddProduct(product3);
            productsRepository.AddProduct(product4);

            var controller = new ProductsController(productsRepository);

            // Act
            var actionResult = controller.Get("iPad", "5", "Apple");
            var response = actionResult as OkNegotiatedContentResult<List<Product>>;
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Content.Count);
        }

        [TestMethod]
        public void GetWithIdShouldReturnThatProduct()
        {
            // Arrange
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            var product2 = new Product
            {
                Id = "2",
                Brand = "Samsung",
                Description = "Galaxy Tab S2",
                Model = "Version 9.7"
            };
            IProductsRepository productsRepository = new ProductsRepository();
            productsRepository.AddProduct(product1);
            productsRepository.AddProduct(product2);

            var controller = new ProductsController(productsRepository);

            // Act
            var actionResult = controller.Get("2");
            var response = actionResult as OkNegotiatedContentResult<Product>;
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("2", response.Content.Id);
        }

    
        [TestMethod]
        public void GetWithWrongIdShouldReturnNotFound()
        {
            // Arrange
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            var product2 = new Product
            {
                Id = "2",
                Brand = "Samsung",
                Description = "Galaxy Tab S2",
                Model = "Version 9.7"
            };
            IProductsRepository productsRepository = new ProductsRepository();
            productsRepository.AddProduct(product1);
            productsRepository.AddProduct(product2);

            var controller = new ProductsController(productsRepository);

            // Act
            var actionResult = controller.Get("3");
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }

        [TestMethod]
        public void PostShouldAddProduct()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };

            var controller = new ProductsController(productsRepository);
            // Act
            var actionResult = controller.Post(product1);
            var response = actionResult as CreatedAtRouteNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("DefaultApi", response.RouteName);
            Assert.AreEqual(response.Content.Id, response.RouteValues["Id"]);
        }

        [TestMethod]
        public void PutShouldUpdateProduct()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            var product2 = new Product
            {
                Id = "1",
                Brand = "Samsung",
                Description = "Galaxy Tab S2",
                Model = "Version 9.7"
            };

            productsRepository.AddProduct(product1);

            var controller = new ProductsController(productsRepository);
            // Act
            var actionResult = controller.Put("1", product2);
            var response = actionResult as OkNegotiatedContentResult<Product>;

            // Assert
            Assert.IsNotNull(response);
            var newProduct = response.Content;

            Assert.AreEqual("1", newProduct.Id);
            Assert.AreEqual(product2.Brand, newProduct.Brand);
            Assert.AreEqual(product2.Description, newProduct.Description);
            Assert.AreEqual(product2.Model, newProduct.Model);
        }

        [TestMethod]
        public void PutShouldReturnsNotFound()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            var product2 = new Product
            {
                Id = "1",
                Brand = "Samsung",
                Description = "Galaxy Tab S2",
                Model = "Version 9.7"
            };

            productsRepository.AddProduct(product1);

            var controller = new ProductsController(productsRepository);
            // Act
            var actionResult = controller.Put("3", product2);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            productsRepository.AddProduct(product1);

            var controller = new ProductsController(productsRepository);
            //Act
            var actionResult = controller.Delete("1");
            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange
            IProductsRepository productsRepository = new ProductsRepository();
            var product1 = new Product
            {
                Id = "1",
                Brand = "Apple",
                Description = "Apple Watch 2",
                Model = "Series 2"
            };
            productsRepository.AddProduct(product1);

            var controller = new ProductsController(productsRepository);
            //Act
            var actionResult = controller.Delete("2");
            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}