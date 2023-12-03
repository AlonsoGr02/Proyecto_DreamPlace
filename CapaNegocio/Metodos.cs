using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Metodos
    {
        public string GenerarCodigoNumerico()
        {
            const int longitudCodigo = 6;
            Random random = new Random();

            StringBuilder codigoBuilder = new StringBuilder(longitudCodigo);

            for (int i = 0; i < longitudCodigo; i++)
            {
                codigoBuilder.Append(random.Next(0, 10)); // Números del 0 al 9
            }

            return codigoBuilder.ToString();
        }

        public void EnviarCorreo(string correoDesti, string clave)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress("prograpruebas@spestechnical.com");
            message.To.Add(new MailAddress(correoDesti));
            message.Subject = "Codigo de verificación";
            message.Body = $"El codigo de verificación es {clave} .";


            SmtpClient client = new SmtpClient("smtp.titan.email");
            client.Port = 587;
            client.Credentials = new NetworkCredential("prograpruebas@spestechnical.com", "CursoCUC2023");
            client.EnableSsl = true;
            client.Send(message);

        }

        public void EnviarCorreoPersonalizado(string correoDesti, string asunto, string cuerpo)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress("prograpruebas@spestechnical.com");
            message.To.Add(new MailAddress(correoDesti));
            message.Subject = asunto;
            message.Body = cuerpo;


            SmtpClient client = new SmtpClient("smtp.titan.email");
            client.Port = 587;
            client.Credentials = new NetworkCredential("prograpruebas@spestechnical.com", "CursoCUC2023");
            client.EnableSsl = true;
            client.Send(message);

        }


    } // Fin de la clase Metodos
}
