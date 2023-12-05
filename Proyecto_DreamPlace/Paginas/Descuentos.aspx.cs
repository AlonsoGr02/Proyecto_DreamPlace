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

            string nombreAlojamiento = ddlAlojamientos.SelectedValue;
            int idDescuento = Convert.ToInt32(ddlDescuento.SelectedValue);

            // Obtener el inmueble con descuento incluido
            Inmueble inmueble = BD.ObtenerDetallesAlojamientosConDescuento(nombreAlojamiento);

            if (inmueble != null)
            {
                // Obtener el ID del inmueble
                int idInmueble = inmueble.IdInmueble;

                // Ejecutar la actualización en la base de datos
                BD.ActualizarDescuentoEnInmueble(idInmueble, idDescuento);
            }

        }
        protected void ddlDescuento_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal precioTotal = ObtenerPrecioTotalActual();

            // Verificar si se obtuvo correctamente el precio total
            if (precioTotal != 0.0M)
            {
                // Obtener el descuento seleccionado
                decimal descuento = Convert.ToDecimal(ddlDescuento.SelectedValue) / 100;

                // Calcular el nuevo precio con el descuento
                decimal nuevoPrecioTotal = precioTotal - (precioTotal * descuento);

                // Actualizar el valor en el TextBox
                txtTotal.Text = nuevoPrecioTotal.ToString("C", CultureInfo.GetCultureInfo("es-CR"));

                // Registrar el script para actualizar la interfaz de usuario
                string script = "actualizarInterfazUsuario();";
                ScriptManager.RegisterStartupScript(this, GetType(), "ActualizarInterfazScript", script, true);
            }
            else
            {
                // Manejar el caso en el que no se pudo obtener el precio total
                // Puedes mostrar un mensaje de error u otra lógica según tus necesidades
            }
        }

        // Método para obtener el precio total actual del inmueble
        private decimal ObtenerPrecioTotalActual()
        {
            // Implementa lógica para obtener el precio total actual del inmueble
            // Puedes acceder a los controles de la página desde aquí para obtener los valores actuales.
            decimal precioTotal;
            if (decimal.TryParse(txtTotal.Text, out precioTotal))
            {
                return precioTotal;
            }
            return 0.0M;
        }


    }
}