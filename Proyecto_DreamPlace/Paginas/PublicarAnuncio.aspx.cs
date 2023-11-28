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
        List<byte[]> imagenesTemporales = new List<byte[]>();


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
                int idCategoriaGuardada;
                if (!string.IsNullOrEmpty(HiddenFieldIdCategoria.Value) && int.TryParse(HiddenFieldIdCategoria.Value, out idCategoriaGuardada))
                {
                    Session["IdCategoria"] = idCategoriaGuardada;
                }
                string Correo = (string)Session["Correo"];
                string cedula = ConexionBD.ObtenerIdCedulaPorCorreo(Correo);
                //-----
                Session["CedAnfitrion"] = cedula;

                //objConexion.InsertarInmuebleCategoria(idCategoria, cedula, idEstado, idDescuento);

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
                    Session["tipoInmueble"] = tipoInmueble;
                }

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                //objConexion.ActualizarTipoInmueble(idInm, tipoInmueble);

                // Luego, oculta el paso 1.2 y muestra el paso 1.3
                contenedor3.Visible = false;
                contenedor4.Visible = true;
            }
            else if (contenedor4.Visible)
            {
                provincia = ddlProvincia.SelectedValue;
                canton = txtCanton.Text;
                direccionExacta = txtDirecExcata.Text;

                Session["Provincia"] = ddlProvincia.SelectedValue;
                Session["Canton"] = txtCanton.Text;
                Session["DireccionExacta"] = txtDirecExcata.Text;

                //objConexion.InsertarUbicacion(provincia, canton, direccionExacta);
                //idUbicacion = objConexion.ObtenerUltimoIdUbicacion();

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

                Session["CantidadHuespedes"] = int.Parse(txtCantidadHuespedes.Text);
                Session["CantidadHabitaciones"] = int.Parse(txtCantidadHabitaciones.Text);
                Session["CantidadCamas"] = int.Parse(txtCantidadCamas.Text);
                Session["CantidadBanos"] = int.Parse(txtBanos.Text);

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                idUbicacion = objConexion.ObtenerUltimoIdUbicacion();
                //objConexion.ActualizarDetallesInmueble(idInm, cantidadHuespedes, cantidadHabitaciones, cantidadBanos, cantidadCamas, idUbicacion);

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
                    Session["IdServicio1"] = idServicio1;
                }
                if (!string.IsNullOrEmpty(hfIdServicio2.Value) && int.TryParse(hfIdServicio2.Value, out idServicio2))
                {
                    Session["IdServicio2"] = idServicio2;
                }

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                //objConexion.InsertarServicioInmueble(idServicio1, idInm);
                //objConexion.InsertarServicioInmueble(idServicio2, idInm);

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
                Session["TituloInmueble"] = txtTitulo.Text;

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                //objConexion.ActualizarNombreInmueble(idInm, tituloInmueble);

                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor10.Visible = false;
                contenedorPaso3.Visible = true;
            }
            else if (contenedorPaso3.Visible)
            {
                // obtener el valor de descripcion
                descripcionIn = txtDescripcionI.Text;
                Session["DescripcionIn"] = txtDescripcionI.Text;

                int idInm = objConexion.ObtenerUltimoIdInmueble();
                //objConexion.ActualizarDescripcionInmueble(idInm, descripcionIn);

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
                    Session["PrecioBase"] = precioBase;
                    Session["Iva"] = iva;
                    Session["PrecioTotal"] = precioTotal;

                    int idInm = objConexion.ObtenerUltimoIdInmueble();
                    //objConexion.InsertarTotal(precioBase, iva, precioTotal, idInm);
                }

                string CorreoSession = (string)Session["Correo"];

                // Luego, oculta el paso 2.1 y muestra el paso 2.2
                contenedor12.Visible = false;

                // variables para hacer la vista previa del inmuble
                string cedAnfitrion = Session["CedAnfitrion"] as string;
                int idEstado = 1;
                int idDescuento = 1;
                int idCategoriaG = (int)Session["IdCategoria"];
                string tipoInmuebleGuardado = Session["tipoInmueble"] as string;
                string provinciaGuardada = Session["Provincia"] as string;
                string cantonGuardado = Session["Canton"] as string;
                string direccionGuardada = Session["DireccionExacta"] as string;
                int canHuespedesGuardada = (int)Session["CantidadHuespedes"];
                int canHabitacionesGuardada = (int)Session["CantidadHabitaciones"];
                int canCamasGuardada = (int)Session["CantidadCamas"];
                int canBanosGuardada = (int)Session["CantidadBanos"];
                int idServicio1Guardado = Session["IdServicio1"] != null ? (int)Session["IdServicio1"] : 0;
                int idServicio2Guardado = Session["IdServicio2"] != null ? (int)Session["IdServicio2"] : 0;
                string tituloInmuebleGuardado = Session["TituloInmueble"] as string;
                string descripcionInGuardada = Session["DescripcionIn"] as string;
                decimal? precioBaseGuardado = Session["PrecioBase"] as decimal?;
                decimal? ivaGuardado = Session["Iva"] as decimal?;
                decimal? precioTotalGuardado = Session["PrecioTotal"] as decimal?;

                ///-------------------

                // Carga vista previa ----------

                List<byte[]> imagenesTemporales = Session["ImagenesTemporales"] as List<byte[]>;

                if (imagenesTemporales != null)
                {
                    Panel carousel = (Panel)FindControl("carousel");

                    foreach (byte[] imagenBytes in imagenesTemporales)
                    {
                        Image img = new Image();
                        img.ImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(imagenBytes)}";
                        carousel.Controls.Add(img);
                    }
                }
                
                lblTituloAlojamiento.Text = "Titulo: " + tituloInmuebleGuardado;
                lblDescripcion.Text = "Descripción: " + descripcionInGuardada;
                lblTipoAlojamiento.Text = "Tipo de Inmueble: " + tipoInmuebleGuardado;
                string nomCategoria = objConexion.ObtenerNombreCategoriaPorId(idCategoriaG);
                lblCategoria.Text = "Categoría: " + nomCategoria;
                
                lblCPersonas.Text = "Cantidad Huéspedes: " + canHuespedesGuardada.ToString();
                lblCDormitorios.Text = "Cantidad Habitaciones: " + canHabitacionesGuardada.ToString();
                lblCCamas.Text = "Cantidad Camas: " + canCamasGuardada.ToString();
                lblCBanos.Text = "Cantidad Baños: " + canBanosGuardada.ToString();

                string nombreSer1 = objConexion.ObtenerNombreServicioPorId(idServicio1Guardado);
                string nombreSer2 = objConexion.ObtenerNombreServicioPorId(idServicio2Guardado);
                lblServicio1.Text = nombreSer1;
                lblServicio2.Text = nombreSer2;
                contenedor14.Visible = true;
            }
            else if (contenedor14.Visible)
            {
                // variables para hacer la insercion del inmuble
                string cedAnfitrion = Session["CedAnfitrion"] as string;
                int idEstado = Convert.ToInt32(ddlEstado.SelectedValue);
                int idDescuento = 1;
                int idCategoriaG = (int)Session["IdCategoria"];
                string tipoInmuebleGuardado = Session["tipoInmueble"] as string;
                string provinciaGuardada = Session["Provincia"] as string;
                string cantonGuardado = Session["Canton"] as string;
                string direccionGuardada = Session["DireccionExacta"] as string;
                int canHuespedesGuardada = (int)Session["CantidadHuespedes"];
                int canHabitacionesGuardada = (int)Session["CantidadHabitaciones"];
                int canCamasGuardada = (int)Session["CantidadCamas"];
                int canBanosGuardada = (int)Session["CantidadBanos"];
                int idServicio1Guardado = Session["IdServicio1"] != null ? (int)Session["IdServicio1"] : 0;
                int idServicio2Guardado = Session["IdServicio2"] != null ? (int)Session["IdServicio2"] : 0;
                string tituloInmuebleGuardado = Session["TituloInmueble"] as string;
                string descripcionInGuardada = Session["DescripcionIn"] as string;
                decimal precioBaseGuardado = (decimal)Session["PrecioBase"];
                decimal ivaGuardado = (decimal)Session["Iva"];
                decimal precioTotalGuardado = (decimal)Session["PrecioTotal"];

                List<byte[]> imagenesTemporales = Session["ImagenesTemporales"] as List<byte[]>;
                string listaImagenesBase64 = ObtenerListaImagenesBase64(imagenesTemporales);

                objConexion.InsertarInmuebleConmpleto(cedAnfitrion,idEstado,idDescuento,idCategoriaG,tipoInmuebleGuardado,provinciaGuardada,cantonGuardado,direccionGuardada,canHuespedesGuardada,canHabitacionesGuardada,canCamasGuardada,canBanosGuardada,idServicio1Guardado,idServicio2Guardado,tituloInmuebleGuardado,descripcionInGuardada,precioBaseGuardado,ivaGuardado,precioTotalGuardado,listaImagenesBase64);
                string CorreoSession = (string)Session["Correo"];
                Response.Redirect("CuentaAnfitrion.aspx?Correo=" + CorreoSession);
            }
        } // Fin del boton next


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
            List<byte[]> imagenesTemporales = Session["ImagenesTemporales"] as List<byte[]>;

            if (imagenesTemporales == null)
            {
                imagenesTemporales = new List<byte[]>();
            }

            if (FileUpload1.HasFile)
            {
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    bytes = br.ReadBytes(FileUpload1.PostedFile.ContentLength);
                }
                imagenesTemporales.Add(bytes);

                Session["ImagenesTemporales"] = imagenesTemporales;

                Label2.Text = "Imagen subida exitosamente.";
            }
        }

        private string ObtenerListaImagenesBase64(List<byte[]> imagenes)
        {
            List<string> imagenesBase64 = new List<string>();

            foreach (byte[] imagenBytes in imagenes)
            {
                string base64String = Convert.ToBase64String(imagenBytes);
                imagenesBase64.Add(base64String);
            }

            // Une las imágenes base64 separadas por comas
            string listaImagenes = string.Join(",", imagenesBase64);

            return listaImagenes;
        }

    } // Fin de la clase 
}