using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Satelite
{
    public partial class MarketSearch
    {
        public static DataTable getSearchs(string search)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Id, z.Nombre, z.Precio, i.Url ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE z.Id = i.IdProducto ";
            query += "AND i.Portada = 1 ";
            query += "AND z.Publico = 1 ";
            query += "AND z.Nombre LIKE \'%" + search + "%\' ";
            /*query += " Select DISTINCT (B.GRUPO) as GRUPO ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.PUBLICO = 1 ";*/

            comando = new SqlCommand(query, cn);
            comando.CommandType = CommandType.Text;

            if (cn.State == 0) cn.Open();
            SqlDataAdapter Da = new SqlDataAdapter(comando);
            ds = new DataSet();
            Da.Fill(ds);
            dt = ds.Tables[0];

            cn.Close();

            return dt;
        }
    }
}