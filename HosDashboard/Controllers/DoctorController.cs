using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using HS.Models;

namespace HosDashboard.Controllers
{
    public class DoctorController : Controller
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        string connectionString = "Data Source=desktop-o0enem6\\sqlexpress;Initial Catalog=hospitalpappdb;Integrated Security=True";
        IWebHostEnvironment _webHostEnvironment;
        SqlConnection con;

        public DoctorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Doctor employee)
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
                    cmd = new SqlCommand("Insert into Doctor(Name,Email,Password,DOB,Phone,Gender,Specialist,CreatedDate,PhotoPath,Salary) " +
                        "values('" + employee.Name + "','" + employee.Email + "','" + employee.Password + "','" + DateTime.Parse(employee.DOB.ToString()).ToString("yyyy-MM-dd hh:MM:ss") + "','" + employee.Phone + "','" + employee.Gender + "','" + employee.Specialist + "','" + DateTime.Now.ToString() + "','" + employee.PhotoPath + "'" +
                        ",'" + employee.Salary + "')", con);
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
        public IActionResult AllDoctors(int? id)
        {
            using (con = new SqlConnection(connectionString))
            {
                List<Doctor> employee;
                Doctor emp;
                if (id != null)
                {
                    //cmd = new SqlCommand("Update tblEmployee set IsActive=0 where Id=" + id + "", con); to update the IsActive field 0
                    cmd = new SqlCommand("Delete from Doctor where Id=" + id + "", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Removed";
                }

                cmd = new SqlCommand("select * from Doctor", con); //get all data where IsActive is 1
                cmd.CommandType = CommandType.Text;
                con.Open();
                employee = new List<Doctor>();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            emp = new Doctor();
                            emp.Id = Convert.ToInt32(dr[0].ToString());
                            emp.Name = dr[1].ToString();
                            emp.Email = dr[2].ToString();
                            emp.Phone = dr[3].ToString();
                            emp.Specialist = dr[4].ToString();
                            employee.Add(emp);
                        }
                    }
                }
                con.Close();
                ViewBag.employee = dt;
                return View(employee);
            }

        }
        public IActionResult Details(int id)
        {
            Doctor emp = GetbyId(id);
            return View(emp);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Doctor emp = GetbyId(id);
            return View(emp);
        }

        //[HttpPost]
        //public IActionResult Edit(Doctor employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (employee.PhotoPath != null)
        //        {
        //            string img = SaveImage(employee);
        //            employee.PhotoPath = img;
        //        }

        //        using (con = new SqlConnection(connectionString))
        //        {
        //            cmd = new SqlCommand("UPDATE Doctor SET Name = @Name, Email = @Email, Phone = @Phone,  " +
        //                                  "Gender = @Gender,Specialist=@Specialist,CreatedDate="+ DateTime.Now.ToString() + ",PhotoPath = @PhotoPath, DOB = @DOB, Salary = @Salary" +
        //                                  "WHERE Id = @Id", con);

        //            cmd.Parameters.AddWithValue("@Name", employee.Name);
        //            cmd.Parameters.AddWithValue("@Email", employee.Email);
        //            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
        //            cmd.Parameters.AddWithValue("@Gender", employee.Gender);
        //            cmd.Parameters.AddWithValue("@Specialist", employee.Specialist);
        //        //    cmd.Parameters.AddWithValue( DateTime.Now.ToString(), employee.CreatedDate).ToString();
        //            cmd.Parameters.AddWithValue("@PhotoPath", employee.PhotoPath);

        //            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
        //            cmd.Parameters.AddWithValue("@Id", employee.Id);

        //            cmd.CommandType = CommandType.Text;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();

        //            ViewBag.msg = "Success";
        //        }
        //    }
        //    Doctor emp = GetbyId(employee.Id);
        //    return View(emp);
        //}



        [HttpPost]
        public IActionResult Edit(Doctor employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.PhotoPath != null)
                {
                    string img = SaveImage(employee);
                    employee.PhotoPath = img;
                }

                using (con = new SqlConnection(connectionString))
                {
                    cmd = new SqlCommand("Update Doctor set Name='" + employee.Name + "',Email='" + employee.Email + "',Phone='" + employee.Phone + "'," +
                        "',PhotoPath='" + employee.PhotoPath + "',Gender='" + employee.Gender + "'," +
                        "DOB='" + DateTime.Parse(employee.DOB.ToString()).ToString("yyyy-MM-dd hh:ss:mm") + "',Salary='" + employee.Salary + "',Specialist='" + employee.Specialist + "' where Id=" + employee.Id + "", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ViewBag.msg = "Success";
                }
            }
            Doctor emp = GetbyId(employee.Id);
            return View(emp);

        }



        public Doctor GetbyId(int id)
        {
            Doctor emp;
            using (con = new SqlConnection(connectionString))
            {
                cmd = new SqlCommand("Select * from Doctor where Id=" + id + "", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteReader();
                con.Close();
                using (sda = new SqlDataAdapter(cmd))
                {
                    using (dt = new DataTable())
                    {
                        sda.Fill(dt);
                        emp = new Doctor();
                        emp.Id = Convert.ToInt32(dt.Rows[0][0].ToString());
                        emp.Name = dt.Rows[0][1].ToString();
                        emp.Email = dt.Rows[0][2].ToString();
                        emp.Password = dt.Rows[0][3].ToString();
                        emp.DOB = Convert.ToDateTime(dt.Rows[0][4].ToString());
                        emp.Phone = dt.Rows[0][5].ToString();
                        emp.Gender = dt.Rows[0][6].ToString();
                        emp.Specialist = dt.Rows[0][7].ToString();
                        emp.CreatedDate = Convert.ToDateTime(dt.Rows[0][8].ToString());
                        emp.PhotoPath = dt.Rows[0][9].ToString();
                        emp.Salary = dt.Rows[0][10].ToString();
                    }
                }
            }
            return emp;
        }
        public string SaveImage(Doctor employee)
        {
            string filename = "";
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "DoctorImage");

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
            string ext = Path.GetExtension(employee.PhotoPath);
            filename = Guid.NewGuid().ToString() + "_cover" + ext;
            //var filepath = Path.Combine(path, filename);
            //using (var fileStream = new FileStream(filepath, FileMode.Create))
            //{
            //    employee.PhotoPath.(fileStream);

            //}

            return filename;
        }
    }
}

