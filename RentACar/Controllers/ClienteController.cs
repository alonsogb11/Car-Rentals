using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public ClienteController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Cliente> lista = await _appDbContext.Clientes.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            ViewBag.TipoPersona = Enum.GetValues(typeof(TipoPersona))
                .Cast<TipoPersona>()
                .Select(tp => new SelectListItem
                {
                    Text = tp.ToString(),
                    Value = ((int)tp).ToString()
                })
                .ToList();

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
        public async Task<IActionResult> Nuevo(Cliente cliente)
        {
            await _appDbContext.Clientes.AddAsync(cliente);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ViewBag.TipoPersona = Enum.GetValues(typeof(TipoPersona))
                .Cast<TipoPersona>()
                .Select(tp => new SelectListItem
                {
                    Text = tp.ToString(),
                    Value = ((int)tp).ToString()
                })
                .ToList();

            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString()
                })
                .ToList();

            Cliente cliente = await _appDbContext.Clientes.FirstAsync(e => e.Id == Id);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            _appDbContext.Clientes.Update(cliente);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Cliente cliente = await _appDbContext.Clientes.FirstAsync(e => e.Id == Id);
            _appDbContext.Clientes.Remove(cliente);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
