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
using System.Drawing;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Configuration;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace Satelite
{
    public partial class Flujos : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;
        public string CampoOrden = "";

        private string Tabla = "";
        private string TablaObj = "";
        private string Campos = "";

        public System.Data.DataTable dtMacros = null;
        public System.Data.DataTable dtMacro = null;
        public System.Data.DataTable dtAntiMacro = null;

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Principal.aspx"); //Default

            try
            {
                if (Session["Session"] != null)
                {
                }
                else
                {
                    Server.Transfer("Login.aspx"); //Default
                }

                if (this.Session["MiNivel"].ToString() != "9")
                {
                    Server.Transfer("Inicio.aspx"); //Default
                }


                if (!IsPostBack)
                {
                    CampoOrden = "ZDESCRIPCION";
                    this.Session["Edicion"] = "0";

                    this.Session["FormatoCampos"] = null;
                    this.Session["idflujo"] = null;
                    this.Session["idestado"] = null;
                    this.Session["TablaObj"] = "";
                    this.Session["TablaName"] = "";
                    this.Session["EstadoCondicion"] = "";
                    this.Session["iddummie"] = "";
                    this.Session["proceso"] = "";
                    this.Session["Laplantilla"] = "";
                    this.Session["Elmarcador"] = "";
                    this.Session["Elproceso"] = "";


                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Server.Transfer("Inicio.aspx"); //Default
                    }

                    if(this.Session["Edicion"].ToString() == "1")
                    {
                        if(this.Session["proceso"].ToString() == "profile") { EditaProfile(1); }
                        if (this.Session["proceso"].ToString() == "proceso") { EditaProceso(1); }
                        if (this.Session["proceso"].ToString() == "plantilla") { EditaPlantilla(1); }
                        if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }

                    }

                    LbIDArchivo.InnerHtml = "Relaciones ";
                    this.Session["IDArchivo"] = 1;

                   
                    

                    Actualiza_Archivos();
                  



                }
            }
            catch (Exception ex)
            {
                string a = Main.ETrazas("", "1", " AltaArchivos.Page_load --> Error:" + ex.Message);
                Server.Transfer("Login.aspx");
            }
        }



        private void Actualiza_Archivos()
        {

            //dlNivel.DataValueField = "ZID";
            //dlNivel.DataTextField = "ZDESCRIPCION";
            DataTable dt3 = new DataTable();
            dt3 = Main.CargaNivel().Tables[0];
            this.Session["Niveles"] = dt3;
            //dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
            //dlNivel.DataBind();

            DrFlujos.Items.Clear();

            DataTable dt = new DataTable();
            dt = Main.CargaFlujos("0", "0").Tables[0];
            this.Session["Flujos"] = dt;

            DrFlujos.DataValueField = "ZID";
            DrFlujos.DataTextField = "ZDESCRIPCION";

            DrFlujos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrFlujos.DataSource = dt;
            DrFlujos.DataBind();

            DrFlujoProfile.DataValueField = "ZID";
            DrFlujoProfile.DataTextField = "ZDESCRIPCION";
            DrFlujoProfile.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrFlujoProfile.DataSource = dt;
            DrFlujoProfile.DataBind();

            

            dt = Main.CargaFlujosEstados(0).Tables[0];
            this.Session["FlujosEstados"] = dt;

            dt = Main.CargaEstadosFl(0).Tables[0];
            this.Session["EstadosFlujos"] = dt;

            DrEstado.DataValueField = "ZID";
            DrEstado.DataTextField = "ZDESCRIPCION";

            //DrEstado.DataSource = dt;
            //DrEstado.DataBind();

            DrArchivos.Items.Clear();
            DrConexion.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            DrConexion.DataValueField = "ZID";
            DrConexion.DataTextField = "ZDESCRIPCION";

            DrArchivos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrConexion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));


            DrProcedimientoProfile.DataValueField = "ZID";
            DrProcedimientoProfile.DataTextField = "ZDESCRIPCION";
            DrProcedimientoProfile.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            string SQL = " SELECT ZID, ZDESCRIPCION FROM  ZPROCEDIMIENTOS ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            DrProcedimientoProfile.DataSource = dt1;
            DrProcedimientoProfile.DataBind();


            DrTemplates.DataValueField = "ZID";
            DrTemplates.DataTextField = "ZDESCRIPCION";
            DrTemplates.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            SQL = " SELECT ZID, ZDESCRIPCION FROM  ZTEMPLATE ";
            dt1 = Main.BuscaLote(SQL).Tables[0];
            DrTemplates.DataSource = dt1;
            DrTemplates.DataBind();

            dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            DrArchivos.DataSource = dt;
            DrConexion.DataSource = dt;
            DrArchivos.DataBind();
            DrConexion.DataBind();
            this.Session["Archivos"] = dt;

            DrArchivoProfile.DataValueField = "ZID";
            DrArchivoProfile.DataTextField = "ZDESCRIPCION";
            DrArchivoProfile.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrArchivoProfile.DataSource = dt;
            DrArchivoProfile.DataBind();

            


            Dratras.Items.Clear();
            Drsiguiente.Items.Clear();
            Dralternativo.Items.Clear();
            Drfinal.Items.Clear();

            Dratras.DataValueField = "ZID";
            Dratras.DataTextField = "ZDESCRIPCION";

            Drsiguiente.DataValueField = "ZID";
            Drsiguiente.DataTextField = "ZDESCRIPCION";

            Dralternativo.DataValueField = "ZID";
            Dralternativo.DataTextField = "ZDESCRIPCION";

            Drfinal.DataValueField = "ZID";
            Drfinal.DataTextField = "ZDESCRIPCION";

            //DrFlujos.DataValueField = "ZID";
            //DrFlujos.DataTextField = "ZDESCRIPCION";

            DrEstado.DataValueField = "ZID";
            DrEstado.DataTextField = "ZNOMBRE";

            Dratras.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drsiguiente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Dralternativo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drfinal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrEstado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrPagina.Items.Clear();
            DrPagina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            SQL = " SELECT ZID, ZDESCRIPCION FROM  ZCONFIGRUTASFILES ";
            dt1 = Main.BuscaLote(SQL).Tables[0];
            DrPagina.DataValueField = "ZID";
            DrPagina.DataTextField = "ZDESCRIPCION";
            DrPagina.DataSource = dt1;
            DrPagina.DataBind();

            DrPaginaProfile.Items.Clear();
            DrPaginaProfile.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrPaginaProfile.DataValueField = "ZID";
            DrPaginaProfile.DataTextField = "ZDESCRIPCION";
            SQL = " SELECT ZID, ZDESCRIPCION FROM  ZPAGINA ";
            dt1 = Main.BuscaLote(SQL).Tables[0];
            DrPaginaProfile.DataSource = dt1;
            DrPaginaProfile.DataBind();


            DataTable dtEstadosFlujos = Main.CargaEstadosFlujos("0","0").Tables[0];

            DrEstadoProfile.DataValueField = "ZID";
            DrEstadoProfile.DataTextField = "ZNOMBRE";
            DrEstadoProfile.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrEstadoProfile.DataSource = dtEstadosFlujos;
            DrEstadoProfile.DataBind();

            DrEstado.DataSource = dtEstadosFlujos;
            DrEstado.DataBind();

            Dratras.DataSource = dtEstadosFlujos;
            Dratras.DataBind();

            Drsiguiente.DataSource = dtEstadosFlujos;
            Drsiguiente.DataBind();

            Dralternativo.DataSource = dtEstadosFlujos;
            Dralternativo.DataBind();

            Drfinal.DataSource = dtEstadosFlujos;
            Drfinal.DataBind();

            //dlEstado.DataValueField = "ZID";
            //dlEstado.DataTextField = "ZDESCRIPCION";

            DataTable dt4 = new DataTable();
            dt4 = Main.CargaEstados().Tables[0];
            this.Session["Estados"] = dt4;
            //dlEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
            //dlEstado.DataBind();

            DrConexion.DataValueField = "ZID";
            DrConexion.DataTextField = "ZDESCRIPCION";
            //DataTable dt5 = new DataTable();
            //dt5 = Main.CargaConexiones().Tables[0];
            ////this.Session["Conexion"] = dt5;
            //DrConexion.DataSource = dt5; // EvaluacionSel.GargaQuery().Tables[0];
            //DrConexion.DataBind();

            //DrDuplicado.DataValueField = "ZID";
            //DrDuplicado.DataTextField = "ZDESCRIPCION";
            //dt5 = new DataTable();
            //dt5 = Main.CargaOpciones().Tables[0];
            //this.Session["Conexion"] = dt5;
            //DrDuplicado.DataSource = dt5; // EvaluacionSel.GargaQuery().Tables[0];
            //DrDuplicado.DataBind();

            //Dtipo.DataValueField = "ZID";
            //Dtipo.DataTextField = "ZDESCRIPCION";

            dt4 = new DataTable();
            dt4 = Main.CargaJerarquia().Tables[0];
            this.Session["TipoArchivos"] = dt4;
            //Dtipo.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
            //Dtipo.DataBind();

            Relaciones(1, "");
            if (this.Session["IDGridA"].ToString() != "")
            {
                //si es llamado desde otra página
                for (int i = 0; i < DrArchivos.Items.Count; i++)
                {
                    if (DrArchivos.Items[i].Value == this.Session["IDGridA"].ToString())
                    {
                        DrArchivos.SelectedIndex = i;
                        this.Session["idarchivo"] = DrArchivos.SelectedIndex.ToString();
                        break;
                    }
                }
                DrArchivos_SelectedIndexChanged(null, null);
            }



        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            MiOpenMenu();
        }
        protected void DrTemplatesTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListTemplaID.Items.RemoveAt(ListTempla.SelectedIndex);
            //ListTempla.Items.RemoveAt(ListTempla.SelectedIndex);
        }
        protected void btnDeleteArchivoTemplate_Click(object sender, EventArgs e)
        {
            ListTemplaID.Items.RemoveAt(ListTempla.SelectedIndex);
            ListTempla.Items.RemoveAt(ListTempla.SelectedIndex);
        }
        protected void btnDeleteFiltroProfile_Click(object sender, EventArgs e)
        {
            ListBox4.Items.RemoveAt(ListBox3.SelectedIndex);
            ListBox3.Items.RemoveAt(ListBox3.SelectedIndex);
        }

        protected void btnDeleteArDoc_Click(object sender, EventArgs e)
        {
            ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
        }

        protected void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            ListTemplateID.Items.RemoveAt(ListTemplate.SelectedIndex);
            ListTemplate.Items.RemoveAt(ListTemplate.SelectedIndex);
        }

        protected void btnDeleteProcesPlant_Click(object sender, EventArgs e)
        {
            ListPlantillasID.Items.RemoveAt(ListPlantillas.SelectedIndex);
            ListPlantillas.Items.RemoveAt(ListPlantillas.SelectedIndex);
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }
        protected void btnDeletePlantMarc_Click(object sender, EventArgs e)
        {
            ListPlantMarcaID.Items.RemoveAt(ListPlantMarca.SelectedIndex);
            ListPlantMarca.Items.RemoveAt(ListPlantMarca.SelectedIndex);
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }

        

        protected void ListBoxFiltroProfile_DoubleClick(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProfile(1);
            }
        }

        protected void DrArchivosTemp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ListTemplaListBoxArchivo_DoubleClick(object sender, EventArgs e)
        {

        }
        
        protected void ListBoxArchivo_DoubleClick(object sender, EventArgs e)
        {
           
        }
        protected void ListPlantMarca_DoubleClick(object sender, EventArgs e)
        {
        }

        protected void btnMuestraProcesos_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            PanelFlujos.Visible = false;
            PanelProcesos.Visible = true;
            divAsigEstado.Visible = false;
            //Carga Procesos

            string SQL = "SELECT ZID, ZDESCRIPCION, ZPLANTILLAS, ZID_ESTADO, ZQUERY, ZID_ARCHIVO, ZID_FLUJO, ZEJECUCION, ZPARAMETROS ";
            SQL += " FROM ZPROCESOS ORDER BY ZDESCRIPCION ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            DrProcesos.Items.Clear();
            DrProcesos.DataValueField = "ZID";
            DrProcesos.DataTextField = "ZDESCRIPCION";
            DrProcesos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrProcesos.DataSource = dt;
            DrProcesos.DataBind();


            SQL = "SELECT ZID, ZDESCRIPCION, ZRUTAENTRADA, ZRUTASALIDA, ZCOPYORIGINAL, ZSIGNPDF, ZRUTAALTERNATIVA ";
            SQL += " FROM ZPLANTILLAS ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrPlantillas.Items.Clear();
            DrPlantillas.DataValueField = "ZID";
            DrPlantillas.DataTextField = "ZDESCRIPCION";
            DrPlantillas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrPlantillas.DataSource = dt;
            DrPlantillas.DataBind();

            DrProcesoPlantilla.Items.Clear();
            DrProcesoPlantilla.DataValueField = "ZID";
            DrProcesoPlantilla.DataTextField = "ZDESCRIPCION";
            DrProcesoPlantilla.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrProcesoPlantilla.DataSource = dt;
            DrProcesoPlantilla.DataBind();
            

            SQL = "SELECT ZID, ZNOMBRE,  ZCAMPO, ZENTRADA, ZSALIDA, ZPAGINA, ZX, ZY, ZROTACION, ZSELLO, ZROOT ";
            SQL += " FROM ZMARCADORES ORDER BY ZNOMBRE ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrMarcadores.Items.Clear();
            DrMarcadores.DataValueField = "ZID";
            DrMarcadores.DataTextField = "ZNOMBRE";
            DrMarcadores.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrMarcadores.DataSource = dt;
            DrMarcadores.DataBind();

            DrMarcadoresPlantillas.Items.Clear();
            DrMarcadoresPlantillas.DataValueField = "ZID";
            DrMarcadoresPlantillas.DataTextField = "ZNOMBRE";
            DrMarcadoresPlantillas.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrMarcadoresPlantillas.DataSource = dt;
            DrMarcadoresPlantillas.DataBind();

            DrTipoMarcador.Items.Clear();
            DrTipoMarcador.DataValueField = "ZID";
            DrTipoMarcador.DataTextField = "ZDESCRIPCION";
            DrTipoMarcador.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));            

            SQL = "SELECT ZID, ZDESCRIPCION  ";
            SQL += " FROM ZTIPO_MARCADOR ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrTipoMarcador.DataSource = dt;
            DrTipoMarcador.DataBind();

            SQL = "SELECT ZID, ZDESCRIPCION  ";
            SQL += " FROM ZOPCION ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrEstadoIni.Items.Clear();
            DrEstadoIni.DataValueField = "ZID";
            DrEstadoIni.DataTextField = "ZDESCRIPCION";

            DrArchivoIni.Items.Clear();
            DrArchivoIni.DataValueField = "ZID";
            DrArchivoIni.DataTextField = "ZDESCRIPCION";

            DrFlujoIni.Items.Clear();
            DrFlujoIni.DataValueField = "ZID";
            DrFlujoIni.DataTextField = "ZDESCRIPCION";

            DrfinPDF.Items.Clear();
            DrfinPDF.DataValueField = "ZID";
            DrfinPDF.DataTextField = "ZDESCRIPCION";

            DrPrinter.Items.Clear();
            DrPrinter.DataValueField = "ZID";
            DrPrinter.DataTextField = "ZDESCRIPCION";
            
            DrRutaAlternativa.Items.Clear();
            DrRutaAlternativa.DataValueField = "ZID";
            DrRutaAlternativa.DataTextField = "ZDESCRIPCION";

            DrCopia.Items.Clear();
            DrCopia.DataValueField = "ZID";
            DrCopia.DataTextField = "ZDESCRIPCION";

            Drroot.Items.Clear();
            Drroot.DataValueField = "ZID";
            Drroot.DataTextField = "ZDESCRIPCION";
            Drroot.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrEjecucion.Items.Clear();
            DrEjecucion.DataValueField = "ZID";
            DrEjecucion.DataTextField = "ZDESCRIPCION";

            DrCopia.DataSource = dt;
            DrCopia.DataBind();
            DrCopia.SelectedIndex = -1;

            DrfinPDF.DataSource = dt;
            DrfinPDF.DataBind();
            DrfinPDF.SelectedIndex = -1;

            DrPrinter.DataSource = dt;
            DrPrinter.DataBind();
            DrPrinter.SelectedIndex = -1;

            DrRutaAlternativa.DataSource = dt;
            DrRutaAlternativa.DataBind();
            DrRutaAlternativa.SelectedIndex = -1;;

            DrEjecucion.DataSource = dt;
            DrEjecucion.DataBind();
            DrEjecucion.SelectedIndex = -1;

            SQL = "SELECT ZID, ZDESCRIPCION ";
            SQL += " FROM ZESTADOSFLUJO ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrEstadoIni.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrEstadoIni.DataSource = dt;
            DrEstadoIni.DataBind();
            DrEstadoIni.SelectedIndex = -1;

            SQL = "SELECT ZID, ZDESCRIPCION ";
            SQL += " FROM ZARCHIVOS ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrArchivoIni.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrArchivoIni.DataSource = dt;
            DrArchivoIni.DataBind();
            DrArchivoIni.SelectedIndex = -1;

            SQL = "SELECT ZID, ZDESCRIPCION ";
            SQL += " FROM ZFLUJOS ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];

            DrFlujoIni.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrFlujoIni.DataSource = dt;
            DrFlujoIni.DataBind();
            DrFlujoIni.SelectedIndex = -1;

            SQL = "SELECT ZID, ZDESCRIPCION ";
            SQL += " FROM ZCONFIGRUTASFILES ORDER BY ZDESCRIPCION ";
            dt = Main.BuscaLote(SQL).Tables[0];


            Drroot.DataSource = dt;
            Drroot.DataBind();
            Drroot.SelectedIndex = -1;

            DrPagina.Items.Clear();
            DrPagina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            SQL = " SELECT ZID, ZDESCRIPCION FROM  ZCONFIGRUTASFILES ORDER BY ZDESCRIPCION";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            DrPagina.DataValueField = "ZID";
            DrPagina.DataTextField = "ZDESCRIPCION";
            DrPagina.DataSource = dt1;
            DrPagina.DataBind();
        }
        protected void btnRetroflujos_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            PanelFlujos.Visible = true;
            PanelProcesos.Visible = false;
            divAsigEstado.Visible = true;
        }


        protected void btnProfiles_Click(object sender, EventArgs e)
        {
            if (DivPanelFlujo.Visible == false)
            {
                //Consulta ZPROFILES
                Label19.Text = "Flujo de Trabajo:";
                DivProfiles.Visible = false;
                DivPanelFlujo.Visible = true;
                DvProfile.Visible = false;
                DivSQL.Visible = false;
                EditaProfile(0);
            }
            else
            {

                //Relacion Archivo-campos----------------------------------------------------------------------------------------------
                string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
                SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();

                DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
                DataTable dtCampos = Main.CargaCampos().Tables[0];

                dtCampos = RelacionesArchivo(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos);
                //---------------------------------------------------------------------------------------------------------------------;
                DrcampoFiltro.DataValueField = "ZID";
                DrcampoFiltro.DataTextField = "ZTITULO";
                DrcampoFiltro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
                DrcampoFiltro.DataSource = dtCampos;
                DrcampoFiltro.DataBind();

                drCampofiltroseleccion.DataValueField = "ZID";
                drCampofiltroseleccion.DataTextField = "ZTITULO";
                drCampofiltroseleccion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
                drCampofiltroseleccion.DataSource = dtCampos;
                drCampofiltroseleccion.DataBind();

                SQL = " SELECT ZID, ZID_ARCHIVO, ZID_FLUJO, ZID_ESTADO, ZID_PAGINA, ZQUERY, ZID_PROCEDIMIENTO, ";
                SQL += " ZCAMPODOC, ZCAMPOFILTRO, ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS FROM  ZPROFILES ";
                SQL += " WHERE ZID_ARCHIVO = " + DrArchivos.SelectedValue;
                SQL += " AND ZID_FLUJO = " + DrFlujos.SelectedValue;
                //SQL += " AND ZID_ESTADO = " + DrEstado.SelectedValue;
                DataTable dt = Main.BuscaLote(SQL).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    TxtIdProfiles.Text = dr["ZID"].ToString();
                    DrArchivoProfile.SelectedValue = dr["ZID_ARCHIVO"].ToString();
                    DrFlujoProfile.SelectedValue = dr["ZID_FLUJO"].ToString();
                    DrEstadoProfile.SelectedValue = dr["ZID_ESTADO"].ToString();
                    DrPaginaProfile.SelectedValue = dr["ZID_PAGINA"].ToString();
                    //TxtQueryProfile.Text = dr["ZQUERY"].ToString().Replace("'", "\"");
                    //CommentSQL.Text = dr["ZQUERY"].ToString().Replace("'", "\"");
                    TxtQueryProfile.Text = dr["ZQUERY"].ToString().Replace("&#39;", "'");
                    TxtParam.Text = dr["ZFILTROCONDICION"].ToString().Replace("&#39;", "'");
                    CommentSQL.Text = dr["ZQUERY"].ToString().Replace("&#39;", "'");
                    DrProcedimientoProfile.SelectedValue = dr["ZID_PROCEDIMIENTO"].ToString();
                    DrcampoFiltro.SelectedItem.Text = dr["ZCAMPODOC"].ToString();
                    drCampofiltroseleccion.SelectedItem.Text = dr["ZCAMPOFILTRO"].ToString();
                    //DrcampoFiltro.Text = dr["ZCAMPODOC"].ToString();
                    //drCampofiltroseleccion.Text = dr["ZCAMPOFILTRO"].ToString();

                    string[] Fields = System.Text.RegularExpressions.Regex.Split(dr["ZFILTROCONDICION"].ToString(), ";");
                    ListBox3.Items.Clear();
                    ListBox4.Items.Clear();
                    Boolean Esta = false;

                    foreach (string Field in Fields)
                    {
                        foreach (DataRow MM in dtCampos.Rows)
                        {
                            if (Field == MM["ZTITULO"].ToString())
                            {
                                ListBox3.Items.Add(new ListasID(Field, Convert.ToInt32(MM["ZID"].ToString())).ToString());
                                ListBox4.Items.Add(new ListasID(MM["ZID"].ToString(), Convert.ToInt32(MM["ZID"].ToString())).ToString());
                                Esta = true;
                                break;
                            }
                        }
                    }
                    if (Esta == false)
                    {
                        int i = 0;
                        foreach (string Field in Fields)
                        {
                            ListBox3.Items.Add(new ListasID(Field,i).ToString());
                            ListBox4.Items.Add(new ListasID(Field, i).ToString());
                            i += 1;
                        }
                    }


                    TxtDocsProfile.Text = dr["ZDOCUMENTOS"].ToString();
                    TxtDirProfile.Text = dr["ZDIRECTORIOS"].ToString();
                    break;
                }
                Label19.Text = "Perfil del Flujo de Trabajo:";
                //DivCampoIzq.Visible = false;
                DivProfiles.Visible = true;
                DivPanelFlujo.Visible = false;
                DvProfile.Visible = true;
                DivSQL.Visible = false;
            }
        }

        protected void btnCondicionProfile_Click(object sender, EventArgs e)
        {
            if (DivSQL.Visible == true)
            {
                DvProfile.Visible = true;
                DivSQL.Visible = false;
            }
            else
            {
                DvProfile.Visible = false;
                DivSQL.Visible = true;
            }
        }
        protected void btnCondicionTemplate_Click(object sender, EventArgs e)
        {
            DivTxtQuery.Visible = false;
            DivTemplate.Visible = false;
            DivEstados.Visible = true;
        }

        protected void btnguardaTemplate_Click(object sender, EventArgs e)
        {

        }
        protected void btnCondicionSQL_Click(object sender, EventArgs e)
        {
            DivTemplate.Visible = false;
            DivTxtQuery.Visible = true;
        }
        protected void btnCondicionNOSQL_Click(object sender, EventArgs e)
        {
            DivTemplate.Visible = true;
            DivTxtQuery.Visible = false;
        }

        protected void btnCopiaTemplate_Click(object sender, EventArgs e)
        {
            DivGralTemplates.Visible = true;
            PanelProcesos.Visible = false;
            PanelFlujos.Visible = false;
        }
        
        protected void btnRetroProcesos_Click(object sender, EventArgs e)
        {
            DivGralTemplates.Visible = false;
            PanelProcesos.Visible = true;
            PanelFlujos.Visible = false;
        }

        protected void btnMuestraCondicion_Click(object sender, EventArgs e)
        {
            if(DivTemplate.Visible == true)
            {
                DivEstados.Visible = true;
                DivTxtQuery.Visible = false;
                DivTemplate.Visible = false;
            }
            else
            {
                DivEstados.Visible = false;
                DivTemplate.Visible = false;
                DivTxtQuery.Visible = false;
                if (this.Session["EstadoCondicion"].ToString() == "" || this.Session["EstadoCondicion"].ToString() == null)
                {
                    DivTxtQuery.Visible = true;
                }
                else
                {
                    DivTemplate.Visible = true;
                }
            }
        }

        private void LimpioPLantilla()
        {
            TxtNPlantilla.Text = "";
            TxtDPlantilla.Text = "";
            TxtRutaEPlantilla.Text = "";
            TxtRutaSPlantilla.Text = "";
            DrCopia.SelectedIndex = -1;
            DrfinPDF.SelectedIndex = -1;
            DrRutaAlternativa.SelectedIndex = -1;
            DrMarcadoresPlantillas.SelectedIndex = -1;
            ListPlantMarca.Items.Clear();
            ListPlantMarcaID.Items.Clear();
            DrPrinter.SelectedIndex = -1;
        }

        private void EditaPlantilla(int Abierto)
        {
            if (Abierto == 0)
            {
                TxtNPlantilla.Enabled = false;
                TxtDPlantilla.Enabled = false;
                TxtRutaEPlantilla.Enabled = false;
                TxtRutaSPlantilla.Enabled = false;
                TxtParametros.Enabled = false;
                DrCopia.Enabled = false;
                DrfinPDF.Enabled = false;
                DrRutaAlternativa.Enabled = false;
                DrMarcadoresPlantillas.Enabled = false;
                DrPrinter.Enabled = false;
                ListPlantMarca.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListPlantMarcaID.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtNPlantilla.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDPlantilla.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtRutaEPlantilla.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtRutaSPlantilla.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrCopia.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrfinPDF.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrRutaAlternativa.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrMarcadoresPlantillas.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrPrinter.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtParametros.BackColor = System.Drawing.Color.FromName("#ffffff");
                Button11.Enabled = false;
                Button14.Enabled = false;
                Button26.Enabled = false;

            }
            else
            {
                TxtNPlantilla.Enabled = true;
                TxtDPlantilla.Enabled = true;
                TxtRutaEPlantilla.Enabled = true;
                TxtRutaSPlantilla.Enabled = true;
                DrCopia.Enabled = true;
                DrfinPDF.Enabled = true;
                DrRutaAlternativa.Enabled = true;
                DrPrinter.Enabled = true;
                TxtParametros.Enabled = true;
                DrMarcadoresPlantillas.Enabled = true;
                ListPlantMarca.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListPlantMarcaID.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtNPlantilla.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDPlantilla.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtRutaEPlantilla.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtRutaSPlantilla.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrCopia.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrfinPDF.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrRutaAlternativa.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrMarcadoresPlantillas.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPrinter.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtParametros.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Button11.Enabled = true;
                Button14.Enabled = true;
                Button26.Enabled = true;
            }
            //EditaProceso(0);
            //EditaMarcador(0);
        }

        private void LimpioProceso()
        {
            TextIdProceso.Text = "";
            TxtDescProceso.Text = "";
            DrEstadoIni.SelectedIndex = -1;
            DrArchivoIni.SelectedIndex = -1;
            DrFlujoIni.SelectedIndex = -1;
            DrEjecucion.SelectedIndex = -1;
            DrProcesoPlantilla.SelectedIndex = -1;
            ListPlantillas.Items.Clear();
            ListPlantillasID.Items.Clear();
            TxtZQuery.Text = "";
            TxtParametros.Text = "";
        }
        private void Cancelado()
        {
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaPlantilla(0);
            EditaProceso(0);
            EditaMarcador(0);
            divTarea.Visible = false;
            divNePlantilla.Visible = false;

        }

        private void EditaProceso(int Abierto)
        {
            if (Abierto == 0)
            {
                TextIdProceso.Enabled = false;
                TxtDescProceso.Enabled = false;
                DrEstadoIni.Enabled = false;
                DrArchivoIni.Enabled = false;
                DrFlujoIni.Enabled = false;
                DrEjecucion.Enabled = false;
                TxtParametros.Enabled = false;
                DrProcesoPlantilla.Enabled = false;
                ListPlantillas.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListPlantillasID.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtZQuery.BackColor = System.Drawing.Color.FromName("#ffffff");
                TextIdProceso.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDescProceso.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrEstadoIni.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrArchivoIni.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrFlujoIni.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrEjecucion.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrProcesoPlantilla.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtParametros.BackColor = System.Drawing.Color.FromName("#ffffff");
                Button12.Enabled = false;
                Button13.Enabled = false;
            }
            else
            {
                TextIdProceso.Enabled = true;
                TxtDescProceso.Enabled = true;
                DrEstadoIni.Enabled = true;
                DrArchivoIni.Enabled = true;
                DrFlujoIni.Enabled = true;
                DrEjecucion.Enabled = true;
                TxtParametros.Enabled = true;
                DrProcesoPlantilla.Enabled = true;
                ListPlantillas.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListPlantillasID.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtZQuery.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TextIdProceso.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDescProceso.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrEstadoIni.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrArchivoIni.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrFlujoIni.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrEjecucion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrProcesoPlantilla.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtParametros.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Button12.Enabled = true;
                Button13.Enabled = true;
            }
            //EditaPlantilla(0);
            //EditaMarcador(0);
        }

        

        protected void TxtParametros_TextChanged(object sender, EventArgs e)
        {


        }
        protected void TxtParametros_Click(object sender, EventArgs e)
        {

            
        }

        protected void btnCopiaProceso_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //LimpioProceso();
            EditaProceso(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "proceso";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPROCESOS", dbParams));
            TextIdProceso.Text = (MiID + 1).ToString();
            TxtDescProceso.Text = "Copia de " + TxtDescProceso.Text;

        }
        
        protected void btnCreaProceso_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0") 
            {
                Cancelado();
                return; 
            }
            LimpioProceso();
            EditaProceso(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "proceso";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPROCESOS", dbParams));
            TextIdProceso.Text = (MiID + 1).ToString();

        }
        protected void btnEditProceso_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaProceso(1);
            this.Session["Edicion"] = "4";
            this.Session["proceso"] = "proceso";
        }
        protected void btnDeleteProceso_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4")
            {
                string Column = "DELETE FROM ZPROCESOS WHERE  ZID = " + TextIdProceso.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpioProceso();
                EditaProceso(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                btnMuestraProcesos_Click(sender, e);
            }
        }
        protected void btnAllProceso_Click(object sender, EventArgs e)
        {

        }

        protected void btnCopiaPlantilla_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //LimpioPLantilla();
            EditaPlantilla(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "plantilla";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPLANTILLAS", dbParams));
            TxtNPlantilla.Text = (MiID + 1).ToString();
            TxtDPlantilla.Text = "Copia de " + TxtDPlantilla.Text;
        }
        

        protected void btnCreaPlantilla_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            LimpioPLantilla();
            EditaPlantilla(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "plantilla";
            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPLANTILLAS", dbParams));
            TxtNPlantilla.Text = (MiID + 1).ToString();
        }
        protected void btnEditPlantilla_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaPlantilla(1);
            this.Session["Edicion"] = "4";
            this.Session["proceso"] = "plantilla";
        }
        protected void btnDeletePlantilla_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4")
            {
                string Column = "DELETE FROM ZPLANTILLAS WHERE  ZID = " + TxtNPlantilla.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpioPLantilla();
                EditaPlantilla(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                btnMuestraProcesos_Click(sender, e);
            }
        }
        protected void btnAllPlantilla_Click(object sender, EventArgs e)
        {

        }

        private void LimpioMarcador()
        {
            TxtDMarcador.Text = "";
            TxtIdMarcador.Text = "";
            TxtMarEntrada.Text = "";
            TxtMarSalida.Text = "";
            Txtpagina.Text = "";
            TxtejeX.Text = "";
            TxtejeY.Text = "";
            TxtRotacion.Text = "";
            DrTipoMarcador.SelectedIndex = -1;
            TextSello.Text = "";
            Drroot.SelectedIndex = -1;
        }


        private void EditaMarcador(int Abierto)
        {
            if (Abierto == 0)
            {
                TxtDMarcador.Enabled = false;
                TxtIdMarcador.Enabled = false;
                TxtMarEntrada.Enabled = false;
                TxtMarSalida.Enabled = false;
                Txtpagina.Enabled = false;
                TxtejeX.Enabled = false;
                TxtejeY.Enabled = false;
                TxtRotacion.Enabled = false;
                DrTipoMarcador.Enabled = false;
                TextSello.Enabled = false;
                Drroot.Enabled = false;
                TxtDMarcador.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtIdMarcador.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtMarEntrada.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtMarSalida.BackColor = System.Drawing.Color.FromName("#ffffff");
                Txtpagina.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtejeX.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtejeY.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtRotacion.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrTipoMarcador.BackColor = System.Drawing.Color.FromName("#ffffff");
                TextSello.BackColor = System.Drawing.Color.FromName("#ffffff");
                Drroot.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtMarEntradaDumie.BackColor = System.Drawing.Color.FromName("#ffffff");

                ListBox1Tar.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox1IDTar.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2Tar.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2IDTar.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDescTarea.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDummieE.BackColor = System.Drawing.Color.FromName("#ffffff");
                Button15.Enabled = false;
                Button16.Enabled = false;
                ImgDummie.Visible = false;
                ImgEjecDummie.Visible = false;
                ImgEditDummie.Visible = false;
                BtAsigTar.Enabled = false;
                BtDesAsigTat.Enabled = false;
                BtsubeTar.Enabled = false;
                BtBajaTar.Enabled = false;
                Button26.Enabled = false;
                

                if (DrTipoMarcador.SelectedValue == "5")
                {
                    Label41.Text = "Macro Dummie:";
                    Label42.Text = "Salida Fichero:";
                    Label43.Text = "Separador Fichero:";
                    TxtMarEntrada.Visible = false;
                    TxtMarEntradaDumie.Visible = true;
                    Label44.Visible = false;
                    TxtejeX.Visible = false;
                    Label45.Visible = false;
                    TxtejeY.Visible = false;
                    Label47.Visible = false;
                    TxtRotacion.Visible = false;
                    Label48.Visible = false;
                    TextSello.Visible = false;
                    Label49.Visible = false;
                    Drroot.Visible = false;
                    Button24.Visible = true;
                    Button25.Visible = false;
                    ImgDummie.Visible = true;
                    ImgEjecDummie.Visible = false;
                    ImgEditDummie.Visible = true;
                    //BtAsigTar.Enabled = true;
                    //BtDesAsigTat.Enabled = true;
                    //BtsubeTar.Enabled = true;
                    //BtBajaTar.Enabled = true;
                    //divAsigEstado.Visible = true;
                    //DrTipoMarcador.Width = Drroot.Width - 100;
                }
                else
                {
                    TxtMarEntrada.Visible = true;
                    TxtMarEntradaDumie.Visible = false;
                    Label41.Text = "Caracter Entrada:";
                    Label42.Text = "Caracter Salida:";
                    Label43.Text = "En Páginas:";
                    Label44.Visible = true;
                    TxtejeX.Visible = true;
                    Label45.Visible = true;
                    TxtejeY.Visible = true;
                    Label47.Visible = true;
                    TxtRotacion.Visible = true;
                    Label48.Visible = true;
                    TextSello.Visible = true;
                    Label49.Visible = true;
                    Drroot.Visible = true;
                    Button24.Visible = true;
                    Button25.Visible = true;
                }

            }
            else
            {
                TxtDMarcador.Enabled = true;
                TxtIdMarcador.Enabled = true;
                TxtMarEntrada.Enabled = true;
                TxtMarSalida.Enabled = true;
                Txtpagina.Enabled = true;
                TxtejeX.Enabled = true;
                TxtejeY.Enabled = true;
                TxtRotacion.Enabled = true;
                DrTipoMarcador.Enabled = true;
                TextSello.Enabled = true;
                Drroot.Enabled = true;
                TxtDMarcador.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtIdMarcador.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtMarEntrada.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtMarSalida.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Txtpagina.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtejeX.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtejeY.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtRotacion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrTipoMarcador.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TextSello.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Drroot.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtMarEntradaDumie.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox1Tar.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox1IDTar.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2Tar.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2IDTar.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDescTarea.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDummieE.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Button15.Enabled = true;
                Button16.Enabled = true;
                BtAsigTar.Enabled = false;
                BtDesAsigTat.Enabled = false;
                BtsubeTar.Enabled = false;
                BtBajaTar.Enabled = false;
                Button26.Enabled = false;
                if (DrTipoMarcador.SelectedValue == "5")
                {
                    Label41.Text = "Macro Dummie:";
                    Label42.Text = "Salida Fichero:";
                    Label43.Text = "Separador Fichero:";
                    TxtMarEntrada.Visible = false;
                    TxtMarEntradaDumie.Visible = true;
                    Label44.Visible = false;
                    TxtejeX.Visible = false;
                    Label45.Visible = false;
                    TxtejeY.Visible = false;
                    Label47.Visible = false;
                    TxtRotacion.Visible = false;
                    Label48.Visible = false;
                    TextSello.Visible = false;
                    Label49.Visible = false;
                    Drroot.Visible = false;
                    Button24.Visible = false;
                    Button25.Visible = true;
                    BtAsigTar.Enabled = true;
                    BtDesAsigTat.Enabled = true;
                    BtsubeTar.Enabled = true;
                    BtBajaTar.Enabled = true;
                    Button26.Enabled = true;
                }
                else
                {
                    TxtMarEntrada.Visible = true;
                    TxtMarEntradaDumie.Visible = false;
                    Label41.Text = "Caracter Entrada:";
                    Label42.Text = "Caracter Salida:";
                    Label43.Text = "En Páginas:";
                    Label44.Visible = true;
                    TxtejeX.Visible = true;
                    Label45.Visible = true;
                    TxtejeY.Visible = true;
                    Label47.Visible = true;
                    TxtRotacion.Visible = true;
                    Label48.Visible = true;
                    TextSello.Visible = true;
                    Label49.Visible = true;
                    Drroot.Visible = true;
                    Button24.Visible = false;
                    Button25.Visible = false;
                }
            }
            //EditaPlantilla(0);
            //EditaProceso(0);
        }

        protected void btnAbreDummie_Click(object sender, EventArgs e)
        {
            if(divTarea.Visible == true)
            {
                divTarea.Visible = false;
            }
            else
            {
                //Carga Tareas
                RelacionesTareas();
                divTarea.Visible = true;
            }

        }
        protected void GuardaTareas_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];
            string Column = "DELETE FROM ZDUMMIEHAND WHERE ZID = " + LbIDPlantDummie.Text;
            DBHelper.ExecuteNonQuerySQL(Column, dbParams);

            for (int i = 0; i < ListBox2IDTar.Items.Count; i++)
            {
                Column = "INSERT INTO ZDUMMIEHAND (ZIDDUMMIE, ZIDHAND, ZORDEN) ";
                string ColumnVal = " VALUES(" + LbIDPlantDummie.Text + "," + ListBox2IDTar.Items[i].Value + "," + (i + 1) + ")";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
            }

            Column = "UPDATE FROM ZMARCADORES SET ZENTRADA = '" + LbIDPlantDummie.Text + "'  WHERE ZID = " + TxtIdMarcador.Text;
            DBHelper.ExecuteNonQuerySQL(Column, dbParams);

            EditaPlantilla(0);
        }
        protected void btnCopiaMarcador_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //LimpioMarcador();
            EditaMarcador(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "marcador";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMARCADORES", dbParams));
            TxtIdMarcador.Text = (MiID + 1).ToString();
            TxtDMarcador.Text = "Copia de " + TxtDMarcador.Text;
        }
        

        protected void btnCreaMarcador_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            LimpioMarcador();
            EditaMarcador(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "marcador";
            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMARCADORES", dbParams));
            TxtIdMarcador.Text = (MiID + 1).ToString();
        }
        protected void btnEditMarcador_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaMarcador(1);
            this.Session["Edicion"] = "4";
            this.Session["proceso"] = "marcador";
        }

        protected void btnGuardarProceso_Click(object sender, EventArgs e)
        {
            //ImageButton5.ImageUrl = "~/Images/allwhite.png";

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "4")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea Proceso
                    string Column = "INSERT INTO ZPROCESOS (ZDESCRIPCION, ZPLANTILLAS, ZID_ESTADO, ZQUERY, ZID_ARCHIVO, ZID_FLUJO, ZEJECUCION, ZPARAMETROS) ";
                    string ColumnVal = " VALUES('" + TxtDescProceso.Text + "','";

                    for (int i = 0; i < ListPlantillasID.Items.Count; i++)
                    {
                        if (i == 0)
                        {
                            ColumnVal += ListPlantillasID.Items[i].Text ;
                        }
                        else
                        {
                            ColumnVal += "-" + ListPlantillasID.Items[i].Text ;
                        }
                    }
                    ColumnVal += "'," + DrEstadoIni.SelectedItem.Value + ",'" + TxtZQuery.Text.Replace("'", "&#39;") + "'," + DrArchivoIni.SelectedItem.Value + "," + DrFlujoIni.SelectedItem.Value + "," + DrEjecucion.SelectedItem.Value + ", '" + TxtParametros.Text + "')";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    LimpioProceso();
                    this.Session["Elproceso"] = "";
                }
                else
                {
                    //Edita flujo
                    string Column = "UPDATE ZPROCESOS SET ZDESCRIPCION = '" + TxtDescProceso.Text + "', ";

                    Column += " ZPLANTILLAS = '";

                    for (int i = 0; i < ListPlantillasID.Items.Count; i++)
                    {
                        if (i == 0)
                        {
                            Column += ListPlantillasID.Items[i].Text;
                        }
                        else
                        {
                            Column += "-" + ListPlantillasID.Items[i].Text;
                        }
                    }

                    Column += "', ZID_ESTADO =" + DrEstadoIni.SelectedItem.Value + ", ";
                    Column += " ZQUERY = '" + TxtZQuery.Text.Replace("'", "&#39;") + "', ";
                    Column += " ZID_ARCHIVO = " + DrArchivoIni.SelectedItem.Value + ", ";
                    Column += " ZID_FLUJO = " + DrFlujoIni.SelectedItem.Value + ", ";
                    Column += " ZEJECUCION = " + DrEjecucion.SelectedItem.Value + ", ";
                    Column += " ZPARAMETROS = '" + TxtParametros.Text + "' "; 
                    Column += " WHERE ZID =" + TextIdProceso.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }

            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaProceso(0);
            btnMuestraProcesos_Click(sender, e);
            CargaFoto();
        }
        protected void btnCancelarProceso_Click(object sender, EventArgs e)
        {
            Cancelado();
        }
        protected void btnGuardarPlantilla_Click(object sender, EventArgs e)
        {
            //ImageButton9.ImageUrl = "~/Images/allwhite.png";

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "4")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea Plantilla
                    string Column = "INSERT INTO ZPLANTILLAS (ZDESCRIPCION, ZRUTAENTRADA, ZRUTASALIDA, ZCOPYORIGINAL, ZSIGNPDF, ZRUTAALTERNATIVA, ZPRINTER) ";
                    string ColumnVal = " VALUES('" + TxtDPlantilla.Text + "','" + TxtRutaEPlantilla.Text + "','" + TxtRutaSPlantilla.Text + "',";
                    ColumnVal += DrCopia.SelectedItem.Value + "," + DrfinPDF.SelectedItem.Value + "," + DrRutaAlternativa.SelectedItem.Value + "," + DrPrinter.SelectedItem.Value + ")";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPLANTILLAS", dbParams));
                    TxtNPlantilla.Text = MiID.ToString();

                    Column = "DELETE FROM ZPLANTILLAMARCADOR WHERE  ZID_PLANTILLA = " + TxtNPlantilla.Text + " ";
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    for (int i = 0; i <= ListPlantMarcaID.Items.Count -1; i++)
                    {
                        Column = "INSERT INTO ZPLANTILLAMARCADOR (ZID_PLANTILLA, ZID_MARCADOR) ";
                        ColumnVal = " VALUES(" + TxtNPlantilla.Text + "," + ListPlantMarcaID.Items[i].Text + ")";
                        Column += ColumnVal;
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }
                    LimpioPLantilla();
                    this.Session["Laplantilla"] = "";
                }
                else
                {
                    //Edita Plantilla
                    string Column = "UPDATE ZPLANTILLAS SET ZDESCRIPCION = '" + TxtDPlantilla.Text + "', ";
                    Column += " ZRUTAENTRADA = '" + TxtRutaEPlantilla.Text + "',";
                    Column += " ZRUTASALIDA = '" + TxtRutaSPlantilla.Text + "',";
                    Column += " ZCOPYORIGINAL = " + DrCopia.SelectedItem.Value + ", ";
                    Column += " ZSIGNPDF = " + DrfinPDF.SelectedItem.Value + ", ";
                    Column += " ZPRINTER = " + DrPrinter.SelectedItem.Value + ", ";
                    Column += " ZRUTAALTERNATIVA = " + DrRutaAlternativa.SelectedItem.Value;
                    Column += " WHERE ZID =" + TxtNPlantilla.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }

            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaPlantilla(0);
            btnMuestraProcesos_Click(sender, e);
            CargaFoto();
        }
        protected void btnCancelarPlantilla_Click(object sender, EventArgs e)
        {
            Cancelado();
        }
        protected void btnGuardarMarcador_Click(object sender, EventArgs e)
        {
            //ImageButton13.ImageUrl = "~/Images/allwhite.png";

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "4")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea Marcador
                    string Column = "INSERT INTO ZMARCADORES (ZNOMBRE, ZCAMPO, ZENTRADA, ZSALIDA, ZPAGINA, ZX, ZY, ZROTACION, ZSELLO, ZROOT) ";
                    string ColumnVal = " VALUES('" + TxtDMarcador.Text + "'," + DrTipoMarcador.SelectedItem.Value + ",'" + TxtMarEntrada.Text + "',";
                    ColumnVal += "'" + TxtMarSalida.Text + "','" + Txtpagina.Text + "','" + TxtejeX.Text + "',";
                    ColumnVal += "'" + TxtejeY.Text + "','" + TxtRotacion.Text + "','" + TextSello.Text + "'," + Drroot.SelectedItem.Value + ")";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    LimpioMarcador();
                    this.Session["Elmarcador"] = "";
                }
                else
                {
                    //Edita Marcador
                    string Column = "UPDATE ZMARCADORES SET ZNOMBRE = '" + TxtDMarcador.Text + "', ";
                    Column += " ZCAMPO = " + DrTipoMarcador.SelectedItem.Value + ",";
                    Column += " ZENTRADA = '" + TxtMarEntrada.Text + "',";
                    Column += " ZSALIDA = '" + TxtMarSalida.Text  + "', ";
                    Column += " ZPAGINA = '" + Txtpagina.Text + "', ";
                    Column += " ZX = '" + TxtejeX.Text + "', ";
                    Column += " ZY = '" + TxtejeY.Text + "', ";
                    Column += " ZROTACION = '" + TxtRotacion.Text + "', ";
                    Column += " ZSELLO = '" + TextSello.Text + "', ";
                    Column += " ZROOT = " + Drroot.SelectedItem.Value + " ";
                    Column += " WHERE ZID =" + TxtIdMarcador.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }

            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaMarcador(0);
            btnMuestraProcesos_Click(sender, e);
            CargaFoto();
        }

        private void CargaFoto()
        {
            if (this.Session["Elmarcador"].ToString() != "")
            {
                DrMarcadores.SelectedIndex = Convert.ToInt32(this.Session["Elmarcador"].ToString());
            }
            if (this.Session["Laplantilla"].ToString() != "")
            {
                DrPlantillas.SelectedIndex = Convert.ToInt32(this.Session["Laplantilla"].ToString());
            }
            if (this.Session["Elproceso"].ToString() != "")
            {
                DrProcesos.SelectedIndex = Convert.ToInt32(this.Session["Elproceso"].ToString());
            }
        }
        protected void btnCancelarMarcador_Click(object sender, EventArgs e)
        {
            Cancelado();
        }


        protected void btnDeleteMarcador_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4")
            {
                string Column = "DELETE FROM ZMARCADORES WHERE  ZID = " + TxtIdMarcador.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpioMarcador();
                EditaMarcador(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                btnMuestraProcesos_Click(sender, e);
            }
        }
        protected void btnAllMarcador_Click(object sender, EventArgs e)
        {

        }
        protected void DrMarcadoresPlantillas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListPlantMarca.BackColor = System.Drawing.Color.FromName("#bdecb6");
            ListPlantMarca.Items.Add(new ListasID(DrMarcadoresPlantillas.SelectedItem.Text, Convert.ToInt32(DrMarcadoresPlantillas.SelectedItem.Value)).ToString());
            ListPlantMarcaID.Items.Add(new ListasID(DrMarcadoresPlantillas.SelectedItem.Value, Convert.ToInt32(DrMarcadoresPlantillas.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }
        protected void DrTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListPlantMarca.BackColor = System.Drawing.Color.FromName("#bdecb6");
            ListPlantMarca.Items.Add(new ListasID(DrMarcadoresPlantillas.SelectedItem.Text, Convert.ToInt32(DrMarcadoresPlantillas.SelectedItem.Value)).ToString());
            ListPlantMarcaID.Items.Add(new ListasID(DrMarcadoresPlantillas.SelectedItem.Value, Convert.ToInt32(DrMarcadoresPlantillas.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }
        

        protected void DrMarcadorPlanti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }
        protected void DrAsigUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int Index = DrAsigUser.SelectedIndex;
            //ListPlantMarcaID.SelectedIndex = DrAsigUser.SelectedIndex;
        }
            
        protected void ListPlantMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListPlantMarca.SelectedIndex;
            ListPlantMarcaID.SelectedIndex = ListPlantMarca.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;

            string a = ListPlantMarcaID.SelectedValue;
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
                //Posiciona en Marcadores
                for (int i = 0; i < DrMarcadores.Items.Count; i++)
                {
                    if (DrMarcadores.Items[i].Value == a)
                    {
                        DrMarcadores.SelectedIndex = i;
                        //this.Session["idarchivo"] = DrPlantillas.SelectedIndex.ToString();
                        break;
                    }
                }
                DrMarcadores_SelectedIndexChanged(null, null);
            }
            else
            {
                EditaPlantilla(1);
            }
        }

        protected void ListPlantillas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListPlantillas.SelectedIndex;
            ListPlantillasID.SelectedIndex = ListPlantillas.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;

            string a = ListPlantillasID.SelectedValue;

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
                //Posiciona en Plantillas
                
                for (int i = 0; i < DrPlantillas.Items.Count; i++)
                {
                    if (DrPlantillas.Items[i].Value == a)
                    {
                        DrPlantillas.SelectedIndex = i;
                        //this.Session["idarchivo"] = DrPlantillas.SelectedIndex.ToString();
                        break;
                    }
                }
                DrPlantillas_SelectedIndexChanged(null, null);
            }
            else
            {
                EditaProceso(1);
            }
        }

        

        protected void DrSello_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Drroot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         
        protected void DrEjecucion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }
        protected void DrUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrMarcadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = DrMarcadores.SelectedIndex;
            this.Session["Elmarcador"] = DrMarcadores.SelectedIndex;
            //MProcesos.Attributes["style"] = "background-color:#ffffff;";
            //MPlantilas.Attributes["style"] = "background-color:#bdecb6;";
            //MMarcadores.Attributes["style"] = "background-color:#ffffff;";
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                string SQL = "SELECT ZID, ZNOMBRE,  ZCAMPO, ZENTRADA, ZSALIDA, ZPAGINA, ZX, ZY, ZROTACION, ZSELLO, ZROOT ";
                SQL += " FROM ZMARCADORES WHERE ZID = " + DrMarcadores.SelectedValue;

                DataTable dt = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    TxtIdMarcador.Text = dr["ZID"].ToString();
                    TxtDMarcador.Text = dr["ZNOMBRE"].ToString();
                    TxtMarEntrada.Text = dr["ZENTRADA"].ToString();
                    TxtMarEntradaDumie.Text = dr["ZENTRADA"].ToString();
                    TxtMarSalida.Text = dr["ZSALIDA"].ToString();
                    Txtpagina.Text = dr["ZPAGINA"].ToString();
                    TxtejeX.Text = dr["ZX"].ToString();
                    TxtejeY.Text = dr["ZY"].ToString();
                    TxtRotacion.Text = dr["ZROTACION"].ToString();
                    TextSello.Text = dr["ZSELLO"].ToString();
                    Drroot.SelectedValue = dr["ZROOT"].ToString();
                    DrTipoMarcador.SelectedValue = dr["ZCAMPO"].ToString();
                    ImgDummie.Visible = false;
                    ImgEjecDummie.Visible = false;
                    //DrTipoMarcador.Style.Add("width", "100%");

                    if (dr["ZCAMPO"].ToString() == "5")
                    {
                        Label41.Text = "Macro Dummie:";
                        Label42.Text = "Salida Fichero:";
                        Label43.Text = "Separador Fichero:";
                        TxtMarEntrada.Visible = false;
                        TxtMarEntradaDumie.Visible = true;
                        Label44.Visible = false;
                        TxtejeX.Visible = false;
                        Label45.Visible = false;
                        TxtejeY.Visible = false;
                        Label47.Visible = false;
                        TxtRotacion.Visible = false;
                        Label48.Visible = false;
                        TextSello.Visible = false;
                        Label49.Visible = false;
                        Drroot.Visible = false;
                        Button24.Visible = true;
                        Button25.Visible = false;
                        ImgDummie.Visible = true;
                        ImgEjecDummie.Visible = true;
                        //DrTipoMarcador.Style.Add("width", "80%");
                    }
                    else if (dr["ZCAMPO"].ToString() == "6")
                    {
                        Label41.Text = "Ruta Entrada:";
                        Label42.Text = "Ruta Salida:";
                        Label43.Text = "Separador Fichero:";
                        TxtMarEntrada.Visible = false;
                        TxtMarEntradaDumie.Visible = true;
                        Label44.Visible = false;
                        TxtejeX.Visible = false;
                        Label45.Visible = false;
                        TxtejeY.Visible = false;
                        Label47.Visible = false;
                        TxtRotacion.Visible = false;
                        Label48.Visible = false;
                        TextSello.Visible = false;
                        Label49.Visible = false;
                        Drroot.Visible = false;
                        Button24.Visible = true;
                        Button25.Visible = false;
                    }
                    else if (dr["ZCAMPO"].ToString() == "7")
                    {
                        Label41.Text = "Ruta Entrada:";
                        Label42.Text = "Ruta Salida:";
                        Label43.Text = "Separador:";
                        TxtMarEntrada.Visible = false;
                        TxtMarEntradaDumie.Visible = true;
                        Label44.Visible = false;
                        TxtejeX.Visible = false;
                        Label45.Visible = false;
                        TxtejeY.Visible = false;
                        Label47.Visible = false;
                        TxtRotacion.Visible = false;
                        Label48.Visible = false;
                        TextSello.Visible = false;
                        Label49.Visible = false;
                        Drroot.Visible = false;
                        Button24.Visible = true;
                        Button25.Visible = false;
                    }
                    else
                    {
                        TxtMarEntrada.Visible = true;
                        TxtMarEntradaDumie.Visible = false;
                        Label41.Text = "Caracter Entrada:";
                        Label42.Text = "Caracter Salida:";
                        Label43.Text = "En Páginas:";
                        Label44.Visible = true;
                        TxtejeX.Visible = true;
                        Label45.Visible = true;
                        TxtejeY.Visible = true;
                        Label47.Visible = true;
                        TxtRotacion.Visible = true;
                        Label48.Visible = true;
                        TextSello.Visible = true;
                        Label49.Visible = true;
                        Drroot.Visible = true;
                        Button24.Visible = false;
                        Button25.Visible = false;
                    }

                    //ListPlantMarca.Text = dr["ZQUERY"].ToString();
                    break;
                }
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }

        protected void btnEjecutarMarcador_Click(object sender, EventArgs e)
        {
            string parametros = "";

            if (TxtIdMarcador.Text != "")
            {
                parametros = " ip/no port/no mar/" + TxtIdMarcador.Text;
            }
            else
            {
                parametros = " ip/0 port/0 mar/0";
            }


            string SQL = "INSERT INTO ZPROCESOS (ZEJECUCION , ZPARAMETROS, ZID_ESTADO)";
            SQL += " VALUES (8,'" + parametros + "', " + TxtIdMarcador.Text + ")";

            //Mensaje 
            Lbmensaje.Text = "Se ha lanzado el Proceso para la ejecución de un Dummie en el Servidor. ";
            cuestion.Visible = false;
            Asume.Visible = true;
            windowmessaje.Visible = true;
            MiCloseMenu();

            DBHelper.ExecuteNonQuery(SQL);

            //string Espera = Main.TrazaProceso(this.Session["ComputerName"].ToString(), null);

            ////Metodo sincrono. Hasta que Procesos Automaticos no termine, no se pasa el testigo
            //while (File.Exists(Espera))
            //{
            //}
        }
        protected void btnEditDummieMarcador_Click(object sender, EventArgs e)
        {
            string parametros = "";

            //if (TxtIdMarcador.Text != "")
            //{
            //    parametros = " /0 /0 /0 /  /  /" + TxtIdMarcador.Text;
            //}
            //else
            //{
            //    parametros = " /0 /0 /0";
            //}
            if (TxtIdMarcador.Text != "")
            {
                parametros = " ip/no port/no mar/" + TxtIdMarcador.Text;
            }
            else
            {
                parametros = " ip/0 port/0 mar/0";
            }


             string SQL = "INSERT INTO ZPROCESOS (ZEJECUCION , ZPARAMETROS, ZID_ESTADO)";
            SQL += " VALUES (6,'" + parametros + "', " + TxtIdMarcador.Text + ")";

            //Mensaje 
            Lbmensaje.Text = "Se ha lanzado el Proceso para la edición de un Dummie en el Servidor. ";
            cuestion.Visible = false;
            Asume.Visible = true;
            windowmessaje.Visible = true;
            MiCloseMenu();

            DBHelper.ExecuteNonQuery(SQL);
        }

        protected void DrPlantillas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = DrPlantillas.SelectedIndex;
            this.Session["Laplantilla"] = DrPlantillas.SelectedIndex;

            //MProcesos.Attributes["style"] = "background-color:#ffffff;";
            //MPlantilas.Attributes["style"] = "background-color:#bdecb6;";
            //MMarcadores.Attributes["style"] = "background-color:#ffffff;";
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                string SQL = "SELECT ZID, ZDESCRIPCION, ZRUTAENTRADA, ZRUTASALIDA, ZCOPYORIGINAL, ZSIGNPDF, ZRUTAALTERNATIVA, ZPRINTER ";
                SQL += " FROM ZPLANTILLAS WHERE ZID = " + DrPlantillas.SelectedValue ;

                DataTable dt = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    TxtNPlantilla.Text = dr["ZID"].ToString();
                    TxtDPlantilla.Text = dr["ZDESCRIPCION"].ToString();
                    TxtRutaEPlantilla.Text = dr["ZRUTAENTRADA"].ToString();
                    TxtRutaSPlantilla.Text = dr["ZRUTASALIDA"].ToString();
                    DrCopia.SelectedValue = dr["ZCOPYORIGINAL"].ToString();
                    DrPrinter.SelectedValue = dr["ZPRINTER"].ToString();
                    DrfinPDF.SelectedValue = dr["ZSIGNPDF"].ToString();
                    DrRutaAlternativa.SelectedValue = dr["ZRUTAALTERNATIVA"].ToString();

                    SQL = " SELECT A.ZID, A.ZNOMBRE AS ZDESCRIPCION ";
                    SQL += " FROM ZMARCADORES A, ZPLANTILLAMARCADOR B, ZPLANTILLAS C ";
                    SQL += " WHERE C.ZID = " + dr["ZID"].ToString();
                    SQL += " AND B.ZID_PLANTILLA = C.ZID ";
                    SQL += " AND B.ZID_MARCADOR = A.ZID ";
                    SQL += " ORDER BY A.ZNOMBRE ";

                    DataTable dtA = Main.BuscaLote(SQL).Tables[0];

                    ListPlantMarca.Items.Clear();
                    ListPlantMarcaID.Items.Clear();

                    foreach (DataRow drA in dtA.Rows)
                    {
                        ListPlantMarca.Items.Add(drA["ZDESCRIPCION"].ToString());
                        ListPlantMarcaID.Items.Add(drA["ZID"].ToString());
                    }
                    break;
                }
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }

        private void BuscaCampos(string Campo, string Formato, string Tipo, DataTable Todo, string Archivo)
        {
            SqlParameter[] dbParams = new SqlParameter[0];
            string ID = "";
            Boolean Esta = false;
            DataTable dt = null;

            string SQL = "";
            int MiID  = 0;

            //Id Formato
            SQL = "SELECT ZID, ZFORMATO FROM ZTIPOCAMPO ORDER BY ZID";
            dt = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dt.Rows)
            {
                if (fila["ZFORMATO"].ToString() == Formato.ToUpper())
                {
                    ID = fila["ZID"].ToString();
                    if(Formato == "INT"){Formato = "8";}
                    if (Formato == "VARCHAR") { Formato = "255"; }
                    if (Formato == "DATETIME") { Formato = "8"; }
                    if (Formato == "DECIMAL") { Formato = "5"; }
                    if (Formato == "NUMERIC") { Formato = "4"; }
                    break;
                }
            }


            foreach (DataRow filas in Todo.Rows)
            {
                if (filas["ZTITULO"].ToString() == Campo)
                {
                    //Cojo el ID y lo paso a relacion ArchivoCampo
                    Esta = true;
                    SQL = "INSERT INTO ZARCHIVOCAMPOS (ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) ";
                    SQL += " VALUES (" + Archivo + ", " + filas["ZID"].ToString() + ", 0, 0)";
                    break;
                }
            }
            if(Esta == false)
            {
                //Creo el Campo
                SQL = "INSERT INTO ZCAMPOS (ZTITULO, ZDESCRIPCION, ZNIVEL, ZESTADO, ZFECHA, ZTIPO, ZVALOR, ZVALORDEFECTO, ZVALIDACION, ZACTIVO) ";
                SQL += " VALUES ('" + Campo + "', '" + Campo + "', 9, 1, '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "'," + ID + ", " + Formato +  ", 0, 0, 1)";

                Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                if (Con == null)
                {
                }
                else
                {
                    MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams)) + 1;
                    SQL = "INSERT INTO ZARCHIVOCAMPOS (ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) ";
                    SQL += " VALUES (" + Archivo + ", " + Con + ", 0, 0)";
                }
            }
        }
        protected void btnEjecProceso_Click(object sender, EventArgs e)
        {
            Boolean Esta = false;
            int MiID = 0;
            string rANUAL = "";
            string rBBDDa = ""; // "NET_PRVR" + rANUAL;
            string rBBDDb = ""; // "NET_PRVV" + rANUAL;
            string[] Fields = System.Text.RegularExpressions.Regex.Split(CommentSQL.Text, "RUN>#");
            string[] Datos = System.Text.RegularExpressions.Regex.Split(CommentSQL.Text, "RUN>#");
            string CReate = "";
            string Formato = "";
            string SQL = "";
            Object Con = null;
            string M = "";
            string Consulta = CommentSQL.Text ;
            DataTable dt = null;

            SqlParameter[] dbParams = new SqlParameter[0];

            //si tiene parametros
            if (TxtParam.Text != "")
            {
                Fields = System.Text.RegularExpressions.Regex.Split(TxtParam.Text, ";");
                if (Fields.Count() > 0)
                {
                    if (Fields[0] != "")
                    {
                        for (int i = 0; i < Fields.Count(); i++)
                        {
                            string[] Trozos = System.Text.RegularExpressions.Regex.Split(Fields[i], ":");
                            if (Trozos.Count() > 0)
                            {
                                for (int a = 0; a < Trozos.Count(); a++)
                                {
                                    if (Trozos[a].ToUpper().Trim() == "TABLA")
                                    {
                                        Tabla = Trozos[a + 1];
                                    }
                                    if (Trozos[a].ToUpper().Trim() == "TABLAOBJ")
                                    {
                                        TablaObj = Trozos[a + 1];
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //si tiene Tabla objetos
            if (TablaObj != "")
            {//Creamos la tabla de objetos
                //Comprobamos si existe 
                //SQL = " SELECT ZBASEDATOS FROM ZCONEXIONES WHERE ZID = " + ConfigurationManager.AppSettings.Get("DefaultIDDataBase");
                SQL = "SELECT count(*) as Exist from INFORMATION_SCHEMA.TABLES where table_name = '" + TablaObj + "'";
                Con = DBHelper.ExecuteScalarSQL(SQL, null);

                if (Con.ToString() == "0")
                {
                    SQL = " CREATE TABLE " + TablaObj + " ( ";
                    SQL += " ZID int IDENTITY(1, 1) NOT NULL, ";
                    SQL += " ZID_DOMAIN int NULL, ";
                    SQL += " ZID_ARCHIVO int NULL, ";
                    SQL += " ZDESCRIPCION varchar(255) NULL, ";
                    SQL += " ZTITULO varchar(255) NULL, ";
                    SQL += " ZDIRECTORIO varchar(255) NULL, ";
                    SQL += " ZRUTA varchar(255) NULL, ";
                    SQL += " ZPESO varchar(255) NULL, ";
                    SQL += " ZROOT int NULL, ";
                    SQL += " ZKEY int NULL, ";
                    SQL += " ZESTADO int NULL, ";
                    SQL += " ZFECHA datetime NULL, ";
                    SQL += " ZCATEGORIA varchar(255) NULL, ";
                    SQL += " ZSUBCATEGORIA varchar(255) NULL, ";
                    SQL += " ZUSER varchar(255) NULL, ";
                    SQL += " ZNIVEL int NULL, ";
                    SQL += " ZID_VOLUMEN int NULL, ";
                    SQL += " ZID_REGISTRO int NULL, ";
                    SQL += " ZFIRMA int NULL, ";
                    SQL += " ZLLAVE varchar(MAX) NULL) ";

                    M = Main.ETrazas(SQL, "1", "  Crea tabla objetos " + TablaObj);
                    DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                }
            }

            //busca si existe Base de Datos en la Consulta
            if (Consulta.Contains("RBBDDa#") == true || Consulta.Contains("RBBDDb#") == true)
            {
                string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA WHERE ACTIVO = 1";
                DataTable dtV = Main.BuscaLote(Data).Tables[0];
                foreach (DataRow fila3 in dtV.Rows)
                {
                    rANUAL = fila3["ZANO"].ToString();
                    rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
                    rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
                    //Consulta.Replace("RBBDDa#", rBBDDa);
                    //Consulta.Replace("RBBDDb#", rBBDDb);
                    break;
                }
                Fields = System.Text.RegularExpressions.Regex.Split(Consulta, Environment.NewLine);
                string a = ""; 
                for (int i = 0; i < Fields.Count(); i++)
                {
                    if (Fields[i].Contains("DESDE#") == true)
                    {
                        a = Fields[i].Replace("DESDE#", System.DateTime.Now.ToString("dd/MM/yyyy"));
                        SQL += a.Replace("HASTA#", System.DateTime.Now.ToString("dd/MM/yyyy")) + Environment.NewLine;
                    }
                    else
                    {
                        a = Fields[i].Replace("RBBDDa#", rBBDDa);
                        SQL += a.Replace("RBBDDb#", rBBDDb) + Environment.NewLine;
                    }
                }
                Consulta = SQL;
            }

            //Si la consulta tiene RUN
            if (Consulta.ToUpper().Contains("RUN>#") == true)
            {
                if (Consulta.ToUpper().Contains("RUN<#") == true)
                {
                    Fields = System.Text.RegularExpressions.Regex.Split(Consulta, "RUN>#");
                    Datos = System.Text.RegularExpressions.Regex.Split(Fields[1], "RUN<#");
                    Datos = System.Text.RegularExpressions.Regex.Split(Datos[0], "#GO#");
                    //Lanza todo si no existe
                    try
                    {
                        for (int i = 0; i < Datos.Count(); i++)
                        {
                            try
                            {
                                if (Datos[i].ToUpper().Contains("CREATE") == true && Esta == false)
                                {
                                    Esta = true;
                                    CReate = Datos[i];
                                    //M = Main.ETrazas("", "1", " Ejecuta consulta CREATE RUN :" + CReate);
                                }
                                SQL = "SELECT count(*) as Exist from INFORMATION_SCHEMA.TABLES where table_name = '" + Tabla + "'";
                                Con = DBHelper.ExecuteScalarSQL(SQL, null);

                                if (Con.ToString() == "0")
                                {
                                    DBHelper.ExecuteNonQuerySQL(Datos[i], dbParams);
                                }
                            }
                            catch (Exception ex)
                            {
                                M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error lanzando RUN>#:" + Datos[0] + ", Error =" + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error RUN<# :" + Datos[0] + ", Error =" + ex.Message);
                    }
                }
                else
                {
                    Lbmensaje.Text = "La Consulta no contiene el termino para cerrar RUN<# ";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    //M = Main.ETrazas(Variables.Error, "1", " La Consulta no contiene el termino para cerrar RUN<# ");
                    return;
                }
                Esta = true;
            }

            //Si la consulta tiene PROCESO
            if (Consulta.ToUpper().Contains("PROCESO>#") == true)
            {
                if (Consulta.ToUpper().Contains("PROCESO<#") == true)
                {
                    Fields = System.Text.RegularExpressions.Regex.Split(Consulta, "PROCESO>#");
                    Datos = System.Text.RegularExpressions.Regex.Split(Fields[1], "PROCESO<#");
                    //Lanza todo si no existe
                    try
                    {
                        try
                        {
                            //M = Main.ETrazas("", "1", " Ejecuta consulta PROCESO :" + Datos[0] );
                            Consulta = Datos[0];
                            dt = Main.BuscaLoteGold(Consulta).Tables[0];
                            //DBHelper.ExecuteNonQueryGolden(Datos[0], dbParams);
                        }
                        catch (Exception ex)
                        {
                            M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error lanzando consulta PROCESO>#:" + Datos[0] + ", Error =" + ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error lanzando consulta :" + Datos[0] + ", Error =" + ex.Message);
                    }
                }
                else
                {
                    Lbmensaje.Text = "La Consulta no contiene el termino para cerrar PROCESO<# ";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    //M = Main.ETrazas(Variables.Error, "1", " La Consulta no contiene el termino para cerrar PROCESO<# ");
                    return;
                }
                Esta = true;
            }
            else
            {//Si no tiene PROCESO es consulta general
                try
                {
                    M = Main.ETrazas("", "1", " Ejecuta consulta 80 :" + Consulta);
                    dt = Main.BuscaLoteGold(Consulta).Tables[0];
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "La Consulta muestra error: " + ex.Message + ", Consulta= " + Consulta;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    M = Main.ETrazas(Variables.Error, "1", " La Consulta no contiene el termino para cerrar PROCESO<# ");
                    return;
                }
            }
            //El resto Consulta normal ya que dt es correcta
            if(Esta == true)
            {
                //Busco los campos
            }
            //Campos = "CONTADOR, TIPO, SERIE, NUMERO, ZFECHA, ZCLICODIGO, ZCLIDOMICILIO, ZCLIPAIS, CLIPAIS, IMPORTE, INCOTERM, FORMAPAGO, ZULTMOD, ZREGFECHA, ZCODUSU, ZDOC, ZCLASOPE, ZREGFECHIMPR, ZCONTACTO, CONTADORLINEA, PRODUCTOLINEA, DESCRIPCIONLINEA, UNIDADES, PVPLINEA, MEDIDA, ZEMPRESA, ZFECHACONTRATO";
            
            //M = Main.ETrazas(CReate, "1", "Seccion concluida. Esta es: " + Esta + " . Data.Colum:" + dt.Columns.Count + " . Data.Row: " + dt.Rows.Count);
            Fields = System.Text.RegularExpressions.Regex.Split(CReate, Environment.NewLine);
            //Cargo las Columnas y el formato
            foreach (DataColumn column in dt.Columns)
            {
                if (Campos == "")
                {
                    Campos = column.ColumnName.ToUpper().Trim();
                }
                else
                {
                    Campos += ", " + column.ColumnName.ToUpper().Trim();
                }
            }
            M = Main.ETrazas("", "1", "Campos: " + Campos);
            Fields = System.Text.RegularExpressions.Regex.Split(CReate, Environment.NewLine);
            string[] Columnas = System.Text.RegularExpressions.Regex.Split(Campos, ",");
            //M = Main.ETrazas("Las columnas son:" + Columnas.Count() + ". ", "1", "Create es: " + CReate);
            SQL = "";
            Formato = "";
            string Campillo = "";
            string Formatillo = "";
            string Completillo = "";

            for (int i = 0; i < Fields.Count(); i++)
            {
                Fields[i] = Fields[i].Replace("["," ");
                Fields[i] = Fields[i].Replace("]", " ");
                if (Fields[i].ToUpper().Contains("WHERE") == true) { break; }
                if (Fields[i].ToUpper().Contains("CREATE") == true || Fields[i].Trim() == "")
                {
                }
                else
                {


                    for (int a = 0; a < Columnas.Count() ; a++)
                    {
                        //M = Main.ETrazas("La fila a comparar es:" + Fields[i].ToUpper() + " con " + Columnas[a], "1", "" );
                        if (Columnas[a].Trim() != "ZID")
                        {
                            if (Fields[i].ToUpper().Contains(Columnas[a].Trim()) == true)
                            {
                                if (Fields[i].ToUpper().Contains(" INT ") == true)
                                {
                                    if (SQL == "")
                                    {
                                        SQL = "CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL, " + Columnas[a] + " INT NULL  ";
                                    }
                                    else
                                    {
                                        SQL += ", " + Columnas[a] + " INT NULL  ";
                                    }
                                    Formato += "INT" + Environment.NewLine; ;
                                    //M = Main.ETrazas("La fila encontrada es:------->" + Fields[i].ToUpper() + " con " + Columnas[a], "1", "");
                                    Campillo += Columnas[a].Trim() + Environment.NewLine;
                                    Formatillo += "INT" + Environment.NewLine;
                                    Completillo += Columnas[a].Trim() + " = INT" + Environment.NewLine;
                                    break;
                                }
                                else if (Fields[i].ToUpper().Contains("DECIMAL") == true)
                                {
                                    if (SQL == "")
                                    {
                                        SQL = "CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL, " + Columnas[a] + "  DECIMAL(8.2) NULL  ";
                                    }
                                    else
                                    {
                                        SQL += ", " + Columnas[a] + "  DECIMAL(8.2) NULL  ";
                                    }
                                    Formato += "DECIMAL" + Environment.NewLine; ;
                                    //M = Main.ETrazas("La fila encontrada es:------->" + Fields[i].ToUpper() + " con " + Columnas[a], "1", "");
                                    Campillo += Columnas[a].Trim() + Environment.NewLine;
                                    Formatillo += "DECIMAL" + Environment.NewLine;
                                    Completillo += Columnas[a].Trim() + " = DECIMAL" + Environment.NewLine;
                                    break;
                                }
                                else if (Fields[i].ToUpper().Contains("DATETIME") == true)
                                {
                                    if (SQL == "")
                                    {
                                        SQL = "CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL, " + Columnas[a] + "   DATETIME NULL  ";
                                    }
                                    else
                                    {
                                        SQL += ", " + Columnas[a] + "   DATETIME NULL  ";
                                    }
                                    Formato += "DATETIME" + Environment.NewLine; ;
                                    //M = Main.ETrazas("La fila encontrada es:------->" + Fields[i].ToUpper() + " con " + Columnas[a], "1", "");
                                    Campillo += Columnas[a].Trim() + Environment.NewLine;
                                    Formatillo += "DATETIME" + Environment.NewLine;
                                    Completillo += Columnas[a].Trim() + " = DATETIME " + Environment.NewLine;
                                    break;
                                }
                                else //Varchar
                                {
                                    if (SQL == "")
                                    {
                                        SQL = "CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL, " + Columnas[a] + "   VARCHAR(255) NULL  ";
                                    }
                                    else
                                    {
                                        SQL += ", " + Columnas[a] + "   VARCHAR(255) NULL  ";
                                    }
                                    Formato += "VARCHAR" + Environment.NewLine; ;
                                    Campillo += Columnas[a].Trim() + Environment.NewLine;
                                    Formatillo += "VARCHAR" + Environment.NewLine;
                                    Completillo += Columnas[a].Trim() + "  = VARCHAR " + Environment.NewLine;
                                    break;
                                    //M = Main.ETrazas("La fila encontrada es:------->" + Fields[i].ToUpper() + " con " + Columnas[a], "1", "La consulta creada es: " + SQL);
                                }
                            }
                        }
                    }
                }
            }
            //Campos para FLujos
            SQL += ", ZID_FLUJO INT NULL  ";
            SQL += ", ZID_ESTADO IN NULL  ";
            SQL += ")  ";

            //M = Main.ETrazas(SQL, "1", " Consulta crea tabla : " + Tabla);
            //DBHelper.ExecuteNonQuerySQL(SQL, dbParams);

            if (dt != null)
            {
                M = Main.ETrazas("OJO: Tenemos recuperados " + dt.Rows.Count + " Registros. Los Campos son " + Completillo, "1", " El formato es: " + Formato + " y la Consulta creada es " + SQL);
            }
            else
            {
                M = Main.ETrazas("OJO: No hay datos. Los Campos son " + Completillo, "1" + ". Los registros son:" + dt.Rows, " El formato es: " + Formato + " y la Consulta creada es " + SQL);
            }
            string[] Formatos = System.Text.RegularExpressions.Regex.Split(Formatillo, Environment.NewLine);
            Columnas = System.Text.RegularExpressions.Regex.Split(Campillo, Environment.NewLine);
            string Valor = "";

            try
            {
                foreach (DataRow fila in dt.Rows)
                {
                    SQL = "INSERT INTO " + Tabla + " (" + Campos + ") VALUES ( ";
                    Valor = "";
                    for (int i = 0; i < Columnas.Count(); i++)
                    {
                        if (Columnas[i] != "")
                        {
                            if (Formatos[i].Trim() == "VARCHAR" || Formatos[i].Trim() == "DATETIME")
                            {
                                if (Valor == "")
                                {
                                    Valor += " '" + fila[Columnas[i].Trim()].ToString() + "' ";
                                }
                                else
                                {
                                    Valor += ",'" + fila[Columnas[i].Trim()].ToString() + "'";
                                }
                            }
                            else
                            {
                                if (Valor == "")
                                {
                                    Valor += fila[Columnas[i].Trim()].ToString().Replace(",", ".") + " ";
                                }
                                else
                                {
                                    Valor += "," + fila[Columnas[i].Trim()].ToString().Replace(",", ".") + " ";
                                }
                            }
                        }
                    }
                    SQL += Valor + ")";
                    M = Main.ETrazas("", "1", " Se monta la query INSERT INTO:" + SQL);
                    DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                }

                //Si llega hasta aqui cargo Relaciones
                //Inserta en Archivos
                SQL = "INSERT INTO ZARCHIVOS (ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZDUPLICADOS, ZKEY, ZVIEW, ZCONEXION) ";
                SQL += " VALUES ('" + Tabla + "', 9, 0, '" + Tabla + "','" + TablaObj + "', 1, 2, 1, 0, 0," + ConfigurationManager.AppSettings.Get("DefaultIDDataBase") + ")";

                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZARCHIVOS", dbParams));
                //Consulta Campos
                SQL = "SELECT * FROM ZCAMPOS ORDER BY ZID ";
                DataTable dtCampo = Main.BuscaLote(SQL).Tables[0];
                for (int i = 0; i < Columnas.Count(); i++)
                {
                    if (Columnas[i] != "")
                    {
                        BuscaCampos(Columnas[i], Formatos[i].Trim(), Formatos[i].Trim(), dtCampo, MiID.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                M = Main.ETrazas("Los Formatos son" + Formatos.Count() + ". Las columnas son: " + Columnas.Count(), "1", "Error en Inserción tabla. " + ex.Message);
            }


            //Si la consulta tiene POSTRUN
            if (CommentSQL.Text.ToUpper().Contains("POSTRUN>#") == true)
            {
                if (CommentSQL.Text.ToUpper().Contains("POSTRUN<#") == true)
                {
                    Fields = System.Text.RegularExpressions.Regex.Split(CommentSQL.Text, "POSTRUN>#");
                    Datos = System.Text.RegularExpressions.Regex.Split(Fields[1], "POSTRUN<#");
                    Datos = System.Text.RegularExpressions.Regex.Split(Datos[0], "#GO#");
                    //Lanza todo si no existeImgatras_Click
                    try
                    {
                        for (int i = 0; i < Datos.Count(); i++)
                        {
                            try
                            {
                                DBHelper.ExecuteNonQuerySQL(Datos[i], dbParams);

                                //if (Datos[i].ToUpper().Contains("CREATE") == true && Esta == false)
                                //{
                                //    Esta = true;
                                //    CReate = Datos[i];
                                //    //M = Main.ETrazas("", "1", " Ejecuta consulta CREATE PSTRUN :" + CReate);
                                //}
                                //SQL = "SELECT count(*) as Exist from INFORMATION_SCHEMA.TABLES where table_name = '" + Tabla + "'";
                                //Con = DBHelper.ExecuteScalarSQL(SQL, null);

                                //if (Con.ToString() == "0")
                                //{
                                //    DBHelper.ExecuteNonQuerySQL(Datos[i], dbParams);
                                //}
                            }
                            catch (Exception ex)
                            {
                                M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error lanzando PSTRUN>#:" + Datos[0] + ", Error =" + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error PSTRUN<# :" + Datos[0] + ", Error =" + ex.Message);
                    }
                }
                else
                {
                    Lbmensaje.Text = "La Consulta no contiene el termino para cerrar RUN<# ";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                    //M = Main.ETrazas(Variables.Error, "1", " La Consulta no contiene el termino para cerrar RUN<# ");
                    return;
                }
                Esta = true;
            }


            //M = Main.ETrazas("", "1", "Importacion Concluida. " );

            //if (TxtParametros.Text != "")
            //{
            //    Fields = System.Text.RegularExpressions.Regex.Split(TxtParametros.Text, ";");
            //    if (Fields.Count() > 0)
            //    {
            //        if (Fields[0] != "")
            //        {
            //            for (int i = 0; i < Fields.Count(); i++)
            //            {
            //                string[] Trozos = System.Text.RegularExpressions.Regex.Split(Fields[i], ":");
            //                if (Trozos.Count() > 0)
            //                {
            //                    for (int a = 0; a < Trozos.Count(); a++)
            //                    {
            //                        if (Trozos[a].ToUpper().Trim() == "TABLA")
            //                        {
            //                            Tabla = Trozos[a + 1];
            //                        }
            //                        if (Trozos[a].ToUpper().Trim() == "TABLAOBJ")
            //                        {
            //                            TablaObj = Trozos[a + 1];
            //                        }
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    //¿Existe la tabla?
            //    if(Tabla != "")
            //    {
            //        try
            //        {
            //            Object Con = null;
            //            string SQL = "";
            //            string M = "";

            //            try
            //            {
            //                SQL = "SELECT TOP 1 * FROM " + Tabla;
            //                Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //            }
            //            catch(Exception ex)
            //            {
            //                M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click --> Error:" + ex.Message);
            //            }

            //            if (Con == null)
            //            {
            //                //Lanza la consulta al origen
            //                Campos = "CONTADOR, TIPO, SERIE, NUMERO, ZFECHA, ZCLICODIGO, ZCLIDOMICILIO, ZCLIPAIS, CLIPAIS, IMPORTE, INCOTERM, FORMAPAGO, ZULTMOD, ZREGFECHA, ZCODUSU, ZDOC, ZCLASOPE, ZREGFECHIMPR, ZCONTACTO, CONTADORLINEA, TIPOLINEA, SERIELINEA, NUMEROLINEA, PRODUCTOLINEA, DESCRIPCIONLINEA, UNIDADES, PVPLINEA, MEDIDA, ZEMPRESA, ZFECHACONTRATO";

            //                DataTable dt = Main.BuscaLoteGold(TxtZQuery.Text).Tables[0];
            //                Fields = System.Text.RegularExpressions.Regex.Split(TxtZQuery.Text, Environment.NewLine);

            //                if (Fields[0] != "")
            //                {
            //                    for (int i = 0; i < Fields.Count(); i++)
            //                    {
            //                        foreach (DataColumn column in dt.Columns)
            //                        {
            //                            if (Campos == "")
            //                            {
            //                                Campos = column.ColumnName.ToUpper().Trim();
            //                            }
            //                            else
            //                            {
            //                                Campos += ", " + column.ColumnName.ToUpper().Trim();
            //                            }
            //                        }
            //                        break;
            //                    }
            //                    M = Main.ETrazas(Campos, "1", " Creacion de campos" );

            //                    //busca Base de Datos en la Consulta
            //                    if(TxtZQuery.Text.Contains("RBBDDa#") == true || TxtZQuery.Text.Contains("RBBDDb#") == true)
            //                    {
            //                        string Data = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA WHERE ACTIVO = 1";
            //                        DataTable dtV = Main.BuscaLote(Data).Tables[0];
            //                        foreach (DataRow fila3 in dtV.Rows)
            //                        {
            //                            rANUAL = fila3["ZANO"].ToString();
            //                            rBBDDa = fila3["DBVRE"].ToString() + rANUAL;
            //                            rBBDDb = fila3["DBVIVA"].ToString() + rANUAL;
            //                            TxtZQuery.Text.Replace("RBBDDa#", rBBDDa);
            //                            TxtZQuery.Text.Replace("RBBDDb#", rBBDDb);
            //                            break;
            //                        }
            //                    }

            //                    //Cargo los Campos
            //                    //SQL = "SELECT * FROM ZCAMPOS ORDER BY ZID ";
            //                    //DataTable dtCampo = Main.BuscaLote(SQL).Tables[0];




            //                        //Crea la Tabla Archivos
            //                    SQL = " CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL ";
            //                    string[] Columnas = System.Text.RegularExpressions.Regex.Split(Campos, ",");
            //                    string Formato = "";


            //                    for (int i = 0; i < Fields.Count(); i++)
            //                    {
            //                        if (Fields[i].ToUpper().Contains("WHERE") == true) { break; }
            //                        for (int a = 0; a < Columnas.Count() - 1; a++)
            //                        {
            //                            if (Columnas.Count() > 0)
            //                            {
            //                                if (Fields[i].ToUpper().Contains(Columnas[a]) == true)
            //                                {
            //                                    if (Fields[i].ToUpper().Contains("INT") == true)
            //                                    {
            //                                        SQL += ", " + Columnas[a] + " INT NULL  ";
            //                                        Formato += "INT, ";
            //                                    }
            //                                    else if (Fields[i].ToUpper().Contains("DECIMAL") == true)
            //                                    {
            //                                        SQL += ", " + Columnas[a] + " DECIMAL(8.2) NULL  ";
            //                                        Formato += "DECIMAL, ";
            //                                    }
            //                                    else if (Fields[i].ToUpper().Contains("DATETIME") == true)
            //                                    {
            //                                        SQL += ", " + Columnas[a] + " DATETIME NULL  ";
            //                                        Formato += "DATETIME, ";
            //                                    }
            //                                    else //Varchar
            //                                    {
            //                                        SQL += ", " + Columnas[a] + "VARCHAR(255) NULL  ";
            //                                        Formato += "VARCHAR, ";
            //                                    }
            //                                    break;
            //                                }
            //                            }
            //                            else
            //                            {
            //                                break;
            //                            }
            //                        }
            //                        //Campos para FLujos
            //                        SQL += ", ZID_FLUJO INT NULL  ";
            //                        SQL += ", ZID_ESTADO IN NULL  ";
            //                        SQL += ")  ";

            //                        M = Main.ETrazas(SQL, "1", " Consulta crea tabla");
            //                        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);

            //                    }

            //                    if (TablaObj != "")
            //                    {//Creamos la tabla de objetos
            //                        SQL = " CREATE TABLE " + TablaObj + " ( ";
            //                        SQL += " ZID int IDENTITY(1, 1) NOT NULL, ";
            //                        SQL += " ZID_DOMAIN int NULL, ";
            //                        SQL += " ZID_ARCHIVO int NULL, ";
            //                        SQL += " ZDESCRIPCION varchar(255) NULL, ";
            //                        SQL += " ZTITULO varchar(255) NULL, ";
            //                        SQL += " ZDIRECTORIO varchar(255) NULL, ";
            //                        SQL += " ZRUTA varchar(255) NULL, ";
            //                        SQL += " ZPESO varchar(255) NULL, ";
            //                        SQL += " ZROOT int NULL, ";
            //                        SQL += " ZKEY int NULL, ";
            //                        SQL += " ZESTADO int NULL, ";
            //                        SQL += " ZFECHA datetime NULL, ";
            //                        SQL += " ZCATEGORIA varchar(255) NULL, ";
            //                        SQL += " ZSUBCATEGORIA varchar(255) NULL, ";
            //                        SQL += " ZUSER varchar(255) NULL, ";
            //                        SQL += " ZNIVEL int NULL, ";
            //                        SQL += " ZID_VOLUMEN int NULL, ";
            //                        SQL += " ZID_REGISTRO int NULL, ";
            //                        SQL += " ZFIRMA int NULL, ";
            //                        SQL += " ZLLAVE varchar(MAX) NULL) ";

            //                        M = Main.ETrazas(SQL, "1", "  Consulta crea tabla objetos");
            //                        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);

            //                    }

            //                    //Los datos se insertan

            //                    //string[] Columnas = System.Text.RegularExpressions.Regex.Split(Campos, ",");
            //                    string[] Formatos = System.Text.RegularExpressions.Regex.Split(Formato, ",");
            //                    string Valor = "";

            //                    foreach (DataRow fila in dt.Rows)
            //                    {
            //                        SQL = "INSERT INTO " + Tabla + " (" + Campos + ") VALUES (";
            //                        for (int i = 0; i < Columnas.Count() -1; i++)
            //                        {
            //                            if (Formatos[i] == "VARCHAR" || Formatos[i] == "DATETIME")
            //                            {
            //                                if(Valor == "")
            //                                {
            //                                    SQL += "'" + fila[Columnas[i]].ToString() + "',";
            //                                }
            //                                else
            //                                {
            //                                    SQL += ",'" + fila[Columnas[i]].ToString() + "'";
            //                                }
            //                            }
            //                            else
            //                            {
            //                                if (Valor == "")
            //                                {
            //                                    SQL += " " + fila[Columnas[i]].ToString() + " ";
            //                                }
            //                                else
            //                                {
            //                                    SQL += "," + fila[Columnas[i]].ToString() + " ";
            //                                }
            //                            }

            //                        }
            //                        M = Main.ETrazas(SQL, "1", " btnEjecProceso_Click");
            //                    }

            //                    //Inserta en Archivos
            //                    //SQL = "INSERT INTO ZARCHIVOS (ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZDUPLICADOS, ZKEY, ZVIEW, ZCONEXION) ";
            //                    //SQL += " VALUES ('" + Tabla + "', 9, 0, '" + Tabla + "','" + TablaObj + "', 1, 2, 1, 0, 0, 39)";

            //                    //MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZARCHIVOS", dbParams)) + 1;
            //                    //Inserta Campo

            //                    //Insertta Archivos Campos
            //                    //Column = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
            //                }
            //                else
            //                {
            //                    Esta = true;
            //                }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            if (Variables.Error != "")
            //            {
            //                Lbmensaje.Text = "Error. " + Variables.Error + " " + ex.Message;
            //                cuestion.Visible = false;
            //                Asume.Visible = true;
            //                DvPreparado.Visible = true;
            //                string M = Main.ETrazas(Variables.Error, "1", " btnEjecProceso_Click -->" + ex.Message);
            //            }
            //        }
            //    }

            //}

        }

        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
        }
        protected void DrProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = DrProcesos.SelectedIndex;
            this.Session["Elproceso"] = DrProcesos.SelectedIndex;

            //MProcesos.Attributes["style"] = "background-color:#bdecb6;";
            //MPlantilas.Attributes["style"] = "background-color:#ffffff;";
            //MMarcadores.Attributes["style"] = "background-color:#ffffff;";

            ListPlantillas.Items.Clear();
            ListPlantillasID.Items.Clear();

#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                string SQL = "SELECT ZID, ZDESCRIPCION, ZPLANTILLAS, ZID_ESTADO, ZQUERY, ZID_ARCHIVO, ZID_FLUJO, ZEJECUCION, ZPARAMETROS ";
                SQL += " FROM ZPROCESOS WHERE ZID = " + DrProcesos.SelectedValue  ;
                DataTable dt = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    TextIdProceso.Text = dr["ZID"].ToString();
                    TxtDescProceso.Text = dr["ZDESCRIPCION"].ToString();
                    DrEstadoIni.SelectedValue = dr["ZID_ESTADO"].ToString();
                    DrArchivoIni.SelectedValue = dr["ZID_ARCHIVO"].ToString();
                    DrFlujoIni.SelectedValue = dr["ZID_FLUJO"].ToString();
                    DrEjecucion.SelectedValue = dr["ZEJECUCION"].ToString();
                    //TxtPlantillas.Text = dr["ZPLANTILLAS"].ToString();




                    string [] Fields = System.Text.RegularExpressions.Regex.Split(dr["ZPLANTILLAS"].ToString(), "-");
                    if (Fields.Count() > 0)
                    {
                        if (Fields[0] != "")
                        {
                            for (int i = 0; i < Fields.Count(); i++)
                            {
                                SQL = "SELECT ZID, ZDESCRIPCION ";
                                SQL += " FROM ZPLANTILLAS WHERE ZID = " + Fields[i];
                                DataTable dtA = Main.BuscaLote(SQL).Tables[0];

                                foreach (DataRow drA in dtA.Rows)
                                {
                                    ListPlantillas.Items.Add(drA["ZDESCRIPCION"].ToString());
                                    ListPlantillasID.Items.Add(drA["ZID"].ToString());
                                }
                            }
                        }
                    }

                    TxtZQuery.Text = dr["ZQUERY"].ToString().Replace("&#39;", "'");
                    TxtParametros.Text = dr["ZPARAMETROS"].ToString();
                    break;
                }
                PopulateRootLevel();
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }

        protected void DrProcesoPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListPlantillas.BackColor = System.Drawing.Color.FromName("#bdecb6");
            ListPlantillas.Items.Add(new ListasID(DrProcesoPlantilla.SelectedItem.Text, Convert.ToInt32(DrProcesoPlantilla.SelectedItem.Value)).ToString());
            ListPlantillasID.Items.Add(new ListasID(DrProcesoPlantilla.SelectedItem.Value, Convert.ToInt32(DrProcesoPlantilla.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }

        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            DrArchivos.BackColor = System.Drawing.Color.FromName("#bdecb6");
            //DrCampoasig.BackColor = Color.FromName("#ffffff");

            DataTable dt = new DataTable();
            dt = Main.CargaFlujos("0", DrArchivos.SelectedItem.Value).Tables[0];
            this.Session["idarchivo"] = DrArchivos.SelectedItem.Value;

            DrFlujos.Items.Clear();
            DrFlujos.DataValueField = "ZID";
            DrFlujos.DataTextField = "ZDESCRIPCION";

            //DrCampoasig.DataValueField = "ZID";
            //DrCampoasig.DataTextField = "ZDESCRIPCION";

            DrFlujos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrFlujos.DataSource = dt;
            DrFlujos.DataBind();








        }

        protected void Busca_Root(int ID)
        {
            string SQL = "";
            string A = "";
            int N = -1;
            try
            {
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];

                    N = -1;
                    A = Convert.ToString(ID);
                    int Salida = ID;

                    while (N != 0)
                    {
                        SQL += "SELECT A.ZROOT FROM ZARCHIVOS A WHERE A.ZID =" + ID;
                        N = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT A.ZROOT FROM ZARCHIVOS A WHERE A.ZID =" + ID, dbParams));
                        if ((N != 0))
                        {
                            ID = N;
                            A = Convert.ToString(N) + "-" + A;
                        }
                        else
                        {
                            Buscaregistro = ID;
                            break;
                        }
                    }

                    ListadoArchivos = A.Split('-');
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];

                    //int N = -1;
                    //string A = Convert.ToString(ID);
                    //int Salida = ID;

                    //while (N != 0)
                    //{
                    //    N = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT A.ZROOT FROM USER_GEDESPOL.ZARCHIVOS A WHERE A.ZID =" + ID, dbParams));
                    //    if ((N != 0))
                    //    {
                    //        ID = N;
                    //        A = Convert.ToString(N) + "-" + A;
                    //    }
                    //    else
                    //    {
                    //        Buscaregistro = ID;
                    //        break;
                    //    }
                    //}

                    //ListadoArchivos = A.Split('-');

                }
            }
            catch(Exception ex)
            {
                string a = Main.ETrazas(SQL, "1", " Busca_Root --> Error:" + ex.Message + " --> " + A.ToString());
            }

            //int I = 0;
            //foreach (var word in words)
            //{
            //    if(Convert.ToString(Salida) != word)
            //    {
            //        if (I == 0)
            //        {
            //            PopulateRootLevel(Convert.ToInt32(Convert.ToInt32(word)));
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}

        }

        protected void DrPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

        }

        protected void drCampofiltroselección_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean Esta = false;
            ListBox3.BackColor = System.Drawing.Color.FromName("#bdecb6");
            for (int i = 0; i < ListBox3.Items.Count; i++)
            {
                if (drCampofiltroseleccion.SelectedItem.Text == ListBox3.Items[i].Text)
                {
                    Esta = true;
                    break;
                }
            }
            if(Esta == false)
            {
                ListBox3.Items.Add(new ListasID(drCampofiltroseleccion.SelectedItem.Text, Convert.ToInt32(drCampofiltroseleccion.SelectedItem.Value)).ToString());
                ListBox4.Items.Add(new ListasID(drCampofiltroseleccion.SelectedItem.Value, Convert.ToInt32(drCampofiltroseleccion.SelectedItem.Value)).ToString());
            }

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProfile(1);
            }

        }

        protected void drProfilesAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProfile(1);
            }
        }

        protected void DrArchivTempla_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListTempla.BackColor = System.Drawing.Color.FromName("#bdecb6");
            ListTempla.Items.Add(new ListasID(DrArchivTempla.SelectedItem.Text, Convert.ToInt32(DrArchivTempla.SelectedItem.Value)).ToString());
            ListTemplaID.Items.Add(new ListasID(DrArchivTempla.SelectedItem.Value, Convert.ToInt32(DrArchivTempla.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaTemplate(1);
            }

        }
        protected void dlConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#bdecb6");
            ListBoxArchivo.Items.Add(new ListasID(DrConexion.SelectedItem.Text, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            ListBoxArchivoID.Items.Add(new ListasID(DrConexion.SelectedItem.Value, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

        }
        protected void ListBoxArchivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
        }

        protected void ListTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListTemplateID.Items.RemoveAt(ListTemplate.SelectedIndex);
            ListTemplate.Items.RemoveAt(ListTemplate.SelectedIndex);
        }

        private void CompruebaCampos(string MiId, string TablaName, string TablaNameobj, string Campos, string Formatos)
        {
            SqlParameter[] dbParams = new SqlParameter[0];
            Boolean Esta = false;

            string Column = "";
            int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
            string Tabla = "ZARCHIVOS"; // this.Session["Tabla"].ToString();
            string SQL = "";
            this.Session["IDArchivo"] = MiId;

            //Directamente sobre la Tabla Campos
            DataTable dtCampos = Main.CargaCampos().Tables[0];
            DataTable dtformatos = Main.CargaFormatoCampos().Tables[0];

            //Borra la relacion de campos y los vuelve a escribir segun los encuentra
            Column = "DELETE FROM ZARCHIVOCAMPOS WHERE ZIDARCHIVO = " + MiId;
            DBHelper.ExecuteNonQuerySQL(Column, dbParams);
            int Contador = 1;
            //Directamente sobre la Tabla Archivo
            try
            {
                String [] Fields = System.Text.RegularExpressions.Regex.Split(Campos, Environment.NewLine);
                String[] Formats = System.Text.RegularExpressions.Regex.Split(Formatos, Environment.NewLine);

                //Busco cada campo seleccionado en la consulta, en la lista de campos
                for (int i = 0; i < Fields.Count(); i++)
                {
                    foreach (DataRow filacampo in dtCampos.Rows)
                    {
                        //Si encuentra el nombre del Campo
                        if (filacampo["ZTITULO"].ToString() == Fields[i])
                        {
                            Esta = true;
                            //si encuentra el mismo formato
                            foreach (DataRow filaFormato in dtformatos.Rows)
                            {
                                for (int a = 0; a < Formats.Count(); a++)
                                {
                                    if (filaFormato["ZFORMATO"].ToString() == Formats[a])
                                    {
                                        Esta = true;
                                        SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiId + "," + filacampo["ZID"].ToString() + "," + Contador + ", 0) ";
                                        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                        Contador += 1;
                                        break;
                                    }
                                }
                                if (Esta == true)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (Esta == false)
                    {
                        //Inserta en Campos
                        string TipoFormato = "1";
                        string ValorFormato = "255";

                        foreach (DataRow filaFormato in dtformatos.Rows)
                        {
                            if (filaFormato["ZFORMATO"].ToString() == Formats[i])
                            {
                                Esta = true;
                                TipoFormato = filaFormato["ZID"].ToString();
                                ValorFormato = filaFormato["ZVALOR"].ToString();
                                break;
                            }
                        }
                        SQL = " INSERT INTO ZCAMPOS ( ZTITULO, ZDESCRIPCION, ZESTADO, ZNIVEL, ZFECHA, ZTIPO, ZVALOR, ZVALORDEFECTO, ZVALIDACION, ZACTIVO) ";
                        SQL += " VALUES ('" + Fields[i] + "','" + Fields[i] + "', 1, 5,'" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "," + TipoFormato + "," + ValorFormato + ",0,1) ";
                        
                        int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams)) + 1;
                        SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiId + "," + MiID + "," + Contador + ", 0) ";
                        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                        Contador += 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int MiID = 0;
            //string Ver = "";
            //Nada
            Boolean Esta = false;

            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 0)
            {

            }
            //Si es nuevo
            SqlParameter[] dbParams = new SqlParameter[0];
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 1)
            {


                string Column = "";
#pragma warning disable CS0219 // La variable 'ColumnVal' está asignada pero su valor nunca se usa
                string ColumnVal = "(";
#pragma warning restore CS0219 // La variable 'ColumnVal' está asignada pero su valor nunca se usa
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = "ZARCHIVOS"; // this.Session["Tabla"].ToString();

                //int Existe = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT COUNT(ZID) FROM ZARCHIVOS WHERE ZIDTABLA = '" + TxtNombre.Text + "' AND ZDESCRIPCION = '" + TxtDescripcion.Text + "'";// AND ZTABLENAME = '" + TablaName.Text + "'", dbParams));

                //if(Existe == 0)
                //{
                //    int M = 0;
                //    if (DrCampoasig.SelectedValue == "") { M = 0; } else { M = Convert.ToInt32(DrCampoasig.SelectedValue); }
                //    Column = "INSERT INTO " + Tabla + " (ZIDTABLA, ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZKEY, ZDUPLICADOS) ";
                //    ColumnVal = " VALUES('" + TxtNombre.Text + "','" + TxtDescripcion.Text + "'," + dlNivel.SelectedValue + "," + Djerarquia.SelectedValue + ",'" + TablaName.Text + "','" + TablaObj.Text + "'," + Dtipo.SelectedValue + "," + dlEstado.SelectedValue + "," + DrConexion.SelectedValue + "," + M + "," + DrDuplicado.SelectedValue + ")";
                //    Column += ColumnVal;
                //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                //}


                //MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT ZID FROM ZARCHIVOS WHERE ZIDTABLA = '" + TxtNombre.Text + "' AND ZDESCRIPCION = '" + TxtDescripcion.Text + "' AND ZTABLENAME = '" + TablaName.Text + "'", dbParams));

                this.Session["IDArchivo"] = MiID;

                //Directamente sobre la Tabla Campos
                DataTable dtCampos = Main.CargaCampos().Tables[0];

                //Borra la relacion de campos y los vuelve a escribir segun los encuentra
                Column = "DELETE FROM ZARCHIVOCAMPOS WHERE ZIDARCHIVO = " + MiID;
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                int Contador = 1;
                //Directamente sobre la Tabla Archivo
                try
                {
                   //Tabla = TablaName.Text;
                   DataTable dtTAbla = Main.PropiedadesTabla(Tabla).Tables[0];
                    //string Campo = "";
                    string SQL = "";
                    //Si está vacia recorro y creo la Tabla de informacion.
                    if (dtTAbla.Rows.Count > 0)
                    {//Existe, busca los que no esten y los actualiza
                        foreach (DataRow filacampo in dtCampos.Rows)
                        {
                           //Busco el campo seleccionado en la lista
                            for (int i = 0; i < ListBox2ID.Items.Count; i++)
                            {
                                if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                {
                                    //Busco si está la columna está creada
                                    Esta = false;
                                    foreach (DataRow filatabla in dtTAbla.Rows)
                                    {
                                        if (filacampo["ZTITULO"].ToString() == filatabla["Columna"].ToString())
                                        {
                                            Esta = true;
                                            break;
                                        }
                                    }
                                    if (Esta == false)
                                    {
                                        //No está
                                        if (Tabla != "")
                                        {
                                            Tabla = Tabla.Replace(" ", "_");
                                            if (filacampo["ZFORMATO"].ToString() == "Decimal" || filacampo["ZFORMATO"].ToString() == "Varchar" || filacampo["ZFORMATO"].ToString() == "Numerico")
                                            {
                                                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + "); ";
                                            }
                                            else
                                            {
                                                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " ;"; // + filacampo["ZVALOR"].ToString() + "; ";
                                            }
                                            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                        }
                                    }

                                    SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                    DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                    Contador += 1;
                                }
                            }
                        }
                    }
                    else
                    {//No Existe, crea todos los campos seleccionados
                        if (Tabla != "")
                        {
                            Tabla = Tabla.Replace(" ", "_");

                            SQL = " CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL ";
                            for (int i = 0; i < ListBox2ID.Items.Count; i++)
                            {
                                foreach (DataRow filacampo in dtCampos.Rows)
                                {
                                    if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                    {
                                        if (filacampo["ZFORMATO"].ToString().ToLower() == "decimal" || filacampo["ZFORMATO"].ToString().ToLower() == "varchar" || filacampo["ZFORMATO"].ToString().ToLower() == "numerico")
                                        {
                                            SQL += ", " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + ") ";
                                        }
                                        else
                                        {
                                            SQL += ",  " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + "  "; // + filacampo["ZVALOR"].ToString() + "; ";
                                        }
                                        Column = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                                        Contador += 1;
                                    }
                                }
                            }

                            SQL += ")  ";
                            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                        }
                        else
                        {
                            for (int i = 0; i < ListBox2ID.Items.Count; i++)
                            {
                                foreach (DataRow filacampo in dtCampos.Rows)
                                {
                                    if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                    {
                                        Column = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                                        Contador += 1;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    string a = ex.Message;
                }

                DataTable dtO = null;
                //Si está vacia recorro y creo la Tabla Objetos
                //if (TablaObj.Text != "")
                //{
                //    if (TablaObj.Text.Contains("OBJ"))
                //    {
                //        dtO = Main.PropiedadesTablaObjeto(TablaObj.Text).Tables[0];
                //        Tabla = TablaObj.Text;
                //    }
                //    else
                //    {
                //        Tabla = Tabla + "OBJ";
                //        dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                //    }
                //}
                if (dtO != null)
                {//Existe,
                    if (dtO.Rows.Count > 0)
                    {//Existe,
                    }
                    else
                    {//Creamos la tabla
                        if (Tabla != "")
                        {
                            Tabla = Tabla.Replace(" ", "_");
                            string SQL = " CREATE TABLE " + Tabla + " ( ";
                            SQL += " ZID int IDENTITY(1, 1) NOT NULL, ";
                            SQL += " ZID_DOMAIN int NULL, ";
                            SQL += " ZID_ARCHIVO int NULL, ";
                            SQL += " ZDESCRIPCION varchar(255) NULL, ";
                            SQL += " ZTITULO varchar(255) NULL, ";
                            SQL += " ZDIRECTORIO varchar(255) NULL, ";
                            SQL += " ZRUTA varchar(255) NULL, ";
                            SQL += " ZPESO varchar(255) NULL, ";
                            SQL += " ZROOT int NULL, ";
                            SQL += " ZKEY int NULL, ";
                            SQL += " ZESTADO int NULL, ";
                            SQL += " ZFECHA datetime NULL, ";
                            SQL += " ZCATEGORIA varchar(255) NULL, ";
                            SQL += " ZSUBCATEGORIA varchar(255) NULL, ";
                            SQL += " ZUSER varchar(255) NULL, ";
                            SQL += " ZNIVEL int NULL, ";
                            SQL += " ZID_VOLUMEN int NULL, ";
                            SQL += " ZID_REGISTRO int NULL, ";
                            SQL += " ZFIRMA int NULL, ";
                            SQL += " ZLLAVE varchar(MAX) NULL) ";

                            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                        }
                    }
                }

                //Existe = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT COUNT(ZID) FROM ZVOLUMENES WHERE ZID_ARCHIVO = " + MiID , dbParams));

                //if (Existe == 0)
                //{
                //    string ruta = Convert.ToString(DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZRUTA) FROM ZVOLUMENES ", dbParams));
                //    //Cambiar por contador de caracteres
                //    string mascara = "ZVRE00000000" + MiID;

                //    Column = "INSERT INTO ZVOLUMENES (ZID_DOMINIO, ZID_ARCHIVO, ZNOMBRE, ZRUTA, ZFECHA_CREATE,ZFECHA_MOD, ZSIZE, ZACTIVO) ";
                //    ColumnVal = " VALUES('0','" + MiID  + "','" + mascara + "','" + ruta + "','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "','" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "',0,1)";
                //    Column += ColumnVal;
                //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                //}
               
                this.Session["Edicion"] = 0;
                this.Session["proceso"] = "";
            }
            //Si es edición
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 2)
            {
                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = "";
                MiID = Convert.ToInt32(this.Session["IDArchivo"].ToString());
                if (Variables.configuracionDB == 0)
                {
                    //SqlParameter[] dbParams = new SqlParameter[0];
                    Tabla = "ZARCHIVOS"; // this.Session["Tabla"].ToString();
                    //Column = "UPDATE " + Tabla + " SET  ZDESCRIPCION ='" + TxtDescripcion.Text + "', ZNIVEL = " + dlNivel.SelectedValue + ", ZROOT ='" + Djerarquia.SelectedValue + "',";
                    //Column += "ZTABLENAME ='" + TablaName.Text + "', ZTABLEOBJ = '" + TablaObj.Text + "', ZTIPO = " + Dtipo.SelectedValue + ", ZESTADO =" + dlEstado.SelectedValue + ", ";
                    Column += "ZCONEXION ='" + DrConexion.SelectedValue + "', ";
                    //Column += "ZDUPLICADOS ='" + DrDuplicado.SelectedValue + "', ";
                    //Column += "ZKEY ='" + DrCampoasig.SelectedValue + "' ";
                    Column += " WHERE ZID = " + MiID;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    Tabla = "ZARCHIVOCAMPOS"; // this.Session["Tabla"].ToString();
                    Column = "DELETE FROM  " + Tabla + " ";
                    ColumnVal = " WHERE ZIDARCHIVO = " + MiID;
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    //ListBox2 contiene los campos seleccionados
                    //modifica la Tabla Archivo

                    //Directamente sobre la Tabla Campos
                    DataTable dtCampos = Main.CargaCampos().Tables[0];

                    int Contador = 1;
                    //Directamente sobre la Tabla Archivo
                    try
                    {

                        //Tabla = TablaName.Text;
                        DataTable dtTAbla = Main.PropiedadesTabla(Tabla).Tables[0];
                        string SQL = "";
                        //Si está vacia recorro y creo la Tabla de informacion.
                        if (dtTAbla.Rows.Count > 0)
                        {//Existe, busca los que no esten y los actualiza
                         //Busco el campo seleccionado en la lista
                            for (int i = 0; i < ListBox2ID.Items.Count; i++)
                            {
                                Esta = false;
                                foreach (DataRow filacampo in dtCampos.Rows)
                                {
                                    if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                    {
                                        //Busco si está la columna está creada
                                        foreach (DataRow filatabla in dtTAbla.Rows)
                                        {
                                            if (filacampo["ZTITULO"].ToString() == filatabla["Columna"].ToString())
                                            {
                                                Esta = true;
                                                break;
                                            }
                                        }
                                        if (Esta == false)
                                        {
                                            //No está
                                            if (Tabla != "")
                                            {
                                                Tabla = Tabla.Replace(" ", "_");
                                                if (filacampo["ZFORMATO"].ToString() == "Decimal" || filacampo["ZFORMATO"].ToString() == "Varchar" || filacampo["ZFORMATO"].ToString() == "Numerico")
                                                {
                                                    SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + "); ";
                                                }
                                                else
                                                {
                                                    SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " ;"; // + filacampo["ZVALOR"].ToString() + "; ";
                                                }
                                                DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                            }
                                        }
                                        SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                        Contador += 1;
                                    }
                                }
                            }
                        }
                        else
                        {//No Existe, crea todos los campos seleccionados
                            if (Tabla != "")
                            {
                                Tabla = Tabla.Replace(" ", "_");

                                SQL = " CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL ";
                                for (int i = 0; i < ListBox2ID.Items.Count; i++)
                                {
                                    foreach (DataRow filacampo in dtCampos.Rows)
                                    {
                                        if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                        {
                                            if (filacampo["ZFORMATO"].ToString() == "Decimal" || filacampo["ZFORMATO"].ToString() == "Varchar" || filacampo["ZFORMATO"].ToString() == "Numerico")
                                            {
                                                SQL += ", " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + ") ";
                                            }
                                            else
                                            {
                                                SQL += ",  " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + "  "; // + filacampo["ZVALOR"].ToString() + "; ";
                                            }
                                            Column = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                            DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                                            Contador += 1;
                                        }
                                    }
                                }
                                SQL += ")  ";
                                DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            }
                            else
                            {
                                for (int i = 0; i < ListBox2ID.Items.Count; i++)
                                {
                                    foreach (DataRow filacampo in dtCampos.Rows)
                                    {
                                        if (filacampo["ZID"].ToString() == ListBox2ID.Items[i].Text)
                                        {
                                            Column = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", " + ListKeys.Items[i].Text + ") ";
                                            DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                                            Contador += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string a = ex.Message;
                    }
                    //Mdifica la Tabla Objetos
                    Esta = false;

                    DataTable dtO = null;
                    //Si está vacia recorro y creo la Tabla Objetos
                    //if (TablaObj.Text != "")
                    //{
                    //    if (TablaObj.Text.Contains("OBJ"))
                    //    {
                    //        Tabla = TablaObj.Text;
                    //        //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                    //        dtO = Main.ExisteTabla(Tabla).Tables[0];

                    //        Esta = true;
                    //    }
                    //    else
                    //    {
                    //        Tabla = TablaObj.Text + "OBJ";
                    //        //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                    //        dtO = Main.ExisteTabla(Tabla).Tables[0];
                    //        Esta = true;
                    //    }
                    //}
                    //Existe Vacio
                    if (dtO != null)
                    {
                        if (dtO.Rows.Count > 0)
                        {//Existe,
                        }
                        else
                        {//Creamos la tabla
                            string SQL = " CREATE TABLE " + Tabla + " ( ";
                            SQL += " ZID int IDENTITY(1, 1) NOT NULL, ";
                            SQL += " ZID_DOMAIN int NULL, ";
                            SQL += " ZID_ARCHIVO int NULL, ";
                            SQL += " ZDESCRIPCION varchar(255) NULL, ";
                            SQL += " ZTITULO varchar(255) NULL, ";
                            SQL += " ZDIRECTORIO varchar(255) NULL, ";
                            SQL += " ZRUTA varchar(255) NULL, ";
                            SQL += " ZPESO varchar(255) NULL, ";
                            SQL += " ZROOT int NULL, ";
                            SQL += " ZKEY int NULL, ";
                            SQL += " ZESTADO int NULL, ";
                            SQL += " ZFECHA datetime NULL, ";
                            SQL += " ZCATEGORIA varchar(255) NULL, ";
                            SQL += " ZSUBCATEGORIA varchar(255) NULL, ";
                            SQL += " ZUSER varchar(255) NULL, ";
                            SQL += " ZNIVEL int NULL, ";
                            SQL += " ZID_VOLUMEN int NULL, ";
                            SQL += " ZID_REGISTRO int NULL, ";
                            SQL += " ZFIRMA int NULL, ";
                            SQL += " ZLLAVE varchar(MAX) NULL) ";

                            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                        }
                    }

                    this.Session["Edicion"] = 0;
                    this.Session["proceso"] = "";
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //Tabla = "USER_GEDESPOL.ZARCHIVOS"; // this.Session["Tabla"].ToString();
                    //Column = "UPDATE " + Tabla + " SET  ZNOMBRE ='" + TxtNombre.Text + "', ZDESCRIPCION ='" + TxtDescripcion.Text + "', ZNIVEL = " + dlNivel.SelectedValue + ", ZROOT ='" + Djerarquia.SelectedValue + "',";
                    //Column += "ZTABLENAME ='" + TablaName.Text + "', ZTABLEOBJ = '" + TablaObj.Text + "', ZTIPO = " + Dtipo.SelectedValue + ", ZESTADO =" + dlEstado.SelectedValue + "";
                    //Column += " WHERE ZID = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                    //Tabla = "USER_GEDESPOL.ZARCHIVOCAMPO"; // this.Session["Tabla"].ToString();
                    //Column = "DELETE FROM  " + Tabla + " ";
                    //ColumnVal = " WHERE ZID_ARCHIVO = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    //Column += ColumnVal;
                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                }

                //DataTable dtA = this.Session["Campo"] as DataTable;

                //int J = 1;


                //foreach (var items in ListBox2.Items)
                //{
                //    foreach (DataRow fila in dtA.Rows)
                //    {
                //        //ver = items.ToString();
                //        //Ver = fila["ZNOMBRE"].ToString();

                //        if (items.ToString() == fila["ZDESCRIPCION"].ToString())
                //        {
                //            if (Variables.configuracionDB == 0)
                //            {
                //                //SqlParameter[] dbParams = new SqlParameter[0];
                //                Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                //                ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                //                Column += ColumnVal;
                //                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                //            }
                //            else if (Variables.configuracionDB == 1)
                //            {
                //                //OracleParameter[] dbParams = new OracleParameter[0];
                //                //Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                //                //ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                //                //Column += ColumnVal;
                //                //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                //            }
                //            J += 1;
                //            break;
                //        }

                //    }

                //}


                //messageBox.ShowMessage("El Registro se modificó correctamente!");
                this.Session["Edicion"] = 0;
                this.Session["proceso"] = "";
            }

            foreach (System.Web.UI.Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }

            }
            DrArchivos.Enabled = true;
            DrArchivos.Items.Clear();
            //Djerarquia.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            //Djerarquia.DataValueField = "ZID";
            //Djerarquia.DataTextField = "ZDESCRIPCION";

            DrArchivos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            //Djerarquia.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            this.Session["Archivo"] = dt;
            DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
            DrArchivos.DataBind();

            //Djerarquia.DataSource = dt;
            //Djerarquia.DataBind();

            DesactivarTxt();
            Limpiar();

            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";
            //btnEditar.Visible = true;
            //btnNuevo.Visible = true;
            //btnGuardar.Visible = false;
            //btnCancelar.Visible = false;
        }

        public void Limpiar()
        {
            int MiID = 0;
            //MiID =  Convert.ToInt32(cmd.ExecuteScalar());
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                //maximo id
                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZARCHIVOS", dbParams));
            }
            else
            {
                //OracleParameter[] dbParams = new OracleParameter[0];

                ////maximo id
                //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZARCHIVOS", dbParams));
            }
            MiID = MiID + 1;

            LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + MiID;
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            //TablaName.Text = "";
            //TablaObj.Text = "";

            //TextDocElec.Text = "";
            //TextRuta.Text = "";
            //DrDuplicado.SelectedIndex = 0;
            DrConexion.SelectedIndex = 0;
            //dlNivel.SelectedIndex = 9;
            //Djerarquia.SelectedIndex = -1;
            //dlEstado.SelectedIndex = 1;
            //Dtipo.SelectedIndex = 0;
            ////TextRuta.Text = Server.MapPath("~/volumen/"); 
            //TextRegistro.Text = "0";
            //Textunidad.Text = "0";
            //TextHardDisc.Text = "0 MB";
            CommentSQL.Text = "";

            //TextDuplicado.Text = "";
            //dlNivel.Text = "";
            //TxtMail.Text = "";
            this.Session["Edicion"] = 0;
            this.Session["proceso"] = "";
        }

        protected void ListBox1ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1ID.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1ID.SelectedIndex;
            ListBox1.SelectedIndex = ListBox1ID.SelectedIndex;

            string a = ListBox1ID.SelectedValue;


            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

            //foreach (DataRow fila in dtCampos.Rows)
            //{
            //    if (fila["ZID"].ToString() == a)
            //    {
            //        LbCampo1.Text = "Campo:" + fila["ZTITULO"].ToString();
            //        LbId1.Text = "ID:" + fila["ZID"].ToString();
            //        LbTipo1.Text = "Tipo:" + fila["ZFORMATO"].ToString();
            //        LbSize1.Text = "Tamaño:" + fila["ZVALOR"].ToString();
            //        break;
            //    }
            //}
        }

  
        

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1.SelectedIndex;
            ListBox1ID.SelectedIndex = ListBox1.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;

            string a = ListBox1ID.SelectedValue;

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                ListBox1.BackColor = System.Drawing.Color.FromName("#bdecb6");
            }

            DataTable dt = Main.CargaEstadosFlujos("0",ListBox1ID.SelectedItem.Value).Tables[0];

            this.Session["idestado"] = ListBox1ID.SelectedItem.Value;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ZID"].ToString() == ListBox1ID.SelectedItem.Value)
                {
                    TxtidEstado.Text = dr["ZID"].ToString();
                    TxtEstado.Text = dr["ZNOMBRE"].ToString();
                    TxtCondicion.Text = dr["ZIMG"].ToString();
                    TxtCondicionQR.Text = dr["ZCONDICION"].ToString();
                    string miro = dr["ZPREVIUS"].ToString();

                    if (dr["ZPREVIUSVIEW"].ToString() == "1")
                    {
                        Imgatras.Visible = true;
                        ImgatrasC.Visible = false;
                    }
                    else
                    {
                        Imgatras.Visible = false;
                        ImgatrasC.Visible = true;
                    }
                    if (dr["ZNEXTVIEW"].ToString() == "1")
                    {
                        ImgNext.Visible = true;
                        ImgNextC.Visible = false;
                    }
                    else
                    {
                        ImgNext.Visible = false;
                        ImgNextC.Visible = true;
                    }
                    if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
                    {
                        ImgAlter.Visible = true;
                        ImgAlterC.Visible = false;
                    }
                    else
                    {
                        ImgAlter.Visible = false;
                        ImgAlterC.Visible = true;
                    }
                    if (dr["ZENDVIEW"].ToString() == "1")
                    {
                        imgFin.Visible = true;
                        imgFinC.Visible = false;
                    }
                    else
                    {
                        imgFin.Visible = false;
                        imgFinC.Visible = true;
                    }

                    for (int i = 0; i <= Dratras.Items.Count - 1; i++)
                    {
                        if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
                        {
                            Dratras.SelectedValue = dr["ZPREVIUS"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
                    {
                        if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
                        {
                            Drsiguiente.SelectedValue = dr["ZNEXT"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
                    {
                        if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
                        {
                            Dralternativo.SelectedValue = dr["ZALTERNATIVE"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
                    {
                        if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
                        {
                            Drfinal.SelectedValue = dr["ZEND"].ToString();
                            break;
                        }
                    }
                }
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2.SelectedIndex;
            ListBox2ID.SelectedIndex = ListBox2.SelectedIndex;
            //ListBox2Col.SelectedIndex = ListBox2.SelectedIndex;
            //ListKeys.SelectedIndex = ListBox2.SelectedIndex;
            //string a = ListBox2ID.SelectedValue;
            //string b = ListKeys.SelectedValue;

            for (int i = 0; i <= DrEstado.Items.Count - 1; i++)
            {
                if (DrEstado.Items[i].Value == ListBox2ID.SelectedItem.Value)
                {
                    DrEstado.SelectedIndex = i;
                    break;
                }
            }

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                ListBox2.BackColor = System.Drawing.Color.FromName("#bdecb6");
            }

            DataTable dt = Main.BuscaEstado(Convert.ToInt32(ListBox2ID.SelectedItem.Value)).Tables[0];

            this.Session["idestado"] = ListBox2ID.SelectedItem.Value;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ZID"].ToString() == ListBox2ID.SelectedItem.Value)
                {
                    TxtidEstado.Text = dr["ZID"].ToString();
                    TxtEstado.Text = dr["ZNOMBRE"].ToString();
                    TxtCondicion.Text = dr["ZIMG"].ToString();
                    TxtCondicionQR.Text = dr["ZCONDICION"].ToString();
                    string miro = dr["ZPREVIUS"].ToString();

                    if (dr["ZPREVIUSVIEW"].ToString() == "1")
                    {
                        Imgatras.Visible = true;
                        ImgatrasC.Visible = false;
                    }
                    else
                    {
                        Imgatras.Visible = false;
                        ImgatrasC.Visible = true;
                    }
                    if (dr["ZNEXTVIEW"].ToString() == "1")
                    {
                        ImgNext.Visible = true;
                        ImgNextC.Visible = false;
                    }
                    else
                    {
                        ImgNext.Visible = false;
                        ImgNextC.Visible = true;
                    }
                    if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
                    {
                        ImgAlter.Visible = true;
                        ImgAlterC.Visible = false;
                    }
                    else
                    {
                        ImgAlter.Visible = false;
                        ImgAlterC.Visible = true;
                    }
                    if (dr["ZENDVIEW"].ToString() == "1")
                    {
                        imgFin.Visible = true;
                        imgFinC.Visible = false;
                    }
                    else
                    {
                        imgFin.Visible = false;
                        imgFinC.Visible = true;
                    }

                    for (int i = 0; i <= Dratras.Items.Count - 1; i++)
                    {
                        if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
                        {
                            Dratras.SelectedValue = dr["ZPREVIUS"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
                    {
                        if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
                        {
                            Drsiguiente.SelectedValue = dr["ZNEXT"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
                    {
                        if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
                        {
                            Dralternativo.SelectedValue = dr["ZALTERNATIVE"].ToString();
                            break;
                        }
                    }

                    for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
                    {
                        if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
                        {
                            Drfinal.SelectedValue = dr["ZEND"].ToString();
                            break;
                        }
                    }
                }
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else 
            { 
                EditaFlujo(1);
            }
        }



        protected void ListBox2ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2ID.SelectedIndex;
            //ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
            ListBox2.SelectedIndex = ListBox2ID.SelectedIndex;
            ListKeys.SelectedIndex = ListBox2ID.SelectedIndex;

            string a = ListBox2ID.SelectedValue;
            string b = ListKeys.SelectedValue;
            //chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));

            //string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            //SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            //SQL += " WHERE A.ZTIPO = B.ZID ";
            //SQL += " AND A.ZID = " + a;
            //SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            //DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            //foreach (DataRow fila in dtCampos.Rows)
            //{
            //    if (fila["ZID"].ToString() == a)
            //    {
            //        LbCampo2.Text = "Campo:" + fila["ZTITULO"].ToString();
            //        LbId2.Text = "ID:" + fila["ZID"].ToString();
            //        LbTipo2.Text = "Tipo:" + fila["ZFORMATO"].ToString();
            //        LbSize2.Text = "Tamaño:" + fila["ZVALOR"].ToString();
            //        break;
            //    }
            //}
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

        protected void treeUser_SelectedNodeChanged(object sender, EventArgs e)
        {
            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + TreeDoc.SelectedNode.Value;
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
            this.Session["Archivos"] = dtArchivos;

            for (int i = 0; i < DrArchivos.Items.Count; i++)
            {
                if (DrArchivos.Items[i].Value == TreeDoc.SelectedNode.Value)
                {
                    DrArchivos.SelectedIndex = i;
                    //this.Session["idarchivo"] = DrArchivos.SelectedIndex.ToString();
                    this.Session["idarchivo"] = DrArchivos.SelectedItem.Value;
                    DrArchivos.BackColor = System.Drawing.Color.FromName("#bdecb6");
                    break;
                }
            }

            foreach (DataRow fila2 in dtArchivos.Rows)
            {

                if (fila2["ZTIPO"].ToString() == "2")
                {
                    //Si no es to Archivo Documental no muestra Tabla
                    break;
                }
                else
                {
#pragma warning disable CS0219 // La variable 'dtCampos' está asignada pero su valor nunca se usa
                    DataTable dtCampos = null;
#pragma warning restore CS0219 // La variable 'dtCampos' está asignada pero su valor nunca se usa
                    DataTable dt = Main.CargaCampos().Tables[0];
                    this.Session["Campos"] = dt;

                    //this.Session["Vista"] = "1";
                    //dtCampos = Relaciones(Convert.ToInt32(DrArchivos.SelectedItem.Value), dt);
                    //this.Session["Campos"] = dtCampos;
                    break;
                }
            }

        }

        private void PopulateRootLevel()
        {
            DataTable dt = new DataTable();

            TreeDoc.Nodes.Clear();
            string[] Fields = null;
            int a = 0;

            string SQL = "SELECT ZID, ZDESCRIPCION, ZPLANTILLAS, ZID_ESTADO, ZQUERY, ZID_ARCHIVO, ZID_FLUJO, ZEJECUCION ";
            SQL += " FROM ZPROCESOS WHERE ZID = " + DrProcesos.SelectedValue;
            dt = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dt.Rows)
            {
                if (a == 0) 
                {
                    PopulateNodoNuevo(TreeDoc.Nodes, fila["ZDESCRIPCION"].ToString());
                    a += 1;
                }
                
                Fields = System.Text.RegularExpressions.Regex.Split(fila["ZPLANTILLAS"].ToString(), "-");
                if (Fields.Count() > 0)
                {
                    for (int i = 0; i < Fields.Count() - 1; i++)
                    {
                        SQL = "SELECT ZID, ZDESCRIPCION, ZRUTAENTRADA, ZRUTASALIDA, ZCOPYORIGINAL, ZSIGNPDF, ZRUTAALTERNATIVA ";
                        SQL += " FROM ZPLANTILLAS WHERE ZID = " + Fields[i];
                        dt = Main.BuscaLote(SQL).Tables[0];

                        PopulateNodes(dt, TreeDoc.Nodes, 1, Fields[i]);
                    }
                }
                else
                {
                    PopulateNodes(dt, TreeDoc.Nodes, 0, "0");
                }
            }
        }

        private void PopulateNodoNuevo(TreeNodeCollection nodes, string Dato)
        {
            TreeNode tn = new TreeNode();
            tn.Text = Dato;
            tn.Value = "-1";
            nodes.Add(tn);
        }

        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes, int Nulo, string Plantilla)
        {
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
                    string SQL = "SELECT B.ZID, B.ZNOMBRE as ZDESCRIPCION, A.ZID_PLANTILLA, A.ZID_MARCADOR ";
                    SQL += " FROM ZPLANTILLAMARCADOR A, ZMARCADORES B WHERE A.ZID_PLANTILLA = " + Plantilla;
                    SQL += " AND A.ZID_MARCADOR = B.ZID ";
                    dt = Main.BuscaLote(SQL).Tables[0];

                    TreeNode tn = new TreeNode(); //TreeDoc
                    tn.Text = dr["ZDESCRIPCION"].ToString();
                    tn.Value = dr["ZID"].ToString();
                    nodes.Add(tn);

                    if (dt.Rows.Count > 0)
                    {
                        PopulateSubLevel(Convert.ToInt32(tn.Value), tn);
                    }
                }
            }
        }


        public DataTable RelacionesArchivo(int ID, DataTable DtCampos)
        {
            int MiID = ID;
            //DrCampos.Items.Clear();
            //DrCampos.Items.Insert(0, new ListItem("Ninguno", "0"));

            //DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dt1 = new DataTable();

            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];


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

        public void Relaciones(int ID, string sortExpression = null)
        {
            int MiID = ID;
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox1ID.Items.Clear();
            ListBox2ID.Items.Clear();
            //ListBox1Col.Items.Clear();
            //ListBox2Col.Items.Clear();
            ListKeys.Items.Clear();

            DrEstado.Items.Clear();
            DrFlujos.Items.Clear();
            Dratras.Items.Clear();
            Drsiguiente.Items.Clear();
            Dralternativo.Items.Clear();
            Drfinal.Items.Clear();

            Dratras.DataValueField = "ZID";
            Dratras.DataTextField = "ZDESCRIPCION";

            Drsiguiente.DataValueField = "ZID";
            Drsiguiente.DataTextField = "ZDESCRIPCION";

            Dralternativo.DataValueField = "ZID";
            Dralternativo.DataTextField = "ZDESCRIPCION";

            Drfinal.DataValueField = "ZID";
            Drfinal.DataTextField = "ZDESCRIPCION";

            DrFlujos.DataValueField = "ZID";
            DrFlujos.DataTextField = "ZDESCRIPCION";

            DrEstado.DataValueField = "ZID";
            DrEstado.DataTextField = "ZNOMBRE";

            DrFlujos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Dratras.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drsiguiente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Dralternativo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drfinal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            DrEstado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrPagina.Items.Clear();
            DrPagina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            string SQL = " SELECT ZID, ZDESCRIPCION FROM  ZCONFIGRUTASFILES ";
            DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
            DrPagina.DataValueField = "ZID";
            DrPagina.DataTextField = "ZDESCRIPCION";
            DrPagina.DataSource = dt1;
            DrPagina.DataBind();

            //SMKey.InnerText = "";

            DataTable dtFlujos = this.Session["Flujos"] as DataTable;
            DataTable dtFlujosEstados = this.Session["FlujosEstados"] as DataTable;
            DataTable dtEstadosFlujos = Main.CargaEstadosFl(0).Tables[0]; 

            DrFlujos.DataSource = dtFlujos;
            DrFlujos.DataBind();

            DrEstado.DataSource = dtEstadosFlujos;
            DrEstado.DataBind();

            Dratras.DataSource = dtEstadosFlujos;
            Dratras.DataBind();

            Drsiguiente.DataSource = dtEstadosFlujos;
            Drsiguiente.DataBind();

            Dralternativo.DataSource = dtEstadosFlujos;
            Dralternativo.DataBind();

            Drfinal.DataSource = dtEstadosFlujos;
            Drfinal.DataBind();



#pragma warning disable CS0219 // La variable 'visto' está asignada pero su valor nunca se usa
            int visto = 0;
#pragma warning restore CS0219 // La variable 'visto' está asignada pero su valor nunca se usa

            DataTable dtt = new DataTable("Tabla");
            DataTable dto = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZDESCRIPCION");

            dto.Columns.Add("ZID");
            dto.Columns.Add("ZDESCRIPCION");

            DataRow drr;

#pragma warning disable CS0219 // La variable 'i' está asignada pero su valor nunca se usa
            int i = 0;
#pragma warning restore CS0219 // La variable 'i' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'Key' está asignada pero su valor nunca se usa
            string Key = "";
#pragma warning restore CS0219 // La variable 'Key' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'x' está asignada pero su valor nunca se usa
            string x = "";
#pragma warning restore CS0219 // La variable 'x' está asignada pero su valor nunca se usa

            //Si no hay Flujos
            if (dtFlujos.Rows.Count == 0 || sortExpression == "")
            {
                //Si hay Estados asociados a es Flujo
                foreach (DataRow fila in dtEstadosFlujos.Rows)//Campos
                {
                    drr = dtt.NewRow();
                    drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                    drr[1] = fila["ZDESCRIPCION"].ToString();
                    dtt.Rows.Add(drr);

                    ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
                drr = dto.NewRow();
                drr[0] = Convert.ToInt32(0);
                drr[1] = "";
                dto.Rows.Add(drr);
                //ListBox1.DataSource = dtt;
                //ListBox1ID.DataBind();
            }
            else
            {
                //si tiene Relacion archivos Flujos Estados
                foreach (DataRow dr in dtEstadosFlujos.Rows)
                {
                    //Key = dr["ZKEY"].ToString();
                    visto = 0;
                    foreach (DataRow fila in dtFlujosEstados.Rows)//Esdos de Flujo
                    {
                        if (fila["ZID_ESTADO"].ToString() == dr["ZID"].ToString())
                        {
                            drr = dto.NewRow();
                            drr[0] = Convert.ToInt32(fila["ZID_ESTADO"].ToString());
                            drr[1] = fila["ZDESCRIPCION"].ToString();
                            dto.Rows.Add(drr);

                            ListBox2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                            ListBox2ID.Items.Add(new ListasID(fila["ZID_ESTADO"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());                        
                            visto = 1;
                            break;
                        }
                    }
                }
            }

            //i = 0;
            //foreach (DataRow fila in dtEstadosFlujos.Rows)//Campos
            //{
            //    visto = 0;
            //    foreach (DataRow filaVisto in dto.Rows)//Campos
            //    {
            //        if (fila["ZID"].ToString() == filaVisto["ZID"].ToString())
            //        {
            //            visto = 1;
            //            break;
            //        }
            //        else
            //        {
            //            visto = 0;
            //        }
            //    }

            //    if (visto == 0)
            //    {
            //        i += 1;
            //        ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //        ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //    }
            //}
            //LbCampoCount.Text = i.ToString();

            //if (x != "") { SMKey.InnerText = ". Keys de este Archivo: (" + x + ")"; }

            //if (i != 0)
            //{
            //    foreach (DataRow fila in dto.Rows)//Campos
            //    {
            //        if(fila["ZDESCRIPCION"].ToString() == "")
            //        {
            //            //LbUtilizados.Text = "0";
            //        }
            //        else
            //        {
            //            //LbUtilizados.Text = dto.Rows.Count.ToString();
            //            DrCampoasig.DataSource = dto;
            //            DrCampoasig.DataBind();
            //            this.Session["SelArchivoCampo"] = dto;
            //            if (Key == "0" || Key == null)
            //            { }
            //            else
            //            {
            //                for (int a = 0; a <= DrCampoasig.Items.Count - 1; a++)
            //                {
            //                    if (DrCampoasig.Items[a].Value == Key)
            //                    {
            //                        DrCampoasig.SelectedIndex = a;
            //                        DrCampoasig.BackColor = Color.FromName("#bdecb6");
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        break;
            //    }
            //}
        }


        private void EditaProfile(int Abierto)
        {
            if (Abierto == 0)
            {
                TxtIdProfiles.Enabled = false;
                DrArchivoProfile.Enabled = false;
                DrFlujoProfile.Enabled = false;
                DrEstadoProfile.Enabled = false;
                DrPaginaProfile.Enabled = false;
                DrProcedimientoProfile.Enabled = false;
                DrcampoFiltro.Enabled = false;
                drCampofiltroseleccion.Enabled = false;
                ListBox3.Enabled = false;
                TxtDocsProfile.Enabled = false;
                TxtDirProfile.Enabled = false;
                BteditProfile.Visible = true;
                BtGuardaProfile.Visible = false;
                BtCancelaProfile.Visible = false;
                //BtCerrarProfile.Enabled = false;
                TxtIdProfiles.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrArchivoProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrFlujoProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrEstadoProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrPaginaProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrProcedimientoProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrcampoFiltro.BackColor = System.Drawing.Color.FromName("#ffffff");
                drCampofiltroseleccion.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox3.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDocsProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDirProfile.BackColor = System.Drawing.Color.FromName("#ffffff");
            }
            else
            {
                TxtIdProfiles.Enabled = true;
                DrArchivoProfile.Enabled = true;
                DrFlujoProfile.Enabled = true;
                DrEstadoProfile.Enabled = true;
                DrPaginaProfile.Enabled = true;
                DrcampoFiltro.Enabled = true;
                DrProcedimientoProfile.Enabled = true;
                drCampofiltroseleccion.Enabled = true;
                ListBox3.Enabled = true;
                TxtDocsProfile.Enabled = true;
                TxtDirProfile.Enabled = true;
                BteditProfile.Visible = false;
                BtGuardaProfile.Visible = true;
                BtCancelaProfile.Visible = true;
                //BtCerrarProfile.Enabled = true;

                TxtIdProfiles.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrArchivoProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrFlujoProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrEstadoProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPaginaProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrProcedimientoProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrcampoFiltro.BackColor = System.Drawing.Color.FromName("#bdecb6");
                drCampofiltroseleccion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox3.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDocsProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDirProfile.BackColor = System.Drawing.Color.FromName("#bdecb6");
            }
        }

        private void EditaTemplate(int Abierto)
        {
            if (Abierto == 0)
            {
                //Hay que cambiar controles
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                DrConexion.Enabled = false;
                ListBoxArchivo.Enabled = false;
                BtguardaFlujo.Enabled = false;
                BtCancelFlujo.Enabled = false;
                BtGuardaRel.Enabled = false;
                BtCancelRel.Enabled = false;
                TxtNombre.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDescripcion.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrConexion.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrPagina.Enabled = false;
                DrPagina.BackColor = System.Drawing.Color.FromName("#ffffff");

                ListBox1.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#ffffff");

                Button1.Enabled = false;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
            }
            else
            {
                TxtNombre.Enabled = true;
                TxtDescripcion.Enabled = true;
                DrConexion.Enabled = true;
                ListBoxArchivo.Enabled = true;
                BtguardaFlujo.Enabled = true;
                BtCancelFlujo.Enabled = true;
                BtGuardaRel.Enabled = true;
                BtCancelRel.Enabled = true;
                TxtNombre.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrConexion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPagina.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPagina.Enabled = true;

                ListBox1.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#bdecb6");

                Button1.Enabled = true;
                Button2.Enabled = true;
                Button4.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
            }
        }
        private void EditaFlujo(int Abierto)
        {
            if(Abierto == 0)
            {
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                DrConexion.Enabled = false;
                ListBoxArchivo.Enabled = false;
                BtguardaFlujo.Enabled = false;
                BtCancelFlujo.Enabled = false;
                BtGuardaRel.Enabled = false;
                BtCancelRel.Enabled = false;
                TxtNombre.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtDescripcion.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrConexion.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#ffffff");
                DrPagina.Enabled = false;
                DrPagina.BackColor = System.Drawing.Color.FromName("#ffffff");

                ListBox1.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#ffffff");

                Button1.Enabled = false;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
            }
            else
            {
                TxtNombre.Enabled = true;
                TxtDescripcion.Enabled = true;
                DrConexion.Enabled = true;
                ListBoxArchivo.Enabled = true;
                BtguardaFlujo.Enabled = true;
                BtCancelFlujo.Enabled = true;
                BtGuardaRel.Enabled = true;
                BtCancelRel.Enabled = true;
                TxtNombre.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrConexion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPagina.BackColor = System.Drawing.Color.FromName("#bdecb6");
                DrPagina.Enabled = true;

                ListBox1.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2.BackColor = System.Drawing.Color.FromName("#bdecb6");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#bdecb6");

                Button1.Enabled = true;
                Button2.Enabled = true;
                Button4.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
            }
        }

        private void LimpioFlujo()
        {
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            //DrConexion.Text = "";
            ListBoxArchivo.Items.Clear();
            ListBoxArchivoID.Items.Clear();
        }

        private void EditaEstado(int Abierto)
        {
            if (Abierto == 0)
            {
                TxtidEstado.Enabled = false;
                TxtEstado.Enabled = false;
                Dratras.Enabled = false;
                Drsiguiente.Enabled = false;
                Dralternativo.Enabled = false;
                Drfinal.Enabled = false;
                TxtCondicion.Enabled = false;
                BtGuardaEstado.Enabled = false;
                BtCancelEstado.Enabled = false;
                TxtidEstado.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtEstado.BackColor = System.Drawing.Color.FromName("#ffffff");
                Dratras.BackColor = System.Drawing.Color.FromName("#ffffff");
                Drsiguiente.BackColor = System.Drawing.Color.FromName("#ffffff");
                Dralternativo.BackColor = System.Drawing.Color.FromName("#ffffff");
                Drfinal.BackColor = System.Drawing.Color.FromName("#ffffff");
                TxtCondicion.BackColor = System.Drawing.Color.FromName("#ffffff");
                Button3.Enabled = false;
                Button8.Visible = false;
            }
            else
            {
                TxtidEstado.Enabled = true;
                TxtEstado.Enabled = true;
                Dratras.Enabled = true;
                Drsiguiente.Enabled = true;
                Dralternativo.Enabled = true;
                Drfinal.Enabled = true;
                TxtCondicion.Enabled = true;
                BtGuardaEstado.Enabled = true;
                BtCancelEstado.Enabled = true;
                TxtidEstado.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtEstado.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Dratras.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Drsiguiente.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Dralternativo.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Drfinal.BackColor = System.Drawing.Color.FromName("#bdecb6");
                TxtCondicion.BackColor = System.Drawing.Color.FromName("#bdecb6");
                Button3.Enabled = true;
                Button8.Visible = true;
            }
        }
        protected void btncierraSQLProfile_Click(object sender, EventArgs e)
        {
            DivProfiles.Visible = true;
            DivSQLProfile.Visible = false;
        }

        

        protected void btncierraSQL_Click(object sender, EventArgs e)
        {
            DivCampoDer.Visible = true;
            DivSQL.Visible = false;   
        }

        private void LimpioEstado()
        {
            TxtidEstado.Text = "";
            TxtEstado.Text = "";
            Dratras.SelectedIndex = -1;
            Drsiguiente.SelectedIndex = -1;
            Dralternativo.SelectedIndex = -1;
            Drfinal.SelectedIndex = -1;
            TxtCondicion.Text = "";
        }
        protected void btnGuardarRelacion_Click(object sender, EventArgs e)
        {
            //Guardar Relación


            EditaFlujo(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }

        protected void btnCancelarRelacion_Click(object sender, EventArgs e)
        {

            EditaFlujo(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }

        protected void btnCancelarProfile_Click(object sender, EventArgs e)
        {
            EditaProfile(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }
        protected void btnCancelarFlujo_Click(object sender, EventArgs e)
        {
            EditaFlujo(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }
        protected void btnCancelarEstado_Click(object sender, EventArgs e)
        {
             Cancelado();
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }

        protected void btnGuardarProfile_Click(object sender, EventArgs e)
        {

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "5")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea ProfileReplace("&#39;", "'");
                    string Column = "INSERT INTO ZPROFILES (ZID_ARCHIVO, ZID_FLUJO, ZID_ESTADO, ZID_PAGINA, ZQUERY, ZID_PROCEDIMIENTO, ZCAMPODOC, ZCAMPOFILTRO, ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS) ";
                    string ColumnVal = " VALUES(" + this.Session["idarchivo"].ToString() + "," + DrFlujoProfile.SelectedValue + "," + DrEstadoProfile.SelectedValue + ",";
                    ColumnVal +=  DrPagina.SelectedValue + ",'" + TxtQueryProfile.Text.Replace("'", "&#39;") + "'," + DrProcedimientoProfile.SelectedValue + ",'" + drCampofiltroseleccion.SelectedItem.Text + "',";
                    ColumnVal += "'" + DrcampoFiltro.SelectedItem.Text + "','";
                    for (int i = 0; i <= ListBox3.Items.Count - 1; i++)
                    {
                        ColumnVal += ListBox3.Items[i].Value + ";" ;
                    }
                    ColumnVal += "'" + TxtDocsProfile.Text + "','" + TxtDirProfile.Text + "')";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else
                {
                    //Edita Profile
                    string Column = "UPDATE ZPROFILES SET ZID_ARCHIVO = '" + this.Session["idarchivo"].ToString() + "', ";
                    Column += " ZID_FLUJO = " + DrFlujoProfile.SelectedValue + ", ";
                    Column += " ZID_ESTADO = " + DrEstadoProfile.SelectedValue + ", ";
                    Column += " ZID_PAGINA = " + DrPagina.SelectedValue + ", ";
                    Column += " ZQUERY = '" + TxtQueryProfile.Text.Replace("'", "&#39;") + "', ";
                    Column += " ZID_PROCEDIMIENTO = " + DrProcedimientoProfile.SelectedValue + ", ";
                    Column += " ZCAMPODOC = '" + drCampofiltroseleccion.SelectedItem.Text + "', ";
                    Column += " ZCAMPOFILTRO = '" + DrcampoFiltro.SelectedItem.Text + "',";
                    string X = "";
                    for (int i = 0; i <= ListBox3.Items.Count - 1; i++)
                    {
                        X += ListBox3.Items[i].Value + ";";
                    }
                    Column += " ZFILTROCONDICION = '" + X + "', ";
                    Column += " ZDOCUMENTOS = '" + TxtDocsProfile.Text + "', ";
                    Column += " ZDIRECTORIOS = '" + TxtDirProfile.Text + "' ";
                    Column += " WHERE ZID =" + TxtIdProfiles.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaProfile(0);
            Actualiza_Archivos();

        }
        protected void btnGuardarFlujo_Click(object sender, EventArgs e)
        {
            IbtAllFlujo.ImageUrl = "~/Images/allwhite.png";
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "2")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea Flujo
                    string Column = "INSERT INTO ZFLUJOS (ZDESCRIPCION, ZID_ARCHIVO, ZID_REGISTRO, ZID_ESTADOS, ZID_PAGINA) ";
                    string ColumnVal = " VALUES('" + TxtDescripcion.Text + "'," + this.Session["idarchivo"].ToString() + ",0,0," + DrPagina.SelectedValue  + ")";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZFLUJOS", dbParams));
                    TxtNombre.Text = (MiID).ToString();

                    Column = "DELETE FROM ZARCHIVOFLUJOS WHERE  ZID_FLUJO = " + TxtNombre.Text + " ";
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int a = ListBoxArchivo.Items.Count;

                    for (int i = 0; i <= ListBoxArchivo.Items.Count - 1; i++)
                    {
                        Column = "INSERT INTO ZARCHIVOFLUJOS (ZID_ARCHIVO, ZID_FLUJO, ZNIVEL) ";
                        ColumnVal = " VALUES(" + ListBoxArchivoID.Items[i].Value + "," + TxtNombre.Text + ",0)";
                        Column += ColumnVal;
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }

                    Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_FLUJO = " + TxtNombre.Text;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    //Vincular Estados relacionados
                    for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
                    {
                        Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ";
                        Column += " ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND) ";
                        ColumnVal = " VALUES(" + TxtNombre.Text + "," + ListBox2ID.Items[i].Value + ",";

                        if (Imgatras.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (ImgNext.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (ImgAlter.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (imgFin.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (Dratras.SelectedItem.Value != "0") 
                        {
                            ColumnVal += Dratras.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drsiguiente.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drsiguiente.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drsiguiente.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drsiguiente.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drfinal.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drfinal.SelectedItem.Value + " )";
                        }
                        else
                        {
                            ColumnVal += " 0)";
                        }

                        Column += ColumnVal;
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }
                }
                else
                {
                    //Edita flujo
                    string Column = "UPDATE ZFLUJOS SET ZDESCRIPCION = '" + TxtDescripcion.Text + "', ";
                    Column += " ZID_ARCHIVO = " + this.Session["idarchivo"].ToString() + ", ";
                    Column += " ZID_REGISTRO = 0, ZID_ESTADOS = 0, ";
                    Column += " ZID_PAGINA = " + DrPagina.SelectedValue + " ";
                    Column += " WHERE ZID =" + TxtNombre.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    Column = "DELETE FROM ZARCHIVOFLUJOS WHERE  ZID_FLUJO = " + TxtNombre.Text + " ";
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int a = ListBoxArchivoID.Items.Count;

                    for (int i = 0; i <= ListBoxArchivo.Items.Count - 1; i++)
                    {
                        Column = "INSERT INTO ZARCHIVOFLUJOS (ZID_ARCHIVO, ZID_FLUJO, ZNIVEL) ";
                        string ColumnVal = " VALUES(" + ListBoxArchivoID.Items[i].Value + "," + TxtNombre.Text + ",0)";
                        Column += ColumnVal;
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }

                    Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_FLUJO = " + TxtNombre.Text;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    //Vincular Estados relacionados
                    for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
                    {
                        Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ";
                        Column += " ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND) ";
                        string ColumnVal = " VALUES(" + TxtNombre.Text + "," + ListBox2ID.Items[i].Value + ",";

                        if (Imgatras.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (ImgNext.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (ImgAlter.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (imgFin.Visible == true)
                        {
                            ColumnVal += " 1,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }

                        if (Dratras.SelectedItem.Value != "0")
                        {
                            ColumnVal += Dratras.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drsiguiente.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drsiguiente.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drsiguiente.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drsiguiente.SelectedItem.Value + " ,";
                        }
                        else
                        {
                            ColumnVal += " 0,";
                        }
                        if (Drfinal.SelectedItem.Value != "0")
                        {
                            ColumnVal += Drfinal.SelectedItem.Value + " )";
                        }
                        else
                        {
                            ColumnVal += " 0)";
                        }

                        Column += ColumnVal;
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }

                }
            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaFlujo(0);
            Actualiza_Archivos();

        }
        protected void Imgatras_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            if(Img.ID == "Imgatras")
            {
                Imgatras.Visible = false;
                ImgatrasC.Visible = true;
            }
            else
            {
                Imgatras.Visible = true;
                ImgatrasC.Visible = false;
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }

        protected void ImgNext_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            if (Img.ID == "ImgNext")
            {
                ImgNext.Visible = false;
                ImgNextC.Visible = true;
            }
            else
            {
                ImgNext.Visible = true;
                ImgNextC.Visible = false;
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }

        protected void ImgAlter_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            if (Img.ID == "ImgAlter")
            {
                ImgAlter.Visible = false;
                ImgAlterC.Visible = true;
            }
            else
            {
                ImgAlter.Visible = true;
                ImgAlterC.Visible = false;
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }

        protected void imgFin_Click(object sender, EventArgs e)
        {
            ImageButton Img = (ImageButton)sender;
            if (Img.ID == "imgFin")
            {
                imgFin.Visible = false;
                imgFinC.Visible = true;
            }
            else
            {
                imgFin.Visible = true;
                imgFinC.Visible = false;
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }

        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            IbtAllEstado.ImageUrl = "~/Images/allwhite.png";

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "3" || this.Session["Edicion"].ToString() == "4")
            {
                if (this.Session["Edicion"].ToString() == "3")
                {
                    //Crea Estado de Flujo 
                    string Column = "INSERT INTO ZESTADOSFLUJO (ZNOMBRE, ZORDEN, ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND, ZIMG) ";
                    string ColumnVal = " VALUES('" + TxtEstado.Text + "',0," + Dratras.SelectedItem.Value + "," + Drsiguiente.SelectedItem.Value + "," + Dralternativo.SelectedItem.Value + "," + Drfinal.SelectedItem.Value + ",'" + TxtCondicion.Text + "')";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZESTADOSFLUJO", dbParams));
                    TxtidEstado.Text = (MiID).ToString();

                    string flujo = "0";
                    if(DrFlujos.SelectedItem.Value != "0")
                    {
                        flujo = DrFlujos.SelectedItem.Value;
                    }
                    string archivo = "0";
                    if (DrArchivos.SelectedItem.Value != "0")
                    {
                        archivo = DrArchivos.SelectedItem.Value;
                    }

                    Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_ESTADO = " + TxtidEstado.Text + " AND ZID_ARCHIVO = " + archivo + " ";
                    Column += " AND ZID_FLUJO = " + flujo + " ";
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZID_ARCHIVO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ZCONDICION) ";
                    ColumnVal = " VALUES(" + flujo + "," + TxtidEstado.Text + "," + archivo + ",";

                    if (Imgatras.Visible == true)
                    {
                        ColumnVal += " 1, ";
                    }
                    else
                    {
                        ColumnVal += " 0, ";
                    }

                    if (ImgNext.Visible == true)
                    {
                        ColumnVal += " 1, ";
                    }
                    else
                    {
                        ColumnVal += " 0, ";
                    }

                    if (ImgAlter.Visible == true)
                    {
                        ColumnVal += " 1, ";
                    }
                    else
                    {
                        ColumnVal += " 0, ";
                    }

                    if (imgFin.Visible == true)
                    {
                        ColumnVal += " 1, ";
                    }
                    else
                    {
                        ColumnVal += " 0, ";
                    }


                    ColumnVal += "'" + CommentSQL.Text.Replace("'", "&#39;") + "')";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else
                {
                    //Edita flujo
                    string flujo = "0";
                    if (DrFlujos.SelectedItem.Value != "0")
                    {
                        flujo = DrFlujos.SelectedItem.Value;
                    }
                    string archivo = "0";
                    if (DrArchivos.SelectedItem.Value != "0")
                    {
                        archivo = DrArchivos.SelectedItem.Value;
                    }

                    string Column = "UPDATE ZESTADOSFLUJO SET ZNOMBRE = '" + TxtDescripcion.Text + "', ";
                    
                    Column += " ZPREVIUS = " + Dratras.SelectedItem.Value + ", ";
                    Column += " ZNEXT =" + Drsiguiente.SelectedItem.Value + ", ";
                    Column += " ZALTERNATIVE =" + Dralternativo.SelectedItem.Value + ", ";
                    Column += " ZEND =" + Drfinal.SelectedItem.Value + ", ";
                    Column += " ZORDEN = 0, ";
                    Column += " ZIMG = '" + TxtCondicion.Text + "' ";
                    Column += " WHERE ZID =" + TxtidEstado.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    Column = " UPDATE ZFLUJOSESTADOS SET ";

                    if (Imgatras.Visible == true)
                    {
                        Column += " ZPREVIUSVIEW = 1, ";
                    }
                    else
                    {
                        Column += " ZPREVIUSVIEW = 0, ";
                    }

                    if (ImgNext.Visible == true)
                    {
                        Column += " ZNEXTVIEW = 1, ";
                    }
                    else
                    {
                        Column += " ZNEXTVIEW = 0, ";
                    }

                    if (ImgAlter.Visible == true)
                    {
                        Column += " ZALTERNATIVEVIEW = 1, ";
                    }
                    else
                    {
                        Column += " ZALTERNATIVEVIEW = 0, ";
                    }

                    if (imgFin.Visible == true)
                    {
                        Column += " ZENDVIEW = 1, ";
                    }
                    else
                    {
                        Column += " ZENDVIEW = 0, ";
                    }

                    Column += " ZCONDICION = '" + CommentSQL.Text.Replace("'", "&#39;") + "' ";
                    Column += " WHERE ZID_ESTADO = " + TxtidEstado.Text + " ";
                    Column += " AND ZID_ARCHIVO = " + archivo + " ";
                    Column += " AND ZID_FLUJO = " + flujo + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                }

            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaEstado(0);
            Actualiza_Archivos();
        }
        
        protected void DrTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCopiaFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0") { return; }
            //LimpioFlujo();
            EditaFlujo(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "flujo";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZFLUJOS", dbParams));
            TxtNombre.Text = (MiID + 1).ToString();
            TxtDescripcion.Text = "Copia de " + TxtDescripcion.Text;
        }
        
        protected void btnCreaFlujo_Click(object sender, EventArgs e)
        {
            if(this.Session["Edicion"].ToString() != "0") { return; }
            LimpioFlujo();
            EditaFlujo(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "flujo";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZFLUJOS", dbParams));
            TxtNombre.Text = (MiID + 1).ToString(); 
        }
        protected void btnEditFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaFlujo(1);
            this.Session["Edicion"] = "2";
            this.Session["proceso"] = "flujo";
        }
        protected void btnDeleteFlujo_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4") 
            {
                string Column = "DELETE FROM ZFLUJOS WHERE  ZID = " + TxtNombre.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                Column = "DELETE FROM ZFLUJOSESTADOS WHERE  ZID_FLUJO = " + TxtNombre.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpioFlujo();
                EditaFlujo(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                Actualiza_Archivos();
            }
        }
        protected void btnAllFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            DrFlujos.Items.Clear();
            DrFlujos.DataValueField = "ZID";
            DrFlujos.DataTextField = "ZDESCRIPCION";

            DataTable dt = Main.CargaFlujos("0", "0").Tables[0];

            DrFlujos.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DrFlujos.DataSource = dt;
            DrFlujos.DataBind();
            IbtAllFlujo.ImageUrl = "~/Images/allgreen.png";

        }

        protected void btnCopiaEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //LimpioEstado();
            EditaEstado(1);
            this.Session["Edicion"] = "3";
            this.Session["proceso"] = "estado";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZESTADOSFLUJO", dbParams));
            TxtidEstado.Text = (MiID + 1).ToString();
            TxtEstado.Text = "Copia de " + TxtEstado.Text;
        }
        
        protected void btnCreaEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            LimpioEstado();
            EditaEstado(1);
            this.Session["Edicion"] = "3";
            this.Session["proceso"] = "estado";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZESTADOSFLUJO", dbParams));
            TxtidEstado.Text = (MiID + 1).ToString();
        }
        protected void btnEditEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaEstado(1);
            this.Session["Edicion"] = "4";
            this.Session["proceso"] = "estado";
        }

        protected void btnEditaProfile_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaProfile(1);
            this.Session["Edicion"] = "5";
            this.Session["proceso"] = "profile";
        }

        
        protected void btnDeleteEstado_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4")
            {
                string Column = "DELETE FROM ZESTADOSFLUJO WHERE  ZID = " + TxtidEstado.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                Column = "DELETE FROM ZFLUJOSESTADOS WHERE  ZID_ESTADO = " + TxtidEstado.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpiaEstados();
                EditaEstado(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                Actualiza_Archivos();
            }
        }
        protected void btnAllEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            DrEstado.Items.Clear();
            DrEstado.DataValueField = "ZID";
            DrEstado.DataTextField = "ZDESCRIPCION";

            //DataTable dt = Main.CargaFlujosEstados(0).Tables[0];
            DataTable dt = Main.CargaEstadosFl(0).Tables[0];
            DrEstado.DataSource = dt;
            DrEstado.DataBind();
            IbtAllEstado.ImageUrl = "~/Images/allgreen.png";
        }


        protected void DrPeso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            int MiID = 0;
            //MiID =  Convert.ToInt32(cmd.ExecuteScalar());
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                this.Session["Edicion"] = 1;
                this.Session["proceso"] = "archivo";
                //maximo id
                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZARCHIVOS", dbParams));
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //this.Session["Edicion"] = 1;
                ////maximo id
                //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZARCHIVOS", dbParams));
            }
            MiID = MiID + 1;
            LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + MiID;
            DrEstado.Items.Clear();
            TxtDescripcion.Text = "";
            TxtNombre.Text = MiID.ToString();
            //TablaName.Text = "";
            ////TextDuplicado.Text = "";
            //TablaObj.Text = "";
            //dlNivel.SelectedIndex = 9;
            //Djerarquia.SelectedIndex = 1;
            //Dtipo.SelectedIndex = 2;
            //dlEstado.SelectedIndex = 2;

            //DrDuplicado.SelectedIndex = 0;
            //DrConexion.SelectedIndex = 0;
            //dlNivel.SelectedIndex = 9;
            //Djerarquia.SelectedIndex = -1;
            //dlEstado.SelectedIndex = 0;
            //Dtipo.SelectedIndex = 2;
            //TxtMail.Text = "";
            this.Session["IDArchivo"] = MiID;

            foreach (System.Web.UI.Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = true;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
            Relaciones(0, CampoOrden);
            ActivarTxt();
            //btnEditar.CssClass = "myButtonOver";
            //btnNuevo.CssClass = "myButtonOver";
            //btnGuardar.CssClass = "myButtonOff";
            //btnCancelar.CssClass = "myButtonOff";
            //btnEditar.Visible = false;
            //btnNuevo.Visible = false;
            //btnGuardar.Visible = true;
            //btnCancelar.Visible = true;
            DrArchivos.Enabled = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = 2;
            this.Session["proceso"] = "archivo";
            foreach (System.Web.UI.Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = true;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
            ActivarTxt();
            //btnEditar.CssClass = "myButtonOver";
            //btnNuevo.CssClass = "myButtonOver";
            //btnGuardar.CssClass = "myButtonOff";
            //btnCancelar.CssClass = "myButtonOff";
            //btnEditar.Visible = false;
            //btnNuevo.Visible = false;
            //btnGuardar.Visible = true;
            //btnCancelar.Visible = true;
            DrArchivos.Enabled = false;

        }

        protected void DrCompetencia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrObligatoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrClase_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDeleteTabla_Click(object sender, EventArgs e)
        {
            //MArca como borrado el registro de ZARCHIVOS como borrado
            if(this.Session["IDArchivo"].ToString() != "")
            {

                Lbmensaje.Text = "Se eliminará el Archivo Documental " + DrArchivos.SelectedItem.Text + " de la base de datos.¿Desea Continuar?";
                windowmessaje.Visible = true;
                cuestion.Visible = true;
                Asume.Visible = false;
                Page.MaintainScrollPositionOnPostBack = false;
                MiCloseMenu();

            }
        }
        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Eliminamos todos los registros del listbox2
            for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
            {
                ListBox1.Items.Add(ListBox2.Items[i]);
                ListBox1ID.Items.Add(ListBox2ID.Items[i]);
            }
            ListBox2.Items.Clear();
            ListBox2ID.Items.Clear();
            ListKeys.Items.Clear();
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }
        }

        protected void btnEliminarSeleccionados_Click(object sender, EventArgs e)
        {
            //Eliminamos los registros seleccionados

            while (ListBox2.GetSelectedIndices().Length > 0)
            {
                //ListBox2.Items.Remove(ListBox2.SelectedItem);
                ListBox1.Items.Add(ListBox2.SelectedItem);
                ListBox1ID.Items.Add(ListBox2ID.SelectedItem);
                ListBox2ID.Items.RemoveAt(ListBox2.SelectedIndex);
                ListKeys.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2.Items.Remove(ListBox2.SelectedItem);

            }
        }

        protected void btnPasarSeleccionados_Click(object sender, EventArgs e)
        {
            //Pasamos los items seleccionados de listbox2 a listbox1
            //for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            //{
            //    ListBox1.Items.Add(ListBox2.SelectedItem);
            //    ListBox1ID.Items.Add(ListBox2ID.SelectedItem);
            //}
            //ListBox2.Items.Clear();
            //ListBox2ID.Items.Clear();
            try
            {
                while (ListBox1.GetSelectedIndices().Length > 0)
                {
                    ListBox2.Items.Add(ListBox1.SelectedItem);
                    ListBox2ID.Items.Add(ListBox1ID.SelectedItem);
                    //ListBox2Col.Items.Add(ListBox1Col.SelectedItem);
                    ListKeys.Items.Add("0");
                    ListBox1ID.Items.RemoveAt(ListBox1.SelectedIndex);
                    //ListBox1Col.Items.RemoveAt(ListBox1.SelectedIndex);
                    ListBox1.Items.Remove(ListBox1.SelectedItem);
                }
                if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
                {
                }
                else
                {
                    EditaFlujo(1);
                }
            }
            catch
            {

            }


        }

        protected void btnRegresarSeleccionados_Click(object sender, EventArgs e)
        {
            //Regresamos los items seleccionados de listbox2 a listbox1

            while (ListBox2.GetSelectedIndices().Length > 0)
            {
                ListBox1.Items.Add(ListBox2.SelectedItem);
                ListBox1ID.Items.Add(ListBox2ID.SelectedItem);
                //ListBox1Col.Items.Add(ListBox2Col.SelectedItem);
                ListBox2ID.Items.RemoveAt(ListBox2.SelectedIndex);
                ListKeys.Items.RemoveAt(ListBox2.SelectedIndex);
                //ListBox2Col.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2.Items.Remove(ListBox2.SelectedItem);
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en listbox1

            for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            {
                if (ListBox1.Items[i].Text.Contains(TextBox1.Text))
                {
                    string a = ListBox1.Items[i].Text;
                    a = ListBox1.Items[i].Value;
                    ListBox1.Items[i].Selected = true;
                    ListBox1ID.Items[i].Selected = true;
                    //ListBox1Col.Items[i].Selected = true;
                    break;
                }
            }
            for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            {
                if (ListBox2.Items[i].Text.Contains(TextBox1.Text))
                {
                    string a = ListBox2.Items[i].Text;
                    a = ListBox2.Items[i].Value;
                    ListBox2.Items[i].Selected = true;
                    ListBox2ID.Items[i].Selected = true;
                    //ListBox2Col.Items[i].Selected = true;
                    break;
                }
            }

            //    if (ListBox1.Items.FindByText(TextBox1.Text) != null)
            //{
            //    ListBox1.Items.FindByText(TextBox1.Text).Selected = true;
            //}
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }
        }

        public void DesactivarTxt()
        {

            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //DrDuplicado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //dlEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //ListBox1Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //ListBox2Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //TablaName.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //TablaObj.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //Djerarquia.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //Dtipo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ////TextDocElec.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ////TextRuta.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //Textunidad.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //TextHardDisc.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //TextRegistro.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtNombre.Enabled = false;
            TxtDescripcion.Enabled = false;
            //DrDuplicado.Enabled = false;
            //dlNivel.Enabled = false;
            //dlEstado.Enabled = false;
            ////ListBox1.Enabled = false;
            ////ListBox2.Enabled = false;
            ////TablaName.Enabled = false;
            ////TablaObj.Enabled = false;
            //Djerarquia.Enabled = false;
            //Dtipo.Enabled = false;
            ////TextDocElec.Enabled = false;
            ////TextRuta.Enabled = false;
            //Textunidad.Enabled = false;
            //TextHardDisc.Enabled = false;
            //TextRegistro.Enabled = false;
            DrConexion.Enabled = false;
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button4.Enabled = false;
            Button6.Enabled = false;
            Button7.Enabled = false;
        }

        public void ActivarTxt()
        {
            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtNombre.Enabled = true;
            TxtDescripcion.Enabled = true;
            DrConexion.Enabled = true;
            Button1.Enabled = true;
            Button2.Enabled = true;
            Button4.Enabled = true;
            Button6.Enabled = true;
            Button7.Enabled = true;
        }


        private void PopulateRootLevel(int ID)
        {
            //Archivo y sus Campos

            DataTable dt = new DataTable();
            dt = Main.CargaArbolArchivos(ID, Convert.ToInt32((string)Session["MiNivel"]));
            //dt = Main.CargaNodosArchivo(ID, Convert.ToInt32((string)Session["MiNivel"])).Tables[0];
            //PopulateNodes(dt, tvControl.Nodes);

        }


        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            string miro = "";
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Contador += 1;
                    miro = dr["ZID"].ToString();
                    miro = Convert.ToString(ListadoArchivos.GetUpperBound(0));
                    if (Contador > ListadoArchivos.GetUpperBound(0)) { break; }
                    TreeNode tn = new TreeNode();

                    tn.Text = "<font color='#9bf23e'><strong>" + dr["ZDESCRIPCION"].ToString() + "</strong></font>";
                    tn.Value = dr["ZID"].ToString();

                    tn.Expanded = true;
                    nodes.Add(tn);
                    miro = ListadoArchivos[Contador];

                    if (Convert.ToString(registro) == dr["ZID"].ToString())
                    {
                        PopulateSubLevelCampos(Convert.ToInt32(ListadoArchivos[Contador]), tn);
                        break;
                    }
                    else
                    {
                        PopulateSubLevel(Convert.ToInt32(ListadoArchivos[Contador]), tn);
                    }

                    if (ListadoArchivos.GetUpperBound(0) == Contador)
                    {
                        break;
                    }
                }
            }
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }



        private void MisNodes(DataTable dt, TreeNodeCollection nodes)
        {
            string miro = "";

            foreach (DataRow dr in dt.Rows)
            {
                miro = dr["ZID"].ToString();
                TreeNode tn = new TreeNode();
                tn.Text = "<font color='#fffec4'><strong>" + dr["ZDESCRIPCION"].ToString() + "</strong></font>";
                tn.Value = dr["ZID"].ToString();
                tn.Expanded = true;
                nodes.Add(tn);
            }
        }

        private void PopulateSubLevel(int parentid, TreeNode parentNode)
        {
            DataTable dt = new DataTable();

            dt = Main.CargaArbolArchivos(parentid, Convert.ToInt32((string)Session["MiNivel"]));

            PopulateNodes(dt, parentNode.ChildNodes);
        }

        private void PopulateSubLevelCampos(int parentid, TreeNode parentNode)
        {
            DataTable dt = new DataTable();
            dt = Main.TVCargaCampos(parentid, Convert.ToInt32((string)Session["MiNivel"])).Tables[0];


            MisNodes(dt, parentNode.ChildNodes);
        }


        protected void tvControl_SelectedNodeChanged(object sender, EventArgs e)
        {

            //TextBox1.Text = "You Selected " + tvControl.SelectedNode.Text + " category";
            TextBox1.Text = tvControl.SelectedNode.Text;

            DataTable dt = new DataTable();
            dt = Main.TVCargaCampos(Convert.ToInt32(tvControl.SelectedNode.Value), Convert.ToInt32((string)Session["MiNivel"])).Tables[0];


        }

        protected void Subir_Click(object sender, EventArgs e)
        {
            int J = ListBox2.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
#pragma warning disable CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            string sTmpCol = "";
#pragma warning restore CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2.SelectedIndex;
                if (index == 0) { return; }
                sTmp = ListBox2.Items[index - 1].Text;
                sTmpID = ListBox2ID.Items[index - 1].Text;
                //sTmpCol = ListBox2Col.Items[index - 1].Text;
                ListBox2.Items[index - 1].Text = ListBox2.Items[index].Text;
                ListBox2ID.Items[index - 1].Text = ListBox2ID.Items[index].Text;
                //ListBox2Col.Items[index - 1].Text = ListBox2Col.Items[index].Text;
                ListBox2.Items[index].Text = sTmp;
                ListBox2ID.Items[index].Text = sTmpID;
                //ListBox2Col.Items[index].Text = sTmpCol;
                if (index > 0)
                {
                    ListBox2.SelectedIndex = index - 1;
                    ListBox2ID.SelectedIndex = index - 1;
                    //ListBox2Col.SelectedIndex = index - 1;
                }

            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

        }

        protected void Bajar_Click(object sender, EventArgs e)
        {
            int J = ListBox2.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
#pragma warning disable CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            string sTmpCol = "";
#pragma warning restore CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2.SelectedIndex;
                if (index < ListBox2.Items.Count)
                {
                    if(index == ListBox2.Items.Count - 1) { return; }
                    sTmp = ListBox2.Items[index + 1].Text;
                    sTmpID = ListBox2ID.Items[index + 1].Text;
                    //sTmpCol = ListBox2Col.Items[index + 1].Text;
                    ListBox2.Items[index + 1].Text = ListBox2.Items[index].Text;
                    ListBox2ID.Items[index + 1].Text = ListBox2ID.Items[index].Text;
                    //ListBox2Col.Items[index + 1].Text = ListBox2Col.Items[index].Text;
                    ListBox2.Items[index].Text = sTmp;
                    ListBox2ID.Items[index].Text = sTmpID;
                    //ListBox2Col.Items[index].Text = sTmpCol;
                    ListBox2.SelectedIndex = index + 1;
                    ListBox2ID.SelectedIndex = index + 1;
                    //ListBox2Col.SelectedIndex = index + 1;
                }
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }
        }

        protected void TexTosEntradaDumie_Click(object sender, EventArgs e)
        {

        }
        protected void TxtGrupoName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TxtIDtemplate_TextChanged(object sender, EventArgs e)
        {

        }
        
        protected void dlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrUsuarioasig_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {


            foreach (System.Web.UI.Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }

            DesactivarTxt();

            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";
            //btnEditar.Visible = true;
            //btnNuevo.Visible = true;
            //btnGuardar.Visible = false;
            //btnCancelar.Visible = false;
            DivSQL.Visible = false;
            DivCampoDer.Visible = true;
            DrArchivos.Enabled = true;
        }
        protected void Fin_Click(object sender, ImageClickEventArgs e)
        {
            Session.Abandon();
            Server.Transfer("Login.aspx"); //Default
        }

        protected void FinHome_Click(object sender, ImageClickEventArgs e)
        {
            //Session.Abandon();
            Server.Transfer("Site.aspx"); //Default
        }
 
        private void LimpiaEstados()
        {
            TxtidEstado.Text = "";
            TxtEstado.Text = "";
            Dratras.SelectedIndex = 0;
            Imgatras.Visible = true;
            ImgatrasC.Visible = false;
            Drsiguiente.SelectedIndex = 0;
            ImgNext.Visible = true;
            ImgNextC.Visible = false;
            Dralternativo.SelectedIndex = 0;
            ImgAlter.Visible = true;
            ImgAlterC.Visible = false;
            Drfinal.SelectedIndex =0;
            imgFin.Visible = true;
            imgFinC.Visible = false;
            TxtCondicion.Text = "";
        }

        protected void DrEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Identificación de campo clave ZKEY, si está en edición asignarlo y poner en verde
            DrEstado.BackColor = System.Drawing.Color.FromName("#bdecb6");
            LimpiaEstados();

            DataTable dt = Main.CargaEstadosFlujos("0",DrEstado.SelectedItem.Value).Tables[0];
         
            this.Session["idestado"] = DrEstado.SelectedItem.Value;
            this.Session["EstadoCondicion"] = "";

            foreach (DataRow dr in dt.Rows)
            {
                if(dr["ZID"].ToString() == DrEstado.SelectedItem.Value)
                {
                    TxtidEstado.Text = dr["ZID"].ToString();
                    TxtEstado.Text = dr["ZNOMBRE"].ToString();
                    string miro = dr["ZCONDICION"].ToString();
                    

                    if (miro != "")
                    {
                        if (miro.Contains("ZTEMPLATE#") == true)
                        {
                            miro = miro.Replace("ZTEMPLATE#", "");
                            this.Session["EstadoCondicion"] = miro;
                            string[] Fields = System.Text.RegularExpressions.Regex.Split(miro, "-");
                            ListTemplate.Items.Clear();
                            ListTemplateID.Items.Clear();
                            if (Fields.Count() > 0)
                            {
                                foreach (string Field in Fields)
                                {
                                    for (int i = 0; i < DrTemplates.Items.Count; i++)
                                    {
                                        if (DrTemplates.Items[i].Value == Field)
                                        {
                                            ListTemplate.Items.Add(new ListasID(DrTemplates.Items[i].Text, Convert.ToInt32(Field)).ToString());
                                            ListTemplateID.Items.Add(new ListasID(Convert.ToInt32(Field).ToString(), Convert.ToInt32(Field)).ToString());
                                            break;
                                        }
                                    }
                                }
                            }
                            //DivTemplate.Visible = true;
                            //DivTxtQuery.Visible = false;
                        }
                        else
                        {
                            TxtQueryProfile.Text = miro.Replace("&#39;","'");
                            //DivTxtQuery.Visible = true;
                            //DivTemplate.Visible = false;
                        }
                    }

                    miro = dr["ZPREVIUS"].ToString();


                    for (int i = 0; i <= Dratras.Items.Count - 1; i++)
                    {
                        if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
                        {
                            Dratras.SelectedIndex = i;
                            if (dr["ZPREVIUSVIEW"].ToString() == "1") 
                            { 
                                Imgatras.Visible = true;
                                ImgatrasC.Visible = false;
                            } 
                            else 
                            {
                                Imgatras.Visible = false;
                                ImgatrasC.Visible = true; 
                            }
                            break;
                        }
                    }

                    for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
                    {
                        if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
                        {
                            Drsiguiente.SelectedIndex = i;
                            if (dr["ZNEXTVIEW"].ToString() == "1")
                            {
                                ImgNext.Visible = true;
                                ImgNextC.Visible = false;
                            }
                            else
                            {
                                ImgNext.Visible = false;
                                ImgNextC.Visible = true;
                            }
                            break;
                        }
                    }

                    for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
                    {
                        if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
                        {
                            Dralternativo.SelectedIndex = i;
                            if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
                            {
                                ImgAlter.Visible = true;
                                ImgAlterC.Visible = false;
                            }
                            else
                            {
                                ImgAlter.Visible = false;
                                ImgAlterC.Visible = true;
                            }
                            break;
                        }
                    }

                    for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
                    {
                        if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
                        {
                            Drfinal.SelectedIndex = i;
                            if (dr["ZENDVIEW"].ToString() == "1")
                            {
                                imgFin.Visible = true;
                                imgFinC.Visible = false;
                            }
                            else
                            {
                                imgFin.Visible = false;
                                imgFinC.Visible = true;
                            }
                            break;
                        }
                    }

                    break;
                }
            }

        }
        
        //protected void btnDeleteArchivoTemplate_Click(object sender, EventArgs e)
        //{
 
        //}
        protected void HConsultaSQL_clik(object sender, EventArgs e)
        {
            if (DivTxtQuery.Visible == true)
            {
                DivTxtQuery.Visible = false;
                DivTemplate.Visible = true;
            }
            else
            {
                DivTxtQuery.Visible = true;
                DivTemplate.Visible = false;
            }
        }
        protected void DrEstadoIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }
        protected void DrArchivoIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }
        protected void TexTosPlantillas_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }

        protected void TexTosPlantillas_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaPlantilla(1);
            }
        }

        protected void TexTosProcesos_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }
        protected void TexTosProcesos_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }

        protected void TexTosMarcador_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaMarcador(1);
            }
        }
        protected void TexTosMarcador_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaMarcador(1);
            }
        }

        protected void TxTemplate_click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaTemplate(1);
            }
        }

        protected void DrFlujoIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }

        protected void DrBoxIniPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaProceso(1);
            }
        }


        protected void dratras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }
        protected void drsiguiente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }
        protected void dralternativo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }
        protected void drfinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaEstado(1);
            }
        }


        protected void DrDuplicado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrFlujos.BackColor = System.Drawing.Color.FromName("#bdecb6");
            this.Session["idflujo"] = DrFlujos.SelectedItem.Value;
            LbRelFlujo.Text = "Estados Asociados: " + DrFlujos.SelectedItem.Text;

            DrEstado.Items.Clear();
            DrEstado.DataValueField = "ZID_ESTADO";
            DrEstado.DataTextField = "ZNOMBRE";

            Dratras.Items.Clear();
            Dratras.DataValueField = "ZID_ESTADO";
            Dratras.DataTextField = "ZNOMBRE";

            Drsiguiente.Items.Clear();
            Drsiguiente.DataValueField = "ZID_ESTADO";
            Drsiguiente.DataTextField = "ZNOMBRE";

            Dralternativo.Items.Clear();
            Dralternativo.DataValueField = "ZID_ESTADO";
            Dralternativo.DataTextField = "ZNOMBRE";

            Drfinal.Items.Clear();
            Drfinal.DataValueField = "ZID_ESTADO";
            Drfinal.DataTextField = "ZNOMBRE";

            DrEstado.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Dratras.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drsiguiente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Dralternativo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));
            Drfinal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            dt = Main.CargaFlujosEstados(Convert.ToInt32(DrFlujos.SelectedItem.Value)).Tables[0];

            if(dt.Rows.Count == 0)
            {
                //dt = Main.CargaSoloFlujos(Convert.ToInt32(DrFlujos.SelectedItem.Value)).Tables[0];
                dt = Main.CargaEstadosFl(0).Tables[0];
                this.Session["EstadosFlujos"] = dt;

                DrEstado.DataSource = dt;
                DrEstado.DataBind();

                Dratras.DataSource = dt;
                Dratras.DataBind();

                Drsiguiente.DataSource = dt;
                Drsiguiente.DataBind();

                Dralternativo.DataSource = dt;
                Dralternativo.DataBind();

                Drfinal.DataSource = dt;
                Drfinal.DataBind();

                TxtNombre.Text = "";
                TxtDescripcion.Text = "";

                //Cargar Flujo y Estados
                foreach (DataRow dr in dt.Rows)
                {
                    TxtNombre.Text = dr["ZID"].ToString();
                    TxtDescripcion.Text = dr["ZDESCRIPCION"].ToString();
                    break;
                }
                ListBox1.Items.Clear();
                ListBox1ID.Items.Clear();
                ListBox2.Items.Clear();
                ListBox2ID.Items.Clear();

                foreach (DataRow fila in dt.Rows)//Campos
                {
                    ListBox1.Items.Add(new ListasID(fila["ZNOMBRE"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                    ListBox1ID.Items.Add(new ListasID(fila["ZID_ESTADO"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                }


                dt = Main.CargaArchivos(5).Tables[0];
                ListBox1.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#ffffff");

                ListBoxArchivo.Items.Clear();
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBoxArchivoID.Items.Clear();
                ListBoxArchivoID.BackColor = System.Drawing.Color.FromName("#ffffff");

                foreach (DataRow dr in dt.Rows)
                {
                    //ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#bdecb6");
                    ListBoxArchivo.Items.Add(new ListasID(dr["ZDESCRIPCION"].ToString(), Convert.ToInt32(dr["ZID"].ToString())).ToString());
                    ListBoxArchivoID.Items.Add(new ListasID(dr["ZID"].ToString(), Convert.ToInt32(dr["ZID"].ToString())).ToString());
                }
            }
            else
            {
                DrEstado.DataSource = dt;
                DrEstado.DataBind();

                Dratras.DataSource = dt;
                Dratras.DataBind();

                Drsiguiente.DataSource = dt;
                Drsiguiente.DataBind();

                Dralternativo.DataSource = dt;
                Dralternativo.DataBind();

                Drfinal.DataSource = dt;
                Drfinal.DataBind();

                TxtNombre.Text = "";
                TxtDescripcion.Text = "";

                //Cargar Flujo y Estados
                foreach (DataRow dr in dt.Rows)
                {
                    TxtNombre.Text = dr["ZID"].ToString();
                    TxtDescripcion.Text = dr["ZDESCRIPCION"].ToString();
                    break;
                }

                dt = Main.CargaArchivos(5).Tables[0];
                ListBox1.Items.Clear();
                ListBox1ID.Items.Clear();
                ListBox2.Items.Clear();
                ListBox2ID.Items.Clear();
                ListBox1.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox1ID.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBox2ID.BackColor = System.Drawing.Color.FromName("#ffffff");

                ListBoxArchivo.Items.Clear();
                ListBoxArchivo.BackColor = System.Drawing.Color.FromName("#ffffff");
                ListBoxArchivoID.Items.Clear();
                ListBoxArchivoID.BackColor = System.Drawing.Color.FromName("#ffffff");

                int visto = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    //ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
                    ListBoxArchivo.Items.Add(new ListasID(dr["ZDESCRIPCION"].ToString(), Convert.ToInt32(dr["ZID"].ToString())).ToString());
                    ListBoxArchivoID.Items.Add(new ListasID(dr["ZID"].ToString(), Convert.ToInt32(dr["ZID"].ToString())).ToString());
                    if(visto == 0)
                    {
                        for (int i = 0; i <= DrArchivos.Items.Count - 1; i++)
                        {
                            if (DrArchivos.Items[i].Value == dr["ZID"].ToString())
                            {
                                DrArchivos.SelectedIndex = i;
                                DrArchivos.BackColor = System.Drawing.Color.FromName("#bdecb6");
                                this.Session["idarchivo"] = dr["ZID"].ToString();
                                break;
                            }
                        }
                        visto = 1;
                    }
                }

                visto = 0;

                dt = Main.CargaEstadosFl(0).Tables[0];
                //Si hay Estados asociados a es Flujo
                foreach (DataRow fila in dt.Rows)//Campos
                {
                    visto = 0;
                    for (int i = 0; i <= DrEstado.Items.Count - 1; i++)
                    {
                        if (DrEstado.Items[i].Value == fila["ZID_ESTADO"].ToString())
                        {
                            ListBox2.Items.Add(new ListasID(fila["ZNOMBRE"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                            ListBox2ID.Items.Add(new ListasID(fila["ZID_ESTADO"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                            visto = 1;
                            break;
                        }
                    }
                    if (visto == 0)
                    {
                        ListBox1.Items.Add(new ListasID(fila["ZNOMBRE"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                        ListBox1ID.Items.Add(new ListasID(fila["ZID_ESTADO"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
                    }
                }
            }



        }

        protected void Djerarquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Btconecta_Click(object sender, EventArgs e)
        {
            //Prueba conexion a base de datos
            if(DrConexion.SelectedIndex == 0) { return; }
            string SQL = "";

            try
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SQL = DBHelper.ExecuteScalarSQL("SELECT ZCONECTSTRING FROM ZCONEXION WHERE ZID = " + DrConexion.SelectedIndex, dbParams).ToString();

                SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQL.Text, dbParams, SQL).ToString();
                //Iconexion.Attributes["style"] = "margin-top:-10px; color:green;";//dark
            }
            catch(Exception ex)
            {
                //Iconexion.Attributes["style"] = "margin-top:-10px; color:red;";//dark
                Lbmensaje.Text = "Error. " + ex.Message;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();

            }
        }


        protected void sellectKey(object sender, EventArgs e)
        {
            //Busco el select id y escribo en la lista keys para guardar
            string a = this.Session["Edicion"].ToString();
            //if (this.Session["Edicion"].ToString() == "0" )
            //{
            //    if (chkKey.Checked == true)
            //    {
            //        chkKey.Checked = false;
            //    }
            //    else
            //    {
            //        chkKey.Checked = true;
            //    }
            //    return;
            //}
            //if (chkKey.Checked == true)
            //{
            //    int index = ListKeys.SelectedIndex;
            //    ListKeys.Items[index].Text = "1";
            //    ListKeys.Items[index].Value = "1";
            //}
            //else
            //{
            //    int index = ListKeys.SelectedIndex;
            //    ListKeys.Items[index].Text = "0";
            //    ListKeys.Items[index].Value = "0";
            //}

            string x = "";
            for (int i = 0; i <= ListKeys.Items.Count - 1; i++)
            {
                if (ListKeys.Items[i].Value == "1")
                {
                    if (x == "")
                    {
                        x = ListBox2.Items[i].Text;
                    }
                    else
                    {
                        x += ", " + ListBox2.Items[i].Text;
                    }
                }
            }
            //if (x != "") { SMKey.InnerText = ". Keys de este Archivo: (" + x + ")"; }
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            SqlParameter[] dbParams = new SqlParameter[0];
            DBHelper.ExecuteNonQuerySQL("UPDATE ZARCHIVOS SET ZESTADO = 3 WHERE ZID = " + this.Session["IDArchivo"].ToString(), dbParams);
            Actualiza_Archivos();
            MiOpenMenu();

        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            MiOpenMenu();
        }

        protected void BtTipo_Click(object sender, EventArgs e)
        {
            if(DivSQL.Visible == false)
            {
                DivSQL.Visible = true;
                CommentSQL.Width = Unit.Percentage(100);
                CommentSQL.Height = 345;
                //DivSQL.Attributes.Add("height", "500px");
                //DivSQL.Attributes.Add("Width", "300px");
                DivCampoDer.Visible = false;
                //Iconexion.Attributes["style"] = "margin-top:-10px;";
            }
            else
            {
                DivSQL.Visible = false;
                DivCampoDer.Visible = true;
            }
        }

        protected void btnCopiaCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnCreaCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeleteCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnAllCustion_Click(object sender, EventArgs e)
        {

        }

        protected void DrCuestion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TxtIDcuestion_TextChanged(object sender, EventArgs e)
        {

        }
        protected void TxIDCuestion_click(object sender, EventArgs e)
        {

        }
        protected void DrMarcadorCuestion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnDeleteCuestion_Click(object sender, EventArgs e)
        {

        }
        protected void ListMarcaCuestion_DoubleClick(object sender, EventArgs e)
        {

        }
        protected void sellectAll(object sender, EventArgs e)
        {

        }
        protected void btnTipoTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnCopiaTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnCreaTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeleteTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnAllTemplateM_Click(object sender, EventArgs e)
        {

        }

        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
        }

        protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();


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


                if ((e.Row.DataItemIndex % 2) == 0)
                {
                    //Par
                    e.Row.BackColor = System.Drawing.Color.FromName("#fff");
                }
                else
                {
                    //Impar
                    e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
                }
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

                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
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
                    //this.Session["idregistro"] = gvLista.DataKeys[index].Value.ToString();
                    this.Session["iddocumento"] = gvLista.DataKeys[index].Value.ToString();

                    DataTable dt = this.Session["SelTableFiles"] as DataTable;

                    foreach (DataRow filas in dt.Rows)
                    {
                        if (filas["ZID"].ToString() == gvLista.DataKeys[index].Value.ToString())
                        {
                            string fileName = System.IO.Path.GetFileName(@filas["ZDIRECTORIO"].ToString());


                            //if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == false)
                            //{
                            //    //mensaje que no existe el fichero
                            //    Lbmensaje.Text = " El fichero no se encuentra en el Servidor Web.";
                            //    cuestion.Visible = false;
                            //    Asume.Visible = true;
                            //    DvPreparado.Visible = true;
                            //    return;

                            //}
                            //else if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == true)
                            //{
                            //    string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["idregistro"].ToString(), fileName));
                            //    string Midirectorio = Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString());

                            //    //string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["iddocumento"].ToString(), fileName));
                            //    //string Midirectorio = Server.MapPath("~/Docs/" + this.Session["iddocumento"].ToString());

                            //    if (Directory.Exists(Midirectorio) == false)
                            //    {
                            //        DirectoryInfo di = Directory.CreateDirectory(Midirectorio);
                            //    }


                            //    if (File.Exists(@MiPath) == false)
                            //    {
                            //        File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                            //    }
                            //    else
                            //    {
                            //        File.Delete(MiPath);
                            //        File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
                            //    }

                            //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                            //    string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
                            //    string[] Separado = url.Split('/');
                            //    url = "";
                            //    if (Separado.Count() > 0)
                            //    {
                            //        for (int i = 0; i < Separado.Count() - 1; i++)
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

                            //        url += "/Docs/" + this.Session["idregistro"].ToString() + "/" + fileName;
                            //        //url += "/Docs/" + this.Session["iddocumento"].ToString() + "/" + fileName;
                            //    }



                            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "', '_blank');", true);

                            //    string a = Main.Ficherotraza("gvLista rowcommand -->  La ruta del fichero esta en " + url);
                            //}
                            break;
                        }
                    }
                    //Reemplazar "pdffile.pdf" con el nombre de su archivo PDF.
                }
                else if (e.CommandName == "ImprimirDoc")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvLista rowcommand --> " + ex.Message);
            }
        }

        protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvLista.SelectedRow.BackColor = System.Drawing.Color.FromName("#565656");
        }
        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            return;
        }
        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();
            gvLista.EditIndex = -1;

            gvLista.DataBind();
        }
        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();
            try
            {
                gvLista.EditIndex = -1;

                gvLista.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);

            }
        }
        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
        }
        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }










        protected void ListBox1Tar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1Tar.SelectedIndex;
            ListBox1IDTar.SelectedIndex = ListBox1Tar.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;
            LbIDTarea.Text = ListBox1IDTar.SelectedItem.Text;
            string a = ListBox1IDTar.SelectedValue;
            if (DivCancel.Visible == false)
            {
                DivRel.Visible = false;
                DivEdita.Visible = true;
            }

            //ListBox1Tar.BackColor = Color.FromName("#bdecb6");
        }

        protected void ListBox2Tar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2Tar.SelectedIndex;
            ListBox2IDTar.SelectedIndex = ListBox2Tar.SelectedIndex;


            for (int i = 0; i <= DrEstado.Items.Count - 1; i++)
            {
                if (DrEstado.Items[i].Value == ListBox2IDTar.SelectedItem.Value)
                {
                    DrEstado.SelectedIndex = i;
                    break;
                }
            }

            //ListBox2Tar.BackColor = Color.FromName("#bdecb6");
        }



        protected void ListBox2IDTar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2IDTar.SelectedIndex;
            //ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
            ListBox2.SelectedIndex = ListBox2IDTar.SelectedIndex;
            //ListKeys.SelectedIndex = ListBox2IDTar.SelectedIndex;

            string a = ListBox2IDTar.SelectedValue;
            //string b = ListKeys.SelectedValue;
            //chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));
        }

        protected void btnEditaTar_Click(object sender, EventArgs e)
        {
            divTarea.Visible = false;
            divNePlantilla.Visible = true;
            BtEditTarea.Visible=false;

            string SQL = "SELECT ZID,ZDESCRIPCION,ZHAND ";
            SQL += " FROM ZHANDS ";
            SQL += " WHERE ZID = " + ListBox1IDTar.SelectedIndex;
            System.Data.DataTable dtt = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in dtt.Rows) //Tabla Archivos seleccionado consulta en tabla Procesos
            {
                TxtDescTarea.Text = filas["ZDESCRIPCION"].ToString();
                TxtDummieE.Text = filas["ZHAND"].ToString();
                break;
            }
            SQL = "SELECT A.ZID, A.ZID_HAND, B.ZID as ZID_USUARIO, B.ZALIAS ";
            SQL += " FROM ZUSUARIOPROCESOS A, ZUSUARIOS B ";
            SQL += " WHERE A.ZID_HAND = " + ListBox1IDTar.SelectedIndex;
            SQL += " AND B.ZID = A.ZID_USUARIO ";

            dtt = Main.BuscaLote(SQL).Tables[0];
            DrAsigUser.DataValueField = "ZID";
            DrAsigUser.DataTextField = "ZALIAS";
            DrAsigUser.DataSource = dtt;
            DrAsigUser.DataBind();
        }

            
        protected void ListBox1IDTar_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1IDTar.SelectedIndex;
            //ListBox1Col.SelectedIndex = ListBox1ID.SelectedIndex;
            ListBox1Tar.SelectedIndex = ListBox1IDTar.SelectedIndex;
            LbIDTarea.Text = ListBox1IDTar.SelectedItem.Text;
            if (DivCancel.Visible == false)
            {
                DivRel.Visible = false;
                DivEdita.Visible = true;
            }

            //string a = ListBox1IDTar.SelectedValue;

            //string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            //SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            //SQL += " WHERE A.ZTIPO = B.ZID ";
            //SQL += " AND A.ZID = " + a;
            //SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            //DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];
        }

        protected void btnPasarSeleccionadosTar_Click(object sender, EventArgs e)
        {
            //Pasamos los items seleccionados de listbox2 a listbox1

            try
            {
                while (ListBox1Tar.GetSelectedIndices().Length > 0)
                {
                    ListBox2Tar.Items.Add(ListBox1Tar.SelectedItem);
                    ListBox2IDTar.Items.Add(ListBox1IDTar.SelectedItem);
                    //ListBox2Col.Items.Add(ListBox1Col.SelectedItem);
                    //ListKeys.Items.Add("0");
                    ListBox1IDTar.Items.RemoveAt(ListBox1Tar.SelectedIndex);
                    //ListBox1Col.Items.RemoveAt(ListBox1.SelectedIndex);
                    ListBox1Tar.Items.Remove(ListBox1Tar.SelectedItem);
                }

            }
            catch
            {

            }


        }

        protected void btnRegresarSeleccionadosTar_Click(object sender, EventArgs e)
        {
            //Regresamos los items seleccionados de listbox2 a listbox1

            while (ListBox2Tar.GetSelectedIndices().Length > 0)
            {
                ListBox1Tar.Items.Add(ListBox2Tar.SelectedItem);
                ListBox1IDTar.Items.Add(ListBox2IDTar.SelectedItem);
                //ListBox1Col.Items.Add(ListBox2Col.SelectedItem);
                ListBox2IDTar.Items.RemoveAt(ListBox2Tar.SelectedIndex);
                //ListKeys.Items.RemoveAt(ListBox2Tar.SelectedIndex);
                //ListBox2Col.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2Tar.Items.Remove(ListBox2Tar.SelectedItem);
            }
        }

        protected void btnBuscarTar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en listbox1

            for (int i = 0; i < ListBox1Tar.Items.Count - 1; i++)
            {
                if (ListBox1Tar.Items[i].Text.Contains(TxtBuscaTar.Text))
                {
                    string a = ListBox1Tar.Items[i].Text;
                    a = ListBox1Tar.Items[i].Value;
                    ListBox1Tar.Items[i].Selected = true;
                    ListBox1IDTar.Items[i].Selected = true;
                    //ListBox1Col.Items[i].Selected = true;
                    break;
                }
            }
            for (int i = 0; i < ListBox2Tar.Items.Count - 1; i++)
            {
                if (ListBox2Tar.Items[i].Text.Contains(TxtBuscaTar.Text))
                {
                    string a = ListBox2Tar.Items[i].Text;
                    a = ListBox2Tar.Items[i].Value;
                    ListBox2Tar.Items[i].Selected = true;
                    ListBox2IDTar.Items[i].Selected = true;
                    //ListBox2Col.Items[i].Selected = true;
                    break;
                }
            }


        }
        protected void btnLimpiarTar_Click(object sender, EventArgs e)
        {
            //
            divTarea.Visible = false;
            divNePlantilla.Visible = true;
            SqlParameter[] dbParams = new SqlParameter[0];

            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZHANDS", dbParams));
            LbIDTarea.Text = (MiID + 1).ToString();

            if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }

            //col6List1.Visible = false;
            //col6Relacion.Visible = false;
            //col6List2.Visible = false;
            //DivColTarea.Visible = true;
            //DivTextTarea.Visible = true;

            ////Eliminamos todos los registros del listbox2
            //for (int i = 0; i <= ListBox2Tar.Items.Count - 1; i++)
            //{
            //    ListBox1Tar.Items.Add(ListBox2Tar.Items[i]);
            //    ListBox1IDTar.Items.Add(ListBox2IDTar.Items[i]);
            //}
            //ListBox2Tar.Items.Clear();
            //ListBox2IDTar.Items.Clear();
            //ListKeys.Items.Clear();

        }

        protected void BtSelUser_Click(object sender, EventArgs e)
        {
            //aqui
            string SQL = "SELECT ZID,ZID_USUARIO,ZID_ARCHIVO,ZID_CAMPO,ZID_FLUJO,ZID_ESTADO,ZID_PROCESO ";
            SQL += ", ZID_PLANTILLA, ZID_PROFILES, ZID_MARCADOR, ZID_ALMACEN, ZID_DUMMIE, ZID_HAND ";
            SQL += " FROM ZUSUARIOPROCESOS ";
            SQL += " WHERE ZID_USUARIO = " + DrUsuarios.SelectedValue;
            SQL += " AND ZID_HAND = " + LbIDTarea.Text;

            Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            if (Con == null)
            {

                SqlParameter[] dbParams = new SqlParameter[0];
                SQL = "INSERT INTO ZUSUARIOPROCESOS (ZID_USUARIO, ZID_HAND) ";
                SQL += " VALUES (" + DrUsuarios.SelectedValue + "," + LbIDTarea.Text + ")";
                DBHelper.ExecuteNonQuerySQL(SQL, dbParams);

                SQL = "SELECT A.ZID, A.ZID_HAND, B.ZID as ZID_USUARIO, B.ZALIAS ";
                SQL += " FROM ZUSUARIOPROCESOS A, ZUSUARIOS B ";
                SQL += " WHERE A.ZID_HAND = " + LbIDTarea.Text;
                SQL += " AND B.ZID = A.ZID_USUARIO ";

                System.Data.DataTable dtt = Main.BuscaLote(SQL).Tables[0];
                DrAsigUser.DataValueField = "ZID";
                DrAsigUser.DataTextField = "ZALIAS";
                DrAsigUser.DataSource = dtt;
                DrAsigUser.DataBind();
            }
            else
            {
                //El usuario ya existe
                //Mensaje 
                Lbmensaje.Text = "El Usuario ya está asignado en la lista de Tareas. ";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();

            }
        }
        protected void SubirTar_Click(object sender, EventArgs e)
        {
            int J = ListBox2Tar.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
#pragma warning disable CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            string sTmpCol = "";
#pragma warning restore CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2Tar.SelectedIndex;
                if (index == 0) { return; }
                sTmp = ListBox2Tar.Items[index - 1].Text;
                sTmpID = ListBox2IDTar.Items[index - 1].Text;
                //sTmpCol = ListBox2Col.Items[index - 1].Text;
                ListBox2Tar.Items[index - 1].Text = ListBox2Tar.Items[index].Text;
                ListBox2IDTar.Items[index - 1].Text = ListBox2IDTar.Items[index].Text;
                //ListBox2Col.Items[index - 1].Text = ListBox2Col.Items[index].Text;
                ListBox2Tar.Items[index].Text = sTmp;
                ListBox2IDTar.Items[index].Text = sTmpID;
                //ListBox2Col.Items[index].Text = sTmpCol;
                if (index > 0)
                {
                    ListBox2Tar.SelectedIndex = index - 1;
                    ListBox2IDTar.SelectedIndex = index - 1;
                    //ListBox2Col.SelectedIndex = index - 1;
                }

            }


        }

        protected void btnGuardarRelacionTar_Click(object sender, EventArgs e)
        {
            //Guardar Relación
            //EditaFlujo(0);
            //this.Session["Edicion"] = "0";

        }

        protected void btnCancelarRelacionTar_Click(object sender, EventArgs e)
        {

            //EditaFlujo(0);
            //this.Session["Edicion"] = "0";

        }

        protected void BajarTar_Click(object sender, EventArgs e)
        {
            int J = ListBox2Tar.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
#pragma warning disable CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            string sTmpCol = "";
#pragma warning restore CS0219 // La variable 'sTmpCol' está asignada pero su valor nunca se usa
            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2Tar.SelectedIndex;
                if (index < ListBox2Tar.Items.Count)
                {
                    if (index == ListBox2Tar.Items.Count - 1) { return; }
                    sTmp = ListBox2Tar.Items[index + 1].Text;
                    sTmpID = ListBox2IDTar.Items[index + 1].Text;
                    //sTmpCol = ListBox2Col.Items[index + 1].Text;
                    ListBox2Tar.Items[index + 1].Text = ListBox2Tar.Items[index].Text;
                    ListBox2IDTar.Items[index + 1].Text = ListBox2IDTar.Items[index].Text;
                    //ListBox2Col.Items[index + 1].Text = ListBox2Col.Items[index].Text;
                    ListBox2Tar.Items[index].Text = sTmp;
                    ListBox2IDTar.Items[index].Text = sTmpID;
                    //ListBox2Col.Items[index].Text = sTmpCol;
                    ListBox2Tar.SelectedIndex = index + 1;
                    ListBox2IDTar.SelectedIndex = index + 1;
                    //ListBox2Col.SelectedIndex = index + 1;
                }
            }

        }

        public void RelacionesTareas()
        {
            int MiID = Convert.ToInt32(TxtIdMarcador.Text);
            DrDummie.Items.Clear();
            ListBox1Tar.Items.Clear();
            ListBox2Tar.Items.Clear();
            ListBox1IDTar.Items.Clear();
            ListBox2IDTar.Items.Clear();

            string SQL = "SELECT ZID , ZNOMBRE, ZCAMPO, ZENTRADA , ZSALIDA, ZPAGINA, ZX ";
            SQL += " FROM ZMARCADORES ";
            System.Data.DataTable dtMarcadores = Main.BuscaLote(SQL).Tables[0];

            SQL = " SELECT ZID, ZDESCRIPCION FROM ZDUMMIE ORDER BY ZID ";
            System.Data.DataTable dtDummie = Main.BuscaLote(SQL).Tables[0];
            DrDummie.DataValueField = "ZID";
            DrDummie.DataTextField = "ZDESCRIPCION";
            DrDummie.DataSource = dtDummie;
            DrDummie.DataBind();


            SQL = " SELECT ZID, ZALIAS FROM ZUSUARIOS ORDER BY ZID ";
            System.Data.DataTable dtusuario = Main.BuscaLote(SQL).Tables[0];
            DrUsuarios.DataValueField = "ZID";
            DrUsuarios.DataTextField = "ZALIAS";
            DrUsuarios.DataSource = dtusuario;
            DrUsuarios.DataBind();




            foreach (DataRow filas in dtMarcadores.Rows) //Tabla Archivos seleccionado consulta en tabla Procesos
            {
                if (TxtIdMarcador.Text == filas["ZID"].ToString())
                {
                    //Si el identificador es tipo campo 5 
                    if (filas["ZCAMPO"].ToString() == "5" )
                    {
                        int n;
                        bool isNumeric = int.TryParse(filas["ZENTRADA"].ToString(), out n);
                        if (isNumeric == true)
                        {
                            for (int d = 0; d < DrDummie.Items.Count; d++)
                            {
                                if (DrDummie.Items[d].Value == filas["ZENTRADA"].ToString())
                                {
                                    DrDummie.SelectedIndex = d;
                                    this.Session["iddummie"] = DrDummie.Items[d].Value.ToString();
                                    break;
                                }
                            }
                            CargaDummies(this.Session["iddummie"].ToString());

                            string LaMacro = "";
                            //Busca y concatena las macros del ZID seleccionado en ZENTRADA del ZDUMMIE
                            foreach (DataRow fila in dtMacro.Rows)
                            {
                                LaMacro += fila["ZHAND"].ToString() + Environment.NewLine;
                            }

                            //if (LaMacro != "")
                            //{
                                //if (Mistrazas == "1")
                                //{
                                //    string h = Main.Ficherotraza("Lanza Dummie --> Macro: " + idmarcador);
                                //}

                                //EjecutaDummie_Click(LaMacro, idmarcador);
                            //}
                        }
                        else
                        {
                            //Sino, se queda como está y no muestra el las realciones


                        }
                    }
                }
            }

            //Si no hay Flujos
            if (dtMacro.Rows.Count > 0 )
            {
                //Si hay Estados asociados a es Flujo
                foreach (DataRow fila in dtMacro.Rows)//Campos
                {
                    ListBox2Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ListBox2IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
            if (dtAntiMacro.Rows.Count > 0)
            {
                //los Estados no asociados
                foreach (DataRow fila in dtAntiMacro.Rows)//Campos
                {
                    ListBox1Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ListBox1IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
        }

        private void CargaDummies(string id)
        {
            //Busca la macro o conjunto de macros para lanzar con dummie
            string SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION, A.ZID as ZIDDUMMIE, A.ZDESCRIPCION ,B.ZORDEN ";
            SQL += " FROM ZDUMMIE A, ZDUMMIEHAND B, ZHANDS C ";
            SQL += " WHERE B.ZIDDUMMIE = A.ZID ";
            SQL += " AND B.ZIDHAND = C.ZID ";
            SQL += " ORDER BY B.ZORDEN, A.ZDESCRIPCION ,C.ZDESCRIPCION ";
            dtMacros = Main.BuscaLote(SQL).Tables[0];

            //Busca la macro o conjunto de macros para lanzar con dummie
            SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION, A.ZDESCRIPCION ,B.ZORDEN ";
            SQL += " FROM ZDUMMIE A, ZDUMMIEHAND B, ZHANDS C ";
            SQL += " WHERE B.ZIDDUMMIE = A.ZID ";
            SQL += " AND B.ZIDHAND = C.ZID ";
            SQL += " AND A.ZID = " + id;
            SQL += " ORDER BY B.ZORDEN, A.ZDESCRIPCION ,C.ZDESCRIPCION ";
            dtMacro = Main.BuscaLote(SQL).Tables[0];

            //Busca la macro o conjunto de macros para lanzar con dummie
            SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION ";
            SQL += " FROM ZHANDS C ";
            SQL += " ORDER BY C.ZID ";
            dtAntiMacro = Main.BuscaLote(SQL).Tables[0];
        }

        protected void DrDummie_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox1Tar.Items.Clear();
            ListBox2Tar.Items.Clear();
            ListBox1IDTar.Items.Clear();
            ListBox2IDTar.Items.Clear();
            CargaDummies(DrDummie.SelectedValue);

            if (dtMacros.Rows.Count > 0)
            {
                //Si hay Estados asociados a es Flujo
                foreach (DataRow fila in dtMacros.Rows)//Campos
                {
                    if(fila["ZIDDUMMIE"].ToString() == DrDummie.SelectedValue)
                    {
                        ListBox2Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        ListBox2IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    }
                }
                Boolean Esta = false;
                foreach (DataRow filas in dtAntiMacro.Rows)//Campos
                {
                    Esta = false;
                    for (int i = 0; i < ListBox2IDTar.Items.Count; i++)
                    {
                        if (ListBox2IDTar.Items[i].Value == filas["ZID"].ToString())
                        {
                            Esta = true;
                            break;
                        }
                    }
                    if (Esta == false)
                    {
                        ListBox1Tar.Items.Add(new ListasID(filas["ZDESCRIPCION"].ToString(), Convert.ToInt32(filas["ZID"].ToString())).ToString());
                        ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        ListBox1IDTar.Items.Add(new ListasID(filas["ZID"].ToString(), Convert.ToInt32(filas["ZID"].ToString())).ToString());
                    }
                }
            }
           if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }
        }

        protected void btnCancelNewDummie_Click(object sender, EventArgs e)
        {
            divTarea.Visible = true;
            divNePlantilla.Visible = false;
            DivRel.Visible = true;
            DivEdita.Visible = false;
        }
        protected void btnSaveNewDummie_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            string SQL = "SELECT ZID ";
            SQL += " FROM ZHANDS ";
            SQL += " WHERE ZID = " + LbIDTarea.Text;

            Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            if (Con == null)
            {
                string Column = "INSERT INTO ZHANDS (ZDESCRIPCION, ZHAND) ";
                Column += " VALUES('" + TxtDescTarea.Text + "','" + TxtDummieE.Text + "')";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
            }
            else
            {
                string Column = "UPDATE ZHANDS SET ZDESCRIPCION ='" + TxtDescTarea.Text + "', ZHAND = '" + TxtDummieE.Text + "'";
                Column += " WHERE ZID = " + LbIDTarea.Text;
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
            }
            divTarea.Visible = true;
            divNePlantilla.Visible = false;
            RelacionesTareas();
            TxtDescTarea.Text = "";
            TxtDummieE.Text = "";
        }
        
        protected void btnNewDummie_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];
            BtEditTarea.Visible = false;
            if (BtNewDummie.Text == "Nueva Plantilla")
            {
                //maximo id
                int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZDUMMIE", dbParams));
                LbIDPlantDummie.Text = (MiID + 1).ToString();

                TxtNewDummie.Text = "";
                DivRel.Visible = false;
                DivCancel.Visible = true;
                TxtNewDummie.Visible = true;
                DrDummie.Visible = false;
                BtNewDummie.Text = "Guardar Plantilla";
                BtNewDummie.Attributes["class"] = "btn btn-success btn-block";
            }
            else
            {
                //Guarda en base de datos con MAX
                string Column = "INSERT INTO ZDUMMIE (ZDESCRIPCION) ";
                Column += " VALUES('" + TxtNewDummie.Text + "')";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                DivRel.Visible = true;
                DivCancel.Visible = false;
                TxtNewDummie.Visible = false;
                DrDummie.Visible = true;
                BtNewDummie.Text = "Nueva Plantilla";
                BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
                RelacionesTareas();
            }
        }
        protected void btnCancelaTar_Click(object sender, EventArgs e)
        {
            DivRel.Visible = true;
            DivCancel.Visible = false;
            TxtNewDummie.Visible = false;
            DrDummie.Visible = true;
            BtNewDummie.Text = "Nueva Plantilla";
            BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
        }
        protected void BtEliminaDummie_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];
            string Column = "DELETE FROM ZDUMMIE WHERE ZID = " + LbIDPlantDummie.Text;
            DBHelper.ExecuteNonQuerySQL(Column, dbParams);

            DivRel.Visible = true;
            DivCancel.Visible = false;
            TxtNewDummie.Visible = false;
            DrDummie.Visible = true;
            BtNewDummie.Text = "Nueva Plantilla";
            BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
        }
        
    }
}