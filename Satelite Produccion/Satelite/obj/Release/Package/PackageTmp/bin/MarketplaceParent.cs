using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Satelite
{
    public partial class MarketplaceParent
    {
        public static DataTable getProduct(int id)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Nombre, z.Precio, i.Url ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE z.Id = i.IdProducto ";
            query += "AND i.Portada = 1 ";
            query += "AND z.Id = " + id;
            /*query += " Select NOMBRE, B.PRECIO, B.FOTO  ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.ZID = " + id;*/



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