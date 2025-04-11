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

namespace Satelite
{
    public partial class ImportTabla : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;
        public string CampoOrden = "";

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

                    this.Session["TablaExterna"] = null;
                    this.Session["FormatoCampos"] = null;
                    this.Session["Conexion"] = null;

                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Server.Transfer("Inicio.aspx"); //Default
                    }


                    //LbIDArchivo.InnerHtml = "Relaciones ";
                    this.Session["IDArchivo"] = 1;


                    DataTable dt5 = new DataTable();
                    dt5 = Main.CargaCampos().Tables[0];
                    this.Session["Campos"] = dt5;

                    Actualiza_Archivos();

                    DrConexion.DataValueField = "ZID";
                    DrConexion.DataTextField = "ZDESCRIPCION";

                    DrConexionDest.DataValueField = "ZID";
                    DrConexionDest.DataTextField = "ZDESCRIPCION";

                    dt5 = new DataTable();
                    dt5 = Main.CargaConexiones().Tables[0];
                    //this.Session["Conexion"] = dt5;
                    DrConexion.DataSource = dt5; // EvaluacionSel.GargaQuery().Tables[0];
                    DrConexion.DataBind();

                    DrConexionDest.DataSource = dt5;
                    DrConexionDest.DataBind();

                    Relaciones(1, CampoOrden);
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
            }
            catch (Exception ex)
            {
                string a = Main.ETrazas("", "1", " AltaArchivos.Page_load --> Error:" + ex.Message);
                Server.Transfer("Login.aspx");
            }
        }

        protected void btBuscaFile_Click(Object sender, EventArgs e)
        {

        }
        protected void btCambiaBBDD_Click(Object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if(bt.ID == "BTIrBBDD")
            {
                BTIrArchivo.Visible = true;
                BTIrBBDD.Visible = false;
                lbTipoBBDD.Text = "Conexión BBDD:";
                DrArchivos.Visible = false;
                DrConexionDest.Visible = true;
                LbtablaDest.Visible = true;
                DrTablasDest.Visible = true;
            }
            else //BTIrArchivo
            {
                BTIrArchivo.Visible = false; 
                BTIrBBDD.Visible = true;
                lbTipoBBDD.Text = "Archivos:";
                DrArchivos.Visible = true;
                DrConexionDest.Visible = false;
                LbtablaDest.Visible = false;
                DrTablasDest.Visible = false;

            }

        }

        

        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu0") { this.Session["Menu"] = "0"; }
            if (Miro == "aMenu1") { this.Session["Menu"] = "1"; }
            Carga_Menus();
        }


        public void Carga_Menus()
        {
            //pagevistaform.Attributes["style"] = "";
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu0");
            HtmlGenericControl panel = (HtmlGenericControl)cont.FindControl("accordion0");

            if (this.Session["Menu"].ToString() == "0")
            {
                //el 0
                li = (HtmlGenericControl)cont.FindControl("Menu0");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "";

                panel = (HtmlGenericControl)cont.FindControl("MenuTablas");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("MenuArchivos");
                panel.Attributes["class"] = "tab-pane fade";

                MenuTablas.Visible = true;
                MenuArchivos.Visible = false;
            }

            if (this.Session["Menu"].ToString() == "1")
            {
                //el 1
                li = (HtmlGenericControl)cont.FindControl("Menu1");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu0");
                li.Attributes["class"] = "";
                
                panel = (HtmlGenericControl)cont.FindControl("MenuArchivos");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("MenuTablas");
                panel.Attributes["class"] = "tab-pane fade";

                MenuTablas.Visible = false;
                MenuArchivos.Visible = true;
                
            }
        }

        private void Actualiza_Archivos()
        {

            DrArchivos.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            DrArchivos.DataSource = dt;
            DrArchivos.DataBind();
            this.Session["Archivos"] = dt;
        }

        protected void dlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Consulta en Base de datos las conexion
            string Tabla = DrTablas.SelectedItem.Text;
            DataTable dt = Main.BuscaBBDDConexion(Convert.ToInt32(DrConexion.SelectedItem.Value) , Tabla).Tables[0];
#pragma warning disable CS0219 // La variable 'CambioTabla' está asignada pero su valor nunca se usa
            string CambioTabla = "";
#pragma warning restore CS0219 // La variable 'CambioTabla' está asignada pero su valor nunca se usa
            int i = 0;
            int a = 1;
            DataTable dtt = new DataTable("Tabla");
            ListBox1.Items.Clear();
            ListBox1ID.Items.Clear();
            ListBox1Col.Items.Clear();

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZIDCAMPO");
            dtt.Columns.Add("Tabla");
            dtt.Columns.Add("Columna");
            dtt.Columns.Add("Tipo");
            dtt.Columns.Add("Tamaño");
            dtt.Columns.Add("RELACION");
            dtt.Columns.Add("VALORRELACION");
            dtt.Columns.Add("VALORCOLUMNA");

            foreach (DataRow filacampo in dt.Rows)
            {
                ListBox1.Items.Add(new ListasID(filacampo["Columna"].ToString(), Convert.ToInt32(a)).ToString());
                ListBox1ID.Items.Add(new ListasID(filacampo["RELACION"].ToString().ToString(), Convert.ToInt32(a)).ToString());
                ListBox1Col.Items.Add(new ListasID(filacampo["VALORCOLUMNA"].ToString(), Convert.ToInt32(a)).ToString());
                


                DataRow drt;

                drt = dtt.NewRow();
                drt[0] = i;
                drt[1] = a;
                drt[2] = filacampo["Tabla"].ToString();
                drt[3] = filacampo["Columna"].ToString();
                drt[4] = filacampo["Tipo"].ToString();
                drt[5] = filacampo["Tamaño"].ToString();
                drt[6] = filacampo["RELACION"].ToString();
                drt[7] = filacampo["VALORRELACION"].ToString();
                drt[8] = filacampo["VALORCOLUMNA"].ToString();

                dtt.Rows.Add(drt);
                a += 1;
            }

            this.Session["CamposExterna"] = dtt;
        }


        protected void DrArchivos2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            DrArchivos.BackColor = Color.FromName("#bdecb6");
            chkKey.Checked = false;
            DataTable dt = this.Session["Archivos"] as DataTable;
            foreach (DataRow fila in dt.Rows)
            {

                if (fila["ZID"].ToString() == DrArchivos.SelectedItem.Value)
                {
                    //LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + DrArchivos.SelectedItem.Value;


                    //Djerarquia.SelectedIndex = Convert.ToInt32(fila["ZROOT"].ToString());
                    //Dtipo.SelectedIndex = Convert.ToInt32(fila["ZTIPO"].ToString());
                    //dlEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());



                    this.Session["IDArchivo"] = Convert.ToInt32(fila["ZID"].ToString());
                    Relaciones(Convert.ToInt32(fila["ZID"].ToString()), CampoOrden);

              
                    //DrDuplicado.Text = fila["ZDUPLICADOS"].ToString();
                    for (int u = 0; u <= DrConexion.Items.Count - 1; u++)
                    {
                        if (DrConexion.Items[u].Value == fila["ZCONEXION"].ToString())
                        {
                            DrConexion.SelectedIndex = u;
                            break;
                        }
                    }
                    //PopulateRootLevel(Buscaregistro);
                    DataTable dtVolumen = Main.CargaVolumen(fila["ZID_VOLUMEN"].ToString(), DrArchivos.SelectedItem.Value).Tables[0];
                    string Volumen = "";

#pragma warning disable CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
                    Boolean Esta = false;
#pragma warning restore CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
                    foreach (DataRow filaVol in dtVolumen.Rows)
                    {
                        Esta = true;
                        Volumen = filaVol["ZRUTA"].ToString();
                        break;
                    }
  
                    //TxtMail.Text = fila["ZEMAIL"].ToString();
                    break;
                }

                //string valor = fila["NombreDeLaColumna"].ToString();//por indice campo string valor = fila[0].ToString();

            }
        }

        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            DrArchivos.BackColor = Color.FromName("#bdecb6");
            chkKey.Checked = false;
            DataTable dt = this.Session["Archivos"] as DataTable;
            foreach (DataRow fila in dt.Rows)
            {

                if (fila["ZID"].ToString() == DrArchivos.SelectedItem.Value)
                {
                    //LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + DrArchivos.SelectedItem.Value;

                  

                    //Djerarquia.SelectedIndex = Convert.ToInt32(fila["ZROOT"].ToString());
                    //Dtipo.SelectedIndex = Convert.ToInt32(fila["ZTIPO"].ToString());
                    //dlEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());

                    

                    this.Session["IDArchivo"] = Convert.ToInt32(fila["ZID"].ToString());
                    Relaciones(Convert.ToInt32(fila["ZID"].ToString()), CampoOrden);

                    //DrDuplicado.Text = fila["ZDUPLICADOS"].ToString();
                    for (int u = 0; u <= DrConexion.Items.Count - 1; u++)
                    {
                        if (DrConexion.Items[u].Value == fila["ZCONEXION"].ToString())
                        {
                            DrConexion.SelectedIndex = u;
                            break;
                        }
                    }
                    //DrConexion.Text = fila["ZCONEXION"].ToString();
                    //tvControl.Nodes.Clear();
                    registro = Convert.ToInt32(fila["ZID"].ToString());
                    //Busca_Root(Convert.ToInt32(Convert.ToInt32(fila["ZID"].ToString())));
                    Contador = 0;

                    //PopulateRootLevel(Buscaregistro);
                    DataTable dtVolumen = Main.CargaVolumen(fila["ZID_VOLUMEN"].ToString(), DrArchivos.SelectedItem.Value).Tables[0];
                    string Volumen = "";

#pragma warning disable CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
                    Boolean Esta = false;
#pragma warning restore CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
                    foreach (DataRow filaVol in dtVolumen.Rows)
                    {
                        Esta = true;
                        Volumen = filaVol["ZRUTA"].ToString();
                        break;
                    }
                    //TxtMail.Text = fila["ZEMAIL"].ToString();
                    break;
                }

                //string valor = fila["NombreDeLaColumna"].ToString();//por indice campo string valor = fila[0].ToString();

            }
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

        protected void dlConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Consulta en Base de datos las conexion
            string Tabla = "";
            DataTable dt = Main.BuscaBBDDConexion(Convert.ToInt32(DrConexion.SelectedItem.Value), Tabla).Tables[0];
            string CambioTabla = "";
            int i = 0;
            int a = 1;
            DataTable dtd = new DataTable("Tabla");
            DataTable dtt = new DataTable("Tabla");

            DrTablas.Items.Clear();
            DrTablas.DataValueField = "ZID";
            DrTablas.DataTextField = "ZDESCRIPCION";

            dtd.Columns.Add("ZID");
            dtd.Columns.Add("ZDESCRIPCION");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZIDCAMPO");
            dtt.Columns.Add("Tabla");
            dtt.Columns.Add("Columna");
            dtt.Columns.Add("Tipo");
            dtt.Columns.Add("Tamaño");
            dtt.Columns.Add("RELACION");
            dtt.Columns.Add("VALORRELACION");
            dtt.Columns.Add("VALORCOLUMNA");

            //Password=L0sViv3r0s.Fr3sas;Persist Security Info=True;User ID=RioEresmaCon;Initial Catalog=NET_VIVA22;Data Source=SERVIDOR-VER\GOLDENNET
            string[] Partes = System.Text.RegularExpressions.Regex.Split(this.Session["Conexion"].ToString(), ";");
            string Server = "";
            string Catalogo = "";

            for (int x = 0; x < Partes.Count() ; x++)
            {
                if (Partes[x].Contains("Data Source"))
                {
                    Server = Partes[x].Replace("Data Source=", "");
                }
                if (Partes[x].Contains("Catalog"))
                {
                    Catalogo = Partes[x].Replace("Initial Catalog=", "");

                }
            }
            LbOrigen.Text = "ORIGEN: " + Server + " (" + Catalogo + ")";

            foreach (DataRow filacampo in dt.Rows)
            {
                if (CambioTabla != filacampo["Tabla"].ToString())
                {
                    CambioTabla = filacampo["Tabla"].ToString();
                    i += 1;
                    DataRow drr;

                    drr = dtd.NewRow();
                    drr[0] = i;
                    drr[1] = CambioTabla;
                    dtd.Rows.Add(drr);
                }
                    
                DataRow drt;                     

                drt = dtt.NewRow();
                drt[0] = i;
                drt[1] = a;
                drt[2] = filacampo["Tabla"].ToString();
                drt[3] = filacampo["Columna"].ToString();
                drt[4] = filacampo["Tipo"].ToString();
                drt[5] = filacampo["Tamaño"].ToString();
                drt[6] = filacampo["RELACION"].ToString();
                drt[7] = filacampo["VALORRELACION"].ToString();
                drt[8] = filacampo["VALORCOLUMNA"].ToString();

                dtt.Rows.Add(drt);
                a += 1;
            }

            DrTablas.DataSource = dtd;
            DrTablas.DataBind();

            this.Session["TablaExterna"] = dtt;
        }

        protected void dlConexionDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Consulta en Base de datos las conexion
            string Tabla = "";
            DataTable dt = Main.BuscaBBDDConexion(Convert.ToInt32(DrConexionDest.SelectedItem.Value), Tabla).Tables[0];
            string CambioTabla = "";
            int i = 0;
            int a = 1;
            DataTable dtd = new DataTable("Tabla");
            DataTable dtt = new DataTable("Tabla");

            DrTablasDest.Items.Clear();
            DrTablasDest.DataValueField = "ZID";
            DrTablasDest.DataTextField = "ZDESCRIPCION";

            dtd.Columns.Add("ZID");
            dtd.Columns.Add("ZDESCRIPCION");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZIDCAMPO");
            dtt.Columns.Add("Tabla");
            dtt.Columns.Add("Columna");
            dtt.Columns.Add("Tipo");
            dtt.Columns.Add("Tamaño");
            dtt.Columns.Add("RELACION");
            dtt.Columns.Add("VALORRELACION");
            dtt.Columns.Add("VALORCOLUMNA");


            foreach (DataRow filacampo in dt.Rows)
            {
                if (CambioTabla != filacampo["Tabla"].ToString())
                {
                    CambioTabla = filacampo["Tabla"].ToString();
                    i += 1;
                    DataRow drr;

                    drr = dtd.NewRow();
                    drr[0] = i;
                    drr[1] = CambioTabla;
                    dtd.Rows.Add(drr);
                }

                DataRow drt;

                drt = dtt.NewRow();
                drt[0] = i;
                drt[1] = a;
                drt[2] = filacampo["Tabla"].ToString();
                drt[3] = filacampo["Columna"].ToString();
                drt[4] = filacampo["Tipo"].ToString();
                drt[5] = filacampo["Tamaño"].ToString();
                drt[6] = filacampo["RELACION"].ToString();
                drt[7] = filacampo["VALORRELACION"].ToString();
                drt[8] = filacampo["VALORCOLUMNA"].ToString();

                dtt.Rows.Add(drt);
            }

            DrTablasDest.DataSource = dtd;
            DrTablasDest.DataBind();

            this.Session["TablaExternaDestino"] = dtt;

        }

        protected void dlTablasDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Consulta en Base de datos las conexion
            string Tabla = DrTablasDest.SelectedItem.Text;
            DataTable dt = Main.BuscaBBDDConexion(Convert.ToInt32(DrConexionDest.SelectedItem.Value), Tabla).Tables[0];
#pragma warning disable CS0219 // La variable 'CambioTabla' está asignada pero su valor nunca se usa
            string CambioTabla = "";
#pragma warning restore CS0219 // La variable 'CambioTabla' está asignada pero su valor nunca se usa
            int i = 0;
            int a = 1;
            DataTable dtt = new DataTable("Tabla");
            ListBox2.Items.Clear();
            ListBox2ID.Items.Clear();
            ListBox2Col.Items.Clear();
            ListBox2Form.Items.Clear();

            foreach (DataRow filacampo in dt.Rows)
            {
                ListBox2.Items.Add(new ListasID(filacampo["Columna"].ToString(), Convert.ToInt32(a)).ToString());
                ListBox2ID.Items.Add(new ListasID(filacampo["RELACION"].ToString().ToString(), Convert.ToInt32(a)).ToString());
                ListBox2Form.Items.Add(new ListasID(filacampo["RELACION"].ToString().ToString(), Convert.ToInt32(a)).ToString());
                ListBox2Col.Items.Add(new ListasID(filacampo["VALORCOLUMNA"].ToString(), Convert.ToInt32(a)).ToString());
                a += 1;


                DataRow drt;

                drt = dtt.NewRow();
                drt[0] = i;
                drt[1] = a;
                drt[2] = filacampo["Tabla"].ToString();
                drt[3] = filacampo["Columna"].ToString();
                drt[4] = filacampo["Tipo"].ToString();
                drt[5] = filacampo["Tamaño"].ToString();
                drt[6] = filacampo["RELACION"].ToString();
                drt[7] = filacampo["VALORRELACION"].ToString();
                drt[8] = filacampo["VALORCOLUMNA"].ToString();

                dtt.Rows.Add(drt);
            }

            this.Session["CamposDestinoExterna"] = dtt;
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

                //int Existe = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT COUNT(ZID) FROM ZARCHIVOS WHERE ZIDTABLA = '" + TxtNombre.Text + "' AND ZDESCRIPCION = '" + TxtDescripcion.Text + "' AND ZTABLENAME = '" + TablaName.Text + "'", dbParams));

                //if(Existe == 0)
                //{
                //    int M = 0;
                //    //if (DrCampoasig.SelectedValue == "") { M = 0; } else { M = Convert.ToInt32(DrCampoasig.SelectedValue); }
                //    Column = "INSERT INTO " + Tabla + " (ZIDTABLA, ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZKEY, ZDUPLICADOS) ";
                //    //ColumnVal = " VALUES('" + TxtNombre.Text + "','" + TxtDescripcion.Text + "'," + dlNivel.SelectedValue + "," + Djerarquia.SelectedValue + ",'" + TablaName.Text + "','" + TablaObj.Text + "'," + Dtipo.SelectedValue + "," + dlEstado.SelectedValue + "," + DrConexion.SelectedValue + "," + M + "," + DrDuplicado.SelectedValue + ")";
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
                    //Column += "ZKEY ='" + DrCampoasig.SelectedValue + "', ";
                    //Column += "ZVIEW ='" + CommentSQL.Text + "' ";
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
                    //DivSQL.Visible = false;
                    //DivCampoDer.Visible = true;
                    this.Session["Edicion"] = 0;
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
            DrArchivos.Enabled = true;
            DrArchivos.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";


            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            this.Session["Archivo"] = dt;
            DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
            DrArchivos.DataBind();


            DesactivarTxt();
            Limpiar();

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

            //LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + MiID;
            DrConexion.SelectedIndex = 0;

            //TextDuplicado.Text = "";
            //dlNivel.Text = "";
            //TxtMail.Text = "";
            this.Session["Edicion"] = 0;
        }

        protected void ListBox1ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1ID.SelectedIndex;
            ListBox1Col.SelectedIndex = ListBox1ID.SelectedIndex;
            ListBox1.SelectedIndex = ListBox1ID.SelectedIndex;

            string a = ListBox1ID.SelectedValue;


            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo1.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId1.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo1.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize1.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }
        }

        protected void ListBox1Col_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1Col.SelectedIndex;
            ListBox1ID.SelectedIndex = ListBox1Col.SelectedIndex;
            ListBox1.SelectedIndex = ListBox1Col.SelectedIndex;

            string a = ListBox1ID.SelectedValue;


            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo1.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId1.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo1.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize1.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }
        }
        

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1.SelectedIndex;
            ListBox1ID.SelectedIndex = ListBox1.SelectedIndex;
            ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;

            string a = ListBox1ID.SelectedValue;


            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a ;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo1.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId1.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo1.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize1.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }
        }

        protected void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2.SelectedIndex;
            ListBox2ID.SelectedIndex = ListBox2.SelectedIndex;
            ListBox2Col.SelectedIndex = ListBox2.SelectedIndex;
            ListKeys.SelectedIndex = ListBox2.SelectedIndex;
            string a = ListBox2ID.SelectedValue;
            string b = ListKeys.SelectedValue;
            chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));

            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo2.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId2.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo2.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize2.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }           
        }

        protected void ListBox2Col_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2Col.SelectedIndex;
            ListBox2ID.SelectedIndex = ListBox2Col.SelectedIndex;
            ListBox2.SelectedIndex = ListBox2Col.SelectedIndex;
            ListKeys.SelectedIndex = ListBox2Col.SelectedIndex;

            string a = ListBox2ID.SelectedValue;
            string b = ListKeys.SelectedValue;
            chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));

            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo2.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId2.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo2.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize2.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }
        }

        protected void ListBox2ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox2ID.SelectedIndex;
            ListBox2Col.SelectedIndex = ListBox2ID.SelectedIndex;
            ListBox2.SelectedIndex = ListBox2ID.SelectedIndex;
            ListKeys.SelectedIndex = ListBox2ID.SelectedIndex;

            string a = ListBox2ID.SelectedValue;
            string b = ListKeys.SelectedValue;
            chkKey.Checked = Convert.ToBoolean(Convert.ToInt32(b));

            string SQL = " SELECT A.ZID, A.ZNIVEL, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, A.ZVALOR , B.ZFORMATO ";
            SQL += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
            SQL += " WHERE A.ZTIPO = B.ZID ";
            SQL += " AND A.ZID = " + a;
            SQL += " AND ZNIVEL <= " + this.Session["MiNivel"] + " ORDER BY ZID ";
            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow fila in dtCampos.Rows)
            {
                if (fila["ZID"].ToString() == a)
                {
                    LbCampo2.Text = "Campo:" + fila["ZTITULO"].ToString();
                    LbId2.Text = "ID:" + fila["ZID"].ToString();
                    LbTipo2.Text = "Tipo:" + fila["ZFORMATO"].ToString();
                    LbSize2.Text = "Tamaño:" + fila["ZVALOR"].ToString();
                    break;
                }
            }
        }

        public void Relaciones(int ID, string sortExpression = null)
        {
            int MiID = ID;
            ListBox2.Items.Clear();
            ListBox2ID.Items.Clear();
            ListBox2Col.Items.Clear();
            ListBox2Form.Items.Clear();
            ListKeys.Items.Clear();



            DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dtTemporal = new DataTable();
            DataTable dt1 = new DataTable();

            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];
            this.Session["ArchivoCampos"] = dt1;

            DataTable dtFormato = Main.CargaFormatoCampos().Tables[0];
            this.Session["FormatoCampos"] = dtFormato;
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

            int i = 0;
            string Key = "";
            string x = "";

            if (dt1.Rows.Count == 0)
            {
                //Archivo sin campos, muestrta todos para seleccionar, ahora primero tabla temporal porque el control list y dropdownlist web no tiene Sorted
                foreach (DataRow fila in dt.Rows)//Campos
                {
                    drr = dtt.NewRow();
                    drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                    drr[1] = fila["ZDESCRIPCION"].ToString();
                    dtt.Rows.Add(drr);
                }
                drr = dto.NewRow();
                drr[0] = Convert.ToInt32(0);
                drr[1] = "";
                dto.Rows.Add(drr);
            }
            else
            {
                //si tiene Relacion archivos y campos
                foreach (DataRow dr in dt1.Rows)
                {
                    Key = dr["ZKEY"].ToString();
                    visto = 0;
                    foreach (DataRow fila in dt.Rows)//Campos
                    {
                        if (fila["ZID"].ToString() == dr["ZID_CAMPO"].ToString())
                        {
                            if (fila["ZID"].ToString() == Key)
                            {
                                CampoOrden = fila["ZTITULO"].ToString();
                            }

                            foreach (DataRow miFormat in dtFormato.Rows)
                            {
                                if (fila["ZTIPO"].ToString() == miFormat["ZID"].ToString())
                                {
                                    drr = dto.NewRow();
                                    drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                                    drr[1] = fila["ZDESCRIPCION"].ToString();
                                    dto.Rows.Add(drr);

                                    ListBox2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                                    ListBox2Form.Items.Add(new ListasID(fila["ZFORMATO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                                    ListBox2ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                                    ListBox2Col.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());

                                    if (dr["KEYCAMPO"].ToString() == "")
                                    {
                                        string a = "0";
                                        ListKeys.Items.Add(new ListasID(a, Convert.ToInt32(a)).ToString());
                                    }
                                    else
                                    {
                                        if (dr["KEYCAMPO"].ToString() == "1")
                                        {
                                            if (x == "")
                                            {
                                                x = fila["ZDESCRIPCION"].ToString();
                                            }
                                            else
                                            {
                                                x += ", " + fila["ZDESCRIPCION"].ToString();
                                            }
                                        }
                                        ListKeys.Items.Add(new ListasID(dr["KEYCAMPO"].ToString(), Convert.ToInt32(dr["KEYCAMPO"].ToString())).ToString());
                                    }
                                    visto = 1;
                                    break;
                                }
                            }
                            i += 1;
                            break;
                        }
                    }
                }
            }
            LbCampoCount.Text = i.ToString();  
        }

        protected void DrTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            //LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + MiID;
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
            DrArchivos.Enabled = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = 2;

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
            }
        }
        
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Eliminamos todos los registros del listbox
            //ListBox1.Items.Clear();
            //for (int i = 0; i <= ListBox2.Items.Count - 1; i++)
            //{
            //    ListBox1.Items.Add(ListBox2.Items[i]);
            //    ListBox1ID.Items.Add(ListBox2ID.Items[i]);
            //}
            ListBox2.Items.Clear();
            ListBox2ID.Items.Clear();
            ListBox2Col.Items.Clear();
            ListKeys.Items.Clear();
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

            while (ListBox1.GetSelectedIndices().Length > 0)
            {
                ListBox3.Items.Add(ListBox1.SelectedItem);
                ListBox3ID.Items.Add(ListBox1ID.SelectedItem);
                ListBox3Form.Items.Add(ListBox1Col.SelectedItem);
            }
        }

        protected void btnRegresarSeleccionados_Click(object sender, EventArgs e)
        {
            //Regresamos los items seleccionados de listbox2 a listbox1

            while (ListBox2.GetSelectedIndices().Length > 0)
            {
                ListBox4.Items.Add(ListBox2.SelectedItem);
                ListBox4ID.Items.Add(ListBox2ID.SelectedItem);
                ListBox4Form.Items.Add(ListBox2Col.SelectedItem);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en listbox1

            //for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            //{
            //    if (ListBox1.Items[i].Text.Contains(TextBox1.Text))
            //    {
            //        string a = ListBox1.Items[i].Text;
            //        a = ListBox1.Items[i].Value;
            //        ListBox1.Items[i].Selected = true;
            //        ListBox1ID.Items[i].Selected = true;
            //        ListBox1Col.Items[i].Selected = true;
            //        break;
            //    }
            //}
            //for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            //{
            //    if (ListBox2.Items[i].Text.Contains(TextBox1.Text))
            //    {
            //        string a = ListBox2.Items[i].Text;
            //        a = ListBox2.Items[i].Value;
            //        ListBox2.Items[i].Selected = true;
            //        ListBox2ID.Items[i].Selected = true;
            //        ListBox2Col.Items[i].Selected = true;
            //        break;
            //    }
            //}

            //    if (ListBox1.Items.FindByText(TextBox1.Text) != null)
            //{
            //    ListBox1.Items.FindByText(TextBox1.Text).Selected = true;
            //}
        }

        public void DesactivarTxt()
        {

            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2Form.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Button1.Enabled = false;
            Button2.Enabled = false;
            Button4.Enabled = false;
            Button6.Enabled = false;
            Button7.Enabled = false;
        }

        public void ActivarTxt()
        {
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2Form.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
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
            //TextBox1.Text = tvControl.SelectedNode.Text;

            //DataTable dt = new DataTable();
            //dt = Main.TVCargaCampos(Convert.ToInt32(tvControl.SelectedNode.Value), Convert.ToInt32((string)Session["MiNivel"])).Tables[0];


        }

        protected void Subir_Click(object sender, EventArgs e)
        {
            int J = ListBox2.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
            string sTmpCol = "";
            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2.SelectedIndex;
                if (index == 0) { return; }
                sTmp = ListBox2.Items[index - 1].Text;
                sTmpID = ListBox2ID.Items[index - 1].Text;
                sTmpCol = ListBox2Col.Items[index - 1].Text;
                ListBox2.Items[index - 1].Text = ListBox2.Items[index].Text;
                ListBox2ID.Items[index - 1].Text = ListBox2ID.Items[index].Text;
                ListBox2Col.Items[index - 1].Text = ListBox2Col.Items[index].Text;
                ListBox2.Items[index].Text = sTmp;
                ListBox2ID.Items[index].Text = sTmpID;
                ListBox2Col.Items[index].Text = sTmpCol;
                if (index > 0)
                {
                    ListBox2.SelectedIndex = index - 1;
                    ListBox2ID.SelectedIndex = index - 1;
                    ListBox2Col.SelectedIndex = index - 1;
                }

            }

        }

        protected void Bajar_Click(object sender, EventArgs e)
        {
            int J = ListBox2.Rows - 1;
            string sTmp = "";
            string sTmpID = "";
            string sTmpCol = "";
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
                    sTmpCol = ListBox2Col.Items[index + 1].Text;
                    ListBox2.Items[index + 1].Text = ListBox2.Items[index].Text;
                    ListBox2ID.Items[index + 1].Text = ListBox2ID.Items[index].Text;
                    ListBox2Col.Items[index + 1].Text = ListBox2Col.Items[index].Text;
                    ListBox2.Items[index].Text = sTmp;
                    ListBox2ID.Items[index].Text = sTmpID;
                    ListBox2Col.Items[index].Text = sTmpCol;
                    ListBox2.SelectedIndex = index + 1;
                    ListBox2ID.SelectedIndex = index + 1;
                    ListBox2Col.SelectedIndex = index + 1;
                }
            }

        }

        protected void TxtGrupoName_TextChanged(object sender, EventArgs e)
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

        protected void DrCampoasig_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Identificación de campo clave ZKEY, si está en edición asignarlo y poner en verde
            DrArchivos.BackColor = Color.FromName("#ffffff");

            if(this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "2")
            {
                //DrCampoasig.BackColor = Color.FromName("#bdecb6");
            }
        }

        protected void DrDuplicado_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                Lbmensaje.Text = "Ejecución de Consulta correcta. ";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
            }
            catch(Exception ex)
            {
                Lbmensaje.Text = "Error. " + ex.Message;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
            }
        }

        protected void BtconectaRep_Click(object sender, EventArgs e)
        {
            //Prueba conexion a base de datos
            if (DrConexion.SelectedIndex == 0) { return; }
            string SQL = "";

            try
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SQL = DBHelper.ExecuteScalarSQL("SELECT ZCONECTSTRING FROM ZCONEXION WHERE ZID = " + DrConexion.SelectedIndex, dbParams).ToString();

                //SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQLRep.Text, dbParams, SQL).ToString();
                //Lbmensaje.Text = "Ejecución de Consulta correcta. ";
                //SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQLDoc.Text, dbParams, SQL).ToString();
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
            }
            catch (Exception ex)
            {
                Lbmensaje.Text = "Error. " + ex.Message;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
            }
        }

        protected void sellectKey(object sender, EventArgs e)
        {
            //Busco el select id y escribo en la lista keys para guardar
            string a = this.Session["Edicion"].ToString();
            if (this.Session["Edicion"].ToString() == "0" )
            {
                if (chkKey.Checked == true)
                {
                    chkKey.Checked = false;
                }
                else
                {
                    chkKey.Checked = true;
                }
                return;
            }
            if (chkKey.Checked == true)
            {
                int index = ListKeys.SelectedIndex;
                ListKeys.Items[index].Text = "1";
                ListKeys.Items[index].Value = "1";
            }
            else
            {
                int index = ListKeys.SelectedIndex;
                ListKeys.Items[index].Text = "0";
                ListKeys.Items[index].Value = "0";
            }

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

            //Password=L0sViv3r0s.Fr3sas;Persist Security Info=True;User ID=RioEresmaCon;Initial Catalog=NET_VIVA22;Data Source=192.168.1.80
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
        }

        protected void BtTipo_Click(object sender, EventArgs e)
        {
            
        }

        protected void MigraRep_Click(object sender, EventArgs e)
        {
            //Copia la vista de datos en local y genera la tabla de objetos

        }

        protected void BtTipoRep_Click(object sender, EventArgs e)
        {

        }

        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }
    }
}