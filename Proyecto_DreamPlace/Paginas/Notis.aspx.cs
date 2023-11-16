using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using System.Data;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Notis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();

            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    DataTable Notificaciones = BD.ObtenerNotificacionesPorCorreo(correo);

                    gvNotificaciones.DataSource = Notificaciones;
                    gvNotificaciones.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

            }
        }
    }
}