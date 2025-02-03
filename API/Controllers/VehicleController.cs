using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Domain.Entity.Vehicles;
using Application.DTOs.Vehicles;
using Domain.ErrorManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly TurboZoneDbContext _DBContext;
        public VehicleController(TurboZoneDbContext DBContext)
        {
            _DBContext = DBContext;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<VehicleDTO>>();
            var listaVehiclesDTO = new List<VehicleDTO>();

            try
            {
                foreach (var item in await _DBContext.Vehicles.ToListAsync())
                {
                    listaVehiclesDTO.Add(new VehicleDTO
                    {
                        VehicleId = item.VehicleId,
                        Year = item.Year,
                        Brand = item.Brand,
                        Model = item.Model,
                        Price = item.Price,
                        Condition = item.Condition,
                        Motor = item.Motor, 
                        Type = item.Type,
                        Seats = item.Seats,
                        ExteriorColor = item.ExteriorColor,
                        InteriorColor = item.InteriorColor,
                        FuelType = item.FuelType,
                        Transmission = item.Transmission,
                        Mileage = item.Mileage,
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaVehiclesDTO;
            }
            catch (Exception ex) 
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<VehicleDTO>();
            var VehiclesDTO = new VehicleDTO();

            try
            {
                var dbVehicle = await _DBContext.Vehicles.FirstOrDefaultAsync(x => x.VehicleId == id);

                if (dbVehicle != null) 
                {
                    VehiclesDTO.VehicleId = dbVehicle.VehicleId;
                    VehiclesDTO.Year = dbVehicle.Year;
                    VehiclesDTO.Brand = dbVehicle.Brand;
                    VehiclesDTO.Model = dbVehicle.Model;
                    VehiclesDTO.Price = dbVehicle.Price;
                    VehiclesDTO.Condition = dbVehicle.Condition;
                    VehiclesDTO.Motor = dbVehicle.Motor;
                    VehiclesDTO.Type = dbVehicle.Type;
                    VehiclesDTO.Seats = dbVehicle.Seats;
                    VehiclesDTO.ExteriorColor = dbVehicle.ExteriorColor;
                    VehiclesDTO.InteriorColor = dbVehicle.InteriorColor;
                    VehiclesDTO.FuelType = dbVehicle.FuelType;
                    VehiclesDTO.Transmission = dbVehicle.Transmission;
                    VehiclesDTO.Mileage = dbVehicle.Mileage;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = VehiclesDTO;
                } 
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Resultado no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(VehicleDTO vehicle)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbVehicle = new Vehicle
                {
                    Year = vehicle.Year,
                    Brand = vehicle.Brand,
                    Model = vehicle.Model,
                    Price = vehicle.Price,
                    Condition = vehicle.Condition,
                    Motor = vehicle.Motor,
                    Type = vehicle.Type,
                    Seats = vehicle.Seats,
                    ExteriorColor = vehicle.ExteriorColor,
                    InteriorColor = vehicle.InteriorColor,
                    FuelType = vehicle.FuelType,
                    Transmission = vehicle.Transmission,
                    Mileage = vehicle.Mileage,
                };

                _DBContext.Vehicles.Add(dbVehicle);
                await _DBContext.SaveChangesAsync();

                if (dbVehicle.VehicleId != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbVehicle.VehicleId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No se pudieron guardar los cambios";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(VehicleDTO vehicle, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbVehicle = await _DBContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);

                if (dbVehicle != null)
                {
                    dbVehicle.Year = vehicle.Year;
                    dbVehicle.Brand = vehicle.Brand;
                    dbVehicle.Model = vehicle.Model;
                    dbVehicle.Price = vehicle.Price;
                    dbVehicle.Condition = vehicle.Condition;
                    dbVehicle.Motor = vehicle.Motor;
                    dbVehicle.Type = vehicle.Type;
                    dbVehicle.Seats = vehicle.Seats;
                    dbVehicle.ExteriorColor = vehicle.ExteriorColor;
                    dbVehicle.InteriorColor = vehicle.InteriorColor;
                    dbVehicle.FuelType = vehicle.FuelType;
                    dbVehicle.Transmission = vehicle.Transmission;
                    dbVehicle.Mileage = vehicle.Mileage;

                    _DBContext.Vehicles.Update(dbVehicle);
                    await _DBContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbVehicle.VehicleId;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Vehículo no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbVehicle = await _DBContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);

                if (dbVehicle != null)
                {
                    _DBContext.Vehicles.Remove(dbVehicle);
                    await _DBContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Vehículo no encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
