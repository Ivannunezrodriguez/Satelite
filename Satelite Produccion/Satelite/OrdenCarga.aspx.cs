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

namespace Satelite
{
    public partial class OrdenCarga : System.Web.UI.Page
    {
        Reports.ReportListCamion dtsE = null;

        //private int registros = 0;
        //private string[] ListadoArchivos;
        //private static int IDDiv = 0;
        //private static string IDTABLA = "-1";
        //private Boolean Cargando = false;

        //private string ElIDaBorrar = "";
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
                ReportViewer1.LocalReport.Refresh();

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



                    TxtNumero.Enabled = false;
                    DataTable dt = new DataTable();
                    this.Session["TablaLista"] = dt;
                    //ChkSlot.Visible = false;
                    Variables.mensajeserver = "";
                    ArrayTextBoxs = new TextBox[20];
                    ArrayCombos = new DropDownList[20];
                    ArrayLabels = new Label[20];
                    Lberror.Visible = false;
                    Carga_Filtros();
                    Campos_ordenados();
                    Carga_tabla();
                    Carga_tablaLista();

                    //LinkButton lnkUp = (gvLista.Rows[0].FindControl("lnkUp") as LinkButton);
                    //LinkButton lnkDown = (gvLista.Rows[gvLista.Rows.Count - 1].FindControl("lnkDown") as LinkButton);
                    //lnkUp.Enabled = false;
                    //lnkUp.CssClass = "button disabled";
                    //lnkDown.Enabled = false;
                    //lnkDown.CssClass = "button disabled";

                    //Carga_tablaCabecera();
                    //Carga_Menus();

                    //TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //TxtFechaPrepara.Text = "";


                  
                    //Formularios
                    if (this.Session["MiMenu"].ToString() == "OrdenCarga")
                    {
                        if (this.Session["Param1"].ToString() != "")
                        {
                            for (int i = 0; i < DrConsultas.Items.Count; i++)
                            {
                                string a = this.Session["Param1"].ToString();
                                if (DrConsultas.Items[i].Text.Contains(a))
                                {
                                    DrConsultas.Text = DrConsultas.Items[i].Value; 
                                    break;
                                }
                            }
                            //Estado donde buscar
                            if (this.Session["Param3"].ToString() == "0, 1")
                            {
                                //Nuevo Confirmacion, etc
                                this.Session["Param3"] = " AND (ESTADO in (0, 1) OR ESTADO is NULL) ";
                                Carga_tablaCabecera();
                            }
                            if (this.Session["Param3"].ToString() == "2")
                            {
                                //Cerrados
                                this.Session["Param3"] = " AND ESTADO = 2 ";
                                Carga_tablaCabecera();
                            }
                            if (this.Session["Param3"].ToString() == "3")
                            {
                                //Eliminados
                                checkCabeceraListas_Click(null,null);
                                //Carga_tablaCabeceraClose();
                                this.Session["Param3"] = " AND ESTADO = 3 ";
                            }
                        }
                        this.Session["Param1"] = "";
                        this.Session["Param2"] = "";
                        this.Session["Param3"] = "";
                        this.Session["MiMenu"] = "";
                    }

                    Carga_Menus();

                    TxtFecha.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    TxtFechaPrepara.Text = "";

                }
                else
                {

                }

                //if (this.Session["DESARROLLO"].ToString() == "DESARROLLO")
                //{
                //    H3Titulo.Visible = false;
                //    H3Desarrollo.Visible = true;
                //    Lbhost2.Text = "(" + this.Session["ComputerName"].ToString() + ")";
                //}
                //else
                //{
                //    H3Titulo.Visible = true;
                //    H3Desarrollo.Visible = false;
                //    Lbhost1.Text = "(" + this.Session["ComputerName"].ToString() + ")";
                //}

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
            DivEtiquetas.Visible = false;

            //if (Request.Browser.Browser.Equals("IE"))
            //{
            //    BtnImprimir.Visible = false;
            //    BtnImprimir.Enabled = false;
            //}

            //PNreportLista.Visible = false;
            //PNFiltrosLista.Visible = true;
            //PNGridLista.Visible = true;
            //alerta.Visible = false;
            //Lberror.Visible = false;
        }

        protected void lbFilClose_Click(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            //HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            if (btn.ID == "btn")
            {
                //Ia = (HtmlGenericControl)INombre;
                Tx = (TextBox)TxtCodigo;
            }


            //Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //Ia.Attributes["title"] = "Desactivado";
            //Ia.Attributes.Add("style", "color:darkred;");
            Tx.Attributes.Add("style", "");
            Tx.Text = "";
            BtBusca_Click(sender, e);
        }

        protected void btnAsc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "ASC";
            //this.Session["sortExpression"] = "ASC";

                if (DrNombre.SelectedIndex > 0)
            {
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    Carga_tablaCabecera(DrNombre.Text);
                }
                else
                {
                    Carga_tablaCabeceraClose(DrNombre.Text);
                }
            }


        }

        protected void btnDesc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "DESC";
            //this.Session["sortExpression"] = "DESC";
            if (DrNombre.SelectedIndex > 0)
            {
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    Carga_tablaCabecera(DrNombre.Text);
                }
                else
                {
                    Carga_tablaCabeceraClose(DrNombre.Text);
                }
            }

        }

        protected void BtBuscaUd_Click(Object sender, EventArgs e) //  1
        {
            
            if (TxtUdBase.Text == "" && TxtCodGolden.Text == "" && TxtVariedad.Text == "" && TextTipoFormato.Text == "" && TxtTipoPlanta.Text == "" && TxtDTipoPlanta.Text == "" && TxtNumPlanta.Text == "" )
            {
                Carga_Tablas_Variedades();

            }
            else
            {
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
                    }
                    else
                    {
                        SQLGolden += " AND UD_BASE = '" + TxtUdBase.Text + "' ";
                    }
                }
                if (TxtCodGolden.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND ZCODGOLDEN = '" + TxtCodGolden.Text + "' ";
                    }
                }
                if (TxtVariedad.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND ZVARIEDAD = '" + TxtVariedad.Text + "' ";
                    }
                }
                if (TxtTipoPlanta.Text != "")
                {
                    if (SQLGolden.Substring(SQLGolden.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLGolden += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLGolden += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    if (SQLTipoPlanta.Substring(SQLTipoPlanta.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLTipoPlanta += " ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLTipoPlanta += " AND ZTIPO_PLANTA = '" + TxtTipoPlanta.Text + "' ";
                    }
                }
                if (TextTipoFormato.Text != "")
                {
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZTIPO_FORMATO = '" + TextTipoFormato.Text + "' ";
                    }
                }
                if (TxtNumPlanta.Text != "")
                {
                    if (SQLBandejas.Substring(SQLBandejas.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLBandejas += " ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLBandejas += " AND ZNUMERO_PLANTAS = '" + TxtNumPlanta.Text + "' ";
                    }
                }
                if (TxtDTipoPlanta.Text != "")
                {
                    if (SQLTipoPlanta.Substring(SQLTipoPlanta.ToString().Length - 5, 5) == "WHERE")
                    {
                        SQLTipoPlanta += " ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                    }
                    else
                    {
                        SQLTipoPlanta += " AND ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' ";
                    }
                }

                dt = Main.BuscaLote(SQLGolden).Tables[0];
                GvGolden.DataSource = dt;
                GvGolden.DataBind();

                dt = null;
                dt = Main.BuscaLote(SQLTipoPlanta).Tables[0];

                GvTablaDes.DataSource = dt;
                GvTablaDes.DataBind();


                dt = null;
                dt = Main.BuscaLote(SQLBandejas).Tables[0];

                GvBandeja.DataSource = dt;
                GvBandeja.DataBind();

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

                BtModificaVariedad.Visible = true;
                btNuevaVariedad.Visible = false;
                BtGuardaVariedad.Visible = false;
                BtCancelaVariedad.Enabled = false;
                BtDeleteVariedad.Enabled = false;
            }          
        }

        protected void BtBusca_Click(Object sender, EventArgs e) //  1
        {
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
        }

        //protected void BtContiene_Click(Object sender, EventArgs e) //  1
        //{
        //    HtmlButton btn = (HtmlButton)sender;
        //    HtmlGenericControl Ia = new HtmlGenericControl();
        //    TextBox Tx = new TextBox();
        //    DropDownList Cb = new DropDownList();
        //    Boolean Esta = false;

        //    if (btn.ID == "BtCodigo")
        //    {
        //        Ia = (HtmlGenericControl)INombre;
        //        Tx = (TextBox)TxtCodigo;
        //    }

        //    //Busqueda particular de cada grid

        //    Ia.Attributes.Add("style", "");
        //    Tx.Attributes.Add("style", "background-color:#e6f2e1;");


        //    if (Ia.Attributes["class"] == "fa fa-circle fa-2x")
        //    {
        //        Ia.Attributes["title"] = "No contiene estos datos.";
        //        Ia.Attributes["class"] = "fa fa-circle-o fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-adjust fa-2x")
        //    {
        //        Ia.Attributes["title"] = "No incluye en su contenido este datos.";
        //        Ia.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-circle-o fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Incluye en su contenido este dato.";
        //        Ia.Attributes["class"] = "fa fa-adjust fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Menor que";
        //        Ia.Attributes["class"] = "fa fa-chevron-left fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-chevron-left fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Mayor que";
        //        Ia.Attributes["class"] = "fa fa-chevron-right fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-chevron-right fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Distinto de";
        //        Ia.Attributes["class"] = "fa fa-arrows-alt fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-arrows-alt fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Desactivado";
        //        Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
        //        Ia.Attributes.Add("style", "color:darkred;");
        //        Tx.Attributes.Add("style", "");
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-hand-o-up fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Contiene estos datos";
        //        Ia.Attributes["class"] = "fa fa-circle fa-2x";
        //    }


        //    if (Esta == true)
        //    {
        //        if (Cb.SelectedItem.Value == "Ninguno")
        //        {
        //            Tx.Attributes.Add("style", "");
        //        }
        //    }
        //}

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


        private void Campos_ordenados()
        {
            this.Session["SelectQR"] = ConfigurationManager.AppSettings.Get("CRcode");

            ddCabeceraPageSize.Items.Clear();
            ddCabeceraPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddCabeceraPageSize.Items.Insert(1, new ListItem("20", "20"));
            ddCabeceraPageSize.Items.Insert(2, new ListItem("30", "30"));
            ddCabeceraPageSize.Items.Insert(3, new ListItem("Todos", "1000"));
            //ddCabeceraPageSize.Items.Insert(0, new ListItem("5", "5"));
            //ddCabeceraPageSize.Items.Insert(1, new ListItem("10", "10"));
            //ddCabeceraPageSize.Items.Insert(2, new ListItem("25", "25"));
            //ddCabeceraPageSize.Items.Insert(3, new ListItem("50", "50"));
            //ddCabeceraPageSize.Items.Insert(4, new ListItem("100", "100"));
            //ddCabeceraPageSize.Items.Insert(5, new ListItem("200", "200"));
            //ddCabeceraPageSize.Items.Insert(6, new ListItem("500", "500"));
            //ddCabeceraPageSize.Items.Insert(7, new ListItem("Todos", "1000"));

            ddControlPageSize.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            ddListaPageSize.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());

            DrNombre.Items.Clear();
            DrNombre.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            DrNombre.Items.Insert(1, new ListItem("Empresa", "EMPRESA"));
            DrNombre.Items.Insert(2, new ListItem("Fecha Preparación", "FECHAPREPARACION"));
            DrNombre.Items.Insert(2, new ListItem("Fecha Carga", "FECHACARGA"));
            DrNombre.Items.Insert(3, new ListItem("Teléfono", "TELEFONO"));
            DrNombre.Items.Insert(4, new ListItem("Número", "NUMERO"));
            DrNombre.Items.Insert(5, new ListItem("País", "PAIS"));
            DrNombre.Items.Insert(6, new ListItem("Matrícula", "MATRICULA"));
            DrNombre.Items.Insert(7, new ListItem("Transportista", "TRANSPORTISTA"));
            DrNombre.Items.Insert(8, new ListItem("Estado", "ESTADO"));


            //DrOrden2.Items.Clear();
            //DrOrden3.Items.Clear();
            //DrOrden4.Items.Clear();
            //DrOrden5.Items.Clear();
            //DrOrden6.Items.Clear();
            //DrOrden7.Items.Clear();

            //DrOrden2.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());
            //DrOrden3.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());
            //DrOrden4.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());
            //DrOrden5.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());
            //DrOrden6.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());
            //DrOrden7.Items.AddRange(DrOrden1.Items.OfType<ListItem>().ToArray());

            DrNombre.SelectedIndex = -1;
            //DrOrden2.SelectedIndex = -1;
            //DrOrden3.SelectedIndex = -1;
            //DrOrden4.SelectedIndex = -1;
            //DrOrden5.SelectedIndex = -1;
            //DrOrden6.SelectedIndex = -1;
            //DrOrden7.SelectedIndex = -1;

            //DrControl1.Items.Clear();
            //DrControl1.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrControl1.Items.Insert(1, new ListItem("EMPRESA", "Empresa"));
            //DrControl1.Items.Insert(2, new ListItem("CLIENTEPROVEEDOR", "Cliente Proveedor"));
            //DrControl1.Items.Insert(3, new ListItem("NOMBREFISCAL", "Nombre Cliente"));
            //DrControl1.Items.Insert(4, new ListItem("NUMERO", "Número"));
            //DrControl1.Items.Insert(5, new ListItem("LINEA", "Línea"));
            //DrControl1.Items.Insert(6, new ListItem("ARTICULO", "Articulo"));
            //DrControl1.Items.Insert(7, new ListItem("UDSPEDIDAS", "Unidades Pedidas"));
            //DrControl1.Items.Insert(8, new ListItem("UDSSERVIDAS", "Unidades Servidas"));
            //DrControl1.Items.Insert(9, new ListItem("UDSENCARGA", "Unidades en Carga"));
            //DrControl1.Items.Insert(10, new ListItem("UDSPENDIENTES", "Unidades Pendientes"));
            //DrControl1.Items.Insert(11, new ListItem("UDSACARGAR", "Unidades a Cargar"));
            //DrControl1.Items.Insert(12, new ListItem("NUMPALET", "Número Palet"));
            //DrControl1.Items.Insert(13, new ListItem("RUTA", "Ruta"));
            //DrControl1.Items.Insert(14, new ListItem("FECHAENTREGA", "Fecha entrega"));
            //DrControl1.Items.Insert(15, new ListItem("ESTADO", "Estado"));
            //DrControl1.Items.Insert(16, new ListItem("ID_CABECERA", "Id Cabecera"));


            //DrControl2.Items.Clear();
            //DrControl3.Items.Clear();
            //DrControl4.Items.Clear();
            //DrControl5.Items.Clear();
            //DrControl6.Items.Clear();
            //DrControl7.Items.Clear();

            //DrControl2.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());
            //DrControl3.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());
            //DrControl4.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());
            //DrControl5.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());
            //DrControl6.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());
            //DrControl7.Items.AddRange(DrControl1.Items.OfType<ListItem>().ToArray());

            //DrControl1.SelectedIndex = -1;
            //DrControl2.SelectedIndex = -1;
            //DrControl3.SelectedIndex = -1;
            //DrControl4.SelectedIndex = -1;
            //DrControl5.SelectedIndex = -1;
            //DrControl6.SelectedIndex = -1;
            //DrControl7.SelectedIndex = -1;


            //DrLista1.Items.Clear();
            //DrLista1.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrLista1.Items.Insert(1, new ListItem("ID_CABECERA", "Id Cabecera"));
            //DrLista1.Items.Insert(2, new ListItem("EMPRESA", "Empresa"));
            //DrLista1.Items.Insert(3, new ListItem("CLIENTEPROVEEDOR", "Cliente Proveedor"));
            //DrLista1.Items.Insert(4, new ListItem("NOMBREFISCAL", "Nombre Cliente"));
            //DrLista1.Items.Insert(5, new ListItem("RUTA", "Ruta"));
            //DrLista1.Items.Insert(6, new ListItem("NUMERO", "Número"));
            //DrLista1.Items.Insert(7, new ListItem("NUMERO_LINEA", "Línea"));
            //DrLista1.Items.Insert(8, new ListItem("ARTICULO", "Articulo"));
            //DrLista1.Items.Insert(9, new ListItem("UDSENCARGA", "Unidades en Carga"));
            //DrLista1.Items.Insert(10, new ListItem("NUMPALET", "Número Palet"));
            //DrLista1.Items.Insert(11, new ListItem("POSICIONCAMION", "Posición Camión"));
            ////DrLista1.Items.Insert(12, new ListItem("FECHAENTREGA", "Fecha entrega"));
            //DrLista1.Items.Insert(12, new ListItem("OBSERVACIONES", "Observaciones"));
            //DrLista1.Items.Insert(13, new ListItem("ESTADO", "Estado"));

            //DrLista2.Items.Clear();
            //DrLista3.Items.Clear();
            //DrLista4.Items.Clear();
            //DrLista5.Items.Clear();
            //DrLista6.Items.Clear();
            //DrLista7.Items.Clear();

            //DrLista2.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());
            //DrLista3.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());
            //DrLista4.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());
            //DrLista5.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());
            //DrLista6.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());
            //DrLista7.Items.AddRange(DrLista1.Items.OfType<ListItem>().ToArray());

            //DrLista1.SelectedIndex = -1;
            //DrLista2.SelectedIndex = -1;
            //DrLista3.SelectedIndex = -1;
            //DrLista4.SelectedIndex = -1;
            //DrLista5.SelectedIndex = -1;
            //DrLista6.SelectedIndex = -1;
            //DrLista7.SelectedIndex = -1;

        }

        private void DejaPosAcordeon()
        {
            if (this.Session["collapse1"].ToString() == "1")
            {
                collapse1.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse1.Attributes["class"] = "panel-collapse collapse";
            }
            if (this.Session["collapse2"].ToString() == "1")
            {
                collapse2.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse2.Attributes["class"] = "panel-collapse collapse";
            }
            if (this.Session["collapse3"].ToString() == "1")
            {
                collapse3.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse3.Attributes["class"] = "panel-collapse collapse";
            }
            if (this.Session["collapse4"].ToString() == "1")
            {
                collapse4.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse4.Attributes["class"] = "panel-collapse collapse";
            }
            if (this.Session["collapse5"].ToString() == "1")
            {
                collapse5.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse5.Attributes["class"] = "panel-collapse collapse";
            }
            if (this.Session["collapse6"].ToString() == "1")
            {
                collapse6.Attributes["class"] = "panel-collapse collapse in";
            }
            else
            {
                collapse6.Attributes["class"] = "panel-collapse collapse";
            }
            //Carga_tabla();
            //Carga_tablaLista();

            //gvCabecera.DataBind();
            //gvControl.DataBind();
            //gvLista.DataBind();
        }

        private void Carga_los_palet()
        {
            string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + TxtNumero.Text; // this.Session["IDCabecera"].ToString();

            Lberror.Text += SQL + "1- Carga_los_palet " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 1- Carga_los_palet " + Variables.mensajeserver;

            this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
            CreaPalets(dt);
        }

        //protected void BtMenus_Click(object sender, EventArgs e)
        //{
        //    if (divMenu.Visible == true)
        //    {
        //        divMenu.Visible = false;
        //        HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
        //        li.Attributes.CssStyle.Add("margin", "0");
        //        MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
        //    }
        //    else
        //    {
        //        divMenu.Visible = true;
        //        HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
        //        li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
        //        MasMinMenu.Attributes["class"] = "fa fa-chevron-left fa-2x";
        //    }
        //}

        public void Carga_Menus()
        {
            //pagevistaform.Attributes["style"] = "";
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu0");
            HtmlGenericControl panel = (HtmlGenericControl)cont.FindControl("accordion0");

            if (this.Session["Menu"].ToString() == "1")
            {
                //el 1
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = true;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                accordion5.Visible = false;
                accordion6.Visible = false;

                //if (CaCheck.Checked == false)
                //{
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    Carga_tablaCabecera();
                }
                else
                {
                    Carga_tablaCabeceraClose();
                }
            }
            if (this.Session["Menu"].ToString() == "2")
            {
                //el 3
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade";
                accordion3.Visible = true;
                accordion2.Visible = false;
                accordion.Visible = false;
                accordion4.Visible = false;
                accordion5.Visible = false;
                accordion6.Visible = false;
                Carga_tabla();
            }
            if (this.Session["Menu"].ToString() == "3")
            {
                //el 4
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade";
                accordion4.Visible = true;
                accordion.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion5.Visible = false;
                accordion6.Visible = false;
                Carga_tablaLista();
            }
            if (this.Session["Menu"].ToString() == "4")
            {
                //el 2
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade";
                accordion2.Visible = true;
                accordion.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                accordion5.Visible = false;
                accordion6.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "5")
            {
                //el 5
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";

                //pagevistaform.Attributes["style"] = "height: 100%;";

                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade";
                accordion5.Visible = true;
                accordion.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                accordion6.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "6")
            {
                //el 5
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";

                //pagevistaform.Attributes["style"] = "height: 100%;";

                panel = (HtmlGenericControl)cont.FindControl("accordion6");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                accordion6.Visible = true;
                accordion.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                accordion5.Visible = false;

                Carga_Tablas_Variedades();
            }

            //if (divMenu.Visible == false)
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
            //    li.Attributes.CssStyle.Add("margin", "0");
            //}
            //else
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
            //    li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
            //}
        }

        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            gvCabecera.EditIndex = -1;
            gvControl.EditIndex = -1;
            gvLista.EditIndex = -1;

            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu1") { this.Session["Menu"] = "1"; }
            if (Miro == "aMenu2") { this.Session["Menu"] = "2"; }
            if (Miro == "aMenu3") { this.Session["Menu"] = "3"; }
            if (Miro == "aMenu4") { this.Session["Menu"] = "4"; }
            if (Miro == "aMenu5") { this.Session["Menu"] = "5"; }
            if (Miro == "aMenu6") { this.Session["Menu"] = "6"; }
            this.Session["GridOrden"] = "";
            this.Session["GridEdicion"] = "0";
            Carga_Menus();


        }

        protected void BtLimpiaUd_Click(Object sender, EventArgs e)
        {
            BtModificaVariedad.Visible = false;
            btNuevaVariedad.Visible = true;
            BtGuardaVariedad.Visible = false;
            BtCancelaVariedad.Enabled = false;
            BtDeleteVariedad.Enabled = false;

            TxtCodGolden.Text = "";
            TxtCodGolden.Text = "";
            TxtVariedad.Text = "";
            TextTipoFormato.Text = "";
            TxtTipoPlanta.Text = "";
            TxtDTipoPlanta.Text = "";
            TxtNumPlanta.Text = "";

            Carga_Tablas_Variedades();
        }

        private void Carga_Tablas_Variedades()
        {

            string SQL = " SELECT  ZID , ZTIPO_PLANTA, ZDESCRIPTIPO FROM ZTIPOPLANTADESCRIP ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            GvTablaDes.DataSource = dt;
            GvTablaDes.DataBind();

            SQL = " SELECT  ZID, ZTIPO_PLANTA, ZTIPO_FORMATO, ZNUMERO_PLANTAS FROM ZBANDEJAS ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";

            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];

            GvBandeja.DataSource = dt;
            GvBandeja.DataBind();


            SQL = " SELECT  ZID, ZTIPO_PLANTA, ZVARIEDAD, ZCODGOLDEN, UD_BASE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN ORDER BY ZID ";
            //SQL += " WHERE ZTIPO_PLANTA LIKE '%" + "%' ";

            dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            GvGolden.DataSource = dt;
            GvGolden.DataBind();

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
            Carga_tablaLista();

        }


        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            Variables.Error = "";
            Carga_tablaNueva();
            Carga_tablaLista();
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

        protected void ImgOrdenMin_Click(object sender, EventArgs e)
        {
            VistaOrden.Visible = false;
            VistaOrdenNO.Visible = true;
            PanelGeneralCabecera.Visible = true;
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



        protected void ImageBtn_Click(object sender, EventArgs e)
        {

            string SQL = "UPDATE ZCARGA_LINEA set ESTADO =  2 ";

            Variables.Error = "";
            Lberror.Text = SQL;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaLista();


            SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2 ,";
            SQL += " ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            SQL += " WHERE NUMERO = " + TxtNumero.Text;

            Variables.Error = "";
            Lberror.Text = SQL;

            DBHelper.ExecuteNonQuery(SQL);
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();

            this.Session["Menu"] = "5";
            Carga_Menus();
            PNreportLista.Visible = false;
            DivEtiquetas.Visible = true;

            CargaTodaLista("");
            DivEtiquetas.Visible = true;
            return;
            //string SQL = "";
            //int i = 1;
            //DataTable dt = ConsultaPalet();

            //HtmlGenericControl Eldrag = (HtmlGenericControl)FindALL(fuego.Controls, "drag0");
            ////HtmlGenericControl Eldrag = (HtmlGenericControl)FindALL(this.Page.Controls, "drag0");
            //string miro = Eldrag.ID;
            //miro = ElID;
            //HtmlGenericControl Mfuego = (HtmlGenericControl)FindControlRecursive(idPadre, "fuego");
            ////Control fuego = FindControl("drag0");
            //if (fuego != null)
            //{
            //    // Get control's parent.
            //    Control myControl2 = fuego.Parent;
            //    miro = myControl2.ID;
            //}
            //else
            //{
            //    miro = "Control not found";
            //}


            //return;

            //HtmlGenericControl Mfuego = (HtmlGenericControl)FindControlRecursive(idPadre, "fuego");
            //HtmlGenericControl Magua = (HtmlGenericControl)FindControlRecursive(idPadre, "agua");

            /*           HtmlGenericControl Eldrag = (HtmlGenericControl)FindControlRecursive(Mfuego, "drag2")*/
            ;


            //foreach (Control c in Mfuego.Controls)
            //{
            //    string miro = c.ID;
            //    if (miro != null) 
            //    { 
            //        if (c.ID.Contains("drag"))
            //        {
            //            foreach (Control childc in c.Controls)
            //            {
            //                if (childc.ID.Contains("lbPalet"))
            //                {
            //                    if (childc is Label)
            //                    {
            //                        string Etiqueta = ((Label)childc).Text;
            //                        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Etiqueta, "-");
            //                        SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION =" + i;
            //                        SQL += " WHERE NUMERO_LINEA = " + CadaLinea[0];
            //                        //SQL += " AND POSICIONCAMION = " + CadaLinea[1];
            //                        i += 2;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //int a = 2;
            //foreach (Control c in agua.Controls)
            //{
            //    string miro = c.ID;
            //    if (c.ID.Contains("drag"))
            //    {
            //        foreach (Control childc in c.Controls)
            //        {
            //            if (childc.ID.Contains("lbPalet"))
            //            {
            //                if (childc is Label)
            //                {
            //                    string Etiqueta = ((Label)childc).Text;
            //                    string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Etiqueta, "-");
            //                    SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION =" + a;
            //                    SQL += " WHERE NUMERO_LINEA = " + CadaLinea[0];
            //                    //SQL += " AND POSICIONCAMION = " + CadaLinea[1];
            //                    a += 2;
            //                }
            //            }
            //        }
            //    }
            //}

            //int resto = 0;
            //if (i < a)
            //{
            //    resto = a + 1;
            //}
            //else
            //{
            //    resto = i + 1;
            //}

            //foreach (Control c in container2.Controls)
            //{
            //    string miro = c.ID;
            //    if (c.ID.Contains("drag"))
            //    {
            //        foreach (Control childc in c.Controls)
            //        {
            //            if (childc.ID.Contains("lbPalet"))
            //            {
            //                if (childc is Label)
            //                {
            //                    string Etiqueta = ((Label)childc).Text;
            //                    string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Etiqueta, "-");
            //                    SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION =" + resto;
            //                    SQL += " WHERE NUMERO_LINEA = " + CadaLinea[0];
            //                    //SQL += " AND POSICIONCAMION = " + CadaLinea[1];
            //                    resto += 1;
            //                }
            //            }
            //        }
            //    }
            //}
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
            DivEtiquetas.Visible = false;
        }

        protected void btListaEmpresas_Click(object sender, EventArgs e)
        {
            if (EmpresaGV.Visible == true)
            {
                ContentPlaceHolder cont = new ContentPlaceHolder();
                HtmlGenericControl li = null;
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                li = (HtmlGenericControl)cont.FindControl("idgvCabecera");
                //li = (HtmlGenericControl)FindControl("idgvCabecera");
                li.Attributes["class"] = "fa fa-angle-down fa-2x";
                EmpresaGV.Visible = false;
            }
            else
            {
                ContentPlaceHolder cont = new ContentPlaceHolder();
                HtmlGenericControl li = null;
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                li = (HtmlGenericControl)cont.FindControl("idgvCabecera");
                li.Attributes["class"] = "fa fa-angle-up fa-2x";
                //if (CaCheck.Checked == false)
                //{
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    Carga_tablaCabecera();
                }
                else
                {
                    Carga_tablaCabeceraClose();
                }
                //Carga_tablaCabecera();
                EmpresaGV.Visible = true;
            }
        }



        private void Limpia_CajasCaberera()
        {
            DrEmpresa.SelectedIndex = -1;
            DrPais.SelectedIndex = -1;
            TxtNumero.Text = "";
            TxtFechaPrepara.Text = "";
            TxtFecha.Text = "";
            TxtTelefono.Text = "";
            TxtMatricula.Text = "";
            TxtObservaciones.Text = "";
            TxtEstadoCab.Text = "";
            TxtTransportista.Text = "";
            TxtPais.Text = "";
            TxtEmpresa.Text = "";
            DrTransportista.SelectedIndex = -1;
            Btreviertelote.Visible = false;
            //LBCountLista.Visible = true;
            LBCountLista.Text = "Contiene: 0 Líneas de carga";
        }

        protected void BtCancelCabecera_Click(object sender, EventArgs e)
        {
            DrSelCab.Items.Clear();
            DrOrdenMin.Items.Clear();
            DrOrdenMin.Attributes.Add("style", "background-color:#ffffff");
            DrSelCab.Attributes.Add("style", "background-color:#ffffff");

            HtmlButton btn = (HtmlButton)sender;

            ContentPlaceHolder cont = new ContentPlaceHolder();
            HtmlButton li = null;
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            li = (HtmlButton)cont.FindControl("BtnuevaCabecera");

            btn.Visible = false;
            li.Visible = true;

            BtCancelCabecera.Visible = false;
            BtnuevaCabecera.Visible = true;
            //BtnNewCabecera.Visible = true;

            //TxtNumero.Enabled = true;
            DrEmpresa.Enabled = true;
            DrPais.Enabled = true;
            TxtFechaPrepara.Enabled = true;
            TxtFecha.Enabled = true;
            TxtTelefono.Enabled = true;
            TxtMatricula.Enabled = true;
            TxtObservaciones.Enabled = true;
            TxtEstadoCab.Enabled = true;
            TxtTransportista.Enabled = true;
            TxtPais.Enabled = true;
            TxtEmpresa.Enabled = true;
            DrTransportista.Enabled = true;

            Limpia_CajasCaberera();

            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();
            PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");

        }

        //protected void BtNewCabecera_Click(object sender, EventArgs e)
        //{
        //    BtnuevaCabecera.Visible = true;
        //    BtnNewCabecera.Visible = false;
        //    DrEmpresa.Enabled = true;
        //    DrPais.Enabled = true;
        //    TxtFecha.Enabled = true;
        //    TxtTelefono.Enabled = true;
        //    TxtMatricula.Enabled = true;
        //    TxtObservaciones.Enabled = true;
        //    TxtTransportista.Enabled = true;
        //    TxtPais.Enabled = true;
        //    TxtEmpresa.Enabled = true;
        //    DrTransportista.Enabled = true;

        //    DrEmpresa.SelectedIndex = -1;
        //    DrPais.SelectedIndex = -1;
        //    TxtNumero.Text = "";
        //    TxtFecha.Text = "";
        //    TxtTelefono.Text = "";
        //    TxtMatricula.Text = "";
        //    TxtObservaciones.Text = "";
        //    TxtTransportista.Text = "";
        //    TxtPais.Text = "";
        //    TxtEmpresa.Text = "";
        //    DrTransportista.SelectedIndex = -1;
        //}

        protected void BtnuevaCabecera_Click(object sender, EventArgs e)
        {
            PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");
            if (TxtEmpresa.Text == "" && TxtObservaciones.Text == "" && TxtMatricula.Text == "" && TxtPais.Text == "" && TxtTelefono.Text == "" && TxtTransportista.Text == "")
            {
                //TextAlerta.Text = "Debe añadir algún dato en las casillas obligatorias para crear un registro nuevo.";
                //alerta.Visible = true;
                Lbmensaje.Text = "Debe añadir algún dato en las casillas obligatorias para crear un registro nuevo.";
                Asume.Visible = true;
                Modifica.Visible = false;
                cuestion.Visible = false;
                Decide.Visible = false;
                //btnasume.Visible = true;
                windowmessaje.Visible = true;
                //BtnAcepta.Visible = false;
                //BTnNoAcepta.Visible = false;
                MiCloseMenu();

                return;
            }
            int N = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT ID_SECUENCIA FROM ZCARGA_CABECERA A WHERE ID = 1 ", null));
            TxtNumero.Text = N.ToString();
            string SQL = "UPDATE ZCARGA_CABECERA SET ID_SECUENCIA = (ID_SECUENCIA + 1) WHERE ID = 1 ";
            DBHelper.ExecuteNonQuery(SQL);
            SQL = "INSERT INTO ZCARGA_CABECERA (NUMERO, ID_SECUENCIA, EMPRESA, PAIS, FECHACARGA, ";
            SQL += " TELEFONO, MATRICULA, TRANSPORTISTA, ESTADO, OBSERVACIONES, FECHAPREPARACION , ZSYSDATE)";
            SQL += " VALUES(" + TxtNumero.Text + "," + N + ",'" + TxtEmpresa.Text + "','" + TxtPais.Text + "','";
            if (TxtFecha.Text == "")
            {
                SQL += System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','";
            }
            else
            {
                SQL += TxtFecha.Text + "','";
            }
            TxtEstadoCab.Text = "Nuevo";
            SQL += TxtTelefono.Text + "','" + TxtMatricula.Text + "','" + TxtTransportista.Text + "',0,'" + TxtObservaciones.Text + "','";
            if (TxtFechaPrepara.Text == "")
            {
                SQL += System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','";
            }
            else
            {
                SQL += TxtFechaPrepara.Text + "','";
            }
            SQL += System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ) ";

            DBHelper.ExecuteNonQuery(SQL);

            SeleccionCabecera();
            //LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + TxtEmpresa.Text + ", Pais: " + TxtPais.Text + ", Fecha: " + TxtFecha.Text;
            //LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + TxtTransportista.Text + ")";
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();
            //BtnuevaCabecera.Visible = false;
            //BtnNewCabecera.Visible = true;
            HtmlButton btn = (HtmlButton)sender;

            ContentPlaceHolder cont = new ContentPlaceHolder();
            HtmlButton li = null;
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            li = (HtmlButton)cont.FindControl("BtnuevaCabecera");

            //HtmlButton li = (HtmlButton)FindControl("BtnuevaCabecera");
            btn.Visible = true;
            li.Visible = false;

            BtCancelCabecera.Visible = true;
            BtnuevaCabecera.Visible = false;
            //BtnNewCabecera.Visible = true;

            //TxtNumero.Enabled = true;
            DrEmpresa.Enabled = false;
            DrPais.Enabled = false;
            TxtFechaPrepara.Enabled = false;
            TxtFecha.Enabled = false;
            TxtTelefono.Enabled = false;
            TxtMatricula.Enabled = false;
            TxtObservaciones.Enabled = false;
            TxtEstadoCab.Enabled = false;
            TxtTransportista.Enabled = false;
            TxtPais.Enabled = false;
            TxtEmpresa.Enabled = false;
            DrTransportista.Enabled = false;
        }

        private void ColorCabecera()
        {
            if (TxtEstadoCab.Text == "Nuevo")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#c5e1eb");
                DrOrdenMin.Attributes.Add("style", "background-color:#c5e1eb");
                DrSelCab.Attributes.Add("style", "background-color:#c5e1eb");
                TxtEstadoCab.Attributes.Add("style", "background-color:#c5e1eb");

            }
            else if (TxtEstadoCab.Text == "Secuencia")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#dcf0f2");
                DrOrdenMin.Attributes.Add("style", "background-color:#dcf0f2");
                DrSelCab.Attributes.Add("style", "background-color:#dcf0f2");
                TxtEstadoCab.Attributes.Add("style", "background-color:#dcf0f2");

            }
            else if(TxtEstadoCab.Text == "En preparación")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#e5dcf2");
                DrOrdenMin.Attributes.Add("style", "background-color:#e5dcf2");
                DrSelCab.Attributes.Add("style", "background-color:#e5dcf2");
                TxtEstadoCab.Attributes.Add("style", "background-color:#e5dcf2");

            }
            else if (TxtEstadoCab.Text == "Confirmado")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#f2dcf2");
                DrOrdenMin.Attributes.Add("style", "background-color:#f2dcf2");
                DrSelCab.Attributes.Add("style", "background-color:#f2dcf2");
                TxtEstadoCab.Attributes.Add("style", "background-color:#f2dcf2");

            }
            else if (TxtEstadoCab.Text == "Cerrado")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#fcd9d9");
                DrOrdenMin.Attributes.Add("style", "background-color:#fcd9d9");
                DrSelCab.Attributes.Add("style", "background-color:#fcd9d9");
                TxtEstadoCab.Attributes.Add("style", "background-color:#fcd9d9");

            }
            else if (TxtEstadoCab.Text == "MODIFICADO")
            {
                PanelCabecera.Attributes.Add("style", "background-color:#eff2dc");
                DrOrdenMin.Attributes.Add("style", "background-color:#eff2dc");
                DrSelCab.Attributes.Add("style", "background-color:#eff2dc");
                TxtEstadoCab.Attributes.Add("style", "background-color::#eff2dc");

            }
            else
            {
                PanelCabecera.Attributes.Add("style", "background-color:#e6f2e1");
                DrOrdenMin.Attributes.Add("style", "background-color:#e6f2e1");
                DrSelCab.Attributes.Add("style", "background-color:#e6f2e1");
                TxtEstadoCab.Attributes.Add("style", "background-color:#e6f2e1");
            }
        }
        private void SeleccionCabecera()
        {

            ColorCabecera();

            //PanelCabecera.Attributes.Add("style", "background-color:#e6f2e1");

            DrSelCab.Items.Clear();
            DrSelCab.Items.Add("Número: " + TxtNumero.Text);
            DrSelCab.Items.Add("Empresa: " + TxtEmpresa.Text);// DrEmpresa.SelectedItem.Value);
            DrSelCab.Items.Add("Pais: " + TxtPais.Text);// DrPais.SelectedItem.Value);
            DrSelCab.Items.Add("Fecha preparación: " + TxtFechaPrepara.Text);
            DrSelCab.Items.Add("Fecha: " + TxtFecha.Text);
            DrSelCab.Items.Add("Teléfono: " + TxtTelefono.Text);
            DrSelCab.Items.Add("Matricula: " + TxtMatricula.Text);
            DrSelCab.Items.Add("Transportista: " + TxtTransportista.Text);  //DrTransportista.SelectedItem.Value);
            DrSelCab.Items.Add("Estado: " + TxtEstadoCab.Text);
            //DrSelCab.Attributes.Add("style", "background-color:#e6f2e1");

            DrOrdenMin.Items.Clear();
            DrOrdenMin.Items.Add(TxtNumero.Text + "; " + TxtPais.Text + "; " + TxtFecha.Text);// + DrPais.SelectedItem.Value + "; " + TxtFecha.Text);
            DrOrdenMin.Items.Add("Número: " + TxtNumero.Text);
            DrOrdenMin.Items.Add("Empresa: " + TxtEmpresa.Text);// DrEmpresa.SelectedItem.Value);
            DrOrdenMin.Items.Add("Pais: " + TxtPais.Text);//DrPais.SelectedItem.Value);
            DrOrdenMin.Items.Add("Fecha preparación: " + TxtFechaPrepara.Text);
            DrOrdenMin.Items.Add("Fecha: " + TxtFecha.Text);
            DrOrdenMin.Items.Add("Teléfono: " + TxtTelefono.Text);
            DrOrdenMin.Items.Add("Matricula: " + TxtMatricula.Text);
            DrOrdenMin.Items.Add("Transportista: " + TxtTransportista.Text);
            DrOrdenMin.Items.Add("Estado: " + TxtEstadoCab.Text);
            //DrOrdenMin.Attributes.Add("style", "background-color:#e6f2e1");

        }



        protected void checkCabeceraListas_Click(object sender, EventArgs e)
        {
            //OrdenCabecera();
            gvCabecera.EditIndex = -1;
            gvControl.EditIndex = -1;
            gvLista.EditIndex = -1;
            DrOrdenMin.Items.Clear();
            DrSelCab.Items.Clear();
            LBCountLista.Text = "Contiene: 0 Líneas de carga";
            Btreviertelote.Visible = false;
            //LBCountLista.Visible = true;
            TxtNumero.Text = "";
            TxtEmpresa.Text = "";
            TxtPais.Text = "";
            TxtFechaPrepara.Text = "";
            TxtFecha.Text = "";
            TxtMatricula.Text = "";
            TxtTelefono.Text = "";
            TxtTransportista.Text = "";
            TxtObservaciones.Text = "";
            TxtEstadoCab.Text = "";

            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "1")
            {
                this.Session["Cerrados"] = "0";
                ColorCabecera();
                Carga_tablaCabecera();
                ImgAbiertos.Visible = true;
                ImgCerrados.Visible = false;
                aTitulo.InnerText = " ORDENES DE CARGA: Cabeceras Órdenes en tratamiento";
            }
            else
            {
                //ocultar boton modificar
                this.Session["Cerrados"] = "1";
                TxtEstadoCab.Text = "Cerrado";
                ColorCabecera();
                Carga_tablaCabeceraClose();
                ImgAbiertos.Visible = false;
                ImgCerrados.Visible = true;
                aTitulo.InnerText = " ORDENES DE CARGA: Cabeceras Órdenes cerradas";
            }
            TxtEstadoCab.Text = "";
            gvCabecera.DataBind();
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            //if (chkOnOff.Checked == false)
            //{
            if (ImgEnvidos.Visible == false)
            {
                DrTransportista.Visible = false;
                DrPais.Visible = false;
                DrEmpresa.Visible = false;
                TxtTransportista.Visible = true;
                TxtPais.Visible = true;
                TxtEmpresa.Visible = true;
                ImgEnvidos.Visible = true;
                ImgNoEnvidos.Visible = false;
                //aTitulo.InnerText = " ORDENES DE CARGA: Cabeceras Órdenes";
            }
            else
            {
                DrTransportista.Visible = true;
                DrPais.Visible = true;
                DrEmpresa.Visible = true;
                TxtTransportista.Visible = false;
                TxtPais.Visible = false;
                TxtEmpresa.Visible = false;
                ImgEnvidos.Visible = false;
                ImgNoEnvidos.Visible = true;
                //aTitulo.InnerText = " ORDENES DE CARGA: Cabeceras Órdenes";
            }
        }


        protected void Btreviertelote_Click(object sender, EventArgs e)
        {
            //coloca el estado de la orden cerrada en confirmada
            string SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2,  ";
            SQL += "ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            SQL += " WHERE ID = " + this.Session["IDCabecera"].ToString();

            this.Session["EstadoCabecera"] = "2";
            Variables.Error = "";
            Lberror.Text = SQL;
            DBHelper.ExecuteNonQuery(SQL);

            Limpia_CajasCaberera();


            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            DrSelCab.Items.Clear();
            //DrOrdenMin.Items.Clear();
            //DrOrdenMin.Attributes.Add("style", "background-color:#ffffff");
            DrSelCab.Attributes.Add("style", "background-color:#ffffff");

        }


        //private void Carga_Impresoras(string ID)
        //{
        //    try
        //    {
        //        string SQL = "";
        //        if (ID == "0")
        //        {
        //            SQL = " SELECT DISTINCT(A.ZID) as IDPRINT, A.ZDESCRIPCION ";
        //            SQL += " FROM ZPRINTER A ";
        //            SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_SECUENCIA ";
        //            SQL += " INNER JOIN ZSECUENCIAS C ON B.ZID_SECUENCIA = C.ZID ";
        //        }
        //        else
        //        {
        //            SQL = " SELECT DISTINCT(B.ZID_PRINTER) as IDPRINT, A.ZDESCRIPCION, B.ZORDEN ";
        //            SQL += " FROM ZPRINTER A ";
        //            SQL += " INNER JOIN ZPRINTERFORM B ON B.ZID_PRINTER = A.ZID ";
        //            SQL += " INNER JOIN ZSECUENCIAS C ON C.ZID = B.ZID_SECUENCIA ";
        //            SQL += " WHERE C.ZID = '" + ID + "'";
        //            SQL += " ORDER BY B.ZORDEN ";
        //            //SQL = " SELECT DISTINCT(C.ZID) as IDPRINT, C.ZDESCRIPCION ";
        //            //SQL += " FROM ZSECUENCIAS A ";
        //            //SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_SECUENCIA ";
        //            //SQL += " INNER JOIN ZPRINTER C ON B.ZID_PRINTER = C.ZID ";
        //            //SQL += " WHERE A.ZID = '" + ID + "'";
        //        }

        //        DataTable dbB = Main.BuscaLote(SQL).Tables[0];
        //        DrPrinters.Items.Clear();
        //        DrPrinters.DataValueField = "IDPRINT";
        //        DrPrinters.DataTextField = "ZDESCRIPCION";
        //        DrPrinters.DataSource = dbB;
        //        DrPrinters.DataBind();
        //        Printers(DrPrinters.SelectedItem.Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Redirect("Principal.aspx");
        //    }
        //}

        protected void ImageOrden_Click(object sender, EventArgs e)
        {
            //PanelGeneralCabecera.Visible = false;
            //VistaOrden.Visible = true;
            //VistaOrdenNO.Visible = false;
        }

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {
            if (PanelgeneralFiltro.Visible == true)
            {
                PanelgeneralFiltro.Visible = false;
            }
            else
            {
                PanelgeneralFiltro.Visible = true;
            }
        }

        //protected void BtMenus_Click(object sender, EventArgs e)
        //{
        //    if(divMenu.Visible == true)
        //    {
        //        divMenu.Visible = false;
        //        HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
        //        li.Attributes.CssStyle.Add("margin", "0");
        //    }
        //    else
        //    {
        //        divMenu.Visible = true;
        //        HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
        //        li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
        //    }
        //}

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



        protected void DrEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtEmpresa.Text = DrEmpresa.SelectedItem.Text;
        }
        protected void DrPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtPais.Text = DrPais.SelectedItem.Text;
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

        

        protected void cbBuscaCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            gvLista.PageSize = Convert.ToInt32(ddListaPageSize.SelectedItem.Value);
            Carga_tablaLista();
        }

        protected void GvBandeja_PageSize_Changed(object sender, EventArgs e)
        {
        }

         protected void gvCabecera_PageSize_Changed(object sender, EventArgs e)
        {
            gvCabecera.PageSize = Convert.ToInt32(ddCabeceraPageSize.SelectedItem.Value);
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();
        }
        protected void gvControl_PageSize_Changed(object sender, EventArgs e)
        {
            gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
            Carga_tabla();
        }

        //private void OrdenCabecera()
        //{
        //    ElOrden = "";
        //    DrOrden1.BackColor = Color.FromName("#ffffff");
        //    DrOrden2.BackColor = Color.FromName("#ffffff");
        //    DrOrden3.BackColor = Color.FromName("#ffffff");
        //    DrOrden4.BackColor = Color.FromName("#ffffff");
        //    DrOrden5.BackColor = Color.FromName("#ffffff");
        //    DrOrden6.BackColor = Color.FromName("#ffffff");
        //    DrOrden7.BackColor = Color.FromName("#ffffff");


        //    if (DrOrden1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            if (ElOrden.Contains(DrOrden1.SelectedItem.Text))
        //            {}
        //            else
        //            {
        //                ElOrden = " ORDER BY " + DrOrden1.SelectedItem.Text;
        //            }

        //        }
        //        DrOrden1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", CONVERT(INT, " + DrOrden2.SelectedItem.Text + ")";
        //            }
        //        }
        //        DrOrden2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden3.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden3.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden3.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + DrOrden3.SelectedItem.Text;
        //            }
        //        }
        //        DrOrden3.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden4.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden4.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden4.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + DrOrden4.SelectedItem.Text;
        //            }
        //        }
        //        DrOrden4.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden5.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden5.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden5.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + DrOrden5.SelectedItem.Text;
        //            }
        //        }
        //        DrOrden5.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden6.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden6.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden6.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + DrOrden6.SelectedItem.Text;
        //            }
        //        }
        //        DrOrden6.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrOrden7.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + DrOrden7.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(DrOrden7.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + DrOrden7.SelectedItem.Text;
        //            }
        //        }
        //        DrOrden7.BackColor = Color.FromName("#fcf5d2");
        //    }
        //    //if (CaCheck.Checked == false)
        //    //{
        //    //    Carga_tablaCabecera();
        //    //}
        //    //else
        //    //{
        //    //    Carga_tablaCabeceraClose();
        //    //}
        //    //Carga_tablaCabecera();
        //}
        //Depurar unificar este procedimiento

        protected void PrintReportOff_Click(object sender, EventArgs e)
        {
            ReportViewer1.Visible = false;

            DVtLista.Visible = true;
            DVtListaOff.Visible = false;

            PNreportLista.Visible = false;
            PNFiltrosLista.Visible = true;
            PNGridLista.Visible = true;
        }
        protected void sellectAllEmpleado(object sender, EventArgs e)
        {
        }

        protected void PrintReport_Click(object sender, EventArgs e)
        {
            //DVtLista.Visible = false;
            //DVtListaOff.Visible = true;
            DataTable DT = null;
            string ZRuta = "";
            string ZReporte = "";
            string ZDataSet = "";

            if (TxtNumero.Text == "")
            {
                Lbmensaje.Text = "No tiene seleccionada una Orden de Carga para poder generar un informe.";
                cuestion.Visible = true;
                Asume.Visible = true;
                Modifica.Visible = false;
                cuestion.Visible = false;
                Decide.Visible = false;
                windowmessaje.Visible = false;
                MiCloseMenu();

                //BtnAcepta.Visible = false;
                //BTnNoAcepta.Visible = false;
                return;
            }

            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                this.Session["Centro"] = "ABIERTO";
            }
            else
            {
                this.Session["Centro"] = "CERRADO";
            }

            string SQL = " SELECT A.REPORTE , B.ZRUTA, B.ZREPORTE, B.ZDATASET, B.ZSQL  ";
            SQL += " FROM REC_PARAM A, ZINFORMES B";
            SQL += " WHERE A.REPORTE = B.ZID";
            SQL += " AND A.CENTRO = '" + this.Session["Centro"].ToString() + "' ";

            DT = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in DT.Rows)
            {
                ZRuta = filas["ZRUTA"].ToString();
                ZReporte = filas["ZREPORTE"].ToString();
                ZDataSet = filas["ZSQL"].ToString();
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
                break;
            }


            try
            {
                ReportViewer1.Visible = false;
                //Limpio los enlaces del ReportViewer 
                ReportViewer1.LocalReport.DataSources.Clear();


                //string logo = t1.Text;
                //string logo = "http://localhost/webRdlc/images/logo.jpg";


                //if (CaCheck.Checked == false)
                //{
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);

                    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rpalvcamionlista.rdlc");


                    SQL = " SELECT ";
                    SQL += " B.ID_CABECERA AS ZCAMION,   ";
                    SQL += " B.EMPRESA AS ZEMPRESA,    ";
                    SQL += " E.PAIS AS ZPAIS,    ";
                    SQL += " FORMAT(E.FECHACARGA, 'dd-MM-yyyy') AS ZFECHACARGA,   ";
                    SQL += " E.MATRICULA AS ZMATRICULA,    ";
                    SQL += " E.TRANSPORTISTA AS ZTRANSPORTISTA,    ";
                    SQL += " E.TELEFONO AS ZTELEFONO,    ";
                    SQL += " E.OBSERVACIONES AS ZOBSERVACION,    ";
                    SQL += " B.POSICIONCAMION AS ZPOSICIONCAMION,   ";
                    SQL += " B.NUMERO AS ZPEDIDO,       ";
                    SQL += " B.NOMBREFISCAL AS ZCLIENTE,   ";
                    SQL += " D.ZTIPO_FORMATO + ' ' + CONVERT(VARCHAR(255), D.ZNUMERO_PLANTAS) AS ZFORMATO, ";
                    SQL += " C.ZVARIEDAD,    ";
                    if(chkbUdCarga.Checked == true)
                    {
                        SQL += " REPLACE(B.UDSENCARGA, '.0', '') AS ZNUMCAJAS,";
                        //SQL += " CONVERT(DECIMAL(8, 2), B.UDSENCARGA) as UNIDADCARGA,";
                        //SQL += " SUBSTRING(B.UDSENCARGA, 0, CHARINDEX('.', B.UDSENCARGA)) AS ZNUMCAJAS,";
                        chkbUdCarga.Checked = false;
                    }
                    else
                    {
                        SQL += " CONVERT(INT, CONVERT(DECIMAL(8, 3), B.UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)  AS ZNUMCAJAS,";
                    }
                    //SQL += " CONVERT(INT, REPLACE(UDSENCARGA, '.', '')) / CONVERT(INT, D.ZNUMERO_PLANTAS)  AS ZNUMCAJAS, "; + ' ' + CONVERT(varchar(15),CAST(E.ZSYSDATE AS TIME),100)
                    //SQL += " REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(Money, CONVERT(INT, (CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)),1),'.00',''),',','.')  AS ZNUMPLANTAS,";
                    SQL += " CONVERT(INT, (CONVERT(DECIMAL(8, 3), B.UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000) AS ZNUMPLANTAS,";
                    SQL += " D.ZNUMERO_PLANTAS AS ZNUMPLANTAS,    ";
                    SQL += " B.OBSERVACIONES AS ZOBSERVACIONES, ";
                    SQL += " CONVERT(DECIMAL (8,2), B.NUMPALET) AS NUMPALET,   ";
                    SQL += " (SELECT  COUNT(DISTINCT POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = E.NUMERO) AS ZMIPALET, ";
                    SQL += " CONVERT(varchar(20),FORMAT (E.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
                    SQL += " FROM ZCARGA_LINEA B, ZCARGA_CABECERA E , ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZBANDEJAS D ";
                    SQL += " WHERE C.ZTIPO_PLANTA = D.ZTIPO_PLANTA ";
                    SQL += " AND C.ZCODGOLDEN = B.ARTICULO ";
                    //Gloria 11/11/2021 debe mirar como ajustar tipo formato para lineas de carga con ZBANDEJAS (¿columna nueva? por no tener zentrada para ver A.UNIDADES)
                    SQL += " AND D.ZTIPO_FORMATO = 'CAJAS' ";
                    SQL += " AND B.ID_CABECERA = E.NUMERO ";
                    SQL += " AND E.NUMERO = " + TxtNumero.Text;
                    SQL += " ORDER BY CONVERT(INT, B.POSICIONCAMION) ";
                }
                else
                {
                    //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rpalvcamionlote.rdlc");
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);


                    SQL = " SELECT ";
                    SQL += " A.LOTE AS ZLOTE,   ";
                    SQL += " B.ID_CABECERA AS ZCAMION,   ";
                    SQL += " B.EMPRESA AS ZEMPRESA,    ";
                    SQL += " E.PAIS AS ZPAIS,    ";
                    SQL += " FORMAT(E.FECHACARGA, 'dd-MM-yyyy') AS ZFECHACARGA,   ";
                    SQL += " E.MATRICULA AS ZMATRICULA,    ";
                    SQL += " E.TRANSPORTISTA AS ZTRANSPORTISTA,    ";
                    SQL += " E.TELEFONO AS ZTELEFONO,    ";
                    SQL += " E.OBSERVACIONES AS ZOBSERVACION,    ";
                    SQL += " B.POSICIONCAMION AS ZPOSICIONCAMION,   ";
                    SQL += " B.NUMERO AS ZPEDIDO,       ";
                    SQL += " B.NOMBREFISCAL AS ZCLIENTE,   ";
                    SQL += " D.ZTIPO_FORMATO + ' ' + CONVERT(VARCHAR(255), D.ZNUMERO_PLANTAS) AS ZFORMATO, ";
                    SQL += " C.ZVARIEDAD,    ";
                    //SQL += " CONVERT(DECIMAL(8,3), UDSENCARGA) / CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS)  AS ZNUMCAJAS, "; + ' ' + CONVERT(varchar(15),CAST(E.ZSYSDATE AS TIME),100)
                    //SQL += " (CONVERT(DECIMAL(8,3), UDSENCARGA) / CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS) AS ZNUMPLANTAS,    ";
                    //SQL += " CONVERT(INT, CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)  AS ZNUMCAJAS,";
                    SQL += " CONVERT(DECIMAL (8,0),A.NUM_UNIDADES) AS ZNUMCAJAS, ";
                    //SQL += " A.NUM_UNIDADES AS ZNUMCAJAS, ";
                    //SQL += " CONVERT(INT, (CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000) AS ZNUMPLANTAS,";
                    SQL += "  CONVERT(INT, (CONVERT(DECIMAL(8, 3), A.NUM_UNIDADES) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS))) AS ZNUMPLANTAS, ";
                    SQL += " B.OBSERVACIONES AS ZOBSERVACIONES, ";
                    SQL += " CONVERT(DECIMAL (8,2), B.NUMPALET) AS NUMPALET ,  ";
                    //SQL += " (SELECT COUNT(DISTINTC B.POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = " + TxtNumero.Text + ") AS SUMPALET";

                    SQL += " (SELECT  COUNT(DISTINCT POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = E.NUMERO) AS ZMIPALET, ";
                    SQL += " CONVERT(varchar(20),FORMAT (E.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
                    SQL += " FROM ZENTRADA A , ZCARGA_LINEA B, ZCARGA_CABECERA E , ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZBANDEJAS D ";
                    SQL += " WHERE A.TIPO_FORM = 'Venta' ";
                    //SQL += " and B.HASTA LIKE A.HASTA + '%' "; //Generar el campo hasta de carga_linea
                    //restricción de la consulta para retorno de carro y salto de linea
                    SQL += " and REPLACE(REPLACE(B.HASTA, CHAR(10), ''), CHAR(13), '') = REPLACE(REPLACE(A.HASTA, CHAR(10), ''), CHAR(13), '') ";
                    SQL += " and C.ZTIPO_PLANTA = A.TIPO_PLANTA ";
                    SQL += " and A.VARIEDAD = C.ZVARIEDAD ";
                    SQL += " and A.TIPO_PLANTA = D.ZTIPO_PLANTA ";
                    SQL += " AND D.ZTIPO_FORMATO = A.UNIDADES   ";
                    SQL += " AND B.ID_CABECERA = E.NUMERO ";
                    SQL += " AND E.NUMERO = " + TxtNumero.Text;
                    SQL += " ORDER BY CONVERT(INT, B.POSICIONCAMION) ";
                }



                //ReportViewer1.LocalReport.EnableExternalImages = true;

                //#region "RECUPERACIÓN DATOS"



                //SQL += " INNER JOIN ZCARGA_LINEA B ";
                //SQL += " ON  A.HASTA like (CONVERT(VARCHAR(255), B.ID_CABECERA) + '|' + B.CLIENTEPROVEEDOR + '|' + CONVERT(VARCHAR(255), B.NUMERO) + '|%'  ";
                //SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C ";
                //SQL += " ON C.ZCODGOLDEN = B.ARTICULO ";
                //SQL += " INNER JOIN ZBANDEJAS D ";
                //SQL += " ON C.ZTIPO_PLANTA = D.ZTIPO_PLANTA ";
                //SQL += " INNER JOIN ZCARGA_CABECERA E ";
                //SQL += " ON B.ID_CABECERA = E.NUMERO  ";
                //SQL += " WHERE A.TIPO_FORM = 'Venta'";
                //SQL += " AND  E.NUMERO = " + TxtNumero.Text;

                DataTable dt = Main.BuscaLote(SQL).Tables[0];


                dtsE.Tables.RemoveAt(0);    //Eliminamos la tabla que crea por defecto
                                            //DataTable dtCopy = dt.Copy();


                //DataTable dtCopy = dt.ToTable.DefaultView.ToTable(False, strSelectedCols)).Tables(0).Clone();
                dtsE.Tables.Add(dt.Copy());   //Añadimos la tabla que acabamos de crear dtlistacamion



                //if (CaCheck.Checked == false)
                //{
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtalvcamionlote", dtsE.Tables[0]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsE.Tables[0]));
                }
                else
                {
                    //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtalvcamionlote", dtsE.Tables[0]));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsE.Tables[0]));
                }

                //ReportDataSource datasource = new ReportDataSource("DTListaCamion", dtsE.Tables[0]);
                //ReportViewer1.LocalReport.DataSources.Add(datasource);

                //Ahora abre menu 5
                this.Session["Menu"] = "5";
                Carga_Menus();
                PNreportLista.Visible = true;
                DivEtiquetas.Visible = false;


                ReportViewer1.DataBind();

                //ReportParameter[] parameters = new ReportParameter[1];  //Array que contendrá los parámetros
                //parameters[0] = new ReportParameter("logo", logo);      //Establecemos el valor de los parámetros
                //ReportViewer1.LocalReport.SetParameters(parameters);    //Pasamos el array de los parámetros al ReportViewer

                ReportViewer1.LocalReport.Refresh();    //Actualizamos el report
                ReportViewer1.Visible = true;
            }
            catch (Exception ex)
            {
                //Variables.Error = ex.Message;
                //TextAlerta.Text = ex.Message;
                //alerta.Visible = true;
                Lbmensaje.Text = ex.Message;
                Asume.Visible = true;
                Modifica.Visible = false;
                cuestion.Visible = false;
                Decide.Visible = false;
                //btnasume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();

                //BtnAcepta.Visible = false;
                //BTnNoAcepta.Visible = false;
            }
            //PNreportLista.Visible = true;
            //PNFiltrosLista.Visible = false;
            //PNGridLista.Visible = false;
        }

        protected void DrOrden1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void DrOrden2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}
        //protected void DrOrden3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}
        //protected void DrOrden4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}
        //protected void DrOrden5_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}
        //protected void DrOrden6_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}

        //protected void DrOrden7_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    OrdenCabecera();
        //}
        protected void DrControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tabla();
        }
        //protected void DrControl2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}
        //protected void DrControl3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}
        //protected void DrControl4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}
        //protected void DrControl5_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}
        //protected void DrControl6_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}
        //protected void DrControl7_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tabla();
        //}

        protected void DrLista1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tablaLista();
        }
        //protected void DrLista2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}
        //protected void DrLista3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}
        //protected void DrLista4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}
        //protected void DrLista5_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}
        //protected void DrLista6_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}
        //protected void DrLista7_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Carga_tablaLista();
        //}

        //private void OrdenLista()
        //{
        //    ElOrdenLista = "";
        //    DrLista1.BackColor = Color.FromName("#ffffff");
        //    DrLista2.BackColor = Color.FromName("#ffffff");
        //    DrLista3.BackColor = Color.FromName("#ffffff");
        //    DrLista4.BackColor = Color.FromName("#ffffff");
        //    DrLista5.BackColor = Color.FromName("#ffffff");
        //    DrLista6.BackColor = Color.FromName("#ffffff");
        //    DrLista7.BackColor = Color.FromName("#ffffff");


        //    if (DrLista1.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista1.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista = " ORDER BY " + DrLista1.SelectedItem.Text;
        //        //    }

        //        //}
        //        CompruebaCampoDecimalLista(DrLista1.SelectedItem.Text);
        //        DrLista1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista2.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista2.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista2.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista2.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista2.SelectedItem.Text);
        //        DrLista2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista3.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista3.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista3.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista3.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista3.SelectedItem.Text);
        //        DrLista3.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista4.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista4.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista4.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista4.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista4.SelectedItem.Text);
        //        DrLista4.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista5.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista5.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista5.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista5.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista5.SelectedItem.Text);
        //        DrLista5.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista6.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista6.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista6.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista6.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista6.SelectedItem.Text);
        //        DrLista6.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrLista7.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenLista == "")
        //        //{
        //        //    ElOrdenLista = " ORDER BY " + DrLista7.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenLista.Contains(DrLista7.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenLista += ", " + DrLista7.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimalLista(DrLista7.SelectedItem.Text);
        //        DrLista7.BackColor = Color.FromName("#fcf5d2");
        //    }
        //    //Carga_tablaLista();
        //}

        private void CompruebaCampoDecimal(string combo)
        {
            if (combo == "UDSPEDIDAS" || combo == "UDSSERVIDAS" || combo == "UDSENCARGA" || combo == "UDSPENDIENTES" || combo == "UDSACARGAR" || combo == "NUMPALET")
            {
                if (ElOrdenControl == "")
                {
                    ElOrdenControl = " ORDER BY CONVERT(DECIMAL (10,3), " + combo + ")";
                }
                else
                {
                    ElOrdenControl += ", CONVERT(DECIMAL (10,3), " + combo + ")";
                }
            }
            else
            {
                if (ElOrdenControl == "")
                {
                    ElOrdenControl = " ORDER BY " + combo + " ";
                }
                else
                {
                    if (combo == "NUMERO" || combo == "POSICIONCAMION" || combo == "LINEA" || combo == "NUMERO_LINEA")
                    {
                        ElOrdenControl += ", CONVERT(INT, " + combo + ") ";
                    }
                    else
                    {
                        ElOrdenControl += ", " + combo + " ";
                    }

                }
            }
        }

        private void CompruebaCampoDecimalLista(string combo)
        {
            if (combo == "UDSENCARGA" || combo == "UDSENCARGA")
            {
                if (ElOrdenLista == "")
                {
                    ElOrdenLista = " ORDER BY CONVERT(DECIMAL (10,3), " + combo + ")";
                }
                else
                {
                    ElOrdenLista += ", CONVERT(DECIMAL (10,3), " + combo + ")";
                }
            }
            //else if (combo == "POSICIONCAMION" || combo == "NUMERO" || combo == "LINEA" || combo == "NUMERO_LINEA")
            //{
            //    if (ElOrdenLista == "")
            //    {
            //        ElOrdenLista = " ORDER BY CONVERT(INT, " + combo + ")";
            //    }
            //    else
            //    {
            //        ElOrdenLista += ", CONVERT(INT, " + combo + ")";
            //    }
            //}
            else
            {
                if (ElOrdenLista == "")
                {
                    ElOrdenLista = " ORDER BY " + combo + " ";
                }
                else
                {
                    ElOrdenLista += ", " + combo + " ";
                }
            }
        }

        //private void OrdenControl()
        //{
        //    ElOrdenControl = "";
        //    DrControl1.BackColor = Color.FromName("#ffffff");
        //    DrControl2.BackColor = Color.FromName("#ffffff");
        //    DrControl3.BackColor = Color.FromName("#ffffff");
        //    DrControl4.BackColor = Color.FromName("#ffffff");
        //    DrControl5.BackColor = Color.FromName("#ffffff");
        //    DrControl6.BackColor = Color.FromName("#ffffff");
        //    DrControl7.BackColor = Color.FromName("#ffffff");


        //    if (DrControl1.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    //if (ElOrdenControl.Contains(DrControl1.SelectedItem.Text))
        //        //    //{ }
        //        //    //else
        //        //    //{
        //        //    //    ElOrdenControl = " ORDER BY " + DrControl1.SelectedItem.Text;
        //        //    //}
        //        //}
        //        CompruebaCampoDecimal(DrControl1.SelectedItem.Text);
        //        DrControl1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl2.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl2.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl2.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl2.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl2.SelectedItem.Text);
        //        DrControl2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl3.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl3.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl3.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl3.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl3.SelectedItem.Text);
        //        DrControl3.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl4.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl4.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl4.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl4.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl4.SelectedItem.Text);
        //        DrControl4.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl5.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl5.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl5.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl5.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl5.SelectedItem.Text);
        //        DrControl5.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl6.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl6.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl6.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl6.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl6.SelectedItem.Text);
        //        DrControl6.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrControl7.SelectedItem.Value != "Ninguno")
        //    {
        //        //if (ElOrdenControl == "")
        //        //{
        //        //    ElOrdenControl = " ORDER BY " + DrControl7.SelectedItem.Text;
        //        //}
        //        //else
        //        //{
        //        //    if (ElOrdenControl.Contains(DrControl7.SelectedItem.Text))
        //        //    { }
        //        //    else
        //        //    {
        //        //        ElOrdenControl += ", " + DrControl7.SelectedItem.Text;
        //        //    }
        //        //}
        //        CompruebaCampoDecimal(DrControl7.SelectedItem.Text);
        //        DrControl7.BackColor = Color.FromName("#fcf5d2");
        //    }
        //    //Carga_tabla();
        //}

        protected void DrTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtTransportista.Text = DrTransportista.SelectedItem.Text;
        }
        protected void gvCabecera_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
        }

        protected void GvBandeja_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvCabecera.PageIndex = e.NewPageIndex;

            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();
        }

        protected void gvControl_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvControl.PageIndex = e.NewPageIndex;
            Carga_tabla();
        }

        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            Carga_tablaLista();
        }

        protected void gvControl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();
            gvControl.EditIndex = -1;
            Carga_tabla();
            gvControl.DataBind();
            this.Session["GridEdicion"] = "0";


            //decimal NUMPALET = 1.0M;
            //decimal UNIDADES = 1.0M;
            //decimal REPARTO = 1.0M;
            //string Cabecera = "";
            //string SQL = "";

            //TextBox txtBox = (TextBox)(row.Cells[13].Controls[0]);
            //if (txtBox != null)
            //{
            //    UNIDADES = Convert.ToDecimal(txtBox.Text);
            //}

            //txtBox = (TextBox)(row.Cells[14].Controls[0]);
            //if (txtBox != null)
            //{
            //    NUMPALET = Convert.ToDecimal(txtBox.Text);
            //}

            //txtBox = (TextBox)(row.Cells[15].Controls[0]);
            //if (txtBox != null)
            //{
            //    Cabecera = txtBox.Text;
            //}

            //REPARTO = (UNIDADES / NUMPALET);

            //decimal Unidad = 1.00M;
            //int Linea = 0;
            //int N = 0;
            //Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            //if (Con is System.DBNull)
            //{
            //    N = 1;
            //}
            //else
            //{
            //    N = Convert.ToInt32(Con) + 1;
            //}
            //Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            //if (Con is System.DBNull)
            //{
            //    Linea = 1;
            //}
            //else
            //{
            //    Linea = Convert.ToInt32(Con) + 1;
            //}

            //while (NUMPALET >= 1)
            //{
            //    SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
            //    SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA )";
            //    SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
            //    SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA ";
            //    SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

            //    DBHelper.ExecuteNonQuery(SQL);

            //    //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            //    //DBHelper.ExecuteNonQuery(SQL);
            //    NUMPALET = NUMPALET - Unidad;
            //    N += 1;
            //    Linea += 1;
            //}
            //if (NUMPALET > 0)
            //{
            //    SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
            //    SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA )";
            //    SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
            //    SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA ";
            //    SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

            //    DBHelper.ExecuteNonQuery(SQL);

            //    //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            //    //DBHelper.ExecuteNonQuery(SQL);
            //    NUMPALET = NUMPALET - Unidad;
            //}

            //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            //DBHelper.ExecuteNonQuery(SQL);

            //this.Session["NumeroPalet"] = Linea.ToString();

            //Carga_tabla();
            //Carga_tablaLista();

            //gvControl.EditIndex = -1;

            ////DataTable dt = this.Session["MiConsulta"] as DataTable;
            ////gvControl.DataSource = dt;
            //gvControl.DataBind();



            ////DataTable dt = this.Session["MiConsulta"] as DataTable;
            ////gvControl.DataSource = dt;
            ////gvControl.DataBind();
        }
        protected void GvBandeja_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void gvCabecera_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridViewRow row = gvCabecera.Rows[e.RowIndex];
            string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();
            gvCabecera.EditIndex = -1;

            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera();
            }
            else
            {
                Carga_tablaCabeceraClose();
            }
            //Carga_tablaCabecera();
            gvCabecera.DataBind();
            this.Session["GridEdicion"] = "0";
            //this.Session["IDCabecera"] = miro;
            //Carga_tablaCabecera();
            //gvCabecera.EditIndex = -1;

            //gvCabecera.DataBind();

            //string SQL = "SELECT NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA, TRANSPORTISTA, OBSERVACIONES FROM  ZCARGA_CABECERA WHERE ID_SECUENCIA = " + miro;
            //DataTable dt = Main.BuscaLote(SQL).Tables[0];

            //foreach (DataRow filas in dt.Rows)
            //{
            //    TxtNumero.Text = filas["NUMERO"].ToString();
            //    int ps = DrEmpresa.Items.IndexOf(DrEmpresa.Items.FindByText(filas["EMPRESA"].ToString()));
            //    DrEmpresa.SelectedIndex = ps;
            //    //DrEmpresa.Text = filas["EMPRESA"].ToString();
            //    //DrPais.Text = filas["PAIS"].ToString();
            //    ps = DrPais.Items.IndexOf(DrPais.Items.FindByText(filas["PAIS"].ToString()));
            //    DrPais.SelectedIndex = ps;
            //    TxtFecha.Text = filas["FECHACARGA"].ToString();
            //    TxtTelefono.Text = filas["TELEFONO"].ToString();
            //    TxtMatricula.Text = filas["MATRICULA"].ToString();
            //    TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();

            //    TxtTransportista.Text = filas["TRANSPORTISTA"].ToString();
            //    TxtPais.Text = filas["PAIS"].ToString();
            //    TxtEmpresa.Text = filas["EMPRESA"].ToString();

            //    ps = DrTransportista.Items.IndexOf(DrTransportista.Items.FindByText(filas["TRANSPORTISTA"].ToString()));
            //    DrTransportista.SelectedIndex = ps;

            //    TxtNumero.Enabled = false;
            //    DrEmpresa.Enabled = false;
            //    DrPais.Enabled = false;
            //    TxtFecha.Enabled = false;
            //    TxtTelefono.Enabled = false;
            //    TxtMatricula.Enabled = false;
            //    TxtObservaciones.Enabled = false;
            //    TxtTransportista.Enabled = false;
            //    TxtPais.Enabled = false;
            //    TxtEmpresa.Enabled = false;
            //    DrTransportista.Enabled = false;

            //    SeleccionCabecera();

            //    //LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + DrEmpresa.SelectedItem.Value + ", Pais: " + DrPais.SelectedItem.Value + ", Fecha: " + TxtFecha.Text;
            //    //LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + DrTransportista.SelectedItem.Value + ")";
            //    //BtnuevaCabecera.Text = "Cancelar";
            //    HtmlButton btn = (HtmlButton)FindControl("BtnuevaCabecera");
            //    HtmlButton li = (HtmlButton)FindControl("BtCancelCabecera");
            //    btn.Visible = false;
            //    li.Visible = true;
            //    //BtnuevaCabecera.Attributes["class"] = "btn btn-warning  btn-block";
            //    break;
            //}

            ////Carga_tablaCabecera();
            ////Carga_tabla();
            ////Carga_tablaLista();

            //SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            //SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + miro;

            //dt = Main.BuscaLote(SQL).Tables[0];
            //gvLista.DataSource = dt;
            //gvLista.DataBind();
            //this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
            //CreaPalets(dt);

            //gvCabecera.EditIndex = -1;

            //gvCabecera.DataBind();
        }

        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

            Carga_tablaLista();
            gvLista.EditIndex = -1;
            gvLista.DataBind();
            this.Session["GridEdicion"] = "0";

            ////sube la linea a la orden
            //string Numero = "";
            //decimal UNIDADES = 1.0M;
            ////string SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + this.Session["IDGridB"].ToString();

            //TextBox txtBox = (TextBox)(row.Cells[10].Controls[0]);
            //if (txtBox != null)
            //{
            //    UNIDADES = Convert.ToDecimal(txtBox.Text);
            //}
            //txtBox = (TextBox)(row.Cells[13].Controls[0]);
            //if (txtBox != null)
            //{
            //    Numero = txtBox.Text;
            //}
            ////DBHelper.ExecuteNonQuery(SQL);

            //string SQL = "UPDATE ZCARGA_ORDEN SET (UNIDADESENCARGA = UNIDADESENCARGA + UNIDADES) WHERE ID_CABECERA = " + miro;
            //DBHelper.ExecuteNonQuery(SQL);

            //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID_SECUENCIA = " + miro + " AND NUMERO_LINEA = " + Numero;

            //Carga_tabla();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
        }

        protected void TabOCajas_TextChanged(object sender, EventArgs e)
        {
            //calculo palets y plantas
            this.Session["CalculoPaletPlanta"] = "TabOCajas";
        }

        private void CalculoPorPlantas()
        {
            //Calculo bandejas segun tabla bandejas
            //calculo Palet segun que bandejas entran en palet
        }

        private void CalculoPorPalets()
        {
            //Calculo plantas segun tabla bandejas
            //calculo cajas segun que bandejas entran en caja
        }

        private void CalculoPorCajas()
        {
            //Calculo plantas segun tabla bandejas
            //calculo Palet segun que Cajas entran en palet
        }


        protected void gvControl_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string a = e.SortExpression;
            this.Session["GridOrden"] = e.SortExpression;

            Carga_tabla(e.SortExpression);
        }

        protected void gvControl_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            if (TxtNumero.Text == "")
            {
                //TextAlerta.Text = "Debe crear o editar previamente una órden de Cabecera para tratar con la lista de pedidos";
                //alerta.Visible = true;
                Lbmensaje.Text = "Debe crear o editar previamente una órden de Cabecera para tratar con la lista de pedidos";
                Asume.Visible = true;
                Modifica.Visible = false;
                cuestion.Visible = false;
                Decide.Visible = false;
                //btnasume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();

                //BtnAcepta.Visible = false;
                //BTnNoAcepta.Visible = false;
                return;
            }
            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

            //UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES THEN(CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA))

            decimal rUDSPEDIDAS = 1.0M;
            decimal rUDSSERVIDAS = 1.0M;
            decimal rUDSENCARGA = 1.0M;

            decimal rUDSPENDIENTES = 1.0M;
            decimal rUDSACARGAR = 1.0M; ;
            decimal rNUMPALET = 1.0M;
            decimal rCAJAS = 1.0M;
            decimal rUNIDADES = 1.0M;
            //string rID_CABECERA = "";
            //string rID = "";
            //string rSERIE_PEDIDO = "";
            //string Mira = "";

            try
            {
                //Mira = Server.HtmlDecode(row.Cells[10].Text);
                //if (Mira != "")
                //{
                //    rUDSPEDIDAS = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //Mira = Server.HtmlDecode(row.Cells[11].Text);
                //if (Mira != "")
                //{
                //    rUDSSERVIDAS = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //Mira = Server.HtmlDecode(row.Cells[12].Text);
                //if (Mira != "")
                //{
                //    rUDSENCARGA = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //Mira = Server.HtmlDecode(row.Cells[13].Text);
                //if (Mira != "")
                //{
                //    rUDSPENDIENTES = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //Mira = Server.HtmlDecode(row.Cells[14].Text);
                //if (Mira != "")
                //{
                //    rUDSACARGAR = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //Mira = Server.HtmlDecode(row.Cells[15].Text);
                //if (Mira != "")
                //{
                //    rNUMPALET = Convert.ToDecimal(Mira.Replace(".", ","));
                //}
                //test
                //TextBox txtValor = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsPedidas") as TextBox);
                //miro = txtValor.Text;

                TextBox txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsPedidas") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsServidas") as TextBox);
                //txtBox = (TextBox)(row.Cells[11].Controls[0]);
                if (txtBox != null)
                {
                    rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[12].Controls[0]);
                if (txtBox != null)
                {
                    rUDSENCARGA = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPendientes") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rUDSPENDIENTES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }

                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCargar") as TextBox);
                if (txtBox != null)
                {
                    rUDSACARGAR = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }

                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOTipo") as TextBox);
                if (txtBox != null)
                {
                    if (txtBox.Text != "") // txtBox.Text && Esta == false)
                    {
                        rUNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    else
                    {
                        string AA = "0.00";
                        rUNIDADES = Convert.ToDecimal(AA.Replace(".", ","));
                    }
                }

                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
                if (txtBox != null)
                {
                    rNUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }


                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCajas") as TextBox);
                if (txtBox != null)
                {
                    if (txtBox.Text != "") // txtBox.Text && Esta == false)
                    {
                        if (Convert.ToInt32(rUNIDADES) > 0)
                        {
                            rCAJAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                            if (rNUMPALET > 0)
                            {
                                rUDSACARGAR = ((rCAJAS * rUNIDADES) * rNUMPALET) / 1000;
                            }
                            else
                            {
                                rUDSACARGAR = (rCAJAS * rUNIDADES) / 1000;
                            }
                        }
                    }
                }

                //txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
                ////txtBox = (TextBox)(row.Cells[15].Controls[0]);
                //if (txtBox != null)
                //{
                //    rNUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                //}
                //txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOSerie") as TextBox);
                ////txtBox = (TextBox)(row.Cells[15].Controls[0]);
                //if (txtBox != null)
                //{
                //    rSERIE_PEDIDO =txtBox.Text;
                //}



                // rUDSACARGAR + rUDSSERVIDAS <= UDSPEDIDAS
                if ((rUDSACARGAR + rUDSSERVIDAS) <= rUDSPEDIDAS)
                {
                    //rUDSSERVIDAS += rUDSACARGAR; 
                    //rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSACARGAR);
                    rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSACARGAR);
                }
                else
                {
                    //Variables.Error = "No se pueden cargar más unidades de las que quedan pendientes.";
                    //TextAlerta.Text = Variables.Error;
                    Lbmensaje.Text = "No se pueden cargar más unidades de las que quedan pendientes.";
                    //alerta.Visible = true;
                    Asume.Visible = true;
                    Modifica.Visible = false;
                    cuestion.Visible = false;
                    Decide.Visible = false;
                    //btnasume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    //BtnAcepta.Visible = false;
                    //BTnNoAcepta.Visible = false;
                    return;
                }
                //int a = container2.Controls.Count;


                string SQL = " SELECT SUM(CONVERT(DECIMAL(18,3), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND A.ID = B.ID ";
                SQL += " AND A.ID = " + miro;
                SQL += " GROUP BY A.ID_CABECERA ";
                Variables.Error = "";
                Lberror.Text = SQL;


                Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
                rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));// + rUDSACARGAR;
                Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;

                //DBHelper.ExecuteNonQuery(SQL);

                SQL = "UPDATE ZCARGA_ORDEN set UDSPENDIENTES = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                SQL += " UDSACARGAR = " + rUDSACARGAR.ToString().Replace(",", ".") + ", ";
                SQL += " UDSENCARGA = " + rUDSENCARGA.ToString().Replace(",", ".") + ", ";
                SQL += " NUMPALET = " + rNUMPALET.ToString().Replace(",", ".") + ", ";
                SQL += " ID_CABECERA = " + TxtNumero.Text + ", ";
                SQL += " CAJAS = '" + rUNIDADES + "', ";
                //SQL += " SERIE_PED = '" + rSERIE_PEDIDO + "', ";               
                SQL += " ESTADO = 1 ";
                SQL += " WHERE ID = " + miro;

                Variables.Error = "";
                Lberror.Text = SQL;

                Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;




                Carga_tabla();

                gvControl.EditIndex = -1;
                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvControl.DataSource = dt;
                gvControl.DataBind();
            }
            catch (Exception ex)
            {
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void GvBandeja_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void gvCabecera_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvCabecera.Rows[e.RowIndex];
            string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();

            string rID = ""; // gvCabecera.DataKeyNames[e.RowIndex].ToString();

            string rNUMERO = "";
            string rEMPRESA = "";
            string rPAIS = "";
            string rFECHAPREPARA = "";
            string rFECHACARGA = "";
            string rTELEFONO = "";
            string rMATRICULA = "";
            string rTRANSPORTISTA = "";
            string rTELEFONO_USER = "";
            //string rLATITUD = "";
            //string rLONGITUD = "";
            string rOBSERVACIONES = "";
            string rESTADO = "";
            string MiESTADO = "";
            //string Mira = "";

            try
            {
                //Mira = Server.HtmlDecode(row.Cells[3].Text);
                //if (Mira != "")
                //{
                //    rID = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[3].Text);
                //if (Mira != "")
                //{
                //    rNUMERO = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[4].Text);
                //if (Mira != "")
                //{
                //    rEMPRESA = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[5].Text);
                //if (Mira != "")
                //{
                //    rPAIS = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[6].Text);
                //if (Mira != "")
                //{
                //    rFECHACARGA = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[7].Text);
                //if (Mira != "")
                //{
                //    rTELEFONO = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[8].Text);
                //if (Mira != "")
                //{
                //    rMATRICULA = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[9].Text);
                //if (Mira != "")
                //{
                //    rTRANSPORTISTA = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[10].Text);
                //if (Mira != "")
                //{
                //    rTELEFONO_USER = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[11].Text);
                //if (Mira != "")
                //{
                //    rLATITUD = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[12].Text);
                //if (Mira != "")
                //{
                //    rLONGITUD = Mira.Replace(".", ",");
                //}
                //Mira = Server.HtmlDecode(row.Cells[13].Text);
                //if (Mira != "")
                //{
                //    rOBSERVACIONES = Mira.Replace(".", ",");
                //}
                TextBox txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rFECHAPREPARA = txtBox.Text;
                }
                //txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[11].Controls[0]);
                //if (txtBox != null)
                //{
                //    rLATITUD = txtBox.Text;
                //}
                //txtBox = (TextBox)(row.Cells[12].Controls[0]);
                //if (txtBox != null)
                //{
                //    rLONGITUD = txtBox.Text;
                //}
                txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACIONES = txtBox.Text;
                }

                Label box = (gvCabecera.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    MiESTADO = box.Text;
                }



                DropDownList Etiqueta = row.FindControl("drDescripcionEstado") as DropDownList;
                rESTADO = Etiqueta.SelectedValue;



                string SQL = "UPDATE ZCARGA_CABECERA set NUMERO = " + rNUMERO + ", ";
                SQL += " EMPRESA = '" + rEMPRESA + "', ";
                SQL += " PAIS = '" + rPAIS + "', ";
                SQL += " FECHAPREPARACION = '" + rFECHAPREPARA + "', ";
                SQL += " FECHACARGA = '" + rFECHACARGA + "', ";
                SQL += " TELEFONO = '" + rTELEFONO + "', ";
                SQL += " MATRICULA = '" + rMATRICULA + "', ";
                SQL += " TRANSPORTISTA = '" + rTRANSPORTISTA + "', ";
                SQL += " TELEFONO_USER = '" + rTELEFONO_USER + "', ";
                //SQL += " LATITUD = '" + rLATITUD + "', ";
                //SQL += " LONGITUD = '" + rLONGITUD + "', ";
                SQL += "ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', ";
                SQL += " OBSERVACIONES = '" + rOBSERVACIONES + "', ";

                if (MiESTADO == "Confirmado")//Confirmado
                {
                    if (rESTADO == "3")
                    {
                        SQL += " ESTADO = " + rESTADO;
                    }
                    else
                    {
                        SQL += " ESTADO = 4 ";
                    }
                }
                else
                {
                    SQL += " ESTADO = " + rESTADO;
                }
                SQL += " WHERE ID = " + miro;

                this.Session["EstadoCabecera"] = rESTADO;

                Variables.Error = "";
                Lberror.Text = SQL;
                DBHelper.ExecuteNonQuery(SQL);


                //if (CaCheck.Checked == false)
                //{
                if (this.Session["Cerrados"].ToString() == "0")
                {
                    Carga_tablaCabecera(this.Session["GridOrden"].ToString());
                }
                else
                {
                    Carga_tablaCabeceraClose(this.Session["GridOrden"].ToString());
                }
                //Carga_tablaCabecera();

                gvCabecera.EditIndex = -1;

                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvControl.DataSource = dt;
                gvCabecera.DataBind();





                if (rESTADO == "3")
                {
                    SQL = "UPDATE ZCARGA_LINEA set ESTADO = " + rESTADO + " ";
                    SQL += " WHERE ID_CABECERA = " + rID;

                    Carga_tablaLista();
                    gvLista.DataBind();
                }
            }
            catch (Exception ex)
            {
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            string a = e.SortExpression;
            this.Session["GridOrden"] = e.SortExpression;
            Carga_tablaLista(e.SortExpression);
        }


        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

            string rNumPalet = "";
            string rCantidad = "";
            string MiPOSICION = "";
            string rPOSICION = "";
            string rOBSERVACION = "";
            string rNUMEROLINEA = "";
            string rCABECERA = "";
            //string Mira = "";
            try
            {
                //Mira = Server.HtmlDecode(row.Cells[4].Text);
                //if (Mira != "")
                //{
                //    rNUMEROLINEA = Mira.Replace(".", ",");
                //}

                //Mira = Server.HtmlDecode(row.Cells[12].Text);
                //if (Mira != "")
                //{
                //    if (Mira != "")
                //    {
                //        rCantidad = Mira;
                //    }
                //    else
                //    {
                //        rCantidad = "0";
                //    }
                //}
                //Mira = Server.HtmlDecode(row.Cells[13].Text);
                //if (Mira != "")
                //{
                //    if (Mira != "")
                //    {
                //        rNumPalet = Mira;
                //    }
                //    else
                //    {
                //        rNumPalet = "0";
                //    }
                //}
                //Mira = Server.HtmlDecode(row.Cells[14].Text);
                //if (Mira != "")
                //{
                //    if (Mira != "")
                //    {
                //        rPOSICION = Mira;
                //    }
                //    else
                //    {
                //        rPOSICION = "0";
                //    }
                //}
                //Mira = Server.HtmlDecode(row.Cells[15].Text);
                //if (Mira != "")
                //{
                //    rOBSERVACION = Mira.Replace(".", ",");
                //}

                TextBox txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLCarga") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rCantidad = txtBox.Text;
                    }
                    else
                    {
                        rCantidad = "0";
                    }
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLPalet") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rNumPalet = txtBox.Text;
                    }
                    else
                    {
                        rNumPalet = "0";
                    }
                }

                Label LabelBox = (gvLista.Rows[Indice].Cells[14].FindControl("LabLcamion") as Label);
                //txtBox = (TextBox)(row.Cells[14].Controls[0]);
                if (LabelBox != null)
                {
                    if (LabelBox.Text != "")
                    {
                        MiPOSICION = LabelBox.Text;
                    }
                    else
                    {
                        MiPOSICION = "0";
                    }
                }

                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLcamion") as TextBox);
                //txtBox = (TextBox)(row.Cells[14].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rPOSICION = txtBox.Text;
                    }
                    else
                    {
                        rPOSICION = "0";
                    }
                    this.Session["ModificaLinea"] = rPOSICION;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[15].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACION = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLNumLinea") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rNUMEROLINEA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLCabecera") as TextBox);
                //txtBox = (TextBox)(row.Cells[2].Controls[0]);
                if (txtBox != null)
                {
                    rCABECERA = txtBox.Text;
                }

                //SELECT PARA BUSCAR LA POSICION ANTERIOR A CAMBIAR DEL CAMION
                //string SQL = "SELECT POSICIONCAMION FROM ZCARGA_LINEA ";
                //SQL += " WHERE ID = " + miro;
                //SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                //SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                //int N = 0;
                //Object Con = DBHelper.ExecuteScalarSQL(SQL, null); //ID =" + miro + " AND
                //if (Con is System.DBNull)
                //{
                //    N = 1;
                //}
                //else
                //{
                //    N = Convert.ToInt32(Con) ;
                //}

                //SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = '" + N + "' ";
                //SQL += " WHERE POSICIONCAMION = " + rPOSICION;

                //DBHelper.ExecuteNonQuery(SQL);

                //Problema del retorno de carro para Codigo QR si quito /r/n no se lee

                string SQL = "SELECT POSICIONCAMION ";
                SQL += " FROM ZCARGA_LINEA ";
                SQL += " WHERE ID = " + miro;
                SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                Object Con = DBHelper.ExecuteScalarSQL(SQL, null);

                if (Con is null)
                {
                    MiPOSICION = "0";
                    this.Session["ElIDaBorrar"] = "0" + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;
                }
                else
                {
                    // MiPosicion, Lanuevaposicion, ID, ID_CABECERA, NUMERO_LINEA
                    MiPOSICION = Con.ToString();
                    this.Session["ElIDaBorrar"] = Con + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;
                }


                SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = " + rPOSICION + ", ";
                //string SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = " + rPOSICION.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ", ";
                SQL += " OBSERVACIONES = '" + rOBSERVACION + "', ";
                SQL += " NUMPALET = " + rNumPalet.Replace(",", ".") + ", ";
                SQL += " UDSENCARGA = " + rCantidad.Replace(",", ".") + ", ";
                //SQL += " ZSYSDATE = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', ";
                //SQL += " HASTA =  CONVERT(VARCHAR (255), ID_CABECERA) + '|' +  CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + rPOSICION.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + "', ";
                SQL += " HASTA =  CONVERT(VARCHAR (255), ID_CABECERA) + '|' +  CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + rPOSICION + "', ";
                SQL += " ESTADO = 1 ";
                SQL += " WHERE ID = " + miro;
                SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                Lberror.Text += SQL + "1- gvLista_RowUpdating " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 1- gvLista_RowUpdating " + Variables.mensajeserver;

                if (MiPOSICION != rPOSICION)
                {
                    if (Convert.ToInt32(MiPOSICION) > Convert.ToInt32(rPOSICION))
                    {
                        Lbmensaje.Text = "La posición del Camión ha cambiado a una posición menor.";
                    }
                    else
                    {
                        Lbmensaje.Text = "La posición del Camión ha cambiado a una posición mayor.";
                    }
                    Lbmensaje.Text += " ¿Desea desplazar los palets de posiciones intermedias?";
                    this.Session["SelectLinea"] = SQL;
                    Modifica.Visible = true;
                    Asume.Visible = false;
                    cuestion.Visible = false;
                    Decide.Visible = false;
                    windowmessaje.Visible = true;
                    gvLista.EditIndex = -1;
                    MiCloseMenu();

                    return;
                }
                else
                {
                    Variables.Error = "";

                    if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    else
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    DBHelper.ExecuteNonQuery(SQL);

                    Carga_tablaCabecera(this.Session["GridOrden"].ToString());
                    Carga_tablaLista();

                    gvLista.EditIndex = -1;

                    //DataTable dt = this.Session["MiConsulta"] as DataTable;
                    //gvControl.DataSource = dt;
                    gvLista.DataBind();
                }
            }
            catch (Exception ex)
            {
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }


        protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvControl.EditIndex = e.NewEditIndex;
            this.Session["GridEdicion"] = "1";

            Carga_tabla(this.Session["GridOrden"].ToString());
            //string carga = gvControl.Width.ToString();
            //int cuantos = gvControl.Rows[indice].Cells.Count - 2;
            //int Parcial = Convert.ToInt32(carga) - cuantos;

            //gvControl.Rows[indice].Cells[0].Enabled = false;
            gvControl.Rows[indice].Cells[1].Enabled = false;
            gvControl.Rows[indice].Cells[2].Enabled = false;
            gvControl.Rows[indice].Cells[3].Enabled = false;
            gvControl.Rows[indice].Cells[4].Enabled = false;
            gvControl.Rows[indice].Cells[5].Enabled = false;
            gvControl.Rows[indice].Cells[6].Enabled = false;
            gvControl.Rows[indice].Cells[7].Enabled = false;
            gvControl.Rows[indice].Cells[8].Enabled = false;
            gvControl.Rows[indice].Cells[9].Enabled = false;
            gvControl.Rows[indice].Cells[10].Enabled = false;
            gvControl.Rows[indice].Cells[11].Enabled = false;
            gvControl.Rows[indice].Cells[12].Enabled = false;
            gvControl.Rows[indice].Cells[13].Enabled = false;
            gvControl.Rows[indice].Cells[17].Enabled = false;
            gvControl.Rows[indice].Cells[18].Enabled = false;

            //gvControl.Rows[indice].Cells[1].Width = 40;
            //gvControl.Rows[indice].Cells[2].Width = 40;
            //gvControl.Rows[indice].Cells[3].Width = 40;
            //gvControl.Rows[indice].Cells[4].Width = 40;
            //gvControl.Rows[indice].Cells[5].Width = 40;
            //gvControl.Rows[indice].Cells[6].Width = 40;
            //gvControl.Rows[indice].Cells[7].Width = 40;
            //gvControl.Rows[indice].Cells[8].Width = 40;
            //gvControl.Rows[indice].Cells[9].Width = 40;
            //gvControl.Rows[indice].Cells[10].Width = 40;
            //gvControl.Rows[indice].Cells[11].Width = 40;
            //gvControl.Rows[indice].Cells[12].Width = 40;
            //gvControl.Rows[indice].Cells[13].Width = 40;
            //gvControl.Rows[indice].Cells[14].Width = 40;
            //gvControl.Rows[indice].Cells[15].Width = 40;
            //gvControl.Rows[indice].Cells[16].Width = 40;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();

            GridViewRow row = gvControl.Rows[indice];
            row.BackColor = Color.FromName("#ffead1");

        }
        protected void GvBandeja_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void gvCabecera_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvCabecera.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvCabecera.EditIndex = e.NewEditIndex;
            string rID = "";
            this.Session["GridEdicion"] = "1";
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera(this.Session["GridOrden"].ToString());
            }
            else
            {
                Carga_tablaCabeceraClose(this.Session["GridOrden"].ToString());
            }
            //Carga_tablaCabecera();


            Label txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            if (txtBox != null)
            {
                rID = txtBox.Text;
            }
            DropDownList combo = gvCabecera.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
            if (combo != null)
            {
                for (int i = 0; i < combo.Items.Count; i++)
                {
                    if (combo.Items[i].Text == rID)
                    {
                        combo.SelectedValue = combo.Items[i].Value;
                        break;
                    }
                }
            }

            GridViewRow row = gvCabecera.Rows[indice];
            row.BackColor = Color.FromName("#ffead1");
            gvCabecera.Rows[indice].Cells[13].Enabled = false;


            //Si esta cerrado le envio a uno normal
            //Carga_tablaCabecera_close();
            //Carga_tablaCabecera();
            //gvControl.Rows[indice].Cells[0].Enabled = false;
            //gvControl.Rows[indice].Cells[1].Enabled = false;
            //gvControl.Rows[indice].Cells[2].Enabled = false;
            //gvControl.Rows[indice].Cells[3].Enabled = false;
            //gvControl.Rows[indice].Cells[4].Enabled = false;
            //gvControl.Rows[indice].Cells[5].Enabled = false;
            //gvControl.Rows[indice].Cells[6].Enabled = false;
            //gvControl.Rows[indice].Cells[7].Enabled = false;
            //gvControl.Rows[indice].Cells[8].Enabled = false;
            //gvControl.Rows[indice].Cells[9].Enabled = false;
            //gvControl.Rows[indice].Cells[10].Enabled = false;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();
        }

        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvLista.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //gvLista.AutoResizeColumns();
            //gvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //gvLista.AutoResizeColumns(DataGridViewAutoSizeColumnsMo‌​de.Fill);
            int indice = gvLista.EditIndex = e.NewEditIndex;
            //int i = gvLista.Rows[indice].Cells.Count;

            //for (int i = 0; i < gvLista.Columns.Count; i++)
            //{
            //    gvLista.Columns[i].ItemStyle.Width = 10;
            //}
            this.Session["GridEdicion"] = "1";

            Carga_tablaLista();
            //gvControl.Rows[indice].Cells[0].Enabled = false;
            //gvLista.Rows[indice].Cells[1].Enabled = false;
            gvLista.Rows[indice].Cells[2].Enabled = false;
            gvLista.Rows[indice].Cells[3].Enabled = false;
            gvLista.Rows[indice].Cells[4].Enabled = false;
            gvLista.Rows[indice].Cells[5].Enabled = false;
            gvLista.Rows[indice].Cells[6].Enabled = false;
            gvLista.Rows[indice].Cells[7].Enabled = false;
            gvLista.Rows[indice].Cells[8].Enabled = false;
            gvLista.Rows[indice].Cells[9].Enabled = false;
            gvLista.Rows[indice].Cells[10].Enabled = false;
            gvLista.Rows[indice].Cells[11].Enabled = false;
            gvLista.Rows[indice].Cells[12].Enabled = false;
            gvLista.Rows[indice].Cells[13].Enabled = false;
            gvLista.Rows[indice].Cells[16].Enabled = false;

            GridViewRow row = gvLista.Rows[indice];
            row.BackColor = Color.FromName("#ffead1");

            //gvLista.Rows[indice].Cells[16].Enabled = false;
            //Carga_tablaLista();
            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();
        }

        protected void gvControl_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string V = this.Session["EstadoCabecera"].ToString();

            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();
                    GridViewRow row = gvControl.Rows[index];
                    row.BackColor = Color.FromName("#ffead1");
                    //Carga_tabla();
                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;
                    //gvLista.DataBind();
                }

                if (e.CommandName == "BajaOrden")
                {
                    if (TxtNumero.Text == "" || TxtNumero.Text == "0")
                    {
                        Lbmensaje.Text = "Orden de Carga sin asignar. Debe seleccionar previamente una Orden de Carga para asignar algún pedido.";
                        Asume.Visible = true;
                        Modifica.Visible = false;

                        cuestion.Visible = false;
                        Decide.Visible = false;
                        //btnasume.Visible = true;
                        windowmessaje.Visible = true;
                        //BtnAcepta.Visible = false;
                        //BTnNoAcepta.Visible = false;
                        MiCloseMenu();

                        return;
                    }
                    //string Mira = "";
                    decimal rUDSPEDIDAS = 1.0M;
                    decimal rUDSSERVIDAS = 1.0M;

                    decimal NUMPALET = 1.0M;
                    decimal UNIDADES = 1.0M;
                    decimal REPARTO = 1.0M;
                    decimal PARCIAL = 0;
                    string Cabecera = TxtNumero.Text;
                    string SQL = "";
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvControl.Rows[index];
                    string miro = gvControl.DataKeys[index].Value.ToString();

                    row.BackColor = Color.FromName("#ffead1");

                    Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvControl.Rows[Indice].Cells[11].FindControl("LabOUdsServidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvControl.Rows[Indice].Cells[14].FindControl("LabOCargar") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        UNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvControl.Rows[Indice].Cells[15].FindControl("LabOPalet") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        NUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }


                    //Mira = Server.HtmlDecode(row.Cells[10].Text);
                    //if (Mira != "")
                    //{
                    //    rUDSPEDIDAS = Convert.ToDecimal(Mira.Replace(".",",")) ;
                    //}
                    //Mira = Server.HtmlDecode(row.Cells[11].Text);
                    //if (Mira != "")
                    //{
                    //    rUDSSERVIDAS = Convert.ToDecimal(Mira.Replace(".", ","));
                    //}


                    //Mira = Server.HtmlDecode(row.Cells[14].Text);
                    //if(Mira != "")
                    //{
                    //    UNIDADES = Convert.ToDecimal(Mira.Replace(".", ","));
                    //}
                    //Mira = Server.HtmlDecode(row.Cells[15].Text);
                    //if (Mira != "")
                    //{
                    //    NUMPALET = Convert.ToDecimal(Mira.Replace(".", ","));
                    //}
                    //Mira = Server.HtmlDecode(row.Cells[16].Text);
                    //if (Mira != "")
                    //{
                    //    Cabecera = Mira;
                    //}
                    //30.000 

                    string Temporal = NUMPALET.ToString().Replace(".", ",");
                    if (Convert.ToDecimal(Temporal) == 0)
                    {
                        //TextAlerta.Text = "Seleccione un número de palets para asignar";
                        //alerta.Visible = true;

                        Lbmensaje.Text = "Seleccione un número de palets para asignar";
                        Asume.Visible = true;
                        cuestion.Visible = false;
                        Decide.Visible = false;
                        Modifica.Visible = false;
                        //btnasume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                        //BtnAcepta.Visible = false;
                        //BTnNoAcepta.Visible = false;
                        return;
                    }
                    string[] Partes = System.Text.RegularExpressions.Regex.Split(NUMPALET.ToString(), ",");

                    if (Partes[0] != "" && Partes[1] != "")
                    {
                        decimal Parte1 = (UNIDADES * 1000);
                        REPARTO = (Parte1 / NUMPALET);
                        PARCIAL = (REPARTO * Convert.ToDecimal("0," + Partes[1])) / 1000;
                        REPARTO = (REPARTO / 1000);

                        //decimal Parcial = ((UNIDADES * 1000) * NUMPALET) / Convert.ToDecimal("0," + Partes[1]);

                        //decimal Unidades = Convert.ToDecimal(Partes[0]);
                        //decimal resto = Convert.ToDecimal("0," + Partes[1]);
                        //if (resto > 0)
                        //{
                        //    decimal ParteA = (UNIDADES * 1000) * resto;
                        //    int cantidad = Convert.ToInt32(ParteA / NUMPALET);//redondeo a entero
                        //    REPARTO = Convert.ToDecimal(cantidad * 2) /1000;
                        //    decimal Totales = (cantidad * 2) * Unidades;
                        //    PARCIAL = Convert.ToDecimal(Convert.ToInt32((UNIDADES * 1000) - Totales)) /1000;
                        //}
                    }
                    else
                    {
                        //80080 ----- 10,4                       
                        //X    ------ 1    

                        REPARTO = (UNIDADES / NUMPALET);
                    }

                    decimal Unidad = 1.00M;
                    int Linea = 0;
                    int N = 0;
                    Lberror.Text += SQL + "1- gvControl_RowCommand " + Variables.mensajeserver;
                    Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    Lberror.Text += " 1- gvControl_RowCommand " + Variables.mensajeserver;

                    if (Con is System.DBNull)
                    {
                        N = 1;
                    }
                    else
                    {
                        N = Convert.ToInt32(Con) + 1;
                    }
                    Lberror.Text += SQL + "2- gvControl_RowCommand " + Variables.mensajeserver;
                    Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    Lberror.Text += " 2- gvControl_RowCommand " + Variables.mensajeserver;

                    if (Con is System.DBNull)
                    {
                        Linea = 1;
                    }
                    else
                    {
                        Linea = Convert.ToInt32(Con) + 1;
                    }

                    while (NUMPALET >= 1)
                    {
                        SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                        SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED, HASTA  )";
                        SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
                        SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED, ";
                        SQL += "'" + Cabecera + "|' +   CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + N + "' ";
                        //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                        SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                        Lberror.Text += SQL + "3- gvControl_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        Lberror.Text += " 3- gvControl_RowCommand " + Variables.mensajeserver;



                        NUMPALET = NUMPALET - Unidad;
                        N += 1;
                        Linea += 1;
                    }
                    if (NUMPALET > 0)
                    {
                        SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                        SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED, HASTA )";//, ZSYSDATE 
                        SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + PARCIAL.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
                        SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED, ";
                        SQL += "'" + Cabecera + "|' +   CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + N + "' ";
                        //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                        SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                        Lberror.Text += SQL + "4- gvControl_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        Lberror.Text += " 4- gvControl_RowCommand " + Variables.mensajeserver;
                        NUMPALET = NUMPALET - Unidad;
                    }

                    //Calculo de ZCARGA_ORDEN SEGUN ZCARGA_LINEa
                    decimal rUDSENCARGA = 1.0M;
                    decimal rUDSPENDIENTES = 1.0M;

                    //SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                    //SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                    //SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
                    //SQL += " AND A.ID = B.ID ";
                    //SQL += " AND A.ID = " + miro ;
                    //SQL += " GROUP BY A.ID_CABECERA ";

                    SQL = " SELECT SUM(CONVERT(DECIMAL(18,3), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                    SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                    SQL += " WHERE A.ID = B.ID ";
                    SQL += " AND A.NUMERO = B.NUMERO ";
                    SQL += " AND A.LINEA = B.LINEA ";
                    SQL += " AND A.EMPRESA = B.EMPRESA ";
                    SQL += " AND A.ID = " + miro;
                    SQL += " GROUP BY A.ID_CABECERA ";

                    Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));
                    Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;

                    Variables.Error = "";


                    rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSENCARGA);

                    SQL = "UPDATE ZCARGA_ORDEN set UDSPENDIENTES = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                    SQL += " UDSACARGAR = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                    SQL += " UDSENCARGA = " + rUDSENCARGA.ToString().Replace(",", ".") + ", ";
                    SQL += " NUMPALET = 0.00 , ";
                    //SQL += " ID_CABECERA = " + TxtNumero.Text + ", ";
                    SQL += " ESTADO = 1 ";
                    SQL += " WHERE ID = " + miro;

                    Variables.Error = "";
                    Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    DBHelper.ExecuteNonQuery(SQL);
                    Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;


                    if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    else
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    DBHelper.ExecuteNonQuery(SQL);

                    this.Session["NumeroPalet"] = Linea.ToString();

                    Carga_tabla(this.Session["GridOrden"].ToString());
                    Carga_tablaLista(this.Session["GridOrden"].ToString());

                    gvControl.EditIndex = -1;
                    gvControl.DataBind();
                }
                if (e.CommandName == "Cancel")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();
                    string Miro = gvControl.DataKeys[index].Value.ToString();
                    GridViewRow row = gvControl.Rows[index];
                    row.BackColor = Color.FromName("#ffead1");
                    //GridViewRow row = (GridViewRow)gvControl.Rows[e.CommandArgument];
                    //gvControl_BajaOrden(Miro, row);
                }

            }
            catch (Exception ex)
            {
                Lberror.Text = "Control RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvControl.DataKeys[index].Value.ToString() + " " + ex.Message;
                Lberror.Visible = true;
            }
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

        protected void gvCabecera_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvCabecera.Rows[index];
                miro = gvCabecera.DataKeys[index].Value.ToString();



                Label Etiqueta = row.FindControl("lblEstado") as Label;
                if (Etiqueta.Text == "En preparación") { this.Session["EstadoCabecera"] = "1"; }
                if (Etiqueta.Text == "Confirmado") { this.Session["EstadoCabecera"] = "2"; }
                if (Etiqueta.Text == "Cerrado") { this.Session["EstadoCabecera"] = "3"; }

                if (this.Session["EstadoCabecera"].ToString() == "3")
                {
                    if (e.CommandName == "SubeCabecera" || e.CommandName == "Edit" || e.CommandName == "Cancel")
                    {
                        gvCabecera_Selecciona(miro);
                    }
                    row.BackColor = Color.FromName("#ffead1");
                    return;
                }
                row.BackColor = Color.FromName("#ffead1");
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                return;
            }

            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    GridViewRow row = gvCabecera.Rows[index];
                    row.BackColor = Color.FromName("#ffead1");

                    this.Session["IDGridA"] = gvCabecera.DataKeys[index].Value.ToString();
                    //gvLista.EditIndex = -1;
                    //gvLista.DataBind();
                }

                if (e.CommandName == "SubeCabecera")
                {

                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvCabecera.Rows[index];
                    //string Miro = gvCabecera.DataKeys[index].Value.ToString();

                    ////////GridViewRow row = gvCabecera.Rows[index];
                    ////////string miro = gvCabecera.DataKeys[index].Value.ToString();

                    ////////Label Etiqueta = row.FindControl("lblEstado") as Label;
                    ////////if (Etiqueta.Text == "En preparación") { this.Session["EstadoCabecera"] = "1"; }
                    ////////if (Etiqueta.Text == "Confirmado") { this.Session["EstadoCabecera"] = "2"; }
                    ////////if (Etiqueta.Text == "Cerrado") { this.Session["EstadoCabecera"] = "3"; }
                    //index = int.Parse(e.CommandArgument.ToString());
                    //Indice = index;
                    //this.Session["IDGridA"] = gvCabecera.DataKeys[index].Value.ToString();
                    //string Miro =  gvCabecera.DataKeys[index].Value.ToString();



                    //HtmlImage SubeCab = e.CommandSource as HtmlImage;
                    //LinkButton l = e.CommandSource as LinkButton;
                    //GridViewRow row = (l.NamingContainer as GridViewRow);
                    gvCabecera_Selecciona(miro);
                    row.BackColor = Color.FromName("#ffead1");
                }

                if (e.CommandName == "Ubicacion")
                {
                    //index = int.Parse(e.CommandArgument.ToString());
                    //Indice = index;

                    //this.Session["IDGridA"] = gvCabecera.DataKeys[index].Value.ToString();
                    //string Miro = gvCabecera.DataKeys[index].Value.ToString();
                    index = Convert.ToInt32(e.CommandArgument);
                    //Indice = index;
                    //GridViewRow row = gvCabecera.Rows[index];
                    //string Miro = gvCabecera.DataKeys[index].Value.ToString();

                    //////////GridViewRow row = gvCabecera.Rows[index];
                    //////////string miro = gvCabecera.DataKeys[index].Value.ToString();

                    //////////Label Etiqueta = row.FindControl("lblEstado") as Label;
                    //////////if (Etiqueta.Text == "En preparación") { this.Session["EstadoCabecera"] = "1"; }
                    //////////if (Etiqueta.Text == "Confirmado") { this.Session["EstadoCabecera"] = "2"; }
                    //////////if (Etiqueta.Text == "Cerrado") { this.Session["EstadoCabecera"] = "3"; }
                    //gvCabecera_Selecciona(Miro);
                }
            }
            catch (Exception ex)
            {
                Lberror.Text = "Control RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvCabecera.DataKeys[index].Value.ToString() + " " + ex.Message;
                Lberror.Visible = true;
            }
        }

        private void LimpiaCajasPrint()
        {
            DLbEmpresa0.Text = "";
            DLbLote0.Text = "";
            DLbOrdenCarga0.Text = "";
            DLbPosCamion0.Text = "";
            DlbCliente0.Text = "";
            DLbVariedad0.Text = "";
            DLbNumerPlanta0.Text = "";
            DivEtiquetas.Visible = false;
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


        protected void gvLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            LimpiaCajasPrint();
            try
            {
                if (e.CommandName == "SubeCarga")
                {
                    decimal UNIDADES = 1.0M;
                    string Cabecera = TxtNumero.Text;
                    string rPosicion = "";

                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvLista.Rows[index];
                    string miro = gvLista.DataKeys[index].Value.ToString();
                    //sube la linea a la orden
                    string Numero = "";

                    Label txtBox = (gvLista.Rows[Indice].Cells[8].FindControl("LabLCarga") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        UNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvLista.Rows[Indice].Cells[4].FindControl("LabLNumLinea") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }
                    txtBox = (gvLista.Rows[Indice].Cells[14].FindControl("LabLcamion") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rPosicion = txtBox.Text;
                    }
                    //ID; UNIDADES; numero linea
                    this.Session["ElIDaBorrar"] = miro + ";" + UNIDADES + ";" + Numero + ";" + rPosicion;

                    this.Session["ModificaLinea"] = "Sube;";

                    Lbmensaje.Text = " El registro seleccionado se eliminará y las existencias volverán a Listas de Pedidos pendientes. ";
                    Lbmensaje.Text += " Puede eliminarlo, puede eliminar y además corregir la posiciones de Camión, o bien cancelar todo. ";
                    Lbmensaje.Text += "Seleccione una de la opciones de más abajo.";

                    cuestion.Visible = false;
                    Asume.Visible = false;
                    Decide.Visible = true;
                    Modifica.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    //BtnAcepta.Visible = true;
                    //BTnNoAcepta.Visible = true;

                    row.BackColor = Color.FromName("#ffead1");

                    //string Mira = Server.HtmlDecode(row.Cells[8].Text);
                    //if (Mira != "")
                    //{
                    //    UNIDADES = Convert.ToDecimal(Mira.Replace(".", ","));
                    //}

                    //Mira = Server.HtmlDecode(row.Cells[4].Text);
                    //if (Mira != "")
                    //{
                    //    Numero = Mira;
                    //}

                    //string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + UNIDADES.ToString().Replace(",", ".") + ")), ";
                    //SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + UNIDADES.ToString().Replace(",", ".") + ")), ";
                    //SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + UNIDADES.ToString().Replace(",", ".") + ")),  ";
                    //SQL += " NUMPALET = 0.00 ";
                    //SQL += " WHERE ID = " + miro;
                    //DBHelper.ExecuteNonQuery(SQL);

                    //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + miro + " AND NUMERO_LINEA = " + Numero;

                    //DBHelper.ExecuteNonQuery(SQL);

                    //Carga_tabla();
                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;

                    //gvLista.DataBind();


                }

                if (e.CommandName == "CargaCamion")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvLista.Rows[index];
                    string miro = gvLista.DataKeys[index].Value.ToString();

                    string Numero = "";

                    //string Mira = Server.HtmlDecode(row.Cells[4].Text);
                    //if (Mira != "")
                    //{
                    //    Numero = Mira;
                    //}
                    Label txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("LabLNumLinea") as Label);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }

                    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
                    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
                    SQL += " WHERE ID_CABECERA = " + TxtNumero.Text;
                    SQL += " AND NUMERO_LINEA = " + Numero;
                    Lberror.Text += SQL + "1- gvlista_rowcomand " + Variables.mensajeserver;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 2- gvlista_rowcomand  " + Variables.mensajeserver;
                    Vista_Print(dt, Numero);

                    this.Session["Menu"] = "5";
                    Carga_Menus();
                    PNreportLista.Visible = false;
                    DivEtiquetas.Visible = true;

                    SQL = " UPDATE ZCARGA_LINEA SET ESTADO = 2 WHERE ID_CABECERA = " + TxtNumero.Text + " AND NUMERO_LINEA = " + Numero;
                    DBHelper.ExecuteNonQuery(SQL);
                    //SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
                    //SQL += " WHERE NUMERO_LINEA = " + Numero; //Miro con ID lo hace con todos

                    DBHelper.ExecuteNonQuery(SQL);

                    if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    else
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    DBHelper.ExecuteNonQuery(SQL);

                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;

                    //gvLista.DataBind();
                }

                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridB"] = gvLista.DataKeys[index].Value.ToString();
                    //gvControl.EditIndex = -1;
                    //gvControl.DataBind();
                }

                Lberror.Visible = false;
            }
            catch (Exception ex)
            {
                Lberror.Text = "Lista RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvLista.DataKeys[index].Value.ToString() + " " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvControl_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < gvControl.Columns.Count; i++)
                {
                    if (i == 0) { e.Row.Cells[i].ToolTip = "Edición de Registro"; }
                    else if (i == 1) { e.Row.Cells[i].ToolTip = "Selecciona Órden"; }
                    else if (i == 10) { e.Row.Cells[i].ToolTip = "Unidades pedidas desde GoldenSoft"; }
                    else if (i == 11) { e.Row.Cells[i].ToolTip = "Unidades servidas desde GoldenSoft"; }
                    else if (i == 12) { e.Row.Cells[i].ToolTip = "Cálculo automático de la suma de las unidades alojadas en las listas de carga inferior"; }
                    else if (i == 13) { e.Row.Cells[i].ToolTip = "Cálculo automático de las Unidades pendientes resultantes de las Unidades pedidas, menos las Unidades a Cargar más las Unidades servidas"; }
                    else if (i == 14) { e.Row.Cells[i].ToolTip = "Seleccione manualmente las Unidades a cargar en este momento"; }
                    else if (i == 15) { e.Row.Cells[i].ToolTip = "Cálculo manual de los Palets necesarios para las Unidades a cargar en este momento"; }
                    else
                    {
                        e.Row.Cells[i].ToolTip = gvControl.Columns[i].HeaderText;
                    }
                }
                e.Row.BackColor = Color.FromName("#f5f5f5");
                //e.Row.TableSection = TableRowSection.TableHeader;
            }

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.TableSection = TableRowSection.TableFooter;
            //}

            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txt = e.Row.FindControl("TabOCajas") as TextBox;
                //txt.TextChanged += new EventHandler(TabOCajas_TextChanged);

                string miro = DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();

                //ShowEditButton
                if (this.Session["Cerrados"].ToString() == "1")
                {
                    //HtmlImage editar = (HtmlImage)e.Row.Cells[0].Controls[0];
                    ImageButton editar = e.Row.Cells[0].Controls[0] as ImageButton;
                    editar.Visible = false;
                    ImageButton SubeCarga = ((ImageButton)e.Row.FindControl("ibtbajaOrden"));
                    SubeCarga.Visible = false;

                    //editar = e.Row.Cells[1].Controls[0] as ImageButton;
                    //editar.Visible = false;
                    //CommandField editbutton = (CommandField)sender;
                    //editbutton.ShowEditButton = false;
                }

                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
                    //verde
                }
                else
                {
                    miro = DataBinder.Eval(e.Row.DataItem, "NUMPALET").ToString();
                    if (miro != "0,00")
                    {
                        e.Row.BackColor = Color.FromName("#d2f2f6");
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

                //e.Row.TableSection = TableRowSection.TableBody;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }
        
        protected void GrResultado_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
        }
        protected void gvCabecera_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
        }
        protected void GvBandeja_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvCabecera.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvControl_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvControl.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvLista.SelectedRow.BackColor = Color.FromName("#565656");
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
            

        protected void gvCabecera_OnSorting(object sender, GridViewSortEventArgs e)
        {
            //if (CaCheck.Checked == false)
            //{
            string a = e.SortExpression;
            this.Session["GridOrden"] = e.SortExpression;
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera(e.SortExpression);
            }
            else
            {
                Carga_tablaCabeceraClose(e.SortExpression);
            }
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

        //sube baja fila

        //protected void ChangePreference(object sender, EventArgs e)
        //{
        //    string commandArgument = (sender as LinkButton).CommandArgument;

        //    int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

        //    GridViewRow row = gvLista.Rows[rowIndex];
        //    string Miro = Server.HtmlDecode(row.Cells[6].Text);

        //    Miro = gvLista.Rows[rowIndex].Cells[0].Text;

        //    Miro = gvLista.Rows[rowIndex].Cells[0].Text; //ZID
        //    Miro = gvLista.Rows[rowIndex].Cells[1].Text; //ID
        //    Miro = gvLista.Rows[rowIndex].Cells[2].Text;//ID_CABECERA
        //    Miro = gvLista.Rows[rowIndex].Cells[3].Text; //EMPRESA
        //    Miro = gvLista.Rows[rowIndex].Cells[4].Text; //CLIENTEPROVEEDOR
        //    Miro = gvLista.Rows[rowIndex].Cells[5].Text;//NOMBREFISCAL
        //    Miro = gvLista.Rows[rowIndex].Cells[6].Text;//RUTA
        //    Miro = gvLista.Rows[rowIndex].Cells[7].Text;//NUMERO
        //    Miro = gvLista.Rows[rowIndex].Cells[8].Text;//NUMERO_LINEA
        //    Miro = gvLista.Rows[rowIndex].Cells[9].Text;//LINEA
        //    Miro = gvLista.Rows[rowIndex].Cells[10].Text;//ARTICULO
        //    Miro = gvLista.Rows[rowIndex].Cells[11].Text;//UDSENCARGA
        //    Miro = gvLista.Rows[rowIndex].Cells[12].Text;//NUMPALET
        //    Miro = gvLista.Rows[rowIndex].Cells[13].Text;//POSICIONCAMION
        //    Miro = gvLista.Rows[rowIndex].Cells[14].Text;//OBSERVACIONES
        //    Miro = gvLista.Rows[rowIndex].Cells[15].Text;//ESTADO
        //    Miro = gvLista.Rows[rowIndex].Cells[16].Text;//FECHAENTREGA
        //    Miro = gvLista.Rows[rowIndex].Cells[17].Text;//COMPUTER
        //    //Miro = gvLista.Rows[rowIndex].Cells[18].Text;//SERIE_PED
        //    //Miro = gvLista.Rows[rowIndex].Cells[19].Text;//HASTA
        //    //Miro = gvLista.Rows[rowIndex].Cells[20].Text;//ZSYSDATE
        //    //Miro = gvLista.Rows[rowIndex].Cells[21].Text;//ZORDEN





        //    int locationId = Convert.ToInt32(gvLista.Rows[rowIndex].Cells[0].Text);
        //    int preference = Convert.ToInt32(gvLista.DataKeys[rowIndex].Value);
        //    preference = commandArgument == "up" ? preference - 1 : preference + 1;
        //    this.UpdatePreference(locationId, preference);

        //    rowIndex = commandArgument == "up" ? rowIndex - 1 : rowIndex + 1;
        //    locationId = Convert.ToInt32(gvLista.Rows[rowIndex].Cells[0].Text);
        //    preference = Convert.ToInt32(gvLista.DataKeys[rowIndex].Value);
        //    preference = commandArgument == "up" ? preference + 1 : preference - 1;
        //    this.UpdatePreference(locationId, preference);

        //    Response.Redirect(Request.Url.AbsoluteUri);
        //}

        //private void UpdatePreference(int locationId, int preference)
        //{
        //    string SQL = "UPDATE ZCARGA_LINEA SET ZORDEN = " + preference + " WHERE ZID = " + locationId;
        //    DBHelper.ExecuteNonQuery(SQL);

        //    //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    //using (SqlConnection con = new SqlConnection(constr))
        //    //{
        //    //    using (SqlCommand cmd = new SqlCommand("UPDATE ZCARGA_LINEA SET ZORDEN = " + preference + " WHERE ZID = " + locationId))
        //    //    {
        //    //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //    //        {
        //    //            cmd.CommandType = CommandType.Text;
        //    //            cmd.Parameters.AddWithValue("@Id", locationId);
        //    //            cmd.Parameters.AddWithValue("@Preference", preference);
        //    //            cmd.Connection = con;
        //    //            con.Open();
        //    //            cmd.ExecuteNonQuery();
        //    //            con.Close();
        //    //        }
        //    //    }
        //    //}
        //}

        
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



                //e.Row.TableSection = TableRowSection.TableBody;
            }
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //e.Row.TableSection = TableRowSection.TableFooter;
            //    e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //e.Row.TableSection = TableRowSection.TableHeader;
            //    e.Row.Cells[4].Width = 1040;
            //    //e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.TableSection = TableRowSection.TableFooter;
            //}
        }

        protected void checkSiEl_Click(object sender, EventArgs e)
        {
            //Cuestion: solo elimina
            checkSi_Click(sender, e);
        }

        protected void checkSiElC_Click(object sender, EventArgs e)
        {
            //Cuestion: elimina y corrige
            if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////             ID; UNIDADES; numero linea; posicion camion
            //ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //Select
            string SQL = " SELECT * FROM ZCARGA_LINEA  ";
            SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
            SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";
            SQL += " ORDER BY POSICIONCAMION ";

            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            int A = 0;

            foreach (DataRow filas in dt.Rows)
            {
                if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[3]))
                {
                    A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
                    SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
                    SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                    SQL += " AND ZID = " + filas["ZID"].ToString() + " ";

                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            //Cuestion: ahora elimina
            checkSi_Click(sender, e);
        }

        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
        }
        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            Lbmensaje.Text = "";
            this.Session["ElIDaBorrar"] = "";
            MiOpenMenu();
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            Lbmensaje.Text = "";
            MiOpenMenu();

            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;

            if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////             ID; UNIDADES; numero linea
            //ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 3), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
            SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 3), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
            SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 3), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
            SQL += " NUMPALET = 0.00 ";
            SQL += " WHERE ID = " + Trata[0];
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

            DBHelper.ExecuteNonQuery(SQL);

            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            Carga_tabla();
            Carga_tablaLista();

            gvLista.EditIndex = -1;

            gvLista.DataBind();

            this.Session["ElIDaBorrar"] = "";
        }



        protected void checkSiMlC_Click(object sender, EventArgs e)
        {
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //Select
            string SQL = " SELECT * FROM ZCARGA_LINEA  ";
            SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
            SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";
            SQL += " ORDER BY POSICIONCAMION ";

            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            int A = 0;
            //int B = 0;
            // MiPosicion, Lanuevaposicion, ID, ID_CABECERA, NUMERO_LINEA
            //this.Session["ElIDaBorrar"] = Con + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;

            //si la Lanuevaposicion es mayor que MiPosicion actual
            if (Convert.ToInt32(Trata[1]) > Convert.ToInt32(Trata[0]))
            {
                foreach (DataRow filas in dt.Rows)
                {
                    //si la posicion es igual o mayor que la posicion elegida
                    string miro = filas["POSICIONCAMION"].ToString();

                    if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) >= Convert.ToInt32(Trata[1]))
                    {
                        //si la posicion es igual que la posicion elegida
                        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[1]))
                        {
                            if (filas["NUMERO_LINEA"].ToString() == Trata[4])
                            {
                            }
                            else
                            {
                                A = Convert.ToInt32(Trata[1]) - 1;
                                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
                                SQL += " ESTADO = 1 ";
                                SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
                                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
                                SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
                                DBHelper.ExecuteNonQuery(SQL);
                            }
                        }
                    }
                    //si la posicion es mayor que la posicion que tenia pero menor que la eligida
                    else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) >= Convert.ToInt32(Trata[0]) &&
                        Convert.ToInt32(filas["POSICIONCAMION"].ToString()) < Convert.ToInt32(Trata[1]) &&
                        filas["NUMERO_LINEA"].ToString() != Trata[4])
                    {
                        A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
                        SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
                        SQL += " ESTADO = 1 ";
                        SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
                        SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                        SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
                        SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
                        DBHelper.ExecuteNonQuery(SQL);
                    }
                }
            }
            //si la Lanuevaposicion es menor que MiPosicion actual
            else
            {
                foreach (DataRow filas in dt.Rows)
                {
                    string miro = filas["POSICIONCAMION"].ToString();
                    //si la posicion es igual que la posicion elegida
                    if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[1]))
                    {
                        if (filas["NUMERO_LINEA"].ToString() == Trata[4])
                        {
                        }
                        else
                        {
                            A = Convert.ToInt32(Trata[1]) + 1;
                            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
                            SQL += " ESTADO = 1 ";
                            SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
                            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
                            SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                    }
                    //si la posicion es igual que la posicion elegida pero menor o igual que la posicion que tenia
                    else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[1]) && Convert.ToInt32(filas["POSICIONCAMION"].ToString()) <= Convert.ToInt32(Trata[0]))
                    {
                        //si la posicion es menor o igual que la posicion que tenia
                        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) <= Convert.ToInt32(Trata[0]))
                        {
                            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) + 1;
                            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
                            SQL += " ESTADO = 1 ";
                            SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
                            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
                            SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                        //si la posicion es menor a la que tenia y mayor que la elegida
                        else
                        {
                            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
                            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
                            SQL += " ESTADO = 1 ";
                            SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
                            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
                            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
                            SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                    }
                }
            }


            //    //si la posicion es igual a la que tenia
            //    else if (B == Convert.ToInt32(Trata[0]))
            //    {
            //        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[0]))
            //        {
            //            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString());
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //        }
            //        else
            //        {
            //            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //        }

            //        DBHelper.ExecuteNonQuery(SQL);
            //    }
            //    //si la posicion es mayor a la que tenia
            //    else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[1]))
            //    {
            //        A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //        SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //        SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //        SQL += " AND ZID = " + filas["ZID"].ToString() + " ";

            //        DBHelper.ExecuteNonQuery(SQL);
            //    }
            //}


            ////si la nueva posicion es menor que la que tiene
            //if (Convert.ToInt32(Trata[1]) > Convert.ToInt32(Trata[0]))
            //{
            //    foreach (DataRow filas in dt.Rows)
            //    {
            //        //si la posicion es igual a la que pide
            //        B += 1;
            //        string miro = filas["POSICIONCAMION"].ToString();
            //        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[1]))
            //        {
            //            A = Convert.ToInt32(Trata[1]);
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + Trata[2] + " ";
            //            SQL += " AND ID_CABECERA = " + Trata[3] + " ";
            //            SQL += " AND NUMERO_LINEA = " + Trata[4] + " ";
            //            DBHelper.ExecuteNonQuery(SQL);

            //            A = Convert.ToInt32(Trata[1]) + 1;
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //            SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //            DBHelper.ExecuteNonQuery(SQL);
            //        }
            //        //si la posicion es igual a la que tenia
            //        else if ( B == Convert.ToInt32(Trata[0]))
            //        {
            //            if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[0]))
            //            {
            //                A = Convert.ToInt32(filas["POSICIONCAMION"].ToString());
            //                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //            }
            //            else
            //            {
            //                A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //            }

            //            DBHelper.ExecuteNonQuery(SQL);
            //        }
            //        //si la posicion es mayor a la que tenia
            //        else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[1]))
            //        {
            //            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";

            //            DBHelper.ExecuteNonQuery(SQL);
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (DataRow filas in dt.Rows)
            //    {
            //        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[1]))
            //        {
            //            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";

            //            DBHelper.ExecuteNonQuery(SQL);
            //        }
            //    }
            //}

            Modifica.Visible = false;
            Asume.Visible = false;
            cuestion.Visible = false;
            Decide.Visible = false;
            windowmessaje.Visible = false;
            MiOpenMenu();

            //DBHelper.ExecuteNonQuery(this.Session["SelectLinea"].ToString());
            //Lberror.Text += " 1- checkSiMlC_Click " + Variables.mensajeserver;
            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            Carga_tablaLista();

            gvLista.EditIndex = -1;
            gvLista.DataBind();

        }

        protected void checkNoMlC_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            MiOpenMenu();

            string SQL = "";
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA set  ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
                SQL += " WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            this.Session["ElIDaBorrar"] = "";

            Carga_tablaCabecera(this.Session["GridOrden"].ToString());
            Carga_tablaLista();

            gvLista.EditIndex = -1;
            gvLista.DataBind();
        }

        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            MiOpenMenu();
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            this.Session["ElIDaBorrar"] = "";
        }

        //private void CargaLineasSeleccion(String[] Trata)
        //{
        //    if(Trata[0] == "Msg")
        //    {
        //        string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
        //        SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
        //        SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
        //        SQL += " NUMPALET = 0.00 ";
        //        SQL += " WHERE ID = " + Trata[0];

        //        DBHelper.ExecuteNonQuery(SQL);

        //        SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

        //        DBHelper.ExecuteNonQuery(SQL);

        //        Lbmensaje.Text = "¿Desea desplazar los palets de posiciones intermedias?.";
        //        cuestion.Visible = true;
        //        Asume.Visible = false;
        //        Decide.Visible = false;
        //        windowmessaje.Visible = true;
        //        //BtnAcepta.Visible = true;
        //        //BTnNoAcepta.Visible = true;
        //    }

        //}

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow row = (GridViewRow)gvLista.Rows[e.RowIndex];

            this.Session["ElIDaBorrar"] = gvLista.DataKeys[e.RowIndex].Value.ToString();

            cuestion.Visible = true;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            windowmessaje.Visible = true;
            MiOpenMenu();

            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;

            //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            //SQL += " WHERE ID = " + miro;

            //DBHelper.ExecuteNonQuery(SQL);
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
        }
        protected void GvBandeja_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gvCabecera_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = (GridViewRow)gvCabecera.Rows[e.RowIndex];

            string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();

            string SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2, ";
            SQL += "ZSYSDATE = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            //if (CaCheck.Checked == false)
            //{
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Carga_tablaCabecera(this.Session["GridOrden"].ToString());
            }
            else
            {
                Carga_tablaCabeceraClose(this.Session["GridOrden"].ToString());
            }
            //Carga_tablaCabecera();

            gvCabecera.EditIndex = -1;

            gvCabecera.DataBind();
        }

        private void Actualiza_tabla()
        {
            string SQL = this.Session["Filtro"].ToString();
            DataTable dt = null;
            //Carga_Filtros();

            if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
            {
                //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, ID ";
                //SQL = "SELECT * FROM ZORDEN_CARGA  WHERE ESTADO = 0 ";
                dt = Main.BuscaLote(SQL).Tables[0];
            }
            else
            {
                //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, ID ";
                //SQL = " SELECT * FROM ZORDEN_CARGA WHERE ESTADO = 0  ";
                dt = Main.BuscaLoteGold(SQL).Tables[0];
            }

            this.Session["MiConsulta"] = dt;
            gvControl.DataSource = dt;
            gvControl.DataBind();


        }

        private void Carga_tablaCabeceraClose(string sortExpression = null)
        {
            Carga_tablaListaFiltro();
            string filtros = this.Session["Filtro"].ToString();
            if (this.Session["Param3"].ToString() != "")
            {
                filtros += this.Session["Param3"].ToString();
            }
            string SQL = "";
            if (this.Session["sortExpression"].ToString() != "")
            {
                sortExpression = this.Session["sortExpression"].ToString();
                if(DrNombre.Text != "Ninguno")
                {
                    ElOrden = " ORDER BY A." + this.Session["sortExpression"].ToString() + " " + this.SortDirection;
                }
                else
                {
                    ElOrden = "";
                }
            }
            else 
            {
                sortExpression = null;
            }


            try
            {
                //Llamada a los campos activos desde Archivos
                //Para agregar una columna usar esto:

                //HyperLinkField link = new HyperLinkField();
                //link.HeaderText = "Columna";
                //GridView1.Columns.Add(link);
                //GridView1.DataBind();

                //o

                //HyperLinkField link = new HyperLinkField();
                //link.NavigateUrl = "URLCol";
                //link.HeaderText = "headerText";
                //GridView1.Columns.Add(link);

                //DataTable result = new DataTable();
                //GridView1.DataSource = result;
                //GridView1.DataBind();

                //o

                //BoundField test = new BoundField();
                //test.DataField = "New DATAfield Name";
                //test.HeaderText = "New Header";
                //GridView1.Columns.Add(test);



                DataTable dt = null;
                //Carga_Filtros();
                //OrdenCabecera();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //Casillas verdes a rellenar. Al mostrar inicialmente UdsACargar= UdsPed-UdsServ-UdsEnCarga + ' ' + CONVERT(varchar(15),CAST(A.ZSYSDATE AS TIME),100)
                    //UdsPend campo calculado(UdsPend = UdsPed - UdsServ - UdsEnCarga - UdsACargar).Al mostrar inicialmente el cálculo será 0.(UDSPEDIDAS - UDSSERVIDAS - UDSENCARGA - UDSACARGAR)
                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO, A.ZSYSDATE, ";
                    SQL += " CONVERT(varchar(20),FORMAT (A.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss')) AS ZSYSHORA ";
                    SQL += " FROM ZCARGA_CABECERA A INNER JOIN ZCARGAESTADO B ON A.ESTADO = B.ZID  WHERE A.ESTADO = 3 ";

                    //SQL = " SELECT ID, NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA,";
                    //SQL += " TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO ";
                    //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE ESTADO = 2  ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    //SQL = "SELECT * FROM ZCARGA_ORDEN  WHERE ESTADO = 0 ";+ ' ' + CONVERT(varchar(15),CAST(A.ZSYSDATE AS TIME),100)
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO, A.ZSYSDATE, ";
                    SQL += " CONVERT(varchar(20),FORMAT (A.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
                    SQL += " FROM ZCARGA_CABECERA A INNER JOIN ZCARGAESTADO B ON A.ESTADO = B.ZID  WHERE A.ESTADO = 3 ";

                    //SQL = " SELECT ID, NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA,";
                    //SQL += " TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO ";
                    //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_CABECERA  WHERE ESTADO = 2  "; //jose
                    //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE (ESTADO <> 2 OR ESTADO IS NULL) ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    dt = Main.BuscaLote(SQL).Tables[0];
                }

                this.Session["MiConsulta"] = dt;

                if (sortExpression == null || sortExpression == "")
                {
                    gvCabecera.DataSource = dt;
                }
                else
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvCabecera.DataSource = dv;
                }
                gvCabecera.DataBind();
                lbRowCabecera.Text = "Registros: " + dt.Rows.Count;
                //gvCabecera.DataSource = dt;
                //gvCabecera.DataBind();

                Carga_FiltrosCabecera();
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Lberror.Text = "Carga tabla: gvCabecera " + SQL;
                Lberror.Visible = true;
            }


        }

        private void Carga_tablaCabecera(string sortExpression = null)
        {
            string SQL = ""; // "NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA, TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO";
            Carga_tablaListaFiltro();
            string filtros = this.Session["Filtro"].ToString();
            if(this.Session["Param3"].ToString() != "")
            {
                filtros += this.Session["Param3"].ToString();
            }
            //OrdenCabecera();

            Lberror.Text = "";
            try
            {

                DataTable dt = null;
                //Carga_Filtros();
                //OrdenCabecera();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //Casillas verdes a rellenar. Al mostrar inicialmente UdsACargar= UdsPed-UdsServ-UdsEnCarga + ' ' + CONVERT(varchar(15),CAST(A.ZSYSDATE AS TIME),100)
                    //UdsPend campo calculado(UdsPend = UdsPed - UdsServ - UdsEnCarga - UdsACargar).Al mostrar inicialmente el cálculo será 0.(UDSPEDIDAS - UDSSERVIDAS - UDSENCARGA - UDSACARGAR)

                    //SQL = " SELECT ID, NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA,";
                    //SQL += " TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO ";
                    //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE (ESTADO <> 2 OR ESTADO IS NULL) AND ID <> 1 ";
                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO, A.ZSYSDATE , ";
                    SQL += " CONVERT(varchar(20),FORMAT (A.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
                    SQL += " FROM ZCARGA_CABECERA A INNER JOIN ZCARGAESTADO B ON A.ESTADO = B.ZID  WHERE (A.ESTADO <> 3 OR A.ESTADO IS NULL) AND A.ESTADO <> -1  ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    //SQL = "SELECT * FROM ZCARGA_ORDEN  WHERE ESTADO = 0 ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    //SQL = " SELECT ID, NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA,"; + ' ' + CONVERT(varchar(15),CAST(A.ZSYSDATE AS TIME),100)
                    //SQL += " TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO ";
                    //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_CABECERA  WHERE (ESTADO <> 2 OR ESTADO IS NULL) AND ID <> 1  "; //jose
                    ////SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE (ESTADO <> 2 OR ESTADO IS NULL) ";

                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO, A.ZSYSDATE, ";
                    SQL += " CONVERT(varchar(20),FORMAT (A.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
                    SQL += " FROM ZCARGA_CABECERA A INNER JOIN ZCARGAESTADO B ON A.ESTADO = B.ZID WHERE (A.ESTADO <> 3 OR A.ESTADO IS NULL)  AND A.ESTADO <> -1 "; //jose
                    //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE (ESTADO <> 2 OR ESTADO IS NULL) ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    dt = Main.BuscaLote(SQL).Tables[0];
                }

                this.Session["MiConsulta"] = dt;

                if (sortExpression != null)
                {
                    if (sortExpression == "")
                    {
                        gvCabecera.DataSource = dt;
                    }
                    else
                    {
                        DataView dv = dt.AsDataView();
                        if (this.Session["GridEdicion"].ToString() == "0")
                        {
                            this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        }
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvCabecera.DataSource = dv;
                    }
                }
                else
                {
                    gvCabecera.DataSource = dt;
                }
                gvCabecera.DataBind();
                lbRowCabecera.Text = "Registros: " + dt.Rows.Count;
                //gvCabecera.DataSource = dt;
                //gvCabecera.DataBind();

                Carga_FiltrosCabecera();
            }
            catch (Exception ex)
            {
                Lberror.Text = "Carga tabla: gvCabecera " + Variables.Error + " " + ex.Message + SQL;
                Lberror.Visible = true;
            }


        }

        private void Carga_tabla(string sortExpression = null)
        {
            //string Temporal = ""; //Jose
            Carga_tablaListaFiltroOrden();
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
                Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), TxtNumero.Text);
                this.Session["MiConsulta"] = dt;

                if (sortExpression == null || sortExpression == "")
                {
                    gvControl.DataSource = dt;
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
                    gvControl.DataSource = dv;
                }
                gvControl.DataBind();
                LbRowControl.Text = "Registros: " + dt.Rows.Count;
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

        private void Carga_FiltrosCabecera()
        {
            string SQL = "";
            DataTable dt = null;

            DrEmpresa.Items.Clear();
            DrEmpresa.DataValueField = "EMPRESA";
            DrEmpresa.DataTextField = "EMPRESA";
            DrEmpresa.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY EMPRESA ";
            SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY EMPRESA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrEmpresa.DataSource = dt;
            DrEmpresa.DataBind();
            DrEmpresa.SelectedIndex = -1;

            //DrFecha.Items.Clear();
            //DrFecha.DataValueField = "FECHACARGA";
            //DrFecha.DataTextField = "FECHACARGA";
            //DrFecha.Items.Insert(0, new ListItem("Seleccione uno", ""));
            ////SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY FECHAENTREGA ";
            //SQL = "SELECT DISTINCT(FECHACARGA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY FECHACARGA ";
            //dt = Main.BuscaLote(SQL).Tables[0];
            //DrFecha.DataSource = dt;
            //DrFecha.DataBind();
            //DrFecha.SelectedIndex = -1;

            DrPais.Items.Clear();
            DrPais.DataValueField = "PAIS";
            DrPais.DataTextField = "PAIS";
            DrPais.Items.Insert(0, new ListItem("Seleccione uno", ""));
            SQL = "SELECT DISTINCT(PAIS) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY PAIS ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrPais.DataSource = dt;
            DrPais.DataBind();
            DrPais.SelectedIndex = -1;

            DrTransportista.Items.Clear();
            DrTransportista.DataValueField = "TRANSPORTISTA";
            DrTransportista.DataTextField = "TRANSPORTISTA";
            DrTransportista.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY RUTA ";
            SQL = "SELECT DISTINCT(TRANSPORTISTA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY TRANSPORTISTA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrTransportista.DataSource = dt;
            DrTransportista.DataBind();
            DrTransportista.SelectedIndex = -1;
        }

        private void Carga_Filtros()
        {
            string SQL = "";
            DataTable dt = null;

            DrConsultas.Items.Clear();
            DrConsultas.DataValueField = "EMPRESA";
            DrConsultas.DataTextField = "EMPRESA";
            DrConsultas.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY EMPRESA ";
            SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE (ESTADO < 4 OR ESTADO IS NULL) ORDER BY EMPRESA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrConsultas.DataSource = dt;
            DrConsultas.DataBind();
            DrConsultas.SelectedIndex = -1;

            DrDesde.Items.Clear();
            DrDesde.DataValueField = "FECHAENTREGA";
            DrDesde.DataTextField = "FECHAENTREGA";
            DrDesde.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY FECHAENTREGA ";
            SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY FECHAENTREGA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrDesde.DataSource = dt;
            DrDesde.DataBind();
            DrDesde.SelectedIndex = -1;

            DrHasta.Items.Clear();
            DrHasta.DataValueField = "FECHAENTREGA";
            DrHasta.DataTextField = "FECHAENTREGA";
            DrHasta.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINC(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO = 0 ORDER BY FECHAENTREGA ";
            //dt = Main.BuscaLote(SQL).Tables[0];
            DrHasta.DataSource = dt;
            DrHasta.DataBind();
            DrHasta.SelectedIndex = -1;

            DrRutas.Items.Clear();
            DrRutas.DataValueField = "RUTA";
            DrRutas.DataTextField = "RUTA";
            DrRutas.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY RUTA ";
            SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY RUTA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrRutas.DataSource = dt;
            DrRutas.DataBind();
            DrRutas.SelectedIndex = -1;

            DrCliente.Items.Clear();
            DrCliente.DataValueField = "NOMBREFISCAL"; // "CLIENTEPROVEEDOR";
            DrCliente.DataTextField = "NOMBREFISCAL"; //"CLIENTEPROVEEDOR";
            DrCliente.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(CLIENTEPROVEEDOR) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY CLIENTEPROVEEDOR ";
            SQL = "SELECT DISTINCT(NOMBREFISCAL) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY NOMBREFISCAL ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrCliente.DataSource = dt;
            DrCliente.DataBind();
            DrCliente.SelectedIndex = -1;
        }

        private void Carga_tablaListaFiltro()
        {

            string SQL = ""; // "SELECT * FROM ZCARGA_ORDEN ";
            string Filtros = "";
            DrSelectFiltro.Items.Clear();


            //LbFiltros.Text = "";
            try
            {
                if (DrNombre.SelectedItem.Value != "")
                {
                    if (DrNombre.SelectedItem.Value != "Ninguno")
                    {
                        if (TxtCodigo.Text == "''")
                        {
                            Filtros += " AND " + DrNombre.SelectedItem.Value + " = ''";
                            //this.Session["sortExpression"] = DrNombre.SelectedItem.Value;
                        }
                        else if (TxtCodigo.Text != "")
                        {
                            Filtros += " AND " + DrNombre.SelectedItem.Value + " = '" + TxtCodigo.Text + "'";
                        }
                        this.Session["sortExpression"] = DrNombre.SelectedItem.Value;

                    }
                    else
                    {
                        TxtCodigo.Text = "";
                    }
                }

                if (this.Session["MiMenu"].ToString() == "OrdenCarga")
                {
                    for (int i = 0; i < DrNombre.Items.Count; i++)
                    {
                        string a = "Empresa";
                        if (DrNombre.Items[i].Text.Contains(a))
                        {
                            DrNombre.Text = DrNombre.Items[i].Value;
                            TxtCodigo.Text = this.Session["Param1"].ToString();
                            Filtros += " AND EMPRESA = '" + TxtCodigo.Text + "' ";
                            break;
                        }
                    }
                }
                

                if (Filtros != "")
                {
                    this.Session["Filtro"] = SQL + Filtros;
                    //LbFiltros.Text += ")";
                    //PanelFiltros.Attributes.Add("style", "background-color:#e6f2e1");
                    PanelgeneralFiltro.Attributes.Add("style", "background-color:#e6f2e1");
                }
                else
                {
                    this.Session["Filtro"] = "";
                    //this.Session["sortExpression"] = "";
                    if (this.Session["FiltroEmpresa"].ToString() != "")
                    {
                        //PanelFiltros.Attributes.Add("style", "background-color:#e6f2e1");
                        //PanelgeneralFiltro.Attributes.Add("style", "background-color:#e6f2e1");
                        PanelOrden.Attributes.Add("style", "background-color:#e6f2e1");
                    }
                    else
                    {
                        //PanelFiltros.Attributes.Add("style", "background-color:#f5f5f5");
                        //PanelgeneralFiltro.Attributes.Add("style", "background-color:#f5f5f5");
                        PanelOrden.Attributes.Add("style", "background-color:#f5f5f5");
                        
                    }
                }
            }
            catch (Exception ex)
            {
                string b = ex.Message;
            }

            //string SQL = "";
            //DataTable dt = null;

            //if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
            //{
            //    SQL = " SELECT * FROM ZORDEN_LINEA ORDER BY ZPOSICIONCAMION ";
            //    dt = Main.BuscaLote(SQL).Tables[0];
            //}
            //else
            //{
            //    SQL = " SELECT * FROM [RIOERESMA].[dbo].ZORDEN_LINEA ORDER BY ZPOSICIONCAMION ";
            //    dt = Main.BuscaLoteGold(SQL).Tables[0];
            //}

            //this.Session["MiConsulta"] = dt;
            //gvLista.DataSource = dt;
            //gvLista.DataBind();
            //break;
            //}
        }



        private void Carga_tablaListaFiltroOrden()
        {

            string SQL = ""; // "SELECT * FROM ZCARGA_ORDEN ";
            string Filtros = "";
            this.Session["FiltroEmpresa"] = "";
            this.Session["FiltroFecha"] = "";
            this.Session["FiltroCliente"] = "";
            this.Session["FiltroRuta"] = "";
            DrSelectFiltro.Items.Clear();


            //LbFiltros.Text = "";
            try
            {
                //if (DrNombre.SelectedItem.Value != "")
                //{
                //    if (DrNombre.SelectedItem.Value != "Ninguno")
                //    {
                //        if (TxtCodigo.Text == "''")
                //        {
                //            Filtros += " AND " + DrNombre.SelectedItem.Value + " = ''";
                //        }
                //        else if (TxtCodigo.Text != "")
                //        {
                //            if (Filtros == "")
                //            {
                //                Filtros += " AND " + DrNombre.SelectedItem.Value + " = '" + TxtCodigo.Text + "'";
                //            }
                //            else
                //            {
                //                Filtros += " AND " + DrNombre.SelectedItem.Value + " = '" + TxtCodigo.Text + "'";
                //            }
                //        }
                //    }
                //}



                if (DrConsultas.SelectedItem.Value != "")
                {

                    Filtros += " AND EMPRESA = '" + DrConsultas.SelectedItem.Value + "'";
                    this.Session["FiltroEmpresa"] = DrConsultas.SelectedItem.Value;
                    DrSelectFiltro.Items.Add("Empresa: " + DrConsultas.SelectedItem.Value);
                    //LbFiltros.Text = " ( Empresa: " + DrConsultas.SelectedItem.Value;
                }
                if (DrDesde.SelectedItem.Value != "" && DrHasta.SelectedItem.Value != "")
                {
                    Filtros += " AND FECHAENTREGA BETWEEN  '" + DrDesde.SelectedItem.Value + "' AND '" + DrHasta.SelectedItem.Value + "'";
                    this.Session["FiltroFecha"] = " B.Libre6 BETWEEN  '" + DrDesde.SelectedItem.Value + "' AND '" + DrHasta.SelectedItem.Value + "'";
                    DrSelectFiltro.Items.Add("Fecha Desde: " + DrDesde.SelectedItem.Value + ", Fecha Hasta: " + DrHasta.SelectedItem.Value);
                    //LbFiltros.Text += ", Fecha Desde: " + DrDesde.SelectedItem.Value + ", Fecha Hasta: " + DrHasta.SelectedItem.Value;
                }
                else
                {
                    if (DrDesde.SelectedItem.Value != "")
                    {
                        Filtros += " AND FECHAENTREGA = '" + DrDesde.SelectedItem.Value + "'";
                        this.Session["FiltroFecha"] = " B.Libre6 = '" + DrDesde.SelectedItem.Value + "'";
                        DrSelectFiltro.Items.Add("Fecha Desde: " + DrDesde.SelectedItem.Value);
                        //LbFiltros.Text += ", Fecha Desde: " + DrDesde.SelectedItem.Value;
                    }
                    if (DrHasta.SelectedItem.Value != "")
                    {
                        Filtros += " AND FECHAENTREGA = '" + DrHasta.SelectedItem.Value + "'";
                        this.Session["FiltroFecha"] = " B.Libre6 = '" + DrHasta.SelectedItem.Value + "'";
                        DrSelectFiltro.Items.Add("Fecha Hasta: " + DrHasta.SelectedItem.Value);
                        //LbFiltros.Text += ", Fecha Hasta: " + DrHasta.SelectedItem.Value;
                    }
                }
                if (DrCliente.SelectedItem.Value != "")
                {
                    Filtros += " AND NOMBREFISCAL = '" + DrCliente.SelectedItem.Value + "' ";
                    this.Session["FiltroCliente"] = " A.[Nombre Fiscal] = '" + DrCliente.SelectedItem.Value + "'";
                    DrSelectFiltro.Items.Add("Cliente: " + DrCliente.SelectedItem.Value);


                    //Filtros += " AND CLIENTEPROVEEDOR = " + DrCliente.SelectedItem.Value + "";
                    ////}
                    //this.Session["FiltroCliente"] = " A.[Cliente Proveedor] = " + DrCliente.SelectedItem.Value + "";
                    //DrSelectFiltro.Items.Add("Cliente: " + DrCliente.SelectedItem.Value);
                    ////LbFiltros.Text += ", Cliente: " + DrCliente.SelectedItem.Value;
                }
                if (DrRutas.SelectedItem.Value != "")
                {
                    Filtros += " AND RUTA = '" + DrRutas.SelectedItem.Value + "'";
                    this.Session["FiltroRuta"] = " A.[Ruta Envio] = '" + DrRutas.SelectedItem.Value + "'";
                    DrSelectFiltro.Items.Add("Ruta: " + DrRutas.SelectedItem.Value);
                    //LbFiltros.Text += ", Ruta: " + DrRutas.SelectedItem.Value;
                }
                if (Filtros != "")
                {
                    this.Session["Filtro"] = SQL + Filtros;
                    //LbFiltros.Text += ")";
                    //PanelFiltros.Attributes.Add("style", "background-color:#e6f2e1");
                    PanelgeneralFiltro.Attributes.Add("style", "background-color:#e6f2e1");
                }
                else
                {
                    //this.Session["Filtro"] = "";
                    //if (this.Session["FiltroEmpresa"].ToString() != "")
                    //{
                        //PanelFiltros.Attributes.Add("style", "background-color:#e6f2e1");
                        //PanelgeneralFiltro.Attributes.Add("style", "background-color:#e6f2e1");
                        //PanelOrden.Attributes.Add("style", "background-color:#e6f2e1");
                    //}
                    //else
                    //{
                    //    //PanelFiltros.Attributes.Add("style", "background-color:#f5f5f5");
                    PanelgeneralFiltro.Attributes.Add("style", "background-color:#f5f5f5");
                    //PanelOrden.Attributes.Add("style", "background-color:#f5f5f5");
                    //}
                }
            }
            catch (Exception ex)
            {
                string b = ex.Message;
            }

            //string SQL = "";
            //DataTable dt = null;

            //if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
            //{
            //    SQL = " SELECT * FROM ZORDEN_LINEA ORDER BY ZPOSICIONCAMION ";
            //    dt = Main.BuscaLote(SQL).Tables[0];
            //}
            //else
            //{
            //    SQL = " SELECT * FROM [RIOERESMA].[dbo].ZORDEN_LINEA ORDER BY ZPOSICIONCAMION ";
            //    dt = Main.BuscaLoteGold(SQL).Tables[0];
            //}

            //this.Session["MiConsulta"] = dt;
            //gvLista.DataSource = dt;
            //gvLista.DataBind();
            //break;
            //}
        }

        private void Carga_tablaLista(string sortExpression = null)
        {
            string SQL = "";
            DataTable dt = null;

            //Carga_tablaListaFiltro();
            string filtro = ""; // this.Session["Filtro"].ToString();
            //OrdenLista();
            if (TxtNumero.Text == "") { TxtNumero.Text = "0"; }
            try
            {
                //this.Session["NumeroPalet"] = "0";
                filtro = filtro.Replace("WHERE", "AND");
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT * FROM ZCARGA_LINEA  ";
                    SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                    SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";
                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    if (ElOrdenLista != "")
                    {
                        SQL += ElOrdenLista;
                    }
                    else
                    {
                        SQL += " ORDER BY POSICIONCAMION ";
                    }


                    Lberror.Text += " 1- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + "1- Carga_tablaLista " + SQL + Environment.NewLine;


                }
                else
                {
                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT * FROM ZCARGA_LINEA  ";
                    SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                    SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";

                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    if (ElOrdenLista != "")
                    {
                        SQL += ElOrdenLista;
                    }
                    else
                    {
                        SQL += " ORDER BY POSICIONCAMION ";
                    }

                    Lberror.Text += " 2- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_tablaLista " + SQL + Environment.NewLine;


                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ORDER BY POSICIONCAMION ";
                    //dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

                if (sortExpression == null || sortExpression == "")
                {
                    gvLista.DataSource = dt;
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
                    gvLista.DataSource = dv;
                }
                gvLista.DataBind();

                //gvLista.DataSource = dt;
                //gvLista.DataBind();
                //break;
                //< div class="contenedor" draggable="true" ondragstart="drag(event)" id="drag0" data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                //    <img class="pokemon" src="images/palet200X300.png" />
                //    <div id = "dragText0" class="centrado">QR 21P322</div>
                //</div>  dt.Rows.Count;
                LbRowLista.Text = "Registros: " + dt.Rows.Count;

                this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
                if (TxtNumero.Text == "0")
                {
                    LBCountLista.Text = "Contiene: 0 Líneas de carga";
                }
                else
                {
                    LBCountLista.Text = "Contiene: " + dt.Rows.Count.ToString() + " Líneas de carga";
                }
                CreaPalets(dt);
                gvLista.EditIndex = -1;

                //Busca Error
                Lberror.Text = "";

            }
            catch (Exception mm)
            {
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
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


        //private void CreaPalets(DataTable dt)
        //{
        //    int i = Convert.ToInt32(this.Session["NumeroPalet"].ToString());

        //    Lberror.Visible = true;
        //    Lberror.Text = "Numero de palets: " + i;

        //    if (i == 0) { return; }
        //    container2.Controls.Clear();
        //    fuego.Controls.Clear();
        //    agua.Controls.Clear();
        //    i = 0;
        //    foreach (DataRow filas in dt.Rows)
        //    {
        //        //<a data-toggle="collapse" onclick="submitit3()" href="#collapse3">
        //        System.Web.UI.HtmlControls.HtmlAnchor createAnchor = new System.Web.UI.HtmlControls.HtmlAnchor();
        //        createAnchor.ID = "Anchordrag" + i;
        //        createAnchor.Attributes["onclick"] = "submitAnchor()";
        //        createAnchor.Attributes["runat"] = "server";

        //        System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //        createDiv.ID = "drag" + i;
        //        //createDiv.InnerHtml = " I'm a div, from code behind ";
        //        createDiv.Attributes["class"] = "contenedor";
        //        string miro = filas["POSICIONCAMION"].ToString() + Environment.NewLine;
        //        miro += filas["EMPRESA"].ToString() + Environment.NewLine;
        //        miro += filas["CLIENTEPROVEEDOR"].ToString() + Environment.NewLine;
        //        miro += filas["NOMBREFISCAL"].ToString() + Environment.NewLine;
        //        miro += filas["ARTICULO"].ToString() + Environment.NewLine;
        //        miro += filas["RUTA"].ToString() + Environment.NewLine;
        //        miro += filas["NUMERO"].ToString() + Environment.NewLine;
        //        miro += filas["UDSENCARGA"].ToString() + Environment.NewLine;

        //        createDiv.Attributes.Add("draggable", "true");
        //        createDiv.Attributes.Add("ondragstart", "drag(event)");
        //        createDiv.Attributes.Add("data-tooltip", miro);

        //        System.Web.UI.WebControls.Image imgPalet = new System.Web.UI.WebControls.Image();
        //        if (filas["UDSENCARGA"].ToString().Contains(","))
        //        {
        //            imgPalet.Attributes["src"] = "images/mediopalet200X300.png";
        //        }
        //        else
        //        {
        //            imgPalet.Attributes["src"] = "images/palet200X300.png";
        //        }

        //        imgPalet.Attributes["class"] = "pokemon";
        //        imgPalet.Visible = true;
        //        //imgPalet.onclick = "submitit()"
        //        createDiv.Controls.Add(imgPalet);





        //        System.Web.UI.WebControls.Label lbPalet = new System.Web.UI.WebControls.Label();
        //        lbPalet.ID = "lbPalet" + i;
        //        lbPalet.Text = filas["NUMERO_LINEA"].ToString()  + "-" + filas["POSICIONCAMION"].ToString();
        //        lbPalet.Visible = false;
        //        createDiv.Controls.Add(lbPalet);


        //        System.Web.UI.HtmlControls.HtmlGenericControl createDiv2 = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
        //        createDiv2.ID = "dragText" + i;
        //        createDiv2.Visible = true;
        //        createDiv2.Attributes["class"] = "centrado";
        //        createDiv2.InnerHtml = filas["NUMERO"].ToString() + "-" + filas["ARTICULO"].ToString();
        //        //createDiv2.Attributes["runat"] = "server";
        //        //createDiv2.ServerClick += btnEdit_Click;
        //        createDiv.Controls.Add(createDiv2);
        //        createDiv.Visible = true;

        //        createAnchor.Controls.Add(createDiv);

        //        //fuego.Controls.Add(createDiv);
        //        if (filas["ESTADO"].ToString() == "0" || filas["ESTADO"].ToString() == "1")
        //            container2.Controls.Add(createAnchor);
        //        else
        //        {
        //            if (fuego.Controls.Count > agua.Controls.Count)
        //            {
        //                agua.Controls.Add(createAnchor);
        //            }
        //            else if (agua.Controls.Count > fuego.Controls.Count)
        //            {
        //                fuego.Controls.Add(createAnchor);
        //            }
        //            else
        //            {
        //                fuego.Controls.Add(createAnchor);
        //            }
        //        }
        //        i += 1;
        //    }
        //    //this.Session["NumeroPalet"] = i.ToString();
        //}

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


        private void CreaPalets(DataTable dt)
        {
            int i = Convert.ToInt32(this.Session["NumeroPalet"].ToString());


            //Lberror.Visible = true;
            //Lberror.Text += "Numero de palets: " + i;

            if (i == 0) { return; }
            //container2.Controls.Clear();
            fuego.Controls.Clear();
            agua.Controls.Clear();
            i = 0;
            foreach (DataRow filas in dt.Rows)
            {
                string MiContent = "drag" + filas["POSICIONCAMION"].ToString(); //drag1
                HtmlGenericControl DivContent = (HtmlGenericControl)FindControlRecursive(container2, MiContent);
                //HtmlGenericControl DivContent = (HtmlGenericControl)FindControl(MiContent);
                if (DivContent is null) { break; }
                //string miro = "Pos.Camión:" + filas["POSICIONCAMION"].ToString() + "\n";
                //miro += "Línea:" + filas["NUMERO_LINEA"].ToString() + "\n";
                //miro += "Empresa:" + filas["EMPRESA"].ToString() + "\n";
                //miro += "Cliente:" + filas["CLIENTEPROVEEDOR"].ToString() + "\n";
                //miro += "Nombre:" + filas["NOMBREFISCAL"].ToString() + "\n";
                //miro += "Articulo:" + filas["ARTICULO"].ToString() + "\n";
                //miro += "Ruta:" + filas["RUTA"].ToString() + "\n";
                //miro += "Número:" + filas["NUMERO"].ToString() + "\n";
                //miro += "uni:" + filas["UDSENCARGA"].ToString() + "\n";

                string miro = filas["POSICIONCAMION"].ToString() + "\n";
                miro += filas["NUMERO_LINEA"].ToString() + "\n";
                miro += filas["EMPRESA"].ToString() + "\n";
                miro += filas["CLIENTEPROVEEDOR"].ToString() + "\n";
                miro += filas["NOMBREFISCAL"].ToString() + "\n";
                miro += filas["ARTICULO"].ToString() + "\n";
                miro += filas["RUTA"].ToString() + "\n";
                miro += filas["NUMERO"].ToString() + "\n";
                miro += filas["UDSENCARGA"].ToString() + "\n";

                DivContent.Attributes.Add("data-tooltip", miro);

                string MiImagen = "Imgdrag" + filas["POSICIONCAMION"].ToString(); // filas["NUMERO_LINEA"].ToString(); //Imgdrag1
                System.Web.UI.HtmlControls.HtmlImage Paletimg = (System.Web.UI.HtmlControls.HtmlImage)FindControlRecursive(container2, MiImagen);
                // System.Web.UI.WebControls.Image Paletimg = (System.Web.UI.WebControls.Image)FindControl(MiContent);

                if (filas["UDSENCARGA"].ToString().Contains("0."))
                {
                    Paletimg.Attributes["src"] = "images/mediopalet200X300.png";
                }
                else
                {
                    Paletimg.Attributes["src"] = "images/palet200X300.png";
                }

                string MiDivText = "dragText" + filas["POSICIONCAMION"].ToString(); // + filas["NUMERO_LINEA"].ToString(); //dragText1
                HtmlGenericControl DivText = (HtmlGenericControl)FindControlRecursive(container2, MiDivText);
                //HtmlGenericControl DivText = (HtmlGenericControl)FindControl(MiDivText);
                DivText.InnerHtml = filas["NUMERO"].ToString() + "-" + filas["ARTICULO"].ToString();
                //DivText.InnerHtml = filas["NUMERO"].ToString() + "< br/>" + filas["ARTICULO"].ToString();

                DivContent.Visible = true;
                if (filas["ESTADO"].ToString() == "0" || filas["ESTADO"].ToString() == "1")
                {
                }
                else
                {
                    HtmlGenericControl Mfuego = (HtmlGenericControl)FindControlRecursive(idPadre, "fuego");
                    HtmlGenericControl Magua = (HtmlGenericControl)FindControlRecursive(idPadre, "agua");

                    //if (Mfuego.Controls.Count > Magua.Controls.Count)
                    //{
                    //    Magua.Controls.Add(DivContent);
                    //}
                    //else if (agua.Controls.Count > Mfuego.Controls.Count)
                    //{
                    //    Mfuego.Controls.Add(DivContent);
                    //}
                    //else
                    //{
                    //    Mfuego.Controls.Add(DivContent);
                    //}
                    //al revés
                    if (Magua.Controls.Count > Mfuego.Controls.Count)
                    {
                        Mfuego.Controls.Add(DivContent);
                    }
                    else if (Mfuego.Controls.Count > Magua.Controls.Count)
                    {
                        Magua.Controls.Add(DivContent);
                    }
                    else
                    {
                        Magua.Controls.Add(DivContent);
                    }
                }
            }

            //Busca Error
            Lberror.Text = "";
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }

        protected void MuevePalet_Click(object sender, EventArgs e)
        {
            Button nombre = (Button)sender;
            string Id = nombre.ID;
            string Parent = nombre.Parent.ID.ToString();
            string Miro = LbPosicionCamion.Text;
            HtmlGenericControl DivText = (HtmlGenericControl)FindControl(Parent);
            Parent = DivText.Parent.ID;

            string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
            SQL += " WHERE ID_CABECERA = " + TxtNumero.Text;
            SQL += " AND POSICIONCAMION = " + Id.Replace("Btndrag", "");
            Lberror.Text += SQL + "1- gvCabecera_Selecciona " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 1- gvCabecera_Selecciona " + Variables.mensajeserver;
            gvLista.DataSource = dt;
            gvLista.DataBind();
            this.Session["NumeroPalet"] = dt.Rows.Count.ToString();

            Vista_Print(dt, Id.Replace("Btndrag", ""));
        }

        private void Limpia_cajas()
        {
            DLbLote0.Text = "";
            DLbOrdenCarga0.Text = "";
            DLbPosCamion0.Text = "";
            DlbCliente0.Text = "";
            DLbVariedad0.Text = "";
            DLbEmpresa0.Text = "";
            DLbNumerPlanta0.Text = "";
        }

        private void Vista_Print(DataTable dt, string ID)
        {
            //Unificar si se puede porque es igual que CargaTodaLista()
            CargaTodaLista(ID);
            return;

            //string EtiquetaQR = "";
            //string EtiquetaUD_BASE = "";

            //foreach (DataRow filas in dt.Rows)
            //{
            //    if (filas["NUMERO_LINEA"].ToString() == ID)
            //    {
            //        //Bug Codigo QR si termina en cero hay que añadir un retorno de carro o no se lee
            //        if (filas["POSICIONCAMION"].ToString().Contains("0"))
            //        {
            //            DLbLote0.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString() + Environment.NewLine;
            //        }
            //        else
            //        {
            //            DLbLote0.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString();
            //        }
            //        DLbOrdenCarga0.Text = "Orden Carga: " + filas["ID_CABECERA"].ToString();
            //        DLbPosCamion0.Text = "Posición Camión: " + filas["POSICIONCAMION"].ToString();
            //        DlbCliente0.Text = "Cliente: " + filas["NOMBREFISCAL"].ToString();  //filas["CLIENTEPROVEEDOR"].ToString();
            //        string N = "";
            //        string SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is null)
            //        {
            //            DLbVariedad0.Text = "Variedad: ";
            //        }
            //        else
            //        {
            //            DLbVariedad0.Text = "Variedad: " + Con.ToString();
            //        }

            //        SQL = "SELECT TOP 1 (A.ZVARIEDAD) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is null)
            //        {
            //            EtiquetaQR = "NO EXISTE ";
            //        }
            //        else
            //        {
            //            EtiquetaQR =  Con.ToString();
            //        }

            //        N = "";
            //        SQL = "SELECT TOP 1 (A.ZEMPRESA) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Con = DBHelper.ExecuteScalarSQL(SQL, null);

            //        if (Con is null)
            //        {
            //            DLbEmpresa0.Text = "NO EXISTE";
            //        }
            //        else
            //        {
            //            if (Con.ToString().Contains("VIVA"))
            //            {

            //                DLbEmpresa0.Text = "Viveros Valsaín, SLU";
            //            }
            //            else
            //            {
            //                DLbEmpresa0.Text = "Viveros Río Eresma, SLU";
            //            }
            //        }


            //        SQL = "SELECT TOP 1 (B.UD_BASE) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Con = DBHelper.ExecuteScalarSQL(SQL, null);

            //        if (Con is null)
            //        {
            //            EtiquetaUD_BASE = "";
            //        }
            //        else
            //        {
            //            EtiquetaUD_BASE = Con.ToString();
            //        }





            //        //Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
            //        //DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        //string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".",","), ",");
            //        //if (CadaLinea.Count() > 0)
            //        //{
            //        //    if (CadaLinea[1].Length < 3)
            //        //    {
            //        //        for (int a = CadaLinea[1].Length; a < 4; a++)
            //        //        {
            //        //            CadaLinea[1] += "0";
            //        //        }
            //        //    }
            //        //    Double Value = Convert.ToDouble(CadaLinea[1].ToString());
            //        //    DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        //}
            //        //else
            //        //{
            //        //    Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
            //        //    DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        //}


            //        string miro = filas["UDSENCARGA"].ToString().Replace(".", ",");
            //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
            //        if (CadaLinea.Count() > 1)
            //        {
            //            if (CadaLinea[1].Length < 3)
            //            {
            //                for (int a = CadaLinea[1].Length; a < 3; a++)
            //                {
            //                    CadaLinea[1] += "0";
            //                }
            //            }
            //            else
            //            {
            //                CadaLinea[1] = CadaLinea[1].Substring(0, 3);
            //            }

            //            if (EtiquetaUD_BASE != "")
            //            {
            //                string[] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
            //                int Value = Convert.ToInt32(AA[0]);
            //                DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
            //                //Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString()); // * Convert.ToDouble(EtiquetaUD_BASE);
            //                //DLbNumerPlanta0.Text = "Cantidad:: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
            //            }
            //            else
            //            {
            //                Double Value = Convert.ToDouble(CadaLinea[0].ToString() + "." + CadaLinea[1].ToString());
            //                DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //            }
            //        }
            //        else
            //        {
            //            if (CadaLinea[0].Length < 3)
            //            {
            //                CadaLinea[0] += ".";
            //                for (int a = 1; a < 4; a++)
            //                {
            //                    CadaLinea[0] += "0";
            //                }
            //            }
            //            else
            //            {
            //                CadaLinea[0] = CadaLinea[0].Substring(0, 3);
            //            }


            //            if (EtiquetaUD_BASE != "")
            //            {
            //                string[] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
            //                int Value = Convert.ToInt32(AA[0]);
            //                DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;

            //                //Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());// * Convert.ToDouble(EtiquetaUD_BASE);
            //                //DLbNumerPlanta0.Text = "Cantidad:: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
            //            }
            //            else
            //            {
            //                Double Value = Convert.ToDouble(CadaLinea[0].ToString());
            //                DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //            }
            //        }



            //        btnGenerate_Click(DLbLote0, PlaceHolderGR0);
            //        btnGenerateTodo_Click(EtiquetaQR, PlaceHolderMIN0);
            //        break;
            //    }
            //}
            //return;
        }

        private void EliminaQRs()
        {
        }

        private void CargaTodaLista(string ID)
        {

            string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
            SQL += " WHERE ID_CABECERA = " + TxtNumero.Text + " ";
            if (ID != "")
            {
                SQL += " AND NUMERO_LINEA = " + ID + " ";
            }
            SQL += " ORDER BY POSICIONCAMION ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            int i = 0;
            string EtiquetaQR = "";
            string EtiquetaUD_BASE = "";
            string TipoPlantaQR = "";




            foreach (DataRow filas in dt.Rows)
            {
                if (i == 0)
                {
                    //divArray0
                    //Bug Codigo QR si termina en cero hay que añadir un retorno de carro o no se lee
                    if (filas["POSICIONCAMION"].ToString().Contains("0"))
                    {
                        DLbLote0.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString() + Environment.NewLine;
                    }
                    else
                    {
                        DLbLote0.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString();
                    }
                    DLbOrdenCarga0.Text = "Orden Carga: " + filas["ID_CABECERA"].ToString();
                    DLbPosCamion0.Text = "Posición Camión: " + filas["POSICIONCAMION"].ToString();
                    DlbCliente0.Text = "Cliente: " + filas["NOMBREFISCAL"].ToString(); // filas["CLIENTEPROVEEDOR"].ToString();

                    //Alternativa
                    //Consulta con una SELECT el comportamiento de la falta de una campo no es del todo correcta
                    //SQL = "SELECT A.ZDESCRIPCION, B.UD_BASE, B.ZTIPO_PLANTA, A.ZVARIEDAD, A.ZEMPRESA, ";
                    //SQL += " FROM ZEMPRESAVARIEDAD A ";
                    //SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    //SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    //SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    //SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    //SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();

                    //DataTable dtConsulta = Main.BuscaLote(SQL).Tables[0];

                    //foreach (DataRow filaconsulta in dtConsulta.Rows)
                    //{
                        //Para cada campo
                        //if (filaconsulta["ZDESCRIPCION"].ToString() == "" || filaconsulta["ZDESCRIPCION"].ToString() == null)
                        //{
                        //    DLbVariedad0.Text = "NO EXISTE DESCRIPCION";
                        //    Lbmensaje.Text = "No se encuentra la descripción de la Variedad " + filas["VARIEDAD"].ToString() + " en la tabla ZEMPRESAVARIEDAD para relacionar el campo ZCODGOLDEN  en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        //    cuestion.Visible = false;
                        //    Asume.Visible = true;
                        //    windowmessaje.Visible = true;
                        //}
                        //else
                        //{
                        //    DLbVariedad0.Text = "Variedad: " + Con;
                        //}
                    //}




                    //
                    //string N = "";
                    SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();

                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is System.DBNull)
                    {
                        DLbVariedad0.Text = "NO EXISTE DESCRIPCION";
                        Lbmensaje.Text = "No se encuentra la descripción de la Variedad " + filas["VARIEDAD"].ToString() + " en la tabla ZEMPRESAVARIEDAD para relacionar el campo ZCODGOLDEN  en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        DLbVariedad0.Text = "Variedad: " + Con;
                    }

                    SQL = "SELECT TOP 1 (B.UD_BASE) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        DLbVariedad0.Text = "NO EXISTE UD_BASE";
                        Lbmensaje.Text = "No se encuentra UD_BASE de la Tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZCARGA_LINEA.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        EtiquetaUD_BASE = Con.ToString();
                    }

                    TipoPlantaQR = "";
                    SQL = "SELECT TOP 1 (B.ZTIPO_PLANTA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        Lbmensaje.Text = "No se encuentra ZTIPO_PLANTA de la Tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        TipoPlantaQR = Con.ToString();
                    }

                    SQL = "SELECT TOP 1 (A.ZVARIEDAD) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        DLbVariedad0.Text = "NO EXISTE VARIEDAD";
                        Lbmensaje.Text = "No se encuentra ZVARIEDAD de la Tabla ZEMPRESAVARIEDAD relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        EtiquetaQR = Con.ToString();
                    }

                    //N = "";
                    SQL = "SELECT TOP 1 (A.ZEMPRESA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        DLbEmpresa0.Text = "NO EXISTE EMPRESA";
                        Lbmensaje.Text = "No se encuentra ZEMPRESA de la Tabla ZEMPRESAVARIEDAD relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        if (Con.ToString().Contains("VIVA"))
                        {

                            DLbEmpresa0.Text = "Viveros Valsaín, SLU";
                        }
                        else
                        {
                            DLbEmpresa0.Text = "Viveros Río Eresma, SLU";
                        }
                    }
                    //Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
                    //DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                    string miro = filas["UDSENCARGA"].ToString();//.Replace(".", ",");
                    //Unidades por mil
                    Double Value = 0;

                    string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
                    if (CadaLinea.Count() > 1)
                    {
                        if (CadaLinea[1].Length < 3)
                        {
                            for (int a = CadaLinea[1].Length; a < 3; a++)
                            {
                                CadaLinea[1] += "0";
                            }
                        }
                        else
                        {
                            CadaLinea[1] = CadaLinea[1].Substring(0, 3);
                        }
                        Value = Convert.ToDouble(CadaLinea[0].ToString() + "." + CadaLinea[1].ToString());
                        //DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";//"Número Plantas:"
                        DLbNumerPlanta0.Text = Value.ToString("N0") ;//"Número Plantas:"
                    }
                    else
                    {
                        Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
                        //DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";//"Número Plantas:"
                        DLbNumerPlanta0.Text = Value.ToString("N0") ;//"Número Plantas:"
                    }
                    if (EtiquetaUD_BASE != "")
                    {
                        Object Dato = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (FACT_DIV) FROM  ZUD_BASE  WHERE UD_BASE ='" + EtiquetaUD_BASE + "' ", null);
                        if (Dato is null)
                        {
                            //DLbNumerPlanta0.Text = Value.ToString("N0") + " " + EtiquetaUD_BASE;//"Número Plantas:"
                            Lbmensaje.Text = "No se encuentra registro de la Tabla ZUD__BASE relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                            cuestion.Visible = false;
                            Asume.Visible = true;
                            windowmessaje.Visible = true;
                            MiCloseMenu();

                        }
                        else
                        {
                            DLbNumerPlanta0.Text = Convert.ToDouble(Value / Convert.ToInt32(Dato)).ToString("N0") + " " + EtiquetaUD_BASE; // / 1000 + " " + Unidad_Base;
                        }
                    }
                    Update_listaHasta(DLbLote0.Text + Environment.NewLine, filas["ID"].ToString(), filas["NUMERO_LINEA"].ToString());
                    btnGenerate_Click(DLbLote0, PlaceHolderGR0);
                    btnGenerateTodo_Click(EtiquetaQR, PlaceHolderMIN0);
                }
                else
                {
                    string MiDiv = "divArray" + i;
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    createDiv.ID = MiDiv;
                    createDiv.Attributes["class"] = "panel panel-default";
                    createDiv.Attributes["style"] = "display:inline-block; border-style:none; width:100%; font-weight: bold; font-size:20px;";
                    createDiv.Controls.Add(new LiteralControl("<br/>"));


                    MiDiv = "divLabel" + i;
                    System.Web.UI.HtmlControls.HtmlGenericControl divLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    divLabel.ID = MiDiv;
                    divLabel.Attributes["class"] = "col-lg-12";

                    MiDiv = "divMIN" + i;
                    System.Web.UI.HtmlControls.HtmlGenericControl divMIN = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    divMIN.ID = MiDiv;
                    divMIN.Attributes["class"] = "col-lg-12";

                    PlaceHolder QR = new PlaceHolder();
                    QR.ID = "PlaceHolderGR" + i;

                    PlaceHolder QRMin = new PlaceHolder();
                    QRMin.ID = "PlaceHolderMIN" + i;

                    Label lbEmpresa = new Label();
                    lbEmpresa.ID = "DLbEmpresa" + i;
                    lbEmpresa.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbLote = new Label();
                    lbbLote.ID = "DLbLote" + i;
                    lbbLote.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbOrden = new Label();
                    lbbOrden.ID = "DLbOrdenCarga" + i;
                    lbbOrden.Attributes["style"] = " display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbPosCamion = new Label();
                    lbbPosCamion.ID = "DLbPosCamio" + i;
                    lbbPosCamion.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbCliente = new Label();
                    lbbCliente.ID = "DlbCliente" + i;
                    lbbCliente.Attributes["style"] = "display:inline-block; width:40%; font-weight: bold; font-size:20px;";

                    Label lbbVariedad = new Label();
                    lbbVariedad.ID = "DLbVariedad" + i;
                    lbbVariedad.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbEmpresa = new Label();
                    lbbEmpresa.ID = "DLbEmpresa" + i;
                    lbbEmpresa.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    Label lbbNumerPlanta = new Label();
                    lbbNumerPlanta.ID = "DLbNumerPlanta" + i;
                    lbbNumerPlanta.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

                    //Bug en QRCoder Codigo QR si termina en cero hay que añadir un retorno de carro o no se lee
                    if (filas["POSICIONCAMION"].ToString().Contains("0"))
                    {
                        lbbLote.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString() + Environment.NewLine;
                    }
                    else
                    {
                        lbbLote.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString();
                    }
                    lbbOrden.Text = "Orden Carga: " + filas["ID_CABECERA"].ToString();
                    lbbPosCamion.Text = "Posición Camión: " + filas["POSICIONCAMION"].ToString();
                    lbbCliente.Text = "Cliente: " + filas["NOMBREFISCAL"].ToString(); // filas["CLIENTEPROVEEDOR"].ToString();

                    //string N = "";
                    SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        lbbVariedad.Text = "NO EXISTE DESCRIPCION";
                        Lbmensaje.Text = "No se encuentra ZDESCRIPCION de la Tabla ZEMPRESAVARIEDAD relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        lbbVariedad.Text = "Variedad: " + Con.ToString();
                    }

                    SQL = "SELECT TOP 1 (B.UD_BASE) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        lbbVariedad.Text = "NO EXISTE UD_BASE";
                        Lbmensaje.Text = "No se encuentra UD_BASE de la Tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN relacionando ZCODGOLDEN con ARTICULO " + filas["ARTICULO"].ToString() + " en la tabla ZCARGA_LINEA.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        EtiquetaUD_BASE = Con.ToString();
                    }


                    SQL = "SELECT TOP 1 (A.ZVARIEDAD) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        lbbVariedad.Text = "NO EXISTE VARIEDAD";
                        Lbmensaje.Text = "No se encuentra ZVARIEDAD de la Tabla ZEMPRESAVARIEDAD relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        EtiquetaQR = Con.ToString();
                    }

                    TipoPlantaQR = "";
                    SQL = "SELECT TOP 1 (B.ZTIPO_PLANTA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        Lbmensaje.Text = "No se encuentra ZTIPO_PLANTA de la Tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        TipoPlantaQR = Con.ToString();
                    }

                    //N = "";
                    SQL = "SELECT TOP 1 (A.ZEMPRESA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    SQL += " AND C.ID_CABECERA = " + filas["ID_CABECERA"].ToString();
                    SQL += " AND C.NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString();
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        lbbEmpresa.Text = "NO EXISTE EMPRESA";
                        Lbmensaje.Text = "No se encuentra ZEMPRESA de la Tabla ZEMPRESAVARIEDAD relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                        cuestion.Visible = false;
                        Asume.Visible = true;
                        windowmessaje.Visible = true;
                        MiCloseMenu();

                    }
                    else
                    {
                        if (Con.ToString().Contains("VIVA"))
                        {

                            lbbEmpresa.Text = "Viveros Valsaín, SLU";
                            this.Session["Centro"] = "VIVA";
                        }
                        else
                        {
                            lbbEmpresa.Text = "Viveros Río Eresma, SLU";
                            this.Session["Centro"] = "VRE";
                        }
                    }

                    string miro = filas["UDSENCARGA"].ToString().Replace(".", ",");
                    Double Value = 0;
                    string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
                    if (CadaLinea.Count() > 1)
                    {
                        if (CadaLinea[1].Length < 3)
                        {
                            for (int a = CadaLinea[1].Length; a < 3; a++)
                            {
                                CadaLinea[1] += "0";
                            }
                        }
                        else
                        {
                            CadaLinea[1] = CadaLinea[1].Substring(0, 3);
                        }
                        miro = CadaLinea[0] + "." + CadaLinea[1];

                        Value = Convert.ToDouble(miro);
                        //lbbNumerPlanta.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS"; //Número Plantas: 
                        lbbNumerPlanta.Text = Value.ToString("N0") ;//"Número Plantas:"
                    }
                    else
                    {
                        Value = Convert.ToDouble(filas["UDSENCARGA"].ToString()); // * 1000;
                        //lbbNumerPlanta.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";//Número Plantas: 
                        lbbNumerPlanta.Text = Value.ToString("N0") ;//"Número Plantas:"
                    }

                    if (EtiquetaUD_BASE != "")
                    {
                        Object Dato = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (FACT_DIV) FROM  ZUD_BASE  WHERE UD_BASE ='" + EtiquetaUD_BASE + "' ", null);
                        if (Dato is null)
                        {
                            Lbmensaje.Text = "No se encuentra registro de la Tabla ZUD__BASE relacionando ZCODGOLDEN con Articulo " + filas["ARTICULO"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
                            cuestion.Visible = false;
                            Asume.Visible = true;
                            windowmessaje.Visible = true;
                            MiCloseMenu();

                        }
                        else
                        {
                            lbbNumerPlanta.Text = Convert.ToDouble(Value / Convert.ToInt32(Dato)).ToString("N0") + " " + EtiquetaUD_BASE; // / 1000 + " " + Unidad_Base;
                        }
                    }

                    Update_listaHasta(lbbLote.Text + Environment.NewLine, filas["ID"].ToString(), filas["NUMERO_LINEA"].ToString());

                    btnGenerate_Click(lbbLote, QR);
                    btnGenerateTodo_Click(EtiquetaQR, QRMin);
                    //divArray0
                    createDiv.Controls.Add(lbbEmpresa);
                    createDiv.Controls.Add(lbbLote);

                    //QR Placeholder
                    createDiv.Controls.Add(QR);

                    //divLabel0
                    divLabel.Controls.Add(lbbOrden);
                    divLabel.Controls.Add(lbbPosCamion);
                    divLabel.Controls.Add(lbbCliente);
                    divLabel.Controls.Add(lbbVariedad);
                    divLabel.Controls.Add(lbbNumerPlanta);

                    //divMIN0
                    divMIN.Controls.Add(QRMin);

                    //divArray0
                    createDiv.Controls.Add(divLabel);
                    createDiv.Controls.Add(divMIN);

                    //Al contenedor
                    pnlContents2.Controls.Add(createDiv);
                }
                i += 1;
            }
        }

        private void Update_listaHasta(string Dato, string ID, string Numero_linea)
        {
            string SQL = " UPDATE ZCARGA_LINEA ";
            SQL += " SET HASTA ='" + Dato + "'";
            SQL += " WHERE ID = " + ID;
            SQL += " AND NUMERO_LINEA = " + Numero_linea;

            DBHelper.ExecuteNonQuery(SQL);
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

            //try
            //{
            //    imgBarCode.Height = 200; // 250;
            //    imgBarCode.Width = 200; // 250;
            //}
            //catch (Exception a)
            //{
            //    string b = a.Message;
            //    imgBarCode.Height = 200; // 250;
            //    imgBarCode.Width = 200; // 250;
            //}
            //using (Bitmap bitMap = qrCode.GetGraphic(40))
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();
            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //    Contenedor.Controls.Add(imgBarCode);
            //}
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


            //try
            //{
            //    imgBarCode.Height = 100;
            //    imgBarCode.Width = 100;
            //}
            //catch (Exception a)
            //{
            //    string b = a.Message;
            //}

            //using (Bitmap bitMap = qrCode.GetGraphic(40))
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();
            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //    Contenedor.Controls.Add(imgBarCode);
            //}
        }

        private void DrPrinters_Click()
        {
            //btnPrintA1.Visible = false;
        }

        private void Carga_tablaNueva()
        {
            string temporal = ""; //Jose
            //Lberror.Text = "";
            string SQL = "";
            string Filtros = "";
            DataTable dt = null;
            DataTable dtV = null;
            //DataTable dtV2 = null;
            try
            {
                //
                //Petición no aplicar filtros a la consulta general y eliminarlos
                //
                //Carga_tablaListaFiltro();
                SQL = ""; // this.Session["Filtro"].ToString();
                Filtros = ""; // = this.Session["Filtro"].ToString();
                Carga_Filtros();
                //
                if (Filtros == "")
                {
                    if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                    {
                        //01/10/2024
                        //Las tablas implicadas son ZBANDEJAS, ZTIPOPLANTADESCRIP, ZPLANTA_TIPO_VARIEDAD_CODGOLDEN
                        // Ojo, solo buscamos por el dato CAJAS en la condición sobre ZBANDEJAS en ZTIPO_FORMATO


                        //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        SQL = "1- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM ZCARGA_ORDEN ";
                        DBHelper.DeleteProcedureTemp("");

                        SQL = Main.BuscaGold("", "");
                        Lberror.Text += SQL + "2- Carga_tablaNueva BuscaGold " + Variables.mensajeserver;
                        dtV = Main.BuscaLoteGold(SQL).Tables[0];

                        Lberror.Text += SQL + " 2- termina Carga_tablaNueva BuscaLoteGold " + Variables.mensajeserver;

                        foreach (DataRow fila in dtV.Rows)
                        {
                            SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                            SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                            SQL += fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " INNER JOIN ZBANDEJAS C ";
                            SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
                            SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
                            SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";
                            Lberror.Text += "3- Carga_tablaNueva " + SQL + Environment.NewLine;

                            DBHelper.ExecuteNonQuery(SQL);
                        }


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
                        SQL += " FROM ZCARGA_ORDEN  WHERE ESTADO <> 3  ";




                        ////SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
                        //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        //SQL += " CONVERT(DECIMAL(18, 3), UDSPEDIDAS) AS UDSPEDIDAS, CONVERT(DECIMAL(18, 3), UDSSERVIDAS) AS UDSSERVIDAS, CONVERT(DECIMAL(18, 3), UDSENCARGA) AS UDSENCARGA, CONVERT(DECIMAL(18, 3), UDSPENDIENTES) AS UDSPENDIENTES, ";
                        //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                        //SQL += " CASE ";
                        //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                        //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                        //SQL += " END AS UDSACARGAR, ";
                        //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                        //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";


                        //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
                        Lberror.Text += "4- Carga_tablaNueva " + SQL + Environment.NewLine;

                        dt = Main.BuscaLote(SQL).Tables[0];
                        Lberror.Text += "4- Carga_tablaNueva " + SQL + Environment.NewLine;
                    }
                    else
                    {
                        //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        if (temporal == "")
                        {

                            SQL = "5- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM ZCARGA_ORDEN ";
                            DBHelper.DeleteProcedureTemp("");

                            SQL = Main.BuscaGold("", "");
                            Lberror.Text = "6- Carga_tablaNueva BuscaLoteGold" + Environment.NewLine;
                            dtV = Main.BuscaLoteGold(SQL).Tables[0];

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                                SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " INNER JOIN ZBANDEJAS C ";
                                SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
                                SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
                                SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";
                                Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;

                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
                        }

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
                        SQL += " FROM ZCARGA_ORDEN  WHERE ESTADO <> 3  ";



                        //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                        //SQL += " CASE ";
                        //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                        //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                        //SQL += " END AS UDSACARGAR, ";
                        //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                        //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";

                        //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                        //SQL += " CASE ";
                        //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
                        //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                        //SQL += " END AS UDSACARGAR, ";
                        //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO ";
                        //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3 ";
                        //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
                        Lberror.Text += "8- Carga_tablaNueva " + SQL + Environment.NewLine;

                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                }
                else //Si tiene filtros
                {
                    if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                    {
                        //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN ";
                        Lberror.Text += "9-  DeleteProcedureTemp " + SQL + Environment.NewLine;
                        DBHelper.DeleteProcedureTemp("");

                        string Miro = "";
                        if (this.Session["FiltroFecha"].ToString() != "")
                        {
                            Miro += " AND " + this.Session["FiltroFecha"].ToString();
                        }
                        if (this.Session["FiltroRuta"].ToString() != "")
                        {
                            Miro += " AND " + this.Session["FiltroRuta"].ToString();
                        }
                        if (this.Session["FiltroCliente"].ToString() != "")
                        {
                            Miro += " AND " + this.Session["FiltroCliente"].ToString();
                        }
                        if (this.Session["FiltroEmpresa"].ToString() != "")
                        {
                            Lberror.Text += "10- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
                            SQL = Main.BuscaGold(this.Session["FiltroEmpresa"].ToString(), Miro);
                            Lberror.Text += SQL + " 10- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
                        }
                        else
                        {
                            Lberror.Text = "11- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
                            SQL = Main.BuscaGold("", Miro);
                            Lberror.Text = SQL + "11- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
                        }
                        dtV = Main.BuscaLoteGold(SQL).Tables[0];

                        foreach (DataRow fila in dtV.Rows)
                        {
                            SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString() + "," + fila["UDSPENDIENTES"].ToString() + ",";
                            SQL += fila["UDSACARGAR"].ToString() + "," + fila["NUMPALET"].ToString() + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                            SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                            SQL += fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " INNER JOIN ZBANDEJAS C ";
                            SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
                            SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
                            SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";


                            Lberror.Text += "12- Carga_tablaNueva " + SQL + Environment.NewLine;

                            DBHelper.ExecuteNonQuery(SQL);
                        }

                        //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN " + Filtros + " AND  ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";

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
                        SQL += " FROM ZCARGA_ORDEN  WHERE ESTADO <> 3  ";



                        //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                        //SQL += " CASE ";
                        //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                        //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                        //SQL += " END AS UDSACARGAR, ";
                        //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                        //SQL += " ZCARGA_ORDEN  WHERE ESTADO <> 3  ";
                        //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
                        Lberror.Text += "13- Carga_tablaNueva " + SQL + Environment.NewLine;
                        dt = Main.BuscaLote(SQL).Tables[0];

                    }
                    else
                    {

                        //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        if (temporal == "")
                        {
                            SQL += "DELETE FROM ZCARGA_ORDEN ORDER BY ID";
                            Lberror.Text += "14- DeleteProcedureTemp " + SQL + Environment.NewLine;

                            DBHelper.DeleteProcedureTemp("");

                            string Miro = "";
                            if (this.Session["FiltroFecha"].ToString() != "")
                            {
                                Miro += " AND " + this.Session["FiltroFecha"].ToString();
                            }
                            if (this.Session["FiltroRuta"].ToString() != "")
                            {
                                Miro += " AND " + this.Session["FiltroRuta"].ToString();
                            }
                            if (this.Session["FiltroCliente"].ToString() != "")
                            {
                                Miro += " AND " + this.Session["FiltroCliente"].ToString();
                            }
                            if (this.Session["FiltroEmpresa"].ToString() != "")
                            {
                                Lberror.Text += "15- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
                                SQL = Main.BuscaGold(this.Session["FiltroEmpresa"].ToString(), Miro);
                                Lberror.Text += SQL + " 15- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
                            }
                            else
                            {
                                Lberror.Text += "16- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
                                SQL = Main.BuscaGold("", Miro);
                                Lberror.Text += SQL + " 16- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
                            }

                            dtV = Main.BuscaLoteGold(SQL).Tables[0];

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                                SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " INNER JOIN ZBANDEJAS C ";
                                SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
                                SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
                                SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";


                                Lberror.Text += "17- Carga_tablaNueva " + SQL + Environment.NewLine;

                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN " + Filtros + " AND ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
                        }

                        SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        SQL += " CONVERT(DECIMAL(18, 3), UDSPEDIDAS) AS UDSPEDIDAS, CONVERT(DECIMAL(18, 3), UDSSERVIDAS) AS UDSSERVIDAS, CONVERT(DECIMAL(18, 3), UDSENCARGA) AS UDSENCARGA, CONVERT(DECIMAL(18, 3), UDSPENDIENTES) AS UDSPENDIENTES, ";
                        SQL += " CASE ";
                        SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                        SQL += " WHEN ESTADO = 1 THEN CONVERT(DECIMAL(18, 3), UDSACARGAR) ";
                        SQL += " END AS UDSACARGAR, ";
                        SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                        SQL += " FROM ZCARGA_ORDEN  WHERE ESTADO <> 3  ";

                        //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
                        //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
                        //SQL += " CASE ";
                        //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 3), UDSPEDIDAS) - CONVERT(DECIMAL(18, 3), UDSSERVIDAS) - CONVERT(DECIMAL(18, 3), UDSENCARGA)) ";
                        //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
                        //SQL += " END AS UDSACARGAR, ";
                        //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
                        //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";
                        ////SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN ORDER BY ID ";
                        Lberror.Text += "18- Carga_tablaNueva " + SQL + Environment.NewLine;

                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                }

                //busca Error donde no se puede depurar
                //Lberror.Text += " Mirar: " + Variables.Error + " " + SQL;
                //Calcula con lo que tiene en Lista de carga camión
                dt = Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), TxtNumero.Text);

                this.Session["MiConsulta"] = dt;

                gvControl.DataSource = dt;
                gvControl.DataBind();

                //busca Error donde no se puede depurar
                //Lberror.Visible = true;

                Lberror.Text = "";
            }
            catch (Exception mm)
            {
                Variables.Error += mm.Message;
                Lberror.Visible = true;
                Lberror.Text += ". Error: " + mm.Message;
            }


        }

        private static DataTable Calcula_OrdenesCarga(DataTable dt, string Estado, string Numero)
        {
            string SQL = "";
            try
            {
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
                    SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
                    SQL += " FROM ZCARGA_LINEA  WHERE ESTADO < 3  ";
                    if(Numero == "")
                    {
                        SQL += "AND ID_CABECERA = 0 ";
                    }
                    else
                    {
                        SQL += "AND ID_CABECERA = " + Numero; 
                    }
                    
                    SQL += "  ORDER BY NUMERO_LINEA ";
                }
                else
                {
                    SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
                    SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
                    SQL += " FROM ZCARGA_LINEA  WHERE ESTADO < 3  ";

                    if (Numero == "")
                    {
                        SQL += "AND ID_CABECERA = 0 ";
                    }
                    else
                    {
                        SQL += "AND ID_CABECERA = " + Numero;
                    }

                    SQL += " ORDER BY NUMERO_LINEA ";
                }

                DataTable ds = Main.BuscaLote(SQL).Tables[0];

                dt.AcceptChanges();
                foreach (DataRow fila in dt.Rows)
                {
                    Boolean Esta = false;
                    decimal rUDSENCARGA = 0.0M;

                    foreach (DataRow fila2 in ds.Rows)
                    {
                        if (fila["NUMERO"].ToString() == fila2["NUMERO"].ToString() && fila["LINEA"].ToString() == fila2["LINEA"].ToString() &&
                            fila["EMPRESA"].ToString() == fila2["EMPRESA"].ToString() && fila["CLIENTEPROVEEDOR"].ToString() == fila2["CLIENTEPROVEEDOR"].ToString())
                        {
                            if (Estado != "3")
                            {
                                Esta = true;
                                rUDSENCARGA += Convert.ToDecimal(fila2["UDSENCARGA"].ToString().Replace(".", ","));
                            }
                            else
                            {
                                fila["UDSENCARGA"] = "0,00";
                            }
                        }
                    }
                    if (Esta == true)
                    {
                        fila["UDSENCARGA"] = rUDSENCARGA;
                        fila["UDSPENDIENTES"] = "0"; // Convert.ToDecimal(fila["UDSPEDIDAS"].ToString().Replace(".", ",")) - (Convert.ToDecimal(fila["UDSSERVIDAS"].ToString().Replace(".", ",")) + rUDSENCARGA);
                        string miro = fila["NUMPALET"].ToString();
                        if (miro != "0,00")
                        {
                        }
                        else
                        {
                            fila["UDSACARGAR"] = Convert.ToDecimal(fila["UDSPEDIDAS"].ToString().Replace(".", ",")) - (Convert.ToDecimal(fila["UDSSERVIDAS"].ToString().Replace(".", ",")) + rUDSENCARGA);  //fila["UDSPENDIENTES"].ToString();
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                Variables.Error = SQL + " " + Ex.Message;
            }
            return dt;
        }

        protected void BtGuardaVariedad_Click(object sender, EventArgs e)
        {
            string SQL = "UPDATE ZBANDEJAS SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZTIPO_FORMATO ='" + TextTipoFormato.Text + "', ZNUMERO_PLANTAS ='" + TxtNumPlanta.Text + "' WHERE ZID = " + this.Session["GVBandejaID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "UPDATE ZPLANTA_TIPO_VARIEDAD_CODGOLDEN SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZVARIEDAD ='" + TxtVariedad.Text + "' , ZCODGOLDEN ='" + TxtCodGolden.Text + "', UD_BASE = '" + TxtUdBase.Text + "' WHERE ZID = " + this.Session["GVGoldenID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "UPDATE ZTIPOPLANTADESCRIP SET ZTIPO_PLANTA ='" + TxtTipoPlanta.Text + "', ZDESCRIPTIPO = '" + TxtDTipoPlanta.Text + "' WHERE ZID = " + this.Session["GVVariedadID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

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

            BtModificaVariedad.Visible = true;
            btNuevaVariedad.Visible = false;
            BtGuardaVariedad.Visible = false;
            BtCancelaVariedad.Enabled = false;
            BtDeleteVariedad.Enabled = false;


        }
        protected void btnDeleteVariedad_Click(object sender, EventArgs e)
        {
            string SQL = "DELETE FROM ZBANDEJAS WHERE ZID = " + this.Session["GVBandejaID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "DELETE FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN WHERE ZID = " + this.Session["GVGoldenID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "DELETE FROM ZTIPOPLANTADESCRIP WHERE ZID = " + this.Session["GVVariedadID"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            Carga_Tablas_Variedades();

            Variables.MiColor = "#ffffff";
            TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
            TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
            TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
            TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
            TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);

        }
        protected void btnModificaVariedad_Click(object sender, EventArgs e)
        {
            BtModificaVariedad.Visible = true;
            btNuevaVariedad.Visible = false;
            BtGuardaVariedad.Visible = true;
            BtCancelaVariedad.Enabled = true;
            BtDeleteVariedad.Enabled = true;

            Variables.MiColor = "#bdecb6";
            TxtUdBase.BackColor = Color.FromName(Variables.MiColor);
            TxtCodGolden.BackColor = Color.FromName(Variables.MiColor);
            TxtVariedad.BackColor = Color.FromName(Variables.MiColor);
            TextTipoFormato.BackColor = Color.FromName(Variables.MiColor);
            TxtTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtDTipoPlanta.BackColor = Color.FromName(Variables.MiColor);
            TxtNumPlanta.BackColor = Color.FromName(Variables.MiColor);
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

        

        protected void gvCabecera_Selecciona(string Index)
        {
            string miro = Index;
            this.Session["IDCabecera"] = miro;
            gvCabecera.EditIndex = -1;


            if (this.Session["EstadoCabecera"].ToString() == "3")
            {
                Carga_tablaCabeceraClose(this.Session["GridOrden"].ToString());
                Btreviertelote.Visible = true;
                //LBCountLista.Visible = false;
            }
            else
            {
                Carga_tablaCabecera(this.Session["GridOrden"].ToString());
                Btreviertelote.Visible = false;
                //LBCountLista.Visible = true;
            }


            PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");
            //gvCabecera.DataBind();

            string SQL = "SELECT NUMERO, EMPRESA, PAIS, FECHAPREPARACION, FECHACARGA, TELEFONO, MATRICULA, TRANSPORTISTA, OBSERVACIONES, ESTADO FROM  ZCARGA_CABECERA WHERE ID = " + miro;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in dt.Rows)
            {
                TxtNumero.Text = filas["NUMERO"].ToString();
                int ps = DrEmpresa.Items.IndexOf(DrEmpresa.Items.FindByText(filas["EMPRESA"].ToString()));
                DrEmpresa.SelectedIndex = ps;
                //DrEmpresa.Text = filas["EMPRESA"].ToString();
                //DrPais.Text = filas["PAIS"].ToString();
                ps = DrPais.Items.IndexOf(DrPais.Items.FindByText(filas["PAIS"].ToString()));
                DrPais.SelectedIndex = ps;
                TxtFechaPrepara.Text = filas["FECHAPREPARACION"].ToString();
                TxtFecha.Text = filas["FECHACARGA"].ToString();
                TxtTelefono.Text = filas["TELEFONO"].ToString();
                TxtMatricula.Text = filas["MATRICULA"].ToString();
                TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();

                if (filas["ESTADO"].ToString() == "-1") { TxtEstadoCab.Text = "Secuencia"; }
                if (filas["ESTADO"].ToString() == "0") { TxtEstadoCab.Text = "Nuevo"; }
                if (filas["ESTADO"].ToString() == "1") { TxtEstadoCab.Text = "En preparación"; }
                if (filas["ESTADO"].ToString() == "2") { TxtEstadoCab.Text = "Confirmado"; }
                if (filas["ESTADO"].ToString() == "3") { TxtEstadoCab.Text = "Cerrado"; }
                if (filas["ESTADO"].ToString() == "4") { TxtEstadoCab.Text = "MODIFICADO"; }
               
                //TxtEstadoCab.Text = filas["ESTADO"].ToString();
                TxtTransportista.Text = filas["TRANSPORTISTA"].ToString();
                TxtPais.Text = filas["PAIS"].ToString();
                TxtEmpresa.Text = filas["EMPRESA"].ToString();

                ps = DrTransportista.Items.IndexOf(DrTransportista.Items.FindByText(filas["TRANSPORTISTA"].ToString()));
                DrTransportista.SelectedIndex = ps;

                TxtNumero.Enabled = false;
                DrEmpresa.Enabled = false;
                DrPais.Enabled = false;
                TxtFechaPrepara.Enabled = false;
                TxtFecha.Enabled = false;
                TxtTelefono.Enabled = false;
                TxtMatricula.Enabled = false;
                TxtObservaciones.Enabled = false;
                TxtEstadoCab.Enabled = false;
                TxtTransportista.Enabled = false;
                TxtPais.Enabled = false;
                TxtEmpresa.Enabled = false;
                DrTransportista.Enabled = false;

                SeleccionCabecera();

                //LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + DrEmpresa.SelectedItem.Value + ", Pais: " + DrPais.SelectedItem.Value + ", Fecha: " + TxtFecha.Text;
                //LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + DrTransportista.SelectedItem.Value + ")";
                //BtnuevaCabecera.Text = "Cancelar";
                //HtmlButton btn = (HtmlButton)FindControl("BtnuevaCabecera");
                //HtmlButton li = (HtmlButton)FindControl("BtCancelCabecera");
                BtnuevaCabecera.Visible = false;
                BtCancelCabecera.Visible = true;
                //BtnuevaCabecera.Attributes["class"] = "btn btn-warning  btn-block";
                break;
            }

            //Carga_tablaCabecera();
            //Carga_tabla();
            //Carga_tablaLista();

            SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + miro;
            Lberror.Text += SQL + "1- gvCabecera_Selecciona " + Variables.mensajeserver;
            dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 1- gvCabecera_Selecciona " + Variables.mensajeserver;
            gvLista.DataSource = dt;
            gvLista.DataBind();
            this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
            CreaPalets(dt);

            Carga_tablaLista();

            gvCabecera.EditIndex = -1;

            gvCabecera.DataBind();
        }

        protected void gvLista_Sube(string Index)
        {
            GridViewRow row = gvLista.Rows[Convert.ToInt32(Index)];
            string miro = gvLista.DataKeys[Convert.ToInt32(Index)].Value.ToString();
            //sube la linea a la orden
            string Numero = "";
            decimal UNIDADES = 1.0M;
            //string Mira = "";
            //string SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + this.Session["IDGridB"].ToString();


            //Mira = Server.HtmlDecode(row.Cells[10].Text);
            //if (Mira != "")
            //{
            //    UNIDADES = Convert.ToDecimal(Mira.Replace(".", ","));
            //}
            //Mira = Server.HtmlDecode(row.Cells[13].Text);
            //if (Mira != "")
            //{
            //    Numero = Mira.Replace(".", ",");
            //}

            TextBox txtBox = (TextBox)gvLista.FindControl("TabLCarga"); //antes 10
            if (txtBox != null)
            {
                UNIDADES = Convert.ToDecimal(txtBox.Text);
            }
            txtBox = (TextBox)gvLista.FindControl("TabLNumLinea"); //antes 13
            if (txtBox != null)
            {
                Numero = txtBox.Text;
            }
            //DBHelper.ExecuteNonQuery(SQL);

            string SQL = "UPDATE ZCARGA_ORDEN SET (UNIDADESENCARGA = UNIDADESENCARGA + UNIDADES) WHERE ID_CABECERA = " + miro;

            Lberror.Text += SQL + "1- gvLista_Sube " + Variables.mensajeserver;
            DBHelper.ExecuteNonQuery(SQL);
            Lberror.Text += " 1- gvLista_Sube " + Variables.mensajeserver;


            SQL = "DELETE FROM ZCARGA_LINEA WHERE ID_SECUENCIA = " + miro + " AND NUMERO_LINEA = " + Numero;


            Carga_tabla();
            Carga_tablaLista();

            gvLista.EditIndex = -1;

            gvLista.DataBind();
        }

        protected void gvLista_Camion(string Index)
        {

            GridViewRow row = (GridViewRow)gvLista.Rows[Convert.ToInt32(Index)];

            string miro = gvLista.DataKeys[Convert.ToInt32(Index)].Value.ToString();
            string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaLista();

            gvLista.EditIndex = -1;

            gvLista.DataBind();
        }

        protected void gvControl_BajaOrden(string Index)
        {
            GridViewRow row = gvControl.Rows[Convert.ToInt32(Index)];
            string miro = gvControl.DataKeys[Convert.ToInt32(Index)].Value.ToString();

            decimal NUMPALET = 1.0M;
            decimal UNIDADES = 1.0M;
            decimal REPARTO = 1.0M;
            string Cabecera = "";
            string SQL = "";
            //string Mira = "";

            //Mira = Server.HtmlDecode(row.Cells[12].Text);
            //if (Mira != "")
            //{
            //    UNIDADES = Convert.ToDecimal(Mira.Replace(".", ","));
            //}
            //Mira = Server.HtmlDecode(row.Cells[14].Text);
            //if (Mira != "")
            //{
            //    NUMPALET = Convert.ToDecimal(Mira.Replace(".", ","));
            //}
            //Mira = Server.HtmlDecode(row.Cells[15].Text);
            //if (Mira != "")
            //{
            //    Cabecera = Mira.Replace(".", ",");
            //}

            TextBox txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCargar") as TextBox);
            //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
            if (txtBox != null)
            {
                UNIDADES = Convert.ToDecimal(txtBox.Text);
            }

            txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
            //txtBox = (TextBox)(row.Cells[14].Controls[0]);
            if (txtBox != null)
            {
                NUMPALET = Convert.ToDecimal(txtBox.Text);
            }

            txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCabecera") as TextBox);
            //txtBox = (TextBox)(row.Cells[15].Controls[0]);
            if (txtBox != null)
            {
                Cabecera = txtBox.Text;
            }

            REPARTO = (UNIDADES / NUMPALET);

            decimal Unidad = 1.00M;
            int Linea = 0;
            int N = 0;

            Lberror.Text += SQL + "1- gvControl_BajaOrden " + Variables.mensajeserver;
            Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            Lberror.Text += " 1- gvControl_BajaOrden " + Variables.mensajeserver;

            if (Con is System.DBNull)
            {
                N = 1;
            }
            else
            {
                N = Convert.ToInt32(Con) + 1;
            }
            Lberror.Text += SQL + "2- gvControl_BajaOrden " + Variables.mensajeserver;
            Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            Lberror.Text += " 2- gvControl_BajaOrden " + Variables.mensajeserver;

            if (Con is System.DBNull)
            {
                Linea = 1;
            }
            else
            {
                Linea = Convert.ToInt32(Con) + 1;
            }

            while (NUMPALET >= 1)
            {
                SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, HASTA)"; //, ZSYSDATE 
                SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
                SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA, ";
                SQL += " ID_CABECERA + '|' + CLIENTEPROVEEDOR + '|' + NUMERO + '|' + LINEA + '|' + " + N + "' ";
                //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                Lberror.Text += SQL + "3- gvControl_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 3- gvControl_BajaOrden " + Variables.mensajeserver;


                //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
                //DBHelper.ExecuteNonQuery(SQL);
                NUMPALET = NUMPALET - Unidad;
                N += 1;
                Linea += 1;
            }
            if (NUMPALET > 0)
            {
                SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, HASTA  )";//, ZSYSDATE
                SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
                SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA, ";
                SQL += " ID_CABECERA + '|' + CLIENTEPROVEEDOR + '|' + NUMERO + '|' + LINEA + '|' + " + N + "' ";
                //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                Lberror.Text += SQL + "4- gvControl_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 4- gvControl_BajaOrden " + Variables.mensajeserver;

                //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
                //DBHelper.ExecuteNonQuery(SQL);
                NUMPALET = NUMPALET - Unidad;
            }

            SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            Lberror.Text += SQL + "5- gvControl_BajaOrden " + Variables.mensajeserver;
            DBHelper.ExecuteNonQuery(SQL);
            Lberror.Text += " 5- gvControl_BajaOrden " + Variables.mensajeserver;

            this.Session["NumeroPalet"] = Linea.ToString();

            Carga_tabla();
            Carga_tablaLista();

            gvControl.EditIndex = -1;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            gvControl.DataBind();



            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();
        }


        //protected void PrintGridView(object sender, EventArgs e)
        //{
        //    //Disable Paging if all Pages need to be Printed.
        //    GridView Grid = null;

        //    HtmlButton btn = (HtmlButton)sender;
        //    if(btn.ID == "btPrintCabecera")
        //    {
        //        Grid = gvCabecera as GridView;
        //    }
        //    if (btn.ID == "BtPrintOrden")
        //    {
        //        Grid = gvControl as GridView;
        //    }
        //    if (btn.ID == "BtPrintListas")
        //    {
        //        Grid = gvLista as GridView;
        //    }

        //    if(Grid == null) { return; }
        //    //Disable Paging.
        //    Grid.AllowPaging = false;

        //    //Re-bind the GridView.
        //    //this.BindGrid();

        //    //For Printing Header on each Page.
        //    Grid.UseAccessibleHeader = true;
        //    Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    Grid.FooterRow.TableSection = TableRowSection.TableFooter;
        //    Grid.Attributes["style"] = "border-collapse:separate";
        //    foreach (GridViewRow row in Grid.Rows)
        //    {
        //        if ((row.RowIndex + 1) % Grid.PageSize == 0 && row.RowIndex != 0)
        //        {
        //            row.Attributes["style"] = "page-break-after:always;";
        //        }
        //    }
        //    //}
        //    //else
        //    //{
        //    //    //Hide the Pager.
        //    //    Grid.PagerSettings.Visible = false;
        //    //    //this.BindGrid();
        //    //}

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        //Render GridView to HTML.
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //        //Grid.RenderControl(hw);

        //        //Enable Paging.
        //        Grid.AllowPaging = true;
        //        //this.BindGrid();

        //        //Remove single quotes to avoid JavaScript error.
        //        string gridHTML = sw.ToString().Replace(Environment.NewLine, "");
        //        string gridCSS = gridStyles.InnerText.Replace("\"", "'").Replace(Environment.NewLine, "");


        //        //Print the GridView.
        //        string script = "window.onload = function() { PrintGrid('" + gridHTML + "', '" + gridCSS + "'); }";
        //        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", script, true);
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /*Verifies that the control is rendered */
        //}
    }
}