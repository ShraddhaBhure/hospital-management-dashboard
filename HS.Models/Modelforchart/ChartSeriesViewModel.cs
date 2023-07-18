namespace HS.Models.Modelforchart
{
    public class ChartSeriesViewModel
    {
        public ChartSeriesViewModel()
        {
        }

        public ChartSeriesViewModel(string name)
        {
            Name = name;
            DataPoints = new List<ChartDataPointViewModel>();
        }

        public string Name { get; set; }
        public List<ChartDataPointViewModel> DataPoints { get; set; }
    }




}
