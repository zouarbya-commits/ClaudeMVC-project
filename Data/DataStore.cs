using ClaudeMVC.Models;

namespace ClaudeMVC.Data;

public static class DataStore
{
    private static List<Parcelle> _parcelles = new();
    private static List<Intervention> _interventions = new();
    private static List<RendementAnnuel> _rendements = new();
    private static List<ScenarioEcoTourisme> _scenarios = new();
    private static int _nextParcelleId = 10;
    private static int _nextCultureId = 10;
    private static int _nextInterventionId = 100;

    static DataStore() => Seed();

    public static List<Parcelle> Parcelles => _parcelles;
    public static List<Intervention> Interventions => _interventions;
    public static List<RendementAnnuel> Rendements => _rendements;
    public static List<ScenarioEcoTourisme> Scenarios => _scenarios;

    public static void AjouterParcelle(Parcelle p, List<Culture>? cultures = null)
    {
        p.Id = _nextParcelleId++;
        if (cultures != null)
        {
            foreach (var c in cultures)
            {
                c.Id = _nextCultureId++;
                c.ParcelleId = p.Id;
                p.Cultures.Add(c);
            }
        }
        _parcelles.Add(p);
    }

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
        // ─── TERRAIN 1 : 3 400 m² — Vide, potentiel mixte touristique/agricole ──
        var p1 = new Parcelle
        {
            Id = 1,
            Nom = "Terrain A",
            Localisation = "À préciser",
            SuperficieM2 = 3_400,
            Type = TypeParcelle.Mixte,
            Statut = StatutParcelle.EnEvaluation,
            Description = "Terrain de 3 400 m² non exploité. Potentiel mixte : activité touristique légère (point de vue, aire de pique-nique) et/ou plantation fruitière intensive.",
            RevenusAnnuelsEstimes = 0,
            CoutsAnnuelsEstimes = 1_200,
        };

        // ─── TERRAIN 2 : 1 064 m² — 45 pommiers, potentiel mixte ──────────────
        var p2 = new Parcelle
        {
            Id = 2,
            Nom = "Terrain B – Verger pommiers",
            Localisation = "À préciser",
            SuperficieM2 = 1_064,
            Type = TypeParcelle.Mixte,
            Statut = StatutParcelle.EnProduction,
            Description = "Parcelle de 1 064 m² avec 45 pommiers en production. Double vocation : production fruitière et accueil de visiteurs (cueillette, agritourisme).",
            RevenusAnnuelsEstimes = 4_050,
            CoutsAnnuelsEstimes = 1_800,
        };
        p2.Cultures.Add(new Culture
        {
            Id = 2, ParcelleId = 2,
            Espece = "Pommier",
            SuperficieHa = 1_064.0 / 10_000.0,
            NombreArbres = 45,
            Anneeplantation = 2020,
            RendementKgHa = 12_000,
            PrixVenteKg = 3.0,
            CoutProductionHa = 8_000,
        });

        // ─── TERRAIN 3 : 1,5 ha — Arboricole (500 pommes + 100 olives) ─────────
        var p3 = new Parcelle
        {
            Id = 3,
            Nom = "Terrain C – Verger mixte",
            Localisation = "À préciser",
            SuperficieM2 = 15_000,
            Type = TypeParcelle.Arboricole,
            Statut = StatutParcelle.EnProduction,
            Description = "Verger de 1,5 ha avec 500 pommiers et 100 oliviers. Parcelle principale de production fruitière et oléicole.",
            RevenusAnnuelsEstimes = 47_500,
            CoutsAnnuelsEstimes = 18_000,
        };
        p3.Cultures.AddRange(new[]
        {
            new Culture
            {
                Id = 3, ParcelleId = 3,
                Espece = "Pommier",
                SuperficieHa = 1.2,
                NombreArbres = 500,
                Anneeplantation = 2018,
                RendementKgHa = 15_000,
                PrixVenteKg = 3.0,
                CoutProductionHa = 9_000,
            },
            new Culture
            {
                Id = 4, ParcelleId = 3,
                Espece = "Olivier",
                SuperficieHa = 0.3,
                NombreArbres = 100,
                Anneeplantation = 2015,
                RendementKgHa = 3_000,
                PrixVenteKg = 5.0,
                CoutProductionHa = 5_000,
            },
        });

        // ─── TERRAIN 4 : 3 ha — Vide ────────────────────────────────────────
        var p4 = new Parcelle
        {
            Id = 4,
            Nom = "Terrain D",
            Localisation = "À préciser",
            SuperficieM2 = 30_000,
            Type = TypeParcelle.TerrainVide,
            Statut = StatutParcelle.Disponible,
            Description = "Terrain de 3 ha non exploité. Surface suffisante pour plantation arboricole à grande échelle ou projet éco-touristique (circuits, campement).",
            RevenusAnnuelsEstimes = 0,
            CoutsAnnuelsEstimes = 2_500,
        };

        _parcelles = new List<Parcelle> { p1, p2, p3, p4 };
        _nextParcelleId = 10;
        _nextCultureId = 10;

        // ─── INTERVENTIONS ────────────────────────────────────────────────────
        var interventions = new List<Intervention>
        {
            // Terrain B – 45 pommiers
            new() { Id = 1, ParcelleId = 2, NomParcelle = p2.Nom, Type = TypeIntervention.Taille,
                Description = "Taille de formation – 45 pommiers", DatePrevue = new DateTime(2026,1,20),
                DateRealisee = new DateTime(2026,1,22), CoutMain = 600, CoutMateriel = 50, NombreJourneesHomme = 2 },
            new() { Id = 2, ParcelleId = 2, NomParcelle = p2.Nom, Type = TypeIntervention.Traitement,
                Description = "Bouillie bordelaise – prévention tavelure", DatePrevue = new DateTime(2026,3,15),
                DateRealisee = new DateTime(2026,3,15), CoutMain = 150, CoutMateriel = 250, NombreJourneesHomme = 1 },
            new() { Id = 3, ParcelleId = 2, NomParcelle = p2.Nom, Type = TypeIntervention.Recolte,
                Description = "Récolte pommiers (cueillette manuelle)", DatePrevue = new DateTime(2026,9,15),
                CoutMain = 800, CoutMateriel = 100, NombreJourneesHomme = 3 },

            // Terrain C – Verger mixte 1,5 ha
            new() { Id = 4, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Taille,
                Description = "Taille de fructification – 500 pommiers", DatePrevue = new DateTime(2026,1,10),
                DateRealisee = new DateTime(2026,1,14), CoutMain = 4_500, CoutMateriel = 300, NombreJourneesHomme = 15 },
            new() { Id = 5, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Fertilisation,
                Description = "Apport engrais azoté pré-floraison", DatePrevue = new DateTime(2026,3,10),
                DateRealisee = new DateTime(2026,3,12), CoutMain = 800, CoutMateriel = 1_400, NombreJourneesHomme = 2 },
            new() { Id = 6, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Traitement,
                Description = "Insecticide carpocapse (1ère génération)", DatePrevue = new DateTime(2026,5,15),
                CoutMain = 1_000, CoutMateriel = 2_000, NombreJourneesHomme = 3 },
            new() { Id = 7, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Irrigation,
                Description = "Mise en route irrigation – début saison sèche", DatePrevue = new DateTime(2026,6,1),
                CoutMain = 400, CoutMateriel = 300, NombreJourneesHomme = 1 },
            new() { Id = 8, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Recolte,
                Description = "Récolte pommiers (500 arbres)", DatePrevue = new DateTime(2026,9,10),
                CoutMain = 8_000, CoutMateriel = 700, NombreJourneesHomme = 28 },
            new() { Id = 9, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Recolte,
                Description = "Récolte olives (100 arbres)", DatePrevue = new DateTime(2026,11,5),
                CoutMain = 1_800, CoutMateriel = 300, NombreJourneesHomme = 6 },
            new() { Id = 10, ParcelleId = 3, NomParcelle = p3.Nom, Type = TypeIntervention.Taille,
                Description = "Taille oliviers (annuelle)", DatePrevue = new DateTime(2026,12,1),
                CoutMain = 1_200, CoutMateriel = 150, NombreJourneesHomme = 4 },

            // Terrain A – 3 400 m² en évaluation
            new() { Id = 11, ParcelleId = 1, NomParcelle = p1.Nom, Type = TypeIntervention.Surveillance,
                Description = "Analyse de sol et relevé topographique", DatePrevue = new DateTime(2026,6,15),
                CoutMain = 0, CoutMateriel = 2_000, NombreJourneesHomme = 1 },

            // Terrain D – 3 ha vide
            new() { Id = 12, ParcelleId = 4, NomParcelle = p4.Nom, Type = TypeIntervention.Surveillance,
                Description = "Étude faisabilité – plantation ou éco-tourisme", DatePrevue = new DateTime(2026,7,1),
                CoutMain = 1_500, CoutMateriel = 1_000, NombreJourneesHomme = 3 },
        };

        foreach (var i in interventions)
        {
            _interventions.Add(i);
            _parcelles.FirstOrDefault(p => p.Id == i.ParcelleId)?.Interventions.Add(i);
        }
        _nextInterventionId = 100;

        // ─── RENDEMENTS HISTORIQUES (Terrain C) ──────────────────────────────
        _rendements = new List<RendementAnnuel>
        {
            new() { Id=1, ParcelleId=3, Annee=2023, Espece="Pommier", QuantiteKg=16_000, PrixVenteKg=2.8, CoutsTotal=12_000, Notes="1ère vraie récolte" },
            new() { Id=2, ParcelleId=3, Annee=2023, Espece="Olivier",  QuantiteKg=2_500,  PrixVenteKg=4.8, CoutsTotal=2_200,  Notes="Bonne campagne" },
            new() { Id=3, ParcelleId=3, Annee=2024, Espece="Pommier", QuantiteKg=14_000, PrixVenteKg=3.0, CoutsTotal=12_500, Notes="Tavelure modérée" },
            new() { Id=4, ParcelleId=3, Annee=2024, Espece="Olivier",  QuantiteKg=1_800,  PrixVenteKg=5.0, CoutsTotal=2_200,  Notes="Année de repos" },
            new() { Id=5, ParcelleId=3, Annee=2025, Espece="Pommier", QuantiteKg=18_000, PrixVenteKg=3.0, CoutsTotal=13_000, Notes="Meilleure année" },
            new() { Id=6, ParcelleId=3, Annee=2025, Espece="Olivier",  QuantiteKg=3_200,  PrixVenteKg=5.0, CoutsTotal=2_200,  Notes="Bonne alternance" },
            // Terrain B – quelques données
            new() { Id=7, ParcelleId=2, Annee=2024, Espece="Pommier", QuantiteKg=900,  PrixVenteKg=3.0, CoutsTotal=1_400, Notes="Arbres jeunes" },
            new() { Id=8, ParcelleId=2, Annee=2025, Espece="Pommier", QuantiteKg=1_200, PrixVenteKg=3.0, CoutsTotal=1_600, Notes="Montée en charge" },
        };

        // ─── SCÉNARIOS ÉCO-TOURISME ────────────────────────────────────────────
        _scenarios = new List<ScenarioEcoTourisme>
        {
            new() { Id=1, ParcelleId=1, Nom="Aire de détente (Terrain A)", Modele=ModeleRevenu.EntreesCircuits,
                VisiteursParAn=300, PrixEntreeParVisiteur=20, NombreCircuits=0, PrixCircuitParPersNne=0,
                NombreNuitees=0, PrixNuitee=0, NombreGuidesEmployes=0, SalaireGuide=0,
                InvestissementInitial=15_000, CoutsAnnuelsExploitation=3_000 },
            new() { Id=2, ParcelleId=2, Nom="Cueillette agritouristique (Terrain B)", Modele=ModeleRevenu.EntreesCircuits,
                VisiteursParAn=200, PrixEntreeParVisiteur=50, NombreCircuits=0, PrixCircuitParPersNne=0,
                NombreNuitees=0, PrixNuitee=0, NombreGuidesEmployes=0, SalaireGuide=0,
                InvestissementInitial=8_000, CoutsAnnuelsExploitation=2_000 },
            new() { Id=3, ParcelleId=4, Nom="Plantation arboricole (Terrain D)", Modele=ModeleRevenu.EntreesCircuits,
                VisiteursParAn=0, PrixEntreeParVisiteur=0, NombreCircuits=0, PrixCircuitParPersNne=0,
                NombreNuitees=0, PrixNuitee=0, NombreGuidesEmployes=0, SalaireGuide=0,
                InvestissementInitial=90_000, CoutsAnnuelsExploitation=20_000 },
            new() { Id=4, ParcelleId=4, Nom="Éco-camp randonnée (Terrain D)", Modele=ModeleRevenu.Combine,
                VisiteursParAn=600, PrixEntreeParVisiteur=30, NombreCircuits=40, PrixCircuitParPersNne=120,
                NombreNuitees=300, PrixNuitee=200, NombreGuidesEmployes=2, SalaireGuide=3_000,
                InvestissementInitial=120_000, CoutsAnnuelsExploitation=25_000 },
        };
    }
}
