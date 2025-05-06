using System;
using System.Collections.Generic;

namespace LogicaDeNegocio
{
    public class GimnasioService
    {
        private List<Socio> socios = new();
        private List<Actividad> actividades = new();
        private List<Inscripcion> inscripciones = new();
        private List<Asistencia> asistencias = new();

        public Resultado RegistrarSocio(Socio socio)
        {
            foreach (var s in socios)
            {
                if (s.Documento == socio.Documento)
                {
                    return new Resultado { Success = false, Message = "El socio ya está registrado." };
                }
            }

            socios.Add(socio);
            return new Resultado { Success = true, Message = "Socio registrado con éxito." };
        }

        public Resultado RegistrarActividad(Actividad actividad)
        {
            foreach (var a in actividades)
            {
                if (a.Codigo == actividad.Codigo)
                {
                    return new Resultado { Success = false, Message = "La actividad ya existe." };
                }
            }

            actividades.Add(actividad);
            return new Resultado { Success = true, Message = "Actividad registrada con éxito." };
        }

        public Resultado InscribirSocio(string documentoSocio, string codigoActividad, DateTime fecha)
        {
            Socio socioEncontrado = null;
            foreach (var s in socios)
            {
                if (s.Documento == documentoSocio)
                {
                    socioEncontrado = s;
                    break;
                }
            }
            if (socioEncontrado == null)
            {
                return new Resultado { Success = false, Message = "Socio no encontrado." };
            }

            Actividad actividadEncontrada = null;
            foreach (var a in actividades)
            {
                if (a.Codigo == codigoActividad)
                {
                    actividadEncontrada = a;
                    break;
                }
            }
            if (actividadEncontrada == null)
            {
                return new Resultado { Success = false, Message = "Actividad no encontrada." };
            }

            if (socioEncontrado.NivelMembresia < actividadEncontrada.NivelRequerido)
            {
                return new Resultado { Success = false, Message = "El nivel de membresía no es suficiente." };
            }

            if (actividadEncontrada is ClaseGrupal)
            {
                int inscriptos = 0;
                foreach (var ins in inscripciones)
                {
                    if (ins.CodigoActividad == codigoActividad && !ins.Cancelada)
                    {
                        inscriptos++;
                    }
                }

                ClaseGrupal grupal = (ClaseGrupal)actividadEncontrada;
                if (inscriptos >= grupal.CupoMaximo)
                {
                    return new Resultado { Success = false, Message = "Cupo máximo alcanzado." };
                }
            }

            foreach (var ins in inscripciones)
            {
                if (ins.DocumentoSocio == documentoSocio && ins.CodigoActividad == codigoActividad && !ins.Cancelada)
                {
                    return new Resultado { Success = false, Message = "El socio ya está inscripto en esta actividad." };
                }
            }

            Inscripcion nuevaInscripcion = new Inscripcion
            {
                DocumentoSocio = documentoSocio,
                CodigoActividad = codigoActividad,
                FechaInscripcion = fecha,
                Cancelada = false
            };
            inscripciones.Add(nuevaInscripcion);

            return new Resultado { Success = true, Message = "Inscripción exitosa." };
        }

        public Resultado RegistrarAsistencia(string documentoSocio, string codigoActividad, DateTime fechaHora)
        {
            bool inscrito = false;

            foreach (var ins in inscripciones)
            {
                if (ins.DocumentoSocio == documentoSocio && ins.CodigoActividad == codigoActividad && !ins.Cancelada)
                {
                    inscrito = true;
                    break;
                }
            }

            if (!inscrito)
            {
                return new Resultado { Success = false, Message = "El socio no está inscripto en esta actividad." };
            }

            Asistencia nuevaAsistencia = new Asistencia
            {
                DocumentoSocio = documentoSocio,
                CodigoActividad = codigoActividad,
                FechaHora = fechaHora
            };
            asistencias.Add(nuevaAsistencia);

            return new Resultado { Success = true, Message = "Asistencia registrada." };
        }

        public List<AsistenciaDetalle> ConsultarAsistencias(string documento, DateTime desde, DateTime hasta)
        {
            List<AsistenciaDetalle> resultado = new List<AsistenciaDetalle>();

            for (int i = 0; i < asistencias.Count; i++)
            {
                Asistencia asistencia = asistencias[i];

                if (asistencia.DocumentoSocio == documento &&
                    asistencia.FechaHora.Date >= desde.Date &&
                    asistencia.FechaHora.Date <= hasta.Date)
                {
                    for (int j = 0; j < actividades.Count; j++)
                    {
                        Actividad actividad = actividades[j];

                        if (actividad.Codigo == asistencia.CodigoActividad)
                        {
                            AsistenciaDetalle detalle = new AsistenciaDetalle();
                            detalle.Fecha = asistencia.FechaHora;
                            detalle.NombreActividad = actividad.Nombre;
                            detalle.Profesor = actividad.Profesor;
                            detalle.Descripcion = actividad.ObtenerDescripcion();

                            resultado.Add(detalle);
                            break;
                        }
                    }
                }
            }

            return resultado;
        }

        public List<Socio> ConsultarSociosPorActividad(string codigoActividad)
        {
            List<Socio> resultado = new();

            foreach (var ins in inscripciones)
            {
                if (ins.CodigoActividad == codigoActividad && !ins.Cancelada)
                {
                    foreach (var socio in socios)
                    {
                        if (socio.Documento == ins.DocumentoSocio)
                        {
                            bool yaAgregado = false;
                            foreach (var s in resultado)
                            {
                                if (s.Documento == socio.Documento)
                                {
                                    yaAgregado = true;
                                    break;
                                }
                            }

                            if (!yaAgregado)
                            {
                                resultado.Add(socio);
                            }
                        }
                    }
                }
            }

      
            for (int i = 0; i < resultado.Count - 1; i++)
            {
                for (int j = i + 1; j < resultado.Count; j++)
                {
                    if (string.Compare(resultado[i].NombreCompleto, resultado[j].NombreCompleto) > 0)
                    {
                        Socio temp = resultado[i];
                        resultado[i] = resultado[j];
                        resultado[j] = temp;
                    }
                }
            }

            return resultado;
        }

        public Resultado CancelarInscripcion(string documento, string codigoActividad)
        {
            foreach (var ins in inscripciones)
            {
                if (ins.DocumentoSocio == documento && ins.CodigoActividad == codigoActividad && !ins.Cancelada)
                {
                    ins.Cancelada = true;
                    return new Resultado { Success = true, Message = "Inscripción cancelada." };
                }
            }

            return new Resultado { Success = false, Message = "No existe la inscripción activa." };
        }
    }
}
