Imports ComptaAna.Business
Imports Obout.Ajax.UI
Imports ComptaAna.net.Droit
Imports System.Drawing
Imports Telerik.Charting.ChartAxisItem
Imports Telerik.Charting

Public Class Formation
    Inherits System.Web.UI.Page
    Dim iEmployeID As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        iEmployeID = CInt(Request.QueryString("ID"))

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
            CChargerDDLAnneeGridView()
            fsNouvelle.Visible = False
            fsRecherche.Visible = True
            lblConfirmation.Visible = False
        End If
        lblConfirmation.Visible = False

    End Sub

    ''' <summary>
    ''' Permet de charger le tableau des formations de l'employé
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeView()
        Dim oFormationDAO As New CFormationDAO
        Dim ds As DataSet = oFormationDAO.GetFormationsEmploye(iEmployeID)
        gvFormation.DataSource = ds
        gvFormation.DataBind()

    End Sub

    ''' <summary>
    ''' Permet d'enregister une nouvelle formation prévue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btEnregistrer.Click
        Dim oFormation As CFormation
        Dim oFormationDAO As New CFormationDAO
        Dim slibelle As String = tbLibelle.Text
        Dim dNbHeure As Decimal = CDec(tbNbHeures.Text)
        Dim sOrganisme As String = tbOrganisme.Text
        Dim iType As Integer = 2

        Dim dDateDeb As Date = CDate(tbDateDebutPer.Text)
        Dim dDateFin As Date = CDate(tbDateFinPer.Text)
        Dim dblCout As Double = CDbl((tbMontant.Text))

        oFormation = New CFormation(slibelle, iEmployeID, dNbHeure, iType, sOrganisme, dDateDeb, dDateFin, dblCout)
        Dim iInsert As Integer = oFormationDAO.InsertFormation(oFormation)

        If iInsert = 1 Then
            lblConfirmation.Visible = True
            lblConfirmation.ForeColor = Drawing.Color.Blue
            lblConfirmation.Text = "Formation enregistrée"
            LoadTreeView()
            fsRecherche.Visible = True
            fsNouvelle.Visible = False
        Else
            lblConfirmation.Visible = True
            lblConfirmation.ForeColor = Drawing.Color.Red
            lblConfirmation.Text = "Enregistrement non réussi"

        End If
    End Sub

    ''' <summary>
    ''' Evênement sur le bouton Nouvelle formation prévue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnInsererFormation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsererFormation.Click
        fsRecherche.Visible = False
        fsNouvelle.Visible = True
    End Sub

    ''' <summary>
    ''' Permet de valider le recherche de formations
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ibValider_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibValider.Click
        LoadTreeViewRecherche()
        cDateDeb.SelectedDate = Nothing
        cDateFin.SelectedDate = Nothing
    End Sub

    Private Sub mOnglets_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mOnglets.MenuItemClick
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

    ''' <summary>
    ''' Permet de charger le tableau des formations suivant les critères de recherches (Type et dates)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadTreeViewRecherche()
        Dim oFormationDAO As New CFormationDAO
        Dim ds As DataSet = oFormationDAO.GetFormationEmployeParTypePeriode(cbbTypeFormation.SelectedValue, tbDateDeb.Text, tbDateFin.Text, iEmployeID)
        gvFormation.DataSource = ds
        gvFormation.DataBind()
    End Sub

    Private Sub gvFormation_DataBound1(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvFormation.DataBound
        gvFormation.Columns(9).Visible = False
    End Sub



    Private Sub gvFormation_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFormation.RowCommand
        If e.CommandName = "SupprimerFormation" Then
            Dim oFormationDAO As New CFormationDAO
            Dim iFormationID As Integer = CInt(e.CommandArgument.ToString)
            Dim iRes As Integer = oFormationDAO.DeleteFormation(iFormationID)
            If iRes = 1 Then
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Drawing.Color.Blue
                lblConfirmation.Text = "Formation supprimée"
            Else
                lblConfirmation.Visible = True
                lblConfirmation.ForeColor = Drawing.Color.Red
                lblConfirmation.Text = "Formation non supprimée"
            End If
            LoadTreeView()
        End If
    End Sub


    Private Sub gvFormation_DataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvFormation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim iType As Integer = CInt(DataBinder.Eval(e.Row.DataItem, "FormationType"))
            Select Case iType
                Case 0
                    e.Row.Cells(3).Text = "Suivie"
                    e.Row.Cells(5).Text = "Non renseigné"
                    e.Row.Cells(7).Text = e.Row.Cells(9).Text
                    e.Row.Cells(8).Text = e.Row.Cells(9).Text
                Case 1
                    e.Row.Cells(3).Text = "Dispensée"
                    e.Row.Cells(6).Text = "Non renseigné"
                    e.Row.Cells(7).Text = e.Row.Cells(9).Text
                    e.Row.Cells(8).Text = e.Row.Cells(9).Text
                Case 2
                    e.Row.Cells(3).Text = "Prévue"
                    e.Row.Cells(5).Text = "Non renseigné"
                    'e.Row.Cells(6).Text = "Non renseigné"
                    'e.Row.Cells(7).Text = "Non renseigné"
                    e.Row.Cells(10).Visible = True

            End Select

        End If
    End Sub

    Private Sub btRetourListeFormation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRetourListeFormation.Click
        Response.Redirect("EmployeLister.aspx")
    End Sub

    Private Sub btnGridview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGridview.Click
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsData, dsResultat, dsTest As New DataSet
        dsResultat.Tables.Add("GridViewFormation")
        'dsData.Tables.Add("Test")
        'dsData.Tables("Test").Columns.Add("Nom", GetType(String))
        'dsData.Tables("Test").Columns.Add("Fevrier", GetType(String))
        'Ajout de colonnes à la table GridViewFormation.
        dsResultat.Tables("GridViewFormation").Columns.Add("Nom", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Janvier", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Fevrier", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Mars", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Avril", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Mai", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Juin", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Juillet", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Aout", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Septembre", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Octobre", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Novembre", GetType(String))
        dsResultat.Tables("GridViewFormation").Columns.Add("Decembre", GetType(String))
        'Permet d'obtenir les formation sur une année.
        Dim strDateDeb As String = "01/01/" + ddlAnneeGridView.SelectedItem.Text
        Dim strDateFin As String = "31/12/" + ddlAnneeGridView.SelectedItem.Text
        'requete a la base de donnée.
        dsTest = oEmployeDAO.GetAllFormationGridView(CDate(strDateDeb), CDate(strDateFin))

        For Each row As DataRow In dsTest.Tables(0).Rows
            If Contient(dsResultat, CStr(row(0))) Then
                Dim i As Integer = indice(dsResultat, CStr(row(0)))
                'Dim DataRow As DataRow = dsResultat.Tables("GridViewFormation").Rows(i)
                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(1)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(1) = CStr(row(1))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(1) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(1)) + " <br/> " + CStr(row(1))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(2)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(2) = CStr(row(2))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(2) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(2)) + " <br/> " + CStr(row(2))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(3)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(3) = CStr(row(3))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(3) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(3)) + " <br/> " + CStr(row(3))
                End If
                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(4)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(4) = CStr(row(4))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(4) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(4)) + " <br/> " + CStr(row(4))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(5)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(5) = CStr(row(5))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(5) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(5)) + " <br/> " + CStr(row(5))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(6)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(6) = CStr(row(6))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(6) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(6)) + " <br/> " + CStr(row(6))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(7)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(7) = CStr(row(7))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(7) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(7)) + " <br/> " + CStr(row(7))
                End If
                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(8)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(8) = CStr(row(8))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(8) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(8)) + " <br/> " + CStr(row(8))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(9)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(9) = CStr(row(9))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(9) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(9)) + " <br/> " + CStr(row(9))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(10)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(10) = CStr(row(10))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(10) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(10)) + " <br/> " + CStr(row(10))
                End If

                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(11)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(11) = CStr(row(11))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(11) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(11)) + " <br/> " + CStr(row(11))
                End If
                If CStr(dsResultat.Tables("GridViewFormation").Rows(i)(12)) = "" Then
                    dsResultat.Tables("GridViewFormation").Rows(i)(12) = CStr(row(12))
                Else
                    dsResultat.Tables("GridViewFormation").Rows(i)(12) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(12)) + " <br/> " + CStr(row(12))
                End If
                'dsResultat.Tables("GridViewFormation").Rows(i)(3) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(3)) + " <br/> " + CStr(row(3))
                'dsResultat.Tables("GridViewFormation").Rows(i)(4) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(4)) + " <br/> " + CStr(row(4))
                'dsResultat.Tables("GridViewFormation").Rows(i)(5) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(5)) + " <br/> " + CStr(row(5))
                'dsResultat.Tables("GridViewFormation").Rows(i)(6) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(6)) + " <br/> " + CStr(row(6))
                'dsResultat.Tables("GridViewFormation").Rows(i)(7) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(7)) + " <br/> " + CStr(row(7))
                'dsResultat.Tables("GridViewFormation").Rows(i)(8) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(8)) + " <br/> " + CStr(row(8))
                'dsResultat.Tables("GridViewFormation").Rows(i)(9) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(9)) + " <br/> " + CStr(row(9))
                'dsResultat.Tables("GridViewFormation").Rows(i)(10) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(10)) + " <br/> " + CStr(row(10))
                'dsResultat.Tables("GridViewFormation").Rows(i)(11) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(11)) + " <br/> " + CStr(row(11))
                'dsResultat.Tables("GridViewFormation").Rows(i)(12) = CStr(dsResultat.Tables("GridViewFormation").Rows(i)(12)) + " <br/> " + CStr(row(12))
                'dsData.Tables("Test").Rows(i)(1) = CStr(dsData.Tables("Test").Rows(i)(1)) + " " + CStr(row(1))
            Else
                'dsData.Tables("Test").Rows.Add(row(0), row(1))
                dsResultat.Tables("GridViewFormation").Rows.Add(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7), row(8), row(9), row(10), row(11), row(12))
            End If
        Next
        gvGrapheFormation.DataSource = dsResultat
        'gvTest.DataSource = dsData
        'gvTest.DataBind()
        gvGrapheFormation.DataBind()
        GrapheEcheance(rcFormation, LabrcFormation)
    End Sub

    Public Sub GrapheEcheance(ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label)
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsData As New DataSet
        Dim dsResultat As New DataSet
        dsResultat.Tables.Add("GrapheFormation")
        'ajout des colonnes du DataSet
        dsResultat.Tables("GrapheFormation").Columns.Add("Nom", GetType(String))
        dsResultat.Tables("GrapheFormation").Columns.Add("DateDeb", GetType(Double))
        dsResultat.Tables("GrapheFormation").Columns.Add("DateFin", GetType(Double))
        dsResultat.Tables("GrapheFormation").Columns.Add("DateDebString", GetType(String))
        'permet d'obtenir les échéances annuelles sur une année.
        Dim strDateDeb As String = "01/01/" & ddlAnneeGridView.SelectedItem.Text
        Dim strDateFin As String = "31/12/" & ddlAnneeGridView.SelectedItem.Text
        ' requete à la base de donnée.
        dsData = oEmployeDAO.GetAllFormationGraphe(CDate(strDateDeb), CDate(strDateFin))

        If dsData.Tables(0).Rows.Count > 0 Then
            'Si la base contient des échéances annuelles sur l'année passé en paramètre.
            For Each row As DataRow In dsData.Tables(0).Rows
                Dim dDateDeb As Date = CDate(row(1))
                Dim dDateFin As Date = CDate(row(2))
                dsResultat.Tables("GrapheFormation").Rows.Add(row(0), dDateDeb.ToOADate(), dDateFin.ToOADate(), (CStr(dDateDeb).Substring(0, 5)) + " au " + (CStr(dDateFin).Substring(0, 5)))
            Next

            RcGraphe.DataSource = dsResultat
            RcGraphe.DataBind()
            RcGraphe.Height = (dsResultat.Tables(0).Rows.Count * 15) + 150
            'changement des itemlabels.
            For row = 0 To RcGraphe.Series(0).Items.Count - 1
                'on ne récupère que l'année le mois et le jour.
                Dim strSub As String = CStr(dsResultat.Tables("GrapheFormation").Rows(row)(3))
                RcGraphe.Series(0).SetItemLabel(row, strSub)
            Next
            RcGraphe.Visible = True
            label.Text = ""
            Dim barColors As Color() = New Color(0) {Color.GreenYellow}
            For Each item As ChartSeriesItem In RcGraphe.Series(0).Items
                item.Appearance.FillStyle.MainColor = barColors(0)
                item.Appearance.FillStyle.SecondColor = barColors(0)
            Next

        Else 'Quand la base ne contient pas d'échéances annuelles. 
            RcGraphe.Visible = False
            label.Text = "Pas d'échéance annuelle sur cette année."
            label.ForeColor = Color.Red
        End If
            

    End Sub
    ''' <summary>
    ''' chargement des années disponibles pour cet employé
    ''' </summary>
    Private Sub CChargerDDLAnneeGridView()
        Dim oEmployeDAO As New CEmployeDAO
        ddlAnneeGridView.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetAllAnneeDispoFormation()

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Annee").ToString, CStr(i))
            ddlAnneeGridView.Items.Add(li)
        Next

    End Sub
    Private Function Contient(ByVal dsData As DataSet, ByVal str As String) As Boolean
        For Each row As DataRow In dsData.Tables(0).Rows
            If CStr(row(0)) = str Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function indice(ByVal dsData As DataSet, ByVal str As String) As Integer
        Dim I As Integer = 0
        For Each row As DataRow In dsData.Tables(0).Rows
            If CStr(row(0)) = str Then
                Return I
            End If
            I = I + 1
        Next
        Return I
    End Function
    'Private Sub gvFormation_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvTest.DataBound
    '    'If CDbl(gvTest.Rows(gvTest.Rows.Count - 1).Cells(4).Text) = 0 Then
    '    '    gvTest.Rows(gvTest.Rows.Count - 1).Visible = False
    '    'End If


    'End Sub
    'Protected Sub gvBonCommande_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvTest.RowEditing
    '    gvTest.EditIndex = e.NewEditIndex


    'End Sub
    'Protected Sub gvFormation_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvTest.RowUpdating
    '    Dim oEmployeDAO As New CEmployeDAO
    '    Dim dsData, dsResultat, dsTest As New DataSet
    '    dsData.Tables.Add("Test")
    '    dsData.Tables("Test").Columns.Add("Nom", GetType(String))
    '    dsData.Tables("Test").Columns.Add("Janvier", GetType(String))
    '    dsData.Tables("Test").Columns.Add("ID", GetType(Integer))
    '    'Ajout de colonnes à la table GridViewFormation.
    '    'gvTest.Rows(e.RowIndex).Cells(1).Text
    '    'Permet d'obtenir les formation sur une année.
    '    Dim strDateDeb As String = "01/01/" + ddlAnneeGridView.SelectedItem.Text
    '    Dim strDateFin As String = "31/12/" + ddlAnneeGridView.SelectedItem.Text
    '    'requete a la base de donnée.
    '    dsTest = oEmployeDAO.GetAllFormationGridView(CDate(strDateDeb), CDate(strDateFin))

    '    For Each row As DataRow In dsTest.Tables(0).Rows
    '        If Contient(dsData, CStr(row(0))) Then
    '            Dim i As Integer = indice(dsData, CStr(row(0)))


    '            dsData.Tables("Test").Rows(i)(1) = CStr(dsData.Tables("Test").Rows(i)(1)) + " " + CStr(row(1))
    '        Else
    '            dsData.Tables("Test").Rows.Add(row(0), row(1), row(13))
    '        End If
    '    Next
    '    dsResultat = oEmployeDAO.GetFormationInformation(CInt(dsData.Tables("Test").Rows(e.RowIndex)(2)), CDate(strDateDeb), CDate(strDateFin))
    '    gvTest.Columns(1).ControlStyle.Width = 123
    '    gvPopup.DataSource = dsResultat
    '    gvPopup.DataBind()
    '    pVisualisation.Visible = True
    'End Sub
    'Protected Sub gvFacture_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)

    'End Sub

    'Private Sub gvFacture_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTest.RowDataBound

    'End Sub
End Class