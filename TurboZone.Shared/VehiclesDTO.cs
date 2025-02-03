using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace TurboZone.Shared
{
    public class VehiclesDTO
    {
        public int VehicleId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Year { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Brand { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Model { get; set; } = null!;
        public int Price { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Condition { get; set; } = null!;
        public string? Motor { get; set; }
        public string Type { get; set; } = null!;
        public int Seats { get; set; }
        public string? ExteriorColor { get; set; }
        public string? InteriorColor { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FuelType { get; set; } = null!;
        public string Transmission { get; set; } = null!;
        public int Mileage { get; set; }
    }
}
