using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Satelite
{
    public partial class Marketplace : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getProductoDestacado();
        }

        private void getProductoDestacado()
        {
           

            DataTable dt1 = Marketplace.getDestacado();
            foreach (DataRow tupla in dt1.Rows)
            {
                prueba.InnerText = tupla["Nombre"].ToString();
                price.InnerText = tupla["Precio"].ToString() + "€";
                /*descp.InnerText = tupla[2].ToString();*/
                img.Src = tupla["Url"].ToString();
                eimg.Attributes["href"] = "MarketProduct.aspx?product=" + tupla["Id"].ToString();
                break;
            }
        }
    }
  

}