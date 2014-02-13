Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class EmployeDroit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
           
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesEmployeEcriture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            chargerDroit()
        End If


    End Sub

    
    Protected Sub btValider_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btValider.Click
        Dim iEmployeID As Integer
        Dim oEmployeModule As New CEmployeModuleDAO
        iEmployeID = CInt(Request.QueryString("id"))
        Dim htDroit As New Hashtable
        htDroit.Add(1, rbDroit1.SelectedIndex)
        htDroit.Add(2, rbDroit2.SelectedIndex)
        htDroit.Add(3, rbDroit3.SelectedIndex)
        htDroit.Add(4, rbDroit4.SelectedIndex)
        htDroit.Add(5, rbDroit5.SelectedIndex)
        htDroit.Add(6, rbDroit6.SelectedIndex)
        htDroit.Add(7, rbDroit7.SelectedIndex)
        htDroit.Add(8, rbDroit8.SelectedIndex)
        htDroit.Add(9, rbDroit9.SelectedIndex)
        htDroit.Add(10, rbDroit10.SelectedIndex)
        htDroit.Add(11, rbDroit11.SelectedIndex)
        htDroit.Add(12, rbDroit12.SelectedIndex)
        htDroit.Add(13, rbDroit13.SelectedIndex)
        htDroit.Add(14, rbDroit14.SelectedIndex)
        htDroit.Add(15, rbDroit15.SelectedIndex)
        htDroit.Add(16, rbDroit16.SelectedIndex)
        htDroit.Add(17, rbDroit17.SelectedIndex)
        htDroit.Add(18, rbDroit18.SelectedIndex)
        htDroit.Add(19, rbDroit19.SelectedIndex)
        htDroit.Add(20, rbDroit20.SelectedIndex)
        ' htDroit.Add(21, rbDroit21.SelectedIndex)
        'ajout droit accèsGraphiques
        htDroit.Add(22, rbDroit22.SelectedIndex)
        htDroit.Add(23, rbDroit23.SelectedIndex)
      

        Try
            oEmployeModule.SaveDroitRB(iEmployeID, htDroit)
            saveOK.visible = True
        Catch ex As Exception
            Response.Redirect("~/Login.aspx?Erreur=401")
        End Try



    End Sub

    Protected Sub btRetourListeEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btRetourListeEmploye.Click
        Response.Redirect("EmployeLister.aspx")
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


    Protected Sub chargerDroit()
        Dim iEmployeID As Integer
        Dim oEmployeModule As New CEmployeModuleDAO
        iEmployeID = CInt(Request.QueryString("id"))
        Dim lDroit As Hashtable
        'Dim iModule As Integer
        lDroit = oEmployeModule.GetDroitsEmploye(iEmployeID)

        rbDroit1.SelectedIndex = 0

        rbDroit2.SelectedIndex = 0

        rbDroit3.SelectedIndex = 0

        rbDroit4.SelectedIndex = 0

        rbDroit5.SelectedIndex = 0

        rbDroit6.SelectedIndex = 0

        rbDroit7.SelectedIndex = 0

        rbDroit8.SelectedIndex = 0

        rbDroit9.SelectedIndex = 0

        rbDroit10.SelectedIndex = 0

        rbDroit11.SelectedIndex = 0

        rbDroit12.SelectedIndex = 0

        rbDroit13.SelectedIndex = 0

        rbDroit14.SelectedIndex = 0

        rbDroit15.SelectedIndex = 0

        rbDroit16.SelectedIndex = 0

        rbDroit17.SelectedIndex = 0
        rbDroit18.SelectedIndex = 0

        rbDroit19.SelectedIndex = 0
        rbDroit20.SelectedIndex = 0
        ' rbDroit21.SelectedIndex = 0
        'ajout droit accèsGraphiques
        rbDroit22.SelectedIndex = 0
        rbDroit23.SelectedIndex = 0


        For Each iModule As String In lDroit.Keys
            If iModule = "1" Then
                rbDroit1.SelectedIndex = 1
            ElseIf iModule = "2" Then
                rbDroit2.SelectedIndex = 1
            ElseIf iModule = "3" Then
                rbDroit3.SelectedIndex = 1
            ElseIf iModule = "4" Then
                rbDroit4.SelectedIndex = 1
            ElseIf iModule = "5" Then
                rbDroit5.SelectedIndex = 1
            ElseIf iModule = "6" Then
                rbDroit6.SelectedIndex = 1
            ElseIf iModule = "7" Then
                rbDroit7.SelectedIndex = 1
            ElseIf iModule = "8" Then
                rbDroit8.SelectedIndex = 1
            ElseIf iModule = "9" Then
                rbDroit9.SelectedIndex = 1
            ElseIf iModule = "10" Then
                rbDroit10.SelectedIndex = 1
            ElseIf iModule = "11" Then
                rbDroit11.SelectedIndex = 1
            ElseIf iModule = "12" Then
                rbDroit12.SelectedIndex = 1
            ElseIf iModule = "13" Then
                rbDroit13.SelectedIndex = 1
            ElseIf iModule = "14" Then
                rbDroit14.SelectedIndex = 1
            ElseIf iModule = "15" Then
                rbDroit15.SelectedIndex = 1
            ElseIf iModule = "16" Then
                rbDroit16.SelectedIndex = 1
            ElseIf iModule = "17" Then
                rbDroit17.SelectedIndex = 1
            ElseIf iModule = "18" Then
                rbDroit18.SelectedIndex = 1
            ElseIf iModule = "19" Then
                rbDroit19.SelectedIndex = 1
            ElseIf iModule = "20" Then
                rbDroit20.SelectedIndex = 1
            'ElseIf iModule = "21" Then
            '    rbDroit21.SelectedIndex = 1
            ElseIf iModule = "22" Then
                rbDroit22.SelectedIndex = 1
            ElseIf iModule = "23" Then
                rbDroit23.SelectedIndex = 1
            End If
        Next


    End Sub
End Class