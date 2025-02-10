namespace RentACar.Models
{
    public class TipoCombustible
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public Estado Estado { get; set; }
    }
}
