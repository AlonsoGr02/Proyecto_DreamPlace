using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{

    public class Servicio
    {
        public string Nombre { get; set; }
        public byte[] Icono { get; set; }
        public string IconClass { get; set; }
        public string ImagenBase64 => Convert.ToBase64String(Icono);
    }
}
