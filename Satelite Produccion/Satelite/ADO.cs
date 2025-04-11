using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations.Model;

namespace Satelite
{
    public class Ado
    {
        //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
        string con = ConfigurationManager.AppSettings.Get("connectionSQL");
        SqlConnection conexion;

        public Ado()
        {
        }

        public DataTable Retrieve(string stored)
        {
            DataTable dt = new DataTable();
            //de momento select, luego procedimientos o consultas
            int Procedure = 0;

            try
            {
                SqlCommand com = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter();

                if (Procedure == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                    SqlCommand comando;

                    DataSet ds = null;

                    DataSet MyDataSet = new DataSet();
                    string Miquery = stored;
                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;
                    comando.Connection = cn;

                    //string a = ETrazas(SQL, "0", " BuscaLote -->" + context.Session["Procedimiento"].ToString());

                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    cn.Close();
                    return ds.Tables[0];

                }
                else
                {
                    Conectar();
                    com.Connection = conexion;

                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = stored;
                    adapter.SelectCommand = com;
                    adapter.Fill(dt);
                    return dt;
                }

                //Conectar();
                //com.Connection = conexion;
                //com.CommandType = CommandType.StoredProcedure;
                //com.CommandText = stored;
                //adapter.SelectCommand = com;
                //adapter.Fill(dt);
                //return dt;
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;

                return dt = null;
            }
            finally
            {
                if (Procedure == 0)
                {
                }
                else
                {
                    Desconcestar();
                }
            }
        }

        private void Conectar()
        {
            conexion = new SqlConnection(con);
            conexion.Open();
        }

        private void Desconcestar()
        {
            try
            {
                conexion.Close();
                conexion = null;
            }
            catch(Exception ex)
            {
                conexion = null;
            }
        }
    }
}