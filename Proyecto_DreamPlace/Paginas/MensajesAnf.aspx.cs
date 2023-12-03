using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
                    string IdCuedulaUsuario= ConexionBD.ObtenerIdCedulaPorCorreo(correo);
                    Session["IdCedula"]= IdCuedulaUsuario;

                    BD.CargarNombresInmuebles(ddlAlojamientos, correo);

                    
                    string correoAnfitrionSeleccionado = ddlhuesped.SelectedValue;

                    

                    string IdCuedulaUsuar = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
                    string nombreCompletoSeleccionado = ddlhuesped.SelectedValue;
                    string IdCedulaAnfitrion = BD.ObtenerIdCedulaPorN(nombreCompletoSeleccionado);

                    // Aquí deberías llamar a un método que obtenga los mensajes entre el usuario y el anfitrión seleccionado
                    List<CapaNegocio.Models.Mensajes> mensajes = BD.ObtenerMensajesEntreUsuarios(IdCuedulaUsuar, IdCedulaAnfitrion);

                    // Lógica para mostrar los mensajes en el control deseado (Repeater, ListView, etc.)
                    rptMensajes.DataSource = mensajes;
                    rptMensajes.DataBind();
                    ddlhuesped.ClearSelection();

                    rptMensajes.ItemDataBound += rptMensajes_ItemDataBound;
                    CargarMensajesEnRepeater();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void ddlAlojamientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();

            string nombre = ddlAlojamientos.SelectedValue;
            int IdInmueble = BD.ObtenerIdInmueblePorNombre(nombre);

            object IdCedulaObj = BD.ObtenerIdFechaReservada(IdInmueble);
            string IdCedula = IdCedulaObj != null ? IdCedulaObj.ToString() : string.Empty;

            BD.Huespedes(ddlhuesped, IdCedula);

            
        }        

        protected void ddlhuesped_SelectedIndexChanged(object sender, EventArgs e)
        {
            string correo = Session["Correo"].ToString();
            string correoAnfitrionSeleccionado = ddlhuesped.SelectedValue; 

            ConexionBD BD = new ConexionBD();

            string IdCuedulaUsuario = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
            string nombreCompletoSeleccionado = ddlhuesped.SelectedValue;
            string IdCedulaAnfitrion = BD.ObtenerIdCedulaPorN(nombreCompletoSeleccionado);

            // Aquí deberías llamar a un método que obtenga los mensajes entre el usuario y el anfitrión seleccionado
            List<CapaNegocio.Models.Mensajes> mensajes = BD.ObtenerMensajesEntreUsuarios(IdCuedulaUsuario, IdCedulaAnfitrion);

            // Lógica para mostrar los mensajes en el control deseado (Repeater, ListView, etc.)
            rptMensajes.DataSource = mensajes;
            rptMensajes.DataBind();
            

        }

        private void CargarMensajesEnRepeater()
        {
            string correo = Session["Correo"].ToString();
            string correoAnfitrionSeleccionado = ddlhuesped.SelectedValue;

            ConexionBD BD = new ConexionBD();

            string IdCuedulaUsuario = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
            string nombreCompletoSeleccionado = ddlhuesped.SelectedValue;
            string IdCedulaAnfitrion = BD.ObtenerIdCedulaPorNombre(nombreCompletoSeleccionado);

            
            List<CapaNegocio.Models.Mensajes> mensajes = BD.ObtenerMensajesEntreUsuarios(IdCuedulaUsuario, IdCedulaAnfitrion);
            
            rptMensajes.DataSource = mensajes;
            rptMensajes.DataBind();
        }
        protected void rptMensajes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string correo = Session["Correo"].ToString();
            string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

            Session["IdCedula"] = IdCedula;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel panelMensaje = (Panel)e.Item.FindControl("panelMensaje");
                CapaNegocio.Models.Mensajes mensaje = (CapaNegocio.Models.Mensajes)e.Item.DataItem;

                if (mensaje.IdCedula == Session["IdCedula"].ToString())
                {
                    panelMensaje.CssClass = "mensaje-derecha";
                }
                else
                {
                    panelMensaje.CssClass = "mensaje-izquierda";
                }
            }
        }

        protected string ObtenerClaseMensaje(string idCedula)
        {
            string correo = Session["Correo"].ToString();
            string idCedulaSesion = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

            return idCedula == idCedulaSesion ? "mensaje-derecha" : "mensaje-izquierda";
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            string nombreCompletoSeleccionado = ddlhuesped.SelectedValue;
            string mensaje = txtMensaje.Text;
            string correo = Session["Correo"].ToString();
            string IdCuedulaUsuario = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
            DateTime Fecha = DateTime.Now;
            string IdCedulaAnfitrion = BD.ObtenerIdCedulaPorN(nombreCompletoSeleccionado);

            if (!string.IsNullOrEmpty(nombreCompletoSeleccionado) && !string.IsNullOrEmpty(mensaje) && !string.IsNullOrEmpty(correo))
            {
                try
                {
                    BD.Insert_Mensajes(mensaje, Fecha, IdCuedulaUsuario, IdCedulaAnfitrion);

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
