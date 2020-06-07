using Microsoft.AspNetCore.Mvc;
using HairSaloon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System;

namespace HairSaloon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSaloonContext _db;

    public ClientsController(HairSaloonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Client> model = _db.Clients.Include(clients => clients.Stylist).ToList();
      //List<Client> model1 = _db.Clients.ToList();
      //int Count = 0;
      // foreach (Client client in model)
      // {
      //   Console.WriteLine($"{client.ClientId}");
      //   if (client.Stylist.StylistId != 0)
      //   { 
      //     Count +=1;
      //   }
      // }
      // if (Count>0)
      // {
      //   Console.WriteLine("we are inside count greater than 0");
      //   return View(model);
      // }else
      // {
      //   Console.WriteLine($"we are inside count less than 0");
      //    return View(model1); 
      // }

      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      
      _db.Clients.Add(client);
      if(client.StylistId>0)
     {
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName");
      return View();
    }

    public ActionResult Details(int id)
    {
       Client thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      return View(thisClient);
    }

    public ActionResult Edit(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "StylistName");
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      return View(thisClient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id) // need to confirm with instructor as specifically "id" was not passed from cshtml
    {
      var thisClient = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}