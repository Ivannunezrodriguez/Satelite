using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Satelite
{
    public partial class Usuarios : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;
        private UsuarioService _usuarioService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar la conexión a la base de datos y los servicios
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
            _usuarioService = new UsuarioService(_dbConnection, _queryBuilder);

            if (!IsPostBack)
            {
                // Verificar sesión de usuario y permisos de administrador
                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Session["RolUsuario"] == null || Session["RolUsuario"].ToString() != "Administrador")
                {
                    Response.Redirect("AccesoDenegado.aspx");
                    return;
                }

                // Inicializar controles
                CargarRoles();
                CargarUsuarios();
            }
        }

        protected void CargarRoles()
        {
            DataTable roles = _usuarioService.ObtenerRoles();

            ddlRol.DataSource = roles;
            ddlRol.DataTextField = "Nombre";
            ddlRol.DataValueField = "RolId";
            ddlRol.DataBind();
        }

        protected void CargarUsuarios()
        {
            DataTable usuarios = _usuarioService.ObtenerUsuarios();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();

            // Mostrar mensaje si no hay usuarios
            lblSinUsuarios.Visible = (usuarios.Rows.Count == 0);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar datos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarMensaje("Por favor complete los campos requeridos: Nombre y Email.", false);
                return;
            }

            try
            {
                // Obtener datos del formulario
                string nombre = txtNombre.Text.Trim();
                string email = txtEmail.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                int rolId = Convert.ToInt32(ddlRol.SelectedValue);
                bool activo = chkActivo.Checked;
                int usuarioActualId = Convert.ToInt32(Session["UsuarioId"]);

                // Validar formato de email
                if (!IsValidEmail(email))
                {
                    MostrarMensaje("Por favor ingrese una dirección de email válida.", false);
                    return;
                }

                if (ViewState["UsuarioId"] != null) // Editar usuario existente
                {
                    int usuarioId = Convert.ToInt32(ViewState["UsuarioId"]);

                    // Verificar si se está cambiando la contraseña
                    string nuevaPassword = null;
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        nuevaPassword = HashPassword(txtPassword.Text);
                    }

                    _usuarioService.ActualizarUsuario(usuarioId, nombre, email, telefono, rolId, activo, usuarioActualId, nuevaPassword);
                    MostrarMensaje("Usuario actualizado exitosamente.", true);
                }
                else // Nuevo usuario
                {
                    // Validar que se haya ingresado una contraseña
                    if (string.IsNullOrEmpty(txtPassword.Text))
                    {
                        MostrarMensaje("Por favor ingrese una contraseña para el nuevo usuario.", false);
                        return;
                    }

                    // Encriptar contraseña
                    string password = HashPassword(txtPassword.Text);

                    _usuarioService.CrearUsuario(nombre, email, telefono, password, rolId, activo, usuarioActualId);
                    MostrarMensaje("Usuario creado exitosamente.", true);
                }

                // Limpiar formulario y recargar datos
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int usuarioId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Cargar datos del usuario para edición
                DataTable usuarioData = _usuarioService.ObtenerUsuarioPorId(usuarioId);

                if (usuarioData.Rows.Count > 0)
                {
                    DataRow row = usuarioData.Rows[0];

                    // Establecer valores en el formulario
                    txtNombre.Text = row["NombreCompleto"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtTelefono.Text = row["Telefono"].ToString();
                    ddlRol.SelectedValue = row["RolId"].ToString();
                    chkActivo.Checked = Convert.ToBoolean(row["Activo"]);

                    // Limpiar campo de contraseña y cambiar placeholder
                    txtPassword.Text = string.Empty;
                    txtPassword.Attributes["placeholder"] = "Dejar en blanco para mantener la actual";

                    // Guardar ID de usuario para actualización
                    ViewState["UsuarioId"] = usuarioId;

                    // Cambiar texto del botón
                    btnGuardar.Text = "Actualizar";
                }
            }
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    // Validar que no sea el usuario actual
                    int usuarioActualId = Convert.ToInt32(Session["UsuarioId"]);
                    if (usuarioId == usuarioActualId)
                    {
                        MostrarMensaje("No puede eliminar su propio usuario.", false);
                        return;
                    }

                    // Eliminar usuario (baja lógica)
                    _usuarioService.EliminarUsuario(usuarioId, usuarioActualId);

                    MostrarMensaje("Usuario eliminado exitosamente.", true);

                    // Recargar datos
                    CargarUsuarios();
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al eliminar: " + ex.Message, false);
                }
            }
            else if (e.CommandName == "ResetPassword")
            {
                try
                {
                    // Generar contraseña temporal
                    string tempPassword = GenerateRandomPassword(8);
                    string hashedPassword = HashPassword(tempPassword);

                    // Actualizar contraseña del usuario
                    int usuarioActualId = Convert.ToInt32(Session["UsuarioId"]);
                    _usuarioService.ResetearPassword(usuarioId, hashedPassword, usuarioActualId);

                    // Mostrar contraseña temporal al administrador
                    MostrarMensaje($"Contraseña restablecida. Contraseña temporal: {tempPassword}", true);
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al restablecer contraseña: " + ex.Message, false);
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
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPassword.Attributes["placeholder"] = "Contraseña";

            // Restablecer rol y estado por defecto
            if (ddlRol.Items.Count > 0)
                ddlRol.SelectedIndex = 0;

            chkActivo.Checked = true;

            // Limpiar ID de usuario y restablecer botón
            ViewState["UsuarioId"] = null;
            btnGuardar.Text = "Guardar";
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
            lblMensaje.Visible = true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(validChars.Length);
                sb.Append(validChars[index]);
            }

            return sb.ToString();
        }
    }

    // Clase de servicio para gestión de usuarios
    public class UsuarioService
    {
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public UsuarioService(DatabaseConnection dbConnection, DatabaseQueryBuilder queryBuilder)
        {
            _dbConnection = dbConnection;
            _queryBuilder = queryBuilder;
        }

        public DataTable ObtenerRoles()
        {
            return _queryBuilder
                .Select("RolId, Nombre, Descripcion")
                .From("Roles")
                .OrderBy("Nombre")
                .Execute();
        }

        public DataTable ObtenerUsuarios()
        {
            return _queryBuilder
                .Select("u.UsuarioId, u.NombreCompleto, u.Email, u.Telefono, " +
                        "r.Nombre AS Rol, u.UltimoAcceso, u.Activo")
                .From("Usuarios AS u")
                .Join("Roles AS r", "u.RolId = r.RolId")
                .OrderBy("u.NombreCompleto")
                .Execute();
        }

        public DataTable ObtenerUsuarioPorId(int usuarioId)
        {
            return _queryBuilder
                .Select("UsuarioId, NombreCompleto, Email, Telefono, RolId, Activo")
                .From("Usuarios")
                .Where(true, "UsuarioId", "=", usuarioId)
                .Execute();
        }

        public bool ExisteUsuarioConEmail(string email, int? usuarioId = null)
        {
            var query = _queryBuilder
                .Select("COUNT(*)")
                .From("Usuarios")
                .Where(true, "Email", "=", email);

            if (usuarioId.HasValue)
            {
                query.AndWhere(true, "UsuarioId", "!=", usuarioId.Value);
            }

            var resultado = query.Execute();

            return Convert.ToInt32(resultado.Rows[0][0]) > 0;
        }

        public void CrearUsuario(
            string nombre,
            string email,
            string telefono,
            string password,
            int rolId,
            bool activo,
            int usuarioCreadorId)
        {
            // Verificar que no exista un usuario con el mismo email
            if (ExisteUsuarioConEmail(email))
            {
                throw new Exception("Ya existe un usuario con este email.");
            }

            string sql = @"
                INSERT INTO Usuarios (
                    NombreCompleto,
                    Email,
                    Telefono,
                    Password,
                    RolId,
                    Activo,
                    FechaCreacion,
                    UsuarioCreacion
                ) VALUES (
                    @NombreCompleto,
                    @Email,
                    @Telefono,
                    @Password,
                    @RolId,
                    @Activo,
                    @FechaCreacion,
                    @UsuarioCreacion
                )";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@NombreCompleto", nombre },
                { "@Email", email },
                { "@Telefono", telefono },
                { "@Password", password },
                { "@RolId", rolId },
                { "@Activo", activo },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioCreadorId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioCreadorId, "Crear", "Usuarios", nombre);
        }

        public void ActualizarUsuario(
            int usuarioId,
            string nombre,
            string email,
            string telefono,
            int rolId,
            bool activo,
            int usuarioModificadorId,
            string nuevaPassword = null)
        {
            // Verificar que no exista otro usuario con el mismo email
            if (ExisteUsuarioConEmail(email, usuarioId))
            {
                throw new Exception("Ya existe otro usuario con este email.");
            }

            string sql;
            Dictionary<string, object> parameters;

            if (nuevaPassword != null)
            {
                // Actualizar incluyendo la contraseña
                sql = @"
                    UPDATE Usuarios SET
                        NombreCompleto = @NombreCompleto,
                        Email = @Email,
                        Telefono = @Telefono,
                        Password = @Password,
                        RolId = @RolId,
                        Activo = @Activo,
                        FechaModificacion = @FechaModificacion,
                        UsuarioModificacion = @UsuarioModificacion
                    WHERE UsuarioId = @UsuarioId";

                parameters = new Dictionary<string, object>
                {
                    { "@UsuarioId", usuarioId },
                    { "@NombreCompleto", nombre },
                    { "@Email", email },
                    { "@Telefono", telefono },
                    { "@Password", nuevaPassword },
                    { "@RolId", rolId },
                    { "@Activo", activo },
                    { "@FechaModificacion", DateTime.Now },
                    { "@UsuarioModificacion", usuarioModificadorId }
                };
            }
            else
            {
                // Actualizar sin cambiar la contraseña
                sql = @"
                    UPDATE Usuarios SET
                        NombreCompleto = @NombreCompleto,
                        Email = @Email,
                        Telefono = @Telefono,
                        RolId = @RolId,
                        Activo = @Activo,
                        FechaModificacion = @FechaModificacion,
                        UsuarioModificacion = @UsuarioModificacion
                    WHERE UsuarioId = @UsuarioId";

                parameters = new Dictionary<string, object>
                {
                    { "@UsuarioId", usuarioId },
                    { "@NombreCompleto", nombre },
                    { "@Email", email },
                    { "@Telefono", telefono },
                    { "@RolId", rolId },
                    { "@Activo", activo },
                    { "@FechaModificacion", DateTime.Now },
                    { "@UsuarioModificacion", usuarioModificadorId }
                };
            }

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioModificadorId, "Actualizar", "Usuarios", nombre);
        }

        public void EliminarUsuario(int usuarioId, int usuarioModificadorId)
        {
            // Verificar que no sea el usuario administrador principal
            var esAdminPrincipal = _queryBuilder
                .Select("COUNT(*)")
                .From("Usuarios")
                .Where(true, "UsuarioId", "=", usuarioId)
                .AndWhere(true, "Email", "=", "admin@sistema.com")
                .Execute();

            if (Convert.ToInt32(esAdminPrincipal.Rows[0][0]) > 0)
            {
                throw new Exception("No se puede eliminar el usuario administrador principal.");
            }

            // Obtener nombre de usuario para el historial
            var usuario = ObtenerUsuarioPorId(usuarioId);
            string nombreUsuario = usuario.Rows[0]["NombreCompleto"].ToString();

            // Implementar baja lógica en lugar de eliminar físicamente
            string sql = @"
                UPDATE Usuarios SET
                    Activo = 0,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE UsuarioId = @UsuarioId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UsuarioId", usuarioId },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioModificadorId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioModificadorId, "Eliminar", "Usuarios", nombreUsuario);
        }

        public void ResetearPassword(int usuarioId, string nuevaPassword, int usuarioModificadorId)
        {
            string sql = @"
                UPDATE Usuarios SET
                    Password = @Password,
                    PasswordCambiada = 0,
                    FechaModificacion = @FechaModificacion,
                    UsuarioModificacion = @UsuarioModificacion
                WHERE UsuarioId = @UsuarioId";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@UsuarioId", usuarioId },
                { "@Password", nuevaPassword },
                { "@FechaModificacion", DateTime.Now },
                { "@UsuarioModificacion", usuarioModificadorId }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);

            // Obtener nombre de usuario para el historial
            var usuario = ObtenerUsuarioPorId(usuarioId);
            string nombreUsuario = usuario.Rows[0]["NombreCompleto"].ToString();

            // Registrar acción en historial
            RegistrarAccionHistorial(usuarioModificadorId, "ResetPassword", "Usuarios", nombreUsuario);
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