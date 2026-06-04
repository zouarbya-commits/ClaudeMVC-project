namespace ClaudeMVC.Models;

public class Culture
{
    public int Id { get; set; }
    public int ParcelleId { get; set; }
    public string Espece { get; set; } = "";
    public double SuperficieHa { get; set; }
    public int NombreArbres { get; set; }
    public int Anneeplantation { get; set; }
    public double RendementKgHa { get; set; }
    public double PrixVenteKg { get; set; }
    public double RevenuBrutEstime => SuperficieHa * RendementKgHa * PrixVenteKg;
    public double CoutProductionHa { get; set; }
    public double MargeNette => RevenuBrutEstime - (CoutProductionHa * SuperficieHa);
    public string Icone => Espece.ToLower() switch {
        var s when s.Contains("pomme") => "🍎",
        var s when s.Contains("olive") => "🫒",
        var s when s.Contains("noyer") => "🌰",
        var s when s.Contains("cerisier") => "🍒",
        _ => "🌿"
    };
}
