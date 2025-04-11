using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;




namespace Satelite
{
    public partial class AltaDirectorio : System.Web.UI.Page
    {
        //private int registros = 0;
        //private int Competencia = 0;
        //private string[] ListadoArchivos;
        private static int IDDiv = 0;
        //private static string IDTABLA = "-1";

        static TextBox[] ArrayTextBoxs;
        static Label[] ArrayLabels;
        static DropDownList[] ArrayCombos;
        static int contadorControles;

        protected System.Web.UI.WebControls.TreeView tvControl;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("Principal.aspx"); //Default

                //if (Session["UserID"] != null)
                //{
                //    if (Session["UserName"] != null)
                //    {
                //        TSeleccion.InnerHtml = this.Session["UserName"].ToString();
                //    }
                //    else
                //    {
                //        Response.Redirect("Login.aspx"); //Default
                //    }

                //    if (Session["UserAlias"] != null)
                //    {
                //        LbAlias.Text = this.Session["UserAlias"].ToString();
                //    }
                //    else
                //    {
                //        Response.Redirect("Login.aspx"); //Default
                //    }

                //    if (Session["UserDescripcion"] != null)
                //    {
                //        LbCargo.Text = this.Session["UserDescripcion"].ToString();
                //    }
                //    else
                //    {
                //        Response.Redirect("Login.aspx"); //Default
                //    }

                //    if (Session["UserIdentificacion"] != null)
                //    {
                //        lbindentificacion.Text = this.Session["UserIdentificacion"].ToString();
                //    }
                //    else
                //    {
                //        Response.Redirect("Login.aspx"); //Default
                //    }
                //}
                //else
                //{
                //    Response.Redirect("Login.aspx"); //Default
                //}

                //lbindentificacion.BackColor = System.Drawing.ColorTranslator.FromHtml("#9bd2ee");
                //LbCargo.BackColor = System.Drawing.ColorTranslator.FromHtml("#9bd2ee");
                //LbAlias.BackColor = System.Drawing.ColorTranslator.FromHtml("#9bd2ee");
                //LbDestino.BackColor = System.Drawing.ColorTranslator.FromHtml("#9bd2ee");
                if (this.Session["MiNivel"].ToString() != "9")
                {
                    Server.Transfer("Inicio.aspx"); //Default
                }


                if (!IsPostBack)
                {
                    this.Session["MiUEIDEstado"] = "0";
                    this.Session["TipoConexion"] = "0";

                    //TSeleccion.InnerHtml = this.Session["UserName"].ToString();
                    //LbAlias.Text = this.Session["UserAlias"].ToString();
                    //LbCargo.Text = this.Session["UserDescripcion"].ToString();
                    //lbindentificacion.Text = this.Session["UserIdentificacion"].ToString();

                    DataTable dt = new DataTable();
                    dt = Main.ArbolArchivos().Tables[0];
                    this.Session["Archivos"] = dt;

                    PopulateRootLevel();

                    //Dtipo.DataValueField = "ZID";
                    //Dtipo.DataTextField = "ZDescripcion";

                    //if (Session["Conexion"] != null)
                    //{
                    //    DataTable dtA = Session["Conexion"] as DataTable;
                    //    Dtipo.DataSource = dtA; 
                    //    Dtipo.DataBind();
                    //}
                    //else
                    //{
                    //    DataTable dtB = new DataTable();
                    //    dtB = Main.ArbolConexion().Tables[0];
                    //    this.Session["Conexion"] = dtB;
                    //    Dtipo.DataSource = dtB;
                    //    Dtipo.DataBind();
                    //}

                    //DTipoArchivo.DataValueField = "ZID";
                    //DTipoArchivo.DataTextField = "ZDescripcion";

                    //if (this.Session["TipoArchivo"] != null)
                    //{
                    //    DataTable dt1 = this.Session["TipoArchivo"] as DataTable;
                    //    DTipoArchivo.DataSource = dt1;
                    //    DTipoArchivo.DataBind();
                    //}
                    //else
                    //{
                    //    DataTable dt1 = new DataTable();
                    //    dt1 = Main.CargaJerarquia().Tables[0];
                    //    DTipoArchivo.DataSource = dt1;
                    //    DTipoArchivo.DataBind();
                    //    this.Session["TipoArchivo"] = dt1;
                    //}

                    //DrCampo.DataValueField = "ZID";
                    //DrCampo.DataTextField = "ZDescripcion";

                    //if (this.Session["FormatoCampo"] != null)
                    //{
                    //    DataTable dt1 = this.Session["FormatoCampo"] as DataTable;
                    //    DrCampo.DataSource = dt1;
                    //    DrCampo.DataBind();
                    //}
                    //else
                    //{
                    //    DataTable dt1 = new DataTable();
                    //    dt1 = Main.CargaFormatoCampos().Tables[0];
                    //    DrCampo.DataSource = dt1;
                    //    DrCampo.DataBind();
                    //    this.Session["FormatoCampo"] = dt1;
                    //}
                    ArrayTextBoxs = new TextBox[20];
                    ArrayCombos = new DropDownList[20];
                    ArrayLabels = new Label[20];
                    contadorControles = 0;

                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = Main.ArbolArchivos().Tables[0];
                    this.Session["Archivos"] = dt;

                    try
                    {
                        for (int i = 0; i < contadorControles; i++)
                            AgregarControles(ArrayTextBoxs[i], ArrayCombos[i], ArrayLabels[i]);
                    }
                    catch (Exception ex)
                    {
                        Variables.mensajeserver = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }

        private void btnAgregar_Click(string MiCampo)
        {
            try
            {
                int numeroRegistro = contadorControles;
                TextBox nuevoTxt = new TextBox();
                nuevoTxt.ID = "txt" + numeroRegistro.ToString();
                nuevoTxt.Width = 400;
                nuevoTxt.Text = "";
                ArrayTextBoxs[numeroRegistro] = nuevoTxt;

                Label nuevoLB = new Label();
                nuevoLB.ID = "LB" + numeroRegistro.ToString();
                nuevoLB.Width = 400;
                nuevoLB.Text = MiCampo;
                ArrayLabels[numeroRegistro] = nuevoLB;

                DropDownList nuevoCmb = new DropDownList();
                nuevoCmb.ID = "cmb" + numeroRegistro.ToString();
                nuevoCmb.Items.Add("Seleccione uno");
                nuevoCmb.SelectedIndex = 0;

                ArrayCombos[numeroRegistro] = nuevoCmb;
                AgregarControles(nuevoTxt, nuevoCmb, nuevoLB);
                contadorControles++;
            }
            catch (Exception ex)
            {
                Variables.mensajeserver = ex.Message;
            }
        }

        private void AgregarControles(TextBox txt, DropDownList cmb, Label lb)
        {
            try
            {
                pnlControles.Controls.Add(lb);
                pnlControles.Controls.Add(new LiteralControl("lb"));
                pnlControles.Controls.Add(txt);
                pnlControles.Controls.Add(new LiteralControl("tx"));
                pnlControles.Controls.Add(cmb);
                pnlControles.Controls.Add(new LiteralControl("cb"));
            }
            catch (Exception ex)
            {
                Variables.mensajeserver = ex.Message;
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
            PopulateNodoNuevo( treeUser.Nodes);
            PopulateNodes(dt, treeUser.Nodes);

        }

        private void PopulateNodoNuevo( TreeNodeCollection nodes)
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


        protected void treeUser_SelectedNodeChanged(object sender, EventArgs e)
        {
            if(treeUser.SelectedNode.Value == "-1")
            {
                IDDiv = 0;
                BuscoDiv_Click();
            }
            else
            {
                IDDiv = 1;
                BuscoDiv_Click();

                DataTable dt = this.Session["Archivos"] as DataTable;

                //int i = 1;
                int NumColumnas = gvControl.Columns.Count;  

                while (1 != NumColumnas)
                {
                    NumColumnas = gvControl.Columns.Count;
                    if(NumColumnas > 1)
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
                            DataTable dtA = new  DataTable();
                            dtA = Main.ConsultaMiTabla(fila["ZTABLENAME"].ToString()).Tables[0];
                            //gvControl.Columns.Clear();  //borra todos                      
                            foreach (DataRow fila2 in dtA.Rows)
                            {
                                BoundField columna = new BoundField();
                                columna.HeaderText = fila2["COLUMN_NAME"].ToString();
                                columna.DataField = fila2["COLUMN_NAME"].ToString();
                                gvControl.Columns.Add(columna);
                                btnAgregar_Click(fila2["COLUMN_NAME"].ToString());
                                //ArrayLabels[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayTextBoxs[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayCombos[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //AgregarControles(ArrayTextBoxs[contadorControles], ArrayCombos[contadorControles], ArrayLabels[contadorControles]);
                                //contadorControles += 1;
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

        
        protected void treeBases_SelectedNodeChanged(object sender, EventArgs e)
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

                //int i = 1;
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
                                btnAgregar_Click(fila2["COLUMN_NAME"].ToString());
                                //ArrayLabels[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayTextBoxs[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayCombos[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //AgregarControles(ArrayTextBoxs[contadorControles], ArrayCombos[contadorControles], ArrayLabels[contadorControles]);
                                //contadorControles += 1;
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

                //int i = 1;
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
                                btnAgregar_Click(fila2["COLUMN_NAME"].ToString());
                                //ArrayLabels[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayTextBoxs[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //ArrayCombos[contadorControles].Text = fila2["COLUMN_NAME"].ToString();
                                //AgregarControles(ArrayTextBoxs[contadorControles], ArrayCombos[contadorControles], ArrayLabels[contadorControles]);
                                //contadorControles += 1;
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


        public void CargarMenu(int cantidad, DataTable dt, int Id)
        {

            //string contenido = "<ul>";
            //contenido += "<li><a href='#' class='current'><span>Home</span></a></li>";

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (dr["ZID_ARCHIVO"].ToString() == Convert.ToString(Id))
            //    {
            //        contenido += ("<li><a href='#' class='current'><span>" + dr["ZNOMBRE"].ToString() + "</span></a></li>");
            //    }

            //}
            //contenido += "</ul>";

            //Stylefour.Controls.Add(new LiteralControl(contenido));
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

        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaDirectorio.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }

        protected void gvControl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvControl.EditIndex = -1;

            DataTable dt = this.Session["MiConsulta"] as DataTable;
            gvControl.DataSource = dt;
            gvControl.DataBind();
        }

        protected void gvControl_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gvControl.EditIndex = -1;

            DataTable dt = this.Session["MiConsulta"] as DataTable;
            gvControl.DataSource = dt;
            gvControl.DataBind();
        }

        protected void gvControl_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvControl.EditIndex = Convert.ToInt16(e.NewEditIndex);

            DataTable dt = this.Session["MiConsulta"] as DataTable;
            gvControl.DataSource = dt;
            gvControl.DataBind();
        }

        protected void gvIG_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void Flujo_Click(object sender, ImageClickEventArgs e)
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
                    MyDiv.Visible = true;
                    TablasFlujo.Visible = false;
                    IDDiv = 1;
                }
                else
                {
                    MyDiv.Visible = false;
                    TablasFlujo.Visible = true;
                    IDDiv = 0;
                }
            }
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

        //protected void EditarBaseDatos(object sender, EventArgs e)
        //{
        //    if (this.Session["TipoConexion"].ToString() == "0" || this.Session["TipoConexion"].ToString() == "3" || this.Session["TipoConexion"].ToString() == "4")
        //    {
        //        EditaBase.Text = "Base de Datos";
        //        this.Session["MiConexion"] = "";
        //        //messageBox.ShowMessage("Sin implementar");
        //        Variables.mensajeserver = "Sin implementar";
        //    }
        //    else
        //    {
        //        if (TxtServidor.Text == "")
        //        {
        //            //messageBox.ShowMessage("Introduzca IP o Hostname del servidor de Datos");
        //            Variables.mensajeserver = "Introduzca IP o Hostname del servidor de Datos";
        //        }
        //        else
        //        {
        //            if (TxtBaseDatos.Text == "")
        //            {
        //                //messageBox.ShowMessage("Introduzca nombre de Base de Datos");
        //                Variables.mensajeserver = "Introduzca nombre de Base de Datos";
        //            }
        //            else
        //            {
        //                if (TxtUser.Text == "")
        //                {
        //                    //messageBox.ShowMessage("Intruzca Usuario de Base de Datos");
        //                    Variables.mensajeserver = "Intruzca Usuario de Base de Datos";
        //                }
        //                else
        //                {
        //                    if (TxtPassword.Text == "")
        //                    {
        //                        //messageBox.ShowMessage("Introduzca password de usuario de Base de Datos");
        //                        Variables.mensajeserver = "Introduzca password de usuario de Base de Datos";
        //                    }
        //                    else
        //                    {
        //                        if (this.Session["TipoConexion"].ToString() == "1")
        //                        {
        //                            this.Session["MiConexion"] = "server="+ TxtServidor.Text + ";Password=" + TxtPassword.Text + ";Persist Security Info=True; User ID=" + TxtUser.Text + ";database=" + TxtBaseDatos.Text;
        //                            ConectaYTablas(this.Session["TipoConexion"].ToString(), this.Session["MiConexion"].ToString());
        //                        }
        //                        if (this.Session["TipoConexion"].ToString() == "2")
        //                        {
        //                            this.Session["MiConexion"] = "Data Source=" + TxtServidor.Text + ";Persist Security Info = True; User ID =" + TxtUser.Text + ";Password =" + TxtPassword.Text + ";"; // ;providerName = System.Data.OracleClient";
        //                            //this.Session["MiConexion"] = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + TxtServidor.Text + ")(PORT=1521))(CONNECT_DATA=(SID=" + TxtBaseDatos.Text  + "))); User Id =" + TxtUser.Text + "; Password=" + TxtPassword.Text + ";Connection Timeout=120;";
        //                            ConectaYTablas(this.Session["TipoConexion"].ToString(), this.Session["MiConexion"].ToString());
        //                        }
        //                        if (this.Session["TipoConexion"].ToString() == "5")
        //                        {
        //                            this.Session["MiConexion"] = "Driver ={ Microsoft Access Driver(*.mdb)}; DBQ =" + TxtServidor.Text  + ";";
        //                            ConectaYTablas(this.Session["TipoConexion"].ToString(), this.Session["MiConexion"].ToString());
        //                        }
        //                        //if (this.Session["TipoConexion"].ToString() == "4")
        //                        //{
        //                        //    this.Session["MiConexion"] = "";
        //                        //    messageBox.ShowMessage("Sin implementar");
        //                        //}
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected void ConectaYTablas(string TipoConexion, string Conexion)
        //{
        //    DataTable dt = new DataTable();
        //    int i = 0;

        //    if (TipoConexion == "1")
        //    {
        //        dt = Main.ConectaSQLServer(Conexion, TxtUser.Text).Tables[0];

        //        foreach (DataRow fila in dt.Rows)
        //        {
        //            ListTablas.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            //ListTablas2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            i += 1;
        //        }
        //    }
        //    if (TipoConexion == "2")
        //    {
        //        dt = Main.ConectaOracle(Conexion, TxtUser.Text).Tables[0];

        //        foreach (DataRow fila in dt.Rows)
        //        {
        //            ListTablas.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            //ListTablas2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            i += 1;
        //        }
        //    }

        //    if (TipoConexion == "5")
        //    {
        //        dt = Main.ConectaAccess(Conexion, TxtUser.Text).Tables[0];

        //        foreach (DataRow fila in dt.Rows)
        //        {
        //            ListTablas.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            //ListTablas2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            i += 1;
        //        }
        //    }
        //}

        //protected void EditaTabla(object sender, EventArgs e)
        //{
        //    if(ListTablas2.SelectedIndex != -1)
        //    {
        //        int index = Convert.ToInt32(ListTablas2.SelectedIndex);
        //        ListTablas2.Items[index].Text = TxtNombreTabla.Text;
        //    }
  
        //}

        //protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataTable dt = this.Session["Conexion"] as DataTable;
        //    foreach (DataRow fila in dt.Rows)
        //    {

        //        if (fila["ZID"].ToString() == Dtipo.SelectedItem.Value)
        //        {
        //            this.Session["TipoConexion"]= Dtipo.SelectedItem.Value;
        //            EditaBase.Text = "Conectar";
        //            break;

        //        }
        //    }
        //}

        protected void EditaCampo(object sender, EventArgs e)
        {
            //int index = Convert.ToInt32(ListCampos2.SelectedIndex);
            //ListCampos2.Items[index].Text = TxtNombreCampo.Text;
        }

        protected void DTipoArchivo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        protected void ListTablas_ondblclick(object sender, EventArgs e)
        {

        }

        protected void ListTablas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

        //private void ListTablas_Click(object sender, EventArgs e)
        //{
        //    ListBox lb = sender as ListBox;
        //    if (lb != null)
        //    {
        //        TxtNombreTabla.Text = Convert.ToString(lb.SelectedItem);
        //        IDTABLA = Convert.ToString(lb.SelectedValue);
        //    }
        //}

        protected void ListTablas_Click(object sender, EventArgs e)
        {
            //object o = ListTablas.SelectedItem;
            ////
            //if (o is LBItem)
            //{
            //    LBItem lbi;
            //    lbi = (LBItem)o;
            //    TxtNombreTabla.Text = lbi.Contenido;
            //    IDTABLA = lbi.Valor.ToString();
            //}
            //else
            //{
            //    TxtNombreTabla.Text = ListTablas.SelectedItem.ToString();
            //    IDTABLA = ListTablas.SelectedValue.ToString();
            //}
        }

        protected void ListTablas2_Click(object sender, EventArgs e)
        {
            //object o = ListTablas2.SelectedItem;
            ////
            //if (o is LBItem)
            //{
            //    LBItem lbi;
            //    lbi = (LBItem)o;
            //    TxtNombreTabla.Text = lbi.Contenido;
            //    IDTABLA = lbi.Valor.ToString();
            //}
            //else
            //{
            //    TxtNombreTabla.Text = ListTablas2.SelectedItem.ToString();
            //    IDTABLA = ListTablas2.SelectedValue.ToString();
            //}
        }

        protected void DformatoCampo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //protected void btnPasarSeleccionados_Click(object sender, EventArgs e)
        //{
        //    //Pasamos los items seleccionados de listbox1 a listbox2

        //    while (ListTablas.GetSelectedIndices().Length > 0)
        //    {
        //        ListTablas2.Items.Add(ListTablas.SelectedItem);
        //        TxtNombreTabla.Text = ListTablas.SelectedValue.ToString();
        //        ListTablas.Items.Remove(ListTablas.SelectedItem);
        //        int i = 0;
        //        DataTable dtA = new DataTable();

        //        if (this.Session["TipoConexion"].ToString() == "1")
        //        {
        //            dtA = Main.ConsultaMiTablaOracle(TxtNombreTabla.Text, this.Session["MiConexion"].ToString(), TxtUser.Text).Tables[0];
        //        }
        //        if (this.Session["TipoConexion"].ToString() == "2")
        //        {
        //            dtA = Main.ConsultaMiTablaSQL(TxtNombreTabla.Text, this.Session["MiConexion"].ToString(), TxtUser.Text).Tables[0];
        //        }
        //        if (this.Session["TipoConexion"].ToString() == "5")
        //        {
        //            dtA = Main.ConsultaMiTablaACCESS(TxtNombreTabla.Text, this.Session["MiConexion"].ToString(), TxtUser.Text);
        //        }
                
        //        ListCampos.Items.Clear();

        //        foreach (DataRow fila in dtA.Rows)
        //        {
        //            ListCampos.Items.Add(new ListasID(fila["ZCOLUMNA"].ToString(), i).ToString());
        //            //ListTablas2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), i).ToString());
        //            i += 1;
        //        }
        //        if (i > 0)
        //        {
        //            ImportaTabla.Visible = true;
        //        }
        //        else
        //        {
        //            ImportaTabla.Visible = false;
        //        }
        //    }
        //}

        //protected void btnRegresarSeleccionados_Click(object sender, EventArgs e)
        //{
        //    //Regresamos los items seleccionados de listbox2 a listbox1

        //    while (ListTablas2.GetSelectedIndices().Length > 0)
        //    {
        //        ListTablas.Items.Add(ListTablas2.SelectedItem);
        //        TxtNombreTabla.Text = ListTablas2.SelectedValue.ToString();
        //        ListTablas2.Items.Remove(ListTablas2.SelectedItem);
        //    }
        //}

        //protected void btnPasarSeleccionados2_Click(object sender, EventArgs e)
        //{
        //    //Pasamos los items seleccionados de listbox1 a listbox2

        //    while (ListCampos.GetSelectedIndices().Length > 0)
        //    {
        //        ListCampos2.Items.Add(ListCampos.SelectedItem);
        //        TxtNombreCampo.Text = ListCampos.SelectedValue.ToString();
        //        ListCampos.Items.Remove(ListCampos.SelectedItem);
        //    }
        //}

        //protected void btnRegresarSeleccionados2_Click(object sender, EventArgs e)
        //{
        //    //Regresamos los items seleccionados de listbox2 a listbox1

        //    while (ListCampos2.GetSelectedIndices().Length > 0)
        //    {
        //        ListCampos.Items.Add(ListCampos2.SelectedItem);
        //        TxtNombreCampo.Text = ListCampos2.SelectedValue.ToString();
        //        ListCampos2.Items.Remove(ListCampos2.SelectedItem);
        //    }
        //}

        protected void ImportarTabla(object sender, EventArgs e)
        {

        }

        protected void AbrirFile_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void VinculaTabla(object sender, EventArgs e)
        {

        }

        protected void TxtNombreTabla_TextChanged(object sender, EventArgs e)
        {

        }

        protected void VinculaCampo(object sender, EventArgs e)
        {

        }
    }
}