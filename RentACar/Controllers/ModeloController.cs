using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ModeloController : Controller
    {
        private readonly AppDBContext _appDbContext;

        public ModeloController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Modelo> lista = await _appDbContext.Modelos
                .Include(m => m.Marca)
                .ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Nuevo()
        {
            ViewBag.Marcas = await _appDbContext.Marcas
                .Where(m => m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString()
                })
                .ToListAsync();

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
        public async Task<IActionResult> Nuevo(Modelo modelo)
        {
            var marcaExiste = await _appDbContext.Marcas
                .AnyAsync(m => m.Id == modelo.MarcaId);

            if (!marcaExiste)
            {
                ViewBag.Marcas = await _appDbContext.Marcas
                    .Where(m => m.Estado == Estado.Activo)
                    .Select(m => new SelectListItem
                    {
                        Text = m.Descripcion,
                        Value = m.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Estados = Enum.GetValues(typeof(Estado))
                    .Cast<Estado>()
                    .Select(e => new SelectListItem
                    {
                        Text = e.ToString(),
                        Value = ((int)e).ToString()
                    })
                    .ToList();

                ModelState.AddModelError("MarcaId", "La marca seleccionada no existe");
                return View(modelo);
            }

            await _appDbContext.Modelos.AddAsync(modelo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.Marcas = await _appDbContext.Marcas
                .Where(m => m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString()
                })
                .ToListAsync();

            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString()
                })
                .ToList();

            Modelo modelo = await _appDbContext.Modelos
                .Include(m => m.Marca)
                .FirstAsync(e => e.Id == Id);

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Modelo modelo)
        {
            var marcaExiste = await _appDbContext.Marcas
                .AnyAsync(m => m.Id == modelo.MarcaId);

            if (!marcaExiste)
            {
                ViewBag.Marcas = await _appDbContext.Marcas
                    .Where(m => m.Estado == Estado.Activo)
                    .Select(m => new SelectListItem
                    {
                        Text = m.Descripcion,
                        Value = m.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Estados = Enum.GetValues(typeof(Estado))
                    .Cast<Estado>()
                    .Select(e => new SelectListItem
                    {
                        Text = e.ToString(),
                        Value = ((int)e).ToString()
                    })
                    .ToList();

                ModelState.AddModelError("MarcaId", "La marca seleccionada no existe");
                return View(modelo);
            }

            _appDbContext.Modelos.Update(modelo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Modelo modelo = await _appDbContext.Modelos.FirstAsync(e => e.Id == Id);
            _appDbContext.Modelos.Remove(modelo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}