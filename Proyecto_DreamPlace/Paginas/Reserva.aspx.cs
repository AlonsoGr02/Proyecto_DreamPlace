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
using System.Data;
using System.Web.UI.HtmlControls;

namespace Proyecto_DreamPlace
{
    public partial class Reserva : System.Web.UI.Page
    {
        private int idInmueble;
        ConexionBD BD= new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblubucacion.Visible = false;

                if (int.TryParse(Request.QueryString["IdInmueble"], out idInmueble))
                {                    
                    string correo = Request.QueryString["Correo"];
                    Session["IdInmueble"] = idInmueble;

                    MostrarGaleriaDeImagenes();

                    string[] datosInmueble = ConexionBD.ObtenerDatosInmueblePorIdInmueble(idInmueble);


                    lblNombre.Text = datosInmueble[0];
                    lbldescripcion.Text = datosInmueble[1];
                    lblCantidadPersonas.Text = datosInmueble[2];
                    lblCantidadDormitorios.Text = datosInmueble[3];
                    lblCantidadBanos.Text = datosInmueble[4];
                    lblCantidadCamas.Text = datosInmueble[5];

                    lblubucacion.Text= datosInmueble[6];

                    ConexionBD conexion = new ConexionBD();
                    string idUbi = lblubucacion.Text;
                    DataTable datosUbicacion = conexion.ObtenerUbicacion(Convert.ToInt32(idUbi));

                    
                    if (datosUbicacion.Rows.Count > 0)
                    {
                        string provincia = datosUbicacion.Rows[0]["Provincia"].ToString();
                        string canton = datosUbicacion.Rows[0]["Canton"].ToString();
                        string posicionGPS = datosUbicacion.Rows[0]["PosicionGPS"].ToString();
                       
                        LabelUbicacion.Text = $"Provincia: {provincia}, Canton: {canton}, {posicionGPS}";
                    }



                    string[] datosInmueblePrecio = ConexionBD.ObtenerPrecioInmueble(idInmueble);
                    lblPrecio.Text = datosInmueblePrecio[0];


                    List<object> listaServicios = ConexionBD.ObtenerServicios(idInmueble).Cast<object>().ToList();

                    rptAmenities.DataSource = listaServicios;
                    rptAmenities.DataBind();

                    List<string> fechasReservadas = ConexionBD.ObtenerFechasReservadas(idInmueble);

                    CalendarUpdate(DateTime.Today.Year, DateTime.Today.Month);
                    CargarComentarios();

                }
            }
        }
        // Clase de modelo para representar una imagen
        public class ImagenModel
        {
            public byte[] ImagenData { get; set; }
            // Otras propiedades relacionadas con la imagen (nombre, descripción, etc.)
        }

        // En tu método MostrarGaleriaDeImagenes
        protected void MostrarGaleriaDeImagenes()
        {
            List<ImagenModel> listaImagenes = ConexionBD.ObtenerImagenesPorIdInmueble(idInmueble)
                                                .Select(data => new ImagenModel { ImagenData = data })
                                                .Take(5)
                                                .ToList();

            imageRepeater.DataSource = listaImagenes;
            imageRepeater.DataBind();
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
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#fff");
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalError();", true);
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

        private void CargarComentarios()
        {
            Session["IdInmueble"] = idInmueble;
            DataTable comentariosTable = BD.ObtenerComentariosPorInmueble(idInmueble);

            ComentariosRepeater.DataSource = comentariosTable;
            ComentariosRepeater.DataBind();
        }
        protected void btnCrearResena_Click(object sender, EventArgs e)
        {
            string correo = Session["Correo"].ToString();
            string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

            if (Session["IdInmueble"] != null && int.TryParse(Session["IdInmueble"].ToString(), out idInmueble))
            {
                string comentario = txtComentario.Text;
                BD.InsertarComentario(IdCedula, idInmueble, comentario);
                CargarComentarios();
            }

        }
    }
}