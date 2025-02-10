namespace RentACar.Models
{
    public enum TipoPersona
    {
        Fisica,
        Juridica
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Cedula { get; set; } = "";
        public string NumTarjetaCredito { get; set; } = "";
        public TipoPersona TipoPersona { get; set; }
        public Estado Estado { get; set; }
    }
}
