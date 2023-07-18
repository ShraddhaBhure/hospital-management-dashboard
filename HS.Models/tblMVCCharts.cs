using System.ComponentModel.DataAnnotations;

namespace HS.Models
{
    public class tblMVCCharts
    {[Key]
        public int ChartID { get; set; }
        public int Growth_Year { get; set; }
        public float Growth_Value { get; set; }

    }
}
