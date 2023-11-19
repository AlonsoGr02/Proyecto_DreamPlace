﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio.Models;
using CapaNegocio;
using System.Data;
using System.Reflection.Emit;
using Label = System.Web.UI.WebControls.Label;

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

            // Mostrar el total en una etiqueta
            Label4.Text = totalStars.ToString();
        }




        protected void btnEnviarCalificacion_Click(object sender, EventArgs e)
        {
            try
            {
                string correoC = Session["Correo"].ToString();
                int totalStars = Convert.ToInt32(Label4.Text);
                int idInmueble = -1; // Valor por defecto, puede ser -1 o cualquier otro valor que represente "no encontrado"

                // Recorrer el Repeater para obtener el IdInmueble correspondiente al ítem seleccionado
                foreach (RepeaterItem item in repeaterInmuebles.Items)
                {
                    CheckBox chkStar1 = (CheckBox)item.FindControl("chkStar1");
                    if (chkStar1 != null && chkStar1.Checked)
                    {
                        // Obtener el IdInmueble desde el Repeater
                        System.Web.UI.WebControls.Label lblIdInmueble = (Label)item.FindControl("lblIdInmueble");
                        if (lblIdInmueble != null)
                        {
                            idInmueble = Convert.ToInt32(lblIdInmueble.Text); // Convertir el texto del Label a int
                            break; 
                        }
                    }
                }

                string IdCedula = ConexionBD.ObtenerIdCedulaPorCorreo(correoC);

                // Guardar en variables de sesión
                Session["IdCedula"] = IdCedula;
                Session["TotalCalificacion"] = totalStars;
                Session["IdInmueble"] = idInmueble;

          
                ConexionBD.InsertarCalificacion(totalStars, IdCedula, idInmueble);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalExito();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "AbrirModalErrorCalificacion();", true);
            } 

        }
    }
}