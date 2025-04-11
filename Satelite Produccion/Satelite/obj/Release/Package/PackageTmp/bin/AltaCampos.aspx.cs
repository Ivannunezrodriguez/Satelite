using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace Satelite
{
    public partial class AltaCampos : System.Web.UI.Page
    {
        //private int registros = 0;
        //private int Competencia = 0;
        //private string VAR = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Inicio.aspx"); //Default

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
                    TextBox1.Attributes.Add("placeholder", "Buscar...");
                    LbID.Text = "CAMP-01";

                    DFormato.DataValueField = "ZID";
                    DFormato.DataTextField = "ZDescripcion";

                    if (this.Session["FormatoCampo"] != null)
                    {
                        DataTable dt1 = this.Session["FormatoCampo"] as DataTable;
                        DFormato.DataSource = dt1;
                        DFormato.DataBind();
                        DFormato.SelectedIndex = -1;
                    }
                    else
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Main.CargaFormatoCampos().Tables[0];
                        DFormato.DataSource = dt1;
                        DFormato.DataBind();
                        this.Session["FormatoCampo"] = dt1;
                        DFormato.SelectedIndex = -1;
                    }

                    DrDenominacion.DataValueField = "ZID";
                    DrDenominacion.DataTextField = "ZDESCRIPCION";
                    //DrDenominacion.Items.Insert(0, new ListItem("0", "Ninguno"));

                    if (this.Session["Cargo"] != null)
                    {
                        DataTable dt1 = this.Session["Cargo"] as DataTable;
                        DrDenominacion.DataSource = dt1;
                        DrDenominacion.DataBind();
                        DrDenominacion.SelectedIndex = -1;
                    }
                    else
                    {
                        Actualiza_campos();
                    }


                    dlNivel.DataValueField = "ZID";
                    dlNivel.DataTextField = "ZDescripcion";

                    if (this.Session["Niveles"] != null)
                    {
                        DataTable dt3 = this.Session["Niveles"] as DataTable;
                        dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                        dlNivel.DataBind();
                        dlNivel.SelectedIndex = -1;
                    }
                    else
                    {
                        DataTable dt3 = new DataTable();
                        dt3 = Main.CargaNivel().Tables[0];
                        this.Session["Niveles"] = dt3;
                        dlNivel.DataSource = dt3; // EvaluacionSel.GargaQuery().Tables[0];
                        dlNivel.DataBind();
                        dlNivel.SelectedIndex = -1;
                    }

                    DTvalidacion.DataValueField = "ZID";
                    DTvalidacion.DataTextField = "ZDescripcion";
                    DTvalidacion.Items.Insert(0, new ListItem("Ninguno", "0"));


                    if (this.Session["TablasValidacion"] != null)
                    {
                        DataTable dt4 = this.Session["TablasValidacion"] as DataTable;
                        DTvalidacion.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        DTvalidacion.DataBind();
                        DTvalidacion.SelectedIndex = -1;
                    }
                    else
                    {
                        DataTable dt4 = new DataTable();
                        dt4 = Main.TablasValidacion().Tables[0];
                        this.Session["TablasValidacion"] = dt4;
                        DTvalidacion.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        DTvalidacion.DataBind();
                        DTvalidacion.SelectedIndex = -1;
                    }

                    DEstado.DataValueField = "ZID";
                    DEstado.DataTextField = "ZDescripcion";

                    if (this.Session["Estados"] != null)
                    {
                        DataTable dt4 = this.Session["Estados"] as DataTable;
                        DEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        DEstado.DataBind();
                        DEstado.SelectedIndex = -1;
                    }
                    else
                    {
                        DataTable dt4 = new DataTable();
                        dt4 = Main.CargaEstados().Tables[0];
                        this.Session["Estados"] = dt4;
                        DEstado.DataSource = dt4; // EvaluacionSel.GargaQuery().Tables[0];
                        DEstado.DataBind();

                        DEstado.SelectedIndex = -1;
                    }

                    Limpiar();
                }

            }
            catch (Exception ex)
            {
                string a = ex.Message;
                Server.Transfer("thEnd.aspx");
            }
        }

        private void Actualiza_campos()
        {
            int i = 0;
            DataTable dt = new DataTable();
            dt = Main.CargaCampos().Tables[0];
            DrDenominacion.Items.Clear();

            DrDenominacion.DataValueField = "ZID";
            DrDenominacion.DataTextField = "ZDESCRIPCION";

            DrDenominacion.DataSource = dt;
            DrDenominacion.DataBind();

            this.Session["Campos"] = dt;
            //DrDenominacion.SelectedIndex = -1;
            ListBox1.Items.Clear();
            ListBox1Col.Items.Clear();
            ListBox1ID.Items.Clear();
            

            foreach (DataRow fila in dt.Rows)//Campos
            {
                i += 1;
                ListBox1.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                ListBox1ID.Items.Add(new ListasID(fila["ZID"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                ListBox1Col.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                //DrDenominacion.Items.Add(new ListasID(fila["ZTITULO"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
            }
            LbCampoCount.Text = i.ToString();
        }

        private void Limpia_Seleccion()
        {
            for (int i = 0; i < DrDenominacion.Items.Count - 1; i++)
            {
                DrDenominacion.Items[i].Selected = false;
                ListBox1.Items[i].Selected = false;
                ListBox1ID.Items[i].Selected = false;
                ListBox1Col.Items[i].Selected = false;
            }
        }

        protected void btnLimpia_Click(object sender, EventArgs e)
        {
            Limpia_Seleccion();
            TextBox1.Text = "";
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            //Buscamos un item en los listbox
            Boolean Esta = false;
            
            string desde = ListBox1.SelectedIndex.ToString();

            if (desde != "-1")
            {
                for (int i = Convert.ToInt32(desde) +1; i < ListBox1.Items.Count - 1; i++)
                {
                    if (ListBox1.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                    {
                        Limpia_Seleccion();
                        string a = ListBox1.Items[i].Text;
                        a = ListBox1.Items[i].Value;
                        ListBox1.Items[i].Selected = true;
                        ListBox1ID.Items[i].Selected = true;
                        ListBox1Col.Items[i].Selected = true;
                        DrDenominacion.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < ListBox1.Items.Count - 1; i++)
                {
                    if (ListBox1.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                    {
                        string a = ListBox1.Items[i].Text;
                        a = ListBox1.Items[i].Value;
                        ListBox1.Items[i].Selected = true;
                        ListBox1ID.Items[i].Selected = true;
                        ListBox1Col.Items[i].Selected = true;
                        DrDenominacion.Items[i].Selected = true;
                        Esta = true;
                        break;
                    }
                }
            }
            if (Esta == false)
            {
                desde = ListBox1ID.SelectedIndex.ToString();
                if (desde != "-1")
                {
                    for (int i = Convert.ToInt32(desde)+1; i < ListBox1ID.Items.Count - 1; i++)
                    {
                        if (ListBox1ID.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                        {
                            Limpia_Seleccion();
                            string a = ListBox1ID.Items[i].Text;
                            a = ListBox1ID.Items[i].Value;
                            ListBox1.Items[i].Selected = true;
                            ListBox1ID.Items[i].Selected = true;
                            ListBox1Col.Items[i].Selected = true;
                            DrDenominacion.Items[i].Selected = true;
                            Esta = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ListBox1ID.Items.Count - 1; i++)
                    {
                        if (ListBox1ID.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                        {
                            string a = ListBox1ID.Items[i].Text;
                            a = ListBox1ID.Items[i].Value;
                            ListBox1.Items[i].Selected = true;
                            ListBox1ID.Items[i].Selected = true;
                            ListBox1Col.Items[i].Selected = true;
                            DrDenominacion.Items[i].Selected = true;
                            Esta = true;
                            break;
                        }
                    }
                }
            }
            if (Esta == false)
            {
                desde = ListBox1Col.SelectedIndex.ToString();
                if (desde != "-1")
                {
                    for (int i = Convert.ToInt32(desde)+1; i < ListBox1Col.Items.Count - 1; i++)
                    {
                        if (ListBox1Col.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                        {
                            Limpia_Seleccion();
                            string a = ListBox1Col.Items[i].Text;
                            a = ListBox1Col.Items[i].Value;
                            ListBox1.Items[i].Selected = true;
                            ListBox1ID.Items[i].Selected = true;
                            ListBox1Col.Items[i].Selected = true;
                            DrDenominacion.Items[i].Selected = true;
                            Esta = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ListBox1Col.Items.Count - 1; i++)
                    {
                        if (ListBox1Col.Items[i].Text.ToLower() == TextBox1.Text.ToLower())
                        {
                            string a = ListBox1Col.Items[i].Text;
                            a = ListBox1Col.Items[i].Value;
                            ListBox1.Items[i].Selected = true;
                            ListBox1ID.Items[i].Selected = true;
                            ListBox1Col.Items[i].Selected = true;
                            DrDenominacion.Items[i].Selected = true;
                            Esta = true;
                            break;
                        }
                    }
                }
            }
            if(Esta == true)
            {
                //DrDenominacion_SelectedIndexChanged(sender,e);
                DataTable dt = Main.CargaCampos().Tables[0];
                foreach (DataRow fila in dt.Rows)
                {

                    if (fila["ZID"].ToString() == ListBox1ID.SelectedItem.Value)
                    {
                        DrDenominacion.SelectedIndex = DrDenominacion.Items.IndexOf(DrDenominacion.Items.FindByValue(fila["ZID"].ToString()));

                        TxtCampoName.Text = fila["ZID"].ToString();
                        Txttitulo.Text = fila["ZDESCRIPCION"].ToString();
                        txtNombre.Text = fila["ZTITULO"].ToString();


                        if (fila["ZTIPO"].ToString() != "")
                        {
                            DFormato.SelectedIndex = DFormato.Items.IndexOf(DFormato.Items.FindByValue(fila["ZTIPO"].ToString()));
                        }

                        txtCapacidad.Text = fila["ZVALOR"].ToString();

                        if (fila["ZVALIDACION"].ToString() != "0")
                        {
                            DTvalidacion.SelectedIndex = DTvalidacion.Items.IndexOf(DTvalidacion.Items.FindByValue(fila["ZVALIDACION"].ToString()));
                        }

                        if (fila["ZNIVEL"].ToString() != "")
                        {
                            dlNivel.SelectedIndex = dlNivel.Items.IndexOf(dlNivel.Items.FindByValue(fila["ZNIVEL"].ToString()));
                        }

                        if (fila["ZESTADO"].ToString() != "")
                        {
                            DEstado.SelectedIndex = DEstado.Items.IndexOf(DEstado.Items.FindByValue(fila["ZESTADO"].ToString()));
                        }
                        TxtFecha.Text = fila["ZFECHA"].ToString();
                        LbID.Text = "Identificador de Campo: (" + Convert.ToInt32(fila["ZID"].ToString()) + ")";
                        this.Session["IDCampo"] = Convert.ToInt32(fila["ZID"].ToString());
                        break;
                    }
                }
            }
        }

        protected void DrDenominacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            //int index = 0;
            TextBox1.Text = "";
            DataTable dt = Main.CargaCampos().Tables[0];
            foreach (DataRow fila in dt.Rows)
            {

                if (fila["ZID"].ToString() == DrDenominacion.SelectedItem.Value)
                {
                    for (int i = 0; i < ListBox1ID.Items.Count - 1; i++)
                    {
                        if (ListBox1ID.Items[i].Value == fila["ZID"].ToString())
                        {
                            Limpia_Seleccion();
                            string a = ListBox1.Items[i].Text;
                            a = ListBox1.Items[i].Value;
                            ListBox1.Items[i].Selected = true;
                            ListBox1ID.Items[i].Selected = true;
                            ListBox1Col.Items[i].Selected = true;
                            //DrDenominacion.Items[i].Selected = true;
                           //DrDenominacion.Text = a;
                            break;
                        }
                    }
                    //for (int i = 0; i < DrDenominacion.Items.Count - 1; i++)
                    //{
                    //    if (DrDenominacion.Items[i].Value == fila["ZID"].ToString())
                    //    {
                    //        DrDenominacion.Text = DrDenominacion.Items[i].Text;
                    //        break;
                    //    }
                    //}
                    //DrDenominacion.Text = DrDenominacion.SelectedItem.Text;
                    DrDenominacion.SelectedIndex = DrDenominacion.Items.IndexOf(DrDenominacion.Items.FindByValue(fila["ZID"].ToString()));

                    TxtCampoName.Text = fila["ZID"].ToString();
                    Txttitulo.Text = fila["ZDESCRIPCION"].ToString();
                    //txtNombre.Text = fila["ZVALIDACION"].ToString();
                    //txtNombre.Text = fila["ZTIPO"].ToString();
                    txtNombre.Text = fila["ZTITULO"].ToString();


                    if (fila["ZTIPO"].ToString() != "")
                    {
                        //DFormato.SelectedIndex = Convert.ToInt32(fila["ZFORMATO"].ToString());
                        DFormato.SelectedIndex = DFormato.Items.IndexOf(DFormato.Items.FindByValue(fila["ZTIPO"].ToString()));
                    }

                    txtCapacidad.Text = fila["ZVALOR"].ToString();

                    if (fila["ZVALIDACION"].ToString() != "0")
                    {
                        //DTvalidacion.SelectedIndex = Convert.ToInt32(fila["ZTVALIDACION"].ToString());
                        DTvalidacion.SelectedIndex = DTvalidacion.Items.IndexOf(DTvalidacion.Items.FindByValue(fila["ZVALIDACION"].ToString()));
                    }

                    if (fila["ZNIVEL"].ToString() != "")
                    {
                        //dlNivel.SelectedIndex = Convert.ToInt32(fila["ZNIVEL"].ToString());
                        dlNivel.SelectedIndex = dlNivel.Items.IndexOf(dlNivel.Items.FindByValue(fila["ZNIVEL"].ToString()));
                    }

                    if (fila["ZESTADO"].ToString() != "")
                    {
                        //DEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());
                        DEstado.SelectedIndex = DEstado.Items.IndexOf(DEstado.Items.FindByValue(fila["ZESTADO"].ToString()));
                    }
                    //if (fila["ZFECHA"].ToString() != "")
                    //{
                        //DEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());
                        TxtFecha.Text = fila["ZFECHA"].ToString();
                    //}

                    

                    LbID.Text = "Identificador de Campo: (" + Convert.ToInt32(fila["ZID"].ToString()) + ")";
                    this.Session["IDCampo"] = Convert.ToInt32(fila["ZID"].ToString());
                    break;
                }

                //string valor = fila["NombreDeLaColumna"].ToString();//por indice campo string valor = fila[0].ToString();

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //DateTime fecha;
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 0)
            {

            }
            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 1)
            {
                int MiID = 0;

                //MiID =  Convert.ToInt32(cmd.ExecuteScalar());

                //maximo id
                string Tabla = "";
                if (Variables.configuracionDB == 0)
                {
                    Tabla = "ZCAMPOS";
                    SqlParameter[] dbParams = new SqlParameter[0];
                    MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams));
                }
                else if (Variables.configuracionDB == 1)
                {
                    //Tabla = "USER_GEDESPOL.ZCAMPOS";
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZCAMPOS", dbParams));
                }
                MiID = MiID + 1;

                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                //string Tabla = "USER_GEDESPOL.ZCAMPOS"; // this.Session["Tabla"].ToString();


                Column = "INSERT INTO " + Tabla + " ( ZTITULO, ZDESCRIPCION, ZTIPO, ZVALOR, ZVALIDACION, ZNIVEL, ZESTADO, ZACTIVO, ZFECHA) VALUES (";

                ColumnVal = "'" + txtNombre.Text.Replace(" ", "_") + "','" + Txttitulo.Text + "','" + DFormato.SelectedValue + "','" + txtCapacidad.Text + "',";

                if (DTvalidacion.SelectedValue == "" || DTvalidacion.SelectedItem.Value == "Ninguno")
                {
                    ColumnVal += " 0,";
                }
                else
                {
                    ColumnVal += DTvalidacion.SelectedValue + ",";
                }

                ColumnVal += dlNivel.SelectedValue + "," + DEstado.SelectedValue + "," + DEstado.SelectedValue  + ",'" + DateTime.Now.ToString("dd/MM/yyyy") + "')";

                Column += ColumnVal;
                this.Session["IDCampo"] = MiID;
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);
                }

                //messageBox.ShowMessage("El Registro se inserto correctamente");
                this.Session["Edicion"] = 0;
            }

            if (Convert.ToInt32(this.Session["Edicion"].ToString()) == 2)
            {

                string Column = "";
                string ColumnVal = "(";
                int Tipo = Convert.ToInt32(this.Session["TipoConexion"]);
                string Tabla = ""; // this.Session["Tabla"].ToString();
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    Tabla = "ZCAMPOS";
                    Column = "UPDATE " + Tabla + " SET ZTITULO ='" + txtNombre.Text.Replace(" ", "_") + "',  ZDESCRIPCION ='" + Txttitulo.Text + "', ";
                    Column += " ZTIPO = " + DFormato.SelectedValue + ",";
                    Column += " ZVALOR = '" + txtCapacidad.Text + "', ";
                    if (DTvalidacion.SelectedValue == "" || DTvalidacion.SelectedItem.Value == "Ninguno")
                    {
                        Column += " ZVALIDACION = 0 , ";
                    }
                    else
                    {
                        Column += " ZVALIDACION = " + DTvalidacion.SelectedValue + ", ";
                    }
                    Column += " ZNIVEL = " + dlNivel.SelectedValue + ", ";
                    Column += " ZESTADO = " + DEstado.SelectedValue + ", ";
                    Column += " ZACTIVO = " + DEstado.SelectedValue + " ";
                    ColumnVal = " WHERE ZID = " + Convert.ToInt32(this.Session["IDCampo"].ToString());
                    Column += ColumnVal;
                    DBHelper.ExecuteNonQuerySQL(Column, dbParams);
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //Tabla = "USER_GEDESPOL.ZCAMPOS";
                    //Column = "UPDATE " + Tabla + " SET ZNOMBRE ='" + txtNombre.Text.Replace(" ", "_") + "',  ZDESCRIPCION ='" + Txttitulo.Text + "', ZFORMATO =" + DFormato.SelectedValue + ",";
                    //Column += " ZCAPACIDAD ='" + txtCapacidad.Text + "',ZTVALIDACION =" + DTvalidacion.SelectedValue + ", ZNIVEL =" + dlNivel.SelectedValue + ", ZESTADO = " + DEstado.SelectedValue + " ZACTIVO =" + DEstado.SelectedValue + " ";
                    //ColumnVal = " WHERE ZID = " + Convert.ToInt32(this.Session["IDCampo"].ToString());
                    //Column += ColumnVal;
                    //DBHelper.ExecuteNonQueryOra(Column, dbParams);
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
                    //MiTxt.ReadOnly = false;
                }
            }
            DesactivarTxt();
            //btnEditar.Enabled = true;
            //btnNuevo.Enabled = true;
            //btnEditar.Attributes.Add("onclick", "this.disabled=false;");
            //btnNuevo.Attributes.Add("onclick", "this.disabled=false;");
            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";

            btnNuevo.Visible = true;
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;

            Actualiza_campos();

            //DataTable dt1 = new DataTable();
            //dt1 = Main.CargaCampos().Tables[0];

            //DrDenominacion.Items.Clear();

            //DrDenominacion.DataValueField = "ZID";
            //DrDenominacion.DataTextField = "ZDescripcion";
            //DrDenominacion.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));

            //DrDenominacion.DataSource = dt1;
            //DrDenominacion.DataBind();
            //this.Session["Campos"] = dt1;
        }

        protected void ListBox1ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1ID.SelectedIndex;
            ListBox1Col.SelectedIndex = ListBox1ID.SelectedIndex;
            ListBox1.SelectedIndex = ListBox1ID.SelectedIndex;
            DrDenominacion.SelectedIndex = ListBox1ID.SelectedIndex;
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
            DrDenominacion_SelectedIndexChanged(DrDenominacion, e);
        }

        protected void ListBox1Col_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1Col.SelectedIndex;
            ListBox1ID.SelectedIndex = ListBox1Col.SelectedIndex;
            ListBox1.SelectedIndex = ListBox1Col.SelectedIndex;
            DrDenominacion.SelectedIndex = ListBox1Col.SelectedIndex;
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
            DrDenominacion_SelectedIndexChanged(DrDenominacion, e);
        }


        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = ListBox1.SelectedIndex;
            ListBox1ID.SelectedIndex = ListBox1.SelectedIndex;
            ListBox1Col.SelectedIndex = ListBox1.SelectedIndex;
            DrDenominacion.SelectedIndex = ListBox1.SelectedIndex;
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
            DrDenominacion_SelectedIndexChanged(sender, e);
        }

        public void Limpiar()
        {
            int MiID = 0;
            //MiID =  Convert.ToInt32(cmd.ExecuteScalar());

            //maximo id
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams));
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZCAMPOS", dbParams));
            }
            MiID = MiID + 1;

            TxtCampoName.Text = "";
            Txttitulo.Text = "";
            txtNombre.Text = "";
            //DFormato.SelectedIndex = Convert.ToInt32(fila["ZID_CARGO"].ToString());
            txtCapacidad.Text = "";
            //DTvalidacion.SelectedIndex = Convert.ToInt32(fila["ZTVALIDACION"].ToString());
            //dlNivel.SelectedIndex = Convert.ToInt32(fila["ZNIVEL"].ToString());
            //DEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());
            LbID.Text = "CAMP-" + MiID;
            this.Session["IDCampo"] = MiID;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
        }

        public void DesactivarTxt()
        {
            TxtCampoName.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            Txttitulo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            txtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DFormato.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            txtCapacidad.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DTvalidacion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            DEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtValorDefecto.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtFecha.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
        }

        public void ActivarTxt()
        {

            TxtCampoName.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            Txttitulo.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            txtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DFormato.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            txtCapacidad.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DTvalidacion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            dlNivel.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DEstado.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtValorDefecto.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtFecha.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
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
            this.Session["Edicion"] = 1;
            //maximo id
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZCAMPOS", dbParams));
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //MiID = Convert.ToInt32(DBHelper.ExecuteScalarOra("SELECT MAX(ZID) FROM USER_GEDESPOL.ZCAMPOS", dbParams));
            }
            MiID = MiID + 1;

            TxtCampoName.Text = Convert.ToString(MiID);
            Txttitulo.Text = "";
            txtNombre.Text = "";
            txtCapacidad.Text = "";
            LbID.Text = "CAMP-" + MiID;
            this.Session["IDCampo"] = MiID;
            this.Session["Edicion"] = 1;
            //btnEditar.Attributes.Add("onclick", "this.disabled=true;");
            //btnNuevo.Attributes.Add("onclick", "this.disabled=true;");
            //btnEditar.CssClass = "myButtonOver";
            //btnNuevo.CssClass = "myButtonOver";
            //btnGuardar.CssClass = "myButtonOff";
            //btnCancelar.CssClass = "myButtonOff";
            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;

            //btnEditar.Enabled = false;
            //btnNuevo.Enabled = false;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    //MiTxt.ReadOnly = true;
                    MiTxt.Enabled = true;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");

                }
            }
            ActivarTxt();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = 2;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    //MiTxt.ReadOnly = true;
                    MiTxt.Enabled = true;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
                }
            }
            ActivarTxt();
            //btnEditar.Enabled = false;
            //btnNuevo.Enabled = false;


            //btnEditar.Attributes.Add("cssClass", "botonoculto");
            //btnEditar.Attributes.Add("cssClass", "botonoculto");
            //btnNuevo.Attributes.Add("cssClass", "botonoculto");
            //btnEditar.CssClass = "myButtonOver";
            //btnNuevo.CssClass = "myButtonOver";
            //btnGuardar.CssClass = "myButtonOff";
            //btnCancelar.CssClass = "myButtonOff";
            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox MiTxt = (TextBox)ctrl;
                    MiTxt.Enabled = false;
                    //MiTxt.ReadOnly = false;
                    //MiTxt.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                }
            }
            DesactivarTxt();
            //btnEditar.Enabled = true;
            //btnNuevo.Enabled = true;
            //btnEditar.Attributes.Add("onclick", "this.disabled=false;");
            //btnNuevo.Attributes.Add("onclick", "this.disabled=false;");
            //btnEditar.CssClass = "myButtonOn";
            //btnNuevo.CssClass = "myButtonOn";
            //btnGuardar.CssClass = "myButtonOn";
            //btnCancelar.CssClass = "myButtonOn";
            btnNuevo.Visible = true;
            btnEditar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
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

        protected void DFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(DrDenominacion.SelectedItem.Value);
            //string index = DrDenominacion.SelectedItem.Text;
            //1   Varchar VARCHAR
            //2   Entero INT
            //3   Memo VARCHAR(MAX)
            //4   Fecha DATETIME
            //5   Decimal DECIMAL
            //6   Numerico    NUMERIC
            switch (index)
            {
                case 1:
                    txtCapacidad.Text = "255";
                    break;
                case 2:
                    txtCapacidad.Text = "8";
                    break;
                case 3:
                    txtCapacidad.Text = "MAX";
                    break;
                case 4:
                    txtCapacidad.Text = "10";
                    break;
                case 5:
                    txtCapacidad.Text = "8,2";
                    break;
                case 6:
                    txtCapacidad.Text = "18";
                    break;
            }

            //DataTable dt = this.Session["Campos"] as DataTable;
            //foreach (DataRow fila in dt.Rows)
            //{

            //    if (fila["ZID"].ToString() == DrDenominacion.SelectedItem.Value)
            //    {
            //        if (fila["ZTVALIDACION"].ToString() != "")
            //        {
            //            DFormato.SelectedIndex = Convert.ToInt32(fila["ZTVALIDACION"].ToString());
            //            this.Session["IDTVCampo"] = Convert.ToInt32(fila["ZTVALIDACION"].ToString());
            //            break;
            //        }
            //        else
            //        {
            //            DFormato.SelectedIndex = Convert.ToInt32(0);
            //            this.Session["IDTVCampo"] = Convert.ToInt32(0);
            //            break;
            //        }

            //    }

            //    //string valor = fila["NombreDeLaColumna"].ToString();//por indice campo string valor = fila[0].ToString();

            //}
        }

        protected void DTvalidacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dlNivel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaCampo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }
    }

}