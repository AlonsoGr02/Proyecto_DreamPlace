using CapaNegocio.Models;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Denuncias : System.Web.UI.Page
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


        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {
           
                
                string denunciaSeleccionada = Request.Form["denunciaSeleccionada"];
            if (!string.IsNullOrEmpty(denunciaSeleccionada))
            {
                string correo = Session["Correo"].ToString();

                string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

                ConexionBD.InsertarDenuncia(denunciaSeleccionada, IdCedula, IdInmueDenuncia);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Denuncia seleccionada: {denunciaSeleccionada}');", true);
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, selecciona una denuncia.');", true);
            }
        }

      
    }
}