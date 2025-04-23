using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Satelite
{
    public partial class Flujos : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private FlujoService _flujoService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _flujoService = new FlujoService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Inicializar controles
                CargarRangos();
                CargarEtapas();
                CargarResponsables();

                // Si hay parámetros en la URL, cargar datos específicos
                if (!string.IsNullOrEmpty(Request.QueryString["ranAnio"]) &&
                    !string.IsNullOrEmpty(Request.QueryString["ranCodigo"]))
                {
                    int ranAnio = Convert.ToInt32(Request.QueryString["ranAnio"]);
                    string ranCodigo = Request.QueryString["ranCodigo"];

                    // Preseleccionar el rango y cargar sus flujos
                    PreseleccionarRango(ranAnio, ranCodigo);
                    CargarFlujos(ranAnio, ranCodigo);
                }
            }
        }

        protected void CargarRangos()
        {
            DataTable rangos = _flujoService.ObtenerRangosActivos();

            ddlRangos.DataSource = rangos;
            ddlRangos.DataTextField = "RangoInfo";
            ddlRangos.DataValueField = "RangoId";
            ddlRangos.DataBind();

            // Agregar opción por defecto
            ddlRangos.Items.Insert(0, new ListItem("-- Seleccione un rango --", "0"));
        }

        protected void CargarEtapas()
        {
            DataTable etapas = _flujoService.ObtenerEtapas();

            ddlEtapa.DataSource = etapas;
            ddlEtapa.DataTextField = "Nombre";
            ddlEtapa.DataValueField = "EtapaId";
            ddlEtapa.DataBind();

            // Agregar opción por defecto
            ddlEtapa.Items.Insert(0, new ListItem("-- Seleccione una etapa --", "0"));
        }

        protected void CargarResponsables()
        {
            DataTable responsables = _flujoService.ObtenerResponsables();

            ddlResponsable.DataSource = responsables;
            ddlResponsable.DataTextField = "NombreCompleto";
            ddlResponsable.DataValueField = "UsuarioId";
            ddlResponsable.DataBind();

            // Agregar opción por defecto
            ddlResponsable.Items.Insert(0, new ListItem("-- Seleccione un responsable --", "0"));
        }

        private void PreseleccionarRango(int ranAnio, string ranCodigo)
        {
            string rangoId = $"{ranAnio}-{ranCodigo}";
            if (ddlRangos.Items.FindByValue(rangoId) != null)
            {
                ddlRangos.SelectedValue = rangoId;
            }
        }

        protected void ddlRangos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRangos.SelectedValue != "0")
            {
                // Obtener el año y código del rango seleccionado
                string[] rangoInfo = ddlRangos.SelectedValue.Split('-');
                if (rangoInfo.Length == 2)
                {
                    int ranAnio = Convert.ToInt32(rangoInfo[0]);
                    string ranCodigo = rangoInfo[1];

                    // Cargar flujos para el rango seleccionado
                    CargarFlujos(ranAnio, ranCodigo);

                    // Mostrar panel de nuevo flujo
                    pnlNuevoFlujo.Visible = true;
                }
            }
            else
            {
                // Ocultar panel de nuevo flujo y limpiar grid de flujos
                pnlNuevoFlujo.Visible = false;
                gvFlujos.DataSource = null;
                gvFlujos.DataBind();
            }
        }

        private void CargarFlujos(int ranAnio, string ranCodigo)
        {
            DataTable flujos = _flujoService.ObtenerFlujosPorRango(ranAnio, ranCodigo);

            gvFlujos.DataSource = flujos;
            gvFlujos.DataBind();

            // Mostrar mensaje si no hay flujos
            lblSinFlujos.Visible = (flujos.Rows.Count == 0);
        }

        protected void btnCrearFlujo_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un rango
            if (ddlRangos.SelectedValue == "0")
            {
                MostrarMensaje("Por favor seleccione un rango.", false);
                return;
            }

            // Validar que se haya ingresado una descripción
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MostrarMensaje("Por favor ingrese una descripción para el flujo.", false);
                return;
            }

            try
            {
                // Obtener información del rango seleccionado
                string[] rangoInfo = ddlRangos.SelectedValue.Split('-');
                int ranAnio = Convert.ToInt32(rangoInfo[0]);
                string ranCodigo = rangoInfo[1];

                // Obtener usuario actual
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                string usuarioNombre = Session["NombreUsuario"].ToString();

                // Crear nuevo flujo
                string descripcion = txtDescripcion.Text.Trim();
                int flujoId = _flujoService.CrearFlujo(descripcion, usuarioNombre, ranAnio, ranCodigo, usuarioId);

                // Verificar si se debe agregar la primera etapa automáticamente
                if (chkAgregarEtapa.Checked)
                {
                    if (ddlEtapa.SelectedValue == "0" || ddlResponsable.SelectedValue == "0")
                    {
                        MostrarMensaje("Flujo creado, pero no se pudo agregar la etapa. Por favor seleccione etapa y responsable.", false);
                    }
                    else
                    {
                        // Agregar primera etapa al flujo
                        int etapaId = Convert.ToInt32(ddlEtapa.SelectedValue);
                        int responsableId = Convert.ToInt32(ddlResponsable.SelectedValue);

                        _flujoService.AgregarEtapaFlujo(flujoId, etapaId, responsableId, DateTime.Now, "Pendiente", usuarioId);
                        MostrarMensaje("Flujo creado exitosamente con su primera etapa.", true);
                    }
                }
                else
                {
                    MostrarMensaje("Flujo creado exitosamente.", true);
                }

                // Recargar flujos y limpiar formulario
                CargarFlujos(ranAnio, ranCodigo);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear el flujo: " + ex.Message, false);
            }
        }

        protected void gvFlujos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalles")
            {
                int flujoId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"DetallesFlujo.aspx?id={flujoId}");
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }

        private void LimpiarFormulario()
        {
            txtDescripcion.Text = string.Empty;
            ddlEtapa.SelectedValue = "0";
            ddlResponsable.SelectedValue = "0";
            chkAgregarEtapa.Checked = false;
            pnlEtapaInicial.Visible = false;
        }

        protected void chkAgregarEtapa_CheckedChanged(object sender, EventArgs e)
        {
            pnlEtapaInicial.Visible = chkAgregarEtapa.Checked;
        }
    }

    // Clase de servicio para gestión de flujos
    public class FlujoService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public FlujoService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerRangosActivos()
        {
            return _queryBuilder
                .Select("CAST(RanAnio AS VARCHAR) + '-' + RanCodigo AS RangoInfo, " +
                        "CAST(RanAnio AS VARCHAR) + '-' + RanCodigo AS RangoId, " +
                        "RanAnio, RanCodigo")
                .From("Rangos")
                .Where(true, "Estado", "=", "Activo")
                .OrderBy("RanAnio DESC, RanCodigo")
                .Execute();
        }

        public DataTable ObtenerEtapas()
        {
            return _queryBuilder
                .Select("EtapaId, Nombre, Descripcion")
                .From("Etapas")
                .OrderBy("Orden")
                .Execute();
        }

        public DataTable ObtenerResponsables()
        {
            return _queryBuilder
                .Select("UsuarioId, NombreCompleto, Email")
                .From("Usuarios")
                .Where(true, "Activo", "=", true)
                .OrderBy("NombreCompleto")
                .Execute();
        }

        public DataTable ObtenerFlujosPorRango(int ranAnio, string ranCodigo)
        {
            return _queryBuilder
                .Select("f.FlujoId, f.Descripcion, f.FechaCreacion, f.UsuarioCreacion, " +
                        "(SELECT COUNT(*) FROM FlujoDetalles fd WHERE fd.FlujoId = f.FlujoId) AS NumeroEtapas, " +
                        "(SELECT COUNT(*) FROM FlujoDetalles fd WHERE fd.FlujoId = f.FlujoId AND fd.Estado = 'Completado') AS EtapasCompletadas")
                .From("Flujos AS f")
                .Join("FlujoRangos AS fr", "f.FlujoId = fr.FlujoId")
                .Where(true, "fr.RanAnio", "=", ranAnio)
                .AndWhere(true, "fr.RanCodigo", "=", ranCodigo)
                .OrderBy("f.FechaCreacion DESC")
                .Execute();
        }

        public int CrearFlujo(string descripcion, string usuarioCreacion, int ranAnio, string ranCodigo, int usuarioId)
        {
            // Verificar que el rango exista y esté activo
            var rangoActivo = _queryBuilder
                .Select("COUNT(*)")
                .From("Rangos")
                .Where(true, "RanAnio", "=", ranAnio)
                .AndWhere(true, "RanCodigo", "=", ranCodigo)
                .AndWhere(true, "Estado", "=", "Activo")
                .Execute();

            if (Convert.ToInt32(rangoActivo.Rows[0][0]) == 0)
            {
                throw new Exception("El rango seleccionado no existe o no está activo.");
            }

            // Primero crear el flujo
            string insertFlujoSql = "INSERT INTO Flujos (Descripcion, FechaCreacion, UsuarioCreacion) " +
                                   "VALUES (@Descripcion, @FechaCreacion, @UsuarioCreacion); " +
                                   "SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> flujoParams = new Dictionary<string, object>
            {
                { "@Descripcion", descripcion },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioCreacion }
            };

            // Ejecutar la inserción y obtener el ID generado
            object result = _dbConnection.ExecuteScalar(insertFlujoSql, flujoParams);
            int flujoId = Convert.ToInt32(result);

            // Luego asociar el flujo con el rango
            string insertFlujoRangoSql = "INSERT INTO FlujoRangos (FlujoId, RanAnio, RanCodigo) " +
                                        "VALUES (@FlujoId, @RanAnio, @RanCodigo)";

            Dictionary<string, object> flujoRangoParams = new Dictionary<string, object>
            {
                { "@FlujoId", flujoId },
                { "@RanAnio", ranAnio },
                { "@RanCodigo", ranCodigo }
            };

            _dbConnection.ExecuteNonQuery(insertFlujoRangoSql, flujoRangoParams);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "Flujos", descripcion);

            return flujoId;
        }

        public void AgregarEtapaFlujo(int flujoId, int etapaId, int responsableId, DateTime fechaInicio, string estado, int usuarioId)
        {
            // Verificar que el flujo exista
            var flujoExiste = _queryBuilder
                .Select("COUNT(*)")
                .From("Flujos")
                .Where(true, "FlujoId", "=", flujoId)
                .Execute();

            if (Convert.ToInt32(flujoExiste.Rows[0][0]) == 0)
            {
                throw new Exception("El flujo seleccionado no existe.");
            }

            // Verificar que la etapa no esté ya asignada en este flujo
            var etapaExistente = _queryBuilder
                .Select("COUNT(*)")
                .From("FlujoDetalles")
                .Where(true, "FlujoId", "=", flujoId)
                .AndWhere(true, "EtapaId", "=", etapaId)
                .Execute();

            if (Convert.ToInt32(etapaExistente.Rows[0][0]) > 0)
            {
                throw new Exception("Esta etapa ya está asignada a este flujo.");
            }

            string sql = @"
                INSERT INTO FlujoDetalles (
                    FlujoId, 
                    EtapaId, 
                    ResponsableId, 
                    FechaInicio, 
                    Estado, 
                    Orden
                ) VALUES (
                    @FlujoId, 
                    @EtapaId, 
                    @ResponsableId, 
                    @FechaInicio, 
                    @Estado, 
                    (SELECT ISNULL(MAX(Orden), 0) + 1 FROM FlujoDetalles WHERE FlujoId = @FlujoId)
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@FlujoId", flujoId },
                { "@EtapaId", etapaId },
                { "@ResponsableId", responsableId },
                { "@FechaInicio", fechaInicio },
                { "@Estado", estado }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Obtener nombre de la etapa para el historial
            var etapaNombre = _queryBuilder
                .Select("Nombre")
                .From("Etapas")
                .Where(true, "EtapaId", "=", etapaId)
                .Execute();

            string nombreEtapa = etapaNombre.Rows[0]["Nombre"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "AgregarEtapa", "FlujoDetalles", $"Etapa {nombreEtapa} al flujo {flujoId}");
        }

        private void RegistrarAccionHistorial(int usuarioId, string accion, string tablaAfectada, string detalles)
        {
            string sql = @"
                INSERT INTO HistorialAcciones (
                    UsuarioId,
                    FechaRegistro,
                    Accion,
                    TablaAfectada,
                    Detalles
                ) VALUES (
                    @UsuarioId,
                    @FechaRegistro,
                    @Accion,
                    @TablaAfectada,
                    @Detalles
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UsuarioId", usuarioId },
                { "@FechaRegistro", DateTime.Now },
                { "@Accion", accion },
                { "@TablaAfectada", tablaAfectada },
                { "@Detalles", detalles }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);
        }
    }
}