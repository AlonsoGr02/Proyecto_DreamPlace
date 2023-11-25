using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto_DreamPlace.Paginas
{
    public partial class VerficarIdentidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            byte[] imagenFrontal = null;
            byte[] imagenTrasera = null;

            // Validar que los archivos estén adjuntos siempre
            if (!FileUploadFrontal.HasFile || !FileUploadTrasera.HasFile)
            {
                lblRespu.Text = "Debe adjuntar ambos archivos";
                return;
            }

            if (FileUploadFrontal.HasFile)
            {
                imagenFrontal = ConvertirImagenABytes(FileUploadFrontal.PostedFile);
                lblInfo.Text = "Adjunta la foto trasera de la cédula";
                lblInfoTrasera.Text = "";
            }
            if (FileUploadTrasera.HasFile)
            {
                imagenTrasera = ConvertirImagenABytes(FileUploadTrasera.PostedFile);
            }

            lblRespu.Text = "mostar respu aqui";
        }

        private byte[] ConvertirImagenABytes(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                byte[] imagenEnBytes = new byte[file.ContentLength];
                file.InputStream.Read(imagenEnBytes, 0, file.ContentLength);
                return imagenEnBytes;
            }
            return null;
        }
    }
}