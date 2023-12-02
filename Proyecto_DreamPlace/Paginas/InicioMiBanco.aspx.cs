using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class InicioMiBanco : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string correo = Request.QueryString["correo"];
                ObtenerTipoDeCambio();

                if (!string.IsNullOrEmpty(correo))
                {
                    try
                    {
                        MiBanco infoMiBanco = BD.ObtenerInfoMiBancoPorCorreo(correo);

                        if (infoMiBanco != null)
                        {
                            txtCedula.Text = infoMiBanco.IdCedula;
                            txtNombre.Text = infoMiBanco.NombreCompleto;
                            txtNTarjeta.Text = infoMiBanco.IdNTarjeta;
                            txtSaldoDisponible.Text = infoMiBanco.Saldo.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        lblInfoDolar.Text = "Error: " + ex.Message;
                    }
                }
            }
        }
        private void ObtenerTipoDeCambio()
        {
            // URL de la API de tipo de cambio
            string url = "https://tipodecambio.paginasweb.cr/api";

            // Crea una instancia de HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realiza la solicitud GET a la API de forma síncrona
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    // Verifica si la solicitud fue exitosa
                    if (response.IsSuccessStatusCode)
                    {
                        // Lee el contenido de la respuesta de forma síncrona
                        string contenido = response.Content.ReadAsStringAsync().Result;

                        // Deserializa el JSON para obtener los datos
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic datos = serializer.Deserialize<dynamic>(contenido);

                        // Accede al tipo de cambio y actualiza tus controles
                        double tipoCambioCompra = Convert.ToDouble(datos["compra"]);
                        double tipoCambioVenta = Convert.ToDouble(datos["venta"]);

                        txtCompra.Text = "Compra: ₡" + tipoCambioCompra.ToString();
                        txtVenta.Text = "Venta: ₡" + tipoCambioVenta.ToString();
                    }
                    else
                    {
                        // Maneja el caso en que la solicitud no fue exitosa
                        lblInfoDolar.Text = "Error al obtener el tipo de cambio.";
                    }
                }
                catch (Exception ex)
                {
                    // Maneja excepciones
                    lblInfoDolar.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}