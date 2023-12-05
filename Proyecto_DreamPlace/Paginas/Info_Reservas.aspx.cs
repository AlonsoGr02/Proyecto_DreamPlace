using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Info_Reservas : System.Web.UI.Page
    {
        int IdInmueDenuncia;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    ConexionBD BD = new ConexionBD();

                    string correo = Session["Correo"].ToString();

                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);


                    DataTable lista = BD.Obtener_Reservas_IdCedula(IdCedula);

                    rptDenuncias.DataSource = lista;
                    rptDenuncias.DataBind();

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void rptDenuncias_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label labelNombreInmueble = (Label)e.Item.FindControl("LabelNombreInmueble");
                if (labelNombreInmueble != null)
                {
                    string nombreInmueble = labelNombreInmueble.Text;
                    ConexionBD bD = new ConexionBD();
                    IdInmueDenuncia = bD.ObtenerIdInmueblePorNombre(nombreInmueble);
                }
            }
        }
    }
}