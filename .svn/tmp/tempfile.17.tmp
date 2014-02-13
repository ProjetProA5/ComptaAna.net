Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Initialisation de la variable "Connecter" à False, et déconnecte l'employe lorsqu'il clique sur le bouton déconnection
        Session("Connecter") = False
        lFailureText.Visible = False

        If CConfiguration.ModePreprod Then
            lblPreprod.Visible = True
            lblPreprod.Text = "Attention, vous êtes sur un site de test. Toutes les modifications que vous effectuez sur ce site ne seront pas visibles sur le site de production."
        End If

        If Not Page.IsPostBack And Not IsNothing(Request.QueryString("Erreur")) Then
            If CStr(Request.QueryString("Erreur")) = "403" Then
                LogErreur(CType(Session("Employe"), CEmploye).EmployeNom, "Erreur 403")

                lFailureText.Text = "Vous n'avez pas assez de droit."
                lFailureText.Visible = True

            ElseIf CStr(Request.QueryString("Erreur")) = "401" Then
                LogErreur(CType(Session("Employe"), CEmploye).EmployeNom, "Session expirée")

                lFailureText.Text = "Votre session a expiré, veuillez vous reconnecter."
                lFailureText.Visible = True
            End If
        End If

        If Not Page.IsPostBack And Not IsNothing(Request.QueryString("aspxerrorpath")) Then
            LogErreur(CType(Session("Employe"), CEmploye).EmployeNom, "Erreur 500")

            lFailureText.Text = "Erreur 500, contactez YB ou DL en cas d'urgence."
            lFailureText.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' Connection sur le click du bouton
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles LoginButton.Click
        Dim oEmployeDAO As New CEmployeDAO
        Dim oEmployeModule As New CEmployeModuleDAO
        Dim oEmploye As CEmploye
        Dim lDroit As Hashtable

        If (Page.IsValid) Then     
            If (oEmployeDAO.isGoodLogin(tbLoginUser.Text, tbPassword.Text)) Then
                oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword(tbLoginUser.Text, tbPassword.Text)
                lDroit = oEmployeModule.GetDroitsEmploye(oEmploye.EmployeID)
                Session("employe") = oEmploye
                Session("droit") = lDroit
                Session("Connecter") = True

                'page de redirection apres la connection
                If oEmploye.ProfilID = 1 Then
                    Response.Redirect("~/Activite/ReleveActivite.aspx")
                Else
                    Response.Redirect("~/Gestion/Affaire/AffaireLister.aspx")
                End If


                'Try
                '    If verifDroit(lDroit, 11) Then
                '        Response.Redirect("~/Activite/ReleveActivite.aspx")
                '    End If
                'Catch ex As NullReferenceException
                '    Response.Redirect("~/Login.aspx?Erreur=401")
                'End Try
            Else
                lFailureText.Visible = True
            End If
        End If
    End Sub
End Class