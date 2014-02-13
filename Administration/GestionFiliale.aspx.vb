Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class GestionFiliale
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


        lblEnregistrement.Visible = False
        LoadGridView()

    End Sub
    '''<summary>
    '''Charge le GridVIew de filiales.
    '''</summary>
    Private Sub LoadGridView()
        Dim oFilialeDAO As New CFilialeDAO
        Dim dsFiliale As DataSet

        dsFiliale = oFilialeDAO.getAllFiliale

        gvFiliale.DataSource = dsFiliale
        gvFiliale.DataBind()
    End Sub

    '''<summary>
    ''' Insert une nouvelle Filiale.
    '''</summary>
    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oFilialeDAO As New CFilialeDAO
        Dim oFiliale As CFiliale

        oFiliale = New CFiliale(tbNouveauNom.Text)
        oFilialeDAO.InsertFiliale(oFiliale)

        LoadGridView()
    End Sub

    ''' <summary>
    ''' Enregistre les modifications dans les filiales.
    ''' </summary>
    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oFilialeDAO As New CFilialeDAO
        Dim oFiliale As CFiliale

        Dim iCount As Integer = gvFiliale.Rows.Count

        For i = 0 To iCount - 1
            Dim tbFilialeNom As TextBox = CType(gvFiliale.Rows(i).Cells(0).FindControl("tbFilialeNom"), TextBox)
            Dim iFilialeID As Integer = CType(CType(gvFiliale.Rows(i).Cells(1).FindControl("lblFilialeID"), Label).Text, Integer)
            Dim bFilialeActif As Boolean = (CType(gvFiliale.Rows(i).Cells(2).FindControl("cbFilialeActif"), CheckBox).Checked)

            oFiliale = New CFiliale(iFilialeID, tbFilialeNom.Text, bFilialeActif)
            oFilialeDAO.UpdateFiliale(oFiliale)
            lblEnregistrement.Visible = True
        Next
    End Sub
End Class