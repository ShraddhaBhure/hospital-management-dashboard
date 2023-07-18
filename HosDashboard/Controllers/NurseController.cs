
using HS.Models;
using HS.Services;
using Microsoft.AspNetCore.Mvc;
namespace HosDashboard.Controllers
{
    public class NurseController : Controller
    {
        private readonly NurseService _nurseService;
        public NurseController(NurseService nurseService)
        {
            _nurseService = nurseService;
        }

        public async Task<IActionResult> Index()
        {
            var nurses = await _nurseService.GetNursesAsync();
            return View(nurses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                await _nurseService.InsertNurseAsync(nurse);
                return RedirectToAction(nameof(Index));
            }

            return View(nurse);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nurse = await _nurseService.GetNurseByIdAsync(id);

            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                await _nurseService.UpdateNurseAsync(nurse);
                return RedirectToAction(nameof(Index));
            }

            return View(nurse);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nurse = await _nurseService.GetNurseByIdAsync(id.Value);

            if (nurse == null)
            {
                return NotFound();
            }

            nurse.PhotoPath = nurse.PhotoPath != null ? "/nurseimage/" + nurse.PhotoPath : null; // update photo path to include nurseimage folder

            return View(nurse);
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var nurse = await _nurseService.GetNurseByIdAsync(id.Value);

        //    if (nurse == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(nurse);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var nurse = await _nurseService.GetNurseByIdAsync(id);

            if (nurse == null)
            {
                return NotFound();
            }

            return View(nurse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nurseService.DeleteNurseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
