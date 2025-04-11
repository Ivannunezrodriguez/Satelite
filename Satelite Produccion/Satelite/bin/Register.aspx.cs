using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.IO;
//using Oracle.DataAccess.Client;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;


namespace Satelite
{
    public partial class Register : System.Web.UI.Page
    {
        DataTable dsUsuarios = new DataTable();
        DataTable dsHash = new DataTable();
        DataTable dsTablas = new DataTable();
        DataTable dsDisco = new DataTable();
        private string INFO_DIR = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000002B.000";
        public static int requestCount;
        string[] MiEquipo = null;
        //Login
        protected void Page_Load(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(GetType(),"Agua", "function();", true);
            //RegistrarScript();
            if (!IsPostBack)
            {
                this.Session["idusuario"] = "";
                this.Session["alias"] = "";
                this.Session["usuario"] = "";
                this.Session["password"] = "";
                this.Session["imgusuario"] = "";
                this.Session["Mail"] = "";
                this.Session["usuarioClave"] = "";

                this.Session["idotheruser"] = "";
                this.Session["otherAlias"] = "";
                this.Session["otheruser"] = "";
                this.Session["password"] = "";
                this.Session["otherpassword"] = "";
                this.Session["imgotherUser"] = "";
                this.Session["otherMail"] = "";
                this.Session["OtherUserClave"] = "";
                this.Session["Otherindice"] = "";
                this.Session["Miindice"] = "";

                this.Session["SessionD"] = "";
                this.Session["idsesion"] = "";
                this.Session["instalacion"] = "";
                this.Session["pks"] = "0"; //false;
                this.Session["pki1"] = "";
                this.Session["pki2"] = "";
                this.Session["pki0"] = "";
                this.Session["pkiM"] = "";
                this.Session["dispositivo1"] = "";
                this.Session["dispositivo2"] = "";
                this.Session["dispositivo3"] = "";

                this.Session["Sube"] = "0";
                this.Session["Publico"] = "0"; //false;
                this.Session["Editar"] = "0"; //false;
                this.Session["Historial"] = "0"; //false;
                this.Session["Personal"] = "0"; //false;
                this.Session["Profesional"] = "0"; //false;
                this.Session["Laboral"] = "0"; //false;
                this.Session["Desempeno"] = "0"; //false;

                this.Session["Academico"] = "0"; //false;
                this.Session["EditKnow"] = "0"; //false;
                this.Session["Registro"] = "0"; //false;
                this.Session["PublicaPersonal"] = "0"; //false;
                this.Session["PublicaProfesional"] = "0"; //false;

                this.Session["Paginacion"] = 0;
                this.Session["PaginaAnt"] = 0;
                this.Session["NumeroAlbaran"] = "0";
                this.Session["NumeroFactura"] = "0";

                this.Session["Collapse"] = 0;
                this.Session["Menu"] = 0;

                this.Session["idSolicitud"] = "0";
                this.Session["idimagen"] = "0";
                this.Session["categoria"] = "0";
                this.Session["micategoria"] = "0";
                this.Session["Mnext"] = "0";
                this.Session["MBefore"] = "0";

                this.Session["MStart"] = "0";
                this.Session["iddefault"] = "0";
                this.Session["LineaPersonal"] = 0;
                this.Session["IDFormulario"] = "0";
                this.Session["IDMiFormulario"] = "0";
                this.Session["IDotherFormulario"] = "0";
                this.Session["idProvincia"] = "0";
                this.Session["idMunicipio"] = "0";

                this.Session["TSuAccion"] = "0";
                this.Session["TMiAccion"] = "0";
                this.Session["MiAccion"] = "0";
                this.Session["MiMacro"] = "0";
                this.Session["MiAccionD"] = "0";
                this.Session["MiMacroD"] = "0";
                this.Session["LineIDForm"] = "0";
                this.Session["FactMoS"] = "0"; //false;
                this.Session["Filesub"] = "";
                this.Session["Intercambia"] = "0"; //false;
                this.Session["Pesada"] = "0"; //false;
                this.Session["Precio"] = "";

                this.Session["Produccion"] = "";

                /////////////////// Esto si queda publicado
                //dsTablas = DBHelper.CargaDataTables("ZTABLAS", "0", HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\" + "0000000000.000","-1", false, 0, HttpContext.Current.Session["Cifrado"].ToString());

                string SQL = "SELECT A.ZID AS ZID_CAMPO, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, B.ZVIEW, D.ZFORMATO ,";
                SQL += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO, B.ZTABLENAME, B.ZTABLEOBJ, A.ZVALIDACION, A.ZVALORDEFECTO, B.ZKEY , C.ZKEY AS KEYCAMPO, B.ZDUPLICADOS, B.ZTIPO AS ZTIPOARCHIVO ";
                SQL += " FROM ZCAMPOS A, ZARCHIVOS B, ZARCHIVOCAMPOS C  , ZTIPOCAMPO D ";
                SQL += " WHERE A.ZID = C.ZIDCAMPO ";
                SQL += " AND  B.ZID = C.ZIDARCHIVO   ";
                SQL += " AND D.ZID = A.ZTIPO ";
                SQL += " AND B.ZESTADO = 2 ";
                SQL += " ORDER BY C.ZORDEN ";
                dsTablas  = Main.BuscaLote(SQL).Tables[0];
                ////////////////////
            }
            else
            {
                //if (Tssu.Text == "" && Tssp.Text == "" && SsID.Text == "" && TssSB.Text == "")
                //{
                    if (Tssu.Text != "" && Tssp.Text != "" &&  Tssd.Text == "" && SsID.Text != "" && TssSB.Text != "")
                    {
                        //Ok
                        //string CC = "document.getElementById('Tssu').value = '" + Variables.instalacion + "'";
                        //string DD = "document.getElementById('Tssp').value = '" + Variables.pkiM + "'";
                        //string EE = "document.getElementById('Tssd').value = '" + Variables.idusuario + "'";
                        //string GG = "document.getElementById('SsID').value = '" + Variables.pki0 + "'";
                        //string FF = "document.getElementById('TssSB').value ='" + Variables.usuario + "'";
                        
                        string clientPCName;
                        string[] computer_name = System.Net.Dns.GetHostEntry(
                        Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                        clientPCName = computer_name[0].ToString();
                        Tssd.Text = Seguridad.cifrar("remote_host = " + clientPCName + Environment.NewLine, false,0);
                        RegistraEquipo();
                        Tssd.Text = DBHelper.Carga_SesionRegistro(Tssu.Text, Tssp.Text, Tssd.Text, SsID.Text, TssSB.Text, MiEquipo);

                        Tssd.Text = this.Session["Produccion"].ToString();
                        Tssu.Text = "";
                        Tssp.Text = "";
                        SsID.Text = "";
                        TssSB.Text = "";
                        RegistraEquipo();
                        if (Tssd.Text == "HomeSite")
                        {
                            Response.Redirect("HomeSite.aspx");
                            return;
                        }
                        else if (Tssd.Text == "Login" || Tssd.Text != "" )
                        {
                        }
                        else
                        {
                            //Tssu.Text = "El Usuario, password o correo, no son correctos.";
                        }
                    }
                    else if (Tssu.Text == "" && Tssp.Text == "" && Tssd.Text == "" && SsID.Text == "" && TssSB.Text == "")
                    {
                        //Descarta al usuario
                        //DBHelper.Borra_SesionRegistro();
                        Response.Redirect("HomeSite.aspx");
                        return;
                    }
                    else
                    {
                        Tssd.Text = this.Session["Produccion"].ToString(); ;
                        return;
                    }     
                //}
                //else
                //{
                //    Response.Redirect("HomeSite.aspx");
                //    return;
                //}
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

        protected void MaxParam()
        {
            //OracleParameter[] dbParams = new OracleParameter[0];

            //string StR2 = "SELECT MIN(ZMINIMO) FROM USER_GEDESPOL.ZMAXMIN  ";
            //this.Session["Minimo"]  = Convert.ToString(DBHelper.ExecuteScalar(StR2, dbParams));

            //StR2 = "SELECT MIN(ZMAXIMO) FROM USER_GEDESPOL.ZMAXMIN  ";
            //this.Session["Maximo"] = Convert.ToString(DBHelper.ExecuteScalar(StR2, dbParams));

        }


        protected void toggleFullscreen(object sender, EventArgs e)
        {
            //Validacion desde equipo
            Tssd.Text = this.Session["Produccion"].ToString();
            //this.Session["idsesion"] = Tssu.Text;
            //this.Session["idusuario"] = Tssp.Text;
            //this.Session["pki2"] = Tssd.Text;
            //this.Session["pkiM"] = SsID.Text;
            //this.Session["Sube"] = TssSB.Text;
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
        public string GetIpAddress()
        {
            return IPHelper.GetIPAddress(HttpContext.Current.Request.ServerVariables["HTTP_VIA"],
                            HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                            HttpContext.Current.Request.UserHostAddress);

        }
        public string GetIpAddress(System.Web.HttpRequest request)
        {

            // Recuperamos la IP de la máquina del cliente
            // Primero comprobamos si se accede desde un proxy
            string ipAddress1 = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            // Acceso desde una máquina particular
            string ipAddress2 = request.ServerVariables["REMOTE_ADDR"];
            string ipAddress = string.IsNullOrEmpty(ipAddress1) ? ipAddress2 : ipAddress1;
            // Devolvemos la ip
            return ipAddress;


        }

        private string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new
                WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        public void RegistraEquipo()
        {
            this.Session["Registro"] = "0";// egistro = false;
            string Miro = "";

            int requestNumber = Interlocked.Increment(ref requestCount);

            try
            {
                // <snippet2>
                // Write request information to the file with HTML encoding.
                //this.Session["idsesion"] = Tssu.Text;
                //this.Session["idusuario"] = Tssp.Text;
                //this.Session["pki2"] = Tssd.Text;
                //this.Session["pkiM"] = SsID.Text;
                //this.Session["Sube"] = TssSB.Text;

                Miro = "Alias = " + this.Session["alias"].ToString() + Environment.NewLine;
                Miro += "Email = " + this.Session["Mail"].ToString() + Environment.NewLine;
                Miro += "Pass = " + this.Session["password"].ToString() + Environment.NewLine;
                Miro += "IP = " + GetIpAddress() + Environment.NewLine;
                Miro += "IP externa = " + getExternalIp() + Environment.NewLine;
                string clientPCName;
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();
                Miro += "remote_host = " + clientPCName + Environment.NewLine;
                Miro += "MAC = " + GetMAC() + Environment.NewLine;

                Miro = Seguridad.cifrar(Miro, false, 0);

                //Miro += Server.HtmlEncode(requestNumber.ToString()) + Environment.NewLine;
                //Miro += Server.HtmlEncode(DateTime.Now.ToString()) + Environment.NewLine;
                //Miro += Server.HtmlEncode(Request.CurrentExecutionFilePath) + Environment.NewLine;
                //Miro += Server.HtmlEncode(Request.ApplicationPath) + Environment.NewLine;
                //Miro += Server.HtmlEncode(Request.FilePath) + Environment.NewLine;
                //Miro += Server.HtmlEncode(Request.Path) + Environment.NewLine;
                //Miro += "" + Environment.NewLine;
                //HttpBrowserCapabilities bc = Request.Browser;
                //Miro += "Browser Capabilities:" + Environment.NewLine;
                //Miro += "Platform = " + bc.Platform + Environment.NewLine;
                //Miro += "Type = " + bc.Type + Environment.NewLine;
                //Miro += "Name = " + bc.Browser + Environment.NewLine;
                //Miro += "Version = " + bc.Version + Environment.NewLine;
                //Miro += "ECMA Script Version = " + bc.EcmaScriptVersion.ToString() + Environment.NewLine;
                //Miro += "Minor Version = " + bc.MinorVersion + Environment.NewLine;
                //Miro += "Platform = " + bc.Platform + Environment.NewLine;
                //Miro += "Is Beta = " + bc.Beta + Environment.NewLine;
                //Miro += "Is Crawler = " + bc.Crawler + Environment.NewLine;
                //Miro += "Is AOL = " + bc.AOL + Environment.NewLine;
                //Miro += "Is Win16 = " + bc.Win16 + Environment.NewLine;
                //Miro += "Is Win32 = " + bc.Win32 + Environment.NewLine;
                //Miro += "Supports Frames = " + bc.Frames + Environment.NewLine;
                //Miro += "Supports Tables = " + bc.Tables + Environment.NewLine;
                //Miro += "Supports Cookies = " + bc.Cookies + Environment.NewLine;
                //Miro += "Supports VB Script = " + bc.VBScript + Environment.NewLine;
                //Miro += "Supports JavaScript = " + bc.JavaScript + Environment.NewLine;
                //Miro += "Supports Java Applets = " + bc.JavaApplets + Environment.NewLine;
                //Miro += "Supports ActiveX Controls = " + bc.ActiveXControls + Environment.NewLine;
                //Miro += "CDF = " + bc.CDF + Environment.NewLine;
                //Miro += "Agentes de Usuario = " + HttpContext.Current.Request.UserAgent + Environment.NewLine;
                //Miro += "" + Environment.NewLine;
                //// <snippet8>
                //// Iterate through the UserLanguages collection and
                //// write its HTML encoded values to the file.
                //for (langCount = 0; langCount < Request.UserLanguages.Length; langCount++)
                //{
                //    Miro += "User Language " + langCount + ": " + Server.HtmlEncode(Request.UserLanguages[langCount]) + Environment.NewLine;
                //}
            }

            finally
            {
                // Close the stream to the file.
                MiEquipo = System.Text.RegularExpressions.Regex.Split(Miro, Environment.NewLine);
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ////carga resultado de clase Login.cs
            ////string var = Seguridad.Encriptar(txtPassword.Text + txtUserID.Text);
            //string var = txtPassword.Text;
            //DataSet ds = Login.ValidarRegistro(txtUserID.Text, var, dsTablas, dsDisco);

            ////if (Variables.idusuario != "" && Variables.pki1 != "" && Variables.pks == true)
            ////{
            //if (this.Session["idusuario"].ToString() != "" && this.Session["pki1"].ToString() != "" && this.Session["pks"].ToString() == "1")//true
            //{
            //    //Variables.SessionD = DBHelper.Carga_Sesion(Tssu.Text, Tssp.Text, Tssd.Text);
            //    this.Session["idSolicitud"] = "0";
            //    //Variables.idSolicitud = "0";
            //    Response.Redirect("Fabrica.aspx");
            //}
            //else
            //{
            //    lbErr.Text = "Usuario o password incorrectos.";
            //}
        }


            //DataTable dt = ds.Tables[0];
            //if (dt.Rows.Count == 0)
            //{
            //    //messageBox.ShowMessage("Usuario y/o Password incorrectos");
            //}
            //else
            //{
            //    this.Session["Session"] = dt.Rows[0]["ZPASSWORD"].ToString();
            //    this.Session["UserID"] = dt.Rows[0]["ZID"].ToString();
            //    this.Session["UserName"] = dt.Rows[0]["ZNOMBRE"].ToString();
            //    this.Session["UserAlias"] = dt.Rows[0]["ZALIAS"].ToString();
            //    this.Session["UserDescripcion"] = dt.Rows[0]["ZDESCRIPCION"].ToString();
            //    this.Session["UserPassword"] = dt.Rows[0]["ZPASSWORD"].ToString();
            //    this.Session["UserIdentificacion"] = dt.Rows[0]["ZDNI"].ToString();
            //    this.Session["MiCodigo"] = dt.Rows[0]["ZCOD"].ToString();
            //    this.Session["MiIG"] = dt.Rows[0]["NIVEL_DE_EVALUACION"].ToString();
            //    this.Session["MiPlantilla"] = dt.Rows[0]["PLANTILLA"].ToString();
            //    this.Session["MiCargo"] = dt.Rows[0]["ZPUESTO_TRABAJO"].ToString();
            //    this.Session["MiNivel"] = dt.Rows[0]["ZNIVEL"].ToString();
            //    this.Session["MiComisaria"] = dt.Rows[0]["PLANTILLA"].ToString();
            //    this.Session["Edicion"] = "0";
            //    this.Session["TipoConexion"] = "1";
            //    this.Session["MiFlujoEstado"] = "0";

            //    if (this.Session["UserAlias"].ToString() == "COD.SP")
            //    {
            //        Response.Redirect("InstanciaR.aspx");
            //        return;
            //    }
                

            //    if (txtUserID.Text == "Administrador")
            //    {
            //        Response.Redirect("Main.aspx");
            //    }
            //    else
            //    {
            //        //CARGA LAS AREAS Y COMPETENCIAS
            //            //si no existen Areas

            //                ////CARGA EL USUARIO QUE HACE LOGIN CON PERMISOS
            //                ///
            //                MaxParam();

            //                DataTable dt1 = new DataTable();
            //                dt1 = null;// Main.CargaMiUserEval().Tables[0];
            //                this.Session["UsuarioEvaluacion"] = dt1;
            //                this.Session["Response"] = "ObsEvaluados.aspx";
            //                Response.Redirect("~/Site.aspx"); //Default
            //                //Response.Redirect("ObsEvaluados.aspx");
            //    }
            //}
        //}

    }
}