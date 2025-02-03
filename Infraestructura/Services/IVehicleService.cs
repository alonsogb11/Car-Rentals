using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Vehicles;

namespace Infrastructure.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> Lista();
        Task<VehicleDTO> Buscar(int id);
        Task<int> Guardar(VehicleDTO vehicle);
        Task<int> Editar(VehicleDTO vehicle);
        Task<bool> Eliminar(int id);
    }
}
