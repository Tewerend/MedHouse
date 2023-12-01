using MedHouse.Models;
using MedHouse.Models.Data;
using MedHouse.ViewModels.Providers;
using MedHouse.ViewModels.UniqueStorages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;

namespace MedHouse.Controllers
{
    public class UniqueStoragesController : Controller
    {
        private readonly AppCtx _context;

        public UniqueStoragesController(AppCtx context)
        {
            _context = context;
        }

        // GET: UniqueStorages
        public async Task<IActionResult> Index()
        {
            // через контекст данных получаем доступ к таблице базы UniqueStorages
            var AppCtx = _context.UniqueStorages
                .OrderBy(f => f.NameStorage);   // сортируем все записи по имени поставщиков

            // возвращаем в представление полученный список записей
            return View(await AppCtx.ToListAsync());
        }

        // GET: UniqueStorages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UniqueStorages == null)
            {
                return NotFound();
            }

            var uniqueStorage = await _context.UniqueStorages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uniqueStorage == null)
            {
                return NotFound();
            }

            return View(uniqueStorage);
        }

        // GET: UniqueStorages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UniqueStorages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUniqueStorageViewModel model)
        {
            if (_context.UniqueStorages
                .Where(f => f.NameStorage == model.NameStorage && f.AdressStorage == model.AdressStorage)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный склад уже существует");
            }

            if (ModelState.IsValid)
            {
                UniqueStorage uniqueStorage = new()
                {
                    NameStorage = model.NameStorage,
                    AdressStorage = model.AdressStorage
                };

                _context.Add(uniqueStorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: UniqueStorages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UniqueStorages == null)
            {
                return NotFound();
            }

            var uniqueStorage = await _context.UniqueStorages.FindAsync(id);
            if (uniqueStorage == null)
            {
                return NotFound();
            }

            EditUniqueStorageViewModel model = new()
            {
                Id = uniqueStorage.Id,
                NameStorage = uniqueStorage.NameStorage,
                AdressStorage = uniqueStorage.AdressStorage
            };
            return View(model);
        }

        // POST: UniqueStorages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUniqueStorageViewModel model)
        {
            if (_context.UniqueStorages
                .Where(f => f.NameStorage == model.NameStorage && f.AdressStorage == model.AdressStorage)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный склад уже существует");
            }

            UniqueStorage uniqueStorage = await _context.UniqueStorages.FindAsync(id);

            if (id != uniqueStorage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uniqueStorage.NameStorage = model.NameStorage;
                    uniqueStorage.AdressStorage = model.AdressStorage;
                    _context.Update(uniqueStorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniqueStorageExists(uniqueStorage.Id))
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

        // GET: UniqueStorages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UniqueStorages == null)
            {
                return NotFound();
            }

            var uniqueStorage = await _context.UniqueStorages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uniqueStorage == null)
            {
                return NotFound();
            }

            return View(uniqueStorage);
        }

        // POST: UniqueStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UniqueStorages == null)
            {
                return Problem("Entity set 'AppCtx.UniqueStorages'  is null.");
            }
            var uniqueStorage = await _context.UniqueStorages.FindAsync(id);
            if (uniqueStorage != null)
            {
                _context.UniqueStorages.Remove(uniqueStorage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniqueStorageExists(int id)
        {
          return (_context.UniqueStorages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
