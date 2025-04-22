using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Satelite
{
    public partial class AltaArchivos : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private ArchivoService _archivoService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y el constructor de consultas
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _archivoService = new ArchivoService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Cargar datos iniciales
                CargarTiposArchivo();
                CargarRangos();

                // Si hay un ID de rango en QueryString, preseleccionar en el DropDownList
                if (!string.IsNullOrEmpty(Request.QueryString["rangoId"]))
                {
                    string rangoId = Request.QueryString["rangoId"];
                    ddlRangos.SelectedValue = rangoId;
                }
            }
        }

        protected void CargarTiposArchivo()
        {
            DataTable tiposArchivo = _archivoService.ObtenerTiposArchivo();

            ddlTipoArchivo.DataSource = tiposArchivo;
            ddlTipoArchivo.DataTextField = "Nombre";
            ddlTipoArchivo.DataValueField = "TipoArchivoId";
            ddlTipoArchivo.DataBind();

            // Agregar opción por defecto
            ddlTipoArchivo.Items.Insert(0, new ListItem("-- Seleccione un tipo --", "0"));
        }

        protected void CargarRangos()
        {
            DataTable rangos = _archivoService.ObtenerRangosActivos();

            ddlRangos.DataSource = rangos;
            ddlRangos.DataTextField = "RangoInfo"; // Un campo concatenado con año y código
            ddlRangos.DataValueField = "RangoId";
            ddlRangos.DataBind();

            // Agregar opción por defecto
            ddlRangos.Items.Insert(0, new ListItem("-- Seleccione un rango --", "0"));
        }

        protected void btnSubirArchivo_Click(object sender, EventArgs e)
        {
            // Validar que se haya seleccionado un archivo
            if (fileUpload.HasFile == false)
            {
                lblMensaje.Text = "Por favor seleccione un archivo para subir.";
                lblMensaje.CssClass = "text-danger";
                return;
            }

            // Validar que se haya seleccionado un tipo de archivo y un rango
            if (ddlTipoArchivo.SelectedValue == "0" || ddlRangos.SelectedValue == "0")
            {
                lblMensaje.Text = "Por favor seleccione un tipo de archivo y un rango.";
                lblMensaje.CssClass = "text-danger";
                return;
            }

            try
            {
                // Obtener datos del archivo
                string fileName = Path.GetFileName(fileUpload.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Validar la extensión del archivo
                if (!_archivoService.EsExtensionPermitida(fileExtension))
                {
                    lblMensaje.Text = "Tipo de archivo no permitido. Use .pdf, .doc, .docx, .xls, .xlsx, .jpg, .png";
                    lblMensaje.CssClass = "text-danger";
                    return;
                }

                // Generar nombre único para el archivo
                string nuevoNombre = Guid.NewGuid().ToString() + fileExtension;

                // Ruta donde se guardará el archivo
                string rutaArchivos = Server.MapPath("~/Archivos/");

                // Verificar si existe el directorio y si no, crearlo
                if (!Directory.Exists(rutaArchivos))
                {
                    Directory.CreateDirectory(rutaArchivos);
                }

                // Guardar el archivo físicamente
                string rutaCompleta = Path.Combine(rutaArchivos, nuevoNombre);
                fileUpload.SaveAs(rutaCompleta);

                // Obtener el usuario actual
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Extraer información del rango seleccionado (formato: "año-código")
                string[] rangoInfo = ddlRangos.SelectedItem.Text.Split('-');
                int ranAnio = Convert.ToInt32(rangoInfo[0].Trim());
                string ranCodigo = rangoInfo[1].Trim();

                // Registrar el archivo en la base de datos
                int tipoArchivoId = Convert.ToInt32(ddlTipoArchivo.SelectedValue);
                string descripcion = txtDescripcion.Text.Trim();

                _archivoService.RegistrarArchivo(
                    fileName,
                    nuevoNombre,
                    fileExtension,
                    rutaCompleta,
                    descripcion,
                    tipoArchivoId,
                    ranAnio,
                    ranCodigo,
                    usuarioId);

                // Mostrar mensaje de éxito
                lblMensaje.Text = "Archivo subido exitosamente.";
                lblMensaje.CssClass = "text-success";

                // Limpiar campos del formulario
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                // Registrar el error
                // LogError(ex);

                // Mostrar mensaje de error
                lblMensaje.Text = "Error al subir el archivo: " + ex.Message;
                lblMensaje.CssClass = "text-danger";
            }
        }

        private void LimpiarFormulario()
        {
            ddlTipoArchivo.SelectedValue = "0";
            txtDescripcion.Text = string.Empty;
            // No limpiar el rango si vino preseleccionado por QueryString
            if (string.IsNullOrEmpty(Request.QueryString["rangoId"]))
            {
                ddlRangos.SelectedValue = "0";
            }
        }
    }

    // Clase de servicio para gestión de archivos
    public class ArchivoService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private readonly string[] _extensionesPermitidas = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".png" };

        public ArchivoService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerTiposArchivo()
        {
            return _queryBuilder
                .Select("TipoArchivoId, Nombre, Descripcion")
                .From("TiposArchivo")
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerRangosActivos()
        {
            return _queryBuilder
                .Select("CAST(RanAnio AS VARCHAR) + '-' + RanCodigo AS RangoInfo, " +
                        "CAST(RanAnio AS VARCHAR) + '-' + RanCodigo AS RangoId")
                .From("Rangos")
                .Where(true, "Estado", "=", "Activo")
                .OrderBy("RanAnio DESC, RanCodigo")
                .Execute();
        }

        public bool EsExtensionPermitida(string extension)
        {
            return _extensionesPermitidas.Contains(extension.ToLower());
        }

        public void RegistrarArchivo(
            string nombreOriginal,
            string nombreUnico,
            string extension,
            string rutaCompleta,
            string descripcion,
            int tipoArchivoId,
            int ranAnio,
            string ranCodigo,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO Archivos (
                    NombreOriginal, 
                    NombreUnico, 
                    Extension, 
                    Ruta, 
                    Descripcion, 
                    TipoArchivoId, 
                    RanAnio, 
                    RanCodigo, 
                    FechaSubida, 
                    UsuarioId
                ) VALUES (
                    @NombreOriginal, 
                    @NombreUnico, 
                    @Extension, 
                    @Ruta, 
                    @Descripcion, 
                    @TipoArchivoId, 
                    @RanAnio, 
                    @RanCodigo, 
                    @FechaSubida, 
                    @UsuarioId
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NombreOriginal", nombreOriginal },
                { "@NombreUnico", nombreUnico },
                { "@Extension", extension },
                { "@Ruta", rutaCompleta },
                { "@Descripcion", descripcion },
                { "@TipoArchivoId", tipoArchivoId },
                { "@RanAnio", ranAnio },
                { "@RanCodigo", ranCodigo },
                { "@FechaSubida", DateTime.Now },
                { "@UsuarioId", usuarioId }
            };

            // Registrar en la base de datos
            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar la acción en el historial
            RegistrarAccionHistorial(usuarioId, "Subir", "Archivos", nombreOriginal);
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