namespace RentACar.Models
{
    public class ProcesoRentaDevolucion
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public required Empleado Empleado { get; set; }
        public int VehiculoId { get; set; }
        public required Vehiculo Vehiculo { get; set; }
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public DateTime FechaRenta { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public decimal MontoXDia { get; set; }
        public int CantidadDias { get; set; }
        public string Comentario { get; set; } = "";
        public Estado Estado { get; set; }
    }
}
