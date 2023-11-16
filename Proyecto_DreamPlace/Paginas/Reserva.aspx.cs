using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio.Models;


namespace Proyecto_DreamPlace
{
    public partial class Reserva : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            int idInmueble;

            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["IdInmueble"], out idInmueble))
                {
                    string correo = Request.QueryString["Correo"];


                    List<byte[]> listaImagenes = ConexionBD.ObtenerImagenesPorIdInmueble(idInmueble);


                    imageGallery.Controls.Clear();


                    for (int i = 0; i < listaImagenes.Count; i++)
                    {
                        var image = new System.Web.UI.WebControls.Image();
                        image.ID = "Image" + (i + 1);
                        image.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(listaImagenes[i]);
                        image.CssClass = "image-box";


                        imageGallery.Controls.Add(image);
                    }

                    string[] datosInmueble = ConexionBD.ObtenerDatosInmueblePorIdInmueble(idInmueble);


                    lblNombre.Text = datosInmueble[0];
                    lbldescripcion.Text = datosInmueble[1];
                    lblCantidadPersonas.Text = datosInmueble[2];
                    lblCantidadDormitorios.Text = datosInmueble[3];
                    lblCantidadBanos.Text = datosInmueble[4];
                    lblCantidadCamas.Text = datosInmueble[5];
                    lblTipo.Text = datosInmueble[6];

                    string[] datosInmueblePrecio = ConexionBD.ObtenerPrecioInmueble(idInmueble);
                    lblPrecio.Text = datosInmueblePrecio[0];


                    List<object> listaServicios = ConexionBD.ObtenerServicios(idInmueble).Cast<object>().ToList();

                    rptAmenities.DataSource = listaServicios;
                    rptAmenities.DataBind();

                }
            }
        }
        protected void AbrirModal(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModal();", true);

        }

        protected void CerrarModal(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hideModal", "CerrarModal();", true);
        }


        protected void RestarNinos(object sender, EventArgs e)
        {

        }

        protected void ReservarButton_Click(object sender, EventArgs e)
        {
            int idInmueble;
            if (int.TryParse(Request.QueryString["IdInmueble"], out idInmueble))
            {
                Session["IdInmueble"] = idInmueble;
                Session["CantidadAdultos"] = txtcantidadAdultos.Text;
                Session["FechaLlegada"] = txtfechaLlegada.Text;
                Session["FechaSalida"] = txtfechaSalida.Text;

                string correo = Request.QueryString["Correo"];

                Response.Redirect($"Solicitud_Reserva.aspx?IdInmueble={idInmueble}&Correo={HttpUtility.UrlEncode(correo)}");
            }

        }

        public string ObtenerIconClass(byte[] icono, string nombre)
        {
            if (icono != null && icono.Length > 0)
            {
                string base64String = Convert.ToBase64String(icono);
                return "icon " + "icon-" + nombre.ToLower() + " { background-image: url('data:image/png;base64," + base64String + "'); }";
            }
            else
            {
                return "icon icon-por-defecto";
            }
        }
    }
}