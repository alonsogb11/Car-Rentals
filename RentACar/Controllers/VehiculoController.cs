using Microsoft.AspNetCore.Mvc;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehiculoController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public VehiculoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Vehiculo> lista = await _appDbContext.Vehiculos
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .Include(v => v.TipoCombustible)
                .ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Nuevo()
        {
            ViewBag.TiposVehiculo = await _appDbContext.TipoVehiculos
                .Where(tv => tv.Estado == Estado.Activo)
                .Select(tv => new SelectListItem
                {
                    Text = tv.Descripcion,
                    Value = tv.Id.ToString()
                })
                .ToListAsync();

            ViewBag.Marcas = await _appDbContext.Marcas
                .Where(m => m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString()
                })
                .ToListAsync();

            ViewBag.TiposCombustible = await _appDbContext.TipoCombustibles
                .Where(tc => tc.Estado == Estado.Activo)
                .Select(tc => new SelectListItem
                {
                    Text = tc.Descripcion,
                    Value = tc.Id.ToString()
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

            ViewBag.Modelos = new List<SelectListItem>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Vehiculo vehiculo)
        {
            var tipoVehiculoExiste = await _appDbContext.TipoVehiculos
                .AnyAsync(tv => tv.Id == vehiculo.TipoVehiculoId);

            var marcaExiste = await _appDbContext.Marcas
                .AnyAsync(m => m.Id == vehiculo.MarcaId);

            var modeloExiste = await _appDbContext.Modelos
                .AnyAsync(mo => mo.Id == vehiculo.ModeloId && mo.MarcaId == vehiculo.MarcaId);

            var tipoCombustibleExiste = await _appDbContext.TipoCombustibles
                .AnyAsync(tc => tc.Id == vehiculo.TipoCombustibleId);

            if (!tipoVehiculoExiste || !marcaExiste || !modeloExiste || !tipoCombustibleExiste)
            {
                ViewBag.TiposVehiculo = await _appDbContext.TipoVehiculos
                    .Where(tv => tv.Estado == Estado.Activo)
                    .Select(tv => new SelectListItem
                    {
                        Text = tv.Descripcion,
                        Value = tv.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Marcas = await _appDbContext.Marcas
                    .Where(m => m.Estado == Estado.Activo)
                    .Select(m => new SelectListItem
                    {
                        Text = m.Descripcion,
                        Value = m.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.TiposCombustible = await _appDbContext.TipoCombustibles
                    .Where(tc => tc.Estado == Estado.Activo)
                    .Select(tc => new SelectListItem
                    {
                        Text = tc.Descripcion,
                        Value = tc.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Modelos = await _appDbContext.Modelos
                    .Where(m => m.MarcaId == vehiculo.MarcaId && m.Estado == Estado.Activo)
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

                if (!tipoVehiculoExiste)
                    ModelState.AddModelError("TipoVehiculoId", "El tipo de vehículo seleccionado no existe");
                if (!marcaExiste)
                    ModelState.AddModelError("MarcaId", "La marca seleccionada no existe");
                if (!modeloExiste)
                    ModelState.AddModelError("ModeloId", "El modelo seleccionado no existe o no pertenece a la marca indicada");
                if (!tipoCombustibleExiste)
                    ModelState.AddModelError("TipoCombustibleId", "El tipo de combustible seleccionado no existe");

                return View(vehiculo);
            }

            await _appDbContext.Vehiculos.AddAsync(vehiculo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            Vehiculo vehiculo = await _appDbContext.Vehiculos
                .Include(v => v.TipoVehiculo)
                .Include(v => v.Marca)
                .Include(v => v.Modelo)
                .Include(v => v.TipoCombustible)
                .FirstAsync(v => v.Id == Id);

            ViewBag.TiposVehiculo = await _appDbContext.TipoVehiculos
                .Where(tv => tv.Estado == Estado.Activo)
                .Select(tv => new SelectListItem
                {
                    Text = tv.Descripcion,
                    Value = tv.Id.ToString(),
                    Selected = tv.Id == vehiculo.TipoVehiculoId
                })
                .ToListAsync();

            ViewBag.Marcas = await _appDbContext.Marcas
                .Where(m => m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString(),
                    Selected = m.Id == vehiculo.MarcaId
                })
                .ToListAsync();

            ViewBag.Modelos = await _appDbContext.Modelos
                .Where(m => (m.MarcaId == vehiculo.MarcaId || m.Id == vehiculo.ModeloId) && m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString(),
                    Selected = m.Id == vehiculo.ModeloId
                })
                .ToListAsync();

            ViewBag.TiposCombustible = await _appDbContext.TipoCombustibles
                .Where(tc => tc.Estado == Estado.Activo)
                .Select(tc => new SelectListItem
                {
                    Text = tc.Descripcion,
                    Value = tc.Id.ToString(),
                    Selected = tc.Id == vehiculo.TipoCombustibleId
                })
                .ToListAsync();

            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == vehiculo.Estado
                })
                .ToList();

            return View(vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Vehiculo vehiculo)
        {
            var tipoVehiculoExiste = await _appDbContext.TipoVehiculos
                .AnyAsync(tv => tv.Id == vehiculo.TipoVehiculoId);

            var marcaExiste = await _appDbContext.Marcas
                .AnyAsync(m => m.Id == vehiculo.MarcaId);

            var modeloExiste = await _appDbContext.Modelos
                .AnyAsync(mo => mo.Id == vehiculo.ModeloId && mo.MarcaId == vehiculo.MarcaId);

            var tipoCombustibleExiste = await _appDbContext.TipoCombustibles
                .AnyAsync(tc => tc.Id == vehiculo.TipoCombustibleId);

            if (!tipoVehiculoExiste || !marcaExiste || !modeloExiste || !tipoCombustibleExiste)
            {
                ViewBag.TiposVehiculo = await _appDbContext.TipoVehiculos
                    .Where(tv => tv.Estado == Estado.Activo)
                    .Select(tv => new SelectListItem
                    {
                        Text = tv.Descripcion,
                        Value = tv.Id.ToString(),
                        Selected = tv.Id == vehiculo.TipoVehiculoId
                    })
                    .ToListAsync();

                ViewBag.Marcas = await _appDbContext.Marcas
                    .Where(m => m.Estado == Estado.Activo)
                    .Select(m => new SelectListItem
                    {
                        Text = m.Descripcion,
                        Value = m.Id.ToString(),
                        Selected = m.Id == vehiculo.MarcaId
                    })
                    .ToListAsync();

                ViewBag.Modelos = await _appDbContext.Modelos
                    .Where(m => (m.MarcaId == vehiculo.MarcaId || m.Id == vehiculo.ModeloId) && m.Estado == Estado.Activo)
                    .Select(m => new SelectListItem
                    {
                        Text = m.Descripcion,
                        Value = m.Id.ToString(),
                        Selected = m.Id == vehiculo.ModeloId
                    })
                    .ToListAsync();

                ViewBag.TiposCombustible = await _appDbContext.TipoCombustibles
                    .Where(tc => tc.Estado == Estado.Activo)
                    .Select(tc => new SelectListItem
                    {
                        Text = tc.Descripcion,
                        Value = tc.Id.ToString(),
                        Selected = tc.Id == vehiculo.TipoCombustibleId
                    })
                    .ToListAsync();

                ViewBag.Estados = Enum.GetValues(typeof(Estado))
                    .Cast<Estado>()
                    .Select(e => new SelectListItem
                    {
                        Text = e.ToString(),
                        Value = ((int)e).ToString(),
                        Selected = e == vehiculo.Estado
                    })
                    .ToList();

                if (!tipoVehiculoExiste)
                    ModelState.AddModelError("TipoVehiculoId", "El tipo de vehículo seleccionado no existe");
                if (!marcaExiste)
                    ModelState.AddModelError("MarcaId", "La marca seleccionada no existe");
                if (!modeloExiste)
                    ModelState.AddModelError("ModeloId", "El modelo seleccionado no existe o no pertenece a la marca indicada");
                if (!tipoCombustibleExiste)
                    ModelState.AddModelError("TipoCombustibleId", "El tipo de combustible seleccionado no existe");

                return View(vehiculo);
            }

            _appDbContext.Vehiculos.Update(vehiculo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Vehiculo vehiculo = await _appDbContext.Vehiculos.FirstAsync(v => v.Id == Id);
            _appDbContext.Vehiculos.Remove(vehiculo);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerModelosPorMarca(int marcaId)
        {
            var modelos = await _appDbContext.Modelos
                .Where(m => m.MarcaId == marcaId && m.Estado == Estado.Activo)
                .Select(m => new SelectListItem
                {
                    Text = m.Descripcion,
                    Value = m.Id.ToString()
                })
                .ToListAsync();

            return Json(modelos);
        }
    }
}