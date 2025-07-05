using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaService
{
    public class Entrada : ItemMenu
    {
        public bool EsParaCompartir { get; set; }
        public int CantidadPersonasRecomendada { get; set; }

        public override double DevolverTiempoPreparacionEnMinutos(int cantidadPorciones, Enums.MomentoDelDia momentoDia)
        {
            double tiempoBase = 15; // Tiempo base de preparación en minutos
            double tiempoPorPorcion = 3; // Tiempo adicional por porción
            double multiplicadorMomento = 1.0;

            switch (momentoDia)
            {
                case Enums.MomentoDelDia.Almuerzo:
                    multiplicadorMomento = 1.1; // Ligero aumento en almuerzo
                    break;
                case Enums.MomentoDelDia.Cena:
                    multiplicadorMomento = 1.2; // Más tiempo en cena por mayor complejidad
                    break;
                default:
                    multiplicadorMomento = 1.0;
                    break;
            }

            return (tiempoBase + (cantidadPorciones * tiempoPorPorcion)) * multiplicadorMomento;
        }

        public override string DevolverDescripcion()
        {
            string descripcionBase = base.DevolverDescripcion();
            return $"{descripcionBase} {(EsParaCompartir ? $"Es para compartir entre {CantidadPersonasRecomendada} personas" : "Es una entrada individual")}.";
        }
    }
} 