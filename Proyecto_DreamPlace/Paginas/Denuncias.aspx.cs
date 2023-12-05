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
        private string nombre;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    ConexionBD BD = new ConexionBD();

                    string correo = Session["Correo"].ToString();

                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);


                    DataTable lista = BD.Obtener_ReservasDenuncia_IdCedula(IdCedula);

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

        protected void btnDenunciar_Click(object sender, EventArgs e)
        {
            Button btnDenunciar = sender as Button;

            if (btnDenunciar != null)
            {
                int idReserva = Convert.ToInt32(btnDenunciar.CommandArgument);
                Session["IdReserva"] = idReserva;
                ConexionBD bD = new ConexionBD();

                DataTable reservas = bD.Obtener_Reservas_IdReserva(idReserva);

                if (reservas.Rows.Count > 0)
                {
                    string nombreInmueble = reservas.Rows[0]["Nombre"].ToString();
                    Session["NombreInmuebleSeleccionado"] = nombreInmueble;

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "MostrarModal", $"AbrirModal({idReserva});", true);

            }
        }

        protected void btnEnviarDenuncia_Click(object sender, EventArgs e)
        {
            int Id= Convert.ToInt32(Session["IdReserva"]);
            ConexionBD bD = new ConexionBD();
            if (Session["NombreInmuebleSeleccionado"] != null)
            {
                string nombreInmueble = Session["NombreInmuebleSeleccionado"].ToString();

                IdInmueDenuncia = bD.ObtenerIdInmueblePorNombre(nombreInmueble);
                string denunciaSeleccionada = Request.Form["denunciaSeleccionada"];


                if (!string.IsNullOrEmpty(denunciaSeleccionada))
                {
                    string correo = Session["Correo"].ToString();
                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

                    ConexionBD.InsertarDenuncia(denunciaSeleccionada, IdCedula, Id);

                    //*** Correo***
                    string cuerpoMensajeDenuncia = "¡Hola!\n\n" +
                           "Hemos recibido una denuncia relacionada con tu inmueble en DreamPlace.\n\n" +
                           "Puedes revisar dicha denuncia en nuestra plataforma.\n\n" +
                           "Equipo de DreamPlace";

                    string asuntoDenuncia = "Denuncia en tu Inmueble - DreamPlace";

                    ConexionBD conexionBD = new ConexionBD();
                    string correoDesti = conexionBD.ObtenerCorreoAnfitrionPorIdInmueble(IdInmueDenuncia);

                    Metodos metodos = new Metodos();
                    metodos.EnviarCorreoPersonalizado(correoDesti, asuntoDenuncia, cuerpoMensajeDenuncia);
                    // fin de correo********

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Denuncia seleccionada: {denunciaSeleccionada}');", true);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Por favor, selecciona una denuncia.');", true);
                }
            }
        }          
    }
}