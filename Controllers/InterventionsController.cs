using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class InterventionsController : Controller
{
    public IActionResult Index(int? parcelleId, bool? enRetard)
    {
        var query = DataStore.Interventions.AsQueryable();
        if (parcelleId.HasValue) query = query.Where(i => i.ParcelleId == parcelleId.Value);
        if (enRetard == true) query = query.Where(i => !i.EstRealisee && i.DatePrevue < DateTime.Today);
        ViewBag.Parcelles = DataStore.Parcelles;
        ViewBag.SelectedParcelle = parcelleId;
        return View(query.OrderBy(i => i.DatePrevue).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Parcelles = DataStore.Parcelles;
        return View(new Intervention { DatePrevue = DateTime.Today.AddDays(7) });
    }

    [HttpPost]
    public IActionResult Create(Intervention model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Parcelles = DataStore.Parcelles;
            return View(model);
        }
        DataStore.AjouterIntervention(model);
        TempData["Success"] = "Intervention ajoutée avec succès.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult MarquerRealisee(int id)
    {
        DataStore.MarquerRealisee(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Calendrier()
    {
        ViewBag.Interventions = DataStore.Interventions;
        return View();
    }
}
