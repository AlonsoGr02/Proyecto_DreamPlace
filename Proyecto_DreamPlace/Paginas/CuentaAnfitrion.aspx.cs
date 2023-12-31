﻿using System;
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
                    string IdCedulaHuesped = Session["IdCedula"] != null ? Session["IdCedula"].ToString() : string.Empty;


                    string correo = Session["Correo"].ToString();
                    Usuario usuario = BD.ObtenerDatosUsuario(correo);
                    DataTable Notificaciones = BD.ObtenerNotificacionesPorCorreo(correo);

                    gvNotificaciones.DataSource = Notificaciones;
                    gvNotificaciones.DataBind();

                    if (usuario != null)
                    {
                        lblNombre.Text = usuario.Nombre;
                        lblApellido.Text = usuario.Apellidos;
                        lblCorreo.Text = usuario.Correo;
                    }


                    string correoC = Session["Correo"].ToString();

                    string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correoC);


                    CargarDatosRepeater();

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void CargarDatosRepeater()
        {
            string correo = Session["Correo"].ToString();

            string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correo);

            DataTable datosReserva = BD.ObtenerDatosHuesped(IdCedula);

            repeaterInmuebles.DataSource = datosReserva;
            repeaterInmuebles.DataBind();
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
                            

                            System.Web.UI.WebControls.Label labelIdCedula = (Label)item.FindControl("LabelIdCedula");
                            System.Web.UI.WebControls.Label labelIdReserva = (Label)item.FindControl("LabelIdReserva");

                            if (labelIdReserva != null)
                            {
                               int IdReserva = Convert.ToInt32(labelIdReserva.Text);

                                if (labelIdCedula != null)
                                {
                                    string IdCedula = labelIdCedula.Text;

                                    Session["TotalCalificacion"] = totalStars;
                                    Session["IdInmueble"] = idInmueble;
                                    ConexionBD.CalificacionHuesped(totalStars, IdCedula, IdReserva);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalExito();", true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalErrorCalificacion();", true);
            }
        }


    }
}