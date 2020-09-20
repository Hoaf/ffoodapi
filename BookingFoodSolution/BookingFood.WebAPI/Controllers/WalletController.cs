
using DataService.Enum;
using DataService.Models.RequestModels;
using DataService.Models.RequestModels.PutRequests;
using DataService.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BookingFood.WebAPI.Controllers
{
    [Authorize]
    [Route("api/wallet")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _service;
        public WalletController(WalletService service)
        {
            _service = service;
        }

        // Get by ID.
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


        [HttpGet]
        public IActionResult Get([FromQuery] WalletFilterModel walletFilterModel)
        {
            var result = _service.Get(walletFilterModel).ToList();
            if (result.Count == 0)
            {
                return BadRequest("Empty Record !!");
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


        // Create wallet.
        //[HttpPost]
        //public IActionResult CreateWallet(int AccountId)
        //{
        //    var result = _service.CreateWallet(AccountId);
        //    if (result == null)
        //    {
        //        return BadRequest("Wallet is existed !!!");
        //    }
        //    else
        //    {
        //        return new JsonResult(new
        //        {
        //            message = StatusMethod.SUCCESS,
        //            data = result
        //        });
        //    }
        //}

        // Delete wallet.
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteWallet(int id)
        {
            var wallet = _service.DeleteWallet(id);
            if (wallet == false)
            {
                return BadRequest("Wallet not existed !!!");
            }
            else
            {
                return Ok("Delete Successfull");
            }
        }
        // Update wallet.
        [Authorize(Roles = "2")] 
        [HttpPatch]
        public IActionResult UpdateWallet(WalletPutRequestModel model)
        {
          
                var wallet = _service.Update(model);
                if (wallet == null)
                {
                    return BadRequest("Wallet not existed !!!");
                }
                else
                {
                    return new JsonResult(new
                    {
                        message = StatusMethod.SUCCESS,
                        data = wallet

                    });
                }
            
        }
    }
}