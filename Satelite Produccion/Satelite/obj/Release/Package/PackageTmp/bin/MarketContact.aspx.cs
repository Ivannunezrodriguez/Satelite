using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
#pragma warning disable CS0105 // La directiva using para 'System.Net.Mail' aparece previamente en este espacio de nombres
using System.Net.Mail;
#pragma warning restore CS0105 // La directiva using para 'System.Net.Mail' aparece previamente en este espacio de nombres
#pragma warning disable CS0105 // La directiva using para 'System.Net' aparece previamente en este espacio de nombres
using System.Net;
#pragma warning restore CS0105 // La directiva using para 'System.Net' aparece previamente en este espacio de nombres
using System.Threading.Tasks;


namespace Satelite
{
    public partial class MarketplaceContact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Enviar_Click(object Source, EventArgs e)
        {
            if (chck.Checked == true)
            {
                string a = "Desde Pagina de Contactos en MarketPlace:<br/><br/>";
                a += "Nombre y apellidos: " + TxName.Text + "<br/>";
                a += "Mail: " + TxMail.Text + "<br/>";
                a += "Telefono: " + TxTelefono.Text + "<br/><br/>";
                a += TxMensaje.Text + "<br/>";
                SendEmailAsync("formacion@viveroseresma.com", null, a);
                SendEmailAsync(TxMail.Text, null, a);//


            }
            else
            {
                //mostrar asterisco en el lavel mensaje
                return;
            }
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("formacion@viveroseresma.com", "i6as_ZE4B");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("viveroseresma.markeplace@viveroseresma.com", "Solicitud de contacto"),
                    Subject = "Solicitud Contacto MarketPlace",
                    Body = message, // "MENSAJE",
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.viveroseresma.com",
                    EnableSsl = false,
                    Credentials = credentials
                };

                // Send it...         
                  client.Send(mail);
            }
            catch
            {
                //error
            }

            return Task.CompletedTask;
        }

    }
  

}