namespace ClaudeMVC.Models;

public class DashboardViewModel
{
    public List<Parcelle> Parcelles { get; set; } = new();
    public List<Intervention> ProchainesInterventions { get; set; } = new();
    public double TotalSuperficieHa { get; set; }
    public double TotalRevenusEstimes { get; set; }
    public double TotalCoutsEstimes { get; set; }
    public double MargeNetteTotale => TotalRevenusEstimes - TotalCoutsEstimes;
    public int NombreInterventionsMois { get; set; }
    public int NombreInterventionsEnRetard { get; set; }
    public List<RendementAnnuel> DerniersRendements { get; set; } = new();
}
