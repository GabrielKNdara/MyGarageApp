            using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGarageApp.Models.ViewModel
{
    public class UserListViewModel
    {
        public IList<ApplicationUser> ApplicationUserList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
