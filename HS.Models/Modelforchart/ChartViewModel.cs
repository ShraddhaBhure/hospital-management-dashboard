namespace HS.Models.Modelforchart
{
    public class ChartViewModel
    {
        public ChartViewModel()
        {
        }

        public ChartViewModel(string title, string xTitle, string yTitle)
        {
            Title = title;
            XTitle = xTitle;
            YTitle = yTitle;
            Series = new List<ChartSeriesViewModel>();
        }

        public string Title { get; set; }
        public string XTitle { get; set; }
        public string YTitle { get; set; }
        public List<ChartSeriesViewModel> Series { get; set; }
    }

}
