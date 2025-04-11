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
using ZXing;
using System.Drawing.Imaging;
using System.Configuration;

namespace Satelite
{
    public partial class LotesAutoManualS : System.Web.UI.Page
    {
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
                    Server.Transfer("Inicio.aspx"); //Default
                }

                if (!IsPostBack)
                {
                    this.Session["IDSecuencia"] = "0";
                    //this.Session["DESARROLLO"] = "0";
                    this.Session["SelectQR"] = "0";
                    this.Session["QR"] = "";
                    this.Session["Cerrados"] = "0";
                    this.Session["Campos"] = "0";
                    this.Session["CodeQR"] = "";
                    this.Session["CancelaConsulta"] = "";

                    //De mmomento ZENTRADA hasta generar los Menus
                    if (this.Session["IndexPage"].ToString() == "0")
                    {
                        this.Session["IndexPage"] = "23";
                    }
                    
                    DataTable dtCampos = null;
                    DataTable dt = Main.CargaCampos().Tables[0];
                    this.Session["Campos"] = dt;

                    this.Session["idarchivo"] = this.Session["IndexPage"].ToString();

                    string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                    SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
                    DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

                    dtCampos = Relaciones(Convert.ToInt32(this.Session["idarchivo"].ToString()), dt);
                    CreaGridControl(dtArchivos, dtCampos);
                    Carga_tablaControl(dtArchivos, dtCampos);

                    //Select top 10 Id, Fecha, Servicio,CM, Valor
                    //From TuTabla
                    //Order by(Fecha) DESC,(CM)DESC

                    if (txtQRCode.Text == "")
                    {
                        Nueva_Secuencia();
                    }
                    this.Session["IDLote"] = "0";
                    Carga_Impresoras("0");
                    Campos_ordenados();
                    Habilita_Boton(0);
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
                        //Lberror.Text += ex.Message;
                        string a = ex.Message;
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
                string a = ex.Message;
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
            //Habilita_contoles();
            if (this.Session["MiMenu"].ToString() == "OrdenCompra")
            {
                //ContentPlaceHolder cont = new ContentPlaceHolder();
                //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                //HtmlGenericControl li = (HtmlGenericControl)cont.FindControl("Pag1");
                //li.Visible = false;
                //HtmlGenericControl li = (HtmlGenericControl)FindControl("Content1");
                //li.Visible = false;
                //this.Session["MiMenu"] = "";
            }
            else
            {
                //ContentPlaceHolder cont = new ContentPlaceHolder();
                //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                //HtmlGenericControl li = (HtmlGenericControl)FindControl("NavMenu");
                //li.Visible = true;
            }


            dvPrinters.Visible = true;
            dvDrlist.Visible = false;
        }

        protected void FindLote_Click(object sender, EventArgs e)
        {
            Filtra_Lotes(this.Session["IDSecuencia"].ToString());
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

            btnGenerate_Click(null, null);
            //if (DrPrinters.SelectedItem.Value == "4")
            //{
            //    btnGeneraTodoPerf_Click(sender, e);
            //}
            //else
            //{
            //    btnGenerateTodo_Click(sender, e);
            //}

        }


        private void Campos_ordenados()
        {
            this.Session["SelectQR"] = ConfigurationManager.AppSettings.Get("CRcode");
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

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            Server.Transfer("RecoNomina.aspx");
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
                        SQL = " SELECT DISTINCT(B.ZLOTE) AS LOTE, A.ID, A.LOTEDESTINO, A.ESTADO, B.ZID_SECUENCIA, A.TIPO_FORM,  A.LOTE + ' (' + A.TIPO_FORM + ')    ' + A.LOTEDESTINO  AS TODO  ";
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

            lbBuscaCodEscaneado.Text = "Códigos QR creados / escaneados ; " + dbA.Rows.Count + "";


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
                        LbSecuenciaLote.Text = Cadena;
                    }
                    else
                    {
                        LbSecuenciaLote.Text = Cadena;
                    }

                    btnGenerate_Click(null, null);

                    LbCodigoLote.Text = "CÓDIGO LOTE:";
                    LbCodeQRPalteAlv.Text = Cadena;  //"CÓDIGO LOTE:";

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

        private void MiOpenMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(0);
        }
        private void MiCloseMenu()
        {
            ((Satelite.Default)this.Page.Master).InputMenus(1);
        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;


            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
            }
            MiOpenMenu();
        }
        protected void btnDeleteTabla_Click(object sender, EventArgs e)
        {
            //Elimina de ZLOTESCREADOS el Lote
            string SQL = "DELETE FROM ZLOTESCREADOS WHERE ZID = '" + LbIDLote.Text + "' ";
            DBHelper.ExecuteNonQuery(SQL);
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            //Modificacion Gloria
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

        protected void checkSi_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Elimina_Borrados();
            Page.MaintainScrollPositionOnPostBack = true;
            MiOpenMenu();


        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            windowmessaje.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
            btnCancelaLote_Click(null,null);
            MiOpenMenu();

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
                //string targetId = Page.Request.Params.Get("__EVENTTARGET");
                Page.MaintainScrollPositionOnPostBack = false;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('btNew').focus()", true);
                MiCloseMenu();

                return;
            }
            //txtQRCodebis.Visible = false;
            txtQRCode.Visible = true;

            alerta.Visible = false;
            alertaErr.Visible = false;
            alertaLog.Visible = false;

            //BtGuardaLote.Visible = true;
            //BtCancelaLote.Enabled = true;
            //BtModifica.Visible = false;
            //BtModifica.Enabled = false;
            //BtDelete.Enabled = false;
            Habilita_Boton(2);
            Habilita_contoles();

            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
            }
        }

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
                //string targetId = Page.Request.Params.Get("__EVENTTARGET");
                Page.MaintainScrollPositionOnPostBack = true;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('btNew').focus()", true);
                MiCloseMenu();

                return;
            }
            //txtQRCodebis.Visible = false;
            txtQRCode.Visible = true;


            Lbmensaje.Text = "Se eliminará el Lote " + txtQRCode.Text + " de la base de datos.¿Desea Continuar?";
            windowmessaje.Visible = true;
            cuestion.Visible = true;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
            MiCloseMenu();



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
            this.Session["IDLote"] = "0";
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
        }

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

        protected void btnCancelaLote_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn != null && btn.ID == "BtCancelaLote")
            {
                if (this.Session["CancelaConsulta"].ToString() == "DrVariedad")
                {
                    DrVariedad_SelectedIndexChanged(null, null);
                }
                else if (this.Session["CancelaConsulta"].ToString() == "DrLotes")
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

            Habilita_Boton(1);

            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtModifica.Enabled = true;
            //BtCancelaLote.Enabled = false;
            //BtDelete.Enabled = false;
            //BtDelete.Enabled = true;
            Deshabilita_contoles();
            //DrLotes_SelectedIndexChanged(null, null);

            //btnNuevoLote.Visible = true;
            //BtGuardaLote.Visible = false;
            //BtModifica.Visible = true;
            //BtCancelaLote.Visible = false;
            //btGeneraNew.Visible = false;
            //BtDelete.Visible = true;
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

        //protected void btnCancelaLote_Click(object sender, EventArgs e)
        //{
        //    alerta.Visible = false;
        //    alertaErr.Visible = false;
        //    alertaLog.Visible = false;

        //    BtGuardaLote.Visible = false;
        //    BtModifica.Visible = true;
        //    BtCancelaLote.Enabled = false;
        //    BtDelete.Enabled = true;
        //    Deshabilita_contoles();
        //    DrLotes_SelectedIndexChanged(null, null);


        //}

        //protected void btnCancelaLote_Click(object sender, EventArgs e)
        //{
        //    btnNuevoLote.Visible = true;
        //    BtGuardaLote.Visible = false;
        //    BtModifica.Visible = true;
        //    BtCancelaLote.Visible = false;
        //    btGeneraNew.Visible = false;
        //    BtDelete.Visible = true;
        //}

        private void Repara_Fecha( string Fecha)
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
                                    if((dt.Columns.Count - 1) < i) { i += 1; }
                                    
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
                if(SQLUpdate != "" && SQL != "" && SQLWhere != "")
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
            //Si viene con salto de linea, verificar de donde sale ahora esta incidencia
            code = code.Replace("\r\n", "");
            if (code == "") { return; }
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

            PlaceHolder2.Controls.Add(imgBarCode);

            this.Session["QR"] = code;
            ReadQRCode();
        }

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

                btnGenerate_Click(sender, e);
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

                btnGenerate_Click(sender, e);

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
                    //antes estado = 1
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

            //Modificación gloria 14/06/2023
            Carga_Lotes(this.Session["IDSecuencia"].ToString());
            Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
            
            DrPrinters_Click();
            //Carga_Lotes(DrVariedad.SelectedItem.Value);
            //Carga_LotesScaneados(DrVariedad.SelectedItem.Value);
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

        //        btnGenerate_Click(sender, e);
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

        //        btnGenerate_Click(sender, e);
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
            btnGenerate_Click(sender, e);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(sender, e);
            }
            else
            {
                btnGenerateTodo_Click(sender, e);
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
                    LbSecuenciaLote.Text = "";
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
                    LbSecuenciaLote.Text = txtQRCode.Text;
                    LbCodigoLote.Text = "CÓDIGO LOTE:";

                    string SQL = "INSERT INTO ZLOTESCREADOS (ZLOTE, ZFECHA, ZESTADO, ZID_SECUENCIA) ";
                    SQL += " VALUES ('" + txtQRCode.Text + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "',0," + DrVariedad.SelectedItem.Value + ")";
                    DBHelper.ExecuteNonQuery(SQL);

                    btnGenerate_Click(sender, e);
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
            //PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
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
                PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                BodyQR.Attributes.Add("style", "background-color: white;");
                Habilita_Boton(0);
                //BtModifica.Enabled = true;
                txtQRCodeManu.Text = "";
            }
            else
            {
                BtMasCodeManu.Visible = false;
                LbQR.Text = "Búsqueda de códigos QR para modificar";
                BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                BodyQR.Attributes["style"] = "background-color: #e9f5ef;";
                Habilita_Boton(0);
                //BtModifica.Enabled = false;
                txtQRCodeManu.Text = "";
            }
        }

        protected void checkListas_Click(object sender, EventArgs e)
        {
            alerta.Visible = false;
            ImageButton btn = (ImageButton)sender;
            if (this.Session["Cerrados"].ToString() == "1")
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
                BodyQR.Attributes.Add("style", "background-color: white;");
                PanelQR.Attributes["style"] = "min-height:420px; background-color: white;";
                Habilita_Boton(1);
                //BtModifica.Enabled = true;
                txtQRCodeManu.Text = "";
                //BodyAll.Attributes.Add("style", "background-color: white;");
            }
            else 
            {
                lbFiltro.Visible = true;
                dvFiltro.Visible = true;
                ImgFiltro.Visible = true;
                Imageprocesa1.Visible = true; 
                Imageprocesa2.Visible = false;
                this.Session["Cerrados"] = "1";
                DrVariedad_SelectedIndexChanged(null, null);
                //if (Permisos == "1") { BtMasCodeManu.Visible = true; } else { BtMasCodeManu.Visible = false; }
                BtMasCodeManu.Visible = false;
                LbQR.Text = "Búsqueda de códigos QR para modificar";
                BodyLote.Attributes["style"] = "background-color: #e9f5ef;";
                BodyCampos.Attributes["style"] = "background-color: #e9f5ef;";
                BodyLotes.Attributes["style"] = "background-color: #e9f5ef;";
                BodyQR.Attributes["style"] = "background-color: #e9f5ef;";
                PanelQR.Attributes["style"] = "min-height:420px; background-color: #e9f5ef;";
                Habilita_Boton(0);
                //BtModifica.Enabled = false;
                txtQRCodeManu.Text = "";
                //BodyAll.Attributes["style"] = "background-color: #e9f5ef;";
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
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

            LbSecuenciaLote.Text = txtQRCode.Text;
            LbSecuenciaLoteQR.Text = txtQRCode.Text;
            LbCodeQRPalteAlv.Text = txtQRCode.Text;


            //ajustar a  LM Q H deberá ser desde configuración en tabla
            string code = txtQRCode.Text.Trim();// + " ";
            LbSecuenciaLote.Text = code;

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
                ImgQRCodeA1.Visible = true;
                ImgQRCodeA2.Visible = false;
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
                ImgQRCodeA1.Visible = false;
                ImgQRCodeA2.Visible = true;
            }

            //PlaceHolder1.Controls.Add(imgBarCode);
            PlaceHolder1.Visible = false;
            PlaceHolderQR.Visible = false;
            pnlContentsFT.Visible = false;
            PlaceHolderPaletAlv.Visible = false;

            //Aqui va una plantilla única para todos, comentado a cliente fase 3
            if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            {
                PlaceHolder1.Controls.Add(imgBarCode);
                PlaceHolder1.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "2")
            {
                PlaceHolderQR.Controls.Add(imgBarCode);
                PlaceHolderQR.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "3")
            {
                pnlContentsFT.Controls.Add(imgBarCode);
                pnlContentsFT.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "6")
            {
                PlaceHolderPaletAlv.Controls.Add(imgBarCode);
                PlaceHolderPaletAlv.Visible = true;
            }
            //Aqui verifica si la imagen es correcta
            //imgBarCode




            







            LbCodigoLote.Text = "CÓDIGO LOTE:";
            this.Session["QR"] = code;
            ReadQRCode();

            //Comentar en produccion
            //btnGenerateTodo_Click(sender, e);

        }

        protected void btnGenerateTodo_Click(object sender, EventArgs e)
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
                LbCodePaletAlv.Text = "";
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
                if(code == "") { return; }
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

                    PlaceHolderPaletAlvMin.Controls.Add(imgBarCode);
                }
                else
                {
                    PlaceHolder2.Controls.Add(imgBarCode);
                }
                this.Session["QR"] = code;
                ReadQRCode();
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

                LbCampoS.Text = "";
                LbFechaS.Text = "";
                LbPlantaS.Text = "";
                LbVariedadS.Text = "";
                LbCajasS.Text = "";
                LbPlantasS.Text = "";
                Lbcompleto.Text = "";
                LbSecuenciaLote.Text = "";
                LbCodePaletAlv.Text = "";
                LbCodeQRPalteAlv.Text = "";
                LbTipoPlantaP.Text = "";
                LbVariedadP.Text = "";
                LbVariedadS.Text = "";
                lbUnidadesP.Text = "";
                lbNumPlantasP.Text = "";
                TxtID.Text = "";
                LbCodigoLote.Text = "SIN CÓDIGO LOTE";

            }
            catch (NullReferenceException ex)
            {
                //Lberror.Text += ex.Message;
                string a = Main.Ficherotraza("Limpia Cajas-->" + ex.Message);
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
                    LimpiaCajas();
                }
                else
                {
                    if (TxL0.Visible== true && TxL0.ReadOnly == false)
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

            TextAlerta.Text = "";
            TextAlertaErr.Text = "";
            alerta.Visible = false;
            btProcesa.Visible = false;
            btProcesa.Visible = false;
            btPorcesa.Visible = false;

            this.Session["CancelaConsulta"] = "DrVariedad";
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
                            LbCodigoLote.Text = "SIN CÓDIGO LOTE";
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
                            LbCodigoLote.Text = "CÓDIGO LOTE:";
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
                    LbSecuenciaLote.Text = "";
                    LbCodigoLote.Text = "SIN CÓDIGO LOTE";
                    txtQRCode.Text = "";
                    alerta.Visible = false;
                    alertaErr.Visible = false;
                    alertaLog.Visible = false;
                    LbCampoS.Text = "";
                    LbFechaS.Text = "";
                    LbPlantaS.Text = "";
                    LbVariedadS.Text = "";
                    LbCajasS.Text = "";
                    LbPlantasS.Text = "";
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

            if (Variables.mensajeserver != "")
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

        protected void DrScaneados_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["IDLote"] = "1";
            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];
            //string Unidad_Base = "";
            LimpiaCajas();
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
                //Por aqui no calcula
                foreach (DataRow filas in dbA.Rows)
                {
                    LbIDLote.Text = filas["ZID"].ToString();
                    txtQRCode.Text = filas["ZLOTE"].ToString();
                    //txtQRCodebis.Text = filas["ZLOTE"].ToString();
                    TxtID.Text = filas["ZLOTE"].ToString();

                    lbFechaLote.Text = "Creación: " + filas["ZFECHA"].ToString();
                    //TxtFecha.Text = filas["ZFECHA"].ToString();
                    //TxtVariedad.Text = "";
                    //TxtCajas.Text = "";
                    //TxtPlantas.Text = "";
                    LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);
                    LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
                    LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); // //  filas["SendTime"].ToString().Substring(10);
                    LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //// filas["SendTime"].ToString().Substring(10);

                    if (DrPrinters.SelectedItem.Value == "6")
                    {
                        LbCodeQRPalteAlv.Text = filas["ZID"].ToString();
                        LbTipoPlantaP.Text = "";
                        LbVariedadP.Text = "";
                        LbVariedadS.Text = "";
                        lbUnidadesP.Text = "";
                        lbNumPlantasP.Text = "";
                    }
                    else
                    {
                        LbCampoS.Text = "";
                        LbFechaS.Text = "";
                        LbVariedadS.Text = "";
                        LbCajasS.Text = "";
                        LbPlantasS.Text = "";
                        Lbcompleto.Text = "";
                        LbPlantaS.Text = "";
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


                    btnGenerate_Click(sender, e);
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
            //btNew.Enabled = true;
            Habilita_Boton(0);
            //BtGuardaLote.Enabled = false;
            //BtModifica.Enabled = false;
            //BtCancelaLote.Enabled = false;
            //BtDelete.Enabled = false;

        }

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
        //    foreach (DataRow filas in dbA.Rows)
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
        //        LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            LbCodeQRPalteAlv.Text = filas["LOTE"].ToString();
        //            LbTipoPlantaP.Text = "";
        //            LbVariedadP.Text = "";
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


        //            //if (TxtCajas.Text == "CAJAS")
        //            //{
        //            //    lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //            //    lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            //}
        //            //if (TxtCajas.Text == "PLANTAS")
        //            //{
        //            //    lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //            //    lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //}
        //            //if (TxtCajas.Text == "BANDEJAS")
        //            //{
        //            //    lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString(); filas["UNIDADES"].ToString();
        //            //    lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //}

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


        //            //LbTipoPlantaP.Text = "Tipo Planta: " + filas["UNIDADES"].ToString() + " " + filas["TIPO_PLANTA"].ToString(); // "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con is System.DBNull)
        //            {
        //                LbVariedadP.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //            }
        //            else
        //            {
        //                LbVariedadP.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con == null)
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
        //            //if (TxtCajas.Text == "CAJAS")
        //            //{
        //            //    // LbnumeroPlantas.Text = "Número de Cajas:";
        //            //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //            //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            //}
        //            //if (TxtCajas.Text == "PLANTAS")
        //            //{
        //            //    //LbnumeroPlantas.Text = "Número de Plantas:";
        //            //    LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //            //    LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //}

        //            LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
        //            LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
        //            LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString();
        //            LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();

        //            Lbcompleto.Text = "QR COMPLETO";
        //        }


        //        try
        //        {
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





















        //                    //if (filas["UNIDADES"].ToString() == "PLANTAS")
        //                    //{
        //                    //    if (DrPrinters.SelectedItem.Value == "6")
        //                    //    {
        //                    //        lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                    //        //value = 1234567890.123456;
        //                    //        Double Value = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());
        //                    //        lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                    //        // Displays 1,234,567,890.1
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                    //    }
        //                    //    break;
        //                    //}
        //                    //else if (filas["UNIDADES"].ToString() == "CAJAS")
        //                    //{
        //                    //    if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                    //    {
        //                    //        if (DrPrinters.SelectedItem.Value == "6")
        //                    //        {
        //                    //            string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //            lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //            Double Value = Convert.ToDouble(Cuantos);
        //                    //            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //        }

        //                    //        //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        foreach (DataRow fila3 in dbP.Rows)
        //                    //        {
        //                    //            if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                    //            {
        //                    //                int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());

        //                    //                if (DrPrinters.SelectedItem.Value == "6")
        //                    //                {
        //                    //                    string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                    lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                    Double Value = Convert.ToDouble(Cuantos);
        //                    //                    lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                    //                }
        //                    //                else
        //                    //                {
        //                    //                    LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                }
        //                    //                //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                break;
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //    break;
        //                    //}
        //                    //else if (filas["UNIDADES"].ToString() == "BANDEJAS")
        //                    //{
        //                    //    if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                    //    {
        //                    //        if (DrPrinters.SelectedItem.Value == "6")
        //                    //        {
        //                    //            string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //            lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //            Double Value = Convert.ToDouble(Cuantos);
        //                    //            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //        }
        //                    //        //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        foreach (DataRow fila3 in dbP.Rows)
        //                    //        {
        //                    //            if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //                    //            {
        //                    //                int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());

        //                    //                if (DrPrinters.SelectedItem.Value == "6")
        //                    //                {
        //                    //                    string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                    lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                    Double Value = Convert.ToDouble(Cuantos);
        //                    //                    lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                    //                }
        //                    //                else
        //                    //                {
        //                    //                    LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                }
        //                    //                //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                    //                break;
        //                    //            }
        //                    //        }
        //                    //    }
        //                    //    break;
        //                    //}
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
            Object Con = null;

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
                        if(DivRegistro.Visible == true)
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
                                        LbCampoS.Text = "Movimiento Desde: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "TIPO_PLANTA")
                                    {
                                        TmpLbPlantaS = "Tipo Planta: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantaS.Text = "Tipo Planta: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "FECHA")
                                    {
                                        TmpLbFechaS = "Fecha: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbFechaS.Text = "Fecha: " + filas[dt.Columns[i]].ToString();
                                    }                                   
                                    if (dt.Columns[i].ColumnName == "UNIDADES")
                                    {
                                        TmpLbCajasS = "Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCajasS.Text = "Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "NUM_UNIDADES")
                                    {
                                        TmpLbPlantasS = "Número de Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantasS.Text = "Número de Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "VARIEDAD")
                                    {
                                        TmpLbVariedadS = "Variedad: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbVariedadS.Text = "Variedad: " + filas[dt.Columns[i]].ToString();                                       
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
                                        LbCampoS.Text = "Movimiento Desde: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "TIPO_PLANTA")
                                    {
                                        TmpLbPlantaS = "Tipo Planta: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantaS.Text = "Tipo Planta: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "FECHA")
                                    {
                                        TmpLbFechaS = "Fecha: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbFechaS.Text = "Fecha: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "UNIDADES")
                                    {
                                        TmpLbCajasS = "Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbCajasS.Text = "Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "NUM_UNIDADES")
                                    {
                                        TmpLbPlantasS = "Número de Unidades: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbPlantasS.Text = "Número de Unidades: " + filas[dt.Columns[i]].ToString();
                                    }
                                    if (dt.Columns[i].ColumnName == "VARIEDAD")
                                    {
                                        TmpLbVariedadS = "Variedad: " + filas[dt.Columns[i]].ToString() + Environment.NewLine;
                                        LbVariedadS.Text = "Variedad: " + filas[dt.Columns[i]].ToString();
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
                LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
                LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
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

                LbCodeQRPalteAlv.Text = filas["LOTE"].ToString();
                LbTipoPlantaP.Text = "";
                LbVariedadP.Text = "";
                lbUnidadesP.Text = "";
                lbNumPlantasP.Text = "";

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "' AND UD_BASE IS NOT NULL AND UD_BASE <> '' ", null);

                if (Con == null)
                {
                    //mensaje
                    Lbmensaje.Text = "No se encuentra el registro de la tabla ZPLANTA_TIPO_VARIEDAD_CODGOLDEN para Tipo Planta '" + filas["TIPO_PLANTA"].ToString() + "' y Variedad " + filas["VARIEDAD"].ToString() + "'.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    Unidad_Base = "";
                    MiCloseMenu();

                }
                else
                {
                    Unidad_Base = Con.ToString();
                    double Dato = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());
                    lbUnidadesP.Text = Dato.ToString("N0") + " " + filas["UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
                    LbCajasS.Text = lbUnidadesP.Text;
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

                                lbNumPlantasP.Text = (Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(Resto) / Convert.ToInt32(Res)).ToString("N0") + " " + Unidad_Base; // / 1000 + " " + Unidad_Base;
                                LbPlantasS.Text = lbNumPlantasP.Text;

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
          
                                            LbPlantasS.Text =  String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".")  + " " + Unidad_Base;
                                            lbNumPlantasP.Text = LbPlantasS.Text;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

                if (Con == null)
                {
                    LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
                    //mensaje
                    Lbmensaje.Text = "No se encuentra la tabla ZTIPOPLANTADESCRIP para la Variedad " + filas["TIPO_PLANTA"].ToString() ;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                }
                else
                {
                    LbTipoPlantaP.Text = "Tipo Planta: " + Con;
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

                if (Con == null)
                {
                    LbVariedadP.Text = "Variedad: " + filas["VARIEDAD"].ToString();
                    //mensaje
                    Lbmensaje.Text = "No se encuentra el registro de la tabla ZEMPRESAVARIEDAD para la variedad " + filas["VARIEDAD"].ToString() ;
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                }
                else
                {
                    LbVariedadP.Text = "Variedad: " + Con;
                }

                Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

                if (Con == null)
                {
                    //VIVA: Viveros Valsaín, SLU
                    //VRE: Viveros Río Eresma, SLU
                    LbCodePaletAlv.Text = "";
                    Lbmensaje.Text = "No se encuentra el registro de la tabla ZEMPRESAVARIEDAD para la variedad " + filas["VARIEDAD"].ToString();
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

                        LbCodePaletAlv.Text = "Viveros Valsaín, SLU";
                    }
                    else
                    {
                        LbCodePaletAlv.Text = "Viveros Río Eresma, SLU";
                    }
                }
                Lbcompleto.Text = "QR COMPLETO";

                LbCampoS.Text = "CAMPO O SECTOR: " + filas["DESDE"].ToString();
                LbPlantaS.Text = "TIPO PLANTAS: " + filas["TIPO_PLANTA"].ToString();
                LbFechaS.Text = "FECHA CORTE: " + filas["FECHA"].ToString().Substring(0,10);
                LbVariedadS.Text = "VARIEDAD: " + filas["VARIEDAD"].ToString();

                Oculta_Datos(1);

                alerta.Visible = false;
                alertaErr.Visible = false;
                alertaLog.Visible = false;
                //int Runidades = 0;

                Con = DBHelper.ExecuteScalarSQL("SELECT ZMANUAL FROM  ZSECUENCIAS  WHERE ZID ='" + DrVariedad.SelectedItem.Value + "'", null);

                if (Con == null)
                {
                    LbCodePaletAlv.Text = "";
                    Lbmensaje.Text = "No se encuentra valor para ZMANUAL (0,1) en la Tabla ZSECUENCIAS para completar valores en el código QR según selección de Lotes.";
                    cuestion.Visible = false;
                    Asume.Visible = true;
                    windowmessaje.Visible = true;
                    MiCloseMenu();

                }
                else
                {

                }

                //Pinto Todo

                if (DrPrinters.SelectedItem.Value == "6" )
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
                    btnGenerate_Click(null, null);
                }
                //
                if (DrPrinters.SelectedItem.Value == "4")
                {
                    btnGeneraTodoPerf_Click(null, null);
                }
                else
                {
                    btnGenerateTodo_Click(null, null);
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

                //11/04/2022 (comentado) En descripcion del lote 

                string Miro = filas["ESTADO"].ToString();
                SQL = "UPDATE ZLOTESCREADOS SET ZESTADO = -1 WHERE ZLOTE = '" + txtQRCode.Text + "'";
                DBHelper.ExecuteNonQuery(SQL);

                //Carga_Lotes(this.Session["IDSecuencia"].ToString());
                Carga_LotesScaneados(this.Session["IDSecuencia"].ToString());
                Posiciona_Permiso();
                break;
            }
        }


        //private void CargaDescripcionLote(DataTable dt)
        //{
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
        //        LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
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

        //        if (DrPrinters.SelectedItem.Value == "6")
        //        {
        //            LbCodeQRPalteAlv.Text = filas["LOTE"].ToString();
        //            LbTipoPlantaP.Text = "";
        //            LbVariedadP.Text = "";
        //            lbUnidadesP.Text = "";
        //            lbNumPlantasP.Text = "";

        //            Object Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (UD_BASE) FROM  ZPLANTA_TIPO_VARIEDAD_CODGOLDEN  WHERE ZTIPO_PLANTA ='" + filas["TIPO_PLANTA"].ToString() + "' AND UD_BASE IS NOT NULL AND UD_BASE <> '' ", null);

        //            if (Con is null)
        //            {
        //                //seguimos como está.
        //                Unidad_Base = "";
        //            }
        //            else
        //            {
        //                Unidad_Base = Con.ToString();
        //            }

        //            //Modificar a Tablas de validación
        //            if (TxtCajas.Text != "PLANTAS")
        //            {
        //                if (Unidad_Base != "")
        //                {
        //                    lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();

        //                    foreach (DataRow filbase in dbBase.Rows)
        //                    {
        //                        if (filas["UNIDADES"].ToString() == filas["UD_BASE"].ToString()) // "GRAMOS")
        //                        {
        //                            Esta = true;
        //                            lbNumPlantasP.Text = "Cantidad: " + Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) / Convert.ToInt32(filas["FACT_DIV"].ToString()) + Unidad_Base; // / 1000 + " " + Unidad_Base;
        //                        }

        //                    }
        //                    if (Esta == false)
        //                    {
        //                        lbNumPlantasP.Text = "Cantidad: " + Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) + " " + Unidad_Base;
        //                    }

        //                    //lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                    //if(filas["UNIDADES"].ToString() == TxtCajas.Text) // "GRAMOS")
        //                    //{
        //                    //    lbNumPlantasP.Text = "Cantidad: " + Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) / 1000 + " " + Unidad_Base;
        //                    //}
        //                    //else
        //                    //{
        //                    //    lbNumPlantasP.Text = "Cantidad: " + Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) + " " + Unidad_Base;
        //                    //}
        //                }
        //                else
        //                {
        //                    lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                    lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                }

        //            }
        //            else
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }

        //            TxtPlantas.Text = filas["NUM_UNIDADES"].ToString();
        //            //LbTipoPlantaP.Text = "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPTIPO) FROM  ZTIPOPLANTADESCRIP  WHERE ZTIPO_PLANTA = '" + filas["TIPO_PLANTA"].ToString() + "'", null);

        //            if (Con is null)
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + filas["TIPO_PLANTA"].ToString();
        //                //mensaje
        //                Lbmensaje.Text = "No se encuentra la Variedad " + filas["TIPO_PLANTA"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPTIPO con TIPO_PLANTA en la tabla ZTIPOPLANTADESCRIP.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //            }
        //            else
        //            {
        //                LbTipoPlantaP.Text = "Tipo Planta: " + Con;
        //            }

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            //if (Con is System.DBNull)
        //            if (Con is null)
        //            {
        //                LbVariedadP.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //                //mensaje
        //                Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZDESCRIPCION con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //            }
        //            else
        //            {
        //                LbVariedadP.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            //if (Con is System.DBNull)
        //            if (Con is null)
        //            {
        //                //VIVA: Viveros Valsaín, SLU
        //                //VRE: Viveros Río Eresma, SLU
        //                LbCodePaletAlv.Text = "";
        //                Lbmensaje.Text = "No se encuentra la Variedad " + filas["VARIEDAD"].ToString() + " en Lotes, para relacionar el campo ZEMPRESA con VARIEDAD en la tabla ZEMPRESAVARIEDAD.";
        //                cuestion.Visible = false;
        //                Asume.Visible = true;
        //                DvPreparado.Visible = true;
        //                //mensaje
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
        //        { //Resto de impresoras
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
        //            foreach (DataRow fila2 in dbP.Rows)
        //            {
        //                AA = fila2["ZTIPO_PLANTA"].ToString();
        //                CC = fila2["ZTIPO_FORMATO"].ToString();
        //                BB = filas["TIPO_PLANTA"].ToString();

        //                DD = filas["UNIDADES"].ToString();//CAJAS
        //                EE = filas["NUM_UNIDADES"].ToString();//40

        //                if (fila2["ZTIPO_PLANTA"].ToString() == BB && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //                {
        //                    if (filas["UNIDADES"].ToString() == "PLANTAS")
        //                    {
        //                        if (DrPrinters.SelectedItem.Value == "6")
        //                        {
        //                            lbNumPlantasP.Text = "Cantidad: " + filas["NUM_UNIDADES"].ToString() + " PLANTAS";
        //                            //lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                            //value = 1234567890.123456;
        //                            Double Value = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());
        //                            lbNumPlantasP.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";
        //                            // Displays 1,234,567,890.1
        //                        }
        //                        else
        //                        {
        //                            LbPlantasS.Text = "Cantidad: " + filas["NUM_UNIDADES"].ToString() + " PLANTAS";
        //                        }
        //                        break;
        //                    }
        //                    else
        //                    {//Si es distinto a plantas
        //                        if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //                        {
        //                            if (DrPrinters.SelectedItem.Value == "6")
        //                            {
        //                                if (Unidad_Base != "")
        //                                {
        //                                    if (filas["UNIDADES"].ToString() != Unidad_Base)
        //                                    {
        //                                        Esta = false;
        //                                        foreach (DataRow filbase in dbBase.Rows)
        //                                        {
        //                                            if (filas["UNIDADES"].ToString() == filas["UD_BASE"].ToString()) // "GRAMOS")
        //                                            {
        //                                                Esta = true;
        //                                                lbNumPlantasP.Text = "Cantidad: " + Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) / Convert.ToInt32(filas["FACT_DIV"].ToString()) + Unidad_Base; // / 1000 + " " + Unidad_Base;
        //                                            }

        //                                        }
        //                                        if (Esta == false)
        //                                        {
        //                                            string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) / 1000).ToString();
        //                                            lbNumPlantasP.Text = "Cantidad: " + Cuantos + " " + Unidad_Base;
        //                                        }

        //                                        ////string Cuantos = ((Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) / 1000).ToString();
        //                                        //string Cuantos = ((Convert.ToDouble(filas["NUM_UNIDADES"].ToString()) * Convert.ToDouble(fila2["ZNUMERO_PLANTAS"].ToString()))).ToString();
        //                                        //if (filas["UNIDADES"].ToString() == "GRAMOS")
        //                                        ////if (filas["UNIDADES"].ToString() == TxtCajas.Text)
        //                                        //{
        //                                        //    //lbNumPlantasP.Text = "Cantidad: " + Convert.ToDouble(Cuantos)  + " " + Unidad_Base;
        //                                        //    lbNumPlantasP.Text = "Cantidad: " + (Convert.ToDouble(Cuantos) / 1000) + " " + Unidad_Base;
        //                                        //}
        //                                        //else
        //                                        //{
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
        //                                    string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                    lbNumPlantasP.Text = "Cantidad: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString() + " PLANTAS";
        //                                    Double Value = Convert.ToDouble(Cuantos);
        //                                    lbNumPlantasP.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";

        //                                    //string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                    //lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //                                    //Double Value = Convert.ToDouble(Cuantos);
        //                                    //lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                LbPlantasS.Text = "Cantidad: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString() + " PLANTAS";
        //                            }
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
        //                                        if (Unidad_Base != "")
        //                                        {
        //                                            if (filas["UNIDADES"].ToString() != "Unidad_Base")
        //                                            {
        //                                                string Cuantos = filas["NUM_UNIDADES"].ToString(); // Convert.ToInt32(Unidad_Base)).ToString();
        //                                                lbNumPlantasP.Text = "Cantidad: " + filas["NUM_UNIDADES"].ToString() + " " + Unidad_Base;
        //                                                Double Value = Convert.ToDouble(Cuantos);
        //                                                lbNumPlantasP.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                                            }
        //                                            else
        //                                            {
        //                                                lbNumPlantasP.Text = "";
        //                                            }
        //                                        }
        //                                        else
        //                                        {
        //                                            string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                            lbNumPlantasP.Text = "Cantidad: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString() + " PLANTAS";
        //                                            Double Value = Convert.ToDouble(Cuantos);
        //                                            lbNumPlantasP.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";

        //                                            //string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                            //lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                            //Double Value = Convert.ToDouble(Cuantos);
        //                                            //lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        //LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //                                        LbPlantasS.Text = "Cantidad: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString() + " PLANTAS";
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
        //            string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
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

        //        Oculta_Datos(1);

        //        alerta.Visible = false;
        //        alertaErr.Visible = false;
        //        alertaLog.Visible = false;
        //        int Runidades = 0;


        //        if (DrVariedad.SelectedItem.Value == "10") //en revision es 7
        //        {
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
        //                            lbNumPlantasP.Text = "Cantidad: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".") + " PLANTAS";
        //                            //lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
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
        //                        string a = Main.Ficherotraza("DRLotes_SelectedIndexChanged-->" + ex.Message);
        //                    }
        //                }
        //            }
        //        }

        //        if(this.Session["IDLote"].ToString() == "1")
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


        protected void DrLotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Session["IDLote"] = "1";
            string SQL = "SELECT * FROM ZBANDEJAS  ";
            DataTable dbP = Main.BuscaLote(SQL).Tables[0];
            //string AA = "";
            //string CC = "";
            //string BB = "";
            //string DD = "";
            //string EE = "";
            //string FF = "";
            this.Session["CancelaConsulta"] = "DrLotes";
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
        }

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

        //    SQL = "SELECT * FROM ZENTRADA  WHERE ID = '" + DrLotes.SelectedItem.Value + "' ";
        //    DataTable dbA = Main.BuscaLote(SQL).Tables[0];
        //    foreach (DataRow filas in dbA.Rows)
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
        //        LbDateFT.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateContents.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDateQR.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);
        //        LbDatePaletAlv.Text = DateTime.Now.ToString("HH:mm:ss"); //filas["SendTime"].ToString().Substring(10);

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
        //            else if (TxtCajas.Text == "PLANTAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }
        //            else if (TxtCajas.Text == "BANDEJAS")
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString(); filas["UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            }
        //            else 
        //            {
        //                lbUnidadesP.Text = "Unidades: " + filas["UNIDADES"].ToString();
        //                //lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //                lbNumPlantasP.Text = "Nº Unidades: " + filas["NUM_UNIDADES"].ToString();
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


        //            //LbTipoPlantaP.Text = "Tipo Planta: " + filas["UNIDADES"].ToString() + " " + filas["TIPO_PLANTA"].ToString(); // "Tipo Planta: BANDEJAS " + filas["TIPO_PLANTA"].ToString();

        //            string N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZDESCRIPCION) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con is System.DBNull)
        //            {
        //                LbVariedadP.Text = "Variedad: " + filas["VARIEDAD"].ToString();
        //            }
        //            else
        //            {
        //                LbVariedadP.Text = "Variedad: " + Con;
        //            }

        //            N = "";
        //            Con = DBHelper.ExecuteScalarSQL("SELECT TOP 1 (ZEMPRESA) FROM  ZEMPRESAVARIEDAD  WHERE ZVARIEDAD ='" + filas["VARIEDAD"].ToString() + "'", null);

        //            if (Con is System.DBNull)
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
        //            if (TxtCajas.Text == "CAJAS")
        //            {
        //                // LbnumeroPlantas.Text = "Número de Cajas:";
        //                LbCajasS.Text = "Unidades: " + filas["UNIDADES"].ToString() + " " + filas["NUM_UNIDADES"].ToString(); //+ filas["UNIDADES"].ToString();
        //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();

        //            }
        //            if (TxtCajas.Text == "PLANTAS")
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
        //            //foreach (DataRow fila2 in dbP.Rows)
        //            //{
        //            //    if (fila2["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila2["ZTIPO_FORMATO"].ToString() == filas["UNIDADES"].ToString())
        //            //    {
        //            //        if (filas["UNIDADES"].ToString() == "PLANTAS")
        //            //        {
        //            //            if (DrPrinters.SelectedItem.Value == "6")
        //            //            {
        //            //                lbNumPlantasP.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //                //value = 1234567890.123456;
        //            //                Double Value = Convert.ToDouble(filas["NUM_UNIDADES"].ToString());
        //            //                lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //            //                // Displays 1,234,567,890.1
        //            //            }
        //            //            else
        //            //            {
        //            //                LbPlantasS.Text = "Nº Plantas: " + filas["NUM_UNIDADES"].ToString();
        //            //            }
        //            //            break;
        //            //        }
        //            //        else if (filas["UNIDADES"].ToString() == "CAJAS")
        //            //        {
        //            //            if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //            //            {
        //            //                if (DrPrinters.SelectedItem.Value == "6")
        //            //                {
        //            //                    string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                    lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                    Double Value = Convert.ToDouble(Cuantos);
        //            //                    lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //            //                }
        //            //                else
        //            //                {
        //            //                    LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                }
        //            //            }
        //            //            else
        //            //            {
        //            //                foreach (DataRow fila3 in dbP.Rows)
        //            //                {
        //            //                    if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //            //                    {
        //            //                        if (DrPrinters.SelectedItem.Value == "6")
        //            //                        {
        //            //                            string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                            lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                            Double Value = Convert.ToDouble(Cuantos);
        //            //                            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());
        //            //                            LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                        }
        //            //                        break;
        //            //                    }
        //            //                }
        //            //            }
        //            //            break;
        //            //        }
        //            //        else if (filas["UNIDADES"].ToString() == "BANDEJAS")
        //            //        {
        //            //            if (filas["MANOJOS"].ToString() == "0" || filas["MANOJOS"].ToString() == "" || filas["MANOJOS"].ToString() == null)
        //            //            {
        //            //                if (DrPrinters.SelectedItem.Value == "6")
        //            //                {
        //            //                    string Cuantos = (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                    lbNumPlantasP.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                    Double Value = Convert.ToDouble(Cuantos);
        //            //                    lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //            //                }
        //            //                else
        //            //                {
        //            //                    LbPlantasS.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //                }
        //            //                //LbPlantasSQR.Text = "Nº Plantas: " + (Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())).ToString();
        //            //            }
        //            //            else
        //            //            {
        //            //                foreach (DataRow fila3 in dbP.Rows)
        //            //                {
        //            //                    if (fila3["ZTIPO_PLANTA"].ToString() == filas["TIPO_PLANTA"].ToString() && fila3["ZTIPO_FORMATO"].ToString() == "MANOJOS")
        //            //                    {
        //            //                        int NN = Convert.ToInt32(filas["MANOJOS"].ToString()) * Convert.ToInt32(fila3["ZNUMERO_PLANTAS"].ToString());

        //            //                        if (DrPrinters.SelectedItem.Value == "6")
        //            //                        {
        //            //                            string Cuantos = ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                            lbNumPlantasP.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                            Double Value = Convert.ToDouble(Cuantos);
        //            //                            lbNumPlantasP.Text = "Nº Plantas: " + String.Format(CultureInfo.InvariantCulture, "{0:0,0}", Value).Replace(",", ".");
        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            LbPlantasS.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                        }
        //            //                        //LbPlantasSQR.Text = "Nº Plantas: " + ((Convert.ToInt32(filas["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString())) + NN).ToString();
        //            //                        break;
        //            //                    }
        //            //                }
        //            //            }
        //            //            break;
        //            //        }
        //            //    }
        //            //}
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
        //                    BB = filas["TIPO_PLANTA"].ToString();
        //                //}

        //                //BB = filas["TIPO_PLANTA"].ToString();//Tips produccion (P-TIPS)
        //                DD = filas["UNIDADES"].ToString();//CAJAS
        //                EE = filas["NUM_UNIDADES"].ToString();//40
        //                //GG = filas["MANOJOS"].ToString();//3

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
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Lberror.Text = ex.Message;
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
        //            Oculta_Datos(1);
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
        //         {
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
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                else if (filaCount["UNIDADES"].ToString() == "BANDEJAS")
        //                                {
        //                                    if (filaCount["MANOJOS"].ToString() == "0" || filaCount["MANOJOS"].ToString() == "" || filaCount["MANOJOS"].ToString() == null)
        //                                    {
        //                                        Totales += (Convert.ToInt32(filaCount["NUM_UNIDADES"].ToString()) * Convert.ToInt32(fila2["ZNUMERO_PLANTAS"].ToString()));
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

            }
        }

        protected void DrPrintQR_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList Combo = (DropDownList)sender;
            string Miro = Combo.SelectedItem.Value;


            PlaceHolderFito.Controls.Clear();
            if (DrLotes.SelectedItem.Value != "" || DrScaneados.SelectedItem.Value != "" || txtQRCode.Text != "")
            {
                if (pnlContentsFT.Visible == true)
                {
                    PlaceHolderFito.Controls.Add(new LiteralControl("<iframe src='/Templates/Factura.html'  style='height:100%; width:100%; border:0px;'></iframe>"));
                }
                else
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

        protected void DrPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Miro = DrPrinters.SelectedItem.Value;
            Printers(DrPrinters.SelectedItem.Value);
            PlaceHolderFito.Controls.Clear();
            //this.Session["SelectPrinter"] = "1";
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
                    if (DrPrinters.SelectedItem.Value == "4")
                    {
                        btnGeneraTodoPerf_Click(sender, e);
                    }
                    else
                    {
                        btnGenerateTodo_Click(sender, e);
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

        private void Printers(string ID)
        {
            if (ID == "1" || ID == "4")
            {
                pnlContents.Visible = true;
                pnlContentsQR.Visible = false;
                pnlContentsFT.Visible = false;
                pnlContentsPaletAlv.Visible = false;
            }
            else if (ID == "2")
            {
                pnlContents.Visible = false;
                pnlContentsQR.Visible = true;
                pnlContentsFT.Visible = false;
                pnlContentsPaletAlv.Visible = false;
            }
            else if (ID == "3")
            {
                pnlContents.Visible = false;
                pnlContentsQR.Visible = false;
                pnlContentsFT.Visible = true;
                pnlContentsPaletAlv.Visible = false;
            }
            else if (ID == "6")
            {
                pnlContents.Visible = false;
                pnlContentsQR.Visible = false;
                pnlContentsFT.Visible = false;
                pnlContentsPaletAlv.Visible = true;
            }
        }

        //private void Printers(string ID)
        //{
        //    if (ID == "1")
        //    {
        //        pnlContents.Visible = true;
        //        pnlContentsQR.Visible = false;
        //        pnlContentsFT.Visible = false;
        //    }
        //    else if (ID == "2")
        //    {
        //        pnlContents.Visible = false;
        //        pnlContentsQR.Visible = true;
        //        pnlContentsFT.Visible = false;
        //    }
        //    else if (ID == "3")
        //    {
        //        pnlContents.Visible = false;
        //        pnlContentsQR.Visible = false;
        //        pnlContentsFT.Visible = true;
        //    }
        //}

        private void Carga_Impresoras(string ID)
        {
            string SQL = "";
            try
            {
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
                    //SQL = " SELECT DISTINCT(C.ZID) as IDPRINT, C.ZDESCRIPCION ";
                    //SQL += " FROM ZSECUENCIAS A ";
                    //SQL += " INNER JOIN ZPRINTERFORM B ON A.ZID = B.ZID_SECUENCIA ";
                    //SQL += " INNER JOIN ZPRINTER C ON B.ZID_PRINTER = C.ZID ";
                    //SQL += " WHERE A.ZID = '" + ID + "'";
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
                //Verificar tabla impresoras
                //Response.Redirect("Principal.aspx");
                string a = Main.Ficherotraza("Lee impresoras QR --> " + ex.Message + " --> " + SQL);
                Server.Transfer("Inicio.aspx");

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
                if (btn.ID == "ImgQRCodeA1") { ImgQRCodeA1.Visible = false; ImgQRCodeA2.Visible = true; }
                this.Session["SelectQR"] = "1";
            }
            if (btn.ID == "ImgQRCodeA2" || btn.ID == "ImgQRCodeB2" || btn.ID == "ImgQRCodeC3" || btn.ID == "ImgQRCodeD4")
            {
                if (btn.ID == "ImgQRCodeA2") { ImgQRCodeA2.Visible = false; ImgQRCodeA1.Visible = true; }
                this.Session["SelectQR"] = "0";
            }

            btnGenerate_Click(null, null);
            if (DrPrinters.SelectedItem.Value == "4")
            {
                btnGeneraTodoPerf_Click(null, null);
            }
            else
            {
                btnGenerateTodo_Click(null, null);
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
                    barcodeBitmap.Save(memory, ImageFormat.Png);
                    byte[] bytes = memory.ToArray();
                    //fs.Write(bytes, 0, bytes.Length);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(bytes);
                    imgBarCode.Visible = true;
                    //fs.Close();
                    //imgBarCode.ImageUrl = "~/images/QRImage.jpg";
                }
            }

            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));


            PlaceHolder1.Visible = false;
            PlaceHolderQR.Visible = false;
            pnlContentsFT.Visible = false;
            PlaceHolderPaletAlv.Visible = false;

            if (DrPrinters.SelectedItem.Value == "1" || DrPrinters.SelectedItem.Value == "4")
            {
                PlaceHolder1.Controls.Add(imgBarCode);
                PlaceHolder1.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "2")
            {
                PlaceHolderQR.Controls.Add(imgBarCode);
                PlaceHolderQR.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "3")
            {
                pnlContentsFT.Controls.Add(imgBarCode);
                pnlContentsFT.Visible = true;
            }
            if (DrPrinters.SelectedItem.Value == "6")
            {
                PlaceHolderPaletAlv.Controls.Add(imgBarCode);
                PlaceHolderPaletAlv.Visible = true;
            }
            LbCodigoLote.Text = "CÓDIGO LOTE:";
            this.Session["CodigoQR"] = barcodeBitmap;

            ReadQRCode();

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
                            LBReadQR.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQR.Attributes["sytle"] = "color:red;";
                        }
                        else
                        {
                            LBReadQR.Text = "Resultado lectura QRCoder:  " + result.Text;
                            LBReadQR.Attributes["sytle"] = "color:black;";
                        }
                    }
                    else
                    {
                        if (this.Session["QR"].ToString() != result.Text)
                        {
                            LBReadQR.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQR.Attributes["sytle"] = "color:red;";
                        }
                        else
                        {
                            LBReadQR.Text = "Resultado lectura Zxing QR:  " + result.Text;
                            LBReadQR.Attributes["sytle"] = "color:black;";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string a = Main.Ficherotraza("Lee Imagen QR --> " + ex.Message);
            }
        }
    }
}