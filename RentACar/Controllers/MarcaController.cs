using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarcaController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public MarcaController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Marca> lista = await _appDbContext.Marcas.ToListAsync();
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
        public async Task<IActionResult> Nuevo(Marca marca)
        {
            await _appDbContext.Marcas.AddAsync(marca);
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

            Marca marca = await _appDbContext.Marcas.FirstAsync(e => e.Id == Id);
            return View(marca);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Marca marca)
        {
            _appDbContext.Marcas.Update(marca);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Marca marca = await _appDbContext.Marcas.FirstAsync(e => e.Id == Id);
            _appDbContext.Marcas.Remove(marca);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
