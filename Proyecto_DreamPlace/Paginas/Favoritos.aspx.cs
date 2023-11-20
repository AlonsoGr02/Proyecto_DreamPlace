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
                    //CargarTarjetasInmueblesFav();
                    GenerarEstructuraHTML(ced);
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void CargarTarjetasInmuebles()
        {
            string correo = Session["Correo"].ToString();
            string idCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
            DataTable dtInmuebles = objConexion.ObtenerInmueblesFavoritos(idCedula);

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjetaFav";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carouselFav";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    img.Style.Add("max-width", "100%");
                    img.Style.Add("height", "auto");
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacionFav";
                informacionEnlace.Style.Add("font-family", "'Montserrat', sans-serif");
                informacionEnlace.NavigateUrl = $"Login.aspx?"; // Especifica la URL de destino
                informacionEnlace.Style.Add("text-decoration", "none"); // Eliminar subrayado
                informacionEnlace.Style.Add("color", "#333");

                // Agregar la información (puedes personalizar esto según tus necesidades)
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblIdInmueble = new Label();
                lblIdInmueble.Visible = false;
                lblIdInmueble.Text = row["IdInmueble"].ToString();
                informacionEnlace.Controls.Add(lblIdInmueble);

                Label lblNombreInmueble = new Label();
                lblNombreInmueble.Text = row["NombreInmueble"].ToString();
                lblNombreInmueble.Style.Add("font-weight", "bold");
                lblNombreInmueble.Style.Add("margin-bottom", "8px");

                informacionEnlace.Controls.Add(lblNombreInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblTipoInmueble = new Label();
                lblTipoInmueble.Text = $"Tipo de Inmueble: {row["TipoInmueble"]}";
                Image iconoE = new Image();
                iconoE.ImageUrl = "~/img/estrella.png";
                iconoE.Style.Add("width", "16px");
                iconoE.Style.Add("height", "16px");
                iconoE.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(iconoE);
                informacionEnlace.Controls.Add(lblTipoInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                // Concatenar cantón y provincia con una coma
                string ubicacion = $"{row["Canton"]}, {row["Provincia"]}";

                Label lblUbicacion = new Label();
                lblUbicacion.Text = $"Ubicación: {ubicacion}";

                Image icono = new Image();
                icono.ImageUrl = "~/img/gps.png";
                icono.Style.Add("width", "16px");
                icono.Style.Add("height", "16px");
                icono.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(icono);

                informacionEnlace.Controls.Add(lblUbicacion);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblPrecioTotal = new Label();
                lblPrecioTotal.Text = $"Precio por Noche: ₡ {row["Total"]}";
                Image iconoP = new Image();
                iconoP.ImageUrl = "~/img/dolar.png";
                iconoP.Style.Add("width", "16px");
                iconoP.Style.Add("height", "16px");
                iconoP.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(iconoP);
                informacionEnlace.Controls.Add(lblPrecioTotal);

                // Agregar espacio entre las etiquetas
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                // Agregar margen izquierdo a todas las etiquetas dentro del HyperLink
                foreach (Control control in informacionEnlace.Controls)
                {
                    if (control is Label)
                    {
                        ((Label)control).Style.Add("margin-left", "10px"); // Puedes ajustar el valor según tus necesidades
                    }
                }

                // Agregar el div de información al área clickeable de la tarjeta
                tarjeta.Controls.Add(informacionEnlace);

                // Agregar la tarjeta al contenedor de tarjetas en tu página ASP.NET
                //contenedorTarjetas.Controls.Add(tarjeta);
            }
        }

        protected void CargarTarjetasInmueblesFav()
        {
            string correo = Session["Correo"].ToString();
            string idCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);
            DataTable dtInmuebles = objConexion.ObtenerInmueblesFavoritos(idCedula);

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjetaFav";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carouselFav";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    img.CssClass = "imagenInmueble"; // Agrega una clase para aplicar estilos si es necesario
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacionFav";
                informacionEnlace.Style.Add("font-family", "'Montserrat', sans-serif");
                informacionEnlace.NavigateUrl = $"Login.aspx?"; // Especifica la URL de destino
                informacionEnlace.Style.Add("text-decoration", "none"); // Eliminar subrayado
                informacionEnlace.Style.Add("color", "#333");

                // Agregar la información (puedes personalizar esto según tus necesidades)
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblIdInmueble = new Label();
                lblIdInmueble.Visible = false;
                lblIdInmueble.Text = row["IdInmueble"].ToString();
                informacionEnlace.Controls.Add(lblIdInmueble);

                Label lblNombreInmueble = new Label();
                lblNombreInmueble.Text = row["NombreInmueble"].ToString();
                lblNombreInmueble.Style.Add("font-weight", "bold");
                lblNombreInmueble.Style.Add("margin-bottom", "8px");

                informacionEnlace.Controls.Add(lblNombreInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblTipoInmueble = new Label();
                lblTipoInmueble.Text = $"Tipo de Inmueble: {row["TipoInmueble"]}";
                Image iconoE = new Image();
                iconoE.ImageUrl = "~/img/estrella.png";
                iconoE.Style.Add("width", "16px");
                iconoE.Style.Add("height", "16px");
                iconoE.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(iconoE);
                informacionEnlace.Controls.Add(lblTipoInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                // Concatenar cantón y provincia con una coma
                string ubicacion = $"{row["Canton"]}, {row["Provincia"]}";

                Label lblUbicacion = new Label();
                lblUbicacion.Text = $"Ubicación: {ubicacion}";

                Image icono = new Image();
                icono.ImageUrl = "~/img/gps.png";
                icono.Style.Add("width", "16px");
                icono.Style.Add("height", "16px");
                icono.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(icono);

                informacionEnlace.Controls.Add(lblUbicacion);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblPrecioTotal = new Label();
                lblPrecioTotal.Text = $"Precio por Noche: ₡ {row["Total"]}";
                Image iconoP = new Image();
                iconoP.ImageUrl = "~/img/dolar.png";
                iconoP.Style.Add("width", "16px");
                iconoP.Style.Add("height", "16px");
                iconoP.Style.Add("margin-left", "10px");
                informacionEnlace.Controls.Add(iconoP);
                informacionEnlace.Controls.Add(lblPrecioTotal);

                // Agregar espacio entre las etiquetas
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                // Agregar margen izquierdo a todas las etiquetas dentro del HyperLink
                foreach (Control control in informacionEnlace.Controls)
                {
                    if (control is Label)
                    {
                        ((Label)control).Style.Add("margin-left", "10px"); // Puedes ajustar el valor según tus necesidades
                    }
                }

                // Agregar el div de información al área clickeable de la tarjeta
                tarjeta.Controls.Add(informacionEnlace);

                // Agregar la tarjeta al contenedor de tarjetas en tu página ASP.NET
                ///contenedorTarjetas.Controls.Add(tarjeta);
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