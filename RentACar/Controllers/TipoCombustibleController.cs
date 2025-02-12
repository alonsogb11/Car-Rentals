using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    public class TipoCombustibleController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public TipoCombustibleController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<TipoCombustible> lista = await _appDbContext.TipoCombustibles.ToListAsync();
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
        public async Task<IActionResult> Nuevo(TipoCombustible tipoCombustible)
        {
            await _appDbContext.TipoCombustibles.AddAsync(tipoCombustible);
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

            TipoCombustible tipoCombustible = await _appDbContext.TipoCombustibles.FirstAsync(e => e.Id == Id);
            return View(tipoCombustible);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TipoCombustible tipoCombustible)
        {
            _appDbContext.TipoCombustibles.Update(tipoCombustible);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            TipoCombustible tipoCombustible = await _appDbContext.TipoCombustibles.FirstAsync(e => e.Id == Id);
            _appDbContext.TipoCombustibles.Remove(tipoCombustible);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
