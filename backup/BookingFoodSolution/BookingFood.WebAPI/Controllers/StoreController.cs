using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataService.Enum;
using DataService.Models.RequestModels.Filter;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingFood.WebAPI.Controllers
{
    //[Authorize]
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _service;
        public StoreController(StoreService service)
        {
            _service = service;
        }
        // get by id
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _service.Get(id);
            if (result == null)
            {
                return BadRequest("ID not exist");
            }
            else
            {
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = result

                });
            }

        }
        // get all
        [HttpGet]
        public IActionResult Get([FromQuery]StoreFilter filter)
        {
            var result = _service.Get(filter).ToList();
            if (result.Count == 0)
            {
                return BadRequest("Data is empty");
            }
            else
            {

                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = result

                });
            }

        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(StorePostRequestModel model)
        {
            int accountId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            if (accountId != model.AccountId)
            {
                return StatusCode(401);
            }
            else
            {
                var result = _service.Create(model);
                if (result == null)
                {
                    return BadRequest("Can't create store => store is existed");
                }
                else
                {
                    return new JsonResult(new
                    {
                        message = StatusMethod.SUCCESS,
                        data = result
                    });
                }
            }            
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult DeleteStore(int id)
        {
            var account = _service.DeleteStore(id);
            if (account == false)
            {
                return BadRequest("Account not existed !!");
            }
            else
            {
                return Ok("Delete Successfull");
            }
        }
        [HttpPatch]
    
        [Route("confirm_admin/{id}")]
        [Authorize(Roles = "2")]
        public IActionResult ConfirmAdmin(int id,int module)
        {
            var account = _service.ComfirmAdmin(id, module);
            if (account == false)
            {
                return BadRequest("Can't confirm");
            }
            else
            {
                return Ok("Confirm successfull");
            }
        }
    }
}
