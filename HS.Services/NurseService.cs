
using HS.Data;
using HS.Models;
using Microsoft.EntityFrameworkCore;

namespace HS.Services
{
    public class NurseService
    {
        private readonly MainContext _dbContext;

        public NurseService(MainContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Nurse> GetNurseByIdAsync(int id)
        {
            return await _dbContext.Nurses.FindAsync(id);
        }

        public async Task<List<Nurse>> GetNursesAsync()
        {
            return await _dbContext.Nurses.ToListAsync();
        }

        
        public async Task InsertNurseAsync(Nurse nurse)
        {
            nurse.CreatedDate = DateTime.Now;

            if (nurse.Photo != null && nurse.Photo.Length > 0)
            {
                var fileName = Path.GetFileName(nurse.Photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "nurseimage", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await nurse.Photo.CopyToAsync(fileStream);
                }
                nurse.PhotoPath = fileName;
            }

            await _dbContext.Nurses.AddAsync(nurse);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateNurseAsync(Nurse nurse)
        {
            var existingNurse = await _dbContext.Nurses.FirstOrDefaultAsync(n => n.Id == nurse.Id);

            if (existingNurse != null)
            {
                existingNurse.Name = nurse.Name;
                existingNurse.Email = nurse.Email;
                existingNurse.Password = nurse.Password;
                existingNurse.DOB = nurse.DOB;
                existingNurse.Phone = nurse.Phone;
                existingNurse.Gender = nurse.Gender;
                existingNurse.Specialist = nurse.Specialist;
                existingNurse.Salary = nurse.Salary;

                if (nurse.Photo != null && nurse.Photo.Length > 0)
                {
                    var fileName = Path.GetFileName(nurse.Photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "nurseimage", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await nurse.Photo.CopyToAsync(fileStream);
                    }
                    existingNurse.PhotoPath = fileName;
                }

                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task DeleteNurseAsync(int nurseId)
        {
            var existingNurse = await _dbContext.Nurses.FirstOrDefaultAsync(n => n.Id == nurseId);

            if (existingNurse != null)
            {
                _dbContext.Nurses.Remove(existingNurse);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
