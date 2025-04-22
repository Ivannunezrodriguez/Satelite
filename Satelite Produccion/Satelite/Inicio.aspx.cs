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
    public partial class Inicio : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private DashboardService _dashboardService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y el constructor de consultas
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _dashboardService = new DashboardService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Cargar dashboard con datos
                CargarDashboard();
            }
        }

        protected void CargarDashboard()
        {
            // Obtener ID de usuario de la sesión
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            string rolUsuario = Session["RolUsuario"].ToString();

            // Cargar estadísticas generales
            CargarEstadisticasGenerales();

            // Cargar tareas pendientes del usuario
            CargarTareasPendientes(usuarioId);

            // Cargar actividad reciente según el rol
            CargarActividadReciente(usuarioId, rolUsuario);
        }

        private void CargarEstadisticasGenerales()
        {
            var estadisticas = _dashboardService.ObtenerEstadisticasGenerales();

            // Asignar los valores a los controles de la UI
            lblTotalRangos.Text = estadisticas["TotalRangos"].ToString();
            lblRangosActivos.Text = estadisticas["RangosActivos"].ToString();
            lblFlujosPendientes.Text = estadisticas["FlujosPendientes"].ToString();
            lblUsuariosActivos.Text = estadisticas["UsuariosActivos"].ToString();
        }

        private void CargarTareasPendientes(int usuarioId)
        {
            // Obtener tareas pendientes para el usuario
            DataTable tareasPendientes = _dashboardService.ObtenerTareasPendientes(usuarioId);

            // Vincular los datos al GridView o repeater correspondiente
            gvTareasPendientes.DataSource = tareasPendientes;
            gvTareasPendientes.DataBind();
        }

        private void CargarActividadReciente(int usuarioId, string rolUsuario)
        {
            // Obtener la actividad reciente basada en el rol del usuario
            DataTable actividadReciente = _dashboardService.ObtenerActividadReciente(usuarioId, rolUsuario);

            // Vincular los datos al GridView o repeater correspondiente
            gvActividadReciente.DataSource = actividadReciente;
            gvActividadReciente.DataBind();
        }

        protected void btnVerDetalles_Click(object sender, EventArgs e)
        {
            // Obtener el ID del elemento seleccionado (por ejemplo, un flujo o un rango)
            Button btn = (Button)sender;
            int itemId = Convert.ToInt32(btn.CommandArgument);
            string tipoItem = btn.CommandName;

            // Redirigir a la página de detalles correspondiente
            switch (tipoItem)
            {
                case "Flujo":
                    Response.Redirect($"DetallesFlujo.aspx?id={itemId}");
                    break;
                case "Rango":
                    Response.Redirect($"DetallesRango.aspx?id={itemId}");
                    break;
                default:
                    // Manejar caso desconocido
                    break;
            }
        }
    }

    // Clase de servicio para el Dashboard
    public class DashboardService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public DashboardService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public Dictionary<string, int> ObtenerEstadisticasGenerales()
        {
            Dictionary<string, int> estadisticas = new Dictionary<string, int>();

            // Total de rangos
            var totalRangosResult = _queryBuilder
                .Select("COUNT(*)")
                .From("Rangos")
                .Execute();
            estadisticas["TotalRangos"] = Convert.ToInt32(totalRangosResult.Rows[0][0]);

            // Rangos activos
            var rangosActivosResult = _queryBuilder
                .Select("COUNT(*)")
                .From("Rangos")
                .Where(true, "Estado", "=", "Activo")
                .Execute();
            estadisticas["RangosActivos"] = Convert.ToInt32(rangosActivosResult.Rows[0][0]);

            // Flujos pendientes
            var flujosPendientesResult = _queryBuilder
                .Select("COUNT(DISTINCT f.FlujoId)")
                .From("Flujos AS f")
                .Join("FlujoDetalles AS fd", "f.FlujoId = fd.FlujoId")
                .Where(true, "fd.Estado", "=", "Pendiente")
                .Execute();
            estadisticas["FlujosPendientes"] = Convert.ToInt32(flujosPendientesResult.Rows[0][0]);

            // Usuarios activos
            var usuariosActivosResult = _queryBuilder
                .Select("COUNT(*)")
                .From("Usuarios")
                .Where(true, "Activo", "=", true)
                .Execute();
            estadisticas["UsuariosActivos"] = Convert.ToInt32(usuariosActivosResult.Rows[0][0]);

            return estadisticas;
        }

        public DataTable ObtenerTareasPendientes(int usuarioId)
        {
            return _queryBuilder
                .Select("fd.FlujoId, f.Descripcion, e.Nombre AS Etapa, r.RanCodigo, r.Rad, " +
                       "fd.FechaInicio, DATEDIFF(day, fd.FechaInicio, GETDATE()) AS DiasTranscurridos")
                .From("FlujoDetalles AS fd")
                .Join("Flujos AS f", "fd.FlujoId = f.FlujoId")
                .Join("Etapas AS e", "fd.EtapaId = e.EtapaId")
                .Join("FlujoRangos AS fr", "f.FlujoId = fr.FlujoId")
                .Join("Rangos AS r", "fr.RanAnio = r.RanAnio AND fr.RanCodigo = r.RanCodigo")
                .Where(true, "fd.ResponsableId", "=", usuarioId)
                .AndWhere(true, "fd.Estado", "=", "Pendiente")
                .OrderBy("fd.FechaInicio")
                .Execute();
        }

        public DataTable ObtenerActividadReciente(int usuarioId, string rolUsuario)
        {
            // La consulta varía según el rol del usuario
            if (rolUsuario == "Administrador")
            {
                // Los administradores ven toda la actividad
                return _queryBuilder
                    .Select("h.FechaRegistro, u.NombreCompleto AS Usuario, h.Accion, " +
                           "h.TablaAfectada, h.RegistroId, h.Detalles")
                    .From("HistorialAcciones AS h")
                    .Join("Usuarios AS u", "h.UsuarioId = u.UsuarioId")
                    .OrderBy("h.FechaRegistro DESC")
                    .Execute();
            }
            else
            {
                // Otros usuarios solo ven su actividad y la relacionada con sus tareas
                return _queryBuilder
                    .Select("h.FechaRegistro, u.NombreCompleto AS Usuario, h.Accion, " +
                           "h.TablaAfectada, h.RegistroId, h.Detalles")
                    .From("HistorialAcciones AS h")
                    .Join("Usuarios AS u", "h.UsuarioId = u.UsuarioId")
                    .Where(true, "h.UsuarioId", "=", usuarioId)
                    .OrWhere(true, "h.RegistroId", "IN",
                            "(SELECT fd.FlujoId FROM FlujoDetalles AS fd WHERE fd.ResponsableId = @ResponsableId)")
                    .OrderBy("h.FechaRegistro DESC")
                    .Execute();
            }
        }
    }
}