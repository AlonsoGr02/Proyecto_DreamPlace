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

        protected void Conectar_Click(object sender, EventArgs e)
        {
            string correo = Request.Form["idUsuario"];
            string codigo = Request.Form["password"];

            if (ValidarLogin(correo, codigo))
            {
                string CorreoSession = Request.Form["idUsuario"];
                Session["Correo"] = CorreoSession;
                lblMensaje.Text = "Inicio de sesión exitoso";

                Response.Redirect("InicioMiBanco.aspx?Correo=" + CorreoSession);
            }
            else
            {
                lblMensaje.Text = "Código de verificación incorrecto";
            }

        }

        protected void btnSoliCodigo_Click(object sender, EventArgs e)
        {
            string correo = Request.Form["idUsuario"];

            if (ValidarCorreo(correo))
            {
                ConexionBD objConexion = new ConexionBD();
                Metodos objMetodos = new Metodos();

                string clave = objMetodos.GenerarCodigoNumerico();
                objConexion.ActualizarClaveUsuario(clave, correo);
                objMetodos.EnviarCorreo(correo, clave);
                lblMensaje.Text = "El código de verificación fue enviado al correo " + correo;
            }
            else
            {
                lblMensaje.Text = "El correo proporcionado no está registrado en la base de datos.";
            }
        }

        private bool ValidarCorreo(string correo)
        {
            ConexionBD objConexion = new ConexionBD();
            return objConexion.ExisteCorreo(correo);
        }

        private bool ValidarLogin(string correo, string codigo)
        {
            ConexionBD objConexion = new ConexionBD();

            return objConexion.ValidarLogin(correo, codigo);
        }
    }
}