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
//using Teach;

namespace Satelite
{
    public partial class AltaCampo : System.Web.UI.Page
    {
        //private int registros = 0;
        //private int Competencia = 0;
        private string VAR = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("Principal.aspx"); //Default

            try
            {
                ////if (Session["Session"] != null)
                ////{
                ////}
                ////else
                ////{
                ////    Response.Redirect("Login.aspx"); //Default
                ////}

                if (this.Session["MiNivel"].ToString() != "9")
                {
                    Response.Redirect("Principal.aspx"); //Default
                }

                if (!IsPostBack)
                {

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
                    DrDenominacion.DataTextField = "ZDescripcion";

                    if (this.Session["Cargo"] != null)
                    {
                        DataTable dt1 = this.Session["Cargo"] as DataTable;
                        DrDenominacion.DataSource = dt1;
                        DrDenominacion.DataBind();
                        DrDenominacion.SelectedIndex = -1;
                    }
                    else
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = Main.CargaCampos().Tables[0];
                        DrDenominacion.DataSource = dt1;
                        DrDenominacion.DataBind();
                        this.Session["Campos"] = dt1;
                        DrDenominacion.SelectedIndex = -1;
                    }


                    dlNivel.DataValueField = "ZID";
                    dlNivel.DataTextField = "ZDescripcion";

                    if (this.Session["Niveles"] != null)
                    {
                        DataTable dt3 = this.Session["Niveles"]  as DataTable;
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
                Response.Redirect("thEnd.aspx");
            }
        }

        protected void DrDenominacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dEvalucion.SelectedIndex = dEvalucion.Items.IndexOf(dEvalucion.Items.FindByText(dEvalucion.SelectedItem.Text)); ;// + " - " + ddlFruits.SelectedItem.Value;
            //int index = 0;

            DataTable dt = this.Session["Campos"] as DataTable;
            foreach (DataRow fila in dt.Rows)
            {
                
                if (fila["ZID"].ToString() == DrDenominacion.SelectedItem.Value)
                {
                    TxtCampoName.Text =  fila["ZID"].ToString();
                    Txttitulo.Text = fila["ZDESCRIPCION"].ToString();
                    txtNombre.Text = fila["ZVALIDACION"].ToString();
                    txtNombre.Text = fila["ZTIPO"].ToString();
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

                Column = "INSERT INTO " + Tabla + " (ZID, ZNOMBRE, ZDESCRIPCION, ZFORMATO, ZCAPACIDAD, ZTVALIDACION, ZNIVEL, ZESTADO, ZACTIVO) VALUES (";
               
                ColumnVal = MiID + ",'" + txtNombre.Text.Replace(" ", "_") + "','" + Txttitulo.Text + "','" + DFormato.SelectedValue + "'," + txtCapacidad.Text + "," ;
                
                if(DTvalidacion.SelectedValue == "")
                {
                    ColumnVal +=  " 0,";
                }
                else
                {
                    ColumnVal += DTvalidacion.SelectedValue + ",";
                }
                
                ColumnVal = dlNivel.SelectedValue + "," + DEstado.SelectedValue + ",1)";

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
                    Column = "UPDATE " + Tabla + " SET ZNOMBRE ='" + txtNombre.Text.Replace(" ", "_") + "',  ZDESCRIPCION ='" + Txttitulo.Text + "', ";
                    Column += " ZFORMATO = " + DFormato.SelectedValue + ",";
                    Column += " ZCAPACIDAD = '" + txtCapacidad.Text + "', ";
                    if(DTvalidacion.SelectedValue == "")
                    {
                        Column += " ZTVALIDACION = 0 , ";
                    }
                    else
                    {
                        Column += " ZTVALIDACION = " + DTvalidacion.SelectedValue + ", ";
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
            btnEditar.CssClass = "myButtonOn";
            btnNuevo.CssClass = "myButtonOn";
            btnGuardar.CssClass = "myButtonOn";
            btnCancelar.CssClass = "myButtonOn";

            DataTable dt1 = new DataTable();
            dt1 = Main.CargaCampos().Tables[0];
            DrDenominacion.DataSource = dt1;
            DrDenominacion.DataBind();
            this.Session["Campos"] = dt1;

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
            Txttitulo.Text ="";
            txtNombre.Text = "";
            //DFormato.SelectedIndex = Convert.ToInt32(fila["ZID_CARGO"].ToString());
            txtCapacidad.Text = "";
            //DTvalidacion.SelectedIndex = Convert.ToInt32(fila["ZTVALIDACION"].ToString());
            //dlNivel.SelectedIndex = Convert.ToInt32(fila["ZNIVEL"].ToString());
            //DEstado.SelectedIndex = Convert.ToInt32(fila["ZESTADO"].ToString());
            LbID.Text = "CAMP-" +MiID;
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
            btnEditar.CssClass = "myButtonOver";
            btnNuevo.CssClass = "myButtonOver";
            btnGuardar.CssClass = "myButtonOff";
            btnCancelar.CssClass = "myButtonOff";
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
            btnEditar.CssClass = "myButtonOver";
            btnNuevo.CssClass = "myButtonOver";
            btnGuardar.CssClass = "myButtonOff";
            btnCancelar.CssClass = "myButtonOff";
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

        protected void DFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = Convert.ToInt32(DrDenominacion.SelectedItem.Value);

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
            Response.Redirect("CambioLogin.aspx"); //Default
        }
    }

}