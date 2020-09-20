using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataService.Enum;
using DataService.Models;
using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingFood.WebAPI.Controllers
{
    [Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("{orderid}")]
        public IActionResult GetById(int orderid)
        {
            var order = _service.GetOrderById(orderid);
            if (order == null)
            {
                return BadRequest("No record");
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = order
            });
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery]OrderFilterModel orderFilterModel)
        {
            var orderList = _service.GetAll(orderFilterModel);
            if (orderList.Count == 0)
            {
                return BadRequest("No record");
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = orderList
            });
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderPostRequestModel order)
        {

            if (order == null || order.AccountId ==0)
            {
                return BadRequest("Bad request");
            }
            if (_service.IsValidOrderUnpaid(order.AccountId))
            {
                var orderCreate = _service.Create(order);
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = orderCreate
                });
            }

            return new JsonResult(new
            {
                message = StatusMethod.FAILED,
                data = "Order haven't paid already existed"
            });
        }

        [HttpDelete]
        [Route("{orderid}")]
        public IActionResult DeleteOrder(int orderid)
        {
            bool result = _service.DeleteOrderById(orderid);
            if (result)
            {
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = "id : "+ orderid+ " deleted"
                });
            }

            return BadRequest("can't delete "+ orderid);
        }

        [HttpGet]
        [Route("sellerhistory/{storeid}")]
        public IActionResult GetSellerHistory(int? storeid)
        {
            var result = _service.GetSellerHistory(storeid);
            if (result.Count == 0)
            {
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = "no item sell"
                });
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }
    }
}