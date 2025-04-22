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
using System.Globalization;

namespace Satelite
{
    public partial class RevisionLotes : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private RevisionLotesService _revisionService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _revisionService = new RevisionLotesService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Cargar datos iniciales
                CargarResponsables();
                CargarProductos();
                CargarEstados();

                // Si hay ID en QueryString, cargar revisión existente
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int revisionId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarRevisionExistente(revisionId);
                }
                else
                {
                    // Nueva revisión
                    ConfigurarNuevaRevision();
                }

                // Cargar listado de revisiones
                CargarRevisionesExistentes();
            }
        }

        protected void CargarResponsables()
        {
            DataTable responsables = _revisionService.ObtenerResponsables();

            ddlResponsable.DataSource = responsables;
            ddlResponsable.DataTextField = "NombreCompleto";
            ddlResponsable.DataValueField = "UsuarioId";
            ddlResponsable.DataBind();

            // Agregar opción por defecto
            ddlResponsable.Items.Insert(0, new ListItem("-- Seleccione un responsable --", "0"));
        }

        protected void CargarProductos()
        {
            DataTable productos = _revisionService.ObtenerProductos();

            ddlProducto.DataSource = productos;
            ddlProducto.DataTextField = "NombreProducto";
            ddlProducto.DataValueField = "ProductoId";
            ddlProducto.DataBind();

            // Agregar opción por defecto
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
        }

        protected void CargarEstados()
        {
            // Los estados se cargan directamente en el aspx, pero podría obtenerse de la base de datos
            // Pendiente (default), EnProceso, Aprobado, Rechazado
        }

        protected void ConfigurarNuevaRevision()
        {
            // Establecer valores predeterminados
            txtFechaCreacion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtNumeroLote.Text = GenerarNumeroLote();
            ddlEstado.SelectedValue = "Pendiente";

            // Usuario actual como responsable predeterminado
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            if (ddlResponsable.Items.FindByValue(usuarioId.ToString()) != null)
            {
                ddlResponsable.SelectedValue = usuarioId.ToString();
            }

            // Deshabilitar campos según estado
            ConfigurarCamposPorEstado("Pendiente");

            // Limpiar ViewState
            ViewState["RevisionId"] = null;

            // Establecer título y texto del botón
            lblTitulo.Text = "Nueva Revisión de Lote";
            btnGuardar.Text = "Guardar";

            // Mostrar panel de creación y ocultar panel de resultados
            pnlDatosRevision.Visible = true;
            pnlResultadosRevision.Visible = false;
        }

        protected void CargarRevisionExistente(int revisionId)
        {
            DataTable revision = _revisionService.ObtenerRevisionPorId(revisionId);

            if (revision.Rows.Count > 0)
            {
                DataRow row = revision.Rows[0];

                // Establecer valores en los controles
                txtNumeroLote.Text = row["NumeroLote"].ToString();
                txtFechaCreacion.Text = Convert.ToDateTime(row["FechaCreacion"]).ToString("yyyy-MM-dd");
                ddlProducto.SelectedValue = row["ProductoId"].ToString();

                if (row["ResponsableId"] != DBNull.Value)
                {
                    ddlResponsable.SelectedValue = row["ResponsableId"].ToString();
                }

                txtCantidad.Text = row["Cantidad"].ToString();
                txtUnidad.Text = row["Unidad"].ToString();
                txtLoteProveedor.Text = row["LoteProveedor"].ToString();
                txtFechaProduccion.Text = row["FechaProduccion"] != DBNull.Value ?
                    Convert.ToDateTime(row["FechaProduccion"]).ToString("yyyy-MM-dd") : string.Empty;
                txtFechaCaducidad.Text = row["FechaCaducidad"] != DBNull.Value ?
                    Convert.ToDateTime(row["FechaCaducidad"]).ToString("yyyy-MM-dd") : string.Empty;
                txtObservaciones.Text = row["Observaciones"].ToString();

                // Establecer estado
                string estado = row["Estado"].ToString();
                ddlEstado.SelectedValue = estado;

                // Cargar resultados de revisión si aplica
                if (estado != "Pendiente")
                {
                    CargarResultadosRevision(revisionId);
                }

                // Configurar campos según estado
                ConfigurarCamposPorEstado(estado);

                // Guardar ID para actualización
                ViewState["RevisionId"] = revisionId;

                // Cambiar título y texto del botón
                lblTitulo.Text = "Editar Revisión de Lote";
                btnGuardar.Text = "Actualizar";
            }
            else
            {
                // No se encontró la revisión, redirigir a nueva revisión
                Response.Redirect("RevisionLotes.aspx");
            }
        }

        protected void CargarResultadosRevision(int revisionId)
        {
            DataTable resultados = _revisionService.ObtenerResultadosRevision(revisionId);

            if (resultados.Rows.Count > 0)
            {
                DataRow row = resultados.Rows[0];

                // Establecer valores en los controles de resultados
                txtFechaRevision.Text = Convert.ToDateTime(row["FechaRevision"]).ToString("yyyy-MM-dd");
                txtResultadoVisual.Text = row["ResultadoVisual"].ToString();
                txtResultadoOlfativo.Text = row["ResultadoOlfativo"].ToString();
                txtResultadoGustativo.Text = row["ResultadoGustativo"].ToString();
                txtResultadoTextura.Text = row["ResultadoTextura"].ToString();
                txtResultadoColor.Text = row["ResultadoColor"].ToString();
                txtOtrosResultados.Text = row["OtrosResultados"].ToString();
                chkCumpleNorma.Checked = Convert.ToBoolean(row["CumpleNormas"]);
                txtComentariosFinales.Text = row["ComentariosFinales"].ToString();

                // Mostrar panel de resultados
                pnlResultadosRevision.Visible = true;
            }
            else
            {
                // No hay resultados todavía
                pnlResultadosRevision.Visible = true;

                // Establecer valores predeterminados para resultados
                txtFechaRevision.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void CargarRevisionesExistentes()
        {
            DataTable revisiones = _revisionService.ObtenerRevisiones();

            gvRevisiones.DataSource = revisiones;
            gvRevisiones.DataBind();

            // Mostrar mensaje si no hay revisiones
            lblSinRevisiones.Visible = (revisiones.Rows.Count == 0);
        }

        protected void ConfigurarCamposPorEstado(string estado)
        {
            bool esPendiente = (estado == "Pendiente");
            bool esEnProceso = (estado == "EnProceso");
            bool esFinalizado = (estado == "Aprobado" || estado == "Rechazado");

            // Campos de la revisión
            txtNumeroLote.Enabled = esPendiente;
            txtFechaCreacion.Enabled = esPendiente;
            ddlProducto.Enabled = esPendiente;
            ddlResponsable.Enabled = esPendiente || esEnProceso;
            txtCantidad.Enabled = esPendiente;
            txtUnidad.Enabled = esPendiente;
            txtLoteProveedor.Enabled = esPendiente;
            txtFechaProduccion.Enabled = esPendiente;
            txtFechaCaducidad.Enabled = esPendiente;
            txtObservaciones.Enabled = esPendiente || esEnProceso;

            // Campos de resultados
            pnlResultadosRevision.Visible = esEnProceso || esFinalizado;
            txtFechaRevision.Enabled = esEnProceso;
            txtResultadoVisual.Enabled = esEnProceso;
            txtResultadoOlfativo.Enabled = esEnProceso;
            txtResultadoGustativo.Enabled = esEnProceso;
            txtResultadoTextura.Enabled = esEnProceso;
            txtResultadoColor.Enabled = esEnProceso;
            txtOtrosResultados.Enabled = esEnProceso;
            chkCumpleNorma.Enabled = esEnProceso;
            txtComentariosFinales.Enabled = esEnProceso;

            // Estado
            ddlEstado.Enabled = !esFinalizado;

            // Documentos
            pnlDocumentos.Visible = !esPendiente;

            // Botón de guardar
            btnGuardar.Enabled = !esFinalizado;
        }

        protected string GenerarNumeroLote()
        {
            // Generar número de lote con formato: L-YYYYMMDD-XXX (donde XXX es un número secuencial)
            string fechaParte = DateTime.Now.ToString("yyyyMMdd");

            // Obtener el último número secuencial para la fecha actual
            int secuencial = _revisionService.ObtenerSecuencialLote(fechaParte);

            // Incrementar el secuencial
            secuencial++;

            // Formatear el número de lote
            return $"L-{fechaParte}-{secuencial:D3}";
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estado = ddlEstado.SelectedValue;
            ConfigurarCamposPorEstado(estado);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos requeridos
            if (string.IsNullOrWhiteSpace(txtNumeroLote.Text) || ddlProducto.SelectedValue == "0" ||
                string.IsNullOrWhiteSpace(txtCantidad.Text) || string.IsNullOrWhiteSpace(txtUnidad.Text))
            {
                MostrarMensaje("Por favor complete todos los campos requeridos.", false);
                return;
            }

            try
            {
                // Obtener datos comunes
                string numeroLote = txtNumeroLote.Text.Trim();
                DateTime fechaCreacion = DateTime.Parse(txtFechaCreacion.Text);
                int productoId = Convert.ToInt32(ddlProducto.SelectedValue);

                int? responsableId = null;
                if (ddlResponsable.SelectedValue != "0")
                {
                    responsableId = Convert.ToInt32(ddlResponsable.SelectedValue);
                }

                decimal cantidad = Convert.ToDecimal(txtCantidad.Text, CultureInfo.InvariantCulture);
                string unidad = txtUnidad.Text.Trim();
                string loteProveedor = txtLoteProveedor.Text.Trim();

                DateTime? fechaProduccion = null;
                if (!string.IsNullOrWhiteSpace(txtFechaProduccion.Text))
                {
                    fechaProduccion = DateTime.Parse(txtFechaProduccion.Text);
                }

                DateTime? fechaCaducidad = null;
                if (!string.IsNullOrWhiteSpace(txtFechaCaducidad.Text))
                {
                    fechaCaducidad = DateTime.Parse(txtFechaCaducidad.Text);
                }

                string observaciones = txtObservaciones.Text.Trim();
                string estado = ddlEstado.SelectedValue;

                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Datos de resultados (si aplica)
                DateTime? fechaRevision = null;
                string resultadoVisual = null;
                string resultadoOlfativo = null;
                string resultadoGustativo = null;
                string resultadoTextura = null;
                string resultadoColor = null;
                string otrosResultados = null;
                bool cumpleNormas = false;
                string comentariosFinales = null;

                if (estado != "Pendiente")
                {
                    if (!string.IsNullOrWhiteSpace(txtFechaRevision.Text))
                    {
                        fechaRevision = DateTime.Parse(txtFechaRevision.Text);
                    }

                    resultadoVisual = txtResultadoVisual.Text.Trim();
                    resultadoOlfativo = txtResultadoOlfativo.Text.Trim();
                    resultadoGustativo = txtResultadoGustativo.Text.Trim();
                    resultadoTextura = txtResultadoTextura.Text.Trim();
                    resultadoColor = txtResultadoColor.Text.Trim();
                    otrosResultados = txtOtrosResultados.Text.Trim();
                    cumpleNormas = chkCumpleNorma.Checked;
                    comentariosFinales = txtComentariosFinales.Text.Trim();

                    // Validar resultados si el estado es finalizado
                    if (estado == "Aprobado" || estado == "Rechazado")
                    {
                        if (!fechaRevision.HasValue || string.IsNullOrWhiteSpace(resultadoVisual) ||
                            string.IsNullOrWhiteSpace(resultadoOlfativo) || string.IsNullOrWhiteSpace(resultadoGustativo) ||
                            string.IsNullOrWhiteSpace(comentariosFinales))
                        {
                            MostrarMensaje("Para finalizar la revisión debe completar todos los resultados.", false);
                            return;
                        }
                    }
                }

                int revisionId;

                if (ViewState["RevisionId"] != null)
                {
                    // Actualizar revisión existente
                    revisionId = Convert.ToInt32(ViewState["RevisionId"]);

                    _revisionService.ActualizarRevision(
                        revisionId,
                        numeroLote,
                        fechaCreacion,
                        productoId,
                        responsableId,
                        cantidad,
                        unidad,
                        loteProveedor,
                        fechaProduccion,
                        fechaCaducidad,
                        observaciones,
                        estado,
                        usuarioId);

                    // Si hay resultados, actualizarlos o crearlos
                    if (estado != "Pendiente")
                    {
                        if (_revisionService.ExistenResultadosRevision(revisionId))
                        {
                            _revisionService.ActualizarResultadosRevision(
                                revisionId,
                                fechaRevision,
                                resultadoVisual,
                                resultadoOlfativo,
                                resultadoGustativo,
                                resultadoTextura,
                                resultadoColor,
                                otrosResultados,
                                cumpleNormas,
                                comentariosFinales,
                                usuarioId);
                        }
                        else
                        {
                            _revisionService.CrearResultadosRevision(
                                revisionId,
                                fechaRevision,
                                resultadoVisual,
                                resultadoOlfativo,
                                resultadoGustativo,
                                resultadoTextura,
                                resultadoColor,
                                otrosResultados,
                                cumpleNormas,
                                comentariosFinales,
                                usuarioId);
                        }
                    }

                    MostrarMensaje("Revisión actualizada exitosamente.", true);
                }
                else
                {
                    // Crear nueva revisión
                    revisionId = _revisionService.CrearRevision(
                        numeroLote,
                        fechaCreacion,
                        productoId,
                        responsableId,
                        cantidad,
                        unidad,
                        loteProveedor,
                        fechaProduccion,
                        fechaCaducidad,
                        observaciones,
                        estado,
                        usuarioId);

                    // Si hay resultados, crearlos
                    if (estado != "Pendiente")
                    {
                        _revisionService.CrearResultadosRevision(
                            revisionId,
                            fechaRevision,
                            resultadoVisual,
                            resultadoOlfativo,
                            resultadoGustativo,
                            resultadoTextura,
                            resultadoColor,
                            otrosResultados,
                            cumpleNormas,
                            comentariosFinales,
                            usuarioId);
                    }

                    MostrarMensaje("Revisión creada exitosamente.", true);

                    // Redirigir a edición de la revisión creada
                    Response.Redirect($"RevisionLotes.aspx?id={revisionId}");
                }

                // Recargar listado de revisiones
                CargarRevisionesExistentes();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar la revisión: " + ex.Message, false);
            }
        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            // Redirigir a nueva revisión
            Response.Redirect("RevisionLotes.aspx");
        }

        protected void gvRevisiones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarRevision")
            {
                int revisionId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"RevisionLotes.aspx?id={revisionId}");
            }
            else if (e.CommandName == "ImprimirRevision")
            {
                int revisionId = Convert.ToInt32(e.CommandArgument);
                // Implementar la lógica para generar e imprimir la revisión
                Response.Redirect($"ImprimirRevision.aspx?id={revisionId}");
            }
            else if (e.CommandName == "EliminarRevision")
            {
                try
                {
                    int revisionId = Convert.ToInt32(e.CommandArgument);
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                    // Verificar que la revisión esté en estado pendiente
                    string estado = _revisionService.ObtenerEstadoRevision(revisionId);
                    if (estado != "Pendiente")
                    {
                        MostrarMensaje("Solo se pueden eliminar revisiones en estado Pendiente.", false);
                        return;
                    }

                    // Eliminar la revisión
                    _revisionService.EliminarRevision(revisionId, usuarioId);

                    MostrarMensaje("Revisión eliminada exitosamente.", true);

                    // Recargar listado de revisiones
                    CargarRevisionesExistentes();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar la revisión: " + ex.Message, false);
                }
            }
        }

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            // Verificar que haya un archivo seleccionado
            if (!fileUpload.HasFile)
            {
                MostrarMensaje("Por favor seleccione un archivo para subir.", false);
                return;
            }

            // Verificar que exista ID de revisión
            if (ViewState["RevisionId"] == null)
            {
                MostrarMensaje("Debe guardar la revisión antes de subir documentos.", false);
                return;
            }

            try
            {
                // Obtener datos del archivo
                string fileName = Path.GetFileName(fileUpload.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Validar la extensión del archivo
                string[] extensionesPermitidas = { ".pdf", ".doc", ".docx", ".jpg", ".png", ".xls", ".xlsx" };
                if (!extensionesPermitidas.Contains(fileExtension))
                {
                    MostrarMensaje("Tipo de archivo no permitido. Use formatos comunes (PDF, Word, Excel, imágenes).", false);
                    return;
                }

                // Generar nombre único para el archivo
                string nuevoNombre = $"revision_{ViewState["RevisionId"]}_{Guid.NewGuid()}{fileExtension}";

                // Ruta donde se guardará el archivo
                string rutaArchivos = Server.MapPath("~/Archivos/RevisionLotes/");

                // Verificar si existe el directorio y si no, crearlo
                if (!Directory.Exists(rutaArchivos))
                {
                    Directory.CreateDirectory(rutaArchivos);
                }

                // Guardar el archivo físicamente
                string rutaCompleta = Path.Combine(rutaArchivos, nuevoNombre);
                fileUpload.SaveAs(rutaCompleta);

                // Registrar el documento en la base de datos
                int revisionId = Convert.ToInt32(ViewState["RevisionId"]);
                string tipoDocumento = ddlTipoDocumento.SelectedValue;
                string descripcion = txtDescripcionDoc.Text.Trim();
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                _revisionService.AgregarDocumentoRevision(
                    revisionId,
                    tipoDocumento,
                    descripcion,
                    fileName,
                    nuevoNombre,
                    rutaCompleta,
                    usuarioId);

                MostrarMensaje("Documento subido exitosamente.", true);

                // Limpiar controles
                ddlTipoDocumento.SelectedIndex = 0;
                txtDescripcionDoc.Text = string.Empty;

                // Recargar documentos
                CargarDocumentosRevision(revisionId);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al subir el documento: " + ex.Message, false);
            }
        }

        protected void CargarDocumentosRevision(int revisionId)
        {
            DataTable documentos = _revisionService.ObtenerDocumentosRevision(revisionId);

            gvDocumentos.DataSource = documentos;
            gvDocumentos.DataBind();

            // Mostrar mensaje si no hay documentos
            lblSinDocumentos.Visible = (documentos.Rows.Count == 0);
        }

        protected void gvDocumentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DescargarDocumento")
            {
                try
                {
                    int documentoId = Convert.ToInt32(e.CommandArgument);
                    DescargarDocumento(documentoId);
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al descargar el documento: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "EliminarDocumento")
            {
                try
                {
                    int documentoId = Convert.ToInt32(e.CommandArgument);
                    int revisionId = Convert.ToInt32(ViewState["RevisionId"]);
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                    _revisionService.EliminarDocumentoRevision(documentoId, usuarioId);

                    MostrarMensaje("Documento eliminado exitosamente.", true);

                    // Recargar documentos
                    CargarDocumentosRevision(revisionId);
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar el documento: " + ex.Message, false);
                }
            }
        }

        private void DescargarDocumento(int documentoId)
        {
            try
            {
                // Obtener información del documento
                DataTable documentoInfo = _revisionService.ObtenerDocumentoPorId(documentoId);

                if (documentoInfo.Rows.Count > 0)
                {
                    DataRow row = documentoInfo.Rows[0];
                    string nombreOriginal = row["NombreOriginal"].ToString();
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
                        else if (extension == ".jpg" || extension == ".jpeg")
                            contentType = "image/jpeg";
                        else if (extension == ".png")
                            contentType = "image/png";

                        // Enviar el archivo al cliente
                        Response.Clear();
                        Response.ContentType = contentType;
                        Response.AddHeader("Content-Disposition", $"attachment; filename=\"{nombreOriginal}\"");
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
                    MostrarMensaje("El documento solicitado no existe.", false);
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

    // Clase de servicio para gestión de revisiones de lotes
    public class RevisionLotesService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public RevisionLotesService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
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

        public DataTable ObtenerProductos()
        {
            return _queryBuilder
                .Select("ProductoId, Codigo + ' - ' + Nombre AS NombreProducto, Unidad")
                .From("Productos")
                .Where(true, "Activo", "=", true)
                .OrderBy("Codigo")
                .Execute();
        }

        public DataTable ObtenerRevisiones()
        {
            return _queryBuilder
                .Select("r.RevisionId, r.NumeroLote, p.Nombre AS Producto, r.Cantidad, r.Unidad, " +
                        "r.FechaCreacion, r.Estado, u.NombreCompleto AS Responsable")
                .From("RevisionesLotes AS r")
                .Join("Productos AS p", "r.ProductoId = p.ProductoId")
                .LeftJoin("Usuarios AS u", "r.ResponsableId = u.UsuarioId")
                .OrderBy("r.FechaCreacion DESC")
                .Execute();
        }

        public DataTable ObtenerRevisionPorId(int revisionId)
        {
            return _queryBuilder
                .Select("r.RevisionId, r.NumeroLote, r.FechaCreacion, r.ProductoId, " +
                        "r.ResponsableId, r.Cantidad, r.Unidad, r.LoteProveedor, " +
                        "r.FechaProduccion, r.FechaCaducidad, r.Observaciones, r.Estado")
                .From("RevisionesLotes AS r")
                .Where(true, "r.RevisionId", "=", revisionId)
                .Execute();
        }

        public DataTable ObtenerResultadosRevision(int revisionId)
        {
            return _queryBuilder
            .Select("RevisionId, FechaRevision, ResultadoVisual, " +
                        "ResultadoOlfativo, ResultadoGustativo, ResultadoTextura, " +
                        "ResultadoColor, OtrosResultados, CumpleNormas, ComentariosFinales")
                .From("ResultadosRevisionLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();
        }

        public DataTable ObtenerDocumentosRevision(int revisionId)
        {
            return _queryBuilder
                .Select("d.DocumentoId, d.TipoDocumento, d.Descripcion, d.NombreOriginal, " +
                        "d.FechaSubida, u.NombreCompleto AS UsuarioSubida")
                .From("RevisionLotesDocumentos AS d")
                .Join("Usuarios AS u", "d.UsuarioSubida = u.UsuarioId")
                .Where(true, "d.RevisionId", "=", revisionId)
                .OrderBy("d.FechaSubida DESC")
                .Execute();
        }

        public DataTable ObtenerDocumentoPorId(int documentoId)
        {
            return _queryBuilder
                .Select("DocumentoId, RevisionId, TipoDocumento, Descripcion, " +
                        "NombreOriginal, NombreUnico, RutaArchivo")
                .From("RevisionLotesDocumentos")
                .Where(true, "DocumentoId", "=", documentoId)
                .Execute();
        }

        public string ObtenerEstadoRevision(int revisionId)
        {
            var resultado = _queryBuilder
                .Select("Estado")
                .From("RevisionesLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            if (resultado.Rows.Count > 0)
            {
                return resultado.Rows[0]["Estado"].ToString();
            }

            return string.Empty;
        }

        public int ObtenerSecuencialLote(string fechaParte)
        {
            var resultado = _queryBuilder
                .Select("MAX(CAST(SUBSTRING(NumeroLote, LEN(NumeroLote) - 2, 3) AS INT)) AS Secuencial")
                .From("RevisionesLotes")
                .Where(true, "NumeroLote", "LIKE", $"L-{fechaParte}-%")
                .Execute();

            if (resultado.Rows.Count > 0 && resultado.Rows[0]["Secuencial"] != DBNull.Value)
            {
                return Convert.ToInt32(resultado.Rows[0]["Secuencial"]);
            }

            return 0;
        }

        public bool ExistenResultadosRevision(int revisionId)
        {
            var resultado = _queryBuilder
                .Select("COUNT(*)")
                .From("ResultadosRevisionLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            return Convert.ToInt32(resultado.Rows[0][0]) > 0;
        }

        public int CrearRevision(
            string numeroLote,
            DateTime fechaCreacion,
            int productoId,
            int? responsableId,
            decimal cantidad,
            string unidad,
            string loteProveedor,
            DateTime? fechaProduccion,
            DateTime? fechaCaducidad,
            string observaciones,
            string estado,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO RevisionesLotes (
                    NumeroLote,
                    FechaCreacion,
                    ProductoId,
                    ResponsableId,
                    Cantidad,
                    Unidad,
                    LoteProveedor,
                    FechaProduccion,
                    FechaCaducidad,
                    Observaciones,
                    Estado,
                    UsuarioCreacion,
                    FechaRegistro
                ) VALUES (
                    @NumeroLote,
                    @FechaCreacion,
                    @ProductoId,
                    @ResponsableId,
                    @Cantidad,
                    @Unidad,
                    @LoteProveedor,
                    @FechaProduccion,
                    @FechaCaducidad,
                    @Observaciones,
                    @Estado,
                    @UsuarioCreacion,
                    @FechaRegistro
                ); SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NumeroLote", numeroLote },
                { "@FechaCreacion", fechaCreacion },
                { "@ProductoId", productoId },
                { "@ResponsableId", responsableId.HasValue ? (object)responsableId.Value : DBNull.Value },
                { "@Cantidad", cantidad },
                { "@Unidad", unidad },
                { "@LoteProveedor", string.IsNullOrEmpty(loteProveedor) ? DBNull.Value : (object)loteProveedor },
                { "@FechaProduccion", fechaProduccion.HasValue ? (object)fechaProduccion.Value : DBNull.Value },
                { "@FechaCaducidad", fechaCaducidad.HasValue ? (object)fechaCaducidad.Value : DBNull.Value },
                { "@Observaciones", string.IsNullOrEmpty(observaciones) ? DBNull.Value : (object)observaciones },
                { "@Estado", estado },
                { "@UsuarioCreacion", usuarioId },
                { "@FechaRegistro", DateTime.Now }
            };

            // Ejecutar la inserción y obtener el ID generado
            object result = _dbConnection.ExecuteScalar(sql, parameters);
            int revisionId = Convert.ToInt32(result);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "RevisionesLotes", $"Revisión de lote {numeroLote}");

            return revisionId;
        }

        public void ActualizarRevision(
            int revisionId,
            string numeroLote,
            DateTime fechaCreacion,
            int productoId,
            int? responsableId,
            decimal cantidad,
            string unidad,
            string loteProveedor,
            DateTime? fechaProduccion,
            DateTime? fechaCaducidad,
            string observaciones,
            string estado,
            int usuarioId)
        {
            // Obtener el estado actual para verificar cambios
            string estadoAnterior = ObtenerEstadoRevision(revisionId);

            string sql = @"
                UPDATE RevisionesLotes SET
                    NumeroLote = @NumeroLote,
                    FechaCreacion = @FechaCreacion,
                    ProductoId = @ProductoId,
                    ResponsableId = @ResponsableId,
                    Cantidad = @Cantidad,
                    Unidad = @Unidad,
                    LoteProveedor = @LoteProveedor,
                    FechaProduccion = @FechaProduccion,
                    FechaCaducidad = @FechaCaducidad,
                    Observaciones = @Observaciones,
                    Estado = @Estado,
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = @FechaModificacion
                WHERE RevisionId = @RevisionId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@RevisionId", revisionId },
                { "@NumeroLote", numeroLote },
                { "@FechaCreacion", fechaCreacion },
                { "@ProductoId", productoId },
                { "@ResponsableId", responsableId.HasValue ? (object)responsableId.Value : DBNull.Value },
                { "@Cantidad", cantidad },
                { "@Unidad", unidad },
                { "@LoteProveedor", string.IsNullOrEmpty(loteProveedor) ? DBNull.Value : (object)loteProveedor },
                { "@FechaProduccion", fechaProduccion.HasValue ? (object)fechaProduccion.Value : DBNull.Value },
                { "@FechaCaducidad", fechaCaducidad.HasValue ? (object)fechaCaducidad.Value : DBNull.Value },
                { "@Observaciones", string.IsNullOrEmpty(observaciones) ? DBNull.Value : (object)observaciones },
                { "@Estado", estado },
                { "@UsuarioModificacion", usuarioId },
                { "@FechaModificacion", DateTime.Now }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar cambio de estado si aplica
            if (estadoAnterior != estado)
            {
                RegistrarAccionHistorial(usuarioId, "CambioEstado", "RevisionesLotes", $"Revisión {revisionId}: de {estadoAnterior} a {estado}");
            }
            else
            {
                RegistrarAccionHistorial(usuarioId, "Actualizar", "RevisionesLotes", $"Revisión de lote {numeroLote}");
            }
        }

        public void EliminarRevision(int revisionId, int usuarioId)
        {
            // Primero obtener información para el historial
            var revision = _queryBuilder
                .Select("NumeroLote, ProductoId")
                .From("RevisionesLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            if (revision.Rows.Count == 0)
            {
                throw new Exception("La revisión no existe.");
            }

            string numeroLote = revision.Rows[0]["NumeroLote"].ToString();
            int productoId = Convert.ToInt32(revision.Rows[0]["ProductoId"]);

            // Obtener nombre del producto
            var producto = _queryBuilder
                .Select("Nombre")
                .From("Productos")
                .Where(true, "ProductoId", "=", productoId)
                .Execute();

            string nombreProducto = producto.Rows[0]["Nombre"].ToString();

            // Eliminar documentos relacionados
            DataTable documentos = ObtenerDocumentosRevision(revisionId);
            foreach (DataRow doc in documentos.Rows)
            {
                int documentoId = Convert.ToInt32(doc["DocumentoId"]);
                EliminarDocumentoRevision(documentoId, usuarioId, false); // Sin registrar en historial individualmente
            }

            // Eliminar resultados si existen
            if (ExistenResultadosRevision(revisionId))
            {
                string sqlResultados = "DELETE FROM ResultadosRevisionLotes WHERE RevisionId = @RevisionId";
                Dictionary<string, object> parametersResultados = new Dictionary<string, object>
                {
                    { "@RevisionId", revisionId }
                };

                _dbConnection.ExecuteNonQuery(sqlResultados, parametersResultados);
            }

            // Eliminar la revisión
            string sqlRevision = "DELETE FROM RevisionesLotes WHERE RevisionId = @RevisionId";
            Dictionary<string, object> parametersRevision = new Dictionary<string, object>
            {
                { "@RevisionId", revisionId }
            };

            _dbConnection.ExecuteNonQuery(sqlRevision, parametersRevision);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "RevisionesLotes", $"Revisión de lote {numeroLote} para {nombreProducto}");
        }

        public void CrearResultadosRevision(
            int revisionId,
            DateTime? fechaRevision,
            string resultadoVisual,
            string resultadoOlfativo,
            string resultadoGustativo,
            string resultadoTextura,
            string resultadoColor,
            string otrosResultados,
            bool cumpleNormas,
            string comentariosFinales,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO ResultadosRevisionLotes (
                    RevisionId,
                    FechaRevision,
                    ResultadoVisual,
                    ResultadoOlfativo,
                    ResultadoGustativo,
                    ResultadoTextura,
                    ResultadoColor,
                    OtrosResultados,
                    CumpleNormas,
                    ComentariosFinales,
                    UsuarioCreacion,
                    FechaRegistro
                ) VALUES (
                    @RevisionId,
                    @FechaRevision,
                    @ResultadoVisual,
                    @ResultadoOlfativo,
                    @ResultadoGustativo,
                    @ResultadoTextura,
                    @ResultadoColor,
                    @OtrosResultados,
                    @CumpleNormas,
                    @ComentariosFinales,
                    @UsuarioCreacion,
                    @FechaRegistro
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@RevisionId", revisionId },
                { "@FechaRevision", fechaRevision.HasValue ? (object)fechaRevision.Value : DBNull.Value },
                { "@ResultadoVisual", string.IsNullOrEmpty(resultadoVisual) ? DBNull.Value : (object)resultadoVisual },
                { "@ResultadoOlfativo", string.IsNullOrEmpty(resultadoOlfativo) ? DBNull.Value : (object)resultadoOlfativo },
                { "@ResultadoGustativo", string.IsNullOrEmpty(resultadoGustativo) ? DBNull.Value : (object)resultadoGustativo },
                { "@ResultadoTextura", string.IsNullOrEmpty(resultadoTextura) ? DBNull.Value : (object)resultadoTextura },
                { "@ResultadoColor", string.IsNullOrEmpty(resultadoColor) ? DBNull.Value : (object)resultadoColor },
                { "@OtrosResultados", string.IsNullOrEmpty(otrosResultados) ? DBNull.Value : (object)otrosResultados },
                { "@CumpleNormas", cumpleNormas },
                { "@ComentariosFinales", string.IsNullOrEmpty(comentariosFinales) ? DBNull.Value : (object)comentariosFinales },
                { "@UsuarioCreacion", usuarioId },
                { "@FechaRegistro", DateTime.Now }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Obtener número de lote para el historial
            var revision = _queryBuilder
                .Select("NumeroLote")
                .From("RevisionesLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            string numeroLote = revision.Rows[0]["NumeroLote"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "CrearResultados", "ResultadosRevisionLotes", $"Resultados para revisión de lote {numeroLote}");
        }

        public void ActualizarResultadosRevision(
            int revisionId,
            DateTime? fechaRevision,
            string resultadoVisual,
            string resultadoOlfativo,
            string resultadoGustativo,
            string resultadoTextura,
            string resultadoColor,
            string otrosResultados,
            bool cumpleNormas,
            string comentariosFinales,
            int usuarioId)
        {
            string sql = @"
                UPDATE ResultadosRevisionLotes SET
                    FechaRevision = @FechaRevision,
                    ResultadoVisual = @ResultadoVisual,
                    ResultadoOlfativo = @ResultadoOlfativo,
                    ResultadoGustativo = @ResultadoGustativo,
                    ResultadoTextura = @ResultadoTextura,
                    ResultadoColor = @ResultadoColor,
                    OtrosResultados = @OtrosResultados,
                    CumpleNormas = @CumpleNormas,
                    ComentariosFinales = @ComentariosFinales,
                    UsuarioModificacion = @UsuarioModificacion,
                    FechaModificacion = @FechaModificacion
                WHERE RevisionId = @RevisionId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@RevisionId", revisionId },
                { "@FechaRevision", fechaRevision.HasValue ? (object)fechaRevision.Value : DBNull.Value },
                { "@ResultadoVisual", string.IsNullOrEmpty(resultadoVisual) ? DBNull.Value : (object)resultadoVisual },
                { "@ResultadoOlfativo", string.IsNullOrEmpty(resultadoOlfativo) ? DBNull.Value : (object)resultadoOlfativo },
                { "@ResultadoGustativo", string.IsNullOrEmpty(resultadoGustativo) ? DBNull.Value : (object)resultadoGustativo },
                { "@ResultadoTextura", string.IsNullOrEmpty(resultadoTextura) ? DBNull.Value : (object)resultadoTextura },
                { "@ResultadoColor", string.IsNullOrEmpty(resultadoColor) ? DBNull.Value : (object)resultadoColor },
                { "@OtrosResultados", string.IsNullOrEmpty(otrosResultados) ? DBNull.Value : (object)otrosResultados },
                { "@CumpleNormas", cumpleNormas },
                { "@ComentariosFinales", string.IsNullOrEmpty(comentariosFinales) ? DBNull.Value : (object)comentariosFinales },
                { "@UsuarioModificacion", usuarioId },
                { "@FechaModificacion", DateTime.Now }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Obtener número de lote para el historial
            var revision = _queryBuilder
                .Select("NumeroLote")
                .From("RevisionesLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            string numeroLote = revision.Rows[0]["NumeroLote"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "ActualizarResultados", "ResultadosRevisionLotes", $"Resultados para revisión de lote {numeroLote}");
        }

        public void AgregarDocumentoRevision(
            int revisionId,
            string tipoDocumento,
            string descripcion,
            string nombreOriginal,
            string nombreUnico,
            string rutaArchivo,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO RevisionLotesDocumentos (
                    RevisionId,
                    TipoDocumento,
                    Descripcion,
                    NombreOriginal,
                    NombreUnico,
                    RutaArchivo,
                    FechaSubida,
                    UsuarioSubida
                ) VALUES (
                    @RevisionId,
                    @TipoDocumento,
                    @Descripcion,
                    @NombreOriginal,
                    @NombreUnico,
                    @RutaArchivo,
                    @FechaSubida,
                    @UsuarioSubida
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@RevisionId", revisionId },
                { "@TipoDocumento", tipoDocumento },
                { "@Descripcion", string.IsNullOrEmpty(descripcion) ? DBNull.Value : (object)descripcion },
                { "@NombreOriginal", nombreOriginal },
                { "@NombreUnico", nombreUnico },
                { "@RutaArchivo", rutaArchivo },
                { "@FechaSubida", DateTime.Now },
                { "@UsuarioSubida", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Obtener número de lote para el historial
            var revision = _queryBuilder
                .Select("NumeroLote")
                .From("RevisionesLotes")
                .Where(true, "RevisionId", "=", revisionId)
                .Execute();

            string numeroLote = revision.Rows[0]["NumeroLote"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "SubirDocumento", "RevisionLotesDocumentos", $"Documento {tipoDocumento} para revisión de lote {numeroLote}");
        }

        public void EliminarDocumentoRevision(int documentoId, int usuarioId, bool registrarHistorial = true)
        {
            // Obtener información del documento para el historial y eliminar archivo físico
            DataTable docInfo = ObtenerDocumentoPorId(documentoId);

            if (docInfo.Rows.Count == 0)
            {
                throw new Exception("El documento no existe.");
            }

            int revisionId = Convert.ToInt32(docInfo.Rows[0]["RevisionId"]);
            string tipoDocumento = docInfo.Rows[0]["TipoDocumento"].ToString();
            string rutaArchivo = docInfo.Rows[0]["RutaArchivo"].ToString();

            // Eliminar el archivo físico si existe
            if (File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }

            // Eliminar el registro de la base de datos
            string sql = "DELETE FROM RevisionLotesDocumentos WHERE DocumentoId = @DocumentoId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@DocumentoId", documentoId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            if (registrarHistorial)
            {
                // Obtener número de lote para el historial
                var revision = _queryBuilder
                    .Select("NumeroLote")
                    .From("RevisionesLotes")
                    .Where(true, "RevisionId", "=", revisionId)
                    .Execute();

                string numeroLote = revision.Rows[0]["NumeroLote"].ToString();

                RegistrarAccionHistorial(usuarioId, "EliminarDocumento", "RevisionLotesDocumentos", $"Documento {tipoDocumento} de revisión de lote {numeroLote}");
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
}