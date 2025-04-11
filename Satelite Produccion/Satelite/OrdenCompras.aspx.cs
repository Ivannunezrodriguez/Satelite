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

namespace Satelite
{
    public partial class OrdenCompras : System.Web.UI.Page
    {
        Reports.ReportListCamion dtsE = null;

        private int registros = 0;
        private string[] ListadoArchivos;
        private static int IDDiv = 0;
        private static string IDTABLA = "-1";
        private Boolean Cargando = false;

        //private string ElIDaBorrar = "";
        private string ElID = "";
        private string ElOrden = "";
        private string ElOrdenControl = "";
        private string ElOrdenLista = "";

        static TextBox[] ArrayTextBoxs;
        static Label[] ArrayLabels;
        static DropDownList[] ArrayCombos;
        static int contadorControles;
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
                    Response.Redirect("Login.aspx"); //Default
                }

                if (this.Session["MiNivel"].ToString() == "9")
                {
                    Nominas.Visible = true;
                }

                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    Response.Redirect("Login.aspx"); //Default
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
                    this.Session["Filtro"] = "0";
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
                    //accion de la linea (linea actual; nueva linea; Id cabecera, Id; NUMERO_LINEA)  
                    this.Session["ModificaLinea"] = "";
                    this.Session["SelectLinea"] = "";

                    //TxtNumero.Enabled = false;
                    DataTable dt = new DataTable();
                    this.Session["TablaLista"] = dt;
                    //ChkSlot.Visible = false;
                    Variables.mensajeserver = "";
                    ArrayTextBoxs = new TextBox[20];
                    ArrayCombos = new DropDownList[20];
                    ArrayLabels = new Label[20];
                    contadorControles = 0;
                    //Lberror.Visible = false;
                    Carga_Filtros();
                    Campos_ordenados();
                    Carga_tabla();
                    //Carga_tablaLista();

                    //LinkButton lnkUp = (gvLista.Rows[0].FindControl("lnkUp") as LinkButton);
                    //LinkButton lnkDown = (gvLista.Rows[gvLista.Rows.Count - 1].FindControl("lnkDown") as LinkButton);
                    //lnkUp.Enabled = false;
                    //lnkUp.CssClass = "button disabled";
                    //lnkDown.Enabled = false;
                    //lnkDown.CssClass = "button disabled";

                    //Carga_tablaCabecera();
                    Carga_Menus();
                    //accordion3.Visible = true;

                    //TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //TxtFechaPrepara.Text = "";

                }
                else
                {

                }

                if (this.Session["DESARROLLO"].ToString() == "DESARROLLO")
                {
                    H3Titulo.Visible = false;
                    H3Desarrollo.Visible = true;
                    Lbhost2.Text = "(" + this.Session["ComputerName"].ToString() + ")";
                }
                else
                {
                    H3Titulo.Visible = true;
                    H3Desarrollo.Visible = false;
                    Lbhost1.Text = "(" + this.Session["ComputerName"].ToString() + ")";
                }

            }
            catch (Exception ex)
            {
                if (this.Session["Error"].ToString() == "0")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("thEnd.aspx");
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




     

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecoNomina.aspx");
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
            ddControlPageSize.Items.Clear();
            ddControlPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddControlPageSize.Items.Insert(1, new ListItem("20", "20"));
            ddControlPageSize.Items.Insert(2, new ListItem("30", "30"));
            ddControlPageSize.Items.Insert(3, new ListItem("Todos", "1000"));
            ddControlPageSize.SelectedIndex = -1;

            ddControlPageSize2.Items.Clear();
            ddControlPageSize2.Items.Insert(0, new ListItem("2", "2"));
            ddControlPageSize2.Items.Insert(1, new ListItem("10", "10"));
            ddControlPageSize2.Items.Insert(2, new ListItem("20", "20"));
            ddControlPageSize2.Items.Insert(3, new ListItem("30", "30"));
            ddControlPageSize2.Items.Insert(4, new ListItem("Todos", "1000"));
            ddControlPageSize2.SelectedIndex = -1;


            ddControlPageSize3.Items.Clear();
            ddControlPageSize3.Items.Insert(0, new ListItem("10", "10"));
            ddControlPageSize3.Items.Insert(1, new ListItem("20", "20"));
            ddControlPageSize3.Items.Insert(2, new ListItem("30", "30"));
            ddControlPageSize3.Items.Insert(3, new ListItem("Todos", "1000"));
            ddControlPageSize3.SelectedIndex = -1;
            //ddCabeceraPageSize.Items.Insert(0, new ListItem("5", "5"));
            //ddCabeceraPageSize.Items.Insert(1, new ListItem("10", "10"));
            //ddCabeceraPageSize.Items.Insert(2, new ListItem("25", "25"));
            //ddCabeceraPageSize.Items.Insert(3, new ListItem("50", "50"));
            //ddCabeceraPageSize.Items.Insert(4, new ListItem("100", "100"));
            //ddCabeceraPageSize.Items.Insert(5, new ListItem("200", "200"));
            //ddCabeceraPageSize.Items.Insert(6, new ListItem("500", "500"));
            //ddCabeceraPageSize.Items.Insert(7, new ListItem("Todos", "1000"));

            //ddControlPageSize.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //ddListaPageSize.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());



        }

        private void DejaPosAcordeon()
        {
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

            //Lberror.Text += SQL + "1- Carga_los_palet " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            //Lberror.Text += " 1- Carga_los_palet " + Variables.mensajeserver;
            
            this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
            CreaPalets(dt);
        }

        protected void BtMenus_Click(object sender, EventArgs e)
        {
            if (divMenu.Visible == true)
            {
                divMenu.Visible = false;
                HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
                li.Attributes.CssStyle.Add("margin", "0");
                MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
            }
            else
            {
                divMenu.Visible = true;
                HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
                li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
                MasMinMenu.Attributes["class"] = "fa fa-chevron-left fa-2x";
            }
        }

        public void Carga_Menus()
        {
            pagevistaform.Attributes["style"] = "";

            if (this.Session["Menu"].ToString() == "1")
            {
                ////el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                accordion2.Visible = false;
                accordion3.Visible = true;
                accordion5.Visible = false;
                accordion8.Visible = false;
                Horizontal.Visible = false;
                DivLotes.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "2")
            {
                //el 3
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                accordion3.Visible = true;
                accordion2.Visible = false;
                accordion8.Visible = false;
                accordion5.Visible = false;
                Horizontal.Visible = false;
                DivLotes.Visible = false;
                Carga_tabla();
            }
            if (this.Session["Menu"].ToString() == "3")
            {
                //el 4
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                this.Session["IDSession"] = "Children";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion8");
                this.Session["IDSession"] = "Children";
                //panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                accordion3.Visible = true;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion5.Visible = false;
                accordion8.Visible = true;
                Horizontal.Visible = false;
                DivLotes.Visible = false;
                ////Carga_tablaLista();
            }
            if (this.Session["Menu"].ToString() == "4")
            {
                ////el 2
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                accordion2.Visible = true;
                accordion3.Visible = false;
                accordion5.Visible = false;
                accordion8.Visible = false;
                Horizontal.Visible = false;
                DivLotes.Visible = true;
            }
            if (this.Session["Menu"].ToString() == "5")
            {
                //el 5
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";

                pagevistaform.Attributes["style"] = "height: 100%;";

                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                accordion5.Visible = true;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion8.Visible = false;
                Horizontal.Visible = false;
                DivLotes.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "6")
            {
                //el 5
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";

                pagevistaform.Attributes["style"] = "height: 100%;";

                HtmlGenericControl panel = (HtmlGenericControl)FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                accordion5.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion8.Visible = false;
                Horizontal.Visible = true;
                DivLotes.Visible = false;
            }

            if (divMenu.Visible == false)
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
                li.Attributes.CssStyle.Add("margin", "0");
            }
            else
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
                li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
            }
        }

        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu1") { this.Session["Menu"] = "1"; }
            if (Miro == "aMenu2") { this.Session["Menu"] = "2"; }
            if (Miro == "aMenu3") { this.Session["Menu"] = "3"; }
            if (Miro == "aMenu4") { this.Session["Menu"] = "4"; }
            if (Miro == "aMenu5") { this.Session["Menu"] = "5"; }
            if (Miro == "aMenu6") { this.Session["Menu"] = "6"; }
            Carga_Menus();
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
            //Carga_tablaLista();

        }


        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            Variables.Error = "";
            Carga_tablaNueva();
            //Carga_tablaLista();
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
            //VistaOrden.Visible = false;
            //VistaOrdenNO.Visible = true;
            //PanelGeneralCabecera.Visible = true;
        }

        private Control BuscarControl(Control pForm, Control pControlPadre, string pControlNombre)
        {
            if (pControlPadre.ID == pControlNombre)
            {
                //Retornamos el control si es igual al control padre
                return pControlPadre;
            }

            //Recorremos los controles que hayan dentro del control padre
            foreach (Control subControl in pControlPadre.Controls)
            {
                Control resultado = BuscarControl(pForm, subControl, pControlNombre);
                if (resultado != null)
                {
                    //Retornamos el control que estamos buscando
                    return resultado;
                }
            }

            //Sino lo encuentra retornamos nulo
            return null;
        }

        private Control FindALL(ControlCollection page, string id)
        {
            foreach (Control c in page)
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

            string SQL = "UPDATE ZCARGA_LINEA set ESTADO =  2 " ;
            
            Variables.Error = "";
            //Lberror.Text = SQL;

            DBHelper.ExecuteNonQuery(SQL);
            //Carga_tablaLista();


            //SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2 ,";
            //SQL += " ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            //SQL += " WHERE NUMERO = " + TxtNumero.Text;

            //Variables.Error = "";
            ////Lberror.Text = SQL;

            //DBHelper.ExecuteNonQuery(SQL);

            //Carga_tablaCabecera();

            this.Session["Menu"] = "5";
            Carga_Menus();
            PNreportLista.Visible = false;
            DivEtiquetas.Visible = true;

            CargaTodaLista("");
            DivEtiquetas.Visible = true;
            return;
 
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
            //if (EmpresaGV.Visible == true)
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("idgvCabecera");
            //    li.Attributes["class"] = "fa fa-angle-down fa-2x";
            //    EmpresaGV.Visible = false;
            //}
            //else
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("idgvCabecera");
            //    li.Attributes["class"] = "fa fa-angle-up fa-2x";
            //    if (CaCheck.Checked == false)
            //    {
            //        Carga_tablaCabecera();
            //    }
            //    else
            //    {
            //        Carga_tablaCabeceraClose();
            //    }
            //    //Carga_tablaCabecera();
            //    EmpresaGV.Visible = true;
            //}
        }



        private void Limpia_CajasCaberera()
        {
            //DrEmpresa.SelectedIndex = -1;
            //DrPais.SelectedIndex = -1;
            //TxtNumero.Text = "";
            //TxtFechaPrepara.Text = "";
            //TxtFecha.Text = "";
            //TxtTelefono.Text = "";
            //TxtMatricula.Text = "";
            //TxtObservaciones.Text = "";
            //TxtTransportista.Text = "";
            //TxtPais.Text = "";
            //TxtEmpresa.Text = "";
            //DrTransportista.SelectedIndex = -1;
            //Btreviertelote.Visible = false;
            ////LBCountLista.Visible = true;
            //LBCountLista.Text = "Contiene: 0 Líneas de carga";
        }

        protected void BtCancelCabecera_Click(object sender, EventArgs e)
        {
            //DrSelCab.Items.Clear();
            //DrOrdenMin.Items.Clear();
            //DrOrdenMin.Attributes.Add("style", "background-color:#ffffff");
            //DrSelCab.Attributes.Add("style", "background-color:#ffffff");

            //HtmlButton btn = (HtmlButton)sender;
            //HtmlButton li = (HtmlButton)FindControl("BtnuevaCabecera");
            //btn.Visible = false;
            //li.Visible = true;

            //BtCancelCabecera.Visible = false;
            //BtnuevaCabecera.Visible = true;
            ////BtnNewCabecera.Visible = true;

            ////TxtNumero.Enabled = true;
            //DrEmpresa.Enabled = true;
            //DrPais.Enabled = true;
            //TxtFechaPrepara.Enabled = true;
            //TxtFecha.Enabled = true;
            //TxtTelefono.Enabled = true;
            //TxtMatricula.Enabled = true;
            //TxtObservaciones.Enabled = true;
            //TxtTransportista.Enabled = true;
            //TxtPais.Enabled = true;
            //TxtEmpresa.Enabled = true;
            //DrTransportista.Enabled = true;

            //Limpia_CajasCaberera();

            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaCabecera();
            //PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");

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
            //PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");
            //if (TxtEmpresa.Text == "" && TxtObservaciones.Text == "" && TxtMatricula.Text == "" && TxtPais.Text == "" && TxtTelefono.Text =="" && TxtTransportista.Text == "")
            //{
            //    //TextAlerta.Text = "Debe añadir algún dato en las casillas obligatorias para crear un registro nuevo.";
            //    //alerta.Visible = true;
            //    Lbmensaje.Text = "Debe añadir algún dato en las casillas obligatorias para crear un registro nuevo.";
            //    Asume.Visible = true;
            //    Modifica.Visible = false;
            //    cuestion.Visible = false;
            //    Decide.Visible = false;
            //    //btnasume.Visible = true;
            //    DvPreparado.Visible = true;
            //    //BtnAcepta.Visible = false;
            //    //BTnNoAcepta.Visible = false;
            //    return;
            //}
            //int N = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT ID_SECUENCIA FROM ZCARGA_CABECERA A WHERE ID = 1 ", null));
            //TxtNumero.Text = N.ToString();
            //string SQL = "UPDATE ZCARGA_CABECERA SET ID_SECUENCIA = (ID_SECUENCIA + 1) WHERE ID = 1 ";
            //DBHelper.ExecuteNonQuery(SQL);
            //SQL = "INSERT INTO ZCARGA_CABECERA (NUMERO, ID_SECUENCIA, EMPRESA, PAIS, FECHACARGA, ";
            //SQL += " TELEFONO, MATRICULA, TRANSPORTISTA, ESTADO, OBSERVACIONES, FECHAPREPARACION , ZSYSDATE)";
            //SQL += " VALUES(" + TxtNumero.Text + "," + N + ",'" + TxtEmpresa.Text + "','" + TxtPais.Text + "','";
            //if(TxtFecha.Text == "")
            //{
            //    SQL += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','";
            //}
            //else
            //{
            //    SQL += TxtFecha.Text + "','";
            //}
            //SQL += TxtTelefono.Text + "','" + TxtMatricula.Text + "','" + TxtTransportista.Text + "',0,'" + TxtObservaciones.Text + "','";
            //if (TxtFechaPrepara.Text == "")
            //{
            //    SQL += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','";
            //}
            //else
            //{
            //    SQL += TxtFechaPrepara.Text + "','";
            //}
            //SQL += DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ) ";

            //DBHelper.ExecuteNonQuery(SQL);

            //SeleccionCabecera();
            ////LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + TxtEmpresa.Text + ", Pais: " + TxtPais.Text + ", Fecha: " + TxtFecha.Text;
            ////LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + TxtTransportista.Text + ")";
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaCabecera();
            ////BtnuevaCabecera.Visible = false;
            ////BtnNewCabecera.Visible = true;
            //HtmlButton btn = (HtmlButton)sender;
            //HtmlButton li = (HtmlButton)FindControl("BtnuevaCabecera");
            //btn.Visible = true;
            //li.Visible = false;

            //BtCancelCabecera.Visible = true;
            //BtnuevaCabecera.Visible = false;
            ////BtnNewCabecera.Visible = true;

            ////TxtNumero.Enabled = true;
            //DrEmpresa.Enabled = false;
            //DrPais.Enabled = false;
            //TxtFechaPrepara.Enabled = false;
            //TxtFecha.Enabled = false;
            //TxtTelefono.Enabled = false;
            //TxtMatricula.Enabled = false;
            //TxtObservaciones.Enabled = false;
            //TxtTransportista.Enabled = false;
            //TxtPais.Enabled = false;
            //TxtEmpresa.Enabled = false;
            //DrTransportista.Enabled = false;
        }

        private void SeleccionCabecera()
        {
            //PanelCabecera.Attributes.Add("style", "background-color:#e6f2e1");

            //DrSelCab.Items.Clear();
            //DrSelCab.Items.Add("Número: " + TxtNumero.Text);
            //DrSelCab.Items.Add("Empresa: " + DrEmpresa.SelectedItem.Value);
            //DrSelCab.Items.Add("Pais: " + DrPais.SelectedItem.Value);
            //DrSelCab.Items.Add("Fecha preparación: " + TxtFechaPrepara.Text);
            //DrSelCab.Items.Add("Fecha: " + TxtFecha.Text);
            //DrSelCab.Items.Add("Teléfono: " + TxtTelefono.Text);
            //DrSelCab.Items.Add("Matricula: " + TxtMatricula.Text);
            //DrSelCab.Items.Add("Transportista: " + DrTransportista.SelectedItem.Value);
            //DrSelCab.Attributes.Add("style", "background-color:#e6f2e1");

            //DrOrdenMin.Items.Clear();
            //DrOrdenMin.Items.Add( TxtNumero.Text + "; " + DrPais.SelectedItem.Value + "; " + TxtFecha.Text);
            //DrOrdenMin.Items.Add("Número: " + TxtNumero.Text);
            //DrOrdenMin.Items.Add("Empresa: " + DrEmpresa.SelectedItem.Value);
            //DrOrdenMin.Items.Add("Pais: " + DrPais.SelectedItem.Value);
            //DrOrdenMin.Items.Add("Fecha preparación: " + TxtFechaPrepara.Text);
            //DrOrdenMin.Items.Add("Fecha: " + TxtFecha.Text);
            //DrOrdenMin.Items.Add("Teléfono: " + TxtTelefono.Text);
            //DrOrdenMin.Items.Add("Matricula: " + TxtMatricula.Text);
            //DrOrdenMin.Items.Add("Transportista: " + DrTransportista.SelectedItem.Value);
            //DrOrdenMin.Attributes.Add("style", "background-color:#e6f2e1"); 
        }


        
        protected void checkCabeceraListas_Click(object sender, EventArgs e)
        {
            ////OrdenCabecera();
            //gvCabecera.EditIndex = -1;
            //gvControl.EditIndex = -1;
            //gvLista.EditIndex = -1;

            //Btreviertelote.Visible = false;
            ////LBCountLista.Visible = true;

            //if (CaCheck.Checked == false)
            //{
            //    this.Session["Cerrados"] = "0";
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    //ocultar boton modificar
            //    this.Session["Cerrados"] = "1";
            //    Carga_tablaCabeceraClose();
            //}
            //gvCabecera.DataBind();
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            //if (chkOnOff.Checked == false)
            //{
            //    DrTransportista.Visible = false;
            //    DrPais.Visible = false;
            //    DrEmpresa.Visible = false;
            //    TxtTransportista.Visible = true;
            //    TxtPais.Visible = true;
            //    TxtEmpresa.Visible = true;
            //}
            //else
            //{
            //    DrTransportista.Visible = true;
            //    DrPais.Visible = true;
            //    DrEmpresa.Visible = true;
            //    TxtTransportista.Visible = false;
            //    TxtPais.Visible = false;
            //    TxtEmpresa.Visible = false;
            //}
        }

        
        protected void Btreviertelote_Click(object sender, EventArgs e)
        {
            ////coloca el estado de la orden cerrada en confirmada
            //string SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2,  " ;
            //SQL += "ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            //SQL += " WHERE ID = " + this.Session["IDCabecera"].ToString();

            //this.Session["EstadoCabecera"] = "2";
            //Variables.Error = "";
            ////Lberror.Text = SQL;
            //DBHelper.ExecuteNonQuery(SQL);

            //Limpia_CajasCaberera();
            

            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            //DrSelCab.Items.Clear();
            //DrOrdenMin.Items.Clear();
            //DrOrdenMin.Attributes.Add("style", "background-color:#ffffff");
            //DrSelCab.Attributes.Add("style", "background-color:#ffffff");

        }

        protected void ImageOrden_Click(object sender, EventArgs e)
        {
            //PanelGeneralCabecera.Visible = false;
            VistaOrden.Visible = true;
            //VistaOrdenNO.Visible = false;
        }

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {
            HtmlGenericControl li = (HtmlGenericControl)FindControl("PanelgeneralFiltro");
            if(li.Visible == true)
            {
                li.Visible = false;
            }
            else
            {
                li.Visible = true;
            }
            li = (HtmlGenericControl)FindControl("PanelgeneralFiltro2");
            if (li.Visible == true)
            {
                li.Visible = false;
            }
            else
            {
                li.Visible = true;
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
            //TxtEmpresa.Text = DrEmpresa.SelectedItem.Text;
        }
        protected void DrPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtPais.Text = DrPais.SelectedItem.Text;
        }
        protected void DrFecha_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        
        protected void gvLista_PageSize_Changed(object sender, EventArgs e)
        {
            //gvLista.PageSize = Convert.ToInt32(ddListaPageSize.SelectedItem.Value);
            //Carga_tablaLista();
        }
        protected void gvCabecera_PageSize_Changed(object sender, EventArgs e)
        {
            //gvCabecera.PageSize = Convert.ToInt32(ddCabeceraPageSize.SelectedItem.Value);
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaCabecera();
        }
        protected void gvControl_PageSize_Changed(object sender, EventArgs e)
        {
            gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
            gvAmbos.PageSize = Convert.ToInt32(ddControlPageSize2.SelectedItem.Value);
            gvHorizontal.PageSize = Convert.ToInt32(ddControlPageSize3.SelectedItem.Value);
            Carga_tabla();
        }

      

        protected void PrintReportOff_Click(object sender, EventArgs e)
        {
            //ReportViewer1.Visible = false;

            //DVtLista.Visible = true;
            //DVtListaOff.Visible = false;

            //PNreportLista.Visible = false;
            //PNFiltrosLista.Visible = true;
            //PNGridLista.Visible = true;
        }

     
        protected void PrintReport_Click(object sender, EventArgs e)
        {
            ////DVtLista.Visible = false;
            ////DVtListaOff.Visible = true;
            //DataTable DT = null;
            //string ZRuta = "";
            //string ZReporte = "";
            //string ZDataSet = "";

            //if (TxtNumero.Text == "")
            //{
            //    Lbmensaje.Text = "No tiene seleccionada una Orden de Carga para poder generar un informe.";
            //    cuestion.Visible = true;
            //    Asume.Visible = true;
            //    Modifica.Visible = false;
            //    cuestion.Visible = false;
            //    Decide.Visible = false;
            //    DvPreparado.Visible = false;
            //    //BtnAcepta.Visible = false;
            //    //BTnNoAcepta.Visible = false;
            //    return;
            //}

            //if (CaCheck.Checked == false)
            //{
            //    this.Session["Centro"] = "ABIERTO";
            //}
            //else
            //{
            //    this.Session["Centro"] = "CERRADO";
            //}

            //string SQL = " SELECT A.REPORTE , B.ZRUTA, B.ZREPORTE, B.ZDATASET, B.ZSQL  ";
            //SQL += " FROM REC_PARAM A, ZINFORMES B";
            //SQL += " WHERE A.REPORTE = B.ZID";
            //SQL += " AND A.CENTRO = '" + this.Session["Centro"].ToString() + "' ";

            //DT = Main.BuscaLote(SQL).Tables[0];

            //foreach (DataRow filas in DT.Rows)
            //{
            //    ZRuta = filas["ZRUTA"].ToString();
            //    ZReporte = filas["ZREPORTE"].ToString();
            //    ZDataSet = filas["ZSQL"].ToString();
            //    //ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
            //    break;
            //}


            //try
            //{
            //    ReportViewer1.Visible = false;
            //    //Limpio los enlaces del ReportViewer 
            //    ReportViewer1.LocalReport.DataSources.Clear();

                
            //    //string logo = t1.Text;
            //    //string logo = "http://localhost/webRdlc/images/logo.jpg";


            //    if (CaCheck.Checked == false)
            //    {
            //        ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);

            //        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rpalvcamionlista.rdlc");


            //        SQL = " SELECT ";
            //        SQL += " B.ID_CABECERA AS ZCAMION,   ";
            //        SQL += " B.EMPRESA AS ZEMPRESA,    ";
            //        SQL += " E.PAIS AS ZPAIS,    ";
            //        SQL += " FORMAT(E.FECHACARGA, 'dd-MM-yyyy') AS ZFECHACARGA,   ";
            //        SQL += " E.MATRICULA AS ZMATRICULA,    ";
            //        SQL += " E.TRANSPORTISTA AS ZTRANSPORTISTA,    ";
            //        SQL += " E.TELEFONO AS ZTELEFONO,    ";
            //        SQL += " E.OBSERVACIONES AS ZOBSERVACION,    ";
            //        SQL += " B.POSICIONCAMION AS ZPOSICIONCAMION,   ";
            //        SQL += " B.NUMERO AS ZPEDIDO,       ";
            //        SQL += " B.NOMBREFISCAL AS ZCLIENTE,   ";
            //        SQL += " D.ZTIPO_FORMATO + ' ' + CONVERT(VARCHAR(255), D.ZNUMERO_PLANTAS) AS ZFORMATO, ";
            //        SQL += " C.ZVARIEDAD,    ";
            //        //SQL += " CONVERT(INT, REPLACE(UDSENCARGA, '.', '')) / CONVERT(INT, D.ZNUMERO_PLANTAS)  AS ZNUMCAJAS, "; + ' ' + CONVERT(varchar(15),CAST(E.ZSYSDATE AS TIME),100)
            //        SQL += " CONVERT(INT, CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)  AS ZNUMCAJAS,";
            //        //SQL += " REPLACE(REPLACE(CONVERT(VARCHAR,CONVERT(Money, CONVERT(INT, (CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)),1),'.00',''),',','.')  AS ZNUMPLANTAS,";
            //        SQL += " CONVERT(INT, (CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000) AS ZNUMPLANTAS,";
            //        SQL += " D.ZNUMERO_PLANTAS AS ZNUMPLANTAS,    ";
            //        SQL += " B.OBSERVACIONES AS ZOBSERVACIONES, ";
            //        SQL += " CONVERT(DECIMAL (4,2), B.NUMPALET) AS NUMPALET,   ";
            //        SQL += " (SELECT  COUNT(DISTINCT POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = E.NUMERO) AS ZMIPALET, ";
            //        SQL += " CONVERT(varchar(20),FORMAT (E.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
            //        SQL += " FROM ZCARGA_LINEA B, ZCARGA_CABECERA E , ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZBANDEJAS D ";
            //        SQL += " WHERE C.ZTIPO_PLANTA = D.ZTIPO_PLANTA ";
            //        SQL += " AND C.ZCODGOLDEN = B.ARTICULO ";
            //        //Gloria 11/11/2021 debe mirar como ajustar tipo formato para lineas de carga con ZBANDEJAS (¿columna nueva? por no tener zentrada para ver A.UNIDADES)
            //        SQL += " AND D.ZTIPO_FORMATO = 'CAJAS' ";
            //        SQL += " AND B.ID_CABECERA = E.NUMERO ";
            //        SQL += " AND E.NUMERO = " + TxtNumero.Text;
            //        SQL += " ORDER BY CONVERT(INT, B.POSICIONCAMION) ";
            //    }
            //    else
            //    {
            //        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rpalvcamionlote.rdlc");
            //        ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
                    

            //        SQL = " SELECT ";
            //        SQL += " A.LOTE AS ZLOTE,   ";
            //        SQL += " B.ID_CABECERA AS ZCAMION,   ";
            //        SQL += " B.EMPRESA AS ZEMPRESA,    ";
            //        SQL += " E.PAIS AS ZPAIS,    ";
            //        SQL += " FORMAT(E.FECHACARGA, 'dd-MM-yyyy') AS ZFECHACARGA,   ";
            //        SQL += " E.MATRICULA AS ZMATRICULA,    ";
            //        SQL += " E.TRANSPORTISTA AS ZTRANSPORTISTA,    ";
            //        SQL += " E.TELEFONO AS ZTELEFONO,    ";
            //        SQL += " E.OBSERVACIONES AS ZOBSERVACION,    ";
            //        SQL += " B.POSICIONCAMION AS ZPOSICIONCAMION,   ";
            //        SQL += " B.NUMERO AS ZPEDIDO,       ";
            //        SQL += " B.NOMBREFISCAL AS ZCLIENTE,   ";
            //        SQL += " D.ZTIPO_FORMATO + ' ' + CONVERT(VARCHAR(255), D.ZNUMERO_PLANTAS) AS ZFORMATO, ";
            //        SQL += " C.ZVARIEDAD,    ";
            //        //SQL += " CONVERT(DECIMAL(8,3), UDSENCARGA) / CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS)  AS ZNUMCAJAS, "; + ' ' + CONVERT(varchar(15),CAST(E.ZSYSDATE AS TIME),100)
            //        //SQL += " (CONVERT(DECIMAL(8,3), UDSENCARGA) / CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8,3), D.ZNUMERO_PLANTAS) AS ZNUMPLANTAS,    ";
            //        //SQL += " CONVERT(INT, CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000)  AS ZNUMCAJAS,";
            //        SQL += " CONVERT(DECIMAL (4,0),A.NUM_UNIDADES) AS ZNUMCAJAS, ";
            //        //SQL += " A.NUM_UNIDADES AS ZNUMCAJAS, ";
            //        //SQL += " CONVERT(INT, (CONVERT(DECIMAL(8, 3), UDSENCARGA) / CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS)) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS) * 1000) AS ZNUMPLANTAS,";
            //        SQL += "  CONVERT(INT, (CONVERT(DECIMAL(8, 3), A.NUM_UNIDADES) * CONVERT(DECIMAL(8, 3), D.ZNUMERO_PLANTAS))) AS ZNUMPLANTAS, ";
            //        SQL += " B.OBSERVACIONES AS ZOBSERVACIONES, ";
            //        SQL += " CONVERT(DECIMAL (4,2), B.NUMPALET) AS NUMPALET ,  ";
            //        //SQL += " (SELECT COUNT(DISTINTC B.POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = " + TxtNumero.Text + ") AS SUMPALET";

            //        SQL += " (SELECT  COUNT(DISTINCT POSICIONCAMION) FROM ZCARGA_LINEA WHERE ID_CABECERA = E.NUMERO) AS ZMIPALET, ";
            //        SQL += " CONVERT(varchar(20),FORMAT (E.ZSYSDATE, 'dd/MM/yyyy HH:mm:ss'))  AS ZSYSHORA ";
            //        SQL += " FROM ZENTRADA A , ZCARGA_LINEA B, ZCARGA_CABECERA E , ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZBANDEJAS D ";
            //        SQL += " WHERE A.TIPO_FORM = 'Venta' ";
            //        //SQL += " and B.HASTA LIKE A.HASTA + '%' "; //Generar el campo hasta de carga_linea
            //        //restricción de la consulta para retorno de carro y salto de linea
            //        SQL += " and REPLACE(REPLACE(B.HASTA, CHAR(10), ''), CHAR(13), '') = REPLACE(REPLACE(A.HASTA, CHAR(10), ''), CHAR(13), '') ";
            //        SQL += " and C.ZTIPO_PLANTA = A.TIPO_PLANTA ";
            //        SQL += " and A.VARIEDAD = C.ZVARIEDAD ";
            //        SQL += " and A.TIPO_PLANTA = D.ZTIPO_PLANTA ";
            //        SQL += " AND D.ZTIPO_FORMATO = A.UNIDADES   ";
            //        SQL += " AND B.ID_CABECERA = E.NUMERO ";
            //        SQL += " AND E.NUMERO = " + TxtNumero.Text;
            //        SQL += " ORDER BY CONVERT(INT, B.POSICIONCAMION) ";
            //    }


            //    DataTable dt = Main.BuscaLote(SQL).Tables[0];


            //    dtsE.Tables.RemoveAt(0);    //Eliminamos la tabla que crea por defecto
            //                                //DataTable dtCopy = dt.Copy();


            //    //DataTable dtCopy = dt.ToTable.DefaultView.ToTable(False, strSelectedCols)).Tables(0).Clone();
            //    dtsE.Tables.Add(dt.Copy());   //Añadimos la tabla que acabamos de crear dtlistacamion



            //    if (CaCheck.Checked == false)
            //    {
            //        //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtalvcamionlote", dtsE.Tables[0]));
            //        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsE.Tables[0]));
            //    }
            //    else
            //    {
            //        //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dtalvcamionlote", dtsE.Tables[0]));
            //        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsE.Tables[0]));
            //    }

            //    //ReportDataSource datasource = new ReportDataSource("DTListaCamion", dtsE.Tables[0]);
            //    //ReportViewer1.LocalReport.DataSources.Add(datasource);

            //    //Ahora abre menu 5
            //    this.Session["Menu"] = "5";
            //    Carga_Menus();
            //    PNreportLista.Visible = true;
            //    DivEtiquetas.Visible = false;


            //    ReportViewer1.DataBind();

            //    //ReportParameter[] parameters = new ReportParameter[1];  //Array que contendrá los parámetros
            //    //parameters[0] = new ReportParameter("logo", logo);      //Establecemos el valor de los parámetros
            //    //ReportViewer1.LocalReport.SetParameters(parameters);    //Pasamos el array de los parámetros al ReportViewer

            //    ReportViewer1.LocalReport.Refresh();    //Actualizamos el report
            //    ReportViewer1.Visible = true;
            //}
            //catch (Exception ex)
            //{
            //    //Variables.Error = ex.Message;
            //    //TextAlerta.Text = ex.Message;
            //    //alerta.Visible = true;
            //    Lbmensaje.Text = ex.Message;
            //    Asume.Visible = true;
            //    Modifica.Visible = false;
            //    cuestion.Visible = false;
            //    Decide.Visible = false;
            //    //btnasume.Visible = true;
            //    DvPreparado.Visible = true;
            //    //BtnAcepta.Visible = false;
            //    //BTnNoAcepta.Visible = false;
            //}
            ////PNreportLista.Visible = true;
            ////PNFiltrosLista.Visible = false;
            ////PNGridLista.Visible = false;
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
            //Carga_tablaLista();
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
            if(combo == "UDSPEDIDAS" || combo == "UDSSERVIDAS" || combo == "UDSENCARGA" || combo == "UDSPENDIENTES" || combo == "UDSACARGAR" || combo == "NUMPALET")
            {
                if(ElOrdenControl == "")
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
                    if(combo == "NUMERO" || combo == "POSICIONCAMION" || combo == "LINEA" || combo == "NUMERO_LINEA")
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



        protected void DrTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtTransportista.Text = DrTransportista.SelectedItem.Text;
        }

        protected void gvCabecera_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            //gvCabecera.PageIndex = e.NewPageIndex;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaCabecera();
        }

        protected void gvControl_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvControl.PageIndex = e.NewPageIndex;
            gvAmbos.PageIndex = e.NewPageIndex;
            gvHorizontal.PageIndex = e.NewPageIndex;
            Carga_tabla();
        }

        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            //gvLista.PageIndex = e.NewPageIndex;
            //Carga_tablaLista();
        }

        protected void gvControl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();
            gvControl.EditIndex = -1;
            gvAmbos.EditIndex = -1;
            gvHorizontal.EditIndex = -1;
            Carga_tabla();
            gvControl.DataBind();
            gvAmbos.DataBind();
            gvHorizontal.DataBind();


        }

        protected void gvCabecera_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            //GridViewRow row = gvCabecera.Rows[e.RowIndex];
            //string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();
            //gvCabecera.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaCabecera();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaCabecera();
            //gvCabecera.DataBind();

            
        }

        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            //GridViewRow row = gvLista.Rows[e.RowIndex];
            //string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

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
             Carga_tabla(e.SortExpression);
        }

        protected void gvControl_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            return;
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
                DvPreparado.Visible = true;
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

            try
            {
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
                    if(txtBox.Text != "") // txtBox.Text && Esta == false)
                    {
                        if(Convert.ToInt32(rUNIDADES) > 0)
                        {
                            rCAJAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                            if(rNUMPALET > 0)
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

                if ((rUDSACARGAR + rUDSSERVIDAS) <= rUDSPEDIDAS)
                {
                    rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSACARGAR);
                }
                else
                {
                    Lbmensaje.Text = "No se pueden cargar más unidades de las que quedan pendientes.";
                    Asume.Visible = true;
                    Modifica.Visible = false;
                    cuestion.Visible = false;
                    Decide.Visible = false;
                    DvPreparado.Visible = true;
                    return;
                }
                //int a = container2.Controls.Count;


                string SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND A.ID = B.ID " ;
                SQL += " AND A.ID = " + miro;
                SQL += " GROUP BY A.ID_CABECERA ";
                Variables.Error = "";
                //Lberror.Text = SQL;

                
                //Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
                rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));// + rUDSACARGAR;
                //Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;

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
                //Lberror.Text = SQL;

                //Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;
                



                Carga_tabla();

                gvControl.EditIndex = -1;
                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvControl.DataSource = dt;
                gvControl.DataBind();
            }
            catch (Exception ex)
            {
                //Lberror.Text += ". " + ex.Message;
                //Lberror.Visible = true;
            }
        }

       
        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            //Carga_tablaLista(e.SortExpression);
        }

        protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
        {

            return; 
            gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvControl.EditIndex = e.NewEditIndex;


            Carga_tabla();
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

        protected void gvControl_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
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
                        DvPreparado.Visible = true;
                        //BtnAcepta.Visible = false;
                        //BTnNoAcepta.Visible = false;
                        return;
                    }
                    string Mira = "";
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
                    if(Convert.ToDecimal(Temporal) == 0) 
                    {
                        //TextAlerta.Text = "Seleccione un número de palets para asignar";
                        //alerta.Visible = true;

                        Lbmensaje.Text = "Seleccione un número de palets para asignar";
                        Asume.Visible = true;
                        cuestion.Visible = false;
                        Decide.Visible = false;
                        Modifica.Visible = false;
                        //btnasume.Visible = true;
                        DvPreparado.Visible = true;
                        //BtnAcepta.Visible = false;
                        //BTnNoAcepta.Visible = false;
                        return; 
                    }
                    string[] Partes =  System.Text.RegularExpressions.Regex.Split(NUMPALET.ToString(), ",");

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
                    //Lberror.Text += SQL + "1- gvControl_RowCommand " + Variables.mensajeserver;
                    Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    //Lberror.Text += " 1- gvControl_RowCommand " + Variables.mensajeserver;
                    
                    if (Con is System.DBNull)
                    {
                        N = 1;
                    }
                    else
                    {
                        N = Convert.ToInt32(Con) + 1;
                    }
                    //Lberror.Text += SQL + "2- gvControl_RowCommand " + Variables.mensajeserver;
                    Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    //Lberror.Text += " 2- gvControl_RowCommand " + Variables.mensajeserver;
                    
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

                        //Lberror.Text += SQL + "3- gvControl_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        //Lberror.Text += " 3- gvControl_RowCommand " + Variables.mensajeserver;


                        
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

                        //Lberror.Text += SQL + "4- gvControl_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        //Lberror.Text += " 4- gvControl_RowCommand " + Variables.mensajeserver;
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

                    SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                    SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                    SQL += " WHERE A.ID = B.ID ";
                    SQL += " AND A.NUMERO = B.NUMERO ";
                    SQL += " AND A.LINEA = B.LINEA ";
                    SQL += " AND A.EMPRESA = B.EMPRESA ";
                    SQL += " AND A.ID = " + miro;
                    SQL += " GROUP BY A.ID_CABECERA ";

                    //Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));
                    //Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;

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
                    //Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;


                    if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    else
                    {
                        SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    }
                    DBHelper.ExecuteNonQuery(SQL);

                    this.Session["NumeroPalet"] = Linea.ToString();

                    Carga_tabla();
                    //Carga_tablaLista();

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
                //Lberror.Text = "Control RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvControl.DataKeys[index].Value.ToString() + " " + ex.Message;
                //Lberror.Visible = true;
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

        protected void gvControl_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < gvControl.Columns.Count; i++)
                {
                    if(i == 0) { e.Row.Cells[i].ToolTip = "Edición de Registro"; }
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
                    if(miro != "0,00")
                    {
                        e.Row.BackColor = Color.FromName("#d2f2f6");
                    }
                    else if((e.Row.DataItemIndex % 2) == 0)
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

        protected void gvControl_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvControl.SelectedRow.BackColor = Color.FromName("#565656");
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
            string SQL =  " SELECT * FROM ZCARGA_LINEA  ";
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


        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            Lbmensaje.Text = "";
            this.Session["ElIDaBorrar"] = "";
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            Lbmensaje.Text = "";
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;

            if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////             ID; UNIDADES; numero linea
            //ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
            SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
            SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
            SQL += " NUMPALET = 0.00 ";
            SQL += " WHERE ID = " + Trata[0];
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

            DBHelper.ExecuteNonQuery(SQL);

            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            Carga_tabla();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();

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
            int B = 0;
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
            DvPreparado.Visible = false;
            //DBHelper.ExecuteNonQuery(this.Session["SelectLinea"].ToString());
            ////Lberror.Text += " 1- checkSiMlC_Click " + Variables.mensajeserver;
            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            //Carga_tablaLista();

            //gvLista.EditIndex = -1;
            //gvLista.DataBind();

        }

        protected void checkNoMlC_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            string SQL = "";
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;
            if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            {
                SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            }
            else
            {
                SQL = "UPDATE ZCARGA_CABECERA set  ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
                SQL += " WHERE NUMERO = " + TxtNumero.Text;
            }
            DBHelper.ExecuteNonQuery(SQL);

            this.Session["ElIDaBorrar"] = "";

            //Carga_tablaCabecera();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;
            //gvLista.DataBind();
        }

        protected void checkNo_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
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
        //        DvPreparado.Visible = true;
        //        //BtnAcepta.Visible = true;
        //        //BTnNoAcepta.Visible = true;
        //    }

        //}

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //GridViewRow row = (GridViewRow)gvLista.Rows[e.RowIndex];

            //this.Session["ElIDaBorrar"] = gvLista.DataKeys[e.RowIndex].Value.ToString();

            cuestion.Visible = true;
            Asume.Visible = false;
            Modifica.Visible = false;
            Decide.Visible = false;
            DvPreparado.Visible = true;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;

            //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            //SQL += " WHERE ID = " + miro;

            //DBHelper.ExecuteNonQuery(SQL);
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
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
            gvAmbos.DataSource = dt;
            gvAmbos.DataBind();
            gvHorizontal.DataSource = dt;
            gvHorizontal.DataBind();
            

        }


        private void Carga_tabla(string sortExpression = null)
        {
            string Temporal = ""; //Jose
            Carga_tablaListaFiltro();
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

                    SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                    SQL += " FROM ZPEDIDOS_COMPRA  ";
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
                    //Lberror.Text += SQL + "1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    //Lberror.Text += " 1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                }
                else
                {
                    SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                    SQL += " FROM ZPEDIDOS_COMPRA  ";
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
                    //Lberror.Text += SQL + "2- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    //Lberror.Text += " 2- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    
                    //}
                }
                //Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), TxtNumero.Text);
                this.Session["MiConsulta"] = dt;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvControl.DataSource = dv;
                    gvAmbos.DataSource = dv;
                    gvHorizontal.DataSource = dv;
                }
                else
                {
                    gvControl.DataSource = dt;
                    gvAmbos.DataSource = dt;
                    gvHorizontal.DataSource = dt;
                }
                gvControl.DataBind();

                gvHorizontal.DataBind();
                gvAmbos.DataBind();
                LbRowControl.Text = "Registros: " + dt.Rows.Count;
                //gvControl.DataSource = dt;
                //gvControl.DataBind();

                //busca Error donde no se puede depurar
                //Lberror.Text = "";

            }
            catch (Exception ex)
            {
                //Lberror.Text = "Carga tabla: " + SQL;
                //Lberror.Visible = true;
            }

        }

        private void Carga_FiltrosCabecera()
        {
            //string SQL = "";
            //DataTable dt = null;

            //DrEmpresa.Items.Clear();
            //DrEmpresa.DataValueField = "EMPRESA";
            //DrEmpresa.DataTextField = "EMPRESA";
            //DrEmpresa.Items.Insert(0, new ListItem("Seleccione uno", ""));
            ////SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY EMPRESA ";
            //SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY EMPRESA ";
            //dt = Main.BuscaLote(SQL).Tables[0];
            //DrEmpresa.DataSource = dt;
            //DrEmpresa.DataBind();
            //DrEmpresa.SelectedIndex = -1;

            ////DrFecha.Items.Clear();
            ////DrFecha.DataValueField = "FECHACARGA";
            ////DrFecha.DataTextField = "FECHACARGA";
            ////DrFecha.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //////SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY FECHAENTREGA ";
            ////SQL = "SELECT DISTINCT(FECHACARGA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY FECHACARGA ";
            ////dt = Main.BuscaLote(SQL).Tables[0];
            ////DrFecha.DataSource = dt;
            ////DrFecha.DataBind();
            ////DrFecha.SelectedIndex = -1;

            //DrPais.Items.Clear();
            //DrPais.DataValueField = "PAIS";
            //DrPais.DataTextField = "PAIS";
            //DrPais.Items.Insert(0, new ListItem("Seleccione uno", ""));
            //SQL = "SELECT DISTINCT(PAIS) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY PAIS ";
            //dt = Main.BuscaLote(SQL).Tables[0];
            //DrPais.DataSource = dt;
            //DrPais.DataBind();
            //DrPais.SelectedIndex = -1;

            //DrTransportista.Items.Clear();
            //DrTransportista.DataValueField = "TRANSPORTISTA";
            //DrTransportista.DataTextField = "TRANSPORTISTA";
            //DrTransportista.Items.Insert(0, new ListItem("Seleccione uno", ""));
            ////SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY RUTA ";
            //SQL = "SELECT DISTINCT(TRANSPORTISTA) FROM ZCARGA_CABECERA WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY TRANSPORTISTA ";
            //dt = Main.BuscaLote(SQL).Tables[0];
            //DrTransportista.DataSource = dt;
            //DrTransportista.DataBind();
            //DrTransportista.SelectedIndex = -1;
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
            SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY EMPRESA ";
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
                    //if (Filtros == "")
                    //{
                    //    Filtros += " WHERE EMPRESA = '" + DrConsultas.SelectedItem.Value + "'";
                    //}
                    //else
                    //{
                    //    Filtros += " AND EMPRESA = '" + DrConsultas.SelectedItem.Value + "'";
                    //}
                    this.Session["FiltroEmpresa"] = DrConsultas.SelectedItem.Value;
                    DrSelectFiltro.Items.Add("Empresa: " + DrConsultas.SelectedItem.Value);
                    //LbFiltros.Text = " ( Empresa: " + DrConsultas.SelectedItem.Value;
                }
                if (DrDesde.SelectedItem.Value != "" && DrHasta.SelectedItem.Value != "")
                {
                    //if (Filtros == "")
                    //{
                    //    Filtros += " WHERE BEETWEN FECHAENTREGA = '" + DrDesde.SelectedItem.Value + "' AND '" + DrHasta.SelectedItem.Value + "'";
                    //}
                    //else
                    //{
                    Filtros += " AND FECHAENTREGA BETWEEN  '" + DrDesde.SelectedItem.Value + "' AND '" + DrHasta.SelectedItem.Value + "'";
                    //}
                    this.Session["FiltroFecha"] = " B.Libre6 BETWEEN  '" + DrDesde.SelectedItem.Value + "' AND '" + DrHasta.SelectedItem.Value + "'";
                    DrSelectFiltro.Items.Add("Fecha Desde: " + DrDesde.SelectedItem.Value + ", Fecha Hasta: " + DrHasta.SelectedItem.Value);
                    //LbFiltros.Text += ", Fecha Desde: " + DrDesde.SelectedItem.Value + ", Fecha Hasta: " + DrHasta.SelectedItem.Value;
                }
                else
                {
                    if (DrDesde.SelectedItem.Value != "")
                    {
                        //if (Filtros == "")
                        //{
                        //    Filtros += " WHERE FECHAENTREGA = '" + DrDesde.SelectedItem.Value + "'";
                        //}
                        //else
                        //{
                        Filtros += " AND FECHAENTREGA = '" + DrDesde.SelectedItem.Value + "'";
                        //}
                        this.Session["FiltroFecha"] = " B.Libre6 = '" + DrDesde.SelectedItem.Value + "'";
                        DrSelectFiltro.Items.Add("Fecha Desde: " + DrDesde.SelectedItem.Value);
                        //LbFiltros.Text += ", Fecha Desde: " + DrDesde.SelectedItem.Value;
                    }
                    if (DrHasta.SelectedItem.Value != "")
                    {
                        //if (Filtros == "")
                        //{
                        //    Filtros += " WHERE FECHAENTREGA = '" + DrHasta.SelectedItem.Value + "'";
                        //}
                        //else
                        //{
                        Filtros += " AND FECHAENTREGA = '" + DrHasta.SelectedItem.Value + "'";
                        //}
                        this.Session["FiltroFecha"] = " B.Libre6 = '" + DrHasta.SelectedItem.Value + "'";
                        DrSelectFiltro.Items.Add("Fecha Hasta: " + DrHasta.SelectedItem.Value);
                        //LbFiltros.Text += ", Fecha Hasta: " + DrHasta.SelectedItem.Value;
                    }
                }
                if (DrCliente.SelectedItem.Value != "")
                {
                    //if (Filtros == "")
                    //{
                    //    Filtros += " WHERE CLIENTEPROVEEDOR = " + DrCliente.SelectedItem.Value + "";
                    //}
                    //else
                    //{
                    Filtros += " AND NOMBREFISCAL = '" + DrCliente.SelectedItem.Value + "' ";
                    //}
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
                    //if (Filtros == "")
                    //{
                    //    Filtros += " WHERE RUTA = '" + DrRutas.SelectedItem.Value + "'";
                    //}
                    //else
                    //{
                    Filtros += " AND RUTA = '" + DrRutas.SelectedItem.Value + "'";
                    //}
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
                    this.Session["Filtro"] = "";
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

        public static DataTable ConsultaPalet()
        {
            string SQL = "";
            DataTable dt = null;
            try
            {
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  ";
                    SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
                    SQL += " ORDER BY POSICIONCAMION ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ";
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

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
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
            ////Lberror.Text += "Numero de palets: " + i;

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
                if(DivContent is null) { break; }
                //string miro = "Pos.Camión:" + filas["POSICIONCAMION"].ToString() + "\n";
                //miro += "Línea:" + filas["NUMERO_LINEA"].ToString() + "\n";
                //miro += "Empresa:" + filas["EMPRESA"].ToString() + "\n";
                //miro += "Cliente:" + filas["CLIENTEPROVEEDOR"].ToString() + "\n";
                //miro += "Nombre:" + filas["NOMBREFISCAL"].ToString() + "\n";
                //miro += "Articulo:" + filas["ARTICULO"].ToString() + "\n";
                //miro += "Ruta:" + filas["RUTA"].ToString() + "\n";
                //miro += "Número:" + filas["NUMERO"].ToString() + "\n";
                //miro += "uni:" + filas["UDSENCARGA"].ToString() + "\n";

                string miro =  filas["POSICIONCAMION"].ToString() + "\n";
                miro +=  filas["NUMERO_LINEA"].ToString() + "\n";
                miro +=  filas["EMPRESA"].ToString() + "\n";
                miro +=  filas["CLIENTEPROVEEDOR"].ToString() + "\n";
                miro +=  filas["NOMBREFISCAL"].ToString() + "\n";
                miro +=  filas["ARTICULO"].ToString() + "\n";
                miro +=  filas["RUTA"].ToString() + "\n";
                miro +=  filas["NUMERO"].ToString() + "\n";
                miro +=  filas["UDSENCARGA"].ToString() + "\n";

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
            //Lberror.Text = "";
            ////Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

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
            //Lberror.Text += SQL + "1- gvCabecera_Selecciona " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            //Lberror.Text += " 1- gvCabecera_Selecciona " + Variables.mensajeserver;
            //gvLista.DataSource = dt;
            //gvLista.DataBind();
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
            if(ID != "")
            {
                SQL += " AND NUMERO_LINEA = " + ID + " ";
            }
            SQL += " ORDER BY POSICIONCAMION ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            int i = 0;
            string EtiquetaQR = "";
            string EtiquetaUD_BASE = "";


         

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
                    string N = "";
                    SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is System.DBNull)
                    {
                        DLbVariedad0.Text = "Variedad: ";
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
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        EtiquetaUD_BASE = "";
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
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        EtiquetaQR = "NO EXISTE";
                    }
                    else
                    {
                        EtiquetaQR = Con.ToString();
                    }
        
                    N = "";
                    SQL = "SELECT TOP 1 (A.ZEMPRESA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        DLbEmpresa0.Text = "";
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
                    string miro = filas["UDSENCARGA"].ToString().Replace(".", ",");
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
                        Double Value = Convert.ToDouble(CadaLinea[0].ToString() + "." + CadaLinea[1].ToString());
                        DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                    }
                    else
                    {
                        Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
                        DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                    }
                    if(EtiquetaUD_BASE != "")
                    {
                        string [] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
                        int Value = Convert.ToInt32(AA[0]) ;
                        DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
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

                    //Bug Codigo QR si termina en cero hay que añadir un retorno de carro o no se lee
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

                    string N = "";
                    SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        lbbVariedad.Text = "Variedad: NO EXISTE";
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
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        EtiquetaUD_BASE = "";
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
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        EtiquetaQR = "NO EXISTE";
                    }
                    else
                    {
                        EtiquetaQR = Con.ToString();
                    }

                    N = "";
                    SQL = "SELECT TOP 1 (A.ZEMPRESA) ";
                    SQL += " FROM ZEMPRESAVARIEDAD A ";
                    SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
                    SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
                    SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
                    Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                        lbbEmpresa.Text = "NO EXISTE";
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

                        Double Value = Convert.ToDouble(miro);
                        lbbNumerPlanta.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                    }
                    else 
                    {
                        Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString()) * 1000;
                        lbbNumerPlanta.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                    }

                    if (EtiquetaUD_BASE != "")
                    {
                        string[] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
                        int Value = Convert.ToInt32(AA[0]);
                        lbbNumerPlanta.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
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
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            try
            {
                imgBarCode.Height = 200; // 250;
                imgBarCode.Width = 200; // 250;
            }
            catch (Exception a)
            {
                imgBarCode.Height = 200; // 250;
                imgBarCode.Width = 200; // 250;
            }
            using (Bitmap bitMap = qrCode.GetGraphic(40))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                Contenedor.Controls.Add(imgBarCode);
            }
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
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                imgBarCode.Height = 100;
                imgBarCode.Width = 100;
            }
            catch (Exception a)
            {

            }

            using (Bitmap bitMap = qrCode.GetGraphic(40))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                Contenedor.Controls.Add(imgBarCode);
            }
        }

        private void DrPrinters_Click()
        {
            //btnPrintA1.Visible = false;
        }

        private void Carga_tablaNueva()
        {
            string temporal = ""; //Jose
            ////Lberror.Text = "";
            string SQL = "";
            string Filtros = "";
            DataTable dt = null;
            DataTable dtV = null;
            DataTable dtV2 = null;
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
                        //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        SQL = "1- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM [DESARROLLO].[dbo].ZPEDIDOS_COMPRA ";
                        DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");
                        temporal = "1";
                        SQL = Main.BuscaVentasGold("", "");
                        temporal = "2";
                        //Lberror.Text += SQL +  "2- Carga_tablaNueva BuscaGold " + Variables.mensajeserver;
                        dtV = Main.BuscaLoteGold(SQL).Tables[0];
                        temporal = "3";
                        //Lberror.Text += SQL + " 2- termina Carga_tablaNueva BuscaLoteGold " + Variables.mensajeserver;

                        //Datos = " Select 'VRE' AS EMPRESA, C.[Cliente Proveedor] as CLIENTEPROVEEDOR, C.[Nombre Fiscal] as NOMBREFISCAL, C.[Fecha Entrega] as FECHAENTREGA, ";
                        //Datos += " C.Serie as SERIE_PED, C.Numero as NUMERO, L.Línea as LINEA, L.Producto as PRODUCTO, L.Descripcion as DESCRIPCION,";
                        //Datos += " L.[Uds Pedidas] as UDSPEDIDAS, L.[Uds Servidas] as UDSSERVIDAS, ([Uds Pedidas] -[Uds Servidas]) as UDSPENDIENTES ";
 

                        foreach (DataRow fila in dtV.Rows)
                        {
                            SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA,  PRODUCTO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSPENDIENTES, FECHAENTREGA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD )";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["PRODUCTO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",'";
                            SQL +=fila["FECHAENTREGA"].ToString() + "','" + fila["DESCRIPCION"].ToString() + "','";
                            SQL +=fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 A.ZTIPO_PLANTA  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),(";
                            SQL += " SELECT TOP 1 A.ZVARIEDAD  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'))";
                            //Lberror.Text += "3- Carga_tablaNueva " + SQL + Environment.NewLine;
                            string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                            temporal = "4";
                            DBHelper.ExecuteNonQuery(SQL);
                        }



                        SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                        SQL += " FROM ZPEDIDOS_COMPRA  ";
                        temporal = "5";
                        dt = Main.BuscaLote(SQL).Tables[0];
                        //Lberror.Text += "4- Carga_tablaNueva " + SQL + Environment.NewLine;
                    }
                    else
                    {
                        //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        if (temporal == "")
                        {

                            SQL = "5- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM [RIOERESMA].[dbo].ZPEDIDOS_COMPRA ";
                            temporal = "6";
                            DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");

                            SQL = Main.BuscaVentasGold("", "");
                            temporal = "7";
                            //Lberror.Text = "6- Carga_tablaNueva BuscaLoteGold" + Environment.NewLine;
                            dtV = Main.BuscaLoteGold(SQL).Tables[0];
                            temporal = "8";

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD)";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                                SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 B.ZTIPO_PLANTA WHERE  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "),(";
                                SQL += " SELECT TOP 1 B.ZVARIEDAD WHERE  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "))";
                                //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                                string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
                        }
                        temporal = "9";
                        SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                        SQL += " FROM ZPEDIDOS_COMPRA  ";

                        temporal = "10";

                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                }
                else //Si tiene filtros
                {
                    if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                    {
                        DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");

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
                            temporal = "11";
                            SQL = Main.BuscaVentasGold(this.Session["FiltroEmpresa"].ToString(), Miro);
                        }
                        else
                        {
                            temporal = "12";
                            SQL = Main.BuscaVentasGold("", Miro);
                        }
                        temporal = "13";
                        dtV = Main.BuscaLoteGold(SQL).Tables[0];
                        temporal = "14";
                        foreach (DataRow fila in dtV.Rows)
                        {
                            SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD)";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                            SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                            SQL += fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 B.ZTIPO_PLANTA WHERE  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "),(";
                            SQL += " SELECT TOP 1 B.ZVARIEDAD WHERE  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "))";
                            //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                            string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                            DBHelper.ExecuteNonQuery(SQL);
                        }

                        temporal = "15";
                        SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                        SQL += " FROM ZPEDIDOS_COMPRA  ";
                        dt = Main.BuscaLote(SQL).Tables[0];

                    }
                    else
                    {

                        //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        if (temporal == "")
                        {
                            DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");

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
                                temporal = "17";
                                SQL = Main.BuscaVentasGold(this.Session["FiltroEmpresa"].ToString(), Miro);

                            }
                            else
                            {
                                temporal = "18";
                                SQL = Main.BuscaVentasGold("", Miro);
                            }
                            temporal = "19";
                            dtV = Main.BuscaLoteGold(SQL).Tables[0];

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD)";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
                                SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 B.ZTIPO_PLANTA WHERE  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "),(";
                                SQL += " SELECT TOP 1 B.ZVARIEDAD WHERE  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = " + fila["PRODUCTO"].ToString().Trim() + "))";
                                //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                                string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD ";
                            SQL += " FROM ZPEDIDOS_COMPRA  ";
                            temporal = "20";
                            dt = Main.BuscaLote(SQL).Tables[0];
                        }
                    }
                }

                //busca Error donde no se puede depurar
                ////Lberror.Text += " Mirar: " + Variables.Error + " " + SQL;
                //Calcula con lo que tiene en Lista de carga camión
                //dt = Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), TxtNumero.Text);

                this.Session["MiConsulta"] = dt;

                gvControl.DataSource = dt;
                gvControl.DataBind();
                gvAmbos.DataSource = dt;
                gvAmbos.DataBind();
                gvHorizontal.DataSource = dt;
                gvHorizontal.DataBind();

                //busca Error donde no se puede depurar
                //Lberror.Visible = true;

                //Lberror.Text = "";
            }
            catch (Exception mm)
            {
                Variables.Error += mm.Message;
                string a = Main.Ficherotraza("Error ZPEDIDOS_COMPRA --> Paso:" + temporal + " -->Error:" + mm.Message + " --> SQL:" + SQL);
                //Lberror.Visible = true;
                //Lberror.Text += ". Error: " + mm.Message;
            }


        }

        private static DataTable Calcula_OrdenesCarga(DataTable dt, string Estado, string Numero)
        {
            return null;
            string SQL = "";
            try
            {                
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
                    SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
                    SQL += " ZCARGA_LINEA  WHERE ESTADO < 3  AND ID_CABECERA = " + Numero ;
                    SQL += "  ORDER BY NUMERO_LINEA ";               
                }
                else
                {
                    SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
                    SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
                    SQL += " FROM [RIOERESMA].[dbo].ZCARGA_LINEA  WHERE ESTADO < 3  AND ID_CABECERA = " + Numero;
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


        protected void gvLista_Sube(string Index)
        {
            //GridViewRow row = gvLista.Rows[Convert.ToInt32(Index)];
            //string miro = gvLista.DataKeys[Convert.ToInt32(Index)].Value.ToString();
            ////sube la linea a la orden
            //string Numero = "";
            //decimal UNIDADES = 1.0M;
            ////string Mira = "";
            ////string SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + this.Session["IDGridB"].ToString();


            ////Mira = Server.HtmlDecode(row.Cells[10].Text);
            ////if (Mira != "")
            ////{
            ////    UNIDADES = Convert.ToDecimal(Mira.Replace(".", ","));
            ////}
            ////Mira = Server.HtmlDecode(row.Cells[13].Text);
            ////if (Mira != "")
            ////{
            ////    Numero = Mira.Replace(".", ",");
            ////}

            //TextBox txtBox = (TextBox)gvLista.FindControl("TabLCarga"); //antes 10
            //if (txtBox != null)
            //{
            //    UNIDADES = Convert.ToDecimal(txtBox.Text);
            //}
            //txtBox = (TextBox)gvLista.FindControl("TabLNumLinea"); //antes 13
            //if (txtBox != null)
            //{
            //    Numero = txtBox.Text;
            //}
            ////DBHelper.ExecuteNonQuery(SQL);

            //string SQL = "UPDATE ZCARGA_ORDEN SET (UNIDADESENCARGA = UNIDADESENCARGA + UNIDADES) WHERE ID_CABECERA = " + miro;

            ////Lberror.Text += SQL + "1- gvLista_Sube " + Variables.mensajeserver;
            //DBHelper.ExecuteNonQuery(SQL);
            ////Lberror.Text += " 1- gvLista_Sube " + Variables.mensajeserver;
            

            //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID_SECUENCIA = " + miro + " AND NUMERO_LINEA = " + Numero;


            //Carga_tabla();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
        }

        protected void gvLista_Camion(string Index)
        {

            //GridViewRow row = (GridViewRow)gvLista.Rows[Convert.ToInt32(Index)];

            //string miro = gvLista.DataKeys[Convert.ToInt32(Index)].Value.ToString();
            //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            //SQL += " WHERE ID = " + miro;

            //DBHelper.ExecuteNonQuery(SQL);
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
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

            //Lberror.Text += SQL + "1- gvControl_BajaOrden " + Variables.mensajeserver;
            Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            //Lberror.Text += " 1- gvControl_BajaOrden " + Variables.mensajeserver;
            
            if (Con is System.DBNull)
            {
                N = 1;
            }
            else
            {
                N = Convert.ToInt32(Con) + 1;
            }
            //Lberror.Text += SQL + "2- gvControl_BajaOrden " + Variables.mensajeserver;
            Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            //Lberror.Text += " 2- gvControl_BajaOrden " + Variables.mensajeserver;
            
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

                //Lberror.Text += SQL + "3- gvControl_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 3- gvControl_BajaOrden " + Variables.mensajeserver;
                

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

                //Lberror.Text += SQL + "4- gvControl_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 4- gvControl_BajaOrden " + Variables.mensajeserver;

                //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
                //DBHelper.ExecuteNonQuery(SQL);
                NUMPALET = NUMPALET - Unidad;
            }

            SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            //Lberror.Text += SQL + "5- gvControl_BajaOrden " + Variables.mensajeserver;
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " 5- gvControl_BajaOrden " + Variables.mensajeserver;

            this.Session["NumeroPalet"] = Linea.ToString();

            Carga_tabla();
            //Carga_tablaLista();

            gvControl.EditIndex = -1;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            gvControl.DataBind();
            gvAmbos.EditIndex = -1;
            gvAmbos.DataBind();
            gvHorizontal.EditIndex = -1;
            gvHorizontal.DataBind();



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