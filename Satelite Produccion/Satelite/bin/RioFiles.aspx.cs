using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace Satelite
{
    public partial class RioFiles : System.Web.UI.Page
    {
        public Boolean Esta = false;
        private int Indice = 0;


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
                    //Response.Redirect("Login.aspx"); //Default
                    Server.Transfer("Login.aspx");
                }

                if (Session["Session"].ToString() == "" || Session["Session"].ToString() == "0")
                {
                    this.Session["Error"] = "0";
                    //Response.Redirect("Login.aspx"); //Default
                    Server.Transfer("Login.aspx");
                }

                if (!IsPostBack)
                {
                    this.Session["IDSecuencia"] = "0";
                    //this.Session["DESARROLLO"] = "0";
                    //his.Session["SelectPrinter"] = "0";

                    this.Session["IDLote"] = "0";
                    Campos_ordenados();
                    Carga_tablaLista();

                    if(this.Session["Upload"].ToString() != "")
                    {
                        if (Directory.Exists(Server.MapPath(this.Session["Upload"].ToString())) == false)
                        {
                            DirectoryInfo di = Directory.CreateDirectory(Server.MapPath(this.Session["Upload"].ToString()));
                        }
                    }

                }
                else
                {
                    try
                    {
                        this.Session["Error"] = "0";
                        if (this.Session["IDSecuencia"].ToString() == null)
                        {
                            //Response.Redirect("thEnd.aspx");
                            Server.Transfer("thEnd.aspx");
                        }
                        this.Session["Secuencias"] = Main.CargaSecuencia().Tables[0];
                    }
                    catch (NullReferenceException ex)
                    {
                        string b = ex.Message;
                        //Lberror.Text += ex.Message;
                        if (Session["Session"] == null)
                        {
                            //Response.Redirect("Login.aspx");
                            Server.Transfer("Login.aspx");
                        }
                        else if (this.Session["Error"].ToString() == "0")
                        {
                            //Response.Redirect("Login.aspx");
                            Server.Transfer("Login.aspx");
                        }
                        else
                        {
                            //Response.Redirect("thEnd.aspx");
                            Server.Transfer("thEnd.aspx");
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
                string b = ex.Message;
                if (Session["Session"] == null)
                {
                    //Response.Redirect("Login.aspx");
                    Server.Transfer("Login.aspx");
                }
                else if (this.Session["Error"].ToString() == "0")
                {
                    //Response.Redirect("Login.aspx");
                    Server.Transfer("Login.aspx");
                }
                else
                {
                    //Response.Redirect("thEnd.aspx");
                    Server.Transfer("thEnd.aspx");
                }
            }
            //Habilita_contoles();
            dvPrinters.Visible = true;
            dvDrlist.Visible = false;
        }

        protected void HtmlAnchor_Click(Object sender, EventArgs e)
        {
            LinkButton micontrol = (LinkButton)sender;
            string Miro = micontrol.ID.ToString();
            if (Miro == "aMenu0") { this.Session["Menu"] = "0"; }
            if (Miro == "aMenu1") { this.Session["Menu"] = "1"; }
            if (Miro == "aMenu2") { this.Session["Menu"] = "2"; }
            Carga_Menus();
        }


        public void Carga_Menus()
        {

            if (this.Session["Menu"].ToString() == "0")
            {
                //el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";

                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";

                accordion0.Visible = true;
                accordion.Visible = false;
                accordion2.Visible = false;
            }

            if (this.Session["Menu"].ToString() == "1")
            {
                //el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "";

                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade";
                accordion.Visible = true;
                accordion0.Visible = false;
                accordion2.Visible = false;
            }
            if (this.Session["Menu"].ToString() == "2")
            {
                //el 1
                HtmlGenericControl li = (HtmlGenericControl)FindControl("Menu2");
                li.Attributes["class"] = "active";
                li = (HtmlGenericControl)FindControl("Menu0");
                li.Attributes["class"] = "";
                li = (HtmlGenericControl)FindControl("Menu1");
                li.Attributes["class"] = "";

                HtmlGenericControl panel = (HtmlGenericControl)FindControl("accordion2");
                panel.Attributes["class"] = "tab-pane fade active in";
                panel = (HtmlGenericControl)FindControl("accordion0");
                panel.Attributes["class"] = "tab-pane fade";
                panel = (HtmlGenericControl)FindControl("accordion");
                panel.Attributes["class"] = "tab-pane fade";
                accordion2.Visible = true;
                accordion0.Visible = false;
                accordion.Visible = false;
            }

        }

        protected void BtLinkNomina_Click(object sender, EventArgs e)
        {
            //Response.Redirect("RecoNomina.aspx");
            Server.Transfer("Login.aspx");
        }

        //private void Nueva_Secuencia()
        //{
        //    DataTable dt3 = Main.CargaSecuencia().Tables[0];
        //    this.Session["Secuencias"] = dt3;
        //    DrVariedad.Items.Clear();

        //    DrVariedad.AppendDataBoundItems = true;
        //    DrVariedad.Items.Add("Seleccione un tipo de lote...");
        //    DrVariedad.DataValueField = "ZID";
        //    DrVariedad.DataTextField = "ZDESCRIPCION";


        //    DrVariedad.DataSource = dt3;
        //    DrVariedad.DataBind();

        //    foreach (DataRow fila in dt3.Rows)
        //    {
        //        if (fila["ZID"].ToString() == DrVariedad.SelectedItem.Value)
        //        {
        //            this.Session["IDSecuencia"] = fila["ZID"].ToString();
        //            this.Session["LaMascara"] = fila["ZMASCARA"].ToString();
        //            //GeneraSecuencia(fila["ZMASCARA"].ToString(), fila["ZSECUENCIA"].ToString());
        //            break;
        //        }
        //    }
        //    //DrVariedad.Text = "";

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

        //private void GeneraSecuencia(string Dato, string Secuencia)
        //{
        //    //La secuencia es =>   LITERAL#aaS;FECHA#mm;FECHA#dd;SECUENCIA#000
        //    string Cadena = "";
        //    string Miro = "";
        //    try
        //    {
        //        string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(Dato, ";");
        //        foreach (string Linea in CadaLinea)
        //        {
        //            if (Linea != "")
        //            {
        //                if (Linea.Contains("LITERAL#"))
        //                {
        //                    Cadena += Linea.Replace("LITERAL#", "");
        //                }
        //                if (Linea.Contains("FECHA#"))
        //                {
        //                    Miro = Linea.Replace("FECHA#", "");
        //                    Miro = DateTime.Now.ToString(Miro);
        //                    Cadena += Miro;
        //                }
        //                if (Linea.Contains("SECUENCIA#"))
        //                {
        //                    Miro = Linea.Replace("SECUENCIA#", "");
        //                    if (Miro != null)
        //                    {
        //                        Miro = Miro.Substring(0, Miro.Length - Secuencia.Length) + Secuencia.ToString();
        //                        this.Session["NumeroSecuencia"] = Secuencia.ToString();
        //                    }
        //                    Cadena += Miro;
        //                }
        //            }
        //        }
        //        if (Cadena != "")
        //        {
        //            txtQRCode.Text = Cadena;
        //            this.Session["LOTEenCURSO"] = Cadena;

        //            if (DrPrinters.SelectedItem.Value == "6")
        //            {
        //                LbSecuenciaLote.Text = Cadena;
        //            }
        //            else
        //            {
        //                LbSecuenciaLote.Text = Cadena;
        //            }

        //            btnGenerate_Click(null, null);
        //            LbCodigoLote.Text = "CÓDIGO LOTE:";
        //            LbCodeQRPalteAlv.Text = Cadena;  //"CÓDIGO LOTE:";

        //        }

        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        Lberror.Text += ex.Message;
        //        //alertaErr.Visible = true;
        //        //TextAlertaErr.Text = ex.Message;
        //    }

        //}

        protected void checkOk_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
        }
        protected void checkSi_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
            //if (this.Session["ElIDaBorrar"].ToString() == "") { return; }
            ////ElIDaBorrar = miro + ";" + UNIDADES + ";" + Numero;
            //string[] Trata = System.Text.RegularExpressions.Regex.Split(this.Session["ElIDaBorrar"].ToString(), ";");

            //string SQL = "UPDATE ZCARGA_ORDEN SET UDSENCARGA = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSENCARGA) - " + Trata[1].ToString().Replace(",", ".") + ")), ";
            //SQL += " UDSPENDIENTES = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")), ";
            //SQL += " UDSACARGAR = CONVERT(VARCHAR, (CONVERT(DECIMAL(18, 6), UDSPENDIENTES) + " + Trata[1].ToString().Replace(",", ".") + ")),  ";
            //SQL += " NUMPALET = 0.00 ";
            //SQL += " WHERE ID = " + Trata[0];
            //DBHelper.ExecuteNonQuery(SQL);

            //SQL = "DELETE FROM ZCARGA_LINEA WHERE ID = " + Trata[0] + " AND NUMERO_LINEA = " + Trata[2];

            //DBHelper.ExecuteNonQuery(SQL);

            //Carga_tabla();
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();

            //this.Session["ElIDaBorrar"] = "";

        }
        protected void checkNo_Click(object sender, EventArgs e)
        {
            DvPreparado.Visible = false;
            cuestion.Visible = false;
            Asume.Visible = false;
            Page.MaintainScrollPositionOnPostBack = true;
        }

        protected void Files_SaveBtn_Click(object sender, EventArgs e)
        {
            string sourcePath = Server.MapPath("~/uploads/");
            try
            {

                if (System.IO.Directory.Exists(sourcePath))
                {
                    string[] files = System.IO.Directory.GetFiles(sourcePath);

                    // Copy the files and overwrite destination files if they already exist. Docs\folders
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path. oFileInfo.Length.ToString().Replace(",", ".")
                        string fileName = sourcePath + System.IO.Path.GetFileName(s);
                        string DsfileName = Seguridad.cifrar(System.IO.Path.GetFileName(s), false, 0);
                        string NameFile = "";
                        string SQL = " SELECT MAX(ZID) FROM ZPRODFILES ";
                        Object Con = DBHelper.ExecuteScalarSQL(SQL, null);
                        if (Con.ToString() == "")
                        {
                            Con = "1";
                            NameFile = Con.ToString().PadLeft(10, '0');
                        }
                        else
                        {
                            NameFile = Con.ToString().PadLeft(10, '0');
                        }

                        FileInfo oFileInfo = new FileInfo(fileName);
                        string destFile = System.IO.Path.Combine(Server.MapPath("~/Docs/folders/"), DsfileName) + oFileInfo.Extension;
                        string Peso = oFileInfo.Length.ToString();
                        if (Peso.Length >= 12)
                        {
                            Peso = (Convert.ToDecimal(string.Format("{0:0.00}", ((oFileInfo.Length / 1024f) / 1024f) / 1024f))).ToString() + " GB";
                        }
                        else if (Peso.Length >= 7 && Peso.Length <= 12)
                        {
                            Peso = (Convert.ToDecimal(string.Format("{0:0.00}", ( oFileInfo.Length / 1024f) / 1024f))).ToString() + " MB";
                        }
                        else
                        {
                            Peso = ((Convert.ToDecimal(string.Format("{0:0.00}", oFileInfo.Length / 1024f)))).ToString() + " KB";
                        }


                        SQL = "INSERT INTO ZPRODFILES (ID_DOMAIN, ID_CABECERA, NOMBRE, ID_NAME, PESO, RUTA, ROOT, ZKEY, FECHA, ESTADO, CATEGORIA, SUBCATEGORIA, ZUSER )  ";
                        SQL += "VALUES (0, 0, '" + System.IO.Path.GetFileName(s) + "', '" + DsfileName + "','" +  Peso + "' , '" + NameFile + "', '0', '0', '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + "', 0, '" + DrCategoria.SelectedItem.Value + "', '" + DrSubCategoria.SelectedItem.Value + "','" + this.Session["UserAlias"].ToString() + "' )  ";
                        DBHelper.ExecuteNonQuery(SQL);

                        if (Directory.Exists(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString())) == false)
                        {
                            DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString()));
                        }

                        System.IO.File.Move(fileName, System.IO.Path.Combine(Server.MapPath("~/Docs/folders/" + this.Session["UserAlias"].ToString() + "/"), NameFile)); // destFile);


                        //System.IO.File.Copy(s, destFile, true);
                        //Console.WriteLine("My File's Name: \"" + oFileInfo.Name + "\"");
                        //DateTime dtCreationTime = oFileInfo.CreationTime;
                        //Console.WriteLine("Date and Time File Created: " + dtCreationTime.ToString());
                        //Console.WriteLine("myFile Extension: " + oFileInfo.Extension);
                        //Console.WriteLine("myFile total Size: " + oFileInfo.Length.ToString());
                        //Console.WriteLine("myFile filepath: " + oFileInfo.DirectoryName);
                        //Console.WriteLine("My File's Full Name: \"" + oFileInfo.FullName + "\"");
                    }
                }
                else
                {
                    Console.WriteLine("No existe la ruta indicada.");
                }

            }
            catch(Exception ex)
            {
                Lbmensaje.Text = "Error - No se puede guardar el fichero. Intenteló más tarde." + ex.Message;
                DvPreparado.Visible = true;
                cuestion.Visible = false;
                Asume.Visible = true;
                Page.MaintainScrollPositionOnPostBack = true;
                //return;
            }
            Carga_tablaLista();
        }


//protected void FileUpload_SaveBtn_Click(object sender, EventArgs e)
//{
//    //check if user has selected a file
//    if (FileUpload_Control.HasFile)
//    {
//        try
//        {
//            ///*save file to location.
//            //Make sure the directory path is correct.*/
//            ////FileUpload_Control.SaveAs("C:\\FileUploadExample\\" + FileUpload_Control.FileName);
//            //FileUpload_Control.SaveAs(Server.MapPath("~/erDvgh453DVLO9jk/" + FileUpload_Control.FileName));
//            ////Server.MapPath("~/Reports/rpalvcamionlista.rdlc");
//            //FileUpload_Msg.Text = "fichero subido correctamente.";
//            string SQL = "";
//            int filecount = 0;
//            int fileuploadcount = 0;
//            //check No of Files Selected  
//            filecount = FileUpload_Control.PostedFiles.Count();
//            if (filecount <= 10)
//            {
//                Lbmensaje.Text = "";
//                foreach (HttpPostedFile postfiles in FileUpload_Control.PostedFiles)
//                {
//                    //Get The File Extension  
//                    string filetype = Path.GetExtension(postfiles.FileName);
//                    if (filetype.ToLower() == ".docx" ||
//                        filetype.ToLower() == ".pdf" ||
//                        filetype.ToLower() == ".txt" ||
//                        filetype.ToLower() == ".doc" ||
//                        filetype.ToLower() == ".avi" ||
//                        filetype.ToLower() == ".mpg" ||
//                        filetype.ToLower() == ".mp4" ||
//                        filetype.ToLower() == ".xls" ||
//                        filetype.ToLower() == ".xlsx" ||
//                        filetype.ToLower() == ".doc" ||
//                        filetype.ToLower() == ".jpg" ||
//                        filetype.ToLower() == ".bmp" ||
//                        filetype.ToLower() == ".gif" ||
//                        filetype.ToLower() == ".png")
//                    {
//                        //Get The File Size In Bite  
//                        double filesize = postfiles.ContentLength;

//                        double A = (filesize / (1024 * 1034));

//                        if (filesize < (10485760))
//                        {
//                            fileuploadcount++;
//                            string serverfolder = string.Empty;
//                            string serverpath = string.Empty;

//                            serverfolder = Server.MapPath(@"erDvgh453DVLO9jk\");
//                            //check Folder avlalible or not  
//                            if (!Directory.Exists(serverfolder))
//                            {
//                                // create Folder  
//                                Directory.CreateDirectory(serverfolder);
//                            }
//                            serverpath = serverfolder + Path.GetFileName(postfiles.FileName);
//                            FileUpload_Control.SaveAs(serverpath);

//                            string Directorio = Seguridad.cifrar(postfiles.FileName, false, 0);

//                            SQL = "INSERT INTO ZPRODFILES (ID_DOMAIN, ID_CABECERA, NOMBRE, ID_NAME, PESO, RUTA, ROOT, ZKEY, FECHA, ESTADO )  ";
//                            SQL += "VALUES (0, 0, '" + postfiles.FileName + "', '" + postfiles.FileName + "', " + A.ToString().Replace(",", ".") + ", '" + Directorio + "', '0', '0', '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 0 )  ";
//                            DBHelper.ExecuteNonQuery(SQL);
//                            //System.Threading.Thread.Sleep(8000);


//                        }
//                        else
//                        {
//                            Lbmensaje.Text += "El fichero (" + postfiles.FileName + " tiene un peso de " + A.ToString().Replace(",", ".") + " MB < br/>";
//                            Lbmensaje.Text += "Por favor seleccione un maximo de 10 ficheros";
//                            DvPreparado.Visible = true;
//                            cuestion.Visible = false;
//                            Asume.Visible = true;
//                            Page.MaintainScrollPositionOnPostBack = true;
//                        }
//                    }
//                    else
//                    {
//                        Lbmensaje.Text += "Sólo se permiten subir ficheros con extensión doc, docx, pdf, txt, xls, xlsx, avi, mp4, mpg, mpge, jpg, png, gif, bmp < br/>";
//                        DvPreparado.Visible = true;
//                        cuestion.Visible = false;
//                        Asume.Visible = true;
//                        Page.MaintainScrollPositionOnPostBack = true;
//                        //break;
//                    }
//                }
//            }
//            else
//            {
//                Lbmensaje.Text = "Ha seleccionado (" + filecount + ") ficheros <br/>";
//                Lbmensaje.Text += "Por favor seleccione un maximo de 10 ficheros";
//                DvPreparado.Visible = true;
//                cuestion.Visible = false;
//                Asume.Visible = true;
//                Page.MaintainScrollPositionOnPostBack = true;
//                //return;
//            }

//        }
//        catch
//        {
//            Lbmensaje.Text = "Error - No se puede guardar el fichero. Intenteló más tarde.";
//            DvPreparado.Visible = true;
//            cuestion.Visible = false;
//            Asume.Visible = true;
//            Page.MaintainScrollPositionOnPostBack = true;
//            //return;
//        }
//    }
//    else
//    {
//        FileUpload_Msg.Text = "Error - No hay fichero seleccionado.";
//    }
//    Carga_tablaLista();
//}

protected void gvLista_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = e.Row.DataItem as DataRowView;
                string miro = drv["ESTADO"].ToString();

                if (drv["ESTADO"].ToString() == "1")
                {
                    e.Row.BackColor = Color.FromName("#eaf5dc");
                    //verde
                }
                else if (drv["ESTADO"].ToString() == "2")
                {
                    //crema
                    e.Row.BackColor = Color.FromName("#fcf5d2");
                }
                else if ((e.Row.DataItemIndex % 2) == 0)
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
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    //e.Row.TableSection = TableRowSection.TableFooter;
            //    e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    //e.Row.TableSection = TableRowSection.TableHeader;
            //    e.Row.Cells[4].Width = 1040;
            //    //e.Row.BackColor = Color.FromName("#f5f5f5");
            //}
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.TableSection = TableRowSection.TableFooter;
            //}
        }

        protected void gvLista_OnSorting(object sender, GridViewSortEventArgs e)
        {
            Carga_tablaLista(e.SortExpression);
        }

        protected void gvLista_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            gvLista.PageIndex = e.NewPageIndex;
            Carga_tablaLista();
        }

        protected void gvLista_PageSize_Changed(object sender, EventArgs e)
        {
            gvLista.PageSize = Convert.ToInt32(ddListaPageSize.SelectedItem.Value);
            Carga_tablaLista();
        }

        protected void gvLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

            Carga_tablaLista();
            gvLista.EditIndex = -1;
            gvLista.DataBind();

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

        protected void gvLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int index = 0;
            //if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            try
            {
                if (e.CommandName == "SubeCarga")
                {
                    //string strRuta = Server.MapPath("~/Docs/folders/"); ;
                    string strFile = "";
                    string DestFile = "";
                    string FileUser = "";

                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvLista.Rows[index];
                    string miro = gvLista.DataKeys[index].Value.ToString();
                    //sube la linea a la orden
                    //string Numero = "";

                    Label txtBox = (gvLista.Rows[Indice].Cells[8].FindControl("LabLNumLinea") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        strFile = txtBox.Text;
                    }
                    txtBox = (gvLista.Rows[Indice].Cells[8].FindControl("LabLLEmpresa") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        DestFile = txtBox.Text;
                    }
                    txtBox = (gvLista.Rows[Indice].Cells[8].FindControl("LabLuser") as Label);
                    //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                    if (txtBox != null)
                    {
                        FileUser = txtBox.Text;
                    }


                    //string FileName = "Durgesh.jpg"; // It's a file name displayed on downloaded file on client side.

                    System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = ""; // "image/jpeg";
                    response.AddHeader("Content-Disposition", "attachment; filename=" + strFile + ";");
                    response.TransmitFile(Server.MapPath("~/Docs/folders/" + FileUser + "/" + DestFile));
                    //response.TransmitFile(Server.MapPath("~/File/001.jpg"));
                    response.Flush();
                    //response.End();


                    //FileInfo ObjArchivo = new System.IO.FileInfo(strRuta + strFile);
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                    //Response.AddHeader("Content-Length", ObjArchivo.Length.ToString());
                    //Response.ContentType = "application/pdf";
                    //Response.WriteFile(ObjArchivo.FullName);
                    //Response.End();
                }

                if (e.CommandName == "CargaCamion")
                {
                    index = Convert.ToInt32(e.CommandArgument);
                    Indice = index;
                    GridViewRow row = gvLista.Rows[index];
                    string miro = gvLista.DataKeys[index].Value.ToString();

                    string Numero = "";

                    //string Mira = Server.HtmlDecode(row.Cells[4].Text);
                    //if (Mira != "")
                    //{
                    //    Numero = Mira;
                    //}
                    Label txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("LabLNumLinea") as Label);
                    if (txtBox != null)
                    {
                        Numero = txtBox.Text;
                    }

                    //string SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, RUTA, NUMERO, LINEA, ARTICULO, ";
                    //SQL += "UDSENCARGA, NUMPALET, POSICIONCAMION, OBSERVACIONES, FECHAENTREGA, COMPUTER, ESTADO, NUMERO_LINEA, ID_CABECERA FROM  ZCARGA_LINEA ";
                    //SQL += " WHERE ID_CABECERA = " + TxtNumero.Text;
                    //SQL += " AND NUMERO_LINEA = " + Numero;
                    //Lberror.Text += SQL + "1- gvlista_rowcomand " + Variables.mensajeserver;
                    //DataTable dt = Main.BuscaLote(SQL).Tables[0];
                    //Lberror.Text += " 2- gvlista_rowcomand  " + Variables.mensajeserver;


                    //SQL = " UPDATE ZCARGA_LINEA SET ESTADO = 2 WHERE ID_CABECERA = " + TxtNumero.Text + " AND NUMERO_LINEA = " + Numero;
                    //DBHelper.ExecuteNonQuery(SQL);
                    //SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
                    //SQL += " WHERE NUMERO_LINEA = " + Numero; //Miro con ID lo hace con todos

                    //DBHelper.ExecuteNonQuery(SQL);
                    //Carga_tablaLista();

                    //gvLista.EditIndex = -1;

                    //gvLista.DataBind();
                }

                if (e.CommandName == "Edit" || e.CommandName == "Update")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    Indice = index;
                    this.Session["IDGridB"] = gvLista.DataKeys[index].Value.ToString();
                    //gvControl.EditIndex = -1;
                    //gvControl.DataBind();
                }





            }
            catch (Exception ex)
            {
                string b = ex.Message;
                //Lberror.Text = "Lista RowCommand = index: " + index + "; Grid: " + this.Session["IDGridA"].ToString() + "Key: " + gvLista.DataKeys[index].Value.ToString() + " " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            GridViewRow row = (GridViewRow)gvLista.Rows[e.RowIndex];

            this.Session["ElIDaBorrar"] = gvLista.DataKeys[e.RowIndex].Value.ToString();

            cuestion.Visible = true;
            Asume.Visible = false;
            DvPreparado.Visible = true;
            //BtnAcepta.Visible = false;
            //BTnNoAcepta.Visible = false;

            //string SQL = "UPDATE ZCARGA_LINEA set ESTADO = 2 ";
            //SQL += " WHERE ID = " + miro;

            //DBHelper.ExecuteNonQuery(SQL);
            //Carga_tablaLista();

            //gvLista.EditIndex = -1;

            //gvLista.DataBind();
        }

        protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            gvLista.EditIndex = Convert.ToInt16(e.NewEditIndex);

            //gvLista.AutoResizeColumns();
            //gvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //gvLista.AutoResizeColumns(DataGridViewAutoSizeColumnsMo‌​de.Fill);
            int indice = gvLista.EditIndex = e.NewEditIndex;
            //int i = gvLista.Rows[indice].Cells.Count;

            //for (int i = 0; i < gvLista.Columns.Count; i++)
            //{
            //    gvLista.Columns[i].ItemStyle.Width = 10;
            //}


            Carga_tablaLista();
            //gvControl.Rows[indice].Cells[0].Enabled = false;

            //gvLista.Rows[indice].Cells[1].Enabled = false;
            //gvLista.Rows[indice].Cells[2].Enabled = false;
            //gvLista.Rows[indice].Cells[3].Enabled = false;
            //gvLista.Rows[indice].Cells[4].Enabled = false;
            //gvLista.Rows[indice].Cells[5].Enabled = false;
            //gvLista.Rows[indice].Cells[6].Enabled = false;
            //gvLista.Rows[indice].Cells[7].Enabled = false;
            //gvLista.Rows[indice].Cells[8].Enabled = false;
            //gvLista.Rows[indice].Cells[9].Enabled = false;

            //gvLista.Rows[indice].Cells[10].Enabled = false;
            //gvLista.Rows[indice].Cells[11].Enabled = false;

            GridViewRow row = gvLista.Rows[indice];
            row.BackColor = Color.FromName("#ffead1");

            //gvLista.Rows[indice].Cells[16].Enabled = false;
            //Carga_tablaLista();
            //DataTable dt = this.Session["MiConsulta"] as DataTable;
            //gvControl.DataSource = dt;
            //gvControl.DataBind();
        }

        protected void gvLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (this.Session["EstadoCabecera"].ToString() == "3") { return; }
            GridViewRow row = gvLista.Rows[e.RowIndex];
            string miro = gvLista.DataKeys[e.RowIndex].Value.ToString();

            string rNumPalet = "";
            string rCantidad = "";
            string MiPOSICION = "";
            string rPOSICION = "";
            string rOBSERVACION = "";
            string rNUMEROLINEA = "";
            string rCABECERA = "";
            //string Mira = "";
            try
            {

                TextBox txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLCarga") as TextBox);
                //TextBox txtBox = (TextBox)(row.Cells[12].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rCantidad = txtBox.Text;
                    }
                    else
                    {
                        rCantidad = "0";
                    }
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLPalet") as TextBox);
                //txtBox = (TextBox)(row.Cells[13].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rNumPalet = txtBox.Text;
                    }
                    else
                    {
                        rNumPalet = "0";
                    }
                }

                Label LabelBox = (gvLista.Rows[Indice].Cells[14].FindControl("LabLcamion") as Label);
                //txtBox = (TextBox)(row.Cells[14].Controls[0]);
                if (LabelBox != null)
                {
                    if (LabelBox.Text != "")
                    {
                        MiPOSICION = LabelBox.Text;
                    }
                    else
                    {
                        MiPOSICION = "0";
                    }
                }

                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLcamion") as TextBox);
                //txtBox = (TextBox)(row.Cells[14].Controls[0]);
                if (txtBox != null)
                {
                    if (txtBox.Text != "")
                    {
                        rPOSICION = txtBox.Text;
                    }
                    else
                    {
                        rPOSICION = "0";
                    }
                    this.Session["ModificaLinea"] = rPOSICION;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLObservaciones") as TextBox);
                //txtBox = (TextBox)(row.Cells[15].Controls[0]);
                if (txtBox != null)
                {
                    rOBSERVACION = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLNumLinea") as TextBox);
                //txtBox = (TextBox)(row.Cells[4].Controls[0]);
                if (txtBox != null)
                {
                    rNUMEROLINEA = txtBox.Text;
                }
                txtBox = (gvLista.Rows[Indice].Cells[10].FindControl("TabLCabecera") as TextBox);
                //txtBox = (TextBox)(row.Cells[2].Controls[0]);
                if (txtBox != null)
                {
                    rCABECERA = txtBox.Text;
                }


                string SQL = "SELECT POSICIONCAMION ";
                SQL += " FROM ZCARGA_LINEA ";
                SQL += " WHERE ID = " + miro;
                //SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                Object Con = DBHelper.ExecuteScalarSQL(SQL, null);

                if (Con is null)
                {
                    MiPOSICION = "0";
                    //this.Session["ElIDaBorrar"] = "0" + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;
                }
                else
                {
                    // MiPosicion, Lanuevaposicion, ID, ID_CABECERA, NUMERO_LINEA
                    MiPOSICION = Con.ToString();
                    //this.Session["ElIDaBorrar"] = Con + ";" + rPOSICION + ";" + miro + ";" + TxtNumero.Text + ";" + rNUMEROLINEA;
                }


                SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = " + rPOSICION + ", ";
                //string SQL = "UPDATE ZCARGA_LINEA set POSICIONCAMION = " + rPOSICION.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + ", ";
                SQL += " OBSERVACIONES = '" + rOBSERVACION + "', ";
                SQL += " NUMPALET = " + rNumPalet.Replace(",", ".") + ", ";
                SQL += " UDSENCARGA = " + rCantidad.Replace(",", ".") + ", ";
                //SQL += " ZSYSDATE = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', ";
                //SQL += " HASTA =  CONVERT(VARCHAR (255), ID_CABECERA) + '|' +  CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + rPOSICION.Replace("\r\n", "").Replace("\n", "").Replace("\r", "") + "', ";
                SQL += " HASTA =  CONVERT(VARCHAR (255), ID_CABECERA) + '|' +  CONVERT(VARCHAR (255), CLIENTEPROVEEDOR) + '|' +  CONVERT(VARCHAR (255), NUMERO) + '|' +  CONVERT(VARCHAR (255), LINEA) + '|" + rPOSICION + "', ";
                SQL += " ESTADO = 1 ";
                SQL += " WHERE ID = " + miro;
                //SQL += " AND ID_CABECERA = " + TxtNumero.Text;
                SQL += " AND NUMERO_LINEA = " + rNUMEROLINEA;

                Lberror.Text += SQL + "1- gvLista_RowUpdating " + Variables.mensajeserver;
                DBHelper.ExecuteNonQuery(SQL);
                Lberror.Text += " 1- gvLista_RowUpdating " + Variables.mensajeserver;

                if (MiPOSICION != rPOSICION)
                {
                    if (Convert.ToInt32(MiPOSICION) > Convert.ToInt32(rPOSICION))
                    {
                        Lbmensaje.Text = "La posición del Camión ha cambiado a una posición menor.";
                    }
                    else
                    {
                        Lbmensaje.Text = "La posición del Camión ha cambiado a una posición mayor.";
                    }
                    Lbmensaje.Text += " ¿Desea desplazar los palets de posiciones intermedias?";
                    this.Session["SelectLinea"] = SQL;
                    Asume.Visible = false;
                    cuestion.Visible = false;
                    DvPreparado.Visible = true;
                    gvLista.EditIndex = -1;
                    return;
                }
                else
                {
                    Variables.Error = "";

                    Carga_tablaLista();

                    gvLista.EditIndex = -1;

                    //DataTable dt = this.Session["MiConsulta"] as DataTable;
                    //gvControl.DataSource = dt;
                    gvLista.DataBind();
                }
            }
            catch (Exception ex)
            {
                Lberror.Text += ". " + ex.Message;
                Lberror.Visible = true;
            }
        }

        protected void gvLista_SelectedIndexChanged(Object sender, GridViewSelectEventArgs e)
        {
            gvLista.SelectedRow.BackColor = Color.FromName("#565656");
        }

        private void Carga_tablaLista(string sortExpression = null)
        {
            string SQL = "";
            DataTable dt = null;

            //Carga_tablaListaFiltro();
            string filtro = ""; // this.Session["Filtro"].ToString();
            //OrdenLista();
            try
            {
                //this.Session["NumeroPalet"] = "0";
                filtro = filtro.Replace("WHERE", "AND");
                if (ConfigurationManager.AppSettings.Get("Desarrollo").ToString() == "1")
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT ZID, ID_DOMAIN, ID_CABECERA, NOMBRE, ID_NAME, RUTA, ROOT, PESO, ZKEY, FECHA, ESTADO, CATEGORIA, SUBCATEGORIA, ZUSER FROM ZPRODFILES  ";

                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    else
                    {
                        SQL += " ORDER BY ZID ";
                    }


                    Lberror.Text += " 1- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + "1- Carga_tablaLista " + SQL + Environment.NewLine;


                }
                else
                {
                    //SQL = " SELECT * FROM [DESARROLLO].[dbo].ZCARGA_LINEA  WHERE COMPUTER = '" + this.Session["ComputerName"].ToString() + "' " + filtro + " ORDER BY POSICIONCAMION ";
                    SQL = " SELECT ZID, ID_DOMAIN, ID_CABECERA, NOMBRE, ID_NAME, RUTA, ROOT, PESO, ZKEY, FECHA, ESTADO, CATEGORIA, SUBCATEGORIA, ZUSER FROM ZPRODFILES  ";

                    if (filtro != "")
                    {
                        SQL += filtro;
                    }
                    else
                    {
                        SQL += " ORDER BY ZID ";
                    }

                    Lberror.Text += " 2- Carga_tablaLista " + SQL + Environment.NewLine;
                    dt = Main.BuscaLote(SQL).Tables[0];
                    Lberror.Text += SQL + " 2- Carga_tablaLista " + SQL + Environment.NewLine;


                    //SQL = " SELECT * FROM [RIOERESMA].[dbo].ZCARGA_LINEA  ORDER BY POSICIONCAMION ";
                    //dt = Main.BuscaLote(SQL).Tables[0];
                }


                this.Session["MiConsulta"] = dt;
                this.Session["TablaLista"] = dt;

                if (sortExpression != null)
                {
                    DataView dv = dt.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvLista.DataSource = dv;
                }
                else
                {
                    gvLista.DataSource = dt;
                }
                gvLista.DataBind();

                //gvLista.DataSource = dt;
                //gvLista.DataBind();
                //break;
                //< div class="contenedor" draggable="true" ondragstart="drag(event)" id="drag0" data-tooltip="QR 21P322   Variedad FORTUNA    Plantas:15.458  Almacen: C1 Campo: Avestruces">
                //    <img class="pokemon" src="images/palet200X300.png" />
                //    <div id = "dragText0" class="centrado">QR 21P322</div>
                //</div>  dt.Rows.Count;
                LbRowLista.Text = "Total ficheros: " + dt.Rows.Count;

                gvLista.EditIndex = -1;

                //Busca Error
                Lberror.Text = "";

            }
            catch (Exception mm)
            {
                Variables.Error = mm.Message;
                Lberror.Visible = true;
                Lberror.Text = mm.Message;
            }
        }
        private void Campos_ordenados()
        {
            ddListaPageSize.Items.Clear();
            ddListaPageSize.Items.Insert(0, new ListItem("10", "10"));
            ddListaPageSize.Items.Insert(1, new ListItem("20", "20"));
            ddListaPageSize.Items.Insert(2, new ListItem("30", "30"));
            ddListaPageSize.Items.Insert(3, new ListItem("Todos", "1000"));
            ddListaPageSize.SelectedIndex = -1;

            //DrCategoria.Items.Clear();
            //DrCategoria.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrCategoria.Items.Insert(1, new ListItem("Sistemas", "1"));
            //DrCategoria.Items.Insert(2, new ListItem("Desarrollo", "2"));
            //DrCategoria.Items.Insert(2, new ListItem("Instaladores", "3"));
            //DrCategoria.Items.Insert(3, new ListItem("Web", "4"));
            //DrCategoria.Items.Insert(4, new ListItem("Office", "5"));
            //DrCategoria.Items.Insert(5, new ListItem("Sistemas Operativos", "6"));
            //DrCategoria.Items.Insert(6, new ListItem("Redes", "7"));
            //DrCategoria.Items.Insert(7, new ListItem("Aplicaciones", "8"));
            //DrCategoria.SelectedIndex = -1;
            DrCategoria.Items.Clear();
            DrCategoria.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            DrCategoria.Items.Insert(1, new ListItem("Informes", "1"));
            DrCategoria.Items.Insert(2, new ListItem("Impresoras", "2"));
            DrCategoria.Items.Insert(2, new ListItem("Tareas", "3"));
            DrCategoria.Items.Insert(3, new ListItem("Trabajos", "4"));
            DrCategoria.Items.Insert(4, new ListItem("Producción", "5"));
            DrCategoria.Items.Insert(5, new ListItem("Orden Carga", "6"));
            DrCategoria.Items.Insert(6, new ListItem("Lotes", "7"));
            DrCategoria.Items.Insert(7, new ListItem("Documentación", "8"));
            DrCategoria.SelectedIndex = -1;



            //DrSubCategoria.Items.Clear();
            //DrSubCategoria.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            //DrSubCategoria.Items.Insert(1, new ListItem("ASP.Net", "1"));
            //DrSubCategoria.Items.Insert(2, new ListItem("C++", "2"));
            //DrSubCategoria.Items.Insert(2, new ListItem("C#", "3"));
            //DrSubCategoria.Items.Insert(3, new ListItem("Python", "4"));
            //DrSubCategoria.Items.Insert(4, new ListItem("JAVA", "5"));
            //DrSubCategoria.Items.Insert(5, new ListItem("Maquinas Virtuales", "6"));
            //DrSubCategoria.Items.Insert(6, new ListItem("Imagen ISO", "7"));
            //DrSubCategoria.Items.Insert(7, new ListItem("Docker", "8"));
            //DrSubCategoria.SelectedIndex = -1;
            //DrSubCategoria.Items.Clear();

            DrSubCategoria.Items.Insert(0, new ListItem("Ninguno", "Ninguno"));
            DrSubCategoria.Items.Insert(1, new ListItem("Campos", "1"));
            DrSubCategoria.Items.Insert(2, new ListItem("Zona", "2"));
            DrSubCategoria.Items.Insert(2, new ListItem("Invernadero", "3"));
            DrSubCategoria.Items.Insert(3, new ListItem("Recursos Humanos", "4"));

            DrSubCategoria.SelectedIndex = -1;
        }


    }
}