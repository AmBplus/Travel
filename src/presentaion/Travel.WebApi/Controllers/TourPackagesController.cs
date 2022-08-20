using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Travel.Data.Contexts;
using Travel.Domain.Entities;

namespace Travel.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TourPackagesController : ControllerBase
    {
        private readonly TravelDbContext _dbContext;

        public TourPackagesController(TravelDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.TourPackages);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.TourPackages.FirstOrDefault(x => x.Id == id));
        }
        /// <summary>
        /// Create TourPackages
        /// </summary>
        /// <param name="tourPackage"></param>
        /// <returns>if Add 201 - If Null TourPackage Return BadRequest</returns>
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourPackage tourPackage)
        {
            if (tourPackage is null)
            {
                return BadRequest($"{nameof(tourPackage)} Is Null");
            }

            await _dbContext.TourPackages.AddAsync(tourPackage);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(actionName: "Get", new { id = tourPackage.Id });
        }
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tourPackage = _dbContext.TourPackages.FirstOrDefault(x => x.Id == id);
            if (tourPackage == null) return BadRequest("Null Nothing to delete");
            _dbContext.TourPackages.Remove(tourPackage);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TourPackage tourPackage)
        {
            _dbContext.Update(tourPackage);
            await _dbContext.SaveChangesAsync();
            return Ok(tourPackage);
        }
    }
}