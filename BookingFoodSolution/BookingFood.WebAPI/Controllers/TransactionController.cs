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
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;

        public TransactionController(TransactionService transactionService)
        {
            _service = transactionService;
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult checkout(TransactionPostRequest transaction)
        {
            var result = _service.CreateTransaction(transaction);
            if(result == null)
            {
                return BadRequest("can't perform payment");
            }
            return new JsonResult(new {
                message = StatusMethod.SUCCESS,
                data = result
            });
        }

        [HttpGet]
        [Route("{walletid}")]
        public IActionResult GetByWallet(int walletid)
        {
            var result = _service.GetByWalletId(walletid);
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