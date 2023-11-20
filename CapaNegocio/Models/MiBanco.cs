using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Models
{
    public class MiBanco
    {
        public string IdCedula { get; set; }
        public string NombreCompleto { get; set; }
        public string IdNTarjeta { get; set; }
        public decimal Saldo { get; set; }
    }
}
