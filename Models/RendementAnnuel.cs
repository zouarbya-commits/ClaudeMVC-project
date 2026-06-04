namespace ClaudeMVC.Models;

public class RendementAnnuel
{
    public int Id { get; set; }
    public int ParcelleId { get; set; }
    public int Annee { get; set; }
    public string Espece { get; set; } = "";
    public double QuantiteKg { get; set; }
    public double PrixVenteKg { get; set; }
    public double RevenuBrut => QuantiteKg * PrixVenteKg;
    public double CoutsTotal { get; set; }
    public double MargeNette => RevenuBrut - CoutsTotal;
    public string? Notes { get; set; }
}
