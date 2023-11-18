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
                    BD.CargarAlojamientosEnDropDownList(ddlAlojamientos, correoSession);
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
            CargarDetallesAlojamiento();
        }


        private void CargarDetallesAlojamiento()
        {
            
            string correoUsuario = Session["Correo"].ToString();
            string nombreAlojamiento = ddlAlojamientos.SelectedValue;

            // Llamar al método para obtener detalles del alojamiento
            Inmueble detallesAlojamiento = BD.ObtenerDetallesAlojamientos(correoUsuario, nombreAlojamiento);

            if (detallesAlojamiento != null)
            {
                // Asignar los valores a los controles correspondientes
                txtNombre.Text = detallesAlojamiento.Nombre;
                txtDescripcion.Text = detallesAlojamiento.Descripcion;
                txtCantidadP.Text = detallesAlojamiento.CantidadPersonas.ToString();
                txtCantidadD.Text = detallesAlojamiento.CantidadDormitorios.ToString();
                txtCantidadB.Text = detallesAlojamiento.CantidadBanos.ToString();
                txtCantiCamas.Text = detallesAlojamiento.CantidadCamas.ToString();
                ddlCategoria.SelectedValue = detallesAlojamiento.IdCategoria.ToString();
                ddlEstado.SelectedValue = detallesAlojamiento.IdEstado.ToString();
                txtDescripcionEstado.Text = detallesAlojamiento.DescripcionEstado;
            }
        }
    }
}
