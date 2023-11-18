using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocio;
namespace Proyecto_DreamPlace.Paginas
{
    public partial class ManteAlojamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["Correo"] != null)
                {
                    string correo = Session["Correo"].ToString();
                    CargarImagenes();
                }
                
            }
        }
        public void CargarImagenes()
        {
            int ID = 39;
            List<byte[]> listaimagenes = ConexionBD.ObtenerImagenesPorIdInmueble(ID);
            int limite = Math.Min(listaimagenes.Count, 5);
            for (int i = 0; i < limite; i++)
            {
                string imagenid = "Image" + (i + 1);
                System.Web.UI.WebControls.Image ImageC = FindControl(imagenid) as System.Web.UI.WebControls.Image;
                if (ImageC != null)
                {
                    ImageC.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(listaimagenes[i]);
                    ImageC.CssClass = "imagen-estilo";
                }
            }
        }
    }
}