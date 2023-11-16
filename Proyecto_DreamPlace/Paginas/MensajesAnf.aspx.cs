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
    public partial class MensajesAnf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            if (!IsPostBack)
            {

                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    BD.CargarHuespedEnDropDownList(ddlhuesped);
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

        protected void btnEnviarMensaje_Click(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            string nombre = ddlhuesped.SelectedValue;
            string mensaje = txtMensaje.Text;
            string correo = Session["Correo"].ToString();

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(mensaje))
            {
                BD.InsertarMensajeSP(nombre, mensaje, correo);
                CargarMensajesEnRepeater();

                txtMensaje.Text = string.Empty;

            }
            else
            {

            }
        }
    }
}
