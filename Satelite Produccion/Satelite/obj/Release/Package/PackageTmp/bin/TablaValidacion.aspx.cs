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

namespace Satelite
{
    public partial class TablaValidacion : System.Web.UI.Page
    {
        private int registro = 0;
        private int Contador = 0;
        private int Buscaregistro = 0;
        private string[] ListadoArchivos;
        protected System.Web.UI.WebControls.TreeView tvControl;
        public string CampoOrden = "";

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
                    CampoOrden = "ZDESCRIPCION";
                    this.Session["Edicion"] = "0";

                    this.Session["FormatoCampos"] = null;
                    this.Session["idflujo"] = null;
                    this.Session["idestado"] = null;
                    this.Session["TablaObj"] = "";
                    this.Session["TablaName"] = "";
                    this.Session["EstadoCondicion"] = "0";


                    if (this.Session["MiNivel"].ToString() != "9")
                    {
                        Server.Transfer("Inicio.aspx"); //Default
                    }


                    this.Session["IDArchivo"] = 1;
                    //if(this.Session["TablaName"].ToString() != "")
                    //{
                        Actualiza_Archivos("1");
                    //}
                }
            }
            catch (Exception ex)
            {
                string a = Main.ETrazas("", "1", " AltaArchivos.Page_load --> Error:" + ex.Message);
                Server.Transfer("Login.aspx");
            }
        }



        private void Actualiza_Archivos(string Tipo)
        {

            DataTable dt3 = new DataTable();
            dt3 = Main.CargaNivel().Tables[0];
            this.Session["Niveles"] = dt3;


            DrArchivos.Items.Clear();
            DrConexion.Items.Clear();

            DrArchivos.DataValueField = "ZID";
            DrArchivos.DataTextField = "ZDESCRIPCION";

            BtNuevo.Enabled = false;
            BtEdita.Enabled = false;

            DrArchivos.Items.Insert(0, new ListItem("Ninguno", "0"));

            DataTable dt = new DataTable();
            if(Tipo == "1")
            {
                dt = Main.CargaArchivos(2).Tables[0];// TablaValidacion validacion
            }
            else
            {
                dt = Main.CargaArchivos(1).Tables[0];// todas las Tablas
            }
            DrArchivos.DataSource = dt;
            DrArchivos.DataBind();
            this.Session["Archivos"] = dt;


            //Relaciones(1, "");
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

            ddEntradaPageSize.Items.Clear();
            ddEntradaPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddEntradaPageSize.Items.Insert(1, new ListItem("30", "30"));
            ddEntradaPageSize.Items.Insert(2, new ListItem("50", "50"));
            ddEntradaPageSize.Items.Insert(3, new ListItem("Todos", "1000"));


        }

        protected void DrTemplatesTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ListTemplaID.Items.RemoveAt(ListTempla.SelectedIndex);
            //ListTempla.Items.RemoveAt(ListTempla.SelectedIndex);
        }


        protected void btnDeleteArDoc_Click(object sender, EventArgs e)
        {
            ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
        }    

        protected void DrArchivosTemp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ListTemplaListBoxArchivo_DoubleClick(object sender, EventArgs e)
        {

        }
        
        protected void ListBoxArchivo_DoubleClick(object sender, EventArgs e)
        {
           
        }
        protected void ListPlantMarca_DoubleClick(object sender, EventArgs e)
        {
        }
        protected void btnCondicionTemplate_Click(object sender, EventArgs e)
        {
            DivEstados.Visible = true;
        }

        protected void btnguardaTemplate_Click(object sender, EventArgs e)
        {

        }

        protected void btCancela_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "0";
            CajasEnabled(this.Session["Edicion"].ToString());
            BtEdita.Visible = true;
            BtNuevo.Visible = true;
            BtCancela.Enabled = true;
            BtGuarda.Visible = false;
        }
        protected void btNuevo_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "1";
            CajasVacias();
            CajasEnabled(this.Session["Edicion"].ToString());
            BtEdita.Visible = false;
            BtNuevo.Visible = false;
            BtCancela.Enabled = true;
            BtGuarda.Visible = true;
        }

        protected void btEdita_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "2";
            CajasEnabled(this.Session["Edicion"].ToString());
            BtEdita.Visible = false;
            BtNuevo.Visible = false;
            BtCancela.Enabled = true;
            BtGuarda.Visible = true;
        }

        private void CreaConsultaInsercion(string TipoConsulta)
        {
            int AA = 0;
            //int UU = 0;
            string Vista = "";
            string Key = "";
            string MiCampo = "";
            string DatoCampo = "";
            string Tabla = "";
            //string SQL = "";
            string SQLInsercion = "";
            string SQLUpdate = "";

            string SQLConsulta = "";
            string SQLConsultaWhere = "";

            int cuantos = DivCampos0.Controls.Count;

            DataTable dtC = this.Session["Campos"] as DataTable;
            //DataTable dtA = this.Session["Archivos"] as DataTable;
            DataTable dtA = Main.CargaRelacionesArchivos(Convert.ToInt32(this.Session["idarchivo"].ToString())).Tables[0];
            //dtC = dtA;
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

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
                    windowmessaje.Visible = true;
                    MiCloseMenu();

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
                            TextBox DivLabel = cont.FindControl(MiContent) as TextBox;
                            if (DivLabel.Text.Contains("'")) { DivLabel.Text.Replace("'", ""); }
                            //if (DivLabel.Text == "") { DivLabel.Text = "''"; }

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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta = " SELECT " + fila["ZTITULO"].ToString();
                                        }

                                        SQLInsercion = " INSERT INTO " + Tabla + " (" + fila["ZTITULO"].ToString();
                                        //entero o decimal
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate = " VALUES (0";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLUpdate = " VALUES (" + DivDL.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        SQLUpdate = " VALUES (" + DivLabel.Text.Replace(",", ".");
                                                    }
                                                }
                                            }
                                        }
                                        else//Texto, Fecha
                                        {

                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLUpdate = " VALUES ('" + DivDL.SelectedValue + "'";
                                                }
                                                else
                                                {
                                                    SQLUpdate = " VALUES ('" + DivLabel.Text + "'";
                                                }
                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    if (MiCampo == fila["ZTITULO"].ToString())
                                    {
                                        if (fila["ZVALIDACION"].ToString() != "0")
                                        {
                                            string OtroConten = "DivReg" + N;
                                            OtroConten = "DrL" + N; //linea campo
                                            DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                            DatoCampo = DivDL.SelectedValue;
                                        }
                                        else
                                        {
                                            DatoCampo = DivLabel.Text;
                                        }
                                    }
                                    else
                                    {
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta += ", " + fila["ZTITULO"].ToString();
                                        }

                                        SQLInsercion += ", " + fila["ZTITULO"].ToString();
                                        //entero o decimal
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate += ", 0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                SQLUpdate += "," + DivLabel.Text.Replace(",", ".");
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                            }
                                        }
                                        else //Texto, Fechas
                                        {
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLUpdate += ",'" + DivDL.SelectedValue + "'";
                                                }
                                                else
                                                {
                                                    SQLUpdate += ",'" + DivLabel.Text + "'";
                                                }
                                            }
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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta = " SELECT " + fila["ZTITULO"].ToString();
                                        }

                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = 0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = '" + DivDL.SelectedValue + "'";
                                                }
                                                else
                                                {
                                                    SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                                }
                                            }
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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta += ", " + fila["ZTITULO"].ToString();
                                        }
                                        //entero o decimal
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = 0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                                else
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrL" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue;
                                                    }
                                                    else
                                                    {
                                                        SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                                    }
                                                }
                                            }
                                        }
                                        else//Texto, Fechas
                                        {
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
                                            else
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrL" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = '" + DivDL.SelectedValue + "'";
                                                }
                                                else
                                                {
                                                    SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                                }
                                            }
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
                            TextBox DivLabel = cont.FindControl(MiContent) as TextBox;
                            //DivLabel.Text = "";
                            if (DivLabel.Text.Contains("'")) { DivLabel.Text.Replace("'", ""); }
                            //if (DivLabel.Text == "") { DivLabel.Text = "''"; }

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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta = " SELECT " + fila["ZTITULO"].ToString();
                                        }

                                        SQLInsercion = " INSERT INTO " + Tabla + " (" + fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate = " VALUES (0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                SQLUpdate = " VALUES (" + DivLabel.Text.Replace(",", ".");
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrR" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SQLUpdate = " VALUES ('" + DivLabel.Text + "'";
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrR" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta += ", " + fila["ZTITULO"].ToString();
                                        }

                                        SQLInsercion += ", " + fila["ZTITULO"].ToString();
                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate += ",0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                SQLUpdate += "," + DivLabel.Text.Replace(",", ".");
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrR" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SQLUpdate += ",'" + DivLabel.Text + "'";
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrR" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta = " SELECT " + fila["ZTITULO"].ToString();
                                        }

                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = 0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrR" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SQLUpdate = " UPDATE " + Tabla + " SET " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrR" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " WHERE " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere = " WHERE " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
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
                                        if (fila["KEYCAMPO"].ToString() == "1")
                                        {
                                            SQLConsulta += ", " + fila["ZTITULO"].ToString();
                                        }

                                        if (fila["ZTIPO"].ToString() == "2" || fila["ZTIPO"].ToString() == "5")//entero o decimal
                                        {
                                            if (DivLabel.Text == "")
                                            {
                                                SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = 0 ";
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = 0 ";
                                                }
                                            }
                                            else
                                            {
                                                SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".");
                                                if (fila["KEYCAMPO"].ToString() == "1")
                                                {
                                                    if (fila["ZVALIDACION"].ToString() != "0")
                                                    {
                                                        string OtroConten = "DivReg" + N;
                                                        OtroConten = "DrR" + N; //linea campo
                                                        DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                    }
                                                    else
                                                    {
                                                        SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivLabel.Text.Replace(",", ".") + " ";
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SQLUpdate += ", " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "'";
                                            if (fila["KEYCAMPO"].ToString() == "1")
                                            {
                                                if (fila["ZVALIDACION"].ToString() != "0")
                                                {
                                                    string OtroConten = "DivReg" + N;
                                                    OtroConten = "DrR" + N; //linea campo
                                                    DropDownList DivDL = (DropDownList)cont.FindControl(OtroConten);
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = " + DivDL.SelectedValue + " ";
                                                }
                                                else
                                                {
                                                    SQLConsultaWhere += " AND " + fila["ZTITULO"].ToString() + " = '" + DivLabel.Text + "' ";
                                                }
                                            }
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
                    //Pregunta si permite duplicados
                    Boolean Esta = Main.PermitirArchivoDuplicado(dtA);
                    if (Esta == false)
                    {
                    }
                    else
                    {
                        if (SQLConsulta != "")
                        {
                            string SQL = SQLConsulta + " FROM " + Tabla + " " + SQLConsultaWhere;
                            //SQLConsulta += " FROM " + Tabla + " " + SQLConsultaWhere;
                            Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                            if (Con == null)
                            {
                                //No Existe
                            }
                            else
                            {
                                if (TipoConsulta == "1")
                                {
                                    //Si está actualizando es el registro
                                }
                                else
                                {
                                    Lbmensaje.Text = "Está intentando duplicar un registro y este Archivo documental que no lo permite. ";
                                    cuestion.Visible = false;
                                    Asume.Visible = true;
                                    windowmessaje.Visible = true;
                                    MiCloseMenu();

                                    return;
                                }
                            }
                        }
                        else
                        {
                            Lbmensaje.Text = "El Archivo documental no permite duplicados, pero no tiene definidas Columnas para las Keys únicas de validación de registro. ";
                            cuestion.Visible = false;
                            Asume.Visible = true;
                            windowmessaje.Visible = true;
                            MiCloseMenu();

                            return;
                        }
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
                this.Session["IDGridA"] = "";
            }
            catch (Exception mm)
            {
                //string a = Main.Ficherotraza("Carga_TablaEmpleados --> " + mm.Message);
                string a = Main.ETrazas(SQLUpdate, "1", " CreaConsultaInsercion -->" + mm.Message);
                Variables.Error = mm.Message;
            }
        }

        protected void btGuarda_Click(object sender, EventArgs e)
        {
            SqlParameter[] dbParams = new SqlParameter[0];

            if (this.Session["Edicion"].ToString() == "1" || this.Session["Edicion"].ToString() == "2")
            {
                if (this.Session["Edicion"].ToString() == "1")
                {
                    //Crea registro
                    CreaConsultaInsercion("0"); //Insercion
                }
                else
                {
                    //Edita flujo
                    CreaConsultaInsercion("1"); //actualizacion


                }
            }

            BtNuevo.Visible = true;
            BtEdita.Visible = true;
            BtGuarda.Visible = false;
            BtCancela.Enabled = false;

            DrArchivos_SelectedIndexChanged(null,null);

        }

        private void Cancelado()
        {
            this.Session["Edicion"] = "0";
        }
        protected void DrSello_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Drroot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
         

        protected void DrArchivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrArchivos.BackColor = Color.FromName("#bdecb6");
            this.Session["idarchivo"] = DrArchivos.SelectedItem.Value;

            Carga_Lotes(null);
            BtNuevo.Enabled = false;
            BtEdita.Enabled = false;

        }



        private void Carga_Lotes(string sortExpression = null)
        {
            string SQL = "";

            DataTable dtCampos = null;
            DataTable dt = Main.CargaCampos().Tables[0];
            this.Session["Campos"] = dt;

            SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + this.Session["idarchivo"].ToString();
            DataTable dtArchivos = Main.BuscaLote(SQL).Tables[0];

            if (dtArchivos.Rows.Count != 0)
            {

                foreach (DataRow filaKey in dtArchivos.Rows)
                {
                    TxtNombre.Text = filaKey["ZID"].ToString();
                    TxtDescripcion.Text = filaKey["ZDESCRIPCION"].ToString();
                    TxTabla.Text = filaKey["ZTABLENAME"].ToString();
                    break;
                }
            }

            dtCampos = RelacionesArchivo(Convert.ToInt32(this.Session["idarchivo"].ToString()), dt);

            this.Session["Campos"] = dtCampos;

            if (dtArchivos.Rows.Count != 0)
            {
                DataTable ds = Main.CargaRelacionesArchivoTval(Convert.ToInt32(this.Session["idarchivo"].ToString())).Tables[0];
                
                DrConexion.Items.Clear();
                DrConexion.DataValueField = "ZID";
                DrConexion.DataTextField = "ZDESCRIPCION";
                DrConexion.DataSource = dt;
                DrConexion.DataBind();

                ListBoxArchivo.Items.Clear();
                ListBoxArchivo.DataValueField = "ZID";
                ListBoxArchivo.DataTextField = "ZDESCRIPCION";
                Boolean Esta = false;

                if (this.Session["EstadoCondicion"].ToString() == "0")
                {
                    for (int i = 0; i <= DrConexion.Items.Count - 1; i++)
                    {
                        foreach (DataRow fila in dtCampos.Rows)
                        {
                            if (fila["ZDESCRIPCION"].ToString() == DrConexion.Items[i].Text)
                            {
                                SQL = "SELECT DISTINCT(A.ZID) as ZID, A.ZDESCRIPCION FROM ZARCHIVOS A, ZARCHIVOCAMPOS B ";
                                SQL += " WHERE B.ZIDCAMPO = " + fila["ZID"].ToString();
                                dt = Main.BuscaLote(SQL).Tables[0];
                                ListBoxArchivo.DataSource = dt;
                                ListBoxArchivo.DataBind();
                                DrConexion.SelectedIndex = i;
                                Esta = true;
                                break;
                            }
                        }
                        if (Esta == true)
                        {
                            break;
                        }
                    }
                }
            }

            if (this.Session["TablaName"].ToString() == "" || this.Session["TablaName"].ToString() == "Sin Tabla")
            {
                gvEntrada.DataSource = null;
                gvEntrada.DataBind();
            }
            else 
            { 
                SQL = " SELECT * FROM  " + this.Session["TablaName"];
                dt = Main.BuscaLote(SQL).Tables[0];
                if (dt.Rows.Count != 0)
                {
                    //CreaGridControl(dtArchivos, dtCampos);
                    CreaGridMiControl(dtArchivos, dtCampos);
                }
                gvEntrada.DataSource = dt;
                gvEntrada.DataBind();
                lbRowEntrada.Text = dt.Rows.Count.ToString(); //color:olivedrab ;
            }

        }

        private void CreaGridControl(DataTable dtArchivo, DataTable dtCampo)
        {
            //int i =  Convert.ToInt32(this.Session["NumeroPalet"].ToString());
            //Para dinamico Me.controls.item(contador).visible = false

            gvEntrada.AutoGenerateColumns = false;
            //DropListaColumna.Items.Clear();
            //DropListaColumna.DataValueField = "ZTITULO";
            //DropListaColumna.DataTextField = "ZDESCRIPCION";
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
                    if (filas["ZTITULO"].ToString() == "ZID")
                    {
                        gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = filas["ZTITULO"].ToString(), DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });
                    }
                    else 
                    { 
                        BoundField Campo = new BoundField();
                        Campo.DataField = filas["ZTITULO"].ToString();
                        Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                        DataControlField DataControlField = Campo;
                        gvEntrada.Columns.Add(DataControlField);
                        //DropListaColumna.Items.Insert(i, new ListItem(filas["ZDESCRIPCION"].ToString(), filas["ZTITULO"].ToString()));
                    }
                    i += 1;
                }
            }

            //Lberror.Text = "";
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

        }

        protected void DrFindL0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList btn = (DropDownList)sender;
            string Miro = btn.ID.Replace("DrFindL", "");
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl DivCampos0 = (HtmlGenericControl)cont.FindControl("DivCampos0");
            if (btn.SelectedIndex == 0)
            {
                string DivTexto = "TxL" + Miro;
                TextBox DivTextAVer = (DivCampos0.FindControl(DivTexto) as TextBox);
                if (DivTextAVer.Visible == true)
                {
                    DivTextAVer.Attributes["style"] = "width:100%;border-style:inset;background-color:#ffffff;";
                }
                else
                {
                    DivTexto = "DrL" + Miro;
                    DropDownList DivComboVer = (DivCampos0.FindControl(DivTexto) as DropDownList);
                    DivComboVer.Attributes["style"] = "width:100%;background-color:#ffffff;";
                }
            }
            else
            {
                string DivTexto = "TxL" + Miro;
                TextBox DivTextAVer = (DivCampos0.FindControl(DivTexto) as TextBox);
                if (DivTextAVer.Visible == true)
                {
                    DivTextAVer.Attributes["style"] = "width:100%;border-style:inset;background-color:#c8f2f7;";
                }
                else
                {
                    DivTexto = "DrL" + Miro;
                    DropDownList DivComboVer = (DivCampos0.FindControl(DivTexto) as DropDownList);
                    DivComboVer.Attributes["style"] = "width:100%;background-color:#c8f2f7;";
                }
            }

        }
        protected void DrLs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void BtnNewDato_click(object sender, EventArgs e)
        {

        }
        protected void BtnGuardaDato_click(object sender, EventArgs e)
        {

        }
        protected void btnOpenFiles_Click(object sender, EventArgs e)
        {

        }
        protected void BtnModificaDato_Click(object sender, EventArgs e)
        {

        }
        protected void BtnCancela_click(object sender, EventArgs e)
        {

        }
        protected void BtnEliminaDato_click(object sender, EventArgs e)
        {

        }
        
        protected void DrFindR0_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList btn = (DropDownList)sender;
            string Miro = btn.ID.Replace("DrFindR", "");
            ContentPlaceHolder cont = new ContentPlaceHolder();
            cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            HtmlGenericControl DivCampos0 = (HtmlGenericControl)cont.FindControl("DivCampos0");
            if (btn.SelectedIndex == 0)
            {
                string DivTexto = "TxD" + Miro;
                TextBox DivTextAVer = (DivCampos0.FindControl(DivTexto) as TextBox);
                if (DivTextAVer.Visible == true)
                {
                    DivTextAVer.Attributes["style"] = "width:100%;border-style:inset;background-color:#ffffff;";
                }
                else
                {
                    DivTexto = "DrR" + Miro;
                    DropDownList DivComboVer = (DivCampos0.FindControl(DivTexto) as DropDownList);
                    DivComboVer.Attributes["style"] = "width:100%;background-color:#ffffff;";
                }
            }
            else
            {
                string DivTexto = "TxD" + Miro;
                TextBox DivTextAVer = (DivCampos0.FindControl(DivTexto) as TextBox);
                if (DivTextAVer.Visible == true)
                {
                    DivTextAVer.Attributes["style"] = "width:100%;border-style:inset;background-color:#c8f2f7;";
                }
                else
                {
                    DivTexto = "DrR" + Miro;
                    DropDownList DivComboVer = (DivCampos0.FindControl(DivTexto) as DropDownList);
                    DivComboVer.Attributes["style"] = "width:100%;background-color:#c8f2f7;";
                }
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

        private void CajasVacias()
        {
            for (int N = 0; N <= 50; N++)//Hasta 50 campos
            {
                string MiContent = "DivReg" + N;
                ContentPlaceHolder cont = new ContentPlaceHolder();
                cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");//
                HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);

                if (DivColumA.Visible == true)
                {
                    MiContent = "TxL" + N; //linea campo DivTextA0                       
                    TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                    if (DivLabel.Visible == true)
                    {
                        DivLabel.Text ="";
                        MiContent = "DrL" + N; //linea campo
                        DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                        //DivCombo.Text = "";
                    }
                    else
                    {
                        DivLabel.Text = "";
                        MiContent = "DrL" + N; //linea campo
                        DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                        //DivCombo.Text = "";
                    }

                    MiContent = "TxD" + N; //linea campo DivTextA0                       
                    DivLabel = (TextBox)cont.FindControl(MiContent);
                    if (DivLabel.Visible == true)
                    {
                        DivLabel.Text = "";
                        MiContent = "DrR" + N; //linea campo
                        DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                        DivCombo.Visible = false;
                    }
                    else
                    {
                        DivLabel.Text = "";
                        MiContent = "DrR" + N; //linea campo
                        DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                        DivCombo.Visible = true;
                    }
                }
            }
        }
        private void CajasEnabled( string tipo)
        {
            if (tipo != "0") //Si es escritura
            {
                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N;
                    ContentPlaceHolder cont = new ContentPlaceHolder();
                    cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");//
                    HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);

                    if (DivColumA.Visible == true)
                    {
                        MiContent = "TxL" + N; //linea campo DivTextA0                       
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        if (DivLabel.Visible == true)
                        {
                            if(N == 0)
                            {
                                DivLabel.ReadOnly = true;
                            }
                            else
                            {
                                DivLabel.ReadOnly = false;
                            }
                            MiContent = "DrL" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = false;
                            DivCombo.Enabled = false;
                        }
                        else
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrL" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = true;
                            DivCombo.Enabled = true;
                        }

                        MiContent = "TxD" + N; //linea campo DivTextA0                       
                        DivLabel = (TextBox)cont.FindControl(MiContent);
                        if (DivLabel.Visible == true)
                        {
                            DivLabel.ReadOnly = false;
                            MiContent = "DrR" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = false;
                            DivCombo.Enabled = false;
                        }
                        else
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrR" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = true;
                            DivCombo.Enabled = true;
                        }
                    }
                }
            }
            else //Si es 0 //Si es escritura false
            {
                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N;
                    ContentPlaceHolder cont = new ContentPlaceHolder();
                    cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");//
                    HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);

                    if (DivColumA.Visible == true)
                    {
                        MiContent = "TxL" + N; //linea campo DivTextA0                       
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        if (DivLabel.Visible == true)
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrL" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = false;
                            DivCombo.Enabled = false;
                        }
                        else
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrL" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = true;
                            DivCombo.Enabled = false;
                        }

                        MiContent = "TxD" + N; //linea campo DivTextA0                       
                        DivLabel = (TextBox)cont.FindControl(MiContent);
                        if (DivLabel.Visible == true)
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrR" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = false;
                            DivCombo.Enabled = false;
                        }
                        else
                        {
                            DivLabel.ReadOnly = true;
                            MiContent = "DrR" + N; //linea campo
                            DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                            DivCombo.Visible = true;
                            DivCombo.Enabled = false;
                        }
                    }
                }
            }

        }
        private void CreaGridMiControl(DataTable dtArchivo, DataTable dtCampo)
        {

            gvEntrada.Columns.Clear();
            gvEntrada.AutoGenerateColumns = false;
            //controles
            //Según implementacion dinamicos
            //DivCampos0.Controls.Clear();
            //Según implementacion html
            //Variable opcion manual o dinamica desde web.config
            int Manual = 0;
            int cuantos = 0;
            int i = 0;
            int a = 0;
            Boolean Esta = false;

            string data = "";

            if (Manual == 0) //Manual. La variable en web.config
            {
                cuantos = DivCampos0.Controls.Count;


                for (int N = 0; N <= 50; N++)//Hasta 50 campos
                {
                    string MiContent = "DivReg" + N;
                    //HtmlGenericControl DivColumA = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);

                    ContentPlaceHolder cont = new ContentPlaceHolder();
                    cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");//
                    HtmlGenericControl DivColumA = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivColumA.Visible = false;

                    //if (N != 0)
                    //{
                    //    MiContent = "DrFindL" + N; //linea combo DivLabelA0   DrFindL0                   
                    //    DropDownList Divcombo = (DropDownList)cont.FindControl(MiContent);
                    //    Divcombo.Items.Clear();
                    //}

                    MiContent = "DivLabelA" + N; //linea campo   DivFindL0                     
                    HtmlGenericControl DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivCon.Visible = false;

                    MiContent = "DivTextA" + N; //linea campo DivTextA0                       
                    DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivCon.Visible = false;

                    MiContent = "DivLabelB" + N; //linea campo   DivFindL0                     
                    DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                    DivCon.Visible = false;

                    MiContent = "DivTextB" + N; //linea campo DivTextA0                       
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

                    //MiContent = "DrFindR" + N; //linea campo
                    //DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                    //DivCombo.Visible = false;
                    //DivCombo.Items.Clear();

                    DivLabel.Visible = false;
                    MiContent = "TxD" + N; //linea campo                       
                    DivLabel = (TextBox)cont.FindControl(MiContent);
                    DivLabel.Text = "";
                    DivLabel.Visible = false;
                    MiContent = "DrL" + N; //linea campo
                    DropDownList DivCombo = (DropDownList)cont.FindControl(MiContent);
                    DivCombo.Visible = false;
                    DivCombo.Enabled = false;
                    DivCombo.Items.Clear();
                    MiContent = "DrR" + N; //linea campo
                    DivCombo = (DropDownList)cont.FindControl(MiContent);
                    DivCombo.Visible = false;
                    DivCombo.Enabled = false;
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
                                //gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = filas["ZTITULO"].ToString(), DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });
                                gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = filas["ZTITULO"].ToString(), DataTextField = filas["ZTITULO"].ToString(), ImageUrl = "~/Images/sendDown20x20.png" });
                                MiCampo = filas["ZTITULO"].ToString();
                                gvEntrada.DataKeyNames = new string[] { filas["ZTITULO"].ToString() };
                                Esta = true;
                                break;
                            }
                        }
                        if (Esta == false)
                        {
                            gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", HeaderText = "ID", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });
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



                //foreach (DataRow filas in dtCampo.Rows)
                //{
                //    if (filas["ZTITULO"].ToString() == "ZID")
                //    {
                //        MiCampo = filas["ZTITULO"].ToString();
                //        break;
                //    }
                //}


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

                        MiContent = "DivLabelA" + a; //linea campo   DivFindL0                     
                        HtmlGenericControl DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                        DivCon.Visible = true;

                        //if (a != 0)
                        //{
                        //    MiContent = "DrFindL" + a; //linea campo                       
                        //    DropDownList Divcombo = (DropDownList)cont.FindControl(MiContent);
                        //    Divcombo.Items.AddRange(DrFindL0.Items.OfType<ListItem>().ToArray());
                        //    Divcombo.Visible = false;
                        //}

                        DivColumA.Visible = true;

                        MiContent = "lbL" + a; //linea campo                       
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        MiContent = "TxL" + a; //linea campo                       
                        DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZTITULO"].ToString();
                        DivLabel.Visible = false;

                        MiContent = "DivTextA" + a; //linea campo
                        HtmlGenericControl DivColumB = (HtmlGenericControl)cont.FindControl(MiContent);

                        string MM = filas["ZVALIDACION"].ToString();
                        if (filas["ZVALIDACION"].ToString() != "0")
                        {
                            //Busca su valor en la tabla de validación
                            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + filas["ZVALIDACION"].ToString();
                            DataTable dtValidacion = Main.BuscaLote(SQL).Tables[0];

                            //MiContent = "DivFindL" + a; //linea campo
                            //HtmlGenericControl DivColumC = (HtmlGenericControl)cont.FindControl(MiContent);
                            //DivColumC.Visible = true;
                            DivColumB.Visible = true;

                            foreach (DataRow fila in dtValidacion.Rows)
                            {
                                if (fila["ZID"].ToString() == filas["ZVALIDACION"].ToString())
                                {
                                    SQL = "SELECT ZID, ZDESCRIPCION AS " + filas["ZTITULO"].ToString() + "  FROM " + fila["ZTABLENAME"].ToString() + " ORDER BY ZID ";//" + filas["ZTITULO"].ToString() + "
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
                            //DivTexto.BorderStyle = BorderStyle.None;
                            DivColumB.Visible = true;
                        }
                    }
                    else
                    {
                        // DERECHA

                        string MiContent = "DivReg" + a; //linea campo
                        //HtmlGenericControl DivColumD = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                        ContentPlaceHolder cont = new ContentPlaceHolder();
                        cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
                        HtmlGenericControl DivColumD = (HtmlGenericControl)cont.FindControl(MiContent);
                        DivColumD.Visible = true;

                        MiContent = "DivLabelB" + a; //linea campo   DivFindL0                     
                        HtmlGenericControl DivCon = (HtmlGenericControl)cont.FindControl(MiContent);
                        DivCon.Visible = true;

                        //MiContent = "DrFindR" + a; //linea campo                       
                        //DropDownList Divcombo = (DropDownList)cont.FindControl(MiContent);
                        //Divcombo.Items.AddRange(DrFindL0.Items.OfType<ListItem>().ToArray());
                        //Divcombo.Visible = false;

                        MiContent = "lbD" + a; //linea campo
                        TextBox DivLabel = (TextBox)cont.FindControl(MiContent);
                        DivLabel.Text = filas["ZDESCRIPCION"].ToString();
                        DivLabel.Visible = true;

                        //MiContent = "LBCOLD" + a; //linea campo                       
                        //DivLabel = (TextBox)cont.FindControl(MiContent);
                        //DivLabel.Text = filas["ZTITULO"].ToString();
                        //DivLabel.Visible = false;

                        MiContent = "DivTextB" + a; //linea campo
                        HtmlGenericControl DivColumE = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);


                        if (filas["ZVALIDACION"].ToString() != "0")
                        {
                            //Busca su valor en la tabla de validación
                            string SQL = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZVIEW, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION ";
                            SQL += " FROM ZARCHIVOS WHERE ZNIVEL <= " + this.Session["MiNivel"] + " AND ZID = " + filas["ZVALIDACION"].ToString();
                            DataTable dtValidacion = Main.BuscaLote(SQL).Tables[0];
                            //MiContent = "DivFindR" + a; //linea campo
                            //HtmlGenericControl DivColumF = (DivCampos0.FindControl(MiContent) as HtmlGenericControl);
                            //DivColumF.Visible = true;
                            DivColumE.Visible = true;

                            foreach (DataRow fila in dtValidacion.Rows)
                            {
                                if (fila["ZID"].ToString() == filas["ZVALIDACION"].ToString())
                                {
                                    SQL = "SELECT ZID, ZDESCRIPCION AS " + filas["ZTITULO"].ToString() + " FROM " + fila["ZTABLENAME"].ToString() + " ORDER BY ZID ";//" + filas["ZTITULO"].ToString() + "
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
                            DivColumE.Visible = true;

                        }
                        a += 1;

                    }

                    if (filas["ZTITULO"].ToString() != MiCampo)
                    {
                        BoundField Campo = new BoundField();
                        Campo.DataField = filas["ZTITULO"].ToString();
                        Campo.HeaderText = filas["ZDESCRIPCION"].ToString();

                        DataControlField DataControlField = Campo;
                        gvEntrada.Columns.Add(DataControlField);
                    }

                    i += 1;
                }

                //gvEntrada.DataKeyNames = System.Text.RegularExpressions.Regex.Split(data, ";");

                DivCampos0.Attributes["height"] = (i * 50).ToString();
            }
            else //Dinamicos "Falta tabla validacion"
            {
                DivCampos0.Controls.Clear();

                HtmlGenericControl DivContent = new HtmlGenericControl();

                gvEntrada.Columns.Add(new ButtonField() { CommandName = "BajaOrden", DataTextField = "ZID", ImageUrl = "~/Images/sendDown20x20.png" });

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
                        //gvEntrada.Columns.Remove(DataControlField);
                    }

                    if ((i % 2) != 0)
                    {
                        DivCampos0.Controls.Add(DivContent);
                        a += 1;
                    }

                    BoundField Campo = new BoundField();

                    Campo.DataField = filas["ZTITULO"].ToString();
                    Campo.HeaderText = filas["ZDESCRIPCION"].ToString();


                    DataControlField DataControlField = Campo;
                    gvEntrada.Columns.Add(DataControlField);
                    i += 1;
                }

                //gvControl.DataKeyNames = System.Text.RegularExpressions.Regex.Split(data, ";");
            }

            //Crea el grid ficheros temporal
            //CreaGridFilesVacio();
            //Busca Error
            //Lberror.Text += " 1- Sale CreaPalets "  + Environment.NewLine;

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
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
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

                if (e.CommandName == "BajaOrden")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    int Indice = index;
                    this.Session["IDGridA"] = gvEntrada.DataKeys[index].Value.ToString();

                    GridViewRow row = gvEntrada.Rows[index];
                    string campo = gvEntrada.DataKeyNames[0].ToString();
                    string miro = gvEntrada.DataKeys[index].Value.ToString();

                    row.BackColor = Color.FromName("#ffead1");

                    string campos = "";
                    DataTable dtCam = this.Session["Campos"] as DataTable;
                    foreach (DataRow filas in dtCam.Rows)
                    {
                        if (campos == "")
                        {
                            campos = filas["ZTITULO"].ToString();
                        }
                        else
                        {
                            campos += ", " + filas["ZTITULO"].ToString();
                        }
                    }
                    string SQL = "SELECT " + campos + " FROM " + this.Session["TablaName"].ToString() + " WHERE " + campo + " = '" + miro + "' ";
                    DataTable dt = Main.BuscaLote(SQL).Tables[0];

                    int AA = 0;
#pragma warning disable CS0219 // La variable 'UU' está asignada pero su valor nunca se usa
                    int UU = 0;
#pragma warning restore CS0219 // La variable 'UU' está asignada pero su valor nunca se usa
                    string NombreDivCampo = "";
                    string NombreCajaTexto = "";

                    foreach (DataRow filas in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if ((i % 2) == 0)
                            {
                                //Par
                                NombreCajaTexto = "TxL" + AA;
                                NombreDivCampo = "DivReg" + AA;
                                //HtmlGenericControl DivParam = new HtmlGenericControl();
                                HtmlGenericControl TB = (DivEstados.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                //TextBox TB = (DivCampos0.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if (filas[dt.Columns[i].ToString()].ToString() == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(filas[dt.Columns[i].ToString()].ToString());
                                    }
                                }

                                string MiContent = "DrL" + AA; //linea campo
                                DropDownList DivDL = (DivEstados.FindControl(MiContent) as DropDownList); // (DropDownList)cont.FindControl(MiContent);
                                if(DivDL.Visible == true)
                                {
                                    for (int b = 0; b < DivDL.Items.Count; b++)
                                    {
                                        if (DivDL.Items[b].Value == MiControl.Text)
                                        {
                                            DivDL.SelectedIndex = b;
                                            break;
                                        }
                                    }
                                }
                            }
                            else//Impar
                            {
                                NombreCajaTexto = "TxD" + AA;
                                HtmlGenericControl TB = (DivEstados.FindControl(NombreDivCampo) as HtmlGenericControl);
                                TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                                if (MiControl.Text != null)
                                {
                                    if (filas[dt.Columns[i].ToString()].ToString() == "&nbsp;")
                                    {
                                        MiControl.Text = "";
                                    }
                                    else
                                    {
                                        MiControl.Text = HTMLaTXT(filas[dt.Columns[i].ToString()].ToString());
                                    }
                                }

                                string MiContent = "DrR" + AA; //linea campo
                                DropDownList DivDL = (DivEstados.FindControl(MiContent) as DropDownList); // (DropDownList)cont.FindControl(MiContent);
                                if (DivDL.Visible == true)
                                {
                                    for (int b = 0; b < DivDL.Items.Count; b++)
                                    {
                                        if (DivDL.Items[b].Value == MiControl.Text)
                                        {
                                            DivDL.SelectedIndex = b;
                                            break;
                                        }
                                    }
                                }

                                AA += 1;
                            }
                        }
                        break;
                    }
                    BtNuevo.Enabled = true;
                    BtEdita.Enabled = true;

                    //foreach (TableCell cell in row.Cells)
                    //{
                    //    //string NombreCajaTexto = "";

                    //    if ((AA % 2) == 0)
                    //    {
                    //        //Par
                    //        NombreCajaTexto = "TxL" + UU;
                    //        NombreDivCampo = "DivReg" + UU;
                    //        //HtmlGenericControl DivParam = new HtmlGenericControl();
                    //        HtmlGenericControl TB = (DivEstados.FindControl(NombreDivCampo) as HtmlGenericControl);
                    //        TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                    //        //TextBox TB = (DivCampos0.FindControl(NombreCajaTexto) as TextBox);
                    //        if (MiControl.Text != null)
                    //        {
                    //            if (cell.Text == "&nbsp;")
                    //            {
                    //                MiControl.Text = "";
                    //            }
                    //            else
                    //            {
                    //                MiControl.Text = HTMLaTXT(cell.Text);
                    //            }
                    //        }
                    //    }
                    //    else//Impar
                    //    {
                    //        NombreCajaTexto = "TxD" + UU;
                    //        HtmlGenericControl TB = (DivEstados.FindControl(NombreDivCampo) as HtmlGenericControl);
                    //        TextBox MiControl = (TB.FindControl(NombreCajaTexto) as TextBox);
                    //        if (MiControl.Text != null)
                    //        {
                    //            if (cell.Text == "&nbsp;")
                    //            {
                    //                MiControl.Text = "";
                    //            }
                    //            else
                    //            {
                    //                MiControl.Text = HTMLaTXT(cell.Text);
                    //            }
                    //        }
                    //        UU += 1;
                    //    }
                    //    AA += 1;
                    //}
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
            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
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
            s = s.Replace("&aacute;", "á");
            s = s.Replace("&eacute;", "é");
            s = s.Replace("&iacute;", "í");
            s = s.Replace("&uacute;", "ú");
            s = s.Replace("&oacute;", "ó");
            s = s.Replace("&oacute;", "ó");
            s = s.Replace("&ntilde;", "ñ");
            s = s.Replace("&quot;", "\"");
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

            DataTable dtCampos = this.Session["Campos"] as DataTable;

#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
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
            }
            catch (Exception ex)
            {
            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }

        protected void gvEntrada_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvEntrada.SelectedRow.BackColor = Color.FromName("#565656");
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

        protected void DrPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

        }


        protected void dlConexion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
            ListBoxArchivo.Items.Add(new ListasID(DrConexion.SelectedItem.Text, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            ListBoxArchivoID.Items.Add(new ListasID(DrConexion.SelectedItem.Value, Convert.ToInt32(DrConexion.SelectedItem.Value)).ToString());
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaFlujo(1);
            }

        }
        protected void ListBoxArchivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBoxArchivoID.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
            ListBoxArchivo.Items.RemoveAt(ListBoxArchivo.SelectedIndex);
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

            TxtNombre.Text = "";
            TxtDescripcion.Text = "";

            DrConexion.SelectedIndex = 0;

            this.Session["Edicion"] = 0;
        }

   

    
        public DataTable RelacionesArchivo(int ID, DataTable DtCampos)
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

                        this.Session["TablaObj"] = dr["ZTABLEOBJ"].ToString();
                        this.Session["TablaName"] = dr["ZTABLENAME"].ToString();
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

        private void EditaTemplate(int Abierto)
        {
            if (Abierto == 0)
            {
                //Hay que cambiar controles
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                DrConexion.Enabled = false;
                ListBoxArchivo.Enabled = false;
                //BtguardaFlujo.Enabled = false;
                //BtCancelFlujo.Enabled = false;
                TxtNombre.BackColor = Color.FromName("#ffffff");
                TxtDescripcion.BackColor = Color.FromName("#ffffff");
                DrConexion.BackColor = Color.FromName("#ffffff");
                ListBoxArchivo.BackColor = Color.FromName("#ffffff");
            }
            else
            {
                TxtNombre.Enabled = true;
                TxtDescripcion.Enabled = true;
                DrConexion.Enabled = true;
                ListBoxArchivo.Enabled = true;
                //BtguardaFlujo.Enabled = true;
                //BtCancelFlujo.Enabled = true;
                TxtNombre.BackColor = Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = Color.FromName("#bdecb6");
                DrConexion.BackColor = Color.FromName("#bdecb6");
                ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
            }
        }
        private void EditaFlujo(int Abierto)
        {
            if(Abierto == 0)
            {
                TxtNombre.Enabled = false;
                TxtDescripcion.Enabled = false;
                DrConexion.Enabled = false;
                ListBoxArchivo.Enabled = false;
                //BtguardaFlujo.Enabled = false;
                //BtCancelFlujo.Enabled = false;
                TxtNombre.BackColor = Color.FromName("#ffffff");
                TxtDescripcion.BackColor = Color.FromName("#ffffff");
                DrConexion.BackColor = Color.FromName("#ffffff");
                ListBoxArchivo.BackColor = Color.FromName("#ffffff");
                //DrPagina.Enabled = false;
                //DrPagina.BackColor = Color.FromName("#ffffff");
            }
            else
            {
                TxtNombre.Enabled = true;
                TxtDescripcion.Enabled = true;
                DrConexion.Enabled = true;
                ListBoxArchivo.Enabled = true;
                //BtguardaFlujo.Enabled = true;
                //BtCancelFlujo.Enabled = true;
                TxtNombre.BackColor = Color.FromName("#bdecb6");
                TxtDescripcion.BackColor = Color.FromName("#bdecb6");
                DrConexion.BackColor = Color.FromName("#bdecb6");
                ListBoxArchivo.BackColor = Color.FromName("#bdecb6");
                //DrPagina.BackColor = Color.FromName("#bdecb6");
                //DrPagina.Enabled = true;
            }
        }

        private void LimpioFlujo()
        {
            TxtNombre.Text = "";
            TxtDescripcion.Text = "";
            //DrConexion.Text = "";
            ListBoxArchivo.Items.Clear();
            ListBoxArchivoID.Items.Clear();
        }

        protected void btncierraSQL_Click(object sender, EventArgs e)
        {
            DivCampoDer.Visible = true;
        }

        private void LimpioEstado()
        {
            //TxtidEstado.Text = "";
            //TxtEstado.Text = "";
        }
        protected void btnGuardarRelacion_Click(object sender, EventArgs e)
        {
            //Guardar Relación


            EditaFlujo(0);
            this.Session["Edicion"] = "0";

        }

        protected void btnCancelarRelacion_Click(object sender, EventArgs e)
        {

            EditaFlujo(0);
            this.Session["Edicion"] = "0";

        }

        protected void btnCancelarProfile_Click(object sender, EventArgs e)
        {
            this.Session["Edicion"] = "0";

        }
        protected void btnCancelarFlujo_Click(object sender, EventArgs e)
        {
            EditaFlujo(0);
            this.Session["Edicion"] = "0";

        }
        protected void btnCancelarEstado_Click(object sender, EventArgs e)
        {
             Cancelado();
            this.Session["Edicion"] = "0";

        }
        protected void btnGuardarFlujo_Click(object sender, EventArgs e)
        {

        }
 
        protected void DrTipoEvaluacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnCopiaFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0") { return; }
            //LimpioFlujo();
            EditaFlujo(1);
            this.Session["Edicion"] = "1";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZFLUJOS", dbParams));
            TxtNombre.Text = (MiID + 1).ToString();
            TxtDescripcion.Text = "Copia de " + TxtDescripcion.Text;
        }
        
        protected void btnCreaFlujo_Click(object sender, EventArgs e)
        {
            if(this.Session["Edicion"].ToString() != "0") { return; }
            LimpioFlujo();
            EditaFlujo(1);
            this.Session["Edicion"] = "1";

            SqlParameter[] dbParams = new SqlParameter[0];
            //maximo id
            int MiID = Convert.ToInt32(DBHelper.ExecuteScalarSQL("SELECT MAX(ZID) FROM ZFLUJOS", dbParams));
            TxtNombre.Text = (MiID + 1).ToString(); 
        }
        protected void btnEditFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            EditaFlujo(1);
            this.Session["Edicion"] = "2";
        }

        protected void btnAllFlujo_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }
            if(IbtAllFlujo.ImageUrl == "~/Images/allwhite.png")
            {
                this.Session["EstadoCondicion"] = "1";
                IbtAllFlujo.ImageUrl = "~/Images/allgreen.png";
                Actualiza_Archivos("2");
                lbTable.Text = "Archivo Documental:";
                Label20.Text = "Registro actual de este Archivo Documental";
                Label10.Text = "Archivos Documentales";
            }
            else
            {
                this.Session["EstadoCondicion"] = "0";
                IbtAllFlujo.ImageUrl = "~/Images/allwhite.png";
                Actualiza_Archivos("1");
                lbTable.Text = "Registro actual de esta Tabla de Validación:";
                Label10.Text = "Tablas de Validación";
            }
            gvEntrada.DataSource = null;
            gvEntrada.DataBind();
        }

        protected void btnAllEstado_Click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() != "0")
            {
                Cancelado();
                return;
            }

            //DataTable dt = Main.CargaFlujosEstados(0).Tables[0];
            DataTable dt = Main.CargaEstadosFl(0).Tables[0];
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
            TxtDescripcion.Text = "";
            TxtNombre.Text = MiID.ToString();

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
            //Relaciones(0, CampoOrden);
            ActivarTxt();
          
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
            //btnEditar.Visible = false;
            //btnNuevo.Visible = false;
            //btnGuardar.Visible = true;
            //btnCancelar.Visible = true;
            DrArchivos.Enabled = false;

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
        

 
        public void DesactivarTxt()
        {

            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");

            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            //CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            TxtNombre.Enabled = false;
            TxtDescripcion.Enabled = false;

            DrConexion.Enabled = false;
        }

        public void ActivarTxt()
        {
            TxtNombre.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtDescripcion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            //CommentSQL.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            DrConexion.BackColor = System.Drawing.ColorTranslator.FromHtml("#eaf7cd");
            TxtNombre.Enabled = true;
            TxtDescripcion.Enabled = true;
            DrConexion.Enabled = true;
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
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
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
            catch(Exception ex)
            {

            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
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

            DataTable dt = new DataTable();
            dt = Main.TVCargaCampos(Convert.ToInt32(tvControl.SelectedNode.Value), Convert.ToInt32((string)Session["MiNivel"])).Tables[0];


        }

        protected void TxtGrupoName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TxtIDtemplate_TextChanged(object sender, EventArgs e)
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

            //DivSQL.Visible = false;
            DivCampoDer.Visible = true;
            DrArchivos.Enabled = true;
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
 
        private void LimpiaEstados()
        {
            //TxtidEstado.Text = "";
            //TxtEstado.Text = "";

        }

  

        protected void TxTemplate_click(object sender, EventArgs e)
        {
            if (this.Session["Edicion"].ToString() == "0" || this.Session["Edicion"].ToString() == "")
            {
            }
            else
            {
                EditaTemplate(1);
            }
        }






 

        protected void DrDuplicado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Djerarquia_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            MiOpenMenu();

        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            MiOpenMenu();
            SqlParameter[] dbParams = new SqlParameter[0];
            DBHelper.ExecuteNonQuerySQL("UPDATE ZARCHIVOS SET ZESTADO = 3 WHERE ZID = " + this.Session["IDArchivo"].ToString(), dbParams);
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            cuestion.Visible = false;
            Asume.Visible = false;
            windowmessaje.Visible = false;
            MiOpenMenu();

        }
        protected void btnCopiaCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnCreaCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeleteCustion_Click(object sender, EventArgs e)
        {

        }
        protected void btnAllCustion_Click(object sender, EventArgs e)
        {

        }

        protected void DrCuestion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void TxtIDcuestion_TextChanged(object sender, EventArgs e)
        {

        }
        protected void TxIDCuestion_click(object sender, EventArgs e)
        {

        }
        protected void DrMarcadorCuestion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnDeleteCuestion_Click(object sender, EventArgs e)
        {

        }
        protected void ListMarcaCuestion_DoubleClick(object sender, EventArgs e)
        {

        }
        protected void sellectAll(object sender, EventArgs e)
        {

        }
        protected void btnTipoTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnCopiaTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnCreaTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnEditTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeleteTemplateM_Click(object sender, EventArgs e)
        {

        }
        protected void btnAllTemplateM_Click(object sender, EventArgs e)
        {

        }

        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = this.Session["Archivos"] as DataTable;
            DataTable dt1 = this.Session["Campos"] as DataTable;
        }

        protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string miro = "1"; // DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();


                miro = DataBinder.Eval(e.Row.DataItem, "ZID").ToString();
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    //e.Row.Cells[0].Visible = false;
                    //e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    //e.Row.Cells[7].Visible = false;
                    //e.Row.Cells[17].Visible = false;
                }


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
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.TableSection = TableRowSection.TableHeader;
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    //e.Row.Cells[0].Visible = false;
                    //e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    //e.Row.Cells[7].Visible = false;
                    //e.Row.Cells[17].Visible = false;
                }

                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.TableSection = TableRowSection.TableFooter;
                e.Row.BackColor = Color.FromName("#f5f5f5");
            }
        }
    
        protected void Dtipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Cambio_pass(object sender, ImageClickEventArgs e)
        {
            this.Session["PagePreview"] = "AltaArchivo.aspx";
            Server.Transfer("CambioLogin.aspx"); //Default
        }
    }
}