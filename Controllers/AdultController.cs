using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApplication.Data;

namespace LoginExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultController : ControllerBase
    {
        private IFamilyData _familyData;
        public AdultController(IFamilyData _familyData)
        {
            this._familyData = _familyData;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Adult>>>
            GetAdults([FromQuery] int? Id, [FromQuery] string? firstname)
        {
            try
            {
                IList<Adult> adults = await _familyData.GetAdultsAsync();
                if (Id != null)
                {
                    adults = adults.Where(adult => adult.Id == Id).ToList();
                }

                if (firstname != null)
                {
                    adults = adults.Where(adult => adult.FirstName == firstname).ToList();
                }

                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await _familyData.RemoveAdultAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Adult added = await _familyData.AddAdultAsync(adult);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await _familyData.UpdateAsync(adult);
                return Ok(updatedAdult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}