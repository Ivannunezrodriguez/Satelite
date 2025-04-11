using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using static ClosedXML.Excel.XLPredefinedFormat;
using DateTime = System.DateTime;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace Satelite
{
    public partial class MarketplaceFunction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string procedure = Request.Form["function"];
            switch (procedure) {
                case "addCestaProduct":
                    addCestaProduct();
                    break;
                 case "removeCestaProduct":
                    removeCestaProduct();
                    break;
                case "EnviaRegistro_Click":
                    EnviaRegistro_Click();
                    break;
                case "SolicitaInfo":
                    SolicitaInfo();
                    break;
            }
            //addCestaProduct();
        }

       
        protected void addCestaProduct()
        {
             try
            {
                List<int> cesta = (List<int>)this.Session["cesta"];
                int idProducto = int.Parse(Request.Form["idProducto"]);
                int cantidad = int.Parse(Request.Form["cantidad"]);
                int costeunico = 0;
                int contdifPrecio = 0;

                if (cesta.Count(x => x == idProducto) < cantidad)
                {
                    while (cesta.Count(x => x == idProducto) < cantidad)
                    {
                        cesta.Add(idProducto);
                        contdifPrecio++;
                    }
                }
                else
                {
                    while (cesta.Count(x => x == idProducto) > cantidad)
                    {
                        int index = cesta.LastIndexOf(idProducto);
                        if (index != -1)
                        {
                            cesta.RemoveAt(index);
                            contdifPrecio--;
                        }
                    }
                }

                this.Session["cesta"] = cesta;

                DataTable dt1 = MarketplaceParent.getProduct(idProducto);

                //recupero el coste de una unidad del producto para calcular la diferencia de precio.
                foreach (DataRow tupla in dt1.Rows)
                {
                    costeunico = int.Parse(tupla["Precio"].ToString());
                }

                int difPrecio = costeunico * contdifPrecio;


                var jsonObject = new
                {
                    difPrecio = difPrecio.ToString(),
                    udsCesta = cesta.Count().ToString()
                };

                var jsonSerializer = new JavaScriptSerializer();
                string jsonString = jsonSerializer.Serialize(jsonObject);


                Literal1.Text = jsonString;
            }
            catch (Exception ex) {
                Literal1.Text = ex.ToString();
            }

        }


        protected void removeCestaProduct()
        {
            try
            {
                List<int> cesta = (List<int>)this.Session["cesta"];
                int idProducto = int.Parse(Request.Form["idProducto"]);

                
                while (cesta.Count(x => x == idProducto) > 0)
                {
                    cesta.Remove(idProducto);
                }

                this.Session["cesta"] = cesta;

                var jsonObject = new
                {
                    sol = 1
                };

                var jsonSerializer = new JavaScriptSerializer();
                string jsonString = jsonSerializer.Serialize(jsonObject);


                Literal1.Text = jsonString;
            }
            catch (Exception ex)
            {
                Literal1.Text = ex.ToString();
            }

        }

        protected void EnviaRegistro_Click()
        {
            try
            {
                //recuperamos data
                List<int> cesta = (List<int>)this.Session["cesta"];
                string nombre = Request.Form["nombre"];
                string municipio = Request.Form["municipio"];
                string provincia = Request.Form["provincia"];
                string telefono = Request.Form["telefono"];
                string mail = Request.Form["mail"];
                string articulos = Request.Form["articulos"];
                string preciototal = Request.Form["preciototal"];
                preciototal = preciototal.Replace(" €", "");


                //obtener identificador;
                int identificador = 0;

                string SQL = "SELECT ZID FROM ZDATACLIENT WHERE TELEFONO = '" + telefono + "' AND EMAIL = '" + mail + "'";
                Object con = DBHelper.ExecuteScalarSQL(SQL, null);
                if (con == null)
                {
                    SQL = "INSERT INTO ZDATACLIENT( NOMBRE, MUNICIPIO, PROVINCIA, EMAIL, FECHA, TELEFONO) ";
                    SQL += " VALUES('" + nombre + "','" + municipio + "','" + provincia + "','" + mail + "','" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + telefono + "')";
                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = " SELECT MAX(ZID) FROM ZDATACLIENT ";
                    Object con2 = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (con2 == null)
                    {
                    }
                    else
                    {
                        identificador = Convert.ToInt32(con2);
                    }
                }
                else
                {
                    identificador = Convert.ToInt32(con);
                }

                //Inserta el ticket
                SQL = "INSERT INTO ZPASARELA( NOMBRE, FECHA, CANTIDAD, IMPORTE, NUMLOOK, PASARELA) ";
                string ticket = DateTime.Now.ToString("yyMMddhh") + identificador; // identificador.ToString("###0");
                SQL += " VALUES('" + nombre + "','" + DateTime.Now.ToString("dd/MM/yyyy") + "','" + articulos + "','" + preciototal + "','" + identificador + "','" + ticket + "')";
                DBHelper.ExecuteNonQuery(SQL);

                //Recuperamos ide tiket
                int Pasarela = 0;
                SQL = " SELECT MAX(ZID) FROM ZPASARELA ";
                Object con1 = DBHelper.ExecuteScalarSQL(SQL, null);
                if (con1 == null)
                {
                    Pasarela = 0;
                }
                else
                {
                    Pasarela = Convert.ToInt32(con1);
                }


                //Inserta desglose del ticket
                HashSet<int> cestaUnica = new HashSet<int>();
                int price = 0;

                foreach (int idProducto in cesta)
                {
                    cestaUnica.Add(idProducto);
                }


                foreach (int idProducto in cestaUnica)
                {
                    DataTable dt1 = MarketplaceParent.getProduct(idProducto);

                    foreach (DataRow tupla in dt1.Rows)
                    {
                        price = int.Parse(tupla["Precio"].ToString());
                    }

                    int precioproductototal = cesta.Count(x => x == idProducto) * price;

                    SQL = "INSERT INTO ZPEDIDO ( ZIDPASARELA, ARTICULO, CANTIDAD, PRECIO, DESCUENTO, IMPORTE, FECHA, USUARIO) VALUES (";
                    SQL += Pasarela + ",'" + idProducto + "'," + cesta.Count(x => x == idProducto) + ",";
                    SQL += precioproductototal + ",0," + ticket + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "',";
                    SQL += identificador + ")";
                    DBHelper.ExecuteNonQuery(SQL);
                }

                //mail de pedido
                string msg = "";
                msg = "Total de Articulos Comprados: " + articulos + "<br/>";
                msg += "Por un Importe Total: "+ preciototal +" €" + "<br/>";
                msg += "Nº de cuenta para pagar el curso: Sabadell: ES68 0081 7175 5700 0161 3166" + "<br/>";
                msg += "El justificante de pago deberá enviarlo a formacion@viveroseresma.com" + "<br/>"+"<br/>";
                msg += "Guarde este número de justificante, para identificar su Compra: "+ ticket+ "<br/>" + "<br/>" + "<br/>";
                msg += "Muchas gracias por utilizar nuestros servicios<br/>";
                msg += "Atentamente: viveroseresma.com<br/>";

                SendEmailAsync("formacion@viveroseresma.com", null, msg);
                SendEmailAsync(mail, null, msg);

                //vaciamos la cesta
                this.Session["cesta"] = null;

                //enviamos ticket
                var jsonObject = new
                {
                    ticket = ticket
                };

                var jsonSerializer = new JavaScriptSerializer();
                string jsonString = jsonSerializer.Serialize(jsonObject);


                Literal1.Text = jsonString;
            }
            catch (Exception ex)
            {
                Literal1.Text = ex.ToString();
            }
        }

        protected void SolicitaInfo()
        {

            string nombre = Request.Form["nombre"];
            string municipio = Request.Form["municipio"];
            string provincia = Request.Form["provincia"];
            string telefono = Request.Form["telefono"];
            string mail = Request.Form["mail"];
            string articulo = Request.Form["product"];
            string productName = Request.Form["productName"];

            string msg = "";
            msg += "Han solicitado informacion sobre el articulo: " + productName + "<br/>";
            msg += "id: " + articulo + "<br/>";
            msg += "Nombre: " + nombre + "<br/>";
            msg += "Municipio: " + municipio + "<br/>";
            msg += "Provincia: " + provincia + "<br/>";
            msg += "Telefono: " + telefono + "<br/>";
            msg += "Mail: " + mail + "<br/>";




            string msgClient = "";

            msgClient += "Has solicitado informacion sobre: " + productName + "<br/>" + "<br/>";
            msgClient += "Recibiras un correo con la informacion solicitada desde el correo: formacion@viveroseresma.com<br/><br/>";
            msgClient += "Muchas gracias por utilizar nuestros servicios<br/>";
            msgClient += "Atentamente: viveroseresma.com<br/>";

            SendEmailAsyncInfo("formacion@viveroseresma.com", null, msg);
            SendEmailAsyncInfo(mail, null, msgClient);

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
                    From = new MailAddress("formacion@viveroseresma.com", "Contratación de Cursos"),
                    Subject = "Contratación de Cursos",
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
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }

        public Task SendEmailAsyncInfo(string email, string subject, string message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("formacion@viveroseresma.com", "i6as_ZE4B");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("formacion@viveroseresma.com", "Informacion de producto"),
                    Subject = "Informacion de producto",
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
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
