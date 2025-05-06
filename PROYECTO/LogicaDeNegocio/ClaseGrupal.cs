
namespace LogicaDeNegocio
{
    public class ClaseGrupal : Actividad
    {
        public int CupoMaximo { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"{Nombre} - Grupal con {Profesor}, duración: {DuracionMinutos} min, cupo: {CupoMaximo}";
        }
    }
}
