using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;

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

                            string[] nombreApellido = ConexionBD.ObtenerNombreYApellidoPorIdCedula(idCedula);

                            if (nombreApellido.Length == 2)
                            {

                                txtCedula.Text = idCedula;
                                txtNombre.Text = $"{nombreApellido[0]} {nombreApellido[1]}";


                                Tuple<string, decimal, string, string> infoMiBanco = ConexionBD.ObtenerInfoMiBancoPorCedula(idCedula);


                                txtNTarjeta.Text = infoMiBanco.Item4;
                                txtMontoTotal.Text = infoMiBanco.Item2.ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}