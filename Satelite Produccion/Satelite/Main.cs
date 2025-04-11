using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using Admin.Clases;
//using Admin.Classes;
//using Oracle.DataAccess.Client;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Satelite
{
    public partial class Main
    {

        //LOGIN
        public static void Sesion_Salir()
        {
            HttpContext context = HttpContext.Current;
      
            context.Session["UserID"] = "";
            context.Session["UserName"] = "";
            context.Session["UserAlias"] = "";
            context.Session["UserDestino"] = "";
            context.Session["UserDescripcion"] = "";
            context.Session["UserPassword"] = "";
            context.Session["UserIdentificacion"] = "";
            context.Session["Nivel"] = "";
            context.Session["Area"] = "";
            context.Session["IDArea"] = "";
            context.Session["Competencia"] = "";
            context.Session["Calificacion"] = "";
            context.Session["Peso"] = "";
            context.Session["Evaluacion"] = "";
            context.Session["Rol"] = "";
            context.Session["Grupo"] = "";
            context.Session["Area"] = "";
            context.Session["Session"] = "";
            context.Session["UserID"] = "";
            context.Session["UserName"] = "";
            context.Session["UserAlias"] = "";
            context.Session["UserDescripcion"] = "";
            context.Session["UserPassword"] = "";
            context.Session["UserIdentificacion"] = "";
            context.Session["UserDestino"] = "";
            context.Session["MiNivel"] = "";
            context.Session["Query"] = "";
            context.Session["Edicion"] = "";
            context.Session["TipoConexion"] = "";

            
        }


        //USUARIOS
        public static DataSet CargaIDUsuario(int ID)
        {

            DataSet ds = null;

            //DataSet ds = null;
            DataSet MyDataSet = new DataSet();
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                
                string Miquery = "SELECT ZROW FROM ZARCHIVOS WHERE ZID =" + ID + ";";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZROW FROM ZARCHIVOS WHERE ZID =" + ID + ";";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaUsuario()
        {
           
            //Consulta con Procedimiento almacenado
            //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_USUARIOS", cn);
            //comando.CommandType = CommandType.StoredProcedure;

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));
            //tabla usuarios
            //string Miquery = "SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZNOMBRE, A.ZAPELLIDO1, A.ZAPELLIDO2, A.ZDESCRIPCION, A.ZPASSWORD, C.ZDESCRIPCION AS ZCARGO,";
            //Miquery += " A.ZNIVEL AS ZID_NIVEL, ";
            //Miquery += " B.ZDESCRIPCION AS ZNIVEL, A.ZRESPONSABLE AS ZID_RESPONSABLE, M.ZNOMBRE || ' ' || M.ZAPELLIDO1 || ' ' || M.ZAPELLIDO2 AS ZRESPONSABLE,";
            //Miquery += " A.ZDOMICILIO,";
            //Miquery += "  A.ZCARGO AS ZID_CARGO, C.ZDESCRIPCION AS ZCARGO,";
            //Miquery += " A.ZPROVINCIA AS ZID_PROVINCIA, D.ZDESCRIPCION AS ZPROVINCIA, ";
            //Miquery += " A.ZMUNICIPIO AS ZID_MUNICIPIO, E.ZDESCRIPCION AS ZMUNICIPIO, ";
            //Miquery += "  A.ZNACIONALIDAD AS ZID_NACIONALIDAD, F.ZDESCRIPCION AS ZNACIONALIDAD,  ";
            //Miquery += "  A.ZESTADO AS ZID_ESTADO, J.ZDESCRIPCION AS ZESTADO,";
            //Miquery += "  A.ZTELEFONO, ";
            //Miquery += " A.ZDEPARTAMENTO as ZID_DEPARTAMENTO, G.ZDESCRIPCION AS ZDEPARTAMENTO, ";
            //Miquery += " A.ZDESTINO AS ZID_DESTINO, H.ZDESCRIPCION AS ZDESTINO, ";
            //Miquery += " A.ZALIAS , A.ZIDENTIFICACION , to_char(A.ZFECHA_ALTA,'dd/mm/yyyy') AS ZFECHA_ALTA , to_char(A.ZFECHA_BAJA,'dd/mm/yyyy') AS ZFECHA_BAJA, to_char(A.ZFECHA_STOP,'dd/mm/yyyy') AS ZFECHA_STOP ";
            //Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZNIVEL B ON A.ZNIVEL = B.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZCARGOS C ON A.ZCARGO = C.ZID";
            //Miquery += " INNER JOIN  USER_GEDESPOL.ZPROVINCIAS D  ON A.ZPROVINCIA = D.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZMUNICIPIOS E ON A.ZMUNICIPIO = E.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZPAISES F ON A.ZNACIONALIDAD = F.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZDEPARTAMENTOS G ON A.ZDEPARTAMENTO = G.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZDESTINOS H ON A.ZDESTINO = H.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZESTADOS J ON A.ZESTADO = J.ZID";
            //Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZUSUARIOS M ON M.ZID = A.ZRESPONSABLE";
            //Miquery += "  WHERE A.ZNIVEL <= " + IDNivel;
            //Miquery += " ORDER BY A.ZID ";

            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZNOMBRE, A.ZAPELLIDO1, A.ZAPELLIDO2, A.ZDESCRIPCION, A.ZPASSWORD, A.ZDESCRIPCION AS ZCARGO, ";
            Miquery += " 1 AS ZID_NIVEL, ";
            Miquery += " A.ZPUESTO_TRABAJO AS ZNIVEL, 1 AS ZID_RESPONSABLE, A.SU_RESPONSABLE_SUPERIOR_ES AS ZRESPONSABLE, ";
            Miquery += " A.PLANTILLA as ZDOMICILIO, ";
            Miquery += " 1 AS ZID_CARGO, A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " 1 AS ZID_PROVINCIA, A.PROVINCIA AS ZPROVINCIA, ";
            Miquery += " 1 AS ZID_MUNICIPIO, A.ZCENTRO_DIRECTIVO AS ZMUNICIPIO, ";
            Miquery += " 1 AS ZID_NACIONALIDAD, A.ZCENTRO_DIRECTIVO AS ZNACIONALIDAD,  ";
            Miquery += " 1 AS ZID_ESTADO, 'Activo' AS ZESTADO, ";
            Miquery += " '22211111' as ZTELEFONO, ";
            Miquery += " 1 as ZID_DEPARTAMENTO, A.PLANTILLA AS ZDEPARTAMENTO, ";
            Miquery += " 1 AS ZID_DESTINO, A.PLANTILLA AS ZDESTINO, ";
            Miquery += " A.ZALIAS , A.ZDNI AS ZIDENTIFICACION , to_char(A.ZFECHA_ALTA, 'dd/mm/yyyy') AS ZFECHA_ALTA, ";
            Miquery += " to_char(A.ZFECHA_BAJA, 'dd/mm/yyyy') AS ZFECHA_BAJA, to_char(A.ZFECHA_STOP, 'dd/mm/yyyy') AS ZFECHA_STOP ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM ZUSUARIOS A ";
            }

            Miquery += "  WHERE A.ZNIVEL <= " + IDNivel;
            Miquery += " ORDER BY A.ZID ";

            //Consulta con Query
            DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;

        }

        public static DataSet CargaSCANform()
        {


            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                string Miquery = " SELECT ZID, ZDESCRIPCION, ZSECUENCIAS, ZID_PROCEDIMIENTO, ZTITULO FROM ZFORMULARIOS ";

                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
                if (Miro.Contains("DESARROLLO") == true)
                {
                    context.Session["DESARROLLO"] = "DESARROLLO";
                }

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                //string Miquery = " SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZNIVEL WHERE ZID <=" + IDNivel;
                //Miquery += " ORDER BY ZID ";

                ////Consulta con Query
                //DataSet ds = null;
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaSecuencia()
        {
            //Consulta con Procedimiento almacenado
            //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_USUARIOS", cn);
            //comando.CommandType = CommandType.StoredProcedure;

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                string Miquery = " SELECT ZID, ZID_ARCHIVO, ZTIPO, ZDESCRIPCION, ZSECUENCIA, ZMASCARA, ZMANUAL FROM ZSECUENCIAS ";
                Miquery += " ORDER BY ZID ";

                string Miro = ConfigurationManager.AppSettings.Get("connectionSQL");
                if(Miro.Contains("DESARROLLO") == true)
                {
                    context.Session["DESARROLLO"] = "DESARROLLO";
                }

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                //string Miquery = " SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZNIVEL WHERE ZID <=" + IDNivel;
                //Miquery += " ORDER BY ZID ";

                ////Consulta con Query
                //DataSet ds = null;
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaCategorias()
        {
            //Consulta con Procedimiento almacenado
            //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_USUARIOS", cn);
            //comando.CommandType = CommandType.StoredProcedure;

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                string Miquery = " SELECT ZID, ZDESCRIPCION FROM ZCATEGORIAS "; // WHERE ZID <=" + IDNivel;
                Miquery += " ORDER BY ZID ";

                //Consulta con Query

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                //string Miquery = " SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZNIVEL WHERE ZID <=" + IDNivel;
                //Miquery += " ORDER BY ZID ";

                ////Consulta con Query
                //DataSet ds = null;
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaNivel()
        {
            //Consulta con Procedimiento almacenado
            //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_USUARIOS", cn);
            //comando.CommandType = CommandType.StoredProcedure;

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                string Miquery = " SELECT ZID, ZDESCRIPCION FROM ZNIVELES WHERE ZID <=" + IDNivel;
                Miquery += " ORDER BY ZID ";

                //Consulta con Query
                
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));

                //string Miquery = " SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZNIVEL WHERE ZID <=" + IDNivel;
                //Miquery += " ORDER BY ZID ";

                ////Consulta con Query
                //DataSet ds = null;
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }
        //public static DataSet CargaNivel()
        //{
        //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
        //    OracleCommand comando;
        //    OracleDataAdapter Da;
        //    DataSet MyDataSet = new DataSet();

        //    comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_NIVELES", cn);
        //    comando.CommandType = CommandType.StoredProcedure;

        //    // Abrimos conexión
        //    if (cn.State == 0) cn.Open();

        //    OracleCommandBuilder.DeriveParameters(comando);

        //    // Le pasamos los parametros (si existe cursor para el primero se pasa un null
        //    comando.Parameters[0].Value = 0; // Nombre
        //    //comando.Parameters[1].Value = null;

        //    // Ejecutamos y llenamo Ds
        //    Da = new OracleDataAdapter(comando);
        //    Da.Fill(MyDataSet);

        //    //Cerrar conex
        //    cn.Close();

        //    return MyDataSet;
        //}

        
        public static DataSet TablasValidacion()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO  FROM ZARCHIVOS WHERE ZTIPO = 4 ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS WHERE ZTIPO = 2 ORDER BY ZID ";
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


        public static DataSet CargaCargos()
        {
            DataSet MyDataSet = new DataSet();
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));

                SqlCommand comando;
                SqlDataAdapter Da;
                

                comando = new SqlCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_CARGOS", cn);
                comando.CommandType = CommandType.StoredProcedure;

                // Abrimos conexión
                if (cn.State == 0) cn.Open();

                SqlCommandBuilder.DeriveParameters(comando);

                // Le pasamos los parametros (si existe cursor para el primero se pasa un null
                comando.Parameters[0].Value = 0; // Nombre
                                                 //comando.Parameters[1].Value = null;

                // Ejecutamos y llenamo Ds
                Da = new SqlDataAdapter(comando);
                Da.Fill(MyDataSet);

                //Cerrar conex
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));

                //OracleCommand comando;
                //OracleDataAdapter Da;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_CARGOS", cn);
                //comando.CommandType = CommandType.StoredProcedure;

                //// Abrimos conexión
                //if (cn.State == 0) cn.Open();

                //OracleCommandBuilder.DeriveParameters(comando);

                //// Le pasamos los parametros (si existe cursor para el primero se pasa un null
                //comando.Parameters[0].Value = 0; // Nombre
                //                                 //comando.Parameters[1].Value = null;

                //// Ejecutamos y llenamo Ds
                //Da = new OracleDataAdapter(comando);
                //Da.Fill(MyDataSet);

                ////Cerrar conex
                //cn.Close();
            }

            return MyDataSet;

        }

        public static DataSet CargaArchivoCampos()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZIDARCHIVO,  ZIDCAMPO  FROM ZARCHIVOCAMPOS ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM USER_GEDESPOL.ZFCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaFormatoArchivo()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM ZTIPOARCHIVO ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM USER_GEDESPOL.ZFCAMPOS ORDER BY ZID ";
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
        public static DataSet CargaNiveles()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM ZNIVELES ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM USER_GEDESPOL.ZFCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaSecuencias()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID,  ZDESCRIPCION, ZTIPO, ZSECUENCIA, ZMASCARA  FROM ZSECUENCIAS ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM USER_GEDESPOL.ZFCAMPOS ORDER BY ZID ";
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
        public static DataSet CargaFormatoCampos()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT * FROM ZTIPOCAMPO ORDER BY ZID "; //ZID,  ZDESCRIPCION
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID,  ZDESCRIPCION  FROM USER_GEDESPOL.ZFCAMPOS ORDER BY ZID ";
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

        public static DataSet BuscaLoteReco(string SQL)
        {

            DataSet ds = null;

            try
            {
                //DataSet ds = null;
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];

                    //SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionSQLReco"].ConnectionString);
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLReco"));
                    //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                    SqlCommand comando;
                    HttpContext context = HttpContext.Current;
                    DataSet MyDataSet = new DataSet();
                    string Miquery = SQL;
                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;

                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    cn.Close();
                    //string a = Main.Ficherotraza("BuscaLoteReco -->" + SQL);

                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    //OracleCommand comando;
                    //DataSet MyDataSet = new DataSet();
                    //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
                    //comando = new OracleCommand(Miquery, cn);
                    //comando.CommandType = CommandType.Text;

                    //if (cn.State == 0) cn.Open();
                    //OracleDataAdapter Da = new OracleDataAdapter(comando);
                    //ds = new DataSet();
                    //Da.Fill(ds);
                    ////ListUser.Add(ds);
                    //cn.Close();
                }
            }
             catch (Exception Ex)
            {
                //crear mensaje
                string a = Main.Ficherotraza("BuscaLoteReco --> " + Ex.Message + "==> " + SQL);
                Variables.mensajeserver = Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL");
                throw new Exception("Error de base de datos.", Ex);
            }

            return ds;

        }

        public static DataSet BuscaConexionBBDD(string SQL)
        {
            DataSet ds = null;
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];

                string miro = ConfigurationManager.AppSettings.Get("connectionSQLGold").ToString();
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                SqlCommand comando;
                HttpContext context = HttpContext.Current;
                DataSet MyDataSet = new DataSet();
                string Miquery = SQL;
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        //public static DataSet BuscaLoteReco(string SQL)
        //{

        //    DataSet ds = null;


        //    //DataSet ds = null;
        //    if (Variables.configuracionDB == 0)
        //    {
        //        SqlParameter[] dbParams = new SqlParameter[0];

        //        string miro = ConfigurationManager.AppSettings.Get("connectionSQLGold").ToString();
        //        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
        //        //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
        //        SqlCommand comando;
        //        HttpContext context = HttpContext.Current;
        //        DataSet MyDataSet = new DataSet();
        //        string Miquery = SQL;
        //        comando = new SqlCommand(Miquery, cn);
        //        comando.CommandType = CommandType.Text;

        //        if (cn.State == 0) cn.Open();
        //        SqlDataAdapter Da = new SqlDataAdapter(comando);
        //        ds = new DataSet();
        //        Da.Fill(ds);
        //        //ListUser.Add(ds);
        //        cn.Close();
        //    }
        //    else if (Variables.configuracionDB == 1)
        //    {
        //        //OracleParameter[] dbParams = new OracleParameter[0];
        //        //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
        //        //OracleCommand comando;
        //        //DataSet MyDataSet = new DataSet();
        //        //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static DataSet BuscaLoteGold(string SQL)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            context.Session["Procedimiento"] = "BuscaLoteGold";
            try
            {
                //DataSet ds = null;
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];

                    string miro = ConfigurationManager.AppSettings.Get("connectionSQLGold").ToString();
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                    //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                    SqlCommand comando;
                    //HttpContext context = HttpContext.Current;
                    DataSet MyDataSet = new DataSet();
                    string Miquery = SQL;
                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;
                    string a = ETrazas(SQL, "0", " BuscaLoteGold -->" + context.Session["Procedimiento"].ToString());
                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    ds = new DataSet();
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    cn.Close();
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    //OracleCommand comando;
                    //DataSet MyDataSet = new DataSet();
                    //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
                    //comando = new OracleCommand(Miquery, cn);
                    //comando.CommandType = CommandType.Text;

                    //if (cn.State == 0) cn.Open();
                    //OracleDataAdapter Da = new OracleDataAdapter(comando);
                    //ds = new DataSet();
                    //Da.Fill(ds);
                    ////ListUser.Add(ds);
                    //cn.Close();
                }
            }
            catch (Exception Ex)
            {
                //crear mensaje
                Variables.Error = Ex.Message;
                string a = ETrazas(SQL, "1", " BuscaLoteGold -->" + Ex.Message + " --> " + context.Session["Procedimiento"].ToString());
                //string a = Main.Ficherotraza("BuscaLoteGold --> " + Ex.Message + "==> " + SQL);
                Variables.mensajeserver = "BuscaLoteGold --> " + Ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL");
                throw new Exception("Error de base de datos.", Ex);
            }

            return ds;

        }




        public static DataSet BuscaBBDDConexion(int Conexion, string Tabla)
        {
            string SQL = " SELECT ";
            SQL += " so.name AS Tabla, ";
            SQL += " sc.name AS Columna, ";
            SQL += " st.name AS Tipo, ";
            SQL += " sc.max_length AS Tamaño, ";
            SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN 'varchar' ";
            SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN 'numeric' ";
            SQL += " WHEN st.name = 'decimal' THEN 'decimal' ";
            SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN 'Fecha'  END AS RELACION, ";
            SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN '1' ";
            SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN '2' ";
            SQL += " WHEN st.name = 'decimal' THEN '5' ";
            SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN '4'  END AS VALORRELACION, ";
            SQL += " CASE WHEN CONVERT(VARCHAR(255), sc.max_length) = '-1' OR CONVERT(VARCHAR(255),sc.max_length) = '8000' THEN 'MAX' ELSE CONVERT(VARCHAR(255),sc.max_length) END VALORCOLUMNA ";
            SQL += " FROM ";
            SQL += " sys.objects so INNER JOIN ";
            SQL += " sys.columns sc ON ";
            SQL += " so.object_id = sc.object_id INNER JOIN ";
            SQL += " sys.types st ON ";
            SQL += " st.system_type_id = sc.system_type_id ";
            SQL += " AND st.name != 'sysname' ";
            SQL += " WHERE so.type = 'U' ";
            if(Tabla != "")
            {
                SQL += " AND so.name = '"+ Tabla + "' ";
            }
            SQL += " ORDER BY so.name, sc.name ";

            DataSet ds = null;
            string connectionString = "";
            HttpContext context = HttpContext.Current;
            context.Session["Procedimiento"] = "BuscaLote";
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                try
                {
                    string Sqlinst = " SELECT * FROM ZCONEXION ORDER BY ZID ";
                    System.Data.DataTable dt = Main.BuscaLote(Sqlinst).Tables[0];
                    foreach (DataRow fila in dt.Rows)
                    {
                        if (Conexion.ToString() == fila["ZID"].ToString())
                        {
                            connectionString = fila["ZCONECTSTRING"].ToString();
                            context.Session["Conexion"] = connectionString;
                            break;
                        }
                    }

                    SqlParameter[] dbParams = new SqlParameter[0];

                    //string miro = ConfigurationManager.AppSettings.Get("connectionSQLGold").ToString();
                    //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                    SqlConnection cn = new SqlConnection(connectionString);
                    SqlCommand comando;

                    DataSet MyDataSet = new DataSet();
                    string Miquery = SQL;
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
                    //Trazas
                    //string a = Main.Ficherotraza("BuscaLoteReco -->" + SQL);

                }
                catch (Exception ex)
                {
                    Variables.Error = " " + SQL + " " + ex.Message;
                    string a = ETrazas(SQL, "1", " BuscaLoteConexion --> ID Conexion: " + Conexion + ", ConectString:" + connectionString);
                    //string a = Main.Ficherotraza("BuscaLote --> " + ex.Message + "==> " + SQL);
                    Variables.mensajeserver = ex.Message + ". Conexión: " + connectionString;
                    throw new Exception("Error de base de datos.", ex);

                }
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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


        public static DataSet BuscaLoteConexion(string SQL, int Conexion)
        {

            DataSet ds = null;
            string connectionString ="";
            HttpContext context = HttpContext.Current;
            context.Session["Procedimiento"] = "BuscaLote";
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                try
                {
                    string Sqlinst = " SELECT * FROM ZCONEXION ORDER BY ZID ";
                    System.Data.DataTable dt = Main.BuscaLote(Sqlinst).Tables[0];
                    foreach (DataRow fila in dt.Rows)
                    {
                        if (Conexion.ToString() == fila["ZID"].ToString())
                        {
                            connectionString = fila["ZCONECTSTRING"].ToString();
                            break;
                        }
                    }

                    SqlParameter[] dbParams = new SqlParameter[0];

                    //string miro = ConfigurationManager.AppSettings.Get("connectionSQLGold").ToString();
                    //SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                    SqlConnection cn = new SqlConnection(connectionString);
                    SqlCommand comando;

                    DataSet MyDataSet = new DataSet();
                    string Miquery = SQL;
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
                    //Trazas
                    //string a = Main.Ficherotraza("BuscaLoteReco -->" + SQL);

                }
                catch (Exception ex)
                {
                    Variables.Error = " " + SQL + " " + ex.Message;
                    string a = ETrazas(SQL, "1", " BuscaLoteConexion --> ID Conexion: " + Conexion + ", ConectString:" + connectionString);
                    //string a = Main.Ficherotraza("BuscaLote --> " + ex.Message + "==> " + SQL);
                    Variables.mensajeserver = ex.Message + ". Conexión: " + connectionString;
                    throw new Exception("Error de base de datos.", ex);

                }
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static DataSet BuscaLote(string SQL)
        {
            
            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            context.Session["Procedimiento"] = "BuscaLote";
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                try
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                    SqlCommand comando;

                    DataSet MyDataSet = new DataSet();
                    string Miquery = SQL;
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
                    //Trazas
                    //string a = Main.Ficherotraza("BuscaLoteReco -->" + SQL);

                }
                catch (Exception ex)
                {
                    Variables.Error = " " + SQL + " " + ex.Message;
                    string a = ETrazas(SQL, "1", " BuscaLote -->" + context.Session["Procedimiento"].ToString());
                    //string a = Main.Ficherotraza("BuscaLote --> " + ex.Message + "==> " + SQL);
                    Variables.mensajeserver = ex.Message + "." + ConfigurationManager.AppSettings.Get("connectionSQL");
                    //throw new Exception("Error de base de datos.", ex);
                    
                }
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static string TrazaProceso(string Data, string sortExpression = null)
        {

                string path = HttpContext.Current.Server.MapPath("/logs") + "/" + Data;
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        if (sortExpression != null)
                        {
                            tw.WriteLine(Data);
                        }
                        else
                        {
                            tw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " = " + Data);
                        }
                    }
                }
                else if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        if (sortExpression != null)
                        {
                            tw.WriteLine(Data);
                        }
                        else
                        {
                            tw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " = " + Data);
                        }
                    }
                }
            return path;
        }
        public static string Ficherotraza(string Data, string sortExpression = null)
        {
            if(Data.Contains("Subproceso anulado") == false)
            {
                string path = HttpContext.Current.Server.MapPath("/logs") + "/" + ConfigurationManager.AppSettings.Get("FileLog");
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (TextWriter tw = new StreamWriter(path))
                    {
                        if(sortExpression != null)
                        {
                            tw.WriteLine(Data);
                        }
                        else
                        {
                            tw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " = " + Data);
                        }
                    }
                }
                else if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        if (sortExpression != null)
                        {
                            tw.WriteLine(Data);
                        }
                        else
                        {
                            tw.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " = " + Data);
                        }
                    }
                }
            }
            return Data;
        }

        public static object ExecuteScalar(string sqlSpName)
        {
            SqlParameter[] dbParams1 = new SqlParameter[0];
            SqlConnection cn1 = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
            //SqlConnection cn1 = new SqlConnection(Connecta);
            SqlCommand cmd1 = new SqlCommand(sqlSpName, cn1);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandType = CommandType.Text;
            object retVal = null;

            if (dbParams1 != null)
            {
                foreach (SqlParameter dbParam in dbParams1)
                {
                    cmd1.Parameters.Add(dbParam);
                }
            }
            cn1.Open();
            try
            {
                retVal = cmd1.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn1)
                    cn1.Close();
            }

            return retVal;
        }

        public static string BuscaTablasReco(int Modo, string StrFechas, string StrZonaCultivo, string StrEnvase, string StrVariedad, string StrEmpleados, string StrOrden, string Condicion)
        {
            string Datos = "";
            //empleado, nombre, apellido, centro, categoria, vivienda, envase, variedad, zona
            string FiltroDiaMEs = "";

            string[] MiSeleccion = System.Text.RegularExpressions.Regex.Split(Condicion, "-");
            if (StrFechas == "")
            {              
                if (Modo == 2)
                {
                    StrFechas = " where A.Fecha between(select DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)) and select DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE()) - 1, -1) ";
                }
                else
                {
                    StrFechas = " and Fecha between (select DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)) and select DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1) ";
                }
            }
            else
            {
                FiltroDiaMEs = " and Fecha between " + StrFechas + "  ";

                if (Modo == 2)
                {
                    StrFechas = " where A.Fecha between " + StrFechas + " ";
                }
                else
                {
                    StrFechas = " and Fecha between " + StrFechas + "  ";
                }
            }

            if (StrZonaCultivo == "")
            {
                if (Modo == 1) { StrZonaCultivo = " and A.Zona not in ('S1', 'S2', 'S3', 'S4') "; }
                if (Modo == 2) { StrZonaCultivo = " and A.NombreTablet not in ('T6', 'T4') "; }
                if (Modo == 3) { StrZonaCultivo = " and A.Zona not like('S%')  ";  }
            }
            else
            {
                if (StrZonaCultivo.Contains("'")){}
                else
                {
                    StrZonaCultivo = StrZonaCultivo.Replace(",", "','");
                }
                if (Modo == 1) 
                {
                    if (MiSeleccion[8] == "0") { StrZonaCultivo = " and A.Zona in ('" + StrZonaCultivo + "') "; }
                    if (MiSeleccion[8] == "1") { StrZonaCultivo = " and A.Zona not in ('" + StrZonaCultivo + "') "; }
                    if (MiSeleccion[8] == "2") { StrZonaCultivo = " and A.Zona like ('" + StrZonaCultivo + "') "; }
                }
                if (Modo == 2) 
                {
                    if (MiSeleccion[8] == "0") { StrZonaCultivo = " and A.NombreTablet in ('" + StrZonaCultivo + "') "; }
                    if (MiSeleccion[8] == "1") { StrZonaCultivo = " and A.NombreTablet not in ('" + StrZonaCultivo + "') "; }
                    if (MiSeleccion[8] == "2") { StrZonaCultivo = " and A.NombreTablet like ('" + StrZonaCultivo + "') "; }
                }
                if (Modo == 3) { StrZonaCultivo = " and A.Zona not like('" + StrZonaCultivo + "')  "; }
            }

            if (StrEnvase == "")
            {
                StrEnvase = " and A.envase not in ('Z0') ";
            }
            else
            {
                if (StrEnvase.Contains("'")) { }
                else
                {
                    StrEnvase = StrEnvase.Replace(",", "','");
                }
                if (MiSeleccion[6] == "0") { StrEnvase = " and A.envase in ('" + StrEnvase + "') "; }
                if (MiSeleccion[6] == "1") { StrEnvase = " and A.envase not in ('" + StrEnvase + "') "; }
                if (MiSeleccion[6] == "2") { StrEnvase = " and A.envase like ('" + StrEnvase + "') "; }

            }
            if (StrEmpleados == "")
            {                
                //StrEmpleados = " and A.Empleado not in ('0020999', '20208', '100008', '100011', '100015', '100029', '100329')  ";         
            }
            else
            {
                if (StrEmpleados.Contains("'")) { }
                else
                {
                    StrEmpleados = StrEmpleados.Replace(",", "','");
                }
                if(Modo == 2)
                {
                    if (MiSeleccion[0] == "0") { StrEmpleados = " and A.Empleado in ('" + StrEmpleados + "') "; }
                    if (MiSeleccion[0] == "1") { StrEmpleados = " and A.Empleado not in ('" + StrEmpleados + "') "; }
                    if (MiSeleccion[0] == "2") { StrEmpleados = " and A.Empleado like ('" + StrEmpleados + "') "; }
                }
                else
                {
                    if (MiSeleccion[0] == "0") { StrEmpleados = " and Empleado in ('" + StrEmpleados + "') "; }
                    if (MiSeleccion[0] == "1") { StrEmpleados = " and Empleado not in ('" + StrEmpleados + "') "; }
                    if (MiSeleccion[0] == "2") { StrEmpleados = " and Empleado like ('" + StrEmpleados + "') "; }
                }
            }
            if (StrVariedad == "")
            {
                StrVariedad = " and A.Variedad like('T%') "; 
            }
            else
            {
                if (StrVariedad.Contains("'")) { }
                else
                {
                    StrVariedad = StrVariedad.Replace(",", "','");
                }
                if (MiSeleccion[0] == "0") { StrVariedad = " and A.Variedad in ('" + StrVariedad + "') "; }
                if (MiSeleccion[0] == "1") { StrVariedad = " and A.Variedad not in ('" + StrVariedad + "') "; }
                if (MiSeleccion[0] == "2") { StrVariedad = " and A.Variedad like ('" + StrVariedad + "') "; }
                //StrVariedad = " and A.Variedad like('" + StrVariedad + "') ";
            }
            if (StrOrden == "")
            {
                if (Modo == 1) { StrOrden = " order by A.Empleado, F.HoraIni, A.fecha, A.hora " ; } //" order by A.Empleado, A.fecha, A.hora "
                if (Modo == 2) { StrOrden = " order by A.Empleado, A.fecha, A.horaini "; }
                if (Modo == 3) { StrOrden = " order by A.Empleado, A.fecha, A.hora  "; }
            }
            else
            {
                StrOrden = " order by " + StrOrden + " ";
            }

            if (Modo == 1)
            {
                ////SELECT  * from .[REC_TAREAS]
                Datos = " select  ";
                Datos += " Empleado AS COD_EMPLEADO,  ";
                Datos += " [dbo].[EMPLEADOS].Nombre AS NOMBRE,  ";
                Datos += " [dbo].[EMPLEADOS].Apellidos AS APELLIDOS,  ";
                Datos += " Fecha AS FECHA_EMPLEADOS,  ";
                Datos += " Hora as HORA_EMPLEADO, ";
                Datos += " [NombreTablet]  AS TABLET,  ";
                Datos += " [Zona].codFinca AS CODFINCA,  ";
                Datos += " [Fincas2].Nombre as DESCRFINCA, ";
                Datos += " Zona AS ZONA,  ";
                Datos += " [Zona].Descripcion as DESCRZONAZ,   ";
                Datos += " Variedad AS TAREA,  ";
                Datos += " [Tarea].Descripcion as DESCRTAREA,   ";

                Datos += " Envase AS INIFIN,  ";
                Datos += " [Envases].Descripcion as DESCRINIFIN  ";

                //Datos += " from[dbo].[ApuntesRecogida], [dbo].[EMPLEADOS], [dbo].[Tareas] as [Tarea], [dbo].[Tareas] as [Zona], [Fincas2]  ";

                Datos += " from [Recodat].[dbo].[ApuntesRecogida], [Recodat].[dbo].[EMPLEADOS], [Recodat].[dbo].[Tareas] as [Tarea], [Recodat].[dbo].[Tareas] as [Zona], [Recodat].[dbo].[Fincas2], [dbo].[Tareas] as [Envases]  ";


                Datos += " where Empleado = CodEmpleado  ";
                Datos += " and[Fincas2].codFinca = [Zona].codFinca  ";
                Datos += " and Variedad =[Tarea].CodTarea  ";
                Datos += " and Zona =[Zona].CodTarea  ";

                Datos += " and Envase =[Envases].CodTarea  ";

                Datos += StrFechas;
                //Datos += " and Fecha >= '01/09/2021'  ";
                //Datos += " and Fecha <= '30/09/2021'  ";
                //Datos += " and Zona not in ('S1', 'S2', 'S3', 'S4')  ";
                //Datos += " and envase not in ('Z0') ";
                Datos += " and codCategoria <> 'FI' ";
                Datos += " and Empleado not in (0020999, 20208, 100008, 100011, 100015, 100029, 100329)   ";
                //Solicitud Angeles 22-12-2021
                if(StrEmpleados != "")
                {
                    Datos += StrEmpleados;
                }
                //08/11/2022 Restriccion Angeles
                Datos += " and Variedad like('T%')  ";
                Datos += " order by Empleado, fecha, hora  ";
                //Datos += StrFechas;
                //Datos += StrZonaCultivo;
                //Datos += StrEnvase;
                //Datos += StrEmpleados;
                //Datos += StrVariedad;
                //Datos += StrOrden;

                //Datos += " order by Empleado, fecha, hora  ";


                //Datos = " select A.Empleado AS  COD_EMPLEADO,  ";
                //Datos += " B.Nombre AS NOMBRE,  ";
                //Datos += " B.Apellidos AS APELLIDOS,  ";
                //Datos += " A.Fecha AS FECHA_EMPLEADOS,  ";
                //Datos += " A.Hora as HORA_EMPLEADO,  ";
                //Datos += " A.NombreTablet AS TABLET,  ";
                //Datos += " D.codFinca AS CODFINCA,  ";
                //Datos += " E.Nombre as DESCRFINCA, ";
                //Datos += " A.Zona AS ZONA,  ";
                //Datos += " D.Descripcion as DESCRZONAZ,  A.Variedad AS TAREA, C.Descripcion as DESCRTAREA, ";
                //Datos += " F.Fecha AS FECHA_JORNADA,  ";
                //Datos += " F.HoraIni AS HORAINI,  ";
                //Datos += " F.HoraFin AS HORAFIN,  ";
                //Datos += " CONVERT(CHAR(8), DATEADD(s, DATEDIFF(s, F.HoraIni, F.HoraFin), '1900-1-1'), 8) AS TRANSCURRIDO, ";
                //Datos += " F.NombreTablet AS RECOTABLET ";
                //Datos += " from ApuntesRecogida A ";
                //Datos += " INNER JOIN EMPLEADOS B ON  A.Empleado = B.CodEmpleado ";
                //Datos += " INNER JOIN Tareas C ON A.Variedad = C.CodTarea ";
                //Datos += " INNER JOIN Tareas D ON A.Zona = D.CodTarea ";
                //Datos += " INNER JOIN Fincas2 E ON E.codFinca = D.codFinca ";
                //Datos += " INNER JOIN ApuntesJornada F ON F.Empleado = B.CodEmpleado ";
                ////Datos += " where A.Fecha between '17/05/2021' and '31/05/2021' ";
                ////Datos += "  and A.Zona not in ('S1', 'S2', 'S3', 'S4') /*no incluimos zonas de Cartaya*/ ";
                ////Datos += " and A.envase not in ('Z0') /*quito las horas fin*/ ";
                ////Datos += " and A.Empleado not in (0020999, 20208, 100008, 100011, 100015, 100029, 100329) /*Empleados oficina y  prueba*/ ";
                ////Datos += " and A.Variedad like('T%') ";
                ////Datos += " and F.NombreTablet not in ('T6', 'T4')  /*Tablets de Cartaya*/ ";
                ////Datos += " and A.NombreTablet = F.NombreTablet ";
                ////Datos += " and A.Fecha = F.Fecha ";
                ////Datos += " order by A.Empleado, F.HoraIni, A.fecha, A.hora--738 ";




                ////Datos = " Select A.Empleado, B.Nombre, B.Apellidos, A.Fecha, A.Hora as FechaHora, ";
                ////Datos += " A.NombreTablet, D.codFinca, E.Nombre as FincaDescr,A.Zona, D.Descripcion as ZonaDescr,  A.Variedad as Tarea, ";
                ////Datos += " C.Descripcion as TareaDescr ";
                ////Datos += " from ApuntesRecogida A ";
                ////Datos += " INNER JOIN EMPLEADOS B ON  A.Empleado = B.CodEmpleado ";
                ////Datos += " INNER JOIN Tareas C ON A.Variedad = C.CodTarea ";
                ////Datos += " INNER JOIN Tareas D ON A.Zona = D.CodTarea ";
                ////Datos += " INNER JOIN Fincas2 E ON E.codFinca = D.codFinca ";

                //Datos += StrFechas;
                //Datos += StrZonaCultivo ;
                //Datos += StrEnvase;
                //Datos += StrEmpleados;
                //Datos +=StrVariedad;
                //Datos += " and A.NombreTablet = F.NombreTablet ";
                //Datos += " and A.Fecha = F.Fecha ";
                //Datos += StrOrden;

                ////Datos += " where A.Fecha between '17/05/2021' and '31/05/2021' ";
                ////Datos += " and A.Zona not in ('S1', 'S2', 'S3', 'S4') ";
                ////Datos += " and A.envase not in ('Z0') ";
                ////Datos += " and A.Empleado not in (0020999, 20208, 100008, 100011, 100015, 100029, 100329) ";
                ////Datos += " and A.Variedad like('T%') ";
                ////Datos += " order by A.Empleado, A.fecha, A.hora ";
            }
            else if (Modo == 2)
            {
                //SELECT from [REC_JORNADA]
                Datos = " select A.Empleado AS  COD_EMPLEADO, B.Nombre AS NOMBRE, B.Apellidos AS APELLIDOS, A.Fecha AS FECHA_JORNADA, A.HoraFin AS HORAFIN, A.NombreTablet AS RECOTABLET, A.HoraIni AS HORAINI, ";
                Datos += " CONVERT(CHAR(8), DATEADD(s, DATEDIFF(s, A.HoraIni, A.HoraFin), '1900-1-1'), 8) AS TRANSCURRIDO, ";
                //Datos += " (SELECT SUM(Importe) FROM Anticipos WHERE A.Empleado = CodEmpleado AND CodConcepto = 'VD' " + FiltroDiaMEs + " )  as DIAS_DESCUENTO ";
                Datos += " (SELECT COALESCE(SUM(Importe), 0) FROM Anticipos WHERE A.Empleado = CodEmpleado AND CodConcepto = 'VD' " + FiltroDiaMEs + " )  as DIAS_DESCUENTO ";

                Datos += " from ApuntesJornada A ";
                Datos += " INNER JOIN EMPLEADOS B ON A.Empleado = B.CodEmpleado ";

                //Datos += " Where A.Fecha >= '01/09/2021'  ";
                //Datos += " and A.Fecha <= '30/09/2021'  ";
                Datos += StrFechas;
                Datos += " and codCategoria<> 'FI' ";

                //Datos += StrZonaCultivo;
                //Datos += StrEmpleados;
                //Datos += StrOrden;

                //Datos += " where A.Fecha between '17/05/2021' and '31/05/2021' ";
                //Datos += " and A.NombreTablet not in ('T6', 'T4')  /*Tablets de Cartaya*/ ";
                Datos += " and A.Empleado not in (0020999, 20208, 100008, 100011, 100015, 100029, 100329) ";
                //Solicitud Angeles 22-12-2021
                if (StrEmpleados != "")
                {
                    Datos += StrEmpleados;
                }
                Datos += " order by A.Empleado, A.fecha, A.horaini";
            }
            else if (Modo == 3)
            {
                // SELECT FROM REC_PRODUCCION
                //Datos = " select A.Empleado AS  COD_EMPLEADO, B.Nombre AS NOMBRE, B.Apellidos AS APELLIDOS, A.Fecha AS FECHA_EMPLEADOS,  ";
                //Datos += " A.Hora as HORA_EMPLEADO, A.NombreTablet AS TABLET, D.codFinca AS CODFINCA, F.Nombre as DESCRFINCA,  ";
                //Datos += " A.Zona AS ZONA, D.Descripcion as DESCRZONAZ, A.Variedad AS TAREA, C.Descripcion as DESCRTAREA, A.Envase AS ENVASE,  ";
                //Datos += " E.Descripcion as DESCRENVASE, A.Envasesmarcaje as MARCAENVASE, A.Kg as PLANTAS  ";
                //Datos += " from ApuntesRecogida A ";
                //Datos += " INNER JOIN EMPLEADOS B ON A.Empleado = B.CodEmpleado ";
                //Datos += " INNER JOIN Tareas C ON A.Variedad = C.CodTarea ";
                //Datos += " INNER JOIN Tareas D ON A.Zona = D.CodTarea ";
                //Datos += " INNER JOIN Tareas E ON A.Envase = E.CodTarea ";
                //Datos += " INNER JOIN Fincas2 F ON F.codFinca = D.codFinca ";


                Datos = " Select Empleado as COD_EMPLEADO, [dbo].[EMPLEADOS].Nombre AS NOMBRE, [dbo].[EMPLEADOS].Apellidos AS APELLIDOS, Fecha AS FECHA_EMPLEADOS, Hora as HORA_EMPLEADO,[NombreTablet] AS TABLET,  ";
                Datos += " [Zona].codFinca AS CODFINCA, [Fincas2].Nombre as DESCRFINCA, ";
                Datos += " Zona AS ZONA, [Zona].Descripcion as DESCRZONAZ,   ";
                Datos += " Variedad AS TAREA, [Variedad].Descripcion as DESCRTAREA,  ";
                Datos += " Envase AS ENVASE, [Envases].Descripcion as DESCRENVASE, [ApuntesRecogida].Envasesmarcaje as MARCAENVASE, [ApuntesRecogida].Kg as PLANTAS ";
                Datos += " from[dbo].[ApuntesRecogida], [dbo].[EMPLEADOS], [dbo].[Tareas] as [Variedad], [dbo].[Tareas] as [Zona], [dbo].[Tareas] as [Envases], [Fincas2] ";
                Datos += " where Empleado = CodEmpleado ";
                Datos += " and[Fincas2].codFinca = [Zona].codFinca ";
                Datos += " and Variedad =[Variedad].CodTarea ";
                Datos += " and Zona =[Zona].CodTarea ";
                Datos += " and Envase =[Envases].CodTarea ";
                //Datos += " and FECHA = CONVERT(DATE, GETDATE() - 1) ";
                //Datos += " and Fecha between '01/09/2021' and '30/09/2021' ";
                Datos += StrFechas;
                //Datos += " and Fecha >= '01/09/2021'  ";
                //Datos += " and Fecha <= '30/09/2021'  ";
                //Datos += " and Zona not like('S%')  ";
                //Datos += " and envase not in ('Z0')  ";
                Datos += " and [EMPLEADOS].codCategoria <> 'FI' ";
                Datos += " and Empleado not in ('0020999', '0020998', '0020997', '20208', '100008', '100011', '100015', '100029', '100329') ";
                //Solicitud Angeles 22-12-2021
                if (StrEmpleados != "")
                {
                    Datos += StrEmpleados;
                }
                Datos += " and (Variedad like 'V%' OR Variedad like 'U%' OR Variedad like 'Y%' OR Variedad like 'F%' OR Variedad like 'X%' OR Variedad like '0%') ";
                Datos += " order by Empleado, fecha, hora ";

                //Datos += StrFechas;
                //Datos += StrZonaCultivo;
                //Datos += StrEnvase;
                //Datos += StrEmpleados;
                //Datos += StrVariedad;
                //Datos += StrOrden;

                //Datos += " where A.Fecha between '23/07/2021' and '23/07/2021' ";
                //Datos += " and A.Zona not like('S%') /*no incluimos zonas de Cartaya*/ ";
                //Datos += " and A.envase not in ('Z0') /*quito las horas fin*/ ";
                //Datos += " and A.Empleado not in ('0020999', '0020998', '0020997', '20208', '100008', '100011', '100015', '100029', '100329')  ";
                //Datos += " and A.Variedad like('V%') ";
                //Datos += " order by A.Empleado, A.fecha, A.hora ";
            }
            else if (Modo == 4)
            {
                // SELECT FROM REC_PRODUCCION-T-V
                Datos = " Select Empleado as COD_EMPLEADO, [dbo].[EMPLEADOS].Nombre AS NOMBRE, [dbo].[EMPLEADOS].Apellidos AS APELLIDOS, Fecha AS FECHA_EMPLEADOS, Hora as HORA_EMPLEADO,[NombreTablet] AS TABLET,  ";
                Datos += " [Zona].codFinca AS CODFINCA, [Fincas2].Nombre as DESCRFINCA, ";
                Datos += " Zona AS ZONA, [Zona].Descripcion as DESCRZONAZ,   ";
                Datos += " Variedad AS TAREA, [Variedad].Descripcion as DESCRTAREA,  ";
                Datos += " Envase AS ENVASE, [Envases].Descripcion as DESCRENVASE, [ApuntesRecogida].Envasesmarcaje as MARCAENVASE, [ApuntesRecogida].Kg as PLANTAS ";
                Datos += " from[dbo].[ApuntesRecogida], [dbo].[EMPLEADOS], [dbo].[Tareas] as [Variedad], [dbo].[Tareas] as [Zona], [dbo].[Tareas] as [Envases], [Fincas2] ";
                Datos += " where Empleado = CodEmpleado ";
                Datos += " and[Fincas2].codFinca = [Zona].codFinca ";
                Datos += " and Variedad =[Variedad].CodTarea ";
                Datos += " and Zona =[Zona].CodTarea ";
                Datos += " and Envase =[Envases].CodTarea ";
                Datos += StrFechas;
                Datos += " and(Variedad like 'T%') ";
                Datos += " and Empleado not in ('0020999', '0020998', '0020997', '20208', '100008', '100011', '100015', '100029', '100329') ";
                //Solicitud Angeles 22-12-2021
                if (StrEmpleados != "")
                {
                    Datos += StrEmpleados;
                }
                Datos += " AND Envase in ('85', '86') ";
                Datos += " and NombreTablet like 'M%' ";
                Datos += " order by Empleado, fecha, hora ";
            }

            return Datos;
        }

        public static string PTrazas(string Equipo, string Nivel, string Procedimiento)
        {
            string a = "";
            HttpContext context = HttpContext.Current;
            try
            {
                a = Main.TrazaProceso(context.Session["ComputerName"].ToString());
            }
            catch (Exception ex)
            {
                string b = ex.Message;
                a = Main.Ficherotraza(context.Session["MiNivel"].ToString() + " Error en el Proceso Automatico - " + context.Session["ComputerName"].ToString() + " - " + context.Session["UserAlias"].ToString());
            }
            return a;
        }

        public static string ETrazas(string SQL, string Nivel, string Procedimiento)
        {
            string a = "";
            HttpContext context = HttpContext.Current;
            try
            {
                if (Procedimiento == null)
                {
                    Procedimiento = "";
                }

                //if (Session["Username"] != null)
                //{
                //    string username = (string)Session["Username"];
                //}


                if (context.Session["TrazaLog"].ToString() == null)
                {
                    context.Session["TrazaLog"] = "";
                }
                if (context.Session["MiNivel"].ToString() == null)
                {
                    context.Session["MiNivel"] = "";
                }
                if (context.Session["ComputerName"].ToString() == null)
                {
                    context.Session["ComputerName"] = "";
                }
                if (context.Session["UserAlias"].ToString() == null)
                {
                    context.Session["UserAlias"] = "";
                }

                if (context.Session["TrazaLog"].ToString() == "2" || (context.Session["TrazaLog"].ToString() == "1" && context.Session["MiNivel"].ToString() != "0"))
                {
                    a = Main.Ficherotraza(context.Session["MiNivel"].ToString() + " - " + context.Session["ComputerName"].ToString() + " - " + context.Session["UserAlias"].ToString() + " - " + Procedimiento + " --> " + SQL);
                }
            }
            catch 
            {
                //try
                //{
                    //string b = ex.Message;
                    //a = Main.Ficherotraza(context.Session["MiNivel"].ToString() + " - " + context.Session["ComputerName"].ToString() + " - " + context.Session["UserAlias"].ToString() + " - " + Procedimiento + " --> " + SQL + " <<<<< TrazaLog: " + context.Session["TrazaLog"].ToString() + " >>>>> Error: " + b);
                //}
                //catch 
                //{ 
                //}
            }
            return a;
        }


        public static string BuscaVentasGold(string StrEmpresa, string StrFiltro)
        {
            string Datos = "";

            string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
            string rBBDDa = ""; // "NET_PRVR" + rANUAL;
            string rBBDDb = ""; // "NET_PRVV" + rANUAL;

            string SQL = "SELECT DISTINCT(ZTIPO_PLANTA) FROM ZFAMILIA_PLANTAS ";
            System.Data.DataTable dtFiltro = Main.BuscaLote(SQL).Tables[0];
            string CampoIn = "";
            foreach (DataRow fila in dtFiltro.Rows)
            {
                if(CampoIn == "")
                {
                    CampoIn = "'" + fila["ZTIPO_PLANTA"].ToString() + "'";
                }
                else
                {
                    CampoIn += ",'" + fila["ZTIPO_PLANTA"].ToString() + "'";
                }
            }
            
            SQL = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA WHERE ACTIVO = 1 ";
            System.Data.DataTable dtV = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dtV.Rows)
            {
                rANUAL = fila["ZANO"].ToString();
                rBBDDa = fila["DBVRE"].ToString() + rANUAL;
                rBBDDb = fila["DBVIVA"].ToString() + rANUAL;
                break;
            }

            //SQL = " SELECT ID, EMPRESA, CLIENTEPROVEEDOR, NOMBREFISCAL, NUMERO, LINEA, PRODUCTO, UDSPEDIDAS,";
            //SQL += "UDSSERVIDAS, UDSENCARGA, UDSPENDIENTES, UDSACARGAR, NUMPALET, FECHAENTREGA, ESTADO, RUTA, DESCRIPCION, SERIE_PED, TIPO_PLANTA, VARIEDAD, FAMILIA ";
            //SQL += " FROM ZPEDIDOS_COMPRA  ";


            //--condicion especial si empresa tiene algo
            if (StrEmpresa != "")
            {
                if (StrEmpresa.Contains("PRVR"))
                {
                    Datos = " Select 'VRE' AS EMPRESA, C.[Cliente Proveedor] as CLIENTEPROVEEDOR, C.[Nombre Fiscal] as NOMBREFISCAL, C.[Fecha Entrega] as FECHAENTREGA, ";
                    Datos += " C.Serie as SERIE_PED, C.Numero as NUMERO, L.Línea as LINEA, L.Producto as PRODUCTO, L.Descripcion as DESCRIPCION,";
                    Datos += " L.[Uds Pedidas] as UDSPEDIDAS, L.[Uds Servidas] as UDSSERVIDAS, ([Uds Pedidas] -[Uds Servidas]) as UDSPENDIENTES, A.[Familia] as FAMILIA  ";
                    Datos += " FROM [" + rBBDDa + "].[dbo].[Cabecera Facturacion Compras] C, [" + rBBDDa + "].[dbo].[Lineas Facturacion Compras] L, [" + rBBDDa + "].[dbo].[Articulos] A ";
                    Datos += " WHERE L.tipo = 'P' ";
                    Datos += " AND C.tipo = L.tipo ";
                    Datos += " AND C.Serie = L.Serie ";
                    Datos += " AND C.Numero = L.Numero ";
                    Datos += " AND C.Realizado = 0 ";
                    Datos += " AND L.Producto = A.Codigo ";
                    Datos += " AND A.Familia in(" + CampoIn + ") ";
                    Datos += " AND L.[Uds Servidas] < L.[Uds Pedidas] " + StrFiltro;
                }

                //IF SUBSTRING(@EMPRESA,1, 4) = 'PRVV'
                if (StrEmpresa.Contains("PRVV"))
                {
                    Datos = " Select 'VIVA' AS EMPRESA, C.[Cliente Proveedor] as CLIENTEPROVEEDOR, C.[Nombre Fiscal] as NOMBREFISCAL, C.[Fecha Entrega] as FECHAENTREGA, ";
                    Datos += " C.Serie as SERIE_PED, C.Numero as NUMERO, L.Línea as LINEA, L.Producto as PRODUCTO, L.Descripcion as DESCRIPCION,";
                    Datos += " L.[Uds Pedidas] as UDSPEDIDAS, L.[Uds Servidas] as UDSSERVIDAS, ([Uds Pedidas] -[Uds Servidas]) as UDSPENDIENTES, A.[Familia] as FAMILIA  ";
                    Datos += " FROM [" + rBBDDb + "].[dbo].[Cabecera Facturacion Compras] C, [" + rBBDDb + "].[dbo].[Lineas Facturacion Compras] L, [" + rBBDDb + "].[dbo].[Articulos] A ";
                    Datos += " WHERE L.tipo = 'P' ";
                    Datos += " AND C.tipo = L.tipo ";
                    Datos += " AND C.Serie = L.Serie ";
                    Datos += " AND C.Numero = L.Numero ";
                    Datos += " AND C.Realizado = 0 ";
                    Datos += " AND L.Producto = A.Codigo ";
                    Datos += " AND A.Familia in(" + CampoIn + ") ";
                    Datos += " AND L.[Uds Servidas] < L.[Uds Pedidas] " + StrFiltro;
                }
            }
            else
            {
                //condicion sin empresa. Resto de condiciones o sin condicione
                Datos = " Select 'VRE' AS EMPRESA, C.[Cliente Proveedor] as CLIENTEPROVEEDOR, C.[Nombre Fiscal] as NOMBREFISCAL, C.[Fecha Entrega] as FECHAENTREGA, ";
                Datos += " C.Serie as SERIE_PED, C.Numero as NUMERO, L.Línea as LINEA, L.Producto as PRODUCTO, L.Descripcion as DESCRIPCION,";
                Datos += " L.[Uds Pedidas] as UDSPEDIDAS, L.[Uds Servidas] as UDSSERVIDAS, ([Uds Pedidas] -[Uds Servidas]) as UDSPENDIENTES, A.[Familia] as FAMILIA  ";
                Datos += " FROM [" + rBBDDa + "].[dbo].[Cabecera Facturacion Compras] C, [" + rBBDDa + "].[dbo].[Lineas Facturacion Compras] L, [" + rBBDDa + "].[dbo].[Articulos] A ";
                Datos += " WHERE L.tipo = 'P' ";
                Datos += " AND C.tipo = L.tipo ";
                Datos += " AND C.Serie = L.Serie ";
                Datos += " AND C.Numero = L.Numero ";
                Datos += " AND C.Realizado = 0 ";
                Datos += " AND L.Producto = A.Codigo ";
                Datos += " AND A.Familia in(" + CampoIn + ") ";
                Datos += " AND L.[Uds Servidas] < L.[Uds Pedidas] " + StrFiltro;
                Datos += " UNION ALL ";
                Datos += " Select 'VIVA' AS EMPRESA, C.[Cliente Proveedor] as CLIENTEPROVEEDOR, C.[Nombre Fiscal] as NOMBREFISCAL, C.[Fecha Entrega] as FECHAENTREGA, ";
                Datos += " C.Serie as SERIE_PED, C.Numero as NUMERO, L.Línea as LINEA, L.Producto as PRODUCTO, L.Descripcion as DESCRIPCION,";
                Datos += " L.[Uds Pedidas] as UDSPEDIDAS, L.[Uds Servidas] as UDSSERVIDAS, ([Uds Pedidas] -[Uds Servidas]) as UDSPENDIENTES, A.[Familia] as FAMILIA  ";
                Datos += " FROM [" + rBBDDb + "].[dbo].[Cabecera Facturacion Compras] C, [" + rBBDDb + "].[dbo].[Lineas Facturacion Compras] L, [" + rBBDDb + "].[dbo].[Articulos] A ";
                Datos += " WHERE L.tipo = 'P' ";
                Datos += " AND C.tipo = L.tipo ";
                Datos += " AND C.Serie = L.Serie ";
                Datos += " AND C.Numero = L.Numero ";
                Datos += " AND C.Realizado = 0 ";
                Datos += " AND L.Producto = A.Codigo ";
                Datos += " AND A.Familia in(" + CampoIn + ") ";
                Datos += " AND L.[Uds Servidas] < L.[Uds Pedidas] " + StrFiltro;
            }
            return Datos;
        }

        public static string BuscaGold(string StrEmpresa, string StrFiltro)
        {
            // Identifica el numero de almacen desde la tabla  [Lineas Facturacion] Golden en la 80
            string Datos = "";

            string rANUAL = ""; // Convert.ToString(Main.ExecuteScalar("SELECT MIN(ZANO) FROM ANO_AGRICOLA"));
            string rBBDDa = ""; // "NET_PRVR" + rANUAL;
            string rBBDDb = ""; // "NET_PRVV" + rANUAL;


            string SQL = "SELECT TOP 1 ZANO, DBVRE, DBVIVA FROM ANO_AGRICOLA WHERE ACTIVO = 1";
            System.Data.DataTable dtV = Main.BuscaLote(SQL).Tables[0];
            foreach (DataRow fila in dtV.Rows)
            {
                rANUAL = fila["ZANO"].ToString();
                rBBDDa = fila["DBVRE"].ToString() + rANUAL;
                rBBDDb = fila["DBVIVA"].ToString() + rANUAL;
                break;
            }


            //--condicion especial si empresa tiene algo
            if (StrEmpresa != "")
            {
                if (StrEmpresa.Contains("PRVR"))
                {
                    Datos = " Select  '" + StrEmpresa + "'  AS EMPRESA, ";
                    Datos += " A.[Cliente Proveedor] AS CLIENTEPROVEEDOR, ";
                    Datos += " A.[Nombre Fiscal] AS NOMBREFISCAL, ";
                    Datos += " A.Numero AS NUMERO, ";
                    Datos += " B.Linea AS LINEA, ";
                    Datos += " B.Producto AS ARTICULO, ";
                    Datos += " B.[Uds Pedidas] AS UDSPEDIDAS, ";
                    Datos += " B.[Uds Servidas] AS UDSSERVIDAS, ";
                    Datos += " 0 AS UDSENCARGA, ";
                    Datos += " 0 AS UDSPENDIENTES, ";
                    Datos += " 0 AS UDSACARGAR, ";
                    Datos += " 0 AS NUMPALET, ";
                    Datos += " A.[Ruta Envio] AS RUTA, ";
                    Datos += " B.Libre6 AS FECHAENTREGA, ";
                    Datos += " 0 AS ESTADO, ";
                    Datos += " '' AS COMPUTER, ";
                    Datos += " A.[Serie] AS SERIE_PED ";   
                    Datos += " From [" + rBBDDa + "].[dbo].[Cabecera Facturacion] A ";
                    Datos += " INNER JOIN [" + rBBDDa + "].[dbo].[Lineas Facturacion] B ";
                    Datos += " ON A.Serie = B.Serie AND A.Numero = B.Numero AND A.Tipo = B.Tipo ";
                    Datos += " INNER JOIN [" + rBBDDa + "].[dbo].[Artículos] C ";
                    Datos += " ON B.[Producto] = C.[Codigo] ";
                    //Datos += " WHERE not exists (SELECT NUMERO FROM [DESARROLLO].[dbo].ZCARGA_ORDEN where NUMERO = A.Numero) ";
                    //Datos += " AND B.Tipo = 'P' ";
                    Datos += " WHERE B.Tipo = 'P'  ";
                    Datos += " AND A.Realizado = '0' ";
                    Datos += " AND C.[Familia] like '%0000%' ";
                    Datos += " AND B.[Uds Pedidas] > B.[Uds Servidas] ";
                    Datos += " AND ISNUMERIC(B.Almacen) > 0 " + StrFiltro;
                }

                //IF SUBSTRING(@EMPRESA,1, 4) = 'PRVV'
                if (StrEmpresa.Contains("PRVV"))
                {
                    Datos = " Select  '" + StrEmpresa + "'  AS EMPRESA, ";
                    Datos += " A.[Cliente Proveedor] AS CLIENTEPROVEEDOR, ";
                    Datos += " A.[Nombre Fiscal] AS NOMBREFISCAL, ";
                    Datos += " A.Numero AS NUMERO, ";
                    Datos += " B.Linea AS LINEA, ";
                    Datos += " B.Producto AS ARTICULO, ";
                    Datos += " B.[Uds Pedidas] AS UDSPEDIDAS, ";
                    Datos += " B.[Uds Servidas] AS UDSSERVIDAS, ";
                    Datos += " 0 AS UDSENCARGA, ";
                    Datos += " 0 AS UDSPENDIENTES, ";
                    Datos += " 0 AS UDSACARGAR, ";
                    Datos += " 0 AS NUMPALET, ";
                    Datos += " A.[Ruta Envio] AS RUTA,";
                    Datos += " B.Libre6 AS FECHAENTREGA, ";
                    Datos += " 0 AS ESTADO, ";
                    Datos += " '' AS COMPUTER, ";
                    Datos += " A.[Serie] AS SERIE_PED ";
                    Datos += " From [" + rBBDDb + "].[dbo].[Cabecera Facturacion] A ";
                    Datos += " INNER JOIN [" + rBBDDb + "].[dbo].[Lineas Facturacion] B ON A.Serie = B.Serie ";
                    Datos += " AND A.Numero = B.Numero ";
                    Datos += " AND A.Tipo = B.Tipo ";
                    Datos += " INNER JOIN [" + rBBDDb + "].[dbo].[Artículos] C ";
                    Datos += " ON B.[Producto] = C.[Codigo] ";
                    //Datos += " WHERE not exists (SELECT NUMERO FROM [DESARROLLO].[dbo].ZCARGA_ORDEN where NUMERO = A.Numero) ";
                    //Datos += " AND B.Tipo = 'P' ";
                    Datos += " WHERE B.Tipo = 'P'  ";
                    Datos += " AND A.Realizado = '0' ";
                    Datos += " AND C.[Familia] like '%0000%' ";
                    Datos += " AND B.[Uds Pedidas] > B.[Uds Servidas] ";
                    Datos += " AND ISNUMERIC(B.Almacen) > 0 " + StrFiltro;
                }
            }
            else
            {
                //condicion sin empresa. Resto de condiciones o sin condicione
                Datos = " Select  (Select [EmpresaCuentas] FROM [" + rBBDDa + "].[dbo].[Parámetros])  AS EMPRESA, ";
                Datos += " A.[Cliente Proveedor] AS CLIENTEPROVEEDOR, ";
                Datos += " A.[Nombre Fiscal] AS NOMBREFISCAL, ";
                Datos += " A.Numero AS NUMERO, ";
                Datos += " B.Linea AS LINEA, ";
                Datos += " B.Producto AS ARTICULO, ";
                Datos += " B.[Uds Pedidas] AS UDSPEDIDAS,";
                Datos += " B.[Uds Servidas] AS UDSSERVIDAS, ";
                Datos += " 0 AS UDSENCARGA, ";
                Datos += " 0 AS UDSPENDIENTES, ";
                Datos += " 0 AS UDSACARGAR, ";
                Datos += " 0 AS NUMPALET, ";
                Datos += " A.[Ruta Envio] AS RUTA, ";
                Datos += " B.Libre6 AS FECHAENTREGA,  ";
                Datos += " 0 AS ESTADO, ";
                Datos += " '' AS COMPUTER, ";
                Datos += " A.[Serie] AS SERIE_PED ";
                Datos += " From [" + rBBDDa + "].[dbo].[Cabecera Facturacion] A ";
                Datos += " INNER JOIN [" + rBBDDa + "].[dbo].[Lineas Facturacion] B ";
                Datos += " ON A.Serie = B.Serie AND A.Numero = B.Numero AND A.Tipo = B.Tipo ";
                Datos += " INNER JOIN [" + rBBDDa + "].[dbo].[Artículos] C ";
                Datos += " ON B.[Producto] = C.[Codigo] ";
                //Datos += " WHERE not exists (SELECT NUMERO FROM [DESARROLLO].[dbo].ZCARGA_ORDEN where NUMERO = A.Numero) ";
                //Datos += " AND B.Tipo = 'P'  ";
                Datos += " WHERE B.Tipo = 'P'  ";
                Datos += " AND A.Realizado =  '0'  ";
                Datos += " AND C.[Familia] like '%0000%'  ";
                Datos += " AND B.[Uds Pedidas] > B.[Uds Servidas] ";
                Datos += " AND ISNUMERIC(B.Almacen) > 0 " + StrFiltro; 
                Datos += " UNION ALL ";
                Datos += " Select ";
                Datos += " (Select[EmpresaCuentas] FROM[" + rBBDDb + "].[dbo].[Parámetros])  AS EMPRESA, ";
                Datos += " A.[Cliente Proveedor] AS CLIENTEPROVEEDOR, ";
                Datos += " A.[Nombre Fiscal] AS NOMBREFISCAL, ";
                Datos += " A.Numero AS NUMERO, ";
                Datos += " B.Linea AS LINEA, ";
                Datos += " B.Producto AS ARTICULO, ";
                Datos += " B.[Uds Pedidas] AS UDSPEDIDAS,";
                Datos += " B.[Uds Servidas] AS UDSSERVIDAS, ";
                Datos += " 0 AS UDSENCARGA, ";
                Datos += " 0 AS UDSPENDIENTES, ";
                Datos += " 0 AS UDSACARGAR, ";
                Datos += " 0 AS NUMPALET, ";
                Datos += " A.[Ruta Envio] AS RUTA, ";
                Datos += " B.Libre6 AS FECHAENTREGA, ";
                Datos += " 0 AS ESTADO, ";
                Datos += " '' AS COMPUTER, ";
                Datos += " A.[Serie] AS SERIE_PED ";
                Datos += " From [" + rBBDDb + "].[dbo].[Cabecera Facturacion] A ";
                Datos += " INNER JOIN [" + rBBDDb + "].[dbo].[Lineas Facturacion] B ON A.Serie = B.Serie ";
                Datos += " AND A.Numero = B.Numero ";
                Datos += " AND A.Tipo = B.Tipo ";
                Datos += " INNER JOIN [" + rBBDDb + "].[dbo].[Artículos] C ";
                Datos += " ON B.[Producto] = C.[Codigo] ";
                //Datos += " WHERE not exists (SELECT NUMERO FROM [DESARROLLO].[dbo].ZCARGA_ORDEN where NUMERO = A.Numero) ";
                //Datos += " AND B.Tipo = 'P'  ";
                Datos += " WHERE B.Tipo = 'P'  ";
                Datos += " AND A.Realizado =  '0'  ";
                Datos += " AND C.[Familia] like '%0000%'  ";
                Datos += " AND B.[Uds Pedidas] > B.[Uds Servidas] " ;
                Datos += " AND ISNUMERIC(B.Almacen) > 0 " + StrFiltro;
            }
            return Datos;
        }


        public static DataSet BuscaNumeroVRE(string SQL)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLVRE"));
                SqlCommand comando;
                HttpContext context = HttpContext.Current;
                DataSet MyDataSet = new DataSet();
                string Miquery = SQL;
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static DataSet CargaVolumen(string ID, string IdArchivo)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                HttpContext context = HttpContext.Current;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT ZID, ZID_DOMINIO, ZID_ARCHIVO, ZNOMBRE, ZRUTA, ZFECHA_CREATE, ZFECHA_MOD ,ZSIZE, ZACTIVO ";
                Miquery += "FROM ZVOLUMENES WHERE ZACTIVO = 1 AND ZID_ARCHIVO = " + IdArchivo; // AND ZID = " + ID + "

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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static DataSet CargaArchivos(int TipoOrden = 0)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                HttpContext context = HttpContext.Current;
                DataSet MyDataSet = new DataSet();
                string Miquery = "";
                if (context.Session["MiNivel"].ToString() == "9")
                {
                    if (TipoOrden == 0) //ID
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ORDER BY ZID ";
                    }
                    else if (TipoOrden == 1) //Descripcion
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ORDER BY ZDESCRIPCION ";
                    }
                    else if (TipoOrden == 2)//Tabla Validacion
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ";
                        Miquery += " AND ZTIPO = 4 ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 3)//objetos
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " AND ZTABLEOBJ <> '' AND ZESTADO <> 3  ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 4)//objetos-usuarios
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE ZTABLEOBJ <> '' ";
                        Miquery += " AND ZID = " + context.Session["idarchivo"] + " AND ZESTADO <> 3 ";
                        Miquery += " ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 5)//Flujos-Archivos
                    {
                        //Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        //Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        //Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        //Miquery += " FROM ZARCHIVOS A, ZARCHIVOFLUJOS B "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        //Miquery += " WHERE A.ZTABLEOBJ <> '' ";
                        //Miquery += " AND A.ZID = B.ZID_ARCHIVO  ";
                        //Miquery += " AND B.ZID_FLUJO =  " + context.Session["idflujo"] + " ";
                        //Miquery += " ORDER BY A.ZTABLENAME ";

                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A, ZARCHIVOFLUJOS B "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  A.ZID = B.ZID_ARCHIVO  ";
                        Miquery += " AND B.ZID_FLUJO =  " + context.Session["idflujo"] + " AND ZESTADO <> 3 ";
                        Miquery += " ORDER BY A.ZTABLENAME ";
                    }
                    else if (TipoOrden == 6)//Flujos-Archivos-Archivosflujo
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A, ZARCHIVOFLUJOS B "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  A.ZID = B.ZID_ARCHIVO  ";
                        Miquery += " AND B.ZID_FLUJO =  " + context.Session["idflujo"] + " AND ZESTADO <> 3 ";
                        Miquery += " ORDER BY A.ZTABLENAME ";
                    }
                    else if (TipoOrden == 7)//Usuarios-Grupos
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZUSUARIOS','ZGRUPOS') ";//,'ZROLES') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                    else if (TipoOrden == 8)//Usuarios-Grupos
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZUSUARIOS','ZGRUPOS','ZROLES') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                    else if (TipoOrden == 9)//conexiones
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZCONEXION') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                }
                else
                {
                    if (TipoOrden == 0) //ID
                    {
                        Miquery = " SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += " ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS  ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ORDER BY ZID ";
                    }
                    else if (TipoOrden == 1) //Descripcion
                    {
                        Miquery = " SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += " ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS  ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ORDER BY ZDESCRIPCION ";
                    }
                    else if (TipoOrden == 2)//Tabla Validacion
                    {
                        Miquery = " SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += " ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS  ";
                        //Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ORDER BY ZTABLENAME ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " AND ZESTADO <> 3 ";
                        Miquery += " AND ZTIPO = 4 ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 3)//objetos
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " AND ZTABLEOBJ <> '' AND ZESTADO <> 3 ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 4)//objetos-usuarios
                    {
                        Miquery = "SELECT ZID, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ";
                        Miquery += " CASE WHEN ZTABLENAME = '' OR ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE ZTABLENAME END ZTABLENAME, ";
                        Miquery += "ZTABLEOBJ, ZTIPO, ZESTADO, ZCONEXION, ZID_VOLUMEN, ZVIEW, ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE ZTABLEOBJ <> '' ";
                        Miquery += " AND ZID = " + context.Session["idarchivo"] + " AND ZESTADO <> 3 ";
                        Miquery += " ORDER BY ZTABLENAME ";
                    }
                    else if (TipoOrden == 5)//Flujos-Archivos
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A, ZARCHIVOFLUJOS B "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE A.ZTABLEOBJ <> '' ";
                        Miquery += " AND A.ZID = B.ZID_ARCHIVO  ";
                        Miquery += " AND B.ZID_FLUJO =  " + context.Session["idflujo"] + " AND ZESTADO <> 3 ";
                        Miquery += " ORDER BY A.ZTABLENAME ";
                    }
                    else if (TipoOrden == 7)//Usuarios-Grupos
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZUSUARIOS','ZGRUPOS','ZROLES') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                    else if (TipoOrden == 8)//Usuarios-Grupos
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZUSUARIOS','ZGRUPOS','ZROLES') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                    else if (TipoOrden == 9)//conexiones
                    {
                        Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZNIVEL, A.ZROOT, A.ZKEY, ";
                        Miquery += " CASE WHEN A.ZTABLENAME = '' OR A.ZTABLENAME = NULL  THEN 'Sin Tabla' ELSE A.ZTABLENAME END ZTABLENAME, ";
                        Miquery += " A.ZTABLEOBJ, A.ZTIPO, A.ZESTADO, A.ZCONEXION, A.ZID_VOLUMEN, A.ZVIEW, A.ZDUPLICADOS ";
                        Miquery += " FROM ZARCHIVOS A "; // WHERE ZNIVEL <= " + context.Session["MiNivel"] + " ";
                        Miquery += " WHERE  ZESTADO <> 3 AND A.ZTABLENAME in ('ZCONEXION') ";
                        Miquery += " ORDER BY A.ZID ";
                    }
                }

                //Ojo antes orden con ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZTIPO, ZESTADO, ZID_DOMINIO  FROM USER_GEDESPOL.ZARCHIVOS ORDER BY ZID ";
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

        public static DataSet CargaFlujos(string Flujo, string Archivo)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;


                string Miquery = " SELECT  DISTINCT(A.ZID) AS ZID,  A.ZDESCRIPCION, A.ZID_ARCHIVO, A.ZID_REGISTRO, A.ZID_ESTADOS ";
                Miquery += " FROM ZFLUJOS A, ZARCHIVOFLUJOS B  ";
                if (Archivo != "0")
                {
                    Miquery += " WHERE A.ZID = B.ZID_FLUJO ";
                    Miquery += " AND B.ZID_ARCHIVO = " + Archivo;
                    Miquery += " ORDER BY A.ZDESCRIPCION ";
                }
                else
                {
                    if (Flujo == "0")
                    {
                        Miquery += " ORDER BY A.ZDESCRIPCION ";
                    }
                    else
                    {
                        Miquery += " WHERE  A.ZID = B.ZID_FLUJO ";
                        Miquery += " AND A.ZID IN (" + Flujo + ") ";
                        Miquery += " ORDER BY A.ZDESCRIPCION ";
                    }
                }

               
                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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


        public static DataSet CargaEstadosFl(int Flujo)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;

                //Modificación del 17/09/2024
                //Contempla grupo de estados en función del Flujo seleccionado
                string Miquery = " SELECT C.ZNOMBRE, C.ZID as ZID_ESTADO, C.ZID, C.ZDESCRIPCION, C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND ";
                Miquery += " FROM ZESTADOSFLUJO C, ZFLUJOSESTADOS A ";
                if (Flujo == 0)
                {
                    Miquery += "  WHERE A.ZID_ESTADO = C.ZID ";
                }
                else
                {
                    Miquery += " WHERE A.ZID_FLUJO =  " + Flujo;
                    Miquery += " AND A.ZID_ESTADO = C.ZID ";
                }
                
                Miquery += " ORDER BY C.ZID";



                //string Miquery = " SELECT C.ZNOMBRE, C.ZID as ZID_ESTADO, C.ZID, C.ZDESCRIPCION, C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND ";
                //Miquery += " FROM ZESTADOSFLUJO C ";
                //if(Flujo == 0)
                //{
                //}
                //else
                //{
                //    Miquery += " WHERE C.ZID =  " + Flujo;
                //}
                //Miquery += " ORDER BY C.ZID";
             
                //OJO antes orden desde ZID ahora ZORDEN

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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaSoloFlujos(int Flujo)
        {
            DataSet ds = null;
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;

                string Miquery = " SELECT ZID,  ZDESCRIPCION, ZID_ARCHIVO, ZID_REGISTRO, ZID_ESTADOS  ";
                Miquery += " FROM ZFLUJOS ";
                if (Flujo == 0)
                {
                    Miquery += " ORDER BY ZID";
                }
                else
                {
                    Miquery += " WHERE ZID = " + Flujo;
                    Miquery += " ORDER BY ZID";
                }
                //Miquery += " GROUP BY A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, ";
                //Miquery += " C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,  ";
                //Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL";
                //Miquery += " ORDER BY C.ZORDEN";




                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaFlujosEstados(int Flujo)
        {
            DataSet ds = null;
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;

                string Miquery = " SELECT A.ZID_FLUJO AS ZID, A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,  ";
                Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL ";
                Miquery += " FROM ZFLUJOSESTADOS A, ZFLUJOS B, ZESTADOSFLUJO C ";
                Miquery += " WHERE A.ZID_FLUJO =  B.ZID ";
                Miquery += " AND A.ZID_ESTADO =  C.ZID ";


                //string Miquery = " SELECT A.ZID_FLUJO AS ZID, A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, ";
                //Miquery += " C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,   ";
                //Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL";
                //Miquery += " FROM ZFLUJOSESTADOS A, ZFLUJOS B, ZESTADOSFLUJO C, ZARCHIVOFLUJOS D";
                //Miquery += " WHERE A.ZID_FLUJO = B.ZID";
                //Miquery += " AND A.ZID_ESTADO = C.ZID";
                //Miquery += " AND A.ZID_FLUJO = D.ZID_FLUJO";
                if (Flujo == 0)
                {
                    Miquery += " ORDER BY C.ZORDEN";
                }
                else
                {
                    Miquery += " AND B.ZID = " + Flujo;
                    Miquery += " ORDER BY C.ZORDEN";
                }
                //Miquery += " GROUP BY A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, ";
                //Miquery += " C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,  ";
                //Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL";
                //Miquery += " ORDER BY C.ZORDEN";


                
                
                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaMenus(int IdMenu)
        {
            DataSet ds = null;
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;

                string Miquery = "SELECT ZID, ZTITULO, ZDESCRIPCION, ZPAGINA, ZROOT, ZSUBMENU, (SELECT COUNT(*) FROM ZMENU WHERE ZROOT = AA.ZID) AS zHIJOS ";
                Miquery += "FROM ZMENU AA WHERE  ZESTADO <> 3 ";
                if(IdMenu != 0)
                {
                    Miquery += " AND AA.ZID = " + IdMenu;
                }
                Miquery += " ORDER BY ZID, ZROOT";



                //string Miquery = " SELECT A.ZID_FLUJO AS ZID, A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, ";
                //Miquery += " C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,   ";
                //Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL";
                //Miquery += " FROM ZFLUJOSESTADOS A, ZFLUJOS B, ZESTADOSFLUJO C, ZARCHIVOFLUJOS D";
                //Miquery += " WHERE A.ZID_FLUJO = B.ZID";
                //Miquery += " AND A.ZID_ESTADO = C.ZID";
                //Miquery += " AND A.ZID_FLUJO = D.ZID_FLUJO";

                //Miquery += " GROUP BY A.ZID_FLUJO,  B.ZDESCRIPCION, A.ZID_ESTADO, C.ZNOMBRE, ";
                //Miquery += " C.ZORDEN, C.ZPASO, C.ZPREVIUS, C.ZNEXT, C.ZALTERNATIVE, C.ZEND, A.ZPREVIUSVIEW,  ";
                //Miquery += " A.ZNEXTVIEW, A.ZALTERNATIVEVIEW, A.ZENDVIEW, A.ZCONDICION, A.ZNIVEL";
                //Miquery += " ORDER BY C.ZORDEN";




                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet BuscaEstado(int Estado)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;


                //string Miquery = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION, A.ZNIVEL, A.ZVALUE, A.ZORDEN, A.ZACTIVO,  A.ZPASO, A.ZID_DOMINIO, "; 
                //Miquery += " A.ZPREVIUS, A.ZNEXT, A.ZALTERNATIVE, A.ZEND  ";
                //Miquery += " FROM ZESTADOSFLUJO A ";

                string Miquery = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION, A.ZNIVEL, A.ZVALUE, A.ZORDEN, A.ZACTIVO,  A.ZPASO, A.ZID_DOMINIO,  A.ZPREVIUS, A.ZIMG,  ";
                Miquery += " A.ZNEXT, A.ZALTERNATIVE, A.ZEND, A.ZIMG, B.ZPREVIUSVIEW, B.ZNEXTVIEW, B.ZALTERNATIVEVIEW, B.ZENDVIEW, B.ZCONDICION ";
                Miquery += " FROM ZESTADOSFLUJO A, ZFLUJOSESTADOS B ";
                Miquery += " WHERE A.ZID = B.ZID_ESTADO ";
                //Miquery += " ORDER BY  A.ZDESCRIPCION ";


                ////Miquery += " WHERE A.ZTIPO = B.ZID ";
                if (Estado == 0)
                {

                    Miquery += " ORDER BY  A.ZID ";
                }
                else
                {
                    Miquery += " AND A.ZID = " + Estado;
                    //Miquery += " WHERE A.ZID = " + Flujo;
                    Miquery += " ORDER BY  A.ZID ";
                }

                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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


        public static DataSet CargaEstadosFlujos(string Flujo, string Estado)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;


                //string Miquery = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION, A.ZNIVEL, A.ZVALUE, A.ZORDEN, A.ZACTIVO,  A.ZPASO, A.ZID_DOMINIO, "; 
                //Miquery += " A.ZPREVIUS, A.ZNEXT, A.ZALTERNATIVE, A.ZEND  ";
                //Miquery += " FROM ZESTADOSFLUJO A ";

                string Miquery = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION, A.ZNIVEL, A.ZVALUE, A.ZORDEN, A.ZACTIVO,  A.ZPASO, A.ZID_DOMINIO,  A.ZPREVIUS, A.ZIMG,  ";
                Miquery += " A.ZNEXT, A.ZALTERNATIVE, A.ZEND, A.ZIMG, B.ZPREVIUSVIEW, B.ZNEXTVIEW, B.ZALTERNATIVEVIEW, B.ZENDVIEW, B.ZCONDICION, B.ZEJECUCION  ";
                Miquery += " FROM ZESTADOSFLUJO A, ZFLUJOSESTADOS B ";
                Miquery += " WHERE A.ZID = B.ZID_ESTADO ";

                if (context.Session["MiNivel"].ToString() == "0")
                {
                    Miquery += " AND B.ZORDEN > 1 ";
                }

                //Miquery += " ORDER BY  A.ZDESCRIPCION ";


                ////Miquery += " WHERE A.ZTIPO = B.ZID ";
                if (Flujo == "0")
                {
                    if (Estado == "0")
                    {                       
                    }
                    else
                    {
                        Miquery += " AND A.ZID IN (" + Estado + ")";
                    }
                }
                else
                {
                    if (Estado == "0")
                    {
                        Miquery += " AND B.ZID_FLUJO IN (" + Flujo + ")";
                    }
                    else
                    {
                        Miquery += " AND A.ZID IN (" + Estado + ")";
                        Miquery += " AND B.ZID_FLUJO IN (" + Flujo + ")";
                    }
                }
                Miquery += " ORDER BY  B.ZORDEN ";


                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaCampos()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                HttpContext context = HttpContext.Current;

                string Miquery = " SELECT A.ZID, A.ZTITULO, A.ZDESCRIPCION, A.ZNIVEL, A.ZTIPO, A.ZVALOR, A.ZVALORDEFECTO, A.ZACTIVO, A.ZESTADO, A.ZFECHA, A.ZVALIDACION ,B.ZFORMATO ";
                Miquery += " FROM ZCAMPOS A, ZTIPOCAMPO B ";
                if(context.Session["MiNivel"].ToString() == "0")
                {
                    Miquery += "WHERE A.ZTIPO = B.ZID ORDER BY A.ZDESCRIPCION ";
                }
                else
                {
                    Miquery += "WHERE ZNIVEL <= " + context.Session["MiNivel"];
                    //Para desarrollo
                    Miquery += " AND A.ZTIPO = CONVERT( varchar(20),B.ZID)  ORDER BY A.ZDESCRIPCION ";
                }
                //OJO antes orden ZID
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZNIVEL, ZFORMATO, ZVALUE, ZCAPACIDAD, ZACTIVO, ZESTADO, ZTVALIDACION  FROM USER_GEDESPOL.ZCAMPOS ORDER BY ZID ";
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

        public static DataSet CargaJerarquia()
        {
            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZTIPOARCHIVO ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZTIPOS ORDER BY ZID ";
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

        public static DataSet CargaOpciones()//Esto era un procedimiento almacenado
        {
            DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZOPCION ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //OracleDataAdapter Da;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_ESTADOS", cn);
                //comando.CommandType = CommandType.StoredProcedure;

                //// Abrimos conexión
                //if (cn.State == 0) cn.Open();

                //OracleCommandBuilder.DeriveParameters(comando);

                //// Le pasamos los parametros (si existe cursor para el primero se pasa un null
                //comando.Parameters[0].Value = 0; // Nombre
                //                                 //comando.Parameters[1].Value = null;

                //// Ejecutamos y llenamo Ds
                //Da = new OracleDataAdapter(comando);
                //Da.Fill(MyDataSet);

                ////Cerrar conex
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaConexiones()//Esto era un procedimiento almacenado
        {
            DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZCONEXION ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //OracleDataAdapter Da;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_ESTADOS", cn);
                //comando.CommandType = CommandType.StoredProcedure;

                //// Abrimos conexión
                //if (cn.State == 0) cn.Open();

                //OracleCommandBuilder.DeriveParameters(comando);

                //// Le pasamos los parametros (si existe cursor para el primero se pasa un null
                //comando.Parameters[0].Value = 0; // Nombre
                //                                 //comando.Parameters[1].Value = null;

                //// Ejecutamos y llenamo Ds
                //Da = new OracleDataAdapter(comando);
                //Da.Fill(MyDataSet);

                ////Cerrar conex
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaEstados()//Esto era un procedimiento almacenado
        {
            DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZESTADOS ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //OracleDataAdapter Da;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_ESTADOS", cn);
                //comando.CommandType = CommandType.StoredProcedure;

                //// Abrimos conexión
                //if (cn.State == 0) cn.Open();

                //OracleCommandBuilder.DeriveParameters(comando);

                //// Le pasamos los parametros (si existe cursor para el primero se pasa un null
                //comando.Parameters[0].Value = 0; // Nombre
                //                                 //comando.Parameters[1].Value = null;

                //// Ejecutamos y llenamo Ds
                //Da = new OracleDataAdapter(comando);
                //Da.Fill(MyDataSet);

                ////Cerrar conex
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaTipoEvaluacion()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZARCHIVOS WHERE ZTIPO =2 ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZARCHIVOS WHERE ZTIPO =2 ORDER BY ZID ";
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

        public static DataSet CargaDestino()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                //DataSet ds = null;
                //DESTINOS Y DEPARTAMENTOS MIRAN QUE NO SEA EL MISMO
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZDESTINOS ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                ////DataSet ds = null;
                ////DESTINOS Y DEPARTAMENTOS MIRAN QUE NO SEA EL MISMO
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZDESTINOS ORDER BY ZID ";
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

        public static DataSet CargaProvincia()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZPROVINCIAS ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZPROVINCIAS ORDER BY ZID ";
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

        public static DataSet CargaMunicipio()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZMUNICIPIOS ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZMUNICIPIOS ORDER BY ZID ";
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

        public static DataSet CargaPaises()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZPAISES ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZPAISES ORDER BY ZID ";
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


        public static DataSet CargaPeso()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZPESO ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZPESO ORDER BY ZID ";
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

        public static DataSet CargaUsuarios(int MiID)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZCODIGO, ZALIAS as ZNOMBRE, ZPASSWORD, ZKEY, ZROOT, ZNIVEL, ZEQUIPO, ZLLAVE, ZFIRMA, ZEMAIL FROM ZUSUARIOS ";
                Miquery += " WHERE ZNIVEL <= " + MINiVEL;
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZNOMBRE,  ZNIVEL, ZESTADO ZID_DOMINIO, to_char(ZFECHA_ALTA,'dd/mm/yyyy') AS ZFECHA_ALTA, to_char(ZFECHA_BAJA,'dd/mm/yyyy') AS ZFECHA_BAJA, ZEMAIL FROM USER_GEDESPOL.ZGRUPOS ";
                //Miquery += " WHERE ZNIVEL <= " + MINiVEL;
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        //FIN USUARIOS
        //GRUPOS


        public static DataSet CargaGrupo(int MiID)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZNOMBRE,  ZNIVEL, ZESTADO, ZID_DOMINIO, CONVERT(VARCHAR(255),FORMAT(ZFECHA_ALTA,'dd/mm/yyyy')) AS ZFECHA_ALTA, CONVERT(VARCHAR(255),FORMAT(ZFECHA_BAJA,'dd/mm/yyyy')) AS ZFECHA_BAJA, ZEMAIL FROM ZGRUPOS ";
                Miquery += " WHERE ZNIVEL <= " + MINiVEL;
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZNOMBRE,  ZNIVEL, ZESTADO ZID_DOMINIO, to_char(ZFECHA_ALTA,'dd/mm/yyyy') AS ZFECHA_ALTA, to_char(ZFECHA_BAJA,'dd/mm/yyyy') AS ZFECHA_BAJA, ZEMAIL FROM USER_GEDESPOL.ZGRUPOS ";
                //Miquery += " WHERE ZNIVEL <= " + MINiVEL;
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaRelacionesGrupos(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["IDArea"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_USUARIO, A.ZNOMBRE || ' ' ||  A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZNOMBRE, ";
                Miquery += " B.ZID AS ZID_GRUPO, B.ZDESCRIPCION AS ZDESGRUPO ";
                Miquery += " FROM ZUSUARIOS A, ZGRUPOS B, ZGRUPOUSUARIO C  ";
                Miquery += " WHERE A.ZID = C.ZID_USUARIO ";
                Miquery += " AND  B.ZID = C.ZID_GRUPO   ";
                Miquery += " AND B.ZID = " + MiID;
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_USUARIO, A.ZNOMBRE || ' ' ||  A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZNOMBRE, ";
                //Miquery += " B.ZID AS ZID_GRUPO, B.ZDESCRIPCION AS ZDESGRUPO ";
                //Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A, USER_GEDESPOL.ZGRUPOS B, USER_GEDESPOL.ZGRUPOUSUARIO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_USUARIO ";
                //Miquery += " AND  B.ZID = C.ZID_GRUPO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        //FIN GRUPOS

        public static System.Data.DataTable BuscaFlujoEvaluacion(string Ids, string Valor)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            
            //if (Variables.configuracionDB == 0)
            //{
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleParameter[] dbParams = new OracleParameter[0];
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 4 AS RD, B.ZACTIVO, B.ZID_ESTADO, B.ZID_ESTADO AS ESTADO,  ";
            Miquery += " A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 ,  ";
            Miquery += " B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8, B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA,  ";
            Miquery += " B.ZA1ZC1DETALLE , B.ZA1ZC2DETALLE , B.ZA1ZC3DETALLE , B.ZA1ZC4DETALLE , B.ZA1ZC5DETALLE , B.ZA1ZC6DETALLE , ";
            Miquery += " B.ZA1ZC7DETALLE , B.ZA2ZC8DETALLE ,  B.ZA3ZC9DETALLE ,B.ZA3ZC10DETALLE , B.ZA4ZC11DETALLE , B.ZA4ZC12DETALLE, B.ZID_FLUJO, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  ";
            Miquery += "  A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, ";
            Miquery += " A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }

            if(Valor == "-1")
            {
                Miquery += " WHERE A.NIVEL_DE_EVALUACION = 'IG' ";
                Miquery += " AND B.ZID_USUARIO = " + Ids + " ";
            }
            else
            {
                Miquery += " WHERE B.ZID = " + Ids + " ";
                Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            }
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 4 AS RD, B.ZACTIVO, B.ZID_ESTADO, B.ZID_ESTADO AS ESTADO,  ";
            Miquery += " A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 ,  ";
            Miquery += " B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8, B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA,  ";
            Miquery += " B.ZA1ZC1DETALLE , B.ZA1ZC2DETALLE , B.ZA1ZC3DETALLE , B.ZA1ZC4DETALLE , B.ZA1ZC5DETALLE , B.ZA1ZC6DETALLE , ";
            Miquery += " B.ZA1ZC7DETALLE , B.ZA2ZC8DETALLE ,  B.ZA3ZC9DETALLE ,B.ZA3ZC10DETALLE , B.ZA4ZC11DETALLE , B.ZA4ZC12DETALLE,  B.ZID_FLUJO, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  ";
            Miquery += "  A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, ";
            Miquery += " A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }

            if (Valor == "-1")
            {
                Miquery += " WHERE A.NIVEL_DE_EVALUACION = 'IGC' ";
                Miquery += " AND B.ZID_USUARIO = " + Ids + " ";
            }
            else
            {
                Miquery += " WHERE B.ZID = " + Ids + " ";
                Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            }
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 4 AS RD, B.ZACTIVO, B.ZID_ESTADO, B.ZID_ESTADO AS ESTADO,  ";
            Miquery += " A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 ,  ";
            Miquery += " B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8, B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZA1ZC1DETALLE , B.ZA1ZC2DETALLE , B.ZA1ZC3DETALLE , B.ZA1ZC4DETALLE , B.ZA1ZC5DETALLE , B.ZA1ZC6DETALLE , ";
            Miquery += " B.ZA1ZC7DETALLE , B.ZA2ZC8DETALLE ,  B.ZA3ZC9DETALLE ,B.ZA3ZC10DETALLE , B.ZA4ZC11DETALLE , B.ZA4ZC12DETALLE,  B.ZID_FLUJO, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  ";
            Miquery += "  A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, ";
            Miquery += " A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }

            if (Valor == "-1")
            {
                Miquery += " WHERE A.NIVEL_DE_EVALUACION = 'IGB' ";
                Miquery += " AND B.ZID_USUARIO = " + Ids + " ";
            }
            else
            {
                Miquery += " WHERE B.ZID = " + Ids + " ";
                Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            }
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 4 AS RD, B.ZACTIVO, B.ZID_ESTADO, B.ZID_ESTADO AS ESTADO,  ";
            Miquery += " A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 ,  ";
            Miquery += " B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8, B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZA1ZC1DETALLE , B.ZA1ZC2DETALLE , B.ZA1ZC3DETALLE , B.ZA1ZC4DETALLE , B.ZA1ZC5DETALLE , B.ZA1ZC6DETALLE , ";
            Miquery += " B.ZA1ZC7DETALLE , B.ZA2ZC8DETALLE ,  B.ZA3ZC9DETALLE ,B.ZA3ZC10DETALLE , B.ZA4ZC11DETALLE , B.ZA4ZC12DETALLE,  B.ZID_FLUJO, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  ";
            Miquery += "  A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, ";
            Miquery += " A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }

            if (Valor == "-1")
            {
                Miquery += " WHERE A.NIVEL_DE_EVALUACION = 'IGA' ";
                Miquery += " AND B.ZID_USUARIO = " + Ids + " ";
            }
            else
            {
                Miquery += " WHERE B.ZID = " + Ids + " ";
                Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            }
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " ORDER BY ORDEN, ESTADO ";

            if (Variables.configuracionDB == 0)
            {
                //SqlParameter[] dbParams = new SqlParameter[0];
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                context.Session["Query"] = Miquery;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new System.Data.DataTable();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //context.Session["Query"] = Miquery;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataTable();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static System.Data.DataTable CargaArbolFlujos(int Ids, int Profile, string ID_FLUJO)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT A.ZID, A.ZID_USUARIO, A.ZACTIVO, A.ZID_ESTADO, B.ZDESCRIPCION, B.ZNOMBRE, B.ZPREVIUS, B.ZNEXT, B.ZALTERNATIVE, B.ZEND, A.ZID_FLUJO ";
                Miquery += " FROM ZEVAL2018 A LEFT JOIN ZFESTADOS B ON A.ZID_ESTADO = B.ZID ";
                Miquery += " WHERE A.ZID_FLUJO = B.ZID_FLUJO ";
                Miquery += " AND A.ZID_USUARIO = " + Ids + " ";
                Miquery += " ORDER BY A.ZID_FLUJO, A.ZID_ESTADO ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                context.Session["Query"] = Miquery;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new System.Data.DataTable();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //    OracleParameter[] dbParams = new OracleParameter[0];
                //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //    OracleCommand comando;
                //    DataSet MyDataSet = new DataSet();

                //    string Miquery = "SELECT A.ZID, A.ZID_USUARIO, A.ZACTIVO, A.ZID_ESTADO, B.ZDESCRIPCION, B.ZNOMBRE, B.ZPREVIUS, B.ZNEXT, B.ZALTERNATIVE, B.ZEND, A.ZID_FLUJO ";
                //    Miquery += " FROM USER_GEDESPOL.ZEVAL2018 A LEFT JOIN USER_GEDESPOL.ZFESTADOS B ON A.ZID_ESTADO = B.ZID ";
                //    Miquery += " WHERE A.ZID_FLUJO = B.ZID_FLUJO ";
                //    Miquery += " AND A.ZID_USUARIO = " + Ids + " ";
                //    Miquery += " ORDER BY A.ZID_FLUJO, A.ZID_ESTADO ";

                //    comando = new OracleCommand(Miquery, cn);
                //    comando.CommandType = CommandType.Text;

                //    context.Session["Query"] = Miquery;

                //    if (cn.State == 0) cn.Open();
                //    OracleDataAdapter Da = new OracleDataAdapter(comando);
                //    ds = new DataTable();
                //    Da.Fill(ds);

                //    cn.Close();
                //}
            }
            return ds;
        }

        public static System.Data.DataTable CuentaArbolFlujos(int Ids, int Profile, int ID_FLUJO)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT A.ZID, A.ZID_USUARIO, A.ZACTIVO, A.ZID_ESTADO, B.ZDESCRIPCION, B.ZNOMBRE, B.ZPREVIUS, B.ZNEXT, B.ZALTERNATIVE, B.ZEND ";
                Miquery += " FROM ZEVAL2018 A LEFT JOIN ZFESTADOS B ON A.ZID_ESTADO = B.ZID ";
                Miquery += " WHERE A.ZID_USUARIO = " + Ids + " ";
                Miquery += " AND A.ZID_ESTADO = " + Profile + " ";
                Miquery += " AND A.ZID_FLUJO = " + ID_FLUJO + " ";
                Miquery += " AND A.ZID_FLUJO = B.ZID_FLUJO ";
                //Miquery += " AND A.ZACTIVO = 1 ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                context.Session["Query"] = Miquery;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new System.Data.DataTable();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT A.ZID, A.ZID_USUARIO, A.ZACTIVO, A.ZID_ESTADO, B.ZDESCRIPCION, B.ZNOMBRE, B.ZPREVIUS, B.ZNEXT, B.ZALTERNATIVE, B.ZEND ";
                //Miquery += " FROM USER_GEDESPOL.ZEVAL2018 A LEFT JOIN USER_GEDESPOL.ZFESTADOS B ON A.ZID_ESTADO = B.ZID ";
                //Miquery += " WHERE A.ZID_USUARIO = " + Ids + " ";
                //Miquery += " AND A.ZID_ESTADO = " + Profile + " ";
                //Miquery += " AND A.ZID_FLUJO = " + ID_FLUJO + " ";
                //Miquery += " AND A.ZID_FLUJO = B.ZID_FLUJO ";
                ////Miquery += " AND A.ZACTIVO = 1 ";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //context.Session["Query"] = Miquery;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataTable();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static System.Data.DataTable CargaArbolDocumentos(int Ids, int Profile, string Tabla, string Padre)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MiRoot = Convert.ToInt32((context.Session["idarchivo"]));
            //DataSet ds = null;
            try
            {
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                    SqlCommand comando;
                    DataSet MyDataSet = new DataSet();

                    string Miquery = "SELECT ZID, ZID_DOMAIN, ZID_ARCHIVO, ZDESCRIPCION, ZTITULO, ZRUTA, ZPESO, ZROOT, ZDIRECTORIO,  ";
                    Miquery += "ZKEY, ZESTADO, ZFECHA, ZCATEGORIA, ZSUBCATEGORIA, ZID_REGISTRO, ZUSER, (SELECT COUNT(*) FROM " + Tabla + " WHERE ZROOT = AA.ZID) AS zHIJOS ";
                    Miquery += " FROM " + Tabla + " AA ";
                    Miquery += " WHERE ZID_ARCHIVO = " + MiRoot;
                    Miquery += " AND ZID_REGISTRO = " + Ids + " ";
                    Miquery += " ORDER BY ZROOT ";
                    //if (Padre == "0")
                    //{
                    //    Miquery += " AND ZID_REGISTRO = " + Ids + " ";
                    //}
                    //else
                    //{
                    //    Miquery += " AND ZID = " + Ids + " ";
                    //}


                    //string Miquery = "SELECT ZID, ZID_DOMAIN, ZID_ARCHIVO, ZDESCRIPCION, ZTITULO, ZRUTA, ZPESO, ZROOT,  ";
                    //Miquery += "ZKEY, ZESTADO, ZFECHA, ZCATEGORIA, ZSUBCATEGORIA, ZID_REGISTRO, ZUSER, (SELECT COUNT(*) FROM " + Tabla + " WHERE ZROOT = AA.ZID) AS zHIJOS ";
                    //Miquery += " FROM " + Tabla + " AA ";
                    //Miquery += " WHERE ZID <> 0 ";
                    //if(Padre == "0")
                    //{
                    //    Miquery += " AND ZID_REGISTRO = " + Ids + " ";
                    //}
                    //else
                    //{
                    //    Miquery += " AND ZID = " + Ids + " ";
                    //}
                    //Miquery += " FROM " + Tabla + " AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0 ";
                    //Miquery += " ORDER BY ZROOT ";
                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;

                    context.Session["Query"] = Miquery;

                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    ds = new System.Data.DataTable();
                    Da.Fill(ds);

                    cn.Close();
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    //OracleCommand comando;
                    //DataSet MyDataSet = new DataSet();

                    //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, (SELECT COUNT(*) FROM USER_GEDESPOL.ZARCHIVOS WHERE ZROOT = AA.ZID) AS zHIJOS ";
                    //Miquery += " FROM USER_GEDESPOL.ZARCHIVOS AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0";
                    //comando = new OracleCommand(Miquery, cn);
                    //comando.CommandType = CommandType.Text;

                    //context.Session["Query"] = Miquery;

                    //if (cn.State == 0) cn.Open();
                    //OracleDataAdapter Da = new OracleDataAdapter(comando);
                    //ds = new DataTable();
                    //Da.Fill(ds);

                    //cn.Close();
                }
            }
            catch(Exception ex)
            {
                string a = Main.Ficherotraza("CargaArbolDocumentos --> " + ex.Message);
            }

            return ds;
        }

        public static System.Data.DataTable CargaArbolArchivos(int Ids, int Profile)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = 9; // Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                //añadido  AND ZESTADO = 2 para mostrar activos
                string Miquery = "SELECT ZID, ZTABLENAME, ZDESCRIPCION, ZTIPO, (SELECT COUNT(*) FROM ZARCHIVOS WHERE ZROOT = AA.ZID) AS zHIJOS ";
                Miquery += " FROM ZARCHIVOS AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0 AND ZESTADO = 2 ";
                Miquery += " ORDER BY ZDESCRIPCION";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                context.Session["Query"] = Miquery;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new System.Data.DataTable();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, (SELECT COUNT(*) FROM USER_GEDESPOL.ZARCHIVOS WHERE ZROOT = AA.ZID) AS zHIJOS ";
                //Miquery += " FROM USER_GEDESPOL.ZARCHIVOS AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //context.Session["Query"] = Miquery;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataTable();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static System.Data.DataTable CargaArbolCursos(int Ids, int Profile)
        {

            System.Data.DataTable ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT ZID, ZNOMBRE, ZKEY, ZNEXT, TITULOP, PRIMERP, CONSULTAST, SEGUNDOP, TERCEROP, CUARTOP, ";
                Miquery += "  (SELECT COUNT(*) FROM USER_GEDESPOL.ZCOMANDSQL WHERE ZROOT = AA.ZID) AS zHIJOS ";
                Miquery += " FROM ZCOMANDSQL AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                context.Session["Query"] = Miquery;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new System.Data.DataTable();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT ZID, ZNOMBRE, ZKEY, ZNEXT, TITULOP, PRIMERP, CONSULTAST, SEGUNDOP, TERCEROP, CUARTOP, ";
                //Miquery += "  (SELECT COUNT(*) FROM USER_GEDESPOL.ZCOMANDSQL WHERE ZROOT = AA.ZID) AS zHIJOS ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMANDSQL AA WHERE ZROOT = " + Ids + " AND ZNIVEL <= " + MINiVEL + " AND ZID <> 0";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //context.Session["Query"] = Miquery;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataTable();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }
        public static DataSet ArbolArchivos()
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = 9; // Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZESTADO, ZID_DOMINIO , ZROW ";
                Miquery += " FROM ZARCHIVOS  WHERE ZNIVEL <= " + MINiVEL;
                Miquery += " ORDER BY ZID ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, ZNIVEL, ZROOT, ZKEY, ZTABLENAME, ZTABLEOBJ, ZESTADO, ZID_DOMINIO , ZROW ";
                //Miquery += " FROM USER_GEDESPOL.ZARCHIVOS  WHERE ZNIVEL <= " + MINiVEL;
                //Miquery += " ORDER BY ZID ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet ArbolArchivosSQL()
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            int ID = Convert.ToInt32((context.Session["IDCurso"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT * ";
                Miquery += " FROM ZCOMANDSQL  WHERE ZNIVEL <= " + MINiVEL;
                Miquery += " ORDER BY ZID ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT * ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMANDSQL  WHERE ZNIVEL <= " + MINiVEL;
                //Miquery += " ORDER BY ZID ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet ArbolConexion()
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT ZID,  ZDESCRIPCION ";
                Miquery += " FROM ZCONEXION ";
                Miquery += " ORDER BY ZID ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT ZID,  ZDESCRIPCION ";
                //Miquery += " FROM USER_GEDESPOL.ZCONEXION ";
                //Miquery += " ORDER BY ZID ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet ConectaSQLServer(string Conexion, string username)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));

            SqlConnection cn = new SqlConnection(Conexion);
            SqlCommand comando;

            DataSet MyDataSet = new DataSet();

            string Miquery = "SELECT name as ZDESCRIPCION FROM sysobjects WHERE type='U' ORDER BY name ";

            comando = new SqlCommand(Miquery, cn);
            comando.CommandType = CommandType.Text;

            if (cn.State == 0) cn.Open();

            SqlDataAdapter Da = new SqlDataAdapter(comando);
            ds = new DataSet();
            Da.Fill(ds);

            cn.Close();

            return ds;
        }

        
       public static DataSet ConectaAccess(string Conexion, string username)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            DataSet MyDataSet = new DataSet();

            OleDbConnection cn = new OleDbConnection(Conexion);
            //set up connection string
            string Miquery = "SELECT MSysObjects.Name as ZDESCRIPCION FROM MSysObjects WHERE MSysObjects.Type = 1";

            OleDbCommand comando = new OleDbCommand(Miquery, cn);
            //OleDbParameter param0 = new OleDbParameter("@login", OleDbType.VarChar);

            //param0.Value = employeeID.Text;
            //command.Parameters.Add(param0);

            //middle tier to run connect
            if (cn.State == 0) cn.Open();
            OleDbDataAdapter Da = new OleDbDataAdapter(comando);
            ds = new DataSet();
            Da.Fill(ds);

            cn.Close();

            return ds;

        }

        public static DataSet ConectaOracle(string Conexion, string username)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiNivel"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(Conexion);
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT table_name as ZDESCRIPCION FROM all_tables WHERE owner = '" + username + "' ORDER BY table_name ";


                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(Conexion);
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT table_name as ZDESCRIPCION FROM all_tables WHERE owner = '" + username + "' ORDER BY table_name ";


                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet PropiedadesTablaObjeto(string Tabla)
        {
            DataSet ds = null;
            
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                //SQL SERVER
                string SQL = " SELECT ";
                SQL += " so.name AS Tabla, ";
                SQL += " sc.name AS Columna, ";
                SQL += " st.name AS Tipo, ";
                SQL += " sc.max_length AS Tamaño, ";
                SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN 'varchar' ";
                SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN 'numeric' ";
                SQL += " WHEN st.name = 'decimal' THEN 'decimal' ";
                SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN 'Fecha'  END AS RELACION, ";
                SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN '1' ";
                SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN '2' ";
                SQL += " WHEN st.name = 'decimal' THEN '5' ";
                SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN '4'  END AS VALORRELACION, ";
                SQL += " CASE WHEN CONVERT(VARCHAR(255), sc.max_length) = '-1' OR CONVERT(VARCHAR(255),sc.max_length) = '8000' THEN 'MAX' ELSE CONVERT(VARCHAR(255),sc.max_length) END VALORCOLUMNA ";
                //SQL += " INTO TABLASCOLUMNAS ";
                SQL += " FROM ";
                SQL += " sys.objects so INNER JOIN ";
                SQL += " sys.columns sc ON ";
                SQL += " so.object_id = sc.object_id INNER JOIN ";
                SQL += " sys.types st ON ";
                SQL += " st.system_type_id = sc.system_type_id ";
                SQL += " AND st.name != 'sysname' ";
                SQL += " WHERE so.type = 'U' ";
                SQL += " and so.name ='" + Tabla + "' ";
                SQL += " ORDER BY so.name,sc.name ";

                comando = new SqlCommand(SQL, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }




            return ds;
        }

        public static DataSet ExisteTabla(string Tabla)
        {
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                //SQL SERVER
                string SQL = " SELECT TOP 1 (so.name) ";
                SQL += " FROM ";
                SQL += " sys.objects so INNER JOIN ";
                SQL += " sys.columns sc ON ";
                SQL += " so.object_id = sc.object_id INNER JOIN ";
                SQL += " sys.types st ON ";
                SQL += " st.system_type_id = sc.system_type_id ";
                SQL += " AND st.name != 'sysname' ";
                SQL += " WHERE so.type = 'U' ";
                SQL += " and so.name ='" + Tabla + "' ";

                comando = new SqlCommand(SQL, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {

            }




            return ds;
        }




        public static DataSet PropiedadesTabla(string Tabla)
        {
            DataSet ds = null;

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                //SQL SERVER
                string SQL = " SELECT ";
                SQL += " so.name AS Tabla, ";
                SQL += " sc.name AS Columna, ";
                SQL += " st.name AS Tipo, ";
                SQL += " sc.max_length AS Tamaño, ";
                SQL += " sm.ZID, ";
                SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN 'varchar' ";
                SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN 'numeric' ";
                SQL += " WHEN st.name = 'decimal' THEN 'decimal' ";
                SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN 'Fecha'  END AS RELACION, ";
                SQL += " CASE WHEN st.name = 'varchar' OR st.name = 'nvarchar' OR st.name = 'nchar' OR st.name = 'char'  THEN '1' ";
                SQL += " WHEN st.name = 'int' OR st.name = 'numeric' OR st.name = 'smallint' OR st.name = 'tinyint' THEN '2' ";
                SQL += " WHEN st.name = 'decimal' THEN '5' ";
                SQL += " WHEN st.name = 'datetime' OR st.name = 'smalldatetime' OR st.name = 'date' THEN '4'  END AS VALORRELACION, ";
                SQL += " CASE WHEN CONVERT(VARCHAR(255), sc.max_length) = '-1' OR CONVERT(VARCHAR(255),sc.max_length) = '8000' THEN 'MAX' ELSE CONVERT(VARCHAR(255),sc.max_length) END VALORCOLUMNA ";
                //SQL += " INTO TABLASCOLUMNAS ";
                SQL += " FROM ";
                SQL += " sys.objects so INNER JOIN ";
                SQL += " sys.columns sc ON ";
                SQL += " so.object_id = sc.object_id INNER JOIN ";
                SQL += " sys.types st ON ";
                SQL += " st.system_type_id = sc.system_type_id ";
                SQL += " AND st.name != 'sysname' ";
                SQL += " INNER JOIN ZCAMPOS sm ON sm.ZTITULO = sc.name ";
                SQL += " WHERE so.type = 'U' ";
                SQL += " and so.name ='" + Tabla + "' ";
                SQL += " ORDER BY so.name,sc.name ";

                comando = new SqlCommand(SQL, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }


            

            return ds;
        }


        public static DataSet ConsultaMiTabla(string Tabla)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiTabla"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "'"; ORACLE
                //SQL SERVER
                string Miquery = "SELECT * FROM information_schema.columns WHERE  table_name = '" + Tabla + "'";
                Miquery += " ORDER BY ordinal_position  ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet ConsultaMiTablaOracle(string Tabla, string conexion, string username)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiTabla"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(conexion);
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "' and owner = '" + username + "'";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(conexion);
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "' and owner = '" + username + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet ConsultaMiTablaSQL(string Tabla, string conexion, string username)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiTabla"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(conexion);
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "' and owner = '" + username + "'";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(conexion);
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT COLUMN_NAME AS ZCOLUMNA FROM ALL_TAB_COLUMNS WHERE TABLE_NAME  = '" + Tabla + "' and owner = '" + username + "'";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static System.Data.DataTable ConsultaMiTablaACCESS(string Tabla, string conexion, string username)
        {
            using (OleDbConnection connection = new OleDbConnection(conexion))
            {
                connection.Open();
                System.Data.DataTable dtSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
                    new object[] { null, null, Tabla, null });

                return dtSchema;
            }
        }

        public static DataSet ConsultaLATabla(string Tabla)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiTabla"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT * FROM " + Tabla + " ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT * FROM USER_GEDESPOL." + Tabla + " ";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet ConsultaTablaGeneral(string Tabla)
        {

            DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int MINiVEL = Convert.ToInt32((context.Session["MiTabla"]));
            string Miquery = context.Session["LaConsulta"].ToString();

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaSinRelacionesArchivos(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = " SELECT ROW_NUMBER() OVER( ORDER BY SO.NAME ASC ) as 'POSICION', ";
                Miquery += " A.ZID AS ZID_CAMPO, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, C.ZID AS ZID_ARCHIVO, C.ZVIEW, A.ZVALORDEFECTO, C.ZKEY, C.ZDUPLICADOS, C.ZTIPO AS ZTIPOARCHIVO, ";
                Miquery += " C.ZDESCRIPCION AS ZDESARCHIVO, C.ZTABLENAME, C.ZTABLEOBJ,A.ZVALIDACION, SO.NAME AS TABLA, SC.NAME AS COLUMNA, 0 AS KEYCAMPO ";
                Miquery += " FROM sysobjects SO INNER JOIN syscolumns SC";
                Miquery += " ON SO.ID = SC.ID";
                Miquery += " INNER JOIN ZCAMPOS A ON A.ZTITULO = SC.NAME";
                Miquery += " INNER JOIN ZARCHIVOS C ON C.ZTABLENAME = SO.NAME";
                Miquery += " WHERE SO.XTYPE = 'U' ";
                Miquery += " AND A.ZNIVEL <= " + ID;
                Miquery += " AND C.ZNIVEL <= " + ID;
                Miquery += " AND C.ZID = " + MiID;



                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZNOMBRE, A.ZDESCRIPCION, ";
                //Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO ";
                //Miquery += " FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOS B, USER_GEDESPOL.ZARCHIVOCAMPO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_CAMPO ";
                //Miquery += " AND  B.ZID = C.ZID_ARCHIVO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " AND A.ZNIVEL <= " + ID;
                //Miquery += " AND B.ZNIVEL <= " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaRelacionesdocumentosArchivos(int MiID, int Fichero, string TablaObjeto)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "";

                if (Fichero == 1)
                {
                    //--Ruta a fichero
                    Miquery = " SELECT B.ZRUTA, B.ZID_ARCHIVO AS ZID_REGISTRO, A.ZID AS ZID_FILE, A.ZID_VOLUMEN, A.ZRUTA, (B.ZRUTA + A.ZRUTA) AS UNC_FILE, B.ZRUTA + A.ZDESCRIPCION AS PATH_FILE ";
                    Miquery += " FROM " + TablaObjeto + " A, ZVOLUMENES B ";
                    Miquery += " WHERE A.ZID = " + MiID;
                    Miquery += " AND A.ZID = B.ZID_ARCHIVO ";
                    Miquery += " ORDER BY B.ZID ";
                }
                else
                {
                    //--Ficheros del registro
                    Miquery = " SELECT B.ZRUTA, B.ZID_ARCHIVO AS ZID_REGISTRO, A.ZID AS ZID_FILE, A.ZID_VOLUMEN, A.ZRUTA, (B.ZRUTA + A.ZRUTA) AS UNC_FILE, B.ZRUTA + A.ZDESCRIPCION AS PATH_FILE ";
                    Miquery += " FROM " + TablaObjeto + " A, ZVOLUMENES B  ";
                    Miquery += " WHERE A.ZID_ARCHIVO = " + MiID ;
                    Miquery += " AND A.ZID_ARCHIVO = B.ZID_ARCHIVO  ";
                    Miquery += " ORDER BY B.ZID  ";
                }

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZNOMBRE, A.ZDESCRIPCION, ";
                //Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO ";
                //Miquery += " FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOS B, USER_GEDESPOL.ZARCHIVOCAMPO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_CAMPO ";
                //Miquery += " AND  B.ZID = C.ZID_ARCHIVO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " AND A.ZNIVEL <= " + ID;
                //Miquery += " AND B.ZNIVEL <= " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaRelacionesArchivosConsulta(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();


                string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, B.ZVIEW, D.ZFORMATO ,";
                Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO, B.ZTABLENAME, B.ZTABLEOBJ, A.ZVALIDACION, A.ZVALORDEFECTO, B.ZKEY , C.ZKEY AS KEYCAMPO, B.ZDUPLICADOS, B.ZTIPO AS ZTIPOARCHIVO ";
                Miquery += " FROM ZCAMPOS A, ZARCHIVOS B, ZARCHIVOCAMPOS C  , ZTIPOCAMPO D ";
                Miquery += " WHERE A.ZID = C.ZIDCAMPO ";
                Miquery += " AND  B.ZID = C.ZIDARCHIVO   ";
                Miquery += " AND B.ZID = " + MiID;
                if (ID != 0)
                {
                    Miquery += " AND A.ZNIVEL <= " + ID;
                    Miquery += " AND B.ZNIVEL <= " + ID;
                }
                Miquery += " AND D.ZID = A.ZTIPO ";
                Miquery += " AND B.ZESTADO = 2 ";
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZNOMBRE, A.ZDESCRIPCION, ";
                //Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO ";
                //Miquery += " FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOS B, USER_GEDESPOL.ZARCHIVOCAMPO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_CAMPO ";
                //Miquery += " AND  B.ZID = C.ZID_ARCHIVO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " AND A.ZNIVEL <= " + ID;
                //Miquery += " AND B.ZNIVEL <= " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaRelacionesArchivos(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();


                string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, B.ZVIEW, D.ZFORMATO ,";
                Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO, B.ZTABLENAME, B.ZTABLEOBJ, A.ZVALIDACION, A.ZVALORDEFECTO, B.ZKEY , C.ZKEY AS KEYCAMPO, B.ZDUPLICADOS, B.ZTIPO AS ZTIPOARCHIVO ";
                Miquery += " FROM ZCAMPOS A, ZARCHIVOS B, ZARCHIVOCAMPOS C  , ZTIPOCAMPO D ";
                Miquery += " WHERE A.ZID = C.ZIDCAMPO ";
                Miquery += " AND  B.ZID = C.ZIDARCHIVO   ";
                Miquery += " AND B.ZID = " + MiID;
                if (ID != 0)
                {
                    Miquery += " AND A.ZNIVEL <= " + ID;
                    Miquery += " AND B.ZNIVEL <= " + ID;
                }
                Miquery += " AND D.ZID = A.ZTIPO ";
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZNOMBRE, A.ZDESCRIPCION, ";
                //Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO ";
                //Miquery += " FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOS B, USER_GEDESPOL.ZARCHIVOCAMPO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_CAMPO ";
                //Miquery += " AND  B.ZID = C.ZID_ARCHIVO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " AND A.ZNIVEL <= " + ID;
                //Miquery += " AND B.ZNIVEL <= " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaRelacionesArchivoTval(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();


                string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZTITULO, A.ZDESCRIPCION, A.ZTIPO, B.ZVIEW, D.ZFORMATO ,";
                Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO, B.ZTABLENAME, B.ZTABLEOBJ, A.ZVALIDACION, A.ZVALORDEFECTO, B.ZKEY , C.ZKEY AS KEYCAMPO, B.ZDUPLICADOS, B.ZTIPO AS ZTIPOARCHIVO ";
                Miquery += " FROM ZCAMPOS A, ZARCHIVOS B, ZARCHIVOCAMPOS C  , ZTIPOCAMPO D ";
                Miquery += " WHERE A.ZID = C.ZIDCAMPO ";
                Miquery += " AND  B.ZID = C.ZIDARCHIVO   ";
                Miquery += " AND  A.ZVALIDACION <> 0   ";
                Miquery += " AND B.ZID = " + MiID;
                if (ID != 0)
                {
                    Miquery += " AND A.ZNIVEL <= " + ID;
                    Miquery += " AND B.ZNIVEL <= " + ID;
                }
                Miquery += " AND D.ZID = A.ZTIPO ";
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_CAMPO, A.ZNOMBRE, A.ZDESCRIPCION, ";
                //Miquery += " B.ZID AS ZID_ARCHIVO, B.ZDESCRIPCION AS ZDESARCHIVO ";
                //Miquery += " FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOS B, USER_GEDESPOL.ZARCHIVOCAMPO C  ";
                //Miquery += " WHERE A.ZID = C.ZID_CAMPO ";
                //Miquery += " AND  B.ZID = C.ZID_ARCHIVO   ";
                //Miquery += " AND B.ZID = " + MiID;
                //Miquery += " AND A.ZNIVEL <= " + ID;
                //Miquery += " AND B.ZNIVEL <= " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }
        //ROLES
        public static DataSet CargaRol(int MiID)
        {


            DataSet ds = null;

            //DataSet ds = null;
            HttpContext context = HttpContext.Current;
            int IDNivel = Convert.ToInt32((context.Session["MiNivel"]));
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT DISTINCT A.ZID, A.ZDESCRIPCION, A.ZNOMBRE, A.ZNIVEL, A.ZPERMISO, A.ZESTADO, A.ZID_ARCHIVO, A.ZID_DOMINIO, A.ZID_CAMPO,  CONVERT(VARCHAR(255),FORMAT(A.ZFECHA_ALTA,'dd/mm/yyyy')) AS ZFECHA_ALTA,  CONVERT(VARCHAR(255),FORMAT(A.ZFECHA_BAJA,'dd/mm/yyyy')) AS ZFECHA_BAJA, A.ZEMAIL ";
                //Miquery += " FROM ZROLES A, ZGRUPOS B, ZROLESGRUPOSUSUARIOS C ";
                //Miquery += "WHERE A.ZID = C.ZID_ROL  ";
                //Miquery += "AND B.ZID = C.ZID_GRUPO  ";
                //Miquery += "AND A.ZNIVEL <= " + IDNivel;

                string Miquery = "SELECT DISTINCT A.ZID, ";
                Miquery += " A.ZDESCRIPCION, A.ZNOMBRE, A.ZNIVEL, A.ZPERMISO, A.ZESTADO, A.ZID_ARCHIVO, A.ZID_DOMINIO, A.ZID_CAMPO,    ";
                Miquery += " CONVERT(VARCHAR(255), FORMAT(A.ZFECHA_ALTA, 'dd/mm/yyyy')) AS ZFECHA_ALTA,  ";
                Miquery += " CONVERT(VARCHAR(255), FORMAT(A.ZFECHA_BAJA, 'dd/mm/yyyy')) AS ZFECHA_BAJA, A.ZEMAIL  ";
                Miquery += " FROM ZROLES A  ";
                Miquery += " WHERE A.ZNIVEL <= " + IDNivel;


                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT DISTINCT A.ZID, A.ZDESCRIPCION, A.ZNOMBRE, A.ZNIVEL, A.ZPERMISO, A.ZESTADO, A.ZID_ARCHIVO, A.ZID_DOMINIO, A.ZID_CAMPO, to_char(A.ZFECHA_ALTA,'dd/mm/yyyy') AS ZFECHA_ALTA, to_char(A.ZFECHA_BAJA,'dd/mm/yyyy') AS ZFECHA_BAJA, A.ZEMAIL ";
                //Miquery += " FROM USER_GEDESPOL.ZROLES A, USER_GEDESPOL.ZGRUPOS B, USER_GEDESPOL.ZROLGRUPOUSUARIO C ";
                //Miquery += "WHERE A.ZID = C.ZID_ROL  ";
                //Miquery += "AND B.ZID = C.ZID_GRUPO  ";
                //Miquery += "AND A.ZNIVEL <= " + IDNivel;

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }




        public static Boolean PermitirArchivoDuplicado(System.Data.DataTable DtArchivo)
        {
            Boolean Esta = false;
            //Comprueba si el Archivo Documental permite duplicados
            foreach (DataRow fila in DtArchivo.Rows)
            {
                if (fila["ZDUPLICADOS"].ToString() == "1")
                {
                    return Esta;
                }
                else
                {
                    Esta = true;
                }
                break;
            }

           return Esta;
        }

        public static DataSet CargaRelacionesRoles(int MiID)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["IDArea"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZID_ARCHIVO, B.ZID AS ZID_GRUPO, B.ZDESCRIPCION AS ZDESCGRUPO ";
                Miquery += " FROM ZROLES A, ZGRUPOS B, ZROLGRUPOUSUARIO C ";
                Miquery += "WHERE A.ZID = C.ZID_ROL  ";
                Miquery += "AND B.ZID = C.ZID_GRUPO  ";
                Miquery += "AND A.ZID = " + MiID;

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID, A.ZDESCRIPCION, A.ZID_ARCHIVO, B.ZID AS ZID_GRUPO, B.ZDESCRIPCION AS ZDESCGRUPO ";
                //Miquery += " FROM USER_GEDESPOL.ZROLES A, USER_GEDESPOL.ZGRUPOS B, USER_GEDESPOL.ZROLGRUPOUSUARIO C ";
                //Miquery += "WHERE A.ZID = C.ZID_ROL  ";
                //Miquery += "AND B.ZID = C.ZID_GRUPO  ";
                //Miquery += "AND A.ZID = " + MiID;

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaEvaluacionRol()
        {


            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZACTIVO, ZDETALLE, ZTABLENAME, ZTABLEDOC, ZNIVEL FROM ZEVALUACION ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZACTIVO, ZDETALLE, ZTABLENAME, ZTABLEDOC, ZNIVEL FROM USER_GEDESPOL.ZEVALUACION ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        //FIN ROLES

        //COMPETENCIAS

        public static DataSet GargaDatosCompetencias()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZNIVEL FROM ZCOMPETENCIAS ORDER BY ZID";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZNIVEL FROM USER_GEDESPOL.ZCOMPETENCIAS ORDER BY ZID";
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


        public static DataSet GuardaCompetencia(string SQL, int ID)
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = SQL;
                //WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = SQL;
                ////WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //cn.Close();
            }

            //Miquery = "UPDATE USER_GEDESPOL.ZARCHIVOS SET ZROW = " + (ID + 1) + " WHERE ZID = " + ID + ";";
            ////WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
            //comando = new OracleCommand(Miquery, cn);
            //comando.CommandType = CommandType.Text;

            //if (cn.State == 0) cn.Open();
            //comando.ExecuteNonQuery();
            //cn.Close();

            return ds;
        }

        public static DataSet CargaLasCompetencias()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            ////string Var = context.Session["MisCompetencias"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZCAMPO ";
                Miquery += " FROM ZCOMPETENCIAS ";
                //Miquery += " WHERE ZID in (" + Var + ") ";
                Miquery += " ORDER BY ZID ";

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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZCAMPO ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS ";
                ////Miquery += " WHERE ZID in (" + Var + ") ";
                //Miquery += " ORDER BY ZID ";

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

        public static DataSet CargaCompetencias()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Var = context.Session["MisCompetencias"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZCAMPO ";
                Miquery += " FROM ZCOMPETENCIAS ";
                Miquery += " WHERE ZID in (" + Var + ") ";
                Miquery += " ORDER BY ZID ";

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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZCAMPO ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS ";
                //Miquery += " WHERE ZID in (" + Var + ") ";
                //Miquery += " ORDER BY ZID ";

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

        public static DataSet LasRelacionesAreaCompetencia()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            //int ID = Convert.ToInt32((context.Session["IDArea"]));
            string ID = "1,2,3,4";


            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREACOMPETENCIA C  ";
                Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID in (" + ID + ") ";
                Miquery += " ORDER BY B.ZID, A.ZID ";
                //Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREACOMPETENCIA C  ";
                //Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID in (" + ID + ") ";
                //Miquery += " ORDER BY B.ZID, A.ZID ";
                ////Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaRelacionesAreaCompetencia()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["IDArea"]));

            int Nivel = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREACOMPETENCIA C  ";
                Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID = " + ID;
                Miquery += " AND A.ZNIVEL <= " + Nivel;
                Miquery += " AND B.ZNIVEL <=  " + Nivel;
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREACOMPETENCIA C  ";
                //Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID = " + ID;
                //Miquery += " AND A.ZNIVEL <= " + Nivel;
                //Miquery += " AND B.ZNIVEL <=  " + Nivel;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CompletaAreaCompetencia()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            string ID = Convert.ToString((context.Session["BuscaAreas"]));

            //int Nivel = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZDETALLE, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREACOMPETENCIA C  ";
                Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID in (" + ID + ")";
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZDETALLE, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREACOMPETENCIA C  ";
                //Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID in (" + ID + ")";
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet RelacionAreaCompetencia()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current; 
            string ID = Convert.ToString((context.Session["MisAreas"]));

            //int Nivel = Convert.ToInt32((context.Session["MiNivel"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREACOMPETENCIA C  ";
                Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID in (" + ID + ")";
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCCOMP, A.ZTIPOEVALUACION AS ZTIPEVALCOMP, A.ZPESO AS ZPESOCOMP, A.ZOBSERVACION AS ZOBSCOMP, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREACOMPETENCIA C  ";
                //Miquery += " WHERE A.ZID = C.ZID_COMPETENCIA ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID in (" + ID + ")";
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaIDCompetencia(int ID)
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZROW FROM ZARCHIVOS WHERE ZID =" + ID + ";";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZROW FROM ZARCHIVOS WHERE ZID =" + ID + ";";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaCompetencia()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZNIVEL FROM ZCOMPETENCIAS ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZNIVEL FROM USER_GEDESPOL.ZCOMPETENCIAS ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }




   



    //FIN COMPETENCIAS
    //AREAS
    public static DataSet CargaAreas()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;

            string Var = context.Session["MisAreas"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZOBLIGATORIA, ZCLASE, ZNIVEL ";
                Miquery += " FROM ZAREAS ";
                Miquery += " WHERE ZID in (" + Var + ") ";
                Miquery += " ORDER BY ZID ";


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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT DISTINCT ZID , ZDESCRIPCION , ZTIPOEVALUACION, ZPESO , ZOBSERVACION, ZOBLIGATORIA, ZCLASE, ZNIVEL ";
                //Miquery += " FROM USER_GEDESPOL.ZAREAS ";
                //Miquery += " WHERE ZID in (" + Var + ") ";
                //Miquery += " ORDER BY ZID ";


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

        //FIN AREAS
        //EVALUACIONES

        public static DataRow ConsultaEvaluacion()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //DataSet ds = null;
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Var = context.Session["IDCompetencia"].ToString();
            string Var2 = context.Session["IDUsuario"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT A.ZID,A.ZDESCRIPCION,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA2ZC1,A.ZA2ZC2,A.ZA2ZC3,A.ZA3ZC1,A.ZA3ZC2,A.ZA3ZC4,";
                string Miquery = "SELECT A.ZID,A.ZDESCRIPCION,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9.A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12,";
                Miquery += " A.ZID_ESTADO,A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, B.ZID AS ZIDCAL, B.ZDESCRIPCION AS ZDESCCAL ";
                Miquery += " FROM ZEVAL2018 A, ZCALIFICACION B ";
                Miquery += " WHERE ZID_USUARIO in (" + Var + ") ";
                Miquery += " AND ZID_COMPETENCIA in (" + Var + ") ";
                Miquery += " ORDER BY ZID ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                //ds = new DataSet();

                Da.Fill(dt);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                ////string Miquery = "SELECT A.ZID,A.ZDESCRIPCION,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA2ZC1,A.ZA2ZC2,A.ZA2ZC3,A.ZA3ZC1,A.ZA3ZC2,A.ZA3ZC4,";
                //string Miquery = "SELECT A.ZID,A.ZDESCRIPCION,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9.A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12,";
                //Miquery += " A.ZID_ESTADO,A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, B.ZID AS ZIDCAL, B.ZDESCRIPCION AS ZDESCCAL ";
                //Miquery += " FROM USER_GEDESPOL.ZEVAL2018 A, USER_GEDESPOL.ZCALIFICACION B ";
                //Miquery += " WHERE ZID_USUARIO in (" + Var + ") ";
                //Miquery += " AND ZID_COMPETENCIA in (" + Var + ") ";
                //Miquery += " ORDER BY ZID ";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                ////ds = new DataSet();

                //Da.Fill(dt);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                dt.Columns.Add("ZID");
                dt.Columns.Add("ZDescripcion");
                return dt.Rows[0];
            }
  


        }


        public static DataSet ConsultaTipoEvaluacion()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION FROM ZCALIFICACION ORDER BY ZID ";
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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION FROM USER_GEDESPOL.ZCALIFICACION ORDER BY ZID ";
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


        public static DataSet CargaEvaluaciones()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;

            HttpContext context = HttpContext.Current;
            string Var = context.Session["MisEvaluaciones"].ToString();

            if (Var !="")
            {
                //DataSet ds = null;
                if (Variables.configuracionDB == 0)
                {
                    SqlParameter[] dbParams = new SqlParameter[0];
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    SqlCommand comando;
                    DataSet MyDataSet = new DataSet();

                    string Miquery = "SELECT DISTINCT A.ZID , A.ZDESCRIPCION , D.ZID AS ZID_AREA, D.ZDESCRIPCION AS ZDESCAREA, E.ZID AS ZID_COMPETENCIA, E.ZDESCRIPCION AS ZDESCCOMPETENCIA ";
                    Miquery += " FROM ZEVALUACION A, ";
                    Miquery += " ZAREAEVALUACION B, ";
                    Miquery += " ZAREACOMPETENCIA C, ";
                    Miquery += " ZAREAS D, ";
                    Miquery += " ZCOMPETENCIAS E ";
                    Miquery += " WHERE A.ZID in (" + Var + ") ";
                    Miquery += " AND A.ZID = B.ZID_EVALUACION ";
                    Miquery += " AND B.ZID_AREA = C.ZID_AREA ";
                    Miquery += " AND B.ZID_AREA = D.ZID ";
                    Miquery += " AND B.ZID_AREA = C.ZID_AREA ";
                    Miquery += " AND C.ZID_COMPETENCIA = E.ZID ";
                    Miquery += " ORDER BY ZID ";


                    comando = new SqlCommand(Miquery, cn);
                    comando.CommandType = CommandType.Text;

                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    cn.Close();
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleParameter[] dbParams = new OracleParameter[0];
                    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    //OracleCommand comando;
                    //DataSet MyDataSet = new DataSet();

                    //string Miquery = "SELECT DISTINCT A.ZID , A.ZDESCRIPCION , D.ZID AS ZID_AREA, D.ZDESCRIPCION AS ZDESCAREA, E.ZID AS ZID_COMPETENCIA, E.ZDESCRIPCION AS ZDESCCOMPETENCIA ";
                    //Miquery += " FROM USER_GEDESPOL.ZEVALUACION A, ";
                    //Miquery += " USER_GEDESPOL.ZAREAEVALUACION B, ";
                    //Miquery += " USER_GEDESPOL.ZAREACOMPETENCIA C, ";
                    //Miquery += " USER_GEDESPOL.ZAREAS D, ";
                    //Miquery += " USER_GEDESPOL.ZCOMPETENCIAS E ";
                    //Miquery += " WHERE A.ZID in (" + Var + ") ";
                    //Miquery += " AND A.ZID = B.ZID_EVALUACION ";
                    //Miquery += " AND B.ZID_AREA = C.ZID_AREA ";
                    //Miquery += " AND B.ZID_AREA = D.ZID ";
                    //Miquery += " AND B.ZID_AREA = C.ZID_AREA ";
                    //Miquery += " AND C.ZID_COMPETENCIA = E.ZID ";
                    //Miquery += " ORDER BY ZID ";


                    //comando = new OracleCommand(Miquery, cn);
                    //comando.CommandType = CommandType.Text;

                    //if (cn.State == 0) cn.Open();
                    //OracleDataAdapter Da = new OracleDataAdapter(comando);
                    //Da.Fill(ds);
                    ////ListUser.Add(ds);
                    //cn.Close();
                }
            }

                return ds;

        }

        public static DataSet CargaInformes()
        {

            DataSet ds = new DataSet();
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT DISTINCT A.ID , A.CREATE_FECHA , A.MOD_FECHA, A.USUARIO, A.NOMBRE, A.IDFICHERO, A.ACTIVO,A.ORDEN, A.DESCRIPCION ";
                Miquery += " FROM CONF_INFORMES A ";
                Miquery += " ORDER BY A.ID ";


                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT DISTINCT A.ID , A.CREATE_FECHA , A.MOD_FECHA, A.USUARIO, A.NOMBRE, A.IDFICHERO, A.ACTIVO,A.ORDEN, A.DESCRIPCION ";
                //Miquery += " FROM USER_GEDESPOL.CONF_INFORMES A ";
                //Miquery += " ORDER BY A.ID ";


                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;

        }

        public static string Actualiza_ULMenu( string Dato, System.Data.DataTable table)
        {
            HttpContext context = HttpContext.Current;
            System.Data.DataTable MiMenu = context.Session["totalMenus"] as System.Data.DataTable;

#pragma warning disable CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
            Boolean Esta = false;
#pragma warning restore CS0219 // La variable 'Esta' está asignada pero su valor nunca se usa
            Boolean Paso = false;

            string[] Fields = System.Text.RegularExpressions.Regex.Split(Dato, Environment.NewLine);
            Dato = ""; //Borro el Dato trasvasado
            int Contador = 0;
            string CampoAnterior = "";
            foreach (string Field in Fields)
            {
                Esta = false;

                if (Field.Contains("<ul>") && Paso == false && CampoAnterior == "")
                {
                    //Paso = true;
                    Dato += Field + Environment.NewLine;
                }
                else if (Field.Contains("<ul>") && Paso == true && CampoAnterior != "")
                {
                    Paso = true;
                    Dato += CampoAnterior + Environment.NewLine + Field + Environment.NewLine;
                }
                else if (Field.Contains("</ul>") && Paso == true)
                {
                    Paso = false;
                    Dato += Field + Environment.NewLine;
                }
                else if (Field.Contains("<li>") && Paso == false)
                {
                    //foreach (DataRow fila in table.Rows)
                    //{
                    //    if (Field.Contains(fila["ZPAGINA"].ToString()) == true)
                    //    {
                    //        Esta = true;
                    //        break;
                    //    }

                        //foreach (DataRow filamin in MiMenu.Rows)
                        //{
                        //    if (fila["ZROOT"].ToString() == filamin["ZID"].ToString())
                        //    {
                        //        Esta = true;
                        //        break;
                        //    }
                        //}
                        //if (Esta == true) { break; }
                    //}

                    //if (Esta == true)
                    //{
                    //    Dato += Field + "</li>" + Environment.NewLine;
                    //}
                }
                else if (Field.Contains("<li>") && Paso == true)
                {
                    Dato += Field + Environment.NewLine;
                }
                    //else
                    //{
                    //    Dato += Field + Environment.NewLine;
                    //}
                    Contador += 1;
                CampoAnterior = Field;
            }

            return Dato;
        }

        public static string PropiedadesArchivo(string ID, string Ruta)
        {
            //Falta actualizar ZARCHIVOS no se si campos nuevos
            string a = "";
            string SQL = "";
            HttpContext context = HttpContext.Current;
            if (ID == "")
            {
                //Todos los Archivos
                System.Data.DataTable dt = context.Session["Archivos"] as System.Data.DataTable;
                foreach (DataRow fila in dt.Rows)
                {
                    if (fila["ZID"].ToString() == ID)
                    {
                        if (fila["ZTABLENAME"].ToString() != "")
                        {
                            Object Con = DBHelper.ExecuteScalarSQL(" SELECT SO.NAME FROM sysobjects SO WHERE SO.XTYPE = 'U' AND SO.NAME = '" + fila["ZTABLENAME"].ToString() + "' ", null);
                            if (Con == null)
                            { }
                            else
                            {
                                SQL = "SELECT COUNT(*) FROM " + fila["ZTABLENAME"].ToString();
                                a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) + "#";
                            }
                        }
                        if (fila["ZTABLEOBJ"].ToString() != "")
                        {
                            Object Con = DBHelper.ExecuteScalarSQL(" SELECT SO.NAME FROM sysobjects SO WHERE SO.XTYPE = 'U' AND SO.NAME = '" + fila["ZTABLEOBJ"].ToString() + "' ", null);
                            if (Con == null)
                            { }
                            else
                            {
                                SQL = "SELECT COUNT(*) FROM " + fila["ZTABLEOBJ"].ToString();
                                a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) + "#";
                                Ruta = Ruta + fila["ZTABLEOBJ"].ToString(); // fila["ZTABLEOBJ"].ToString();
                                a += Ruta + "#";
                            }
                        }
                        else
                        {
                            a += "#";
                            a += "#";
                        }
                        DirectoryInfo info = new DirectoryInfo(Ruta);
                        int count = 0;
                        long size = 0;
                        if (info.Exists == true)
                        {
                            count = info.EnumerateFiles().Count();
                            System.IO.DirectoryInfo dir = new DirectoryInfo(Ruta);
                            FileSystemInfo[] filelist = dir.GetFileSystemInfos();
                            FileInfo[] fileInfo;
                            fileInfo = dir.GetFiles("*", SearchOption.AllDirectories);
                            for (int i = 0; i < fileInfo.Length; i++)
                            {
                                try
                                {
                                    size += fileInfo[i].Length;
                                    //count = i;
                                }
                                catch { }
                            }
                        }
                        a += count + "#";
                        a += size + "#";
                        break;
                    }
                }
            }
            {
                //entonces El Archivo
                System.Data.DataTable dt = context.Session["Archivos"] as System.Data.DataTable;
                foreach (DataRow fila in dt.Rows)
                {
                    if (fila["ZID"].ToString() == ID)
                    {
                        if (fila["ZTABLENAME"].ToString() != "")
                        {
                            Object Con = DBHelper.ExecuteScalarSQL(" SELECT SO.NAME FROM sysobjects SO WHERE SO.XTYPE = 'U' AND SO.NAME = '" + fila["ZTABLENAME"].ToString() + "' ", null);
                            if (Con == null)
                            { }
                            else
                            {
                                SQL = "SELECT COUNT(*) FROM " + fila["ZTABLENAME"].ToString();
                                a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) + "#";
                            }
                        }
                        if (fila["ZTABLEOBJ"].ToString() != "")
                        {
                            Object Con = DBHelper.ExecuteScalarSQL(" SELECT SO.NAME FROM sysobjects SO WHERE SO.XTYPE = 'U' AND SO.NAME = '" + fila["ZTABLEOBJ"].ToString() + "' ", null);
                            if (Con == null)
                            { }
                            else
                            {
                                SQL = "SELECT COUNT(*) FROM " + fila["ZTABLEOBJ"].ToString();
                                a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) + "#";
                                Ruta = Ruta + fila["ZTABLEOBJ"].ToString(); // fila["ZTABLEOBJ"].ToString();
                                a += Ruta + "#";
                            }
                        }
                        else
                        {
                            a += "#";
                            a += "#";
                        }

                        //if (fila["ZTABLENAME"].ToString() != "") 
                        //{ 
                        //    SQL = "SELECT COUNT(*) FROM " + fila["ZTABLENAME"].ToString();
                        //    a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) +"#";
                        //}
                        //if (fila["ZTABLEOBJ"].ToString() != "") 
                        //{ 
                        //    SQL = "SELECT COUNT(*) FROM " + fila["ZTABLEOBJ"].ToString();
                        //    a += Convert.ToInt32(DBHelper.ExecuteScalarSQL(SQL, null)) + "#";
                        //    Ruta = Ruta + fila["ZTABLEOBJ"].ToString(); // fila["ZTABLEOBJ"].ToString();
                        //    a += Ruta + "#";
                        //}
                        //else
                        //{
                        //    a += "#";
                        //    a += "#";
                        //}
                        DirectoryInfo info = new DirectoryInfo(Ruta);
                        int count = 0;
                        long size = 0;
                        if (info.Exists == true)
                        {
                            count = info.EnumerateFiles().Count();
                            System.IO.DirectoryInfo dir = new DirectoryInfo(Ruta);
                            FileSystemInfo[] filelist = dir.GetFileSystemInfos();
                            FileInfo[] fileInfo;
                            fileInfo = dir.GetFiles("*", SearchOption.AllDirectories);
                            for (int i = 0; i < fileInfo.Length; i++)
                            {
                                try
                                {
                                    size += fileInfo[i].Length;
                                    //count = i;
                                }
                                catch { }
                            }
                        }
                        a += count + "#";
                        a += ((size /1024) /1024) + "#";
                        break;
                    }
                }
            }
            return a;
        }


        public static DataSet CargaMacros()
        {

            DataSet ds = new DataSet();
            HttpContext context = HttpContext.Current;
            string Var = context.Session["IDInforme"].ToString();

            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT DISTINCT A.ID_INFORME , A.ID_MACRO, A.CREATE_FECHA , A.MOD_FECHA, A.USUARIO, A.NOMBRE, A.ACTIVO, A.ORDEN,  A.DESCRIPCION ";
                Miquery += " FROM CONF_MACROS A ";
                Miquery += " WHERE A.ID_INFORME =" + Var;
                Miquery += " ORDER BY A.ID_MACRO ";


                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT DISTINCT A.ID_INFORME , A.ID_MACRO, A.CREATE_FECHA , A.MOD_FECHA, A.USUARIO, A.NOMBRE, A.ACTIVO, A.ORDEN,  A.DESCRIPCION ";
                //Miquery += " FROM USER_GEDESPOL.CONF_MACROS A ";
                //Miquery += " WHERE A.ID_INFORME =" + Var;
                //Miquery += " ORDER BY A.ID_MACRO ";


                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;

        }

        public static DataSet CargaMiMacro()
        {

            DataSet ds = new DataSet();

            HttpContext context = HttpContext.Current;
            string Var = context.Session["IDMacro"].ToString();
            string Var2 = context.Session["IDInforme"].ToString();
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT A.ID, A.ID_INFORME, A.ID_MACRO, A.COLUMNAIZQ , A.COLUMNADER ";
                Miquery += " FROM CONF_ID_MACRO A ";
                Miquery += " WHERE A.ID_MACRO =" + Var;
                Miquery += " AND A.ID_INFORME =" + Var2;
                Miquery += " ORDER BY A.ID ";

                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT A.ID, A.ID_INFORME, A.ID_MACRO, A.COLUMNAIZQ , A.COLUMNADER ";
                //Miquery += " FROM USER_GEDESPOL.CONF_ID_MACRO A ";
                //Miquery += " WHERE A.ID_MACRO =" + Var;
                //Miquery += " AND A.ID_INFORME =" + Var2;
                //Miquery += " ORDER BY A.ID ";

                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;

        }

        public static DataSet CargaInformesAccess()
        {

            DataSet ds = new DataSet();
                //DataSet ds = null;

                /****** Conexión a base de datos */
                // abriendo la conexión
                OleDbConnection cn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; DataSource=C:\\Proyecto\\Administracion\\Admin\\E-Lexis.mdb");

                string Miquery = "SELECT A.* ";
                Miquery += " FROM CONF_INFORMES A, ";
       
                OleDbDataAdapter comando = new OleDbDataAdapter(Miquery, cn);
                // cargando el dataset
                if (cn.State == 0) cn.Open();
                comando.Fill(ds);
                cn.Close();
                /* Fin conexión a base de datos ***/


            return ds;

        }

        public static DataSet CargaPermisos()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["UserID"]));


            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = "SELECT  A.ZID, A.ZDESCRIPCION , F.ZDESCRIPCION AS ZDESCGRUPO, G.ZDESCRIPCION AS ZDESCROL, E.ZDESCRIPCION AS DESCEVALUACION, ";
                Miquery += " F.ZID AS ZID_GRUPO, G.ZID AS ZID_ROL, E.ZID AS ZID_EVALUACION ";
                Miquery += " FROM ZUSUARIOS A,ZGRUPOUSUARIO B,ZGRUPOS F,ZROLGRUPOUSUARIO C, ";
                Miquery += " ZROLES G,ZEVALUACIONROLGRUPO D,ZEVALUACION E WHERE A.ZID =" + ID + " ";
                Miquery += " AND A.ZID = B.ZID_USUARIO ";
                Miquery += " AND B.ZID_GRUPO = C.ZID_GRUPO ";
                Miquery += " AND B.ZID_GRUPO = F.ZID ";
                Miquery += " AND C.ZID_ROL = G.ZID ";
                Miquery += " AND C.ZID_ROL = D.ZID_ROL ";
                Miquery += " AND D.ZID_EVALUACION = E.ZID ";
                Miquery += " ORDER BY A.ZID";

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
                //OracleParameter[] dbParams = new OracleParameter[0];
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = "SELECT  A.ZID, A.ZDESCRIPCION , F.ZDESCRIPCION AS ZDESCGRUPO, G.ZDESCRIPCION AS ZDESCROL, E.ZDESCRIPCION AS DESCEVALUACION, ";
                //Miquery += " F.ZID AS ZID_GRUPO, G.ZID AS ZID_ROL, E.ZID AS ZID_EVALUACION ";
                //Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A,USER_GEDESPOL.ZGRUPOUSUARIO B,USER_GEDESPOL.ZGRUPOS F,USER_GEDESPOL.ZROLGRUPOUSUARIO C, ";
                //Miquery += " USER_GEDESPOL.ZROLES G,USER_GEDESPOL.ZEVALUACIONROLGRUPO D,USER_GEDESPOL.ZEVALUACION E WHERE A.ZID =" + ID + " ";
                //Miquery += " AND A.ZID = B.ZID_USUARIO ";
                //Miquery += " AND B.ZID_GRUPO = C.ZID_GRUPO ";
                //Miquery += " AND B.ZID_GRUPO = F.ZID ";
                //Miquery += " AND C.ZID_ROL = G.ZID ";
                //Miquery += " AND C.ZID_ROL = D.ZID_ROL ";
                //Miquery += " AND D.ZID_EVALUACION = E.ZID ";
                //Miquery += " ORDER BY A.ZID";

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


        public static DataSet CargaLossUsusariosEval()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //AQUI
            DataSet MyDataSet = new DataSet();
            //if (Variables.configuracionDB == 0)
            //{
                SqlParameter[] dbParams = new SqlParameter[0];
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleParameter[] dbParams = new OracleParameter[0];
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet LosUsusariosSiEvaluado()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";
            //IGC  IG + IGC

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet LosUsusariosNoEvaluado()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZACTIVO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ,J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaLaEvalusaciondeUsuario()
        {
            //voy
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            string ID = context.Session["IDUsuario"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZID_USUARIO, ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZID_USUARIO, ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZID_USUARIO,";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , B.ZID_USUARIO,";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";



            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaEvalusaciondeUsuarioIR()
        {
            //voy
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            //string Ids = context.Session["MiCodigo"].ToString();
            string ID = context.Session["IDUsuario"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, J.ZNIVEL AS VER, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";



            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaMisUsusariosEval()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //string ID = context.Session["IDUsuario"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            //Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            //Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            //Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            //Miquery += " AND B.ZID_USUARIO = " + ID;
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";



            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaUsusariosIR()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //string ID = context.Session["IDUsuario"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND J.ZNIVEL = 4 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND J.ZNIVEL = 4 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION, J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND J.ZNIVEL = 4 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION , B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND J.ZNIVEL = 4 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";



            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaElUsusarioEval(string ID)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND B.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery +=  " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND B.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND B.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND B.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaMisUsusarioFlujo(string Estado)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " AND B.ZID_ESTADO = " + Estado;
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " AND B.ZID_ESTADO = " + Estado;
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " AND B.ZID_ESTADO = " + Estado;
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            Miquery += " AND B.ZID_ESTADO = " + Estado;

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet MisUsusariosNoEvaluado()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";
            //IGC  IG + IGC

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGCB' ";
            Miquery += " UNION ALL ";
            //IGB 
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ES_EVALUADO_POR_COD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGCA' ";


            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet MisUsusariosSiEvaluado()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaMiUserEval()
        {
            //Desempeño
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaMiEvalacionEvaluador()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA,  ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA,  ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaEvaluacionUsuario()
        {
            //Autoevacluacion
            //Busca la
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, J.ZNIVEL AS VER, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO , ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, J.ZNIVEL AS VER, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, J.ZNIVEL AS VER, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, J.ZNIVEL AS VER, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaMiEvalUser(string EL_ZID)
        {
            //Autoevaluacion
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();

            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND J.ZID_FLUJO = B.ZID_FLUJO ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO , J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND J.ZID_FLUJO = B.ZID_FLUJO ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";

            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND J.ZID_FLUJO = B.ZID_FLUJO ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";

            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID AS REGISTRO,B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, J.ZNIVEL AS VER, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,  B.ZID_FLUJO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";

            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND J.ZID_FLUJO = B.ZID_FLUJO ";
            Miquery += " AND B.ZID_FLUJO = 1 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";

            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaMiEvaluacion(string EL_ZID)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();

            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND( ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            //Miquery += " AND B.ZID_ESTADO = 0 ";
            Miquery += " AND B.ZID_ESTADO < 3 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC  IG + IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO < 3 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB 
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND(((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO < 3 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IG
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            //Miquery += " ROUND((((((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) * 0.8) + (ZA2ZC8 * 0.2) * 0.8) + (((ZA3ZC9 + ZA3ZC10) / 2) * 0.2) * 0.8) + (((ZA4ZC11 + ZA4ZC12) / 2 ) * 0.2),2) AS VALORIG, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,   A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,   ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, ";
            Miquery += " R.ZID AS ZIDSUPER, R.ZNOMBRE || ' ' || R.ZAPELLIDO1 || ' ' || R.ZAPELLIDO2 AS SUPERCOMPLETO ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS R ON A.SU_RESPONSABLE_SUPERIOR_ES = R.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.ZCOD = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO < 3 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_USUARIO = " + EL_ZID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet SusCompetencias(string id_flujo)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;
            string ID = context.Session["SuUEID"].ToString();
            StR2 = context.Session["SuUEIG"].ToString();

            if (context.Session["SuUEIG"].ToString() == "IG")
            {
                if (Variables.configuracionDB == 0)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6, ZA1ZC7 ";
                    StR2 += " FROM ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1 AND ZID_FLUJO = " + id_flujo;
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6, ZA1ZC7 ";
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1 AND ZID_FLUJO = " + id_flujo;
                }
            }

            if (context.Session["SuUEIG"].ToString() == "IGC")
            {
                if (Variables.configuracionDB == 0)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8 ";
                    StR2 += " FROM ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8 ";
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
            }

            if (context.Session["SuUEIG"].ToString() == "IGB")
            {
                if (Variables.configuracionDB == 0)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8,ZA3ZC9,ZA3ZC10 ";
                    StR2 += " FROM ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8,ZA3ZC9,ZA3ZC10 ";
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
            }

            if (context.Session["SuUEIG"].ToString() == "IGA")
            {
                if (Variables.configuracionDB == 0)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8,ZA3ZC9,ZA3ZC10,ZA4ZC11,ZA4ZC12 ";
                    StR2 += " FROM ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 = " SELECT ZID,to_char(ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,ZDESCRIPCION,ZID_ESTADO,ZID_FLUJO,ZACTIVO,ZID_USUARIO,ZID_RESPONSABLE,ZRESEVALUADO,to_char(ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,ZDESCEVALUADO,";
                    StR2 += " ZRESSUPERVISOR,ZRESEVALUADOR,to_char(ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,ZDESCSUPER,ZA1ZC1,ZA1ZC2,ZA1ZC3,ZA1ZC4,ZA1ZC5,ZA1ZC6,ZA1ZC7,ZA2ZC8,ZA3ZC9,ZA3ZC10,ZA4ZC11,ZA4ZC12 ";
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 WHERE ZID_USUARIO = " + ID + " AND ZACTIVO = 1  AND ZID_FLUJO = " + id_flujo;
                }
            }

            //DataSet ds = null;
            if (context.Session["SuUEIG"].ToString() != null)
            {
                if (Variables.configuracionDB == 0)
                {
                    SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    SqlCommand comando;
                    DataSet MyDataSet = new DataSet();

                    comando = new SqlCommand(StR2, cn);
                    comando.CommandType = CommandType.Text;

                    if (cn.State == 0) cn.Open();
                    SqlDataAdapter Da = new SqlDataAdapter(comando);
                    Da.Fill(ds);
                    //ListUser.Add(ds);
                    cn.Close();
                }
                else if (Variables.configuracionDB == 1)
                {
                    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                    //OracleCommand comando;
                    //DataSet MyDataSet = new DataSet();

                    //comando = new OracleCommand(StR2, cn);
                    //comando.CommandType = CommandType.Text;

                    //if (cn.State == 0) cn.Open();
                    //OracleDataAdapter Da = new OracleDataAdapter(comando);
                    //Da.Fill(ds);
                    ////ListUser.Add(ds);
                    //cn.Close();
                }

                return ds;
            }
            else
            {
                ds = null;
                return ds;
            }

        }


        public static DataSet MaximoRegistroEvaluacion()
        {
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";

            HttpContext context = HttpContext.Current;
            if (Variables.configuracionDB == 0)
            {
                StR2 = " SELECT MAX(A.ZID) AS ZID FROM ZEVAL2018 A ";

                //DataSet ds = null;

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //StR2 = " SELECT MAX(A.ZID) AS ZID FROM USER_GEDESPOL.ZEVAL2018 A ";

                ////DataSet ds = null;

                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CambioRegistroMiEvaluacion(string Id, string FLUJO)
        {
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";

            HttpContext context = HttpContext.Current;

            StR2 = " SELECT MAX(A.ZID) AS ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,A.ZDESCRIPCION,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE,A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
            StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,A.ZFECHASUPER,A.ZDESCSUPER,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9,A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12 ,   ";
            StR2 += " A.ZA1ZC1DETALLE,A.ZA1ZC2DETALLE,A.ZA1ZC3DETALLE,A.ZA1ZC4DETALLE,A.ZA1ZC5DETALLE,A.ZA1ZC6DETALLE,A.ZA1ZC7DETALLE,A.ZA2ZC8DETALLE,A.ZA3ZC9DETALLE,A.ZA3ZC10DETALLE,A.ZA4ZC11DETALLE,A.ZA4ZC12DETALLE, ";
            StR2 += " A.ZMODIFICADO_POR ,A.ZDESCEVALUADOR,A.ZTIPOMOD,to_char(A.ZFECHAEVALUADOR,'dd/mm/yyyy') AS ZFECHAEVALUADOR ";
            if (Variables.configuracionDB == 0)
            {
                StR2 += " FROM ZEVAL2018 A ";
            }
            else if (Variables.configuracionDB == 1)
            {
                StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
            }
            StR2 += " WHERE A.ZID_USUARIO =" + Id;
            StR2 += " AND A.ZACTIVO = 1 ";
            StR2 += " AND A.ZID_FLUJO =" + FLUJO;
            StR2 += " GROUP BY A.ZID,A.ZFECHAID,A.ZDESCRIPCION,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE,A.ZRESEVALUADO,A.ZFECHAEVALUADO,A.ZDESCEVALUADO,";
            StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,A.ZFECHASUPER,A.ZDESCSUPER,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9,A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12,";
            StR2 += " A.ZA1ZC1DETALLE,A.ZA1ZC2DETALLE,A.ZA1ZC3DETALLE,A.ZA1ZC4DETALLE,A.ZA1ZC5DETALLE,A.ZA1ZC6DETALLE,A.ZA1ZC7DETALLE,A.ZA2ZC8DETALLE,A.ZA3ZC9DETALLE,A.ZA3ZC10DETALLE,A.ZA4ZC11DETALLE,";
            StR2 += " A.ZA4ZC12DETALLE,  A.ZMODIFICADO_POR ,A.ZDESCEVALUADOR,A.ZTIPOMOD,ZFECHAEVALUADOR ";

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CambioRegistroEvaluacion(string Id)
        {
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";

            HttpContext context = HttpContext.Current;

            StR2 = " SELECT MAX(A.ZID) AS ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,A.ZDESCRIPCION,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE,A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
            StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,A.ZFECHASUPER,A.ZDESCSUPER,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9,A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12 ,   ";
            StR2 += " A.ZA1ZC1DETALLE,A.ZA1ZC2DETALLE,A.ZA1ZC3DETALLE,A.ZA1ZC4DETALLE,A.ZA1ZC5DETALLE,A.ZA1ZC6DETALLE,A.ZA1ZC7DETALLE,A.ZA2ZC8DETALLE,A.ZA3ZC9DETALLE,A.ZA3ZC10DETALLE,A.ZA4ZC11DETALLE,A.ZA4ZC12DETALLE, ";
            StR2 += " A.ZMODIFICADO_POR ,A.ZDESCEVALUADOR,A.ZTIPOMOD,to_char(A.ZFECHAEVALUADOR,'dd/mm/yyyy') AS ZFECHAEVALUADOR ";
            if (Variables.configuracionDB == 0)
            {
                StR2 += " FROM ZEVAL2018 A ";
            }
            else if (Variables.configuracionDB == 1)
            {
                StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
            }
            StR2 += " WHERE A.ZID_USUARIO =" + Id;
            StR2 += " AND A.ZACTIVO = 1 ";
            StR2 += " AND A.ZID_FLUJO = 2 ";
            StR2 += " GROUP BY A.ZID,A.ZFECHAID,A.ZDESCRIPCION,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE,A.ZRESEVALUADO,A.ZFECHAEVALUADO,A.ZDESCEVALUADO,";
            StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,A.ZFECHASUPER,A.ZDESCSUPER,A.ZA1ZC1,A.ZA1ZC2,A.ZA1ZC3,A.ZA1ZC4,A.ZA1ZC5,A.ZA1ZC6,A.ZA1ZC7,A.ZA2ZC8,A.ZA3ZC9,A.ZA3ZC10,A.ZA4ZC11,A.ZA4ZC12,";
            StR2 += " A.ZA1ZC1DETALLE,A.ZA1ZC2DETALLE,A.ZA1ZC3DETALLE,A.ZA1ZC4DETALLE,A.ZA1ZC5DETALLE,A.ZA1ZC6DETALLE,A.ZA1ZC7DETALLE,A.ZA2ZC8DETALLE,A.ZA3ZC9DETALLE,A.ZA3ZC10DETALLE,A.ZA4ZC11DETALLE,";
            StR2 += " A.ZA4ZC12DETALLE,  A.ZMODIFICADO_POR ,A.ZDESCEVALUADOR,A.ZTIPOMOD,ZFECHAEVALUADOR ";

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet DetalleCompetencia(string Id, string Columna, int id_Flujo)
        {
            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            string Query = Columna + "DETALLE";

            HttpContext context = HttpContext.Current;
            //if (Id == "1"){ Query = "A.ZA1ZC1";}
            //if (Id == "2") { Query = "A.ZA1ZC2"; }
            //if (Id == "3") { Query = "A.ZA1ZC3"; }
            //if (Id == "4") { Query = "A.ZA1ZC4"; }
            //if (Id == "5") { Query = "A.ZA1ZC5"; }
            //if (Id == "6") { Query = "A.ZA1ZC6"; }
            //if (Id == "7") { Query = "A.ZA2ZC7"; }
            //if (Id == "8") { Query = "A.ZA3ZC8"; }
            //if (Id == "9") { Query = "A.ZA3ZC9"; }
            //if (Id == "10") { Query = "A.ZA4ZC10"; }
            //if (Id == "11") { Query = "A.ZA4ZC11"; }

            StR2 = " SELECT DISTINCT(" + Query + ") AS ZCOMPETENCIADETALLE ";
            if (Variables.configuracionDB == 0)
            {
                StR2 += " FROM ZEVAL2018 A ";
                StR2 += " LEFT JOIN ZFESTADOS B  ON B.ZID = A.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                StR2 += " LEFT JOIN USER_GEDESPOL.ZFESTADOS B  ON B.ZID = A.ZID_ESTADO ";
            }
            StR2 += " WHERE A.ZID = " + Id;
            StR2 += " AND A.ZACTIVO = 1 ";
            StR2 += " AND A.ZID_ESTADO = B.ZID ";
            StR2 += " AND A.ZID_FLUJO =" + id_Flujo;


            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet LosDetallesCompetencias(string Id)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;

            if (context.Session["Carga_IG"].ToString() == "IG")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO, to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR, to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " A.ZA1ZC1DETALLE , A.ZA1ZC2DETALLE , A.ZA1ZC3DETALLE , A.ZA1ZC4DETALLE , A.ZA1ZC5DETALLE , A.ZA1ZC6DETALLE , A.ZA1ZC7DETALLE ,A.ZA2ZC8DETALLE , ";
                StR2 += " A.ZA3ZC9DETALLE ,A.ZA3ZC10DETALLE , A.ZA4ZC11DETALLE , A.ZA4ZC12DETALLE, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6,H.ZDESCRIPCION AS DESCA1ZC7 ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                }
                StR2 += " WHERE A.ZID = " + Id;

            }

            if (context.Session["Carga_IG"].ToString() == "IGC")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " A.ZA1ZC1DETALLE , A.ZA1ZC2DETALLE , A.ZA1ZC3DETALLE , A.ZA1ZC4DETALLE , A.ZA1ZC5DETALLE , A.ZA1ZC6DETALLE , A.ZA1ZC7DETALLE ,A.ZA2ZC8DETALLE , ";
                StR2 += " A.ZA3ZC9DETALLE ,A.ZA3ZC10DETALLE , A.ZA4ZC11DETALLE , A.ZA4ZC12DETALLE, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA2ZC7,I.ZDESCRIPCION AS DESCA2ZC8  ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                }
                StR2 += " WHERE A.ZID = " + Id;
            }

            if (context.Session["Carga_IG"].ToString() == "IGB")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " A.ZA1ZC1DETALLE , A.ZA1ZC2DETALLE , A.ZA1ZC3DETALLE , A.ZA1ZC4DETALLE , A.ZA1ZC5DETALLE , A.ZA1ZC6DETALLE , A.ZA1ZC7DETALLE ,A.ZA2ZC8DETALLE , ";
                StR2 += " A.ZA3ZC9DETALLE ,A.ZA3ZC10DETALLE , A.ZA4ZC11DETALLE , A.ZA4ZC12DETALLE, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7,I.ZDESCRIPCION AS DESCA2ZC8, ";
                StR2 += " J.ZDESCRIPCION AS DESCA3ZC9 ,K.ZDESCRIPCION AS DESCA3ZC10 ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                }
                StR2 += " WHERE A.ZID = " + Id;
            }

            if (context.Session["Carga_IG"].ToString() == "IGA")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " A.ZA1ZC1DETALLE , A.ZA1ZC2DETALLE , A.ZA1ZC3DETALLE , A.ZA1ZC4DETALLE , A.ZA1ZC5DETALLE , A.ZA1ZC6DETALLE , A.ZA1ZC7DETALLE ,A.ZA2ZC8DETALLE , ";
                StR2 += " A.ZA3ZC9DETALLE ,A.ZA3ZC10DETALLE , A.ZA4ZC11DETALLE , A.ZA4ZC12DETALLE, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7,I.ZDESCRIPCION AS DESCA2ZC8, ";
                StR2 += " J.ZDESCRIPCION AS DESCA3ZC9 ,K.ZDESCRIPCION AS DESCA3ZC10, L.ZDESCRIPCION AS DESCA4ZC11,M.ZDESCRIPCION AS DESCA4ZC12  ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION L  ON L.ZID = A.ZA4ZC11 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION M  ON M.ZID = A.ZA4ZC12 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION L  ON L.ZID = A.ZA4ZC11 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION M  ON M.ZID = A.ZA4ZC12 ";
                }
                StR2 += " WHERE A.ZID = " + Id;
            }

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet SusDetallesCompetencias(string Id)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;

            if (context.Session["Carga_IG"].ToString() == "IG")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO, to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR, to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7 ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                }
                StR2 += " WHERE A.ZID_USUARIO = " + Id;
                StR2 += " AND A.ZACTIVO = 1 ";
                StR2 += " AND A.ZID_FLUJO = 2 ";

            }

            if (context.Session["Carga_IG"].ToString() == "IGC")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7 , I.ZDESCRIPCION AS DESCA2ZC8  ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                }
                StR2 += " WHERE A.ZID_USUARIO = " + Id;
                StR2 += " AND A.ZACTIVO = 1 ";
                StR2 += " AND A.ZID_FLUJO = 2 ";
            }

            if (context.Session["Carga_IG"].ToString() == "IGB")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7 , I.ZDESCRIPCION AS DESCA2ZC8, ";
                StR2 += " J.ZDESCRIPCION AS DESCA3ZC9 ,K.ZDESCRIPCION AS DESCA3ZC10 ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                }
                StR2 += " WHERE A.ZID_USUARIO = " + Id;
                StR2 += " AND A.ZACTIVO = 1 ";
                StR2 += " AND A.ZID_FLUJO = 2 ";
            }

            if (context.Session["Carga_IG"].ToString() == "IGA")
            {
                StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, A.ZDESCRIPCION, B.ZDESCRIPCION AS ESTADO,A.ZID_ESTADO, A.ZID_FLUJO,A.ZACTIVO,A.ZID_USUARIO,A.ZID_RESPONSABLE, ";
                StR2 += " A.ZRESEVALUADO,to_char(A.ZFECHAEVALUADO,'dd/mm/yyyy') AS ZFECHAEVALUADO,A.ZDESCEVALUADO,  ";
                StR2 += " A.ZRESSUPERVISOR,A.ZRESEVALUADOR,to_char(A.ZFECHASUPER,'dd/mm/yyyy') AS ZFECHASUPER,A.ZDESCSUPER,B.ZDESCRIPCION AS DESCA1ZC1, C.ZDESCRIPCION AS  DESCA1ZC2, ";
                StR2 += " D.ZDESCRIPCION AS DESCA1ZC3,E.ZDESCRIPCION AS DESCA1ZC4,F.ZDESCRIPCION AS DESCA1ZC5,G.ZDESCRIPCION AS DESCA1ZC6 ,H.ZDESCRIPCION AS DESCA1ZC7 , I.ZDESCRIPCION AS DESCA2ZC8,";
                StR2 += " J.ZDESCRIPCION AS DESCA3ZC9, K.ZDESCRIPCION AS DESCA3ZC10 ,L.ZDESCRIPCION AS DESCA4ZC11, M.ZDESCRIPCION AS DESCA4ZC12  ";
                if (Variables.configuracionDB == 0)
                {
                    StR2 += " FROM ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION L  ON L.ZID = A.ZA4ZC11 ";
                    StR2 += " LEFT OUTER JOIN ZCALIFICACION M  ON M.ZID = A.ZA4ZC12 ";
                }
                else if (Variables.configuracionDB == 1)
                {
                    StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION B  ON B.ZID = A.ZA1ZC1 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION C  ON C.ZID = A.ZA1ZC2 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION D  ON D.ZID = A.ZA1ZC3 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION E  ON E.ZID = A.ZA1ZC4 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION F  ON F.ZID = A.ZA1ZC5 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION G  ON G.ZID = A.ZA1ZC6 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION H  ON H.ZID = A.ZA1ZC7 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION I  ON I.ZID = A.ZA2ZC8 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION J  ON J.ZID = A.ZA3ZC9 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION K  ON K.ZID = A.ZA3ZC10 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION L  ON L.ZID = A.ZA4ZC11 ";
                    StR2 += " LEFT OUTER JOIN USER_GEDESPOL.ZCALIFICACION M  ON M.ZID = A.ZA4ZC12 ";
                }
                StR2 += " WHERE A.ZID_USUARIO = " + Id;
                StR2 += " AND A.ZACTIVO = 1 ";
                StR2 += " AND A.ZID_FLUJO = 2 ";
            }

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet SusDetallesObservaciones(string Id, string El_ID)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;

            StR2 = " SELECT A.ZID,to_char(A.ZFECHAID,'dd/mm/yyyy') AS ZFECHAFLU, ";
            StR2 += " A." + Id + " AS ZOLUMNA, B.ZDESCRIPCION AS ZCALIFICACION, A." + Id + "DETALLE AS ZDESCRIPCIONFLU, 0 AS ZVER ";
            if (Variables.configuracionDB == 0)
            {
                StR2 += " FROM ZEVAL2018 A, ZCALIFICACION B ";
            }
            else if (Variables.configuracionDB == 1)
            {
                StR2 += " FROM USER_GEDESPOL.ZEVAL2018 A, USER_GEDESPOL.ZCALIFICACION B ";
            }
            StR2 += " WHERE A.ZID_USUARIO = " + El_ID;
            StR2 += " AND A." + Id + " = B.ZID  ";
            StR2 += " AND A.ZACTIVO = 0  ";
            StR2 += "  ORDER BY A.ZID ";


            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet LosFicherosCompetencia(string Id)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;
            if (Variables.configuracionDB == 0)
            {
                StR2 = " SELECT A.ZID, A.ZID_REGISTRO, to_char(A.ZFECHA,'dd/mm/yyyy') AS ZFECHA, A.ZDIRECTORIO, A.ZFICHERO, A.ZNOMBRE, A.ZID_USUARIO, A.ZLINK, A.ZLINK || A.ZFICHERO AS ZFILE ";
                StR2 += " FROM ZEVAL2018DOC A ";
                StR2 += " WHERE A.ZID_REGISTRO = " + Id;
                StR2 += " ORDER BY ZID ";

                //DataSet ds = null;

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //StR2 = " SELECT A.ZID, A.ZID_REGISTRO, to_char(A.ZFECHA,'dd/mm/yyyy') AS ZFECHA, A.ZDIRECTORIO, A.ZFICHERO, A.ZNOMBRE, A.ZID_USUARIO, A.ZLINK, A.ZLINK || A.ZFICHERO AS ZFILE ";
                //StR2 += " FROM USER_GEDESPOL.ZEVAL2018DOC A ";
                //StR2 += " WHERE A.ZID_REGISTRO = " + Id;
                //StR2 += " ORDER BY ZID ";

                ////DataSet ds = null;

                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet SusFicherosCompetencia(string Id)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            string StR2 = "";
            HttpContext context = HttpContext.Current;
            if (Variables.configuracionDB == 0)
            {
                StR2 = " SELECT A.ZID, A.ZID_REGISTRO, to_char(A.ZFECHA,'dd/mm/yyyy') AS ZFECHA, A.ZDIRECTORIO, A.ZFICHERO, A.ZNOMBRE, A.ZID_USUARIO, A.ZLINK, A.ZLINK || A.ZFICHERO AS ZFILE ";
                StR2 += " FROM ZEVAL2018DOC A ";
                StR2 += " WHERE A.ZID_USUARIO = " + Id;

                //DataSet ds = null;

                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                comando = new SqlCommand(StR2, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //StR2 = " SELECT A.ZID, A.ZID_REGISTRO, to_char(A.ZFECHA,'dd/mm/yyyy') AS ZFECHA, A.ZDIRECTORIO, A.ZFICHERO, A.ZNOMBRE, A.ZID_USUARIO, A.ZLINK, A.ZLINK || A.ZFICHERO AS ZFILE ";
                //StR2 += " FROM USER_GEDESPOL.ZEVAL2018DOC A ";
                //StR2 += " WHERE A.ZID_USUARIO = " + Id;

                ////DataSet ds = null;

                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //comando = new OracleCommand(StR2, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaSusUsusariosEvalSupervisor()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["IDUsuarioSuper"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    //OracleCommand comando;
            //    //DataSet MyDataSet = new DataSet();
            //}

            //string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, A.ZNOMBRE, A.ZAPELLIDO1, A.ZAPELLIDO2, ";
            //Miquery += " A.ZPASSWORD, A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, A.ZLETRA  ,A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO, ";
            //Miquery += " A.ZFORMA_PROV ,A.ZESC_CAT_GRUPO ,A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,A.NIVEL_DE_EVALUACION , ";
            //Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA ";
            //Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
            //Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            //Miquery += " ORDER BY A.ZID";

            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO,  ";
            //Miquery += " ((ZA1ZC1 + ZA1ZC2 + ZA1ZC3 + ZA1ZC4 + ZA1ZC5 + ZA1ZC6 + ZA1ZC7) / 7) AS VALORIG, ZA2ZC8 AS VALORIGB,((ZA3ZC9 + ZA3ZC10) / 2) AS VALORIGB,";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO, ";
            Miquery += " A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO, ";
            Miquery += " A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,A.NIVEL_DE_EVALUACION , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1  ";
            Miquery += " ORDER BY A.ZID ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet CargaLosUsusariosEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;

            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            string Iduser = context.Session["IDUsuarioSuper"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , J.ZNIVEL AS VER, B.ZID_FLUJO, ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO =2";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , J.ZNIVEL AS VER, B.ZID_FLUJO, ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES= '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO =2";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , J.ZNIVEL AS VER, B.ZID_FLUJO, ";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO =2";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IGA
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , J.ZNIVEL AS VER, B.ZID_FLUJO, ";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,  to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO =2";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaMisUsusariosEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, B.ZID_USUARIO, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, B.ZID_USUARIO,";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, B.ZID_USUARIO,";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, B.ZID_USUARIO,";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION , J.ZNIVEL AS VER ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaElUsusarioEvalSuper(string ID)
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZID_FLUJO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 as EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION, B.ZID AS ZID_EVALUACION, J.ZNIVEL ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_FLUJO = J.ZID_FLUJO ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND A.ZID = " + ID + " ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet LosUsusariosNoEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION ,  A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES= '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IGA
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,  to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZID_ESTADO = 1 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            Miquery += " ORDER BY ID_USUARIO, ESTADO ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet MisUsusariosNoEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";


            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";
            //IGB
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet LosUsusariosSiEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG
            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA ,";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO , ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            //Miquery += " AND B.ZID_ESTADO <> 1 ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO >=3";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            //IGC
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION ,A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,   A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES= '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO >=3";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            //IGB
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA,  ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO >=3";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            //IGA
            Miquery += " UNION ALL ";
            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS ORDEN, 1 AS RD, B.ZACTIVO, B.ZID_ESTADO,  B.ZID_ESTADO AS ESTADO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, ";
            Miquery += " B.ZA1ZC1 , B.ZA1ZC2 , B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 ,B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZHORA, ";
            Miquery += " B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO, B.ZID_USUARIO, B.ZID_USUARIO AS ID_USUARIO ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , ";
            Miquery += " A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 ,  A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA,  to_char(B.ZFECHAID,'dd/mm/yyyy') AS ZFECHAID,  ";
            Miquery += " C.ZDESCRIPCION AS RESSUPERVISOR, D.ZDESCRIPCION AS RESEVALUADOR, E.ZDESCRIPCION AS RESEVALUADO ,    ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A  LEFT OUTER JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A  LEFT OUTER JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADO C ON B.ZRESEVALUADO = C.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGEVALUADOR D ON B.ZRESEVALUADOR = D.ZID ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZASIGSUPERVISOR E ON B.ZRESSUPERVISOR = E.ZID ";
                Miquery += " INNER JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " RIGHT OUTER JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_ESTADO >=3";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";
            Miquery += " ORDER BY ID_USUARIO, ESTADO ";
            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet MisUsusariosSiEvalSuper()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            //if (Variables.configuracionDB == 0)
            //{
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
            //}
            //else if (Variables.configuracionDB == 1)
            //{
            //    OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
            //    OracleCommand comando;
            //    DataSet MyDataSet = new DataSet();
            //}
            //IG

            string Miquery = " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IG' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA,  ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGC' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGB' ";
            Miquery += " UNION ALL ";

            Miquery += " SELECT DISTINCT A.ZID, B.ZID AS REGISTRO, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, B.ZA1ZC1 , B.ZA1ZC2 , ";
            Miquery += " B.ZA1ZC3 , B.ZA1ZC4 , B.ZA1ZC5 , B.ZA1ZC6 , B.ZA1ZC7 , B.ZA2ZC8 , B.ZA3ZC9 , B.ZID_FLUJO, B.ZHORA, ";
            Miquery += " B.ZA3ZC10 , B.ZA4ZC11 , B.ZA4ZC12, '0' AS VALORIG, B.ZID_ESTADO, J.ZDESCRIPCION AS FESTADO,  A.ZCOD, A.ZDNI AS ZIDENTIFICACION , A.ZPUESTO_TRABAJO AS ZCARGO,   ";
            Miquery += " A.ZPUESTO_TRABAJO , A.ZFORMA_PROV, A.ZESC_CAT_GRUPO,   A.ZOTRAS ,A.ZCENTRO_DIRECTIVO ,A.PROVINCIA ,A.PLANTILLA , A.NIVEL_DE_EVALUACION , A.NIVEL_DE_EVALUACION2 AS EVALUACION2 , ";
            Miquery += " A.RANGO_DE_SUPERVISION ,A.SU_RESPONSABLE_SUPERIOR_ES,A.ES_EVALUADO_POR_COD, A.ZAREA, to_char(B.ZFECHAID, 'dd/mm/yyyy') AS ZFECHAID, ";
            Miquery += " F.ZID, F.ZNOMBRE || ' ' || F.ZAPELLIDO1 || ' ' || F.ZAPELLIDO2 AS FCOMPLETO, B.ZDESCRIPCION AS ZOBSERVACION ";
            if (Variables.configuracionDB == 0)
            {
                Miquery += " FROM ZUSUARIOS A ";
                Miquery += " LEFT JOIN ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            else if (Variables.configuracionDB == 1)
            {
                Miquery += " FROM USER_GEDESPOL.ZUSUARIOS A ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZEVAL2018 B  ON A.ZID = B.ZID_USUARIO ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZUSUARIOS F ON A.ES_EVALUADO_POR_COD = F.ZCOD ";
                Miquery += " LEFT JOIN USER_GEDESPOL.ZFESTADOS J  ON J.ZID = B.ZID_ESTADO ";
            }
            Miquery += " WHERE A.SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "' ";
            Miquery += " AND B.ZACTIVO = 1 ";
            Miquery += " AND B.ZID_FLUJO = 2 ";
            Miquery += " AND j.ZID_FLUJO = 2 ";
            Miquery += " AND A.NIVEL_DE_EVALUACION = 'IGA' ";

            if (Variables.configuracionDB == 0)
            {
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaLosFlujos()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = " SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND, ZID_FLUJO ";
                Miquery += " FROM ZFESTADOS  ";
                Miquery += " ORDER BY ZID";

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
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = " SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZPREVIUS, ZNEXT, ZALTERNATIVE, ZEND, ZID_FLUJO ";
                //Miquery += " FROM USER_GEDESPOL.ZFESTADOS  ";
                //Miquery += " ORDER BY ZID";

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

        public static DataSet CuentaArchivos()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MiCodigo"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                string Miquery = " SELECT COUNT(ZID) AS CUANTOS ";
                Miquery += " FROM ZARCHIVOS  ";

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
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //string Miquery = " SELECT DISTINCT ZAREA ";
                //Miquery += " FROM USER_GEDESPOL.ZUSUARIOS  ";
                //Miquery += " WHERE ES_EVALUADO_POR_COD = '" + Ids + "'";

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

        public static DataSet GargaDatosUserEval()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            string Ids = context.Session["MisUsuariosEvaluacion"].ToString();
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT DISTINCT  A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, A.ZNOMBRE, A.ZAPELLIDO1, A.ZAPELLIDO2, ";
                Miquery += "A.ZIDENTIFICACION, A.ZDESCRIPCION, B.ZDESCRIPCION AS ZCARGO, A.ZNIVEL, C.ZDESCRIPCION AS ZDEPARTAMENTO, D.ZDESCRIPCION AS ZESTADO, E.ZDESCRIPCION AS ZDESTINO ";
                Miquery += "  FROM ZUSUARIOS A, ZCARGOS B,  ZDEPARTAMENTOS C, ZESTADOS D, ZDESTINOS E ";
                Miquery += " WHERE A.ZID in (" + Ids + ") ";
                Miquery += " AND A.ZCARGO = B.ZID AND A.ZDEPARTAMENTO = C.ZID AND A.ZESTADO = D.ZID AND A.ZDESTINO = E.ZID  ORDER BY A.ZID ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT DISTINCT  A.ZID, A.ZNOMBRE || ' ' || A.ZAPELLIDO1 || ' ' || A.ZAPELLIDO2 AS ZCOMPLETO, A.ZNOMBRE, A.ZAPELLIDO1, A.ZAPELLIDO2, ";
                //Miquery += "A.ZIDENTIFICACION, A.ZDESCRIPCION, B.ZDESCRIPCION AS ZCARGO, A.ZNIVEL, C.ZDESCRIPCION AS ZDEPARTAMENTO, D.ZDESCRIPCION AS ZESTADO, E.ZDESCRIPCION AS ZDESTINO ";
                //Miquery += "  FROM USER_GEDESPOL.ZUSUARIOS A, USER_GEDESPOL.ZCARGOS B,  USER_GEDESPOL.ZDEPARTAMENTOS C, USER_GEDESPOL.ZESTADOS D, USER_GEDESPOL.ZDESTINOS E ";
                //Miquery += " WHERE A.ZID in (" + Ids + ") ";
                //Miquery += " AND A.ZCARGO = B.ZID AND A.ZDEPARTAMENTO = C.ZID AND A.ZESTADO = D.ZID AND A.ZDESTINO = E.ZID  ORDER BY A.ZID ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;
        }

        public static DataSet GargaUsersEvaluacion()
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            int Ids = Convert.ToInt32((context.Session["IDUsersEvaluacion"]));
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT DISTINCT ZID_USUARIO FROM ZEVAL2018 ";
                Miquery += " WHERE ZACTIVO = 1 ORDER BY ZID_USUARIO";
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
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT DISTINCT ZID_USUARIO FROM USER_GEDESPOL.ZEVAL2018 ";
                //Miquery += " WHERE ZACTIVO = 1 ORDER BY ZID_USUARIO";
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

        public static DataSet CargaArea()
        {

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZOBLIGATORIA, ZCLASE FROM ZAREAS ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZOBLIGATORIA, ZCLASE FROM USER_GEDESPOL.ZAREAS ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }


        public static DataSet CargaEvaluacion()
        {


            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZACTIVO, ZDETALLE, ZTABLENAME, ZTABLEDOC FROM ZEVALUACION ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT ZID, ZDESCRIPCION, ZTIPOEVALUACION, ZPESO, ZOBSERVACION, ZACTIVO, ZDETALLE, ZTABLENAME, ZTABLEDOC FROM USER_GEDESPOL.ZEVALUACION ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

 

        public static DataSet CargaRelacionesAreas()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["IDArea"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCEVAL, A.ZTIPOEVALUACION AS ZTIPEVAL, A.ZPESO AS ZPESOEVAL, A.ZOBSERVACION AS ZOBSEVAL, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREAEVALUACION C  ";
                Miquery += " WHERE A.ZID = C.ZID_EVALUACION ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID = " + ID;
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_COMPETENCIA, A.ZDESCRIPCION AS ZDESCEVAL, A.ZTIPOEVALUACION AS ZTIPEVAL, A.ZPESO AS ZPESOEVAL, A.ZOBSERVACION AS ZOBSEVAL, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREAEVALUACION C  ";
                //Miquery += " WHERE A.ZID = C.ZID_EVALUACION ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID = " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        public static DataSet GuardaArea(string SQL, int ID)
        {

            DataSet ds = null;
            //List<Usuario> ListUser = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = SQL;
                //WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = SQL;
                ////WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //cn.Close();
            }

            //Miquery = "UPDATE USER_GEDESPOL.ZARCHIVOS SET ZROW = " + (ID + 1) + " WHERE ZID = " + ID + ";";
            ////WHERE ZID = " + ID.ToString() + " ORDER BY ZID";
            //comando = new OracleCommand(Miquery, cn);
            //comando.CommandType = CommandType.Text;

            //if (cn.State == 0) cn.Open();
            //comando.ExecuteNonQuery();
            //cn.Close();

            return ds;
        }

        //seccion relaciones
        public static DataSet CargaNodosArchivo(int Ids, int Profile)
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;

            DataSet ds = new DataSet();
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;

                string STR = " SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, (SELECT COUNT(*) ";
                STR += "  FROM ZARCHIVOCAMPO WHERE ZID_ARCHIVO = AA.ZID) AS zHIJOS ";
                STR += "  FROM ZARCHIVOS AA ";
                STR += "  WHERE ZID = " + Ids;
                STR += "  AND ZNIVEL <=" + Profile;


                comando = new SqlCommand(STR, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;

                //string STR = " SELECT ZID, ZNOMBRE, ZDESCRIPCION, ZTIPO, (SELECT COUNT(*) ";
                //STR += "  FROM USER_GEDESPOL.ZARCHIVOCAMPO WHERE ZID_ARCHIVO = AA.ZID) AS zHIJOS ";
                //STR += "  FROM USER_GEDESPOL.ZARCHIVOS AA ";
                //STR += "  WHERE ZID = " + Ids;
                //STR += "  AND ZNIVEL <=" + Profile;


                //comando = new OracleCommand(STR, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;

        }

        public static System.Data.DataTable CargaNodos(int Ids, int Profile)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));

                SqlCommand comando;
                SqlDataAdapter Da;
                
                //DataSet MyDataSet = new DataSet();

                //select Row_Id, Category_Name as Name,
                //(select count(*) FROM dbo.Category WHERE Category_Father_Id =sc.Row_Id) childnodecount 
                //FROM Category sc 
                //where Category_Father_Id = 0"
                //SELECT ZID_ARCHIVO, (SELECT COUNT(*) FROM ZARCHIVOS WHERE ZID_ARCHIVO = A.ZID_ARCHIVO)
                //FROM ZARCHIVOS A
                //WHERE ZROOT = VARIABLE(ID);

                comando = new SqlCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_NODOS", cn);
                comando.CommandType = CommandType.StoredProcedure;

                // Abrimos conexión
                if (cn.State == 0) cn.Open();

                SqlCommandBuilder.DeriveParameters(comando);

                // Le pasamos los parametros (si existe cursor para el primero se pasa un null
                comando.Parameters[0].Value = Ids; // ROOT
                comando.Parameters[1].Value = Profile; // ROOT
                                                       //comando.Parameters[1].Value = null;

                // Ejecutamos y llenamo Ds
                Da = new SqlDataAdapter(comando);
                Da.Fill(dt);

                //Cerrar conex
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));

                //OracleCommand comando;
                //OracleDataAdapter Da;
                //DataTable dt = new DataTable();
                ////DataSet MyDataSet = new DataSet();

                ////select Row_Id, Category_Name as Name,
                ////(select count(*) FROM dbo.Category WHERE Category_Father_Id =sc.Row_Id) childnodecount 
                ////FROM Category sc 
                ////where Category_Father_Id = 0"
                ////SELECT ZID_ARCHIVO, (SELECT COUNT(*) FROM ZARCHIVOS WHERE ZID_ARCHIVO = A.ZID_ARCHIVO)
                ////FROM ZARCHIVOS A
                ////WHERE ZROOT = VARIABLE(ID);

                //comando = new OracleCommand("USER_GEDESPOL.PACK_GEDESPOL.Z_GET_NODOS", cn);
                //comando.CommandType = CommandType.StoredProcedure;

                //// Abrimos conexión
                //if (cn.State == 0) cn.Open();

                //OracleCommandBuilder.DeriveParameters(comando);

                //// Le pasamos los parametros (si existe cursor para el primero se pasa un null
                //comando.Parameters[0].Value = Ids; // ROOT
                //comando.Parameters[1].Value = Profile; // ROOT
                //                                       //comando.Parameters[1].Value = null;

                //// Ejecutamos y llenamo Ds
                //Da = new OracleDataAdapter(comando);
                //Da.Fill(dt);

                ////Cerrar conex
                //cn.Close();
            }

            return dt;

        }

        public static DataSet TVCargaCampos(int Ids, int Profile)
        {

            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;

            DataSet ds = new DataSet();
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;

                string STR = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION,(SELECT COUNT(*) - 1 ";
                STR += "  FROM ZCAMPOS WHERE ZID = A.ZID ) AS zHIJOS ";
                STR += "  FROM ZCAMPOS A, USER_GEDESPOL.ZARCHIVOCAMPO B ";
                STR += "  WHERE B.ZID_ARCHIVO = " + Ids;
                STR += "  AND A.ZNIVEL <=" + Profile;
                STR += "  AND A.ZID = B.ZID_CAMPO ";

                comando = new SqlCommand(STR, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                Da.Fill(ds);
                //ListUser.Add(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;

                //string STR = " SELECT A.ZID, A.ZNOMBRE, A.ZDESCRIPCION,(SELECT COUNT(*) - 1 ";
                //STR += "  FROM USER_GEDESPOL.ZCAMPOS WHERE ZID = A.ZID ) AS zHIJOS ";
                //STR += "  FROM USER_GEDESPOL.ZCAMPOS A, USER_GEDESPOL.ZARCHIVOCAMPO B ";
                //STR += "  WHERE B.ZID_ARCHIVO = " + Ids;
                //STR += "  AND A.ZNIVEL <=" + Profile;
                //STR += "  AND A.ZID = B.ZID_CAMPO ";

                //comando = new OracleCommand(STR, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //Da.Fill(ds);
                ////ListUser.Add(ds);
                //cn.Close();
            }

            return ds;

        }


        public static DataSet CargaRelacionesEvaluacion()
        {
            //PASAR PARAMETRO SESSION
            HttpContext context = HttpContext.Current;
            int ID = Convert.ToInt32((context.Session["IDArea"]));

            DataSet ds = null;

            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQL"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();
                string Miquery = "SELECT A.ZID AS ZID_EVALUACION, A.ZDESCRIPCION AS ZDESCEVAL, A.ZTIPOEVALUACION AS ZTIPEVAL, A.ZPESO AS ZPESOEVAL, A.ZOBSERVACION AS ZOBSEVAL, ";
                Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                Miquery += " FROM ZCOMPETENCIAS A, ZAREAS B, ZAREAEVALUACION C  ";
                Miquery += " WHERE A.ZID = C.ZID_EVALUACION ";
                Miquery += " AND  B.ZID = C.ZID_AREA   ";
                Miquery += " AND B.ZID = " + ID;
                Miquery += " ORDER BY C.ZORDEN ";
                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);

                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();
                //string Miquery = "SELECT A.ZID AS ZID_EVALUACION, A.ZDESCRIPCION AS ZDESCEVAL, A.ZTIPOEVALUACION AS ZTIPEVAL, A.ZPESO AS ZPESOEVAL, A.ZOBSERVACION AS ZOBSEVAL, ";
                //Miquery += " B.ZID AS ZID_AREA, B.ZDESCRIPCION AS ZDESAREA, B.ZTIPOEVALUACION AS ZTIPEVALAREA, B.ZPESO AS ZPESOAREA, B.ZOBSERVACION AS ZOBSAREA  ";
                //Miquery += " FROM USER_GEDESPOL.ZCOMPETENCIAS A, USER_GEDESPOL.ZAREAS B, USER_GEDESPOL.ZAREAEVALUACION C  ";
                //Miquery += " WHERE A.ZID = C.ZID_EVALUACION ";
                //Miquery += " AND  B.ZID = C.ZID_AREA   ";
                //Miquery += " AND B.ZID = " + ID;
                //Miquery += " ORDER BY C.ZORDEN ";
                //comando = new OracleCommand(Miquery, cn);
                //comando.CommandType = CommandType.Text;

                //if (cn.State == 0) cn.Open();
                //OracleDataAdapter Da = new OracleDataAdapter(comando);
                //ds = new DataSet();
                //Da.Fill(ds);

                //cn.Close();
            }

            return ds;
        }

        //FIN EVALUACIONES
        public static DataSet CargaEavaluado()
        {

            DataSet ds = new DataSet();
            //List<Usuario> ListUser = null;
            HttpContext context = HttpContext.Current;
            //Prueba
            string Ids = "";
            //Prueba para cero Ids = "C";
            Ids = context.Session["MiCodigo"].ToString();
            string ID = context.Session["QuienSoy"].ToString();
            string Miquery = "";
            //DataSet ds = null;
            if (Variables.configuracionDB == 0)
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionSQLGold"));
                SqlCommand comando;
                DataSet MyDataSet = new DataSet();

                Miquery = " SELECT COUNT(DISTINCT(CodigoTrabajador)) as CUANTOS, CodigoTrabajador, NIF, Nombre, Apellido1, Apellido2 ";
                Miquery += " FROM TCTrabajadores  ";
                Miquery += " WHERE CodigoTrabajador = '" + Ids + "'";


                comando = new SqlCommand(Miquery, cn);
                comando.CommandType = CommandType.Text;

                if (cn.State == 0) cn.Open();
                SqlDataAdapter Da = new SqlDataAdapter(comando);
                ds = new DataSet();
                Da.Fill(ds);
                cn.Close();
            }
            else if (Variables.configuracionDB == 1)
            {
                //OracleConnection cn = new OracleConnection(ConfigurationManager.AppSettings.Get("ConnectionStringOracle"));
                //OracleCommand comando;
                //DataSet MyDataSet = new DataSet();

                //if (context.Session["QuienSoy"].ToString() == "0")
                //{
                //    Miquery = " SELECT COUNT(ZID) AS CUANTOS ";
                //    Miquery += " FROM USER_GEDESPOL.ZUSUARIOS  ";
                //    Miquery += " WHERE ES_EVALUADO_POR_COD = '" + Ids + "'";
                //}
                //else
                //{
                //    Miquery = " SELECT COUNT(ZID) AS CUANTOS ";
                //    Miquery += " FROM USER_GEDESPOL.ZUSUARIOS  ";
                //    Miquery += " WHERE SU_RESPONSABLE_SUPERIOR_ES = '" + Ids + "'";
                //}


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

    }
}


//--CONSULTA AREAS COMPETENCIAS ASIGNADAS A EVALUACION
//SELECT A.ZID AS ZID_AREA, A.ZOBSERVACION, B.ZID AS ZID_COMPETENCIA, B.ZDESCRIPCION, B.ZOBSERVACION, D.ZID AS ZID_EVALUACION, D.ZDESCRIPCION, D.ZTABLENAME, D.ZTABLEDOC
//FROM ZAREAS A, ZCOMPETENCIAS B, ZAREACOMPETENCIA C, ZEVALUACION D
//WHERE A.ZID = C.ZID_AREA
//AND B.ZID = C.ZID_COMPETENCIA
//AND D.ZID = C.ZID_EVALUACION
//AND D.ZID = 2 --evaluacion
//ORDER BY D.ZID, A.ZID, B.ZID;

//--CONSULTA EVALUCION POR USUARIO
//SELECT A.ZID, A.ZDESCRIPCION , A.ZA1ZC1, A.ZA1ZC2, A.ZA1ZC3, A.ZA1ZC4, A.ZA2ZC1,
//  A.ZA2ZC2, A.ZA2ZC3, A.ZA3ZC1, A.ZA3ZC2,
//   A.ZA3ZC4, A.ZID_ESTADO, A.ZID_FLUJO, A.ZACTIVO, A.ZID_USUARIO, A.ZID_RESPONSABLE, B.ZNOMBRE, B.ZAPELLIDO1, B.ZAPELLIDO2, C.ZDESCRIPCION AS ZCARGO
//  FROM ZEVAL2018 A, ZUSUARIOS B, ZCARGOS C
//  WHERE A.ZID_RESPONSABLE = B.ZID
//  AND A.ZID_USUARIO = 3
//  AND B.ZCARGO = C.ZID
//  AND A.ZACTIVO = 1