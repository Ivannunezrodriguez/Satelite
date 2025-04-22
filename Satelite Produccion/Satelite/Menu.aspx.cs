using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;

namespace Satelite
{
    public partial class Menu : System.Web.UI.UserControl
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private MenuService _menuService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _menuService = new MenuService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Cargar menú según el rol del usuario
                CargarMenu();
            }
        }

        protected void CargarMenu()
        {
            try
            {
                // Obtener información del usuario
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                int rolId = Convert.ToInt32(Session["RolId"]);
                string rolNombre = Session["RolUsuario"].ToString();
                string nombreUsuario = Session["NombreUsuario"].ToString();

                // Establecer información del usuario en el menú
                lblNombreUsuario.Text = nombreUsuario;
                lblRolUsuario.Text = rolNombre;

                // Obtener opciones de menú según el rol del usuario
                DataTable menuItems = _menuService.ObtenerMenuPorRol(rolId);

                // Construir el menú
                StringBuilder menuHtml = new StringBuilder();

                // Construir menú principal
                Dictionary<int, List<DataRow>> menuAgrupado = AgruparMenuPorPadre(menuItems);

                // Generar HTML del menú
                if (menuAgrupado.ContainsKey(0)) // Elementos de primer nivel (sin padre)
                {
                    foreach (DataRow item in menuAgrupado[0])
                    {
                        int menuId = Convert.ToInt32(item["MenuId"]);
                        string titulo = item["Titulo"].ToString();
                        string icono = item["Icono"].ToString();
                        string url = item["URL"].ToString();

                        // Verificar si el elemento tiene hijos
                        bool tieneHijos = menuAgrupado.ContainsKey(menuId);

                        if (tieneHijos)
                        {
                            // Elemento con submenú
                            menuHtml.AppendLine("<li class=\"nav-item dropdown\">");
                            menuHtml.AppendLine($"<a class=\"nav-link dropdown-toggle\" href=\"#\" id=\"menu{menuId}\" role=\"button\" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\">");
                            menuHtml.AppendLine($"<i class=\"{icono}\"></i> {titulo}");
                            menuHtml.AppendLine("</a>");
                            menuHtml.AppendLine($"<div class=\"dropdown-menu\" aria-labelledby=\"menu{menuId}\">");

                            // Agregar elementos del submenú
                            foreach (DataRow subItem in menuAgrupado[menuId])
                            {
                                string subTitulo = subItem["Titulo"].ToString();
                                string subIcono = subItem["Icono"].ToString();
                                string subUrl = subItem["URL"].ToString();

                                menuHtml.AppendLine($"<a class=\"dropdown-item\" href=\"{subUrl}\"><i class=\"{subIcono}\"></i> {subTitulo}</a>");
                            }

                            menuHtml.AppendLine("</div>");
                            menuHtml.AppendLine("</li>");
                        }
                        else
                        {
                            // Elemento sin submenú
                            menuHtml.AppendLine("<li class=\"nav-item\">");
                            menuHtml.AppendLine($"<a class=\"nav-link\" href=\"{url}\"><i class=\"{icono}\"></i> {titulo}</a>");
                            menuHtml.AppendLine("</li>");
                        }
                    }
                }

                // Asignar el HTML generado al contenedor del menú
                litMenu.Text = menuHtml.ToString();

                // Configurar opciones específicas según el rol
                ConfigurarOpcionesEspeciales(rolNombre);
            }
            catch (Exception ex)
            {
                // Registrar error
                Console.WriteLine("Error al cargar menú: " + ex.Message);

                // Mostrar menú básico o de emergencia
                MostrarMenuEmergencia();
            }
        }

        private Dictionary<int, List<DataRow>> AgruparMenuPorPadre(DataTable menuItems)
        {
            Dictionary<int, List<DataRow>> menuAgrupado = new Dictionary<int, List<DataRow>>();

            foreach (DataRow row in menuItems.Rows)
            {
                // Obtener el ID del menú padre (0 si es elemento raíz)
                int padreId = row["MenuPadreId"] == DBNull.Value ? 0 : Convert.ToInt32(row["MenuPadreId"]);

                // Crear la lista si no existe
                if (!menuAgrupado.ContainsKey(padreId))
                {
                    menuAgrupado[padreId] = new List<DataRow>();
                }

                // Agregar el elemento a su grupo correspondiente
                menuAgrupado[padreId].Add(row);
            }

            return menuAgrupado;
        }

        private void ConfigurarOpcionesEspeciales(string rolNombre)
        {
            // Configurar opciones específicas según el rol
            // Por ejemplo, mostrar u ocultar ciertas opciones administrativas

            // Opción de administración solo visible para administradores
            pnlAdministracion.Visible = (rolNombre == "Administrador");

            // Otras configuraciones específicas según rol
            // ...
        }

        private void MostrarMenuEmergencia()
        {
            // Menú básico en caso de error
            StringBuilder menuHtml = new StringBuilder();

            menuHtml.AppendLine("<li class=\"nav-item\">");
            menuHtml.AppendLine("<a class=\"nav-link\" href=\"Inicio.aspx\"><i class=\"fa fa-home\"></i> Inicio</a>");
            menuHtml.AppendLine("</li>");

            menuHtml.AppendLine("<li class=\"nav-item\">");
            menuHtml.AppendLine("<a class=\"nav-link\" href=\"Consulta.aspx\"><i class=\"fa fa-search\"></i> Consulta</a>");
            menuHtml.AppendLine("</li>");

            // Asignar el HTML generado al contenedor del menú
            litMenu.Text = menuHtml.ToString();

            // Ocultar paneles de opciones especiales
            pnlAdministracion.Visible = false;
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            // Registrar cierre de sesión en el historial si hay usuario en sesión
            if (Session["UsuarioId"] != null)
            {
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                _menuService.RegistrarCierreSesion(usuarioId);
            }

            // Limpiar sesión
            Session.Clear();
            Session.Abandon();

            // Redirigir a la página de login
            Response.Redirect("Login.aspx");
        }
    }

    // Clase de servicio para gestión del menú
    public class MenuService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public MenuService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerMenuPorRol(int rolId)
        {
            return _queryBuilder
                .Select("m.MenuId, m.MenuPadreId, m.Titulo, m.Descripcion, " +
                        "m.URL, m.Icono, m.Orden")
                .From("Menus AS m")
                .Join("MenuRoles AS mr", "m.MenuId = mr.MenuId")
                .Where(true, "mr.RolId", "=", rolId)
                .AndWhere(true, "m.Activo", "=", true)
                .OrderBy("m.MenuPadreId, m.Orden")
                .Execute();
        }

        public DataTable ObtenerTodosMenus()
        {
            return _queryBuilder
                .Select("m.MenuId, m.MenuPadreId, m.Titulo, m.Descripcion, " +
                        "m.URL, m.Icono, m.Orden, m.Activo")
                .From("Menus AS m")
                .OrderBy("m.MenuPadreId, m.Orden")
                .Execute();
        }

        public void RegistrarCierreSesion(int usuarioId)
        {
            RegistrarAccionHistorial(usuarioId, "CerrarSesion", "Usuarios", "Cierre de sesión");
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

    // Clase de servicio para gestión de menús administrativos
    public class MenuAdminService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public MenuAdminService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerMenusDisponibles()
        {
            return _queryBuilder
                .Select("MenuId, MenuPadreId, Titulo, Descripcion, URL, Icono, Orden, Activo")
                .From("Menus")
                .OrderBy("MenuPadreId, Orden")
                .Execute();
        }

        public DataTable ObtenerRoles()
        {
            return _queryBuilder
                .Select("RolId, Nombre, Descripcion")
                .From("Roles")
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerMenuPorId(int menuId)
        {
            return _queryBuilder
                .Select("MenuId, MenuPadreId, Titulo, Descripcion, URL, Icono, Orden, Activo")
                .From("Menus")
                .Where(true, "MenuId", "=", menuId)
                .Execute();
        }

        public DataTable ObtenerRolesPorMenu(int menuId)
        {
            return _queryBuilder
                .Select("r.RolId, r.Nombre, r.Descripcion, " +
                        "CASE WHEN mr.MenuId IS NOT NULL THEN 1 ELSE 0 END AS Asignado")
                .From("Roles AS r")
                .LeftJoin("MenuRoles AS mr", "r.RolId = mr.RolId AND mr.MenuId = @MenuId")
                .OrderBy("r.Nombre")
                .Execute();
        }

        public void CrearMenu(
            int? menuPadreId,
            string titulo,
            string descripcion,
            string url,
            string icono,
            int orden,
            bool activo,
            int usuarioId)
        {
            string sql = @"
                INSERT INTO Menus (
                    MenuPadreId,
                    Titulo,
                    Descripcion,
                    URL,
                    Icono,
                    Orden,
                    Activo,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @MenuPadreId,
                    @Titulo,
                    @Descripcion,
                    @URL,
                    @Icono,
                    @Orden,
                    @Activo,
                    @FechaCreacion,
                    @UsuarioCreacion
                ); SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MenuPadreId", menuPadreId.HasValue ? (object)menuPadreId.Value : DBNull.Value },
                { "@Titulo", titulo },
                { "@Descripcion", descripcion },
                { "@URL", url },
                { "@Icono", icono },
                { "@Orden", orden },
                { "@Activo", activo },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioId }
            };

            var result = _dbConnection.ExecuteScalar(sql, parameters);
            int menuId = Convert.ToInt32(result);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Crear", "Menus", titulo);

            return;
        }

        public void ActualizarMenu(
            int menuId,
            int? menuPadreId,
            string titulo,
            string descripcion,
            string url,
            string icono,
            int orden,
            bool activo,
            int usuarioId)
        {
            string sql = @"
                UPDATE Menus SET
                    MenuPadreId = @MenuPadreId,
                    Titulo = @Titulo,
                    Descripcion = @Descripcion,
                    URL = @URL,
                    Icono = @Icono,
                    Orden = @Orden,
                    Activo = @Activo,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE MenuId = @MenuId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MenuId", menuId },
                { "@MenuPadreId", menuPadreId.HasValue ? (object)menuPadreId.Value : DBNull.Value },
                { "@Titulo", titulo },
                { "@Descripcion", descripcion },
                { "@URL", url },
                { "@Icono", icono },
                { "@Orden", orden },
                { "@Activo", activo },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Actualizar", "Menus", titulo);
        }

        public void EliminarMenu(int menuId, int usuarioId)
        {
            // Obtener información del menú para el historial
            var menu = ObtenerMenuPorId(menuId);
            string tituloMenu = menu.Rows[0]["Titulo"].ToString();

            // Verificar que no tenga elementos hijos
            var tieneHijos = _queryBuilder
                .Select("COUNT(*)")
                .From("Menus")
                .Where(true, "MenuPadreId", "=", menuId)
                .Execute();

            if (Convert.ToInt32(tieneHijos.Rows[0][0]) > 0)
            {
                throw new Exception("No se puede eliminar el menú porque tiene elementos hijos asociados.");
            }

            // Primero eliminar relaciones en MenuRoles
            string sqlMenuRoles = "DELETE FROM MenuRoles WHERE MenuId = @MenuId";

            Dictionary<string, object> parametersMenuRoles = new Dictionary<string, object>
            {
                { "@MenuId", menuId }
            };

            _dbConnection.ExecuteNonQuery(sqlMenuRoles, parametersMenuRoles);

            // Luego eliminar el menú
            string sqlMenu = "DELETE FROM Menus WHERE MenuId = @MenuId";

            Dictionary<string, object> parametersMenu = new Dictionary<string, object>
            {
                { "@MenuId", menuId }
            };

            _dbConnection.ExecuteNonQuery(sqlMenu, parametersMenu);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Eliminar", "Menus", tituloMenu);
        }

        public void AsignarMenuARol(int menuId, int rolId, int usuarioId)
        {
            // Verificar si ya existe la asignación
            var asignacionExiste = _queryBuilder
                .Select("COUNT(*)")
                .From("MenuRoles")
                .Where(true, "MenuId", "=", menuId)
                .AndWhere(true, "RolId", "=", rolId)
                .Execute();

            if (Convert.ToInt32(asignacionExiste.Rows[0][0]) == 0)
            {
                // No existe, crear la asignación
                string sql = "INSERT INTO MenuRoles (MenuId, RolId) VALUES (@MenuId, @RolId)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@MenuId", menuId },
                    { "@RolId", rolId }
                };

                _dbConnection.ExecuteNonQuery(sql, parameters);

                // Obtener información para el historial
                var menu = ObtenerMenuPorId(menuId);
                string tituloMenu = menu.Rows[0]["Titulo"].ToString();

                var rol = _queryBuilder
                    .Select("Nombre")
                    .From("Roles")
                    .Where(true, "RolId", "=", rolId)
                    .Execute();

                string nombreRol = rol.Rows[0]["Nombre"].ToString();

                // Registrar acción en historial
                RegistrarAccionHistorial(usuarioId, "Asignar", "MenuRoles", $"Menú '{tituloMenu}' asignado a rol '{nombreRol}'");
            }
        }

        public void EliminarMenuDeRol(int menuId, int rolId, int usuarioId)
        {
            // Obtener información para el historial
            var menu = ObtenerMenuPorId(menuId);
            string tituloMenu = menu.Rows[0]["Titulo"].ToString();

            var rol = _queryBuilder
                .Select("Nombre")
                .From("Roles")
                .Where(true, "RolId", "=", rolId)
                .Execute();

            string nombreRol = rol.Rows[0]["Nombre"].ToString();

            // Eliminar la asignación
            string sql = "DELETE FROM MenuRoles WHERE MenuId = @MenuId AND RolId = @RolId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@MenuId", menuId },
                { "@RolId", rolId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioId, "Desasignar", "MenuRoles", $"Menú '{tituloMenu}' eliminado del rol '{nombreRol}'");
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