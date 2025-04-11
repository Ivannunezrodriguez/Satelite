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
using System.Data.Odbc;



namespace Satelite
{
    public partial class FormPrint : System.Web.UI.Page
    {

        //private int registros = 0;
        //private string[] ListadoArchivos;
        //private static int IDDiv = 0;
        //private static string IDTABLA = "-1";

#pragma warning disable CS0649 // El campo 'FormPrint.ArrayTextBoxs' nunca se asigna y siempre tendrá el valor predeterminado null
        static TextBox[] ArrayTextBoxs;
#pragma warning restore CS0649 // El campo 'FormPrint.ArrayTextBoxs' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'FormPrint.ArrayLabels' nunca se asigna y siempre tendrá el valor predeterminado null
        static Label[] ArrayLabels;
#pragma warning restore CS0649 // El campo 'FormPrint.ArrayLabels' nunca se asigna y siempre tendrá el valor predeterminado null
#pragma warning disable CS0649 // El campo 'FormPrint.ArrayCombos' nunca se asigna y siempre tendrá el valor predeterminado null
        static DropDownList[] ArrayCombos;
#pragma warning restore CS0649 // El campo 'FormPrint.ArrayCombos' nunca se asigna y siempre tendrá el valor predeterminado null
        static int contadorControles;

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
                    Server.Transfer("Login.aspx"); //Default
                }

                if (!IsPostBack)
                {
                    this.Session["IDSecuencia"] = "0";
                    this.Session["IDProcedimiento"] = "0";
                    //this.Session["DESARROLLO"] = "0";
                    //ChkSlot.Visible = false;
                    Variables.mensajeserver = "";
                }
                else
                {

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
                string a = ex.Message;
                if (this.Session["Error"].ToString() == "0")
                {
                    Server.Transfer("Login.aspx");
                }
                else
                {
                    Server.Transfer("thEnd.aspx");
                }
            }
        }

        

            
        protected void BtSaltoLinea_Click(object sender, EventArgs e)
        {

        }

        protected void DrPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtCodeQR_Click(object sender, EventArgs e)
        {

        }

        protected void btPrinter_Click(object sender, EventArgs e)
        {

        }


        protected void BtBuscaFiltro_Click(object sender, EventArgs e)
        {
            
        }

        protected void BtGuardaGrid_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancelaGrid_Click(object sender, EventArgs e)
        {

        }
        protected void btnDeleteGrid_Click(object sender, EventArgs e)
        {

        }
        protected void btnModificaGrid_Click(object sender, EventArgs e)
        {

        }
        protected void DrConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(string MiCampo)
        {
            try
            {
                int numeroRegistro = contadorControles;
                TextBox nuevoTxt = new TextBox();
                nuevoTxt.ID = "txt" + numeroRegistro.ToString();
                nuevoTxt.Width = 400;
                nuevoTxt.Text = "";
                ArrayTextBoxs[numeroRegistro] = nuevoTxt;

                Label nuevoLB = new Label();
                nuevoLB.ID = "LB" + numeroRegistro.ToString();
                nuevoLB.Width = 400;
                nuevoLB.Text = MiCampo;
                ArrayLabels[numeroRegistro] = nuevoLB;

                DropDownList nuevoCmb = new DropDownList();
                nuevoCmb.ID = "cmb" + numeroRegistro.ToString();
                nuevoCmb.Items.Add("Seleccione uno");
                nuevoCmb.SelectedIndex = 0;

                ArrayCombos[numeroRegistro] = nuevoCmb;
                AgregarControles(nuevoTxt, nuevoCmb, nuevoLB);
                contadorControles++;
            }
            catch (Exception ex)
            {
                Variables.mensajeserver = ex.Message;
            }
        }

        private void AgregarControles(TextBox txt, DropDownList cmb, Label lb)
        {
            try
            {
                pnlControles.Controls.Add(lb);
                pnlControles.Controls.Add(new LiteralControl("lb"));
                pnlControles.Controls.Add(txt);
                pnlControles.Controls.Add(new LiteralControl("tx"));
                pnlControles.Controls.Add(cmb);
                pnlControles.Controls.Add(new LiteralControl("cb"));
            }
            catch (Exception ex)
            {
                Variables.mensajeserver = ex.Message;
            }
        }
    }
}