using ClaudeMVC.Data;
using ClaudeMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeMVC.Controllers;

public class EvaluationController : Controller
{
    public IActionResult Index()
    {
        var terrainVide = DataStore.Parcelles.FirstOrDefault(p => p.Type == TypeParcelle.TerrainVide);
        return View(terrainVide);
    }
}
