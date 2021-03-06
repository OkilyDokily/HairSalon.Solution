using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(x => x.StylistId == id);
      stylist.Clients = _db.Clients.Where(x => x.StylistId == id).ToList();
      return View(stylist);
    }

    public ActionResult Delete(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(x => x.StylistId == id);
      return View(stylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(x => x.StylistId == id);
      _db.Stylists.Remove(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(x => x.StylistId == id);
      return View(stylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Entry(stylist).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}