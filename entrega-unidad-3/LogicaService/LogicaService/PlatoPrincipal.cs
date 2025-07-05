using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaService
{
    public class PlatoPrincipal : ItemMenu
    {
        public bool IncluyeGuarnicion { get; set; }
        public bool IncluyePostre { get; set; }

        public override double DevolverTiempoPreparacionEnMinutos(int cantidadPorciones, Enums.MomentoDelDia momentoDia)
        {
            double tiempoBase = 30; // Tiempo base de preparación en minutos
            double tiempoPorPorcion = 5; // Tiempo adicional por porción
            double multiplicadorMomento = 1.0;

            switch (momentoDia)
            {
                case Enums.MomentoDelDia.Almuerzo:
                    multiplicadorMomento = 1.2; // Más tiempo en almuerzo por mayor demanda
                    break;
                case Enums.MomentoDelDia.Cena:
                    multiplicadorMomento = 1.1; // Tiempo moderado en cena
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
            return $"{descripcionBase} {(IncluyeGuarnicion ? "Incluye guarnición" : "No incluye guarnición")}. {(IncluyePostre ? "Incluye postre" : "No incluye postre")}.";
        }
    }
} 