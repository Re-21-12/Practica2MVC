using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Perfiles_SA.Models;
using System.Linq;

namespace Perfiles_SA.Controllers
{
    public class EmpleadoesController : Controller
    {
        private readonly Perfiles_SAContext _context;

        public EmpleadoesController(Perfiles_SAContext context)
        {
            _context = context;
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index(string buscarDepartamento, DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Consulta LINQ
            var empleados = from Empleado in _context.Empleados select Empleado;
            Console.WriteLine(buscarDepartamento);

            // Filtrar por departamento si se proporciona
            if (!string.IsNullOrEmpty(buscarDepartamento))
            {
                empleados = empleados.Where(e => e.DepartamentoAsignado.Contains(buscarDepartamento));
            }

            // Filtrar por rango de fechas si se proporciona
            if (fechaInicio.HasValue)
            {
                empleados = empleados.Where(e => e.FechaIngresoEmpresa >= fechaInicio);
            }

            if (fechaFin.HasValue)
            {
                empleados = empleados.Where(e => e.FechaIngresoEmpresa <= fechaFin);
            }

            // Ejecutar la consulta y retornar la vista
            return View(await empleados.ToListAsync());
        }


        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Dpi == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dpi,Nombres,FechaNacimiento,Genero,FechaIngresoEmpresa,Direccion,Nit,DepartamentoAsignado,Edad")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Dpi,Nombres,FechaNacimiento,Genero,FechaIngresoEmpresa,Direccion,Nit,DepartamentoAsignado,Edad")] Empleado empleado)
        {
            if (id != empleado.Dpi)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Dpi))
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
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Dpi == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'Perfiles_SAContext.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(string id)
        {
            return (_context.Empleados?.Any(e => e.Dpi == id)).GetValueOrDefault();
        }
    }
}
