using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Satelite
{
    public partial class BuscaArchivo : System.Web.UI.Page
    {
        private string ElOrden = "";
        private int Indice = 0;
        private string[] ListadoArchivos;
        private static int IDDiv = 0;
        private static string IDTABLA = "-1";
        protected System.Web.UI.WebControls.TreeView tvControl;

        private string SortDirection
        {
            get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Session"] != null)
                {
                }
                else
                {
                    Response.Redirect("Login.aspx"); //Default
                }

                if (this.Session["MiNivel"].ToString() != "9")
                {
                    Response.Redirect("Principal.aspx"); //Default
                }

                if (!IsPostBack)
                {
                    //CreaCamposHtml();

                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();

                    this.Session["Archivos"] = null;
                    this.Session["Campos"] = null;

                    this.Session["idarchivo"] = "";
                    if (this.Session["Campos"] != null)
                    {
                        dt = this.Session["Campos"] as DataTable;
                        //dt = Main.CargaCampos().Tables[0];
                        //ya esta cargado
                    }
                    else
                    {
                        dt = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dt;
                    }

                    Campos_ordenados();

                    DrArchivos.DataValueField = "ZID";
                    DrArchivos.DataTextField = "ZDESCRIPCION";

                    if (this.Session["Archivos"] != null)
                    {
                        dt1 = this.Session["Archivos"] as DataTable;
                        DrArchivos.DataSource = dt1; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();



                    }
                    else
                    {
                        dt1 = Main.CargaArchivos().Tables[0];
                        this.Session["Archivos"] = dt1;
                        DrArchivos.DataSource = dt1; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();
                    }

                    //DrCampos.Items.Clear();
                    //DrCampos.Items.Insert(0, new ListItem("Ninguno", "0"));

                    foreach (DataRow fila in dt1.Rows)
                    {
                        DrCampos.Items.Add(new ListasID(fila["ZTABLENAME"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                    }

                    Relaciones(1, dt);
                    PopulateRootLevel();

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
                Response.Redirect("login.aspx");
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
            dtt.Columns.Add("ZTIPO");

            DataRow drr;

            //const string fic = @"C:\Proyecto\Administracion\Admin\Public\output.txt";
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
            int i = 0;

            foreach (DataRow fila in DtCampos.Rows)
            {
                visto = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    if (fila["ZID"].ToString() == dr["ZID_CAMPO"].ToString())
                    {
                        drr = dtt.NewRow();
                        drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                        drr[1] = fila["ZTITULO"].ToString();
                        drr[2] = fila["ZDESCRIPCION"].ToString();
                        drr[3] = dr["ZTABLENAME"].ToString();
                        drr[4] = dr["ZTIPO"].ToString();
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

        
        protected void btnOpenFiles_Click(object sender, EventArgs e)
        {
            if(DivFicheros.Visible == true)
            {
                DivFicheros.Visible = false;
            }
            else
            {
                DivFicheros.Visible = true;
            }
        }

        //GridView
        protected void ImgOrdenMin_Click(object sender, EventArgs e)
        {
        }

        //protected void gvCabecera_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        //{
        //    gvCabecera.PageIndex = e.NewPageIndex;
        //    DataTable dt = this.Session["Archivos"] as DataTable;
        //    DataTable dt1 = this.Session["Campos"] as DataTable;
        //    Carga_tablaControl(dt, dt1, null);
        //}
        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvControl.PageIndex = e.NewPageIndex;
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            Carga_tablaControl(dt, dt1, null);
        }
        

        protected void gvControl_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvControl.PageIndex = e.NewPageIndex;
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            Carga_tablaControl(dt, dt1, null);
        }

        protected void gvCabecera_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddControlPageSize.SelectedItem.Value == "1000")
            {
                gvControl.AllowPaging = false;
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }
            else
            {
                gvControl.AllowPaging = true;
                gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }

        }

        protected void gvLista_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddControlPageSize.SelectedItem.Value == "1000")
            {
                gvControl.AllowPaging = false;
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }
            else
            {
                gvControl.AllowPaging = true;
                gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }

        }
        

        protected void gvControl_PageSize_Changed(object sender, EventArgs e)
        {
            if (ddControlPageSize.SelectedItem.Value == "1000")
            {
                gvControl.AllowPaging = false;
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }
            else
            {
                gvControl.AllowPaging = true;
                gvControl.PageSize = Convert.ToInt32(ddControlPageSize.SelectedItem.Value);
                DataTable dt = this.Session["Archivos"] as DataTable;
                DataTable dt1 = this.Session["Campos"] as DataTable;
                Carga_tablaControl(dt, dt1, null);
            }

        }

        //protected void gvCabecera_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        //{
        //    int index = 0;
        //    if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    try
        //    {
        //        if (e.CommandName == "Edit" || e.CommandName == "Update")
        //        {
        //            index = int.Parse(e.CommandArgument.ToString());
        //            Indice = index;
        //            this.Session["IDGridA"] = gvCabecera.DataKeys[index].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string a = Main.Ficherotraza("gvControl rowcommand --> " + ex.Message);
        //    }
        //}

        
        protected void gvLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "BajaOrden")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();

                    GridViewRow row = gvControl.Rows[index];
                    string miro = gvControl.DataKeys[index].Value.ToString();

                    row.BackColor = Color.FromName("#ffead1");

                    //foreach (GridViewRow Fila in gvControl.Rows)
                    //{
                    //foreach (TableCell cell in Fila.Cells)
                    //string AA = "";

                    //gvControl.EditIndex = Indice;


                    int AA = 0;
                    int UU = 0;
                    string NombreDivCampo = "";
                    Boolean Esta = false;

                    foreach (TableCell cell in row.Cells)
                    {
                        string NombreCajaTexto = "";
                        if (Esta == false)
                        {
                            Esta = true;
                        }
                        else
                        {
                            if ((AA % 2) == 0)
                            {
                                //Par
                                NombreCajaTexto = "TxL" + UU;
                                NombreDivCampo = "DivReg" + UU;
                                //HtmlGenericControl DivParam = new HtmlGenericControl();
                                HtmlGenericControl TB = (DivCampos0.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                //TextBox TB = (DivCampos0.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if (cell.Text == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(cell.Text);
                                    }
                                }
                            }
                            else//Impar
                            {
                                NombreCajaTexto = "TxD" + UU;
                                HtmlGenericControl TB = (DivCampos0.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if (cell.Text == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(cell.Text);
                                    }
                                }
                                UU += 1;
                            }
                            AA += 1;
                        }
                    }
                    //DivCampos0.Attributes["style"] = "height: " + (UU * 50).ToString() + "px; width: 90%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);";
                    //DivCampos0.Attributes["height"] = (UU * 50).ToString();
                    DivCampos0.Visible = true;

                    //TextBox tBox = new TextBox(); //(gvControl.Rows[Indice].Cells[10].FindControl(NombreCajaTexto) as TextBox);
                    //tBox.ID = NombreCajaTexto;
                    //tBox.Text = cell.Text;



                    //BoundField field = cell.ID; // (BoundField)((DataControlFieldCell)cell).ContainingField;
                    //if (cell.Text == "ColumnName")
                    //    {
                    //        //field.Visible = false;
                    //    }
                    //}
                    //}

                    //Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);

                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvControl rowcommand --> " + ex.Message);
            }
        }

        protected void gvControl_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();
                }
                else if (e.CommandName == "BajaOrden")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridA"] = gvControl.DataKeys[index].Value.ToString();

                    GridViewRow row = gvControl.Rows[index];
                    string miro = gvControl.DataKeys[index].Value.ToString();

                    row.BackColor = Color.FromName("#ffead1");

                    //foreach (GridViewRow Fila in gvControl.Rows)
                    //{
                    //foreach (TableCell cell in Fila.Cells)
                    //string AA = "";

                    //gvControl.EditIndex = Indice;


                    int AA = 0;
                    int UU = 0;
                    string NombreDivCampo = "";
                    Boolean Esta = false;

                    foreach (TableCell cell in row.Cells)
                    {
                        string NombreCajaTexto = "";
                        if (Esta == false)
                        {
                            Esta = true;
                        }
                        else
                        {
                            if ((AA % 2) == 0)
                            {
                                //Par
                                NombreCajaTexto = "TxL" + UU;
                                NombreDivCampo = "DivReg" + UU;
                                //HtmlGenericControl DivParam = new HtmlGenericControl();
                                HtmlGenericControl TB = (DivCampos0.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                //TextBox TB = (DivCampos0.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if(cell.Text == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(cell.Text);
                                    }
                                }
                            }
                            else//Impar
                            {
                                NombreCajaTexto = "TxD" + UU;
                                HtmlGenericControl TB = (DivCampos0.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if (cell.Text == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(cell.Text);
                                    }
                                }
                                UU += 1;
                            }
                            AA += 1;
                        }
                    }
                    //DivCampos0.Attributes["style"] = "height: " + (UU * 50).ToString() + "px; width: 90%;z-index: 999;-moz-border-radius: 10px;-webkit-border-radius: 10px;border-radius: 10px;border: 0px solid black; box-shadow: inset 0 0 5px 5px rgb(157,157,155);";
                    //DivCampos0.Attributes["height"] = (UU * 50).ToString();
                    DivCampos0.Visible = true;

                    //TextBox tBox = new TextBox(); //(gvControl.Rows[Indice].Cells[10].FindControl(NombreCajaTexto) as TextBox);
                    //tBox.ID = NombreCajaTexto;
                    //tBox.Text = cell.Text;



                    //BoundField field = cell.ID; // (BoundField)((DataControlFieldCell)cell).ContainingField;
                    //if (cell.Text == "ColumnName")
                    //    {
                    //        //field.Visible = false;
                    //    }
                    //}
                    //}

                    //Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("LabOUdsPedidas") as Label);

                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvControl rowcommand --> " + ex.Message);
            }
        }

        protected void treeUser_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (treeUser.SelectedNode.Value == "-1")
            {
                IDDiv = 0;
                BuscoDiv_Click();
            }
            else
            {
                string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + treeUser.SelectedNode.Value;
                DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
                this.Session["Archivos"] = dtArchivos;

                for (int i = 0; i < DrArchivos.Items.Count; i++)
                {
                    if (DrArchivos.Items[i].Value == treeUser.SelectedNode.Value)
                    {
                        DrArchivos.SelectedIndex = i;
                        this.Session["idarchivo"] = DrArchivos.SelectedIndex.ToString();
                        break;
                    }
                }

                foreach (DataRow fila2 in dtArchivos.Rows)
                {
                    if (fila2["ZTIPO"].ToString() == "2")
                    {
                        break;
                    }
                    else
                    {
                        DataTable dtCampos = null;
                        DataTable dt = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dt;


                        dtCampos = Relaciones(Convert.ToInt32(DrArchivos.SelectedItem.Value), dt);
                        this.Session["Campos"] = dtCampos;

                        CreaGridControl(dtArchivos, dtCampos);

                        Carga_tablaControl(dtArchivos, dtCampos, null);

                        DrCampos.SelectedIndex = DrArchivos.SelectedIndex;
                        divTree.Visible = false;
                        DivGrid.Visible = true;
                        break;
                    }
                }
            }
        }

        private void PopulateRootLevel()
        {

            DataTable dt = new DataTable();

            dt = Main.CargaArbolArchivos(0, Convert.ToInt32((string)Session["MiNivel"]));

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Proyecto\Administracion\Admin\Public\output.txt", true))
            //{
            //    file.WriteLine(Session["Query"].ToString());
            //    file.Close();
            //}
            //PopulateNodoNuevo(treeUser.Nodes);
            PopulateNodes(dt, treeUser.Nodes);

        }

        private void PopulateNodoNuevo(TreeNodeCollection nodes)
        {
            TreeNode tn = new TreeNode();
            tn.Text = "Importar Datos...";
            tn.Value = "-1";
            nodes.Add(tn);
        }

        private void PopulateNodes(DataTable dt, TreeNodeCollection nodes)
        {
            foreach (DataRow dr in dt.Rows)
            {


                TreeNode tn = new TreeNode();
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
        private void PopulateSubLevel(int parentid, TreeNode parentNode)
        {
            DataTable dt = new DataTable();

            dt = Main.CargaArbolArchivos(parentid, Convert.ToInt32((string)Session["MiNivel"]));

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Proyecto\Administracion\Admin\Public\output.txt", true))
            //{
            //    file.WriteLine(Session["Query"].ToString());
            //    file.Close();
            //}

            PopulateNodes(dt, parentNode.ChildNodes);
        }


        protected void BuscoDiv_Click()
        {
            //string miro = "";

            if (this.Session["MiUEIDEstado"] == null)
            {

            }
            else
            {
                //HtmlControl MyDiv = (HtmlControl)this.FindControl("iframeFlujo");
                HtmlControl MyDiv = (HtmlControl)this.FindControl("Directorio");
                HtmlControl TablasFlujo = (HtmlControl)this.FindControl("DivDirectorio");

                if (IDDiv == 0)
                {
                    MyDiv.Visible = false;
                    //TablasFlujo.Visible = true;
                    IDDiv = 1;
                }
                else
                {
                    MyDiv.Visible = true;
                    //TablasFlujo.Visible = false;
                    IDDiv = 0;
                }
            }
        }

        protected void treeConect_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (treeUser.SelectedNode.Value == "-1")
            {
                IDDiv = 0;
                BuscoDiv_Click();
            }
            else
            {
                IDDiv = 1;
                BuscoDiv_Click();

                DataTable dt = this.Session["Archivos"] as DataTable;

                int i = 1;
                int NumColumnas = gvControl.Columns.Count;

                while (1 != NumColumnas)
                {
                    NumColumnas = gvControl.Columns.Count;
                    if (NumColumnas > 1)
                    {
                        gvControl.Columns.RemoveAt(NumColumnas - 1);
                    }
                }

                foreach (DataRow fila in dt.Rows)
                {
                    if (fila["ZTIPO"].ToString() == "1" || fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "3")
                    {
                        if (fila["ZID"].ToString() == treeUser.SelectedNode.Value)
                        {
                            DataTable dtA = new DataTable();
                            dtA = Main.ConsultaMiTabla(fila["ZTABLENAME"].ToString()).Tables[0];
                            //gvControl.Columns.Clear();  //borra todos                      
                            foreach (DataRow fila2 in dtA.Rows)
                            {
                                BoundField columna = new BoundField();
                                columna.HeaderText = fila2["COLUMN_NAME"].ToString();
                                columna.DataField = fila2["COLUMN_NAME"].ToString();
                                gvControl.Columns.Add(columna);
                                //btnAgregar_Click(fila2["COLUMN_NAME"].ToString());
                            }

                            dtA = Main.ConsultaLATabla(fila["ZTABLENAME"].ToString()).Tables[0];
                            this.Session["MiConsulta"] = dtA;
                            gvControl.DataSource = dtA;
                            gvControl.DataBind();
                            break;
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        
        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = (GridViewRow)gvControl.Rows[e.RowIndex];

            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

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

            gvControl.EditIndex = -1;

            gvControl.DataBind();
        }

        protected void gvControl_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = (GridViewRow)gvControl.Rows[e.RowIndex];

            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

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

            gvControl.EditIndex = -1;

            gvControl.DataBind();
        }

        //protected void gvCabecera_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    GridViewRow row = (GridViewRow)gvCabecera.Rows[e.RowIndex];

        //    string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();

        //    string SQL = "UPDATE ZCARGA_CABECERA set ESTADO = 2 ";
        //    SQL += " WHERE ID = " + miro;

        //    DBHelper.ExecuteNonQuery(SQL);
        //    //if (CaCheck.Checked == false)
        //    //{
        //    //    Carga_tablaJornada();
        //    //}
        //    //else
        //    //{
        //    //    Carga_tablaCabeceraClose();
        //    //}

        //    gvCabecera.EditIndex = -1;

        //    gvCabecera.DataBind();
        //}

        //protected void gvCabecera_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    gvCabecera.EditIndex = Convert.ToInt16(e.NewEditIndex);

        //    int indice = gvCabecera.EditIndex = e.NewEditIndex;
        //    string rID = "";
        //    //if (CaCheck.Checked == false)
        //    //{
        //    //    Carga_tablaJornada();
        //    //}
        //    //else
        //    //{
        //    //    Carga_tablaCabeceraClose();
        //    //}

        //    Label txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
        //    if (txtBox != null)
        //    {
        //        rID = txtBox.Text;
        //    }
        //    DropDownList combo = gvCabecera.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
        //    if (combo != null)
        //    {
        //        for (int i = 0; i < combo.Items.Count; i++)
        //        {
        //            if (combo.Items[i].Text == rID)
        //            {
        //                combo.SelectedValue = combo.Items[i].Value;
        //                break;
        //            }
        //        }
        //    }

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
        //}

        
        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvControl.EditIndex = e.NewEditIndex;
            string rID = "";
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}

            Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            if (txtBox != null)
            {
                rID = txtBox.Text;
            }
            DropDownList combo = gvControl.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
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

        protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            int indice = gvControl.EditIndex = e.NewEditIndex;
            string rID = "";
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}

            Label txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("lblEstado") as Label);
            if (txtBox != null)
            {
                rID = txtBox.Text;
            }
            DropDownList combo = gvControl.Rows[e.NewEditIndex].FindControl("drDescripcionEstado") as DropDownList;
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

        //protected void gvCabecera_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
        //    GridViewRow row = gvCabecera.Rows[e.RowIndex];
        //    string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();

        //    string rID = ""; // gvEmpleado.DataKeyNames[e.RowIndex].ToString();

        //    string rNUMERO = "";
        //    string rEMPRESA = "";
        //    string rPAIS = "";
        //    string rFECHAPREPARA = "";
        //    string rFECHACARGA = "";
        //    string rTELEFONO = "";
        //    string rMATRICULA = "";
        //    string rTRANSPORTISTA = "";
        //    string rTELEFONO_USER = "";
        //    string rLATITUD = "";
        //    string rLONGITUD = "";
        //    string rOBSERVACIONES = "";
        //    string rESTADO = "";
        //    string Mira = "";

        //    try
        //    {
        //        //Mira = Server.HtmlDecode(row.Cells[3].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rID = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[3].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rNUMERO = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[4].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rEMPRESA = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[5].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rPAIS = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[6].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rFECHACARGA = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[7].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rTELEFONO = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[8].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rMATRICULA = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[9].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rTRANSPORTISTA = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[10].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rTELEFONO_USER = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[11].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rLATITUD = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[12].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rLONGITUD = Mira.Replace(".", ",");
        //        //}
        //        //Mira = Server.HtmlDecode(row.Cells[13].Text);
        //        //if (Mira != "")
        //        //{
        //        //    rOBSERVACIONES = Mira.Replace(".", ",");
        //        //}
        //        TextBox txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
        //        //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rID = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[3].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rNUMERO = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[4].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rEMPRESA = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[5].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rPAIS = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[6].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rFECHACARGA = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[7].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rTELEFONO = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[8].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rMATRICULA = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[9].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rTRANSPORTISTA = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[10].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rTELEFONO_USER = txtBox.Text;
        //        }
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[10].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rFECHAPREPARA = txtBox.Text;
        //        }
        //        //txtBox = (gvEmpleado.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[11].Controls[0]);
        //        //if (txtBox != null)
        //        //{
        //        //    rLATITUD = txtBox.Text;
        //        //}
        //        //txtBox = (TextBox)(row.Cells[12].Controls[0]);
        //        //if (txtBox != null)
        //        //{
        //        //    rLONGITUD = txtBox.Text;
        //        //}
        //        txtBox = (gvCabecera.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
        //        //txtBox = (TextBox)(row.Cells[13].Controls[0]);
        //        if (txtBox != null)
        //        {
        //            rOBSERVACIONES = txtBox.Text;
        //        }

        //        DropDownList Etiqueta = row.FindControl("drDescripcionEstado") as DropDownList;
        //        rESTADO = Etiqueta.SelectedValue;



        //        string SQL = "UPDATE ZCARGA_CABECERA set NUMERO = " + rNUMERO + ", ";
        //        SQL += " EMPRESA = '" + rEMPRESA + "', ";
        //        SQL += " PAIS = '" + rPAIS + "', ";
        //        SQL += " FECHAPREPARACION = '" + rFECHAPREPARA + "', ";
        //        SQL += " FECHACARGA = '" + rFECHACARGA + "', ";
        //        SQL += " TELEFONO = '" + rTELEFONO + "', ";
        //        SQL += " MATRICULA = '" + rMATRICULA + "', ";
        //        SQL += " TRANSPORTISTA = '" + rTRANSPORTISTA + "', ";
        //        SQL += " TELEFONO_USER = '" + rTELEFONO_USER + "', ";
        //        //SQL += " LATITUD = '" + rLATITUD + "', ";
        //        //SQL += " LONGITUD = '" + rLONGITUD + "', ";
        //        SQL += " OBSERVACIONES = '" + rOBSERVACIONES + "', ";
        //        SQL += " ESTADO = " + rESTADO;
        //        SQL += " WHERE ID = " + miro;

        //        this.Session["EstadoCabecera"] = rESTADO;

        //        Variables.Error = "";
        //        Lberror.Text = SQL;



        //        DBHelper.ExecuteNonQuery(SQL);
        //        //if (CaCheck.Checked == false)
        //        //{
        //        //    Carga_tablaJornada();
        //        //}
        //        //else
        //        //{
        //        //    Carga_tablaCabeceraClose();
        //        //}

        //        gvCabecera.EditIndex = -1;

        //        gvCabecera.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
        //        Lberror.Text += ". " + ex.Message;
        //        Lberror.Visible = true;
        //    }
        //}

        
        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

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
                TextBox txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
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
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
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

                gvControl.EditIndex = -1;

                gvControl.DataBind();
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

        protected void gvControl_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();

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
                TextBox txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rID = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabNumero") as TextBox);
                //txtBox = (TextBox)(row.Cells[3].Controls[0]);
                if (txtBox != null)
                {
                    rNUMERO = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabEmpresa") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rEMPRESA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabPais") as TextBox);
                //txtBox = (TextBox)(row.Cells[5].Controls[0]);
                if (txtBox != null)
                {
                    rPAIS = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabFechaCarga") as TextBox);
                //txtBox = (TextBox)(row.Cells[6].Controls[0]);
                if (txtBox != null)
                {
                    rFECHACARGA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTelefono") as TextBox);
                //txtBox = (TextBox)(row.Cells[7].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabMatricula") as TextBox);
                //txtBox = (TextBox)(row.Cells[8].Controls[0]);
                if (txtBox != null)
                {
                    rMATRICULA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTransportista") as TextBox);
                //txtBox = (TextBox)(row.Cells[9].Controls[0]);
                if (txtBox != null)
                {
                    rTRANSPORTISTA = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabTconductor") as TextBox);
                //txtBox = (TextBox)(row.Cells[10].Controls[0]);
                if (txtBox != null)
                {
                    rTELEFONO_USER = txtBox.Text;
                }
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabFechaPreparacion") as TextBox);
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
                txtBox = (gvControl.Rows[Indice].Cells[10].FindControl("TabObservaciones") as TextBox);
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

                gvControl.EditIndex = -1;

                gvControl.DataBind();
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("gvEmpleado --> " + ex.Message);
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvControl_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvControl.SelectedRow.BackColor = Color.FromName("#565656");
        }

        //protected void gvCabecera_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        //{
        //    gvCabecera.SelectedRow.BackColor = Color.FromName("#565656");
        //}

        
        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();
            gvControl.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvControl.DataBind();

        }

        protected void gvControl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridViewRow row = gvControl.Rows[e.RowIndex];
            string miro = gvControl.DataKeys[e.RowIndex].Value.ToString();
            gvControl.EditIndex = -1;
            //if (CaCheck.Checked == false)
            //{
            //    Carga_tablaJornada();
            //}
            //else
            //{
            //    Carga_tablaCabeceraClose();
            //}
            gvControl.DataBind();

        }

        //protected void gvCabecera_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{

        //    GridViewRow row = gvCabecera.Rows[e.RowIndex];
        //    string miro = gvCabecera.DataKeys[e.RowIndex].Value.ToString();
        //    gvCabecera.EditIndex = -1;
        //    //if (CaCheck.Checked == false)
        //    //{
        //    //    Carga_tablaJornada();
        //    //}
        //    //else
        //    //{
        //    //    Carga_tablaCabeceraClose();
        //    //}
        //    gvCabecera.DataBind();

        //}
        protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        protected void gvControl_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }

        protected void gvCabecera_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvCabecera_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            Carga_tablaControl(dt, dt1, e.SortExpression);
            //Carga_tablaControl(null, null, e.SortExpression);
        }

        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            Carga_tablaControl(dt, dt1, e.SortExpression);
            //Carga_tablaControl(null, null, e.SortExpression);
        }
        

        protected void gvControl_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
            Carga_tablaControl(dt, dt1, e.SortExpression);
            //Carga_tablaControl(null, null, e.SortExpression);
        }

        private void Campos_ordenados()
        {
            ddControlPageSize.Items.Clear();
            ddControlPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddControlPageSize.Items.Insert(1, new ListItem("15", "15"));
            ddControlPageSize.Items.Insert(2, new ListItem("30", "30"));
            ddControlPageSize.Items.Insert(3, new ListItem("50", "50"));
            ddControlPageSize.Items.Insert(4, new ListItem("Todos", "1000"));
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
            //DrJornalHora.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DrJornalNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DrDestajoNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DrResumenNomina.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DrTrabajo.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DdgvProdImpDiaPage.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());
            //DrpanelRaw.Items.AddRange(ddCabeceraPageSize.Items.OfType<ListItem>().ToArray());

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

        protected void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // If multiple buttons are used in a GridView control, use the
            // CommandName property to determine which button was clicked.
            //if (e.CommandName == "Add")
            //{
            //    // Convert the row index stored in the CommandArgument
            //    // property to an Integer.
            //    int index = Convert.ToInt32(e.CommandArgument);

            //    // Retrieve the row that contains the button clicked 
            //    // by the user from the Rows collection.
            //    GridViewRow row = ContactsGridView.Rows[index];

            //    // Create a new ListItem object for the contact in the row.     
            //    ListItem item = new ListItem();
            //    item.Text = Server.HtmlDecode(row.Cells[2].Text) + " " +
            //      Server.HtmlDecode(row.Cells[3].Text);

            //    // If the contact is not already in the ListBox, add the ListItem 
            //    // object to the Items collection of the ListBox control. 
            //    if (!ContactsListBox.Items.Contains(item))
            //    {
            //        ContactsListBox.Items.Add(item);
            //    }
            //}
        }


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

            //for (int i = 0; i < Fields.Count(); i++)
            //{
            //    Campo = "";
            //    Texto = "";

            //    //if (TxtCodigo.Text != "" && Fields[i] == "A.COD_EMPLEADO")
            //    if (Fields[i] == "A.COD_EMPLEADO" || Fields[i] == "COD_EMPLEADO")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtCodigo.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IContent;
            //        Tx = (TextBox)TxtCodigo;
            //    }
            //    else if (Fields[i] == "A.NOMBRE" || Fields[i] == "NOMBRE")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtNombre.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)INombre;
            //        Tx = (TextBox)TxtNombre;
            //    }

            //    else if (Fields[i] == "A.APELLIDOS" || Fields[i] == "APELLIDOS")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtApellidos.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IApellido;
            //        Tx = (TextBox)TxtApellidos;
            //    }
            //    else if (Fields[i] == "CENTRO")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtCentro.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)ICentro;
            //        Tx = (TextBox)TxtCentro;
            //    }
            //    else if (Fields[i] == "CATEGORIA")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtCategoria.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)ICategoria;
            //        Tx = (TextBox)TxtCategoria;
            //    }
            //    else if (Fields[i] == "VIVIENDA")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtVivienda.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IVivienda;
            //        Tx = (TextBox)TxtVivienda;
            //    }
            //    else if (Fields[i] == "A.ENVASE" || Fields[i] == "B.ENVASE")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtEnvase.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IEnvase;
            //        Tx = (TextBox)TxtEnvase;
            //    }
            //    else if (Fields[i] == "A.TAREA")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtVariedad.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IVariedad;
            //        Tx = (TextBox)TxtVariedad;
            //    }
            //    else if (Fields[i] == "A.VARIEDAD")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtVariedad.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IVariedad;
            //        Tx = (TextBox)TxtVariedad;
            //    }
            //    else if (Fields[i] == "A.ZONA")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtZona.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IZona;
            //        Tx = (TextBox)TxtZona;
            //    }
            //    if (Fields[i] == "A.FECHA_INI")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtBFechaIni.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IFechaIni;
            //        Tx = (TextBox)TxtBFechaIni;
            //    }
            //    else if (Fields[i] == "A.FECHA_FIN")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtBFechaFin.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IFechaFin;
            //        Tx = (TextBox)TxtBFechaFin;
            //    }
            //    else if (Fields[i] == "A.RECOTABLET")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtBTablet.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)ITablet;
            //        Tx = (TextBox)TxtBTablet;
            //    }
            //    else if (Fields[i] == "A.TABLET")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtBTablet.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)ITablet;
            //        Tx = (TextBox)TxtBTablet;
            //    }
            //    if (Fields[i] == "A.FECHA_EMPLEADOS" || Fields[i] == "A.FECHA_JORNADA")
            //    {
            //        Campo = Fields[i];
            //        Texto = TxtBFechaIni.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IFechaIni;
            //        Tx = (TextBox)TxtBFechaIni;
            //    }
            //    else if (Fields[i] == "B.FECHA_EMPLEADOS" || Fields[i] == "B.FECHA_JORNADA")
            //    {
            //        Fields[i] = Fields[i].Replace("B.FECHA_EMPLEADOS", "A.FECHA_EMPLEADOS");
            //        Fields[i] = Fields[i].Replace("B.FECHA_JORNADA", "A.FECHA_JORNADA");
            //        Campo = Fields[i];
            //        Texto = TxtBFechaFin.Text.Replace(",", "','");
            //        Ia = (HtmlGenericControl)IFechaFin;
            //        Tx = (TextBox)TxtBFechaFin;
            //    }




            //    //string Miro = Ia.Attributes["style"].ToString();
            //    if (Campo != "")
            //    {
            //        if (Ia.Attributes["class"] == "fa fa-hand-o-up fa-2x")
            //        {
            //        }
            //        else
            //        {
            //            MontaIconoConsulta(Ia, Tx, Campo, this.Session["Filtro"].ToString());
            //        }
            //    }
            //}

            //string Miro = this.Session["filtrolocal"].ToString();

            //Miro = this.Session["Filtro"].ToString();

            //if (this.Session["Filtro"].ToString() != "")
            //{
            //    DrVistaEmpleado.Attributes.Add("style", "background-color:#e6f2e1");
            //    //this.Session["Filtro"] = Filtros;
            //}
            //else
            //{
            //    DrVistaEmpleado.Attributes.Add("style", "background-color:#fff");
            //}
        }

        private void CreaCamposHtml()
        {
            string Miro = "";

            for (int i = 0; i <= 50; i++)
            {
                Miro =  Environment.NewLine;
                Miro += "<div class=\"row\" runat=\"server\" id=\"DivReg" + i + "\" visible=\"false\">" + Environment.NewLine;
                Miro += "	<div class=\"col-lg-2\" runat=\"server\" id=\"DivColumA" + i + "\">" + Environment.NewLine;
                //Miro += "		<label id=\"lbL" + i + "\" visible=\"true\" runat=\"server\" style=\"width:100%\" >ID:</label>" + Environment.NewLine;
                Miro += "		<asp:TextBox id=\"lbL" + i + "\" runat=\"server\" style=\"width:100%;border-style:none;background-color:#ffffff;\" ReadOnly=\"true\" class=\"form-control\" placeholder=\"Introduzca su identificador\"></asp:TextBox>" + Environment.NewLine;
                Miro += "	</div>" + Environment.NewLine;
                Miro += "	<div class=\"col-lg-4\" runat=\"server\" id=\"DivColumB" + i + "\">" + Environment.NewLine;
                Miro += "		<asp:TextBox id=\"TxL" + i + "\" runat=\"server\" style=\"width:100%\" ReadOnly=\"true\" class=\"form-control\" placeholder=\"Introduzca su identificador\"></asp:TextBox>" + Environment.NewLine;
                Miro += "	</div>" + Environment.NewLine;
                //Miro += "	<div class=\"col-lg-1\" runat=\"server\" id=\"DivColumC" + i + "\">" + Environment.NewLine;
                //Miro += "		<button id=\"BtL" + i + "\" type=\"button\" runat=\"server\" style=\"width:30%\" class=\"btn btn-danger btn-circle\" visible=\"true\" onServerClick=\"BtnActivate_click\"><i id=\"I1\" runat=\"server\" class=\"fa fa-thumbs-o-down\"></i></button>" + Environment.NewLine;
                //Miro += "	</div>" + Environment.NewLine;
                Miro += "	<div class=\"col-lg-2\" runat=\"server\" id=\"DivColumD" + i + "\">" + Environment.NewLine;
                //Miro += "		<label id=\"lbD" + i + "\" visible=\"true\" runat=\"server\" style=\"width:100%\" >LOTE:</label>" + Environment.NewLine;
                Miro += "		<asp:TextBox id=\"lbD" + i + "\" runat=\"server\" style=\"width:100%;border-style:none;background-color:#ffffff;\" ReadOnly=\"true\" class=\"form-control\" placeholder=\"Introduzca su identificador\"></asp:TextBox>" + Environment.NewLine;
                Miro += "	</div>" + Environment.NewLine;
                Miro += "	<div class=\"col-lg-4\" runat=\"server\" id=\"DivColumE" + i + "\">" + Environment.NewLine;
                Miro += "		<asp:TextBox id=\"TxD" + i + "\" runat=\"server\" style=\"width:100%\" ReadOnly=\"true\" class=\"form-control\" placeholder=\"Introduzca su identificador\"></asp:TextBox>" + Environment.NewLine;
                Miro += "	</div>" + Environment.NewLine;
                //Miro += "	<div class=\"col-lg-1\" runat=\"server\" id=\"DivColumF" + i + "\">" + Environment.NewLine;
                //Miro += "		<button id=\"BtD" + i + "\" type=\"button\" runat=\"server\" style=\"width:30%\" class=\"btn btn-danger btn-circle\" visible=\"true\" onServerClick=\"BtnActivate_click\"><i id=\"I7\" runat=\"server\" class=\"fa fa-thumbs-o-down\"></i></button>" + Environment.NewLine;
                //Miro += "	</div>" + Environment.NewLine;
                Miro += "</div>" + Environment.NewLine;
                string a = Main.Ficherotraza(Miro,"1");
                //gvControl.HeaderRow.Cells[i].Visible = false;//hide grid column header
            }

            //<div class="row" runat="server" id="DivReg0" visible="false">
            //	<div class="col-lg-2" runat="server" id="DivColumA0">
            //		<label id="lbL0" visible="true" runat="server" style="width:100%" >ID:</label>
            //	</div>   
            //	<div class="col-lg-3" runat="server" id="DivColumB0">
            //		<asp:TextBox id="TxL0" runat="server" style="width:100%" ReadOnly="true" class="form-control" placeholder="Introduzca su identificador"></asp:TextBox>
            //	</div>  
            //	<div class="col-lg-1" runat="server" id="DivColumC0">
            //		<button id="BtL0" type="button" runat="server" style="width:30%" class="btn btn-danger btn-circle" visible="true" onServerClick="BtnActivate_click"><i id="I1" runat="server" class="fa fa-thumbs-o-down"></i></button>
            //	</div>
            //	<div class="col-lg-2" runat="server" id="DivColumD0">
            //		<label id="lbD0" visible="true" runat="server" style="width:100%" >LOTE:</label>
            //	</div>
            //	
            //	<div class="col-lg-3" runat="server" id="DivColumE0">
            //		<asp:TextBox id="TxD0" runat="server" style="width:100%" ReadOnly="true" class="form-control" placeholder="Introduzca su identificador"></asp:TextBox>
            //	</div>  

            //	<div class="col-lg-1" runat="server" id="DivColumF0">
            //		<button id="BtD0" type="button" runat="server" style="width:30%" class="btn btn-danger btn-circle" visible="true" onServerClick="BtnActivate_click"><i id="I7" runat="server" class="fa fa-thumbs-o-down"></i></button>
            //	</div>
            //</div>

        }

        public string HTMLaTXT(string s)
        {
            //Detalle cami&#243;n en excel &quot;Ordenes de carga 2021&quot;. Italia Sicilia (dep&#243;sito y clientes finales: Agricoop y Angileri)
            //de
            //s = s.Replace("á", "&aacute;");
            //s = s.Replace("é", "&eacute;");
            //s = s.Replace("í", "&iacute;");
            //s = s.Replace("ú", "&uacute;");
            //s = s.Replace("ó", "&oacute;");
            //s = s.Replace("ó", "&oacute;");
            //s = s.Replace("ñ", "&ntilde;");
            //s = s.Replace("\"\"", "&quot;");
            //https://docs.microsoft.com/en-us/office/vba/language/reference/user-interface-help/character-set-128255
            //s = s.Replace("ó", "&#243;");
            //a
            s = s.Replace( "&aacute;" , "á");
            s = s.Replace( "&eacute;" , "é");
            s = s.Replace( "&iacute;" , "í");
            s = s.Replace( "&uacute;" , "ú");
            s = s.Replace( "&oacute;" , "ó");
            s = s.Replace( "&oacute;" , "ó");
            s = s.Replace( "&ntilde;" , "ñ");
            s = s.Replace( "&quot;" , "\"");
            s = s.Replace("&#225;", "á");
            s = s.Replace("&#233;", "é");
            s = s.Replace("&#237;", "í");
            s = s.Replace("&#243;", "ó");
            s = s.Replace("&#250;", "ú");         
            s = s.Replace("&#193;", "Á");
            s = s.Replace("&#201;", "É");
            s = s.Replace("&#205;", "Í");
            s = s.Replace("&#211;", "Ó");
            s = s.Replace("&#218;", "Ú");
            s = s.Replace("&#209;", "Ñ");
            s = s.Replace("&#241;", "ñ");
            s = s.Replace("&#160;", " ");
            return s;
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
                if(this.Session["idarchivo"].ToString() == "0") 
                {
                    gvControl.DataSource = dt;
                    gvControl.DataBind();
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
                                break;
                            }
                        }
                    }
                    break;
                }

                if ( Vista != "" ) //Vista a consultar
                {
                    dt = Main.BuscaLote(Vista).Tables[0];
                }
                else if (MiCampo != "" ) //Key identity distinta
                {
                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();
                        if (SQL == "")
                        {
                            if(MiCampo == fila["ZTITULO"].ToString())
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
                            if (MiCampo == fila["ZTITULO"].ToString())
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
                else //Normal
                {
                    foreach (DataRow fila in dtCampos.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();
                        if(SQL == "")
                        {
                            SQL += fila["ZTITULO"].ToString() ;
                        }
                        else
                        {
                            SQL += "," + fila["ZTITULO"].ToString() ;
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
                    dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

                lbRowControl.Text = "Registros: " + dt.Rows.Count;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvControl.DataSource = dv;
                    //gvCabecera.DataSource = dv;
                }
                else
                {
                    gvControl.DataSource = dt;
                    //gvCabecera.DataSource = dt;
                }
                gvControl.DataBind();
                //gvCabecera.DataSource = dt;
                //gvCabecera.DataBind();

                //if (gvControl.Rows.Count > 0)// check if grid not empty
                //{
                //    for (int i = 0; i <= gvControl.HeaderRow.Cells.Count; i++)
                //    {
                //        gvControl.HeaderRow.Cells[i].Visible = false;//hide grid column header
                //    }
                //    for (int i = 0; i <= gvCabecera.HeaderRow.Cells.Count; i++)
                //    {
                //        gvCabecera.Columns[i].Visible = false;//hide grid column value
                //    }
                //}


                gvControl.EditIndex = -1;
                BtnNewDato_Estado0();
                //Busca Error
                Lberror.Text = "";
                this.Session["IDGridA"] = "";

            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }

        private void BtnNewDato_Estado0()
        {
            //estado general de la pagina
            HtmlButton btn2 = (HtmlButton)FindControl("BtnNewDato");
            btn2.InnerText = "Nuevo Datos";
            btn2.Attributes["class"] = "btn btn-success";

            btn2 = (HtmlButton)FindControl("BtnModificaDato");
            btn2.InnerText = "Editar Datos";
            btn2.Attributes["class"] = "btn btn-success";

            btn2 = (HtmlButton)FindControl("BtnGuardaDato");
            btn2.InnerText = "Cerrar";
            btn2.Attributes["class"] = "btn btn-warning";

            btn2 = (HtmlButton)FindControl("BtnEliminaDato");
            btn2.InnerText = "Eliminar Datos";
            btn2.Attributes["class"] = "btn btn-danger";
        }

        private void EnableCajasTexto(Boolean Habilita, int Nuevo)
        {
            for (int N = 0; N <= 50; N++)//Hasta 50 campos
            {
                string MiContent = "DivReg" + N;
                HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                MiContent = "TxL" + N;                      
                TextBox DivLabel = FindControl(MiContent) as TextBox;
                if (Habilita == true)
                {
                    DivLabel.ReadOnly = false;
                    DivLabel.BorderStyle = BorderStyle.Inset;
                    DivLabel.Attributes["style"] = "width:100%;border-style:inset;background-color:#eff2e2;";
                    if(Nuevo == 1) { DivLabel.Text = ""; }
                    //DivLabel.BackColor = Color.FromName("#eff2e2");
                    //style="width:100%;border-style:none;background-color:#ffffff;"
                    //DivLabel.Enabled = true;
                }
                else
                {
                   // DivLabel.BackColor = Color.FromName("#efefef");
                    DivLabel.Attributes["style"] = "width:100%;border-style:inset;background-color:#efefef;";
                    if (Nuevo == 1) { DivLabel.Text = ""; }
                    DivLabel.ReadOnly = true;
                    //DivLabel.Enabled = false;
                }
                MiContent = "TxD" + N;               
                DivLabel = FindControl(MiContent) as TextBox;
                if (Habilita == true)
                {
                    DivLabel.ReadOnly = false;
                    DivLabel.BorderStyle = BorderStyle.Inset;
                    if (Nuevo == 1) { DivLabel.Text = ""; }
                    //DivLabel.BackColor = Color.FromName("#eff2e2");
                    DivLabel.Attributes["style"] = "width:100%;border-style:inset;background-color:#eff2e2;";
                    //DivLabel.Enabled = true;
                }
                else
                {
                    DivLabel.Attributes["style"] = "width:100%;border-style:inset;background-color:#efefef;";
                    if (Nuevo == 1) { DivLabel.Text = ""; }
                    //DivLabel.BackColor = Color.FromName("#efefef");
                    DivLabel.ReadOnly = true;
                    //DivLabel.Enabled = false;
                }
            }
        }

        protected void BtnNewDato_click(object sender, EventArgs e)
        {
            //estado nuevo de la pagina
            HtmlButton btn2 = (HtmlButton)FindControl("BtnNewDato");
            btn2.InnerText = "Nuevo Datos";
            btn2.Attributes["class"] = "btn btn-success disabled";

            btn2 = (HtmlButton)FindControl("BtnModificaDato");
            btn2.InnerText = "Cancelar Nuevo";
            btn2.Attributes["class"] = "btn btn-warning";

            btn2 = (HtmlButton)FindControl("BtnGuardaDato");
            btn2.InnerText = "Guardar nuevos Datos";
            btn2.Attributes["class"] = "btn btn-primary";

            btn2 = (HtmlButton)FindControl("BtnEliminaDato");
            btn2.InnerText = "Eliminar Datos";
            btn2.Attributes["class"] = "btn btn-danger  disabled";
            EnableCajasTexto(true,1);
        }

        protected void BtnModificaDato_Click(object sender, EventArgs e)
        {
            //estado cancela de la pagina
            HtmlButton btn2 = (HtmlButton)FindControl("BtnModificaDato");
            if(btn2.InnerText == "Cancelar Nuevo" || btn2.InnerText == "Cancelar Edición") //Si esta editado nuevo
            {
                EnableCajasTexto(false,0);
                BtnNewDato_Estado0();
            }
            else //Quiero modificar un dato
            {
                if (this.Session["IDGridA"].ToString() == "")
                {
                    Lbmensaje.Text = "No tiene seleccionado un identificador de la lista. Seleccione uno para mmodificar.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    DvPreparado.Visible = true;
                    return;
                }
                else
                {
                    btn2 = (HtmlButton)FindControl("BtnNewDato");
                    btn2.InnerText = "Dato Editado";
                    btn2.Attributes["class"] = "btn btn-success disabled";

                    btn2 = (HtmlButton)FindControl("BtnModificaDato");
                    btn2.InnerText = "Cancelar Edición";
                    btn2.Attributes["class"] = "btn btn-warning";

                    btn2 = (HtmlButton)FindControl("BtnGuardaDato");
                    btn2.InnerText = "Guardar Dato modificado";
                    btn2.Attributes["class"] = "btn btn-primary";

                    btn2 = (HtmlButton)FindControl("BtnEliminaDato");
                    btn2.InnerText = "Eliminar Datos";
                    btn2.Attributes["class"] = "btn btn-danger disabled";

                    EnableCajasTexto(true, 0);
                }
            }
        }
        protected void BtnGuardaDato_click(object sender, EventArgs e)
        {
            //Estado guardar de la pagina
            HtmlButton btn2 = (HtmlButton)FindControl("BtnModificaDato");
            if (btn2.InnerText == "Cancelar Nuevo") // si esta en Cancelar Edición
            {
                CreaConsultaInsercion("0"); //Insercion
            }
            else if (btn2.InnerText == "Cancelar Edición")// si esta en Cancelar Edición
            {
                CreaConsultaInsercion("1"); //actualizacion
            }
            else //cierra ventana
            {
                DivCampos0.Visible = false;
            }
            //BtnNewDato_Estado0();

            DataTable dtCampos = this.Session["Campos"] as DataTable;
            DataTable dtArchivos = this.Session["Archivos"] as DataTable;
            Carga_tablaControl(dtArchivos, dtCampos, null);
            EnableCajasTexto(false, 1);
            //DivCampos0.Visible = false;
        }
        protected void btnTree_Click(object sender, EventArgs e)
        {
            divTree.Visible = true;
            DivGrid.Visible = false;
            DivCampos0.Visible = false;
        }
            
        protected void BtnEliminaDato_click(object sender, EventArgs e)
        {
            //Elimina el dato
            if (this.Session["IDGridA"].ToString() == "")
            {
                Lbmensaje.Text = "No tiene seleccionado un identificador de la lista. Seleccione uno para eliminar.";
                cuestion.Visible = false;
                Asume.Visible = true;
                DvPreparado.Visible = true;
                return;
            }
            else
            {
                CreaConsultaInsercion("2"); //Eliminacion

                DataTable dtCampos = this.Session["Campos"] as DataTable;
                DataTable dtArchivos = this.Session["Archivos"] as DataTable;
                Carga_tablaControl(dtArchivos, dtCampos, null);
                EnableCajasTexto(false, 1);
                //DivCampos0.Visible = false;
            }
        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            //string miro = TxtCodigo.Text;
            DataTable dtCampos = this.Session["Campos"] as DataTable;
            DataTable dtArchivos = this.Session["Archivos"] as DataTable;

            Carga_tablaControl(dtArchivos, dtCampos, null);
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

        private void CreaConsultaInsercion(string TipoConsulta)
        {
            int AA = 0;
            int UU = 0;
            string Vista = "";
            string Key = "";
            string MiCampo = "";
            string DatoCampo = "";
            string Tabla = "";
            string SQL = "";
            string SQLInsercion = "";
            string SQLUpdate = "";
            
            int cuantos = DivCampos0.Controls.Count;

            DataTable dtC = this.Session["Campos"] as DataTable;
            DataTable dtA = this.Session["Archivos"] as DataTable;

            try
            {
                //Miro si es una Vista
                foreach (DataRow fila in dtA.Rows)//Archivos
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
                //Miro que campo es la Key
                foreach (DataRow fila in dtA.Rows)//Archivos
                {
                    Key = fila["ZKEY"].ToString();
                    if (Key == "0" || Key == "" || Key == null)
                    {
                        Key = "";
                        MiCampo = "ZID";
                    }
                    else
                    {
                        foreach (DataRow fila2 in dtC.Rows)//Campos
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
                    foreach (DataRow fila in dtC.Rows)
                    {
                        Tabla = fila["ZTABLENAME"].ToString();
                        break;
                    }
                    //Con vista no actualiza
                    Lbmensaje.Text = "El Archivo Documental con nombre " + Tabla + " esta creado como una Vista. No se puede actualizar";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    DvPreparado.Visible = true;
                    return;
                }
                else  //normal
                {
                    int N = 0;
                    foreach (DataRow fila in dtC.Rows) //Campos
                    {
                        if (Tabla == "") 
                        { 
                            Tabla = fila["ZTABLENAME"].ToString();
                            
                        }

                        if ((AA % 2) == 0)
                        {
                            //Par
                            string MiContent = "DivReg" + N;
                            MiContent = "TxL" + N; //linea campo                       
                            TextBox DivLabel = FindControl(MiContent) as TextBox;
                            //DivLabel.Text = "";

                            if (TipoConsulta == "0")//Insercion
                            {
                                if (SQLInsercion == "")
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        SQLInsercion = " INSERT INTO " + Tabla + " (" +  fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate = " VALUES (" + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate = " VALUES ('" + DivLabel.Text + "'";
                                        }
                                        
                                    }
                                }
                                else 
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        SQLInsercion += ", " + fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate += "," + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate += ",'" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                            }
                            else if (TipoConsulta == "1")//Actualizacion
                            {
                                if (SQLUpdate == "")
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                                else
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                            }
                            else //Eliminacion 2
                            {
                                if (MiCampo == fila["ZTITULO"].ToString())
                                {
                                    SQLUpdate += " DELETE FROM " + Tabla + " WHERE " + MiCampo + " = " + this.Session["IDGridA"].ToString();
                                    break;
                                }
                            }
                        }
                        else//Impar
                        {
                            string MiContent = "DivReg" + N;
                            MiContent = "TxD" + N; //linea campo                       
                            TextBox DivLabel = FindControl(MiContent) as TextBox;
                            //DivLabel.Text = "";

                            if (TipoConsulta == "0")//Insercion
                            {
                                if (SQLInsercion == "")
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        SQLInsercion = " INSERT INTO " + Tabla + " (" + fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate = " VALUES (" + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate = " VALUES ('" + DivLabel.Text + "'";
                                        }

                                    }
                                }
                                else
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        SQLInsercion += ", " + fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate += "," + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate += ",'" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                            }
                            else if (TipoConsulta == "1")//Actualizacion
                            {
                                if (SQLUpdate == "")
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                                else
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        DatoCampo = DivLabel.Text;
                                    }
                                    else
                                    {
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                        }
                                        else
                                        {
                                            SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                        }
                                    }
                                }
                            }
                            else //Eliminacion 2
                            {
                                if (MiCampo == fila["ZTITULO"].ToString())
                                {
                                    SQLUpdate += " DELETE FROM " + Tabla + " WHERE " + MiCampo + " = " + this.Session["IDGridA"].ToString();
                                    break;
                                }
                            }
                            N += 1;
                        }
                        AA += 1;        
                    }
                    if (TipoConsulta == "0")//Insercion
                    {
                        SQLInsercion += ") ";
                        SQLUpdate += ") ";
                        DBHelper.ExecuteNonQuery(SQLInsercion + SQLUpdate);
                    }
                    else if (TipoConsulta == "1")//Actualizacion
                    {
                        SQLUpdate += " WHERE " + MiCampo + " = " + this.Session["IDGridA"].ToString();
                        DBHelper.ExecuteNonQuery(SQLUpdate);
                    }
                    else //Eliminación 2
                    {
                        DBHelper.ExecuteNonQuery(SQLUpdate);
                    }
                }
                this.Session["IDGridA"]="";
            }
            catch (Exception mm)
            {
                string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }

        protected void BtnGuardaMascaraPer_click(object sender, EventArgs e)
        {
            //this.Session["PublicaPersonal"] = "0";// = false;
            //HtmlButton btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //btn2.Visible = true;
            ////btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            ////btn2.Visible = false;
            //OcultaPersonal();
            //GuardarMascara_Click(sender, e);
            ////GuardarPerfil_Click(sender, e);
        }
        protected void GuardaP4_Click(object sender, EventArgs e)
        {
            //Modificar


            return;
            //HtmlButton btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //btn2.Visible = true;

            //btn2 = (HtmlButton)FindControl("btHAE");
            //btn2.Attributes["class"] = "btn btn-primary";
            //btn2.InnerText = "Editar Datos";
            //btn2 = (HtmlButton)FindControl("btHAC");
            //btn2.Attributes["class"] = "btn btn-warning disabled";

            //GuardarPerfil_Click(sender, e);
            //this.Session["Personal"] = "0";// = false;
        }
        protected void BtnActivate_click(object sender, EventArgs e)
        {
            // Nuevo registro 


            return;
            //HtmlButton btn = (HtmlButton)sender;
            //string Miro = "";
            ////Seccion del menu 2
            //this.Session["Menu"] = "2";
            //if (btn.ID == "EdCon") //edita conocimiento
            //{
            //    HtmlButton btn2 = (HtmlButton)FindControl("GuCon");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        //Variables.Historial = true;
            //        this.Session["EditKnow"] = "1";// true;
            //        txtDescripcion.Visible = false;
            //        tituloaccion.Visible = false;
            //        TxTituKnow.Visible = true;
            //        TxDescripKnow.Visible = true;
            //        TxtVideo.Visible = true;
            //        TxtViewVideo.Visible = false;
            //        DRCategoriaC.Visible = true;
            //        LbCategoriaC.Visible = false;
            //        ModifImageCono.Visible = true;
            //        btn = (HtmlButton)FindControl("PublicCono");
            //        btn.Attributes["class"] = "btn btn-warning";
            //        btn.Visible = false;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        //Variables.Historial = false;
            //        this.Session["EditKnow"] = "0";// false;
            //        txtDescripcion.Visible = true;
            //        tituloaccion.Visible = true;
            //        TxTituKnow.Visible = false;
            //        TxDescripKnow.Visible = false;
            //        TxtVideo.Visible = false;
            //        TxtViewVideo.Visible = true;
            //        DRCategoriaC.Visible = false;
            //        LbCategoriaC.Visible = true;
            //        ModifImageCono.Visible = false;
            //        btn = (HtmlButton)FindControl("PublicCono");
            //        btn.Attributes["class"] = "btn btn-warning";
            //        btn.Visible = true;

            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            ////Seccion del menu 1
            //this.Session["Menu"] = "1";
            //this.Session["Sube"] = "0"; // "4";
            //if (btn.ID == "EditPublicaPersonal") //publicacion personal
            //{
            //    this.Session["Collapse"] = "1";
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Publicación";
            //        btn.Attributes["class"] = "btn btn-success";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi1");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi2");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi3");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi4");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi5");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi6");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi7");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi8");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi9");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi10");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi11");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi12");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi13");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi14");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I4");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I5");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHAE");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHAC");
            //        btn2.Visible = false;
            //        this.Session["PublicaPersonal"] = "1";// true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Publicación";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi1");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi2");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi3");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi4");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi5");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi6");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi7");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi8");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi9");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi10");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi11");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi12");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi13");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi14");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I4");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I5");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHAE");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHAC");
            //        btn2.Visible = true;
            //        this.Session["PublicaPersonal"] = "0";// false;
            //    }

            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "EditPublicProfesional") //publicacion personal
            //{
            //    this.Session["Collapse"] = "2";
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Publicación";
            //        btn.Attributes["class"] = "btn btn-success";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi16");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi17");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi18");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi19");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi20");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi21");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi22");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi23");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi24");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi25");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi26");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi27");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I12");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I15");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicProfesional");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHBE");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHBC");
            //        btn2.Visible = false;


            //        this.Session["PublicaProfesional"] = "1";// = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Publicación";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi16");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi17");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi18");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi19");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi20");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi21");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi22");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi23");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi24");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi25");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi26");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi27");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I12");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I15");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicProfesional");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHBE");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHBC");
            //        btn2.Visible = true;
            //        this.Session["PublicaProfesional"] = "0";// false;
            //    }

            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}


            //if (btn.ID == "btD1D") //desempeño
            //{
            //    this.Session["Collapse"] = "4";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btD2D");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Desempeno"] = "1";// = true;
            //        P3.Visible = false;
            //        TxtP3.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Desempeno"] = "0";// false;
            //        P3.Visible = true;
            //        TxtP3.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "ImgTuring") //herramientas
            //{
            //    this.Session["Collapse"] = "6";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btD2D");
            //    //if (btn.Attributes["class"] == "btn btn-primary")
            //    //{
            //    //    btn.InnerText = "Cancelar";
            //    //    btn.Attributes["class"] = "btn btn-success";
            //    //    btn2.Attributes["class"] = "btn btn-warning";
            //    //    this.Session["Desempeno"] = "1";// = true;
            //    //    P3.Visible = false;
            //    //    TxtP3.Visible = true;
            //    //}
            //    //else
            //    //{
            //    //btn.Attributes["class"] = "btn btn-primary";
            //    //btn.InnerText = "Editar";
            //    //btn2.Attributes["class"] = "btn btn-warning disabled";
            //    //this.Session["Desempeno"] = "0";// false;
            //    //P3.Visible = true;
            //    //TxtP3.Visible = false;
            //    //}
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}


            //if (btn.ID == "btHAE") //edita perfil personal
            //{
            //    this.Session["Collapse"] = "1";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btHAC");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Datos";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Personal"] = "1";// = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;
            //        txtNombre.ReadOnly = false;
            //        TxFechaNacimiento.ReadOnly = false;
            //        TxtDNI.ReadOnly = false;
            //        txCalle.ReadOnly = false;
            //        TxtNumero.ReadOnly = false;
            //        TxtPuerta.ReadOnly = false;
            //        btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //        btn2.Visible = false;

            //        //DlPosCvid.Enabled = true;
            //        //dlPais.Enabled = true;
            //        //dlProvincia.Enabled = true;
            //        //DlMunicipio.Enabled = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Personal"] = "0";// false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;
            //        txtNombre.ReadOnly = true;
            //        TxFechaNacimiento.ReadOnly = true;
            //        TxtDNI.ReadOnly = true;
            //        txCalle.ReadOnly = true;
            //        TxtNumero.ReadOnly = true;
            //        TxtPuerta.ReadOnly = true;
            //        btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //        btn2.Visible = true;
            //        //DlPosCvid.Enabled = false;
            //        //dlPais.Enabled = false;
            //        //dlProvincia.Enabled = false;
            //        //DlMunicipio.Enabled = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btHBE") //edita profesional
            //{
            //    this.Session["Collapse"] = "2";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btHBC");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Datos";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Profesional"] = "1";//  = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;

            //        //DrEntidadFin.Enabled = true;
            //        //DrSolicCertificado.Enabled = true;
            //        //DlDispositivos.Enabled = true;

            //        TxtBlog.ReadOnly = false;
            //        TxtPeriodoFecha.ReadOnly = false;
            //        TxtIBAN.ReadOnly = false;
            //        TxtNumCuenta.ReadOnly = false;
            //        TxtKnow.ReadOnly = false;
            //        Txtvendidos.ReadOnly = false;
            //        TxtKnowInter.ReadOnly = false;
            //        TxtKnowUser.ReadOnly = false;
            //        TxtProfesion.ReadOnly = false;


            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Profesional"] = "0";//false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;

            //        //DrEntidadFin.Enabled = false;
            //        //DrSolicCertificado.Enabled = false;
            //        //DlDispositivos.Enabled = false;

            //        TxtBlog.ReadOnly = true;
            //        TxtPeriodoFecha.ReadOnly = true;
            //        TxtIBAN.ReadOnly = true;
            //        TxtNumCuenta.ReadOnly = true;
            //        TxtKnow.ReadOnly = true;
            //        Txtvendidos.ReadOnly = true;
            //        TxtKnowInter.ReadOnly = true;
            //        TxtKnowUser.ReadOnly = true;
            //        TxtProfesion.ReadOnly = true;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH1E") //academico
            //{
            //    this.Session["Collapse"] = "5";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH1C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Academico"] = "1";// = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Academico"] = "0";//false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH2E") //laboral
            //{
            //    this.Session["Collapse"] = "3";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH2C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Laboral"] = "1";// = true;
            //        P2.Visible = false;
            //        TxtP2.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Laboral"] = "0";//  false;
            //        P2.Visible = true;
            //        TxtP2.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH3E") //edita boton P3
            //{
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH3C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Historial"] = "1";//  = true; 
            //        P3.Visible = false;
            //        TxtP3.Visible = true;
            //        this.Session["Collapse"] = "5";
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Historial"] = "0";// false;
            //        P3.Visible = true;
            //        TxtP3.Visible = false;
            //        this.Session["Collapse"] = "0";
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //Variables.dsMiMascara.AcceptChanges();

            //if (this.Session["PublicaPersonal"].ToString() == "1") // == true)
            //{
            //    this.Session["Collapse"] = "1";
            //    if (btn.ID == "bi1") //ZIDUSUARIO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti1");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows) //dsMascaraPerfil
            //        {
            //            fila["ZUSUARIO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi2")//ZALTAFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti2");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALTAFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi3")//ZALIAS
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti3");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALIAS"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi4")//ZNACIMIENTO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti4");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZNACIMIENTO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi5")//ZDNI
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti5");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZDNI"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi6")//ZPAIS
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti6");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPAIS"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi7")//ZPROVINCIA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti7");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPROVINCIA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi8")//ZMUNICIPIO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti8");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZMUNICIPIO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi9")//ZCALLE
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti9");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZCALLE"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi10")//ZPUERTA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti10");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPUERTA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi11")//ZPUNUMERO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti11");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPUNUMERO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi12")//ZCOVID
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti12");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZCOVID"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi13")//ZALTAFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti13");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALTAFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi14")//ZENTIDAD
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti14");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZENTIDAD"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (btn.ID == "bi1" || btn.ID == "bi2" || btn.ID == "bi3" || btn.ID == "bi4" || btn.ID == "bi5" ||
            //        btn.ID == "bi6" || btn.ID == "bi7" || btn.ID == "bi8" || btn.ID == "bi9" || btn.ID == "bi10" ||
            //        btn.ID == "bi11" || btn.ID == "bi12" || btn.ID == "bi13" || btn.ID == "bi14")
            //    {
            //        this.Session["Collapse"] = "1";
            //    }
            //}

            //if (this.Session["PublicaProfesional"].ToString() == "1") // true)
            //{
            //    this.Session["Collapse"] = "2";
            //    if (btn.ID == "bi17")//ZIBAN
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti17");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZIBAN"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi18")//ZNUMCUENTA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti18");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZNUMCUENTA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi19")//ZSOLICITUD
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti19");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZSOLICITUD"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi20")//ZPERIODOFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti20");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPERIODOFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi21")//ZWORKING
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti21");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKING"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi22")//ZWORKINGVENT
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti22");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKINGVENT"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi23")//ZWORKINGINTER
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti23");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKINGINTER"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi24")//ZBLOG
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti24");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZBLOG"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi25")//ZPROFESION
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti25");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPROFESION"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi26")//ZDISPOSITIVO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti26");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZDISPOSITIVO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi27")//ZMAIL
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti27");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZMAIL"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (btn.ID == "bi16" || btn.ID == "bi17" || btn.ID == "bi18" || btn.ID == "bi19" || btn.ID == "bi20" ||
            //        btn.ID == "bi21" || btn.ID == "bi22" || btn.ID == "bi23" || btn.ID == "bi24" || btn.ID == "bi25" ||
            //        btn.ID == "bi26" || btn.ID == "bi27")
            //    {
            //        this.Session["Collapse"] = "2";
            //    }
            //}

            //if (btn.ID == "I8")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I1");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZDESEMPENO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I9")//I1 I8 I9 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I1");
            //    img.Visible = true;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZDESEMPENO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I6")//I1 I8 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I2");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZLABORAL"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I7")//I1 I8 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I2");
            //    img.Visible = false;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZLABORAL"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I11")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I3");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZACADEMICO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I10")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I3");
            //    img.Visible = false;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZACADEMICO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}



            //Carga_collapse();

        }

        protected void DrCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrArchivos.SelectedIndex = DrCampos.SelectedIndex;
            DrArchivos_SelectedIndexChanged(null, null);
        }
            
        private void CreaGridControl(DataTable dtArchivo, DataTable dtCampo )
        {
            //int i =  Convert.ToInt32(this.Session["NumeroPalet"].ToString());

            //if (i == 0) { return; }
            //container2.Controls.Clear();
            gvControl.Columns.Clear();
            //for (int X = 0; X < gvControl.Columns.Count; X++)
            //{
            //    gvControl.Columns.RemoveAt(X);//column index
            //}

            //gvControl.Columns.Remove(DataControlField);

            //controles
            //Según implementacion dinamicos
            //DivCampos0.Controls.Clear();
            //Según implementacion html
            //Variable opcion manual o dinamica desde web.config
            int Manual = 0;
            int cuantos = 0;
            int i = 0;
            int a = 0;


            if (Manual == 0) //Manual. La variable en web.config
            {
                cuantos = DivCampos0.Controls.Count;

                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N; 
                    HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                    DivColumA.Visible = false;
                    MiContent = "lbL" + N; //linea campo                       
                    TextBox DivLabel = FindControl(MiContent) as TextBox;
                    DivLabel.Text = "";
                    MiContent = "TxL" + N; //linea campo                       
                    DivLabel = FindControl(MiContent) as TextBox;
                    DivLabel.Text = "";
                    MiContent = "lbD" + N; //linea campo                       
                    DivLabel = FindControl(MiContent) as TextBox;
                    DivLabel.Text = "";
                    MiContent = "TxD" + N; //linea campo                       
                    DivLabel = FindControl(MiContent) as TextBox;
                    DivLabel.Text = "";
                }


                HtmlGenericControl DivContent = new HtmlGenericControl();

                gvControl.Columns.Add(new ButtonField() { CommandName = "BajaOrden", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });

                foreach (DataRow filas in dtCampo.Rows)
                {
                    if ((i % 2) == 0)
                    {
                        // IZQUIERDA
                        string MiContent = "DivReg" + a; //linea campo
                        HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        DivColumA.Visible = true;

                        MiContent = "lbL" + a; //linea campo                       
                        TextBox DivLabel = FindControl(MiContent) as TextBox;
                        //Label DivLabel = DivCampos0.FindControl(MiContent) as Label;
                        //Label DivLabel =  new Label();
                        //DivLabel.ID = MiContent;
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        MiContent = "DivColumB" + a; //linea campo
                        HtmlGenericControl DivColumB = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        DivColumB.Visible = true;

                        MiContent = "TxL" + a; //linea campo
                        TextBox DivTexto = FindControl(MiContent) as TextBox;
                        //TextBox DivTexto = (DivColumB.FindControl(MiContent) as TextBox);
                        //TextBox DivTexto = new TextBox();
                        //DivTexto.ID = MiContent;
                        DivTexto.Text = "";//filas["ZTITULO"].ToString();
                        DivTexto.ReadOnly = true;
                        DivTexto.Visible = true;
                        DivTexto.BorderStyle = BorderStyle.None;

                        //MiContent = "DivColumC" + i; //linea campo
                        //HtmlGenericControl DivColumC = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        //DivColumC.Visible = true;

                        //MiContent = "btL" + i; //linea campo
                        //Button BtCampo = (DivCampos0.FindControl(MiContent) as Button);
                        //BtCampo.Visible = true;

                        //BoundField Campo = new BoundField();
                        //Campo.DataField = filas["ZTITULO"].ToString();
                        ////Campo.HeaderText = filas["ZDESCRIPCION"].ToString();
                        //DataControlField DataControlField = Campo;
                        //gvControl.Columns.Add(DataControlField);


                    }
                    else
                    {
                        // DERECHA

                        string MiContent = "DivColumD" + a; //linea campo
                        HtmlGenericControl DivColumD = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        DivColumD.Visible = true;

                        //Label DivLabel = (DivColumD.FindControl(MiContent) as Label);
                        MiContent = "lbD" + a; //linea campo
                        TextBox DivLabel = FindControl(MiContent) as TextBox;
                        //Label DivLabel = new Label();
                        //DivLabel.ID = MiContent;
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        MiContent = "DivColumE" + a; //linea campo
                        HtmlGenericControl DivColumE = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        DivColumE.Visible = true;

                        MiContent = "TxD" + a; //linea campo
                        //TextBox DivTexto = (DivColumE.FindControl(MiContent) as TextBox);
                        TextBox DivTexto = FindControl(MiContent) as TextBox;
                        //TextBox DivTexto = new TextBox();
                        //DivTexto.ID = MiContent;
                        DivTexto.Text = "";//filas["ZTITULO"].ToString();
                        DivTexto.ReadOnly = true;
                        DivTexto.Visible = true;

                        //MiContent = "DivColumF" + a; //linea campo
                        //HtmlGenericControl DivColumF = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        //DivColumF.Visible = true;

                        //MiContent = "btD" + i; //linea campo
                        //Button BtCampo = (DivCampos0.FindControl(MiContent) as Button);
                        //BtCampo.Visible = true;

                        //BoundField Campo = new BoundField();
                        //Campo.DataField = filas["ZTITULO"].ToString();
                        ////Campo.HeaderText = filas["ZDESCRIPCION"].ToString();
                        //DataControlField DataControlField = Campo;
                        //gvControl.Columns.Add(DataControlField);
                        a += 1;
                    }
                    i += 1;
                }
                DivCampos0.Attributes["height"] = (i * 50).ToString();
            }
            else //Dinamicos
            {
                DivCampos0.Controls.Clear();

                HtmlGenericControl DivContent = new HtmlGenericControl();

                gvControl.Columns.Add(new ButtonField() { CommandName = "BajaOrden", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });

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

                        BoundField Campo = new BoundField();
                        Campo.DataField = filas["ZTITULO"].ToString();
                        Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                        //gvCabecera_LabFechaPreparacion_1
                        DataControlField DataControlField = Campo;
                        gvControl.Columns.Add(DataControlField);
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

                        BoundField Campo = new BoundField();
                        Campo.DataField = filas["ZTITULO"].ToString();
                        Campo.HeaderText = filas["ZDESCRIPCION"].ToString();
                        DataControlField DataControlField = Campo;
                        gvControl.Columns.Add(DataControlField);
                        //gvCabecera.Columns.Add(DataControlField);
                        //gvControl.Columns.Remove(DataControlField);
                    }

                    if ((i % 2) != 0)
                    {
                        DivCampos0.Controls.Add(DivContent);
                        a += 1;
                    }

                    i += 1;
                }
            }

            //Crea el grid ficheros
            CreaGridFiles();
            //Busca Error
            Lberror.Text = "";
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }

        private void CreaGridFiles()
        {
            DataTable dtt;
            dtt = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("NOMBRE");
            dtt.Columns.Add("FECHA");
            dtt.Columns.Add("CATEGORIA");
            dtt.Columns.Add("PESO");
            dtt.Columns.Add("ZUSER");
            
            DataRow drr;

            int cou = 6;
            for (int i = 0; i <= cou; i++)
            {
                drr = dtt.NewRow();
                drr[0] = i;
                drr[1] = "Nombre Fichero " + i;
                drr[2] = "Descripcion Fichero " + i;
                drr[3] = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                drr[4] = "Categoría " + i;
                drr[5] = "Peso " + i;
                drr[5] = this.Session["UserAlias"].ToString();
                dtt.Rows.Add(drr);
            }
            this.Session["SelTableFiles"] = dtt;
            gvLista.DataSource = dtt;
            gvLista.DataBind();
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

        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Index
            divTree.Visible = false; 
            DivGrid.Visible = true;
            DivCampos0.Visible = false;

            DataTable dtCampos = null;
            DataTable dt = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dt;
            gvControl.Columns.Clear();
            //gvCabecera.Columns.Clear();

            //or
            //int cou = GridView1.Columns.Count;
            //for (int i = 0; i =< cou; i++)
            //{
            //    gvControl.Columns.RemoveAt(0);
            //}

            //foreach (DataRow fila in dt.Rows)
            //{
            //    BoundField Campo = new BoundField();
            //    Campo.DataField = fila["ZTITULO"].ToString();
            //    Campo.HeaderText = fila["ZDESCRIPCION"].ToString();
            //    DataControlField DataControlField = Campo;
            //    gvControl.Columns.Remove(DataControlField);
            //}

            this.Session["idarchivo"] = DrArchivos.SelectedIndex.ToString();

            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + DrArchivos.SelectedItem.Value;
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];
            
            this.Session["Archivos"] = dtArchivos;

            dtCampos = Relaciones(Convert.ToInt32(DrArchivos.SelectedItem.Value), dt);
            this.Session["Campos"] = dtCampos;

            CreaGridControl(dtArchivos, dtCampos);

            Carga_tablaControl( dtArchivos, dtCampos, null);

            DrCampos.SelectedIndex = DrArchivos.SelectedIndex;

            //Texto
            //Variables.categoria = DrCategoria.SelectedItem.ToString();
            //DlConocimientos.Items.Clear();
            //Repeater1.DataSource = null;
            //Repeater1.DataBind();
            //txtDescripcion.Text = "";
            //HtmlImage img = (HtmlImage)FindControl("ImgPrint");
            //img.Attributes.Add("src", "Images/no-img.jpg");
            ////iImage.ImageUrl = "";
            //tituloaccion.Text = "";
            //txtAutor.Text = "";
            //TxtFecha.Text = "";

            //if (DrCategoria.SelectedItem.ToString() == "Todos")
            //{
            //    Variables.FormularioShell = null;
            //    Repeater1.DataSource = Variables.dsFormularios;
            //    Repeater1.DataBind();
            //    DlConocimientos.DataSource = Variables.dsFormularios;
            //    DlConocimientos.DataValueField = "ZID";
            //    DlConocimientos.DataTextField = "ZTITULO";
            //    DlConocimientos.DataBind();

            //    foreach (DataRow fila in Variables.dsFormularios.Rows)//B
            //    {
            //        string Miro = fila["ZID"].ToString();
            //        //Variables.IDFormulario = fila["ZID"].ToString();
            //        txtDescripcion.Text = fila["ZDESCRIPCION"].ToString();
            //        img.Attributes.Add("src", fila["ZIMAGEN"].ToString());
            //        //iImage.ImageUrl = fila["ZIMAGEN"].ToString();
            //        tituloaccion.Text = fila["ZTITULO"].ToString();
            //        txtAutor.Text = fila["ZTITULO"].ToString();
            //        TxtFecha.Text = fila["ZALTAFECHA"].ToString();
            //        this.Session["categoria"] = fila["ZIDCATEGORIA"].ToString();
            //        this.Session["PublicaComentarios"] = fila["ZCOMENTARIO"].ToString();
            //        this.Session["Publico"] = fila["ZPUBLICO"].ToString();
            //        this.Session["IDFormulario"] = fila["ZID"].ToString();
            //        this.Session["idotheruser"] = fila["ZIDUSUARIO"].ToString();
            //        Carga_mensajes(fila["ZID"].ToString());
            //        if (fila["ZPRECIO"].ToString() == "1")
            //        {
            //            txtPVP.Text = "El importe de este conocimiento es de 1 €, o bien, intercambiar por uno de tus conocimientos con este Creador de conocimientos";
            //        }
            //        else
            //        {
            //            txtPVP.Text = "El importe de este conocimiento es gratuito. Gracias al Creador de este conocimiento por aportar este know how al grupo";
            //        }
            //        solicitaConId.Text = "Solicitar este Conocimiento";
            //        solicitaConId.Enabled = true;
            //        solicitaConId.Attributes["class"] = "btn btn-success";
            //        if (this.Session["idotheruser"].ToString() == this.Session["idusuario"].ToString())
            //        {
            //            //Es un conocimiento suyo
            //            solicitaConId.Text = "Este Conocimiento es tuyo";
            //            solicitaConId.Attributes["class"] = "btn btn-warning";
            //            solicitaConId.Enabled = false;
            //            break;
            //        }

            //        foreach (DataRow fila2 in Variables.dsMisFormularios.Rows)//B
            //        {
            //            if (fila["ZID"].ToString().ToString() == fila2["ZINDICE"].ToString())
            //            {
            //                //Ya lo tiene
            //                solicitaConId.Text = "Este Conocimiento ya lo tienes";
            //                solicitaConId.Attributes["class"] = "btn btn-info";
            //                solicitaConId.Enabled = false;
            //                break;
            //            }
            //        }
            //        break;
            //    }
            //}
            //else
            //{
            //    Variables.FormularioShell = null;
            //    foreach (DataRow fila in Variables.dsFormularios.Rows)//B
            //    {
            //        if (fila["ZIDCATEGORIA"].ToString() == this.Session["categoria"].ToString())
            //        {
            //            Variables.FormularioShell = DBHelper.ClasificaTableUser("ZCATEGORIAS", this.Session["categoria"].ToString(), "", "", 0);
            //            //dsFormularios = DBHelper.BuscaLineasDataTables(fila["ZIDCATEGORIA"].ToString(), HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000000006.000");
            //            Repeater1.DataSource = Variables.FormularioShell;
            //            Repeater1.DataBind();
            //            DlConocimientos.DataSource = Variables.FormularioShell;
            //            DlConocimientos.DataValueField = "ZID";
            //            DlConocimientos.DataTextField = "ZTITULO";
            //            DlConocimientos.DataBind();
            //            break;
            //        }
            //    }

            //    if (Variables.FormularioShell != null)
            //    {
            //        foreach (DataRow fila in Variables.FormularioShell.Rows)//B
            //        {
            //            string Miro = fila["ZID"].ToString();
            //            //Variables.IDFormulario = fila["ZID"].ToString();
            //            txtDescripcion.Text = fila["ZDESCRIPCION"].ToString();
            //            img.Attributes.Add("src", fila["ZIMAGEN"].ToString());
            //            //iImage.ImageUrl = fila["ZIMAGEN"].ToString();
            //            tituloaccion.Text = fila["ZTITULO"].ToString();
            //            txtAutor.Text = fila["ZTITULO"].ToString();
            //            TxtFecha.Text = fila["ZALTAFECHA"].ToString();
            //            this.Session["PublicaComentarios"] = fila["ZCOMENTARIO"].ToString();
            //            this.Session["Publico"] = fila["ZPUBLICO"].ToString();
            //            this.Session["IDFormulario"] = fila["ZID"].ToString();
            //            this.Session["idotheruser"] = fila["ZIDUSUARIO"].ToString();
            //            this.Session["categoria"] = fila["ZIDCATEGORIA"].ToString();
            //            Carga_mensajes(fila["ZID"].ToString());
            //            if (fila["ZPRECIO"].ToString() == "1")
            //            {
            //                txtPVP.Text = "El importe de este conocimiento es de 1 €, o bien, intercambiar por uno de tus conocimientos con este Creador de conocimientos";
            //            }
            //            else
            //            {
            //                txtPVP.Text = "El importe de este conocimiento es gratuito. Gracias al Creador de este conocimiento por aportar este know how al grupo";
            //            }
            //            solicitaConId.Text = "Solicitar este Conocimiento";
            //            solicitaConId.Enabled = true;
            //            solicitaConId.Attributes["class"] = "btn btn-success";
            //            if (this.Session["idotheruser"].ToString() == this.Session["idusuario"].ToString())
            //            {
            //                //Es un conocimiento suyo
            //                solicitaConId.Text = "Este Conocimiento es tuyo";
            //                solicitaConId.Attributes["class"] = "btn btn-warning";
            //                solicitaConId.Enabled = false;
            //                break;
            //            }

            //            foreach (DataRow fila2 in Variables.dsMisFormularios.Rows)//B
            //            {
            //                if (fila["ZID"].ToString().ToString() == fila2["ZINDICE"].ToString())
            //                {
            //                    //Ya lo tiene
            //                    solicitaConId.Text = "Este Conocimiento ya lo tienes";
            //                    solicitaConId.Attributes["class"] = "btn btn-info";
            //                    solicitaConId.Enabled = false;
            //                    break;
            //                }
            //            }
            //            break;
            //        }
            //    }
            //}


            //DataTable dt = DBHelper.ClasificaTableUser("CATEGORIAS", Variables.categoria, "", 0);
            //mysliderF1.DataSource = dt;
            //mysliderF1.DataBind();
        }
    }
}