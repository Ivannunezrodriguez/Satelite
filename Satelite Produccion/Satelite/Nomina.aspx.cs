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
using Satelite.Reports;

namespace Satelite
{
    public partial class Nominas : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private NominaService _nominaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _nominaService = new NominaService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario y permisos
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Verificar permisos - solo administradores y RRHH pueden acceder
                string rolUsuario = Session["RolUsuario"].ToString();
                if (rolUsuario != "Administrador" && rolUsuario != "RRHH")
                {
                    Response.Redirect("AccesoDenegado.aspx");
                    return;
                }

                // Inicializar controles
                CargarEmpleados();
                CargarPeriodos();
                CargarNominas();
            }
        }

        protected void CargarEmpleados()
        {
            DataTable empleados = _nominaService.ObtenerEmpleados();

            ddlEmpleado.DataSource = empleados;
            ddlEmpleado.DataTextField = "NombreCompleto";
            ddlEmpleado.DataValueField = "EmpleadoId";
            ddlEmpleado.DataBind();

            // Agregar opción por defecto
            ddlEmpleado.Items.Insert(0, new ListItem("-- Seleccione un empleado --", "0"));
        }

        protected void CargarPeriodos()
        {
            // Generar lista de periodos (últimos 12 meses)
            List<object> periodos = new List<object>();
            DateTime fechaActual = DateTime.Now;

            for (int i = 0; i < 12; i++)
            {
                DateTime fecha = fechaActual.AddMonths(-i);
                string periodo = $"{fecha.Year}-{fecha.Month:D2}";
                string nombrePeriodo = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(fecha.Month)} {fecha.Year}";

                periodos.Add(new { Periodo = periodo, Nombre = nombrePeriodo });
            }

            ddlPeriodo.DataSource = periodos;
            ddlPeriodo.DataTextField = "Nombre";
            ddlPeriodo.DataValueField = "Periodo";
            ddlPeriodo.DataBind();
        }

        protected void CargarNominas()
        {
            // Cargar nóminas procesadas
            DataTable nominas = _nominaService.ObtenerNominas();

            gvNominas.DataSource = nominas;
            gvNominas.DataBind();

            // Mostrar mensaje si no hay nóminas
            lblSinNominas.Visible = (nominas.Rows.Count == 0);
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            // Validar datos
            if (ddlEmpleado.SelectedValue == "0")
            {
                MostrarMensaje("Por favor seleccione un empleado.", false);
                return;
            }

            try
            {
                // Obtener datos del formulario
                int empleadoId = Convert.ToInt32(ddlEmpleado.SelectedValue);
                string periodo = ddlPeriodo.SelectedValue;

                // Verificar si ya existe una nómina para este empleado y periodo
                if (_nominaService.ExisteNomina(empleadoId, periodo))
                {
                    MostrarMensaje("Ya existe una nómina procesada para este empleado y periodo.", false);
                    return;
                }

                // Calcular días trabajados y ausencias
                int diasTrabajados = 0;
                if (!string.IsNullOrEmpty(txtDiasTrabajados.Text))
                {
                    diasTrabajados = Convert.ToInt32(txtDiasTrabajados.Text);
                }
                else
                {
                    // Si no se especifica, calcular automáticamente los días laborables del mes
                    diasTrabajados = _nominaService.CalcularDiasLaborablesMes(periodo);
                }

                decimal salarioBruto = 0;
                if (!string.IsNullOrEmpty(txtSalarioBruto.Text))
                {
                    salarioBruto = Convert.ToDecimal(txtSalarioBruto.Text, CultureInfo.InvariantCulture);
                }
                else
                {
                    // Si no se especifica, obtener el salario base del empleado
                    salarioBruto = _nominaService.ObtenerSalarioEmpleado(empleadoId);
                }

                // Obtener otros valores del formulario
                decimal horasExtra = !string.IsNullOrEmpty(txtHorasExtra.Text) ?
                    Convert.ToDecimal(txtHorasExtra.Text, CultureInfo.InvariantCulture) : 0;

                decimal importeHorasExtra = !string.IsNullOrEmpty(txtImporteHorasExtra.Text) ?
                    Convert.ToDecimal(txtImporteHorasExtra.Text, CultureInfo.InvariantCulture) : 0;

                decimal otrosIngresos = !string.IsNullOrEmpty(txtOtrosIngresos.Text) ?
                    Convert.ToDecimal(txtOtrosIngresos.Text, CultureInfo.InvariantCulture) : 0;

                decimal retencionIRPF = !string.IsNullOrEmpty(txtRetencionIRPF.Text) ?
                    Convert.ToDecimal(txtRetencionIRPF.Text, CultureInfo.InvariantCulture) : 0;

                decimal seguridadSocial = !string.IsNullOrEmpty(txtSeguridadSocial.Text) ?
                    Convert.ToDecimal(txtSeguridadSocial.Text, CultureInfo.InvariantCulture) : 0;

                decimal otrosDescuentos = !string.IsNullOrEmpty(txtOtrosDescuentos.Text) ?
                    Convert.ToDecimal(txtOtrosDescuentos.Text, CultureInfo.InvariantCulture) : 0;

                string observaciones = txtObservaciones.Text.Trim();
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

                // Procesar la nómina
                int nominaId = _nominaService.ProcesarNomina(
                    empleadoId,
                    periodo,
                    diasTrabajados,
                    salarioBruto,
                    horasExtra,
                    importeHorasExtra,
                    otrosIngresos,
                    retencionIRPF,
                    seguridadSocial,
                    otrosDescuentos,
                    observaciones,
                    usuarioId);

                // Procesar archivo adjunto si existe
                if (fileUpload.HasFile)
                {
                    string nombreArchivo = Path.GetFileName(fileUpload.FileName);
                    string extension = Path.GetExtension(nombreArchivo).ToLower();

                    // Validar extensión del archivo
                    if (extension != ".pdf" && extension != ".doc" && extension != ".docx")
                    {
                        MostrarMensaje("Solo se permiten archivos PDF o Word.", false);
                        return;
                    }

                    // Generar nombre único
                    string nombreUnico = $"nomina_{nominaId}_{Guid.NewGuid()}{extension}";

                    // Ruta donde se guardará el archivo
                    string rutaArchivos = Server.MapPath("~/Archivos/Nominas/");

                    // Verificar si existe el directorio y si no, crearlo
                    if (!Directory.Exists(rutaArchivos))
                    {
                        Directory.CreateDirectory(rutaArchivos);
                    }

                    // Ruta completa
                    string rutaCompleta = Path.Combine(rutaArchivos, nombreUnico);

                    // Guardar el archivo
                    fileUpload.SaveAs(rutaCompleta);

                    // Actualizar la nómina con la referencia al archivo
                    _nominaService.ActualizarArchivoNomina(nominaId, nombreArchivo, nombreUnico, rutaCompleta, usuarioId);
                }

                MostrarMensaje("Nómina procesada exitosamente.", true);

                // Limpiar formulario y recargar datos
                LimpiarFormulario();
                CargarNominas();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void gvNominas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nominaId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Descargar")
            {
                // Obtener información del archivo
                DataTable archivoData = _nominaService.ObtenerArchivoNomina(nominaId);

                if (archivoData.Rows.Count > 0 && archivoData.Rows[0]["RutaArchivo"] != DBNull.Value)
                {
                    DataRow row = archivoData.Rows[0];
                    string nombreOriginal = row["NombreArchivo"].ToString();
                    string rutaArchivo = row["RutaArchivo"].ToString();

                    // Verificar que el archivo exista
                    if (File.Exists(rutaArchivo))
                    {
                        // Determinar el tipo de contenido
                        string extension = Path.GetExtension(nombreOriginal).ToLower();
                        string contentType = "application/octet-stream";

                        if (extension == ".pdf")
                            contentType = "application/pdf";
                        else if (extension == ".doc")
                            contentType = "application/msword";
                        else if (extension == ".docx")
                            contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

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
                    MostrarMensaje("Esta nómina no tiene un archivo adjunto.", false);
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    // Verificar permisos - solo administradores pueden eliminar
                    string rolUsuario = Session["RolUsuario"].ToString();
                    if (rolUsuario != "Administrador")
                    {
                        MostrarMensaje("No tiene permisos para eliminar nóminas.", false);
                        return;
                    }

                    // Eliminar nómina
                    int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                    _nominaService.EliminarNomina(nominaId, usuarioId);

                    MostrarMensaje("Nómina eliminada exitosamente.", true);

                    // Recargar datos
                    CargarNominas();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar datos mínimos
                if (ddlEmpleado.SelectedValue == "0")
                {
                    MostrarMensaje("Por favor seleccione un empleado.", false);
                    return;
                }

                // Obtener datos para el cálculo
                int empleadoId = Convert.ToInt32(ddlEmpleado.SelectedValue);
                decimal salarioBruto = 0;

                // Obtener salario base del empleado
                salarioBruto = _nominaService.ObtenerSalarioEmpleado(empleadoId);
                txtSalarioBruto.Text = salarioBruto.ToString("F2");

                // Calcular días laborables del periodo seleccionado
                string periodo = ddlPeriodo.SelectedValue;
                int diasLaborables = _nominaService.CalcularDiasLaborablesMes(periodo);
                txtDiasTrabajados.Text = diasLaborables.ToString();

                // Calcular retenciones por defecto
                decimal retencionIRPF = salarioBruto * 0.15m; // 15% por defecto
                txtRetencionIRPF.Text = retencionIRPF.ToString("F2");

                decimal seguridadSocial = salarioBruto * 0.0625m; // 6.25% por defecto
                txtSeguridadSocial.Text = seguridadSocial.ToString("F2");

                // Calcular total
                CalcularTotal();

                // Mostrar mensaje informativo
                lblCalculos.Text = "Valores calculados automáticamente basados en el salario base del empleado y los porcentajes estándar de retención.";
                lblCalculos.Visible = true;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al calcular: " + ex.Message, false);
            }
        }

        protected void CalcularTotal()
        {
            try
            {
                // Obtener valores del formulario
                decimal salarioBruto = !string.IsNullOrEmpty(txtSalarioBruto.Text) ?
                    Convert.ToDecimal(txtSalarioBruto.Text, CultureInfo.InvariantCulture) : 0;

                decimal importeHorasExtra = !string.IsNullOrEmpty(txtImporteHorasExtra.Text) ?
                    Convert.ToDecimal(txtImporteHorasExtra.Text, CultureInfo.InvariantCulture) : 0;

                decimal otrosIngresos = !string.IsNullOrEmpty(txtOtrosIngresos.Text) ?
                    Convert.ToDecimal(txtOtrosIngresos.Text, CultureInfo.InvariantCulture) : 0;

                decimal retencionIRPF = !string.IsNullOrEmpty(txtRetencionIRPF.Text) ?
                    Convert.ToDecimal(txtRetencionIRPF.Text, CultureInfo.InvariantCulture) : 0;

                decimal seguridadSocial = !string.IsNullOrEmpty(txtSeguridadSocial.Text) ?
                    Convert.ToDecimal(txtSeguridadSocial.Text, CultureInfo.InvariantCulture) : 0;

                decimal otrosDescuentos = !string.IsNullOrEmpty(txtOtrosDescuentos.Text) ?
                    Convert.ToDecimal(txtOtrosDescuentos.Text, CultureInfo.InvariantCulture) : 0;

                // Calcular totales
                decimal totalIngresos = salarioBruto + importeHorasExtra + otrosIngresos;
                decimal totalRetenciones = retencionIRPF + seguridadSocial + otrosDescuentos;
                decimal salarioNeto = totalIngresos - totalRetenciones;

                // Mostrar resultados
                txtTotalIngresos.Text = totalIngresos.ToString("F2");
                txtTotalRetenciones.Text = totalRetenciones.ToString("F2");
                txtSalarioNeto.Text = salarioNeto.ToString("F2");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al calcular total: " + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            ddlEmpleado.SelectedValue = "0";
            txtDiasTrabajados.Text = string.Empty;
            txtSalarioBruto.Text = string.Empty;
            txtHorasExtra.Text = "0";
            txtImporteHorasExtra.Text = "0.00";
            txtOtrosIngresos.Text = "0.00";
            txtTotalIngresos.Text = "0.00";
            txtRetencionIRPF.Text = "0.00";
            txtSeguridadSocial.Text = "0.00";
            txtOtrosDescuentos.Text = "0.00";
            txtTotalRetenciones.Text = "0.00";
            txtSalarioNeto.Text = "0.00";
            txtObservaciones.Text = string.Empty;
            lblCalculos.Visible = false;

            // Limpiar mensaje
            lblMensaje.Visible = false;
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }
    }

    // Clase de servicio para gestión de nóminas
    public class NominaService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public NominaService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerEmpleados()
        {
            return _queryBuilder
                .Select("e.EmpleadoId, e.NombreCompleto, e.Departamento, e.Puesto, e.SalarioBase")
                .From("Empleados AS e")
                .Where(true, "e.Activo", "=", true)
                .OrderBy("e.NombreCompleto")
                .Execute();
        }

        public DataTable ObtenerNominas()
        {
            return _queryBuilder
                .Select("n.NominaId, e.NombreCompleto AS Empleado, n.Periodo, " +
                        "n.SalarioBruto, n.HorasExtra, n.ImporteHorasExtra, n.OtrosIngresos, " +
                        "n.RetencionIRPF, n.SeguridadSocial, n.OtrosDescuentos, " +
                        "(n.SalarioBruto + n.ImporteHorasExtra + n.OtrosIngresos - " +
                        "n.RetencionIRPF - n.SeguridadSocial - n.OtrosDescuentos) AS SalarioNeto, " +
                        "n.FechaProceso, u.NombreCompleto AS UsuarioProceso, " +
                        "CASE WHEN n.RutaArchivo IS NOT NULL THEN 1 ELSE 0 END AS TieneArchivo")
                .From("Nominas AS n")
                .Join("Empleados AS e", "n.EmpleadoId = e.EmpleadoId")
                .Join("Usuarios AS u", "n.UsuarioProceso = u.UsuarioId")
                .OrderBy("n.FechaProceso DESC")
                .Execute();
        }

        public bool ExisteNomina(int empleadoId, string periodo)
        {
            var resultado = _queryBuilder
                .Select("COUNT(*)")
                .From("Nominas")
                .Where(true, "EmpleadoId", "=", empleadoId)
                .AndWhere(true, "Periodo", "=", periodo)
                .Execute();

            return Convert.ToInt32(resultado.Rows[0][0]) > 0;
        }

        public decimal ObtenerSalarioEmpleado(int empleadoId)
        {
            var resultado = _queryBuilder
                .Select("SalarioBase")
                .From("Empleados")
                .Where(true, "EmpleadoId", "=", empleadoId)
                .Execute();

            if (resultado.Rows.Count > 0)
            {
                return Convert.ToDecimal(resultado.Rows[0]["SalarioBase"]);
            }

            return 0;
        }

        public int CalcularDiasLaborablesMes(string periodo)
        {
            // Extraer año y mes del periodo (formato: YYYY-MM)
            string[] partes = periodo.Split('-');
            int year = Convert.ToInt32(partes[0]);
            int month = Convert.ToInt32(partes[1]);

            // Calcular días laborables (lunes a viernes) en el mes
            DateTime primerDiaMes = new DateTime(year, month, 1);
            int diasEnMes = DateTime.DaysInMonth(year, month);
            int diasLaborables = 0;

            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                DateTime fecha = new DateTime(year, month, dia);
                if (fecha.DayOfWeek != DayOfWeek.Saturday && fecha.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasLaborables++;
                }
            }

            return diasLaborables;
        }

        public int ProcesarNomina(
            int empleadoId,
            string periodo,
            int diasTrabajados,
            decimal salarioBruto,
            decimal horasExtra,
            decimal importeHorasExtra,
            decimal otrosIngresos,
            decimal retencionIRPF,
            decimal seguridadSocial,
            decimal otrosDescuentos,
            string observaciones,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO Nominas (
                    EmpleadoId,
                    Periodo,
                    DiasTrabajados,
                    SalarioBruto,
                    HorasExtra,
                    ImporteHorasExtra,
                    OtrosIngresos,
                    RetencionIRPF,
                    SeguridadSocial,
                    OtrosDescuentos,
                    Observaciones,
                    FechaProceso,
                    UsuarioProceso
                ) VALUES (
                    @EmpleadoId,
                    @Periodo,
                    @DiasTrabajados,
                    @SalarioBruto,
                    @HorasExtra,
                    @ImporteHorasExtra,
                    @OtrosIngresos,
                    @RetencionIRPF,
                    @SeguridadSocial,
                    @OtrosDescuentos,
                    @Observaciones,
                    @FechaProceso,
                    @UsuarioProceso
                ); SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@EmpleadoId", empleadoId },
                { "@Periodo", periodo },
                { "@DiasTrabajados", diasTrabajados },
                { "@SalarioBruto", salarioBruto },
                { "@HorasExtra", horasExtra },
                { "@ImporteHorasExtra", importeHorasExtra },
                { "@OtrosIngresos", otrosIngresos },
                { "@RetencionIRPF", retencionIRPF },
                { "@SeguridadSocial", seguridadSocial },
                { "@OtrosDescuentos", otrosDescuentos },
                { "@Observaciones", observaciones },
                { "@FechaProceso", DateTime.Now },
                { "@UsuarioProceso", usuarioId }
            };

            // Ejecutar la inserción y obtener el ID generado
            object result = _dbConnection.ExecuteScalar(sql, parameters);
            int nominaId = Convert.ToInt32(result);

            // Obtener nombre del empleado para el historial
            var empleado = _queryBuilder
                .Select("NombreCompleto")
                .From("Empleados")
                .Where(true, "EmpleadoId", "=", empleadoId)
                .Execute();

            string nombreEmpleado = empleado.Rows[0]["NombreCompleto"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Procesar", "Nominas", $"Nómina de {nombreEmpleado} para periodo {periodo}");

            return nominaId;
        }

        public void ActualizarArchivoNomina(
            int nominaId,
            string nombreArchivo,
            string nombreUnico,
            string rutaArchivo,
            int usuarioId)
        {
            string sql = @"
                UPDATE Nominas SET
                    NombreArchivo = @NombreArchivo,
                    NombreUnico = @NombreUnico,
                    RutaArchivo = @RutaArchivo,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE NominaId = @NominaId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NominaId", nominaId },
                { "@NombreArchivo", nombreArchivo },
                { "@NombreUnico", nombreUnico },
                { "@RutaArchivo", rutaArchivo },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "AdjuntarArchivo", "Nominas", $"Archivo adjuntado a nómina {nominaId}");
        }

        public DataTable ObtenerArchivoNomina(int nominaId)
        {
            return _queryBuilder
                .Select("NombreArchivo, NombreUnico, RutaArchivo")
                .From("Nominas")
                .Where(true, "NominaId", "=", nominaId)
                .Execute();
        }

        public void EliminarNomina(int nominaId, int usuarioId)
        {
            // Obtener información de la nómina para el historial
            var nomina = _queryBuilder
                .Select("n.EmpleadoId, e.NombreCompleto, n.Periodo, n.RutaArchivo")
                .From("Nominas AS n")
                .Join("Empleados AS e", "n.EmpleadoId = e.EmpleadoId")
                .Where(true, "n.NominaId", "=", nominaId)
                .Execute();

            if (nomina.Rows.Count == 0)
            {
                throw new Exception("La nómina no existe.");
            }

            string nombreEmpleado = nomina.Rows[0]["NombreCompleto"].ToString();
            string periodo = nomina.Rows[0]["Periodo"].ToString();
            string rutaArchivo = nomina.Rows[0]["RutaArchivo"] != DBNull.Value ?
                nomina.Rows[0]["RutaArchivo"].ToString() : null;

            // Eliminar el archivo físico si existe
            if (!string.IsNullOrEmpty(rutaArchivo) && File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }

            // Eliminar el registro de la nómina
            string sql = "DELETE FROM Nominas WHERE NominaId = @NominaId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NominaId", nominaId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "Nominas", $"Nómina de {nombreEmpleado} para periodo {periodo}");
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