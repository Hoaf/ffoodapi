using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;
        public ProductController(ProductService service)
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
        public IActionResult Get([FromQuery]ProductGetFilter filter)
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
        public IActionResult Create(ProductPostRequestModel model)
        {
        
            var result = _service.Create(model);
            if (result == null)
            {
                return BadRequest("Can't create");
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
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {
            var product = _service.DeleteProduct(id);
            if (product == false)
            {
                return BadRequest("Account not existed !!");
            }
            else
            {
                return Ok("Delete Successfull");
            }
        }
        [HttpPatch]
        [Route("{id}")]
        [Authorize]
        public IActionResult UpdateStatusProduct(int id,int module)
        {
            var result = _service.UpdateStatusProduct(id,module);
            if (result == null)
            {
                return BadRequest("Product not exist !!!");
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