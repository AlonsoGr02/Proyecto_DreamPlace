using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Favoritos : System.Web.UI.Page
    {
        ConexionBD objConexion = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    string ced = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
                    GenerarEstructuraHTML(ced);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void GenerarEstructuraHTML(string idCedula)
        {
            DataTable dtInmuebles = objConexion.ObtenerInmueblesFavoritos(idCedula);

            foreach (DataRow row in dtInmuebles.Rows)
            {
                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                string nombreInmueble = row["NombreInmueble"].ToString();
                string tipoInmueble = row["TipoInmueble"].ToString();
                string provincia = row["Provincia"].ToString();
                string canton = row["Canton"].ToString();
                decimal total = Convert.ToDecimal(row["Total"]);

                // Obtener la lista de imágenes para el inmueble actual
                List<byte[]> imagenes = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Crear la estructura HTML para cada inmueble
                Panel tarjetaContainer = new Panel();
                tarjetaContainer.CssClass = "tarjeta";

                Panel carruselContainer = new Panel();
                carruselContainer.CssClass = "carousel";

                foreach (byte[] imagen in imagenes)
                {
                    Image img = new Image();
                    img.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imagen);
                    img.CssClass = "imagen-carousel";
                    carruselContainer.Controls.Add(img);
                }

                Panel infoContainer = new Panel();
                infoContainer.CssClass = "informacion-inmueble";
                infoContainer.Controls.Add(new LiteralControl($"<h2>{nombreInmueble}</h2>"));

                Panel tipoInmuebleContainer = new Panel();
                tipoInmuebleContainer.CssClass = "tipo-container";

                Image iconoTipoInmueble = new Image();
                iconoTipoInmueble.ImageUrl = "~/img/estrella.png";
                iconoTipoInmueble.CssClass = "iconoFav";
                tipoInmuebleContainer.Controls.Add(iconoTipoInmueble);

                LiteralControl tipoInmuebleControl = new LiteralControl($"<p>Tipo de Inmueble: {tipoInmueble}</p>");
                tipoInmuebleContainer.Controls.Add(tipoInmuebleControl);

                infoContainer.Controls.Add(tipoInmuebleContainer);

                // Agregar icono para "Ubicación"
                Panel ubicacionContainer = new Panel();
                ubicacionContainer.CssClass = "tipo-container";

                Image iconoUbicacion = new Image();
                iconoUbicacion.ImageUrl = "~/img/gps.png";
                iconoUbicacion.CssClass = "iconoFav";
                ubicacionContainer.Controls.Add(iconoUbicacion);

                LiteralControl ubicacionControl = new LiteralControl($"<p>Ubicación: {provincia}, {canton}</p>");
                ubicacionContainer.Controls.Add(ubicacionControl);

                infoContainer.Controls.Add(ubicacionContainer);

                // Agregar icono para "Precio por Noche"
                Panel precioContainer = new Panel();
                precioContainer.CssClass = "tipo-container";

                Image iconoPrecio = new Image();
                iconoPrecio.ImageUrl = "~/img/dolar.png";
                iconoPrecio.CssClass = "iconoFav";
                precioContainer.Controls.Add(iconoPrecio);

                LiteralControl precioControl = new LiteralControl($"<p>Precio por noche: ₡ {total}</p>");
                precioContainer.Controls.Add(precioControl);

                infoContainer.Controls.Add(precioContainer);

                tarjetaContainer.Controls.Add(carruselContainer);
                tarjetaContainer.Controls.Add(infoContainer);

                container.Controls.Add(tarjetaContainer);
            }
        }
    }
}