using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net;

//using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using System.Text;
//using Admin.Clases;
//using Admin.Classes;

namespace Satelite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(),"Agua", "function();", true);
            //RegistrarScript();
            if (!IsPostBack)
            {
                this.Session["Session"] = "";
                this.Session["UserAlias"] = "";
                this.Session["MiCodigo"] = "";
                this.Session["MiRoot"] = "";
                this.Session["MiKey"] = "";
                this.Session["MiNivel"] = "";
                this.Session["Edicion"] = "";
                this.Session["TipoConexion"] = "";
                this.Session["MiFlujoEstado"] = "";

                this.Session["AltoC"] = "0";
                this.Session["AnchoC"] = "0";
                this.Session["AltoT"] = "0";
                this.Session["AnchoT"] = "0";
                this.Session["IDLote"] = "0";
                this.Session["IDSecuencia"] = "0";
                this.Session["IDLote"] = "0";
                this.Session["IDLista"] = "0";
                this.Session["IDProcedimiento"] = "0";
                this.Session["Session"] = "0";
                this.Session["Error"] = "0";
                this.Session["MenuUsuario"] = "";
                this.Session["Permisos"] = "";

                //Firma
                this.Session["TablaObj"] = "";
                this.Session["CamposTabla"] = "";
                this.Session["idarchivo"] = "";
                this.Session["collapse1"] = "1";
                this.Session["idregistro"] = "0";
                this.Session["idflujo"] = "";
                this.Session["idestado"] = "";
                this.Session["TablaName"] = "";
                this.Session["Query1"] = "";
                this.Session["Idprocedimiento"] = "";
                this.Session["DocumentacionAdjunta"] = "";
                this.Session["CampoFiltro"] = "";
                this.Session["iddocumento"] = "";
                this.Session["Registros"] = "0";
                this.Session["CondicionEstado"] = "";

                this.Session["NombreCompleto"] = "";
                this.Session["FiltroCondicion"] = "";
                this.Session["FiltroConsulta"] = 1;
                this.Session["FaltaDato"] = 0;

                this.Session["Pagina"] = "Firma.aspx";

                //this.Session["IDSecuencia"] = "0";
                //this.Session["IDProcedimiento"] = "0";
                this.Session["IDGridA"] = "0";
                this.Session["IDGridB"] = "0";
                this.Session["Filtro"] = "0";
                this.Session["NumeroPalet"] = "0";
                this.Session["FiltroEmpresa"] = "";
                this.Session["FiltroFecha"] = "";
                this.Session["FiltroCliente"] = "";
                this.Session["FiltroRuta"] = "";
                this.Session["Menu"] = "0";//1
                this.Session["IDCabecera"] = "0";
                this.Session["CalculoPaletPlanta"] = "";
                this.Session["filtrolocal"] = "";
                this.Session["Delegate"] = "";
                this.Session["EstadoCabecera"] = "";
                this.Session["FiltroCodEmpleado"] = "";
                this.Session["FiltroGral"] = "";
                this.Session["UltimaConsulta"] = "";
                this.Session["UltimaConsultaFin"] = "";
                this.Session["SQL"] = "";
                this.Session["Erroneo"] = "0";
                this.Session["Centro"] = "";

                DvPreparado.Visible = false;
                UpdatePanel3.Update();
            }
            else
            {
                if(txtPassword.Text != "")
                {
                    this.Session["Session"] = "";
                }
            }



        }



        private void RegistrarScript()
        {
            const string ScriptKey = "ScriptKey";
            if (!ClientScript.IsStartupScriptRegistered(this.GetType(), ScriptKey))
            {
                StringBuilder fn = new StringBuilder();
                fn.Append("$(document).ready(function()");
                fn.Append("{");
                fn.Append("try");
                fn.Append("{");
                fn.Append("$('body').ripples({");
                fn.Append("resolution: 512,");
                fn.Append("dropRadius: 20,");
                fn.Append("perturbance: 0.04,");
                fn.Append("}");
                fn.Append("catch (e) {");
                fn.Append("$('.error').show().text(e);");
                fn.Append("}");
                fn.Append("});");


                ClientScript.RegisterStartupScript(this.GetType(),
            ScriptKey, fn.ToString(), true);
            }
        }

        private void Mimenu()
        {
            string SQL = "SELECT A.ZID, A.ZALIAS, A.ZDESCRIPCION , B.ZID_GRUPO, B.ZID_USUARIO, C.ZNOMBRE, C.ZDESCRIPCION, D.ZID as ZIDNIVEL, E.ZDESCRIPCION as ZDESNIVEL,  ";
            SQL += "E.ZID as ZNIVEL, D.ZIDMENU, F.ZDESCRIPCION, F.ZPAGINA, F.ZROOT ";
            SQL += "FROM ZUSUARIOS A ";
            SQL += "INNER JOIN  ZUSUARIOGRUPO B ON A.ZID = B.ZID_USUARIO ";
            SQL += "INNER JOIN  ZGRUPOS C ON C.ZID = B.ZID_GRUPO ";
            SQL += "INNER JOIN  ZMENUPERMISOSGRUPO D ON D.ZID_GRUPO = B.ZID_GRUPO ";
            SQL += "INNER JOIN  ZNIVELES E ON E.ZID = D.ZNIVEL ";
            SQL += "INNER JOIN  ZMENU F ON F.ZID = D.ZIDMENU ";
            SQL += "WHERE A.ZID =  " + this.Session["IDSession"].ToString();
            SQL += " ORDER BY  D.ZIDMENU, F.ZROOT ";


            DataTable Mitable = Main.BuscaLote(SQL).Tables[0];

            this.Session["Permisos"] = Mitable;

            string MiInto = "";
            foreach (DataRow fila in Mitable.Rows)
            {
                if (MiInto == "")
                {
                    MiInto += fila["ZIDMENU"].ToString() + ", " + fila["ZROOT"].ToString();
                }
                else
                {
                    MiInto += " ," + fila["ZIDMENU"].ToString() + ", " + fila["ZROOT"].ToString();
                }
                // Nivel de Menús
                this.Session["MiNivel"] = fila["ZNIVEL"].ToString();
            }

            if (MiInto != "")
            {
                //Aqui creo que deberia filtrar por grupos
                SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                SQL += " FROM ZMENU AA ";
                SQL += " WHERE AA.ZESTADO <> 3 ";
                SQL += "AND AA.ZID IN (" + MiInto + ") ";
                SQL += "ORDER BY ZID, ZROOT";


                DataTable table = Main.BuscaLote(SQL).Tables[0];
                DataRow[] parentMenus = table.Select("ZROOT = 0");

                var sb = new StringBuilder();
                string unorderedList = GenerateUL(parentMenus, table, sb);

                this.Session["MenuUsuario"] = unorderedList;
            }
            else
            {
                this.Session["MenuUsuario"] = "";
            }
        }

        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            sb.AppendLine("<ul>");// class=\"nav nav-second-level collapse\">");

            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string handler = dr["ZPAGINA"].ToString();
                    string menuText = dr["ZTITULO"].ToString();
                    string pid = dr["ZID"].ToString();
                    string parentId = dr["ZROOT"].ToString();

                    string line = String.Format(@"<li><a href=""{0}"">{1}</a>" + Environment.NewLine, handler, menuText);
                    sb.Append(line);

                    DataRow[] subMenu = table.Select(String.Format("ZROOT = {0}", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        var subMenuBuilder = new StringBuilder();
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
                    }
                    sb.Append("</li>" + Environment.NewLine);
                    //sb.Append("</ul>");
                }
            }

            sb.Append("</ul>" + Environment.NewLine);
            return sb.ToString();
        }

        //Clase para guardar la información de los dispositivos
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //carga resultado de clase Login.cs
            //string var = Seguridad.Encriptar(txtPassword.Text + txtUserID.Text);
            //string var = Seguridad.Encriptar(txtPassword.Text);
            string var = txtPassword.Text;
            //Registra equipo y comprueba si es un alta de usuario
            RegistraEquipo(txtUserID.Text, var);



            DataSet ds = Login.ValidarLogin(txtUserID.Text, var);
            //DataTable dt = Login.ValidarLogin(txtUserID.Text, var);
            txtUserID2.Text = txtUserID.Text;

            DataTable dt = ds.Tables[0];

            if(this.Session["Session"].ToString() == "8")
            {
                Dlogin.Visible = false;
                DloginCapcha.Visible = true;
                LbUserDes.Text = txtUserID2.Text;
                return;
            }
            //else
            //{
            //    ds = Login.VLoginIn(txtUserID.Text, var);
            //}

            if (dt.Rows.Count == 0)
            {
                //messageBox.ShowMessage("Usuario y/o Password incorrectos");
            }
            else
            {
                this.Session["Session"] = dt.Rows[0]["ZLLAVE"].ToString(); //ZPASSWORD
                this.Session["IDSession"] = dt.Rows[0]["ZID"].ToString();// "0";
                this.Session["UserAlias"] = dt.Rows[0]["ZALIAS"].ToString();
                this.Session["MiCodigo"] = dt.Rows[0]["ZCODIGO"].ToString();
                this.Session["MiRoot"] = dt.Rows[0]["ZROOT"].ToString();
                this.Session["MiKey"] = dt.Rows[0]["ZKEY"].ToString();
                this.Session["MiNivel"] = dt.Rows[0]["ZNIVEL"].ToString();
                this.Session["Edicion"] = "0";
                this.Session["TipoConexion"] = "1";
                this.Session["MiFlujoEstado"] = "0";
                this.Session["MiMenu"] = "";
                this.Session["Ano"] = Convert.ToInt32(DateTime.Now.Year);
                this.Session["Mes"] = Convert.ToInt32(DateTime.Now.Month);
                this.Session["TrazaLog"] = ConfigurationManager.AppSettings.Get("TrazaLog");
                this.Session["Procedimiento"] = "btnLogin_Click";
                this.Session["TablaObj"] = "";
                this.Session["IDGridA"] = "";
                this.Session["Param1"] = "";
                this.Session["Param2"] = "";
                this.Session["CargaPag"] = "0";
                this.Session["Param3"] = "";
                this.Session["FilesUp"] = "";
                this.Session["IndexPage"] = "0";
                this.Session["idregistro"] = dt.Rows[0]["ZID_REGISTRO"].ToString();
                this.Session["idarchivo"] = dt.Rows[0]["ZID_TABLA"].ToString();
                this.Session["Default"] = dt.Rows[0]["ZDEFAULT"].ToString();
                this.Session["DefaultVolumen"] = ConfigurationManager.AppSettings.Get("DefaultVolumen");


                //Recoge el menú del usuario
                Mimenu();

                string clientPCName;
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();
                this.Session["ComputerName"] = clientPCName;

                string SQL = "UPDATE ZUSUARIOS SET ZEQUIPO = '" + clientPCName + "' WHERE ZID = " + dt.Rows[0]["ZID"].ToString();
                DBHelper.ExecuteNonQuery(SQL);


                DataSet dtsTablasValidación = new DataSet();
                dtsTablasValidación = null;
                this.Session["Tvalidacion"] = dtsTablasValidación;


                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
                if (Miro.Contains("DESARROLLO") == true)
                {
                    this.Session["DESARROLLO"] = "DESARROLLO";
                }
                else
                {
                    this.Session["DESARROLLO"] = "0";
                }


                if (txtUserID.Text == "Administrador")
                {
                    //Response.Redirect("Main.aspx");
                }
                else
                {
                    DataTable dtB = new DataTable();
                    DataTable dtF = new DataTable();
                    //Si existen Areas
                     if (Main.CuentaArchivos().Tables[0] != null)
                    {
                        //string clientPCName;
                        //string[] computer_name = System.Net.Dns.GetHostEntry(
                        //Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                        //clientPCName = computer_name[0].ToString();
                        //this.Session["ComputerName"] = clientPCName;


                        //if (this.Session["MiNivel"].ToString() == "0")
                        //{
                        //    this.Session["Response"] = "Firma.aspx";
                        //    this.Session["Upload"] = "uploads/" + this.Session["UserAlias"] + "/";
                        //    Server.Transfer("Firma.aspx");
                        //}
                        //else
                        //{
                            dtB = Main.CargaArchivos().Tables[0];
                            this.Session["Archivos"] = dtB;
                            dtB = null;
                            dtB = Main.CargaCampos().Tables[0];
                            this.Session["Campos"] = dtB;
                            dtB = null;
                            dtF = Main.CargaFormatoCampos().Tables[0];
                            this.Session["TipoCampo"] = dtB;
                            dtB = null;
                            dtF = Main.CargaArchivoCampos().Tables[0];
                            this.Session["ArchivoCampos"] = dtF;
                            dtB = null;
                            dtF = Main.CargaFormatoArchivo().Tables[0];
                            this.Session["TipoArchivo"] = dtF;
                            dtB = null;
                            dtF = Main.CargaSecuencias().Tables[0];
                            this.Session["Secuencias"] = dtF;
                            dtB = null;
                            dtF = Main.CargaNiveles().Tables[0];
                            this.Session["Niveles"] = dtF;
                            dtB = null;
                            dtF = Main.CargaEstados().Tables[0];
                            this.Session["Estados"] = dtF;
                            dtB = null;
                            dtF = Main.CargaJerarquia().Tables[0];
                            this.Session["TipoArchivos"] = dtF;

                            this.Session["Response"] = "Inicio.aspx";

                            this.Session["Upload"] = "~/uploads/" + this.Session["UserAlias"] + "/";

                            //string clientPCName;
                            //string[] computer_name = System.Net.Dns.GetHostEntry(
                            //Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                            //clientPCName = computer_name[0].ToString();
                            //this.Session["ComputerName"] = clientPCName;

                            //Aqui consulta con permisos de visualizacion
                            Object Con = DBHelper.ExecuteScalarSQL("SELECT ZPAGINA FROM ZMENU  WHERE ZTITULO = '" + this.Session["Default"].ToString() + "'", null);

                            if (Con is System.DBNull)
                            {
                                Server.Transfer("Inicio.aspx");
                            }
                            else
                            {
                                //02/10/2024 Si marca error la pagina establecida por defecto, envio a la pagina de inicio con Panel de Control
                                try
                                {
                                    Server.Transfer(Con.ToString());
                                }
                                catch 
                                {
                                    Lbmensaje.Text = "Este Usuario no tiene su pagina de entrada por defecto correctamente definida.";
                                    cuestion.Visible = false;
                                    Asume.Visible = true;
                                    DvPreparado.Visible = true;
                                    UpdatePanel3.Update();

                                    Server.Transfer("Inicio.aspx");
                                }
                            }

                            //Response.Redirect("Principal.aspx"); //Default
                            //Server.Transfer("Principal.aspx");
                            //Server.Transfer("Inicio.aspx");
                        //}

                    }
                    //si no existen Archivos
                    else
                    {
                        ////CARGA EL USUARIO QUE HACE LOGIN CON PERMISOS
                        dtB = null;
                        dtF = Main.CargaFormatoCampos().Tables[0];
                        this.Session["TipoCampo"] = dtB;
                        dtB = null;
                        dtF = Main.CargaArchivoCampos().Tables[0];
                        this.Session["ArchivoCampos"] = dtF;
                        dtB = null;
                        dtF = Main.CargaFormatoArchivo().Tables[0];
                        this.Session["TipoArchivo"] = dtF;
                        dtB = null;
                        dtF = Main.CargaSecuencias().Tables[0];
                        this.Session["Secuencias"] = dtF;

                        this.Session["Response"] = "AltaArchivo.aspx";
                        //Response.Redirect("AltaArchivo.aspx");
                        Server.Transfer("AltaArchivo.aspx");
                    }
                }
            }
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
        }

        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;

        }
        protected void btnLoginDes_Click(object sender, EventArgs e)
        {
            //carga resultado de clase Login.cs
            //string var = Seguridad.Encriptar(txtPassword.Text + txtUserID.Text);
            //string var = Seguridad.Encriptar(txtPassword.Text);
            string var = txtPassword2.Text;
            RegistraEquipo(txtUserID.Text, var);
            //Registra el equipo
            ValidaEquipo(txtUserID.Text, var);
            //
            DataSet ds = Login.ValidarLogin(txtUserID.Text, var);

            
            //DataTable dt = Login.ValidarLogin(txtUserID.Text, var);
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                //messageBox.ShowMessage("Usuario y/o Password incorrectos");
            }
            else
            {
                this.Session["Session"] = dt.Rows[0]["ZPASSWORD"].ToString();
                this.Session["IDSession"] = dt.Rows[0]["ZID"].ToString(); // "0";
                this.Session["UserAlias"] = dt.Rows[0]["ZALIAS"].ToString();
                this.Session["MiCodigo"] = dt.Rows[0]["ZCODIGO"].ToString();
                this.Session["MiRoot"] = dt.Rows[0]["ZROOT"].ToString();
                this.Session["MiKey"] = dt.Rows[0]["ZKEY"].ToString();
                this.Session["MiNivel"] = dt.Rows[0]["ZNIVEL"].ToString();
                this.Session["Edicion"] = "0";
                this.Session["TipoConexion"] = "1";
                this.Session["MiFlujoEstado"] = "0";
                this.Session["MiMenu"] = "";
                this.Session["Ano"] = Convert.ToInt32(DateTime.Now.Year);
                this.Session["Mes"] = Convert.ToInt32(DateTime.Now.Month);
                this.Session["TrazaLog"] = ConfigurationManager.AppSettings.Get("TrazaLog");
                this.Session["Procedimiento"] = "btnLogin_Click";
                this.Session["TablaObj"] = "";
                this.Session["IDGridA"] = "";
                this.Session["Param1"] = "";
                this.Session["Param2"] = "";
                this.Session["Param3"] = "";
                this.Session["FilesUp"] = "";
                this.Session["IndexPage"] = "0";
                this.Session["idregistro"] = dt.Rows[0]["ZID_REGISTRO"].ToString();
                this.Session["idarchivo"] = dt.Rows[0]["ZID_TABLA"].ToString();
                this.Session["Mydb"] = "";

                string clientPCName;
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();
                this.Session["ComputerName"] = clientPCName;

                string SQL = "UPDATE ZUSUARIOS SET ZEQUIPO = '" + clientPCName + "' WHERE ZID = " + dt.Rows[0]["ZID"].ToString();
                DBHelper.ExecuteNonQuery(SQL);


                DataSet dtsTablasValidación = new DataSet();
                dtsTablasValidación = null;
                this.Session["Tvalidacion"] = dtsTablasValidación;


                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
                if (Miro.Contains("DESARROLLO") == true)
                {
                    this.Session["DESARROLLO"] = "DESARROLLO";
                }
                else
                {
                    this.Session["DESARROLLO"] = "0";
                }


                if (txtUserID.Text == "Administrador")
                {
                    //Response.Redirect("Main.aspx");
                }
                else
                {
                    DataTable dtB = new DataTable();
                    DataTable dtF = new DataTable();
                    //Si existen Areas
                    if (Main.CuentaArchivos().Tables[0] != null)
                    {
                        //string clientPCName;
                        //string[] computer_name = System.Net.Dns.GetHostEntry(
                        //Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                        //clientPCName = computer_name[0].ToString();
                        //this.Session["ComputerName"] = clientPCName;


                        if (this.Session["MiNivel"].ToString() == "0")
                        {
                            this.Session["Response"] = "Firma.aspx";
                            this.Session["Upload"] = "~/uploads/" + this.Session["UserAlias"] + "/";
                            Server.Transfer("Firma.aspx");
                        }
                        else
                        {
                            dtB = Main.CargaArchivos().Tables[0];
                            this.Session["Archivos"] = dtB;
                            dtB = null;
                            dtB = Main.CargaCampos().Tables[0];
                            this.Session["Campos"] = dtB;
                            dtB = null;
                            dtF = Main.CargaFormatoCampos().Tables[0];
                            this.Session["TipoCampo"] = dtB;
                            dtB = null;
                            dtF = Main.CargaArchivoCampos().Tables[0];
                            this.Session["ArchivoCampos"] = dtF;
                            dtB = null;
                            dtF = Main.CargaFormatoArchivo().Tables[0];
                            this.Session["TipoArchivo"] = dtF;
                            dtB = null;
                            dtF = Main.CargaSecuencias().Tables[0];
                            this.Session["Secuencias"] = dtF;
                            dtB = null;
                            dtF = Main.CargaNiveles().Tables[0];
                            this.Session["Niveles"] = dtF;
                            dtB = null;
                            dtF = Main.CargaEstados().Tables[0];
                            this.Session["Estados"] = dtF;
                            dtB = null;
                            dtF = Main.CargaJerarquia().Tables[0];
                            this.Session["TipoArchivos"] = dtF;

                            this.Session["Response"] = "Inicio.aspx";

                            this.Session["Upload"] = "~/uploads/" + this.Session["UserAlias"] + "/";

                            //string clientPCName;
                            //string[] computer_name = System.Net.Dns.GetHostEntry(
                            //Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                            //clientPCName = computer_name[0].ToString();
                            //this.Session["ComputerName"] = clientPCName;

                            //Response.Redirect("Principal.aspx"); //Default
                            //Server.Transfer("Principal.aspx");
                            Server.Transfer("Inicio.aspx");
                        }

                    }
                    //si no existen Archivos
                    else
                    {
                        ////CARGA EL USUARIO QUE HACE LOGIN CON PERMISOS
                        dtB = null;
                        dtF = Main.CargaFormatoCampos().Tables[0];
                        this.Session["TipoCampo"] = dtB;
                        dtB = null;
                        dtF = Main.CargaArchivoCampos().Tables[0];
                        this.Session["ArchivoCampos"] = dtF;
                        dtB = null;
                        dtF = Main.CargaFormatoArchivo().Tables[0];
                        this.Session["TipoArchivo"] = dtF;
                        dtB = null;
                        dtF = Main.CargaSecuencias().Tables[0];
                        this.Session["Secuencias"] = dtF;

                        this.Session["Response"] = "AltaArchivo.aspx";
                        //Response.Redirect("AltaArchivo.aspx");
                        Server.Transfer("AltaArchivo.aspx");
                    }
                }
            }
        }


        private void ValidaEquipo(string User, string pass)
        {
            try
            {
                string SQL = "UPDATE ZUSUARIOS SET ZLLAVE = '" + this.Session["Registro"].ToString() + "', ZPASSWORD = '' ";
                SQL += "WHERE ZALIAS = '" + User + "'";
                //SQL += " AND ZPASSWORD ='" + pass + "'";

                DBHelper.ExecuteNonQuery(SQL);

            }

            finally
            {
                // Close the stream to the file.
            }
        }

        private void RegistraEquipo(string User, string pass)
        {
            
            this.Session["Registro"] = "0";// egistro = false;

            try
            {
                string clientPCName;
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();

                string Miro = "User = " + User + Environment.NewLine + "Pass = " + pass + Environment.NewLine;
                //Miro += "Remote_host = " + clientPCName + Environment.NewLine;
                //Miro += "MAC = " + GetMAC() + Environment.NewLine;


                //HttpBrowserCapabilities bc = Request.Browser;
                //Miro += "Platform = " + bc.Platform + Environment.NewLine;
                //Miro += "Type = " + bc.Type + Environment.NewLine;
                //Miro += "Name = " + bc.Browser + Environment.NewLine;
                //Miro += "Version = " + bc.Version + Environment.NewLine;
                //Miro += "Usuario = " + User + Environment.NewLine;
                //Miro += "Password = " + pass + Environment.NewLine;

                Miro = Seguridad.cifrar(Miro, false, 0);
                this.Session["Registro"] = Miro;
            }
            catch (Exception ex)
            {
                string Miro = "User = " + User + Environment.NewLine + "Pass = " + pass + Environment.NewLine;
                Miro = Seguridad.cifrar(Miro, false, 0);
                this.Session["Registro"] = Miro;
                string a = Main.ETrazas("", "1", " Login, solicitud de Nombre Equipo --> Error:" + ex.Message);
                
            }
            finally
            {
                // Close the stream to the file.
            }
        }

        private string GetMAC()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
    }
}