namespace HS.Models.Modelforchart
{
    public class ChartDataPointViewModel
    {
        public ChartDataPointViewModel()
        {
        }

        public ChartDataPointViewModel(string xValue, int yValue)
        {
            XValue = xValue;
            YValue = yValue;
           
        }

        public string XValue { get; set; }
        public int YValue { get; set; }
       
    }
}
