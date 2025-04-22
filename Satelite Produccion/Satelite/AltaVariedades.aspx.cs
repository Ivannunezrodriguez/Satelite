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
using System.Data.Odbc;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using ZXing;
using DocumentFormat.OpenXml.Presentation;
using System.Web.Services.Description;
using static ClosedXML.Excel.XLPredefinedFormat;
using Satelite.Reports;

namespace Satelite
{
    public partial class AltaVariedades : System.Web.UI.Page
    {
        Reports.ReportListCamion dtsE = null;

 
        private string ElID = "";
        private string ElOrden = "";
        private string ElOrdenControl = "";
        private string ElOrdenLista = "";

        static TextBox[] ArrayTextBoxs;
        static Label[] ArrayLabels;
        static DropDownList[] ArrayCombos;
        private int Indice = 0;

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Session["Procesa"] = "0";

                if (Session["Session"] == null)
                {
                    this.Session["Error"] = "0";
                    Server.Transfer("Login.aspx"); //Default
                }

                if (this.Session["MiNivel"].ToString() != "9")
                {
                    Server.Transfer("Inicio.aspx"); //Default
                }

                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    Server.Transfer("Login.aspx"); //Default
                }

                dtsE = new Reports.ReportListCamion();

                if (!IsPostBack)
                {
                    this.Session["IDSecuencia"] = "0";
                    this.Session["IDProcedimiento"] = "0";
                    //this.Session["DESARROLLO"] = "0";
                    this.Session["IDGridA"] = "0";
                    this.Session["IDGridB"] = "0";
                    this.Session["Filtro"] = "";
                    this.Session["NumeroPalet"] = "0";
                    this.Session["FiltroEmpresa"] = "";
                    this.Session["FiltroFecha"] = "";
                    this.Session["FiltroCliente"] = "";
                    this.Session["FiltroRuta"] = "";
                    this.Session["Menu"] = "1";
                    this.Session["Cerrados"] = "0";
                    this.Session["IDCabecera"] = "0";
                    this.Session["collapse1"] = "1";
                    this.Session["collapse2"] = "1";
                    this.Session["collapse3"] = "1";
                    this.Session["collapse4"] = "1";
                    this.Session["collapse5"] = "1";
                    this.Session["collapse6"] = "1";
                    this.Session["CalculoPaletPlanta"] = "";
                    this.Session["EstadoCabecera"] = "";
                    this.Session["ElIDaBorrar"] = "";
                    this.Session["Centro"] = "";
                    this.Session["GridOrden"] = "";
                    this.Session["GridEdicion"] = "0";
                    //accion de la linea (linea actual; nueva linea; Id cabecera, Id; NUMERO_LINEA)  
                    this.Session["ModificaLinea"] = "";
                    this.Session["SelectLinea"] = "";
                    this.Session["sortExpression"] = "";
                    this.Session["GVBandejaID"] = "";
                    this.Session["GVGoldenID"] = "";
                    this.Session["GVVariedadID"] = "";
                    this.Session["Modificar"] = "0";



                    DataTable dt = new DataTable();
                    this.Session["TablaLista"] = dt;
                    //ChkSlot.Visible = false;
                    Variables.mensajeserver = "";
                    ArrayTextBoxs = new TextBox[20];
                    ArrayCombos = new DropDownList[20];
                    ArrayLabels = new Label[20];
                    Lberror.Visible = false;
                    //Carga_Filtros();
                    Carga_Tablas_Variedades();
                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                string b = ex.Message;
                if (this.Session["Error"].ToString() == "0")
                {
                    Server.Transfer("Login.aspx");
                }
                else
                {
                    Server.Transfer("thEnd.aspx");
                }
            }
            DejaPosAcordeon();
            LimpiaCajasPrint();
        }

        protected void lbFilClose_Click(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            if (btn.ID == "btn")
            {
                //Ia = (HtmlGenericControl)INombre;
                //Tx = (TextBox)TxtCodigo;
            }


            //Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //Ia.Attributes["title"] = "Desactivado";
            //Ia.Attributes.Add("style", "color:darkred;");
            Tx.Attributes.Add("style", "");
            Tx.Text = "";
        }

        protected void BtBuscaUd_Click(Object sender, EventArgs e) //  1
        {
            DrBandejas.Items.Clear();
            DrCodgolden.Items.Clear();
            DrTipoplanta.Items.Clear();

            DrBandejas.DataValueField = "ZID";
            DrBandejas.DataTextField = "ZTIPO_PLANTA";
            DrCodgolden.DataValueField = "ZID";
            DrCodgolden.DataTextField = "ZTIPO_PLANTA";
            DrTipoplanta.DataValueField = "ZID";
            DrTipoplanta.DataTextField = "ZTIPO_PLANTA";

            if (TxtUdBase.Text == "" && TxtCodGolden.Text == "" && TxtVariedad.Text == "" && TextTipoFormato.Text == "" && TxtTipoPlanta.Text == "" && TxtDTipoPlanta.Text == "" && TxtNumPlanta.Text == "" )
            {
                Carga_Tablas_Variedades();

            }
            else
            {
                string SQLCombo1 = " SELECT DISTINCT(ZTIPO_PLANTA), ZID, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE";
                string SQLCombo2 = " SELECT  DISTINCT(ZTIPO_PLANTA), ZID, ZTIPO_FORMATO, ZNUMERO_PLANTAS FROM ZBANDEJAS WHERE";
                string SQLCombo3 = " SELECT  DISTINCT(ZTIPO_PLANTA), ZID, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP WHERE";

                string SQLGolden = " SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE";
                string SQLBandejas = " SELECT  ZID, ZTIPO_PLANTA, ZTIPO_FORMATO, ZNUMERO_PLANTAS FROM ZBANDEJAS WHERE";
                string SQLTipoPlanta = " SELECT  ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP WHERE";

                DataTable dt = null;

                if (TxtUdBase.Text != "")
                {
                    string a = SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5);

                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5,5) == "WHERE")
                    {
                        SQLGolden += " UD_BASE = '" + TxtUdBase.Text + "' ";
                        SQLCombo1 += " UD_BASE = '" + TxtUdBase.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND UD_BASE = '" + TxtUdBase.Text + "' ";
                        SQLCombo1 += " AND UD_BASE = '" + TxtUdBase.Text + "' ";
                    }
                }
                if (TxtCodGolden.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";
                        SQLCombo1 += " ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";

                    }
                    else
                    {
                        SQLGolden += " AND ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";
                        SQLCombo1 += " AND ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";
                    }
                }
                if (TxtVariedad.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                        SQLCombo1 += " ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                        SQLCombo1 += " AND ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                    }
                }
                if (TxtTipoPlanta.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo1 += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo1 += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo2 += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo2 += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    if (SQLTipoPlanta.Substring(SQLTipoPlanta.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLTipoPlanta += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo3 += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLTipoPlanta += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                        SQLCombo3 += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                }
                if (TextTipoFormato.Text != "")
                {
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                        SQLCombo2 += " ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                        SQLCombo2 += " AND ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                    }
                }
                if (TxtNumPlanta.Text != "")
                {
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                        SQLCombo2 += " ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                        SQLCombo2 += " AND ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                    }
                }
                if (TxtDTipoPlanta.Text != "")
                {
                    if (SQLTipoPlanta.Substring(SQLTipoPlanta.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLTipoPlanta += " ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                        SQLCombo3 += " ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLTipoPlanta += " AND ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                        SQLCombo3 += " AND ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                    }
                }

                //Verifica que trae condiciones
                if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                {
                    SQLGolden = SQLGolden.Replace("WHERE","");
                    SQLCombo1 = SQLCombo1.Replace("WHERE", "");
                }
                if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                {
                    SQLBandejas = SQLBandejas.Replace("WHERE", "");
                    SQLCombo2 = SQLCombo2.Replace("WHERE", "");
                }
                if (SQLTipoPlanta.Substring(SQLTipoPlanta.ToString().Length - 5, 5) == "WHERE")
                {
                    SQLTipoPlanta = SQLTipoPlanta.Replace("WHERE", "");
                    SQLCombo3 = SQLCombo3.Replace("WHERE", "");
                }


                dt = Main.BuscaLote(SQLGolden).Tables[0];
                GvGolden.DataSource = dt;
                GvGolden.DataBind();
                LbCountGolden.Text = dt.Rows.Count.ToString();

                dt = null;
                dt = Main.BuscaLote(SQLCombo1).Tables[0];
                DrCodgolden.DataSource = dt;
                DrCodgolden.DataBind();

                dt = null;
                dt = Main.BuscaLote(SQLTipoPlanta).Tables[0];

                GvTablaDes.DataSource = dt;
                GvTablaDes.DataBind();
                LbcountPlanta.Text = dt.Rows.Count.ToString();

                dt = null;
                dt = Main.BuscaLote(SQLCombo3).Tables[0];
                DrTipoplanta.DataSource = dt;
                DrTipoplanta.DataBind();


                dt = null;
                dt = Main.BuscaLote(SQLBandejas).Tables[0];

                GvBandeja.DataSource = dt;
                GvBandeja.DataBind();
                LbcountBandeja.Text = dt.Rows.Count.ToString();

                dt = null;
                dt = Main.BuscaLote(SQLCombo2).Tables[0];
                DrBandejas.DataSource = dt;
                DrBandejas.DataBind();

                string SQL = "SELECT A.ZID, A.ZTIPO_PLANTA, A.ZTIPO_FORMATO, B.ZVARIEDAD, B.ZCODGOLDEN, B.UD_BASE, C.ZTIPO_PLANTA, C.ZDESCRIPTIPO ";
                SQL += "FROM ZBANDEJAS A, ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B,ZTIPOPLANTADESCRIP C WHERE A.ZTIPO_PLANTA = B.ZTIPO_PLANTA ";
                SQL += " AND A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
                if (TxtTipoPlanta.Text != "")
                {
                    SQL += " AND A.ZTIPO_PLANTA LIKE '%" + TxtTipoPlanta.Text + "%'";
                }
                if (TextTipoFormato.Text != "")
                {
                    SQL += " AND A.ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                }
                if (TxtNumPlanta.Text != "")
                {
                    SQL += " AND A.ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                }
                if (TxtDTipoPlanta.Text != "")
                {
                    SQL += " AND C.ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                }
                if (TxtVariedad.Text != "")
                {
                    SQL += " AND B.ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                }

                dt = null;
                dt = Main.BuscaLote(SQL).Tables[0];

                GrResultado.DataSource = dt;
                GrResultado.DataBind();

                LbCountResultado.Text = dt.Rows.Count.ToString();

                BtModificaVariedad.Visible = true;
                btNuevaVariedad.Visible = false;
                BtGuardaVariedad.Visible = false;
                BtCancelaVariedad.Enabled = false;
                BtDeleteVariedad.Enabled = false;
            }          
        }

 



        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Server.Transfer("RecoNomina.aspx");
        }

        protected void check1_Click(Object sender, EventArgs e) //  1
        {
            if (this.Session["collapse1"].ToString() == "1")
            {
                this.Session["collapse1"] = "0";
            }
            else
            {
                this.Session["collapse1"] = "1";
            }
            DejaPosAcordeon();
        }
        protected void check2_Click(Object sender, EventArgs e)//  1
        {
            if (this.Session["collapse2"].ToString() == "1")
            {
                this.Session["collapse2"] = "0";
            }
            else
            {
                this.Session["collapse2"] = "1";
            }
            DejaPosAcordeon();
        }
        protected void check3_Click(Object sender, EventArgs e)//  1
        {
            if (this.Session["collapse3"].ToString() == "1")
            {
                this.Session["collapse3"] = "0";
            }
            else
            {
                this.Session["collapse3"] = "1";
            }
            DejaPosAcordeon();
        }
        protected void check4_Click(Object sender, EventArgs e)//1
        {
            if (this.Session["collapse4"].ToString() == "1")
            {
                this.Session["collapse4"] = "0";
            }
            else
            {
                this.Session["collapse4"] = "1";
            }
            DejaPosAcordeon();
        }
        protected void check5_Click(Object sender, EventArgs e) // 1
        {
            if (this.Session["collapse5"].ToString() == "1")
            {
                this.Session["collapse5"] = "0";
            }
            else
            {
                this.Session["collapse5"] = "1";
            }
            DejaPosAcordeon();
        }
        protected void check6_Click(Object sender, EventArgs e) //1
        {
            if (this.Session["collapse6"].ToString() == "1")
            {
                this.Session["collapse6"] = "0";
            }
            else
            {
                this.Session["collapse6"] = "1";
            }
            DejaPosAcordeon();
        }




        private void DejaPosAcordeon()
        {
            if (this.Session["collapse1"].ToString() == "1")
            {
                //collapse1.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                //collapse1.Attributes["class"] = "panel-collapse collapse";
            }
        }




 
        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {

            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu6") { this.Session["Menu"] = "6"; }
            this.Session["GridOrden"] = "";
            this.Session["GridEdicion"] = "0";

        }

        protected void BtLimpiaUd_Click(Object sender, EventArgs e)
        {
            BtModificaVariedad.Visible = false;
            btNuevaVariedad.Visible = true;
            BtGuardaVariedad.Visible = false;
            BtCancelaVariedad.Enabled = false;
            BtDeleteVariedad.Enabled = false;

            Img1.Visible = false;
            Img2.Visible = false;
            Img3.Visible = false;
            Img4.Visible = false;
            Img5.Visible = false;
            Img6.Visible = false;
            Img7.Visible = false;

            ImgPlanta.Visible = false;
            ImgPlanta1.Visible = false;
            ImgGolden.Visible = false;
            ImgGolden1.Visible = false;
            ImgBandeja.Visible = false;
            ImgBandeja1.Visible = false;

            TxtCodGolden.Enabled = true;
            TxtCodGolden.Enabled = true;
            TxtVariedad.Enabled = true;
            TextTipoFormato.Enabled = true;
            TxtTipoPlanta.Enabled = true;
            TxtDTipoPlanta.Enabled = true;
            TxtNumPlanta.Enabled = true;
            TxtUdBase.Enabled = true;


            TxtCodGolden.Text = "";
            TxtCodGolden.Text = "";
            TxtVariedad.Text = "";
            TextTipoFormato.Text = "";
            TxtTipoPlanta.Text = "";
            TxtDTipoPlanta.Text = "";
            TxtNumPlanta.Text = "";

            Carga_Tablas_Variedades();
            DataTable dt = null;
            GrResultado.DataSource = null;
            GrResultado.DataBind();

        }

        private void Carga_Tablas_Variedades()
        {
            DrBandejas.Items.Clear();
            DrCodgolden.Items.Clear();
            DrTipoplanta.Items.Clear();

            DrBandejas.DataValueField = "ZTIPO_FORMATO";
            DrBandejas.DataTextField = "ZTIPO_FORMATO";
            DrCodgolden.DataValueField = "ZCODGOLDEN";
            DrCodgolden.DataTextField = "ZCODGOLDEN";
            DrTipoplanta.DataValueField = "ZDESCRIPTIPO";
            DrTipoplanta.DataTextField = "ZDESCRIPTIPO";

            string SQL = " SELECT DISTINCT(ZDESCRIPTIPO) FROM ZTIPOPLANTADESCRIP ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            DrTipoplanta.DataSource = dt;
            DrTipoplanta.DataBind();
            dt = null;

            SQL = " SELECT ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            dt = Main.BuscaLote(SQL).Tables[0];
            GvTablaDes.DataSource = dt;
            GvTablaDes.DataBind();
            LbcountPlanta.Text = dt.Rows.Count.ToString();
            ;
            SQL = " SELECT  ZID, ZTIPO_PLANTA, ZNUMERO_PLANTAS, ZTIPO_FORMATO FROM ZBANDEJAS ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            GvBandeja.DataSource = dt;
            GvBandeja.DataBind();
            LbcountBandeja.Text = dt.Rows.Count.ToString();

            SQL = " SELECT  DISTINCT(ZTIPO_FORMATO) FROM ZBANDEJAS";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            DrBandejas.DataSource = dt;
            DrBandejas.DataBind();


            SQL = " SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            GvGolden.DataSource = dt;
            GvGolden.DataBind();
            LbCountGolden.Text = dt.Rows.Count.ToString(); 

            SQL = " SELECT  DISTINCT(ZCODGOLDEN) FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            DrCodgolden.DataSource = dt;
            DrCodgolden.DataBind();

            TxtUdBase.Text = "";
            TxtCodGolden.Text = "";
            TxtVariedad.Text = "";
            TextTipoFormato.Text = "";
            TxtTipoPlanta.Text = "";
            TxtDTipoPlanta.Text = "";
            TxtNumPlanta.Text = "";

            BtModificaVariedad.Visible = false;
            btNuevaVariedad.Visible = true;
            BtGuardaVariedad.Visible = false;
            BtCancelaVariedad.Enabled = false;
            BtDeleteVariedad.Enabled = false;

        }
        protected void BtAyuda_Click (object sender, ImageClickEventArgs e)
        {
            if (multitxt.Visible == true)
            {
                multitxt.Visible = false;
            }
            else
            {
                multitxt.Visible = true;
            }
        }
        protected void ListBoxBandeja_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ListGolden_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ListBoxTIPOPLANTA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        protected void DrPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BuscaAnchor_Click(object sender, EventArgs e)
        {

        }



        protected void Btfiltra_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            Carga_tabla();

        }


        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            Variables.Error = "";
        }
        protected void Colapsar_click(object sender, EventArgs e)
        {

        }

        protected void drag0_Click(object sender, EventArgs e)
        {

        }
        protected void drag1_Click(object sender, EventArgs e)
        {

        }
        protected void drag2_Click(object sender, EventArgs e)
        {

        }


        private System.Web.UI.Control BuscarControl(System.Web.UI.Control pForm, System.Web.UI.Control pControlPadre, string pControlNombre)
        {
            if (pControlPadre.ID == pControlNombre)
            {
                //Retornamos el control si es igual al control padre
                return pControlPadre;
            }

            //Recorremos los controles que hayan dentro del control padre
            foreach (System.Web.UI.Control subControl in pControlPadre.Controls)
            {
                System.Web.UI.Control resultado = BuscarControl(pForm, subControl, pControlNombre);
                if (resultado != null)
                {
                    //Retornamos el control que estamos buscando
                    return resultado;
                }
            }

            //Sino lo encuentra retornamos nulo
            return null;
        }

        private System.Web.UI.Control FindALL(ControlCollection page, string id)
        {
            foreach (System.Web.UI.Control c in page)
            {
                if (c.ID == id)
                {
                    return c;
                }

                if (c.HasControls())
                {
                    var res = FindALL(c.Controls, id);

                    if (res != null)
                    {
                        return res;
                    }
                }
                ElID = c.ID;
            }
            return null;
        }





        protected void btPrinter_Click(object sender, EventArgs e)
        {

        }

        protected void BtBuscaFiltro_Click(object sender, EventArgs e)
        {

        }

        protected void BtGuardaGrid_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelaGrid_Click(object sender, EventArgs e)
        {

        }

        protected void BtnMuestra_Click(object sender, EventArgs e)
        {
        }

  
          protected void ImageOrden_Click(object sender, EventArgs e)
        {
            //PanelGeneralCabecera.Visible = false;
            //VistaOrden.Visible = true;
            //VistaOrdenNO.Visible = false;
        }

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {
        }
        protected void DrConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carga_tabla();
        }

        protected void DrSelCab_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DrSelectFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DrOrdenMin_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
       protected void DrFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void cbVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void cbTipoFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void cbDYipoPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cbBuscaplanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = "  SELECT ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP WHERE ZDESCRIPTIPO = '" + DrTipoplanta.SelectedItem.Value + "'";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            GvTablaDes.DataSource = dt;
            GvTablaDes.DataBind();

        }
        protected void cbBuscagolden_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = " SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN WHERE ZCODGOLDEN = '" + DrCodgolden.SelectedItem.Value + "'";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            GvGolden.DataSource = dt;
            GvGolden.DataBind();

        }
        

        protected void cbBuscaCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = " SELECT  ZID, ZTIPO_PLANTA, ZNUMERO_PLANTAS, ZTIPO_FORMATO FROM ZBANDEJAS WHERE ZTIPO_FORMATO = '" + DrBandejas.SelectedItem.Value + "'";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            GvBandeja.DataSource = dt;
            GvBandeja.DataBind();

        }

        protected void cbNumPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void cbUdbase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cbUdbase.Text = cbUdbase.SelectedItem.Text;
            //cbUdbase.PageSize = Convert.ToInt32(ddListaPageSize.SelectedItem.Value);
            //Carga_tablaLista();
        }

        protected void gvLista_PageSize_Changed(object sender, EventArgs e)
        {
        }

        protected void GvBandeja_PageSize_Changed(object sender, EventArgs e)
        {
        }

        protected void gvControl_PageSize_Changed(object sender, EventArgs e)
        {
        }


     



        protected void PrintReportOff_Click(object sender, EventArgs e)
        {
        }
        protected void sellectAllEmpleado(object sender, EventArgs e)
        {
        }


        protected void DrOrden1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
        protected void DrControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tabla();
        }
 

        protected void DrLista1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvCabecera_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
        }


        protected void gvControl_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            Carga_tabla();
        }

        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
        }

        protected void GvBandeja_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }
        protected void TabOCajas_TextChanged(object sender, EventArgs e)
        {
            //calculo palets y plantas
            this.Session["CalculoPaletPlanta"] = "TabOCajas";
        }
        protected void gvControl_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string a = e.SortExpression;
            this.Session["GridOrden"] = e.SortExpression;

            Carga_tabla(e.SortExpression);
        }

 

        protected void GvBandeja_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }


        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string a = e.SortExpression;
            this.Session["GridOrden"] = e.SortExpression;
        }


 
        protected void GvBandeja_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void GrResultado_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            
        }
        protected void GvBandeja_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GvBandeja.Rows[index];
                miro = GvBandeja.DataKeys[index].Value.ToString();

                this.Session["GVBandejaID"] = miro;

                string SQL = " SELECT  ZID, ZTIPO_PLANTA, ZTIPO_FORMATO, ZNUMERO_PLANTAS FROM ZBANDEJAS  ";
                SQL += " WHERE ZID = " + miro;

                DataTable dt = null;
                dt = Main.BuscaLote(SQL).Tables[0];
                if (dt.Rows.Count == 0)
                {
                    row.BackColor = Color.FromName("#ffead1");
                }
                else
                {
                    row.BackColor = Color.FromName("#ffead1");
                    foreach (DataRow filas in dt.Rows)
                    {
                        TxtTipoPlanta.Text = filas["ZTIPO_PLANTA"].ToString();
                        TextTipoFormato.Text = filas["ZTIPO_FORMATO"].ToString();
                        TxtNumPlanta.Text = filas["ZNUMERO_PLANTAS"].ToString();
                    }
                }

                Img1.Visible = true;
                Img5.Visible = true;
                Img7.Visible = true;
                ImgBandeja.Visible = true;
                ImgBandeja1.Visible = false;

                BtModificaVariedad.Visible = true;
                btNuevaVariedad.Visible = false;
                BtGuardaVariedad.Visible = false;
                BtCancelaVariedad.Enabled = false;
                BtDeleteVariedad.Enabled = false;

            }
            catch (Exception ex)
            {
                string b = ex.Message;
                return;
            }
        }
        private void LimpiaCajasPrint()
        {
        }
        protected void GvGolden_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            try
            {
                if (e.CommandName == "Identificador")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GvGolden.Rows[index];
                    miro = GvGolden.DataKeys[index].Value.ToString();
                    this.Session["GVGoldenID"] = miro;

                    string SQL = " SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  ";
                    SQL += " WHERE ZID = " + miro;

                    DataTable dt = Main.BuscaLote(SQL).Tables[0];

                    if (dt.Rows.Count == 0)
                    {
                        row.BackColor = Color.FromName("#ffead1");
                    }
                    else
                    {
                        row.BackColor = Color.FromName("#ffead1");
                        foreach (DataRow filas in dt.Rows)
                        {
                            TxtTipoPlanta.Text = filas["ZTIPO_PLANTA"].ToString();
                            TxtVariedad.Text = filas["ZVARIEDAD"].ToString();
                            TxtCodGolden.Text = filas["ZCODGOLDEN"].ToString();
                            TxtUdBase.Text = filas["UD_BASE"].ToString();
                        }
                    }
                }
                Img1.Visible = true;
                Img2.Visible = true;
                Img3.Visible = true;
                Img4.Visible = true;
                ImgGolden.Visible = true;
                ImgGolden1.Visible = false;

                BtModificaVariedad.Visible = true;
                btNuevaVariedad.Visible = false;
                BtGuardaVariedad.Visible = false;
                BtCancelaVariedad.Enabled = false;
                BtDeleteVariedad.Enabled = false;

                //02/10/2024
                //Menu
                    //Comercial
                        //Ofertas Clientes
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                return;
            }
        }
        protected void GvTablaDes_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GvTablaDes.Rows[index];
                miro = GvTablaDes.DataKeys[index].Value.ToString();

                this.Session["GVVariedadID"] = miro;

                string SQL = " SELECT  ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP  ";
                SQL += " WHERE ZID = " + miro;
                DataTable dt = Main.BuscaLote(SQL).Tables[0];

                if (dt.Rows.Count == 0)
                {
                    row.BackColor = Color.FromName("#ffead1");
                }
                else
                {
                    row.BackColor = Color.FromName("#ffead1");
                    foreach (DataRow filas in dt.Rows)
                    {
                        TxtTipoPlanta.Text = filas["ZTIPO_PLANTA"].ToString();
                        TxtDTipoPlanta.Text = filas["ZDESCRIPTIPO"].ToString();
                    }
                }
                Img1.Visible = true;
                Img6.Visible = true;
                ImgPlanta.Visible = true;
                ImgPlanta1.Visible = false;
                BtModificaVariedad.Visible = true;
                btNuevaVariedad.Visible = false;
                BtGuardaVariedad.Visible = false;
                BtCancelaVariedad.Enabled = false;
                BtDeleteVariedad.Enabled = false;

            }
            catch (Exception ex)
            {
                string b = ex.Message;
                return;
            }
        }

        protected void Imgatras_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            if (Img.ID == "ImgBandeja")
            {
                ImgBandeja.Visible = false;
                ImgBandeja1.Visible = true;
                Img1.Visible = false;
                Img5.Visible = false;
                Img7.Visible = false;
            }
            else if (Img.ID == "ImgGolden")
            {
                ImgGolden.Visible = false; 
                ImgGolden1.Visible = true;
                Img1.Visible = false;
                Img2.Visible = false;
                Img3.Visible = false;
                Img4.Visible = false;
                Img5.Visible = false;
                Img6.Visible = false;
            }
            else if (Img.ID == "ImgPlanta")
            {
                ImgPlanta.Visible = false;
                ImgPlanta1.Visible = true;
                Img1.Visible = false;
                Img6.Visible = false;
                Img7.Visible = false;
            }
        }
        protected void Imgadelante_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
           if (Img.ID == "ImgBandeja1")
            {
                ImgBandeja.Visible = true;
                ImgBandeja1.Visible = false;
                Img1.Visible = true;
                Img5.Visible = true;
                Img7.Visible = true;
            }
            else if (Img.ID == "ImgGolden1")
            {
                ImgGolden.Visible = true;
                ImgGolden1.Visible = false;
                Img1.Visible = true;
                Img2.Visible = true;
                Img3.Visible = true;
                Img4.Visible = true;
                Img5.Visible = true;
                Img6.Visible = true;
            }
            else if (Img.ID == "ImgPlanta1")
            {
                ImgPlanta.Visible = true;
                ImgPlanta1.Visible = false;
                Img1.Visible = true;
                Img6.Visible = true;
                Img7.Visible = true;
            }
        }

        protected void GvTablaDes_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            GvTablaDes.SelectedRow.BackColor = Color.FromName("#565656");
        }
        
        protected void Gvgolden_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            GvGolden.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void GvBandeja_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            GvBandeja.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void GrResultado_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            GrResultado.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvCabecera_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
        }
        protected void gvControl_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
        }
        protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
        }
        protected void GvGolden_OnSorting(object sender, GridViewSortEventArgs e)
        {
        }
        protected void GvTablaDes_OnSorting(object sender, GridViewSortEventArgs e)
        {
        }
        
        protected void GvBandeja_OnSorting(object sender, GridViewSortEventArgs e)
        {
        }
        protected void GrResultado_OnSorting(object sender, GridViewSortEventArgs e)
        {
        }

        protected void gvCabecera_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            //LinkButton deletebutton = ((LinkButton)gvCabecera.FindControl("YourDeleteButton"));

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ShowEditButton
                if (this.Session["Cerrados"].ToString() == "1")
                {
                    //HtmlImage editar = (HtmlImage)e.Row.Cells[0].Controls[0];
                    ImageButton editar = e.Row.Cells[0].Controls[0] as ImageButton;
                    editar.Visible = false;
                    //CommandField editbutton = (CommandField)sender;
                    //editbutton.ShowEditButton = false;
                }
                //DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
                    //verde
                }
                else if (miro == "3")
                {
                    e.Row.BackColor = Color.FromName("#fcf5d2");
                    //crema
                }
                else if (miro == "4")
                {
                    e.Row.BackColor = Color.FromName("#ff9b8a");
                    //rojo
                }
                else if ((e.Row.DataItemIndex % 2) == 0)
                {
                    //Par
                    e.Row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    e.Row.BackColor = Color.FromName("#f5f5f5");
                }

                //GridViewRow row = (GridViewRow)gvLista.Rows[e.Row];
                //string miro = gvLista.DataKeys[e.Row].Value.ToString();
                //e.Row.TableSection = TableRowSection.TableBody;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        
        protected void GrResultado_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void GvBandeja_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GvGolden_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GvTablaDes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = drv["ESTADO"].ToString();
                //ShowEditButton
                if (this.Session["Cerrados"].ToString() == "1")
                {
                    //HtmlImage editar = (HtmlImage)e.Row.Cells[0].Controls[0];
                    ImageButton editar = e.Row.Cells[0].Controls[0] as ImageButton;
                    editar.Visible = false;
                    //ImageButton SubeCarga = ((ImageButton)gvLista.FindControl("ibtSubeCarga"));
                    ImageButton SubeCarga = ((ImageButton)e.Row.FindControl("ibtSubeCarga"));
                    SubeCarga.Visible = false;

                    //int indice = e.Row.RowIndex;

                    //ImageButton txtBox = (gvLista.Rows[indice].Cells[1].FindControl("ibtSubeCarga") as ImageButton);

                    //txtBox.Visible = false;

                    //CommandField editbutton = (CommandField)sender;
                    //editbutton.ShowEditButton = false;
                }

                if (drv["ESTADO"].ToString() == "1")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
                    //verde
                }
                else if (drv["ESTADO"].ToString() == "2")
                {
                    //crema
                    e.Row.BackColor = Color.FromName("#fcf5d2");
                }
                else if ((e.Row.DataItemIndex % 2) == 0)
                {
                    //Par
                    e.Row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    e.Row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }

        private void MiOpenMenu()
        {
            string MiImagen = "MiMenu"; // filas["NUMERO_LINEA"].ToString(); //Imgdrag1
            System.Web.UI.HtmlControls.HtmlGenericControl Paletimg = (HtmlGenericControl)FindControl(MiImagen);
            if (Paletimg != null)
            {
                Paletimg.Visible = true;
            }
        }
        private void MiCloseMenu()
        {
            string MiImagen = "MiMenu"; // filas["NUMERO_LINEA"].ToString(); //Imgdrag1
            System.Web.UI.HtmlControls.HtmlGenericControl Paletimg = (HtmlGenericControl)FindControl(MiImagen);
            if (Paletimg != null)
            {
                Paletimg.Visible = false;
           }
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            MiOpenMenu();

        }
        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            //Modifica.Visible = false;
            //Decide.Visible = false;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            Lbmensaje.Text = "";
            this.Session["ElIDaBorrar"] = "";
            MiOpenMenu();
        }

  
 
        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            //Modifica.Visible = false;
            //Decide.Visible = false;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            this.Session["ElIDaBorrar"] = "";
            MiOpenMenu();
        }


        protected void GvBandeja_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

          private void Carga_tabla(string sortExpression = null)
        {
            //string Temporal = ""; //Jose
            //OrdenControl();
            string SQL = ""; ;
            string Filtro = this.Session["Filtro"].ToString();
            try
            {

                DataTable dt = null;
                //Carga_Filtros();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //Casillas verdes a rellenar. Al mostrar inicialmente UdsACargar= UdsPed-UdsServ-UdsEnCarga
                    //UdsPend campo calculado(UdsPend = UdsPed - UdsServ - UdsEnCarga - UdsACargar).Al mostrar inicialmente el cálculo será 0.(UDSPEDIDAS - UDSSERVIDAS - UDSENCARGA - UDSACARGAR)

                    SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                    //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                    SQL += " CONVERT(DECIMAL(18, 3), UDSPEDIDAS) AS UDSPEDIDAS, CONVERT(DECIMAL(18, 3), UDSSERVIDAS) AS UDSSERVIDAS, CONVERT(DECIMAL(18, 3), UDSENCARGA) AS UDSENCARGA, CONVERT(DECIMAL(18, 3), UDSPENDIENTES) AS UDSPENDIENTES, ";
                    SQL += " CASE ";
                    //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
                    SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                    SQL += " WHEN ESTADO = 1 THEN CONVERT(DECIMAL(18, 3), UDSACARGAR) ";
                    //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                    SQL += " END AS UDSACARGAR, ";
                    SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                    SQL += " FROM ZCARGA_ORDEN  WHERE (ESTADO <> 2 OR ESTADO IS NULL) ";
                    if (this.Session["FiltroEmpresa"].ToString() != "")
                    {
                        SQL += " AND EMPRESA = '" + this.Session["FiltroEmpresa"].ToString() + "'";
                    }
                    if (Filtro != "")
                    {
                        SQL += Filtro;
                    }
                    if (ElOrdenControl != "")
                    {
                        SQL += ElOrdenControl;
                    }
                    //SQL = "SELECT * FROM ZCARGA_ORDEN  WHERE ESTADO = 0 ";
                    Lberror.Text += SQL + "1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                }
                else
                {
                    SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                    //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                    SQL += " CONVERT(DECIMAL(18, 3), UDSPEDIDAS) AS UDSPEDIDAS, CONVERT(DECIMAL(18, 3), UDSSERVIDAS) AS UDSSERVIDAS, CONVERT(DECIMAL(18, 3), UDSENCARGA) AS UDSENCARGA, CONVERT(DECIMAL(18, 3), UDSPENDIENTES) AS UDSPENDIENTES, ";
                    SQL += " CASE ";
                    //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
                    SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                    SQL += " WHEN ESTADO = 1 THEN CONVERT(DECIMAL(18, 3), UDSACARGAR) ";
                    //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                    SQL += " END AS UDSACARGAR, ";
                    SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                    SQL += " FROM ZCARGA_ORDEN  WHERE (ESTADO <> 2 OR ESTADO IS NULL) ";
                    if (this.Session["FiltroEmpresa"].ToString() != "")
                    {
                        SQL += " AND EMPRESA = '" + this.Session["FiltroEmpresa"].ToString() + "'";
                    }
                    if (Filtro != "")
                    {
                        SQL += Filtro;
                    }
                    if (ElOrdenControl != "")
                    {
                        SQL += ElOrdenControl;
                    }
                    //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, ID ";
                    //SQL = "SELECT * FROM ZCARGA_ORDEN  WHERE ESTADO = 0 ";
                    //if (Temporal == "")
                    //{
                    //    dt = Main.BuscaLoteGold(SQL).Tables[0];
                    //}
                    //else
                    //{
                    Lberror.Text += SQL + "2- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 2- Carga_tabla BuscaLote " + Variables.mensajeserver;

                    //}
                }
                this.Session["MiConsulta"] = dt;

                if (sortExpression == null || sortExpression == "")
                {
                    //gvControl.DataSource = dt;
                }
                else
                {
                    DataView dv = dt.AsDataView();
                    if (this.Session["GridEdicion"].ToString() == "0")
                    {
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                    }
                    //this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    //gvControl.DataSource = dv;
                }
                //gvControl.DataBind();
                //LbRowControl.Text = "Registros: " + dt.Rows.Count;
                //gvControl.DataSource = dt;
                //gvControl.DataBind();

                //busca Error donde no se puede depurar
                Lberror.Text = "";

            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Lberror.Text = "Carga tabla: " + SQL;
                Lberror.Visible = true;
            }

        }
        public static DataTable ConsultaPalet()
        {
            string SQL = "";
            DataTable dt = null;
            try
            {
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT * FROM ZCARGA_LINEA  ";
                    SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                    SQL += " ORDER BY POSICIONCAMION ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    SQL = " SELECT * FROM ZCARGA_LINEA  ";
                    SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                    SQL += " ORDER BY POSICIONCAMION ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                return dt;
            }
            catch (Exception mm)
            {
                Variables.Error = mm.Message;
            }
            return null;
        }

        public static System.Web.UI.Control FindControlRecursive(System.Web.UI.Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (System.Web.UI.Control c in root.Controls)
            {
                System.Web.UI.Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        protected void btnGenerate_Click(Object Objeto, Object Container)
        {
            //Genera la secuencia QR de la etiqueta Lote
            Label Etiqueta = (Label)Objeto;
            PlaceHolder Contenedor = (PlaceHolder)Container;

            if (this.Session["Procesa"].ToString() == "0")
            {
                DrPrinters_Click();
            }

            string code = Etiqueta.Text; // + Environment.NewLine; SANANDREQS

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();


            //Selección de uno u otro generador de QR
            if (this.Session["SelectQR"].ToString() == "0")
            {
                using (Bitmap bitMap = qrCode.GetGraphic(40))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                }
                this.Session["CodigoQR"] = qrCode.GetGraphic(40);
            }
            else
            {
                this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;
                using (MemoryStream memory = new MemoryStream())
                {
                    using (Bitmap bitMap = barcodeBitmap)
                    {
                        barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                        imgBarCode.Visible = true;
                    }
                }
            }

            Contenedor.Controls.Add(imgBarCode);

        }

        protected void btnGenerateTodo_Click(string etique, Object Container)
        {
            string code = "";
            PlaceHolder Contenedor = (PlaceHolder)Container;
            if (etique == "")
            {
                return;
            }
            else
            {
                code = etique; // + Environment.NewLine; SANANDREQS
            }

            DrPrinters_Click();


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            if (this.Session["SelectQR"].ToString() == "0")
            {
                using (Bitmap bitMap = qrCode.GetGraphic(40))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    }
                }
                this.Session["CodigoQR"] = qrCode.GetGraphic(40);
            }
            else
            {
                this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;

                using (MemoryStream memory = new MemoryStream())
                {
                    using (Bitmap bitMap = barcodeBitmap)
                    {
                        barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                        imgBarCode.Visible = true;
                    }
                }
            }
            Contenedor.Controls.Add(imgBarCode);
        }

        private void DrPrinters_Click()
        {
            //btnPrintA1.Visible = false;
        }
         protected void BtGuardaVariedad_Click(object sender, EventArgs e)
        {
            string SQL = "";
            if (this.Session["Modificar"].ToString() == "1")
            {
                if (ImgBandeja.Visible == true)
                {
                    SQL = "UPDATE ZBANDEJAS SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZTIPO_FORMATO ='" + TextTipoFormato.Text + "', ZNUMERO_PLANTAS ='" + TxtNumPlanta.Text + "' WHERE ZID = " + this.Session["GVBandejaID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }
                if (ImgGolden.Visible == true)
                {
                    SQL = "UPDATE ZPLANTA_TIPO_VARIEDAD_CODGOLDEN SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZVARIEDAD ='" + TxtVariedad.Text + "' , ZCODGOLDEN ='" + TxtCodGolden.Text + "', UD_BASE = '" + TxtUdBase.Text + "' WHERE ZID = " + this.Session["GVGoldenID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }

                if (ImgPlanta.Visible == true)
                {
                    SQL = "UPDATE ZTIPOPLANTADESCRIP SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' WHERE ZID = " + this.Session["GVVariedadID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            else if (this.Session["Modificar"].ToString() == "2")
            {
                if (ImgBandeja.Visible == true)
                {
                    SQL = "INSERT INTO ZBANDEJAS (ZTIPO_PLANTA, ZTIPO_FORMATO, ZNUMERO_PLANTAS) Values ('" + TxtTipoPlanta.Text + "', '" + TextTipoFormato.Text + "', '" + TxtNumPlanta.Text + "') ";
                    DBHelper.ExecuteNonQuery(SQL);
                }
                if (ImgGolden.Visible == true)
                {
                    SQL = "INSERT INTO ZPLANTA_TIPO_VARIEDAD_CODGOLDEN (ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE)  Values ('" + TxtTipoPlanta.Text + "','" + TxtVariedad.Text + "' , '" + TxtCodGolden.Text + "', '" + TxtUdBase.Text + "')";
                    DBHelper.ExecuteNonQuery(SQL);
                }

                if (ImgPlanta.Visible == true)
                {
                    SQL = "INSERT INTO  ZTIPOPLANTADESCRIP (ZTIPO_PLANTA, ZDESCRIPTIPO ) Values('" + TxtTipoPlanta.Text + "', '" + TxtDTipoPlanta.Text + "')";
                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            this.Session["Modificar"] = "0";
            Carga_Tablas_Variedades();

        }
        protected void BtNuevaVariedad_Click(object sender, EventArgs e)
        {
            Carga_Tablas_Variedades();

            Variables.MiColor = "#bdecb6";
            TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
            TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
            TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
            TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
            TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);

            BtModificaVariedad.Visible = false;
            btNuevaVariedad.Visible = true;
            BtGuardaVariedad.Visible = true;
            BtCancelaVariedad.Enabled = true;
            BtDeleteVariedad.Enabled = false;

            Img1.Visible = false;
            Img2.Visible = false;
            Img3.Visible = false;
            Img4.Visible = false;
            Img5.Visible = false;
            Img6.Visible = false;
            Img7.Visible = false;
            ImgBandeja.Visible = true;
            ImgBandeja1.Visible = false;
            ImgGolden.Visible = true;
            ImgGolden1.Visible = false;
            ImgPlanta.Visible = true;
            ImgPlanta1.Visible = false;

            this.Session["Modificar"] = "2";

        }
        
        protected void btnCancelaVariedad_Click(object sender, EventArgs e)
        {
            Variables.MiColor = "#ffffff";
            TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
            TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
            TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
            TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
            TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);

            TxtTipoPlanta.Enabled = true;
            TextTipoFormato.Enabled = true;
            TxtDTipoPlanta.Enabled = true;
            TxtVariedad.Enabled = true;
            TxtUdBase.Enabled = true;
            TxtCodGolden.Enabled = true;
            TxtNumPlanta.Enabled = true;

            Img1.Visible = false;
            Img2.Visible = false;
            Img3.Visible = false;
            Img4.Visible = false;
            Img5.Visible = false;
            Img6.Visible = false;
            Img7.Visible = false;
            ImgBandeja.Visible = false;
            ImgBandeja1.Visible = false;
            ImgGolden.Visible = false;
            ImgGolden1.Visible = false;
            ImgPlanta.Visible = false;
            ImgPlanta1.Visible = false;


            BtModificaVariedad.Visible = true;
            btNuevaVariedad.Visible = false;
            BtGuardaVariedad.Visible = false;
            BtCancelaVariedad.Enabled = false;
            BtDeleteVariedad.Enabled = false;

            this.Session["Modificar"] = "0";

        }
        protected void btnDeleteVariedad_Click(object sender, EventArgs e)
        {
            string SQL = "";
            if (this.Session["Modificar"].ToString() == "1")
            {
                if (ImgBandeja.Visible == true)
                {
                    SQL = "DELETE FROM ZBANDEJAS WHERE ZID = " + this.Session["GVBandejaID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }

                if (ImgGolden.Visible == true)
                {
                    SQL = "DELETE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN WHERE ZID = " + this.Session["GVGoldenID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }

                if (ImgPlanta.Visible == true)
                {
                    SQL = "DELETE FROM ZTIPOPLANTADESCRIP WHERE ZID = " + this.Session["GVVariedadID"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }
            }

            Carga_Tablas_Variedades();

            Variables.MiColor = "#ffffff";
            TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
            TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
            TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
            TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
            TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);
            this.Session["Modificar"] = "0";

        }
        protected void btnModificaVariedad_Click(object sender, EventArgs e)
        {
            BtModificaVariedad.Visible = true;
            btNuevaVariedad.Visible = false;
            BtGuardaVariedad.Visible = true;
            BtCancelaVariedad.Enabled = true;
            BtDeleteVariedad.Enabled = true;
            this.Session["Modificar"] = "1";

            Variables.MiColor = "#bdecb6";
            if (ImgBandeja.Visible == true)
            {
                TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
                TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
                TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);

            }
            else
            {
                TxtTipoPlanta.Enabled = false;
                TextTipoFormato.Enabled = false;
                TxtNumPlanta.Enabled = false;
            }
            if (ImgGolden.Visible == true)
            {
                TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
                TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
                TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
                TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);

            }
            else
            {
                TxtVariedad.Enabled = false;
                TxtUdBase.Enabled = false;
                TxtCodGolden.Enabled = false;
            }
            if (ImgPlanta.Visible == true)
            {
                TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);
            }
            else
            {
                TxtNumPlanta.Enabled = false;
            }
        }
        protected void BtBuscaCG_Click(object sender, EventArgs e)
        {


        }
        protected void BtBuscaVa_Click(object sender, EventArgs e)
        {


        }

        protected void BtBuscaTF_Click(object sender, EventArgs e)
        {


        }

        protected void BtBuscaTP_Click(object sender, EventArgs e)
        {


        }
        protected void BtBuscaDTP_Click(object sender, EventArgs e)
        {


        }
        protected void BtBuscaNP_Click(object sender, EventArgs e)
        {


        }
        

        protected void cbTipoPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

    }
}