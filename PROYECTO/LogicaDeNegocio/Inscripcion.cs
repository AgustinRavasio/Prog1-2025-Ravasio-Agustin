
namespace LogicaDeNegocio
{
    public class Inscripcion
    {
        public string DocumentoSocio { get; set; }
        public string CodigoActividad { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool Activa { get; set; } = true;

        public bool Cancelada { get; set; }
    }
}
