using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        private readonly HairSalonContext _db;
        public StylistsController(HairSalonContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Stylist> stylists = _db.Stylists.ToList();
            return View(stylists);
        }
    }
}