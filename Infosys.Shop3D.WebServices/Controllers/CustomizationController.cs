
using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomizationController : Controller
    {
        public Shop3DRepository repository;

        public CustomizationController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetAllCustomizations()
        {
            List<Customization> customizations = new List<Customization>();
            try
            {
                customizations = repository.GetAllCustomizations();
            }
            catch(Exception)
            {
                customizations = null;
            }
            return Json(customizations);

        }

        [HttpGet]
        public JsonResult GetCustomizationbyUserId(int userId)
        {
            Console.WriteLine("Get customizations by Id controller is called");
            Customization customization = null;
            try
            {
                customization = repository.GetCustomizationbyUserId(userId);
                Console.WriteLine("Customization");
                Console.WriteLine(customization);
            }
            catch (Exception)
            {
                customization = null;
            }
            return Json(customization);
        }

        [HttpGet]
        public JsonResult GetCustomizationbyCustomId(int customId)
        {
            Console.WriteLine("Get customizations by CustomId controller is called");
            Customization customization = null;
            try
            {
                customization = repository.GetCustomizationbyCustomId(customId);
                Console.WriteLine("Customization");
                Console.WriteLine(customization);
            }
            catch (Exception)
            {
                customization = null;
            }
            return Json(customization);
        }
        [HttpPost]
        public JsonResult AddCustomization([FromBody] Customization customization)
        {
            bool status=false;
            try
            {
                status = repository.AddCustomization(customization);

            }
            catch
            {
                status = false;
            }
            return Json(status);
        }

    }
}
