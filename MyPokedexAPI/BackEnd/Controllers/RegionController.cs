using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace MyPokedexAPI.Controllers
{
    [Authorize]
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

            Region region;
            if (regionDto.Id == 0)
            {
                region = new Region
                {
                    Name = regionDto.Name,
                    CreatedOn = DateTime.SpecifyKind(regionDto.CreatedOn, DateTimeKind.Utc),
                    CreatedBy = regionDto.CreatedBy,
                    UpdatedOn = regionDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(regionDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                    UpdatedBy = regionDto.UpdatedBy
                };

                await _context.Regions.AddAsync(region);
                await _context.SaveChangesAsync();

                regionDto.Id = region.Id;
            }
            else
            {
                region = await _context.Regions.FindAsync(regionDto.Id);
                if (region == null)
                {
                    return NotFound();
                }

                region.Name = regionDto.Name;
                region.CreatedOn = regionDto.CreatedOn;
                region.CreatedBy = regionDto.CreatedBy;
                region.UpdatedOn = regionDto.UpdatedOn;
                region.UpdatedBy = regionDto.UpdatedBy;

                _context.Regions.Update(region);
                await _context.SaveChangesAsync();
            }

            // Retorna o DTO atualizado com o ID da nova região criada
            regionDto.Id = region.Id;
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
