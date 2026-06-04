namespace ClaudeMVC.Models;

public enum TypeParcelle { Arboricole, EcoTourisme, TerrainVide, Mixte }
public enum StatutParcelle { EnProduction, EnEvaluation, EnDeveloppement, Disponible }

public class Parcelle
{
    public int Id { get; set; }
    public string Nom { get; set; } = "";
    public string Localisation { get; set; } = "";
    public double SuperficieM2 { get; set; }
    public double SuperficieHa => SuperficieM2 / 10_000.0;
    public string SuperficieAffichee => SuperficieM2 >= 10_000
        ? $"{SuperficieHa:F2} ha"
        : $"{SuperficieM2:N0} m²";
    public TypeParcelle Type { get; set; }
    public StatutParcelle Statut { get; set; }
    public string Description { get; set; } = "";
    public string? AltitudeMetre { get; set; }
    public List<Culture> Cultures { get; set; } = new();
    public List<Intervention> Interventions { get; set; } = new();
    public List<RendementAnnuel> Rendements { get; set; } = new();
    public double? RevenusAnnuelsEstimes { get; set; }
    public double? CoutsAnnuelsEstimes { get; set; }
    public double MargeNette => (RevenusAnnuelsEstimes ?? 0) - (CoutsAnnuelsEstimes ?? 0);
    public string IconeBootstrap => Type switch {
        TypeParcelle.Arboricole => "bi-tree",
        TypeParcelle.EcoTourisme => "bi-compass",
        TypeParcelle.Mixte => "bi-grid",
        TypeParcelle.TerrainVide => "bi-question-circle",
        _ => "bi-geo-alt"
    };
    public string CouleurBadge => Type switch {
        TypeParcelle.Arboricole => "success",
        TypeParcelle.EcoTourisme => "info",
        TypeParcelle.Mixte => "purple",
        TypeParcelle.TerrainVide => "warning",
        _ => "secondary"
    };
    public string CouleurBadgeBootstrap => Type switch {
        TypeParcelle.Arboricole => "success",
        TypeParcelle.EcoTourisme => "info",
        TypeParcelle.Mixte => "primary",
        TypeParcelle.TerrainVide => "warning",
        _ => "secondary"
    };
    public string StripClass => Type switch {
        TypeParcelle.EcoTourisme => "strip-eco",
        TypeParcelle.Arboricole => "strip-arbo",
        TypeParcelle.Mixte => "strip-mixte",
        _ => "strip-vide"
    };
}
