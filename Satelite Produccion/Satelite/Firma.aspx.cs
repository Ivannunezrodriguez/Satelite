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
using System.Security.Cryptography;
using System.Text;

namespace Satelite
{
    public partial class Firma : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private FirmaService _firmaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _firmaService = new FirmaService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Verificar si hay un documento pendiente de firma en la URL
                if (!string.IsNullOrEmpty(Request.QueryString["docId"]))
                {
                    int documentoId = Convert.ToInt32(Request.QueryString["docId"]);
                    CargarDocumentoPendiente(documentoId);
                }
                else
                {
                    // Cargar documentos pendientes de firma del usuario actual
                    CargarDocumentosPendientes();
                }
            }
        }

        protected void CargarDocumentosPendientes()
        {
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            DataTable documentosPendientes = _firmaService.ObtenerDocumentosPendientesFirma(usuarioId);

            gvDocumentosPendientes.DataSource = documentosPendientes;
            gvDocumentosPendientes.DataBind();

            // Mostrar mensaje si no hay documentos pendientes
            lblSinDocumentos.Visible = (documentosPendientes.Rows.Count == 0);

            // Ocultar el panel de firma si no hay documentos seleccionados
            pnlFirmaDocumento.Visible = false;
        }

        protected void CargarDocumentoPendiente(int documentoId)
        {
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            // Verificar que el usuario tenga permiso para firmar este documento
            if (!_firmaService.VerificarPermisosDocumento(documentoId, usuarioId))
            {
                MostrarMensaje("No tiene permisos para firmar este documento.", false);
                CargarDocumentosPendientes();
                return;
            }

            // Cargar datos del documento
            DataTable datosDocumento = _firmaService.ObtenerDocumentoPorId(documentoId);

            if (datosDocumento.Rows.Count > 0)
            {
                DataRow row = datosDocumento.Rows[0];

                // Establecer los campos en el formulario
                lblTipoDocumento.Text = row["TipoDocumento"].ToString();
                lblNombreDocumento.Text = row["NombreDocumento"].ToString();
                lblDescripcion.Text = row["Descripcion"].ToString();

                string fechaCreacion = Convert.ToDateTime(row["FechaCreacion"]).ToString("dd/MM/yyyy HH:mm");
                lblFechaCreacion.Text = fechaCreacion;

                lblUsuarioCreador.Text = row["UsuarioCreador"].ToString();

                // Si hay una ruta de archivo, habilitar el botón para ver documento
                if (row["RutaArchivo"] != DBNull.Value)
                {
                    btnVerDocumento.CommandArgument = documentoId.ToString();
                    btnVerDocumento.Visible = true;
                }
                else
                {
                    btnVerDocumento.Visible = false;
                }

                // Guardar el ID del documento actual
                ViewState["DocumentoId"] = documentoId;

                // Mostrar el panel de firma
                pnlFirmaDocumento.Visible = true;

                // Ocultar la lista de documentos pendientes
                pnlDocumentosPendientes.Visible = false;
            }
            else
            {
                MostrarMensaje("El documento solicitado no existe o ha sido eliminado.", false);
                CargarDocumentosPendientes();
            }
        }

        protected void gvDocumentosPendientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Firmar")
            {
                int documentoId = Convert.ToInt32(e.CommandArgument);
                CargarDocumentoPendiente(documentoId);
            }
            else if (e.CommandName == "VerDocumento")
            {
                int documentoId = Convert.ToInt32(e.CommandArgument);
                DescargarDocumento(documentoId);
            }
        }

        protected void btnVerDocumento_Command(object sender, CommandEventArgs e)
        {
            int documentoId = Convert.ToInt32(e.CommandArgument);
            DescargarDocumento(documentoId);
        }

        protected void btnFirmar_Click(object sender, EventArgs e)
        {
            // Validar que se haya ingresado el PIN de firma
            if (string.IsNullOrWhiteSpace(txtPinFirma.Text))
            {
                MostrarMensaje("Por favor ingrese su PIN de firma.", false);
                return;
            }

            try
            {
                // Obtener datos para la firma
                int documentoId = Convert.ToInt32(ViewState["DocumentoId"]);
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                string pinFirma = txtPinFirma.Text.Trim();
                string observaciones = txtObservaciones.Text.Trim();

                // Verificar el PIN de firma
                if (!_firmaService.ValidarPinFirma(usuarioId, pinFirma))
                {
                    MostrarMensaje("El PIN de firma ingresado no es válido.", false);
                    return;
                }

                // Registrar la firma del documento
                _firmaService.FirmarDocumento(documentoId, usuarioId, observaciones);

                // Mostrar mensaje de éxito
                MostrarMensaje("Documento firmado exitosamente.", true);

                // Limpiar campos y volver a la lista de documentos pendientes
                txtPinFirma.Text = string.Empty;
                txtObservaciones.Text = string.Empty;
                ViewState["DocumentoId"] = null;

                // Mostrar nuevamente la lista de documentos pendientes
                pnlFirmaDocumento.Visible = false;
                pnlDocumentosPendientes.Visible = true;

                // Recargar documentos pendientes
                CargarDocumentosPendientes();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al firmar el documento: " + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpiar campos y volver a la lista de documentos pendientes
            txtPinFirma.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            ViewState["DocumentoId"] = null;

            // Mostrar nuevamente la lista de documentos pendientes
            pnlFirmaDocumento.Visible = false;
            pnlDocumentosPendientes.Visible = true;
        }

        protected void btnConfigurarPin_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de configuración de PIN
            Response.Redirect("ConfigurarPinFirma.aspx");
        }

        private void DescargarDocumento(int documentoId)
        {
            try
            {
                // Obtener información del archivo
                DataTable datosArchivo = _firmaService.ObtenerRutaDocumento(documentoId);

                if (datosArchivo.Rows.Count > 0 && datosArchivo.Rows[0]["RutaArchivo"] != DBNull.Value)
                {
                    DataRow row = datosArchivo.Rows[0];
                    string nombreArchivo = row["NombreDocumento"].ToString();
                    string rutaArchivo = row["RutaArchivo"].ToString();

                    // Verificar que el archivo exista
                    if (File.Exists(rutaArchivo))
                    {
                        // Determinar el tipo de contenido según la extensión
                        string extension = Path.GetExtension(rutaArchivo).ToLower();
                        string contentType = "application/octet-stream";

                        if (extension == ".pdf")
                            contentType = "application/pdf";
                        else if (extension == ".doc" || extension == ".docx")
                            contentType = "application/msword";
                        else if (extension == ".xls" || extension == ".xlsx")
                            contentType = "application/vnd.ms-excel";

                        // Enviar el archivo al cliente
                        Response.Clear();
                        Response.ContentType = contentType;
                        Response.AddHeader("Content-Disposition", $"inline; filename=\"{nombreArchivo}\"");
                        Response.TransmitFile(rutaArchivo);
                        Response.End();
                    }
                    else
                    {
                        MostrarMensaje("El archivo solicitado no se encuentra en el servidor.", false);
                    }
                }
                else
                {
                    MostrarMensaje("Este documento no tiene un archivo adjunto.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al descargar el documento: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }
    }

    // Clase de servicio para gestión de firmas
    public class FirmaService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public FirmaService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerDocumentosPendientesFirma(int usuarioId)
        {
            return _queryBuilder
                .Select("d.DocumentoId, td.Nombre AS TipoDocumento, d.NombreDocumento, " +
                        "d.Descripcion, d.FechaCreacion, u.NombreCompleto AS UsuarioCreador, " +
                        "CASE WHEN d.RutaArchivo IS NOT NULL THEN 1 ELSE 0 END AS TieneArchivo")
                .From("Documentos AS d")
                .Join("TiposDocumento AS td", "d.TipoDocumentoId = td.TipoDocumentoId")
                .Join("Usuarios AS u", "d.UsuarioCreacion = u.UsuarioId")
                .Join("DocumentosFirma AS df", "d.DocumentoId = df.DocumentoId")
                .Where(true, "df.UsuarioId", "=", usuarioId)
                .AndWhere(true, "df.FechaFirma", "IS", null)
                .OrderBy("d.FechaCreacion DESC")
                .Execute();
        }

        public DataTable ObtenerDocumentoPorId(int documentoId)
        {
            return _queryBuilder
                .Select("d.DocumentoId, td.Nombre AS TipoDocumento, d.NombreDocumento, " +
                        "d.Descripcion, d.FechaCreacion, u.NombreCompleto AS UsuarioCreador, " +
                        "d.RutaArchivo")
                .From("Documentos AS d")
                .Join("TiposDocumento AS td", "d.TipoDocumentoId = td.TipoDocumentoId")
                .Join("Usuarios AS u", "d.UsuarioCreacion = u.UsuarioId")
                .Where(true, "d.DocumentoId", "=", documentoId)
                .Execute();
        }

        public DataTable ObtenerRutaDocumento(int documentoId)
        {
            return _queryBuilder
                .Select("DocumentoId, NombreDocumento, RutaArchivo")
                .From("Documentos")
                .Where(true, "DocumentoId", "=", documentoId)
                .Execute();
        }

        public bool VerificarPermisosDocumento(int documentoId, int usuarioId)
        {
            var resultado = _queryBuilder
                .Select("COUNT(*)")
                .From("DocumentosFirma")
                .Where(true, "DocumentoId", "=", documentoId)
                .AndWhere(true, "UsuarioId", "=", usuarioId)
                .AndWhere(true, "FechaFirma", "IS", null)
                .Execute();

            return Convert.ToInt32(resultado.Rows[0][0]) > 0;
        }

        public bool ValidarPinFirma(int usuarioId, string pinFirma)
        {
            // Obtener el PIN almacenado (hash)
            var resultado = _queryBuilder
                .Select("PinFirma, SaltFirma")
                .From("Usuarios")
                .Where(true, "UsuarioId", "=", usuarioId)
                .Execute();

            if (resultado.Rows.Count == 0 || resultado.Rows[0]["PinFirma"] == DBNull.Value ||
                resultado.Rows[0]["SaltFirma"] == DBNull.Value)
            {
                return false;
            }

            string pinAlmacenado = resultado.Rows[0]["PinFirma"].ToString();
            string saltAlmacenado = resultado.Rows[0]["SaltFirma"].ToString();

            // Calcular el hash del PIN ingresado con el salt almacenado
            string pinHash = GenerarHash(pinFirma, saltAlmacenado);

            // Comparar los hashes
            return pinHash == pinAlmacenado;
        }

        public void FirmarDocumento(int documentoId, int usuarioId, string observaciones)
        {
            // Verificar que el documento exista y esté pendiente de firma
            if (!VerificarPermisosDocumento(documentoId, usuarioId))
            {
                throw new Exception("No tiene permisos para firmar este documento o ya ha sido firmado.");
            }

            // Actualizar el registro de firma
            string sql = @"
                UPDATE DocumentosFirma SET
                    FechaFirma = @FechaFirma,
                    Observaciones = @Observaciones,
                    DireccionIP = @DireccionIP
                WHERE DocumentoId = @DocumentoId
                AND UsuarioId = @UsuarioId
                AND FechaFirma IS NULL";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@DocumentoId", documentoId },
                { "@UsuarioId", usuarioId },
                { "@FechaFirma", DateTime.Now },
                { "@Observaciones", observaciones },
                { "@DireccionIP", HttpContext.Current.Request.UserHostAddress }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Verificar si todas las firmas requeridas han sido completadas
            var firmasPendientes = _queryBuilder
                .Select("COUNT(*)")
                .From("DocumentosFirma")
                .Where(true, "DocumentoId", "=", documentoId)
                .AndWhere(true, "FechaFirma", "IS", null)
                .Execute();

            if (Convert.ToInt32(firmasPendientes.Rows[0][0]) == 0)
            {
                // Todas las firmas han sido completadas, actualizar el estado del documento
                ActualizarEstadoDocumento(documentoId, "Firmado", usuarioId);
            }

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Firmar", "DocumentosFirma", $"Documento {documentoId}");
        }

        private void ActualizarEstadoDocumento(int documentoId, string nuevoEstado, int usuarioId)
        {
            string sql = @"
                UPDATE Documentos SET
                    Estado = @Estado,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE DocumentoId = @DocumentoId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@DocumentoId", documentoId },
                { "@Estado", nuevoEstado },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);
        }

        public bool ConfigurarPinFirma(int usuarioId, string pinFirma)
        {
            // Generar un salt aleatorio
            string salt = GenerarSalt();

            // Calcular el hash del PIN con el salt
            string pinHash = GenerarHash(pinFirma, salt);

            // Actualizar el PIN y salt en la base de datos
            string sql = @"
                UPDATE Usuarios SET
                    PinFirma = @PinFirma,
                    SaltFirma = @SaltFirma,
                    FechaModificacionPin = @FechaModificacion
                WHERE UsuarioId = @UsuarioId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UsuarioId", usuarioId },
                { "@PinFirma", pinHash },
                { "@SaltFirma", salt },
                { "@FechaModificacion", DateTime.Now }
            };

            int resultado = _dbConnection.ExecuteNonQuery(sql, parameters);

            if (resultado > 0)
            {
                RegistrarAccionHistorial(usuarioId, "ConfigurarPIN", "Usuarios", "Configuración de PIN de firma");
                return true;
            }

            return false;
        }

        private string GenerarSalt()
        {
            byte[] randomBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string GenerarHash(string input, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                // Concatenar el PIN y el salt
                string inputWithSalt = input + salt;

                // Calcular el hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputWithSalt));

                // Convertir a cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
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

    // Clase para la página de configuración del PIN de firma
    public partial class ConfigurarPinFirma : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private FirmaService _firmaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _firmaService = new FirmaService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
            }
        }

        protected void btnGuardarPin_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se hayan ingresado los PIN y que coincidan
                if (string.IsNullOrWhiteSpace(txtNuevoPin.Text))
                {
                    MostrarMensaje("Por favor ingrese un nuevo PIN de firma.", false);
                    return;
                }

                if (txtNuevoPin.Text != txtConfirmarPin.Text)
                {
                    MostrarMensaje("Los PIN ingresados no coinciden.", false);
                    return;
                }

                // Validar que el PIN tenga al menos 4 dígitos
                if (txtNuevoPin.Text.Length < 4)
                {
                    MostrarMensaje("El PIN debe tener al menos 4 dígitos.", false);
                    return;
                }

                // Guardar el nuevo PIN
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                string nuevoPin = txtNuevoPin.Text.Trim();

                if (_firmaService.ConfigurarPinFirma(usuarioId, nuevoPin))
                {
                    MostrarMensaje("PIN de firma configurado exitosamente.", true);

                    // Limpiar campos
                    txtNuevoPin.Text = string.Empty;
                    txtConfirmarPin.Text = string.Empty;
                }
                else
                {
                    MostrarMensaje("Error al configurar el PIN de firma.", false);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de firma
            Response.Redirect("Firma.aspx");
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }
    }
}