using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Solicitud_Reserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdInmueble"] != null && Session["IdInmueble"] is int)
            {
                string correo = Request.QueryString["Correo"];
                string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

                txtCedula.Text = IdCedula;



                string numtarjeta = ConexionBD.ObtenerNumTarjetaPorCedula(IdCedula);
                txtNumeroTarjeta.Text = numtarjeta;

                string cantidadAdultos = Session["CantidadAdultos"] as string;
                string fechaLlegada = Session["FechaLlegada"] as string;
                string fechaSalida = Session["FechaSalida"] as string;


                int idInmueble = (int)Session["IdInmueble"];
                List<byte[]> listaImagenes = ConexionBD.ObtenerImagenesPorIdInmueble(idInmueble);


                if (listaImagenes.Count > 0)
                {
                    var primeraImagen = new System.Web.UI.WebControls.Image();
                    primeraImagen.ID = "PrimeraImagen";
                    primeraImagen.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(listaImagenes[0]);
                    primeraImagen.CssClass = "image-box";


                    imageGallery.Controls.Add(primeraImagen);
                }

                string[] datosInmueble = ConexionBD.ObtenerDatosInmueblePorIdInmueble(idInmueble);
                txtDestino.Text = datosInmueble[0];

                string[] datosInmueblePrecio = ConexionBD.ObtenerPrecioInmueble(idInmueble);
                lblCostoxNoche.Text = datosInmueblePrecio[0];

                txttxtHuespedes.Text = cantidadAdultos;
                txtfechaLlegada.Text = fechaLlegada;
                txtfechaSalida.Text = fechaSalida;


                DateTime fechaLlegadaDateTime, fechaSalidaDateTime;

                if (DateTime.TryParse(fechaLlegada, out fechaLlegadaDateTime) &&
                    DateTime.TryParse(fechaSalida, out fechaSalidaDateTime))
                {

                    int diferenciaDias = (int)(fechaSalidaDateTime - fechaLlegadaDateTime).TotalDays;


                    decimal costoPorNoche;
                    if (decimal.TryParse(lblCostoxNoche.Text, out costoPorNoche))
                    {

                        decimal costoTotal = diferenciaDias * costoPorNoche;


                        lblTotal.Text = costoTotal.ToString("C");
                        //ConexionBD.InsertarReserva(IdCedula, idInmueble, cantidadAdultos, numtarjeta, fechaLlegadaDateTime, fechaSalidaDateTime);

                    }

                }
            }
        }
    }
}