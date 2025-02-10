namespace RentACar.Models
{
    public enum TandaLabor
    {
        Mañana,
        Tarde
    }

    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Cedula { get; set; } = "";
        public TandaLabor TandaLabor { get; set; }
        public decimal PorcientoComision { get; set; }
        public DateOnly FechaContratacion { get; set; }
        public Estado Estado { get; set; }
    }
}
