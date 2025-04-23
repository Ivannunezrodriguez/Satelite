using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Satelite
{
    public class FlujoDatos
    {
        // Instancia del patrón Fluent API
        private DatabaseConnection _dbConnection;
        private DatabaseQueryBuilder _queryBuilder;

        public FlujoDatos()
        {
            // Inicializar la conexión a la base de datos y el constructor de consultas
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);
        }

        // Método para obtener registros con el patrón Fluent API
        public DataTable ObtenerRegistros(string tableName, Dictionary<string, object> filtros = null)
        {
            var query = _queryBuilder
                .Select("*")
                .From(tableName);

            // Agregar condiciones de filtro si existen
            if (filtros != null)
            {
                bool isFirst = true;
                foreach (var filtro in filtros)
                {
                    if (isFirst)
                    {
                        query.Where(true, filtro.Key, "=", filtro.Value);
                        isFirst = false;
                    }
                    else
                    {
                        query.AndWhere(true, filtro.Key, "=", filtro.Value);
                    }
                }
            }

            return query.Execute();
        }

        // Método para obtener flujos de datos específicos
        public DataTable ObtenerFlujos(int anio, string codigo)
        {
            return _queryBuilder
                .Select("f.FlujoId, f.Descripcion, f.FechaCreacion, f.UsuarioCreacion")
                .From("Flujos AS f")
                .Join("FlujoRangos AS fr", "f.FlujoId = fr.FlujoId")
                .Where(true, "fr.RanAnio", "=", anio)
                .AndWhere(true, "fr.RanCodigo", "=", codigo)
                .OrderBy("f.FechaCreacion DESC")
                .Execute();
        }

        // Método para obtener detalles de un flujo específico
        public DataTable ObtenerDetallesFlujo(int flujoId)
        {
            return _queryBuilder
                .Select("fd.EtapaId, e.Nombre AS EtapaNombre, fd.ResponsableId, u.NombreCompleto AS Responsable, " +
                        "fd.FechaInicio, fd.FechaFin, fd.Estado, fd.Comentarios")
                .From("FlujoDetalles AS fd")
                .Join("Etapas AS e", "fd.EtapaId = e.EtapaId")
                .Join("Usuarios AS u", "fd.ResponsableId = u.UsuarioId")
                .Where(true, "fd.FlujoId", "=", flujoId)
                .OrderBy("fd.Orden")
                .Execute();
        }

        // Método para crear un nuevo flujo
        public int CrearFlujo(string descripcion, string usuarioCreacion, int ranAnio, string ranCodigo)
        {
            // Primero crear el flujo
            string insertFlujoSql = "INSERT INTO Flujos (Descripcion, FechaCreacion, UsuarioCreacion) " +
                                   "VALUES (@Descripcion, @FechaCreacion, @UsuarioCreacion); " +
                                   "SELECT SCOPE_IDENTITY();";

            Dictionary<string, object> flujoParams = new Dictionary<string, object>
            {
                { "@Descripcion", descripcion },
                { "@FechaCreacion", DateTime.Now },
                { "@UsuarioCreacion", usuarioCreacion }
            };

            // Ejecutar la inserción y obtener el ID generado
            object result = _dbConnection.ExecuteScalar(insertFlujoSql, flujoParams);
            int flujoId = Convert.ToInt32(result);

            // Luego asociar el flujo con el rango
            string insertFlujoRangoSql = "INSERT INTO FlujoRangos (FlujoId, RanAnio, RanCodigo) " +
                                        "VALUES (@FlujoId, @RanAnio, @RanCodigo)";

            Dictionary<string, object> flujoRangoParams = new Dictionary<string, object>
            {
                { "@FlujoId", flujoId },
                { "@RanAnio", ranAnio },
                { "@RanCodigo", ranCodigo }
            };

            _dbConnection.ExecuteNonQuery(insertFlujoRangoSql, flujoRangoParams);

            return flujoId;
        }

        // Método para agregar una etapa al flujo
        public void AgregarEtapaFlujo(int flujoId, int etapaId, int responsableId, DateTime fechaInicio, string estado)
        {
            string sql = "INSERT INTO FlujoDetalles (FlujoId, EtapaId, ResponsableId, FechaInicio, Estado, Orden) " +
                        "VALUES (@FlujoId, @EtapaId, @ResponsableId, @FechaInicio, @Estado, " +
                        "(SELECT ISNULL(MAX(Orden), 0) + 1 FROM FlujoDetalles WHERE FlujoId = @FlujoId))";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@FlujoId", flujoId },
                { "@EtapaId", etapaId },
                { "@ResponsableId", responsableId },
                { "@FechaInicio", fechaInicio },
                { "@Estado", estado }
            };

            _dbConnection.ExecuteNonQuery(sql, parameters);
        }

        // Método para actualizar el estado de una etapa
        public void ActualizarEtapaFlujo(int flujoId, int etapaId, string estado, string comentarios, DateTime? fechaFin = null)
        {
            string sql = "UPDATE FlujoDetalles SET Estado = @Estado, Comentarios = @Comentarios";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@FlujoId", flujoId },
                { "@EtapaId", etapaId },
                { "@Estado", estado },
                { "@Comentarios", comentarios }
            };

            if (fechaFin.HasValue)
            {
                sql += ", FechaFin = @FechaFin";
                parameters.Add("@FechaFin", fechaFin.Value);
            }

            sql += " WHERE FlujoId = @FlujoId AND EtapaId = @EtapaId";

            _dbConnection.ExecuteNonQuery(sql, parameters);
        }

        // Método para obtener las etapas disponibles
        public DataTable ObtenerEtapas()
        {
            return _queryBuilder
                .Select("EtapaId, Nombre, Descripcion")
                .From("Etapas")
                .OrderBy("Orden")
                .Execute();
        }

        // Método para obtener los responsables disponibles
        public DataTable ObtenerResponsables()
        {
            return _queryBuilder
                .Select("UsuarioId, NombreCompleto, Email")
                .From("Usuarios")
                .Where(true, "Activo", "=", true)
                .OrderBy("NombreCompleto")
                .Execute();
        }
    }

    #region Database Connection Additions
    // Añadir un método adicional a la clase DatabaseConnection
    public partial class DatabaseConnection
    {
        public object ExecuteScalar(string query, Dictionary<string, object> parameters)
        {
            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros para prevenir inyección SQL
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        return command.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine("Error en la operación: " + ex.Message);
                        throw;
                    }
                }
            }
        }
    }
    #endregion
}