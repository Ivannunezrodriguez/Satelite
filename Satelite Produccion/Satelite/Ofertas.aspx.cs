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
    public partial class AltaCampos : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private CampoService _campoService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _campoService = new CampoService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Inicializar controles
                CargarProvincias();
                CargarProductores();
                CargarCampos();
            }
        }

        protected void CargarProvincias()
        {
            DataTable provincias = _campoService.ObtenerProvincias();

            ddlProvincia.DataSource = provincias;
            ddlProvincia.DataTextField = "Nombre";
            ddlProvincia.DataValueField = "ProvinciaId";
            ddlProvincia.DataBind();

            // Agregar opción por defecto
            ddlProvincia.Items.Insert(0, new ListItem("-- Seleccione una provincia --", "0"));
        }

        protected void CargarMunicipios(int provinciaId)
        {
            DataTable municipios = _campoService.ObtenerMunicipiosPorProvincia(provinciaId);

            ddlMunicipio.DataSource = municipios;
            ddlMunicipio.DataTextField = "Nombre";
            ddlMunicipio.DataValueField = "MunicipioId";
            ddlMunicipio.DataBind();

            // Agregar opción por defecto
            ddlMunicipio.Items.Insert(0, new ListItem("-- Seleccione un municipio --", "0"));
        }

        protected void CargarProductores()
        {
            DataTable productores = _campoService.ObtenerProductores();

            ddlProductor.DataSource = productores;
            ddlProductor.DataTextField = "NombreCompleto";
            ddlProductor.DataValueField = "ProductorId";
            ddlProductor.DataBind();

            // Agregar opción por defecto
            ddlProductor.Items.Insert(0, new ListItem("-- Seleccione un productor --", "0"));
        }

        protected void CargarCampos()
        {
            DataTable campos = _campoService.ObtenerCampos();

            gvCampos.DataSource = campos;
            gvCampos.DataBind();
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue != "0")
            {
                int provinciaId = Convert.ToInt32(ddlProvincia.SelectedValue);
                CargarMunicipios(provinciaId);
            }
            else
            {
                ddlMunicipio.Items.Clear();
                ddlMunicipio.Items.Insert(0, new ListItem("-- Seleccione un municipio --", "0"));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos
            if (ddlProductor.SelectedValue == "0" || ddlProvincia.SelectedValue == "0" || ddlMunicipio.SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtSuperficie.Text))
            {
                lblMensaje.Text = "Por favor complete todos los campos requeridos.";
                lblMensaje.CssClass = "text-danger";
                return;
            }

            try
            {
                // Obtener datos del formulario
                int productorId = Convert.ToInt32(ddlProductor.SelectedValue);
                int municipioId = Convert.ToInt32(ddlMunicipio.SelectedValue);
                string nombre = txtNombre.Text.Trim();
                decimal superficie = Convert.ToDecimal(txtSuperficie.Text.Trim());
                string referenciaCatastral = txtReferencia.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string coordenadas = txtCoordenadas.Text.Trim();
                bool activo = chkActivo.Checked;
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Guardar el campo
                if (ViewState["CampoId"] != null) // Editar
                {
                    int campoId = Convert.ToInt32(ViewState["CampoId"]);
                    _campoService.ActualizarCampo(campoId, productorId, municipioId, nombre, superficie,
                        referenciaCatastral, direccion, coordenadas, activo, usuarioId);
                    lblMensaje.Text = "Campo actualizado exitosamente.";
                }
                else // Nuevo campo
                {
                    _campoService.CrearCampo(productorId, municipioId, nombre, superficie,
                        referenciaCatastral, direccion, coordenadas, activo, usuarioId);
                    lblMensaje.Text = "Campo creado exitosamente.";
                }

                lblMensaje.CssClass = "text-success";

                // Limpiar formulario y recargar datos
                LimpiarFormulario();
                CargarCampos();
            }
            catch (Exception ex)
            {
                // Registrar error
                // LogError(ex);

                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        protected void gvCampos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int campoId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Cargar datos del campo para edición
                DataTable campoData = _campoService.ObtenerCampoPorId(campoId);

                if (campoData.Rows.Count > 0)
                {
                    DataRow row = campoData.Rows[0];

                    // Establecer valores en el formulario
                    ddlProductor.SelectedValue = row["ProductorId"].ToString();

                    // Cargar provincia y municipio
                    int municipioId = Convert.ToInt32(row["MunicipioId"]);
                    int provinciaId = _campoService.ObtenerProvinciaIdPorMunicipio(municipioId);

                    ddlProvincia.SelectedValue = provinciaId.ToString();
                    CargarMunicipios(provinciaId);
                    ddlMunicipio.SelectedValue = municipioId.ToString();

                    txtNombre.Text = row["Nombre"].ToString();
                    txtSuperficie.Text = row["Superficie"].ToString();
                    txtReferencia.Text = row["ReferenciaCatastral"].ToString();
                    txtDireccion.Text = row["Direccion"].ToString();
                    txtCoordenadas.Text = row["Coordenadas"].ToString();
                    chkActivo.Checked = Convert.ToBoolean(row["Activo"]);

                    // Guardar ID de campo para actualización
                    ViewState["CampoId"] = campoId;

                    // Cambiar texto del botón
                    btnGuardar.Text = "Actualizar";
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    // Eliminar campo (baja lógica)
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                    _campoService.EliminarCampo(campoId, usuarioId);

                    lblMensaje.Text = "Campo eliminado exitosamente.";
                    lblMensaje.CssClass = "text-success";

                    // Recargar datos
                    CargarCampos();
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
            ddlProductor.SelectedValue = "0";
            ddlProvincia.SelectedValue = "0";
            ddlMunicipio.Items.Clear();
            ddlMunicipio.Items.Insert(0, new ListItem("-- Seleccione un municipio --", "0"));
            txtNombre.Text = string.Empty;
            txtSuperficie.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtCoordenadas.Text = string.Empty;
            chkActivo.Checked = true;
            ViewState["CampoId"] = null;
            btnGuardar.Text = "Guardar";
        }
    }

    // Clase de servicio para gestión de campos
    public class CampoService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public CampoService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerProvincias()
        {
            return _queryBuilder
                .Select("ProvinciaId, Nombre")
                .From("Provincias")
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerMunicipiosPorProvincia(int provinciaId)
        {
            return _queryBuilder
                .Select("MunicipioId, Nombre")
                .From("Municipios")
                .Where(true, "ProvinciaId", "=", provinciaId)
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerProductores()
        {
            return _queryBuilder
                .Select("ProductorId, NombreCompleto")
                .From("Productores")
                .Where(true, "Activo", "=", true)
                .OrderBy("NombreCompleto")
                .Execute();
        }

        public DataTable ObtenerCampos()
        {
            return _queryBuilder
                .Select("c.CampoId, c.Nombre, p.NombreCompleto AS Productor, " +
                        "m.Nombre AS Municipio, pr.Nombre AS Provincia, " +
                        "c.Superficie, c.ReferenciaCatastral, c.Activo")
                .From("Campos AS c")
                .Join("Productores AS p", "c.ProductorId = p.ProductorId")
                .Join("Municipios AS m", "c.MunicipioId = m.MunicipioId")
                .Join("Provincias AS pr", "m.ProvinciaId = pr.ProvinciaId")
                .OrderBy("p.NombreCompleto, c.Nombre")
                .Execute();
        }

        public DataTable ObtenerCampoPorId(int campoId)
        {
            return _queryBuilder
                .Select("CampoId, ProductorId, MunicipioId, Nombre, Superficie, " +
                        "ReferenciaCatastral, Direccion, Coordenadas, Activo")
                .From("Campos")
                .Where(true, "CampoId", "=", campoId)
                .Execute();
        }

        public int ObtenerProvinciaIdPorMunicipio(int municipioId)
        {
            var resultado = _queryBuilder
                .Select("ProvinciaId")
                .From("Municipios")
                .Where(true, "MunicipioId", "=", municipioId)
                .Execute();

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToInt32(resultado.Rows[0]["ProvinciaId"]);
            }

            return 0;
        }

        public void CrearCampo(
            int productorId,
            int municipioId,
            string nombre,
            decimal superficie,
            string referenciaCatastral,
            string direccion,
            string coordenadas,
            bool activo,
            int usuarioId)
        {
            // Verificar que no exista un campo con el mismo nombre para el mismo productor
            var campoExistente = _queryBuilder
                .Select("COUNT(*)")
                .From("Campos")
                .Where(true, "ProductorId", "=", productorId)
                .AndWhere(true, "Nombre", "=", nombre)
                .Execute();

            if (Convert.ToInt32(campoExistente.Rows[0][0]) > 0)
            {
                throw new Exception("Ya existe un campo con este nombre para el productor seleccionado.");
            }

            string sql = @"
                INSERT INTO Campos (
                    ProductorId,
                    MunicipioId,
                    Nombre,
                    Superficie,
                    ReferenciaCatastral,
                    Direccion,
                    Coordenadas,
                    Activo,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @ProductorId,
                    @MunicipioId,
                    @Nombre,
                    @Superficie,
                    @ReferenciaCatastral,
                    @Direccion,
                    @Coordenadas,
                    @Activo,
                    @FechaCreacion,
                    @UsuarioCreacion
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ProductorId", productorId },
                { "@MunicipioId", municipioId },
                { "@Nombre", nombre },
                { "@Superficie", superficie },
                { "@ReferenciaCatastral", referenciaCatastral },
                { "@Direccion", direccion },
                { "@Coordenadas", coordenadas },
                { "@Activo", activo },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "Campos", nombre);
        }

        public void ActualizarCampo(
            int campoId,
            int productorId,
            int municipioId,
            string nombre,
            decimal superficie,
            string referenciaCatastral,
            string direccion,
            string coordenadas,
            bool activo,
            int usuarioId)
        {
            // Verificar que no exista otro campo con el mismo nombre para el mismo productor
            var campoExistente = _queryBuilder
                .Select("COUNT(*)")
                .From("Campos")
                .Where(true, "ProductorId", "=", productorId)
                .AndWhere(true, "Nombre", "=", nombre)
                .AndWhere(true, "CampoId", "!=", campoId)
                .Execute();

            if (Convert.ToInt32(campoExistente.Rows[0][0]) > 0)
            {
                throw new Exception("Ya existe otro campo con este nombre para el productor seleccionado.");
            }

            string sql = @"
                UPDATE Campos SET
                    ProductorId = @ProductorId,
                    MunicipioId = @MunicipioId,
                    Nombre = @Nombre,
                    Superficie = @Superficie,
                    ReferenciaCatastral = @ReferenciaCatastral,
                    Direccion = @Direccion,
                    Coordenadas = @Coordenadas,
                    Activo = @Activo,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE CampoId = @CampoId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@CampoId", campoId },
                { "@ProductorId", productorId },
                { "@MunicipioId", municipioId },
                { "@Nombre", nombre },
                { "@Superficie", superficie },
                { "@ReferenciaCatastral", referenciaCatastral },
                { "@Direccion", direccion },
                { "@Coordenadas", coordenadas },
                { "@Activo", activo },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Actualizar", "Campos", nombre);
        }

        public void EliminarCampo(int campoId, int usuarioId)
        {
            // Obtener nombre de campo para el historial
            var campo = ObtenerCampoPorId(campoId);
            string nombreCampo = campo.Rows[0]["Nombre"].ToString();

            // Verificar si hay parcelas asociadas al campo
            var parcelasAsociadas = _queryBuilder
                .Select("COUNT(*)")
                .From("Parcelas")
                .Where(true, "CampoId", "=", campoId)
                .Execute();

            if (Convert.ToInt32(parcelasAsociadas.Rows[0][0]) > 0)
            {
                throw new Exception("No se puede eliminar el campo porque tiene parcelas asociadas.");
            }

            // Implementar baja lógica en lugar de eliminar físicamente
            string sql = @"
                UPDATE Campos SET
                    Activo = 0,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE CampoId = @CampoId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@CampoId", campoId },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "Campos", nombreCampo);
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