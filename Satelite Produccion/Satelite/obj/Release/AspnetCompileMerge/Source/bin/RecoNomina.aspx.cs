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


namespace QRCode_Demo
{
    public partial class RecoNomina : System.Web.UI.Page
    {
        Reports.RCJornalHora dtsE = null;
        Reports.RCProdEnvDia dtsP = null;
        Reports.RCPrintNominas dtsN = null;

        private int registros = 0;
        private string[] ListadoArchivos;
        private static int IDDiv = 0;
        private static string IDTABLA = "-1";
        private Boolean Cargando = false;

        private string ElID = "";
        private string ElOrden = "";
        //private string ElOrdenEmpleado = "";
        //private string ElOrdenJornada = "";
        //private string ElOrdenTareas = "";
        //private string ElOrdenTareaJornada = "";
        //private string ElOrdenNomina = "";
        //private string ElOrdenResumenNomina = "";
        //private string ElOrdenTrabajos = "";
        //private string ElOrdenProduccion = "";
        //private string ElOrdenJornal_Horas = "";
        //private string ElOrdenJornal_Nominas = "";
        //private string ElOrdenDestajo_Nomina = "";
        //private string ElOrdenProddiaImporte = "";

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

                dtsE = new Reports.RCJornalHora();
                dtsP = new Reports.RCProdEnvDia();
                dtsN = new Reports.RCPrintNominas();

                ReportViewer0.LocalReport.Refresh();

                if (!IsPostBack)
                {
                    DvPreparado.Visible = false;
                    cuestion.Visible = false;
                    Asume.Visible = false;

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
                    this.Session["collapse1"] = "1";
                    this.Session["collapse2"] = "1";
                    this.Session["collapse3"] = "1";
                    this.Session["collapse4"] = "1";
                    this.Session["collapse5"] = "1";
                    this.Session["collapse6"] = "1";
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
                    contadorControles = 0;
                    Lberror.Visible = false;
                    Carga_Menus();
                    //Carga_Filtros();
                    Campos_ordenados();

                    DrVistaEmpleado.Items.Clear();

                    Carga_tablaEmpleados();
                    Carga_panelTareas();
                    Carga_tablaProduccion();
                    Carga_tablaJornada();
                    Carga_Jornal_Horas();
                    Carga_Jornal_Nominas();
                    Carga_Destajo_Nomina();
                    Carga_Nomina_resumen();
                    Carga_ProddiaImporte();
                    Carga_Trabajos();

                    UltimaConsulta();
                    checkListas_Click(null, null);
                    //DateTime dFirstDayOfThisMonth = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
                    //DateTime dLastDayOfLastMonth = dFirstDayOfThisMonth.AddDays(-1);
                    //DateTime dFirstDayOfLastMonth = dFirstDayOfThisMonth.AddMonths(-1);
                    //TxtDesde.Text = dFirstDayOfLastMonth.ToString().Substring(0,10);
                    //TxtHasta.Text = dLastDayOfLastMonth.ToString().Substring(0, 10);


                    //Carga_tablaTrabajos();
                    //Carga_tablaTareas();


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
                string a = Main.Ficherotraza("Page_load --> " + ex.Message);
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
        }

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecoNomina.aspx");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */

        }

        
        protected void BtMarcaError_Click(object sender, EventArgs e)
        {
            if(this.Session["Erroneo"].ToString() == "0")
            {
                this.Session["Erroneo"] = "1";
                BtMarcaError.Text = "Ocultar erróneos";
            }
            else
            {
                this.Session["Erroneo"] = "0";
                BtMarcaError.Text = "Mostrar erróneos";
            }
            Carga_panelTareas();
        }

        protected void BtFilEmpleado_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "1";
            Carga_tablaEmpleados();
        }
        protected void BtfilProduccion_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "2";
            Carga_tablaProduccion();
        }  
        protected void ConsultagvJornada_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "3";
            Carga_tablaJornada();
        }
        protected void BtfilJornalHora_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "4";
            Carga_Jornal_Horas();
        }
        protected void BtfilJornalNomina_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "5";
            Carga_Jornal_Nominas();
        }
        protected void BtfilDestajoNomina_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "6";
            Carga_Destajo_Nomina();
        }
        protected void BtFilProddiaImporte_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "7";
            Carga_ProddiaImporte();
        }
        protected void BtfilResumenNomina_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "8";
            Carga_Nomina_resumen();
        }
        protected void BtfilTrabajos_Click(object sender, EventArgs e)
        {
            this.Session["Delegate"] = "";
            this.Session["filtrolocal"] = "9";
            Carga_Trabajos();
        }

        //protected void btFiltroEmpleado_Click(object sender, EventArgs e)
        //{
        //    if (I1.Attributes["class"] == "fa fa-circle fa-2x")
        //    {
        //        I1.Attributes["title"] = "No deberá contener estos Datos.";
        //        I1.Attributes["class"] = "fa fa-adjust fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-adjust fa-2x")
        //    {
        //        I1.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //        I1.Attributes["class"] = "fa fa-circle-o fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-circle-o fa-2x")
        //    {
        //        I1.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //        I1.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
        //    {
        //        I1.Attributes["title"] = "Deberá contener estos Datos";
        //        I1.Attributes["class"] = "fa fa-chevron-left fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-chevron-left fa-2x")
        //    {
        //        I1.Attributes["title"] = "Deberá contener estos Datos";
        //        I1.Attributes["class"] = "fa fa-chevron-right fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-chevron-right fa-2x")
        //    {
        //        I1.Attributes["title"] = "Deberá contener estos Datos";
        //        I1.Attributes["class"] = "fa fa-arrows-alt fa-2x";
        //    }
        //    else if (I1.Attributes["class"] == "fa fa-arrows-alt fa-2x")
        //    {
        //        I1.Attributes["title"] = "Deberá contener estos Datos";
        //        I1.Attributes["class"] = "fa fa-circle fa-2x";
        //    }
        //}

        protected void BtContiene_Click(Object sender, EventArgs e) //  1
        {
            HtmlButton btn = (HtmlButton)sender;
            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();
            DropDownList Cb = new DropDownList();
            Boolean Esta = false;

            if (btn.ID == "BtCodigo")
            {
                Ia = (HtmlGenericControl)IContent;
                Tx = (TextBox)TxtCodigo;
            }
            if (btn.ID == "BtNombre")
            {
                Ia = (HtmlGenericControl)INombre;
                Tx = (TextBox)TxtNombre;
            }
            if (btn.ID == "BtApellido")
            {
                Ia = (HtmlGenericControl)IApellido;
                Tx = (TextBox)TxtApellidos;              
            }
            if (btn.ID == "BtCentro")
            {
                Ia = (HtmlGenericControl)ICentro;
                Tx = (TextBox)TxtCentro;
            }
            if (btn.ID == "BtCategoria")
            {
                Ia = (HtmlGenericControl)ICategoria;
                Tx = (TextBox)TxtCategoria;
            }
            if (btn.ID == "BtVivienda")
            {
                Ia = (HtmlGenericControl)IVivienda;
                Tx = (TextBox)TxtVivienda;
            }
            if (btn.ID == "BtEnvase")
            {
                Ia = (HtmlGenericControl)IEnvase;
                Tx = (TextBox)TxtEnvase;
            }
            if (btn.ID == "BtVariedad")
            {
                Ia = (HtmlGenericControl)IVariedad;
                Tx = (TextBox)TxtVariedad;
            }
            if (btn.ID == "BtZona")
            {
                Ia = (HtmlGenericControl)IZona;
                Tx = (TextBox)TxtZona;
            }
            if (btn.ID == "BtnFechaIni")
            {
                Ia = (HtmlGenericControl)IFechaIni;
                Tx = (TextBox)TxtBFechaIni;
            }
            if (btn.ID == "BtnFechaFin")
            {
                Ia = (HtmlGenericControl)IFechaFin;
                Tx = (TextBox)TxtBFechaFin;
            }
            if (btn.ID == "BtnTablet")
            {
                Ia = (HtmlGenericControl)ITablet;
                Tx = (TextBox)TxtBTablet;
            }
            //Busqueda particular de cada grid
            
            //if (btn.ID == "BtnFilEmpleado")
            //{
            //    Ia = (HtmlGenericControl)IFilEmpleado;
            //    Tx = (TextBox)TxtConsultaEmpleado;
            //    Cb = (DropDownList)DRgvEmpleado;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnFilProduccion")
            //{
            //    Ia = (HtmlGenericControl)IfilProduccion;
            //    Tx = (TextBox)TxtFilProduccion;
            //    Cb = (DropDownList)DrrowProduccion;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnFilJornada")
            //{
            //    Ia = (HtmlGenericControl)IFilJornada;
            //    Tx = (TextBox)TxtConsultaJornada;
            //    Cb = (DropDownList)DrConsultaJornada;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnFilJornalHora")
            //{
            //    Ia = (HtmlGenericControl)IJornalHora;
            //    Tx = (TextBox)TxtJornalHora;
            //    Cb = (DropDownList)DrrowJornalHora;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnfilJornalNomina")
            //{
            //    Ia = (HtmlGenericControl)IfilJornalNomina;
            //    Tx = (TextBox)TxtFilJornalNomina;
            //    Cb = (DropDownList)DrrowJornalNomina;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnFilProddiaImporte")
            //{
            //    Ia = (HtmlGenericControl)IfilProddiaImporte;
            //    Tx = (TextBox)TxtfilProddiaImporte;
            //    Cb = (DropDownList)DrrowProdDiaImporte;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnFilDestajoNomina")
            //{
            //    Ia = (HtmlGenericControl)IfilDestajoNomina;
            //    Tx = (TextBox)TxtfilDestajoNomina;
            //    Cb = (DropDownList)DrrowDestajoNimina;
            //    Esta = true;
            //}
            //if (btn.ID == "BtnConsResumenNomina")
            //{
            //    Ia = (HtmlGenericControl)IFilResumenNomina;
            //    Tx = (TextBox)TxtFilResumenNomina;
            //    Cb = (DropDownList)DrrowResumenNomina;
            //    Esta = true;
            //}
            //if (btn.ID == "Btnfiltrabajos")
            //{
            //    Ia = (HtmlGenericControl)ITrabajos;
            //    Tx = (TextBox)TxtFilTrabajos;
            //    Cb = (DropDownList)DrFindTrabajo;
            //    Esta = true;
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

            if(Esta == true)
            {
                if (Cb.SelectedItem.Value == "Ninguno")
                {
                    Tx.Attributes.Add("style", "");
                }
            }
        }

        //protected void BtContiene_Click(Object sender, EventArgs e) //  1
        //{
        //    HtmlButton btn = (HtmlButton)sender;
        //    HtmlGenericControl Ia = new HtmlGenericControl();
        //    if (btn.ID == "BtCodigo")
        //    {
        //        Ia = (HtmlGenericControl)IContent;
        //        if (IContent.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IContent.Attributes["title"] = "No deberá contener estos Datos.";
        //            IContent.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IContent.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IContent.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IContent.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IContent.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IContent.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IContent.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IContent.Attributes["title"] = "Deberá contener estos Datos";
        //            IContent.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }

        //    if (Ia.Attributes["class"] == "fa fa-circle fa-2x")
        //    {
        //        Ia.Attributes["title"] = "No deberá contener estos Datos.";
        //        Ia.Attributes["class"] = "fa fa-adjust fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-adjust fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //        Ia.Attributes["class"] = "fa fa-circle-o fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-circle-o fa-2x")
        //    {
        //        Ia.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //        Ia.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Deberá contener estos Datos";
        //        Ia.Attributes["class"] = "fa fa-chevron-left fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-chevron-left fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Deberá contener estos Datos";
        //        Ia.Attributes["class"] = "fa fa-chevron-right fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-chevron-right fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Deberá contener estos Datos";
        //        Ia.Attributes["class"] = "fa fa-random fa-2x";
        //    }
        //    else if (Ia.Attributes["class"] == "fa fa-random fa-2x")
        //    {
        //        Ia.Attributes["title"] = "Deberá contener estos Datos";
        //        Ia.Attributes["class"] = "fa fa-circle fa-2x";
        //    }



        //    if (btn.ID == "BtNombre")
        //    {
        //        if (INombre.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            INombre.Attributes["title"] = "No deberá contener estos Datos.";
        //            INombre.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (INombre.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            INombre.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            INombre.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (INombre.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            INombre.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            INombre.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            INombre.Attributes["title"] = "Deberá contener estos Datos";
        //            INombre.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtApellido")
        //    {
        //        if (IApellido.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IApellido.Attributes["title"] = "No deberá contener estos Datos.";
        //            IApellido.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IApellido.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IApellido.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IApellido.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IApellido.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IApellido.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IApellido.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IApellido.Attributes["title"] = "Deberá contener estos Datos";
        //            IApellido.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtCentro")
        //    {
        //        if (ICentro.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            ICentro.Attributes["title"] = "No deberá contener estos Datos.";
        //            ICentro.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (ICentro.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            ICentro.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            ICentro.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (ICentro.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            ICentro.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            ICentro.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            ICentro.Attributes["title"] = "Deberá contener estos Datos";
        //            ICentro.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtCategoria")
        //    {
        //        if (ICategoria.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            ICategoria.Attributes["title"] = "No deberá contener estos Datos.";
        //            ICategoria.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (ICategoria.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            ICategoria.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            ICategoria.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (ICategoria.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            ICategoria.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            ICategoria.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            ICategoria.Attributes["title"] = "Deberá contener estos Datos";
        //            ICategoria.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtVivienda")
        //    {
        //        if (IVivienda.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IVivienda.Attributes["title"] = "No deberá contener estos Datos.";
        //            IVivienda.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IVivienda.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IVivienda.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IVivienda.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IVivienda.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IVivienda.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IVivienda.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IVivienda.Attributes["title"] = "Deberá contener estos Datos";
        //            IVivienda.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtEnvase")
        //    {
        //        if (IEnvase.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IEnvase.Attributes["title"] = "No deberá contener estos Datos.";
        //            IEnvase.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IEnvase.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IEnvase.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IEnvase.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IEnvase.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IEnvase.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IEnvase.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IEnvase.Attributes["title"] = "Deberá contener estos Datos";
        //            IEnvase.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtVariedad")
        //    {
        //        if (IVariedad.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IVariedad.Attributes["title"] = "No deberá contener estos Datos.";
        //            IVariedad.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IVariedad.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IVariedad.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IVariedad.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IVariedad.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IVariedad.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IVariedad.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IVariedad.Attributes["title"] = "Deberá contener estos Datos";
        //            IVariedad.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }
        //    if (btn.ID == "BtZona")
        //    {
        //        if (IZona.Attributes["class"] == "fa fa-circle fa-2x")
        //        {
        //            IZona.Attributes["title"] = "No deberá contener estos Datos.";
        //            IZona.Attributes["class"] = "fa fa-adjust fa-2x";
        //        }
        //        else if (IZona.Attributes["class"] == "fa fa-adjust fa-2x")
        //        {
        //            IZona.Attributes["title"] = "Deberá tener en el contenido del Dato estos caracteres.";
        //            IZona.Attributes["class"] = "fa fa-circle-o fa-2x";
        //        }
        //        else if (IZona.Attributes["class"] == "fa fa-circle-o fa-2x")
        //        {
        //            IZona.Attributes["title"] = "No deberá tener en el contenido del Dato estos caracteres.";
        //            IZona.Attributes["class"] = "fa fa-dot-circle-o fa-2x";
        //        }
        //        else
        //        {
        //            IZona.Attributes["title"] = "Deberá contener estos Datos";
        //            IZona.Attributes["class"] = "fa fa-circle fa-2x";
        //        }
        //    }

        //}
        protected void lbFilClose_Click(Object sender, EventArgs e)
        {
            LinkButton btn =(LinkButton)sender;
            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            if (btn.ID == "lbFilCodigo")
            {
                Ia = (HtmlGenericControl)IContent;
                Tx = (TextBox)TxtCodigo;
            }
            else if (btn.ID == "LinkNombre")
            {
                Ia = (HtmlGenericControl)INombre;
                Tx = (TextBox)TxtNombre;
            }
            else if (btn.ID == "LinkApellidos")
            {
                Ia = (HtmlGenericControl)IApellido;
                Tx = (TextBox)TxtApellidos;
            }
            else if (btn.ID == "LinkCentro")
            {
                Ia = (HtmlGenericControl)ICentro;
                Tx = (TextBox)TxtCentro;
            }
            else if (btn.ID == "LinkCategoria")
            {
                Ia = (HtmlGenericControl)ICategoria;
                Tx = (TextBox)TxtCategoria;
            }
            else if (btn.ID == "LinkVivienda")
            {
                Ia = (HtmlGenericControl)IVivienda;
                Tx = (TextBox)TxtVivienda;
            }
            else if (btn.ID == "LinkEnvase")
            {
                Ia = (HtmlGenericControl)IEnvase;
                Tx = (TextBox)TxtEnvase;
            }
            else if (btn.ID == "LinkVariedad")
            {
                Ia = (HtmlGenericControl)IVariedad;
                Tx = (TextBox)TxtVariedad;
            }
            else if (btn.ID == "LinkZona")
            {
                Ia = (HtmlGenericControl)IZona;
                Tx = (TextBox)TxtZona;
            }
            else if (btn.ID == "LinkDesde")
            {
                Ia = (HtmlGenericControl)IFechaIni;
                Tx = (TextBox)TxtBFechaIni;
            }
            else if (btn.ID == "LinkHasta")
            {
                Ia = (HtmlGenericControl)IFechaFin;
                Tx = (TextBox)TxtBFechaFin;
            }
            else if (btn.ID == "LinkTablet")
            {
                Ia = (HtmlGenericControl)ITablet;
                Tx = (TextBox)TxtBTablet;
            }
            //else if (btn.ID == "LinkFilEmpleado")
            //{
            //    Ia = (HtmlGenericControl)IFilEmpleado;
            //    Tx = (TextBox)TxtConsultaEmpleado;
            //}
            //else if (btn.ID == "LinkFilProduccion")
            //{
            //    Ia = (HtmlGenericControl)IfilProduccion;
            //    Tx = (TextBox)TxtFilProduccion;
            //}
            //else if (btn.ID == "LinkFilJornada")
            //{
            //    Ia = (HtmlGenericControl)IFilJornada;
            //    Tx = (TextBox)TxtConsultaJornada;
            //}
            //else if (btn.ID == "LinkFilJornalHora")
            //{
            //    Ia = (HtmlGenericControl)IJornalHora;
            //    Tx = (TextBox)TxtJornalHora;
            //}
            //else if (btn.ID == "LinkJornalNomina")
            //{
            //    Ia = (HtmlGenericControl)IfilJornalNomina;
            //    Tx = (TextBox)TxtFilJornalNomina;
            //}
            //else if (btn.ID == "LinkFilDestajoNomina")
            //{
            //    Ia = (HtmlGenericControl)IfilDestajoNomina;
            //    Tx = (TextBox)TxtfilDestajoNomina;
            //}
            //else if (btn.ID == "LinkFilProddiaImporte")
            //{
            //    Ia = (HtmlGenericControl)IfilProddiaImporte;
            //    Tx = (TextBox)TxtfilProddiaImporte;
            //}
            //else if (btn.ID == "LinkConsResumenNomina")
            //{
            //    Ia = (HtmlGenericControl)IFilResumenNomina;
            //    Tx = (TextBox)TxtFilResumenNomina;
            //}
            //else if (btn.ID == "Linkfiltrabajos")
            //{
            //    Ia = (HtmlGenericControl)ITrabajos;
            //    Tx = (TextBox)TxtFilTrabajos;
            //}
            Ia.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            Ia.Attributes["title"] = "Desactivado";
            Ia.Attributes.Add("style", "color:darkred;");
            Tx.Attributes.Add("style", "");
            Tx.Text = "";
        }
            

        protected void BtLimpiaTodo_Click(Object sender, EventArgs e) 
        {
            IContent.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            INombre.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IApellido.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            ICentro.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            ICategoria.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IVivienda.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IEnvase.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IVariedad.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IZona.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IFechaIni.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            IFechaFin.Attributes["class"] = "fa fa-hand-o-up fa-2x";
            ITablet.Attributes["class"] = "fa fa-hand-o-up fa-2x";


            IContent.Attributes["title"] = "Desactivado";
            INombre.Attributes["title"] = "Desactivado";
            IApellido.Attributes["title"] = "Desactivado";
            ICentro.Attributes["title"] = "Desactivado";
            ICategoria.Attributes["title"] = "Desactivado";
            IVivienda.Attributes["title"] = "Desactivado";
            IEnvase.Attributes["title"] = "Desactivado";
            IVariedad.Attributes["title"] = "Desactivado";
            IZona.Attributes["title"] = "Desactivado";
            IFechaIni.Attributes["title"] = "Desactivado";
            IFechaFin.Attributes["title"] = "Desactivado";
            ITablet.Attributes["title"] = "Desactivado";

            IContent.Attributes.Add("style", "color:darkred;");
            INombre.Attributes.Add("style", "color:darkred;");
            IApellido.Attributes.Add("style", "color:darkred;");
            ICentro.Attributes.Add("style", "color:darkred;");
            ICategoria.Attributes.Add("style", "color:darkred;");
            IVivienda.Attributes.Add("style", "color:darkred;");
            IEnvase.Attributes.Add("style", "color:darkred;");
            IVariedad.Attributes.Add("style", "color:darkred;");
            IZona.Attributes.Add("style", "color:darkred;");
            IFechaIni.Attributes.Add("style", "color:darkred;");
            IFechaFin.Attributes.Add("style", "color:darkred;");
            ITablet.Attributes.Add("style", "color:darkred;");

            TxtCodigo.Text = "";
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
            TxtCentro.Text = "";
            TxtCategoria.Text = "";
            TxtVivienda.Text = "";
            TxtEnvase.Text = "";
            TxtVariedad.Text = "";
            TxtZona.Text = "";
            TxtBFechaIni.Text = "";
            TxtBFechaFin.Text = "";

            TxtCodigo.Attributes.Add("style", "background-color:#ffffff;");
            TxtNombre.Attributes.Add("style", "background-color:#ffffff;");
            TxtApellidos.Attributes.Add("style", "background-color:#ffffff;");
            TxtCentro.Attributes.Add("style", "background-color:#ffffff;");
            TxtCategoria.Attributes.Add("style", "background-color:#ffffff;");
            TxtVivienda.Attributes.Add("style", "background-color:#ffffff;");
            TxtEnvase.Attributes.Add("style", "background-color:#ffffff;");
            TxtVariedad.Attributes.Add("style", "background-color:#ffffff;");
            TxtZona.Attributes.Add("style", "background-color:#ffffff;");
            TxtBFechaIni.Attributes.Add("style", "background-color:#ffffff;");
            TxtBFechaFin.Attributes.Add("style", "background-color:#ffffff;");

            DrVistaEmpleado.Items.Clear();
        }

        protected void BtFiltroDefault_Click(Object sender, EventArgs e)
        {
            IContent.Attributes["class"] = "fa fa-circle fa-2x";
            INombre.Attributes["class"] = "fa fa-circle fa-2x";
            IApellido.Attributes["class"] = "fa fa-circle fa-2x";
            ICentro.Attributes["class"] = "fa fa-circle fa-2x";
            ICategoria.Attributes["class"] = "fa fa-circle fa-2x";
            IVivienda.Attributes["class"] = "fa fa-circle fa-2x";
            IEnvase.Attributes["class"] = "fa fa-circle fa-2x";
            IVariedad.Attributes["class"] = "fa fa-circle fa-2x";
            IZona.Attributes["class"] = "fa fa-circle fa-2x";

            IContent.Attributes["title"] = "No deberá contener estos Datos.";
            INombre.Attributes["title"] = "Deberá contener estos Datos";
            IApellido.Attributes["title"] = "Deberá contener estos Datos";
            ICentro.Attributes["title"] = "Deberá contener estos Datos";
            ICategoria.Attributes["title"] = "Deberá contener estos Datos";
            IVivienda.Attributes["title"] = "Deberá contener estos Datos";
            IEnvase.Attributes["title"] = "Deberá contener estos Datos";
            IVariedad.Attributes["title"] = "Deberá contener estos Datos";
            IZona.Attributes["title"] = "Deberá contener estos Datos";

            TxtCodigo.Text = "0020999, 20208, 100008, 100011, 100015, 100029, 100329";
            TxtNombre.Text = "";
            TxtApellidos.Text = "";
            TxtCentro.Text = "";
            TxtCategoria.Text = "";
            TxtVivienda.Text = "";
            TxtEnvase.Text = "Z0";
            TxtVariedad.Text = "T%";
            TxtZona.Text = "S1, S2, S3, S4";
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
            ddCabeceraPageSize.Items.Clear();
            ddCabeceraPageSize.Items.Insert(0, new ListItem("15", "15"));
            ddCabeceraPageSize.Items.Insert(1, new ListItem("30", "30"));
            ddCabeceraPageSize.Items.Insert(2, new ListItem("50", "50"));
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
            DrJornalHora.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DrJornalNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DrDestajoNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DrResumenNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DrTrabajo.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DdgvProdImpDiaPage.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            DrpanelRaw.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());

            //DRgvEmpleado.Items.Clear();
            //DRgvEmpleado.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DRgvEmpleado.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DRgvEmpleado.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DRgvEmpleado.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DRgvEmpleado.Items.Insert(4, new ListItem("Centro", "A.CENTRO"));
            //DRgvEmpleado.Items.Insert(5, new ListItem("Cod. Cotización", "A.COTIZACION"));
            //DRgvEmpleado.Items.Insert(6, new ListItem("Categoría", "A.CATEGORIA"));
            //DRgvEmpleado.Items.Insert(7, new ListItem("Fecha Alta", "A.FECHAALTA"));
            //DRgvEmpleado.Items.Insert(8, new ListItem("Fecha Baja", "A.FECHABAJA"));
            //DRgvEmpleado.Items.Insert(9, new ListItem("Vivienda", "A.VIVIENDA"));



            //DrEmpleado2.Items.Clear();
            //DrEmpleado1.Items.Clear();

            //DrEmpleado2.Items.AddRange(DRgvEmpleado.Items.OfType<ListItem>().ToArray());
            //DrEmpleado1.Items.AddRange(DRgvEmpleado.Items.OfType<ListItem>().ToArray());

            //DrEmpleado1.SelectedIndex = -1;
            //DrEmpleado2.SelectedIndex = -1;
            //DRgvEmpleado.SelectedIndex = -1;

            //DrrowProduccion.Items.Clear();
            //DrrowProduccion.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowProduccion.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowProduccion.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowProduccion.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowProduccion.Items.Insert(4, new ListItem("Fecha", "A.FECHA_EMPLEADOS"));
            //DrrowProduccion.Items.Insert(5, new ListItem("Hora", "A.HORA_EMPLEADO"));
            //DrrowProduccion.Items.Insert(6, new ListItem("Tablet", "A.TABLET"));
            //DrrowProduccion.Items.Insert(7, new ListItem("Código Finca" ,"B.CODFINCA"));
            //DrrowProduccion.Items.Insert(8, new ListItem("Desc. Finca", "B.DESCRFINCA"));
            //DrrowProduccion.Items.Insert(9, new ListItem("Zona", "B.ZONA"));
            //DrrowProduccion.Items.Insert(10, new ListItem("Desc. Zona", "B.DESCRZONAZ"));
            //DrrowProduccion.Items.Insert(11, new ListItem("Tarea", "B.TAREA"));
            //DrrowProduccion.Items.Insert(12, new ListItem("Desc. Tarea", "B.DESCRTAREA"));
            //DrrowProduccion.Items.Insert(13, new ListItem("Envase", "ENVASE"));
            //DrrowProduccion.Items.Insert(14, new ListItem("Desc. Envase", "DESCRENVASE"));
            //DrrowProduccion.Items.Insert(15, new ListItem("Marca Envase", "MARCAENVASE"));
            //DrrowProduccion.Items.Insert(16, new ListItem("Plantas", "PLANTAS"));

            //DrProduccion2.Items.Clear();
            //DrProduccion1.Items.Clear();

            //DrProduccion2.Items.AddRange(DrrowProduccion.Items.OfType<ListItem>().ToArray());
            //DrProduccion1.Items.AddRange(DrrowProduccion.Items.OfType<ListItem>().ToArray());

            //DrProduccion1.SelectedIndex = -1;
            //DrProduccion2.SelectedIndex = -1;
            //DrrowProduccion.SelectedIndex = -1;

            ////repasar
            //DrConsultaJornada.Items.Clear();
            //DrConsultaJornada.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrConsultaJornada.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO" ));
            //DrConsultaJornada.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrConsultaJornada.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrConsultaJornada.Items.Insert(4, new ListItem("Fecha Jornada", "A.FECHA_JORNADA"));
            //DrConsultaJornada.Items.Insert(5, new ListItem("Hora Inicio", "A.HORAINI"));
            //DrConsultaJornada.Items.Insert(6, new ListItem("Hora Final", "A.HORAFIN"));
            //DrConsultaJornada.Items.Insert(7, new ListItem("Tablet", "A.RECOTABLET"));
            //DrConsultaJornada.Items.Insert(8, new ListItem("Tiempo transcurrido", "A.TRANSCURRIDO"));
            //DrConsultaJornada.Items.Insert(9, new ListItem("Importe", "IMPORTE"));

            //DrJornada2.Items.Clear();
            //DrJornada1.Items.Clear();

            //DrJornada2.Items.AddRange(DrConsultaJornada.Items.OfType<ListItem>().ToArray());
            //DrJornada1.Items.AddRange(DrConsultaJornada.Items.OfType<ListItem>().ToArray());

            //DrJornada1.SelectedIndex = -1;
            //DrJornada2.SelectedIndex = -1;
            //DrConsultaJornada.SelectedIndex = -1;

            //DrrowJornalHora.Items.Clear();
            //DrrowJornalHora.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowJornalHora.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowJornalHora.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowJornalHora.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowJornalHora.Items.Insert(4, new ListItem("Fecha Jornada", "A.FECHA_JORNADA"));
            //DrrowJornalHora.Items.Insert(5, new ListItem("Hora Inicio", "A.HORAINI"));
            //DrrowJornalHora.Items.Insert(6, new ListItem("Hora Final", "A.HORAFIN"));
            //DrrowJornalHora.Items.Insert(7, new ListItem("Tiempo producción", "A.TRANSCURRIDO"));
            //DrrowJornalHora.Items.Insert(8, new ListItem("Parcial importe", "A.IMPORTEMINUTOS"));
            //DrrowJornalHora.Items.Insert(9, new ListItem("Total minutos", "A.TOTALTIEMPO"));
            //DrrowJornalHora.Items.Insert(10, new ListItem("Total importe", "A.TOTALIMPORTE"));

            //DRJornalHora2.Items.Clear();
            //DRJornalHora1.Items.Clear();

            //DRJornalHora2.Items.AddRange(DrrowJornalHora.Items.OfType<ListItem>().ToArray());
            //DRJornalHora1.Items.AddRange(DrrowJornalHora.Items.OfType<ListItem>().ToArray());

            //DRJornalHora1.SelectedIndex = -1;
            //DRJornalHora2.SelectedIndex = -1;
            //DrrowJornalHora.SelectedIndex = -1;

            //DrrowResumenNomina.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowResumenNomina.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowResumenNomina.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowResumenNomina.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowResumenNomina.Items.Insert(4, new ListItem("Categoría", "A.CATEGORIA"));
            //DrrowResumenNomina.Items.Insert(5, new ListItem("Plantas", "A.PLANTAS"));
            //DrrowResumenNomina.Items.Insert(6, new ListItem("Importe", "A.IMPORTE"));

            //DRResumenNomina2.Items.Clear();
            //DRResumenNomina1.Items.Clear();

            //DRResumenNomina2.Items.AddRange(DrrowResumenNomina.Items.OfType<ListItem>().ToArray());
            //DRResumenNomina1.Items.AddRange(DrrowResumenNomina.Items.OfType<ListItem>().ToArray());

            //DRResumenNomina1.SelectedIndex = -1;
            //DRResumenNomina2.SelectedIndex = -1;
            //DrrowResumenNomina.SelectedIndex = -1;

            //DrrowJornalNomina.Items.Clear();
            //DrrowJornalNomina.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowJornalNomina.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowJornalNomina.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowJornalNomina.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowJornalNomina.Items.Insert(4, new ListItem("Categoría", "A.CATEGORIA"));
            //DrrowJornalNomina.Items.Insert(5, new ListItem("Tiempo transcurrido", "A.TRANSCURRIDO"));
            //DrrowJornalNomina.Items.Insert(6, new ListItem("Importe Minutos", "A.IMPORTEMINUTOS"));

            //DRJornalNomina2.Items.Clear();
            //DRJornalNomina1.Items.Clear();

            //DRJornalNomina2.Items.AddRange(DrrowJornalNomina.Items.OfType<ListItem>().ToArray());
            //DRJornalNomina1.Items.AddRange(DrrowJornalNomina.Items.OfType<ListItem>().ToArray());

            //DRJornalNomina1.SelectedIndex = -1;
            //DRJornalNomina2.SelectedIndex = -1;
            //DrrowJornalNomina.SelectedIndex = -1;



            //DrrowDestajoNimina.Items.Clear();
            //DrrowDestajoNimina.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowDestajoNimina.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowDestajoNimina.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowDestajoNimina.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowDestajoNimina.Items.Insert(4, new ListItem("Fecha Jornada", "A.FECHA_EMPLEADOS"));
            //DrrowDestajoNimina.Items.Insert(5, new ListItem("Descr. Caja", "DESCRCAJAS"));
            //DrrowDestajoNimina.Items.Insert(6, new ListItem("Cajas", "CAJAS"));
            //DrrowDestajoNimina.Items.Insert(7, new ListItem("Descr. Manojo", "DESCRMANOJOS"));
            //DrrowDestajoNimina.Items.Insert(8, new ListItem("Manojos", "MANOJOS"));
            //DrrowDestajoNimina.Items.Insert(9, new ListItem("Descr. envase", "DESCRENVASE"));
            //DrrowDestajoNimina.Items.Insert(10, new ListItem("Envase", "ENVASE"));
            //DrrowDestajoNimina.Items.Insert(11, new ListItem("Plantas", "PLANTAS"));
            //DrrowDestajoNimina.Items.Insert(12, new ListItem("Importe Minutos", "IMPORTE"));

            ////DrrowDestajoNimina.Items.Clear();
            ////DrrowDestajoNimina.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            ////DrrowDestajoNimina.Items.Insert(1, new ListItem("Código Empleado", "COD_EMPLEADO"));
            ////DrrowDestajoNimina.Items.Insert(2, new ListItem("Nombre", "NOMBRE"));
            ////DrrowDestajoNimina.Items.Insert(3, new ListItem("Apellidos", "APELLIDOS"));
            ////DrrowDestajoNimina.Items.Insert(4, new ListItem("Fecha Jornada", "FECHA_JORNADA"));
            ////DrrowDestajoNimina.Items.Insert(5, new ListItem("Descripción Envase", "DESCRENVASE"));
            ////DrrowDestajoNimina.Items.Insert(6, new ListItem("Envase", "ENVASE"));
            ////DrrowDestajoNimina.Items.Insert(7, new ListItem("Plantas", "PLANTAS"));
            ////DrrowDestajoNimina.Items.Insert(8, new ListItem("Importe Minutos", "IMPORTE"));

            //DRDestajoNomina2.Items.Clear();
            //DRDestajoNomina1.Items.Clear();

            //DRDestajoNomina1.Items.AddRange(DrrowDestajoNimina.Items.OfType<ListItem>().ToArray());
            //DRDestajoNomina2.Items.AddRange(DrrowDestajoNimina.Items.OfType<ListItem>().ToArray());

            //DRDestajoNomina1.SelectedIndex = -1;
            //DRDestajoNomina2.SelectedIndex = -1;
            //DrrowDestajoNimina.SelectedIndex = -1;

            //DrrowProdDiaImporte.Items.Clear();
            //DrrowProdDiaImporte.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrrowProdDiaImporte.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrrowProdDiaImporte.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrrowProdDiaImporte.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrrowProdDiaImporte.Items.Insert(4, new ListItem("Fecha Jornada", "A.FECHA_EMPLEADOS"));
            //DrrowProdDiaImporte.Items.Insert(5, new ListItem("Plantas", "A.PLANTAS"));
            //DrrowProdDiaImporte.Items.Insert(6, new ListItem("Importe", "IMPORTE"));

            //DrProdImpDia2.Items.Clear();
            //DrProdImpDia1.Items.Clear();

            //DrProdImpDia2.Items.AddRange(DrrowProdDiaImporte.Items.OfType<ListItem>().ToArray());
            //DrProdImpDia1.Items.AddRange(DrrowProdDiaImporte.Items.OfType<ListItem>().ToArray());

            //DrProdImpDia1.SelectedIndex = -1;
            //DrProdImpDia2.SelectedIndex = -1;
            //DrrowProdDiaImporte.SelectedIndex = -1;


            //DrFindTrabajo.Items.Clear();
            //DrFindTrabajo.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrFindTrabajo.Items.Insert(1, new ListItem("Código Empleado", "A.COD_EMPLEADO"));
            //DrFindTrabajo.Items.Insert(2, new ListItem("Nombre", "A.NOMBRE"));
            //DrFindTrabajo.Items.Insert(3, new ListItem("Apellidos", "A.APELLIDOS"));
            //DrFindTrabajo.Items.Insert(4, new ListItem("Fecha Jornada", "A.FECHA_JORNADA"));
            //DrFindTrabajo.Items.Insert(5, new ListItem("Hora Inicio", "A.HORAINI"));
            //DrFindTrabajo.Items.Insert(6, new ListItem("Hora Fin", "A.HORAFIN"));
            //DrFindTrabajo.Items.Insert(7, new ListItem("Transcurrido", "A.TRANSCURRIDO"));
            //DrFindTrabajo.Items.Insert(8, new ListItem("RecoTablet", "A.RECOTABLET"));
            //DrFindTrabajo.Items.Insert(9, new ListItem("Tiempo", "TIEMPO"));
            //DrFindTrabajo.Items.Insert(10, new ListItem("Cod. finca", "B.CODFINCA"));
            //DrFindTrabajo.Items.Insert(11, new ListItem("Descr. finca", "B.DESCRFINCA"));
            //DrFindTrabajo.Items.Insert(12, new ListItem("Zona", "B.ZONA"));
            //DrFindTrabajo.Items.Insert(13, new ListItem("Descr. Zona", "B.DESCRZONAZ"));
            //DrFindTrabajo.Items.Insert(14, new ListItem("Tarea", "B.TAREA"));
            //DrFindTrabajo.Items.Insert(15, new ListItem("Descr. Tarea", "DESCRTAREA"));


            //DrTrabajo1.Items.Clear();
            //DrTrabajo0.Items.Clear();

            //DrTrabajo1.Items.AddRange(DrFindTrabajo.Items.OfType<ListItem>().ToArray());
            //DrTrabajo0.Items.AddRange(DrFindTrabajo.Items.OfType<ListItem>().ToArray());

            //DrTrabajo0.SelectedIndex = -1;
            //DrTrabajo1.SelectedIndex = -1;
            //DrFindTrabajo.SelectedIndex = -1;
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
            //if (this.Session["collapse2"].ToString() == "1")
            //{
            //    collapse2.Attributes["class"] = "panel-collapse collapse in";
            //}
            //else
            //{
            //    collapse2.Attributes["class"] = "panel-collapse collapse";
            //}
            //if (this.Session["collapse3"].ToString() == "1")
            //{
            //    collapse3.Attributes["class"] = "panel-collapse collapse in";
            //}
            //else
            //{
            //    collapse3.Attributes["class"] = "panel-collapse collapse";
            //}
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
            //if (this.Session["collapse6"].ToString() == "1")
            //{
            //    collapse6.Attributes["class"] = "panel-collapse collapse in";
            //}
            //else
            //{
            //    collapse6.Attributes["class"] = "panel-collapse collapse";
            //}
            //Carga_tablaProduccion();
            //Carga_tablaEmpleados();

            //gvEmpleado.DataBind();
            //gvProduccion.DataBind();
            //gvJornada.DataBind();
        }

        //private void Carga_los_palet()
        //{
        //    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
        //    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + TxtNumero.Text; // this.Session["IDCabecera"].ToString();

        //    Lberror.Text += SQL + "1- Carga_los_palet " + Variables.mensajeserver;
        //    DataTable dt = Main.BuscaLote(SQL).Tables[0];
        //    Lberror.Text += " 1- Carga_los_palet " + Variables.mensajeserver;
            
        //    this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
        //    CreaPalets(dt);
        //}

        public void Carga_Menus()
        {
            pagevistaform.Attributes["style"] = "";

            if (this.Session["Menu"].ToString() == "0")
            {
                //el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion0.Visible = true;
                accordion.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }

            if (this.Session["Menu"].ToString() == "1")
            {
                //el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = true;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "2")
            {
                //el 3
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion3.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = true;
                accordion.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "3")
            {
                //el 4
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion4.Visible = false;
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = true;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "4")
            {
                //el 2
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = true;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "5")
            {
                //el 5
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = true;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "6")
            {
                //el 6
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = true;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "7")
            {
                //el 7
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = true;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "8")
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = true;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = false;
            }
       
            if (this.Session["Menu"].ToString() == "9")
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnlTrabajos.Visible = false;
                PnInformes.Visible = true;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "10")
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;";
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = true;
                PanelRaw.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "11")
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu11");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu7");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu8");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu9");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu10");
                li.Attributes["class"] = "";
                //pagevistaform.Attributes["style"] = "height: 100%;"; PanelRaw
                HtmlGenericControl panel = (HtmlGenericControl)FindControl("PanelRaw");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion4");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnJornalHora");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnProddiaImporte");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnResumenNomina");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnInformes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("PnlTrabajos");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = false;
                accordion0.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion4.Visible = false;
                PnJornalHora.Visible = false;
                PnJornalNomina.Visible = false;
                PnProddiaImporte.Visible = false;
                PnResumenNomina.Visible = false;
                PnInformes.Visible = false;
                PnlTrabajos.Visible = false;
                PanelRaw.Visible = true;
            }
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
            Carga_tablaProduccion();
            Carga_tablaEmpleados();

        }
        private void Mira_CondicionesGral()
        {
            this.Session["FiltroGral"] = ""; //empleado, nombre, apellido, centro, categoria, vivienda, envase, variedad, zona
            if (IContent.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] = "0-";
            }
            else if (IContent.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] = "1-";
            }
            else
            {
                this.Session["FiltroGral"] = "2-";
            }
            
            if (INombre.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (INombre.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }

            if (IApellido.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (IApellido.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }

            if (ICentro.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (ICentro.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }
            
            if (ICategoria.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (ICategoria.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }

            if (IVivienda.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (IVivienda.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }
            
            if (IEnvase.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (IEnvase.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }
            
            if (IVariedad.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (IVariedad.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }
            
            if (IZona.Attributes["class"] == "fa fa-circle fa-2x")
            {
                this.Session["FiltroGral"] += "0-";
            }
            else if (IZona.Attributes["class"] == "fa fa-adjust fa-2x")
            {
                this.Session["FiltroGral"] += "1-";
            }
            else
            {
                this.Session["FiltroGral"] += "2-";
            }         
        }

        private void UltimaConsulta()
        {
            string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA FROM  ZPROFILES ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dt1.Rows)
            {
                if(fila["ZFECHA"].ToString() != "")
                {
                    string miro = fila["ZFECHA"].ToString();
                    LbUltConsulta.Text = "Última consulta: " + fila["ZFECHAINIRECO"].ToString().Substring(0, 10) + " - " + fila["ZFECHAFINRECO"].ToString().Substring(0, 10);
                    LdDia.Text = " Fecha consulta: " + fila["ZFECHA"].ToString() ;
                }
                else
                {
                    LbUltConsulta.Text = "Última consulta: " + fila["ZFECHAINIRECO"].ToString().Substring(0, 10) + " - " + fila["ZFECHAFINRECO"].ToString().Substring(0, 10) ;
                }

                this.Session["UltimaConsultaFin"] = fila["ZFECHAINIRECO"].ToString().Substring(0, 10);
                this.Session["UltimaConsulta"] = fila["ZFECHAFINRECO"].ToString().Substring(0, 10);
                break;
            }
        }

        protected void BtCuestionGralConsulta_Click(object sender, EventArgs e)
        {
            Lbmensaje.Text = "Se eliminarán los registros actuales " + LbUltConsulta.Text + ", con una nueva consulta ¿Desea continuar?";
            cuestion.Visible = true;
            Asume.Visible = false;
            DvPreparado.Visible = true;
        }

        protected void Lanza80_Click(object sender, EventArgs e)
        {
            BtLimpiaTodo_Click(null, null);
            Lbmensaje.Text = "Eliminando Datos...";
            //Label pmtProc1 = (Label)Progress3.FindControl("LbmensajeET");
            //pmtProc1.Text = "Eliminando Datos...";


            //imgLoad.Visible = true;
            //Carga_tablaListaFiltro();
            //BuscaTablasReco(int Modo, string StrFechas, string StrZonaCultivo, string StrEnvase, string StrVariedad, string StrEmpleados, string StrOrden)
            Variables.Error = "";
            //Lberror.Text = " --> BtGralConsulta_Click " + Environment.NewLine;
            //Consultas
            //REC_TAREAS
            //REC_JORNADA
            //REC_PRODUCCION
            //REC_EMPLEADO
            //
            string Fechas = "";
            Mira_CondicionesGral();

            //elimino  el contenido de las Tablas
            //REC_TRABAJOS composicion tabla REC_TAREAS y REC_JORNADAS
            if (TxtDesde.Text == "" || TxtHasta.Text == "")
            {
                TextAlerta.Text = "Las fechas puestas en rango no son correctas.";
                alerta.Visible = true;
                return;
            }
            Fechas = "'" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "'";

            string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO FROM  ZPROFILES ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            if (dt1.Rows.Count == 0)
            {
                SQL = " INSERT INTO ZPROFILES(ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA) ";
                SQL += " VALUES ('" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "') ";
            }
            else
            {
                SQL = "  UPDATE ZPROFILES SET ZFECHAINIRECO = '" + TxtDesde.Text + "', ";
                SQL += " ZFECHAFINRECO = '" + TxtHasta.Text + "', ";
                SQL += " ZFECHA = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "' ";
                SQL += " WHERE ZID = 1 ";
            }

            DBHelper.ExecuteNonQuery(SQL);


            //BORRA TODOS LAS TABLAS REC_

            //string SQL = " DELETE FROM REC_TRABAJOS ";
            //DBHelper.ExecuteNonQuery(SQL);

            //Lberror.Text += " --> 1- DELETE REC_TRABAJOS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_CALENDARIO ";
            DBHelper.ExecuteNonQuery(SQL);

            SQL = " DELETE FROM REC_TAREAS ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_TAREAS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_JORNADA ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_JORNADA " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_PRODUCCION ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_PRODUCCION " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_EMPLEADO ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1-  DELETE REC_EMPLEADO " + SQL + Variables.Error + Environment.NewLine
            //---------------------------------------------------------------------------------------------------------------------;
            try
            {
                //Tareas
                SQL = Main.BuscaTablasReco(1, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_Jornadas " + SQL + Variables.Error + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_TAREAS ( COD_EMPLEADO, NOMBRE, APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, INIFIN, DESCRINIFIN )";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["INIFIN"].ToString() + "','" + fila["DESCRINIFIN"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_TAREAS " + SQL  + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }

                //Jornadas
                SQL = Main.BuscaTablasReco(2, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_JORNADA ( COD_EMPLEADO,NOMBRE,APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_JORNADA , HORAINI ,HORAFIN ,TRANSCURRIDO , RECOTABLET)";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_JORNADA"].ToString() + "','" + fila["HORAINI"].ToString() + "','" + fila["HORAFIN"].ToString() + "','" + fila["TRANSCURRIDO"].ToString() + "','";
                    SQL += fila["RECOTABLET"].ToString() + "')";

                    //Lberror.Text += " --> 3- REC_JORNADA " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }

                //Produccion
                SQL = Main.BuscaTablasReco(3, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                //Lberror.Text += Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_PRODUCCION ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, ENVASE, DESCRENVASE,  MARCAENVASE, PLANTAS )";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["ENVASE"].ToString() + "','";
                    SQL += fila["DESCRENVASE"].ToString() + "','" + fila["MARCAENVASE"].ToString() + "','" + fila["PLANTAS"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_PRODUCCION (DESTAJOS) " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }

                //EMPLEADOS desde jornadas
                SQL = "SELECT DISTINCT(COD_EMPLEADO) FROM REC_JORNADA ";
                Lberror.Text = " --> 1- Carga_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " SELECT TOP 1 (CodEmpleado) AS COD_EMPLEADO, Nombre AS  NOMBRE, Apellidos AS APELLIDOS, ";
                    SQL += " codCentro AS CENTRO, codCategoria AS CATEGORIA, Domicilio AS VIVIENDA ";
                    SQL += " FROM EMPLEADOS WHERE CodEmpleado = '" + fila["COD_EMPLEADO"].ToString() + "'";
                    DataTable dt2 = Main.BuscaLoteReco(SQL).Tables[0];
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " INSERT INTO REC_EMPLEADO ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                        SQL += " CENTRO, CATEGORIA, VIVIENDA, BUSQUEDA)";
                        SQL += " VALUES('" + fila2["COD_EMPLEADO"].ToString() + "','" + fila2["NOMBRE"].ToString() + "','" + fila2["APELLIDOS"].ToString() + "','";
                        SQL += fila2["CENTRO"].ToString() + "','" + fila2["CATEGORIA"].ToString() + "','" + fila2["VIVIENDA"].ToString() + "',0)";
                        break;
                    }

                    Lberror.Text = " --> 2- REC_EMPLEADO " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    Lberror.Text += Variables.Error + Environment.NewLine;
                }

                //UPDATE REC_EMPLEADO DESDE GOLDENSOFT
                SQL = "SELECT DISTINCT(SUBSTRING(COD_EMPLEADO, 4, LEN(COD_EMPLEADO))) AS TRUNC_EMPLEADO, COD_EMPLEADO, (SUBSTRING(COD_EMPLEADO, 1, 3)) AS CENTRO  FROM REC_EMPLEADO ";
                Lberror.Text = " --> 2- Update_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;

                //Busca la base de datos Variable CENTRO es Nombre BASE Datos Empresa
                string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
                string rBBDDa = ""; // "NET_PRVR" + rANUAL;
                string rBBDDb = ""; // "NET_PRVV" + rANUAL;


                string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA ";
                DataTable dtV = Main.BuscaLote(Data).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila3 in dtV.Rows)
                {
                    rANUAL = fila3["ZANO"].ToString();
                    rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
                    rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
                    break;
                }

                foreach (DataRow fila in dt1.Rows)
                {
                    if (fila["CENTRO"].ToString() == "001")
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDa + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDa + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "') ";
                        //string a = Main.Ficherotraza("CENTRO 001-->" + SQL);
                    }
                    else
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDb + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDb + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador  LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "') ";
                        //string a = Main.Ficherotraza("CENTRO 002-->" + SQL);
                    }

                    DataTable dt2 = Main.BuscaLoteGold(SQL).Tables[0];
                    Lberror.Text = Variables.Error + Environment.NewLine;
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " UPDATE  REC_EMPLEADO SET FECHAALTA ='" + fila2["FECHAINI"].ToString() + "', ";
                        SQL += " FECHABAJA = '" + fila2["FECHAFIN"].ToString() + "', ";
                        SQL += " COTIZACION = " + fila2["Cotizacion"].ToString() + " ";
                        SQL += " WHERE  COD_EMPLEADO = '" + fila["COD_EMPLEADO"].ToString() + "' ";

                        //string a = Main.Ficherotraza("UPDATE ENCONTRADO-->" + SQL);
                        //Lberror.Text = " --> 3- Update REC_EMPLEADO " + SQL + Environment.NewLine;
                        DBHelper.ExecuteNonQuery(SQL);
                        //Lberror.Text += Variables.Error + Environment.NewLine;

                        break;
                    }
                }
            }
            catch (Exception ex) 
            {
                string a = Main.Ficherotraza("Carga_TablaTareas --> " + ex.Message);

                Variables.Error = ex.Message;
                Lberror.Visible = true;
                Lberror.Text = ex.Message;
            }

                //---------------------------------------------------------------------------------------------------------------------;
       }

        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            BtLimpiaTodo_Click(null, null);
            Lbmensaje.Text = "Eliminando Datos...";
            Label pmtProc1 = (Label)Progress3.FindControl("LbmensajeET");
            pmtProc1.Text = "Eliminando Datos...";
            int Donde = 0;

            //imgLoad.Visible = true;
            //Carga_tablaListaFiltro();
            //BuscaTablasReco(int Modo, string StrFechas, string StrZonaCultivo, string StrEnvase, string StrVariedad, string StrEmpleados, string StrOrden)
            Variables.Error = "";
            //Lberror.Text = " --> BtGralConsulta_Click " + Environment.NewLine;
            //Consultas
            //REC_TAREAS
            //REC_JORNADA
            //REC_PRODUCCION
            //REC_EMPLEADO
            //
            string Fechas = "";
            Mira_CondicionesGral();
            Donde = 1;

            //elimino  el contenido de las Tablas
            //REC_TRABAJOS composicion tabla REC_TAREAS y REC_JORNADAS
            if (TxtDesde.Text == "" || TxtHasta.Text == "")
            {
                TextAlerta.Text = "Las fechas puestas en rango no son correctas.";
                alerta.Visible = true;
                return;
            }
            Fechas = "'" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "'";

            string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO FROM  ZPROFILES ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            if (dt1.Rows.Count == 0)
            {
                SQL = " INSERT INTO ZPROFILES(ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA) ";
                SQL += " VALUES ('" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "') ";
            }
            else
            {
                SQL = "  UPDATE ZPROFILES SET ZFECHAINIRECO = '" + TxtDesde.Text + "', ";
                SQL += " ZFECHAFINRECO = '" + TxtHasta.Text + "', ";
                SQL += " ZFECHA = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "' ";
                //Añadir Equipo para que cada uno tenga su consulta personal
                //SQL += " WHERE ZID = 1 ";
            }

            DBHelper.ExecuteNonQuery(SQL);

            UltimaConsulta();

            Donde = 2;

            //BORRA TODOS LAS TABLAS REC_

            //string SQL = " DELETE FROM REC_TRABAJOS ";
            //DBHelper.ExecuteNonQuery(SQL);

            //Lberror.Text += " --> 1- DELETE REC_TRABAJOS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_CALENDARIO ";
            DBHelper.ExecuteNonQuery(SQL);

            SQL = " DELETE FROM REC_TAREAS ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_TAREAS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_JORNADA ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_JORNADA " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_PRODUCCION ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_PRODUCCION " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_EMPLEADO ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1-  DELETE REC_EMPLEADO " + SQL + Variables.Error + Environment.NewLine
            Donde = 3;
            //---------------------------------------------------------------------------------------------------------------------;
            try
            {
                //Tareas
                SQL = Main.BuscaTablasReco(1, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_Jornadas " + SQL + Variables.Error + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_TAREAS ( COD_EMPLEADO, NOMBRE, APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, INIFIN, DESCRINIFIN, HORA_AJUSTADA)";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["INIFIN"].ToString() + "','" + fila["DESCRINIFIN"].ToString() + "','";
                    SQL += fila["HORA_EMPLEADO"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_TAREAS " + SQL  + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 4;
                //Jornadas
                SQL = Main.BuscaTablasReco(2, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_JORNADA ( COD_EMPLEADO,NOMBRE,APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_JORNADA , HORAINI ,HORAFIN ,TRANSCURRIDO , RECOTABLET)";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_JORNADA"].ToString() + "','" + fila["HORAINI"].ToString() + "','" + fila["HORAFIN"].ToString() + "','" + fila["TRANSCURRIDO"].ToString() + "','";
                    SQL += fila["RECOTABLET"].ToString() + "')";

                    //Lberror.Text += " --> 3- REC_JORNADA " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 5;
                //Produccion
                SQL = Main.BuscaTablasReco(3, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                //Lberror.Text += Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_PRODUCCION ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, ENVASE, DESCRENVASE,  MARCAENVASE, PLANTAS )";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["ENVASE"].ToString() + "','";
                    SQL += fila["DESCRENVASE"].ToString() + "','" + fila["MARCAENVASE"].ToString() + "','" + fila["PLANTAS"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_PRODUCCION (DESTAJOS) " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 6;
                //EMPLEADOS desde jornadas
                SQL = "SELECT DISTINCT(COD_EMPLEADO) FROM REC_JORNADA ";
                Lberror.Text = " --> 1- Carga_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " SELECT TOP 1 (CodEmpleado) AS COD_EMPLEADO, Nombre AS  NOMBRE, Apellidos AS APELLIDOS, ";
                    SQL += " codCentro AS CENTRO, codCategoria AS CATEGORIA, Domicilio AS VIVIENDA ";
                    SQL += " FROM EMPLEADOS WHERE CodEmpleado = '" + fila["COD_EMPLEADO"].ToString() + "'";
                    DataTable dt2 = Main.BuscaLoteReco(SQL).Tables[0];
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " INSERT INTO REC_EMPLEADO ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                        SQL += " CENTRO, CATEGORIA, VIVIENDA, BUSQUEDA)";
                        SQL += " VALUES('" + fila2["COD_EMPLEADO"].ToString() + "','" + fila2["NOMBRE"].ToString() + "','" + fila2["APELLIDOS"].ToString() + "','";
                        SQL += fila2["CENTRO"].ToString() + "','" + fila2["CATEGORIA"].ToString() + "','" + fila2["VIVIENDA"].ToString() + "',0)";
                        break;
                    }

                    Lberror.Text = " --> 2- REC_EMPLEADO " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 7;
                //UPDATE REC_EMPLEADO DESDE GOLDENSOFT
                SQL = "SELECT DISTINCT(SUBSTRING(COD_EMPLEADO, 4, LEN(COD_EMPLEADO))) AS TRUNC_EMPLEADO, COD_EMPLEADO, (SUBSTRING(COD_EMPLEADO, 1, 3)) AS CENTRO  FROM REC_EMPLEADO ";
                Lberror.Text = " --> 2- Update_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                Donde = 8;
                //Busca la base de datos Variable CENTRO es Nombre BASE Datos Empresa
                string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
                string rBBDDa = ""; // "NET_PRVR" + rANUAL;
                string rBBDDb = ""; // "NET_PRVV" + rANUAL;


                string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA ";
                DataTable dtV = Main.BuscaLote(Data).Tables[0];
                Donde = 9;
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila3 in dtV.Rows)
                {
                    rANUAL = fila3["ZANO"].ToString();
                    rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
                    rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
                    break;
                }

                foreach (DataRow fila in dt1.Rows)
                {
                    if (fila["CENTRO"].ToString() == "001")
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDa + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDa + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio between '" + TxtDesde.Text + "' and '" + TxtHasta.Text + "')";
                            string a = Main.Ficherotraza("CENTRO 001-->" + SQL);
                    }
                    else
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDb + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDb + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador  LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio between '" + TxtDesde.Text + "' and '" + TxtHasta.Text + "')";

                        string a = Main.Ficherotraza("CENTRO 002-->" + SQL);
                    }

                    DataTable dt2 = Main.BuscaLoteGold(SQL).Tables[0];
                    Lberror.Text = Variables.Error + Environment.NewLine;
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " UPDATE  REC_EMPLEADO SET FECHAALTA ='" + fila2["FECHAINI"].ToString() + "', ";
                        SQL += " FECHABAJA = '" + fila2["FECHAFIN"].ToString() + "', ";
                        SQL += " COTIZACION = " + fila2["Cotizacion"].ToString() + " ";
                        SQL += " WHERE  COD_EMPLEADO = '" + fila["COD_EMPLEADO"].ToString() + "' ";

                        string a = Main.Ficherotraza("UPDATE ENCONTRADO-->" + SQL);
                        //Lberror.Text = " --> 3- Update REC_EMPLEADO " + SQL + Environment.NewLine;
                        DBHelper.ExecuteNonQuery(SQL);
                        //Lberror.Text += Variables.Error + Environment.NewLine;

                        break;
                    }
                }
                Donde = 10;
                //---------------------------------------------------------------------------------------------------------------------;
                //Ahora actualizo campos en local
                //GralConsultalocal();

                Lbmensaje.Text = "Generando calendario para Nóminas...";
                pmtProc1.Text = "Generando calendario para Nóminas...";
                //TAREAS PROCEDIMIENTO
                DBHelper.ExecuteProcedureTareas("");
                Donde = 11;
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
                Donde = 12;

                //Calculo horas laborales y extras
                //TAREAS
                SQL = " UPDATE A ";
                SQL += " SET A.NHORAS = C.CUANTO ,  ";
                SQL += "     A.XHORAS = C.EXTRAS ";
                SQL += " FROM REC_TAREAS A,(SELECT A.COD_EMPLEADO, A.FECHA_EMPLEADOS,N.CENTRO,   ";
                SQL += "                         (CASE WHEN A.ZTOTALMINUTOS > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.ZTOTALMINUTOS END) AS CUANTO, ";
                SQL += "                         (CASE WHEN  A.ZTOTALMINUTOS > B.HORASEXTRAS THEN A.ZTOTALMINUTOS - B.HORASEXTRAS ELSE 0 END ) AS EXTRAS ";
                SQL += "                         FROM REC_TAREAS A,  REC_PARAM B, REC_EMPLEADO N ";
                SQL += "                         WHERE A.COD_EMPLEADO = N.COD_EMPLEADO ";
                SQL += "                         AND B.CENTRO = N.CENTRO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_EMPLEADOS = C.FECHA_EMPLEADOS ";
                //SQL += "                        AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ) C ";


                //antes
                //SQL = " UPDATE REC_TAREAS ";
                //SQL += " SET NHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN 390 ELSE ZTOTALMINUTOS END), ";
                //SQL += "     XHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN ZTOTALMINUTOS - 390 ELSE 0 END) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 13;
                //TAREAS
                //Periodo horas laborales y extras
                SQL = " UPDATE REC_TAREAS ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 14;
                //TAREAS
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
                Donde = 15;
                //TAREAS
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
                Donde = 16;
                //JORNADA
                //Actualizo el resto de campos REC_JORNADA 
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET TOTALMINUTOS = (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) / 60 + DATEPART(HOUR, CONVERT(DATETIME, TRANSCURRIDO)) * 60) + (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) % 60) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 17;

                SQL = " UPDATE A ";
                SQL += " SET A.TOTALTIEMPO = C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, FECHA_JORNADA, SUM(TOTALMINUTOS) AS CUANTO FROM REC_JORNADA ";
                SQL += "                     GROUP BY COD_EMPLEADO, FECHA_JORNADA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 18;

                //Calculo horas laborales y extras
                //SQL = " UPDATE REC_JORNADA A,  REC_PARAM B";
                //SQL += " SET A.NHORAS = (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.TOTALTIEMPO END), ";
                //SQL += "     A.XHORAS = (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN A.TOTALTIEMPO - 390 ELSE 0 END) ";
                //SQL += " WHERE B.CENTRO <> '' ";
                //SQL += " AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ";

                //JORNADA
                SQL = " UPDATE A ";
                SQL += "SET A.NHORAS = C.CUANTO , ";
                SQL += "    A.XHORAS = C.EXTRAS ";
                SQL += "FROM REC_JORNADA A,(SELECT A.COD_EMPLEADO, A.FECHA_JORNADA,  ";
                SQL += "                        (CASE WHEN A.TOTALTIEMPO > B.HORASEXTRAS THEN B.HORASEXTRAS ELSE A.TOTALTIEMPO END) AS CUANTO, ";
                SQL += "                        (CASE WHEN  A.TOTALTIEMPO > B.HORASEXTRAS THEN A.TOTALTIEMPO - B.HORASEXTRAS ELSE 0 END ) AS EXTRAS ";
                SQL += "                        FROM REC_JORNADA A,  REC_PARAM B, REC_EMPLEADO N  ";
                SQL += "                         WHERE A.COD_EMPLEADO = N.COD_EMPLEADO ";
                SQL += "                         AND B.CENTRO = N.CENTRO ) C ";
                //SQL += "                        WHERE B.CENTRO <> '' ";
                //SQL += "                        AND A.COD_EMPLEADO LIKE B.CENTRO + '%' ) C ";
                SQL += "WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "AND A.FECHA_JORNADA = C.FECHA_JORNADA ";


                DBHelper.ExecuteNonQuery(SQL);
                Donde = 19;
                //Periodo horas laborales y extras
                //JORNADA
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 20;
                //Lberror.Text += Variables.Error + Environment.NewLine;
                //JORNADA
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
                Donde = 21;
                //Lberror.Text += Variables.Error + Environment.NewLine;



                //Termino Jornadas. Este lanzamiento despues del anterior TOTALTIEMPO seguidos no funciona en SQl Server asi que intercalo produccion.

                //JORNADA
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
                Donde = 22;
                //Importe horas laborales
                //JORNADA
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
                Donde = 23;
                //Importe horas extras
                //JORNADA
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
                Donde = 24;

                //Ahora que ya está empleado resto de Jornada nuevamente
                //Campos por defecto
                //JORNADA
                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 25;

                //SQL = " UPDATE A ";
                //SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                //SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                //DBHelper.ExecuteNonQuery(SQL);

                //EMPLEADO
                SQL = " UPDATE REC_EMPLEADO SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 26;

                //EMPLEADO
                //De momento mes en curso TxtDesde.Text + "' AND '" + TxtHasta.Text
                SQL = "  UPDATE REC_EMPLEADO SET FECHAALTA_CALCULADA =(CASE  ";
                SQL += " WHEN FECHAALTA is NULL THEN ''  ";
                SQL += " WHEN FECHAALTA <= '" + TxtDesde.Text + "' THEN '" + TxtDesde.Text + "'  ";
                SQL += " WHEN FECHAALTA >= '" + TxtDesde.Text + "' THEN FECHAALTA  ";
                //SQL += " WHEN FECHAALTA >= '" + TxtHasta.Text + "' THEN FECHAALTA  ";               
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 27;
                //EMPLEADO
                SQL = "  UPDATE REC_EMPLEADO SET FECHABAJA_CALCULADA = (CASE  ";
                SQL += " WHEN FECHABAJA is NULL THEN ''  ";
                SQL += " WHEN FECHABAJA <= '" + TxtHasta.Text + "' AND FECHABAJA >= '" + TxtDesde.Text + "' THEN FECHABAJA  ";
                SQL += " ELSE '" + TxtHasta.Text + "'  ";
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 28;
                //SQL = " UPDATE REC_JORNADA SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', FECHAALTA_CALCULADA), '19000101')), FECHABAJA_CALCULADA) ";
                //EMPLEADO
                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_EMPLEADO A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 29;
                //EMPLEADO
                SQL = " UPDATE REC_EMPLEADO SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA')  ";
                //SQL += " TOTAL = TOTALIMPORTE - (DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA')) "; // COSTEVIVIENDA ";
                SQL += " WHERE DIASMES > 0 AND ALQVIVIENDA = 'Si' ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 30;
                //Debug
                SQL = " UPDATE REC_EMPLEADO SET ALQVIVIENDA = 'No', COSTEVIVIENDA = 0 WHERE VIVIENDA is null or  VIVIENDA = 'propia' or  VIVIENDA = '' ";// or VIVIENDA = '' ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 31;

                //JORNADA
                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 32;
                //JORNADA
                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 33;
                //JORNADA
                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, ";
                SQL += "                    VIVIENDA ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 34;


                //Rec_tarifa subtipo vacio en vivienda
                SQL = " UPDATE REC_JORNADA SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') , ";
                SQL += " TOTAL = TOTALIMPORTE - (DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA')) "; // COSTEVIVIENDA ";
                SQL += " WHERE DIASMES > 0 AND ALQVIVIENDA = 'Si' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 35;


                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'No', COSTEVIVIENDA = 0  WHERE VIVIENDA is null or  VIVIENDA = 'propia' ";// or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 36;
                //Ahora que ya está empleado resto de produccion nuevamente

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 37;
                //Campos por defecto
                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 38;
                //De momento mes en curso

                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 39;
                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'No' WHERE VIVIENDA is null or  VIVIENDA = 'propia' or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 40;

                //SQL = " UPDATE REC_PRODUCCION SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', getdate()), '19000101')), getdate()) ";

                SQL = " UPDATE REC_PRODUCCION SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') ";
                SQL += " WHERE DIASMES > 0 AND ALQVIVIENDA = 'Si' ";
                DBHelper.ExecuteNonQuery(SQL);
                Lbmensaje.Text = "Posicionando Fichajes...";
                pmtProc1.Text = "Posicionando Fichajes...";
                Donde = 41;

                //REC_CALENDARIO

                CreaNomina("Total", this.Session["UltimaConsulta"].ToString(), "");
                Donde = 42;
                //REC_EMPLEADO
                //SQL = " UPDATE A ";
                //SQL += " SET A.DIASMES = C.DIASMES, ";
                //SQL += "     A.ALQVIVIENDA = C.ALQVIVIENDA, ";
                //SQL += "     A.COSTEVIVIENDA = C.COSTEVIVIENDA ";
                ////SQL += "     A.VIVIENDA = C.COSTEVIVIENDA  ";
                //SQL += " FROM REC_EMPLEADO A,(SELECT COD_EMPLEADO, DIASMES, ALQVIVIENDA, COSTEVIVIENDA ";
                //SQL += "                     FROM REC_PRODUCCION ";
                //SQL += "                      GROUP BY COD_EMPLEADO, DIASMES, ALQVIVIENDA, COSTEVIVIENDA ) C  ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                //DBHelper.ExecuteNonQuery(SQL);

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
                Donde = 43;
                //SQL = " UPDATE A ";
                //SQL += " SET A.VIVIENDA = C.COSTEVIVIENDA ";
                //SQL += " FROM REC_CALENDARIO A,( SELECT COD_EMPLEADO, COSTEVIVIENDA  ";
                //SQL += "                    FROM REC_PRODUCCION  ) C  ";
                //SQL += "WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";
                //DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE A ";
                //SQL += " SET A.IMPORTE = C.IMPORTE, ";
                //SQL += "    A.INFORME = C.REPORTE ";
                //SQL += " FROM REC_EMPLEADO A, (SELECT R.COD_EMPLEADO, G.REPORTE, CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA AS IMPORTE  ";
                //SQL += "                      FROM REC_JORNADA R, REC_TARIFAS N, REC_PARAM G , REC_EMPLEADO B ";
                //SQL += "                      WHERE N.TIPO = 'VIVIENDA' ";
                //SQL += "                      AND B.CENTRO = G.CENTRO ";
                //SQL += "                      AND R.COD_EMPLEADO = B.COD_EMPLEADO ";
                //SQL += "                      GROUP BY R.COD_EMPLEADO, N.TARIFA, R.COSTEVIVIENDA, G.REPORTE  ) C ";
                //SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                //Cambiar en ambas consultas por tabla PROFILES comentado
                int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
                //int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,ZFECHAINIRECO),0)))) AS DIAS FROM ZPROFILES ", null));


                int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
                //int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL(" SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,ZFECHAINIRECO),0)))) , CONVERT(INT, FORMAT(ZFECHAFINRECO, 'dd'))  AS DIAS FROM ZPROFILES  ", null));

                if (dias != 0)
                {

                    SQL = " UPDATE A  ";
                    SQL += " SET A.NOMINA = C.NOMINA  ";
                    SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO,    ";
                    SQL += "                 CASE WHEN V.COTIZACION = 6  ";
                    SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * 30 / V.DIASMES)))    ";
                    SQL += "                  WHEN V.COTIZACION = 7  ";
                    SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * " + diasNaturales + " / V.DIASMES))) END AS NOMINA  ";
                    SQL += "                 FROM  REC_EMPLEADO V ) C  ";
                    SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";

                    //SQL = " UPDATE A ";
                    //SQL += " SET A.NOMINA = C.NOMINA";
                    //SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO,  ";

                    ////SQL += "                 CASE WHEN V.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(R.PLANTAS * N.TARIFA)) - A.COSTEVIVIENDA)  * 30 / R.DIASMES)) ";
                    ////SQL += "                 WHEN V.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(R.PLANTAS * N.TARIFA)) - R.COSTEVIVIENDA) * " + diasNaturales + " / R.DIASMES)) ";
                    ////SQL += "                 ELSE - 1 END AS NOMINA ";
                    ////SQL += "                 FROM REC_PRODUCCION R, REC_TARIFAS N, REC_EMPLEADO V ";
                    ////SQL += "                 WHERE N.TIPO = 'VIVIENDA' ";
                    ////SQL += "                 AND R.COD_EMPLEADO = V.COD_EMPLEADO ";
                    ////SQL += "                 GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA, R.DIASMES, V.COTIZACION) C ";

                    //SQL += "                 CASE WHEN V.COTIZACION = 6 ";
                    //SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * 30 / R.DIASMES)  ";
                    //SQL += "                  WHEN V.COTIZACION = 7";
                    //SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * " + diasNaturales + " / V.DIASMES) END AS NOMINA  ";
                    //SQL += "                 FROM  REC_EMPLEADO V ";
                    //SQL += "                 GROUP BY V.COD_EMPLEADO, V.COSTEVIVIENDA, V.COTIZACION) C ";

                    //SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                }
                else
                {

                    //SQL = " UPDATE A ";
                    //SQL += " SET A.NOMINA = C.NOMINA ";
                    //SQL += " FROM REC_EMPLEADO A,  ";
                    //SQL += " (SELECT V.COD_EMPLEADO AS COD_EMPLEADO,  ";
                    //SQL += " CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA)) AS NOMINA ";
                    //SQL += " FROM REC_EMPLEADO V ) C ";
                    //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
 
                    SQL = " UPDATE A  ";
                    SQL += " SET A.NOMINA = C.NOMINA  ";
                    SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO,    ";
                    SQL += "                 CASE WHEN V.COTIZACION = 6 AND V.DIASMES < " + diasNaturales + " ";
                    SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * 30 / V.DIASMES)))    ";
                    SQL += "                 WHEN V.COTIZACION = 6  AND V.DIASMES = " + diasNaturales + " ";
                    SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * " + diasNaturales + " / V.DIASMES)))  ";
                    SQL += "                 WHEN V.COTIZACION = 7  ";
                    SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) * " + diasNaturales + " / V.DIASMES))) END AS NOMINA  ";
                    SQL += "                 FROM  REC_EMPLEADO V ) C  ";
                    SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";

                   
                    //SQL = " UPDATE A  ";
                    //SQL += " SET A.NOMINA = C.NOMINA  ";
                    //SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO,     ";
                    //SQL += " CASE WHEN V.COTIZACION = 6  ";
                    //SQL += " THEN CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA))           ";
                    //SQL += " WHEN V.COTIZACION = 7  ";
                    //SQL += " THEN CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA))   ";
                    //SQL += " END AS NOMINA  ";
                    //SQL += " FROM  REC_EMPLEADO V ) C  ";
                    //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";
                    

                    //SQL = " UPDATE A  ";
                    //SQL += " SET A.NOMINA = C.NOMINA  ";
                    //SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO, ";
                    //SQL += "                 CASE WHEN V.COTIZACION = 6  ";
                    //SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA))    ";
                    //SQL += "                  WHEN V.COTIZACION = 7  ";
                    //SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) ) END AS NOMINA  ";
                    //SQL += "                 FROM  REC_EMPLEADO V ) C  ";
                    //SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";

                    //SQL = " UPDATE A  ";
                    //SQL += " SET A.NOMINA = C.NOMINA  ";
                    //SQL += " FROM REC_EMPLEADO A, (SELECT V.COD_EMPLEADO AS COD_EMPLEADO, CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA)) AS NOMINA    ";
                    ////SQL += "                 CASE WHEN V.COTIZACION = 6  ";
                    ////SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA))    ";
                    ////SQL += "                  WHEN V.COTIZACION = 7  ";
                    ////SQL += "                 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), (V.IMPORTE - V.COSTEVIVIENDA) ) END AS NOMINA  ";
                    //SQL += "                 FROM  REC_EMPLEADO V ) C  ";
                    //SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";

                    //SQL = " UPDATE A ";
                    //SQL += " SET A.NOMINA = C.NOMINA ";
                    //SQL += " FROM REC_EMPLEADO A, (SELECT R.COD_EMPLEADO AS COD_EMPLEADO,";
                    //SQL += "                       CASE WHEN V.COTIZACION = 6 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) ";
                    //SQL += "                       WHEN V.COTIZACION = 7 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) END AS NOMINA ";
                    //SQL += "                       FROM REC_JORNADA R, REC_TARIFAS N , REC_EMPLEADO V ";
                    //SQL += "                       WHERE N.TIPO = 'VIVIENDA' ";
                    //SQL += "                       AND R.COD_EMPLEADO = V.COD_EMPLEADO ";
                    //SQL += "                       GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA,  R.DIASMES, V.COTIZACION) C ";
                    //SQL += "  WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                }
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 44;

                Lberror.Text += " Resto de Consultas fin";

                Lbmensaje.Text = "Cargando las listas en cada apartado...";
                pmtProc1.Text = "Cargando las listas en cada apartado...";


                Carga_tablaEmpleados();
                Carga_panelTareas();
                Carga_tablaProduccion();
                Carga_tablaJornada();
                Carga_Jornal_Horas();
                Carga_Jornal_Nominas();
                Carga_Destajo_Nomina();
                Carga_Nomina_resumen();
                Carga_ProddiaImporte();
                Carga_Trabajos();

                Donde = 45;

                Lbmensaje.Text = "El proceso finalizó correctamente";
                pmtProc1.Text = "El proceso finalizó correctamente";
                DvPreparado.Visible = false;
                cuestion.Visible = false;
                Asume.Visible = false;
                Lberror.Visible = false;
                //Response.Redirect("Reconomina.aspx");

            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("btGralCnsulta --> " + Donde + " = " + ex.Message + " --> " + SQL);
                Lberror.Visible = true;
                Lberror.Text += Variables.Error + " --> " + SQL + Environment.NewLine;
                //imgLoad.Visible = false;
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
            Lbmensaje.Text = "Generando calendario para Nóminas...";
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
                Lbmensaje.Text = "Posicionando Fichajes...";
                //pmtProc1.Text = "Posicionando Fichajes...";
                //REC_CALENDARIO
                CreaNomina("Total", this.Session["UltimaConsulta"].ToString(), "");

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

                Lbmensaje.Text = "Cargando las listas en cada apartado...";
                //pmtProc1.Text = "Cargando las listas en cada apartado...";


                Carga_tablaEmpleados();
                Carga_panelTareas();
                Carga_tablaProduccion();
                Carga_tablaJornada();
                Carga_Jornal_Horas();
                Carga_Jornal_Nominas();
                Carga_Destajo_Nomina();
                Carga_Nomina_resumen();
                Carga_ProddiaImporte();
                Carga_Trabajos();

                UltimaConsulta();
                Lbmensaje.Text = "El proceso finalizó correctamente";
                //pmtProc1.Text = "El proceso finalizó correctamente";



                DvPreparado.Visible = false;
                cuestion.Visible = false;
                Asume.Visible = false;


                Lberror.Visible = false;
                //imgLoad.Visible = false;

            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("GralConsultalocal --> " + ex.Message + " --> " + SQL);
                Lberror.Visible = true;
                Lberror.Text += Variables.Error + " --> " + SQL + Environment.NewLine;
                //imgLoad.Visible = false;
                return;
            }
            //Lberror.Text = "";
        }


        protected void BtGralConsulta2_Click(object sender, EventArgs e)
        {
            return;
            BtLimpiaTodo_Click(null,null);

            int Donde = 0;

            //imgLoad.Visible = true;
            //Carga_tablaListaFiltro();
            //BuscaTablasReco(int Modo, string StrFechas, string StrZonaCultivo, string StrEnvase, string StrVariedad, string StrEmpleados, string StrOrden)
            Variables.Error = "";
            //Lberror.Text = " --> BtGralConsulta_Click " + Environment.NewLine;
            //Consultas
            //REC_TAREAS
            //REC_JORNADA
            //REC_PRODUCCION
            //REC_EMPLEADO
            //
            string Fechas = "";
            Mira_CondicionesGral();

            //elimino  el contenido de las Tablas
            //REC_TRABAJOS composicion tabla REC_TAREAS y REC_JORNADAS
            if (TxtDesde.Text =="" || TxtHasta.Text == "")
            {
                TextAlerta.Text = "Las fechas puestas en rango no son correctas.";
                alerta.Visible = true;
                return;
            }
            Fechas = "'" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "'";

            string SQL = " SELECT ZFECHAINIRECO, ZFECHAFINRECO FROM  ZPROFILES ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            if(dt1.Rows.Count == 0)
            {
                SQL = " INSERT INTO ZPROFILES(ZFECHAINIRECO, ZFECHAFINRECO, ZFECHA) ";
                SQL += " VALUES ('" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "') ";
            }
            else
            {
                SQL = "  UPDATE ZPROFILES SET ZFECHAINIRECO = '" + TxtDesde.Text + "', " ;
                SQL += " ZFECHAFINRECO = '" + TxtHasta.Text + "', ";
                SQL += " ZFECHA = '" + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss") + "' ";
                SQL += " WHERE ZID = 1 "; 
            }
            Donde = 1;
            DBHelper.ExecuteNonQuery(SQL);



            //string SQL = " DELETE FROM REC_TRABAJOS ";
            //DBHelper.ExecuteNonQuery(SQL);

            //Lberror.Text += " --> 1- DELETE REC_TRABAJOS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_CALENDARIO ";
            DBHelper.ExecuteNonQuery(SQL);

            SQL = " DELETE FROM REC_TAREAS ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_TAREAS " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_JORNADA ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_JORNADA " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_PRODUCCION ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1- DELETE REC_PRODUCCION " + SQL + Variables.Error + Environment.NewLine;

            SQL = " DELETE FROM REC_EMPLEADO ";
            DBHelper.ExecuteNonQuery(SQL);
            //Lberror.Text += " --> 1-  DELETE REC_EMPLEADO " + SQL + Variables.Error + Environment.NewLine;
            Donde = 2;
            try
            {
                //Tareas
                SQL = Main.BuscaTablasReco(1, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_Jornadas " + SQL + Variables.Error + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_TAREAS ( COD_EMPLEADO, NOMBRE, APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, INIFIN, DESCRINIFIN )";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["INIFIN"].ToString() + "','" + fila["DESCRINIFIN"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_TAREAS " + SQL  + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }

                Donde = 3;


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
                Donde = 4;

                //Calculo horas laborales y extras
                SQL = " UPDATE REC_TAREAS ";
                SQL += " SET NHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN 390 ELSE ZTOTALMINUTOS END), ";
                SQL += "     XHORAS = (CASE WHEN  ZTOTALMINUTOS > 390 THEN ZTOTALMINUTOS - 390 ELSE 0 END) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 5;

                //Periodo horas laborales y extras
                SQL = " UPDATE REC_TAREAS ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 6;

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
                Donde = 7;

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
                Donde = 8;


                //Jornadas
                SQL = Main.BuscaTablasReco(2, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_JORNADA ( COD_EMPLEADO,NOMBRE,APELLIDOS,"; //REC_JORNADAS_RECODAT
                    SQL += " FECHA_JORNADA , HORAINI ,HORAFIN ,TRANSCURRIDO , RECOTABLET)";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_JORNADA"].ToString() + "','" + fila["HORAINI"].ToString() + "','" + fila["HORAFIN"].ToString() + "','" + fila["TRANSCURRIDO"].ToString() + "','";
                    SQL += fila["RECOTABLET"].ToString() + "')";

                    //Lberror.Text += " --> 3- REC_JORNADA " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 9;
                //Actualizo el resto de campos REC_JORNADA 
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET TOTALMINUTOS = (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) / 60 + DATEPART(HOUR, CONVERT(DATETIME, TRANSCURRIDO)) * 60) + (DATEPART(MINUTE, CONVERT(DATETIME, TRANSCURRIDO)) % 60) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 10;
                //Calculo horas laborales y extras
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET NHORAS = (CASE WHEN  TOTALTIEMPO > 390 THEN 390 ELSE TOTALTIEMPO END), ";
                SQL += "     XHORAS = (CASE WHEN  TOTALTIEMPO > 390 THEN TOTALTIEMPO - 390 ELSE 0 END) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 11;
                //Periodo horas laborales y extras
                SQL = " UPDATE REC_JORNADA ";
                SQL += " SET VNHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(NHORAS, 0) % 60) AS VARCHAR(2)), 2)), ";
                SQL += "     VXHORAS = (RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) / 60) AS VARCHAR(8)), 2) +':' +  RIGHT('0' + CAST(FLOOR(COALESCE(XHORAS, 0) % 60) AS VARCHAR(2)), 2)) ";
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 12;
                //Lberror.Text += Variables.Error + Environment.NewLine;

                //SQL = " UPDATE REC_JORNADA ";
                //SQL += " SET IMPORTEMINUTOS = TOTALMINUTOS * (SELECT(TARIFA / 60) FROM[dbo].[REC_TARIFAS] WHERE TIPO = 'JORNAL') ";

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
                Donde = 13;
                //Lberror.Text += Variables.Error + Environment.NewLine;
                SQL = " UPDATE A ";
                SQL += " SET A.TOTALTIEMPO = C.CUANTO ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, FECHA_JORNADA, SUM(TOTALMINUTOS) AS CUANTO FROM REC_JORNADA ";
                SQL += "                     GROUP BY COD_EMPLEADO, FECHA_JORNADA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA_JORNADA = C.FECHA_JORNADA ";
                
                DBHelper.ExecuteNonQuery(SQL);
                Donde = 14;
                //Produccion

                SQL = Main.BuscaTablasReco(3, Fechas, TxtCentro.Text, TxtEnvase.Text, TxtVariedad.Text, TxtCodigo.Text, "", this.Session["FiltroGral"].ToString());
                //Lberror.Text += " --> 1- Carga_destajos " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLoteReco(SQL).Tables[0];
                //Lberror.Text += Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " INSERT INTO REC_PRODUCCION ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                    SQL += " FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA , ";
                    SQL += " DESCRFINCA , ZONA, DESCRZONAZ, ";
                    SQL += " TAREA ,DESCRTAREA, ENVASE, DESCRENVASE,  MARCAENVASE, PLANTAS )";
                    SQL += " VALUES('" + fila["COD_EMPLEADO"].ToString() + "','" + fila["NOMBRE"].ToString() + "','" + fila["APELLIDOS"].ToString() + "','";
                    SQL += fila["FECHA_EMPLEADOS"].ToString() + "','" + fila["HORA_EMPLEADO"].ToString() + "','" + fila["TABLET"].ToString() + "','" + fila["CODFINCA"].ToString() + "','";
                    SQL += fila["DESCRFINCA"].ToString() + "','" + fila["ZONA"].ToString() + "','" + fila["DESCRZONAZ"].ToString() + "','";
                    SQL += fila["TAREA"].ToString() + "','" + fila["DESCRTAREA"].ToString() + "','" + fila["ENVASE"].ToString() + "','";
                    SQL += fila["DESCRENVASE"].ToString() + "','" + fila["MARCAENVASE"].ToString() + "','" + fila["PLANTAS"].ToString() + "')";

                    //Lberror.Text += " --> 2- REC_PRODUCCION (DESTAJOS) " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 15;
                //Termino Jornadas. Este lanzamiento despues del anterior TOTALTIEMPO seguidos no funciona en SQl Server asi que intercalo produccion.

                //SQL = " UPDATE REC_JORNADA SET TOTALIMPORTE = TOTALTIEMPO * (SELECT(TARIFA / 60) FROM[dbo].[REC_TARIFAS] WHERE TIPO = 'JORNAL') ";

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
                Donde = 16;
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
                Donde = 17;
                //Importe horas laborales
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
                Donde = 18;
                //REC EMPLEADOS desde jornadas
                SQL = "SELECT DISTINCT(COD_EMPLEADO) FROM REC_JORNADA ";
                Lberror.Text = " --> 1- Carga_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila in dt1.Rows)
                {
                    SQL = " SELECT TOP 1 (CodEmpleado) AS COD_EMPLEADO, Nombre AS  NOMBRE, Apellidos AS APELLIDOS, ";
                    SQL += " codCentro AS CENTRO, codCategoria AS CATEGORIA, Domicilio AS VIVIENDA ";
                    SQL += " FROM EMPLEADOS WHERE CodEmpleado = '" + fila["COD_EMPLEADO"].ToString() + "'";
                    DataTable dt2 = Main.BuscaLoteReco(SQL).Tables[0];
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " INSERT INTO REC_EMPLEADO ( COD_EMPLEADO, NOMBRE, APELLIDOS,";
                        SQL += " CENTRO, CATEGORIA, VIVIENDA, BUSQUEDA)";
                        SQL += " VALUES('" + fila2["COD_EMPLEADO"].ToString() + "','" + fila2["NOMBRE"].ToString() + "','" + fila2["APELLIDOS"].ToString() + "','";
                        SQL += fila2["CENTRO"].ToString() + "','" + fila2["CATEGORIA"].ToString() + "','" + fila2["VIVIENDA"].ToString() + "',0)";
                        break;
                    }

                    Lberror.Text = " --> 2- REC_EMPLEADO " + SQL + Environment.NewLine;
                    DBHelper.ExecuteNonQuery(SQL);
                    Lberror.Text += Variables.Error + Environment.NewLine;
                }
                Donde = 19;
                //UPDATE REC_EMPLEADO DESDE GOLDENSOFT
                SQL = "SELECT DISTINCT(SUBSTRING(COD_EMPLEADO, 4, LEN(COD_EMPLEADO))) AS TRUNC_EMPLEADO, COD_EMPLEADO, (SUBSTRING(COD_EMPLEADO, 1, 3)) AS CENTRO  FROM REC_EMPLEADO ";
                Lberror.Text = " --> 2- Update_Empleados " + SQL + Environment.NewLine;
                dt1 = Main.BuscaLote(SQL).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                Donde = 20;
                //Busca la base de datos Variable CENTRO es Nombre BASE Datos Empresa
                string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
                string rBBDDa = ""; // "NET_PRVR" + rANUAL;
                string rBBDDb = ""; // "NET_PRVV" + rANUAL;


                string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA ";
                DataTable dtV = Main.BuscaLote(Data).Tables[0];
                Lberror.Text = Variables.Error + Environment.NewLine;
                foreach (DataRow fila3 in dtV.Rows)
                {
                    rANUAL = fila3["ZANO"].ToString();
                    rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
                    rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
                    break;
                }

                foreach (DataRow fila in dt1.Rows)
                {
                    if (fila["CENTRO"].ToString() == "001")
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDa + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDa + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio between '" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "') ";

                        //string a = Main.Ficherotraza("CENTRO 001-->" + SQL);
                    }
                    else
                    {
                        SQL = " SELECT FechaInicio AS FECHAINI, FechaFin AS FECHAFIN, CodigoTrabajador, Cotizacion  ";
                        SQL += " FROM [" + rBBDDb + "].[dbo].TrabajadoresPeriodos ";
                        SQL += " WHERE CodigoTrabajador LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio = ( SELECT MAX (FechaInicio) from [" + rBBDDb + "].[dbo].TrabajadoresPeriodos  where CodigoTrabajador  LIKE '%" + fila["TRUNC_EMPLEADO"].ToString() + "' ";
                        SQL += " AND FechaInicio between '" + TxtDesde.Text + "' AND '" + TxtHasta.Text + "') ";
                        //string a = Main.Ficherotraza("CENTRO 002-->" + SQL);
                    }

                    DataTable dt2 = Main.BuscaLoteGold(SQL).Tables[0];
                    Donde = 21;
                    Lberror.Text = Variables.Error + Environment.NewLine;
                    foreach (DataRow fila2 in dt2.Rows)
                    {
                        SQL = " UPDATE  REC_EMPLEADO SET FECHAALTA ='" + fila2["FECHAINI"].ToString() + "', ";
                        SQL += " FECHABAJA = '" + fila2["FECHAFIN"].ToString() + "', ";
                        SQL += " COTIZACION = " + fila2["Cotizacion"].ToString() + " ";
                        SQL += " WHERE  COD_EMPLEADO = '" + fila["COD_EMPLEADO"].ToString() + "' ";

                        //string a = Main.Ficherotraza("UPDATE ENCONTRADO-->" + SQL);
                        //Lberror.Text = " --> 3- Update REC_EMPLEADO " + SQL + Environment.NewLine;
                        DBHelper.ExecuteNonQuery(SQL);
                        //Lberror.Text += Variables.Error + Environment.NewLine;

                        break;
                    }
                }
                //Ahora que ya está empleado resto de Jornada nuevamente
                Donde = 22;

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 23;
                //Campos por defecto
                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 24;
                SQL = " UPDATE REC_JORNADA SET ALQVIVIENDA = 'No' WHERE VIVIENDA is null or  VIVIENDA = 'propia' or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 25;
                //Consultar con las fechas de empleados estan todas mal
                //SQL = " UPDATE A ";
                //SQL += " SET A.DIASMES = C.DIAS ";
                //SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, DATEDIFF(DAY, FECHAALTA, FECHABAJA) AS DIAS FROM REC_EMPLEADO ";
                //SQL += "                     WHERE FECHABAJA <> '1900-01-01') C  ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                //De momento mes en curso TxtDesde.Text + "' AND '" + TxtHasta.Text
                SQL = "  UPDATE REC_EMPLEADO SET FECHAALTA_CALCULADA =(CASE  ";
                SQL += " WHEN FECHAALTA is NULL THEN ''  ";
                SQL += " WHEN FECHAALTA <= '" + TxtDesde.Text + "' THEN '" + TxtDesde.Text + "'  ";
                SQL += " WHEN FECHAALTA >= '" + TxtDesde.Text + "' THEN FECHAALTA  ";
                //SQL += " WHEN FECHAALTA >= '" + TxtHasta.Text + "' THEN FECHAALTA  ";               
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 26;
                SQL = "  UPDATE REC_EMPLEADO SET FECHABAJA_CALCULADA = (CASE  ";
                SQL += " WHEN FECHABAJA is NULL THEN ''  ";
                SQL += " WHEN FECHABAJA <= '" + TxtHasta.Text + "' AND FECHABAJA >= '" + TxtDesde.Text + "' THEN FECHABAJA  ";
                SQL += " ELSE '" + TxtHasta.Text + "'  ";
                SQL += " END )  ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 27;
                //SQL = " UPDATE REC_JORNADA SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', FECHAALTA_CALCULADA), '19000101')), FECHABAJA_CALCULADA) ";

                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 28;
                //Rec_tarifa subtipo vacio en vivienda
                SQL = " UPDATE REC_JORNADA SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') , ";
                SQL += " TOTAL = TOTALIMPORTE - COSTEVIVIENDA ";
                SQL += " WHERE DIASMES <> -1 AND ALQVIVIENDA = 'Si' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 29;
                //Ahora que ya está empleado resto de produccion nuevamente

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.VIVIENDA ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, VIVIENDA FROM REC_EMPLEADO ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 30;
                //Campos por defecto
                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'Si', DIASMES = 0, COSTEVIVIENDA = 0 ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 31;
                SQL = " UPDATE REC_PRODUCCION SET ALQVIVIENDA = 'No' WHERE VIVIENDA is null or  VIVIENDA = 'propia' or VIVIENDA = '' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 32;
                //Consultar con las fechas de empleados estan todas mal
                //SQL = " UPDATE A ";
                //SQL += " SET A.DIASMES = C.DIAS ";
                //SQL += " FROM REC_JORNADA A,(SELECT COD_EMPLEADO, DATEDIFF(DAY, FECHAALTA, FECHABAJA) AS DIAS FROM REC_EMPLEADO ";
                //SQL += "                     WHERE FECHABAJA <> '1900-01-01') C  ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                //De momento mes en curso

                SQL = " UPDATE A ";
                SQL += " SET A.DIASMES = C.DIAS + 1 ";
                SQL += " FROM REC_PRODUCCION A,(SELECT COD_EMPLEADO, ";
                SQL += "                    CONVERT(INT, FORMAT(FECHABAJA_CALCULADA, 'yyyyMMdd')) - CONVERT(INT, FORMAT(FECHAALTA_CALCULADA, 'yyyyMMdd')) AS DIAS ";
                SQL += "                    FROM REC_EMPLEADO) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 33;
                //SQL = " UPDATE REC_PRODUCCION SET DIASMES = DATEDIFF(DAY, (select dateadd([month], datediff([month], '19000101', getdate()), '19000101')), getdate()) ";

                SQL = " UPDATE REC_PRODUCCION SET COSTEVIVIENDA = DIASMES * (SELECT TARIFA FROM REC_TARIFAS WHERE TIPO = 'VIVIENDA') ";
                SQL += " WHERE DIASMES <> 0 AND ALQVIVIENDA = 'Si' ";

                DBHelper.ExecuteNonQuery(SQL);
                Donde = 34;
                DBHelper.ExecuteProcedureTareas("");
                Donde = 35;
                Lberror.Text += " Resto de Consultas fin";

                Carga_tablaEmpleados();
                Carga_panelTareas();
                Carga_tablaProduccion();
                Carga_tablaJornada();
                Carga_Jornal_Horas();
                Carga_Jornal_Nominas();
                Carga_Destajo_Nomina();
                Carga_Nomina_resumen();
                Carga_ProddiaImporte();
                Carga_Trabajos();
                Donde = 36;

                UltimaConsulta();
                DvPreparado.Visible = false;
                cuestion.Visible = false;
                Asume.Visible = false;

                Lberror.Visible = false;
                //imgLoad.Visible = false;

            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("btGralCnsulta --> " + ex.Message + " = " + Donde + " --> " + SQL);
                Lberror.Visible = true;
                Lberror.Text += Variables.Error + " --> " + SQL + Environment.NewLine;
                //imgLoad.Visible = false;
                return;
            }
            //Lberror.Text = "";
        }

        

        //protected void btOrdenTrabajo_Click(object sender, EventArgs e)
        //{
        //    HtmlButton btn = (HtmlButton)sender;
        //    if (btn.ID == "BtnOrdentrabajo")
        //    {
        //        if (Iord1.Attributes["class"] == "fa fa-angle-up fa-2x")
        //        {
        //            Iord1.Attributes["class"] = "fa fa-angle-down fa-2x";
        //            Iord1.Attributes["title"] = "Orden descendente de las columnas";
        //        }
        //        else
        //        {
        //            Iord1.Attributes["class"] = "fa fa-angle-up fa-2x";
        //            Iord1.Attributes["title"] = "Orden ascendente de las columnas";
        //        }
        //        return;
        //    }

        //}


        //protected void btOrdenEmpleado_Click(object sender, EventArgs e)
        //{
        //    HtmlButton btn = (HtmlButton)sender;
        //    if (btn.ID == "btOrdenEmpleado")
        //    {
        //        if(Iord1.Attributes["class"] == "fa fa-angle-up fa-2x")
        //        {
        //            Iord1.Attributes["class"] = "fa fa-angle-down fa-2x";
        //            Iord1.Attributes["title"] = "Orden descendente de las columnas";
        //        }
        //        else
        //        {
        //            Iord1.Attributes["class"] = "fa fa-angle-up fa-2x";
        //            Iord1.Attributes["title"] = "Orden ascendente de las columnas";
        //        }
        //        return;
        //    }
                
        //}

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            HtmlButton btn = (HtmlButton)sender;
            if (btn.ID == "btPrintCabecera") { ExportGridToExcel("Empleados", gvEmpleado); }
            if (btn.ID == "BtPrintOrden") { ExportGridToExcel("Producción", gvProduccion); }
            if (btn.ID == "BtPrintListas") { ExportGridToExcel("Jornada", gvJornada); }
            if (btn.ID == "BtJornalHora") { ExportGridToExcel("Jornada-dia", gvJornalHora); }
            if (btn.ID == "btnJornalNomina") { ExportGridToExcel("Jornada-mes", gvJornalNomina); }
            if (btn.ID == "BtnDestajoNomina") { ExportGridToExcel("Produccion-Envase-dia", gvDestajoNomina); }
            if (btn.ID == "BtnResumenNomina") { ExportGridToExcel("Produccion-Importe-mes", gvResumenNomina); }
            if (btn.ID == "BtnProdImpDia") { ExportGridToExcel("Produccion-Importe-dia", gvProdImpDia); }
            if (btn.ID == "BtnTrabajoXLS") { ExportGridToExcel("Trabajos", gvTrabajos); }
            if (btn.ID == "BtnTareasXLS") { ExportGridToExcel("Tareas", gvpanelTareas); }
            
        }

        private void ExportGridToExcel(string NameFile, GridView MiGrid)
        {
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            DataTable dt = null;
            //

            if (MiGrid.ID == "gvEmpleado") { Carga_tablaEmpleadosSQL(); dt = Carga_tablaEmpleadosXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvProduccion") { Carga_tablaProduccionSQL(); dt = Carga_tablaProduccionXLS(this.Session["SQL"].ToString());}
            if (MiGrid.ID == "gvJornada") { Carga_tablaJornadaSQL(); dt = Carga_tablaJornadaXLS(this.Session["SQL"].ToString());}
            if (MiGrid.ID == "gvJornalHora") { Carga_Jornal_HorasSQL(); dt = Carga_Jornal_HorasXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvJornalNomina") { Carga_Jornal_NominasSQL(); dt = Carga_Jornal_NominasXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvDestajoNomina") { Carga_Destajo_NominaSQL();  dt = Carga_Destajo_NominaXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvResumenNomina") { Carga_Nomina_resumenSQL(); dt = Carga_Nomina_resumenXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvProdImpDia") { Carga_ProddiaImporteSQL(); dt = Carga_ProddiaImporteXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvTrabajos") { Carga_TrabajosSQL(); dt = Carga_TrabajosXLS(this.Session["SQL"].ToString()); }
            if (MiGrid.ID == "gvpanelTareas") { Carga_panelTareasSQL(); dt = Carga_panelTareaXLS(this.Session["SQL"].ToString()); }


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

        //private void ExportGridToExcel(string NameFile, GridView MiGrid)
        //{
        //    NameFile += " " + DateTime.Now.ToString("dd-MM-yyyy H-mm-ss");
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=" + NameFile + ".xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);


        //        MiGrid.AllowPaging = false;
        //        //Vuelve a cargar el grid
        //        //this.CargarData();
        //        if (MiGrid.ID == "gvEmpleado") { Carga_tablaEmpleados(); }
        //        if (MiGrid.ID == "gvProduccion") { Carga_tablaProduccion(); }
        //        if (MiGrid.ID == "gvJornada") { Carga_tablaJornada(); }
        //        if (MiGrid.ID == "gvJornalHora") { Carga_Jornal_Horas(); }
        //        if (MiGrid.ID == "gvJornalNomina") { Carga_Jornal_Nominas(); }
        //        if (MiGrid.ID == "gvDestajoNomina") { Carga_Destajo_Nomina(); }
        //        if (MiGrid.ID == "gvResumenNomina") { Carga_Nomina_resumen(); }



        //        MiGrid.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in MiGrid.HeaderRow.Cells)
        //        {
        //            cell.BackColor = MiGrid.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in MiGrid.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = MiGrid.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = MiGrid.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        MiGrid.RenderControl(hw);


        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();

        //    }

        //}

        //private void ExportGridToExcel(string NameFile, GridView MiGrid)
        //{
        //    if (NameFile == "") { NameFile = "Export_" + DateTime.Now; }
        //    HttpResponse response = Response;
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    Page pageToRender = new Page();
        //    HtmlForm form = new HtmlForm();
        //    form.Controls.Add(MiGrid);
        //    pageToRender.Controls.Add(form);
        //    response.Clear();
        //    response.Buffer = true;
        //    response.ContentType = "application/vnd.ms-excel";
        //    response.AddHeader("Content-Disposition", "attachment;filename=" + NameFile);
        //    response.Charset = "UTF-8";
        //    response.ContentEncoding = System.Text.Encoding.Default;
        //    pageToRender.RenderControl(htw);
        //    response.Write(sw.ToString());
        //    response.End();
        //}

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
            BtLimpiaTodo_Click(null, null);
            BtGralConsultaMin_Click(null, null);
            //DrVistaEmpleado.Attributes.Add("style", "background-color:#ffffff;");
            //DrVistaEmpleado.Items.Clear();

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
            Lberror.Text = SQL;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaEmpleados();


            //SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2 WHERE NUMERO = " + TxtNumero.Text;

            //Variables.Error = "";
            //Lberror.Text = SQL;

            //DBHelper.ExecuteNonQuery(SQL);
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaJornada();

            //this.Session["Menu"] = "5";
            //Carga_Menus();
            //PNreportLista.Visible = false;
            //DivEtiquetas.Visible = true;

            //CargaTodaLista();

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
        }

        protected void btListaEmpresas_Click(object sender, EventArgs e)
        {
            if (EmpresaGV.Visible == true)
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("idgvEmpleado");
                li.Attributes["class"] = "fa fa-angle-down fa-2x";
                EmpresaGV.Visible = false;
            }
            else
            {
                HtmlGenericControl li = (HtmlGenericControl)FindControl("idgvEmpleado");
                li.Attributes["class"] = "fa fa-angle-up fa-2x";
                //if (CaCheck.Checked == false)
                //{
                //    Carga_tablaJornada();
                //}
                //else
                //{
                //    Carga_tablaCabeceraClose();
                //}
                
                EmpresaGV.Visible = true;
            }
        }

        


        protected void DrVistaEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void BtCancelCabecera_Click(object sender, EventArgs e)
        //{
        //    DrSelCab.Items.Clear();
        //    //DREmpleadoMin.Items.Clear();

        //    HtmlButton btn = (HtmlButton)sender;
        //    HtmlButton li = (HtmlButton)FindControl("BtnuevaCabecera");
        //    btn.Visible = false;
        //    li.Visible = true;

        //    //BtCancelCabecera.Visible = false;
        //    //BtnuevaCabecera.Visible = true;
        //    ////BtnNewCabecera.Visible = true;

        //    ////TxtNumero.Enabled = true;
        //    //DrEmpresa.Enabled = true;
        //    //DrPais.Enabled = true;
        //    //TxtFechaPrepara.Enabled = true;
        //    //TxtFecha.Enabled = true;
        //    //TxtTelefono.Enabled = true;
        //    //TxtMatricula.Enabled = true;
        //    //TxtObservaciones.Enabled = true;
        //    //TxtTransportista.Enabled = true;
        //    //TxtPais.Enabled = true;
        //    //TxtEmpresa.Enabled = true;
        //    //DrTransportista.Enabled = true;

        //    //DrEmpresa.SelectedIndex = -1;
        //    //DrPais.SelectedIndex = -1;
        //    //TxtNumero.Text = "";
        //    //TxtFechaPrepara.Text = "";
        //    //TxtFecha.Text = "";
        //    //TxtTelefono.Text = "";
        //    //TxtMatricula.Text = "";
        //    //TxtObservaciones.Text = "";
        //    //TxtTransportista.Text = "";
        //    //TxtPais.Text = "";
        //    //TxtEmpresa.Text = "";
        //    //DrTransportista.SelectedIndex = -1;
        //    //if (CaCheck.Checked == false)
        //    //{
        //    //    Carga_tablaJornada();
        //    //}
        //    //else
        //    //{
        //    //    Carga_tablaCabeceraClose();
        //    //}
        //    ////Carga_tablaJornada();
        //    //PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");

        //}

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
            //    TextAlerta.Text = "Debe añadir algún dato en las casillas obligatorias para crear un registro nuevo.";
            //    alerta.Visible = true;
            //    return;
            //}
            //int N = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT ID_SECUENCIA FROM ZCARGA_CABECERA A WHERE ID = 1 ", null));
            //TxtNumero.Text = N.ToString();
            //string SQL = "UPDATE ZCARGA_CABECERA SET ID_SECUENCIA = (ID_SECUENCIA + 1) WHERE ID = 1 ";
            //DBHelper.ExecuteNonQuery(SQL);
            //SQL = "INSERT INTO ZCARGA_CABECERA (NUMERO, ID_SECUENCIA, EMPRESA, PAIS, FECHACARGA, ";
            //SQL += " TELEFONO, MATRICULA, TRANSPORTISTA, ESTADO, OBSERVACIONES, FECHAPREPARACION )";
            //SQL += " VALUES(" + TxtNumero.Text + "," + N + ",'" + TxtEmpresa.Text + "','" + TxtPais.Text + "','" + TxtFecha.Text + "','";
            //SQL += TxtTelefono.Text + "','" + TxtMatricula.Text + "','" + TxtTransportista.Text + "',0,'" + TxtObservaciones.Text + "','" + TxtFechaPrepara.Text + "') ";
            //DBHelper.ExecuteNonQuery(SQL);

            //SeleccionCabecera();
            ////LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + TxtEmpresa.Text + ", Pais: " + TxtPais.Text + ", Fecha: " + TxtFecha.Text;
            ////LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + TxtTransportista.Text + ")";
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            ////Carga_tablaJornada();
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
        }


        
        protected void checkCabeceraListas_Click(object sender, EventArgs e)
        {
            //OrdenCabecera();
            gvEmpleado.EditIndex = -1;
            gvProduccion.EditIndex = -1;
            gvJornada.EditIndex = -1;

            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvEmpleado.DataBind();
        }

        protected void checkTodo_Click(object sender, EventArgs e)
        {
            if(LabelAltas.InnerText == "Todos los Empleados:")
            {
                LabelAltas.InnerText = "Sólo alta en esas fechas:";
            }
            else
            {
                LabelAltas.InnerText = "Todos los Empleados:";
            }
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            if (chkOnOff.Checked == true)
            {
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
            //PanelGeneralCabecera.Visible = false;
            //VistaOrden.Visible = true;
            //VistaOrdenNO.Visible = false;
        }

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {
            //if(PanelgeneralFiltro.Visible == true)
            //{
            //    PanelgeneralFiltro.Visible = false;
            //}
            //else
            //{
            //    PanelgeneralFiltro.Visible = true;
            //}
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

        protected void gvJornalHora_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrJornalHora.SelectedItem.Value == "1000")
            {
                gvJornalHora.AllowPaging = false;
                Carga_Jornal_Horas();
            }
            else
            {
                gvJornalHora.AllowPaging = true;
                gvJornalHora.PageSize = Convert.ToInt32(DrJornalHora.SelectedItem.Value);
                Carga_Jornal_Horas();
            }
        }

        protected void gvJornalNomina_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrJornalNomina.SelectedItem.Value == "1000")
            {
                gvJornalNomina.AllowPaging = false;
                Carga_Jornal_Nominas();
            }
            else
            {
                gvJornalNomina.AllowPaging = true;
                gvJornalNomina.PageSize = Convert.ToInt32(DrJornalNomina.SelectedItem.Value);
                Carga_Jornal_Nominas();
            }
        }
        protected void gvDestajoNomina_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrDestajoNomina.SelectedItem.Value == "1000")
            {
                gvDestajoNomina.AllowPaging = false;
                Carga_Destajo_Nomina();
            }
            else
            {
                gvDestajoNomina.AllowPaging = true;
                gvDestajoNomina.PageSize = Convert.ToInt32(DrDestajoNomina.SelectedItem.Value);
                Carga_Destajo_Nomina();
            }
        }
        protected void gvResumenNomina_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrResumenNomina.SelectedItem.Value == "1000")
            {
                gvResumenNomina.AllowPaging = false;
                Carga_Nomina_resumen();
            }
            else
            {
                gvResumenNomina.AllowPaging = true;
                gvResumenNomina.PageSize = Convert.ToInt32(DrResumenNomina.SelectedItem.Value);
                Carga_Nomina_resumen();
            }
        }

        protected void gvTrabajos_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrTrabajo.SelectedItem.Value == "1000")
            {
                gvTrabajos.AllowPaging = false;
                Carga_Trabajos();
            }
            else
            {
                gvTrabajos.AllowPaging = true;
                gvTrabajos.PageSize = Convert.ToInt32(DrTrabajo.SelectedItem.Value);
                Carga_Trabajos();
            }
        }

        protected void gvJornada_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddListaPageSize.SelectedItem.Value == "1000")
            {
                gvJornada.AllowPaging = false;
                Carga_tablaJornada();
            }
            else
            {
                gvJornada.AllowPaging = true;
                gvJornada.PageSize = Convert.ToInt32(ddListaPageSize.SelectedItem.Value);
                Carga_tablaJornada();
            }
        }
        protected void gvEmpleado_PageSize_Changed(object sender, EventArgs e)
        {
            if(ddCabeceraPageSize.SelectedItem.Value == "1000")
            {
                gvEmpleado.AllowPaging = false;
                Carga_tablaEmpleados();
            }
            else
            {
                gvEmpleado.AllowPaging = true;
                gvEmpleado.PageSize = Convert.ToInt32(ddCabeceraPageSize.SelectedItem.Value);
                Carga_tablaEmpleados();
            }

        }
        protected void gvProduccion_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddControlPageSize.SelectedItem.Value == "1000")
            {
                gvProduccion.AllowPaging = false;
                Carga_tablaProduccion();
            }
            else
            {
                gvProduccion.AllowPaging = true;
                gvProduccion.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
                Carga_tablaProduccion();
            }
        }

        
        protected void gvpanelTareas_PageSize_Changed(object sender, EventArgs e)
        {
            if (DrpanelRaw.SelectedItem.Value == "1000")
            {
                gvpanelTareas.AllowPaging = false;
                Carga_panelTareas();
            }
            else
            {
                gvpanelTareas.AllowPaging = true;
                gvpanelTareas.PageSize = Convert.ToInt32(DrpanelRaw.SelectedItem.Value);
                Carga_panelTareas();
            }
        }

        protected void gvProdImpDia_PageSize_Changed(object sender, EventArgs e)
        {
            if (DdgvProdImpDiaPage.SelectedItem.Value == "1000")
            {
                gvProdImpDia.AllowPaging = false;
                Carga_ProddiaImporte();
            }
            else
            {
                gvProdImpDia.AllowPaging = true;
                gvProdImpDia.PageSize = Convert.ToInt32(DdgvProdImpDiaPage.SelectedItem.Value);
                Carga_ProddiaImporte();
            }
        }

        

        //private void OrdenDeGrid(DropDownList Dr1, DropDownList Dr2 )
        //{
        //    ElOrden = "";           
        //    //DRgvEmpleado.BackColor = Color.FromName("#ffffff");
        //    //TxtConsultaEmpleado.BackColor = Color.FromName("#ffffff");
        //    Dr1.BackColor = Color.FromName("#ffffff");
        //    Dr2.BackColor = Color.FromName("#ffffff");

        //    if(Dr1.ID == "DrEmpleado1")
        //    {
        //        string Condicion = TxtConsultaEmpleado.Text;
        //        if (DRgvEmpleado.SelectedItem.Value != "Ninguno")
        //        {
        //            DRgvEmpleado.BackColor = Color.FromName("#fcf5d2");
        //            TxtConsultaEmpleado.BackColor = Color.FromName("#fcf5d2");
        //        }
        //    }
 


        //    if (Dr1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            if (ElOrden.Contains(Dr1.SelectedItem.Value))
        //            {}
        //            else
        //            {
        //                ElOrden = " ORDER BY " + Dr1.SelectedItem.Value;
        //            }
                    
        //        }
        //        Dr1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (Dr2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrden == "")
        //        {
        //            ElOrden = " ORDER BY " + Dr2.SelectedItem.Value;
        //        }
        //        else
        //        {
        //            if (ElOrden.Contains(Dr2.SelectedItem.Value))
        //            { }
        //            else
        //            {
        //                ElOrden += ", " + Dr2.SelectedItem.Value + " ";
        //            }
        //        }
        //        Dr2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    //if (DREmpleado3.SelectedItem.Value != "Ninguno")
        //    //{
        //    //    if (ElOrden == "")
        //    //    {
        //    //        ElOrden = " ORDER BY " + DREmpleado3.SelectedItem.Text;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (ElOrden.Contains(DREmpleado3.SelectedItem.Text))
        //    //        { }
        //    //        else
        //    //        {
        //    //            ElOrden += ", " + DREmpleado3.SelectedItem.Text;
        //    //        }
        //    //    }
        //    //    DREmpleado3.BackColor = Color.FromName("#fcf5d2");
        //    //}

        //    //if (DREmpleado4.SelectedItem.Value != "Ninguno")
        //    //{
        //    //    if (ElOrden == "")
        //    //    {
        //    //        ElOrden = " ORDER BY " + DREmpleado4.SelectedItem.Text;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (ElOrden.Contains(DREmpleado4.SelectedItem.Text))
        //    //        { }
        //    //        else
        //    //        {
        //    //            ElOrden += ", " + DREmpleado4.SelectedItem.Text;
        //    //        }
        //    //    }
        //    //    DREmpleado4.BackColor = Color.FromName("#fcf5d2");
        //    //}

        //    //if (DREmpleado5.SelectedItem.Value != "Ninguno")
        //    //{
        //    //    if (ElOrden == "")
        //    //    {
        //    //        ElOrden = " ORDER BY " + DREmpleado5.SelectedItem.Text;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (ElOrden.Contains(DREmpleado5.SelectedItem.Text))
        //    //        { }
        //    //        else
        //    //        {
        //    //            ElOrden += ", " + DREmpleado5.SelectedItem.Text;
        //    //        }
        //    //    }
        //    //    DREmpleado5.BackColor = Color.FromName("#fcf5d2");
        //    //}

        //    //if (DREmpleado6.SelectedItem.Value != "Ninguno")
        //    //{
        //    //    if (ElOrden == "")
        //    //    {
        //    //        ElOrden = " ORDER BY " + DREmpleado6.SelectedItem.Text;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (ElOrden.Contains(DREmpleado6.SelectedItem.Text))
        //    //        { }
        //    //        else
        //    //        {
        //    //            ElOrden += ", " + DREmpleado6.SelectedItem.Text;
        //    //        }
        //    //    }
        //    //    DREmpleado6.BackColor = Color.FromName("#fcf5d2");
        //    //}

        //    //if (DREmpleado7.SelectedItem.Value != "Ninguno")
        //    //{
        //    //    if (ElOrden == "")
        //    //    {
        //    //        ElOrden = " ORDER BY " + DREmpleado7.SelectedItem.Text;
        //    //    }
        //    //    else
        //    //    {
        //    //        if (ElOrden.Contains(DREmpleado7.SelectedItem.Text))
        //    //        { }
        //    //        else
        //    //        {
        //    //            ElOrden += ", " + DREmpleado7.SelectedItem.Text;
        //    //        }
        //    //    }
        //    //    DREmpleado7.BackColor = Color.FromName("#fcf5d2");
        //    //}
        //    ////if (CaCheck.Checked == false)
        //    ////{
        //    ////    Carga_tablaJornada();
        //    ////}
        //    ////else
        //    ////{
        //    ////    Carga_tablaCabeceraClose();
        //    ////}
        //    ////Carga_tablaJornada();
        //}
        //Depurar unificar este procedimiento
        protected void PrintReportOff_Click(object sender, EventArgs e)
        {
            ReportViewer0.Visible = false;

            //PNreportLista.Visible = false;
            PNFiltrosLista.Visible = true;
            PNGridLista.Visible = true;
        }


        protected void PrintNominas(string CodEmpleado, string Centro)
        {
            try
            {
                DataTable DT = null;
                string ZRuta = "";
                string ZReporte = "";
                string ZDataSet = "";

                this.Session["Centro"] = Centro;

                string SQL = " SELECT A.REPORTE , B.ZRUTA, B.ZREPORTE, B.ZDATASET, B.ZSQL ";
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


                string filtros = "";
                ReportViewer0.Visible = false;


                SQL = "A.FECHA, A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.ENVASES, A.CANTIDAD, A.PLANTAS, A.TOTALCORTE, A.ACTIVIDAD, A.HORAS, A.TOTALJORNADA, A.TOTALDIA, A.VIVIENDA, A.TOTALES, TOTALPLANTAS, SUMCORTE, SUMJORNADA, SUMTOTALDIA,  SUMTIEMPO"; //, A.TABLET, B.ZONA ";

                Carga_FiltrosGral(SQL);
                filtros = this.Session["Filtro"].ToString();
                if (Centro == "001" || Centro == "")
                {
                    SQL = " SELECT FORMAT(A.FECHA, 'dd-MM-yyyy') AS FECHA ";
                    SQL += " ,A.COD_EMPLEADO ";
                    SQL += " ,A.NOMBRE ";
                    SQL += " ,A.APELLIDOS ";
                    SQL += " ,A.ENVASES ";
                    SQL += " ,A.CANTIDAD ";
                    SQL += " ,A.PLANTAS ";
                    SQL += " ,A.TOTALCORTE ";
                    SQL += " ,A.ACTIVIDAD ";
                    SQL += " ,A.HORAS ";
                    SQL += " ,A.TOTALJORNADA ";
                    SQL += " ,A.TOTALDIA ";
                    SQL += " ,A.VIVIENDA ";
                    SQL += " ,A.TOTALES ";
                    SQL += " ,(SELECT SUM(PLANTAS)  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO) AS TOTALPLANTAS ";

                    SQL += "  ,(SELECT SUM(TOTALCORTE)  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO) AS SUMCORTE ";

                    SQL += "  ,(SELECT SUM(TOTALJORNADA)  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO) AS SUMJORNADA ";

                    SQL += "  ,(SELECT SUM(TOTALDIA)  ";
                    SQL += "  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO) AS SUMTOTALDIA ";

                    SQL += "  ,(SELECT SUM(TOTALDIA) - A.VIVIENDA  ";
                    SQL += "  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO) AS SUMTOTAL ";

                    SQL += " ,STUFF((SELECT '  ' + DESCRENVASE + ' = ' + CONVERT(VARCHAR(255), SUM(MARCAENVASE)) + ' ; '  ";
                    SQL += " FROM REC_PRODUCCION WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO, DESCRENVASE FOR XML PATH('')),1,1,'') AS CONCATENVASE "; // CONCATCANTIDAD ";

                    SQL += ", RIGHT('000' + CAST(FLOOR(COALESCE(A.TOTALMINUTOS, 0) / 60) AS VARCHAR(8)), 3) +':' +  ";
                    SQL += " RIGHT('0' + CAST(FLOOR(COALESCE(A.TOTALMINUTOS, 0) % 60) AS VARCHAR(2)), 2)  AS SUMTIEMPO    ";

                    SQL += " ,A.NHORAS ";
                    SQL += " ,A.XHORAS ";
                    SQL += " ,A.JNHORAS ";
                    SQL += " ,A.JXHORAS ";

                    SQL += " ,A.VNHORAS ";
                    SQL += " ,A.VXHORAS ";
                    SQL += " ,A.JVNHORAS ";
                    SQL += " ,A.JVXHORAS ";

                    SQL += ", (SELECT RIGHT('0' + CAST(FLOOR(COALESCE(SUM(NHORAS), 0) / 60) AS VARCHAR(8)), 2) +':' +  ";
                    SQL += " RIGHT('0' + CAST(FLOOR(COALESCE(SUM(NHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    SQL += " GROUP BY COD_EMPLEADO)  AS SUMVNHORAS    ";

                    SQL += ", (SELECT RIGHT('0' + CAST(FLOOR(COALESCE(SUM(XHORAS), 0) / 60) AS VARCHAR(8)), 2) + ':'  +  ";
                    SQL += "  RIGHT('0' + CAST(FLOOR(COALESCE(SUM(XHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO)  AS SUMVXHORAS ";

                    SQL += ", (SELECT RIGHT('0' + CAST(FLOOR(COALESCE(SUM(JNHORAS), 0) / 60) AS VARCHAR(8)), 2) +':' +  ";
                    SQL += " RIGHT('0' + CAST(FLOOR(COALESCE(SUM(JNHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    SQL += " GROUP BY COD_EMPLEADO)  AS SUMJVNHORAS    ";

                    SQL += ", (SELECT RIGHT('0' + CAST(FLOOR(COALESCE(SUM(JXHORAS), 0) / 60) AS VARCHAR(8)), 2) +':' +  ";
                    SQL += " RIGHT('0' + CAST(FLOOR(COALESCE(SUM(JXHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    SQL += " GROUP BY COD_EMPLEADO)  AS SUMJVXHORAS    ";


                    SQL += " ,A.TIMPNHORAS ";
                    SQL += " ,A.TIMPXHORAS ";

                    SQL += " ,(SELECT FORMAT(FECHAALTA_CALCULADA, 'dd-MM-yyyy')  FROM REC_EMPLEADO WHERE COD_EMPLEADO = A.COD_EMPLEADO) AS FECHAALTA_CALCULADA ";
                    SQL += " ,(SELECT FORMAT(FECHABAJA_CALCULADA, 'dd-MM-yyyy')  FROM REC_EMPLEADO WHERE COD_EMPLEADO = A.COD_EMPLEADO) AS FECHABAJA_CALCULADA ";
                    //SQL += " ,(SELECT ISNULL(TIMPNHORAS,0) + ISNULL(TIMPXHORAS,0) FROM REC_CALENDARIO     ";
                    //SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    //SQL += " GROUP BY COD_EMPLEADO)  AS JTIMPXHORAS    ";
                    SQL += " FROM REC_CALENDARIO A, REC_EMPLEADO C ";
                }
                else
                {
                    SQL = " SELECT FORMAT(A.FECHA, 'dd-MM-yyyy') AS FECHA ";
                    SQL += " ,A.COD_EMPLEADO ";
                    SQL += " ,A.NOMBRE ";
                    SQL += " ,A.APELLIDOS ";

                    //SQL += "  ,(SELECT SUM(TOTALDIA) - A.VIVIENDA  ";
                    //SQL += "  FROM REC_CALENDARIO ";
                    //SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    //SQL += "  GROUP BY COD_EMPLEADO) AS SUMTOTAL ";
                    SQL += "  ,(SELECT SUM(TIMPNHORAS + TIMPXHORAS) - VIVIENDA  ";
                    SQL += "  FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO ";
                    SQL += "  GROUP BY COD_EMPLEADO,  VIVIENDA) AS SUMTOTAL ";

                    SQL += " ,A.VIVIENDA ";
                    SQL += " ,A.NHORAS ";
                    SQL += " ,A.XHORAS ";
                    SQL += " ,A.VNHORAS ";
                    SQL += " ,A.VXHORAS ";


                    SQL += ", (SELECT RIGHT('000' + CAST(FLOOR(COALESCE(SUM(NHORAS), 0) / 60) AS VARCHAR(8)), 3) +':' +  ";
                    SQL += " RIGHT('0' + CAST(FLOOR(COALESCE(SUM(NHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    SQL += " GROUP BY COD_EMPLEADO)  AS SUMVNHORAS    ";

                    SQL += ", (SELECT RIGHT('000' + CAST(FLOOR(COALESCE(SUM(XHORAS), 0) / 60) AS VARCHAR(8)), 3) + ':'  +  ";
                    SQL += "  RIGHT('0' + CAST(FLOOR(COALESCE(SUM(XHORAS), 0) % 60) AS VARCHAR(2)), 2) FROM REC_CALENDARIO ";
                    SQL += "  WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO)  AS SUMVXHORAS ";

                    SQL += " ,A.TIMPNHORAS ";
                    SQL += " ,A.TIMPXHORAS ";

                    SQL += ",(SELECT SUM(ISNULL(TIMPNHORAS, 0))  FROM REC_CALENDARIO  WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO)  AS SUMJVNHORAS ";
                    SQL += ",(SELECT SUM(ISNULL(TIMPXHORAS, 0))  FROM REC_CALENDARIO  WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO)  AS SUMJVXHORAS ";

                    //SQL += " ,(SELECT ISNULL(A.TIMPNHORAS,0) + ISNULL(A.TIMPXHORAS,0) FROM REC_CALENDARIO     ";
                    //SQL += " WHERE COD_EMPLEADO = A.COD_EMPLEADO";
                    //SQL += " GROUP BY COD_EMPLEADO)  AS JTIMPXHORAS    ";


                    SQL += " ,TIMPNHORAS + TIMPXHORAS AS JTIMPXHORAS   ";

                    SQL += " ,(SELECT FORMAT(FECHAALTA_CALCULADA, 'dd-MM-yyyy')  FROM REC_EMPLEADO WHERE COD_EMPLEADO = A.COD_EMPLEADO) AS FECHAALTA_CALCULADA ";
                    SQL += " ,(SELECT FORMAT(FECHABAJA_CALCULADA, 'dd-MM-yyyy')  FROM REC_EMPLEADO WHERE COD_EMPLEADO = A.COD_EMPLEADO) AS FECHABAJA_CALCULADA ";


                    SQL += " FROM REC_CALENDARIO A, REC_EMPLEADO C ";
                }

                if (CodEmpleado == "Todos")
                {
                    SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += " AND C.BUSQUEDA = 1 ";
                }
                else
                {
                    SQL += " WHERE A.COD_EMPLEADO ='" + CodEmpleado + "' ";
                    SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += " AND C.BUSQUEDA = 1 ";
                }

                if (filtros != "")
                {
                    SQL += filtros;
                    if (filtros.Contains("CENTRO"))
                    {
                        string[] Partes = System.Text.RegularExpressions.Regex.Split(filtros, "AND");
                        for (int i = 0; i < Partes.Count(); i++)
                        {
                            if (Partes[i].Contains("CENTRO"))
                            {
                                Centro = Partes[i].Replace("CENTRO", "");
                                Centro = Centro.Replace(" ", "");
                                Centro = Centro.Replace("=", "");
                                Centro = Centro.Replace("<", "");
                                Centro = Centro.Replace("<>", "");
                                Centro = Centro.Replace(">", "");
                                Centro = Centro.Replace("IN", "");
                                Centro = Centro.Replace("NOT", "");
                                break;
                            }
                        }
                    }
                }

                if (ElOrden != "")
                {
                    SQL += ElOrden;
                }
                else
                {
                    SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA ";
                }

                ReportViewer0.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
                

                //if (Centro == "")
                //{
                //    ReportViewer0.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportPrintNominas001.rdlc");
                //}
                //else
                //{
                //    if (Centro == "001")
                //    {
                //        ReportViewer0.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportPrintNominas001.rdlc");
                //    }
                //    else
                //    {
                //        ReportViewer0.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportPrintNominas002.rdlc");
                //    }
                //}

                ReportViewer0.LocalReport.EnableExternalImages = true;
                DataTable dt = Main.BuscaLote(SQL).Tables[0];
                dtsN.Tables.RemoveAt(0);    //Eliminamos la tabla que crea por defecto
                                            //DataTable dtCopy = dt.Copy();
                dtsN.Tables.Add(dt.Copy());   //Añadimos la tabla que acabamos de crear dtlistacamion

                //Limpio los enlaces del ReportViewer 
                ReportViewer0.LocalReport.DataSources.Clear();
                //ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource("DtPrintNomina", dtsN.Tables[0]));
                ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsN.Tables[0]));

                this.Session["Menu"] = "9";
                Carga_Menus();
                ReportViewer0.DataBind();

                ReportViewer0.LocalReport.Refresh();    //Actualizamos el report
                ReportViewer0.Visible = true;

            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("PrintNomina --> " + ex.Message);
                Variables.Error = ex.Message;
                TextAlerta.Text = ex.Message;
                alerta.Visible = true;
            }

        }

        protected void PrintReport_Click(object sender, EventArgs e)
        {
            try
            {
                string SQL = "";
                string filtros = "";
                HtmlButton btn = (HtmlButton)sender;
                ReportViewer0.Visible = false;

                DataTable DT = null;
                string ZRuta = "";
                string ZReporte = "";
                string ZDataSet = "";

                SQL = " SELECT A.REPORTE , B.ZRUTA, B.ZREPORTE, B.ZDATASET, B.ZSQL ";
                SQL += " FROM REC_PARAM A, ZINFORMES B";
                SQL += " WHERE A.REPORTE = B.ZID";
                SQL += " AND A.CENTRO = '" + btn.ID + "' ";

                DT = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow filas in DT.Rows)
                {
                    ZRuta = filas["ZRUTA"].ToString();
                    ZReporte = filas["ZREPORTE"].ToString();
                    ZDataSet = filas["ZSQL"].ToString();
                    //ReportViewer1.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
                    break;
                }




                if (btn.ID == "BtnPrintDestajoNomina")
                {
                    //ReportViewer0.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportProdEnvDia.rdlc");
                    ReportViewer0.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);

                    //SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, B.FECHA_JORNADA,  DESCRCAJAS, CAJAS, DESCRMANOJOS, MANOJOS , DESCRENVASE, A.ENVASE, B.TAREA, PLANTAS, IMPORTE, A.TABLET, B.ZONA ";
                    SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS,  DESCRCAJAS, CAJAS,  B.TAREA, PLANTAS, IMPORTE "; //, A.TABLET, B.ZONA ";

                    Carga_FiltrosGral(SQL);
                    filtros = this.Session["Filtro"].ToString();

                    SQL = " SELECT DISTINCT(A.COD_EMPLEADO) AS COD_EMPLEADO, A.NOMBRE,   A.APELLIDOS, 		  ";
                    SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
                    SQL += " A.DIASMES, ";
                    SQL += " A.COSTEVIVIENDA, ";

                    SQL += " STUFF((SELECT '| ' + DESCRENVASE + ' ' ";
                    SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
                    SQL += " GROUP BY DESCRENVASE ";
                    SQL += " FOR XML PATH('')),1,1,'') AS DESCRCAJAS, ";

                    SQL += " STUFF((SELECT '| ' +  CONVERT(VARCHAR(255), SUM(REC_PRODUCCION.MARCAENVASE)) + ' ' ";
                    SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
                    SQL += " GROUP BY DESCRENVASE ";
                    SQL += " FOR XML PATH('')),1,1,'') AS CAJAS, ";

                    SQL += " (SELECT SUM(REC_PRODUCCION.PLANTAS) AS CAJAS  FROM REC_PRODUCCION  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS) AS PLANTAS, ";

                    SQL += " '" + this.Session["UltimaConsulta"].ToString() + "' AS FECHAFIN, ";
                    SQL += " '" + this.Session["UltimaConsultaFin"].ToString() + "' AS FECHAINI, ";

                    SQL += " (SELECT CONVERT(DECIMAL(8, 2), SUM(REC_PRODUCCION.PLANTAS * REC_TARIFAS.TARIFA)) ";
                    SQL += " FROM REC_PRODUCCION, REC_TARIFAS  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO    AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  AND REC_TARIFAS.TIPO = 'DESTAJO' ";
                    SQL += " AND REC_PRODUCCION.ENVASE = REC_TARIFAS.SUBTIPO   GROUP BY REC_PRODUCCION.COD_EMPLEADO ) AS IMPORTE, ";


                    SQL += " ((SELECT CONVERT(DECIMAL(8, 2), (SUM(REC_PRODUCCION.PLANTAS * REC_TARIFAS.TARIFA)))  ";
                    SQL += " FROM REC_PRODUCCION, REC_TARIFAS  ";
                    SQL += " WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  ";
                    SQL += " AND REC_TARIFAS.TIPO = 'DESTAJO'  ";
                    SQL += " AND REC_PRODUCCION.ENVASE = REC_TARIFAS.SUBTIPO  ";
                    SQL += " GROUP BY REC_PRODUCCION.COD_EMPLEADO) -A.COSTEVIVIENDA) AS TOTAL  ";


                    SQL += "  FROM REC_PRODUCCION A  ";
                    SQL += "  INNER JOIN REC_EMPLEADO Z ON A.COD_EMPLEADO = Z.COD_EMPLEADO  AND Z.BUSQUEDA = 1  ";
                    SQL += "  WHERE A.COD_EMPLEADO IS NOT NULL  ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }

                    SQL += "  GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.MARCAENVASE, A.COSTEVIVIENDA, A.DIASMES  ";

                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, FECHA_EMPLEADOS  ";
                    }

                    ReportViewer0.LocalReport.EnableExternalImages = true;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    dtsP.Tables.RemoveAt(0);    //Eliminamos la tabla que crea por defecto
                                                //DataTable dtCopy = dt.Copy();
                    dtsP.Tables.Add(dt.Copy());   //Añadimos la tabla que acabamos de crear dtlistacamion

                    //Limpio los enlaces del ReportViewer 
                    //Ahora abre menu 9
                    //PNreportLista.Visible = true;
                    ReportViewer0.LocalReport.DataSources.Clear();
                    //ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource("DtProdenvDia", dtsP.Tables[0]));
                    ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsP.Tables[0]));
                }
                else if (btn.ID == "BtnPrintJornalHora")
                {
                    //ReportViewer0.LocalReport.ReportPath = Server.MapPath("~/Reports/ReportJornalHora.rdlc");
                    ReportViewer0.LocalReport.ReportPath = Server.MapPath(ZRuta + ZReporte);
                    
                    SQL = " COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_JORNADA, TOTALIMPORTE, TOTALTIEMPO";//HORAINI, HORAFIN,IMPORTEMINUTOS,
                    Carga_FiltrosGral(SQL);

                    //OrdenDeGrid(DRJornalHora1, DRJornalHora2);

                    filtros = this.Session["Filtro"].ToString();

                    SQL = " SELECT A.COD_EMPLEADO,   ";
                    SQL += " A.NOMBRE,   ";
                    SQL += " A.APELLIDOS,   ";
                    SQL += " FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA,  ";
                    SQL += " SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, A.TOTALTIEMPO, ''), 114), 1, 5) TIEMPO,      ";
                    SQL += " A.COSTEVIVIENDA as VIVIENDA, A.DIASMES AS DIAS,  ";

                    SQL += " (SELECT CONVERT(Decimal(8, 2), ((sum(V.TOTALMINUTOS) * B.TARIFA / 60)))  ";
                    SQL += "  FROM REC_JORNADA V, REC_TARIFAS B  ";
                    SQL += " WHERE V.COD_EMPLEADO = A.COD_EMPLEADO AND B.TIPO = 'JORNAL'  ";
                    SQL += " AND V.FECHA_JORNADA = A.FECHA_JORNADA  ";
                    SQL += " AND B.SUBTIPO = Z.CATEGORIA  ";
                    SQL += " GROUP BY V.COD_EMPLEADO, V.FECHA_JORNADA, B.TARIFA) AS TOTALIMPORTE,  ";

                    SQL += " (SELECT CONVERT(Decimal(8, 2), ((sum(V.TOTALMINUTOS) * B.TARIFA / 60))) - V.COSTEVIVIENDA   ";
                    SQL += "  FROM REC_JORNADA V, REC_TARIFAS B  ";
                    SQL += "  WHERE V.COD_EMPLEADO = A.COD_EMPLEADO AND B.TIPO = 'JORNAL'  ";
                    SQL += "  AND B.SUBTIPO = Z.CATEGORIA   ";
                    SQL += "  GROUP BY V.COD_EMPLEADO, V.COSTEVIVIENDA, B.TARIFA) AS TOTAL,  ";
                    SQL += " '" + this.Session["UltimaConsulta"].ToString() + "' AS FECHAFIN,";
                    SQL += " '" + this.Session["UltimaConsultaFin"].ToString() + "' AS FECHAINI ";

                    SQL += "  FROM REC_JORNADA A, REC_EMPLEADO Z, REC_TARIFAS B  ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL  ";
                    SQL += "  AND A.COD_EMPLEADO = Z.COD_EMPLEADO  AND Z.BUSQUEDA = 1  ";

                    //SQL = " SELECT ";
                    //SQL += " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA,  ";
                    //SQL += "  A.TOTALIMPORTE, ";
                    //SQL += " SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, A.TOTALTIEMPO, ''), 114),1,5) TIEMPO,   ";
                    //SQL += " A.COSTEVIVIENDA as VIVIENDA, A.DIASMES AS DIAS, ";
                    //SQL += " (SELECT CONVERT(Decimal(8, 2), ((sum(V.TOTALMINUTOS) * B.TARIFA / 60))) - V.COSTEVIVIENDA FROM REC_JORNADA V, REC_TARIFAS B ";
                    //SQL += " WHERE V.COD_EMPLEADO = A.COD_EMPLEADO AND B.TIPO = 'JORNAL' ";
                    //SQL += " GROUP BY V.COD_EMPLEADO,V.COSTEVIVIENDA, B.TARIFA) AS TOTAL, ";
                    //SQL += " '" + this.Session["UltimaConsulta"].ToString() + "' AS FECHAFIN,";
                    //SQL += " '" + this.Session["UltimaConsultaFin"].ToString() + "' AS FECHAINI ";
                    //SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z , REC_TARIFAS B ";
                    //SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    //SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    //SQL += " AND Z.BUSQUEDA = 1 ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    SQL += "  GROUP BY  A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, A.TOTALTIEMPO, A.COSTEVIVIENDA, A.DIASMES  ";
                    if (btn.ID == "BtnPrintJornalHora") {SQL += ", Z.CATEGORIA  "; }

                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA ";
                    }

                    ReportViewer0.LocalReport.EnableExternalImages = true;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    dtsE.Tables.RemoveAt(0);    //Eliminamos la tabla que crea por defecto
                                                //DataTable dtCopy = dt.Copy();
                    dtsE.Tables.Add(dt.Copy());   //Añadimos la tabla que acabamos de crear dtlistacamion

                    //Limpio los enlaces del ReportViewer 
                    //Ahora abre menu 9
                    //PNreportLista.Visible = true;
                    ReportViewer0.LocalReport.DataSources.Clear();
                    //ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource("DtJornalHora", dtsE.Tables[0]));
                    ReportViewer0.LocalReport.DataSources.Add(new ReportDataSource(ZDataSet, dtsE.Tables[0]));
                }

                this.Session["Menu"] = "9";
                Carga_Menus();
                ReportViewer0.DataBind();

                ReportViewer0.LocalReport.Refresh();    //Actualizamos el report
                ReportViewer0.Visible = true;
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("PrintReport --> " + ex.Message);
                Variables.Error = ex.Message;
                TextAlerta.Text = ex.Message;
                alerta.Visible = true;
            }

        }

        protected void PrintNomina_Click(object sender, EventArgs e)
        {
            CreaNomina("Todos", this.Session["UltimaConsulta"].ToString(), "");
        }

        private void CreaNomina(string CodEmpleado, string Fecha, string Centro)
        {
            string SQL = "";
            try
            {
                DataTable dt = null;
                DataTable dtA = null;
                string filtros = "";
                int rUDS = 0;
                SQL = " SELECT COUNT(COD_EMPLEADO) FROM REC_CALENDARIO";
                rUDS = Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null));

                SQL = "CENTRO";
                Carga_FiltrosGral(SQL);
                filtros = this.Session["Filtro"].ToString();
                if(filtros != "") { Centro = TxtCentro.Text; }

                if (rUDS == 0 || rUDS < 32)
                {}
                else
                {
                    PrintNominas(CodEmpleado, Centro);
                    return;
                }

                //else if ( rUDS > 32 && CodEmpleado == "Todos")
                //{
                //    PrintNominas(CodEmpleado, Centro);
                //    return;
                //}
                //else if (rUDS < 32 && rUDS > 1 && CodEmpleado == "Todos")
                //{
                //    PrintNominas(CodEmpleado, Centro);
                //    return;
                //}
                //else
                //{
                //    SQL = "DELETE FROM REC_CALENDARIO ";
                //    DBHelper.ExecuteNonQuery(SQL);
                //}

                ////Primero obtenemos el día actual
                //DateTime date = Convert.ToDateTime(Fecha);

                ////Asi obtenemos el primer dia del mes actual
                //DateTime startDate = new DateTime(date.Year, date.Month, 1);
                ////Y de la siguiente forma obtenemos el ultimo dia del mes
                ////agregamos 1 mes al objeto anterior y restamos 1 día.
                //DateTime stopDate = startDate.AddMonths(1).AddDays(-1);
                ////DateTime startDate = DateTime.Parse("1/1/2009"); 
                ////DateTime stopDate = DateTime.Parse("12/31/2009");

                //Preparo la plantilla
                if (CodEmpleado == "Todos" || CodEmpleado == "Total")
                {
                    //-------------------------------------------------------------------------------------------------------------------------
                    //-------------------------------------------------------------------------------------------------------------------------
                    //-------------------------------------------------------------------------------------------------------------------------
                    SQL = "COD_EMPLEADO, NOMBRE, APELLIDOS ,CENTRO";

                    Carga_FiltrosGral(SQL);
                    filtros = this.Session["Filtro"].ToString();

                    SQL = " SELECT COD_EMPLEADO, NOMBRE, APELLIDOS ,CENTRO";
                    SQL += " FROM REC_EMPLEADO ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    SQL += " ORDER BY COD_EMPLEADO ";

                    dt = Main.BuscaLote(SQL).Tables[0];
                    foreach (DataRow filas in dt.Rows)
                    {
                        DateTime date = Convert.ToDateTime(Fecha);
                        DateTime startDate = new DateTime(date.Year, date.Month, 1);
                        DateTime TimeDay = new DateTime(date.Year, date.Month, 1);
                        DateTime stopDate = startDate.AddMonths(1).AddDays(-1);
                        int interval = 0;
                        //INSERTA EMPLEADO EN CALENDARIO
                        while ((startDate = startDate.AddDays(interval)) <= stopDate)
                        {
                            // crea el registro de cada dia
                            SQL = " INSERT INTO REC_CALENDARIO (FECHA, COD_EMPLEADO, NOMBRE, APELLIDOS) " ; //, ENVASES, CANTIDAD, PLANTAS, TOTALCORTE, ACTIVIDAD, HORAS, TOTALJORNADA, TOTALDIA, VIVIENDA, TOTALES) ";
                            SQL += " VALUES ('" + startDate + "','" + filas["COD_EMPLEADO"].ToString() + "','" + filas["NOMBRE"].ToString() + "','" + filas["APELLIDOS"].ToString() + "') ";
                            DBHelper.ExecuteNonQuery(SQL);
                            interval += 1;
                            startDate = TimeDay;
                        }
                    }
                }
                else
                {
                    SQL = " SELECT COD_EMPLEADO, NOMBRE, APELLIDOS ,CENTRO FROM REC_EMPLEADO WHERE COD_EMPLEADO = '" + CodEmpleado + "' ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                    foreach (DataRow filas in dt.Rows)
                    {
                        DateTime date = Convert.ToDateTime(Fecha);
                        DateTime startDate = new DateTime(date.Year, date.Month, 1);
                        DateTime TimeDay = new DateTime(date.Year, date.Month, 1);
                        DateTime stopDate = startDate.AddMonths(1).AddDays(-1);
                        int interval = 0;
                        //INSERTA DE EMPLEADO EL REGISTRO DE CADA DIS EN CALENDARIO
                        while ((startDate = startDate.AddDays(interval)) <= stopDate)
                        {
                            // crea el registro de cada dia
                            SQL = " INSERT INTO REC_CALENDARIO (FECHA, COD_EMPLEADO, NOMBRE, APELLIDOS) "; //, ENVASES, CANTIDAD, PLANTAS, TOTALCORTE, ACTIVIDAD, HORAS, TOTALJORNADA, TOTALDIA, VIVIENDA, TOTALES) ";
                            SQL += " VALUES ('" + startDate + "','" + filas["COD_EMPLEADO"].ToString() + "','" + filas["NOMBRE"].ToString() + "','" + filas["APELLIDOS"].ToString() + "') ";
                            DBHelper.ExecuteNonQuery(SQL);
                            interval += 1;
                            startDate = TimeDay;
                        }
                    }
                }

                // INSERTA PRODUCCION EN CALENDARIO
                SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FECHA_EMPLEADOS,  DESCRCAJAS, CAJAS,  B.TAREA, PLANTAS, IMPORTE "; //, A.TABLET, B.ZONA ";
                Carga_FiltrosGral(SQL);
                filtros = this.Session["Filtro"].ToString();

                SQL = " SELECT DISTINCT(A.COD_EMPLEADO) AS COD_EMPLEADO, A.COSTEVIVIENDA,	  ";
                SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";

                SQL += " STUFF((SELECT '| ' + DESCRENVASE + ' ' ";
                SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
                SQL += " GROUP BY DESCRENVASE ";
                SQL += " FOR XML PATH('')),1,1,'') AS ENVASES, ";

                SQL += " STUFF((SELECT '| ' +  CONVERT(VARCHAR(255), SUM(REC_PRODUCCION.MARCAENVASE)) + ' ' ";
                SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
                SQL += " GROUP BY DESCRENVASE ";
                SQL += " FOR XML PATH('')),1,1,'') AS CANTIDAD, ";

                SQL += " (SELECT SUM(REC_PRODUCCION.PLANTAS) AS CAJAS  FROM REC_PRODUCCION  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS) AS PLANTAS, ";

                //ISNULL(TOTALJORNADA, 0)

                SQL += " ISNULL((SELECT CONVERT(DECIMAL(8, 2), SUM(REC_PRODUCCION.PLANTAS * REC_TARIFAS.TARIFA)) ";
                SQL += " FROM REC_PRODUCCION, REC_TARIFAS  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO    AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  AND REC_TARIFAS.TIPO = 'DESTAJO' ";
                SQL += " AND REC_PRODUCCION.ENVASE = REC_TARIFAS.SUBTIPO   GROUP BY REC_PRODUCCION.COD_EMPLEADO ), 0) AS IMPORTE ";


                SQL += "  FROM REC_PRODUCCION A  ";
                SQL += "  INNER JOIN REC_EMPLEADO Z ON A.COD_EMPLEADO = Z.COD_EMPLEADO "; // AND Z.BUSQUEDA = 1  ";
                if (CodEmpleado == "Todos" || CodEmpleado == "Total")
                {
                    SQL += "  WHERE A.COD_EMPLEADO IS NOT NULL  ";
                }
                else
                {
                    SQL += "  WHERE A.COD_EMPLEADO = '" + CodEmpleado + "' ";
                }

                if (filtros != "")
                {
                    SQL += filtros;
                }
                SQL += "  GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.MARCAENVASE , A.COSTEVIVIENDA ";
                if (ElOrden != "")
                {
                    SQL += ElOrden;
                }
                else
                {
                    SQL += " ORDER BY A.COD_EMPLEADO, FECHA_EMPLEADOS, A.COSTEVIVIENDA ";
                }




                dtA = Main.BuscaLote(SQL).Tables[0];
                //ACTUALIZA CALENDARIO CON PRODUCCION
                foreach (DataRow filas in dtA.Rows)
                {
                    //if(filas["COSTEVIVIENDA"].ToString().Replace(",", ".") != "0.00")
                    //{
                    //    string aa = filas["COSTEVIVIENDA"].ToString().Replace(",", ".");
                    //}
                    SQL = " UPDATE REC_CALENDARIO SET ENVASES = '" + filas["ENVASES"].ToString() + "', ";
                    SQL += " CANTIDAD = '" + filas["CANTIDAD"].ToString() + "',  ";
                    SQL += " PLANTAS = " + filas["PLANTAS"].ToString() + ",  ";
                    SQL += " TOTALCORTE = " + filas["IMPORTE"].ToString().Replace(",", ".") + "  ";
                    //SQL += " VIVIENDA = " + filas["COSTEVIVIENDA"].ToString().Replace(",", ".") + "  ";
                    SQL += " WHERE FECHA = '" + filas["FECHA_EMPLEADOS"].ToString() + "'  ";
                    SQL += " AND COD_EMPLEADO = '" + filas["COD_EMPLEADO"].ToString() + "'  ";
                    DBHelper.ExecuteNonQuery(SQL);
                    //SQL = " UPDATE REC_CALENDARIO SET  VIVIENDA = " + filas["COSTEVIVIENDA"].ToString().Replace(",", ".") + "  ";
                    //SQL += " WHERE COD_EMPLEADO = '" + filas["COD_EMPLEADO"].ToString() + "'  ";
                    //DBHelper.ExecuteNonQuery(SQL);

                }

                //TRABAJOS EN CALENDARIO


                SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO ";

                Carga_FiltrosGral(SQL);
                filtros = this.Session["Filtro"].ToString();

                SQL = " SELECT A.COD_EMPLEADO , ";
                SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
                SQL += " A.HORA_EMPLEADO,  RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA, ";
                SQL += " A.ZHORAS, ";
                SQL += " A.ZTOTALMINUTOS, ";
                SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
                SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
                if (CodEmpleado == "Todos" || CodEmpleado == "Total")
                {
                    SQL += "  WHERE A.COD_EMPLEADO IS NOT NULL  ";
                }
                else
                {
                    SQL += "  WHERE A.COD_EMPLEADO = '" + CodEmpleado + "' ";
                }
                SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
                SQL += " AND B.TIPO = 'JORNAL' ";
                SQL += " AND C.CATEGORIA = B.SUBTIPO ";

                if (filtros != "")
                {
                    SQL += filtros;
                }
                if (ElOrden != "")
                {
                    SQL += ElOrden;
                }
                else
                {
                    SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS,  A.HORA_EMPLEADO ";
                }

                dtA = Main.BuscaLote(SQL).Tables[0];
                //ACTUALIZA CALENDARIO TRABAJOS
                foreach (DataRow filas in dtA.Rows)
                {
                    SQL = " UPDATE REC_CALENDARIO SET HORAS ='" + filas["ZHORAS"].ToString() + "', ";
                    SQL += " TOTALJORNADA = " + filas["IMPORTE"].ToString().Replace(",", ".") + ",  ";
                    SQL += " TOTALMINUTOS = " + filas["ZTOTALMINUTOS"].ToString() + "  ";
                    SQL += " WHERE FECHA = '" + filas["FECHA_EMPLEADOS"].ToString() + "'  ";
                    SQL += " AND COD_EMPLEADO = '" + filas["COD_EMPLEADO"].ToString() + "'  ";
                    SQL += " AND TOTALCORTE <> 0  ";

                    DBHelper.ExecuteNonQuery(SQL);
                }

                //JORNADAS Ahora comprueba si no hay tareas y añade jornal a ese dia

                SQL = " UPDATE REC_CALENDARIO SET TOTALJORNADA = 0 WHERE TOTALJORNADA is null ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE REC_CALENDARIO SET TOTALCORTE = 0 WHERE TOTALCORTE is null ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " COD_EMPLEADO ,FECHA_JORNADA , TRANSCURRIDO , TOTALIMPORTE";//, IMPORTEMINUTOS, TOTALMINUTOS

                Carga_FiltrosGral(SQL);
                filtros = this.Session["Filtro"].ToString();


                SQL = " SELECT DISTINCT(FORMAT(FECHA_JORNADA, 'dd-MM-yyyy')) AS FECHA_JORNADA, COD_EMPLEADO , ";
                SQL += " SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, TOTALTIEMPO, ''), 114),1,5) AS  TRANSCURRIDO, ";
                SQL += " TOTALIMPORTE, TOTALTIEMPO ";
                SQL += " FROM REC_JORNADA ";
                if (CodEmpleado == "Todos" || CodEmpleado == "Total")
                {
                    SQL += "  WHERE COD_EMPLEADO IS NOT NULL  ";
                }
                else
                {
                    SQL += "  WHERE COD_EMPLEADO = '" + CodEmpleado + "' ";
                }
                SQL += "  GROUP BY COD_EMPLEADO, FECHA_JORNADA, TOTALIMPORTE, TOTALTIEMPO   ";
                if (ElOrden != "")
                {
                    SQL += ElOrden;
                }
                else
                {
                    SQL += " ORDER BY COD_EMPLEADO, FECHA_JORNADA  ";
                }

                dtA = Main.BuscaLote(SQL).Tables[0];





                //ACTUALIZA CALENDARIO JORNADAS
                //Debug
                foreach (DataRow filas in dtA.Rows)
                {
                    string Miro = filas["TOTALTIEMPO"].ToString();
                    if(filas["TRANSCURRIDO"].ToString() != "")
                    {
                        SQL = " UPDATE REC_CALENDARIO SET HORAS ='" + filas["TRANSCURRIDO"].ToString() + "', ";
                    }
                    else
                    {
                        SQL = " UPDATE REC_CALENDARIO SET HORAS ='', ";
                    }
                    if (filas["TOTALTIEMPO"].ToString() != "")
                    {
                        SQL += " TOTALMINUTOS = " + filas["TOTALTIEMPO"].ToString().Replace(",", ".") + ",  ";
                    }
                    else
                    {
                        SQL += " TOTALMINUTOS = 0 ,  ";
                    }
                    if (filas["TOTALIMPORTE"].ToString() != "")
                    {
                        SQL += " TOTALJORNADA = " + filas["TOTALIMPORTE"].ToString().Replace(",", ".") + "  ";
                    }
                    else
                    {
                        SQL += " TOTALJORNADA = 0  ";
                    }
                    SQL += " WHERE FECHA = '" + filas["FECHA_JORNADA"].ToString() + "'  ";
                    SQL += " AND COD_EMPLEADO = '" + filas["COD_EMPLEADO"].ToString() + "'  ";
                    //SQL += " AND (HORAS is null OR HORAS = '00:00')   ";
                    //SQL += " AND TOTALJORNADA = 0.00  ";
                    SQL += " AND TOTALCORTE = 0.00  ";

                    string a = Main.Ficherotraza(SQL);

                    DBHelper.ExecuteNonQuery(SQL);
                }

                SQL = " UPDATE REC_CALENDARIO SET TOTALDIA = ISNULL(TOTALCORTE, 0) + ISNULL(TOTALJORNADA, 0) ";
                DBHelper.ExecuteNonQuery(SQL);


                //SQL = " UPDATE A  SET A.VIVIENDA = C.COSTEVIVIENDA  FROM REC_CALENDARIO A,(SELECT COD_EMPLEADO, COSTEVIVIENDA ";
                //SQL += " FROM REC_EMPLEADO ";
                //SQL += " GROUP BY COD_EMPLEADO, COSTEVIVIENDA) C ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                ////SQL += " AND A.VIVIENDA IS NULL ";
                //DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE A ";
                SQL += " SET A.VIVIENDA = C.COSTEVIVIENDA ";
                SQL += " FROM REC_CALENDARIO A,(SELECT COD_EMPLEADO, COSTEVIVIENDA ";
                SQL += "                     FROM REC_EMPLEADO ";
                SQL += "                     GROUP BY COD_EMPLEADO, COSTEVIVIENDA ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                //SQL += " AND A.VIVIENDA IS NULL ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE REC_CALENDARIO SET VIVIENDA = 0 WHERE VIVIENDA IS NULL ";
                DBHelper.ExecuteNonQuery(SQL);

                SQL = " UPDATE A ";
                SQL += " SET A.TOTALMINUTOS = C.TOTAL  ";
                SQL += " FROM REC_CALENDARIO A,(SELECT COD_EMPLEADO, SUM(ISNULL(TOTALMINUTOS, 0)) AS TOTAL  ";
                SQL += "                        FROM REC_CALENDARIO  ";
                SQL += "                        GROUP BY COD_EMPLEADO ) C  ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";
                DBHelper.ExecuteNonQuery(SQL);



                //SQL = " UPDATE A ";
                //SQL += " SET A.TOTALMINUTOS = C.TOTAL  ";
                //SQL += " FROM REC_CALENDARIO A,(SELECT COD_EMPLEADO, SUM(ISNULL(TOTALMINUTOS, 0)) AS TOTAL  ";
                //SQL += "                        FROM REC_CALENDARIO  ";
                //SQL += "                        GROUP BY COD_EMPLEADO ) C  ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO  ";
                //DBHelper.ExecuteNonQuery(SQL);


                //Ahora actualizo según Tenga datos REC_PRODUCCION, REC_TRABAJOS o ambos 

                SQL = " UPDATE A ";
                SQL += "  SET A.NHORAS = C.NHORAS, ";
                SQL += "  A.XHORAS = C.XHORAS, ";
                SQL += "  A.VNHORAS = C.VNHORAS, ";
                SQL += "  A.VXHORAS = C.VXHORAS, ";
                SQL += "  A.TIMPNHORAS = C.TIMPNHORAS, ";
                SQL += "  A.TIMPXHORAS = C.TIMPXHORAS ";
                SQL += "  FROM REC_CALENDARIO A,(SELECT E.COD_EMPLEADO, ";
                SQL += "                        E.FECHA_JORNADA, ";
                SQL += "                        E.NHORAS, ";
                SQL += "                        E.XHORAS, ";
                SQL += "                        E.VNHORAS, ";
                SQL += "                        E.VXHORAS,";
                SQL += "                        E.TIMPNHORAS, ";
                SQL += "                        E.TIMPXHORAS ";
                SQL += "                        FROM REC_JORNADA E, REC_CALENDARIO B ";
                SQL += "                        WHERE B.COD_EMPLEADO = E.COD_EMPLEADO  ";
                SQL += "                        AND B.TOTALCORTE = 0.0  ";
                SQL += "                        AND B.TOTALJORNADA  = 0.0 ) C ";
                SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND A.FECHA = C.FECHA_JORNADA ";
                DBHelper.ExecuteNonQuery(SQL);


                //SQL = " UPDATE A ";
                //SQL += "  SET A.NHORAS = C.NHORAS, ";
                //SQL += "  A.XHORAS = C.XHORAS, ";
                //SQL += "  A.TIMPNHORAS = C.TIMPNHORAS, ";
                //SQL += "  A.TIMPXHORAS = C.TIMPXHORAS ";
                //SQL += "  FROM REC_CALENDARIO A,(SELECT E.COD_EMPLEADO, ";
                //SQL += "                        E.FECHA_EMPLEADOS, ";
                //SQL += "                        SUM(ISNULL(E.NHORAS, 0)) AS NHORAS, ";
                //SQL += "                        SUM(ISNULL(E.XHORAS, 0)) AS XHORAS, ";
                //SQL += "                        SUM(ISNULL(E.TIMPNHORAS, 0)) AS TIMPNHORAS, ";
                //SQL += "                        SUM(ISNULL(E.TIMPXHORAS, 0)) AS TIMPXHORAS ";
                //SQL += "                        FROM REC_TAREAS E, REC_CALENDARIO B ";
                //SQL += "                        WHERE B.COD_EMPLEADO = E.COD_EMPLEADO  ";
                ////SQL += "                        AND = E.COD_EMPLEADO  ";
                //SQL += "                        GROUP BY E.COD_EMPLEADO, E.FECHA_EMPLEADOS ) C ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                //SQL += " AND A.FECHA = C.FECHA_EMPLEADOS ";
                //DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE A ";
                //SQL += "  SET A.JNHORAS = C.NHORAS, ";
                //SQL += "  A.JXHORAS = C.XHORAS, ";
                //SQL += "  A.JTIMPNHORAS = C.TIMPNHORAS, ";
                //SQL += "  A.JTIMPXHORAS = C.TIMPXHORAS ";
                //SQL += "  FROM REC_CALENDARIO A,(SELECT COD_EMPLEADO, ";
                //SQL += "                        FECHA_JORNADA, ";
                //SQL += "                        SUM(ISNULL(NHORAS, 0)) AS NHORAS, ";
                //SQL += "                        SUM(ISNULL(XHORAS, 0)) AS XHORAS, ";
                //SQL += "                        SUM(ISNULL(TIMPNHORAS, 0)) AS TIMPNHORAS, ";
                //SQL += "                        SUM(ISNULL(TIMPXHORAS, 0)) AS TIMPXHORAS ";
                //SQL += "                        FROM REC_JORNADA ";
                //SQL += "                        GROUP BY COD_EMPLEADO, FECHA_JORNADA ) C ";
                //SQL += " WHERE A.COD_EMPLEADO = C.COD_EMPLEADO ";
                //SQL += " AND A.FECHA = C.FECHA_JORNADA ";
                //DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE REC_CALENDARIO SET  VNHORAS = SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, NHORAS, ''), 114), 1, 5),";
                //SQL +=  "  VXHORAS = SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, XHORAS, ''), 114), 1, 5) ";
                //DBHelper.ExecuteNonQuery(SQL);

                //SQL = " UPDATE REC_CALENDARIO SET  JVNHORAS = SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, JNHORAS, ''), 114), 1, 5),";
                //SQL += "  JVXHORAS = SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, JXHORAS, ''), 114), 1, 5) ";
                //DBHelper.ExecuteNonQuery(SQL);

                if (CodEmpleado != "Total")
                {
                    PrintNominas(CodEmpleado, Centro);
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("CreaNomina --> " + ex.Message + " --> " + SQL);
                return;
            }
        }

        protected void BtGralConsultaMin_Click(object sender, EventArgs e)
        {
            Carga_tablaEmpleados();
            Carga_panelTareas();
            Carga_tablaProduccion();
            Carga_tablaJornada();
            Carga_Jornal_Horas();
            Carga_Jornal_Nominas();
            Carga_Destajo_Nomina();
            Carga_Nomina_resumen();
            Carga_ProddiaImporte();
            Carga_Trabajos();
            //string SQL = " DELETE FROM REC_CALENDARIO ";
            //DBHelper.ExecuteNonQuery(SQL);


        }

        //protected void DrFiltro1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DropDownList Dr = (DropDownList)sender;
        //    HtmlButton btn = new HtmlButton();

        //    if (Dr.ID == "DRgvEmpleado") { btn = (HtmlButton)BtnFilEmpleado; }
        //    if (Dr.ID == "DrrowProduccion") { btn = (HtmlButton)BtnFilProduccion; }
        //    if (Dr.ID == "DrConsultaJornada") { btn = (HtmlButton)BtnFilJornada; }
        //    if (Dr.ID == "DrrowJornalHora") { btn = (HtmlButton)BtnFilJornalHora; }
        //    if (Dr.ID == "DrrowJornalNomina") { btn = (HtmlButton)BtnfilJornalNomina; }
        //    if (Dr.ID == "DrrowProdDiaImporte") { btn = (HtmlButton)BtnFilProddiaImporte; }
        //    if (Dr.ID == "DrrowDestajoNimina") { btn = (HtmlButton)BtnFilDestajoNomina; }
        //    if (Dr.ID == "DrrowResumenNomina") { btn = (HtmlButton)BtnConsResumenNomina; }

        //    if (Dr.SelectedItem.Value == "Ninguno")
        //    {
        //        Dr.Attributes.Add("style", "background-color:#fff");
        //        this.Session["Filtro"] = "0";
        //    }
        //    else
        //    {
        //        Dr.Attributes.Add("style", "background-color:#e6f2e1");
        //    }
        //    //this.Session["Delegate"] = "1";
        //    //BtContiene_Click(btn, e);
        //}
        protected void DRJornalHora1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Jornal_Horas();   
        }

        protected void DRDestajoNomina1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Destajo_Nomina();
        }

        protected void DrEmpleado1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tablaEmpleados();
        }

        protected void DRJornalNomina1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Jornal_Nominas();
        }
        protected void DrProdImpDia1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_ProddiaImporte();
        }
        protected void DRResumenNomina1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Nomina_resumen();
        }
        protected void DrProduccion1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tablaProduccion();
        } 
        protected void DrJornada1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_tablaJornada(); 
        }
        protected void DRTrabajo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carga_Trabajos();
        }

        //private void OrdenProddiaImporte()
        //{
        //    ElOrdenProddiaImporte = "";
        //    DrProdImpDia1.BackColor = Color.FromName("#ffffff");
        //    DrProdImpDia2.BackColor = Color.FromName("#ffffff");

        //    if (DrProdImpDia1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenProddiaImporte == "")
        //        {
        //            if (ElOrdenProddiaImporte.Contains(DrProdImpDia1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenProddiaImporte = " ORDER BY " + DrProdImpDia1.SelectedItem.Text;
        //            }
        //        }
        //        DrProdImpDia1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrProdImpDia2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenProddiaImporte == "")
        //        {
        //            ElOrdenProddiaImporte = " ORDER BY " + DrProdImpDia2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenProddiaImporte.Contains(DrProdImpDia2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenProddiaImporte += ", " + DrProdImpDia2.SelectedItem.Text;
        //            }
        //        }
        //        DrProdImpDia2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}


        //private void OrdenResumenNomina()
        //{
        //    ElOrdenResumenNomina = "";
        //    DRResumenNomina1.BackColor = Color.FromName("#ffffff");
        //    DRResumenNomina2.BackColor = Color.FromName("#ffffff");

        //    if (DRResumenNomina1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenResumenNomina == "")
        //        {
        //            if (ElOrdenResumenNomina.Contains(DRResumenNomina1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenResumenNomina = " ORDER BY " + DRResumenNomina1.SelectedItem.Text;
        //            }
        //        }
        //        DRResumenNomina1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DRResumenNomina2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenResumenNomina == "")
        //        {
        //            ElOrdenResumenNomina = " ORDER BY " + DRResumenNomina2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenResumenNomina.Contains(DRResumenNomina2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenResumenNomina += ", " + DRResumenNomina2.SelectedItem.Text;
        //            }
        //        }
        //        DRResumenNomina2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}



        //private void OrdenEmpleaado()
        //{
        //    ElOrdenEmpleado = "";
        //    DrEmpleado1.BackColor = Color.FromName("#ffffff");
        //    DrEmpleado2.BackColor = Color.FromName("#ffffff");

        //    if (DrEmpleado1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenEmpleado == "")
        //        {
        //            if (ElOrdenEmpleado.Contains(DrEmpleado1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenEmpleado = " ORDER BY " + DrEmpleado1.SelectedItem.Text;
        //            }
        //        }
        //        DrEmpleado1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrEmpleado2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenEmpleado == "")
        //        {
        //            ElOrdenEmpleado = " ORDER BY " + DrEmpleado2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenEmpleado.Contains(DrEmpleado2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenEmpleado += ", " + DrEmpleado2.SelectedItem.Text;
        //            }
        //        }
        //        DrEmpleado2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}


        //private void OrdenJornal_Horas()
        //{
        //    ElOrdenJornal_Horas = "";
        //    DRJornalHora1.BackColor = Color.FromName("#ffffff");
        //    DRJornalHora2.BackColor = Color.FromName("#ffffff");

        //    if (DRJornalHora1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornal_Horas == "")
        //        {
        //            if (ElOrdenJornal_Horas.Contains(DRJornalHora1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornal_Horas = " ORDER BY " + DRJornalHora1.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRJornalHora1.SelectedItem.Text);
        //        DRJornalHora1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DRJornalHora2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornal_Horas == "")
        //        {
        //            ElOrdenJornal_Horas = " ORDER BY " + DRJornalHora2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenJornal_Horas.Contains(DRJornalHora2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornal_Horas += ", " + DRJornalHora2.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRJornalHora2.SelectedItem.Text);
        //        DRJornalHora2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}

        //private void OrdenDestajo_Nomina()
        //{
        //    ElOrdenDestajo_Nomina = "";
        //    DRDestajoNomina1.BackColor = Color.FromName("#ffffff");
        //    DRDestajoNomina2.BackColor = Color.FromName("#ffffff");

        //    if (DRDestajoNomina1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenDestajo_Nomina == "")
        //        {
        //            if (ElOrdenDestajo_Nomina.Contains(DRDestajoNomina1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenDestajo_Nomina = " ORDER BY " + DRDestajoNomina1.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRDestajoNomina1.SelectedItem.Text);
        //        DRDestajoNomina1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DRDestajoNomina2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenDestajo_Nomina == "")
        //        {
        //            ElOrdenDestajo_Nomina = " ORDER BY " + DRDestajoNomina2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenDestajo_Nomina.Contains(DRDestajoNomina2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenDestajo_Nomina += ", " + DRDestajoNomina2.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRDestajoNomina2.SelectedItem.Text);
        //        DRDestajoNomina2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}

        //private void OrdenJornal_Nominas()
        //{
        //    ElOrdenJornal_Nominas = "";
        //    DRJornalNomina1.BackColor = Color.FromName("#ffffff");
        //    DRJornalNomina2.BackColor = Color.FromName("#ffffff");

        //    if (DRJornalNomina1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornal_Nominas == "")
        //        {
        //            if (ElOrdenJornal_Nominas.Contains(DRJornalNomina1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornal_Nominas = " ORDER BY " + DRJornalNomina1.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRJornalNomina1.SelectedItem.Text);
        //        DRJornalNomina1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DRJornalNomina2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornal_Nominas == "")
        //        {
        //            ElOrdenJornal_Nominas = " ORDER BY " + DRJornalNomina2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenJornal_Nominas.Contains(DRJornalNomina2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornal_Nominas += ", " + DRJornalNomina2.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DRJornalNomina2.SelectedItem.Text);
        //        DRJornalNomina2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}

        //private void OrdenJornada()
        //{
        //    ElOrdenJornada = "";
        //    DrJornada1.BackColor = Color.FromName("#ffffff");
        //    DrJornada2.BackColor = Color.FromName("#ffffff");
  
        //    if (DrJornada1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornada == "")
        //        {
        //            if (ElOrdenJornada.Contains(DrJornada1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornada = " ORDER BY " + DrJornada1.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DrJornada1.SelectedItem.Text);
        //        DrJornada1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrJornada2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenJornada == "")
        //        {
        //            ElOrdenJornada = " ORDER BY " + DrJornada2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenJornada.Contains(DrJornada2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenJornada += ", " + DrJornada2.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimalLista(DrJornada2.SelectedItem.Text);
        //        DrJornada2.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    ////Carga_tablaEmpleados();
        //}

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

        //private void CompruebaCampoDecimalLista(string combo)
        //{
        //    if (combo == "UDSENCARGA" || combo == "UDSENCARGA")
        //    {
        //        if (ElOrdenJornada == "")
        //        {
        //            ElOrdenJornada = " ORDER BY CONVERT(DECIMAL (10,3), " + combo + ")";
        //        }
        //        else
        //        {
        //            ElOrdenJornada += ", CONVERT(DECIMAL (10,3), " + combo + ")";
        //        }
        //    }
        //    //else if (combo == "POSICIONCAMION" || combo == "NUMERO" || combo == "LINEA" || combo == "NUMERO_LINEA")
        //    //{
        //    //    if (ElOrdenJornada == "")
        //    //    {
        //    //        ElOrdenJornada = " ORDER BY CONVERT(INT, " + combo + ")";
        //    //    }
        //    //    else
        //    //    {
        //    //        ElOrdenJornada += ", CONVERT(INT, " + combo + ")";
        //    //    }
        //    //}
        //    else
        //    {
        //        if (ElOrdenJornada == "")
        //        {
        //            ElOrdenJornada = " ORDER BY " + combo + " ";
        //        }
        //        else
        //        {
        //            ElOrdenJornada += ", " + combo + " ";
        //        }
        //    }
        //}

        //private void OrdenProduccion()
        //{
        //    ElOrdenProduccion = "";
        //    DrProduccion1.BackColor = Color.FromName("#ffffff");
        //    DrProduccion2.BackColor = Color.FromName("#ffffff");

        //    if (DrProduccion1.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenProduccion == "")
        //        {
        //            if (ElOrdenProduccion.Contains(DrProduccion1.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenProduccion = " ORDER BY " + DrProduccion1.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimal(DrProduccion1.SelectedItem.Text);
        //        DrProduccion1.BackColor = Color.FromName("#fcf5d2");
        //    }

        //    if (DrProduccion2.SelectedItem.Value != "Ninguno")
        //    {
        //        if (ElOrdenProduccion == "")
        //        {
        //            ElOrdenProduccion = " ORDER BY " + DrProduccion2.SelectedItem.Text;
        //        }
        //        else
        //        {
        //            if (ElOrdenProduccion.Contains(DrProduccion2.SelectedItem.Text))
        //            { }
        //            else
        //            {
        //                ElOrdenProduccion += ", " + DrProduccion2.SelectedItem.Text;
        //            }
        //        }
        //        CompruebaCampoDecimal(DrProduccion2.SelectedItem.Text);
        //        DrProduccion2.BackColor = Color.FromName("#fcf5d2");
        //    }

 
        //    ////Carga_tablaProduccion();
        //}

        protected void DrTransportista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TxtTransportista.Text = DrTransportista.SelectedItem.Text;
        }

        protected void gvEmpleado_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvEmpleado.PageIndex = e.NewPageIndex;
            Carga_tablaEmpleados();
        }

        
        protected void gvpanelTareas_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvpanelTareas.PageIndex = e.NewPageIndex;
            Carga_panelTareas();
        }

        protected void gvProdImpDia_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvProdImpDia.PageIndex = e.NewPageIndex;
            Carga_ProddiaImporte();
        }

        protected void gvJornalHora_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvJornalHora.PageIndex = e.NewPageIndex;
            Carga_Jornal_Horas();
        }

        protected void gvJornalNomina_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvJornalNomina.PageIndex = e.NewPageIndex;
            Carga_Jornal_Nominas();
        }

        protected void gvDestajoNomina_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvDestajoNomina.PageIndex = e.NewPageIndex;
            Carga_Destajo_Nomina();
        }

        
        protected void gvTrabajos_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvTrabajos.PageIndex = e.NewPageIndex;
            Carga_Trabajos();
        }

        protected void gvResumenNomina_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvResumenNomina.PageIndex = e.NewPageIndex;
            Carga_Nomina_resumen();
        }

        protected void gvProduccion_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvProduccion.PageIndex = e.NewPageIndex;
            Carga_tablaProduccion();
        }

        protected void gvJornada_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvJornada.PageIndex = e.NewPageIndex;
            Carga_tablaJornada();
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
        


        protected void gvProduccion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvProduccion.Rows[e.RowIndex];
            string miro = gvProduccion.DataKeys[e.RowIndex].Value.ToString();
            gvProduccion.EditIndex = -1;
            Carga_tablaProduccion();
            gvProduccion.DataBind();


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

            //Carga_tablaProduccion();
            //Carga_tablaEmpleados();

            //gvProduccion.EditIndex = -1;

            ////DataTable dt = this.Session["MiConsulta"] as DataTable;
            ////gvProduccion.DataSource = dt;
            //gvProduccion.DataBind();



            ////DataTable dt = this.Session["MiConsulta"] as DataTable;
            ////gvProduccion.DataSource = dt;
            ////gvProduccion.DataBind();
        }



        protected void gvJornalNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvJornalNomina.Rows[e.RowIndex];
            string miro = gvJornalNomina.DataKeys[e.RowIndex].Value.ToString();
            gvJornalNomina.EditIndex = -1;
            gvJornalNomina.DataBind();
        }
        protected void gvDestajoNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvDestajoNomina.Rows[e.RowIndex];
            string miro = gvDestajoNomina.DataKeys[e.RowIndex].Value.ToString();
            gvDestajoNomina.EditIndex = -1;
            gvDestajoNomina.DataBind();
        }
        protected void gvResumenNomina_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvResumenNomina.Rows[e.RowIndex];
            string miro = gvResumenNomina.DataKeys[e.RowIndex].Value.ToString();
            gvResumenNomina.EditIndex = -1;
            gvResumenNomina.DataBind();
        }

        protected void gvTrabajos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvTrabajos.Rows[e.RowIndex];
            string miro = gvTrabajos.DataKeys[e.RowIndex].Value.ToString();
            gvTrabajos.EditIndex = -1;
            gvTrabajos.DataBind();
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

        }

        protected void gvJornada_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvJornada.Rows[e.RowIndex];
            string miro = gvJornada.DataKeys[e.RowIndex].Value.ToString();

            Carga_tablaEmpleados();
            gvJornada.EditIndex = -1;
            gvJornada.DataBind();

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

            //Carga_tablaProduccion();
            //Carga_tablaEmpleados();

            //gvJornada.EditIndex = -1;

            //gvJornada.DataBind();
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

        protected void gvProduccion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            //if (TxtNumero.Text == "")
            //{
            //    TextAlerta.Text = "Debe crear o editar previamente una órden de Cabecera para tratar con la lista de pedidos";
            //    alerta.Visible = true;
            //    return;
            //}
            GridViewRow row = gvProduccion.Rows[e.RowIndex];
            string miro = gvProduccion.DataKeys[e.RowIndex].Value.ToString();

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
                //TextBox txtValor = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOUdsPedidas") as TextBox);
                //miro = txtValor.Text;

                TextBox txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOUdsPedidas") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOUdsServidas") as TextBox);
                //txtBox = (TextBox)(row.Cells[11].Controls[0]);
                if (txtBox != null)
                {
                    rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOUdsCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[12].Controls[0]);
                if (txtBox != null)
                {
                    rUDSENCARGA = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }
                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOPendientes") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    rUDSPENDIENTES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }

                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOCargar") as TextBox);
                if (txtBox != null)
                {
                    rUDSACARGAR = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }

                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOTipo") as TextBox);
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

                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
                if (txtBox != null)
                {
                    rNUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                }


                txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOCajas") as TextBox);
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

                //txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
                ////txtBox = (TextBox)(row.Cells[15].Controls[0]);
                //if (txtBox != null)
                //{
                //    rNUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                //}
                //txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOSerie") as TextBox);
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
                    Variables.Error = "No se pueden cargar más unidades de las que quedan pendientes.";
                    return;
                }


                //string SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                //SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                //SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
                //SQL += " AND A.ID = B.ID " ;
                //SQL += " AND A.ID = " + miro;
                //SQL += " GROUP BY A.ID_CABECERA ";
                //Variables.Error = "";
                //Lberror.Text = SQL;

                
                //Lberror.Text += SQL + "1- gvProduccion_RowUpdating " + Variables.mensajeserver;
                //rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));// + rUDSACARGAR;
                //Lberror.Text += " 1- gvProduccion_RowUpdating " + Variables.mensajeserver;


                //SQL = "UPDATE ZCARGA_ORDEN set UDSPENDIENTES = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                //SQL += " UDSACARGAR = " + rUDSACARGAR.ToString().Replace(",", ".") + ", ";
                //SQL += " UDSENCARGA = " + rUDSENCARGA.ToString().Replace(",", ".") + ", ";
                //SQL += " NUMPALET = " + rNUMPALET.ToString().Replace(",", ".") + ", ";
                //SQL += " ID_CABECERA = " + TxtNumero.Text + ", ";
                //SQL += " CAJAS = '" + rUNIDADES + "', ";           
                //SQL += " ESTADO = 1 ";
                //SQL += " WHERE ID = " + miro;

                //Variables.Error = "";
                //Lberror.Text = SQL;

                //Lberror.Text += SQL + "1- gvProduccion_RowUpdating " + Variables.mensajeserver;
                //DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 1- gvProduccion_RowUpdating " + Variables.mensajeserver;
                



                Carga_tablaProduccion();

                gvProduccion.EditIndex = -1;
                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvProduccion.DataSource = dt;
                gvProduccion.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvProduccion --> " +  ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
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
            string rLATITUD = "";
            string rLONGITUD = "";
            string rOBSERVACIONES = "";
            string rESTADO = "";
            string Mira = "";

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

                if(rESTADO == "3")
                {
                    SQL = "UPDATE ZCARGA_LINEA set ESTADO = " + rESTADO + " ";
                    SQL += " WHERE ID_CABECERA = " + rID;

                    Carga_tablaEmpleados();
                    gvJornada.DataBind();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvJornalHora_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvJornalHora.Rows[e.RowIndex];
            string miro = gvJornalHora.DataKeys[e.RowIndex].Value.ToString();
        }

        protected void gvJornalNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvJornalNomina.Rows[e.RowIndex];
            string miro = gvJornalNomina.DataKeys[e.RowIndex].Value.ToString();
        }
        protected void gvDestajoNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvDestajoNomina.Rows[e.RowIndex];
            string miro = gvDestajoNomina.DataKeys[e.RowIndex].Value.ToString();
        }
        protected void gvResumenNomina_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvResumenNomina.Rows[e.RowIndex];
            string miro = gvResumenNomina.DataKeys[e.RowIndex].Value.ToString();
        }

        protected void gvTrabajos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvTrabajos.Rows[e.RowIndex];
            string miro = gvTrabajos.DataKeys[e.RowIndex].Value.ToString();
        }

        protected void gvpanelTareas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvpanelTareas.Rows[e.RowIndex];
            string miro = gvpanelTareas.DataKeys[e.RowIndex].Value.ToString();
        }
        

        protected void gvProdImpDia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvProdImpDia.Rows[e.RowIndex];
            string miro = gvProdImpDia.DataKeys[e.RowIndex].Value.ToString();
        }

        


        protected void gvJornada_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvJornada.Rows[e.RowIndex];
            string miro = gvJornada.DataKeys[e.RowIndex].Value.ToString();

            string rNumPalet = "";
            string rCantidad = "";
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

                TextBox txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLCarga") as TextBox);
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
                txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLPalet") as TextBox);
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
                txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLcamion") as TextBox);
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
                }
                txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[15].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACION = txtBox.Text;
                }
                txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLNumLinea") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rNUMEROLINEA = txtBox.Text;
                }
                txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("TabLCabecera") as TextBox);
                //txtBox = (TextBox)(row.Cells[2].Controls[0]);
                if (txtBox != null)
                {
                    rCABECERA = txtBox.Text;
                }

                //string SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = " + rPOSICION + ", ";
                //SQL += " OBSERVACIONES = '" + rOBSERVACION + "', ";
                //SQL += " NUMPALET = " + rNumPalet.Replace(",", ".") + ", ";
                //SQL += " UDSENCARGA = " + rCantidad.Replace(",", ".") + ", ";
                //SQL += " HASTA = 'ID_CABECERA + '|' + CLIENTEPROVEEDOR + '|' + NUMERO + '|' + LINEA + '|' + " + rPOSICION + "', ";
                //SQL += " ESTADO = 1 ";
                //SQL += " WHERE ID = " + miro;
                //SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                //SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                //Variables.Error = "";

                //Lberror.Text += SQL + "1- gvJornada_RowUpdating " + Variables.mensajeserver;
                //DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 1- gvJornada_RowUpdating " + Variables.mensajeserver;

                Carga_tablaEmpleados();

                gvJornada.EditIndex = -1;

                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvProduccion.DataSource = dt;
                gvJornada.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvJornada --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        
        protected void gvJornalHora_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvJornalHora.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvJornalHora.EditIndex = e.NewEditIndex;
        }

        protected void gvJornalNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvJornalNomina.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvJornalNomina.EditIndex = e.NewEditIndex;
        }
        protected void gvDestajoNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvDestajoNomina.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvDestajoNomina.EditIndex = e.NewEditIndex;
        }
        protected void gvResumenNomina_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvResumenNomina.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvResumenNomina.EditIndex = e.NewEditIndex;
        }


        protected void gvTrabajos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvTrabajos.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvTrabajos.EditIndex = e.NewEditIndex;
        }

        protected void gvpanelTareas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvpanelTareas.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvpanelTareas.EditIndex = e.NewEditIndex;
        }
        

        protected void gvProdImpDia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvProdImpDia.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvProdImpDia.EditIndex = e.NewEditIndex;
        }

        

        protected void gvProduccion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvProduccion.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvProduccion.EditIndex = e.NewEditIndex;

            Carga_tablaProduccion();
            //string carga = gvProduccion.Width.ToString();
            //int cuantos = gvProduccion.Rows[indice].Cells.Count - 2;
            //int Parcial = Convert.ToInt32(carga) - cuantos;

            //gvProduccion.Rows[indice].Cells[0].Enabled = false;
            gvProduccion.Rows[indice].Cells[1].Enabled = false;           
            gvProduccion.Rows[indice].Cells[2].Enabled = false;
            gvProduccion.Rows[indice].Cells[3].Enabled = false;
            gvProduccion.Rows[indice].Cells[4].Enabled = false;
            gvProduccion.Rows[indice].Cells[5].Enabled = false;
            gvProduccion.Rows[indice].Cells[6].Enabled = false;
            gvProduccion.Rows[indice].Cells[7].Enabled = false;
            gvProduccion.Rows[indice].Cells[8].Enabled = false;
            gvProduccion.Rows[indice].Cells[9].Enabled = false;
            gvProduccion.Rows[indice].Cells[10].Enabled = false;
            gvProduccion.Rows[indice].Cells[11].Enabled = false;
            gvProduccion.Rows[indice].Cells[12].Enabled = false;
            gvProduccion.Rows[indice].Cells[13].Enabled = false;
            gvProduccion.Rows[indice].Cells[18].Enabled = false;

            //gvProduccion.Rows[indice].Cells[1].Width = 40;
            //gvProduccion.Rows[indice].Cells[2].Width = 40;
            //gvProduccion.Rows[indice].Cells[3].Width = 40;
            //gvProduccion.Rows[indice].Cells[4].Width = 40;
            //gvProduccion.Rows[indice].Cells[5].Width = 40;
            //gvProduccion.Rows[indice].Cells[6].Width = 40;
            //gvProduccion.Rows[indice].Cells[7].Width = 40;
            //gvProduccion.Rows[indice].Cells[8].Width = 40;
            //gvProduccion.Rows[indice].Cells[9].Width = 40;
            //gvProduccion.Rows[indice].Cells[10].Width = 40;
            //gvProduccion.Rows[indice].Cells[11].Width = 40;
            //gvProduccion.Rows[indice].Cells[12].Width = 40;
            //gvProduccion.Rows[indice].Cells[13].Width = 40;
            //gvProduccion.Rows[indice].Cells[14].Width = 40;
            //gvProduccion.Rows[indice].Cells[15].Width = 40;
            //gvProduccion.Rows[indice].Cells[16].Width = 40;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvProduccion.DataSource = dt;
            //gvProduccion.DataBind();
        }


        protected void gvEmpleado_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if(this.Session["EstadoCabecera"].ToString() == "3") { return; }
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
                    if(combo.Items[i].Text == rID)
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

        protected void gvJornada_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvJornada.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //gvJornada.AutoResizeColumns();
            //gvJornada.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //gvJornada.AutoResizeColumns(DataGridViewAutoSizeColumnsMo‌​de.Fill);
            int indice = gvJornada.EditIndex = e.NewEditIndex;
            //int i = gvJornada.Rows[indice].Cells.Count;

            //for (int i = 0; i < gvJornada.Columns.Count; i++)
            //{
            //    gvJornada.Columns[i].ItemStyle.Width = 10;
            //}

            
            Carga_tablaEmpleados();
            //gvProduccion.Rows[indice].Cells[0].Enabled = false;
            //gvJornada.Rows[indice].Cells[1].Enabled = false;
            gvJornada.Rows[indice].Cells[2].Enabled = false;
            gvJornada.Rows[indice].Cells[3].Enabled = false;
            gvJornada.Rows[indice].Cells[4].Enabled = false;
            gvJornada.Rows[indice].Cells[5].Enabled = false;
            gvJornada.Rows[indice].Cells[6].Enabled = false;
            gvJornada.Rows[indice].Cells[7].Enabled = false;
            gvJornada.Rows[indice].Cells[8].Enabled = false;
            gvJornada.Rows[indice].Cells[9].Enabled = false;
            gvJornada.Rows[indice].Cells[10].Enabled = false;
            gvJornada.Rows[indice].Cells[11].Enabled = false;
            //gvJornada.Rows[indice].Cells[16].Enabled = false;
            //Carga_tablaEmpleados();
            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvProduccion.DataSource = dt;
            //gvProduccion.DataBind();
        }

        protected void gvJornalHora_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvJornalHora.DataKeys[index].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                string a = Main.Ficherotraza("gvJornalHora --> " + ex.Message);
            }
        }
        
        protected void gvJornalNomina_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvJornalNomina.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvJornalNomina --> " + ex.Message);
            }
        }
        protected void gvDestajoNomina_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvDestajoNomina.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvDestajoNomina --> " + ex.Message);
            }
        }
        protected void gvResumenNomina_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvResumenNomina.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvResumenNomina --> " + ex.Message);
            }
        }

        protected void gvTrabajos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvTrabajos.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvTrabajos --> " + ex.Message);
            }
        }


        protected void gvpanelTareas_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvpanelTareas.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvpanelTareas --> " + ex.Message);
            }
        }
        

        protected void gvProdImpDia_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvProdImpDia.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvProdImpDia --> " + ex.Message);
            }
        }

        

        protected void gvProduccion_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvProduccion.DataKeys[index].Value.ToString();
                    //Carga_tablaProduccion();
                    //Carga_tablaEmpleados();

                    //gvJornada.EditIndex = -1;
                    //gvJornada.DataBind();
                }

                if (e.CommandName == "BajaOrden")
                {
                    string Mira = "";
                    decimal rUDSPEDIDAS = 1.0M;
                    decimal rUDSSERVIDAS = 1.0M;

                    decimal NUMPALET = 1.0M;
                    decimal UNIDADES = 1.0M;
                    decimal REPARTO = 1.0M;
                    decimal PARCIAL = 0;
                    string Cabecera = ""; // TxtNumero.Text;
                    string SQL = "";
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvProduccion.Rows[index];
                    string miro = gvProduccion.DataKeys[index].Value.ToString();

                    Label txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvProduccion.Rows[Indice].Cells[11].FindControl("LabOUdsServidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvProduccion.Rows[Indice].Cells[14].FindControl("LabOCargar") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        UNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvProduccion.Rows[Indice].Cells[15].FindControl("LabOPalet") as Label);
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
                        TextAlerta.Text = "Seleccione un número de palets para asignar";
                        alerta.Visible = true;
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
                    Lberror.Text += SQL + "1- gvProduccion_RowCommand " + Variables.mensajeserver;
                    Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    Lberror.Text += " 1- gvProduccion_RowCommand " + Variables.mensajeserver;
                    
                    if (Con is System.DBNull)
                    {
                        N = 1;
                    }
                    else
                    {
                        N = Convert.ToInt32(Con) + 1;
                    }
                    Lberror.Text += SQL + "2- gvProduccion_RowCommand " + Variables.mensajeserver;
                    Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    Lberror.Text += " 2- gvProduccion_RowCommand " + Variables.mensajeserver;
                    
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
                        SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED )";
                        SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
                        SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED ";
                        SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                        Lberror.Text += SQL + "3- gvProduccion_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        Lberror.Text += " 3- gvProduccion_RowCommand " + Variables.mensajeserver;


                        
                        NUMPALET = NUMPALET - Unidad;
                        N += 1;
                        Linea += 1;
                    }
                    if (NUMPALET > 0)
                    {
                        SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                        SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED )";
                        SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + PARCIAL.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
                        SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED ";
                        SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                        Lberror.Text += SQL + "4- gvProduccion_RowCommand " + Variables.mensajeserver;
                        DBHelper.ExecuteNonQuery(SQL);
                        Lberror.Text += " 4- gvProduccion_RowCommand " + Variables.mensajeserver;
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

                    Lberror.Text += SQL + "5- gvProduccion_RowCommand " + Variables.mensajeserver;
                    rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));
                    Lberror.Text += " 5- gvProduccion_RowCommand " + Variables.mensajeserver;

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
                    Lberror.Text += SQL + "5- gvProduccion_RowCommand " + Variables.mensajeserver;
                    DBHelper.ExecuteNonQuery(SQL);
                    Lberror.Text += " 5- gvProduccion_RowCommand " + Variables.mensajeserver;



                    

                    this.Session["NumeroPalet"] = Linea.ToString();

                    Carga_tablaProduccion();
                    Carga_tablaEmpleados();

                    gvProduccion.EditIndex = -1;
                    gvProduccion.DataBind();
                }
                if (e.CommandName == "Cancel")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvProduccion.DataKeys[index].Value.ToString();
                    string Miro = gvProduccion.DataKeys[index].Value.ToString();
                    //GridViewRow row = (GridViewRow)gvProduccion.Rows[e.CommandArgument];
                    //gvProduccion_BajaOrden(Miro, row);
                }

            }
            catch (Exception ex)
            {
                Lberror.Text = "Control RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvProduccion.DataKeys[index].Value.ToString() + " " + ex.Message;
                string a = Main.Ficherotraza("gvProduccion Comand --> " + Lberror.Text);

                Lberror.Visible = true;
            }
        }

        protected void gvEmpleado_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            string miro = "";
            string codigo = "";
            string Centro = "";
            string Fecha = "";

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
                    CreaNomina(codigo, this.Session["UltimaConsulta"].ToString(), Centro);
                }
            }
            catch(Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpledo Comand --> " + ex.Message);
                return;
            }
        }

        protected void gvJornada_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "SubeCarga")
                {
                    decimal UNIDADES = 1.0M;
                    string Cabecera = ""; // TxtNumero.Text;

                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvJornada.Rows[index];
                    string miro = gvJornada.DataKeys[index].Value.ToString();

                    //sube la linea a la orden
                    string Numero = "";

                    Label txtBox = (gvJornada.Rows[Indice].Cells[8].FindControl("LabLCarga") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        UNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvJornada.Rows[Indice].Cells[4].FindControl("LabLNumLinea") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }

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

                    string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + UNIDADES.ToString().Replace(",", ".") + ")), ";
                    SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + UNIDADES.ToString().Replace(",", ".") + ")), ";
                    SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + UNIDADES.ToString().Replace(",", ".") + ")),  ";
                    SQL += " NUMPALET = 0.00 ";
                    SQL += " WHERE ID = " + miro;
                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + miro + " AND NUMERO_LINEA = " + Numero;

                    DBHelper.ExecuteNonQuery(SQL);

                    Carga_tablaProduccion();
                    Carga_tablaEmpleados();

                    gvJornada.EditIndex = -1;

                    gvJornada.DataBind();


                }

                if (e.CommandName == "CargaCamion")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvJornada.Rows[index];
                    string miro = gvJornada.DataKeys[index].Value.ToString();

                    string Numero = "";

                    //string Mira = Server.HtmlDecode(row.Cells[4].Text);
                    //if (Mira != "")
                    //{
                    //    Numero = Mira;
                    //}
                    Label txtBox = (gvJornada.Rows[Indice].Cells[10].FindControl("LabLNumLinea") as Label);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }

                    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
                    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
                    SQL += " WHERE ID_CABECERA = "; // + TxtNumero.Text;
                    SQL += " AND NUMERO_LINEA = " + Numero;
                    Lberror.Text += SQL + "1- gvJornada_rowcomand " + Variables.mensajeserver;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 2- gvJornada_rowcomand  " + Variables.mensajeserver;

                    this.Session["Menu"] = "5";
                    Carga_Menus();
                    ReportViewer0.Visible = false;

                    //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
                    //SQL += " WHERE NUMERO_LINEA = " + Numero; //Miro con ID lo hace con todos

                    //DBHelper.ExecuteNonQuery(SQL);
                    //Carga_tablaEmpleados();

                    //gvJornada.EditIndex = -1;

                    //gvJornada.DataBind();
                }

                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridB"] = gvJornada.DataKeys[index].Value.ToString();
                    //gvProduccion.EditIndex = -1;
                    //gvProduccion.DataBind();
                }
            }
            catch (Exception ex)
            {
                Lberror.Text = "Lista RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvJornada.DataKeys[index].Value.ToString() + " " + ex.Message;
                string a = Main.Ficherotraza("gvJornada RowComand --> " + Lberror.Text);
                Lberror.Visible = true;
            }
        }

        protected void gvJornalHora_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        protected void gvJornalNomina_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }


        protected void gvpanelTareas_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = drv["ZESTADO"].ToString();
                if (this.Session["Erroneo"].ToString() == "0")
                {
                    miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                    if (miro == "2")
                    {
                        e.Row.BackColor = Color.FromName("#eaf5dc");
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
                else
                {
                    if (miro == "<---MAL---")
                    {
                        e.Row.BackColor = Color.FromName("#ff7e62");
                    }
                    else
                    {
                        miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                        if (miro == "2")
                        {
                            e.Row.BackColor = Color.FromName("#eaf5dc");
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
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }
        

        protected void gvDestajoNomina_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        protected void gvResumenNomina_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }


        protected void gvProduccion_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < gvProduccion.Columns.Count; i++)
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
                        e.Row.Cells[i].ToolTip = gvProduccion.Columns[i].HeaderText;
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

                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
                }
                else
                {
                    if((e.Row.DataItemIndex % 2) == 0)
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

        protected void gvpanelTareas_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvpanelTareas.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvProdImpDia_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvProdImpDia.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvJornalHora_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvJornalHora.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvJornalNomina_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvJornalNomina.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvDestajoNomina_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvDestajoNomina.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvResumenNomina_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvResumenNomina.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvTrabajos_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvTrabajos.SelectedRow.BackColor = Color.FromName("#565656");
        }

        protected void gvEmpleado_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvEmpleado.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvProduccion_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvProduccion.SelectedRow.BackColor = Color.FromName("#565656");
        }
        protected void gvJornada_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvJornada.SelectedRow.BackColor = Color.FromName("#565656");
        }


        protected void gvTrabajos_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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

                //GridViewRow row = (GridViewRow)gvJornada.Rows[e.Row];
                //string miro = gvJornada.DataKeys[e.Row].Value.ToString();
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

        protected void gvProdImpDia_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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

                //GridViewRow row = (GridViewRow)gvJornada.Rows[e.Row];
                //string miro = gvJornada.DataKeys[e.Row].Value.ToString();
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

        protected void gvEmpleado_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();
                if (miro == "2")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
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

                //GridViewRow row = (GridViewRow)gvJornada.Rows[e.Row];
                //string miro = gvJornada.DataKeys[e.Row].Value.ToString();
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

        protected void gvJornada_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
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
            //    e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.TableSection = TableRowSection.TableFooter;
            //}
        }

        protected void gvpanelTareas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        

        protected void gvProdImpDia_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gvResumenNomina_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gvTrabajos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gvDestajoNomina_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gvJornalNomina_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
        

        protected void gvJornalHora_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void gvJornada_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow row = (GridViewRow)gvJornada.Rows[e.RowIndex];

            string miro = gvJornada.DataKeys[e.RowIndex].Value.ToString();
            string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaEmpleados();

            gvJornada.EditIndex = -1;

            gvJornada.DataBind();
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
            gvProduccion.DataSource = dt;
            gvProduccion.DataBind();


        }

        private void Carga_tablaCabeceraClose()
        {
            //Carga_tablaListaFiltro();
            string filtros = ""; // this.Session["Filtro"].ToString();
            string SQL = "";
            try
            {

                DataTable dt = null;
                //Carga_Filtros();
                //OrdenEmpleado();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //Casillas verdes a rellenar. Al mostrar inicialmente UdsACargar= UdsPed-UdsServ-UdsEnCarga
                    //UdsPend campo calculado(UdsPend = UdsPed - UdsServ - UdsEnCarga - UdsACargar).Al mostrar inicialmente el cálculo será 0.(UDSPEDIDAS - UDSSERVIDAS - UDSENCARGA - UDSACARGAR)
                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO ";
                    SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA A INNER JOIN [DESARROLLO].[dbo].ZCARGAESTADO B ON A.ESTADO = B.ZID  WHERE A.ESTADO = 3 ";

                    //SQL = " SELECT ID, NUMERO, EMPRESA, PAIS, FECHACARGA, TELEFONO, MATRICULA,";
                    //SQL += " TRANSPORTISTA, TELEFONO_USER, LATITUD, LONGITUD, OBSERVACIONES, ID_SECUENCIA, ESTADO ";
                    //SQL += " FROM [DESARROLLO].[dbo].ZCARGA_CABECERA  WHERE ESTADO = 2  ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if(ElOrden != "")
                    {
                        SQL += ElOrden;
                    }


                    //SQL = "SELECT * FROM ZCARGA_ORDEN  WHERE ESTADO = 0 ";
                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    SQL = " SELECT A.ID, A.NUMERO, A.EMPRESA, A.PAIS, A.FECHAPREPARACION, A.FECHACARGA, A.TELEFONO, A.MATRICULA,";
                    SQL += " A.TRANSPORTISTA, A.TELEFONO_USER, A.LATITUD, A.LONGITUD, A.OBSERVACIONES, A.ID_SECUENCIA, B.ZDESCRIPCION, A.ESTADO ";
                    SQL += " FROM [RIOERESMA].[dbo].ZCARGA_CABECERA A INNER JOIN [DESARROLLO].[dbo].ZCARGAESTADO B ON A.ESTADO = B.ZID  WHERE A.ESTADO = 3 ";

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
                gvEmpleado.DataSource = dt;
                gvEmpleado.DataBind();

                Carga_FiltrosCabecera();
            }
            catch (Exception ex)
            {
                Lberror.Text = "Carga tabla: gvEmpleado " + SQL;
                string a = Main.Ficherotraza("CargaTablacabecera --> " + Lberror.Text);

                Lberror.Visible = true;
            }


        }


        private void Carga_tablaJornadaSQL()
        {
            string SQL = "A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,A.FECHA_JORNADA ,A.HORAINI ,A.HORAFIN ,A.TRANSCURRIDO ,A.RECOTABLET";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();
            Lberror.Text = "";
            //OrdenDeGrid(DrJornada1, DrJornada2);
            DataTable dt = null;
            //Carga_Filtros();
            //OrdenJornada();

            try
            {
                SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA_JORNADA ,FORMAT(A.HORAINI, 'HH:mm') AS HORAINI , FORMAT(A.HORAFIN, 'HH:mm') AS HORAFIN ,";
                SQL += " CASE";
                SQL += " WHEN A.TRANSCURRIDO = '' THEN  '<-----MAL---->' ";
                SQL += " ELSE SUBSTRING(A.TRANSCURRIDO, 1, 5) ";
                SQL += " END as TRANSCURRIDO, ";
                SQL += " A.RECOTABLET, "; //, TOTALMINUTOS, TOTALIMPORTE ";
                SQL += " A.NHORAS, A.XHORAS, A.VNHORAS, A.VXHORAS, A.TIMPNHORAS, A.TIMPXHORAS ";
                SQL += " FROM REC_JORNADA A, REC_EMPLEADO X ";
                SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                SQL += " AND A.COD_EMPLEADO = X.COD_EMPLEADO ";
                SQL += " AND  X.BUSQUEDA = 1 ";
                if (filtros != "")
                {
                    SQL += filtros;
                }
                if (ElOrden != "")
                {
                    SQL += ElOrden;
                }
                else
                {
                    SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA , A.HORAINI";
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("Carga_TablaJornadaSQL --> " + ex.Message);
            }
            this.Session["SQL"] = SQL;

        }

        public static DataTable Carga_tablaJornadaXLS(string SQL)
        {
            DataTable dt = Main.BuscaLote(SQL).Tables[0];         
            return dt;
        }

        private void Carga_tablaJornada(string sortExpression = null)
        {
            //Carga_tablaListaFiltro();
            //Tabla REC_JORNADA
            //DrConsultaJornada.Items.Clear();
            //OrdenCabecera();
            string SQL = " A.COD_EMPLEADO, A.NOMBRE ,A.APELLIDOS ,A.FECHA_JORNADA ,B.FECHA_JORNADA, A.HORAINI ,A.HORAFIN ,A.TRANSCURRIDO ,A.RECOTABLET";//, IMPORTEMINUTOS, TOTALMINUTOS

            //var strsplited = SQL.Split(",".ToCharArray());
            //foreach (var VARIABLE in strsplited)
            //{
            //    DrConsultaJornada.Items.Add(VARIABLE);
            //}

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();
            Lberror.Text = "";
            //OrdenDeGrid(DrJornada1, DrJornada2);

            try
            {
                DataTable dt = null;
                //Carga_Filtros();
                //OrdenJornada();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT  A.ID, A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS , FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA_JORNADA , FORMAT(A.HORAINI, 'HH:mm') AS HORAINI , FORMAT(A.HORAFIN, 'HH:mm') AS HORAFIN ,";
                    SQL += " CASE";
                    SQL += " WHEN A.TRANSCURRIDO = '' THEN  '<-----MAL---->' ";
                    SQL += " ELSE SUBSTRING(A.TRANSCURRIDO, 1, 5) ";
                    SQL += " END as TRANSCURRIDO, ";
                    SQL += " A.RECOTABLET "; //, TOTALMINUTOS, TOTALIMPORTE ";
                    SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    SQL += " AND Z.BUSQUEDA = 1 ";

                    if (filtros != "")
                    {
                        //if (filtros.Contains("COD_EMPLEADO")) { filtros = filtros.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (filtros.Contains("NOMBRE")) { filtros = filtros.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (filtros.Contains("APELLIDOS")) { filtros = filtros.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (filtros.Contains("FECHA_JORNADA")) { filtros = filtros.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                        //if (filtros.Contains("HORAINI")) { filtros = filtros.Replace("HORAINI", "A.HORAINI"); }
                        //if (filtros.Contains("HORAFIN")) { filtros = filtros.Replace("HORAFIN", "A.HORAFIN"); }
                        //if (filtros.Contains("TRANSCURRIDO")) { filtros = filtros.Replace("TRANSCURRIDO", "A.TRANSCURRIDO"); }
                        //if (filtros.Contains("RECOTABLET")) { filtros = filtros.Replace("RECOTABLET", "A.RECOTABLET"); }                    
                        SQL += filtros;
                    }

                    if(ElOrden != "")
                    {
                        //if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (ElOrden.Contains("FECHA_JORNADA")) { ElOrden = ElOrden.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                        //if (ElOrden.Contains("HORAINI")) { ElOrden = ElOrden.Replace("HORAINI", "A.HORAINI"); }
                        //if (ElOrden.Contains("HORAFIN")) { ElOrden = ElOrden.Replace("HORAFIN", "A.HORAFIN"); }
                        //if (ElOrden.Contains("TRANSCURRIDO")) { ElOrden = ElOrden.Replace("TRANSCURRIDO", "A.TRANSCURRIDO"); }
                        //if (ElOrden.Contains("RECOTABLET")) { ElOrden = ElOrden.Replace("RECOTABLET", "A.RECOTABLET"); }
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA ,A.HORAINI";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                else
                {
                    //SQL = " SELECT  A.ID, A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS , FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA_JORNADA , FORMAT(A.HORAINI, 'HH:mm') AS HORAINI , FORMAT(A.HORAFIN, 'HH:mm') AS HORAFIN ,";
                    //SQL += " CASE";
                    //SQL += " WHEN A.TRANSCURRIDO = '' THEN  '<-----MAL---->' ";
                    //SQL += " ELSE SUBSTRING(A.TRANSCURRIDO, 1, 5) ";
                    //SQL += " END as TRANSCURRIDO, ";
                    //SQL += " A.RECOTABLET "; //, TOTALMINUTOS, TOTALIMPORTE ";
                    //SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z ";
                    //SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    //SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    //SQL += " AND Z.COD_EMPLEADO = 1 ";
                    SQL = " SELECT  A.ID, A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS , FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA_JORNADA , FORMAT(A.HORAINI, 'HH:mm') AS HORAINI , FORMAT(A.HORAFIN, 'HH:mm') AS HORAFIN ,";
                    SQL += " CASE";
                    SQL += " WHEN A.TRANSCURRIDO = '' THEN  '<-----MAL---->' ";
                    SQL += " ELSE SUBSTRING(A.TRANSCURRIDO, 1, 5) ";
                    SQL += " END as TRANSCURRIDO, ";
                    SQL += " A.RECOTABLET "; //, TOTALMINUTOS, TOTALIMPORTE ";
                    SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    SQL += " AND Z.BUSQUEDA = 1 ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA ,A.HORAINI";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                }

                this.Session["MiConsulta"] = dt;
                lbRowJornada.Text = "Registros: " + dt.Rows.Count;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvJornada.DataSource = dv;
                }
                else
                {
                    gvJornada.DataSource = dt;
                }
                gvJornada.DataBind();

                //gvJornada.DataSource = dt;
                //gvJornada.DataBind();

                //Carga_FiltrosCabecera();
            }
            catch (Exception ex)
            {
                Lberror.Text = "Carga tabla: gvJornada " + Variables.Error + " --> " + ex.Message + SQL;
                string a = Main.Ficherotraza("Carga_TablaJornada --> " + Lberror.Text);

                Lberror.Visible = true;
            }


        }

        //protected void btconsultaEmpleado_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = gvEmpleado.DataSource as DataTable;
        //    string Campo = DRgvEmpleado.SelectedItem.Value;
        //    string Condicion = TxtConsultaEmpleado.Text;
        //    if (Campo == "Ninguno")
        //    {
        //        Carga_tablaEmpleados();
        //        //Carga_tablaProduccion();
        //        //Carga_tablaJornada();
        //        //Carga_Jornal_Horas();
        //        //Carga_Jornal_Nominas();
        //        //Carga_Destajo_Nomina();
        //        //Carga_Nomina_resumen();
        //        //Carga_ProddiaImporte();
        //    }
        //    else
        //    {
        //        var results = from myRow in dt.AsEnumerable()
        //                      where myRow.Field<string>(Campo).Contains("/" + Condicion + "/")
        //                      select myRow;

        //        gvEmpleado.DataSource = results;
        //        gvEmpleado.DataBind();
        //    }
        //    //Si es todo Carga_tablaJornada()
        //}

        //protected void ConsultagvJornada_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = gvJornada.DataSource as DataTable;
        //    string Campo = DrConsultaJornada.SelectedItem.Value;
        //    string Condicion = TxtConsultaJornada.Text;
        //    if (Campo == "Ninguno")
        //    {
        //        Carga_tablaJornada();
        //    }
        //    else
        //    {
        //        var results = from myRow in dt.AsEnumerable()
        //                      where myRow.Field<string>(Campo).Contains("/" + Condicion + "/")
        //                      select myRow;

        //        gvJornada.DataSource = results;
        //        gvJornada.DataBind();
        //    }
        //    //Si es todo Carga_tablaJornada()
        //}
        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            //string miro = TxtCodigo.Text;
            BtGralConsulta_Click(sender, e);
            //checkOk_Click(sender, e);
            //Response.Redirect("RecoNomina.aspx");
            Lbmensaje.Text = "Ya";
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            UpdatePanel3.Update();

        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }

        //protected void ConsultagvProduccion_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = gvJornada.DataSource as DataTable;
        //    string Campo = DrConsultaJornada.SelectedItem.Value;
        //    string Condicion = TxtConsultaJornada.Text;
        //    if(Campo == "Ninguno")
        //    {
        //        Carga_tablaJornada();
        //    }
        //    else
        //    {
        //        var results = from myRow in dt.AsEnumerable()
        //                      where myRow.Field<string>(Campo).Contains("/" + Condicion + "/") 
        //                      select myRow;

        //        gvJornada.DataSource = results;
        //        gvJornada.DataBind();
        //    }
        //    //Si es todo Carga_tablaJornada()
        //}


        private void Carga_tablaProduccionSQL()
        {
            //REC_PRODUCCION
            DataTable dt = null;
            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, B.FECHA_EMPLEADOS, A.HORA_EMPLEADO, A.TABLET, A.CODFINCA, ";
            SQL += "A.DESCRFINCA, A.ZONA, A.DESCRZONAZ, A.TAREA, A.DESCRTAREA, A.ENVASE, A.DESCRENVASE, A.MARCAENVASE, A.PLANTAS ";

            Carga_FiltrosGral(SQL);
            //OrdenDeGrid(DrProduccion1, DrProduccion2);

            string Filtro = this.Session["Filtro"].ToString();

            SQL = " SELECT A.ID, A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, FORMAT(A.HORA_EMPLEADO, 'HH:mm') AS HORA_EMPLEADO, A.TABLET, A.CODFINCA,";
            SQL += " A.DESCRFINCA, A.ZONA, A.DESCRZONAZ, A.TAREA, A.DESCRTAREA, A.ENVASE, A.DESCRENVASE, A.MARCAENVASE, A.PLANTAS ";
            SQL += " FROM REC_PRODUCCION A, REC_EMPLEADO Z  ";
            SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
            SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
            SQL += " AND Z.BUSQUEDA = 1 ";

            if (this.Session["FiltroCodEmpleado"].ToString() != "")
            {
                SQL += " AND COD_EMPLEADO = '" + this.Session["FiltroCodEmpleado"].ToString() + "'";
            }

            if (Filtro != "")
            {
                SQL += Filtro;
            }

            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY COD_EMPLEADO, FECHA_EMPLEADOS, HORA_EMPLEADO";
            }
            this.Session["SQL"] = SQL;
        }


        public static DataTable Carga_tablaProduccionXLS(string SQL)
        {
            //REC_PRODUCCION

            DataTable dt = null;
            dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_tablaProduccion(string sortExpression = null)
        {
            //REC_PRODUCCION
            //Carga_tablaListaFiltro();
            //
            //OrdenProduccion();
            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, B.FECHA_EMPLEADOS, A.HORA_EMPLEADO, A.TABLET, A.CODFINCA, ";
            SQL += "A.DESCRFINCA, A.ZONA, A.DESCRZONAZ, A.TAREA, A.DESCRTAREA, A.ENVASE, A.DESCRENVASE, A.MARCAENVASE, A.PLANTAS ";
            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            //OrdenDeGrid(DrProduccion1, DrProduccion2);
            string Filtro = this.Session["Filtro"].ToString();
            try
            {

                DataTable dt = null;
                //Carga_Filtros();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //Casillas verdes a rellenar. Al mostrar inicialmente UdsACargar= UdsPed-UdsServ-UdsEnCarga
                    //UdsPend campo calculado(UdsPend = UdsPed - UdsServ - UdsEnCarga - UdsACargar).Al mostrar inicialmente el cálculo será 0.(UDSPEDIDAS - UDSSERVIDAS - UDSENCARGA - UDSACARGAR)

 


                    SQL = " SELECT A.ID, A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, FORMAT(A.HORA_EMPLEADO, 'HH:mm') AS HORA_EMPLEADO, A.TABLET, A.CODFINCA,";
                    SQL += " A.DESCRFINCA, A.ZONA, A.DESCRZONAZ, A.TAREA, A.DESCRTAREA, A.ENVASE, A.DESCRENVASE, A.MARCAENVASE, A.PLANTAS ";
                    SQL += " FROM REC_PRODUCCION A, REC_EMPLEADO Z  ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    SQL += " AND Z.BUSQUEDA = 1 ";

                    if (this.Session["FiltroCodEmpleado"].ToString() != "")
                    {
                        SQL += " AND A.COD_EMPLEADO = '" + this.Session["FiltroCodEmpleado"].ToString() + "'";
                    }
                    if (Filtro != "")
                    {
                        //if (Filtro.Contains("COD_EMPLEADO")) { Filtro = Filtro.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (Filtro.Contains("NOMBRE")) { Filtro = Filtro.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (Filtro.Contains("APELLIDOS")) { Filtro = Filtro.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (Filtro.Contains("FECHA_EMPLEADOS")) { Filtro = Filtro.Replace("FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS"); }
                        //if (Filtro.Contains("HORA_EMPLEADO")) { Filtro = Filtro.Replace("HORA_EMPLEADO", "A.HORA_EMPLEADO"); }
                        //if (Filtro.Contains("TABLET")) { Filtro = Filtro.Replace("TABLET", "A.TABLET"); }
                        //if (Filtro.Contains("CODFINCA")) { Filtro = Filtro.Replace("CODFINCA", "A.CODFINCA"); }
                        //if (Filtro.Contains("DESCRFINCA")) { Filtro = Filtro.Replace("DESCRFINCA", "A.DESCRFINCA"); }
                        //if (Filtro.Contains("ZONA")) { Filtro = Filtro.Replace("ZONA", "A.ZONA"); }
                        //if (Filtro.Contains("TAREA")) { Filtro = Filtro.Replace("TAREA", "A.TAREA"); }
                        //if (Filtro.Contains("DESCRTAREA")) { Filtro = Filtro.Replace("DESCRTAREA", "A.DESCRTAREA"); }
                        //if (Filtro.Contains("DESCRZONAZ")) { Filtro = Filtro.Replace("DESCRZONAZ", "A.DESCRZONAZ"); }
                        //if (Filtro.Contains("ENVASE")) { Filtro = Filtro.Replace("ENVASE", "A.ENVASE"); }
                        //if (Filtro.Contains("DESCRENVASE")) { Filtro = Filtro.Replace("DESCRENVASE", "A.DESCRENVASE"); }
                        //if (Filtro.Contains("MARCAENVASE")) { Filtro = Filtro.Replace("MARCAENVASE", "A.MARCAENVASE"); }
                        //if (Filtro.Contains("PLANTAS")) { Filtro = Filtro.Replace("PLANTAS", "A.PLANTAS"); }
                        SQL += Filtro;
                    }
                    if (ElOrden != "")
                    {
                        //if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (ElOrden.Contains("FECHA_EMPLEADOS")) { ElOrden = ElOrden.Replace("FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS"); }
                        //if (ElOrden.Contains("HORA_EMPLEADO")) { ElOrden = ElOrden.Replace("HORA_EMPLEADO", "A.HORA_EMPLEADO"); }
                        //if (ElOrden.Contains("TABLET")) { ElOrden = ElOrden.Replace("TABLET", "A.TABLET"); }
                        //if (ElOrden.Contains("CODFINCA")) { ElOrden = ElOrden.Replace("CODFINCA", "A.CODFINCA"); }
                        //if (ElOrden.Contains("DESCRFINCA")) { ElOrden = ElOrden.Replace("DESCRFINCA", "A.DESCRFINCA"); }
                        //if (ElOrden.Contains("ZONA")) { ElOrden = ElOrden.Replace("ZONA", "A.ZONA"); }
                        //if (ElOrden.Contains("TAREA")) { ElOrden = ElOrden.Replace("TAREA", "A.TAREA"); }
                        //if (ElOrden.Contains("DESCRTAREA")) { ElOrden = ElOrden.Replace("DESCRTAREA", "A.DESCRTAREA"); }
                        //if (ElOrden.Contains("DESCRZONAZ")) { ElOrden = ElOrden.Replace("DESCRZONAZ", "A.DESCRZONAZ"); }
                        //if (ElOrden.Contains("ENVASE")) { ElOrden = ElOrden.Replace("ENVASE", "A.ENVASE"); }
                        //if (ElOrden.Contains("DESCRENVASE")) { ElOrden = ElOrden.Replace("DESCRENVASE", "A.DESCRENVASE"); }
                        //if (ElOrden.Contains("MARCAENVASE")) { ElOrden = ElOrden.Replace("MARCAENVASE", "A.MARCAENVASE"); }
                        //if (ElOrden.Contains("PLANTAS")) { ElOrden = ElOrden.Replace("PLANTAS", "A.PLANTAS"); }
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO";
                    }

                    Lberror.Text += SQL + "1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 1- Carga_tabla BuscaLote " + Variables.mensajeserver;
                }
                else
                {
                    SQL = " SELECT A.ID, A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, FORMAT(A.HORA_EMPLEADO, 'HH:mm') AS HORA_EMPLEADO, A.TABLET, A.CODFINCA,";
                    SQL += " A.DESCRFINCA, A.ZONA, A.DESCRZONAZ, A.TAREA, A.DESCRTAREA, A.ENVASE, A.DESCRENVASE, A.MARCAENVASE, A.PLANTAS ";
                    SQL += " FROM REC_PRODUCCION A, REC_EMPLEADO Z  ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
                    SQL += " AND Z.BUSQUEDA = 1 ";
                    if (this.Session["FiltroCodEmpleado"].ToString() != "")
                    {
                        SQL += " AND A.COD_EMPLEADO = '" + this.Session["FiltroCodEmpleado"].ToString() + "'";
                    }
                    if (Filtro != "")
                    {
                        //if (Filtro.Contains("COD_EMPLEADO")) { Filtro = Filtro.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (Filtro.Contains("NOMBRE")) { Filtro = Filtro.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (Filtro.Contains("APELLIDOS")) { Filtro = Filtro.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (Filtro.Contains("FECHA_EMPLEADOS")) { Filtro = Filtro.Replace("FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS"); }
                        //if (Filtro.Contains("HORA_EMPLEADO")) { Filtro = Filtro.Replace("HORA_EMPLEADO", "A.HORA_EMPLEADO"); }
                        //if (Filtro.Contains("TABLET")) { Filtro = Filtro.Replace("TABLET", "A.TABLET"); }
                        //if (Filtro.Contains("CODFINCA")) { Filtro = Filtro.Replace("CODFINCA", "A.CODFINCA"); }
                        //if (Filtro.Contains("DESCRFINCA")) { Filtro = Filtro.Replace("DESCRFINCA", "A.DESCRFINCA"); }
                        //if (Filtro.Contains("ZONA")) { Filtro = Filtro.Replace("ZONA", "A.ZONA"); }
                        //if (Filtro.Contains("TAREA")) { Filtro = Filtro.Replace("TAREA", "A.TAREA"); }
                        //if (Filtro.Contains("DESCRTAREA")) { Filtro = Filtro.Replace("DESCRTAREA", "A.DESCRTAREA"); }
                        //if (Filtro.Contains("DESCRZONAZ")) { Filtro = Filtro.Replace("DESCRZONAZ", "A.DESCRZONAZ"); }
                        //if (Filtro.Contains("ENVASE")) { Filtro = Filtro.Replace("ENVASE", "A.ENVASE"); }
                        //if (Filtro.Contains("DESCRENVASE")) { Filtro = Filtro.Replace("DESCRENVASE", "A.DESCRENVASE"); }
                        //if (Filtro.Contains("MARCAENVASE")) { Filtro = Filtro.Replace("MARCAENVASE", "A.MARCAENVASE"); }
                        //if (Filtro.Contains("PLANTAS")) { Filtro = Filtro.Replace("PLANTAS", "A.PLANTAS"); }
                        SQL += Filtro;
                    }
                    if (ElOrden != "")
                    {
                        //if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                        //if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                        //if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                        //if (ElOrden.Contains("FECHA_EMPLEADOS")) { ElOrden = ElOrden.Replace("FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS"); }
                        //if (ElOrden.Contains("HORA_EMPLEADO")) { ElOrden = ElOrden.Replace("HORA_EMPLEADO", "A.HORA_EMPLEADO"); }
                        //if (ElOrden.Contains("TABLET")) { ElOrden = ElOrden.Replace("TABLET", "A.TABLET"); }
                        //if (ElOrden.Contains("CODFINCA")) { ElOrden = ElOrden.Replace("CODFINCA", "A.CODFINCA"); }
                        //if (ElOrden.Contains("DESCRFINCA")) { ElOrden = ElOrden.Replace("DESCRFINCA", "A.DESCRFINCA"); }
                        //if (ElOrden.Contains("ZONA")) { ElOrden = ElOrden.Replace("ZONA", "A.ZONA"); }
                        //if (ElOrden.Contains("TAREA")) { ElOrden = ElOrden.Replace("TAREA", "A.TAREA"); }
                        //if (ElOrden.Contains("DESCRTAREA")) { ElOrden = ElOrden.Replace("DESCRTAREA", "A.DESCRTAREA"); }
                        //if (ElOrden.Contains("DESCRZONAZ")) { ElOrden = ElOrden.Replace("DESCRZONAZ", "A.DESCRZONAZ"); }
                        //if (ElOrden.Contains("ENVASE")) { ElOrden = ElOrden.Replace("ENVASE", "A.ENVASE"); }
                        //if (ElOrden.Contains("DESCRENVASE")) { ElOrden = ElOrden.Replace("DESCRENVASE", "A.DESCRENVASE"); }
                        //if (ElOrden.Contains("MARCAENVASE")) { ElOrden = ElOrden.Replace("MARCAENVASE", "A.MARCAENVASE"); }
                        //if (ElOrden.Contains("PLANTAS")) { ElOrden = ElOrden.Replace("PLANTAS", "A.PLANTAS"); }
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO";
                    }

                    Lberror.Text += SQL + "2- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 2- Carga_tabla BuscaLote " + Variables.mensajeserver;
                    
                    //}
                }
                //Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), ""); // TxtNumero.Text);
                this.Session["MiConsulta"] = dt;
                lbRowProduccion.Text = "Registros: " + dt.Rows.Count;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvProduccion.DataSource = dv;
                }
                else
                {
                    gvProduccion.DataSource = dt;
                }
                gvProduccion.DataBind();

                //gvProduccion.DataSource = dt;
                //gvProduccion.DataBind();

                //busca Error donde no se puede depurar
                Lberror.Text = "";

            }
            catch (Exception ex)
            {
                Variables.Error = "";
                Lberror.Text = "Carga tabla: " + SQL;
                string a = Main.Ficherotraza("Carga_TablaProduccion --> " + Lberror.Text);

                Lberror.Visible = true;
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

        //private void Carga_Filtros()
        //{
        //    string SQL = "";
        //    DataTable dt = null;

        //    DrConsultas.Items.Clear();
        //    DrConsultas.DataValueField = "EMPRESA";
        //    DrConsultas.DataTextField = "EMPRESA";
        //    DrConsultas.Items.Insert(0, new ListItem("Seleccione uno", ""));
        //    //SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY EMPRESA ";
        //    SQL = "SELECT DISTINCT(EMPRESA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY EMPRESA ";
        //    dt = Main.BuscaLote(SQL).Tables[0];
        //    DrConsultas.DataSource = dt;
        //    DrConsultas.DataBind();
        //    DrConsultas.SelectedIndex = -1;

        //    DrDesde.Items.Clear();
        //    DrDesde.DataValueField = "FECHAENTREGA";
        //    DrDesde.DataTextField = "FECHAENTREGA";
        //    DrDesde.Items.Insert(0, new ListItem("Seleccione uno", ""));
        //    //SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY FECHAENTREGA ";
        //    SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY FECHAENTREGA ";
        //    dt = Main.BuscaLote(SQL).Tables[0];
        //    DrDesde.DataSource = dt;
        //    DrDesde.DataBind();
        //    DrDesde.SelectedIndex = -1;

        //    DrHasta.Items.Clear();
        //    DrHasta.DataValueField = "FECHAENTREGA";
        //    DrHasta.DataTextField = "FECHAENTREGA";
        //    DrHasta.Items.Insert(0, new ListItem("Seleccione uno", ""));
        //    //SQL = "SELECT DISTINC(FECHAENTREGA) FROM ZCARGA_ORDEN WHERE ESTADO = 0 ORDER BY FECHAENTREGA ";
        //    //dt = Main.BuscaLote(SQL).Tables[0];
        //    DrHasta.DataSource = dt;
        //    DrHasta.DataBind();
        //    DrHasta.SelectedIndex = -1;

        //    DrRutas.Items.Clear();
        //    DrRutas.DataValueField = "RUTA";
        //    DrRutas.DataTextField = "RUTA";
        //    DrRutas.Items.Insert(0, new ListItem("Seleccione uno", ""));
        //    //SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY RUTA ";
        //    SQL = "SELECT DISTINCT(RUTA) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY RUTA ";
        //    dt = Main.BuscaLote(SQL).Tables[0];
        //    DrRutas.DataSource = dt;
        //    DrRutas.DataBind();
        //    DrRutas.SelectedIndex = -1;

        //    DrCliente.Items.Clear();
        //    DrCliente.DataValueField = "NOMBREFISCAL"; // "CLIENTEPROVEEDOR";
        //    DrCliente.DataTextField = "NOMBREFISCAL"; //"CLIENTEPROVEEDOR";
        //    DrCliente.Items.Insert(0, new ListItem("Seleccione uno", ""));
        //    //SQL = "SELECT DISTINCT(CLIENTEPROVEEDOR) FROM ZCARGA_ORDEN WHERE ESTADO <> 2 AND (COMPUTER = '" + this.Session["ComputerName"].ToString() + "' OR COMPUTER IS NULL) ORDER BY CLIENTEPROVEEDOR ";
        //    SQL = "SELECT DISTINCT(NOMBREFISCAL) FROM ZCARGA_ORDEN WHERE (ESTADO <> 2 OR ESTADO IS NULL) ORDER BY NOMBREFISCAL ";
        //    dt = Main.BuscaLote(SQL).Tables[0];
        //    DrCliente.DataSource = dt;
        //    DrCliente.DataBind();
        //    DrCliente.SelectedIndex = -1;
        //}

        private void Carga_FiltrosGral(string Campos)
        {
            Campos = Campos.Replace(" ", "");
            string[] Fields = System.Text.RegularExpressions.Regex.Split(Campos.ToString(), ",");
            string SQL = "";
            string Campo = "";
            string Texto = "";
            string Filtros = "";
            this.Session["Filtro"] = "";
            
            HtmlGenericControl Ia = new HtmlGenericControl();
            TextBox Tx = new TextBox();

            for (int i = 0; i < Fields.Count(); i++)
            {
                Campo = "";
                Texto = "";

                //if (TxtCodigo.Text != "" && Fields[i] == "A.COD_EMPLEADO")
                if (Fields[i] == "A.COD_EMPLEADO" || Fields[i] == "COD_EMPLEADO")
                {
                    Campo = Fields[i];
                    Texto = TxtCodigo.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IContent;
                    Tx = (TextBox)TxtCodigo;
                }
                else if ( Fields[i] == "A.NOMBRE" || Fields[i] == "NOMBRE")
                {
                    Campo = Fields[i];
                    Texto = TxtNombre.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)INombre;
                    Tx = (TextBox)TxtNombre;
                }

                else if ( Fields[i] == "A.APELLIDOS" || Fields[i] == "APELLIDOS")
                {
                    Campo = Fields[i];
                    Texto = TxtApellidos.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IApellido;
                    Tx = (TextBox)TxtApellidos;
                }
                else if ( Fields[i] == "CENTRO")
                {
                    Campo = Fields[i];
                    Texto = TxtCentro.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)ICentro;
                    Tx = (TextBox)TxtCentro;
                }
                else if ( Fields[i] == "CATEGORIA")
                {
                    Campo = Fields[i];
                    Texto = TxtCategoria.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)ICategoria;
                    Tx = (TextBox)TxtCategoria;
                }
                else if ( Fields[i] == "VIVIENDA")
                {
                    Campo = Fields[i];
                    Texto = TxtVivienda.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IVivienda;
                    Tx = (TextBox)TxtVivienda;
                }
                else if ( Fields[i] == "A.ENVASE" || Fields[i] == "B.ENVASE")
                {
                    Campo = Fields[i];
                    Texto = TxtEnvase.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IEnvase;
                    Tx = (TextBox)TxtEnvase;
                }
                else if (Fields[i] == "A.TAREA")
                {
                    Campo = Fields[i];
                    Texto = TxtVariedad.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IVariedad;
                    Tx = (TextBox)TxtVariedad;
                }
                else if ( Fields[i] == "A.VARIEDAD")
                {
                    Campo = Fields[i];
                    Texto = TxtVariedad.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IVariedad;
                    Tx = (TextBox)TxtVariedad;
                }
                else if ( Fields[i] == "A.ZONA")
                {
                    Campo = Fields[i];
                    Texto = TxtZona.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IZona;
                    Tx = (TextBox)TxtZona;
                }
                if ( Fields[i] == "A.FECHA_INI")
                {
                    Campo = Fields[i];
                    Texto = TxtBFechaIni.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IFechaIni;
                    Tx = (TextBox)TxtBFechaIni;
                }
                else if ( Fields[i] == "A.FECHA_FIN")
                {
                    Campo = Fields[i];
                    Texto = TxtBFechaFin.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IFechaFin;
                    Tx = (TextBox)TxtBFechaFin;
                }
                else if ( Fields[i] == "A.RECOTABLET")
                {
                    Campo = Fields[i];
                    Texto = TxtBTablet.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)ITablet;
                    Tx = (TextBox)TxtBTablet;
                }
                else if (Fields[i] == "A.TABLET")
                {
                    Campo = Fields[i];
                    Texto = TxtBTablet.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)ITablet;
                    Tx = (TextBox)TxtBTablet;
                }
                if ( Fields[i] == "A.FECHA_EMPLEADOS" || Fields[i] == "A.FECHA_JORNADA")
                {
                    Campo = Fields[i];
                    Texto = TxtBFechaIni.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IFechaIni;
                    Tx = (TextBox)TxtBFechaIni;
                }
                else if ( Fields[i] == "B.FECHA_EMPLEADOS" || Fields[i] == "B.FECHA_JORNADA")
                {
                    Fields[i] = Fields[i].Replace("B.FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS");
                    Fields[i] = Fields[i].Replace("B.FECHA_JORNADA", "A.FECHA_JORNADA");
                    Campo = Fields[i];
                    Texto = TxtBFechaFin.Text.Replace(",", "','");
                    Ia = (HtmlGenericControl)IFechaFin;
                    Tx = (TextBox)TxtBFechaFin;
                }




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
            //Ahora consultas por grid
            //if (this.Session["filtrolocal"].ToString() != "0")
            //{
            //    if(this.Session["filtrolocal"].ToString() == "1")
            //    {
            //        if (IFilEmpleado.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IFilEmpleado, TxtConsultaEmpleado, DRgvEmpleado.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "2")
            //    {
            //        if (IfilProduccion.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IfilProduccion, TxtFilProduccion, DrrowProduccion.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "3")
            //    {
            //        if (IFilJornada.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IFilJornada, TxtConsultaJornada, DrConsultaJornada.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "4")
            //    {
            //        if (IJornalHora.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IJornalHora, TxtJornalHora, DrrowJornalHora.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "5")
            //    {
            //        if (IfilJornalNomina.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IfilJornalNomina, TxtFilJornalNomina, DrrowJornalNomina.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "6")
            //    {
            //        if (IfilDestajoNomina.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IfilDestajoNomina, TxtfilDestajoNomina, DrrowDestajoNimina.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "7")
            //    {
            //        if (IfilProddiaImporte.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IfilProddiaImporte, TxtfilProddiaImporte, DrrowProdDiaImporte.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "8")
            //    {
            //        if (IFilResumenNomina.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(IFilResumenNomina, TxtFilResumenNomina, DrrowResumenNomina.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //    else if (this.Session["filtrolocal"].ToString() == "9")
            //    {
            //        if (ITrabajos.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        { }
            //        else
            //        {
            //            MontaIconoConsulta(ITrabajos, TxtFilTrabajos, DrFindTrabajo.SelectedItem.Value, this.Session["Filtro"].ToString());
            //        }
            //    }
            //}
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
            if(Campo == "Ninguno") { return; }
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
            string SQL = "";
            string Campo = "";
            string Texto = "";
            string Filtros = "";
            this.Session["Filtro"] = "";
            DrVistaEmpleado.Items.Clear();

            for (int i = 0; i < Fields.Count(); i++)
            {
               if (TxtCodigo.Text != "" && Fields[i] == "COD_EMPLEADO")
               {
                    Campo = Fields[i];
                    Texto = TxtCodigo.Text.Replace(",", "','");

                   if (IContent.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND COD_EMPLEADO IN ('" + TxtCodigo.Text.Replace(",","','") + "')";
                       DrVistaEmpleado.Items.Add("Código contiene: " + TxtCodigo.Text);
                   }
                   else if (IContent.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                       Filtros += " AND COD_EMPLEADO = LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Código incluye: " + TxtCodigo.Text);
                   }
                   else if (IContent.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND COD_EMPLEADO = NOT IN ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Código no contiene: " + TxtCodigo.Text);
                   }
                   else if (IContent.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
                   }


                    else if (IContent.Attributes["class"] == "fa fa-chevron-left fa-2x")
                    {
                        Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
                    }
                    else if (IContent.Attributes["class"] == "fa fa-chevron-right fa-2x")
                    {
                        Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
                    }
                    else if (IContent.Attributes["class"] == "fa fa-arrows-alt fa-2x")
                    {
                        Filtros += " AND COD_EMPLEADO = NOT LIKE ('" + TxtCodigo.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Código no incluye: " + TxtCodigo.Text);
                    }

                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               
               if (TxtNombre.Text != "" && Fields[i] == "NOMBRE")
                {
                   if (INombre.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND NOMBRE IN ('" + TxtNombre.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Nombre contiene: " + TxtNombre.Text);
                   }
                   else if (INombre.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                       Filtros += " AND NOMBRE = LIKE ('" + TxtNombre.Text.Replace(",", "','") + "')";  
                       DrVistaEmpleado.Items.Add("Nombre incluye: " + TxtNombre.Text);
                   }
                   else if (INombre.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND NOMBRE = NOT IN ('" + TxtNombre.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Nombre no contiene: " + TxtNombre.Text);
                   }
                   else if (INombre.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND NOMBRE = NOT LIKE ('" + TxtNombre.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Nombre no incluye: " + TxtNombre.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtApellidos.Text != "" && Fields[i] == "APELLIDOS")
                {
                   if (IApellido.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND APELLIDOS IN ('" + TxtApellidos.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Apellidos contiene: " + TxtApellidos.Text);
                   }
                   else if (IApellido.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND APELLIDOS = LIKE ('" + TxtApellidos.Text.Replace(",", "','") + "')";    
                       DrVistaEmpleado.Items.Add("Apellidos incluye: " + TxtApellidos.Text);
                   }
                   else if (IApellido.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND APELLIDOS = NOT IN ('" + TxtApellidos.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Apellidos no contiene: " + TxtApellidos.Text);
                   }
                   else if (IApellido.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND APELLIDOS = NOT LIKE ('" + TxtApellidos.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Apellidos no incluye: " + TxtApellidos.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtCentro.Text != "" && Fields[i] == "CENTRO")
                {
                   if (ICentro.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND CENTRO IN ('" + TxtCentro.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Centro contiene: " + TxtCentro.Text);
                   }
                   else if (ICentro.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND CENTRO = LIKE ('" + TxtCentro.Text.Replace(",", "','") + "')";  
                       DrVistaEmpleado.Items.Add("Centro incluye: " + TxtCentro.Text);
                   }
                   else if (ICentro.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND CENTRO = NOT IN ('" + TxtCentro.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Centro no contiene: " + TxtCentro.Text);
                   }
                   else if (ICentro.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND CENTRO = NOT LIKE ('" + TxtCentro.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Centro no incluye: " + TxtCentro.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtCategoria.Text != "" && Fields[i] == "CATEGORIA")
                {
                   if (ICategoria.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND CATEGORIA IN ('" + TxtCategoria.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Categoria contiene: " + TxtCategoria.Text);
                   }
                   else if (ICategoria.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND CATEGORIA = LIKE ('" + TxtCategoria.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Categoria incluye: " + TxtCategoria.Text);
                   }
                   else if (ICategoria.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND CATEGORIA = NOT IN ('" + TxtCategoria.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Categoria no contiene: " + TxtCategoria.Text);
                   }
                   else if (ICategoria.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND CATEGORIA = NOT LIKE ('" + TxtCategoria.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Categoria no incluye: " + TxtCategoria.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtVivienda.Text != "" && Fields[i] == "VIVIENDA")
                {
                   if (IVivienda.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND VIVIENDA IN ('" + TxtVivienda.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Vivienda contiene: " + TxtVivienda.Text);
                   }
                   else if (IVivienda.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND VIVIENDA = LIKE ('" + TxtVivienda.Text.Replace(",", "','") + "')";   
                       DrVistaEmpleado.Items.Add("Vivienda incluye: " + TxtVivienda.Text);
                   }
                   else if (IVivienda.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND VIVIENDA = NOT IN ('" + TxtVivienda.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Vivienda no contiene: " + TxtVivienda.Text);
                   }
                   else if (IVivienda.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND VIVIENDA = NOT LIKE ('" + TxtVivienda.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Vivienda no incluye: " + TxtVivienda.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtEnvase.Text != "" && Fields[i] == "ENVASE")
                {
                   if (IEnvase.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND ENVASE IN ('" + TxtEnvase.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Envase contiene: " + TxtEnvase.Text);
                   }
                   else if (IEnvase.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND ENVASE = LIKE ('" + TxtEnvase.Text.Replace(",", "','") + "')"; 
                       DrVistaEmpleado.Items.Add("Envase incluye: " + TxtEnvase.Text);
                   }
                   else if (IEnvase.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND ENVASE = NOT IN ('" + TxtEnvase.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Envase no contiene: " + TxtEnvase.Text);
                   }
                   else if (IEnvase.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND ENVASE = NOT LIKE ('" + TxtEnvase.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Envase no incluye: " + TxtEnvase.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtVariedad.Text != "" && Fields[i] == "VARIEDAD")
                {
                   if (IVariedad.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND VARIEDAD IN ('" + TxtVariedad.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Variedad contiene: " + TxtVariedad.Text);
                   }
                   else if (IVariedad.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND VARIEDAD = LIKE ('" + TxtVariedad.Text.Replace(",", "','") + "')"; 
                       DrVistaEmpleado.Items.Add("Variedad incluye: " + TxtVariedad.Text);
                   }
                   else if (IVariedad.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND VARIEDAD = NOT IN ('" + TxtVariedad.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Variedad no contiene: " + TxtVariedad.Text);
                   }
                   else if (IVariedad.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND VARIEDAD = NOT LIKE ('" + TxtVariedad.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Variedad no incluye: " + TxtVariedad.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtZona.Text != "" && Fields[i] == "ZONA")
                {
                   if (IZona.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND ZONA IN ('" + TxtZona.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Zona contiene: " + TxtZona.Text);
                   }
                   else if (IZona.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND ZONA = LIKE ('" + TxtZona.Text.Replace(",", "','") + "')"; 
                       DrVistaEmpleado.Items.Add("Zona incluye: " + TxtZona.Text);
                   }
                   else if (IZona.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND ZONA = NOT IN ('" + TxtZona.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Zona no contiene: " + TxtZona.Text);
                   }
                   else if (IZona.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND ZONA = NOT LIKE ('" + TxtZona.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Zona no incluye: " + TxtZona.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtBFechaIni.Text != "" && Fields[i] == "FECHA_INI")
                {
                   if (IFechaIni.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND FECHA_INI IN ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Fecha Inicio contiene: " + TxtBFechaIni.Text);
                   }
                   else if (IFechaIni.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND FECHA_INI = LIKE ('" + TxtBFechaIni.Text.Replace(",", "','") + "')"; 
                       DrVistaEmpleado.Items.Add("Fecha Inicio incluye: " + TxtBFechaIni.Text);
                   }
                   else if (IFechaIni.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND FECHA_INI = NOT IN ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha Inicio no contiene: " + TxtBFechaIni.Text);
                   }
                   else if (IFechaIni.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND FECHA_INI = NOT LIKE ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Fecha Inicio no incluye: " + TxtBFechaIni.Text);
                   }
                    else if (IFechaIni.Attributes["class"] == "fa fa-chevron-left fa-2x")
                    {
                        Filtros += " AND FECHA_INI < ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha Inicio menor que: " + TxtBFechaIni.Text);
                    }
                    else if (IFechaIni.Attributes["class"] == "fa fa-chevron-right fa-2x")
                    {
                        Filtros += " AND FECHA_INI > ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha Inicio mayor que: " + TxtBFechaIni.Text);
                    }
                    else if (IFechaIni.Attributes["class"] == "fa fa-random fa-2x")
                    {
                        Filtros += " AND FECHA_INI <> ('" + TxtBFechaIni.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha Inicio distinto de: " + TxtBFechaIni.Text);
                    }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtBFechaFin.Text != "" && Fields[i] == "FECHA_FIN")
                {
                   if (IFechaFin.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND FECHA_FIN IN ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Fecha final contiene: " + TxtBFechaFin.Text);
                   }
                   else if (IFechaFin.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND FECHA_FIN = LIKE ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Fecha final incluye: " + TxtBFechaFin.Text);
                   }
                   else if (IFechaFin.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND FECHA_FIN = NOT IN ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha final no contiene: " + TxtBFechaFin.Text);
                   }
                   else if (IFechaFin.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND FECHA_FIN = NOT LIKE ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Fecha final no incluye: " + TxtBFechaFin.Text);
                   }
                    else if (IFechaFin.Attributes["class"] == "fa fa-chevron-left fa-2x")
                    {
                        Filtros += " AND FECHA_FIN < ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha final menor que: " + TxtBFechaFin.Text);
                    }
                    else if (IFechaFin.Attributes["class"] == "fa fa-chevron-right fa-2x")
                    {
                        Filtros += " AND FECHA_FIN > ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha final mayor que: " + TxtBFechaFin.Text);
                    }
                    else if (IFechaFin.Attributes["class"] == "fa fa-random fa-2x")
                    {
                        Filtros += " AND FECHA_FIN <> ('" + TxtBFechaFin.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Fecha final distinto de: " + TxtBFechaFin.Text);
                    }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }

               if (TxtBTablet.Text != "" && Fields[i] == "RECOTABLET")
                {
                   if (ITablet.Attributes["class"] == "fa fa-circle fa-2x")
                   {
                       Filtros += " AND RECOTABLET IN ('" + TxtBTablet.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Tablet contiene: " + TxtBTablet.Text);
                   }
                   else if (ITablet.Attributes["class"] == "fa fa-adjust fa-2x")
                   {
                        Filtros += " AND RECOTABLET = LIKE ('" + TxtBTablet.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Tablet incluye: " + TxtBTablet.Text);
                   }
                   else if (ITablet.Attributes["class"] == "fa fa-circle-o fa-2x")
                   {
                        Filtros += " AND RECOTABLET = NOT IN ('" + TxtBTablet.Text.Replace(",", "','") + "')";
                        DrVistaEmpleado.Items.Add("Tablet no contiene: " + TxtBTablet.Text);
                   }
                   else if (ITablet.Attributes["class"] == "fa fa-dot-circle-o fa-2x")
                   {
                       Filtros += " AND RECOTABLET = NOT LIKE ('" + TxtBTablet.Text.Replace(",", "','") + "')";
                       DrVistaEmpleado.Items.Add("Tablet no incluye: " + TxtBTablet.Text);
                   }
                    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
                }
            }

            this.Session["Filtro"] = Filtros;
        }



        private void Carga_tablaTareas()
        {
            //Tabla REC_TAREAS
            string SQL = "COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_EMPLEADOS, HORA_EMPLEADO ,TABLET, CODFINCA ,  DESCRFINCA , ZONA, DESCRZONAZ, TAREA ,DESCRTAREA, INIFIN, DESCRINIFIN ";
            DataTable dt = null;
            string Dato = ""; // TxtNumero.Text;
            //Carga_tablaListaFiltro();
            string filtro = ""; // this.Session["Filtro"].ToString();
            //OrdenJornada();
            if (Dato == "") { Dato = "0"; }
            try
            {
                //this.Session["NumeroPalet"] = "0";

                filtro = filtro.Replace("WHERE", "AND");
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT COD_EMPLEADO ,NOMBRE ,APELLIDOS ,FECHA_EMPLEADOS ,HORA_EMPLEADO ,TABLET  ";
                    SQL += " ,CODFINCA ,DESCRFINCA ,ZONA ,DESCRZONAZ ,TAREA ,DESCRTAREA, INIFIN, DESCRINIFIN ";
                    SQL += " FROM [DESARROLLO].[dbo].REC_TAREAS  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
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
                    SQL = " SELECT COD_EMPLEADO ,NOMBRE ,APELLIDOS ,FECHA_EMPLEADOS ,HORA_EMPLEADO ,TABLET  ";
                    SQL += " ,CODFINCA ,DESCRFINCA ,ZONA ,DESCRZONAZ ,TAREA ,DESCRTAREA INIFIN, DESCRINIFIN ";
                    SQL += " FROM [RIOERESMA].[dbo].REC_TAREAS  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
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

                    Lberror.Text += " 2- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_tablaLista " + SQL + Environment.NewLine;


                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ORDER BY POSICIONCAMION ";
                    //dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;
                gvEmpleado.DataSource = dt;
                gvEmpleado.DataBind();

                //this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
                //CreaPalets(dt);
                gvEmpleado.EditIndex = -1;

                //Busca Error
                Lberror.Text = "";

            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaTareas --> " + mm.Message);

                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }


        private void Carga_tablaTrabajos(string sortExpression = null)
        {
            //Tabla REC_TRABAJOS
            string SQL = "";
            DataTable dt = null;
            string Dato = ""; // TxtNumero.Text;
            //Carga_tablaListaFiltro();
            string filtro = ""; // this.Session["Filtro"].ToString();
            //OrdenJornada();
            if (Dato == "") { Dato = "0"; }
            try
            {
                //this.Session["NumeroPalet"] = "0";
                SQL = " COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_EMPLEADOS, HORA_EMPLEADO, TABLET, CODFINCA, DESCRFINCA, ZONA, DESCRZONAZ, TAREA,DESCRTAREA, FECHA_JORNADA, HORAINI, HORAFIN, TRANSCURRIDO, RECOTABLET ";
                Carga_FiltrosGral(SQL);

                string Filtro = this.Session["Filtro"].ToString();

                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_EMPLEADOS, HORA_EMPLEADO, TABLET, CODFINCA, DESCRFINCA, ";
                    SQL += " ZONA, DESCRZONAZ, TAREA,DESCRTAREA, FECHA_JORNADA, HORAINI, HORAFIN, TRANSCURRIDO, RECOTABLET ";
                    SQL += " FROM REC_TRABAJOS  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
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
                    SQL = " SELECT COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_EMPLEADOS, HORA_EMPLEADO, TABLET, CODFINCA, DESCRFINCA, ";
                    SQL += " ZONA, DESCRZONAZ, TAREA,DESCRTAREA, FECHA_JORNADA, HORAINI, HORAFIN, TRANSCURRIDO, RECOTABLET ";
                    SQL += " FROM REC_TRABAJOS  ";
                    SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
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

                    Lberror.Text += " 2- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_tablaLista " + SQL + Environment.NewLine;


                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ORDER BY POSICIONCAMION ";
                    //dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

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

                gvEmpleado.DataSource = dt;
                gvEmpleado.DataBind();

                //this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
                //CreaPalets(dt);
                gvEmpleado.EditIndex = -1;

                //Busca Error
                Lberror.Text = "";

            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaTrabajos --> " + mm.Message);

                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }

        private void Carga_tablaEmpleadosSQL()
        {

            string SQL = "Ninguno, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, CATEGORIA, FECHAALTA, COTIZACION, FECHABAJA, VIVIENDA";
            DataTable dt = null;
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
            //Tabla REC_Empleados
            //string SQL = "";
            DataTable dt = null;
            //string Dato = ""; // TxtNumero.Text;
            //if (Dato == "") { Dato = "0"; }
            try
            {
                //SQL = " SELECT ID, COD_EMPLEADO, NOMBRE, APELLIDOS, CENTRO, TRABAJO, CATEGORIA, FECHAALTA, FECHABAJA, VIVIENDA ";
                //SQL += " FROM REC_EMPLEADO  ";
                //SQL += " WHERE COD_EMPLEADO IS NOT NULL ";
                //SQL += " ORDER BY COD_EMPLEADO ";

                dt = Main.BuscaLote(SQL).Tables[0];
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaEmpleadosXLS --> " + mm.Message);

                Variables.Error = mm.Message;
            }
            return dt;
        }

        private void Carga_tablaEmpleados(string sortExpression = null)
        {
            //Tabla REC_Empleados
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
                    if (CheckTodo.Checked == true)
                    {
                        SQL += " AND FORMAT (FECHABAJA_CALCULADA, 'dd/MM/yyyy') = '" + this.Session["UltimaConsulta"].ToString() + "' ";
                    }
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
                string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }
        protected void gvTrabajos_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_Trabajos(e.SortExpression);
        }
        protected void gvEmpleado_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_tablaEmpleados(e.SortExpression);
        }
        protected void gvProduccion_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_tablaProduccion(e.SortExpression);
        }
        protected void gvJornada_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_tablaJornada(e.SortExpression);

        }
        protected void gvJornalHora_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_Jornal_Horas(e.SortExpression);
        }
        protected void gvJornalNomina_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_Jornal_Nominas(e.SortExpression);
        }
        protected void gvDestajoNomina_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_Destajo_Nomina(e.SortExpression);
        }
        protected void gvpanelTareas_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_panelTareas(e.SortExpression);
        }
        
        protected void gvProdImpDia_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_ProddiaImporte(e.SortExpression);
        }
        protected void gvResumenNomina_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_Nomina_resumen(e.SortExpression);
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
                string a = Main.Ficherotraza("ConsultaPalet --> " + mm.Message);

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


        //private void CreaPalets(DataTable dt)
        //{
        //    int i = Convert.ToInt32(this.Session["NumeroPalet"].ToString());

            
        //    //Lberror.Visible = true;
        //    //Lberror.Text += "Numero de palets: " + i;

        //    if (i == 0) { return; }
        //    //container2.Controls.Clear();
        //    fuego.Controls.Clear();
        //    agua.Controls.Clear();
        //    i = 0;
        //    foreach (DataRow filas in dt.Rows)
        //    {
        //        string MiContent = "drag" + filas["POSICIONCAMION"].ToString(); //drag1
        //        HtmlGenericControl DivContent = (HtmlGenericControl)FindControlRecursive(container2, MiContent);
        //        //HtmlGenericControl DivContent = (HtmlGenericControl)FindControl(MiContent);
        //        if(DivContent is null) { break; }
        //        //string miro = "Pos.Camión:" + filas["POSICIONCAMION"].ToString() + "\n";
        //        //miro += "Línea:" + filas["NUMERO_LINEA"].ToString() + "\n";
        //        //miro += "Empresa:" + filas["EMPRESA"].ToString() + "\n";
        //        //miro += "Cliente:" + filas["CLIENTEPROVEEDOR"].ToString() + "\n";
        //        //miro += "Nombre:" + filas["NOMBREFISCAL"].ToString() + "\n";
        //        //miro += "Articulo:" + filas["ARTICULO"].ToString() + "\n";
        //        //miro += "Ruta:" + filas["RUTA"].ToString() + "\n";
        //        //miro += "Número:" + filas["NUMERO"].ToString() + "\n";
        //        //miro += "uni:" + filas["UDSENCARGA"].ToString() + "\n";

        //        string miro =  filas["POSICIONCAMION"].ToString() + "\n";
        //        miro +=  filas["NUMERO_LINEA"].ToString() + "\n";
        //        miro +=  filas["EMPRESA"].ToString() + "\n";
        //        miro +=  filas["CLIENTEPROVEEDOR"].ToString() + "\n";
        //        miro +=  filas["NOMBREFISCAL"].ToString() + "\n";
        //        miro +=  filas["ARTICULO"].ToString() + "\n";
        //        miro +=  filas["RUTA"].ToString() + "\n";
        //        miro +=  filas["NUMERO"].ToString() + "\n";
        //        miro +=  filas["UDSENCARGA"].ToString() + "\n";

        //        DivContent.Attributes.Add("data-tooltip", miro);

        //        string MiImagen = "Imgdrag" + filas["POSICIONCAMION"].ToString(); // filas["NUMERO_LINEA"].ToString(); //Imgdrag1
        //        System.Web.UI.HtmlControls.HtmlImage Paletimg = (System.Web.UI.HtmlControls.HtmlImage)FindControlRecursive(container2, MiImagen);
        //       // System.Web.UI.WebControls.Image Paletimg = (System.Web.UI.WebControls.Image)FindControl(MiContent);
 
        //        if (filas["UDSENCARGA"].ToString().Contains("0."))
        //        {
        //            Paletimg.Attributes["src"] = "images/mediopalet200X300.png";
        //        }
        //        else
        //        {
        //            Paletimg.Attributes["src"] = "images/palet200X300.png";
        //        }

        //        string MiDivText = "dragText" + filas["POSICIONCAMION"].ToString(); // + filas["NUMERO_LINEA"].ToString(); //dragText1
        //        HtmlGenericControl DivText = (HtmlGenericControl)FindControlRecursive(container2, MiDivText);
        //        //HtmlGenericControl DivText = (HtmlGenericControl)FindControl(MiDivText);
        //        DivText.InnerHtml = filas["NUMERO"].ToString() + "-" + filas["ARTICULO"].ToString();
        //        //DivText.InnerHtml = filas["NUMERO"].ToString() + "< br/>" + filas["ARTICULO"].ToString();

        //        DivContent.Visible = true;
        //        if (filas["ESTADO"].ToString() == "0" || filas["ESTADO"].ToString() == "1")
        //        { 
        //        }
        //        else
        //        {
        //            HtmlGenericControl Mfuego = (HtmlGenericControl)FindControlRecursive(idPadre, "fuego");
        //            HtmlGenericControl Magua = (HtmlGenericControl)FindControlRecursive(idPadre, "agua");

        //            //if (Mfuego.Controls.Count > Magua.Controls.Count)
        //            //{
        //            //    Magua.Controls.Add(DivContent);
        //            //}
        //            //else if (agua.Controls.Count > Mfuego.Controls.Count)
        //            //{
        //            //    Mfuego.Controls.Add(DivContent);
        //            //}
        //            //else
        //            //{
        //            //    Mfuego.Controls.Add(DivContent);
        //            //}
        //            //al revés
        //            if (Magua.Controls.Count > Mfuego.Controls.Count)
        //            {
        //                Mfuego.Controls.Add(DivContent);
        //            }
        //            else if (Mfuego.Controls.Count > Magua.Controls.Count)
        //            {
        //                Magua.Controls.Add(DivContent);
        //            }
        //            else
        //            {
        //                Magua.Controls.Add(DivContent);
        //            }
        //        }
        //    }

        //    //Busca Error
        //    Lberror.Text = "";
        //    //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        //}

        //protected void MuevePalet_Click(object sender, EventArgs e)
        //{
        //    Button nombre = (Button)sender;
        //    string Id = nombre.ID;
        //    string Dato = ""; // TxtNumero.Text;
        //    string Parent = nombre.Parent.ID.ToString();
        //    string Miro = LbPosicionCamion.Text;
        //    HtmlGenericControl DivText = (HtmlGenericControl)FindControl(Parent);
        //    Parent = DivText.Parent.ID;

        //    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
        //    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
        //    SQL += " WHERE ID_CABECERA = " + Dato;
        //    SQL += " AND POSICIONCAMION = " + Id.Replace("Btndrag", "");
        //    Lberror.Text += SQL + "1- gvEmpleado_Selecciona " + Variables.mensajeserver;
        //    DataTable dt = Main.BuscaLote(SQL).Tables[0];
        //    Lberror.Text += " 1- gvEmpleado_Selecciona " + Variables.mensajeserver;
        //    gvJornada.DataSource = dt;
        //    gvJornada.DataBind();
        //    this.Session["NumeroPalet"] = dt.Rows.Count.ToString();

        //    Vista_Print(dt, Id.Replace("Btndrag", ""));
        //}



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

        //private void Carga_tablaNueva()
        //{
        //    string temporal = ""; //Jose
        //    //Lberror.Text = "";
        //    string SQL = "";
        //    string Filtros = "";
        //    DataTable dt = null;
        //    DataTable dtV = null;
        //    DataTable dtV2 = null;
        //    try
        //    {
        //        //
        //        //Petición no aplicar filtros a la consulta general y eliminarlos
        //        //
        //        //Carga_tablaListaFiltro();
        //        SQL = ""; // this.Session["Filtro"].ToString();
        //        Filtros = ""; // = this.Session["Filtro"].ToString();
        //        //Carga_Filtros();
        //        //
        //        if (Filtros == "")
        //        {
        //            if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
        //            {
        //                //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
        //                SQL = "1- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN ";
        //                DBHelper.DeleteProcedureTemp("");

        //                SQL = Main.BuscaGold("", "");
        //                Lberror.Text += SQL +  "2- Carga_tablaNueva BuscaGold " + Variables.mensajeserver;
        //                dtV = Main.BuscaLoteGold(SQL).Tables[0];
                        
        //                Lberror.Text += SQL + " 2- termina Carga_tablaNueva BuscaLoteGold " + Variables.mensajeserver;

        //                foreach (DataRow fila in dtV.Rows)
        //                {
        //                    SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
        //                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
        //                    SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
        //                    SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
        //                    SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
        //                    SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
        //                    SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
        //                    SQL += fila["SERIE_PED"].ToString() + "',(";
        //                    SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
        //                    SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
        //                    SQL += " INNER JOIN ZBANDEJAS C ";
        //                    SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
        //                    SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
        //                    SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";
        //                    Lberror.Text += "3- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                    DBHelper.ExecuteNonQuery(SQL);
        //                }



        //                //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
        //                SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
        //                SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
        //                SQL += " CASE ";
        //                SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
        //                SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
        //                SQL += " END AS UDSACARGAR, ";
        //                SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
        //                SQL += " FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";


        //                //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
        //                Lberror.Text += "4- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                dt = Main.BuscaLote(SQL).Tables[0];
        //                Lberror.Text += "4- Carga_tablaNueva " + SQL + Environment.NewLine;
        //            }
        //            else
        //            {
        //                //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
        //                if (temporal == "")
        //                {

        //                    SQL = "5- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN ";
        //                    DBHelper.DeleteProcedureTemp("");

        //                    SQL = Main.BuscaGold("", "");
        //                    Lberror.Text = "6- Carga_tablaNueva BuscaLoteGold" + Environment.NewLine;
        //                    dtV = Main.BuscaLoteGold(SQL).Tables[0];

        //                    foreach (DataRow fila in dtV.Rows)
        //                    {
        //                        SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
        //                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
        //                        SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
        //                        SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
        //                        SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
        //                        SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
        //                        SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
        //                        SQL += fila["SERIE_PED"].ToString() + "',(";
        //                        SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
        //                        SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
        //                        SQL += " INNER JOIN ZBANDEJAS C ";
        //                        SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
        //                        SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
        //                        SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";
        //                        Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                        DBHelper.ExecuteNonQuery(SQL);
        //                    }

        //                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
        //                }

        //                SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
        //                SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
        //                SQL += " CASE ";
        //                SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
        //                SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
        //                SQL += " END AS UDSACARGAR, ";
        //                SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
        //                SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";

        //                //SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
        //                //SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
        //                //SQL += " CASE ";
        //                //SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
        //                //SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
        //                //SQL += " END AS UDSACARGAR, ";
        //                //SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO ";
        //                //SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3 ";
        //                //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
        //                Lberror.Text += "8- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                dt = Main.BuscaLote(SQL).Tables[0];
        //            }
        //        }
        //        else //Si tiene filtros
        //        {
        //            if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
        //            {
        //                //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
        //                //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN ";
        //                Lberror.Text += "9-  DeleteProcedureTemp " + SQL + Environment.NewLine;
        //                DBHelper.DeleteProcedureTemp("");

        //                string Miro = "";
        //                if (this.Session["FiltroFecha"].ToString() != "")
        //                {
        //                    Miro += " AND " + this.Session["FiltroFecha"].ToString();
        //                }
        //                if (this.Session["FiltroRuta"].ToString() != "")
        //                {
        //                    Miro += " AND " + this.Session["FiltroRuta"].ToString();
        //                }
        //                if (this.Session["FiltroCliente"].ToString() != "")
        //                {
        //                    Miro += " AND " + this.Session["FiltroCliente"].ToString();
        //                }
        //                if (this.Session["FiltroEmpresa"].ToString() != "")
        //                {
        //                    Lberror.Text += "10- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
        //                    SQL = Main.BuscaGold(this.Session["FiltroEmpresa"].ToString(), Miro);
        //                    Lberror.Text += SQL + " 10- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
        //                }
        //                else
        //                {
        //                    Lberror.Text = "11- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
        //                    SQL = Main.BuscaGold("", Miro);
        //                    Lberror.Text = SQL +  "11- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
        //                }
        //                dtV = Main.BuscaLoteGold(SQL).Tables[0];

        //                foreach (DataRow fila in dtV.Rows)
        //                {
        //                    SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
        //                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
        //                    SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
        //                    SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
        //                    SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString() + "," + fila["UDSPENDIENTES"].ToString() + ",";
        //                    SQL += fila["UDSACARGAR"].ToString() + "," + fila["NUMPALET"].ToString() + ",'" + fila["FECHAENTREGA"].ToString() + "',";
        //                    SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
        //                    SQL += fila["SERIE_PED"].ToString() + "',(";
        //                    SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
        //                    SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
        //                    SQL += " INNER JOIN ZBANDEJAS C ";
        //                    SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
        //                    SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
        //                    SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";


        //                    Lberror.Text += "12- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                    DBHelper.ExecuteNonQuery(SQL);
        //                }

        //                //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN " + Filtros + " AND  ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";

        //                SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
        //                SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
        //                SQL += " CASE ";
        //                SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
        //                SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
        //                SQL += " END AS UDSACARGAR, ";
        //                SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
        //                SQL += " FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";
        //                //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN  ORDER BY ID ";
        //                Lberror.Text += "13- Carga_tablaNueva " + SQL + Environment.NewLine;
        //                dt = Main.BuscaLote(SQL).Tables[0];

        //            }
        //            else
        //            {

        //                //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
        //                if (temporal == "")
        //                {
        //                    SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN ORDER BY ID";
        //                    Lberror.Text += "14- DeleteProcedureTemp " + SQL + Environment.NewLine;

        //                    DBHelper.DeleteProcedureTemp("");

        //                    string Miro = "";
        //                    if (this.Session["FiltroFecha"].ToString() != "")
        //                    {
        //                        Miro += " AND " + this.Session["FiltroFecha"].ToString();
        //                    }
        //                    if (this.Session["FiltroRuta"].ToString() != "")
        //                    {
        //                        Miro += " AND " + this.Session["FiltroRuta"].ToString();
        //                    }
        //                    if (this.Session["FiltroCliente"].ToString() != "")
        //                    {
        //                        Miro += " AND " + this.Session["FiltroCliente"].ToString();
        //                    }
        //                    if (this.Session["FiltroEmpresa"].ToString() != "")
        //                    {
        //                        Lberror.Text += "15- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
        //                        SQL = Main.BuscaGold(this.Session["FiltroEmpresa"].ToString(), Miro);
        //                        Lberror.Text += SQL + " 15- Carga_tablaNueva ExecuteProcedureQueryGold - FiltroEmpresa - Miro" + Environment.NewLine;
        //                    }
        //                    else
        //                    {
        //                        Lberror.Text += "16- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
        //                        SQL = Main.BuscaGold("", Miro);
        //                        Lberror.Text += SQL + " 16- Carga_tablaNueva ExecuteProcedureQueryGold - Miro" + Environment.NewLine;
        //                    }

        //                    dtV = Main.BuscaLoteGold(SQL).Tables[0];

        //                    foreach (DataRow fila in dtV.Rows)
        //                    {
        //                        SQL = "INSERT INTO ZCARGA_ORDEN( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO, UDSPEDIDAS,";
        //                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, COMPUTER, SERIE_PED, UNIDADES)";
        //                        SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
        //                        SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["ARTICULO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
        //                        SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSENCARGA"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",";
        //                        SQL += fila["UDSACARGAR"].ToString().Replace(",", ".") + "," + fila["NUMPALET"].ToString().Replace(",", ".") + ",'" + fila["FECHAENTREGA"].ToString() + "',";
        //                        SQL += fila["ESTADO"].ToString() + ",'" + fila["RUTA"].ToString() + "','" + this.Session["ComputerName"].ToString() + "','";
        //                        SQL += fila["SERIE_PED"].ToString() + "',(";
        //                        SQL += " SELECT TOP 1 C.ZNUMERO_PLANTAS ";
        //                        SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
        //                        SQL += " INNER JOIN ZBANDEJAS C ";
        //                        SQL += " ON A.ZTIPO_PLANTA = C.ZTIPO_PLANTA ";
        //                        SQL += " WHERE A.ZCODGOLDEN = '" + fila["ARTICULO"].ToString().Trim() + "'";
        //                        SQL += " AND C.ZTIPO_FORMATO = 'CAJAS' ))";

                           
        //                        Lberror.Text += "17- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                        DBHelper.ExecuteNonQuery(SQL);
        //                    }

        //                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN " + Filtros + " AND ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
        //                }

        //                SQL = " SELECT EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, ARTICULO,";
        //                SQL += " UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, ";
        //                SQL += " CASE ";
        //                SQL += " WHEN ESTADO = 0 THEN (CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA)) ";
        //                SQL += " WHEN ESTADO = 1 THEN UDSACARGAR ";
        //                SQL += " END AS UDSACARGAR, ";
        //                SQL += " NUMPALET, ID, RUTA, FECHAENTREGA, ID_CABECERA, ESTADO, SERIE_PED, UNIDADES, CAJAS ";
        //                SQL += " FROM [RIOERESMA].[dbo].ZCARGA_ORDEN  WHERE ESTADO <> 3  ";
        //                //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_ORDEN ORDER BY ID ";
        //                Lberror.Text += "18- Carga_tablaNueva " + SQL + Environment.NewLine;

        //                dt = Main.BuscaLote(SQL).Tables[0];
        //            }
        //        }

        //        //busca Error donde no se puede depurar
        //        //Lberror.Text += " Mirar: " + Variables.Error + " " + SQL;
        //        //Calcula con lo que tiene en Lista de carga camión
        //        dt = Calcula_OrdenesCarga(dt, this.Session["EstadoCabecera"].ToString(), ""); // TxtNumero.Text);

        //        this.Session["MiConsulta"] = dt;

        //        gvProduccion.DataSource = dt;
        //        gvProduccion.DataBind();

        //        //busca Error donde no se puede depurar
        //        //Lberror.Visible = true;

        //        Lberror.Text = "";
        //    }
        //    catch (Exception mm)
        //    {
        //        Variables.Error += mm.Message;
        //        Lberror.Visible = true;
        //        Lberror.Text += ". Error: " + mm.Message;
        //    }


        //}

        private static DataTable Calcula_OrdenesCarga(DataTable dt, string Estado, string Numero)
        {
            string SQL = "";
            try
            {                
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
                    SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
                    SQL += " FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE ESTADO < 3  AND ID_CABECERA = " + Numero ;
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
                string a = Main.Ficherotraza("Calcula_OrdenesCarga --> " + Ex.Message);

            }
            return dt;
        }


        protected void gvEmpleado_Selecciona(string Index)
        {
            string miro = Index;
            this.Session["IDCabecera"] = miro;
            gvEmpleado.EditIndex = -1;

            if (this.Session["EstadoCabecera"].ToString() == "3")
            {
                Carga_tablaCabeceraClose();
            }
            else
            {
                Carga_tablaJornada();
            }

            
            PanelCabecera.Attributes.Add("style", "background-color:#f5f5f5");
            //gvEmpleado.DataBind();

            string SQL = "SELECT NUMERO, EMPRESA, PAIS, FECHAPREPARACION, FECHACARGA, TELEFONO, MATRICULA, TRANSPORTISTA, OBSERVACIONES, ESTADO FROM  ZCARGA_CABECERA WHERE ID = " + miro;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in dt.Rows)
            {
                //TxtNumero.Text = filas["NUMERO"].ToString();
                //int ps = DrEmpresa.Items.IndexOf(DrEmpresa.Items.FindByText(filas["EMPRESA"].ToString()));
                //DrEmpresa.SelectedIndex = ps;
                ////DrEmpresa.Text = filas["EMPRESA"].ToString();
                ////DrPais.Text = filas["PAIS"].ToString();
                //ps = DrPais.Items.IndexOf(DrPais.Items.FindByText(filas["PAIS"].ToString()));
                //DrPais.SelectedIndex = ps;
                //TxtFechaPrepara.Text = filas["FECHAPREPARACION"].ToString();
                //TxtFecha.Text = filas["FECHACARGA"].ToString();
                //TxtTelefono.Text = filas["TELEFONO"].ToString();
                //TxtMatricula.Text = filas["MATRICULA"].ToString();
                //TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();

                //TxtTransportista.Text = filas["TRANSPORTISTA"].ToString();
                //TxtPais.Text = filas["PAIS"].ToString();
                //TxtEmpresa.Text = filas["EMPRESA"].ToString();

                //ps = DrTransportista.Items.IndexOf(DrTransportista.Items.FindByText(filas["TRANSPORTISTA"].ToString()));
                //DrTransportista.SelectedIndex = ps;

                //TxtNumero.Enabled = false;
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

                SeleccionCabecera();

                //LbCabecera.Text = " ( Número: " + TxtNumero.Text + ", Empresa: " + DrEmpresa.SelectedItem.Value + ", Pais: " + DrPais.SelectedItem.Value + ", Fecha: " + TxtFecha.Text;
                //LbCabecera.Text += ", Teléfono: " + TxtTelefono.Text + ", Matricula: " + TxtMatricula.Text + ", Transportista: " + DrTransportista.SelectedItem.Value + ")";
                //BtnuevaCabecera.Text = "Cancelar";
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
            Lberror.Text +=  " 1- gvEmpleado_Selecciona " + Variables.mensajeserver;
            gvJornada.DataSource = dt;
            gvJornada.DataBind();
            this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
            //CreaPalets(dt);

            Carga_tablaEmpleados();

            gvEmpleado.EditIndex = -1;

            gvEmpleado.DataBind();
        }

        protected void gvJornada_Sube(string Index)
        {
            GridViewRow row = gvJornada.Rows[Convert.ToInt32(Index)];
            string miro = gvJornada.DataKeys[Convert.ToInt32(Index)].Value.ToString();
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

            TextBox txtBox = (TextBox)gvJornada.FindControl("TabLCarga"); //antes 10
            if (txtBox != null)
            {
                UNIDADES = Convert.ToDecimal(txtBox.Text);
            }
            txtBox = (TextBox)gvJornada.FindControl("TabLNumLinea"); //antes 13
            if (txtBox != null)
            {
                Numero = txtBox.Text;
            }
            //DBHelper.ExecuteNonQuery(SQL);

            string SQL = "UPDATE ZCARGA_ORDEN SET (UNIDADESENCARGA = UNIDADESENCARGA + UNIDADES) WHERE ID_CABECERA = " + miro;

            Lberror.Text += SQL + "1- gvJornada_Sube " + Variables.mensajeserver;
            DBHelper.ExecuteNonQuery(SQL);
            Lberror.Text += " 1- gvJornada_Sube " + Variables.mensajeserver;
            

            SQL = "DELETE FROM ZCARGA_LINEA WHERE ID_SECUENCIA = " + miro + " AND NUMERO_LINEA = " + Numero;


            Carga_tablaProduccion();
            Carga_tablaEmpleados();

            gvJornada.EditIndex = -1;

            gvJornada.DataBind();
        }

        private void Carga_Jornal_HorasSQL()
        {
            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, A.TOTALIMPORTE, A.TOTALTIEMPO";//HORAINI, HORAFIN,IMPORTEMINUTOS,
            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            //OrdenJornal_Horas();
            string filtros = this.Session["Filtro"].ToString();

            SQL = " SELECT row_number() OVER (ORDER BY A.NOMBRE, A.APELLIDOS, A.APELLIDOS) as ID, ";
            SQL += " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA,  ";
            //SQL += " RIGHT(CONVERT(DATETIME, HORAINI, 108), 8) AS HORAINI, RIGHT(CONVERT(DATETIME, HORAFIN, 108), 8) AS HORAFIN , ";
            SQL += "  A.TOTALIMPORTE, "; //TOTALTIEMPO,TOTALMINUTOS,IMPORTEMINUTOS,
            //SQL += " CONVERT(char(8), DATEADD(MINUTE, TOTALMINUTOS, ''), 114) TIEMPO ";
            SQL += " SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, A.TOTALTIEMPO, ''), 114),1,5) TIEMPO ,  ";
            SQL += " A.NHORAS, A.XHORAS, A.VNHORAS, A.VXHORAS, A.TIMPNHORAS, A.TIMPXHORAS ";
            //SQL += " SUBSTRING(TRANSCURRIDO,1,5) TIEMPO ";
            SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z ";
            SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
            SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
            SQL += " AND Z.BUSQUEDA = 1 ";

            if (filtros != "")
            {
                SQL += filtros;
            }
            SQL += " GROUP BY  A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, A.TOTALIMPORTE, A.TOTALTIEMPO, A.NHORAS, A.XHORAS, A.VNHORAS, A.VXHORAS, A.TIMPNHORAS, A.TIMPXHORAS ";
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA ";
            }
            this.Session["SQL"] = SQL;

        }

        public static DataTable Carga_Jornal_HorasXLS(string SQL)
        {
            //string MiSQL = SQL;
            //string SQL = " SELECT ID, COD_EMPLEADO, NOMBRE +' ' + APELLIDOS AS COMPLETO, FORMAT(FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA, RIGHT(CONVERT(DATETIME, HORAINI, 108), 8) AS HORAINI, ";
            //SQL += " RIGHT(CONVERT(DATETIME, HORAFIN, 108), 8) AS HORAFIN , ";
            //SQL += " TRANSCURRIDO ";
            //SQL += " FROM REC_JORNADA ";
            //SQL += " ORDER BY COD_EMPLEADO, FECHA_JORNADA ,HORAINI";

            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_Jornal_Horas(string sortExpression = null)
        {

            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, B.FECHA_JORNADA, A.TOTALIMPORTE, A.TOTALTIEMPO, A.RECOTABLET";//HORAINI, HORAFIN,IMPORTEMINUTOS,
            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            //OrdenDeGrid(DRJornalHora1, DRJornalHora2);
            //OrdenJornal_Horas();

            string filtros = this.Session["Filtro"].ToString();

            SQL = " SELECT row_number() OVER (ORDER BY A.NOMBRE, A.APELLIDOS, A.APELLIDOS) as ID, ";
            SQL += " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, FORMAT(A.FECHA_JORNADA, 'dd-MM-yyyy') AS FECHA,  ";
            //SQL += " RIGHT(CONVERT(DATETIME, HORAINI, 108), 8) AS HORAINI, RIGHT(CONVERT(DATETIME, HORAFIN, 108), 8) AS HORAFIN , ";
            SQL += "  A.TOTALIMPORTE, "; //TOTALTIEMPO,TOTALMINUTOS,IMPORTEMINUTOS,
            //SQL += " CONVERT(char(8), DATEADD(MINUTE, TOTALMINUTOS, ''), 114) TIEMPO ";
            SQL += " SUBSTRING(CONVERT(char(8), DATEADD(MINUTE, A.TOTALTIEMPO, ''), 114),1,5) TIEMPO   ";
            //SQL += " SUBSTRING(TRANSCURRIDO,1,5) TIEMPO ";
            SQL += " FROM REC_JORNADA A, REC_EMPLEADO Z ";
            SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
            SQL += " AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
            SQL += " AND Z.BUSQUEDA = 1 ";

            if (filtros != "")
            {
                //if (filtros.Contains("COD_EMPLEADO")) { filtros = filtros.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                //if (filtros.Contains("NOMBRE")) { filtros = filtros.Replace("NOMBRE", "A.NOMBRE"); }
                //if (filtros.Contains("APELLIDOS")) { filtros = filtros.Replace("APELLIDOS", "A.APELLIDOS"); }
                //if (filtros.Contains("FECHA_JORNADA")) { filtros = filtros.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                //if (filtros.Contains("TOTALIMPORTE")) { filtros = filtros.Replace("TOTALIMPORTE", "A.TOTALIMPORTE"); }
                //if (filtros.Contains("TOTALTIEMPO")) { filtros = filtros.Replace("TOTALTIEMPO", "A.TOTALTIEMPO"); }
                SQL += filtros;
            }
            SQL += " GROUP BY  A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_JORNADA, A.TOTALIMPORTE, A.TOTALTIEMPO ";
            if (ElOrden != "")
            {
                //if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                //if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                //if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                //if (ElOrden.Contains("FECHA_JORNADA")) { ElOrden = ElOrden.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                //if (ElOrden.Contains("TOTALIMPORTE")) { ElOrden = ElOrden.Replace("TOTALIMPORTE", "A.TOTALIMPORTE"); }
                //if (ElOrden.Contains("TOTALTIEMPO")) { ElOrden = ElOrden.Replace("TOTALTIEMPO", "A.TOTALTIEMPO"); }
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_JORNADA ";
            }

            Lberror.Text += SQL + "1- Carga_Jornal_Horas " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 2- Carga_Jornal_Horas " + Variables.mensajeserver;
            lbRowJornalHora.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvJornalHora.DataSource = dv;
            }
            else
            {
                gvJornalHora.DataSource = dt;
            }
            gvJornalHora.DataBind();

            //gvJornalHora.DataSource = dt;
            //gvJornalHora.DataBind();
        }

         private void  Carga_Jornal_NominasSQL()
        {
            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.NOMINA, A.CATEGORIA, A.TOTALMINUTOS,A.TOTAL, A.TIEMPO  ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));

            int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
            if (dias != 0)
            {
                //si no es mes completo
                SQL = " SELECT A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,A.DIASMES ";
                SQL += " ,A.VIVIENDA ";
                SQL += " ,A.ALQVIVIENDA ";
                SQL += " ,A.COSTEVIVIENDA ";
                //SQL += " ,A.NOMINA ";
                SQL += " ,C.CATEGORIA ";

                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'JORNAL' ";
                SQL += "   AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA ";
                SQL += "   GROUP BY N.TARIFA)  as IMPORTE ";

                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO  ";
                SQL += "   GROUP BY N.TARIFA) as TOTAL  ";

                SQL += " , (SELECT SUM(TOTALMINUTOS) FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TOTALMINUTOSS ";

                SQL += " , (SELECT RIGHT('000' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 3600))))), 3) + ':' + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 60) - FLOOR((sum(TOTALMINUTOS) * 60) / 3600) * 60)))), 2) ";
                SQL += " FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO ) AS TIEMPO ";

                SQL += " , (SELECT ";
                SQL += "   CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * 30 / R.DIASMES) ";
                SQL += "   WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * " + diasNaturales + " / R.DIASMES) END ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "   GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA, R.DIASMES) AS NOMINA ";


                SQL += " FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO C ";
                SQL += " WHERE B.TIPO = 'JORNAL' AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND C.BUSQUEDA = 1 ";

            }
            else
            {
                //si es mes completo
                SQL = " SELECT A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,A.DIASMES ";
                SQL += " ,A.VIVIENDA ";
                SQL += " ,A.ALQVIVIENDA ";
                SQL += " ,A.COSTEVIVIENDA ";
                //SQL += " ,A.NOMINA ";
                SQL += " ,C.CATEGORIA ";
                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'JORNAL' ";
                SQL += "   AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA ";
                SQL += "   GROUP BY N.TARIFA)  as IMPORTE ";

                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO  ";
                SQL += "   GROUP BY N.TARIFA) as TOTAL  ";

                SQL += " , (SELECT SUM(TOTALMINUTOS) FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TOTALMINUTOSS ";

                SQL += " , (SELECT RIGHT('000' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 3600))))), 3) + ':' + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 60) - FLOOR((sum(TOTALMINUTOS) * 60) / 3600) * 60)))), 2) ";
                SQL += " FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO ) AS TIEMPO ";

                SQL += " , (SELECT ";
                SQL += "   CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) ";
                SQL += "   WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) END ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "   GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA) AS NOMINA ";



                SQL += " FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO C ";
                SQL += " WHERE B.TIPO = 'JORNAL' AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND C.BUSQUEDA = 1 ";
            }

            if (filtros != "")
            {
                SQL += filtros;
            }
            //SQL += "GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, CATEGORIA, TARIFA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.TOTAL, A.TOTALIMPORTE, A.NOMINA ";
            SQL += "GROUP BY A.COD_EMPLEADO, C.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, CATEGORIA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA,  A.NOMINA, C.COTIZACION ";

            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO ";
            }

            this.Session["SQL"] = SQL;
            //string SQL = " SELECT ID, COD_EMPLEADO, NOMBRE +' ' + APELLIDOS AS COMPLETO, FORMAT(FECHA_JORNADA, 'dd-MM-yyyy')  AS FECHA, RIGHT(CONVERT(DATETIME, HORAINI, 108), 8) AS HORAINI, ";
            //SQL += " RIGHT(CONVERT(DATETIME, HORAFIN, 108), 8) AS HORAFIN , TRANSCURRIDO, IMPORTEMINUTOS  ";
            //SQL += " FROM REC_JORNADA ";
            //SQL += " ORDER BY COD_EMPLEADO, FECHA_JORNADA, HORAINI ";
            //Carga_Jornal_NominasXLS(SQL);
        }

        public static DataTable Carga_Jornal_NominasXLS(string SQL)
        {

            //string MiSQL = SQL;

            //string SQL = " SELECT ID, COD_EMPLEADO, NOMBRE +' ' + APELLIDOS AS COMPLETO, FORMAT(FECHA_JORNADA, 'dd-MM-yyyy')  AS FECHA, RIGHT(CONVERT(DATETIME, HORAINI, 108), 8) AS HORAINI, ";
            //SQL += " RIGHT(CONVERT(DATETIME, HORAFIN, 108), 8) AS HORAFIN , TRANSCURRIDO, IMPORTEMINUTOS  ";
            //SQL += " FROM REC_JORNADA ";
            //SQL += " ORDER BY COD_EMPLEADO, FECHA_JORNADA, HORAINI ";

            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_Jornal_Nominas(string sortExpression = null)
        {
            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, C.CATEGORIA, A.TOTALMINUTOS,   A.NOMINA, A.RECOTABLET  ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //OrdenDeGrid(DRJornalNomina1, DRJornalNomina2);
            //OrdenJornal_Nominas();

            int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));

            int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
            if (dias != 0)
            {
                //si no es mes completo
                SQL = " SELECT A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,A.DIASMES ";
                SQL += " ,A.VIVIENDA ";
                SQL += " ,A.ALQVIVIENDA ";
                SQL += " ,A.COSTEVIVIENDA ";
                //SQL += " ,A.NOMINA ";
                SQL += " ,C.CATEGORIA ";

                //SQL += " , CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) as IMPORTE ";

                //SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60)) ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'JORNAL' AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA)  as IMPORTE ";
                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'JORNAL' ";
                SQL += "   AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA ";
                SQL += "   GROUP BY N.TARIFA)  as IMPORTE ";

                //SQL += " , CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA as TOTAL ";

                //SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO) as TOTAL ";
                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO  ";
                SQL += "   GROUP BY N.TARIFA) as TOTAL  ";

                SQL += " , (SELECT SUM(TOTALMINUTOS) FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TOTALMINUTOSS ";

                SQL += " , (SELECT RIGHT('000' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 3600))))), 3) + ':' + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 60) - FLOOR((sum(TOTALMINUTOS) * 60) / 3600) * 60)))), 2) ";
                SQL += " FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO ) AS TIEMPO ";

                //SQL += " , (SELECT RIGHT('00' + CAST(FLOOR(COALESCE(sum(A.TOTALMINUTOS), 0) / 60) AS VARCHAR(8)), 3) + ':' + RIGHT('0' + CAST(FLOOR(COALESCE(sum(A.TOTALMINUTOS), 0) % 60) AS VARCHAR(2)), 2)  ";
                //SQL += "  FROM REC_JORNADA WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TIEMPO ";

                //SQL += " , CASE WHEN C.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA) * 30 / A.DIASMES) ";
                //SQL += "   WHEN C.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA) * " + diasNaturales + " / A.DIASMES) END AS NOMINA ";

                //SQL += " , (SELECT CASE WHEN C.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * 30 / R.DIASMES) ";
                //SQL += "   WHEN C.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * " + diasNaturales + " / R.DIASMES) END  ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO) AS NOMINA ";

                SQL += " , (SELECT ";
                SQL += "   CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * 30 / R.DIASMES) ";
                SQL += "   WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA) * " + diasNaturales + " / R.DIASMES) END ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "   GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA, R.DIASMES) AS NOMINA ";


                SQL += " FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO C ";
                SQL += " WHERE B.TIPO = 'JORNAL' AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND C.BUSQUEDA = 1 ";

            }
            else
            {
                //si es mes completo
                SQL = " SELECT A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,A.DIASMES ";
                SQL += " ,A.VIVIENDA ";
                SQL += " ,A.ALQVIVIENDA ";
                SQL += " ,A.COSTEVIVIENDA ";
                //SQL += " ,A.NOMINA ";
                SQL += " ,C.CATEGORIA ";
                //SQL += " , CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) as IMPORTE ";

                //SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60)) ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'JORNAL' AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA)  as IMPORTE ";

                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'JORNAL' ";
                SQL += "   AND R.COD_EMPLEADO = C.COD_EMPLEADO AND N.SUBTIPO = C.CATEGORIA ";
                SQL += "   GROUP BY N.TARIFA)  as IMPORTE ";


                //SQL += " , CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA as TOTAL ";

                //SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO) as TOTAL ";


                SQL += " , (SELECT CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - A.COSTEVIVIENDA  ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N    WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO  ";
                SQL += "   GROUP BY N.TARIFA) as TOTAL  ";

                SQL += " , (SELECT SUM(TOTALMINUTOS) FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TOTALMINUTOSS ";

                SQL += " , (SELECT RIGHT('000' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 3600))))), 3) + ':' + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(TOTALMINUTOS) * 60) / 60) - FLOOR((sum(TOTALMINUTOS) * 60) / 3600) * 60)))), 2) ";
                SQL += " FROM REC_JORNADA WHERE A.COD_EMPLEADO = COD_EMPLEADO GROUP BY COD_EMPLEADO ) AS TIEMPO ";
                //SQL += " , (SELECT RIGHT('00' + CAST(FLOOR(COALESCE(sum(A.TOTALMINUTOS), 0) / 60) AS VARCHAR(8)), 3) + ':' + RIGHT('0' + CAST(FLOOR(COALESCE(sum(A.TOTALMINUTOS), 0) % 60) AS VARCHAR(2)), 2) ";
                //SQL += "  FROM REC_JORNADA WHERE COD_EMPLEADO = A.COD_EMPLEADO GROUP BY COD_EMPLEADO) AS TIEMPO ";

                //SQL += " , CASE WHEN C.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA)) ";
                //SQL += "   WHEN C.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(A.TOTALMINUTOS) * B.TARIFA / 60))) - A.COSTEVIVIENDA))  END AS NOMINA ";

                //SQL += " , (SELECT CASE WHEN C.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) ";
                //SQL += "   WHEN C.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2),(CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA))  ";
                //SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                //SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO) AS NOMINA ";

                SQL += " , (SELECT ";
                SQL += "   CASE WHEN C.COTIZACION = 6 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) ";
                SQL += "   WHEN C.COTIZACION = 7 THEN CONVERT(DECIMAL(8, 2), (CONVERT(Decimal(8, 2), ((sum(R.TOTALMINUTOS) * N.TARIFA / 60))) - R.COSTEVIVIENDA)) END ";
                SQL += "   FROM REC_JORNADA R, REC_TARIFAS N ";
                SQL += "   WHERE N.TIPO = 'VIVIENDA' AND R.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += "   GROUP BY R.COD_EMPLEADO, R.COSTEVIVIENDA, N.TARIFA) AS NOMINA ";



                SQL += " FROM REC_JORNADA A, REC_TARIFAS B, REC_EMPLEADO C ";
                SQL += " WHERE B.TIPO = 'JORNAL' AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                SQL += " AND C.BUSQUEDA = 1 ";
            }
            //Conversion dates
            ////SQL += " ,CONVERT(VARCHAR(12), FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400)) + ' dias, '  ";
            ////SQL += "  + CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 3600) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400) * 24)) + ' Horas, '  ";
            ////SQL += " + CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 60) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 3600) * 60)) + ' Minutos, ' AS TIEMPO ";

            ////SQL += " ,CONVERT(VARCHAR(12), FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400)) + ':'  ";
            ////SQL += "  + CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 3600) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400) * 24)) + ':'  ";
            ////SQL += " + CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 60) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 3600) * 60)) AS TIEMPO ";

            ////SQL += " ,RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400)))), 2) + ':'  ";
            ////SQL += " + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 3600) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 86400) * 24)))), 2) + ':'  ";
            ////SQL += " + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 60) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 3600) * 60)))), 2) AS TIEMPO  ";

            //SQL += ", RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 3600))))),2) + ':'   + RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(12), FLOOR(((sum(A.TOTALMINUTOS) * 60) / 60) - FLOOR((sum(A.TOTALMINUTOS) * 60) / 3600) * 60)))), 2) AS TIEMPO   ";

            if (filtros != "")
            {
                //if (filtros.Contains("COD_EMPLEADO")) { filtros = filtros.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                //if (filtros.Contains("NOMBRE")) { filtros = filtros.Replace("NOMBRE", "A.NOMBRE"); }
                //if (filtros.Contains("APELLIDOS")) { filtros = filtros.Replace("APELLIDOS", "A.APELLIDOS"); }
                //if (filtros.Contains("CATEGORIA")) { filtros = filtros.Replace("CATEGORIA", "C.CATEGORIA"); }
                //if (filtros.Contains("TOTALMINUTOS")) { filtros = filtros.Replace("TOTALMINUTOS", "A.TOTALMINUTOS"); }
                SQL += filtros;
            }
            //SQL += "GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, CATEGORIA, TARIFA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.TOTAL, A.TOTALIMPORTE, A.NOMINA ";
            SQL += "GROUP BY A.COD_EMPLEADO, C.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, CATEGORIA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA,  A.NOMINA, C.COTIZACION ";

            if (ElOrden != "")
            {
                //if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                //if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                //if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                //if (ElOrden.Contains("CATEGORIA")) { ElOrden = ElOrden.Replace("CATEGORIA", "C.CATEGORIA"); }
                //if (ElOrden.Contains("TOTALMINUTOS")) { ElOrden = ElOrden.Replace("TOTALMINUTOS", "A.TOTALMINUTOS"); }
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO ";
            }
            
            Lberror.Text += SQL + "1- Carga_Jornal_Nominas " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 2- Carga_Jornal_Nominas " + Variables.mensajeserver;
            lbRowJornalNomina.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvJornalNomina.DataSource = dv;
            }
            else
            {
                gvJornalNomina.DataSource = dt;
            }
            gvJornalNomina.DataBind();

            //gvJornalNomina.DataSource = dt;
            //gvJornalNomina.DataBind();
        }

        public static DataTable Carga_Destajo_NominaXLS(string SQL)
        {
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_Destajo_NominaSQL()
        {
            //string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS,  DESCRCAJAS, CAJAS, DESCRMANOJOS, MANOJOS , DESCRENVASE, A.ENVASE, B.TAREA, PLANTAS, IMPORTE, A.TABLET, B.ZONA ";
            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS,  DESCRCAJAS, CAJAS,  B.TAREA, PLANTAS, IMPORTE "; //, A.TABLET, B.ZONA ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //STRING_AGG desde SQL 2017

            SQL = " SELECT DISTINCT(A.COD_EMPLEADO) AS COD_EMPLEADO, A.NOMBRE,   A.APELLIDOS, 		  ";
            SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";

            SQL += " STUFF((SELECT '| ' + DESCRENVASE + ' ' ";
            SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
            SQL += " GROUP BY DESCRENVASE ";
            SQL += " FOR XML PATH('')),1,1,'') AS DESCRCAJAS, ";

            SQL += " STUFF((SELECT '| ' +  CONVERT(VARCHAR(255), SUM(REC_PRODUCCION.MARCAENVASE)) + ' ' ";
            SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
            SQL += " GROUP BY DESCRENVASE ";
            SQL += " FOR XML PATH('')),1,1,'') AS CAJAS, ";

            SQL += " (SELECT SUM(REC_PRODUCCION.PLANTAS) AS CAJAS  FROM REC_PRODUCCION  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS) AS PLANTAS, ";

            SQL += " (SELECT CONVERT(DECIMAL(8, 2), SUM(REC_PRODUCCION.PLANTAS * REC_TARIFAS.TARIFA)) ";
            SQL += " FROM REC_PRODUCCION, REC_TARIFAS  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO    AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  AND REC_TARIFAS.TIPO = 'DESTAJO' ";
            SQL += " AND REC_PRODUCCION.ENVASE = REC_TARIFAS.SUBTIPO   GROUP BY REC_PRODUCCION.COD_EMPLEADO ) AS IMPORTE ";


            SQL += "  FROM REC_PRODUCCION A  ";
            SQL += "  INNER JOIN REC_EMPLEADO Z ON A.COD_EMPLEADO = Z.COD_EMPLEADO  AND Z.BUSQUEDA = 1  ";
            SQL += "  WHERE A.COD_EMPLEADO IS NOT NULL  ";




            if (filtros != "")
            {
                SQL += filtros;
            }

            SQL += "  GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.MARCAENVASE  ";

            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, FECHA_EMPLEADOS ";
            }

            this.Session["SQL"] = SQL;

        }



        private void Carga_Destajo_Nomina(string sortExpression = null)
        {
            //número Incremental ROW_NUMBER() OVER (ORDER BY COD_EMPLEADO) AS ID, ó identity(int,1,1) as id, si tiene un into la select

            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS,  DESCRCAJAS, CAJAS,  B.TAREA, PLANTAS, IMPORTE "; //, A.TABLET, B.ZONA ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //OrdenDeGrid(DRDestajoNomina1, DRDestajoNomina2);
            //OrdenDestajo_Nomina();

            SQL = " SELECT DISTINCT(A.COD_EMPLEADO) AS COD_EMPLEADO, A.NOMBRE,   A.APELLIDOS, 		  ";
            SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";

            SQL += " STUFF((SELECT '| ' + DESCRENVASE + ' ' ";
            SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
            SQL += " GROUP BY DESCRENVASE ";
            SQL += " FOR XML PATH('')),1,1,'') AS DESCRCAJAS, ";

            SQL += " STUFF((SELECT '| ' +  CONVERT(VARCHAR(255), SUM(REC_PRODUCCION.MARCAENVASE)) + ' ' ";
            SQL += " FROM  REC_PRODUCCION WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  ";
            SQL += " GROUP BY DESCRENVASE ";
            SQL += " FOR XML PATH('')),1,1,'') AS CAJAS, ";

            SQL += " (SELECT SUM(REC_PRODUCCION.PLANTAS) AS CAJAS  FROM REC_PRODUCCION  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO  AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS) AS PLANTAS, ";

            SQL += " (SELECT CONVERT(DECIMAL(8, 2), SUM(REC_PRODUCCION.PLANTAS * REC_TARIFAS.TARIFA)) ";
            SQL += " FROM REC_PRODUCCION, REC_TARIFAS  WHERE A.COD_EMPLEADO = REC_PRODUCCION.COD_EMPLEADO    AND REC_PRODUCCION.FECHA_EMPLEADOS = A.FECHA_EMPLEADOS  AND REC_TARIFAS.TIPO = 'DESTAJO' ";
            SQL += " AND REC_PRODUCCION.ENVASE = REC_TARIFAS.SUBTIPO   GROUP BY REC_PRODUCCION.COD_EMPLEADO ) AS IMPORTE ";


            SQL += "  FROM REC_PRODUCCION A  ";
            SQL += "  INNER JOIN REC_EMPLEADO Z ON A.COD_EMPLEADO = Z.COD_EMPLEADO  AND Z.BUSQUEDA = 1  ";
            SQL += "  WHERE A.COD_EMPLEADO IS NOT NULL  ";

            if (filtros != "")
            {
                SQL += filtros;
            }

            SQL += "  GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.MARCAENVASE  ";

            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, FECHA_EMPLEADOS ";
            }

            //Lberror.Text += SQL + "1- Carga_Destajo_Nomina " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 2- Carga_Destajo_Nomina " + Variables.mensajeserver;
            lbRowDestajoNomina.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvDestajoNomina.DataSource = dv;
            }
            else
            {
                gvDestajoNomina.DataSource = dt;
            }
            gvDestajoNomina.DataBind();


            //gvDestajoNomina.DataSource = dt;
            //gvDestajoNomina.DataBind();

        }


        private void Carga_Nomina_resumenSQL()
        {
            string SQL = " COD_EMPLEADO, NOMBRE, APELLIDOS, CATEGORIA, PLANTAS, DIASMES, VIVIENDA, ALQVIVIENDA, COSTEVIVIENDA ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();
            //OrdenResumenNomina();
            //OrdenDeGrid(DRResumenNomina1, DRResumenNomina2);

            int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));

            int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
            if (dias != 0)
            {
                //si no es mes completo
                SQL = " SELECT ";
                SQL += " A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,B.CATEGORIA ";
                SQL += " ,SUM(A.PLANTAS)   AS PLANTAS ";
                SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)))) AS IMPORTE ";
                SQL += " , (CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)))) - A.COSTEVIVIENDA) AS TOTAL ";
                SQL += " , A.DIASMES ";
                SQL += " , A.VIVIENDA ";
                SQL += " , A.ALQVIVIENDA ";
                SQL += " , A.COSTEVIVIENDA ";
                SQL += " , CASE WHEN B.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)) - A.COSTEVIVIENDA)  * 30 / A.DIASMES)) ";
                SQL += "   WHEN B.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)) - A.COSTEVIVIENDA) * " + diasNaturales + " / A.DIASMES)) ";
                SQL += "   ELSE - 1 END AS NOMINA ";
                SQL += " FROM REC_PRODUCCION A,  REC_EMPLEADO B, REC_TARIFAS C ";
                SQL += " WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                SQL += " AND C.TIPO = 'DESTAJO' ";
                SQL += " AND C.SUBTIPO = A.ENVASE ";
                SQL += " AND B.BUSQUEDA = 1 ";
            }
            else
            {
                //si es mes completo
                SQL = " SELECT ";
                SQL += " A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,B.CATEGORIA ";
                SQL += " ,SUM(A.PLANTAS)   AS PLANTAS ";
                SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ))) AS IMPORTE ";
                SQL += " , (CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ))) - A.COSTEVIVIENDA) AS TOTAL ";
                SQL += " , A.DIASMES ";
                SQL += " , A.VIVIENDA ";
                SQL += " , A.ALQVIVIENDA ";
                SQL += " , A.COSTEVIVIENDA ";
                SQL += " , CASE WHEN B.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ) - A.COSTEVIVIENDA)  - A.COSTEVIVIENDA)) ";
                SQL += "   WHEN B.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ) - A.COSTEVIVIENDA)  - A.COSTEVIVIENDA))  ";
                SQL += "   ELSE - 1 END AS NOMINA ";
                SQL += " FROM REC_PRODUCCION A,  REC_EMPLEADO B, REC_TARIFAS C ";
                SQL += " WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                SQL += " AND C.TIPO = 'DESTAJO' ";
                SQL += " AND C.SUBTIPO = A.ENVASE ";
                SQL += " AND B.BUSQUEDA = 1 ";
            }
            if (filtros != "")
            {
                if (filtros.Contains("COD_EMPLEADO")) { filtros = filtros.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                if (filtros.Contains("NOMBRE")) { filtros = filtros.Replace("NOMBRE", "A.NOMBRE"); }
                if (filtros.Contains("APELLIDOS")) { filtros = filtros.Replace("APELLIDOS", "A.APELLIDOS"); }
                if (filtros.Contains("CATEGORIA")) { filtros = filtros.Replace("CATEGORIA", "A.CATEGORIA"); }
                if (filtros.Contains("PLANTAS")) { filtros = filtros.Replace("PLANTAS", "A.PLANTAS"); }
                if (filtros.Contains("IMPORTE")) { filtros = filtros.Replace("IMPORTE", "A.IMPORTE"); }

                SQL += filtros;
            }
            //SQL += " GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, B.CATEGORIA, C.TARIFA ";
            SQL += " GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, B.CATEGORIA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.NOMINA, B.COTIZACION ";

            if (ElOrden != "")
            {
                if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                if (ElOrden.Contains("CATEGORIA")) { ElOrden = ElOrden.Replace("CATEGORIA", "A.CATEGORIA"); }
                if (ElOrden.Contains("PLANTAS")) { ElOrden = ElOrden.Replace("PLANTAS", "A.PLANTAS"); }
                if (ElOrden.Contains("IMPORTE")) { ElOrden = ElOrden.Replace("IMPORTE", "A.IMPORTE"); }
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO ";
            }

            this.Session["SQL"] = SQL;
        }

        public static DataTable Carga_Nomina_resumenXLS(string SQL)
        {
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_TrabajosSQL()
        {
            string SQL = "ID, COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_JORNADA, HORAINI, HORAFIN, TRANSCURRIDO, RECOTABLET, TABLET, TOTALTIEMPO, CODFINCA, DESCRFINCA, ZONA, DESCRZONAZ, TAREA, DESCRTAREA   ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //OrdenDeGrid(DrTrabajo0, DrTrabajo1);
            //OrdenResumenNomina();
            SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,   ";
            SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, "; //A.TABLET,
            //SQL += " A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, "; //A.TABLET,
            SQL += " A.ZHORAS, A.ZESTADO,    ";
            SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
            SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
            SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
            SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
            SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
            SQL += " AND B.TIPO = 'JORNAL' ";
            SQL += " AND C.CATEGORIA = B.SUBTIPO ";
            SQL += " AND C.BUSQUEDA = 1 ";


            //SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,  ";
            //SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
            //SQL += " A.HORA_EMPLEADO,  RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA, ";
            //SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN,  ";
            //SQL += " A.ZHORAS, A.ZESTADO,   ";
            //SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE, ";
            //SQL += " A.NHORAS, A.XHORAS, A.VNHORAS, A.VXHORAS, A.TIMPNHORAS, A.TIMPXHORAS ";
            //SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
            //SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
            //SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
            //SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
            //SQL += " AND B.TIPO = 'JORNAL' ";
            //SQL += " AND C.CATEGORIA = B.SUBTIPO ";


            if (filtros != "")
            {
                if (filtros.Contains("COD_EMPLEADO")) { filtros = filtros.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                if (filtros.Contains("NOMBRE")) { filtros = filtros.Replace("NOMBRE", "A.NOMBRE"); }
                if (filtros.Contains("APELLIDOS")) { filtros = filtros.Replace("APELLIDOS", "A.APELLIDOS"); }
                if (filtros.Contains("FECHA_JORNADA")) { filtros = filtros.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                if (filtros.Contains("HORAINI")) { filtros = filtros.Replace("HORAINI", "A.HORAINI"); }
                if (filtros.Contains("HORAFIN")) { filtros = filtros.Replace("HORAFIN", "A.HORAFIN"); }
                if (filtros.Contains("TRANSCURRIDO")) { filtros = filtros.Replace("TRANSCURRIDO", "A.TRANSCURRIDO"); }
                if (filtros.Contains("RECOTABLET")) { filtros = filtros.Replace("RECOTABLET", "A.RECOTABLET"); }
                if (filtros.Contains("TABLET")) { filtros = filtros.Replace("TABLET", "B.TABLET"); }
                if (filtros.Contains("TOTALTIEMPO")) { filtros = filtros.Replace("TOTALTIEMPO", "A.TOTALTIEMPO"); }
                if (filtros.Contains("CODFINCA")) { filtros = filtros.Replace("CODFINCA", "B.CODFINCA"); }
                if (filtros.Contains("DESCRFINCA")) { filtros = filtros.Replace("DESCRFINCA", "B.DESCRFINCA"); }
                if (filtros.Contains("ZONA")) { filtros = filtros.Replace("ZONA", "B.ZONA"); }
                if (filtros.Contains("DESCRZONAZ")) { filtros = filtros.Replace("DESCRZONAZ", "B.DESCRZONAZ"); }
                if (filtros.Contains("DESCRTAREA")) { filtros = filtros.Replace("DESCRTAREA", "B.DESCRTAREA"); }
                if (filtros.Contains("TAREA")) { filtros = filtros.Replace("TAREA", "B.TAREA"); }

                SQL += filtros;
            }

            SQL += " GROUP by A.COD_EMPLEADO,A.NOMBRE ,A.APELLIDOS , A.FECHA_EMPLEADOS, "; // A.TABLET,  ";
            SQL += "  A.ZHORAS, A.ZESTADO, ZTOTALMINUTOS, B.TARIFA  "; //A.ZONA, A.DESCRZONAZ ,A.TAREA, A.DESCRTAREA, A.CODFINCA, A.DESCRFINCA , 


            if (ElOrden != "")
            {
                if (ElOrden.Contains("COD_EMPLEADO")) { ElOrden = ElOrden.Replace("COD_EMPLEADO", "A.COD_EMPLEADO"); }
                if (ElOrden.Contains("NOMBRE")) { ElOrden = ElOrden.Replace("NOMBRE", "A.NOMBRE"); }
                if (ElOrden.Contains("APELLIDOS")) { ElOrden = ElOrden.Replace("APELLIDOS", "A.APELLIDOS"); }
                if (ElOrden.Contains("FECHA_JORNADA")) { ElOrden = ElOrden.Replace("FECHA_JORNADA", "A.FECHA_JORNADA"); }
                if (ElOrden.Contains("HORAINI")) { ElOrden = ElOrden.Replace("HORAINI", "A.HORAINI"); }
                if (ElOrden.Contains("HORAFIN")) { ElOrden = ElOrden.Replace("HORAFIN", "A.HORAFIN"); }
                if (ElOrden.Contains("TRANSCURRIDO")) { ElOrden = ElOrden.Replace("TRANSCURRIDO", "A.TRANSCURRIDO"); }
                if (ElOrden.Contains("RECOTABLET")) { ElOrden = ElOrden.Replace("RECOTABLET", "A.RECOTABLET"); }
                if (ElOrden.Contains("TABLET")) { ElOrden = ElOrden.Replace("TABLET", "B.TABLET"); }
                if (ElOrden.Contains("TOTALTIEMPO")) { ElOrden = ElOrden.Replace("TOTALTIEMPO", "A.TOTALTIEMPO"); }
                if (ElOrden.Contains("CODFINCA")) { ElOrden = ElOrden.Replace("CODFINCA", "B.CODFINCA"); }
                if (ElOrden.Contains("DESCRFINCA")) { ElOrden = ElOrden.Replace("DESCRFINCA", "B.DESCRFINCA"); }
                if (ElOrden.Contains("ZONA")) { ElOrden = ElOrden.Replace("ZONA", "B.ZONA"); }
                if (ElOrden.Contains("DESCRZONAZ")) { ElOrden = ElOrden.Replace("DESCRZONAZ", "B.DESCRZONAZ"); }
                if (ElOrden.Contains("DESCRTAREA")) { ElOrden = ElOrden.Replace("DESCRTAREA", "B.DESCRTAREA"); }
                if (ElOrden.Contains("TAREA")) { ElOrden = ElOrden.Replace("TAREA", "B.TAREA"); }

                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS ";
            }

            this.Session["SQL"] = SQL;

        }

        public static DataTable Carga_TrabajosXLS(string SQL)
        {
            //Tabla REC_TRABAJOS
            DataTable dt = null;
            try
            {
                dt = Main.BuscaLote(SQL).Tables[0];
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TrabajosXLS --> " + mm.Message);

                Variables.Error = mm.Message;
            }
            return dt;
        }

        private void Carga_Trabajos(string sortExpression = null)
        {

            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO , A.TABLET,  A.CODFINCA ,  A.DESCRFINCA , A.ZONA, A.DESCRZONAZ, A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN ";

            Carga_FiltrosGral(SQL);
            string filtros = this.Session["Filtro"].ToString();
            //Tabla REC_TAREAS
            DataTable dt = null;
            string Dato = ""; // TxtNumero.Text;

            if (Dato == "") { Dato = "0"; }
            try
            {
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,  ";
                    //SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
                    //SQL += " A.HORA_EMPLEADO,  RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA, ";
                    //SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN,  ";
                    //SQL += " A.ZHORAS, A.ZESTADO,   ";
                    //SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
                    //SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
                    //SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    //SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    //SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
                    //SQL += " AND B.TIPO = 'JORNAL' ";
                    //SQL += " AND C.CATEGORIA = B.SUBTIPO ";
                    //SQL += " AND C.BUSQUEDA = 1 ";

                    SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,   ";
                    SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, "; //A.TABLET,
                    //SQL += " A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, "; //A.TABLET,
                    SQL += " A.ZHORAS, A.ZESTADO,    ";
                    SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
                    SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
                    SQL += " AND B.TIPO = 'JORNAL' ";
                    SQL += " AND C.CATEGORIA = B.SUBTIPO ";
                    SQL += " AND C.BUSQUEDA = 1 ";

                    if (filtros != "")
                    {
                        SQL += filtros;
                    }

                    SQL += " GROUP by A.COD_EMPLEADO,A.NOMBRE ,A.APELLIDOS , A.FECHA_EMPLEADOS, "; // A.TABLET,  ";
                    SQL += "  A.ZHORAS, A.ZESTADO, ZTOTALMINUTOS, B.TARIFA  "; //A.ZONA, A.DESCRZONAZ ,A.TAREA, A.DESCRTAREA, A.CODFINCA, A.DESCRFINCA , 

                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS ";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + "1- Carga_gvpanelTareas " + SQL + Environment.NewLine;
                }
                else
                {
                    //SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,  ";
                    //SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
                    //SQL += " A.HORA_EMPLEADO,  RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA, ";
                    //SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN,  ";
                    //SQL += " A.ZHORAS, A.ZESTADO,   ";
                    //SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
                    //SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
                    //SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    //SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    //SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
                    //SQL += " AND B.TIPO = 'JORNAL' ";
                    //SQL += " AND C.CATEGORIA = B.SUBTIPO ";
                    //SQL += " AND C.BUSQUEDA = 1 ";

                    SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,   ";
                    SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, ";
                    //SQL += " A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, "; //A.TABLET,
                    SQL += " A.ZHORAS, A.ZESTADO,    ";
                    SQL += " CONVERT(DECIMAL(8, 2), ((A.ZTOTALMINUTOS * (B.TARIFA / 60)))) AS IMPORTE ";
                    SQL += " FROM REC_TAREAS A,  REC_TARIFAS B, REC_EMPLEADO C ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL ";
                    SQL += " AND A.COD_EMPLEADO = C.COD_EMPLEADO ";
                    SQL += " AND(A.ZHORAS <> '' AND A.ZHORAS NOT LIKE '00:00%') ";
                    SQL += " AND B.TIPO = 'JORNAL' ";
                    SQL += " AND C.CATEGORIA = B.SUBTIPO ";
                    SQL += " AND C.BUSQUEDA = 1 ";


                    if (filtros != "")
                    {
                        SQL += filtros;
                    }

                    SQL += " GROUP by A.COD_EMPLEADO,A.NOMBRE ,A.APELLIDOS , A.FECHA_EMPLEADOS, "; // A.TABLET,  ";
                    SQL += "  A.ZHORAS, A.ZESTADO, ZTOTALMINUTOS, B.TARIFA  "; //A.ZONA, A.DESCRZONAZ ,A.TAREA, A.DESCRTAREA, A.CODFINCA, A.DESCRFINCA , 

                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS ";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_Trabajos " + SQL + Environment.NewLine;
                }
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_Trabajos --> " + mm.Message);

                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }

            LbCountTrabajo.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvTrabajos.DataSource = dv;
            }
            else
            {
                gvTrabajos.DataSource = dt;
            }
            gvTrabajos.DataBind();

            //gvTrabajos.DataSource = dt;
            //gvTrabajos.DataBind();

        }


        private void Carga_Nomina_resumen(string sortExpression = null)
        {
            string SQL = " A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, B.CATEGORIA, A.PLANTAS, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.TABLET, A.TAREA, A.ZONA ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //OrdenDeGrid(DRResumenNomina1, DRResumenNomina2);
            //OrdenResumenNomina();

            int diasNaturales = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));

            int dias = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT TOP 1 CONVERT(INT, DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,FECHA_JORNADA),0)))) - DIASMES AS DIAS FROM REC_JORNADA WHERE DIASMES IS NOT NULL ", null));
            if (dias != 0)
            {
                //si no es mes completo
                SQL = " SELECT ";
                SQL += " A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,B.CATEGORIA ";
                SQL += " ,SUM(A.PLANTAS)   AS PLANTAS ";
                SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)))) AS IMPORTE ";
                SQL += " , (CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)))) - A.COSTEVIVIENDA) AS TOTAL ";
                SQL += " , A.DIASMES ";
                SQL += " , A.VIVIENDA ";
                SQL += " , A.ALQVIVIENDA ";
                SQL += " , A.COSTEVIVIENDA ";
                SQL += " , CASE WHEN B.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)) - A.COSTEVIVIENDA)  * 30 / A.DIASMES)) ";
                SQL += "   WHEN B.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA)) - A.COSTEVIVIENDA) * " + diasNaturales + " / A.DIASMES)) ";
                SQL += "   ELSE - 1 END AS NOMINA ";
                SQL += " FROM REC_PRODUCCION A,  REC_EMPLEADO B, REC_TARIFAS C ";
                SQL += " WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                SQL += " AND C.TIPO = 'DESTAJO' ";
                SQL += " AND C.SUBTIPO = A.ENVASE ";
                SQL += " AND B.BUSQUEDA = 1 ";
            }
            else
            {
                //si es mes completo
                SQL = " SELECT ";
                SQL += " A.COD_EMPLEADO ";
                SQL += " ,A.NOMBRE ";
                SQL += " ,A.APELLIDOS ";
                SQL += " ,B.CATEGORIA ";
                SQL += " ,SUM(A.PLANTAS)   AS PLANTAS ";
                SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ))) AS IMPORTE ";
                SQL += " , (CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ))) - A.COSTEVIVIENDA) AS TOTAL ";
                SQL += " , A.DIASMES ";
                SQL += " , A.VIVIENDA ";
                SQL += " , A.ALQVIVIENDA ";
                SQL += " , A.COSTEVIVIENDA ";
                SQL += " , CASE WHEN B.COTIZACION = 6 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ) - A.COSTEVIVIENDA)  - A.COSTEVIVIENDA)) ";
                SQL += "   WHEN B.COTIZACION = 7 THEN  CONVERT(DECIMAL (8,2), CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS * C.TARIFA) ) - A.COSTEVIVIENDA)  - A.COSTEVIVIENDA))  ";
                SQL += "   ELSE - 1 END AS NOMINA ";
                SQL += " FROM REC_PRODUCCION A,  REC_EMPLEADO B, REC_TARIFAS C ";
                SQL += " WHERE A.COD_EMPLEADO = B.COD_EMPLEADO ";
                SQL += " AND C.TIPO = 'DESTAJO' ";
                SQL += " AND C.SUBTIPO = A.ENVASE ";
                SQL += " AND B.BUSQUEDA = 1 ";
            }


            if (filtros != "")
            {
                SQL += filtros;
            }
            SQL += " GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, B.CATEGORIA, A.DIASMES, A.VIVIENDA, A.ALQVIVIENDA, A.COSTEVIVIENDA, A.NOMINA, B.COTIZACION ";
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO ";
            }

            Lberror.Text += SQL + "1- Carga_Nomina_resumen " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 2- Carga_Nomina_resumen " + Variables.mensajeserver;
            lbRowResumenNomina.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvResumenNomina.DataSource = dv;
            }
            else
            {
                gvResumenNomina.DataSource = dt;
            }
            gvResumenNomina.DataBind();

            //gvResumenNomina.DataSource = dt;
            //gvResumenNomina.DataBind();

        }

        public static DataTable Carga_ProddiaImporteXLS(string SQL)
        {
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_ProddiaImporteSQL()
        {
            string SQL = " COD_EMPLEADO, NOMBRE, APELLIDOS, FECHA_EMPLEADOS ,PLANTAS, IMPORTE ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();
            //OrdenProddiaImporte();
            //OrdenDeGrid(DrProdImpDia1, DrProdImpDia2);

            SQL = " SELECT ";
            SQL += " A.COD_EMPLEADO ";
            SQL += " , A.NOMBRE ";
            SQL += " , A.APELLIDOS ";
            SQL += " , FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS ";
            SQL += " ,SUM(A.PLANTAS) AS PLANTAS ";
            SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS) * B.TARIFA))) AS IMPORTE ";
            SQL += "  FROM REC_PRODUCCION A, REC_TARIFAS B, REC_EMPLEADO Z ";
            SQL += "  WHERE B.TIPO = 'DESTAJO' ";
            SQL += "  AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
            SQL += "  AND A.ENVASE = B.SUBTIPO ";
            SQL += "  AND Z.BUSQUEDA = 1 ";

            if (filtros != "")
            {
                SQL += filtros;
            }
            SQL += "   GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, B.TARIFA ";
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += "  ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS";
            }
            this.Session["SQL"] = SQL;
        }


        private void Carga_panelTareasSQL()
        {
            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO ,A.TABLET, A.CODFINCA ,  A.DESCRFINCA , A.ZONA, A.DESCRZONAZ, A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,";
            SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, 	  ";
            SQL += " A.HORA_EMPLEADO, ";
            SQL += " RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA,  ";
            SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN, ";
            SQL += " A.ZHORAS, A.ZESTADO, A.ZPERIODO ";
            SQL += " FROM [DESARROLLO].[dbo].REC_TAREAS A, REC_EMPLEADO B  ";
            SQL += " WHERE A.COD_EMPLEADO IS NOT NULL";
            SQL += " AND A.COD_EMPLEADO = B.COD_EMPLEADO ";
            SQL += " AND B.BUSQUEDA = 1 ";
            if (filtros != "")
            {
                SQL += filtros;
            }
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS,  A.HORA_EMPLEADO ";
            }

            this.Session["SQL"] = SQL;
        }

        public static DataTable Carga_panelTareaXLS(string SQL)
        {
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            return dt;
        }

        private void Carga_panelTareas(string sortExpression = null)
        {
            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO ,A.TABLET, A.CODFINCA ,  A.DESCRFINCA , A.ZONA, A.DESCRZONAZ, A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN ";

            Carga_FiltrosGral(SQL);
            string filtros = this.Session["Filtro"].ToString();
            //Tabla REC_TAREAS
            DataTable dt = null;
            string Dato = ""; // TxtNumero.Text;

            if (Dato == "") { Dato = "0"; }
            try
            {
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,";
                    SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, 	  ";
                    SQL += " A.HORA_EMPLEADO, ";
                    SQL += " RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA,  ";
                    SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN, ";
                    SQL += " A.ZHORAS, A.ZESTADO, A.ZPERIODO, FORMAT(A.HORA_AJUSTADA, 'hh:mm:ss') AS HORA_AJUSTADA ";
                    SQL += " FROM [DESARROLLO].[dbo].REC_TAREAS A, REC_EMPLEADO B  ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL";
                    SQL += " AND A.COD_EMPLEADO = B.COD_EMPLEADO ";
                    SQL += " AND B.BUSQUEDA = 1 ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS,  A.HORA_EMPLEADO ";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + "1- Carga_gvpanelTareas " + SQL + Environment.NewLine;
                }
                else 
                {
                    SQL = " SELECT A.COD_EMPLEADO ,A.NOMBRE ,A.APELLIDOS ,";
                    SQL += " FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS, 	  ";
                    SQL += " A.HORA_EMPLEADO,";
                    SQL += " RIGHT(CONVERT(DATETIME, A.HORA_EMPLEADO, 108), 8) AS HORA,  ";
                    SQL += " A.TABLET, A.CODFINCA ,A.DESCRFINCA ,A.ZONA ,A.DESCRZONAZ ,A.TAREA ,A.DESCRTAREA, A.INIFIN, A.DESCRINIFIN, ";
                    SQL += " A.ZHORAS, A.ZESTADO, A.ZPERIODO, FORMAT(A.HORA_AJUSTADA, 'hh:mm:ss') AS HORA_AJUSTADA ";
                    SQL += " FROM [RIOERESMA].[dbo].REC_TAREAS A , REC_EMPLEADO B  ";
                    SQL += " WHERE A.COD_EMPLEADO IS NOT NULL";
                    SQL += " AND A.COD_EMPLEADO = B.COD_EMPLEADO ";
                    SQL += " AND B.BUSQUEDA = 1 ";
                    if (filtros != "")
                    {
                        SQL += filtros;
                    }
                    if (ElOrden != "")
                    {
                        SQL += ElOrden;
                    }
                    else
                    {
                        SQL += " ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS, A.HORA_EMPLEADO ";
                    }

                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_gvpanelTareas " + SQL + Environment.NewLine;
                }

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvpanelTareas.DataSource = dv;
                }
                else
                {
                    gvpanelTareas.DataSource = dt;
                }

                LbPanelRaw.Text = "Registros: " + dt.Rows.Count;

                gvpanelTareas.DataBind();
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_gvpanelTareas --> " + mm.Message);

                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }



        private void Carga_ProddiaImporte(string sortExpression = null)
        {
            string SQL = "A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS ,B.FECHA_EMPLEADOS , A.PLANTAS, A.IMPORTE, A.TABLET, A.ENVASE, A.TAREA, A.ZONA ";

            Carga_FiltrosGral(SQL);
            //Carga_tablaListaFiltros(SQL);
            string filtros = this.Session["Filtro"].ToString();

            //OrdenDeGrid(DrProdImpDia1, DrProdImpDia2);
            //OrdenProddiaImporte();


            SQL = " SELECT ";
            SQL += " A.COD_EMPLEADO ";
            SQL += " , A.NOMBRE ";
            SQL += " , A.APELLIDOS ";
            SQL += " , FORMAT(A.FECHA_EMPLEADOS, 'dd-MM-yyyy') AS FECHA_EMPLEADOS ";
            SQL += " ,SUM(A.PLANTAS) AS PLANTAS ";
            SQL += " , CONVERT(DECIMAL(8, 2), ((SUM(A.PLANTAS) * B.TARIFA))) AS IMPORTE ";
            SQL += "  FROM REC_PRODUCCION A, REC_TARIFAS B, REC_EMPLEADO Z ";
            SQL += "  WHERE B.TIPO = 'DESTAJO' ";
            SQL += "  AND A.COD_EMPLEADO = Z.COD_EMPLEADO ";
            SQL += "  AND A.ENVASE = B.SUBTIPO ";
            SQL += "  AND Z.BUSQUEDA = 1 ";
            if (filtros != "")
            {
                SQL += filtros;
            }
            SQL += "   GROUP BY A.COD_EMPLEADO, A.NOMBRE, A.APELLIDOS, A.FECHA_EMPLEADOS, B.TARIFA ";
            if (ElOrden != "")
            {
                SQL += ElOrden;
            }
            else
            {
                SQL += "  ORDER BY A.COD_EMPLEADO, A.FECHA_EMPLEADOS";
            }

            Lberror.Text += SQL + "1- Carga_ProddiaImporte " + Variables.mensajeserver;
            DataTable dt = Main.BuscaLote(SQL).Tables[0];
            Lberror.Text += " 2- Carga_ProddiaImporte " + Variables.mensajeserver;
            lbRowProddiaImporte.Text = "Registros: " + dt.Rows.Count;

            if (sortExpression != null)
            {
                DataView dv = dt.AsDataView();
                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                dv.Sort = sortExpression + " " + this.SortDirection;
                gvProdImpDia.DataSource = dv;
            }
            else
            {
                gvProdImpDia.DataSource = dt;
            }
            gvProdImpDia.DataBind();

            //gvProdImpDia.DataSource = dt;
            //gvProdImpDia.DataBind();

        }

        protected void gvJornada_Camion(string Index)
        {

            GridViewRow row = (GridViewRow)gvJornada.Rows[Convert.ToInt32(Index)];

            string miro = gvJornada.DataKeys[Convert.ToInt32(Index)].Value.ToString();
            string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_tablaEmpleados();

            gvJornada.EditIndex = -1;

            gvJornada.DataBind();
        }

        protected void gvProduccion_BajaOrden(string Index)
        {
            GridViewRow row = gvProduccion.Rows[Convert.ToInt32(Index)];
            string miro = gvProduccion.DataKeys[Convert.ToInt32(Index)].Value.ToString();

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

            TextBox txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOCargar") as TextBox);
            //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
            if (txtBox != null)
            {
                UNIDADES = Convert.ToDecimal(txtBox.Text);
            }

            txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
            //txtBox = (TextBox)(row.Cells[14].Controls[0]);
            if (txtBox != null)
            {
                NUMPALET = Convert.ToDecimal(txtBox.Text);
            }

            txtBox = (gvProduccion.Rows[Indice].Cells[10].FindControl("TabOCabecera") as TextBox);
            //txtBox = (TextBox)(row.Cells[15].Controls[0]);
            if (txtBox != null)
            {
                Cabecera = txtBox.Text;
            }

            REPARTO = (UNIDADES / NUMPALET);

            decimal Unidad = 1.00M;
            int Linea = 0;
            int N = 0;

            Lberror.Text += SQL + "1- gvProduccion_BajaOrden " + Variables.mensajeserver;
            Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            Lberror.Text += " 1- gvProduccion_BajaOrden " + Variables.mensajeserver;
            
            if (Con is System.DBNull)
            {
                N = 1;
            }
            else
            {
                N = Convert.ToInt32(Con) + 1;
            }
            Lberror.Text += SQL + "2- gvProduccion_BajaOrden " + Variables.mensajeserver;
            Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE ID_CABECERA =" + Cabecera, null);
            Lberror.Text += " 2- gvProduccion_BajaOrden " + Variables.mensajeserver;
            
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
                SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, HASTA )";
                SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
                SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA, ";
                SQL += " ID_CABECERA + '|' + CLIENTEPROVEEDOR + '|' + NUMERO + '|' + LINEA + '|' + " + N;
                SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                Lberror.Text += SQL + "3- gvProduccion_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 3- gvProduccion_BajaOrden " + Variables.mensajeserver;
                

                //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
                //DBHelper.ExecuteNonQuery(SQL);
                NUMPALET = NUMPALET - Unidad;
                N += 1;
                Linea += 1;
            }
            if (NUMPALET > 0)
            {
                SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, HASTA  )";
                SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
                SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", ID_CABECERA, ";
                SQL += " ID_CABECERA + '|' + CLIENTEPROVEEDOR + '|' + NUMERO + '|' + LINEA + '|' + " + N;
                SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                Lberror.Text += SQL + "4- gvProduccion_BajaOrden " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 4- gvProduccion_BajaOrden " + Variables.mensajeserver;

                //SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
                //DBHelper.ExecuteNonQuery(SQL);
                NUMPALET = NUMPALET - Unidad;
            }

            SQL = "UPDATE ZCARGA_ORDEN SET ESTADO = 1 WHERE ID = " + miro;
            Lberror.Text += SQL + "5- gvProduccion_BajaOrden " + Variables.mensajeserver;
            DBHelper.ExecuteNonQuery(SQL);
            Lberror.Text += " 5- gvProduccion_BajaOrden " + Variables.mensajeserver;

            this.Session["NumeroPalet"] = Linea.ToString();

            Carga_tablaProduccion();
            Carga_tablaEmpleados();

            gvProduccion.EditIndex = -1;

            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvProduccion.DataSource = dt;
            gvProduccion.DataBind();



            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvProduccion.DataSource = dt;
            //gvProduccion.DataBind();
        }


        //protected void PrintGridView(object sender, EventArgs e)
        //{
        //    //Disable Paging if all Pages need to be Printed.
        //    GridView Grid = null;
                
        //    HtmlButton btn = (HtmlButton)sender;
        //    if(btn.ID == "btPrintCabecera")
        //    {
        //        Grid = gvEmpleado as GridView;
        //    }
        //    if (btn.ID == "BtPrintOrden")
        //    {
        //        Grid = gvProduccion as GridView;
        //    }
        //    if (btn.ID == "BtPrintListas")
        //    {
        //        Grid = gvJornada as GridView;
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