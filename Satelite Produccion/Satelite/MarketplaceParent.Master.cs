using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Satelite
{
    public partial class MarketplaceParent : System.Web.UI.MasterPage
    {
        static int totalPrice = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            RepeaterProductGrup.DataSource = getGroups();
            RepeaterProductGrup.DataBind();

            //Si es la primera vez que se recarga 
            if (this.Session["cesta"] == null)
            {
                List<int> cesta = new List<int>();
                this.Session["cesta"] = cesta;
            }
            else
            {
                generarDivCesta();

            }
         }

        protected DataTable getGroups()
        {
            DataTable dt1 = MarketplaceProducts.getProductGrups();
            DataTable dt = new DataTable();

            //Generar columnas
            dt.Columns.Add("Grupo", typeof(string));

            foreach (DataRow tupla in dt1.Rows)
            {
                dt.Rows.Add(
                    tupla["Grupo"].ToString()
                );
            }

            return dt;
        }
        public static List<int> sendCesta(object Source, EventArgs e, List<int> cesta)
        {
            //Recuperar el id del producto añadido a la cesta
            string idProducto = ((HtmlButton)Source).Attributes["data-id"];

            //recuperar variable de sesion
            cesta.Add(int.Parse(idProducto));

            //modifico el contador de productos en la  
            Page currentPage = HttpContext.Current.Handler as Page;
            ((MarketplaceParent)currentPage.Master).cont_cesta.InnerHtml = cesta.Count.ToString();
            //apartir de aqui se deberia hacer con Jquery
            ((MarketplaceParent)currentPage.Master).addDivCesta(int.Parse(idProducto));


            return cesta;
        }


        protected void addDivCesta(int idProducto)
        {

            //recuperamos cesta
            List<int> cesta = (List<int>)this.Session["cesta"];
            string title = "";
            string foto = "";
            int price = 0;

            //Recupero dato del producto
            DataTable dt1 = MarketplaceParent.getProduct(idProducto);

            foreach (DataRow tupla in dt1.Rows)
            {
                title = tupla["Nombre"].ToString();
                foto = tupla["Url"].ToString();
                price = int.Parse(tupla["Precio"].ToString());
            }

            //comprobar si el producto esta ya en la cesta -> COUN.ID > 5
            int count = cesta.Count(x => x == idProducto);

            if (count == 1)
            {
                string row = "";
                row += "<tr id=\""+idProducto+"\">" +
                            "<td class=\"uds_producto_tabla\">" +
                                "<input id=\"cantidad_producto_"+idProducto+"\" type=\"number\" min=\"1\" onchange=\"add_uds_cesta('" + idProducto + "')\" value=\"1\">" +
                            "</td>" +
                            "<td  class=\"img_producto_tabla\">" +
                                "<a href=\"MarketProduct.aspx?product="+idProducto+"\">" +
                                    "<img src=\"" + foto + "\" />" +
                                "</a>" +
                            "</td>" +
                            "<td class=\"title_producto_tabla\">" +
                                title +
                            "</td>" +
                            "<td id=\"precio_producto_"+idProducto+"\" class=\"price_producto_tabla\">" +
                            price + " €</td>" +
                            "<td id=\"precio_producto_" + idProducto + "\" class=\"price_producto_tabla\">" +
                                " <svg width=\"16\" height=\"16\" fill=\"currentColor\" class=\"bi bi-trash3-fill trash\" viewBox=\"0 0 16 16\" onclick=\"remove_cesta('\" + idProducto + \"')\">\r\n  <path d=\"M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z\"/>\r\n</svg>" +
                            "</td>" +
                       "</tr>";

                tabla_produduto_body.InnerHtml += row;

                //RECALCULAR PRECIO TOTAL
                totalPrice = totalPrice + price;
                total_price.InnerHtml = totalPrice.ToString() + " €";
                LbCantidad.InnerHtml = cesta.Count().ToString();
                LbTotal.InnerHtml = totalPrice.ToString() + " €";
            }
            else
            {
                generarDivCesta();
            }
           

           

        }

       /* public static void sendGenCetsta(List<int> cesta)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            ((MarketplaceParent)currentPage.Master).cont_cesta.InnerHtml = cesta.Count.ToString();
            //apartir de aqui se deberia hacer con Jquery
            ((MarketplaceParent)currentPage.Master).generarDivCesta();
        }*/

        protected void generarDivCesta()
        {
            List<int> cesta = (List<int>)this.Session["cesta"];
            HashSet<int> cestaUnica = new HashSet<int>();
            totalPrice = 0;

            tabla_produduto_body.InnerHtml = "";

            if (cesta.Count > 0)
            {
                cont_cesta.InnerHtml = cesta.Count.ToString();
            }

            foreach (int idProducto in cesta)
            {
                cestaUnica.Add(idProducto);
            }

            foreach (int idProducto in cestaUnica)
            {

                string title = "";
                string foto = "";
                int price = 0;

                //Recupero dato del producto
                DataTable dt1 = MarketplaceParent.getProduct(idProducto);

                foreach (DataRow tupla in dt1.Rows)
                {
                    title = tupla["Nombre"].ToString();
                    foto = tupla["Url"].ToString();
                    price = int.Parse(tupla["Precio"].ToString());
                }

                //comprobar si el producto esta ya en la cesta -> COUN.ID > 5
                int count = cesta.Count(x => x == idProducto);

                string row = "";
                row += "<tr id=\""+idProducto+"\">" +
                            "<td class=\"uds_producto_tabla\">" +
                                "<input id=\"cantidad_producto_"+idProducto+"\" type=\"number\" min=\"1\" onchange=\"add_uds_cesta('" + idProducto + "')\" value=\""+ count + "\">" +
                            "</td>" +
                            "<td  class=\"img_producto_tabla\">" +
                                "<a href=\"MarketProduct.aspx?product=" + idProducto + "\">" +
                                    "<img src=\"" + foto + "\" />" +
                                "</a>" +
                            "</td>" +
                            "<td class=\"title_producto_tabla\">" +
                                title +
                            "</td>" +
                            "<td id=\"precio_producto_"+idProducto+"\" class=\"price_producto_tabla\">" +
                                price* count + " €" +
                            "</td>" +
                             "<td id=\"precio_producto_" + idProducto + "\" class=\"price_producto_tabla\">" +
                                " <svg width=\"16\" height=\"16\" fill=\"currentColor\" class=\"bi bi-trash3-fill trash\" viewBox=\"0 0 16 16\" onclick=\"remove_cesta('" + idProducto + "')\">\r\n  <path d=\"M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z\"/>\r\n</svg>" +
                            "</td>" +
                        "</tr>";

                tabla_produduto_body.InnerHtml += row;


                
                //RECALCULAR PRECIO TOTAL
                totalPrice = totalPrice + (price * cesta.Count(x => x == idProducto));
                total_price.InnerHtml = totalPrice.ToString() + " €";
                LbCantidad.InnerHtml = cesta.Count().ToString();
                LbTotal.InnerHtml = totalPrice.ToString() + " €";
            }
        }
    }
}