using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;


namespace Proyecto_DreamPlace.Paginas
{
    public partial class ManteAlojamiento : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correoSession = Session["Correo"].ToString();
                    BD.CargarNombresInmuebles(ddlAlojamientos, correoSession);
                    BD.CargarCategorias(ddlCategoria);
                    CargarImagenes();

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        public void CargarImagenes()
        {
            int ID = 39;
            List<byte[]> listaimagenes = ConexionBD.ObtenerImagenesPorIdInmueble(ID);
            int limite = Math.Min(listaimagenes.Count, 5);
            for (int i = 0; i < limite; i++)
            {
                string imagenid = "Image" + (i + 1);
                System.Web.UI.WebControls.Image ImageC = FindControl(imagenid) as System.Web.UI.WebControls.Image;
                if (ImageC != null)
                {
                    ImageC.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(listaimagenes[i]);
                    ImageC.CssClass = "imagen-estilo";
                }
            }
        }
        protected void ddlAlojamientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreAlojamiento = ddlAlojamientos.SelectedValue;
            Inmueble inmueble = BD.ObtenerDetallesAlojamientos(nombreAlojamiento);

            if (inmueble != null)
            {
                txtNombre.Text = inmueble.Nombre;
                txtDescripcion.Text = inmueble.Descripcion;
                txtCantidadP.Text = inmueble.CantidadPersonas.ToString();
                txtCantidadD.Text = inmueble.CantidadDormitorios.ToString();
                txtCantidadB.Text = inmueble.CantidadBanos.ToString();
                txtCantiCamas.Text = inmueble.CantidadCamas.ToString();

                BD.CargarCategoriasInmueble(ddlCategoria, nombreAlojamiento);
                BD.CargarCategorias(ddlCategoria);

                BD.CargarEstadosInmueble(ddlEstado, nombreAlojamiento);
                BD.CargarEstados(ddlEstado);

                txtDescripcionEstado.Text = inmueble.DescripcionEstado;

                // Mostrar la imagen
                if (inmueble.Imagen != null && inmueble.Imagen.Length > 0)
                {
                    Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(inmueble.Imagen);
                    Image1.CssClass = "imagen-estilo";

                    Image2.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(inmueble.Imagen);
                    Image2.CssClass = "imagen-estilo";

                    Image3.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(inmueble.Imagen);
                    Image3.CssClass = "imagen-estilo";

                    Image4.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(inmueble.Imagen);
                    Image4.CssClass = "imagen-estilo";

                    Image5.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(inmueble.Imagen);
                    Image5.CssClass = "imagen-estilo";

                   
                }
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private int ObtenerIdInmuebleSeleccionado()
        {
            int idInmueble = 0;

            if (ddlAlojamientos.SelectedIndex != -1)
            {
                // Intentar convertir el valor seleccionado a un entero
                if (int.TryParse(ddlAlojamientos.SelectedValue, out idInmueble))
                {
                    // La conversión fue exitosa
                    return idInmueble;
                }
            }

            return idInmueble;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener el ID del inmueble seleccionado
            int idInmueble = ObtenerIdInmuebleSeleccionado();

            // Validar que se haya seleccionado un inmueble
            if (idInmueble > 0)
            {
                // Obtener los valores actualizados desde los controles
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                int personas = Convert.ToInt32(txtCantidadP.Text);
                int dormitorios = Convert.ToInt32(txtCantidadD.Text);
                int banos = Convert.ToInt32(txtCantidadB.Text);
                int camas = Convert.ToInt32(txtCantiCamas.Text);
                int idCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
                int idEstado = Convert.ToInt32(ddlEstado.SelectedValue);

                // Llamar al método de actualización
                ConexionBD.ActualizarInmueble(idInmueble, nombre, descripcion, personas, dormitorios, banos, camas, idCategoria, idEstado);
            }
        }
    }
}
