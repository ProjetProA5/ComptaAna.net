Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class EmployeModifier
    Inherits System.Web.UI.Page
    Dim lDroit As Hashtable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            lDroit = CType(Session("droit"), Hashtable)

            Try
                If Not verifDroit(lDroit, eModule.AccesEmployeEcriture) Then
                    modifEmpoye.Enabled = False
                    btValider.Enabled = False
                End If
                If Not verifDroit(lDroit, eModule.AccesEmployeCoutDroit) Then
                    mOnglets.FindItem("2").Enabled = False
                    mOnglets.FindItem("3").Enabled = False
                End If
                If Not verifDroit(lDroit, eModule.AccesEmployeFormation) Then
                    mOnglets.FindItem("4").Enabled = False
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            ChargerListeQualification()
            ChargerListeProfil()
            ChargerListeService()

            'Si il n'y a pas d'id dans l'url c'est que l'on insert un nouvel utilisateur
            If IsNothing(Request.QueryString("id")) Then
                lbInsererModifierUtilisateur.Text = "Nouvel employé"
                lbService.SelectedIndex = 0
                lbProfil.SelectedIndex = 0
                lbQualification.SelectedIndex = 0
                mOnglets.FindItem("2").Enabled = False
                tbMotDePasse.Visible = True
                lbProfil.Visible = True
                'Sinon c'est que l'on modifie un utilisateur existant
                'on doit donc valoriser les différents champs avec les valeurs présentent dans la bdd
            Else
                lbInsererModifierUtilisateur.Text = "Modification d'un employé"
                Dim iEmployeID As Integer
                Dim oEmployeDAO As New CEmployeDAO
                Dim oEmployeCout As New CEmployeCout
                Dim oEmploye As New CEmploye

                iEmployeID = CInt(Request.QueryString("id"))
                oEmploye = oEmployeDAO.GetEmployeById(iEmployeID)

                tbNom.Text = CStr(oEmploye.EmployeNom)
                tbPrenom.Text = CStr(oEmploye.EmployePrenom)
                tbLogin.Text = CStr(oEmploye.EmployeLogin)
                tbMotDePasse.Text = CStr(oEmploye.EmployePwd)
                cbActif.Checked = oEmploye.EmployeActif
                tbDate.Text = CStr(oEmploye.EmployeDateEntree)
                tbCoutFacturationInterne.Text = CStr(oEmploye.EmployeMntFactInterneHT)
                tbEmail.Text = CStr(oEmploye.EmployeMail)
                lbService.SelectedValue = CStr(oEmploye.ServiceID)
                lbProfil.SelectedValue = CStr(oEmploye.ProfilID)
                lbQualification.SelectedValue = CStr(oEmploye.QualificationID)
                rbStatut.SelectedIndex = CInt(oEmploye.EmployeStatut)
                rbSexe.SelectedIndex = CInt(oEmploye.EmployeSexe)

                If Not verifDroit(lDroit, eModule.AdminGeneral) Then
                    tbLogin.Visible = False
                    tbMotDePasse.Visible = False
                    lbProfil.Visible = False
                End If
                'tbMotDePasse.Visible = oEmploye.ProfilID <> 3 Or CType(Session("Employe"), CEmploye).ProfilID = 3
                'lbProfil.Visible = oEmploye.ProfilID <> 3 Or CType(Session("Employe"), CEmploye).ProfilID = 3
            End If
        End If



    End Sub

    Protected Sub btValider_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btValider.Click
        If Page.IsPostBack Then
            'On va donc inserer un nouvel utilisateur
            If IsNothing(Request.QueryString("id")) Then
                Dim oEmployeDAO As New CEmployeDAO
                Dim oEmploye As New CEmploye(tbNom.Text, tbPrenom.Text, tbLogin.Text, _
                tbMotDePasse.Text, CInt(lbProfil.SelectedValue), CInt(lbService.SelectedValue), _
                CInt(lbQualification.SelectedValue), CDec(Replace(tbCoutFacturationInterne.Text, ".", ",")), _
                CDate(tbDate.Text), cbActif.Checked, tbEmail.Text, rbStatut.SelectedIndex, rbSexe.SelectedIndex)

                oEmployeDAO.InsererEmploye(oEmploye)
            Else
                'On va donc modifier un employe qui éxiste dans la bdd
                Dim iEmployeID As Integer
                Dim oEmploye As CEmploye

                iEmployeID = CInt(Request.QueryString("id"))
                Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO

                oEmploye = oEmployeDAO.GetEmployeById(iEmployeID)

                oEmploye.EmployeNom = tbNom.Text
                oEmploye.EmployePrenom = tbPrenom.Text
                oEmploye.EmployeLogin = tbLogin.Text
                oEmploye.EmployePwd = tbMotDePasse.Text
                oEmploye.EmployeActif = cbActif.Checked
                oEmploye.EmployeDateEntree = CDate(tbDate.Text)
                oEmploye.EmployeMntFactInterneHT = CDec(tbCoutFacturationInterne.Text.ToString.Replace(".", ","))
                oEmploye.EmployeMail = tbEmail.Text
                oEmploye.ServiceID = CInt(lbService.SelectedValue)
                oEmploye.ProfilID = CInt(lbProfil.SelectedValue)
                oEmploye.QualificationID = CInt(lbQualification.SelectedValue)
                oEmploye.EmployeStatut = CInt(rbStatut.SelectedIndex)
                oEmploye.EmployeSexe = CInt(rbSexe.SelectedIndex)
                oEmployeDAO.ModifierEmploye(oEmploye)
            End If
            lEnregistrementOk.Visible = True
            ' Response.Redirect("EmployeLister.aspx")
        End If
    End Sub

    Protected Sub btRetourListeEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetourListeEmploye.Click
        Response.Redirect("EmployeLister.aspx")
    End Sub

    ''' <summary>
    ''' Permet de charger la liste des qualifications
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub ChargerListeQualification()
        Dim oQualificationDAO As New CQualificationDAO
        Dim dsQualif As DataSet

        dsQualif = oQualificationDAO.GetAllQualification()

        lbQualification.DataSource = dsQualif
        lbQualification.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de charger la liste des profils
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChargerListeProfil()
        Dim oProfilDAO As New CProfilDAO
        Dim dsProfil As DataSet

        If verifDroit(lDroit, eModule.AdminGeneral) Then
            'en fonction de droit AdminGen
            dsProfil = oProfilDAO.LoadAll("", "ProfilLibelle")
        Else
            dsProfil = oProfilDAO.LoadAll("ProfilID<>3", "ProfilLibelle")
        End If

        lbProfil.DataSource = dsProfil
        lbProfil.DataBind()
    End Sub

    ''' <summary>
    ''' Permet de charger la liste des services
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChargerListeService()
        Dim oServiceDAO As New CServiceDAO
        Dim dsProfil As DataSet

        dsProfil = oServiceDAO.GetAllService()

        lbService.DataSource = dsProfil
        lbService.DataBind()
    End Sub

    Private Sub mOnglets_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles mOnglets.MenuItemClick
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
End Class