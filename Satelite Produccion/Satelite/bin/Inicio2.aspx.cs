using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Satelite
{
    public partial class Inicio2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean Esta = false;
            if (Session["Session"] != null)
            {
                if (!IsPostBack)
                {
                    string[] Partes = System.Text.RegularExpressions.Regex.Split(this.Session["MenuUsuario"].ToString(), Environment.NewLine);
                    for (int i = 0; i < Partes.Count() - 1; i++)
                    {
                        if (Partes[i].ToLower().Contains(this.Session["Default"].ToString()) == true)
                        {
                            //Aqui consulta con permisos de visualizacion
                            Object Con = DBHelper.ExecuteScalarSQL("SELECT ZPAGINA FROM ZMENU  WHERE ZTITULO = '" + this.Session["Default"].ToString() + "'", null);

                            if (Con is System.DBNull)
                            {
                                Server.Transfer("Login.aspx");
                            }
                            else
                            {
                                Server.Transfer(Con.ToString());
                            }
                            break;
                        }
                    }
                    for (int i = 0; i < Partes.Count() - 1; i++)
                    {
                        if (Partes[i].ToLower().Contains("inicio.aspx") == true)
                        {
                            Procesos_Entrada();
                            Procesos_OrdenCarga();
                            Produccionanual.Visible = true;
                            formularios.Visible = true;
                            PendienteImportacion.Visible = true;
                            DivVideo.Visible = false;
                            Esta = true;
                            break;
                        }
                    }
                    if(Esta == false)
                    {
                        Produccionanual.Visible = false;
                        formularios.Visible = false;
                        PendienteImportacion.Visible = false;
                        DivVideo.Visible = true;
                    }

                    //}
                }
                else
                {
                    string[] Partes = System.Text.RegularExpressions.Regex.Split(this.Session["MenuUsuario"].ToString(), Environment.NewLine);
                    for (int i = 0; i < Partes.Count() - 1; i++)
                    {
                        if (Partes[i].ToLower().Contains(this.Session["Default"].ToString()) == true)
                        {
                            //Aqui consulta con permisos de visualizacion
                            Object Con = DBHelper.ExecuteScalarSQL("SELECT ZPAGINA FROM ZMENU  WHERE ZTITULO = '" + this.Session["Default"].ToString() + "'", null);

                            if (Con is System.DBNull)
                            {
                                Server.Transfer("Login.aspx");
                            }
                            else
                            {
                                Server.Transfer(Con.ToString());
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                //Response.Redirect("Login.aspx"); //Default
                Server.Transfer("Login.aspx");
            }

            //ContentPlaceHolder cont = new ContentPlaceHolder();
            //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //HtmlGenericControl panel = this.Master.FindControl("DivEstructura") as HtmlGenericControl; //(HtmlGenericControl)cont.FindControl("DivEstructura");

            //panel.Visible = false;
        }
        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            //Response.Redirect("RecoNomina.aspx");
            Server.Transfer("Nomina.aspx");
        }

        protected void ARepeater_clik(object sender, EventArgs e)
        {
            //Nuevos
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl panel = cont.FindControl("collapseImportacion") as HtmlGenericControl; 
            

            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " ");
                if (Parte.Count() >= 3)
                {
                    this.Session["Param1"] = Parte[0];
                    this.Session["Param2"] = Parte[2];
                    this.Session["Param3"] = "Entradas";
                    this.Session["MiMenu"] = "RevisionLotes";
                    Server.Transfer("RevisionLotes.aspx");
                }
            }
        }

        protected void BRepeater_clik(object sender, EventArgs e)
        {
            //Nuevos
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl panel = cont.FindControl("collapseImportacion") as HtmlGenericControl;


            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " ");
                if (Parte.Count() >= 3)
                {
                    this.Session["Param1"] = Parte[0];
                    this.Session["Param2"] = Parte[2];
                    this.Session["Param3"] = "Finalizados";
                    this.Session["MiMenu"] = "RevisionLotes";
                    Server.Transfer("RevisionLotes.aspx");
                }
            }
        }

        protected void CRepeater_clik(object sender, EventArgs e)
        {
            //Nuevos
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl panel = cont.FindControl("collapseImportacion") as HtmlGenericControl;

            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " ");
                if (Parte.Count() >= 3)
                {
                    this.Session["Param1"] = Parte[0];
                    this.Session["Param2"] = Parte[2];
                    this.Session["Param3"] = "Importados";
                    this.Session["MiMenu"] = "RevisionLotes";
                    Server.Transfer("RevisionLotes.aspx");
                }
            }
        }

        protected void DRepeater_clik(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " contiene ");
                if (Parte.Count() >= 2)
                {
                    this.Session["Param1"] = Parte[0];
                    this.Session["Param2"] = Parte[1];
                    this.Session["Param3"] = "0, 1";
                    this.Session["MiMenu"] = "OrdenCarga";
                    Server.Transfer("OrdenCarga.aspx");
                }
            }
        }
        protected void ERepeater_clik(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " contiene ");
                if (Parte.Count() >= 2)
                {
                    this.Session["Param1"] = Parte[0];
                    this.Session["Param2"] = Parte[1];
                    this.Session["Param3"] = "2";
                    this.Session["MiMenu"] = "OrdenCarga";
                    Server.Transfer("OrdenCarga.aspx");
                }
            }
        }
        protected void FRepeater_clik(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlAnchor createAnchor = (System.Web.UI.HtmlControls.HtmlAnchor)sender;
            string a = createAnchor.InnerText;
            //MovAlma contiene 10 Lotes
            string[] Partes = System.Text.RegularExpressions.Regex.Split(createAnchor.InnerText, "/i>");
            if (Partes.Count() >= 1)
            {
                string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[1], " contiene ");
                if (Parte.Count() >= 2)
                {
                    this.Session["Param1"] = Parte[0];//Empresa
                    this.Session["Param2"] = Parte[1];// cantidad ordenes estado
                    this.Session["Param3"] = "3";
                    this.Session["MiMenu"] = "OrdenCarga";
                    Server.Transfer("OrdenCarga.aspx");
                }
            }
        }

        private void Procesos_Entrada()
        {
            int Cuanto = 0;
            RpImportacion0.DataSource = null;
            RpImportacion1.DataSource = null;

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

            SQL = " Select TOP 1 ZANO AS ANO  ";
            SQL += " FROM[dbo].[ANO_AGRICOLA] ";
            SQL += " WHERE ACTIVO = 1 ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow filas in dt1.Rows)
            {
                this.Session["Ano"] = filas["ANO"].ToString();
                break;
            }

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
            RpTOrden0.DataSource = null;
            RpTOrden1.DataSource = null;
            RpTOrden2.DataSource = null;

            string SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO, EMPRESA ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO in( 0, 1) ";
            SQL += "  GROUP BY ESTADO, EMPRESA ";
            SQL += " ORDER BY ESTADO, EMPRESA ";

            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow filas in dt1.Rows)
            {
                Io1.InnerText = filas["CUANTOS"].ToString();
                break;
            }

            Cuanto = dt1.Rows.Count;
            RpTOrden0.DataSource = dt1;
            RpTOrden0.DataBind();

            SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO, EMPRESA ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO = 2 ";
            SQL += "  GROUP BY ESTADO, EMPRESA ";
            SQL += " ORDER BY ESTADO, EMPRESA ";

            dt1 = Main.BuscaLote(SQL).Tables[0];
            Cuanto += dt1.Rows.Count;
            foreach (DataRow filas in dt1.Rows)
            {
                Io2.InnerText = filas["CUANTOS"].ToString();
                break;
            }
            RpTOrden1.DataSource = dt1;
            RpTOrden1.DataBind();

            SQL = " Select COUNT(NUMERO) CUANTOS,  ESTADO, EMPRESA ";
            SQL += " FROM[dbo].[ZCARGA_CABECERA] ";
            SQL += " WHERE ESTADO = 3 ";
            SQL += "  GROUP BY ESTADO, EMPRESA ";
            SQL += " ORDER BY ESTADO, EMPRESA ";

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

        [WebMethod]
        public static LineaCharts CantidadRegistros()
        {
            DataTable dt = new Datos().getDatos();

            if (dt != null && dt.Rows.Count > 0)
            {
                List<SeriesItem> series = new List<SeriesItem>();

                string[] fechas = new string[dt.Rows.Count];
                int[] sc = new int[dt.Rows.Count];
                int[] lp = new int[dt.Rows.Count];
                int[] cb = new int[dt.Rows.Count];

                int[] cuantos = new int[dt.Rows.Count];
                string[] unidades = new string[dt.Rows.Count];
                string[] variedad = new string[dt.Rows.Count];
                string[] fecha = new string[dt.Rows.Count];

                int i = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    //fechas[i] = dr[0].ToString();
                    cuantos[i] = Convert.ToInt32(dr[0].ToString());
                    unidades[i] = dr[1].ToString();
                    variedad[i] = dr[2].ToString();
                    fecha[i] = dr[3].ToString() + " - " + dr[4].ToString();

                    //sc[i] = Convert.ToInt32(dr[1].ToString());
                    //lp[i] = Convert.ToInt32(dr[2].ToString());
                    //cb[i] = Convert.ToInt32(dr[3].ToString());

                    i++;
                }
                series.Add(new SeriesItem() { name = "", data = cuantos });
                //series.Add(new SeriesItem() { name = "La Paz", data = unidades });
                //series.Add(new SeriesItem() { name = "Cochabamba", data = variedad });

                //series.Add(new SeriesItem() { name = "", data = sc });
                //series.Add(new SeriesItem() { name = "La Paz", data = lp });
                //series.Add(new SeriesItem() { name = "Cochabamba", data = cb });

                return new LineaCharts(series, fecha);

            }
            else
            {
                return null;
            }
        }
    }
}