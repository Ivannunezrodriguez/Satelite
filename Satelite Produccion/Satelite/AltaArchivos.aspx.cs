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

//Modificacion 25/03/2024
//Integracion de filtro para listado de tablas

namespace Satelite
{
    public partial class AltaArchivos : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;
        public string CampoOrden = "";
        //public string MiColor = "#ffffff";
        
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
                Variables.GetData = "0";
                string a = Main.ETrazas("", "0", " AltaArchivos.Page_load --> Envia a Server.Transfer(Login.aspx). La Sesion:" + Session["Session"].ToString());
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
                    Variables.MiColor = "#fffff";
                    this.Session["IndiceCampo"] = "0";

                    TextBox1.Attributes.Add("placeholder", "Buscar...");
                    CampoOrden = "ZDESCRIPCION";
                    //TSeleccion.InnerHtml = this.Session["UserName"].ToString();
                    //LbAlias.Text = this.Session["UserAlias"].ToString();
                    //LbCargo.Text = this.Session["UserDescripcion"].ToString();
                    //lbindentificacion.Text = this.Session["UserIdentificacion"].ToString();
                    this.Session["FormatoCampos"] = null;

                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Server.Transfer("Inicio.aspx"); //Default
                    }


                    LbIDArchivo.InnerHtml = "Relaciones ";
                    this.Session["IDArchivo"] = 1;

                    dlNivel.DataValueField = "ZID";
                    dlNivel.DataTextField = "ZDESCRIPCION";

                    //if (this.Session["Niveles"] != null)
                    //{
                    //    DataTable dt3 = this.Session["Niveles"] as DataTable;
                    //    dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                    //    dlNivel.DataBind();
                    //}
                    //else
                    //{
                        DataTable dt3 = new DataTable();
                        dt3 = Main.CargaNivel().Tables[0];
                        this.Session["Niveles"] = dt3;
                        dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                        dlNivel.DataBind();
                    //}


                    //if (this.Session["Campos"] != null)
                    //{
                    //    //ya esta cargado
                    //}
                    //else
                    //{
                        DataTable dt = new DataTable();
                        dt = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dt;
                    //}

                    Actualiza_Archivos();

                    //DrArchivos.DataValueField = "ZID";
                    //DrArchivos.DataTextField = "ZDESCRIPCION";

                    //Djerarquia.DataValueField = "ZID";
                    //Djerarquia.DataTextField = "ZDESCRIPCION";

                    ////if (this.Session["Archivos"] != null)
                    ////{
                    ////    DataTable dt = this.Session["Archivos"] as DataTable;
                    ////    DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
                    ////    DrArchivos.DataBind();

                    ////    Djerarquia.DataSource = dt;
                    ////    Djerarquia.DataBind();

                    ////}
                    ////else
                    ////{
                    //DrArchivos.Items.Clear();
                    //Djerarquia.Items.Clear();

                    //dt = new DataTable();
                    //dt = Main.CargaArchivos().Tables[0];

                    //Peticion de ordenar por Descripcion el 31/03/2022
                    //Introducir el Campo para ordenar por el
                    //string Orden = "ZDESCRIPCION";
                    //this.Session["Archivos"] = dt;

                    ////if (Orden != null)
                    ////{
                    ////    DataView dv = dt.AsDataView();
                    ////    this.SortDirection = "ASC";

                    ////    dv.Sort = Orden + " " + this.SortDirection;
                    ////    DrArchivos.DataSource = dv.ToTable();
                    ////    Djerarquia.DataSource = dv.ToTable();
                    ////}
                    ////else
                    ////{
                    //    DrArchivos.DataSource = dt;
                    //    Djerarquia.DataSource = dt;
                    ////}
                    //DrArchivos.DataBind();
                    //Djerarquia.DataBind();
                    
                    dlEstado.DataValueField = "ZID";
                    dlEstado.DataTextField = "ZDESCRIPCION";

                    //if (this.Session["Estados"] != null)
                    //{
                    //    DataTable dt4 = this.Session["Estados"] as DataTable;
                    //    dlEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                    //    dlEstado.DataBind();
                    //}
                    //else
                    //{
                        DataTable dt4 = new DataTable();
                        dt4 = Main.CargaEstados().Tables[0];
                        this.Session["Estados"] = dt4;
                        dlEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        dlEstado.DataBind();
                    //}

                    DrConexion.DataValueField = "ZID";
                    DrConexion.DataTextField = "ZDESCRIPCION";
                    DataTable dt5 = new DataTable();
                    dt5 = Main.CargaConexiones().Tables[0];
                    //this.Session["Conexion"] = dt5;
                    DrConexion.DataSource = dt5; // EvaluacionSel.GargaQuery().Tables[0];
                    DrConexion.DataBind();

                    DrDuplicado.DataValueField = "ZID";
                    DrDuplicado.DataTextField = "ZDESCRIPCION";
                    dt5 = new DataTable();
                    dt5 = Main.CargaOpciones().Tables[0];
                    //this.Session["Conexion"] = dt5;
                    DrDuplicado.DataSource = dt5; // EvaluacionSel.GargaQuery().Tables[0];
                    DrDuplicado.DataBind();


                    Dtipo.DataValueField = "ZID";
                    Dtipo.DataTextField = "ZDESCRIPCION";

                    //if (this.Session["TipoArchivos"] != null)
                    //{
                    //    DataTable dt4 = this.Session["TipoArchivos"] as DataTable;
                    //    Dtipo.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                    //    Dtipo.DataBind();
                    //}
                    //else
                    //{
                        dt4 = new DataTable();
                        dt4 = Main.CargaJerarquia().Tables[0];
                        this.Session["TipoArchivos"] = dt4;
                        Dtipo.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        Dtipo.DataBind();
                    //}
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
                string a = Main.ETrazas("", "1", " AltaArchivos.Page_load --> Envia a Server.Transfer(Login.aspx). El Error:" + ex.Message);
                Server.Transfer("Login.aspx");
            }
        }

        private void Actualiza_Archivos()
        {

            DrArchivos.Items.Clear();
            Djerarquia.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            Djerarquia.DataValueField = "ZID";
            Djerarquia.DataTextField = "ZDESCRIPCION";

            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "0"));
            Djerarquia.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            DrArchivos.DataSource = dt;
            Djerarquia.DataSource = dt;
            DrArchivos.DataBind();
            Djerarquia.DataBind();
            this.Session["Archivos"] = dt;
        }

        protected void btnFiltraTabla_Click(object sender, EventArgs e)
        {
            DrArchivos.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";


            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();

            string Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
            Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
            Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
            Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZESTADO <> 3 ";
            if(Txtfiltrar.Text != "")
            {
                Miquery += " AND ZDESCRIPCION LIKE '%" + Txtfiltrar.Text + "%' ORDER BY ZDESCRIPCION ";

            }
            else
            {
                Miquery += " ORDER BY ZDESCRIPCION ";
            }


            dt = Main.BuscaLote(Miquery).Tables[0];
            DrArchivos.DataSource = dt;
            DrArchivos.DataBind();
        }
        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImgTablaCampo.Visible = false;
            btnTablaCampo_Click(sender, e);

            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            DrArchivos.BackColor = Color.FromName("#bdecb6");
            DrCampoasig.BackColor = Color.FromName("#ffffff");
            chkKey.Checked = false;
            DataTable dt = this.Session["Archivos"] as DataTable;
            foreach (DataRow fila in dt.Rows)
            {

                if (fila["ZID"].ToString() == DrArchivos.SelectedItem.Value)
                {
                    LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + DrArchivos.SelectedItem.Value;
                    TxtNombre.Text = fila["ZID"].ToString(); // fila["ZTABLENAME"].ToString();
                    TablaName.Text = fila["ZTABLENAME"].ToString();
                    //dlNivel.SelectedIndex = Convert.ToInt32(fila["ZNIVEL"].ToString());
                    //string VV = fila["ZROOT"].ToString();

                    for (int u = 0; u <= dlNivel.Items.Count - 1; u++)
                    {
                        if (dlNivel.Items[u].Value == fila["ZNIVEL"].ToString())
                        {
                            dlNivel.SelectedIndex = u;
                            break;
                        }
                    }
                    for (int u = 0; u <= Djerarquia.Items.Count - 1; u++)
                    {
                        if (Djerarquia.Items[u].Value == fila["ZROOT"].ToString())
                        {
                            Djerarquia.SelectedIndex = u;
                            break;
                        }
                    }
                    for (int u = 0; u <= Dtipo.Items.Count - 1; u++)
                    {
                        if (Dtipo.Items[u].Value == fila["ZTIPO"].ToString())
                        {
                            Dtipo.SelectedIndex = u;
                            break;
                        }
                    }
                    for (int u = 0; u <= dlEstado.Items.Count - 1; u++)
                    {
                        if (dlEstado.Items[u].Value == fila["ZESTADO"].ToString())
                        {
                            dlEstado.SelectedIndex = u;
                            break;
                        }
                    }

                    //Djerarquia.SelectedIndex = Convert.ToInt32(fila["ZROOT"].ToString());
                    //Dtipo.SelectedIndex = Convert.ToInt32(fila["ZTIPO"].ToString());
                    //dlEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());

                    

                    this.Session["IDArchivo"] = Convert.ToInt32(fila["ZID"].ToString());
                    Relaciones(Convert.ToInt32(fila["ZID"].ToString()), CampoOrden);
                    TxtDescripcion.Text = fila["ZDESCRIPCION"].ToString();

                    for (int u = 0; u <= DrDuplicado.Items.Count - 1; u++)
                    {
                        if (DrDuplicado.Items[u].Value == fila["ZDUPLICADOS"].ToString())
                        {
                            DrDuplicado.SelectedIndex = u;
                            break;
                        }
                    }
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
                    TablaObj.Text = fila["ZTABLEOBJ"].ToString();
                    //tvControl.Nodes.Clear();
                    registro = Convert.ToInt32(fila["ZID"].ToString());
                    //Busca_Root(Convert.ToInt32(Convert.ToInt32(fila["ZID"].ToString())));
                    Contador = 0;

                    CommentSQL.Text = fila["ZVIEW"].ToString();

                    if (fila["ZTIPO"].ToString() == "3")
                    {
                        BtTipo.Visible = true;
                    }
                    else
                    {
                        BtTipo.Visible = false;
                    }
                    //PopulateRootLevel(Buscaregistro);
                    DataTable dtVolumen = Main.CargaVolumen(fila["ZID_VOLUMEN"].ToString(), DrArchivos.SelectedItem.Value).Tables[0];
                    string Volumen = "";

                    Boolean Esta = false;
                    foreach (DataRow filaVol in dtVolumen.Rows)
                    {
                        Esta = true;
                        Volumen = filaVol["ZRUTA"].ToString();
                        break;
                    }
                    if (Esta == false)
                    {
                        TextRegistro.Text = "0";
                        TextDocElec.Text = "0";
                        TextRuta.Text = Server.MapPath("~/volumen/"); // @"C:\inetpub\wwwroot\Desarrollo\\";
                        Textunidad.Text = "0";
                        TextHardDisc.Text = "0 MB"; 
                    }
                    else
                    {
                        string a = Main.PropiedadesArchivo(DrArchivos.SelectedItem.Value, Volumen);
                        string[] Partes = System.Text.RegularExpressions.Regex.Split(a, "#");
                        for (int i = 0; i < Partes.Count(); i++)
                        {
                            if (i == 0) { TextRegistro.Text = Partes[i]; }
                            if (i == 1) { TextDocElec.Text = Partes[i]; }
                            if (i == 2) { TextRuta.Text = Partes[i]; }
                            if (i == 3) { Textunidad.Text = Partes[i]; }
                            if (i == 4) { TextHardDisc.Text = Partes[i] + " MB"; }
                        }
                    }
                    //TxtMail.Text = fila["ZEMAIL"].ToString();

                    Verifica_Tabla(sender, e);
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

        }


        protected void btnGuardacolumna(object sender, EventArgs e)
        {
            int MiID = 0;
            //string Ver = "";
            //Nada
            Boolean Esta = false;

            SqlParameter[] dbParams = new SqlParameter[0];
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
                    Column = "UPDATE " + Tabla + " SET  ZDESCRIPCION ='" + TxtDescripcion.Text + "', ZNIVEL = " + dlNivel.SelectedValue + ", ZROOT ='" + Djerarquia.SelectedItem.Value + "',";
                    Column += "ZTABLENAME ='" + TablaName.Text + "', ZTABLEOBJ = '" + TablaObj.Text + "', ZTIPO = " + Dtipo.SelectedValue + ", ZESTADO =" + dlEstado.SelectedValue + ", ";
                    Column += "ZCONEXION ='" + DrConexion.SelectedValue + "', ";
                    Column += "ZDUPLICADOS ='" + DrDuplicado.SelectedValue + "', ";
                    Column += "ZKEY ='" + DrCampoasig.SelectedValue + "', ";
                    Column += "ZVIEW ='" + CommentSQL.Text + "' ";
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

                        Tabla = TablaName.Text;
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
                                        //Busco si está la columna creada
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
                    if (TablaObj.Text != "")
                    {
                        if (TablaObj.Text.Contains("OBJ"))
                        {
                            Tabla = TablaObj.Text;
                            //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                            dtO = Main.ExisteTabla(Tabla).Tables[0];

                            Esta = true;
                        }
                        else
                        {
                            Tabla = TablaObj.Text + "OBJ";
                            //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                            dtO = Main.ExisteTabla(Tabla).Tables[0];
                            Esta = true;
                        }
                    }
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
                    DivSQL.Visible = false;
                    DivCampoDer.Visible = true;
                    this.Session["Edicion"] = 0;
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                }
                //messageBox.ShowMessage("El Registro se modificó correctamente!");
                this.Session["Edicion"] = 0;
            }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                }

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
            if (TxtNombre.Text == "" || TablaName.Text == "")
            {
                Lbmensaje.Text = " No se permiten campos nulos en los campos clave Descripción y Nombre de Tabla.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                return;
            }

            //Si es nuevo
            SqlParameter[] dbParams = new SqlParameter[0];
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 1)
            {
                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = "ZARCHIVOS"; // this.Session["Tabla"].ToString();

                int Existe = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT COUNT(ZID) FROM ZARCHIVOS WHERE ZIDTABLA = '" + TxtNombre.Text + "' AND ZDESCRIPCION = '" + TxtDescripcion.Text + "' AND ZTABLENAME = '" + TablaName.Text + "'", dbParams));

                if(Existe == 0)
                {
                    int M = 0;
                    if (DrCampoasig.SelectedValue == "") { M = 0; } else { M = Convert.ToInt32(DrCampoasig.SelectedValue); }
                    Column = "INSERT INTO " + Tabla + " (ZIDTABLA, ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZKEY, ZDUPLICADOS) ";
                    ColumnVal = " VALUES('" + TxtNombre.Text + "','" + TxtDescripcion.Text + "'," + dlNivel.SelectedValue + "," + Djerarquia.SelectedItem.Value + ",'" + TablaName.Text + "','" + TablaObj.Text + "'," + Dtipo.SelectedValue + "," + dlEstado.SelectedValue + "," + DrConexion.SelectedValue + "," + M + "," + DrDuplicado.SelectedValue + ")";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }


                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT ZID FROM ZARCHIVOS WHERE ZIDTABLA = '" + TxtNombre.Text + "' AND ZDESCRIPCION = '" + TxtDescripcion.Text + "' AND ZTABLENAME = '" + TablaName.Text + "'", dbParams));

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
                   Tabla = TablaName.Text;
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
                                    //Busco si está la columna  creada
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
                            Esta = false;
                            for (int i = 0; i < ListBox2Col.Items.Count; i++)
                            {
                                if (ListBox2Col.Items[i].Text == "ZID")
                                {
                                    Esta = true;
                                    SQL = " CREATE TABLE " + Tabla + " ( ZID_DUPLICADO int IDENTITY(1, 1) NOT NULL ";
                                    break;
                                }
                            }
                            if(Esta == false)
                            {
                                SQL = " CREATE TABLE " + Tabla + " ( ZID int IDENTITY(1, 1) NOT NULL ";
                            }

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
                if (TablaObj.Text != "")
                {
                    if (TablaObj.Text.Contains("OBJ"))
                    {
                        dtO = Main.PropiedadesTablaObjeto(TablaObj.Text).Tables[0];
                        Tabla = TablaObj.Text;
                    }
                    else
                    {
                        Tabla = Tabla + "OBJ";
                        dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                    }
                }
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

                Existe = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT COUNT(ZID) FROM ZVOLUMENES WHERE ZID_ARCHIVO = " + MiID , dbParams));

                if (Existe == 0)
                {
                    string ruta = Convert.ToString(DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZRUTA) FROM ZVOLUMENES ", dbParams));
                    //Cambiar por contador de caracteres
                    string mascara = "ZVRE00000000" + MiID;

                    Column = "INSERT INTO ZVOLUMENES (ZID_DOMINIO, ZID_ARCHIVO, ZNOMBRE, ZRUTA, ZFECHA_CREATE,ZFECHA_MOD, ZSIZE, ZACTIVO) ";
                    ColumnVal = " VALUES('0','" + MiID  + "','" + mascara + "','" + ruta + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0,1)";
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
               
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
                    Column = "UPDATE " + Tabla + " SET  ZDESCRIPCION ='" + TxtDescripcion.Text + "', ZNIVEL = " + dlNivel.SelectedValue + ", ZROOT ='" + Djerarquia.SelectedValue + "',";
                    Column += "ZTABLENAME ='" + TablaName.Text + "', ZTABLEOBJ = '" + TablaObj.Text + "', ZTIPO = " + Dtipo.SelectedValue + ", ZESTADO =" + dlEstado.SelectedValue + ", ";
                    Column += "ZCONEXION ='" + DrConexion.SelectedValue + "', ";
                    Column += "ZDUPLICADOS ='" + DrDuplicado.SelectedValue + "', ";
                    Column += "ZKEY ='" + DrCampoasig.SelectedValue + "', ";
                    Column += "ZVIEW ='" + CommentSQL.Text + "' ";
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

                        Tabla = TablaName.Text;
                        DataTable dtTAbla = Main.PropiedadesTabla(Tabla).Tables[0];
                        string SQL = "";
                        Boolean ExisteDesc = false;//Existe campo critico para tipo de Archivo
                        Boolean ExisteID = false;//Existe campo critico para tipo de Archivo

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
                                        //Busco si está la columna creada
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
                                                if(ExisteDesc == false && ListBox2ID.Items[i].Text == "ZDESCRIPCION")
                                                {
                                                    ExisteDesc = true;
                                                }
                                                if (ExisteID == false && ListBox2ID.Items[i].Text == "ZID")
                                                {
                                                    ExisteID = true;
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
                            //if (ExisteDesc == false)
                            //{
                            //    foreach (DataRow filacampo in dtCampos.Rows)
                            //    {
                            //        if(filacampo["ZTITULO"].ToString() == "ZDESCRIPCION")
                            //        {
                            //            if (filacampo["ZFORMATO"].ToString() == "Decimal" || filacampo["ZFORMATO"].ToString() == "Varchar" || filacampo["ZFORMATO"].ToString() == "Numerico")
                            //            {
                            //                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + "); ";
                            //            }
                            //            else
                            //            {
                            //                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " ;"; // + filacampo["ZVALOR"].ToString() + "; ";
                            //            }
                            //            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            //            SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", 0) ";
                            //            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            //            Contador += 1;

                            //            break;
                            //        }
                            //    }
                            //}
                            //if (ExisteID == false)
                            //{
                            //    foreach (DataRow filacampo in dtCampos.Rows)
                            //    {
                            //        if (filacampo["ZTITULO"].ToString() == "ZID")
                            //        {
                            //            if (filacampo["ZFORMATO"].ToString() == "Decimal" || filacampo["ZFORMATO"].ToString() == "Varchar" || filacampo["ZFORMATO"].ToString() == "Numerico")
                            //            {
                            //                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " (" + filacampo["ZVALOR"].ToString() + "); ";
                            //            }
                            //            else
                            //            {
                            //                SQL = " ALTER TABLE " + Tabla + " ADD " + filacampo["ZTITULO"].ToString() + " " + filacampo["ZFORMATO"].ToString() + " ;"; // + filacampo["ZVALOR"].ToString() + "; ";
                            //            }
                            //            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            //            SQL = " INSERT INTO ZARCHIVOCAMPOS ( ZIDARCHIVO, ZIDCAMPO, ZORDEN, ZKEY) VALUES (" + MiID + "," + filacampo["ZID"].ToString() + "," + Contador + ", 1) ";
                            //            DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            //            Contador += 1;

                            //            break;
                            //        }
                            //    }
                            //}
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
                                if(Dtipo.SelectedValue == "2" || Dtipo.SelectedValue == "5" || Dtipo.SelectedValue == "6")
                                {
                                    //Estos tipos no crean tabla
                                }
                                else
                                {
                                    DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                                }
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
                    if (TablaObj.Text != "")
                    {
                        if (TablaObj.Text.Contains("OBJ"))
                        {
                            Tabla = TablaObj.Text;
                            //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                            dtO = Main.ExisteTabla(Tabla).Tables[0];

                            Esta = true;
                        }
                        else
                        {
                            Tabla = TablaObj.Text + "OBJ";
                            //dtO = Main.PropiedadesTablaObjeto(Tabla).Tables[0];
                            dtO = Main.ExisteTabla(Tabla).Tables[0];
                            Esta = true;
                        }
                    }
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

                            if (Dtipo.SelectedValue == "2" || Dtipo.SelectedValue == "5" || Dtipo.SelectedValue == "6")
                            {
                                //Estos tipos no crean tabla
                            }
                            else
                            {
                                DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                            }
                        }  
                    }
                    DivSQL.Visible = false;
                    DivCampoDer.Visible = true;
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
            Djerarquia.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            Djerarquia.DataValueField = "ZID";
            Djerarquia.DataTextField = "ZDESCRIPCION";

            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "0"));
            Djerarquia.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            dt = Main.CargaArchivos(1).Tables[0];
            this.Session["Archivo"] = dt;
            DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
            DrArchivos.DataBind();

            Djerarquia.DataSource = dt;
            Djerarquia.DataBind();

            DesactivarTxt();
            Limpiar();

            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";
            btnEditar.Visible = true;
            btnNuevo.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            BtDeleteColum.Enabled = false;
            BtAsigna.Enabled = false;
            ImgTablaCampo.Visible = false;
            btnTablaCampo_Click(sender, e);
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
            TablaName.Text = "";
            TablaObj.Text = "";

            TextDocElec.Text = "";
            TextRuta.Text = "";
            DrDuplicado.SelectedIndex = 0;
            DrConexion.SelectedIndex = 0;
            dlNivel.SelectedIndex = 9;
            Djerarquia.SelectedIndex = -1;
            dlEstado.SelectedIndex = 1;
            Dtipo.SelectedIndex = 0;
            TextRuta.Text = Server.MapPath("~/volumen/"); 
            TextRegistro.Text = "0";
            Textunidad.Text = "0";
            TextHardDisc.Text = "0 MB";
            CommentSQL.Text = "";

            //TextDuplicado.Text = "";
            //dlNivel.Text = "";
            //TxtMail.Text = "";
            this.Session["Edicion"] = 0;
        }

        protected void Verifica_Tabla(object sender, EventArgs e)
        {
            string SQL = "SELECT ";
            SQL += "so.name AS Tabla, ";
            SQL += "sc.name AS Columna, ";
            SQL += "st.name AS Tipo, ";
            SQL += "sc.max_length AS Tamaño, ";
            SQL += "CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN 'varchar' ";
            SQL += "WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN 'numeric' ";
            SQL += "WHEN st.name = 'decimal' THEN 'decimal' ";
            SQL += "WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN 'Fecha'  END AS RELACION, ";
            SQL += "CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN '1' ";
            SQL += "WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN '2' ";
            SQL += "WHEN st.name = 'decimal' THEN '5' ";
            SQL += "WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN '4'  END AS VALORRELACION, ";
            SQL += "CASE WHEN CONVERT(VARCHAR(255), sc.max_length) = '-1' OR CONVERT(VARCHAR(255),sc.max_length) = '8000' THEN 'MAX' ELSE CONVERT(VARCHAR(255),sc.max_length) END VALORCOLUMNA ";
            SQL += "FROM ";
            SQL += "sys.objects so INNER JOIN ";
            SQL += "sys.columns sc ON ";
            SQL += "so.object_id = sc.object_id INNER JOIN ";
            SQL += "sys.types st ON ";
            SQL += "st.system_type_id = sc.system_type_id ";
            SQL += "AND st.name != 'sysname' ";
            SQL += "WHERE so.type = 'U' ";
            SQL += "AND so.name = '" + TablaName.Text + "' ";
            //SQL += "ORDER BY so.name,sc.name ";

            DataTable dtCampos = Main.BuscaLote(SQL).Tables[0];

            ListBox1T.Items.Clear();
            ListBox1IDT.Items.Clear();
            ListBox1ColT.Items.Clear();

            int I = 0;
            foreach (DataRow fila in dtCampos.Rows)
            {
                if (I == 0)
                {
                    LbCampo1T.Text = "Campo:" + fila["Columna"].ToString();
                    LbId1T.Text = "ID:" + fila["Columna"].ToString();
                    LbTipo1T.Text = "Tipo:" + fila["RELACION"].ToString();
                    LbSize1T.Text = "Tamaño:" + fila["VALORCOLUMNA"].ToString();
                }
                ListBox1T.Items.Add(new ListasID(fila["Columna"].ToString(), I).ToString());
                ListBox1IDT.Items.Add(new ListasID(fila["VALORCOLUMNA"].ToString(), I).ToString());
                ListBox1ColT.Items.Add(new ListasID(fila["RELACION"].ToString(), I).ToString());
                I += 1;
            }
            LbCampoCountT.Text = I.ToString();
            I = 0;
            Boolean Esta = false;
            for (int a = 0; a <= ListBox1T.Items.Count - 1; a++)
            {
                string columna = ListBox1T.Items[a].Text;
                Esta = false;

                for (int b = 0; b <= ListBox2Col.Items.Count - 1; b++)
                {
                    if (ListBox2Col.Items[b].Text == columna)
                    {
                        //ListBox2Col.SelectedIndex = b;
                        //ListBox1T.SelectedIndex = a;
                        Variables.MiColor = "#bdecb6";
                        ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                        ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                        ListBox1Col.BackColor = Color.FromName(Variables.MiColor);
                        Esta = true;
                        break;
                    }
                }
                if (Esta == false)
                {
                    I += 1;
                }
            }
            if (I > 0)
            {
                Variables.MiColor = "#f0ddaa";
                ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                ListBox1Col.BackColor = Color.FromName(Variables.MiColor);
            }
        }

        protected void btnTablaCampo_Click(object sender, EventArgs e)
        {
            if (ImgTablaCampo.Visible == true)
            {
                ImgTablaCampo.Visible = false;
                ImgTablaCampoA.Visible = true;

                Verifica_Tabla(sender, e);


                PanelCampos.Visible = false;
                PanelTabla.Visible = true;
            }
            else
            {
                ImgTablaCampo.Visible = true;
                ImgTablaCampoA.Visible = false;
                PanelCampos.Visible = true;
                PanelTabla.Visible = false;
                Variables.MiColor = "#fffff";
                ListBox1T.BackColor = ListBox2.BackColor;
            }
        }
        protected void ListBox1IDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(this.Session["IndiceCampo"].ToString()); //ListBox1IDT.SelectedIndex;
            ListBox1T.SelectedIndex = ListBox1IDT.SelectedIndex;
            ListBox1ColT.SelectedIndex = ListBox1IDT.SelectedIndex;

            string a = ListBox1IDT.SelectedValue;

            LbCampo1T.Text = "Campo:" + ListBox1T.SelectedItem;
            LbId1T.Text = "ID:" + ListBox1T.SelectedItem;
            LbTipo1T.Text = "Tipo:" + ListBox1ColT.SelectedItem;
            LbSize1T.Text = "Tamaño:" + ListBox1IDT.SelectedItem;

            ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
            ListBox1T.BackColor = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = Color.FromName(Variables.MiColor);


        }
        protected void btnDeletecolum_Click(object sender, EventArgs e)
        {
            //ALTER TABLE dbo.doc_exb DROP COLUMN column_b;

            int Index = Convert.ToInt32(this.Session["IndiceCampo"].ToString());
            //this.Session["IndiceCampo"] = Index;
            string Columna = ListBox1T.Items[Index].Text;

#pragma warning disable CS0219 // La variable 'I' está asignada pero su valor nunca se usa
            int I = 0;
#pragma warning restore CS0219 // La variable 'I' está asignada pero su valor nunca se usa
            Boolean Esta = false;

            for (int b = 0; b <= ListBox2Col.Items.Count - 1; b++)
            {
                if (ListBox2Col.Items[b].Text == Columna)
                {
                    lbCuestion.Text = "La Columna " + Columna + " está vinculada como Campo al Archivo Documental " + DrArchivos.SelectedItem.Text + ". Debe desasignar la relación del Campo antes de eliminar la Columna de la Tabla.";
                    DivCuestion.Visible = true;
                    ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                    ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                    ListBox1Col.BackColor = Color.FromName(Variables.MiColor);
                    Esta = true;
                    break;
                }
            }
            if(Esta == false)
            {
                string SQL = "ALTER TABLE " + TablaName.Text + " DROP COLUMN " + Columna + " ";
                SqlParameter[] dbParams = new SqlParameter[0];
                DBHelper.ExecuteNonQuerySQL(SQL, dbParams);
                lbCuestion.Text = "La Columna " + Columna + " se eliminó correctemente sobre la Tabla " + TablaName.Text + ".";
                DivCuestion.Visible = true;
                ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                ListBox1Col.BackColor = Color.FromName(Variables.MiColor);

            }

        }
        protected void Aceptar_Click(object sender, EventArgs e)
        {
            DivCuestion.Visible = false;
        }
        protected void btnAsignacolum_Click(object sender, EventArgs e)
        {
            int I = 0;
            Boolean Esta = false;

            string columna = ListBox1T.Items[Convert.ToInt32(this.Session["IndiceCampo"].ToString())].Text;
            Esta = false;

            for (int b = 0; b <= ListBox2Col.Items.Count - 1; b++)
            {
                if (ListBox2Col.Items[b].Text == columna)
                {
                    //Ya esiste

                    Esta = true;
                    //ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                    //ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                    lbCuestion.Text = "La Columna " + columna + " ya está vinculada como Campo al Archivo Documental " + DrArchivos.SelectedItem.Text + ".";
                    //cuestion.Visible = false;
                    //Asume.Visible = true;
                    //DvPreparado.Visible = true;
                    DivCuestion.Visible = true;
                    MiCloseMenu();
                    break;
                }
            }
            if (Esta == false)
            {
                for (int b = 0; b <= ListBox1Col.Items.Count - 1; b++)
                {
                    if (ListBox1Col.Items[b].Text == columna)
                    {
                        //Esiste el campo creado
                        ListBox1Col.Items[b].Selected = true;
                        ListBox1.Items[b].Selected = true;
                        ListBox1ID.Items[b].Selected = true;
                        Esta = true;

                        Lbmensaje.Text = "Se vinculará la Columna " + columna + " al Archivo Documental " + DrArchivos.SelectedItem.Text + ".¿Desea Continuar?";
                        windowmessaje.Visible = true;
                        cuestion.Visible = true;
                        Asume.Visible = false;
                        Page.MaintainScrollPositionOnPostBack = false;
                        MiCloseMenu();
                        break;
                    }
                }
                if (Esta == false)
                {
                    I += 1;
                    //No esiste
                    string M = ListBox1IDT.Items[Convert.ToInt32(this.Session["IndiceCampo"].ToString())].Text;
                    string SQL = "";
                    if (M.Contains("MAX") == true)
                    {
                        SQL = "SELECT ZID FROM ZTIPOCAMPO WHERE ZFORMATO = 'VARCHAR(MAX)'";
                    }
                    else
                    {
                        SQL = "SELECT ZID FROM ZTIPOCAMPO WHERE ZFORMATO = '" + ListBox1ColT.SelectedItem.Text.ToUpper() + "'";
                    }


                    string ZID = "0";
                    Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                    if (Con is null)
                    {
                    }
                    else
                    {
                        ZID = Con.ToString();
                    }


                    String Column = "INSERT INTO ZCAMPOS ( ZTITULO, ZDESCRIPCION, ZTIPO, ZVALOR, ZVALIDACION, ZNIVEL, ZESTADO, ZACTIVO, ZFECHA) VALUES (";

                    string ColumnVal = "'" + columna + "','" + columna + "'," + ZID + ",'" + ListBox1IDT.Items[Convert.ToInt32(this.Session["IndiceCampo"].ToString())].Text + "',";
                    ColumnVal += " 0,";
                    //Cambio formato Fecha a numerico yyyyMMddHHmmss
                    ColumnVal +=  "5,1,1,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";

                    Column += ColumnVal;
                    SqlParameter[] dbParams = new SqlParameter[0];
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams));

                    ListBox2Col.Items.Add(columna) ;
                    ListBox2.Items.Add(columna);
                    ListBox2ID.Items.Add(MiID.ToString());


                    btnGuardacolumna(sender, e);
                    //btnGuardar_Click(sender, e);
                    //btnEditar_Click(sender, e);

                    lbCuestion.Text = "Se ha creado la Columna " + columna + " en la Tabla Campos para vincularlo como Campo al Archivo Documental " + DrArchivos.SelectedItem.Text + ".";
                    //cuestion.Visible = false;
                    //Asume.Visible = true;
                    //DvPreparado.Visible = true;
                    DivCuestion.Visible = true;
                    MiCloseMenu();

                    ImgTablaCampo.Visible = true;
                    btnTablaCampo_Click(sender, e);


                    ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
                    ListBox1T.BackColor = Color.FromName(Variables.MiColor);
                    ListBox1Col.BackColor = Color.FromName(Variables.MiColor);
                }
            }
        }

        private void CargacolumnaEnArchivo()
        {
            btnPasarSeleccionados_Click(null, null);
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

        protected void ListBox1ColT_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = Convert.ToInt32(this.Session["IndiceCampo"].ToString());
            //int Index = ListBox1ColT.SelectedIndex;
            ListBox1T.SelectedIndex = ListBox1ColT.SelectedIndex;
            ListBox1IDT.SelectedIndex = ListBox1ColT.SelectedIndex;

            string a = ListBox1ColT.SelectedValue;

            LbCampo1T.Text = "Campo:" + ListBox1T.SelectedItem;
            LbId1T.Text = "ID:" + ListBox1T.SelectedItem;
            LbTipo1T.Text = "Tipo:" + ListBox1ColT.SelectedItem;
            LbSize1T.Text = "Tamaño:" + ListBox1IDT.SelectedItem;

            ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
            ListBox1T.BackColor = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = Color.FromName(Variables.MiColor);

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

        protected void ListBox1T_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["IndiceCampo"] = ListBox1T.SelectedIndex;
            int Index = ListBox1T.SelectedIndex;
            ListBox1IDT.SelectedIndex = ListBox1T.SelectedIndex;
            ListBox1ColT.SelectedIndex = ListBox1T.SelectedIndex;

            string b = ListBox1T.SelectedValue;
            for (int a = 0; a <= ListBox2Col.Items.Count - 1; a++)
            {
                ListBox2Col.Items[a].Selected = false;
            }

            LbCampo1T.Text = "Campo:" + ListBox1T.SelectedItem;
            LbId1T.Text = "ID:" + ListBox1T.SelectedItem;
            LbTipo1T.Text = "Tipo:" + ListBox1ColT.SelectedItem;
            LbSize1T.Text = "Tamaño:" + ListBox1IDT.SelectedItem;

            ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
            ListBox1T.BackColor  = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = Color.FromName(Variables.MiColor);




            for (int a = 0; a <= ListBox2Col.Items.Count - 1; a++)
            {
                if (ListBox2Col.Items[a].Text == b)
                {
                    ListBox2Col.SelectedIndex = a;
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
            var color = Color.FromName(Variables.MiColor);

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
            ListBox2Col.BackColor = Color.FromName(Variables.MiColor);
            ListBox1T.BackColor = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = Color.FromName(Variables.MiColor);
        }



        protected void ListBox2Col_SelectedIndexChanged(object sender, EventArgs e)
        {
            var color = Color.FromName(Variables.MiColor);

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
            ListBox2Col.BackColor = color;
            ListBox1T.BackColor = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = color;

        }

        protected void ListBox2ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var color = Color.FromName(Variables.MiColor);

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
            ListBox2Col.BackColor = color;
            ListBox1T.BackColor = Color.FromName(Variables.MiColor);
            ListBox1Col.BackColor = color;
        }

        public void Relaciones(int ID, string sortExpression = null)
        {
            int MiID = ID;
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();
            ListBox1ID.Items.Clear();
            ListBox2ID.Items.Clear();
            ListBox1Col.Items.Clear();
            ListBox2Col.Items.Clear();
            ListKeys.Items.Clear();

            DrCampoasig.Items.Clear();
            DrCampoasig.DataValueField = "ZID";
            DrCampoasig.DataTextField = "ZDESCRIPCION";
            SMKey.InnerText = "";

            //DrCompetencia.Items.Insert(0, ""); //Primero vacio

            DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dtTemporal = new DataTable();
            DataTable dt1 = new DataTable();

            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];
            this.Session["ArchivoCampos"] = dt1;

            DataTable dtFormato = Main.CargaFormatoCampos().Tables[0];
            this.Session["FormatoCampos"] = dtFormato;
            int visto = 0;

            DataTable dtt = new DataTable("Tabla");
            DataTable dto = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZDESCRIPCION");

            dto.Columns.Add("ZID");
            dto.Columns.Add("ZDESCRIPCION");

            DataRow drr;

            //const string fic = @"C:\Proyecto\Administracion\Admin\Public\output.txt";
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
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

                    //ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    //ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
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
                                    //ListBox2.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
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

            i = 0;
            foreach (DataRow fila in dt.Rows)//Campos
            {
                visto = 0;
                foreach (DataRow filaVisto in dto.Rows)//Campos
                {
                    if (fila["ZID"].ToString() == filaVisto["ZID"].ToString())
                    {
                        visto = 1;
                        break;
                    }
                    else
                    {
                        visto = 0;
                    }
                }

                if (visto == 0)
                {
                    i += 1;
                    ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    ListBox1Col.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
            LbCampoCount.Text = i.ToString();

            //muestrta todos para seleccionar, ahora primero tabla temporal porque el control list y dropdownlist web no tiene Sorted
            //ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            ////ListBox1.Items.Add(new ListasID("(" + miFormat["ZDESCRIPCION"].ToString() + ") - " + fila["ZDESCRIPCION"].ToString() , Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //        break;
            //    }
            //}
            //}



            if (x != "") { SMKey.InnerText = ". Keys de este Archivo: (" + x + ")"; }
                //LbIDArchivo.InnerHtml += Environment.NewLine + "Keys: (" + x + ")";
            
            ////Carga Ordenado
            //if (sortExpression != null)
            //{
            //    DataView dv = dtt.AsDataView();
            //    this.SortDirection = "ASC";
            //    //Pasar Dataview a DataTable
            //    dv.Sort = sortExpression + " " + this.SortDirection;
            //    ListBox1.DataSource  = dv.ToTable();
            //    ListBox1ID.DataSource = dv.ToTable();
            //}
            //else
            //{
            //foreach (DataRow fila in dtt.Rows)//Campos no utilizados
            //{
            //    ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //    ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //    ListBox1Col.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            //}
            
            
            //ListBox1.DataSource = dtt;
            //ListBox1ID.DataSource = dtt;

            //ListBox1.DataBind();
            //ListBox1ID.DataBind();

            //sw.Close();
            if (i != 0)
            {
                foreach (DataRow fila in dto.Rows)//Campos
                {
                    if(fila["ZDESCRIPCION"].ToString() == "")
                    {
                        LbUtilizados.Text = "0";
                    }
                    else
                    {
                        LbUtilizados.Text = dto.Rows.Count.ToString();
                        DrCampoasig.DataSource = dto;
                        DrCampoasig.DataBind();
                        this.Session["SelArchivoCampo"] = dto;
                        if (Key == "0" || Key == null)
                        { }
                        else
                        {
                            for (int a = 0; a <= DrCampoasig.Items.Count - 1; a++)
                            {
                                if (DrCampoasig.Items[a].Value == Key)
                                {
                                    DrCampoasig.SelectedIndex = a;
                                    DrCampoasig.BackColor = Color.FromName("#bdecb6");
                                    break;
                                    //DrCampoasig.Text = DrCampoasig.Items[a].Text;
                                }
                            }
                            //DrCampoasig.SelectedIndex = Convert.ToInt32(Key);
                        }
                    }
                    break;
                }
            }
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
            LbIDArchivo.InnerHtml = "Relaciones con Id Archivo " + MiID;
            DrCampoasig.Items.Clear();
            TxtDescripcion.Text = "";
            TxtNombre.Text = MiID.ToString();
            TablaName.Text = "";
            //TextDuplicado.Text = "";
            TablaObj.Text = "";
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
            btnEditar.Visible = false;
            btnNuevo.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
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
            //btnEditar.CssClass = "myButtonOver";
            //btnNuevo.CssClass = "myButtonOver";
            //btnGuardar.CssClass = "myButtonOff";
            //btnCancelar.CssClass = "myButtonOff";
            btnEditar.Visible = false;
            btnNuevo.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            DrArchivos.Enabled = false;
            BtDeleteColum.Enabled = true;
            BtAsigna.Enabled = true;

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
            //for (int i = 0; i < ListBox2.Items.Count - 1; i++)
            //{
            //    ListBox1.Items.Add(ListBox2.SelectedItem);
            //    ListBox1ID.Items.Add(ListBox2ID.SelectedItem);
            //}
            //ListBox2.Items.Clear();
            //ListBox2ID.Items.Clear();
            while (ListBox1.GetSelectedIndices().Length > 0)
            {
                ListBox2.Items.Add(ListBox1.SelectedItem);
                ListBox2ID.Items.Add(ListBox1ID.SelectedItem);
                ListBox2Col.Items.Add(ListBox1Col.SelectedItem);
                ListKeys.Items.Add("0");
                ListBox1ID.Items.RemoveAt(ListBox1.SelectedIndex);
                ListBox1Col.Items.RemoveAt(ListBox1.SelectedIndex);
                ListBox1.Items.Remove(ListBox1.SelectedItem);
            }
        }

        protected void btnRegresarSeleccionados_Click(object sender, EventArgs e)
        {
            //Regresamos los items seleccionados de listbox2 a listbox1

            while (ListBox2.GetSelectedIndices().Length > 0)
            {
                ListBox1.Items.Add(ListBox2.SelectedItem);
                ListBox1ID.Items.Add(ListBox2ID.SelectedItem);
                ListBox1Col.Items.Add(ListBox2Col.SelectedItem);
                ListBox2ID.Items.RemoveAt(ListBox2.SelectedIndex);
                ListKeys.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2Col.Items.RemoveAt(ListBox2.SelectedIndex);
                ListBox2.Items.Remove(ListBox2.SelectedItem);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en los listbox
            Boolean Esta = false;
            for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            {
                ListBox1.Items[i].Selected = false;
                ListBox1ID.Items[i].Selected = false;
                ListBox1Col.Items[i].Selected = false;
            }



            for (int i = 0; i < ListBox1.Items.Count - 1; i++)
            {
                if (ListBox1.Items[i].Text.Contains(TextBox1.Text))
                {
                    string a = ListBox1.Items[i].Text;
                    a = ListBox1.Items[i].Value;
                    ListBox1.Items[i].Selected = true;
                    ListBox1ID.Items[i].Selected = true;
                    ListBox1Col.Items[i].Selected = true;
                    Esta = true;
                    break;
                }
            }
            if (Esta == false)
            {
                for (int i = 0; i < ListBox1ID.Items.Count - 1; i++)
                {
                    if (ListBox1ID.Items[i].Text.Contains(TextBox1.Text))
                    {
                        string a = ListBox1ID.Items[i].Text;
                        a = ListBox1ID.Items[i].Value;
                        ListBox1.Items[i].Selected = true;
                        ListBox1ID.Items[i].Selected = true;
                        ListBox1Col.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            if (Esta == false)
            {
                for (int i = 0; i < ListBox1Col.Items.Count - 1; i++)
                {
                    if (ListBox1Col.Items[i].Text.Contains(TextBox1.Text))
                    {
                        string a = ListBox1Col.Items[i].Text;
                        a = ListBox1Col.Items[i].Value;
                        ListBox1.Items[i].Selected = true;
                        ListBox1ID.Items[i].Selected = true;
                        ListBox1Col.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            if (Esta == false)
            {
                for (int i = 0; i < ListBox2.Items.Count - 1; i++)
                {
                    if (ListBox2.Items[i].Text.Contains(TextBox1.Text))
                    {
                        string a = ListBox2.Items[i].Text;
                        a = ListBox2.Items[i].Value;
                        ListBox2.Items[i].Selected = true;
                        ListBox2ID.Items[i].Selected = true;
                        ListBox2Col.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            if (Esta == false)
            {
                for (int i = 0; i < ListBox2ID.Items.Count - 1; i++)
                {
                    if (ListBox2ID.Items[i].Text.Contains(TextBox1.Text))
                    {
                        string a = ListBox2ID.Items[i].Text;
                        a = ListBox2ID.Items[i].Value;
                        ListBox2.Items[i].Selected = true;
                        ListBox2ID.Items[i].Selected = true;
                        ListBox2Col.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            if (Esta == false)
            {
                for (int i = 0; i < ListBox2Col.Items.Count - 1; i++)
                {
                    if (ListBox2Col.Items[i].Text.Contains(TextBox1.Text))
                    {
                        string a = ListBox2Col.Items[i].Text;
                        a = ListBox2Col.Items[i].Value;
                        ListBox2.Items[i].Selected = true;
                        ListBox2ID.Items[i].Selected = true;
                        ListBox2Col.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }


            //    if (ListBox1.Items.FindByText(TextBox1.Text) != null)
            //{
            //    ListBox1.Items.FindByText(TextBox1.Text).Selected = true;
            //}
        }

        public void DesactivarTxt()
        {

            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DrDuplicado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            dlEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TablaName.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TablaObj.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Djerarquia.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Dtipo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TextDocElec.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TextRuta.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Textunidad.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TextHardDisc.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TextRegistro.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtNombre.Enabled = false;
            TxtDescripcion.Enabled = false;
            DrDuplicado.Enabled = false;
            dlNivel.Enabled = false;
            dlEstado.Enabled = false;
            //ListBox1.Enabled = false;
            //ListBox2.Enabled = false;
            TablaName.Enabled = false;
            TablaObj.Enabled = false;
            Djerarquia.Enabled = false;
            Dtipo.Enabled = false;
            TextDocElec.Enabled = false;
            TextRuta.Enabled = false;
            Textunidad.Enabled = false;
            TextHardDisc.Enabled = false;
            TextRegistro.Enabled = false;
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
            DrDuplicado.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            dlEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2Col.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2ID.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TablaName.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TablaObj.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Djerarquia.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Dtipo.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TextDocElec.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TextRuta.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Textunidad.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TextHardDisc.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TextRegistro.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtNombre.Enabled = true;
            TxtDescripcion.Enabled = true;
            DrDuplicado.Enabled = true;
            dlNivel.Enabled = true;
            dlEstado.Enabled = true;
            //ListBox1.Enabled = true;
            //ListBox2.Enabled = true;
            TablaName.Enabled = true;
            TablaObj.Enabled = true;
            Djerarquia.Enabled = true;
            Dtipo.Enabled = true;
            TextDocElec.Enabled = true;
            TextRuta.Enabled = true;
            Textunidad.Enabled = true;
            TextHardDisc.Enabled = true;
            TextRegistro.Enabled = true;
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
            TextBox1.Text = tvControl.SelectedNode.Text;

            DataTable dt = new DataTable();
            dt = Main.TVCargaCampos(Convert.ToInt32(tvControl.SelectedNode.Value), Convert.ToInt32((string)Session["MiNivel"])).Tables[0];


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

            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";
            btnEditar.Visible = true;
            btnNuevo.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            DivSQL.Visible = false;
            DivCampoDer.Visible = true;
            DrArchivos.Enabled = true;
            BtDeleteColum.Enabled = false;
            BtAsigna.Enabled = false;
            ImgTablaCampo.Visible = false;
            btnTablaCampo_Click(sender, e);

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
                DrCampoasig.BackColor = Color.FromName("#bdecb6");
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

                SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQL.Text, dbParams, SQL).ToString();
                Iconexion.Attributes["style"] = "margin-top:-10px; color:green;";//dark
                Lbmensaje.Text = "Ejecución de Consulta correcta. ";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }
            catch (Exception ex)
            {
                Iconexion.Attributes["style"] = "margin-top:-10px; color:red;";//dark
                Lbmensaje.Text = "Error. " + ex.Message;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
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

                SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQLRep.Text, dbParams, SQL).ToString();
                Iconexion.Attributes["style"] = "margin-top:-10px; color:green;";//dark
                Lbmensaje.Text = "Ejecución de Consulta correcta. ";
                SQL = DBHelper.ExecuteScalarSQLNoNe(CommentSQLDoc.Text, dbParams, SQL).ToString();
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }
            catch (Exception ex)
            {
                Iconexion.Attributes["style"] = "margin-top:-10px; color:red;";//dark
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
            if (x != "") { SMKey.InnerText = ". Keys de este Archivo: (" + x + ")"; }
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;

            int Index = Convert.ToInt32(this.Session["IndiceCampo"].ToString());
            if(Index > 0)
            {
                CargacolumnaEnArchivo();
            }
            else
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                DBHelper.ExecuteNonQuerySQL("UPDATE ZARCHIVOS SET ZESTADO = 3 WHERE ZID = " + this.Session["IDArchivo"].ToString(), dbParams);
                Actualiza_Archivos();
            }
            MiOpenMenu();
            //Password=L0sViv3r0s.Fr3sas;Persist Security Info=True;User ID=RioEresmaCon;Initial Catalog=NET_VIVA22;Data Source=192.168.1.80
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
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

        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
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
                Iconexion.Attributes["style"] = "margin-top:-10px;";
            }
            else
            {
                DivSQL.Visible = false;
                DivCampoDer.Visible = true;
            }
        }

        protected void MigraRep_Click(object sender, EventArgs e)
        {
            //Copia la vista de datos en local y genera la tabla de objetos

        }

        protected void BtTipoRep_Click(object sender, EventArgs e)
        {
            if (DivReplica.Visible == false)
            {
                DivReplica.Visible = true;
                CommentSQLRep.Width = Unit.Percentage(100);
                CommentSQLRep.Height = 170;
                CommentSQLDoc.Width = Unit.Percentage(100);
                CommentSQLDoc.Height = 175;
                //DivSQL.Attributes.Add("height", "500px");
                //DivSQL.Attributes.Add("Width", "300px");
                DivCampoDer.Visible = false;
                Iconexion.Attributes["style"] = "margin-top:-10px;";
            }
            else
            {
                DivReplica.Visible = false;
                DivCampoDer.Visible = true;
            }
        }

        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Dtipo.SelectedItem.Value == "3")
            {
                //Es vista
                BtTipo.Visible = true;
                //Dtipo.Width = Unit.Percentage(95);
            }
            else
            {
                //Normal
                BtTipo.Visible = false;
                Dtipo.Width = dlEstado.Width;
            }
        }

        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }
    }
}