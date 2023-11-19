using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class LoginMiBanco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSoliCodigo_Click(object sender, EventArgs e)
        {
            string correo = txtcorreo.Text;

            if (ValidarCorreo(correo))
            {
                ConexionBD objConexion = new ConexionBD();
                Metodos objMetodos = new Metodos();

                string clave = objMetodos.GenerarCodigoNumerico();
                objConexion.ActualizarClaveUsuario(clave, correo);
                objMetodos.EnviarCorreo(correo, clave);
                lblRespu.Text = "El código de verificación fue enviado al correo " + correo;
            }
            else
            {
                lblRespu.Text = "El correo proporcionado no está registrado en la base de datos.";
            }
        }
        private bool ValidarCorreo(string correo)
        {
            ConexionBD objConexion = new ConexionBD();
            return objConexion.ExisteCorreo(correo);
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ConexionBD Mante = new ConexionBD();
            string correo = txtcorreo.Text;
            string codigo = txtcodigoVerificion.Text;

            if (ValidarLogin(correo, codigo))
            {
                // Realiza la lógica de obtención del IdRol (asumo que tienes una función para obtener el IdRol del usuario)
                int idRol = Mante.ObtenerIdRol(correo); // Ajusta esta línea según tu implementación

                // Almacena el IdRol en la sesión para poder acceder a él en la página de destino
                Session["IdRol"] = idRol;

                string CorreoSession = txtcorreo.Text;
                Session["Correo"] = CorreoSession;
                lblRespu.Text = "Inicio de sesión exitoso";

                // Redirige según el IdRol
                if (idRol == 1)
                {
                    Response.Redirect("InicioMiBanco.aspx?Correo=" + CorreoSession);
                }
                else if (idRol == 2)
                {
                    Response.Redirect("InicioMiBancoAnf.aspx?Correo=" + CorreoSession);
                }
                else
                {
                    // Maneja cualquier otro caso o muestra un mensaje de error
                    lblRespu.Text = "Rol no reconocido";
                }
            }
            else
            {
                lblRespu.Text = "Código de verificación incorrecto";
            }
        }

        private bool ValidarLogin(string correo, string codigo)
        {
            ConexionBD objConexion = new ConexionBD();

            return objConexion.ValidarLogin(correo, codigo);
        }

    }
}