using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                DateTime fechaLlegada = (DateTime)Session["FechaLlegada"];
                DateTime fechaSalida = (DateTime)Session["FechaSalida"];

                txtfechaLlegada.Text = fechaLlegada.ToString("dd/MM/yyyy");
                txtfechaSalida.Text = fechaSalida.ToString("dd/MM/yyyy");

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

                DateTime fechaLlegadaDateTime, fechaSalidaDateTime;

                int diferenciaDias = (int)(fechaSalida - fechaLlegada).TotalDays;

                decimal costoPorNoche;
                if (decimal.TryParse(lblCostoxNoche.Text, out costoPorNoche))
                {
                    decimal costoTotal = diferenciaDias * costoPorNoche;
                    lblTotal.Text = costoTotal.ToString("C");
                }
            }
        }

        protected void ConfirmarReservaButton_Click(object sender, EventArgs e)
        {
            int cantidadAdultos;
            if (int.TryParse(txttxtHuespedes.Text, out cantidadAdultos))
            {
                string IdCedula = txtCedula.Text;
                int idInmueble = (int)Session["IdInmueble"];
                string numtarjeta = txtNumeroTarjeta.Text;

                string lbTotal = lblTotal.Text;

                lbTotal = lbTotal.Replace("$", "").Replace(",", "");

                decimal costoTotal;
                if (decimal.TryParse(lbTotal, NumberStyles.Currency, CultureInfo.CurrentCulture, out costoTotal))
                {
                    
                    if (ConexionBD.TieneSaldoSuficiente(numtarjeta, costoTotal))
                    {
                        ConexionBD.InsertarPagos(numtarjeta, costoTotal, idInmueble, IdCedula);

                        DateTime fechaLlegada, fechaSalida;
                        if (DateTime.TryParse(txtfechaLlegada.Text, out fechaLlegada) && DateTime.TryParse(txtfechaSalida.Text, out fechaSalida))
                        {
                            Session["FechaLlegada"] = fechaLlegada;
                            Session["FechaSalida"] = fechaSalida;
                            ConexionBD.InsertarFechaReservada(fechaLlegada, fechaSalida, idInmueble, IdCedula);

                            int idFechaReservada = ConexionBD.ObtenerIdFechaReservada(IdCedula);
                            ConexionBD.InsertarReserva(IdCedula, idInmueble, cantidadAdultos, numtarjeta, idFechaReservada);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalExito();", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalSaldoInsuficiente();", true);
                        return; 
                    }
                }
                else
                {
                    // Manejar el escenario en el que lbTotal no es un valor decimal válido después de la limpieza
                    // Por ejemplo, mostrar un mensaje de error o establecer un valor predeterminado
                }
            }
        }

    }
}