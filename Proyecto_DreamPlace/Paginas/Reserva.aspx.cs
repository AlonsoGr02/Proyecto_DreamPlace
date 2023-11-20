using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio.Models;
using Newtonsoft.Json;
using System.Web.Optimization;

namespace Proyecto_DreamPlace
{
    public partial class Reserva : System.Web.UI.Page
    {
        private int idInmueble;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["IdInmueble"], out idInmueble))
                {
                    string correo = Request.QueryString["Correo"];
                    Session["IdInmueble"] = idInmueble;

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

                    string[] datosInmueblePrecio = ConexionBD.ObtenerPrecioInmueble(idInmueble);
                    lblPrecio.Text = datosInmueblePrecio[0];


                    List<object> listaServicios = ConexionBD.ObtenerServicios(idInmueble).Cast<object>().ToList();

                    rptAmenities.DataSource = listaServicios;
                    rptAmenities.DataBind();

                    List<string> fechasReservadas = ConexionBD.ObtenerFechasReservadas(idInmueble);

                    CalendarUpdate(DateTime.Today.Year, DateTime.Today.Month);

                }
            }
        }

        protected void PrevButton_Click(object sender, EventArgs e)
        {
            DateTime current = calendar.VisibleDate;
            CalendarUpdate(current.AddMonths(-1).Year, current.AddMonths(-1).Month);
        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            DateTime current = calendar.VisibleDate;
            CalendarUpdate(current.AddMonths(1).Year, current.AddMonths(1).Month);
        }

        protected void CalendarUpdate(int year, int month)
        {
            calendar.VisibleDate = new DateTime(year, month, 1);

        }

        protected void calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime currentDay = e.Day.Date;

            if (Session["IdInmueble"] != null && int.TryParse(Session["IdInmueble"].ToString(), out idInmueble))
            {
                List<string> fechasReservadas = ConexionBD.ObtenerFechasReservadas(idInmueble);

                if (fechasReservadas.Contains(currentDay.ToString("yyyy-MM-dd")))
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#E53935");
                }
                else
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#A3AB78");
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

                DateTime fechaLlegada, fechaSalida;
                if (DateTime.TryParse(txtfechaLlegada.Text, out fechaLlegada) && DateTime.TryParse(txtfechaSalida.Text, out fechaSalida))
                {
                    Session["FechaLlegada"] = fechaLlegada;
                    Session["FechaSalida"] = fechaSalida;

                    string correo = Request.QueryString["Correo"];
                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

                    bool fechasReservadas = ConexionBD.FechasReservadasExisten(fechaLlegada, fechaSalida, idInmueble);

                    if (!fechasReservadas)
                    {
                        Response.Redirect($"Solicitud_Reserva.aspx?IdInmueble={idInmueble}&Correo={HttpUtility.UrlEncode(correo)}");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalError();", true);
                    }
                }
                else
                {
                    lblMensajeError.Text = "Por favor, introduce fechas válidas.";
                }
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

        protected void btnFavorritos_Click(object sender, EventArgs e)
        {
            int idInmueble;
            if (int.TryParse(Request.QueryString["IdInmueble"], out idInmueble))
            {
                Session["IdInmueble"] = idInmueble;
            }

            string correo = Session["Correo"].ToString();
            string idCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

            ConexionBD objConexion = new ConexionBD();
            objConexion.InsertarFavorito(idCedula, idInmueble);
        }
    }
}