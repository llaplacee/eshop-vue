using System;
using System.Linq;
using Core.Contracts;
using DataLayer.Entities;
using EshopApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EshopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet("AddProductToOrder")]
        public IActionResult AddOrder(int productId, int? count)
        {
            if (!User.Identity.IsAuthenticated) return Ok(new { status = "NotAuth" });

            if (!_productService.IsExistProductById(productId)) return Ok(new { status = "NotFoundProduct" });

            if (count != null && count <= 0) return Ok(new { status = "InvalidCount" });

            _orderService.AddProductToUserOrder(User.GetUserId(), productId, count);

            return Ok(new { status = "Done" });
        }

        [HttpGet("GetShopCart")]
        public IActionResult GetUserOpenOrder()
        {
            if (!User.Identity.IsAuthenticated) return Ok(new { status = "NotAuth" });

            if (!_orderService.IsExistUserOpenOrder(User.GetUserId())) return Ok(new { status = "NotExist" });

            return Ok(new { status = "success", result = _orderService.GetUserOpenOrder(User.GetUserId()) });
        }

        [HttpGet("DeleteDetail")]
        public IActionResult DeleteOrderDetail(int detailId)
        {
            if (!User.Identity.IsAuthenticated) return Ok(new { status = "NotAuth" });

            if (!_orderService.IsExistOrderDetailById(detailId)) return Ok(new { status = "NotExist" });

            if (!_orderService.IsDetailForUser(detailId, User.GetUserId())) return Ok(new { status = "NotForUser" });

            _orderService.DeleteOrderDetail(detailId);

            return Ok(new { status = "success", result = _orderService.GetUserOpenOrder(User.GetUserId()) });
        }

        [HttpGet("CompleteShop")]
        public IActionResult CompleteShop()
        {
            if (!User.Identity.IsAuthenticated) return Ok(new { status = "NotAuth" });

            if (!_orderService.IsExistUserOpenOrder(User.GetUserId())) return Ok(new { status = "NotExist" });

            var openOrder = _orderService.GetUserOpenOrder(User.GetUserId());

            if (openOrder.OrderDetails == null || !openOrder.OrderDetails.Any()) return Ok(new { status = "EmptyShopCart" });
            
            openOrder.IsFinaly = true;
            openOrder.PaidOn = DateTime.Now;
            _orderService.EditOrder(openOrder);
            
            return Ok(new { status = "success" });
        }
    }
}