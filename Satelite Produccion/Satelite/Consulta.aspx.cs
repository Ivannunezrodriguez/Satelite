using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Satelite
{
    public partial class ConsultaFluent : System.Web.UI.Page
    {
        #region Variables
        ConexionSql conexion = new ConexionSql();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización inicial
                new ConsultaBuilder()
                    .WithDefaultConfiguration()
                    .Initialize(this);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Utilizando el Fluent API para realizar búsquedas
            new ConsultaBuilder()
                .WithFechas(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFiltros(ddlStatus.SelectedValue, txtBus.Text)
                .Buscar(this);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            // Utilizando el Fluent API para exportar
            new ConsultaBuilder()
                .WithFechas(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFiltros(ddlStatus.SelectedValue, txtBus.Text)
                .Exportar(this);
        }
    }
