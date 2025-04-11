using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;
using System.Data.SqlClient;
using static System.Data.Entity.Infrastructure.Design.Executor;
using DocumentFormat.OpenXml.Drawing.Charts;


namespace Satelite
{
    public partial class Inicio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean Esta = false;
            if (Session["Session"] != null)
            {
                if (!IsPostBack)
                {
                    
                    //if(this.Session["CargaPag"].ToString() == "0")
                    //{
                        //this.Session["CargaPag"] = "1";
                        GetChartTypes();
                    CargaDefectoCombos();

                    string SQL = "SELECT MESNUM, MES, AÑO, VARIEDAD, UNIDADES, SUM(CONVERT(int, NUM_UNIDADES))  AS CANTIDAD " + Environment.NewLine;
                    SQL += " FROM Satelite_Backup.dbo.ZGRAFICAS" + Environment.NewLine;
                    SQL += " WHERE " + Environment.NewLine; 
                        //SQL += " AÑO = '2023' ";
                        //SQL += " AND MESNUM = '03' ";
                        //SQL += " AND TIPO_FORM = 'PLANTAS' ";
                        SQL += " UNIDADES = 'PLANTAS' " + Environment.NewLine;
                        SQL += " GROUP BY MES, AÑO, MESNUM, VARIEDAD, UNIDADES " + Environment.NewLine;
                    SQL += " ORDER BY MES, AÑO, MESNUM " + Environment.NewLine;

                        this.Session["consulta"] = SQL;
                        GetChartData(SQL, 0);
                    //}
                    //GetChartData();


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
                            //Produccionanual.Visible = true;
                            formularios.Visible = true;
                            PendienteImportacion.Visible = true;
                            DivVideo.Visible = false;
                            Esta = true;
                            break;
                        }
                    }
                    if(Esta == false)
                    {
                        //Produccionanual.Visible = false;
                        formularios.Visible = false;
                        PendienteImportacion.Visible = false;
                        DivVideo.Visible = true;
                    }

                    //}
                }
                else
                {
                    //CargaDefectoCombos();
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
            //if (Session["JSChecked"] == null)
            ////JSChecked -indicates if it tried to run the javascript version
            //{
            //    // prevent infinite loop
            //    Session["JSChecked"] = "Checked";
            //    string path = Request.Url + "?JScript=1";
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect",
            //      "window.location.href='" + path + "';", true);
            //}
            //if (Request.QueryString["JScript"] == null)
            //    Response.Write("JavaScript is not enabled.");
            //else
            //    Response.Write("JavaScript is enabled.");

            //this.btncolumna.Attributes.Add("OnClick", "javascript:return fnAceptar();");

        }

        private void GetChartTypes()
        {
            foreach (int chartType in Enum.GetValues(typeof(SeriesChartType)))
            {
                ListItem li = new ListItem(Enum.GetName(typeof(SeriesChartType),
                    chartType), chartType.ToString());
                cbchart.Items.Add(li);
            }
        }
        protected void cbchart_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetChartData();

            this.Chart1.Series["Series1"].ChartType = (SeriesChartType)Enum.Parse(
                typeof(SeriesChartType), cbchart.SelectedValue);

            GetChartData(this.Session["consulta"].ToString(), 0);

        }
        protected void DrForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData(this.Session["consulta"].ToString(), 0);
        }
        protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData(this.Session["consulta"].ToString(), 0);
        }

        protected void DrUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData(this.Session["consulta"].ToString(), 0);
        }

        protected void DrMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData(this.Session["consulta"].ToString(), 0);
        }

        protected void Drano_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChartData(this.Session["consulta"].ToString(), 0);
        }
        protected void cbConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Consulta seleccionada
            Object Con = DBHelper.ExecuteScalarSQL(DrConsulta.SelectedValue, null);
            if (Con is System.DBNull)
            {
                Lbmensaje.Text = "Esta Consulta no devuelve resultados <br/> <br/>";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }
            else
            {
                GetChartData(DrConsulta.SelectedValue, 0);
                //string SQL = " SELECT * FROM ZCARGA_LINEA  ";
                //SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                //SQL += " ORDER BY POSICIONCAMION ";
                //DataTable dt = Main.BuscaLote(SQL).Tables[0];
            }

            System.Data.DataTable Consulta = Main.BuscaLote(DrConsulta.SelectedValue).Tables[0];
        }

        private void CargaDefectoCombos()
        {
            DrVariedad.Items.Clear();
            DrUnidades.Items.Clear();
            DrMes.Items.Clear();
            Drano.Items.Clear();
            DrForms.Items.Clear();
            DrConsulta.Items.Clear();

            DrVariedad.DataValueField = "VARIEDAD";
            DrVariedad.DataTextField = "VARIEDAD";

            string SQL = "SELECT DISTINCT(VARIEDAD) FROM ZGRAFICAS ORDER BY VARIEDAD";
            System.Data.DataTable dt = Main.BuscaLote(SQL).Tables[0];
            DrVariedad.Items.Add("Todos");
            DrVariedad.DataSource = dt;
            DrVariedad.DataBind();

            Drano.DataValueField = "AÑO";
            Drano.DataTextField = "AÑO";

            SQL = "SELECT DISTINCT(AÑO) FROM ZGRAFICAS ORDER BY AÑO";
            dt = Main.BuscaLote(SQL).Tables[0];
            Drano.Items.Add("Todos");
            Drano.DataSource = dt;
            Drano.DataBind();

            DrForms.DataValueField = "TIPO_FORM";
            DrForms.DataTextField = "TIPO_FORM";

            SQL = "SELECT DISTINCT(TIPO_FORM) FROM ZGRAFICAS ORDER BY TIPO_FORM";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrForms.Items.Add("Todos");
            DrForms.DataSource = dt;
            DrForms.DataBind();

            DrUnidades.DataValueField = "UNIDADES";
            DrUnidades.DataTextField = "UNIDADES";

            SQL = "SELECT DISTINCT(UNIDADES) FROM ZGRAFICAS ORDER BY UNIDADES";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrUnidades.Items.Add("Todos");
            DrUnidades.DataSource = dt;
            DrUnidades.DataBind();

            DrMes.DataValueField = "MESNUM";
            DrMes.DataTextField = "MESNUM";

            SQL = "SELECT DISTINCT(MESNUM), MES FROM ZGRAFICAS";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrMes.Items.Add("Todos");
            DrMes.DataSource = dt;
            DrMes.DataBind();

            DrConsulta.DataValueField = "ZQUERY";
            DrConsulta.DataTextField = "ZTITULO";

            SQL = "SELECT DISTINCT(ZQUERY), ZTITULO FROM ZCONSULTAS";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrConsulta.Items.Add("Todos");
            DrConsulta.DataSource = dt;
            DrConsulta.DataBind();

        }
        private void LimpiaConsulta(string Consulta)
        {
            string SQL = Consulta;
            string Dato = "";
            Boolean Esta = false;
            string[] Partes = System.Text.RegularExpressions.Regex.Split(SQL, Environment.NewLine);
            SQL = "";

            if (Partes.Count() >= 1)
            {
                for (int i = 0; i < Partes.Count() - 1; i++)
                {
                    if (Partes[i].Contains("WHERE") == true)
                    {
                        Esta = true;
                        if (Drano.SelectedValue == "Todos")
                        {
                        }
                        else
                        {
                            if (Dato == "")
                            {
                                Dato += "WHERE AÑO = '" + Drano.SelectedValue + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Dato += "AND AÑO = '" + Drano.SelectedValue + "'" + Environment.NewLine;
                            }
                        }

                        if (DrVariedad.SelectedValue == "Todos")
                        {
                        }
                        else
                        {
                            if (Dato == "")
                            {
                                Dato += "WHERE VARIEDAD = '" + DrVariedad.SelectedValue + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Dato += "AND VARIEDAD = '" + DrVariedad.SelectedValue + "'" + Environment.NewLine;
                            }
                        }

                        if (DrUnidades.SelectedValue == "Todos")
                        {
                        }
                        else
                        {
                            if (Dato == "")
                            {
                                Dato += "WHERE UNIDADES = '" + DrUnidades.SelectedValue + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Dato += "AND UNIDADES = '" + DrUnidades.SelectedValue + "'" + Environment.NewLine;
                            }
                        }

                        if (DrMes.SelectedValue == "Todos")
                        {
                        }
                        else
                        {
                            if (Dato == "")
                            {
                                Dato += "WHERE MESNUM = '" + DrMes.SelectedValue + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Dato += "AND MESNUM = '" + DrMes.SelectedValue + "'" + Environment.NewLine;
                            }
                        }

                        if (DrForms.SelectedValue == "Todos")
                        {
                        }
                        else
                        {
                            if (Dato == "")
                            {
                                Dato += "WHERE TIPO_FORM = '" + DrForms.SelectedValue + "'" + Environment.NewLine;
                            }
                            else
                            {
                                Dato += "AND TIPO_FORM = '" + DrForms.SelectedValue + "'" + Environment.NewLine;
                            }
                        }
                        if(Dato == "")
                        {
                            SQL += "WHERE" + Environment.NewLine;
                        }
                        else
                        {
                            SQL += Dato + Environment.NewLine;
                        }
                    }
                    else
                    {
                        if (Partes[i].Contains("SELECT") == true)
                        {
                            SQL += Partes[i] + Environment.NewLine;
                        }
                        else if (Partes[i].ToUpper().Contains("FROM ") == true)
                        {
                            SQL += Partes[i] + Environment.NewLine;
                        }
                        else if (Partes[i].ToUpper().Contains("GROUP BY") == true)
                        {
                            if(Esta == false)
                            {
                                if (Drano.SelectedValue == "Todos")
                                {
                                }
                                else
                                {
                                    if (Dato == "")
                                    {
                                        Dato += "WHERE AÑO = '" + Drano.SelectedValue + "'" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        Dato += "AND AÑO = '" + Drano.SelectedValue + "'" + Environment.NewLine;
                                    }
                                }

                                if (DrVariedad.SelectedValue == "Todos")
                                {
                                }
                                else
                                {
                                    if (Dato == "")
                                    {
                                        Dato += "WHERE VARIEDAD = '" + DrVariedad.SelectedValue + "'" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        Dato += "AND VARIEDAD = '" + DrVariedad.SelectedValue + "'" + Environment.NewLine;
                                    }
                                }

                                if (DrUnidades.SelectedValue == "Todos")
                                {
                                }
                                else
                                {
                                    if (Dato == "")
                                    {
                                        Dato += "WHERE UNIDADES = '" + DrUnidades.SelectedValue + "'" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        Dato += "AND UNIDADES = '" + DrUnidades.SelectedValue + "'" + Environment.NewLine;
                                    }
                                }

                                if (DrMes.SelectedValue == "Todos")
                                {
                                }
                                else
                                {
                                    if (Dato == "")
                                    {
                                        Dato += "WHERE MESNUM = '" + DrMes.SelectedValue + "'" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        Dato += "AND MESNUM = '" + DrMes.SelectedValue + "'" + Environment.NewLine;
                                    }
                                }

                                if (DrForms.SelectedValue == "Todos")
                                {
                                }
                                else
                                {
                                    if (Dato == "")
                                    {
                                        Dato += "WHERE TIPO_FORM = '" + DrForms.SelectedValue + "'" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        Dato += "AND TIPO_FORM = '" + DrForms.SelectedValue + "'" + Environment.NewLine;
                                    }
                                }
                                if (Dato == "")
                                {
                                }
                                else
                                {
                                    SQL += Dato + Environment.NewLine;
                                }
                            }
                            SQL += Partes[i] + Environment.NewLine;
                        }
                        else if (Partes[i].ToUpper().Contains("ORDER BY") == true)
                        {
                            SQL += Partes[i] + Environment.NewLine;
                        }
                    }
                }
            }
            Dato = "";

            Partes = System.Text.RegularExpressions.Regex.Split(SQL, Environment.NewLine);
            if (Partes.Count() >= 1)
            {
                for (int i = 0; i < Partes.Count() - 1; i++)
                {
                    if (Partes[i] == "" || Partes[i] == Environment.NewLine || Partes[i] == " ")
                    {
                    }
                    else
                    {
                        Dato += Partes[i] + Environment.NewLine;
                    }
                }

                Partes = System.Text.RegularExpressions.Regex.Split(Dato, Environment.NewLine);
                Dato = "";

                for (int i = 0; i < Partes.Count() - 1; i++)
                {
                    if (Partes[i] != "")
                    {
                        if (Partes[i].ToUpper().Contains("WHERE"))
                        {
                            if (Partes[i + 1].ToUpper().Contains("GROUP"))
                            {
                            }
                            else
                            {
                                Dato += Partes[i] + Environment.NewLine;
                            }
                        }
                        else
                        {
                            Dato += Partes[i] + Environment.NewLine;
                        }

                    }
                }
            }

            this.Session["consulta"] = Dato;
        }
        private void GetChartData(string Consulta, int Index)
        {
            //string cs = ConfigurationManager.ConnectionStrings["connectionSQL"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(cs))
            //{
            // Query to retrieve database data for the chart control
            //SqlCommand cmd = new SqlCommand
            //    ("Select StudentName, TotalMarks from Students", con);
            LimpiaConsulta(Consulta);

            //string SQL = "SELECT MESNUM, MES, AÑO, VARIEDAD, UNIDADES, SUM(CONVERT(int, NUM_UNIDADES))  AS CANTIDAD ";
            //SQL += " FROM Satelite_Backup.dbo.ZGRAFICAS";
            //SQL += " WHERE ";
            //SQL += " AÑO = '2023' ";
            //SQL += " AND MESNUM = '03' ";
            //SQL += " AND UNIDADES = 'PLANTAS' ";
            //SQL += " GROUP BY MES, AÑO, MESNUM, VARIEDAD, UNIDADES ";
            //SQL += " ORDER BY MES, AÑO, MESNUM ";






            System.Data.DataTable dt = Main.BuscaLote(this.Session["consulta"].ToString()).Tables[0];
            // Specify the column name that contains values for X-Axis
            Chart1.Series[0].Points.Clear();

            Chart1.ChartAreas[0].AxisX.Minimum = 10;
            Chart1.ChartAreas[0].AxisX.Maximum = 50;
            Chart1.Series["Series1"].XValueMember = "VARIEDAD";
            Chart1.Series["Series1"].ToolTip = Chart1.Series["Series1"].XValueMember;

            // Specify the column name that contains values for Y-Axis
            Chart1.Series["Series1"].YValueMembers = "CANTIDAD";
                // Set the datasource
                Chart1.DataSource = dt;
                // Finally call DataBind()
                Chart1.DataBind();
            //}
        }
        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            MiOpenMenu();
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            //BtGralConsulta_Click(sender, e);

            //Lbmensaje.Text = "Ya";
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            //DivProgress.Visible = false;
            MiOpenMenu();
        }

        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            MiOpenMenu();
        }
        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
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

            System.Data.DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
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

            System.Data.DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
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
            System.Data.DataTable dt = new Datos().getDatos();

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
                series.Add(new SeriesItem() { name = "Santa Cruz", data = cuantos });
                //series.Add(new SeriesItem() { name = "La Paz", data = unidades });
                //series.Add(new SeriesItem() { name = "Cochabamba", data = variedad });

                //series.Add(new SeriesItem() { name = "Santa Cruz", data = sc });
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