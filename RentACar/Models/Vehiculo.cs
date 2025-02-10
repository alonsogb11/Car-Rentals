namespace RentACar.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public string NumChasis { get; set; } = "";
        public string Motor { get; set; } = "";
        public string NumPlaca { get; set; } = "";
        public int TipoVehiculoId { get; set; }
        public required TipoVehiculo TipoVehiculo { get; set; }
        public int MarcaId { get; set; }
        public required Marca Marca { get; set; }
        public int ModeloId { get; set; }
        public required Modelo Modelo { get; set; }
        public int TipoCombustibleId { get; set; }
        public required TipoCombustible TipoCombustible { get; set; }
        public Estado Estado { get; set; }
    }
}
