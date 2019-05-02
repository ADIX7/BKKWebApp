using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKKWebApp.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BKKWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public IndexModel(UserCommandHandler userCommandHandler)
        {

        }
    }
}
