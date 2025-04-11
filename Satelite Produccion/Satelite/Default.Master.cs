using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Management;
using System.Reflection.Emit;
using System.Web.UI.HtmlControls;

namespace Satelite
{
    public partial class Default : System.Web.UI.MasterPage
    {
        //public bool NavegadorVisible
        //{
        //    get { return DivEstructura.Visible; }
        //    set { DivEstructura.Visible = value; }
        //}

        // o si deseas todo el div
        //public HtmlGenericControl Navegador => navegador;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.Session["MiNivel"].ToString() == "9")
            //{
            //    Nominas.Visible = true;
            //}

            this.Session["Procesa"] = "0";

            if (Session["Session"] == null)
            {
                this.Session["Error"] = "0";
                //Response.Redirect("Login.aspx"); //Default
                Server.Transfer("Login.aspx");
            }
            this.Session["MenuUsuario"] = "";
            if (this.Session["MiNivel"].ToString() == "9")
            {
                //Nominas.Visible = true;
                //AEstructura.Visible = true;
                ////AImportaciones.Visible = true;
                //BtablaVal.Visible = true;
                //BTablaArchivo.Visible = true;
                //AConsulta.Visible = true;
                //BTablaMenus.Visible = true;

                //Pedido por Cecilia por wassap 06/10/2023
                if (this.Session["UserAlias"].ToString() == "Jose")
                {
                    //AUbicaFisi.Visible = true;
                }
            }

            if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
            {
                this.Session["Error"] = "0";
                Server.Transfer("Login.aspx"); //Default
            }


            if (this.Session["DESARROLLO"].ToString() == "DESARROLLO")
            {
                H3Titulo.Visible = false;
                H3Desarrollo.Visible = true;
                //if(this.Session["MiCodigo"].ToString() == "00001")
                //{
                //    BtUdTables.Visible = true;
                //}
            }
            else
            {
                H3Titulo.Visible = true;
                H3Desarrollo.Visible = false;
                //BtUdTables.Visible = false;
            }
            if (!IsPostBack)
            {
                //this.Session["Menu"] = "";
            }
            //ElTituloMenu(this.Session["MiMenu"].ToString());

            ElTituloMenu(Request.Url.AbsoluteUri);
             
            //if(this.Session["MiMenu"].ToString() == "OrdenCompra")
            //{
            //    //wrapper.Visible = false;
            //    //this.Session["MiMenu"] = "";
            //}
            //else
            //{
            //    wrapper.Visible = true;
            //}

            //DataSet ds = new DataSet();
            //ds = DataProvider.Connect_Select("SELECT * FROM Menu");
            //DataTable table = ds.Tables[0];
            //DataRow[] parentMenus = table.Select("ParentId = 0");

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

            if(MiInto != "")
            {
                //Aqui creo que deberia filtrar por grupos
                SQL = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                SQL += " FROM ZMENU AA ";
                SQL += " WHERE AA.ZESTADO <> 3 ";
                SQL += "AND AA.ZID IN (" + MiInto + ") ";
                SQL += "ORDER BY ZID, ZROOT";


                //SQL = "SELECT AA.ZID, AA.ZTITULO, AA.ZDESCRIPCION, AA.ZPAGINA, AA.ZROOT, AA.ZSUBMENU, ";
                //SQL += " (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                //SQL += " FROM ZMENU AA ";
                //SQL += " ZUSUARIOGRUPO BB, ";
                //SQL += " ZMENUPERMISOSGRUPO CC ";
                //SQL += " WHERE AA.ZESTADO <> 3 ";
                //SQL += "AND AA.ZID IN (" + MiInto + ") ";
                //SQL += "ORDER BY AA.ZID, AA.ZROOT";
                DataTable table = Main.BuscaLote(SQL).Tables[0];
                DataRow[] parentMenus = table.Select("ZROOT = 0");

                var sb = new StringBuilder();
                string unorderedList = GenerateUL(parentMenus, table, sb);

                navwrapper.InnerHtml = unorderedList;
                this.Session["MenuUsuario"] = unorderedList;
            }
            else
            {
                navwrapper.InnerHtml = "";
                this.Session["MenuUsuario"] = "";
            }
            //Response.Write(unorderedList);

            //Llamadas a los USB
            //Usb oUsb = new Usb();
            //List<USBInfo> lstUSBD = oUsb.GetUSBDevices();

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
        public class USBInfo
        {

            //constructor
            public USBInfo(string deviceID, string pnpDeviceID, string description)
            {
                this.DeviceID = deviceID;
                this.PnpDeviceID = pnpDeviceID;
                this.Description = description;
            }

            /// <summary>
            /// Device ID
            /// </summary>
            public string DeviceID { get; private set; }

            /// <summary>
            /// Pnp Device Id
            /// </summary>
            public string PnpDeviceID { get; private set; }

            /// <summary>
            /// Descripción del dispositivo o nombre
            /// </summary>
            public string Description { get; private set; }
        }

        //Clase que se encarga de obtener los dispositivos
        public class Usb
        {

            /// <summary>
            /// obtiene las usb de la computadora
            /// </summary>
            /// <returns></returns>
            public List<USBInfo> GetUSBDevices()
            {

                //creamos una lista de USBInfo
                List<USBInfo> lstDispositivos = new List<USBInfo>();

                //creamos un ManagementObjectCollection para obtener nuestros dispositivos
                ManagementObjectCollection collection;

                //utilizando la WMI clase Win32_USBHub obtenemos todos los dispositivos USB
                using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))

                    //asignamos los dispositivos a nuestra coleccion
                    collection = searcher.Get();

                //recorremos la colección
                foreach (var device in collection)
                {
                    //asignamos el dispositivo a nuestra lista
                    lstDispositivos.Add(new USBInfo(
                    (string)device.GetPropertyValue("DeviceID"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    (string)device.GetPropertyValue("Description")
                    ));
                }

                //liberamos el objeto collection
                collection.Dispose();
                //regresamos la lista
                return lstDispositivos;
            }
        }
        private void ElTituloMenu(string Miro)
        {
            TodoMenuDefault();

            //Carga Menus y compone el titulo
            string SQL = "SELECT F.ZID, F.ZTITULO, F.ZDESCRIPCION, F.ZPAGINA, F.ZROOT ";
            SQL += " FROM  ZMENU F ";
            SQL += " WHERE ZESTADO <> 3 ";
            SQL += " ORDER BY  F.ZID ";
            DataTable Mitable = Main.BuscaLote(SQL).Tables[0];
            string a = System.IO.Path.GetFileName(Miro);

            StringBuilder sbDivIndirizzi = new StringBuilder();
            if (this.Session["DESARROLLO"].ToString() != "DESARROLLO")
            {
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    if (Miro.ToLower().Contains("firma"))
                    {
                        sbDivIndirizzi.Append("Firma digital a mano alzada para Documentación electronica (WACOM) <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Firma documentación autocomplementada única/múltiple con tan solo una rublica y desde memoria aquellos documentos que esten seleccionados por el conjunto de los allí firmantes. Puede completar el cierre del documento con Certificado digital FMT ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") (" + this.Session["Mydb"].ToString() + ") </small>");
                        H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                        //ANominas.Attributes["class"] = "active";
                        //UNominas.Attributes["class"] = "nav nav-second-level active";
                        //LinkFirma.Attributes["class"] = "nav nav-second-level active";

                    }
                }
                else
                {
                    foreach (DataRow fila in Mitable.Rows)
                    {
                        if (a == fila["ZPAGINA"].ToString())
                        {
                            sbDivIndirizzi.Append( fila["ZTITULO"].ToString() + ",  " + fila["ZDESCRIPCION"].ToString() + ", (" + this.Session["ComputerName"].ToString() + ") (" + this.Session["Mydb"].ToString() + ") ");
                            H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                            H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                            break;
                        }
                    }

                    //if (Miro.ToLower().Contains("flujosdatos"))
                    //{
                    //    sbDivIndirizzi.Append("Consulta Flujos <i class=\"fa fa-long-arrow-right\"></i> <small> \"  “Consulta General para Flujos de Trabajo ”\"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}


                    //if (Miro.ToLower().Contains("default"))
                    //{
                    //    sbDivIndirizzi.Append("Articulos OnLine <i class=\"fa fa-long-arrow-right\"></i> <small> \"  “Gestión de Articulos por Categorias ”\"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro.ToLower().Contains("nomina"))
                    //{
                    //    sbDivIndirizzi.Append("Nóminas RecoDat <i class=\"fa fa-long-arrow-right\"></i> <small> \"  “Gestión de Empleados, Jornal, Destajo y Tareas ”\"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro.ToLower().Contains("lotesautomanualssssss"))
                    //{
                    //    sbDivIndirizzi.Append("Generación de lotes automáticos <i class=\"fa fa-long-arrow-right\"></i> <small> \"Para generar lotes de forma automática e imprimir código QR \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro.ToLower().Contains("lotesautomanuals"))
                    //{
                    //    sbDivIndirizzi.Append("(Generación de lotes automáticos Documental<i class=\"fa fa-long-arrow-right\" ></i> <small> \"Para generar lotes de forma automática e imprimir código QR desde una Tabla de Archivos Documentales  \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro.ToLower().Contains("ordencompra"))
                    //{
                    //    sbDivIndirizzi.Append("Órdenes de Compra <i class=\"fa fa-long-arrow-right\"></i> <small> \"Pedidos pendientes con orden de compra \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro.ToLower().Contains("inicio") == true || Miro =="")
                    //{
                    //    sbDivIndirizzi.Append("Panel de Control <i class=\"fa fa-long-arrow-right\"></i> <small> \"Estadísticas \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AInicio.Attributes["class"] = "active";
                    //}
                    //if (Miro.ToLower().Contains("altacampos"))
                    //{
                    //    sbDivIndirizzi.Append("Panel de Campos <i class=\"fa fa-long-arrow-right\"></i> <small> \"Campos creados para la Estructura de Información \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Consulta")
                    //{
                    //    sbDivIndirizzi.Append("Búsqueda de Contenidos de Información <i class=\"fa fa-long-arrow-right\"></i> <small> \"Selección de listados sobre tablas \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AConsulta.Attributes["class"] = "active";
                    //    //UConsulta.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "ConsultaFlujo")
                    //{
                    //    sbDivIndirizzi.Append("Búsqueda de Contenidos de Información en Flujos de Trabajo <i class=\"fa fa-long-arrow-right\"></i> <small> \"Selección de listados sobre Flujos \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AConsulta.Attributes["class"] = "active";
                    //    //UConsulta.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "OrdenCarga")
                    //{
                    //    sbDivIndirizzi.Append("Órdenes de Carga <i class=\"fa fa-long-arrow-right\"></i> <small> \" Selección de listados pendientes de Lotes \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AExpediciones.Attributes["class"] = "active";
                    //    //UExpediciones.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "RevisionLotes")
                    //{
                    //    sbDivIndirizzi.Append(" Revisión de lotes <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Revisión lotes y preparación importación a GoldenSoft \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "RevisionLotesS")
                    //{
                    //    sbDivIndirizzi.Append(" Revisión de lotes <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Revisión lotes y preparación importación a GoldenSoft \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "AltaArchivos")
                    //{
                    //    sbDivIndirizzi.Append("Relación de Archivos documentales <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Crea, vincula o desvincula Tablas y columnas o campos relacionales” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Firma")
                    //{
                    //    sbDivIndirizzi.Append("Firma digital a mano alzada para Documentación electronica (WACOM) <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Firma documentación autocomplementada única/múltiple con tan solo una rublica y desde memoria aquellos documentos que esten seleccionados por el conjunto de los allí firmantes. Puede completar el cierre del documento con Certificado digital FMT ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Flujo")
                    //{
                    //    sbDivIndirizzi.Append("Flujos de Trabajo y Estados de Flujo <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición y diseño de Flujos de Trabajo.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "ImportTabla")
                    //{
                    //    sbDivIndirizzi.Append("Migrar Tablas de un origen a un Destino <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de la estructura a migrar.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AImportaciones.Attributes["class"] = "active";
                    //    //UImportaciones.Attributes["class"] = "nav nav-second-level";
                    //}
                    //if (Miro == "TablaValidacion")
                    //{
                    //    sbDivIndirizzi.Append("Configura Tablas de Validación <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Tablas de validación.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BtablaVal.Attributes["class"] = "active";
                    //}
                    //if (Miro == "TablaUsuarios")
                    //{
                    //    sbDivIndirizzi.Append("Gestión de Usuarios <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Usuarios del Sistema.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BUser.Attributes["class"] = "active";
                    //}
                    //if (Miro == "TablaMenus")
                    //{
                    //    sbDivIndirizzi.Append("Gestión de Menús <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Menús para usuarios del Sistema.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BUser.Attributes["class"] = "active";
                    //}
                }
            }
            else if (this.Session["DESARROLLO"].ToString() == "DESARROLLO")
            {
                if (this.Session["MiNivel"].ToString() == "0")
                {
                    if (Miro == "")
                    {
                        sbDivIndirizzi.Append("(DESARROLLO) --> Firma digital a mano alzada para Documentación electronica (WACOM) <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Firma documentación autocomplementada única/múltiple con tan solo una rublica y desde memoria aquellos documentos que esten seleccionados por el conjunto de los allí firmantes. Puede completar el cierre del documento con Certificado digital FMT ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                        H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                        H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                        //ANominas.Attributes["class"] = "active";
                        //UNominas.Attributes["class"] = "nav nav-second-level active";
                        //LinkFirma.Attributes["class"] = "nav nav-second-level active";
                    }
                }
                else
                {
                    foreach (DataRow fila in Mitable.Rows)
                    {
                        if (a == fila["ZPAGINA"].ToString())
                        {
                            sbDivIndirizzi.Append("DESARROLLO --> " + fila["ZTITULO"].ToString() + ",  " + fila["ZDESCRIPCION"].ToString() + ", (" + this.Session["ComputerName"].ToString() + ") (" + this.Session["Mydb"].ToString() + ")");
                            H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                            H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                            break;
                        }
                    }


                    //if (Miro == "ConsultaFlujo")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Consulta Flujos <i class=\"fa fa-long-arrow-right\"></i> <small> \"  “Consultas Generales para Flujos de Trabajo ”\"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}

                    //if (Miro == "OnLine")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Articulos OnLine <i class=\"fa fa-long-arrow-right\"></i> <small> \"  “Gestión de Articulos por Categorias ”\"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}

                    //if (Miro == "Nomina")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Nóminas RecoDat <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“ Gestión de Empleados, Jornal, Destajo y Tareas ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "LotesAutoManual")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Generación de lotes automáticos <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Para generar lotes de forma automática e imprimir código QR  \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "LotesAutoManualS")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Generación de lotes automáticos Documental<i class=\"fa fa-long-arrow-right\" ></i> <small> \"Para generar lotes de forma automática e imprimir código QR desde una Tabla de Archivos Documentales  \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "OrdenCompra")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Órdenes de Compra <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Pedidos pendientes con orden de compra \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Inicio" || Miro == "")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Panel de Control <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Estadísticas \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AInicio.Attributes["class"] = "active";
                    //    //BtUdTables.Visible = true;
                    //}
                    //if (Miro == "AltaCampos")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Panel de Campos <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Campos creados para la Estructura de Información \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Consulta")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Búsqueda de Contenidos de Información <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Selección de listados sobre tablas \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AConsulta.Attributes["class"] = "active";
                    //    //UConsulta.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "ConsultaFlujo")
                    //{
                    //    sbDivIndirizzi.Append("Búsqueda de Contenidos de Información en Flujos de Trabajo <i class=\"fa fa-long-arrow-right\"></i> <small> \"Selección de listados sobre Flujos \"  </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Titulo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AConsulta.Attributes["class"] = "active";
                    //    //UConsulta.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "OrdenCarga")
                    //{
                    //    sbDivIndirizzi.Append("DESARROLLO --> Órdenes de Carga <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Selección de listados pendientes de Lotes \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AExpediciones.Attributes["class"] = "active";
                    //    //UExpediciones.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "RevisionLotes")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO)-- > Revisión de lotes <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Revisión lotes y preparación importación a GoldenSoft \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "RevisionLotesS")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO)-- > Revisión de lotes <i class=\"fa fa-long-arrow-right\" ></i> <small> \"Revisión lotes y preparación importación a GoldenSoft \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ACodeQR.Attributes["class"] = "active";
                    //    //UCodeQR.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "AltaArchivos")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Relación de Archivos documentales <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Crea, vincula o desvincula Tablas y columnas o campos relacionales” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Firma")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Firma digital a mano alzada para Documentación electronica (WACOM) <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Firma documentación autocomplementada única/múltiple con tan solo una rublica y desde memoria aquellos documentos que esten seleccionados por el conjunto de los allí firmantes. Puede completar el cierre del documento con Certificado digital FMT ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //ANominas.Attributes["class"] = "active";
                    //    //UNominas.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "Flujo")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Flujos de Trabajo y Estados de Flujo <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición y diseño de Flujos de Trabajo.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //BTablaArchivo.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level active";
                    //}
                    //if (Miro == "ImportTabla")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Migrar Tablas de un origen a un Destino <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de la estructura a migrar.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                       
                    //}
                    //if (Miro == "TablaValidacion")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Configura Tablas de Validación <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Tablas de validación.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BtablaVal.Attributes["class"] = "active";
                    //}
                    //if (Miro == "TablaUsuarios")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Gestión de Usuarios <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Usuarios del Sistema.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BUser.Attributes["class"] = "active";
                    //}

                    //if (Miro == "TablaMenus")
                    //{
                    //    sbDivIndirizzi.Append("(DESARROLLO) --> Gestión de Menús <i class=\"fa fa-long-arrow-right\" ></i> <small> \"“Definición de Menús para usuarios del Sistema.  ” \" </small> <small> (" + this.Session["ComputerName"].ToString() + ") </small>");
                    //    H3Desarrollo.InnerHtml = sbDivIndirizzi.ToString();
                    //    //AEstructura.Attributes["class"] = "active";
                    //    //UEstructura.Attributes["class"] = "nav nav-second-level";
                    //    //BUser.Attributes["class"] = "active";
                    //}
                }
            }
        }


        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;

        }

        private void TodoMenuDefault()
        {
            if (this.Session["MiNivel"].ToString() == "0")
            {
                //AInicio.Attributes["class"] = "";
                //AEstructura.Attributes["class"] = "";
                //UEstructura.Attributes["class"] = "nav nav-second-level collapse";
                //ACodeQR.Attributes["class"] = "";
                //UCodeQR.Attributes["class"] = "nav nav-second-level collapse";
                //AExpediciones.Attributes["class"] = "";
                //UExpediciones.Attributes["class"] = "nav nav-second-level collapse";
                //ANominas.Attributes["class"] = "";
                //UNominas.Attributes["class"] = "nav nav-second-level collapse";
                //AConsulta.Attributes["class"] = "";
                //UConsulta.Attributes["class"] = "nav nav-second-level collapse";

                //AInicio.Visible = false;
                //AEstructura.Visible = false;
                //AConsulta.Visible = false;
                //ACodeQR.Visible = false;
                //AExpediciones.Visible = false;
                //LinkNomina.Visible = false;
                //BtUdTables.Visible = false;
                //Nominas.Visible = true;
                //ANominas.Visible = true;
                ////LinkFirma.Visible = true;

            }
            else
            {
                //AInicio.Attributes["class"] = "";
                //AEstructura.Attributes["class"] = "";
                //UEstructura.Attributes["class"] = "nav nav-second-level collapse";
                //ACodeQR.Attributes["class"] = "";
                //UCodeQR.Attributes["class"] = "nav nav-second-level collapse";
                //AExpediciones.Attributes["class"] = "";
                //UExpediciones.Attributes["class"] = "nav nav-second-level collapse";
                //ANominas.Attributes["class"] = "";
                //UNominas.Attributes["class"] = "nav nav-second-level collapse";
                //AConsulta.Attributes["class"] = "";
                //UConsulta.Attributes["class"] = "nav nav-second-level collapse";
                //AUbicaFisi.Attributes["class"] = "";
                //UlFisica.Attributes["class"] = "nav nav-second-level collapse";
                //BtUdTables.Visible = false;
            }

            //BArchivo.Attributes["class"] = "";
            //BCampo.Attributes["class"] = "";
            //BBusca.Attributes["class"] = "";
            //BtablaVal.Attributes["class"] = "";
            //BTablaArchivo.Attributes["class"] = "";
            //CArchivo.Attributes["class"] = "";
            //CCampo.Attributes["class"] = "";
            //CBusca.Attributes["class"] = "";
            //CtablaVal.Attributes["class"] = "";
            //CTablaArchivo.Attributes["class"] = "";
        }

        protected void checkSi_Click(object sender, EventArgs e)
        {


            //Lbmensaje.Text = "Ya";
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;

            //UpdatePanel3.Update();
            //Update2.Update();

        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            //ContentPlaceHolder cont = new ContentPlaceHolder();
            //cont = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            //HtmlGenericControl li = (HtmlGenericControl)FindControl("DvPreparado");
            //li.Visible = false;
            //li = (HtmlGenericControl)FindControl("cuestion");
            //li.Visible = false;
            //li = (HtmlGenericControl)FindControl("Asume");
            //li.Visible = false;
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
        }


        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Nomina.aspx");
            this.Session["MiMenu"] = "Nomina";
            DivProgress.Visible = true;
            Server.Transfer("Nomina.aspx");
            DivProgress.Visible = false;
            //Update.Update();
        }

        protected void AAMigraTabla_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "ImportTabla";
            Server.Transfer("ImportTabla.aspx");
        }

        protected void AGeneral_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //AConsulta.Attributes["class"] = "active";
            //UConsulta.Attributes["class"] = "nav nav-second-level";
        }

        
        protected void AEstructura_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //AEstructura.Attributes["class"] = "active";
            //UEstructura.Attributes["class"] = "nav nav-second-level";
        }
        protected void AImportaciones_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //AImportaciones.Attributes["class"] = "active";
            //UImportaciones.Attributes["class"] = "nav nav-second-level";
        }
        protected void ACodeQR_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //ACodeQR.Attributes["class"] = "active";
            //UCodeQR.Attributes["class"] = "nav nav-second-level";
        }
        protected void AExpediciones_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //AExpediciones.Attributes["class"] = "active";
            //UExpediciones.Attributes["class"] = "nav nav-second-level";
        }
        protected void ANominas_clik(object sender, EventArgs e)
        {
            TodoMenuDefault();
            //ANominas.Attributes["class"] = "active";
            //UNominas.Attributes["class"] = "nav nav-second-level";
        }

        protected void ALoteAutoS_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "LotesAutoManualS";
            Server.Transfer("LotesAutoManualS.aspx");
        }

        protected void ALoteAuto_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "LotesAutoManual";
            Server.Transfer("LotesAutoManual.aspx");
        }

        protected void AOrdenCompra_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "OrdenCompra";
            Server.Transfer("OrdenCompra.aspx");
            //Server.Transfer("OrdenCompras.aspx"); sin unir menu
        }
        protected void UpdateTables_Click(object sender, EventArgs e)
        {
            //Lanza Procedimiento si el usuario tiene el codigo. 
            DBHelper.ExecuteProcedureUpdateTables("");
            DvPreparado.Visible = true;
            Lbmensaje.Text = "Se eliminaron y crearon correctamente las tablas de Desarrollo incorpotadas en el Procedimiento Delete_Tablas_Desarrollo_inserta_copia_de_produccion alojado.";
            cuestion.Visible = false;
            Asume.Visible = true;
        }

        protected void ImgFoto_Click(object sender, EventArgs e)
        {
            //DivFoto.Visible = false;
        }

        

        protected void APrincipal_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "Inicio";
            Server.Transfer("Inicio.aspx");
        }

        protected void AaltaCampo_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "AltaCampos";
            Server.Transfer("AltaCampos.aspx");
        }

        protected void AAltaArchivo_clik(object sender, EventArgs e)
        {
            this.Session["IDGridA"] = "";
            this.Session["MiMenu"] = "AltaArchivos";
            Server.Transfer("AltaArchivos.aspx");
        }
        protected void BtablaVal_clik(object sender, EventArgs e)
        {
            this.Session["IDGridA"] = "";
            this.Session["MiMenu"] = "TablaValidacion";
            Server.Transfer("TablaValidacion.aspx");
        }

        protected void ABuscaFlujo_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "ConsultaFlujo";
            Server.Transfer("FlujosDatos.aspx");
        }
        protected void ABuscaDatos_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "Consulta";
            Server.Transfer("Consulta.aspx");
        }
        protected void ABuscaUsuario_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "TablaUsuarios";
            Server.Transfer("Usuarios.aspx");
        }

        protected void AOrdenCargaLin_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "OrdenCarga";
            Server.Transfer("OrdenCarga.aspx");
        }
        protected void ALoteRevision_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "RevisionLotes";
            Server.Transfer("RevisionLotes.aspx");
        }
        protected void ALoteRevisionS_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "RevisionLotesS";
            Server.Transfer("RevisionLotesS.aspx");
        }

        protected void BtLinkFirma_Click(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "Firma";
            Server.Transfer("Firma.aspx");
        }
        protected void BTablaArchivo_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "Flujo";
            Server.Transfer("Flujos.aspx");
        }
        protected void BTablaMenus_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "TablaMenus";
            Server.Transfer("Menu.aspx");
        }
        protected void CArchivo_clik(object sender, EventArgs e)
        {

        }
        protected void CCampo_clik(object sender, EventArgs e)
        {

        }
        protected void CBusca_clik(object sender, EventArgs e)
        {

        }
        protected void CTablaval_clik(object sender, EventArgs e)
        {

        }
        protected void CContenedor_clik(object sender, EventArgs e)
        {

        }
        protected void AUbicaFisi_clik(object sender, EventArgs e)
        {

        }

        

        protected void AViveros_clik(object sender, EventArgs e)
        {
            this.Session["MiMenu"] = "default";
            Server.Transfer("default.aspx");

            //if(DivEstructura.Visible == true)
            //{
            //    DivEstructura.Visible = false;
            //}
            //else
            //{
            //    DivEstructura.Visible = true;
            //}
        }

        public void InputMenus(int Uno)
        {
            if(Uno == 0)
            {
                if (DivEstructura.Visible == true)
                {
                    DivEstructura.Visible = false;
                    pagevistaform.Attributes.CssStyle.Add("margin", "0");
                    pagevistaform.Attributes.CssStyle.Add("width", "100%");
                    MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
                }
                else
                {
                    DivEstructura.Visible = true;
                    pagevistaform.Attributes.CssStyle.Add("margin", "0 0 0 250px");
                    pagevistaform.Attributes.CssStyle.Add("width", "90%");
                    MasMinMenu.Attributes["class"] = "fa fa-chevron-left fa-2x";
                }
            }
            else
            {
                DivEstructura.Visible = false;
                pagevistaform.Attributes.CssStyle.Add("margin", "0");
                pagevistaform.Attributes.CssStyle.Add("width", "100%");
                MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
            }
        }
        protected void BtMenus_Click(object sender, EventArgs e)
        {
            if (DivEstructura.Visible == true)
            {
                DivEstructura.Visible = false;
                pagevistaform.Attributes.CssStyle.Add("margin", "0");
                pagevistaform.Attributes.CssStyle.Add("width", "100%");
                MasMinMenu.Attributes["class"] = "fa fa-chevron-right fa-2x";
            }
            else
            {
                DivEstructura.Visible = true;
                pagevistaform.Attributes.CssStyle.Add("margin", "0 0 0 250px");
                pagevistaform.Attributes.CssStyle.Add("width", "90%");
                MasMinMenu.Attributes["class"] = "fa fa-chevron-left fa-2x";
            }
        }
    }
}