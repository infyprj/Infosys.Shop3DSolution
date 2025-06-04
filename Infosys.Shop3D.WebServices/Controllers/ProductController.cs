using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : Controller
    {
        public Shop3DRepository repository;

        public ProductController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = repository.GetAllProducts();
            }
            catch (Exception )
            {
                products = null;
            }
            return Json(products);
        }

        [HttpGet]
        public JsonResult GetProductById(int id)
        {
            Console.WriteLine("Get Product BY ID controller is called");
            Product product = null;
            try
            {
                product = repository.GetProductById(id);
                Console.WriteLine("Product :");
                Console.WriteLine(product);
            }
            catch (Exception ex)
            {
                product = null;
            }
            return Json(product);
        }

        [HttpGet]
        public JsonResult GetProductsByCategory(int categoryId)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = repository.GetProductsByCategory(categoryId);
            }
            catch (Exception ex)
            {
                products = null;
            }
            return Json(products);
        }

        [HttpGet]
        public JsonResult GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = repository.GetProductsByPriceRange(minPrice, maxPrice);
            }
            catch (Exception ex)
            {
                products = null;
            }
            return Json(products);
        }

        [HttpGet]
        public JsonResult GetProductsCount(int? categoryId = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            int count = 0;
            try
            {
                count = repository.GetProductsCount(categoryId, minPrice, maxPrice);
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return Json(count);
        }

        [HttpGet]
        public JsonResult SearchProducts(string searchTerm)
        {
            List<Product> products = new List<Product>();
            try
            {
                products = repository.SearchProducts(searchTerm);
            }
            catch (Exception ex)
            {
                products = null;
            }
            return Json(products);
        }

        [HttpPost]
        public JsonResult AddProduct([FromBody] Product product)
        {
            bool status;
            try
            {
                status = repository.AddProduct(product);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPut]
        public JsonResult UpdateProduct([FromBody] Product product)
        {
            bool status = false;
            try
            {
                status = repository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpDelete]
        public JsonResult DeleteProduct(int id)
        {
            bool status = false;
            try
            {
                status = repository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpGet]
        public JsonResult GetProductImages(int productId)
        {
            List<ProductImage> images = new List<ProductImage>();
            try
            {
                images = repository.GetProductImages(productId);
            }
            catch (Exception ex)
            {
                images = null;
            }
            return Json(images);
        }

        [HttpGet]
        public JsonResult GetProductStock(int productId)
        {
            int stock = 0;
            try
            {
                stock = repository.GetProductStock(productId);
            }
            catch (Exception)
            {
                stock = 0;
            }
            return Json(stock);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public JsonResult SetProductDiscount(int productId, decimal discountPercentage)
        {
            try
            {
                // Call DAL/repository method
                int finalPrice = repository.SetProductDiscount(productId, discountPercentage);

                if (finalPrice == -1)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "Failed to apply discount. Product not found or invalid input."
                    });
                }

                return Json(new
                {
                    Success = true,
                    Message = $"Discount of {discountPercentage}% applied successfully.",
                    FinalPrice = finalPrice
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Optional: log to a file or monitoring tool

                return Json(new
                {
                    Success = false,
                    Message = "An error occurred while applying the discount.",
                    Error = ex.Message
                });
            }
        }


    }
}
