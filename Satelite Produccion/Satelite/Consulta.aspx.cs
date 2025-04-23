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
    public partial class Consulta : System.Web.UI.Page
    {
        // Instancias del patrón Fluent API
        private DatabaseQueryBuilder _queryBuilder;
        private DatabaseConnection _dbConnection;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Inicializar el builder y la conexión
            _dbConnection = new DatabaseConnection(ConfigurationManager.ConnectionStrings["SateliteConnection"].ConnectionString);
            _queryBuilder = new DatabaseQueryBuilder(_dbConnection);

            if (!IsPostBack)
            {
                LlenarCombos();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Usar Fluent API para construir y ejecutar la consulta
            string referenciaRanCodigo = RanCodigo.Text.Trim();
            string referenciaRad = Rad.Text.Trim();

            var resultado = _queryBuilder
                .Select("t1.RanAnio, t1.RanCodigo, t1.Rad, t2.RequisitoCodigo, t3.Observacion, t1.RanGenerado")
                .From("Rangos AS t1")
                .Join("RangoRequisito AS t2", "t1.RanAnio = t2.RanAnio AND t1.RanCodigo = t2.RanCodigo")
                .LeftJoin("Requisitos AS t3", "t2.RequisitoCodigo = t3.RequisitoCodigo")
                .Where(!string.IsNullOrEmpty(referenciaRanCodigo), "t1.RanCodigo", "=", referenciaRanCodigo)
                .AndWhere(!string.IsNullOrEmpty(referenciaRad), "t1.Rad", "=", referenciaRad)
                .OrderBy("t1.RanAnio")
                .Execute();

            GridView1.DataSource = resultado;
            GridView1.DataBind();
        }

        protected void LlenarCombos()
        {
            // Usar Fluent API para llenar los combos
            var resultadoEstudiantes = _queryBuilder
                .Select("EstudianteId, NombreCompleto")
                .From("Estudiantes")
                .OrderBy("NombreCompleto")
                .Execute();

            // Aquí completarías el llenado de combos según tu lógica actual
            // ComboEstudiantes.DataSource = resultadoEstudiantes;
            // ComboEstudiantes.DataTextField = "NombreCompleto";
            // ComboEstudiantes.DataValueField = "EstudianteId";
            // ComboEstudiantes.DataBind();
        }

        // Otros métodos pueden seguir el mismo patrón de uso del Fluent API
    }

    // Implementaciones concretas del Fluent API

    #region Database Connection
    public class DatabaseConnection
    {
        private string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
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

                    DataTable dataTable = new DataTable();
                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine("Error en la consulta: " + ex.Message);
                        throw;
                    }
                    return dataTable;
                }
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
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
                        return command.ExecuteNonQuery();
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

    #region Query Builder
    public class DatabaseQueryBuilder
    {
        private string _selectClause = string.Empty;
        private string _fromClause = string.Empty;
        private List<string> _joinClauses = new List<string>();
        private List<string> _whereClauses = new List<string>();
        private string _orderByClause = string.Empty;
        private string _groupByClause = string.Empty;
        private Dictionary<string, object> _parameters = new Dictionary<string, object>();
        private int _paramCount = 0;
        private DatabaseConnection _connection;

        public DatabaseQueryBuilder(DatabaseConnection connection)
        {
            _connection = connection;
        }

        public DatabaseQueryBuilder Select(string columns)
        {
            _selectClause = "SELECT " + columns;
            return this;
        }

        public DatabaseQueryBuilder From(string table)
        {
            _fromClause = "FROM " + table;
            return this;
        }

        public DatabaseQueryBuilder Join(string table, string condition)
        {
            _joinClauses.Add("INNER JOIN " + table + " ON " + condition);
            return this;
        }

        public DatabaseQueryBuilder LeftJoin(string table, string condition)
        {
            _joinClauses.Add("LEFT JOIN " + table + " ON " + condition);
            return this;
        }

        public DatabaseQueryBuilder Where(bool condition, string column, string operation, object value)
        {
            if (condition)
            {
                string paramName = "@p" + _paramCount++;
                _whereClauses.Add(column + " " + operation + " " + paramName);
                _parameters.Add(paramName, value);
            }
            return this;
        }

        public DatabaseQueryBuilder AndWhere(bool condition, string column, string operation, object value)
        {
            if (condition && _whereClauses.Count > 0)
            {
                string paramName = "@p" + _paramCount++;
                _whereClauses.Add("AND " + column + " " + operation + " " + paramName);
                _parameters.Add(paramName, value);
            }
            else if (condition)
            {
                return Where(true, column, operation, value);
            }
            return this;
        }

        public DatabaseQueryBuilder OrWhere(bool condition, string column, string operation, object value)
        {
            if (condition && _whereClauses.Count > 0)
            {
                string paramName = "@p" + _paramCount++;
                _whereClauses.Add("OR " + column + " " + operation + " " + paramName);
                _parameters.Add(paramName, value);
            }
            else if (condition)
            {
                return Where(true, column, operation, value);
            }
            return this;
        }

        public DatabaseQueryBuilder OrderBy(string column)
        {
            _orderByClause = "ORDER BY " + column;
            return this;
        }

        public DatabaseQueryBuilder GroupBy(string column)
        {
            _groupByClause = "GROUP BY " + column;
            return this;
        }

        public DataTable Execute()
        {
            string query = BuildQuery();
            return _connection.ExecuteQuery(query, _parameters);
        }

        public int ExecuteNonQuery()
        {
            string query = BuildQuery();
            return _connection.ExecuteNonQuery(query, _parameters);
        }

        private string BuildQuery()
        {
            string query = _selectClause + " " + _fromClause;

            if (_joinClauses.Count > 0)
                query += " " + string.Join(" ", _joinClauses);

            if (_whereClauses.Count > 0)
                query += " WHERE " + string.Join(" ", _whereClauses);

            if (!string.IsNullOrEmpty(_groupByClause))
                query += " " + _groupByClause;

            if (!string.IsNullOrEmpty(_orderByClause))
                query += " " + _orderByClause;

            return query;
        }
    }
    #endregion
}