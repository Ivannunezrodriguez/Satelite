using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Satelite
{
    public partial class MarketplaceProducts
        {
        public static DataTable getSubgrups(string grp)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Id, z.Nombre, z.Precio, z.Descp, z.Subgrupo, i.Url ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE z.Id = i.IdProducto ";
            query += "AND i.Portada = 1 ";
            query += "AND z.Publico = 1 ";
            query += "AND z.Grupo = \'"+grp+"\' ";
            query += "AND z.Tipo = 1 ";
            /*query += " Select B.NOMBRE, B.ZDESCRIPCION as Descp, B.PRECIO, B.DESCUENTO, B.TIPO, B.PUBLICO, ";
            query += " B.FOTO, B.GRUPO, B.SUBGRUPO, A.TEMARIO, A.SUBTEMA1, A.SUBTEMA2  ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            //query += "WHERE B.ZID = 2  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.GRUPO = \'" + grp + "\' ";
            query += "AND B.TIPO = 1 " ;*/


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

        public static DataTable getProductSubgrups(string subgrupo, string grp)
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT z.Id, z.Nombre, z.Precio, z.Descp, i.Url ";
            query += "FROM ZMARKET z, ZMARKETIMG i ";
            query += "WHERE z.Id = i.IdProducto ";
            query += "AND i.Portada = 1 ";
            query += "AND z.Publico = 1 ";
            query += "AND Subgrupo = \'" + subgrupo + "\' ";
            query += "AND Grupo = \'" + grp + "\' ";
            query += "ORDER BY Tipo, Id";
            /*query += " Select B.ZID as id, NOMBRE, B.ZDESCRIPCION as Descp, B.PRECIO, B.DESCUENTO, B.TIPO, B.PUBLICO, ";
            query += " B.FOTO, B.GRUPO, B.SUBGRUPO, A.TEMARIO, A.SUBTEMA1, A.SUBTEMA2  ";
            query += "FROM ZCURSOTODO A, ZCURSOS B  ";
            query += "WHERE A.ZIDCURSO = B.ZID  ";
            query += "AND B.SUBGRUPO = \'" + subgrupo + "\' ";
            query += "AND B.PUBLICO = 1 ";
            query += "ORDER BY B.TIPO, B.ZID";*/

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

        public static DataTable getProductGrups()
        {
            DataSet ds = null;
            DataTable dt = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand comando;

            string query = "";

            //generar query
            query += "SELECT DISTINCT Grupo ";
            query += "FROM ZMARKET ";
            query += "WHERE Publicado = 1 ";
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