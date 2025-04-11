using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace QRCode_Demo
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Session"] != null)
            {
                if (!IsPostBack)
                {
                    if (this.Session["MiNivel"].ToString() == "9")
                    {
                        Nominas.Visible = true;
                    }
                    Procesos_Entrada();
                    Procesos_OrdenCarga();
                }
            }
            else
            {
                Response.Redirect("Login.aspx"); //Default
            }

        }

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecoNomina.aspx");
        } 

        private void Procesos_Entrada()
        {
            int Cuanto = 0;
            string SQL = " Select COUNT(TIPO_FORM) CUANTOS, TIPO_FORM, ESTADO ";
            SQL += " FROM[dbo].[ZENTRADA] ";
            SQL += " WHERE ESTADO is null ";
            SQL += " GROUP BY TIPO_FORM, ESTADO ";
            SQL += " ORDER BY ESTADO, TIPO_FORM ";

            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            Rpt0.InnerText = dt1.Rows.Count.ToString();
            Cuanto = dt1.Rows.Count;
            RpImportacion0.DataSource = dt1;
            RpImportacion0.DataBind();

            SQL = " Select COUNT(TIPO_FORM) CUANTOS, TIPO_FORM, ESTADO ";
            SQL += " FROM[dbo].[ZENTRADA] ";
            SQL += " WHERE ESTADO in (1) ";
            SQL += " GROUP BY TIPO_FORM, ESTADO ";
            SQL += " ORDER BY ESTADO, TIPO_FORM ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            Cuanto += dt1.Rows.Count;
            Rpt1.InnerText = dt1.Rows.Count.ToString();
            RpImportacion1.DataSource = dt1;
            RpImportacion1.DataBind();

            SQL = " Select COUNT(TIPO_FORM) CUANTOS, TIPO_FORM, ESTADO ";
            SQL += " FROM[dbo].[ZENTRADA] ";
            SQL += " WHERE ESTADO in (2) ";
            SQL += " GROUP BY TIPO_FORM, ESTADO ";
            SQL += " ORDER BY ESTADO, TIPO_FORM ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            Cuanto += dt1.Rows.Count;
            Rpt2.InnerText = dt1.Rows.Count.ToString();
            RpImportacion2.DataSource = dt1;
            RpImportacion2.DataBind();

            T1.InnerText = Cuanto.ToString();

            //grafica de geneticas enviadas por pais
            //SELECT A.PAIS, B.ARTICULO, SUM(B.NUMPALET) AS CUANTOS_PALETS
            //FROM[dbo].[ZCARGA_CABECERA] A
            //INNER JOIN[dbo].[ZCARGA_LINEA] B ON A.ID_SECUENCIA = B.ID_CABECERA
            //GROUP BY A.PAIS, B.ARTICULO
            //ORDER BY A.PAIS, B.ARTICULO

            //Lotes a verificar
            //SELECT * FROM [DESARROLLO].[dbo].[ZLOTESCREADOS] WHERE ZESTADO<> -1
        }

        private void Procesos_OrdenCarga()
        {
            int Cuanto = 0;
            string SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO = 0 ";
            SQL += "  GROUP BY ESTADO ";
            SQL += " ORDER BY ESTADO "; 

            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow filas in dt1.Rows)
            {
                Io1.InnerText = filas["CUANTOS"].ToString();
                break;
            }

            Cuanto = dt1.Rows.Count;
            RpTOrden0.DataSource = dt1;
            RpTOrden0.DataBind();

            SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO = 2 ";
            SQL += "  GROUP BY ESTADO ";
            SQL += " ORDER BY ESTADO ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            Cuanto += dt1.Rows.Count;
            foreach (DataRow filas in dt1.Rows)
            {
                Io2.InnerText = filas["CUANTOS"].ToString();
                break;
            }
            RpTOrden1.DataSource = dt1;
            RpTOrden1.DataBind();

            SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO = 3 ";
            SQL += "  GROUP BY ESTADO ";
            SQL += " ORDER BY ESTADO ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            Cuanto += dt1.Rows.Count;
            foreach (DataRow filas in dt1.Rows)
            {
                Io3.InnerText = filas["CUANTOS"].ToString();
                break;
            }
            RpTOrden2.DataSource = dt1;
            RpTOrden2.DataBind();

            T2.InnerText = Cuanto.ToString();
        }
    }
}