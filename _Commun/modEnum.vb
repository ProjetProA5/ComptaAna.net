Module modEnum
    Public Enum eTypeAffaire
        Forfait = 1
        ContratCadre = 2
        Regie = 3
        Recurrent = 4
    End Enum

    Public Enum eModule
        AccesEmployeEcriture = 1
        AccesEmployeLecture = 2
        AccesEmployeListe = 3
        AccesEmployeCoutDroit = 4
        AccesEmployeFormation = 5
        AccesClientEcriture = 6
        AccesClientLecture = 7
        AccesClientListe = 8
        AccesAffaireEcriture = 9
        AccesAffaireLecture = 10
        AccesFacture = 11
        AccesSuiviFactures = 12
        AccesCatalogue = 13
        AccesReleveActiviteVisu = 14
        AccesStatsGenerales = 15
        AccesStatsProduits = 16
        AccesStatsEmployeServiceRex = 17
        AccesStatsCoutSalaires = 18
        AccesAdministration = 19
        AdminGeneral = 20
        ' AccesGestionCo = 21
        accesGraphiqueGeneral = 22
        accesGraphiqueRespoBU = 23

    End Enum

        public Enum eFormationType
        FormationSuivie = 0
        FormationDispensee = 1
        FormationPrevue = 2

    End Enum

End Module
