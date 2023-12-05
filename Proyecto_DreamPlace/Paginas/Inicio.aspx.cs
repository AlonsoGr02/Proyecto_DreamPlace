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
    public partial class Inicio : System.Web.UI.Page
    {
        ConexionBD objConexion = new ConexionBD();
        Conexion2 objConexion2 = new Conexion2();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnLFamosos.Visible = false;
            CargarCategorias();
            CargarTarjetasInmuebles();
        }

        private void CargarCategorias()
        {
            DataTable categorias = objConexion.ObtenerCategoriasActivas();
            categorias.Columns.Add("ImagenBase64", typeof(string));

            foreach (DataRow row in categorias.Rows)
            {
                byte[] iconoBytes = (byte[])row["Icono"];
                string iconoBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(iconoBytes);
                row["ImagenBase64"] = iconoBase64;
            }

            repeaterCategorias.DataSource = categorias;
            repeaterCategorias.DataBind();
        }  // Fin metodo CargarCategorias

        protected void CargarTarjetasInmuebles()
        {
            DataTable dtInmuebles = objConexion.ObtenerInfoInmueblesMain();

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjeta";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carousel";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacion";
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
                contenedorTarjetas.Controls.Add(tarjeta);
            }
        }

        private void FiltrarTarjetasInmuebles(int idCategoria)
        {
            // Limpiar los controles existentes en el contenedor
            contenedorTarjetas.Controls.Clear();

            DataTable dtInmuebles = objConexion.ObtenerInmueblesPorCategoria(idCategoria);

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjeta";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carousel";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacion";
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

                // Agregar el div de información a la tarjeta
                tarjeta.Controls.Add(informacionEnlace);

                // Agregar la tarjeta al contenedor de tarjetas en tu página ASP.NET
                contenedorTarjetasFiltradas.Controls.Add(tarjeta);
            }
        }

        private void FiltrarTarjetasInmueblesNombre(string nombreIn)
        {
            // Limpiar los controles existentes en el contenedor
            contenedorTarjetas.Controls.Clear();

            DataTable dtInmuebles = objConexion.ObtenerInmueblesPorNombre(nombreIn);

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjeta";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carousel";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacion";
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

                // Agregar el div de información a la tarjeta
                tarjeta.Controls.Add(informacionEnlace);

                // Agregar la tarjeta al contenedor de tarjetas en tu página ASP.NET
                contenedorTarjetasFiltradas.Controls.Add(tarjeta);
            }
        }

        protected void btnLFamosos_Click(object sender, EventArgs e)
        {
            CargarInmueblesBaratos();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HiddenFieldIdCategoria.Value))
            {
                // Limpiar los controles existentes en el contenedor
                contenedorTarjetasFiltradas.Controls.Clear();

                int idCategoria = Convert.ToInt32(HiddenFieldIdCategoria.Value);
                FiltrarTarjetasInmuebles(idCategoria);
            }
            if (!string.IsNullOrEmpty(txtBusqueda.Value))
            {
                // Limpiar los controles existentes en el contenedor
                contenedorTarjetasFiltradas.Controls.Clear();

                string nombreI = txtBusqueda.Value;
                FiltrarTarjetasInmueblesNombre(nombreI);
                txtBusqueda.Value ="";
            }

        }

        protected void CargarInmueblesBaratos()
        {
            DataTable dtInmuebles = objConexion2.ObtenerInmueblesBaratos();

            foreach (DataRow row in dtInmuebles.Rows)
            {
                // Crear una nueva tarjeta
                Panel tarjeta = new Panel();
                tarjeta.CssClass = "tarjeta";

                // Crear un div para el carrusel de imágenes
                Panel carousel = new Panel();
                carousel.CssClass = "carousel";

                int idInmueble = Convert.ToInt32(row["IdInmueble"]);
                List<byte[]> imagenesInmueble = objConexion.ObtenerImagenesInmueble(idInmueble);

                // Agregar cada imagen al carrusel
                foreach (byte[] imagenBytes in imagenesInmueble)
                {
                    Image img = new Image();
                    img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                    carousel.Controls.Add(img);
                }

                // Agregar el carrusel a la tarjeta
                tarjeta.Controls.Add(carousel);

                // Crear el div de información como un enlace (hipervínculo)
                HyperLink informacionEnlace = new HyperLink();
                informacionEnlace.CssClass = "informacion";
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
                contenedorTarjetas.Controls.Add(tarjeta);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inicio.aspx");
            CargarCategorias();
            CargarTarjetasInmuebles();
        }
    }
}