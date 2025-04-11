using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using Admin.Clases;
//using Admin.Classes;
//using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Teach;



namespace Satelite
{
    public partial class AltaArchivo : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;

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
                    Response.Redirect("Login.aspx"); //Default
                }

  
                if (!IsPostBack)
                {

                    //TSeleccion.InnerHtml = this.Session["UserName"].ToString();
                    //LbAlias.Text = this.Session["UserAlias"].ToString();
                    //LbCargo.Text = this.Session["UserDescripcion"].ToString();
                    //lbindentificacion.Text = this.Session["UserIdentificacion"].ToString();
                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Response.Redirect("Principal.aspx"); //Default
                    }


                    LbIDArchivo.InnerHtml = "Relaciones " ;
                    this.Session["IDArchivo"] = 1;

                    dlNivel.DataValueField = "ZID";
                    dlNivel.DataTextField = "ZDESCRIPCION";

                    if (this.Session["Niveles"] != null)
                    {
                        DataTable dt3 = this.Session["Niveles"] as DataTable;
                        dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                        dlNivel.DataBind();
                    }
                    else
                    {
                        DataTable dt3 = new DataTable();
                        dt3 = Main.CargaNivel().Tables[0];
                        this.Session["Niveles"] = dt3;
                        dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                        dlNivel.DataBind();
                    }


                    if (this.Session["Campos"] != null)
                    {
                        //ya esta cargado
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dt;
                    }

                    DrArchivos.DataValueField = "ZID";
                    DrArchivos.DataTextField = "ZDESCRIPCION";

                    Djerarquia.DataValueField = "ZID";
                    Djerarquia.DataTextField = "ZDESCRIPCION";

                    if (this.Session["Archivos"] != null)
                    {
                        DataTable dt = this.Session["Archivos"] as DataTable;
                        DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();

                        Djerarquia.DataSource = dt; 
                        Djerarquia.DataBind();
                    
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = Main.CargaArchivos().Tables[0];
                        this.Session["Archivos"] = dt;
                        DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();

                        Djerarquia.DataSource = dt;
                        Djerarquia.DataBind();
                    }
 

                    dlEstado.DataValueField = "ZID";
                    dlEstado.DataTextField = "ZDESCRIPCION";

                    if (this.Session["Estados"] != null)
                    {
                        DataTable dt4 = this.Session["Estados"] as DataTable;
                        dlEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        dlEstado.DataBind();
                    }
                    else
                    {
                        DataTable dt4 = new DataTable();
                        dt4 = Main.CargaEstados().Tables[0];
                        this.Session["Estados"] = dt4;
                        dlEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        dlEstado.DataBind();
                    }

                    Dtipo.DataValueField = "ZID";
                    Dtipo.DataTextField = "ZDESCRIPCION";

                    if (this.Session["TipoArchivos"] != null)
                    {
                        DataTable dt4 = this.Session["TipoArchivos"] as DataTable;
                        Dtipo.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        Dtipo.DataBind();
                    }
                    else
                    {
                        DataTable dt4 = new DataTable();
                        dt4 = Main.CargaJerarquia().Tables[0];
                        this.Session["TipoArchivos"] = dt4;
                        Dtipo.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        Dtipo.DataBind();
                    }
                    Relaciones(1);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("thEnd.aspx");
            }
        }

        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;

            DataTable dt = this.Session["Archivos"] as DataTable;
            foreach (DataRow fila in dt.Rows)
            {
                
                if (fila["ZID"].ToString() == DrArchivos.SelectedItem.Value)
                {
                    LbIDArchivo.InnerHtml = "Relaciones con Id Archivo "  + DrArchivos.SelectedItem.Value;
                    TxtNombre.Text = fila["ZTABLENAME"].ToString();
                    dlNivel.SelectedIndex = Convert.ToInt32(fila["ZNIVEL"].ToString());
                    Djerarquia.SelectedIndex = Convert.ToInt32(fila["ZROOT"].ToString());
                    Dtipo.SelectedIndex = Convert.ToInt32(fila["ZTIPO"].ToString());
                    this.Session["IDArchivo"] = Convert.ToInt32(fila["ZID"].ToString());
                    Relaciones(Convert.ToInt32(fila["ZID"].ToString()));
                    TxtDescripcion.Text = fila["ZDESCRIPCION"].ToString();
                    TextConexion.Text = fila["ZCONEXION"].ToString();
                    TablaObj.Text = fila["ZTABLEOBJ"].ToString();
                    //tvControl.Nodes.Clear();
                    registro = Convert.ToInt32(fila["ZID"].ToString());
                    Busca_Root(Convert.ToInt32(Convert.ToInt32(fila["ZID"].ToString())));
                    Contador = 0;
                    PopulateRootLevel(Buscaregistro);
                    //TxtMail.Text = fila["ZEMAIL"].ToString();
                    break;
                    
                }

                //string valor = fila["NombreDeLaColumna"].ToString();//por indice campo string valor = fila[0].ToString();

            }
        }

        protected void Busca_Root(int ID)
        {
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                int N = -1;
                string A = Convert.ToString(ID);
                int Salida = ID;

                while (N != 0)
                {
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int MiID = 0;
            //string Ver = "";

            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 0)
            {

            }
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 1)
            {
                if (Variables.configuracionDB == 0)
                {
                    //MiID =  Convert.ToInt32(cmd.ExecuteScalar());
                    SqlParameter[] dbParams = new SqlParameter[0];

                    //maximo id
                    MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM USER_GEDESPOL.ZARCHIVOS", dbParams));
                }
                else
                {
                    //MiID =  Convert.ToInt32(cmd.ExecuteScalar());
                    //OracleParameter[] dbParams = new OracleParameter[0];

                    ////maximo id
                    //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZARCHIVOS", dbParams));
                }


                MiID = MiID + 1;

                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = "USER_GEDESPOL.ZARCHIVOS"; // this.Session["Tabla"].ToString();

                Column = "INSERT INTO " + Tabla + " (ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO) ";
                ColumnVal = " VALUES(" + MiID + ",'" + TxtNombre.Text + "','" + TxtDescripcion.Text + "'," + dlNivel.SelectedValue + "," + Djerarquia.SelectedValue + ",'" + TablaName.Text + "','" + TablaObj.Text + "'," + Dtipo.SelectedValue + "," + dlEstado.SelectedValue + ")";
                
                Column += ColumnVal;
                this.Session["IDArchivo"] = MiID;

                //const string fic = @"C:\Proyecto\Administracion\Admin\Public\output.txt";
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
                //sw.WriteLine(Column);
                //sw.Close();

                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                    Tabla = "ZARCHIVOCAMPO"; // this.Session["Tabla"].ToString();
                    Column = "DELETE FROM  " + Tabla + " ";
                    ColumnVal = " WHERE ZID_ARCHIVO = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    Column += ColumnVal;

                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);

                    //Tabla = "USER_GEDESPOL.ZARCHIVOCAMPO"; // this.Session["Tabla"].ToString();
                    //Column = "DELETE FROM  " + Tabla + " ";
                    //ColumnVal = " WHERE ZID_ARCHIVO = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    //Column += ColumnVal;

                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                }


                

                DataTable dtA = this.Session["Campo"] as DataTable;

                int J = 1;


                foreach (var items in ListBox2.Items)
                {
                    foreach (DataRow fila in dtA.Rows)
                    {
                        //Ver = items.ToString();
                        //Ver = fila["ZNOMBRE"].ToString();
                        
                        if (items.ToString() == fila["ZDESCRIPCION"].ToString())
                        {
                            if (Variables.configuracionDB == 0)
                            {
                                SqlParameter[] dbParams = new SqlParameter[0];
                                Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                                ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                                Column += ColumnVal;
                                DBHelper.ExecuteNonQuerySQL(Column, dbParams);

                            }
                            else if (Variables.configuracionDB == 1)
                            {
                                //OracleParameter[] dbParams = new OracleParameter[0];
                                //Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                                //ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                                //Column += ColumnVal;
                                //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                            }
                            
                            J += 1;
                            break;
                        }

                    }

                }

                //messageBox.ShowMessage("El Registro se inserto correctamente!");
                this.Session["Edicion"] = 0;
            }

            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 2)
            {

                

                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = "";
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    Tabla = "ZARCHIVOS"; // this.Session["Tabla"].ToString();
                    Column = "UPDATE " + Tabla + " SET  ZNOMBRE ='" + TxtNombre.Text + "', ZDESCRIPCION ='" + TxtDescripcion.Text + "', ZNIVEL = " + dlNivel.SelectedValue + ", ZROOT ='" + Djerarquia.SelectedValue + "',";
                    Column += "ZTABLENAME ='" + TablaName.Text + "', ZTABLEOBJ = '" + TablaObj.Text + "', ZTIPO = " + Dtipo.SelectedValue + ", ZESTADO =" + dlEstado.SelectedValue + "";
                    Column += " WHERE ZID = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                    Tabla = "USER_GEDESPOL.ZARCHIVOCAMPO"; // this.Session["Tabla"].ToString();
                    Column = "DELETE FROM  " + Tabla + " ";
                    ColumnVal = " WHERE ZID_ARCHIVO = " + Convert.ToInt32(this.Session["IDArchivo"].ToString());
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
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

                DataTable dtA = this.Session["Campo"] as DataTable;

                int J = 1;
                

                foreach (var items in ListBox2.Items)
                {
                    foreach (DataRow fila in dtA.Rows)
                    {
                        //ver = items.ToString();
                        //Ver = fila["ZNOMBRE"].ToString();

                        if (items.ToString() == fila["ZDESCRIPCION"].ToString())
                        {
                            if (Variables.configuracionDB == 0)
                            {
                                SqlParameter[] dbParams = new SqlParameter[0];
                                Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                                ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                                Column += ColumnVal;
                                DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                            }
                            else if (Variables.configuracionDB == 1)
                            {
                                //OracleParameter[] dbParams = new OracleParameter[0];
                                //Column = "INSERT INTO " + Tabla + " (ZID_ARCHIVO, ZID_CAMPO, ZORDEN, ZACTIVO) VALUES (";
                                //ColumnVal = this.Session["IDArchivo"] + "," + fila["ZID"].ToString() + "," + J + ",1)";
                                //Column += ColumnVal;
                                //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                            }
                            J += 1;
                            break;
                        }

                    }

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
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }

            }
            DataTable dt = new DataTable();
            dt = Main.CargaArchivos().Tables[0];
            this.Session["Archivo"] = dt;
            DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
            DrArchivos.DataBind();

            Djerarquia.DataSource = dt;
            Djerarquia.DataBind();

            DesactivarTxt();

            btnEditar.CssClass = "myButtonOn";
            btnNuevo.CssClass = "myButtonOn";
            btnGuardar.CssClass = "myButtonOn";
            btnCancelar.CssClass = "myButtonOn";
        }

        public void Limpiar()
        {
            int MiID = 0;
            //MiID =  Convert.ToInt32(cmd.ExecuteScalar());
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                //maximo id
                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM USER_GEDESPOL.ZARCHIVOS", dbParams));
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
            //dlNivel.Text = "";
            //TxtMail.Text = "";
            this.Session["Edicion"] = 0;
        }

        public void Relaciones(int ID)
        {
            int MiID = ID;
            ListBox1.Items.Clear();
            ListBox2.Items.Clear();

            DrCampoasig.Items.Clear();
            DrCampoasig.DataValueField = "ZID";
            DrCampoasig.DataTextField = "ZDESCRIPCION";

            //DrCompetencia.Items.Insert(0, ""); //Primero vacio

            DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dt1 = new DataTable();
            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];
            this.Session["ArchivoCampos"] = dt1;
            int visto = 0;

            DataTable dtt;
            dtt = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZDESCRIPCION");

            DataRow drr;

            //const string fic = @"C:\Proyecto\Administracion\Admin\Public\output.txt";
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
            int i = 0;

            foreach (DataRow fila in dt.Rows)
            {
                visto = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    if (fila["ZID"].ToString()== dr["ZID_CAMPO"].ToString())
                    {
                        drr = dtt.NewRow();
                        drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                        drr[1] = fila["ZDESCRIPCION"].ToString();
                        dtt.Rows.Add(drr);
                        //MiID = Convert.ToInt32(fila["ZID"].ToString());
                        //sw.WriteLine("VALOR " + MiID + ", Descripcion " + fila["ZDESCRIPCION"].ToString());

                        //DrCompetencia.Items.Insert(MiID, fila["ZDESCRIPCION"].ToString());
                        ListBox2.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        visto = 1;
                        i += 1;
                        break;
                    }
                }

                if (visto == 0)
                {
                    ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
            //sw.Close();
            if (i != 0)
            {
                DrCampoasig.DataSource = dtt;
                DrCampoasig.DataBind();
                this.Session["SelArchivoCampo"] = dtt;
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
            DrCampoasig.Text = "";
            TxtDescripcion.Text = "";
            TxtNombre.Text = "";
            TablaName.Text = "";
            TablaObj.Text = "";
            //dlNivel.Text = "";
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
            Relaciones(0);
            ActivarTxt();
            btnEditar.CssClass = "myButtonOver";
            btnNuevo.CssClass = "myButtonOver";
            btnGuardar.CssClass = "myButtonOff";
            btnCancelar.CssClass = "myButtonOff";
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
            btnEditar.CssClass = "myButtonOver";
            btnNuevo.CssClass = "myButtonOver";
            btnGuardar.CssClass = "myButtonOff";
            btnCancelar.CssClass = "myButtonOff";
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            //Eliminamos todos los registros del listbox
            ListBox1.Items.Clear();
        }

        protected void btnEliminarSeleccionados_Click(object sender, EventArgs e)
        {
            //Eliminamos los registros seleccionados

            while (ListBox1.GetSelectedIndices().Length > 0)
            {
                ListBox1.Items.Remove(ListBox1.SelectedItem);
            }
        }

        protected void btnPasarSeleccionados_Click(object sender, EventArgs e)
        {
            //Pasamos los items seleccionados de listbox1 a listbox2

            while (ListBox1.GetSelectedIndices().Length > 0)
            {
                ListBox2.Items.Add(ListBox1.SelectedItem);
                ListBox1.Items.Remove(ListBox1.SelectedItem);
            }
        }

        protected void btnRegresarSeleccionados_Click(object sender, EventArgs e)
        {
            //Regresamos los items seleccionados de listbox2 a listbox1

            while (ListBox2.GetSelectedIndices().Length > 0)
            {
                ListBox1.Items.Add(ListBox2.SelectedItem);
                ListBox2.Items.Remove(ListBox2.SelectedItem);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en listbox1

            if (ListBox1.Items.FindByText(TextBox1.Text) != null)
            {
                ListBox1.Items.FindByText(TextBox1.Text).Selected = true;
            }
        }

        public void DesactivarTxt()
        {

            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            dlEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TablaName.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TablaObj.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Djerarquia.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Dtipo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");

        }

        public void ActivarTxt()
        {
            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            dlEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            //DrCampoasig.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox1.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            ListBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TablaName.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TablaObj.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Djerarquia.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Dtipo.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
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

                if(ListadoArchivos.GetUpperBound(0) == Contador)
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

            if (J==0)
            {

            }
            else
            {
                int index = ListBox2.SelectedIndex;
                sTmp = ListBox2.Items[index - 1].Text;
                ListBox2.Items[index - 1].Text = ListBox2.Items[index].Text;
                ListBox2.Items[index].Text = sTmp;
                if (index > 1)
                {
                    ListBox2.SelectedIndex = index - 1;
                }
                
            }

        }

        protected void Bajar_Click(object sender, EventArgs e)
        {
            int J = ListBox2.Rows - 1;
            string sTmp = "";

            if (J == 0)
            {

            }
            else
            {
                int index = ListBox2.SelectedIndex;
                if (index < ListBox2.Items.Count)
                {
                    sTmp = ListBox2.Items[index + 1].Text;
                    ListBox2.Items[index + 1].Text = ListBox2.Items[index].Text;
                    ListBox2.Items[index].Text = sTmp;
                    ListBox2.SelectedIndex = index + 1;
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

            btnEditar.CssClass = "myButtonOn";
            btnNuevo.CssClass = "myButtonOn";
            btnGuardar.CssClass = "myButtonOn";
            btnCancelar.CssClass = "myButtonOn";
        }
        protected void Fin_Click(object sender, ImageClickEventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx"); //Default
        }

        protected void FinHome_Click(object sender, ImageClickEventArgs e)
        {
            //Session.Abandon();
            Response.Redirect("Site.aspx"); //Default
        }

        protected void DrCampoasig_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Djerarquia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Response.Redirect("CambioLogin.aspx"); //Default
        }
    }
}