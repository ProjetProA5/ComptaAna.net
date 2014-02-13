Imports ComptaAna.Business
Public Class GestionParametreGlobal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack Then

        Else
            fsEffacerTypeEcheance.Visible = True
            LoadGridviewEffacerEcheance(gvEffacerTypeEcheanceAnnuelle)
        End If
    End Sub

    Private Sub mOnglets_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles mOnglets.MenuItemClick

        Select Case e.Item.Value
            Case "1"
                Response.Redirect("GestionParametreGlobal.aspx")
        End Select
    End Sub

    Private Sub btnAjoutTypeEcheance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjoutTypeEcheance.Click
        LabTypeEcheanceErreur.Text = ""
        fsNouveauTypeEcheance.Visible = True
        fsEffacerTypeEcheance.Visible = False
    End Sub
    Private Sub btnEffacerTypeEcheance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEffacerTypeEcheance.Click
        'charge l'affichage pour supprimer un type d'échéance.
        fsNouveauTypeEcheance.Visible = False
        LoadGridviewEffacerEcheance(gvEffacerTypeEcheanceAnnuelle)
        fsEffacerTypeEcheance.Visible = True
        LabTypeEcheanceErreur.Text = ""

    End Sub
   
    Private Sub btnEnregistrerTypeEcheance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrerTypeEcheance.Click
        Dim oEmployeDAO As New CEmployeDAO
        Dim ds As DataSet = oEmployeDAO.GetEcheanceAnnuelleByName(tbTypeEcheance.Text)
        Try
            ' permet d'enregistrer un nouveau type d'échéance.
            If tbTypeEcheance.Text = "" Then
                LabTypeEcheanceErreur.Text = "Vous n'avez pas saisi de nom."
                LabTypeEcheanceErreur.ForeColor = Drawing.Color.Red
            ElseIf ds.Tables(0).Rows.Count = 0 Then
                LabTypeEcheanceErreur.Text = "Nouveau type d'échéance enregistré avec succès."
                LabTypeEcheanceErreur.ForeColor = Drawing.Color.Green
                oEmployeDAO.InsererTypeEcheanceAnnuelle(tbTypeEcheance.Text)
                fsNouveauTypeEcheance.Visible = True
            Else
                LabTypeEcheanceErreur.Text = "Ce type d'échéance existe déja."
                LabTypeEcheanceErreur.ForeColor = Drawing.Color.Red
            End If


        Catch
            LabTypeEcheanceErreur.Text = "L'enregistrement du nouveau type d'échéance a échoué. Il est possible que vous ayez tapé un caractère illicite."
            LabTypeEcheanceErreur.ForeColor = Drawing.Color.Red
        End Try
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
    Private Sub LoadGridviewEffacerEcheance(ByRef gvEffacerTypeEcheanc As GridView)
        Dim oEmployeDAO As New CEmployeDAO
        Dim dsData As New DataSet
        dsData.Tables.Add("Echeance")
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleID", GetType(Integer))
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleLibelle", GetType(String))
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleActif", GetType(Boolean))
        Dim ds As New DataSet
        ds = oEmployeDAO.GetEcheanceAnnuelleGridview()
        For Each row As DataRow In ds.Tables(0).Rows
            If CInt(row(2)) = 1 Then
                dsData.Tables("Echeance").Rows.Add(row(0), row(1), True)
            Else
                dsData.Tables("Echeance").Rows.Add(row(0), row(1), False)
            End If

        Next
        gvEffacerTypeEcheanc.DataSource = dsData
        gvEffacerTypeEcheanc.DataBind()

    End Sub

    Protected Sub gvEcheance_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvEffacerTypeEcheanceAnnuelle.RowUpdating
        Dim oEmployeDAO As New CEmployeDAO


        Dim dsData As New DataSet
        dsData.Tables.Add("Echeance")
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleID", GetType(Integer))
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleLibelle", GetType(String))
        dsData.Tables("Echeance").Columns.Add("TypeEcheanceAnnuelleActif", GetType(Boolean))
        Dim ds As New DataSet
        ds = oEmployeDAO.GetEcheanceAnnuelleGridview()
        For Each row As DataRow In ds.Tables(0).Rows
            If CInt(row(2)) = 1 Then
                dsData.Tables("Echeance").Rows.Add(row(0), row(1), True)
            Else
                dsData.Tables("Echeance").Rows.Add(row(0), row(1), False)
            End If
        Next
        If CBool(dsData.Tables("Echeance").Rows(e.RowIndex)(2)) Then ' on désactive le type d'échéance.
            oEmployeDAO.SupprimerEcheanceTypeAnnuelle(CInt(dsData.Tables("Echeance").Rows(e.RowIndex)(0)), 0)
        Else ' on réactive le type d'échéance.
            oEmployeDAO.SupprimerEcheanceTypeAnnuelle(CInt(dsData.Tables("Echeance").Rows(e.RowIndex)(0)), 1)
        End If
        'on recharge le gridview
        LoadGridviewEffacerEcheance(gvEffacerTypeEcheanceAnnuelle)

    End Sub
End Class