using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

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
                lblInfo.Text = "Adjunta una foto visible de tu cara";
                lblInfoTrasera.Text = "";
            }
            if (FileUploadTrasera.HasFile)
            {
                imagenTrasera = ConvertirImagenABytes(FileUploadTrasera.PostedFile);
            }

            // Llamada a la función de comparación de caras
            float similitudMinima = 90F; // Puedes ajustar este valor según tus necesidades
            bool coincidencia = CompararCaras(imagenFrontal, imagenTrasera, similitudMinima);

            if (coincidencia)
            {
                lblRespu.Text = "Las caras coinciden";
            }
            else
            {
                lblRespu.Text = "Las caras no coinciden";
            }
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

        private bool CompararCaras(byte[] imagenFrontal, byte[] imagenTrasera, float similitudMinima)
        {
            try
            {
                if (imagenFrontal == null || imagenTrasera == null)
                {
                    return false; // No hay imágenes para comparar
                }

                using (AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient())
                {
                    Amazon.Rekognition.Model.Image imageSource = new Amazon.Rekognition.Model.Image()
                    {
                        Bytes = new MemoryStream(imagenFrontal)
                    };

                    Amazon.Rekognition.Model.Image imageTarget = new Amazon.Rekognition.Model.Image()
                    {
                        Bytes = new MemoryStream(imagenTrasera)
                    };

                    CompareFacesRequest compareFacesRequest = new CompareFacesRequest()
                    {
                        SourceImage = imageSource,
                        TargetImage = imageTarget,
                        SimilarityThreshold = similitudMinima
                    };

                    // Llamada a la operación
                    CompareFacesResponse compareFacesResponse = rekognitionClient.CompareFaces(compareFacesRequest);

                    // Ver resultados
                    foreach (CompareFacesMatch match in compareFacesResponse.FaceMatches)
                    {
                        ComparedFace face = match.Face;
                        BoundingBox position = face.BoundingBox;
                        Console.WriteLine("Face at " + position.Left
                              + " " + position.Top
                              + " matches with " + match.Similarity
                              + "% confidence.");
                    }

                    return compareFacesResponse.FaceMatches.Count > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí, puedes imprimir un mensaje de error o registrar la excepción en algún lugar
                Console.WriteLine("Error durante la comparación de caras: " + ex.Message);
                return false;
            }
        }

    }
}