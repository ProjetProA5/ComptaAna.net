Imports ComptaAna.Business
Imports Obout.Ajax.UI
Imports ComptaAna.net.Droit

Public Class EmployeCout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
          
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesEmployeEcriture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            LoadTreeView()

        End If
    End Sub

    Public Sub LoadEmployeCout(ByVal idEmploye As Integer, ByVal idEmployeCout As Integer)
        Dim oEmployeDAO As New CEmployeDAO
        Dim oEmployeCout As New CEmployeCout
        Dim oEmploye As New CEmploye

        Dim bACoutEmploye As Boolean = oEmployeDAO.testSiEmployeACout(idEmploye)

        If bACoutEmploye = True Then
            oEmployeCout = oEmployeDAO.getEmployeCoutById(idEmployeCout)
            ChargerGrilleRepartitionFiliale(oEmployeCout.EmployeCoutID)
            ChargerGridViewRepartitionService(oEmployeCout.EmployeCoutID)

            If Not gvRepartition.Rows.Count > 0 Then
                ChargerGrilleRepartitionFilialeVide()
            End If

            If Not gvRepartitionService.Rows.Count > 0 Then
                ChargerGrilleRepartitionServiceVide()
            End If
            cbAugmentation.Checked = CBool(oEmployeCout.EmployeCoutAugmentation)
            If cbAugmentation.Checked Then
                lblTx.Visible = True
                tbTaux.Visible = True
                tbTaux.Text = CStr(oEmployeCout.EmployeCoutTaux)
            End If
            tbDateDebut.Text = CStr(oEmployeCout.EmployeCoutDateDebut)
            tbLibelleCout.Text = CStr(oEmployeCout.EmployeCoutLibelle)
            tbCoutGlobal.Text = CStr(oEmployeCout.EmployeCoutGlobal)
            cbNonFacturable.Checked = CBool(oEmployeCout.EmployeCoutFacturable)
            If cbNonFacturable.Checked Then
                rdFacturable.SelectedIndex = 1
                fNonFacturable.Visible = True
            Else
                rdFacturable.SelectedIndex = 0
                fNonFacturable.Visible = False
            End If
        Else
            cbNouveauCout.Checked = False
            ChargerGrilleRepartitionFilialeVide()
            ChargerGrilleRepartitionServiceVide()
        End If
    End Sub

    Protected Sub btEnregistrer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btEnregistrer.Click
        Dim iEmployeID As Integer
        Dim oEmployeCout As CEmployeCout
        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO

        iEmployeID = CInt(Request.QueryString("id"))

        If tvEmployeCout.SelectedNode Is Nothing Then ' On est Dans le cas d'une creation d'un nouveau cout

            If cbAugmentation.Checked Then
                If CDec(Replace(tbTaux.Text, ".", ",")) < 1 Then
                    LabTauxAuguementationInfAUn.Visible = True
                Else
                    oEmployeCout = New CEmployeCout(tbLibelleCout.Text, iEmployeID, CDec(Replace(tbCoutGlobal.Text, ".", ",")), CDate(tbDateDebut.Text), _
                      Nothing, cbNonFacturable.Checked, 1, CDec(Replace(tbTaux.Text, ".", ",")))
                    verifieEtInsereCout(oEmployeCout)
                    LabTauxAuguementationInfAUn.Visible = False
                End If
               
            Else
                oEmployeCout = New CEmployeCout(tbLibelleCout.Text, iEmployeID, CDec(Replace(tbCoutGlobal.Text, ".", ",")), CDate(tbDateDebut.Text), _
                        Nothing, cbNonFacturable.Checked, 0, 0)
                verifieEtInsereCout(oEmployeCout)
            End If


        Else ' On est dans une modification de cout

            Dim sdateDeb = getDateFormatSql(CDate(tbDateDebut.Text))

            Dim bPeriodeCorrecte As Boolean = VerifPeriode(sdateDeb)

            If bPeriodeCorrecte Then

                oEmployeCout = oEmployeDAO.getEmployeCoutById(CInt(tvEmployeCout.SelectedNode.Value))
                oEmployeCout.EmployeCoutDateDebut = CDate(tbDateDebut.Text)
                oEmployeCout.EmployeCoutFacturable = CInt(cbNonFacturable.Checked)
                oEmployeCout.EmployeCoutGlobal = CDec(tbCoutGlobal.Text)
                oEmployeCout.EmployeCoutLibelle = CStr(tbLibelleCout.Text)
                If cbAugmentation.Checked Then
                    oEmployeCout.EmployeCoutAugmentation = 1
                    oEmployeCout.EmployeCoutTaux = CDec(Replace(tbTaux.Text, ".", ","))
                Else
                    oEmployeCout.EmployeCoutAugmentation = 0
                    oEmployeCout.EmployeCoutTaux = 0
                End If
                If testInsertionRepartitionFilialeOk() Then
                    If testInsertionRepartitionServiceOk() Or Not cbNonFacturable.Checked Then
                        If noeudPrecedentExiste(getPositionDuNoeudActuel(tvEmployeCout.SelectedNode.Parent, tvEmployeCout.SelectedNode.Text), tvEmployeCout.SelectedNode.Parent) Then
                            Dim noeudPrecedent = getNoeudPrecedent(getPositionDuNoeudActuel(tvEmployeCout.SelectedNode.Parent, tvEmployeCout.SelectedNode.Text), tvEmployeCout.SelectedNode.Parent)
                            oEmployeDAO.UpdateEmployeCoutDateFinSansVerif(CInt(noeudPrecedent.Value), tbDateDebut.Text)
                        End If
                        oEmployeDAO.ModifierEmployeCout(oEmployeCout)
                        modifierEmployeRepartitionFiliale(CInt(tvEmployeCout.SelectedNode.Value))
                        If cbNonFacturable.Checked Then
                            modifierEmployeRepartitionService(CInt(tvEmployeCout.SelectedNode.Value))
                        Else
                            ModifierEmployeRepartitionServiceNull(CInt(tvEmployeCout.SelectedNode.Value))
                        End If
                        lEnregistrementReussi.Visible = True
                        btEnregistrer.Visible = False
                        btSupprimer.Visible = False
                        fCoutEmploye.Visible = False
                        fRepartitionFiliale.Visible = False
                        fNonFacturable.Visible = False
                        rdFacturable.Visible = False
                        LoadTreeView()
                    Else
                        lbVerifReparitionService.Visible = True
                    End If
                Else
                    lbVerifReparition.Visible = True
                End If
            Else
                lPeriodeIncorrecte.Visible = True
            End If
        End If

        lSuppressionImpossible.Visible = False
    End Sub

    Protected Sub gvRepartition_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        e.Row.Cells(0).Visible = False
    End Sub

    Protected Sub gvRepartitionService_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        e.Row.Cells(0).Visible = False
    End Sub

    ''' <summary>
    ''' Permet de charger la grille de répartition des filiales
    ''' (Dans le cas d'un nouvel utilisateur les valeurs seront donc vides)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChargerGrilleRepartitionFilialeVide()
        Dim oFilialeDAO As New CFilialeDAO
        Dim dsFiliale As DataSet
        Dim dbFiliale As New DataTable
        Dim dcRepartition As New DataColumn("Repartition")

        dsFiliale = oFilialeDAO.GetAllFilialeActive()
        dbFiliale = dsFiliale.Tables(0)
        dbFiliale.Columns.Add(dcRepartition)

        gvRepartition.DataSource = dsFiliale
        gvRepartition.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de charger la grille de repartition des filiales 
    ''' remplie en fonction de l'id de l'employe
    ''' </summary>
    ''' <param name="idEmploye">id d'un employe</param>
    ''' <remarks></remarks>
    Private Sub ChargerGrilleRepartitionFiliale(ByVal idEmploye As Integer)
        Dim oFilialeDAO As New CFilialeDAO
        Dim dsFiliale As DataSet = New DataSet

        dsFiliale = oFilialeDAO.getFilialeAndRepartition(idEmploye)

        gvRepartition.DataSource = dsFiliale
        gvRepartition.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de modifier les valeurs de la répartition des filiales
    ''' </summary>
    ''' <param name="idEmployeCout">idEmployeCout : l'id d'un employeCout éxistant dans la base</param>
    ''' <remarks></remarks>
    Public Sub modifierEmployeRepartitionFiliale(ByVal idEmployeCout As Integer)
        Dim iCount As Integer = gvRepartition.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iFilialeID As Integer = CInt(gvRepartition.Rows(i).Cells(0).Text)
            Dim tbRepartition As TextBox = CType(gvRepartition.Rows(i).Cells(2).FindControl("tbPourcent"), TextBox)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(tbRepartition.Text))
            oEmployeDAO.modifierEmployeRepartition(iFilialeID, idEmployeCout, dValeur)
        Next
    End Sub

    ''' <summary>
    ''' Permet d'inserer dans la base de données l'ensemble des pourcentages
    ''' des différentes filiales pour un utilisateur
    ''' </summary>
    ''' <param name="iEmployeCoutID">idEmployeCout : l'id d'un employeCout éxistant dans la base</param>
    ''' <remarks></remarks>
    Private Sub InsererEmployeRepartitionFiliale(ByVal iEmployeCoutID As Integer)
        Dim iCount As Integer = gvRepartition.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iFilialeID As Integer = CInt(gvRepartition.Rows(i).Cells(0).Text)
            Dim tbRepartition As TextBox = CType(gvRepartition.Rows(i).Cells(2).FindControl("tbPourcent"), TextBox)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(tbRepartition.Text))
            oEmployeDAO.insererEmployeRepartition(iFilialeID, iEmployeCoutID, dValeur)
        Next
    End Sub

    ''' <summary>
    ''' Permet de tester si la somme des pourcentages des répartitions des filiales est 
    ''' égale à 100%
    ''' </summary>
    ''' <returns>True si 100% False sinon</returns>
    ''' <remarks></remarks>
    Private Function testInsertionRepartitionFilialeOk() As Boolean
        Dim iCount As Integer = gvRepartition.Rows.Count
        Dim dSomme As Decimal

        For i = 0 To iCount - 1
            Dim tbRepartition As TextBox = CType(gvRepartition.Rows(i).Cells(2).FindControl("tbPourcent"), TextBox)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(tbRepartition.Text))
            dSomme = dSomme + dValeur
        Next

        Return dSomme = 100
    End Function

    ''' <summary>
    ''' Permet de charger la liste des services avec des champs vides.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChargerGrilleRepartitionServiceVide()
        Dim oServiceDAO As New CServiceDAO
        Dim dsService As DataSet
        Dim dbService As New DataTable
        Dim dcRepartitionService As New DataColumn("Repartition")

        dsService = oServiceDAO.GetAllServiceActif()
        dbService = dsService.Tables(0)
        dbService.Columns.Add(dcRepartitionService)

        gvRepartitionService.DataSource = dsService
        gvRepartitionService.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de charger la liste des services, avec les valeurs des différents services
    ''' pour cet employe
    ''' </summary>
    ''' <param name="iEmployeCoutID">iEmployeCoutID : l'idEmployeCout de l'employe </param>
    ''' <remarks></remarks>
    Private Sub ChargerGridViewRepartitionService(ByVal iEmployeCoutID As Integer)
        Dim oServiceDAO As New CServiceDAO
        Dim dsService As DataSet = New DataSet

        dsService = oServiceDAO.getServiceAndRepartition(iEmployeCoutID)

        gvRepartitionService.DataSource = dsService
        gvRepartitionService.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de modifier la répartition des services pour un employe donné
    ''' </summary>
    ''' <param name="idEmployeCout">iEmployeCoutID : l'idEmployeCout de l'employe</param>
    ''' <remarks></remarks>
    Public Sub modifierEmployeRepartitionService(ByVal idEmployeCout As Integer)
        Dim iCount As Integer = gvRepartitionService.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iServiceID As Integer = CInt(gvRepartitionService.Rows(i).Cells(0).Text)
            Dim tbRepartitionService As TextBox = CType(gvRepartitionService.Rows(i).Cells(2).FindControl("tbPourcentService"), TextBox)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(tbRepartitionService.Text))
            oEmployeDAO.modifierEmployeRepartitionService(iServiceID, idEmployeCout, dValeur)
        Next
    End Sub

    ''' <summary>
    ''' Permet d'inserer la répartition des services pour un nouvel employé
    ''' </summary>
    ''' <param name="iEmployeCoutID">l'idEmployeCout de l'employe venant d'être inseré</param>
    ''' <remarks></remarks>
    Private Sub InsererEmployeRepartitionService(ByVal iEmployeCoutID As Integer)
        Dim iCount As Integer = gvRepartitionService.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iServiceID As Integer = CInt(gvRepartitionService.Rows(i).Cells(0).Text)
            Dim tbRepartitionService As TextBox = CType(gvRepartitionService.Rows(i).Cells(2).FindControl("tbPourcentService"), TextBox)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(tbRepartitionService.Text))
            oEmployeDAO.insererEmployeRepartitionService(iServiceID, iEmployeCoutID, dValeur)
        Next
    End Sub

    ''' <summary>
    ''' Permet de tester si la répartition des services est égale à 100%
    ''' </summary>
    ''' <returns>True si = 100%, False sinon</returns>
    ''' <remarks></remarks>
    Private Function testInsertionRepartitionServiceOk() As Boolean
        Dim iCount As Integer = gvRepartitionService.Rows.Count
        Dim dSomme As Decimal

        For i = 0 To iCount - 1
            Dim dValeur As Decimal
            Dim tbPourcentService As TextBox = CType(gvRepartitionService.Rows(i).Cells(2).FindControl("tbPourcentService"), TextBox)
            dValeur = CDec(Formater.FormatDecimal(tbPourcentService.Text))
            dSomme = dSomme + dValeur
        Next

        Return dSomme = 100
    End Function

    ''' <summary>
    ''' Permet de vérifier si la répartition des filiales et des Services vaut 100% 
    ''' puis les insère si elles sont valides
    ''' </summary>
    ''' <param name="oEmployeCout">oEmployeCout : Un objet de type CEmployeCout</param>
    ''' <remarks></remarks>
    Private Sub verifieEtInsereCout(ByVal oEmployeCout As CEmployeCout)
        Dim oEmployeDAO As New CEmployeDAO
        Dim bRepartitionFilialeOk, bRepartitionServiceOk, bFacturable As Boolean
        bFacturable = cbNonFacturable.Checked
        bRepartitionFilialeOk = testInsertionRepartitionFilialeOk()
        bRepartitionServiceOk = testInsertionRepartitionServiceOk()

        If bFacturable = False Then
            If bRepartitionFilialeOk = True Then
                lbVerifReparition.Visible = False

                Dim dateFinSql = getDateFormatSql(CDate(tbDateDebut.Text))
                Dim bPeriodeCorrecte As Boolean = True
                ' si il existe un cout employé dans l'arbre alors on lui donne une date de fin et on vérifie au passage si la periode donnée est acceptable
                If Not tvEmployeCout.Nodes.Count = 0 Then
                    bPeriodeCorrecte = oEmployeDAO.UpdateEmployeCoutDateFin(getCoutEmployeIdPlusRecent, dateFinSql)
                End If
                'si la periode donnée est acceptable alors on insére ce nouveau cout
                If bPeriodeCorrecte Then
                    oEmployeDAO.InsererEmployeCout(oEmployeCout)
                    oEmployeCout.EmployeCoutID = oEmployeDAO.getIdDernierEmployeCoutInserer()
                    InsererEmployeRepartitionFiliale(oEmployeCout.EmployeCoutID)
                    InsererEmployeRepartitionServiceNull(oEmployeCout.EmployeCoutID)

                    lPeriodeIncorrecte.Visible = False
                    lEnregistrementReussi.Visible = True
                    btEnregistrer.Visible = False
                    btSupprimer.Visible = False
                    fCoutEmploye.Visible = False
                    fRepartitionFiliale.Visible = False
                    fNonFacturable.Visible = False
                    rdFacturable.Visible = False
                Else
                    lPeriodeIncorrecte.Visible = True
                End If
            Else
                lbVerifReparition.Visible = True
            End If
        Else
            If bRepartitionFilialeOk = True Then
                lbVerifReparition.Visible = False
                If bRepartitionServiceOk = True Then
                    lbVerifReparitionService.Visible = False

                    Dim dateFinSql = getDateFormatSql(CDate(tbDateDebut.Text))
                    Dim bPeriodeCorrecte As Boolean = True

                    If Not tvEmployeCout.Nodes.Count = 0 Then
                        bPeriodeCorrecte = oEmployeDAO.UpdateEmployeCoutDateFin(getCoutEmployeIdPlusRecent, dateFinSql)
                    End If
                    If bPeriodeCorrecte Then
                        oEmployeDAO.InsererEmployeCout(oEmployeCout)
                        oEmployeCout.EmployeCoutID = oEmployeDAO.getIdDernierEmployeCoutInserer()
                        InsererEmployeRepartitionFiliale(oEmployeCout.EmployeCoutID)
                        InsererEmployeRepartitionService(oEmployeCout.EmployeCoutID)

                        lPeriodeIncorrecte.Visible = False
                        lEnregistrementReussi.Visible = True
                        btEnregistrer.Visible = False
                        btSupprimer.Visible = False
                        fCoutEmploye.Visible = False
                        fRepartitionFiliale.Visible = False
                        fNonFacturable.Visible = False
                        rdFacturable.Visible = False
                    Else
                        lPeriodeIncorrecte.Visible = True
                    End If
                Else
                    lbVerifReparitionService.Visible = True
                End If
            Else
                lbVerifReparition.Visible = True
            End If
        End If

        LoadTreeView()
    End Sub

    Protected Sub btNouveauCout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNouveauCout.Click
        tbLibelleCout.Text = ""
        tbCoutGlobal.Text = ""
        tbDateDebut.Text = ""
        fsPrime.Visible = False
        gvPrime.Visible = False
        gvAugmentation.Visible = False
        lblAug.Visible = False
        lblPrime.Visible = False
        cbNonFacturable.Checked = False
        rdFacturable.SelectedIndex = 0
        fNonFacturable.Visible = False
        cbNouveauCout.Checked = True
        ChargerGrilleRepartitionFilialeVide()
        ChargerGrilleRepartitionServiceVide()

        LoadTreeView()
        btEnregistrer.Visible = True
        btSupprimer.Visible = False
        fRepartitionFiliale.Visible = True
        fCoutEmploye.Visible = True
        lSuppressionImpossible.Visible = False
        lPeriodeIncorrecte.Visible = False
        lEnregistrementReussi.Visible = False
        rdFacturable.Visible = True
    End Sub

    Private Sub InsererEmployeRepartitionServiceNull(ByVal iEmployeCoutID As Integer)
        Dim iCount As Integer = gvRepartitionService.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iServiceID As Integer = CInt(gvRepartitionService.Rows(i).Cells(0).Text)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(""))
            oEmployeDAO.insererEmployeRepartitionService(iServiceID, iEmployeCoutID, dValeur)
        Next
    End Sub

    Private Sub ModifierEmployeRepartitionServiceNull(ByVal iEmployeCoutID As Integer)
        Dim iCount As Integer = gvRepartitionService.Rows.Count
        Dim oEmployeDAO As New CEmployeDAO

        For i = 0 To iCount - 1
            Dim iServiceID As Integer = CInt(gvRepartitionService.Rows(i).Cells(0).Text)
            Dim dValeur As Decimal
            dValeur = CDec(Formater.FormatDecimal(""))
            oEmployeDAO.modifierEmployeRepartitionService(iServiceID, iEmployeCoutID, dValeur)
        Next
    End Sub

    ''' <summary>
    ''' charger le treeview
    ''' </summary>
    Public Sub LoadTreeView()
        Dim oEmploye As New CEmployeDAO
        Dim dsAnnee, dsDate As New DataSet
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))

        Dim tvnAnnee As Obout.Ajax.UI.TreeView.Node
        Dim tvnDate As Obout.Ajax.UI.TreeView.Node

        If Not IsCallback Then
            tvEmployeCout.Nodes.Clear()
        End If

        dsAnnee = oEmploye.GetEmployeCoutAnnee(iEmployeID)

        If dsAnnee.Tables(0).Rows.Count > 0 Then
            For i = 0 To dsAnnee.Tables(0).Rows.Count - 1
                tvnAnnee = New Obout.Ajax.UI.TreeView.Node(CStr(dsAnnee.Tables(0).Rows(i)("annee")))
                tvnAnnee.ImageUrl = "~/App_Themes/Axone/Design/folder.png"

                Dim iAnnee As Integer = CInt(dsAnnee.Tables(0).Rows(i)("annee"))
                dsDate = oEmploye.GetEmployeCoutDate(iAnnee, iEmployeID)

                If dsDate.Tables(0).Rows.Count > 0 Then
                    For j = 0 To dsDate.Tables(0).Rows.Count - 1
                        tvnDate = New Obout.Ajax.UI.TreeView.Node(CStr(dsDate.Tables(0).Rows(j)("periode")))
                        tvnDate.Value = dsDate.Tables(0).Rows(j)("EmployeCoutID").ToString
                        tvnAnnee.ChildNodes.Add(tvnDate)
                        tvnAnnee.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand
                    Next
                End If

                tvEmployeCout.Nodes.Add(tvnAnnee)
            Next
        End If
    End Sub

    ''' <summary>
    ''' chargement quand elle est selectionnee dans le treeview
    ''' </summary>
    Protected Sub tvEmployeCout_SelectedTreeNodeChanged(ByVal sender As Object, ByVal e As Obout.Ajax.UI.TreeView.NodeEventArgs) Handles tvEmployeCout.SelectedTreeNodeChanged
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))
        LoadEmployeCout(iEmployeID, CInt(e.Node.Value))
        lblAug.Visible = False
        lblPrime.Visible = False
        gvAugmentation.Visible = False
        gvPrime.Visible = False
        btEnregistrer.Visible = True
        btSupprimer.Visible = True
        fRepartitionFiliale.Visible = True
        fCoutEmploye.Visible = True
        lSuppressionImpossible.Visible = False
        lPeriodeIncorrecte.Visible = False
        lEnregistrementReussi.Visible = False
        lbVerifReparition.Visible = False
        lbVerifReparitionService.Visible = False
        rdFacturable.Visible = True
    End Sub

    ''' <summary>
    ''' Permet de récupérer l'EmployeId le Plus Recent ( attention verifier que l'arbre n'est pas vide ) 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCoutEmployeIdPlusRecent() As Integer
        Dim nodeParentPlusEleve As Integer = 0
        Dim res = 0
        Dim i As Integer = 0
        Dim collectNodes = tvEmployeCout.Nodes
        Dim indexNodeParentPlusEleve = 0

        While (i < collectNodes.Count)
            If CInt(collectNodes(i).Text) > nodeParentPlusEleve Then
                nodeParentPlusEleve = CInt(collectNodes(i).Text)
                indexNodeParentPlusEleve = i
            End If

            i = i + 1
        End While

        i = 0
        Dim collectNodesFils = collectNodes(indexNodeParentPlusEleve).ChildNodes

        While (i < (collectNodesFils.Count))
            If CInt(collectNodesFils(i).Value) > res Then
                res = CInt(collectNodesFils(i).Value)
            End If

            i = i + 1
        End While

        Return res

    End Function

    Private Sub btSupprimer_Click(sender As Object, e As System.EventArgs) Handles btSupprimer.Click
        Dim iEmployeCountId = CInt(tvEmployeCout.SelectedNode.Value)
        Dim oEmploye As New CEmployeDAO

        Dim bSupprimé = oEmploye.SupprimerCoutEmployeAvecId(iEmployeCountId)
        If bSupprimé Then
            Dim positionDuNoeudActuel = getPositionDuNoeudActuel(tvEmployeCout.SelectedNode.Parent, tvEmployeCout.SelectedNode.Text)
            Dim positionParentDuNoeudActuel = getPositionParentDuNoeudActuel(tvEmployeCout.SelectedNode.Parent)
            Dim coutEmployeID As Integer = -1
            If positionDuNoeudActuel - 1 < 0 Then
                If positionParentDuNoeudActuel + 1 < tvEmployeCout.Nodes.Count Then
                    coutEmployeID = CInt(tvEmployeCout.Nodes(positionParentDuNoeudActuel + 1).ChildNodes(tvEmployeCout.Nodes(positionParentDuNoeudActuel + 1).ChildNodes.Count - 1).Value)
                Else

                End If
            Else
                coutEmployeID = CInt(tvEmployeCout.Nodes(positionParentDuNoeudActuel).ChildNodes(positionDuNoeudActuel - 1).Value)
            End If

            oEmploye.RemiseNullEmployeCoutDateFin(coutEmployeID)

            btEnregistrer.Visible = False
            btSupprimer.Visible = False
            fCoutEmploye.Visible = False
            fNonFacturable.Visible = False
            fRepartitionFiliale.Visible = False
            rdFacturable.Visible = False
            LoadTreeView()
        Else
            lSuppressionImpossible.Visible = True
        End If
    End Sub
    ''' <summary>
    ''' met une date au format sql 
    ''' </summary>
    ''' <param name="dateFin">la date à transformer</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getDateFormatSql(dateFin As Date) As String

        Dim daystrFin, monthstrFin As String

        If dateFin.Day < 10 Then
            daystrFin = "0" & dateFin.Day
        Else
            daystrFin = "" & dateFin.Day
        End If


        If dateFin.Month < 10 Then
            monthstrFin = "0" & dateFin.Month
        Else
            monthstrFin = "" & dateFin.Month
        End If

        Return "" & dateFin.Year & "-" & monthstrFin & "-" & daystrFin
    End Function
    ''' <summary>
    ''' Permet de savoir si la période est entre la période avant et après
    ''' </summary>
    ''' <param name="sdateDeb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerifPeriode(sdateDeb As String) As Boolean

        Dim noeudActuel = tvEmployeCout.SelectedNode.Text
        Dim parentNoeudActuel = tvEmployeCout.SelectedNode.Parent
        'permet de récupérer la position du noeud actuel dans l'arbre
        Dim positionDuNoeudActuel = getPositionDuNoeudActuel(parentNoeudActuel, noeudActuel)
        Dim bNoeudPrecedent = noeudPrecedentExiste(positionDuNoeudActuel, parentNoeudActuel)
        Dim bNoeudSuivant = noeudSuivantExiste(positionDuNoeudActuel, parentNoeudActuel)
        Dim bPeriodeok = True
        Dim noeudPrecedent As TreeView.Node = New TreeView.Node
        Dim noeudSuivant As TreeView.Node = New TreeView.Node

        

        'On va recuperer le noeudPrecedent
        If bNoeudPrecedent Then
            noeudPrecedent = getNoeudPrecedent(positionDuNoeudActuel, parentNoeudActuel)
        End If

        'On va recuperer le noeudPrecedent
        If bNoeudSuivant Then
            noeudSuivant = getNoeudSuivant(positionDuNoeudActuel, parentNoeudActuel)
        End If

        ' comparaison avec la date enfin 

        If bNoeudPrecedent Then
            ' maintenant, on va recuperer la date du noeud precedent
            Dim sDateDebNoeudPrecedent As String = getDateDebTree(noeudPrecedent)
            Dim sDateDebNoeudPrecedentFormatSQL As String = sDateDebNoeudPrecedent.Split(CChar("-"))(2).Trim & "-" & sDateDebNoeudPrecedent.Split(CChar("-"))(1) & "-" & sDateDebNoeudPrecedent.Split(CChar("-"))(0)
            bPeriodeok = CEmployeDAO.CompareStringDate(sDateDebNoeudPrecedentFormatSQL, sdateDeb)
        End If

        If bNoeudSuivant And bPeriodeok Then
            'on va recuperer la date du noeud Suivant
            Dim sDateDebNoeudSuivant As String = getDateDebTree(noeudSuivant)
            Dim sDateDebNoeudSuivantFormatSQL As String = sDateDebNoeudSuivant.Split(CChar("-"))(2) & "-" & sDateDebNoeudSuivant.Split(CChar("-"))(1) & "-" & sDateDebNoeudSuivant.Split(CChar("-"))(0)
            bPeriodeok = CEmployeDAO.CompareStringDate(sdateDeb, sDateDebNoeudSuivantFormatSQL)
        End If

        Return bPeriodeok
    End Function

    Private Function getDateFinTree(noeudPrecedent As TreeView.Node) As String
        Dim i As Integer = 0
        Dim sNoeudPrecedent = noeudPrecedent.Text

        While Not sNoeudPrecedent(i) = "/"
            i = i + 1
        End While
        i = i + 2
        Return noeudPrecedent.Text.Substring(i, sNoeudPrecedent.Length - i)
    End Function

    Private Function getDateDebTree(noeudSuivant As TreeView.Node) As String
        Dim i As Integer = 0
        Dim sNoeudSuivant = noeudSuivant.Text

        'While Not sNoeudSuivant(i) = "/" And i < sNoeudSuivant.Length - 1
        '    i = i + 1
        'End While
        Return sNoeudSuivant.Split(CChar("/"))(0)
    End Function

    ''' <summary>
    ''' Permet de recuperer la position du noeud actuel dans le treeview
    ''' </summary>
    ''' <param name="parentNoeudActuel">le noeud parent</param>
    ''' <param name="noeudActuel">le texte contenu dans le noeud actuel</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getPositionDuNoeudActuel(parentNoeudActuel As TreeView.Node, noeudActuel As String) As Integer
        Dim positionDuNoeudActuel As Integer = 0
        While ((positionDuNoeudActuel < parentNoeudActuel.ChildNodes.Count) And Not (parentNoeudActuel.ChildNodes(positionDuNoeudActuel).Text = noeudActuel))
            positionDuNoeudActuel = positionDuNoeudActuel + 1
        End While
        Return positionDuNoeudActuel
    End Function

    ''' <summary>
    ''' Permet de recuperer la position du parent du noeud actuel dans le treeview
    ''' </summary>
    ''' <param name="parentNoeudActuel"> le noeud parent du noeud actuel</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getPositionParentDuNoeudActuel(parentNoeudActuel As TreeView.Node) As Integer
        Dim positionParentDuNoeudActuel = 0
        While ((positionParentDuNoeudActuel < tvEmployeCout.Nodes.Count) And Not (tvEmployeCout.Nodes(positionParentDuNoeudActuel).Text = parentNoeudActuel.Text))
            positionParentDuNoeudActuel = positionParentDuNoeudActuel + 1
        End While
        Return positionParentDuNoeudActuel
    End Function

    ''' <summary>
    ''' Permet d'obtenir le noeud precedent au noeud actuel
    ''' </summary>
    ''' <param name="positionDuNoeudActuel"></param>
    ''' <param name="parentNoeudActuel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getNoeudPrecedent(positionDuNoeudActuel As Integer, parentNoeudActuel As TreeView.Node) As TreeView.Node
        Dim noeudPrecedent As TreeView.Node = New TreeView.Node
        Dim positionParentDuNoeudActuel As Integer = 0

        If positionDuNoeudActuel = 0 Then ' cela veut dire qu'on est en train de modifier le 1er noeud d'une année
            'donc on récupère la position du noeud parent
            positionParentDuNoeudActuel = getPositionParentDuNoeudActuel(parentNoeudActuel)
            'on va recupere le noeud precedent
            If positionParentDuNoeudActuel = tvEmployeCout.Nodes.Count - 1 Then 'cela veut dire qu'il n'existe pas de noeudParent precedent

            Else ' Sinon on le recupère
                noeudPrecedent = tvEmployeCout.Nodes(positionParentDuNoeudActuel + 1).ChildNodes(tvEmployeCout.Nodes(positionParentDuNoeudActuel).ChildNodes.Count - 1)
            End If
        Else ' autrement c'est qu'il est juste avant le noeud actuel
            noeudPrecedent = parentNoeudActuel.ChildNodes(positionDuNoeudActuel - 1)
        End If
        Return noeudPrecedent
    End Function

    ''' <summary>
    ''' Permet de savoir si le noeud precedent au noeud actuel existe
    ''' </summary>
    ''' <param name="positionDuNoeudActuel"></param>
    ''' <param name="parentNoeudActuel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function noeudPrecedentExiste(positionDuNoeudActuel As Integer, parentNoeudActuel As TreeView.Node) As Boolean
        Dim positionParentDuNoeudActuel As Integer = 0
        Dim bNoeudPrecedent = True

        If positionDuNoeudActuel = 0 Then ' cela veut dire qu'on est en train de modifier le 1er noeud d'une année
            'donc on récupère la position du noeud parent
            positionParentDuNoeudActuel = getPositionParentDuNoeudActuel(parentNoeudActuel)
            'on va recupere le noeud precedent
            If positionParentDuNoeudActuel = tvEmployeCout.Nodes.Count - 1 Then 'cela veut dire qu'il n'existe pas de noeudParent precedent
                bNoeudPrecedent = False
            Else ' Sinon on le recupère

            End If
        Else ' autrement c'est qu'il est juste avant le noeud actuel

        End If
        Return bNoeudPrecedent
    End Function
    ''' <summary>
    ''' Permet d'obtenir le noeud suivant au noeud actuel
    ''' </summary>
    ''' <param name="positionDuNoeudActuel"></param>
    ''' <param name="parentNoeudActuel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getNoeudSuivant(positionDuNoeudActuel As Integer, parentNoeudActuel As TreeView.Node) As TreeView.Node
        Dim noeudSuivant As TreeView.Node = New TreeView.Node
        Dim positionParentDuNoeudActuel As Integer = 0

        If positionDuNoeudActuel = parentNoeudActuel.ChildNodes.Count - 1 Then ' cela veut dire qu'on est en train modifié le dernier noeud d'une année
            'donc on récupère la position du noeud parent
            positionParentDuNoeudActuel = getPositionParentDuNoeudActuel(parentNoeudActuel)
            'on va recupere le noeud suivant
            If positionParentDuNoeudActuel = 0 Then 'cela veut dire qu'il n'existe pas de noeudParent suivant

            Else ' Sinon on le recupère
                noeudSuivant = tvEmployeCout.Nodes(positionParentDuNoeudActuel - 1).ChildNodes(0)
            End If
        Else ' autrement c'est qu'il est juste après le noeud actuel
            noeudSuivant = parentNoeudActuel.ChildNodes(positionDuNoeudActuel + 1)
        End If

        Return noeudSuivant
    End Function
    ''' <summary>
    ''' Permet de savoir si le noeud suivant au noeud actuel existe
    ''' </summary>
    ''' <param name="positionDuNoeudActuel"></param>
    ''' <param name="parentNoeudActuel"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function noeudSuivantExiste(positionDuNoeudActuel As Integer, parentNoeudActuel As TreeView.Node) As Boolean
        Dim positionParentDuNoeudActuel As Integer = 0
        Dim bNoeudSuivant = True

        If positionDuNoeudActuel = parentNoeudActuel.ChildNodes.Count - 1 Then ' cela veut dire qu'on est en train modifié le dernier noeud d'une année
            'donc on récupère la position du noeud parent
            positionParentDuNoeudActuel = getPositionParentDuNoeudActuel(parentNoeudActuel)
            'on va recupere le noeud suivant
            If positionParentDuNoeudActuel = 0 Then 'cela veut dire qu'il n'existe pas de noeudParent suivant
                bNoeudSuivant = False
            Else ' Sinon on le recupère

            End If
        Else ' autrement c'est qu'il est juste après le noeud actuel

        End If
        Return bNoeudSuivant
    End Function

    Private Sub rdFacturable_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdFacturable.SelectedIndexChanged
        fNonFacturable.Visible = Not fNonFacturable.Visible
        cbNonFacturable.Checked = fNonFacturable.Visible
    End Sub

    Private Sub mOnglets_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles mOnglets.MenuItemClick
        Dim idURL = Request.QueryString("id")

        Select Case e.Item.Value
            Case "1"
                If IsNothing(idURL) Then
                    Response.Redirect("EmployeModifier.aspx")
                Else
                    Response.Redirect("EmployeModifier.aspx?id=" & idURL)
                End If
            Case "2"
                If IsNothing(idURL) Then
                    Response.Redirect("EmployeCout.aspx")
                Else
                    Response.Redirect("EmployeCout.aspx?id=" & idURL)
                End If
            Case "3"
                If IsNothing(idURL) Then
                    Response.Redirect("EmployeDroit.aspx")
                Else
                    Response.Redirect("EmployeDroit.aspx?id=" & idURL)
                End If
            Case "4"
                If IsNothing(idURL) Then
                    Response.Redirect("Formation.aspx")
                Else
                    Response.Redirect("Formation.aspx?id=" & idURL)
                End If
            Case "5"
                If IsNothing(idURL) Then
                    Response.Redirect("EmployeEcheanceAnnuelle.aspx")
                Else
                    Response.Redirect("EmployeEcheanceAnnuelle.aspx?id=" & idURL)
                End If
        End Select
    End Sub

   
    Private Sub cbAugmentation_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAugmentation.CheckedChanged
        lblTx.Visible = cbAugmentation.Checked
        tbTaux.Visible = cbAugmentation.Checked
    End Sub

    Private Sub btNouvellePrime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btNouvellePrime.Click
        fsPrime.Visible = True
        lblPrime.Visible = True
        btEnregistrerPrime.Visible = True
        fCoutEmploye.Visible = False
        btSupprimer.Visible = False
        fRepartitionFiliale.Visible = False
        fNonFacturable.Visible = False
        btEnregistrer.Visible = False
        gvPrime.Visible = True
        rdFacturable.Visible = False
        gvAugmentation.Visible = False
        lblAug.Visible = False
        lEnregistrementReussi.Visible = False
        loadPrime()
        gvPrime.Columns(6).Visible = True
    End Sub

    Private Sub btEnregistrerPrime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEnregistrerPrime.Click
        Dim oEmployePrime As CEmployePrime
        Dim oEmployePrimeDAO As New CEmployePrimeDAO
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))
        Dim iAvNat As Integer
        Dim sModele As String
        If cbAvNat.Checked Then
            iAvNat = 1
            sModele = tbModele.Text
        Else
            iAvNat = 0
            sModele = ""
        End If
        oEmployePrime = New CEmployePrime(iEmployeID, CDec(tbMontantPrime.Text), CDate(tbDatePrime.Text), iAvNat, sModele)
        oEmployePrimeDAO.InsererPrime(oEmployePrime)
        lEnregistrementReussi.Visible = True
        fsPrime.Visible = False
        loadPrime()
    End Sub

    Private Sub cbAvNat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbAvNat.CheckedChanged
        tbModele.Visible = cbAvNat.Checked
        lblAvNat.visible = cbAvNat.Checked
    End Sub

    Private Sub loadPrime()
        Dim oEmployePrimeDAO As New CEmployePrimeDAO
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))
        Dim ds As DataSet

        ds = oEmployePrimeDAO.GetPrimeEmploye(iEmployeID)
        gvPrime.Visible = True
        gvPrime.DataSource = ds
        gvPrime.DataBind()
    End Sub
    Private Sub loadAugmentation()
        Dim oEmployeDAO As New CEmployeDAO
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))
        Dim ds As DataSet = oEmployeDAO.GetAugmentation(iEmployeID)
        gvAugmentation.Visible = True
        gvAugmentation.DataSource = ds
        gvAugmentation.DataBind()
    End Sub
    Private Sub gvPrime_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvPrime.RowCommand
        Dim oEmployePrimeDAO As New CEmployePrimeDAO
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))

        If e.CommandName = "SupprimerPrime" Then
            oEmployePrimeDAO.SupprimerPrime(iEmployeID, CInt(e.CommandArgument.ToString))
            loadPrime()
        End If
    End Sub

    Private Sub btHistorique_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btHistorique.Click
        Dim oEmployeDAO As New CEmployeDAO
        Dim oEmployePrimeDAO As New CEmployePrimeDAO
        Dim iEmployeID As Integer = CInt(Request.QueryString("id"))
        loadAugmentation()
        gvPrime.Columns(6).Visible = False
        loadPrime()
        lEnregistrementReussi.Visible = False
        fsPrime.Visible = False
        fNonFacturable.Visible = False
        fRepartitionFiliale.Visible = False
        rdFacturable.Visible = False
        lblAug.Visible = True
        lblPrime.Visible = True
        btSupprimer.Visible = False
    End Sub

   
    Private Sub gvPrime_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPrime.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(4).Text = "1" Then
                e.Row.Cells(4).Text = "OUI"
            Else
                e.Row.Cells(4).Text = "NON"
                e.Row.Cells(5).Text = "Aucun"
            End If
        End If
    End Sub
    Protected Sub btRetourListeEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetourListeEmploye.Click
        Response.Redirect("EmployeLister.aspx")
    End Sub
End Class