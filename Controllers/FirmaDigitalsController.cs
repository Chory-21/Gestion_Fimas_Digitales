using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Fimas_Digitales.Data;
using Gestion_Fimas_Digitales.Models;
using Gestion_Fimas_Digitales.Services;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Fimas_Digitales.Controllers
{
    [Authorize] 
    public class FirmaDigitalsController : Controller
    {
        private readonly Db_context _context;
        private readonly IFirmaDigitalService _firmaDigitalService;
        

        public FirmaDigitalsController(Db_context context, IFirmaDigitalService firmaDigitalService)
        {
            _context = context;
            _firmaDigitalService = firmaDigitalService;
            
        }

        // GET: FirmaDigitals
        public async Task<IActionResult> Index(string search)
        {
            var firmas = from f in _context.Firmas
                         select f;

            if (!String.IsNullOrEmpty(search))
            {
                firmas = firmas.Where(s => s.RazonSocial.Contains(search) || s.RepresentanteLegal.Contains(search));
            }

            return View(await firmas.ToListAsync());
        }

        // GET: FirmaDigitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmaDigital = await _context.Firmas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (firmaDigital == null)
            {
                return NotFound();
            }

            return View(firmaDigital);
        }

        // GET: FirmaDigitals/Create
        // GET: Muestra el formulario para crear una nueva firma digital
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recibe los datos del formulario y guarda la firma digital
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FirmaDigital firmaDigital)
        {
            if (ModelState.IsValid)
            {
                // Convertir las fechas a UTC antes de guardar
                firmaDigital.FechaEmision = DateTime.SpecifyKind(firmaDigital.FechaEmision, DateTimeKind.Utc);
                firmaDigital.FechaVencimiento = DateTime.SpecifyKind(firmaDigital.FechaVencimiento, DateTimeKind.Utc);

                _context.Firmas.Add(firmaDigital);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(firmaDigital);
        }

        // GET: Muestra el formulario de edición de la firma digital
        public IActionResult Edit(int id)
        {
            var firma = _context.Firmas.Find(id);
            if (firma == null)
            {
                return NotFound();
            }
            return View(firma);
        }

        // POST: Recibe los datos del formulario de edición y actualiza la firma digital
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FirmaDigital firmaDigital)
        {
            if (id != firmaDigital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Convertir las fechas a UTC antes de actualizar
                firmaDigital.FechaEmision = DateTime.SpecifyKind(firmaDigital.FechaEmision, DateTimeKind.Utc);
                firmaDigital.FechaVencimiento = DateTime.SpecifyKind(firmaDigital.FechaVencimiento, DateTimeKind.Utc);

                _context.Firmas.Update(firmaDigital);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(firmaDigital);
        }

        // GET: Muestra la vista de confirmación para eliminar una firma digital
        public IActionResult Delete(int id)
        {
            var firma = _context.Firmas.Find(id);
            if (firma == null)
            {
                return NotFound();
            }
            return View(firma);
        }

        // POST: Elimina la firma digital después de la confirmación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var firma = _context.Firmas.Find(id);
            if (firma == null)
            {
                return NotFound();
            }

            _context.Firmas.Remove(firma);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool FirmaDigitalExists(int id)
        {
            return _context.Firmas.Any(e => e.Id == id);
        }

        // GET: FirmaDigitals/ExportToExcel
        public IActionResult ExportToExcel()
        {
            var firmas = _context.Firmas.ToList();
            var stream = _firmaDigitalService.ExportToExcel(firmas);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FirmasDigitales.xlsx");
        }
    }
}
