using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class ParcellesController : Controller
{
    public IActionResult Index()
    {
        return View(DataStore.Parcelles);
    }

    public IActionResult Details(int id)
    {
        var p = DataStore.Parcelles.FirstOrDefault(x => x.Id == id);
        if (p == null) return NotFound();
        return View(p);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new NouvelleParcelleViewModel());
    }

    [HttpPost]
    public IActionResult Create(NouvelleParcelleViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var parcelle = new Parcelle
        {
            Nom = vm.Nom,
            Localisation = vm.Localisation,
            SuperficieM2 = vm.SuperficieM2,
            Type = vm.Type,
            Statut = vm.Statut,
            Description = vm.Description,
            AltitudeMetre = string.IsNullOrWhiteSpace(vm.AltitudeMetre) ? null : vm.AltitudeMetre,
            RevenusAnnuelsEstimes = vm.RevenusAnnuelsEstimes,
            CoutsAnnuelsEstimes = vm.CoutsAnnuelsEstimes,
        };

        var cultures = vm.Cultures
            .Where(c => c.EstRemplie)
            .Select(c => new Culture
            {
                Espece = c.Espece!,
                NombreArbres = c.NombreArbres,
                SuperficieHa = c.SuperficieHa,
                Anneeplantation = c.AnneePlantation,
                RendementKgHa = c.RendementKgHa,
                PrixVenteKg = c.PrixVenteKg,
                CoutProductionHa = c.CoutProductionHa,
            })
            .ToList();

        DataStore.AjouterParcelle(parcelle, cultures.Any() ? cultures : null);
        TempData["Success"] = $"Parcelle « {parcelle.Nom} » ajoutée avec succès.";
        return RedirectToAction(nameof(Details), new { id = parcelle.Id });
    }
}
