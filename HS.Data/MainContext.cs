
using HS.Models;
using Microsoft.EntityFrameworkCore;


namespace HS.Data

{
    public class MainContext : DbContext
    {

        public MainContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<VisitedPatient> VisitedPatients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
       // public DbSet<EquipmentViewModelMain> EquipmentViewModelMain { get; set; }
        public DbSet<OrderEquipment> OrderEquipment { get; set; }
        public DbSet<EquipmentViewModel> MedicalEquipmentList { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalEquipment> medical_equipment { get; set; }
        public DbSet<tblMVCCharts> tblMVCCharts { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
        public object LoggedInUsers { get; set; }

        // public DbSet<DashboardViewModel> DashboardViewModel { get; set; }

    }
}
