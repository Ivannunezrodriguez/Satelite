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
using ClosedXML;

//EXEC master..xp_cmdshell 'BCP DESARROLLO.dbo.ZARCHIVOS OUT C:\Rioeresma\Exportacion\Demo.csv -T -c'    //Separador Tabulador
//EXEC master..xp_cmdshell 'BCP DESARROLLO.dbo.ZARCHIVOS OUT C:\Rioeresma\Exportacion\Demo.csv -T -c -t;'    //Separador ;
//E:\Golden\Golden .NET\Datos\Trabajadores\2022\03436550M Ruta a Golden DNI usuario 
//\\192.168.1.80\Golden\Golden .NET\Datos\Trabajadores\2022\03436550M Ruta a Golden DNI usuario 

//using NovaCode;    https://www.c-sharpcorner.com/UploadFile/07c1e7/convert-pdf-to-word-using-C-Sharp/
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security;

namespace Satelite
{
    public partial class FlujosDatosCopia : System.Web.UI.Page
    {
#pragma warning disable CS0414 // El campo 'FlujosDatosCopia.dtsE' está asignado pero su valor nunca se usa
        Reports.RCJornalHora dtsE = null;
#pragma warning restore CS0414 // El campo 'FlujosDatosCopia.dtsE' está asignado pero su valor nunca se usa
#pragma warning disable CS0414 // El campo 'FlujosDatosCopia.dtsP' está asignado pero su valor nunca se usa
        Reports.RCProdEnvDia dtsP = null;
#pragma warning restore CS0414 // El campo 'FlujosDatosCopia.dtsP' está asignado pero su valor nunca se usa
#pragma warning disable CS0414 // El campo 'FlujosDatosCopia.dtsN' está asignado pero su valor nunca se usa
        Reports.RCPrintNominas dtsN = null;
#pragma warning restore CS0414 // El campo 'FlujosDatosCopia.dtsN' está asignado pero su valor nunca se usa



        private string ElID = "";
        private string ElOrden = "";

        public string ImgFlujo = "";
        public string ImgEstado = "";

        static TextBox[] ArrayTextBoxs;
        static Label[] ArrayLabels;
        static DropDownList[] ArrayCombos;
        //static int contadorControles;
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

                if (this.Session["MiNivel"].ToString() == "9")
                {
                    //Nominas.Visible = true;
                }
        
                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    Server.Transfer("Login.aspx"); //Default
                }



                if (!IsPostBack)
                {
                    //desarrollo pruebas usuario basico
                    //this.Session["MiNivel"] = "0";                    
                    this.Session["SelectMes"] = "0";
                    DvPreparado.Visible = false;
                    cuestion.Visible = false;
                    Asume.Visible = false;
                    this.Session["TablaObj"] = "";
                    this.Session["idarchivo"] = "";
                    this.Session["collapse1"] = "1";
                    this.Session["idregistro"] = "0";
                    this.Session["idflujo"] = "";
                    this.Session["idestado"] = "";
                    this.Session["TablaName"] = "";
                    this.Session["Query1"] = "";
                    this.Session["Idprocedimiento"] = "";
                    this.Session["DocumentacionAdjunta"] = "";
                    this.Session["CampoFiltro"] = "";
                    this.Session["iddocumento"] = "";
                    this.Session["Registros"] = "0";
                    this.Session["CondicionEstado"] = "";

                    this.Session["NombreCompleto"] = "";
                    this.Session["FiltroCondicion"] = "";
                    this.Session["FiltroConsulta"] = 1;
                    this.Session["FaltaDato"] = 0;
                    this.Session["Campos"] = 0;
                    this.Session["Pagina"] = "";

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
                    this.Session["Menu"] = "0";//1
                    this.Session["IDCabecera"] = "0";
                    this.Session["CalculoPaletPlanta"] = "";
                    this.Session["filtrolocal"] = "";
                    this.Session["Delegate"] = "";
                    this.Session["EstadoCabecera"] = "";
                    this.Session["FiltroCodEmpleado"] = "";
                    this.Session["FiltroGral"] = "";
                    this.Session["UltimaConsulta"] = "";
                    this.Session["UltimaConsultaFin"] = "";
                    this.Session["SQL"] = "";
                    this.Session["Erroneo"] = "0";
                    this.Session["Centro"] = "";
                    //TxtNumero.Enabled = false;
                    DataTable dt = new DataTable();
                    this.Session["TablaLista"] = dt;
                    //ChkSlot.Visible = false;
                    Variables.mensajeserver = "";
                    ArrayTextBoxs = new TextBox[20];
                    ArrayCombos = new DropDownList[20];
                    ArrayLabels = new Label[20];
                    //contadorControles = 0;
                    Lberror.Visible = false;

                    Campos_ordenados();

                    if (this.Session["MiNivel"].ToString() == "0")
                    {
                        //PanelGralFiltro
                        DrVistaEmpleado.Enabled = true;
                        DrFlujo.Enabled = true;
                        DrFlujoEstado.Enabled = true;
                        //ImageEjecucion.Enabled = false;
                        //ImagenEstado.Enabled = false;
                        //ImgDetiene.Enabled = false;
                        //ImgInicia.Enabled = false;
                        //ImgInicia.Visible = false;
                        //LinkFlujo.Enabled = false;
                        ImgAyuda.Enabled = false;
                        //Button1.Enabled = true;
                        BtAtras.Enabled = true;
                        BtAtras.Visible = false;
                        TxtDesde.Enabled = false;
                        TxtHasta.Enabled = false;
                        ImgFechamenos.Enabled = false;
                        DrAno.Enabled = false;
                        DrMeses.Enabled = false;
                        ImgFechamas.Enabled = false;
                        ImgOrdenMin.Visible = false;
                        BtGralConsulta.Visible= false;
                        DivGralConsulta.Visible = false;

                        //De momento el estado y el Flujo se meten a pelo
                        Carga_tablaArchivos();
                        //DrVistaEmpleado_SelectedIndexChanged(sender, e);
                        //DrFlujo.SelectedIndex = 1;
                        //DrFlujo_Changed(sender, e);
                        //DrFlujoEstado.SelectedIndex = 0;
                        //DrFlujoEstado_Changed(sender, e);

                        UltimaConsulta(null);
                        checkListas_Click(null, null);
                        //Carga_ConsultaUser();
                        //CreaGridFiles(this.Session["TablaObj"].ToString());
                    }
                    else
                    {
                        DrVistaEmpleado.Enabled = true;
                        DrFlujo.Enabled = true;
                        DrFlujoEstado.Enabled = true;
                        //ImageEjecucion.Enabled = true;
                        //ImagenEstado.Enabled = true;
                        //ImgDetiene.Enabled = true;
                        //ImgInicia.Enabled = true;
                        //LinkFlujo.Enabled = true;
                        ImgAyuda.Enabled = true;
                        //Button1.Enabled = true;
                        BtAtras.Enabled = true;
                        BtAtras.Visible = true;
                        TxtDesde.Enabled = true;
                        TxtHasta.Enabled = true;
                        ImgFechamenos.Enabled = true;
                        DrAno.Enabled = true;
                        DrMeses.Enabled = true;
                        ImgFechamas.Enabled = true;
                        ImgOrdenMin.Visible = true;
                        BtGralConsulta.Visible = true;
                        DivGralConsulta.Visible = true;
                        //Carga_Tabla_Empleados();
                        Carga_tablaArchivos();
                        UltimaConsulta(null);
                        checkListas_Click(null, null);
                    }
                }

                //if (this.Session["MiNivel"].ToString() == "0")
                //{
                //    PanelGralFiltro.Visible = false;
                //    DivTreeDoc.Visible = false;
                //    DivFicheros.Visible = false;
                //    PanelCabecera.Visible = false;
                //    DivArchivos.Visible = false;
                //    DivUser.Visible = true;
                //    DivCampos0.Visible = true;
                //    Lbusuario.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                //string a = Main.Ficherotraza("Page_load --> " + ex.Message);
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas("", "1", " Firma.Page_load --> Error:" + ex.Message);

                if (this.Session["Error"].ToString() == "0")
                {
                    //Server.Transfer("Login.aspx");
                Server.Transfer("Login.aspx");
                }
                else
                {
                    //Server.Transfer("thEnd.aspx");
                    Server.Transfer("thEnd.aspx");
                }
            }
        }


        private void Carga_ConsultaUser()
        {

        }
        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            //Server.Transfer("RecoNomina.aspx");
            Server.Transfer("Nomina.aspx");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }


        protected void BtMarcaError_Click(object sender, EventArgs e)
        {
            if (this.Session["Erroneo"].ToString() == "0")
            {
                this.Session["Erroneo"] = "1";
            }
            else
            {
                this.Session["Erroneo"] = "0";
            }
            //Carga_panelTareas();
        }

        protected void BtFilEmpleado_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "1";
            Carga_tablaEmpleados();
        }

  

        protected void BtContiene_Click(Object sender, EventArgs e) //  1
        {
            HtmlButton btn = (HtmlButton)sender;
            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();
            DropDownList Cb = new DropDownList();
            Boolean Esta = false;

            //if (btn.ID == "BtCodigo")
            //{
            //    Ia = (HtmlGenericControl)IContent;
            //    Tx = (TextBox)TxtCodigo;
            //}
            //if (btn.ID == "BtNombre")
            //{
            //    Ia = (HtmlGenericControl)INombre;
            //    Tx = (TextBox)TxtNombre;
            //}
            //if (btn.ID == "BtApellido")
            //{
            //    Ia = (HtmlGenericControl)IApellido;
            //    Tx = (TextBox)TxtApellidos;
            //}
            //if (btn.ID == "BtCentro")
            //{
            //    Ia = (HtmlGenericControl)ICentro;
            //    Tx = (TextBox)TxtCentro;
            //}
            //if (btn.ID == "BtCategoria")
            //{
            //    Ia = (HtmlGenericControl)ICategoria;
            //    Tx = (TextBox)TxtCategoria;
            //}
            //if (btn.ID == "BtVivienda")
            //{
            //    Ia = (HtmlGenericControl)IVivienda;
            //    Tx = (TextBox)TxtVivienda;
            //}
            
            Ia.Attributes.Add("style", "");
            Tx.Attributes.Add("style", "background-color:#e6f2e1;");

            //if (this.Session["Delegate"].ToString() == "")
            //{
            if (Ia.Attributes["class"] == "fa fa-circle fa-2x")
            {
                Ia.Attributes["title"] = "No contiene estos datos.";
                Ia.Attributes["class"] = "fa fa-circle-o fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                Ia.Attributes["title"] = "No incluye en su contenido este datos.";
                Ia.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-circle-o fa-2x")
            {
                Ia.Attributes["title"] = "Incluye en su contenido este dato.";
                Ia.Attributes["class"] = "fa fa-adjust fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            {
                Ia.Attributes["title"] = "Menor que";
                Ia.Attributes["class"] = "fa fa-chevron-left fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-chevron-left fa-2x")
            {
                Ia.Attributes["title"] = "Mayor que";
                Ia.Attributes["class"] = "fa fa-chevron-right fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-chevron-right fa-2x")
            {
                Ia.Attributes["title"] = "Distinto de";
                Ia.Attributes["class"] = "fa fa-arrows-alt fa-2x";
            }
            else if (Ia.Attributes["class"] == "fa fa-arrows-alt fa-2x")
            {
                Ia.Attributes["title"] = "Desactivado";
                Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
                Ia.Attributes.Add("style", "color:darkred;");
                Tx.Attributes.Add("style", "");
            }
            else if (Ia.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            {
                Ia.Attributes["title"] = "Contiene estos datos";
                Ia.Attributes["class"] = "fa fa-circle fa-2x";
            }
            //}

            if (Esta == true)
            {
                if (Cb.SelectedItem.Value == "Ninguno")
                {
                    Tx.Attributes.Add("style", "");
                }
            }
        }

        protected void lbFilClose_Click(Object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            //if (btn.ID == "lbFilCodigo")
            //{
            //    Ia = (HtmlGenericControl)IContent;
            //    Tx = (TextBox)TxtCodigo;
            //}
            //else if (btn.ID == "LinkNombre")
            //{
            //    Ia = (HtmlGenericControl)INombre;
            //    Tx = (TextBox)TxtNombre;
            //}
            //else if (btn.ID == "LinkApellidos")
            //{
            //    Ia = (HtmlGenericControl)IApellido;
            //    Tx = (TextBox)TxtApellidos;
            //}
            //else if (btn.ID == "LinkCentro")
            //{
            //    Ia = (HtmlGenericControl)ICentro;
            //    Tx = (TextBox)TxtCentro;
            //}
            //else if (btn.ID == "LinkCategoria")
            //{
            //    Ia = (HtmlGenericControl)ICategoria;
            //    Tx = (TextBox)TxtCategoria;
            //}
            //else if (btn.ID == "LinkVivienda")
            //{
            //    Ia = (HtmlGenericControl)IVivienda;
            //    Tx = (TextBox)TxtVivienda;
            //}
          
            Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            Ia.Attributes["title"] = "Desactivado";
            Ia.Attributes.Add("style", "color:darkred;");
            Tx.Attributes.Add("style", "");
            Tx.Text = "";
        }


        protected void BtLimpiaTodo_Click(Object sender, EventArgs e)
        {
            //IContent.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //INombre.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //IApellido.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //ICentro.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //ICategoria.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            //IVivienda.Attributes["class"] = "fa fa-hand-o-up fa-2x";
    


            //IContent.Attributes["title"] = "Desactivado";
            //INombre.Attributes["title"] = "Desactivado";
            //IApellido.Attributes["title"] = "Desactivado";
            //ICentro.Attributes["title"] = "Desactivado";
            //ICategoria.Attributes["title"] = "Desactivado";
            //IVivienda.Attributes["title"] = "Desactivado";
     

            //IContent.Attributes.Add("style", "color:darkred;");
            //INombre.Attributes.Add("style", "color:darkred;");
            //IApellido.Attributes.Add("style", "color:darkred;");
            //ICentro.Attributes.Add("style", "color:darkred;");
            //ICategoria.Attributes.Add("style", "color:darkred;");
            //IVivienda.Attributes.Add("style", "color:darkred;");
 
            //TxtCodigo.Text = "";
            //TxtNombre.Text = "";
            //TxtApellidos.Text = "";
            //TxtCentro.Text = "";
            //TxtCategoria.Text = "";
            //TxtVivienda.Text = "";  

            //TxtCodigo.Attributes.Add("style", "background-color:#ffffff;");
            //TxtNombre.Attributes.Add("style", "background-color:#ffffff;");
            //TxtApellidos.Attributes.Add("style", "background-color:#ffffff;");
            //TxtCentro.Attributes.Add("style", "background-color:#ffffff;");
            //TxtCategoria.Attributes.Add("style", "background-color:#ffffff;");
            //TxtVivienda.Attributes.Add("style", "background-color:#ffffff;");

            //DrVistaEmpleado.Items.Clear();
        }

        protected void checknada_Click(Object sender, EventArgs e)
        {

        }
        protected void BtFiltroDefault_Click(Object sender, EventArgs e)
        {
            //IContent.Attributes["class"] = "fa fa-circle fa-2x";
            //INombre.Attributes["class"] = "fa fa-circle fa-2x";
            //IApellido.Attributes["class"] = "fa fa-circle fa-2x";
            //ICentro.Attributes["class"] = "fa fa-circle fa-2x";
            //ICategoria.Attributes["class"] = "fa fa-circle fa-2x";
            //IVivienda.Attributes["class"] = "fa fa-circle fa-2x";

            //IContent.Attributes["title"] = "No deberá contener estos Datos.";
            //INombre.Attributes["title"] = "Deberá contener estos Datos";
            //IApellido.Attributes["title"] = "Deberá contener estos Datos";
            //ICentro.Attributes["title"] = "Deberá contener estos Datos";
            //ICategoria.Attributes["title"] = "Deberá contener estos Datos";
            //IVivienda.Attributes["title"] = "Deberá contener estos Datos";
          
            //TxtCodigo.Text = "0020999, 20208, 100008, 100011, 100015, 100029, 100329";
            //TxtNombre.Text = "";
            //TxtApellidos.Text = "";
            //TxtCentro.Text = "";
            //TxtCategoria.Text = "";
            //TxtVivienda.Text = "";
        }


        protected void check1_Click(Object sender, EventArgs e) //  1
        {
            //if (this.Session["collapse1"].ToString() == "1")
            //{
            //    this.Session["collapse1"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse1"] = "1";
            //}
            //DejaPosAcordeon();
        }
        protected void check2_Click(Object sender, EventArgs e)//  1
        {
            //if (this.Session["collapse2"].ToString() == "1")
            //{
            //    this.Session["collapse2"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse2"] = "1";
            //}
            //DejaPosAcordeon();
        }
        protected void check3_Click(Object sender, EventArgs e)//  1
        {
            //if (this.Session["collapse3"].ToString() == "1")
            //{
            //    this.Session["collapse3"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse3"] = "1";
            //}
            //DejaPosAcordeon();
        }
        protected void check4_Click(Object sender, EventArgs e)//1
        {
            //if (this.Session["collapse4"].ToString() == "1")
            //{
            //    this.Session["collapse4"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse4"] = "1";
            //}
            //DejaPosAcordeon();
        }
        protected void check5_Click(Object sender, EventArgs e) // 1
        {
            //if (this.Session["collapse5"].ToString() == "1")
            //{
            //    this.Session["collapse5"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse5"] = "1";
            //}
            //DejaPosAcordeon();
        }
        protected void check6_Click(Object sender, EventArgs e) //1
        {
            //if (this.Session["collapse6"].ToString() == "1")
            //{
            //    this.Session["collapse6"] = "0";
            //}
            //else
            //{
            //    this.Session["collapse6"] = "1";
            //}
            //DejaPosAcordeon();
        }


        private void Campos_ordenados()
        {
            ddCabeceraPageSize.Items.Clear();
            ddCabeceraPageSize.Items.Insert(0, new ListItem("5", "5"));
            ddCabeceraPageSize.Items.Insert(1, new ListItem("10", "10"));
            ddCabeceraPageSize.Items.Insert(2, new ListItem("15", "15"));
            ddCabeceraPageSize.Items.Insert(3, new ListItem("30", "30"));
            ddCabeceraPageSize.Items.Insert(4, new ListItem("50", "50"));
            ddCabeceraPageSize.Items.Insert(5, new ListItem("Todos", "1000"));

            DrMeses.Items.Clear();
            DrMeses.Items.Insert(0, new ListItem("Enero","0"));
            DrMeses.Items.Insert(1, new ListItem("Febrero", "1"));
            DrMeses.Items.Insert(2, new ListItem("Marzo", "2"));
            DrMeses.Items.Insert(3, new ListItem("Abril", "3"));
            DrMeses.Items.Insert(4, new ListItem("Mayo", "4"));
            DrMeses.Items.Insert(5, new ListItem("Junio", "5"));
            DrMeses.Items.Insert(6, new ListItem("Julio", "6"));
            DrMeses.Items.Insert(7, new ListItem("Agosto", "7"));
            DrMeses.Items.Insert(8, new ListItem("Septiembre", "8"));
            DrMeses.Items.Insert(9, new ListItem( "Ocutbre", "9"));
            DrMeses.Items.Insert(10, new ListItem( "Noviembre", "10"));
            DrMeses.Items.Insert(11, new ListItem( "Diciembre", "11"));

            DrAno.Items.Clear();
            DrAno.DataValueField = "MiYEAR";
            DrAno.DataTextField = "MiYEAR";

            string Data = "SELECT '20' + ZANO as MiYEAR FROM ANO_AGRICOLA ORDER BY ZANO DESC";
            DataTable dtV = Main.BuscaLote(Data).Tables[0];
            DrAno.DataSource = dtV;
            DrAno.DataBind();
            DrAno.SelectedIndex = 0;

            //DrAno.Items.Insert(0, new ListItem("2022", "0"));
            //DrAno.Items.Insert(1, new ListItem("2021", "1"));
            //DrAno.Items.Insert(2, new ListItem("2020", "2"));
            //DrAno.Items.Insert(3, new ListItem("2019", "3"));
            //DrAno.SelectedIndex = 0;

            DrDispositivos.Items.Clear();
            string SQL = " SELECT ZID, ZDESCRIPCION FROM  ZDISPOSITIVOS ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];

            DrDispositivos.DataValueField = "ZID";
            DrDispositivos.DataTextField = "ZDESCRIPCION";
            DrDispositivos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DrDispositivos.DataSource = dt1;
            DrDispositivos.DataBind();
            DrDispositivos.SelectedIndex = 0;

            for (int i = 0; i < DrDispositivos.Items.Count; i++)
            {
                if (DrDispositivos.Items[i].Text == this.Session["ComputerName"].ToString())
                {
                    DrDispositivos.SelectedValue = DrDispositivos.Items[i].Value;
                    break;
                }
            }
        }

        private void Rechazados(GridView Datos)
        {
            if (Datos.Rows.Count > 0)
            {
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    BtAtras.Visible = false;
                }
                else
                {
                    BtAtras.Visible = true;
                }
            }
            else
            {
                BtAtras.Visible = false;
            }
           
        }

        protected void Limpiar_Click(Object sender, EventArgs e)
        {
            //ImgDetiene.Visible = false;
            //ImgInicia.Visible = false;
            LbAutomatico.Visible = false;
            LbimgEstado.Visible = false;
            //ImagenEstado.Visible = false;
            //ImageEjecucion.Visible = false;
            DataTable dt = null;
            gvEmpleado.DataSource = dt;
            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);
           
            gvLista.DataSource = dt;
            gvLista.DataBind();
            lbRowEmpleado.Text= "Registros 0";
        }


        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu0") { this.Session["Menu"] = "0"; }
            if (Miro == "aMenu1") { this.Session["Menu"] = "1"; }
            if (Miro == "aMenu2") { this.Session["Menu"] = "2"; }
            if (Miro == "aMenu3") { this.Session["Menu"] = "3"; }
            if (Miro == "aMenu4") { this.Session["Menu"] = "4"; }
            if (Miro == "aMenu5") { this.Session["Menu"] = "5"; }
            if (Miro == "aMenu6") { this.Session["Menu"] = "6"; }
            if (Miro == "aMenu7") { this.Session["Menu"] = "7"; }
            if (Miro == "aMenu8") { this.Session["Menu"] = "8"; }
            if (Miro == "aMenu9") { this.Session["Menu"] = "9"; }
            if (Miro == "aMenu10") { this.Session["Menu"] = "10"; }
            if (Miro == "aMenu11") { this.Session["Menu"] = "11"; }
        }

        protected void btnSing_Click(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                string fileName = "Firma-RioEresma.exe";
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", fileName));
                Response.WriteFile(Server.MapPath(@"~/bin/Firma-RioEresma.exe"));
                Response.End();
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }
        protected void BtnCambioficha_Click(object sender, EventArgs e)
        {
            if(DivGridDoc.Visible == false)
            {
                DivGridDoc.Visible = true;
                DivFicha.Visible = false;
                BtdocAdjunto.Text = "Protección de Datos";
                H3TituFicha.Visible = false;
                H3Titulo.Visible = true;
                BtOpenFicha.Visible=false;

            }
            else
            {
                DivGridDoc.Visible = false;
                DivFicha.Visible = true;
                BtdocAdjunto.Text = "Documentación Adjunta";
                H3TituFicha.Visible = true;
                H3Titulo.Visible = false;
                BtOpenFicha.Visible = true;
            }
        }
        
        protected void DrPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BuscaAnchor_Click(object sender, EventArgs e)
        {

        }
        protected void DrFlujo_Changed(object sender, EventArgs e)
        {
            //ImgInicia.Visible = false;
            LbAutomatico.Visible = false;
            this.Session["idflujo"] = DrFlujo.SelectedItem.Value;

            DataTable dt = Main.CargaEstadosFlujos(DrFlujo.SelectedItem.Value, "0").Tables[0];
            DrFlujoEstado.Items.Clear();
            DrFlujoEstado.DataValueField = "ZID";
            DrFlujoEstado.DataTextField = "ZNOMBRE";
            DrFlujoEstado.Items.Insert(0, new ListItem("Ninguno", "0"));
            DrFlujoEstado.DataSource = dt;
            DrFlujoEstado.DataBind();
            DrFlujoEstado.SelectedIndex = 0;
            //ImageEjecucion.Visible = false;
            //ImagenEstado.Visible = true;

            DrFlujoEstado_Changed(null, null);

        }



        protected void Btfiltra_Click(object sender, EventArgs e)
        {
            Carga_tablaEmpleados();

        }
        private void Mira_CondicionesGral()
        {
            this.Session["FiltroGral"] = ""; //empleado, nombre, apellido, centro, categoria, vivienda, envase, variedad, zona
            //if (IContent.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] = "0-";
            //}
            //else if (IContent.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] = "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] = "2-";
            //}

            //if (INombre.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] += "0-";
            //}
            //else if (INombre.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] += "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] += "2-";
            //}

            //if (IApellido.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] += "0-";
            //}
            //else if (IApellido.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] += "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] += "2-";
            //}

            //if (ICentro.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] += "0-";
            //}
            //else if (ICentro.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] += "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] += "2-";
            //}

            //if (ICategoria.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] += "0-";
            //}
            //else if (ICategoria.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] += "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] += "2-";
            //}

            //if (IVivienda.Attributes["class"] == "fa fa-circle fa-2x")
            //{
            //    this.Session["FiltroGral"] += "0-";
            //}
            //else if (IVivienda.Attributes["class"] == "fa fa-adjust fa-2x")
            //{
            //    this.Session["FiltroGral"] += "1-";
            //}
            //else
            //{
            //    this.Session["FiltroGral"] += "2-";
            //}
        }

        private void UltimaConsulta(DataTable dt1)
        {
            if(dt1 == null)
            {
                string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA, ZQUERY, ZID_PROCEDIMIENTO, ZCAMPODOC, ";
                SQL += " ZCAMPOFILTRO, ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS FROM  ZPROFILES ";
                SQL += " WHERE ZID_ARCHIVO = 0 ";

                dt1 = Main.BuscaLote(SQL).Tables[0];
            }
            LbUltConsulta.Text = "";
            LdDia.Text = "";
            if(dt1.Rows.Count > 0)
            {
                foreach (DataRow fila in dt1.Rows)
                {
                    if (fila["ZFECHA"].ToString() != "")
                    {
                        string miro = fila["ZFECHA"].ToString();
                        LbUltConsulta.Text = "Última consulta: " + fila["ZFECHAINIRECO"].ToString().Substring(0, 10) + " - " + fila["ZFECHAFINRECO"].ToString().Substring(0, 10);
                        LdDia.Text = " Fecha consulta: " + fila["ZFECHA"].ToString();
                    }
                    else
                    {
                        LbUltConsulta.Text = "Última consulta: " + fila["ZFECHAINIRECO"].ToString().Substring(0, 10) + " - " + fila["ZFECHAFINRECO"].ToString().Substring(0, 10);
                    }
                    this.Session["Idprocedimiento"] = fila["ZID_PROCEDIMIENTO"].ToString();
                    this.Session["UltimaConsultaFin"] = fila["ZFECHAINIRECO"].ToString().Substring(0, 10);
                    this.Session["UltimaConsulta"] = fila["ZFECHAFINRECO"].ToString().Substring(0, 10);
                    this.Session["Query1"] = fila["ZQUERY"].ToString();
                    string a = this.Session["Query1"].ToString();
                    this.Session["DocumentacionAdjunta"] = fila["ZCAMPODOC"].ToString();
                    this.Session["CampoFiltro"] = fila["ZCAMPOFILTRO"].ToString();
                    this.Session["FiltroCondicion"] = fila["ZFILTROCONDICION"].ToString();
                    this.Session["RutaServer"] = fila["ZDOCUMENTOS"].ToString();
                    this.Session["RutaModelosPDF"] = fila["ZDIRECTORIOS"].ToString();
                    break;
                }
            }
        }

        protected void BtFechaMas_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            DateTime date = DateTime.Now;

            string Miyear = "20" + this.Session["Ano"].ToString();

            if (Img.ID == "ImgFechamenos")
            {
                //Resta meses
            
                if(DrMeses.SelectedIndex == 0)
                {
                    DrMeses.SelectedIndex = 11;
                    this.Session["Ano"] = (Convert.ToInt32(this.Session["Ano"].ToString()) - 1).ToString();
                    this.Session["Mes"] = "11";
                    Miyear = "20" + this.Session["Ano"].ToString();
                    for (int i = 0; i < DrAno.Items.Count; i++)
                    {
                        if (DrAno.Items[i].Text == Miyear)
                        {
                            DrAno.SelectedValue = DrAno.Items[i].Value;
                            break;
                        }
                    }
                }
                else
                {
                    DrMeses.SelectedIndex = DrMeses.SelectedIndex - 1;
                    this.Session["Mes"] = DrMeses.SelectedIndex.ToString();
                }

                //DrMeses.SelectedIndex = Convert.ToInt32(this.Session["SelectMes"].ToString()) - 1;
                //this.Session["SelectMes"] = (DrMeses.SelectedIndex + 1 ).ToString();
                DateTime oPrimerDiaDelMes = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 1);
                TxtDesde.Text = oPrimerDiaDelMes.ToString().Substring(0, 10);
                DateTime dLastDayOfLastMonth = DateTime.Now;
                DateTime dtUltimodia = dLastDayOfLastMonth.AddDays(-1);

                if (Convert.ToInt32(this.Session["Mes"].ToString()) >= 11)
                {
                    dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 31);
                    dtUltimodia = dLastDayOfLastMonth.AddDays(0);
                }
                else
                {
                    dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 2, 1);
                    dtUltimodia = dLastDayOfLastMonth.AddDays(-1);
                }


                //DateTime dLastDayOfLastMonth = new DateTime(Convert.ToInt32(this.Session["Ano"].ToString()), Convert.ToInt32(this.Session["Mes"].ToString()) + 2, 1);
                //DateTime dtUltimodia = dLastDayOfLastMonth.AddDays(-1);
                TxtHasta.Text = dtUltimodia.ToString().Substring(0, 10);

                //DateTime dFirstDayOfThisMonth = DateTime.Today.AddDays(-(DateTime.Today.Day - Convert.ToInt32(this.Session["SelectMes"].ToString())));
                //DateTime dLastDayOfLastMonth = oPrimerDiaDelMes.AddDays(-1);

                //DateTime dFirstDayOfLastMonth = dFirstDayOfThisMonth.AddMonths(-Convert.ToInt32(this.Session["SelectMes"].ToString()));
                //TxtDesde.Text = dFirstDayOfLastMonth.ToString().Substring(0, 10);
                //TxtHasta.Text = dLastDayOfLastMonth.ToString().Substring(0, 10);
                ImgFechamas.Visible = true;
            }
            else
            {
                //suma meses
                //si es igual al actual.
                if (DrMeses.SelectedIndex == 11)
                {
                    DrMeses.SelectedIndex = 0;
                    this.Session["Ano"] = (Convert.ToInt32(this.Session["Ano"].ToString()) + 1).ToString();
                    this.Session["Mes"] = DrMeses.SelectedIndex.ToString();
                    Miyear = "20" + this.Session["Ano"].ToString();
                    for (int i = 0; i < DrAno.Items.Count; i++)
                    {
                        if (DrAno.Items[i].Text == Miyear)
                        {
                            DrAno.SelectedValue = DrAno.Items[i].Value;
                            break;
                        }
                    }
                }
                else
                {
                    DrMeses.SelectedIndex = DrMeses.SelectedIndex + 1;
                    this.Session["Mes"] = DrMeses.SelectedIndex.ToString();
                }

                DateTime DelMes = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 1);
                DateTime MiMes = new DateTime(date.Year, date.Month, 1);

                if (DelMes == MiMes)
                {
                    ImgFechamas.Visible = false;
                }

                //DrMeses.SelectedIndex = Convert.ToInt32(this.Session["SelectMes"].ToString()) + 1;
                this.Session["SelectMes"] = (Convert.ToInt32(DrMeses.SelectedIndex).ToString()).ToString();


                DateTime oPrimerDiaDelMes = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 1);
                TxtDesde.Text = oPrimerDiaDelMes.ToString().Substring(0, 10);
                DateTime dLastDayOfLastMonth = DateTime.Now;
                DateTime dtUltimodia = dLastDayOfLastMonth.AddDays(-1);


                if (Convert.ToInt32(this.Session["Mes"].ToString()) >= 11)
                {
                    dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 31);
                    dtUltimodia = dLastDayOfLastMonth.AddDays(0);
                }
                else
                {
                    dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 2, 1);
                    dtUltimodia = dLastDayOfLastMonth.AddDays(-1);
                }
                TxtHasta.Text = dtUltimodia.ToString().Substring(0, 10);
            }
        }
            
        protected void BtCuestionGralConsulta_Click(object sender, EventArgs e)
        {
            if (DrFlujo.SelectedValue == null || DrVistaEmpleado.SelectedValue == null || DrFlujo.SelectedValue == "")
            {
                lbCuestion.Text = "Debe seleccionar un Archivo Documental con Flujo asociado para poder solicitar consultas a GoldenSoft y RecoDAT.";
                DivCuestion.Visible = true;
                return;
            }
            else
            {
                Lbmensaje.Text = "Con esta nueva consulta volverá a agregar los registros rechazados además de los nuevos que se hayan creado ";
                Lbmensaje.Text += "y que cumplan las condiciones de fechas seleccionadas. Si pulsa ACEPTAR el sistema quedará actualizado una ";
                Lbmensaje.Text += "vez desaparezca este mensaje. ¿Desea continuar? ";
                cuestion.Visible = true;
                Asume.Visible = false;
                DvPreparado.Visible = true;
            }
            //DivProgress.Visible = true;

        }

        protected void Aceptar_Click(object sender, EventArgs e)
        {
            DivCuestion.Visible = false;
        }

        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            //Trae todos los registros de Golden pero compara si ya existen
            BtLimpiaTodo_Click(null, null);

            int Donde = 0;
            string Volumen = "";

#pragma warning disable CS0219 // La variable 'CodigosInsertados' está asignada pero su valor nunca se usa
            string CodigosInsertados = "";
#pragma warning restore CS0219 // La variable 'CodigosInsertados' está asignada pero su valor nunca se usa

            Variables.Error = "";
            //Consultas
            string Fechas = "";
            Mira_CondicionesGral();
            Donde = 1;
            this.Session["FiltroCondicion"] = "";


            //elimino  el contenido de las Tablas
            //string SQL = " DELETE FROM ZEMPLEADOS ";
            //DBHelper.ExecuteNonQuery(SQL);

            if (TxtDesde.Text == "" || TxtHasta.Text == "")
            {
                TextAlerta.Text = "Las fechas puestas en rango no son correctas.";
                alerta.Visible = true;
                return;
            }
            Fechas = "'" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "'";

            string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA, ZQUERY, ZID_PROCEDIMIENTO, ZCAMPODOC, ";
            SQL += " ZCAMPOFILTRO, ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS FROM  ZPROFILES ";
            SQL += " WHERE ZID_FLUJO = " + DrFlujo.SelectedValue + " ";
            SQL += " AND ZID_ARCHIVO = " + DrVistaEmpleado.SelectedValue + " ";
            SQL += " AND ZID_PAGINA = " + this.Session["Pagina"] + " ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];

            if (dt1.Rows.Count == 0)
            {
                SQL = " INSERT INTO ZPROFILES(ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA, ZID_FLUJO, ZID_ARCHIVO, ZID_PAGINA) ";
                SQL += " VALUES ('" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "'," + DrFlujo.SelectedValue + "," + DrVistaEmpleado.SelectedValue + "," + this.Session["Pagina"] + ") ";
            }
            else
            {
                SQL = "  UPDATE ZPROFILES SET ZFECHAINIRECO = '" + TxtDesde.Text + "', ";
                SQL += " ZFECHAFINRECO = '" + TxtHasta.Text + "', ";
                SQL += " ZFECHA = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "', ";
                SQL += " ZID_PAGINA = " + this.Session["Pagina"] + " ";
                SQL += " WHERE ZID_FLUJO = " + DrFlujo.SelectedValue + " ";
                SQL += " AND ZID_ARCHIVO = " + DrVistaEmpleado.SelectedValue + " ";
            }

            DBHelper.ExecuteNonQuery(SQL);

            UltimaConsulta(dt1);

            Donde = 2;

            //Relacion Archivo-campos----------------------------------------------------------------------------------------------
            SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
            DataTable dtCampos = Main.CargaCampos().Tables[0];
            
            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
            //---------------------------------------------------------------------------------------------------------------------;

            //Relacion profiles-procedimientos----------------------------------------------------------------------------------------------
            SQL = "SELECT ZCOMPARA ";
            SQL += " FROM ZPROCEDIMIENTOS WHERE ZID = " + this.Session["Idprocedimiento"].ToString() + " ";

            Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            if (Con is null)
            {
            }
            else
            {
                this.Session["Idprocedimiento"] = Con.ToString();
            }
            //---------------------------------------------------------------------------------------------------------------------;
            try
            {
                string a = "";
                //Busca la base de datos Variable CENTRO es Nombre BASE Datos Empresa en Golden

                string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
                string rBBDDa = ""; // "NET_PRVR" + rANUAL;
                string rBBDDb = ""; // "NET_PRVV" + rANUAL;
                string RutaModelosPDF = "";

                string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA, ZGOLDEN, ZRUTA FROM ANO_AGRICOLA WHERE ACTIVO = 1";
                DataTable dtV = Main.BuscaLote(Data).Tables[0];
                Donde = 9;
                Lberror.Text = Variables.Error + Environment.NewLine;
                string RutaServer = ""; //"\\\\192.168.1.80\\Golden\\Golden .NET\\Datos\\Trabajadores\\20";
                string Ano = "20";
                foreach (DataRow fila3 in dtV.Rows)
                {
                    rANUAL = fila3["ZANO"].ToString();
                    Ano += rANUAL;
                    RutaServer = this.Session["RutaServer"].ToString();
                    RutaModelosPDF = this.Session["RutaModelosPDF"].ToString();
                    //RutaServer = fila3["ZGOLDEN"].ToString();// + rANUAL + "\\"; //+ rANUAL
                    //"\\\\192.168.1.80\\Golden .NET\\Datos\\Modelos PDF\\20" +
                    //RutaModelosPDF = fila3["ZRUTA"].ToString();// + rANUAL + "\\"; //+ rANUAL
                    rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
                    rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
                    break;
                }
                



                Donde = 3;
                //SQL += " SELECT DISTINCT(SUBSTRING(COD_EMPLEADO, 4, LEN(COD_EMPLEADO))) AS TRUNC_EMPLEADO, COD_EMPLEADO, (SUBSTRING(COD_EMPLEADO, 1, 3)) AS CENTRO  ";
                //SQL += " FROM ZEMPLEADOS ";
                this.Session["Query1"] = this.Session["Query1"].ToString().Replace("RBBDDb#", rBBDDb);
                this.Session["Query1"] = this.Session["Query1"].ToString().Replace("RBBDDa#", rBBDDa);
                this.Session["Query1"] = this.Session["Query1"].ToString().Replace("DESDE#", TxtDesde.Text);
                this.Session["Query1"] = this.Session["Query1"].ToString().Replace("HASTA#", TxtHasta.Text);

                dt1 = Main.BuscaLoteGold(this.Session["Query1"].ToString()).Tables[0];

                //Desarrollo-----------------------------------------------------------

                //SQL = " SELECT CodigoTrabajador ";
                //SQL += " , Nombre ";
                //SQL += ", Apellido1 ";
                //SQL += ", Apellido2 ";
                //SQL += ", NOMBRECOMPLETO ";
                //SQL += ", Abreviatura ";
                //SQL += ", NIF ";
                //SQL += ", Foto ";
                //SQL += ", Email ";
                //SQL += ", FechaInicio ";
                //SQL += ", FechaFin ";
                //SQL += ", CodigoContrato ";
                //SQL += ", EsIndefinido ";
                //SQL += ", EsTiempoParcial ";
                //SQL += ", ModeloContrato ";
                //SQL += ", DatosPDF ";
                //SQL += ", DocumentosAdjuntos ";
                //SQL += ", ZEMPRESA ";
                //SQL += ", ZFECHA ";
                //SQL += " FROM ZCONSULTA_GOLDEN ";
                //SQL += "WHERE FechaInicio BETWEEN '20220701' AND '20220801' ";
                //SQL += "ORDER BY CodigoTrabajador DESC ";

                //dt1 = Main.BuscaLote(SQL).Tables[0];
                //---------------------------------------------------------------------



                //dt1 = Main.BuscaLoteGold(SQL).Tables[0];
                string Compara = "";
                string MiEmpleado = "";
                Boolean Esta = false;
                //a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 1  Entrada consulta = " + dt1.Rows.Count);

                //Recoge las columnas añadidas a la tabla para filtrar
                Donde = 4;

                String[] ColumnasFiltro = System.Text.RegularExpressions.Regex.Split(this.Session["FiltroCondicion"].ToString(), ";");
                a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 1  ColumnasFiltro = " + ColumnasFiltro);
                string sourcePath = RutaServer; // "\\\\192.168.1.80\\Golden\\Golden .NET\\Datos\\Trabajadores\\" + DateTime.Now.Year + "\\";
                string idestado = "";
                //Consulta de Golden comprueba CodigoTrabajador y ahora ademas fecha
                foreach (DataRow fila in dt1.Rows)
                {
                    //if (Compara != fila["CodigoTrabajador"].ToString())
                    //SI TIENE FILTRO EN TABLAS
                    if (ColumnasFiltro.Count() > 0)
                    {
                        //Si no existe en la Tabla lo inserto. Cuando veamos que llegan los siguientes hay que añadir mas filtros en WHERE
                        SQL = " SELECT " + this.Session["Idprocedimiento"].ToString() + " FROM " + this.Session["TablaName"].ToString();// + " WHERE ";// + this.Session["Idprocedimiento"].ToString() + " = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                        Donde = 5;
                        if (ColumnasFiltro.Count() > 0)
                        {
                            for (int i = 0; i < ColumnasFiltro.Count() - 1; i++)
                            {
                                if (ColumnasFiltro[i] != "")
                                {
                                    if (i == 0)
                                    {
                                        SQL += " WHERE " + ColumnasFiltro[i] + " = '" + fila[ColumnasFiltro[i]].ToString() + "' ";
                                    }
                                    else
                                    {
                                        SQL += " AND " + ColumnasFiltro[i] + " = '" + fila[ColumnasFiltro[i]].ToString() + "' ";
                                    }
                                }
                            }
                            if (SQL.Contains("WHERE"))
                            {
                                SQL += " AND " + this.Session["Idprocedimiento"].ToString() + " = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                            }
                            else
                            {
                                SQL += " WHERE " + this.Session["Idprocedimiento"].ToString() + " = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                            }
                        }
                        else
                        {
                            SQL += " WHERE " + this.Session["Idprocedimiento"].ToString() + " = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                        }
                        Donde = 6;

                        Con = DBHelper.ExecuteScalarSQL(SQL, null);
                        MiEmpleado = SQL;
                        if (Con == null)
                        {
                            Esta = false;
                        }
                        else
                        {
                            Esta = true;
                        }
                        a = Main.ETrazas("", "1", " btGralCnsulta --> Donde " + Donde + " ,Esta = " + Esta + "  ColumnasFiltro = " + SQL);
                    }

                    //Si la consulta previa a dado como resultado null

                    if (Compara != fila[this.Session["Idprocedimiento"].ToString()].ToString() && Esta == false)
                    {
                        //Llamar a VOLUMENES para traer el path
                        SQL = " SELECT ZRUTA FROM ZVOLUMENES WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                        Con = DBHelper.ExecuteScalarSQL(SQL, null);
                        if (Con is null)
                        {
                        }
                        else
                        {
                            //foreach (DataRow filas in dtCampos.Rows)
                            //{
                            //    string BuscaCampo = filas["ZTITULO"].ToString() + "#";

                            //    if (Con.ToString().Contains(BuscaCampo) == true)
                            //    {
                            //        Con = Con.ToString().Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString() );
                            //    }
                            //    if (sourcePath.ToString().Contains(BuscaCampo) == true)
                            //    {
                            //        sourcePath = sourcePath.ToString().Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                            //    }
                                //if (RutaServer.ToString().Contains(BuscaCampo) == true)
                                //{
                                //    RutaServer = RutaServer.ToString().Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                //}
                                //if (RutaModelosPDF.ToString().Contains(BuscaCampo) == true)
                                //{
                                //    RutaModelosPDF = RutaModelosPDF.ToString().Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                //}
                            //}
                            Volumen = Con.ToString();
                        }

                        //a = Main.ETrazas(SQL, "1", " btGralCnsulta --> Con codigo trabajador" + fila["CodigoTrabajador"].ToString());
                        //Busca el primer Estado 
                        SQL = " SELECT ZID_ESTADO FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                        SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZORDEN = 1 ";
                        Object Coni = DBHelper.ExecuteScalarSQL(SQL, null);
                        idestado = Coni.ToString();
                        if (Coni == null)
                        {
                            idestado = "0";
                        }
                        else
                        {
                            idestado = Coni.ToString();
                            SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                            SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"].ToString();
                            Object Eje = DBHelper.ExecuteScalarSQL(SQL, null);
                            if (Eje.ToString() == "1")
                            {
                                if(gvEmpleado.Rows.Count > 0)
                                {
                                    if (this.Session["MiNivel"].ToString() == "0")
                                    {
                                        //ImgInicia.Visible = false;
                                        LbAutomatico.Visible = false;

                                    }
                                    else
                                    {
                                        //ImgInicia.Visible = true;
                                        LbAutomatico.Visible = true;
                                    }
                                }
                                else if (Eje.ToString() == "2")
                                {
                                    //ImgInicia.Visible = false;
                                    LbAutomatico.Visible = false;
                                }
                                else
                                {
                                    //ImgInicia.Visible = false;
                                    LbAutomatico.Visible = false;
                                }
                            }
                        }

                        //Si no existe en la Tabla lo inserto. Cuando veamos que llegan los siguientes hay que añadir mas filtros en WHERE
                        //SQL = " SELECT " + this.Session["Idprocedimiento"].ToString() + " FROM " + this.Session["TablaName"].ToString() + " WHERE " + this.Session["Idprocedimiento"].ToString() + " = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                        //SQL = " SELECT CodigoTrabajador FROM " + this.Session["TablaName"].ToString() + " WHERE CodigoTrabajador = '" + fila["CodigoTrabajador"].ToString() + "'";
                        //Nuevamente la consulta por si lo han ejecutado desde otro lado
                        Con = DBHelper.ExecuteScalarSQL(MiEmpleado, null);
                        Donde = 5;
                        if (Con == null)
                        {
                            //consulta recodat y tarifas --> mirar como parametrizar automaticamente
                            SQL = " SELECT TOP 1 (codCategoria) AS CATEGORIA FROM EMPLEADOS  ";
                            SQL += " WHERE SUBSTRING(CodEmpleado,4,LEN(CodEmpleado)) = '" + fila[this.Session["Idprocedimiento"].ToString()].ToString() + "'";
                            //Desarrollo
                            //Object Categoria = "S2";// DBHelper.ExecuteScalarSQLReco(SQL, null);
                            Object Categoria = DBHelper.ExecuteScalarSQLReco(SQL, null);
                            Donde = 6;
                            Object Prevencion = null;
                            if (Categoria == null)
                            {}
                            else
                            {
                                SQL = " SELECT TOP 1 (PREVENCION) FROM REC_TARIFAS  ";
                                SQL += " WHERE SUBTIPO = '" + Categoria + "'";
                                Prevencion = DBHelper.ExecuteScalarSQL(SQL, null);
                                Donde = 7;
                            }

                            SQL = " INSERT INTO " + this.Session["TablaName"].ToString() + "(";
                            string SQLValues = " VALUES ( ";
                            //Consulta Columnas campo
                            foreach (DataRow filas in dtCampos.Rows)
                            {
                                if (filas["ZTITULO"].ToString() == "ZRUTA")
                                {
                                    SQL += filas["ZTITULO"].ToString() + ",";
                                    SQLValues += "'" + RutaServer + "',";
                                }
                                else if (filas["ZTITULO"].ToString() == "ZCATEGORIA")
                                {
                                    SQL += filas["ZTITULO"].ToString() + ",";
                                    SQLValues += "'" + Categoria + "',";
                                }
                                else if (filas["ZTITULO"].ToString() == "ZPREVENCION")
                                {
                                    SQL += filas["ZTITULO"].ToString() + ",";
                                    SQLValues += "'" + Prevencion + "',";
                                }
                                else
                                {
                                    if(filas["ZTITULO"].ToString() != "ZID")
                                    {
                                        SQL += filas["ZTITULO"].ToString() + ",";
                                        SQLValues += "'" + fila[filas["ZTITULO"].ToString()].ToString() + "',";
                                    }
                                }
                            }

                            Donde = 8;
                            SQL = SQL.Substring(0, SQL.Length - 1) + ")";
                            SQLValues = SQLValues.Substring(0, SQLValues.Length - 1) + ")";


                            //Busca campos en consulta

                            foreach (DataRow filas in dtCampos.Rows)
                            {
                                string BuscaCampo = filas["ZTITULO"].ToString() + "#";

                                if (SQL.Contains(BuscaCampo) == true)
                                {
                                    SQL.Replace(BuscaCampo , fila[filas["ZTITULO"].ToString()].ToString());
                                }
                            }

                            SQL = SQL + SQLValues;
                            a = Main.ETrazas(SQL, "1", " btGralCnsulta -->  Donde = " + Donde + " GralConsulta Registro SQL: " + SQL);

                            DBHelper.ExecuteNonQuery(SQL);

                            SQL = " SELECT MAX(ZID) FROM " + this.Session["TablaName"].ToString();

                            Object Identificador = DBHelper.ExecuteScalarSQL(SQL, null);

                            if(Identificador == null)
                            {
                                Identificador = 0;
                            }
                            Donde = 9;

                            //Si tiene Golden Documentos adjunto

                            if (fila[this.Session["DocumentacionAdjunta"].ToString()].ToString() != "")
                            {
                                string[] Fields = System.Text.RegularExpressions.Regex.Split(fila[this.Session["DocumentacionAdjunta"].ToString()].ToString(), ";");
                                string MiFichero = Volumen;
                                string RutaOrigen = sourcePath ;
                                Donde = 10;
                                //Busca campos en Rutas
                                foreach (DataRow filas in dtCampos.Rows)
                                {
                                    string BuscaCampo = filas["ZTITULO"].ToString() + "#";

                                    if (Fields.Count() > 0)
                                    {
                                        for (int i = 0; i < Fields.Count() - 1; i++)
                                        {
                                            if (Fields[i].Contains(BuscaCampo) == true)
                                            {
                                                Fields[i] = Fields[i].Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                            }
                                        }
                                    }

                                    if (MiFichero.Contains(BuscaCampo) == true)
                                    {
                                        MiFichero = MiFichero.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                    }
                                    if (RutaOrigen.Contains(BuscaCampo) == true)
                                    {
                                        RutaOrigen = RutaOrigen.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                    }
                                }

                                //string MiFichero = Volumen + fila["NIF"].ToString() + "/" + fila["FechaInicio"].ToString();// sourcePath + fila["NIF"].ToString() ;
                                //string RutaOrigen = sourcePath + fila["NIF"].ToString();

                                if (Fields.Count() > 0)
                                {                                    
                                    for (int i = 0; i < Fields.Count() - 1; i++)
                                    {
                                        // Copy the files and overwrite destination files if they already exist. Docs\folders
                                        //a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 2  Documentacion adjunta  " + Fields[i]);
                                        Donde = 11;
                                        if (Fields[i].ToLower().Contains("copia") == true)
                                        {
                                            a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 2  Documentacion adjunta: No entra por contener en el nombre copia " + Fields[i].ToLower());
                                        }
                                        else
                                        {
                                            foreach (string s in Fields)
                                            {
                                                // Use static Path methods to extract only the file name from the path. oFileInfo.Length.ToString().Replace(",", ".")
                                                string fileName = MiFichero + "\\" + System.IO.Path.GetFileName(s);
                                                string fileNameOrigen = RutaOrigen + "\\" + System.IO.Path.GetFileName(s);

                                                string DsfileName = System.IO.Path.GetFileName(s);
                                                string NameFile = "";
                                                SQL = " SELECT MAX(ZID) FROM " + this.Session["TablaName"].ToString() ;// + TablaDocumentos;

                                                Con = DBHelper.ExecuteScalarSQL(SQL, null);
                                                if (Con is null || Con.ToString() == "")
                                                {
                                                    Con = "1";
                                                    NameFile = Con.ToString().PadLeft(10, '0');
                                                }
                                                else
                                                {
                                                    NameFile = Con.ToString().PadLeft(10, '0');
                                                }

                                                FileInfo oFileInfo = null;
                                                string destFile = "";
                                                string Peso ="";

                                                try
                                                {
                                                    oFileInfo = new FileInfo(fileNameOrigen);
                                                    destFile = System.IO.Path.Combine(sourcePath, DsfileName) + oFileInfo.Extension;
                                                    Peso = oFileInfo.Length.ToString();

                                                    if (Peso.Length >= 12)
                                                    {
                                                        Peso = (Convert.ToDecimal(string.Format("{0:0.00}", ((oFileInfo.Length / 1024f) / 1024f) / 1024f))).ToString() + " GB";
                                                    }
                                                    else if (Peso.Length >= 7 && Peso.Length <= 12)
                                                    {
                                                        Peso = (Convert.ToDecimal(string.Format("{0:0.00}", (oFileInfo.Length / 1024f) / 1024f))).ToString() + " MB";
                                                    }
                                                    else
                                                    {
                                                        Peso = ((Convert.ToDecimal(string.Format("{0:0.00}", oFileInfo.Length / 1024f)))).ToString() + " KB";
                                                    }

                                                    if (s.ToLower().Contains("copia") == true)
                                                    {
                                                        //a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 2  Documentacion adjunta: No entra por contener en el nombre copia " + s.ToString());
                                                    }
                                                    else
                                                    {
                                                        //+ TablaDocumentos 
                                                        //Mira si existe
                                                        //SQL = "SELECT ZID_REGISTRO FROM ZEMPLEADOSOBJ WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " AND  ZID_REGISTRO = " + Con + " ";
                                                        //Object Miro = DBHelper.ExecuteScalarSQL(SQL, null);
                                                        //if (Miro is null)
                                                        //{
                                                        SQL = "INSERT INTO " + this.Session["TablaObj"].ToString() + " (ZID_DOMAIN, ZID_ARCHIVO, ZID_REGISTRO, ZDESCRIPCION, ZPESO, ZROOT, ZKEY, ZFECHA, ZESTADO, ZDIRECTORIO, ZNIVEL )  ";
                                                        SQL += "VALUES (0," + this.Session["idarchivo"].ToString() + ", " + Con + ", '" + DsfileName + "','" + Peso + "', '2', '0', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', 0, '" + fileName + "', 0 )  ";
                                                        DBHelper.ExecuteNonQuery(SQL);
                                                        a = Main.ETrazas(SQL, "1", " btGralCnsulta -->  Donde = " + Donde + " GralConsulta Objetos SQL: " + SQL);

                                                        //}
                                                    }
                                                    DirectoryInfo di = Directory.CreateDirectory(MiFichero);
                                                }
                                                catch (Exception ex)
                                                {
                                                    a = Main.ETrazas(SQL, "1", " Error propiedades fichero --> No Existe. = Error: " + ex.Message);
                                                    continue;
                                                }
                                                //a = Main.ETrazas(SQL, "1", " Crea Directorio si no existe --> " + MiFichero);

                                                try
                                                {
                                                    if (System.IO.File.Exists(fileName) == true)
                                                    {
                                                        System.IO.File.Delete(fileName);
                                                    }
                                                    if (s.ToLower().Contains("copia") == true)
                                                    {
                                                    }
                                                    else
                                                    {
                                                        System.IO.File.Copy(fileNameOrigen, fileName);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    a = Main.ETrazas(SQL, "1", " Error --> " + ex.Message);
                                                    a = Main.ETrazas(SQL, "1", " Volumen --> " + Volumen);
                                                    a = Main.ETrazas("", "1", " Ruta con Volumen --> " + MiFichero);
                                                    a = Main.ETrazas("", "1", " Ruta con Origen --> " + RutaOrigen);
                                                    a = Main.ETrazas("", "1", " Directorio Salida si no existe --> " + MiFichero);
                                                    a = Main.ETrazas("", "1", " Fichero en Volumen --> " + fileName);
                                                    a = Main.ETrazas("", "1", " Fichero en Origen --> " + fileNameOrigen);
                                                    continue;
                                                }
                                                //System.IO.File.Move(fileName, System.IO.Path.Combine(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString() + "/"), NameFile)); // destFile);
                                            }
                                        }
                                    }
                                }
                            }

                            //Ahora busca en directorio por DNI
                            Donde = 12;
                            SQL = " SELECT ZRUTA FROM ZVOLUMENES WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";

                            Con = DBHelper.ExecuteScalarSQL(SQL, null);
                            if (Con is null)
                            {
                            }
                            else
                            {
                                Volumen = Con.ToString();
                            }
                            if (@RutaModelosPDF != "")
                            {

                                string[] Misfiles = Directory.GetFiles(@RutaModelosPDF, "*.pdf");

                                Donde = 9;

                                foreach (var file in Misfiles)
                                {
                                    Donde = 13;
                                    //if (file.Contains(fila["NIF"].ToString()))
                                    if (file.Contains(fila[this.Session["CampoFiltro"].ToString()].ToString()))
                                    {
                                        string[] Fields = System.Text.RegularExpressions.Regex.Split(file, ";");
                                        string MiFichero = Volumen; // + fila["NIF"].ToString() + "/" + fila["FechaInicio"].ToString();// sourcePath + fila["NIF"].ToString() ;
                                        string RutaOrigen = RutaModelosPDF;// + fila["NIF"].ToString();

                                        //Busca campos
                                        foreach (DataRow filas in dtCampos.Rows)
                                        {
                                            string BuscaCampo = filas["ZTITULO"].ToString() + "#";

                                            if (Fields.Count() > 0)
                                            {
                                                for (int i = 0; i < Fields.Count() - 1; i++)
                                                {
                                                    if (Fields[i].Contains(BuscaCampo) == true)
                                                    {
                                                        Fields[i] = Fields[i].Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                                    }
                                                }
                                            }

                                            if (MiFichero.Contains(BuscaCampo) == true)
                                            {
                                                MiFichero = MiFichero.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                            }
                                            if (RutaOrigen.Contains(BuscaCampo) == true)
                                            {
                                                RutaOrigen = RutaOrigen.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                                            }
                                        }

                                        if (Fields.Count() > 0)
                                        {
                                            for (int i = 0; i < Fields.Count(); i++)
                                            {
                                                // Copy the files and overwrite destination files if they already exist. Docs\folders
                                                Donde = 14;
                                                foreach (string s in Fields)
                                                {
                                                    // Use static Path methods to extract only the file name from the path. oFileInfo.Length.ToString().Replace(",", ".")
                                                    string fileName = MiFichero + "\\" + System.IO.Path.GetFileName(s);
                                                    string fileNameOrigen = RutaOrigen + System.IO.Path.GetFileName(s);

                                                    string DsfileName = System.IO.Path.GetFileName(s);

                                                    FileInfo oFileInfo = null;
                                                    string destFile = "";
                                                    string Peso = "";

                                                    try
                                                    {
                                                        oFileInfo = new FileInfo(fileNameOrigen);//fileName
                                                        destFile = System.IO.Path.Combine(RutaOrigen, DsfileName) + oFileInfo.Extension;
                                                        Peso = oFileInfo.Length.ToString();
                                                        if (Peso.Length >= 12)
                                                        {
                                                            Peso = (Convert.ToDecimal(string.Format("{0:0.00}", ((oFileInfo.Length / 1024f) / 1024f) / 1024f))).ToString() + " GB";
                                                        }
                                                        else if (Peso.Length >= 7 && Peso.Length <= 12)
                                                        {
                                                            Peso = (Convert.ToDecimal(string.Format("{0:0.00}", (oFileInfo.Length / 1024f) / 1024f))).ToString() + " MB";
                                                        }
                                                        else
                                                        {
                                                            Peso = ((Convert.ToDecimal(string.Format("{0:0.00}", oFileInfo.Length / 1024f)))).ToString() + " KB";
                                                        }
                                                        if (s.ToLower().Contains("copia") == true)
                                                        {
                                                            a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 2  Documentacion adjunta: No entra por contener en el nombre copia " + s.ToString());
                                                        }
                                                        else
                                                        {
                                                            //+ TablaDocumentos 
                                                            SQL = "INSERT INTO " + this.Session["TablaObj"].ToString() + " (ZID_DOMAIN, ZID_ARCHIVO, ZID_REGISTRO, ZDESCRIPCION, ZPESO, ZROOT, ZKEY, ZFECHA, ZESTADO, ZDIRECTORIO, ZNIVEL )  ";
                                                            SQL += "VALUES (0, " + this.Session["idarchivo"].ToString() + ", " + Identificador.ToString() + ", '" + DsfileName + "','" + Peso + "', '3', '0', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', 0, '" + fileName + "',0 )  ";
                                                            DBHelper.ExecuteNonQuery(SQL);
                                                            a = Main.ETrazas(SQL, "1", " btGralCnsulta -->  Donde = " + Donde + " GralConsulta MoDeloPDF SQL: " + SQL);

                                                        }

                                                        DirectoryInfo di = Directory.CreateDirectory(MiFichero);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        a = Main.ETrazas(SQL, "1", " Error Propiedades Fichero. No Existe: --> " + ex.Message);
                                                        continue;
                                                    }


                                                    try
                                                    {
                                                        if (File.Exists(fileName) == true)
                                                        {
                                                            File.Delete(fileName);
                                                        }
                                                        if (s.ToLower().Contains("copia") == true)
                                                        {
                                                        }
                                                        else
                                                        {
                                                            System.IO.File.Copy(fileNameOrigen, fileName);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        a = Main.ETrazas(SQL, "1", " Error --> " + ex.Message);
                                                        a = Main.ETrazas(SQL, "1", " Volumen --> " + Volumen);
                                                        a = Main.ETrazas("", "1", " Ruta con Volumen --> " + MiFichero);
                                                        a = Main.ETrazas("", "1", " Ruta con Origen --> " + RutaOrigen);
                                                        a = Main.ETrazas("", "1", " Directorio Salida si no existe --> " + MiFichero);
                                                        a = Main.ETrazas("", "1", " Fichero en Volumen --> " + fileName);
                                                        a = Main.ETrazas("", "1", " Fichero en Origen --> " + fileNameOrigen);
                                                        continue;
                                                    }
                                                    //System.IO.File.Move(fileName, System.IO.Path.Combine(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString() + "/"), NameFile)); // destFile);
                                                }
                                            }
                                        }
                                        //break;
                                    }
                                }
                            }
                        }
                    }
                    Compara = fila["CodigoTrabajador"].ToString();
                }
                Donde = 6;
                //---------------------------------------------------------------------------------------------------------------------;
                //Termina seccion con la maquina 80

                ////Llamar a VOLUMENES para traer el path
                //SQL = " SELECT ZRUTA FROM ZVOLUMENES WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";

                //Con = DBHelper.ExecuteScalarSQL(SQL, null);
                //if (Con is null)
                //{
                //}
                //else
                //{

                //    Volumen = Con.ToString();
                //}

                //string[] files = Directory.GetFiles(@RutaModelosPDF, "*.pdf");

                //SQL = " SELECT * FROM  " + this.Session["TablaName"].ToString() + " WHERE ZID IN(" + CodigosInsertados + ") ORDER BY ZID";

                //a = Main.ETrazas(SQL, "1", " btGralCnsulta --> Consulta directorios con ZID insertados --> ");

                //dt1 = Main.BuscaLote(SQL).Tables[0];
                //Donde = 9;

                ////Ahora busca en directorio por DNI
                //foreach (DataRow fila in dt1.Rows)
                //{
                //    string IdArchivo = fila["ZID"].ToString(); // Con.ToString().PadLeft(10, '0');

                //    foreach (var file in files)
                //    {
                //        //if (file.Contains(fila["NIF"].ToString()))
                //        if (file.Contains(fila[this.Session["CampoFiltro"].ToString()].ToString()))
                //        {
                //            string[] Fields = System.Text.RegularExpressions.Regex.Split(file, ";");
                //            string MiFichero = Volumen; // + fila["NIF"].ToString() + "/" + fila["FechaInicio"].ToString();// sourcePath + fila["NIF"].ToString() ;
                //            string RutaOrigen = RutaModelosPDF;// + fila["NIF"].ToString();

                //            //Busca campos
                //            foreach (DataRow filas in dtCampos.Rows)
                //            {
                //                string BuscaCampo = filas["ZTITULO"].ToString() + "#";

                //                if (Fields.Count() > 0)
                //                {
                //                    for (int i = 0; i < Fields.Count() - 1; i++)
                //                    {
                //                        if (Fields[i].Contains(BuscaCampo) == true)
                //                        {
                //                            Fields[i] = Fields[i].Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                //                        }
                //                    }
                //                }

                //                if (MiFichero.Contains(BuscaCampo) == true)
                //                {
                //                    MiFichero = MiFichero.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                //                }
                //                if (RutaOrigen.Contains(BuscaCampo) == true)
                //                {
                //                    RutaOrigen = RutaOrigen.Replace(BuscaCampo, fila[filas["ZTITULO"].ToString()].ToString());
                //                }
                //            }

                //            if (Fields.Count() > 0)
                //            {
                //                for (int i = 0; i < Fields.Count(); i++)
                //                {
                //                    // Copy the files and overwrite destination files if they already exist. Docs\folders
                //                    foreach (string s in Fields)
                //                    {
                //                        // Use static Path methods to extract only the file name from the path. oFileInfo.Length.ToString().Replace(",", ".")
                //                        string fileName = MiFichero + "\\" + System.IO.Path.GetFileName(s);
                //                        string fileNameOrigen = RutaOrigen +  System.IO.Path.GetFileName(s);



                //                        string DsfileName = System.IO.Path.GetFileName(s);

                //                        FileInfo oFileInfo = null;
                //                        string destFile = "";
                //                        string Peso = "";

                //                        try
                //                        {
                //                            oFileInfo = new FileInfo(fileNameOrigen);//fileName
                //                            destFile = System.IO.Path.Combine(RutaOrigen, DsfileName) + oFileInfo.Extension;
                //                            Peso = oFileInfo.Length.ToString();
                //                            if (Peso.Length >= 12)
                //                            {
                //                                Peso = (Convert.ToDecimal(string.Format("{0:0.00}", ((oFileInfo.Length / 1024f) / 1024f) / 1024f))).ToString() + " GB";
                //                            }
                //                            else if (Peso.Length >= 7 && Peso.Length <= 12)
                //                            {
                //                                Peso = (Convert.ToDecimal(string.Format("{0:0.00}", (oFileInfo.Length / 1024f) / 1024f))).ToString() + " MB";
                //                            }
                //                            else
                //                            {
                //                                Peso = ((Convert.ToDecimal(string.Format("{0:0.00}", oFileInfo.Length / 1024f)))).ToString() + " KB";
                //                            }
                //                            if (s.ToLower().Contains("copia") == true)
                //                            {
                //                                a = Main.ETrazas(SQL, "1", " btGralCnsulta --> 2  Documentacion adjunta: No entra por contener en el nombre copia " + s.ToString());
                //                            }
                //                            else
                //                            {
                //                                //+ TablaDocumentos 
                //                                SQL = "INSERT INTO " + this.Session["TablaObj"].ToString() + " (ZID_DOMAIN, ZID_ARCHIVO, ZID_REGISTRO, ZDESCRIPCION, ZPESO, ZROOT, ZKEY, ZFECHA, ZESTADO, ZDIRECTORIO, ZNIVEL )  ";
                //                                SQL += "VALUES (0, " + this.Session["idarchivo"].ToString() + ", " + IdArchivo + ", '" + DsfileName + "','" + Peso + "', '3', '0', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', 0, '" + fileName + "',0 )  ";
                //                                DBHelper.ExecuteNonQuery(SQL);
                //                                a = Main.ETrazas(SQL, "1", " btGralCnsulta -->  Donde = " + Donde + " GralConsulta MoDeloPDF SQL: " + SQL);

                //                            }

                //                            DirectoryInfo di = Directory.CreateDirectory(MiFichero);
                //                        }
                //                        catch (Exception ex)
                //                        {
                //                            a = Main.ETrazas(SQL, "1", " Error Propiedades Fichero. No Existe: --> " + ex.Message);
                //                            continue;
                //                        }


                //                        try
                //                        {
                //                            if (File.Exists(fileName) == true)
                //                            {
                //                                File.Delete(fileName);
                //                            }
                //                            if (s.ToLower().Contains("copia") == true)
                //                            {
                //                            }
                //                            else
                //                            {
                //                                System.IO.File.Copy(fileNameOrigen, fileName);
                //                            }
                //                        }
                //                        catch (Exception ex)
                //                        {
                //                            a = Main.ETrazas(SQL, "1", " Error --> " + ex.Message);
                //                            a = Main.ETrazas(SQL, "1", " Volumen --> " + Volumen);
                //                            a = Main.ETrazas("", "1", " Ruta con Volumen --> " + MiFichero);
                //                            a = Main.ETrazas("", "1", " Ruta con Origen --> " + RutaOrigen);
                //                            a = Main.ETrazas("", "1", " Directorio Salida si no existe --> " + MiFichero);
                //                            a = Main.ETrazas("", "1", " Fichero en Volumen --> " + fileName);
                //                            a = Main.ETrazas("", "1", " Fichero en Origen --> " + fileNameOrigen);
                //                            continue;
                //                        }
                //                        //System.IO.File.Move(fileName, System.IO.Path.Combine(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString() + "/"), NameFile)); // destFile);
                //                    }
                //                }
                //            }
                //            //break;
                //        }
                //    }
                //}

                //Ahora pinto el grid
                //gvEmpleado.DataSource = dt1;
                //gvEmpleado.DataBind();
                ////////dtCampos = Main.CargaCampos().Tables[0];
                ////////this.Session["Campos"] = dtCampos;

                ////////int Cuantos = gvEmpleado.Rows.Count;

                ////////SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
                ////////SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
                ////////dtArchivos = Main.BuscaLote(SQL).Tables[0];

                ////////dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos);
                ////////CreaGridControl(dtArchivos, dtCampos);
                ////////Carga_tablaControl(dtArchivos, dtCampos, this.Session["idestado"].ToString(), null, this.Session["idflujo"].ToString());

                ////////if (gvEmpleado.Rows.Count > 0)
                ////////{
                ////////    BtAtras.Visible = true;
                ////////}
                ////////else
                ////////{
                ////////    BtAtras.Visible = false;
                ////////}
                ////////DvPreparado.Visible = false;
                ////////cuestion.Visible = false;
                ////////Asume.Visible = false;
                ////////Lberror.Visible = false;


            }
            catch (Exception ex)
            {
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas(SQL, "1", " btGralCnsulta -->  Donde = " + Donde + " Error:" + ex.Message + " --> " + context.Session["Procedimiento"].ToString());

                Lberror.Visible = true;
                Lberror.Text += Variables.Error + " Donde = " + Donde  + " --> " + SQL + Environment.NewLine;
                DvPreparado.Visible = false;
                cuestion.Visible = false;
                Asume.Visible = false;
                Lberror.Visible = false;
                return;
            }
            //Lberror.Text = "";
        }


        private void GralConsultalocal()
        {
            string SQL = "";
            //Lbmensaje.Text = "Generando calendario para Nóminas...";
            //Label pmtProc1 = (Label)Progress3.FindControl("LbmensajeET");
            //pmtProc1.Text = "Generando calendario para Nóminas...";
            try
            {
                DBHelper.ExecuteProcedureTareas("");

                SQL = " UPDATE A ";
                SQL += " SET A.ZTOTALMINUTOS = C.CUANTO ";
                SQL += " FROM REC_TAREAS A,(SELECT A.COD_EMPLEADO, A.FECHA_EMPLEADOS, SUM(A.ZMINUTOS) AS CUANTO  ";
                SQL += "                     FROM REC_TAREAS A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_EMPLEADOS = C.FECHA_EMPLEADOS ";
                DBHelper.ExecuteNonQuery(SQL);


                //Calculo horas laborales y extras

                SQL = " UPDATE A ";
                SQL = " SET A.NHORAS = C.CUANTO ,  ";
                SQL = "     A.XHORAS = C.EXTRAS ";
                SQL = " FROM REC_TAREAS A,(SELECT A.COD_EMPLEADO, A.FECHA_EMPLEADOS,N.CENTRO,   ";
                SQL = "                         (CASE WHEN A.ZTOTALMINUTOS > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.ZTOTALMINUTOS END) AS CUANTO, ";
                SQL = "                         (CASE WHEN  A.ZTOTALMINUTOS > B.HORASEXTRAS THEN A.ZTOTALMINUTOS - B.HORASEXTRAS ELSE 0 END ) AS EXTRAS ";
                SQL = "                         FROM REC_TAREAS A,  REC_PARAM B, REC_EMPLEADO N ";
                SQL = "                         WHERE A.COD_EMPLEADO = N.COD_EMPLEADO ";
                SQL = "                         AND B.CENTRO = N.CENTRO ) C ";
                SQL = " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL = " AND A.FECHA_EMPLEADOS = C.FECHA_EMPLEADOS ";
                //SQL += "                        AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ) C ";


                //antes
                //SQL = " UPDATE REC_TAREAS ";
                //SQL += " SET NHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN 390 ELSE ZTOTALMINUTOS END), ";
                //SQL += "     XHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN ZTOTALMINUTOS - 390 ELSE 0 END) ";
                DBHelper.ExecuteNonQuery(SQL);

                //Periodo horas laborales y extras
                SQL = " UPDATE REC_TAREAS ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);

                //Importe horas laborales
                SQL = " UPDATE A ";
                SQL += " SET A.TIMPNHORAS = NHORAS * C.CUANTO ";
                SQL += " FROM REC_TAREAS A,(SELECT A.COD_EMPLEADO, A.FECHA_EMPLEADOS, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_TAREAS A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'JORNAL' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_EMPLEADOS = C.FECHA_EMPLEADOS ";

                DBHelper.ExecuteNonQuery(SQL);

                //Importe horas laborales
                SQL = " UPDATE A ";
                SQL += " SET A.TIMPXHORAS = XHORAS * C.CUANTO ";
                SQL += " FROM REC_TAREAS A,(SELECT A.COD_EMPLEADO, A.FECHA_EMPLEADOS, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_TAREAS A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'EXTRA' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_EMPLEADOS = C.FECHA_EMPLEADOS ";

                DBHelper.ExecuteNonQuery(SQL);

                //Actualizo el resto de campos REC_JORNADA 
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET TOTALMINUTOS = (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) / 60 + DATEPART(HOUR, CONVERT(DATETIME, TRANSCURRIDO)) * 60) + (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) % 60) ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE A ";
                SQL += " SET A.TOTALTIEMPO = C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, FECHA_JORNADA, SUM(TOTALMINUTOS) AS CUANTO FROM REC_JORNADA ";
                SQL += "                     GROUP BY COD_EMPLEADO, FECHA_JORNADA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";

                DBHelper.ExecuteNonQuery(SQL);


                //Calculo horas laborales y extras
                //SQL = " UPDATE REC_JORNADA A,  REC_PARAM B";
                //SQL += " SET A.NHORAS = (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.TOTALTIEMPO END), ";
                //SQL += "     A.XHORAS = (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN A.TOTALTIEMPO - 390 ELSE 0 END) ";
                //SQL += " WHERE B.CENTRO <> '' ";
                //SQL += " AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ";


                SQL = " UPDATE A ";
                SQL += "SET A.NHORAS = C.CUANTO , ";
                SQL += "    A.XHORAS = C.EXTRAS ";
                SQL += "FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA,  ";
                SQL += "                        (CASE WHEN A.TOTALTIEMPO > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.TOTALTIEMPO END) AS CUANTO, ";
                SQL += "                        (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN A.TOTALTIEMPO - B.HORASEXTRAS ELSE 0 END ) AS EXTRAS ";
                SQL += "                        FROM REC_JORNADA A,  REC_PARAM B, REC_EMPLEADO N  ";
                SQL = "                         WHERE A.COD_EMPLEADO = N.COD_EMPLEADO ";
                SQL = "                         AND B.CENTRO = N.CENTRO ) C ";
                //SQL += "                        WHERE B.CENTRO <> '' ";
                //SQL += "                        AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ) C ";
                SQL += "WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "AND A.FECHA_JORNADA = C.FECHA_JORNADA ";


                DBHelper.ExecuteNonQuery(SQL);

                //Periodo horas laborales y extras
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);

                //Lberror.Text += Variables.Error + Environment.NewLine;

                SQL = " UPDATE A ";
                SQL += " SET A.IMPORTEMINUTOS = TOTALMINUTOS * C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'JORNAL' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_JORNADA, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";



                //Lberror.Text += " --> 3.2- REC_JORNADA - IMPORTEMINUTOS" + SQL + Environment.NewLine;
                DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += Variables.Error + Environment.NewLine;



                //Termino Jornadas. Este lanzamiento despues del anterior TOTALTIEMPO seguidos no funciona en SQl Server asi que intercalo produccion.


                SQL = " UPDATE A ";
                SQL += " SET A.TOTALIMPORTE = TOTALTIEMPO * C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'JORNAL' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_JORNADA, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";

                DBHelper.ExecuteNonQuery(SQL);

                //Importe horas laborales
                SQL = " UPDATE A ";
                SQL += " SET A.TIMPNHORAS = NHORAS * C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'JORNAL' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_JORNADA, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";

                DBHelper.ExecuteNonQuery(SQL);

                //Importe horas extras
                SQL = " UPDATE A ";
                SQL += " SET A.TIMPXHORAS = XHORAS * C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA, (B.TARIFA / 60) AS CUANTO  ";
                SQL += "                     FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO D ";
                SQL += "                     WHERE A.COD_EMPLEADO = D.COD_EMPLEADO ";
                SQL += "                     AND D.CATEGORIA = B.SUBTIPO ";
                SQL += "                     AND B.TIPO = 'EXTRA' ";
                SQL += "                     GROUP BY A.COD_EMPLEADO, A.FECHA_JORNADA, B.TARIFA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";

                DBHelper.ExecuteNonQuery(SQL);


                //Ahora que ya está empleado resto de Jornada nuevamente

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                //Campos por defecto
                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";

                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'No' WHERE VIVIENDA is null or  VIVIENDA = 'propia' or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);

                //De momento mes en curso TxtDesde.Text + "' AND '" + TxtHasta.Text
                SQL = "  UPDATE REC_EMPLEADO SET FECHAALTA_CALCULADA =(CASE  ";
                SQL += " WHEN FECHAALTA is NULL THEN ''  ";
                SQL += " WHEN FECHAALTA <= '" + TxtDesde.Text + "' THEN '" + TxtDesde.Text + "'  ";
                SQL += " WHEN FECHAALTA >= '" + TxtDesde.Text + "' THEN FECHAALTA  ";
                //SQL += " WHEN FECHAALTA >= '" + TxtHasta.Text + "' THEN FECHAALTA  ";               
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);

                SQL = "  UPDATE REC_EMPLEADO SET FECHABAJA_CALCULADA = (CASE  ";
                SQL += " WHEN FECHABAJA is NULL THEN ''  ";
                SQL += " WHEN FECHABAJA <= '" + TxtHasta.Text + "' AND FECHABAJA >= '" + TxtDesde.Text + "' THEN FECHABAJA  ";
                SQL += " ELSE '" + TxtHasta.Text + "'  ";
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE REC_JORNADA SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', FECHAALTA_CALCULADA), '19000101')), FECHABAJA_CALCULADA) ";

                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);

                //Rec_tarifa subtipo vacio en vivienda
                SQL = " UPDATE REC_JORNADA SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') , ";
                SQL += " TOTAL = TOTALIMPORTE - COSTEVIVIENDA ";
                SQL += " WHERE DIASMES <> -1 AND ALQVIVIENDA = 'Si' ";

                DBHelper.ExecuteNonQuery(SQL);

                //Ahora que ya está empleado resto de produccion nuevamente

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                //Campos por defecto
                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";

                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'No' WHERE VIVIENDA is null or  VIVIENDA = 'propia' or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);

                //De momento mes en curso

                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE REC_PRODUCCION SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', getdate()), '19000101')), getdate()) ";

                SQL = " UPDATE REC_PRODUCCION SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') ";
                SQL += " WHERE DIASMES <> 0 AND ALQVIVIENDA = 'Si' ";
                DBHelper.ExecuteNonQuery(SQL);
                //Lbmensaje.Text = "Posicionando Fichajes...";
                //pmtProc1.Text = "Posicionando Fichajes...";
                //REC_CALENDARIO
                //--CreaNomina("Total", this.Session["UltimaConsulta"].ToString(), "");

                //REC_EMPLEADO
                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIASMES, ";
                SQL += "     A.ALQVIVIENDA = C.ALQVIVIENDA, ";
                SQL += "     A.COSTEVIVIENDA = C.COSTEVIVIENDA ";
                SQL += " FROM REC_EMPLEADO A,(SELECT COD_EMPLEADO, DIASMES, ALQVIVIENDA ";
                SQL += "                     FROM REC_JORNADA ";
                SQL += "                      WHERE COD_EMPLEADO  ";
                SQL += "                      GROUP BY COD_EMPLEADO, DIASMES, ALQVIVIENDA ) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE A ";
                SQL += " SET A.IMPORTE = C.IMPORTE, ";
                SQL += "    A.INFORME = C.REPORTE ";
                SQL += " FROM REC_EMPLEADO A,(SELECT A.COD_EMPLEADO, N.REPORTE , CASE WHEN N.REPORTE = '1' ";
                SQL += "                      THEN ";
                SQL += "                          (SELECT SUM(TOTALDIA) FROM REC_CALENDARIO WHERE COD_EMPLEADO = A.COD_EMPLEADO) ";
                SQL += "                      ELSE ";
                SQL += "                         (SELECT SUM(TIMPNHORAS + TIMPXHORAS) FROM REC_CALENDARIO WHERE COD_EMPLEADO = A.COD_EMPLEADO) ";
                SQL += "                      END as IMPORTE ";
                SQL += "                     FROM REC_CALENDARIO A, REC_EMPLEADO B, REC_PARAM N ";
                SQL += "                      WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                SQL += "                      AND B.CENTRO = N.CENTRO ";
                SQL += "                      GROUP BY A.COD_EMPLEADO, N.REPORTE ) C ";
                SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                DBHelper.ExecuteNonQuery(SQL);

                int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));

                int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
                if (dias != 0)
                {
                    SQL = " UPDATE A ";
                    SQL += " SET A.NOMINA = C.NOMINA ";
                    SQL += " FROM REC_EMPLEADO A, (SELECT ";
                    SQL += "                       CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * 30 / R.DIASMES) ";
                    SQL += "                        WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * " + diasNaturales + " / R.DIASMES) END AS NOMINA ";
                    SQL += "                        FROM REC_JORNADA R, REC_TARIFAS N ";
                    SQL += "                        WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += "                        GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA, R.DIASMES) C ";
                    SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                }
                else
                {
                    SQL = " UPDATE A ";
                    SQL += " SET A.NOMINA = C.NOMINA ";
                    SQL += " FROM REC_EMPLEADO A, (SELECT ";
                    SQL += "                       CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) ";
                    SQL += "                       WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) END AS NOMINA ";
                    SQL += "                       FROM REC_JORNADA R, REC_TARIFAS N ";
                    SQL += "                       WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += "                       GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA) C ";
                    SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                }
                DBHelper.ExecuteNonQuery(SQL);

                Lberror.Text += " Resto de Consultas fin";

                //Lbmensaje.Text = "Cargando las listas en cada apartado...";
                //pmtProc1.Text = "Cargando las listas en cada apartado...";


                Carga_tablaEmpleados();


                UltimaConsulta(null);
                //Lbmensaje.Text = "El proceso finalizó correctamente";
                //pmtProc1.Text = "El proceso finalizó correctamente";

                //HtmlGenericControl li = (HtmlGenericControl)Master.FindControl("DvPreparado");
                //li.Visible = false;
                //li = (HtmlGenericControl)Master.FindControl("cuestion");
                //li.Visible = false;
                //li = (HtmlGenericControl)Master.FindControl("Asume");
                //li.Visible = false;

                DvPreparado.Visible = false;
                cuestion.Visible = false;
                Asume.Visible = false;


                Lberror.Visible = false;
                //imgLoad.Visible = false;

            }
            catch (Exception ex)
            {
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas(SQL, "1", " GralConsultalocal --> Error:" + ex.Message + " --> " + context.Session["Procedimiento"].ToString());

                //string a = Main.Ficherotraza("GralConsultalocal --> " + ex.Message + " --> " + SQL);
                Lberror.Visible = true;
                Lberror.Text += Variables.Error + " --> " + SQL + Environment.NewLine;
                //imgLoad.Visible = false;
                return;
            }
            //Lberror.Text = "";
        }

        private void ExportGridToExcel(string NameFile, GridView MiGrid)
        {
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            DataTable dt = null;
            //

            if (MiGrid.ID == "gvEmpleado") { Carga_tablaEmpleadosSQL(); dt = Carga_tablaEmpleadosXLS(this.Session["SQL"].ToString()); }
  

            wbook.Worksheets.Add(dt, NameFile);
            NameFile += " " + DateTime.Now.ToString("dd-MM-yyyy H-mm-ss");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Provide you file name here
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + NameFile + ".xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        protected void Colapsar_click(object sender, EventArgs e)
        {

        }

        protected void PDF_WORD_click(object sender, EventArgs e)
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
            //BtLimpiaTodo_Click(null, null);
            //BtGralConsultaMin_Click(null, null);
            DrVistaEmpleado.SelectedIndex = -1;
            //DrFlujo.SelectedIndex = -1;
            //DrFlujoEstado.SelectedIndex = -1;
            DrVistaEmpleado_SelectedIndexChanged(sender, e);

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

        protected void btDriver_Click(object sender, EventArgs e)
        {
            string fileName = "Wacom-STU-Driver-5.4.5.exe";
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", fileName));
            Response.WriteFile(Server.MapPath(@"~/bin/Wacom-STU-Driver-5.4.5.exe"));
            Response.End();

            //System.Net.WebClient webClient = new System.Net.WebClient();
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //string[] Separado = url.Split('/');
            //url = "";
            //if (Separado.Count() > 0)
            //{
            //    for (int i = 0; i < Separado.Count() - 1; i++)
            //    {
            //        if (Separado[i].ToString().Contains("http"))
            //        {
            //            url += Separado[i] + "//";
            //        }
            //        else
            //        {
            //            url += Separado[i] + "/";
            //        }
            //    }
            //    url += "/Bin/Wacom-STU-Driver-5.4.5.exe";
            //}

            //Stream data = webClient.OpenRead(url);
            //string filename = Path.GetFileName(url);
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            //data.CopyTo(Response.OutputStream);
            //Response.End();
        }

        protected void btnSDK_Click(object sender, EventArgs e)
        {
            string fileName = "Wacom-STU-SDK-x86-2.15.4.msi";
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", fileName));
            Response.WriteFile(Server.MapPath(@"~/bin/Wacom-STU-SDK-x86-2.15.4.msi"));
            Response.End();



            //System.Net.WebClient webClient = new System.Net.WebClient();
            ////string url = HttpContext.Current.Request.Url.AbsoluteUri + "/Bin/WacomTablet_6.3.46-1.exe";
            ////"https://www.w3.org/WAI/ER/tests/xhtml/testfiles/resources/pdf/dummy.pdf";
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //string[] Separado = url.Split('/');
            //url = "";
            //if (Separado.Count() > 0)
            //{
            //    for (int i = 0; i < Separado.Count() - 1; i++)
            //    {
            //        if (Separado[i].ToString() != "")
            //        {
            //            if (Separado[i].ToString().Contains("http"))
            //            {
            //                url += Separado[i] + "//";
            //            }
            //            else
            //            {
            //                url += Separado[i] + "/";
            //            }
            //        }
            //    }
            //    url += "bin/WacomTablet_6.3.46-1.exe";
            //}
            //Stream data = webClient.OpenRead(url);
            //string filename = Path.GetFileName(url);
            //Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            //data.CopyTo(Response.OutputStream);
            //Response.End();
        }

        protected void ImageBtn_Click(object sender, EventArgs e)
        {

            string SQL = "UPDATE ZCARGA_LINEA set ESTADO =  2 ";

            Variables.Error = "";
            Lberror.Text = SQL;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaEmpleados();

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
        protected void CierraRegistro_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString())) == true)
            {
                System.IO.Directory.Delete(Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString()), true);
            }
            DivGridDoc.Visible = true;
            DivFicha.Visible = false;
            DivDispositivos.Visible = true;
            BtdocAdjunto.Visible = false;
            H3TituFicha.Visible = false;
            H3Titulo.Visible = true;
            BtOpenFicha.Visible = false;
            DivCampos0.Visible = false;
            UpdateCampos.Update();
        }

        protected void TreeDoc_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeDoc.SelectedNode.Value == "-1")
            {
            }
            else
            {
                if (TreeDoc.SelectedNode.Value == "0")
                {
                    //El root
                }
                else
                {
                    string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                    SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + TreeDoc.SelectedNode.Value;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];

                    foreach (DataRow fila2 in dt.Rows)
                    {
                        if (fila2["ZID"].ToString() == TreeDoc.SelectedNode.Value)
                        {
                            //descarga fichero
                            break;
                        }
                    }
                }

            }
        }


        protected void BtnMuestra_Click(object sender, EventArgs e)
        {
        }

        protected void btListaEmpresas_Click(object sender, EventArgs e)
        {
            //ContentPlaceHolder cont = new ContentPlaceHolder();
            //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //if (EmpresaGV.Visible == true)
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)cont.FindControl("idgvEmpleado");
            //    li.Attributes["class"] = "fa fa-angle-down fa-2x";
            //    EmpresaGV.Visible = false;
            //}
            //else
            //{
            //    HtmlGenericControl li = (HtmlGenericControl)cont.FindControl("idgvEmpleado");
            //    li.Attributes["class"] = "fa fa-angle-up fa-2x";
            //    //if (CaCheck.Checked == false)
            //    //{
            //    //    Carga_tablaJornada();
            //    //}
            //    //else
            //    //{
            //    //    Carga_tablaCabeceraClose();
            //    //}

            //    EmpresaGV.Visible = true;
            //}
        }



        private void PopulateRootLevel()
        {
            //Grid Ficheros, El arbol jerarquico falta por definir en tabla. root desfase por orden id, root y secuencia de creacion para directorios posteriores
            //TreeDoc.Nodes.Clear();
            //dt = Main.CargaArbolDocumentos(Convert.ToInt32(this.Session["idregistro"].ToString()), Convert.ToInt32((string)Session["MiNivel"]), this.Session["TablaObj"].ToString(), "1");

            return;
            //DataTable dt = new DataTable();
            //TreeDoc.Nodes.Clear();
            //dt = Main.CargaArbolDocumentos(Convert.ToInt32(this.Session["idregistro"].ToString()), Convert.ToInt32((string)Session["MiNivel"]), this.Session["TablaObj"].ToString(), "1");

            //if (dt.Rows.Count == 0)
            //{
            //    PopulateNodes(dt, TreeDoc.Nodes, 0);
            //}
            //else
            //{
            //    PopulateNodes(dt, TreeDoc.Nodes, 1);
            //}
        }

        private void PopulateNodoNuevo(TreeNodeCollection nodes)
        {
            TreeNode tn = new TreeNode();
            tn.Text = "Documentación asociada a este registro...";
            tn.Value = "-1";
            nodes.Add(tn);
        }

        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes, int Nulo)
        {
            //if (DivCampos0.Visible == false)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        TreeNode tn = new TreeNode();
            //        tn.Text = dr["ZDESCRIPCION"].ToString();
            //        tn.Value = dr["ZID"].ToString();
            //        nodes.Add(tn);
            //        int Cuantos = Convert.ToInt32(dr["zHIJOS"].ToString());
            //        if (Cuantos > 0)
            //        {
            //            PopulateSubLevel(Convert.ToInt32(tn.Value), tn);
            //        }
            //    }
            //}
            //else
            //{
                if (Nulo == 0)
                {
                    TreeNode tn = new TreeNode(); //TreeDoc
                    tn.Text = "Ninguno";
                    tn.Value = 0.ToString();
                    nodes.Add(tn);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TreeNode tn = new TreeNode(); //TreeDoc
                        tn.Text = dr["ZDESCRIPCION"].ToString();
                        tn.Value = dr["ZID"].ToString();
                        nodes.Add(tn);
                        int Cuantos = Convert.ToInt32(dr["zHIJOS"].ToString());
                        if (Cuantos > 0)
                        {
                            PopulateSubLevel(Convert.ToInt32(tn.Value), tn);
                        }
                    }
                }
            //}
        }
        private void PopulateSubLevel(int parentid, TreeNode parentNode)
        {
            DataTable dt = new DataTable();

            dt = Main.CargaArbolDocumentos(parentid, Convert.ToInt32((string)Session["MiNivel"]), this.Session["TablaObj"].ToString(), "0");

            PopulateNodes(dt, parentNode.ChildNodes, 1);

        }
        protected void ActualizaDatos_Click(object sender, EventArgs e)
        {
            //ImageEjecucion.Visible = false;
            //ImagenEstado.Visible = true;

            string ColumnaSel = DrListaColumna.SelectedItem.Text;
            this.Session["GridOrden"] = ColumnaSel;

            DataTable dtCampos = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dtCampos;

            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
            CreaGridControl(dtArchivos, dtCampos);
            //Carga_tablaControl("",dtArchivos, dtCampos, this.Session["idestado"].ToString(), null, this.Session["idflujo"].ToString(),"0");
            Carga_tablaControl(DrListaColumna.SelectedValue, dtArchivos, dtCampos, this.Session["idestado"].ToString(), ColumnaSel, this.Session["idflujo"].ToString(), "0");
           
            if (gvEmpleado.Rows.Count > 0)
            {
                SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"] + " ";
                Object Eje = DBHelper.ExecuteScalarSQL(SQL, null);
                if (Eje.ToString() != null)
                {
                    if (Eje.ToString() == "1")
                    {
                        if (this.Session["MiNivel"].ToString() == "0")
                        {
                            //ImgInicia.Visible = false;
                            LbAutomatico.Visible = false;

                        }
                        else
                        {
                            //ImgInicia.Visible = true;
                            LbAutomatico.Visible = true;
                        }
                    }
                    else if (Eje.ToString() == "2")
                    {
                        //ImgInicia.Visible = false;
                        LbAutomatico.Visible = false;
                    }
                    else
                    {
                        //ImgInicia.Visible = false;
                        LbAutomatico.Visible = false;
                    }
                }
            }

            UpdatePanelGV.Update();
            UpdatePanelEje.Update();
            UpdateMenu.Update();
            UpdateCampos.Update();
        }


        protected void DrVistaEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Archivo seleccionado
            Limpiar_Click(sender,e);
            string SQL = "";
            //ImageEjecucion.Visible = false;
            //ImagenEstado.Visible = true;
            TextAlerta.Text = "";
            alerta.Visible = false;
            this.Session["FiltroConsulta"] = 1;
            try
            {
                int n;
                bool isNumeric = int.TryParse(DrVistaEmpleado.SelectedItem.Value, out n);
                if (isNumeric == true)
                {
                    if (DrVistaEmpleado.SelectedItem.Value == "0")
                    {
                        DrFlujo.Items.Clear();
                        DrFlujo.DataValueField = "ZID";
                        DrFlujo.DataTextField = "ZDESCRIPCION";
                        DrFlujo.Items.Insert(0, new ListItem("Ninguno", "0"));

                        DrFlujoEstado.Items.Clear();
                        DrFlujoEstado.DataValueField = "ZID";
                        DrFlujoEstado.DataTextField = "ZNOMBRE";

                        DrFlujoEstado.Items.Insert(0, new ListItem("Ninguno", "0"));
                    }
                    else
                    {
                        this.Session["idarchivo"] = n;
                        DataTable dtCampos = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dtCampos;


                        SQL = " SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, A.ZVIEW, A.ZTABLENAME, A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, ";
                        SQL += " A.ZID_FLUJO, A.ZID_ESTADOFLUJO,  B.ZDESCRIPCION AS ID_ESTADO ";
                        if (this.Session["MiNivel"].ToString() == "0")
                        {
                            SQL += " FROM ZARCHIVOS A, ZESTADOSFLUJO B WHERE  A.ZID = " + this.Session["idarchivo"].ToString();
                        }
                        else
                        {
                            SQL += " FROM ZARCHIVOS A, ZESTADOSFLUJO B WHERE A.ZNIVEL <= " + this.Session["MiNivel"] + " AND A.ZID = " + this.Session["idarchivo"].ToString();
                        }
                        SQL += " AND A.ZID_ESTADOFLUJO = B.ZID ";
                        DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

                        dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
                        CreaGridControl(dtArchivos, dtCampos);

                        //DataTable dtObjetos = Main.CargaRelacionesdocumentosArchivos(n, 1, this.Session["TablaObj"].ToString()).Tables[0];

                        //PopulateRootLevel();
                        DataTable dt = new DataTable();
                        DataTable dt2 = new DataTable();

                        string Flujos = "";
                        string Estados = "";

                        SQL = " SELECT A.ZID_ARCHIVO, A.ZID_FLUJO ";
                        SQL += " FROM ZARCHIVOFLUJOS A WHERE A.ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                        DataTable dt1 = Main.BuscaLote(SQL).Tables[0];

                        foreach (DataRow fila in dt1.Rows)
                        {
                            if(Flujos == "")
                            {
                                Flujos += fila["ZID_FLUJO"].ToString();
                            }
                            else
                            {
                                Flujos += ", " + fila["ZID_FLUJO"].ToString();
                            }
                            if (Estados == "")
                            {
                                Estados += fila["ZID_FLUJO"].ToString();
                            }
                            else
                            {
                                Estados += ", " + fila["ZID_FLUJO"].ToString();
                            }
                        }

                        string[] Fields = System.Text.RegularExpressions.Regex.Split(Flujos, ",");
                        if(Fields.Length == 0)
                        {
                            dt = Main.CargaEstadosFlujos(Fields[0], "0").Tables[0];
                        }
                        else
                        {
                            dt = Main.CargaEstadosFlujos(Flujos, "0").Tables[0];
                        }

                        dt = Main.CargaEstadosFlujos(Flujos, "0").Tables[0];
                        dt2 = Main.CargaFlujos(Flujos, "0").Tables[0];

                        //Busca si es el primero
                        foreach (DataRow fila2 in dt.Rows)
                        {
                            //ImgDetiene.Visible = false;

                            LbimgEstado.InnerText = fila2["ZNOMBRE"].ToString();
                            LbimgEstado.Visible = true;
                            //ImagenEstado.Visible = true;
                            break;
                        }

                        DrFlujo.Items.Clear();
                        DrFlujo.DataValueField = "ZID";
                        DrFlujo.DataTextField = "ZDESCRIPCION";
                        DrFlujo.Items.Insert(0, new ListItem("Ninguno", "0"));

                        DrFlujo.DataSource = dt2;
                        DrFlujo.DataBind();

                        this.Session["EstadosFlujo"] = dt;

                        DrFlujoEstado.Items.Clear();
                        DrFlujoEstado.DataValueField = "ZID";
                        DrFlujoEstado.DataTextField = "ZNOMBRE";

                        DrFlujoEstado.Items.Insert(0, new ListItem("Ninguno", "0"));

                        DrFlujoEstado.DataSource = dt;
                        DrFlujoEstado.DataBind();

                        //DrFindEstado.Items.Clear();
                        //DrFindEstado.DataValueField = "ZID";
                        //DrFindEstado.DataTextField = "ZNOMBRE";

                        //DrFindEstado.Items.Insert(0, new ListItem("Todos", "Todos"));

                        //DrFindEstado.DataSource = dt;
                        //DrFindEstado.DataBind();
                        LbArchivoDoc.InnerText = this.Session["TablaName"].ToString();
                        //if (this.Session["MiNivel"].ToString() == "0")
                        //{
                        //    DrFlujo.SelectedIndex = 1;
                        //    DrFlujoEstado.SelectedIndex = 1;
                        //}
                        //else
                        //{
                            DrFlujo.SelectedIndex = -1;
                            DrFlujoEstado.SelectedIndex = -1;
                        //}
                        this.Session["idflujo"] = DrFlujo.SelectedItem.Value;
                        this.Session["idestado"] = DrFlujoEstado.SelectedItem.Value;

                        Carga_tablaControl("",dtArchivos, dtCampos, DrFlujoEstado.SelectedItem.Value, null, DrFlujo.SelectedItem.Value, "0");
                        if (gvEmpleado.Rows.Count > 0)
                        {
                            SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                            SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"] + " ";
                            Object Eje = DBHelper.ExecuteScalarSQL(SQL, null);
                            if (Eje.ToString() != null)
                            {
                                if (Eje.ToString() == "1")
                                {
                                    if (this.Session["MiNivel"].ToString() == "0")
                                    {
                                        //ImgInicia.Visible = false;
                                        LbAutomatico.Visible = false;

                                    }
                                    else
                                    {
                                        //ImgInicia.Visible = true;
                                        LbAutomatico.Visible = true;
                                    }
                                }
                                else if (Eje.ToString() == "2")
                                {
                                    //ImgInicia.Visible = false;
                                    LbAutomatico.Visible = false;
                                }
                                else
                                {
                                    //ImgInicia.Visible = false;
                                    LbAutomatico.Visible = false;
                                }
                            }
                        }

                        SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA, ZQUERY, ZID_PROCEDIMIENTO, ZCAMPODOC, ZCAMPOFILTRO, ";
                        SQL += " ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS FROM  ZPROFILES ";
                        SQL += " WHERE ZID_FLUJO = " + DrFlujo.SelectedValue + " ";
                        SQL += " AND ZID_ARCHIVO = " + DrVistaEmpleado.SelectedValue + " ";
                        //SQL += " AND ZID_PAGINA = " + this.Session["Pagina"] + " ";
                        dt1 = Main.BuscaLote(SQL).Tables[0];
                        UltimaConsulta(dt1);
                    }

                    DrFlujoEstado_Changed(null, null);
                }
                else
                {
                    DrFlujo.Items.Clear();
                    DrFlujoEstado.Items.Clear();
                }
            }
            catch (NullReferenceException ex)
            {
                //Lberror.Text += ex.Message;
                string a = Main.Ficherotraza("DVariedad_SelectedIndexChanged-->" + ex.Message);
                //alerta.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }
        }

        private void Carga_tablaControl(string CampoOrden, DataTable dtArchivos, DataTable dtCampos, string ID_Estado,  string sortExpression = null, string Flujo = null, string MiRows = "0")
        {
            //cualquier Tabla 
            string SQL = "";
            string Tabla = "";
            string Vista = "";
            string Key = "";
            int Conexion = 0;
            DataTable dt = null;
            string Dato = "";
            string MiCampo = "";
            string OrderBy = CampoOrden;

            try
            {
                if (this.Session["idarchivo"].ToString() == "0")
                {
                    gvEmpleado.DataSource = dt;
                    gvEmpleado.DataBind();
                    return;
                }
                foreach (DataRow fila in dtArchivos.Rows)
                {
                    if (fila["ZVIEW"].ToString() == "" || fila["ZVIEW"].ToString() == "0" || fila["ZVIEW"].ToString() == null)
                    {
                        Vista = "";
                        Conexion = 0;
                    }
                    else
                    {
                        Vista = fila["ZVIEW"].ToString();
                        Conexion = Convert.ToInt32(fila["ZCONEXION"].ToString());
                        //dt = Main.BuscaLote(Vista).Tables[0];
                    }
                    break;
                }

                foreach (DataRow fila in dtArchivos.Rows)
                {
                    Key = fila["ZKEY"].ToString();
                    if (Key == "0" || Key == "" || Key == null)
                    {
                        Key = "";
                    }
                    else
                    {
                        foreach (DataRow fila2 in dtCampos.Rows)
                        {
                            if (fila2["ZID"].ToString() == fila["ZKEY"].ToString())
                            {
                                MiCampo = fila2["ZTITULO"].ToString();
                                //dt = Main.BuscaLote(Vista).Tables[0];
                                OrderBy = " ORDER BY ZID ";
                                break;
                            }
                        }
                    }
                    break;
                }

                if (Vista != "") //Vista a consultar
                {
                    dt = Main.BuscaLoteConexion(Vista, Conexion).Tables[0];
                }
                else if (MiCampo != "") //Key identity distinta
                {
                    SQL = "";

                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();
                        if (fila["ZTITULO"].ToString() == "NOMBRECOMPLETO")
                        {
                            this.Session["NombreCompleto"] = "NOMBRECOMPLETO";
                        }

                        if (SQL == "")
                        {
                            if (MiCampo == fila["ZTITULO"].ToString() && MiCampo != "ZID")
                            {
                                SQL += fila["ZTITULO"].ToString() + " AS ZID, " + MiCampo + " ";
                            }
                            else
                            {
                                SQL += fila["ZTITULO"].ToString();
                                
                            }
                        }
                        else
                        {
                            if (MiCampo == fila["ZTITULO"].ToString() && MiCampo != "ZID")
                            {
                                SQL += "," + fila["ZTITULO"].ToString() + " AS ZID,  " + MiCampo + " ";
                            }
                            else
                            {
                                SQL += "," + fila["ZTITULO"].ToString();
                            }
                        }
                    }

                    if (Dato == "") { Dato = "0"; }

                    SQL = " SELECT " + SQL;
                    SQL += "  FROM " + Tabla;
                    if(Flujo != null)
                    {
                        SQL += " WHERE ZID_ESTADO = " + DrFlujoEstado.SelectedItem.Value;
                        SQL += OrderBy;
                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                    else
                    {
                        SQL += OrderBy;
                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                }
                else //Key ID Normal
                {
                    SQL = "";
                    if (MiCampo == "")
                    {
                        foreach (DataRow fila in dtCampos.Rows)
                        {
                            if (fila["ZTITULO"].ToString() == "ZID")
                            {
                                MiCampo = fila["ZTITULO"].ToString();
                                OrderBy = " ORDER BY ZID ";
                                break;
                            }
                        }
                    }

                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();
                        if (fila["ZTITULO"].ToString() == "NOMBRECOMPLETO")
                        {
                            this.Session["NombreCompleto"] = "NOMBRECOMPLETO";
                        }

                        if (SQL == "")
                        {
                            if (MiCampo == "")
                            {
                                SQL += fila["ZTITULO"].ToString() + " AS ZID, " + fila["ZTITULO"].ToString() + " ";
                                MiCampo = "ZID";
                            }
                            else
                            {
                                if (MiCampo == fila["ZTITULO"].ToString() && MiCampo != "ZID")
                                {
                                    SQL += fila["ZTITULO"].ToString() + " AS ZID, " + MiCampo + " ";
                                }
                                else
                                {
                                    SQL += fila["ZTITULO"].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (MiCampo == "")
                            {
                                SQL += fila["ZTITULO"].ToString() + " AS ZID, " + fila["ZTITULO"].ToString() + " ";
                                MiCampo = "ZID";
                            }
                            else
                            {
                                if (MiCampo == fila["ZTITULO"].ToString() && MiCampo != "ZID")
                                {
                                    SQL += "," + fila["ZTITULO"].ToString() + " AS ZID,  " + MiCampo + " ";
                                }
                                else
                                {
                                    SQL += "," + fila["ZTITULO"].ToString();
                                }
                            }
                        }
                    }



                    Carga_FiltrosGral(SQL);
                    string filtro = this.Session["Filtro"].ToString();

                    if (Dato == "") { Dato = "0"; }

                    SQL = " SELECT " + SQL;
                    SQL += "  FROM " + Tabla;
                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    if (Flujo != null)
                    {
                        SQL += " WHERE ZID_FLUJO = " + Flujo;
                        if(ID_Estado != null)
                        {
                            SQL += " AND ZID_ESTADO = " + ID_Estado;
                        }
                        SQL += OrderBy;
                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                    else
                    {
                        SQL += OrderBy;
                        dt = Main.BuscaLote(SQL).Tables[0];
                    }
                }
                //ZTIPO_PLANTA,ZTIPO_FORMATO,ZNUMERO_PLANTAS,ZID_TIPO_FORMATO,ZID 

                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;
                if(MiRows == "0")
                {
                    this.Session["Registros"] = dt.Rows.Count;
                }

                lbRowEmpleado.Text = "Registros: " + dt.Rows.Count;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    //this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvEmpleado.DataSource = dv;
                    //gvCabecera.DataSource = dv;
                }
                else
                {
                    gvEmpleado.DataSource = dt;
                    //gvCabecera.DataSource = dt;
                }
                gvEmpleado.DataBind();

                Rechazados(gvEmpleado);

            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }


        private void CreaGridControl(DataTable dtArchivo, DataTable dtCampo)
        {
            this.Session["Campos"] = dtCampo;
            //gvEmpleado.Columns.Clear();
            //for (int X = 1; X < gvEmpleado.Columns.Count; X++)
            //{
            //    if(gvEmpleado.Columns[X].HeaderText == "Todos")
            //    {
            //    }
            //    else
            //    {
            //        gvEmpleado.Columns.RemoveAt(X);
            //    }
            //}

            //for (int i = gvEmpleado.Columns.Count - 1; i >= 2; i--)
            //////for (int X = 2; X < gvEmpleado.Columns.Count; X++)
            //{
            //    gvEmpleado.Columns.RemoveAt(i);//column index
            //}
            //gvEmpleado.AutoGenerateColumns = false;

            string MiCampo = "";
            Boolean Esta = false;

            if (gvEmpleado.Columns.Count > 1)
            {
                foreach (DataRow filaKey in dtArchivo.Rows)
                {
                    if (filaKey["ZKEY"].ToString() != "")
                    {
                        foreach (DataRow filas in dtCampo.Rows)
                        {
                            if (filaKey["ZKEY"].ToString() == filas["ZID"].ToString())
                            {
                                string SQL = "";
                                try
                                {
                                    if (DrFlujoEstado.SelectedIndex == -1)
                                    {
                                        SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID = 0";
                                    }
                                    else
                                    {
                                        SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID =" + DrFlujoEstado.SelectedItem.Value + " ";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID =" + filaKey["ZID_ESTADOFLUJO"].ToString() + " ";
                                    string m = Main.ETrazas("", "1", " CreaGridControl --> DrFlujoEstado.SelectedIndex = Pasó por Error:" + ex.Message);
                                    //continue;
                                }
                                DataTable dtEstados = Main.BuscaLote(SQL).Tables[0];

                                foreach (DataRow fil in dtEstados.Rows)
                                {
                                    ImgFlujo = fil["ZIMG"].ToString();
                                    //ImagenEstado.ImageUrl = ImgFlujo;
                                    LbAyuda.Text = fil["ZDESCRIPCION"].ToString();


                                    //gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = ImgFlujo.ToString() });
                                    Esta = true;
                                    break;
                                }
                                MiCampo = filas["ZTITULO"].ToString();

                                break;
                            }
                        }
                        if (Esta == false)
                        {
                            MiCampo = "ZID";
                            ImgFlujo = "~/Images/docerror.png";
                        }
                    }
                    else
                    {
                        foreach (DataRow filas in dtCampo.Rows)
                        {
                            if (filas["ZTITULO"].ToString() == "ZID")
                            {
                                MiCampo = filas["ZTITULO"].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                gvEmpleado.AutoGenerateColumns = false;

                int Manual = 0;
                int cuantos = 0;
                //int i = 0;
#pragma warning disable CS0219 // La variable 'a' está asignada pero su valor nunca se usa
                int a = 0;
#pragma warning restore CS0219 // La variable 'a' está asignada pero su valor nunca se usa

                //string[] CamposConsulta = null;
#pragma warning disable CS0219 // La variable 'data' está asignada pero su valor nunca se usa
                string data = "";
#pragma warning restore CS0219 // La variable 'data' está asignada pero su valor nunca se usa

                try
                {
                    if (Manual == 0) //Manual. La variable en web.config
                    {
                        cuantos = dtCampo.Rows.Count;

                        

                        HtmlGenericControl DivContent = new HtmlGenericControl();


                        //Busco la clave
                        Esta = false;
                        foreach (DataRow filaKey in dtArchivo.Rows)
                        {
                            if (filaKey["ZKEY"].ToString() != "")
                            {
                                foreach (DataRow filas in dtCampo.Rows)
                                {
                                    if (filaKey["ZKEY"].ToString() == filas["ZID"].ToString())
                                    {
                                        string SQL = "";
                                        try
                                        {
                                            if (DrFlujoEstado.SelectedIndex == -1)
                                            {
                                                SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID = 0";
                                            }
                                            else
                                            {
                                                SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID =" + DrFlujoEstado.SelectedItem.Value + " ";
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            SQL = "SELECT ZIMG, ZDESCRIPCION FROM ZESTADOSFLUJO WHERE ZID =" + filaKey["ZID_ESTADOFLUJO"].ToString() + " ";
                                            string m = Main.ETrazas("", "1", " CreaGridControl --> DrFlujoEstado.SelectedIndex = Pasó por Error:" + ex.Message);
                                            //continue;
                                        }
                                        DataTable dtEstados = Main.BuscaLote(SQL).Tables[0];

                                        foreach (DataRow fil in dtEstados.Rows)
                                        {
                                            ImgFlujo = fil["ZIMG"].ToString();
                                            //ImagenEstado.ImageUrl = ImgFlujo;
                                            LbAyuda.Text = fil["ZDESCRIPCION"].ToString();


                                            //gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = ImgFlujo.ToString() });
                                            Esta = true;
                                            break;
                                        }
                                        MiCampo = filas["ZTITULO"].ToString();

                                        //ImageField img = new ImageField();
                                        //img.HeaderText = MiCampo;
                                        //img.DataImageUrlField = "~/Images/firma.png";//Your Column Name Representing the image.
                                        //gvEmpleado.Columns.Add(img);

                                        //Object Con = DBHelper.ExecuteScalarSQL(SQL, null);

                                        //if (Con is null)
                                        //{
                                        //}
                                        //else
                                        //{
                                        //    ImgFlujo = Con.ToString();
                                        //    ImagenEstado.ImageUrl = ImgFlujo;

                                        //    gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = Con.ToString() });
                                        //}


                                        break;
                                    }
                                }
                                if (Esta == false)
                                {
                                    MiCampo = "ZID";
                                    //ImageField img = new ImageField();
                                    //img.HeaderText = MiCampo;
                                    //img.DataImageUrlField = "~/Images/Leer.png";//Your Column Name Representing the image.
                                    //gvEmpleado.Columns.Add(img);
                                    ImgFlujo = "~/Images/docerror.png";

                                    //gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = "~/Images/docerror.png" });
                                }
                            }
                            else
                            {
                                foreach (DataRow filas in dtCampo.Rows)
                                {
                                    if (filas["ZTITULO"].ToString() == "ZID")
                                    {
                                        MiCampo = filas["ZTITULO"].ToString();
                                        break;
                                    }
                                }
                            }
                        }




                        //Busca en los Campos
                        foreach (DataRow filas in dtCampo.Rows)
                        {
                            if (filas["ZTITULO"].ToString() != "ZID")
                            {
                                if (filas["ZTIPO"].ToString() == "7")
                                {
                                    //Añadiendo columnas al grid
                                    //gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = ImgFlujo.ToString() });
                                    //ButtonField imfImagen = new ButtonField();
                                    //imfImagen.CommandName = "ImgButton";
                                    //imfImagen.HeaderText = filas["ZDESCRIPCION"].ToString();
                                    //imfImagen.ImageUrl = filas["ZTITULO"].ToString();
                                    //imfImagen.DataTextField = filas["ZTITULO"].ToString();
                                    //imfImagen.ItemStyle.Height = 100;
                                    //imfImagen.ItemStyle.Width = 100;

                                    //imfImagen.ItemStyle()
                                    //gvEmpleado.AutoGenerateColumns = false;
                                    //Columna tipo imagen
                                    ImageField imfImagen = new ImageField();
                                    imfImagen.ShowHeader = true;
                                    imfImagen.HeaderText = filas["ZDESCRIPCION"].ToString();
                                    imfImagen.DataImageUrlField = filas["ZTITULO"].ToString();
                                    //imfImagen.ControlStyle.Height = 100;
                                    //imfImagen.ControlStyle.Width = 100;
                                    imfImagen.ItemStyle.Height = 20;
                                    imfImagen.ItemStyle.Width = 20;
                                   
                                    //imfImagen.ControlStyle.ImageAlign = "Middle"
                                    //imfImagen.ItemStyle("Height")
                                    DataControlField dcfControl = imfImagen;
                                    gvEmpleado.Columns.Add(dcfControl);
                                }
                                else if (filas["ZTIPO"].ToString() == "8")
                                {
                                    //Columna tipo Hyperlink
                                    HyperLinkField hlfLink = new HyperLinkField();
                                    hlfLink.ShowHeader = true;
                                    hlfLink.HeaderText = filas["ZDESCRIPCION"].ToString();
                                    hlfLink.DataTextField = filas["ZTITULO"].ToString();
                                    hlfLink.NavigateUrl = "";
                                    DataControlField dcfControl = hlfLink;
                                    gvEmpleado.Columns.Add(hlfLink);
                                }
                                else
                                {
                                    BoundField Campo = new BoundField();
                                    Campo.DataField = filas["ZTITULO"].ToString();
                                    Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                                    DataControlField DataControlField = Campo;
                                    gvEmpleado.Columns.Add(DataControlField);
                                }
                            }
                            else
                            {
                                gvEmpleado.Columns.Add(new ButtonField() { CommandName = "LeeDoc", HeaderText = "ID", DataTextField = "ZID", ImageUrl = ImgFlujo.ToString() });
                                //BoundField Campo = new BoundField();
                                //Campo.DataField = filas["ZTITULO"].ToString();
                                //Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                                //DataControlField DataControlField = Campo;
                                //gvEmpleado.Columns.Add(DataControlField);
                            }

                            //i += 1;
                        }


                        //DivCampos0.Attributes["height"] = (i * 50).ToString();
                    }



                    Lberror.Text = "";
                    //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    string m = Main.ETrazas("", "1", " CreaGridControl --> Error:" + ex.Message);
                }
            }

        }


        public DataTable Relaciones(int ID, DataTable DtCampos, int Actualiza)
        {
            int MiID = ID;
            //DrCampos.Items.Clear();
            //DrCampos.Items.Insert(0, new ListItem("Ninguno", "0"));

            //DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dt1 = new DataTable();

            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];

            if(Actualiza == 1)
            {
                DrListaColumna.Items.Clear();
                DrListaColumna.DataValueField = "ZID_CAMPO";
                DrListaColumna.DataTextField = "ZTITULO";

                DrListaColumna.Items.Insert(0, new ListItem("Ninguno", "0"));

                DrListaColumna.DataSource = dt1;
                DrListaColumna.DataBind();
            }

            this.Session["ArchivoCampos"] = dt1;
            int visto = 0;

            DataTable dtt;
            dtt = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZTITULO");
            dtt.Columns.Add("ZDESCRIPCION");
            dtt.Columns.Add("ZTABLENAME");
            dtt.Columns.Add("ZTABLEOBJ");
            dtt.Columns.Add("ZTIPO");
            dtt.Columns.Add("ZVALIDACION");
            dtt.Columns.Add("ZKEY");
            dtt.Columns.Add("KEYCAMPO");
            dtt.Columns.Add("ZTIPOARCHIVO");
            dtt.Columns.Add("ZDESARCHIVO");

            //Tablaseleccion.Text = "";

            DataRow drr;

            int i = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                visto = 0;
                foreach (DataRow fila in DtCampos.Rows)
                {
                    if (fila["ZID"].ToString() == dr["ZID_CAMPO"].ToString())
                    {
                        drr = dtt.NewRow();
                        drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                        drr[1] = fila["ZTITULO"].ToString();
                        drr[2] = fila["ZDESCRIPCION"].ToString();
                        drr[3] = dr["ZTABLENAME"].ToString();
                        drr[4] = dr["ZTABLEOBJ"].ToString();
                        drr[5] = dr["ZTIPO"].ToString();
                        drr[6] = dr["ZVALIDACION"].ToString();
                        drr[7] = dr["ZKEY"].ToString();

                        this.Session["TablaObj"] = dr["ZTABLEOBJ"].ToString();
                        this.Session["TablaName"] = dr["ZTABLENAME"].ToString();
                        //if (Tablaseleccion.Text == "")
                        //{
                        //    if (this.Session["Vista"].ToString() == "0")
                        //    {
                        //        Tablaseleccion.Text = " " + dr["ZTABLENAME"].ToString() + ", Tabla Objetos: (" + dr["ZTABLEOBJ"].ToString() + ")";
                        //    }
                        //    else
                        //    {
                        //        Object Con = DBHelper.ExecuteScalarSQL("SELECT ZDESCRIPCION FROM ZTIPOARCHIVO WHERE ZID =" + dr["ZTIPOARCHIVO"].ToString(), null);
                        //        if (Con == null)
                        //        {
                        //        }
                        //        else
                        //        {
                        //            Tablaseleccion.Text = " " + dr["ZDESARCHIVO"].ToString() + " (" + Con.ToString() + ")";
                        //        }
                        //    }
                        //}
                        drr[8] = dr["KEYCAMPO"].ToString();
                        drr[9] = dr["ZTIPOARCHIVO"].ToString();
                        this.Session["TablaObj"] = dr["ZTABLEOBJ"].ToString();
                        dtt.Rows.Add(drr);
                        //LBcampos.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        visto = 1;
                        i += 1;
                        break;
                    }
                }

                if (visto == 0)
                {
                    //DrCampos.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
            //sw.Close();
            if (i != 0)
            {
                this.Session["SelArchivoCampo"] = dtt;
            }
            return dtt;
        }


        protected void BtnuevaCabecera_Click(object sender, EventArgs e)
        {
                
        }

        private void SeleccionCabecera()
        {

        }



        protected void checkCabeceraListas_Click(object sender, EventArgs e)
        {
            gvEmpleado.EditIndex = -1;
            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);
            
        }
        protected void DrFlujoEstadoFile_Changed(object sender, EventArgs e)
        {
            DrFlujoEstado_Changed(sender, e);
        }
        

        protected void checkTodo_Click(object sender, EventArgs e)
        {
        //    if (LabelAltas.InnerText == "Todos los Empleados:")
        //    {
        //        LabelAltas.InnerText = "Sólo alta en esas fechas:";
        //        ImgAbiertos.Visible = false;
        //        ImgCerrados.Visible = true;
        //    }
        //    else
        //    {
        //        LabelAltas.InnerText = "Todos los Empleados:";
        //        ImgAbiertos.Visible = true;
        //        ImgCerrados.Visible = false;
        //    }
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            if (this.Session["SelectMes"].ToString() != "0")
            {
                DateTime date = DateTime.Now;

                string a = date.Month.ToString();
                DrMeses.SelectedIndex = Convert.ToInt32(a);
                this.Session["SelectMes"] = date.Month.ToString();

                DateTime dFirstDayOfThisMonth = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
                DateTime dLastDayOfLastMonth = dFirstDayOfThisMonth.AddDays(-1);
                DateTime dFirstDayOfLastMonth = dFirstDayOfThisMonth.AddMonths(-1);
                TxtDesde.Text = dFirstDayOfLastMonth.ToString().Substring(0, 10);
                TxtHasta.Text = dLastDayOfLastMonth.ToString().Substring(0, 10);

            }
            else
            {
                //Primero obtenemos el día actual
                DateTime date = DateTime.Now;

                string a = date.Month.ToString();
                DrMeses.SelectedIndex = Convert.ToInt32(a) -1;
                this.Session["SelectMes"] = DrMeses.SelectedIndex.ToString();
                //Asi obtenemos el primer dia del mes actual


                DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
                TxtDesde.Text = oPrimerDiaDelMes.ToString().Substring(0, 10);
                //TxtHasta.Text = date.ToString().Substring(0, 10);
                TxtHasta.Text = DateTime.Now.AddDays(-1).ToString().Substring(0, 10);
                //Y de la siguiente forma obtenemos el ultimo dia del mes
                //agregamos 1 mes al objeto anterior y restamos 1 día.
                //DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.Value.AddMonths(1).AddDays(-1);

            }
        }

        protected void ImageOrden_Click(object sender, EventArgs e)
        {

        }


        private void ChequeaKeyProceso()
        {
            CheckBox ChkBoxHeader = (CheckBox)gvEmpleado.HeaderRow.FindControl("chkb1Empleado");
            int i = 0;
            foreach (GridViewRow row in gvEmpleado.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemEmpleado");
                if (ChkBoxRows.Checked == true)
                {
                    string code = gvEmpleado.DataKeys[i].Value.ToString();

                    string SQL = " UPDATE " + this.Session["TablaName"].ToString() + " SET ZKEY = 1 ";
                    SQL += " WHERE ZID = " + code;

                    DBHelper.ExecuteNonQuery(SQL);
                    this.Session["FaltaDato"] = "1";
                    //string a = Main.ETrazas("", "1", " Envia Kkey =1  --> " + SQL);

                }
                i += 1;
            }
        }



        protected void btIniFlujo_Click(object sender, EventArgs e)
        {
            try
            {

                //Actualiza los registros en tabla a Estado 1
                //El Estado1 tiene asociado un Proceso de la TABLA ZPROCESOS
                //string parametros = "archivo/" + this.Session["idarchivo"].ToString() + " flujo/" + this.Session["idflujo"].ToString() + " estado/" + this.Session["idestado"].ToString() ;

                //Select en procesos con Zkey = 1
                this.Session["FaltaDato"] = "0";
                ChequeaKeyProceso();
                if(this.Session["FaltaDato"].ToString() == "0")
                {
                    lbCuestion.Text = "Debe activar al menos un check para procesar la documentación de algún registro.";
                    DivCuestion.Visible = true;
                    return;
                }
                //ImageEjecucion.Visible = true;
                //ImagenEstado.Visible = false;
                LbimgEstado.Visible = false;
                Lbejecutando.Visible = true;
                //ImgInicia.Visible = false;
                LbAutomatico.Visible = false;
                UpdateMenu.Update();

                string parametros = "";

                if (TxtDesde.Text == "0" || TxtDesde.Text == "1")
                {
                    parametros = " /" + this.Session["idarchivo"].ToString() + " /" + this.Session["idflujo"].ToString() + " /" + this.Session["idestado"].ToString() + " /" + TxtDesde.Text;
                }
                else
                {
                    parametros = " /" + this.Session["idarchivo"].ToString() + " /" + this.Session["idflujo"].ToString() + " /" + this.Session["idestado"].ToString();
                }



                string SQL = "UPDATE ZPROCESOS SET ZEJECUCION = 1, ZPARAMETROS = '" + this.Session["ComputerName"].ToString() + "' WHERE ZID_FLUJO =" + this.Session["idflujo"].ToString() + " AND ZID_ESTADO =" + this.Session["idestado"].ToString() + " AND ZID_ARCHIVO =" + this.Session["idarchivo"].ToString() ;
                DBHelper.ExecuteNonQuery(SQL);

                string Espera = Main.TrazaProceso(this.Session["ComputerName"].ToString(), null);

                //Metodo sincrono. Hasta que Procesos Automaticos no termine, no se pasa el testigo
                while (File.Exists(Espera))
                {
                }

                //ImageEjecucion.Visible = false;
                //ImagenEstado.Visible = true;
                LbimgEstado.Visible = true;
                Lbejecutando.Visible = false;
                //ImgInicia.Visible = true;
                LbAutomatico.Visible = true;

                DrFlujoEstado_Changed(null, null);

                UpdatePanelGV.Update();
                UpdatePanelEje.Update();
                UpdateMenu.Update();
                UpdateCampos.Update();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("Proceso lanzando --> " + ex.Message);
            }
        }


        public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }


        protected void btFinFlujo_Click(object sender, EventArgs e)
        {

        }
        

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {

        }

        protected void BtMenus_Click(object sender, EventArgs e)
        {

        }

        protected void DrConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carga_tablaProduccion();
        }

        protected void DrSelCab_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void DrSelectFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void BtnFirma_Click(object sender, EventArgs e)
        {
        }

        protected void BtnCambiaFirma_Click(object sender, EventArgs e)
        {
            string SQL = "UPDATE ZDISPOSITIVOSARCHIVOS SET ZID_DISPOSITIVO =" + DrDispositivos.SelectedItem.Value;
            SQL += " WHERE  ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
            SQL += " AND ZID_REGISTRO = " + this.Session["idregistro"].ToString();
            SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            DivCampos0.Visible = false;
            UpdateCampos.Update();
            //DrFlujoEstado_Changed(sender, e);
        }

        protected void BtnOpenFicha_Click(object sender, EventArgs e)
        {
            //Actualiza los documentos para que lea el usuario desde el listado
            //Update tablaobjetos set
            int Esta = 0;
            string SQL = "";
            CheckBox ChkBoxHeader = (CheckBox)gvCuestion.HeaderRow.FindControl("chkSI");
            //CheckBox ChkBoxNo = (CheckBox)gvCuestion.HeaderRow.FindControl("chkNO");
            foreach (GridViewRow row in gvCuestion.Rows)
            {
                Esta = 0;
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemSI");
                if (ChkBoxRows.Checked == true)
                {
                    Esta = 1;
                }
                else
                {
                    Esta = 0;
                }
                //if(Esta == false)
                //{
                //    ChkBoxRows = (CheckBox)row.FindControl("chbItemNO");
                //    if (ChkBoxRows.Checked == true)
                //    {
                //        Esta = true;
                //    }
                //    else
                //    {
                //        Esta = false;
                //        break;
                //    }
                //}
                
                Label LaID = (Label)row.FindControl("Labid");
                Label LaTemplate = (Label)row.FindControl("LabTemplate");
                Label LaArchivo = (Label)row.FindControl("Labarchivo");
                Label LaFlujo = (Label)row.FindControl("Labflujo");
                Label LaEstado = (Label)row.FindControl("Labestado");
                Label LaCuestion = (Label)row.FindControl("Labcuestion");
                

                SQL = " SELECT ZID FROM ZTEMPLATECUESTION ";
                SQL += " WHERE ZID_TEMPLATE = " + LaTemplate.Text;
                SQL += " AND ZID_CUESTION = " + LaCuestion.Text;
                SQL += " AND ZID_ARCHIVO = " + LaArchivo.Text;
                SQL += " AND ZID_FLUJO = " + LaFlujo.Text;
                SQL += " AND ZID_ESTADO = " + LaEstado.Text;
                SQL += " AND ZID_REGISTRO = " + this.Session["idregistro"].ToString();
                //SQL += " AND ZID_DATO = " + Esta;
                SQL += " AND ZFECHA is not NULL ";

                Object Coni = DBHelper.ExecuteScalarSQL(SQL, null);

                if (Coni == null)
                {
                    SQL = " INSERT INTO ZTEMPLATECUESTION (ZID_TEMPLATE, ZID_CUESTION, ZID_ARCHIVO, ZID_FLUJO, ZID_ESTADO, ZID_REGISTRO, ZID_DATO, ZFECHA)";
                    SQL += " VALUES (" + LaTemplate.Text + "," + LaCuestion.Text + "," + LaArchivo.Text + "," + LaFlujo.Text + "," + LaEstado.Text + "," + this.Session["idregistro"].ToString() + "," + Esta + ",'" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "')";
                    DBHelper.ExecuteNonQuery(SQL);
                }
                else
                {
                    SQL = " UPDATE ZTEMPLATECUESTION ";
                    SQL += " SET ZID_TEMPLATE = " + LaTemplate.Text;
                    SQL += " , ZID_CUESTION = " + LaCuestion.Text;
                    SQL += " , ZID_ARCHIVO = " + LaArchivo.Text;
                    SQL += " , ZID_FLUJO = " + LaFlujo.Text;
                    SQL += " , ZID_ESTADO = " + LaEstado.Text;
                    SQL += " , ZID_REGISTRO = " + this.Session["idregistro"].ToString();
                    SQL += " , ZID_DATO = " + Esta;
                    SQL += " , ZFECHA = '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "'";
                    SQL += " WHERE ZID = " + Coni.ToString();
                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            SQL = " UPDATE " + this.Session["TablaName"].ToString() + " SET ZKEY = 1 ";
            SQL += " WHERE ZID = " + this.Session["idregistro"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            SQL = "UPDATE ZPROCESOS SET ZEJECUCION = 1, ZPARAMETROS = '" + this.Session["ComputerName"].ToString() + "' WHERE ZID_FLUJO =" + this.Session["idflujo"].ToString() + " AND ZID_ESTADO =" + this.Session["idestado"].ToString() + " AND ZID_ARCHIVO =" + this.Session["idarchivo"].ToString();
            DBHelper.ExecuteNonQuery(SQL);

            string Espera = Main.TrazaProceso(this.Session["ComputerName"].ToString(), null);

            //Metodo sincrono. Hasta que Procesos Automaticos no termine, no se pasa el testigo
            while (File.Exists(Espera))
            {
            }

            DrFlujoEstado_Changed(null,null);

            DivCampos0.Visible = false;
            UpdateCampos.Update();

        }


        
        protected void BtnOpenFirma_Click(object sender, EventArgs e)
        {
            //Actualiza el puesto para que vea el usuario en la tableta WACOM
            //Update tablaobjetos set
            Boolean Esta = false;
            CheckBox ChkBoxHeader = (CheckBox)gvLista.HeaderRow.FindControl("chkb1");
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItem");
                if (ChkBoxRows.Checked == true)
                {
                    Esta = true;
                }
                else
                {
                    Esta = false;
                    break;
                }
            }

            if(Esta == false)
            {
                //Mensaje que chequee todos
                //Lbmensfirma.Text = "Debe leer todos los Documentos y activar el check en cada uno de ellos para estar de acuerdo. Le avisarán posteriormente para firmar estos mismos Documentos que ya ha leido, desde cualquier equipo que disponga de Tableta digital de firmas.";
                //DivFirma.Visible = true;

                lbCuestion.Text = "Debe leer todos los Documentos y activar el check en cada uno de ellos para estar de acuerdo. Le avisarán posteriormente para firmar estos mismos Documentos que ya ha leido, desde cualquier equipo que disponga de Tableta digital de firmas.";
                //cuestion.Visible = false;
                //Asume.Visible = true;
                //DvPreparado.Visible = true;
                //lbCuestion.Text = "Debe seleccionar un Archivo Documental con Flujo asociado para poder solicitar consultas a GoldenSoft y RecoDAT.";
                DivCuestion.Visible = true;

                return;
            }
            else
            {
                //Ahora busca siguiente Estado de transicion

                int idestado = SiguienteEstado(this.Session["idestado"].ToString(),this.Session["idarchivo"].ToString(),this.Session["idflujo"].ToString(),1);

                if (idestado == 0)
                {
                    //mensaje que no existe el fichero
                    Lbmensaje.Text = " Este Estado asociado al Flujo de Trabajo tiene mal asignado su siguiente Estado de transición.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    DvPreparado.Visible = true;
                    return;
                }
                else
                {
                    string SQL = "UPDATE " + this.Session["TablaName"].ToString() + " SET ZID_ESTADO = " + idestado + ", ";
                    SQL += " ZKEY = 1 ";
                    SQL += " WHERE ZID = " + this.Session["idregistro"].ToString();
                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = " SELECT ZID FROM ZPROCESOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                    SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " ";
                    SQL += " AND ZID_ESTADO = " + idestado + " ";
                    Object Coni = DBHelper.ExecuteScalarSQL(SQL, null);

                    string ElIDProceso = "";

                    if (Coni == null)
                    {
                        ElIDProceso = "0";
                    }
                    else
                    {
                        ElIDProceso = Coni.ToString();
                    }

                    SQL = "INSERT INTO ZDISPOSITIVOSARCHIVOS ( ZID_REGISTRO, ZID_ARCHIVO, ZID_DISPOSITIVO, ZFECHA, ZID_PROCESO, ZID_FLUJO)";
                    SQL += " VALUES ( " + this.Session["idregistro"].ToString() + ',' + this.Session["idarchivo"].ToString() + ',' + DrDispositivos.SelectedItem.Value + ",'";

                    SQL += DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "'," + ElIDProceso + "," + this.Session["idflujo"].ToString() +")";
                    DBHelper.ExecuteNonQuery(SQL);

                    DivCampos0.Visible = false;
                    DrFlujoEstado_Changed(sender, e);

                    if(System.IO.Directory.Exists(Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString())) == true)
                    {
                        System.IO.Directory.Delete(Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString()), true);
                    }
                }
            }
        }

        public static int SiguienteEstado(string Estado, string Archivo, string Flujo, int Paso)
        {
            //El paso = Atras 0, Siguiente 1, Alternativo 2, final 3
            int A = 0;
            Object Coni = null;
            string SQL = "";

            if (Paso == 0)
            {
                SQL = " SELECT ZPREVIUS FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + Archivo + " ";
                SQL += " AND ZID_FLUJO = " + Flujo + " AND ZID_ESTADO = " + Estado;
                Coni = DBHelper.ExecuteScalarSQL(SQL, null);
            }
            if (Paso == 1)
            {
                SQL = " SELECT ZNEXT FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + Archivo + " ";
                SQL += " AND ZID_FLUJO = " + Flujo + " AND ZID_ESTADO = " + Estado;
                Coni = DBHelper.ExecuteScalarSQL(SQL, null);
            }
            if (Paso == 2)
            {
                SQL = " SELECT ZALTERNATIVE FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + Archivo + " ";
                SQL += " AND ZID_FLUJO = " + Flujo + " AND ZID_ESTADO = " + Estado;
                Coni = DBHelper.ExecuteScalarSQL(SQL, null);
            }
            if (Paso == 3)
            {
                SQL = " SELECT ZEND FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + Archivo + " ";
                SQL += " AND ZID_FLUJO = " + Flujo + " AND ZID_ESTADO = " + Estado;
                Coni = DBHelper.ExecuteScalarSQL(SQL, null);
            }

            if (Coni == null)
            {
            }
            else
            {
                A = Convert.ToInt32(Coni.ToString()) ;
            }
            return A;
        }

        protected void checkYa_Click(object sender, EventArgs e)
        {
            DivFirma.Visible = false;
        }
        protected void DrDispositivo_Changed(object sender, EventArgs e)
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
        protected void BtiniciaFlujo_Click(object sender, EventArgs e)
        {
        }
        

        protected void DrFlujoEstado_Changed(object sender, EventArgs e)
        {
            try
            {
                //ImageEjecucion.Visible = false;
                //ImagenEstado.Visible = true;
                //ImgInicia.Visible = false;
                LbAutomatico.Visible = false;
                string firma = "";
                string SQL = "";
                DataTable dtCampos = Main.CargaCampos().Tables[0];
                this.Session["Campos"] = dtCampos;
                Object Eje = null;
                Object Coni = null;
                BtOpenFirma.Visible = false;
                BtModeFirma.Visible = false;

                if (DrFlujoEstado.SelectedItem.Value == null && 
                    (this.Session["idestado"].ToString() == null || this.Session["idestado"].ToString() == ""))
                {
                    SQL = " SELECT ZID_ESTADO FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                    SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZORDEN = 1 ";
                    Coni = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Coni == null)
                    {
                        this.Session["idestado"] = "0";
                    }
                    else
                    {
                        this.Session["idestado"] = Coni.ToString();
                        
                        //SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                        //SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZORDEN = 1 ";
                        //Eje = DBHelper.ExecuteScalarSQL(SQL, null);
                        //if (Eje.ToString() == "1")
                        //{
                        //    if (gvEmpleado.Rows.Count > 0)
                        //    {
                        //        ImgInicia.Visible = true;
                        //        LbAutomatico.Visible = true;
                        //    }
                        //}
                    }
                }
                else
                {
                    this.Session["idestado"] = DrFlujoEstado.SelectedItem.Value;
                    LbimgEstado.InnerText = DrFlujoEstado.SelectedItem.Text;

                    //SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                    //SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZORDEN = 1 ";
                    //Eje = DBHelper.ExecuteScalarSQL(SQL, null);
                    //if (Eje.ToString() == "1")
                    //{
                    //    if (gvEmpleado.Rows.Count > 0)
                    //    {
                    //        ImgInicia.Visible = true;
                    //        LbAutomatico.Visible = true;
                    //    }
                    //}
                }

                SQL = " SELECT ZCONDICION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"];
                Coni = DBHelper.ExecuteScalarSQL(SQL, null);
                if (Coni == null)
                {
                    this.Session["CondicionEstado"] = "";
                }
                else
                {
                    this.Session["CondicionEstado"] = Coni.ToString();
                }

                //Contempla estado de firma
                SQL = "SELECT ZFIRMA ";
                SQL += " FROM ZESTADOSFLUJO WHERE ZID = " + this.Session["idestado"].ToString();

                Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                string a = Main.ETrazas("", "1", " DrFlujoEstado_Changed ID :" + Con + " --> SQL:" + SQL);
                if (Con == null)
                {
                    firma = "0";
                    BtOpenFirma.Visible = false;
                    BtModeFirma.Visible = false;
                }
                else
                {
                    firma = Con.ToString();
                    if (firma == "0")
                    {
                        BtOpenFirma.Visible = false;
                        BtModeFirma.Visible = false;
                    }
                    else if (firma == "1")
                    {
                        BtOpenFirma.Visible = true;
                        BtModeFirma.Visible = false;
                    }
                    else if (firma == "2")
                    {
                        BtOpenFirma.Visible = false;
                        BtModeFirma.Visible = true;
                    }
                }


                SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
                SQL += " FROM ZARCHIVOS ";
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    SQL += " WHERE  ZID = " + this.Session["idarchivo"].ToString();
                }
                else
                {
                    SQL += " WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
                }
                DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

                dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
                CreaGridControl(dtArchivos, dtCampos);
                string ColumnaSel = DrListaColumna.SelectedItem.Text;
                this.Session["GridOrden"] = ColumnaSel;
                Carga_tablaControl(DrListaColumna.SelectedValue, dtArchivos, dtCampos, this.Session["idestado"].ToString(), ColumnaSel, this.Session["idflujo"].ToString(), "0");
                //Carga_tablaControl("",dtArchivos, dtCampos, this.Session["idestado"].ToString(), null, this.Session["idflujo"].ToString(),"0");

                SQL = " SELECT ZEJECUCION FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + " ";
                SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"].ToString();
                Eje = DBHelper.ExecuteScalarSQL(SQL, null);

                if (Eje.ToString() == "1")
                {
                    if (gvEmpleado.Rows.Count > 0)
                    {
                        if (this.Session["MiNivel"].ToString() == "0")
                        {
                            //ImgInicia.Visible = false;
                            LbAutomatico.Visible = false;

                        }
                        else
                        {
                            //ImgInicia.Visible = true;
                            LbAutomatico.Visible = true;
                        }
                    }
                    else if (Eje.ToString() == "2")
                    {
                        //ImgInicia.Visible = false;
                        LbAutomatico.Visible = false;
                    }
                    else
                    {
                        //ImgInicia.Visible = false;
                        LbAutomatico.Visible = false;
                    }
                }
                UpdatePanelGV.Update();
                UpdatePanelEje.Update();
                UpdateMenu.Update();
                UpdateCampos.Update();
            }
            catch(Exception ex)
            {
                string a = Main.ETrazas("", "1", " DrFlujoEstado_Changed nuevo --> Error:" + ex.Message);
            }
        }

        private void ActualizaEstados()
        {


        }

        protected void DrMeses_Changed(object sender, EventArgs e)
        {
            //Resta meses
            string Miyear = DrAno.SelectedItem.Text;
            this.Session["Mes"] = DrMeses.SelectedIndex.ToString();
            this.Session["Ano"] = Miyear.Substring(2, 2);
            DateTime oPrimerDiaDelMes = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 1);
            TxtDesde.Text = oPrimerDiaDelMes.ToString().Substring(0, 10);
            DateTime dLastDayOfLastMonth = DateTime.Now;
            DateTime dtUltimodia = dLastDayOfLastMonth.AddDays(-1);

            if (Convert.ToInt32(this.Session["Mes"].ToString()) >= 11)
            {
                dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 1, 31);
                dtUltimodia = dLastDayOfLastMonth.AddDays(0);
            }
            else
            {
                dLastDayOfLastMonth = new DateTime(Convert.ToInt32(Miyear), Convert.ToInt32(this.Session["Mes"].ToString()) + 2, 1);
                dtUltimodia = dLastDayOfLastMonth.AddDays(-1);
            }
            TxtHasta.Text = dtUltimodia.ToString().Substring(0, 10);
            ImgFechamas.Visible = true;
        }

        protected void BtAyuda_Click(object sender, EventArgs e)
        {
            if(DivAyuda.Visible == true)
            {
                DivAyuda.Visible = false;
                VistaGrid.Visible = true;
            }
            else
            {
                DivAyuda.Visible = true;
                VistaGrid.Visible = false;
            }
        }
        

        protected void DrAno_Changed(object sender, EventArgs e)
        {
            //Resta meses
        }


        protected void gvEmpleado_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddCabeceraPageSize.SelectedItem.Value == "1000")
            {
                gvEmpleado.AllowPaging = false;
                //Carga_tablaEmpleados();
                DrFlujoEstado_Changed(sender, e);
            }
            else
            {
                gvEmpleado.AllowPaging = true;
                gvEmpleado.PageSize = Convert.ToInt32(ddCabeceraPageSize.SelectedItem.Value);
               //Carga_tablaEmpleados();
                DrFlujoEstado_Changed(sender, e);
            }

        }





        protected void BtGralConsultaMin_Click(object sender, EventArgs e)
        {
            Carga_tablaEmpleados();

        }

    

        protected void DrEmpleado1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tablaEmpleados();
        }

   
        private void CompruebaCampoDecimal(string combo)
        {
            //if(combo == "UDSPEDIDAS" || combo == "UDSSERVIDAS" || combo == "UDSENCARGA" || combo == "UDSPENDIENTES" || combo == "UDSACARGAR" || combo == "NUMPALET")
            //{
            //    if(ElOrdenControl == "")
            //    {
            //        ElOrdenControl = " ORDER BY CONVERT(DECIMAL (10,3), " + combo + ")";
            //    }
            //    else
            //    {
            //        ElOrdenControl += ", CONVERT(DECIMAL (10,3), " + combo + ")";
            //    }
            //}
            //else
            //{
            //    if (ElOrdenControl == "")
            //    {
            //        ElOrdenControl = " ORDER BY " + combo + " ";
            //    }
            //    else
            //    {
            //        if(combo == "NUMERO" || combo == "POSICIONCAMION" || combo == "LINEA" || combo == "NUMERO_LINEA")
            //        {
            //            ElOrdenControl += ", CONVERT(INT, " + combo + ") ";
            //        }
            //        else
            //        {
            //            ElOrdenControl += ", " + combo + " ";
            //        }

            //    }
            //}
        }

        private void CreaGridFilesVacio()
        {
            return;
            //DataTable dtt;
            //dtt = new DataTable("Tabla");

            //dtt.Columns.Add("ZID");
            //dtt.Columns.Add("NOMBRE");
            //dtt.Columns.Add("ZFECHA");
            //dtt.Columns.Add("ZCATEGORIA");
            //dtt.Columns.Add("ZPESO");
            //dtt.Columns.Add("ZUSER");
            //dtt.Columns.Add("ZRUTA");
            //dtt.Columns.Add("ZDESCRIPCION");
            //dtt.Columns.Add("ZFIRMA");
            //dtt.Columns.Add("ZLLAVE");
            
                

            //DataRow drr;

            ////int cou = 6;
            ////for (int i = 0; i <= cou; i++)
            ////{
            //drr = dtt.NewRow();
            //drr[0] = 1;
            //drr[1] = "Sin Nombre Fichero ";
            //drr[2] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            //drr[3] = "Sin Categoría ";
            //drr[4] = "Sin Peso ";
            //drr[5] = this.Session["UserAlias"].ToString();
            //drr[6] = "Sin Ruta";
            //drr[7] = "";
            //drr[8] = "";
            //drr[9] = "";
            //dtt.Rows.Add(drr);
            ////}

            //ImgEstado = "~/Images/lee.png";
            //this.Session["SelTableFiles"] = dtt;
            //gvLista.DataSource = dtt;
            //gvLista.DataBind();
        }

        protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();

                ImageButton editar = e.Row.Cells[0].FindControl("ibtveDoc") as ImageButton;
                editar.ImageUrl = ImgEstado;

                //DataRowView drv = e.Row.DataItem as DataRowView;
                //miro = DataBinder.Eval(e.Row.DataItem, "ZFIRMA").ToString();
                miro = DataBinder.Eval(e.Row.DataItem, "ZID").ToString();
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    //e.Row.Cells[0].Visible = false;
                    //e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    //e.Row.Cells[7].Visible = false;
                    //e.Row.Cells[17].Visible = false;
                }
                //ShowEditButton

                //DataTable dt = this.Session["EstadosFlujo"] as DataTable;

                //foreach (DataRow filas in dt.Rows)
                //{
                //    if (filas["ZPREVIUS"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtAnterior") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZNEXT"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtSiguiente") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZALTERNATIVE"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtRechazado") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZEND"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtFinal") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //}

                //if (miro == "1")
                //{
                //    //Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);

                //    ImageButton editar = e.Row.Cells[0].FindControl("ibtLeeDoc") as ImageButton;
                //    editar.ImageUrl = "~/Images/leer.png";
                //    editar.Visible = false;
                //}
                //else
                //{
                //    ImageButton editar = e.Row.Cells[0].FindControl("ibtFirma") as ImageButton;
                //    editar.Visible = true;
                //}

                //miro = "1";
                ////DataRowView drv = e.Row.DataItem as DataRowView;
                //if (miro == "2")
                //{
                //    e.Row.BackColor = Color.FromName("#eaf5dc");
                //}

                if ((e.Row.DataItemIndex % 2) == 0)
                {
                    //Par
                    e.Row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    e.Row.BackColor = Color.FromName("#f5f5f5");
                }





                //int cellIndex = 0;
                //int countCells = 0;

                //countCells = e.Row.Cells.Count - 1;
                //foreach (TableCell cell in e.Row.Cells)
                //{
                //    TextBox rowButton = new TextBox();
                //    //Button rowButton = new Button();
                //    rowButton.Text = cell.Text;
                //    rowButton.Width = cell.Width;
                //    rowButton.Height = cell.Height;
                //    rowButton.Visible = false;
                //    rowButton.TextMode = TextBoxMode.MultiLine;

                //    //rowButton.Text = "RowButton" + cellIndex;
                //    if (cellIndex != 0)
                //    {
                //        e.Row.Cells[cellIndex].Controls.Add(rowButton);
                //    }

                //    cellIndex++;
                //}

                //GridViewRow row = (GridViewRow)gvJornada.Rows[e.Row];
                //string miro = gvJornada.DataKeys[e.Row].Value.ToString();
                //e.Row.TableSection = TableRowSection.TableBody;
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.TableSection = TableRowSection.TableHeader;
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    //e.Row.Cells[0].Visible = false;
                    //e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    //e.Row.Cells[7].Visible = false;
                    //e.Row.Cells[17].Visible = false;
                }

                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        protected void gvCuestion_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = miro = DataBinder.Eval(e.Row.DataItem, "ZFECHA").ToString();  // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();

                if(miro == "" || miro == null)
                {
                    CheckBox editar = e.Row.Cells[11].FindControl("chbItemSI") as CheckBox;
                    editar.Checked = true;
                }
                else
                {
                    miro = DataBinder.Eval(e.Row.DataItem, "ZID_DATO").ToString();
                    if (miro == "0")
                    {
                        CheckBox editar = e.Row.Cells[11].FindControl("chbItemSI") as CheckBox;
                        editar.Checked = false;
                    }
                    else
                    {
                        CheckBox editar = e.Row.Cells[11].FindControl("chbItemSI") as CheckBox;
                        editar.Checked = true;
                    }
                }



                if ((e.Row.DataItemIndex % 2) == 0)
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.TableSection = TableRowSection.TableHeader;
                //if (this.Session["MiNivel"].ToString() == "0")
                //{
                //    //e.Row.Cells[0].Visible = false;
                //    //e.Row.Cells[1].Visible = false;
                //    e.Row.Cells[2].Visible = false;
                //    e.Row.Cells[4].Visible = false;
                //    e.Row.Cells[5].Visible = false;
                //    e.Row.Cells[6].Visible = false;
                //    //e.Row.Cells[7].Visible = false;
                //    //e.Row.Cells[17].Visible = false;
                //}

                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }
        protected void gvLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "LveDoc")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    //this.Session["idregistro"] = gvLista.DataKeys[index].Value.ToString();
                    this.Session["iddocumento"] = gvLista.DataKeys[index].Value.ToString();

                    DataTable dt = this.Session["SelTableFiles"] as DataTable;

                    foreach (DataRow filas in dt.Rows)
                    {
                        if (filas["ZID"].ToString() == gvLista.DataKeys[index].Value.ToString())
                        {
                            string fileName = System.IO.Path.GetFileName(@filas["ZDIRECTORIO"].ToString());


                            if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == false)
                            {
                                //mensaje que no existe el fichero
                                Lbmensaje.Text = " El fichero no se encuentra en el Servidor Web.";
                                cuestion.Visible = false;
                                Asume.Visible = true;
                                DvPreparado.Visible = true;
                                return;

                            }
                            else if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == true)
                            {
                                string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["idregistro"].ToString(), fileName));
                                string Midirectorio = Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString());

                                //string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["iddocumento"].ToString(), fileName));
                                //string Midirectorio = Server.MapPath("~/Docs/" + this.Session["iddocumento"].ToString());

                                if (Directory.Exists(Midirectorio) == false)
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(Midirectorio);
                                }


                                if (File.Exists(@MiPath) == false)
                                {
                                    File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                                }
                                else
                                {
                                    File.Delete(MiPath);
                                    File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                                }

                                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                                string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
                                string[] Separado = url.Split('/');
                                url = "";
                                if (Separado.Count() > 0)
                                {
                                    for (int i = 0; i < Separado.Count() - 1; i++)
                                    {
                                        if (Separado[i].ToString().Contains("http"))
                                        {
                                            url += Separado[i] + "//";
                                        }
                                        else
                                        {
                                            url += Separado[i] + "/";
                                        }
                                    }

                                    url += "/Docs/" + this.Session["idregistro"].ToString() + "/" + fileName;
                                    //url += "/Docs/" + this.Session["iddocumento"].ToString() + "/" + fileName;
                                }



                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "', '_blank');", true);

                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "', '_blank');", true);
                                
                                string a = Main.Ficherotraza("gvLista rowcommand -->  La ruta del fichero esta en " + url);
                            }
                            break;
                        }
                    }
                    //Reemplazar "pdffile.pdf" con el nombre de su archivo PDF.
                }
                else if (e.CommandName == "ImprimirDoc" )
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvLista rowcommand --> " + ex.Message);
            }
        }

        protected void gvCuestion_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "LveDoc")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    //this.Session["idregistro"] = gvLista.DataKeys[index].Value.ToString();
                    this.Session["iddocumento"] = gvCuestion.DataKeys[index].Value.ToString();

                    DataTable dt = this.Session["SelTableFiles"] as DataTable;

                    foreach (DataRow filas in dt.Rows)
                    {
                        if (filas["ZID"].ToString() == gvCuestion.DataKeys[index].Value.ToString())
                        {
                            string fileName = System.IO.Path.GetFileName(@filas["ZDIRECTORIO"].ToString());


                            if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == false)
                            {
                                //mensaje que no existe el fichero
                                Lbmensaje.Text = " El fichero no se encuentra en el Servidor Web.";
                                cuestion.Visible = false;
                                Asume.Visible = true;
                                DvPreparado.Visible = true;
                                return;

                            }
                            else if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == true)
                            {
                                string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["idregistro"].ToString(), fileName));
                                string Midirectorio = Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString());

                                //string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["iddocumento"].ToString(), fileName));
                                //string Midirectorio = Server.MapPath("~/Docs/" + this.Session["iddocumento"].ToString());

                                if (Directory.Exists(Midirectorio) == false)
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(Midirectorio);
                                }


                                if (File.Exists(@MiPath) == false)
                                {
                                    File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                                }
                                else
                                {
                                    File.Delete(MiPath);
                                    File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                                }

                                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                                string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
                                string[] Separado = url.Split('/');
                                url = "";
                                if (Separado.Count() > 0)
                                {
                                    for (int i = 0; i < Separado.Count() - 1; i++)
                                    {
                                        if (Separado[i].ToString().Contains("http"))
                                        {
                                            url += Separado[i] + "//";
                                        }
                                        else
                                        {
                                            url += Separado[i] + "/";
                                        }
                                    }

                                    url += "/Docs/" + this.Session["idregistro"].ToString() + "/" + fileName;
                                    //url += "/Docs/" + this.Session["iddocumento"].ToString() + "/" + fileName;
                                }



                                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "', '_blank');", true);

                                string a = Main.Ficherotraza("gvLista rowcommand -->  La ruta del fichero esta en " + url);
                            }
                            break;
                        }
                    }
                    //Reemplazar "pdffile.pdf" con el nombre de su archivo PDF.
                }
                else if (e.CommandName == "ImprimirDoc")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvCuestion.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvCuestion.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "LeeCuestion" )
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    string miro = gvCuestion.DataKeys[index].Value.ToString();
                    string SQL = "SELECT ZDESCRIPCION FROM ZCUESTION WHERE ZID = " + miro;
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                    }
                    else
                    {
                        lbCuestion.Text = Con.ToString();
                        //cuestion.Visible = true;
                        DivCuestion.Visible = true;
                        return;

                    }
                }
                
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvLista rowcommand --> " + ex.Message);
            }
        }
        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            return;

            //gvLista.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //int indice = gvLista.EditIndex = e.NewEditIndex;
            //string rID = "";
            ////if (CaCheck.Checked == false)
            ////{
            ////    Carga_tablaJornada();
            ////}
            ////else
            ////{
            ////    Carga_tablaCabeceraClose();
            ////}

            //Label txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            //if (txtBox != null)
            //{
            //    rID = txtBox.Text;
            //}
            //DropDownList combo = gvLista.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
            //if (combo != null)
            //{
            //    for (int i = 0; i < combo.Items.Count; i++)
            //    {
            //        if (combo.Items[i].Text == rID)
            //        {
            //            combo.SelectedValue = combo.Items[i].Value;
            //            break;
            //        }
            //    }
            //}
        }

        protected void gvCuestion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            return;

            //gvLista.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //int indice = gvLista.EditIndex = e.NewEditIndex;
            //string rID = "";
            ////if (CaCheck.Checked == false)
            ////{
            ////    Carga_tablaJornada();
            ////}
            ////else
            ////{
            ////    Carga_tablaCabeceraClose();
            ////}

            //Label txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            //if (txtBox != null)
            //{
            //    rID = txtBox.Text;
            //}
            //DropDownList combo = gvLista.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
            //if (combo != null)
            //{
            //    for (int i = 0; i < combo.Items.Count; i++)
            //    {
            //        if (combo.Items[i].Text == rID)
            //        {
            //            combo.SelectedValue = combo.Items[i].Value;
            //            break;
            //        }
            //    }
            //}
        }
        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

            string rID = ""; // gvEmpleado.DataKeyNames[e.RowIndex].ToString();

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
                TextBox txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rFECHAPREPARA = txtBox.Text;
                }
                //txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
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
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACIONES = txtBox.Text;
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
                SQL += " OBSERVACIONES = '" + rOBSERVACIONES + "', ";
                SQL += " ESTADO = " + rESTADO;
                SQL += " WHERE ID = " + miro;

                this.Session["EstadoCabecera"] = rESTADO;

                Variables.Error = "";
                Lberror.Text = SQL;



                DBHelper.ExecuteNonQuery(SQL);
                //if (CaCheck.Checked == false)
                //{
                //    Carga_tablaJornada();
                //}
                //else
                //{
                //    Carga_tablaCabeceraClose();
                //}
                ImgEstado = "~/Images/lee.png";
                gvLista.EditIndex = -1;

                gvLista.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvCuestion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvCuestion.Rows[e.RowIndex];
            string miro = gvCuestion.DataKeys[e.RowIndex].Value.ToString();

            string rID = ""; // gvEmpleado.DataKeyNames[e.RowIndex].ToString();

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
                TextBox txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rFECHAPREPARA = txtBox.Text;
                }
                //txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
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
                txtBox = (gvCuestion.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACIONES = txtBox.Text;
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
                SQL += " OBSERVACIONES = '" + rOBSERVACIONES + "', ";
                SQL += " ESTADO = " + rESTADO;
                SQL += " WHERE ID = " + miro;

                this.Session["EstadoCabecera"] = rESTADO;

                Variables.Error = "";
                Lberror.Text = SQL;



                DBHelper.ExecuteNonQuery(SQL);
                //if (CaCheck.Checked == false)
                //{
                //    Carga_tablaJornada();
                //}
                //else
                //{
                //    Carga_tablaCabeceraClose();
                //}
                ImgEstado = "~/Images/lee.png";
                gvCuestion.EditIndex = -1;

                gvCuestion.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }
        protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvLista.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvCuestion_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvCuestion.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();
            ImgEstado = "~/Images/lee.png";
            gvLista.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvLista.DataBind();
        }

        protected void gvCuestion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvCuestion.Rows[e.RowIndex];
            string miro = gvCuestion.DataKeys[e.RowIndex].Value.ToString();
            ImgEstado = "~/Images/lee.png";
            gvCuestion.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvCuestion.DataBind();
        }

        protected void gvLista_PageSize_Changed(object sender, EventArgs e)
        {
            //if (ddControlPageSize.SelectedItem.Value == "1000")
            //{
            //    gvControl.AllowPaging = false;
            //    DataTable dt = this.Session["Archivos"] as DataTable;
            //    DataTable dt1 = this.Session["Campos"] as DataTable;
            //    Carga_tablaControl(dt, dt1, null);
            //}
            //else
            //{
            //    gvControl.AllowPaging = true;
            //    gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
            //    DataTable dt = this.Session["Archivos"] as DataTable;
            //    DataTable dt1 = this.Session["Campos"] as DataTable;
            //    Carga_tablaControl(dt, dt1, null);
            //}

        }

        protected void gvCuestion_PageSize_Changed(object sender, EventArgs e)
        {
            //if (ddControlPageSize.SelectedItem.Value == "1000")
            //{
            //    gvControl.AllowPaging = false;
            //    DataTable dt = this.Session["Archivos"] as DataTable;
            //    DataTable dt1 = this.Session["Campos"] as DataTable;
            //    Carga_tablaControl(dt, dt1, null);
            //}
            //else
            //{
            //    gvControl.AllowPaging = true;
            //    gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
            //    DataTable dt = this.Session["Archivos"] as DataTable;
            //    DataTable dt1 = this.Session["Campos"] as DataTable;
            //    Carga_tablaControl(dt, dt1, null);
            //}

        }

        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            //Carga_tablaControlArchivo(dt, dt1, null);
        }

        protected void gvCuestion_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvCuestion.PageIndex = e.NewPageIndex;
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            //Carga_tablaControlArchivo(dt, dt1, null);
        }
        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            //Carga_tablaControl(dt, dt1, e.SortExpression);
            //Carga_tablaControl(null, null, e.SortExpression);
        }

        protected void gvCuestion_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            //Carga_tablaControl(dt, dt1, e.SortExpression);
            //Carga_tablaControl(null, null, e.SortExpression);
        }

        protected void sellectAllA(object sender, EventArgs e)
        {
        }
        protected void sellectAllB(object sender, EventArgs e)
        {
        }
        protected void sellectAllC(object sender, EventArgs e)
        {
        }
        protected void sellectAllD(object sender, EventArgs e)
        {
        }
        protected void sellectAllE(object sender, EventArgs e)
        {
        }
        protected void sellectAllF(object sender, EventArgs e)
        {
        }
        protected void sellectAllG(object sender, EventArgs e)
        {
        }
        protected void sellectAllEmpleado(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)gvEmpleado.HeaderRow.FindControl("chkb1Empleado");
            foreach (GridViewRow row in gvEmpleado.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemEmpleado");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
                if ((row.DataItemIndex % 2) == 0)
                {
                    //Par
                    row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }



        

        protected void sellectAll(object sender, EventArgs e)
        {

            CheckBox ChkBoxHeader = (CheckBox)gvLista.HeaderRow.FindControl("chkb1");
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItem");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
                if ((row.DataItemIndex % 2) == 0)
                {
                    //Par
                    row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }


        protected void sellectSiAll(object sender, EventArgs e)
        {

            CheckBox ChkBoxHeader = (CheckBox)gvCuestion.HeaderRow.FindControl("chkSI");
            //CheckBox ChkBoxHeaderNO = (CheckBox)gvCuestion.HeaderRow.FindControl("chkNO");
            //ChkBoxHeaderNO.Checked = false;

            foreach (GridViewRow row in gvCuestion.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemSI");
                //CheckBox ChkBoxRowsNO = (CheckBox)row.FindControl("chbItemNO");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                    //ChkBoxRowsNO.Checked = false;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                    //ChkBoxRowsNO.Checked = true;
                }
                if ((row.DataItemIndex % 2) == 0)
                {
                    //Par
                    row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }
        protected void sellectSiCK(object sender, EventArgs e)
        {

            CheckBox ChkBoxHeader = (CheckBox)gvCuestion.HeaderRow.FindControl("chkSI");
            //CheckBox ChkBoxHeaderNO = (CheckBox)gvCuestion.HeaderRow.FindControl("chkNO");
            ChkBoxHeader.Checked = false;
            //ChkBoxHeaderNO.Checked = false;

            foreach (GridViewRow row in gvCuestion.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemSI");
                //CheckBox ChkBoxRowsNO = (CheckBox)row.FindControl("chbItemNO");
                if (ChkBoxRows.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                    //ChkBoxRowsNO.Checked = false;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                    //ChkBoxRowsNO.Checked = true;
                }
                if ((row.DataItemIndex % 2) == 0)
                {
                    //Par
                    row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }

        //protected void sellectNoCK(object sender, EventArgs e)
        //{

        //    CheckBox ChkBoxHeader = (CheckBox)gvCuestion.HeaderRow.FindControl("chkNO");
        //    CheckBox ChkBoxHeaderNO = (CheckBox)gvCuestion.HeaderRow.FindControl("chkSI");
        //    ChkBoxHeader.Checked = false;
        //    ChkBoxHeaderNO.Checked = false;

        //    foreach (GridViewRow row in gvCuestion.Rows)
        //    {
        //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemNO");
        //        CheckBox ChkBoxRowsNO = (CheckBox)row.FindControl("chbItemSI");
        //        if (ChkBoxRows.Checked == true)
        //        {
        //            ChkBoxRows.Checked = true;
        //            ChkBoxRowsNO.Checked = false;
        //        }
        //        else
        //        {
        //            ChkBoxRows.Checked = false;
        //            ChkBoxRowsNO.Checked = true;
        //        }
        //        if ((row.DataItemIndex % 2) == 0)
        //        {
        //            //Par
        //            row.BackColor = Color.FromName("#fff");
        //        }
        //        else
        //        {
        //            //Impar
        //            row.BackColor = Color.FromName("#f5f5f5");
        //        }
        //    }
        //}
        protected void sellectNoAll(object sender, EventArgs e)
        {

            CheckBox ChkBoxHeader = (CheckBox)gvCuestion.HeaderRow.FindControl("chkNO");
            CheckBox ChkBoxHeaderNO = (CheckBox)gvCuestion.HeaderRow.FindControl("chkSI");
            ChkBoxHeaderNO.Checked = false;

            foreach (GridViewRow row in gvCuestion.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItemNO");
                CheckBox ChkBoxRowsNO = (CheckBox)row.FindControl("chbItemSI");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                    ChkBoxRowsNO.Checked = false;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                    ChkBoxRowsNO.Checked = true;
                }
                if ((row.DataItemIndex % 2) == 0)
                {
                    //Par
                    row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    row.BackColor = Color.FromName("#f5f5f5");
                }
            }
        }

        private void CreaGridFiles(string Tablaficheros)
        {
            //select tabla objetos
            try
            {
                //Los ficheros que tenga en sus sesión
                string SQL = "SELECT ZID, ZID_DOMAIN, ZID_ARCHIVO, ZDESCRIPCION, ZTITULO, ZRUTA, ZPESO, ZROOT,  ";
                SQL += "ZKEY, ZESTADO, ZFECHA, ZDIRECTORIO, ZCATEGORIA, ZSUBCATEGORIA, ZID_REGISTRO, ";
                
                if(this.Session["NombreCompleto"].ToString() != "")
                {
                    SQL += " '" + this.Session["NombreCompleto"].ToString() + "' AS ZUSER, ";
                }
                else
                {
                    SQL += " ZUSER,";
                }
                SQL += " ZFIRMA, ZLLAVE, ZNIVEL ";

                SQL += " FROM " + Tablaficheros + " ";
                //SQL += " WHERE ZNIVEL < " + this.Session["MiNivel"].ToString() + " ";
                SQL += " WHERE ZID_REGISTRO = " + this.Session["idregistro"].ToString();
                if (this.Session["MiNivel"].ToString() == "0")
                {
                }
                else
                {
                    SQL += " AND ZNIVEL <= " + this.Session["MiNivel"].ToString();
                }
                SQL += " AND ZDIRECTORIO LIKE '%.pdf' ";

                SQL += " ORDER BY ZID ";
                DataTable dt = Main.BuscaLote(SQL).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ImgEstado = "~/Images/lee.png";
                    this.Session["SelTableFiles"] = dt;
                    gvLista.DataSource = dt;
                    gvLista.DataBind();

                    //Busca el dispositivo
                    SQL = "SELECT ZID_DISPOSITIVO FROM ZDISPOSITIVOSARCHIVOS WHERE ZID_REGISTRO = " + this.Session["idregistro"].ToString();
                    SQL += " AND ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                    SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString();

                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);

                    if (Con is null)
                    {
                    }
                    else
                    {
                        DrDispositivos.SelectedItem.Value = Con.ToString();
                    }

                    //PopulateRootLevel();
                }
                else
                {
                    CreaGridFilesVacio();
                }
                H3Nombre.InnerText = "Nombre Completo: " + this.Session["NombreCompleto"].ToString();
                H3Titulo.InnerText = "Documentación adjunta del Registro: " + this.Session["idregistro"].ToString() + ". Resumen: " + dt.Rows.Count + " Ficheros."; ;
            }
            catch (Exception ex)
            {
                Lbmensaje.Text = "Tabla de objetos con errores. " + ex.Message + ".";
                cuestion.Visible = false;
                Asume.Visible = true;
                DvPreparado.Visible = true;
                return;
            }
        }

        protected void DrTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtTransportista.Text = DrTransportista.SelectedItem.Text;
        }

        protected void gvEmpleado_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvEmpleado.PageIndex = e.NewPageIndex;
            DrFlujoEstado_Changed(sender, e);
            //Carga_tablaEmpleados();
        }

        protected void gvJornalHora_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void gvProdImpDia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }
        protected void gvpanelTareas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
        }

        protected void gvEmpleado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridViewRow row = gvEmpleado.Rows[e.RowIndex];
            string miro = gvEmpleado.DataKeys[e.RowIndex].Value.ToString();
            gvEmpleado.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);
            
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

     
        protected void gvEmpleado_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvEmpleado.Rows[e.RowIndex];
            string miro = gvEmpleado.DataKeys[e.RowIndex].Value.ToString();

            string rID = ""; // gvEmpleado.DataKeyNames[e.RowIndex].ToString();

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
                TextBox txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rFECHAPREPARA = txtBox.Text;
                }
                //txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
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
                txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACIONES = txtBox.Text;
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
                SQL += " OBSERVACIONES = '" + rOBSERVACIONES + "', ";
                SQL += " ESTADO = " + rESTADO;
                SQL += " WHERE ID = " + miro;

                this.Session["EstadoCabecera"] = rESTADO;

                Variables.Error = "";
                Lberror.Text = SQL;



                DBHelper.ExecuteNonQuery(SQL);
                //if (CaCheck.Checked == false)
                //{
                //    Carga_tablaJornada();
                //}
                //else
                //{
                //    Carga_tablaCabeceraClose();
                //}

                gvEmpleado.EditIndex = -1;

                gvEmpleado.DataBind();
                Rechazados(gvEmpleado);
                
            }
            catch (Exception ex)
            {
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas("", "1", " gvEmpleado_RowUpdating --> Error:" + ex.Message + " --> " + context.Session["Procedimiento"].ToString());

                //string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvEmpleado.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvEmpleado.EditIndex = e.NewEditIndex;
            string rID = "";
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}

            Label txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            if (txtBox != null)
            {
                rID = txtBox.Text;
            }
            DropDownList combo = gvEmpleado.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
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

            //Si esta cerrado le envio a uno normal
            //Carga_tablaCabecera_close();
            //Carga_tablaJornada();
            //gvProduccion.Rows[indice].Cells[0].Enabled = false;
            //gvProduccion.Rows[indice].Cells[1].Enabled = false;
            //gvProduccion.Rows[indice].Cells[2].Enabled = false;
            //gvProduccion.Rows[indice].Cells[3].Enabled = false;
            //gvProduccion.Rows[indice].Cells[4].Enabled = false;
            //gvProduccion.Rows[indice].Cells[5].Enabled = false;
            //gvProduccion.Rows[indice].Cells[6].Enabled = false;
            //gvProduccion.Rows[indice].Cells[7].Enabled = false;
            //gvProduccion.Rows[indice].Cells[8].Enabled = false;
            //gvProduccion.Rows[indice].Cells[9].Enabled = false;
            //gvProduccion.Rows[indice].Cells[10].Enabled = false;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvProduccion.DataSource = dt;
            //gvProduccion.DataBind();
        }

        protected void gvEmpleado_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            string codigo = "";
            string Centro = "";
            //ImageEjecucion.Visible = false;
            //ImagenEstado.Visible = true;

            try
            {
                index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEmpleado.Rows[index];
                miro = gvEmpleado.DataKeys[index].Value.ToString();

                if (e.CommandName == "Ubicacion")
                {
                    Label txtBox = (gvEmpleado.Rows[index].Cells[8].FindControl("Labcodigo") as Label);
                    if (txtBox != null)
                    {
                        codigo = txtBox.Text;
                    }
                    Label txtBox2 = (gvEmpleado.Rows[index].Cells[8].FindControl("Labcentro") as Label);
                    if (txtBox2 != null)
                    {
                        Centro = txtBox2.Text;
                    }
                    //CreaNomina(codigo, this.Session["UltimaConsulta"].ToString(), Centro);
                }
                if (e.CommandName == "BajaOrden")
                {
                    this.Session["idregistro"]  = gvEmpleado.DataKeys[index].Value.ToString();

                    CreaGridFiles(this.Session["TablaObj"].ToString());
                    //PopulateRootLevel();
                    DivCampos0.Visible = true;
                }
                if (e.CommandName == "LeeDoc")
                {
                    this.Session["idregistro"] = gvEmpleado.DataKeys[index].Value.ToString();

                    string SQL = " SELECT TOP 1 NOMBRECOMPLETO FROM " + this.Session["TablaName"].ToString() + " WHERE ZID = " + this.Session["idregistro"].ToString() + " ";
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                        this.Session["NombreCompleto"] = "";
                    }
                    else
                    {
                        this.Session["NombreCompleto"] = Con.ToString();
                    }

                    if (this.Session["CondicionEstado"].ToString() == null || this.Session["CondicionEstado"].ToString() == "" ||
                        this.Session["CondicionEstado"].ToString() == "0")
                    {
                        CreaGridFiles(this.Session["TablaObj"].ToString());
                        DivGridDoc.Visible = true;
                        DivFicha.Visible = false;
                        DivDispositivos.Visible = true;
                        BtdocAdjunto.Visible = false;
                        H3TituFicha.Visible = false;
                        H3Titulo.Visible = true;
                        BtOpenFicha.Visible = false;
                        BtdocAdjunto.Text = "Protección de Datos";
                    }
                    else
                    {
                        CreaGridFiles(this.Session["TablaObj"].ToString());

                        H3Nombre.InnerText = this.Session["NombreCompleto"].ToString();
                        H3Titulo.Visible = false;
                        H3TituFicha.InnerText = "Ley de Protección de Datos";
                        H3TituFicha.Visible = true;

                        DivDispositivos.Visible = false;
                        BtdocAdjunto.Visible = false;
                        BtOpenFicha.Visible = false;

                        miro = this.Session["CondicionEstado"].ToString();

                        if (miro != "")
                        {
                            if (miro.Contains("ZTEMPLATE#") == true)
                            {
                                miro = miro.Replace("ZTEMPLATE#", "");
                                miro = miro.Replace("-", ", ");
                                //SQL = " SELECT A.ZID, A.ZDESCRIPCION, B.ZID, B.ZID_TEMPLATE, B.ZID_CUESTION, C.ZTITULO, C.ZDESCRIPCION ";
                                SQL = " SELECT B.ZID, A.ZDESCRIPCION AS ZTEMPLATE, B.ZID_TEMPLATE, C.ZTITULO, C.ZDESCRIPCION, B.ZID_DATO, ";
                                SQL += " B.ZID_CUESTION, B.ZID_ARCHIVO, B.ZID_FLUJO, B.ZID_ESTADO, B.ZID_REGISTRO, B.ZFECHA ";
                                SQL += " FROM ZTEMPLATE A, ZTEMPLATECUESTION B, ZCUESTION C ";
                                SQL += " WHERE A.ZID IN(" + miro + ") ";//ZCONDICION
                                SQL += " AND B.ZID_CUESTION = C.ZID ";
                                SQL += " AND A.ZID = B.ZID_TEMPLATE ";
                                SQL += " AND B.ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                                SQL += " AND B.ZID_FLUJO = " + this.Session["idflujo"].ToString();
                                SQL += " AND B.ZID_ESTADO = " + this.Session["idestado"].ToString();
                                SQL += " AND B.ZID_REGISTRO = " + this.Session["idregistro"].ToString();

                                Con = DBHelper.ExecuteScalarSQL(SQL, null);
                                if (Con is null)
                                {
                                    SQL = " SELECT B.ZID, A.ZDESCRIPCION AS ZTEMPLATE, B.ZID_TEMPLATE, C.ZTITULO, C.ZDESCRIPCION, B.ZID_DATO, ";
                                    SQL += " B.ZID_CUESTION, B.ZID_ARCHIVO, B.ZID_FLUJO, B.ZID_ESTADO, B.ZID_REGISTRO, B.ZFECHA ";
                                    SQL += " FROM ZTEMPLATE A, ZTEMPLATECUESTION B, ZCUESTION C ";
                                    SQL += " WHERE A.ZID IN(" + miro + ") ";//ZCONDICION
                                    SQL += " AND B.ZID_CUESTION = C.ZID ";
                                    SQL += " AND A.ZID = B.ZID_TEMPLATE ";
                                    SQL += " AND B.ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                                    SQL += " AND B.ZID_FLUJO = " + this.Session["idflujo"].ToString();
                                    SQL += " AND B.ZID_ESTADO = " + this.Session["idestado"].ToString();
                                    SQL += " AND B.ZFECHA is NULL " ;
                                    SQL += " AND B.ZID_REGISTRO = 0 ";
                                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                                    gvCuestion.DataSource = dt;
                                    gvCuestion.DataBind();
                                }
                                else
                                {
                                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                                    gvCuestion.DataSource = dt;
                                    gvCuestion.DataBind();
                                }

                                DivFicha.Visible = true;
                                DivGridDoc.Visible = false;
                                 BtOpenFicha.Visible = true;
                                BtOpenFirma.Visible = false;
                                BtModeFirma.Visible = false;
                                //if(Convert.ToInt32(this.Session["MiNivel"].ToString()) > 5)
                                //{
                                    BtdocAdjunto.Visible = true;
                                //}
                                BtdocAdjunto.Text = "Documentación Adjunta";

                            }
                            else
                            {
                                DivGridDoc.Visible = true;
                                DivFicha.Visible = false;
                                BtOpenFicha.Visible = false;
                                BtOpenFirma.Visible = true;
                                BtModeFirma.Visible = false;
                                BtdocAdjunto.Visible = false;
                                BtdocAdjunto.Text = "Protección de Datos";

                                //if (Convert.ToInt32(this.Session["MiNivel"].ToString()) > 5)
                                //{
                                //    BtdocAdjunto.Visible = true;
                                //}
                            }
                        }
                        
                    }
                    DivCampos0.Visible = true;
                    UpdateCampos.Update();
                }
            }
            catch (Exception ex)
            {
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas("", "1", "gvEmpledo_RowCommand --> Error:" + ex.Message + " --> " + context.Session["Procedimiento"].ToString());

                //string a = Main.Ficherotraza("gvEmpledo Comand --> " + ex.Message);
                return;
            }
        }


        protected void gvEmpleado_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                DataTable dtCampo = this.Session["Campos"] as DataTable;
                int Contador = 0;

                if (dtCampo.Columns.Count > 1)
                {
                    foreach (DataRow filaKey in dtCampo.Rows)
                    {
                        if (filaKey["ZTIPO"].ToString() == "7")
                        {
                            string miro = DataBinder.Eval(e.Row.DataItem, filaKey["ZTITULO"].ToString()).ToString();
                            string ID = e.Row.DataItem.ToString();
                            if (miro.Contains("~") == true)
                            {
                                e.Row.Height = 10;
                                e.Row.Width = 10;

                                TableCell tableCell = e.Row.Cells[Contador]; // Column 3 in the grid have the Image Button 
                                foreach (var control in tableCell.Controls)
                                {
                                    ImageField imfImagen = new ImageField();
                                    var F = typeof(System.Web.UI.WebControls.ImageField);
                                    //if (control.GetType() == typeof(System.Web.UI.WebControls.ImageButton))
                                    if (control.GetType() == typeof(System.Web.UI.WebControls.Image))
                                    {
                                        System.Web.UI.WebControls.Image iButton =  (System.Web.UI.WebControls.Image)control;
                                        iButton.ImageUrl = miro;
                                        iButton.Height = 20;
                                        iButton.Width = 20;
                                        //ImageButton iButton = control as ImageButton;
                                        ////iButton.ImageUrl = "/Logo.jpg";
                                        //iButton.Height = 20;
                                        //iButton.Width = 20;
                                    }
                                }

                            }
                        }
                        Contador += 1;
                    }
                }


                //System.Drawing.Image img = (System.Drawing.Image)e.Row.FindControl("YourImageControlId");
                //    img.Width / = 10;
                //    img.Height * = 20;


                //DataRowView drv = e.Row.DataItem as DataRowView;
                //string miro = DataBinder.Eval(e.Row.DataItem, "ZID_ESTADO").ToString();
                //if (miro == "NOMBRECOMPLETO")
                //{
                //    this.Session["NombreCompleto"] = "NOMBRECOMPLETO";
                //}

                //CommandName = "LeeDoc"

                //ImageButton editar = e.Row.Cells[0].FindControl("ibtLeeDoc") as ImageButton;
                //ImageButton editar = e.Row.Cells[0].FindControl("LeeDoc") as ImageButton;
                //editar.ImageUrl = ImgFlujo;


                //ShowEditButton

                //DataTable dt = this.Session["EstadosFlujo"] as DataTable;

                //foreach (DataRow filas in dt.Rows)
                //{
                //    if (filas["ZPREVIUS"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtAnterior") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZNEXT"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtSiguiente") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZALTERNATIVE"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtRechazado") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //    if (filas["ZEND"].ToString() == miro)
                //    {
                //        ImageButton editar = e.Row.Cells[0].FindControl("ibtFinal") as ImageButton;
                //        editar.ImageUrl = filas["ZDESCRIPCION"].ToString();
                //    }
                //}

                if ((e.Row.DataItemIndex % 2) == 0)
                {
                    //Par
                    e.Row.BackColor = Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    e.Row.BackColor = Color.FromName("#f5f5f5");
                }

                //GridViewRow row = (GridViewRow)gvJornada.Rows[e.Row];
                //string miro = gvJornada.DataKeys[e.Row].Value.ToString();
                //e.Row.TableSection = TableRowSection.TableBody;
            }
            //else if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //e.Row.TableSection = TableRowSection.TableHeader;
            //    e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //e.Row.TableSection = TableRowSection.TableFooter;
            //    e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
        }


        protected void gvEmpleado_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = (GridViewRow)gvEmpleado.Rows[e.RowIndex];

            string miro = gvEmpleado.DataKeys[e.RowIndex].Value.ToString();

            string SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2 ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}

            gvEmpleado.EditIndex = -1;

            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);

        }


        protected void RetrocedeEstado_Click(object sender, EventArgs e)
        {
            int i = 0;
            Boolean Esta = false;
            //Antes retrocedia, ahora elimina
            //ImageEjecucion.Visible = false;
            //ImagenEstado.Visible = true;

            try
            {

                foreach (GridViewRow row in gvEmpleado.Rows)
                {

                    CheckBox check = row.FindControl("chbItemEmpleado") as CheckBox;

                    if (check.Checked)
                    {
                        Esta = true;
                        string code = gvEmpleado.DataKeys[i].Value.ToString();
                        //int indice = i;
                        //string code = gvEmpleado.DataKeys[row.RowIndex].Values[0].ToString();
                        //string a = gvEmpleado.Rows[indice].Cells[3].Text;

                        //Seleccion del estado y su columna alternativa

                        //string SQL = " SELECT ZALTERNATIVEVIEW FROM ZFLUJOSESTADOS WHERE ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                        //SQL += " AND ZID_FLUJO = " + this.Session["idflujo"].ToString() + " AND ZID_ESTADO = " + this.Session["idestado"].ToString();
                        //Object Con = DBHelper.ExecuteScalarSQL(SQL, null);


                        //if (Con is null)
                        //{
                        //}
                        //else
                        //{





                        string Directorio = "";
                        string SQL = " SELECT TOP 1 ZDIRECTORIO FROM " + this.Session["TablaObj"].ToString() + " WHERE ZID_REGISTRO = " + code + " ";
                        Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                        if (Con is null)
                        {
                        }
                        else
                           {
                            Directorio = "";
                            Con = Con.ToString().Replace("\\", "@");
                            string[] Fields = System.Text.RegularExpressions.Regex.Split(Con.ToString(), "@");

                            for (int a = 1; a < Fields.Count() - 1; a++)
                            {
                                Directorio += "\\" + Fields[a];
                            }
                            Directorio = Directorio.Substring(0, Directorio.Length);
                            string Salida = "";
                            string Comprueba = Directorio.Substring(Directorio.Length -1 , 1);
                            if(Comprueba == "\\")
                            {
                                Salida = Directorio.Substring(0, Directorio.Length - 1) + "-BORRADO-" + DateTime.Now.ToString("yyyy-MM-dd hhmmss");
                            }
                            else
                            {
                                Salida = Directorio + "-BORRADO-" + DateTime.Now.ToString("yyyy-MM-dd hhmmss");
                            }


                            if (Directory.Exists(@Directorio) == true)
                            {
                                try
                                {
                                    if (Directory.Exists(@Salida) == false)
                                    {
                                        Directory.Move(@Directorio, @Salida);
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                //Directory.CreateDirectory(@Salida);
                                string a = Main.ETrazas("", "1", "No existe el Directorio " + @Directorio  + " para renombrarlo como " + @Salida + " así que se crea." );
                            }
                        }

                        SQL = " DELETE FROM " + this.Session["TablaName"].ToString() + " WHERE ZID = " + code;
                        DBHelper.ExecuteNonQuery(SQL);
                        SQL = " DELETE FROM " + this.Session["TablaObj"].ToString() + " WHERE ZID_REGISTRO = " + code;
                        DBHelper.ExecuteNonQuery(SQL);
                        SQL = " DELETE FROM ZDISPOSITIVOSARCHIVOS WHERE ZID_REGISTRO = " + code + " AND ZID_FLUJO = " + this.Session["idflujo"].ToString().ToString() + " AND ZID_ARCHIVO = " + this.Session["idarchivo"].ToString();
                        DBHelper.ExecuteNonQuery(SQL);
                        SQL = " DELETE FROM ZTEMPLATECUESTION WHERE ZID_REGISTRO = " + code + " AND ZID_FLUJO =" + this.Session["idflujo"].ToString() + " AND ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() ;
                        DBHelper.ExecuteNonQuery(SQL);


                        //SQL = " UPDATE ZEMPLEADOS SET ZID_ESTADO = " + Con.ToString();
                        //SQL += " WHERE ZID = " + code;
                        //DBHelper.ExecuteNonQuery(SQL);
                        //}
                    }
                    i += 1;
                }
                if (Esta == false)
                {
                    lbCuestion.Text = "Debe seleccionar un registro para poder rechazarlo.";
                    DivCuestion.Visible = true;
                    //Lbmensaje.Text = "Debe seleccionar un registro para poder rechazarlo.";
                    //cuestion.Visible = false;
                    //Asume.Visible = true;
                    //DvPreparado.Visible = true;
                }
                else
                {
                    DrFlujoEstado_Changed(sender, e);
                }
            }
            catch(Exception ex)
            {
                string a = Main.ETrazas("", "1", "RetrocedeEstado_Click-- > Error:" + ex.Message);
            }
        }


        protected void checkOk_Click(object sender, EventArgs e)
        {
            //HtmlGenericControl li = (HtmlGenericControl)Master.FindControl("DvPreparado");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("cuestion");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("Asume");
            //li.Visible = false;

            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }

        protected void Ultima_Click(object sender, EventArgs e)
        {
            UltimaConsulta(null);
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            //string miro = TxtCodigo.Text;
            //DivProgress.Visible = true;
            BtGralConsulta_Click(sender, e);
            //checkOk_Click(sender, e);
            //Response.Redirect("RecoNomina.aspx");
            //Pagina maestra
            //HtmlGenericControl li = (HtmlGenericControl)Master.FindControl("DvPreparado");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("cuestion");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("Asume");
            //li.Visible = false;


            //Lbmensaje.Text = "Ya";
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            //DivProgress.Visible = false;

            UpdatePanel3.Update();
            Update2.Update();

            DataTable dtCampos = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dtCampos;

            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
            CreaGridControl(dtArchivos, dtCampos);
            Carga_tablaControl("",dtArchivos, dtCampos, this.Session["idestado"].ToString(), null, this.Session["idflujo"].ToString(), "0");

            Rechazados(gvEmpleado);

            //DvPreparado.Visible = false;
            //cuestion.Visible = false;
            //Asume.Visible = false;
            //Lberror.Visible = false;
            DrFlujoEstado_Changed(null, null);
            UpdatePanelGV.Update();
            UpdatePanelEje.Update();
            UpdateMenu.Update();

        }

        protected void checkNo_Click(object sender, EventArgs e)
        {
            //HtmlGenericControl li = (HtmlGenericControl)Master.FindControl("DvPreparado");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("cuestion");
            //li.Visible = false;
            //li = (HtmlGenericControl)Master.FindControl("Asume");
            //li.Visible = false;
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }

     
        private void Carga_FiltrosGral(string Campos)
        {
            Campos = Campos.Replace(" ", "");
            string[] Fields = System.Text.RegularExpressions.Regex.Split(Campos.ToString(), ",");
            //string SQL = "";
            string Campo = "";
#pragma warning disable CS0219 // La variable 'Texto' está asignada pero su valor nunca se usa
            string Texto = "";
#pragma warning restore CS0219 // La variable 'Texto' está asignada pero su valor nunca se usa
            //string Filtros = "";
            this.Session["Filtro"] = "";

            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            for (int i = 0; i < Fields.Count(); i++)
            {
                Campo = "";
                Texto = "";

                //if (TxtCodigo.Text != "" && Fields[i] == "A.COD_EMPLEADO")
                //if (Fields[i] == "A.COD_EMPLEADO" || Fields[i] == "COD_EMPLEADO")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtCodigo.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)IContent;
                //    Tx = (TextBox)TxtCodigo;
                //}
                //else if (Fields[i] == "A.NOMBRE" || Fields[i] == "NOMBRE")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtNombre.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)INombre;
                //    Tx = (TextBox)TxtNombre;
                //}

                //else if (Fields[i] == "A.APELLIDOS" || Fields[i] == "APELLIDOS")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtApellidos.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)IApellido;
                //    Tx = (TextBox)TxtApellidos;
                //}
                //else if (Fields[i] == "CENTRO")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtCentro.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)ICentro;
                //    Tx = (TextBox)TxtCentro;
                //}
                //else if (Fields[i] == "CATEGORIA")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtCategoria.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)ICategoria;
                //    Tx = (TextBox)TxtCategoria;
                //}
                //else if (Fields[i] == "VIVIENDA")
                //{
                //    Campo = Fields[i];
                //    Texto = TxtVivienda.Text.Replace(",", "','");
                //    Ia = (HtmlGenericControl)IVivienda;
                //    Tx = (TextBox)TxtVivienda;
                //}
               
                //string Miro = Ia.Attributes["style"].ToString();
                if (Campo != "")
                {
                    if (Ia.Attributes["class"] == "fa fa-hand-o-up fa-2x")
                    {
                    }
                    else
                    {
                        MontaIconoConsulta(Ia, Tx, Campo, this.Session["Filtro"].ToString());
                    }
                }
            }

            string Miro = this.Session["filtrolocal"].ToString();
 
            Miro = this.Session["Filtro"].ToString();

            if (this.Session["Filtro"].ToString() != "")
            {
                DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                //this.Session["Filtro"] = Filtros;
            }
            else
            {
                DrVistaEmpleado.Attributes.Add("style", "background-color:#fff");
            }
        }

        private void CargaVistaEmpleado(string Dato)
        {
            Boolean Esta = false;
            foreach (ListItem item in DrVistaEmpleado.Items)
            {
                if (item.Value == Dato)
                {
                    Esta = true;
                    break;
                }
            }
            if (Esta == false)
            {
                DrVistaEmpleado.Items.Add(Dato);
            }
        }

        private void MontaIconoConsulta(HtmlGenericControl Ia, TextBox Texto, string Campo, string Filtros)
        {
            if (Campo == "Ninguno") { return; }
            if (Ia.Attributes["class"] == "fa fa-circle fa-2x")
            {
                Filtros += " AND " + Campo + " IN ('" + Texto.Text + "')";
                CargaVistaEmpleado("Contiene estos datos: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("Contiene estos datos: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                Filtros += " AND " + Campo + " LIKE ('" + Texto.Text + "')";
                CargaVistaEmpleado("Incluye en su contenido este dato: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("Incluye en su contenido este dato: " + Texto.Text);
                //Filtros += " AND " + Campo + " NOT IN ('" + Texto.Text + "')";
                //DrVistaEmpleado.Items.Add("No contiene estos datos: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-circle-o fa-2x")
            {
                Filtros += " AND " + Campo + " NOT IN ('" + Texto.Text + "')";
                CargaVistaEmpleado("No contiene estos datos: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("No contiene estos datos: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            {
                Filtros += " AND " + Campo + " NOT LIKE ('" + Texto.Text + "')";
                CargaVistaEmpleado("No incluye en su contenido este datos: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("No incluye en su contenido este datos: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-chevron-left fa-2x")
            {
                if (Campo == "A.FECHA_EMPLEADOS" || Campo == "A.FECHA_JORNADA" || Campo == "CENTRO" || Campo == "A.TAREA" || Campo == "A.TABLET" || Campo == "A.RECOTABLET"
                    || Campo == "A.VARIEDAD" || Campo == "A.ZONA" || Campo == "A.COD_EMPLEADO" || Campo == "COD_EMPLEADO" || Campo == "A.NOMBRE" || Campo == "NOMBRE"
                    || Campo == "A.APELLIDOS" || Campo == "APELLIDOS")
                {
                    Filtros += " AND " + Campo + " < ('" + Texto.Text + "')";
                }
                else
                {
                    Filtros += " AND " + Campo + " < (" + Texto.Text + ")";
                }
                CargaVistaEmpleado("Menor que: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("Menor que: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-chevron-right fa-2x")
            {
                if (Campo == "A.FECHA_EMPLEADOS" || Campo == "A.FECHA_JORNADA" || Campo == "CENTRO" || Campo == "A.TAREA" || Campo == "A.TABLET" || Campo == "A.RECOTABLET"
                    || Campo == "A.VARIEDAD" || Campo == "A.ZONA" || Campo == "A.COD_EMPLEADO" || Campo == "COD_EMPLEADO" || Campo == "A.NOMBRE" || Campo == "NOMBRE"
                    || Campo == "A.APELLIDOS" || Campo == "APELLIDOS")
                {
                    Filtros += " AND " + Campo + " > ('" + Texto.Text + "')";
                }
                else
                {
                    Filtros += " AND " + Campo + " > (" + Texto.Text + ")";
                }
                CargaVistaEmpleado("Mayor que: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("Mayor que: " + Texto.Text);
            }
            else if (Ia.Attributes["class"] == "fa fa-arrows-alt fa-2x")
            {
                Filtros += " AND " + Campo + " <> ('" + Texto.Text + "')";
                CargaVistaEmpleado("Distinto de: " + Texto.Text);
                //DrVistaEmpleado.Items.Add("Distinto de: " + Texto.Text);
            }
            this.Session["Filtro"] = Filtros;
        }

        private void Carga_tablaListaFiltros(string Campos)
        {
            Campos = Campos.Replace(" ", "");
            string[] Fields = System.Text.RegularExpressions.Regex.Split(Campos.ToString(), ",");
            //string SQL = "";
#pragma warning disable CS0219 // La variable 'Campo' está asignada pero su valor nunca se usa
            string Campo = "";
#pragma warning restore CS0219 // La variable 'Campo' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'Texto' está asignada pero su valor nunca se usa
            string Texto = "";
#pragma warning restore CS0219 // La variable 'Texto' está asignada pero su valor nunca se usa
            string Filtros = "";
            this.Session["Filtro"] = "";
            //DrVistaEmpleado.Items.Clear();

            //for (int i = 0; i < Fields.Count(); i++)
            //{
            //    if (TxtCodigo.Text != "" && Fields[i] == "COD_EMPLEADO")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtCodigo.Text.Replace(",", "','");

            //        if (IContent.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO IN ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código contiene: " + TxtCodigo.Text);
            //        }
            //        else if (IContent.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código incluye: " + TxtCodigo.Text);
            //        }
            //        else if (IContent.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = NOT IN ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código no contiene: " + TxtCodigo.Text);
            //        }
            //        else if (IContent.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
            //        }


            //        else if (IContent.Attributes["class"] == "fa fa-chevron-left fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
            //        }
            //        else if (IContent.Attributes["class"] == "fa fa-chevron-right fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
            //        }
            //        else if (IContent.Attributes["class"] == "fa fa-arrows-alt fa-2x")
            //        {
            //            Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
            //        }

            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }


            //    if (TxtNombre.Text != "" && Fields[i] == "NOMBRE")
            //    {
            //        if (INombre.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND NOMBRE IN ('" + TxtNombre.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Nombre contiene: " + TxtNombre.Text);
            //        }
            //        else if (INombre.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND NOMBRE = LIKE ('" + TxtNombre.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Nombre incluye: " + TxtNombre.Text);
            //        }
            //        else if (INombre.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND NOMBRE = NOT IN ('" + TxtNombre.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Nombre no contiene: " + TxtNombre.Text);
            //        }
            //        else if (INombre.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND NOMBRE = NOT LIKE ('" + TxtNombre.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Nombre no incluye: " + TxtNombre.Text);
            //        }
            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }

            //    if (TxtApellidos.Text != "" && Fields[i] == "APELLIDOS")
            //    {
            //        if (IApellido.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND APELLIDOS IN ('" + TxtApellidos.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Apellidos contiene: " + TxtApellidos.Text);
            //        }
            //        else if (IApellido.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND APELLIDOS = LIKE ('" + TxtApellidos.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Apellidos incluye: " + TxtApellidos.Text);
            //        }
            //        else if (IApellido.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND APELLIDOS = NOT IN ('" + TxtApellidos.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Apellidos no contiene: " + TxtApellidos.Text);
            //        }
            //        else if (IApellido.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND APELLIDOS = NOT LIKE ('" + TxtApellidos.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Apellidos no incluye: " + TxtApellidos.Text);
            //        }
            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }

            //    if (TxtCentro.Text != "" && Fields[i] == "CENTRO")
            //    {
            //        if (ICentro.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND CENTRO IN ('" + TxtCentro.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Centro contiene: " + TxtCentro.Text);
            //        }
            //        else if (ICentro.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND CENTRO = LIKE ('" + TxtCentro.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Centro incluye: " + TxtCentro.Text);
            //        }
            //        else if (ICentro.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND CENTRO = NOT IN ('" + TxtCentro.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Centro no contiene: " + TxtCentro.Text);
            //        }
            //        else if (ICentro.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND CENTRO = NOT LIKE ('" + TxtCentro.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Centro no incluye: " + TxtCentro.Text);
            //        }
            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }

            //    if (TxtCategoria.Text != "" && Fields[i] == "CATEGORIA")
            //    {
            //        if (ICategoria.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND CATEGORIA IN ('" + TxtCategoria.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Categoria contiene: " + TxtCategoria.Text);
            //        }
            //        else if (ICategoria.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND CATEGORIA = LIKE ('" + TxtCategoria.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Categoria incluye: " + TxtCategoria.Text);
            //        }
            //        else if (ICategoria.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND CATEGORIA = NOT IN ('" + TxtCategoria.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Categoria no contiene: " + TxtCategoria.Text);
            //        }
            //        else if (ICategoria.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND CATEGORIA = NOT LIKE ('" + TxtCategoria.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Categoria no incluye: " + TxtCategoria.Text);
            //        }
            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }

            //    if (TxtVivienda.Text != "" && Fields[i] == "VIVIENDA")
            //    {
            //        if (IVivienda.Attributes["class"] == "fa fa-circle fa-2x")
            //        {
            //            Filtros += " AND VIVIENDA IN ('" + TxtVivienda.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Vivienda contiene: " + TxtVivienda.Text);
            //        }
            //        else if (IVivienda.Attributes["class"] == "fa fa-adjust fa-2x")
            //        {
            //            Filtros += " AND VIVIENDA = LIKE ('" + TxtVivienda.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Vivienda incluye: " + TxtVivienda.Text);
            //        }
            //        else if (IVivienda.Attributes["class"] == "fa fa-circle-o fa-2x")
            //        {
            //            Filtros += " AND VIVIENDA = NOT IN ('" + TxtVivienda.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Vivienda no contiene: " + TxtVivienda.Text);
            //        }
            //        else if (IVivienda.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
            //        {
            //            Filtros += " AND VIVIENDA = NOT LIKE ('" + TxtVivienda.Text.Replace(",", "','") + "')";
            //            DrVistaEmpleado.Items.Add("Vivienda no incluye: " + TxtVivienda.Text);
            //        }
            //        DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    }
            //}

            this.Session["Filtro"] = Filtros;
        }

        private void Carga_tablaEmpleadosSQL()
        {

            string SQL = "Ninguno, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FECHAALTA, COTIZACION, FECHABAJA, VIVIENDA";
            //DataTable dt = null;
            //string Dato = ""; // TxtNumero.Text;
            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtro = this.Session["Filtro"].ToString();
            //OrdenEmpleaado();
            //OrdenDeGrid(DrEmpleado1, DrEmpleado2);

            //SQL = " SELECT ID, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FORMAT(FECHAALTA, 'dd-MM-yyyy') AS FECHAALTA, COTIZACION, ";
            //SQL += " CASE WHEN FORMAT(FECHABAJA, 'dd-MM-yyyy') = '01-01-1900' THEN null ELSE FORMAT(FECHABAJA, 'dd-MM-yyyy') END AS FECHABAJA , VIVIENDA, ";
            //SQL += " ALQVIVIENDA, DIASMES, COSTEVIVIENDA, IMPORTE, NOMINA ";
            //SQL += " FROM REC_EMPLEADO  ";
            //SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
            //SQL += " AND  BUSQUEDA = 1 ";

            SQL = " SELECT ID, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FORMAT(FECHAALTA, 'dd-MM-yyyy') AS FECHAALTA, COTIZACION, ";
            SQL += " CASE WHEN FORMAT(FECHABAJA, 'dd-MM-yyyy') = '01-01-1900' THEN null ELSE FORMAT(FECHABAJA, 'dd-MM-yyyy') END AS FECHABAJA , VIVIENDA, ";
            SQL += " ALQVIVIENDA, DIASMES, COSTEVIVIENDA, (IMPORTE - COSTEVIVIENDA) AS  IMPORTE, NOMINA ";
            SQL += " FROM REC_EMPLEADO  ";
            SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
            SQL += " AND  BUSQUEDA = 1 ";


            if (filtro != "")
            {
                SQL += filtro;
            }
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY COD_EMPLEADO ";
            }

            this.Session["SQL"] = SQL;

        }




        public static DataTable Carga_tablaEmpleadosXLS(string SQL)
        {
            DataTable dt = null;

            try
            {


                dt = Main.BuscaLote(SQL).Tables[0];
            }
            catch (Exception mm)
            {
                HttpContext context = HttpContext.Current;
                string a = Main.ETrazas(SQL, "1", "Carga_TablaEmpleadosXLS --> Error:" + mm.Message + " --> " + context.Session["Procedimiento"].ToString());

                //string a = Main.Ficherotraza("Carga_TablaEmpleadosXLS --> " + mm.Message);

                Variables.Error = mm.Message;
            }
            return dt;
        }

        private void Carga_tablaEmpleados(string sortExpression = null)
        {
            //Tabla Archivos




            string SQL = " COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FECHAALTA, COTIZACION, FECHABAJA, VIVIENDA, ALQVIVIENDA, DIASMES, COSTEVIVIENDA, IMPORTE, NOMINA";
            DataTable dt = null;
            string Dato = ""; // TxtNumero.Text;
            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtro = this.Session["Filtro"].ToString();

            SQL = " UPDATE REC_EMPLEADO SET BUSQUEDA = 0 ";
            DBHelper.ExecuteNonQuery(SQL);

            //OrdenDeGrid(DrEmpleado1, DrEmpleado2);
            //OrdenEmpleaado();

            if (Dato == "") { Dato = "0"; }
            try
            {
                //this.Session["NumeroPalet"] = "0";
                //filtro = filtro.Replace("WHERE", "AND");
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT ID, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FORMAT(FECHAALTA, 'dd-MM-yyyy') AS FECHAALTA, COTIZACION, ";
                    SQL += " CASE WHEN FORMAT(FECHABAJA, 'dd-MM-yyyy') = '01-01-1900' THEN null ELSE FORMAT(FECHABAJA, 'dd-MM-yyyy') END AS FECHABAJA , VIVIENDA, ";
                    SQL += " ALQVIVIENDA, DIASMES, COSTEVIVIENDA, (IMPORTE - COSTEVIVIENDA) AS  IMPORTE, NOMINA ";
                    SQL += " FROM REC_EMPLEADO  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
                    //if (CheckTodo.Checked == true)
                    //{
                    //if (ImgCerrados.Visible == true)
                    //{
                    //    SQL += " AND FORMAT (FECHABAJA_CALCULADA, 'dd/MM/yyyy') = '" + this.Session["UltimaConsulta"].ToString() + "' ";
                    //}
                    //SQL += " AND ID_CABECERA = " + Dato + " ";
                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY COD_EMPLEADO ";
                    }


                    Lberror.Text += " 1- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + "1- Carga_tablaLista " + SQL + Environment.NewLine;


                }
                else
                {
                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT ID, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FORMAT(FECHAALTA, 'dd-MM-yyyy') AS FECHAALTA, COTIZACION, ";
                    SQL += " CASE WHEN FORMAT(FECHABAJA, 'dd-MM-yyyy') = '01-01-1900' THEN null ELSE FORMAT(FECHABAJA, 'dd-MM-yyyy') END AS FECHABAJA , VIVIENDA, ";
                    SQL += " ALQVIVIENDA, DIASMES, COSTEVIVIENDA, IMPORTE, NOMINA ";
                    SQL += " FROM REC_EMPLEADO  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
                    //SQL += " AND ID_CABECERA = " + Dato + " ";
                    //if (CheckTodo.Checked == true)
                    //{
                    //if (ImgCerrados.Visible == true)
                    //{
                    //    SQL += " AND FORMAT (FECHABAJA_CALCULADA, 'dd/MM/yyyy') = '" + this.Session["UltimaConsulta"].ToString() + "' ";
                    //}

                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY COD_EMPLEADO ";
                    }

                    Lberror.Text += " 2- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_tablaLista " + SQL + Environment.NewLine;


                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ORDER BY POSICIONCAMION ";
                    //dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

                foreach (DataRow filas in dt.Rows)
                {
                    SQL = " UPDATE REC_EMPLEADO SET BUSQUEDA = 1 WHERE COD_EMPLEADO = '" + filas["COD_EMPLEADO"].ToString() + "'";
                    DBHelper.ExecuteNonQuery(SQL);
                }

                lbRowEmpleado.Text = "Registros: " + dt.Rows.Count;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvEmpleado.DataSource = dv;
                }
                else
                {
                    gvEmpleado.DataSource = dt;
                }
                gvEmpleado.DataBind();
                Rechazados(gvEmpleado);
                //gvEmpleado.DataSource = dt;
                //gvEmpleado.DataBind();

                //this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
                //CreaPalets(dt);
                gvEmpleado.EditIndex = -1;

                //Busca Error
                Lberror.Text = "";

            }
            catch (Exception mm)
            {
                string a = Main.ETrazas(SQL, "1", " Carga_TablaEmpleados --> Error:" + mm.Message + " --> " + this.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }

        private void Carga_Tabla_Empleados()
        {
            //Busca en la 80 empleados para firmar
            string SQL = " SELECT * FROM  ZEMPLEADOS ORDER BY ZID";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];

            gvEmpleado.DataSource = dt1;
            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);

        }


        private void Carga_tablaArchivos()
        {
            //Tabla Archivos
            try
            {
                DataTable dt = null;
                string SQL = "";
                //string SQL = "SELECT ZID ";
                //SQL += " FROM ZPAGINA WHERE ZDESCRIPCION = '" + this.Session["Pagina"] + "' ";

                //Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                //if (Con is null)
                //{
                //}
                //else
                //{
                //    this.Session["Pagina"] = Con.ToString();
                //}


                DrVistaEmpleado.Items.Clear();
                DrVistaEmpleado.DataValueField = "ZID";
                DrVistaEmpleado.DataTextField = "ZDESCRIPCION";
                //DataTable dt = new DataTable();

                SQL = " SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                SQL += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME,  ";
                SQL += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                SQL += " FROM ZARCHIVOS A, ZARCHIVOFLUJOS B , ZFLUJOS C ";
                SQL += " WHERE A.ZID = B.ZID_ARCHIVO ";
                SQL += " AND B.ZID_FLUJO = C.ZID ";
                //SQL += " AND C.ZID_PAGINA = " + this.Session["Pagina"];
                SQL += " ORDER BY A.ZTABLENAME ";

                //if (this.Session["MiNivel"].ToString() == "0")
                //{
                //    dt = Main.BuscaLote(SQL).Tables[0];
                //    DrVistaEmpleado.DataSource = dt;
                //    DrVistaEmpleado.DataBind();
                //    DrVistaEmpleado.SelectedIndex = 0;
                //}
                //else
                //{
                    DrVistaEmpleado.Items.Insert(0, new ListItem("Ninguno", "0"));
                    dt = Main.BuscaLote(SQL).Tables[0];
                    DrVistaEmpleado.DataSource = dt;
                    DrVistaEmpleado.DataBind();
                //}

                //DataTable dt = Main.BuscaLote(SQL).Tables[0];
                foreach (DataRow fila in dt.Rows)
                {
                    this.Session["TablaObj"] = fila["ZTABLEOBJ"].ToString();
                    this.Session["TablaName"] = fila["ZTABLENAME"].ToString();
                    //LbArchivoDoc.InnerText = fila["ZTABLENAME"].ToString();
                    Lbusuario.Text = fila["ZDESCRIPCION"].ToString();
                    break;
                }


                //if (this.Session["MiNivel"].ToString() == "0")
                //{
                //    dt = Main.CargaArchivos(4).Tables[0];
                //    foreach (DataRow fila in dt.Rows)
                //    {
                //        this.Session["TablaObj"] = fila["ZTABLEOBJ"].ToString();
                //        this.Session["TablaName"] = fila["ZTABLENAME"].ToString();
                //        LbArchivoDoc.InnerText = fila["ZTABLENAME"].ToString();
                //        Lbusuario.Text = fila["ZDESCRIPCION"].ToString();
                //        break;
                //    }
                //}
                //else
                //{
                //    dt = Main.CargaArchivos(3).Tables[0];
                //}      


                this.Session["Archivos"] = dt;
            }
            catch (Exception mm)
            {
                string a = Main.ETrazas("", "1", " Carga_tablaArchivos --> Error:" + mm.Message + " --> " + this.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }


        //protected void PrintNomina_Click(string ID)
        //{
        //    Carga_tablaEmpleados(e.SortExpression);
        //}


        protected void gvEmpleado_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_tablaEmpleados(e.SortExpression);
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


        private void EliminaQRs()
        {
        }

        private void Update_listaHasta(string Dato, string ID, string Numero_linea)
        {
            string SQL = " UPDATE ZCARGA_LINEA ";
            SQL += " SET HASTA ='" + Dato + "'";
            SQL += " WHERE ID = " + ID;
            SQL += " AND NUMERO_LINEA = " + Numero_linea;

            DBHelper.ExecuteNonQuery(SQL);
        }

        protected void PrintNomina_Click(Object Objeto, Object Container)
        {
            //Carga_tablaEmpleados(e.SortExpression);
        }

        protected void btnPrint_Click(Object Objeto, Object Container)
        {
            //Carga_tablaEmpleados(e.SortExpression);
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

            string code = Etiqueta.Text + Environment.NewLine;


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            try
            {
                imgBarCode.Height = 250;
                imgBarCode.Width = 250;
            }
            catch (Exception a)
            {
                string b = a.Message;
                imgBarCode.Height = 250;
                imgBarCode.Width = 250;
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
                code = etique + Environment.NewLine;
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
                string b = a.Message;
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

   
    


        protected void gvEmpleado_Selecciona(string Index)
        {
            string miro = Index;
            this.Session["IDCabecera"] = miro;
            gvEmpleado.EditIndex = -1;

            if (this.Session["EstadoCabecera"].ToString() == "3")
            {
                //Carga_tablaCabeceraClose();
            }
            else
            {
                //Carga_tablaJornada();
            }


            PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");
            //gvEmpleado.DataBind();

            string SQL = "SELECT NUMERO, EMPRESA, PAIS, FECHAPREPARACION, FECHACARGA, TELEFONO, MATRICULA, TRANSPORTISTA, OBSERVACIONES, ESTADO FROM  ZCARGA_CABECERA WHERE ID = " + miro;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in dt.Rows)
            {

                SeleccionCabecera();
                HtmlButton btn = (HtmlButton)FindControl("BtnuevaCabecera");
                HtmlButton li = (HtmlButton)FindControl("BtCancelCabecera");
                btn.Visible = false;
                li.Visible = true;
                //BtnuevaCabecera.Attributes["class"] = "btn btn-warning  btn-block";
                break;
            }

            //Carga_tablaJornada();
            //Carga_tablaProduccion();
            //Carga_tablaEmpleados();

            SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + miro;
            Lberror.Text += SQL + "1- gvEmpleado_Selecciona " + Variables.mensajeserver;
            dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 1- gvEmpleado_Selecciona " + Variables.mensajeserver;


            Carga_tablaEmpleados();

            gvEmpleado.EditIndex = -1;

            gvEmpleado.DataBind();
            Rechazados(gvEmpleado);
        }
        protected void ListaColumnas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sorting
            this.Session["FiltroConsulta"] = 0;
        }     
       protected void btnAsc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "ASC"; 
            string ColumnaSel = DrListaColumna.SelectedItem.Text;
            this.Session["GridOrden"] = ColumnaSel;
            DataTable dtCampos = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dtCampos;

            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
            CreaGridControl(dtArchivos, dtCampos);

            Carga_tablaControl(DrListaColumna.SelectedValue, dtArchivos, dtCampos, this.Session["idestado"].ToString(), ColumnaSel, this.Session["idflujo"].ToString(), "0");

        }

        protected void btnDesc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "DESC";
            string ColumnaSel = DrListaColumna.SelectedItem.Text;
            this.Session["GridOrden"] = ColumnaSel;
            DataTable dtCampos = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dtCampos;

            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos, Convert.ToInt32(this.Session["FiltroConsulta"].ToString()));
            CreaGridControl(dtArchivos, dtCampos);
            Carga_tablaControl(DrListaColumna.SelectedItem.Text, dtArchivos, dtCampos, this.Session["idestado"].ToString(), ColumnaSel, this.Session["idflujo"].ToString(), "0");
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //DataTable dtCampos = Main.CargaCampos().Tables[0];
            //this.Session["Campos"] = dtCampos;

            //int Cuantos = Convert.ToInt32(this.Session["Registros"].ToString());

            //string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
            //SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            //DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            //dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos);
            //CreaGridControl(dtArchivos, dtCampos);
            //Carga_tablaControl(dtArchivos, dtCampos, this.Session["idestado"].ToString(), null, this.Session["idflujo"].ToString(), "0");

            //if (this.Session["Registros"].ToString() != Cuantos.ToString())
            //{
            //    Timer1.Enabled = false;
            //    ImagenEstado.Visible = true;
            //    ImageEjecucion.Visible = false;
            //    LbimgEstado.Visible = true;
            //    Lbejecutando.Visible = false;
            //}
        }
    }
}