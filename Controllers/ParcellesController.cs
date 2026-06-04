using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class ParcellesController : Controller
{
    public IActionResult Index()
    {
        var parcelles = DataStore.Parcelles;
        return View(parcelles);
    }

    public IActionResult Details(int id)
    {
        var p = DataStore.Parcelles.FirstOrDefault(x => x.Id == id);
        if (p == null) return NotFound();
        return View(p);
    }
}
