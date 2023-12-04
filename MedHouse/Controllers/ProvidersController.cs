using MedHouse.Models;
using MedHouse.Models.Data;
using MedHouse.ViewModels.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedHouse.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly AppCtx _context;

        public ProvidersController(AppCtx context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index()
        {
            // через контекст данных получаем доступ к таблице базы Providers
            var AppCtx = _context.Providers
                .OrderBy(f => f.NameProvider);   // сортируем все записи по имени поставщиков

            // возвращаем в представление полученный список записей
            return View(await AppCtx.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProviderViewModel model)
        {
            if (_context.Providers
                .Where(f => f.NameProvider == model.NameProvider)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный поставщик уже существует");
            }

            if (ModelState.IsValid)
            {
                Provider provider = new()
                {
                    NameProvider = model.NameProvider
                };

                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            EditProviderViewModel model = new()
            {
                Id = provider.Id,
                NameProvider = provider.NameProvider
            };
            return View(model);
        }

        // POST: Providers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProviderViewModel model)
        {
            if (_context.Providers
                .Where(f => f.NameProvider == model.NameProvider)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный поставщик уже существует");
            }

            Provider provider = await _context.Providers.FindAsync(id);

            if (id != provider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    provider.NameProvider = model.NameProvider;
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.Id))
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

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Providers == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Providers == null)
            {
                return Problem("Entity set 'AppCtx.Providers'  is null.");
            }
            var provider = await _context.Providers.FindAsync(id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
          return (_context.Providers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
