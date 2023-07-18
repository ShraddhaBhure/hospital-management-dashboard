
using HS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HosDashboard.Controllers
{
    public class EmployeeController : Controller
    {
      
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        string connectionString = "Data Source=desktop-o0enem6\\sqlexpress;Initial Catalog=hospitalpappdb;Integrated Security=True";
        IWebHostEnvironment _webHostEnvironment;
       SqlConnection con;

        public EmployeeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(tblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                using (con = new SqlConnection(connectionString))
                {
                    string img = SaveImage(employee);
                    if (img != "")
                    {
                        employee.PhotoPath = img;
                    }
                    cmd = new SqlCommand("Insert into tblEmployee(Name,Email,Phone,Address,Photo,Gender,DOB,Salary,IsActive) " +
                        "values('" + employee.Name + "','" + employee.Email + "','" + employee.Phone + "','" + employee.Address + "','" + employee.PhotoPath + "'" +
                        ",'" + employee.Gender + "','" + DateTime.Parse(employee.DOB.ToString()).ToString("yyyy-MM-dd hh:MM:ss") + "','" + employee.Salary + "',1)", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Success";
                }
            }
            // return RedirectToAction("AllEmployees");
            return View();
        }
        public IActionResult AllEmployees(int? id)
        {
            using (con = new SqlConnection(connectionString))
            {
                List<tblEmployee> employee;
                tblEmployee emp;
                if (id != null)
                {
                    //cmd = new SqlCommand("Update tblEmployee set IsActive=0 where Id=" + id + "", con); to update the IsActive field 0
                    cmd = new SqlCommand("Delete from tblEmployee where Id=" + id + "", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Removed";
                }

                cmd = new SqlCommand("select * from tblEmployee where IsActive=1", con); //get all data where IsActive is 1
                cmd.CommandType = CommandType.Text;
                con.Open();
                employee = new List<tblEmployee>();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            emp = new tblEmployee();
                            emp.Id = Convert.ToInt32(dr[0].ToString());
                            emp.Name = dr[1].ToString();
                            emp.Email = dr[2].ToString();
                            emp.Phone = dr[3].ToString();
                            employee.Add(emp);
                        }
                    }
                }
                con.Close();
                //ViewBag.employee = dt;
                return View(employee);
            }

        }
        public IActionResult Details(int id)
        {
            tblEmployee emp = GetbyId(id);
            return View(emp);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            tblEmployee emp = GetbyId(id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(tblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Photo != null)
                {
                    string img = SaveImage(employee);
                    employee.PhotoPath = img;
                }

                using (con = new SqlConnection(connectionString))
                {
                    cmd = new SqlCommand("Update tblEmployee set Name='" + employee.Name + "',Email='" + employee.Email + "',Phone='" + employee.Phone + "'," +
                        "Address='" + employee.Address + "',Photo='" + employee.PhotoPath + "',Gender='" + employee.Gender + "'," +
                        "DOB='" + DateTime.Parse(employee.DOB.ToString()).ToString("yyyy-MM-dd hh:ss:mm") + "',Salary='" + employee.Salary + "',IsActive=1 where Id=" + employee.Id + "", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Success";
                }
            }
            tblEmployee emp = GetbyId(employee.Id);
            return View(emp);

        }
        public tblEmployee GetbyId(int id)
        {
            tblEmployee emp;
            using (con = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("Select * from tblEmployee where Id=" + id + "", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteReader();
                con.Close();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        emp = new tblEmployee();
                        emp.Id = Convert.ToInt32(dt.Rows[0][0].ToString());
                        emp.Name = dt.Rows[0][1].ToString();
                        emp.Email = dt.Rows[0][2].ToString();
                        emp.Phone = dt.Rows[0][3].ToString();
                        emp.Address = dt.Rows[0][4].ToString();
                        emp.PhotoPath = dt.Rows[0][5].ToString();
                        emp.Gender = dt.Rows[0][6].ToString();
                        emp.DOB = Convert.ToDateTime(dt.Rows[0][7].ToString());
                        emp.Salary = dt.Rows[0][8].ToString();
                    }
                }
            }
            return emp;
        }
        public string SaveImage(tblEmployee employee)
        {
            string filename = "";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "ProfileImage");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (employee.PhotoPath != null)
            {
                FileInfo file = new FileInfo(Path.Combine(path, employee.PhotoPath));
                if (file.Exists)
                {
                    file.Delete();
                }
            }
            string ext = Path.GetExtension(employee.Photo.FileName);
            filename = Guid.NewGuid().ToString() + "_cover" + ext;
            var filepath = Path.Combine(path, filename);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                employee.Photo.CopyTo(fileStream);

            }

            return filename;
        }
    }
}

