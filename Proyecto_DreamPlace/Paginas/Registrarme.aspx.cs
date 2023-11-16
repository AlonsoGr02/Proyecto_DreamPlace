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

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string cedula = registerCedula.Text;
            string correo = registerUsername.Text;
            string nombre = registerNombre.Text;
            string apellidos = registerApellidos.Text;
            string fechaNac = registerFechaNac.Text;
            string telefono = registerTelefono.Text;
            //string tipoUser = registerTipoUsuario.Text;
            int opcionSeleccionada = Convert.ToInt32(seleccionarOp.Value);
            int rol = opcionSeleccionada;
            byte[] imagenFrontal = null;
            byte[] imagenTrasera = null;

            //int idRol;
            DateTime fechaNacimiento;
            if (!DateTime.TryParse(fechaNac, out fechaNacimiento))
            {
                // Manejar error de conversión de fecha
                return;
            }

            //if (tipoUser == "Húesped")
            //{
            //    idRol = 1;
            //}
            //else
            //{
            //    idRol = 2;
            //}

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
    }
}