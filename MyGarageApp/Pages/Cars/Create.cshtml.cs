using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyGarageApp.Data;
using MyGarageApp.Models;

namespace MyGarageApp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Car Car { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(string UserId=null)
        {
            Car = new Car();
            if (UserId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                UserId = claim.Value;
            }
            Car.UserId = UserId;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Car.Add(Car);
            await _db.SaveChangesAsync();
            StatusMessage = "Car has been added successfully";
            return RedirectToPage("Index", new { UserId = Car.UserId });
        }
    }
}
