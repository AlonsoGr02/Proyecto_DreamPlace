using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;


namespace Proyecto_DreamPlace.Paginas
{
    public partial class Descuentos : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    BD.CargarNombresInmuebles(ddlAlojamientos, correo);
                    BD.CargarDescuentos(ddlDescuento);
                   
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                
            }
        }
        protected void ddlAlojamientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreAlojamiento = ddlAlojamientos.SelectedValue;
            decimal descuentoSeleccionado = Convert.ToDecimal(ddlDescuento.SelectedValue);

            Inmueble inmueble = BD.ObtenerDetallesAlojamientosConDescuento(nombreAlojamiento);

            if (inmueble != null)
            {
                // Aplicar el descuento al precio total
                inmueble.Total = inmueble.Total - (inmueble.Total * (descuentoSeleccionado / 100));

                // Mostrar detalles actualizados en la consola o agregar alertas para verificar
                Console.WriteLine($"Nombre: {inmueble.Nombre}, Descripción: {inmueble.Descripcion}, Total: {inmueble.Total}");

                // Mostrar los detalles actualizados en los controles TextBox
                txtNombre.Text = inmueble.Nombre;
                txtDescripcion.Text = inmueble.Descripcion;
                txtTotal.Text = inmueble.Total.ToString("C", new CultureInfo("es-CR"));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
       
    }
}