Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If CConfiguration.ModePreprod Then
            lblPreprod.Visible = True
            lblPreprod.Text = "Attention, vous êtes sur un site de test. Toutes les modifications que vous effectuez sur ce site ne seront pas visibles sur le site de production."
        End If


        If (CBool(Session("Connecter"))) Then
            Dim iProfil As Integer
            Dim oEmploye As CEmploye = CType(Session("Employe"), CEmploye)
            iProfil = oEmploye.ProfilID

            lblConnecte.Visible = True
            lblConnecte.Text = oEmploye.EmployePrenom & " " & oEmploye.EmployeNom & Space(1)

            ' GESTION DU MENU EN FONCTION DES DROITS
            MainMenu.Items.Clear()

            ' Menu GESTION
            Dim menuGestion = New MenuItem("Gestion", "", "", "~/Gestion/Employe/EmployeLister.aspx")
            'menuGestion.PopOutImageUrl = "~/App_Themes/ComptaAna/Design/Menu_popup.png"
            Dim menuEmploye = New MenuItem("Employés", "", "", "~/Gestion/Employe/EmployeLister.aspx")
            Dim menuClient = New MenuItem("Clients", "", "", "~/Gestion/Client/ClientListe.aspx")
            Dim menuAffaire = New MenuItem("Affaires", "", "", "~/Gestion/Affaire/AffaireLister.aspx")
            Dim menuFacture As New MenuItem
            If CConfiguration.NouvelleVersion Then
                menuFacture = New MenuItem("Factures", "", "", "~/Gestion/Affaire/AffaireFacturesListe.aspx")
            End If


            Dim menuCatalogue = New MenuItem("Catalogue", "", "", "~/Gestion/Catalogue.aspx")

            ' Menu relevé d'activité
            Dim menuReleve = New MenuItem("Relevé d'activité", "", "", "~/activite/ReleveActivite.aspx")

            ' Menu Recherche
            Dim menurecherche = New MenuItem("Recherche")

            ' Menu Statistique
            Dim menuStat = New MenuItem("Statistiques", "", "", "~/Statistiques/StatistiquesGenerales.aspx")
            'menuStat.PopOutImageUrl = "~/App_Themes/ComptaAna/Design/Menu_popup.png"
            Dim menuStatG = New MenuItem("Statistiques Générales", "", "", "~/Statistiques/StatistiquesGenerales.aspx")
            Dim menuStatEmployeService = New MenuItem("Calcul de la rentabilité", "", "", "~/Statistiques/StatEmployeService.aspx")
            Dim menuStatTypeProduit = New MenuItem("Statistiques sur les frais fixes et les frais variables", "", "", "~/Statistiques/StatTypeProduit.aspx")
            Dim menuStatCoutSalaire = New MenuItem("Statistiques sur les coûts salariaux", "", "", "~/Statistiques/StatCoutsSalariaux.aspx")
            Dim menuStatFormation = New MenuItem("Statistiques sur les formations", "", "", "~/Statistiques/StatFormation.aspx")
            '------------------------------------------------------------------------------------
            Dim menuGraphique = New MenuItem("Graphiques", "", "", "~/Statistiques/Graphiques.aspx")
            Dim menuStatPrimeAugmentation = New MenuItem("Statistiques sur les primes et augmentations", "", "", "~/Statistiques/StatPrimesAugmentations.aspx")
            ' Menu Administration
            Dim menuAdministration = New MenuItem("Administration", "", "", "~/Administration/RIB.aspx")
            'menuAdministration.PopOutImageUrl = "~/App_Themes/ComptaAna/Design/Menu_popup.png"
            Dim menuTypeProduit = New MenuItem("Types de produit", "", "", "~/Administration/GestionTypeProduit.aspx")
            Dim menuQualif = New MenuItem("Qualifications", "", "", "~/Administration/GestionPrixQualifTypeAffaire.aspx")
            Dim menuService = New MenuItem("Services", "", "", "~/Administration/GestionService.aspx")
            Dim menuTva = New MenuItem("TVA", "", "", "~/Administration/GestionTVA.aspx")
            Dim menuFiliale = New MenuItem("Filiales", "", "", "~/Administration/GestionFiliale.aspx")
            Dim menuParametreGlobal = New MenuItem("Paramètre Global", "", "", "~/Administration/GestionParametreGlobal.aspx")
            Dim menuPoste = New MenuItem("Postes", "", "", "~/Administration/GestionPoste.aspx")

            Dim menuCoordonneeBancaire As New MenuItem
            If CConfiguration.NouvelleVersion Then
                menuCoordonneeBancaire = New MenuItem("Coordonnées Bancaires", "", "", "~/Administration/RIB.aspx")
            End If


            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)

            Dim lDroitAVerifGestion As New ArrayList
            lDroitAVerifGestion.Add(3)
            lDroitAVerifGestion.Add(6)
            lDroitAVerifGestion.Add(9)
            lDroitAVerifGestion.Add(10)

            If verifDroit(lDroit, eModule.AccesEmployeListe) Then menuGestion.ChildItems.Add(menuEmploye)
            If verifDroit(lDroit, eModule.AccesClientLecture) Then menuGestion.ChildItems.Add(menuClient)
            If verifDroit(lDroit, eModule.AccesAffaireLecture) Then menuGestion.ChildItems.Add(menuAffaire)

            If CConfiguration.NouvelleVersion Then
                If verifDroit(lDroit, eModule.AccesSuiviFactures) Then menuGestion.ChildItems.Add(menuFacture)
            End If

            If verifDroit(lDroit, eModule.AccesCatalogue) Then menuGestion.ChildItems.Add(menuCatalogue)


            If existeUnDroit(lDroit, lDroitAVerifGestion) Then
                MainMenu.Items.Add(menuGestion)
            End If

            MainMenu.Items.Add(menuReleve)

            Dim lDroitAVerif As New ArrayList
            lDroitAVerif.Add(13)
            lDroitAVerif.Add(14)
            lDroitAVerif.Add(15)
            lDroitAVerif.Add(16)

            If verifDroit(lDroit, eModule.AccesStatsGenerales) Then menuStat.ChildItems.Add(menuStatG)
            If verifDroit(lDroit, eModule.AccesStatsProduits) Then menuStat.ChildItems.Add(menuStatTypeProduit)
            If verifDroit(lDroit, eModule.AccesStatsEmployeServiceRex) Then menuStat.ChildItems.Add(menuStatEmployeService)
            If verifDroit(lDroit, eModule.AccesStatsCoutSalaires) Then
                menuStat.ChildItems.Add(menuStatCoutSalaire)
                menuStat.ChildItems.Add(menuStatPrimeAugmentation)
            End If

            If verifDroit(lDroit, eModule.AccesStatsGenerales) Then menuStat.ChildItems.Add(menuStatFormation)
            '--------------------------------------------------------------------------------------------
            If verifDroit(lDroit, eModule.accesGraphiqueGeneral) Or verifDroit(lDroit, eModule.accesGraphiqueRespoBU) Then menuStat.ChildItems.Add(menuGraphique)
            If existeUnDroit(lDroit, lDroitAVerif) Then
                MainMenu.Items.Add(menuStat)
            End If


            If verifDroit(lDroit, eModule.AccesAdministration) Then
                If CConfiguration.NouvelleVersion Then
                    menuAdministration.ChildItems.Add(menuCoordonneeBancaire)
                End If
                menuAdministration.ChildItems.Add(menuFiliale)
                menuAdministration.ChildItems.Add(menuParametreGlobal)
                menuAdministration.ChildItems.Add(menuPoste)
                menuAdministration.ChildItems.Add(menuQualif)
                menuAdministration.ChildItems.Add(menuService)
                menuAdministration.ChildItems.Add(menuTva)
                menuAdministration.ChildItems.Add(menuTypeProduit)
                MainMenu.Items.Add(menuAdministration)
            End If

          
        Else
            MsgBox(Prompt:="erreur System")
        End If


    End Sub

    Protected Sub LogoutButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LogoutButton.Click
        Session.Clear()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class