using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Security.Cryptography; // Libreria de cifrado.
using System.Text;
using System.Drawing;

namespace Satelite
{
    public static class Seguridad
    {
        /// Rijndael - AES
        /// Esta clase contiene funciones para encriptar/desencriptar
        /// El ser estática no es necesario instanciar un objeto para 
        /// usar las funciones Encriptar y DesEncriptar
        /// 
        /// El usuario a través del ejecutable en sus equipo, verifica su propio hardware y con su sesión de usuario (usuario, password) 
        /// además de la llave alojada en el servidor creada por el propio usuario en su alta dentro de su perfil personal se completan para 
        /// desencriptar la información que contanga dicho perfil personal. Se denomina TRIPLE AES

        //ejemplo Clave:                 1290-E48E-B887-B0F8-E786-E002-A9F6-25E4
        public static string ClKyClone = "random del hardware del equipo"; // Clave de cifrado. NOTA: Puede ser cualquier combinación de carácteres. Cada usuario lleva su ID hardware en el EXE
        public static string Clave = "1290-E48E-B887-B0F8-E786-E002-A9F6-25E4"; // Clave de cifrado del servidor para ese random del hardware del equipo. NOTA: Puede ser cualquier combinación de carácteres. Cada usuario lleva su ID hardware en el EXE

        // Esto va en el servidor para conectarse
        // Esta es la llave creada para el perfil del usuario y para ese dispotivo, con un maximo 3 dispositivos.
        // El usuario deberá entender que si falta una de esas llaves no se podrá leer la información que contenga.
        // Ante robo hurto o perdida es necesario cambiar la clave de ese dispositivo desde el portal,
        // accediendo a través de otro de los dos dispositivos restantes.

        public static string Product = ""; // ejemplo: "opyj3COlSpt8b1flI3ZZcnb4598fWNX91BVbXCPGgpwxFBqdoYl7ew==";  Clave publica de cifrado del servidor para su (Hardware del equipo) ClKyClone en su alta. NOTA: Puede ser cualquier combinación de carácteres. Cada usuario lleva su ID en el EXE generado en su alta

        public static string HexaADecimal(string ncalcu)
        {
            string result = "";
            for (int x = ncalcu.Length - 1, y = 0; x >= 0; x--, y++)
            {
                switch (ncalcu[x])
                {
                    case 'A': result += (int)(10 * Math.Pow(16, y)); break;
                    case 'B': result += (int)(11 * Math.Pow(16, y)); break;
                    case 'C': result += (int)(12 * Math.Pow(16, y)); break;
                    case 'D': result += (int)(13 * Math.Pow(16, y)); break;
                    case 'E': result += (int)(14 * Math.Pow(16, y)); break;
                    case 'F': result += (int)(15 * Math.Pow(16, y)); break;
                    default: result += (int)(int.Parse(ncalcu[x].ToString()) * Math.Pow(16, y)); break;
                }
            }
            return result;
        }

        /// <summary>
        /// clase para guardar las especificaciones de los dispositivos
        /// </summary>
        /// 

 

        public static string DecimalAHexadecimal(int ncalcu)
        {
            string result = "";
            while (ncalcu != 0)
            {
                switch (ncalcu % 16)
                {
                    case 10: result = "A" + result; break;
                    case 11: result = "B" + result; break;
                    case 12: result = "C" + result; break;
                    case 13: result = "D" + result; break;
                    case 14: result = "E" + result; break;
                    case 15: result = "F" + result; break;
                    default: result = (ncalcu % 16).ToString() + result; break;
                }
                ncalcu /= 16;
            }
            return result;
        }

        public static string AplicaMiMascara(string Ruta, int Objecto)
        {
            string result = "";
            string Mascara = "";

            if (Objecto == 1) //Es tabla
            {
                Mascara = "00000000";
            }
            else // Es extension
            {
                Mascara = "000";
            }

            int Punto = Ruta.Length;
            int Punta = Mascara.Length;

            if (Punta > Punto)
            {
                result = Mascara.Substring(1, (Punta - Punto)) + Ruta;
            }
            else
            {
                result = Ruta;
            }

            return result;
        }

        /// Encripta una cadena
        public static string Encriptar(this string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(this string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public static string cifrar(string cadena, Boolean Tipo, int Modo)
        {
            try
            {
                //int contador = 0;
                //contador = cadena.IndexOf("<@>");
                //if (contador != -1) { return cadena; }

                byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

                byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

                // Ciframos utilizando el Algoritmo MD5.
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                if (Tipo == false)//Tipo == false web
                {
                    if (Modo == 1)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["instalacion"].ToString()));
                    }
                    else if (Modo == 2)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["OtherUserClave"].ToString()));
                    }
                    else
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Clave));
                    }
                }
                else//Tipo == true app
                {
                    if (HttpContext.Current.Session["pki1"].ToString() != "" && HttpContext.Current.Session["pks"].ToString() == "1") // true)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["pki1"].ToString()));
                    }
                    else
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["instalacion"].ToString()));
                    }
                }
                //llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Clave));
                md5.Clear();

                //Ciframos utilizando el Algoritmo 3DES.
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
                tripledes.Clear();

                return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
            }
            catch (Exception e)
            {
                string b = e.Message;
                string cadena_descifrada = "Error de cifrado"; // Obtenemos la cadena
                return cadena_descifrada;
            }
        }

        // Función para descifrar una cadena.
        public static string descifrar(string cadena, Boolean Tipo, int Modo)
        {
            try
            {
                //int contador = 0;
                //contador = cadena.IndexOf("<@>");
                //if (contador != -1) { return cadena; }

                byte[] llave;

                byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

                // Ciframos utilizando el Algoritmo MD5.
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                if (Tipo == false)//Tipo == false es web
                {
                    if (Modo == 1)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["instalacion"].ToString()));
                    }
                    else if (Modo == 2)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["OtherUserClave"].ToString()));
                    }
                    else
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Clave));
                    }
                }
                else//Tipo == true app
                {
                    if (HttpContext.Current.Session["pki1"].ToString() != "" && HttpContext.Current.Session["pks"].ToString() == "1") // true)
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["pki1"].ToString()));
                    }
                    else
                    {
                        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HttpContext.Current.Session["instalacion"].ToString()));
                    }
                }
                //llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Clave));
                md5.Clear();

                //Ciframos utilizando el Algoritmo 3DES.
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateDecryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();

                string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
                return cadena_descifrada; // Devolvemos la cadena
            }
            catch (Exception e)
            {
                string b = e.Message;
                string cadena_descifrada = "Error de descifrado"; // Obtenemos la cadena
                return cadena_descifrada;
            }
        }

        public static Boolean Comprueba(string cadena)
        {
            HttpContext.Current.Session["pks"] = "0";// = false;

            if (HttpContext.Current.Session["pki1"].ToString() == "")
            {
                HttpContext.Current.Session["pki1"] = HttpContext.Current.Session["instalacion"].ToString();
                ////Variables.pki0 = esteban.dmi.0000000001
                ////Variables.pki2 = esteban.dmi.0000000001 encriptado
                ////Si es igual a Clave pongo esteban.dmi.0000000001
                //Variables.pki0 = descifrar(Variables.pki2);
                ////if (Clave  == Variables.pki0)
                ////{
                //    if (Variables.instalacion == Variables.pki0)
                //    {
                //        Variables.pki1 = Variables.pki0;
                //        Variables.pks = true;
                //    }
                ////}
            }
            else if (HttpContext.Current.Session["pki1"].ToString() != "")
            {
                //Variables.pki1 = Variables.pki0;
                //Variables.pks = true;              
            }
            return Convert.ToBoolean(HttpContext.Current.Session["pks"]);
        }
    }
}




















//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace QRCode_Demo
//{
//    public static class Seguridad
//    {

//        /// Esta clase contiene funciones para encriptar/desencriptar
//        /// El ser estática no es necesario instanciar un objeto para 
//        /// usar las funciones Encriptar y DesEncriptar



//            /// Encripta una cadena
//            public static string Encriptar(this string _cadenaAencriptar)
//            {
//                string result = string.Empty;
//                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
//                result = Convert.ToBase64String(encryted);
//                return result;
//            }

//            /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
//            public static string DesEncriptar(this string _cadenaAdesencriptar)
//            {
//                string result = string.Empty;
//                byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
//                //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
//                result = System.Text.Encoding.Unicode.GetString(decryted);
//                return result;
//            }
//     }


//}