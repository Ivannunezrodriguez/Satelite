using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Satelite
{
    public partial class MarketProduct : System.Web.UI.Page
    {
        public static DataTable getGaleria(string productId)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT DISTINCT Url ";
            query += "FROM ZMARKETIMG  ";
            query += "WHERE IdProducto = "+ productId + " ";


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