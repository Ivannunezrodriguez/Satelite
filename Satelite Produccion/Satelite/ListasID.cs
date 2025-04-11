using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Satelite
{
    public class ListasID
    {
        public string Contenido;
        public int Valor;
        //
        public ListasID(string c, int v)
        {
            Contenido = c;
            Valor = v;
        }
        //
        public override string ToString()
        {
            return Contenido;
        }
    }
}
