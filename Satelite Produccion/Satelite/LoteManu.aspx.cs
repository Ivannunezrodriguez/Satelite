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

//LoteAuto: Generación de lotes automáticos
//LoteRevi : Revisión lotes y preparación importación a GoldenSoft
//LoteManu: Generación lotes manuales (para lotes externos) 


namespace Satelite
{


    public partial class LoteManu : System.Web.UI.Page
    {
        public Boolean Esta = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(' myStringVariable ');", true);

            try
            {
                this.Session["Procesa"] = "0";

                if (Session["Session"] == null)
                {
                    this.Session["Error"] = "0";
                    Response.Redirect("Login.aspx"); //Default
                }

                if (this.Session["MiNivel"].ToString() == "9")
                {
                    Nominas.Visible = true;
                }


                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    Response.Redirect("Login.aspx"); //Default
                }

                if (!IsPostBack)
                {
                    //this.Session["DESARROLLO"] = "0";
                    this.Session["IDSecuencia"] = "0";

                    if (txtQRCode.Text == "")
                    {
                        Nueva_Secuencia();
                        txtQRCode.Text = "Seleccione un Tipo de Lote";
                    }

                    this.Session["IDLote"] = "0";

                    this.Session["AltoC"] = "0";
                    this.Session["AnchoC"] = "0";
                    this.Session["AltoT"] = "0";
                    this.Session["AnchoT"] = "0";
                    Carga_Impresoras("0");

                }
                else
                {
                    try
                    {
                        if (this.Session["IDSecuencia"].ToString() == null)
                        {
                            Response.Redirect("thEnd.aspx");
                        }
                        this.Session["Secuencias"] = Main.CargaSecuencia().Tables[0];
                    }
                    catch (NullReferenceException ex)
                    {
                        //Lberror.Text += ex.Message;
                        if (Session["Session"] == null)
                        {
                            Response.Redirect("Login.aspx");
                        }
                        else if (this.Session["Error"].ToString() == "0")
                        {
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            Response.Redirect("thEnd.aspx");
                        }
                    }

                }

                if (this.Session["DESARROLLO"].ToString() == "DESARROLLO")
                {
                    H3Titulo.Visible = false;
                    H3Desarrollo.Visible = true;
                }
                else
                {
                    H3Titulo.Visible = true;
                    H3Desarrollo.Visible = false;
                }

            }
            catch (Exception ex)
            {
                if (Session["Session"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else if (this.Session["Error"].ToString() == "0")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("thEnd.aspx");
                }
            }
            dvPrinters.Visible = true;
            dvDrlist.Visible = false;
        }

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecoNomina.aspx");
        }

        private void Nueva_Secuencia()
        {
            DataTable dt3 = Main.CargaSecuencia().Tables[0];
            this.Session["Secuencias"] = dt3;
            DrVariedad.Items.Clear();
            DrVariedad.AppendDataBoundItems = true;
            DrVariedad.Items.Add("Seleccione un tipo de lote...");
            DrVariedad.DataValueField = "ZID";
            DrVariedad.DataTextField = "ZDESCRIPCION";


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

                if (DrVariedad.SelectedItem.Text == "Seleccione un tipo de lote...")
                {
                    SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " WHERE A.LOTE = B.ZLOTE ";
                    SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0')";
                }
                else
                {
                    SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM, A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
                    SQL += " FROM ZENTRADA A, ZLOTESCREADOS B ";
                    SQL += " WHERE B.ZID_SECUENCIA = " + ID;
                    SQL += " AND A.LOTE = B.ZLOTE ";
                    SQL += " AND(A.ESTADO IS NULL OR A.ESTADO = '0')";
                }



                DataTable dbB = Main.BuscaLote(SQL).Tables[0];
                DrLotes.Items.Clear();
                DrLotes.DataValueField = "ID";
                DrLotes.DataTextField = "TODO";
                DrLotes.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
                DrLotes.DataSource = dbB;
                DrLotes.DataBind();
                DrLotes.SelectedIndex = -1;

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
            catch (Exception ex)
            {
                Response.Redirect("thEnd.aspx");
            }
        }


        //private void Carga_Lotes(string ID)
        //{
        //    //string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 ";

        //    string SQL = "SELECT * FROM ZLOTESCREADOS  WHERE ZESTADO = 0 AND ZID_SECUENCIA = " + ID;
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    DrScaneados.Items.Clear();
        //    DrScaneados.DataValueField = "ZID";
        //    DrScaneados.DataTextField = "ZLOTE";
        //    // insertamos el elemento en la primera posicion:
        //    DrScaneados.Items.Insert(0, new ListItem("Seleccionar Lote", ""));
        //    DrScaneados.DataSource = dbA;
        //    DrScaneados.DataBind();
        //    DrScaneados.SelectedIndex = -1;

        //    //SQL = "SELECT * FROM ZENTRADA  WHERE ESTADO IS NULL OR ESTADO ='0' AND ZID_SECUENCIA = " + ID;

        //    SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM ";
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




        //    SQL = "SELECT LOTE, COUNT(*) as total FROM ZENTRADA GROUP BY LOTE HAVING COUNT(*) > 1";
        //    dbA = Main.BuscaLote(SQL).Tables[0];
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
                    LbSecuenciaLote.Text = Cadena;
                    btnGenerate_Click(null, null);
                    LbCodigoLote.Text = "CÓDIGO LOTE:";

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
            TxtCampo.Enabled = true;
            TxtFecha.Enabled = true;
            TxtVariedad.Enabled = true;
            TxtCajas.Enabled = true;
            TxtPlantas.Enabled = true;
            txtQRCode.Enabled = true;
            TxtLoteDestino.Enabled = true;
            
            //btnGenerate.Visible = true;
            //btnNuevo.Visible = true;
            //TxtID.Enabled = true;
            TxtForm.Enabled = true;
            TxtManojos.Enabled = true;
            TxtDesde.Enabled = true;
            TxtHasta.Enabled = true;
            TxtETDesde.Enabled = true;
            TxtETHasta.Enabled = true;
            TxtTuneles.Enabled = true;
            TxtPasillos.Enabled = true;
            TxtObservaciones.Enabled = true;
            TxtOK.Enabled = true;
            Oculta_Datos(1);
        }

        private void Deshabilita_contoles()
        {
            TxtCampo.Enabled = false;
            TxtFecha.Enabled = false;
            TxtVariedad.Enabled = false;
            TxtCajas.Enabled = false;
            TxtPlantas.Enabled = false;
            txtQRCode.Enabled = false;
            TxtLoteDestino.Enabled = false;
            //btnGenerate.Visible = false;
            //btnNuevo.Visible = false;
            TxtID.Enabled = false;
            TxtForm.Enabled = false;
            TxtManojos.Enabled = false;
            TxtDesde.Enabled = false;
            TxtHasta.Enabled = false;
            TxtETDesde.Enabled = false;
            TxtETHasta.Enabled = false;
            TxtTuneles.Enabled = false;
            TxtPasillos.Enabled = false;
            TxtObservaciones.Enabled = false;
            TxtOK.Enabled = false;
            Oculta_Datos(0);

        }
        //protected void btnValidaUser_Click(object sender, EventArgs e)
        //{
        //    LimpiaCajas();
        //    DataSet ds = Login.ValidarUser(TextUser.Text, TextPass.Text);
        //    DataTable dt = ds.Tables[0];
        //    if (dt.Rows.Count == 0)
        //    {
        //        if (txtQRCode.Text != "")
        //        {
        //            TextAlerta.Text = "El usuario no tiene permisos, pero puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
        //            TextAlertaErr.Text += "";
        //            alerta.Visible = true;
        //            alertaErr.Visible = false;
        //            alertaLog.Visible = false;
        //            btProcesa.Visible = true;
        //            btPorcesa.Visible = false;
        //            Deshabilita_contoles();
        //        }
        //        else
        //        {
        //            TextAlerta.Text = "El usuario no tiene permisos para editar esta página.";
        //            TextAlertaErr.Text += "";
        //            alerta.Visible = true;
        //            alertaErr.Visible = false;
        //            alertaLog.Visible = false;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            Deshabilita_contoles();
        //        }
        //    }
        //    else
        //    {
        //        if (dt.Rows[0]["ZNIVEL"].ToString() != "9")
        //        {
        //            if (txtQRCode.Text != "")
        //            {
        //                TextAlerta.Text = "El usuario no tiene permisos suficientes para editar esta página, pero puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
        //                TextAlertaErr.Text += "";
        //                alerta.Visible = true;
        //                alertaErr.Visible = false;
        //                alertaLog.Visible = false;
        //                btProcesa.Visible = true;
        //                btPorcesa.Visible = false;
        //                Deshabilita_contoles();
        //            }
        //            else
        //            {
        //                TextAlerta.Text = "El usuario no tiene permisos suficientes para editar esta página.";
        //                TextAlertaErr.Text += "";
        //                alerta.Visible = true;
        //                alertaErr.Visible = false;
        //                alertaLog.Visible = false;
        //                btProcesa.Visible = false;
        //                btPorcesa.Visible = false;
        //                Deshabilita_contoles();
        //            }
        //        }
        //        else
        //        {
        //            if (txtQRCode.Text != "")
        //            {
        //                TextAlerta.Text = "Se habilitarán los controles de la página para poder tratar con ellos. Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
        //                TextAlertaErr.Text += "";
        //                alerta.Visible = true;
        //                alertaErr.Visible = false;
        //                alertaLog.Visible = false;
        //                btProcesa.Visible = true;
        //                btPorcesa.Visible = false;
        //                Habilita_contoles();
        //            }
        //            else
        //            {
        //                TextAlerta.Text = "Se habilitarán los controles de la página para poder tratar con ellos.";
        //                TextAlertaErr.Text += "";
        //                alerta.Visible = true;
        //                alertaErr.Visible = false;
        //                alertaLog.Visible = false;
        //                btProcesa.Visible = false;
        //                btPorcesa.Visible = false;
        //                Habilita_contoles();
        //            }
        //        }
        //    }
        //}

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
            alertaErr.Visible = false;
            //Boolean Esta = validateTime(TxtFecha.Text);
            //if (Esta == false)
            //{
            //    TextAlertaErr.Text = "El campo FECHA CORTE no contiene una fecha valida.";
            //    TextAlerta.Text = "";
            //    alertaLog.Visible = false;
            //    alerta.Visible = false;
            //    alertaErr.Visible = true;
            //    return;
            //}
            Repara_Fecha(TxtFecha.Text);

            string SQL = "UPDATE ZENTRADA SET TIPO_FORM = '" + TxtForm.Text + "',";
            SQL += "FECHA ='" + TxtFecha.Text + "',";
            SQL += "TIPO_PLANTA ='" + TxtCampo.Text + "',";
            SQL += "VARIEDAD ='" + TxtVariedad.Text + "',";
            SQL += "LOTE ='" + txtQRCode.Text + "',";
            SQL += "UNIDADES ='" + TxtCajas.Text + "',";
            SQL += "NUM_UNIDADES ='" + TxtPlantas.Text + "',";
            SQL += "MANOJOS ='" + TxtManojos.Text + "',";
            SQL += "DESDE ='" + TxtDesde.Text + "',";
            SQL += "HASTA ='" + TxtHasta.Text + "',";
            SQL += "ETDESDE ='" + TxtETDesde.Text + "',";
            SQL += "ETHASTA ='" + TxtETHasta.Text + "',";
            SQL += "TUNELES ='" + TxtTuneles.Text + "',";
            SQL += "PASILLOS ='" + TxtPasillos.Text + "',";
            SQL += "OBSERVACIONES ='" + TxtObservaciones.Text + "',";
            SQL += "LOTEDESTINO ='" + TxtLoteDestino.Text + "',";
            SQL += "OK ='" + TxtOK.Text + "'";
            SQL += " WHERE ID = " + LbIDLote.Text;
            DBHelper.ExecuteNonQuery(SQL);

            LimpiaCajas();
            Carga_Lotes(this.Session["IDSecuencia"].ToString());

        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //string SQL = "DELETE FROM ZENTRADA WHERE ID = " + LbIDLote.Text;
            string SQL = "UPDATE ZENTRADA  SET ESTADO = '2' ";
            SQL += " WHERE ID = " + LbIDLote.Text;
            //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            DBHelper.ExecuteNonQuery(SQL);
            LimpiaCajas();
            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            //BTerminado.Visible = false;
            //Btfin.Visible = false;
        }

        protected void BTerminado_Click(object sender, EventArgs e)
        {
            string SQL = "UPDATE ZENTRADA  SET ESTADO = '0' ";
            SQL += " WHERE ID = " + LbIDLote.Text;
            //DBHelper.ExecuteNonQuery(Variables.tipo, SQL, Variables.Miconexion, Variables.nomenclatura + "ZSECUENCIAS ");
            DBHelper.ExecuteNonQuery(SQL);
            //Btfin.Visible = true;
            //BTerminado.Visible = false;
            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
            }
            //btnGenerateTodo_Click(sender, e);
            alerta.Visible = false;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if(txtQRCode.Text == "") 
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

            string SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
            SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
            DBHelper.ExecuteNonQuery(SQL);

            btnGenerate_Click(sender, e);


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

        protected void btnUser_Click(object sender, EventArgs e)
        {
            if (TxtForm.Enabled == true)
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
        protected void btnNuevoLote_Click(object sender, EventArgs e)
        {
            //btnNuevoLote.Visible = false;
            //BtGuardaLote.Visible = true;
            //BtModifica.Visible = false;
            //BtCancelaLote.Visible = true;
            //BtDelete.Visible = false;
            //btGeneraNew.Visible = true;
            //Btfin.Visible = false;
            //BTerminado.Visible = false;
            LimpiaCajas();
            TxtForm.Text = "Independiente";
        }

        protected void btnCancelaLote_Click(object sender, EventArgs e)
        {
            //btnNuevoLote.Visible = true;
            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtCancelaLote.Visible = false;
            //btGeneraNew.Visible = false;
            //BtDelete.Visible = true;
        }

        private void Repara_Fecha(string Fecha)
        {
            string Mdia = "";
            string Mmes = "";
            string Mano = "";
            int a = 0;

            if (TxtFecha.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "/");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        TxtFecha.Text = Mano + "-" + Mmes + "-" + Mdia;
                    }
                }
            }

            if (TxtFecha.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "-");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        TxtFecha.Text = Mano + "-" + Mmes + "-" + Mdia;
                    }
                }
            }
        }

        private void Convierte_Fecha(string Fecha)
        {
            string Mdia = "";
            string Mmes = "";
            string Mano = "";
            int a = 0;

            if (TxtFecha.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "/");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        TxtFecha.Text = Mano + Mmes + Mdia;
                    }
                }
            }

            if (TxtFecha.Text.Contains("/"))
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(TxtFecha.Text, "-");

                foreach (string Linea in CadaLinea)
                {
                    if (a == 0) { Mdia = Linea; }
                    if (a == 1) { Mmes = Linea; }
                    if (a == 2)
                    {
                        Mano = Linea;
                        TxtFecha.Text = Mano + Mmes + Mdia;
                    }
                }
            }
        }
        protected void BtGuardaLote_Click(object sender, EventArgs e)
        {
            DateTime dt;

            //if (DateTime.TryParse(TxtFecha.Text, out dt))
            //if (DateTime.TryParseExact(TxtFecha.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            //{
            //    TextAlertaErr.Text = "El campo FECHA CORTE no contiene una fecha valida.";
            //    TextAlerta.Text = "";
            //    alertaLog.Visible = false;
            //    alerta.Visible = false;
            //    alertaErr.Visible = true;
            //    return;
            //}

            Repara_Fecha(TxtFecha.Text);

            btnCancelaLote_Click(sender, e);

            string SQL = "INSERT INTO ZENTRADA (TIPO_FORM, FECHA, TIPO_PLANTA, VARIEDAD, LOTE, UNIDADES, NUM_UNIDADES, MANOJOS, ";
            SQL += "DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS, OBSERVACIONES, OK, LOTEDESTINO) ";
            SQL += " VALUES ('" + TxtForm.Text + "','" + TxtFecha.Text + "','" + TxtCampo.Text + "','" + TxtVariedad.Text + "','" + txtQRCode.Text + "','" + TxtCajas.Text + "',";
            SQL += "'" + TxtPlantas.Text + "','" + TxtManojos.Text + "','" + TxtDesde.Text + "','" + TxtHasta.Text + "','" + TxtETDesde.Text + "','" + TxtETHasta.Text + "',";
            SQL += "'" + TxtTuneles.Text + "','" + TxtPasillos.Text + "','" + TxtObservaciones.Text + "','" + TxtOK.Text + "','" + TxtLoteDestino.Text +"')";

            DBHelper.ExecuteNonQuery(SQL);
            //Btfin.Visible = false;
            //BTerminado.Visible = true;
            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
            }
            //btnGenerateTodo_Click(sender, e);
            alerta.Visible = false;
            Carga_Lotes(this.Session["IDSecuencia"].ToString());
        }
        protected void BTfin_Click(object sender, EventArgs e)
        {

        }
        protected void btnPorcesa_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            alertaErr.Visible = false;
            btnPrint2.Visible = false;
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

            Boolean Esta = false;
            SQL = "SELECT * FROM ZENTRADA  WHERE LOTE = '" + txtQRCode.Text + "' ";
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dbA.Rows)
            {
                if (fila["LOTE"].ToString() == txtQRCode.Text)
                {
                    Esta = true;
                    LbIDLote.Text = fila["ID"].ToString();
                    this.Session["IDSecuencia"] = fila["ID"].ToString();

                    TxtCampo.Text = fila["TIPO_PLANTA"].ToString();
                    TxtFecha.Text = fila["FECHA"].ToString();
                    TxtVariedad.Text = fila["VARIEDAD"].ToString();
                    TxtCajas.Text = fila["UNIDADES"].ToString();//* Tabla BANDEJAS 
                    TxtDesde.Text = fila["DESDE"].ToString();
                    TxtEstado.Text = fila["ESTADO"].ToString();
                    TxtDispositivo.Text = fila["DeviceName"].ToString();
                    TxtLoteDestino.Text = fila["LOTEDESTINO"].ToString();
                    LbDateForm.Text = fila["SendTime"].ToString();
                    TxtID.Text = fila["ID"].ToString();
                    TxtForm.Text = fila["TIPO_FORM"].ToString();
                    TxtManojos.Text = fila["MANOJOS"].ToString();
                    TxtDesde.Text = fila["DESDE"].ToString();
                    TxtHasta.Text = fila["HASTA"].ToString();
                    TxtETDesde.Text = fila["ETDESDE"].ToString();
                    TxtETHasta.Text = fila["ETHASTA"].ToString();
                    TxtTuneles.Text = fila["TUNELES"].ToString();
                    TxtPasillos.Text = fila["PASILLOS"].ToString();
                    TxtObservaciones.Text = fila["OBSERVACIONES"].ToString();
                    TxtOK.Text = fila["OK"].ToString();



                    if (TxtCajas.Text == "CAJAS")
                    {
                        //LbnumeroPlantas.Text = "Número de Cajas:";
                        LbCajasS.Text = "Unidades: " + fila["UNIDADES"].ToString() + " " + fila["NUM_UNIDADES"].ToString(); // fila["UNIDADES"].ToString();
                        LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();

                    }
                    if (TxtCajas.Text == "PLANTAS")
                    {
                        //LbnumeroPlantas.Text = "Número de Plantas:";
                        LbCajasS.Text = "Unidades: " + fila["UNIDADES"].ToString();
                        LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();
                    }

                    TxtPlantas.Text = fila["NUM_UNIDADES"].ToString();
                    LbCampoS.Text = "CAMPO O SECTOR: " + fila["DESDE"].ToString();
                    try
                    {
                        foreach (DataRow fila2 in dbP.Rows)
                        {
                            if (fila2["ZTIPO_PLANTA"].ToString() == fila["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == fila["UNIDADES"].ToString())
                            {
                                if (fila["UNIDADES"].ToString() == "PLANTAS")
                                {
                                    LbPlantasS.Text = "Nº Plantas: " + fila["NUM_UNIDADES"].ToString();
                                    break;
                                }
                                else if (fila["UNIDADES"].ToString() == "CAJAS")
                                {
                                    if (fila["MANOJOS"].ToString() == "0" || fila["MANOJOS"].ToString() == "" || fila["MANOJOS"].ToString() == null)
                                    {
                                        LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(fila["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
                                    }
                                    else
                                    {
                                        foreach (DataRow fila3 in dbP.Rows)
                                        {
                                            if (fila3["ZTIPO_PLANTA"].ToString() == fila["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                                            {
                                                int NN = Convert.ToInt32(fila["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                                                LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(fila["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Lberror.Text = ex.Message;
                    }
                    LbPlantaS.Text = "TIPO PLANTA: " + fila["TIPO_PLANTA"].ToString();
                    LbFechaS.Text = "FECHA CORTE: " + fila["FECHA"].ToString();
                    LbVariedadS.Text = "VARIEDAD: " + fila["VARIEDAD"].ToString();

                    //if (TxtID.Enabled == true)
                    //{
                    //    Oculta_Datos(1);
                    //}
                    //else
                    //{
                    //    Oculta_Datos(0);
                    //}

                    Lbcompleto.Text = "QR COMPLETO";
                    this.Session["Procesa"] = "1";
                    btnGenerate_Click(sender, e);
                    if (DrPrinters.SelectedItem.Value == "4")
                    {
                        btnGeneraTodoPerf_Click(sender, e);
                    }
                    else
                    {
                        btnGenerateTodo_Click(sender, e);
                    }
                    //btnGenerateTodo_Click(sender, e);
                    this.Session["Procesa"] = "0";
                    //if (fila["ESTADO"].ToString() == "" || fila["ESTADO"].ToString() == null)
                    //{
                    //    BTerminado.Visible = true;
                    //}
                    //else
                    //{
                    //    Btfin.Visible = true;
                    //}
                    break;
                }
            }
            if (Esta == false)
            {
                H1Normal.Visible = false;
                H1Seleccion.Visible = false;
                H1Red.Visible = true;
                H1Green.Visible = false;

                //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

                btnGenerate_Click(sender, e);
                TextAlertaErr.Text = "Aún no se encuentra el código de lote en la base de datos para formularios de Scan-IT. Genere el registro del formulario desde el Móvil, " + Environment.NewLine;
                TextAlertaErr.Text += "para este Código QR que se presenta en pantalla y, una vez enviado, pulse nuevamente sobre este botón. O no haga nada, siga con otros lotes," + Environment.NewLine;
                TextAlertaErr.Text += "pues este ha quedado guardado en la lista '2', y una vez se introduzca el formulario correspondiente lo encontrará en la lista '1'" + Environment.NewLine;
                TextAlerta.Text = "";
                alertaLog.Visible = false;
                alerta.Visible = false;
                alertaErr.Visible = true;
                btProcesa.Visible = false;
                btPorcesa.Visible = true;
            }
            else
            {
                //Antes Estado = 1
                SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "' ";
                DBHelper.ExecuteNonQuery(SQL);
                //alerta.Visible = false;
                //alertaErr.Visible = false;
            }
            //if(btNew.Enabled == false)
            //{
            //    SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
            //    SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
            //    DBHelper.ExecuteNonQuery(SQL);

            //    SQL = "SELECT ZSECUENCIA FROM ZSECUENCIAS  WHERE ZID = " + DrVariedad.SelectedItem.Value;
            //    DataTable dbC = Main.BuscaLote(SQL).Tables[0];
            //    foreach (DataRow fila in dbC.Rows)
            //    {
            //        this.Session["NumeroSecuencia"] = fila["ZSECUENCIA"].ToString();
            //        break;
            //    }

            //    int AA = Convert.ToInt32(this.Session["NumeroSecuencia"].ToString()) + 1;
            //    SQL = "UPDATE ZSECUENCIAS  SET ZSECUENCIA = '" + AA + "' ";
            //    SQL += " WHERE ZID = " + DrVariedad.SelectedItem.Value;
            //    DBHelper.ExecuteNonQuery(SQL);
            //    btNew.Enabled = true;
            //}

            Carga_Lotes(this.Session["IDSecuencia"].ToString());
        }

        protected void btnGeneraTodoPerf_Click(object sender, EventArgs e)
        {
            string code = "";
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;
            if (DrPrinters.SelectedItem.Value == "4")
            {
                //code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
                //LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
                code += TxtVariedad.Text + Environment.NewLine;
                LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
            }
            else
            {
                code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
                LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
                code += Label4.Text + TxtCampo.Text + Environment.NewLine;
                LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
                code += Label5.Text + TxtFecha.Text + Environment.NewLine;
                code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
                LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
                code += LbCajasS.Text + Environment.NewLine;
                code += LbPlantasS.Text + Environment.NewLine;
            }


            H1Normal.Visible = false;
            H1Seleccion.Visible = false;
            H1Red.Visible = false;
            H1Green.Visible = true;
            DrPrinters_Click();

            TextAlertaErr.Text = "";


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                imgBarCode.Height = 200; // Convert.ToInt32(TxAltoT.Text);
                imgBarCode.Width = 200; //Convert.ToInt32(TxAnchoT.Text);
            }
            catch (Exception a)
            {
                TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                //TxAltoT.Text = "200";
                //TxAnchoT.Text = "200";
                alertaErr.Visible = true;
            }

            using (Bitmap bitMap = qrCode.GetGraphic(40))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                PlaceHolder2.Controls.Add(imgBarCode);
            }
        }

        protected void btnGeneraNew_Click(object sender, EventArgs e)
        {
            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
            }
            //btnGenerateTodo_Click(sender, e);
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.Session["Procesa"].ToString() == "0")
            {
                H1Normal.Visible = false;
                H1Seleccion.Visible = true;
                H1Red.Visible = false;
                H1Green.Visible = false;
                DrPrinters_Click();
                //HLoteProceso.InnerText = "Código QR seleccionado";
                //HLoteProceso.Attributes.Add("style", "color: black; font-weight:bold;");

                Lbcompleto.Text = "";
                alerta.Visible = false;
                alertaErr.Visible = false;
                TextAlerta.Text = "Código insertado correctamente. Ahora puedes escanear el código QR desde Scan-IT con el Móvil, completar su registro en el formulario y enviarlo. Pulsa sobre este botón cuando lo envíes, para poder continuar.";
                alerta.Visible = true;
                alertaLog.Visible = false;
                btnPrint2.Visible = false;
                btProcesa.Visible = true;
                btPorcesa.Visible = false;
                //BTerminado.Visible = false;
            }

            string code = txtQRCode.Text;// + " ";
            LbSecuenciaLote.Text = code;
            LbSecuenciaLoteQR.Text = txtQRCode.Text;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            try
            {
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    imgBarCode.Height =150;
                    imgBarCode.Width = 150;
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
            using (Bitmap bitMap = qrCode.GetGraphic(40))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                //PlaceHolder1.Controls.Add(imgBarCode);
                if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
                {
                    PlaceHolder1.Controls.Add(imgBarCode);
                }
                if (DrPrinters.SelectedItem.Value == "2")
                {
                    PlaceHolderQR.Controls.Add(imgBarCode);
                }
                if (DrPrinters.SelectedItem.Value == "3")
                {
                    pnlContentsFT.Controls.Add(imgBarCode);
                }
            }

            LbCodigoLote.Text = "CÓDIGO LOTE:";
            //Comentar en produccion
            //btnGenerateTodo_Click(sender, e);
        }

        protected void btnGenerateTodo_Click(object sender, EventArgs e)
        {
            string code = "";
            string CodigoError = "";
            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            if (TxtDesde.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Campo o Sector,";
            }
            else
            {
                code += LbDesde.Text + TxtDesde.Text + Environment.NewLine;
                LbCampoS.Text = LbDesde.Text + " " + TxtDesde.Text;
            }

            if (TxtCampo.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Tipo Plantas,";
            }
            else
            {
                code += Label4.Text + TxtCampo.Text + Environment.NewLine;
                LbPlantaS.Text = Label4.Text + " " + TxtCampo.Text;
            }

            if (TxtFecha.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Fecha Corte,";
            }
            else
            {
                code += Label5.Text + TxtFecha.Text + Environment.NewLine;
                LbFechaS.Text = Label5.Text + " " + TxtFecha.Text.ToString().Substring(0, 10);
            }

            if (TxtVariedad.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Variedad,";
            }
            else
            {
                code += Label6.Text + TxtVariedad.Text + Environment.NewLine;
                LbVariedadS.Text = Label6.Text + " " + TxtVariedad.Text;
            }

            if (TxtCajas.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Número Cajas,";
            }
            else
            {
                //code += Label7.Text + TxtCajas.Text + Environment.NewLine;
                //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
                code += LbCajasS.Text + Environment.NewLine;
                //LbCajasS.Text = Label7.Text + " " + TxtCajas.Text;
            }

            if (TxtPlantas.Text == "")
            {
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                LbPlantaS.Text = "";
                CodigoError += " Número Plantas,";
            }
            else
            {
                code += LbPlantasS.Text + Environment.NewLine;
                //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
                //code += LbnumeroPlantas.Text + TxtPlantas.Text + Environment.NewLine;
                //LbPlantasS.Text = LbnumeroPlantas.Text + " " + TxtPlantas.Text;
            }

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

                //HLoteProceso.InnerText = "Código QR PROCESADO";
                //HLoteProceso.Attributes.Add("style", "color: LimeGreen; font-weight:bold;");

                TextAlerta.Text = "Ahora puedes imprimir el código QR para el palet.";
                TextAlertaErr.Text = "";
                alerta.Visible = true;
                btnPrint2.Visible = true;
                btProcesa.Visible = false;
                //BTerminado.Visible = true;
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.M);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();

            try
            {
                imgBarCode.Height = 200; // Convert.ToInt32(TxAltoT.Text);
                imgBarCode.Width = 200; // Convert.ToInt32(TxAnchoT.Text);
            }
            catch (Exception a)
            {
                TextAlertaErr.Text = "El valor de las medidas para la imagen QR Total no son correctas. Se establece por defecto a 200 X 200 pixeles. El Error :" + a.Message;
                //TxAltoT.Text = "200";
                //TxAnchoT.Text = "200";
                alertaErr.Visible = true;
            }

            using (Bitmap bitMap = qrCode.GetGraphic(40))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                PlaceHolder2.Controls.Add(imgBarCode);
            }
        }

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
        //        //    pnlContentsFT.Controls.Add(imgBarCode);
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
                txtQRCode.Text = "";
                TxtCampo.Text = "";
                TxtFecha.Text = "";
                TxtVariedad.Text = "";
                TxtCajas.Text = "";
                TxtPlantas.Text = "";
                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbPlantaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                Lbcompleto.Text = "";
                LbSecuenciaLote.Text = "";
                TxtEstado.Text = "";
                TxtDispositivo.Text = "";
                TxtLoteDestino.Text = "";

                TxtID.Text = "";
                TxtForm.Text = "";
                TxtManojos.Text = "";
                TxtDesde.Text = "";
                TxtHasta.Text = "";
                TxtETDesde.Text = "";
                TxtETHasta.Text = "";
                TxtTuneles.Text = "";
                TxtPasillos.Text = "";
                TxtObservaciones.Text = "";
                TxtOK.Text = "";
                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
            }
            catch (NullReferenceException ex)
            {
                Lberror.Text += ex.Message;
                //alertaErr.Visible = true;
                //TextAlertaErr.Text = ex.Message;
            }
        }

        private void Oculta_Datos(int Ve)
        {
            try
            {
                if (Ve == 0)
                {
                    //LbID.Visible = false;
                    //TxtID.Visible = false;
                    //LbForm.Visible = false;
                    //TxtForm.Visible = false;
                    ////LbManojo.Visible = false;
                    ////TxtManojos.Visible = false;
                    ////LbDesde.Visible = false;
                    ////TxtDesde.Visible = false;
                    //LbHasta.Visible = false;
                    //TxtHasta.Visible = false;
                    //LbETDesde.Visible = false;
                    //TxtETDesde.Visible = false;
                    //LbETHasta.Visible = false;
                    //TxtETHasta.Visible = false;
                    //LbTuneles.Visible = false;
                    //TxtTuneles.Visible = false;
                    //LbPasillos.Visible = false;
                    //TxtPasillos.Visible = false;
                    //LbObservaciones.Visible = false;
                    //TxtObservaciones.Visible = false;
                    //LbOK.Visible = false;
                    //TxtOK.Visible = false;
                    //btnNuevoLote.Visible = false;
                    //BtDelete.Visible = false;
                    //BtModifica.Visible = false;
                    LimpiaCajas();
                    //BTerminado.Visible = false;
                    //Btfin.Visible = false;
                }
                else
                {
                    //LbID.Visible = true;
                    //TxtID.Visible = true;
                    //LbForm.Visible = true;
                    //TxtForm.Visible = true;
                    ////LbManojo.Visible = true;
                    ////TxtManojos.Visible = true;
                    ////LbDesde.Visible = true;
                    ////TxtDesde.Visible = true;
                    //LbHasta.Visible = true;
                    //TxtHasta.Visible = true;
                    //LbETDesde.Visible = true;
                    //TxtETDesde.Visible = true;
                    //LbETHasta.Visible = true;
                    //TxtETHasta.Visible = true;
                    //LbTuneles.Visible = true;
                    //TxtTuneles.Visible = true;
                    //LbPasillos.Visible = true;
                    //TxtPasillos.Visible = true;
                    //LbObservaciones.Visible = true;
                    //TxtObservaciones.Visible = true;
                    //LbOK.Visible = true;
                    //TxtOK.Visible = true;
                    //if (TxtForm.Enabled == true)
                    //{
                    //    btnNuevoLote.Visible = true;
                    //    BtDelete.Visible = true;
                    //    BtModifica.Visible = true;
                    //}
                }
            }
            catch (NullReferenceException ex)
            {
                Lberror.Text += ex.Message;
                //alertaErr.Visible = true;
                //TextAlertaErr.Text = ex.Message;

            }
        }

        //Llenando un DropDownList mediante Columnas Combinadas en ASP .NET
        //// Hace referencia al DataTable obtenido
        //DataTable dtAutores = dsAutores.Tables["Authors"];
        //// Crea la nueva DataColumn
        //DataColumn dcNombre = new DataColumn("name", Type.GetType("System.String"));
        //// Establece la sentencia a calcular con las columnas existentes
        //dcNombre.Expression = "au_fname + ' ' + au_lname";
        //  // Agrega la nueva columna al DataTable
        //  dtAutores.Columns.Add(dcNombre);
        //  // Asigna el DataTable Autores como fuente de datos para el DropDownList
        //  ddlComputedColumns.DataSource = dtAutores.DefaultView;
        //  // Asigno el valor a mostrar en el DropDownList
        //  ddlComputedColumns.DataTextField = "name";
        //  // Asigno el valor del value en el DropDownList
        //  ddlComputedColumns.DataValueField = "au_id";
        //  // Llena el DropDownList con los datos
        //  ddlComputedColumns.DataBind();



        //otra
    //    string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(str))
    //    {
    //        con.Open();
    //        using (SqlCommand cmd = new SqlCommand("select * from Info ", con))
    //        {
    //            SqlDataReader rdr = cmd.ExecuteReader();
    //            while (rdr.Read())
    //            {
    //                String text = String.Format("{0}|{1}|{2}\n",
    //                    rdr.GetString(1).PadRight(20, '\u00A0'),
    //                    rdr.GetString(2).PadRight(20, '\u00A0'),
    //                    rdr.GetString(3).PadRight(20, '\u00A0'));
    //                    DropDownList1.Items.Add(text.ToString());
    //            }
    //        }
    //    }

        protected void DrDuplicados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BuscaLote(string SQL)

        }

        protected void DrVariedad_SelectedIndexChanged(object sender, EventArgs e)
        {

            LimpiaCajas();
            btnCancelaLote_Click(sender, e);
            DataTable dt = Main.CargaSecuencia().Tables[0];
            Boolean Esta = false;
            H1Normal.Visible = true;
            H1Seleccion.Visible = false;
            H1Red.Visible = false;
            H1Green.Visible = false;


            try
            {
                if (dt == null)
                {
                    DataTable dt3 = Main.CargaSecuencia().Tables[0];
                    foreach (DataRow fila in dt3.Rows)
                    {
                        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                        {
                            Carga_Impresoras("0");
                            DrPrinters_Click();
                            this.Session["IDSecuencia"] = fila["ZID"].ToString();
                            Carga_Lotes(fila["ZID"].ToString());

                            if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
                            {
                                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
                                txtQRCode.Text = "Seleccione un Tipo de Lote";
                                Esta = true;
                            }
                            else
                            {
                                LbCodigoLote.Text = "CÓDIGO LOTE:";
                                txtQRCode.Text = "";
                                btNew.Enabled = true;
                                Esta = true;
                            }
                            //Carga_Impresoras("0");
                            break;
                        }
                    }
                }
                else
                {
                    foreach (DataRow fila in dt.Rows)
                    {
                        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
                        {
                            Carga_Impresoras(fila["ZID"].ToString());
                            DrPrinters_Click();
                            this.Session["IDSecuencia"] = fila["ZID"].ToString();
                            Carga_Lotes(fila["ZID"].ToString());
                            Carga_LotesScaneados(fila["ZID"].ToString());

                            if (fila["ZDESCRIPCION"].ToString() == "Seleccione un tipo de lote...")
                            {
                                LbCodigoLote.Text = "SIN CÓDIGO LOTE";
                                txtQRCode.Text = "Seleccione un Tipo de Lote";
                                Esta = true;
                            }
                            else
                            {
                                LbCodigoLote.Text = "CÓDIGO LOTE:";
                                txtQRCode.Text = "";
                                btNew.Enabled = true;
                                Esta = true;
                            }
                            //Carga_Impresoras(fila["ZID"].ToString());
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                alertaErr.Visible = true;
                TextAlertaErr.Text = Ex.Message;
                btNew.Enabled = false;
            }

            if (Esta == false)
            {
                txtQRCode.Text = "Seleccione un Tipo de Lote";
                btNew.Enabled = false;
            }

            //H1Normal.Visible = true;
            //DrPrinters_Click();

        }

        protected void BtnLanzaPro_Click(object sender, EventArgs e)
        {
            DBHelper.ExecuteNonQueryProcedure("");
            //mensaje
            if (Variables.mensajeserver != "")
            {
                alertaErr.Visible = true;
                TextAlertaErr.Text = Variables.mensajeserver;
                Variables.mensajeserver = "";
                return;
            }

            try
            {
                DBHelper.MigraProcedure("");
            }
            catch (Exception ex)
            {
                alertaErr.Visible = true;
                TextAlertaErr.Text = Variables.mensajeserver + " -> " + ex.Message;
                Variables.mensajeserver = "";
                return;
            }

            if(Variables.mensajeserver != "")
            {
                alertaErr.Visible = true;
                TextAlertaErr.Text = Variables.mensajeserver;
                Variables.mensajeserver = "";
            }
            else
            {
                alerta.Visible = true;
                TextAlerta.Text = "Los datos están en la tabla de GoldenSoft preparados para importar.";
                Variables.mensajeserver = "";
            }
        }

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

        //protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string SQL = "SELECT * FROM ZBANDEJAS  ";
        //    DataTable dbP = Main.BuscaLote(SQL).Tables[0];

        //    //string SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //    SQL = "SELECT * FROM ZENTRADA WHERE LOTE = '" + DrScaneados.SelectedItem.Text + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    if (dbA.Rows.Count == 0)
        //    {
        //        SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
        //        dbA = Main.BuscaLote(SQL).Tables[0];

        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            LbIDLote.Text = filas["ZID"].ToString();
        //            txtQRCode.Text = filas["ZLOTE"].ToString();
        //            TxtID.Text = filas["ZLOTE"].ToString();
        //            TxtFecha.Text = filas["ZFECHA"].ToString();
        //            LbDateForm.Text = filas["SendTime"].ToString();
        //            TxtVariedad.Text = "";
        //            TxtCajas.Text = "";
        //            TxtPlantas.Text = "";
        //            LbCampoS.Text = "";
        //            LbFechaS.Text = "";
        //            LbVariedadS.Text = "";
        //            LbCajasS.Text = "";
        //            LbPlantasS.Text = "";
        //            Lbcompleto.Text = "";
        //            LbPlantaS.Text = "";
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
        //            DrPrinters_Click();

        //            btnGenerate_Click(sender, e);
        //            alerta.Visible = false;
        //            alertaErr.Visible = false;
        //            btnPrint2.Visible = false;
        //            //BTerminado.Visible = false;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            alertaLog.Visible = false;


        //            //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
        //            //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

        //            //Btfin.Visible = false;
        //            //BTerminado.Visible = false;
        //            break;
        //        }
        //    }
        //    else
        //    {
        //        foreach (DataRow filas in dbA.Rows)
        //        {
        //            this.Session["IDSecuencia"] = filas["ID"].ToString();
        //            LbIDLote.Text = filas["ID"].ToString();
        //            txtQRCode.Text = filas["LOTE"].ToString();
        //            TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
        //            TxtFecha.Text = filas["FECHA"].ToString();
        //            TxtVariedad.Text = filas["VARIEDAD"].ToString();
        //            TxtCajas.Text = filas["UNIDADES"].ToString();
        //            TxtEstado.Text = filas["ESTADO"].ToString();
        //            TxtDispositivo.Text = filas["DeviceName"].ToString();
        //            TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
        //            LbDateForm.Text = filas["SendTime"].ToString();
        //            if (TxtCajas.Text == "CAJAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            if (TxtCajas.Text == "PLANTAS")
        //            {
        //                //LbnumeroPlantas.Text = "Número de Plantas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }

        //            try
        //            {
        //                foreach (DataRow fila2 in dbP.Rows)
        //                {
        //                    if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //                    {
        //                        if (filas["UNIDADES"].ToString() == "PLANTAS")
        //                        {
        //                            LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                            break;
        //                        }
        //                        else if (filas["UNIDADES"].ToString() == "CAJAS")
        //                        {
        //                            if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                            {
        //                                LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                            }
        //                            else
        //                            {
        //                                foreach (DataRow fila3 in dbP.Rows)
        //                                {
        //                                    if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                                    {
        //                                        int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //                                        LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
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
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //            LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
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

        //            //if (TxtID.Enabled == true)
        //            //{
        //            Oculta_Datos(1);
        //            //}
        //            //else
        //            //{
        //            //    Oculta_Datos(0);
        //            //}

        //            H1Normal.Visible = false;
        //            H1Seleccion.Visible = false;
        //            H1Red.Visible = true;
        //            H1Green.Visible = false;

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
        //            btnPrint2.Visible = false;
        //            btProcesa.Visible = false;
        //            btPorcesa.Visible = false;
        //            //Btfin.Visible = false;
        //            //BTerminado.Visible = false;
        //            string Miro = filas["ESTADO"].ToString();
        //            //if (filas["ESTADO"].ToString() == "")
        //            //{
        //            //    BTerminado.Visible = true;
        //            //}
        //            //else
        //            //{
        //            //    Btfin.Visible = true;
        //            //}

        //            //SQL = "DELETE FROM ZLOTESCREADOS WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //            SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
        //            DBHelper.ExecuteNonQuery(SQL);
        //            Carga_Lotes(this.Session["IDSecuencia"].ToString());

        //            break;
        //        }
        //    }
        //    btNew.Enabled = true;
        //}



        protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];

            //string SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
            SQL = "SELECT * FROM ZENTRADA WHERE LOTE = '" + DrScaneados.SelectedItem.Text + "' ";
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            if (dbA.Rows.Count == 0)
            {
                SQL = "SELECT * FROM ZLOTESCREADOS WHERE ZID = '" + DrScaneados.SelectedItem.Value + "' ";
                dbA = Main.BuscaLote(SQL).Tables[0];

                foreach (DataRow filas in dbA.Rows)
                {
                    LbIDLote.Text = filas["ZID"].ToString();
                    txtQRCode.Text = filas["ZLOTE"].ToString();
                    TxtID.Text = filas["ZLOTE"].ToString();
                    TxtFecha.Text = filas["ZFECHA"].ToString();
                    LbDateForm.Text = filas["SendTime"].ToString();
                    TxtVariedad.Text = "";
                    TxtCajas.Text = "";
                    TxtPlantas.Text = "";
                    LbCampoS.Text = "";
                    LbFechaS.Text = "";
                    LbVariedadS.Text = "";
                    LbCajasS.Text = "";
                    LbPlantasS.Text = "";
                    Lbcompleto.Text = "";
                    LbPlantaS.Text = "";
                    TxtEstado.Text = "";
                    TxtDispositivo.Text = "";
                    TxtLoteDestino.Text = "";

                    TxtForm.Text = "";
                    TxtManojos.Text = "";
                    TxtDesde.Text = "";
                    TxtHasta.Text = "";
                    TxtETDesde.Text = "";
                    TxtETHasta.Text = "";
                    TxtTuneles.Text = "";
                    TxtPasillos.Text = "";
                    TxtObservaciones.Text = "";
                    TxtOK.Text = "";
                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = true;
                    H1Green.Visible = false;
                    DrPrinters_Click();

                    btnGenerate_Click(sender, e);
                    alerta.Visible = false;
                    alertaErr.Visible = false;
                    btnPrint2.Visible = false;
                    //BTerminado.Visible = false;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    alertaLog.Visible = false;


                    //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                    //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

                    //Btfin.Visible = false;
                    //BTerminado.Visible = false;
                    break;
                }
            }
            else
            {
                foreach (DataRow filas in dbA.Rows)
                {
                    this.Session["IDSecuencia"] = filas["ID"].ToString();
                    LbIDLote.Text = filas["ID"].ToString();
                    txtQRCode.Text = filas["LOTE"].ToString();
                    TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
                    TxtFecha.Text = filas["FECHA"].ToString();
                    TxtVariedad.Text = filas["VARIEDAD"].ToString();
                    TxtCajas.Text = filas["UNIDADES"].ToString();
                    TxtEstado.Text = filas["ESTADO"].ToString();
                    TxtDispositivo.Text = filas["DeviceName"].ToString();
                    TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
                    LbDateForm.Text = filas["SendTime"].ToString();
                    if (TxtCajas.Text == "CAJAS")
                    {
                        //LbnumeroPlantas.Text = "Número de Cajas:";
                        LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //filas["UNIDADES"].ToString();
                        LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

                    }
                    if (TxtCajas.Text == "PLANTAS")
                    {
                        //LbnumeroPlantas.Text = "Número de Plantas:";
                        LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
                        LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                    }

                    try
                    {
                        foreach (DataRow fila2 in dbP.Rows)
                        {
                            if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
                            {
                                if (filas["UNIDADES"].ToString() == "PLANTAS")
                                {
                                    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                                    break;
                                }
                                else if (filas["UNIDADES"].ToString() == "CAJAS")
                                {
                                    if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
                                    {
                                        LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
                                    }
                                    else
                                    {
                                        foreach (DataRow fila3 in dbP.Rows)
                                        {
                                            if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                                            {
                                                int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                                                LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Lberror.Text = ex.Message;
                    }

                    TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
                    LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
                    LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
                    LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
                    LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
                    //LbCajasS.Text = "Nº CAJAS: " + filas["UNIDADES"].ToString();
                    //LbPlantasS.Text = "Nº PLANTAS: " + filas["NUM_UNIDADES"].ToString();
                    Lbcompleto.Text = "QR COMPLETO";

                    TxtID.Text = filas["ID"].ToString();
                    TxtForm.Text = filas["TIPO_FORM"].ToString();
                    TxtManojos.Text = filas["MANOJOS"].ToString();
                    TxtDesde.Text = filas["DESDE"].ToString();
                    TxtHasta.Text = filas["HASTA"].ToString();
                    TxtETDesde.Text = filas["ETDESDE"].ToString();
                    TxtETHasta.Text = filas["ETHASTA"].ToString();
                    TxtTuneles.Text = filas["TUNELES"].ToString();
                    TxtPasillos.Text = filas["PASILLOS"].ToString();
                    TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
                    TxtOK.Text = filas["OK"].ToString();

                    //if (TxtID.Enabled == true)
                    //{
                        Oculta_Datos(1);
                    //}
                    //else
                    //{
                    //    Oculta_Datos(0);
                    //}

                    H1Normal.Visible = false;
                    H1Seleccion.Visible = false;
                    H1Red.Visible = true;
                    H1Green.Visible = false;

                    //HLoteProceso.InnerText = "Código QR YA ASIGNADO, PENDIENTE PROCESAR";
                    //HLoteProceso.Attributes.Add("style", "color: red; font-weight:bold;");

                    alerta.Visible = false;
                    alertaErr.Visible = false;
                    alertaLog.Visible = false;

                    btnGenerate_Click(sender, e);
                    if (DrPrinters.SelectedItem.Value == "4")
                    {
                        btnGeneraTodoPerf_Click(sender, e);
                    }
                    else
                    {
                        btnGenerateTodo_Click(sender, e);
                    }
                    //btnGenerateTodo_Click(sender, e);
                    btnPrint2.Visible = false;
                    btProcesa.Visible = false;
                    btPorcesa.Visible = false;
                    //Btfin.Visible = false;
                    //BTerminado.Visible = false;
                    string Miro = filas["ESTADO"].ToString();
                    //if (filas["ESTADO"].ToString() == "")
                    //{
                    //    BTerminado.Visible = true;
                    //}
                    //else
                    //{
                    //    Btfin.Visible = true;
                    //}

                    //SQL = "DELETE FROM ZLOTESCREADOS WHERE ZLOTE = '" + txtQRCode.Text + "'";
                    SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
                    DBHelper.ExecuteNonQuery(SQL);
                    Carga_Lotes(this.Session["IDSecuencia"].ToString());

                    break;
                }
            }
            btNew.Enabled = true;
        }

        protected void DrLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];


            SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + DrLotes.SelectedItem.Value + "' ";
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow filas in dbA.Rows)
            {
                LbIDLote.Text = filas["ID"].ToString();


                txtQRCode.Text = filas["LOTE"].ToString();
                TxtCampo.Text = filas["TIPO_PLANTA"].ToString();
                TxtFecha.Text = filas["FECHA"].ToString();
                TxtVariedad.Text = filas["VARIEDAD"].ToString();
                TxtCajas.Text = filas["UNIDADES"].ToString();
                TxtEstado.Text = filas["ESTADO"].ToString();
                TxtDispositivo.Text = filas["DeviceName"].ToString();
                TxtLoteDestino.Text = filas["LOTEDESTINO"].ToString();
                LbDateForm.Text = filas["SendTime"].ToString();
                if (TxtCajas.Text == "CAJAS")
                {
                   // LbnumeroPlantas.Text = "Número de Cajas:";
                    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " +  filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
                    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

                }
                if (TxtCajas.Text == "PLANTAS")
                {
                    //LbnumeroPlantas.Text = "Número de Plantas:";
                    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
                    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                }

                TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
                LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
                LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
                LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
                LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();
                //LbCajasS.Text = "Nº CAJAS: " + filas["UNIDADES"].ToString();
                //LbPlantasS.Text = "Nº PLANTAS: " + filas["NUM_UNIDADES"].ToString();
                Lbcompleto.Text = "QR COMPLETO";
                try
                {
                    foreach (DataRow fila2 in dbP.Rows)
                    {
                        //string AA = fila2["ZTIPO_PLANTA"].ToString();
                        //string CC = fila2["ZTIPO_FORMATO"].ToString();
                        //string FF = fila2["ZNUMERO_PLANTAS"].ToString();

                        //string BB = filas["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
                        //string DD = filas["UNIDADES"].ToString();//CAJAS
                        //string EE = filas["NUM_UNIDADES"].ToString();//40
                        //string GG = filas["MANOJOS"].ToString();//3

                        if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
                        {
                            if (filas["UNIDADES"].ToString() == "PLANTAS")
                            {
                                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
                                break;
                            }
                            else if (filas["UNIDADES"].ToString() == "CAJAS")
                            {
                                if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
                                {
                                    LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();                           
                                }
                                else
                                {
                                    foreach (DataRow fila3 in dbP.Rows)
                                    {
                                        if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
                                        {
                                            int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
                                            LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lberror.Text = ex.Message;
                }

                TxtID.Text = filas["ID"].ToString();
                TxtForm.Text = filas["TIPO_FORM"].ToString();
                TxtManojos.Text = filas["MANOJOS"].ToString();
                TxtDesde.Text = filas["DESDE"].ToString();
                TxtHasta.Text = filas["HASTA"].ToString();
                TxtETDesde.Text = filas["ETDESDE"].ToString();
                TxtETHasta.Text = filas["ETHASTA"].ToString();
                TxtTuneles.Text = filas["TUNELES"].ToString();
                TxtPasillos.Text = filas["PASILLOS"].ToString();
                TxtObservaciones.Text = filas["OBSERVACIONES"].ToString();
                TxtOK.Text = filas["OK"].ToString();

                //if(TxtID.Enabled == true)
                //{
                    Oculta_Datos(1);
                //}
                //else
                //{
                //    Oculta_Datos(0);
                //}
                alerta.Visible = false;
                alertaErr.Visible = false;
                alertaLog.Visible = false;

                btnGenerate_Click(sender, e);
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(sender, e);
                }
                else
                {
                    btnGenerateTodo_Click(sender, e);
                }
                //btnGenerateTodo_Click(sender, e);
                btnPrint2.Visible = true;
                btProcesa.Visible = false;
                btPorcesa.Visible = false;
                //Btfin.Visible = false;
                //BTerminado.Visible = false;
                string Miro = filas["ESTADO"].ToString();
                //if (filas["ESTADO"].ToString() == "")
                //{
                //    BTerminado.Visible = true;
                //}
                //else
                //{
                //    Btfin.Visible = true;
                //}

                break;
            }
            btNew.Enabled = true;

        }

        protected void btPrinter_Click(object sender, EventArgs e)
        {
            dvDrlist.Visible = true;
            dvPrinters.Visible = false;
            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                btnGenerate_Click(sender, e);
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(sender, e);
                }
                else
                {
                    btnGenerateTodo_Click(sender, e);
                }
                //btnGenerateTodo_Click(sender, e);
            }
            else
            {
                LimpiaCajas();
                alertaErr.Visible = false;

            }
        }

        protected void DrPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Miro = DrPrinters.SelectedItem.Value;
            Printers(DrPrinters.SelectedItem.Value);
            PlaceHolderFito.Controls.Clear();
            //Carga Los listados de lotes
            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                if (pnlContentsFT.Visible == true)
                {
                    PlaceHolderFito.Controls.Add(new LiteralControl("<iframe src='/Templates/Factura.html'  style='height:100%; width:100%; border:0px;'></iframe>"));
                }
                else
                {
                    btnGenerate_Click(sender, e);
                    if (LbID.Text != "")
                    {
                        if (DrPrinters.SelectedItem.Value == "4")
                        {
                            btnGeneraTodoPerf_Click(sender, e);
                        }
                        else
                        {
                            btnGenerateTodo_Click(sender, e);
                        }
                        //btnGenerateTodo_Click(sender, e);
                    }
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

        private void DrPrinters_Click()
        {
            btnPrintA1.Visible = false;
            btnPrintB1.Visible = false;
            btnPrintC1.Visible = false;
            btnPrintA2.Visible = false;
            btnPrintB2.Visible = false;
            btnPrintC2.Visible = false;
            btnPrintA3.Visible = false;
            btnPrintB3.Visible = false;
            btnPrintC3.Visible = false;
            btnPrintA4.Visible = false;
            btnPrintB4.Visible = false;
            btnPrintC4.Visible = false;

            if (H1Normal.Visible == true)
            {
                if (DrPrinters.SelectedItem.Value == "1")
                {
                    btnPrintA1.Visible = true;
                    btnPrintB1.Visible = false;
                    btnPrintC1.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB1.Visible = true;
                    btnPrintA1.Visible = false;
                    btnPrintC1.Visible = false;
                }
                else
                {
                    btnPrintB1.Visible = false;
                    btnPrintA1.Visible = false;
                    btnPrintC1.Visible = true;
                }
            }

            if (H1Seleccion.Visible == true)
            {
                if (DrPrinters.SelectedItem.Value == "1")
                {
                    btnPrintA2.Visible = true;
                    btnPrintB2.Visible = false;
                    btnPrintC2.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB2.Visible = true;
                    btnPrintA2.Visible = false;
                    btnPrintC2.Visible = false;
                }
                else
                {
                    btnPrintB2.Visible = false;
                    btnPrintA2.Visible = false;
                    btnPrintC2.Visible = true;
                }
            }
            if (H1Red.Visible == true)
            {
                if (DrPrinters.SelectedItem.Value == "1")
                {
                    btnPrintA3.Visible = true;
                    btnPrintB3.Visible = false;
                    btnPrintC3.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB3.Visible = true;
                    btnPrintA3.Visible = false;
                    btnPrintC3.Visible = false;
                }
                else
                {
                    btnPrintB3.Visible = false;
                    btnPrintA3.Visible = false;
                    btnPrintC3.Visible = true;
                }
            }
            if (H1Green.Visible == true)
            {
                if (DrPrinters.SelectedItem.Value == "1")
                {
                    btnPrintA4.Visible = true;
                    btnPrintB4.Visible = false;
                    btnPrintC4.Visible = false;
                }
                else if (DrPrinters.SelectedItem.Value == "2")
                {
                    btnPrintB4.Visible = true;
                    btnPrintA4.Visible = false;
                    btnPrintC4.Visible = false;
                }
                else
                {
                    btnPrintB4.Visible = false;
                    btnPrintA4.Visible = false;
                    btnPrintC4.Visible = true;
                }
            }

            //Carga_Lotes(this.Session["IDSecuencia"].ToString());
            //Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
        }

        private void Printers(string ID)
        {
            if (ID == "1")
            {
                pnlContents.Visible = true;
                pnlContentsQR.Visible = false;
                pnlContentsFT.Visible = false;
            }
            else if (ID == "2")
            {
                pnlContents.Visible = false;
                pnlContentsQR.Visible = true;
                pnlContentsFT.Visible = false;
            }
            else if (ID == "3")
            {
                pnlContents.Visible = false;
                pnlContentsQR.Visible = false;
                pnlContentsFT.Visible = true;
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
                Response.Redirect("thEnd.aspx");
            }
        }
    }
}