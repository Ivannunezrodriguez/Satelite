using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Security.Cryptography;
using Satelite;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Satelite
{
    public partial class MarketProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                try
                {
                    string productId = Request.QueryString["product"];
                    if (productId != null)
                    {
                        getProductoId(productId);
                        RepeaterGaleria.DataSource = GetGaleria(productId);
                        RepeaterGaleria.DataBind();
                    }
                    else
                    {
                        Server.Transfer("default.aspx");
                    }

                }
                catch (Exception ex)
                {
                    Server.Transfer("default.aspx");
                }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

            }
        }

        protected void RepeaterGaleria_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var radioBtn = e.Item.FindControl("navegacion") as RadioButton;

                // Obtén los datos para el ítem actual del Repeater
                var dataItem = e.Item.DataItem as DataRowView;
                var isChecked = dataItem["IsChecked"].ToString();

                if (isChecked == "True")
                {
                    radioBtn.Checked = true;
                }
                else
                {
                    radioBtn.Checked = false;
                }
            }
        }

        private DataTable GetGaleria(string productId)
        {
            DataTable dt1 = MarketProduct.getGaleria(productId);
            DataTable dt = new DataTable();

            //Generar 
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Url", typeof(string));
            dt.Columns.Add("Check", typeof(string));

            int i = 1;
            string check = "";

            foreach (DataRow tupla in dt1.Rows)
            {
                string istr = "_"+i.ToString();
                if (i == 1)
                {
                    check = "checked";
                }
                else
                {
                    check = "";
                }

                dt.Rows.Add(
                    istr,
                    tupla["Url"].ToString(),
                    check
                );

                i++;
            }

            return dt;
        }

        private void getProductoId(string productId)
        {
           

            DataTable dt1 = Marketplace.getProductoId(productId);
            foreach (DataRow tupla in dt1.Rows)
            {
                titulo.InnerHtml = tupla["Nombre"].ToString();
                if (int.Parse(tupla["Precio"].ToString()) != 0)
                {
                    price.InnerHtml = tupla["Precio"].ToString() + "€";
                }
                else
                {
                    comprar.Visible = false;
                    consultar.Visible = true;
                }
                desc.InnerHtml = tupla["Descp"].ToString();
                esptec.InnerHtml = tupla["Esptec"].ToString();
                comprar.Attributes["data-id"] = productId;
            break;
            }
        }

        protected void comprarProduct(object Source, EventArgs e)
        {
            List<int> cesta = (List<int>)this.Session["cesta"];

            List<int> nuevacesta = MarketplaceParent.sendCesta(Source, e, cesta);

            this.Session["cesta"] = nuevacesta;

        }
    }
}