using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Satelite
{
    public partial class Marketplace
    {
        public static DataTable getDestacado()
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Id, z.Nombre, z.Precio, z.Descp, i.Url ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE i.IdProducto =  z.Id ";
            query += "AND z.Publicado = 1 ";
            query += "AND z.Id = 63 ";
            query += "AND i.Portada = 1 ";
            /*query += " Select NOMBRE, B.PRECIO, B.ZDESCRIPCION as Descp, B.FOTO  ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.PUBLICO = 1";
            query += "AND B.ZID = 1" ;*/

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

        public static DataTable getProductoId(string productId)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Nombre, z.Precio, z.Descp, i.Url, z.EspTec ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE i.IdProducto =  z.Id ";
            query += "AND z.Publico = 1 ";
            query += "AND z.Id = "+ productId + " ";
            query += "AND i.Portada = 1 ";
            /*query += " Select NOMBRE, B.PRECIO, B.ZDESCRIPCION as Descp, B.FOTO  ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.PUBLICO = 1";
            query += "AND B.ZID = 1" ;*/

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