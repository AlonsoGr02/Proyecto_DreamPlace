using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio.Models;
using CapaNegocio;
using System.Data;
using System.Drawing.Drawing2D;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class CuentaAnfitrion : System.Web.UI.Page
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


                    DataTable inmuebles = BD.ObtenerInmuebles(IdCedula);

                    if (inmuebles.Rows.Count > 0)
                    {
                        DropDownListInmuebles.DataSource = inmuebles;
                        DropDownListInmuebles.DataTextField = "Nombre";
                        DropDownListInmuebles.DataValueField = "IdInmueble";
                        DropDownListInmuebles.DataBind();
                    }

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        protected void DropDownListInmuebles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string IdInmueble = DropDownListInmuebles.SelectedValue;

                int IdInmuebleInt;
                if (int.TryParse(IdInmueble, out IdInmuebleInt))
                {
                    // Aquí obtienes los datos del inmueble seleccionado
                    DataTable datosAnfitrion = BD.ObtenerDatosHuesped(IdInmuebleInt);

                    if (datosAnfitrion != null && datosAnfitrion.Rows.Count > 0)
                    {
                        // Vincula los datos al Repeater
                        repeaterInmuebles.DataSource = datosAnfitrion;
                        repeaterInmuebles.DataBind();

                        // Ejecuta el script para abrir el modal después de cargar los datos
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalEvaluacion();", true);
                    }
                    else
                    {
                        // Manejar la situación en la que no se encuentran datos
                    }
                }
                else
                {
                    // Manejo de error en caso de que no se pueda convertir a entero
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalErrorCalificacion();", true);
            }
        }

        protected void BtnAgregarTarjetaAnfitrion_Click(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            try
            {
                Usuario usuario = new Usuario
                {
                    Correo = Session["Correo"].ToString(),
                    NumeroDeTarjeta = txtNumeroDeTrajeta.Text,
                    CVV = TXTCVV.Text,
                    FechaVencimientoTarjeta = DateTime.Parse(txtFechaVencimiento.Text)
                };

                int Tarjeta = Int32.Parse(txtNumeroDeTrajeta.Text);
                string correoC = Session["Correo"].ToString();

                string cedula = ConexionBD.ObtenerIdCedulaPorCorreo(correoC);

                ConexionBD.InsertarMetodoPago(usuario.Correo, usuario.NumeroDeTarjeta, usuario.CVV, usuario.FechaVencimientoTarjeta);
                BD.InsertarDatosMiBanco(Tarjeta, cedula, Tarjeta);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "showSuccessModal", "$(document).ready(function () { $('#successModal').modal('show'); setTimeout(function () { $('#successModal').modal('hide'); }, 3000); });", true);

            }
            catch (Exception ex)
            {

            }
        }


        protected void chkStar_CheckedChanged(object sender, EventArgs e)
        {
            RepeaterItem item = ((Control)sender).NamingContainer as RepeaterItem;

            CheckBox chkStar1 = (CheckBox)item.FindControl("chkStar1");
            CheckBox chkStar2 = (CheckBox)item.FindControl("chkStar2");
            CheckBox chkStar3 = (CheckBox)item.FindControl("chkStar3");
            CheckBox chkStar4 = (CheckBox)item.FindControl("chkStar4");
            CheckBox chkStar5 = (CheckBox)item.FindControl("chkStar5");

            int totalStars = 0;

            if (chkStar1.Checked) totalStars += 1;
            if (chkStar2.Checked) totalStars += 1;
            if (chkStar3.Checked) totalStars += 1;
            if (chkStar4.Checked) totalStars += 1;
            if (chkStar5.Checked) totalStars += 1;

            Label4.Text = totalStars.ToString();
        }




        protected void btnEnviarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                string correoC = Session["Correo"].ToString();
                int totalStars = Convert.ToInt32(Label4.Text);
                int idInmueble = -1;

                foreach (RepeaterItem item in repeaterInmuebles.Items)
                {
                    CheckBox chkStar1 = (CheckBox)item.FindControl("chkStar1");
                    if (chkStar1 != null && chkStar1.Checked)
                    {
                        System.Web.UI.WebControls.Label lblIdInmueble = (Label)item.FindControl("lblIdInmueble");
                        if (lblIdInmueble != null)
                        {
                            idInmueble = Convert.ToInt32(lblIdInmueble.Text);
                            break;
                        }
                    }
                }



                Session["TotalCalificacion"] = totalStars;
                Session["IdInmueble"] = idInmueble;


                //ConexionBD.InsertarCalificacion(totalStars, , idInmueble);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalExito();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalErrorCalificacion();", true);
            }
        }

    }
}