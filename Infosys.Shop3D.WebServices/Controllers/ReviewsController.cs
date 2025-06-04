using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        public Shop3DRepository repository;
        public ReviewsController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetReviews(int productId)
        {
            List<ReviewClass> reviews = new List<ReviewClass>();
            try
            {
                reviews = repository.GetReviewsByProduct(productId);
            }
            catch (Exception ex)
            {
                reviews = null;
            }
            return Json(reviews);
        }

        [HttpPost]
        public JsonResult AddReview([FromBody] ReviewModel model)
        {
            bool status = false;
            try
            {
                status = repository.AddReview(model.UserID, model.ProductID, model.Description);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPut]
        public JsonResult UpdateVisibility([FromBody] VisibilityModel model)
        {
            bool status = false;
            try
            {
                status = repository.UpdateReviewVisibility(model.ReviewID, model.UserID, model.IsVisible);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPut]
        public JsonResult UpdateDescription([FromBody] UpdateDescriptionModel model)
        {
            bool status = false;
            try
            {
                status = repository.UpdateReviewDescription(model.ReviewID, model.UserID, model.Description);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = false;
            }
            return Json(status);
        }


    }

    public class ReviewModel
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
    }

    public class VisibilityModel
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public bool IsVisible { get; set; }
    }

    public class UpdateDescriptionModel
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
    }


}

