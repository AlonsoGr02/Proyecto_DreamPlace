using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio.Models;
using CapaNegocio;
using System.Data;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class Cuenta : System.Web.UI.Page
    {
        ConexionBD BD = new ConexionBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                topnav.Visible = false;
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    Usuario usuario = BD.ObtenerDatosUsuario(correo);

                    if (usuario != null)
                    {
                        lblNombre.Text = usuario.Nombre;
                        lblApellido.Text = usuario.Apellidos;
                        lblCorreo.Text = usuario.Correo;
                    }



                    string correoC = Session["Correo"].ToString();

                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correoC);

                    DataTable datosAnfitrion = BD.ObtenerDatosAnfitrion(IdCedula);

                    if (datosAnfitrion != null && datosAnfitrion.Rows.Count > 0)
                    {
                        repeaterInmuebles.DataSource = datosAnfitrion;
                        repeaterInmuebles.DataBind();
                    }
                    else
                    {
                        // Manejar la situación en la que no se encuentran datos
                    }

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
              

            }
        }


        protected void BtnAgregarTarjeta_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Correo = Session["Correo"].ToString(),
                    NumeroDeTarjeta = txtNumeroDeTrajeta.Text,
                    CVV = TXTCVV.Text,
                    FechaVencimientoTarjeta = DateTime.Parse(txtFechaVencimiento.Text)
                };


                ConexionBD.InsertarMetodoPago(usuario.Correo, usuario.NumeroDeTarjeta, usuario.CVV, usuario.FechaVencimientoTarjeta);


                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$(document).ready(function () { $('#successModal').modal('show'); setTimeout(function () { $('#successModal').modal('hide'); }, 3000); });", true);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnStar_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int value = int.Parse(clickedButton.Attributes["data-value"]);

            // Almacena el estado de la estrella en una variable de sesión
            Session["StarValue"] = value;

            // Marca las estrellas seleccionadas hasta el valor clickeado
            foreach (Control control in Controls)
            {
                if (control is Button starButton)
                {
                    int buttonValue = int.Parse(starButton.Attributes["data-value"]);
                    starButton.CssClass = buttonValue <= value ? "star-button-selected" : "star-button";
                }
            }
        }

        protected void btnEnviarCalificacion_Click(object sender, EventArgs e)
        {
            // Obtiene la suma de las estrellas seleccionadas desde la variable de sesión
            int sumaEstrellas = Convert.ToInt32(Session["StarValue"]);

            // Aquí puedes utilizar 'sumaEstrellas' para enviar la calificación al servidor
            // Realiza las operaciones necesarias con 'sumaEstrellas'
        }


    }
}