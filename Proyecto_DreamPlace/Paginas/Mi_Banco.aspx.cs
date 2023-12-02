using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
using CapaNegocio.Models;
using System.Net.Http;



namespace Proyecto_DreamPlace.Paginas
{
    public partial class Mi_Banco : System.Web.UI.Page
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