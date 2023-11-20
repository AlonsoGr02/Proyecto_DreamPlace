using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Mi_BancoAnf : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string correo = Request.QueryString["correo"];

                if (!string.IsNullOrEmpty(correo))
                {
                    try
                    {
                        string idCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

                        if (!string.IsNullOrEmpty(idCedula))
                        {
                            MiBanco infoMiBanco = BD.ObtenerInfoMiBancoPorCorreo(idCedula);

                            if (infoMiBanco != null)
                            {
                                txtCedula.Text = infoMiBanco.IdCedula;
                                txtNombre.Text = infoMiBanco.NombreCompleto;
                                txtNTarjeta.Text = infoMiBanco.IdNTarjeta;
                                txtMontoTotal.Text = infoMiBanco.Saldo.ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de excepciones, por ejemplo, mostrar un mensaje de error.
                    }
                }
            }
        }
        protected void btnDepositar_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginMiBanco.aspx");
        }
    }
}