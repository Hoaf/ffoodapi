using Microsoft.AspNetCore.Mvc;
using DataService.Models.Services;
using System.Linq;
using DataService.Enum;
using DataService.Models.RequestModels.PostRequests;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using DataService.Models.RequestModels.PutRequests;

namespace BookingFood.WebAPI.Controllers
{
    [Authorize]
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public readonly NotificationService _service;
        public NotificationController(NotificationService service)
        {
            _service = service;
        }
        [Authorize(Roles = "2")]
        [HttpPost]
        public IActionResult CreateNotifcation(NotificationPostRequestModel model)
        {
            var currentAccountId = User.FindFirst(ClaimTypes.Name).Value;
            int accountId = Int32.Parse(currentAccountId);

            var result = _service.Create(model, accountId);
            if (result == null)
            {
                return BadRequest("Create Fail");
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
        [HttpGet]
        [Route("{accountId}")]
        public IActionResult Get(int accountId)
        {
            int checkId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            if (checkId != accountId)
            {
                return StatusCode(401);
            }
            
            else
            {
                var result = _service.GetNotificationByAccountId(accountId);
                if (result == null)
                {
                    return BadRequest("Account not exist");
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
        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllNotfication().ToList();
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

        [HttpGet]
        [Route("count/{accountId}")]
        public IActionResult GetCountNotification(int accountId)
        {
            var result = _service.GetCountNotification(accountId);
            if (result == 0)
            {
                return BadRequest("Account not exist");
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
        [Authorize(Roles = "2")]
        [HttpPatch]
        public IActionResult Update(NotificationPutRequestModel model)
        {
            var result = _service.UpdateStatusHasSeen(model);
            if (!result)
            {
                return BadRequest("Update Fail");
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
}