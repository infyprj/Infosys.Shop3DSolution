using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
using Infosys.Shop3D.WebServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.Shop3D.WebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        public Shop3DRepository repository;

        public UserController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult RegisterUser([FromBody] User user)
        {
            User newUser = null;
            try
            {
                newUser = repository.RegisterUser(
                    user.Email,
                    user.PasswordHash,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.Address,
                    user.City,
                    user.State,
                    user.PostalCode,
                    user.Country,
                    user.RoleId
                );
            }
            catch (Exception )
            {
                newUser = null;
            }
            return Json(newUser != null );
        }


        [HttpGet]
        public JsonResult GetUserProfile(int userId)
        {
            User user = new User();
            try
            {
                user = repository.getUserProfile(userId);
            }
            catch (Exception)
            {
                user = null;
            }
            return Json(user);
        }


        [HttpPost]
        public JsonResult AuthenticateUser([FromBody] LoginModel model)
        {
            User user = null;
            LoginUser loginUser=new LoginUser();
            try
            {
                user = repository.AuthenticateUser(model.Email, model.Password);
                loginUser.UserId = user.UserId;
                loginUser.RoleId=user.RoleId;
            }
            catch (Exception)
            {
                loginUser = null;
            }
            
            if(loginUser == null)
            {
                
            return Json(new { User = loginUser, Success = false });
            }
            else
            {
                return Json(new { User = loginUser, Success = true });
            }
              
        }


        [HttpPut]
        public JsonResult UpdateUserDetails([FromBody] Models.Users user)
        {
            bool status = false;
            try
            {
                User updateUser=new User();
                updateUser.FirstName= user.FirstName;
                updateUser.LastName= user.LastName;
                updateUser.PhoneNumber= user.PhoneNumber;
                updateUser.Address= user.Address;   
                updateUser.City= user.City;
                updateUser.State= user.State;
                updateUser.PostalCode= user.PostalCode;
                updateUser.Country= user.Country;
                updateUser.UserId= user.UserId;

                status = repository.UpdateUserDetails(updateUser);
            }
            catch (Exception)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPut]
        public JsonResult ResetUserPassword([FromBody] ResetPasswordRequest request)
        {
            bool status = false;

            try
            {
                status = repository.ResetUserPassword(
                    request.Email,
                    request.OldPassword,        
                    request.NewPassword,
                    request.ConfirmPassword
                );
            }
            catch (Exception)
            {
                status = false;
            }

            return Json(status);
        }
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class ResetPasswordRequest
        {
            public string Email { get; set; }
            public string OldPassword { get; set; }        
            public string NewPassword { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}

