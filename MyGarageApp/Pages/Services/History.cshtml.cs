using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGarageApp.Data;
using MyGarageApp.Models;

namespace MyGarageApp.Pages.Services
{
    
    public class HistoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public HistoryModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public List<ServiceHeader> ServiceHeader { get; set; }
        public string UserId { get; set; }
        public async Task  OnGet(int carId)
        {
            ServiceHeader =await _db.ServiceHeader.Include(s => s.Car).Include(c => c.Car.ApplicationUser).Where(c => c.CarId == carId).ToListAsync();
            UserId = _db.Car.Where(u => u.Id == carId).ToList().FirstOrDefault().UserId;
        }
    }
}
