
using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Infosys.Shop3D.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RatingController : Controller
    {
        public Shop3DRepository repository;
        public RatingController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult AddRating([FromBody] Models.RatingRequest request)
        {
            bool result = false;
            try
            {
                result = repository.AddRating(request.UserId, request.ProductId, request.Rating);
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }


        [HttpGet]
        public JsonResult GetAvgRating(int productId)
        {
            try
            {
                var (averageRating, totalUsers) = repository.GetAvgRating(productId);

                var result = new
                {
                    ProductId = productId,
                    AverageRating = averageRating,
                    TotalUsers = totalUsers
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { ProductId = productId, AverageRating = 0.0, TotalUsers = 0 });
            }
        }

    }
}
