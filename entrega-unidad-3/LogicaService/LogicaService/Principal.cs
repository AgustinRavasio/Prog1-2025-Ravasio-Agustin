using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaService
{
    public class Principal
    {
        private List<ItemMenu> ListaItemsMenu;

        public Principal()
        {
            ListaItemsMenu = new List<ItemMenu>();
        }

        public List<ItemMenu> DevolverListaItemsMenu()
        {
            return ListaItemsMenu;
        }

        public ResultadoValidacion CrearItemMenu(PlatoPrincipal plato)
        {
            ResultadoValidacion result = new ResultadoValidacion();
            ValidarCompletitudDatosPlatoPrincipal(ref result, plato);

            if (result.EsValido)
            {
                plato.Id = ListaItemsMenu.Count + 1;
                ListaItemsMenu.Add(plato);
            }

            return result;
        }

        public ResultadoValidacion CrearItemMenu(Entrada entrada)
        {
            ResultadoValidacion result = new ResultadoValidacion();
            ValidarCompletitudDatosEntrada(ref result, entrada);

            if (result.EsValido)
            {
                entrada.Id = ListaItemsMenu.Count + 1;
                ListaItemsMenu.Add(entrada);
            }

            return result;
        }

        public ItemMenu ObtenerItemMenu(int id)
        {
            return ListaItemsMenu.FirstOrDefault(x => x.Id == id && !x.FechaEliminacion.HasValue);
        }

        public ResultadoValidacion ActualizarItemMenu(int id, ItemMenu item)
        {
            ResultadoValidacion result = new ResultadoValidacion();

            ItemMenu itemGuardado = ObtenerItemMenu(id);
            if (itemGuardado == null)
            {
                result.EsValido = false;
                result.Errores.Add(new ResultadoValidacionItem
                {
                    Mensaje = "No se encontró el ítem del menú que se quiere editar"
                });
                return result;
            }

            ValidarCompletitudDatos(ref result, item);

            if (result.EsValido)
            {
                itemGuardado.Nombre = item.Nombre;
                itemGuardado.Descripcion = item.Descripcion;
                itemGuardado.Precio = item.Precio;
                itemGuardado.TipoCocina = item.TipoCocina;
                itemGuardado.EsVegetariano = item.EsVegetariano;
            }

            return result;
        }

        public string EliminarItemMenu(int id)
        {
            ItemMenu item = ObtenerItemMenu(id);

            if (item == null)
            {
                return "No se encontró el ítem del menú para eliminar";
            }

            item.FechaEliminacion = DateTime.Now;
            return "Ítem del menú eliminado con éxito.";
        }

        public string ObtenerDescripcionItemMenu(int id)
        {
            ItemMenu item = ObtenerItemMenu(id);

            if (item == null)
            {
                return $"No existe un ítem para el id: {id}";
            }

            return item.DevolverDescripcion();
        }

        public List<ItemMenu> FiltrarItemsPorTipoCocina(Enums.TipoCocina tipoCocina)
        {
            return ListaItemsMenu.Where(x => x.TipoCocina == tipoCocina && !x.FechaEliminacion.HasValue).ToList();
        }

        public List<ItemMenu> FiltrarItemsVegetarianos()
        {
            return ListaItemsMenu.Where(x => x.EsVegetariano && !x.FechaEliminacion.HasValue).ToList();
        }

        public double CalcularTiempoPreparacion(int id, int cantidadPorciones, Enums.MomentoDelDia momentoDia)
        {
            ItemMenu item = ObtenerItemMenu(id);

            if (item == null)
            {
                return 0.0;
            }

            return item.DevolverTiempoPreparacionEnMinutos(cantidadPorciones, momentoDia);
        }

        public decimal CalcularPrecioTotal(List<int> idsItems)
        {
            decimal precioTotal = 0;
            foreach (int id in idsItems)
            {
                ItemMenu item = ObtenerItemMenu(id);
                if (item != null)
                {
                    precioTotal += item.Precio;
                }
            }
            return precioTotal;
        }

        public List<ItemMenu> ObtenerItemsPorRangoPrecio(decimal precioMinimo, decimal precioMaximo)
        {
            return ListaItemsMenu.Where(x => x.Precio >= precioMinimo && 
                                           x.Precio <= precioMaximo && 
                                           !x.FechaEliminacion.HasValue).ToList();
        }        

        private void ValidarCompletitudDatos(ref ResultadoValidacion result, ItemMenu item)
        {
            result.EsValido = true;
            
            if (string.IsNullOrWhiteSpace(item.Nombre))
            {
                result.AgregarError("El nombre del ítem es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(item.Descripcion))
            {
                result.AgregarError("La descripción del ítem es obligatoria");
            }

            if (item.Precio <= 0)
            {
                result.AgregarError("El precio debe ser mayor a 0");
            }

            if (result.Errores.Count() > 0)
            {
                result.EsValido = false;
            }
        }

        private void ValidarCompletitudDatosPlatoPrincipal(ref ResultadoValidacion result, ItemMenu item)
        {
            result.EsValido = true;
            
            // Validar datos comunes
            ValidarCompletitudDatos(ref result, item);

            // Validar datos específicos de PlatoPrincipal
            PlatoPrincipal plato = item as PlatoPrincipal;
            if (plato.IncluyeGuarnicion && plato.IncluyePostre)
            {
                result.AgregarError("Un plato principal no puede incluir guarnición y postre simultáneamente");
            }

            if (result.Errores.Count() > 0)
            {
                result.EsValido = false;
            }
        }

        private void ValidarCompletitudDatosEntrada(ref ResultadoValidacion result, ItemMenu item)
        {
            result.EsValido = true;
            
            // Validar datos comunes
            ValidarCompletitudDatos(ref result, item);

            // Validar datos específicos de Entrada
            Entrada entrada = item as Entrada;
            if (entrada.EsParaCompartir && entrada.CantidadPersonasRecomendada <= 1)
            {
                result.AgregarError("Si la entrada es para compartir, debe especificar más de una persona");
            }

            if (result.Errores.Count() > 0)
            {
                result.EsValido = false;
            }
        }
    }
} 