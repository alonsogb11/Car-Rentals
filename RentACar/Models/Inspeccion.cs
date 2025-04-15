namespace RentACar.Models
{
    public enum CantCombustible
    {
        Vacio,
        UnCuarto,
        Mitad,
        TresCuartos,
        Lleno
    }

    public class Inspeccion
    {
        public int Id { get; set; }
        public int ProcesoRentaDevolucionId { get; set; }
        public required ProcesoRentaDevolucion ProcesoRentaDevolucion { get; set; }
        public int VehiculoId { get; set; }
        public required Vehiculo Vehiculo { get; set; }
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }
        public CantCombustible CantCombustible { get; set; }
        public bool TieneRayones { get; set; }
        public bool TieneGomaRepuesta { get; set; }
        public bool TieneGato { get; set; }
        public bool TieneCristalRoto { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public int EmpleadoId { get; set; }
        public required Empleado Empleado { get; set; }
        public Estado Estado { get; set; }
    }
}
