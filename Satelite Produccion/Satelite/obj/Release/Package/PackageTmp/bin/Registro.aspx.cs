using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using Winista.Mime;
using System.IO;
using System.Threading;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;

namespace Satelite
{
    public partial class Registro : Page
    {
        private string INFO_DIR = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000002B.000";
        public static int requestCount;
        DataTable dsTablas = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
       {
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
                this.Session["PublicaComentarios"] = "0"; //false;

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
                this.Session["iddefault"] = "";
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
            }
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
        private void btnSendInfo_Click(object sender, System.EventArgs e)
        {
            lblMessage.Text = "Hola, " + Server.HtmlEncode(TxtAlias.Text) +
              ". Se ha creado el fichero de información.";
        }

        private void RegistraEquipo()
        {
            this.Session["Registro"] = "0";// egistro = false;

            //int langCount;
            StreamWriter swA = File.CreateText(HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + this.Session["idusuario"].ToString() + "\\0000000020.000");
            try
            {
                swA.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
                swA.Close();
            }
            catch
            {
                swA.Close();
            }

            int requestNumber = Interlocked.Increment(ref requestCount);

            // Create the file to contain information about the request.
            string strFilePath = INFO_DIR; //+ requestNumber.ToString() + @".txt";


            StreamWriter sw = File.CreateText(strFilePath);
            try
            {
                // <snippet2>
                // Write request information to the file with HTML encoding.
                string Miro = "Alias = " + TxtAlias.Text + Environment.NewLine;
                Miro += "Email = " + txtEmail.Text + Environment.NewLine;
                Miro += "Pass = " + TextPass.Text + Environment.NewLine;
                Miro += "IP = " + GetIpAddress() + Environment.NewLine;
                Miro += "IP externa = " + getExternalIp() + Environment.NewLine;

                string clientPCName;
                string[] computer_name = System.Net.Dns.GetHostEntry(
                Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                clientPCName = computer_name[0].ToString();

                Miro += "remote_host = " + clientPCName + Environment.NewLine;
                Miro += "MAC = " + GetMAC() + Environment.NewLine;
                

                HttpBrowserCapabilities bc = Request.Browser;
                Miro += "Platform = " + bc.Platform + Environment.NewLine;
                Miro += "Type = " + bc.Type + Environment.NewLine;
                Miro += "Name = " + bc.Browser + Environment.NewLine;
                Miro += "Version = " + bc.Version + Environment.NewLine;

                sw.WriteLine(Seguridad.cifrar(Miro, false, 0));

                //sw.WriteLine("Browser Capabilities:");
                //sw.WriteLine("Platform = " + bc.Platform);
                //sw.WriteLine("Type = " + bc.Type);
                //sw.WriteLine("Name = " + bc.Browser);
                //sw.WriteLine("Version = " + bc.Version);

                //sw.WriteLine("Alias = " + TxtAlias.Text);
                //sw.WriteLine("Email = " + txtEmail.Text);
                //sw.WriteLine("Pass = " + TextPass.Text);
                //sw.WriteLine("IP = " + GetIpAddress());
                //sw.WriteLine("IP externa = " + getExternalIp());
                //string clientPCName;
                //string[] computer_name = System.Net.Dns.GetHostEntry(
                //Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
                //clientPCName = computer_name[0].ToString();
                //sw.WriteLine("remote_host = " + clientPCName);
                //sw.WriteLine("MAC = " + GetMAC());



                //sw.WriteLine(Server.HtmlEncode(requestNumber.ToString()));
                //sw.WriteLine(Server.HtmlEncode(DateTime.Now.ToString()));
                //sw.WriteLine(Server.HtmlEncode(Request.CurrentExecutionFilePath));
                //sw.WriteLine(Server.HtmlEncode(Request.ApplicationPath));
                //sw.WriteLine(Server.HtmlEncode(Request.FilePath));
                //sw.WriteLine(Server.HtmlEncode(Request.Path));
                //sw.WriteLine("");
                //HttpBrowserCapabilities bc = Request.Browser;
                //sw.WriteLine("Browser Capabilities:");
                //sw.WriteLine("Platform = " + bc.Platform);
                //sw.WriteLine("Type = " + bc.Type);
                //sw.WriteLine("Name = " + bc.Browser);
                //sw.WriteLine("Version = " + bc.Version);
                //sw.WriteLine("ECMA Script Version = " + bc.EcmaScriptVersion.ToString());
                //sw.WriteLine("Minor Version = " + bc.MinorVersion);
                //sw.WriteLine("Platform = " + bc.Platform);
                //sw.WriteLine("Is Beta = " + bc.Beta);
                //sw.WriteLine("Is Crawler = " + bc.Crawler);
                //sw.WriteLine("Is AOL = " + bc.AOL);
                //sw.WriteLine("Is Win16 = " + bc.Win16);
                //sw.WriteLine("Is Win32 = " + bc.Win32);
                //sw.WriteLine("Supports Frames = " + bc.Frames);
                //sw.WriteLine("Supports Tables = " + bc.Tables);
                //sw.WriteLine("Supports Cookies = " + bc.Cookies);
                //sw.WriteLine("Supports VB Script = " + bc.VBScript);
                //sw.WriteLine("Supports JavaScript = " + bc.JavaScript);
                //sw.WriteLine("Supports Java Applets = " + bc.JavaApplets);
                //sw.WriteLine("Supports ActiveX Controls = " + bc.ActiveXControls);
                //sw.WriteLine("CDF = " + bc.CDF);
                //sw.WriteLine("Agentes de Usuario = " + HttpContext.Current.Request.UserAgent);
                //sw.WriteLine("");
                //// <snippet8>
                //// Iterate through the UserLanguages collection and
                //// write its HTML encoded values to the file.
                //for (langCount = 0; langCount < Request.UserLanguages.Length; langCount++)
                //{
                //    sw.WriteLine(@"User Language " + langCount + ": " + Server.HtmlEncode(Request.UserLanguages[langCount]));
                //}
            }

            finally
            {
                // Close the stream to the file.
                sw.Close();
            }
        }

        //private void RegistraEquipo() //Aqui todo
        //{
        //    this.Session["Registro"] = "0";// egistro = false;

        //    int langCount;

        //    int requestNumber = Interlocked.Increment(ref requestCount);

        //    // Create the file to contain information about the request.
        //    string strFilePath = INFO_DIR; //+ requestNumber.ToString() + @".txt";


        //    StreamWriter sw = File.CreateText(strFilePath);

        //    try
        //    {
        //        // <snippet2>
        //        // Write request information to the file with HTML encoding.

        //        sw.WriteLine("Alias = " + TxtAlias.Text);
        //        sw.WriteLine("Email = " + txtEmail.Text);
        //        sw.WriteLine("Pass = " + TextPass.Text);
        //        sw.WriteLine("IP = " + GetIpAddress());
        //        sw.WriteLine("IP externa = " + getExternalIp());
        //        string clientPCName;
        //        string[] computer_name = System.Net.Dns.GetHostEntry(
        //        Request.ServerVariables["remote_host"]).HostName.Split(new Char[] { '.' });
        //        clientPCName = computer_name[0].ToString();
        //        sw.WriteLine("remote_host = " + clientPCName);
        //        sw.WriteLine("MAC = " + GetMAC());


        //        sw.WriteLine(Server.HtmlEncode(requestNumber.ToString()));
        //        sw.WriteLine(Server.HtmlEncode(DateTime.Now.ToString()));
        //        sw.WriteLine(Server.HtmlEncode(Request.CurrentExecutionFilePath));
        //        sw.WriteLine(Server.HtmlEncode(Request.ApplicationPath));
        //        sw.WriteLine(Server.HtmlEncode(Request.FilePath));
        //        sw.WriteLine(Server.HtmlEncode(Request.Path));
        //        sw.WriteLine("");
        //        HttpBrowserCapabilities bc = Request.Browser;
        //        sw.WriteLine("Browser Capabilities:");
        //        sw.WriteLine("Platform = " + bc.Platform);
        //        sw.WriteLine("Type = " + bc.Type);
        //        sw.WriteLine("Name = " + bc.Browser);
        //        sw.WriteLine("Version = " + bc.Version);
        //        sw.WriteLine("ECMA Script Version = " + bc.EcmaScriptVersion.ToString());
        //        sw.WriteLine("Minor Version = " + bc.MinorVersion);
        //        sw.WriteLine("Platform = " + bc.Platform);
        //        sw.WriteLine("Is Beta = " + bc.Beta);
        //        sw.WriteLine("Is Crawler = " + bc.Crawler);
        //        sw.WriteLine("Is AOL = " + bc.AOL);
        //        sw.WriteLine("Is Win16 = " + bc.Win16);
        //        sw.WriteLine("Is Win32 = " + bc.Win32);
        //        sw.WriteLine("Supports Frames = " + bc.Frames);
        //        sw.WriteLine("Supports Tables = " + bc.Tables);
        //        sw.WriteLine("Supports Cookies = " + bc.Cookies);
        //        sw.WriteLine("Supports VB Script = " + bc.VBScript);
        //        sw.WriteLine("Supports JavaScript = " + bc.JavaScript);
        //        sw.WriteLine("Supports Java Applets = " + bc.JavaApplets);
        //        sw.WriteLine("Supports ActiveX Controls = " + bc.ActiveXControls);
        //        sw.WriteLine("CDF = " + bc.CDF);
        //        sw.WriteLine("Agentes de Usuario = " + HttpContext.Current.Request.UserAgent);
        //        sw.WriteLine("");
        //        int loop1, loop2;
        //        NameValueCollection coll;

        //        // Load ServerVariable collection into NameValueCollection object.
        //        coll = Request.ServerVariables;
        //        // Get names of all keys into a string array.
        //        String[] arr1 = coll.AllKeys;
        //        for (loop1 = 0; loop1 < arr1.Length; loop1++)
        //        {
        //            //sw.WriteLine(arr1[loop1] );
        //            String[] arr2 = coll.GetValues(arr1[loop1]);
        //            for (loop2 = 0; loop2 < arr2.Length; loop2++)
        //            {
        //                sw.WriteLine(arr1[loop1] + " = " + loop2 + ": " + Server.HtmlEncode(arr2[loop2]));
        //            }
        //        }
        //        // </snippet2>

        //        // <snippet3>
        //        // Iterate through the Form collection and write
        //        // the values to the file with HTML encoding.
        //        // String[] formArray = Request.Form.AllKeys;
        //        foreach (string s in Request.Form)
        //        {
        //            sw.WriteLine("Form: " + Server.HtmlEncode(s));
        //        }
        //        // </snippet3>
        //        sw.WriteLine("");
        //        // <snippet4>
        //        // Write the PathInfo property value
        //        // or a string if it is empty.
        //        if (Request.PathInfo == String.Empty)
        //        {
        //            sw.WriteLine("The PathInfo property contains no information.");
        //        }
        //        else
        //        {
        //            sw.WriteLine(Server.HtmlEncode(Request.PathInfo));
        //        }
        //        // </snippet4>
        //        sw.WriteLine("");
        //        // <snippet5>
        //        // Write request information to the file with HTML encoding.
        //        sw.WriteLine(Server.HtmlEncode(Request.PhysicalApplicationPath));
        //        sw.WriteLine(Server.HtmlEncode(Request.PhysicalPath));
        //        sw.WriteLine(Server.HtmlEncode(Request.RawUrl));
        //        // </snippet5>
        //        sw.WriteLine("");
        //        // <snippet6>
        //        // Write a message to the file dependent upon
        //        // the value of the TotalBytes property.
        //        if (Request.TotalBytes > 1000)
        //        {
        //            sw.WriteLine("The request is 1KB or greater");
        //        }
        //        else
        //        {
        //            sw.WriteLine("The request is less than 1KB");
        //        }
        //        // </snippet6>

        //        // <snippet7>
        //        // Write request information to the file with HTML encoding.
        //        sw.WriteLine("");
        //        sw.WriteLine(Server.HtmlEncode(Request.RequestType));
        //        sw.WriteLine(Server.HtmlEncode(Request.UserHostAddress));
        //        sw.WriteLine(Server.HtmlEncode(Request.UserHostName));
        //        sw.WriteLine(Server.HtmlEncode(Request.HttpMethod));
        //        // </snippet7>
        //        sw.WriteLine("");
        //        // <snippet8>
        //        // Iterate through the UserLanguages collection and
        //        // write its HTML encoded values to the file.
        //        for (langCount = 0; langCount < Request.UserLanguages.Length; langCount++)
        //        {
        //            sw.WriteLine(@"User Language " + langCount + ": " + Server.HtmlEncode(Request.UserLanguages[langCount]));
        //        }
        //        // </snippet8>
        //    }

        //    finally
        //    {
        //        // Close the stream to the file.
        //        sw.Close();
        //    }

        //    //lblMessage.Text = "Information about this request has been sent to a file.";
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int Miro = 0;
            this.Session["Pesada"] = "0"; //= false;
            /////////////////// Esto si queda publicado
            try
            {
                if (Variables.configuracionDB == 0)
                {
                    if (txtCaptcha.Text == Session["CaptchaCode"].ToString())
                    {
                        if (txtEmail.Text != "" && TextPass.Text != "" && TxtAlias.Text != "")
                        {
                            //dsTablas = DBHelper.CargaDataTables("ZTABLAS", "0", HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\" + "0000000000.000", "-1", false, 0, HttpContext.Current.Session["Cifrado"].ToString());
                            Miro = DBHelper.BuscaMailAlias(TxtAlias.Text, txtEmail.Text, TextPass.Text, 0);
                            if (Miro == -1)
                            {
                                //Correcto
                                string var = TextPass.Text;
                                System.Data.DataSet ds = Login.ValidarLogin(TxtAlias.Text, var);
                                this.Session["idSolicitud"] = "0";
                                if (this.Session["idusuario"].ToString() != "" && this.Session["pki1"].ToString() != "" && this.Session["pks"].ToString() == "1") // true)
                                {
                                    INFO_DIR = HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + this.Session["idusuario"].ToString() + "\\000000002B.000";
                                    RegistraEquipo();
                                    this.Session["Sube"] = "0";
                                    Response.Redirect("Fabrica.aspx");
                                    return;
                                }

                            }
                            else if (Miro == 1)
                            {
                                lblMessage.Text = "¡¡¡La cuenta de correo ya está en uso!!!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                txtCaptcha.Text = "";
                            }
                            else if (Miro == 2)
                            {
                                lblMessage.Text = "¡¡¡El Alias ya está en uso!!!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                txtCaptcha.Text = "";
                            }
                            else
                            {
                                lblMessage.Text = "¡¡¡Por favor, Complete los datos!!!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                txtCaptcha.Text = "";
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "¡¡¡Por favor, introduzca correctamente la cadena de caracteres que aparecen en la imagen Capcha!!!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        txtCaptcha.Text = "";
                    }
                }
                else
                {
                    //Comprueba si no existen los datos
                    DataSet ds = null;
                    DataTable dt = null;

                    if (txtCaptcha.Text == Session["CaptchaCode"].ToString())
                    {
                        if (txtEmail.Text != "" && TextPass.Text != "" && TxtAlias.Text != "")
                        {
                            //DataSet ds = null;
                            string cc = ConfigurationManager.AppSettings.Get("connectionSQL");
                            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                            SqlCommand comando;
                            DataSet MyDataSet = new DataSet();
                            string Miquery = " Select A.ZCODIGO, A.ZALIAS, A.ZPASSWORD, A.ZROOT, A.ZKEY, A.ZNIVEL, B.ZID_TABLA, B.ZID_REGISTRO ";
                            Miquery += " from ZUSUARIOS A, ZLLAVES B ";
                            Miquery += " where A.ZALIAS = '" + TxtAlias.Text + "'";
                            Miquery += " and A.ZPASSWORD ='" + TextPass.Text + "'";
                            Miquery += " and A.ZMAIL ='" + txtEmail.Text + "'";
                            //Miquery += " and B.ZID_USER = A.ZID ";txtEmail.Text
                            Miquery += " and B.ZID = A.ZKEY ";

                            comando = new SqlCommand(Miquery, cn);
                            comando.CommandType = CommandType.Text;

                            if (cn.State == 0) cn.Open();
                            SqlDataAdapter Da = new SqlDataAdapter(comando);
                            ds = new DataSet();
                            Da.Fill(ds);
                            //ListUser.Add(ds);
                            dt = ds.Tables[0];
                            cn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string miro = ex.Message; 
            }
        }
    }
}