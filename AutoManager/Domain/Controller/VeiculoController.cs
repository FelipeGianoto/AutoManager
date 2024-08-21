using AutoManager.Cache;
using AutoManager.Data;
using AutoManager.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace AutoManager.Domain.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly AutoManagerContext _context;
        private readonly ICacheService _cacheService;
        private const string cacheKey = "veiculos";

        public VeiculoController(AutoManagerContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var cachedData = _cacheService.GetData<IEnumerable<Veiculo>>(cacheKey);
            if(cachedData is not null)
            {
                return Ok(cachedData);
            }
            var expirationTime = DateTime.Now.AddMinutes(5);
            cachedData = _context.Veiculos.ToList();
            _cacheService.SetData<IEnumerable<Veiculo>>(cacheKey, cachedData, expirationTime);
            return Ok(cachedData);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Veiculo veiculo)
        {
            var obj = await _context.Veiculos.AddAsync(veiculo);
            _context.SaveChanges();
            _cacheService.RemoveData(cacheKey);
            return Ok(obj.Entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var veiculo = _context.Veiculos.Find(id);
            if(veiculo is null) return NotFound();
            _context.Veiculos.Remove(veiculo);
            _context.SaveChanges();
            _cacheService.RemoveData(cacheKey);
            return NoContent();
        }
    }
}
