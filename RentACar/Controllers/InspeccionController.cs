using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InspeccionController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public InspeccionController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Inspeccion> lista = await _appDbContext.Inspecciones
                .Include(v => v.ProcesoRentaDevolucion)
                .Include(v => v.Vehiculo)
                .Include(v => v.Cliente)
                .Include(v => v.Empleado)
                .ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Nuevo()
        {
            ViewBag.Empleados = await _appDbContext.Empleados
                .Where(em => em.Estado == Estado.Activo)
                .Select(em => new SelectListItem
                {
                    Text = em.Nombre,
                    Value = em.Id.ToString()
                })
                .ToListAsync();


            ViewBag.Vehiculos = await _appDbContext.Vehiculos
                .Where(v => v.Estado == Estado.Activo)
                .Select(v => new SelectListItem
                {
                    Text = v.Modelo.Descripcion,
                    Value = v.Id.ToString()
                })
                .ToListAsync();

            ViewBag.Clientes = await _appDbContext.Clientes
                .Where(c => c.Estado == Estado.Activo)
                .Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                })
                .ToListAsync();

            ViewBag.ProcesosRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion
                .Where(prd => prd.Estado == Estado.Activo)
                .Select(prd => new SelectListItem
                {
                    Text = prd.Comentario,
                    Value = prd.Id.ToString()
                })
                .ToListAsync();

            ViewBag.CantCombustibles = Enum.GetValues(typeof(CantCombustible))
                .Cast<CantCombustible>()
                .Select(cc => new SelectListItem
                {
                    Text = cc.ToString(),
                    Value = ((int)cc).ToString()
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
        public async Task<IActionResult> Nuevo(Inspeccion Inspeccion)
        {
            var empleadoExiste = await _appDbContext.Empleados
                .AnyAsync(em => em.Id == Inspeccion.EmpleadoId);

            var vehiculoExiste = await _appDbContext.Vehiculos
                .AnyAsync(v => v.Id == Inspeccion.VehiculoId);

            var clienteExiste = await _appDbContext.Clientes
                .AnyAsync(c => c.Id == Inspeccion.ClienteId);

            var procesoExiste = await _appDbContext.ProcesosRentaDevolucion
                .AnyAsync(prd => prd.Id == Inspeccion.ProcesoRentaDevolucionId);

            if (!empleadoExiste || !vehiculoExiste || !clienteExiste || !procesoExiste)
            {
                ViewBag.Empleados = await _appDbContext.Empleados
                    .Where(em => em.Estado == Estado.Activo)
                    .Select(em => new SelectListItem
                    {
                        Text = em.Nombre,
                        Value = em.Id.ToString()
                    })
                    .ToListAsync();


                ViewBag.Vehiculos = await _appDbContext.Vehiculos
                    .Where(v => v.Estado == Estado.Activo)
                    .Select(v => new SelectListItem
                    {
                        Text = v.Modelo.Descripcion,
                        Value = v.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Clientes = await _appDbContext.Clientes
                    .Where(c => c.Estado == Estado.Activo)
                    .Select(c => new SelectListItem
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.ProcesosRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion
                    .Where(prd => prd.Estado == Estado.Activo)
                    .Select(prd => new SelectListItem
                    {
                        Text = prd.Comentario,
                        Value = prd.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.CantCombustibles = Enum.GetValues(typeof(CantCombustible))
                    .Cast<CantCombustible>()
                    .Select(cc => new SelectListItem
                    {
                        Text = cc.ToString(),
                        Value = ((int)cc).ToString()
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

                if (!empleadoExiste)
                    ModelState.AddModelError("EmpleadoId", "El empleado seleccionado no existe");
                if (!vehiculoExiste)
                    ModelState.AddModelError("VehiculoId", "El vehículo seleccionada no existe");
                if (!clienteExiste)
                    ModelState.AddModelError("ClienteId", "El cliente seleccionado no existe");
                if (!procesoExiste)
                    ModelState.AddModelError("ProcesoRentaDevolucionId", "El proceso de renta seleccionado no existe");

                return View(Inspeccion);
            }

            await _appDbContext.Inspecciones.AddAsync(Inspeccion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            Inspeccion Inspeccion = await _appDbContext.Inspecciones
                .Include(p => p.Empleado)
                .Include(p => p.Vehiculo)
                .Include(p => p.Cliente)
                .Include(p => p.ProcesoRentaDevolucion)
                .FirstAsync(p => p.Id == Id);

            ViewBag.Empleados = await _appDbContext.Empleados
                .Where(em => em.Estado == Estado.Activo)
                .Select(em => new SelectListItem
                {
                    Text = em.Nombre,
                    Value = em.Id.ToString(),
                    Selected = em.Id == Inspeccion.EmpleadoId
                })
                .ToListAsync();


            ViewBag.Vehiculos = await _appDbContext.Vehiculos
                .Where(v => v.Estado == Estado.Activo)
                .Select(v => new SelectListItem
                {
                    Text = v.Modelo.Descripcion,
                    Value = v.Id.ToString(),
                    Selected = v.Id == Inspeccion.VehiculoId
                })
                .ToListAsync();

            ViewBag.Clientes = await _appDbContext.Clientes
                .Where(c => c.Estado == Estado.Activo)
                .Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                    Selected = c.Id == Inspeccion.ClienteId
                })
                .ToListAsync();

            ViewBag.ProcesosRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion
                .Where(prd => prd.Estado == Estado.Activo)
                .Select(prd => new SelectListItem
                {
                    Text = prd.Comentario,
                    Value = prd.Id.ToString()
                })
                .ToListAsync();

            ViewBag.CantCombustibles = Enum.GetValues(typeof(CantCombustible))
                .Cast<CantCombustible>()
                .Select(cc => new SelectListItem
                {
                    Text = cc.ToString(),
                    Value = ((int)cc).ToString()
                })
                .ToList();

            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == Inspeccion.Estado
                })
                .ToList();

            return View(Inspeccion);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Inspeccion Inspeccion)
        {
            var empleadoExiste = await _appDbContext.Empleados
                .AnyAsync(em => em.Id == Inspeccion.EmpleadoId);

            var vehiculoExiste = await _appDbContext.Vehiculos
                .AnyAsync(v => v.Id == Inspeccion.VehiculoId);

            var clienteExiste = await _appDbContext.Clientes
                .AnyAsync(c => c.Id == Inspeccion.ClienteId);

            var procesoExiste = await _appDbContext.ProcesosRentaDevolucion
                .AnyAsync(prd => prd.Id == Inspeccion.ProcesoRentaDevolucionId);

            if (!empleadoExiste || !vehiculoExiste || !clienteExiste || !procesoExiste)
            {
                ViewBag.Empleados = await _appDbContext.Empleados
                    .Where(em => em.Estado == Estado.Activo)
                    .Select(em => new SelectListItem
                    {
                        Text = em.Nombre,
                        Value = em.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Vehiculos = await _appDbContext.Vehiculos
                    .Where(v => v.Estado == Estado.Activo)
                    .Select(v => new SelectListItem
                    {
                        Text = v.Modelo.Descripcion,
                        Value = v.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.Clientes = await _appDbContext.Clientes
                    .Where(c => c.Estado == Estado.Activo)
                    .Select(c => new SelectListItem
                    {
                        Text = c.Nombre,
                        Value = c.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.ProcesosRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion
                    .Where(prd => prd.Estado == Estado.Activo)
                    .Select(prd => new SelectListItem
                    {
                        Text = prd.Comentario,
                        Value = prd.Id.ToString()
                    })
                    .ToListAsync();

                ViewBag.CantCombustibles = Enum.GetValues(typeof(CantCombustible))
                    .Cast<CantCombustible>()
                    .Select(cc => new SelectListItem
                    {
                        Text = cc.ToString(),
                        Value = ((int)cc).ToString()
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

                if (!empleadoExiste)
                    ModelState.AddModelError("EmpleadoId", "El empleado seleccionado no existe");
                if (!vehiculoExiste)
                    ModelState.AddModelError("VehiculoId", "El vehículo seleccionada no existe");
                if (!clienteExiste)
                    ModelState.AddModelError("ClienteId", "El cliente seleccionado no existe");
                if (!procesoExiste)
                    ModelState.AddModelError("ProcesosRentaDevolucionId", "El proceso de renta seleccionado no existe");

                return View(Inspeccion);
            }

            _appDbContext.Inspecciones.Update(Inspeccion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            Inspeccion Inspeccion = await _appDbContext.Inspecciones.FirstAsync(e => e.Id == Id);
            _appDbContext.Inspecciones.Remove(Inspeccion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
