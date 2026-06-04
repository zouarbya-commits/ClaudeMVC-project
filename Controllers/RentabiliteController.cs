using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class RentabiliteController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Parcelles = DataStore.Parcelles;
        ViewBag.Rendements = DataStore.Rendements;
        return View();
    }

    public IActionResult Parcelle(int id)
    {
        var p = DataStore.Parcelles.FirstOrDefault(x => x.Id == id);
        if (p == null) return NotFound();
        ViewBag.Rendements = DataStore.Rendements.Where(r => r.ParcelleId == id).OrderBy(r => r.Annee).ToList();
        return View(p);
    }

    public IActionResult Comparaison()
    {
        ViewBag.Parcelles = DataStore.Parcelles.Where(p => p.Type == TypeParcelle.Arboricole).ToList();
        ViewBag.Rendements = DataStore.Rendements;
        return View();
    }
}
