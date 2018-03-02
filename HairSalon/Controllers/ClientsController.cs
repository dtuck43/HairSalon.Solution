using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalonApp.Models;

namespace HairSalonApp.Controllers
{
    public class ClientsController : Controller
    {
      [HttpGet("/clients")]
      public ActionResult AllClients()
      {
        List<Client> allClients = Client.GetAll();
        return View("AllClients", allClients);
      }

      [HttpGet("/clients/new")]
      public ActionResult NewClient()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpPost("/clients")]
      public ActionResult AddClient()
      {
        string name = Request.Form["clientName"];
        int number = Int32.Parse(Request.Form["clientNumber"]);
        int assignedStylist = Int32.Parse(Request.Form["assignedStylist"]);
        Client newClient = new Client(name, number, assignedStylist);
        newClient.Save();
        return RedirectToAction("AllClients");
      }

      [HttpGet("/clients/edit/{id}")]
      public ActionResult EditClientForm(int id)
      {
        Client thisClient = Client.Find(id);
        return RedirectToAction("AllClients", thisClient);
      }

      [HttpPost("/clients/edit/{id}")]
      public ActionResult EditClient(int id)
      {
        Client thisClient = Client.Find(id);
        thisClient.Edit(Request.Form["edit-clientName"], Int32.Parse(Request.Form["edit-clientNumber"]), Int32.Parse(Request.Form["edit-assignedStylist"]));
        return RedirectToAction("AllClients");
      }

      [HttpPost("/clients/delete/{id}")]
      public ActionResult DeleteClient(int id)
      {
        Client thisClient = Client.Find(id);
        thisClient.Delete();
        return RedirectToAction("AllStylists", "Stylists");
      }

      [HttpPost("/clients/delete/all")]
      public ActionResult DeleteAllClients()
      {
        Client.DeleteAll();
        return RedirectToAction("AllClients");
      }
    }

}
