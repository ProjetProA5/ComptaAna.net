Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class GestionPrixQualifTypeAffaire
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    If Not CType(Session("Employe"), CEmploye).ProfilID = 3 Then
        '        Response.Redirect("~/Login.aspx?Erreur=403")
        '    End If
        'Catch ex As NullReferenceException
        '    Response.Redirect("~/Login.aspx?Erreur=401")
        'End Try

        Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
        Try
            If Not verifDroit(lDroit, eModule.AccesAdministration) Then
                Response.Redirect("~/Login.aspx?Erreur=403")
            End If
        Catch ex As NullReferenceException
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try


        LoadGridView()
        lblEnregistrement.Visible = False
    End Sub
    ''' <summary>
    ''' Charge le GridView avec les prix / Qualif / typeAffaire
    ''' </summary>
    Private Sub LoadGridView()
        Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
        Dim dsPrixQualifTypeAffaire As DataSet = oPrixQualifTypeAffaireDAO.GetAllPrixQualifTypeAffaire

        gvPrixQualifTypeAffaire.DataSource = dsPrixQualifTypeAffaire
        gvPrixQualifTypeAffaire.DataBind()
    End Sub

    ''' <summary>
    ''' Insert une nouvelle qualification et associe les prix HT en fonction des type d'affaire
    ''' </summary>
    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oPrixQualifTypeAffaire As CPrixQualifTypeAffaire
        Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
        Dim oQualificationDAO As New CQualificationDAO

        Dim iLastQualificationID As Integer
        Dim sQualificationLibelle As String
        Dim sContratCadre As String
        Dim sForfait As String
        Dim sRegie As String

        sContratCadre = Formater.FormatDecimal(tbNouveauContratCadre.Text)
        sForfait = Formater.FormatDecimal(tbNouveauForfait.Text)
        sRegie = Formater.FormatDecimal(tbNouveauRegie.Text)

        ' Contrat cadre = 1 || Forfait = 2 || Régie = 3
        sQualificationLibelle = tbNouveauLibelle.Text
        oQualificationDAO.InsertQualification(New CQualification(sQualificationLibelle))
        iLastQualificationID = oQualificationDAO.GetLastQualificationID

        ' Prix en fonction de la qualif --> Contrat cadre = 1 || Forfait = 2 || Régie = 3
        oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iLastQualificationID, 1, CDec(sContratCadre))
        oPrixQualifTypeAffaireDAO.InsertPrixQualifTypeAffaire(oPrixQualifTypeAffaire)

        oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iLastQualificationID, 2, CDec(sForfait))
        oPrixQualifTypeAffaireDAO.InsertPrixQualifTypeAffaire(oPrixQualifTypeAffaire)

        oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iLastQualificationID, 3, CDec(sRegie))
        oPrixQualifTypeAffaireDAO.InsertPrixQualifTypeAffaire(oPrixQualifTypeAffaire)

        LoadGridView()
    End Sub

    ''' <summary>
    ''' Enregistre les modifications des qualif et prix affaire.
    ''' </summary>
    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oPrixQualifTypeAffaire As CPrixQualifTypeAffaire
        Dim oPrixQualifTypeAffaireDAO As New CPrixQualifTypeAffaireDAO
        Dim oQualificationDAO As New CQualificationDAO
        Dim sContratCadre As String
        Dim sRegie As String
        Dim sForfait As String

        Dim iCount As Integer = gvPrixQualifTypeAffaire.Rows.Count

        For i = 0 To iCount - 1
            Dim tbQualificationLibelle As TextBox = CType(gvPrixQualifTypeAffaire.Rows(i).Cells(1).FindControl("tbQualificationLibelle"), TextBox)
            Dim tbContratCadre As TextBox = CType(gvPrixQualifTypeAffaire.Rows(i).Cells(2).FindControl("tbContratCadre"), TextBox)
            Dim tbForfait As TextBox = CType(gvPrixQualifTypeAffaire.Rows(i).Cells(3).FindControl("tbForfait"), TextBox)
            Dim tbRegie As TextBox = CType(gvPrixQualifTypeAffaire.Rows(i).Cells(4).FindControl("tbRegie"), TextBox)

            sContratCadre = Formater.FormatDecimal(tbContratCadre.Text)
            sForfait = Formater.FormatDecimal(tbForfait.Text)
            sRegie = Formater.FormatDecimal(tbRegie.Text)

            ' MAJ Qualification
            Dim iQualificationID As Integer = CType(CType(gvPrixQualifTypeAffaire.Rows(i).Cells(5).FindControl("lblQualificationID"), Label).Text, Integer)
            Dim bQualificationActif As Boolean = CType(gvPrixQualifTypeAffaire.Rows(i).Cells(0).FindControl("cbQualificationActif"), CheckBox).Checked
            oQualificationDAO.UpdateQualification(New CQualification(iQualificationID, tbQualificationLibelle.Text, bQualificationActif))

            ' Forfait = 1 || Contrat cadre = 2 || Régie = 3
            oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iQualificationID, 1, CDec(sForfait))
            oPrixQualifTypeAffaireDAO.UpdatePrixQualifTypeAffaire(oPrixQualifTypeAffaire)

            oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iQualificationID, 2, CDec(sContratCadre))
            oPrixQualifTypeAffaireDAO.UpdatePrixQualifTypeAffaire(oPrixQualifTypeAffaire)

            oPrixQualifTypeAffaire = New CPrixQualifTypeAffaire(iQualificationID, 3, CDec(sRegie))
            oPrixQualifTypeAffaireDAO.UpdatePrixQualifTypeAffaire(oPrixQualifTypeAffaire)
        Next

        lblEnregistrement.Visible = True
    End Sub
End Class