using ClaudeMVC.Models;

namespace ClaudeMVC.Data;

public static class DataStore
{
    private static List<Parcelle> _parcelles = new();
    private static List<Intervention> _interventions = new();
    private static List<RendementAnnuel> _rendements = new();
    private static List<ScenarioEcoTourisme> _scenarios = new();
    private static int _nextInterventionId = 100;

    static DataStore() => Seed();

    public static List<Parcelle> Parcelles => _parcelles;
    public static List<Intervention> Interventions => _interventions;
    public static List<RendementAnnuel> Rendements => _rendements;
    public static List<ScenarioEcoTourisme> Scenarios => _scenarios;

    public static void AjouterIntervention(Intervention i)
    {
        i.Id = _nextInterventionId++;
        var p = _parcelles.FirstOrDefault(p => p.Id == i.ParcelleId);
        i.NomParcelle = p?.Nom ?? "";
        _interventions.Add(i);
        p?.Interventions.Add(i);
    }

    public static void MarquerRealisee(int id)
    {
        var i = _interventions.FirstOrDefault(x => x.Id == id);
        if (i != null) i.DateRealisee = DateTime.Today;
    }

    private static void Seed()
    {
        var today = DateTime.Today;

        // ─── PARCELLE 1 : Sentier Aït Benhaddou (Éco-tourisme) ───────────
        var p1 = new Parcelle
        {
            Id = 1,
            Nom = "Sentier Aït Benhaddou",
            Localisation = "Province de Ouarzazate",
            SuperficieHa = 8.5,
            Type = TypeParcelle.EcoTourisme,
            Statut = StatutParcelle.EnDeveloppement,
            AltitudeMetre = "1 200 m",
            Description = "Terrain en zone présaharienne avec vue panoramique sur la Vallée du Drâa. Potentiel fort pour circuits de trekking et bivouacs écologiques.",
            RevenusAnnuelsEstimes = 85_000,
            CoutsAnnuelsEstimes = 38_000,
        };

        // ─── PARCELLE 2 : Sentier Aït Bouguemez (Éco-tourisme) ──────────
        var p2 = new Parcelle
        {
            Id = 2,
            Nom = "Sentier Vallée des Ait Bouguemez",
            Localisation = "Province d'Azilal, Haut Atlas",
            SuperficieHa = 12.0,
            Type = TypeParcelle.EcoTourisme,
            Statut = StatutParcelle.EnEvaluation,
            AltitudeMetre = "1 900 m",
            Description = "Terrain en haute montagne dans la « Vallée Heureuse ». Accès à des sentiers GR traversant villages berbères et paysages glaciaires. Capacité hébergement tente/yourte.",
            RevenusAnnuelsEstimes = 120_000,
            CoutsAnnuelsEstimes = 52_000,
        };

        // ─── PARCELLE 3 : Domaine Arboricole Immouzer (Arboricole) ───────
        var p3 = new Parcelle
        {
            Id = 3,
            Nom = "Domaine Arboricole Immouzer",
            Localisation = "Immouzer Marmoucha, Province de Boulemane",
            SuperficieHa = 5.2,
            Type = TypeParcelle.Arboricole,
            Statut = StatutParcelle.EnProduction,
            AltitudeMetre = "1 500 m",
            Description = "Verger mixte pommes Golden/Gala (3 ha) et oliviers Picholine marocaine (2,2 ha). Irrigation gravitaire. Accès à une coopérative locale.",
            RevenusAnnuelsEstimes = 96_000,
            CoutsAnnuelsEstimes = 41_500,
        };
        p3.Cultures.AddRange(new[]
        {
            new Culture { Id = 1, ParcelleId = 3, Espece = "Pommier Golden", SuperficieHa = 1.8, NombreArbres = 360, Anneeplantation = 2015, RendementKgHa = 18_000, PrixVenteKg = 2.8, CoutProductionHa = 12_000 },
            new Culture { Id = 2, ParcelleId = 3, Espece = "Pommier Gala", SuperficieHa = 1.2, NombreArbres = 240, Anneeplantation = 2018, RendementKgHa = 16_000, PrixVenteKg = 3.2, CoutProductionHa = 11_500 },
            new Culture { Id = 3, ParcelleId = 3, Espece = "Olivier Picholine", SuperficieHa = 2.2, NombreArbres = 330, Anneeplantation = 2010, RendementKgHa = 3_200, PrixVenteKg = 4.5, CoutProductionHa = 4_800 },
        });

        // ─── PARCELLE 4 : Terrain Vide Oulmès ───────────────────────────
        var p4 = new Parcelle
        {
            Id = 4,
            Nom = "Terrain Vide Oulmès",
            Localisation = "Oulmès, Province de Khénifra",
            SuperficieHa = 3.0,
            Type = TypeParcelle.TerrainVide,
            Statut = StatutParcelle.Disponible,
            AltitudeMetre = "1 100 m",
            Description = "Terrain non exploité en zone tempérée humide. Plusieurs scénarios en cours d'évaluation : plantation fruitière, location, ou intégration à un circuit randonnée.",
            RevenusAnnuelsEstimes = 0,
            CoutsAnnuelsEstimes = 3_500,
        };

        _parcelles = new List<Parcelle> { p1, p2, p3, p4 };

        // ─── INTERVENTIONS ────────────────────────────────────────────────
        var interventions = new List<Intervention>
        {
            // Parcelle 3 - Domaine Arboricole
            new() { Id = 1,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Taille,        Description = "Taille de fructification pommiers Golden", DatePrevue = new DateTime(2026,1,15), DateRealisee = new DateTime(2026,1,18), CoutMain = 1_800, CoutMateriel = 200, NombreJourneesHomme = 6 },
            new() { Id = 2,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Taille,        Description = "Taille pommiers Gala", DatePrevue = new DateTime(2026,1,22), DateRealisee = new DateTime(2026,1,24), CoutMain = 1_200, CoutMateriel = 150, NombreJourneesHomme = 4 },
            new() { Id = 3,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Traitement,    Description = "Bouillie bordelaise – tavelure pommiers", DatePrevue = new DateTime(2026,3,10), DateRealisee = new DateTime(2026,3,10), CoutMain = 600, CoutMateriel = 1_200, NombreJourneesHomme = 2 },
            new() { Id = 4,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Fertilisation, Description = "Apport engrais azoté (urée) pré-floraison", DatePrevue = new DateTime(2026,3,20), DateRealisee = new DateTime(2026,3,22), CoutMain = 500, CoutMateriel = 900, NombreJourneesHomme = 2 },
            new() { Id = 5,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Traitement,    Description = "Insecticide – carpocapse des pommes (1ère génération)", DatePrevue = new DateTime(2026,5,15), CoutMain = 700, CoutMateriel = 1_500, NombreJourneesHomme = 2 },
            new() { Id = 6,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Traitement,    Description = "Insecticide – carpocapse (2ème génération)", DatePrevue = new DateTime(2026,6,20), CoutMain = 700, CoutMateriel = 1_500, NombreJourneesHomme = 2 },
            new() { Id = 7,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Irrigation,    Description = "Réglage système gravitaire – début été", DatePrevue = new DateTime(2026,6,1), CoutMain = 400, CoutMateriel = 300, NombreJourneesHomme = 1 },
            new() { Id = 8,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Recolte,       Description = "Récolte pommes Gala (maturité précoce)", DatePrevue = new DateTime(2026,8,25), CoutMain = 4_500, CoutMateriel = 500, NombreJourneesHomme = 15 },
            new() { Id = 9,  ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Recolte,       Description = "Récolte pommes Golden", DatePrevue = new DateTime(2026,9,20), CoutMain = 6_000, CoutMateriel = 600, NombreJourneesHomme = 20 },
            new() { Id = 10, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Recolte,       Description = "Récolte olives – huile et conserves", DatePrevue = new DateTime(2026,11,10), CoutMain = 3_500, CoutMateriel = 400, NombreJourneesHomme = 12 },
            new() { Id = 11, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Taille,        Description = "Taille oliviers (alternance)", DatePrevue = new DateTime(2026,12,5), CoutMain = 1_500, CoutMateriel = 200, NombreJourneesHomme = 5 },
            // Parcelle 1 – Sentier Aït Benhaddou
            new() { Id = 12, ParcelleId = 1, NomParcelle = p1.Nom, Type = TypeIntervention.Amenagement,   Description = "Balisage et signalétique sentier principal (4 km)", DatePrevue = new DateTime(2026,4,1), CoutMain = 3_000, CoutMateriel = 5_000, NombreJourneesHomme = 10 },
            new() { Id = 13, ParcelleId = 1, NomParcelle = p1.Nom, Type = TypeIntervention.Amenagement,   Description = "Installation 3 aires de bivouac écologique", DatePrevue = new DateTime(2026,5,1), CoutMain = 8_000, CoutMateriel = 12_000, NombreJourneesHomme = 25 },
            new() { Id = 14, ParcelleId = 1, NomParcelle = p1.Nom, Type = TypeIntervention.Entretien,     Description = "Nettoyage post-saison et entretien sentiers", DatePrevue = new DateTime(2026,10,15), CoutMain = 1_200, CoutMateriel = 300, NombreJourneesHomme = 4 },
            // Parcelle 2 – Ait Bouguemez
            new() { Id = 15, ParcelleId = 2, NomParcelle = p2.Nom, Type = TypeIntervention.Amenagement,   Description = "Étude topographique et tracé circuit GR", DatePrevue = new DateTime(2026,4,15), CoutMain = 5_000, CoutMateriel = 2_000, NombreJourneesHomme = 8 },
            new() { Id = 16, ParcelleId = 2, NomParcelle = p2.Nom, Type = TypeIntervention.Surveillance,  Description = "Comptage faune/flore – biodiversité", DatePrevue = new DateTime(2026,5,20), CoutMain = 2_000, CoutMateriel = 500, NombreJourneesHomme = 5 },
            // Parcelle 4 – Terrain Vide
            new() { Id = 17, ParcelleId = 4, NomParcelle = p4.Nom, Type = TypeIntervention.Surveillance,  Description = "Analyse de sol (pH, texture, matière organique)", DatePrevue = new DateTime(2026,6,10), CoutMain = 0, CoutMateriel = 2_500, NombreJourneesHomme = 1 },
            new() { Id = 18, ParcelleId = 4, NomParcelle = p4.Nom, Type = TypeIntervention.Amenagement,   Description = "Étude hydrologique – ressource eau", DatePrevue = new DateTime(2026,7,1), CoutMain = 1_500, CoutMateriel = 1_000, NombreJourneesHomme = 3 },
        };

        foreach (var i in interventions)
        {
            _interventions.Add(i);
            _parcelles.FirstOrDefault(p => p.Id == i.ParcelleId)?.Interventions.Add(i);
        }
        _nextInterventionId = 100;

        // ─── RENDEMENTS HISTORIQUES ───────────────────────────────────────
        _rendements = new List<RendementAnnuel>
        {
            new() { Id=1, ParcelleId=3, Annee=2023, Espece="Pommier Golden",   QuantiteKg=29_000, PrixVenteKg=2.5, CoutsTotal=21_600, Notes="Bonne année, peu de tavelure" },
            new() { Id=2, ParcelleId=3, Annee=2023, Espece="Pommier Gala",     QuantiteKg=17_000, PrixVenteKg=3.0, CoutsTotal=13_800, Notes="" },
            new() { Id=3, ParcelleId=3, Annee=2023, Espece="Olivier Picholine", QuantiteKg=6_500,  PrixVenteKg=4.2, CoutsTotal=10_560, Notes="Bonne campagne oléicole" },
            new() { Id=4, ParcelleId=3, Annee=2024, Espece="Pommier Golden",   QuantiteKg=32_000, PrixVenteKg=2.8, CoutsTotal=22_500, Notes="Rendement record" },
            new() { Id=5, ParcelleId=3, Annee=2024, Espece="Pommier Gala",     QuantiteKg=19_000, PrixVenteKg=3.1, CoutsTotal=14_200, Notes="" },
            new() { Id=6, ParcelleId=3, Annee=2024, Espece="Olivier Picholine", QuantiteKg=4_200,  PrixVenteKg=4.5, CoutsTotal=10_560, Notes="Année de repos physiologique" },
            new() { Id=7, ParcelleId=3, Annee=2025, Espece="Pommier Golden",   QuantiteKg=30_500, PrixVenteKg=2.9, CoutsTotal=22_000, Notes="" },
            new() { Id=8, ParcelleId=3, Annee=2025, Espece="Pommier Gala",     QuantiteKg=18_500, PrixVenteKg=3.2, CoutsTotal=13_900, Notes="" },
            new() { Id=9, ParcelleId=3, Annee=2025, Espece="Olivier Picholine", QuantiteKg=7_000,  PrixVenteKg=4.6, CoutsTotal=10_560, Notes="Excellente campagne" },
        };

        // ─── SCÉNARIOS ÉCO-TOURISME ───────────────────────────────────────
        _scenarios = new List<ScenarioEcoTourisme>
        {
            new() { Id=1, ParcelleId=1, Nom="Entrées & Bivouac (P1)", Modele=ModeleRevenu.EntreesCircuits,
                VisiteursParAn=800, PrixEntreeParVisiteur=50, NombreCircuits=60, PrixCircuitParPersNne=80,
                NombreNuitees=400, PrixNuitee=120, NombreGuidesEmployes=2, SalaireGuide=3_000,
                InvestissementInitial=150_000, CoutsAnnuelsExploitation=20_000 },
            new() { Id=2, ParcelleId=1, Nom="Hébergement Premium (P1)", Modele=ModeleRevenu.HebergementLeger,
                VisiteursParAn=400, PrixEntreeParVisiteur=0, NombreCircuits=0, PrixCircuitParPersNne=0,
                NombreNuitees=800, PrixNuitee=350, NombreGuidesEmployes=3, SalaireGuide=3_500,
                InvestissementInitial=280_000, CoutsAnnuelsExploitation=45_000 },
            new() { Id=3, ParcelleId=2, Nom="Circuit Trekking GR (P2)", Modele=ModeleRevenu.GuidesDeTrekking,
                VisiteursParAn=1_200, PrixEntreeParVisiteur=30, NombreCircuits=120, PrixCircuitParPersNne=150,
                NombreNuitees=600, PrixNuitee=180, NombreGuidesEmployes=4, SalaireGuide=3_200,
                InvestissementInitial=200_000, CoutsAnnuelsExploitation=30_000 },
            new() { Id=4, ParcelleId=2, Nom="Éco-lodge & Randonnée (P2)", Modele=ModeleRevenu.Combine,
                VisiteursParAn=900, PrixEntreeParVisiteur=40, NombreCircuits=80, PrixCircuitParPersNne=200,
                NombreNuitees=1_200, PrixNuitee=280, NombreGuidesEmployes=5, SalaireGuide=3_500,
                InvestissementInitial=400_000, CoutsAnnuelsExploitation=65_000 },
        };
    }
}
