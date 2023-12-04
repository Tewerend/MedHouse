using MedHouse.Models;
using MedHouse.Models.Data;
using MedHouse.ViewModels.MeasuringMedications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedHouse.Controllers
{
    public class MeasuringMedicationsController : Controller
    {
        private readonly AppCtx _context;

        public MeasuringMedicationsController(AppCtx context)
        {
            _context = context;
        }

        // GET: MeasuringMedications
        public async Task<IActionResult> Index()
        {
            // через контекст данных получаем доступ к таблице базы MeasuringMedications
            var AppCtx = _context.MeasuringMedications
                .OrderBy(f => f.Measuring);   // сортируем все записи по имени поставщиков

            // возвращаем в представление полученный список записей
            return View(await AppCtx.ToListAsync());
        }

        // GET: MeasuringMedications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeasuringMedications == null)
            {
                return NotFound();
            }

            var measuringMedication = await _context.MeasuringMedications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measuringMedication == null)
            {
                return NotFound();
            }

            return View(measuringMedication);
        }

        // GET: MeasuringMedications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeasuringMedications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMeasuringMedicationViewModel model)
        {
            if (_context.MeasuringMedications
                .Where(f => f.Measuring == model.Measuring)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный измерение уже существует");
            }

            if (ModelState.IsValid)
            {
                MeasuringMedication measuringMedication = new()
                {
                    Measuring = model.Measuring
                };

                _context.Add(measuringMedication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: MeasuringMedications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeasuringMedications == null)
            {
                return NotFound();
            }

            var measuringMedication = await _context.MeasuringMedications.FindAsync(id);
            if (measuringMedication == null)
            {
                return NotFound();
            }

            EditMeasuringMedicationViewModel model = new()
            {
                Id = measuringMedication.Id,
                Measuring = measuringMedication.Measuring
            };
            return View(model);
        }

        // POST: MeasuringMedications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMeasuringMedicationViewModel model)
        {
            if (_context.MeasuringMedications
                .Where(f => f.Measuring == model.Measuring)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный измерение уже существует");
            }

            MeasuringMedication measuringMedication = await _context.MeasuringMedications.FindAsync(id);

            if (id != measuringMedication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    measuringMedication.Measuring = model.Measuring;
                    _context.Update(measuringMedication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasuringMedicationExists(measuringMedication.Id))
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
            return View(model);
        }

        // GET: MeasuringMedications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeasuringMedications == null)
            {
                return NotFound();
            }

            var measuringMedication = await _context.MeasuringMedications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (measuringMedication == null)
            {
                return NotFound();
            }

            return View(measuringMedication);
        }

        // POST: MeasuringMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeasuringMedications == null)
            {
                return Problem("Entity set 'AppCtx.MeasuringMedications'  is null.");
            }
            var measuringMedication = await _context.MeasuringMedications.FindAsync(id);
            if (measuringMedication != null)
            {
                _context.MeasuringMedications.Remove(measuringMedication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeasuringMedicationExists(int id)
        {
          return (_context.MeasuringMedications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
