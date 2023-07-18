
using HS.Data;
using HS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace HosDashboard.Controllers
{
    public class OrderController : Controller
    {
        private readonly MainContext db;
        public OrderController(MainContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult OrderChart()
        {
            // Retrieve data from the Order table
            List<Order> orders = db.Orders.ToList();

            // Create a new chart object
            Chart chart = new Chart(width: 600, height: 400)
                .AddTitle("Orders by Customer")
                .AddSeries(
                    name: "Orders",
                    chartType: "pie",
                    xValue: orders,
                    xField: "CustomerID",
                    yValues: orders,
                    yFields: "OrderID");

            // Pass the chart object to the view
            return View(chart);
        }
    }
}