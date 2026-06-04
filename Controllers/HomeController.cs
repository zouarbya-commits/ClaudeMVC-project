using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var today = DateTime.Today;
        var vm = new DashboardViewModel
        {
            Parcelles = DataStore.Parcelles,
            TotalSuperficieHa = DataStore.Parcelles.Sum(p => p.SuperficieHa),
            TotalRevenusEstimes = DataStore.Parcelles.Sum(p => p.RevenusAnnuelsEstimes ?? 0),
            TotalCoutsEstimes = DataStore.Parcelles.Sum(p => p.CoutsAnnuelsEstimes ?? 0),
            ProchainesInterventions = DataStore.Interventions
                .Where(i => !i.EstRealisee && i.DatePrevue >= today)
                .OrderBy(i => i.DatePrevue)
                .Take(6)
                .ToList(),
            NombreInterventionsMois = DataStore.Interventions
                .Count(i => !i.EstRealisee && i.DatePrevue.Month == today.Month && i.DatePrevue.Year == today.Year),
            NombreInterventionsEnRetard = DataStore.Interventions
                .Count(i => !i.EstRealisee && i.DatePrevue < today),
            DerniersRendements = DataStore.Rendements.OrderByDescending(r => r.Annee).Take(6).ToList(),
        };
        return View(vm);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
}
