using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Caracteristica
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public Caracteristica(string nombre, int cantidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
        }

        public void Restar()
        {
            if (Cantidad > 0)
            {
                Cantidad--;
            }
        }

        public void Sumar()
        {
            Cantidad++;
        }
    }
}
