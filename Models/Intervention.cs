namespace ClaudeMVC.Models;

public enum TypeIntervention
{
    Taille, Traitement, Fertilisation, Irrigation, Recolte,
    Desherbage, Entretien, Amenagement, Surveillance
}

public class Intervention
{
    public int Id { get; set; }
    public int ParcelleId { get; set; }
    public string NomParcelle { get; set; } = "";
    public TypeIntervention Type { get; set; }
    public string Description { get; set; } = "";
    public DateTime DatePrevue { get; set; }
    public DateTime? DateRealisee { get; set; }
    public bool EstRealisee => DateRealisee.HasValue;
    public double CoutMain { get; set; }
    public double CoutMateriel { get; set; }
    public double CoutTotal => CoutMain + CoutMateriel;
    public int NombreJourneesHomme { get; set; }
    public string? Notes { get; set; }
    public string IconeType => Type switch {
        TypeIntervention.Taille => "bi-scissors",
        TypeIntervention.Traitement => "bi-droplet",
        TypeIntervention.Fertilisation => "bi-bag",
        TypeIntervention.Irrigation => "bi-water",
        TypeIntervention.Recolte => "bi-basket",
        TypeIntervention.Desherbage => "bi-flower1",
        TypeIntervention.Amenagement => "bi-tools",
        TypeIntervention.Surveillance => "bi-eye",
        _ => "bi-calendar-check"
    };
    public string CouleurType => Type switch {
        TypeIntervention.Taille => "primary",
        TypeIntervention.Traitement => "danger",
        TypeIntervention.Fertilisation => "success",
        TypeIntervention.Irrigation => "info",
        TypeIntervention.Recolte => "warning",
        TypeIntervention.Desherbage => "secondary",
        TypeIntervention.Amenagement => "dark",
        TypeIntervention.Surveillance => "light",
        _ => "secondary"
    };
}
