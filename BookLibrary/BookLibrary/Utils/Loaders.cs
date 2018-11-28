using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookLibrary.Models;

namespace BookLibrary.Utils
{
    public static class Loaders
    {
        private static ApplicationDbContext dbApp = new ApplicationDbContext();

        public static IEnumerable<SelectListItem> RolesToSelectItem()
        {
            return dbApp.Roles.Select(x => new SelectListItem { Value = x.Name, Text = x.Name });
        }
    }
}