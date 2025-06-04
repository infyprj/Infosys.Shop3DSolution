//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Infosys.Shop3D.WebServices.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//    }
//}

//using Infosys.Shop3D.DataAccessLayer.Models;
//using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
//using Infosys.Shop3D.DataAccessLayer;
using Infosys.Shop3D.DataAccessLayer.Models;
//using YourNamespace.Models;
//using YourNamespace.Repository;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : Controller
    {
        public Shop3DRepository repository;
        public CartController(Shop3DRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public JsonResult CreateCart(int userId)
        {
            bool status = false;
            try
            {
                int result = repository.CreateCart(userId);
                if (result > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpGet]
        public JsonResult GetCart(int userId)
        {
            Cart cart = null;
            try
            {
                cart = repository.GetCartByUserId(userId);
            
            }
            catch (Exception ex)
            {
                cart = null;
            }
            return Json(cart);
        }

        [HttpPost]
        public JsonResult AddToCart([FromBody] CartAddRequest request)
        {
            bool status = false;
            try
            {
                int result = repository.AddToCart(request.UserId, request.ProductId, request.Quantity);
                if (result > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPut]
        public JsonResult UpdateCartItemQuantity([FromBody] CartUpdateRequest request)
        {
            bool status = false;
            try
            {
                int result = repository.UpdateCartItemQuantity(request.CartItemId, request.Quantity);
                if (result > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpDelete ]
        public JsonResult RemoveFromCart(int cartItemId)
        {
            bool status = false;
            try
            {
                int result = repository.RemoveFromCart(cartItemId);
                if (result > 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpDelete]
        public JsonResult ClearCart(int cartId)
        {
            bool status = false;
            try
            {
                int result = repository.ClearCart(cartId);
                if (result >= 0)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpGet]
        public JsonResult GetCartItems(int userId)
        {
            List<CartItemDetail> cartItems=new List<CartItemDetail>();
            try
            {
                cartItems = repository.GetCartItems(userId);
                
            }
            catch (Exception ex)
            {
                cartItems = null;
            }
            return Json(cartItems);
        }
    }

    // Request models
    public class CartAddRequest
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class CartUpdateRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
