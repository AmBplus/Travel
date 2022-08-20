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
    public class TourListController : ControllerBase
    {
        private readonly TravelDbContext _dbContext;

        public TourListController(TravelDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.TourLists);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_dbContext.TourLists.FirstOrDefault(x => x.Id == id));
        }
        /// <summary>
        /// Create TourLists
        /// </summary>
        /// <param name="TourList"></param>
        /// <returns>if Add 201 - If Null TourList Return BadRequest</returns>
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourList tourList)
        {
            if (tourList is null)
            {
                return BadRequest($"{nameof(tourList)} Is Null");
            }

            await _dbContext.TourLists.AddAsync(tourList);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(actionName: "Get", new { id = tourList.Id });
        }
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tourList = _dbContext.TourLists.FirstOrDefault(x => x.Id == id);
            if (tourList == null) return BadRequest("Null Nothing to delete");
            _dbContext.TourLists.Remove(tourList);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TourList tourList)
        {
            _dbContext.Update(tourList);
            await _dbContext.SaveChangesAsync();
            return Ok(tourList);
        }
    }
}