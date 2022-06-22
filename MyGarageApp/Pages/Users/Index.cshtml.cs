using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGarageApp.Data;
using MyGarageApp.Models;
using MyGarageApp.Models.ViewModel;
using MyGarageApp.Utility;

namespace MyGarageApp.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;    
        }
        [BindProperty]
        public UserListViewModel UserListVM { get; set; }
        public async Task<IActionResult> OnGet(int productPage=1,string SearchEmail=null,string SearchName=null,string SearchPhone=null)
        {
            UserListVM = new UserListViewModel()
            {
                ApplicationUserList = await _db.ApplicationUser.ToListAsync()
            } ;

            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");
            param.Append("&SearchEmail=");
            if(SearchEmail != null)
            {
                param.Append(SearchEmail);
            }
            param.Append("&SearchName=");
            if (SearchName != null)
            {
                param.Append(SearchName);
            }
            param.Append("&SearchPhone=");
            if (SearchPhone != null)
            {
                param.Append(SearchPhone);
            }

            if (SearchEmail != null)
            {
                UserListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.ToLower().Contains(SearchEmail.ToLower())).ToListAsync();
            }
            else
            {
                if (SearchName != null)
                {
                    UserListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Name.ToLower().Contains(SearchName.ToLower())).ToListAsync();
                }
                else
                {
                    if (SearchPhone != null)
                    {
                        UserListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.PhoneNumber.ToLower().Contains(SearchPhone.ToLower())).ToListAsync();
                    }
                }
            }


            var count = UserListVM.ApplicationUserList.Count;
            UserListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = SD.PaginationUsersPageSize,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            UserListVM.ApplicationUserList = UserListVM.ApplicationUserList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * SD.PaginationUsersPageSize)
                .Take(SD.PaginationUsersPageSize).ToList();
            return Page();
        }
    }
}
