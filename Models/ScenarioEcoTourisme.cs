namespace ClaudeMVC.Models;

public enum ModeleRevenu { EntreesCircuits, GuidesDeTrekking, HebergementLeger, Combine }

public class ScenarioEcoTourisme
{
    public int Id { get; set; }
    public int ParcelleId { get; set; }
    public string Nom { get; set; } = "";
    public ModeleRevenu Modele { get; set; }
    public string Description { get; set; } = "";

    // Hypothèses
    public int VisiteursParAn { get; set; }
    public double PrixEntreeParVisiteur { get; set; }
    public int NombreCircuits { get; set; }
    public double PrixCircuitParPersNne { get; set; }
    public int NombreNuitees { get; set; }
    public double PrixNuitee { get; set; }
    public int NombreGuidesEmployes { get; set; }
    public double SalaireGuide { get; set; }

    // Investissement initial
    public double InvestissementInitial { get; set; }
    public double CoutsAnnuelsExploitation { get; set; }

    // Calculs
    public double RevenuEntrees => VisiteursParAn * PrixEntreeParVisiteur;
    public double RevenuCircuits => NombreCircuits * PrixCircuitParPersNne * (VisiteursParAn / Math.Max(NombreCircuits, 1));
    public double RevenuHebergement => NombreNuitees * PrixNuitee;
    public double RevenuBrutTotal => RevenuEntrees + RevenuCircuits + RevenuHebergement;
    public double CoutPersonnel => NombreGuidesEmployes * SalaireGuide * 12;
    public double CoutsTotal => CoutsAnnuelsExploitation + CoutPersonnel;
    public double MargeNette => RevenuBrutTotal - CoutsTotal;
    public double RetourInvestissement => InvestissementInitial > 0 ? InvestissementInitial / Math.Max(MargeNette, 1) : 0;
}
