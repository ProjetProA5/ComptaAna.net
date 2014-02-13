Imports a6tm.Framework.CConversion
Imports ComptaAna.Business
Imports System.Data
Imports System.Data.SqlClient
Imports ComptaAna.net.Droit

Partial Public Class ListerEmploye
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
           
            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesEmployeEcriture) Then
                    btNouvelEmploye.Visible = False
                    gvEmploye.Columns(5).Visible = False
                    gvEmploye.Columns(6).Visible = True
                End If
                If Not verifDroit(lDroit, eModule.AccesEmployeLecture) Then
                    gvEmploye.Columns(6).Visible = False
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try
            chargerListeEmploye()

        End If
    End Sub
 
    Protected Sub gvEmploye_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "modifierEmploye" Then
            Response.Redirect("EmployeModifier.aspx?id=" & e.CommandArgument.ToString)
        ElseIf e.CommandName = "consulterEmploye" Then
            Response.Redirect("EmployeModifier.aspx?id=" & e.CommandArgument.ToString)
        ElseIf e.CommandName = "SupprimerEmploye" Then
            Dim oEmployeDAO As New CEmployeDAO
            Dim iDelete As Long = oEmployeDAO.SupprimerEmployeAvecId(CInt(e.CommandArgument))

            chargerListeEmploye()

            If Not iDelete = 0 Then
                lMessage.Text = "Suppression effectuée!"
            Else
                lMessage.Text = "Suppression échouée."
            End If
        End If
    End Sub

    Protected Sub gvEmploye_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim isActif As Boolean
            isActif = CBool(DataBinder.Eval(e.Row.DataItem, "EmployeActif"))
            If Not isActif Then
                e.Row.BackColor = Drawing.Color.SkyBlue
            End If
        End If
    End Sub

    ''' <summary>
    ''' Charge la liste des employe
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub chargerListeEmploye()
        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO
        Dim ds As DataSet

        ds = oEmployeDAO.GetAllEmploye()

        gvEmploye.DataSource = ds
        gvEmploye.DataBind()
    End Sub

    Protected Sub btNouvelEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btNouvelEmploye.Click
        Response.Redirect("EmployeModifier.aspx")
    End Sub

    Protected Sub ibRechercheEmploye_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibRechercheEmploye.Click
        Dim oEmployeDAO As CEmployeDAO = New CEmployeDAO
        Dim ds As DataSet

        ds = oEmployeDAO.RechercherEmploye(tbRechercheEmploye.Text)

        gvEmploye.DataSource = ds
        gvEmploye.DataBind()
    End Sub


End Class