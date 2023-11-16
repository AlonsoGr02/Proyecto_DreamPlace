using CapaNegocio;
using CapaNegocio.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class PublicarAnuncio : System.Web.UI.Page
    {
        ConexionBD objConexion = new ConexionBD();
        int idUbicacion;
        int idCategoria;
        string tipoInmueble;
        string provincia;
        string canton;
        string direccionExacta;
        int cantidadHuespedes;
        int cantidadHabitaciones;
        int cantidadCamas;
        int cantidadBanos;
        int idServicio1;
        int idServicio2;
        string tituloInmueble;
        string descripcionIn;

        protected void Page_Load(object sender, EventArgs e)
        {
            ConexionBD BD = new ConexionBD();
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    Usuario usuario = BD.ObtenerDatosUsuario(correo);

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                    // Mostrar el primer paso y ocultar los demás
                contenedorPrincipal.Visible = true;
                contenedor2.Visible = false;
                contenedor3.Visible = false;
                contenedor4.Visible = false;
                contenedor5.Visible = false;
                contenedorPaso2.Visible = false;
                contenedor6.Visible = false;
                contenedor7.Visible = false;
                contenedor8.Visible = false;
                contenedor10.Visible = false;
                contenedorPaso3.Visible = false;
                contenedor12.Visible = false;
                contenedor14.Visible = false;
            }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
          
            if (contenedorPrincipal.Visible)
            {
                // Luego, oculta el paso 1 y muestra el paso 1.1
                contenedorPrincipal.Visible = false;
                contenedor2.Visible = true;

                // Carga los datos de las categorias
                DataTable categorias = objConexion.ObtenerCategoriasActivas();
                // Agregar una columna para la representación en base64 de la imagen
                categorias.Columns.Add("ImagenBase64", typeof(string));
                foreach (DataRow row in categorias.Rows)
                {
                    byte[] iconoBytes = (byte[])row["Icono"];
                    string iconoBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(iconoBytes);
                    row["ImagenBase64"] = iconoBase64;
                }
                repeaterCategorias.DataSource = categorias;
                repeaterCategorias.DataBind();
            }
            else if (contenedor2.Visible)
            {
                // obetner id categoria
                if (!string.IsNullOrEmpty(HiddenFieldIdCategoria.Value) && int.TryParse(HiddenFieldIdCategoria.Value, out idCategoria))
                {

                }
                string Correo = (string) Session["Correo"];
                string cedula = ConexionBD.ObtenerIdCedulaPorCorreo(Correo); // cambiar luego con las de session **********************
                int idEstado = 1;
                int idDescuento = 1;
                objConexion.InsertarInmuebleCategoria(idCategoria, cedula, idEstado, idDescuento);

                // Luego, oculta el paso 1.1 y muestra el paso 1.2
                contenedor2.Visible = false;
                contenedor3.Visible = true;
            }
            else if (contenedor3.Visible)
            {
                if (HiddenFieldTipoInmueble != null)
                {
                    // obtener el valor del tipoInmueble
                    tipoInmueble = HiddenFieldTipoInmueble.Value;
                }

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                objConexion.ActualizarTipoInmueble(idInm, tipoInmueble);

                // Luego, oculta el paso 1.2 y muestra el paso 1.3
                contenedor3.Visible = false;
                contenedor4.Visible = true;
            }
            else if (contenedor4.Visible)
            {
                // obtener los campos
                provincia = txtProvincia.Text;
                canton = txtCanton.Text;
                direccionExacta = txtDirecExcata.Text;

                objConexion.InsertarUbicacion(provincia, canton, direccionExacta);
                idUbicacion = objConexion.ObtenerUltimoIdUbicacion();

                // Luego, oculta el paso 1.3 y muestra el paso 1.4
                contenedor4.Visible = false;
                contenedor5.Visible = true;

            }
            else if (contenedor5.Visible)
            {
                // obtener los valores 
                cantidadHuespedes = int.Parse(txtCantidadHuespedes.Text);
                cantidadHabitaciones = int.Parse(txtCantidadHabitaciones.Text);
                cantidadCamas = int.Parse(txtCantidadCamas.Text);
                cantidadBanos = int.Parse(txtBanos.Text);

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                idUbicacion = objConexion.ObtenerUltimoIdUbicacion();
                objConexion.ActualizarDetallesInmueble(idInm, cantidadHuespedes, cantidadHabitaciones, cantidadBanos, cantidadCamas, idUbicacion);

                // Luego, oculta el paso 1.4 y muestra el paso 2
                contenedor5.Visible = false;
                contenedorPaso2.Visible = true;
            }
            else if (contenedorPaso2.Visible)
            {
                // Carga los datos de servicios en el contenedor6
                DataTable servicios = objConexion.ObtenerTodosLosServicios();

                // Agrega una columna para la representación en base64 de la imagen
                servicios.Columns.Add("ImagenBase64", typeof(string));
                foreach (DataRow row in servicios.Rows)
                {
                    byte[] iconoBytes = (byte[])row["Icono"];
                    string iconoBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(iconoBytes);
                    row["ImagenBase64"] = iconoBase64;
                }
                repeaterServicios.DataSource = servicios;
                repeaterServicios.DataBind();

                // Luego, oculta el paso 2 y muestra el paso 2.1
                contenedorPaso2.Visible = false;
                contenedor6.Visible = true;
            }
            else if (contenedor6.Visible)
            {
                // obtener los datos de los servicio 1 y 2
                if (!string.IsNullOrEmpty(hfIdServicio1.Value) && int.TryParse(hfIdServicio1.Value, out idServicio1))
                {

                }
                if (!string.IsNullOrEmpty(hfIdServicio2.Value) && int.TryParse(hfIdServicio2.Value, out idServicio2))
                {

                }

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                objConexion.InsertarServicioInmueble(idServicio1, idInm);
                objConexion.InsertarServicioInmueble(idServicio2, idInm);

                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor6.Visible = false;
                contenedor7.Visible = true;
            }
            else if (contenedor7.Visible)
            {
                //contenedor carga de imagenes , se maneja aparte
                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor7.Visible = false;
                contenedor8.Visible = true;
            }
            else if (contenedor8.Visible)
            {
                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor8.Visible = false;
                contenedor10.Visible = true;
            }
            else if (contenedor10.Visible)
            {
                // obtner le valor del titulo
                tituloInmueble = txtTitulo.Text;

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                objConexion.ActualizarNombreInmueble(idInm, tituloInmueble);

                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor10.Visible = false;
                contenedorPaso3.Visible = true;
            }
            else if (contenedorPaso3.Visible)
            {
                // obtener el valor de descripcion
                descripcionIn = txtDescripcionI.Text;

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                objConexion.ActualizarDescripcionInmueble(idInm, descripcionIn);

                // Luego, oculta el paso 2 y muestra el paso 2.1
                contenedorPaso3.Visible = false;
                contenedor12.Visible = true;
            }
            else if (contenedor12.Visible)
            {
                // obtener valores de lso precios
                decimal precioBase;
                decimal iva;
                decimal precioTotal;
                if (!string.IsNullOrEmpty(hiddenPrecioBase.Value) && decimal.TryParse(hiddenPrecioBase.Value, out precioBase) &&
        !string.IsNullOrEmpty(hiddenIva.Value) && decimal.TryParse(hiddenIva.Value, out iva) &&
        !string.IsNullOrEmpty(hiddenPrecioTotal.Value) && decimal.TryParse(hiddenPrecioTotal.Value, out precioTotal))
                {
                    int idInm = objConexion.ObtenerUltimoIdInmueble();
                    objConexion.InsertarTotal(precioBase, iva, precioTotal, idInm);
                }
                string CorreoSession = (string)Session["Correo"];
                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor12.Visible = false;
                contenedor14.Visible = false;
                Response.Redirect("CuentaAnfitrion.aspx?Correo=" + CorreoSession);

            }
        } // Fin del boton next

        // no se esta utilizando de momento
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            // Maneja el evento del botón "Anterior"
            if (contenedor14.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor14.Visible = false;
                contenedor12.Visible = true;
            }
            //else if (contenedor13.Visible)
            //{
            //    // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

            //    // Luego, oculta el paso 3 y muestra el paso 2
            //    contenedor13.Visible = false;
            //    contenedor12.Visible = true;
            //}
            else if (contenedor12.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor12.Visible = false;
                contenedorPaso3.Visible = true;
            }
            //else if (contenedor11.Visible)
            //{
            //    // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

            //    // Luego, oculta el paso 3 y muestra el paso 2
            //    contenedor11.Visible = false;
            //    contenedorPaso3.Visible = true;
            //}
            else if (contenedorPaso3.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedorPaso3.Visible = false;
                contenedor10.Visible = true;
            }
            else if (contenedor10.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor10.Visible = false;
                contenedor8.Visible = true;
            }
            //else if (contenedor9.Visible)
            //{
            //    // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

            //    // Luego, oculta el paso 3 y muestra el paso 2
            //    contenedor9.Visible = false;
            //    contenedor8.Visible = true;
            //}
            else if (contenedor8.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor8.Visible = false;
                contenedor7.Visible = true;
            }
            else if (contenedor7.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor7.Visible = false;
                contenedor6.Visible = true;
            }
            else if (contenedor6.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor6.Visible = false;
                contenedorPaso2.Visible = true;
            }
            else if (contenedorPaso2.Visible)
            {
                // Si el paso actual es el 2, realiza cualquier procesamiento necesario para el paso 2

                // Luego, oculta el paso 2 y muestra el paso 1
                contenedorPaso2.Visible = false;
                contenedor5.Visible = true;
            }
            else if (contenedor5.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor5.Visible = false;
                contenedor4.Visible = true;
            }
            else if (contenedor4.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor4.Visible = false;
                contenedor3.Visible = true;
            }
            else if (contenedor3.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor3.Visible = false;
                contenedor2.Visible = true;
            }
            else if (contenedor2.Visible)
            {
                // Si el paso actual es el 3, realiza cualquier procesamiento necesario para el paso 3

                // Luego, oculta el paso 3 y muestra el paso 2
                contenedor2.Visible = false;
                contenedorPrincipal.Visible = true;
            }
            // ... Puedes continuar este patrón para más pasos
        } // Fin del boton prev

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Verificar si se seleccionó un archivo
            if (FileUpload1.HasFile)
            {
                // Convertir el archivo a un array de bytes
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength);
                }

                int idInmueble = objConexion.ObtenerUltimoIdInmueble();
                objConexion.InsertarImagenInmueble(bytes, idInmueble);

                Label2.Text = "Imagen subida exitosamente.";
            }
        }


    } // Fin de la clase 
}
