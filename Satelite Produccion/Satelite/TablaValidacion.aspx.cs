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
    public partial class Validacion : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private ValidacionService _validacionService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _validacionService = new ValidacionService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Inicializar controles
                CargarTablas();

                // Si hay parámetros en la URL, cargar datos específicos
                if (!string.IsNullOrEmpty(Request.QueryString["tabla"]))
                {
                    string tabla = Request.QueryString["tabla"];
                    ddlTablas.SelectedValue = tabla;
                    CargarCamposTabla(tabla);
                    CargarReglas(tabla);
                }
            }
        }

        protected void CargarTablas()
        {
            DataTable tablas = _validacionService.ObtenerTablas();

            ddlTablas.DataSource = tablas;
            ddlTablas.DataTextField = "NombreTabla";
            ddlTablas.DataValueField = "NombreTabla";
            ddlTablas.DataBind();

            // Agregar opción por defecto
            ddlTablas.Items.Insert(0, new ListItem("-- Seleccione una tabla --", "0"));
        }

        protected void CargarCamposTabla(string nombreTabla)
        {
            if (nombreTabla != "0")
            {
                DataTable campos = _validacionService.ObtenerCamposTabla(nombreTabla);

                ddlCampo.DataSource = campos;
                ddlCampo.DataTextField = "NombreCampo";
                ddlCampo.DataValueField = "NombreCampo";
                ddlCampo.DataBind();

                // Agregar opción por defecto
                ddlCampo.Items.Insert(0, new ListItem("-- Seleccione un campo --", "0"));

                // Habilitar el panel de reglas
                pnlReglas.Visible = true;
            }
            else
            {
                ddlCampo.Items.Clear();
                ddlCampo.Items.Insert(0, new ListItem("-- Seleccione un campo --", "0"));
                pnlReglas.Visible = false;
            }
        }

        protected void CargarReglas(string nombreTabla)
        {
            DataTable reglas = _validacionService.ObtenerReglasValidacion(nombreTabla);

            gvReglas.DataSource = reglas;
            gvReglas.DataBind();

            // Mostrar mensaje si no hay reglas
            lblSinReglas.Visible = (reglas.Rows.Count == 0);
        }

        protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombreTabla = ddlTablas.SelectedValue;

            // Limpiar formulario
            LimpiarFormulario();

            // Cargar campos y reglas para la tabla seleccionada
            if (nombreTabla != "0")
            {
                CargarCamposTabla(nombreTabla);
                CargarReglas(nombreTabla);
            }
            else
            {
                pnlReglas.Visible = false;
                gvReglas.DataSource = null;
                gvReglas.DataBind();
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCampo.SelectedValue != "0")
            {
                // Cargar tipo de datos del campo seleccionado
                string nombreTabla = ddlTablas.SelectedValue;
                string nombreCampo = ddlCampo.SelectedValue;

                string tipoDato = _validacionService.ObtenerTipoDatoCampo(nombreTabla, nombreCampo);

                // Configurar controles según el tipo de dato
                ConfigurarControlesPorTipoDato(tipoDato);
            }
            else
            {
                // Restablecer controles a estado predeterminado
                ResetearControlesValidacion();
            }
        }

        private void ConfigurarControlesPorTipoDato(string tipoDato)
        {
            // Mostrar u ocultar controles según el tipo de dato
            bool esNumerico = tipoDato.Contains("int") || tipoDato.Contains("decimal") || tipoDato.Contains("float");
            bool esFecha = tipoDato.Contains("date") || tipoDato.Contains("time");
            bool esTexto = tipoDato.Contains("char") || tipoDato.Contains("text");

            // Configurar opciones de tipo de regla
            ddlTipoRegla.Items.Clear();

            // Agregar opción por defecto
            ddlTipoRegla.Items.Add(new ListItem("-- Seleccione un tipo --", "0"));

            // Agregar opciones comunes
            ddlTipoRegla.Items.Add(new ListItem("Requerido", "Requerido"));

            // Agregar opciones específicas según tipo de dato
            if (esNumerico)
            {
                ddlTipoRegla.Items.Add(new ListItem("Rango", "Rango"));
                ddlTipoRegla.Items.Add(new ListItem("Mayor que", "MayorQue"));
                ddlTipoRegla.Items.Add(new ListItem("Menor que", "MenorQue"));
            }
            else if (esFecha)
            {
                ddlTipoRegla.Items.Add(new ListItem("Rango de fechas", "RangoFecha"));
                ddlTipoRegla.Items.Add(new ListItem("Fecha posterior a", "FechaPosterior"));
                ddlTipoRegla.Items.Add(new ListItem("Fecha anterior a", "FechaAnterior"));
            }
            else if (esTexto)
            {
                ddlTipoRegla.Items.Add(new ListItem("Longitud máxima", "LongitudMaxima"));
                ddlTipoRegla.Items.Add(new ListItem("Longitud mínima", "LongitudMinima"));
                ddlTipoRegla.Items.Add(new ListItem("Expresión regular", "RegEx"));
                ddlTipoRegla.Items.Add(new ListItem("Lista de valores", "ListaValores"));
            }

            // Habilitar el panel de tipo de regla
            pnlTipoRegla.Visible = true;
        }

        protected void ddlTipoRegla_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoRegla = ddlTipoRegla.SelectedValue;

            // Ocultar todos los paneles de parámetros
            pnlValorUnico.Visible = false;
            pnlRango.Visible = false;
            pnlRegex.Visible = false;
            pnlListaValores.Visible = false;

            // Mostrar el panel correspondiente según el tipo de regla
            switch (tipoRegla)
            {
                case "Requerido":
                    // No requiere parámetros adicionales
                    break;
                case "Rango":
                case "RangoFecha":
                    pnlRango.Visible = true;
                    break;
                case "MayorQue":
                case "MenorQue":
                case "FechaPosterior":
                case "FechaAnterior":
                case "LongitudMaxima":
                case "LongitudMinima":
                    pnlValorUnico.Visible = true;
                    break;
                case "RegEx":
                    pnlRegex.Visible = true;
                    break;
                case "ListaValores":
                    pnlListaValores.Visible = true;
                    break;
                default:
                    // No mostrar ningún panel si no se ha seleccionado un tipo válido
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos
            if (ddlTablas.SelectedValue == "0" || ddlCampo.SelectedValue == "0" || ddlTipoRegla.SelectedValue == "0")
            {
                MostrarMensaje("Por favor complete todos los campos requeridos.", false);
                return;
            }

            try
            {
                // Obtener datos del formulario
                string nombreTabla = ddlTablas.SelectedValue;
                string nombreCampo = ddlCampo.SelectedValue;
                string tipoRegla = ddlTipoRegla.SelectedValue;
                string parametros = ObtenerParametrosRegla(tipoRegla);
                string mensajeError = txtMensajeError.Text.Trim();
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Verificar si ya existe una regla para esta combinación
                bool reglaExiste = _validacionService.ExisteReglaValidacion(nombreTabla, nombreCampo, tipoRegla);

                if (reglaExiste && ViewState["ReglaId"] == null)
                {
                    MostrarMensaje("Ya existe una regla de este tipo para el campo seleccionado.", false);
                    return;
                }

                if (ViewState["ReglaId"] != null) // Editar regla existente
                {
                    int reglaId = Convert.ToInt32(ViewState["ReglaId"]);
                    _validacionService.ActualizarReglaValidacion(reglaId, nombreTabla, nombreCampo, tipoRegla, parametros, mensajeError, usuarioId);
                    MostrarMensaje("Regla de validación actualizada exitosamente.", true);
                }
                else // Nueva regla
                {
                    _validacionService.CrearReglaValidacion(nombreTabla, nombreCampo, tipoRegla, parametros, mensajeError, usuarioId);
                    MostrarMensaje("Regla de validación creada exitosamente.", true);
                }

                // Limpiar formulario y recargar datos
                LimpiarFormulario();
                CargarReglas(nombreTabla);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        private string ObtenerParametrosRegla(string tipoRegla)
        {
            switch (tipoRegla)
            {
                case "Requerido":
                    return string.Empty; // No requiere parámetros

                case "Rango":
                case "RangoFecha":
                    return $"{txtValorMinimo.Text.Trim()}|{txtValorMaximo.Text.Trim()}";

                case "MayorQue":
                case "MenorQue":
                case "FechaPosterior":
                case "FechaAnterior":
                case "LongitudMaxima":
                case "LongitudMinima":
                    return txtValorUnico.Text.Trim();

                case "RegEx":
                    return txtExpresion.Text.Trim();

                case "ListaValores":
                    return txtListaValores.Text.Trim(); // Valores separados por comas

                default:
                    return string.Empty;
            }
        }

        protected void gvReglas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reglaId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Cargar datos de la regla para edición
                DataTable reglaData = _validacionService.ObtenerReglaValidacionPorId(reglaId);

                if (reglaData.Rows.Count > 0)
                {
                    DataRow row = reglaData.Rows[0];

                    // Establecer valores en el formulario
                    string tabla = row["Tabla"].ToString();
                    string campo = row["Campo"].ToString();
                    string tipoRegla = row["TipoRegla"].ToString();
                    string parametros = row["Parametros"].ToString();

                    // Seleccionar tabla y campo
                    ddlTablas.SelectedValue = tabla;
                    CargarCamposTabla(tabla);
                    ddlCampo.SelectedValue = campo;

                    // Cargar tipo de dato y configurar controles
                    string tipoDato = _validacionService.ObtenerTipoDatoCampo(tabla, campo);
                    ConfigurarControlesPorTipoDato(tipoDato);

                    // Seleccionar tipo de regla
                    ddlTipoRegla.SelectedValue = tipoRegla;
                    ddlTipoRegla_SelectedIndexChanged(null, null); // Disparar evento para mostrar paneles adecuados

                    // Establecer parámetros según el tipo de regla
                    EstablecerParametrosRegla(tipoRegla, parametros);

                    // Establecer mensaje de error
                    txtMensajeError.Text = row["MensajeError"].ToString();

                    // Guardar ID de regla para actualización
                    ViewState["ReglaId"] = reglaId;

                    // Cambiar texto del botón
                    btnGuardar.Text = "Actualizar";
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    // Eliminar regla
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                    _validacionService.EliminarReglaValidacion(reglaId, usuarioId);

                    MostrarMensaje("Regla de validación eliminada exitosamente.", true);

                    // Recargar datos
                    CargarReglas(ddlTablas.SelectedValue);
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        private void EstablecerParametrosRegla(string tipoRegla, string parametros)
        {
            if (string.IsNullOrEmpty(parametros))
                return;

            switch (tipoRegla)
            {
                case "Rango":
                case "RangoFecha":
                    string[] rangoParams = parametros.Split('|');
                    if (rangoParams.Length == 2)
                    {
                        txtValorMinimo.Text = rangoParams[0];
                        txtValorMaximo.Text = rangoParams[1];
                    }
                    break;

                case "MayorQue":
                case "MenorQue":
                case "FechaPosterior":
                case "FechaAnterior":
                case "LongitudMaxima":
                case "LongitudMinima":
                    txtValorUnico.Text = parametros;
                    break;

                case "RegEx":
                    txtExpresion.Text = parametros;
                    break;

                case "ListaValores":
                    txtListaValores.Text = parametros;
                    break;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            // No limpiar tabla seleccionada

            // Limpiar selección de campo
            if (ddlCampo.Items.Count > 0)
                ddlCampo.SelectedValue = "0";

            // Ocultar y limpiar paneles de tipo regla
            ResetearControlesValidacion();

            // Limpiar mensaje de error
            txtMensajeError.Text = string.Empty;

            // Limpiar ID de regla y restablecer botón
            ViewState["ReglaId"] = null;
            btnGuardar.Text = "Guardar";
        }

        private void ResetearControlesValidacion()
        {
            // Ocultar paneles
            pnlTipoRegla.Visible = false;
            pnlValorUnico.Visible = false;
            pnlRango.Visible = false;
            pnlRegex.Visible = false;
            pnlListaValores.Visible = false;

            // Limpiar controles
            if (ddlTipoRegla.Items.Count > 0)
                ddlTipoRegla.SelectedValue = "0";

            txtValorUnico.Text = string.Empty;
            txtValorMinimo.Text = string.Empty;
            txtValorMaximo.Text = string.Empty;
            txtExpresion.Text = string.Empty;
            txtListaValores.Text = string.Empty;
            txtMensajeError.Text = string.Empty;
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }
    }

    // Clase de servicio para gestión de reglas de validación
    public class ValidacionService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public ValidacionService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerTablas()
        {
            string sql = @"
                SELECT TABLE_NAME AS NombreTabla
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_TYPE = 'BASE TABLE'
                AND TABLE_CATALOG = DB_NAME()
                ORDER BY TABLE_NAME";

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            return _dbConnection.ExecuteQuery(sql, parameters);
        }

        public DataTable ObtenerCamposTabla(string nombreTabla)
        {
            string sql = @"
                SELECT COLUMN_NAME AS NombreCampo, DATA_TYPE AS TipoDato
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @NombreTabla
                ORDER BY ORDINAL_POSITION";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NombreTabla", nombreTabla }
            };

            return _dbConnection.ExecuteQuery(sql, parameters);
        }

        public string ObtenerTipoDatoCampo(string nombreTabla, string nombreCampo)
        {
            string sql = @"
                SELECT DATA_TYPE AS TipoDato
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @NombreTabla
                AND COLUMN_NAME = @NombreCampo";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NombreTabla", nombreTabla },
                { "@NombreCampo", nombreCampo }
            };

            var resultado = _dbConnection.ExecuteQuery(sql, parameters);

            if (resultado.Rows.Count > 0)
            {
                return resultado.Rows[0]["TipoDato"].ToString();
            }

            return string.Empty;
        }

        public DataTable ObtenerReglasValidacion(string nombreTabla)
        {
            return _queryBuilder
                .Select("rv.ReglaId, rv.Tabla, rv.Campo, rv.TipoRegla, rv.Parametros, " +
                        "rv.MensajeError, rv.FechaCreacion, u.NombreCompleto AS Usuario")
                .From("ReglasValidacion AS rv")
                .Join("Usuarios AS u", "rv.UsuarioCreacion = u.UsuarioId")
                .Where(true, "rv.Tabla", "=", nombreTabla)
                .OrderBy("rv.Campo, rv.TipoRegla")
                .Execute();
        }

        public DataTable ObtenerReglaValidacionPorId(int reglaId)
        {
            return _queryBuilder
                .Select("ReglaId, Tabla, Campo, TipoRegla, Parametros, MensajeError, " +
                        "FechaCreacion, UsuarioCreacion")
                .From("ReglasValidacion")
                .Where(true, "ReglaId", "=", reglaId)
                .Execute();
        }

        public bool ExisteReglaValidacion(string tabla, string campo, string tipoRegla)
        {
            var resultado = _queryBuilder
                .Select("COUNT(*)")
                .From("ReglasValidacion")
                .Where(true, "Tabla", "=", tabla)
                .AndWhere(true, "Campo", "=", campo)
                .AndWhere(true, "TipoRegla", "=", tipoRegla)
                .Execute();

            return Convert.ToInt32(resultado.Rows[0][0]) > 0;
        }

        public void CrearReglaValidacion(string tabla, string campo, string tipoRegla, string parametros, string mensajeError, int usuarioId)
        {
            string sql = @"
                INSERT INTO ReglasValidacion (
                    Tabla,
                    Campo,
                    TipoRegla,
                    Parametros,
                    MensajeError,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @Tabla,
                    @Campo,
                    @TipoRegla,
                    @Parametros,
                    @MensajeError,
                    @FechaCreacion,
                    @UsuarioCreacion
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@Tabla", tabla },
                { "@Campo", campo },
                { "@TipoRegla", tipoRegla },
                { "@Parametros", parametros },
                { "@MensajeError", mensajeError },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "ReglasValidacion", $"Regla {tipoRegla} para {tabla}.{campo}");
        }

        public void ActualizarReglaValidacion(int reglaId, string tabla, string campo, string tipoRegla, string parametros, string mensajeError, int usuarioId)
        {
            string sql = @"
                UPDATE ReglasValidacion SET
                    Tabla = @Tabla,
                    Campo = @Campo,
                    TipoRegla = @TipoRegla,
                    Parametros = @Parametros,
                    MensajeError = @MensajeError,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE ReglaId = @ReglaId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ReglaId", reglaId },
                { "@Tabla", tabla },
                { "@Campo", campo },
                { "@TipoRegla", tipoRegla },
                { "@Parametros", parametros },
                { "@MensajeError", mensajeError },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Actualizar", "ReglasValidacion", $"Regla {tipoRegla} para {tabla}.{campo}");
        }

        public void EliminarReglaValidacion(int reglaId, int usuarioId)
        {
            // Obtener información de la regla para el historial
            var regla = ObtenerReglaValidacionPorId(reglaId);
            string tabla = regla.Rows[0]["Tabla"].ToString();
            string campo = regla.Rows[0]["Campo"].ToString();
            string tipoRegla = regla.Rows[0]["TipoRegla"].ToString();

            string sql = "DELETE FROM ReglasValidacion WHERE ReglaId = @ReglaId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ReglaId", reglaId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "ReglasValidacion", $"Regla {tipoRegla} para {tabla}.{campo}");
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