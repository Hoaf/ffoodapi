using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Enum;
using DataService.Models;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingFood.WebAPI.Controllers
{
    [Authorize]
    [Route("api/orderdetail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailService _service;
        private readonly ProductService _proService;

        public OrderDetailController(OrderDetailService service, ProductService productService)
        {
            _service = service;
            _proService = productService;
        }
        [HttpGet]
        [Route("{orderId}")]
        public IActionResult GetOrderDetailsByOrderId(int orderId)
        {
            var result = _service.GetOrderDetailsByOrderId(orderId);
            if (result == null)
            {
                return BadRequest("No record");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }
        [HttpGet]
        [Route("productcart/{accountid}")]
        public IActionResult GetproductcartbyAccountId(int accountid)
        {
            var result = _service.GetProducstInCart(accountid);
            if(result == null)
            {
                return BadRequest("No record");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }
        [HttpPost]
        public IActionResult CreateOrUpdate(OrderDetailRequestModel orderDetail)
        {
            OrderDetail result = null;
            bool check = CheckValidProduct(orderDetail.ProductId,orderDetail.OrderId);
            if (check)
            {
                result = _service.CreateOrUpdateOrderDetail(orderDetail);
               
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = result
                });
            }

            
            return new JsonResult(new
            {
                message = StatusMethod.FAILED,
                data = "different store is invalid or wrond data"
            });
        }

        private bool CheckValidProduct(int newProduct, int orderid)
        {
            List<OrderDetail> currentStore = _service.GetOrderDetailsByOrderId(orderid);

            if (currentStore == null)
            {
                return true;
            }
            else
            {
                int? newStore = _proService.getProductById(newProduct).StoreId;
                int? oldStore = _proService.getProductById(currentStore.ElementAt(0).ProductId).StoreId;
                if(newStore != oldStore)
                {
                    return false;
                }
            }
            return true;
        }

        [HttpDelete]
        public IActionResult Delete(int orderId, int productId)
        {
            var result = _service.DeleteOrderDetail(orderId, productId);
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }
    }
}