using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data;
using RentACar.Models;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProcesoRentaDevolucionController : Controller
    {
        private readonly AppDBContext _appDbContext;
        public ProcesoRentaDevolucionController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ProcesoRentaDevolucion> lista = await _appDbContext.ProcesosRentaDevolucion
                .Include(v => v.Empleado)
                .Include(v => v.Vehiculo)
                .Include(v => v.Cliente)
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
        public async Task<IActionResult> Nuevo(ProcesoRentaDevolucion procesoRentaDevolucion)
        {
            var empleadoExiste = await _appDbContext.Empleados
                .AnyAsync(em => em.Id == procesoRentaDevolucion.EmpleadoId);

            var vehiculoExiste = await _appDbContext.Vehiculos
                .AnyAsync(v => v.Id == procesoRentaDevolucion.VehiculoId);

            var clienteExiste = await _appDbContext.Clientes
                .AnyAsync(c => c.Id == procesoRentaDevolucion.ClienteId);

            if (!empleadoExiste || !vehiculoExiste || !clienteExiste)
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

                return View(procesoRentaDevolucion);
            }

            await _appDbContext.ProcesosRentaDevolucion.AddAsync(procesoRentaDevolucion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int Id)
        {
            ProcesoRentaDevolucion procesoRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion
                .Include(p => p.Empleado)
                .Include(p => p.Vehiculo)
                .Include(p => p.Cliente)
                .FirstAsync(p => p.Id == Id);

            ViewBag.Empleados = await _appDbContext.Empleados
                .Where(em => em.Estado == Estado.Activo)
                .Select(em => new SelectListItem
                {
                    Text = em.Nombre,
                    Value = em.Id.ToString(),
                    Selected = em.Id == procesoRentaDevolucion.EmpleadoId
                })
                .ToListAsync();


            ViewBag.Vehiculos = await _appDbContext.Vehiculos
                .Where(v => v.Estado == Estado.Activo)
                .Select(v => new SelectListItem
                {
                    Text = v.Modelo.Descripcion,
                    Value = v.Id.ToString(),
                    Selected = v.Id == procesoRentaDevolucion.VehiculoId
                })
                .ToListAsync();

            ViewBag.Clientes = await _appDbContext.Clientes
                .Where(c => c.Estado == Estado.Activo)
                .Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString(),
                    Selected = c.Id == procesoRentaDevolucion.ClienteId
                })
                .ToListAsync();

            ViewBag.Estados = Enum.GetValues(typeof(Estado))
                .Cast<Estado>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString(),
                    Value = ((int)e).ToString(),
                    Selected = e == procesoRentaDevolucion.Estado
                })
                .ToList();

            return View(procesoRentaDevolucion);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProcesoRentaDevolucion procesoRentaDevolucion)
        {
            var empleadoExiste = await _appDbContext.Empleados
                .AnyAsync(em => em.Id == procesoRentaDevolucion.EmpleadoId);

            var vehiculoExiste = await _appDbContext.Vehiculos
                .AnyAsync(v => v.Id == procesoRentaDevolucion.VehiculoId);

            var clienteExiste = await _appDbContext.Clientes
                .AnyAsync(c => c.Id == procesoRentaDevolucion.ClienteId);

            if (!empleadoExiste || !vehiculoExiste || !clienteExiste)
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

                return View(procesoRentaDevolucion);
            }

            _appDbContext.ProcesosRentaDevolucion.Update(procesoRentaDevolucion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int Id)
        {
            ProcesoRentaDevolucion procesoRentaDevolucion = await _appDbContext.ProcesosRentaDevolucion.FirstAsync(e => e.Id == Id);
            _appDbContext.ProcesosRentaDevolucion.Remove(procesoRentaDevolucion);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}
