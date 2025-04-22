using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Configuration;

namespace Satelite
{
    #region Interfaces base para el Fluent API
    public interface IQueryable<T>
    {
        IFilterable<T> WithDateRange(string startDate, string endDate);
    }

    public interface IFilterable<T>
    {
        IExecutable<T> WithFilters(string status, string searchTerm = "");
        IExecutable<T> WithAdvancedFilters(Dictionary<string, string> filters);
    }

    public interface IExecutable<T>
    {
        T Execute();
        T ExportToExcel();
    }
    #endregion

    #region Core del Fluent API
    public class SateliteFluentCore
    {
        protected ConexionSql conexion;
        protected string fechaInicio;
        protected string fechaFin;
        protected Dictionary<string, string> filtros;
        protected Page currentPage;

        public SateliteFluentCore()
        {
            conexion = new ConexionSql();
            filtros = new Dictionary<string, string>();
        }

        protected void DisplayMessage(string message)
        {
            if (currentPage != null)
            {
                // Buscar el control lblMensaje en la página
                Label lblMessage = currentPage.FindControl("lblMensaje") as Label;
                if (lblMessage != null)
                {
                    lblMessage.Text = message;
                    lblMessage.Visible = true;
                }
                else
                {
                    // Alternativa usando ScriptManager
                    ScriptManager.RegisterStartupScript(currentPage, currentPage.GetType(), 
                        "alertMessage", $"alert('{message}');", true);
                }
            }
        }

        protected DataTable ExecuteQuery(string query)
        {
            try
            {
                return conexion.Consulta(query);
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al ejecutar consulta: {ex.Message}");
                return null;
            }
        }

        protected void ExportDataTableToExcel(DataTable dt, string fileName)
        {
            if (currentPage != null && dt != null && dt.Rows.Count > 0)
            {
                HttpResponse response = currentPage.Response;
                response.Clear();
                response.Buffer = true;
                response.AddHeader("content-disposition", $"attachment;filename={fileName}_{DateTime.Now.ToString("ddMMMyyyy")}.xls");
                response.Charset = "";
                response.ContentType = "application/vnd.ms-excel";

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        Table table = new Table();
                        table.BorderWidth = 1;
                        table.BorderStyle = BorderStyle.Solid;
                        table.GridLines = GridLines.Both;
                        
                        // Encabezados
                        TableRow headerRow = new TableRow();
                        headerRow.BackColor = Color.LightGray;
                        headerRow.Font.Bold = true;
                        
                        foreach (DataColumn column in dt.Columns)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = column.ColumnName;
                            headerRow.Cells.Add(cell);
                        }
                        table.Rows.Add(headerRow);
                        
                        // Datos
                        foreach (DataRow row in dt.Rows)
                        {
                            TableRow dataRow = new TableRow();
                            foreach (DataColumn column in dt.Columns)
                            {
                                TableCell cell = new TableCell();
                                cell.Text = row[column].ToString();
                                dataRow.Cells.Add(cell);
                            }
                            table.Rows.Add(dataRow);
                        }
                        
                        table.RenderControl(hw);
                        response.Write(sw.ToString());
                    }
                }
                response.End();
            }
        }

        protected void BindGridView(GridView gridView, DataTable dt, Label lblCount = null)
        {
            if (gridView != null && dt != null)
            {
                gridView.DataSource = dt;
                gridView.DataBind();
                
                if (lblCount != null)
                {
                    lblCount.Text = $"Registros encontrados: {dt.Rows.Count}";
                }
            }
        }
    }
    #endregion

    #region Fluent API para Consulta
    public class ConsultaQueryBuilder : SateliteFluentCore, 
                                       IQueryable<ConsultaResult>,
                                       IFilterable<ConsultaResult>,
                                       IExecutable<ConsultaResult>
    {
        private string statusFilter;
        private string searchTerm;

        public ConsultaQueryBuilder(Page page)
        {
            this.currentPage = page;
        }

        public IFilterable<ConsultaResult> WithDateRange(string startDate, string endDate)
        {
            this.fechaInicio = startDate;
            this.fechaFin = endDate;
            return this;
        }

        public IExecutable<ConsultaResult> WithFilters(string status, string searchTerm = "")
        {
            this.statusFilter = status;
            this.searchTerm = searchTerm;
            return this;
        }

        public IExecutable<ConsultaResult> WithAdvancedFilters(Dictionary<string, string> filters)
        {
            this.filtros = filters;
            return this;
        }

        public ConsultaResult Execute()
        {
            // Validar fechas
            if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
            {
                DisplayMessage("Por favor ingrese un rango de fechas válido");
                return new ConsultaResult { Success = false };
            }

            // Construir consulta SQL
            string query = BuildQuery();
            
            // Ejecutar consulta
            DataTable results = ExecuteQuery(query);
            
            // Preparar resultado
            ConsultaResult result = new ConsultaResult
            {
                Data = results,
                Success = results != null && results.Rows.Count > 0,
                Message = results != null && results.Rows.Count > 0 
                    ? $"Registros encontrados: {results.Rows.Count}" 
                    : "No se encontraron registros con los filtros seleccionados"
            };

            // Actualizar UI si es necesario
            UpdateUI(result);
            
            return result;
        }

        public ConsultaResult ExportToExcel()
        {
            // Validar fechas
            if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
            {
                DisplayMessage("Por favor ingrese un rango de fechas válido para exportar");
                return new ConsultaResult { Success = false };
            }

            // Construir consulta SQL
            string query = BuildQuery();
            
            // Ejecutar consulta
            DataTable results = ExecuteQuery(query);
            
            if (results != null && results.Rows.Count > 0)
            {
                // Exportar a Excel
                ExportDataTableToExcel(results, "Consulta");
                
                return new ConsultaResult
                {
                    Data = results,
                    Success = true,
                    Message = "Datos exportados exitosamente"
                };
            }
            else
            {
                DisplayMessage("No hay datos para exportar");
                return new ConsultaResult
                {
                    Success = false,
                    Message = "No hay datos para exportar"
                };
            }
        }

        private string BuildQuery()
        {
            string query = $"SELECT * FROM dbo.CapturaProduccion WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFin}'";
            
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "0")
            {
                query += $" AND Status = '{statusFilter}'";
            }
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query += $" AND (Folio LIKE '%{searchTerm}%' OR Nombre LIKE '%{searchTerm}%')";
            }
            
            // Añadir filtros adicionales
            foreach (var filtro in filtros)
            {
                if (!string.IsNullOrEmpty(filtro.Value))
                {
                    query += $" AND {filtro.Key} = '{filtro.Value}'";
                }
            }
            
            query += " ORDER BY Fecha DESC";
            
            return query;
        }

        private void UpdateUI(ConsultaResult result)
        {
            GridView gvConsulta = currentPage.FindControl("gvConsulta") as GridView;
            Label lblRegistros = currentPage.FindControl("lblRegistros") as Label;
            
            if (gvConsulta != null)
            {
                if (result.Success && result.Data != null)
                {
                    BindGridView(gvConsulta, result.Data, lblRegistros);
                }
                else
                {
                    // Crear tabla vacía para mostrar
                    DataTable emptyTable = CreateEmptyConsultaTable();
                    BindGridView(gvConsulta, emptyTable, lblRegistros);
                    DisplayMessage(result.Message);
                }
            }
        }

        private DataTable CreateEmptyConsultaTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Folio");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Status");
            dt.Columns.Add("Usuario");
            return dt;
        }

        public void InitializePage()
        {
            try
            {
                // Cargar dropdown de Status
                DropDownList ddlStatus = currentPage.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    ddlStatus.Items.Clear();
                    ddlStatus.Items.Add(new ListItem("Todos", "0"));
                    
                    string query = "SELECT DISTINCT Status FROM dbo.CapturaProduccion ORDER BY Status";
                    DataTable statuses = ExecuteQuery(query);
                    
                    if (statuses != null && statuses.Rows.Count > 0)
                    {
                        foreach (DataRow row in statuses.Rows)
                        {
                            string status = row["Status"].ToString();
                            ddlStatus.Items.Add(new ListItem(status, status));
                        }
                    }
                }
                
                // Inicializar tabla vacía
                GridView gvConsulta = currentPage.FindControl("gvConsulta") as GridView;
                Label lblRegistros = currentPage.FindControl("lblRegistros") as Label;
                
                if (gvConsulta != null)
                {
                    DataTable emptyTable = CreateEmptyConsultaTable();
                    BindGridView(gvConsulta, emptyTable, lblRegistros);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al inicializar página: {ex.Message}");
            }
        }
    }

    public class ConsultaResult
    {
        public DataTable Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    #endregion

    #region Fluent API para Captura
    public class CapturaBuilder : SateliteFluentCore
    {
        private string folio;
        private string cliente;
        private string contacto;
        private string tipo;
        private string status;
        private string descripcion;
        private string usuario;

        public CapturaBuilder(Page page)
        {
            this.currentPage = page;
        }

        public CapturaBuilder WithFolio(string folio)
        {
            this.folio = folio;
            return this;
        }

        public CapturaBuilder WithCliente(string cliente)
        {
            this.cliente = cliente;
            return this;
        }

        public CapturaBuilder WithContacto(string contacto)
        {
            this.contacto = contacto;
            return this;
        }

        public CapturaBuilder WithTipo(string tipo)
        {
            this.tipo = tipo;
            return this;
        }

        public CapturaBuilder WithStatus(string status)
        {
            this.status = status;
            return this;
        }

        public CapturaBuilder WithDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
            return this;
        }

        public CapturaBuilder WithUsuario(string usuario)
        {
            this.usuario = usuario;
            return this;
        }

        public bool Save()
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(folio) || string.IsNullOrEmpty(cliente) || 
                    string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(status))
                {
                    DisplayMessage("Por favor complete todos los campos obligatorios");
                    return false;
                }

                // Crear consulta SQL para insertar o actualizar
                string query = BuildSaveQuery();
                
                // Ejecutar consulta
                DataTable result = ExecuteQuery(query);
                
                // Mostrar mensaje
                DisplayMessage("Registro guardado exitosamente");
                
                return true;
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al guardar: {ex.Message}");
                return false;
            }
        }

        public bool Load(string folioToLoad)
        {
            try
            {
                string query = $"SELECT * FROM dbo.CapturaProduccion WHERE Folio = '{folioToLoad}'";
                DataTable result = ExecuteQuery(query);
                
                if (result != null && result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];
                    
                    // Cargar datos en los controles
                    LoadControlValues(row);
                    
                    return true;
                }
                else
                {
                    DisplayMessage("No se encontró el registro solicitado");
                    return false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al cargar registro: {ex.Message}");
                return false;
            }
        }

        public void InitializePage()
        {
            try
            {
                // Cargar dropdown de Status
                DropDownList ddlStatus = currentPage.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    ddlStatus.Items.Clear();
                    
                    string query = "SELECT DISTINCT Status FROM dbo.CapturaProduccion ORDER BY Status";
                    DataTable statuses = ExecuteQuery(query);
                    
                    if (statuses != null && statuses.Rows.Count > 0)
                    {
                        foreach (DataRow row in statuses.Rows)
                        {
                            string status = row["Status"].ToString();
                            ddlStatus.Items.Add(new ListItem(status, status));
                        }
                    }
                }
                
                // Cargar dropdown de Tipo
                DropDownList ddlTipo = currentPage.FindControl("ddlTipo") as DropDownList;
                if (ddlTipo != null)
                {
                    ddlTipo.Items.Clear();
                    
                    string query = "SELECT DISTINCT Tipo FROM dbo.CapturaProduccion ORDER BY Tipo";
                    DataTable tipos = ExecuteQuery(query);
                    
                    if (tipos != null && tipos.Rows.Count > 0)
                    {
                        foreach (DataRow row in tipos.Rows)
                        {
                            string tipo = row["Tipo"].ToString();
                            ddlTipo.Items.Add(new ListItem(tipo, tipo));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al inicializar página: {ex.Message}");
            }
        }

        private string BuildSaveQuery()
        {
            // Verificar si el folio ya existe
            string checkQuery = $"SELECT COUNT(*) FROM dbo.CapturaProduccion WHERE Folio = '{folio}'";
            DataTable checkResult = ExecuteQuery(checkQuery);
            
            bool exists = false;
            if (checkResult != null && checkResult.Rows.Count > 0)
            {
                exists = Convert.ToInt32(checkResult.Rows[0][0]) > 0;
            }
            
            if (exists)
            {
                // Actualizar
                return $@"UPDATE dbo.CapturaProduccion SET 
                        Cliente = '{cliente}',
                        Contacto = '{contacto}',
                        Tipo = '{tipo}',
                        Status = '{status}',
                        Descripcion = '{descripcion}',
                        FechaModificacion = GETDATE(),
                        Usuario = '{usuario}'
                        WHERE Folio = '{folio}'";
            }
            else
            {
                // Insertar
                return $@"INSERT INTO dbo.CapturaProduccion 
                        (Folio, Cliente, Contacto, Tipo, Status, Descripcion, Fecha, Usuario)
                        VALUES
                        ('{folio}', '{cliente}', '{contacto}', '{tipo}', '{status}', '{descripcion}', GETDATE(), '{usuario}')";
            }
        }

        private void LoadControlValues(DataRow row)
        {
            TextBox txtFolio = currentPage.FindControl("txtFolio") as TextBox;
            TextBox txtCliente = currentPage.FindControl("txtCliente") as TextBox;
            TextBox txtContacto = currentPage.FindControl("txtContacto") as TextBox;
            DropDownList ddlTipo = currentPage.FindControl("ddlTipo") as DropDownList;
            DropDownList ddlStatus = currentPage.FindControl("ddlStatus") as DropDownList;
            TextBox txtDescripcion = currentPage.FindControl("txtDescripcion") as TextBox;
            
            if (txtFolio != null) txtFolio.Text = row["Folio"].ToString();
            if (txtCliente != null) txtCliente.Text = row["Cliente"].ToString();
            if (txtContacto != null) txtContacto.Text = row["Contacto"].ToString();
            if (ddlTipo != null) ddlTipo.SelectedValue = row["Tipo"].ToString();
            if (ddlStatus != null) ddlStatus.SelectedValue = row["Status"].ToString();
            if (txtDescripcion != null) txtDescripcion.Text = row["Descripcion"].ToString();
            
            // Actualizar variables internas
            this.folio = row["Folio"].ToString();
            this.cliente = row["Cliente"].ToString();
            this.contacto = row["Contacto"].ToString();
            this.tipo = row["Tipo"].ToString();
            this.status = row["Status"].ToString();
            this.descripcion = row["Descripcion"].ToString();
            this.usuario = row["Usuario"].ToString();
        }
    }
    #endregion

    #region Fluent API para Reportes
    public class ReporteQueryBuilder : SateliteFluentCore,
                                      IQueryable<ReporteResult>,
                                      IFilterable<ReporteResult>,
                                      IExecutable<ReporteResult>
    {
        private string tipoReporte;
        private Dictionary<string, string> parametros;

        public ReporteQueryBuilder(Page page)
        {
            this.currentPage = page;
            this.parametros = new Dictionary<string, string>();
        }

        public ReporteQueryBuilder WithReporteType(string tipo)
        {
            this.tipoReporte = tipo;
            return this;
        }

        public IFilterable<ReporteResult> WithDateRange(string startDate, string endDate)
        {
            this.fechaInicio = startDate;
            this.fechaFin = endDate;
            return this;
        }

        public IExecutable<ReporteResult> WithFilters(string status, string searchTerm = "")
        {
            if (!string.IsNullOrEmpty(status))
                this.parametros.Add("Status", status);
                
            if (!string.IsNullOrEmpty(searchTerm))
                this.parametros.Add("SearchTerm", searchTerm);
                
            return this;
        }

        public IExecutable<ReporteResult> WithAdvancedFilters(Dictionary<string, string> filters)
        {
            foreach (var filter in filters)
            {
                if (!string.IsNullOrEmpty(filter.Value))
                {
                    this.parametros[filter.Key] = filter.Value;
                }
            }
            return this;
        }

        public ReporteResult Execute()
        {
            // Validar fechas
            if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
            {
                DisplayMessage("Por favor ingrese un rango de fechas válido");
                return new ReporteResult { Success = false };
            }

            // Construir consulta SQL según el tipo de reporte
            string query = BuildReportQuery();
            
            // Ejecutar consulta
            DataTable results = ExecuteQuery(query);
            
            // Preparar resultado
            ReporteResult result = new ReporteResult
            {
                Data = results,
                Success = results != null && results.Rows.Count > 0,
                Message = results != null && results.Rows.Count > 0 
                    ? $"Resultados del reporte: {results.Rows.Count}" 
                    : "No se encontraron datos para el reporte con los filtros seleccionados"
            };

            // Actualizar UI si es necesario
            UpdateUI(result);
            
            return result;
        }

        public ReporteResult ExportToExcel()
        {
            // Validar fechas
            if (string.IsNullOrEmpty(fechaInicio) || string.IsNullOrEmpty(fechaFin))
            {
                DisplayMessage("Por favor ingrese un rango de fechas válido para exportar");
                return new ReporteResult { Success = false };
            }

            // Construir consulta SQL
            string query = BuildReportQuery();
            
            // Ejecutar consulta
            DataTable results = ExecuteQuery(query);
            
            if (results != null && results.Rows.Count > 0)
            {
                // Exportar a Excel
                ExportDataTableToExcel(results, $"Reporte_{tipoReporte}");
                
                return new ReporteResult
                {
                    Data = results,
                    Success = true,
                    Message = "Datos exportados exitosamente"
                };
            }
            else
            {
                DisplayMessage("No hay datos para exportar");
                return new ReporteResult
                {
                    Success = false,
                    Message = "No hay datos para exportar"
                };
            }
        }

        private string BuildReportQuery()
        {
            string query = "";
            
            switch (tipoReporte?.ToLower() ?? "")
            {
                case "produccion":
                    query = $@"SELECT Folio, Cliente, Tipo, Status, Fecha, Usuario,
                            COUNT(*) OVER() as Total
                            FROM dbo.CapturaProduccion 
                            WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFin}'";
                    break;
                    
                case "status":
                    query = $@"SELECT Status, COUNT(*) as Cantidad
                            FROM dbo.CapturaProduccion 
                            WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFin}'
                            GROUP BY Status";
                    break;
                    
                case "usuarios":
                    query = $@"SELECT Usuario, COUNT(*) as Registros, 
                            COUNT(DISTINCT Cliente) as Clientes
                            FROM dbo.CapturaProduccion 
                            WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFin}'
                            GROUP BY Usuario";
                    break;
                    
                default:
                    query = $@"SELECT Folio, Cliente, Tipo, Status, Fecha, Usuario
                            FROM dbo.CapturaProduccion
                            WHERE Fecha BETWEEN '{fechaInicio}' AND '{fechaFin}'";
                    break;
            }
            
            // Añadir filtros adicionales
            if (parametros.ContainsKey("Status") && !string.IsNullOrEmpty(parametros["Status"]) && parametros["Status"] != "0")
            {
                query += $" AND Status = '{parametros["Status"]}'";
            }
            
            if (parametros.ContainsKey("SearchTerm") && !string.IsNullOrEmpty(parametros["SearchTerm"]))
            {
                query += $" AND (Folio LIKE '%{parametros["SearchTerm"]}%' OR Cliente LIKE '%{parametros["SearchTerm"]}%')";
            }
            
            // Ordenar según el tipo de reporte
            if (tipoReporte?.ToLower() == "status" || tipoReporte?.ToLower() == "usuarios")
            {
                // Ya tiene GROUP BY, no añadir ORDER BY
            }
            else
            {
                query += " ORDER BY Fecha DESC";
            }
            
            return query;
        }

        private void UpdateUI(ReporteResult result)
        {
            GridView gvReporte = currentPage.FindControl("gvReporte") as GridView;
            Label lblResultados = currentPage.FindControl("lblResultados") as Label;
            
            if (gvReporte != null)
            {
                if (result.Success && result.Data != null)
                {
                    BindGridView(gvReporte, result.Data, lblResultados);
                }
                else
                {
                    // Crear tabla vacía para mostrar
                    DataTable emptyTable = new DataTable();
                    switch (tipoReporte?.ToLower() ?? "")
                    {
                        case "status":
                            emptyTable.Columns.Add("Status");
                            emptyTable.Columns.Add("Cantidad");
                            break;
                        case "usuarios":
                            emptyTable.Columns.Add("Usuario");
                            emptyTable.Columns.Add("Registros");
                            emptyTable.Columns.Add("Clientes");
                            break;
                        default:
                            emptyTable.Columns.Add("Folio");
                            emptyTable.Columns.Add("Cliente");
                            emptyTable.Columns.Add("Tipo");
                            emptyTable.Columns.Add("Status");
                            emptyTable.Columns.Add("Fecha");
                            emptyTable.Columns.Add("Usuario");
                            break;
                    }
                    
                    BindGridView(gvReporte, emptyTable, lblResultados);
                    DisplayMessage(result.Message);
                }
            }
        }

        public void InitializePage()
        {
            try
            {
                // Cargar dropdown de Status
                DropDownList ddlStatus = currentPage.FindControl("ddlStatus") as DropDownList;
                if (ddlStatus != null)
                {
                    ddlStatus.Items.Clear();
                    ddlStatus.Items.Add(new ListItem("Todos", "0"));
                    
                    string query = "SELECT DISTINCT Status FROM dbo.CapturaProduccion ORDER BY Status";
                    DataTable statuses = ExecuteQuery(query);
                    
                    if (statuses != null && statuses.Rows.Count > 0)
                    {
                        foreach (DataRow row in statuses.Rows)
                        {
                            string status = row["Status"].ToString();
                            ddlStatus.Items.Add(new ListItem(status, status));
                        }
                    }
                }
                
                // Cargar dropdown de Tipo de Reporte
                DropDownList ddlTipoReporte = currentPage.FindControl("ddlTipoReporte") as DropDownList;
                if (ddlTipoReporte != null)
                {
                    ddlTipoReporte.Items.Clear();
                    ddlTipoReporte.Items.Add(new ListItem("Producción", "produccion"));
                    ddlTipoReporte.Items.Add(new ListItem("Por Status", "status"));
                    ddlTipoReporte.Items.Add(new ListItem("Por Usuarios", "usuarios"));
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al inicializar página: {ex.Message}");
            }
        }
    }

    public class ReporteResult
    {
        public DataTable Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    #endregion

    #region Página de ejemplo para Consulta con Fluent API
    public partial class ConsultaFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización con Fluent API
                new ConsultaQueryBuilder(this).InitializePage();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Realizar búsqueda con Fluent API
            new ConsultaQueryBuilder(this)
                .WithDateRange(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFilters(ddlStatus.SelectedValue, txtBus.Text)
                .Execute();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            // Exportar con Fluent API
            new ConsultaQueryBuilder(this)
                .WithDateRange(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFilters(ddlStatus.SelectedValue, txtBus.Text)
                .ExportToExcel();
        }
    }
    #endregion

    #region Página de ejemplo para Captura con Fluent API
    public partial class CapturaFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización con Fluent API
                new CapturaBuilder(this).InitializePage();
                
                // Verificar si se está recibiendo un folio para cargar
                string folioToLoad = Request.QueryString["folio"];
                if (!string.IsNullOrEmpty(folioToLoad))
                {
                    new CapturaBuilder(this).Load(folioToLoad);}
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar con Fluent API
            string usuario = Session["Usuario"]?.ToString() ?? "Sistema";
            
            bool success = new CapturaBuilder(this)
                .WithFolio(txtFolio.Text)
                .WithCliente(txtCliente.Text)
                .WithContacto(txtContacto.Text)
                .WithTipo(ddlTipo.SelectedValue)
                .WithStatus(ddlStatus.SelectedValue)
                .WithDescripcion(txtDescripcion.Text)
                .WithUsuario(usuario)
                .Save();
                
            if (success)
            {
                // Limpiar controles después de guardar
                txtFolio.Text = string.Empty;
                txtCliente.Text = string.Empty;
                txtContacto.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Cargar registro por folio
            if (!string.IsNullOrEmpty(txtFolio.Text))
            {
                new CapturaBuilder(this).Load(txtFolio.Text);
            }
            else
            {
                // Mostrar mensaje de error
                Label lblMensaje = FindControl("lblMensaje") as Label;
                if (lblMensaje != null)
                {
                    lblMensaje.Text = "Por favor ingrese un folio para buscar";
                    lblMensaje.Visible = true;
                }
            }
        }
    }
    #endregion

    #region Página de ejemplo para Reportes con Fluent API
    public partial class ReportesFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización con Fluent API
                new ReporteQueryBuilder(this).InitializePage();
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            // Generar reporte con Fluent API
            string tipoReporte = ddlTipoReporte.SelectedValue;
            
            new ReporteQueryBuilder(this)
                .WithReporteType(tipoReporte)
                .WithDateRange(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFilters(ddlStatus.SelectedValue, txtBusqueda.Text)
                .Execute();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            // Exportar reporte con Fluent API
            string tipoReporte = ddlTipoReporte.SelectedValue;
            
            new ReporteQueryBuilder(this)
                .WithReporteType(tipoReporte)
                .WithDateRange(txtFechaInicio.Text, txtFechaFin.Text)
                .WithFilters(ddlStatus.SelectedValue, txtBusqueda.Text)
                .ExportToExcel();
        }
    }
    #endregion

    #region Fluent API para Usuario (Login/Seguridad)
    public class UsuarioBuilder : SateliteFluentCore
    {
        private string username;
        private string password;
        private string nombre;
        private string rol;

        public UsuarioBuilder(Page page)
        {
            this.currentPage = page;
        }

        public UsuarioBuilder WithCredentials(string username, string password)
        {
            this.username = username;
            this.password = password;
            return this;
        }

        public UsuarioBuilder WithUserInfo(string nombre, string rol)
        {
            this.nombre = nombre;
            this.rol = rol;
            return this;
        }

        public LoginResult Authenticate()
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Por favor ingrese usuario y contraseña",
                        NombreUsuario = string.Empty,
                        Rol = string.Empty
                    };
                }

                // Construir consulta SQL para verificar credenciales
                string query = $"SELECT * FROM dbo.Usuarios WHERE Usuario = '{username}' AND Password = '{password}'";
                DataTable result = ExecuteQuery(query);
                
                if (result != null && result.Rows.Count > 0)
                {
                    DataRow userData = result.Rows[0];
                    
                    // Autenticación exitosa
                    return new LoginResult
                    {
                        Success = true,
                        Message = "Autenticación exitosa",
                        NombreUsuario = userData["Nombre"].ToString(),
                        Rol = userData["Rol"].ToString()
                    };
                }
                else
                {
                    // Autenticación fallida
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Usuario o contraseña incorrectos",
                        NombreUsuario = string.Empty,
                        Rol = string.Empty
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = $"Error: {ex.Message}",
                    NombreUsuario = string.Empty,
                    Rol = string.Empty
                };
            }
        }

        public bool Save()
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || 
                    string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(rol))
                {
                    DisplayMessage("Por favor complete todos los campos obligatorios");
                    return false;
                }

                // Verificar si el usuario ya existe
                string checkQuery = $"SELECT COUNT(*) FROM dbo.Usuarios WHERE Usuario = '{username}'";
                DataTable checkResult = ExecuteQuery(checkQuery);
                
                bool exists = false;
                if (checkResult != null && checkResult.Rows.Count > 0)
                {
                    exists = Convert.ToInt32(checkResult.Rows[0][0]) > 0;
                }
                
                string query;
                if (exists)
                {
                    // Actualizar
                    query = $@"UPDATE dbo.Usuarios SET 
                            Password = '{password}',
                            Nombre = '{nombre}',
                            Rol = '{rol}',
                            FechaModificacion = GETDATE()
                            WHERE Usuario = '{username}'";
                }
                else
                {
                    // Insertar
                    query = $@"INSERT INTO dbo.Usuarios 
                            (Usuario, Password, Nombre, Rol, FechaCreacion, FechaModificacion)
                            VALUES
                            ('{username}', '{password}', '{nombre}', '{rol}', GETDATE(), GETDATE())";
                }
                
                // Ejecutar consulta
                ExecuteQuery(query);
                
                // Mostrar mensaje
                DisplayMessage("Usuario guardado exitosamente");
                
                return true;
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al guardar usuario: {ex.Message}");
                return false;
            }
        }

        public bool Load(string usernameToLoad)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(usernameToLoad))
                {
                    DisplayMessage("Por favor ingrese un nombre de usuario");
                    return false;
                }

                // Consultar usuario
                string query = $"SELECT * FROM dbo.Usuarios WHERE Usuario = '{usernameToLoad}'";
                DataTable result = ExecuteQuery(query);
                
                if (result != null && result.Rows.Count > 0)
                {
                    DataRow userData = result.Rows[0];
                    
                    // Cargar datos en controles
                    TextBox txtUsername = currentPage.FindControl("txtUsername") as TextBox;
                    TextBox txtPassword = currentPage.FindControl("txtPassword") as TextBox;
                    TextBox txtNombre = currentPage.FindControl("txtNombre") as TextBox;
                    DropDownList ddlRol = currentPage.FindControl("ddlRol") as DropDownList;
                    
                    if (txtUsername != null) txtUsername.Text = userData["Usuario"].ToString();
                    if (txtPassword != null) txtPassword.Text = userData["Password"].ToString();
                    if (txtNombre != null) txtNombre.Text = userData["Nombre"].ToString();
                    if (ddlRol != null) ddlRol.SelectedValue = userData["Rol"].ToString();
                    
                    // Actualizar variables internas
                    this.username = userData["Usuario"].ToString();
                    this.password = userData["Password"].ToString();
                    this.nombre = userData["Nombre"].ToString();
                    this.rol = userData["Rol"].ToString();
                    
                    return true;
                }
                else
                {
                    DisplayMessage("Usuario no encontrado");
                    return false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al cargar usuario: {ex.Message}");
                return false;
            }
        }

        public bool Delete(string usernameToDelete)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(usernameToDelete))
                {
                    DisplayMessage("Por favor seleccione un usuario para eliminar");
                    return false;
                }

                // Eliminar usuario
                string query = $"DELETE FROM dbo.Usuarios WHERE Usuario = '{usernameToDelete}'";
                ExecuteQuery(query);
                
                DisplayMessage("Usuario eliminado exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al eliminar usuario: {ex.Message}");
                return false;
            }
        }

        public void InitializePage()
        {
            try
            {
                // Cargar dropdown de Roles
                DropDownList ddlRol = currentPage.FindControl("ddlRol") as DropDownList;
                if (ddlRol != null)
                {
                    ddlRol.Items.Clear();
                    ddlRol.Items.Add(new ListItem("Administrador", "Administrador"));
                    ddlRol.Items.Add(new ListItem("Usuario", "Usuario"));
                    ddlRol.Items.Add(new ListItem("Consulta", "Consulta"));
                }
                
                // Cargar lista de usuarios
                GridView gvUsuarios = currentPage.FindControl("gvUsuarios") as GridView;
                if (gvUsuarios != null)
                {
                    string query = "SELECT Usuario, Nombre, Rol, FechaCreacion FROM dbo.Usuarios ORDER BY Usuario";
                    DataTable usuarios = ExecuteQuery(query);
                    
                    if (usuarios != null)
                    {
                        gvUsuarios.DataSource = usuarios;
                        gvUsuarios.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al inicializar página: {ex.Message}");
            }
        }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
    }
    #endregion

    #region Página de ejemplo para Login con Fluent API
    public partial class LoginFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicialización
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Autenticar con Fluent API
            LoginResult result = new UsuarioBuilder(this)
                .WithCredentials(txtUsuario.Text, txtPassword.Text)
                .Authenticate();
                
            if (result.Success)
            {
                // Establecer variables de sesión
                Session["Usuario"] = txtUsuario.Text;
                Session["NombreUsuario"] = result.NombreUsuario;
                Session["Rol"] = result.Rol;
                
                // Redirigir al menú principal
                Response.Redirect("Menu.aspx");
            }
            else
            {
                // Mostrar mensaje de error
                lblMensaje.Text = result.Message;
                lblMensaje.Visible = true;
            }
        }
    }
    #endregion

    #region Página de ejemplo para Administración de Usuarios con Fluent API
    public partial class UsuariosFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar rol de usuario
                string rol = Session["Rol"]?.ToString() ?? string.Empty;
                if (rol != "Administrador")
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                
                // Inicialización con Fluent API
                new UsuarioBuilder(this).InitializePage();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Guardar usuario con Fluent API
            bool success = new UsuarioBuilder(this)
                .WithCredentials(txtUsername.Text, txtPassword.Text)
                .WithUserInfo(txtNombre.Text, ddlRol.SelectedValue)
                .Save();
                
            if (success)
            {
                // Recargar lista de usuarios
                new UsuarioBuilder(this).InitializePage();
                
                // Limpiar controles
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtNombre.Text = string.Empty;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Buscar usuario por nombre
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                new UsuarioBuilder(this).Load(txtUsername.Text);
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string username = e.CommandArgument.ToString();
                new UsuarioBuilder(this).Load(username);
            }
            else if (e.CommandName == "Eliminar")
            {
                string username = e.CommandArgument.ToString();
                if (new UsuarioBuilder(this).Delete(username))
                {
                    // Recargar lista de usuarios
                    new UsuarioBuilder(this).InitializePage();
                }
            }
        }
    }
    #endregion

    #region Utilidades para aplicación en varias páginas
    public static class FluentExtensions
    {
        // Métodos de extensión para facilitar el uso del Fluent API
        
        public static void ShowAlert(this Page page, string message)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(), 
                "alertMessage", $"alert('{message}');", true);
        }
        
        public static void ShowConfirm(this Page page, string message, string trueAction, string falseAction = "")
        {
            string script = $"if(confirm('{message}')) {{ {trueAction} }} ";
            if (!string.IsNullOrEmpty(falseAction))
            {
                script += $"else {{ {falseAction} }}";
            }
            
            ScriptManager.RegisterStartupScript(page, page.GetType(), 
                "confirmScript", script, true);
        }
        
        public static bool IsAuthenticated(this Page page)
        {
            return page.Session["Usuario"] != null;
        }
        
        public static bool HasRole(this Page page, string role)
        {
            string userRole = page.Session["Rol"]?.ToString() ?? string.Empty;
            return userRole == role;
        }
        
        public static bool HasAnyRole(this Page page, params string[] roles)
        {
            string userRole = page.Session["Rol"]?.ToString() ?? string.Empty;
            return roles.Contains(userRole);
        }
    }
    #endregion

    #region Ejemplo de Implementación en Global.asax
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Configuraciones globales
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Inicialización de sesión
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Verificación de autenticación para páginas protegidas
            string path = Request.Path.ToLower();
            
            // Lista de páginas que requieren autenticación
            string[] securedPages = {
                "/consulta.aspx",
                "/captura.aspx",
                "/reportes.aspx",
                "/usuarios.aspx",
                "/menu.aspx"
            };
            
            // Verificar si la página actual requiere autenticación
            if (securedPages.Any(p => path.EndsWith(p)) && Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            
            // Verificar permisos específicos
            if (path.EndsWith("/usuarios.aspx") && Session["Rol"]?.ToString() != "Administrador")
            {
                Response.Redirect("Menu.aspx");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Manejo global de errores
            Exception ex = Server.GetLastError();
            
            // Registrar error en log
            string logPath = Server.MapPath("~/App_Data/ErrorLog.txt");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
                sw.WriteLine($"Stack Trace: {ex.StackTrace}");
                sw.WriteLine(new string('-', 50));
            }
            
            // Redirigir a página de error
            Server.ClearError();
            Response.Redirect("Error.aspx");
        }
    }
    #endregion

    #region Fluent API para manejo de archivos
    public class ArchivoBuilder : SateliteFluentCore
    {
        private string folio;
        private FileUpload fileUpload;
        private string tipoArchivo;
        private string descripcion;
        private string usuario;
        private string rutaDestino;

        public ArchivoBuilder(Page page)
        {
            this.currentPage = page;
            this.rutaDestino = page.Server.MapPath("~/Archivos/");
        }

        public ArchivoBuilder WithFolio(string folio)
        {
            this.folio = folio;
            return this;
        }

        public ArchivoBuilder WithFileUpload(FileUpload fileUpload)
        {
            this.fileUpload = fileUpload;
            return this;
        }

        public ArchivoBuilder WithTipo(string tipo)
        {
            this.tipoArchivo = tipo;
            return this;
        }

        public ArchivoBuilder WithDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
            return this;
        }

        public ArchivoBuilder WithUsuario(string usuario)
        {
            this.usuario = usuario;
            return this;
        }

        public ArchivoBuilder WithDestinationPath(string path)
        {
            this.rutaDestino = path;
            return this;
        }

        public bool Upload()
        {
            try
            {
                // Validaciones
                if (string.IsNullOrEmpty(folio))
                {
                    DisplayMessage("Por favor ingrese un folio");
                    return false;
                }
                
                if (fileUpload == null || !fileUpload.HasFile)
                {
                    DisplayMessage("Por favor seleccione un archivo");
                    return false;
                }

                // Crear directorio si no existe
                if (!Directory.Exists(rutaDestino))
                {
                    Directory.CreateDirectory(rutaDestino);
                }

                // Guardar archivo
                string nombreArchivo = $"{folio}_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{fileUpload.FileName}";
                string rutaCompleta = Path.Combine(rutaDestino, nombreArchivo);
                
                fileUpload.SaveAs(rutaCompleta);
                
                // Registrar en base de datos
                string query = $@"INSERT INTO dbo.Archivos 
                                (Folio, NombreArchivo, RutaArchivo, TipoArchivo, Descripcion, FechaCarga, Usuario)
                                VALUES
                                ('{folio}', '{fileUpload.FileName}', '{nombreArchivo}', '{tipoArchivo}', 
                                '{descripcion}', GETDATE(), '{usuario}')";
                                
                ExecuteQuery(query);
                
                DisplayMessage("Archivo cargado exitosamente");
                return true;
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al cargar archivo: {ex.Message}");
                return false;
            }
        }

        public DataTable ListarArchivos()
        {
            try
            {
                // Validar folio
                if (string.IsNullOrEmpty(folio))
                {
                    DisplayMessage("Por favor ingrese un folio para listar archivos");
                    return null;
                }

                // Consultar archivos
                string query = $@"SELECT Id, NombreArchivo, TipoArchivo, Descripcion, FechaCarga, Usuario
                                FROM dbo.Archivos
                                WHERE Folio = '{folio}'
                                ORDER BY FechaCarga DESC";
                                
                DataTable archivos = ExecuteQuery(query);
                
                // Actualizar GridView si existe
                GridView gvArchivos = currentPage.FindControl("gvArchivos") as GridView;
                if (gvArchivos != null && archivos != null)
                {
                    gvArchivos.DataSource = archivos;
                    gvArchivos.DataBind();
                }
                
                return archivos;
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al listar archivos: {ex.Message}");
                return null;
            }
        }

        public bool EliminarArchivo(int idArchivo)
        {
            try
            {
                // Obtener información del archivo
                string queryInfo = $"SELECT RutaArchivo FROM dbo.Archivos WHERE Id = {idArchivo}";
                DataTable info = ExecuteQuery(queryInfo);
                
                if (info != null && info.Rows.Count > 0)
                {
                    string rutaArchivo = info.Rows[0]["RutaArchivo"].ToString();
                    string rutaCompleta = Path.Combine(rutaDestino, rutaArchivo);
                    
                    // Eliminar archivo físico si existe
                    if (File.Exists(rutaCompleta))
                    {
                        File.Delete(rutaCompleta);
                    }
                    
                    // Eliminar registro de base de datos
                    string queryDelete = $"DELETE FROM dbo.Archivos WHERE Id = {idArchivo}";
                    ExecuteQuery(queryDelete);
                    
                    DisplayMessage("Archivo eliminado exitosamente");
                    return true;
                }
                else
                {
                    DisplayMessage("No se encontró el archivo");
                    return false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al eliminar archivo: {ex.Message}");
                return false;
            }
        }

        public void DescargarArchivo(int idArchivo)
        {
            try
            {
                // Obtener información del archivo
                string queryInfo = $"SELECT NombreArchivo, RutaArchivo FROM dbo.Archivos WHERE Id = {idArchivo}";
                DataTable info = ExecuteQuery(queryInfo);
                
                if (info != null && info.Rows.Count > 0)
                {
                    string nombreOriginal = info.Rows[0]["NombreArchivo"].ToString();
                    string rutaArchivo = info.Rows[0]["RutaArchivo"].ToString();
                    string rutaCompleta = Path.Combine(rutaDestino, rutaArchivo);
                    
                    // Verificar si el archivo existe
                    if (File.Exists(rutaCompleta))
                    {
                        // Preparar respuesta para descarga
                        HttpResponse response = currentPage.Response;
                        response.Clear();
                        response.ContentType = "application/octet-stream";
                        response.AddHeader("Content-Disposition", $"attachment; filename=\"{nombreOriginal}\"");
                        response.WriteFile(rutaCompleta);
                        response.End();
                    }
                    else
                    {
                        DisplayMessage("El archivo no existe en el servidor");
                    }
                }
                else
                {
                    DisplayMessage("No se encontró el archivo");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error al descargar archivo: {ex.Message}");
            }
        }
    }
    #endregion

    #region Ejemplo de uso de Fluent API para una nueva página de Archivos
    public partial class ArchivosFluent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si se está recibiendo un folio
                string folioParam = Request.QueryString["folio"];
                if (!string.IsNullOrEmpty(folioParam))
                {
                    txtFolio.Text = folioParam;
                    // Cargar archivos para este folio
                    CargarArchivos();
                }
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            string usuario = Session["Usuario"]?.ToString() ?? "Sistema";
            
            // Subir archivo con Fluent API
            bool success = new ArchivoBuilder(this)
                .WithFolio(txtFolio.Text)
                .WithFileUpload(fileUpload)
                .WithTipo(ddlTipoArchivo.SelectedValue)
                .WithDescripcion(txtDescripcion.Text)
                .WithUsuario(usuario)
                .Upload();
                
            if (success)
            {
                // Recargar lista de archivos
                CargarArchivos();
                
                // Limpiar controles
                txtDescripcion.Text = string.Empty;
            }
        }

        protected void gvArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Descargar")
            {
                int idArchivo = Convert.ToInt32(e.CommandArgument);
                
                // Descargar archivo con Fluent API
                new ArchivoBuilder(this)
                    .WithFolio(txtFolio.Text)
                    .DescargarArchivo(idArchivo);
            }
            else if (e.CommandName == "Eliminar")
            {
                int idArchivo = Convert.ToInt32(e.CommandArgument);
                
                // Eliminar archivo con Fluent API
                if (new ArchivoBuilder(this).EliminarArchivo(idArchivo))
                {
                    // Recargar lista de archivos
                    CargarArchivos();
                }
            }
        }

        private void CargarArchivos()
        {
            // Listar archivos con Fluent API
            new ArchivoBuilder(this)
                .WithFolio(txtFolio.Text)
                .ListarArchivos();
        }
    }
    #endregion
}