
namespace LogicaDeNegocio
{
    public abstract class Actividad
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int DuracionMinutos { get; set; }
        public NivelMembresia NivelRequerido { get; set; }
        public string Profesor { get; set; }

        public abstract string ObtenerDescripcion();
    }
}
