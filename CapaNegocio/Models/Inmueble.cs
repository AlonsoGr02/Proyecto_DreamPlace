using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class Inmueble
    {
        public int IdInmueble { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CantidadPersonas { get; set; }
        public int CantidadDormitorios { get; set; }
        public int CantidadBanos { get; set; }
        public int CantidadCamas { get; set; }
        public int IdCategoria { get; set; }
        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; }
    }
}
