using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Perfil_Huesped : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    Usuario usuario = BD.ObtenerDatosUsuario(correo);

                    if (usuario != null)
                    {
                        // Asignar los valores a los TextBox
                        txtCedula.Text = usuario.Cedula;
                        txtNombre.Text = usuario.Nombre;
                        txtApellidos.Text = usuario.Apellidos;
                        txtTelefono.Text = usuario.Telefono;
                        txtFechaNac.Text = usuario.FechaNac.ToString("yyyy-MM-dd");
                        txtCorreo.Text = usuario.Correo;
                        txtClave.Text = usuario.Clave;
                        txtRol.Text = usuario.Rol;
                    }
                    if (Request.QueryString["MostrarMensaje"] == "true")
                    {
                        MostrarMensaje("Cambios Realizados con Éxito");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();

            string CorreoSession = txtCorreo.Text;
            Session["Correo"] = CorreoSession;

            string cedula = txtCedula.Text;
            string nombre = txtNombre.Text;
            string apellidos = txtApellidos.Text;
            string telefono = txtTelefono.Text;

            BD.ActualizarUsuario(cedula, nombre, apellidos, telefono);
            Response.Redirect($"Perfil_Huesped.aspx?Correo={CorreoSession}&MostrarMensaje=true");
            MostrarMensaje("Cambios Realizados con Exito");


        }
        private void MostrarMensaje(string mensaje)
        {
            string script = $"Swal.fire('{mensaje}')";
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", script, true);
        }
    }
}
