using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Registrarme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblRespu.ForeColor = System.Drawing.Color.Red;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int opcionSeleccionada = Convert.ToInt32(seleccionarOp.Value); ;
            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(registerCedula.Text) ||
                string.IsNullOrWhiteSpace(registerUsername.Text) ||
                string.IsNullOrWhiteSpace(registerNombre.Text) ||
                string.IsNullOrWhiteSpace(registerApellidos.Text) ||
                string.IsNullOrWhiteSpace(registerFechaNac.Text) ||
                string.IsNullOrWhiteSpace(registerTelefono.Text) ||
                opcionSeleccionada <= 0)  // Validar que la opción seleccionada no sea menor o igual a cero
            {
                lblRespu.Text = "Todos los campos son obligatorios";
                
                return;
            }

            string cedula = registerCedula.Text;
            string correo = registerUsername.Text;
            string nombre = registerNombre.Text;
            string apellidos = registerApellidos.Text;
            string fechaNac = registerFechaNac.Text;
            string telefono = registerTelefono.Text;
            //string tipoUser = registerTipoUsuario.Text;
            //opcionSeleccionada = Convert.ToInt32(seleccionarOp.Value);
            int rol = opcionSeleccionada;
            byte[] imagenFrontal = null;
            byte[] imagenTrasera = null;

            DateTime fechaNacimiento;
            if (!DateTime.TryParse(fechaNac, out fechaNacimiento))
            {
                lblRespu.Text = "Formato de fecha incorrecto";
                return;
            }

            // Validar que la persona sea mayor de edad
            if (!EsMayorDeEdad(fechaNacimiento))
            {
                lblRespu.Text = "La persona debe ser mayor de edad para registrarse.";
                return;
            }

            // Validar tipos de datos y tamaños
            if (!EsTipoDeDatoValido(cedula, "char", 20) ||
                !EsTipoDeDatoValido(correo, "varchar", 100) ||
                !EsTipoDeDatoValido(nombre, "varchar", 50) ||
                !EsTipoDeDatoValido(apellidos, "varchar", 50) ||
                !EsTipoDeDatoValido(fechaNac, "date", -1) ||
                !EsTipoDeDatoValido(telefono, "varchar", 20))
            {
                lblRespu.Text = "Los tipos de datos no son válidos o los tamaños son incorrectos.";
                return;
            }

            // Validar que los archivos estén adjuntos siempre
            if (!FileUploadFrontal.HasFile || !FileUploadTrasera.HasFile)
            {
                lblRespu.Text = "Debe adjuntar ambos archivos";
                return;
            }

            if (FileUploadFrontal.HasFile)
            {
                imagenFrontal = ConvertirImagenABytes(FileUploadFrontal.PostedFile);
                lblInfo.Text = "Adjunta la foto trasera de la cédula";
                lblInfoTrasera.Text = "";
            }
            if (FileUploadTrasera.HasFile)
            {
                imagenTrasera = ConvertirImagenABytes(FileUploadTrasera.PostedFile);
            }

            ConexionBD objConexion = new ConexionBD();
            Metodos objMetodos = new Metodos();

            string clave = objMetodos.GenerarCodigoNumerico();
            objConexion.InsertarIdentificacion(cedula, nombre, apellidos, fechaNacimiento, telefono, imagenFrontal, imagenTrasera);
            objConexion.InsertarUsuario(correo, clave, rol, cedula);

            lblRespu.Text = "El registro se realizó con éxito";
        }

        private byte[] ConvertirImagenABytes(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] imagenEnBytes = new byte[file.ContentLength];
                file.InputStream.Read(imagenEnBytes, 0, file.ContentLength);
                return imagenEnBytes;
            }
            return null;
        }

        // Función para validar el tipo de dato y el tamaño
        private bool EsTipoDeDatoValido(string valor, string tipoDeDato, int tamanoMaximo)
        {
            switch (tipoDeDato.ToLower())
            {
                case "char":
                    return valor.Length <= tamanoMaximo;
                case "varchar":
                    return valor.Length <= tamanoMaximo;
                case "date":
                    DateTime fecha;
                    return DateTime.TryParse(valor, out fecha);
                default:
                    return false;
            }
        }

        private bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            // Calcular la edad comparando con la fecha actual
            int edad = DateTime.Today.Year - fechaNacimiento.Year;

            // Si el cumpleaños aún no ha pasado este año, restar 1 a la edad
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }
            return edad >= 18;
        }
    }
}