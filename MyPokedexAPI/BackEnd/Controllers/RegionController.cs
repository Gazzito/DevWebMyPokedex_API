using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace MyPokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _context.Regions.ToListAsync();
            return Ok(regions);
        }

        [HttpPost("CreateOrUpdateRegion")]
        public async Task<IActionResult> CreateOrUpdateRegion([FromBody] RegionDTO regionDto)
        {
            if (regionDto == null)
            {
                return BadRequest();
            }

            var region = new Region
            {
                Id = regionDto.Id,
                Name = regionDto.Name,
                CreatedOn = DateTime.SpecifyKind(regionDto.CreatedOn, DateTimeKind.Utc),
                CreatedById = regionDto.CreatedById,
                UpdatedOn = DateTime.SpecifyKind(regionDto.UpdatedOn, DateTimeKind.Utc),
                UpdatedById = regionDto.UpdatedById
            };

            if (region.Id == 0)
            {
                await _context.Regions.AddAsync(region);
            }
            else
            {
                var existingRegion = await _context.Regions.FindAsync(region.Id);
                if (existingRegion == null)
                {
                    return NotFound();
                }

                existingRegion.Name = region.Name;
                existingRegion.CreatedOn = region.CreatedOn;
                existingRegion.CreatedById = region.CreatedById;
                existingRegion.UpdatedOn = region.UpdatedOn;
                existingRegion.UpdatedById = region.UpdatedById;

                _context.Regions.Update(existingRegion);
            }

            await _context.SaveChangesAsync();
            return Ok(region);
        }

        [HttpDelete("DeleteRegion/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
