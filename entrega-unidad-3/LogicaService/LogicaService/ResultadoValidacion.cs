using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaService
{
    public class ResultadoValidacion
    {
        public bool EsValido { get; set; }
        public List<ResultadoValidacionItem> Errores { get; set; }

        public ResultadoValidacion()
        {
            EsValido = true;
            Errores = new List<ResultadoValidacionItem>();
        }

        public void AgregarError(string mensaje)
        {
            EsValido = false;
            Errores.Add(new ResultadoValidacionItem { Mensaje = mensaje });
        }
    }
} 