namespace RentACar.Models
{
    public class Modelo
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public required Marca Marca { get; set; }
        public string Descripcion { get; set; } = "";
        public Estado Estado { get; set; }
    }
}
