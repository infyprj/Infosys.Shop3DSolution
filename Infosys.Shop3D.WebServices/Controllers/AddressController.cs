using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Infosys.Shop3D.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : Controller
    {
        public Shop3DRepository repository;
        public AddressController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public JsonResult GetAddressesByUserId(int userId)
        {
            List<Address> addresses = new List<Address>();
           
            try
            {
                addresses = repository.GetAddressesByUserId(userId);
                
            }
            catch (Exception)
            {
                addresses = null;
            }
            return Json(addresses);
        }

        [HttpPost]
        public JsonResult AddNewAddress([FromBody] Address address)
        {
            bool result = false;
            try
            {
                result = repository.AddNewAddress(address);
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }

        [HttpPut]
        public JsonResult UpdateAddress([FromBody] Models.Address_ address_)
        {
            bool result = false;
            Address address=new Address();
            address.AddressId = address_.AddressId;
            address.Name = address_.Name;
            address.PhoneNumber = address_.PhoneNumber;
            address.AddressLine= address_.AddressLine;
            address.City= address_.City;
            address.State= address_.State;
            address.PostalCode= address_.PostalCode;
            address.Country= address_.Country;
            try
            {
                result = repository.UpdateAddress(address);
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }

        [HttpDelete]
        public JsonResult RemoveAddress(int addressId)
        {
            bool result = false;
            try
            {
                result = repository.RemoveAddress(addressId);
            }
            catch (Exception)
            {
                result = false;
            }
            return Json(result);
        }


    }
}

