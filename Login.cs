using Satelite.Models;
using System;
using System.Data; 
using System.Linq; 
using System.Web;

namespace Satelite
{
    public partial class Login
    {
        private static bool IsDateCheckOk()
        {
            // (Misma lógica de comprobación de fecha que antes)
            try { /* ... tu código de fecha ... */ return true; } // Simplificado para el ejemplo
            catch { /* Loggear error */ return false; }
        }

        // --- MÉTODO NUEVO CON ENTITY FRAMEWORK ---
        // Devuelve la entidad ZUSUARIOS o null. ¡Implementar HASHING para password!
        public static ZUSUARIOS ValidarLoginEF(string sUserName, string sPassword)
        {
            if (!IsDateCheckOk()) return null;

            HttpContext context = HttpContext.Current;
            ZUSUARIOS usuario = null;

            using (var db = new Satelite_BackupEntities()) // Usa el nombre correcto de tu DbContext
            {
                try
                {
                    // Intento 1: Por Alias y Password (¡NECESITA HASHING!)
                    usuario = db.ZUSUARIOS
                                .FirstOrDefault(u => u.ZALIAS == sUserName && u.ZPASSWORD == sPassword);

                    if (usuario != null)
                    {
                        context.Session["Session"] = "8"; // Marca para posible segundo paso (Captcha)
                    }
                    else
                    {
                        // Intento 2: Por Alias y Llave (¡NECESITA HASHING si es sensible!)
                        string registroLlave = context.Session["Registro"]?.ToString();
                        if (!string.IsNullOrEmpty(registroLlave))
                        {
                            usuario = db.ZUSUARIOS
                                        .FirstOrDefault(u => u.ZALIAS == sUserName && u.ZLLAVE == registroLlave);
                            // Si se encuentra por llave, puede que no necesite marcar Session["Session"] = "8"
                            // if (usuario != null) context.Session["Session"] = "1"; // O lo que sea
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"ERROR en ValidarLoginEF: {ex.ToString()}");
                    return null; // Devuelve null en caso de error
                }
            }
            return usuario;
        }


        // --- MÉTODO PUENTE TEMPORAL: ValidarLogin (Devuelve DataSet) ---
        // Llama a ValidarLoginEF y construye un DataSet para compatibilidad.
        public static DataSet ValidarLogin(string sUserName, string sPassword)
        {
            ZUSUARIOS usuario = ValidarLoginEF(sUserName, sPassword);

            // Si el usuario no es válido o hubo error, devuelve un DataSet vacío o null
            if (usuario == null)
            {
                // Decide si devolver null o un DataSet vacío según cómo lo manejen los callers
                // return null;
                return new DataSet(); // Más seguro para evitar NullReferenceException en los callers
            }

            // Construye el DataSet similar al original
            DataSet ds = new DataSet("ResultadoLogin");
            DataTable dt = new DataTable("UsuarioInfo");
            ds.Tables.Add(dt);

            // Añade las columnas que devolvía tu consulta original *y que se usan*
            // ¡Asegúrate de incluir TODAS las columnas que necesitan los archivos que llaman a este método!
            dt.Columns.Add("ZID", typeof(int));
            dt.Columns.Add("ZCODIGO", typeof(string));
            dt.Columns.Add("ZALIAS", typeof(string));
            dt.Columns.Add("ZPASSWORD", typeof(string)); // Debería ser el hash
            dt.Columns.Add("ZROOT", typeof(decimal));
            dt.Columns.Add("ZKEY", typeof(decimal));
            dt.Columns.Add("ZNIVEL", typeof(decimal));
            // --- Columnas que venían de ZLLAVES ---
            // Necesitas obtener estos datos si los llamadores los usan.
            // Esto podría requerir ajustar ValidarLoginEF para incluir ZLLAVES o hacer otra consulta.
            // Por ahora, las añadimos como null o valor por defecto si no las tienes fácilmente.
            dt.Columns.Add("ZID_TABLA", typeof(int));    // Placeholder
            dt.Columns.Add("ZID_REGISTRO", typeof(int)); // Placeholder
            //------------------------------------
            dt.Columns.Add("ZLLAVE", typeof(string));    // Debería ser el hash
            dt.Columns.Add("ZDEFAULT", typeof(string));


            // Crea y llena la fila
            DataRow dr = dt.NewRow();
            dr["ZID"] = usuario.ZID;
            dr["ZCODIGO"] = (object)usuario.ZCODIGO ?? DBNull.Value;
            dr["ZALIAS"] = (object)usuario.ZALIAS ?? DBNull.Value;
            dr["ZPASSWORD"] = (object)usuario.ZPASSWORD ?? DBNull.Value; // Hash
            dr["ZROOT"] = (object)usuario.ZROOT ?? DBNull.Value;
            dr["ZKEY"] = (object)usuario.ZKEY ?? DBNull.Value;
            dr["ZNIVEL"] = (object)usuario.ZNIVEL ?? DBNull.Value;

            // --- Placeholders para ZLLAVES ---
            // Si necesitas estos datos, tendrás que cargarlos. Ejemplo:
            // var llaveInfo = db.ZLLAVES.FirstOrDefault(l => l.ZID == usuario.ZKEY);
            // dr["ZID_TABLA"] = (llaveInfo != null) ? (object)llaveInfo.ZID_TABLA : DBNull.Value;
            // dr["ZID_REGISTRO"] = (llaveInfo != null) ? (object)llaveInfo.ZID_REGISTRO : DBNull.Value;
            dr["ZID_TABLA"] = DBNull.Value;    // Valor por defecto temporal
            dr["ZID_REGISTRO"] = DBNull.Value; // Valor por defecto temporal
            // -----------------------------------

            dr["ZLLAVE"] = (object)usuario.ZLLAVE ?? DBNull.Value;     // Hash
            dr["ZDEFAULT"] = (object)usuario.ZDEFAULT ?? DBNull.Value;

            dt.Rows.Add(dr);

            return ds;
        }

        // --- MÉTODO PUENTE TEMPORAL: ValidarUser (Devuelve DataSet) ---
        // Asume que ValidarUser era una versión simplificada, reutiliza ValidarLoginEF
        // y devuelve un DataSet con menos columnas si era diferente.
        
        public static DataSet ValidarUser(string sUserName, string sPassword)
        {
            ZUSUARIOS usuario = ValidarLoginEF(sUserName, sPassword); // Reutiliza la lógica principal

            if (usuario == null)
            {
                return new DataSet(); // Devuelve DataSet vacío
            }

            // Construye DataSet solo con las columnas que usaba ValidarUser
            DataSet ds = new DataSet("ResultadoUser");
            DataTable dt = new DataTable("UsuarioBasico");
            ds.Tables.Add(dt);

            // Columnas que probablemente devolvía ValidarUser (ajusta si eran otras)
            dt.Columns.Add("ZCODIGO", typeof(string));
            dt.Columns.Add("ZALIAS", typeof(string));
            dt.Columns.Add("ZPASSWORD", typeof(string)); // Hash
            dt.Columns.Add("ZROOT", typeof(decimal));
            dt.Columns.Add("ZKEY", typeof(decimal));
            dt.Columns.Add("ZNIVEL", typeof(decimal));

            DataRow dr = dt.NewRow();
            dr["ZCODIGO"] = (object)usuario.ZCODIGO ?? DBNull.Value;
            dr["ZALIAS"] = (object)usuario.ZALIAS ?? DBNull.Value;
            dr["ZPASSWORD"] = (object)usuario.ZPASSWORD ?? DBNull.Value; // Hash
            dr["ZROOT"] = (object)usuario.ZROOT ?? DBNull.Value;
            dr["ZKEY"] = (object)usuario.ZKEY ?? DBNull.Value;
            dr["ZNIVEL"] = (object)usuario.ZNIVEL ?? DBNull.Value;
            dt.Rows.Add(dr);

            return ds;
        }

    } // Fin Clase
} // Fin Namespace