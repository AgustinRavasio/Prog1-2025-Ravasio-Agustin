using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaService
{
    public abstract class ItemMenu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public Enums.TipoCocina TipoCocina { get; set; }
        public bool EsVegetariano { get; set; }

        public virtual string DevolverDescripcion()
        {
            return $"El plato {Nombre} es de tipo {TipoCocina}, {(EsVegetariano ? "es vegetariano" : "no es vegetariano")} y cuesta ${Precio}.";
        }

        public abstract double DevolverTiempoPreparacionEnMinutos(int cantidadPorciones, Enums.MomentoDelDia momentoDia);
    }
} 