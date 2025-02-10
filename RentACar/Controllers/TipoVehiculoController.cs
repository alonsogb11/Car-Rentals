using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    public class TipoVehiculoController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public TipoVehiculoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<TipoVehiculo> lista = await _appDbContext.TipoVehiculos.ToListAsync();
            return View(lista);
        }
    }
}
