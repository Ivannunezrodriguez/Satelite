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
    public partial class AltaVariedades : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private VariedadService _variedadService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _variedadService = new VariedadService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Inicializar controles
                CargarCultivos();
                CargarVariedades();
            }
        }

        protected void CargarCultivos()
        {
            DataTable cultivos = _variedadService.ObtenerCultivos();

            ddlCultivos.DataSource = cultivos;
            ddlCultivos.DataTextField = "Nombre";
            ddlCultivos.DataValueField = "CultivoId";
            ddlCultivos.DataBind();

            // Agregar opción por defecto
            ddlCultivos.Items.Insert(0, new ListItem("-- Seleccione un cultivo --", "0"));
        }

        protected void CargarVariedades()
        {
            // Cargar todas las variedades o filtrar por cultivo si está seleccionado
            int cultivoId = 0;
            if (ddlCultivos.SelectedValue != "0")
            {
                cultivoId = Convert.ToInt32(ddlCultivos.SelectedValue);
            }

            DataTable variedades = _variedadService.ObtenerVariedades(cultivoId);

            gvVariedades.DataSource = variedades;
            gvVariedades.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos
            if (ddlCultivos.SelectedValue == "0")
            {
                lblMensaje.Text = "Por favor seleccione un cultivo.";
                lblMensaje.CssClass = "text-danger";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                lblMensaje.Text = "Por favor complete todos los campos requeridos.";
                lblMensaje.CssClass = "text-danger";
                return;
            }

            try
            {
                // Obtener datos del formulario
                int cultivoId = Convert.ToInt32(ddlCultivos.SelectedValue);
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                string ciclo = txtCiclo.Text.Trim();
                string caracteristicas = txtCaracteristicas.Text.Trim();
                bool activo = chkActivo.Checked;
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Guardar la variedad
                if (ViewState["VariedadId"] != null) // Editar
                {
                    int variedadId = Convert.ToInt32(ViewState["VariedadId"]);
                    _variedadService.ActualizarVariedad(variedadId, cultivoId, nombre, descripcion, ciclo, caracteristicas, activo, usuarioId);
                    lblMensaje.Text = "Variedad actualizada exitosamente.";
                }
                else // Nueva variedad
                {
                    _variedadService.CrearVariedad(cultivoId, nombre, descripcion, ciclo, caracteristicas, activo, usuarioId);
                    lblMensaje.Text = "Variedad creada exitosamente.";
                }

                lblMensaje.CssClass = "text-success";

                // Limpiar formulario y recargar datos
                LimpiarFormulario();
                CargarVariedades();
            }
            catch (Exception ex)
            {
                // Registrar error
                // LogError(ex);

                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        protected void ddlCultivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recargar variedades cuando cambia el cultivo seleccionado
            CargarVariedades();
        }

        protected void gvVariedades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int variedadId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Cargar datos de la variedad para edición
                DataTable variedadData = _variedadService.ObtenerVariedadPorId(variedadId);

                if (variedadData.Rows.Count > 0)
                {
                    DataRow row = variedadData.Rows[0];

                    // Establecer valores en el formulario
                    ddlCultivos.SelectedValue = row["CultivoId"].ToString();
                    txtNombre.Text = row["Nombre"].ToString();
                    txtDescripcion.Text = row["Descripcion"].ToString();
                    txtCiclo.Text = row["Ciclo"].ToString();
                    txtCaracteristicas.Text = row["Caracteristicas"].ToString();
                    chkActivo.Checked = Convert.ToBoolean(row["Activo"]);

                    // Guardar ID de variedad para actualización
                    ViewState["VariedadId"] = variedadId;

                    // Cambiar texto del botón
                    btnGuardar.Text = "Actualizar";
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    // Eliminar variedad (baja lógica)
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                    _variedadService.EliminarVariedad(variedadId, usuarioId);

                    lblMensaje.Text = "Variedad eliminada exitosamente.";
                    lblMensaje.CssClass = "text-success";

                    // Recargar datos
                    CargarVariedades();
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar: " + ex.Message;
                    lblMensaje.CssClass = "text-danger";
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCiclo.Text = string.Empty;
            txtCaracteristicas.Text = string.Empty;
            chkActivo.Checked = true;
            ViewState["VariedadId"] = null;
            btnGuardar.Text = "Guardar";
        }
    }

    // Clase de servicio para gestión de variedades
    public class VariedadService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public VariedadService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerCultivos()
        {
            return _queryBuilder
                .Select("CultivoId, Nombre, Descripcion")
                .From("Cultivos")
                .Where(true, "Activo", "=", true)
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerVariedades(int cultivoId = 0)
        {
            var query = _queryBuilder
                .Select("v.VariedadId, v.CultivoId, c.Nombre AS Cultivo, v.Nombre, " +
                        "v.Descripcion, v.Ciclo, v.Caracteristicas, v.Activo, " +
                        "v.FechaCreacion, u.NombreCompleto AS Usuario")
                .From("Variedades AS v")
                .Join("Cultivos AS c", "v.CultivoId = c.CultivoId")
                .Join("Usuarios AS u", "v.UsuarioCreacion = u.UsuarioId");

            // Si se especifica un cultivo, filtrar por él
            if (cultivoId > 0)
            {
                query.Where(true, "v.CultivoId", "=", cultivoId);
            }

            return query.OrderBy("c.Nombre, v.Nombre").Execute();
        }

        public DataTable ObtenerVariedadPorId(int variedadId)
        {
            return _queryBuilder
                .Select("VariedadId, CultivoId, Nombre, Descripcion, Ciclo, " +
                        "Caracteristicas, Activo, FechaCreacion, UsuarioCreacion")
                .From("Variedades")
                .Where(true, "VariedadId", "=", variedadId)
                .Execute();
        }

        public void CrearVariedad(
            int cultivoId,
            string nombre,
            string descripcion,
            string ciclo,
            string caracteristicas,
            bool activo,
            int usuarioId)
        {
            // Verificar que no exista una variedad con el mismo nombre para el mismo cultivo
            var variedadExistente = _queryBuilder
                .Select("COUNT(*)")
                .From("Variedades")
                .Where(true, "CultivoId", "=", cultivoId)
                .AndWhere(true, "Nombre", "=", nombre)
                .Execute();

            if (Convert.ToInt32(variedadExistente.Rows[0][0]) > 0)
            {
                throw new Exception("Ya existe una variedad con este nombre para el cultivo seleccionado.");
            }

            string sql = @"
                INSERT INTO Variedades (
                    CultivoId,
                    Nombre,
                    Descripcion,
                    Ciclo,
                    Caracteristicas,
                    Activo,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @CultivoId,
                    @Nombre,
                    @Descripcion,
                    @Ciclo,
                    @Caracteristicas,
                    @Activo,
                    @FechaCreacion,
                    @UsuarioCreacion
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@CultivoId", cultivoId },
                { "@Nombre", nombre },
                { "@Descripcion", descripcion },
                { "@Ciclo", ciclo },
                { "@Caracteristicas", caracteristicas },
                { "@Activo", activo },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "Variedades", nombre);
        }

        public void ActualizarVariedad(
            int variedadId,
            int cultivoId,
            string nombre,
            string descripcion,
            string ciclo,
            string caracteristicas,
            bool activo,
            int usuarioId)
        {
            // Verificar que no exista otra variedad con el mismo nombre para el mismo cultivo
            var variedadExistente = _queryBuilder
                .Select("COUNT(*)")
                .From("Variedades")
                .Where(true, "CultivoId", "=", cultivoId)
                .AndWhere(true, "Nombre", "=", nombre)
                .AndWhere(true, "VariedadId", "!=", variedadId)
                .Execute();

            if (Convert.ToInt32(variedadExistente.Rows[0][0]) > 0)
            {
                throw new Exception("Ya existe otra variedad con este nombre para el cultivo seleccionado.");
            }

            string sql = @"
                UPDATE Variedades SET
                    CultivoId = @CultivoId,
                    Nombre = @Nombre,
                    Descripcion = @Descripcion,
                    Ciclo = @Ciclo,
                    Caracteristicas = @Caracteristicas,
                    Activo = @Activo,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE VariedadId = @VariedadId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@VariedadId", variedadId },
                { "@CultivoId", cultivoId },
                { "@Nombre", nombre },
                { "@Descripcion", descripcion },
                { "@Ciclo", ciclo },
                { "@Caracteristicas", caracteristicas },
                { "@Activo", activo },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Actualizar", "Variedades", nombre);
        }

        public void EliminarVariedad(int variedadId, int usuarioId)
        {
            // Obtener nombre de variedad para el historial
            var variedad = ObtenerVariedadPorId(variedadId);
            string nombreVariedad = variedad.Rows[0]["Nombre"].ToString();

            // Implementar baja lógica en lugar de eliminar físicamente
            string sql = @"
                UPDATE Variedades SET
                    Activo = 0,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE VariedadId = @VariedadId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@VariedadId", variedadId },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "Variedades", nombreVariedad);
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