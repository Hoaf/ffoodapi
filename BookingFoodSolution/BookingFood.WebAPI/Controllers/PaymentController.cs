using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Enum;
using DataService.Models.RequestModels;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingFood.WebAPI.Controllers
{
    [Authorize]
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentController(PaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetByIdOrTransactionId([FromQuery]PaymentFilterModel filter)
        {
            var payment = _service.GetByIdOrTransactionId(filter);
            if (payment == null)
            {
                return BadRequest("No record");
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = payment
            });
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var payment = _service.GetAll();
            if (payment == null)
            {
                return BadRequest("No record");
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = payment
            });
        }

        [HttpDelete]
        [Route("{paymentid}")]
        public IActionResult Delete(int paymentid)
        {
            bool check = _service.Delete(paymentid);
            if (!check)
            {
                return BadRequest("No payment was found");
            }

            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = "deleted"
            });
        }
    }
}