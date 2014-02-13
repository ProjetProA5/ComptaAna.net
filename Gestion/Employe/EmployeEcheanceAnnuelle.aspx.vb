Imports ComptaAna.Business
'Imports System.Windows.Forms
Imports Telerik.Charting
Imports System.Drawing
'Imports Telerik.Web.UI

Public Class EmployeEcheanceAnnuelle
    Inherits System.Web.UI.Page
    Dim iEmployeID As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        iEmployeID = CInt(Request.QueryString("id"))
        If Not Page.IsPostBack Then
            ChargerDDLAnnee()
            LoadEcheance()
            CChargerDDLAnneeGridView()
            ChargerDDLEcheanceGridView(ddlAnneeGridViewEcheance)

        End If
    End Sub

    Private Sub mOnglets_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mOnglets.MenuItemClick

        Select Case e.Item.Value
            Case "1"
                If IsNothing(iEmployeID) Then
                    Response.Redirect("EmployeModifier.aspx")
                Else
                    Response.Redirect("EmployeModifier.aspx?id=" & iEmployeID)
                End If
            Case "2"
                If IsNothing(iEmployeID) Then
                    Response.Redirect("EmployeCout.aspx")
                Else
                    Response.Redirect("EmployeCout.aspx?id=" & iEmployeID)
                End If
            Case "3"
                If IsNothing(iEmployeID) Then
                    Response.Redirect("EmployeDroit.aspx")
                Else
                    Response.Redirect("EmployeDroit.aspx?id=" & iEmployeID)
                End If
            Case "4"
                If IsNothing(iEmployeID) Then
                    Response.Redirect("Formation.aspx")
                Else
                    Response.Redirect("Formation.aspx?id=" & iEmployeID)
                End If
            Case "5"
                If IsNothing(iEmployeID) Then
                    Response.Redirect("EmployeEcheanceAnnuelle.aspx")
                Else
                    Response.Redirect("EmployeEcheanceAnnuelle.aspx?id=" & iEmployeID)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' chargement des années disponibles pour cet employé
    ''' </summary>
    Private Sub ChargerDDLAnnee()
        Dim oEmployeDAO As New CEmployeDAO
        ddlAnnee.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetAnneeDispo(iEmployeID)

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Annee").ToString, CStr(i))
            ddlAnnee.Items.Add(li)
        Next

    End Sub

    ''' <summary>
    ''' chargement des années disponibles pour cet employé
    ''' </summary>
    Private Sub ChargerDDLEcheance(ByRef ddlEchean As DropDownList)
        Dim oEmployeDAO As New CEmployeDAO
        ddlEchean.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetEcheanceAnnuelleAjout()

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("TypeEcheanceAnnuelleLibelle").ToString, ds.Tables(0).Rows(i)("TypeEcheanceAnnuelleID").ToString)
            ddlEchean.Items.Add(li)
        Next

    End Sub
    Private Sub ChargerDDLEcheanceGridView(ByRef ddlEchean As DropDownList)
        Dim oEmployeDAO As New CEmployeDAO
        ddlEchean.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetEcheanceAnnuelle()
        Dim li1 As ListItem
        li1 = New ListItem("ToutesEcheances", "-1")
        ddlEchean.Items.Add(li1)
        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("TypeEcheanceAnnuelleLibelle").ToString, ds.Tables(0).Rows(i)("TypeEcheanceAnnuelleID").ToString)
            ddlEchean.Items.Add(li)
        Next
    End Sub
    Private Sub ChargerDDLEmploye(ByRef ddlEchean As DropDownList)
        Dim oEmployeDAO As New CEmployeDAO
        ddlEchean.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetAllEmployeBis()
        'Dim li1 As ListItem
        'li1 = New ListItem("ToutesEcheances", "-1")
        'ddlEchean.Items.Add(li1)
        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Nom").ToString, ds.Tables(0).Rows(i)("EmployeID").ToString)
            ddlEchean.Items.Add(li)
        Next
    End Sub

    ''' <summary>
    ''' chargement des années disponibles pour tout les employés
    ''' </summary>
    Private Sub CChargerDDLAnneeGridView()
        Dim oEmployeDAO As New CEmployeDAO
        ddlAnneeGridView.Items.Clear()
        Dim ds As New DataSet
        ds = oEmployeDAO.GetAllAnneeDispo()

        Dim li As ListItem
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            li = New ListItem(ds.Tables(0).Rows(i)("Annee").ToString, CStr(i))
            ddlAnneeGridView.Items.Add(li)
        Next

    End Sub

    Private Sub btnAjout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjout.Click
        LabErreurNouvelleEcheance.Text = ""
        fsNouvelleEcheance.Visible = True
        
        ChargerDDLEcheance(ddlEcheance)
        ChargerDDLEmploye(ddlEmploye)



    End Sub

    Private Sub btnAfficherEcheance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAfficherEcheance.Click
        LoadEcheance()
        CChargerDDLAnneeGridView()
        ChargerDDLEcheanceGridView(ddlAnneeGridViewEcheance)
    End Sub
    Private Sub LoadEcheance()
        Dim oEmployeDAO As New CEmployeDAO
        Dim ds As DataSet
        Try
            ds = oEmployeDAO.GetAllEcheanceAnnuelleParEmploye(iEmployeID, CInt(ddlAnnee.SelectedItem.Text))
            gvEcheance.DataSource = ds
            gvEcheance.DataBind()
            fsEcheances.Visible = True
            gvEcheance.Visible = True
        Catch ex As Exception
            fsEcheances.Visible = True
            lblErreur.Visible = True
        End Try

    End Sub
    Private Sub gvEcheance_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEcheance.RowCommand
        Dim oEmployeDAO As New CEmployeDAO
        If e.CommandName = "SupprimerEcheance" Then
            oEmployeDAO.SupprimerEcheance(CInt(e.CommandArgument.ToString))
            LoadEcheance()
        End If
    End Sub

    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oEmployeDAO As New CEmployeDAO
        If tbDate.Text = "" Then
            LabErreurNouvelleEcheance.Text = "Veuiliez entrer une date."
            LabErreurNouvelleEcheance.ForeColor = Drawing.Color.Red
        Else
            Try
                LabErreurNouvelleEcheance.Text = ""
                oEmployeDAO.InsererEcheanceAnnuelle(CInt(ddlEmploye.SelectedValue), ddlEcheance.SelectedValue, tbDate.Text)
                fsNouvelleEcheance.Visible = False
                lblErreur.Visible = False
                ChargerDDLAnnee()
                LoadEcheance()
            Catch ex As Exception
                LabErreurNouvelleEcheance.Text = "Format de date incorrect"
                LabErreurNouvelleEcheance.ForeColor = Drawing.Color.Red
            End Try
            
        End If

    End Sub

    Protected Sub btRetourListeEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetourListeEmploye.Click
        Response.Redirect("EmployeLister.aspx")
    End Sub

    Sub CustomersGridView_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

        ' The GridViewCommandEventArgs class does not contain a 
        ' property that indicates which row's command button was
        ' clicked. To identify which row's button was clicked, use 
        ' the button's CommandArgument property by setting it to the 
        ' row's index.
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Retrieve the LinkButton control from the first column.
            Dim addButton As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)

            ' Set the LinkButton's CommandArgument property with the
            ' row's index.
            addButton.CommandArgument = e.Row.RowIndex.ToString()

        End If

    End Sub
    

    Private Sub btnGridview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGridview.Click
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsData As New DataSet
        Dim dsResultat As New DataSet
        dsResultat.Tables.Add("Echeance")
        'ajout des colone du DataSet
        dsResultat.Tables("Echeance").Columns.Add("Nom", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Janvier", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Fevrier", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Mars", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Avril", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Mai", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Juin", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Juillet", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Aout", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Septembre", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Octobre", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Novembre", GetType(String))
        dsResultat.Tables("Echeance").Columns.Add("Decembre", GetType(String))
        'permet d'obtenir les échéances annuelles sur une année.
        Dim strDateDeb As String = "01/01/" & ddlAnneeGridView.SelectedItem.Text
        Dim strDateFin As String = "31/12/" & ddlAnneeGridView.SelectedItem.Text
        ' requete à la base de donnée.
        If ddlAnneeGridViewEcheance.SelectedValue = "-1" Then
            dsData = oEmployeDAO.GetAllEcheanceAnnuelle(CDate(strDateDeb), CDate(strDateFin), -1)
        Else
            dsData = oEmployeDAO.GetAllEcheanceAnnuelle(CDate(strDateDeb), CDate(strDateFin), CInt(ddlAnneeGridViewEcheance.SelectedValue))
        End If

        For Each row As DataRow In dsData.Tables(0).Rows
            If Contient(dsResultat, CStr(row(0))) Then
                'Cas ou le nom de l'employe figure déja dans le dataset résultat.
                Dim i As Integer = indice(dsResultat, CStr(row(0)))
                ' on récupère le mois de léchéance courante.
                Dim monNombreCoupe() As String
                monNombreCoupe = Split(CStr(row(1)), "/", 3)

                Select Case monNombreCoupe(1) 'remplissage en fonction du mois de l'echéance.
                    Case "01"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(1)) = "" Then ' si la cellule est vide
                            dsResultat.Tables("Echeance").Rows(i)(1) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else ' sinon
                            dsResultat.Tables("Echeance").Rows(i)(1) = CStr(dsResultat.Tables("Echeance").Rows(i)(1)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "02"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(2)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(2) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(2) = CStr(dsResultat.Tables("Echeance").Rows(i)(2)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "03"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(3)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(3) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(3) = CStr(dsResultat.Tables("Echeance").Rows(i)(3)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "04"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(4)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(4) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(4) = CStr(dsResultat.Tables("Echeance").Rows(i)(4)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "05"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(5)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(5) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(5) = CStr(dsResultat.Tables("Echeance").Rows(i)(5)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "06"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(6)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(6) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(6) = CStr(dsResultat.Tables("Echeance").Rows(i)(6)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "07"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(7)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(7) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(7) = CStr(dsResultat.Tables("Echeance").Rows(i)(7)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "08"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(8)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(8) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(8) = CStr(dsResultat.Tables("Echeance").Rows(i)(8)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "09"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(9)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(9) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(9) = CStr(dsResultat.Tables("Echeance").Rows(i)(9)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "10"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(10)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(10) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(10) = CStr(dsResultat.Tables("Echeance").Rows(i)(10)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "11"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(11)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(11) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(11) = CStr(dsResultat.Tables("Echeance").Rows(i)(11)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                    Case "12"
                        If CStr(dsResultat.Tables("Echeance").Rows(i)(12)) = "" Then
                            dsResultat.Tables("Echeance").Rows(i)(12) = CStr(row(2)) + " le : " + CStr(row(1))
                        Else
                            dsResultat.Tables("Echeance").Rows(i)(12) = CStr(dsResultat.Tables("Echeance").Rows(i)(12)) + " <br/> " + CStr(row(2)) + " le : " + CStr(row(1))
                        End If
                End Select
            Else
                'Cas ou le nom de l'employe na pas encore été ajouter dans le dataset résultat.
                ' on récupère le mois de l'échéance courante.
                Dim monNombreCoupe() As String
                monNombreCoupe = Split(CStr(row(1)), "/", 3)
               
                Select Case monNombreCoupe(1)
                    Case "01"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "", "", "", "", "", "")
                    Case "02"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "", "", "", "", "")
                    Case "03"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "", "", "", "")
                    Case "04"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "", "", "")
                    Case "05"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "", "")
                    Case "06"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "", "")
                    Case "07"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "", "")
                    Case "08"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "", "")
                    Case "09"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "", "")
                    Case "10"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "", "")
                    Case "11"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)), "")
                    Case "12"
                        dsResultat.Tables("Echeance").Rows.Add(row(0), "", "", "", "", "", "", "", "", "", "", "", CStr(row(2)) + " le : " + CStr(row(1)))
                End Select
            End If

        Next
        ' on envoie le dataset dans le gridview.
        gvGrapheEcheance.DataSource = dsResultat
        gvGrapheEcheance.DataBind()
        ' on rend le filedset visible.
        fsGridView.Visible = True
        'permet d'afficher le graphique sur les échéance annuelle.
        GrapheEcheance(rcEcheanceAnnuelle, LabrcEchenaceAnnuelle)
    End Sub
    Public Sub GrapheEcheance(ByRef RcGraphe As Telerik.Web.UI.RadChart, ByRef label As Label)
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsData As New DataSet
        Dim dsResultat As New DataSet
        dsResultat.Tables.Add("GrapheEcheance")
        'ajout des colone du DataSet
        dsResultat.Tables("GrapheEcheance").Columns.Add("ID", GetType(Integer))
        dsResultat.Tables("GrapheEcheance").Columns.Add("Nom", GetType(String))
        dsResultat.Tables("GrapheEcheance").Columns.Add("DateDeb", GetType(Double))
        dsResultat.Tables("GrapheEcheance").Columns.Add("DateFin", GetType(Double))
        dsResultat.Tables("GrapheEcheance").Columns.Add("DateDebString", GetType(String))
        'permet d'obtenir les échéances annuelles sur une année.
        Dim strDateDeb As String = "01/01/" & ddlAnneeGridView.SelectedItem.Text
        Dim strDateFin As String = "31/12/" & ddlAnneeGridView.SelectedItem.Text
        ' requete à la base de donnée.
        If ddlAnneeGridViewEcheance.SelectedValue = "-1" Then
            dsData = oEmployeDAO.GetAllEcheanceAnnuelleGraphe(CDate(strDateDeb), CDate(strDateFin), -1)
        Else
            dsData = oEmployeDAO.GetAllEcheanceAnnuelleGraphe(CDate(strDateDeb), CDate(strDateFin), CInt(ddlAnneeGridViewEcheance.SelectedValue))
        End If
        If dsData.Tables(0).Rows.Count > 0 Then
            'Si la base contient des échéances annuelles sur l'année passé en paramètre.
            For Each row As DataRow In dsData.Tables(0).Rows
                Dim dDate As Date = CDate(row(2))

                dsResultat.Tables("GrapheEcheance").Rows.Add(row(0), row(1), dDate.ToOADate(), dDate.AddDays(3).ToOADate(), dDate)
            Next

            RcGraphe.DataSource = dsResultat
            RcGraphe.DataBind()
            RcGraphe.Height = (dsResultat.Tables(0).Rows.Count * 15) + 150
            'changement des itemlabels.
            For row = 0 To RcGraphe.Series(0).Items.Count - 1
                'on ne récupère que l'année le mois et le jour.
                Dim strSub As String = CStr(dsResultat.Tables("GrapheEcheance").Rows(row)(4)).Substring(0, 5)
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

End Class