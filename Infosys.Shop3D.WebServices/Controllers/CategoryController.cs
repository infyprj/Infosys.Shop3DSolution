using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController:Controller
    {
        public Shop3DRepository repository;
        public CategoryController(Shop3DRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public JsonResult GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = repository.GetAllCategories();
            }
            catch (Exception ex)
            {
                categories = null;
            }
            return Json(categories);
        }
    }
}
