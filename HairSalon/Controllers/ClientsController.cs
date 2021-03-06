using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;
    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Client> clients = _db.Clients.Include(x => x.Stylist).ToList();
      return View(clients);
    }

    public ActionResult Create()
    {
      ViewBag.AnyStylists = _db.Stylists.ToList().Count != 0;
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "LastName");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Client client)
    {
      _db.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Client client = _db.Clients.FirstOrDefault(x => x.ClientId == id);
      return View(client);
    }

    public ActionResult Delete(int id)
    {
      Client client = _db.Clients.FirstOrDefault(x => x.ClientId == id);
      return View(client);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client client = _db.Clients.FirstOrDefault(x => x.ClientId == id);
      _db.Clients.Remove(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "LastName");
      Client client = _db.Clients.FirstOrDefault(x => x.ClientId == id);
      return View(client);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}