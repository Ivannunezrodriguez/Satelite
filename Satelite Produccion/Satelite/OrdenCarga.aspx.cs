using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace Satelite
{
    public partial class OrdenCarga : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private OrdenCargaService _ordenCargaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _ordenCargaService = new OrdenCargaService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Cargar listas de selección
                CargarClientes();
                CargarTransportistas();
                CargarProductos();

                // Si hay un ID en QueryString, es edición
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int ordenId = Convert.ToInt32(Request.QueryString["id"]);
                    CargarOrdenExistente(ordenId);
                }
                else
                {
                    // Nueva orden, establecer valores predeterminados
                    EstablecerValoresPredeterminados();
                }

                // Cargar listado de órdenes existentes
                CargarOrdenesExistentes();
            }
        }

        protected void CargarClientes()
        {
            DataTable clientes = _ordenCargaService.ObtenerClientes();

            ddlCliente.DataSource = clientes;
            ddlCliente.DataTextField = "NombreCliente";
            ddlCliente.DataValueField = "ClienteId";
            ddlCliente.DataBind();

            // Agregar opción por defecto
            ddlCliente.Items.Insert(0, new ListItem("-- Seleccione un cliente --", "0"));
        }

        protected void CargarTransportistas()
        {
            DataTable transportistas = _ordenCargaService.ObtenerTransportistas();

            ddlTransportista.DataSource = transportistas;
            ddlTransportista.DataTextField = "NombreTransportista";
            ddlTransportista.DataValueField = "TransportistaId";
            ddlTransportista.DataBind();

            // Agregar opción por defecto
            ddlTransportista.Items.Insert(0, new ListItem("-- Seleccione un transportista --", "0"));
        }

        protected void CargarProductos()
        {
            DataTable productos = _ordenCargaService.ObtenerProductos();

            ddlProducto.DataSource = productos;
            ddlProducto.DataTextField = "NombreProducto";
            ddlProducto.DataValueField = "ProductoId";
            ddlProducto.DataBind();

            // Agregar opción por defecto
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione un producto --", "0"));
        }

        protected void CargarOrdenesExistentes()
        {
            // Cargar órdenes pendientes y en proceso
            DataTable ordenes = _ordenCargaService.ObtenerOrdenesCarga();

            gvOrdenes.DataSource = ordenes;
            gvOrdenes.DataBind();

            // Mostrar mensaje si no hay órdenes
            lblSinOrdenes.Visible = (ordenes.Rows.Count == 0);
        }

        protected void CargarOrdenExistente(int ordenId)
        {
            // Cargar datos de la orden existente
            DataTable orden = _ordenCargaService.ObtenerOrdenCargaPorId(ordenId);

            if (orden.Rows.Count > 0)
            {
                DataRow row = orden.Rows[0];

                // Establecer valores en los controles
                ddlCliente.SelectedValue = row["ClienteId"].ToString();
                CargarDireccionesCliente(Convert.ToInt32(row["ClienteId"]));
                ddlDireccionEntrega.SelectedValue = row["DireccionEntregaId"].ToString();

                txtNumeroReferencia.Text = row["NumeroReferencia"].ToString();
                txtFechaEntrega.Text = Convert.ToDateTime(row["FechaEntrega"]).ToString("yyyy-MM-dd");

                if (row["HoraEntregaInicio"] != DBNull.Value)
                {
                    txtHoraEntregaInicio.Text = Convert.ToDateTime(row["HoraEntregaInicio"]).ToString("HH:mm");
                }

                if (row["HoraEntregaFin"] != DBNull.Value)
                {
                    txtHoraEntregaFin.Text = Convert.ToDateTime(row["HoraEntregaFin"]).ToString("HH:mm");
                }

                txtInstrucciones.Text = row["InstruccionesEspeciales"].ToString();

                if (row["TransportistaId"] != DBNull.Value)
                {
                    ddlTransportista.SelectedValue = row["TransportistaId"].ToString();
                }

                txtMatricula.Text = row["Matricula"].ToString();
                txtConductor.Text = row["Conductor"].ToString();
                txtTelefono.Text = row["TelefonoConductor"].ToString();

                // Cargar detalles de productos
                CargarDetallesOrden(ordenId);

                // Establecer estado de la orden
                string estado = row["Estado"].ToString();
                ddlEstado.SelectedValue = estado;

                // Controlar visibilidad de controles según el estado
                ConfigurarControlesPorEstado(estado);

                // Guardar ID de la orden para actualización
                ViewState["OrdenId"] = ordenId;

                // Cambiar el título y el texto del botón
                lblTitulo.Text = "Editar Orden de Carga";
                btnGuardar.Text = "Actualizar";
            }
            else
            {
                // No se encontró la orden, redirigir a nueva orden
                Response.Redirect("OrdenCarga.aspx");
            }
        }

        protected void CargarDetallesOrden(int ordenId)
        {
            // Cargar los detalles de productos de la orden
            DataTable detalles = _ordenCargaService.ObtenerDetallesOrden(ordenId);

            // Vincular a la tabla de detalles
            gvDetalles.DataSource = detalles;
            gvDetalles.DataBind();

            // Recalcular totales
            CalcularTotales();
        }

        protected void EstablecerValoresPredeterminados()
        {
            // Fecha de entrega: mañana por defecto
            txtFechaEntrega.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            // Hora de entrega: 9:00 - 14:00 por defecto
            txtHoraEntregaInicio.Text = "09:00";
            txtHoraEntregaFin.Text = "14:00";

            // Estado inicial: Pendiente
            ddlEstado.SelectedValue = "Pendiente";

            // Limpiar ViewState
            ViewState["OrdenId"] = null;

            // Establecer título y texto del botón
            lblTitulo.Text = "Nueva Orden de Carga";
            btnGuardar.Text = "Guardar";

            // Configurar controles por estado
            ConfigurarControlesPorEstado("Pendiente");
        }

        protected void ddlCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCliente.SelectedValue != "0")
            {
                int clienteId = Convert.ToInt32(ddlCliente.SelectedValue);
                CargarDireccionesCliente(clienteId);
            }
            else
            {
                ddlDireccionEntrega.Items.Clear();
                ddlDireccionEntrega.Items.Insert(0, new ListItem("-- Seleccione una dirección --", "0"));
            }
        }

        protected void CargarDireccionesCliente(int clienteId)
        {
            DataTable direcciones = _ordenCargaService.ObtenerDireccionesCliente(clienteId);

            ddlDireccionEntrega.DataSource = direcciones;
            ddlDireccionEntrega.DataTextField = "DireccionCompleta";
            ddlDireccionEntrega.DataValueField = "DireccionId";
            ddlDireccionEntrega.DataBind();

            // Agregar opción por defecto
            ddlDireccionEntrega.Items.Insert(0, new ListItem("-- Seleccione una dirección --", "0"));
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estado = ddlEstado.SelectedValue;
            ConfigurarControlesPorEstado(estado);
        }

        protected void ConfigurarControlesPorEstado(string estado)
        {
            // Configurar la visibilidad y habilitación de controles según el estado
            bool esPendiente = (estado == "Pendiente");
            bool esEnProceso = (estado == "EnProceso");
            bool esCompletada = (estado == "Completada");
            bool esCancelada = (estado == "Cancelada");

            // Controles de cliente y entrega
            ddlCliente.Enabled = esPendiente;
            ddlDireccionEntrega.Enabled = esPendiente;
            txtNumeroReferencia.Enabled = esPendiente;
            txtFechaEntrega.Enabled = esPendiente;
            txtHoraEntregaInicio.Enabled = esPendiente;
            txtHoraEntregaFin.Enabled = esPendiente;
            txtInstrucciones.Enabled = esPendiente;

            // Controles de transportista
            pnlTransportista.Visible = esEnProceso || esCompletada;
            ddlTransportista.Enabled = esEnProceso;
            txtMatricula.Enabled = esEnProceso;
            txtConductor.Enabled = esEnProceso;
            txtTelefono.Enabled = esEnProceso;

            // Controles de productos
            pnlAgregarProducto.Enabled = esPendiente;
            btnAgregarProducto.Enabled = esPendiente;

            // Control de estado
            ddlEstado.Enabled = !esCancelada && !esCompletada;

            // Panel de documentos
            pnlDocumentos.Visible = esEnProceso || esCompletada;
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Validar datos del producto
            if (ddlProducto.SelectedValue == "0")
            {
                MostrarMensaje("Por favor seleccione un producto.", false);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text) || !decimal.TryParse(txtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Por favor ingrese una cantidad válida mayor que cero.", false);
                return;
            }

            try
            {
                // Obtener datos del producto
                int productoId = Convert.ToInt32(ddlProducto.SelectedValue);
                string nombreProducto = ddlProducto.SelectedItem.Text;
                decimal cantidadProducto = Convert.ToDecimal(txtCantidad.Text, CultureInfo.InvariantCulture);

                // Si es una orden existente, guardar el detalle en la base de datos
                if (ViewState["OrdenId"] != null)
                {
                    int ordenId = Convert.ToInt32(ViewState["OrdenId"]);
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                    _ordenCargaService.AgregarDetalleOrden(ordenId, productoId, cantidadProducto, usuarioId);

                    // Recargar detalles
                    CargarDetallesOrden(ordenId);
                }
                else
                {
                    // Es una orden nueva, guardar en una tabla temporal en sesión
                    DataTable detallesTemp = ObtenerTablaDetallesTemp();

                    // Verificar si el producto ya existe en la tabla
                    DataRow[] filasExistentes = detallesTemp.Select($"ProductoId = {productoId}");
                    if (filasExistentes.Length > 0)
                    {
                        // Actualizar cantidad
                        filasExistentes[0]["Cantidad"] = Convert.ToDecimal(filasExistentes[0]["Cantidad"]) + cantidadProducto;
                    }
                    else
                    {
                        // Agregar nuevo detalle
                        DataRow newRow = detallesTemp.NewRow();
                        newRow["ProductoId"] = productoId;
                        newRow["NombreProducto"] = nombreProducto;
                        newRow["Cantidad"] = cantidadProducto;
                        detallesTemp.Rows.Add(newRow);
                    }

                    // Guardar la tabla en sesión
                    Session["DetallesOrdenTemp"] = detallesTemp;

                    // Vincular la tabla al grid
                    gvDetalles.DataSource = detallesTemp;
                    gvDetalles.DataBind();
                }

                // Limpiar controles de producto
                ddlProducto.SelectedValue = "0";
                txtCantidad.Text = string.Empty;

                // Calcular totales
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al agregar el producto: " + ex.Message, false);
            }
        }

        protected DataTable ObtenerTablaDetallesTemp()
        {
            // Obtener tabla de detalles temporal de la sesión, o crear una nueva si no existe
            if (Session["DetallesOrdenTemp"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductoId", typeof(int));
                dt.Columns.Add("NombreProducto", typeof(string));
                dt.Columns.Add("Cantidad", typeof(decimal));
                Session["DetallesOrdenTemp"] = dt;
            }

            return (DataTable)Session["DetallesOrdenTemp"];
        }

        protected void CalcularTotales()
        {
            decimal cantidadTotal = 0;
            int numeroProductos = 0;

            // Obtener datos de los detalles
            if (ViewState["OrdenId"] != null)
            {
                // Orden existente, obtener de la base de datos
                int ordenId = Convert.ToInt32(ViewState["OrdenId"]);
                DataTable detalles = _ordenCargaService.ObtenerDetallesOrden(ordenId);

                foreach (DataRow row in detalles.Rows)
                {
                    cantidadTotal += Convert.ToDecimal(row["Cantidad"]);
                    numeroProductos++;
                }
            }
            else
            {
                // Orden nueva, obtener de la tabla temporal
                DataTable detallesTemp = ObtenerTablaDetallesTemp();

                foreach (DataRow row in detallesTemp.Rows)
                {
                    cantidadTotal += Convert.ToDecimal(row["Cantidad"]);
                    numeroProductos++;
                }
            }

            // Mostrar totales
            lblTotalProductos.Text = numeroProductos.ToString();
            lblCantidadTotal.Text = cantidadTotal.ToString("N2");
        }

        protected void gvDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EliminarDetalle")
            {
                try
                {
                    if (ViewState["OrdenId"] != null)
                    {
                        // Orden existente, eliminar de la base de datos
                        int ordenId = Convert.ToInt32(ViewState["OrdenId"]);
                        int detalleId = Convert.ToInt32(e.CommandArgument);
                        int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                        _ordenCargaService.EliminarDetalleOrden(detalleId, usuarioId);

                        // Recargar detalles
                        CargarDetallesOrden(ordenId);
                    }
                    else
                    {
                        // Orden nueva, eliminar de la tabla temporal
                        int productoId = Convert.ToInt32(e.CommandArgument);
                        DataTable detallesTemp = ObtenerTablaDetallesTemp();

                        DataRow[] filasEliminar = detallesTemp.Select($"ProductoId = {productoId}");
                        if (filasEliminar.Length > 0)
                        {
                            detallesTemp.Rows.Remove(filasEliminar[0]);

                            // Actualizar grid
                            gvDetalles.DataSource = detallesTemp;
                            gvDetalles.DataBind();

                            // Calcular totales
                            CalcularTotales();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar el producto: " + ex.Message, false);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos requeridos
            if (ddlCliente.SelectedValue == "0")
            {
                MostrarMensaje("Por favor seleccione un cliente.", false);
                return;
            }

            if (ddlDireccionEntrega.SelectedValue == "0")
            {
                MostrarMensaje("Por favor seleccione una dirección de entrega.", false);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFechaEntrega.Text))
            {
                MostrarMensaje("Por favor ingrese una fecha de entrega.", false);
                return;
            }

            try
            {
                // Obtener datos comunes
                int clienteId = Convert.ToInt32(ddlCliente.SelectedValue);
                int direccionEntregaId = Convert.ToInt32(ddlDireccionEntrega.SelectedValue);
                string numeroReferencia = txtNumeroReferencia.Text.Trim();
                DateTime fechaEntrega = DateTime.Parse(txtFechaEntrega.Text);

                // Horas de entrega (opcionales)
                DateTime? horaEntregaInicio = null;
                DateTime? horaEntregaFin = null;

                if (!string.IsNullOrWhiteSpace(txtHoraEntregaInicio.Text))
                {
                    TimeSpan horaInicio = TimeSpan.Parse(txtHoraEntregaInicio.Text);
                    horaEntregaInicio = fechaEntrega.Date.Add(horaInicio);
                }

                if (!string.IsNullOrWhiteSpace(txtHoraEntregaFin.Text))
                {
                    TimeSpan horaFin = TimeSpan.Parse(txtHoraEntregaFin.Text);
                    horaEntregaFin = fechaEntrega.Date.Add(horaFin);
                }

                string instrucciones = txtInstrucciones.Text.Trim();
                string estado = ddlEstado.SelectedValue;

                // Datos de transportista (opcionales)
                int? transportistaId = null;
                if (ddlTransportista.SelectedValue != "0")
                {
                    transportistaId = Convert.ToInt32(ddlTransportista.SelectedValue);
                }

                string matricula = txtMatricula.Text.Trim();
                string conductor = txtConductor.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                if (ViewState["OrdenId"] != null)
                {
                    // Actualizar orden existente
                    int ordenId = Convert.ToInt32(ViewState["OrdenId"]);

                    _ordenCargaService.ActualizarOrdenCarga(
                        ordenId,
                        clienteId,
                        direccionEntregaId,
                        numeroReferencia,
                        fechaEntrega,
                        horaEntregaInicio,
                        horaEntregaFin,
                        instrucciones,
                        estado,
                        transportistaId,
                        matricula,
                        conductor,
                        telefono,
                        usuarioId);

                    MostrarMensaje("Orden de carga actualizada exitosamente.", true);
                }
                else
                {
                    // Validar que existan productos
                    DataTable detallesTemp = ObtenerTablaDetallesTemp();
                    if (detallesTemp.Rows.Count == 0)
                    {
                        MostrarMensaje("Debe agregar al menos un producto a la orden.", false);
                        return;
                    }

                    // Crear nueva orden
                    int ordenId = _ordenCargaService.CrearOrdenCarga(
                        clienteId,
                        direccionEntregaId,
                        numeroReferencia,
                        fechaEntrega,
                        horaEntregaInicio,
                        horaEntregaFin,
                        instrucciones,
                        estado,
                        transportistaId,
                        matricula,
                        conductor,
                        telefono,
                        usuarioId);

                    // Agregar detalles de productos
                    foreach (DataRow row in detallesTemp.Rows)
                    {
                        int productoId = Convert.ToInt32(row["ProductoId"]);
                        decimal cantidad = Convert.ToDecimal(row["Cantidad"]);

                        _ordenCargaService.AgregarDetalleOrden(ordenId, productoId, cantidad, usuarioId);
                    }

                    // Limpiar tabla temporal
                    Session["DetallesOrdenTemp"] = null;

                    MostrarMensaje("Orden de carga creada exitosamente.", true);

                    // Redirigir a edición de la orden creada
                    Response.Redirect($"OrdenCarga.aspx?id={ordenId}");
                }

                // Recargar listado de órdenes
                CargarOrdenesExistentes();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar la orden: " + ex.Message, false);
            }
        }

        protected void btnNueva_Click(object sender, EventArgs e)
        {
            // Limpiar tabla temporal y redirigir a nueva orden
            Session["DetallesOrdenTemp"] = null;
            Response.Redirect("OrdenCarga.aspx");
        }

        protected void gvOrdenes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarOrden")
            {
                int ordenId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"OrdenCarga.aspx?id={ordenId}");
            }
            else if (e.CommandName == "ImprimirOrden")
            {
                int ordenId = Convert.ToInt32(e.CommandArgument);
                // Implementar la lógica para generar e imprimir la orden
                // Aquí puedes redirigir a una página de impresión o abrir un PDF
                Response.Redirect($"ImprimirOrdenCarga.aspx?id={ordenId}");
            }
            else if (e.CommandName == "EliminarOrden")
            {
                try
                {
                    int ordenId = Convert.ToInt32(e.CommandArgument);
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                    // Verificar que la orden esté en estado pendiente
                    string estado = _ordenCargaService.ObtenerEstadoOrden(ordenId);
                    if (estado != "Pendiente")
                    {
                        MostrarMensaje("Solo se pueden eliminar órdenes en estado Pendiente.", false);
                        return;
                    }

                    // Eliminar la orden
                    _ordenCargaService.EliminarOrdenCarga(ordenId, usuarioId);

                    MostrarMensaje("Orden de carga eliminada exitosamente.", true);

                    // Recargar listado de órdenes
                    CargarOrdenesExistentes();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar la orden: " + ex.Message, false);
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

            // Verificar que exista ID de orden
            if (ViewState["OrdenId"] == null)
            {
                MostrarMensaje("Debe guardar la orden antes de subir documentos.", false);
                return;
            }

            try
            {
                // Obtener datos del archivo
                string fileName = Path.GetFileName(fileUpload.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Validar la extensión del archivo
                string[] extensionesPermitidas = { ".pdf", ".doc", ".docx", ".jpg", ".png" };
                if (!extensionesPermitidas.Contains(fileExtension))
                {
                    MostrarMensaje("Tipo de archivo no permitido. Use PDF, Word o imágenes.", false);
                    return;
                }

                // Generar nombre único para el archivo
                string nuevoNombre = $"orden_{ViewState["OrdenId"]}_{Guid.NewGuid()}{fileExtension}";

                // Ruta donde se guardará el archivo
                string rutaArchivos = Server.MapPath("~/Archivos/OrdenesCarga/");

                // Verificar si existe el directorio y si no, crearlo
                if (!Directory.Exists(rutaArchivos))
                {
                    Directory.CreateDirectory(rutaArchivos);
                }

                // Guardar el archivo físicamente
                string rutaCompleta = Path.Combine(rutaArchivos, nuevoNombre);
                fileUpload.SaveAs(rutaCompleta);

                // Registrar el documento en la base de datos
                int ordenId = Convert.ToInt32(ViewState["OrdenId"]);
                string tipoDocumento = ddlTipoDocumento.SelectedValue;
                string descripcion = txtDescripcionDoc.Text.Trim();
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                _ordenCargaService.AgregarDocumentoOrden(
                    ordenId,
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
                CargarDocumentosOrden(ordenId);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al subir el documento: " + ex.Message, false);
            }
        }

        protected void CargarDocumentosOrden(int ordenId)
        {
            DataTable documentos = _ordenCargaService.ObtenerDocumentosOrden(ordenId);

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
                    int ordenId = Convert.ToInt32(ViewState["OrdenId"]);
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                    _ordenCargaService.EliminarDocumentoOrden(documentoId, usuarioId);

                    MostrarMensaje("Documento eliminado exitosamente.", true);

                    // Recargar documentos
                    CargarDocumentosOrden(ordenId);
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
                DataTable documentoInfo = _ordenCargaService.ObtenerDocumentoPorId(documentoId);

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

    // Clase de servicio para gestión de órdenes de carga
    public class OrdenCargaService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public OrdenCargaService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerClientes()
        {
            return _queryBuilder
                .Select("ClienteId, NombreComercial AS NombreCliente, CIF")
                .From("Clientes")
                .Where(true, "Activo", "=", true)
                .OrderBy("NombreComercial")
                .Execute();
        }

        public DataTable ObtenerDireccionesCliente(int clienteId)
        {
            return _queryBuilder
                .Select("DireccionId, Direccion + ', ' + CodigoPostal + ' ' + Poblacion + ' (' + Provincia + ')' AS DireccionCompleta")
                .From("DireccionesCliente")
                .Where(true, "ClienteId", "=", clienteId)
                .AndWhere(true, "Activo", "=", true)
                .OrderBy("Direccion")
                .Execute();
        }

        public DataTable ObtenerTransportistas()
        {
            return _queryBuilder
                .Select("TransportistaId, NombreComercial AS NombreTransportista, CIF")
                .From("Transportistas")
                .Where(true, "Activo", "=", true)
                .OrderBy("NombreComercial")
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

        public DataTable ObtenerOrdenesCarga()
        {
            return _queryBuilder
                .Select("o.OrdenId, o.NumeroReferencia, c.NombreComercial AS Cliente, " +
                        "o.FechaEntrega, o.Estado, " +
                        "(SELECT COUNT(*) FROM OrdenCargaDetalles WHERE OrdenId = o.OrdenId) AS NumProductos, " +
                        "o.FechaCreacion, u.NombreCompleto AS UsuarioCreador")
                .From("OrdenesCarga AS o")
                .Join("Clientes AS c", "o.ClienteId = c.ClienteId")
                .Join("Usuarios AS u", "o.UsuarioCreacion = u.UsuarioId")
                .Where(true, "o.Estado", "IN", "'Pendiente', 'EnProceso'")
                .OrderBy("o.FechaEntrega DESC, o.FechaCreacion DESC")
                .Execute();
        }

        public DataTable ObtenerOrdenCargaPorId(int ordenId)
        {
            return _queryBuilder
                .Select("o.OrdenId, o.ClienteId, o.DireccionEntregaId, o.NumeroReferencia, " +
                        "o.FechaEntrega, o.HoraEntregaInicio, o.HoraEntregaFin, " +
                        "o.InstruccionesEspeciales, o.Estado, o.TransportistaId, " +
                        "o.Matricula, o.Conductor, o.TelefonoConductor, " +
                        "o.FechaCreacion, o.UsuarioCreacion")
                .From("OrdenesCarga AS o")
                .Where(true, "o.OrdenId", "=", ordenId)
                .Execute();
        }

        public DataTable ObtenerDetallesOrden(int ordenId)
        {
            return _queryBuilder
                .Select("d.DetalleId, d.ProductoId, p.Codigo, p.Nombre AS NombreProducto, " +
                        "d.Cantidad, p.Unidad")
                .From("OrdenCargaDetalles AS d")
                .Join("Productos AS p", "d.ProductoId = p.ProductoId")
                .Where(true, "d.OrdenId", "=", ordenId)
                .OrderBy("p.Codigo")
                .Execute();
        }

        public DataTable ObtenerDocumentosOrden(int ordenId)
        {
            return _queryBuilder
                .Select("d.DocumentoId, d.TipoDocumento, d.Descripcion, d.NombreOriginal, " +
                        "d.FechaSubida, u.NombreCompleto AS UsuarioSubida")
                .From("OrdenCargaDocumentos AS d")
                .Join("Usuarios AS u", "d.UsuarioSubida = u.UsuarioId")
                .Where(true, "d.OrdenId", "=", ordenId)
                .OrderBy("d.FechaSubida DESC")
                .Execute();
        }

        public DataTable ObtenerDocumentoPorId(int documentoId)
        {
            return _queryBuilder
                .Select("DocumentoId, OrdenId, TipoDocumento, Descripcion, " +
                        "NombreOriginal, NombreUnico, RutaArchivo")
                .From("OrdenCargaDocumentos")
                .Where(true, "DocumentoId", "=", documentoId)
                .Execute();
        }

        public string ObtenerEstadoOrden(int ordenId)
        {
            var resultado = _queryBuilder
                .Select("Estado")
                .From("OrdenesCarga")
                .Where(true, "OrdenId", "=", ordenId)
                .Execute();

            if (resultado.Rows.Count > 0)
            {
                return resultado.Rows[0]["Estado"].ToString();
            }

            return string.Empty;
        }

        public int CrearOrdenCarga(
            int clienteId,
            int direccionEntregaId,
            string numeroReferencia,
            DateTime fechaEntrega,
            DateTime? horaEntregaInicio,
            DateTime? horaEntregaFin,
            string instruccionesEspeciales,
            string estado,
            int? transportistaId,
            string matricula,
            string conductor,
            string telefonoConductor,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO OrdenesCarga (
                    ClienteId,
                    DireccionEntregaId,
                    NumeroReferencia,
                    FechaEntrega,
                    HoraEntregaInicio,
                    HoraEntregaFin,
                    InstruccionesEspeciales,
                    Estado,
                    TransportistaId,
                    Matricula,
                    Conductor,
                    TelefonoConductor,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @ClienteId,
                    @DireccionEntregaId,
                    @NumeroReferencia,
                    @FechaEntrega,
                    @HoraEntregaInicio,
                    @HoraEntregaFin,
                    @InstruccionesEspeciales,
                    @Estado,
                    @TransportistaId,
                    @Matricula,
                    @Conductor,
                    @TelefonoConductor,
                    @FechaCreacion,
                    @UsuarioCreacion
                ); SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@ClienteId", clienteId },
                { "@DireccionEntregaId", direccionEntregaId },
                { "@NumeroReferencia", numeroReferencia },
                { "@FechaEntrega", fechaEntrega },
                { "@HoraEntregaInicio", horaEntregaInicio.HasValue ? (object)horaEntregaInicio.Value : DBNull.Value },
                { "@HoraEntregaFin", horaEntregaFin.HasValue ? (object)horaEntregaFin.Value : DBNull.Value },
                { "@InstruccionesEspeciales", instruccionesEspeciales },
                { "@Estado", estado },
                { "@TransportistaId", transportistaId.HasValue ? (object)transportistaId.Value : DBNull.Value },
                { "@Matricula", matricula },
                { "@Conductor", conductor },
                { "@TelefonoConductor", telefonoConductor },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioId }
            };

            // Ejecutar la inserción y obtener el ID generado
            object result = _dbConnection.ExecuteScalar(sql, parameters);
            int ordenId = Convert.ToInt32(result);

            // Obtener nombre del cliente para el historial
            var cliente = _queryBuilder
                .Select("NombreComercial")
                .From("Clientes")
                .Where(true, "ClienteId", "=", clienteId)
                .Execute();

            string nombreCliente = cliente.Rows[0]["NombreComercial"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "OrdenesCarga", $"Orden para {nombreCliente} con fecha de entrega {fechaEntrega.ToShortDateString()}");

            return ordenId;
        }

        public void ActualizarOrdenCarga(
            int ordenId,
            int clienteId,
            int direccionEntregaId,
            string numeroReferencia,
            DateTime fechaEntrega,
            DateTime? horaEntregaInicio,
            DateTime? horaEntregaFin,
            string instruccionesEspeciales,
            string estado,
            int? transportistaId,
            string matricula,
            string conductor,
            string telefonoConductor,
            int usuarioId)
        {
            // Obtener el estado actual para verificar cambios
            string estadoAnterior = ObtenerEstadoOrden(ordenId);

            string sql = @"
                UPDATE OrdenesCarga SET
                    ClienteId = @ClienteId,
                    DireccionEntregaId = @DireccionEntregaId,
                    NumeroReferencia = @NumeroReferencia,
                    FechaEntrega = @FechaEntrega,
                    HoraEntregaInicio = @HoraEntregaInicio,
                    HoraEntregaFin = @HoraEntregaFin,
                    InstruccionesEspeciales = @InstruccionesEspeciales,
                    Estado = @Estado,
                    TransportistaId = @TransportistaId,
                    Matricula = @Matricula,
                    Conductor = @Conductor,
                    TelefonoConductor = @TelefonoConductor,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE OrdenId = @OrdenId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@OrdenId", ordenId },
                { "@ClienteId", clienteId },
                { "@DireccionEntregaId", direccionEntregaId },
                { "@NumeroReferencia", numeroReferencia },
                { "@FechaEntrega", fechaEntrega },
                { "@HoraEntregaInicio", horaEntregaInicio.HasValue ? (object)horaEntregaInicio.Value : DBNull.Value },
                { "@HoraEntregaFin", horaEntregaFin.HasValue ? (object)horaEntregaFin.Value : DBNull.Value },
                { "@InstruccionesEspeciales", instruccionesEspeciales },
                { "@Estado", estado },
                { "@TransportistaId", transportistaId.HasValue ? (object)transportistaId.Value : DBNull.Value },
                { "@Matricula", matricula },
                { "@Conductor", conductor },
                { "@TelefonoConductor", telefonoConductor },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar cambio de estado si aplica
            if (estadoAnterior != estado)
            {
                RegistrarAccionHistorial(usuarioId, "CambioEstado", "OrdenesCarga", $"Orden {ordenId}: de {estadoAnterior} a {estado}");
            }
            else
            {
                RegistrarAccionHistorial(usuarioId, "Actualizar", "OrdenesCarga", $"Orden {ordenId}");
            }
        }

        public void EliminarOrdenCarga(int ordenId, int usuarioId)
        {
            // Primero obtener información para el historial
            var orden = _queryBuilder
                .Select("o.NumeroReferencia, c.NombreComercial")
                .From("OrdenesCarga AS o")
                .Join("Clientes AS c", "o.ClienteId = c.ClienteId")
                .Where(true, "o.OrdenId", "=", ordenId)
                .Execute();

            if (orden.Rows.Count == 0)
            {
                throw new Exception("La orden no existe.");
            }

            string refOrden = orden.Rows[0]["NumeroReferencia"].ToString();
            string nombreCliente = orden.Rows[0]["NombreComercial"].ToString();

            // Eliminar documentos relacionados
            DataTable documentos = ObtenerDocumentosOrden(ordenId);
            foreach (DataRow doc in documentos.Rows)
            {
                int documentoId = Convert.ToInt32(doc["DocumentoId"]);
                EliminarDocumentoOrden(documentoId, usuarioId, false); // Sin registrar en historial individualmente
            }

            // Eliminar detalles
            string sqlDetalles = "DELETE FROM OrdenCargaDetalles WHERE OrdenId = @OrdenId";
            Dictionary<string, object> parametersDetalles = new Dictionary<string, object>
            {
                { "@OrdenId", ordenId }
            };

            _dbConnection.ExecuteNonQuery(sqlDetalles, parametersDetalles);

            // Eliminar la orden
            string sqlOrden = "DELETE FROM OrdenesCarga WHERE OrdenId = @OrdenId";
            Dictionary<string, object> parametersOrden = new Dictionary<string, object>
            {
                { "@OrdenId", ordenId }
            };

            _dbConnection.ExecuteNonQuery(sqlOrden, parametersOrden);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "OrdenesCarga", $"Orden {refOrden} para {nombreCliente}");
        }

        public void AgregarDetalleOrden(int ordenId, int productoId, decimal cantidad, int usuarioId)
        {
            // Verificar si ya existe un detalle para este producto en esta orden
            var detalleExistente = _queryBuilder
                .Select("DetalleId, Cantidad")
                .From("OrdenCargaDetalles")
                .Where(true, "OrdenId", "=", ordenId)
                .AndWhere(true, "ProductoId", "=", productoId)
                .Execute();

            if (detalleExistente.Rows.Count > 0)
            {
                // Actualizar la cantidad
                int detalleId = Convert.ToInt32(detalleExistente.Rows[0]["DetalleId"]);
                decimal cantidadActual = Convert.ToDecimal(detalleExistente.Rows[0]["Cantidad"]);
                decimal nuevaCantidad = cantidadActual + cantidad;

                string sql = @"
                    UPDATE OrdenCargaDetalles SET
                        Cantidad = @Cantidad,
                        FechaModificacion = @FechaModificacion,
                        UsuarioModificacion = @UsuarioModificacion
                    WHERE DetalleId = @DetalleId";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@DetalleId", detalleId },
                    { "@Cantidad", nuevaCantidad },
                    { "@FechaModificacion", DateTime.Now },
                    { "@UsuarioModificacion", usuarioId }
                };

                _dbConnection.ExecuteNonQuery(sql, parameters);
            }
            else
            {
                // Insertar nuevo detalle
                string sql = @"
                    INSERT INTO OrdenCargaDetalles (
                        OrdenId,
                        ProductoId,
                        Cantidad,
                        FechaCreacion,
                        UsuarioCreacion
                    ) VALUES (
                        @OrdenId,
                        @ProductoId,
                        @Cantidad,
                        @FechaCreacion,
                        @UsuarioCreacion
                    )";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@OrdenId", ordenId },
                    { "@ProductoId", productoId },
                    { "@Cantidad", cantidad },
                    { "@FechaCreacion", DateTime.Now },
                    { "@UsuarioCreacion", usuarioId }
                };

                _dbConnection.ExecuteNonQuery(sql, parameters);
            }

            // Obtener información del producto para el historial
            var producto = _queryBuilder
                .Select("Codigo, Nombre")
                .From("Productos")
                .Where(true, "ProductoId", "=", productoId)
                .Execute();

            string codigoProducto = producto.Rows[0]["Codigo"].ToString();
            string nombreProducto = producto.Rows[0]["Nombre"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "AgregarProducto", "OrdenCargaDetalles", $"Producto {codigoProducto} - {nombreProducto} a Orden {ordenId}");
        }

        public void EliminarDetalleOrden(int detalleId, int usuarioId)
        {
            // Obtener información para el historial
            var detalle = _queryBuilder
                .Select("d.OrdenId, d.ProductoId, p.Codigo, p.Nombre")
                .From("OrdenCargaDetalles AS d")
                .Join("Productos AS p", "d.ProductoId = p.ProductoId")
                .Where(true, "d.DetalleId", "=", detalleId)
                .Execute();

            if (detalle.Rows.Count == 0)
            {
                throw new Exception("El detalle no existe.");
            }

            int ordenId = Convert.ToInt32(detalle.Rows[0]["OrdenId"]);
            string codigoProducto = detalle.Rows[0]["Codigo"].ToString();
            string nombreProducto = detalle.Rows[0]["Nombre"].ToString();

            // Eliminar el detalle
            string sql = "DELETE FROM OrdenCargaDetalles WHERE DetalleId = @DetalleId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@DetalleId", detalleId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "EliminarProducto", "OrdenCargaDetalles", $"Producto {codigoProducto} - {nombreProducto} de Orden {ordenId}");
        }

        public void AgregarDocumentoOrden(
            int ordenId,
            string tipoDocumento,
            string descripcion,
            string nombreOriginal,
            string nombreUnico,
            string rutaArchivo,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO OrdenCargaDocumentos (
                    OrdenId,
                    TipoDocumento,
                    Descripcion,
                    NombreOriginal,
                    NombreUnico,
                    RutaArchivo,
                    FechaSubida,
                    UsuarioSubida
                ) VALUES (
                    @OrdenId,
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
                { "@OrdenId", ordenId },
                { "@TipoDocumento", tipoDocumento },
                { "@Descripcion", descripcion },
                { "@NombreOriginal", nombreOriginal },
                { "@NombreUnico", nombreUnico },
                { "@RutaArchivo", rutaArchivo },
                { "@FechaSubida", DateTime.Now },
                { "@UsuarioSubida", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "SubirDocumento", "OrdenCargaDocumentos", $"Documento {tipoDocumento} para Orden {ordenId}");
        }

        public void EliminarDocumentoOrden(int documentoId, int usuarioId, bool registrarHistorial = true)
        {
            // Obtener información del documento para el historial y eliminar archivo físico
            DataTable docInfo = ObtenerDocumentoPorId(documentoId);

            if (docInfo.Rows.Count == 0)
            {
                throw new Exception("El documento no existe.");
            }

            int ordenId = Convert.ToInt32(docInfo.Rows[0]["OrdenId"]);
            string tipoDocumento = docInfo.Rows[0]["TipoDocumento"].ToString();
            string rutaArchivo = docInfo.Rows[0]["RutaArchivo"].ToString();

            // Eliminar el archivo físico si existe
            if (File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }

            // Eliminar el registro de la base de datos
            string sql = "DELETE FROM OrdenCargaDocumentos WHERE DocumentoId = @DocumentoId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@DocumentoId", documentoId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            if (registrarHistorial)
            {
                RegistrarAccionHistorial(usuarioId, "EliminarDocumento", "OrdenCargaDocumentos", $"Documento {tipoDocumento} de Orden {ordenId}");
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