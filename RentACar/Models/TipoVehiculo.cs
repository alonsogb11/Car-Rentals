namespace RentACar.Models
{
    public enum Estado
    {
        Activo,
        Inactivo
    }

    public class TipoVehiculo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public Estado Estado { get; set; }
    }
}
