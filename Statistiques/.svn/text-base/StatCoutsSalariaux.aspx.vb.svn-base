Imports ComptaAna.Business
Imports Obout.ComboBox
Imports ComptaAna.net.Droit

Public Class StatCoutsSalariaux
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesStatsCoutSalaires) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            ChargerCbbAnnee()
        End If


    End Sub

    Public Sub ChargeCoutsSalariaux()
        gvStatCoutSalaire.Columns.Clear()
        Dim oStatistiquesDAO As New CStatistiquesDAO

        Dim bndEmployeID As New BoundField
        bndEmployeID.DataField = "EmployeID"
        bndEmployeID.HeaderText = "EmployeID"
        bndEmployeID.Visible = False
        gvStatCoutSalaire.Columns.Add(bndEmployeID)

        Dim bndEmployeNom As New BoundField
        bndEmployeNom.DataField = "Employe"
        bndEmployeNom.HeaderText = "Employé"
        gvStatCoutSalaire.Columns.Add(bndEmployeNom)

        Dim bndSalaire As New BoundField
        bndSalaire.DataField = "Salaire"
        bndSalaire.HeaderText = "Salaire"
        gvStatCoutSalaire.Columns.Add(bndSalaire)

        Dim bndRepICPE As New BoundField
        bndRepICPE.DataField = "rep_ICPE"
        bndRepICPE.HeaderText = "Répartition ICPE"
        gvStatCoutSalaire.Columns.Add(bndRepICPE)

        Dim bndCoutICPE As New BoundField
        bndCoutICPE.DataField = "cout_ICPE"
        bndCoutICPE.HeaderText = "Coût ICPE"
        gvStatCoutSalaire.Columns.Add(bndCoutICPE)

        Dim bndRepSTJ As New BoundField
        bndRepSTJ.DataField = "rep_STJ"
        bndRepSTJ.HeaderText = "Répartition STJ"
        gvStatCoutSalaire.Columns.Add(bndRepSTJ)

        Dim bndCoutSTJ As New BoundField
        bndCoutSTJ.DataField = "cout_STJ"
        bndCoutSTJ.HeaderText = "Coût STJ"
        gvStatCoutSalaire.Columns.Add(bndCoutSTJ)

        Dim bndRepET As New BoundField
        bndRepET.DataField = "rep_ET"
        bndRepET.HeaderText = "Répartition ET"
        gvStatCoutSalaire.Columns.Add(bndRepET)

        Dim bndCoutET As New BoundField
        bndCoutET.DataField = "cout_ET"
        bndCoutET.HeaderText = "Coût ET"
        gvStatCoutSalaire.Columns.Add(bndCoutET)

        Dim bndRepADMIN As New BoundField
        bndRepADMIN.DataField = "rep_ADMIN"
        bndRepADMIN.HeaderText = "Répartition ADMINISTRATIF"
        gvStatCoutSalaire.Columns.Add(bndRepADMIN)

        Dim bndCoutADMIN As New BoundField
        bndCoutADMIN.DataField = "cout_ADMIN"
        bndCoutADMIN.HeaderText = "Coût ADMINISTRATIF"
        gvStatCoutSalaire.Columns.Add(bndCoutADMIN)

        Dim bndRepASA As New BoundField
        bndRepASA.DataField = "rep_ASA"
        bndRepASA.HeaderText = "Répartition ASA"
        gvStatCoutSalaire.Columns.Add(bndRepASA)

        Dim bndCoutASA As New BoundField
        bndCoutASA.DataField = "cout_ASA"
        bndCoutASA.HeaderText = "Coût ASA"
        gvStatCoutSalaire.Columns.Add(bndCoutASA)

        Dim dt As New DataTable()
        dt.Columns.Add("EmployeID")
        dt.Columns.Add("Employe")
        dt.Columns.Add("Salaire")
        dt.Columns.Add("rep_ICPE")
        dt.Columns.Add("cout_ICPE")
        dt.Columns.Add("rep_STJ")
        dt.Columns.Add("cout_STJ")
        dt.Columns.Add("rep_ET")
        dt.Columns.Add("cout_ET")
        dt.Columns.Add("rep_ADMIN")
        dt.Columns.Add("cout_ADMIN")
        dt.Columns.Add("rep_ASA")
        dt.Columns.Add("cout_ASA")
        dt.Rows.Add(dt.NewRow())

        gvStatCoutSalaire.DataSource = dt
        gvStatCoutSalaire.DataBind()
    End Sub

    Public Sub ChargerCbbAnnee()
        cbbPeriodeAnnee.Items.Clear()

        Dim iAnneeCourante As Integer = Now.Year
        Dim iAnneeDebut As Integer = 2009

        For i = iAnneeDebut To iAnneeCourante
            Dim cbbAnneeItem As New ComboBoxItem
            cbbAnneeItem.Value = CStr(i)
            cbbAnneeItem.Text = CStr(i)

            cbbPeriodeAnnee.Items.Insert(0, cbbAnneeItem)
        Next

        cbbPeriodeAnnee.DataBind()
        cbbPeriodeAnnee.SelectedValue = CStr(iAnneeCourante)
    End Sub

    Private Sub btnRechercheStat_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRechercheStat.Click
        ChargeCoutsSalariaux()
    End Sub
End Class