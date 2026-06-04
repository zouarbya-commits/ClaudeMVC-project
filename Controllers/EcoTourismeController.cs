using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class EcoTourismeController : Controller
{
    public IActionResult Index()
    {
        var parcelles = DataStore.Parcelles.Where(p => p.Type == TypeParcelle.EcoTourisme).ToList();
        var scenarios = DataStore.Scenarios;
        ViewBag.Scenarios = scenarios;
        return View(parcelles);
    }

    public IActionResult BusinessPlan(int parcelleId)
    {
        var p = DataStore.Parcelles.FirstOrDefault(x => x.Id == parcelleId);
        if (p == null) return NotFound();
        var scenarios = DataStore.Scenarios.Where(s => s.ParcelleId == parcelleId).ToList();
        ViewBag.Scenarios = scenarios;
        return View(p);
    }

    public IActionResult Reglementation()
    {
        return View();
    }
}
