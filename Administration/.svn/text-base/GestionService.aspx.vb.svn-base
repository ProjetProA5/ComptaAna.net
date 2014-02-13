Imports ComptaAna.business
Imports ComptaAna.net.Droit

Public Class GestionService
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
    End Sub
    ''' <summary>
    ''' Charge le gridView avec la liste des services
    ''' </summary>
    Private Sub LoadGridView()
        Dim oServiceDAO As New CServiceDAO
        Dim dsService As DataSet

        dsService = oServiceDAO.getAllService

        gvService.DataSource = dsService
        gvService.DataBind()
    End Sub

    ''' <summary>
    ''' Insert un nouveau Service
    ''' </summary>
    Private Sub btnAjouterNouveau_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAjouterNouveau.Click
        Dim oService As CService
        Dim oServiceDAO As New CServiceDAO

        Dim sServiceObjectif As String
        Dim sServiceDureeMouvement As String

        sServiceObjectif = Formater.FormatDecimal(tbNouveauServiceObjectif.Text)
        sServiceDureeMouvement = Formater.FormatDecimal(tbNouveauServiceDureeMouvement.Text)

        oService = New CService(tbNouveauLibelle.Text, CDec(sServiceObjectif),
                                CDec(sServiceDureeMouvement), CBool(cbNouveauServiceActif.Checked))
        oServiceDAO.InsertService(oService)
        LoadGridView()
    End Sub

    ''' <summary>
    ''' Enregistre les modifications des services.
    ''' </summary>
    Private Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnregistrer.Click
        Dim oService As CService
        Dim oServiceDAO As New CServiceDAO
        Dim sServiceObjectif As String
        Dim sServiceDureeMouvement As String

        Dim iCount As Integer = gvService.Rows.Count

        For i = 0 To iCount - 1
            Dim tbLibelle As TextBox = CType(gvService.Rows(i).Cells(1).FindControl("tbServiceLibelle"), TextBox)
            Dim tbServiceObjectif As TextBox = CType(gvService.Rows(i).Cells(2).FindControl("tbServiceObjectif"), TextBox)
            Dim tbServiceDureeMouvement As TextBox = CType(gvService.Rows(i).Cells(3).FindControl("tbServiceDureeMouvement"), TextBox)
            Dim cbServiceActif As CheckBox = CType(gvService.Rows(i).Cells(0).FindControl("cbServiceActif"), CheckBox)

            Dim iServiceID As Integer = CType(CType(gvService.Rows(i).Cells(4).FindControl("lblServiceID"), Label).Text, Integer)

            sServiceDureeMouvement = Formater.FormatDecimal(tbServiceDureeMouvement.Text)
            sServiceObjectif = Formater.FormatDecimal(tbServiceObjectif.Text)

            oService = New CService(iServiceID, tbLibelle.Text,
                    CDec(sServiceObjectif), CDec(sServiceDureeMouvement),
                    CBool(cbServiceActif.Checked))
            oServiceDAO.UpdateService(oService)
        Next
    End Sub

    Protected Sub gvService_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "SupprimerService" Then
            Dim oServiceDAO As CServiceDAO = New CServiceDAO
            Dim iDelete As Integer = oServiceDAO.DeleteService(CInt(e.CommandArgument.ToString))
            LoadGridView()
        End If
    End Sub
End Class