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
            var regions = await _context.Regions
                .Select(r => new RegionDTO
                {
                    Id = r.Id,
                    Name = r.Name,
                    CreatedOn = r.CreatedOn,
                    CreatedBy = r.CreatedBy,
                    UpdatedOn = r.UpdatedOn,
                    UpdatedBy = r.UpdatedBy
                })
                .ToListAsync();

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
                CreatedBy = regionDto.CreatedBy,
                UpdatedOn = regionDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(regionDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                UpdatedBy = regionDto.UpdatedBy
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
                existingRegion.CreatedBy = region.CreatedBy;
                existingRegion.UpdatedOn = region.UpdatedOn;
                existingRegion.UpdatedBy = region.UpdatedBy;

                _context.Regions.Update(existingRegion);
            }

            await _context.SaveChangesAsync();
            return Ok(regionDto);
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
