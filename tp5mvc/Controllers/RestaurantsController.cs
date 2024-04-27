using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tp5mvc.Models.RestosModel;

namespace tp5mvc.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestosDbContext _context;

        public RestaurantsController(RestosDbContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var restosDbContext = _context.Restaurants.Include(r => r.LeProprio);
            return View(await restosDbContext.ToListAsync());
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.LeProprio)
                .FirstOrDefaultAsync(m => m.CodeResto == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Email");
            return View();
        }

        // POST: Restaurants/Create
       
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CodeResto,NomResto,Specialite,Ville,Tel,NumProp")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Email", restaurant.NumProp);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Email", restaurant.NumProp);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
     
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CodeResto,NomResto,Specialite,Ville,Tel,NumProp")] Restaurant restaurant)
        {
            if (id != restaurant.CodeResto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.CodeResto))
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
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Email", restaurant.NumProp);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.LeProprio)
                .FirstOrDefaultAsync(m => m.CodeResto == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.CodeResto == id);
        }
        public async Task<IActionResult> DetailsWithReviews(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .Include(r => r.LesAvis)
                .FirstOrDefaultAsync(m => m.CodeResto == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        public async Task<IActionResult> GetRestaurantReviews(int id)
        {
            var reviews = await _context.Avis
                .Where(a => a.NumResto == id)
                .ToListAsync();

            return Json(reviews); 
        }
        public async Task<IActionResult> RestaurantsAboveAverageRating()
        {
            var restaurants = await _context.Restaurants
                .Include(r => r.LesAvis)
                .Where(r => r.LesAvis.Any() && r.LesAvis.Average(a => a.Note) >= 3.5)
                .ToListAsync();

            return View(restaurants);
        }


    }
}
