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
    public partial class InicioMiBanco : System.Web.UI.Page
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
                        // Manejo de excepciones, por ejemplo, mostrar un mensaje de error.
                    }
                }
            }
        }

    }
}