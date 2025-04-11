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
    public partial class MarketSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
                {
                    string search = Request.QueryString["search"];
                    if (search != null)
                    {
                        RepeaterShearch.DataSource = getSearch(search);
                        RepeaterShearch.DataBind();
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

        private DataTable getSearch(string search)
        {
            DataTable dt1 = MarketSearch.getSearchs(search);
            DataTable dt = new DataTable();

            //Generar 
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Url", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Precio", typeof(string));

            foreach (DataRow tupla in dt1.Rows)
            {
                dt.Rows.Add(
                    tupla["Id"].ToString(),
                    tupla["Url"].ToString(),
                    tupla["Nombre"].ToString(),
                    tupla["Precio"].ToString()
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
