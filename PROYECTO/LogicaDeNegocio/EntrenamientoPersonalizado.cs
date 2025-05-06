
namespace LogicaDeNegocio
{
    public class EntrenamientoPersonalizado : Actividad
    {
        public decimal CostoAdicional { get; set; }
        public string Horario { get; set; }

        public override string ObtenerDescripcion()
        {
            return $"{Nombre} - Personalizado con {Profesor}, duración: {DuracionMinutos} min, costo: ${CostoAdicional}, horario: {Horario}";
        }
    }
}
