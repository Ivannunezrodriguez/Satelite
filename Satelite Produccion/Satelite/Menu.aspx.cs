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
using System.Management;
using System.Text;

namespace Satelite
{
    public partial class Menu : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;
        public string CampoOrden = "";
  

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
                    this.Session["idmenu"] = null;
                    this.Session["idestado"] = null;
                    this.Session["TablaObj"] = "";
                    this.Session["TablaMenus"] = "";
                    this.Session["Grupos"] = null;
                    this.Session["iddummie"] = "";
                    this.Session["proceso"] = "";
                    this.Session["totalMenus"] = null;
                    this.Session["indice"] = null;
                    this.Session["Cargando"] = "0";
                    this.Session["Contador"] = 0;
                    this.Session["TotalContador"] = 0;


                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Server.Transfer("Inicio.aspx"); //Default
                    }

                    if(this.Session["Edicion"].ToString() == "1")
                    {
                        //if(this.Session["proceso"].ToString() == "profile") { EditaProfile(1); }
                        //if (this.Session["proceso"].ToString() == "proceso") { EditaProceso(1); }
                        //if (this.Session["proceso"].ToString() == "plantilla") { EditaPlantilla(1); }
                        //if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }

                    }

                    LbIDArchivo.InnerHtml = "Relaciones ";
                    this.Session["IDArchivo"] = 1;

                    DataTable dt = Main.CargaGrupo(0).Tables[0];
                    this.Session["Grupos"] = dt;
                    Drsiguiente.Items.Clear();
                    Drsiguiente.DataValueField = "ZID";
                    Drsiguiente.DataTextField = "ZNOMBRE";
                    Drsiguiente.Items.Insert(0, new ListItem("Ninguno", "0"));
                    Drsiguiente.DataSource = dt;
                    Drsiguiente.DataBind();

                    Actualiza_Archivos();

                    CargaLosMenus();



                }
            }
            catch (Exception ex)
            {
                string a = Main.ETrazas("", "1", " AltaArchivos.Page_load --> Error:" + ex.Message);
                Server.Transfer("Login.aspx");
            }
        }

        private void CargaLosMenus()
        {
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox1ID.Items.Clear();
            ListBox2ID.Items.Clear();
            ListBoxNivel.Items.Clear();


            //DataTable dtt = new DataTable("Tabla");
            //DataTable dto = new DataTable("Tabla");

            //dtt.Columns.Add("ZID");
            //dtt.Columns.Add("ZDESCRIPCION");

            //dto.Columns.Add("ZID");
            //dto.Columns.Add("ZDESCRIPCION");

            DataTable dt = Main.CargaMenus(0).Tables[0];
            foreach (DataRow fila in dt.Rows)//Menus
            {
                ListBox1.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            }
            ListBox1.BackColor = Color.FromName("#ffffff");
            ListBox1ID.BackColor = Color.FromName("#ffffff");
            ListBox2.BackColor = Color.FromName("#ffffff");
            ListBox2ID.BackColor = Color.FromName("#ffffff");
            LbDisponible.Text = dt.Rows.Count.ToString();

            DrNiveles.Items.Clear();
            DrNiveles.DataValueField = "ZID";
            DrNiveles.DataTextField = "ZID"; //= "ZDESCRIPCION";

            dt = Main.CargaNiveles().Tables[0];
            DrNiveles.DataSource = dt;
            DrNiveles.DataBind();
        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            sb.AppendLine("<ul>" + Environment.NewLine);// class=\"nav nav-second-level collapse\">");

            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string handler = dr["ZPAGINA"].ToString();
                    string menuText = dr["ZTITULO"].ToString();
                    string pid = dr["ZID"].ToString();
                    string parentId = dr["ZROOT"].ToString();

                    string line = String.Format(@"<li><a href=""{0}"">{1}</a>" + Environment.NewLine, handler, menuText);
                    sb.Append(line);

                    DataRow[] subMenu = table.Select(String.Format("ZROOT = {0}", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
                    }
                    sb.Append("</li>" + Environment.NewLine);
                    //sb.Append("</ul>");
                }
            }

            sb.Append("</ul>" + Environment.NewLine);
            return sb.ToString();
        }


        private string ULMenu(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            string Miro = sb.ToString();
            try
            {

                if (menu.Length > 0)
                {
                    Miro += "<ul>" + Environment.NewLine;
                    foreach (DataRow dr in menu)
                    {
                        string handler = dr["ZPAGINA"].ToString();
                        string menuText = dr["ZTITULO"].ToString();
                        string pid = dr["ZID"].ToString();
                        string parentId = dr["ZROOT"].ToString();
                        Miro += String.Format(@"<li><a href=""{0}"">{1}</a>ZID=" + pid + "</li>" + Environment.NewLine, handler, menuText);

                        DataRow[] subMenu = table.Select(String.Format("ZROOT = {0}", pid));
                        if (subMenu.Length > 0 && !pid.Equals(parentId))
                        {
                            var subMenuBuilder = new StringBuilder();
                            Miro += ULMenu(subMenu, table, subMenuBuilder);
                            Miro += "</li>";
                        }
                    }
                    Miro += "</ul>" + Environment.NewLine;
                }
            }
            catch
            {
                Miro += "</li></ul>" + Environment.NewLine;
            }
            return Miro.ToString();
        }


        private string GenerateULMenu(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            string Miro = sb.ToString();
            try
            {
                if (menu.Length > 0)
                {
                    Miro += "<ul>" + Environment.NewLine;
                    foreach (DataRow dr in menu)
                    {
                        string handler = dr["ZPAGINA"].ToString();
                        string menuText = dr["ZTITULO"].ToString();
                        string pid = dr["ZID"].ToString();
                        string parentId = dr["ZROOT"].ToString();
                        Miro += String.Format(@"<li><a href=""{0}"">{1}</a></li>" + Environment.NewLine, handler, menuText) ;
                        DataRow[] subMenu = table.Select(String.Format("ZROOT = {0}", pid));
                        if (subMenu.Length > 0 && !pid.Equals(parentId))
                        {
                            var subMenuBuilder = new StringBuilder();
                            Miro += GenerateULMenu(subMenu, table, subMenuBuilder);
                            Miro += "</li>" ;
                        }
                    }
                    Miro += "</ul>" + Environment.NewLine;
                }
            }
            catch
            {
                Miro += "</li></ul>" + Environment.NewLine;
            }
            return Miro.ToString();
        }


        private void Actualiza_Archivos()
        {
            DataTable dt3 = new DataTable();
            dt3 = Main.CargaNivel().Tables[0];
            this.Session["Niveles"] = dt3;


            string SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
            SQL += "FROM ZMENU AA WHERE  ZESTADO <> 3 ";
            SQL += "ORDER BY ZID, ZROOT";
            DataTable table = Main.BuscaLote(SQL).Tables[0];
            this.Session["totalMenus"] = table;
            DataRow[] parentMenus = table.Select("ZROOT = 0");           

            DrFlujos.Items.Clear();
            this.Session["Flujos"] = table;

            DrFlujos.DataValueField = "ZID";
            DrFlujos.DataTextField = "ZTITULO";

            DrFlujos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DrFlujos.DataSource = table;
            DrFlujos.DataBind();

            DrConexion.Items.Clear();
            
            DrConexion.DataValueField = "ZID";
            DrConexion.DataTextField = "ZTITULO";
            DrConexion.Items.Insert(0, new ListItem("Ninguno", "0"));

            DrConexion.DataSource = table;
            DrConexion.DataBind();

            DataTable dt = Main.CargaGrupo(0).Tables[0];
            this.Session["Grupos"] = dt;

            if (this.Session["Edicion"].ToString() != "0") 
            { 
                Drsiguiente.Items.Clear();
                Drsiguiente.DataValueField = "ZID";
                Drsiguiente.DataTextField = "ZNOMBRE";
                Drsiguiente.Items.Insert(0, new ListItem("Ninguno", "0"));
                Drsiguiente.DataSource = dt;
                Drsiguiente.DataBind();
            }


            //var sb = new StringBuilder();
            //string unorderedList = GenerateUL(parentMenus, table, sb);
            //DivEstados.InnerHtml = unorderedList;
        }

        protected void DrTemplatesTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListTemplaID.Items.RemoveAt(ListTempla.SelectedIndex);
            //ListTempla.Items.RemoveAt(ListTempla.SelectedIndex);
        }
        //protected void btnDeleteArchivoTemplate_Click(object sender, EventArgs e)
        //{
        //    ListTemplaID.Items.RemoveAt(ListTempla.SelectedIndex);
        //    ListTempla.Items.RemoveAt(ListTempla.SelectedIndex);
        //}
        //protected void btnDeleteFiltroProfile_Click(object sender, EventArgs e)
        //{
        //    ListBox4.Items.RemoveAt(ListBox3.SelectedIndex);
        //    ListBox3.Items.RemoveAt(ListBox3.SelectedIndex);
        //}

        //protected void btnDeleteArDoc_Click(object sender, EventArgs e)
        //{
        //    ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
        //    ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
        //}

        protected void btnDeleteTemplate_Click(object sender, EventArgs e)
        {
            //ListTemplateID.Items.RemoveAt(ListTemplate.SelectedIndex);
            //ListTemplate.Items.RemoveAt(ListTemplate.SelectedIndex);
        }

        //protected void btnDeleteProcesPlant_Click(object sender, EventArgs e)
        //{
        //    ListPlantillasID.Items.RemoveAt(ListPlantillas.SelectedIndex);
        //    ListPlantillas.Items.RemoveAt(ListPlantillas.SelectedIndex);
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaProceso(1);
        //    }
        //}
        //protected void btnDeletePlantMarc_Click(object sender, EventArgs e)
        //{
        //    ListPlantMarcaID.Items.RemoveAt(ListPlantMarca.SelectedIndex);
        //    ListPlantMarca.Items.RemoveAt(ListPlantMarca.SelectedIndex);
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaPlantilla(1);
        //    }
        //}

        

        protected void ListBoxFiltroProfile_DoubleClick(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProfile(1);
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

       //protected void btnMuestraProcesos_Click(object sender, EventArgs e)
       // {
       //     this.Session["Edicion"] = "0";
       //     this.Session["proceso"] = "";
       //     PanelFlujos.Visible = false;
       //     PanelProcesos.Visible = true;
       //     divAsigEstado.Visible = false;
       //     //Carga Procesos

       //     string SQL = "SELECT ZID, ZDESCRIPCION, ZPLANTILLAS, ZID_ESTADO, ZQUERY, ZID_ARCHIVO, ZID_FLUJO, ZEJECUCION ";
       //     SQL += " FROM ZPROCESOS ORDER BY ZID ";
       //     DataTable dt = Main.BuscaLote(SQL).Tables[0];

       //     DrProcesos.Items.Clear();
       //     DrProcesos.DataValueField = "ZID";
       //     DrProcesos.DataTextField = "ZDESCRIPCION";
       //     DrProcesos.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrProcesos.DataSource = dt;
       //     DrProcesos.DataBind();


       //     SQL = "SELECT ZID, ZDESCRIPCION, ZRUTAENTRADA, ZRUTASALIDA, ZCOPYORIGINAL, ZSIGNPDF, ZRUTAALTERNATIVA ";
       //     SQL += " FROM ZPLANTILLAS ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrPlantillas.Items.Clear();
       //     DrPlantillas.DataValueField = "ZID";
       //     DrPlantillas.DataTextField = "ZDESCRIPCION";
       //     DrPlantillas.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrPlantillas.DataSource = dt;
       //     DrPlantillas.DataBind();

       //     DrProcesoPlantilla.Items.Clear();
       //     DrProcesoPlantilla.DataValueField = "ZID";
       //     DrProcesoPlantilla.DataTextField = "ZDESCRIPCION";
       //     DrProcesoPlantilla.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrProcesoPlantilla.DataSource = dt;
       //     DrProcesoPlantilla.DataBind();
            

       //     SQL = "SELECT ZID, ZNOMBRE,  ZCAMPO, ZENTRADA, ZSALIDA, ZPAGINA, ZX, ZY, ZROTACION, ZSELLO, ZROOT ";
       //     SQL += " FROM ZMARCADORES ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrMarcadores.Items.Clear();
       //     DrMarcadores.DataValueField = "ZID";
       //     DrMarcadores.DataTextField = "ZNOMBRE";
       //     DrMarcadores.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrMarcadores.DataSource = dt;
       //     DrMarcadores.DataBind();

       //     DrMarcadoresPlantillas.Items.Clear();
       //     DrMarcadoresPlantillas.DataValueField = "ZID";
       //     DrMarcadoresPlantillas.DataTextField = "ZNOMBRE";
       //     DrMarcadoresPlantillas.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrMarcadoresPlantillas.DataSource = dt;
       //     DrMarcadoresPlantillas.DataBind();

       //     DrTipoMarcador.Items.Clear();
       //     DrTipoMarcador.DataValueField = "ZID";
       //     DrTipoMarcador.DataTextField = "ZDESCRIPCION";
       //     DrTipoMarcador.Items.Insert(0, new ListItem("Ninguno", "0"));            

       //     SQL = "SELECT ZID, ZDESCRIPCION  ";
       //     SQL += " FROM ZTIPO_MARCADOR ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];
       //     DrTipoMarcador.DataSource = dt;
       //     DrTipoMarcador.DataBind();

       //     SQL = "SELECT ZID, ZDESCRIPCION  ";
       //     SQL += " FROM ZOPCION ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrEstadoIni.Items.Clear();
       //     DrEstadoIni.DataValueField = "ZID";
       //     DrEstadoIni.DataTextField = "ZDESCRIPCION";

       //     DrArchivoIni.Items.Clear();
       //     DrArchivoIni.DataValueField = "ZID";
       //     DrArchivoIni.DataTextField = "ZDESCRIPCION";

       //     DrFlujoIni.Items.Clear();
       //     DrFlujoIni.DataValueField = "ZID";
       //     DrFlujoIni.DataTextField = "ZDESCRIPCION";

       //     DrfinPDF.Items.Clear();
       //     DrfinPDF.DataValueField = "ZID";
       //     DrfinPDF.DataTextField = "ZDESCRIPCION";

       //     DrPrinter.Items.Clear();
       //     DrPrinter.DataValueField = "ZID";
       //     DrPrinter.DataTextField = "ZDESCRIPCION";
            
       //     DrRutaAlternativa.Items.Clear();
       //     DrRutaAlternativa.DataValueField = "ZID";
       //     DrRutaAlternativa.DataTextField = "ZDESCRIPCION";

       //     DrCopia.Items.Clear();
       //     DrCopia.DataValueField = "ZID";
       //     DrCopia.DataTextField = "ZDESCRIPCION";

       //     Drroot.Items.Clear();
       //     Drroot.DataValueField = "ZID";
       //     Drroot.DataTextField = "ZDESCRIPCION";
       //     Drroot.Items.Insert(0, new ListItem("Ninguno", "0"));

       //     DrEjecucion.Items.Clear();
       //     DrEjecucion.DataValueField = "ZID";
       //     DrEjecucion.DataTextField = "ZDESCRIPCION";

       //     DrCopia.DataSource = dt;
       //     DrCopia.DataBind();
       //     DrCopia.SelectedIndex = -1;

       //     DrfinPDF.DataSource = dt;
       //     DrfinPDF.DataBind();
       //     DrfinPDF.SelectedIndex = -1;

       //     DrPrinter.DataSource = dt;
       //     DrPrinter.DataBind();
       //     DrPrinter.SelectedIndex = -1;

       //     DrRutaAlternativa.DataSource = dt;
       //     DrRutaAlternativa.DataBind();
       //     DrRutaAlternativa.SelectedIndex = -1;;

       //     DrEjecucion.DataSource = dt;
       //     DrEjecucion.DataBind();
       //     DrEjecucion.SelectedIndex = -1;

       //     SQL = "SELECT ZID, ZDESCRIPCION ";
       //     SQL += " FROM ZESTADOSFLUJO ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrEstadoIni.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrEstadoIni.DataSource = dt;
       //     DrEstadoIni.DataBind();
       //     DrEstadoIni.SelectedIndex = -1;

       //     SQL = "SELECT ZID, ZDESCRIPCION ";
       //     SQL += " FROM ZARCHIVOS ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrArchivoIni.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrArchivoIni.DataSource = dt;
       //     DrArchivoIni.DataBind();
       //     DrArchivoIni.SelectedIndex = -1;

       //     SQL = "SELECT ZID, ZDESCRIPCION ";
       //     SQL += " FROM ZFLUJOS ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];

       //     DrFlujoIni.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     DrFlujoIni.DataSource = dt;
       //     DrFlujoIni.DataBind();
       //     DrFlujoIni.SelectedIndex = -1;

       //     SQL = "SELECT ZID, ZDESCRIPCION ";
       //     SQL += " FROM ZCONFIGRUTASFILES ORDER BY ZID ";
       //     dt = Main.BuscaLote(SQL).Tables[0];


       //     Drroot.DataSource = dt;
       //     Drroot.DataBind();
       //     Drroot.SelectedIndex = -1;

       //     DrPagina.Items.Clear();
       //     DrPagina.Items.Insert(0, new ListItem("Ninguno", "0"));
       //     SQL = " SELECT ZID, ZDESCRIPCION FROM  ZCONFIGRUTASFILES ";
       //     DataTable dt1 = Main.BuscaLote(SQL).Tables[0];
       //     DrPagina.DataValueField = "ZID";
       //     DrPagina.DataTextField = "ZDESCRIPCION";
       //     DrPagina.DataSource = dt1;
       //     DrPagina.DataBind();
       // }
        // protected void btnRetroflujos_Click(object sender, EventArgs e)
        //{
        //    this.Session["Edicion"] = "0";
        //    this.Session["proceso"] = "";
        //    PanelFlujos.Visible = true;
        //    PanelProcesos.Visible = false;
        //    divAsigEstado.Visible = true;
        //}


        //protected void btnProfiles_Click(object sender, EventArgs e)
        //{
        //    if (DivPanelFlujo.Visible == false)
        //    {
        //        //Consulta ZPROFILES
        //        Label19.Text = "Flujo de Trabajo:";
        //        DivProfiles.Visible = false;
        //        DivPanelFlujo.Visible = true;
        //        DvProfile.Visible = false;
        //        DivSQL.Visible = false;
        //        EditaProfile(0);
        //    }
        //    else
        //    {

        //        //Relacion Archivo-campos----------------------------------------------------------------------------------------------
        //        string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_FLUJO ";
        //        SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();

        //        DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
        //        DataTable dtCampos = Main.CargaCampos().Tables[0];

        //        dtCampos = RelacionesArchivo(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos);
        //        //---------------------------------------------------------------------------------------------------------------------;
        //        DrcampoFiltro.DataValueField = "ZID";
        //        DrcampoFiltro.DataTextField = "ZTITULO";
        //        DrcampoFiltro.Items.Insert(0, new ListItem("Ninguno", "0"));
        //        DrcampoFiltro.DataSource = dtCampos;
        //        DrcampoFiltro.DataBind();

        //        drCampofiltroseleccion.DataValueField = "ZID";
        //        drCampofiltroseleccion.DataTextField = "ZTITULO";
        //        drCampofiltroseleccion.Items.Insert(0, new ListItem("Ninguno", "0"));
        //        drCampofiltroseleccion.DataSource = dtCampos;
        //        drCampofiltroseleccion.DataBind();

        //        SQL = " SELECT ZID, ZID_ARCHIVO, ZID_FLUJO, ZID_ESTADO, ZID_PAGINA, ZQUERY, ZID_PROCEDIMIENTO, ";
        //        SQL += " ZCAMPODOC, ZCAMPOFILTRO, ZFILTROCONDICION, ZDOCUMENTOS, ZDIRECTORIOS FROM  ZPROFILES ";
        //        SQL += " WHERE ZID_ARCHIVO = 1";// + DrArchivos.SelectedValue;
        //        SQL += " AND ZID_FLUJO = " + DrFlujos.SelectedValue;
        //        //SQL += " AND ZID_ESTADO = " + DrEstado.SelectedValue;
        //        DataTable dt = Main.BuscaLote(SQL).Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            TxtIdProfiles.Text = dr["ZID"].ToString();
        //            DrArchivoProfile.SelectedValue = dr["ZID_ARCHIVO"].ToString();
        //            DrFlujoProfile.SelectedValue = dr["ZID_FLUJO"].ToString();
        //            DrEstadoProfile.SelectedValue = dr["ZID_ESTADO"].ToString();
        //            DrPaginaProfile.SelectedValue = dr["ZID_PAGINA"].ToString();
        //            TxtQueryProfile.Text = dr["ZQUERY"].ToString().Replace("'", "\"");
        //            CommentSQL.Text = dr["ZQUERY"].ToString().Replace("'", "\"");
        //            DrProcedimientoProfile.SelectedValue = dr["ZID_PROCEDIMIENTO"].ToString();
        //            DrcampoFiltro.SelectedItem.Text = dr["ZCAMPODOC"].ToString();
        //            drCampofiltroseleccion.SelectedItem.Text = dr["ZCAMPOFILTRO"].ToString();
        //            //DrcampoFiltro.Text = dr["ZCAMPODOC"].ToString();
        //            //drCampofiltroseleccion.Text = dr["ZCAMPOFILTRO"].ToString();

        //            string[] Fields = System.Text.RegularExpressions.Regex.Split(dr["ZFILTROCONDICION"].ToString(), ";");
        //            ListBox3.Items.Clear();
        //            ListBox4.Items.Clear();
        //            foreach (string Field in Fields)
        //            {
        //                foreach (DataRow MM in dtCampos.Rows)
        //                {
        //                    if (Field == MM["ZTITULO"].ToString())
        //                    {
        //                        ListBox3.Items.Add(new ListasID(Field, Convert.ToInt32(MM["ZID"].ToString())).ToString());
        //                        ListBox4.Items.Add(new ListasID(MM["ZID"].ToString(), Convert.ToInt32(MM["ZID"].ToString())).ToString());
        //                        break;
        //                    }
        //                }
        //            }

        //            TxtDocsProfile.Text = dr["ZDOCUMENTOS"].ToString();
        //            TxtDirProfile.Text = dr["ZDIRECTORIOS"].ToString();
        //            break;
        //        }
        //        Label19.Text = "Perfil del Flujo de Trabajo:";
        //        //DivCampoIzq.Visible = false;
        //        DivProfiles.Visible = true;
        //        DivPanelFlujo.Visible = false;
        //        DvProfile.Visible = true;
        //        DivSQL.Visible = false;
        //    }
        //}

        //protected void btnCondicionProfile_Click(object sender, EventArgs e)
        //{
        //    if (DivSQL.Visible == true)
        //    {
        //        DvProfile.Visible = true;
        //        DivSQL.Visible = false;
        //    }
        //    else
        //    {
        //        DvProfile.Visible = false;
        //        DivSQL.Visible = true;
        //    }
        //}
        protected void btnCondicionTemplate_Click(object sender, EventArgs e)
        {
            //DivTxtQuery.Visible = false;
            //DivTemplate.Visible = false;
            //DivEstados.Visible = true;
        }

        protected void btnguardaTemplate_Click(object sender, EventArgs e)
        {

        }
        protected void btnCondicionSQL_Click(object sender, EventArgs e)
        {
            //DivTemplate.Visible = false;
            //DivTxtQuery.Visible = true;
        }
        protected void btnCondicionNOSQL_Click(object sender, EventArgs e)
        {
            //DivTemplate.Visible = true;
            //DivTxtQuery.Visible = false;
        }

        //protected void btnCopiaTemplate_Click(object sender, EventArgs e)
        //{
        //    DivGralTemplates.Visible = true;
        //    PanelProcesos.Visible = false;
        //    PanelFlujos.Visible = false;
        //}
        
        //protected void btnRetroProcesos_Click(object sender, EventArgs e)
        //{
        //    DivGralTemplates.Visible = false;
        //    PanelProcesos.Visible = true;
        //    PanelFlujos.Visible = false;
        //}

        protected void btnMuestraCondicion_Click(object sender, EventArgs e)
        {
            //if(DivTemplate.Visible == true)
            //{
            //    DivEstados.Visible = true;
            //    DivTxtQuery.Visible = false;
            //    DivTemplate.Visible = false;
            //}
            //else
            //{
            //    DivEstados.Visible = false;
            //    DivTemplate.Visible = false;
            //    DivTxtQuery.Visible = false;
            //    if (this.Session["EstadoCondicion"].ToString() == "" || this.Session["EstadoCondicion"].ToString() == null)
            //    {
            //        DivTxtQuery.Visible = true;
            //    }
            //    else
            //    {
            //        DivTemplate.Visible = true;
            //    }
            //}
        }

        //private void LimpioPLantilla()
        //{
        //    TxtNPlantilla.Text = "";
        //    TxtDPlantilla.Text = "";
        //    TxtRutaEPlantilla.Text = "";
        //    TxtRutaSPlantilla.Text = "";
        //    DrCopia.SelectedIndex = -1;
        //    DrfinPDF.SelectedIndex = -1;
        //    DrRutaAlternativa.SelectedIndex = -1;
        //    DrMarcadoresPlantillas.SelectedIndex = -1;
        //    ListPlantMarca.Items.Clear();
        //    ListPlantMarcaID.Items.Clear();
        //    DrPrinter.SelectedIndex = -1;
        //}

        //private void EditaPlantilla(int Abierto)
        //{
        //    if (Abierto == 0)
        //    {
        //        TxtNPlantilla.Enabled = false;
        //        TxtDPlantilla.Enabled = false;
        //        TxtRutaEPlantilla.Enabled = false;
        //        TxtRutaSPlantilla.Enabled = false;
        //        DrCopia.Enabled = false;
        //        DrfinPDF.Enabled = false;
        //        DrRutaAlternativa.Enabled = false;
        //        DrMarcadoresPlantillas.Enabled = false;
        //        DrPrinter.Enabled = false;
        //        ListPlantMarca.BackColor = Color.FromName("#ffffff");
        //        ListPlantMarcaID.BackColor = Color.FromName("#ffffff");
        //        TxtNPlantilla.BackColor = Color.FromName("#ffffff");
        //        TxtDPlantilla.BackColor = Color.FromName("#ffffff");
        //        TxtRutaEPlantilla.BackColor = Color.FromName("#ffffff");
        //        TxtRutaSPlantilla.BackColor = Color.FromName("#ffffff");
        //        DrCopia.BackColor = Color.FromName("#ffffff");
        //        DrfinPDF.BackColor = Color.FromName("#ffffff");
        //        DrRutaAlternativa.BackColor = Color.FromName("#ffffff");
        //        DrMarcadoresPlantillas.BackColor = Color.FromName("#ffffff");
        //        DrPrinter.BackColor = Color.FromName("#ffffff");
        //        Button11.Enabled = false;
        //        Button14.Enabled = false;
        //        Button26.Enabled = false;

        //    }
        //    else
        //    {
        //        TxtNPlantilla.Enabled = true;
        //        TxtDPlantilla.Enabled = true;
        //        TxtRutaEPlantilla.Enabled = true;
        //        TxtRutaSPlantilla.Enabled = true;
        //        DrCopia.Enabled = true;
        //        DrfinPDF.Enabled = true;
        //        DrRutaAlternativa.Enabled = true;
        //        DrPrinter.Enabled = true;
        //        DrMarcadoresPlantillas.Enabled = true;
        //        ListPlantMarca.BackColor = Color.FromName("#bdecb6");
        //        ListPlantMarcaID.BackColor = Color.FromName("#bdecb6");
        //        TxtNPlantilla.BackColor = Color.FromName("#bdecb6");
        //        TxtDPlantilla.BackColor = Color.FromName("#bdecb6");
        //        TxtRutaEPlantilla.BackColor = Color.FromName("#bdecb6");
        //        TxtRutaSPlantilla.BackColor = Color.FromName("#bdecb6");
        //        DrCopia.BackColor = Color.FromName("#bdecb6");
        //        DrfinPDF.BackColor = Color.FromName("#bdecb6");
        //        DrRutaAlternativa.BackColor = Color.FromName("#bdecb6");
        //        DrMarcadoresPlantillas.BackColor = Color.FromName("#bdecb6");
        //        DrPrinter.BackColor = Color.FromName("#bdecb6");
        //        Button11.Enabled = true;
        //        Button14.Enabled = true;
        //        Button26.Enabled = true;
        //    }
        //    //EditaProceso(0);
        //    //EditaMarcador(0);
        //}

        //private void LimpioProceso()
        //{
        //    TextIdProceso.Text = "";
        //    TxtDescProceso.Text = "";
        //    DrEstadoIni.SelectedIndex = -1;
        //    DrArchivoIni.SelectedIndex = -1;
        //    DrFlujoIni.SelectedIndex = -1;
        //    DrEjecucion.SelectedIndex = -1;
        //    DrProcesoPlantilla.SelectedIndex = -1;
        //    ListPlantillas.Items.Clear();
        //    ListPlantillasID.Items.Clear();
        //    TxtZQuery.Text = "";
        //}
        private void Cancelado()
        {
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            //EditaPlantilla(0);
            //EditaProceso(0);
            //EditaMarcador(0);
            //divTarea.Visible = false;
            //divNePlantilla.Visible = false;

        }

        //private void EditaProceso(int Abierto)
        //{
        //    if (Abierto == 0)
        //    {
        //        TextIdProceso.Enabled = false;
        //        TxtDescProceso.Enabled = false;
        //        DrEstadoIni.Enabled = false;
        //        DrArchivoIni.Enabled = false;
        //        DrFlujoIni.Enabled = false;
        //        DrEjecucion.Enabled = false;
        //        DrProcesoPlantilla.Enabled = false;
        //        ListPlantillas.BackColor = Color.FromName("#ffffff");
        //        ListPlantillasID.BackColor = Color.FromName("#ffffff");
        //        TxtZQuery.BackColor = Color.FromName("#ffffff");
        //        TextIdProceso.BackColor = Color.FromName("#ffffff");
        //        TxtDescProceso.BackColor = Color.FromName("#ffffff");
        //        DrEstadoIni.BackColor = Color.FromName("#ffffff");
        //        DrArchivoIni.BackColor = Color.FromName("#ffffff");
        //        DrFlujoIni.BackColor = Color.FromName("#ffffff");
        //        DrEjecucion.BackColor = Color.FromName("#ffffff");
        //        DrProcesoPlantilla.BackColor = Color.FromName("#ffffff");
        //        Button12.Enabled = false;
        //        Button13.Enabled = false;
        //    }
        //    else
        //    {
        //        TextIdProceso.Enabled = true;
        //        TxtDescProceso.Enabled = true;
        //        DrEstadoIni.Enabled = true;
        //        DrArchivoIni.Enabled = true;
        //        DrFlujoIni.Enabled = true;
        //        DrEjecucion.Enabled = true;
        //        DrProcesoPlantilla.Enabled = true;
        //        ListPlantillas.BackColor = Color.FromName("#bdecb6");
        //        ListPlantillasID.BackColor = Color.FromName("#bdecb6");
        //        TxtZQuery.BackColor = Color.FromName("#bdecb6");
        //        TextIdProceso.BackColor = Color.FromName("#bdecb6");
        //        TxtDescProceso.BackColor = Color.FromName("#bdecb6");
        //        DrEstadoIni.BackColor = Color.FromName("#bdecb6");
        //        DrArchivoIni.BackColor = Color.FromName("#bdecb6");
        //        DrFlujoIni.BackColor = Color.FromName("#bdecb6");
        //        DrEjecucion.BackColor = Color.FromName("#bdecb6");
        //        DrProcesoPlantilla.BackColor = Color.FromName("#bdecb6");
        //        Button12.Enabled = true;
        //        Button13.Enabled = true;
        //    }
        //    //EditaPlantilla(0);
        //    //EditaMarcador(0);
        //}


        //protected void btnCopiaProceso_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    //LimpioProceso();
        //    EditaProceso(1);
        //    this.Session["Edicion"] = "1";
        //    this.Session["proceso"] = "proceso";

        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    //maximo id
        //    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPROCESOS", dbParams));
        //    TextIdProceso.Text = (MiID + 1).ToString();
        //    TxtDescProceso.Text = "Copia de " + TxtDescProceso.Text;

        //}

        //protected void btnCreaProceso_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0") 
        //    {
        //        Cancelado();
        //        return; 
        //    }
        //    LimpioProceso();
        //    EditaProceso(1);
        //    this.Session["Edicion"] = "1";
        //    this.Session["proceso"] = "proceso";

        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    //maximo id
        //    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPROCESOS", dbParams));
        //    TextIdProceso.Text = (MiID + 1).ToString();

        //}
        //protected void btnEditProceso_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    EditaProceso(1);
        //    this.Session["Edicion"] = "4";
        //    this.Session["proceso"] = "proceso";
        //}
        //protected void btnDeleteProceso_Click(object sender, EventArgs e)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[0];

        //    if (this.Session["Edicion"].ToString() != "4")
        //    {
        //        string Column = "DELETE FROM ZPROCESOS WHERE  ZID = " + TextIdProceso.Text + " ";
        //        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
        //        LimpioProceso();
        //        EditaProceso(0);
        //        this.Session["Edicion"] = "0";
        //        this.Session["proceso"] = "";
        //        btnMuestraProcesos_Click(sender, e);
        //    }
        //}
        protected void btnAllProceso_Click(object sender, EventArgs e)
        {

        }

        //protected void btnCopiaPlantilla_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    //LimpioPLantilla();
        //    EditaPlantilla(1);
        //    this.Session["Edicion"] = "1";
        //    this.Session["proceso"] = "plantilla";

        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    //maximo id
        //    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPLANTILLAS", dbParams));
        //    TxtNPlantilla.Text = (MiID + 1).ToString();
        //    TxtDPlantilla.Text = "Copia de " + TxtDPlantilla.Text;
        //}
        

        //protected void btnCreaPlantilla_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    LimpioPLantilla();
        //    EditaPlantilla(1);
        //    this.Session["Edicion"] = "1";
        //    this.Session["proceso"] = "plantilla";
        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    //maximo id
        //    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZPLANTILLAS", dbParams));
        //    TxtNPlantilla.Text = (MiID + 1).ToString();
        //}
        //protected void btnEditPlantilla_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    EditaPlantilla(1);
        //    this.Session["Edicion"] = "4";
        //    this.Session["proceso"] = "plantilla";
        //}
        //protected void btnDeletePlantilla_Click(object sender, EventArgs e)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[0];

        //    if (this.Session["Edicion"].ToString() != "4")
        //    {
        //        string Column = "DELETE FROM ZPLANTILLAS WHERE  ZID = " + TxtNPlantilla.Text + " ";
        //        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
        //        LimpioPLantilla();
        //        EditaPlantilla(0);
        //        this.Session["Edicion"] = "0";
        //        this.Session["proceso"] = "";
        //        btnMuestraProcesos_Click(sender, e);
        //    }
        //}
        protected void btnAllPlantilla_Click(object sender, EventArgs e)
        {

        }




    

        

        protected void DrSello_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Drroot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         
  
        protected void DrUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        //protected void DrPagina_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaMenu(1);
        //    }

        //}

  

        protected void drProfilesAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProfile(1);
            }
        }


        protected void dlConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
            //ListBoxArchivo.Items.Add(new ListasID(DrConexion.SelectedItem.Text, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            //ListBoxArchivoID.Items.Add(new ListasID(DrConexion.SelectedItem.Value, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaMenu(1);
            }

        }
            //protected void ListBoxArchivo_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            //    ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            //}

            protected void ListTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListTemplateID.Items.RemoveAt(ListTemplate.SelectedIndex);
            //ListTemplate.Items.RemoveAt(ListTemplate.SelectedIndex);
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

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }

            }
            //DrArchivos.Enabled = true;
            //DrArchivos.Items.Clear();

            //DrArchivos.DataValueField = "ZID";
            //DrArchivos.DataTextField = "ZDESCRIPCION";


            //DrArchivos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            this.Session["Archivo"] = dt;
            //DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
            //DrArchivos.DataBind();

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
                EditaMenu(1);
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
                ListBox1.BackColor = Color.FromName("#bdecb6");
            }

            //DataTable dt = Main.CargaEstadosFlujos("0",ListBox1ID.SelectedItem.Value).Tables[0];

            this.Session["idestado"] = ListBox1ID.SelectedItem.Value;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["ZID"].ToString() == ListBox1ID.SelectedItem.Value)
            //    {
            //        TxtidEstado.Text = dr["ZID"].ToString();
            //        TxtEstado.Text = dr["ZNOMBRE"].ToString();
            //        //TxtCondicion.Text = dr["ZIMG"].ToString();
            //        TxtCondicionQR.Text = dr["ZCONDICION"].ToString();
            //        string miro = dr["ZPREVIUS"].ToString();

            //        //if (dr["ZPREVIUSVIEW"].ToString() == "1")
            //        //{
            //        //    Imgatras.Visible = true;
            //        //    ImgatrasC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    Imgatras.Visible = false;
            //        //    ImgatrasC.Visible = true;
            //        //}
            //        //if (dr["ZNEXTVIEW"].ToString() == "1")
            //        //{
            //        //    ImgNext.Visible = true;
            //        //    ImgNextC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    ImgNext.Visible = false;
            //        //    ImgNextC.Visible = true;
            //        //}
            //        //if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
            //        //{
            //        //    ImgAlter.Visible = true;
            //        //    ImgAlterC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    ImgAlter.Visible = false;
            //        //    ImgAlterC.Visible = true;
            //        //}
            //        //if (dr["ZENDVIEW"].ToString() == "1")
            //        //{
            //        //    imgFin.Visible = true;
            //        //    imgFinC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    imgFin.Visible = false;
            //        //    imgFinC.Visible = true;
            //        //}

            //        //for (int i = 0; i <= Dratras.Items.Count - 1; i++)
            //        //{
            //        //    if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
            //        //    {
            //        //        Dratras.SelectedValue = dr["ZPREVIUS"].ToString();
            //        //        break;
            //        //    }
            //        //}

            //        for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
            //        {
            //            if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
            //            {
            //                Drsiguiente.SelectedValue = dr["ZNEXT"].ToString();
            //                break;
            //            }
            //        }

            //        //for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
            //        //{
            //        //    if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
            //        //    {
            //        //        Dralternativo.SelectedValue = dr["ZALTERNATIVE"].ToString();
            //        //        break;
            //        //    }
            //        //}

            //        //for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
            //        //{
            //        //    if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
            //        //    {
            //        //        Drfinal.SelectedValue = dr["ZEND"].ToString();
            //        //        break;
            //        //    }
            //        //}
            //    }
            //}
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
                //si es llamado desde otra página
                for (int i = 0; i < DrFlujos.Items.Count; i++)
                {
                    if (DrFlujos.Items[i].Value == a)
                    {
                        DrFlujos.SelectedIndex = i;
                        DrFlujos_SelectedIndexChanged(null, null);
                        break;
                    }
                }
            }
            else
            {
                //EditaMenu(1);
            }
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.Session["Cargando"].ToString() == "1") 
            { 
                return; 
            }
            this.Session["Cargando"] = "1";
            int Index = ListBox2.SelectedIndex;
            ListBox2ID.SelectedIndex = ListBox2.SelectedIndex;
            ListBoxNivel.SelectedIndex = ListBox2.SelectedIndex;
            this.Session["indice"] = Index;
            DrNiveles.SelectedIndex = 0;
            //ListBox2Col.SelectedIndex = ListBox2.SelectedIndex;
            //ListKeys.SelectedIndex = ListBox2.SelectedIndex;
            //string a = ListBox2ID.SelectedValue;
            //string b = ListKeys.SelectedValue;

            //for (int i = 0; i <= DrEstado.Items.Count - 1; i++)
            //{
            //    if (DrEstado.Items[i].Value == ListBox2ID.SelectedItem.Value)
            //    {
            //        DrEstado.SelectedIndex = i;
            //        break;
            //    }
            //}

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                ListBox2.BackColor = Color.FromName("#bdecb6");
            }

            //DataTable dt = Main.BuscaEstado(Convert.ToInt32(ListBox2ID.SelectedItem.Value)).Tables[0];

            this.Session["idestado"] = ListBox2ID.SelectedItem.Value;
            //LbNivel.Text = ListBoxNivel.SelectedItem.Text;

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["ZID"].ToString() == ListBox2ID.SelectedItem.Value)
            //    {
            //        TxtidEstado.Text = dr["ZID"].ToString();
            //        TxtEstado.Text = dr["ZNOMBRE"].ToString();
            //        //TxtCondicion.Text = dr["ZIMG"].ToString();
            //        TxtCondicionQR.Text = dr["ZCONDICION"].ToString();
            //        string miro = dr["ZPREVIUS"].ToString();

            //        //if (dr["ZPREVIUSVIEW"].ToString() == "1")
            //        //{
            //        //    Imgatras.Visible = true;
            //        //    ImgatrasC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    Imgatras.Visible = false;
            //        //    ImgatrasC.Visible = true;
            //        //}
            //        //if (dr["ZNEXTVIEW"].ToString() == "1")
            //        //{
            //        //    ImgNext.Visible = true;
            //        //    ImgNextC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    ImgNext.Visible = false;
            //        //    ImgNextC.Visible = true;
            //        //}
            //        //if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
            //        //{
            //        //    ImgAlter.Visible = true;
            //        //    ImgAlterC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    ImgAlter.Visible = false;
            //        //    ImgAlterC.Visible = true;
            //        //}
            //        //if (dr["ZENDVIEW"].ToString() == "1")
            //        //{
            //        //    imgFin.Visible = true;
            //        //    imgFinC.Visible = false;
            //        //}
            //        //else
            //        //{
            //        //    imgFin.Visible = false;
            //        //    imgFinC.Visible = true;
            //        //}

            //        //for (int i = 0; i <= Dratras.Items.Count - 1; i++)
            //        //{
            //        //    if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
            //        //    {
            //        //        Dratras.SelectedValue = dr["ZPREVIUS"].ToString();
            //        //        break;
            //        //    }
            //        //}

            //        for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
            //        {
            //            if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
            //            {
            //                Drsiguiente.SelectedValue = dr["ZNEXT"].ToString();
            //                break;
            //            }
            //        }

            //        //for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
            //        //{
            //        //    if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
            //        //    {
            //        //        Dralternativo.SelectedValue = dr["ZALTERNATIVE"].ToString();
            //        //        break;
            //        //    }
            //        //}

            //        //for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
            //        //{
            //        //    if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
            //        //    {
            //        //        Drfinal.SelectedValue = dr["ZEND"].ToString();
            //        //        break;
            //        //    }
            //        //}
            //    }
            //}
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else 
            {
                //EditaMenu(1);
                EditaGrupo(1);
            }

            this.Session["Cargando"] = "0";
            
        }
        protected void ListBoxNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Session["Cargando"].ToString() == "1")
                {
                    return;
                }
                //this.Session["Cargando"] = "1";
                //ListBox2.SelectedIndex = ListBoxNivel.SelectedIndex;
                //ListBox2ID.SelectedIndex = ListBoxNivel.SelectedIndex;
                //int Index = ListBoxNivel.SelectedIndex;
                //int Index = Convert.ToInt32(this.Session["indice"].ToString());
                //this.Session["indice"] = Index;

                //int Index = Convert.ToInt32(this.Session["indice"].ToString());
                //DrNiveles.SelectedIndex = 0;
                //LbNivel.Text = ListBoxNivel.Items[Index].Value;
                //ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
                //ListBox2.SelectedIndex = ListBoxNivel.SelectedIndex;
                //ListKeys.SelectedIndex = ListBoxNivel.SelectedIndex;
                //this.Session["indice"] = Index;

                //string a = ListBoxNivel.SelectedValue;
                //string b = ListBoxNivel.SelectedValue;
                //this.Session["Cargando"] = "0";
                //if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
                //{
                //}
                //else
                //{
                //    EditaGrupo(1);
                //}
            }
            catch
            {

            }
        }


        protected void ListBox2ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Cargando"].ToString() == "1") 
            { 
                return; 
            }
            this.Session["Cargando"] = "1";

            int Index = ListBox2ID.SelectedIndex;
            //ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
            ListBox2.SelectedIndex = ListBox2ID.SelectedIndex;
            //ListKeys.SelectedIndex = ListBox2ID.SelectedIndex;
            ListBoxNivel.SelectedIndex = ListBox2ID.SelectedIndex;

            this.Session["indice"] = Index;
            string a = ListBox2ID.SelectedValue;
            string b = ListBoxNivel.SelectedValue;
            //DrNiveles.Items.IndexOf()

            //LbNivel.Text = DrNiveles.Items[Index].Value;
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
            this.Session["Cargando"] = "0";
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaGrupo(1);
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
            ListKeys.Items.Clear();


#pragma warning disable CS0219 // La variable 'visto' está asignada pero su valor nunca se usa
            int visto = 0;
#pragma warning restore CS0219 // La variable 'visto' está asignada pero su valor nunca se usa

            DataTable dtt = new DataTable("Tabla");
            DataTable dto = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZDESCRIPCION");

            dto.Columns.Add("ZID");
            dto.Columns.Add("ZDESCRIPCION");

#pragma warning disable CS0168 // La variable 'drr' se ha declarado pero nunca se usa
            DataRow drr;
#pragma warning restore CS0168 // La variable 'drr' se ha declarado pero nunca se usa

#pragma warning disable CS0219 // La variable 'i' está asignada pero su valor nunca se usa
            int i = 0;
#pragma warning restore CS0219 // La variable 'i' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'Key' está asignada pero su valor nunca se usa
            string Key = "";
#pragma warning restore CS0219 // La variable 'Key' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'x' está asignada pero su valor nunca se usa
            string x = "";
#pragma warning restore CS0219 // La variable 'x' está asignada pero su valor nunca se usa

            ////Si no hay Flujos
            //if (dtFlujos.Rows.Count == 0 || sortExpression == "")
            //{
            //    //Si hay Estados asociados a es Flujo
            //    foreach (DataRow fila in dtEstadosFlujos.Rows)//Campos
            //    {
            //        drr = dtt.NewRow();
            //        drr[0] = Convert.ToInt32(fila["ZID"].ToString());
            //        drr[1] = fila["ZDESCRIPCION"].ToString();
            //        dtt.Rows.Add(drr);

            //        ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //        ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //        ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //    }
            //    drr = dto.NewRow();
            //    drr[0] = Convert.ToInt32(0);
            //    drr[1] = "";
            //    dto.Rows.Add(drr);
            //    //ListBox1.DataSource = dtt;
            //    //ListBox1ID.DataBind();
            //}
            //else
            //{
            //    //si tiene Relacion archivos Flujos Estados
            //    foreach (DataRow dr in dtEstadosFlujos.Rows)
            //    {
            //        //Key = dr["ZKEY"].ToString();
            //        visto = 0;
            //        foreach (DataRow fila in dtFlujosEstados.Rows)//Esdos de Flujo
            //        {
            //            if (fila["ZID_ESTADO"].ToString() == dr["ZID"].ToString())
            //            {
            //                drr = dto.NewRow();
            //                drr[0] = Convert.ToInt32(fila["ZID_ESTADO"].ToString());
            //                drr[1] = fila["ZDESCRIPCION"].ToString();
            //                dto.Rows.Add(drr);

            //                ListBox2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());
            //                ListBox2ID.Items.Add(new ListasID(fila["ZID_ESTADO"].ToString(), Convert.ToInt32(fila["ZID_ESTADO"].ToString())).ToString());                        
            //                visto = 1;
            //                break;
            //            }
            //        }
            //    }
            //}
        }


  
        private void EditaTemplate(int Abierto)
        {
            if (Abierto == 0)
            {
                //Hay que cambiar controles
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                DrConexion.Enabled = false;
                //ListBoxArchivo.Enabled = false;
                BtguardaFlujo.Enabled = false;
                BtCancelFlujo.Enabled = false;
                BtGuardaRel.Enabled = false;
                BtCancelRel.Enabled = false;
                TxtNombre.BackColor = Color.FromName("#ffffff");
                TxtDescripcion.BackColor = Color.FromName("#ffffff");
                DrConexion.BackColor = Color.FromName("#ffffff");
                //ListBoxArchivo.BackColor = Color.FromName("#ffffff");
                //DrPagina.Enabled = false;
                //DrPagina.BackColor = Color.FromName("#ffffff");

                ListBox1.BackColor = Color.FromName("#ffffff");
                ListBox1ID.BackColor = Color.FromName("#ffffff");
                ListBox2.BackColor = Color.FromName("#ffffff");
                ListBox2ID.BackColor = Color.FromName("#ffffff");

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
                //ListBoxArchivo.Enabled = true;
                BtguardaFlujo.Enabled = true;
                BtCancelFlujo.Enabled = true;
                BtGuardaRel.Enabled = true;
                BtCancelRel.Enabled = true;
                TxtNombre.BackColor = Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = Color.FromName("#bdecb6");
                DrConexion.BackColor = Color.FromName("#bdecb6");
                //ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
                //DrPagina.BackColor = Color.FromName("#bdecb6");
                //DrPagina.Enabled = true;

                ListBox1.BackColor = Color.FromName("#bdecb6");
                ListBox1ID.BackColor = Color.FromName("#bdecb6");
                ListBox2.BackColor = Color.FromName("#bdecb6");
                ListBox2ID.BackColor = Color.FromName("#bdecb6");

                Button1.Enabled = true;
                Button2.Enabled = true;
                Button4.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
            }
        }
        private void EditaMenu(int Abierto)
        {
            if(Abierto == 0)
            {
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                TxtDescripcion.Enabled = false;
                TxtDescripcion.Enabled = false;

                ImageCopiaFlujo.Enabled = false;
                IbtCreaFlujo.Enabled = true;
                ibtEditFlujo.Enabled = false;
                ibtDeleteFlujo.Enabled = false;
                ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30Black.png";
                IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30.png";
                ibtEditFlujo.ImageUrl = "~/images/editdoc35X30Black.png";
                ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30Black.png";


                TxtPagW.Enabled = false;
                DrConexion.Enabled = false;
                //ListBoxArchivo.Enabled = false;
                BtguardaFlujo.Enabled = false;
                BtCancelFlujo.Enabled = false;
                BtGuardaRel.Enabled = false;
                BtCancelRel.Enabled = false;
                TextID.BackColor = Color.FromName("#ffffff");
                TxtNombre.BackColor = Color.FromName("#ffffff");
                TxtDescripcion.BackColor = Color.FromName("#ffffff");
                DrConexion.BackColor = Color.FromName("#ffffff");
                TxtPagW.BackColor = Color.FromName("#ffffff");
                DrNiveles.Enabled = false;

                //ListBoxArchivo.BackColor = Color.FromName("#ffffff");
                //DrPagina.Enabled = false;
                //DrPagina.BackColor = Color.FromName("#ffffff");

                //ListBox1.BackColor = Color.FromName("#ffffff");
                //ListBox1ID.BackColor = Color.FromName("#ffffff");
                //ListBox2.BackColor = Color.FromName("#ffffff");
                //ListBox2ID.BackColor = Color.FromName("#ffffff");

                //Button1.Enabled = false;
                //Button2.Enabled = false;
                //Button4.Enabled = false;
                //Button6.Enabled = false;
                //Button7.Enabled = false;
            }
            else
            {
                ImageCopiaFlujo.Enabled = false;
                IbtCreaFlujo.Enabled = false;
                ibtEditFlujo.Enabled = false;
                ibtDeleteFlujo.Enabled = true;
                ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30Black.png";
                IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30Black.png";
                ibtEditFlujo.ImageUrl = "~/images/editdoc35X30Black.png";
                ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30.png";

                TxtNombre.Enabled = true;
                TxtDescripcion.Enabled = true;
                DrConexion.Enabled = true;
                TxtPagW.Enabled = true;
                //ListBoxArchivo.Enabled = true;
                BtguardaFlujo.Enabled = true;
                BtCancelFlujo.Enabled = true;
                BtGuardaRel.Enabled = true;
                BtCancelRel.Enabled = true;
                TextID.BackColor = Color.FromName("#bdecb6");
                TxtNombre.BackColor = Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = Color.FromName("#bdecb6");
                DrConexion.BackColor = Color.FromName("#bdecb6");
                TxtPagW.BackColor = Color.FromName("#bdecb6");
                DrNiveles.Enabled = true;
                //ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
                //DrPagina.BackColor = Color.FromName("#bdecb6");
                //DrPagina.Enabled = true;

                //ListBox1.BackColor = Color.FromName("#bdecb6");
                //ListBox1ID.BackColor = Color.FromName("#bdecb6");
                //ListBox2.BackColor = Color.FromName("#bdecb6");
                //ListBox2ID.BackColor = Color.FromName("#bdecb6");

                //Button1.Enabled = true;
                //Button2.Enabled = true;
                //Button4.Enabled = true;
                //Button6.Enabled = true;
                //Button7.Enabled = true;
            }
        }

        private void LimpioFlujo()
        {
            TextID.Text = "";
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            DrConexion.SelectedIndex = 0;
            TxtPagW.Text = "";
            //DrConexion.Text = "";
            //ListBoxArchivo.Items.Clear();
            //ListBoxArchivoID.Items.Clear();
        }

        
        //private void EditaMenu(int Abierto)
        //{
        //    if (Abierto == 0)
        //    {
        //        //Cambia botones y carga zgrupousuarios

        //        Drsiguiente.Enabled = false;
        //        Drsiguiente.BackColor = Color.FromName("#ffffff");
        //    }
        //    else
        //    {
        //        Drsiguiente.Enabled = true;
        //        Drsiguiente.BackColor = Color.FromName("#bdecb6");
        //    }
        //}

        private void EditaGrupo(int Abierto)
        {
            //El Grupo
            if (Abierto == 0)
            {
                //Drsiguiente.Enabled = false;
                Drsiguiente.BackColor = Color.FromName("#ffffff");
                BtGuardaEstado.Enabled = false;
                ibtEditEstado.Enabled = true;
                ibtDeleteEstado.Enabled = false;
                DrFlujos.Enabled = true;
                //ImageCopiaFlujo.Enabled = true;
                //IbtCreaFlujo.Enabled = true;
                //ibtEditFlujo.Enabled = true;
                //ibtDeleteFlujo.Enabled = true;
                ibtEditEstado.ImageUrl = "~/images/editdoc35X30.png";
                ibtDeleteEstado.ImageUrl = "~/images/elimina35X30Black.png";
                //ImageCopiaFlujo.Enabled = false;
                //IbtCreaFlujo.Enabled = false;
                //ibtEditFlujo.Enabled = false;
                //ibtDeleteFlujo.Enabled = false;
                //ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30Black.png";
                //IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30Black.png";
                //ibtEditFlujo.ImageUrl = "~/images/editdoc35X30Black.png";
                //ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30Black.png";

                //BtCancelEstado.Visible = false;
                //BtCancelEstado.Visible = true;
                BtCancelEstado.Enabled = false;
                ListBox1.BackColor = Color.FromName("#ffffff");
                ListBox1ID.BackColor = Color.FromName("#ffffff");
                ListBox2.BackColor = Color.FromName("#ffffff");
                ListBox2ID.BackColor = Color.FromName("#ffffff");
                ListBoxNivel.BackColor = Color.FromName("#ffffff");
                DivCampoDer.Attributes["style"] = "background-color: #ffffff";
                DivPanelFlujo.Attributes["style"] = "background-color: #ffffff";
                ListBoxNivel.Enabled = false;
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                DrNiveles.Enabled = false;
                Drsiguiente.Enabled = true;
            }
            else
            {
                ibtEditEstado.Enabled = false;
                ibtDeleteEstado.Enabled = true;
                ibtEditEstado.ImageUrl = "~/images/editdoc35X30Black.png";
                ibtDeleteEstado.ImageUrl = "~/images/elimina35X30.png";

                //Drsiguiente.Enabled = true;
                //ImageCopiaFlujo.Enabled = true;
                //IbtCreaFlujo.Enabled = true;
                //ibtEditFlujo.Enabled = true;
                //ibtDeleteFlujo.Enabled = false;
                //ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30.png";
                //IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30.png";
                //ibtEditFlujo.ImageUrl = "~/images/editdoc35X30.png";
                //ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30.png";

                Drsiguiente.BackColor = Color.FromName("#bdecb6");
                BtGuardaEstado.Enabled = true;
                ibtEditEstado.Enabled = false;
                ibtDeleteEstado.Enabled = true;
                //BtCancelEstado.Visible = true;
                BtCancelEstado.Enabled = true;
                ListBox1.BackColor = Color.FromName("#bdecb6");
                ListBox1ID.BackColor = Color.FromName("#bdecb6");
                ListBox2.BackColor = Color.FromName("#bdecb6");
                ListBox2ID.BackColor = Color.FromName("#bdecb6");
                ListBoxNivel.BackColor = Color.FromName("#bdecb6");
                DivCampoDer.Attributes["style"] = "background-color: #bdecb6";
                DivPanelFlujo.Attributes["style"] = "background-color: #ecdfb6";
                ListBoxNivel.Enabled = true;
                Button1.Enabled = true;
                Button2.Enabled = true;
                Button4.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                DrNiveles.Enabled = true;
                Drsiguiente.Enabled = false;

                DrFlujos.Enabled = false;
                ImageCopiaFlujo.Enabled = false;
                IbtCreaFlujo.Enabled = true;
                IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30.png";
                ibtEditFlujo.Enabled = false;
                ibtDeleteFlujo.Enabled = false;

            }
            LbAsociado.Text = ListBox2.Items.Count.ToString();
        }
   
        private void LimpioEstado()
        {
            //TxtidEstado.Text = "";
            //TxtEstado.Text = "";
            //Dratras.SelectedIndex = -1;
            Drsiguiente.SelectedIndex = -1;
            //Dralternativo.SelectedIndex = -1;
            //Drfinal.SelectedIndex = -1;
            //TxtCondicion.Text = "";
        }
        protected void btnGuardarRelacion_Click(object sender, EventArgs e)
        {
            //Guardar Relación


            EditaMenu(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }

        protected void btnCancelarRelacion_Click(object sender, EventArgs e)
        {

            EditaMenu(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }

 
        protected void btnCancelarFlujo_Click(object sender, EventArgs e)
        {
            EditaMenu(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";

        }
        protected void btnCancelarEstado_Click(object sender, EventArgs e)
        {
             Cancelado();
            EditaGrupo(0);
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            drsiguiente_SelectedIndexChanged(null, null);

            //Actualiza_Archivos();
            //drsiguiente_SelectedIndexChanged(null, null);

        }

    
        protected void btnGuardarFlujo_Click(object sender, EventArgs e)
        {
            //IbtAllFlujo.ImageUrl = "~/Images/allwhite.png";
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "2")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea Menu
                    string Column = "INSERT INTO ZMENU (ZDESCRIPCION, ZTITULO, ZROOT, ZESTADO, ZPAGINA) ";
                    string ColumnVal = " VALUES('" + TxtDescripcion.Text + "','" + TxtNombre.Text + "'," + DrConexion.SelectedValue + ",1,'" + TxtPagW.Text + "')";// + DrPagina.SelectedValue  + ")";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMENU", dbParams));
                    TextID.Text = (MiID).ToString();


                    //Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_FLUJO = " + TxtNombre.Text;
                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    ////Vincular Estados relacionados
                    //for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
                    //{
                    //    Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ";
                    //    Column += " ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND) ";
                    //    ColumnVal = " VALUES(" + TxtNombre.Text + "," + ListBox2ID.Items[i].Value + ",";

                    //    Column += ColumnVal;
                    //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    //}
                }
                else
                {
                    //Edita Menu
                    string Column = "UPDATE ZMENU SET ZDESCRIPCION = '" + TxtDescripcion.Text + "', ";
                    Column += " ZTITULO = '" + TxtNombre.Text + "', ";
                    Column += " ZROOT = " + DrConexion.SelectedValue + ", ";
                    Column += " ZPAGINA = '" + TxtPagW.Text + "' ";
                    Column += " WHERE ZID =" + TextID.Text + " ";

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);


                    //Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_FLUJO = " + TxtNombre.Text;
                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    //Vincular Estados relacionados
                    //for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
                    //{
                    //    Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ";
                    //    Column += " ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND) ";
                    //    string ColumnVal = " VALUES(" + TxtNombre.Text + "," + ListBox2ID.Items[i].Value + ",";

                    //    Column += ColumnVal;
                    //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    //}

                }
            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaMenu(0);
            if(Drsiguiente.SelectedIndex == 0)
            {

            }
            else
            {
                Actualiza_Archivos();
                CargaLosMenus();
            }
        }
        //protected void Imgatras_Click(object sender, EventArgs e)
        //{
        //    ImageButton Img = (ImageButton)sender;
        //    if(Img.ID == "Imgatras")
        //    {
        //        Imgatras.Visible = false;
        //        ImgatrasC.Visible = true;
        //    }
        //    else
        //    {
        //        Imgatras.Visible = true;
        //        ImgatrasC.Visible = false;
        //    }
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaEstado(1);
        //    }
        //}

        //protected void ImgNext_Click(object sender, EventArgs e)
        //{
        //    ImageButton Img = (ImageButton)sender;
        //    if (Img.ID == "ImgNext")
        //    {
        //        ImgNext.Visible = false;
        //        ImgNextC.Visible = true;
        //    }
        //    else
        //    {
        //        ImgNext.Visible = true;
        //        ImgNextC.Visible = false;
        //    }
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaEstado(1);
        //    }
        //}

        //protected void ImgAlter_Click(object sender, EventArgs e)
        //{
        //    ImageButton Img = (ImageButton)sender;
        //    if (Img.ID == "ImgAlter")
        //    {
        //        ImgAlter.Visible = false;
        //        ImgAlterC.Visible = true;
        //    }
        //    else
        //    {
        //        ImgAlter.Visible = true;
        //        ImgAlterC.Visible = false;
        //    }
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaEstado(1);
        //    }
        //}

        //protected void imgFin_Click(object sender, EventArgs e)
        //{
        //    ImageButton Img = (ImageButton)sender;
        //    if (Img.ID == "imgFin")
        //    {
        //        imgFin.Visible = false;
        //        imgFinC.Visible = true;
        //    }
        //    else
        //    {
        //        imgFin.Visible = true;
        //        imgFinC.Visible = false;
        //    }
        //    if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
        //    {
        //    }
        //    else
        //    {
        //        EditaEstado(1);
        //    }
        //}

        protected void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            //IbtAllEstado.ImageUrl = "~/Images/allwhite.png";

            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "3" || this.Session["Edicion"].ToString() == "4")
            {
                if (this.Session["Edicion"].ToString() == "3")
                {
                    //Crea la relación Menu -Grupo
                    //SELECT ZID, ZID_USUARIO, ZID_GRUPO, ZNIVEL, ZIDMENU from ZMENUPERMISOSGRUPO


                    //string Column = "INSERT INTO ZESTADOSFLUJO (ZNOMBRE, ZORDEN, ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND, ZIMG) ";
                    //string ColumnVal = " VALUES('" + TxtEstado.Text + "',0," + Dratras.SelectedItem.Value + "," + Drsiguiente.SelectedItem.Value + ",";// + Dralternativo.SelectedItem.Value + "," + Drfinal.SelectedItem.Value + ",'" + TxtCondicion.Text + "')";
                    //Column += ColumnVal;
                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMENUPERMISOSGRUPO", dbParams));
                    //TxtidEstado.Text = (MiID).ToString();

                    //string flujo = "0";
                    //if(DrFlujos.SelectedItem.Value != "0")
                    //{
                    //    flujo = DrFlujos.SelectedItem.Value;
                    //}
                    //string archivo = "0";
                    //if (DrArchivos.SelectedItem.Value != "0")
                    //{
                    //    archivo = DrArchivos.SelectedItem.Value;
                    //}

                    //Column = "DELETE FROM ZFLUJOSESTADOS WHERE ZID_ESTADO = " + TxtidEstado.Text + " AND ZID_ARCHIVO = " + archivo + " ";
                    //Column += " AND ZID_FLUJO = " + flujo + " ";
                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    //Column = "INSERT INTO ZFLUJOSESTADOS (ZID_FLUJO, ZID_ESTADO, ZID_ARCHIVO, ZPREVIUSVIEW, ZNEXTVIEW, ZALTERNATIVEVIEW, ZENDVIEW, ZCONDICION) ";
                    //ColumnVal = " VALUES(" + flujo + "," + TxtidEstado.Text + "," + archivo + ",";

                    //if (Imgatras.Visible == true)
                    //{
                    //    ColumnVal += " 1, ";
                    //}
                    //else
                    //{
                    //    ColumnVal += " 0, ";
                    //}

                    //if (ImgNext.Visible == true)
                    //{
                    //    ColumnVal += " 1, ";
                    //}
                    //else
                    //{
                    //    ColumnVal += " 0, ";
                    //}

                    //if (ImgAlter.Visible == true)
                    //{
                    //    ColumnVal += " 1, ";
                    //}
                    //else
                    //{
                    //    ColumnVal += " 0, ";
                    //}

                    //if (imgFin.Visible == true)
                    //{
                    //    ColumnVal += " 1, ";
                    //}
                    //else
                    //{
                    //    ColumnVal += " 0, ";
                    //}


                    //ColumnVal += "'" + CommentSQL.Text + "')";
                    //Column += ColumnVal;
                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else
                {
                    //Edita flujo
                    string flujo = "0";
                    if (DrFlujos.SelectedItem.Value != "0")
                    {
                        flujo = DrFlujos.SelectedItem.Value;
                    }
#pragma warning disable CS0219 // La variable 'archivo' está asignada pero su valor nunca se usa
                    string archivo = "0";
#pragma warning restore CS0219 // La variable 'archivo' está asignada pero su valor nunca se usa
                    //if (DrArchivos.SelectedItem.Value != "0")
                    //{
                    //    archivo = DrArchivos.SelectedItem.Value;
                    //}

                    string Column = "DELETE ZMENUPERMISOSGRUPO WHERE ZID_GRUPO = " + Drsiguiente.SelectedItem.Value + " ";
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
                    {
                        Column = "INSERT INTO ZMENUPERMISOSGRUPO (ZID_GRUPO, ZNIVEL, ZIDMENU, ZID_USUARIO) " ;
                        Column += " VALUES(" + Drsiguiente.SelectedItem.Value + "," + ListBoxNivel.Items[i].Text + "," + ListBox2ID.Items[i].Text + ",0)";
                        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    }


                    //string Column = "UPDATE ZMENUPERMISOSGRUPO SET ZNOMBRE = '" + TxtDescripcion.Text + "', ";

                    //Column += " ZPREVIUS = " + Dratras.SelectedItem.Value + ", ";
                    //Column += " ZNEXT =" + Drsiguiente.SelectedItem.Value + ", ";
                    //Column += " ZALTERNATIVE =" + Dralternativo.SelectedItem.Value + ", ";
                    //Column += " ZEND =" + Drfinal.SelectedItem.Value + ", ";
                    //Column += " ZORDEN = 0, ";
                    //Column += " ZIMG = '" + TxtCondicion.Text + "' ";
                    //Column += " WHERE ZID =" + TxtidEstado.Text + " ";

                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    //Column = " UPDATE ZFLUJOSESTADOS SET ";

                    //if (Imgatras.Visible == true)
                    //{
                    //    Column += " ZPREVIUSVIEW = 1, ";
                    //}
                    //else
                    //{
                    //    Column += " ZPREVIUSVIEW = 0, ";
                    //}

                    //if (ImgNext.Visible == true)
                    //{
                    //    Column += " ZNEXTVIEW = 1, ";
                    //}
                    //else
                    //{
                    //    Column += " ZNEXTVIEW = 0, ";
                    //}

                    //if (ImgAlter.Visible == true)
                    //{
                    //    Column += " ZALTERNATIVEVIEW = 1, ";
                    //}
                    //else
                    //{
                    //    Column += " ZALTERNATIVEVIEW = 0, ";
                    //}

                    //if (imgFin.Visible == true)
                    //{
                    //    Column += " ZENDVIEW = 1, ";
                    //}
                    //else
                    //{
                    //    Column += " ZENDVIEW = 0, ";
                    //}

                    //Column += " ZCONDICION = '" + CommentSQL.Text + "' ";
                    //Column += " WHERE ZID_ESTADO = " + TxtidEstado.Text + " ";
                    //Column += " AND ZID_ARCHIVO = " + archivo + " ";
                    //Column += " AND ZID_FLUJO = " + flujo + " ";

                    //DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                }

            }
            this.Session["Edicion"] = "0";
            this.Session["proceso"] = "";
            EditaGrupo(0);
            drsiguiente_SelectedIndexChanged(null, null);
            //Actualiza_Archivos();
        }
        
        protected void DrTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCopiaFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0") { return; }
            //LimpioFlujo();
            EditaMenu(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "Menu";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMENU", dbParams));
            TextID.Text = (MiID + 1).ToString();
            TxtDescripcion.Text = "Copia de " + TxtDescripcion.Text;
        }
        
        protected void btnCreaFlujo_Click(object sender, EventArgs e)
        {
            if(this.Session["Edicion"].ToString() != "0") { return; }
            LimpioFlujo();
            EditaMenu(1);
            this.Session["Edicion"] = "1";
            this.Session["proceso"] = "Menu";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZMENU", dbParams));
            TextID.Text = (MiID + 1).ToString(); 
        }
        protected void btnEditFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaMenu(1);
            this.Session["Edicion"] = "2";
            this.Session["proceso"] = "Menu";
        }

        protected void btnDeleteFMenu_Click(object sender, EventArgs e)
        {
            Lbmensaje.Text = "Se eliminará el Menú seleccionado. ¿Desea hacerlo?";
            windowmessaje.Visible = true;
            cuestion.Visible = true;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = false;
            MiCloseMenu();

        }

        protected void btnDeleteFlujo_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() != "4") 
            {
                string Column = "DELETE FROM ZMENU WHERE  ZID = " + TextID.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                Column = "DELETE FROM ZMENUPERMISOSGRUPO WHERE  ZIDMENU = " + TextID.Text + " ";
                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                LimpioFlujo();
                EditaMenu(0);
                this.Session["Edicion"] = "0";
                this.Session["proceso"] = "";
                Actualiza_Archivos();
                CargaLosMenus();
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

            DrFlujos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DrFlujos.DataSource = dt;
            DrFlujos.DataBind();
            //IbtAllFlujo.ImageUrl = "~/Images/allgreen.png";

        }

        protected void btnCopiaEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //LimpioEstado();
            EditaGrupo(1);
            this.Session["Edicion"] = "3";
            this.Session["proceso"] = "grupo";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZESTADOSFLUJO", dbParams));
            //TxtidEstado.Text = (MiID + 1).ToString();
            //TxtEstado.Text = "Copia de " + TxtEstado.Text;
        }
        
        protected void btnCreaEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            LimpioEstado();
            EditaGrupo(1);
            this.Session["Edicion"] = "3";
            this.Session["proceso"] = "grupo";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZESTADOSFLUJO", dbParams));
            //TxtidEstado.Text = (MiID + 1).ToString();
        }
        protected void btnEditEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaGrupo(1);
            this.Session["Edicion"] = "4";
            this.Session["proceso"] = "grupo";
        }

        //protected void btnEditaProfile_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Edicion"].ToString() != "0")
        //    {
        //        Cancelado();
        //        return;
        //    }
        //    //EditaProfile(1);
        //    this.Session["Edicion"] = "5";
        //    this.Session["proceso"] = "profile";
        //}
        protected void btnDeleteMenu_Click(object sender, EventArgs e)
        {
            Lbmensaje.Text = "Se eliminará la relaciones de Menús para este Grupo. ¿Desea hacerlo?";
            windowmessaje.Visible = true;
            cuestion.Visible = true;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = false;
            MiCloseMenu();


        }

        protected void btnDeleteEstado_Click(object sender, EventArgs e)
        {

            SqlParameter[] dbParams = new SqlParameter[0];
            string Column = "DELETE ZMENUPERMISOSGRUPO WHERE ZID_GRUPO = " + Drsiguiente.SelectedItem.Value + " ";
            DBHelper.ExecuteNonQuerySQL(Column, dbParams);

            //if (this.Session["Edicion"].ToString() != "4")
            //{
            //    string Column = "DELETE FROM ZESTADOSFLUJO WHERE  ZID = " + TxtidEstado.Text + " ";
            //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

            //    Column = "DELETE FROM ZFLUJOSESTADOS WHERE  ZID_ESTADO = " + TxtidEstado.Text + " ";
            //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
            //    LimpiaEstados();
            //    EditaEstado(0);
            //    this.Session["Edicion"] = "0";
            //    this.Session["proceso"] = "";
            Actualiza_Archivos();
            //}
        }
        protected void btnAllEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            //DrEstado.Items.Clear();
            //DrEstado.DataValueField = "ZID";
            //DrEstado.DataTextField = "ZDESCRIPCION";

            //DataTable dt = Main.CargaFlujosEstados(0).Tables[0];
            DataTable dt = Main.CargaEstadosFl(0).Tables[0];
            //DrEstado.DataSource = dt;
            //DrEstado.DataBind();
            //IbtAllEstado.ImageUrl = "~/Images/allgreen.png";
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
            //DrEstado.Items.Clear();
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

            foreach (Control ctrl in this.Controls)
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
            //DrArchivos.Enabled = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = 2;
            this.Session["proceso"] = "archivo";
            foreach (Control ctrl in this.Controls)
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
            //DrArchivos.Enabled = false;

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

                //Lbmensaje.Text = "Se eliminará el Archivo Documental " + DrArchivos.SelectedItem.Text + " de la base de datos.¿Desea Continuar?";
                Lbmensaje.Text = "Se eliminará el Archivo Documental ";
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
            ListBoxNivel.Items.Clear();

            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaMenu(1);
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
                    //ListBoxNivel.Items.Add("0");
                    ListBoxNivel.Items.Add(LbNivel.Text);
                    
                }
                this.Session["indice"] = ListBox2.SelectedIndex;

                if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
                {
                }
                else
                {
                    //EditaMenu(1);
                    EditaGrupo(1);
                    LbAsociado.Text = ListBox2.Items.Count.ToString();
                    CargaMenuList();
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
                //ListKeys.Items.RemoveAt(ListBox2.SelectedIndex);
                //ListBox2Col.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBoxNivel.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2.Items.Remove(ListBox2.SelectedItem);
            }
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaMenu(1);
                EditaGrupo(1);
                LbAsociado.Text = ListBox2.Items.Count.ToString();
                CargaMenuList();
            }
            for (int i = 0; i < ListBoxNivel.Items.Count - 1; i++)
            {
                ListBoxNivel.Items[i].Selected = false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
           
            //Desmarcammos
            for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            {
                ListBox1.Items[i].Selected = false;
                ListBox1ID.Items[i].Selected = false;
            }
            for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            {
                ListBox2.Items[i].Selected = false;
                ListBox2ID.Items[i].Selected = false;
            }

            //Buscamos un item en listbox1 en minusculas
            for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            {
                if (ListBox1.Items[i].Text.ToLower().Contains(TextBox1.Text.ToLower()))
                {
                    string a = ListBox1.Items[i].Text;
                    a = ListBox1.Items[i].Value;
                    ListBox1.Items[i].Selected = true;
                    ListBox1ID.Items[i].Selected = true;
                    //ListBox1Col.Items[i].Selected = true;
                    //break;
                }
            }
            //Buscamos un item en listbox2 en minusculas
            for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            {
                if (ListBox2.Items[i].Text.ToLower().Contains(TextBox1.Text.ToLower()))
                {
                    string a = ListBox2.Items[i].Text;
                    a = ListBox2.Items[i].Value;
                    ListBox2.Items[i].Selected = true;
                    ListBox2ID.Items[i].Selected = true;
                    //ListBox2Col.Items[i].Selected = true;
                    //break;
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
                //EditaMenu(1);
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
            //CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
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
            //CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
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
                EditaMenu(1);
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
                EditaMenu(1);
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


            foreach (Control ctrl in this.Controls)
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
            //DivSQL.Visible = false;
            DivCampoDer.Visible = true;
            //DrArchivos.Enabled = true;
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
            //TxtidEstado.Text = "";
            //TxtEstado.Text = "";
            //Dratras.SelectedIndex = 0;
            //Imgatras.Visible = true;
            //ImgatrasC.Visible = false;
            Drsiguiente.SelectedIndex = 0;
            //ImgNext.Visible = true;
            //ImgNextC.Visible = false;
            //Dralternativo.SelectedIndex = 0;
            //ImgAlter.Visible = true;
            //ImgAlterC.Visible = false;
            //Drfinal.SelectedIndex =0;
            //imgFin.Visible = true;
            //imgFinC.Visible = false;
            //TxtCondicion.Text = "";
        }

        protected void DrEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Identificación de campo clave ZKEY, si está en edición asignarlo y poner en verde
            //DrEstado.BackColor = Color.FromName("#bdecb6");
            //LimpiaEstados();

            //DataTable dt = Main.CargaEstadosFlujos("0",DrEstado.SelectedItem.Value).Tables[0];
         
            //this.Session["idestado"] = DrEstado.SelectedItem.Value;
            //this.Session["EstadoCondicion"] = "";

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if(dr["ZID"].ToString() == DrEstado.SelectedItem.Value)
            //    {
            //        TxtidEstado.Text = dr["ZID"].ToString();
            //        TxtEstado.Text = dr["ZNOMBRE"].ToString();
            //        string miro = dr["ZCONDICION"].ToString();
                    

            //        if (miro != "")
            //        {
            //            if (miro.Contains("ZTEMPLATE#") == true)
            //            {
            //                miro = miro.Replace("ZTEMPLATE#", "");
            //                this.Session["EstadoCondicion"] = miro;
            //                string[] Fields = System.Text.RegularExpressions.Regex.Split(miro, "-");
            //                ListTemplate.Items.Clear();
            //                ListTemplateID.Items.Clear();
            //                //if (Fields.Count() > 0)
            //                //{
            //                //    foreach (string Field in Fields)
            //                //    {
            //                //        for (int i = 0; i < DrTemplates.Items.Count; i++)
            //                //        {
            //                //            if (DrTemplates.Items[i].Value == Field)
            //                //            {
            //                //                ListTemplate.Items.Add(new ListasID(DrTemplates.Items[i].Text, Convert.ToInt32(Field)).ToString());
            //                //                ListTemplateID.Items.Add(new ListasID(Convert.ToInt32(Field).ToString(), Convert.ToInt32(Field)).ToString());
            //                //                break;
            //                //            }
            //                //        }
            //                //    }
            //                //}
            //                //DivTemplate.Visible = true;
            //                //DivTxtQuery.Visible = false;
            //            }
            //            else
            //            {
            //                TxtQueryProfile.Text = miro;
            //                //DivTxtQuery.Visible = true;
            //                //DivTemplate.Visible = false;
            //            }
            //        }

            //        miro = dr["ZPREVIUS"].ToString();


            //        //for (int i = 0; i <= Dratras.Items.Count - 1; i++)
            //        //{
            //        //    if (Dratras.Items[i].Value == dr["ZPREVIUS"].ToString())
            //        //    {
            //        //        Dratras.SelectedIndex = i;
            //        //        //if (dr["ZPREVIUSVIEW"].ToString() == "1") 
            //        //        //{ 
            //        //        //    Imgatras.Visible = true;
            //        //        //    ImgatrasC.Visible = false;
            //        //        //} 
            //        //        //else 
            //        //        //{
            //        //        //    Imgatras.Visible = false;
            //        //        //    ImgatrasC.Visible = true; 
            //        //        //}
            //        //        break;
            //        //    }
            //        //}

            //        for (int i = 0; i <= Drsiguiente.Items.Count - 1; i++)
            //        {
            //            if (Drsiguiente.Items[i].Value == dr["ZNEXT"].ToString())
            //            {
            //                Drsiguiente.SelectedIndex = i;
            //                //if (dr["ZNEXTVIEW"].ToString() == "1")
            //                //{
            //                //    ImgNext.Visible = true;
            //                //    ImgNextC.Visible = false;
            //                //}
            //                //else
            //                //{
            //                //    ImgNext.Visible = false;
            //                //    ImgNextC.Visible = true;
            //                //}
            //                break;
            //            }
            //        }

            //        //for (int i = 0; i <= Dralternativo.Items.Count - 1; i++)
            //        //{
            //        //    if (Dralternativo.Items[i].Value == dr["ZALTERNATIVE"].ToString())
            //        //    {
            //        //        Dralternativo.SelectedIndex = i;
            //                //if (dr["ZALTERNATIVEVIEW"].ToString() == "1")
            //                //{
            //                //    ImgAlter.Visible = true;
            //                //    ImgAlterC.Visible = false;
            //                //}
            //                //else
            //                //{
            //                //    ImgAlter.Visible = false;
            //                //    ImgAlterC.Visible = true;
            //                //}
            //        //        break;
            //        //    }
            //        //}

            //        //for (int i = 0; i <= Drfinal.Items.Count - 1; i++)
            //        //{
            //        //    if (Drfinal.Items[i].Value == dr["ZEND"].ToString())
            //        //    {
            //        //        Drfinal.SelectedIndex = i;
            //                //if (dr["ZENDVIEW"].ToString() == "1")
            //                //{
            //                //    imgFin.Visible = true;
            //                //    imgFinC.Visible = false;
            //                //}
            //                //else
            //                //{
            //                //    imgFin.Visible = false;
            //                //    imgFinC.Visible = true;
            //                //}
            //        //        break;
            //        //    }
            //        //}

            //        break;
            //    }
            //}

        }
        
        //protected void btnDeleteArchivoTemplate_Click(object sender, EventArgs e)
        //{
 
        //}
        protected void HConsultaSQL_clik(object sender, EventArgs e)
        {
            //if (DivTxtQuery.Visible == true)
            //{
            //    DivTxtQuery.Visible = false;
            //    DivTemplate.Visible = true;
            //}
            //else
            //{
            //    DivTxtQuery.Visible = true;
            //    DivTemplate.Visible = false;
            //}
        }
        protected void DrEstadoIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProceso(1);
            }
        }
        protected void DrArchivoIni_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProceso(1);
            }
        }
        protected void TexTosPlantillas_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaPlantilla(1);
            }
        }

        protected void TexTosPlantillas_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaPlantilla(1);
            }
        }

        protected void TexTosProcesos_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProceso(1);
            }
        }
        protected void TexTosProcesos_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProceso(1);
            }
        }

        protected void TexTosMarcador_TextChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaMarcador(1);
            }
        }
        protected void TexTosMarcador_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaMarcador(1);
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
                //EditaProceso(1);
            }
        }

        protected void DrBoxIniPlantilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                //EditaProceso(1);
            }
        }


        protected void dratras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaGrupo(1);
            }
        }

        private void CargaMenuList()
        {
            string a = "";
            for (int i = 0; i <= ListBox2ID.Items.Count - 1; i++)
            {
                if(a == "")
                {
                    a += "(" + ListBox2ID.Items[i].Value;
                }
                else
                {
                    a += "," + ListBox2ID.Items[i].Value;
                }
                
            }
            a += ")";

            if (a == ")")
            {
                return;
            }
            string SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
            SQL += "FROM ZMENU AA WHERE  ZESTADO <> 3 ";
            SQL += " AND ZID in " + a;
            SQL += " OR ZID in ( SELECT ZROOT FROM  ZMENU WHERE ZID in " + a + ")";
            SQL += "ORDER BY ZID, ZROOT";

            DataTable table = Main.BuscaLote(SQL).Tables[0];
            this.Session["totalMenus"] = table;
            DataRow[] parentMenus = table.Select("ZROOT = 0");

            var sb = new StringBuilder();
            string unorderedList = GenerateULMenu(parentMenus, table, sb);
            //unorderedList = Main.Actualiza_ULMenu(unorderedList, table);

            DivEstados.InnerHtml = unorderedList;
            DivEstados.Visible = true;
        }
        protected void drsiguiente_SelectedIndexChanged(object sender, EventArgs e)
        {

            //string SQL = "SELECT A.ZID, A.ZID_USUARIO, A.ZID_GRUPO, A.ZORDEN, A.ZKEY, A.ZPERMISOS, A.ZIDMENU, B.ZID AS ID_MENU, B.ZROOT, B.ZPAGINA,  B.ZTITULO ";
            //SQL += " FROM ZGRUPOUSUARIOS A, ZMENU B ";
            //SQL += " WHERE A.ZID_GRUPO = " + Drsiguiente.SelectedItem.Value;
            //SQL += " AND B.ZID = A.ZIDMENU ";
            //SQL += " ORDER BY A.ZIDMENU, B.ZROOT ";
            //string SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
            //SQL += "FROM ZMENU AA WHERE  ZESTADO <> 3 ";
            //SQL += "ORDER BY ZID, ZROOT";
            //DataTable table = Main.BuscaLote(SQL).Tables[0];
            //this.Session["totalMenus"] = table;
            DivEstados.InnerHtml = "";
            //DivEstados.Visible = false;

            if (Drsiguiente.SelectedItem.Value == "0")
            {
                ibtEditEstado.Enabled = false;
                ibtDeleteEstado.Enabled = false;
                ibtEditEstado.ImageUrl = "~/images/editdoc35X30Black.png";
                ibtDeleteEstado.ImageUrl = "~/images/elimina35X30Black.png";
                return;
            }

            ibtEditEstado.Enabled = true;
            ibtDeleteEstado.Enabled = false;

            ibtEditEstado.ImageUrl = "~/images/editdoc35X30.png";
            ibtDeleteEstado.ImageUrl = "~/images/elimina35X30Black.png";


            //SQL = " SELECT DISTINCT(D.ZIDMENU) AS ZID_MENU, ";
            //SQL += " C.ZNOMBRE, C.ZDESCRIPCION, D.ZID, F.ZID as ID_MENU,  ";
            //SQL += " F.ZDESCRIPCION,F.ZPAGINA, F.ZROOT,  F.ZTITULO, D.ZNIVEL, C.ZNIVEL as ZNIVELGRUPO ";
            //SQL += " FROM ZGRUPOS C ";
            //SQL += " INNER JOIN  ZMENUPERMISOSGRUPO D ON D.ZID_GRUPO = C.ZID ";
            //SQL += " INNER JOIN  ZMENU F ON F.ZID = D.ZIDMENU ";
            //SQL += " WHERE C.ZID =  " + Drsiguiente.SelectedItem.Value;
            //SQL += " AND D.ZIDMENU = F.ZID ";
            //SQL += " ORDER BY   F.ZROOT,D.ZIDMENU ";

            string SQL = " SELECT DISTINCT(D.ZIDMENU) AS ZID_MENU, C.ZNOMBRE, C.ZDESCRIPCION, D.ZID, F.ZID as ID_MENU,  ";
            SQL += " F.ZDESCRIPCION,F.ZPAGINA, F.ZROOT,  F.ZTITULO, D.ZNIVEL, C.ZNIVEL as ZNIVELGRUPO   ";
            SQL += " FROM ZGRUPOS C ";
            SQL += " INNER JOIN ZMENUPERMISOSGRUPO D ON D.ZID_GRUPO = C.ZID ";
            SQL += " INNER JOIN  ZNIVELES E ON E.ZID = D.ZNIVEL ";
            SQL += " INNER JOIN  ZMENU F ON F.ZID = D.ZIDMENU ";
            SQL += " WHERE C.ZID in  (" + Drsiguiente.SelectedItem.Value + ") ";
            SQL += " ORDER BY  D.ZIDMENU, F.ZROOT ";

            //string SQL = " SELECT DISTINCT(D.ZIDMENU) AS ZID_MENU, ";
            //SQL += " C.ZNOMBRE, C.ZDESCRIPCION, D.ZID, F.ZID as ID_MENU,  ";
            //SQL += " F.ZDESCRIPCION,F.ZPAGINA, F.ZROOT,  F.ZTITULO, D.ZNIVEL, C.ZNIVEL as ZNIVELGRUPO ";
            //SQL += "FROM ZUSUARIOS A ";
            //SQL += "INNER JOIN  ZUSUARIOGRUPO B ON A.ZID = B.ZID_USUARIO ";
            //SQL += "INNER JOIN  ZGRUPOS C ON C.ZID = B.ZID_GRUPO ";
            //SQL += "INNER JOIN  ZMENUPERMISOSGRUPO D ON D.ZID_GRUPO = B.ZID_GRUPO ";
            //SQL += "INNER JOIN  ZNIVELES E ON E.ZID = D.ZNIVEL ";
            //SQL += "INNER JOIN  ZMENU F ON F.ZID = D.ZIDMENU ";
            //SQL += " WHERE C.ZID in  (" + Drsiguiente.SelectedItem.Value + ") "; 
            //SQL += " ORDER BY  D.ZIDMENU, F.ZROOT ";

            DataTable table = Main.BuscaLote(SQL).Tables[0];
            this.Session["totalMenus"] = table;
            if (table.Rows.Count > 0)
            {
                string a = "";
                foreach (DataRow drow in table.Rows)
                {
                    if (a == "")
                    {
                        a += "(" + drow["ZID_MENU"].ToString();
                    }
                    else
                    {
                        a += "," + drow["ZID_MENU"].ToString();
                    }
                }
                a += ")";

                SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                SQL += "FROM ZMENU AA WHERE  ZESTADO <> 3 ";
                SQL += " AND ZID in " + a;
                SQL += " OR ZID in ( SELECT ZROOT FROM  ZMENU WHERE ZID in " + a + ")";
                SQL += " ORDER BY ZID, ZROOT";

                table = Main.BuscaLote(SQL).Tables[0];
                //this.Session["totalMenus"] = table;
                DataRow[] parentMenus = table.Select("ZROOT = 0");

                var sb = new StringBuilder();
                string unorderedList = GenerateULMenu(parentMenus, table, sb);
                //unorderedList = Main.Actualiza_ULMenu(unorderedList, table);

                DataTable MiMenu = this.Session["totalMenus"] as DataTable;

                //table = Main.BuscaLote(SQL).Tables[0];
                //DataRow[] parentMenus = MiMenu.Select("ZROOT = 0");

                //var sb = new StringBuilder();
                //this.Session["Contador"] = 0;
                ////string G = MiMenu.Rows.ToString();
                //this.Session["TotalContador"] = 500;
                ////string unorderedList = GenerateUL(parentMenus, table, sb);ç
                //string unorderedList = GenerateULMenu(parentMenus, table, sb);

                if (Drsiguiente.SelectedItem.Value.ToString() == "0")
                {
                    DivEstados.InnerHtml = "";
                    DivEstados.Visible = false;
                }
                else
                {
                    DivEstados.InnerHtml = unorderedList;
                    DivEstados.Visible = true;
                }

                int visto = 0;
                ListBox1.Items.Clear();
                ListBox1ID.Items.Clear();
                ListBox2.Items.Clear();
                ListBox2ID.Items.Clear();
                ListBoxNivel.Items.Clear();
                int Contador = 0;

                SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                SQL += "FROM ZMENU AA  ";
                SQL += " WHERE  ZESTADO <> 3 ";
                SQL += " AND  ZPAGINA <> '#' ";
                SQL += " ORDER BY ZID, ZROOT";

                table = Main.BuscaLote(SQL).Tables[0];
                //Si hay Menus asociados a ese Grupo
                if (Drsiguiente.SelectedItem.Value.ToString() != "0")
                {
                    foreach (DataRow filamin in table.Rows)
                    {
                        visto = 0;
                        foreach (DataRow fila in MiMenu.Rows)
                        {
                            if (filamin["ZID"].ToString() == fila["ZID_MENU"].ToString())
                            {
                                ListBox2.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID_MENU"].ToString())).ToString());
                                ListBox2ID.Items.Add(new ListasID(fila["ZID_MENU"].ToString(), Convert.ToInt32(fila["ZID_MENU"].ToString())).ToString());
                                ListBoxNivel.Items.Add(new ListasID(fila["ZNIVEL"].ToString(), Convert.ToInt32(fila["ZNIVEL"].ToString())).ToString());
                                LbNivel.Text = fila["ZNIVELGRUPO"].ToString();
                                visto = 1;
                                break;
                            }
                        }
                        if (visto == 0)
                        {
                            //if (filamin["ZPAGINA"].ToString() != "#")
                            //{
                                ListBox1.Items.Add(new ListasID(filamin["ZTITULO"].ToString(), Convert.ToInt32(filamin["ZID"].ToString())).ToString());
                                ListBox1ID.Items.Add(new ListasID(filamin["ZID"].ToString(), Convert.ToInt32(filamin["ZID"].ToString())).ToString());
                                Contador += 1;
                            //}
                        }
                    }
                    LbDisponible.Text = Contador.ToString(); // MiMenu.Rows.Count.ToString();
                    LbAsociado.Text = ListBox2.Items.Count.ToString();
                }
            }
            else //Si la consulta devuelve vacio, reseteo todo
            {
                ListBox1.Items.Clear();
                ListBox1ID.Items.Clear();
                ListBox2.Items.Clear();
                ListBox2ID.Items.Clear();
                ListBoxNivel.Items.Clear();
                int Contador = 0;

                Object Con = DBHelper.ExecuteScalarSQL("SELECT ZNIVEL FROM ZGRUPOS  WHERE ZID = " + Drsiguiente.SelectedItem.Value , null);

                if (Con is System.DBNull)
                {
                    LbNivel.Text = "0";
                }
                else
                {
                    LbNivel.Text = Con.ToString();
                }

                SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                SQL += "FROM ZMENU AA  ";
                SQL += " WHERE  ZESTADO <> 3 ";
                SQL += " AND  ZPAGINA <> '#' ";
                SQL += " ORDER BY ZID, ZROOT";

                table = Main.BuscaLote(SQL).Tables[0];
                //Si hay Menus asociados a ese Grupo
                if (Drsiguiente.SelectedItem.Value.ToString() != "0")
                {
                    foreach (DataRow filamin in table.Rows)
                    {
                        ListBox1.Items.Add(new ListasID(filamin["ZTITULO"].ToString(), Convert.ToInt32(filamin["ZID"].ToString())).ToString());
                        ListBox1ID.Items.Add(new ListasID(filamin["ZID"].ToString(), Convert.ToInt32(filamin["ZID"].ToString())).ToString());
                        Contador += 1;
                    }
                    LbDisponible.Text = Contador.ToString(); // MiMenu.Rows.Count.ToString();
                    LbAsociado.Text = ListBox2.Items.Count.ToString();
                }
            }
        }
        protected void dralternativo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaGrupo(1);
            }
        }
        protected void drfinal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaGrupo(1);
            }
        }


        protected void DrDuplicado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DrFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrFlujos.BackColor = Color.FromName("#bdecb6");
            this.Session["idmenu"] = DrFlujos.SelectedItem.Value;
            if(DrFlujos.SelectedItem.Value == "0")
            {
                ImageCopiaFlujo.Enabled = false;
                IbtCreaFlujo.Enabled = true;
                ibtEditFlujo.Enabled = false;
                ibtDeleteFlujo.Enabled = false;
                ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30Black.png";
                IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30.png";
                ibtEditFlujo.ImageUrl = "~/images/editdoc35X30Black.png";
                ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30Black.png";
                return;
            }

            ImageCopiaFlujo.Enabled = true;
            IbtCreaFlujo.Enabled = true;
            ibtEditFlujo.Enabled = true;
            ibtDeleteFlujo.Enabled = false;

            ImageCopiaFlujo.ImageUrl = "~/images/copiar35X30.png";
            IbtCreaFlujo.ImageUrl = "~/images/newdoc35X30.png";
            ibtEditFlujo.ImageUrl = "~/images/editdoc35X30.png";
            ibtDeleteFlujo.ImageUrl = "~/images/elimina35X30Black.png";

            DataTable dt = new DataTable();
            dt = Main.CargaMenus(Convert.ToInt32(DrFlujos.SelectedItem.Value)).Tables[0];

            if(dt.Rows.Count == 0)
            {


                TxtNombre.Text = "";
                TxtDescripcion.Text = "";

                foreach (DataRow dr in dt.Rows)
                {
                    TextID.Text = dr["ZID"].ToString();
                    TxtNombre.Text = dr["ZTITULO"].ToString();
                    TxtDescripcion.Text = dr["ZDESCRIPCION"].ToString();
                    TxtPagW.Text = dr["ZPAGINA"].ToString();
                    for (int i = 0; i <= DrConexion.Items.Count - 1; i++)
                    {
                        if (DrConexion.Items[i].Value == dr["ZROOT"].ToString())
                        {
                            DrConexion.SelectedIndex = i;
                            DrConexion.Text = DrConexion.Items[i].Text;
                            break;
                        }
                    }
                }

                ListBox1.Items.Clear();
                ListBox1ID.Items.Clear();
                ListBox2.Items.Clear();
                ListBox2ID.Items.Clear();
                //if(Drsiguiente.SelectedIndex != 0)
                //{
                //    drsiguiente_SelectedIndexChanged(null, null);
                //}
                //else
                //{
                    if (Drsiguiente.SelectedItem.Value.ToString() != "0")
                    {
                        drsiguiente_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        dt = Main.CargaMenus(0).Tables[0];
                        foreach (DataRow fila in dt.Rows)//Menus
                        {
                            ListBox1.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                            ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        }
                        //dt = Main.CargaArchivos(7).Tables[0];
                        ListBox1.BackColor = Color.FromName("#ffffff");
                        ListBox1ID.BackColor = Color.FromName("#ffffff");
                        ListBox2.BackColor = Color.FromName("#ffffff");
                        ListBox2ID.BackColor = Color.FromName("#ffffff");
                        LbDisponible.Text = dt.Rows.Count.ToString();
                }
                //}
            }
            else
            {
                TxtNombre.Text = "";
                TxtDescripcion.Text = "";

                //Cargar Menus y asignados
                foreach (DataRow dr in dt.Rows)
                {
                    TextID.Text = dr["ZID"].ToString();
                    TxtNombre.Text = dr["ZTITULO"].ToString();
                    TxtDescripcion.Text = dr["ZDESCRIPCION"].ToString();
                    TxtPagW.Text = dr["ZPAGINA"].ToString();
                    for (int i = 0; i <= DrConexion.Items.Count - 1; i++)
                    {
                        if(DrConexion.Items[i].Value == dr["ZROOT"].ToString())
                        {
                            DrConexion.SelectedIndex = i;
                            //DrConexion.Text = dr["ZTITULO"].ToString();
                            //DrConexion.Text = dr["ZTITULO"].ToString();
                            break;
                        }
                    }
                }

                //if (Drsiguiente.SelectedIndex != 0)
                //{
                //    drsiguiente_SelectedIndexChanged(null, null);
                //}
                //else
                //{
                    if(Drsiguiente.SelectedItem.Value.ToString() != "0")
                    {
                        drsiguiente_SelectedIndexChanged(null, null );
                    }
                    else
                    {

                    

                    dt = Main.CargaMenus(0).Tables[0];
                        foreach (DataRow fila in dt.Rows)//Menus
                        {
                            ListBox1.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                            ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        }
                        //dt = Main.CargaArchivos(7).Tables[0];
                        ListBox1.BackColor = Color.FromName("#ffffff");
                        ListBox1ID.BackColor = Color.FromName("#ffffff");
                        ListBox2.BackColor = Color.FromName("#ffffff");
                        ListBox2ID.BackColor = Color.FromName("#ffffff");
                    }
                    LbDisponible.Text = dt.Rows.Count.ToString();

                //}

                //dt = Main.CargaArchivos(7).Tables[0];
                //ListBox1.Items.Clear();
                //ListBox1ID.Items.Clear();
                //ListBox2.Items.Clear();
                //ListBox2ID.Items.Clear();
                //ListBox1.BackColor = Color.FromName("#ffffff");
                //ListBox1ID.BackColor = Color.FromName("#ffffff");
                //ListBox2.BackColor = Color.FromName("#ffffff");
                //ListBox2ID.BackColor = Color.FromName("#ffffff");

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

                //SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQL.Text, dbParams, SQL).ToString();
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
            //Carga_Lotes();
            MiOpenMenu();
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            //SqlParameter[] dbParams = new SqlParameter[0];
            //DBHelper.ExecuteNonQuerySQL("UPDATE ZARCHIVOS SET ZESTADO = 3 WHERE ZID = " + this.Session["IDArchivo"].ToString(), dbParams);
            //Actualiza_Archivos();
            if(Lbmensaje.Text.Contains("relaciones de Menús") == true)
            {
                btnDeleteEstado_Click(null, null);
            }
            else
            {
                btnDeleteFlujo_Click(null, null);
            }
            MiOpenMenu();
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            MiOpenMenu();

        }

        //protected void BtTipo_Click(object sender, EventArgs e)
        //{
        //    if(DivSQL.Visible == false)
        //    {
        //        DivSQL.Visible = true;
        //        CommentSQL.Width = Unit.Percentage(100);
        //        CommentSQL.Height = 345;
        //        //DivSQL.Attributes.Add("height", "500px");
        //        //DivSQL.Attributes.Add("Width", "300px");
        //        DivCampoDer.Visible = false;
        //        //Iconexion.Attributes["style"] = "margin-top:-10px;";
        //    }
        //    else
        //    {
        //        DivSQL.Visible = false;
        //        DivCampoDer.Visible = true;
        //    }
        //}

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
        protected void DrNiveles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = DrNiveles.SelectedItem.Value;
            string b = DrNiveles.SelectedItem.Text;
            //LbNivel.Text = a;

            EditaGrupo(1);
            //ListBoxNivel.DataSource = null;
            //ListBoxNivel.DataSource = ListBoxNivel;

        }
        protected void btnAllMenu_Click(object sender, EventArgs e)
        {
            if (chkb1.Checked == true)
            {
                string b = DrNiveles.SelectedItem.Text;
                ListBoxNivel.Items.Clear();

                for (int i = 0; i < ListBox2ID.Items.Count; i++)
                {
                    ListBoxNivel.Items.Insert(i, b);
                }
            }
            else
            {
                string a = DrNiveles.SelectedItem.Value;
                string b = DrNiveles.SelectedItem.Text;
                //LbNivel.Text = a;
                int Index = Convert.ToInt32(this.Session["indice"].ToString());
                //ListBox2.SelectedIndex = Index;
                //ListBox2ID.SelectedIndex = Index;

                this.Session["indice"] = Index;

                ListBoxNivel.Items.RemoveAt(Convert.ToInt32(this.Session["indice"].ToString()));
                ListBoxNivel.Items.Insert(Convert.ToInt32(this.Session["indice"].ToString()), DrNiveles.SelectedItem.Text);

                DrNiveles.SelectedIndex = 0;
                //LbNivel.Text = "0";
                ListBoxNivel.SelectedIndex = ListBox2.SelectedIndex;
            }
            EditaGrupo(1);
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
        //protected void gvLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        //{
        //    int index = 0;
        //    //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    try
        //    {
        //        if (e.CommandName == "LveDoc")
        //        {
        //            index = int.Parse(e.CommandArgument.ToString());
        //            //this.Session["idregistro"] = gvLista.DataKeys[index].Value.ToString();
        //            this.Session["iddocumento"] = gvLista.DataKeys[index].Value.ToString();

        //            DataTable dt = this.Session["SelTableFiles"] as DataTable;

        //            foreach (DataRow filas in dt.Rows)
        //            {
        //                if (filas["ZID"].ToString() == gvLista.DataKeys[index].Value.ToString())
        //                {
        //                    string fileName = System.IO.Path.GetFileName(@filas["ZDIRECTORIO"].ToString());


        //                    //if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == false)
        //                    //{
        //                    //    //mensaje que no existe el fichero
        //                    //    Lbmensaje.Text = " El fichero no se encuentra en el Servidor Web.";
        //                    //    cuestion.Visible = false;
        //                    //    Asume.Visible = true;
        //                    //    DvPreparado.Visible = true;
        //                    //    return;

        //                    //}
        //                    //else if (File.Exists(@filas["ZDIRECTORIO"].ToString()) == true)
        //                    //{
        //                    //    string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["idregistro"].ToString(), fileName));
        //                    //    string Midirectorio = Server.MapPath("~/Docs/" + this.Session["idregistro"].ToString());

        //                    //    //string MiPath = Server.MapPath(Path.Combine("~/Docs/" + this.Session["iddocumento"].ToString(), fileName));
        //                    //    //string Midirectorio = Server.MapPath("~/Docs/" + this.Session["iddocumento"].ToString());

        //                    //    if (Directory.Exists(Midirectorio) == false)
        //                    //    {
        //                    //        DirectoryInfo di = Directory.CreateDirectory(Midirectorio);
        //                    //    }


        //                    //    if (File.Exists(@MiPath) == false)
        //                    //    {
        //                    //        File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        File.Delete(MiPath);
        //                    //        File.Copy(@filas["ZDIRECTORIO"].ToString(), MiPath);
        //                    //    }

        //                    //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
        //                    //    string cadena = HttpContext.Current.Request.Url.AbsoluteUri;
        //                    //    string[] Separado = url.Split('/');
        //                    //    url = "";
        //                    //    if (Separado.Count() > 0)
        //                    //    {
        //                    //        for (int i = 0; i < Separado.Count() - 1; i++)
        //                    //        {
        //                    //            if (Separado[i].ToString().Contains("http"))
        //                    //            {
        //                    //                url += Separado[i] + "//";
        //                    //            }
        //                    //            else
        //                    //            {
        //                    //                url += Separado[i] + "/";
        //                    //            }
        //                    //        }

        //                    //        url += "/Docs/" + this.Session["idregistro"].ToString() + "/" + fileName;
        //                    //        //url += "/Docs/" + this.Session["iddocumento"].ToString() + "/" + fileName;
        //                    //    }



        //                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + url + "', '_blank');", true);

        //                    //    string a = Main.Ficherotraza("gvLista rowcommand -->  La ruta del fichero esta en " + url);
        //                    //}
        //                    break;
        //                }
        //            }
        //            //Reemplazar "pdffile.pdf" con el nombre de su archivo PDF.
        //        }
        //        else if (e.CommandName == "ImprimirDoc")
        //        {
        //            index = int.Parse(e.CommandArgument.ToString());
        //            this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
        //        }
        //        else if (e.CommandName == "Edit" || e.CommandName == "Update")
        //        {
        //            index = int.Parse(e.CommandArgument.ToString());
        //            this.Session["IDGridA"] = gvLista.DataKeys[index].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string a = Main.Ficherotraza("gvLista rowcommand --> " + ex.Message);
        //    }
        //}

        //protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        //{
        //    gvLista.SelectedRow.BackColor = Color.FromName("#565656");
        //}
        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            return;
        }
        //protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GridViewRow row = gvLista.Rows[e.RowIndex];
        //    string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();
        //    gvLista.EditIndex = -1;

        //    gvLista.DataBind();
        //}
        //protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    GridViewRow row = gvLista.Rows[e.RowIndex];
        //    string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();
        //    try
        //    {
        //        gvLista.EditIndex = -1;

        //        gvLista.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);

        //    }
        //}
        //protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        //{
        //    gvLista.PageIndex = e.NewPageIndex;
        //    DataTable dt = this.Session["Archivos"] as DataTable;
        //    DataTable dt1 = this.Session["Campos"] as DataTable;
        //}
        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }










        //protected void ListBox1Tar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int Index = ListBox1Tar.SelectedIndex;
        //    ListBox1IDTar.SelectedIndex = ListBox1Tar.SelectedIndex;
        //    //ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;
        //    LbIDTarea.Text = ListBox1IDTar.SelectedItem.Text;
        //    string a = ListBox1IDTar.SelectedValue;
        //    if (DivCancel.Visible == false)
        //    {
        //        DivRel.Visible = false;
        //        DivEdita.Visible = true;
        //    }

        //    //ListBox1Tar.BackColor = Color.FromName("#bdecb6");
        //}

        //protected void ListBox2Tar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int Index = ListBox2Tar.SelectedIndex;
        //    ListBox2IDTar.SelectedIndex = ListBox2Tar.SelectedIndex;


        //    for (int i = 0; i <= DrEstado.Items.Count - 1; i++)
        //    {
        //        if (DrEstado.Items[i].Value == ListBox2IDTar.SelectedItem.Value)
        //        {
        //            DrEstado.SelectedIndex = i;
        //            break;
        //        }
        //    }

        //    //ListBox2Tar.BackColor = Color.FromName("#bdecb6");
        //}



        //protected void ListBox2IDTar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int Index = ListBox2IDTar.SelectedIndex;
        //    //ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
        //    ListBox2.SelectedIndex = ListBox2IDTar.SelectedIndex;
        //    //ListKeys.SelectedIndex = ListBox2IDTar.SelectedIndex;

        //    string a = ListBox2IDTar.SelectedValue;
        //    //string b = ListKeys.SelectedValue;
        //    //chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));
        //}

        //protected void btnEditaTar_Click(object sender, EventArgs e)
        //{
        //    divTarea.Visible = false;
        //    divNePlantilla.Visible = true;
        //    BtEditTarea.Visible=false;

        //    string SQL = "SELECT ZID,ZDESCRIPCION,ZHAND ";
        //    SQL += " FROM ZHANDS ";
        //    SQL += " WHERE ZID = " + ListBox1IDTar.SelectedIndex;
        //    System.Data.DataTable dtt = Main.BuscaLote(SQL).Tables[0];

        //    foreach (DataRow filas in dtt.Rows) //Tabla Archivos seleccionado consulta en tabla Procesos
        //    {
        //        TxtDescTarea.Text = filas["ZDESCRIPCION"].ToString();
        //        TxtDummieE.Text = filas["ZHAND"].ToString();
        //        break;
        //    }
        //    SQL = "SELECT A.ZID, A.ZID_HAND, B.ZID as ZID_USUARIO, B.ZALIAS ";
        //    SQL += " FROM ZUSUARIOPROCESOS A, ZUSUARIOS B ";
        //    SQL += " WHERE A.ZID_HAND = " + ListBox1IDTar.SelectedIndex;
        //    SQL += " AND B.ZID = A.ZID_USUARIO ";

        //    dtt = Main.BuscaLote(SQL).Tables[0];
        //    DrAsigUser.DataValueField = "ZID";
        //    DrAsigUser.DataTextField = "ZALIAS";
        //    DrAsigUser.DataSource = dtt;
        //    DrAsigUser.DataBind();
        //}

            
        //protected void ListBox1IDTar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int Index = ListBox1IDTar.SelectedIndex;
        //    //ListBox1Col.SelectedIndex = ListBox1ID.SelectedIndex;
        //    ListBox1Tar.SelectedIndex = ListBox1IDTar.SelectedIndex;
        //    LbIDTarea.Text = ListBox1IDTar.SelectedItem.Text;
        //    if (DivCancel.Visible == false)
        //    {
        //        DivRel.Visible = false;
        //        DivEdita.Visible = true;
        //    }

        //    //string a = ListBox1IDTar.SelectedValue;

        //    //string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
        //    //SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
        //    //SQL += " WHERE A.ZTIPO = B.ZID ";
        //    //SQL += " AND A.ZID = " + a;
        //    //SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
        //    //DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];
        //}

        //protected void btnPasarSeleccionadosTar_Click(object sender, EventArgs e)
        //{
        //    //Pasamos los items seleccionados de listbox2 a listbox1

        //    try
        //    {
        //        while (ListBox1Tar.GetSelectedIndices().Length > 0)
        //        {
        //            ListBox2Tar.Items.Add(ListBox1Tar.SelectedItem);
        //            ListBox2IDTar.Items.Add(ListBox1IDTar.SelectedItem);
        //            //ListBox2Col.Items.Add(ListBox1Col.SelectedItem);
        //            //ListKeys.Items.Add("0");
        //            ListBox1IDTar.Items.RemoveAt(ListBox1Tar.SelectedIndex);
        //            //ListBox1Col.Items.RemoveAt(ListBox1.SelectedIndex);
        //            ListBox1Tar.Items.Remove(ListBox1Tar.SelectedItem);
        //        }

        //    }
        //    catch
        //    {

        //    }


        //}

        //protected void btnRegresarSeleccionadosTar_Click(object sender, EventArgs e)
        //{
        //    //Regresamos los items seleccionados de listbox2 a listbox1

        //    while (ListBox2Tar.GetSelectedIndices().Length > 0)
        //    {
        //        ListBox1Tar.Items.Add(ListBox2Tar.SelectedItem);
        //        ListBox1IDTar.Items.Add(ListBox2IDTar.SelectedItem);
        //        //ListBox1Col.Items.Add(ListBox2Col.SelectedItem);
        //        ListBox2IDTar.Items.RemoveAt(ListBox2Tar.SelectedIndex);
        //        //ListKeys.Items.RemoveAt(ListBox2Tar.SelectedIndex);
        //        //ListBox2Col.Items.RemoveAt(ListBox2.SelectedIndex);
        //        ListBox2Tar.Items.Remove(ListBox2Tar.SelectedItem);
        //    }
        //}

        //protected void btnBuscarTar_Click(object sender, EventArgs e)
        //{
        //    //Buscamos un item en listbox1

        //    for (int i = 0; i < ListBox1Tar.Items.Count - 1; i++)
        //    {
        //        if (ListBox1Tar.Items[i].Text.Contains(TxtBuscaTar.Text))
        //        {
        //            string a = ListBox1Tar.Items[i].Text;
        //            a = ListBox1Tar.Items[i].Value;
        //            ListBox1Tar.Items[i].Selected = true;
        //            ListBox1IDTar.Items[i].Selected = true;
        //            //ListBox1Col.Items[i].Selected = true;
        //            break;
        //        }
        //    }
        //    for (int i = 0; i < ListBox2Tar.Items.Count - 1; i++)
        //    {
        //        if (ListBox2Tar.Items[i].Text.Contains(TxtBuscaTar.Text))
        //        {
        //            string a = ListBox2Tar.Items[i].Text;
        //            a = ListBox2Tar.Items[i].Value;
        //            ListBox2Tar.Items[i].Selected = true;
        //            ListBox2IDTar.Items[i].Selected = true;
        //            //ListBox2Col.Items[i].Selected = true;
        //            break;
        //        }
        //    }


        //}
        //protected void btnLimpiarTar_Click(object sender, EventArgs e)
        //{
        //    //
        //    divTarea.Visible = false;
        //    divNePlantilla.Visible = true;
        //    SqlParameter[] dbParams = new SqlParameter[0];

        //    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZHANDS", dbParams));
        //    LbIDTarea.Text = (MiID + 1).ToString();

        //    if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }

        //    //col6List1.Visible = false;
        //    //col6Relacion.Visible = false;
        //    //col6List2.Visible = false;
        //    //DivColTarea.Visible = true;
        //    //DivTextTarea.Visible = true;

        //    ////Eliminamos todos los registros del listbox2
        //    //for (int i = 0; i <= ListBox2Tar.Items.Count - 1; i++)
        //    //{
        //    //    ListBox1Tar.Items.Add(ListBox2Tar.Items[i]);
        //    //    ListBox1IDTar.Items.Add(ListBox2IDTar.Items[i]);
        //    //}
        //    //ListBox2Tar.Items.Clear();
        //    //ListBox2IDTar.Items.Clear();
        //    //ListKeys.Items.Clear();

        //}

        //protected void BtSelUser_Click(object sender, EventArgs e)
        //{
        //    //aqui
        //    string SQL = "SELECT ZID,ZID_USUARIO,ZID_ARCHIVO,ZID_CAMPO,ZID_FLUJO,ZID_ESTADO,ZID_PROCESO ";
        //    SQL += ", ZID_PLANTILLA, ZID_PROFILES, ZID_MARCADOR, ZID_ALMACEN, ZID_DUMMIE, ZID_HAND ";
        //    SQL += " FROM ZUSUARIOPROCESOS ";
        //    SQL += " WHERE ZID_USUARIO = " + DrUsuarios.SelectedValue;
        //    SQL += " AND ZID_HAND = " + LbIDTarea.Text;

        //    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
        //    if (Con == null)
        //    {

        //        SqlParameter[] dbParams = new SqlParameter[0];
        //        SQL = "INSERT INTO ZUSUARIOPROCESOS (ZID_USUARIO, ZID_HAND) ";
        //        SQL += " VALUES (" + DrUsuarios.SelectedValue + "," + LbIDTarea.Text + ")";
        //        DBHelper.ExecuteNonQuerySQL(SQL, dbParams);

        //        SQL = "SELECT A.ZID, A.ZID_HAND, B.ZID as ZID_USUARIO, B.ZALIAS ";
        //        SQL += " FROM ZUSUARIOPROCESOS A, ZUSUARIOS B ";
        //        SQL += " WHERE A.ZID_HAND = " + LbIDTarea.Text;
        //        SQL += " AND B.ZID = A.ZID_USUARIO ";

        //        System.Data.DataTable dtt = Main.BuscaLote(SQL).Tables[0];
        //        DrAsigUser.DataValueField = "ZID";
        //        DrAsigUser.DataTextField = "ZALIAS";
        //        DrAsigUser.DataSource = dtt;
        //        DrAsigUser.DataBind();
        //    }
        //    else
        //    {
        //        //El usuario ya existe
        //        //Mensaje 
        //        Lbmensaje.Text = "El Usuario ya está asignado en la lista de Tareas. ";
        //        cuestion.Visible = false;
        //        Asume.Visible = true;
        //        DvPreparado.Visible = true;
        //    }
        //}
        //protected void SubirTar_Click(object sender, EventArgs e)
        //{
        //    int J = ListBox2Tar.Rows - 1;
        //    string sTmp = "";
        //    string sTmpID = "";
        //    string sTmpCol = "";
        //    if (J == 0)
        //    {

        //    }
        //    else
        //    {
        //        int index = ListBox2Tar.SelectedIndex;
        //        if (index == 0) { return; }
        //        sTmp = ListBox2Tar.Items[index - 1].Text;
        //        sTmpID = ListBox2IDTar.Items[index - 1].Text;
        //        //sTmpCol = ListBox2Col.Items[index - 1].Text;
        //        ListBox2Tar.Items[index - 1].Text = ListBox2Tar.Items[index].Text;
        //        ListBox2IDTar.Items[index - 1].Text = ListBox2IDTar.Items[index].Text;
        //        //ListBox2Col.Items[index - 1].Text = ListBox2Col.Items[index].Text;
        //        ListBox2Tar.Items[index].Text = sTmp;
        //        ListBox2IDTar.Items[index].Text = sTmpID;
        //        //ListBox2Col.Items[index].Text = sTmpCol;
        //        if (index > 0)
        //        {
        //            ListBox2Tar.SelectedIndex = index - 1;
        //            ListBox2IDTar.SelectedIndex = index - 1;
        //            //ListBox2Col.SelectedIndex = index - 1;
        //        }

        //    }


        //}

        protected void btnGuardarRelacionTar_Click(object sender, EventArgs e)
        {
            //Guardar Relación
            //EditaMenu(0);
            //this.Session["Edicion"] = "0";

        }

        protected void btnCancelarRelacionTar_Click(object sender, EventArgs e)
        {

            //EditaMenu(0);
            //this.Session["Edicion"] = "0";

        }

        //protected void BajarTar_Click(object sender, EventArgs e)
        //{
        //    int J = ListBox2Tar.Rows - 1;
        //    string sTmp = "";
        //    string sTmpID = "";
        //    string sTmpCol = "";
        //    if (J == 0)
        //    {

        //    }
        //    else
        //    {
        //        int index = ListBox2Tar.SelectedIndex;
        //        if (index < ListBox2Tar.Items.Count)
        //        {
        //            if (index == ListBox2Tar.Items.Count - 1) { return; }
        //            sTmp = ListBox2Tar.Items[index + 1].Text;
        //            sTmpID = ListBox2IDTar.Items[index + 1].Text;
        //            //sTmpCol = ListBox2Col.Items[index + 1].Text;
        //            ListBox2Tar.Items[index + 1].Text = ListBox2Tar.Items[index].Text;
        //            ListBox2IDTar.Items[index + 1].Text = ListBox2IDTar.Items[index].Text;
        //            //ListBox2Col.Items[index + 1].Text = ListBox2Col.Items[index].Text;
        //            ListBox2Tar.Items[index].Text = sTmp;
        //            ListBox2IDTar.Items[index].Text = sTmpID;
        //            //ListBox2Col.Items[index].Text = sTmpCol;
        //            ListBox2Tar.SelectedIndex = index + 1;
        //            ListBox2IDTar.SelectedIndex = index + 1;
        //            //ListBox2Col.SelectedIndex = index + 1;
        //        }
        //    }

        //}

        //public void RelacionesTareas()
        //{
        //    int MiID = Convert.ToInt32(TxtIdMarcador.Text);
        //    DrDummie.Items.Clear();
        //    ListBox1Tar.Items.Clear();
        //    ListBox2Tar.Items.Clear();
        //    ListBox1IDTar.Items.Clear();
        //    ListBox2IDTar.Items.Clear();

        //    string SQL = "SELECT ZID , ZNOMBRE, ZCAMPO, ZENTRADA , ZSALIDA, ZPAGINA, ZX ";
        //    SQL += " FROM ZMARCADORES ";
        //    System.Data.DataTable dtMarcadores = Main.BuscaLote(SQL).Tables[0];

        //    SQL = " SELECT ZID, ZDESCRIPCION FROM ZDUMMIE ORDER BY ZID ";
        //    System.Data.DataTable dtDummie = Main.BuscaLote(SQL).Tables[0];
        //    DrDummie.DataValueField = "ZID";
        //    DrDummie.DataTextField = "ZDESCRIPCION";
        //    DrDummie.DataSource = dtDummie;
        //    DrDummie.DataBind();


        //    SQL = " SELECT ZID, ZALIAS FROM ZUSUARIOS ORDER BY ZID ";
        //    System.Data.DataTable dtusuario = Main.BuscaLote(SQL).Tables[0];
        //    DrUsuarios.DataValueField = "ZID";
        //    DrUsuarios.DataTextField = "ZALIAS";
        //    DrUsuarios.DataSource = dtusuario;
        //    DrUsuarios.DataBind();




        //    foreach (DataRow filas in dtMarcadores.Rows) //Tabla Archivos seleccionado consulta en tabla Procesos
        //    {
        //        if (TxtIdMarcador.Text == filas["ZID"].ToString())
        //        {
        //            //Si el identificador es tipo campo 5 
        //            if (filas["ZCAMPO"].ToString() == "5" )
        //            {
        //                int n;
        //                bool isNumeric = int.TryParse(filas["ZENTRADA"].ToString(), out n);
        //                if (isNumeric == true)
        //                {
        //                    for (int d = 0; d < DrDummie.Items.Count; d++)
        //                    {
        //                        if (DrDummie.Items[d].Value == filas["ZENTRADA"].ToString())
        //                        {
        //                            DrDummie.SelectedIndex = d;
        //                            this.Session["iddummie"] = DrDummie.Items[d].Value.ToString();
        //                            break;
        //                        }
        //                    }
        //                    CargaDummies(this.Session["iddummie"].ToString());

        //                    string LaMacro = "";
        //                    //Busca y concatena las macros del ZID seleccionado en ZENTRADA del ZDUMMIE
        //                    foreach (DataRow fila in dtMacro.Rows)
        //                    {
        //                        LaMacro += fila["ZHAND"].ToString() + Environment.NewLine;
        //                    }

        //                    //if (LaMacro != "")
        //                    //{
        //                        //if (Mistrazas == "1")
        //                        //{
        //                        //    string h = Main.Ficherotraza("Lanza Dummie --> Macro: " + idmarcador);
        //                        //}

        //                        //EjecutaDummie_Click(LaMacro, idmarcador);
        //                    //}
        //                }
        //                else
        //                {
        //                    //Sino, se queda como está y no muestra el las realciones


        //                }
        //            }
        //        }
        //    }

        //    //Si no hay Flujos
        //    if (dtMacro.Rows.Count > 0 )
        //    {
        //        //Si hay Estados asociados a es Flujo
        //        foreach (DataRow fila in dtMacro.Rows)//Campos
        //        {
        //            ListBox2Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //            ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //            ListBox2IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //        }
        //    }
        //    if (dtAntiMacro.Rows.Count > 0)
        //    {
        //        //los Estados no asociados
        //        foreach (DataRow fila in dtAntiMacro.Rows)//Campos
        //        {
        //            ListBox1Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //            ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //            ListBox1IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //        }
        //    }
        //}

        //private void CargaDummies(string id)
        //{
        //    //Busca la macro o conjunto de macros para lanzar con dummie
        //    string SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION, A.ZID as ZIDDUMMIE, A.ZDESCRIPCION ,B.ZORDEN ";
        //    SQL += " FROM ZDUMMIE A, ZDUMMIEHAND B, ZHANDS C ";
        //    SQL += " WHERE B.ZIDDUMMIE = A.ZID ";
        //    SQL += " AND B.ZIDHAND = C.ZID ";
        //    SQL += " ORDER BY B.ZORDEN, A.ZDESCRIPCION ,C.ZDESCRIPCION ";
        //    dtMacros = Main.BuscaLote(SQL).Tables[0];

        //    //Busca la macro o conjunto de macros para lanzar con dummie
        //    SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION, A.ZDESCRIPCION ,B.ZORDEN ";
        //    SQL += " FROM ZDUMMIE A, ZDUMMIEHAND B, ZHANDS C ";
        //    SQL += " WHERE B.ZIDDUMMIE = A.ZID ";
        //    SQL += " AND B.ZIDHAND = C.ZID ";
        //    SQL += " AND A.ZID = " + id;
        //    SQL += " ORDER BY B.ZORDEN, A.ZDESCRIPCION ,C.ZDESCRIPCION ";
        //    dtMacro = Main.BuscaLote(SQL).Tables[0];

        //    //Busca la macro o conjunto de macros para lanzar con dummie
        //    SQL = " SELECT C.ZHAND, C.ZID, C.ZDESCRIPCION ";
        //    SQL += " FROM ZHANDS C ";
        //    SQL += " ORDER BY C.ZID ";
        //    dtAntiMacro = Main.BuscaLote(SQL).Tables[0];
        //}

        //protected void DrDummie_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ListBox1Tar.Items.Clear();
        //    ListBox2Tar.Items.Clear();
        //    ListBox1IDTar.Items.Clear();
        //    ListBox2IDTar.Items.Clear();
        //    CargaDummies(DrDummie.SelectedValue);

        //    if (dtMacros.Rows.Count > 0)
        //    {
        //        //Si hay Estados asociados a es Flujo
        //        foreach (DataRow fila in dtMacros.Rows)//Campos
        //        {
        //            if(fila["ZIDDUMMIE"].ToString() == DrDummie.SelectedValue)
        //            {
        //                ListBox2Tar.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //                ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //                ListBox2IDTar.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //            }
        //        }
        //        Boolean Esta = false;
        //        foreach (DataRow filas in dtAntiMacro.Rows)//Campos
        //        {
        //            Esta = false;
        //            for (int i = 0; i < ListBox2IDTar.Items.Count; i++)
        //            {
        //                if (ListBox2IDTar.Items[i].Value == filas["ZID"].ToString())
        //                {
        //                    Esta = true;
        //                    break;
        //                }
        //            }
        //            if (Esta == false)
        //            {
        //                ListBox1Tar.Items.Add(new ListasID(filas["ZDESCRIPCION"].ToString(), Convert.ToInt32(filas["ZID"].ToString())).ToString());
        //                ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
        //                ListBox1IDTar.Items.Add(new ListasID(filas["ZID"].ToString(), Convert.ToInt32(filas["ZID"].ToString())).ToString());
        //            }
        //        }
        //    }
        //   if (this.Session["proceso"].ToString() == "marcador") { EditaMarcador(1); }
        //}

        //protected void btnCancelNewDummie_Click(object sender, EventArgs e)
        //{
        //    divTarea.Visible = true;
        //    divNePlantilla.Visible = false;
        //    DivRel.Visible = true;
        //    DivEdita.Visible = false;
        //}
        //protected void btnSaveNewDummie_Click(object sender, EventArgs e)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[0];

        //    string SQL = "SELECT ZID ";
        //    SQL += " FROM ZHANDS ";
        //    SQL += " WHERE ZID = " + LbIDTarea.Text;

        //    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
        //    if (Con == null)
        //    {
        //        string Column = "INSERT INTO ZHANDS (ZDESCRIPCION, ZHAND) ";
        //        Column += " VALUES('" + TxtDescTarea.Text + "','" + TxtDummieE.Text + "')";
        //        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
        //    }
        //    else
        //    {
        //        string Column = "UPDATE ZHANDS SET ZDESCRIPCION ='" + TxtDescTarea.Text + "', ZHAND = '" + TxtDummieE.Text + "'";
        //        Column += " WHERE ZID = " + LbIDTarea.Text;
        //        DBHelper.ExecuteNonQuerySQL(Column, dbParams);
        //    }
        //    divTarea.Visible = true;
        //    divNePlantilla.Visible = false;
        //    RelacionesTareas();
        //    TxtDescTarea.Text = "";
        //    TxtDummieE.Text = "";
        //}
        
        //protected void btnNewDummie_Click(object sender, EventArgs e)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    BtEditTarea.Visible = false;
        //    if (BtNewDummie.Text == "Nueva Plantilla")
        //    {
        //        //maximo id
        //        int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZDUMMIE", dbParams));
        //        LbIDPlantDummie.Text = (MiID + 1).ToString();

        //        TxtNewDummie.Text = "";
        //        DivRel.Visible = false;
        //        DivCancel.Visible = true;
        //        TxtNewDummie.Visible = true;
        //        DrDummie.Visible = false;
        //        BtNewDummie.Text = "Guardar Plantilla";
        //        BtNewDummie.Attributes["class"] = "btn btn-success btn-block";
        //    }
        //    else
        //    {
        //        //Guarda en base de datos con MAX
        //        string Column = "INSERT INTO ZDUMMIE (ZDESCRIPCION) ";
        //        Column += " VALUES('" + TxtNewDummie.Text + "')";
        //        DBHelper.ExecuteNonQuerySQL(Column, dbParams);

        //        DivRel.Visible = true;
        //        DivCancel.Visible = false;
        //        TxtNewDummie.Visible = false;
        //        DrDummie.Visible = true;
        //        BtNewDummie.Text = "Nueva Plantilla";
        //        BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
        //        RelacionesTareas();
        //    }
        //}
        //protected void btnCancelaTar_Click(object sender, EventArgs e)
        //{
        //    DivRel.Visible = true;
        //    DivCancel.Visible = false;
        //    TxtNewDummie.Visible = false;
        //    DrDummie.Visible = true;
        //    BtNewDummie.Text = "Nueva Plantilla";
        //    BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
        //}
        //protected void BtEliminaDummie_Click(object sender, EventArgs e)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[0];
        //    string Column = "DELETE FROM ZDUMMIE WHERE ZID = " + LbIDPlantDummie.Text;
        //    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

        //    DivRel.Visible = true;
        //    DivCancel.Visible = false;
        //    TxtNewDummie.Visible = false;
        //    DrDummie.Visible = true;
        //    BtNewDummie.Text = "Nueva Plantilla";
        //    BtNewDummie.Attributes["class"] = "btn btn-info btn-block";
        //}
        
    }
}