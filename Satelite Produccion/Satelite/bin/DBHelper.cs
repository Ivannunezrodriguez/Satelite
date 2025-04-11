using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
//using Oracle.DataAccess.Client;


//Oracle(ODP.Net) : OracleConnection, OracleCommand, OracleDataReader, y OracleParameter.
//Hay un proveedor de MS: Microsoft’s.NET for Oracle Provider, pero el Oracle Data Provider for .NET, 
//es el oficial de Oracle. 

namespace Satelite
{
    public class DBHelper 
    {
        public DBHelper()
        {
            //
            // TODO: Add constructor comentado es para SQL Server
            //
        }

        //return DBHelper.ExecuteDataSet("GED_Login_ValidarLogin", dbParams);
        public static DataSet ExecuteDataSetSQL(string sqlSpName, SqlParameter[] dbParams)
        //public static DataSet ExecuteDataSet(string sqlSpName, OracleParameter[] dbParams)
        {
            DataSet ds = null;
            //try
            //{
                ds = new DataSet();
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //OracleCommand cmd = new OracleCommand(sqlSpName, cn);
            cmd.CommandTimeout = 600;
                
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //OracleDataAdapter da = new OracleDataAdapter(cmd);
            if (dbParams != null)
                {
                    foreach (SqlParameter dbParam in dbParams)
                    //foreach (OracleParameter dbParam in dbParams)   
                    {
                        da.SelectCommand.Parameters.Add(dbParam);
                    }
                }
                da.Fill(ds);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return ds;
        }

        public static bool ExecuteXmlSQL(string sqlSpName, SqlParameter[] dbParams, System.Xml.XmlDocument dXml)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();
            bool bReturn;
            try
            {
                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.Read())
                    {
                        //System.Data.SqlTypes.SqlXml oXml = dr.GetSqlXml(dr.GetOrdinal("Xml"));
                        // System.Data.SqlTypes.SqlXml oXml = dr.GetXmlReader(1));

                        //dXml.LoadXml(oXml.Value);
                        bReturn = true;
                    }
                    else
                    {
                        bReturn = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bReturn;
        }



        public static SqlDataReader ExecuteDataReaderSQL(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlDataReader dr;

            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);

            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();

            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            return dr;
        }

        public bool TableExists(SqlConnection conn, string database, string name)
        {
            string strCmd = null;
            SqlCommand sqlCmd = null;

            try
            {
                strCmd = "select case when exists((select '['+SCHEMA_NAME(schema_id)+'].['+name+']' As name FROM [" + database + "].sys.tables WHERE name = '" + name + "')) then 1 else 0 end";
                sqlCmd = new SqlCommand(strCmd, conn);

                return (int)sqlCmd.ExecuteScalar() == 1;
            }
            catch { return false; }
        }

        public static void ExecuteNonQuerySQL(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);


            //cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            //cmd.CommandType = CommandType.StoredProcedure   
            cmd.CommandType = CommandType.Text;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                throw new Exception("Error de base de datos.", Ex);
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

         }


        public static void MigraProcedurePinchAlb(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            string SQL = "SELECT ID, EMPRESA, SERIE, NUMERO, ARTICULO, DESCRIPCION, ALMACEN_E, UBICACION_E, UNIDADES, SERIE_LOTE_CAB, FECHA_FABR,";
            SQL += "FECHA_CADUC, ALMACEN_S, LIBRE2, LIBRE3, COMPONENTE, UNIDADES_C, ALMACEN_C, UBICACION_C, SERIE_LOTE_C, ESTADO ";
            SQL += " FROM ZIMPPINCHALV WHERE ESTADO = '1'";// AND TIPO_FORM = 'PinchAlvCab' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            SqlCommand cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["DESCRIPCION"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_E"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_E"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE_CAB"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_FABR"].ToString() + "',";

                DatosCampos += "'" + filas["FECHA_CADUC"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_S"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE2"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE3"].ToString() + "',";
                DatosCampos += "'" + filas["COMPONENTE"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES_C"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_C"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_C"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE_C"].ToString() + "',";
                DatosCampos += "'2'";

               //Destino GOlden
               SQL = "INSERT INTO ZIMPPINCHALV (ID, EMPRESA, SERIE, NUMERO, ARTICULO, DESCRIPCION, ALMACEN_E, UBICACION_E, UNIDADES, SERIE_LOTE_CAB, FECHA_FABR,";
                SQL += "FECHA_CADUC, ALMACEN_S, LIBRE2, LIBRE3, COMPONENTE, UNIDADES_C, ALMACEN_C, UBICACION_C, SERIE_LOTE_C, ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGolden"));
                cmd1 = new SqlCommand(SQL, cn1);
                cmd1.CommandType = CommandType.Text;

                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                cmd1 = new SqlCommand(SQL, cn1);
                cmd1.CommandType = CommandType.Text;

                SQL = "UPDATE ZIMPPINCHALV SET ESTADO = '2' ";
                SQL += " WHERE ID = " + filas["ID"].ToString();

                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;

                DBHelper.ExecuteNonQuery(SQL);

            }

            
        }

        //public static void MigraProcedurePinchAlb(string sqlSpName)
        //{
        //    //int I = 0;
        //    //Variables.Error = "";
        //    //DataSet ds = new DataSet();

        //    //DataTable Datos = new DataTable();
        //    //Variables.mensajeserver = "";
        //    ////Origen
        //    //string SQL = " INSERT INTO ZIMPPINCHALV (EMPRESA, SERIE, ARTICULO, DESCRIPCION, UBICACION_E, ";
        //    //SQL += " ALMACEN_E, SERIE_LOTE_CAB, FECHA_FABR, ";
        //    //SQL += " FECHA_CADUC, LIBRE2, ESTADO)";
        //    //SQL += " SELECT";
        //    //SQL += " B.EMPRESA, 'OPA' AS SERIE, C.ZCODGOLDEN AS ARTICULO, 'DESCRIPCION' AS DESCRIPCION, SUBSTRING(A.DESDE, CHARINDEX('|', A.DESDE) + 1, LEN(A.DESDE) - CHARINDEX('|', A.DESDE)) AS UBICACION_ENTRADA,";
        //    //SQL += " REPLACE(SUBSTRING(A.DESDE, 1, CHARINDEX('|', A.DESDE, 1)), '|', '')  AS ALMACEN_ENTRADA, A.LOTE, FORMAT(A.FECHA, 'dd/MM/yyyy') AS FECHAFABR,";
        //    ////Pasar a SELECT la fecha
        //    //SQL += " '31/12/2021' AS FECHA_CADUC, 'T' + A.TUNELES + ' P' + A.PASILLOS AS LIBRE2, '0' AS ESTADO";
        //    //SQL += " FROM ZENTRADA A";
        //    //SQL += " INNER JOIN RELACION_CAMPO_VARIEDAD_OCU B";
        //    //SQL += " on A.VARIEDAD = B.VARIEDAD";
        //    //SQL += " INNER JOIN ZPLANTA_TIPO_VARIEDAD_CODGOLDEN C";
        //    //SQL += "  on A.VARIEDAD = C.ZVARIEDAD";
        //    //SQL += " WHERE A.TIPO_FORM = 'PinchAlvCab'";
        //    //SQL += "  AND C.ZTIPO_PLANTA = A.TIPO_PLANTA";
        //    //SQL += "  AND B.CAMPO_CULTIVO = SUBSTRING(A.DESDE, CHARINDEX('|', A.DESDE) + 1, LEN(A.DESDE) - CHARINDEX('|', A.DESDE))";
        //    //SQL += "  AND(A.ESTADO = '0' OR A.ESTADO is null)";
        //    //SQL += " ORDER BY A.LOTE, A.TIPO_PLANTA, A.VARIEDAD ";
        //    //SQL += " UBICACION, ESTADO FROM ZIMPPRODTIPS WHERE ESTADO = 0 ";

        //    //SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
        //    //SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
        //    //Variables.mensajeserver = "Procesando migración: ";
        //    //cmd1 = new SqlCommand(SQL, cn1);
        //    //cmd1.CommandType = CommandType.Text;

        //    //try
        //    //{
        //    //    if (cn1.State == 0) cn1.Open();
        //    //    SqlDataAdapter Da = new SqlDataAdapter(cmd1);
        //    //    Da.Fill(ds);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    //MessageBox.Show(ex.Message); si es nulo mensaje
        //    //    Variables.mensajeserver += ex.Message;
        //    //    cn1.Close();
        //    //    return;
        //    //}
        //    //cn1.Close();

        //    //Lanzo procedimiento ZIMPPINCHALV





        //    DBHelper.ExecuteNonQueryProcedurePinch("");
        //}

        public static void DeleteProcedureTemp(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            SqlCommand cmd = null;

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            if(sqlSpName == "ZPEDIDOS_COMPRA")
            {
                Variables.Error = "Delete_Temporal_Compra";
                cmd = new SqlCommand("Delete_Temporal_Compra", cn);
                sqlSpName = "";
            }
            else
            {
                Variables.Error = "Delete_Temporal_Orden";
                cmd = new SqlCommand("Delete_Temporal_Orden", cn);
            }

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@MAXRECORD", "");
                cmd.Parameters.AddWithValue("@CURRENTRECORD", "");
                cmd.Parameters.AddWithValue("@CONSULTA", "");     
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta


            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                string a = Main.Ficherotraza(Variables.Error + " --> " + Ex.Message);
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message + " - " + Variables.Error;
                throw new Exception("Error de base de datos.", Ex);
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }


        public static void MigraProcedureModAlv(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen     
            string SQL = "SELECT ID, EMPRESA, ARTICULO, ALMACEN_DESDE, UBICACION_DESDE, ALMACEN_HASTA, UBICACION_HASTA, UNIDADES, SERIE_LOTE, SERIE,NUMERO, TIPO_OPERACION, SUBTIPO, FECHA, ESTADO ";
            SQL += " FROM ZIMPMOVALMA WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_DESDE"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_DESDE"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_HASTA"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_HASTA"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";
                DatosCampos += "'" + filas["TIPO_OPERACION"].ToString() + "',";
                DatosCampos += "'" + filas["SUBTIPO"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPMOVALMA (ID, EMPRESA, ARTICULO, ALMACEN_DESDE, UBICACION_DESDE, ALMACEN_HASTA, UBICACION_HASTA, UNIDADES, SERIE_LOTE, SERIE, NUMERO, ";
                SQL += " TIPO_OPERACION, SUBTIPO, FECHA, ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPMOVALMA SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void MigraProcedureVentas(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID, TIPO_FORM,FECHA, TIPO_PLANTA, VARIEDAD, REPLACE(REPLACE(LOTE, CHAR(10),''), CHAR(13),'') AS LOTE, LOTEDESTINO, UNIDADES, NUM_UNIDADES, MANOJOS, DESDE, REPLACE(REPLACE(HASTA, CHAR(10),''), CHAR(13),'') AS HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS, OBSERVACIONES, OK, DeviceID, DeviceName, SendTime, ReceiveTime, Barcode, ESTADO ";
            SQL += " FROM ZENTRADA WHERE TIPO_FORM = 'Venta' AND ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                string a = Main.Ficherotraza("MigraProcedureVentas --> " + ex.Message);
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            try
            {
                foreach (DataRow filas in Datos.Rows)
                {
                    DatosCampos = "" + filas["ID"].ToString() + ",";
                    DatosCampos += "'" + filas["TIPO_FORM"].ToString() + "',";
                    DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                    DatosCampos += "'" + filas["TIPO_PLANTA"].ToString() + "',";//antes numero
                    DatosCampos += "'" + filas["VARIEDAD"].ToString() + "',";
                    DatosCampos += "'" + filas["LOTE"].ToString() + "',";
                    DatosCampos += "'" + filas["LOTEDESTINO"].ToString() + "',";
                    DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                    DatosCampos += "'" + filas["NUM_UNIDADES"].ToString() + "',";
                    DatosCampos += "'" + filas["MANOJOS"].ToString() + "',";
                    DatosCampos += "'" + filas["DESDE"].ToString() + "',";
                    DatosCampos += "'" + filas["HASTA"].ToString() + "',";
                    DatosCampos += "'" + filas["ETDESDE"].ToString() + "',";
                    DatosCampos += "'" + filas["ETHASTA"].ToString() + "',";
                    DatosCampos += "'" + filas["TUNELES"].ToString() + "',";
                    DatosCampos += "'" + filas["PASILLOS"].ToString() + "',";
                    DatosCampos += "'" + filas["OBSERVACIONES"].ToString() + "',";
                    DatosCampos += "'" + filas["OK"].ToString() + "',";
                    DatosCampos += "'" + filas["DeviceID"].ToString() + "',";
                    DatosCampos += "'" + filas["DeviceName"].ToString() + "',";
                    DatosCampos += "'" + filas["SendTime"].ToString() + "',";
                    DatosCampos += "'" + filas["ReceiveTime"].ToString() + "',";
                    DatosCampos += "'" + filas["Barcode"].ToString() + "',";
                    DatosCampos += "'2'";

                    //Destino
                    SQL = "INSERT INTO ZENTRADA_VENTA (ID, TIPO_FORM,FECHA, TIPO_PLANTA, VARIEDAD, LOTE, LOTEDESTINO, UNIDADES, NUM_UNIDADES, MANOJOS, DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS,";
                    SQL += "  OBSERVACIONES, OK, DeviceID, DeviceName, SendTime, ReceiveTime, Barcode, ESTADO )";
                    SQL += " VALUES (" + DatosCampos + ")";

                    //DBHelper.ExecuteNonQuery(SQL);
                    //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                    //
                    DBHelper.ExecuteNonQueryGolden(SQL);
                    //Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                    ////Actualiza origen
                    //SQL = "UPDATE ZENTRADA SET ESTADO = '2' ";
                    //SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                    DBHelper.ExecuteNonQuery(SQL);
                    //Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                string a = Main.Ficherotraza("MigraProcedureVentas --> " + ex.Message + ", SQL = " + SQL );
                cn1.Close();
                return;
            }
        }


        public static void RevierteProcedureVentas(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //

            string SQL = "SELECT ID, TIPO_FORM,FECHA, TIPO_PLANTA, VARIEDAD, REPLACE(REPLACE(LOTE, CHAR(10),''), CHAR(13),'') AS LOTE, LOTEDESTINO, UNIDADES, NUM_UNIDADES, MANOJOS, DESDE, REPLACE(REPLACE(HASTA, CHAR(10),''), CHAR(13),'') AS HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS,";
            SQL += "  OBSERVACIONES, OK, DeviceID, DeviceName, SendTime, ReceiveTime, Barcode, ESTADO  FROM  ZENTRADA_VENTA ORDER BY ID";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando revertir: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                string a = Main.Ficherotraza("RevertirProcedureVentas --> " + ex.Message);
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["TIPO_FORM"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'" + filas["TIPO_PLANTA"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["VARIEDAD"].ToString() + "',";
                DatosCampos += "'" + filas["LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["LOTEDESTINO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["MANOJOS"].ToString() + "',";
                DatosCampos += "'" + filas["DESDE"].ToString() + "',";
                DatosCampos += "'" + filas["HASTA"].ToString() + "',";
                DatosCampos += "'" + filas["ETDESDE"].ToString() + "',";
                DatosCampos += "'" + filas["ETHASTA"].ToString() + "',";
                DatosCampos += "'" + filas["TUNELES"].ToString() + "',";
                DatosCampos += "'" + filas["PASILLOS"].ToString() + "',";
                DatosCampos += "'" + filas["OBSERVACIONES"].ToString() + "',";
                DatosCampos += "'" + filas["OK"].ToString() + "',";
                DatosCampos += "'" + filas["DeviceID"].ToString() + "',";
                DatosCampos += "'" + filas["DeviceName"].ToString() + "',";
                DatosCampos += "'" + filas["SendTime"].ToString() + "',";
                DatosCampos += "'" + filas["ReceiveTime"].ToString() + "',";
                DatosCampos += "'" + filas["Barcode"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZENTRADA_VENTA (ID, TIPO_FORM,FECHA, TIPO_PLANTA, VARIEDAD, LOTE, LOTEDESTINO, UNIDADES, NUM_UNIDADES, MANOJOS, DESDE, HASTA, ETDESDE, ETHASTA, TUNELES, PASILLOS,";
                SQL += "  OBSERVACIONES, OK, DeviceID, DeviceName, SendTime, ReceiveTime, Barcode, ESTADO )";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                ////Actualiza origen
                //SQL = "UPDATE ZENTRADA SET ESTADO = '2' ";
                //SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void MigraProcedureCompras(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();
            HttpContext context = HttpContext.Current;
            context.Session["Procedimiento"] = "MigraProcedureCompras";
            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID, EMPRESA, SERIE, NUMERO, FECHA, PROVEEDOR, PRODUCTO, UNIDADES_REC, SERIE_PEDIDO, NUM_PEDIDO, ";
            SQL += " NUM_LINEA, SERIE_LOTE, ALMACEN, UBICACION, FECHA_CADUC, ALBPROV, ESTADO ";
            SQL += " FROM ZIMPCOMPRA WHERE ESTADO = '1'  ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                SQL += " NUM_LINEA, SERIE_LOTE, ALMACEN, UBICACION, FECHA_CADUC, ALBPROV, ESTADO ";
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'" + filas["PROVEEDOR"].ToString() + "',";
                DatosCampos += "'" + filas["PRODUCTO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES_REC"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_LINEA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_CADUC"].ToString() + "',";
                DatosCampos += "'" + filas["ALBPROV"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPCOMPRA (ID, EMPRESA, SERIE, NUMERO, FECHA, PROVEEDOR, PRODUCTO, UNIDADES_REC, SERIE_PEDIDO, NUM_PEDIDO, ";
                SQL += " NUM_LINEA, SERIE_LOTE, ALMACEN, UBICACION, FECHA_CADUC, ALBPROV, ESTADO )";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPCOMPRA SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void MigraProcedurePlantacion(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID ,EMPRESA ,SERIE ,NUMERO ,ARTICULO ,UNIDADES ,COMPONENTE ,UNIDADES1 ,SERIE_LOTE,FECHA ,ALMACEN ,UBICACION ,ESTADO ";
            SQL += " FROM ZIMPPLANTACION WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["COMPONENTE"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES1"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPPLANTACION (ID ,EMPRESA ,SERIE ,NUMERO ,ARTICULO ,UNIDADES ,COMPONENTE ,UNIDADES1 , ";
                SQL += " SERIE_LOTE,FECHA ,ALMACEN ,UBICACION ,ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPPLANTACION SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void MigraProcedureFrigo(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            


            string SQL = "SELECT ID ,EMPRESA ,SERIE ,NUMERO ,ARTICULO ,UNIDADES ,COMPONENTE ,UNIDADES1 ,SERIE_LOTE,FECHA_FABR,FECHA_CADUC,LIBRE1,LIBRE3 ,ALMACEN ,UBICACION ,ESTADO ";
            SQL += " FROM ZIMPPRODFRESFRIG WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["COMPONENTE"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES1"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_FABR"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_CADUC"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE1"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE3"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPPRODFRESFRIG (ID ,EMPRESA ,SERIE ,NUMERO ,ARTICULO ,UNIDADES ,COMPONENTE ,UNIDADES1 , ";
                SQL += " SERIE_LOTE,FECHA_FABR,FECHA_CADUC,LIBRE1,LIBRE3 ,ALMACEN ,UBICACION ,ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPPRODFRESFRIG SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }


        public static void MigraProcedurePaletAlv(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID, EMPRESA, SERIE, NUMERO, ARTICULO, DESCRIPCION, ALMACEN_E, UBICACION_E, UNIDADES, SERIE_LOTE_CAB, FECHA_FABR, FECHA_CADUC, ALMACEN_S, LIBRE1, COMPONENTE, UNIDADES_C, ALMACEN_C, UBICACION_C, SERIE_LOTE_C, ESTADO ";
            SQL += " FROM ZIMPPALETALV WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["ID"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["DESCRIPCION"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_E"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_E"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE_CAB"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_FABR"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_CADUC"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_S"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE1"].ToString() + "',";
                DatosCampos += "'" + filas["COMPONENTE"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES_C"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN_C"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION_C"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE_C"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPPALETALV (ID, EMPRESA, SERIE, NUMERO, ARTICULO, DESCRIPCION, ALMACEN_E, UBICACION_E, UNIDADES, SERIE_LOTE_CAB, FECHA_FABR, FECHA_CADUC, ALMACEN_S, LIBRE1,  ";
                SQL += " COMPONENTE, UNIDADES_C, ALMACEN_C, UBICACION_C, SERIE_LOTE_C, ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPPALETALV SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void MigraProcedureVenta(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID,EMPRESA,SERIE,NUMERO,FECHA,CLIENTE,PRODUCTO,UNIDADES_SERV,SERIE_PEDIDO,NUM_PEDIDO,SERIE_LOTE,ALMACEN,UBICACION,NUM_LINEA,ETIQUETAS,ORDEN_CARGA,ESTADO ";
            SQL += " FROM ZIMPVENTA WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver += "Procesando migración: " + SQL;
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                string a = Main.Ficherotraza("MigraProcedureVenta--> " + ex.Message);
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'" + filas["CLIENTE"].ToString() + "',";
                DatosCampos += "'" + filas["PRODUCTO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES_SERV"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_LINEA"].ToString() + "',";
                DatosCampos += "'" + filas["ETIQUETAS"].ToString() + "',";
                DatosCampos += "'" + filas["ORDEN_CARGA"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPVENTA (ID,EMPRESA,SERIE,NUMERO,FECHA,CLIENTE,PRODUCTO,UNIDADES_SERV,SERIE_PEDIDO,NUM_PEDIDO,SERIE_LOTE, ";
                SQL += " ALMACEN,UBICACION,NUM_LINEA,ETIQUETAS,ORDEN_CARGA,ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";
                //Variables.mensajeserver += SQL ;
                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                //Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPVENTA SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += SQL + "Registro actualizado." + Environment.NewLine;

            }
        }


        public static void MigraProcedureVentaDEP(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            //
            string SQL = "SELECT ID,EMPRESA,SERIE,NUMERO,FECHA,CLIENTE,PRODUCTO,UNIDADES_SERV,SERIE_PEDIDO,NUM_PEDIDO,SERIE_LOTE,ALMACEN,UBICACION,NUM_LINEA,ETIQUETAS,ORDEN_CARGA,ESTADO ";
            SQL += " FROM ZIMPVENTADEP WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver += "Procesando migración: " + SQL;
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";

            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";//antes numero
                DatosCampos += "'" + filas["FECHA"].ToString() + "',";
                DatosCampos += "'" + filas["CLIENTE"].ToString() + "',";
                DatosCampos += "'" + filas["PRODUCTO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES_SERV"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_PEDIDO"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'" + filas["NUM_LINEA"].ToString() + "',";
                DatosCampos += "'" + filas["ETIQUETAS"].ToString() + "',";
                DatosCampos += "'" + filas["ORDEN_CARGA"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPVENTADEP (ID,EMPRESA,SERIE,NUMERO,FECHA,CLIENTE,PRODUCTO,UNIDADES_SERV,SERIE_PEDIDO,NUM_PEDIDO,SERIE_LOTE, ";
                SQL += " ALMACEN,UBICACION,NUM_LINEA,ETIQUETAS,ORDEN_CARGA,ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";
                //Variables.mensajeserver += SQL;
                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                //Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPVENTADEP SET ESTADO = '2' ";
                SQL += " WHERE ID =" + filas["ID"].ToString() + " ";

                DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += SQL + "Registro actualizado." + Environment.NewLine;
                //

            }
        }


        public static void MigraProcedure(string sqlSpName)
        {


            //int I = 0;
            Variables.Error = "";
            DataSet ds = new DataSet();

            DataTable Datos = new DataTable();
            Variables.mensajeserver = "";
            //Origen
            string SQL = "SELECT ID, EMPRESA, SERIE, NUMERO, ARTICULO, UNIDADES, COMPONENTE, UNIDADES1, SERIE_LOTE, FECHA_FABR, FECHA_CADUC, LIBRE3, ALMACEN, ";
            SQL += "UBICACION, ESTADO FROM ZIMPPRODTIPS WHERE ESTADO = '1' ";

            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            Variables.mensajeserver = "Procesando migración: ";
            cmd1 = new SqlCommand(SQL, cn1);
            cmd1.CommandType = CommandType.Text;

            try
            {
                if (cn1.State == 0) cn1.Open();
                SqlDataAdapter Da = new SqlDataAdapter(cmd1);
                Da.Fill(ds);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message); si es nulo mensaje
                Variables.mensajeserver += ex.Message;
                cn1.Close();
                return;
            }
            cn1.Close();

            Datos = ds.Tables[0];
            string DatosCampos = "";
            
            foreach (DataRow filas in Datos.Rows)
            {
                DatosCampos = "" + filas["ID"].ToString() + ",";
                DatosCampos += "'" + filas["EMPRESA"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE"].ToString() + "',";
                DatosCampos += "'" + filas["NUMERO"].ToString() + "',";
                DatosCampos += "'" + filas["ARTICULO"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES"].ToString() + "',";
                DatosCampos += "'" + filas["COMPONENTE"].ToString() + "',";
                DatosCampos += "'" + filas["UNIDADES1"].ToString() + "',";
                DatosCampos += "'" + filas["SERIE_LOTE"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_FABR"].ToString() + "',";
                DatosCampos += "'" + filas["FECHA_CADUC"].ToString() + "',";
                DatosCampos += "'" + filas["LIBRE3"].ToString() + "',";
                DatosCampos += "'" + filas["ALMACEN"].ToString() + "',";
                DatosCampos += "'" + filas["UBICACION"].ToString() + "',";
                DatosCampos += "'2'";

                //Destino
                SQL = "INSERT INTO ZIMPPRODTIPS(ID, EMPRESA, SERIE, NUMERO, ARTICULO, UNIDADES, COMPONENTE, UNIDADES1, SERIE_LOTE, FECHA_FABR, FECHA_CADUC, LIBRE3, ALMACEN, ";
                SQL += " UBICACION, ESTADO)";
                SQL += " VALUES (" + DatosCampos + ")";

                //DBHelper.ExecuteNonQuery(SQL);
                //Variables.mensajeserver += "Registro Insertado en local." + Environment.NewLine;
                //
                DBHelper.ExecuteNonQueryGolden(SQL);
                Variables.mensajeserver += "Registro Insertado en Servidor Golden." + Environment.NewLine;

                //Actualiza origen
                SQL = "UPDATE ZIMPPRODTIPS SET ESTADO = '2' ";
                //SQL += " WHERE ID = " + filas["ID"].ToString();
                SQL += " WHERE EMPRESA ='" + filas["EMPRESA"].ToString() + "' ";
                SQL += " AND SERIE ='" + filas["SERIE"].ToString() + "' ";
                SQL += " AND NUMERO ='" + filas["NUMERO"].ToString() + "' ";
                SQL += " AND ARTICULO ='" + filas["ARTICULO"].ToString() + "' ";
                SQL += " AND UNIDADES ='" + filas["UNIDADES"].ToString() + "' ";
                SQL += " AND COMPONENTE ='" + filas["COMPONENTE"].ToString() + "' ";
                SQL += " AND UNIDADES1 ='" + filas["UNIDADES1"].ToString() + "' ";
                SQL += " AND SERIE_LOTE ='" + filas["SERIE_LOTE"].ToString() + "' ";
                SQL += " AND FECHA_FABR ='" + filas["FECHA_FABR"].ToString() + "' ";
                SQL += " AND FECHA_CADUC ='" + filas["FECHA_CADUC"].ToString() + "' ";
                SQL += " AND LIBRE3 ='" + filas["LIBRE3"].ToString() + "' ";
                SQL += " AND UBICACION ='" + filas["UBICACION"].ToString() + "' ";

                DBHelper.ExecuteNonQuery(SQL);
                Variables.mensajeserver += "Registro actualizado." + Environment.NewLine;
            }
        }

        public static void ExecuteNonQueryProcedureVenta(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("dbo.VentaEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                Variables.mensajeserver += "VentaEjecucion ejecutado sin error";
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento MovVentaImportacion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedureModAlv(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("MovAlmaImportacion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@ID", "");
                cmd.Parameters.AddWithValue("@TIPO_FORM", "");
                cmd.Parameters.AddWithValue("@FECHA", "");
                cmd.Parameters.AddWithValue("@TIPO_PLANTA", "");
                cmd.Parameters.AddWithValue("@VARIEDAD", "");
                cmd.Parameters.AddWithValue("@LOTE", "");
                cmd.Parameters.AddWithValue("@UNIDADES", "");
                cmd.Parameters.AddWithValue("@NUM_UNIDADES", "");
                cmd.Parameters.AddWithValue("@MANOJOS", "");
                cmd.Parameters.AddWithValue("@DESDE", "");
                cmd.Parameters.AddWithValue("@HASTA", "");
                cmd.Parameters.AddWithValue("@OBSERVACIONES", "");
                cmd.Parameters.AddWithValue("@OK", "");
                cmd.Parameters.AddWithValue("@ESTADO", "");
                cmd.Parameters.AddWithValue("@OCU", "");
                cmd.Parameters.AddWithValue("@EMPRESA", "");
                cmd.Parameters.AddWithValue("@CODIGOGOLDEN", "");
                cmd.Parameters.AddWithValue("@FORMATO", "");//19
                cmd.Parameters.AddWithValue("@REGISTROANTERIOR", "");
                cmd.Parameters.AddWithValue("@CONTADOR", "");
                cmd.Parameters.AddWithValue("@SUMAUNIDADES", "");
                cmd.Parameters.AddWithValue("@BUSCAMANOJOS ", "");
                cmd.Parameters.AddWithValue("@TOTAL_PLANTAS", "");
                cmd.Parameters.AddWithValue("@FECHAANTERIOR", "");
                cmd.Parameters.AddWithValue("@VARIEDADANTERIOR", "");
                cmd.Parameters.AddWithValue("@OCUANTERIOR", "");
                cmd.Parameters.AddWithValue("@ALMACENANTERIOR", "");//28
                cmd.Parameters.AddWithValue("@UBICACIONANTERIOR", "");
                cmd.Parameters.AddWithValue("@DESDEANTERIOR", "");
                cmd.Parameters.AddWithValue("@MANOJOSANTERIOR ", "");
                cmd.Parameters.AddWithValue("@UNIDADESANTERIOR", "");
                cmd.Parameters.AddWithValue("@TIPO_PLANTAANTERIOR", "");
                cmd.Parameters.AddWithValue("@CONCATENALOTE", "");
                cmd.Parameters.AddWithValue("@CONCATENAFECHAINI", "");
                cmd.Parameters.AddWithValue("@FECHAFIN", "");
                cmd.Parameters.AddWithValue("@CONCATENAFECHAFIN ", "");//37
                cmd.Parameters.AddWithValue("@PROCESADO", "");
                cmd.Parameters.AddWithValue("@O_C_U", "");
                cmd.Parameters.AddWithValue("@CANTIDAD_PLANTAS", "");
                cmd.Parameters.AddWithValue("@ULTIMO_REGISTRO", "");
                cmd.Parameters.AddWithValue("@CODGOLDENANTERIOR", "");
                cmd.Parameters.AddWithValue("@ALMACEN_DESDE", "");//43
                cmd.Parameters.AddWithValue("@ALMACEN_HASTA", "");
                cmd.Parameters.AddWithValue("@ALMACENDESDEANTERIOR", "");
                cmd.Parameters.AddWithValue("@ALMACENHASTAANTERIOR", "");//46
                cmd.Parameters.AddWithValue("@UBICACIONDESDEANTERIOR", "");
                cmd.Parameters.AddWithValue("@UBICACIONHASTAANTERIOR", "");
                cmd.Parameters.AddWithValue("@UBICACION_DESDE", "");//49
                cmd.Parameters.AddWithValue("@UBICACION_HASTA", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento MovAlmaImportacion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }


        public static void ExeNonQueryProcMovVentas(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("VentaEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
                //cmd.Parameters.AddWithValue("@VARIOSPALET", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                string a = Main.Ficherotraza("ExeNonQueryProcMovVentas--> " + Ex.Message);
                Variables.mensajeserver = "Error de base de datos para el procedimiento MovVenta. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedureCompra(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("MovComprasEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento MovComprasEjecucion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedurePlantacion(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("PlantacionEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento PlantacionEjecucion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedureFrigo(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("FrigoEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento PaletAlvEjecucion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedureFlujos(string sqlSpName, string Procedimiento)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(Procedimiento, cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            //bLOTE =""; VARIOSPALET=""
            if (sqlSpName != "")
            {
                string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(sqlSpName, ";");

                foreach (string Linea in CadaLinea)
                {
                    string[] Parmaetro = System.Text.RegularExpressions.Regex.Split(Linea, "=");
                    cmd.Parameters.AddWithValue("@" + Parmaetro[0], Parmaetro[1]);
                }

                //cmd.Parameters.AddWithValue("@bLOTE", "");
                //cmd.Parameters.AddWithValue("@VARIOSPALET", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento PaletAlvEjecucion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedurePaletAlv(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("PaletAlvEjecucion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
                cmd.Parameters.AddWithValue("@VARIOSPALET", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos para el procedimiento PaletAlvEjecucion. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }


        public static void ExecuteNQProcedureAll(string IDProcedimiento)
        {
            //int I = 0;
            Variables.Error = "";
            //SELECT de consulta para las variables
            string SQL = "SELECT * FROM ZPROCEDIMIENTOS WHERE ZID =" + IDProcedimiento;
            DataTable dbA = Main.BuscaLote(SQL).Tables[0];


            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            
            SqlCommand cmd = null;
            Boolean Esta = false;
            foreach (DataRow filas in dbA.Rows)
            {
                if (filas["ZID"].ToString() == IDProcedimiento)
                {
                    cmd = new SqlCommand(filas["ZTITULO"].ToString(), cn);
                    Esta = true;
                }
            }
            if(Esta == false) 
            {
                Variables.mensajeserver = "Error de base de datos. No se encuentra el Procedimiento almacenado. " ;
                return;
            }
            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            foreach (DataRow filas in dbA.Rows)
            {
                if (filas["ZID"].ToString() == IDProcedimiento)
                {
                    string[] CadaLinea = System.Text.RegularExpressions.Regex.Split(filas["ZVARIABLES"].ToString(), ";");
                    foreach (string Linea in CadaLinea)
                    {
                        if (Linea.Contains("="))
                        {
                            string[] LineaIgual = System.Text.RegularExpressions.Regex.Split(Linea, "=");
                            cmd.Parameters.AddWithValue(LineaIgual[0], LineaIgual[1]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(Linea, "");
                        }
                    }
                }
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta


            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteProcedureQueryGold(string sqlSpName, string Empresa, string Filtros)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlCommand cmd = new SqlCommand("Orden_Carga_Filtro", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@ID", "");
                cmd.Parameters.AddWithValue("@ANUAL", "");
                cmd.Parameters.AddWithValue("@EMPRESA", Empresa);
                cmd.Parameters.AddWithValue("@BBDDa", "");
                cmd.Parameters.AddWithValue("@BBDDb", "");
                cmd.Parameters.AddWithValue("@COMPUTER", "");
                cmd.Parameters.AddWithValue("@CONDICIONES", Filtros);
                cmd.Parameters.AddWithValue("@CONSULTA", "");
                cmd.Parameters.AddWithValue("@MAXRECORD", "");
                cmd.Parameters.AddWithValue("@CURRENTRECORD", "");                
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteProcedureTareas(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlCommand cmd = new SqlCommand("FichajeTareas", cn);
            SqlCommand cmd = new SqlCommand("FichajeTareasEjecucion", cn);
            //Este procedimiento Ejecuta FichajeTareasAjusteHoras y FichajeTareas en este orden

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }
            //if (sqlSpName == "")
            //{
            //    cmd.Parameters.AddWithValue("@ID", "");
            //    cmd.Parameters.AddWithValue("@CODEMPLEADO", "");
            //    cmd.Parameters.AddWithValue("@NOMBRE", "");
            //    cmd.Parameters.AddWithValue("@APELLIDOS", "");
            //    cmd.Parameters.AddWithValue("@FECHA_EMPLEADOS", "");
            //    cmd.Parameters.AddWithValue("@HORA_EMPLEADO", "");
            //    cmd.Parameters.AddWithValue("@TABLET", "");
            //    cmd.Parameters.AddWithValue("@CODFINCA", "");
            //    cmd.Parameters.AddWithValue("@DESCRFINCA", "");
            //    cmd.Parameters.AddWithValue("@ZONA", "");
            //    cmd.Parameters.AddWithValue("@DESCRZONAZ", "");
            //    cmd.Parameters.AddWithValue("@TAREA", "");
            //    cmd.Parameters.AddWithValue("@DESCRTAREA", "");
            //    cmd.Parameters.AddWithValue("@INIFIN", "");
            //    cmd.Parameters.AddWithValue("@DESCRINIFIN", "");
            //    cmd.Parameters.AddWithValue("@ZPERIODO", "");
            //    cmd.Parameters.AddWithValue("@ZMINUTOS", "");
            //    cmd.Parameters.AddWithValue("@ZTOTALMINUTOS", "");
            //    cmd.Parameters.AddWithValue("@ZESTADO", "");
            //    cmd.Parameters.AddWithValue("@IDANTERIOR", "");
            //    cmd.Parameters.AddWithValue("@FECHAANTERIOR", "");
            //    cmd.Parameters.AddWithValue("@CODEMPLEADOANTERIOR", "");
            //    cmd.Parameters.AddWithValue("@HORA_EMPLEADOANTERIOR", "");
            //    cmd.Parameters.AddWithValue("@INIFINANTERIOR", "");
            //    cmd.Parameters.AddWithValue("@CONTADOR", "");
            //    cmd.Parameters.AddWithValue("@CUANTOS", "");
            //}

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta


            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "ExecuteProcedureTareas --> " + Ex.Message;
                string a = Main.Ficherotraza("FichajeTareasEjecucion --> " + Ex.Message + " --> FichajeTareasEjecucion");
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteProcedureUpdateTables(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("Delete_Tablas_Desarrollo_inserta_copia_de_produccion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@MAXRECORD", "");
                cmd.Parameters.AddWithValue("@CURRENTRECORD", "");
                cmd.Parameters.AddWithValue("@CONSULTA", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta


            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static void ExecuteNonQueryProcedure(string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand("ProdTipsImportacion", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@VEMPRESAANTERIOR", "");
                cmd.Parameters.AddWithValue("@VID", "");
                cmd.Parameters.AddWithValue("@VTIPO_FORM", "");
                cmd.Parameters.AddWithValue("@VFECHA", "");
                cmd.Parameters.AddWithValue("@VATIPO_PLANTA", "");
                cmd.Parameters.AddWithValue("@VAVARIEDAD", "");
                cmd.Parameters.AddWithValue("@VLOTE", "");
                cmd.Parameters.AddWithValue("@VUNIDADES", "");
                cmd.Parameters.AddWithValue("@VNUM_UNIDADES", "");
                cmd.Parameters.AddWithValue("@VMANOJOS", "");
                cmd.Parameters.AddWithValue("@VDESDE", "");
                cmd.Parameters.AddWithValue("@VHASTA", "");
                cmd.Parameters.AddWithValue("@VOBSERVACIONES", "");
                cmd.Parameters.AddWithValue("@VOK", "");
                cmd.Parameters.AddWithValue("@VESTADO", "");
                cmd.Parameters.AddWithValue("@VOCU", "");
                cmd.Parameters.AddWithValue("@VCAMPO_CULTIVO", "");
                cmd.Parameters.AddWithValue("@VEMPRESA", "");
                cmd.Parameters.AddWithValue("@VBVARIEDAD", "");
                cmd.Parameters.AddWithValue("@VCODGOLDEN", "");//19

                cmd.Parameters.AddWithValue("@VCTIPO_PLANTA", "");
                cmd.Parameters.AddWithValue("@VCVARIEDAD", "");
                cmd.Parameters.AddWithValue("@VALMACEN", "");
                cmd.Parameters.AddWithValue("@VDTIPO_PLANTA", "");
                cmd.Parameters.AddWithValue("@VTIPO_FORMATO", "");
                cmd.Parameters.AddWithValue("@VREGISTROANTERIOR", "");
                cmd.Parameters.AddWithValue("@VCONTADOR", "");
                cmd.Parameters.AddWithValue("@VCONUNIDADES", "");
                cmd.Parameters.AddWithValue("@VBUSCAMANOJOS", "");//28

                cmd.Parameters.AddWithValue("@TOTAL_PLANTAS", "");
                cmd.Parameters.AddWithValue("@VFECHAANTERIOR", "");
                cmd.Parameters.AddWithValue("@VARIEDADANTERIOR", "");
                cmd.Parameters.AddWithValue("@VOCUANTERIOR", "");
                cmd.Parameters.AddWithValue("@ALMACENANTERIOR", "");
                cmd.Parameters.AddWithValue("@VUBICACIONANTERIOR", "");
                cmd.Parameters.AddWithValue("@VDESDEANTERIOR", "");
                cmd.Parameters.AddWithValue("@VMANOJOSANTERIOR", "");
                cmd.Parameters.AddWithValue("@VUNIDADESANTERIOR", "");//37

                cmd.Parameters.AddWithValue("@VATIPO_PLANTAANTERIOR", "");
                cmd.Parameters.AddWithValue("@CONCATENALOTE", "");
                cmd.Parameters.AddWithValue("@CONCATENAFECHAINI", "");
                cmd.Parameters.AddWithValue("@FECHAFIN", "");
                cmd.Parameters.AddWithValue("@CONCATENAFECHAFIN", "");
                cmd.Parameters.AddWithValue("@VPROCESADO", "");//43

                cmd.Parameters.AddWithValue("@VO_C_U", "");
                cmd.Parameters.AddWithValue("@VCANTIDAD_PLANTAS", "");
                cmd.Parameters.AddWithValue("@VULTIMO_REGISTRO", "");//46

                cmd.Parameters.AddWithValue("@VHECTAREA", "");
                cmd.Parameters.AddWithValue("@VHECTAREAANTERIOR", "");
                cmd.Parameters.AddWithValue("@VCODGOLDENANTERIOR", "");//49
                cmd.Parameters.AddWithValue("@VUBICACION", "");
            }

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta
           
            
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }





        public static void ExecuteNonQueryProcedurePinch(string sqlSpName)
        {
            HttpContext context = HttpContext.Current;
            //int I = 0;
            Variables.Error = "";
            string SQL = "select [Siguente] as Siguiente from Contadores where Clase = 'R' and Tipo = 'R' and Codigo = 'OPA'";
            string NumeroSecuencia = "0";
            string a = Main.ETrazas(sqlSpName, "0", " ExecuteNonQueryProcedurePinch -->" + context.Session["Procedimiento"].ToString());
#pragma warning disable CS0168 // La variable 'ex' se ha declarado pero nunca se usa
            try
            {
                DataTable dbB = Main.BuscaNumeroVRE(SQL).Tables[0];
                foreach (DataRow fila in dbB.Rows)
                {
                    NumeroSecuencia = fila["Siguiente"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                NumeroSecuencia = "1";
            }
#pragma warning restore CS0168 // La variable 'ex' se ha declarado pero nunca se usa


            SqlParameter[] dbParams1 = new SqlParameter[0];
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlConnection.SqlDataSource.ConflictDetection
            //SqlCommand cmd = new SqlCommand("PinchAlvCabImportacion", cn);
            SqlCommand cmd = new SqlCommand("PinchAlvCabEjecucion", cn);
            
            cmd.CommandType = CommandType.StoredProcedure;

            //Si necesita parámetros de casillas de texto enviar valor desde string del procedimiento
            if (sqlSpName == "")
            {
                cmd.Parameters.AddWithValue("@bLOTE", "");
            }
            //if (sqlSpName == "")
            //{
            //    cmd.Parameters.AddWithValue("@aSUMADOR", "");
            //    cmd.Parameters.AddWithValue("@aLOTEDESTINO", "");
            //    cmd.Parameters.AddWithValue("@aLOTE", "");
            //    cmd.Parameters.AddWithValue("@aEMPRESA", "");
            //    cmd.Parameters.AddWithValue("@aSERIE", "");
            //    cmd.Parameters.AddWithValue("@aARTICULO", "");
            //    cmd.Parameters.AddWithValue("@aDESCRIPCION", "");
            //    cmd.Parameters.AddWithValue("@aDESDE", "");
            //    cmd.Parameters.AddWithValue("@aUBICACION_ENTRADA", "");
            //    cmd.Parameters.AddWithValue("@aALMACEN_ENTRADA", "");
            //    cmd.Parameters.AddWithValue("@aFECHAFABR", "");
            //    cmd.Parameters.AddWithValue("@aFECHA_CADUC", "");
            //    cmd.Parameters.AddWithValue("@aESTADO", "");
            //    cmd.Parameters.AddWithValue("@aTIPO_PLANTA", "");
            //    cmd.Parameters.AddWithValue("@aLIBRE2", "");
            //    cmd.Parameters.AddWithValue("@aTIPO_FORM", "");
            //    cmd.Parameters.AddWithValue("@aUNIDADES", "");
            //    cmd.Parameters.AddWithValue("@aNUM_UNIDADES", "");
            //    cmd.Parameters.AddWithValue("@aVARIEDAD", "");//19
            //    cmd.Parameters.AddWithValue("@aTUNELES", "");
            //    cmd.Parameters.AddWithValue("@aPASILLOS", "");
            //    cmd.Parameters.AddWithValue("@aID", "");
            //    cmd.Parameters.AddWithValue("@aPRUEBA", "");
            //    cmd.Parameters.AddWithValue("@cLOTEDESTINO", "");
            //    cmd.Parameters.AddWithValue("@aTOTAL_PLANTAS", "");//0
            //    cmd.Parameters.AddWithValue("@aULTIMO_REGISTRO", "");//0
            //    cmd.Parameters.AddWithValue("@aCANTIDAD_PLANTAS", "");//0
            //    cmd.Parameters.AddWithValue("@aBUSCAMANOJOS", "");//0
            //    cmd.Parameters.AddWithValue("@aSUMUNIDADES", "");//0
            //    cmd.Parameters.AddWithValue("@aCONCATENALOTE", "");
            //    cmd.Parameters.AddWithValue("@aSUMA_PLANTAS", "");//0
            //    cmd.Parameters.AddWithValue("@aMANOJOS", "");
            //    cmd.Parameters.AddWithValue("@aCONTADOR", "");//0
            //    cmd.Parameters.AddWithValue("@aPROCESADO", "");//0
            //    cmd.Parameters.AddWithValue("@aCOMPONENTE", "");
            //    cmd.Parameters.AddWithValue("@bID", "");
            //    cmd.Parameters.AddWithValue("@bTIPO_FORM", "");
            //    cmd.Parameters.AddWithValue("@bFECHA", "");
            //    cmd.Parameters.AddWithValue("@bTIPO_PLANTA", "");
            //    cmd.Parameters.AddWithValue("@bVARIEDAD", "");
            //    cmd.Parameters.AddWithValue("@bLOTE", "");
            //    cmd.Parameters.AddWithValue("@bLOTEDESTINO", "");
            //    cmd.Parameters.AddWithValue("@bUBICACION_SALIDA", "");
            //    cmd.Parameters.AddWithValue("@bALMACEN_SALIDA", "");
            //    cmd.Parameters.AddWithValue("@bUNIDADES", "");
            //    cmd.Parameters.AddWithValue("@bNUM_UNIDADES", "");
            //    cmd.Parameters.AddWithValue("@bMANOJOS", "");
            //    cmd.Parameters.AddWithValue("@bDESDE", "");
            //    cmd.Parameters.AddWithValue("@bHASTA", "");
            //    cmd.Parameters.AddWithValue("@bETDESDE", "");
            //    cmd.Parameters.AddWithValue("@bETHASTA", "");
            //    cmd.Parameters.AddWithValue("@bLIBRE3", "");
            //    cmd.Parameters.AddWithValue("@bOBSERVACIONES", "");
            //    cmd.Parameters.AddWithValue("@bOK", "");
            //    cmd.Parameters.AddWithValue("@bESTADO", "");
            //    cmd.Parameters.AddWithValue("@bFECHAEXP", "");
            //    cmd.Parameters.AddWithValue("@bID_SECUENCIA", "");
            //    cmd.Parameters.AddWithValue("@ULTIMO_REGISTRO", "");//0
            //    cmd.Parameters.AddWithValue("@CONTADOR", "");//0
            //    cmd.Parameters.AddWithValue("@NUMERO", "");// NumeroSecuencia);
            //    cmd.Parameters.AddWithValue("@CODIGOGOLDEN", "");

            //}

            //declaramos el parámetro de retorno
            SqlParameter ValorRetorno = new SqlParameter("@Comprobacion", SqlDbType.Int);
     
            //asignamos el valor de retorno
            ValorRetorno.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(ValorRetorno);
            //executamos la consulta
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                a = Main.ETrazas(sqlSpName, "1", " ExecuteNonQueryProcedurePinch -->" + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL") + " .-Valor retorno =" + ValorRetorno.ToString() + " --> " + context.Session["Procedimiento"].ToString());
                Variables.mensajeserver = "Error de base de datos. El error: " + Ex.Message;
                throw new Exception("Error de base de datos.", Ex);

            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }


        //public static void ExecuteNonQuery(int TipoConexion, string sqlSpName, string Conexion, string Tabla)

        public static void ExecuteNonQueryGolden(string sqlSpName, SqlParameter[] dbParams = null)
        {
            //int I = 0;
            Variables.Error = "";
            HttpContext context = HttpContext.Current;
            SqlParameter[] dbParams1 = new SqlParameter[0];
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandType = CommandType.Text;
            string a = Main.ETrazas("", "0", " ExecuteNonQueryGolden --> " + sqlSpName + " # " + context.Session["Procedimiento"].ToString());
            if (dbParams1 != null)
            {
                foreach (SqlParameter dbParam in dbParams1)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
           
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                //crear mensaje
                a = Main.ETrazas(sqlSpName, "1", " ExecuteNonQueryGolden -->" + Ex.Message + ". Configuracion -->" + ConfigurationManager.AppSettings.Get("connectionSQL") + " --> Procedimiento: " + context.Session["Procedimiento"].ToString()) + " --> SQL = " + sqlSpName;
                //string a = Main.Ficherotraza("ExecuteNonQueryGolden --> " + Ex.Message);
                Variables.mensajeserver = Ex.Message;
                throw new Exception("Error de base de datos.", Ex);
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }


        public static void ExecuteNonQuery( string sqlSpName)
        {
            //int I = 0;
            Variables.Error = "";
            HttpContext context = HttpContext.Current;

            SqlParameter[] dbParams1 = new SqlParameter[0];
            //SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            //cmd1.CommandType = CommandType.StoredProcedure;
            string a = "";// Main.ETrazas(sqlSpName, "0", " ExecuteNonQuery -->" + context.Session["Procedimiento"].ToString());

            cmd.CommandType = CommandType.Text;
            if (dbParams1 != null)
            {
                foreach (SqlParameter dbParam in dbParams1)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                //Traza
                //string a = Main.Ficherotraza("BuscaLoteReco -->" + sqlSpName);

            }
            catch (Exception Ex)
            {
                //crear mensaje
                a = Main.ETrazas(sqlSpName, "1", " ExecuteNonQuery -->" + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL") + " --> " + context.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("ExecuteNonQuery --> " + Ex.Message + "==> " + sqlSpName );
                Variables.Error = a;
                Variables.mensajeserver = "ExecuteNonQuery --> " + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL");
                throw new Exception("Error de base de datos.", Ex);
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }
        }

        public static object ExecuteScalarSQL(string sqlSpName, SqlParameter[] dbParams) 
        {
            HttpContext context = HttpContext.Current;
            object retVal = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            string a = ""; // Main.ETrazas(sqlSpName, "0", " ExecuteScalarSQL -->" + context.Session["Procedimiento"].ToString());
            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    {
                    cmd.Parameters.Add(dbParam);
                }
            }

           cn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                a = Main.ETrazas(sqlSpName, "1", " ExecuteScalarSQL -->" + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL") + " --> " + context.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("ExecuteScalarSQL --> " + Ex.Message);
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

            return retVal;
        }

        public static object ExecuteScalarSQLReco(string sqlSpName, SqlParameter[] dbParams)
        {
            HttpContext context = HttpContext.Current;
            object retVal = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLReco"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            string a = ""; // Main.ETrazas(sqlSpName, "0", " ExecuteScalarSQL -->" + context.Session["Procedimiento"].ToString());
            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                a = Main.ETrazas(sqlSpName, "1", " ExecuteScalarSQLReco -->" + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQLReco") + " --> " + context.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("ExecuteScalarSQL --> " + Ex.Message);
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

            return retVal;
        }

        public static object ExecuteScalarSQLNoNe(string sqlSpName, SqlParameter[] dbParams, string Conexion)
        {
            HttpContext context = HttpContext.Current;
            object retVal = null;
            SqlConnection cn = new SqlConnection(Conexion);
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            string a = ""; // Main.ETrazas(sqlSpName, "0", " ExecuteScalarSQL -->" + context.Session["Procedimiento"].ToString());
            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                a = Main.ETrazas(sqlSpName, "1", " ExecuteScalarSQLNone -->" + Ex.Message + "." + Conexion + " --> " + context.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("ExecuteScalarSQL --> " + Ex.Message);
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

            return retVal;
        }

        public static SqlParameter MakeParamSQL(string paramName, SqlDbType dbType, int size, object objValue)
        {
            SqlParameter param;
          
            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Value = objValue;

            return param;
        }

        public static SqlParameter MakeParamOutputSQL(string paramName, SqlDbType dbType, int size)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);         
            else
                param = new SqlParameter(paramName, dbType);

            param.Direction = ParameterDirection.Output;

            return param;
        }

        public static void CreateEmptyDirectory(string fullPath)
        {
            if (!System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.CreateDirectory(fullPath);
            }
        }
        //Buscar en fichero


        public static string Carga_SesionRegistro(string sUserName, string sPassword, string CuentaMail, string GetIPMiHost, string Linea, string[] MiEquipo)
        {
            HttpContext.Current.Session["OtherUserClave"] = sUserName; //key
            HttpContext.Current.Session["pkiM"] = sPassword;

            string miro = Seguridad.descifrar(sPassword, false, 2);
            string miro2 = Seguridad.descifrar(Linea, false, 2);
            string miro3 = Seguridad.DesEncriptar(GetIPMiHost);
            string miro5 = Seguridad.DesEncriptar(miro3);
            //string miro4 = Seguridad.descifrar(GetIPMiHost, false, 2);
            //HttpContext.Current.Session["pki1"] = Seguridad.descifrar(miro2, false, 2);

            string[] headers = System.Text.RegularExpressions.Regex.Split(miro2, ";");
            for (int a = 0; a < headers.Count(); a++)
            {
                if (a == 0) { HttpContext.Current.Session["otheralias"] = headers[a]; }
                if (a == 1) { HttpContext.Current.Session["otherpassword"] = headers[a]; }
                if (a == 2) { HttpContext.Current.Session["otherMail"] = headers[a]; }
            }

            //Si no encuentra una sesion de usuario no se conecta.
            if (Variables.configuracionDB == 0)
            {

            }
            else
            {
                string strFilePathMail = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000001B.000";
                string strFilePathAlias = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000001A.000";
                string MiLinea = "";
                string Miro = "";
                Boolean Esta = false;
                int AA = 0;
                int BB = 0;
#pragma warning disable CS0219 // La variable 'IdSession' está asignada pero su valor nunca se usa
                string IdSession = "";
#pragma warning restore CS0219 // La variable 'IdSession' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'Midisco' está asignada pero su valor nunca se usa
                string Midisco = "";
#pragma warning restore CS0219 // La variable 'Midisco' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'Ruta' está asignada pero su valor nunca se usa
                string Ruta = "";
#pragma warning restore CS0219 // La variable 'Ruta' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'na' está asignada pero su valor nunca se usa
                string na = "";
#pragma warning restore CS0219 // La variable 'na' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'CC' está asignada pero su valor nunca se usa
                string CC = "";
#pragma warning restore CS0219 // La variable 'CC' está asignada pero su valor nunca se usa
                DataTable dsMio = new DataTable();
#pragma warning disable CS0219 // La variable 'EstaID' está asignada pero su valor nunca se usa
                Boolean EstaID = false;
#pragma warning restore CS0219 // La variable 'EstaID' está asignada pero su valor nunca se usa
#pragma warning disable CS0219 // La variable 'EstaDp' está asignada pero su valor nunca se usa
                Boolean EstaDp = false;
#pragma warning restore CS0219 // La variable 'EstaDp' está asignada pero su valor nunca se usa
                //Si entra por registro busco en las tablas de usuario y mail para que me de una linea
                using (StreamReader READERm = new StreamReader(strFilePathMail)) // "Mail"))
                {
                    while (!READERm.EndOfStream)
                    {
                        MiLinea = READERm.ReadLine();
                        if (MiLinea.ToLower().Contains(HttpContext.Current.Session["otherMail"].ToString().ToLower()))
                        {
                            Esta = true;
                            break;
                        }
                        AA += 1;
                    }
                }
                //Si no es true no busco ni el alias
                if (Esta == true)
                {
                    BB = 0;
                    //Si no encontró el mail,Aqui busca y debe ser correcto en la misma linea
                    using (StreamReader READERa = new StreamReader(strFilePathAlias)) // "Alias"))
                    {
                        while (!READERa.EndOfStream)
                        {
                            MiLinea = READERa.ReadLine();
                            if (AA == BB)
                            {
                                if (MiLinea.ToLower().Contains(HttpContext.Current.Session["otheralias"].ToString().ToLower()))
                                {
                                    Esta = true;
                                    break;
                                }
                                Esta = false;
                            }
                            BB += 1;
                        }
                    }
                }
                //La cuenta y el alias es true
                if (Esta == true)
                {
                    // Ahora compruebo que es el mismo equipo que se registro
                    Esta = false;
                    //Si llega hasta aqui, esta utilizando el mismo equipo, el mismo login, la misma ip publica y la ip privada, asi que ahora compruebo la semilla 
                    Miro = HttpContext.Current.Session["otheralias"] + "<@>" + HttpContext.Current.Session["otherpassword"];
                    Miro = Seguridad.cifrar(HttpContext.Current.Session["otheralias"] + "<@>" + HttpContext.Current.Session["otherpassword"], false, 0);
                    Miro = DBHelper.BuscaUnaLinea(Miro, HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000000008.000"); // LOGIN.txt");
                    if (Miro != "")
                    {
                        string MiDato = "";
                        headers = System.Text.RegularExpressions.Regex.Split(Miro, ";");
                        Miro = DBHelper.BuscaLaLinea(headers[0], HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000000012.000"); // USUARIO.txt");
                                                                                                                                                  //Descifrar la linea con la misma de arriba
                        headers = System.Text.RegularExpressions.Regex.Split(Miro, "<@>");
                        for (int a = 0; a < headers.Count(); a++)
                        {
                            if (headers[1].ToString() == HttpContext.Current.Session["otheralias"].ToString() && headers[2].ToString() == HttpContext.Current.Session["otherpassword"].ToString())
                            {
                                if (a == 3) { HttpContext.Current.Session["idotheruser"] = headers[a].ToString(); break; }//identificador                            
                            }
                            MiDato += headers[a].ToString() + "<@>";
                        }
                    }
                    //Si no encuentra usuario, no existe
                    if (HttpContext.Current.Session["idotheruser"].ToString() == "") { return "HomeSite"; }

                    //Ahora busco en la imagen del equipo que se registró en su momento
                    string fic = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\000000002B.000";
                    System.IO.StreamReader sr = new System.IO.StreamReader(fic);
                    string texto = sr.ReadToEnd();
                    sr.Close();

                    String[] Misel = System.Text.RegularExpressions.Regex.Split(texto, Environment.NewLine);

                    int i = 0;
                    for (int a = 0; a < Misel.Count(); a++)
                    {
                        string mm = Misel[a];
                        string nn = MiEquipo[a];
                        if (Misel[a] != "")
                        {
                            if (Misel[a].Contains("Alias = "))
                            {
                                string prueba = HttpContext.Current.Session["otheralias"].ToString();
                                if (Misel[a].Contains(HttpContext.Current.Session["otheralias"].ToString()))
                                {
                                    i += 1;
                                }
                            }
                            else if (Misel[a].Contains("Email = "))
                            {
                                string prueba = HttpContext.Current.Session["otherMail"].ToString();
                                if (Misel[a].Contains(HttpContext.Current.Session["otherMail"].ToString()))
                                {
                                    i += 1;
                                }
                            }
                            else if (Misel[a].Contains("Pass = "))
                            {
                                string prueba = HttpContext.Current.Session["otherpassword"].ToString();
                                if (Misel[a].Contains(HttpContext.Current.Session["otherpassword"].ToString()))
                                {
                                    i += 1;
                                }
                            }
                            else if (Misel[a].ToString() == MiEquipo[a].ToString())
                            {
                                i += 1;
                            }
                        }
                    }

                    if (i < 6) { return "HomeSite"; }

                    //Por si es la primera vez que entra encripto con la semilla de usuario y servidor
                    Miro = DBHelper.BuscaLaLinea("1", HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000000007.000");

                    if (Miro.Contains("<@>"))
                    {
                        ////Correcto. Es la primera vez que entra. Cifro con la semilla de usuario y servidor
                        StreamWriter swA = File.CreateText(HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000000007.000");
                        try
                        {
                            swA.WriteLine(Seguridad.cifrar(HttpContext.Current.Session["OtherUserClave"].ToString(), false, 0));//La primera es de usuario
                            swA.WriteLine(Seguridad.cifrar(miro + "<@>" + miro2 + "<@>" + miro5, false, 0));//La segunda es para el servidor
                            swA.Close();
                        }
                        catch
                        {
                            swA.Close();
                        }
                    }
                    else
                    {//Si esta encriptado
                        fic = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000000007.000";
                        System.IO.StreamReader sr2 = new System.IO.StreamReader(fic);
                        texto = sr2.ReadToEnd();
                        sr2.Close();
                        Esta = false;
                        string Llave = Seguridad.cifrar(HttpContext.Current.Session["OtherUserClave"].ToString(), false, 0);

                        Misel = System.Text.RegularExpressions.Regex.Split(texto, Environment.NewLine);

                        for (int a = 0; a < Misel.Count(); a++)
                        {
                            string mm = Misel[a];
                            if (Misel[a] != "")
                            {
                                if (Misel[a] == Llave)
                                {
                                    Esta = true;
                                    break;
                                }
                            }
                        }
                        if (Esta == false)
                        {
                            using (System.IO.StreamWriter File = new System.IO.StreamWriter(fic, true))
                            {
                                File.WriteLine(Seguridad.cifrar(HttpContext.Current.Session["OtherUserClave"].ToString(), false, 0));//La primera es de usuario
                                File.WriteLine(Seguridad.cifrar(miro + "<@>" + miro2 + "<@>" + miro5, false, 0));//La segunda es para el servidor
                            }
                            Esta = true;
                        }

                        //Envio la configuracion inicial
                        fic = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000002C.000";
                        System.IO.StreamReader sr3 = new System.IO.StreamReader(fic);
                        texto = sr3.ReadToEnd();
                        sr3.Close();
                        texto += Environment.NewLine;
                        //0000001C.000
                        string ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000001C.000";
                        System.IO.StreamReader srA = new System.IO.StreamReader(ficA);
                        string textoA = srA.ReadToEnd();
                        srA.Close();
                        String ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000001C.zip";
                        Carga_FicherosINI(textoA, ficB);
                        string MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000001C.zip");
                        texto += "00<@>00<@>0000001C.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000002A.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000002A.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000002A.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000002A.zip");
                        texto += "00<@>00<@>0000002A.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000003A.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000003A.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000003A.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000003A.zip");
                        texto += "00<@>00<@>0000003A.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000003B.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000003B.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000003B.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000003B.zip");
                        texto += "00<@>00<@>0000003B.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000003C.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000003C.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000003C.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000003C.zip");
                        texto += "00<@>00<@>0000003C.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000003D.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000003D.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000003D.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000003D.zip");
                        texto += "00<@>00<@>0000003D.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //0000003E.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\0000003E.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\0000003E.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/0000003E.zip");
                        texto += "00<@>00<@>0000003E.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000027.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000027.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000027.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000027.zip");
                        texto += "00<@>00<@>00000027.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000034.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000034.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000034.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000034.zip");
                        texto += "00<@>00<@>00000034.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000035.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000035.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000035.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000035.zip");
                        texto += "00<@>00<@>00000035.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000036.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000036.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000036.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000036.zip");
                        texto += "00<@>00<@>00000036.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000037.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000037.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000037.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000037.zip");
                        texto += "00<@>00<@>00000037.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;
                        //00000039.000
                        ficA = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\00000039.000";
                        srA = new System.IO.StreamReader(ficA);
                        textoA = srA.ReadToEnd();
                        srA.Close();
                        ficB = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + HttpContext.Current.Session["idotheruser"].ToString() + "\\00000039.zip";
                        Carga_FicherosINI(textoA, ficB);
                        MiUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", "User/" + HttpContext.Current.Session["idotheruser"].ToString() + "/00000039.zip");
                        texto += "00<@>00<@>00000039.000<@>URL<@>000<@>0<@>" + MiUrl + Environment.NewLine;



                        HttpContext.Current.Session["Produccion"] = Seguridad.cifrar(texto, false, 2);
                        return HttpContext.Current.Session["Produccion"].ToString();
                    }
                }
                else
                {
                    return "HomeSite";
                }
            }
            return "";
        }
        public static string Carga_FicherosINI(string ContentOrigen, string FileDestino)
        {
            string[] headers = System.Text.RegularExpressions.Regex.Split(ContentOrigen, Environment.NewLine);

            StreamWriter swA = File.CreateText(FileDestino);
            try
            {
                for (int a = 0; a < headers.Count(); a++)
                {
                    swA.WriteLine(Seguridad.cifrar(headers[a], false, 2));//La primera es de usuario
                }
                swA.Close();
            }
            catch
            {
                swA.Close();
            }
            return "";
        }
        public static string BuscaUnaLinea(string Lines, string strFilePath)
        {
            string MiLinea = "";
            int i = 1;
            using (StreamReader READER = new StreamReader(strFilePath))
            {
                while (!READER.EndOfStream)
                {
                    MiLinea = READER.ReadLine();
                    if (MiLinea == Lines)
                    {
                        return i.ToString() + ";" + MiLinea;
                    }
                    i += 1;
                }
            }
            MiLinea = "";
            return MiLinea;
        }

        public static string BuscaLaLinea(string Lines, string strFilePath)
        {
            string MiLinea = "";
            int i = 1;
            using (StreamReader READER = new StreamReader(strFilePath))
            {
                while (!READER.EndOfStream)
                {
                    MiLinea = READER.ReadLine();
                    if (i.ToString() == Lines)
                    {
                        //return Seguridad.descifrar(MiLinea, false); comentado por registro, no se si ya esta encriptado
                        return MiLinea;
                    }
                    i += 1;
                }
            }
            MiLinea = "";
            return MiLinea;
        }
        public static int BuscaMailAlias(string Alias, string Email, string Pass, int TipoDatos)
        {
            int Miro = 0;

            if (TipoDatos == 1)
            {
                string strFilePathMail = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000001B.000";
                string strFilePathAlias = HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000001A.000";
                string MiLinea = "";
                Boolean CifradoA = false;
#pragma warning disable CS0219 // La variable 'CifradoB' está asignada pero su valor nunca se usa
                Boolean CifradoB = false;
#pragma warning restore CS0219 // La variable 'CifradoB' está asignada pero su valor nunca se usa
                string BuscoA = "";//String
                string BuscoB = "";//String


                ///Mira si hay o no Cifrado
                using (StreamReader READERm = new StreamReader(strFilePathMail)) // "Mail"))
                {
                    while (!READERm.EndOfStream)
                    {
                        MiLinea = READERm.ReadLine();
                        if (MiLinea.Contains("@") && BuscoA == "")//Esta sin cifrar
                        {
                            BuscoA = Email;//usuaroi
                        }
                        else if (BuscoA == "")
                        {
                            BuscoA = Seguridad.cifrar(Email, false, 0);//Maquina
                            CifradoA = true;
                        }
                        if (MiLinea == BuscoA)
                        {
                            Miro = 1;
                            break;
                        }
                    }
                }
                if (Miro == 0)
                {
                    using (StreamReader READERa = new StreamReader(strFilePathAlias)) // "Alias"))
                    {
                        while (!READERa.EndOfStream)
                        {
                            MiLinea = READERa.ReadLine();
                            if (MiLinea.Contains("<@>") && BuscoB == "")//Esta sin cifrar
                            {
                                BuscoB = Alias;//usuario
                            }
                            else if (BuscoB == "")
                            {
                                BuscoB = Seguridad.cifrar(Alias, false, 0);//Maquina
                                CifradoB = true;
                            }
                            if (MiLinea == BuscoB)
                            {
                                Miro = 2;
                                break;
                            }
                        }
                    }
                }
                if (Miro == 0)
                {
                    //Carga todas las tablas
                    //To Hex:
                    //string hex = intValue.ToString("X");
                    //To int:
                    //int intValue = int.Parse(hex, System.Globalization.NumberStyles.HexNumber)

                    //Escribe email en fichero registro
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(strFilePathMail, true);
                    if (CifradoA == true)
                    {
                        sw.WriteLine(Seguridad.cifrar(Email, false, 0));
                    }
                    else
                    {
                        sw.WriteLine(Email);
                    }

                    sw.Close();
                    //escribe alias en fichero registro Environment.NewLine
                    sw = new System.IO.StreamWriter(strFilePathAlias, true);
                    if (CifradoA == true)
                    {
                        sw.WriteLine(Seguridad.cifrar(Alias, false, 0));
                    }
                    else
                    {
                        sw.WriteLine(Alias);
                    }
                    sw.Close();
                    //Lee contador usuario cifrado
                    string fic = @HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\000000001C.000";
                    System.IO.StreamReader sr = new System.IO.StreamReader(fic);
                    string texto = sr.ReadToEnd();
                    sr.Close();
                    //Suma 1 y guarda al contador usuario
                    texto = Seguridad.descifrar(texto, false, 0);
                    int Contador = Convert.ToInt32(texto) + 1;
                    sw = new System.IO.StreamWriter(fic, false); //sobrescribe
                    sw.WriteLine(Seguridad.cifrar(Contador.ToString(), false, 0));
                    sw.Close();
                    //convierte a hexadecimal
                    string hex = Contador.ToString("X");
                    //Creo directorio
                    string directorio = "";
                    for (int i = 0; i < (10 - hex.Length); i++)
                    {
                        directorio += "0";
                    }
                    directorio += hex.ToString();
                    HttpContext.Current.Session["idusuario"] = directorio;
                    string Ruta = @HttpContext.Current.Request.PhysicalApplicationPath + "\\User\\" + directorio;
                    CreateEmptyDirectory(Ruta);
                    //Prueba de generacion directorio perfil
                    Carga_secuencia_Directorios();
                    //Cargo ficheros
                    Tablas_Usuario(Alias, Email, Pass);
                    return -1;

                }
            }
            else if (TipoDatos == 0)
            {
                //Resto Base de Datos
                DataSet ds = null;
                DataTable dt = null;

                string CC = ConfigurationManager.AppSettings.Get("connectionSQL");
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = " Select A.ZCODIGO, A.ZALIAS, A.ZPASSWORD, A.ZROOT, A.ZKEY, A.ZNIVEL, B.ZID_TABLA, B.ZID_REGISTRO ";
                Miquery += " from ZUSUARIOS A, ZLLAVES B ";
                Miquery += " where A.ZALIAS = '" + Alias + "'";
                Miquery += " and A.ZPASSWORD ='" + Pass + "'";
                Miquery += " and A.ZMAIL = '" + Email + "'";
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

            }
            return Miro;
        }

        public static void Tablas_Usuario(string Alias, string Email, string Pass)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Ruta_Fichero\\", true);
            //cifrarlo
            string texto = Seguridad.cifrar(Alias + "<@>" + Pass, false, 0);
            //string texto = Alias + "<@>" + Pass;
            //string texto = Alias + "<@>" + Pass;
            sw.WriteLine(texto);
            sw.Close();
            using (StreamWriter wr = File.CreateText("Ruta_Fichero"))//particular
            {
                //Leer del Data
                System.IO.StreamReader sr = new System.IO.StreamReader(HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Ruta_Fichero\\");
                string texto2 = sr.ReadLine();
                sr.Close();
                wr.WriteLine(texto2);
                wr.WriteLine(texto);
            }
        }
        private static string Carga_secuencia_Directorios()
        {
            //convierte a hexadecimal con mascara
            CreateEmptyDirectory(HttpContext.Current.Request.PhysicalApplicationPath + "\\Data\\Secuencia_Directorios");
            for (int i = 0; i < 30; i++)
            {
                string hex = i.ToString("X");
                //Creo directorio
                string directorio = "";
                for (int a = 0; a < (10 - hex.Length); a++)
                {
                    directorio += "0";
                }
                directorio += hex.ToString();
                string Ruta = @HttpContext.Current.Request.PhysicalApplicationPath + "\\\\Data\\Secuencia_Directorios\\" + i + "-" + directorio;
                CreateEmptyDirectory(Ruta);
            }
            return "";
        }

        public static int ExecuteNonQueryOutputSQL(string sqlSpName, SqlParameter[] dbParams, string paramName, SqlDbType dbType, int size)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);

            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    cmd.Parameters.Add(dbParam);
            }
            SqlParameter OutParam = MakeParamOutputSQL(paramName, dbType, size);
            cmd.Parameters.Add(OutParam);

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
            if (OutParam.Value == null) return 0;
            else return System.Convert.ToInt16(OutParam.Value);
        }

        #region Example
        //public static DataSet Get(CGasto gasto)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[]
        //        {
        //            DBHelper.MakeParam("@Id", SqlDbType.Int, 0, gasto.Id),
        //        };
        //    return DBHelper.ExecuteDataSet("usp_CListGasto_Get", dbParams);

        //} 
        #endregion

    }
}
