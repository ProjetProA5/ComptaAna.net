Imports ComptaAna.Business
Public Class RIB
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadGridView()
        End If
    End Sub

    'Private Sub rblIBANouRIB_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblIBANouRIB.SelectedIndexChanged
    '    If rblIBANouRIB.SelectedIndex = 1 Then
    '        panIBAN.Visible = True
    '    Else
    '        panIBAN.Visible = False
    '    End If
    'End Sub

    Private Sub btnEnregistrer_Click(sender As Object, e As System.EventArgs) Handles btnEnregistrer.Click
        Dim bParDefaut As Boolean = cbDefault.Checked
        'Dim bIBAN As Boolean = True
        'If rblIBANouRIB.SelectedIndex = 0 Then
        '    bIBAN = False
        'End If
        If legend.InnerText = "Modification de la Coordonnée Bancaire" Then
            If tbCC.Text = "" Then
                lblMsgError.Text = "Veuillez renseigner le champs CC"
                lblMsgError.ForeColor = Drawing.Color.Red
            Else
                lblMsgError.Text = ""
                Dim oRibDAO As New CRibDAO
                oRibDAO.modificationCoordonnee(CInt(lblRibID.Text), tbLibelleRib.Text, tbCP.Text, tbCC.Text, tbCB.Text, tbCG.Text, tbNC.Text, tbCle.Text, bParDefaut, tbBic.Text)
                LoadGridView()
                lblMsg.Text = "La Coordonnée bancaire a été modifié avec succés"
                lblMsg.ForeColor = Drawing.Color.Blue
                fsNouveau.Visible = False
            End If
        Else
            If tbCC.Text = "" Then
                lblMsgError.Text = "Veuillez renseigner le champs CC"
                lblMsgError.ForeColor = Drawing.Color.Red
            Else
                lblMsgError.Text = ""
                Dim oRibDAO As New CRibDAO
                oRibDAO.enregistrerNouvelleCoordonnee(tbLibelleRib.Text, tbCP.Text, tbCC.Text, tbCB.Text, tbCG.Text, tbNC.Text, tbCle.Text, bParDefaut, tbBic.Text)
                LoadGridView()
                lblMsg.Text = "La Coordonnée bancaire a été correctement enregistré"
                lblMsg.ForeColor = Drawing.Color.Blue
                fsNouveau.Visible = False
            End If
        End If
    End Sub

    Private Sub btnNouveau_Click(sender As Object, e As System.EventArgs) Handles btnNouveau.Click
        tbLibelleRib.Text = ""
        tbCP.Text = "FR"
        tbCC.Text = ""
        tbCB.Text = ""
        tbCG.Text = ""
        tbNC.Text = ""
        tbCle.Text = ""
        legend.InnerText = "Création d'une Nouvelle Coordonnée Bancaire"
        cbDefault.Checked = False
        fsNouveau.Visible = True
        lblMsg.Text = ""
    End Sub

    Private Sub gvCoordonnéesBancaires_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCoordonnéesBancaires.RowCommand
        Dim iRibID As Integer = CInt(e.CommandArgument)

        If e.CommandName = "SuppressionCoordonnesBancaires" Then
            SupprimerFacture(iRibID)
            LoadGridView()
            lblMsg.Visible = True
            lblMsg.Text = "La Coordonne Bancaire a bien été supprimée"
            lblMsg.ForeColor = Drawing.Color.Blue
        Else
            legend.InnerText = "Modification de la Coordonnée Bancaire"
            fsNouveau.Visible = True
            chargerCoordonneeBancaire(iRibID)
        End If
    End Sub

    Private Sub LoadGridView()
        gvCoordonnéesBancaires.Columns(0).Visible = True
        Dim oRibDAO As New CRibDAO
        Dim ds As DataSet = oRibDAO.GetListeCoordonnéesBancaires()

        gvCoordonnéesBancaires.DataSource = ds
        gvCoordonnéesBancaires.DataBind()
    End Sub

    Private Sub SupprimerFacture(ByVal iRibID As Integer)
        Dim oRibDAO As New CRibDAO
        oRibDAO.DeleteCoordonnéesBancaires(iRibID)
    End Sub

    Private Sub chargerCoordonneeBancaire(ByVal iRibID As Integer)
        Dim oRibDAO As New CRibDAO
        Dim ds As DataSet = oRibDAO.GetListeCoordonnéeBancaireAvecRibID(iRibID)

        lblRibID.Text = "" & iRibID
        tbLibelleRib.Text = ds.Tables(0).Rows(0)("RibLibelle").ToString
        tbCP.Text = "FR"
        tbCC.Text = ds.Tables(0).Rows(0)("Cc").ToString
        tbCB.Text = ds.Tables(0).Rows(0)("Cb").ToString
        tbCG.Text = ds.Tables(0).Rows(0)("Cg").ToString
        tbNC.Text = ds.Tables(0).Rows(0)("Nc").ToString
        tbCle.Text = ds.Tables(0).Rows(0)("Cle").ToString
        tbBic.Text = ds.Tables(0).Rows(0)("Bic").ToString
        cbDefault.Checked = CBool(ds.Tables(0).Rows(0)("RibParDefault"))
       
    End Sub


    Private Sub gvCoordonnéesBancaires_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCoordonnéesBancaires.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim oRibDAO As New CRibDAO

            If Not CStr(e.Row.Cells(0).Text) = "" Then
                Dim iRibID As Integer = CInt(e.Row.Cells(0).Text)

                If oRibDAO.isCoordonnéeParDefaut(iRibID) Then
                    e.Row.BackColor = Drawing.Color.FromArgb(255, 255, 122)
                End If
            End If
            If e.Row.RowIndex = oRibDAO.getCountRows - 1 Then
                gvCoordonnéesBancaires.Columns(0).Visible = False
            End If
        End If

    End Sub
End Class