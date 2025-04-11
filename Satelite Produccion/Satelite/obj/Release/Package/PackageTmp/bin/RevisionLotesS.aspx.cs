using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace Satelite
{
    public partial class RevisionLotesS : System.Web.UI.Page
    {
        public Boolean Esta = false;

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

                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    Server.Transfer("Login.aspx"); //Default
                }



                if (!IsPostBack)
                {
                    this.Session["SQL"] = "";
                    this.Session["IDSecuencia"] = "0";
                    this.Session["IDProcedimiento"] = "0";
                    this.Session["GridOrden"] = "";
                    this.Session["GridEdicion"] = "0";
                    this.Session["Columna"] = "";
                    //this.Session["DESARROLLO"] = "0";
                    //ChkSlot.Visible = false;

                    Variables.mensajeserver = "";
                    //if (txtQRCode.Text == "")
                    //{
                    //    Nueva_Secuencia();
                    //    txtQRCode.Text = "Seleccione un código de Lote";
                    //    txtQRCodebis.Text = "Seleccione un código de Lote";
                    //    txtQRCodebis.Visible = true;
                    //    txtQRCode.Visible = false;

                    //}
                    //else
                    //{
                    //    txtQRCodebis.Visible = false;
                    //    txtQRCode.Visible = true;
                    //}
                    Campos_ordenados();
                    Nueva_Secuencia();
                    //Carga_Lotes("");

                    this.Session["IDLote"] = "0";
                    this.Session["IDLista"] = "0";
                    //try
                    //{
                    //    if (this.Session["MiNivel"].ToString() == null)
                    //    {
                    //        LBCheck.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        if (Convert.ToInt32(this.Session["MiNivel"].ToString()) == 9)
                    //        {
                    //            LBCheck.Visible = true;
                    //            bool check01 = chkOnOff.Checked;
                    //        }
                    //    }
                    //}
                    //catch
                    //{
                    //    LBCheck.Visible = false;
                    //    this.Session["MiNivel"] = "0";
                    //}
                    //Formularios
                    if (this.Session["Param1"].ToString() != "")
                    {
                        for (int i = 0; i < DrVariedad.Items.Count; i++)
                        {
                            string a = this.Session["Param1"].ToString();
                            if (DrVariedad.Items[i].Text.Contains(this.Session["Param1"].ToString()))
                            {
                                DrVariedad.SelectedValue = DrVariedad.Items[i].Value;
                                break;
                            }
                        }
                    }
                    //Estado
                    if (this.Session["Param3"].ToString() != "")
                    {
                        for (int i = 0; i < dtEntrada.Items.Count; i++)
                        {
                            string a = this.Session["Param3"].ToString();
                            if (dtEntrada.Items[i].Text.Contains(this.Session["Param3"].ToString()))
                            {
                                dtEntrada.SelectedValue = dtEntrada.Items[i].Value;
                                break;
                            }
                        }
                    }
                    DrVariedad_SelectedIndexChanged(null, null);
                }
                else
                {
                    try
                    {
                        if (this.Session["IDSecuencia"].ToString() == null)
                        {
                            Server.Transfer("thEnd.aspx");
                        }

                        //if (txtQRCode.Text == "")
                        //{
                        //    txtQRCode.Text = "Seleccione un código de Lote";
                        //    txtQRCodebis.Text = "Seleccione un código de Lote";
                        //    txtQRCodebis.Visible = true;
                        //    txtQRCode.Visible = false;
                        //}
                        //else
                        //{
                        //    txtQRCodebis.Visible = false;
                        //    txtQRCode.Visible = true;
                        //}
                        //try
                        //{
                        //    if (this.Session["MiNivel"].ToString() == null)
                        //    {
                        //        LBCheck.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        if (Convert.ToInt32(this.Session["MiNivel"].ToString()) == 9)
                        //        {
                        //            LBCheck.Visible = true;
                        //            bool check01 = chkOnOff.Checked;
                        //        }
                        //    }
                        //}
                        //catch
                        //{
                        //    LBCheck.Visible = false;
                        //    this.Session["MiNivel"] = "0";
                        //}
                    }
                    catch (NullReferenceException ex)
                    {
                        Lberror.Text += ex.Message;

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


            //Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
            //cuestion.Visible = false;
            //Asume.Visible = true;
            //DvPreparado.Visible = true;

        }

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Server.Transfer("RecoNomina.aspx");
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            //if (chkOnOff.Checked == false)
            //{
            //    //DrTransportista.Visible = false;
            //    BTerminado.Visible = false;
            //}
            //else
            //{
            //    if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
            //    {
            //        BTerminado.Visible = false;
            //    }
            //    else
            //    {
            //        BTerminado.Visible = true;
            //    }
            //}
            //Carga_Lotes();
        }

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
            //Carga_Lotes();
        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Carga_Lotes();
            MiOpenMenu();
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            BTElimina_Click(sender, e);
            MiOpenMenu();
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            MiOpenMenu();
        }


        private void Campos_ordenados()
        {
            ddEntradaPageSize.Items.Clear();
            ddEntradaPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddEntradaPageSize.Items.Insert(1, new ListItem("30", "30"));
            ddEntradaPageSize.Items.Insert(2, new ListItem("50", "50"));
            ddEntradaPageSize.Items.Insert(3, new ListItem("Todos", "1000"));

            dtEntrada.Items.Clear();
            dtEntrada.Items.Insert(0, new ListItem("Entradas", "1"));
            dtEntrada.Items.Insert(1, new ListItem("Finalizados", "2"));
            dtEntrada.Items.Insert(2, new ListItem("Importados", "3"));
            dtEntrada.Items.Insert(3, new ListItem("Eliminados", "4"));
        }

        protected void dtEntrada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
            {
                BTerminado.Visible = false;
                BtFinalizaTodos.Visible = false;
                BtEnviaFinalizados.Visible = false;
                Btfin.Visible = true;
                //return; 
            }

            if (dtEntrada.SelectedItem.Value == "1")
            {
                BTerminado.Visible = false;
                BtFinalizaTodos.Visible = true;
                BtEnviaFinalizados.Visible = false;
                Btfin.Visible = true;

            }
            else if (dtEntrada.SelectedItem.Value == "2")
            {
                BTerminado.Visible = true;
                BtFinalizaTodos.Visible = false;
                BtEnviaFinalizados.Visible = true;
                Btfin.Visible = true;
            }
            else if (dtEntrada.SelectedItem.Value == "3")
            {
                BTerminado.Visible = true;
                BtFinalizaTodos.Visible = false;
                BtEnviaFinalizados.Visible = false;
                Btfin.Visible = true;
            }
            else if (dtEntrada.SelectedItem.Value == "4")
            {
                BTerminado.Visible = true;
                BtFinalizaTodos.Visible = false;
                BtEnviaFinalizados.Visible = false;
                Btfin.Visible = false;
            }
            Carga_Lotes();
        }

        private void Nueva_Secuencia()
        {
            //DataTable dt3 = Main.CargaSecuencia().Tables[0];
            DataTable dt3 = Main.CargaSCANform().Tables[0];
            this.Session["Secuencias"] = dt3;
            DrVariedad.Items.Clear();

            DrVariedad.AppendDataBoundItems = true;
            //DrVariedad.Items.Add("Seleccione un tipo de lote...");
            //DrVariedad.DataValueField = "ZID";
            //DrVariedad.DataTextField = "ZDESCRIPCION";
            DrVariedad.Items.Add("Todos los Formularios");
            DrVariedad.DataValueField = "ZID";
            DrVariedad.DataTextField = "ZDESCRIPCION";

            this.Session["IDProcedimiento"] = "0";

            DrVariedad.DataSource = dt3;
            DrVariedad.DataBind();

            foreach (DataRow fila in dt3.Rows)
            {
                string miro = fila["ZDESCRIPCION"].ToString();
                if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
                {
                    this.Session["IDSecuencia"] = "1,2,3,4,5,6";
                    break;
                }
                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                {
                    this.Session["IDSecuencia"] = fila["ZID_SECUENCIA"].ToString();
                    this.Session["IDProcedimiento"] = fila["ZID_PROCEDIMIENTO"].ToString();
                    //this.Session["LaMascara"] = fila["ZMASCARA"].ToString();
                    //GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
                    break;
                }
            }
            Carga_Lotes();
            //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            //DrVariedad.Text = "";

        }


        private void Carga_Lotes2(string ID)
        {
            //Prueba con gold
            string SQL = "SELECT * FROM IMPORTACION ";
            DataTable dbA = Main.BuscaLoteGold(SQL).Tables[0];
            //DrScaneados.Items.Clear();
            //DrScaneados.DataValueField = "SERIE_LOTE";
            //DrScaneados.DataTextField = "ARTICULO";
            //// insertamos el elemento en la primera posicion:
            //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            //DrScaneados.DataSource = dbA;
            //DrScaneados.DataBind();
            //DrScaneados.SelectedIndex = -1;
            return;
            //
        }


        private void Carga_Lotes3(string ID)
        {
            //Prueba con gold
            string SQL = "SELECT * FROM IMPORTACION ";
            DataTable dbA = Main.BuscaLoteReco(SQL).Tables[0];
            //DrScaneados.Items.Clear();
            //DrScaneados.DataValueField = "SERIE_LOTE";
            //DrScaneados.DataTextField = "ARTICULO";
            //// insertamos el elemento en la primera posicion:
            //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
            //DrScaneados.DataSource = dbA;
            //DrScaneados.DataBind();
            //DrScaneados.SelectedIndex = -1;
            return;
            //
        }

        private void Carga_Impresoras(string ID)
        {
            try
            {
                string SQL = "";
                if (ID == "0")
                {
                    SQL = " SELECT DISTINCT(C.ZID) as IDPRINT, C.ZDESCRIPCION ";
                    SQL += " FROM ZFORMULARIOS A ";
                    SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_FORMULARIO ";
                    SQL += " INNER JOIN ZPRINTER C ON B.ZID_PRINTER = C.ZID ";
                }
                else
                {
                    SQL = " SELECT DISTINCT(C.ZID) as IDPRINT, C.ZDESCRIPCION ,B.ZORDEN ";
                    SQL += " FROM ZFORMULARIOS A ";
                    SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_FORMULARIO ";
                    SQL += " INNER JOIN ZPRINTER C ON B.ZID_PRINTER = C.ZID ";
                    SQL += " WHERE A.ZID = '" + ID + "'";
                    SQL += " ORDER BY B.ZORDEN ";
                    //SQL = " SELECT DISTINCT(C.ZID) as IDPRINT, C.ZDESCRIPCION ";
                    //SQL += " FROM ZFORMULARIOS A ";
                    //SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_FORMULARIO ";
                    //SQL += " INNER JOIN ZPRINTER C ON B.ZID_PRINTER = C.ZID ";
                    //SQL += " WHERE A.ZID = '" + ID + "'";
                }

                DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                //DrPrinters.Items.Clear();
                //DrPrinters.DataValueField = "IDPRINT";
                //DrPrinters.DataTextField = "ZDESCRIPCION";
                //DrPrinters.DataSource = dbB;
                //DrPrinters.DataBind();
                //Printers(DrPrinters.SelectedItem.Value);
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }

        private void Elimina_Procesados()
        {
            ////Cambia de tabla solo aquellos que se ajusten al formulario con estado a 2
            ////Queda a 2 la ultima inserción para poder revertir la importacion
            //try
            //{
            //    string SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.ESTADO ";
            //    SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
            //    SQL += " WHERE B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
            //    SQL += " AND A.TIPO_FORM = B.ZTITULO ";
            //    SQL += " AND A.ESTADO = 2 ";
            //    DataTable dbB = Main.BuscaLote(SQL).Tables[0];

            //    foreach (DataRow fila in dbB.Rows)
            //    {
            //        SQL = " INSERT INTO ZENTRADA_BORRADOS (ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
            //        SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA) ";
            //        SQL += " SELECT ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
            //        SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA ";
            //        SQL += " FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

            //        DBHelper.ExecuteNonQuery(SQL);

            //        SQL = " DELETE FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

            //        DBHelper.ExecuteNonQuery(SQL);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    TextAlerta.Text = ex.Message;
            //    alerta.Visible = true;
            //}

        }

        private void Elimina_Borrados()
        {
            ////Cambia de tabla solo aquellos que se ajusten al formulario con estado a 2
            ////Queda a 2 la ultima inserción para poder revertir la importacion
            try
            {
                string SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.ESTADO ";
                SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
                SQL += " WHERE B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                SQL += " AND A.TIPO_FORM = B.ZTITULO ";
                //SQL += " AND A.ID = " + TxtID.Text;
                DataTable dbB = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow fila in dbB.Rows)
                {
                    SQL = " INSERT INTO ZENTRADA_BORRADOS (ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA) ";
                    SQL += " SELECT ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA ";
                    SQL += " FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = " DELETE FROM ZENTRADA WHERE ID = " + fila["ID"].ToString();

                    DBHelper.ExecuteNonQuery(SQL);
                }
            }
            catch (Exception ex)
            {
                //TextAlerta.Text = ex.Message;
                //alerta.Visible = true;
                Lbmensaje.Text = ex.Message;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }

        }


        protected void gvEntrada_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddEntradaPageSize.SelectedItem.Value == "1000")
            {
                gvEntrada.AllowPaging = false;
                Carga_Lotes();
            }
            else
            {
                gvEntrada.AllowPaging = true;
                gvEntrada.PageSize = Convert.ToInt32(ddEntradaPageSize.SelectedItem.Value);
                Carga_Lotes();
            }

        }

        protected void gvEntrada_OnSorting(object sender, GridViewSortEventArgs e)
        {
           //string ColumnaSel = e.SortExpression;
            string ColumnaSel = this.Session["Columna"].ToString();
            this.Session["GridOrden"] = ColumnaSel;
            Carga_Lotes(ColumnaSel);

            //string COLUMNA = "";

            //DataTable dtCampos = Main.CargaCampos().Tables[0];
            //this.Session["Campos"] = dtCampos;

            //for (int i = 0; i <= gvEntrada.Columns.Count - 1; i++)
            //{
            //    if (gvEntrada.Columns[i].HeaderText != "")
            //    {
            //        foreach (DataRow fila in dtCampos.Rows)
            //        {
            //            if (gvEntrada.Columns[i].HeaderText == fila["ZDESCRIPCION"].ToString())
            //            {
            //                COLUMNA = fila["ZTITULO"].ToString();
            //                break;
            //            }
            //        }
            //    }
            //}

            //this.Session["GridOrden"] = COLUMNA;
            //Carga_Lotes(COLUMNA);
        }

        protected void gvEntrada_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView MiGrid = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = drv["ESTADO"].ToString();

                //if (e.Row.RowIndex == 0)
                //    e.Row.Style.Add("height", "50px");

                if (dtEntrada.SelectedItem.Value == "4")
                {
                    e.Row.BackColor = Color.FromName("#ff7e62");
                }
                else if (drv["ESTADO"].ToString() == "2")
                {
                    //e.Row.BackColor = Color.FromArgb(228, 237, 128);
                    e.Row.BackColor = Color.FromName("#c7e2f2");
                }
                else if (drv["ESTADO"].ToString() == "1")
                {
                    //e.Row.BackColor = Color.FromArgb(228, 237, 128);
                    e.Row.BackColor = Color.FromName("#ffcf62");
                }
                else if (drv["ESTADO"].ToString() == "0")
                {
                    //e.Row.BackColor = Color.FromArgb(228, 237, 128);
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
            else if (e.Row.RowType == DataControlRowType.Header)
            {

                //string imgAsc = @" <img src='images\asc.png' title='Ascending' />";
                //string imgDes = @" <img src='images\desc.png' title='Descendng' />";
                //e.Row.TableSection = TableRowSection.TableHeader;
                e.Row.BackColor = Color.FromName("#f5f5f5");
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

        protected void gvEntrada_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow row = (GridViewRow)gvEntrada.Rows[e.RowIndex];

            string miro = gvEntrada.DataKeys[e.RowIndex].Value.ToString();
            string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            SQL += " WHERE ID = " + miro;

            DBHelper.ExecuteNonQuery(SQL);
            Carga_Lotes();

            gvEntrada.EditIndex = -1;

            gvEntrada.DataBind();
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvEntrada.ClientID + "', 600, 1100 , 40 ,true); </script>", false);
        }

        protected void gvEntrada_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvEntrada.PageIndex = e.NewPageIndex;
            Carga_Lotes();
        }

        protected void gvEntrada_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvEntrada.Rows[e.RowIndex];
            string miro = gvEntrada.DataKeys[e.RowIndex].Value.ToString();

            string a = this.Session["GridOrden"].ToString();
            Carga_Lotes(this.Session["GridOrden"].ToString());
            gvEntrada.EditIndex = -1;
            gvEntrada.DataBind();
            this.Session["GridEdicion"] = "0";
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvEntrada.ClientID + "', 600, 1100 , 40 ,true); </script>", false);

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

        protected void gvEntrada_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "SubeCarga")
                {
                    decimal UNIDADES = 1.0M;
                    //string Cabecera = TxtNumero.Text;

                    index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvEntrada.Rows[index];
                    string miro = gvEntrada.DataKeys[index].Value.ToString();
                    row.BackColor = Color.FromName("#ffead1");

                    //sube la linea a la orden
                    string Numero = "";

                    Label txtBox = (gvEntrada.Rows[index].Cells[8].FindControl("LabLCarga") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        UNIDADES = Convert.ToDecimal(txtBox.Text.Replace(".", ","));
                    }
                    txtBox = (gvEntrada.Rows[index].Cells[4].FindControl("LabLNumLinea") as Label);
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

                    //Carga_tabla();
                    //Carga_tablaLista();

                    gvEntrada.EditIndex = -1;

                    gvEntrada.DataBind();
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvEntrada.ClientID + "', 600, 1100 , 40 ,true); </script>", false);


                }

                if (e.CommandName == "CargaCamion")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvEntrada.Rows[index];
                    string miro = gvEntrada.DataKeys[index].Value.ToString();
                    row.BackColor = Color.FromName("#ffead1");
                    string Numero = "";

                    //string Mira = Server.HtmlDecode(row.Cells[4].Text);
                    //if (Mira != "")
                    //{
                    //    Numero = Mira;
                    //}
                    Label txtBox = (gvEntrada.Rows[index].Cells[10].FindControl("LabLNumLinea") as Label);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }

                    string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
                    SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
                    SQL += " WHERE ID_CABECERA = " + index;
                    SQL += " AND NUMERO_LINEA = " + Numero;
                    Lberror.Text += SQL + "1- gvlista_rowcomand " + Variables.mensajeserver;
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += " 2- gvlista_rowcomand  " + Variables.mensajeserver;


                    //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
                    //SQL += " WHERE NUMERO_LINEA = " + Numero; //Miro con ID lo hace con todos

                    //DBHelper.ExecuteNonQuery(SQL);
                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;

                    //gvLista.DataBind();
                }

                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    this.Session["IDGridB"] = gvEntrada.DataKeys[index].Value.ToString();
                    this.Session["GridEdicion"] = "1";
                    //gvControl.EditIndex = -1;
                    //gvControl.DataBind();
                    
                }

                DataTable dtCampos = this.Session["Campos"] as DataTable;
                foreach (DataRow filas in dtCampos.Rows)
                {
                    if (filas["ZTITULO"].ToString() == e.CommandName)
                    {
                        this.Session["Columna"] = e.CommandName;
                        gvEntrada_OnSorting(sender, null);
                        break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Lberror.Text = "Lista RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvEntrada.DataKeys[index].Value.ToString() + " " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvEntrada_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvEntrada.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //gvLista.AutoResizeColumns();
            //gvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //gvLista.AutoResizeColumns(DataGridViewAutoSizeColumnsMo‌​de.Fill);
            int indice = gvEntrada.EditIndex = e.NewEditIndex;
            //int i = gvLista.Rows[indice].Cells.Count;

            //for (int i = 0; i < gvLista.Columns.Count; i++)
            //{
            //    gvLista.Columns[i].ItemStyle.Width = 10;
            //}

            

            Carga_Lotes(this.Session["GridOrden"].ToString());
            this.Session["GridEdicion"] = "1";
            GridViewRow row = gvEntrada.Rows[indice];
            row.BackColor = Color.FromName("#ffead1");
            //gvControl.Rows[indice].Cells[0].Enabled = false;
            //gvLista.Rows[indice].Cells[1].Enabled = false;
            //gvEntrada.Rows[indice].Cells[2].Enabled = false;
            //gvEntrada.Rows[indice].Cells[3].Enabled = false;
            //gvEntrada.Rows[indice].Cells[4].Enabled = false;
            //gvEntrada.Rows[indice].Cells[5].Enabled = false;
            //gvEntrada.Rows[indice].Cells[6].Enabled = false;
            //gvEntrada.Rows[indice].Cells[7].Enabled = false;
            //gvEntrada.Rows[indice].Cells[8].Enabled = false;
            //gvEntrada.Rows[indice].Cells[9].Enabled = false;
            //gvEntrada.Rows[indice].Cells[10].Enabled = false;
            //gvEntrada.Rows[indice].Cells[11].Enabled = false;
            //gvLista.Rows[indice].Cells[16].Enabled = false;
            //Carga_tablaLista();
            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();
        }

        protected void gvEntrada_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvEntrada.Rows[e.RowIndex];
            string miro = gvEntrada.DataKeys[e.RowIndex].Value.ToString();
            int indice = gvEntrada.EditIndex;
            string miLOTE = "";
            Object Con = DBHelper.ExecuteScalarSQL(" SELECT LOTE FROM ZENTRADA WHERE ID = " + miro, null);

            if (Con is null)
            {
                miLOTE = "";
            }
            else
            {
                miLOTE = Con.ToString();
            }
            //string miLOTE = " SELECT LOTE FROM ZENTRADA WHERE ID = " + miro;

            DataTable dtCampos = this.Session["Campos"] as DataTable;

            try
            {
                TextBox txt = new TextBox();

                string SQL = "UPDATE ZENTRADA SET ";

                for (int i = 0; i <= gvEntrada.Columns.Count - 1; i++)
                {
                    if (gvEntrada.Columns[i].HeaderText != "")
                    {
                        foreach (DataRow fila in dtCampos.Rows)
                        {
                            if (gvEntrada.Columns[i].HeaderText == fila["ZDESCRIPCION"].ToString())
                            {
                                string a = gvEntrada.Rows[indice].Cells[i].Text;
                                if (i >= 3)
                                {
                                    a = ((TextBox)(row.Cells[i].Controls[0])).Text;
                                    if (a != "")
                                    {
                                        if (SQL == "UPDATE ZENTRADA SET ")
                                        {
                                            SQL += " " + fila["ZTITULO"] + " ='" + a + "' ";
                                        }
                                        else
                                        {
                                            SQL += ", " + fila["ZTITULO"] + " ='" + a + "' ";
                                        }
                                        if(fila["ZTITULO"].ToString() == "LOTE")
                                        {
                                            if(a != miLOTE)
                                            {
                                                string SQLa = " UPDATE ZLOTESCREADOS SET ZLOTE = '" + a + "' WHERE ZLOTE ='" + miLOTE + "'";
                                                DBHelper.ExecuteNonQuery(SQLa);
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }

                SQL += " WHERE ID = " + miro;


                Variables.Error = "";

                DBHelper.ExecuteNonQuery(SQL);
                //Lberror.Text += " 1- gvEntrada_RowUpdating " + Variables.mensajeserver;

                Carga_Lotes();

                gvEntrada.EditIndex = -1;

                //DataTable dt = this.Session["MiConsulta"] as DataTable;
                //gvControl.DataSource = dt;
                gvEntrada.DataBind();
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + gvEntrada.ClientID + "', 600, 1100 , 40 ,true); </script>", false);
                Lberror.Visible = false;
            }
            catch (Exception ex)
            {
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvEntrada_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvEntrada.SelectedRow.BackColor = Color.FromName("#565656");
        }

        public class DynamicTemplateField : ITemplate
        {

            public void InstantiateIn(Control container)
            {
                //define the control to be added , i take text box as your need
                TextBox txt1 = new TextBox();
                txt1.ID = "txt1";
                container.Controls.Add(txt1);

                CheckBox chkb1 = new CheckBox();
                chkb1.ID = "chkb1";
                chkb1.AutoPostBack = true;
                chkb1.ToolTip = "Actualiza la visualización de todos los registros para poder seleccionarlos desde pantalla.";
                //chkb1.CheckedChanged += new EventHandler(sellectAll);
                container.Controls.Add(chkb1);


            }
        }

        private void CreaGridControl(DataTable dtArchivo, DataTable dtCampo)
        {
            //int i =  Convert.ToInt32(this.Session["NumeroPalet"].ToString());
            //Para dinamico Me.controls.item(contador).visible = false

            gvEntrada.AutoGenerateColumns = false;
            DropListaColumna.Items.Clear();
            DropListaColumna.DataValueField = "ZTITULO";
            DropListaColumna.DataTextField = "ZDESCRIPCION";
            //DropListaColumna.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));

            //controles
            //Según implementacion dinamicos
            //DivCampos0.Controls.Clear();
            //Según implementacion html
            //Variable opcion manual o dinamica desde web.config
            int Manual = 0;
            int cuantos = 0;
            int i = 0;
#pragma warning disable CS0219 // La variable 'a' está asignada pero su valor nunca se usa
            int a = 0;
#pragma warning restore CS0219 // La variable 'a' está asignada pero su valor nunca se usa
            Boolean Esta = false;
            //string[] CamposConsulta = null;
#pragma warning disable CS0219 // La variable 'data' está asignada pero su valor nunca se usa
            string data = "";
#pragma warning restore CS0219 // La variable 'data' está asignada pero su valor nunca se usa

            if (Manual == 0) //Manual. La variable en web.config
            {
                cuantos = dtCampo.Rows.Count;
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
                                //gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = filas["ZTITULO"].ToString(), DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });
                                MiCampo = filas["ZTITULO"].ToString();
                                //DropListaColumna.Items.Insert(i, new ListItem(filas["ZTITULO"].ToString(), filas["ZID"].ToString()));
                                Esta = true;
                                //i += 1;
                                break;
                            }
                        }
                        if (Esta == false)
                        {

                            //gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = "ID", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });
                            MiCampo = "ZID";
                            //DropListaColumna.Items.Insert(i, new ListItem(filaKey["ZTITULO"].ToString(), filaKey["ZID"].ToString()));
                            //i += 1;
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
                    //if (filas["ZTITULO"].ToString() != "ZID")
                    //{
                    //gvEntrada.Columns.Add(new ButtonField() { CommandName = filas["ZTITULO"].ToString(), HeaderText = filas["ZDESCRIPCION"].ToString(), DataTextField = filas["ZTITULO"].ToString() });

                    BoundField Campo = new BoundField();
                    Campo.DataField = filas["ZTITULO"].ToString();
                    Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                    DataControlField DataControlField = Campo;
                    gvEntrada.Columns.Add(DataControlField);
                    DropListaColumna.Items.Insert(i, new ListItem(filas["ZDESCRIPCION"].ToString(), filas["ZTITULO"].ToString()));
                    i += 1;
                    //}
                }
            }

            Lberror.Text = "";
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }


        private void Carga_Lotes(string sortExpression = null)
        {
            string SQL = "";
            //Carga ZEntrada

            //De mmomento ZENTRADA hasta generar los Menus
            this.Session["idarchivo"] = this.Session["IndexPage"].ToString();

            if (this.Session["IndexPage"].ToString() == "0")
            {
                this.Session["IndexPage"] = "23";
                this.Session["idarchivo"] = this.Session["IndexPage"].ToString();
            }

            DataTable dtCampos = null;
            DataTable dt = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dt;

            this.Session["idarchivo"] = this.Session["IndexPage"].ToString();

            SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dt);
            this.Session["Campos"] = dtCampos;


            int C = gvEntrada.Columns.Count;

            if (gvEntrada.Columns.Count == 2)
            {
                CreaGridControl(dtArchivos, dtCampos);
            }
            C = gvEntrada.Columns.Count;

            DataTable dtFormularios = Main.CargaSCANform().Tables[0];
            ////Busca en el grid
            string SQLTabla = "";
            string SQLWhere = "";
            string SQLOrder = "";
            string COLUMNA = "";
            Boolean Esta = false;

            SQL = " SELECT DISTINCT(A.LOTE) AS LOTE ";

            for (int i = 0; i <= gvEntrada.Columns.Count - 1; i++)
            {
                COLUMNA = "";

                if (gvEntrada.Columns[i].HeaderText != "")
                {
                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        if (gvEntrada.Columns[i].HeaderText == fila["ZDESCRIPCION"].ToString())
                        {
                            COLUMNA = fila["ZTITULO"].ToString();
                            break;
                        }
                    }

                    if (gvEntrada.Columns[i].HeaderText == "LOTE")
                    {
                    }
                    else if (COLUMNA  != "")
                    {
                        if(COLUMNA == "ID" && Esta == false)
                        {
                            //SQL += ", A." + COLUMNA + " AS ZID ";
                            SQL += ", A." + COLUMNA + " ";
                            Esta = true;
                        }
                        else
                        {
                            SQL += ", A." + COLUMNA + " ";
                        }

                        if (dtEntrada.SelectedItem.Value == "1")
                        {
                            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
                            {
                                SQLWhere = " WHERE (A.ESTADO = 0 OR A.ESTADO is null)  ";
                                SQLTabla = " FROM ZENTRADA A ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                            else
                            {
                                SQLWhere = " WHERE (A.ESTADO = 0 OR A.ESTADO is null)  ";
                                SQLWhere += " AND A.TIPO_FORM = B.ZTITULO ";
                                SQLWhere += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                                SQLTabla = " FROM ZENTRADA A, ZFORMULARIOS B ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                        }
                        else if (dtEntrada.SelectedItem.Value == "2")
                        {
                            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
                            {
                                SQLWhere = " WHERE A.ESTADO = 1  ";
                                SQLTabla = " FROM ZENTRADA A ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                            else
                            {
                                SQLWhere = " WHERE A.ESTADO = 1   ";
                                SQLWhere += " AND A.TIPO_FORM = B.ZTITULO ";
                                SQLWhere += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                                SQLTabla = " FROM ZENTRADA A, ZFORMULARIOS B ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                        }
                        else if (dtEntrada.SelectedItem.Value == "3")
                        {
                            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
                            {
                                SQLWhere = " WHERE A.ESTADO = 2  ";
                                SQLTabla = " FROM ZENTRADA A ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                            else
                            {
                                SQLWhere = " WHERE A.ESTADO = 2   ";
                                SQLWhere += " AND A.TIPO_FORM = B.ZTITULO ";
                                SQLWhere += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                                SQLTabla = " FROM ZENTRADA A, ZFORMULARIOS B ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                        }
                        //Borrados
                        else if (dtEntrada.SelectedItem.Value == "4")
                        {
                            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
                            {
                                SQLWhere = "";
                                SQLTabla = " FROM ZENTRADA_BORRADOS A ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                            else
                            {
                                SQLWhere = " WHERE A.TIPO_FORM = B.ZTITULO ";
                                SQLWhere += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                                SQLTabla = " FROM ZENTRADA_BORRADOS A, ZFORMULARIOS B ";
                                SQLOrder = " ORDER BY A.TIPO_FORM, A.LOTE ";
                            }
                        }
                    }
                }
            }

            //No hace falta formularios porque solo se utiliza en el Where
            //if (DrVariedad.SelectedItem.Value != "Todos los Formularios")
            //{
            //    for (int i = 0; i <= dt.Columns.Count - 1; i++)
            //    {
            //        SQL += ", B." + dt.Columns[i].ColumnName + " ";
            //    }
            //}

            if (SQL != "" && SQLTabla != "" && SQLWhere != "")
            {
                string a = "";
                try
                {
                    a = SQL + SQLTabla + SQLWhere + SQLOrder;
                    dt = Main.BuscaLote(a).Tables[0];

                    lbRowEntrada.Text = "Registros: " + dt.Rows.Count;

                    //if (sortExpression != null)
                    if (sortExpression == null || sortExpression == "")
                    {
                        gvEntrada.DataSource = dt;
                    }
                    else
                    {
                        DataView dv = dt.AsDataView();

                        if (this.Session["GridEdicion"].ToString() == "0")
                        {
                            if(this.SortDirection == "")
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                            }
                        }
                        //this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvEntrada.DataSource = dv;
                    }
                    gvEntrada.DataBind();

                    this.Session["SQL"] = a;
                    gvEntrada.EditIndex = -1;
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    string b = Main.Ficherotraza("CargaLotes --> " + ex.Message + "==> " + a);
                    throw new Exception("Error de base de datos.", ex);
                    MiCloseMenu();
                }
            }
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
                if (dtEntrada.SelectedItem.Value == "1")
                {
                    SQL += " AND A.ESTADO is not null ";
                }
                else if (dtEntrada.SelectedItem.Value == "2")
                {
                    SQL += " AND A.ESTADO = 1 ";
                }
                else if (dtEntrada.SelectedItem.Value == "3")
                {
                    SQL += " AND A.ESTADO = 2 ";
                }
                else if (dtEntrada.SelectedItem.Value == "4")
                {
                    SQL += " AND A.ESTADO = -1 ";
                }
                //if (chkOnOff.Checked == true)
                //{
                //    SQL += " AND A.ESTADO is not null ";
                //}
                //else
                //{
                //    SQL += " AND (A.ESTADO <> '2' OR A.ESTADO is null) ";
                //}
            }
            DataTable dbB = Main.BuscaLote(SQL).Tables[0];
            this.SetDropDownListItemColor(dbB);
        }

        private void Carga_LotesScaneados(string ID)
        {
            //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";
            try
            {
                string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";
                SQL += "AND ZID_SECUENCIA in (" + ID + ")";
                SQL += " ORDER BY ZLOTE ";
                DataTable dbA = Main.BuscaLote(SQL).Tables[0];
                //DrScaneados.Items.Clear();
                //DrScaneados.DataValueField = "ZID";
                //DrScaneados.DataTextField = "ZLOTE";
                //// insertamos el elemento en la primera posicion:
                //DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
                //DrScaneados.DataSource = dbA;
                //DrScaneados.DataBind();
                //DrScaneados.SelectedIndex = -1;

                ////SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

                ////SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
                ////SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                ////SQL += " WHERE A.LOTE = B.ZLOTE ";
                ////SQL += " AND B.ZID_SECUENCIA in (" + ID + ")";
                ////SQL += " AND A.ESTADO <> '2' ";
                //if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
                //{
                //    SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
                //    SQL += " FROM ZENTRADA A ";
                //    SQL += " WHERE ";
                //    SQL += "  (A.ESTADO <> '2' OR A.ESTADO is null) ";
                //}
                //else
                //{
                //    SQL = " SELECT DISTINCT(A.LOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO ";
                //    SQL += " FROM ZENTRADA A , ZFORMULARIOS B ";
                //    SQL += " WHERE B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
                //    SQL += " AND A.TIPO_FORM = B.ZTITULO ";
                //    SQL += " AND (A.ESTADO <> '2' OR A.ESTADO is null) ";
                //}


                //DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                //DrLotes.Items.Clear();
                //DrLotes.DataValueField = "ID";
                //DrLotes.DataTextField = "TODO";
                //DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
                //DrLotes.DataSource = dbB;
                //DrLotes.DataBind();
                //DrLotes.SelectedIndex = -1;

                //this.SetDropDownListItemColor(dbB);

                //SQL = "SELECT LOTE, TIPO_FORM, COUNT(*) as total FROM ZENTRADA GROUP BY LOTE, TIPO_FORM HAVING COUNT(*) > 1";
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
            catch (Exception ex)
            {
                string b = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }
        private void SetDropDownListItemColor(DataTable dt)
        {
            foreach (ListItem item in DrVariedad.Items)
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
                    //txtQRCode.Text = Cadena;
                    this.Session["LOTEenCURSO"] = Cadena;
                    //LbSecuenciaLote.Text = Cadena;
                    //LbSecuenciaLoteQR.Text = Cadena;

                    //btnGenerate_Click(null, null);
                    //LbCodigoLote.Text = "CÓDIGO LOTE:";
                }

            }
            catch (NullReferenceException ex)
            {
                Lberror.Text += ex.Message;
                //alertaErr.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }

        }

        private void Habilita_contoles()
        {
            //TxtCampo.Enabled = true;
            //TxtFecha.Enabled = true;
            //TxtVariedad.Enabled = true;
            //TxtCajas.Enabled = true;
            //TxtPlantas.Enabled = true;
            //txtQRCode.Enabled = true;
            //TxtEstado.Enabled = true;
            //TxtDispositivo.Enabled = true;
            ////btnGenerate.Visible = true;
            ////btnNuevo.Visible = true;
            ////TxtID.Enabled = true;
            //TxtForm.Enabled = true;
            //TxtManojos.Enabled = true;
            //TxtDesde.Enabled = true;
            //TxtHasta.Enabled = true;
            //TxtETDesde.Enabled = true;
            //TxtETHasta.Enabled = true;
            //TxtTuneles.Enabled = true;
            //TxtPasillos.Enabled = true;
            //TxtLoteDestino.Enabled = true;
            //TxtObservaciones.Enabled = true;
            //TxtOK.Enabled = true;
        }

        private void Deshabilita_contoles()
        {
            //TxtCampo.Enabled = false;
            //TxtFecha.Enabled = false;
            //TxtVariedad.Enabled = false;
            //TxtCajas.Enabled = false;
            //TxtEstado.Enabled = false;
            //TxtDispositivo.Enabled = false;
            //TxtPlantas.Enabled = false;
            //txtQRCode.Enabled = false;
            //TxtID.Enabled = false;
            //TxtForm.Enabled = false;
            //TxtManojos.Enabled = false;
            //TxtDesde.Enabled = false;
            //TxtHasta.Enabled = false;
            //TxtETDesde.Enabled = false;
            //TxtETHasta.Enabled = false;
            //TxtTuneles.Enabled = false;
            //TxtPasillos.Enabled = false;
            //TxtObservaciones.Enabled = false;
            //TxtOK.Enabled = false;
            //TxtLoteDestino.Enabled = false;
        }

        private void ExportGridToExcel(string NameFile)
        {
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            DataTable dt = null;
            dt = Main.BuscaLote(this.Session["SQL"].ToString()).Tables[0];
            string miro = NameFile.Substring(0, 30);
            wbook.Worksheets.Add(dt, NameFile.Substring(0, 30));
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


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //aqui
            ExportGridToExcel(DrVariedad.SelectedItem.Text + "-" + dtEntrada.SelectedItem.Text);
        }

        protected void btnValidaUser_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            //H1Normal.Visible = false;
            //H1Seleccion.Visible = false;
            //H1Red.Visible = false;
            //H1Green.Visible = false;
            string SQL = "";
            //string SQL = "DELETE FROM ZENTRADA WHERE ID = " + LbIDLote.Text;
            if (this.Session["IDLista"].ToString() == "Escaneados")
            {
                SQL = "UPDATE ZLOSTESCREADOS SET ESTADO = '2' ";
                SQL += " WHERE ID = " + LbIDLote.Text;
            }
            else if (this.Session["IDLista"].ToString() == "Lotes")
            {
                SQL = "UPDATE ZENTRADA  SET ESTADO = '2' ";
                SQL += " WHERE ID = " + LbIDLote.Text;
            }

            DBHelper.ExecuteNonQuery(SQL);
            LimpiaCajas();
            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

            //txtQRCode.Text = "Seleccione de listas QR";
            //H1Normal.Visible = true;
            //DrPrinters_Click();
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


        protected void btnModifica_Click(object sender, EventArgs e)
        {
            //if(TxtID.Text == "")
            //{
            //    alerta.Visible = true;
            //    TextAlerta.Text = "Seleccione un código QR para poder modificar.";
            //    return;
            //}
            //txtQRCodebis.Visible = false;
            //txtQRCode.Visible = true;

            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //BtGuardaLote.Visible = true;
            //BtModifica.Visible = false;
            //BtDelete.Enabled = false;
            Habilita_contoles();
            //btnGenerate_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    //btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    //btnGenerateTodo_Click(sender, e);
            //}
            //btnGenerateTodo_Click(sender, e);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //if (TxtID.Text == "")
            //{
            //    alerta.Visible = true;
            //    TextAlerta.Text = "Seleccione un código QR para poder eliminar.";
            //    return;
            //}
            //txtQRCodebis.Visible = false;
            //txtQRCode.Visible = true;

            //if (BtModifica.Visible == true && TextAlertaLog.Text != "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
            //{
            //    TextAlertaLog.Text = "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar";
            //    alerta.Visible = false;
            //    alertaErr.Visible = false;
            //    alertaLog.Visible = true;
            //    return;

            //}
            if (TextAlertaLog.Text == "Segundo control de verificación. Este registro pasará su Estado ha eliminado. Si desea eliminar el registro vuelva a pulsar el botón eliminar")
            {
                Elimina_Borrados();
                alerta.Visible = false;
                alertaErr.Visible = false;
                alertaLog.Visible = false;
            }
        }

        protected void sellectAll(object sender, EventArgs e)
        {
            if (ddEntradaPageSize.SelectedIndex != 3)
            {
                gvEntrada.AllowPaging = false;
                ddEntradaPageSize.SelectedIndex = 3;
                Carga_Lotes();
                return;
            }

            CheckBox ChkBoxHeader = (CheckBox)gvEntrada.HeaderRow.FindControl("chkb1");
            foreach (GridViewRow row in gvEntrada.Rows)
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
                if (dtEntrada.SelectedItem.Value == "4")
                {
                    row.BackColor = Color.FromName("#ff7e62");
                }
                else if (dtEntrada.SelectedItem.Value == "3")
                {
                    //e.Row.BackColor = Color.FromArgb(228, 237, 128);
                    row.BackColor = Color.FromName("#c7e2f2");
                }
                else if (dtEntrada.SelectedItem.Value == "2")
                {
                    //e.Row.BackColor = Color.FromArgb(228, 237, 128);
                    row.BackColor = Color.FromName("#ffcf62");
                }
                else if ((row.DataItemIndex % 2) == 0)
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


        protected void BTerminado_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            //BTerminado.Visible = false;
            //Btfin.Visible = true;
            string SQL = "";

            if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
            {
                return;
            }
            else
            {
                foreach (GridViewRow row in gvEntrada.Rows)
                {
                    CheckBox check = row.FindControl("chbItem") as CheckBox;

                    if (check.Checked)
                    {
                        if (dtEntrada.SelectedItem.Value == "3")
                        {
                            string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                            SQL = "UPDATE ZENTRADA  SET ESTADO = '1' ";
                            SQL += " WHERE ID = " + code;
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                        else if (dtEntrada.SelectedItem.Value == "2")
                        {
                            string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                            SQL = "UPDATE ZENTRADA  SET ESTADO = '0' ";
                            SQL += " WHERE ID = " + code;
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                        //Borrados Cambiar
                        else if (dtEntrada.SelectedItem.Value == "4")
                        {
                            string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();

                            string SQLInsert = " INSERT INTO ZENTRADA (";
                            string SQLCampos = "";
                            string SQLSelect = ") SELECT ";
                            string SQLWhere = " FROM ZENTRADA_BORRADOS WHERE ID = " + code;

                           DataTable dtCampos = this.Session["Campos"] as DataTable;
                            //Insert en otra tabla
                            for (int i = 3; i <= gvEntrada.Columns.Count - 1; i++)
                            {
                                if (gvEntrada.Columns[i].HeaderText != "")
                                {
                                    foreach (DataRow fila in dtCampos.Rows)
                                    {
                                        if (gvEntrada.Columns[i].HeaderText == fila["ZDESCRIPCION"].ToString())
                                        {
                                            if (SQLCampos == "")
                                            {
                                                SQLCampos = fila["ZTITULO"] + " ";
                                            }
                                            else
                                            {
                                                SQLCampos += ", " + fila["ZTITULO"] + " ";
                                            }
                                            break;
                                        }
                                    }
                                }
                            }

                            SQL = SQLInsert + SQLCampos + SQLSelect + SQLCampos + SQLWhere;

                            DBHelper.ExecuteNonQuery(SQL);

                            SQL = " DELETE FROM ZENTRADA_BORRADOS WHERE ID = " + code;
                            DBHelper.ExecuteNonQuery(SQL);
                        }
                    }
                }
            }
            Carga_Lotes();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {

            alerta.Visible = false;
            alertaLog.Visible = false;
            alertaErr.Visible = false;
            //btProcesa.Visible = false;
            //btPorcesa.Visible = false;
            //btNew.Enabled = false;

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


            //if (BtModifica.Visible == true)
            //{
            //    if (TxtForm.Enabled == true)
            //    {
            //        Oculta_Datos(1);
            //    }
            //    else
            //    {
            //        Oculta_Datos(0);
            //    }

            //    DataTable dt3 = Main.CargaSecuencia().Tables[0];
            //    this.Session["Secuencias"] = dt3;
            //    foreach (DataRow fila in dt3.Rows)
            //    {
            //        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
            //        {
            //            this.Session["IDSecuencia"] = fila["ZID"].ToString();
            //            this.Session["LaMascara"] = fila["ZMASCARA"].ToString();
            //            GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
            //            break;
            //        }
            //    }

            //}
            //else
            //{
            //    string SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO) ";
            //    SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0)";
            //    DBHelper.ExecuteNonQuery(SQL);

            //    if (TxtForm.Enabled == true)
            //    {
            //        Oculta_Datos(1);
            //    }
            //    else
            //    {
            //        Oculta_Datos(0);
            //    }

            //    //Nuevo numero de secuencia
            //    if (DrVariedad.SelectedItem.Value == "-1")
            //    {
            //        alerta.Visible = true;
            //        Nueva_Secuencia();
            //    }
            //    else
            //    {
            //        SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
            //        DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            //        foreach (DataRow fila in dbA.Rows)
            //        {
            //            this.Session["NumeroSecuencia"] = fila["ZSECUENCIA"].ToString();
            //            break;
            //        }

            //        int AA = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
            //        SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + AA + "' ";
            //        SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
            //        //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            //        DBHelper.ExecuteNonQuery(SQL);
            //        DataTable dt3 = Main.CargaSecuencia().Tables[0];
            //        this.Session["Secuencias"] = dt3;
            //        foreach (DataRow fila in dt3.Rows)
            //        {
            //            if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
            //            {
            //                this.Session["IDSecuencia"] = fila["ZID"].ToString();
            //                this.Session["LaMascara"] = fila["ZMASCARA"].ToString();
            //                //GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
            //                break;
            //            }
            //        }
            //        GeneraSecuencia(this.Session["LaMascara"].ToString(), Convert.ToString(AA));
            //    }
            //}
            //Carga_Lotes(this.Session["IDSecuencia"].ToString());

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
        //        //btProcesa.Visible = false;
        //        //btPorcesa.Visible = false;
        //        //BTerminado.Visible = false;
        //        //Btfin.Visible = false;
        //    }
        //}
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
        //    //btnNuevoLote.Visible = false;
        //    BtGuardaLote.Visible = true;
        //    BtModifica.Visible = false;
        //    BtCancelaLote.Visible = true;
        //    BtDelete.Visible = false;
        //    //btGeneraNew.Visible = true;
        //    //Btfin.Visible = false;
        //    //BTerminado.Visible = false;
        //    LimpiaCajas();
        //    TxtForm.Text = "Independiente";
        //}

        protected void btnCancelaLote_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtDelete.Enabled = true;
            Deshabilita_contoles();

            //btnNuevoLote.Visible = true;
            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtCancelaLote.Visible = false;
            //btGeneraNew.Visible = false;
            //BtDelete.Visible = true;
        }

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
        //protected void BtGuardaLote_Click(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

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
        //    txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;

        //    //string SQL = "INSERT INTO ZENTRADA (TIPO_FORM, FECHA, TIPO_PLANTA, VARIEDAD, LOTE, UNIDADES, NUM_UNIDADES, MANOJOS, ";
        //    //SQL += "DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS, OBSERVACIONES, OK) ";
        //    //SQL += " VALUES ('" + TxtForm.Text + "','" + TxtFecha.Text + "','" + TxtCampo.Text + "','" + TxtVariedad.Text + "','" + txtQRCode.Text + "','" + TxtCajas.Text + "',";
        //    //SQL += "'" + TxtPlantas.Text + "','" + TxtManojos.Text + "','" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + TxtETDesde.Text + "','" + TxtETHasta.Text + "',";
        //    //SQL += "'" + TxtTuneles.Text + "','" + TxtPasillos.Text + "','" + TxtObservaciones.Text + "','" + TxtOK.Text + "')";

        //    //DBHelper.ExecuteNonQuery(SQL);
        //    //Si no modifica el estado y viene vacio, como existe el formulario en edicion añado un cero
        //    if(TxtEstado.Text == "" || TxtEstado.Text == null)
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

        //    btnCancelaLote_Click(sender, e);

        //    //LimpiaCajas();



        //    //Btfin.Visible = false;
        //    //BTerminado.Visible = true;
        //    //btnGenerate_Click(sender, e);
        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        //btnGeneraTodoPerf_Click(sender, e);
        //    }
        //    else
        //    {
        //        //btnGenerateTodo_Click(sender, e);
        //    }
        //    //btnGenerateTodo_Click(sender, e);
        //    alerta.Visible = false;
        //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //    Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        //}
        protected void BTfin_Click(object sender, EventArgs e)
        {
            int A = 0;
            foreach (GridViewRow row in gvEntrada.Rows)
            {
                CheckBox check = row.FindControl("chbItem") as CheckBox;

                if (check.Checked)
                {
                    A += 1;
                }
            }

            Lbmensaje.Text = "Se eliminarán " + A + " registros seleccionados, ¿Desea continuar?";
            cuestion.Visible = true;
            Asume.Visible = false;
            windowmessaje.Visible = true;
            MiCloseMenu();
        }

        protected void BTElimina_Click(object sender, EventArgs e)
        {
            //elimina los chequeados
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            string SQL = "";

            foreach (GridViewRow row in gvEntrada.Rows)
            {
                CheckBox check = row.FindControl("chbItem") as CheckBox;

                if (check.Checked)
                {
                    //Insert en otra tabla
                    string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                    SQL = " INSERT INTO ZENTRADA_BORRADOS (ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA) ";
                    SQL += " SELECT ID,TIPO_FORM,FECHA,TIPO_PLANTA,VARIEDAD,LOTE,LOTEDESTINO,UNIDADES,NUM_UNIDADES, ";
                    SQL += " MANOJOS,DESDE,HASTA,ETDESDE,ETHASTA,TUNELES,PASILLOS,OBSERVACIONES,OK,DeviceID,DeviceName,SendTime,ReceiveTime,Barcode,ESTADO,FECHAEXP,ID_SECUENCIA ";
                    SQL += " FROM ZENTRADA WHERE ID = " + code;

                    DBHelper.ExecuteNonQuery(SQL);

                    SQL = " DELETE FROM ZENTRADA WHERE ID = " + code;
                    DBHelper.ExecuteNonQuery(SQL);

                    //string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();     //get datakey
                    //    SQL = " UPDATE ZENTRADA SET ESTADO = -1 WHERE ID = " + code;
                }
            }


            Carga_Lotes();

            if (DrVariedad.SelectedItem.Value == "5")
            {
                BtEnviaFinalizados.Enabled = false;
            }
            else
            {
                BtEnviaFinalizados.Enabled = true;
            }

        }
        //protected void btnPorcesa_Click(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    btnPrint2.Visible = false;
        //    //BTerminado.Visible = false;
        //    //Btfin.Visible = false;
        //    //btProcesa.Visible = false;
        //    //btPorcesa.Visible = false;
        //    alertaLog.Visible = false;
        //    //btNew.Enabled = false;

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];


        //    SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //    DataTable dbB = Main.BuscaLote(SQL).Tables[0];
        //    //foreach (DataRow fila in dbB.Rows)
        //    //{
        //    //    btNew.Enabled = true;
        //    //    break;
        //    //}

        //    Boolean Esta = false;
        //    SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow fila in dbA.Rows)
        //    {
        //        if (fila["LOTE"].ToString() == txtQRCode.Text)
        //        {
        //            Esta = true;
        //            LbIDLote.Text = fila["ID"].ToString();
        //            this.Session["IDSecuencia"] = fila["ZID"].ToString();

        //            TxtCampo.Text = fila["TIPO_PLANTA"].ToString();
        //            TxtFecha.Text = fila["FECHA"].ToString();
        //            TxtVariedad.Text = fila["VARIEDAD"].ToString();
        //            TxtCajas.Text = fila["UNIDADES"].ToString();//* Tabla BANDEJAS 
        //            TxtDesde.Text = fila["DESDE"].ToString();


        //            TxtID.Text = fila["ID"].ToString();
        //            TxtForm.Text = fila["TIPO_FORM"].ToString();
        //            TxtManojos.Text = fila["MANOJOS"].ToString();
        //            TxtDesde.Text = fila["DESDE"].ToString();
        //            TxtHasta.Text = fila["HASTA"].ToString();
        //            TxtETDesde.Text = fila["ETDESDE"].ToString();
        //            TxtETHasta.Text = fila["ETHASTA"].ToString();
        //            TxtTuneles.Text = fila["TUNELES"].ToString();
        //            TxtPasillos.Text = fila["PASILLOS"].ToString();
        //            TxtObservaciones.Text = fila["OBSERVACIONES"].ToString();
        //            TxtOK.Text = fila["OK"].ToString();



        //            if (TxtCajas.Text == "CAJAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + fila["UNIDADES"].ToString() + " " + fila["NUM_UNIDADES"].ToString(); // fila["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();

        //            }
        //            if (TxtCajas.Text == "PLANTAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + fila["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();
        //            }

        //            TxtPlantas.Text = fila["NUM_UNIDADES"].ToString();
        //            LbCampoS.Text = "CAMPO O SECTOR: " + fila["DESDE"].ToString();
        //            try
        //            {
        //                foreach (DataRow fila2 in dbP.Rows)
        //                {
        //                    if (fila2["ZTIPO_PLANTA"].ToString() == fila["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == fila["UNIDADES"].ToString())
        //                    {
        //                        if (fila["UNIDADES"].ToString() == "PLANTAS")
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();
        //                            break;
        //                        }
        //                        else if (fila["UNIDADES"].ToString() == "CAJAS")
        //                        {
        //                            if (fila["MANOJOS"].ToString() == "0" || fila["MANOJOS"].ToString() == "" || fila["MANOJOS"].ToString() == null)
        //                            {
        //                                LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(fila["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                            }
        //                            else
        //                            {
        //                                foreach (DataRow fila3 in dbP.Rows)
        //                                {
        //                                    if (fila3["ZTIPO_PLANTA"].ToString() == fila["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                    {
        //                                        int NN = Convert.ToInt32(fila["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                        LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(fila["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        break;
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Lberror.Text = ex.Message;
        //            }
        //            LbPlantaS.Text = "TIPO PLANTA: " + fila["TIPO_PLANTA"].ToString();
        //            LbFechaS.Text = "FECHA CORTE: " + fila["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + fila["VARIEDAD"].ToString();

        //            //if (TxtID.Enabled == true)
        //            //{
        //            //    Oculta_Datos(1);
        //            //}
        //            //else
        //            //{
        //            //    Oculta_Datos(0);
        //            //}

        //            Lbcompleto.Text = "QR COMPLETO";
        //            this.Session["Procesa"] = "1";
        //            btnGenerate_Click(sender, e);
        //            btnGenerateTodo_Click(sender, e);
        //            this.Session["Procesa"] = "0";
        //            //if (fila["ESTADO"].ToString() == "" || fila["ESTADO"].ToString() == null)
        //            //{
        //            //    BTerminado.Visible = true;
        //            //}
        //            //else
        //            //{
        //            //    Btfin.Visible = true;
        //            //}
        //            break;
        //        }
        //    }
        //    if (Esta == false)
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = false;
        //        H1Red.Visible = true;
        //        H1Green.Visible = false;

        //        //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //        //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

        //        btnGenerate_Click(sender, e);
        //        TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
        //        TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
        //        TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
        //        TextAlerta.Text = "";
        //        alertaLog.Visible = false;
        //        alerta.Visible = false;
        //        alertaErr.Visible = true;
        //        btProcesa.Visible = false;
        //        btPorcesa.Visible = true;
        //    }
        //    else
        //    {
        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = 1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        //alerta.Visible = false;
        //        //alertaErr.Visible = false;
        //    }
        //    if(btNew.Enabled == false)
        //    {
        //        SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
        //        SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
        //        DBHelper.ExecuteNonQuery(SQL);

        //        SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //        DataTable dbC = Main.BuscaLote(SQL).Tables[0];
        //        foreach (DataRow fila in dbC.Rows)
        //        {
        //            this.Session["NumeroSecuencia"] = fila["ZSECUENCIA"].ToString();
        //            break;
        //        }

        //        int AA = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
        //        SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + AA + "' ";
        //        SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
        //        DBHelper.ExecuteNonQuery(SQL);
        //        btNew.Enabled = true;
        //    }

        //    Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //}

        //protected void btnGeneraNew_Click(object sender, EventArgs e)
        //{
        //    btnGenerate_Click(sender, e);
        //    if (DrPrinters.SelectedItem.Value == "4")
        //    {
        //        btnGeneraTodoPerf_Click(sender, e);
        //    }
        //    else
        //    {
        //        btnGenerateTodo_Click(sender, e);
        //    }
        //}           
        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    //Genera la secuencia QR de la etiqueta Lote
        //    if (this.Session["Procesa"].ToString() == "0")
        //    {
        //        H1Normal.Visible = false;
        //        H1Seleccion.Visible = true;
        //        H1Red.Visible = false;
        //        H1Green.Visible = false;
        //        DrPrinters_Click();

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
        //    //LbSecuenciaLote.Text = code;
        //    //LbSecuenciaLoteQR.Text = code;
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //    try
        //    {
        //        if (DrPrinters.SelectedItem.Value == "2" )
        //        {
        //            imgBarCode.Height =150;
        //            imgBarCode.Width = 150;
        //        }
        //        else if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 250;
        //            imgBarCode.Width = 250;
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
        //        else if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 250;
        //            imgBarCode.Width = 250;
        //        }
        //        else
        //        {
        //            TextAlertaErr.Text = "El valor de las medidas para la imagen QR Lote no son correctas. Se establece por defecto a 300 X 300 pixeles. El Error :" + a.Message;
        //            TxAlto.Text = "300";
        //            TxAncho.Text = "300";
        //            alertaErr.Visible = true;
        //        }
        //    }
        //    using (Bitmap bitMap = qrCode.GetGraphic(40))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
        //        {
        //            PlaceHolder1.Controls.Add(imgBarCode);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "2")
        //        {
        //            PlaceHolderQR.Controls.Add(imgBarCode);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "3")
        //        {
        //            pnlContentsFT.Controls.Add(imgBarCode);
        //        }
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            PlaceHolderPaletAlv.Controls.Add(imgBarCode);
        //        }
        //    }

        //    LbCodigoLote.Text = "CÓDIGO LOTE:";
        //    //LbCodigoLoteQR.Text = "CÓDIGO LOTE 2:";
        //    //Comentar en produccion
        //    //btnGenerateTodo_Click(sender, e);
        //}

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
        //        code += TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
        //    }
        //    else
        //    {
        //        code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
        //        LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
        //        code += Label4.Text + TxtCampo.Text + Environment.NewLine;
        //        LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
        //        code += Label5.Text + TxtFecha.Text + Environment.NewLine;
        //        code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
        //        LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
        //        code += LbCajasS.Text + Environment.NewLine;
        //        code += LbPlantasS.Text + Environment.NewLine;
        //    }


        //    H1Normal.Visible = false;
        //    H1Seleccion.Visible = false;
        //    H1Red.Visible = false;
        //    H1Green.Visible = true;
        //    DrPrinters_Click();

        //    TextAlertaErr.Text = "";


        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
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

        //    using (Bitmap bitMap = qrCode.GetGraphic(40))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }
        //        PlaceHolder2.Controls.Add(imgBarCode);
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

        //    if (DrPrinters.SelectedItem.Value == "6")
        //    {
        //        code = TxtVariedad.Text; // + Environment.NewLine;
        //    }


        //    //if (CodigoError != "")
        //    //{
        //    //    //No se puede generar el código QR total por tener los campos siguientes vacios: CodigoError
        //    //    TextAlertaErr.Text = "No se puede generar el código QR total por tener los campos siguientes vacios: " + CodigoError;
        //    //    TextAlertaErr.Text += "Genere un registro desde formularios de Scan-IT desde el Móvil, envielo y pruebe nuevamente desde este botón. " + CodigoError;
        //    //    TextAlerta.Text = "";
        //    //    alertaErr.Visible = true;
        //    //    btnPrint2.Visible = false;
        //    //    //BTerminado.Visible = false;
        //    //    //btProcesa.Visible = true;
        //    //    return;
        //    //}
        //    //else
        //    //{
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
        //    //}
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
        //    System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

        //    try
        //    {
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            imgBarCode.Height = 100;
        //            imgBarCode.Width = 100;
        //        }
        //        else
        //        {
        //            imgBarCode.Height = Convert.ToInt32(TxAltoT.Text);
        //            imgBarCode.Width = Convert.ToInt32(TxAnchoT.Text);
        //        }

        //    }
        //    catch (Exception a)
        //    {
        //        TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
        //        TxAltoT.Text = "200";
        //        TxAnchoT.Text = "200";
        //        alertaErr.Visible = true;
        //    }

        //    using (Bitmap bitMap = qrCode.GetGraphic(40))
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //            byte[] byteImage = ms.ToArray();
        //            imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
        //        }

        //        //if (DrPrinters.SelectedItem.Value == "1")
        //        //{
        //        //PlaceHolder2.Controls.Add(imgBarCode);
        //        //}
        //        //if (DrPrinters.SelectedItem.Value == "2")
        //        //{
        //        //    PlaceHolderQR.Controls.Add(imgBarCode);
        //        //}
        //        //if (DrPrinters.SelectedItem.Value == "3")
        //        //{
        //        //    pnlContentsFT.Controls.Add(imgBarCode);
        //        //}
        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {

        //            PlaceHolderPaletAlvMin.Controls.Add(imgBarCode);
        //        }
        //        else
        //        {
        //            PlaceHolder2.Controls.Add(imgBarCode);
        //        }
        //    }
        //}

        private void BuscaDuplicados()
        {

        }
        private void LimpiaCajas()
        {
            try
            {
                //txtQRCode.Text = "";
                //TxtCampo.Text = "";
                //TxtFecha.Text = "";
                //TxtVariedad.Text = "";
                //TxtCajas.Text = "";
                //TxtPlantas.Text = "";
                //LbCampoS.Text = "";
                //LbFechaS.Text = "";
                //LbPlantaS.Text = "";
                //LbVariedadS.Text = "";
                //LbCajasS.Text = "";
                //LbPlantasS.Text = "";
                //Lbcompleto.Text = "";
                //LbSecuenciaLote.Text = "";
                //LbSecuenciaLoteQR.Text = "";
                //TxtEstado.Text = "";
                //TxtDispositivo.Text = "";
                //TxtLoteDestino.Text = "";

                //LbCodeQRPalteAlv.Text = "";
                //LbTipoPlantaP.Text = "";
                //LbVariedadP.Text = "";
                //LbVariedadS.Text = "";
                //lbUnidadesP.Text = "";
                //lbNumPlantasP.Text = "";

                //TxtID.Text = "";
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
                //LbCodigoLote.Text = "SIN CÓDIGO LOTE";
            }
            catch (NullReferenceException ex)
            {
                Lberror.Text += ex.Message;
            }
        }

        protected void btnRestoreTodo_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            BTerminado.Visible = false;
            Btfin.Visible = false;
            //if (LbIDLote.Text == "") { return; }
            string SQL = "";

            DataTable dt3 = Main.CargaSCANform().Tables[0];

            foreach (DataRow fila in dt3.Rows)
            {
                if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
                {
                    SQL = "UPDATE ZENTRADA SET ESTADO = '0' ";
                    SQL += " WHERE ESTADO <> '2'";
                    DBHelper.ExecuteNonQuery(SQL);
                    break;
                }

                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                {
                    SQL = "UPDATE ZENTRADA SET ESTADO = '0' ";
                    SQL += " WHERE TIPO_FORM = '" + fila["ZTITULO"].ToString() + "'";
                    SQL += " AND ESTADO <> '2'";
                    DBHelper.ExecuteNonQuery(SQL);
                    break;
                }
            }

            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

            //BtFinalizalote.Text = "Finaliza este Lote";
            //BtFinalizalote.Attributes["class"] = "btn btn-warning  btn-block";
            //BtFinalizalote.Enabled = false;
            if (DrVariedad.SelectedItem.Value == "5")
            {
                BtEnviaFinalizados.Enabled = false;
            }
            else
            {
                BtEnviaFinalizados.Enabled = true;
            }
            //BtEnviaFinalizados.Enabled = true;

        }



        protected void btnRestore_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            BTerminado.Visible = false;
            Btfin.Visible = false;
            //if(LbIDLote.Text == "") { return; }
            string SQL = "UPDATE ZENTRADA  SET ESTADO = '0' ";
            SQL += " WHERE ID = " + LbIDLote.Text;
            //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            DBHelper.ExecuteNonQuery(SQL);
            //Btfin.Visible = true;
            //BTerminado.Visible = false;

            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

            //BtFinalizalote.Text = "Finaliza este Lote";
            //BtFinalizalote.Attributes["class"] = "btn btn-warning  btn-block";
            //BtFinalizalote.Enabled = false;
            if (DrVariedad.SelectedItem.Value == "5")
            {
                BtEnviaFinalizados.Enabled = false;
            }
            else
            {
                BtEnviaFinalizados.Enabled = true;
            }
            //BtEnviaFinalizados.Enabled = true;

        }

        protected void ListaColumnas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sorting
            

        }

        protected void btnAsc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "ASC";
            string ColumnaSel = DropListaColumna.SelectedValue.ToString();
            this.Session["GridOrden"] = ColumnaSel;
            Carga_Lotes(ColumnaSel);
        }

        protected void btnDesc_Click(object sender, EventArgs e)
        {
            //sorting
            this.SortDirection = "DESC";
            string ColumnaSel = DropListaColumna.SelectedValue.ToString();
            this.Session["GridOrden"] = ColumnaSel;
            Carga_Lotes(ColumnaSel);

        }

        


        protected void DrDuplicados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BuscaLote(string SQL)

        }

        protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            DataTable dt = Main.CargaSCANform().Tables[0];



            //try
            //{
            if (dt == null)
            {
                DataTable dt3 = Main.CargaSCANform().Tables[0];

                foreach (DataRow fila in dt3.Rows)
                {
                    if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
                    {
                        this.Session["IDSecuencia"] = "1,2,3,4,5,6";

                        this.Session["IDProcedimiento"] = "0";
                        dtEntrada_SelectedIndexChanged(null, null);
                        //Carga_Lotes(); // this.Session["IDSecuencia"].ToString());
                        break;
                    }

                    if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                    {
                        this.Session["IDSecuencia"] = fila["ZSECUENCIAS"].ToString();

                        dtEntrada_SelectedIndexChanged(null, null);
                        //Carga_Lotes(); // this.Session["IDSecuencia"].ToString());
                        this.Session["IDProcedimiento"] = fila["ZID_PROCEDIMIENTO"].ToString();
                        break;
                    }
                }
            }
            else
            {
                foreach (DataRow fila in dt.Rows)
                {
                    if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
                    {
                        this.Session["IDSecuencia"] = "1,2,3,4,5,6";

                        dtEntrada_SelectedIndexChanged(null, null);
                        //Carga_Lotes(); 
                        this.Session["IDProcedimiento"] = "0";
                        break;
                    }
                    string miro = fila["ZID"].ToString();
                    if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                    {
                        this.Session["IDSecuencia"] = fila["ZSECUENCIAS"].ToString();
                        dtEntrada_SelectedIndexChanged(null, null);
                        //Carga_Lotes(); 
                        this.Session["IDProcedimiento"] = fila["ZID_PROCEDIMIENTO"].ToString();
                        break;
                    }
                }
            }
        }


        //protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;
        //    txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;

        //    LimpiaCajas();
        //    btnCancelaLote_Click(sender, e);
        //    DataTable dt = Main.CargaSCANform().Tables[0];
        //    Boolean Esta = false;
        //    H1Normal.Visible = false;
        //    H1Seleccion.Visible = false;
        //    H1Red.Visible = false;
        //    H1Green.Visible = false;
        //    BtnLanzaPro.Visible = false;
        //    BTerminado.Visible = false;
        //    Btfin.Visible = false;

        //    try
        //    {
        //        if (dt == null)
        //        {
        //            DataTable dt3 = Main.CargaSCANform().Tables[0];
        //            foreach (DataRow fila in dt3.Rows)
        //            {
        //                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //                {
        //                    this.Session["IDSecuencia"] = fila["ZID"].ToString();
        //                    Carga_Lotes(fila["ZID"].ToString());
        //                    //if (fila["ZID"].ToString().ToString() == "1" || fila["ZID"].ToString().ToString() == "3")
        //                    //{
        //                    //    BtnLanzaPro.Visible = true;
        //                    //}

        //                    if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
        //                    {
        //                        LbCodigoLote.Text = "SIN CÓDIGO LOTE";
        //                        txtQRCode.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Visible = true;
        //                        txtQRCode.Visible = false;
        //                        Esta = true;
        //                    }
        //                    else
        //                    {
        //                        LbCodigoLote.Text = "CÓDIGO LOTE:";
        //                        txtQRCode.Text = "Seleccione de listas QR";
        //                        txtQRCodebis.Text = "Seleccione de listas QR";
        //                        txtQRCodebis.Visible = true;
        //                        txtQRCode.Visible = false;

        //                        Esta = true;
        //                    }

        //                    if(Esta == false)
        //                    {
        //                        txtQRCode.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Visible = true;
        //                        txtQRCode.Visible = false;

        //                    }
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            foreach (DataRow fila in dt.Rows)
        //            {
        //                if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //                {
        //                    this.Session["IDSecuencia"] = fila["ZID"].ToString();
        //                    Carga_Lotes(fila["ZID"].ToString());

        //                    //if (fila["ZID"].ToString().ToString() == "1" || fila["ZID"].ToString().ToString() == "3")
        //                    //{
        //                    //    BtnLanzaPro.Visible = true;
        //                    //}

        //                    if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
        //                    {
        //                        LbCodigoLote.Text = "SIN CÓDIGO LOTE";
        //                        txtQRCode.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Text = "Seleccione un Tipo de Lote";
        //                        txtQRCodebis.Visible = true;
        //                        txtQRCode.Visible = false;
        //                        Esta = true;
        //                    }
        //                    else
        //                    {
        //                        LbCodigoLote.Text = "CÓDIGO LOTE:";
        //                        txtQRCode.Text = "Seleccione de listas QR";
        //                        txtQRCodebis.Text = "Seleccione de listas QR";
        //                        txtQRCodebis.Visible = true;
        //                        txtQRCode.Visible = false;
        //                        Esta = true;
        //                    }


        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception Ex)
        //    {
        //        alertaErr.Visible = true;
        //        TextAlertaErr.Text = Ex.Message;
        //    }
        //    H1Normal.Visible = true;

        //    if (Esta == false)
        //    {
        //        txtQRCode.Text = "Seleccione un Tipo de Lote";
        //        txtQRCodebis.Text = "Seleccione un Tipo de Lote";
        //        txtQRCodebis.Visible = true;
        //        txtQRCode.Visible = false;
        //    }

        //    //try
        //    //{

        //    //    DataTable dt = Main.CargaSecuencia().Tables[0];

        //    //    //DataTable dt = this.Session["Secuencias"] as DataTable;
        //    //    //if (dt == null)
        //    //    //{
        //    //    //    DataTable dt3 = Main.CargaSecuencia().Tables[0];
        //    //    //    foreach (DataRow fila in dt3.Rows)
        //    //    //    {
        //    //    //        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //    //    //        {

        //    //    //            if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
        //    //    //            {
        //    //    //                btNew.Enabled = false;
        //    //    //                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
        //    //    //            }
        //    //    //            else
        //    //    //            {
        //    //    //                GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
        //    //    //                btNew.Enabled = true;
        //    //    //                LbCodigoLote.Text = "CÓDIGO LOTE:";
        //    //    //                Carga_Lotes();
        //    //    //            }
        //    //    //            break;
        //    //    //        }
        //    //    //    }
        //    //    //}
        //    //    //else
        //    //    //{
        //    //    Esta = false;
        //    //    foreach (DataRow fila in dt.Rows)
        //    //    {
        //    //        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //    //        {
        //    //            this.Session["IDSecuencia"] = fila["ZID"].ToString();

        //    //            if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
        //    //            {
        //    //                //lbBuscaCod.Text = "Códigos QR:";
        //    //                //btNew.Enabled = false;
        //    //                Esta = false;
        //    //                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
        //    //            }
        //    //            else
        //    //            {
        //    //                //lbBuscaCod.Text = "Códigos QR con Tipo Lote " + fila["ZDESCRIPCION"].ToString() + ":";
        //    //                //lbBuscaCod.Text = "Códigos QR:";
        //    //                GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
        //    //                //btNew.Enabled = false;
        //    //                LbCodigoLote.Text = "CÓDIGO LOTE:";
        //    //                Esta = true;
        //    //                Carga_Lotes(fila["ZID"].ToString());
        //    //                H1Normal.Visible = false;
        //    //                H1Seleccion.Visible = true;
        //    //                H1Red.Visible = false;
        //    //                H1Green.Visible = false;
        //    //            }

        //    //            break;
        //    //        }
        //    //    }
        //    //    if (Esta == false)
        //    //    {
        //    //        //btNew.Enabled = false;
        //    //        //BTerminado.Visible = false;
        //    //        LbSecuenciaLote.Text = "";
        //    //        LbCodigoLote.Text = "SIN CÓDIGO LOTE";
        //    //        txtQRCode.Text = "";
        //    //        alerta.Visible = false;
        //    //        alertaErr.Visible = false;
        //    //        alertaLog.Visible = false;
        //    //        LbCampoS.Text = "";
        //    //        LbFechaS.Text = "";
        //    //        LbPlantaS.Text = "";
        //    //        LbVariedadS.Text = "";
        //    //        LbCajasS.Text = "";
        //    //        LbPlantasS.Text = "";
        //    //        TxtCampo.Text = "";
        //    //        TxtFecha.Text = "";
        //    //        TxtVariedad.Text = "";
        //    //        TxtCajas.Text = "";
        //    //        TxtPlantas.Text = "";
        //    //        DrLotes.Items.Clear();
        //    //        LbDuplicados.Text = "No";
        //    //        LbDuplicados.ForeColor = Color.Black;

        //    //        H1Normal.Visible = true;
        //    //        H1Seleccion.Visible = false;
        //    //        H1Red.Visible = false;
        //    //        H1Green.Visible = false;
        //    //        //lbBuscaCod.Text = "Códigos QR:";
        //    //    }

        //    //}
        //    //catch (NullReferenceException ex)
        //    //{
        //    //    Lberror.Text += ex.Message;
        //    //    //alerta.Visible = true;
        //    //    //TextAlertaErr.Text = ex.Message;
        //    //}
        //    //}
        //}

        protected void BtFinalizalote_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            BTerminado.Visible = false;
            Btfin.Visible = false;

            string SQL = "UPDATE ZENTRADA  SET ESTADO = '1' ";
            SQL += " WHERE ID = " + LbIDLote.Text;
            //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            DBHelper.ExecuteNonQuery(SQL);
            //Btfin.Visible = true;
            //BTerminado.Visible = false;
            //btnGenerate_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else 
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}

            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

            //BtFinalizalote.Text = "Lote Finalizado";
            //BtFinalizalote.Attributes["class"] = "btn btn-warning  btn-block";
            //BtFinalizalote.Enabled = false;
            if (DrVariedad.SelectedItem.Value == "5")
            {
                BtEnviaFinalizados.Enabled = false;
            }
            else
            {
                BtEnviaFinalizados.Enabled = true;
            }
            //BtEnviaFinalizados.Enabled = true;

        }

        protected void BtFinalizaTodos_Click(object sender, EventArgs e)
        {
            ////Busca en el grid
            //foreach (DataRow filas in gvEntrada.Rows)
            //{
            //    if(gvEntrada.Columns[i].HeaderText == "")
            //    {

            //    }
            //}

            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            //BTerminado.Visible = false;
            //Btfin.Visible = false;
            string SQL = "";
            Boolean esta = false;

            if (DrVariedad.SelectedItem.Text == "Todos los Formularios")
            {
                //Muestra Mensaje jose
                Lbmensaje.Text = "No se finalizaran Lotes si no selecciona un Formulario";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                return;
            }
            else
            {

                foreach (GridViewRow row in gvEntrada.Rows)
                {
                    CheckBox check = row.FindControl("chbItem") as CheckBox;

                    if (check.Checked)
                    {
                        string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();     //get datakey
                        SQL = " UPDATE ZENTRADA SET ESTADO = '1' WHERE ID = " + code;
                        DBHelper.ExecuteNonQuery(SQL);
                        esta = true;
                    }
                }
            }
            if (esta == false)
            {
                Lbmensaje.Text = "No hay Lotes seleccionados para finalizar.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }


            Carga_Lotes();

            if (DrVariedad.SelectedItem.Value == "5")
            {
                BtEnviaFinalizados.Enabled = false;
            }
            else
            {
                BtEnviaFinalizados.Enabled = true;
            }


        }

        protected void BtEnviaFinalizados_Click(object sender, EventArgs e)
        {
            //Procedimientos Gold
            //Prepara los check para no importar aquellos que no esten seleccionados

            //CheckBox ChkBoxRows = (CheckBox)row.FindControl("chbItem");
            //if (ChkBoxHeader.Checked == true)
            //{
            //    ChkBoxRows.Checked = true;
            //}
            //else
            //{
            //    ChkBoxRows.Checked = false;
            //}
            //if (dtEntrada.SelectedItem.Value == "4")
            //{
            //    row.BackColor = Color.FromName("#ff7e62");
            //}

            // Carga_Lotes2("1");
            Variables.mensajeserver = "";

            //if (this.Session["IDSecuencia"].ToString() == "")
            //{
            //    Lbmensaje.Text = "Este Formulario no tiene configurada ninguna Secuencia en su Tabla para enviar a GoldenSoft.";
            //    cuestion.Visible = false;
            //    Asume.Visible = true;
            //    windowmessaje.Visible = true;
            //    return;
            //}

            if (DrVariedad.SelectedItem.Value == "Todos los Formularios")
            {
                Lbmensaje.Text = "Debe seleccionar un tipo de Formulario para poder agrupar sus Lotes y enviar a GoldenSoft.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                return;
            }

            foreach (GridViewRow row in gvEntrada.Rows)
            {
                CheckBox check = row.FindControl("chbItem") as CheckBox;

                if (check.Checked == true)
                {
                    //string code = "Migrar";
                }
                else
                {
                    string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                    string SQL = "UPDATE ZENTRADA  SET ESTADO = -(CONVERT(int,ESTADO)) ";
                    SQL += " WHERE ID = " + code;
                    DBHelper.ExecuteNonQuery(SQL);
                }
                //check.Checked = false;
            }

            //Elimina_Procesados(); crea un historico y libera la tabla

            // Para todos ExecuteNQProcedureAll
            //Unificar con TABLA PROCEDIMIENTOS

            string miro = this.Session["IDProcedimiento"].ToString();

            //ProdTip
            if (this.Session["IDProcedimiento"].ToString() == "2")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedure("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " ExecuteNonQueryProcedure -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" ExecuteNonQueryProcedure --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedure("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedure -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    string a = Main.Ficherotraza(" MigraProcedure --> " + ex.Message);
                    MiCloseMenu();
                    return;
                }

                //alerta.Visible = true;
                //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //BtnLanzaPro.Visible = false;
                //}
            }
            //PinchAlv, PinchAlvCab, PinchAlvLin
            else if (this.Session["IDProcedimiento"].ToString() == "3")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedurePinch("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " ExecuteNonQueryProcedurePinch -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" ExecuteNonQueryProcedurePinch --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedurePinchAlb("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedurePinchAlb -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" MigraProcedurePinchAlb --> " + ex.Message);
                    return;
                }

                //alerta.Visible = true;
                //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //BtnLanzaPro.Visible = false;
                //
            }
            //ModAlv
            else if (this.Session["IDProcedimiento"].ToString() == "1")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedureModAlv("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " ExecuteNonQueryProcedureModAlv -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" ExecuteNonQueryProcedureModAlv --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedureModAlv("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedureModAlv -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" MigraProcedureModAlv --> " + ex.Message);
                    return;
                }
                //alerta.Visible = true;
                //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //BtnLanzaPro.Visible = false;
                //}
            }
            //PaletAlv
            else if (this.Session["IDProcedimiento"].ToString() == "4")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedurePaletAlv("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " ExecuteNonQueryProcedurePaletAlv -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" ExecuteNonQueryProcedurePaletAlv --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedurePaletAlv("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedurePaletAlv -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" MigraProcedurePaletAlv --> " + ex.Message);
                    return;
                }
                //alerta.Visible = true;
                //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //BtnLanzaPro.Visible = false;
                //}
            }
            //Ventas
            else if (this.Session["IDProcedimiento"].ToString() == "5")
            {
                try
                {
                    TextAlerta.Text += "Ejecuta ENTRADA_VENTAS";
                    DBHelper.MigraProcedureVentas("");
                    TextAlerta.Text += Variables.mensajeserver;
                    //DBHelper.ExecuteNonQueryProcedureVenta("");

                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlerta.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedureVentas -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" MigraProcedureVentas --> " + ex.Message);
                    return;
                }

                try
                {
                    //consulta de ventas todos a 1
                    TextAlerta.Text += "Ejecuta Procedimiento";
                    DBHelper.ExeNonQueryProcMovVentas("");
                    TextAlerta.Text += Variables.mensajeserver;
                    //                 
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlerta.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " ExeNonQueryProcMovVentas -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza(" ExeNonQueryProcMovVentas --> " + ex.Message);
                    return;
                }



                try
                {
                    TextAlerta.Text += "Ejecuta migra IMPVENTA a la 80";
                    DBHelper.MigraProcedureVenta("");
                    TextAlerta.Text += Variables.mensajeserver;
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text += Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedureVenta -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("MigraProcedureVenta --> " + ex.Message);
                    return;
                }

                try
                {
                    TextAlerta.Text += "Ejecuta migra IMPVENTADEP a la 80";
                    DBHelper.MigraProcedureVentaDEP("");
                    TextAlerta.Text += Variables.mensajeserver;
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text += Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text += Variables.mensajeserver + " MigraProcedureVentaDEP -> " + ex.Message;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("MigraProcedureVentaDEP --> " + ex.Message);
                    return;
                }




                //alerta.Visible = true;
                //TextAlerta.Text += "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                ////TextAlertaErr.Text += "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                ////Buscar error
                //alerta.Visible = true;
                ////BtnLanzaPro.Visible = false;
                ////}
            }
            //Frigo 
            else if (this.Session["IDProcedimiento"].ToString() == "6")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedureFrigo("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("ExecuteNonQueryProcedureFrigo --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedureFrigo("");
                }
                catch (Exception ex)
                {
                    //alertaErr.Visible = true;
                    //TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                    //Variables.mensajeserver = "";

                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("MigraProcedureFrigo --> " + ex.Message);
                    return;
                }
                //alerta.Visible = true;
                //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                //BtnLanzaPro.Visible = false;
                //}
            }
            //Plantacion
            else if (this.Session["IDProcedimiento"].ToString() == "7")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedurePlantacion("");
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("ExecuteNonQueryProcedurePlantacion --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedurePlantacion("");
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("MigraProcedurePlantacicion --> " + ex.Message);
                    return;
                }
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }
            //Compra
            else if (this.Session["IDProcedimiento"].ToString() == "8")
            {
                try
                {
                    DBHelper.ExecuteNonQueryProcedureCompra("");
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("ExecuteNonQueryProcedureCompra --> " + ex.Message);
                    return;
                }

                try
                {
                    DBHelper.MigraProcedureCompras("");
                }
                catch (Exception ex)
                {
                    Lbmensaje.Text = "Error: " + ex.Message;
                    cuestion.Visible = true;
                    Asume.Visible = false;
                    windowmessaje.Visible = true;
                    MiCloseMenu();
                    string a = Main.Ficherotraza("MigraProcedureCompras --> " + ex.Message);
                    return;
                }
                Lbmensaje.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text;
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
            }

            else
            {
                //Si no tiene asignado un numero de procedimiento o numero de secuencias en Formulario
                //alerta.Visible = true;
                //TextAlerta.Text += "No se contempla el envio de esta información a GoldenSoft. Deberá seleccionar un formulario la lista de Formularios.";
                //Variables.mensajeserver = "";
                Lbmensaje.Text = "Error de configuración en Tabla de este Formulario para el Procedimiento '" + this.Session["IDProcedimiento"].ToString() + "' y la Secuencias '" + this.Session["IDSecuencia"].ToString() +  "' para enviar información a GoldenSoft.";
                cuestion.Visible = false;
                Asume.Visible = true;
                windowmessaje.Visible = true;
                MiCloseMenu();
                return;
            }

            //deja los check con el estado como estaban de aquellos que no estan seleccionados Carga_Lotes
            int M = 0;
            foreach (GridViewRow row in gvEntrada.Rows)
            {
                CheckBox check = row.FindControl("chbItem") as CheckBox;
                if (check.Checked == false)
                {

                    string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                    string SQL = "UPDATE ZENTRADA  SET ESTADO = (CONVERT(int,ESTADO)) * -1 ";
                    SQL += " WHERE ID = " + code;
                    DBHelper.ExecuteNonQuery(SQL);
                }
                else
                {
                    string code = gvEntrada.DataKeys[row.RowIndex].Values[0].ToString();
                    string SQL = "UPDATE ZENTRADA  SET ESTADO = 2 ";
                    SQL += " WHERE ID = " + code;
                    DBHelper.ExecuteNonQuery(SQL);
                    M += 1;
                }
            }


            //if (TxtID.Text == "")
            //{ }
            //else
            //{
            //btnGenerate_Click(sender, e);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}
            //}
            
            //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            //Carga_Lotes(); // this.Session["IDSecuencia"].ToString());

            //alerta.Visible = true;
            //TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text + Environment.NewLine; // + TextAlerta.Text;
            if (M == 0)
            {
                Lbmensaje.Text = "No tiene seleccionado ningún Lote.";
            }
            else if (M == 1)
            {
                Lbmensaje.Text = "Los datos de " + M + " Lote están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text + Environment.NewLine;
            }
            else if (M > 1)
            {
                Lbmensaje.Text = "Los datos de " + M + " Lotes están en la tabla de GoldenSoft preparados para importar desde su aplicación en " + DrVariedad.SelectedItem.Text + Environment.NewLine;
            }
            cuestion.Visible = false;
            Asume.Visible = true;
            windowmessaje.Visible = true;
            MiCloseMenu();
        }
        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
        }
        //fichero bat para habilitar todos los puertos de SQL SERVER
        //@echo =========  SQL Server Ports  ===================
        //       @echo Enabling SQLServer default instance port 1433
        //       netsh firewall set portopening TCP 1433 "SQLServer"
        //       @echo Enabling Dedicated Admin Connection port 1434
        //       netsh firewall set portopening TCP 1434 "SQL Admin Connection"
        //       @echo Enabling conventional SQL Server Service Broker port 4022
        //       netsh firewall set portopening TCP 4022 "SQL Service Broker"
        //       @echo Enabling Transact-SQL Debugger/RPC port 135
        //       netsh firewall set portopening TCP 135 "SQL Debugger/RPC"
        //       @echo =========  Analysis Services Ports  ==============
        //       @echo Enabling SSAS Default Instance port 2383
        //       netsh firewall set portopening TCP 2383 "Analysis Services"
        //       @echo Enabling SQL Server Browser Service port 2382
        //       netsh firewall set portopening TCP 2382 "SQL Browser"
        //       @echo =========  Misc Applications  ==============
        //       @echo Enabling HTTP port 80
        //       netsh firewall set portopening TCP 80 "HTTP"
        //       @echo Enabling SSL port 443
        //       netsh firewall set portopening TCP 443 "SSL"
        //       @echo Enabling port for SQL Server Browser Service's 'Browse' Button
        //       netsh firewall set portopening UDP 1434 "SQL Browser"
        //       @echo Allowing multicast broadcast response on UDP(Browser Service Enumerations OK)
        //       netsh firewall set multicastbroadcastresponse ENABLE

        //protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;
        //    txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;
        //    //BtnLanzaPro.Visible = false;

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];

        //    this.Session["IDLista"] = "Escaneados";

        //    //string SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //    SQL = "SELECT * FROM ZENTRADA WHERE LOTE = '" + DrScaneados.SelectedItem.Text + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    if (dbA.Rows.Count == 0)
        //    {
        //        SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //        dbA = Main.BuscaLote(SQL).Tables[0];

        //        foreach (DataRow filas in dbA.Rows)
        //        {                 
        //            txtQRCode.Text = filas["ZLOTE"].ToString();
        //            //TxtID.Text = filas["ZID"].ToString();

        //            //if (filas["ZFECHA"].ToString() != "")
        //            //{
        //            //    TxtFecha.Text = filas["ZFECHA"].ToString().Substring(0, 10);
        //            //}
        //            //else
        //            //{
        //            //    TxtFecha.Text = filas["ZFECHA"].ToString();
        //            //}

        //            //TxtVariedad.Text = "";
        //            //TxtCajas.Text = "";
        //            //TxtPlantas.Text = "";
        //            LbIDLote.Text = filas["ZID"].ToString();

        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                //LbCodeQRPalteAlv.Text = filas["ZID"].ToString();
        //                //LbTipoPlantaP.Text = "";
        //                //LbVariedadP.Text = "";
        //                //LbVariedadS.Text = "";
        //                //lbUnidadesP.Text = "";
        //                //lbNumPlantasP.Text = "";
        //            }
        //            else
        //            {
        //                //LbCampoS.Text = "";
        //                //LbFechaS.Text = "";
        //                //LbVariedadS.Text = "";
        //                //LbCajasS.Text = "";
        //                //LbPlantasS.Text = "";
        //                //Lbcompleto.Text = "";
        //                //LbPlantaS.Text = "";
        //            }

        //            //TxtEstado.Text = "";
        //            //TxtDispositivo.Text = "";
        //            //TxtLoteDestino.Text = "";

        //            //TxtForm.Text = "";
        //            //TxtManojos.Text = "";
        //            //TxtDesde.Text = "";
        //            //TxtHasta.Text = "";
        //            //TxtETDesde.Text = "";
        //            //TxtETHasta.Text = "";
        //            //TxtTuneles.Text = "";
        //            //TxtPasillos.Text = "";
        //            //TxtObservaciones.Text = "";
        //            //TxtOK.Text = "";
        //            //H1Normal.Visible = false;
        //            //H1Seleccion.Visible = false;
        //            //H1Red.Visible = true;
        //            //H1Green.Visible = false;
        //            DrPrinters_Click();

        //            btnGenerate_Click(sender, e);
        //            alerta.Visible = false;
        //            alertaErr.Visible = false;
        //            btnPrint2.Visible = false;
        //            //BTerminado.Visible = false;
        //            //btProcesa.Visible = false;
        //            //btPorcesa.Visible = false;
        //            alertaLog.Visible = false;

        //            Btfin.Visible = false;
        //            BTerminado.Visible = false;

        //            //if (DrPrinters.SelectedItem.Value == "1")
        //            //{
        //            //    btnPrintA3.Visible = true;
        //            //}
        //            //else
        //            //{
        //            //    btnPrintB3.Visible = true;
        //            //}

        //            break;
        //        }
        //    }
        //    else
        //    {
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            //this.Session["IDSecuencia"] = filas["ID"].ToString();
        //            LbIDLote.Text = filas["ID"].ToString();
        //            txtQRCode.Text = filas["LOTE"].ToString();
        //            //TxtCampo.Text = filas["TIPO_PLANTA"].ToString();

        //            //if (filas["FECHA"].ToString() != "")
        //            //{
        //            //    TxtFecha.Text = filas["FECHA"].ToString().Substring(0, 10);
        //            //}
        //            //else
        //            //{
        //            //    TxtFecha.Text = filas["FECHA"].ToString();
        //            //}

        //            //TxtVariedad.Text = filas["VARIEDAD"].ToString();
        //            //TxtCajas.Text = filas["UNIDADES"].ToString();
        //            //TxtEstado.Text = filas["ESTADO"].ToString();
        //            //TxtDispositivo.Text = filas["DeviceName"].ToString();
        //            //TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();

        //            //if (TxtCajas.Text == "CAJAS")
        //            //{
        //            //    //LbnumeroPlantas.Text = "Número de Cajas:";
        //            //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
        //            //    //LbCajasSQR.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
        //            //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //    //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            //}
        //            //if (TxtCajas.Text == "PLANTAS")
        //            //{
        //            //    //LbnumeroPlantas.Text = "Número de Plantas:";
        //            //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //            //    //LbCajasSQR.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //            //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //    //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //}

        //            try
        //            {
        //                foreach (DataRow fila2 in dbP.Rows)
        //                {
        //                    if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //                    {
        //                        if (filas["UNIDADES"].ToString() == "PLANTAS")
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                            //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                            break;
        //                        }
        //                        else if (filas["UNIDADES"].ToString() == "CAJAS")
        //                        {
        //                            if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                            {
        //                                LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                            }
        //                            else
        //                            {
        //                                foreach (DataRow fila3 in dbP.Rows)
        //                                {
        //                                    if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                    {
        //                                        int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                        LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        break;
        //                                    }
        //                                }
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Lberror.Text = ex.Message;
        //            }

        //            TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //            LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            //LbCampoSQR.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //            //LbPlantaSQR.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();

        //            if(filas["FECHA"].ToString() != "")
        //            {
        //                LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString().Substring(0,10);
        //            }
        //            else
        //            {
        //                LbFechaS.Text = "FECHA CORTE: ";
        //            }

        //            //LbFechaSQR.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
        //            //LbVariedadSQR.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
        //            //LbCajasS.Text = "Nº CAJAS: " + filas["UNIDADES"].ToString();
        //            //LbPlantasS.Text = "Nº PLANTAS: " + filas["NUM_UNIDADES"].ToString();
        //            Lbcompleto.Text = "QR COMPLETO";

        //            TxtID.Text = filas["ID"].ToString();
        //            TxtForm.Text = filas["TIPO_FORM"].ToString();
        //            TxtManojos.Text = filas["MANOJOS"].ToString();
        //            TxtDesde.Text = filas["DESDE"].ToString();
        //            TxtHasta.Text = filas["HASTA"].ToString();
        //            TxtETDesde.Text = filas["ETDESDE"].ToString();
        //            TxtETHasta.Text = filas["ETHASTA"].ToString();
        //            TxtTuneles.Text = filas["TUNELES"].ToString();
        //            TxtPasillos.Text = filas["PASILLOS"].ToString();
        //            TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
        //            TxtOK.Text = filas["OK"].ToString();

        //            ////if (TxtID.Enabled == true)
        //            ////{
        //                 //Oculta_Datos(1);
        //            ////}
        //            ////else
        //            ////{
        //            ////    Oculta_Datos(0);
        //            ////}

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            if (DrPrinters.SelectedItem.Value == "1")
        //            {
        //                btnPrintA3.Visible = true;
        //            }
        //            else
        //            {
        //                btnPrintB3.Visible = true;
        //            }
        //            H1Green.Visible = false;
        //            btnPrint2.Visible = false;

        //            //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //            //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

        //            alerta.Visible = false;
        //            alertaErr.Visible = false;
        //            alertaLog.Visible = false;

        //            btnGenerate_Click(sender, e);
        //            if (DrPrinters.SelectedItem.Value == "4")
        //            {
        //                btnGeneraTodoPerf_Click(sender, e);
        //            }
        //            else
        //            {
        //                btnGenerateTodo_Click(sender, e);
        //            }
        //            //btnGenerateTodo_Click(sender, e);

        //            //btProcesa.Visible = false;
        //            //btPorcesa.Visible = false;
        //            Btfin.Visible = false;
        //            BTerminado.Visible = false;
        //            string Miro = filas["ESTADO"].ToString();
        //            if (filas["ESTADO"].ToString() == "" || filas["ESTADO"].ToString() == "0")
        //            {
        //                BTerminado.Visible = true;
        //            }
        //            else if (filas["ESTADO"].ToString() == "1")
        //            {
        //                Btfin.Visible = true;
        //            }

        //            //SQL = "DELETE FROM ZLOTESCREADOS WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //            SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //            DBHelper.ExecuteNonQuery(SQL);
        //            Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

        //            break;
        //        }
        //    }           
        //}

        //protected void DrLotes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;
        //    txtQRCodebis.Visible = false;
        //    txtQRCode.Visible = true;
        //    BTerminado.Visible = false;
        //    Btfin.Visible = false;
        //    //BtnLanzaPro.Visible = false;
        //    BtEnviaFinalizados.Enabled = false;
        //    string AA = "";
        //    string CC = "";
        //    string BB = "";
        //    string DD = "";
        //    string EE = "";
        //    string FF = "";


        //    BtFinalizalote.Attributes["class"] = "btn btn-warning  btn-block";

        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];

        //    this.Session["IDLista"] = "Lotes";

        //    SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + DrLotes.SelectedItem.Value + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow filas in dbA.Rows)
        //    {
        //        LbIDLote.Text = filas["ID"].ToString();
        //        if (filas["ESTADO"].ToString().ToString() == "1" )
        //        {
        //            if (DrVariedad.SelectedItem.Value == "5")
        //            {
        //                BtEnviaFinalizados.Enabled = false;
        //            }
        //            else
        //            {
        //                BtEnviaFinalizados.Enabled = true;
        //            }
        //            //BtEnviaFinalizados.Enabled = true;
        //            BtFinalizalote.Attributes["class"] = "btn btn-info  btn-block";
        //        }

        //        txtQRCode.Text = filas["LOTE"].ToString();
        //        TxtCampo.Text = filas["TIPO_PLANTA"].ToString();

        //        if (filas["FECHA"].ToString() != "")
        //        {
        //            TxtFecha.Text = filas["FECHA"].ToString().Substring(0, 10);
        //        }
        //        else
        //        {
        //            TxtFecha.Text = filas["FECHA"].ToString();
        //        }

        //        TxtVariedad.Text = filas["VARIEDAD"].ToString();
        //        TxtCajas.Text = filas["UNIDADES"].ToString();
        //        TxtEstado.Text = filas["ESTADO"].ToString();
        //        TxtDispositivo.Text = filas["DeviceName"].ToString();
        //        TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {                      
        //            LbCodeQRPalteAlv.Text = filas["LOTE"].ToString();
        //            LbTipoPlantaP.Text = "";
        //            LbVariedadP.Text = "";
        //            lbUnidadesP.Text = "";
        //            lbNumPlantasP.Text = "";

        //            if (TxtCajas.Text == "CAJAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            if (TxtCajas.Text == "PLANTAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }
        //            if (TxtCajas.Text == "BANDEJAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString(); filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }

        //            TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();

        //            //LbTipoPlantaP.Text = "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

        //            if (Con is null)
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
        //            }
        //            else
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + Con;
        //            }

        //            //LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString()


        //            //LbTipoPlantaP.Text = "Tipo Planta: " + filas["UNIDADES"].ToString() + " " + filas["TIPO_PLANTA"].ToString(); // "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con is null)
        //            {
        //                LbVariedadP.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //            }
        //            else
        //            {
        //                LbVariedadP.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con is null)
        //            {
        //                //VIVA: Viveros Valsaín, SLU
        //                //VRE: Viveros Río Eresma, SLU
        //                LbCodePaletAlv.Text = "";
        //            }
        //            else
        //            {
        //                if (Con.ToString().Contains("VIVA"))
        //                {

        //                    LbCodePaletAlv.Text = "Viveros Valsaín, SLU";
        //                }
        //                else
        //                {
        //                    LbCodePaletAlv.Text = "Viveros Río Eresma, SLU";
        //                }
        //            }

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }
        //        else
        //        {
        //            LbIDLote.Text = filas["ID"].ToString();

        //            if (TxtCajas.Text == "CAJAS")
        //            {
        //                // LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                //LbCajasSQR.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            if (TxtCajas.Text == "PLANTAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                //LbCajasSQR.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }
        //            if (TxtCajas.Text == "BANDEJAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                //LbCajasSQR.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                //LbPlantasSQR.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }

        //            TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //            LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            //LbCampoSQR.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //            //LbPlantaSQR.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();

        //            if (filas["FECHA"].ToString() != "")
        //            {
        //                LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString().Substring(0, 10);
        //            }
        //            else
        //            {
        //                LbFechaS.Text = "FECHA CORTE: ";
        //            }

        //            //LbFechaSQR.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
        //            //LbVariedadSQR.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
        //            //LbCajasS.Text = "Nº CAJAS: " + filas["UNIDADES"].ToString();
        //            //LbPlantasS.Text = "Nº PLANTAS: " + filas["NUM_UNIDADES"].ToString();
        //            Lbcompleto.Text = "QR COMPLETO";
        //        }


        //        try
        //        {
        //            foreach (DataRow fila2 in dbP.Rows)
        //            {
        //                AA = fila2["ZTIPO_PLANTA"].ToString();
        //                CC = fila2["ZTIPO_FORMATO"].ToString();
        //                //string JJ = filas["UNIDADES"].ToString();
        //                //string FF = fila2["ZNUMERO_PLANTAS"].ToString();

        //                //if (DrPrinters.SelectedItem.Value == "6")
        //                //{
        //                //    BB = "P-ALV-" + filas["TIPO_PLANTA"].ToString();
        //                //}
        //                //else
        //                //{
        //                    BB = filas["TIPO_PLANTA"].ToString();
        //                //}

        //                //BB = filas["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                DD = filas["UNIDADES"].ToString();//CAJAS
        //                //string EE = filas["NUM_UNIDADES"].ToString();//40
        //                //string GG = filas["MANOJOS"].ToString();//3

        //                if (fila2["ZTIPO_PLANTA"].ToString() == BB && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //                {
        //                    if (filas["UNIDADES"].ToString() == "PLANTAS")
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
        //                    else if (filas["UNIDADES"].ToString() == "CAJAS")
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
        //                    else if (filas["UNIDADES"].ToString() == "BANDEJAS")
        //                    {
        //                        if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                        {
        //                            if (DrPrinters.SelectedItem.Value == "6")
        //                            {
        //                                string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                Double Value = Convert.ToDouble(Cuantos);
        //                                lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",",".");
        //                            }
        //                            else
        //                            {
        //                                LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                            }                                   
        //                            //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                        }
        //                        else
        //                        {
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
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Lberror.Text = ex.Message;
        //        }

        //        //TxtID.Text = filas["ID"].ToString();
        //        //TxtForm.Text = filas["TIPO_FORM"].ToString();
        //        //TxtManojos.Text = filas["MANOJOS"].ToString();
        //        //TxtDesde.Text = filas["DESDE"].ToString();
        //        //TxtHasta.Text = filas["HASTA"].ToString();
        //        //TxtETDesde.Text = filas["ETDESDE"].ToString();
        //        //TxtETHasta.Text = filas["ETHASTA"].ToString();
        //        //TxtTuneles.Text = filas["TUNELES"].ToString();
        //        //TxtPasillos.Text = filas["PASILLOS"].ToString();
        //        //TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
        //        //TxtOK.Text = filas["OK"].ToString();


        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        alertaLog.Visible = false;
        //        int Runidades = 0;


        //        if (DrVariedad.SelectedItem.Value == "7")
        //        {
        //            SQL = "SELECT A.* ";
        //            SQL += " FROM ZENTRADA A, ZFORMULARIOS B ";
        //            SQL += " WHERE A.TIPO_FORM = B.ZTITULO ";
        //            SQL += " AND B.ZID = '" + DrVariedad.SelectedItem.Value + "'";
        //            SQL += " AND A.LOTE = '" + filas["LOTE"].ToString() + "'";

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
        //                            string XX = fila2["ZTIPO_FORMATO"].ToString();
        //                            AA = fila2["ZTIPO_PLANTA"].ToString();                                   
        //                            //BB = "P-ALV-" + filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                            BB =  filaCount["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                            DD = filaCount["UNIDADES"].ToString();//CAJAS
        //                            EE = filaCount["NUM_UNIDADES"].ToString();//CAJAS
        //                            FF = fila2["ZNUMERO_PLANTAS"].ToString();//CAJAS

        //                            if (fila2["ZTIPO_PLANTA"].ToString().Contains(BB) && fila2["ZTIPO_FORMATO"].ToString() == filaCount["UNIDADES"].ToString())
        //                            {
        //                                CC = fila2["ZTIPO_FORMATO"].ToString();
        //                                if (filaCount["UNIDADES"].ToString() == "PLANTAS")
        //                                {
        //                                    Totales += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                }
        //                                else if (filaCount["UNIDADES"].ToString() == "CAJAS")
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
        //                                                //Runidades += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()) + NN);
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else if (filaCount["UNIDADES"].ToString() == "BANDEJAS")
        //                                {
        //                                    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                    {
        //                                        Totales +=  (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()));
        //                                        Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                    }
        //                                    else
        //                                    {
        //                                        foreach (DataRow fila3 in dbP.Rows)
        //                                        {
        //                                            if (fila3["ZTIPO_PLANTA"].ToString() == filaCount["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                            {
        //                                                int NN = Convert.ToInt32(filaCount["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                                Totales += ((Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN);
        //                                                Runidades += Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString());
        //                                            }
        //                                        }
        //                                    }
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
        //                        Lberror.Text = ex.Message;
        //                    }
        //                }
        //            }
        //        }

        //        btnGenerate_Click(sender, e);
        //        if (DrPrinters.SelectedItem.Value == "4")
        //        {
        //            btnGeneraTodoPerf_Click(sender, e);
        //        }
        //        else
        //        {
        //            btnGenerateTodo_Click(sender, e);
        //        }
        //        //btnGenerateTodo_Click(sender, e);
        //        //btnPrint2.Visible = true;
        //        //btProcesa.Visible = false;
        //        //btPorcesa.Visible = false;
        //        BtFinalizalote.Enabled = false;
        //        //BtFinalizaTodos.Enabled = true;
        //        BtEnviaFinalizados.Enabled = false;

        //        Btfin.Visible = false;
        //        BTerminado.Visible = false;
        //        string Miro = filas["ESTADO"].ToString();
        //        if (filas["ESTADO"].ToString() == "" || filas["ESTADO"].ToString() == "0")
        //        {
        //            //BTerminado.Visible = true;
        //            BtFinalizalote.Text = "Finalizar este Lote";
        //            BtFinalizalote.Attributes["class"] = "btn btn-success  btn-block";
        //            BtFinalizalote.Enabled = true;
        //            BtEnviaFinalizados.Enabled = false;
        //            //BtFinalizaTodos.Enabled = true;
        //        }
        //        else if (filas["ESTADO"].ToString() == "1")
        //        {
        //            //Btfin.Visible = true;
        //            BtFinalizalote.Text = "Lote Finalizado";
        //            BtFinalizalote.Attributes["class"] = "btn btn-warning  btn-block";
        //            BtFinalizalote.Enabled = false;
        //            if (DrVariedad.SelectedItem.Value == "5")
        //            {
        //                BtEnviaFinalizados.Enabled = false;
        //            }
        //            else
        //            {
        //                BtEnviaFinalizados.Enabled = true;
        //            }
        //            //BtEnviaFinalizados.Enabled = true;
        //            //BtFinalizaTodos.Enabled = true;
        //        }
        //        SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //        DBHelper.ExecuteNonQuery(SQL);
        //        //Carga_Lotes(this.Session["IDSecuencia"].ToString());
        //        Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());

        //        break;
        //    }
        //    //cambia el color del lote
        //    Actualiza_Lote();
        //    //this.SetDropDownListItemColor(dbA);
        //}


        protected void btPrinter_Click(object sender, EventArgs e)
        {
            //dvDrlist.Visible = true;
            //dvPrinters.Visible = false;

        }

    }
}