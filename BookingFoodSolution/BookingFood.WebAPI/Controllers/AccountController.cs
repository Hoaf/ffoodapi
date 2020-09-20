using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataService.Enum;
using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PostRequests;
using DataService.Models.RequestModels.PutRequests;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingFood.WebAPI.Controllers
{  
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginRequestModel model)
        {
            

            var user = _service.CheckLogin(model);
            if (user == null)
            {

                return BadRequest("Username or Password not correct");

            }
            else
            {
                return new JsonResult(new
                {
                    message = StatusMethod.SUCCESS,
                    data = user

                });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateAccount(AccountPostRequestModel model)
        {
            var result = _service.CreateAccount(model);
            if (result == null)
            {
                return BadRequest("Username is existed ! ");
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
        // get by id
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            int accountId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            if(accountId != id)
            {
                return StatusCode(401);
            }
            else
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
            

        }
        // get all
        [HttpGet]
        [Authorize(Roles = "2")]
        public IActionResult Get()
        {
            int accountId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            var result = _service.GetAllAccount(accountId).ToList();
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
        [HttpPatch]
        [Route("change_password")]
        public IActionResult ChangePassword(ChangePasswordRequestModel model)
        {
            int accountId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            if (accountId != model.Id)
            {
                return StatusCode(401);
            }
            else
            {
                var result = _service.ChangePassword(model);
                if (result == null)
                {
                    return BadRequest("Can't change passsword");
                }
                else
                {
                    return Ok("Change password successfull");
                }
            }

            
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "2")]
        public IActionResult DeleteAccount(int id)
        {

            var account = _service.DeleteAccount(id);
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
        [Route("unblock_account/{id}")]
        [Authorize(Roles = "2")]
        public IActionResult UnblockAccount(int id)
        {

            var account = _service.UnBlockAccount(id);
            if (account == false)
            {
                return BadRequest("Account not existed !!");
            }
            else
            {
                return Ok("Delete Successfull");
            }
        }
        [HttpPut]
        public IActionResult UpdateAccount(AccountPutRequestModel model)
        {
            int accountId = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? null);
            if (accountId != model.Id)
            {
                return StatusCode(401);
            }
            else
            {
                var account = _service.Update(model);
                if (account == null)
                {
                    return BadRequest("Account not existed !!");
                }
                else
                {
                    return new JsonResult(new
                    {
                        message = StatusMethod.SUCCESS,
                        data = account

                    });
                }
            }

            
        }
    }
}