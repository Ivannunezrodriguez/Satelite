using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace Satelite
{
    public class Datos
    {
        Ado ado;

        public Datos()
        {
            ado = new Ado();
        }

        public DataTable getDatos()
        {
            //return ado.Retrieve("CantidadRegistros_PorCiudad");
            return ado.Retrieve("SELECT SUM(CONVERT(int,NUM_UNIDADES)) as CUANTOS ,UNIDADES ,VARIEDAD, MONTH(CONVERT(DATE,FECHA)) as MES ,YEAR(CONVERT(DATE,FECHA)) as FECHA  FROM ZENTRADA  GROUP BY  MONTH(CONVERT(DATE,FECHA)),YEAR(CONVERT(DATE,FECHA)),VARIEDAD, UNIDADES order by FECHA, MES");
        }
    }
}