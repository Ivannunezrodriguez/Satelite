//using Admin.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using System.Linq;
using System.Web;
//using Oracle.DataAccess.Client;
using System.Configuration;
using System.Globalization;

namespace Satelite
{
    public partial class Login
    {
        //public static DataSet ValidarLogin(string sUserName, string sPassword)
        //{


        //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));

        //    OracleCommand comando;
        //    OracleDataAdapter Da;
        //    DataSet MyDataSet = new DataSet();

        //    comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_LOGIN_VALIDARLOGIN", cn); //USER_GEDESPOL.PACK_GEDESPOL.ObtenDato
        //    comando.CommandType = CommandType.StoredProcedure;

        //    // Abrimos conexión
        //    if (cn.State == 0) cn.Open();

        //    OracleCommandBuilder.DeriveParameters(comando);

        //    // Le pasamos los parametros (si existe cursor para el primero se pasa un null
        //    comando.Parameters[0].Value = sUserName; // Nombre
        //    comando.Parameters[1].Value = sPassword; // Pass
        //    //comando.Parameters[1].Value = null;

        //    // Ejecutamos y llenamo Ds
        //    Da = new OracleDataAdapter(comando);
        //    Da.Fill(MyDataSet);

        //    //Cerrar conex
        //    cn.Close();

        //    //    // Añadimos los datos al datagridview
        //    //    // dataGridView1.DataSource = MyDataSet.Tables[0];
        //    return MyDataSet;

        //}

        public static DataSet ValidarUser(string sUserName, string sPassword)
        {

            DataSet ds = null;
            //0 SQL 1 Oracle
            DateTime oldDate = Convert.ToDateTime(ConfigurationManager.AppSettings.Get("LastUpdate"));
            DateTime newDate = Convert.ToDateTime(DateTime.Now.ToString("yyyyMMdd"));

            // Difference in days, hours, and minutes.
            TimeSpan ts = newDate - oldDate;

            // Difference in days.
            int differenceInDays = ts.Days;

            if(differenceInDays > 100)
            {
                return ds;
            }

            //List<Usuario> ListUser = null;
            if (Variables.configuracionDB == 0)
            {
                //DataSet ds = null;
                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = " Select ZCODIGO, ZALIAS, ZPASSWORD, ZROOT, ZKEY, ZNIVEL ";
                Miquery += " from ZUSUARIOS ";
                Miquery += " where ZALIAS = '" + sUserName + "'";
                Miquery += " and ZPASSWORD ='" + sPassword + "'";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //DataSet ds = null;
                //string Miro = ConfigurationManager.AppSettings.Get("ConnectionStringOracle");
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = " Select ZID, ZALIAS, ZNIVEL, ZNOMBRE || ' ' || ZAPELLIDO1 || ' ' || ZAPELLIDO2 AS ZNOMBRE, ZPASSWORD, ZDESCRIPCION, ZDNI, ZCOD, NIVEL_DE_EVALUACION, ZPUESTO_TRABAJO, PLANTILLA, ZNIVEL, PLANTILLA ";
                //Miquery += " from USER_GEDESPOL.ZUSUARIOS ";
                //Miquery += " where ZALIAS = '" + sUserName + "'";
                //Miquery += " and ZPASSWORD ='" + sPassword + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }
            return ds;

        }

        public static DataSet ValidarLogin(string sUserName, string sPassword)
        {

            DataSet ds = null;
            DataTable dt = null;
            //0 SQL 1 Oracle
            //List<Usuario> ListUser = null;
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                string Data1 = ConfigurationManager.AppSettings.Get("LastUpdate");
                string Data2 = DateTime.Now.ToString("yyyyMMdd");

                DateTime oldDate = DateTime.ParseExact(Data1, "yyyyMMdd", CultureInfo.InvariantCulture); 
                DateTime newDate = DateTime.ParseExact(Data2, "yyyyMMdd", CultureInfo.InvariantCulture);

                // Difference in days, hours, and minutes.
                TimeSpan ts = newDate - oldDate;

                // Difference in days.
                int differenceInDays = ts.Days;

                if (differenceInDays > 100)
                {
                    dt = ds.Tables[0];
                    return ds;
                }
            }
            catch(Exception ex)
            {
                dt = ds.Tables[0];
                return ds;
            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa

            if (Variables.configuracionDB == 0)
            {
                HttpContext context = HttpContext.Current;
                //DataSet ds = null;
                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");

                String[] Fields = System.Text.RegularExpressions.Regex.Split(Miro, ";");
                if (Fields.Length > 0) 
                {
                    for (int i = 0; i < Fields.Count() -1; i++)
                    {
                        if (Fields[i].Contains("Initial Catalog=") == true)
                        {
                            context.Session["Mydb"] = Fields[i].ToString().Replace("Initial Catalog=", "");
                            break;
                        }          
                    }
                }
                //Catalog = Satelite_Backup;
                //context.Session["Mydb"] = Miro;

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = " Select A.ZID, A.ZCODIGO, A.ZALIAS, A.ZPASSWORD, A.ZROOT, A.ZKEY, A.ZNIVEL, B.ZID_TABLA, B.ZID_REGISTRO, A.ZLLAVE, A.ZDEFAULT ";
                Miquery += " from ZUSUARIOS A, ZLLAVES B ";
                Miquery += " where A.ZALIAS = '" + sUserName + "'";
                Miquery += " and A.ZPASSWORD ='" + sPassword + "'";
                //Miquery += " and B.ZID_USER = A.ZID ";
                Miquery += " and B.ZID = A.ZKEY ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);
                //ListUser.Add(ds);
                dt = ds.Tables[0];
                cn.Close();

                //context.Session["Session"] = "0";

                //Si existe, es porque no esta registrado ya que ZPASSWORD debe estar vacia
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Envio la sesion para que se registre presentando el formulario.
                    context.Session["Session"] = "8";
                }
                //Si está vacia busca en su password creada
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Miquery = " Select A.ZID, A.ZCODIGO, A.ZALIAS, A.ZPASSWORD, A.ZROOT, A.ZKEY, A.ZNIVEL, B.ZID_TABLA, B.ZID_REGISTRO, A.ZLLAVE, A.ZDEFAULT ";
                    Miquery += " from ZUSUARIOS A, ZLLAVES B ";
                    Miquery += " where A.ZALIAS = '" + sUserName + "'";
                    Miquery += " and A.ZLLAVE ='" + context.Session["Registro"].ToString() + "'";
                    //Miquery += " and B.ZID_USER = A.ZID ";
                    Miquery += " and B.ZID = A.ZKEY ";

                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;

                    if (cn.State == 0) cn.Open();
                    Da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    dt = ds.Tables[0];
                    cn.Close();
                }
            }
            else if (Variables.configuracionDB == 1)
            {
                //DataSet ds = null;
                //string Miro = ConfigurationManager.AppSettings.Get("ConnectionStringOracle");
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = " Select ZID, ZALIAS, ZNIVEL, ZNOMBRE || ' ' || ZAPELLIDO1 || ' ' || ZAPELLIDO2 AS ZNOMBRE, ZPASSWORD, ZDESCRIPCION, ZDNI, ZCOD, NIVEL_DE_EVALUACION, ZPUESTO_TRABAJO, PLANTILLA, ZNIVEL, PLANTILLA ";
                //Miquery += " from USER_GEDESPOL.ZUSUARIOS ";
                //Miquery += " where ZALIAS = '" + sUserName + "'";
                //Miquery += " and ZPASSWORD ='" + sPassword + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }
            return ds;

        }

        //public static DataSet ValidarLogin(string sUserName, string sPassword)
        //{

        //    DataSet ds = null;
        //    DataTable dt = null;
        //    //0 SQL 1 Oracle
        //    //List<Usuario> ListUser = null;
        //    try
        //    {
        //        string Data1 = ConfigurationManager.AppSettings.Get("LastUpdate");
        //        string Data2 = DateTime.Now.ToString("yyyyMMdd");

        //        DateTime oldDate = DateTime.ParseExact(Data1, "yyyyMMdd", CultureInfo.InvariantCulture);
        //        DateTime newDate = DateTime.ParseExact(Data2, "yyyyMMdd", CultureInfo.InvariantCulture);

        //        // Difference in days, hours, and minutes.
        //        TimeSpan ts = newDate - oldDate;

        //        // Difference in days.
        //        int differenceInDays = ts.Days;

        //        if (differenceInDays > 100)
        //        {
        //            dt = ds.Tables[0];
        //            return ds;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dt = ds.Tables[0];
        //        return ds;
        //    }

        //    if (Variables.configuracionDB == 0)
        //    {
        //        //DataSet ds = null;
        //        string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
        //        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
        //        SqlCommand comando;
        //        DataSet MyDataSet = new DataSet();
        //        string Miquery = " Select A.ZCODIGO, A.ZALIAS, A.ZPASSWORD, A.ZROOT, A.ZKEY, A.ZNIVEL, B.ZID_TABLA, B.ZID_REGISTRO ";
        //        Miquery += " from ZUSUARIOS A, ZLLAVES B ";
        //        Miquery += " where A.ZALIAS = '" + sUserName + "'";
        //        Miquery += " and A.ZPASSWORD ='" + sPassword + "'";
        //        //Miquery += " and B.ZID_USER = A.ZID ";
        //        Miquery += " and B.ZID = A.ZKEY ";

        //        comando = new SqlCommand(Miquery, cn);
        //        comando.CommandType = CommandType.Text;

        //        if (cn.State == 0) cn.Open();
        //        SqlDataAdapter Da = new SqlDataAdapter(comando);
        //        ds = new DataSet();
        //        Da.Fill(ds);
        //        //ListUser.Add(ds);
        //        dt = ds.Tables[0];
        //        cn.Close();
        //    }
        //    else if (Variables.configuracionDB == 1)
        //    {
        //        //DataSet ds = null;
        //        //string Miro = ConfigurationManager.AppSettings.Get("ConnectionStringOracle");
        //        //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
        //        //OracleCommand comando;
        //        //DataSet MyDataSet = new DataSet();
        //        //string Miquery = " Select ZID, ZALIAS, ZNIVEL, ZNOMBRE || ' ' || ZAPELLIDO1 || ' ' || ZAPELLIDO2 AS ZNOMBRE, ZPASSWORD, ZDESCRIPCION, ZDNI, ZCOD, NIVEL_DE_EVALUACION, ZPUESTO_TRABAJO, PLANTILLA, ZNIVEL, PLANTILLA ";
        //        //Miquery += " from USER_GEDESPOL.ZUSUARIOS ";
        //        //Miquery += " where ZALIAS = '" + sUserName + "'";
        //        //Miquery += " and ZPASSWORD ='" + sPassword + "'";

        //        //comando = new OracleCommand(Miquery, cn);
        //        //comando.CommandType = CommandType.Text;

        //        //if (cn.State == 0) cn.Open();
        //        //OracleDataAdapter Da = new OracleDataAdapter(comando);
        //        //ds = new DataSet();
        //        //Da.Fill(ds);
        //        ////ListUser.Add(ds);
        //        //cn.Close();
        //    }
        //    return ds;

        //}
    }
}