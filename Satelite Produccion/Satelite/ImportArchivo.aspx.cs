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
    public partial class ImportArchivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
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
                    if (this.Session["Campos"] != null)
                    {
                        //ya esta cargado
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = Main.CargaCampos().Tables[0];
                        this.Session["Campos"] = dt;
                    }

                    DrArchivos.DataValueField = "ZID";
                    DrArchivos.DataTextField = "ZDESCRIPCION";

                    if (this.Session["Archivos"] != null)
                    {
                        DataTable dt = this.Session["Archivos"] as DataTable;
                        DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();



                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = Main.CargaArchivos().Tables[0];
                        this.Session["Archivos"] = dt;
                        DrArchivos.DataSource = dt; // EvaluacionSel.GargaQuery().Tables[0];
                        DrArchivos.DataBind();
                    }

                    Relaciones(1);
                }
            }
            catch (Exception ex)
            {
                Server.Transfer("thEnd.aspx");
            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa
        }


        public void Relaciones(int ID)
        {
            int MiID = ID;
            LBcampos.Items.Clear();

            DataTable dt = this.Session["Campos"] as DataTable;

            DataTable dt1 = new DataTable();
            dt1 = Main.CargaRelacionesArchivos(MiID).Tables[0];
            this.Session["ArchivoCampos"] = dt1;
            int visto = 0;

            DataTable dtt;
            dtt = new DataTable("Tabla");

            dtt.Columns.Add("ZID");
            dtt.Columns.Add("ZDESCRIPCION");

            DataRow drr;

            //const string fic = @"C:\Proyecto\Administracion\Admin\Public\output.txt";
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
            int i = 0;

            foreach (DataRow fila in dt.Rows)
            {
                visto = 0;
                foreach (DataRow dr in dt1.Rows)
                {
                    if (fila["ZID"].ToString() == dr["ZID_CAMPO"].ToString())
                    {
                        drr = dtt.NewRow();
                        drr[0] = Convert.ToInt32(fila["ZID"].ToString());
                        drr[1] = fila["ZDESCRIPCION"].ToString();
                        dtt.Rows.Add(drr);
                        //LBcampos.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                        visto = 1;
                        i += 1;
                        break;
                    }
                }

                if (visto == 0)
                {
                    LBcampos.Items.Add(new ListasID(fila["ZDESCRIPCION"].ToString(), Convert.ToInt32(fila["ZID"].ToString())).ToString());
                }
            }
            //sw.Close();
            if (i != 0)
            {
                this.Session["SelArchivoCampo"] = dtt;
            }
        }
        protected void ContactsGridView_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            // If multiple buttons are used in a GridView control, use the
            // CommandName property to determine which button was clicked.
            //if (e.CommandName == "Add")
            //{
            //    // Convert the row index stored in the CommandArgument
            //    // property to an Integer.
            //    int index = Convert.ToInt32(e.CommandArgument);

            //    // Retrieve the row that contains the button clicked 
            //    // by the user from the Rows collection.
            //    GridViewRow row = ContactsGridView.Rows[index];

            //    // Create a new ListItem object for the contact in the row.     
            //    ListItem item = new ListItem();
            //    item.Text = Server.HtmlDecode(row.Cells[2].Text) + " " +
            //      Server.HtmlDecode(row.Cells[3].Text);

            //    // If the contact is not already in the ListBox, add the ListItem 
            //    // object to the Items collection of the ListBox control. 
            //    if (!ContactsListBox.Items.Contains(item))
            //    {
            //        ContactsListBox.Items.Add(item);
            //    }
            //}
        }
        protected void BtnGuardaMascaraPer_click(object sender, EventArgs e)
        {
            //this.Session["PublicaPersonal"] = "0";// = false;
            //HtmlButton btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //btn2.Visible = true;
            ////btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            ////btn2.Visible = false;
            //OcultaPersonal();
            //GuardarMascara_Click(sender, e);
            ////GuardarPerfil_Click(sender, e);
        }
        protected void GuardaP4_Click(object sender, EventArgs e)
        {
            //HtmlButton btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //btn2.Visible = true;

            //btn2 = (HtmlButton)FindControl("btHAE");
            //btn2.Attributes["class"] = "btn btn-primary";
            //btn2.InnerText = "Editar Datos";
            //btn2 = (HtmlButton)FindControl("btHAC");
            //btn2.Attributes["class"] = "btn btn-warning disabled";

            //GuardarPerfil_Click(sender, e);
            //this.Session["Personal"] = "0";// = false;
        }
        protected void BtnActivate_click(object sender, EventArgs e)
        {
            //HtmlButton btn = (HtmlButton)sender;
            //string Miro = "";
            ////Seccion del menu 2
            //this.Session["Menu"] = "2";
            //if (btn.ID == "EdCon") //edita conocimiento
            //{
            //    HtmlButton btn2 = (HtmlButton)FindControl("GuCon");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        //Variables.Historial = true;
            //        this.Session["EditKnow"] = "1";// true;
            //        txtDescripcion.Visible = false;
            //        tituloaccion.Visible = false;
            //        TxTituKnow.Visible = true;
            //        TxDescripKnow.Visible = true;
            //        TxtVideo.Visible = true;
            //        TxtViewVideo.Visible = false;
            //        DRCategoriaC.Visible = true;
            //        LbCategoriaC.Visible = false;
            //        ModifImageCono.Visible = true;
            //        btn = (HtmlButton)FindControl("PublicCono");
            //        btn.Attributes["class"] = "btn btn-warning";
            //        btn.Visible = false;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        //Variables.Historial = false;
            //        this.Session["EditKnow"] = "0";// false;
            //        txtDescripcion.Visible = true;
            //        tituloaccion.Visible = true;
            //        TxTituKnow.Visible = false;
            //        TxDescripKnow.Visible = false;
            //        TxtVideo.Visible = false;
            //        TxtViewVideo.Visible = true;
            //        DRCategoriaC.Visible = false;
            //        LbCategoriaC.Visible = true;
            //        ModifImageCono.Visible = false;
            //        btn = (HtmlButton)FindControl("PublicCono");
            //        btn.Attributes["class"] = "btn btn-warning";
            //        btn.Visible = true;

            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            ////Seccion del menu 1
            //this.Session["Menu"] = "1";
            //this.Session["Sube"] = "0"; // "4";
            //if (btn.ID == "EditPublicaPersonal") //publicacion personal
            //{
            //    this.Session["Collapse"] = "1";
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Publicación";
            //        btn.Attributes["class"] = "btn btn-success";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi1");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi2");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi3");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi4");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi5");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi6");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi7");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi8");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi9");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi10");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi11");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi12");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi13");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi14");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I4");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I5");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHAE");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHAC");
            //        btn2.Visible = false;
            //        this.Session["PublicaPersonal"] = "1";// true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Publicación";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi1");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi2");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi3");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi4");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi5");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi6");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi7");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi8");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi9");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi10");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi11");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi12");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi13");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi14");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I4");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I5");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicaPersonal");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHAE");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHAC");
            //        btn2.Visible = true;
            //        this.Session["PublicaPersonal"] = "0";// false;
            //    }

            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "EditPublicProfesional") //publicacion personal
            //{
            //    this.Session["Collapse"] = "2";
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Publicación";
            //        btn.Attributes["class"] = "btn btn-success";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi16");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi17");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi18");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi19");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi20");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi21");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi22");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi23");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi24");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi25");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi26");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("bi27");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I12");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("I15");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicProfesional");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHBE");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHBC");
            //        btn2.Visible = false;


            //        this.Session["PublicaProfesional"] = "1";// = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Publicación";
            //        HtmlButton btn2 = (HtmlButton)FindControl("bi16");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi17");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi18");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi19");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi20");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi21");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi22");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi23");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi24");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi25");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi26");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("bi27");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I12");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("I15");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("GuardaPublicProfesional");
            //        btn2.Visible = false;
            //        btn2 = (HtmlButton)FindControl("btHBE");
            //        btn2.Visible = true;
            //        btn2 = (HtmlButton)FindControl("btHBC");
            //        btn2.Visible = true;
            //        this.Session["PublicaProfesional"] = "0";// false;
            //    }

            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}


            //if (btn.ID == "btD1D") //desempeño
            //{
            //    this.Session["Collapse"] = "4";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btD2D");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Desempeno"] = "1";// = true;
            //        P3.Visible = false;
            //        TxtP3.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Desempeno"] = "0";// false;
            //        P3.Visible = true;
            //        TxtP3.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "ImgTuring") //herramientas
            //{
            //    this.Session["Collapse"] = "6";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btD2D");
            //    //if (btn.Attributes["class"] == "btn btn-primary")
            //    //{
            //    //    btn.InnerText = "Cancelar";
            //    //    btn.Attributes["class"] = "btn btn-success";
            //    //    btn2.Attributes["class"] = "btn btn-warning";
            //    //    this.Session["Desempeno"] = "1";// = true;
            //    //    P3.Visible = false;
            //    //    TxtP3.Visible = true;
            //    //}
            //    //else
            //    //{
            //    //btn.Attributes["class"] = "btn btn-primary";
            //    //btn.InnerText = "Editar";
            //    //btn2.Attributes["class"] = "btn btn-warning disabled";
            //    //this.Session["Desempeno"] = "0";// false;
            //    //P3.Visible = true;
            //    //TxtP3.Visible = false;
            //    //}
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}


            //if (btn.ID == "btHAE") //edita perfil personal
            //{
            //    this.Session["Collapse"] = "1";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btHAC");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Datos";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Personal"] = "1";// = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;
            //        txtNombre.ReadOnly = false;
            //        TxFechaNacimiento.ReadOnly = false;
            //        TxtDNI.ReadOnly = false;
            //        txCalle.ReadOnly = false;
            //        TxtNumero.ReadOnly = false;
            //        TxtPuerta.ReadOnly = false;
            //        btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //        btn2.Visible = false;

            //        //DlPosCvid.Enabled = true;
            //        //dlPais.Enabled = true;
            //        //dlProvincia.Enabled = true;
            //        //DlMunicipio.Enabled = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Personal"] = "0";// false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;
            //        txtNombre.ReadOnly = true;
            //        TxFechaNacimiento.ReadOnly = true;
            //        TxtDNI.ReadOnly = true;
            //        txCalle.ReadOnly = true;
            //        TxtNumero.ReadOnly = true;
            //        TxtPuerta.ReadOnly = true;
            //        btn2 = (HtmlButton)FindControl("EditPublicaPersonal");
            //        btn2.Visible = true;
            //        //DlPosCvid.Enabled = false;
            //        //dlPais.Enabled = false;
            //        //dlProvincia.Enabled = false;
            //        //DlMunicipio.Enabled = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btHBE") //edita profesional
            //{
            //    this.Session["Collapse"] = "2";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btHBC");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar Datos";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Profesional"] = "1";//  = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;

            //        //DrEntidadFin.Enabled = true;
            //        //DrSolicCertificado.Enabled = true;
            //        //DlDispositivos.Enabled = true;

            //        TxtBlog.ReadOnly = false;
            //        TxtPeriodoFecha.ReadOnly = false;
            //        TxtIBAN.ReadOnly = false;
            //        TxtNumCuenta.ReadOnly = false;
            //        TxtKnow.ReadOnly = false;
            //        Txtvendidos.ReadOnly = false;
            //        TxtKnowInter.ReadOnly = false;
            //        TxtKnowUser.ReadOnly = false;
            //        TxtProfesion.ReadOnly = false;


            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar Datos";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Profesional"] = "0";//false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;

            //        //DrEntidadFin.Enabled = false;
            //        //DrSolicCertificado.Enabled = false;
            //        //DlDispositivos.Enabled = false;

            //        TxtBlog.ReadOnly = true;
            //        TxtPeriodoFecha.ReadOnly = true;
            //        TxtIBAN.ReadOnly = true;
            //        TxtNumCuenta.ReadOnly = true;
            //        TxtKnow.ReadOnly = true;
            //        Txtvendidos.ReadOnly = true;
            //        TxtKnowInter.ReadOnly = true;
            //        TxtKnowUser.ReadOnly = true;
            //        TxtProfesion.ReadOnly = true;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH1E") //academico
            //{
            //    this.Session["Collapse"] = "5";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH1C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Academico"] = "1";// = true;
            //        P1.Visible = false;
            //        TxtP1.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Academico"] = "0";//false;
            //        P1.Visible = true;
            //        TxtP1.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH2E") //laboral
            //{
            //    this.Session["Collapse"] = "3";
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH2C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Laboral"] = "1";// = true;
            //        P2.Visible = false;
            //        TxtP2.Visible = true;
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Laboral"] = "0";//  false;
            //        P2.Visible = true;
            //        TxtP2.Visible = false;
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //if (btn.ID == "btH3E") //edita boton P3
            //{
            //    HtmlButton btn2 = (HtmlButton)FindControl("btH3C");
            //    if (btn.Attributes["class"] == "btn btn-primary")
            //    {
            //        btn.InnerText = "Cancelar";
            //        btn.Attributes["class"] = "btn btn-success";
            //        btn2.Attributes["class"] = "btn btn-warning";
            //        this.Session["Historial"] = "1";//  = true; 
            //        P3.Visible = false;
            //        TxtP3.Visible = true;
            //        this.Session["Collapse"] = "5";
            //    }
            //    else
            //    {
            //        btn.Attributes["class"] = "btn btn-primary";
            //        btn.InnerText = "Editar";
            //        btn2.Attributes["class"] = "btn btn-warning disabled";
            //        this.Session["Historial"] = "0";// false;
            //        P3.Visible = true;
            //        TxtP3.Visible = false;
            //        this.Session["Collapse"] = "0";
            //    }
            //    Carga_Menus();
            //    Carga_collapse();
            //    return;
            //}

            //Variables.dsMiMascara.AcceptChanges();

            //if (this.Session["PublicaPersonal"].ToString() == "1") // == true)
            //{
            //    this.Session["Collapse"] = "1";
            //    if (btn.ID == "bi1") //ZIDUSUARIO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti1");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows) //dsMascaraPerfil
            //        {
            //            fila["ZUSUARIO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi2")//ZALTAFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti2");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALTAFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi3")//ZALIAS
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti3");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALIAS"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi4")//ZNACIMIENTO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti4");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZNACIMIENTO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi5")//ZDNI
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti5");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZDNI"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi6")//ZPAIS
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti6");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPAIS"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi7")//ZPROVINCIA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti7");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPROVINCIA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi8")//ZMUNICIPIO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti8");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZMUNICIPIO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi9")//ZCALLE
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti9");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZCALLE"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi10")//ZPUERTA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti10");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPUERTA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi11")//ZPUNUMERO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti11");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPUNUMERO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi12")//ZCOVID
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti12");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZCOVID"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi13")//ZALTAFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti13");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZALTAFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi14")//ZENTIDAD
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti14");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZENTIDAD"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (btn.ID == "bi1" || btn.ID == "bi2" || btn.ID == "bi3" || btn.ID == "bi4" || btn.ID == "bi5" ||
            //        btn.ID == "bi6" || btn.ID == "bi7" || btn.ID == "bi8" || btn.ID == "bi9" || btn.ID == "bi10" ||
            //        btn.ID == "bi11" || btn.ID == "bi12" || btn.ID == "bi13" || btn.ID == "bi14")
            //    {
            //        this.Session["Collapse"] = "1";
            //    }
            //}

            //if (this.Session["PublicaProfesional"].ToString() == "1") // true)
            //{
            //    this.Session["Collapse"] = "2";
            //    if (btn.ID == "bi17")//ZIBAN
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti17");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZIBAN"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi18")//ZNUMCUENTA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti18");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZNUMCUENTA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi19")//ZSOLICITUD
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti19");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZSOLICITUD"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi20")//ZPERIODOFECHA
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti20");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPERIODOFECHA"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi21")//ZWORKING
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti21");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKING"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi22")//ZWORKINGVENT
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti22");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKINGVENT"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi23")//ZWORKINGINTER
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti23");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZWORKINGINTER"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi24")//ZBLOG
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti24");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZBLOG"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi25")//ZPROFESION
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti25");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZPROFESION"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi26")//ZDISPOSITIVO
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti26");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZDISPOSITIVO"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }

            //    if (btn.ID == "bi27")//ZMAIL
            //    {
            //        HtmlGenericControl img = (HtmlGenericControl)FindControl("ti27");
            //        if (btn.Attributes["class"] == "btn btn-danger btn-circle")
            //        {
            //            btn.Attributes["class"] = "btn btn-success btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-up";
            //            Miro = "1";
            //        }
            //        else
            //        {
            //            btn.Attributes["class"] = "btn btn-danger btn-circle";
            //            img.Attributes["class"] = "fa fa-thumbs-o-down";
            //            Miro = "0";
            //        }
            //        foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //        {
            //            fila["ZMAIL"] = Miro;
            //            break;
            //        }
            //        DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //        return;
            //    }
            //}
            //else
            //{
            //    if (btn.ID == "bi16" || btn.ID == "bi17" || btn.ID == "bi18" || btn.ID == "bi19" || btn.ID == "bi20" ||
            //        btn.ID == "bi21" || btn.ID == "bi22" || btn.ID == "bi23" || btn.ID == "bi24" || btn.ID == "bi25" ||
            //        btn.ID == "bi26" || btn.ID == "bi27")
            //    {
            //        this.Session["Collapse"] = "2";
            //    }
            //}

            //if (btn.ID == "I8")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I1");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZDESEMPENO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I9")//I1 I8 I9 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I1");
            //    img.Visible = true;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZDESEMPENO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I6")//I1 I8 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I2");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZLABORAL"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I7")//I1 I8 desempeño  I2 I6  I7 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I2");
            //    img.Visible = false;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZLABORAL"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I11")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I3");
            //    img.Visible = false;
            //    Miro = "0";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZACADEMICO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}

            //if (btn.ID == "I10")//I1 I8 desempeño  I2 I6 ZLABORAL Laboral  I3 I11 I10 Académico
            //{
            //    HtmlButton img = (HtmlButton)FindControl("I3");
            //    img.Visible = false;
            //    Miro = "1";
            //    foreach (DataRow fila in Variables.dsMiMascara.Rows)
            //    {
            //        fila["ZACADEMICO"] = Miro;
            //        break;
            //    }
            //    DBHelper.Actualiza_Mascara(Variables.dsMiMascara, "");
            //    return;
            //}



            //Carga_collapse();

        }
        protected void DrCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Index
            this.Session["categoria"] = DrArchivos.SelectedIndex.ToString();
            //Texto
            //Variables.categoria = DrCategoria.SelectedItem.ToString();
            //DlConocimientos.Items.Clear();
            //Repeater1.DataSource = null;
            //Repeater1.DataBind();
            //txtDescripcion.Text = "";
            //HtmlImage img = (HtmlImage)FindControl("ImgPrint");
            //img.Attributes.Add("src", "Images/no-img.jpg");
            ////iImage.ImageUrl = "";
            //tituloaccion.Text = "";
            //txtAutor.Text = "";
            //TxtFecha.Text = "";

            //if (DrCategoria.SelectedItem.ToString() == "Todos")
            //{
            //    Variables.FormularioShell = null;
            //    Repeater1.DataSource = Variables.dsFormularios;
            //    Repeater1.DataBind();
            //    DlConocimientos.DataSource = Variables.dsFormularios;
            //    DlConocimientos.DataValueField = "ZID";
            //    DlConocimientos.DataTextField = "ZTITULO";
            //    DlConocimientos.DataBind();

            //    foreach (DataRow fila in Variables.dsFormularios.Rows)//B
            //    {
            //        string Miro = fila["ZID"].ToString();
            //        //Variables.IDFormulario = fila["ZID"].ToString();
            //        txtDescripcion.Text = fila["ZDESCRIPCION"].ToString();
            //        img.Attributes.Add("src", fila["ZIMAGEN"].ToString());
            //        //iImage.ImageUrl = fila["ZIMAGEN"].ToString();
            //        tituloaccion.Text = fila["ZTITULO"].ToString();
            //        txtAutor.Text = fila["ZTITULO"].ToString();
            //        TxtFecha.Text = fila["ZALTAFECHA"].ToString();
            //        this.Session["categoria"] = fila["ZIDCATEGORIA"].ToString();
            //        this.Session["PublicaComentarios"] = fila["ZCOMENTARIO"].ToString();
            //        this.Session["Publico"] = fila["ZPUBLICO"].ToString();
            //        this.Session["IDFormulario"] = fila["ZID"].ToString();
            //        this.Session["idotheruser"] = fila["ZIDUSUARIO"].ToString();
            //        Carga_mensajes(fila["ZID"].ToString());
            //        if (fila["ZPRECIO"].ToString() == "1")
            //        {
            //            txtPVP.Text = "El importe de este conocimiento es de 1 €, o bien, intercambiar por uno de tus conocimientos con este Creador de conocimientos";
            //        }
            //        else
            //        {
            //            txtPVP.Text = "El importe de este conocimiento es gratuito. Gracias al Creador de este conocimiento por aportar este know how al grupo";
            //        }
            //        solicitaConId.Text = "Solicitar este Conocimiento";
            //        solicitaConId.Enabled = true;
            //        solicitaConId.Attributes["class"] = "btn btn-success";
            //        if (this.Session["idotheruser"].ToString() == this.Session["idusuario"].ToString())
            //        {
            //            //Es un conocimiento suyo
            //            solicitaConId.Text = "Este Conocimiento es tuyo";
            //            solicitaConId.Attributes["class"] = "btn btn-warning";
            //            solicitaConId.Enabled = false;
            //            break;
            //        }

            //        foreach (DataRow fila2 in Variables.dsMisFormularios.Rows)//B
            //        {
            //            if (fila["ZID"].ToString().ToString() == fila2["ZINDICE"].ToString())
            //            {
            //                //Ya lo tiene
            //                solicitaConId.Text = "Este Conocimiento ya lo tienes";
            //                solicitaConId.Attributes["class"] = "btn btn-info";
            //                solicitaConId.Enabled = false;
            //                break;
            //            }
            //        }
            //        break;
            //    }
            //}
            //else
            //{
            //    Variables.FormularioShell = null;
            //    foreach (DataRow fila in Variables.dsFormularios.Rows)//B
            //    {
            //        if (fila["ZIDCATEGORIA"].ToString() == this.Session["categoria"].ToString())
            //        {
            //            Variables.FormularioShell = DBHelper.ClasificaTableUser("ZCATEGORIAS", this.Session["categoria"].ToString(), "", "", 0);
            //            //dsFormularios = DBHelper.BuscaLineasDataTables(fila["ZIDCATEGORIA"].ToString(), HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000000006.000");
            //            Repeater1.DataSource = Variables.FormularioShell;
            //            Repeater1.DataBind();
            //            DlConocimientos.DataSource = Variables.FormularioShell;
            //            DlConocimientos.DataValueField = "ZID";
            //            DlConocimientos.DataTextField = "ZTITULO";
            //            DlConocimientos.DataBind();
            //            break;
            //        }
            //    }

            //    if (Variables.FormularioShell != null)
            //    {
            //        foreach (DataRow fila in Variables.FormularioShell.Rows)//B
            //        {
            //            string Miro = fila["ZID"].ToString();
            //            //Variables.IDFormulario = fila["ZID"].ToString();
            //            txtDescripcion.Text = fila["ZDESCRIPCION"].ToString();
            //            img.Attributes.Add("src", fila["ZIMAGEN"].ToString());
            //            //iImage.ImageUrl = fila["ZIMAGEN"].ToString();
            //            tituloaccion.Text = fila["ZTITULO"].ToString();
            //            txtAutor.Text = fila["ZTITULO"].ToString();
            //            TxtFecha.Text = fila["ZALTAFECHA"].ToString();
            //            this.Session["PublicaComentarios"] = fila["ZCOMENTARIO"].ToString();
            //            this.Session["Publico"] = fila["ZPUBLICO"].ToString();
            //            this.Session["IDFormulario"] = fila["ZID"].ToString();
            //            this.Session["idotheruser"] = fila["ZIDUSUARIO"].ToString();
            //            this.Session["categoria"] = fila["ZIDCATEGORIA"].ToString();
            //            Carga_mensajes(fila["ZID"].ToString());
            //            if (fila["ZPRECIO"].ToString() == "1")
            //            {
            //                txtPVP.Text = "El importe de este conocimiento es de 1 €, o bien, intercambiar por uno de tus conocimientos con este Creador de conocimientos";
            //            }
            //            else
            //            {
            //                txtPVP.Text = "El importe de este conocimiento es gratuito. Gracias al Creador de este conocimiento por aportar este know how al grupo";
            //            }
            //            solicitaConId.Text = "Solicitar este Conocimiento";
            //            solicitaConId.Enabled = true;
            //            solicitaConId.Attributes["class"] = "btn btn-success";
            //            if (this.Session["idotheruser"].ToString() == this.Session["idusuario"].ToString())
            //            {
            //                //Es un conocimiento suyo
            //                solicitaConId.Text = "Este Conocimiento es tuyo";
            //                solicitaConId.Attributes["class"] = "btn btn-warning";
            //                solicitaConId.Enabled = false;
            //                break;
            //            }

            //            foreach (DataRow fila2 in Variables.dsMisFormularios.Rows)//B
            //            {
            //                if (fila["ZID"].ToString().ToString() == fila2["ZINDICE"].ToString())
            //                {
            //                    //Ya lo tiene
            //                    solicitaConId.Text = "Este Conocimiento ya lo tienes";
            //                    solicitaConId.Attributes["class"] = "btn btn-info";
            //                    solicitaConId.Enabled = false;
            //                    break;
            //                }
            //            }
            //            break;
            //        }
            //    }
            //}


            //DataTable dt = DBHelper.ClasificaTableUser("CATEGORIAS", Variables.categoria, "", 0);
            //mysliderF1.DataSource = dt;
            //mysliderF1.DataBind();
        }
    }
}