using HS.Data;
using HS.Models;
using HS.Models.Modelforchart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using System.Web.Helpers;
namespace HosDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MainContext db;
        public HomeController(ILogger<HomeController> logger, MainContext _db)
        {
            db = _db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            DashboardViewModel dashboard = new DashboardViewModel();
            /////--------------------card code to count-------------- 
            dashboard.doctors_count = db.Doctors.Count();
            dashboard.nurses_count = db.Nurses.Count();
            dashboard.patients_count = db.VisitedPatients.Count();
            dashboard.medicalequipment_count = db.medical_equipment.Count();
            //dashboard.employee_count = db.VisitedPatients.Count(); 



            ////--------------------chart code visited patients-- -

            var data = db.VisitedPatients
      .GroupBy(p => new { p.VisitYear, p.Gender, p.Department })
      .Select(g => new { g.Key.VisitYear, g.Key.Gender, g.Key.Department, PatientCount = g.Count() })
      .ToList();

            ViewBag.ChartData = JsonConvert.SerializeObject(data);


            return View(dashboard);//imp




        }
        public IActionResult ChartData()
        {
            // Retrieve data from the database using Entity Framework Core
            var data = db.VisitedPatients
                .GroupBy(p => new { p.VisitYear, p.Gender, p.Department })
                .Select(g => new { g.Key.VisitYear, g.Key.Gender, g.Key.Department, PatientCount = g.Count() })
                .ToList();

            // Create an empty chart object
            var chart = new ChartViewModel();

            // Add a chart series for each unique department
            var departments = data.Select(d => d.Department).Distinct();
            foreach (var department in departments)
            {
                // Add a new chart series for this department
                var series = new ChartSeriesViewModel();
                series.Name = department;

                // Filter the data to only include this department
                var departmentData = data.Where(d => d.Department == department);

                // Add data points to the chart series for each gender and visit year
                var genders = departmentData.Select(d => d.Gender).Distinct();
                foreach (var gender in genders)
                {
                    // Add a new chart data point for this gender
                    var dataPoints = departmentData.Where(d => d.Gender == gender).ToList();
                    foreach (var dataPoint in dataPoints)
                    {
                        var point = new ChartDataPointViewModel();
                        point.XValue = dataPoint.VisitYear.ToString();
                        point.YValue = dataPoint.PatientCount;
                        series.DataPoints.Add(point);
                    }
                }

                // Add the chart series to the chart object
                chart.Series.Add(series);
            }

            // Set the chart properties
            chart.Title = "Visited Patients by Department and Gender";
            chart.XTitle = "Visit Year";
            chart.YTitle = "Number of Patients";

            // Send the chart data to the view
            return View(chart);
        }

        //public IActionResult ChartData()
        //{
        //    // Retrieve data from the database using Entity Framework Core
        //    var data = db.VisitedPatients
        //        .GroupBy(p => new { p.VisitYear, p.Gender, p.Department })
        //        .Select(g => new { g.Key.VisitYear, g.Key.Gender, g.Key.Department, PatientCount = g.Count() })
        //        .ToList();

        //    // Create an empty chart object
        //    var chart = new ChartViewModel();

        //    // Add a chart series for each unique department
        //    var departments = data.Select(d => d.Department).Distinct();
        //    foreach (var department in departments)
        //    {
        //        // Add a new chart series for this department
        //        var series = new ChartSeriesViewModel();
        //        series.Name = department;

        //        // Filter the data to only include this department
        //        var departmentData = data.Where(d => d.Department == department);

        //        // Add data points to the chart series for each gender and visit year
        //        var genders = departmentData.Select(d => d.Gender).Distinct();
        //        foreach (var gender in genders)
        //        {
        //            // Add a new chart data point for this gender
        //            var dataPoints = departmentData.Where(d => d.Gender == gender).ToList();
        //            foreach (var dataPoint in dataPoints)
        //            {
        //                var point = new ChartDataPointViewModel();
        //                point.X = dataPoint.VisitYear.ToString();
        //                point.Y = dataPoint.PatientCount;
        //                series.DataPoints.Add(point);
        //            }
        //        }

        //        // Add the chart series to the chart object
        //        chart.Series.Add(series);
        //    }

        //    // Send the chart data to the view
        //    return View(chart);
        //}

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult VisitedPatientsByYear()
        {
            var data = db.VisitedPatients
                .GroupBy(p => new { p.VisitYear, p.Gender, p.Department })
                .Select(g => new { g.Key.VisitYear, g.Key.Gender, g.Key.Department, PatientCount = g.Count() })
                .ToList();

            ViewBag.ChartData = JsonConvert.SerializeObject(data);

            return View();
        }

        public ActionResult ChartPie()
        {
            // var _context = new TestEntities();

            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            var results = (from c in db.tblMVCCharts select c);

            results.ToList().ForEach(rs => xValue.Add(rs.Growth_Year));
            results.ToList().ForEach(rs => yValue.Add(rs.Growth_Value));

            new System.Web.Helpers.Chart(width: 600, height: 400, theme: ChartTheme.Vanilla3D)
            .AddTitle("Chart for Growth [Pie Chart]")
                    .AddLegend("Summary")
                    .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue)
                    .Write("bmp");

            return null;
        }
        public ActionResult Chart()
        {
            // Get data from VisitedPatients table
            var data = db.VisitedPatients.GroupBy(p => new { p.VisitYear, p.Gender, p.Department })
                                          .Select(g => new {
                                              VisitYear = g.Key.VisitYear,
                                              Gender = g.Key.Gender,
                                              Department = g.Key.Department,
                                              PatientCount = g.Count(),
                                              AverageAge = g.Average(p => p.PatientAge)
                                          }).ToList();

            // Create chart data
            var chartData = new List<VisitedPatient>();
            foreach (var item in data)
            {
                chartData.Add(new VisitedPatient
                {
                    VisitYear = item.VisitYear,
                    Gender = item.Gender,
                    Department = item.Department,
                   // PatientCount = item.PatientCount,
                  //  AverageAge = item.AverageAge
                });
            }

            ViewBag.ChartData = JsonConvert.SerializeObject(chartData);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
