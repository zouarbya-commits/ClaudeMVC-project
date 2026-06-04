using System.ComponentModel.DataAnnotations;

namespace ClaudeMVC.Models;

public class NouvelleParcelleViewModel
{
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [Display(Name = "Nom de la parcelle")]
    public string Nom { get; set; } = "";

    [Required(ErrorMessage = "La localisation est obligatoire")]
    [Display(Name = "Localisation")]
    public string Localisation { get; set; } = "";

    [Required, Range(1, 10_000_000, ErrorMessage = "Superficie invalide")]
    [Display(Name = "Superficie (m²)")]
    public double SuperficieM2 { get; set; }

    [Required]
    [Display(Name = "Type de parcelle")]
    public TypeParcelle Type { get; set; }

    [Required]
    [Display(Name = "Statut")]
    public StatutParcelle Statut { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; } = "";

    [Display(Name = "Altitude")]
    public string? AltitudeMetre { get; set; }

    [Display(Name = "Revenus annuels estimés (MAD)")]
    [Range(0, double.MaxValue)]
    public double RevenusAnnuelsEstimes { get; set; }

    [Display(Name = "Coûts annuels estimés (MAD)")]
    [Range(0, double.MaxValue)]
    public double CoutsAnnuelsEstimes { get; set; }

    // Cultures (jusqu'à 3)
    public List<NouvelleCulture> Cultures { get; set; } = new()
    {
        new NouvelleCulture(),
        new NouvelleCulture(),
        new NouvelleCulture(),
    };
}

public class NouvelleCulture
{
    [Display(Name = "Espèce")]
    public string? Espece { get; set; }

    [Display(Name = "Nombre d'arbres")]
    [Range(0, 100_000)]
    public int NombreArbres { get; set; }

    [Display(Name = "Superficie (ha)")]
    [Range(0, 1_000)]
    public double SuperficieHa { get; set; }

    [Display(Name = "Année de plantation")]
    [Range(1900, 2100)]
    public int AnneePlantation { get; set; } = DateTime.Now.Year;

    [Display(Name = "Rendement estimé (kg/ha)")]
    [Range(0, 100_000)]
    public double RendementKgHa { get; set; }

    [Display(Name = "Prix vente (MAD/kg)")]
    [Range(0, 1_000)]
    public double PrixVenteKg { get; set; }

    [Display(Name = "Coût production (MAD/ha)")]
    [Range(0, 500_000)]
    public double CoutProductionHa { get; set; }

    public bool EstRemplie => !string.IsNullOrWhiteSpace(Espece) && NombreArbres > 0;
}
