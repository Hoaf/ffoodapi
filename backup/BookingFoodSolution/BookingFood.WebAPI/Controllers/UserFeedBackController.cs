using System;
using System.Collections.Generic;
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
    [Route("api/UserFeedBack")]
    [ApiController]
    public class UserFeedBackController : ControllerBase
    {
        private readonly UserFeedBackService _service;
        public UserFeedBackController(UserFeedBackService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if(result == null)
            {
                return BadRequest("No data");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }

        [HttpGet]
        public IActionResult GetByOrderOrStore([FromQuery]UserFeedBackFilterModel userFeedBackFilter)
        {
            var result = _service.GetByOrderOrStore(userFeedBackFilter);
            if (result.Count == 0)
            {
                return BadRequest("No data");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }

        [HttpPost]
        public IActionResult Create(UserFeedBackPostRequest userFeedBack)
        {
            var result = _service.Create(userFeedBack);
            if (result == null)
            {
                return BadRequest("Bad req");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }


        [HttpDelete]
        [Route("{orderid}")]
        public IActionResult Delete(int orderid)
        {
            var result = _service.Delete(orderid);
            if (result == null)
            {
                return BadRequest("Bad req");
            }
            return new JsonResult(new
            {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }
    }
}