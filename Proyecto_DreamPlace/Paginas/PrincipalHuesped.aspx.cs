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
    public partial class PrincipalHuesped : System.Web.UI.Page
    {
        ConexionBD objConexion = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    CargarCategorias();
                    CargarTarjetasInmuebles();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            
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
        }
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
                string correo = Session["Correo"].ToString();
                informacionEnlace.NavigateUrl = $"Reserva.aspx?idInmueble={row["IdInmueble"]}&correo={HttpUtility.UrlEncode(correo)}"; // Especifica la URL de destino
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
                informacionEnlace.Controls.Add(lblNombreInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblTipoInmueble = new Label();
                lblTipoInmueble.Text = $"Tipo de Inmueble: {row["TipoInmueble"]}";
                informacionEnlace.Controls.Add(lblTipoInmueble);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                // Concatenar cantón y provincia con una coma
                string ubicacion = $"{row["Canton"]}, {row["Provincia"]}";

                Label lblUbicacion = new Label();
                lblUbicacion.Text = $"Ubicación: {ubicacion}";
                informacionEnlace.Controls.Add(lblUbicacion);
                informacionEnlace.Controls.Add(new LiteralControl("<br />"));

                Label lblPrecioTotal = new Label();
                lblPrecioTotal.Text = $"Precio Total por Noche: ₡ {row["Total"]}";
                informacionEnlace.Controls.Add(lblPrecioTotal);

                // Agregar el div de información al área clickeable de la tarjeta
                tarjeta.Controls.Add(informacionEnlace);

                // Agregar la tarjeta al contenedor de tarjetas en tu página ASP.NET
                contenedorTarjetas.Controls.Add(tarjeta);
            }
        }

    }
}