using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGarageApp.Data;
using MyGarageApp.Models.ViewModel;

namespace MyGarageApp.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustVM { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> OnGet(string UserId = null)
        {
            if (UserId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                UserId = claim.Value;
            }
            CarAndCustVM = new CarAndCustomerViewModel()
            {
                Cars = await _db.Car.Where(c => c.UserId == UserId).ToListAsync(),
                UserObj = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == UserId)
            };
            return Page();
        }
    }
}
