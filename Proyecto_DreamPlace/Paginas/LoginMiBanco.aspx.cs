﻿using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class LoginMiBanco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            paso2.Visible = false;
        }
        protected void btnSoliCodigo_Click(object sender, EventArgs e)
        {
            string correo = txtcorreo.Text;
            string contrasena = txtContrasena.Text;

            if (ValidarLogin(correo, contrasena))
            {
                ConexionBD objConexion = new ConexionBD();
                Metodos objMetodos = new Metodos();

                string clave = objMetodos.GenerarCodigoNumerico();
                objConexion.ActualizarClaveUsuario(clave, correo);
                objMetodos.EnviarCorreo(correo, clave);
                lblRespu.Text = "El código de verificación fue enviado al correo " + correo;

                Session["CorreoLogin"] = correo;

                // ocultar contenedor
                paso1.Visible = false;
                paso2.Visible = true;
            }
            else
            {
                lblRespu.Text = "El correo proporcionado y/o contraseña incorrectos.";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ConexionBD Mante = new ConexionBD();
            string correo = txtcorreo.Text;
            string codigo = txtcodigoVerificion.Text;

            if (ValidarCodigoL(correo, codigo))
            {
                // Realiza la lógica de obtención del IdRol (asumo que tienes una función para obtener el IdRol del usuario)
                int idRol = Mante.ObtenerIdRol(correo); // Ajusta esta línea según tu implementación

                // Almacena el IdRol en la sesión para poder acceder a él en la página de destino
                Session["IdRol"] = idRol;

                string CorreoSession = correo;// txtcorreo.Text;
                Session["Correo"] = CorreoSession;
                lblRespu.Text = "Inicio de sesión exitoso";

                // inico correo*********
                string cuerpo = "¡Bienvenido a MiBanco! " +
                             "Has iniciado sesión con éxito. " +
                             "Si no reconoces esta actividad, por favor, contacta la plataforma.";
                string asunto = "Inicio de Sesión en la Aplicación del Banco";
                // Envía el correo al usuario
                Metodos metodos = new Metodos();
                metodos.EnviarCorreoPersonalizado(correo, asunto, cuerpo);
                //fin correo**********

                // Redirige según el IdRol
                if (idRol == 1)
                {
                    Response.Redirect("InicioMiBanco.aspx?Correo=" + CorreoSession);
                }
                else if (idRol == 2)
                {
                    Response.Redirect("InicioMiBancoAnf.aspx?Correo=" + CorreoSession);
                }
                else
                {
                    // Maneja cualquier otro caso o muestra un mensaje de error
                    lblRespu.Text = "Rol no reconocido";
                }
            }
            else
            {
                lblRespu.Text = "Código de verificación incorrecto";
            }
        }

        private bool ValidarLogin(string correo, string codigo)
        {
            ConexionBD objConexion = new ConexionBD();

            return objConexion.ValidarLogin(correo, codigo);
        }

        private bool ValidarCodigoL(string correo, string codigo)
        {
            ConexionBD objConexion = new ConexionBD();
            return objConexion.ValidarCodigoL(correo, codigo);
        }

    }
}