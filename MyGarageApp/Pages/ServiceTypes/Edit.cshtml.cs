﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGarageApp.Data;
using MyGarageApp.Models;
using MyGarageApp.Utility;

namespace MyGarageApp.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EditModel : PageModel
    {
        private readonly MyGarageApp.Data.ApplicationDbContext _db;

        public EditModel(MyGarageApp.Data.ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType = await _db.ServiceType.FirstOrDefaultAsync(m => m.Id == id);

            if (ServiceType == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var serviceFromDb = await _db.ServiceType.FirstOrDefaultAsync(s => s.Id == ServiceType.Id );
            serviceFromDb.Name = ServiceType.Name;
            serviceFromDb.Price = ServiceType.Price;
            await _db.SaveChangesAsync();

        

            return RedirectToPage("./Index");
        }

    }
}
