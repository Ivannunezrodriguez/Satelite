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
using System.Text;
using System.Drawing.Imaging;

namespace Satelite
{
    public partial class OrdenCompra : System.Web.UI.Page
    {
        Reports.ReportListCamion dtsE = null;

        public Boolean Esta = false;
        public string Permisos = "";

        public string TmpLbIDLote = "";
        public string TmpQRCode = "";
        public string TmpLbCampoS = "";
        public string TmpLbPlantaS = "";
        public string TmpLbFechaS = "";
        public string TmpLbCajasS = "";
        public string TmpLbPlantasS = "";
        public string TmpLbVariedadS = "";
        public string TmpLbEstado = "";
        public string TmpTipoForm = "";
        public int Modifica = 0;
        //private int registros = 0;
        //private string[] ListadoArchivos;
        //private static int IDDiv = 0;
        //private static string IDTABLA = "-1";
        //private Boolean Cargando = false;

        //private string ElIDaBorrar = "";
        private string ElID = "";
        //private string ElOrden = "";
        private string ElOrdenControl = "";
        private string ElOrdenLista = "";

        static TextBox[] ArrayTextBoxs;
        static Label[] ArrayLabels;
        static DropDownList[] ArrayCombos;
#pragma warning disable CS0414 // El campo 'OrdenCompra.contadorControles' está asignado pero su valor nunca se usa
        static int contadorControles;
#pragma warning restore CS0414 // El campo 'OrdenCompra.contadorControles' está asignado pero su valor nunca se usa
        private int Indice = 0;

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true; 

            try
            {
                this.Session["Procesa"] = "0";

                if (Session["Session"] == null)
                {
                    this.Session["Error"] = "0";
                    Server.Transfer("Login.aspx"); //Default
                }

                //if (this.Session["MiNivel"].ToString() == "9")
                //{
                //    Nominas.Visible = true;
                //}

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
                    this.Session["Filtro"] = "0";
                    this.Session["Filtro80"] = "";
                    this.Session["NumeroPalet"] = "0";
                    this.Session["FiltroEmpresa"] = "";
                    this.Session["FiltroFechaEntrega"] = "";
                    this.Session["FiltroProducto"] = "";
                    this.Session["FiltroProveedor"] = "";
                    this.Session["FiltroNumPedido"] = "";
                    this.Session["Menu"] = "6";
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
                    this.Session["Etiqueta"] = "";
                    //accion de la linea (linea actual; nueva linea; Id cabecera, Id; NUMERO_LINEA)  
                    this.Session["ModificaLinea"] = "";
                    this.Session["SelectLinea"] = "";
                    this.Session["CancelaConsulta"] = "";

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

                    //De mmomento ZENTRADA hasta generar los Menus
                    if (this.Session["IndexPage"].ToString() == "0")
                    {
                        this.Session["IndexPage"] = "23";
                    }

                    DataTable dtCampos = Main.CargaCampos().Tables[0];
                    this.Session["Campos"] = dtCampos;

                    this.Session["idarchivo"] = this.Session["IndexPage"].ToString();

                    string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                    SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
                    DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

                    dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dtCampos);
                    CreaGridControl(dtArchivos, dtCampos);
                    Carga_tablaControl(dtArchivos, dtCampos);

                    if (txtQRCode.Text == "")
                    {
                        Nueva_Secuencia();
                    }

   

                    Carga_Filtros();
                    Campos_ordenados();
                    BtGralConsulta_Click(null, null);
                    Carga_tabla();
                    Carga_Impresoras("0");

                    this.Session["IDSecuencia"] = "0";
                    //this.Session["DESARROLLO"] = "0";
                    //this.Session["SelectQR"] = "0";
                    this.Session["QR"] = "";


                    this.Session["IDLote"] = "0";

                    //Carga_tablaLista();

                    //LinkButton lnkUp = (gvLista.Rows[0].FindControl("lnkUp") as LinkButton);
                    //LinkButton lnkDown = (gvLista.Rows[gvLista.Rows.Count - 1].FindControl("lnkDown") as LinkButton);
                    //lnkUp.Enabled = false;
                    //lnkUp.CssClass = "button disabled";
                    //lnkDown.Enabled = false;
                    //lnkDown.CssClass = "button disabled";

                    //Carga_tablaCabecera();
                    Carga_Menus();
                    Habilita_Boton(0);
                    //Imageprocesa2.Visible = true;
                    //accordion3.Visible = true;

                    //TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //TxtFechaPrepara.Text = "";

                }
                else
                {
                    try
                    {
                        this.Session["Error"] = "0";
                        if (this.Session["IDSecuencia"].ToString() == null)
                        {
                            Server.Transfer("thEnd.aspx");
                        }
                        this.Session["Secuencias"] = Main.CargaSecuencia().Tables[0];
                    }
                    catch (NullReferenceException ex)
                    {
                        string b = ex.Message;
                        //Lberror.Text += ex.Message;
                        if (Session["Session"] == null)
                        {
                            Server.Transfer("Login.aspx");
                        }
                        else if (this.Session["Error"].ToString() == "0")
                        {
                            Server.Transfer("Login.aspx");
                        }
                        else
                        {
                            Server.Transfer("thEnd.aspx");
                        }
                    }
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

            dvPrinters.Visible = true;
            dvDrlist.Visible = false;
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
            //if (BodyQR.Visible == true)
            //{
            //    MontaEtiqueta();
            //}
 
        }

        private void Filtra_Lotes(string ID)
        {
            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";

            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";
            //SQL += "AND ZID_SECUENCIA in (" + ID +  ")";
            //DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            //DrScaneados.Items.Clear();
            //DrScaneados.DataValueField = "ZID";
            //DrScaneados.DataTextField = "ZLOTE";
            //// insertamos el elemento en la primera posicion:
            //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            //DrScaneados.DataSource = dbA;
            //DrScaneados.DataBind();
            //DrScaneados.SelectedIndex = -1;

            //SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

            //SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
            //SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
            //SQL += " WHERE A.LOTE = B.ZLOTE ";
            //SQL += " AND B.ZID_SECUENCIA in (" + ID + ")";
            //SQL += " AND A.ESTADO <> '2' ";
            try
            {
                string SQL = "";
                if (this.Session["Cerrados"].ToString() == "1") //Los cerrados y procesados 
                {
                    if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                    {
                        if (TxtfindTXT.Text != "")
                        {
                            SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                            SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                            SQL += " WHERE A.LOTE = B.ZLOTE ";
                            SQL += " AND A.ESTADO IN (1,2) ";
                            SQL += " AND B.ZLOTE LIKE ('%" + TxtfindTXT.Text + "%') ";
                        }
                        else
                        {
                            SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                            SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                            SQL += " WHERE A.LOTE = B.ZLOTE ";
                            SQL += " AND A.ESTADO IN (1,2) ";
                        }
                    }
                    else
                    {
                        if (TxtfindTXT.Text != "")
                        {
                            SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                            SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                            SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                            SQL += " AND A.LOTE = B.ZLOTE ";
                            SQL += " AND A.ESTADO IN (1,2)";
                            SQL += " AND B.ZLOTE LIKE ('%" + TxtfindTXT.Text + "%') ";
                        }
                        else
                        {
                            SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                            SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                            SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                            SQL += " AND A.LOTE = B.ZLOTE ";
                            SQL += " AND A.ESTADO IN (1,2)";
                        }
                    }
                }
                else //Nuevos
                {
                    if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE A.LOTE = B.ZLOTE ";
                        SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0' OR A.ESTADO = '')";
                        //SQL += " AND A.ESTADO = '0' ";
                        //SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '')";

                    }
                    else
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                        SQL += " AND A.LOTE = B.ZLOTE ";
                        SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0' OR A.ESTADO = '')";
                        //SQL += " AND A.ESTADO = '0' ";
                        //SQL += " AND (A.ESTADO IS NULL OR A.ESTADO = '')";
                    }
                }




                DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                DrLotes.Items.Clear();
                DrLotes.DataValueField = "ID";
                DrLotes.DataTextField = "TODO";
                DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
                DrLotes.DataSource = dbB;
                DrLotes.DataBind();
                DrLotes.SelectedIndex = -1;

                lbBuscaCod.Text = "Códigos QR recibidos / finalizados : " + dbB.Rows.Count + "";

                if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                {
                    SQL = "SELECT A.LOTE, A.TIPO_FORM, COUNT(*) as total ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " GROUP BY A.LOTE, A.TIPO_FORM ";
                    SQL += " HAVING COUNT(*) > 1";
                }
                else
                {
                    SQL = "SELECT A.LOTE, A.TIPO_FORM, COUNT(*) as total ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " WHERE A.LOTE = B.ZLOTE ";
                    SQL += " AND B.ZID_SECUENCIA = '" + ID + "'";
                    SQL += " GROUP BY A.LOTE, A.TIPO_FORM ";
                    SQL += " HAVING COUNT(A.LOTE) > 1";
                }

                DataTable dbA = Main.BuscaLote(SQL).Tables[0];

                if (this.Session["Cerrados"].ToString() == "1")
                {
                    LbDuplicados.Text = "";
                    lbtitleLote.Text = "Estás visualizando Lotes finalizados. Sólo imprimir.";
                    BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyQR.Attributes["style"] = "background-color: #e9f5ef;";
                    PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                    //BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
                }
                else
                {
                    lbtitleLote.Text = "Seleccionar código lote. Existen Duplicados:";
                    BodyLote.Attributes.Add("style", "background-color: white;");
                    BodyCampos.Attributes.Add("style", "background-color: white;");
                    BodyLotes.Attributes.Add("style", "background-color: white;");
                    BodyQR.Attributes.Add("style", "background-color: white;");
                    PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                    //BodyAll.Attributes.Add("style", "background-color: white;");

                    if (dbA.Rows.Count == 0)
                    {
                        LbDuplicados.Text = "No";
                        LbDuplicados.ForeColor = Color.Black;
                    }
                    else
                    {
                        LbDuplicados.Text = "Si";
                        LbDuplicados.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }

        protected void FindLote_Click(object sender, EventArgs e)
        {
            Filtra_Lotes(this.Session["IDSecuencia"].ToString());
        }

        public DataTable Relaciones(int ID, DataTable DtCampos)
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

        private void Carga_tablaControl(DataTable dtArchivos, DataTable dtCampos, string sortExpression = null)
        {
            //cualquier Tabla 
            string SQL = "";
            string Tabla = "";
            string Vista = "";
            string Key = "";
            DataTable dt = null;
            string Dato = "";
            string MiCampo = "";

            try
            {
                if (this.Session["idarchivo"].ToString() == "0")
                {
                    return;
                }
                foreach (DataRow fila in dtArchivos.Rows)
                {
                    if (fila["ZVIEW"].ToString() == "" || fila["ZVIEW"].ToString() == "0" || fila["ZVIEW"].ToString() == null)
                    {
                        Vista = "";
                    }
                    else
                    {
                        Vista = fila["ZVIEW"].ToString();
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
                                break;
                            }
                        }
                    }
                    break;
                }

                if (Vista != "") //Vista a consultar
                {
                    dt = Main.BuscaLote(Vista).Tables[0];
                }
                else if (MiCampo != "") //Key identity distinta
                {
                    SQL = "";

                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();

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
                    dt = Main.BuscaLote(SQL).Tables[0];
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
                                break;
                            }
                        }
                    }

                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();

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

                    if (Dato == "") { Dato = "0"; }

                    SQL = " SELECT " + SQL;
                    SQL += "  FROM " + Tabla;

                    dt = Main.BuscaLote(SQL).Tables[0];
                }
                //ZTIPO_PLANTA,ZTIPO_FORMATO,ZNUMERO_PLANTAS,ZID_TIPO_FORMATO,ZID 

                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

                //Busca Error
                //Lberror.Text = "";
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
            }
        }


        private void CreaGridControl(DataTable dtArchivo, DataTable dtCampo)
        {
            //int i =  Convert.ToInt32(this.Session["NumeroPalet"].ToString());

            //Para dinamico Me.controls.item(contador).visible = false


            int Manual = 0;
            int cuantos = 0;
            int i = 0;
            int a = 0;
            Boolean Esta = false;
            string data = "";

            if (Manual == 0) //Manual. La variable en web.config
            {
                cuantos = dtCampo.Rows.Count;

                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N;
                    //HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);

                    ContentPlaceHolder cont = new ContentPlaceHolder();
                    cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                    HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivColumA.Visible = false;

                    MiContent = "DrFindR" + N; //linea campo
                    DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                    DivCombo.Items.Clear();
                    //DivCombo.Visible = false;

                    MiContent = "DrFindL" + N; //linea combo                     
                    DropDownList Divcombo2 = (DropDownList)cont.FindControl(MiContent);
                    if (N != 0) { Divcombo2.Items.Clear(); }

                    MiContent = "DivFindL" + N; //linea campo                       
                    HtmlGenericControl DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivCon.Visible = false;

                    MiContent = "DivFindR" + N; //linea campo                       
                    DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivCon.Visible = false;

                    MiContent = "lbL" + N; //linea campo                       
                    TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                    DivLabel.Text = "";
                    DivLabel.Visible = false;

                    MiContent = "TxL" + N; //linea campo                       
                    DivLabel = (TextBox)cont.FindControl(MiContent);
                    DivLabel.Text = "";
                    DivLabel.Visible = false;

                    MiContent = "lbD" + N; //linea campo                       
                    DivLabel = (TextBox)cont.FindControl(MiContent);
                    DivLabel.Text = "";
                    DivLabel.Visible = false;

                    MiContent = "TxD" + N; //linea campo                       
                    DivLabel = (TextBox)cont.FindControl(MiContent);
                    DivLabel.Text = "";
                    DivLabel.Visible = false;

                    MiContent = "DrL" + N; //linea campo
                    DivCombo = (DropDownList)cont.FindControl(MiContent);
                    DivCombo.Visible = false;
                    DivCombo.Items.Clear();

                    MiContent = "DrR" + N; //linea campo
                    DivCombo = (DropDownList)cont.FindControl(MiContent);
                    DivCombo.Visible = false;
                    DivCombo.Items.Clear();
                }

                string MiCampo = "";

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
                                MiCampo = filas["ZTITULO"].ToString();
                                Esta = true;
                                break;
                            }
                        }
                        if (Esta == false)
                        {
                            MiCampo = "ZID";
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
                    if ((i % 2) == 0)
                    {
                        // IZQUIERDA
                        string MiContent = "DivReg" + a; //linea campo
                        //HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        ContentPlaceHolder cont = new ContentPlaceHolder();
                        cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                        HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);
                        DivColumA.Visible = true;

                        if (a != 0)
                        {
                            MiContent = "DrFindL" + a; //linea campo                       
                            DropDownList DivcomboA = (DropDownList)cont.FindControl(MiContent);
                            DivcomboA.Items.AddRange(DrFindL0.Items.OfType<ListItem>().ToArray());
                        }


                        MiContent = "lbL" + a; //linea campo                       
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        MiContent = "LBCOLL" + a; //linea campo                       
                        DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZTITULO"].ToString();
                        DivLabel.Visible = false;

                        //MiContent = "DivColumB" + a; //linea campo
                        //HtmlGenericControl DivColumB = (HtmlGenericControl)cont.FindControl(MiContent);                       

                        string MM = filas["ZVALIDACION"].ToString();
                        if (filas["ZVALIDACION"].ToString() != "0")
                        {
                            //Busca su valor en la tabla de validación
                            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + filas["ZVALIDACION"].ToString();
                            DataTable dtValidacion = Main.BuscaLote(SQL).Tables[0];

                            //MiContent = "DivColumC" + a; //linea campo
                            //HtmlGenericControl DivColumC = (HtmlGenericControl)cont.FindControl(MiContent);
                            //DivColumC.Visible = true;
                            //DivColumB.Visible = false;

                            foreach (DataRow fila in dtValidacion.Rows)
                            {
                                if (fila["ZID"].ToString() == filas["ZVALIDACION"].ToString())
                                {
                                    SQL = "SELECT ZID, " + filas["ZTITULO"].ToString() + " FROM " + fila["ZTABLENAME"].ToString() + " ORDER BY ZID ";
                                    DataTable dtValor = Main.BuscaLote(SQL).Tables[0];

                                    MiContent = "DrL" + a; //linea campo
                                    DropDownList DivDL = (DropDownList)cont.FindControl(MiContent);
                                    DivDL.DataValueField = "ZID";
                                    DivDL.DataTextField = filas["ZTITULO"].ToString();
                                    DivDL.DataSource = dtValor;
                                    DivDL.DataBind();
                                    DivDL.Visible = true;
                                    //DivDL.SelectedIndex = Convert.ToInt32(filas["ZID"].ToString());
                                    break;
                                }
                            }
                        }
                        else
                        {
                            MiContent = "TxL" + a; //linea campo
                            //TextBox DivTexto = FindControl(MiContent) as TextBox;
                            TextBox DivTexto = (TextBox)cont.FindControl(MiContent);
                            //TextBox DivTexto = (DivColumB.FindControl(MiContent) as TextBox);
                            //TextBox DivTexto = new TextBox();
                            //DivTexto.ID = MiContent;
                            DivTexto.Text = "";//filas["ZTITULO"].ToString();
                            DivTexto.ReadOnly = true;
                            DivTexto.Visible = true;
                            DivTexto.BorderStyle = BorderStyle.None;
                            //DivColumB.Visible = true;
                        }
                    }
                    else
                    {
                        // DERECHA

                        //string MiContent = "DivColumD" + a; //linea campo
                        ////HtmlGenericControl DivColumD = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        ContentPlaceHolder cont = new ContentPlaceHolder();
                        cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                        //HtmlGenericControl DivColumD = (HtmlGenericControl)cont.FindControl(MiContent);
                        //DivColumD.Visible = true;

                        string MiContent = "DrFindR" + a; //linea campo                       
                        DropDownList DivcomboB = (DropDownList)cont.FindControl(MiContent);
                        DivcomboB.Items.AddRange(DrFindL0.Items.OfType<ListItem>().ToArray());

                        MiContent = "lbD" + a; //linea campo
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        MiContent = "LBCOLD" + a; //linea campo                       
                        DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZTITULO"].ToString();
                        DivLabel.Visible = false;

                        //MiContent = "DivColumE" + a; //linea campo
                        //HtmlGenericControl DivColumE = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);


                        if (filas["ZVALIDACION"].ToString() != "0")
                        {
                            //Busca su valor en la tabla de validación
                            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + filas["ZVALIDACION"].ToString();
                            DataTable dtValidacion = Main.BuscaLote(SQL).Tables[0];

                            //MiContent = "DivColumF" + a; //linea campo
                            //HtmlGenericControl DivColumF = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                            //DivColumF.Visible = true;
                            //DivColumE.Visible = false;

                            foreach (DataRow fila in dtValidacion.Rows)
                            {
                                if (fila["ZID"].ToString() == filas["ZVALIDACION"].ToString())
                                {
                                    SQL = "SELECT ZID, " + filas["ZTITULO"].ToString() + " FROM " + fila["ZTABLENAME"].ToString() + " ORDER BY ZID ";
                                    DataTable dtValor = Main.BuscaLote(SQL).Tables[0];

                                    MiContent = "DrR" + a; //linea campo
                                    DropDownList DivDL = (DropDownList)cont.FindControl(MiContent);
                                    DivDL.DataValueField = "ZID";
                                    DivDL.DataTextField = filas["ZTITULO"].ToString();
                                    DivDL.DataSource = dtValor;
                                    DivDL.DataBind();
                                    DivDL.Visible = true;
                                    //DivDL.SelectedIndex = Convert.ToInt32(filas["ZDESCRIPCION"].ToString());
                                    break;
                                }
                            }
                        }
                        else
                        {
                            MiContent = "TxD" + a; //linea campo
                            TextBox DivTexto = (TextBox)cont.FindControl(MiContent);
                            DivTexto.Text = "";//filas["ZTITULO"].ToString();
                            DivTexto.ReadOnly = true;
                            DivTexto.Visible = true;
                            //DivColumE.Visible = true;

                        }
                        a += 1;

                    }

                    if (filas["ZTITULO"].ToString() != "ZID")
                    {
                        BoundField Campo = new BoundField();
                        Campo.DataField = filas["ZTITULO"].ToString();
                        Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                        DataControlField DataControlField = Campo;
                    }

                    i += 1;
                }

                //gvControl.DataKeyNames = System.Text.RegularExpressions.Regex.Split(data, ";");

                //BodyCampos.Attributes["height"] = (i * 50).ToString();
            }
            else //Dinamicos "Falta tabla validacion"
            {
                //DivCampos0.Controls.Clear();

                HtmlGenericControl DivContent = new HtmlGenericControl();

                //gvControl.Columns.Add(new ButtonField() { CommandName = "BajaOrden", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });

                foreach (DataRow filas in dtCampo.Rows)
                {

                    if ((i % 2) == 0)
                    {
                        // IZQUIERDA
                        //<div class="row" id="DivReg0">
                        string MiContent = "DivReg" + a; //linea campo
                        DivContent.ID = MiContent; //linea campo


                        //<input type="image" src="Images/editar20x20.png" alt="Editar" onclick="javascript:__doPostBack('gvCabecera','Edit$0');return false;">
                        //HtmlGenericControl DivContent = (HtmlGenericControl)FindControlRecursive(DivCampos0, MiContent);
                        if (DivContent is null) { break; }
                        DivContent.Attributes["class"] = "row";

                        //<div class="col-lg-2">
                        HtmlGenericControl DivParam = new HtmlGenericControl();
                        DivParam.ID = "DivColumA" + i; //Div label campo
                        DivParam.Attributes["class"] = "col-lg-2";
                        DivParam.Visible = true;
                        //<label id="lbL0" visible="true" runat="server" style="width:100%" >ID:</label>

                        Label lbCampo = new Label();
                        lbCampo.ID = "lbL" + i; //label campo
                        lbCampo.Text = filas["ZDESCRIPCION"].ToString();
                        lbCampo.Visible = true;

                        DivParam.Controls.Add(lbCampo);

                        //<div class="col-lg-3" id="DivColumB0">
                        HtmlGenericControl DivParam1 = new HtmlGenericControl();
                        DivParam1.ID = "DivColumB" + i; //div texto campo
                        DivParam1.Attributes["class"] = "col-lg-3";
                        DivParam1.Visible = true;

                        //<asp:TextBox id="TxL0" runat="server" style="width:100%" ReadOnly="true" class="form-control" placeholder="Introduzca su identificador"></asp:TextBox>
                        TextBox TxCampo = new TextBox();
                        TxCampo.ID = "TxL" + i; //texto campo
                        TxCampo.Text = filas["ZTITULO"].ToString();
                        TxCampo.Attributes["style"] = "width:100%;";
                        TxCampo.Attributes["runat"] = "server";
                        //TxCampo.Attributes["onServerClick"] = "server";
                        TxCampo.ReadOnly = true;
                        TxCampo.Visible = true;

                        data += filas["ZDESCRIPCION"].ToString() + ";";

                        DivParam1.Controls.Add(TxCampo);

                        //<div class="col-lg-1" id="DivColumC0">
                        HtmlGenericControl DivParam2 = new HtmlGenericControl();
                        DivParam2.ID = "DivColumC" + i; //boton campo
                        DivParam2.Attributes["class"] = "col-lg-1";
                        DivParam2.Visible = true;

                        //<button id="BtL0" type="button" runat="server" style="width:30%" class="btn btn-danger btn-circle" visible="true" onServerClick="BtnActivate_click"><i id="I1" runat="server" class="fa fa-thumbs-o-down"></i></button>
                        Button BtCampo = new Button();
                        BtCampo.ID = "btL" + i; //texto campo
                        BtCampo.Text = "";// "<i id='I1' runat='server' class='fa fa-thumbs-o-down'></i>";
                        BtCampo.Attributes["class"] = "btn btn-danger btn-circle";
                        BtCampo.Visible = true;

                        DivParam2.Controls.Add(BtCampo);

                        DivContent.Controls.Add(DivParam);
                        DivContent.Controls.Add(DivParam1);
                        DivContent.Controls.Add(DivParam2);


                        //gvCabecera.Columns.Add(DataControlField);


                    }
                    else
                    {
                        // DERECHA
                        //<div class="col-lg-2">
                        HtmlGenericControl DivParamB = new HtmlGenericControl();
                        DivParamB.ID = "DivColumD" + i; //Div label campo
                        DivParamB.Attributes["class"] = "col-lg-2";
                        DivParamB.Visible = true;
                        //<label id="lbL0" visible="true" runat="server" style="width:100%" >ID:</label>

                        Label lbCampoB = new Label();
                        lbCampoB.ID = "lbD" + i; //label campo
                        lbCampoB.Text = filas["ZDESCRIPCION"].ToString();
                        lbCampoB.Visible = true;

                        data += filas["ZDESCRIPCION"].ToString() + ";";

                        DivParamB.Controls.Add(lbCampoB);

                        //<div class="col-lg-3" id="DivColumB0">
                        HtmlGenericControl DivParam1B = new HtmlGenericControl();
                        DivParam1B.ID = "DivColumE" + i; //div texto campo
                        DivParam1B.Attributes["class"] = "col-lg-3";
                        DivParam1B.Visible = true;

                        //<asp:TextBox id="TxL0" runat="server" style="width:100%" ReadOnly="true" class="form-control" placeholder="Introduzca su identificador"></asp:TextBox>
                        TextBox TxCampoB = new TextBox();
                        TxCampoB.ID = "TxD" + i; //texto campo
                        TxCampoB.Text = filas["ZTITULO"].ToString();
                        TxCampoB.Attributes["style"] = "width:100%;";
                        TxCampoB.Attributes["runat"] = "server";
                        TxCampoB.ReadOnly = true;
                        TxCampoB.Visible = true;

                        DivParam1B.Controls.Add(TxCampoB);

                        //<div class="col-lg-1" id="DivColumC0">
                        HtmlGenericControl DivParam2B = new HtmlGenericControl();
                        DivParam2B.ID = "DivColumF" + i; //boton campo
                        DivParam2B.Attributes["class"] = "col-lg-1";
                        DivParam2B.Visible = true;

                        //<button id="BtL0" type="button" runat="server" style="width:30%" class="btn btn-danger btn-circle" visible="true" onServerClick="BtnActivate_click"><i id="I1" runat="server" class="fa fa-thumbs-o-down"></i></button>
                        Button BtCampoB = new Button();
                        BtCampoB.ID = "btD" + i; //texto campo
                        BtCampoB.Text = "";// "<i id='I1' runat='server' class='fa fa-thumbs-o-down'></i>";
                        BtCampoB.Attributes["class"] = "btn btn-danger btn-circle";
                        BtCampoB.Visible = true;

                        DivParam2B.Controls.Add(BtCampoB);

                        DivContent.Controls.Add(DivParamB);
                        DivContent.Controls.Add(DivParam1B);
                        DivContent.Controls.Add(DivParam2B);


                        //gvCabecera.Columns.Add(DataControlField);
                        //gvControl.Columns.Remove(DataControlField);
                    }

                    if ((i % 2) != 0)
                    {
                        //DivCampos0.Controls.Add(DivContent);
                        a += 1;
                    }

                    i += 1;
                }
                BodyCampos.Attributes["height"] = (i * a).ToString();

                //gvControl.DataKeyNames = System.Text.RegularExpressions.Regex.Split(data, ";");
            }

            //Crea el grid ficheros temporal
            //CreaGridFilesVacio();
            //Busca Error
            //Lberror.Text = "";
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }


        private void MontaEtiquetaOrdenCompra()
        {
            
            string[] Partes = System.Text.RegularExpressions.Regex.Split(this.Session["Etiqueta"].ToString(), Environment.NewLine);
            for (int i = 0; i < Partes.Count() - 1; i++)
            {
                if (Partes[i].Contains("LbEmpresaP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    LbEmpresaPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("LbProveedorP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    LbProveedorPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("lbNumPedidoP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    lbNumPedidoPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("LbLineaPedidoP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    LbLineaPedidoPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("lbSeriePedidoP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    lbSeriePedidoPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("LbProductoP"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    LbProductoPOrdenCompra.Text = Parte[1];
                }
                if (Partes[i].Contains("LbVariedadP1"))
                {
                    string[] Parte = System.Text.RegularExpressions.Regex.Split(Partes[i].ToString(), "=");
                    LbVariedadPOrdenCompra.Text = Parte[1];
                }
            }
            if (this.Session["IDCabecera"].ToString() == "0")
            {
                btnGenerateTodoCompra_Click(null, null);
                this.Session["IDCabecera"] = "1";
            }
        }

        private void Carga_Impresoras(string ID)
        {
            try
            {
                string SQL = "";
                if (ID == "0")
                {
                    SQL = " SELECT DISTINCT(A.ZID) as IDPRINT, A.ZDESCRIPCION ";
                    SQL += " FROM ZPRINTER A ";
                    SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_SECUENCIA ";
                    SQL += " INNER JOIN ZSECUENCIAS C ON B.ZID_SECUENCIA = C.ZID ";
                }
                else
                {
                    SQL = " SELECT DISTINCT(B.ZID_PRINTER) as IDPRINT, A.ZDESCRIPCION, B.ZORDEN ";
                    SQL += " FROM ZPRINTER A ";
                    SQL += " INNER JOIN ZPRINTERFORM B ON B.ZID_PRINTER = A.ZID ";
                    SQL += " INNER JOIN ZSECUENCIAS C ON C.ZID = B.ZID_SECUENCIA ";
                    SQL += " WHERE C.ZID = '" + ID + "'";
                    SQL += " ORDER BY B.ZORDEN ";
                }

                DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                DrPrinters.Items.Clear();
                DrPrinters.DataValueField = "IDPRINT";
                DrPrinters.DataTextField = "ZDESCRIPCION";
                DrPrinters.DataSource = dbB;
                DrPrinters.DataBind();
                Printers(DrPrinters.SelectedItem.Value);
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                //Server.Transfer("Inicio.aspx");
                Server.Transfer("Inicio.aspx");
            }
        }


        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            //Server.Transfer("Nomina.aspx");
            Server.Transfer("Nomina.aspx");
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

        private void Printers(string ID)
        {
            panelContentsLotes.Visible = true;
            panelContentsQRLotes.Visible = false;
            panelContentsFTLotes.Visible = false;
            panelContentsPaletAlvLotes.Visible = false;

            if (ID == "1" || ID == "4")
            {
                panelContentsLotes.Visible = true;
                panelContentsQRLotes.Visible = false;
                panelContentsFTLotes.Visible = false;
                panelContentsPaletAlvLotes.Visible = false;
            }
            else if (ID == "2")
            {
                panelContentsLotes.Visible = false;
                panelContentsQRLotes.Visible = true;
                panelContentsFTLotes.Visible = false;
                panelContentsPaletAlvLotes.Visible = false;
            }
            else if (ID == "3")
            {
                panelContentsLotes.Visible = false;
                panelContentsQRLotes.Visible = false;
                panelContentsFTLotes.Visible = true;
                panelContentsPaletAlvLotes.Visible = false;
            }
            else if (ID == "6")
            {
                panelContentsLotes.Visible = false;
                panelContentsQRLotes.Visible = false;
                panelContentsFTLotes.Visible = false;
                panelContentsPaletAlvLotes.Visible = true;
            }
        }

        private void Campos_ordenados()
        {
            this.Session["SelectQR"] = ConfigurationManager.AppSettings.Get("CRcode");


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
            //ColHorizontal1.Attributes["style"].ToString();
            //Carga_tabla();
            //Carga_tablaLista();

            //gvCabecera.DataBind();
            //gvControl.DataBind();
            //gvLista.DataBind();
        }

        //private void Carga_los_palet()
        //{
        //    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
        //    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA FROM  ZCARGA_LINEA WHERE ID_CABECERA = " + TxtNumero.Text; // this.Session["IDCabecera"].ToString();

        //    //Lberror.Text += SQL + "1- Carga_los_palet " + Variables.mensajeserver;
        //    DataTable dt = Main.BuscaLote(SQL).Tables[0];
        //    //Lberror.Text += " 1- Carga_los_palet " + Variables.mensajeserver;

        //    this.Session["NumeroPalet"] = dt.Rows.Count.ToString();
        //    CreaPalets(dt);
        //}

        protected void BtMenus_Click(object sender, EventArgs e)
        {
            //if (divMenu.Visible == true)
            //{
            //    divMenu.Visible = false;
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
            //    li.Attributes.CssStyle.Add("margin", "0");
            //    MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
            //}
            //else
            //{
            //    divMenu.Visible = true;
            //    HtmlGenericControl li = (HtmlGenericControl)FindControl("pagevistaform");
            //    li.Attributes.CssStyle.Add("margin", "0 0 0 250px");
            //    MasMinMenu.Attributes["class"] = "fa fa-chevron-left fa-2x";
            //}
        }

        public void Carga_Menus()
        {
            pagevistaform.Attributes["style"] = "";
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu0");
            HtmlGenericControl panel = (HtmlGenericControl)cont.FindControl("accordion0");

            if (this.Session["Menu"].ToString() == "1")
            {
                ////el 1
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
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
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)cont.FindControl("Menu1");
                //li.Attributes["class"] = "";
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
                //panel = (HtmlGenericControl)cont.FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)cont.FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
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
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)cont.FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                this.Session["IDSession"] = "Children";
                panel = (HtmlGenericControl)cont.FindControl("accordion8");
                this.Session["IDSession"] = "Children";
                //panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)cont.FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
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
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)cont.FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)cont.FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)cont.FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
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
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)cont.FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "";

                pagevistaform.Attributes["style"] = "height: 100%;";

                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)cont.FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)cont.FindControl("accordion8");
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
                li = (HtmlGenericControl)cont.FindControl("Menu6");
                li.Attributes["class"] = "active";
                //li = (HtmlGenericControl)cont.FindControl("Menu1");
                //li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu2");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu3");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu4");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)cont.FindControl("Menu5");
                li.Attributes["class"] = "";

                pagevistaform.Attributes["style"] = "height: 100%;";

                panel = (HtmlGenericControl)cont.FindControl("Horizontal");
                panel.Attributes["class"] = "tab-pane fade active in";
                //panel = (HtmlGenericControl)cont.FindControl("accordion");
                //panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("DivLotes");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion3");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)cont.FindControl("accordion5");
                panel.Attributes["class"] = "tab-pane fade";
                //panel = (HtmlGenericControl)cont.FindControl("accordion8");
                //panel.Attributes["class"] = "tab-pane fade";
                accordion5.Visible = false;
                accordion2.Visible = false;
                accordion3.Visible = false;
                accordion8.Visible = false;
                Horizontal.Visible = true;
                DivLotes.Visible = false;
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
            this.Session["IDCabecera"] = "0";
            string Miro = DrPrinters.SelectedItem.Value;
            Printers(DrPrinters.SelectedItem.Value);
            PlaceHolderFitoOrdenCompra.Controls.Clear();
            //this.Session["SelectPrinter"] = "1";
            //Carga Los listados de lotes

            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                //this.Session["IDCabecera"] = "0";
                btnGenerateLote_Click(sender, e);
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(sender, e);
                }
                else
                {
                    btnGenerateTodoLote_Click(sender, e);
                }
            }
            else
            {
                if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
                {
                    MontaEtiquetaOrdenCompra();
                }
            }

            //btnGenerateTodoCompra_Click(sender, e);

        }
        protected void BuscaAnchor_Click(object sender, EventArgs e)
        {

        }

        //private void LimpiaCajas()
        //{
        //    LbEmpresaS.Text = "";
        //    LbProveedorS.Text = "";
        //    lbSeriePedidoS.Text = "";
        //    lbNumPedidoS.Text = "";
        //    LbLineaPedidoS.Text = "";
        //    LbProductoS.Text = "";
        //    LbProductoS.Text = "";


        //    LbEmpresaP.Text = "";
        //    LbProveedorP.Text = "";
        //    lbSeriePedidoP.Text = "";
        //    lbNumPedidoP.Text = "";
        //    LbLineaPedidoP.Text = "";
        //    LbProductoP.Text = "";
        //}

        protected void Carga_Combos_vacios_Click(object sender, EventArgs e)
        { 
            DrConsultas.SelectedIndex = -1;
            DrConsultas2.SelectedIndex = -1;
            DrProveedor.SelectedIndex = -1;
            DrProveedor2.SelectedIndex = -1;
            DrNumPedido.SelectedIndex = -1;
            DrNumPedido2.SelectedIndex = -1;
            DrFechaEntrega.SelectedIndex = -1;
            DrFechaEntrega2.SelectedIndex = -1;
            DrProducto.SelectedIndex =  -1;
            DrProducto2.SelectedIndex = -1;

            //Modificación Gloria 13/06/2022
            DrConsultas.Attributes.Add("style", "background-color:#f5f5f5");
            DrConsultas2.Attributes.Add("style", "background-color:#f5f5f5");
            DrProveedor.Attributes.Add("style", "background-color:#f5f5f5");
            DrProveedor2.Attributes.Add("style", "background-color:#f5f5f5");
            DrFechaEntrega.Attributes.Add("style", "background-color:#f5f5f5");
            DrFechaEntrega2.Attributes.Add("style", "background-color:#f5f5f5");
            DrProducto.Attributes.Add("style", "background-color:#f5f5f5");
            DrProducto2.Attributes.Add("style", "background-color:#f5f5f5");
            DrNumPedido.Attributes.Add("style", "background-color:#f5f5f5");
            DrNumPedido2.Attributes.Add("style", "background-color:#f5f5f5");
            Carga_tabla();
            BtGralConsulta_Click(null, null);

        }


        protected void Btfiltra_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            Carga_tabla();

            this.Session["IDCabecera"] = "0";
            if (txtQRCode.Text != "")
            {
                btnGenerateLote_Click(sender, e);
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(sender, e);
                }
                else
                {
                    btnGenerateTodoLote_Click(sender, e);
                }
            }
            if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
            {
                MontaEtiquetaOrdenCompra();
            }
            //Carga_tablaLista();

        }


        protected void BtGralConsulta_Click(object sender, EventArgs e)
        {
            //Carga_tablaListaFiltro();
            HtmlButton btn = (HtmlButton)sender;
            //if(btn.ID == "BtGralConsulta")
            
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

            string SQL = "UPDATE ZCARGA_LINEA set ESTADO =  2 ";

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
            this.Session["IDCabecera"] = "0";
            dvDrlist.Visible = true;
            dvPrinters.Visible = false;
            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                //this.Session["IDCabecera"] = "0";
                btnGenerateLote_Click(sender, e);
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(sender, e);
                }
                else
                {
                    btnGenerateTodoLote_Click(sender, e);
                }

                //btnGenerate_Click(sender, e);
                //btnGenerateZXING_Click(sender, e);
                //if (DrPrinters.SelectedItem.Value == "4")
                //{
                //    btnGeneraTodoPerf_Click(sender, e);
                //}
                //else
                //{
                //    btnGenerateTodo_Click(sender, e);
                //}
                ////btnGenerateTodo_Click(sender, e);
            }
            else
            {
                LimpiaCajas();
                alertaErr.Visible = false;
                if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
                {
                    MontaEtiquetaOrdenCompra();
                }

            }

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
            //DrOrdenMin.Items.Add(TxtNumero.Text + "; " + DrPais.SelectedItem.Value + "; " + TxtFecha.Text);
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
            //VistaOrden.Visible = true;
            //VistaOrdenNO.Visible = false;
        }

        protected void ImageFiltro_Click(object sender, EventArgs e)
        {
            HtmlInputImage img = (HtmlInputImage)sender;
            ContentPlaceHolder cont = new ContentPlaceHolder();
            HtmlGenericControl li = null;

            if (img.ID == "imgFiltro")
            {
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                li = (HtmlGenericControl)cont.FindControl("PanelgeneralFiltro");
            }
            else
            {
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                li = (HtmlGenericControl)cont.FindControl("Div4");
            }
           
            if (li.Visible == true)
            {
                li.Visible = false;
            }
            else
            {
                li.Visible = true;
            }
            li = (HtmlGenericControl)cont.FindControl("PanelgeneralFiltro2"); // (HtmlGenericControl)FindControl("PanelgeneralFiltro2");
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
#pragma warning disable CS0219 // La variable 'a' está asignada pero su valor nunca se usa
            string a = "";
#pragma warning restore CS0219 // La variable 'a' está asignada pero su valor nunca se usa
            DropDownList list = (DropDownList)sender;
            if (list.ID.Contains("DrConsultas2"))
            {
                DrConsultas.Text = DrConsultas2.Text;
                DrConsultas.SelectedIndex = DrConsultas2.SelectedIndex;
                a = "DropDownList7";
            }
            else if (list.ID.Contains("DrConsultas"))
            {
                DrConsultas2.Text = DrConsultas.Text;
                DrConsultas2.SelectedIndex = DrConsultas.SelectedIndex;
                a = "DrSelectFiltro";
            }
            else if (list.ID.Contains("DrProveedor2"))
            {
                DrProveedor.Text = DrProveedor2.Text;
                DrProveedor.SelectedIndex = DrProveedor2.SelectedIndex;
                a = "DropDownList7";
            }
            else if (list.ID.Contains("DrProveedor"))
            {
                DrProveedor2.Text = DrProveedor.Text;
                DrProveedor2.SelectedIndex = DrProveedor.SelectedIndex;
                a = "DrSelectFiltro";
            }
            else if (list.ID.Contains("DrNumPedido2"))
            {
                DrNumPedido.Text = DrNumPedido2.Text;
                DrNumPedido.SelectedIndex = DrNumPedido2.SelectedIndex;
                a = "DropDownList7";
            }
            else if (list.ID.Contains("DrNumPedido"))
            {
                DrNumPedido2.Text = DrNumPedido.Text;
                DrNumPedido2.SelectedIndex = DrNumPedido.SelectedIndex;
                a = "DrSelectFiltro";
            }

            else if (list.ID.Contains("DrFechaEntrega2"))
            {
                DrFechaEntrega.Text = DrFechaEntrega2.Text;
                DrFechaEntrega.SelectedIndex = DrFechaEntrega2.SelectedIndex;
                a = "DropDownList7";
            }
            else if (list.ID.Contains("DrFechaEntrega"))
            {
                DrFechaEntrega2.Text = DrFechaEntrega.Text;
                DrFechaEntrega2.SelectedIndex = DrFechaEntrega.SelectedIndex;
                a = "DrSelectFiltro";
            }
            else if (list.ID.Contains("DrProducto2"))
            {
                DrProducto.Text = DrProducto2.Text;
                DrProducto.SelectedIndex = DrProducto2.SelectedIndex;
                a = "DropDownList7";
            }
            else if (list.ID.Contains("DrProducto"))
            {
                DrProducto2.Text = DrProducto.Text;
                DrProducto2.SelectedIndex = DrProducto.SelectedIndex;
                a = "DrSelectFiltro";
            }
            Btfiltra_Click(null, null);
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
            ////if (TxtNumero.Text == "")
            ////{
            //    ////TextAlerta.Text = "Debe crear o editar previamente una órden de Cabecera para tratar con la lista de pedidos";
            //    ////alerta.Visible = true;
            //    //Lbmensaje.Text = "Debe crear o editar previamente una órden de Cabecera para tratar con la lista de pedidos";
            //    //Asume.Visible = true;
            //    //Modifica.Visible = false;
            //    //cuestion.Visible = false;
            //    //Decide.Visible = false;
            //    ////btnasume.Visible = true;
            //    //DvPreparado.Visible = true;
            //    ////BtnAcepta.Visible = false;
            //    ////BTnNoAcepta.Visible = false;
            ////    return;
            ////}
            //GridViewRow row = gvControl.Rows[e.RowIndex];
            //string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

            ////UDSPEDIDAS, UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES THEN(CONVERT(DECIMAL(18, 6), UDSPEDIDAS) - CONVERT(DECIMAL(18, 6), UDSSERVIDAS) - CONVERT(DECIMAL(18, 6), UDSENCARGA))

            //decimal rUDSPEDIDAS = 1.0M;
            //decimal rUDSSERVIDAS = 1.0M;
            //decimal rUDSENCARGA = 1.0M;

            //decimal rUDSPENDIENTES = 1.0M;
            //decimal rUDSACARGAR = 1.0M; ;
            //decimal rNUMPALET = 1.0M;
            //decimal rCAJAS = 1.0M;
            //decimal rUNIDADES = 1.0M;

            //try
            //{
            //    TextBox txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsPedidas") as TextBox);
            //    //TextBox txtBox = (TextBox)(row.Cells[10].Controls[0]);
            //    if (txtBox != null)
            //    {
            //        rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }
            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsServidas") as TextBox);
            //    //txtBox = (TextBox)(row.Cells[11].Controls[0]);
            //    if (txtBox != null)
            //    {
            //        rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }
            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOUdsCarga") as TextBox);
            //    //txtBox = (TextBox)(row.Cells[12].Controls[0]);
            //    if (txtBox != null)
            //    {
            //        rUDSENCARGA = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }
            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPendientes") as TextBox);
            //    //txtBox = (TextBox)(row.Cells[13].Controls[0]);
            //    if (txtBox != null)
            //    {
            //        rUDSPENDIENTES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }

            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCargar") as TextBox);
            //    if (txtBox != null)
            //    {
            //        rUDSACARGAR = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }

            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOTipo") as TextBox);
            //    if (txtBox != null)
            //    {
            //        if (txtBox.Text != "") // txtBox.Text && Esta == false)
            //        {
            //            rUNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //        }
            //        else
            //        {
            //            string AA = "0.00";
            //            rUNIDADES = Convert.ToDecimal(AA.Replace(".", ","));
            //        }
            //    }

            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOPalet") as TextBox);
            //    if (txtBox != null)
            //    {
            //        rNUMPALET = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //    }


            //    txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabOCajas") as TextBox);
            //    if (txtBox != null)
            //    {
            //        if (txtBox.Text != "") // txtBox.Text && Esta == false)
            //        {
            //            if (Convert.ToInt32(rUNIDADES) > 0)
            //            {
            //                rCAJAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
            //                if (rNUMPALET > 0)
            //                {
            //                    rUDSACARGAR = ((rCAJAS * rUNIDADES) * rNUMPALET) / 1000;
            //                }
            //                else
            //                {
            //                    rUDSACARGAR = (rCAJAS * rUNIDADES) / 1000;
            //                }
            //            }
            //        }
            //    }

            //    if ((rUDSACARGAR + rUDSSERVIDAS) <= rUDSPEDIDAS)
            //    {
            //        rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSACARGAR);
            //    }
            //    else
            //    {
            //        //Lbmensaje.Text = "No se pueden cargar más unidades de las que quedan pendientes.";
            //        //Asume.Visible = true;
            //        //Modifica.Visible = false;
            //        //cuestion.Visible = false;
            //        //Decide.Visible = false;
            //        //DvPreparado.Visible = true;
            //        return;
            //    }
            //    //int a = container2.Controls.Count;


            //    //string SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
            //    //SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
            //    //SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
            //    //SQL += " AND A.ID = B.ID ";
            //    //SQL += " AND A.ID = " + miro;
            //    //SQL += " GROUP BY A.ID_CABECERA ";
            //    //Variables.Error = "";
            //    ////Lberror.Text = SQL;


            //    //Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
            //    //rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));// + rUDSACARGAR;
            //    //Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;

            //    //DBHelper.ExecuteNonQuery(SQL);

            //    //SQL = "UPDATE ZCARGA_ORDEN set UDSPENDIENTES = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
            //    //SQL += " UDSACARGAR = " + rUDSACARGAR.ToString().Replace(",", ".") + ", ";
            //    //SQL += " UDSENCARGA = " + rUDSENCARGA.ToString().Replace(",", ".") + ", ";
            //    //SQL += " NUMPALET = " + rNUMPALET.ToString().Replace(",", ".") + ", ";
            //    //SQL += " ID_CABECERA = " + TxtNumero.Text + ", ";
            //    //SQL += " CAJAS = '" + rUNIDADES + "', ";
            //    ////SQL += " SERIE_PED = '" + rSERIE_PEDIDO + "', ";               
            //    //SQL += " ESTADO = 1 ";
            //    //SQL += " WHERE ID = " + miro;

            //    //Variables.Error = "";
            //    ////Lberror.Text = SQL;

            //    ////Lberror.Text += SQL + "1- gvControl_RowUpdating " + Variables.mensajeserver;
            //    //DBHelper.ExecuteNonQuery(SQL);
            //    //Lberror.Text += " 1- gvControl_RowUpdating " + Variables.mensajeserver;




            //    Carga_tabla();

            //    gvControl.EditIndex = -1;
            //    //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //    //gvControl.DataSource = dt;
            //    gvControl.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    //Lberror.Text += ". " + ex.Message;
            //    //Lberror.Visible = true;
            //}
        }


        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            //Carga_tablaLista(e.SortExpression);
        }

        protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
        {

            return;
            //gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //int indice = gvControl.EditIndex = e.NewEditIndex;


            //Carga_tabla();
            ////string carga = gvControl.Width.ToString();
            ////int cuantos = gvControl.Rows[indice].Cells.Count - 2;
            ////int Parcial = Convert.ToInt32(carga) - cuantos;

            ////gvControl.Rows[indice].Cells[0].Enabled = false;
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
            //gvControl.Rows[indice].Cells[11].Enabled = false;
            //gvControl.Rows[indice].Cells[12].Enabled = false;
            //gvControl.Rows[indice].Cells[13].Enabled = false;
            //gvControl.Rows[indice].Cells[17].Enabled = false;
            //gvControl.Rows[indice].Cells[18].Enabled = false;

            ////gvControl.Rows[indice].Cells[1].Width = 40;
            ////gvControl.Rows[indice].Cells[2].Width = 40;
            ////gvControl.Rows[indice].Cells[3].Width = 40;
            ////gvControl.Rows[indice].Cells[4].Width = 40;
            ////gvControl.Rows[indice].Cells[5].Width = 40;
            ////gvControl.Rows[indice].Cells[6].Width = 40;
            ////gvControl.Rows[indice].Cells[7].Width = 40;
            ////gvControl.Rows[indice].Cells[8].Width = 40;
            ////gvControl.Rows[indice].Cells[9].Width = 40;
            ////gvControl.Rows[indice].Cells[10].Width = 40;
            ////gvControl.Rows[indice].Cells[11].Width = 40;
            ////gvControl.Rows[indice].Cells[12].Width = 40;
            ////gvControl.Rows[indice].Cells[13].Width = 40;
            ////gvControl.Rows[indice].Cells[14].Width = 40;
            ////gvControl.Rows[indice].Cells[15].Width = 40;
            ////gvControl.Rows[indice].Cells[16].Width = 40;

            ////DataTable dt = this.Session["MiConsulta"] as DataTable;
            ////gvControl.DataSource = dt;
            ////gvControl.DataBind();

            //GridViewRow row = gvControl.Rows[indice];
            //row.BackColor = Color.FromName("#ffead1");

        }

        protected void gvControl_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                GridView MiGrid = (GridView)sender;
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = MiGrid.DataKeys[index].Value.ToString();
                    GridViewRow row = MiGrid.Rows[index];
                    row.BackColor = Color.FromName("#ffead1");
                    //Carga_tabla();
                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;
                    //gvLista.DataBind();
                }

                if (e.CommandName == "BajaOrden")
                {
                    //if (TxtNumero.Text == "" || TxtNumero.Text == "0")
                    //{
                    //    Lbmensaje.Text = "Orden de Carga sin asignar. Debe seleccionar previamente una Orden de Carga para asignar algún pedido.";
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
                    string Todo = "";
                    //string Mira = "";
                    //string VARIEDAD = "";
                    string TIPOPLANTA = ""; ;
                    string NOMBRE = "";



                    decimal rUDSPEDIDAS = 1.0M;
                    decimal rUDSSERVIDAS = 1.0M;

                    //decimal NUMPALET = 1.0M;
                    //decimal UNIDADES = 1.0M;
                    //decimal REPARTO = 1.0M;
                    //decimal PARCIAL = 0;
                    //string Cabecera = TxtNumero.Text;
                    //string SQL = "";
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = MiGrid.Rows[index];
                    string miro = MiGrid.DataKeys[index].Value.ToString();

                    row.BackColor = Color.FromName("#ffead1");

                    Label txtBox = (MiGrid.Rows[Indice].Cells[1].FindControl("LabOEmpresa") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        LbEmpresaPOrdenCompra.Text = txtBox.Text;
                        Todo += "LbEmpresaP.Text=" + txtBox.Text + Environment.NewLine;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[2].FindControl("LabOProveedor") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        LbProveedorPOrdenCompra.Text = txtBox.Text;
                        Todo += "LbProveedorP.Text=" + txtBox.Text + Environment.NewLine;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[3].FindControl("LabOFiscal") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        NOMBRE = txtBox.Text;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[4].FindControl("LabONumero") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        lbNumPedidoPOrdenCompra.Text = txtBox.Text;
                        Todo += "lbNumPedidoP.Text=" + txtBox.Text + Environment.NewLine;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[5].FindControl("LabOLinea") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        LbLineaPedidoPOrdenCompra.Text = txtBox.Text;
                        Todo += "LbLineaPedidoP.Text=" + txtBox.Text + Environment.NewLine;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[6].FindControl("LabOSerie") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        lbSeriePedidoPOrdenCompra.Text = txtBox.Text;
                        Todo += "lbSeriePedidoP.Text=" + txtBox.Text + Environment.NewLine;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[7].FindControl("LabOArticulo") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        LbProductoPOrdenCompra.Text = txtBox.Text;
                        Todo += "LbProductoP.Text=" + txtBox.Text + Environment.NewLine;
                    }



                    txtBox = (MiGrid.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSPEDIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[11].FindControl("LabOUdsServidas") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        rUDSSERVIDAS = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[14].FindControl("LabOPlanta") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        TIPOPLANTA = txtBox.Text;
                    }
                    txtBox = (MiGrid.Rows[Indice].Cells[14].FindControl("LabOPalet") as Label);
                    if (txtBox != null)
                    {
                        LbVariedadPOrdenCompra.Text = txtBox.Text;
                        Todo += "LbVariedadP1.Text=" + txtBox.Text + Environment.NewLine;
                    }

                    this.Session["Etiqueta"] = Todo;

                    btnGenerateTodoCompra_Click(null, null);

                    if(txtQRCode.Text != "")
                    {
                        btnAutoGeneradoLotes_Click(null, null);
                    }
                    //------------------------------------------------
                    return;
                    //------------------------------------------------

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

                    //        string Temporal = NUMPALET.ToString().Replace(".", ",");
                    //        if (Convert.ToDecimal(Temporal) == 0)
                    //        {
                    //            //TextAlerta.Text = "Seleccione un número de palets para asignar";
                    //            //alerta.Visible = true;

                    //            //Lbmensaje.Text = "Seleccione un número de palets para asignar";
                    //            //Asume.Visible = true;
                    //            //cuestion.Visible = false;
                    //            //Decide.Visible = false;
                    //            //Modifica.Visible = false;
                    //            ////btnasume.Visible = true;
                    //            //DvPreparado.Visible = true;
                    //            ////BtnAcepta.Visible = false;
                    //            ////BTnNoAcepta.Visible = false;
                    //            return;
                    //        }
                    //        string[] Partes = System.Text.RegularExpressions.Regex.Split(NUMPALET.ToString(), ",");

                    //        if (Partes[0] != "" && Partes[1] != "")
                    //        {
                    //            decimal Parte1 = (UNIDADES * 1000);
                    //            REPARTO = (Parte1 / NUMPALET);
                    //            PARCIAL = (REPARTO * Convert.ToDecimal("0," + Partes[1])) / 1000;
                    //            REPARTO = (REPARTO / 1000);

                    //            //decimal Parcial = ((UNIDADES * 1000) * NUMPALET) / Convert.ToDecimal("0," + Partes[1]);

                    //            //decimal Unidades = Convert.ToDecimal(Partes[0]);
                    //            //decimal resto = Convert.ToDecimal("0," + Partes[1]);
                    //            //if (resto > 0)
                    //            //{
                    //            //    decimal ParteA = (UNIDADES * 1000) * resto;
                    //            //    int cantidad = Convert.ToInt32(ParteA / NUMPALET);//redondeo a entero
                    //            //    REPARTO = Convert.ToDecimal(cantidad * 2) /1000;
                    //            //    decimal Totales = (cantidad * 2) * Unidades;
                    //            //    PARCIAL = Convert.ToDecimal(Convert.ToInt32((UNIDADES * 1000) - Totales)) /1000;
                    //            //}
                    //        }
                    //        else
                    //        {
                    //            //80080 ----- 10,4                       
                    //            //X    ------ 1    

                    //            REPARTO = (UNIDADES / NUMPALET);
                    //        }

                    //        decimal Unidad = 1.00M;
                    //        int Linea = 0;
                    //        int N = 0;
                    //        //Lberror.Text += SQL + "1- gvControl_RowCommand " + Variables.mensajeserver;
                    //        Object Con = DBHelper.ExecuteScalarSQL("SELECT MAX(POSICIONCAMION) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    //                                                                                                                                         //Lberror.Text += " 1- gvControl_RowCommand " + Variables.mensajeserver;

                    //        if (Con is System.DBNull)
                    //        {
                    //            N = 1;
                    //        }
                    //        else
                    //        {
                    //            N = Convert.ToInt32(Con) + 1;
                    //        }
                    //        //Lberror.Text += SQL + "2- gvControl_RowCommand " + Variables.mensajeserver;
                    //        Con = DBHelper.ExecuteScalarSQL("SELECT MAX(NUMERO_LINEA) FROM  ZCARGA_LINEA A WHERE  ID_CABECERA =" + Cabecera, null); //ID =" + miro + " AND
                    //                                                                                                                                //Lberror.Text += " 2- gvControl_RowCommand " + Variables.mensajeserver;

                    //        if (Con is System.DBNull)
                    //        {
                    //            Linea = 1;
                    //        }
                    //        else
                    //        {
                    //            Linea = Convert.ToInt32(Con) + 1;
                    //        }

                    //        while (NUMPALET >= 1)
                    //        {
                    //            SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                    //            SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED, HASTA  )";
                    //            SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + REPARTO.ToString().Replace(",", ".") + ", " + Unidad.ToString().Replace(",", ".") + ",";
                    //            SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED, ";
                    //            SQL += "'" + Cabecera + "|' +   CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + N + "' ";
                    //            //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                    //            SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                    //            //Lberror.Text += SQL + "3- gvControl_RowCommand " + Variables.mensajeserver;
                    //            DBHelper.ExecuteNonQuery(SQL);
                    //            //Lberror.Text += " 3- gvControl_RowCommand " + Variables.mensajeserver;



                    //            NUMPALET = NUMPALET - Unidad;
                    //            N += 1;
                    //            Linea += 1;
                    //        }
                    //        if (NUMPALET > 0)
                    //        {
                    //            SQL = "INSERT INTO ZCARGA_LINEA (ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, UDSENCARGA, NUMPALET, ";
                    //            SQL += " POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA, SERIE_PED, HASTA )";//, ZSYSDATE 
                    //            SQL += " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, " + PARCIAL.ToString().Replace(",", ".") + ", " + NUMPALET.ToString().Replace(",", ".") + ",";
                    //            SQL += N + ", '', FECHAENTREGA, '" + this.Session["ComputerName"].ToString() + "', 0 ," + Linea + ", " + Cabecera + ", SERIE_PED, ";
                    //            SQL += "'" + Cabecera + "|' +   CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + N + "' ";
                    //            //SQL += " '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                    //            SQL += " FROM ZCARGA_ORDEN WHERE ID = " + miro;

                    //            //Lberror.Text += SQL + "4- gvControl_RowCommand " + Variables.mensajeserver;
                    //            DBHelper.ExecuteNonQuery(SQL);
                    //            //Lberror.Text += " 4- gvControl_RowCommand " + Variables.mensajeserver;
                    //            NUMPALET = NUMPALET - Unidad;
                    //        }

                    //        //Calculo de ZCARGA_ORDEN SEGUN ZCARGA_LINEa
                    //        decimal rUDSENCARGA = 1.0M;
                    //        decimal rUDSPENDIENTES = 1.0M;

                    //        //SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                    //        //SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                    //        //SQL += " WHERE A.ID_CABECERA = " + TxtNumero.Text;
                    //        //SQL += " AND A.ID = B.ID ";
                    //        //SQL += " AND A.ID = " + miro ;
                    //        //SQL += " GROUP BY A.ID_CABECERA ";

                    //        SQL = " SELECT SUM(CONVERT(DECIMAL(18,6), A.UDSENCARGA)) AS UDSENCARGA FROM  ZCARGA_LINEA A ";
                    //        SQL += " INNER JOIN ZCARGA_ORDEN B ON  A.NUMERO = B.NUMERO "; // AND  A.LINEA = B.LINEA AND A.ARTICULO = B.ARTICULO ";
                    //        SQL += " WHERE A.ID = B.ID ";
                    //        SQL += " AND A.NUMERO = B.NUMERO ";
                    //        SQL += " AND A.LINEA = B.LINEA ";
                    //        SQL += " AND A.EMPRESA = B.EMPRESA ";
                    //        SQL += " AND A.ID = " + miro;
                    //        SQL += " GROUP BY A.ID_CABECERA ";

                    //        //Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    //        rUDSENCARGA = Convert.ToDecimal(DBHelper.ExecuteScalarSQL(SQL, null));
                    //        //Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;

                    //        Variables.Error = "";


                    //        rUDSPENDIENTES = rUDSPEDIDAS - (rUDSSERVIDAS + rUDSENCARGA);

                    //        SQL = "UPDATE ZCARGA_ORDEN set UDSPENDIENTES = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                    //        SQL += " UDSACARGAR = " + rUDSPENDIENTES.ToString().Replace(",", ".") + ", ";
                    //        SQL += " UDSENCARGA = " + rUDSENCARGA.ToString().Replace(",", ".") + ", ";
                    //        SQL += " NUMPALET = 0.00 , ";
                    //        //SQL += " ID_CABECERA = " + TxtNumero.Text + ", ";
                    //        SQL += " ESTADO = 1 ";
                    //        SQL += " WHERE ID = " + miro;

                    //        Variables.Error = "";
                    //        //Lberror.Text += SQL + "5- gvControl_RowCommand " + Variables.mensajeserver;
                    //        DBHelper.ExecuteNonQuery(SQL);
                    //        //Lberror.Text += " 5- gvControl_RowCommand " + Variables.mensajeserver;


                    //        //if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
                    //        //{
                    //        //    SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    //        //}
                    //        //else
                    //        //{
                    //        //    SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
                    //        //}
                    //        //DBHelper.ExecuteNonQuery(SQL);

                    //        this.Session["NumeroPalet"] = Linea.ToString();

                    //        Carga_tabla();
                    //        //Carga_tablaLista();

                    //        gvControl.EditIndex = -1;
                    //        gvControl.DataBind();
                    //    }
                    //    if (e.CommandName == "Cancel")
                    //    {
                    //        index = int.Parse(e.CommandArgument.ToString());
                    //        Indice = index;
                    //        this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();
                    //        string Miro = gvControl.DataKeys[index].Value.ToString();
                    //        GridViewRow row = gvControl.Rows[index];
                    //        row.BackColor = Color.FromName("#ffead1");
                    //        //GridViewRow row = (GridViewRow)gvControl.Rows[e.CommandArgument];
                    //        //gvControl_BajaOrden(Miro, row);
                    //    }

                }
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                //Lberror.Text = "Control RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvControl.DataKeys[index].Value.ToString() + " " + ex.Message;
                //Lberror.Visible = true;
            }
        }

        protected void btclose_Click(object sender, EventArgs e)
        {
            BodyQR.Visible = false;
            if (txtQRCode.Text != "")
            {
                btnGenerateLote_Click(null, null);
            }
        }


        protected void btnGenerateTodoCompra_Click(object sender, EventArgs e)
        {
            string code = "";
            string CodigoError = "";
            //alerta.Visible = false;
            LbSecuenciaLoteOrdenCompra.Text = "";

            if (LbEmpresaPOrdenCompra.Text == "")
            {
                LimpiaCajas();

                CodigoError += " Empresa,";
            }
            else
            {
                code += LbEmpresaPOrdenCompra.Text + "|";
                LbSecuenciaLoteOrdenCompra.Text += LbEmpresaPOrdenCompra.Text + "|" ;
            }

            if (LbProveedorPOrdenCompra.Text == "")
            {
                LimpiaCajas();

                CodigoError += " Proveedor,";
            }
            else
            {
                code += LbProveedorPOrdenCompra.Text + "|";
                LbSecuenciaLoteOrdenCompra.Text += LbProveedorPOrdenCompra.Text + "|";
            }

            if (lbSeriePedidoPOrdenCompra.Text == "")
            {
                LimpiaCajas();
                CodigoError += " Fecha Corte,";
            }
            else
            {
                code += lbSeriePedidoPOrdenCompra.Text + "|";
                LbSecuenciaLoteOrdenCompra.Text += lbSeriePedidoPOrdenCompra.Text + "|"; 
            }

            if (lbNumPedidoPOrdenCompra.Text == "")
            {
                LimpiaCajas();
                CodigoError += " Número Pedido,";
            }
            else
            {
                code += lbNumPedidoPOrdenCompra.Text + "|";
                LbSecuenciaLoteOrdenCompra.Text += lbNumPedidoPOrdenCompra.Text + "|";
            }

            if (LbLineaPedidoPOrdenCompra.Text == "")
            {
                LimpiaCajas();

                CodigoError += " Número Cajas,";
            }
            else
            {
                code += LbLineaPedidoPOrdenCompra.Text + "|";
                LbSecuenciaLoteOrdenCompra.Text += LbLineaPedidoPOrdenCompra.Text + "|";
            }

            if (LbProductoPOrdenCompra.Text == "")
            {
                LimpiaCajas();

                CodigoError += " Número Plantas,";
            }
            else
            {
                code += LbProductoPOrdenCompra.Text + Environment.NewLine;
                LbSecuenciaLoteOrdenCompra.Text += LbProductoPOrdenCompra.Text;
            }

            LbEmpresaSOrdenCompra.Text ="Empresa: " + LbEmpresaPOrdenCompra.Text;
            LbProveedorSOrdenCompra.Text = "Proveedor: " + LbProveedorPOrdenCompra.Text;
            lbSeriePedidoSOrdenCompra.Text = "Serie Pedido: " + lbSeriePedidoPOrdenCompra.Text;
            lbNumPedidoSOrdenCompra.Text = "Número Pedido: " + lbNumPedidoPOrdenCompra.Text;
            LbLineaPedidoSOrdenCompra.Text = "Línea Pedido: " + LbLineaPedidoPOrdenCompra.Text;
            LbProductoSOrdenCompra.Text = "Producto: " + LbProductoPOrdenCompra.Text;
            if(LbVariedadPOrdenCompra.Text == "")
            {
                LbVariedadPOrdenCompra.Text = "Sin Variedad";
                LbVariedadLSOrdenCompra.Text = LbVariedadPOrdenCompra.Text;
            }
            else
            {
                LbVariedadLSOrdenCompra.Text = LbVariedadPOrdenCompra.Text;
            }
            //if (LbVariedadP.Text == "")
            //{
            //    LbEmpresaP.Text = "";
            //    LbProveedorP.Text = "";
            //    lbSeriePedidoP.Text = "";
            //    lbNumPedidoP.Text = "";
            //    LbLineaPedidoP.Text = "";
            //    LbProductoP.Text = "";
            //    LbVariedadP.Text = "";
            //    CodigoError += " Número Plantas,";
            //}
            //else
            //{
            //    code += LbVariedadP.Text;
            //    //LbCampoS.Text += LbVariedadP.Text ;
            //}

            //if (DrPrinters.SelectedItem.Value == "6")
            //{
            //    code = TxtVariedad.Text; //; + Environment.NewLine;
            //}

            if (CodigoError != "")
            {
                //-----------------------------------------------------
                //No quieren que muestre la ventana de error con los campos vacios
                //-----------------------------------------------------
                //No se puede generar el código QR total por tener los campos siguientes vacios: CodigoError
                //if (this.Session["SelectPrinter"].ToString() == "1")
                //{
                //    LbSecuenciaLote.Text = txtQRCode.Text;
                //}
                //else
                //{
                //Para volver a poner esta ventana de error, Descomentar desde aqui
                //------------------------------
                //TextAlertaErr.Text = "No se puede generar el código QR total por tener los campos siguientes vacios: " + CodigoError;
                //TextAlertaErr.Text += "Genere un registro desde formularios de Scan-IT desde el Móvil, envielo y pruebe nuevamente desde este botón. " + CodigoError;
                //TextAlerta.Text = "";
                //alertaErr.Visible = true;
                //btnPrint2.Visible = false;
                ////BTerminado.Visible = false;
                //btProcesa.Visible = true;
                //---------------------------------
                return;
                //}
            }
            else
            {
                ////H1Normal.Visible = false;
                ////H1Seleccion.Visible = false;
                ////H1Red.Visible = false;
                ////H1Green.Visible = true;

                ////panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR PROCESADO";
                ////panelPrinter.Attributes["style"] = "color: LimeGreen; font-weight:bold;";

                ////HLoteProceso.InnerText = "Código QR PROCESADO";
                ////HLoteProceso.Attributes.Add("style", "color: LimeGreen; font-weight:bold;");
                //if (chkOnOff.Checked == false)
                //{
                //    TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
                //    TextAlertaErr.Text = "";
                //    alerta.Visible = true;
                //}
                //if (DrPrinters.SelectedItem.Value == "6")
                //{
                //    btnPrintPaletAlv.Visible = true;
                //}
                //else
                //{
                //    btnPrint2.Visible = true;
                //}

                //btProcesa.Visible = false;
                ////BTerminado.Visible = true;
            }

            //Primera Imagen
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200;
                    imgBarCode.Width = 200;
                }
                else
                {
                    imgBarCode.Height = 200;
                    imgBarCode.Width = 200;
                }
            }
            catch (Exception a)
            {
                string b = a.Message;
                //TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                ////TxAltoT.Text = "200";
                ////TxAnchoT.Text = "200";
                //alertaErr.Visible = true;
            }


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

            //if (DrPrinters.SelectedItem.Value == "6")
            //{
            //    PlaceHolderPaletAlv.Controls.Add(imgBarCode);
            //}
            //else
            //{
            PlaceHolderOrdenCompra.Controls.Clear();
            PlaceHolderOrdenCompra.Controls.Add(imgBarCode);
            PlaceHolderOrdenCompra.Visible = true;
            //}


            //Segunda imagen
            qrGenerator = new QRCodeGenerator();
            qrCode = qrGenerator.CreateQrCode(LbVariedadPOrdenCompra.Text, QRCodeGenerator.ECCLevel.H);

            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            result = writer.Write(LbVariedadPOrdenCompra.Text);
            barcodeBitmap = new Bitmap(result);

            imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 100;
                    imgBarCode.Width = 100;
                }
                else
                {
                    imgBarCode.Height = 100;
                    imgBarCode.Width = 100;
                }
            }
            catch (Exception a)
            {
                string b = a.Message;
                //TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                ////TxAltoT.Text = "200";
                ////TxAnchoT.Text = "200";
                //alertaErr.Visible = true;
            }


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

            //if (DrPrinters.SelectedItem.Value == "6")
            //{
            //    PlaceHolderPaletAlvMin.Controls.Add(imgBarCode);
            //}
            //else
            //{
            PlaceHolderOrdenCompraMin.Controls.Clear();
            PlaceHolderOrdenCompraMin.Controls.Add(imgBarCode);
            PlaceHolderOrdenCompraMin.Visible = true;
            //}
            Posicion_Tarjeta();

            BodyQR.Visible = true;

            this.Session["QR"] = code;
            //ReadQRCode();
        }

        private void Posicion_Tarjeta()
        {
            //string miro = this.Session["Menu"].ToString();
            //if (this.Session["Menu"].ToString() == "1")
            //{
            //    BodyQR.Attributes["style"] = "top: 200px; left: 30%; height: 720px; width: 40%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); position:absolute;";
            //    BodyQR.Attributes["class"] = "alert alert-grey";
            //}
            //else if (this.Session["Menu"].ToString() == "3")
            //{
            //    BodyQR.Attributes["style"] = "top: 10px; left: 30%; height: 720px; width: 40%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); position:absolute;";
            //    BodyQR.Attributes["class"] = "alert alert-grey";
            //}
            //else if (this.Session["Menu"].ToString() == "6")
            //{
                BodyQR.Attributes["style"] = "top: 300px; left: 5%; height: 720px; width: 40%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155); position:absolute;";
                BodyQR.Attributes["class"] = "alert alert-grey";
            //}
            //else
            //{
            //    BodyQR.Attributes["style"] = "height: 720px; width: 40%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);";
            //    BodyQR.Attributes["class"] = "alert alert-grey centrado";
            //}

        }
        //Read Code from QR Image
        private void ReadQRCode()
        {
            Bitmap imgBarCode = this.Session["CodigoQR"] as Bitmap;

            var reader = new BarcodeReader();
            //string filename = Path.Combine(Request.MapPath("~/images"), "QRImage.png");

            //Detech and decode the barcode inside the bitmap
            try
            {
                var result = reader.Decode(new Bitmap(imgBarCode));
                if (result != null)
                {
                    if (this.Session["SelectQR"].ToString() == "0")
                    {
                        if (this.Session["QR"].ToString() != result.Text)
                        {
                            LBReadQROrdenCompra.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQROrdenCompra.Attributes["sytle"] = "color:red;";
                            LBReadQRLotes.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQRLotes.Attributes["sytle"] = "color:red;";
                        }
                        else
                        {
                            LBReadQROrdenCompra.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQROrdenCompra.Attributes["sytle"] = "color:black;";
                            LBReadQRLotes.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQRLotes.Attributes["sytle"] = "color:black;";
                        }
                    }
                    else
                    {
                        if (this.Session["QR"].ToString() != result.Text)
                        {
                            LBReadQROrdenCompra.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQROrdenCompra.Attributes["sytle"] = "color:red;";
                            LBReadQRLotes.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQRLotes.Attributes["sytle"] = "color:red;";
                        }
                        else
                        {
                            LBReadQROrdenCompra.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQROrdenCompra.Attributes["sytle"] = "color:black;";
                            LBReadQRLotes.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQRLotes.Attributes["sytle"] = "color:black;";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("Lee Imagen QR --> " + ex.Message);
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
            GridView GV = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < GV.Columns.Count; i++)
                {
                    //if (i == 0) { e.Row.Cells[i].ToolTip = "Edición de Registro"; }
                    //else if (i == 1) { e.Row.Cells[i].ToolTip = "Selecciona Órden"; }
                    //else if (i == 10) { e.Row.Cells[i].ToolTip = "Unidades pedidas desde GoldenSoft"; }
                    //else if (i == 11) { e.Row.Cells[i].ToolTip = "Unidades servidas desde GoldenSoft"; }
                    //else if (i == 12) { e.Row.Cells[i].ToolTip = "Cálculo automático de la suma de las unidades alojadas en las listas de carga inferior"; }
                    //else if (i == 13) { e.Row.Cells[i].ToolTip = "Cálculo automático de las Unidades pendientes resultantes de las Unidades pedidas, menos las Unidades a Cargar más las Unidades servidas"; }
                    //else if (i == 14) { e.Row.Cells[i].ToolTip = "Seleccione manualmente las Unidades a cargar en este momento"; }
                    //else if (i == 15) { e.Row.Cells[i].ToolTip = "Cálculo manual de los Palets necesarios para las Unidades a cargar en este momento"; }
                    //else
                    //{
                        e.Row.Cells[i].ToolTip = GV.Columns[i].HeaderText;
                    //}
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
                    //if(GV.ID != "gvControl")
                    //{
                    //    ImageButton editar = e.Row.Cells[0].Controls[0] as ImageButton;
                    //    editar.Visible = false;
                    //}
                    ImageButton SubeCarga = ((ImageButton)e.Row.FindControl("ibtbajaOrden"));
                    SubeCarga.Visible = false;

                    //editar = e.Row.Cells[1].Controls[0] as ImageButton;
                    //editar.Visible = false;
                    //CommandField editbutton = (CommandField)sender;
                    //editbutton.ShowEditButton = false;
                }

                //if (miro == "2")
                //{
                //    e.Row.BackColor = Color.FromName("#eaf5dc");
                //    //verde
                //}
                //else
                //{
                //    miro = DataBinder.Eval(e.Row.DataItem, "NUMPALET").ToString();
                //    if (miro != "0,00")
                //    {
                //        e.Row.BackColor = Color.FromName("#d2f2f6");
                //    }
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
                //}

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
            gvHorizontal.SelectedRow.BackColor = Color.FromName("#565656");
        }

        //protected void checkSiEl_Click(object sender, EventArgs e)
        //{
        //    //Cuestion: solo elimina
        //    checkSi_Click(sender, e);
        //}

        protected void checkSiElC_Click(object sender, EventArgs e)
        {
            //Cuestion: elimina y corrige
            if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////             ID; UNIDADES; numero linea; posicion camion
            //ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //Select
            //string SQL = " SELECT * FROM ZCARGA_LINEA  ";
            //SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
            //SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";
            //SQL += " ORDER BY POSICIONCAMION ";

            //DataTable dt = Main.BuscaLote(SQL).Tables[0];
            //int A = 0;

            //foreach (DataRow filas in dt.Rows)
            //{
            //    if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[3]))
            //    {
            //        A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //        SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + " WHERE ID = " + filas["ID"].ToString() + " ";
            //        SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //        SQL += " AND ZID = " + filas["ZID"].ToString() + " ";

            //        DBHelper.ExecuteNonQuery(SQL);
            //    }
            //}
            //Cuestion: ahora elimina
            checkSi_Click(sender, e);
        }


        //protected void checkOk_Click(object sender, EventArgs e)
        //{
        //    //DvPreparado.Visible = false;
        //    //cuestion.Visible = false;
        //    //Asume.Visible = false;
        //    //Modifica.Visible = false;
        //    //Decide.Visible = false;
        //    ////BtnAcepta.Visible = false;
        //    ////BTnNoAcepta.Visible = false;
        //    //Lbmensaje.Text = "";
        //    this.Session["ElIDaBorrar"] = "";
        //}

        //protected void checkSi_Click(object sender, EventArgs e)
        //{
        //    //DvPreparado.Visible = false;
        //    //cuestion.Visible = false;
        //    //Asume.Visible = false;
        //    //Modifica.Visible = false;
        //    //Decide.Visible = false;
        //    //Lbmensaje.Text = "";
           

        //    //if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
        //    //////             ID; UNIDADES; numero linea
        //    ////ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
        //    //string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

        //    //string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
        //    //SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
        //    //SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
        //    //SQL += " NUMPALET = 0.00 ";
        //    //SQL += " WHERE ID = " + Trata[0];
        //    //DBHelper.ExecuteNonQuery(SQL);

        //    //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

        //    //DBHelper.ExecuteNonQuery(SQL);

        //    //if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
        //    //{
        //    //    SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
        //    //}
        //    //else
        //    //{
        //    //    SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
        //    //}
        //    //DBHelper.ExecuteNonQuery(SQL);

        //    Carga_tabla();
        //    //Carga_tablaLista();

        //    //gvLista.EditIndex = -1;

        //    //gvLista.DataBind();

        //    this.Session["ElIDaBorrar"] = "";
        //}



        protected void checkSiMlC_Click(object sender, EventArgs e)
        {
            string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //Select
            //string SQL = " SELECT * FROM ZCARGA_LINEA  ";
            //SQL += " WHERE (ESTADO in(0,1,2) OR ESTADO IS NULL) ";
            //SQL += " AND ID_CABECERA = " + TxtNumero.Text + " ";
            //SQL += " ORDER BY POSICIONCAMION ";

            //DataTable dt = Main.BuscaLote(SQL).Tables[0];
            //int A = 0;
            //int B = 0;
            //// MiPosicion, Lanuevaposicion, ID, ID_CABECERA, NUMERO_LINEA
            ////this.Session["ElIDaBorrar"] = Con + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;

            ////si la Lanuevaposicion es mayor que MiPosicion actual
            //if (Convert.ToInt32(Trata[1]) > Convert.ToInt32(Trata[0]))
            //{
            //    foreach (DataRow filas in dt.Rows)
            //    {
            //        //si la posicion es igual o mayor que la posicion elegida
            //        string miro = filas["POSICIONCAMION"].ToString();

            //        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) >= Convert.ToInt32(Trata[1]))
            //        {
            //            //si la posicion es igual que la posicion elegida
            //            if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[1]))
            //            {
            //                if (filas["NUMERO_LINEA"].ToString() == Trata[4])
            //                {
            //                }
            //                else
            //                {
            //                    A = Convert.ToInt32(Trata[1]) - 1;
            //                    SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
            //                    SQL += " ESTADO = 1 ";
            //                    SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
            //                    SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                    SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //                    SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //                    DBHelper.ExecuteNonQuery(SQL);
            //                }
            //            }
            //        }
            //        //si la posicion es mayor que la posicion que tenia pero menor que la eligida
            //        else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) >= Convert.ToInt32(Trata[0]) &&
            //            Convert.ToInt32(filas["POSICIONCAMION"].ToString()) < Convert.ToInt32(Trata[1]) &&
            //            filas["NUMERO_LINEA"].ToString() != Trata[4])
            //        {
            //            A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //            SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
            //            SQL += " ESTADO = 1 ";
            //            SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
            //            SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //            SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //            SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //            DBHelper.ExecuteNonQuery(SQL);
            //        }
            //    }
            //}
            ////si la Lanuevaposicion es menor que MiPosicion actual
            //else
            //{
            //    foreach (DataRow filas in dt.Rows)
            //    {
            //        string miro = filas["POSICIONCAMION"].ToString();
            //        //si la posicion es igual que la posicion elegida
            //        if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) == Convert.ToInt32(Trata[1]))
            //        {
            //            if (filas["NUMERO_LINEA"].ToString() == Trata[4])
            //            {
            //            }
            //            else
            //            {
            //                A = Convert.ToInt32(Trata[1]) + 1;
            //                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
            //                SQL += " ESTADO = 1 ";
            //                SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
            //                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //                SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //                DBHelper.ExecuteNonQuery(SQL);
            //            }
            //        }
            //        //si la posicion es igual que la posicion elegida pero menor o igual que la posicion que tenia
            //        else if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) > Convert.ToInt32(Trata[1]) && Convert.ToInt32(filas["POSICIONCAMION"].ToString()) <= Convert.ToInt32(Trata[0]))
            //        {
            //            //si la posicion es menor o igual que la posicion que tenia
            //            if (Convert.ToInt32(filas["POSICIONCAMION"].ToString()) <= Convert.ToInt32(Trata[0]))
            //            {
            //                A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) + 1;
            //                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
            //                SQL += " ESTADO = 1 ";
            //                SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
            //                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //                SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //                DBHelper.ExecuteNonQuery(SQL);
            //            }
            //            //si la posicion es menor a la que tenia y mayor que la elegida
            //            else
            //            {
            //                A = Convert.ToInt32(filas["POSICIONCAMION"].ToString()) - 1;
            //                SQL = " UPDATE ZCARGA_LINEA SET POSICIONCAMION = " + A + ", ";
            //                SQL += " ESTADO = 1 ";
            //                SQL += " WHERE ID = " + filas["ID"].ToString() + " ";
            //                SQL += " AND ID_CABECERA = " + filas["ID_CABECERA"].ToString() + " ";
            //                SQL += " AND ZID = " + filas["ZID"].ToString() + " ";
            //                SQL += " AND NUMERO_LINEA = " + filas["NUMERO_LINEA"].ToString() + " ";
            //                DBHelper.ExecuteNonQuery(SQL);
            //            }
            //        }
            //    }
            //}


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

            //Modifica.Visible = false;
            //Asume.Visible = false;
            //cuestion.Visible = false;
            //Decide.Visible = false;
            //DvPreparado.Visible = false;
            //if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            //{
            //    SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            //}
            //else
            //{
            //    SQL = "UPDATE ZCARGA_CABECERA SET ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            //}
            //DBHelper.ExecuteNonQuery(SQL);

            //Carga_tablaLista();

            //gvLista.EditIndex = -1;
            //gvLista.DataBind();

        }

        protected void checkNoMlC_Click(object sender, EventArgs e)
        {
            ////DvPreparado.Visible = false;
            ////cuestion.Visible = false;
            ////Asume.Visible = false;
            ////Modifica.Visible = false;
            ////Decide.Visible = false;
            //string SQL = "";
            //if (this.Session["EstadoCabecera"].ToString() == "2")//Confirmado
            //{
            //    SQL = "UPDATE ZCARGA_CABECERA SET ESTADO = 4, ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' WHERE NUMERO = " + TxtNumero.Text;
            //}
            //else
            //{
            //    SQL = "UPDATE ZCARGA_CABECERA set  ZSYSDATE = '" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "' ";
            //    SQL += " WHERE NUMERO = " + TxtNumero.Text;
            //}
            //DBHelper.ExecuteNonQuery(SQL);

            //this.Session["ElIDaBorrar"] = "";

            ////Carga_tablaCabecera();
            ////Carga_tablaLista();

            ////gvLista.EditIndex = -1;
            ////gvLista.DataBind();
        }

        //protected void checkNo_Click(object sender, EventArgs e)
        //{
        //    //DvPreparado.Visible = false;
        //    //cuestion.Visible = false;
        //    //Asume.Visible = false;
        //    //Modifica.Visible = false;
        //    //Decide.Visible = false;
        //    this.Session["ElIDaBorrar"] = "";
        //}

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

            //cuestion.Visible = true;
            //Asume.Visible = false;
            //Modifica.Visible = false;
            //Decide.Visible = false;
            //DvPreparado.Visible = true;

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
            //string Temporal = ""; //Jose
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
                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
                    SQL += " FROM ZPEDIDOS_COMPRA  ";
                    SQL += " WHERE ID is not null  ";
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
                    SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
                    SQL += " FROM ZPEDIDOS_COMPRA  ";
                    SQL += " WHERE ID is not null  ";
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
                LbRowControl1.Text = "Registros: " + dt.Rows.Count;

                //gvControl.DataSource = dt;
                //gvControl.DataBind();

                //busca Error donde no se puede depurar
                //Lberror.Text = "";

            }
            catch (Exception ex)
            {
                string b = ex.Message;
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

            DrConsultas2.Items.Clear();
            DrConsultas2.DataValueField = "EMPRESA";
            DrConsultas2.DataTextField = "EMPRESA";
            DrConsultas2.Items.Insert(0, new ListItem("Seleccione uno", ""));

            SQL = "SELECT DISTINCT(EMPRESA) FROM ZPEDIDOS_COMPRA ORDER BY EMPRESA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrConsultas.DataSource = dt;
            DrConsultas.DataBind();
            DrConsultas.SelectedIndex = -1;
            DrConsultas2.DataSource = dt;
            DrConsultas2.DataBind();
            DrConsultas2.SelectedIndex = -1;

            DrProveedor.Items.Clear();
            DrProveedor.DataValueField = "NOMBREFISCAL";
            DrProveedor.DataTextField = "NOMBREFISCAL";
            DrProveedor.Items.Insert(0, new ListItem("Seleccione uno", ""));

            DrProveedor2.Items.Clear();
            DrProveedor2.DataValueField = "NOMBREFISCAL";
            DrProveedor2.DataTextField = "NOMBREFISCAL";
            DrProveedor2.Items.Insert(0, new ListItem("Seleccione uno", ""));

            SQL = "SELECT DISTINCT(NOMBREFISCAL) FROM ZPEDIDOS_COMPRA ORDER BY NOMBREFISCAL ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrProveedor.DataSource = dt;
            DrProveedor.DataBind();
            DrProveedor.SelectedIndex = -1;
            DrProveedor2.DataSource = dt;
            DrProveedor2.DataBind();
            DrProveedor2.SelectedIndex = -1;

            DrNumPedido.Items.Clear();
            DrNumPedido.DataValueField = "NUMERO";
            DrNumPedido.DataTextField = "NUMERO";
            DrNumPedido.Items.Insert(0, new ListItem("Seleccione uno", ""));

            DrNumPedido2.Items.Clear();
            DrNumPedido2.DataValueField = "NUMERO";
            DrNumPedido2.DataTextField = "NUMERO";
            DrNumPedido2.Items.Insert(0, new ListItem("Seleccione uno", ""));

            SQL = "SELECT DISTINCT(NUMERO) FROM ZPEDIDOS_COMPRA ORDER BY NUMERO ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrNumPedido.DataSource = dt;
            DrNumPedido.DataBind();
            DrNumPedido.SelectedIndex = -1;
            DrNumPedido2.DataSource = dt;
            DrNumPedido2.DataBind();
            DrNumPedido2.SelectedIndex = -1;

            DrProducto.Items.Clear();
            DrProducto.DataValueField = "PRODUCTO";
            DrProducto.DataTextField = "PRODUCTO";
            DrProducto.Items.Insert(0, new ListItem("Seleccione uno", ""));

            DrProducto2.Items.Clear();
            DrProducto2.DataValueField = "PRODUCTO";
            DrProducto2.DataTextField = "PRODUCTO";
            DrProducto2.Items.Insert(0, new ListItem("Seleccione uno", ""));

            SQL = "SELECT DISTINCT(PRODUCTO) FROM ZPEDIDOS_COMPRA ORDER BY PRODUCTO ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrProducto.DataSource = dt;
            DrProducto.DataBind();
            DrProducto.SelectedIndex = -1;
            DrProducto2.DataSource = dt;
            DrProducto2.DataBind();
            DrProducto2.SelectedIndex = -1;

            DrFechaEntrega.Items.Clear();
            DrFechaEntrega.DataValueField = "FECHAENTREGA"; // "CLIENTEPROVEEDOR";
            DrFechaEntrega.DataTextField = "FECHAENTREGA"; //"CLIENTEPROVEEDOR";
            DrFechaEntrega.Items.Insert(0, new ListItem("Seleccione uno", ""));

            DrFechaEntrega2.Items.Clear();
            DrFechaEntrega2.DataValueField = "FECHAENTREGA"; // "CLIENTEPROVEEDOR";
            DrFechaEntrega2.DataTextField = "FECHAENTREGA"; //"CLIENTEPROVEEDOR";
            DrFechaEntrega2.Items.Insert(0, new ListItem("Seleccione uno", ""));

            SQL = "SELECT DISTINCT(FECHAENTREGA) FROM ZPEDIDOS_COMPRA ORDER BY FECHAENTREGA ";
            dt = Main.BuscaLote(SQL).Tables[0];
            DrFechaEntrega.DataSource = dt;
            DrFechaEntrega.DataBind();
            DrFechaEntrega.SelectedIndex = -1;
            DrFechaEntrega2.DataSource = dt;
            DrFechaEntrega2.DataBind();
            DrFechaEntrega2.SelectedIndex = -1;
        }

        private void Carga_tablaListaFiltro()
        {

            string SQL = ""; // "SELECT * FROM ZCARGA_ORDEN ";
            string Filtros = "";
            this.Session["Filtro80"] = "";
            this.Session["FiltroEmpresa"] = "";
            this.Session["FiltroFechaEntrega"] = "";
            this.Session["FiltroProducto"] = "";
            this.Session["FiltroProveedor"] = "";
            this.Session["FiltroNumPedido"] = "";
            this.Session["Filtro"] ="";
            //this.Session["FiltroEmpresa"] = "";
            //this.Session["FiltroFecha"] = "";
            //this.Session["FiltroCliente"] = "";
            //this.Session["FiltroProveedor"] = "";
            DrConsultas.Attributes.Add("style", "background-color:#f5f5f5");
            DrConsultas2.Attributes.Add("style", "background-color:#f5f5f5");
            DrProveedor.Attributes.Add("style", "background-color:#f5f5f5");
            DrProveedor2.Attributes.Add("style", "background-color:#f5f5f5");
            DrFechaEntrega.Attributes.Add("style", "background-color:#f5f5f5");
            DrFechaEntrega2.Attributes.Add("style", "background-color:#f5f5f5");
            DrProducto.Attributes.Add("style", "background-color:#f5f5f5");
            DrProducto2.Attributes.Add("style", "background-color:#f5f5f5");
            DrNumPedido.Attributes.Add("style", "background-color:#f5f5f5");
            DrNumPedido2.Attributes.Add("style", "background-color:#f5f5f5");

            //DrSelectFiltro.Items.Clear();
            try
            {
                if (DrConsultas.SelectedItem.Value != "")
                {
                    this.Session["FiltroEmpresa"] = DrConsultas.SelectedItem.Value;
                    this.Session["Filtro"] += " AND EMPRESA =  '" + DrConsultas.SelectedItem.Value + "' ";
                    //DrSelectFiltro.Items.Add("Empresa: " + DrConsultas.SelectedItem.Value);
                    DrConsultas.Attributes.Add("style", "background-color:#e6f2e1");
                    DrConsultas2.Attributes.Add("style", "background-color:#e6f2e1");
                }
                if (DrProveedor.SelectedItem.Value != "" )
                {
                    Filtros += " AND NOMBREFISCAL =  '" + DrProveedor.SelectedItem.Value + "' ";
                    this.Session["Filtro80"] += " AND C.[Nombre Fiscal]  = '" + DrProveedor.SelectedItem.Value + "' ";
                    //DrSelectFiltro.Items.Add("Proveedor: " + DrProveedor.SelectedItem.Value + ", Fecha Hasta: " + DrNumPedido.SelectedItem.Value);
                    DrProveedor.Attributes.Add("style", "background-color:#e6f2e1");
                    DrProveedor2.Attributes.Add("style", "background-color:#e6f2e1");
                }
                if (DrFechaEntrega.SelectedItem.Value != "")
                {
                    Filtros += " AND FECHAENTREGA = '" + DrFechaEntrega.SelectedItem.Value + "' ";
                    this.Session["Filtro80"] += " AND C.[Fecha Entrega] = '" + DrFechaEntrega.SelectedItem.Value + "'";
                    //DrSelectFiltro.Items.Add("Fecha Entrega: " + DrFechaEntrega.SelectedItem.Value);
                    DrFechaEntrega.Attributes.Add("style", "background-color:#e6f2e1");
                    DrFechaEntrega2.Attributes.Add("style", "background-color:#e6f2e1");
                }
                if (DrProducto.SelectedItem.Value != "")
                {
                    Filtros += " AND PRODUCTO = '" + DrProducto.SelectedItem.Value + "'";
                    this.Session["Filtro80"] += " AND L.Producto = '" + DrProducto.SelectedItem.Value + "'";
                    //DrSelectFiltro.Items.Add("Producto: " + DrProducto.SelectedItem.Value);
                    DrProducto.Attributes.Add("style", "background-color:#e6f2e1");
                    DrProducto2.Attributes.Add("style", "background-color:#e6f2e1");
                }
                if (DrNumPedido.SelectedItem.Value != "")
                {
                    Filtros += " AND NUMERO = '" + DrNumPedido.SelectedItem.Value + "'";
                    this.Session["Filtro80"] += " AND C.Numero = '" + DrNumPedido.SelectedItem.Value + "'";
                    //DrSelectFiltro.Items.Add("Nº Pedido: " + DrNumPedido.SelectedItem.Value);
                    DrNumPedido.Attributes.Add("style", "background-color:#e6f2e1");
                    DrNumPedido2.Attributes.Add("style", "background-color:#e6f2e1");
                }
                if (Filtros != "")
                {
                    this.Session["Filtro"] = SQL + Filtros;
                    //PanelgeneralFiltro.Attributes.Add("style", "background-color:#e6f2e1");
                }
                //else
                //{
                //    this.Session["Filtro"] = "";
                //    if (this.Session["FiltroEmpresa"].ToString() != "")
                //    {
                //        PanelOrden.Attributes.Add("style", "background-color:#e6f2e1");
                //    }
                //    else
                //    {
                //        PanelOrden.Attributes.Add("style", "background-color:#f5f5f5");
                //    }
                //}
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
            //Lberror.Text = "";
            ////Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }

        protected void MuevePalet_Click(object sender, EventArgs e)
        {
            //Button nombre = (Button)sender;
            //string Id = nombre.ID;
            //string Parent = nombre.Parent.ID.ToString();
            //string Miro = LbPosicionCamion.Text;
            //HtmlGenericControl DivText = (HtmlGenericControl)FindControl(Parent);
            //Parent = DivText.Parent.ID;

            //string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            //SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
            //SQL += " WHERE ID_CABECERA = " + TxtNumero.Text;
            //SQL += " AND POSICIONCAMION = " + Id.Replace("Btndrag", "");
            ////Lberror.Text += SQL + "1- gvCabecera_Selecciona " + Variables.mensajeserver;
            //DataTable dt = Main.BuscaLote(SQL).Tables[0];
            ////Lberror.Text += " 1- gvCabecera_Selecciona " + Variables.mensajeserver;
            ////gvLista.DataSource = dt;
            ////gvLista.DataBind();
            //this.Session["NumeroPalet"] = dt.Rows.Count.ToString();

            //Vista_Print(dt, Id.Replace("Btndrag", ""));
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

            //string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
            //SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
            //SQL += " WHERE ID_CABECERA = " + TxtNumero.Text + " ";
            //if (ID != "")
            //{
            //    SQL += " AND NUMERO_LINEA = " + ID + " ";
            //}
            //SQL += " ORDER BY POSICIONCAMION ";
            //DataTable dt = Main.BuscaLote(SQL).Tables[0];
            //int i = 0;
            //string EtiquetaQR = "";
            //string EtiquetaUD_BASE = "";




            //foreach (DataRow filas in dt.Rows)
            //{
            //    if (i == 0)
            //    {
            //        //divArray0
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
            //        DlbCliente0.Text = "Cliente: " + filas["NOMBREFISCAL"].ToString(); // filas["CLIENTEPROVEEDOR"].ToString();
            //        string N = "";
            //        SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is System.DBNull)
            //        {
            //            DLbVariedad0.Text = "Variedad: ";
            //        }
            //        else
            //        {
            //            DLbVariedad0.Text = "Variedad: " + Con;
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


            //        SQL = "SELECT TOP 1 (A.ZVARIEDAD) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is null)
            //        {
            //            EtiquetaQR = "NO EXISTE";
            //        }
            //        else
            //        {
            //            EtiquetaQR = Con.ToString();
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
            //            DLbEmpresa0.Text = "";
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
            //        //Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
            //        //DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
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
            //            Double Value = Convert.ToDouble(CadaLinea[0].ToString() + "." + CadaLinea[1].ToString());
            //            DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        }
            //        else
            //        {
            //            Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString());
            //            DLbNumerPlanta0.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        }
            //        if (EtiquetaUD_BASE != "")
            //        {
            //            string[] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
            //            int Value = Convert.ToInt32(AA[0]);
            //            DLbNumerPlanta0.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
            //        }
            //        Update_listaHasta(DLbLote0.Text + Environment.NewLine, filas["ID"].ToString(), filas["NUMERO_LINEA"].ToString());
            //        btnGenerate_Click(DLbLote0, PlaceHolderGR0);
            //        ZsingGenerateTodo_Click(EtiquetaQR, PlaceHolderMIN0);
            //    }
            //    else
            //    {
            //        string MiDiv = "divArray" + i;
            //        System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //        createDiv.ID = MiDiv;
            //        createDiv.Attributes["class"] = "panel panel-default";
            //        createDiv.Attributes["style"] = "display:inline-block; border-style:none; width:100%; font-weight: bold; font-size:20px;";
            //        createDiv.Controls.Add(new LiteralControl("<br/>"));


            //        MiDiv = "divLabel" + i;
            //        System.Web.UI.HtmlControls.HtmlGenericControl divLabel = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //        divLabel.ID = MiDiv;
            //        divLabel.Attributes["class"] = "col-lg-12";

            //        MiDiv = "divMIN" + i;
            //        System.Web.UI.HtmlControls.HtmlGenericControl divMIN = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
            //        divMIN.ID = MiDiv;
            //        divMIN.Attributes["class"] = "col-lg-12";

            //        PlaceHolder QR = new PlaceHolder();
            //        QR.ID = "PlaceHolderGR" + i;

            //        PlaceHolder QRMin = new PlaceHolder();
            //        QRMin.ID = "PlaceHolderMIN" + i;

            //        Label lbEmpresa = new Label();
            //        lbEmpresa.ID = "DLbEmpresa" + i;
            //        lbEmpresa.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbLote = new Label();
            //        lbbLote.ID = "DLbLote" + i;
            //        lbbLote.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbOrden = new Label();
            //        lbbOrden.ID = "DLbOrdenCarga" + i;
            //        lbbOrden.Attributes["style"] = " display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbPosCamion = new Label();
            //        lbbPosCamion.ID = "DLbPosCamio" + i;
            //        lbbPosCamion.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbCliente = new Label();
            //        lbbCliente.ID = "DlbCliente" + i;
            //        lbbCliente.Attributes["style"] = "display:inline-block; width:40%; font-weight: bold; font-size:20px;";

            //        Label lbbVariedad = new Label();
            //        lbbVariedad.ID = "DLbVariedad" + i;
            //        lbbVariedad.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbEmpresa = new Label();
            //        lbbEmpresa.ID = "DLbEmpresa" + i;
            //        lbbEmpresa.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        Label lbbNumerPlanta = new Label();
            //        lbbNumerPlanta.ID = "DLbNumerPlanta" + i;
            //        lbbNumerPlanta.Attributes["style"] = "display:inline-block; width:100%; font-weight: bold; font-size:20px;";

            //        //Bug Codigo QR si termina en cero hay que añadir un retorno de carro o no se lee
            //        if (filas["POSICIONCAMION"].ToString().Contains("0"))
            //        {
            //            lbbLote.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString() + Environment.NewLine;
            //        }
            //        else
            //        {
            //            lbbLote.Text = filas["ID_CABECERA"].ToString() + "|" + filas["CLIENTEPROVEEDOR"].ToString() + "|" + filas["NUMERO"].ToString() + "|" + filas["LINEA"].ToString() + "|" + filas["POSICIONCAMION"].ToString();
            //        }
            //        lbbOrden.Text = "Orden Carga: " + filas["ID_CABECERA"].ToString();
            //        lbbPosCamion.Text = "Posición Camión: " + filas["POSICIONCAMION"].ToString();
            //        lbbCliente.Text = "Cliente: " + filas["NOMBREFISCAL"].ToString(); // filas["CLIENTEPROVEEDOR"].ToString();

            //        string N = "";
            //        SQL = "SELECT TOP 1 (A.ZDESCRIPCION) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is null)
            //        {
            //            lbbVariedad.Text = "Variedad: NO EXISTE";
            //        }
            //        else
            //        {
            //            lbbVariedad.Text = "Variedad: " + Con.ToString();
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


            //        SQL = "SELECT TOP 1 (A.ZVARIEDAD) ";
            //        SQL += " FROM ZEMPRESAVARIEDAD A ";
            //        SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN B ON B.ZVARIEDAD = A.ZVARIEDAD ";
            //        SQL += " INNER JOIN ZCARGA_LINEA C ON C.ARTICULO = B.ZCODGOLDEN ";
            //        SQL += " WHERE B.ZCODGOLDEN = '" + filas["ARTICULO"].ToString() + "'"; // '3-FRT-54'"
            //        Con = DBHelper.ExecuteScalarSQL(SQL, null);
            //        if (Con is null)
            //        {
            //            EtiquetaQR = "NO EXISTE";
            //        }
            //        else
            //        {
            //            EtiquetaQR = Con.ToString();
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
            //            lbbEmpresa.Text = "NO EXISTE";
            //        }
            //        else
            //        {
            //            if (Con.ToString().Contains("VIVA"))
            //            {

            //                lbbEmpresa.Text = "Viveros Valsaín, SLU";
            //                this.Session["Centro"] = "VIVA";
            //            }
            //            else
            //            {
            //                lbbEmpresa.Text = "Viveros Río Eresma, SLU";
            //                this.Session["Centro"] = "VRE";
            //            }
            //        }

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
            //            miro = CadaLinea[0] + "." + CadaLinea[1];

            //            Double Value = Convert.ToDouble(miro);
            //            lbbNumerPlanta.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        }
            //        else
            //        {
            //            Double Value = Convert.ToDouble(filas["UDSENCARGA"].ToString()) * 1000;
            //            lbbNumerPlanta.Text = "Número Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
            //        }

            //        if (EtiquetaUD_BASE != "")
            //        {
            //            string[] AA = System.Text.RegularExpressions.Regex.Split(filas["UDSENCARGA"].ToString().Replace(".", ","), ",");
            //            int Value = Convert.ToInt32(AA[0]);
            //            lbbNumerPlanta.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + EtiquetaUD_BASE;
            //        }

            //        Update_listaHasta(lbbLote.Text + Environment.NewLine, filas["ID"].ToString(), filas["NUMERO_LINEA"].ToString());

            //        btnGenerate_Click(lbbLote, QR);
            //        ZsingGenerateTodo_Click(EtiquetaQR, QRMin);
            //        //divArray0
            //        createDiv.Controls.Add(lbbEmpresa);
            //        createDiv.Controls.Add(lbbLote);

            //        //QR Placeholder
            //        createDiv.Controls.Add(QR);

            //        //divLabel0
            //        divLabel.Controls.Add(lbbOrden);
            //        divLabel.Controls.Add(lbbPosCamion);
            //        divLabel.Controls.Add(lbbCliente);
            //        divLabel.Controls.Add(lbbVariedad);
            //        divLabel.Controls.Add(lbbNumerPlanta);

            //        //divMIN0
            //        divMIN.Controls.Add(QRMin);

            //        //divArray0
            //        createDiv.Controls.Add(divLabel);
            //        createDiv.Controls.Add(divMIN);

            //        //Al contenedor
            //        pnlContents2.Controls.Add(createDiv);
            //    }
            //    i += 1;
            //}
        }

        private void Update_listaHasta(string Dato, string ID, string Numero_linea)
        {
            string SQL = " UPDATE ZCARGA_LINEA ";
            SQL += " SET HASTA ='" + Dato + "'";
            SQL += " WHERE ID = " + ID;
            SQL += " AND NUMERO_LINEA = " + Numero_linea;

            DBHelper.ExecuteNonQuery(SQL);
        }

        protected void btnGenerateCompra_Click(Object Objeto, Object Container)
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
                string b = a.Message;
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

        protected void ZsingGenerateTodo_Click(string etique, Object Container)
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
            //btnPrintB1.Visible = false;
            //btnPrintC1.Visible = false;
            //btnPrintA2.Visible = false;
            //btnPrintB2.Visible = false;
            //btnPrintC2.Visible = false;
            //btnPrintA3.Visible = false;
            //btnPrintB3.Visible = false;
            //btnPrintC3.Visible = false;
            //btnPrintA4.Visible = false;
            //btnPrintB4.Visible = false;
            //btnPrintC4.Visible = false;

            //if (H1Normal.Visible == true)
            //{
            //    if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            //    {
            //        btnPrintA1.Visible = true;
            //        btnPrintB1.Visible = false;
            //        btnPrintC1.Visible = false;
            //    }
            //    else if (DrPrinters.SelectedItem.Value == "2")
            //    {
            //        btnPrintB1.Visible = true;
            //        btnPrintA1.Visible = false;
            //        btnPrintC1.Visible = false;
            //    }
            //    else if (DrPrinters.SelectedItem.Value == "6")
            //    {
            //        btnPrintD1.Visible = true;
            //        btnPrintB1.Visible = false;
            //        btnPrintA1.Visible = false;
            //        btnPrintC1.Visible = false;
            //    }
            //    else
            //    {
            //        btnPrintB1.Visible = false;
            //        btnPrintA1.Visible = false;
            //        btnPrintC1.Visible = true;
            //    }
            //}

            //if (H1Seleccion.Visible == true)
            //{
            //    if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            //    {
            //        btnPrintA2.Visible = true;
            //        btnPrintB2.Visible = false;
            //        btnPrintC2.Visible = false;
            //    }
            //    else if (DrPrinters.SelectedItem.Value == "2")
            //    {
            //        btnPrintB2.Visible = true;
            //        btnPrintA2.Visible = false;
            //        btnPrintC2.Visible = false;
            //    }
            //    else
            //    {
            //        btnPrintB2.Visible = false;
            //        btnPrintA2.Visible = false;
            //        btnPrintC2.Visible = true;
            //    }
            //}
            //if (H1Red.Visible == true)
            //{
            //    if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            //    {
            //        btnPrintA3.Visible = true;
            //        btnPrintB3.Visible = false;
            //        btnPrintC3.Visible = false;
            //    }
            //    else if (DrPrinters.SelectedItem.Value == "2")
            //    {
            //        btnPrintB3.Visible = true;
            //        btnPrintA3.Visible = false;
            //        btnPrintC3.Visible = false;
            //    }
            //    else
            //    {
            //        btnPrintB3.Visible = false;
            //        btnPrintA3.Visible = false;
            //        btnPrintC3.Visible = true;
            //    }
            //}
            //if (H1Green.Visible == true)
            //{
            //    if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            //    {
            //        btnPrintA4.Visible = true;
            //        btnPrintB4.Visible = false;
            //        btnPrintC4.Visible = false;
            //    }
            //    else if (DrPrinters.SelectedItem.Value == "2")
            //    {
            //        btnPrintB4.Visible = true;
            //        btnPrintA4.Visible = false;
            //        btnPrintC4.Visible = false;
            //    }
            //    else
            //    {
            //        btnPrintB4.Visible = false;
            //        btnPrintA4.Visible = false;
            //        btnPrintC4.Visible = true;
            //    }
            //}

            //Segunda
            btnPrintA1.Visible = false;
            btnPrintB1.Visible = false;
            btnPrintC1.Visible = false;
            btnPrintD1.Visible = false;
            btnPrintA2.Visible = false;
            btnPrintB2.Visible = false;
            btnPrintC2.Visible = false;
            btnPrintD2.Visible = false;
            btnPrintA3.Visible = false;
            btnPrintB3.Visible = false;
            btnPrintC3.Visible = false;
            btnPrintD3.Visible = false;
            btnPrintA4.Visible = false;
            btnPrintB4.Visible = false;
            btnPrintC4.Visible = false;
            btnPrintD4.Visible = false;


            if (H1Normal.Visible == true)
            {
                //H1Red.Visible = false;
                //H1Green.Visible = false;
                ////H1Normal.Visible = false;
                //H1Seleccion.Visible = false;
                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    btnPrintA1.Visible = true;
                    btnPrintB1.Visible = false;
                    btnPrintC1.Visible = false;
                    btnPrintD1.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB1.Visible = true;
                    btnPrintA1.Visible = false;
                    btnPrintC1.Visible = false;
                    btnPrintD1.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    btnPrintD1.Visible = true;
                    btnPrintB1.Visible = false;
                    btnPrintA1.Visible = false;
                    btnPrintC1.Visible = false;
                }
                else
                {
                    btnPrintB1.Visible = false;
                    btnPrintA1.Visible = false;
                    btnPrintC1.Visible = true;
                    btnPrintD1.Visible = false;
                }

            }

            if (H1Seleccion.Visible == true)
            {
                //H1Red.Visible = false;
                //H1Green.Visible = false;
                //H1Normal.Visible = false;
                ////H1Seleccion.Visible = false;

                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    btnPrintA2.Visible = true;
                    btnPrintB2.Visible = false;
                    btnPrintC2.Visible = false;
                    btnPrintD2.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB2.Visible = true;
                    btnPrintA2.Visible = false;
                    btnPrintC2.Visible = false;
                    btnPrintD2.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    btnPrintB2.Visible = false;
                    btnPrintA2.Visible = false;
                    btnPrintC2.Visible = false;
                    btnPrintD2.Visible = true;
                }
                else
                {
                    btnPrintB2.Visible = false;
                    btnPrintA2.Visible = false;
                    btnPrintC2.Visible = true;
                    btnPrintD2.Visible = false;
                }
            }
            if (H1Red.Visible == true)
            {
                //H1Red.Visible = false;
                //H1Green.Visible = false;
                //H1Normal.Visible = false;
                //H1Seleccion.Visible = false;
                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    btnPrintA3.Visible = true;
                    btnPrintB3.Visible = false;
                    btnPrintC3.Visible = false;
                    btnPrintD3.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB3.Visible = true;
                    btnPrintA3.Visible = false;
                    btnPrintC3.Visible = false;
                    btnPrintD3.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    btnPrintB3.Visible = false;
                    btnPrintA3.Visible = false;
                    btnPrintC3.Visible = false;
                    btnPrintD3.Visible = true;
                }
                else
                {
                    btnPrintB3.Visible = false;
                    btnPrintA3.Visible = false;
                    btnPrintC3.Visible = true;
                    btnPrintD3.Visible = false;
                }
            }
            if (H1Green.Visible == true)
            {
                //H1Red.Visible = false;
                ////H1Green.Visible = false;
                //H1Normal.Visible = false;
                //H1Seleccion.Visible = false;
                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    btnPrintA4.Visible = true;
                    btnPrintB4.Visible = false;
                    btnPrintC4.Visible = false;
                    btnPrintD4.Visible = false;

                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB4.Visible = true;
                    btnPrintA4.Visible = false;
                    btnPrintC4.Visible = false;
                    btnPrintD4.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    btnPrintB4.Visible = false;
                    btnPrintA4.Visible = false;
                    btnPrintC4.Visible = false;
                    btnPrintD4.Visible = true;
                }
                else
                {
                    btnPrintB4.Visible = false;
                    btnPrintA4.Visible = false;
                    btnPrintC4.Visible = true;
                    btnPrintD4.Visible = false;
                }
            }


            //Segunda
            //btnPrintA1.Visible = false;
            //btnPrintB1.Visible = false;
            //btnPrintC1.Visible = false;
            //btnPrintD1.Visible = false;
            //btnPrintA2.Visible = false;
            //btnPrintB2.Visible = false;
            //btnPrintC2.Visible = false;
            //btnPrintD2.Visible = false;
            //btnPrintA3.Visible = false;
            //btnPrintB3.Visible = false;
            //btnPrintC3.Visible = false;
            //btnPrintD3.Visible = false;
            //btnPrintA4.Visible = false;
            //btnPrintB4.Visible = false;
            //btnPrintC4.Visible = false;
            //btnPrintD4.Visible = false;


            //if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            //{
            //    panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintPanel();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
            //    //btnPrintA1.Attributes.Add("onclick", "return PrintPanel();");
            //}
            //else if (DrPrinters.SelectedItem.Value == "2")
            //{
            //    panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintQR();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
            //    //btnPrintA1.Attributes.Add("onclick", "return PrintQR();");
            //}
            //else if (DrPrinters.SelectedItem.Value == "6")
            //{
            //    panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintFT();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
            //    //btnPrintA1.Attributes.Add("onclick", "return PrintFT();");
            //}
            //else
            //{
            //    panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintPaletAlv();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
            //    //btnPrintA1.Attributes.Add("onclick", "return PrintPaletAlv();");
            //}

            //Carga_Lotes(this.Session["IDSecuencia"].ToString());
            //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        }

        //private void Printers(string ID)
        //{
        //    if (ID == "1" || ID == "4")
        //    {
        //        pnlContents.Visible = true;
        //        panelContentsQROrdenCompra.Visible = false;
        //        panelContentsFTOrdenCompra.Visible = false;
        //        panelContentsPaletAlv.Visible = false;
        //    }
        //    else if (ID == "2")
        //    {
        //        pnlContents.Visible = false;
        //        panelContentsQROrdenCompra.Visible = true;
        //        panelContentsFTOrdenCompra.Visible = false;
        //        panelContentsPaletAlv.Visible = false;
        //    }
        //    else if (ID == "3")
        //    {
        //        pnlContents.Visible = false;
        //        panelContentsQROrdenCompra.Visible = false;
        //        panelContentsFTOrdenCompra.Visible = true;
        //        panelContentsPaletAlv.Visible = false;
        //    }
        //    else if (ID == "6")
        //    {
        //        pnlContents.Visible = false;
        //        panelContentsQROrdenCompra.Visible = false;
        //        panelContentsFTOrdenCompra.Visible = false;
        //        panelContentsPaletAlv.Visible = true;
        //    }
        //}

        private void Carga_tablaNueva()
        {
            string temporal = ""; //Jose
            ////Lberror.Text = "";
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
                Filtros = this.Session["Filtro80"].ToString();
                //
                if (Filtros == "")
                {
                    if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                    {
                        //SQL += "DELETE FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        SQL = "1- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM ZPEDIDOS_COMPRA ";
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
                            SQL += "UDSSERVIDAS, UDSPENDIENTES, FECHAENTREGA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA )";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["PRODUCTO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",'";
                            SQL += fila["FECHAENTREGA"].ToString() + "','" + fila["DESCRIPCION"].ToString() + "','";
                            SQL += fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 A.ZTIPO_PLANTA  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),(";
                            SQL += " SELECT TOP 1 A.ZVARIEDAD  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),";
                            SQL += " '" + fila["FAMILIA"].ToString().Trim() + "')";
                            //Lberror.Text += "3- Carga_tablaNueva " + SQL + Environment.NewLine;
                            string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                            temporal = "4";
                            DBHelper.ExecuteNonQuery(SQL);
                        }



                        SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
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

                            SQL = "5- Carga_tablaNueva DeleteProcedureTemp  DELETE FROM ZPEDIDOS_COMPRA ";
                            temporal = "6";
                            DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");

                            SQL = Main.BuscaVentasGold("", "");
                            temporal = "7";
                            //Lberror.Text = "6- Carga_tablaNueva BuscaLoteGold" + Environment.NewLine;
                            dtV = Main.BuscaLoteGold(SQL).Tables[0];
                            temporal = "8";

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA,  PRODUCTO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSPENDIENTES, FECHAENTREGA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA )";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["PRODUCTO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",'";
                                SQL += fila["FECHAENTREGA"].ToString() + "','" + fila["DESCRIPCION"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 A.ZTIPO_PLANTA  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),(";
                                SQL += " SELECT TOP 1 A.ZVARIEDAD  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),";
                                SQL += " '" + fila["FAMILIA"].ToString().Trim() + "')";
                                //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                                string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'  ORDER BY ID ";
                        }
                        temporal = "9";
                        SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
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

                        //string Miro = "";
                        //if (this.Session["FiltroFechaEntrega"].ToString() != "")
                        //{
                        //    Miro += " AND " + this.Session["FiltroFechaEntrega"].ToString();
                        //}
                        //if (this.Session["FiltroProveedor"].ToString() != "")
                        //{
                        //    Miro += " AND " + this.Session["FiltroProveedor"].ToString();
                        //}
                        //if (this.Session["FiltroProducto"].ToString() != "")
                        //{
                        //    Miro += " AND " + this.Session["FiltroProducto"].ToString();
                        //}
                        //if (this.Session["FiltroEmpresa"].ToString() != "")
                        //{
                        //    temporal = "11";
                        //    SQL = Main.BuscaVentasGold(this.Session["FiltroEmpresa"].ToString(), Miro);
                        //}
                        //else
                        //{
                        //    temporal = "12";
                        //    SQL = Main.BuscaVentasGold("", Miro);
                        //}
                        SQL = Main.BuscaVentasGold("", Filtros);

                        temporal = "13";
                        dtV = Main.BuscaLoteGold(SQL).Tables[0];
                        temporal = "14";
                        foreach (DataRow fila in dtV.Rows)
                        {
                            SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA,  PRODUCTO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSPENDIENTES, FECHAENTREGA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA )";
                            SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                            SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["PRODUCTO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                            SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",'";
                            SQL += fila["FECHAENTREGA"].ToString() + "','" + fila["DESCRIPCION"].ToString() + "','";
                            SQL += fila["SERIE_PED"].ToString() + "',(";
                            SQL += " SELECT TOP 1 A.ZTIPO_PLANTA  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),(";
                            SQL += " SELECT TOP 1 A.ZVARIEDAD  ";
                            SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                            SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),";
                            SQL += " '" + fila["FAMILIA"].ToString().Trim() + "')";
                            //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                            string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                            DBHelper.ExecuteNonQuery(SQL);
                        }

                        temporal = "15";
                        SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                        SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
                        SQL += " FROM ZPEDIDOS_COMPRA  ";
                        dt = Main.BuscaLote(SQL).Tables[0];

                    }
                    else
                    {

                        //SQL += "DELETE FROM [RIOERESMA].[dbo].ZCARGA_ORDEN WHERE ESTADO in ( 0, 1) AND COMPUTER = '" + this.Session["ComputerName"].ToString() + "'";
                        if (temporal == "")
                        {
                            DBHelper.DeleteProcedureTemp("ZPEDIDOS_COMPRA");

                            //string Miro = "";
                            //if (this.Session["FiltroFechaEntrega"].ToString() != "")
                            //{
                            //    Miro += " AND " + this.Session["FiltroFechaEntrega"].ToString();
                            //}
                            //if (this.Session["FiltroProveedor"].ToString() != "")
                            //{
                            //    Miro += " AND " + this.Session["FiltroProveedor"].ToString();
                            //}
                            //if (this.Session["FiltroProducto"].ToString() != "")
                            //{
                            //    Miro += " AND " + this.Session["FiltroProducto"].ToString();
                            //}

                            //if (this.Session["FiltroEmpresa"].ToString() != "")
                            //{
                            //    temporal = "17";
                            //    SQL = Main.BuscaVentasGold(this.Session["FiltroEmpresa"].ToString(), Miro);

                            //}
                            //else
                            //{
                            //    temporal = "18";
                            //    SQL = Main.BuscaVentasGold("", Miro);
                            //}
                            SQL = Main.BuscaVentasGold("", Filtros);
                            temporal = "19";
                            dtV = Main.BuscaLoteGold(SQL).Tables[0];

                            foreach (DataRow fila in dtV.Rows)
                            {
                                SQL = "INSERT INTO ZPEDIDOS_COMPRA( EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA,  PRODUCTO, UDSPEDIDAS,";
                                SQL += "UDSSERVIDAS, UDSPENDIENTES, FECHAENTREGA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA )";
                                SQL += " VALUES('" + fila["EMPRESA"].ToString() + "','" + fila["CLIENTEPROVEEDOR"].ToString() + "','" + fila["NOMBREFISCAL"].ToString() + "',";
                                SQL += fila["NUMERO"].ToString() + "," + fila["LINEA"].ToString() + ",'" + fila["PRODUCTO"].ToString() + "'," + fila["UDSPEDIDAS"].ToString().Replace(",", ".") + ",";
                                SQL += fila["UDSSERVIDAS"].ToString().Replace(",", ".") + "," + fila["UDSPENDIENTES"].ToString().Replace(",", ".") + ",'";
                                SQL += fila["FECHAENTREGA"].ToString() + "','" + fila["DESCRIPCION"].ToString() + "','";
                                SQL += fila["SERIE_PED"].ToString() + "',(";
                                SQL += " SELECT TOP 1 A.ZTIPO_PLANTA  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),(";
                                SQL += " SELECT TOP 1 A.ZVARIEDAD  ";
                                SQL += " FROM ZPLANTA_TIPO_VARIEDAD_CODGOLDEN A ";
                                SQL += " WHERE A.ZCODGOLDEN  = '" + fila["PRODUCTO"].ToString().Trim() + "'),";
                                SQL += " '" + fila["FAMILIA"].ToString().Trim() + "')";
                                //Lberror.Text += "7- Carga_tablaNueva " + SQL + Environment.NewLine;
                                string a = Main.Ficherotraza("Datos ZPEDIDOS_COMPRA -->" + SQL);
                                DBHelper.ExecuteNonQuery(SQL);
                            }

                            SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
                            SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
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

                Carga_Filtros();
                //busca Error donde no se puede depurar
                //Lberror.Visible = true;

                //Lberror.Text = "";
            }
            catch (Exception mm)
            {
                this.Session["Procedimiento"] = "Carga_tablaNueva";
                Variables.Error += mm.Message;
                string a = Main.Ficherotraza("Error ZPEDIDOS_COMPRA --> Paso:" + temporal + " -->Error:" + mm.Message + " --> SQL:" + SQL);
                //Lberror.Visible = true;
                //Lberror.Text += ". Error: " + mm.Message;
            }


        }

        private static DataTable Calcula_OrdenesCarga(DataTable dt, string Estado, string Numero)
        {
            return null;
            //string SQL = "";
            //try
            //{
            //    if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
            //    {
            //        SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
            //        SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
            //        SQL += " ZCARGA_LINEA  WHERE ESTADO < 3  AND ID_CABECERA = " + Numero;
            //        SQL += "  ORDER BY NUMERO_LINEA ";
            //    }
            //    else
            //    {
            //        SQL = " SELECT ID, ID_CABECERA, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, NUMERO_LINEA, LINEA, ARTICULO, ";
            //        SQL += " UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, ESTADO, FECHAENTREGA, COMPUTER ";
            //        SQL += " FROM [RIOERESMA].[dbo].ZCARGA_LINEA  WHERE ESTADO < 3  AND ID_CABECERA = " + Numero;
            //        SQL += " ORDER BY NUMERO_LINEA ";
            //    }

            //    DataTable ds = Main.BuscaLote(SQL).Tables[0];

            //    dt.AcceptChanges();
            //    foreach (DataRow fila in dt.Rows)
            //    {
            //        Boolean Esta = false;
            //        decimal rUDSENCARGA = 0.0M;

            //        foreach (DataRow fila2 in ds.Rows)
            //        {
            //            if (fila["NUMERO"].ToString() == fila2["NUMERO"].ToString() && fila["LINEA"].ToString() == fila2["LINEA"].ToString() &&
            //                fila["EMPRESA"].ToString() == fila2["EMPRESA"].ToString() && fila["CLIENTEPROVEEDOR"].ToString() == fila2["CLIENTEPROVEEDOR"].ToString())
            //            {
            //                if (Estado != "3")
            //                {
            //                    Esta = true;
            //                    rUDSENCARGA += Convert.ToDecimal(fila2["UDSENCARGA"].ToString().Replace(".", ","));
            //                }
            //                else
            //                {
            //                    fila["UDSENCARGA"] = "0,00";
            //                }
            //            }
            //        }
            //        if (Esta == true)
            //        {
            //            fila["UDSENCARGA"] = rUDSENCARGA;
            //            fila["UDSPENDIENTES"] = "0"; // Convert.ToDecimal(fila["UDSPEDIDAS"].ToString().Replace(".", ",")) - (Convert.ToDecimal(fila["UDSSERVIDAS"].ToString().Replace(".", ",")) + rUDSENCARGA);
            //            string miro = fila["NUMPALET"].ToString();
            //            if (miro != "0,00")
            //            {
            //            }
            //            else
            //            {
            //                fila["UDSACARGAR"] = Convert.ToDecimal(fila["UDSPEDIDAS"].ToString().Replace(".", ",")) - (Convert.ToDecimal(fila["UDSSERVIDAS"].ToString().Replace(".", ",")) + rUDSENCARGA);  //fila["UDSPENDIENTES"].ToString();
            //            }
            //        }
            //    }

            //}
            //catch (Exception Ex)
            //{
            //    Variables.Error = SQL + " " + Ex.Message;
            //}
            //return dt;
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

            if (Con == null)
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

            if (Con == null)
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


        //Lotes automaticos












































    

  
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        protected void PrintCurrentPage(object sender, EventArgs e)
        {

            //DataList1.DataBind();
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);
            //DataList1.RenderControl(hw);
            //string gridHTML = sw.ToString().Replace("\"", "'")
            //    .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();


            //var panel = document.getElementById("<%=pnlContents.ClientID %>");
            //var printWindow = window.open('', '', 'height=400,width=800');
            //printWindow.document.write('<html><head><title></title>');
            //printWindow.document.write('</head><body >');
            //printWindow.document.write('<div style="height:100px;"></div>');
            //printWindow.document.write('<div align=center>');
            //printWindow.document.write(panel.innerHTML);
            //printWindow.document.write('</div>');
            //printWindow.document.write('</body></html>');
            //printWindow.document.close();
            //setTimeout(function() {
            //    printWindow.print();
            //}, 500);
            //return false;


            //ContentPlaceHolder cont = new ContentPlaceHolder();
            //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //HtmlGenericControl li = (HtmlGenericControl)cont.FindControl("Pag1");
            Panel div = (Panel)FindControl("panelContentsPaletAlv");


            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '','height=400,width=800')");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(div);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

            //DataList1.DataBind();
        }


        protected void checkQR_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.ID == "btnQRA")
            {
                //if (ChecQRA.Checked == true)
                //{
                //    this.Session["SelectQR"] = "1";
                //    ChecQRB.Checked = true;
                //    ChecQRC.Checked = true;
                //    ChecQRD.Checked = true;
                //}
                //else
                //{
                //    this.Session["SelectQR"] = "0";
                //    ChecQRB.Checked = false;
                //    ChecQRC.Checked = false;
                //    ChecQRD.Checked = false;
                //}
            }
            if (btn.ID == "btnQRB")
            {
                //if (ChecQRB.Checked == true)
                //{
                //    this.Session["SelectQR"] = "1";
                //    ChecQRA.Checked = true;
                //    ChecQRC.Checked = true;
                //    ChecQRD.Checked = true;
                //}
                //else
                //{
                //    this.Session["SelectQR"] = "0";
                //    ChecQRA.Checked = false;
                //    ChecQRC.Checked = false;
                //    ChecQRD.Checked = false;
                //}
            }
            if (btn.ID == "btnQRC")
            {
                //if (ChecQRC.Checked == true)
                //{
                //    this.Session["SelectQR"] = "1";
                //    ChecQRA.Checked = true;
                //    ChecQRB.Checked = true;
                //    ChecQRD.Checked = true;
                //}
                //else
                //{
                //    this.Session["SelectQR"] = "0";
                //    ChecQRA.Checked = false;
                //    ChecQRB.Checked = false;
                //    ChecQRD.Checked = false;
                //}
            }
            if (btn.ID == "btnQRD")
            {
                //if (ChecQRD.Checked == true)
                //{
                //    this.Session["SelectQR"] = "1";
                //    ChecQRA.Checked = true;
                //    ChecQRB.Checked = true;
                //    ChecQRC.Checked = true;
                //}
                //else
                //{
                //    this.Session["SelectQR"] = "0";
                //    ChecQRA.Checked = false;
                //    ChecQRB.Checked = false;
                //    ChecQRC.Checked = false;
                //}
            }

            btnGenerateCompra_Click(null, null);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}

        }



        private void Nueva_Secuencia()
        {
            DataTable dt3 = Main.CargaSecuencia().Tables[0];
            this.Session["Secuencias"] = dt3;
            DrVariedad.Items.Clear();

            DrVariedad.AppendDataBoundItems = true;
            DrVariedad.DataValueField = "ZID";
            DrVariedad.DataTextField = "ZDESCRIPCION";

            DrVariedad.Items.Insert(0, new ListItem("Seleccione un tipo de lote...", "0"));

            // DrVariedad.Items.Add("0","Seleccione un tipo de lote...");


            DrVariedad.DataSource = dt3;
            DrVariedad.DataBind();

            foreach (DataRow fila in dt3.Rows)
            {
                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                {
                    this.Session["IDSecuencia"] = fila["ZID"].ToString();
                    this.Session["LaMascara"] = fila["ZMASCARA"].ToString();
                    //GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
                    break;
                }
            }
            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            //DrVariedad.Text = "";

        }

        private void Carga_Lotes(string ID)
        {
            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";

            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";
            //SQL += "AND ZID_SECUENCIA in (" + ID +  ")";
            //DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            //DrScaneados.Items.Clear();
            //DrScaneados.DataValueField = "ZID";
            //DrScaneados.DataTextField = "ZLOTE";
            //// insertamos el elemento en la primera posicion:
            //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            //DrScaneados.DataSource = dbA;
            //DrScaneados.DataBind();
            //DrScaneados.SelectedIndex = -1;

            //SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

            //SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
            //SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
            //SQL += " WHERE A.LOTE = B.ZLOTE ";
            //SQL += " AND B.ZID_SECUENCIA in (" + ID + ")";
            //SQL += " AND A.ESTADO <> '2' ";
            try
            {
                string SQL = "";
                if (this.Session["Cerrados"].ToString() == "1") //Los cerrados y procesados 
                {
                    if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO, S  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE A.LOTE = B.ZLOTE ";
                        SQL += " AND A.ESTADO IN (1,2) ";
                    }
                    else
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                        SQL += " AND A.LOTE = B.ZLOTE ";
                        SQL += " AND A.ESTADO IN (1,2)";
                    }
                }
                else //Nuevos
                {
                    if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE A.LOTE = B.ZLOTE ";
                        SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0' OR A.ESTADO = '')";
                    }
                    else
                    {
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                        SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                        SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                        SQL += " AND A.LOTE = B.ZLOTE ";
                        SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0' OR A.ESTADO = '')";
                    }
                }

                DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                DrLotes.Items.Clear();
                DrLotes.DataValueField = "ID";
                DrLotes.DataTextField = "TODO";
                DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
                DrLotes.DataSource = dbB;
                DrLotes.DataBind();
                DrLotes.SelectedIndex = -1;

                lbBuscaCod.Text = "Códigos QR recibidos / finalizados : " + dbB.Rows.Count + "";

                if (DrVariedad.SelectedItem.Value == "Seleccione un tipo de lote...")
                {
                    SQL = "SELECT A.LOTE, A.TIPO_FORM, COUNT(*) as total ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " GROUP BY A.LOTE, A.TIPO_FORM ";
                    SQL += " HAVING COUNT(*) > 1";
                }
                else
                {
                    SQL = "SELECT A.LOTE, A.TIPO_FORM, COUNT(*) as total ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " WHERE A.LOTE = B.ZLOTE ";
                    SQL += " AND B.ZID_SECUENCIA = '" + ID + "'";
                    SQL += " GROUP BY A.LOTE, A.TIPO_FORM ";
                    SQL += " HAVING COUNT(A.LOTE) > 1";
                }

                DataTable dbA = Main.BuscaLote(SQL).Tables[0];

                if (this.Session["Cerrados"].ToString() == "1")
                {
                    LbDuplicados.Text = "";
                    lbtitleLote.Text = "Procesados. Sólo imprimir.";
                    BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                    BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
                    PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                    //BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
                }
                else
                {
                    lbtitleLote.Text = "Normales. Existen Duplicados:";
                    BodyLote.Attributes.Add("style", "background-color: white;");
                    BodyCampos.Attributes.Add("style", "background-color: white;");
                    BodyLotes.Attributes.Add("style", "background-color: white;");
                    BodyAll.Attributes.Add("style", "background-color: white;");
                    PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                    //BodyAll.Attributes.Add("style", "background-color: white;");

                    if (dbA.Rows.Count == 0)
                    {
                        LbDuplicados.Text = "No";
                        LbDuplicados.ForeColor = Color.Black;
                    }
                    else
                    {
                        LbDuplicados.Text = "Si";
                        LbDuplicados.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }



        //private void Carga_Lotes(string ID)
        //{
        //    //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";

        //    //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 AND ZID_SECUENCIA = " + ID;
        //    //DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    //DrScaneados.Items.Clear();
        //    //DrScaneados.DataValueField = "ZID";
        //    //DrScaneados.DataTextField = "ZLOTE";
        //    //// insertamos el elemento en la primera posicion:
        //    //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
        //    //DrScaneados.DataSource = dbA;
        //    //DrScaneados.DataBind();
        //    //DrScaneados.SelectedIndex = -1;

        //    //SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

        //    string SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM ";
        //    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
        //    SQL += " WHERE B.ZID_SECUENCIA = " + ID;
        //    SQL += " AND A.LOTE = B.ZLOTE ";
        //    SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0')";


        //    DataTable dbB = Main.BuscaLote(SQL).Tables[0];
        //    DrLotes.Items.Clear();
        //    DrLotes.DataValueField = "ID";
        //    DrLotes.DataTextField = "LOTE";
        //    DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
        //    DrLotes.DataSource = dbB;
        //    DrLotes.DataBind();
        //    DrLotes.SelectedIndex = -1;

        //    //SQL = "SELECT LOTE, COUNT(*) as total FROM ZENTRADA GROUP BY LOTE HAVING COUNT(*) > 1";
        //    SQL = "SELECT LOTE, TIPO_FORM, COUNT(*) as total FROM ZENTRADA GROUP BY LOTE, TIPO_FORM HAVING COUNT(*) > 1";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    if (dbA.Rows.Count == 0)
        //    {
        //        LbDuplicados.Text = "No";
        //        LbDuplicados.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        LbDuplicados.Text = "Si";
        //        LbDuplicados.ForeColor = Color.Red;
        //    }
        //}

        private void Carga_LotesScaneados(string ID)
        {
            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";

            string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 AND ZID_SECUENCIA = " + ID;
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            DrScaneados.Items.Clear();
            DrScaneados.DataValueField = "ZID";
            DrScaneados.DataTextField = "ZLOTE";
            // insertamos el elemento en la primera posicion:
            DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            DrScaneados.DataSource = dbA;
            DrScaneados.DataBind();
            DrScaneados.SelectedIndex = -1;

            lbBuscaCodEscaneado.Text = "Códigos QR creados / escaneados : " + dbA.Rows.Count + " ";

            //SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

            //SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM ";
            //SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
            //SQL += " WHERE B.ZID_SECUENCIA = " + ID;
            //SQL += " AND A.LOTE = B.ZLOTE ";
            //SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0')";


            //DataTable dbB = Main.BuscaLote(SQL).Tables[0];
            //DrLotes.Items.Clear();
            //DrLotes.DataValueField = "ID";
            //DrLotes.DataTextField = "LOTE";
            //DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            //DrLotes.DataSource = dbB;
            //DrLotes.DataBind();
            //DrLotes.SelectedIndex = -1;




            //SQL = "SELECT LOTE, COUNT(*) as total FROM ZENTRADA GROUP BY LOTE HAVING COUNT(*) > 1";
            //dbA = Main.BuscaLote(SQL).Tables[0];
            //if (dbA.Rows.Count == 0)
            //{
            //    LbDuplicados.Text = "No";
            //    LbDuplicados.ForeColor = Color.Black;
            //}
            //else
            //{
            //    LbDuplicados.Text = "Si";
            //    LbDuplicados.ForeColor = Color.Red;
            //}
        }

        private void GeneraSecuencia(string Dato, string Secuencia)
        {
            //La secuencia es =>   LITERAL#aaS;FECHA#mm;FECHA#dd;SECUENCIA#000
            string Cadena = "";
            string Miro = "";
            try
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Dato, ";");
                foreach (string Linea in CadaLinea)
                {
                    if (Linea != "")
                    {
                        if (Linea.Contains("LITERAL#"))
                        {
                            Cadena += Linea.Replace("LITERAL#", "");
                        }
                        if (Linea.Contains("FECHA#"))
                        {
                            Miro = Linea.Replace("FECHA#", "");
                            Miro = DateTime.Now.ToString(Miro);
                            Cadena += Miro;
                        }
                        if (Linea.Contains("SECUENCIA#"))
                        {
                            Miro = Linea.Replace("SECUENCIA#", "");
                            if (Miro != null)
                            {
                                Miro = Miro.Substring(0, Miro.Length - Secuencia.Length) + Secuencia.ToString();
                                this.Session["NumeroSecuencia"] = Secuencia.ToString();
                            }
                            Cadena += Miro;
                        }
                    }
                }
                if (Cadena != "")
                {
                    txtQRCode.Text = Cadena;
                    this.Session["LOTEenCURSO"] = Cadena;

                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        LbSecuenciaLoteLotes.Text = Cadena;
                    }
                    else
                    {
                        LbSecuenciaLoteLotes.Text = Cadena;
                    }
                    //

                    btnGenerateLote_Click(null, null);

                    LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";
                    LbCodeQRPalteAlvLotes.Text = Cadena;  //"CÓDIGO LOTE:";

                }

            }
            catch (NullReferenceException ex)
            {
                string a = Main.Ficherotraza("Genera Secuencia-->" + ex.Message);
                //Lberror.Text += ex.Message;
                //alertaErr.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }

        }

        //private void Habilita_contoles()
        //{
        //    TxtCampo.Enabled = true;
        //    TxtFecha.Enabled = true;
        //    TxtVariedad.Enabled = true;
        //    TxtCajas.Enabled = true;
        //    TxtPlantas.Enabled = true;
        //    txtQRCode.Enabled = true;
        //    //btnGenerate.Visible = true;
        //    //btnNuevo.Visible = true;
        //    //TxtID.Enabled = true;
        //    TxtForm.Enabled = true;
        //    TxtManojos.Enabled = true;
        //    TxtDesde.Enabled = true;
        //    TxtHasta.Enabled = true;
        //    TxtETDesde.Enabled = true;
        //    TxtETHasta.Enabled = true;
        //    TxtTuneles.Enabled = true;
        //    TxtPasillos.Enabled = true;
        //    TxtObservaciones.Enabled = true;
        //    TxtLoteDestino.Enabled = true;

        //    TxtOK.Enabled = true;
        //    Oculta_Datos(1);
        //}

        //private void Deshabilita_contoles()
        //{
        //    TxtCampo.Enabled = false;
        //    TxtFecha.Enabled = false;
        //    TxtVariedad.Enabled = false;
        //    TxtCajas.Enabled = false;
        //    TxtPlantas.Enabled = false;
        //    txtQRCode.Enabled = false;
        //    TxtLoteDestino.Enabled = false;
        //    //btnGenerate.Visible = false;
        //    //btnNuevo.Visible = false;
        //    TxtID.Enabled = false;
        //    TxtForm.Enabled = false;
        //    TxtManojos.Enabled = false;
        //    TxtDesde.Enabled = false;
        //    TxtHasta.Enabled = false;
        //    TxtETDesde.Enabled = false;
        //    TxtETHasta.Enabled = false;
        //    TxtTuneles.Enabled = false;
        //    TxtPasillos.Enabled = false;
        //    TxtObservaciones.Enabled = false;
        //    TxtOK.Enabled = false;
        //    //Oculta_Datos(0);

        //}
        private void Habilita_contoles()
        {
            for (int N = 0; N <= 50; N++)//Hasta 50 campos
            {
                string MiContent = "DivReg" + N;
                ContentPlaceHolder cont = new ContentPlaceHolder();
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                HtmlGenericControl DivRegistro = (HtmlGenericControl)cont.FindControl(MiContent);
                if (DivRegistro.Visible == true)
                {
                    string DivTextoA = "TxL" + N;
                    TextBox DivLabel = (TextBox)cont.FindControl(DivTextoA);

                    if (DivLabel.Visible == true)
                    {
                        DivLabel.ReadOnly = false;
                    }
                    else
                    {
                        string ComboA = "DrL" + N;
                        DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                        DivComboA.Enabled = true;
                    }

                    DivTextoA = "TxD" + N;
                    DivLabel = (TextBox)cont.FindControl(DivTextoA);

                    if (DivLabel.Visible == true)
                    {
                        DivLabel.ReadOnly = false;
                    }
                    else
                    {
                        string ComboA = "DrR" + N;
                        DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                        DivComboA.Enabled = true;
                    }
                }
            }
            Oculta_Datos(1);
        }

        private void Deshabilita_contoles()
        {
            for (int N = 0; N <= 50; N++)//Hasta 50 campos
            {
                string MiContent = "DivReg" + N;
                ContentPlaceHolder cont = new ContentPlaceHolder();
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                HtmlGenericControl DivRegistro = (HtmlGenericControl)cont.FindControl(MiContent);
                if (DivRegistro.Visible == true)
                {
                    string DivTextoA = "TxL" + N;
                    TextBox DivLabel = (TextBox)cont.FindControl(DivTextoA);

                    if (DivLabel.Visible == true)
                    {
                        DivLabel.ReadOnly = true;
                    }
                    else
                    {
                        string ComboA = "DrL" + N;
                        DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                        DivComboA.Enabled = false;
                    }

                    DivTextoA = "TxD" + N;
                    DivLabel = (TextBox)cont.FindControl(DivTextoA);

                    if (DivLabel.Visible == true)
                    {
                        DivLabel.ReadOnly = true;
                    }
                    else
                    {
                        string ComboA = "DrR" + N;
                        DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                        DivComboA.Enabled = false;
                    }
                }
            }
        }

        protected void btnValidaUser_Click(object sender, EventArgs e)
        {
            LimpiaCajas();
            DataSet ds = Login.ValidarUser(TextUser.Text, TextPass.Text);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                if (txtQRCode.Text != "")
                {
                    TextAlerta.Text = "El usuario no tiene permisos, pero puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                    TextAlertaErr.Text += "";
                    alerta.Visible = true;
                    alertaErr.Visible = false;
                    alertaLog.Visible = false;
                    btProcesa.Visible = true;
                    btPorcesa.Visible = false;
                    Deshabilita_contoles();
                }
                else
                {
                    TextAlerta.Text = "El usuario no tiene permisos para editar esta página.";
                    TextAlertaErr.Text += "";
                    alerta.Visible = true;
                    alertaErr.Visible = false;
                    alertaLog.Visible = false;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    Deshabilita_contoles();
                }
            }
            else
            {
                if (dt.Rows[0]["ZNIVEL"].ToString() != "9")
                {
                    if (txtQRCode.Text != "")
                    {
                        TextAlerta.Text = "El usuario no tiene permisos suficientes para editar esta página, pero puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                        TextAlertaErr.Text += "";
                        alerta.Visible = true;
                        alertaErr.Visible = false;
                        alertaLog.Visible = false;
                        btProcesa.Visible = true;
                        btPorcesa.Visible = false;
                        Deshabilita_contoles();
                    }
                    else
                    {
                        TextAlerta.Text = "El usuario no tiene permisos suficientes para editar esta página.";
                        TextAlertaErr.Text += "";
                        alerta.Visible = true;
                        alertaErr.Visible = false;
                        alertaLog.Visible = false;
                        btProcesa.Visible = false;
                        btPorcesa.Visible = false;
                        Deshabilita_contoles();
                    }
                }
                else
                {
                    if (txtQRCode.Text != "")
                    {
                        TextAlerta.Text = "Se habilitarán los controles de la página para poder tratar con ellos. Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                        TextAlertaErr.Text += "";
                        alerta.Visible = true;
                        alertaErr.Visible = false;
                        alertaLog.Visible = false;
                        btProcesa.Visible = true;
                        btPorcesa.Visible = false;
                        Habilita_contoles();
                    }
                    else
                    {
                        TextAlerta.Text = "Se habilitarán los controles de la página para poder tratar con ellos.";
                        TextAlertaErr.Text += "";
                        alerta.Visible = true;
                        alertaErr.Visible = false;
                        alertaLog.Visible = false;
                        btProcesa.Visible = false;
                        btPorcesa.Visible = false;
                        Habilita_contoles();
                    }
                }
            }
        }

        private bool validateTime(string dateInString)
        {
            DateTime temp;
            if (DateTime.TryParse(dateInString, out temp))
            {
                return true;
            }
            return false;
        }

        private void Habilita_Boton(int Estado)
        {
            Modifica = Estado;

            if (Modifica == 0)
            {//Sin lote procesado
                BtGuardaLote.Visible = false;
                BtModifica.Visible = true;
                BtModifica.Enabled = false;
                BtCancelaLote.Enabled = false;
                BtDelete.Enabled = false;
            }
            else if (Modifica == 1)
            {//Con lote procesado
                BtGuardaLote.Visible = false;
                BtModifica.Visible = true;
                BtModifica.Enabled = true;
                BtCancelaLote.Enabled = false;
                BtDelete.Enabled = false;
            }
            else if (Modifica == 2)
            {//Edicion de lote 
                BtGuardaLote.Visible = true;
                BtCancelaLote.Enabled = true;
                BtModifica.Visible = false;
                BtModifica.Enabled = false;
                BtDelete.Enabled = true;
            }
            else if (Modifica == 3)
            {//Cancela Edicion de lote 

                BtGuardaLote.Visible = false;
                BtModifica.Visible = true;
                BtModifica.Enabled = false;
                BtCancelaLote.Enabled = false;
                BtDelete.Enabled = false;
            }
        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            this.Session["ElIDaBorrar"] = "";
            Page.MaintainScrollPositionOnPostBack = true;
            this.Session["IDCabecera"] = "0";
            MiOpenMenu();

            DrVariedad_SelectedIndexChanged(null, null);

            //btnGenerate_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Elimina_Borrados();
            Page.MaintainScrollPositionOnPostBack = true;
            MiOpenMenu();

            //if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            //string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
            //SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
            //SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
            //SQL += " NUMPALET = 0.00 ";
            //SQL += " WHERE ID = " + Trata[0];
            //DBHelper.ExecuteNonQuery(SQL);

            //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

            //DBHelper.ExecuteNonQuery(SQL);

            //Carga_tabla();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();

            //this.Session["ElIDaBorrar"] = "";

        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
            MiOpenMenu();
            this.Session["ElIDaBorrar"] = "";
            if (BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }
            btnCancelaLote_Click(null,null);

        }

        protected void DrFindL0_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void DrFindR0_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void DrLs_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            if (TxtID.Text == "")
            {
                //alerta.Visible = true;
                //TextAlerta.Text = "Seleccione un código QR para poder modificar.";

                Lbmensaje.Text = "Para poder modificar se debe seleccionar previamente un Lote.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //string targetId = Page.Request.Params.Get("__EVENTTARGET");
                Page.MaintainScrollPositionOnPostBack = false;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('btNew').focus()", true);
                return;
            }
            //txtQRCodebis.Visible = false;
            txtQRCode.Visible = true;

            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //BtGuardaLote.Visible = true;
            //BtGuardaLote.Enabled = true;
            //BtModifica.Visible = false;
            //BtCancelaLote.Enabled = true;
            //BtDelete.Enabled = false;
            Habilita_Boton(2);
            Habilita_contoles();

            btnGenerateLote_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodoLote_Click(sender, e);
            }
            if(BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }
        }


        //protected void btnModifica_Click(object sender, EventArgs e)
        //{
        //    this.Session["IDCabecera"] = "0";
        //    if (TxtID.Text == "")
        //    {
        //        //alerta.Visible = true;
        //        //TextAlerta.Text = "Seleccione un código QR para poder modificar.";

        //        Lbmensaje.Text = "Para poder modificar se debe seleccionar previamente un Lote identificado desde los Formularios de Scan-IT.";
        //        cuestion.Visible = false;
        //        Asume.Visible = true;
        //        DvPreparado.Visible = true;
        //        //string targetId = Page.Request.Params.Get("__EVENTTARGET");
        //        Page.MaintainScrollPositionOnPostBack = false;
        //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('btNew').focus()", true);
        //        //if (txtQRCode.Text != "")
        //        //{
        //            //btnAutoGenerado_Click(null, null);
        //            DrVariedad_SelectedIndexChanged(null,null);
        //        //}
        //        //if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //        //{
        //        //    MontaEtiqueta();
        //        //}
        //        return;
        //    }
        //    //txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;

        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

        //    BtGuardaLote.Visible = true;
        //    BtGuardaLote.Enabled = true;
        //    BtModifica.Visible = false;
        //    BtCancelaLote.Enabled = true;
        //    BtCancelaLote.Enabled = true;
        //    BtDelete.Enabled = false;
        //    Habilita_contoles();

        //    btnGenerate_Click(sender, e);
        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        btnGeneraTodoPerf_Click(sender, e);
        //    }
        //    else
        //    {
        //        btnGenerateTodo_Click(sender, e);
        //    }

        //    //btnGenerate_Click(sender, e);
        //    //btnGenerateZXING_Click(sender, e);
        //    //if (DrPrinters.SelectedItem.Value == "4")
        //    //{
        //    //    btnGeneraTodoPerf_Click(sender, e);
        //    //}
        //    //else
        //    //{
        //    //    btnGenerateTodo_Click(sender, e);
        //    //}
        //    //btnGenerateTodo_Click(sender, e);


        //    //Repara_Fecha(TxtFecha.Text);

        //    //string SQL = "UPDATE ZENTRADA SET TIPO_FORM = '" + TxtForm.Text + "',";
        //    //SQL += "FECHA ='" + TxtFecha.Text + "',";
        //    //SQL += "TIPO_PLANTA ='" + TxtCampo.Text + "',";
        //    //SQL += "VARIEDAD ='" + TxtVariedad.Text + "',";
        //    //SQL += "LOTE ='" + txtQRCode.Text + "',";
        //    //SQL += "UNIDADES ='" + TxtCajas.Text + "',";
        //    //SQL += "NUM_UNIDADES ='" + TxtPlantas.Text + "',";
        //    //SQL += "MANOJOS ='" + TxtManojos.Text + "',";
        //    //SQL += "DESDE ='" + TxtDesde.Text + "',";
        //    //SQL += "HASTA ='" + TxtHasta.Text + "',";
        //    //SQL += "ETDESDE ='" + TxtETDesde.Text + "',";
        //    //SQL += "ETHASTA ='" + TxtETHasta.Text + "',";
        //    //SQL += "TUNELES ='" + TxtTuneles.Text + "',";
        //    //SQL += "PASILLOS ='" + TxtPasillos.Text + "',";
        //    //SQL += "OBSERVACIONES ='" + TxtObservaciones.Text + "',";
        //    //SQL += "OK ='" + TxtOK.Text + "'";
        //    //SQL += " WHERE ID = " + LbIDLote.Text;
        //    //DBHelper.ExecuteNonQuery(SQL);

        //    //LimpiaCajas();
        //    //Carga_Lotes(this.Session["IDSecuencia"].ToString());

        //}

        //protected void btnModifica_Click(object sender, EventArgs e)
        //{
        //    alertaErr.Visible = false;
        //    //Boolean Esta = validateTime(TxtFecha.Text);
        //    //if (Esta == false)
        //    //{
        //    //    TextAlertaErr.Text = "El campo FECHA CORTE no contiene una fecha valida.";
        //    //    TextAlerta.Text = "";
        //    //    alertaLog.Visible = false;
        //    //    alerta.Visible = false;
        //    //    alertaErr.Visible = true;
        //    //    return;
        //    //}
        //    Repara_Fecha(TxtFecha.Text);

        //    string SQL = "UPDATE ZENTRADA SET TIPO_FORM = '" + TxtForm.Text + "',";
        //    SQL += "FECHA ='" + TxtFecha.Text + "',";
        //    SQL += "TIPO_PLANTA ='" + TxtCampo.Text + "',";
        //    SQL += "VARIEDAD ='" + TxtVariedad.Text + "',";
        //    SQL += "LOTE ='" + txtQRCode.Text + "',";
        //    SQL += "UNIDADES ='" + TxtCajas.Text + "',";
        //    SQL += "NUM_UNIDADES ='" + TxtPlantas.Text + "',";
        //    SQL += "MANOJOS ='" + TxtManojos.Text + "',";
        //    SQL += "DESDE ='" + TxtDesde.Text + "',";
        //    SQL += "HASTA ='" + TxtHasta.Text + "',";
        //    SQL += "ETDESDE ='" + TxtETDesde.Text + "',";
        //    SQL += "ETHASTA ='" + TxtETHasta.Text + "',";
        //    SQL += "TUNELES ='" + TxtTuneles.Text + "',";
        //    SQL += "PASILLOS ='" + TxtPasillos.Text + "',";
        //    SQL += "OBSERVACIONES ='" + TxtObservaciones.Text + "',";
        //    SQL += "LOTEDESTINO ='" + TxtLoteDestino.Text + "',";
        //    SQL += "OK ='" + TxtOK.Text + "'";
        //    SQL += " WHERE ID = " + LbIDLote.Text;
        //    DBHelper.ExecuteNonQuery(SQL);

        //    LimpiaCajas();
        //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

        //}

        //private void Elimina_Borrados()
        //{
        //    ////Cambia de tabla solo aquellos que se ajusten al formulario con estado a 2
        //    ////Queda a 2 la ultima inserción para poder revertir la importacion
        //    try
        //    {
        //        //string SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.ESTADO ";
        //        //SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
        //        //SQL += " WHERE B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
        //        //SQL += " AND A.TIPO_FORM = B.ZTITULO ";
        //        ////SQL += " AND A.OBSERVACIONES LIKE 'BORRADO%' ";
        //        //SQL += " AND A.ID = " + TxtID.Text;

        //        string SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.ESTADO ";
        //        SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
        //        SQL += " WHERE A.TIPO_FORM = B.ZTITULO ";
        //        SQL += " AND A.ID = " + TxtID.Text;

        //        DataTable dbB = Main.BuscaLote(SQL).Tables[0];

        //        foreach (DataRow fila in dbB.Rows)
        //        {
        //            SQL = " INSERT INTO ZENTRADA_BORRADOS (ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
        //            SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA) ";
        //            SQL += " SELECT ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
        //            SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA ";
        //            SQL += " FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

        //            DBHelper.ExecuteNonQuery(SQL);

        //            SQL = " DELETE FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

        //            DBHelper.ExecuteNonQuery(SQL);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TextAlerta.Text = ex.Message;
        //        alerta.Visible = true;
        //    }
        //    LimpiaCajas();
        //    DrVariedad_SelectedIndexChanged(null, null);
        //    this.Session["IDCabecera"] = "0";
        //    if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //    {
        //        MontaEtiqueta();
        //    }

        //}

        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (TxtID.Text == "")
        //    {
        //        alerta.Visible = true;
        //        TextAlerta.Text = "Seleccione un código QR para poder eliminar.";
        //        return;
        //    }
        //    //txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;


        //    Lbmensaje.Text = "Se eliminará el Lote " + txtQRCode.Text + " de la base de datos.¿Desea Continuar?";
        //    DvPreparado.Visible = true;
        //    cuestion.Visible = true;
        //    Asume.Visible = false;
        //    Page.MaintainScrollPositionOnPostBack = false;
        //    this.Session["IDCabecera"] = "0";
        //    if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //    {
        //        MontaEtiqueta();
        //    }


        //    //if (BtModifica.Visible == true && TextAlertaLog.Text != "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
        //    //{
        //    //    TextAlertaLog.Text = "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar";
        //    //    alerta.Visible = false;
        //    //    alertaErr.Visible = false;
        //    //    alertaLog.Visible = true;
        //    //    return;

        //    //    //btnValidaUser_Click(sender, e);
        //    //}
        //    //if (TextAlertaLog.Text == "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
        //    //{
        //    //    Elimina_Borrados();
        //    //    alerta.Visible = false;
        //    //    alertaErr.Visible = false;
        //    //    alertaLog.Visible = false;
        //    //}
        //}


        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    //string SQL = "DELETE FROM ZENTRADA WHERE ID = " + LbIDLote.Text;
        //    string SQL = "UPDATE ZENTRADA  SET ESTADO = '2' ";
        //    SQL += " WHERE ID = " + LbIDLote.Text;
        //    //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
        //    DBHelper.ExecuteNonQuery(SQL);
        //    LimpiaCajas();
        //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //    //BTerminado.Visible = false;
        //    //Btfin.Visible = false;
        //}

        private void Elimina_Borrados()
        {
            ////Cambia de tabla solo aquellos que se ajusten al formulario con estado a 2
            ////Queda a 2 la ultima inserción para poder revertir la importacion
            try
            {
                string SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.ESTADO ";
                SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
                SQL += " WHERE A.TIPO_FORM = B.ZTITULO ";
                SQL += " AND A.ID = " + TxtID.Text;

                DataTable dbB = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow fila in dbB.Rows)
                {
                    SQL = " INSERT INTO ZENTRADA_BORRADOS (ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA,TIPO_CAJA,TIPO_PALET,LAVADO,TRATAMIENTO,TURBA,INC_CALIDAD) ";
                    SQL += " SELECT ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA,TIPO_CAJA,TIPO_PALET,LAVADO,TRATAMIENTO,TURBA,INC_CALIDAD ";
                    SQL += " FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = " DELETE FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            catch (Exception ex)
            {
                TextAlerta.Text = ex.Message;
                alerta.Visible = true;
            }
            LimpiaCajas();
            DrVariedad_SelectedIndexChanged(null, null);

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (TxtID.Text == "")
            {
                //alerta.Visible = true;
                //TextAlerta.Text = "Seleccione un código QR para poder modificar.";

                Lbmensaje.Text = "Para poder eliminar se debe seleccionar previamente un Lote con identificador.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //string targetId = Page.Request.Params.Get("__EVENTTARGET");
                Page.MaintainScrollPositionOnPostBack = true;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('btNew').focus()", true);
                return;
            }
            //txtQRCodebis.Visible = false;
            txtQRCode.Visible = true;


            Lbmensaje.Text = "Se eliminará el Lote " + txtQRCode.Text + " de la base de datos.¿Desea Continuar?";
            windowmessaje.Visible = true;
            cuestion.Visible = true;
            Asume.Visible = false;
            MiCloseMenu();
            Page.MaintainScrollPositionOnPostBack = false;

            if (BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }

            //if (BtModifica.Visible == true && TextAlertaLog.Text != "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
            //{
            //    TextAlertaLog.Text = "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar";
            //    alerta.Visible = false;
            //    alertaErr.Visible = false;
            //    alertaLog.Visible = true;
            //    return;

            //    //btnValidaUser_Click(sender, e);
            //}
            //if (TextAlertaLog.Text == "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
            //{
            //    Elimina_Borrados();
            //    alerta.Visible = false;
            //    alertaErr.Visible = false;
            //    alertaLog.Visible = false;
            //}
        }

        protected void BTerminado_Click(object sender, EventArgs e)
        {
            string SQL = "UPDATE ZENTRADA  SET ESTADO = '0' ";
            SQL += " WHERE ID = " + LbIDLote.Text;
            //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            DBHelper.ExecuteNonQuery(SQL);
            //Btfin.Visible = true;
            //BTerminado.Visible = false;

            btnGenerateLote_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodoLote_Click(sender, e);
            }

            //btnGenerate_Click(sender, e);
            //btnGenerateZXING_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}
            //btnGenerateTodo_Click(sender, e);
            alerta.Visible = false;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {

            alerta.Visible = false;
            alertaLog.Visible = false;
            alertaErr.Visible = false;
            btProcesa.Visible = false;
            btPorcesa.Visible = false;
            btNew.Enabled = false;
            LimpiaCajas();

            HtmlGenericControl Ia = (HtmlGenericControl)IManual;
            Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
            BodyLote.Attributes["style"] = "background-color: white;";
            LbQR.Text = "Listas para la generación de QR en modo automático";
            Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
            txtQRCodeManu.Visible = false;
            txtQRCode.Visible = true;
            txtQRCodeManu.Text = "";

            DrLotes.SelectedIndex = -1;
            DrScaneados.SelectedIndex = -1;


            string SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dbA.Rows)
            {
                this.Session["NumeroSecuencia"] = fila["ZSECUENCIA"].ToString();
                break;
            }

            DataTable dt3 = Main.CargaSecuencia().Tables[0];
            this.Session["Secuencias"] = dt3;
            foreach (DataRow fila in dt3.Rows)
            {
                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                {
                    this.Session["IDSecuencia"] = fila["ZID"].ToString();
                    GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
                    Carga_Lotes(this.Session["IDSecuencia"].ToString());
                    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
                    alerta.Visible = true;
                    break;
                }
            }

            if (BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }
        }

        //protected void btnUser_Click(object sender, EventArgs e)
        //{
        //    if (TxtForm.Enabled == true)
        //    {
        //        Deshabilita_contoles();
        //        Oculta_Datos(0);
        //    }
        //    else
        //    {
        //        TextAlertaLog.Text = "Deberá introducir un usuario con permisos para poder editar esta página:";
        //        TextAlertaErr.Text = "";
        //        TextAlerta.Text = "";
        //        alerta.Visible = false;
        //        alertaLog.Visible = true;
        //        alertaErr.Visible = false;
        //        btProcesa.Visible = false;
        //        btPorcesa.Visible = false;
        //        //BTerminado.Visible = false;
        //        //Btfin.Visible = false;
        //    }
        //}

        protected void btnUser_Click(object sender, EventArgs e)
        {
            if (TxL0.Enabled == true)
            {
                Deshabilita_contoles();
                Oculta_Datos(0);
            }
            else
            {
                TextAlertaLog.Text = "Deberá introducir un usuario con permisos para poder editar esta página:";
                TextAlertaErr.Text = "";
                TextAlerta.Text = "";
                alerta.Visible = false;
                alertaLog.Visible = true;
                alertaErr.Visible = false;
                btProcesa.Visible = false;
                btPorcesa.Visible = false;
                //BTerminado.Visible = false;
                //Btfin.Visible = false;
            }
        }


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (DrVariedad.SelectedItem.Value == "-1")
            {
                alerta.Visible = true;
                Nueva_Secuencia();
            }
            else
            {
                int AA = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
                string SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + AA + "' ";
                SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
                //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
                DBHelper.ExecuteNonQuery(SQL);
                DataTable dt3 = Main.CargaSecuencia().Tables[0];
                this.Session["Secuencias"] = dt3;
                GeneraSecuencia(this.Session["LaMascara"].ToString(), Convert.ToString(AA));
            }
        }
        //protected void btnNuevoLote_Click(object sender, EventArgs e)
        //{
        //    btnNuevoLote.Visible = false;
        //    BtGuardaLote.Visible = true;
        //    BtModifica.Visible = false;
        //    BtCancelaLote.Visible = true;
        //    BtDelete.Visible = false;
        //    btGeneraNew.Visible = true;
        //    //Btfin.Visible = false;
        //    //BTerminado.Visible = false;
        //    LimpiaCajas();
        //    TxtForm.Text = "Independiente";
        //}


        protected void CancelLote_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            BtGuardaLote.Visible = false;
            BtModifica.Visible = true;
            BtModifica.Enabled = true;
            BtCancelaLote.Enabled = false;
            BtDelete.Enabled = true;
            BtDelete.Enabled = true;
            Deshabilita_contoles();
        }

        protected void btnCancelaLote_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if(btn != null && btn.ID == "BtCancelaLote")
            {
                if(this.Session["CancelaConsulta"].ToString() == "DRVariedad")
                {
                    DrVariedad_SelectedIndexChanged(null, null); 
                }
                else if (this.Session["CancelaConsulta"].ToString() == "DRLotes")
                {
                    DrLotes_SelectedIndexChanged(null, null);
                }
                else if (this.Session["CancelaConsulta"].ToString() == "DrScaneados")
                {
                    DrScaneados_SelectedIndexChanged(null, null);
                }
            }


            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtModifica.Enabled = true;
            //BtCancelaLote.Enabled = false;
            //BtDelete.Enabled = true;
            //BtDelete.Enabled = true;
            Habilita_Boton(1);
            Deshabilita_contoles();
            //DrLotes_SelectedIndexChanged(null, null);

            if (txtQRCode.Text != "")
            {
                btnAutoGeneradoLotes_Click(null, null);
            }

            
            if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }


            //btnNuevoLote.Visible = true;
            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtCancelaLote.Visible = false;
            //btGeneraNew.Visible = false;
            //BtDelete.Visible = true;
        }

        //protected void btnCancelaLote_Click(object sender, EventArgs e)
        //{
        //    btnNuevoLote.Visible = true;
        //    BtGuardaLote.Visible = false;
        //    BtModifica.Visible = true;
        //    BtCancelaLote.Visible = false;
        //    btGeneraNew.Visible = false;
        //    BtDelete.Visible = true;
        //}

        //private void Repara_Fecha(string Fecha)
        //{
        //    string Mdia = "";
        //    string Mmes = "";
        //    string Mano = "";
        //    int a = 0;

        //    if (TxtFecha.Text.Contains("/"))
        //    {
        //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "/");

        //        foreach (string Linea in CadaLinea)
        //        {
        //            if (a == 0) { Mdia = Linea; }
        //            if (a == 1) { Mmes = Linea; }
        //            if (a == 2)
        //            {
        //                Mano = Linea;
        //                TxtFecha.Text = Mano + "-" + Mmes + "-" + Mdia;
        //            }
        //        }
        //    }

        //    if (TxtFecha.Text.Contains("/"))
        //    {
        //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "-");

        //        foreach (string Linea in CadaLinea)
        //        {
        //            if (a == 0) { Mdia = Linea; }
        //            if (a == 1) { Mmes = Linea; }
        //            if (a == 2)
        //            {
        //                Mano = Linea;
        //                TxtFecha.Text = Mano + "-" + Mmes + "-" + Mdia;
        //            }
        //        }
        //    }
        //}

        //private void Convierte_Fecha(string Fecha)
        //{
        //    string Mdia = "";
        //    string Mmes = "";
        //    string Mano = "";
        //    int a = 0;

        //    if (TxtFecha.Text.Contains("/"))
        //    {
        //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "/");

        //        foreach (string Linea in CadaLinea)
        //        {
        //            if (a == 0) { Mdia = Linea; }
        //            if (a == 1) { Mmes = Linea; }
        //            if (a == 2)
        //            {
        //                Mano = Linea;
        //                TxtFecha.Text = Mano + Mmes + Mdia;
        //            }
        //        }
        //    }

        //    if (TxtFecha.Text.Contains("/"))
        //    {
        //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "-");

        //        foreach (string Linea in CadaLinea)
        //        {
        //            if (a == 0) { Mdia = Linea; }
        //            if (a == 1) { Mmes = Linea; }
        //            if (a == 2)
        //            {
        //                Mano = Linea;
        //                TxtFecha.Text = Mano + Mmes + Mdia;
        //            }
        //        }
        //    }
        //}

        private void Repara_Fecha(string Fecha)
        {
            string Mdia = "";
            string Mmes = "";
            string Mano = "";
            int a = 0;

            if (Fecha.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Fecha, "/");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        Fecha = Mano + "-" + Mmes + "-" + Mdia;
                    }
                }
            }

            if (Fecha.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Fecha, "-");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        Fecha = Mano + "-" + Mmes + "-" + Mdia;
                    }
                }
            }
        }

        private void Convierte_Fecha(TextBox Control, string Fecha)
        {
            string Mdia = "";
            string Mmes = "";
            string Mano = "";
            int a = 0;

            if (Control.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Control.Text, "/");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        Control.Text = Mano + Mmes + Mdia;
                    }
                }
            }

            if (Control.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Control.Text, "-");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        Control.Text = Mano + Mmes + Mdia;
                    }
                }
            }
        }

        protected void BtGuardaLote_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            if (TmpLbFechaS != "") { Repara_Fecha(TmpLbFechaS); }
            txtQRCode.Visible = true;

            //Si no modifica el estado y viene vacio, como existe el formulario en edicion añado un cero
            if (TmpLbEstado == "" || TmpLbEstado == null)
            {
                TmpLbEstado = "0";
            }

            string SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + LbIDLote.Text + "' ";
            DataTable dt = Main.BuscaLote(SQL).Tables[0];

            string SQLUpdate = " UPDATE ZENTRADA SET";
            SQL = "";
            string SQLWhere = "";

            if (this.Session["IDLista"].ToString() == "Escaneados")
            {
                SQL = "UPDATE ZLOTESCREADOS SET ZFECHA = '" + TmpLbFechaS + "',";
                SQL += "LOTE ='" + txtQRCode.Text + "',";
                SQL += " WHERE ID = " + LbIDLote.Text;
            }
            else if (this.Session["IDLista"].ToString() == "Lotes")
            {


                foreach (DataRow filas in dt.Rows)
                {
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        for (int N = 0; N <= 50; N++)//Hasta 50 campos
                        {
                            string MiContent = "DivReg" + N;
                            ContentPlaceHolder cont = new ContentPlaceHolder();
                            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                            HtmlGenericControl DivRegistro = (HtmlGenericControl)cont.FindControl(MiContent);
                            if (DivRegistro.Visible == true)
                            {
                                string DivTextoA = "TxL" + N;
                                TextBox DivLabel = (TextBox)cont.FindControl(DivTextoA);

                                if (DivLabel.Visible == true)
                                {
                                    string DivIDA = "LBCOLL" + N;
                                    TextBox DivLabelA = (TextBox)cont.FindControl(DivIDA);

                                    string O = dt.Columns[i].ColumnName;
                                    //Si tiene el mismo nombre de la columna
                                    if (dt.Columns[i].ColumnName == DivLabelA.Text)
                                    {
                                        //DivLabel.Text = filas[dt.Columns[i]].ToString();
                                        if (dt.Columns[i].ColumnName == "ID")
                                        {
                                            SQLWhere = " WHERE ID = " + DivLabel.Text;
                                        }
                                        else
                                        {
                                            if (SQL == "")
                                            {
                                                SQL = " " + DivLabelA.Text + " ='" + DivLabel.Text + "' ";
                                            }
                                            else
                                            {
                                                if (dt.Columns[i].ColumnName == "ID_SECUENCIA")
                                                {
                                                    SQL += ", " + DivLabelA.Text + " = 0 ";
                                                }
                                                else
                                                {
                                                    SQL += ", " + DivLabelA.Text + " ='" + DivLabel.Text + "' ";
                                                }
                                            }
                                        }
                                    }
                                    //Siguiente registro
                                    if ((dt.Columns.Count - 1) < i) { i += 1; }

                                }
                                else
                                {
                                    string ComboA = "DrL" + N;
                                    DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);

                                    //if (filas[dt.Columns[i].ColumnName].ToString() == DivComboA.Text)
                                    if (dt.Columns[i].ColumnName == DivComboA.Text)
                                    {
                                        //DivComboA.Text = filas[dt.Columns[i]].ToString();
                                        if (dt.Columns[i].ColumnName == "ID")
                                        {
                                            LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                        }
                                        if (dt.Columns[i].ColumnName == "LOTE")
                                        {
                                            txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                        }
                                    }
                                    //Siguiente registro
                                    if ((dt.Columns.Count - 1) < i) { i += 1; }
                                }

                                DivTextoA = "TxD" + N;
                                DivLabel = (TextBox)cont.FindControl(DivTextoA);

                                if (DivLabel.Visible == true)
                                {
                                    string DivIDA = "LBCOLD" + N;
                                    TextBox DivLabelA = (TextBox)cont.FindControl(DivIDA);

                                    //if (filas[dt.Columns[i].ColumnName].ToString() == DivLabelA.Text)
                                    if (dt.Columns[i].ColumnName == DivLabelA.Text)
                                    {
                                        //DivLabel.Text = filas[dt.Columns[i]].ToString();
                                        if (dt.Columns[i].ColumnName == "ID")
                                        {
                                            SQLWhere = " WHERE ID = " + DivLabel.Text;
                                        }
                                        else
                                        {
                                            if (SQL == "")
                                            {
                                                SQL = " " + DivLabelA.Text + " ='" + DivLabel.Text + "' ";
                                            }
                                            else
                                            {
                                                if (dt.Columns[i].ColumnName == "ID_SECUENCIA")
                                                {
                                                    SQL += ", " + DivLabelA.Text + " = 0 ";
                                                }
                                                else
                                                {
                                                    SQL += ", " + DivLabelA.Text + " ='" + DivLabel.Text + "' ";
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    string ComboA = "DrR" + N;
                                    DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                                    //if (filas[dt.Columns[i].ColumnName].ToString() == DivComboA.Text)
                                    if (dt.Columns[i].ColumnName == DivComboA.Text)
                                    {
                                        //DivComboA.Text = filas[dt.Columns[i]].ToString();
                                        if (dt.Columns[i].ColumnName == "ID")
                                        {
                                            LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                        }
                                        if (dt.Columns[i].ColumnName == "LOTE")
                                        {
                                            txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (SQLUpdate != "" && SQL != "" && SQLWhere != "")
                {
                    DBHelper.ExecuteNonQuery(SQLUpdate + SQL + SQLWhere);
                }
                else
                {
                    //No se inserta
                    Lbmensaje.Text = "No se puede insertar la consulta " + SQLUpdate + SQL + SQLWhere;
                    windowmessaje.Visible = false;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    MiCloseMenu();
                }
            }

            //Vuelvo a carga la descripcion con la modificación
            SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + LbIDLote.Text + "' ";
            dt = Main.BuscaLote(SQL).Tables[0];

            CargaDescripcionLote(dt);

            BtDelete.Enabled = true;
            btNew.Enabled = true;

            btnCancelaLote_Click(sender, e);



            //btnGenerate_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}
            //////btnGenerateTodo_Click(sender, e);
            ///

            //alerta.Visible = false;
            //Carga_Lotes(this.Session["IDSecuencia"].ToString());
            //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        }


        //protected void BtGuardaLote_Click(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

        //    //DateTime dt;

        //    //if (DateTime.TryParse(TxtFecha.Text, out dt))
        //    //if (DateTime.TryParseExact(TxtFecha.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
        //    //{
        //    //    TextAlertaErr.Text = "El campo FECHA CORTE no contiene una fecha valida.";
        //    //    TextAlerta.Text = "";
        //    //    alertaLog.Visible = false;
        //    //    alerta.Visible = false;
        //    //    alertaErr.Visible = true;
        //    //    return;
        //    //}

        //    Repara_Fecha(TxtFecha.Text);
        //    //txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;

        //    //string SQL = "INSERT INTO ZENTRADA (TIPO_FORM, FECHA, TIPO_PLANTA, VARIEDAD, LOTE, UNIDADES, NUM_UNIDADES, MANOJOS, ";
        //    //SQL += "DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS, OBSERVACIONES, OK) ";
        //    //SQL += " VALUES ('" + TxtForm.Text + "','" + TxtFecha.Text + "','" + TxtCampo.Text + "','" + TxtVariedad.Text + "','" + txtQRCode.Text + "','" + TxtCajas.Text + "',";
        //    //SQL += "'" + TxtPlantas.Text + "','" + TxtManojos.Text + "','" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + TxtETDesde.Text + "','" + TxtETHasta.Text + "',";
        //    //SQL += "'" + TxtTuneles.Text + "','" + TxtPasillos.Text + "','" + TxtObservaciones.Text + "','" + TxtOK.Text + "')";

        //    //DBHelper.ExecuteNonQuery(SQL);
        //    //Si no modifica el estado y viene vacio, como existe el formulario en edicion añado un cero
        //    if (TxtEstado.Text == "" || TxtEstado.Text == null)
        //    {
        //        TxtEstado.Text = "0";
        //    }

        //    string SQL = "";

        //    if (this.Session["IDLista"].ToString() == "Escaneados")
        //    {
        //        SQL = "UPDATE ZLOTESCREADOS SET ZFECHA = '" + TxtFecha.Text + "',";
        //        SQL += "LOTE ='" + txtQRCode.Text + "',";
        //        SQL += " WHERE ID = " + LbIDLote.Text;
        //    }
        //    else if (this.Session["IDLista"].ToString() == "Lotes")
        //    {
        //        SQL = "UPDATE ZENTRADA SET TIPO_FORM = '" + TxtForm.Text + "',";
        //        SQL += "FECHA ='" + TxtFecha.Text + "',";
        //        SQL += "TIPO_PLANTA ='" + TxtCampo.Text + "',";
        //        SQL += "VARIEDAD ='" + TxtVariedad.Text + "',";
        //        SQL += "LOTE ='" + txtQRCode.Text + "',";
        //        SQL += "UNIDADES ='" + TxtCajas.Text + "',";
        //        SQL += "NUM_UNIDADES ='" + TxtPlantas.Text + "',";
        //        SQL += "MANOJOS ='" + TxtManojos.Text + "',";
        //        SQL += "DESDE ='" + TxtDesde.Text + "',";
        //        SQL += "HASTA ='" + TxtHasta.Text + "',";
        //        SQL += "ETDESDE ='" + TxtETDesde.Text + "',";
        //        SQL += "ETHASTA ='" + TxtETHasta.Text + "',";
        //        SQL += "TUNELES ='" + TxtTuneles.Text + "',";
        //        SQL += "PASILLOS ='" + TxtPasillos.Text + "',";
        //        SQL += "OBSERVACIONES ='" + TxtObservaciones.Text + "',";
        //        SQL += "LOTEDESTINO ='" + TxtLoteDestino.Text + "',";
        //        SQL += "OK ='" + TxtOK.Text + "',";
        //        SQL += "ESTADO ='" + TxtEstado.Text + "',";
        //        SQL += "DeviceName ='" + TxtDispositivo.Text + "'";
        //        SQL += " WHERE ID = " + LbIDLote.Text;
        //    }
        //    DBHelper.ExecuteNonQuery(SQL);

        //    SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + LbIDLote.Text + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    CargaDescripcionLote(dbA);

        //    //CargaModificado(LbIDLote.Text);

        //    this.Session["IDLista"] = "Lotes";
        //    BtDelete.Enabled = true;
        //    btNew.Enabled = true;

        //    CancelLote_Click(sender, e);

        //    this.Session["IDCabecera"] = "0";
        //    if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //    {
        //        MontaEtiqueta();
        //    }
        //    //btnGenerate_Click(sender, e);
        //    //if (DrPrinters.SelectedItem.Value == "4")
        //    //{
        //    //    btnGeneraTodoPerf_Click(sender, e);
        //    //}
        //    //else
        //    //{
        //    //    btnGenerateTodo_Click(sender, e);
        //    //}
        //    //////btnGenerateTodo_Click(sender, e);
        //    ///

        //    //alerta.Visible = false;
        //    //Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //    //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //}

        //protected void BtGuardaLote_Click(object sender, EventArgs e)
        //{
        //    DateTime dt;

        //    //if (DateTime.TryParse(TxtFecha.Text, out dt))
        //    //if (DateTime.TryParseExact(TxtFecha.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
        //    //{
        //    //    TextAlertaErr.Text = "El campo FECHA CORTE no contiene una fecha valida.";
        //    //    TextAlerta.Text = "";
        //    //    alertaLog.Visible = false;
        //    //    alerta.Visible = false;
        //    //    alertaErr.Visible = true;
        //    //    return;
        //    //}

        //    Repara_Fecha(TxtFecha.Text);

        //    btnCancelaLote_Click(sender, e);

        //    string SQL = "INSERT INTO ZENTRADA (TIPO_FORM, FECHA, TIPO_PLANTA, VARIEDAD, LOTE, UNIDADES, NUM_UNIDADES, MANOJOS, ";
        //    SQL += "DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS, OBSERVACIONES, OK, LOTEDESTINO) ";
        //    SQL += " VALUES ('" + TxtForm.Text + "','" + TxtFecha.Text + "','" + TxtCampo.Text + "','" + TxtVariedad.Text + "','" + txtQRCode.Text + "','" + TxtCajas.Text + "',";
        //    SQL += "'" + TxtPlantas.Text + "','" + TxtManojos.Text + "','" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + TxtETDesde.Text + "','" + TxtETHasta.Text + "',";
        //    SQL += "'" + TxtTuneles.Text + "','" + TxtPasillos.Text + "','" + TxtObservaciones.Text + "','" + TxtOK.Text + "','" +  TxtLoteDestino.Text + "')";

        //    DBHelper.ExecuteNonQuery(SQL);
        //    //Btfin.Visible = false;
        //    //BTerminado.Visible = true;
        //    btnGenerate_Click(sender, e);
        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        btnGeneraTodoPerf_Click(sender, e);
        //    }
        //    else
        //    {
        //        btnGenerateTodo_Click(sender, e);
        //    }
        //    //btnGenerateTodo_Click(sender, e);
        //    alerta.Visible = false;
        //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //}
        protected void BTfin_Click(object sender, EventArgs e)
        {

        }

        protected void btnGeneraTodoPerf_Click(object sender, EventArgs e)
        {
            string code = this.Session["CodeQR"].ToString();
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    //code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
            //    //LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
            //    code = TxtVariedad.Text + Environment.NewLine;
            //    LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
            //}
            //else
            //{
            //    code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
            //    LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
            //    code += Label4.Text + TxtCampo.Text + Environment.NewLine;
            //    LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
            //    code += Label5.Text + TxtFecha.Text + Environment.NewLine;
            //    code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
            //    LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
            //    code += LbCajasS.Text + Environment.NewLine;
            //    code += LbPlantasS.Text + Environment.NewLine;
            //}

            H1Normal.Visible = false;
            H1Seleccion.Visible = false;
            H1Red.Visible = false;
            H1Green.Visible = true;
            //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR PROCESADO";
            //panelPrinter.Attributes["style"] = "color: LimeGreen; font-weight:bold;"; 
            DrPrinters_Click();

            TextAlertaErr.Text = "";

            //string code = TxtVariedad.Text;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

            code = code.Replace("\r\n", "");
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                imgBarCode.Height = 100;// Convert.ToInt32(TxAltoT.Text);
                imgBarCode.Width = 100;// Convert.ToInt32(TxAnchoT.Text);
            }
            catch (Exception a)
            {
                TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                //TxAltoT.Text = "200";
                //TxAnchoT.Text = "200";
                alertaErr.Visible = true;
            }

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
                        barcodeBitmap.Save(memory, ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                        imgBarCode.Visible = true;
                    }
                }
            }
            if (this.Session["SelectQR"].ToString() == "0")
            {
                PlaceHolderOrdenCompraMin.Controls.Add(imgBarCode);
                PlaceHolderOrdenCompraMin.Visible = true;
            }
            else
            {
                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    PlaceHolderLoteBingB.Controls.Add(imgBarCode);
                    PlaceHolderLoteBingB.Visible = true;
                }
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    PlaceHolderQRLote.Controls.Add(imgBarCode);
                    PlaceHolderQRLote.Visible = true;
                }
                if (DrPrinters.SelectedItem.Value == "3")
                {
                    //PlaceHolderFitoLotes.Controls.Add(imgBarCode);
                    //PlaceHolderFitoLotes.Visible = true;
                    //panelContentsFTLotes.Visible = true;
                }
                if (DrPrinters.SelectedItem.Value == "6")
                {
                    PlaceHolderPaletAlvMinLote.Controls.Add(imgBarCode);
                    PlaceHolderPaletAlvMinLote.Visible = true;
                }




                //PlaceHolderPaletAlvMinLote.Controls.Add(imgBarCode);
                //PlaceHolderPaletAlvMinLote.Visible = true;
                //PlaceHolderPaletAlvMin
            }

            this.Session["QR"] = code;
            ReadQRCode();
        }


        //protected void btnGeneraTodoPerf_Click(object sender, EventArgs e)
        //{
        //    string code = "";
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;
        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        //code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
        //        //LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
        //        code = TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadLS.Text = Label6.Text + " " + TxtVariedad.Text;
        //    }
        //    else
        //    {
        //        code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
        //        LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
        //        code += Label4.Text + TxtCampo.Text + Environment.NewLine;
        //        LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
        //        code += Label5.Text + TxtFecha.Text + Environment.NewLine;
        //        code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadLS.Text = Label6.Text + " " + TxtVariedad.Text;
        //        code += LbCajasS.Text + Environment.NewLine;
        //        code += LbPlantasS.Text + Environment.NewLine;
        //    }

        //    H1Normal.Visible = false;
        //    H1Seleccion.Visible = false;
        //    H1Red.Visible = false;
        //    H1Green.Visible = true;
        //    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR PROCESADO";
        //    //panelPrinter.Attributes["style"] = "color: LimeGreen; font-weight:bold;"; 
        //    DrPrinters_Click();

        //    TextAlertaErr.Text = "";

        //    //string code = TxtVariedad.Text;

        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

        //    var writer = new BarcodeWriter();
        //    writer.Format = BarcodeFormat.QR_CODE;
        //    var result = writer.Write(code);
        //    var barcodeBitmap = new Bitmap(result);

        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

        //    try
        //    {
        //        imgBarCode.Height = 100;// Convert.ToInt32(TxAltoT.Text);
        //        imgBarCode.Width = 100;// Convert.ToInt32(TxAnchoT.Text);
        //    }
        //    catch (Exception a)
        //    {
        //        TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
        //        //TxAltoT.Text = "200";
        //        //TxAnchoT.Text = "200";
        //        alertaErr.Visible = true;
        //    }

        //    if (this.Session["SelectQR"].ToString() == "0")
        //    {
        //        using (Bitmap bitMap = qrCode.GetGraphic(40))
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] byteImage = ms.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //            }
        //        }
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40);
        //    }
        //    else
        //    {
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;
        //        using (MemoryStream memory = new MemoryStream())
        //        {
        //            using (Bitmap bitMap = barcodeBitmap)
        //            {
        //                barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] bytes = memory.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
        //                imgBarCode.Visible = true;
        //            }
        //        }
        //    }

        //    PlaceHolder2.Controls.Clear();
        //    PlaceHolderLoteBingB.Controls.Clear();

        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        PlaceHolderLoteBingB.Controls.Add(imgBarCode);
        //    }
        //    else
        //    {
        //        PlaceHolder2.Controls.Add(imgBarCode);
        //    }

        //    this.Session["QR"] = code;
        //    ReadQRCode();
        //}

        //protected void btnPorcesa_Click(object sender, EventArgs e)
        //{
        //    this.Session["IDCabecera"] = "0";
        //    DataTable dbA = null;
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    btnPrint2.Visible = false;
        //    btnPrintPaletAlv.Visible = false;
        //    //string AA = "";
        //    //string CC = "";
        //    //string BB = "";
        //    //string DD = "";
        //    //string EE = "";
        //    //string FF = "";
        //    //string Unidad_Base = "";

        //    //BTerminado.Visible = false;
        //    //Btfin.Visible = false;
        //    btProcesa.Visible = false;
        //    btPorcesa.Visible = false;
        //    alertaLog.Visible = false;
        //    btNew.Enabled = false;

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];


        //    SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //    DataTable dbB = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow fila in dbB.Rows)
        //    {
        //        btNew.Enabled = true;
        //        break;
        //    }

        //    try
        //    {
        //        //Boolean Esta = false;
        //        SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //        dbA = Main.BuscaLote(SQL).Tables[0];

        //        CargaDescripcionLote(dbA);

        //    }
        //    catch (Exception ex)
        //    {
        //        //Lberror.Text = ex.Message;
        //        string a = Main.Ficherotraza("btnPorcesa_Click --> " + ex.Message + " = " + SQL);
        //    }


        //    SQL = "SELECT COUNT(ZLOTE) as CUANTOS FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //    DataTable dbM = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow fila in dbM.Rows)
        //    {
        //        if (fila["CUANTOS"].ToString() != "0")
        //        {
        //            //Si existe en ZLOTESCREADOS no aumenta numero de secuencia
        //            Esta = true;
        //        }
        //        else
        //        {
        //            //si no existe inserto, subo numero de secuencia y actualizo
        //            SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
        //            SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
        //            DBHelper.ExecuteNonQuery(SQL);

        //            SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //            DataTable dbC = Main.BuscaLote(SQL).Tables[0];
        //            foreach (DataRow fila2 in dbC.Rows)
        //            {
        //                this.Session["NumeroSecuencia"] = fila2["ZSECUENCIA"].ToString();
        //                break;
        //            }

        //            int JJ = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
        //            SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + JJ + "' ";
        //            SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //            DBHelper.ExecuteNonQuery(SQL);
        //            //Esta = true;
        //            btNew.Enabled = true;
        //        }
        //        break;
        //    }

        //    //Vuelvo a buscar por si han escaneado antes de hacer el LOTE
        //    SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //    dbA = Main.BuscaLote(SQL).Tables[0];

        //    if (Esta == false)
        //    {
        //        //CargaDescripcionLote(dbA);
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            Esta = true;
        //            break;
        //        }

        //        btnGenerateLote_Click(sender, e);
        //        //if (DrPrinters.SelectedItem.Value == "4")
        //        //{
        //        //    btnGeneraTodoPerf_Click(sender, e);
        //        //}
        //        //else
        //        //{
        //        //    btnGenerateTodo_Click(sender, e);
        //        //}

        //        //btnGenerate_Click(sender, e);
        //        //btnGenerateZXING_Click(sender, e);
        //        if (Esta == false)
        //        {
        //            TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
        //            TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
        //            TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
        //            TextAlerta.Text = "";
        //            alertaLog.Visible = false;
        //            alerta.Visible = false;
        //            alertaErr.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = true;

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;
        //        }
        //        else
        //        {
        //            TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //            TextAlertaErr.Text = "";
        //            alerta.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                btnPrintPaletAlv.Visible = true;
        //            }
        //            else
        //            {
        //                btnPrint2.Visible = true;
        //            }
        //        }


        //        //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //        //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";
        //    }
        //    else
        //    {
        //        Esta = false;
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            Esta = true;
        //            break;
        //        }

        //        btnGenerateLote_Click(sender, e);
        //        //antes estado = 1
        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        if (Esta == false)
        //        {
        //            TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
        //            TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
        //            TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
        //            TextAlerta.Text = "";
        //            alertaLog.Visible = false;
        //            alerta.Visible = false;
        //            alertaErr.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = true;

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;
        //        }
        //        else
        //        {
        //            TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //            TextAlertaErr.Text = "";
        //            alerta.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                btnPrintPaletAlv.Visible = true;
        //            }
        //            else
        //            {
        //                btnPrint2.Visible = true;
        //            }
        //        }
        //    }
        //    DrPrinters_Click();
        //    Carga_Lotes(DrVariedad.SelectedItem.Value);
        //    Carga_LotesScaneados(DrVariedad.SelectedItem.Value);
        //}

        protected void btnPorcesa_Click(object sender, EventArgs e)
        {
            DataTable dbA = null;
            alerta.Visible = false;
            alertaErr.Visible = false;
            btnPrint2.Visible = false;
            btnPrintPaletAlv.Visible = false;
            //string AA = "";
            //string CC = "";
            //string BB = "";
            //string DD = "";
            //string EE = "";
            //string FF = "";
            //string Unidad_Base = "";

            //BTerminado.Visible = false;
            //Btfin.Visible = false;
            btProcesa.Visible = false;
            btPorcesa.Visible = false;
            alertaLog.Visible = false;
            btNew.Enabled = false;

            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];


            SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "' ";
            DataTable dbB = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dbB.Rows)
            {
                btNew.Enabled = true;
                break;
            }

            try
            {
                //Boolean Esta = false;
                SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
                dbA = Main.BuscaLote(SQL).Tables[0];

                CargaDescripcionLote(dbA);

            }
            catch (Exception ex)
            {
                //Lberror.Text = ex.Message;
                string a = Main.Ficherotraza("btnPorcesa_Click --> " + ex.Message + " = " + SQL);
            }

            SQL = "SELECT COUNT(ZLOTE) as CUANTOS FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "'";
            DataTable dbM = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dbM.Rows)
            {
                if (fila["CUANTOS"].ToString() != "0")
                {
                    //Si existe en ZLOTESCREADOS no aumenta numero de secuencia
                    Esta = true;
                }
                else
                {
                    //si no existe inserto, subo numero de secuencia y actualizo
                    SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
                    SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
                    DataTable dbC = Main.BuscaLote(SQL).Tables[0];
                    foreach (DataRow fila2 in dbC.Rows)
                    {
                        this.Session["NumeroSecuencia"] = fila2["ZSECUENCIA"].ToString();
                        break;
                    }

                    int JJ = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
                    SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + JJ + "' ";
                    SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
                    DBHelper.ExecuteNonQuery(SQL);
                    //Esta = true;
                    btNew.Enabled = true;
                }
                break;
            }

            //Vuelvo a buscar por si han escaneado antes de hacer el LOTE
            SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
            dbA = Main.BuscaLote(SQL).Tables[0];

            if (Esta == false)
            {
                //CargaDescripcionLote(dbA);
                foreach (DataRow filas in dbA.Rows)
                {
                    Esta = true;
                    break;
                }

                btnGenerateLote_Click(sender, e);
                //if (DrPrinters.SelectedItem.Value == "4")
                //{
                //    btnGeneraTodoPerf_Click(sender, e);
                //}
                //else
                //{
                //    btnGenerateTodo_Click(sender, e);
                //}

                //btnGenerate_Click(sender, e);
                //btnGenerateZXING_Click(sender, e);
                if (Esta == false)
                {
                    TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
                    TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
                    TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
                    TextAlerta.Text = "";
                    alertaLog.Visible = false;
                    alerta.Visible = false;
                    alertaErr.Visible = true;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = true;

                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = true;
                    H1Green.Visible = false;
                }
                else
                {
                    TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
                    TextAlertaErr.Text = "";
                    alerta.Visible = true;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        btnPrintPaletAlv.Visible = true;
                    }
                    else
                    {
                        btnPrint2.Visible = true;
                    }
                }

                //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";
            }
            else
            {
                Esta = false;
                foreach (DataRow filas in dbA.Rows)
                {
                    Esta = true;
                    break;
                }

                btnGenerateLote_Click(sender, e);
                //antes estado = 1

                if (Esta == false)
                {
                    TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
                    TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
                    TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
                    TextAlerta.Text = "";
                    alertaLog.Visible = false;
                    alerta.Visible = false;
                    alertaErr.Visible = true;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = true;

                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = true;
                    H1Green.Visible = false;
                }
                else
                {
                    SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";
                    DBHelper.ExecuteNonQuery(SQL);

                    TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
                    TextAlertaErr.Text = "";
                    alerta.Visible = true;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        btnPrintPaletAlv.Visible = true;
                    }
                    else
                    {
                        btnPrint2.Visible = true;
                    }
                }

            }
            DrPrinters_Click();
            //Carga_Lotes(DrVariedad.SelectedItem.Value);
            //Carga_LotesScaneados(DrVariedad.SelectedItem.Value);

            if (BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }
        }



        //protected void btnPorcesa_Click(object sender, EventArgs e)
        //{
        //    DataTable dbA = null;
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    btnPrint2.Visible = false;
        //    btnPrintPaletAlv.Visible = false;
        //    btProcesa.Visible = false;
        //    btPorcesa.Visible = false;
        //    alertaLog.Visible = false;
        //    btNew.Enabled = false;

        //    //Modificación 12/04/2022
        //    //Puede estar en ZENTRADA o NO
        //    //Puede estar en ZLOTESCREADOS o NO, ATENCION: Desarrollo pasar secuencias por llevar id creados en ZLOTESCREADOS

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];

        //    //Ahora miro en ZENTRADA
        //    SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //    dbA = Main.BuscaLote(SQL).Tables[0];

        //    //Busca en Lotes creados por si viene de PRODUCCION
        //    SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //    DataTable dbB = Main.BuscaLote(SQL).Tables[0];
        //    //Si no existe en ZLOTESCREADOS, lo creo
        //    if (dbB.Rows.Count == 0)
        //    {
        //        //no existe ZLOTESCREADOS inserto, subo numero de secuencia y actualizo
        //        SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
        //        SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
        //        DBHelper.ExecuteNonQuery(SQL);

        //        SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //        DataTable dbC = Main.BuscaLote(SQL).Tables[0];
        //        foreach (DataRow fila2 in dbC.Rows)
        //        {
        //            this.Session["NumeroSecuencia"] = fila2["ZSECUENCIA"].ToString();
        //            break;
        //        }

        //        int JJ = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
        //        SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + JJ + "' ";
        //        SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //        DBHelper.ExecuteNonQuery(SQL);

        //        btNew.Enabled = true;
        //        //Ahora miro en ZENTRADA
        //        SQL = "SELECT COUNT(LOTE) AS CUANTOS FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //        dbA = Main.BuscaLote(SQL).Tables[0];

        //        foreach (DataRow fila in dbA.Rows)
        //        {
        //            if (fila["CUANTOS"].ToString() != "0")
        //            {
        //                //Si existe en ZENTRADA, cambio a 0
        //                SQL = "UPDATE ZENTRADA SET ESTADO = 0 WHERE LOTE = '" + txtQRCode.Text + "' ";
        //                DBHelper.ExecuteNonQuery(SQL);
        //                //LOtescreados a -1 porque esta en Entrada
        //                SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //                DBHelper.ExecuteNonQuery(SQL);

        //                SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //                dbA = Main.BuscaLote(SQL).Tables[0];

        //                CargaDescripcionLote(dbA);
        //                Esta = true;
        //                break;
        //            }
        //            else
        //            {
        //                //Si no existe
        //                //consulta general para ZENTRADA
        //                SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //                dbA = Main.BuscaLote(SQL).Tables[0];
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //Si existe en ZLOTESCREADOS
        //        foreach (DataRow fila in dbB.Rows)
        //        {
        //            //Existe. 
        //            btNew.Enabled = true;
        //            break;
        //        }
        //    }

        //    //Si no existe en ZENTRADA
        //    if (Esta == false)
        //    {
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            Esta = true;
        //            break;
        //        }

        //        btnGenerateLote_Click(sender, e); 
        //        if (Esta == false)
        //        {
        //            TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
        //            TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
        //            TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
        //            TextAlerta.Text = "";
        //            alertaLog.Visible = false;
        //            alerta.Visible = false;
        //            alertaErr.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = true;

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;
        //        }
        //        else
        //        {
        //            TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //            TextAlertaErr.Text = "";
        //            alerta.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                btnPrintPaletAlv.Visible = true;
        //            }
        //            else
        //            {
        //                btnPrint2.Visible = true;
        //            }
        //        }

        //        //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //        //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";
        //    }
        //    else
        //    {//Si existe en ZENTRADA
        //        Esta = false;
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            Esta = true;
        //            break;
        //        }

        //        btnGenerateLote_Click(sender, e);
        //        //antes estado = 1
        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";

        //        DBHelper.ExecuteNonQuery(SQL);
        //        if (Esta == false)
        //        {
        //            TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
        //            TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
        //            TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
        //            TextAlerta.Text = "";
        //            alertaLog.Visible = false;
        //            alerta.Visible = false;
        //            alertaErr.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = true;

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;
        //        }
        //        else
        //        {
        //            TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //            TextAlertaErr.Text = "";
        //            alerta.Visible = true;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                btnPrintPaletAlv.Visible = true;
        //            }
        //            else
        //            {
        //                btnPrint2.Visible = true;
        //            }
        //        }

        //    }
        //    DrPrinters_Click();
        //    Carga_Lotes(DrVariedad.SelectedItem.Value);
        //    Carga_LotesScaneados(DrVariedad.SelectedItem.Value);
        //}


        protected void btnGeneraNew_Click(object sender, EventArgs e)
        {
            btnGenerateLote_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodoLote_Click(sender, e);
            }


            //btnGenerate_Click(sender, e);
            //btnGenerateZXING_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}
            //btnGenerateTodo_Click(sender, e);
        }

        protected void BtMasCodeQR_Click(object sender, EventArgs e)
        {

            if (this.Session["Cerrados"].ToString() == "0")
            {
                LimpiaCajas();
                HtmlGenericControl Ia = (HtmlGenericControl)IManual;
                //if (Permisos == "1") { BtMasCodeManu.Visible = true; } else { BtMasCodeManu.Visible = false; }
                BtMasCodeManu.Visible = true;
                if (Ia.Attributes["class"] == "fa fa-plus-square fa-2x")
                {
                    txtQRCodeManu.Text = "";
                    Ia.Attributes["title"] = "Completa la casilla 'Lote' y pulsa aquí para procesarlo manualmente";
                    BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                    Ia.Attributes["class"] = "fa fa-check fa-2x";
                    //txtQRCodeManu.Attributes["style"] = "background-color: #e9f5ef;";
                    txtQRCodeManu.Visible = true;
                    txtQRCode.Visible = false;
                    LbQR.Text = "Listas para la generación de QR en modo manual";
                    alerta.Visible = false;
                    LbSecuenciaLoteLotes.Text = "";
                    if (BodyQR.Visible == true)
                    {
                        this.Session["IDCabecera"] = "0";
                        MontaEtiquetaOrdenCompra();
                    }
                }
                else
                {
                    if (txtQRCodeManu.Text == "")
                    {
                        alerta.Visible = true;
                        TextAlerta.Text = "Introduzca un código de lote Válido.";
                        btProcesa.Visible = false;
                        btnPrint2.Visible = false;
                        return;
                    }
                    alerta.Visible = false;
                    alertaLog.Visible = false;
                    alertaErr.Visible = false;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    //btNew.Enabled = false;
                    Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
                    Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
                    BodyLote.Attributes["style"] = "background-color: white;";
                    LbQR.Text = "Listas para la generación de QR en modo automático";
                    txtQRCode.Text = txtQRCodeManu.Text;
                    txtQRCodeManu.Visible = false;
                    txtQRCode.Visible = true;
                    LbSecuenciaLoteLotes.Text = txtQRCode.Text;
                    LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";

                    string SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
                    SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
                    DBHelper.ExecuteNonQuery(SQL);

                    btnGenerateLote_Click(sender, e);
                    if (BodyQR.Visible == true)
                    {
                        this.Session["IDCabecera"] = "0";
                        MontaEtiquetaOrdenCompra();
                    }
                    //if (DrPrinters.SelectedItem.Value == "4")
                    //{
                    //    btnGeneraTodoPerf_Click(sender, e);
                    //}
                    //else
                    //{
                    //    btnGenerateTodo_Click(sender, e);
                    //}

                    //btnGenerate_Click(sender, e);
                    //btnGenerateZXING_Click(sender, e);

                    ////if (DrPrinters.SelectedItem.Value == "4")
                    ////{
                    ////    btnGeneraTodoPerf_Click(sender, e);
                    ////}
                    ////else
                    ////{
                    ////    btnGenerateTodo_Click(sender, e);
                    ////}
                }

                DrLotes.SelectedIndex = -1;
                DrScaneados.SelectedIndex = -1;
                //Posiciona_Permiso();
            }
            else
            {
                //if (Permisos == "1") { BtMasCodeManu.Visible = true; } else { BtMasCodeManu.Visible = false; }
                Lbmensaje.Text = "No se permite generar Lotes manualmente cuando se están visualizando Lotes Procesados. Elimine antes esta selección.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();

                if (BodyQR.Visible == true)
                {
                    this.Session["IDCabecera"] = "0";
                    MontaEtiquetaOrdenCompra();
                }
                return;
                //BtMasCodeManu.Visible = false;
                //Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
                //BodyLote.Attributes["style"] = "background-color: white;";
                //LbQR.Text = "Listas para la generación de QR en modo automático";
                //Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
                //txtQRCodeManu.Visible = false;
                //txtQRCode.Visible = true;
                //txtQRCodeManu.Text = "";
                //LbSecuenciaLote.Text = "";
            }
        }

        private void Posiciona_Permiso()
        {
            HtmlGenericControl Ia = (HtmlGenericControl)IManual;
            Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
            BodyLote.Attributes["style"] = "background-color: white;";
            LbQR.Text = "Listas para la generación de QR en modo automático";
            Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
            txtQRCodeManu.Visible = false;
            txtQRCode.Visible = true;
            txtQRCodeManu.Text = "";

            if (this.Session["Cerrados"].ToString() == "0")
            {
                //DrTransportista.Visible = false;
                LbQR.Text = "Listas para la generación de QR";
                BodyLote.Attributes.Add("style", "background-color: white;");
                BodyCampos.Attributes.Add("style", "background-color: white;");
                BodyLotes.Attributes.Add("style", "background-color: white;");
                BodyAll.Attributes.Add("style", "background-color: white;");
                PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                //BtModifica.Enabled = true;
                //btNew.Enabled = false;
                //BtGuardaLote.Enabled = false;
                //BtCancelaLote.Enabled = false;
                //BtDelete.Enabled = true;
                Habilita_Boton(0);

                txtQRCodeManu.Text = "";
            }
            else
            {
                BtMasCodeManu.Visible = false;
                LbQR.Text = "Búsqueda de códigos QR para modificar";
                BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
                PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                //BtModifica.Enabled = false;
                //btNew.Enabled = false;
                //BtGuardaLote.Enabled = false;
                //BtCancelaLote.Enabled = false;
                //BtDelete.Enabled = false;
                Habilita_Boton(0);
                txtQRCodeManu.Text = "";
            }
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            ImageButton btn = (ImageButton)sender;
            if (btn.ID == "Imageprocesa1")
            {
                lbFiltro.Visible = false;
                dvFiltro.Visible = false;
                ImgFiltro.Visible = false;
                dvFiltro.InnerText = "";
                Imageprocesa1.Visible = false;
                Imageprocesa2.Visible = true;
                this.Session["Cerrados"] = "0";

                    //BtMasCodeManu.Visible = false;
                    DrVariedad_SelectedIndexChanged(null, null);
                if (Permisos == "1") { BtMasCodeManu.Visible = true; } else { BtMasCodeManu.Visible = false; }
                LbQR.Text = "Listas para la generación de QR";
                BodyLote.Attributes.Add("style", "background-color: white;");
                BodyCampos.Attributes.Add("style", "background-color: white;");
                BodyLotes.Attributes.Add("style", "background-color: white;");
                BodyAll.Attributes.Add("style", "background-color: white;");
                PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                //BtModifica.Enabled = true;
                Habilita_Boton(1);
                txtQRCodeManu.Text = "";
                //BodyAll.Attributes.Add("style", "background-color: white;");
            }
            else
            {
                lbFiltro.Visible = true;
                dvFiltro.Visible = true;
                ImgFiltro.Visible = true;
                this.Session["Cerrados"] = "1";
                Imageprocesa1.Visible = true;
                Imageprocesa2.Visible = false;
                DrVariedad_SelectedIndexChanged(null, null);
                //if (Permisos == "1") { BtMasCodeManu.Visible = true; } else { BtMasCodeManu.Visible = false; }
                BtMasCodeManu.Visible = false;
                LbQR.Text = "Búsqueda de códigos QR para modificar";
                BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
                PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                //BtModifica.Enabled = false;
                Habilita_Boton(0);
                txtQRCodeManu.Text = "";
                //BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
            }
        }

        protected void btnAutoGeneradoLotes_Click(object sender, EventArgs e)
        {
            if (txtQRCode.Text == "") { return; }
            if (this.Session["Procesa"].ToString() == "0")
            {
                H1Normal.Visible = false;
                H1Seleccion.Visible = true;
                H1Red.Visible = false;
                H1Green.Visible = false;
                //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado";
                //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";
                DrPrinters_Click();

                //HLoteProceso.InnerText = "Código QR seleccionado";
                //HLoteProceso.Attributes.Add("style", "color: black; font-weight:bold;");

                Lbcompleto.Text = "";
                alerta.Visible = false;
                alertaErr.Visible = false;

                if (this.Session["Cerrados"].ToString() == "0")
                {
                    TextAlerta.Text = "Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                    alerta.Visible = true;
                    alertaLog.Visible = false;
                    btnPrint2.Visible = false;
                    btnPrintPaletAlv.Visible = false;
                    btProcesa.Visible = true;
                    btPorcesa.Visible = false;
                }
                //BTerminado.Visible = false;
            }

            LbSecuenciaLoteLotes.Text = txtQRCode.Text;
            LbSecuenciaLoteQRLotes.Text = txtQRCode.Text;
            LbCodeQRPalteAlvLotes.Text = txtQRCode.Text;


            //ajustar a  LM Q H deberá ser desde configuración en tabla
            string code = txtQRCode.Text;// + " ";
            LbSecuenciaLoteLotes.Text = code;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
            try
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    imgBarCode.Height = 200; //Convert.ToInt32(TxAlto.Text);
                    imgBarCode.Width = 200; //Convert.ToInt32(TxAncho.Text);
                }
            }
            catch (Exception a)
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
                    //TxAlto.Text = "300";
                    //TxAncho.Text = "300";
                    alertaErr.Visible = true;
                }
            }
            //try
            //{
            //    imgBarCode.Height = Convert.ToInt32(TxAlto.Text);
            //    imgBarCode.Width = Convert.ToInt32(TxAncho.Text);
            //}
            //catch (Exception a)
            //{
            //    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
            //    TxAlto.Text = "300";
            //    TxAncho.Text = "300";
            //    alertaErr.Visible = true;
            //}

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
                ImgQRCodeA1Lotes.Visible = true;
                ImgQRCodeA2Lotes.Visible = false;
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
                ImgQRCodeA1Lotes.Visible = false;
                ImgQRCodeA2Lotes.Visible = true;
            }

            //PlaceHolder1.Controls.Add(imgBarCode);
            PlaceHolderFitoLotes.Controls.Clear();
            PlaceHolderLoteBing.Controls.Clear();
            PlaceHolderQRLote.Controls.Clear();
            PlaceHolderPaletAlvLote.Controls.Clear();

            PlaceHolderLoteBing.Visible = false;
            PlaceHolderQRLote.Visible = false;
            PlaceHolderPaletAlvLote.Visible = false;

            panelContentsFTLotes.Visible = false;
            panelContentsLotes.Visible = false;
            panelContentsQRLotes.Visible = false;
            panelContentsPaletAlvLotes.Visible = false;

            if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            {
                PlaceHolderLoteBing.Controls.Add(imgBarCode);
                PlaceHolderLoteBing.Visible = true;
                panelContentsLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "2")
            {
                PlaceHolderQRLote.Controls.Add(imgBarCode);
                PlaceHolderQRLote.Visible = true;
                panelContentsQRLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "3")
            {
                PlaceHolderFitoLotes.Controls.Add(imgBarCode);
                PlaceHolderFitoLotes.Visible = true;
                panelContentsFTLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "6")
            {
                PlaceHolderPaletAlvLote.Controls.Add(imgBarCode);
                PlaceHolderPaletAlvLote.Visible = true;
                panelContentsPaletAlvLotes.Visible = true;
            }
            //Aqui verifica si la imagen es correcta
            //imgBarCode

            LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";
            this.Session["QR"] = code;
            ReadQRCode();

            //Comentar en produccion
            //btnGenerateTodo_Click(sender, e);
        }


        protected void btnGenerateLote_Click(object sender, EventArgs e)
        {
            if (txtQRCode.Text == "") { return; }
            if (this.Session["Procesa"].ToString() == "0")
            {
                H1Normal.Visible = false;
                H1Seleccion.Visible = true;
                H1Red.Visible = false;
                H1Green.Visible = false;
                //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado";
                //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";
                DrPrinters_Click();

                //HLoteProceso.InnerText = "Código QR seleccionado";
                //HLoteProceso.Attributes.Add("style", "color: black; font-weight:bold;");

                Lbcompleto.Text = "";
                alerta.Visible = false;
                alertaErr.Visible = false;

                if (this.Session["Cerrados"].ToString() == "0")
                {
                    TextAlerta.Text = "Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                    alerta.Visible = true;
                    alertaLog.Visible = false;
                    btnPrint2.Visible = false;
                    btnPrintPaletAlv.Visible = false;
                    btProcesa.Visible = true;
                    btPorcesa.Visible = false;
                }
                //BTerminado.Visible = false;
            }

            LbSecuenciaLoteLotes.Text = txtQRCode.Text;
            LbSecuenciaLoteQRLotes.Text = txtQRCode.Text;
            LbCodeQRPalteAlvLotes.Text = txtQRCode.Text;

            //LbSecuenciaLote1.Text = txtQRCode.Text;
            //LbSecuenciaLoteQR1.Text = txtQRCode.Text;
            //LbCodeQRPalteAlv1.Text = txtQRCode.Text;

            //ajustar a  LM Q H deberá ser desde configuración en tabla
            string code = txtQRCode.Text.Trim();// + " ";
            LbSecuenciaLoteLotes.Text = code;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);

            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(code);
            var barcodeBitmap = new Bitmap(result);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
            try
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    imgBarCode.Height = 200; //Convert.ToInt32(TxAlto.Text);
                    imgBarCode.Width = 200; //Convert.ToInt32(TxAncho.Text);
                }
            }
            catch (Exception a)
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
                    //TxAlto.Text = "300";
                    //TxAncho.Text = "300";
                    alertaErr.Visible = true;
                }
            }
            //try
            //{
            //    imgBarCode.Height = Convert.ToInt32(TxAlto.Text);
            //    imgBarCode.Width = Convert.ToInt32(TxAncho.Text);
            //}
            //catch (Exception a)
            //{
            //    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
            //    TxAlto.Text = "300";
            //    TxAncho.Text = "300";
            //    alertaErr.Visible = true;
            //}

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
                ImgQRCodeA1Lotes.Visible = true;
                ImgQRCodeA2Lotes.Visible = false;
            }
            else
            {
                this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;
                using (MemoryStream memory = new MemoryStream())
                {
                    using (Bitmap bitMap = barcodeBitmap)
                    {
                        barcodeBitmap.Save(memory, ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                        imgBarCode.Visible = true;
                    }
                }
                ImgQRCodeA1Lotes.Visible = false;
                ImgQRCodeA2Lotes.Visible = true;
            }

            //PlaceHolderOrdenCompra.Controls.Add(imgBarCode);

            PlaceHolderLoteBing.Controls.Clear();
            PlaceHolderQRLote.Controls.Clear();
            PlaceHolderPaletAlvLote.Controls.Clear();
            PlaceHolderFitoLotes.Controls.Clear();

            PlaceHolderLoteBing.Visible = false;
            PlaceHolderQRLote.Visible = false;
            PlaceHolderPaletAlvLote.Visible = false;
            PlaceHolderFitoLotes.Visible = false;

            panelContentsFTLotes.Visible = false;
            panelContentsLotes.Visible = false;
            panelContentsQRLotes.Visible = false;
            panelContentsPaletAlvLotes.Visible = false;

            //Aqui va una plantilla única para todos, comentado a cliente fase 3
            if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            {
                PlaceHolderLoteBing.Controls.Add(imgBarCode);
                PlaceHolderLoteBing.Visible = true;
                panelContentsLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "2")
            {
                PlaceHolderQRLote.Controls.Add(imgBarCode);
                PlaceHolderQRLote.Visible = true;
                panelContentsQRLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "3")
            {
                PlaceHolderFitoLotes.Controls.Add(imgBarCode);
                PlaceHolderFitoLotes.Visible = true;
                panelContentsFTLotes.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "6")
            {
                PlaceHolderPaletAlvLote.Controls.Add(imgBarCode);
                PlaceHolderPaletAlvLote.Visible = true;
                panelContentsPaletAlvLotes.Visible = true;
            }
            //Aqui verifica si la imagen es correcta
            //imgBarCode












            LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";
            this.Session["QR"] = code;
            ReadQRCode();

            //Comentar en produccion
            //btnGenerateTodo_Click(sender, e);

        }

        protected void btnGenerateTodoLote_Click(object sender, EventArgs e)
        {
            string code = (this.Session["CodeQR"].ToString().Replace("Variedad: ", "")).Trim();
            string CodigoError = "";
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //Implementado 25/03/2022 
            //Campo condición actualmente no contiene Campo o sector este el 7
            //Crear tabla validación para restricciones en las secuencias o formularios
            //DataTable dtCampos = this.Session["Campos"] as DataTable;

            Object Con = DBHelper.ExecuteScalarSQL("SELECT ZMANUAL FROM  ZSECUENCIAS  WHERE ZID ='" + DrVariedad.SelectedItem.Value + "'", null);

            //if (Con is System.DBNull)
            if (Con == null)
            {
                LbCodePaletAlvLotes.Text = "";
                Lbmensaje.Text = "No se encuentra valor para ZMANUAL (0,1) en la Tabla ZSECUENCIAS para completar valores en el código QR según selección de Lotes.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                return;
            }
            else
            {
                //if (TxtDesde.Text == "")
                //{
                //    if(Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Campo o Sector,";
                //    }                    
                //}
                //else
                //{
                //    code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
                //    LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
                //}

                //if (TxtCampo.Text == "")
                //{
                //    if (Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Tipo Plantas,";
                //    }
                //}
                //else
                //{
                //    code += Label4.Text + TxtCampo.Text + Environment.NewLine;
                //    LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
                //}

                //if (TxtFecha.Text == "")
                //{
                //    if (Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Fecha Corte,";
                //    }
                //}
                //else
                //{
                //    code += Label5.Text + TxtFecha.Text + Environment.NewLine;
                //    LbFechaS.Text = Label5.Text + " " + TxtFecha.Text.ToString().Substring(0, 10);
                //}

                //if (TxtVariedad.Text == "")
                //{
                //    if (Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Variedad,";
                //    }
                //}
                //else
                //{
                //    code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
                //    LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
                //}

                //if (TxtCajas.Text == "")
                //{
                //    if (Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Número Cajas,";
                //    }
                //}
                //else
                //{
                //    //code += Label7.Text + TxtCajas.Text + Environment.NewLine;
                //    //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
                //    code += LbCajasS.Text + Environment.NewLine;
                //    //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
                //}

                //if (TxtPlantas.Text == "")
                //{
                //    if (Con.ToString() == "0")
                //    {
                //        LbCampoS.Text = "";
                //        LbFechaS.Text = "";
                //        LbVariedadS.Text = "";
                //        LbCajasS.Text = "";
                //        LbPlantasS.Text = "";
                //        LbPlantaS.Text = "";
                //        CodigoError += " Número Plantas,";
                //    }
                //}
                //else
                //{
                //    code += LbPlantasS.Text + Environment.NewLine;
                //    //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
                //    //code += LbnumeroPlantas.Text + TxtPlantas.Text + Environment.NewLine;
                //    //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
                //}

                //if (DrPrinters.SelectedItem.Value == "6")
                //{
                //    code = TxtVariedad.Text; //; + Environment.NewLine;
                //}

                if (CodigoError != "")
                {
                    //-----------------------------------------------------
                    //No quieren que muestre la ventana de error con los campos vacios
                    //-----------------------------------------------------
                    //No se puede generar el código QR total por tener los campos siguientes vacios: CodigoError
                    //if (this.Session["SelectPrinter"].ToString() == "1")
                    //{
                    //    LbSecuenciaLote.Text = txtQRCode.Text;
                    //}
                    //else
                    //{
                    //Para volver a poner esta ventana de error, Descomentar desde aqui
                    //------------------------------
                    //TextAlertaErr.Text = "No se puede generar el código QR total por tener los campos siguientes vacios: " + CodigoError;
                    //TextAlertaErr.Text += "Genere un registro desde formularios de Scan-IT desde el Móvil, envielo y pruebe nuevamente desde este botón. " + CodigoError;
                    //TextAlerta.Text = "";
                    //alertaErr.Visible = true;
                    //btnPrint2.Visible = false;
                    ////BTerminado.Visible = false;
                    //btProcesa.Visible = true;
                    //---------------------------------
                    return;
                    //}
                }
                else
                {
                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = false;
                    H1Green.Visible = true;

                    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR PROCESADO";
                    //panelPrinter.Attributes["style"] = "color: LimeGreen; font-weight:bold;";

                    //HLoteProceso.InnerText = "Código QR PROCESADO";
                    //HLoteProceso.Attributes.Add("style", "color: LimeGreen; font-weight:bold;");
                    if (this.Session["Cerrados"].ToString() == "0")
                    {
                        TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
                        TextAlertaErr.Text = "";
                        alerta.Visible = true;
                    }
                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        btnPrintPaletAlv.Visible = true;
                    }
                    else
                    {
                        btnPrint2.Visible = true;
                    }

                    btProcesa.Visible = false;
                    //BTerminado.Visible = true;
                }
                if (code == "") { return; }
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                var result = writer.Write(code);
                var barcodeBitmap = new Bitmap(result);

                System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

                try
                {
                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        imgBarCode.Height = 100;
                        imgBarCode.Width = 100;
                    }
                    //else
                    //{
                    //    imgBarCode.Height = Convert.ToInt32(TxAltoT.Text);
                    //    imgBarCode.Width = Convert.ToInt32(TxAnchoT.Text);
                    //}
                }
                catch (Exception a)
                {
                    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                    //TxAltoT.Text = "200";
                    //TxAnchoT.Text = "200";
                    alertaErr.Visible = true;
                }


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
                            barcodeBitmap.Save(memory, ImageFormat.Png);
                            byte[] bytes = memory.ToArray();
                            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                            imgBarCode.Visible = true;
                        }
                    }
                }

                if (DrPrinters.SelectedItem.Value == "6")
                {

                    PlaceHolderPaletAlvMinLote.Controls.Add(imgBarCode);
                    PlaceHolderPaletAlvMinLote.Visible = true;
                }
                else
                {
                    //PlaceHolderOrdenCompraMin.Controls.Add(imgBarCode);
                }
                this.Session["QR"] = code;
                ReadQRCode();
            }
        }


        //protected void btnGenerate_Click(object sender, EventArgs e) Anterior
        //{
        //    if (txtQRCode.Text == "") { return; }
        //    if (this.Session["Procesa"].ToString() == "0")
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = true;
        //        H1Red.Visible = false;
        //        H1Green.Visible = false;
        //        //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado";
        //        //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";
        //        DrPrinters_Click();

        //        //HLoteProceso.InnerText = "Código QR seleccionado";
        //        //HLoteProceso.Attributes.Add("style", "color: black; font-weight:bold;");

        //        Lbcompleto.Text = "";
        //        alerta.Visible = false;
        //        alertaErr.Visible = false;

        //        if (this.Session["Cerrados"].ToString() == "0")
        //        {
        //            TextAlerta.Text = "Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
        //            alerta.Visible = true;
        //            alertaLog.Visible = false;
        //            btnPrint2.Visible = false;
        //            btnPrintPaletAlv.Visible = false;
        //            btProcesa.Visible = true;
        //            btPorcesa.Visible = false;
        //        }
        //        //BTerminado.Visible = false;
        //    }

        //    LbSecuenciaLote1.Text = txtQRCode.Text;
        //    LbSecuenciaLoteQR1.Text = txtQRCode.Text;
        //    LbCodeQRPalteAlv1.Text = txtQRCode.Text;


        //    //ajustar a  LM Q H deberá ser desde configuración en tabla
        //    string code = txtQRCode.Text;// + " ";
        //    LbSecuenciaLote1.Text = code;

        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);

        //    var writer = new BarcodeWriter();
        //    writer.Format = BarcodeFormat.QR_CODE;
        //    var result = writer.Write(code);
        //    var barcodeBitmap = new Bitmap(result);

        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //    //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
        //    try
        //    {
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            imgBarCode.Height = 150;
        //            imgBarCode.Width = 150;
        //        }
        //        else if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 200; // 250;
        //            imgBarCode.Width = 200; // 250;
        //        }
        //        else
        //        {
        //            imgBarCode.Height = 200; //Convert.ToInt32(TxAlto.Text);
        //            imgBarCode.Width = 200; //Convert.ToInt32(TxAncho.Text);
        //        }
        //    }
        //    catch (Exception a)
        //    {
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            imgBarCode.Height = 150;
        //            imgBarCode.Width = 150;
        //        }
        //        else if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 200; // 250;
        //            imgBarCode.Width = 200; // 250;
        //        }
        //        else
        //        {
        //            TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
        //            //TxAlto.Text = "300";
        //            //TxAncho.Text = "300";
        //            alertaErr.Visible = true;
        //        }
        //    }
        //    //try
        //    //{
        //    //    imgBarCode.Height = Convert.ToInt32(TxAlto.Text);
        //    //    imgBarCode.Width = Convert.ToInt32(TxAncho.Text);
        //    //}
        //    //catch (Exception a)
        //    //{
        //    //    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
        //    //    TxAlto.Text = "300";
        //    //    TxAncho.Text = "300";
        //    //    alertaErr.Visible = true;
        //    //}

        //    //Selección de uno u otro generador de QR
        //    if (this.Session["SelectQR"].ToString() == "0")
        //    {
        //        using (Bitmap bitMap = qrCode.GetGraphic(40))
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] byteImage = ms.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //            }
        //        }
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40);
        //        ImgQRCodeA1.Visible = true;
        //        ImgQRCodeA2.Visible = false;
        //    }
        //    else
        //    {
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;
        //        using (MemoryStream memory = new MemoryStream())
        //        {
        //            using (Bitmap bitMap = barcodeBitmap)
        //            {
        //                barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] bytes = memory.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
        //                imgBarCode.Visible = true;
        //            }
        //        }
        //        ImgQRCodeA1.Visible = false;
        //        ImgQRCodeA2.Visible = true;
        //    }

        //    //PlaceHolderOrdenCompra.Controls.Add(imgBarCode);
        //    PlaceHolderOrdenCompra1.Visible = false;
        //    PlaceHolderQRLote.Visible = false;
        //    panelContentsFTLotes.Visible = false;
        //    PlaceHolderPaletAlvLote.Visible = false;
        //    //Limpio los controles
        //    PlaceHolderOrdenCompra1.Controls.Clear();
        //    PlaceHolderQRLote.Controls.Clear();
        //    panelContentsFTLotes.Controls.Clear();
        //    PlaceHolderPaletAlvLote.Controls.Clear();

        //    if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
        //    {
        //        PlaceHolderOrdenCompra1.Controls.Add(imgBarCode);
        //        PlaceHolderOrdenCompra1.Visible = true;
        //    }
        //    if (DrPrinters.SelectedItem.Value == "2")
        //    {
        //        PlaceHolderQRLote.Controls.Add(imgBarCode);
        //        PlaceHolderQRLote.Visible = true;
        //    }
        //    if (DrPrinters.SelectedItem.Value == "3")
        //    {
        //        panelContentsFTLotes.Controls.Add(imgBarCode);
        //        panelContentsFTLotes.Visible = true;
        //    }
        //    if (DrPrinters.SelectedItem.Value == "6")
        //    {
        //        PlaceHolderPaletAlvLote.Controls.Add(imgBarCode);
        //        PlaceHolderPaletAlvLote.Visible = true;
        //    }
        //    //Aqui verifica si la imagen es correcta
        //    //imgBarCode

        //    LbCodigoLote1.Text = "CÓDIGO LOTE:";
        //    this.Session["QR"] = code;
        //    ReadQRCode();

        //    //Comentar en produccion
        //    //btnGenerateTodo_Click(sender, e);
        //    if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //    {
        //        MontaEtiqueta();
        //    }
        //}

        //protected void btnGenerateTodo_Click(object sender, EventArgs e)
        //{
        //    string code = "";
        //    string CodigoError = "";
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

        //    if (TxtDesde.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Campo o Sector,";
        //    }
        //    else
        //    {
        //        code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
        //        LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
        //    }

        //    if (TxtCampo.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Tipo Plantas,";
        //    }
        //    else
        //    {
        //        code += Label4.Text + TxtCampo.Text + Environment.NewLine;
        //        LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
        //    }

        //    if (TxtFecha.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Fecha Corte,";
        //    }
        //    else
        //    {
        //        code += Label5.Text + TxtFecha.Text + Environment.NewLine;
        //        LbFechaS.Text = Label5.Text + " " + TxtFecha.Text.ToString().Substring(0, 10);
        //    }

        //    if (TxtVariedad.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Variedad,";
        //    }
        //    else
        //    {
        //        code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
        //    }

        //    if (TxtCajas.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Número Cajas,";
        //    }
        //    else
        //    {
        //        //code += Label7.Text + TxtCajas.Text + Environment.NewLine;
        //        //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
        //        code += LbCajasS.Text + Environment.NewLine;
        //        //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
        //    }

        //    if (TxtPlantas.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbVariedadS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        LbPlantaS.Text = "";
        //        CodigoError += " Número Plantas,";
        //    }
        //    else
        //    {
        //        code += LbPlantasS.Text + Environment.NewLine;
        //        //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
        //        //code += LbnumeroPlantas.Text + TxtPlantas.Text + Environment.NewLine;
        //        //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
        //    }

        //    if (DrPrinters.SelectedItem.Value == "6")
        //    {
        //        code = TxtVariedad.Text; //; + Environment.NewLine;
        //    }

        //    if (CodigoError != "")
        //    {
        //        //-----------------------------------------------------
        //        //No quieren que muestre la ventana de error con los campos vacios
        //        //-----------------------------------------------------
        //        //No se puede generar el código QR total por tener los campos siguientes vacios: CodigoError
        //        //if (this.Session["SelectPrinter"].ToString() == "1")
        //        //{
        //        //    LbSecuenciaLote.Text = txtQRCode.Text;
        //        //}
        //        //else
        //        //{
        //        //Para volver a poner esta ventana de error, Descomentar desde aqui
        //        //------------------------------
        //        //TextAlertaErr.Text = "No se puede generar el código QR total por tener los campos siguientes vacios: " + CodigoError;
        //        //TextAlertaErr.Text += "Genere un registro desde formularios de Scan-IT desde el Móvil, envielo y pruebe nuevamente desde este botón. " + CodigoError;
        //        //TextAlerta.Text = "";
        //        //alertaErr.Visible = true;
        //        //btnPrint2.Visible = false;
        //        ////BTerminado.Visible = false;
        //        //btProcesa.Visible = true;
        //        //---------------------------------
        //        btnGenerate_Click(null,null);
        //        return;
        //        //}
        //    }
        //    else
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = false;
        //        H1Red.Visible = false;
        //        H1Green.Visible = true;

        //        //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR PROCESADO";
        //        //panelPrinter.Attributes["style"] = "color: LimeGreen; font-weight:bold;";

        //        //HLoteProceso.InnerText = "Código QR PROCESADO";
        //        //HLoteProceso.Attributes.Add("style", "color: LimeGreen; font-weight:bold;");
        //        if (this.Session["Cerrados"].ToString() == "0")
        //        {
        //            TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //            TextAlertaErr.Text = "";
        //            alerta.Visible = true;
        //            btProcesa.Visible = false;
        //        }
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            btnPrintPaletAlv.Visible = true;
        //        }
        //        else
        //        {
        //            btnPrint2.Visible = true;
        //        }

        //        //BTerminado.Visible = true;
        //    }

        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);

        //    var writer = new BarcodeWriter();
        //    writer.Format = BarcodeFormat.QR_CODE;
        //    var result = writer.Write(code);
        //    var barcodeBitmap = new Bitmap(result);

        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

        //    try
        //    {
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 100;
        //            imgBarCode.Width = 100;
        //        }
        //        //else
        //        //{
        //        //    imgBarCode.Height = Convert.ToInt32(TxAltoT.Text);
        //        //    imgBarCode.Width = Convert.ToInt32(TxAnchoT.Text);
        //        //}
        //    }
        //    catch (Exception a)
        //    {
        //        TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
        //        //TxAltoT.Text = "200";
        //        //TxAnchoT.Text = "200";
        //        alertaErr.Visible = true;
        //    }


        //    if (this.Session["SelectQR"].ToString() == "0")
        //    {
        //        using (Bitmap bitMap = qrCode.GetGraphic(40))
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] byteImage = ms.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //            }
        //        }
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40);
        //    }
        //    else
        //    {
        //        this.Session["CodigoQR"] = qrCode.GetGraphic(40); //barcodeBitmap;

        //        using (MemoryStream memory = new MemoryStream())
        //        {
        //            using (Bitmap bitMap = barcodeBitmap)
        //            {
        //                barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        //                byte[] bytes = memory.ToArray();
        //                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
        //                imgBarCode.Visible = true;
        //            }
        //        }
        //    }

        //    PlaceHolderPaletAlvMinLote.Controls.Clear();
        //    PlaceHolderLoteBingB.Controls.Clear();

        //    if (DrPrinters.SelectedItem.Value == "6")
        //    {

        //        PlaceHolderPaletAlvMinLote.Controls.Add(imgBarCode);
        //    }
        //    else
        //    {
        //        PlaceHolderLoteBingB.Controls.Add(imgBarCode);
        //    }
        //    this.Session["QR"] = code;
        //    //if (BodyQR.Visible == true)
        //    //{
        //    //    MontaEtiqueta();
        //    //}
        //    ReadQRCode();
        //}

        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    if (this.Session["Procesa"].ToString() == "0")
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = true;
        //        H1Red.Visible = false;
        //        H1Green.Visible = false;
        //        if (DrPrinters.SelectedItem.Value == "1")
        //        {
        //            btnPrintA2.Visible = true;
        //        }
        //        else
        //        {
        //            btnPrintB2.Visible = true;
        //        }

        //        //HLoteProceso.InnerText = "Código QR seleccionado";
        //        //HLoteProceso.Attributes.Add("style", "color: black; font-weight:bold;");

        //        Lbcompleto.Text = "";
        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        //TextAlerta.Text = "Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
        //        //alerta.Visible = true;
        //        alertaLog.Visible = false;
        //        btnPrint2.Visible = false;
        //        //btProcesa.Visible = true;
        //        //btPorcesa.Visible = false;
        //        //BTerminado.Visible = false;
        //    }

        //    string code = txtQRCode.Text;
        //    LbSecuenciaLote.Text = code;
        //    LbSecuenciaLoteQR.Text = code;
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //    try
        //    {
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            imgBarCode.Height = 150;
        //            imgBarCode.Width = 150;
        //        }
        //        else
        //        {
        //            imgBarCode.Height = Convert.ToInt32(TxAlto.Text);
        //            imgBarCode.Width = Convert.ToInt32(TxAncho.Text);
        //        }
        //    }
        //    catch (Exception a)
        //    {
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            imgBarCode.Height = 150;
        //            imgBarCode.Width = 150;
        //        }
        //        else
        //        {
        //            TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
        //            TxAlto.Text = "300";
        //            TxAncho.Text = "300";
        //            alertaErr.Visible = true;
        //        }
        //    }
        //    using (Bitmap bitMap = qrCode.GetGraphic(20))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "1")
        //        {
        //            PlaceHolderOrdenCompra.Controls.Add(imgBarCode);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            PlaceHolderQR.Controls.Add(imgBarCode);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "3")
        //        {
        //            panelContentsFTOrdenCompra.Controls.Add(imgBarCode);
        //        }
        //    }

        //    LbCodigoLote.Text = "CÓDIGO LOTE:";
        //    //LbCodigoLoteQR.Text = "CÓDIGO LOTE 2:";
        //    //Comentar en produccion
        //    //btnGenerateTodo_Click(sender, e);
        //}

        //protected void btnGenerateTodo_Click(object sender, EventArgs e)
        //{
        //    string code = "";
        //    string CodigoError = "";
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

        //    if (TxtDesde.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";
        //        CodigoError += " Campo o Sector,";
        //    }
        //    else
        //    {
        //        code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
        //        LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
        //        //LbCampoSQR.Text = LbDesde.Text + " " + TxtDesde.Text;
        //    }

        //    if (TxtCampo.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";
        //        CodigoError += " Tipo Plantas,";
        //    }
        //    else
        //    {
        //        code += Label4.Text + TxtCampo.Text + Environment.NewLine;
        //        LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
        //        //LbPlantaSQR.Text = Label4.Text + " " + TxtCampo.Text;
        //    }

        //    if (TxtFecha.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";
        //        CodigoError += " Fecha Corte,";
        //    }
        //    else
        //    {
        //        code += Label5.Text + TxtFecha.Text + Environment.NewLine;
        //        //LbFechaS.Text = Label5.Text + " " + TxtFecha.Text;


        //        if (TxtFecha.Text != "")
        //        {
        //            LbFechaS.Text = Label5.Text + " " + TxtFecha.Text.ToString().Substring(0, 10);
        //        }
        //        else
        //        {
        //            LbFechaS.Text = Label5.Text + " " + TxtFecha.Text;
        //        }
        //    }

        //    if (TxtVariedad.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";
        //        CodigoError += " Variedad,";
        //    }
        //    else
        //    {
        //        code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
        //    }

        //    if (TxtCajas.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";

        //        CodigoError += " Número Cajas,";
        //    }
        //    else
        //    {
        //        //code += Label7.Text + TxtCajas.Text + Environment.NewLine;
        //        //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
        //        code += LbCajasS.Text + Environment.NewLine;
        //        //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
        //    }

        //    if (TxtPlantas.Text == "")
        //    {
        //        LbCampoS.Text = "";
        //        //LbCampoSQR.Text = "";
        //        LbFechaS.Text = "";
        //        //LbFechaSQR.Text = "";
        //        LbVariedadS.Text = "";
        //        //LbVariedadSQR.Text = "";
        //        LbCajasS.Text = "";
        //        //LbCajasSQR.Text = "";
        //        LbPlantasS.Text = "";
        //        //LbPlantasSQR.Text = "";
        //        LbPlantaS.Text = "";
        //        //LbPlantaSQR.Text = "";
        //        CodigoError += " Número Plantas,";
        //    }
        //    else
        //    {
        //        code += LbPlantasS.Text + Environment.NewLine;
        //        //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
        //        //code += LbnumeroPlantas.Text + TxtPlantas.Text + Environment.NewLine;
        //        //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
        //    }

        //    if (CodigoError != "")
        //    {
        //        //No se puede generar el código QR total por tener los campos siguientes vacios: CodigoError
        //        TextAlertaErr.Text = "No se puede generar el código QR total por tener los campos siguientes vacios: " + CodigoError;
        //        TextAlertaErr.Text += "Genere un registro desde formularios de Scan-IT desde el Móvil, envielo y pruebe nuevamente desde este botón. " + CodigoError;
        //        TextAlerta.Text = "";
        //        alertaErr.Visible = true;
        //        btnPrint2.Visible = false;
        //        //BTerminado.Visible = false;
        //        //btProcesa.Visible = true;
        //        return;
        //    }
        //    else
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = false;
        //        H1Red.Visible = false;
        //        H1Green.Visible = true;
        //        DrPrinters_Click();

        //        //HLoteProceso.InnerText = "Código QR PROCESADO";
        //        //HLoteProceso.Attributes.Add("style", "color: LimeGreen; font-weight:bold;");

        //        //TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
        //        TextAlertaErr.Text = "";
        //        //alerta.Visible = true;
        //        //btnPrint2.Visible = true;
        //        //btProcesa.Visible = false;
        //        //BTerminado.Visible = true;
        //    }
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

        //    try
        //    {
        //        imgBarCode.Height = Convert.ToInt32(TxAltoT.Text);
        //        imgBarCode.Width = Convert.ToInt32(TxAnchoT.Text);
        //    }
        //    catch (Exception a)
        //    {
        //        TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
        //        TxAltoT.Text = "200";
        //        TxAnchoT.Text = "200";
        //        alertaErr.Visible = true;
        //    }

        //    using (Bitmap bitMap = qrCode.GetGraphic(20))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }

        //        //if (DrPrinters.SelectedItem.Value == "1")
        //        //{
        //        PlaceHolder2.Controls.Add(imgBarCode);
        //        //}
        //        //if (DrPrinters.SelectedItem.Value == "2")
        //        //{
        //        //    PlaceHolderQR.Controls.Add(imgBarCode);
        //        //}
        //        //if (DrPrinters.SelectedItem.Value == "3")
        //        //{
        //        //    panelContentsFTOrdenCompra.Controls.Add(imgBarCode);
        //        //}
        //    }
        //}

        private void BuscaDuplicados()
        {

        }

        private void LimpiaCajas()
        {
            try
            {
                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N;
                    ContentPlaceHolder cont = new ContentPlaceHolder();
                    cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                    HtmlGenericControl DivRegistro = (HtmlGenericControl)cont.FindControl(MiContent);
                    if (DivRegistro.Visible == true)
                    {
                        string DivTextoA = "TxL" + N;
                        TextBox DivLabel = (TextBox)cont.FindControl(DivTextoA);

                        if (DivLabel.Visible == true)
                        {
                            DivLabel.Text = "";
                        }
                        else
                        {
                            string ComboA = "DrL" + N;
                            DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                            DivComboA.Text = "";
                        }

                        DivTextoA = "TxD" + N;
                        DivLabel = (TextBox)cont.FindControl(DivTextoA);

                        if (DivLabel.Visible == true)
                        {
                            DivLabel.Text = "";
                        }
                        else
                        {
                            string ComboA = "DrR" + N;
                            DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                            DivComboA.Text = "";
                        }
                    }
                }

                LbVariedadPLotes.Text = "";
                LbCampoSLotes.Text = "";
                LbFechaSLotes.Text = "";
                LbPlantaSLotes.Text = "";
                LbVariedadSLotes.Text = "";
                LbCajasSLotes.Text = "";
                LbPlantasSLotes.Text = "";
                Lbcompleto.Text = "";
                //LbSecuenciaLoteOrdenCompra.Text = "";
                //LbCodePaletAlvOrdenCompra.Text = "";
                //LbCodeQRPalteAlvOrdenCompra.Text = "";
                LbTipoPlantaPLotes.Text = "";
                //LbVariedadPOrdenCompra.Text = "";
                LbVariedadSLotes.Text = "";
                lbUnidadesPLotes.Text = "";
                lbNumPlantasPLotes.Text = "";
                TxtID.Text = "";
                LbCodigoLoteOrdenCompra.Text = "SIN CÓDIGO LOTE";//"CÓDIGO LOTE:";//

            }
            catch (NullReferenceException ex)
            {
                //Lberror.Text += ex.Message;
                string a = Main.Ficherotraza("Limpia Cajas-->" + ex.Message);
                //alertaErr.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }
        }

        //private void LimpiaCajas()
        //{
        //    try
        //    {
        //        txtQRCode.Text = "";
        //        TxtCampo.Text = "";
        //        TxtFecha.Text = "";
        //        TxtVariedad.Text = "";
        //        TxtCajas.Text = "";
        //        TxtPlantas.Text = "";
        //        LbCampoS.Text = "";
        //        LbFechaS.Text = "";
        //        LbPlantaS.Text = "";
        //        LbVariedadLS.Text = "";
        //        LbCajasS.Text = "";
        //        LbPlantasS.Text = "";
        //        Lbcompleto.Text = "";
        //        LbSecuenciaLote.Text = "";

        //        LbCodePaletAlv1.Text = "";
        //        LbCodeQRPalteAlv1.Text = "";
        //        LbTipoPlantaP.Text = "";
        //        LbVariedadP1.Text = "";
        //        LbVariedadP.Text = "";
        //        LbVariedadS.Text = "";
        //        lbUnidadesP.Text = "";
        //        lbNumPlantasP.Text = "";

        //        TxtEstado.Text = "";
        //        TxtDispositivo.Text = "";
        //        TxtLoteDestino.Text = "";

        //        TxtID.Text = "";
        //        TxtForm.Text = "";
        //        TxtManojos.Text = "";
        //        TxtDesde.Text = "";
        //        TxtHasta.Text = "";
        //        TxtETDesde.Text = "";
        //        TxtETHasta.Text = "";
        //        TxtTuneles.Text = "";
        //        TxtPasillos.Text = "";
        //        TxtObservaciones.Text = "";
        //        TxtOK.Text = "";
        //        LbCodigoLote1.Text = "SIN CÓDIGO LOTE";


        //        LbEmpresaS.Text = "";
        //        LbProveedorS.Text = "";
        //        lbSeriePedidoS.Text = "";
        //        lbNumPedidoS.Text = "";
        //        LbLineaPedidoS.Text = "";
        //        LbProductoS.Text = "";
        //        LbProductoS.Text = "";


        //        LbEmpresaP.Text = "";
        //        LbProveedorP.Text = "";
        //        lbSeriePedidoP.Text = "";
        //        lbNumPedidoP.Text = "";
        //        LbLineaPedidoP.Text = "";
        //        LbProductoP.Text = "";
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        //Lberror.Text += ex.Message;
        //        string a = Main.Ficherotraza("Limpia Cajas-->" + ex.Message);
        //        //alertaErr.Visible = true;
        //        //TextAlertaErr.Text = ex.Message;
        //    }
        //}

        private void Oculta_Datos(int Ve)
        {
            try
            {
                if (Ve == 0)
                {
                    LimpiaCajas();
                }
                else
                {
                    if (TxL0.Visible == true && TxL0.ReadOnly == false)
                    {
                        BtDelete.Visible = true;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                string a = Main.Ficherotraza("Limpia Cajas-->" + ex.Message);
            }
        }

        //private void Oculta_Datos(int Ve)
        //{
        //    try
        //    {
        //        if (Ve == 0)
        //        {
        //            LimpiaCajas();
        //            //BTerminado.Visible = false;
        //            //Btfin.Visible = false;
        //        }
        //        else
        //        {

        //            if (TxtForm.Enabled == true)
        //            {
        //                //btnNuevoLote.Visible = true;
        //                BtDelete.Visible = true;
        //                //BtModifica.Visible = true;
        //            }
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        //Lberror.Text += ex.Message;
        //        string a = Main.Ficherotraza("Limpia Cajas-->" + ex.Message);
        //        //alertaErr.Visible = true;
        //        //TextAlertaErr.Text = ex.Message;

        //    }
        //}

        protected void DrDuplicados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BuscaLote(string SQL)

        }
        //protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    TextAlerta.Text = "";
        //    TextAlertaErr.Text = "";
        //    alerta.Visible = false;
        //    btProcesa.Visible = false;
        //    btPorcesa.Visible = false;

        //    this.Session["IDCabecera"] = "0";
        //    CancelLote_Click(null, null);

        //    HtmlGenericControl Ia = (HtmlGenericControl)IManual;
        //    BtMasCodeManu.Visible = false;
        //    Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
        //    BodyLote.Attributes["style"] = "background-color: white;";
        //    LbQR.Text = "Listas para la generación de QR en modo automático";
        //    Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
        //    txtQRCodeManu.Visible = false;
        //    txtQRCode.Visible = true;
        //    txtQRCodeManu.Text = "";


        //    Oculta_Datos(0);
        //    btNew.Enabled = true;
        //    try
        //    {

        //        DataTable dt = Main.CargaSecuencia().Tables[0];


        //        Esta = false;
        //        foreach (DataRow fila in dt.Rows)
        //        {
        //            if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //            {
        //                this.Session["IDSecuencia"] = fila["ZID"].ToString();

        //                Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZMANUAL) FROM ZSECUENCIAS  WHERE ZID =" + fila["ZID"].ToString() + " ", null);

        //                if (Con == null)
        //                {
        //                    Permisos = "";
        //                }
        //                else
        //                {
        //                    Permisos = Con.ToString();
        //                }


        //                if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
        //                {
        //                    //lbBuscaCod.Text = "Códigos QR:";
        //                    btNew.Enabled = false;
        //                    Esta = false;
        //                    LbCodigoLote1.Text = "SIN CÓDIGO LOTE";
        //                }
        //                else
        //                {
        //                    //lbBuscaCod.Text = "Códigos QR con Tipo Lote " + fila["ZDESCRIPCION"].ToString() + ":";
        //                    //lbBuscaCod.Text = "Códigos QR:";
        //                    H1Normal.Visible = false;
        //                    H1Seleccion.Visible = true;
        //                    H1Red.Visible = false;
        //                    H1Green.Visible = false;
        //                    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintPanel();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
        //                    //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";

        //                    Carga_Impresoras(fila["ZID"].ToString());
        //                    DrPrinters_Click();

        //                    GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
        //                    //this.Session["IDCabecera"] = "1";
        //                    btNew.Enabled = false;
        //                    LbCodigoLote1.Text = "CÓDIGO LOTE:";
        //                    Esta = true;
        //                    Carga_Lotes(fila["ZID"].ToString());
        //                    Carga_LotesScaneados(fila["ZID"].ToString());

        //                    if (Permisos == "1") 
        //                    { 
        //                        BtMasCodeManu.Visible = true;
        //                        if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //                        {
        //                            MontaEtiqueta();
        //                        }
        //                    }
        //                }
        //                //Carga_Impresoras(fila["ZID"].ToString());
        //                break;
        //            }
        //        }
        //        if (Esta == false)
        //        {
        //            btNew.Enabled = false;
        //            //BTerminado.Visible = false;
        //            LbSecuenciaLote.Text = "";
        //            LbCodigoLote1.Text = "SIN CÓDIGO LOTE";
        //            txtQRCode.Text = "";
        //            alerta.Visible = false;
        //            alertaErr.Visible = false;
        //            alertaLog.Visible = false;
        //            LbCampoS.Text = "";
        //            LbFechaS.Text = "";
        //            LbPlantaS.Text = "";
        //            LbVariedadS.Text = "";
        //            LbCajasS.Text = "";
        //            LbPlantasS.Text = "";
        //            TxtCampo.Text = "";
        //            TxtFecha.Text = "";
        //            TxtVariedad.Text = "";
        //            TxtCajas.Text = "";
        //            TxtLoteDestino.Text = "";
        //            TxtPlantas.Text = "";
        //            DrLotes.Items.Clear();
        //            LbDuplicados.Text = "No";
        //            LbDuplicados.ForeColor = Color.Black;
        //            TxtEstado.Text = "";
        //            TxtDispositivo.Text = "";

        //            H1Normal.Visible = true;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = false;
        //            H1Green.Visible = false;

        //            this.Session["IDCabecera"] = "0";
        //            if (BodyQR.Visible == true) // && this.Session["IDCabecera"].ToString() == "0")
        //            {
        //                MontaEtiqueta();
        //            }

        //            //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i>  Seleccione un Lote";
        //            //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";
        //            //Carga_Impresoras(fila["ZID"].ToString());
        //            //DrPrinters_Click();
        //            //lbBuscaCod.Text = "Códigos QR:";
        //        }

        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        //Lberror.Text += ex.Message;
        //        string a = Main.Ficherotraza("DVariedad_SelectedIndexChanged-->" + ex.Message);
        //        //alerta.Visible = true;
        //        //TextAlertaErr.Text = ex.Message;
        //    }
        //    Posiciona_Permiso();
        //    //DrPrinters_Click();
        //}
        private void Actualiza_Lotes(string ID)
        {
            string SQL = "SELECT B.ZLOTE AS LOTE, A.ID, B.ZID,  A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO, B.ZESTADO ";
            SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
            SQL += " WHERE B.ZID_SECUENCIA = " + ID;
            SQL += " AND A.LOTE = B.ZLOTE ";
            SQL += " AND A.ESTADO IN(1,2) ";
            SQL += " AND B.ZESTADO = 0 ";
            DataTable dbB = Main.BuscaLote(SQL).Tables[0];

            foreach (DataRow filas in dbB.Rows)
            {
                SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZID = " + filas["ZID"].ToString();
                DBHelper.ExecuteNonQuery(SQL);
            }
        }

        protected void btnDeleteTabla_Click(object sender, EventArgs e)
        {
            //Elimina de ZLOTESCREADOS el Lote
            string SQL = "DELETE FROM ZLOTESCREADOS WHERE ZID = '" + LbIDLote.Text + "' ";
            DBHelper.ExecuteNonQuery(SQL);
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            LimpiaCajas();

            TextAlertaErr.Text = "";
            alertaErr.Visible = false;
            alerta.Visible = false;

            //pnlContents.Visible = true;
            //pnlContentsQR.Visible = false;
            //pnlContentsFT.Visible = false;
            //pnlContentsPaletAlv.Visible = false;
            txtQRCode.Text = "";
        }

        protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["CancelaConsulta"] = "DRVariedad";
            TextAlerta.Text = "";
            TextAlertaErr.Text = "";
            alerta.Visible = false;
            btProcesa.Visible = false;
            btProcesa.Visible = false;
            btPorcesa.Visible = false;
            LimpiaCajas();


            if (DrPrinters.SelectedItem.Value == "6")
            {
                btnPrintPaletAlv.Visible = true;
            }
            else
            {
                btnPrint2.Visible = true;
            }
            this.Session["IDLote"] = "0";
            btnCancelaLote_Click(null, null);
            //Dupplicados
            //SELECT LOTE, COUNT(*) as total 
            //FROM ZENTRADA
            //GROUP BY LOTE
            //HAVING COUNT(*) > 1;

            //SELECT LOTE, COUNT(*) as total
            //FROM ZENTRADA
            //GROUP BY LOTE
            //HAVING COUNT(*) > 1;

            //SELECT TIPO_FORM, LOTE, COUNT(*) as total
            //FROM ZENTRADA
            //GROUP BY LOTE, TIPO_FORM
            //HAVING COUNT(*) > 1;

            //SELECT*
            //FROM ZENTRADA
            //WHERE LOTE = '21P0623-001'
            HtmlGenericControl Ia = (HtmlGenericControl)IManual;
            BtMasCodeManu.Visible = false;
            Ia.Attributes["class"] = "fa fa-plus-square fa-2x";
            BodyLote.Attributes["style"] = "background-color: white;";
            LbQR.Text = "Listas para la generación de QR en modo automático";
            Ia.Attributes["title"] = "Genera puntualmente un código de lote manual para este tipo de lote seleccionado";
            txtQRCodeManu.Visible = false;
            txtQRCode.Visible = true;
            txtQRCodeManu.Text = "";


            Oculta_Datos(0);
            btNew.Enabled = true;
            try
            {

                DataTable dt = Main.CargaSecuencia().Tables[0];

                //DataTable dt = this.Session["Secuencias"] as DataTable;
                //if (dt == null)
                //{
                //    DataTable dt3 = Main.CargaSecuencia().Tables[0];
                //    foreach (DataRow fila in dt3.Rows)
                //    {
                //        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                //        {

                //            if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
                //            {
                //                btNew.Enabled = false;
                //                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
                //            }
                //            else
                //            {
                //                GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
                //                btNew.Enabled = true;
                //                LbCodigoLote.Text = "CÓDIGO LOTE:";
                //                Carga_Lotes();
                //            }
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                Esta = false;
                foreach (DataRow fila in dt.Rows)
                {
                    if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                    {
                        this.Session["IDSecuencia"] = fila["ZID"].ToString();

                        Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZMANUAL) FROM ZSECUENCIAS  WHERE ZID =" + fila["ZID"].ToString() + " ", null);

                        if (Con is null)
                        {
                            Permisos = "";
                        }
                        else
                        {
                            Permisos = Con.ToString();
                        }


                        if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
                        {
                            //lbBuscaCod.Text = "Códigos QR:";
                            btNew.Enabled = false;
                            Esta = false;
                            LbCodigoLoteLotes.Text = "SIN CÓDIGO LOTE";
                        }
                        else
                        {
                            //lbBuscaCod.Text = "Códigos QR con Tipo Lote " + fila["ZDESCRIPCION"].ToString() + ":";
                            //lbBuscaCod.Text = "Códigos QR:";
                            H1Normal.Visible = false;
                            H1Seleccion.Visible = true;
                            H1Red.Visible = false;
                            H1Green.Visible = false;
                            //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Lote con Código QR seleccionado <button id='btnPrintA1' type='button' runat='server' visible='false' class='pull-right text-muted'  onclick='return PrintPanel();'><i title='Imprime la vista previa presentada en pantalla' class='fa fa-print'></i></button>";
                            //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";

                            Carga_Impresoras(fila["ZID"].ToString());
                            DrPrinters_Click();

                            GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
                            btNew.Enabled = false;
                            LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";
                            Esta = true;

                            //ID 23 ZENTRADA
                            //DataTable dt1 = Main.CargaRelacionesArchivos(23).Tables[0];
                            //this.Session["Campos"] = dt1;
                            Actualiza_Lotes(fila["ZID"].ToString());
                            Carga_Lotes(fila["ZID"].ToString());
                            Carga_LotesScaneados(fila["ZID"].ToString());

                            if (Permisos == "1") { BtMasCodeManu.Visible = true; }

                        }
                        //Carga_Impresoras(fila["ZID"].ToString());
                        break;
                    }
                }
                if (Esta == false)
                {
                    btNew.Enabled = false;
                    //BTerminado.Visible = false;
                    LbSecuenciaLoteLotes.Text = "";
                    LbCodigoLoteLotes.Text = "SIN CÓDIGO LOTE";
                    txtQRCode.Text = "";
                    alerta.Visible = false;
                    alertaErr.Visible = false;
                    alertaLog.Visible = false;
                    LbCampoSLotes.Text = "";
                    LbFechaSLotes.Text = "";
                    LbPlantaSLotes.Text = "";
                    LbVariedadSLotes.Text = "";
                    LbCajasSLotes.Text = "";
                    LbPlantasSLotes.Text = "";
                    //TxtCampo.Text = "";
                    //TxtFecha.Text = "";
                    //TxtVariedad.Text = "";
                    //TxtCajas.Text = "";
                    //TxtLoteDestino.Text = "";
                    //TxtPlantas.Text = "";
                    DrLotes.Items.Clear();
                    LbDuplicados.Text = "No";
                    LbDuplicados.ForeColor = Color.Black;
                    //TxtEstado.Text = "";
                    //TxtDispositivo.Text = "";

                    H1Normal.Visible = true;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = false;
                    H1Green.Visible = false;

                    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i>  Seleccione un Lote";
                    //panelPrinter.Attributes["style"] = "color: black; font-weight:bold;";
                    //Carga_Impresoras(fila["ZID"].ToString());
                    //DrPrinters_Click();
                    //lbBuscaCod.Text = "Códigos QR:";
                }

            }
            catch (NullReferenceException ex)
            {
                //Lberror.Text += ex.Message;
                string a = Main.Ficherotraza("DVariedad_SelectedIndexChanged-->" + ex.Message);
                //alerta.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }
            Posiciona_Permiso();
            //DrPrinters_Click();
        }

        protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["IDLote"] = "1";
            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];
            //string Unidad_Base = "";
            LimpiaCajas();
            //Posiciona_Permiso();
            //string SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
            SQL = "SELECT * FROM ZENTRADA WHERE LOTE = '" + DrScaneados.SelectedItem.Text + "' ";
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            this.Session["CancelaConsulta"] = "DrScaneados";
            if (dbA.Rows.Count == 0)
            {
                if (DrScaneados.SelectedItem.Value == "")
                {
                    SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '0' ";
                    //LimpiaCajas();
                    //Posiciona_Permiso();
                    return;
                }
                else
                {
                    SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
                }
                dbA = Main.BuscaLote(SQL).Tables[0];
                this.Session["CancelaConsulta"] = "DrScaneados";
                //Por aqui no calcula
                foreach (DataRow filas in dbA.Rows)
                {
                    LbIDLote.Text = filas["ZID"].ToString();
                    txtQRCode.Text = filas["ZLOTE"].ToString();
                    //txtQRCodebis.Text = filas["ZLOTE"].ToString();
                    TxtID.Text = filas["ZLOTE"].ToString();
                    //TxtFecha.Text = filas["ZFECHA"].ToString();
                    //TxtVariedad.Text = "";
                    //TxtCajas.Text = "";
                    //TxtPlantas.Text = "";
                    LbDateFTLotes.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);
                    LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
                    LbDateQRLotes.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
                    LbDatePaletAlvLotes.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);

                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        LbCodeQRPalteAlvLotes.Text = filas["ZID"].ToString();
                        LbTipoPlantaPLotes.Text = "";
                        LbVariedadPLotes.Text = "";
                        LbVariedadSLotes.Text = "";
                        lbUnidadesPLotes.Text = "";
                        lbNumPlantasPLotes.Text = "";
                    }
                    else
                    {
                        LbCampoSLotes.Text = "";
                        LbFechaSLotes.Text = "";
                        LbVariedadSLotes.Text = "";
                        LbCajasSLotes.Text = "";
                        LbPlantasSLotes.Text = "";
                        Lbcompleto.Text = "";
                        LbPlantaSLotes.Text = "";
                    }
                    //TxtEstado.Text = "";
                    //TxtDispositivo.Text = "";
                    //TxtLoteDestino.Text = "";

                    //TxtForm.Text = "";
                    //TxtManojos.Text = "";
                    //TxtDesde.Text = "";
                    //TxtHasta.Text = "";
                    //TxtETDesde.Text = "";
                    //TxtETHasta.Text = "";
                    //TxtTuneles.Text = "";
                    //TxtPasillos.Text = "";
                    //TxtObservaciones.Text = "";
                    //TxtOK.Text = "";
                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = true;
                    H1Green.Visible = false;
                    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                    //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";
                    DrPrinters_Click();


                    btnGenerateLote_Click(sender, e);


                    //if (DrPrinters.SelectedItem.Value == "4")
                    //{
                    //    btnGeneraTodoPerf_Click(sender, e);
                    //}
                    //else
                    //{
                    //    btnGenerateTodo_Click(sender, e);
                    //}

                    //btnGenerate_Click(sender, e);
                    //btnGenerateZXING_Click(sender, e);

                    //alerta.Visible = false;
                    //alertaErr.Visible = false;
                    //btnPrint2.Visible = false;
                    //btnPrintPaletAlv.Visible = false;

                    //BTerminado.Visible = false;
                    //btProcesa.Visible = false;
                    //btPorcesa.Visible = false;
                    //alertaLog.Visible = false;


                    //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                    //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

                    //Btfin.Visible = false;
                    //BTerminado.Visible = false;
                    break;
                }
            }
            else
            {
                CargaDescripcionLote(dbA);

                //foreach (DataRow filas in dbA.Rows)
                //{
                //    this.Session["IDSecuencia"] = filas["ID"].ToString();
                //    LbIDLote.Text = filas["ID"].ToString();
                //    txtQRCode.Text = filas["LOTE"].ToString();
                //    TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
                //    TxtFecha.Text = filas["FECHA"].ToString();
                //    TxtVariedad.Text = filas["VARIEDAD"].ToString();
                //    TxtCajas.Text = filas["UNIDADES"].ToString();
                //    TxtEstado.Text = filas["ESTADO"].ToString();
                //    TxtDispositivo.Text = filas["DeviceName"].ToString();
                //    TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
                //    LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); // filas["SendTime"].ToString().Substring(10);
                //    LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); // filas["SendTime"].ToString().Substring(10);
                //    LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); // filas["SendTime"].ToString().Substring(10);
                //    LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); // filas["SendTime"].ToString().Substring(10);

                //    if (TxtCajas.Text != "PLANTAS")
                //    {
                //        //LbnumeroPlantas.Text = "Número de Cajas:";
                //        LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
                //        LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

                //    }
                //    else
                //    {
                //        //LbnumeroPlantas.Text = "Número de Plantas:";
                //        LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
                //        LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                //    }

                //    //if (TxtCajas.Text == "CAJAS")
                //    //{
                //    //    //LbnumeroPlantas.Text = "Número de Cajas:";
                //    //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
                //    //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

                //    //}
                //    //if (TxtCajas.Text == "PLANTAS")
                //    //{
                //    //    //LbnumeroPlantas.Text = "Número de Plantas:";
                //    //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
                //    //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                //    //}

                //    try
                //    {
                //        Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND UD_BASE IN NOT NULLL AND UD_BASE <> '' ", null);

                //        if (Con is null)
                //        {
                //            //seguimos como está.
                //            Unidad_Base = "";
                //        }
                //        else
                //        {
                //            Unidad_Base = Con.ToString();
                //        }

                //        foreach (DataRow fila2 in dbP.Rows)
                //        {
                //            if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
                //            {

                //                if (filas["UNIDADES"].ToString() != "PLANTAS")
                //                {
                //                    if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
                //                    {
                //                        if (Unidad_Base != "")
                //                        {
                //                            if (filas["UNIDADES"].ToString() != "Unidad_Base")
                //                            {
                //                                LbPlantasS.Text = "Cantidad: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString() + " " + Unidad_Base;
                //                            }
                //                            else
                //                            {
                //                                LbPlantasS.Text = "";
                //                            }
                //                        }
                //                        else
                //                        {
                //                            LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
                //                        }
                //                    }
                //                    else
                //                    {
                //                        foreach (DataRow fila3 in dbP.Rows)
                //                        {
                //                            if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                //                            {
                //                                int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                //                                LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
                //                                break;
                //                            }
                //                        }
                //                    }
                //                    break;
                //                }
                //                else
                //                {
                //                    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                //                    break;
                //                }

                //                //if (filas["UNIDADES"].ToString() == "PLANTAS")
                //                //{
                //                //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                //                //    break;
                //                //}
                //                //else if (filas["UNIDADES"].ToString() == "CAJAS")
                //                //{
                //                //    if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
                //                //    {
                //                //        LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
                //                //    }
                //                //    else
                //                //    {
                //                //        foreach (DataRow fila3 in dbP.Rows)
                //                //        {
                //                //            if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                //                //            {
                //                //                int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                //                //                LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
                //                //                break;
                //                //            }
                //                //        }
                //                //    }
                //                //    break;
                //                //}
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        //Lberror.Text = ex.Message;
                //        string a = Main.Ficherotraza("DRScaneados_SelectedIndexChanged-->" + ex.Message);
                //    }

                //    TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
                //    LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
                //    LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
                //    LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
                //    LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
                //    //LbCajasS.Text = "Nº CAJAS: " + filas["UNIDADES"].ToString();
                //    //LbPlantasS.Text = "Nº PLANTAS: " + filas["NUM_UNIDADES"].ToString();
                //    Lbcompleto.Text = "QR COMPLETO";

                //    TxtID.Text = filas["ID"].ToString();
                //    TxtForm.Text = filas["TIPO_FORM"].ToString();
                //    TxtManojos.Text = filas["MANOJOS"].ToString();
                //    TxtDesde.Text = filas["DESDE"].ToString();
                //    TxtHasta.Text = filas["HASTA"].ToString();
                //    TxtETDesde.Text = filas["ETDESDE"].ToString();
                //    TxtETHasta.Text = filas["ETHASTA"].ToString();
                //    TxtTuneles.Text = filas["TUNELES"].ToString();
                //    TxtPasillos.Text = filas["PASILLOS"].ToString();
                //    TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
                //    TxtOK.Text = filas["OK"].ToString();

                //    //if (TxtID.Enabled == true)
                //    //{
                //    Oculta_Datos(1);
                //    //}
                //    //else
                //    //{
                //    //    Oculta_Datos(0);
                //    //}

                //    H1Normal.Visible = false;
                //    H1Seleccion.Visible = false;
                //    H1Red.Visible = true;
                //    H1Green.Visible = false;

                //    //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                //    //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";

                //    DrPrinters_Click();
                //    //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                //    //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

                //    alerta.Visible = false;
                //    alertaErr.Visible = false;
                //    alertaLog.Visible = false;

                //    btnGenerate_Click(sender, e);
                //    if (DrPrinters.SelectedItem.Value == "4")
                //    {
                //        btnGeneraTodoPerf_Click(sender, e);
                //    }
                //    else
                //    {
                //        btnGenerateTodo_Click(sender, e);
                //    }


                //    //btnGenerate_Click(sender, e);
                //    //btnGenerateZXING_Click(sender, e);
                //    //if (DrPrinters.SelectedItem.Value == "4")
                //    //{
                //    //    btnGeneraTodoPerf_Click(sender, e);
                //    //}
                //    //else
                //    //{
                //    //    btnGenerateTodo_Click(sender, e);
                //    //}
                //    //btnGenerateTodo_Click(sender, e);
                //    //btnPrint2.Visible = false;
                //    btProcesa.Visible = false;
                //    btPorcesa.Visible = false;
                //    //Btfin.Visible = false;
                //    //BTerminado.Visible = false;
                //    string Miro = filas["ESTADO"].ToString();
                //    //if (filas["ESTADO"].ToString() == "")
                //    //{
                //    //    BTerminado.Visible = true;
                //    //}
                //    //else
                //    //{
                //    //    Btfin.Visible = true;
                //    //}

                //    //SQL = "DELETE FROM ZLOTESCREADOS WHERE ZLOTE = '" + txtQRCode.Text + "'";
                //    SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
                //    DBHelper.ExecuteNonQuery(SQL);
                //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
                //    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
                //    break;
                //}
            }
            DrLotes.SelectedIndex = -1;

            Posiciona_Permiso();
            this.Session["IDLista"] = "Escaneados";
            Habilita_Boton(0);

            if (BodyQR.Visible == true)
            {
                this.Session["IDCabecera"] = "0";
                MontaEtiquetaOrdenCompra();
            }

            //btNew.Enabled = true;

            //BtGuardaLote.Enabled = false;
            //BtModifica.Enabled = false;
            //BtCancelaLote.Enabled = false;
            //BtDelete.Enabled = false;

        }


        //protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.Session["IDCabecera"] = "0";

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];
        //    //string Unidad_Base = "";

        //    //string SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //    SQL = "SELECT * FROM ZENTRADA WHERE LOTE = '" + DrScaneados.SelectedItem.Text + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    if (dbA.Rows.Count == 0)
        //    {
        //        if (DrScaneados.SelectedItem.Value == "")
        //        {
        //            SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '0' ";
        //            LimpiaCajas();
        //            Posiciona_Permiso();
        //            return;
        //        }
        //        else
        //        {
        //            SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //        }
        //        dbA = Main.BuscaLote(SQL).Tables[0];
        //        //Por aqui no calcula
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            LbIDLote.Text = filas["ZID"].ToString();
        //            txtQRCode.Text = filas["ZLOTE"].ToString();
        //            //txtQRCodebis.Text = filas["ZLOTE"].ToString();
        //            TxtID.Text = filas["ZLOTE"].ToString();
        //            TxtFecha.Text = filas["ZFECHA"].ToString();
        //            TxtVariedad.Text = "";
        //            TxtCajas.Text = "";
        //            TxtPlantas.Text = "";
        //            LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);
        //            LbDateContents1.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
        //            LbDateQR1.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
        //            LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);

        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                LbCodeQRPalteAlv1.Text = filas["ZID"].ToString();
        //                LbTipoPlantaP.Text = "";
        //                LbVariedadP1.Text = "";
        //                LbVariedadS.Text = "";
        //                lbUnidadesP.Text = "";
        //                lbNumPlantasP.Text = "";
        //            }
        //            else
        //            {
        //                LbCampoS.Text = "";
        //                LbFechaS.Text = "";
        //                LbVariedadS.Text = "";
        //                LbCajasS.Text = "";
        //                LbPlantasS.Text = "";
        //                Lbcompleto.Text = "";
        //                LbPlantaS.Text = "";
        //            }
        //            TxtEstado.Text = "";
        //            TxtDispositivo.Text = "";
        //            TxtLoteDestino.Text = "";

        //            TxtForm.Text = "";
        //            TxtManojos.Text = "";
        //            TxtDesde.Text = "";
        //            TxtHasta.Text = "";
        //            TxtETDesde.Text = "";
        //            TxtETHasta.Text = "";
        //            TxtTuneles.Text = "";
        //            TxtPasillos.Text = "";
        //            TxtObservaciones.Text = "";
        //            TxtOK.Text = "";
        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;
        //            //panelPrinter.InnerHtml = "<i class='fa fa-long-arrow-right'></i> Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //            //panelPrinter.Attributes["style"] = "color: red; font-weight:bold;";
        //            DrPrinters_Click();

        //            this.Session["IDCabecera"] = "0";
        //            btnGenerate_Click(sender, e);
        //            //if (DrPrinters.SelectedItem.Value == "4")
        //            //{
        //            //    btnGeneraTodoPerf_Click(sender, e);
        //            //}
        //            //else
        //            //{
        //            //    btnGenerateTodo_Click(sender, e);
        //            //}

        //            //btnGenerate_Click(sender, e);
        //            //btnGenerateZXING_Click(sender, e);

        //            //alerta.Visible = false;
        //            //alertaErr.Visible = false;
        //            //btnPrint2.Visible = false;
        //            //btnPrintPaletAlv.Visible = false;

        //            ////BTerminado.Visible = false;
        //            //btProcesa.Visible = false;
        //            //btPorcesa.Visible = false;
        //            //alertaLog.Visible = false;


        //            //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //            //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

        //            //Btfin.Visible = false;
        //            //BTerminado.Visible = false;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        CargaDescripcionLote(dbA);

        //    }
        //    DrLotes.SelectedIndex = -1;

        //    Posiciona_Permiso();
        //    this.Session["IDLista"] = "Escaneados";

        //    //btNew.Enabled = true;
        //    //BtGuardaLote.Enabled = false;
        //    //BtModifica.Enabled = false;
        //    //BtCancelaLote.Enabled = false;
        //    //BtDelete.Enabled = false;

        //}

        //private void CargaModificado(string ID)
        //{
        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];
        //    string AA = "";
        //    string CC = "";
        //    string BB = "";
        //    string DD = "";
        //    string EE = "";
        //    string FF = "";

        //    SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + ID + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow filas in dbA.Rows)//Entrada
        //    {
        //        LbIDLote.Text = filas["ID"].ToString();


        //        txtQRCode.Text = filas["LOTE"].ToString();
        //        //txtQRCodebis.Text = filas["LOTE"].ToString();
        //        TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
        //        TxtFecha.Text = filas["FECHA"].ToString();
        //        TxtVariedad.Text = filas["VARIEDAD"].ToString();
        //        TxtCajas.Text = filas["UNIDADES"].ToString();
        //        TxtEstado.Text = filas["ESTADO"].ToString();
        //        TxtDispositivo.Text = filas["DeviceName"].ToString();
        //        TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
        //        TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //        LbDateFT1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateContents1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateQR1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            LbCodeQRPalteAlv1.Text = filas["LOTE"].ToString();
        //            LbTipoPlantaP.Text = "";
        //            LbVariedadP1.Text = "";
        //            lbUnidadesP.Text = "";
        //            lbNumPlantasP.Text = "";

        //            if (TxtCajas.Text != "PLANTAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            else
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }



        //            TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //            //LbTipoPlantaP.Text = "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

        //            if (Con == null)
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
        //            }
        //            else
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + Con;
        //            }


        //            //LbTipoPlantaP.Text = "Tipo Planta: " + filas["UNIDADES"].ToString() + " " + filas["TIPO_PLANTA"].ToString(); // "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con == null)
        //            {
        //                LbVariedadP1.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //            }
        //            else
        //            {
        //                LbVariedadP1.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con == null)
        //            {
        //                //VIVA: Viveros Valsaín, SLU
        //                //VRE: Viveros Río Eresma, SLU
        //                LbCodePaletAlv1.Text = "";
        //            }
        //            else
        //            {
        //                if (Con.ToString().Contains("VIVA"))
        //                {

        //                    LbCodePaletAlv1.Text = "Viveros Valsaín, SLU";
        //                }
        //                else
        //                {
        //                    LbCodePaletAlv1.Text = "Viveros Río Eresma, SLU";
        //                }
        //            }

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }
        //        else
        //        {

        //            if (TxtCajas.Text != "PLANTAS")
        //            {
        //                // LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            else
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }


        //            LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //            LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }


        //        try
        //        {
        //            //ZBANDEJAS
        //            foreach (DataRow fila2 in dbP.Rows)
        //            {
        //                AA = fila2["ZTIPO_PLANTA"].ToString();
        //                CC = fila2["ZTIPO_FORMATO"].ToString();
        //                //string FF = fila2["ZNUMERO_PLANTAS"].ToString();

        //                //if (DrPrinters.SelectedItem.Value == "6")
        //                //{
        //                //    BB = "P-ALV-" + filas["TIPO_PLANTA"].ToString();
        //                //}
        //                //else
        //                //{
        //                BB = filas["TIPO_PLANTA"].ToString();
        //                //}

        //                //BB = filas["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                DD = filas["UNIDADES"].ToString();//CAJAS
        //                EE = filas["NUM_UNIDADES"].ToString();//40
        //                //GG = filas["MANOJOS"].ToString();//3

        //                if (fila2["ZTIPO_PLANTA"].ToString() == BB && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //                {
        //                    if (filas["UNIDADES"].ToString() != "PLANTAS")
        //                    {
        //                        if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                        {
        //                            if (DrPrinters.SelectedItem.Value == "6")
        //                            {
        //                                string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                Double Value = Convert.ToDouble(Cuantos);
        //                                lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                            }
        //                            else
        //                            {
        //                                LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                            }

        //                            //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                        }
        //                        else
        //                        {
        //                            //ZBANDEJAS
        //                            foreach (DataRow fila3 in dbP.Rows)
        //                            {
        //                                if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                {
        //                                    int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());

        //                                    if (DrPrinters.SelectedItem.Value == "6")
        //                                    {
        //                                        string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        Double Value = Convert.ToDouble(Cuantos);
        //                                        lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                                    }
        //                                    else
        //                                    {
        //                                        LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                    }
        //                                    //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                            //value = 1234567890.123456;
        //                            Double Value = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());
        //                            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                            // Displays 1,234,567,890.1
        //                        }
        //                        else
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                        }
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //Lberror.Text = ex.Message;
        //            string a = Main.Ficherotraza("CargaModificado-->" + ex.Message);
        //        }





        //        TxtID.Text = filas["ID"].ToString();
        //        TxtForm.Text = filas["TIPO_FORM"].ToString();
        //        TxtManojos.Text = filas["MANOJOS"].ToString();
        //        TxtDesde.Text = filas["DESDE"].ToString();
        //        TxtHasta.Text = filas["HASTA"].ToString();
        //        TxtETDesde.Text = filas["ETDESDE"].ToString();
        //        TxtETHasta.Text = filas["ETHASTA"].ToString();
        //        TxtTuneles.Text = filas["TUNELES"].ToString();
        //        TxtPasillos.Text = filas["PASILLOS"].ToString();
        //        TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
        //        TxtOK.Text = filas["OK"].ToString();

        //        //if(TxtID.Enabled == true)
        //        //{
        //        Oculta_Datos(1);
        //        //}
        //        //else
        //        //{
        //        //    Oculta_Datos(0);
        //        //}
        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        alertaLog.Visible = false;
        //        int Runidades = 0;


        //        if (DrVariedad.SelectedItem.Value == "10") //en revision es 7
        //        {
        //            //SQL = "SELECT A.* ";
        //            //SQL += " FROM ZENTRADA A, ZFORMULARIOS B ";
        //            //SQL += " WHERE A.TIPO_FORM = B.ZTITULO ";
        //            //SQL += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
        //            //SQL += " AND A.LOTE = '" + filas["LOTE"].ToString() + "'";

        //            SQL = "SELECT * ";
        //            SQL += " FROM ZENTRADA  ";
        //            SQL += " WHERE LOTE = '" + filas["LOTE"].ToString() + "'";

        //            DataTable dbF = Main.BuscaLote(SQL).Tables[0];

        //            if (dbF.Rows.Count > 1)
        //            {
        //                int Totales = 0;
        //                //ZENTRADA
        //                foreach (DataRow filaCount in dbF.Rows)
        //                {
        //                    try
        //                    {
        //                        //ZBANDEJAS
        //                        foreach (DataRow fila2 in dbP.Rows)
        //                        {
        //                            AA = fila2["ZTIPO_PLANTA"].ToString();
        //                            BB = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                            DD = filaCount["UNIDADES"].ToString();//CAJAS
        //                            EE = filaCount["NUM_UNIDADES"].ToString();//CAJAS
        //                            FF = fila2["ZNUMERO_PLANTAS"].ToString();//CAJAS

        //                            if (fila2["ZTIPO_PLANTA"].ToString().Contains(BB) && fila2["ZTIPO_FORMATO"].ToString() == filaCount["UNIDADES"].ToString())
        //                            {
        //                                CC = fila2["ZTIPO_FORMATO"].ToString();

        //                                if (filaCount["UNIDADES"].ToString() != "PLANTAS")
        //                                {
        //                                    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                    {
        //                                        Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString());
        //                                        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    }
        //                                    else
        //                                    {
        //                                        foreach (DataRow fila3 in dbP.Rows)
        //                                        {
        //                                            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                            {
        //                                                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                                Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()) + NN);
        //                                                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                }

        //                                //if (filaCount["UNIDADES"].ToString() == "PLANTAS")
        //                                //{
        //                                //    Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //    Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //}
        //                                //else if (filaCount["UNIDADES"].ToString() == "CAJAS")
        //                                //{
        //                                //    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                //    {
        //                                //        Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString());
        //                                //        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //    }
        //                                //    else
        //                                //    {
        //                                //        foreach (DataRow fila3 in dbP.Rows)
        //                                //        {
        //                                //            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                //            {
        //                                //                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                //                Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()) + NN);
        //                                //                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //            }
        //                                //        }
        //                                //    }
        //                                //}
        //                                //else if (filaCount["UNIDADES"].ToString() == "BANDEJAS")
        //                                //{
        //                                //    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                //    {
        //                                //        Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()));
        //                                //        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //    }
        //                                //    else
        //                                //    {
        //                                //        foreach (DataRow fila3 in dbP.Rows)
        //                                //        {
        //                                //            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                //            {
        //                                //                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                //                Totales += ((Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN);
        //                                //                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                //            }
        //                                //        }
        //                                //    }
        //                                //}
        //                                break;
        //                            }
        //                        }

        //                        Double Value = Convert.ToDouble(Totales);
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                            lbUnidadesP.Text = "Unidades: " + CC + " " + Runidades.ToString();
        //                        }
        //                        else
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Lberror.Text = ex.Message;
        //                        string a = Main.Ficherotraza("CargaModificado-->" + ex.Message);
        //                    }
        //                }
        //            }
        //        }

        //        btnGenerate_Click(null, null);
        //        if (DrPrinters.SelectedItem.Value == "4")
        //        {
        //            btnGeneraTodoPerf_Click(null, null);
        //        }
        //        else
        //        {
        //            btnGenerateTodo_Click(null, null);
        //        }


        //        //btnGenerate_Click(null, null);
        //        //btnGenerateZXING_Click(null, null);
        //        //if (DrPrinters.SelectedItem.Value == "4")
        //        //{
        //        //    btnGeneraTodoPerf_Click(null, null);
        //        //}
        //        //else
        //        //{
        //        //    btnGenerateTodo_Click(null, null);
        //        //}
        //        //btnGenerateTodo_Click(sender, e);
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            btnPrintPaletAlv.Visible = true;
        //        }
        //        else
        //        {
        //            btnPrint2.Visible = true;
        //        }
        //        //btnPrint2.Visible = true;
        //        btProcesa.Visible = false;
        //        btPorcesa.Visible = false;
        //        DrPrinters_Click();
        //        //Btfin.Visible = false;
        //        //BTerminado.Visible = false;
        //        string Miro = filas["ESTADO"].ToString();
        //        //if (filas["ESTADO"].ToString() == "")
        //        //{
        //        //    BTerminado.Visible = true;
        //        //}
        //        //else
        //        //{
        //        //    Btfin.Visible = true;
        //        //}

        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        //Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //        Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

        //        break;
        //    }
        //    this.Session["IDLista"] = "Lotes";
        //    BtDelete.Enabled = true;
        //    btNew.Enabled = true;
        //}


        //private void CargaDescripcionLotePrint(DataTable dt)
        //{
        //    //SELECT A.TIPO_PLANTA, A.VARIEDAD, A.LOTE,  B.ZTIPO_FORMATO, A.NUM_UNIDADES, B.ZNUMERO_PLANTAS, D.UD_BASE, D.FACT_DIV,
        //    //((B.ZNUMERO_PLANTAS * A.NUM_UNIDADES) / D.FACT_DIV) AS CALCULO
        //    //FROM ZENTRADA A, ZBANDEJAS B, ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZUD_BASE D
        //    //WHERE A.TIPO_PLANTA = B.ZTIPO_PLANTA
        //    //AND A.TIPO_PLANTA = 'FRAMB-220'
        //    //AND A.TIPO_PLANTA = C.ZTIPO_PLANTA
        //    //AND C.UD_BASE = D.UD_BASE

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];
        //    Boolean Esta = false;

        //    SQL = "SELECT * FROM ZUD_BASE ";
        //    DataTable dbBase = Main.BuscaLote(SQL).Tables[0];

        //    string AA = "";
        //    string CC = "";
        //    string BB = "";
        //    string DD = "";
        //    string EE = "";
        //    string FF = "";

        //    //ZENTRADA y cargo las cajas de edicion
        //    foreach (DataRow filasEntrada in dt.Rows)
        //    {
        //        LbIDLote.Text = filasEntrada["ID"].ToString();
        //        txtQRCode.Text = filasEntrada["LOTE"].ToString();
        //        //txtQRCodebis.Text = filasEntrada["LOTE"].ToString();
        //        TxtCampo.Text = filasEntrada["TIPO_PLANTA"].ToString();
        //        TxtFecha.Text = filasEntrada["FECHA"].ToString();
        //        TxtVariedad.Text = filasEntrada["VARIEDAD"].ToString();
        //        TxtCajas.Text = filasEntrada["UNIDADES"].ToString();
        //        TxtEstado.Text = filasEntrada["ESTADO"].ToString();
        //        TxtDispositivo.Text = filasEntrada["DeviceName"].ToString();
        //        TxtLoteDestino.Text = filasEntrada["LOTEDESTINO"].ToString();
        //        TxtPlantas.Text = filasEntrada["NUM_UNIDADES"].ToString();
        //        LbDateFT1.Text = DateTime.Now.ToString("HH:mm:ss"); //filasEntrada["SendTime"].ToString().Substring(10);
        //        LbDateContents1.Text = DateTime.Now.ToString("HH:mm:ss"); //filasEntrada["SendTime"].ToString().Substring(10);
        //        LbDateQR1.Text = DateTime.Now.ToString("HH:mm:ss"); //filasEntrada["SendTime"].ToString().Substring(10);
        //        LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filasEntrada["SendTime"].ToString().Substring(10);
        //        TxtID.Text = filasEntrada["ID"].ToString();
        //        TxtForm.Text = filasEntrada["TIPO_FORM"].ToString();
        //        TxtManojos.Text = filasEntrada["MANOJOS"].ToString();
        //        TxtDesde.Text = filasEntrada["DESDE"].ToString();
        //        TxtHasta.Text = filasEntrada["HASTA"].ToString();
        //        TxtETDesde.Text = filasEntrada["ETDESDE"].ToString();
        //        TxtETHasta.Text = filasEntrada["ETHASTA"].ToString();
        //        TxtTuneles.Text = filasEntrada["TUNELES"].ToString();
        //        TxtPasillos.Text = filasEntrada["PASILLOS"].ToString();
        //        TxtObservaciones.Text = filasEntrada["OBSERVACIONES"].ToString();

        //        string Unidad_Base = "";

        //        //Si es impresora 6
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            LbCodeQRPalteAlv1.Text = filasEntrada["LOTE"].ToString();
        //            LbTipoPlantaP.Text = "";
        //            LbVariedadP1.Text = "";
        //            lbUnidadesP.Text = "";
        //            lbNumPlantasP.Text = "";

        //            Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filasEntrada["TIPO_PLANTA"].ToString() + "' AND UD_BASE IS NOT NULL AND UD_BASE <> '' ", null);

        //            if (Con == null)
        //            {
        //                //seguimos como está.
        //                Unidad_Base = "";
        //                Lbmensaje.Text = "No se encuentra UD_BASE para " + filasEntrada["TIPO_PLANTA"].ToString() + " en la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //            }
        //            else
        //            {
        //                Unidad_Base = Con.ToString();
        //            }

        //            //Ahora se comprueba Si el contenido en Tabla ZENTRADA sobre campo UNIDADES es distinto a PLANTAS igual que filasEntrada["UNIDADES"].ToString() != "PLANTAS", pasar a variable = "PLANTAS"
        //            if (TxtCajas.Text != "PLANTAS")
        //            {
        //                if (Unidad_Base != "")
        //                {
        //                    lbUnidadesP.Text = filasEntrada["NUM_UNIDADES"].ToString() + " " + filasEntrada["UNIDADES"].ToString(); //+ filasEntrada["UNIDADES"].ToString();
        //                    //lbUnidadesP.Text = "Unidades: " + filasEntrada["UNIDADES"].ToString() + " " + filasEntrada["NUM_UNIDADES"].ToString() ; //+ filasEntrada["UNIDADES"].ToString();
        //                    Esta = false;
        //                    foreach (DataRow filbase in dbBase.Rows) //ZBANDEJAS EL CAMPO ZTIPO_FORMATO
        //                    {
        //                        if (filasEntrada["UNIDADES"].ToString() == filbase["UD_BASE"].ToString()) // "GRAMOS")
        //                        {
        //                            Esta = true;//FALTA ZNUMERO_PLANTAS
        //                            lbNumPlantasP.Text = Convert.ToDouble(filasEntrada["NUM_UNIDADES"].ToString()) / Convert.ToInt32(filbase["FACT_DIV"].ToString()) + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
        //                        }

        //                    }
        //                    if (Esta == false)
        //                    {
        //                        lbNumPlantasP.Text = "Verificar relación para " + filasEntrada["UNIDADES"].ToString() + " ya que no encuentra UD_BASE en la tabla ZUD_BASE."; // Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) + " " + Unidad_Base;
        //                    }
        //                }
        //                else
        //                {
        //                    lbUnidadesP.Text = filasEntrada["NUM_UNIDADES"].ToString() + " " + filasEntrada["UNIDADES"].ToString(); //+ filasEntrada["UNIDADES"].ToString();
        //                    //lbNumPlantasP.Text = "Nº Plantas: " + filasEntrada["NUM_UNIDADES"].ToString();
        //                }

        //            }
        //            else
        //            { //Son PLANTAS
        //                lbUnidadesP.Text = filasEntrada["NUM_UNIDADES"].ToString() + " " + filasEntrada["UNIDADES"].ToString();
        //            }

        //            TxtPlantas.Text = filasEntrada["NUM_UNIDADES"].ToString();
        //            //LbTipoPlantaP.Text = "Tipo Planta: BANDEJAS " + filasEntrada["TIPO_PLANTA"].ToString();

        //            //Busca descripción de la panta en ZTIPOPLANTADESCRIP
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filasEntrada["TIPO_PLANTA"].ToString() + "'", null);

        //            if (Con == null)
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + filasEntrada["TIPO_PLANTA"].ToString();
        //                //mensaje
        //                Lbmensaje.Text = "No se encuentra la Variedad " + filasEntrada["TIPO_PLANTA"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPTIPO con TIPO_PLANTA en la tabla ZTIPOPLANTADESCRIP.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //            }
        //            else
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + Con;
        //            }

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filasEntrada["VARIEDAD"].ToString() + "'", null);

        //            //if (Con is System.DBNull)
        //            if (Con == null)
        //            {
        //                LbVariedadP1.Text = "Variedad: " + filasEntrada["VARIEDAD"].ToString();
        //                //mensaje
        //                Lbmensaje.Text = "No se encuentra la Variedad " + filasEntrada["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPCION con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //            }
        //            else
        //            {
        //                LbVariedadP1.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filasEntrada["VARIEDAD"].ToString() + "'", null);

        //            //if (Con is System.DBNull)
        //            if (Con == null)
        //            {
        //                //VIVA: Viveros Valsaín, SLU
        //                //VRE: Viveros Río Eresma, SLU
        //                LbCodePaletAlv1.Text = "";
        //                Lbmensaje.Text = "No se encuentra la Empresa donde la Variedad " + filasEntrada["VARIEDAD"].ToString() + " asignada desde ZENTRADA que contenga el dato en el campo ZEMPRESA en la tabla ZEMPRESAVARIEDAD.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //                //mensaje
        //            }
        //            else
        //            {
        //                if (Con.ToString().Contains("VIVA"))
        //                {

        //                    LbCodePaletAlv1.Text = "Viveros Valsaín, SLU";
        //                }
        //                else
        //                {
        //                    LbCodePaletAlv1.Text = "Viveros Río Eresma, SLU";
        //                }
        //            }

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }
        //        else
        //        { //Resto de impresoras
        //            if (TxtCajas.Text != "PLANTAS")
        //            {
        //                // LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + filasEntrada["UNIDADES"].ToString() + " " + filasEntrada["NUM_UNIDADES"].ToString(); //+ filasEntrada["UNIDADES"].ToString();
        //                double Cuantos = Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString());
        //                LbPlantasS.Text = "Nº Plantas: " + Cuantos.ToString("N0");
        //                //LbPlantasS.Text = "Nº Plantas: " + filasEntrada["NUM_UNIDADES"].ToString();

        //            }
        //            else
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + filasEntrada["UNIDADES"].ToString();
        //                double Cuantos = Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString());
        //                LbPlantasS.Text = "Nº Plantas: " + Cuantos.ToString("N0");
        //                //LbPlantasS.Text = "Nº Plantas: " + filasEntrada["NUM_UNIDADES"].ToString();
        //            }

        //            LbCampoS.Text = "CAMPO O SECTOR: " + filasEntrada["DESDE"].ToString();
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filasEntrada["TIPO_PLANTA"].ToString();
        //            LbFechaS.Text = "FECHA CORTE: " + filasEntrada["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filasEntrada["VARIEDAD"].ToString();

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }


        //        try
        //        {
        //            //ZBANDEJAS
        //            foreach (DataRow filasBandeja in dbP.Rows)
        //            {
        //                AA = filasBandeja["ZTIPO_PLANTA"].ToString();
        //                CC = filasBandeja["ZTIPO_FORMATO"].ToString();
        //                BB = filasEntrada["TIPO_PLANTA"].ToString();

        //                DD = filasEntrada["UNIDADES"].ToString();//CAJAS
        //                EE = filasEntrada["NUM_UNIDADES"].ToString();//40
        //                string a = filasBandeja["ZNUMERO_PLANTAS"].ToString(); //de bandejas
        //                 //filasBandeja["ZTIPO_PLANTA"].ToString() == filasEntrada["TIPO_PLANTA"].ToString()
        //                if (filasBandeja["ZTIPO_PLANTA"].ToString() == BB && filasBandeja["ZTIPO_FORMATO"].ToString() == filasEntrada["UNIDADES"].ToString())
        //                {
        //                    //Si ZENTRADA UNIDADES SON PLANTAS
        //                    if (filasEntrada["UNIDADES"].ToString() == "PLANTAS")
        //                    {
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            //lbNumPlantasP.Text = "Nº Plantas: " + filasEntrada["NUM_UNIDADES"].ToString();
        //                            //value = 1234567890.123456;
        //                            Double Value = Convert.ToDouble(filasEntrada["NUM_UNIDADES"].ToString());
        //                            lbNumPlantasP.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + filasEntrada["UNIDADES"].ToString();
        //                            // Displays 1,234,567,890.1
        //                        }
        //                        else
        //                        {
        //                            double cuantos = Convert.ToDouble(filasEntrada["NUM_UNIDADES"].ToString());
        //                            LbPlantasS.Text = "Nº Plantas: " + cuantos.ToString("N0");
        //                            //LbPlantasS.Text = "Nº Plantas: " + filasEntrada["NUM_UNIDADES"].ToString();
        //                        }
        //                        break;
        //                    }
        //                    else
        //                    {//Si es distinto a plantas
        //                        if (filasEntrada["MANOJOS"].ToString() == "0" || filasEntrada["MANOJOS"].ToString() == "" || filasEntrada["MANOJOS"].ToString() == null)
        //                        {
        //                            if (DrPrinters.SelectedItem.Value == "6")
        //                            {
        //                                if (Unidad_Base != "")
        //                                {
        //                                    if (filasBandeja["ZTIPO_FORMATO"].ToString() == Unidad_Base)//filasEntrada["UNIDADES"].ToString()
        //                                    {
        //                                        //Para que calcule deben existir los registros en ZBANDEJAS y ZUD_BASE
        //                                        Esta = false;
        //                                        foreach (DataRow filbase in dbBase.Rows)
        //                                        {
        //                                            if (filasBandeja["ZTIPO_FORMATO"].ToString() == filbase["UD_BASE"].ToString()) // "GRAMOS")filasEntrada["UNIDADES"].ToString()
        //                                            {
        //                                                Esta = true;
        //                                                lbNumPlantasP.Text = Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString()) / Convert.ToInt32(filbase["FACT_DIV"].ToString()) + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
        //                                            }

        //                                        }
        //                                        //if (Esta == false)
        //                                        //{
        //                                        //    string Cuantos = ((Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())) / 1000).ToString();
        //                                        //    lbNumPlantasP.Text = "Cantidad: " + Cuantos + " " + Unidad_Base;
        //                                        //}
        //                                    }
        //                                    else
        //                                    {
        //                                        lbNumPlantasP.Text = "";
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    string Cuantos = (Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                    lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                    Double Value = Convert.ToDouble(Cuantos);
        //                                    lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                double Cuantos = (Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString()));
        //                                //LbPlantasS.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{i:n0}", Cuantos).Replace(",", ".");
        //                                LbPlantasS.Text = "Nº Plantas: " + Cuantos.ToString("N0");
        //                                //Formato miles
        //                                //precio.ToString("C");  //1.234,35 €
        //                                //precio.ToString("G");  //1234,345
        //                                //precio.ToString("N");  //1.234,35

        //                                ////Se puede definir el número de decimales en C y N
        //                                //precio.ToString("C1");  //1.234,3 €
        //                                //precio.ToString("N0");  //1.234

        //                                ////En el caso de G se refiere al número de dígitos del número
        //                                //precio.ToString("G5"); //1234,3
        //                            }
        //                        }
        //                        else
        //                        {
        //                            foreach (DataRow fila3 in dbP.Rows)
        //                            {
        //                                if (fila3["ZTIPO_PLANTA"].ToString() == filasEntrada["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                {
        //                                    int NN = Convert.ToInt32(filasEntrada["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());

        //                                    if (DrPrinters.SelectedItem.Value == "6")
        //                                    {
        //                                        if (Unidad_Base != "")
        //                                        {
        //                                            if (filasEntrada["UNIDADES"].ToString() != Unidad_Base)
        //                                            {
        //                                                string Cuantos = filasEntrada["NUM_UNIDADES"].ToString(); // Convert.ToInt32(Unidad_Base)).ToString();
        //                                                lbNumPlantasP.Text = filasEntrada["NUM_UNIDADES"].ToString() + " " + Unidad_Base;
        //                                                Double Value = Convert.ToDouble(Cuantos);
        //                                                lbNumPlantasP.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + filasEntrada["UNIDADES"].ToString();
        //                                            }
        //                                            else
        //                                            {
        //                                                lbNumPlantasP.Text = "";
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            string Cuantos = ((Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                            lbNumPlantasP.Text = ((Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())) + NN).ToString() + " " + filasEntrada["UNIDADES"].ToString();
        //                                            Double Value = Convert.ToDouble(Cuantos);
        //                                            lbNumPlantasP.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + filasEntrada["UNIDADES"].ToString();
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        Double Cuantos = ((Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())) + NN);
        //                                        //LbPlantasS.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Cuantos).Replace(",", ".");
        //                                        LbPlantasS.Text = "Nº Plantas: " + Cuantos.ToString("N0");
        //                                    }
        //                                    //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filasEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
        //        }

        //        TxtID.Text = filasEntrada["ID"].ToString();
        //        TxtForm.Text = filasEntrada["TIPO_FORM"].ToString();
        //        TxtManojos.Text = filasEntrada["MANOJOS"].ToString();
        //        TxtDesde.Text = filasEntrada["DESDE"].ToString();
        //        TxtHasta.Text = filasEntrada["HASTA"].ToString();
        //        TxtETDesde.Text = filasEntrada["ETDESDE"].ToString();
        //        TxtETHasta.Text = filasEntrada["ETHASTA"].ToString();
        //        TxtTuneles.Text = filasEntrada["TUNELES"].ToString();
        //        TxtPasillos.Text = filasEntrada["PASILLOS"].ToString();
        //        TxtObservaciones.Text = filasEntrada["OBSERVACIONES"].ToString();
        //        TxtOK.Text = filasEntrada["OK"].ToString();

        //        Oculta_Datos(1);

        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        alertaLog.Visible = false;
        //        int Runidades = 0;


        //        if (DrVariedad.SelectedItem.Value == "10") //en revision es 7, en formulario es StockLote
        //        {
        //            SQL = "SELECT * ";
        //            SQL += " FROM ZENTRADA  ";
        //            SQL += " WHERE LOTE = '" + filasEntrada["LOTE"].ToString() + "'";

        //            DataTable dbF = Main.BuscaLote(SQL).Tables[0];

        //            if (dbF.Rows.Count > 1)
        //            {
        //                int Totales = 0;

        //                foreach (DataRow filaCount in dbF.Rows)
        //                {
        //                    try
        //                    {
        //                        foreach (DataRow filasBandeja in dbP.Rows)
        //                        {
        //                            AA = filasBandeja["ZTIPO_PLANTA"].ToString();
        //                            BB = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                            DD = filaCount["UNIDADES"].ToString();//CAJAS
        //                            EE = filaCount["NUM_UNIDADES"].ToString();//CAJAS
        //                            FF = filasBandeja["ZNUMERO_PLANTAS"].ToString();//CAJAS

        //                            if (filasBandeja["ZTIPO_PLANTA"].ToString().Contains(BB) && filasBandeja["ZTIPO_FORMATO"].ToString() == filaCount["UNIDADES"].ToString())
        //                            {
        //                                CC = filasBandeja["ZTIPO_FORMATO"].ToString();


        //                                if (filaCount["UNIDADES"].ToString() != "PLANTAS")
        //                                {
        //                                    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                    {
        //                                        Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString());
        //                                        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    }
        //                                    else
        //                                    {
        //                                        foreach (DataRow fila3 in dbP.Rows)
        //                                        {
        //                                            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                            {
        //                                                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                                Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filasBandeja["ZNUMERO_PLANTAS"].ToString()) + NN);
        //                                                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                }
        //                                break;
        //                            }
        //                        }

        //                        Double Value = Convert.ToDouble(Totales);
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            lbNumPlantasP.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + filasEntrada["UNIDADES"].ToString();
        //                            lbUnidadesP.Text = Runidades.ToString() + " " + CC;
        //                        }
        //                        else
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Lberror.Text = ex.Message;
        //                        string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
        //                    }
        //                }
        //            }
        //        }

        //        if (this.Session["IDLote"].ToString() == "1")
        //        {
        //            btnGenerate_Click(null, null);
        //        }
        //        //
        //        if (DrPrinters.SelectedItem.Value == "4")
        //        {
        //            btnGeneraTodoPerf_Click(null, null);
        //        }
        //        else
        //        {
        //            btnGenerateTodo_Click(null, null);
        //        }

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            btnPrintPaletAlv.Visible = true;
        //        }
        //        else
        //        {
        //            btnPrint2.Visible = true;
        //        }


        //        btProcesa.Visible = false;
        //        btPorcesa.Visible = false;
        //        DrPrinters_Click();

        //        string Miro = filasEntrada["ESTADO"].ToString();

        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        //Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //        Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //        Posiciona_Permiso();
        //        break;
        //    }
        //}

        private void CargaDescripcionLote(DataTable dt)
        {
            //SELECT A.TIPO_PLANTA, A.VARIEDAD, A.LOTE,  B.ZTIPO_FORMATO, A.NUM_UNIDADES, B.ZNUMERO_PLANTAS, D.UD_BASE, D.FACT_DIV,
            //((B.ZNUMERO_PLANTAS * A.NUM_UNIDADES) / D.FACT_DIV) AS CALCULO
            //FROM ZENTRADA A, ZBANDEJAS B, ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZUD_BASE D
            //WHERE A.TIPO_PLANTA = B.ZTIPO_PLANTA
            //AND A.TIPO_PLANTA = 'FRAMB-220'
            //AND A.TIPO_PLANTA = C.ZTIPO_PLANTA
            //AND C.UD_BASE = D.UD_BASE

            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];
            //Boolean Esta = false;

            SQL = "SELECT * FROM ZUD_BASE ";
            DataTable dbBase = Main.BuscaLote(SQL).Tables[0];

            string AA = "";
#pragma warning disable CS0219 // La variable 'CC' está asignada pero su valor nunca se usa
            string CC = "";
#pragma warning restore CS0219 // La variable 'CC' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'BB' está asignada pero su valor nunca se usa
            string BB = "";
#pragma warning restore CS0219 // La variable 'BB' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'DD' está asignada pero su valor nunca se usa
            string DD = "";
#pragma warning restore CS0219 // La variable 'DD' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'EE' está asignada pero su valor nunca se usa
            string EE = "";
#pragma warning restore CS0219 // La variable 'EE' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'FF' está asignada pero su valor nunca se usa
            string FF = "";
#pragma warning restore CS0219 // La variable 'FF' está asignada pero su valor nunca se usa

            string code = "";
            //ZENTRADA
            //if (row[dt.Columns[i]].ToString() == "0")
            //{
            //    dt.Columns.RemoveAt(i);
            //}

            foreach (DataRow filas in dt.Rows)
            {
                //for (int i = dt.Columns.Count - 1; i >= 0; i--)
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    for (int N = 0; N <= 50; N++)//Hasta 50 campos
                    {
                        string MiContent = "DivReg" + N;
                        ContentPlaceHolder cont = new ContentPlaceHolder();
                        cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                        HtmlGenericControl DivRegistro = (HtmlGenericControl)cont.FindControl(MiContent);
                        if (DivRegistro.Visible == true)
                        {
                            string DivTextoA = "TxL" + N;
                            TextBox DivLabel = (TextBox)cont.FindControl(DivTextoA);

                            if (DivLabel.Visible == true)
                            {
                                string DivIDA = "LBCOLL" + N;
                                TextBox DivLabelA = (TextBox)cont.FindControl(DivIDA);

                                string O = dt.Columns[i].ColumnName;
                                //if (filas[dt.Columns[i].ColumnName].ToString() == DivLabelA.Text)
                                if (dt.Columns[i].ColumnName == DivLabelA.Text)
                                {
                                    DivLabel.Text = filas[dt.Columns[i]].ToString();
                                    if (dt.Columns[i].ColumnName == "ID")
                                    {
                                        LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                        TmpLbIDLote = filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                    }
                                    if (dt.Columns[i].ColumnName == "LOTE")
                                    {
                                        txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                        TmpQRCode = filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                    }
                                    if (dt.Columns[i].ColumnName == "DESDE")
                                    {
                                        TmpLbCampoS = "Movimiento Desde: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCampoSLotes.Text = "Movimiento Desde: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "TIPO_PLANTA")
                                    {
                                        TmpLbPlantaS = "Tipo Planta: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantaSLotes.Text = "Tipo Planta: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "FECHA")
                                    {
                                        TmpLbFechaS = "Fecha: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbFechaSLotes.Text = "Fecha: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "UNIDADES")
                                    {
                                        TmpLbCajasS = "Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCajasSLotes.Text = "Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "NUM_UNIDADES")
                                    {
                                        TmpLbPlantasS = "Número de Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantasSLotes.Text = "Número de Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "VARIEDAD")
                                    {
                                        TmpLbVariedadS = "Variedad: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbVariedadSLotes.Text = "Variedad: " + filas[dt.Columns[i]].ToString();
                                    }
                                }
                            }
                            else
                            {
                                string ComboA = "DrL" + N;
                                DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);

                                //if (filas[dt.Columns[i].ColumnName].ToString() == DivComboA.Text)
                                if (dt.Columns[i].ColumnName == DivComboA.Text)
                                {
                                    DivComboA.Text = filas[dt.Columns[i]].ToString();
                                    if (dt.Columns[i].ColumnName == "ID")
                                    {
                                        LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "LOTE")
                                    {
                                        txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                    }
                                }
                            }

                            DivTextoA = "TxD" + N;
                            DivLabel = (TextBox)cont.FindControl(DivTextoA);

                            if (DivLabel.Visible == true)
                            {
                                string DivIDA = "LBCOLD" + N;
                                TextBox DivLabelA = (TextBox)cont.FindControl(DivIDA);

                                //if (filas[dt.Columns[i].ColumnName].ToString() == DivLabelA.Text)
                                if (dt.Columns[i].ColumnName == DivLabelA.Text)
                                {
                                    DivLabel.Text = filas[dt.Columns[i]].ToString();
                                    if (dt.Columns[i].ColumnName == "ID")
                                    {
                                        LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                        TmpLbIDLote = filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                    }
                                    if (dt.Columns[i].ColumnName == "LOTE")
                                    {
                                        txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                        TmpQRCode = filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                    }
                                    if (dt.Columns[i].ColumnName == "DESDE")
                                    {
                                        TmpLbCampoS = "Movimiento Desde: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCampoSLotes.Text = "Movimiento Desde: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "TIPO_PLANTA")
                                    {
                                        TmpLbPlantaS = "Tipo Planta: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantaSLotes.Text = "Tipo Planta: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "FECHA")
                                    {
                                        TmpLbFechaS = "Fecha: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbFechaSLotes.Text = "Fecha: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "UNIDADES")
                                    {
                                        TmpLbCajasS = "Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCajasSLotes.Text = "Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "NUM_UNIDADES")
                                    {
                                        TmpLbPlantasS = "Número de Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantasSLotes.Text = "Número de Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "VARIEDAD")
                                    {
                                        TmpLbVariedadS = "Variedad: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbVariedadSLotes.Text = "Variedad: " + filas[dt.Columns[i]].ToString();
                                    }
                                }
                            }
                            else
                            {
                                string ComboA = "DrR" + N;
                                DropDownList DivComboA = (DropDownList)cont.FindControl(ComboA);
                                //if (filas[dt.Columns[i].ColumnName].ToString() == DivComboA.Text)
                                if (dt.Columns[i].ColumnName == DivComboA.Text)
                                {
                                    DivComboA.Text = filas[dt.Columns[i]].ToString();
                                    if (dt.Columns[i].ColumnName == "ID")
                                    {
                                        LbIDLote.Text = filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "LOTE")
                                    {
                                        txtQRCode.Text = filas[dt.Columns[i]].ToString();
                                    }
                                }
                            }
                        }
                    }
                }

                //LbIDLote.Text = filas["ID"].ToString();
                //txtQRCode.Text = filas["LOTE"].ToString();
                //txtQRCodebis.Text = filas["LOTE"].ToString();
                //TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
                //TxtFecha.Text = filas["FECHA"].ToString();
                //TxtVariedad.Text = filas["VARIEDAD"].ToString();
                //TxtCajas.Text = filas["UNIDADES"].ToString();
                //TxtEstado.Text = filas["ESTADO"].ToString();
                //TxtDispositivo.Text = filas["DeviceName"].ToString();
                //TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
                //TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
                LbDateFTLotes.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDateQRLotes.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDatePaletAlvLotes.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                TxtID.Text = filas["ID"].ToString();
                //TxtForm.Text = filas["TIPO_FORM"].ToString();
                //TxtManojos.Text = filas["MANOJOS"].ToString();
                //TxtDesde.Text = filas["DESDE"].ToString();
                //TxtHasta.Text = filas["HASTA"].ToString();
                //TxtETDesde.Text = filas["ETDESDE"].ToString();
                //TxtETHasta.Text = filas["ETHASTA"].ToString();
                //TxtTuneles.Text = filas["TUNELES"].ToString();
                //TxtPasillos.Text = filas["PASILLOS"].ToString();
                //TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();

                string Unidad_Base = "";

                LbCodeQRPalteAlvLotes.Text = filas["LOTE"].ToString();
                LbTipoPlantaPLotes.Text = "";
                LbVariedadPLotes.Text = "";
                lbUnidadesPLotes.Text = "";
                lbNumPlantasPLotes.Text = "";

                Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "' AND UD_BASE IS NOT NULL AND UD_BASE <> '' ", null);

                if (Con == null)
                {
                    //mensaje
                    Lbmensaje.Text = "No se encuentra el registro de la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN para Tipo Planta '" + filas["TIPO_PLANTA"].ToString() + "' y Variedad " + filas["VARIEDAD"].ToString() + "'.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    Unidad_Base = "";
                }
                else
                {
                    Unidad_Base = Con.ToString();
                    Decimal Dato = Convert.ToDecimal(filas["NUM_UNIDADES"].ToString());
                    lbUnidadesPLotes.Text = Dato.ToString() + " " + filas["UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString(); Dato.ToString("N0") redondea por encima a entero
                    LbCajasSLotes.Text = lbUnidadesPLotes.Text;
                    string b = filas["UNIDADES"].ToString() + "-" + Unidad_Base.ToString();
                    if (filas["UNIDADES"].ToString() != Unidad_Base.ToString()) // filbase["UD_BASE"].ToString()) 
                    {
                        Object Res = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (FACT_DIV) FROM ZUD_BASE  WHERE UD_BASE ='" + Unidad_Base + "' ", null);

                        if (Res == null)
                        {
                            //mensaje
                            Lbmensaje.Text = "No se encuentra el registro de la tabla ZUD_BASE para la unidad base " + Unidad_Base + ".";
                            cuestion.Visible = false;
                            Asume.Visible = true;
                            windowmessaje.Visible = true;
                            MiCloseMenu();
                            //Unidad_Base = "";
                        }
                        else
                        {
                            Object Resto = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZNUMERO_PLANTAS) FROM ZBANDEJAS  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZTIPO_FORMATO = '" + filas["UNIDADES"].ToString() + "'", null);

                            if (Resto == null)
                            {
                                //mensaje
                                Lbmensaje.Text = "No se encuentra el registro de la tabla ZBANDEJAS  para ZTIPO_PLANTA " + filas["TIPO_PLANTA"].ToString() + ".";
                                cuestion.Visible = false;
                                Asume.Visible = true;
                                windowmessaje.Visible = true;
                                MiCloseMenu();
                            }
                            else
                            {

                                AA = filas["NUM_UNIDADES"].ToString();
                                double Totales = Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToDouble(Res);

                                lbNumPlantasPLotes.Text = (Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToInt32(Res)).ToString("N0") + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
                                LbPlantasSLotes.Text = lbNumPlantasPLotes.Text;

                                if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
                                {
                                    //Totales += Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString());
                                    //Runidades += Convert.ToInt32(filas["NUM_UNIDADES"].ToString());
                                }
                                else
                                {

                                    foreach (DataRow fila3 in dbP.Rows)
                                    {
                                        if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                                        {


                                            int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                                            Totales += NN;

                                            Double Value = Convert.ToDouble(Totales);

                                            LbPlantasSLotes.Text = String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " " + Unidad_Base;
                                            lbNumPlantasPLotes.Text = LbPlantasSLotes.Text;
                                        }

                                    }
                                }




                                //lbNumPlantasPLotes.Text = (Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToInt32(Res)).ToString("N0") + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
                                //LbPlantasSLotes.Text = lbNumPlantasPLotes.Text;
                            }
                        }


                        //Object Res = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (FACT_DIV) FROM ZUD_BASE  WHERE UD_BASE ='" + Unidad_Base + "' ", null);

                        //if (Res == null)
                        //{
                        //    //mensaje
                        //    Lbmensaje.Text = "No se encuentra el registro de la tabla ZUD_BASE para la unidad base " + Unidad_Base + ".";
                        //    cuestion.Visible = false;
                        //    Asume.Visible = true;
                        //    DvPreparado.Visible = true;
                        //}
                        //else
                        //{
                        //    Object Resto = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZNUMERO_PLANTAS) FROM ZBANDEJAS  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZTIPO_FORMATO = '" + filas["UNIDADES"].ToString() + "'", null);

                        //    if (Resto == null)
                        //    {
                        //        //mensaje
                        //        Lbmensaje.Text = "No se encuentra el registro de la tabla ZBANDEJAS  para ZTIPO_PLANTA " + filas["TIPO_PLANTA"].ToString() + ".";
                        //        cuestion.Visible = false;
                        //        Asume.Visible = true;
                        //        DvPreparado.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        lbNumPlantasPLotes.Text = (Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToInt32(Res)).ToString("N0") + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
                        //        LbPlantasSLotes.Text = lbNumPlantasPLotes.Text;
                        //    }
                        //}
                    }
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

                if (Con == null)
                {
                    LbTipoPlantaPLotes.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
                    //mensaje
                    Lbmensaje.Text = "No se encuentra la Variedad " + filas["TIPO_PLANTA"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPTIPO con TIPO_PLANTA en la tabla ZTIPOPLANTADESCRIP.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                }
                else
                {
                    LbTipoPlantaPLotes.Text = "Tipo Planta: " + Con;
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

                if (Con == null)
                {
                    LbVariedadPLotes.Text = "Variedad: " + filas["VARIEDAD"].ToString();
                    //mensaje
                    Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPCION con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                }
                else
                {
                    LbVariedadPLotes.Text = "Variedad: " + Con;
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

                if (Con == null)
                {
                    //VIVA: Viveros Valsaín, SLU
                    //VRE: Viveros Río Eresma, SLU
                    LbCodePaletAlvLotes.Text = "";
                    Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZEMPRESA con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    //mensaje
                }
                else
                {
                    if (Con.ToString().Contains("VIVA"))
                    {

                        LbCodePaletAlvLotes.Text = "Viveros Valsaín, SLU";
                    }
                    else
                    {
                        LbCodePaletAlvLotes.Text = "Viveros Río Eresma, SLU";
                    }
                }
                Lbcompleto.Text = "QR COMPLETO";

                LbCampoSLotes.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
                LbPlantaSLotes.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
                LbFechaSLotes.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
                LbVariedadSLotes.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();

                //TxtID.Text = filas["ID"].ToString();
                //TxtForm.Text = filas["TIPO_FORM"].ToString();
                //TxtManojos.Text = filas["MANOJOS"].ToString();
                //TxtDesde.Text = filas["DESDE"].ToString();
                //TxtHasta.Text = filas["HASTA"].ToString();
                //TxtETDesde.Text = filas["ETDESDE"].ToString();
                //TxtETHasta.Text = filas["ETHASTA"].ToString();
                //TxtTuneles.Text = filas["TUNELES"].ToString();
                //TxtPasillos.Text = filas["PASILLOS"].ToString();
                //TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
                //TxtOK.Text = filas["OK"].ToString();

                Oculta_Datos(1);

                alerta.Visible = false;
                alertaErr.Visible = false;
                alertaLog.Visible = false;
#pragma warning disable CS0219 // La variable 'Runidades' está asignada pero su valor nunca se usa
                int Runidades = 0;
#pragma warning restore CS0219 // La variable 'Runidades' está asignada pero su valor nunca se usa

                Con = DBHelper.ExecuteScalarSQL("SELECT ZMANUAL FROM  ZSECUENCIAS  WHERE ZID ='" + DrVariedad.SelectedItem.Value + "'", null);

                if (Con == null)
                {
                    LbCodePaletAlvLotes.Text = "";
                    Lbmensaje.Text = "No se encuentra valor para ZMANUAL (0,1) en la Tabla ZSECUENCIAS para completar valores en el código QR según selección de Lotes.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                }
                //else
                //{
                //    SQL = "SELECT * ";
                //    SQL += " FROM ZENTRADA  ";
                //    SQL += " WHERE LOTE = '" + filas["LOTE"].ToString() + "'";

                //    DataTable dbF = Main.BuscaLote(SQL).Tables[0];

                //    if (dbF.Rows.Count > 1)
                //    {
                //        int Totales = 0;

                //        foreach (DataRow filaCount in dbF.Rows)
                //        {
                //            try
                //            {
                //                foreach (DataRow fila2 in dbP.Rows)
                //                {
                //                    AA = fila2["ZTIPO_PLANTA"].ToString();
                //                    //Crear en FormScan para TIPO_PLANTA = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();
                //                    //
                //                    BB = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
                //                    DD = filaCount["UNIDADES"].ToString();//CAJAS
                //                    EE = filaCount["NUM_UNIDADES"].ToString();//CAJAS
                //                    FF = fila2["ZNUMERO_PLANTAS"].ToString();//CAJAS

                //                    if (fila2["ZTIPO_PLANTA"].ToString().Contains(BB) && fila2["ZTIPO_FORMATO"].ToString() == filaCount["UNIDADES"].ToString())
                //                    {
                //                        CC = fila2["ZTIPO_FORMATO"].ToString();

                //                        if (filaCount["UNIDADES"].ToString() != "PLANTAS")
                //                        {
                //                            if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
                //                            {
                //                                Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString());
                //                                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
                //                            }
                //                            else
                //                            {
                //                                foreach (DataRow fila3 in dbP.Rows)
                //                                {
                //                                    if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                //                                    {
                //                                        int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                //                                        Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()) + NN);
                //                                        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
                //                                    }
                //                                }
                //                            }
                //                        }
                //                        else
                //                        {
                //                            Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
                //                            Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
                //                        }

                //                        Double Value = Convert.ToDouble(Totales);
                //                        if (DrPrinters.SelectedItem.Value == "6")
                //                        {
                //                            lbNumPlantasPLotes.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                //                            lbUnidadesPLotes.Text = "Unidades: " + CC + " " + Runidades.ToString();
                //                        }
                //                        else
                //                        {
                //                            LbPlantasSLotes.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
                //                        }
                //                        break;
                //                    }
                //                }
                //            }
                //            catch (Exception ex)
                //            {
                //                //Lberror.Text = ex.Message;
                //                string a = Main.Ficherotraza("CargaDescripcionLote-->" + ex.Message);
                //                LbCodePaletAlvLotes.Text = "";
                //                Lbmensaje.Text = "Error en la carga de la descripción del Lote: " + ex.Message;
                //                cuestion.Visible = false;
                //                Asume.Visible = true;
                //                DvPreparado.Visible = true;
                //            }
                //        }
                //    }
                //}


                ////Comprueba si tienen manojos 
                //SQL = "SELECT * FROM ZENTRADA ";
                //SQL += " WHERE LOTE = '" + filas["LOTE"].ToString() + "'";
                //DataTable dbEntrada = Main.BuscaLote(SQL).Tables[0];
                //if (dbEntrada.Rows.Count > 1)
                //{
                //    int Totales = 0;
                //    foreach (DataRow filaEntrada in dbEntrada.Rows)
                //    {
                //        try
                //        {
                //            foreach (DataRow filaBandeja in dbP.Rows)
                //            {
                //                AA = filaBandeja["ZTIPO_PLANTA"].ToString();
                //                BB = "P-ALV-" + filaEntrada["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
                //                DD = filaEntrada["UNIDADES"].ToString();//CAJAS
                //                EE = filaEntrada["NUM_UNIDADES"].ToString();//CAJAS
                //                FF = filaBandeja["ZNUMERO_PLANTAS"].ToString();//CAJAS

                //                if (filaBandeja["ZTIPO_PLANTA"].ToString().Contains(BB) && filaBandeja["ZTIPO_FORMATO"].ToString() == filaEntrada["UNIDADES"].ToString())
                //                {
                //                    CC = filaBandeja["ZTIPO_FORMATO"].ToString();
                //                    if (filaEntrada["UNIDADES"].ToString() != "PLANTAS")
                //                    {
                //                        if (filaEntrada["MANOJOS"].ToString() == "0" || filaEntrada["MANOJOS"].ToString() == "" || filaEntrada["MANOJOS"].ToString() == null)
                //                        {
                //                            Totales += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filaBandeja["ZNUMERO_PLANTAS"].ToString());
                //                            Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
                //                        }
                //                        else
                //                        {
                //                            foreach (DataRow fila3 in dbP.Rows)
                //                            {
                //                                if (fila3["ZTIPO_PLANTA"].ToString() == filaEntrada["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                //                                {
                //                                    int NN = Convert.ToInt32(filaEntrada["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                //                                    Totales += (Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filaBandeja["ZNUMERO_PLANTAS"].ToString()) + NN);
                //                                    Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        Totales += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
                //                        Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
                //                    }
                //                    break;
                //                }
                //            }

                //            Double Value = Convert.ToDouble(Totales);
                //            lbNumPlantasP.Text = Value.ToString("N0") + " " + filas["UNIDADES"].ToString();
                //            lbUnidadesP.Text = Runidades.ToString("N0") + " " + CC;
                //            LbPlantasS.Text = "Nº Plantas: " + Value.ToString("N0");
                //        }
                //        catch (Exception ex)
                //        {
                //            //Lberror.Text = ex.Message;
                //            string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
                //        }
                //    }
                //}

                //Pinto Todo

                if (DrPrinters.SelectedItem.Value == "6")
                {
                    code = TmpLbVariedadS; //; + Environment.NewLine;
                }
                else if (DrPrinters.SelectedItem.Value == "4")
                {
                    code = TmpLbVariedadS.Replace("Variedad: ", "");
                }
                else
                {
                    //Generate
                    code = TmpLbCampoS + Environment.NewLine + TmpLbPlantaS + Environment.NewLine + TmpLbFechaS + Environment.NewLine + TmpLbVariedadS + Environment.NewLine + TmpLbCajasS + Environment.NewLine + TmpLbPlantasS; //; + Environment.NewLine;
                }

                this.Session["CodeQR"] = code;

                if (this.Session["IDLote"].ToString() == "1")
                {
                    btnGenerateLote_Click(null, null);
                }
                //
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(null, null);
                }
                else
                {
                    btnGenerateTodoLote_Click(null, null);
                }

                if (DrPrinters.SelectedItem.Value == "6")
                {
                    btnPrintPaletAlv.Visible = true;
                }
                else
                {
                    btnPrint2.Visible = true;
                }

                btProcesa.Visible = false;
                btPorcesa.Visible = false;
                DrPrinters_Click();

                string Miro = filas["ESTADO"].ToString();

                SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
                DBHelper.ExecuteNonQuery(SQL);
                //Carga_Lotes(this.Session["IDSecuencia"].ToString());
                Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
                Posiciona_Permiso();
                break;
            }
        }
        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
        }

        //private void CargaDescripcionLote(DataTable dt)
        //{
        //      //SELECT A.TIPO_PLANTA, A.VARIEDAD, A.LOTE,  B.ZTIPO_FORMATO, A.NUM_UNIDADES, B.ZNUMERO_PLANTAS, D.UD_BASE, D.FACT_DIV,
        //      //((B.ZNUMERO_PLANTAS * A.NUM_UNIDADES) / D.FACT_DIV) AS CALCULO
        //      //FROM ZENTRADA A, ZBANDEJAS B, ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C, ZUD_BASE D
        //      //WHERE A.TIPO_PLANTA = B.ZTIPO_PLANTA
        //      //AND A.TIPO_PLANTA = 'FRAMB-220'
        //      //AND A.TIPO_PLANTA = C.ZTIPO_PLANTA
        //      //AND C.UD_BASE = D.UD_BASE

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];
        //    //Boolean Esta = false;

        //    SQL = "SELECT * FROM ZUD_BASE ";
        //    DataTable dbBase = Main.BuscaLote(SQL).Tables[0];

        //    string AA = "";
        //    string CC = "";
        //    string BB = "";
        //    string DD = "";
        //    string EE = "";
        //    string FF = "";

        //    //ZENTRADA
        //    foreach (DataRow filas in dt.Rows)
        //    {
        //        LbIDLote.Text = filas["ID"].ToString();
        //        txtQRCode.Text = filas["LOTE"].ToString();
        //        //txtQRCodebis.Text = filas["LOTE"].ToString();
        //        TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
        //        TxtFecha.Text = filas["FECHA"].ToString();
        //        TxtVariedad.Text = filas["VARIEDAD"].ToString();
        //        TxtCajas.Text = filas["UNIDADES"].ToString();
        //        TxtEstado.Text = filas["ESTADO"].ToString();
        //        TxtDispositivo.Text = filas["DeviceName"].ToString();
        //        TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
        //        TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //        LbDateFT1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateContents1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateQR1.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        TxtID.Text = filas["ID"].ToString();
        //        TxtForm.Text = filas["TIPO_FORM"].ToString();
        //        TxtManojos.Text = filas["MANOJOS"].ToString();
        //        TxtDesde.Text = filas["DESDE"].ToString();
        //        TxtHasta.Text = filas["HASTA"].ToString();
        //        TxtETDesde.Text = filas["ETDESDE"].ToString();
        //        TxtETHasta.Text = filas["ETHASTA"].ToString();
        //        TxtTuneles.Text = filas["TUNELES"].ToString();
        //        TxtPasillos.Text = filas["PASILLOS"].ToString();
        //        TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();

        //        string Unidad_Base = "";

        //        LbCodeQRPalteAlv1.Text = filas["LOTE"].ToString();
        //        LbTipoPlantaP.Text = "";
        //        LbVariedadP1.Text = "";
        //        lbUnidadesP.Text = "";
        //        lbNumPlantasP.Text = "";

        //        Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "' AND UD_BASE IS NOT NULL AND UD_BASE <> '' ", null);

        //        if (Con == null)
        //        {
        //            //mensaje
        //            Lbmensaje.Text = "No se encuentra el registro de la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN para Tipo Planta '" + filas["TIPO_PLANTA"].ToString() + "' y Variedad " + filas["VARIEDAD"].ToString() + "'.";
        //            cuestion.Visible = false;
        //            Asume.Visible = true;
        //            DvPreparado.Visible = true;
        //            Unidad_Base = "";
        //        }
        //        else
        //        {
        //            Unidad_Base = Con.ToString();
        //            double Dato = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());                   
        //            lbUnidadesP.Text = Dato.ToString("N0") + " " + filas["UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //            LbCajasS.Text = lbUnidadesP.Text;
        //            string b = filas["UNIDADES"].ToString() + "-" + Unidad_Base.ToString();
        //            if (filas["UNIDADES"].ToString() != Unidad_Base.ToString()) // filbase["UD_BASE"].ToString()) 
        //            {
        //                Object Res = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (FACT_DIV) FROM ZUD_BASE  WHERE UD_BASE ='" + Unidad_Base + "' ", null);

        //                if (Res == null)
        //                {
        //                    //mensaje
        //                    Lbmensaje.Text = "No se encuentra el registro de la tabla ZUD_BASE para la unidad base " + Unidad_Base + ".";
        //                    cuestion.Visible = false;
        //                    Asume.Visible = true;
        //                    DvPreparado.Visible = true;
        //                    //Unidad_Base = "";
        //                }
        //                else
        //                {
        //                    Object Resto = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZNUMERO_PLANTAS) FROM ZBANDEJAS  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZTIPO_FORMATO = '" + filas["UNIDADES"].ToString() + "'" , null);

        //                    if (Resto == null)
        //                    {
        //                        //mensaje
        //                        Lbmensaje.Text = "No se encuentra el registro de la tabla ZBANDEJAS  para ZTIPO_PLANTA " + filas["TIPO_PLANTA"].ToString() + ".";
        //                        cuestion.Visible = false;
        //                        Asume.Visible = true;
        //                        DvPreparado.Visible = true;
        //                    }
        //                    else 
        //                    { 
        //                        lbNumPlantasP.Text = (Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToInt32(Res)).ToString("N0") + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
        //                        LbPlantasS.Text = lbNumPlantasP.Text;
        //                    }
        //                }
        //            }
        //        }

        //        TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //        //LbTipoPlantaP.Text = "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //        Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

        //        if (Con == null)
        //        {
        //            LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
        //            //mensaje
        //            Lbmensaje.Text = "No se encuentra la Variedad " + filas["TIPO_PLANTA"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPTIPO con TIPO_PLANTA en la tabla ZTIPOPLANTADESCRIP.";
        //            cuestion.Visible = false;
        //            Asume.Visible = true;
        //            DvPreparado.Visible = true;
        //        }
        //        else
        //        {
        //            LbTipoPlantaP.Text = "Tipo Planta: " + Con;
        //        }

        //        //string N = "";
        //        Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //        //if (Con is System.DBNull)
        //        if (Con == null)
        //        {
        //            LbVariedadP1.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //            //mensaje
        //            Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPCION con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
        //            cuestion.Visible = false;
        //            Asume.Visible = true;
        //            DvPreparado.Visible = true;
        //        }
        //        else
        //        {
        //            LbVariedadP1.Text = "Variedad: " + Con;
        //        }

        //        //N = "";
        //        Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //        //if (Con is System.DBNull)
        //        if (Con == null)
        //        {
        //            //VIVA: Viveros Valsaín, SLU
        //            //VRE: Viveros Río Eresma, SLU
        //            LbCodePaletAlv1.Text = "";
        //            Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZEMPRESA con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
        //            cuestion.Visible = false;
        //            Asume.Visible = true;
        //            DvPreparado.Visible = true;
        //            //mensaje
        //        }
        //        else
        //        {
        //            if (Con.ToString().Contains("VIVA"))
        //            {

        //                LbCodePaletAlv1.Text = "Viveros Valsaín, SLU";
        //            }
        //            else
        //            {
        //                LbCodePaletAlv1.Text = "Viveros Río Eresma, SLU";
        //            }
        //        }
        //        Lbcompleto.Text = "QR COMPLETO";

        //        LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //        LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //        LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //        LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();

        //        TxtID.Text = filas["ID"].ToString();
        //        TxtForm.Text = filas["TIPO_FORM"].ToString();
        //        TxtManojos.Text = filas["MANOJOS"].ToString();
        //        TxtDesde.Text = filas["DESDE"].ToString();
        //        TxtHasta.Text = filas["HASTA"].ToString();
        //        TxtETDesde.Text = filas["ETDESDE"].ToString();
        //        TxtETHasta.Text = filas["ETHASTA"].ToString();
        //        TxtTuneles.Text = filas["TUNELES"].ToString();
        //        TxtPasillos.Text = filas["PASILLOS"].ToString();
        //        TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
        //        TxtOK.Text = filas["OK"].ToString();

        //        Oculta_Datos(1);

        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        alertaLog.Visible = false;
        //        int Runidades = 0;


        //        if (DrVariedad.SelectedItem.Value == "10") //en revision es 7
        //        {
        //            //SQL = "SELECT A.* ";
        //            //SQL += " FROM ZENTRADA A, ZFORMULARIOS B ";
        //            //SQL += " WHERE A.TIPO_FORM = B.ZTITULO ";
        //            //SQL += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
        //            //SQL += " AND A.LOTE = '" + filas["LOTE"].ToString() + "'";

        //            SQL = "SELECT * ";
        //            SQL += " FROM ZENTRADA  ";
        //            SQL += " WHERE LOTE = '" + filas["LOTE"].ToString() + "'";

        //            DataTable dbF = Main.BuscaLote(SQL).Tables[0];

        //            if (dbF.Rows.Count > 1)
        //            {
        //                int Totales = 0;

        //                foreach (DataRow filaCount in dbF.Rows)
        //                {
        //                    try
        //                    {
        //                        foreach (DataRow fila2 in dbP.Rows)
        //                        {
        //                            AA = fila2["ZTIPO_PLANTA"].ToString();
        //                            BB = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                            DD = filaCount["UNIDADES"].ToString();//CAJAS
        //                            EE = filaCount["NUM_UNIDADES"].ToString();//CAJAS
        //                            FF = fila2["ZNUMERO_PLANTAS"].ToString();//CAJAS

        //                            if (fila2["ZTIPO_PLANTA"].ToString().Contains(BB) && fila2["ZTIPO_FORMATO"].ToString() == filaCount["UNIDADES"].ToString())
        //                            {
        //                                CC = fila2["ZTIPO_FORMATO"].ToString();

        //                                if (filaCount["UNIDADES"].ToString() != "PLANTAS")
        //                                {
        //                                    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                    {
        //                                        Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString());
        //                                        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    }
        //                                    else
        //                                    {
        //                                        foreach (DataRow fila3 in dbP.Rows)
        //                                        {
        //                                            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                            {
        //                                                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                                Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()) + NN);
        //                                                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                }
        //                                break;
        //                            }
        //                        }

        //                        Double Value = Convert.ToDouble(Totales);
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                            lbUnidadesP.Text = "Unidades: " + CC + " " + Runidades.ToString();
        //                        }
        //                        else
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Lberror.Text = ex.Message;
        //                        string a = Main.Ficherotraza("CargaModificado-->" + ex.Message);
        //                    }
        //                }
        //            }
        //        }


        //        ////Comprueba si tienen manojos en formulario es StockLote
        //        //SQL = "SELECT * FROM ZENTRADA ";
        //        //SQL += " WHERE LOTE = '" + filas["LOTE"].ToString() + "'";
        //        //DataTable dbEntrada = Main.BuscaLote(SQL).Tables[0];
        //        //if (dbEntrada.Rows.Count > 1)
        //        //{
        //        //    int Totales = 0;
        //        //    foreach (DataRow filaEntrada in dbEntrada.Rows)
        //        //    {
        //        //        try
        //        //        {
        //        //            foreach (DataRow filaBandeja in dbP.Rows)
        //        //            {
        //        //                AA = filaBandeja["ZTIPO_PLANTA"].ToString();
        //        //                BB = "P-ALV-" + filaEntrada["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //        //                DD = filaEntrada["UNIDADES"].ToString();//CAJAS
        //        //                EE = filaEntrada["NUM_UNIDADES"].ToString();//CAJAS
        //        //                FF = filaBandeja["ZNUMERO_PLANTAS"].ToString();//CAJAS

        //        //                if (filaBandeja["ZTIPO_PLANTA"].ToString().Contains(BB) && filaBandeja["ZTIPO_FORMATO"].ToString() == filaEntrada["UNIDADES"].ToString())
        //        //                {
        //        //                    CC = filaBandeja["ZTIPO_FORMATO"].ToString();
        //        //                    if (filaEntrada["UNIDADES"].ToString() != "PLANTAS")
        //        //                    {
        //        //                        if (filaEntrada["MANOJOS"].ToString() == "0" || filaEntrada["MANOJOS"].ToString() == "" || filaEntrada["MANOJOS"].ToString() == null)
        //        //                        {
        //        //                            Totales += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filaBandeja["ZNUMERO_PLANTAS"].ToString());
        //        //                            Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
        //        //                        }
        //        //                        else
        //        //                        {
        //        //                            foreach (DataRow fila3 in dbP.Rows)
        //        //                            {
        //        //                                if (fila3["ZTIPO_PLANTA"].ToString() == filaEntrada["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //        //                                {
        //        //                                    int NN = Convert.ToInt32(filaEntrada["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //        //                                    Totales += (Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString()) * Convert.ToInt32(filaBandeja["ZNUMERO_PLANTAS"].ToString()) + NN);
        //        //                                    Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
        //        //                                }
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        Totales += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
        //        //                        Runidades += Convert.ToInt32(filaEntrada["NUM_UNIDADES"].ToString());
        //        //                    }
        //        //                    break;
        //        //                }
        //        //            }

        //        //            Double Value = Convert.ToDouble(Totales);
        //        //            lbNumPlantasP.Text = Value.ToString("N0") + " " + filas["UNIDADES"].ToString();
        //        //            lbUnidadesP.Text = Runidades.ToString("N0") + " " + CC;
        //        //            LbPlantasS.Text = "Nº Plantas: " + Value.ToString("N0");
        //        //        }
        //        //        catch (Exception ex)
        //        //        {
        //        //            //Lberror.Text = ex.Message;
        //        //            string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
        //        //        }
        //        //    }
        //        //}

        //        if (this.Session["IDLote"].ToString() == "1")
        //        {
        //            btnGenerate_Click(null, null);
        //        }
        //        //
        //        if (DrPrinters.SelectedItem.Value == "4")
        //        {
        //            btnGeneraTodoPerf_Click(null, null);
        //        }
        //        else
        //        {
        //            btnGenerateTodo_Click(null, null);
        //        }

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            btnPrintPaletAlv.Visible = true;
        //        }
        //        else
        //        {
        //            btnPrint2.Visible = true;
        //        }

        //        btProcesa.Visible = false;
        //        btPorcesa.Visible = false;
        //        DrPrinters_Click();

        //        string Miro = filas["ESTADO"].ToString();

        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        //Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //        Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //        Posiciona_Permiso();
        //        break;
        //    }
        //}


        //protected void DrLotes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];
        //    string AA = "";
        //    string CC = "";
        //    string BB = "";
        //    string DD = "";
        //    string EE = "";
        //    string FF = "";

        //    if (DrLotes.SelectedItem.Value == "")
        //    {
        //        SQL = "SELECT * FROM ZENTRADA  WHERE ID = '0' ";
        //        LimpiaCajas();
        //        Posiciona_Permiso();
        //        return;
        //    }
        //    else
        //    {
        //        SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + DrLotes.SelectedItem.Value + "' ";
        //    }

        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    CargaDescripcionLote(dbA);

        //    Posiciona_Permiso();
        //    this.Session["IDLista"] = "Lotes";

        //    this.Session["IDCabecera"] = "0";
        //    if (BodyQR.Visible == true)
        //    {
        //        MontaEtiqueta();
        //    }

        //    //if (this.Session["Cerrados"].ToString() == "0")
        //    //{
        //    //    BtDelete.Enabled = true;
        //    //    BtModifica.Enabled = true;
        //    //}
        //    //else
        //    //{
        //    //    BtDelete.Enabled = false;
        //    //    BtModifica.Enabled = false;
        //    //}
        //    //btNew.Enabled = true;
        //}

        protected void DrLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["IDLote"] = "1";
            this.Session["IDCabecera"] = "0";

            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];
            //string AA = "";
            //string CC = "";
            //string BB = "";
            //string DD = "";
            //string EE = "";
            //string FF = "";

            if (DrLotes.SelectedItem.Value == "")
            {
                SQL = "SELECT * FROM ZENTRADA  WHERE ID = '0' ";
                LimpiaCajas();
                Posiciona_Permiso();
                return;
            }
            else
            {
                SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + DrLotes.SelectedItem.Value + "' ";
            }

            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            CargaDescripcionLote(dbA);
            this.Session["CancelaConsulta"] = "DRLotes";

            this.Session["IDLista"] = "Lotes";
            if (this.Session["Cerrados"].ToString() == "0")
            {
                Habilita_Boton(1);
                //BtDelete.Enabled = true;
                //BtModifica.Enabled = true;
            }
            else
            {
                Habilita_Boton(0);
                //BtDelete.Enabled = false;
                //BtModifica.Enabled = false;
            }
            btNew.Enabled = true;

            if(BodyQR.Visible == true)
            {
                MontaEtiquetaOrdenCompra();
            }
        }



        protected void DrPrintQR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList Combo = (DropDownList)sender;
            string Miro = Combo.SelectedItem.Value;


            PlaceHolderFitoLotes.Controls.Clear();
            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                if (panelContentsFTLotes.Visible == true)
                {
                    PlaceHolderFitoLotes.Controls.Add(new LiteralControl("<iframe src='/Templates/Factura.html'  style='height:100%; width:100%; border:0px;'></iframe>"));
                }
                else
                {
                    btnGenerateLote_Click(sender, e);
                    if (DrPrinters.SelectedItem.Value == "4")
                    {
                        btnGeneraTodoPerf_Click(sender, e);
                    }
                    else
                    {
                        btnGenerateTodoLote_Click(sender, e);
                    }

                    //btnGenerate_Click(sender, e);
                    //btnGenerateZXING_Click(sender, e);
                    //if (LbID.Text != "")
                    //{
                    //    if (DrPrinters.SelectedItem.Value == "4")
                    //    {
                    //        btnGeneraTodoPerf_Click(sender, e);
                    //    }
                    //    else
                    //    {
                    //        btnGenerateTodo_Click(sender, e);
                    //    }
                    //    //btnGenerateTodo_Click(sender, e);
                    //}                   
                }

            }
            else
            {
                LimpiaCajas();
                alertaErr.Visible = false;
            }

            Actualiza_Lote();
            DrPrinters_Click();
        }



        private void Actualiza_Lote()
        {
            string SQL = "";

            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
            {
                SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
                SQL += " FROM ZENTRADA A ";
                SQL += " WHERE ";
                SQL += "  (A.ESTADO <> '2' OR A.ESTADO is null) ";
            }
            else
            {
                SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
                SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
                SQL += " WHERE B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                SQL += " AND A.TIPO_FORM = B.ZTITULO ";
                SQL += " AND (A.ESTADO <> '2' OR A.ESTADO is null) ";
            }
            DataTable dbB = Main.BuscaLote(SQL).Tables[0];
            this.SetDropDownListItemColor(dbB);
        }

        private void SetDropDownListItemColor(DataTable dt)
        {
            foreach (ListItem item in DrLotes.Items)
            {
                foreach (DataRow fila in dt.Rows)
                {
                    if (fila["ID"].ToString() == item.Value)
                    {
                        if (fila["ESTADO"].ToString() == "0")
                        {
                            item.Attributes.CssStyle.Add("color", "green");
                        }
                        else if (fila["ESTADO"].ToString() == "1")
                        {
                            item.Attributes.CssStyle.Add("color", "orange");
                        }
                        else if (fila["ESTADO"].ToString() == "2")
                        {
                            item.Attributes.CssStyle.Add("color", "#21c9cf");
                        }
                        else
                        {
                            //item.Attributes.CssStyle.Add("color", "grey");
                        }
                        break;
                    }
                }
            }
        }


        //Codigo QR ZXING.NET


        public Bitmap ConvertToBitmap(string fileName)
        {
            Bitmap bitmap;
            using (Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(bmpStream);

                bitmap = new Bitmap(image);

            }
            return bitmap;
        }
        protected void BtQRCodeA1_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            if (btn.ID == "ImgQRCodeA1" || btn.ID == "ImgQRCodeB1" || btn.ID == "ImgQRCodeC1" || btn.ID == "ImgQRCodeD1")
            {
                if (btn.ID == "ImgQRCodeA1") { ImgQRCodeA1Lotes.Visible = false; ImgQRCodeA2Lotes.Visible = true; }
                this.Session["SelectQR"] = "1";
            }
            if (btn.ID == "ImgQRCodeA2" || btn.ID == "ImgQRCodeB2" || btn.ID == "ImgQRCodeC3" || btn.ID == "ImgQRCodeD4")
            {
                if (btn.ID == "ImgQRCodeA2") { ImgQRCodeA2Lotes.Visible = false; ImgQRCodeA1Lotes.Visible = true; }
                this.Session["SelectQR"] = "0";
            }

            btnGenerateLote_Click(null, null);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(null, null);
            }
            else
            {
                btnGenerateTodoLote_Click(null, null);
            }
        }



        protected void btnGenerateZXING_Click(object sender, EventArgs e)
        {
            //string code = txtQRCode.Text;
            //string fichero = Path.Combine(Request.MapPath("~/images"), "QRImage.png");
            //if (File.Exists(fichero) == true)
            //{
            //    File.Delete(fichero);
            //}
            GenerateCode(txtQRCode.Text);
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            ReadQRCode();
        }

        //Generate QRCode ZXING
        private void GenerateCode(string name)
        {
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            var result = writer.Write(name);

            //string path = Server.MapPath("~/images/QRImage.png");

            //Bitmap croppedSurvey = new Bitmap(crop.Width, crop.Height);
            var barcodeBitmap = new Bitmap(result);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;

                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    imgBarCode.Height = 200; //Convert.ToInt32(TxAlto.Text);
                    imgBarCode.Width = 200; //Convert.ToInt32(TxAncho.Text);
                }
            }
            catch (Exception a)
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height = 150;
                    imgBarCode.Width = 150;
                }
                else if (DrPrinters.SelectedItem.Value == "6")
                {
                    imgBarCode.Height = 200; // 250;
                    imgBarCode.Width = 200; // 250;
                }
                else
                {
                    TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
                    //TxAlto.Text = "300";
                    //TxAncho.Text = "300";
                    alertaErr.Visible = true;
                }
            }


            using (MemoryStream memory = new MemoryStream())
            {
                //using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                using (Bitmap bitMap = barcodeBitmap)
                {
                    barcodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] bytes = memory.ToArray();
                    //fs.Write(bytes, 0, bytes.Length);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                    imgBarCode.Visible = true;
                    //fs.Close();
                    //imgBarCode.ImageUrl = "~/images/QRImage.jpg";
                }
            }

            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));


            PlaceHolderOrdenCompra.Visible = false;
            PlaceHolderQRLote.Visible = false;
            panelContentsFTLotes.Visible = false;
            PlaceHolderPaletAlvLote.Visible = false;

            if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            {
                PlaceHolderOrdenCompra.Controls.Add(imgBarCode);
                PlaceHolderOrdenCompra.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "2")
            {
                PlaceHolderQROrdenCompra.Controls.Add(imgBarCode);
                PlaceHolderQROrdenCompra.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "3")
            {
                panelContentsFTOrdenCompra.Controls.Add(imgBarCode);
                panelContentsFTOrdenCompra.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "6")
            {
                PlaceHolderPaletAlvLote.Controls.Add(imgBarCode);
                PlaceHolderPaletAlvLote.Visible = true;
            }
            LbCodigoLoteLotes.Text = "CÓDIGO LOTE:";
            this.Session["CodigoQR"] = barcodeBitmap;

            ReadQRCode();

        }


    }
}