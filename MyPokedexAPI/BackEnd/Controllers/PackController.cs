using Microsoft.AspNetCore.Mvc;
using MyPokedexAPI.Data;
using MyPokedexAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyPokedexAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetMostPopularPacks")]
        public async Task<IActionResult> GetMostPopularPacks()
        {
            var mostPopularPacks = await _context.Packs
                .OrderByDescending(p => p.TotalBought)
                .Take(5)
                .Select(p => new PackDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BronzeChance = p.BronzeChance,
                    SilverChance = p.SilverChance,
                    GoldChance = p.GoldChance,
                    PlatinumChance = p.PlatinumChance,
                    DiamondChance = p.DiamondChance,
                    TotalBought = p.TotalBought,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy,
                    UpdatedOn = p.UpdatedOn,
                    UpdatedBy = p.UpdatedBy,
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null
                })
                .ToListAsync();

            return Ok(mostPopularPacks);
        }

        [HttpGet("GetPacksWithPaginationAndSearch")]
        public async Task<IActionResult> GetPacksWithPaginationAndSearch(int page, int maxRecords, string searchKeyword)
        {
            var query = _context.Packs.AsQueryable();

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                searchKeyword = searchKeyword.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchKeyword));
            }

            var totalPacks = await query.CountAsync();
            var packs = await query
                .Skip((page - 1) * maxRecords)
                .Take(maxRecords)
                .Select(p => new PackDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    BronzeChance = p.BronzeChance,
                    SilverChance = p.SilverChance,
                    GoldChance = p.GoldChance,
                    PlatinumChance = p.PlatinumChance,
                    DiamondChance = p.DiamondChance,
                    TotalBought = p.TotalBought,
                    CreatedOn = p.CreatedOn,
                    CreatedBy = p.CreatedBy,
                    UpdatedOn = p.UpdatedOn,
                    UpdatedBy = p.UpdatedBy,
                    Image = p.Image != null ? Convert.ToBase64String(p.Image) : null
                })
                .ToListAsync();

            return Ok(new { totalPacks, packs });
        }

        [HttpPost("CreateOrUpdatePack")]
        public async Task<IActionResult> CreateOrUpdatePack([FromBody] PackDTO packDto)
        {
            if (packDto == null)
            {
                return BadRequest();
            }

            Pack pack;
            if (packDto.Id == 0)
            {
                pack = new Pack
                {
                    Name = packDto.Name,
                    Price = packDto.Price,
                    Image = !string.IsNullOrEmpty(packDto.Image) ? Convert.FromBase64String(packDto.Image) : null,
                    BronzeChance = packDto.BronzeChance,
                    SilverChance = packDto.SilverChance,
                    GoldChance = packDto.GoldChance,
                    PlatinumChance = packDto.PlatinumChance,
                    DiamondChance = packDto.DiamondChance,
                    TotalBought = packDto.TotalBought,
                    CreatedOn = DateTime.SpecifyKind(packDto.CreatedOn, DateTimeKind.Utc),
                    CreatedBy = packDto.CreatedBy,
                    UpdatedOn = packDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(packDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null,
                    UpdatedBy = packDto.UpdatedBy
                };

                await _context.Packs.AddAsync(pack);
                await _context.SaveChangesAsync();

                packDto.Id = pack.Id;
            }
            else
            {
                pack = await _context.Packs.FindAsync(packDto.Id);
                if (pack == null)
                {
                    return NotFound();
                }

                pack.Name = packDto.Name;
                pack.Price = packDto.Price;
                pack.Image = !string.IsNullOrEmpty(packDto.Image) ? Convert.FromBase64String(packDto.Image) : null;
                pack.BronzeChance = packDto.BronzeChance;
                pack.SilverChance = packDto.SilverChance;
                pack.GoldChance = packDto.GoldChance;
                pack.PlatinumChance = packDto.PlatinumChance;
                pack.DiamondChance = packDto.DiamondChance;
                pack.TotalBought = packDto.TotalBought;
                pack.CreatedOn = DateTime.SpecifyKind(packDto.CreatedOn, DateTimeKind.Utc);
                pack.CreatedBy = packDto.CreatedBy;
                pack.UpdatedOn = packDto.UpdatedOn.HasValue ? DateTime.SpecifyKind(packDto.UpdatedOn.Value, DateTimeKind.Utc) : (DateTime?)null;
                pack.UpdatedBy = packDto.UpdatedBy;

                _context.Packs.Update(pack);
                await _context.SaveChangesAsync();

                packDto.Id = pack.Id;
            }

            // Retorna o DTO atualizado com o ID do pack criado ou atualizado
            return Ok(packDto);
        }

        [HttpDelete("DeletePack/{id}")]
        public async Task<IActionResult> DeletePack(int id)
        {
            var pack = await _context.Packs.FindAsync(id);
            if (pack == null)
            {
                return NotFound();
            }

            _context.Packs.Remove(pack);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
