using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class DenunciasAnf : System.Web.UI.Page
    {
        Conexion2 conexionBD = new Conexion2();
        ConexionBD conexion = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    string cedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
                    CargarDenuncias(cedula);

                    //gvDenuncias.DataSource = dtDenuncias;
                    //gvDenuncias.DataBind();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        private void CargarDenuncias(string cedula)
        {
            Conexion2 conexionBD = new Conexion2();
            List<Denuncia> denuncias = conexionBD.ObtenerDenunciasPorCedula(cedula);

            rptDenuncias.DataSource = denuncias;
            rptDenuncias.DataBind();
        }

    }
}