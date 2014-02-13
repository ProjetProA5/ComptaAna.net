Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class GestionTVA
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
    ''' Charge le GridView avec la liste des TVA
    ''' </summary>
    Private Sub LoadGridView()
        Dim oTVADAO As New CTVADAO
        Dim dsTva As DataSet

        dsTva = oTVADAO.getAllTva

        gvTva.DataSource = dsTva
        gvTva.DataBind()
    End Sub

    ''' <summary>
    ''' Insert une nouvelle TVA
    ''' </summary>
    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oTVADAO As New CTVADAO
        Dim oTVA As CTVA
        Dim sTvaTaux As String

        sTvaTaux = Formater.FormatDecimal(tbNouveauTaux.Text)
        
        oTVA = New CTVA(CDec(sTvaTaux))
        oTVADAO.InsertTva(oTVA)
        LoadGridView()
    End Sub

    ''' <summary>
    ''' Enregistre les modifications des TVA
    ''' </summary>
    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oTVADAO As New CTVADAO
        Dim oTVA As CTVA
        Dim sTvaTaux As String
        Dim iCount As Integer = gvTva.Rows.Count

        For i = 0 To iCount - 1
            Dim tbTvaTaux As TextBox = CType(gvTva.Rows(i).Cells(1).FindControl("tbTvaTaux"), TextBox)
            Dim iTvaID As Integer = CType(CType(gvTva.Rows(i).Cells(2).FindControl("lblTvaID"), Label).Text, Integer)
            Dim bTvaActif As Boolean = CType(gvTva.Rows(i).Cells(3).FindControl("cbTvaActif"), CheckBox).Checked

            sTvaTaux = Formater.FormatDecimal(tbTvaTaux.Text)
            oTVA = New CTVA(iTvaID, CDec(sTvaTaux), bTvaActif)
            oTVADAO.UpdateTva(oTVA)

        Next
        lblEnregistrement.Visible = True
    End Sub
End Class