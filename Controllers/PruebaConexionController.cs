using Gestion_Fimas_Digitales.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Fimas_Digitales.Controllers
{
    public class PruebaConexionController : Controller
    {
        private readonly Db_context _context;

        public PruebaConexionController(Db_context context)
        {
            _context = context;
        }

        public async Task<IActionResult> TestConexion()
        {
            try
            {
                var exists = await _context.Firmas.AnyAsync();
                return Content("Conexión exitosa a la base de datos.");
            }
            catch (Exception ex)
            {
                return Content($"Error de conexión: {ex.Message}");
            }
        }
    }
}
