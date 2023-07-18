
using HS.Data;
using HS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HosDashboard.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly MainContext _context;

        public EquipmentController(MainContext context)
        {
            _context = context;
        }

        // GET: Equipment
        public IActionResult Index()
        {
            var equipmentList = _context.MedicalEquipmentList.ToList();
            return View(equipmentList);
        }

        // GET: Equipment/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = _context.MedicalEquipmentList.FirstOrDefault(m => m.EquipmentId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // GET: Equipment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EquipmentName,ManufacturerName,ModelNumber,Quantity,Price")] EquipmentViewModel equipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipment/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = _context.MedicalEquipmentList.FirstOrDefault(m => m.EquipmentId == id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EquipmentId,EquipmentName,ManufacturerName,ModelNumber,Quantity,Price")] EquipmentViewModel equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipment);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.EquipmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipment/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = _context.MedicalEquipmentList.FirstOrDefault(m => m.EquipmentId == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var equipment = _context.MedicalEquipmentList.Find(id);
            _context.MedicalEquipmentList.Remove(equipment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.MedicalEquipmentList.Any(e => e.EquipmentId == id);
        }



        ////// prchase


        public async Task<IActionResult> EditQuantity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.MedicalEquipmentList.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            var viewModel = new EquipmentViewModel
            {
                EquipmentId = equipment.EquipmentId,
                EquipmentName = equipment.EquipmentName,
                ManufacturerName = equipment.ManufacturerName,
                ModelNumber = equipment.ModelNumber,
                Quantity = equipment.Quantity,
                Price = equipment.Price
            };

            return View(viewModel);
        }


        //[HttpPost]
        //public IActionResult SaveOrder([FromBody] OrderEquipment order)
        //{
        //    try
        //    {
        //        var orderData = new OrderEquipment
        //        {
        //            EquipmentName = order.EquipmentName,
        //            ManufacturerName = order.ManufacturerName,
        //            ModelNumber = order.ModelNumber,
        //            Quantity = order.Quantity,
        //            PricePerUnit = order.Price,
        //            TotalPrice = Convert.ToDecimal(order.TotalPrice),
        //            PurchaseDept = order.PurchaseDept,
        //            PurchaseAuthority = order.PurchaseAuthority,
        //            PurchaseDate =Convert.ToDateTime(order.PurchaseDate)
        //        };

        //        _context.OrderEquipment.Add(orderData);
        //        _context.SaveChanges();

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //}

        //[HttpPost]
        //public IActionResult EditQuantity(OrderEquipment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var orderEquipment = new OrderEquipment
        //        {
        //            EquipmentId = model.EquipmentId,
        //            EquipmentName = model.EquipmentName,
        //            ManufacturerName = model.ManufacturerName,
        //            ModelNumber = model.ModelNumber,
        //            Quantity = model.Quantity,
        //            PricePerUnit = model.Price,
        //            TotalPrice = model.Quantity * model.Price,
        //            PurchaseDept = model.PurchaseDept,
        //            PurchaseAuthority = model.PurchaseAuthority,
        //            PurchaseDate = DateTime.Now
        //        };

        //        // Add the new order equipment to the database
        //        _context.OrderEquipment.Add(orderEquipment);
        //        _context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View(model);
        // }



    }
}