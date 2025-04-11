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

namespace Satelite
{
    public partial class MarketplaceProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    string grp = Request.QueryString["grp"];
                    if (grp != null)
                    {
                        group.InnerHtml = grp.ToString();

                        RepeaterSubgrup.DataSource = GetSubgrupos(grp);
                        RepeaterSubgrup.DataBind();
                    }
                    else
                    {
                        Server.Transfer("default" +
                        ".aspx");
                    }

                } catch (Exception ex)
                {
                    Server.Transfer("default" +
                        ".aspx");
                }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

            }
        }

        protected void RepeaterSubgrup_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string grp = Request.QueryString["grp"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtener el subgrupo para la fila actual
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                string subgrupo = rowView["Subgrupo"].ToString();

                // Obtener los productos para el subgrupo actual
                DataTable dtProductos = GetProductsBySubgrup(subgrupo, grp);

                // Obtener el RepeaterProductos para la fila actual
                Repeater RepeaterProductos = (Repeater)e.Item.FindControl("RepeaterProductos");

                // Establecer la fuente de datos del RepeaterProductos
                RepeaterProductos.DataSource = dtProductos;

                // Enlazar los datos al RepeaterProductos
                RepeaterProductos.DataBind();
            }
        }

        private DataTable GetSubgrupos(string grp)
        {
            DataTable dt1 = MarketplaceProducts.getSubgrups(grp);
            DataTable dt = new DataTable();

            //Generar columnas
            dt.Columns.Add("Idp", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Descp", typeof(string));
            dt.Columns.Add("Subgrupo", typeof(string));
            dt.Columns.Add("Url", typeof(string));

            foreach (DataRow tupla in dt1.Rows)
            {
             /*   string precio = " ";
                if (tupla["Precio"].ToString() != "0")
                {
                    precio = tupla["Precio"].ToString();
                }*/
                dt.Rows.Add(
                    tupla["Id"].ToString(),
                    tupla["Nombre"].ToString(),
                    tupla["Precio"].ToString(),
                    tupla["Descp"].ToString(),
                    tupla["Subgrupo"].ToString(),
                    tupla["Url"].ToString()
                );
            }

            return dt;
        }

        private DataTable GetProductsBySubgrup(string subgrupo, string grp)
        {
            DataTable dt1 = MarketplaceProducts.getProductSubgrups(subgrupo, grp);
            DataTable dt = new DataTable();

            //Generar columnas
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio", typeof(string));
            dt.Columns.Add("Descp", typeof(string));
            dt.Columns.Add("Url", typeof(string));

            foreach (DataRow tupla in dt1.Rows)
            {
                dt.Rows.Add(
                    tupla["Id"].ToString(),
                    tupla["Nombre"].ToString(),
                    tupla["Precio"].ToString(),
                    tupla["Descp"].ToString(),
                    tupla["Url"].ToString()
                );
            }

            return dt;
        }

        protected void sendCesta(object Source, EventArgs e)
        {
            List<int> cesta = (List<int>)this.Session["cesta"];

            List<int> nuevacesta = MarketplaceParent.sendCesta(Source, e, cesta);

            this.Session["cesta"] = nuevacesta;
        }

    }
}
