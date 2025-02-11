using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                      .Cast<Estado>()
                      .Select(e => new SelectListItem
                      {
                          Text = e.ToString(),
                          Value = ((int)e).ToString()
                      })
                      .ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(TipoVehiculo tipoVehiculo)
        {
            await _appDbContext.TipoVehiculos.AddAsync(tipoVehiculo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                      .Cast<Estado>()
                      .Select(e => new SelectListItem
                      {
                          Text = e.ToString(),
                          Value = ((int)e).ToString()
                      })
                      .ToList();

            TipoVehiculo tipoVehiculo = await _appDbContext.TipoVehiculos.FirstAsync(e => e.Id == Id);
            return View(tipoVehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoVehiculo tipoVehiculo)
        {
            _appDbContext.TipoVehiculos.Update(tipoVehiculo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            TipoVehiculo tipoVehiculo = await _appDbContext.TipoVehiculos.FirstAsync(e => e.Id == Id);

            _appDbContext.TipoVehiculos.Remove(tipoVehiculo); 
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
