using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Mensajes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    BD.CargarAnfitrionesEnDropDownList(ddlAnfitriones);
                    CargarMensajesEnRepeater();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarMensajesEnRepeater()
        {
            string correo = Session["Correo"].ToString();
            ConexionBD BD = new ConexionBD();
            List<CapaNegocio.Models.Mensajes> mensajes = BD.ObtenerMensajesPorCorreo(correo);
            rptMensajes.DataSource = mensajes;
            rptMensajes.DataBind();
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            string nombreCompletoSeleccionado = ddlAnfitriones.SelectedValue;
            string mensaje = txtMensaje.Text;
            string correo = Session["Correo"].ToString();

            if (!string.IsNullOrEmpty(nombreCompletoSeleccionado) && !string.IsNullOrEmpty(mensaje) && !string.IsNullOrEmpty(correo))
            {
                try
                {
                    BD.InsertarMensajeSP(nombreCompletoSeleccionado, mensaje, correo);
                    CargarMensajesEnRepeater();
                    txtMensaje.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    Response.Write("Error al insertar el mensaje: " + ex.Message);
                }
            }
            else
            {
                Response.Write("Nombre, mensaje o correo está vacío o nulo.");
            }
        }
    }
}