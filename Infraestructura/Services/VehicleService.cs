using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Vehicles;
using System.Net.Http.Json;
using Domain.ErrorManagement;
using Domain.Entity.Vehicles;
using Infrastructure.Services;

namespace Infrastructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _http;

        public VehicleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<VehicleDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<VehicleDTO>>>("api/Vehicle/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<VehicleDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<VehicleDTO>>($"api/Vehicle/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(VehicleDTO vehicle)
        {
            var result = await _http.PostAsJsonAsync("api/Vehicle/Guardar", vehicle);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(VehicleDTO vehicle)
        {
            var result = await _http.PutAsJsonAsync($"api/Vehicle/Editar/{vehicle.VehicleId}", vehicle);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Vehicle/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
                throw new Exception(response.Mensaje);
        }
    }
}
