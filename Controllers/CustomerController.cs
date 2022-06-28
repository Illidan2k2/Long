using FPTLibrary.Models;
using FPTLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FPTLibrary.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;
        public CustomerController (ApplicationDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var customer = context.Customers.ToList();
            return View(customer);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = context.Customers.FirstOrDefault(c => c.Id == id);
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Update(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string SearchCustomer)
        {
            ViewData["GetCustomer"] = SearchCustomer;
            var query = from item in context.Customers select item;
            if (!string.IsNullOrEmpty(SearchCustomer))
            {
                query = query.Where(b => b.FirstName.Contains(SearchCustomer) || b.LastName.Contains(SearchCustomer));
            }
            return View(await query.AsNoTracking().ToListAsync());
        }
    }
}
