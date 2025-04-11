using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Satelite
{
    public class LineaCharts
    {
        public List<SeriesItem> series { get; set; }
        public string[] fechas { get; set; }
        public LineaCharts(List<SeriesItem> iSeries, string[] iFechas)
        {
            series = iSeries;
            fechas = iFechas;
        }
    }
}