using BroadageSportsCaseWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BroadageSportsCaseWebApp.Controllers
{
    public class BroadageSportsMatchListController : Controller
    {
        BroadageSportsContext _context;

        public BroadageSportsMatchListController(BroadageSportsContext context)=>_context=context;
       

        public IActionResult Index()
        {
            var list = _context.Matches.ToList();
            return View(list);
        }

      
    }
}