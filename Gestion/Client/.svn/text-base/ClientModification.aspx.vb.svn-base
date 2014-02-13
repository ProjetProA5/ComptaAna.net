Imports ComptaAna.Business
Imports ComptaAna.net.Droit

Public Class ClientModification
    Inherits System.Web.UI.Page
    Dim iClientID As Integer
    ''' <summary>
    ''' charger la page: client modification
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            'gestion des droits
           

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesClientEcriture) Then
                    bEnregistrerModifClient.Visible = False
                    panelModif.Enabled = False
                    gvClientSite.Columns(2).Visible = False
                End If
                If Not verifDroit(lDroit, eModule.AccesClientLecture) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")
                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try

            If Not IsNothing(Request.QueryString("updateClient")) Then
                lblTitre.Text = "Modification d'un client"
                lblLegende.Text = "Informations du client"

                'la page charge toutes les informations d'un client

                Dim oClientDAO As New CClientDAO
                Dim oClient As New CClient

                iClientID = CInt(Request.QueryString("updateClient"))

                oClient = oClientDAO.GetClientById(iClientID)

                tbClientNom.Text = CStr(oClient.ClientNom)
                cbClientActif.Checked = oClient.ClientActif
                tbClientAdresse.Text = CStr(oClient.ClientAdresse)
                If CInt(oClient.ClientCP) = 0 Then
                    tbClientCP.Text = ""
                Else
                    tbClientCP.Text = CStr(CInt(oClient.ClientCP))
                End If
                tbClientVille.Text = CStr(oClient.ClientVille)
                tbClientMail.Text = CStr(oClient.ClientMail)
                tbFacturationComplement.Text = CStr(oClient.ClientFacturationComplement)
                If oClient.ClientFacturationNumTVA <> "" Then
                    Dim sNumTVA As String = oClient.ClientFacturationNumTVA.ToString()
                    If Len(oClient.ClientFacturationNumTVA) = 17 Then
                        sNumTVA = oClient.ClientFacturationNumTVA.ToString().Replace(" ", "")
                    End If

                    tbFacturationNumTVAFR.Text = sNumTVA.Substring(0, 2)
                    tbFacturationNumTVA2.Text = sNumTVA.Substring(2, 2)
                    tbFacturationNumTVA9.Text = sNumTVA.Substring(4, 9)
                Else
                    tbFacturationNumTVAFR.Text = ""
                    tbFacturationNumTVA2.Text = ""
                    tbFacturationNumTVA9.Text = ""
                End If

                gvClientSite.DataSource = oClientDAO.GetClientSiteByID(iClientID)
                gvClientSite.DataBind()

                gvClientAffaire.DataSource = oClientDAO.GetListeAffaireByClientID(iClientID)
                gvClientAffaire.DataBind()
                bFicheClient.Visible = False
                bListeClients.Visible = True
            Else
                lblTitre.Text = "Nouveau client"
                lblLegende.Text = "Informations du client"
                lbllisteSite.Visible = False
                lblListeAffaire.Visible = False
                gvClientAffaire.Visible = False
                bFicheClient.Visible = False
                bListeClients.Visible = True
            End If

            If Not IsNothing(Request.QueryString("updateSite")) Then
                lblTitre.Text = "Modification d'un site"
                lblLegende.Text = "Informations du site"

                'la page charge toutes les informations d'un client site
                Dim iClientID As Integer
                Dim oClientDAO As New CClientDAO
                Dim oClient As New CClient

                iClientID = CInt(Request.QueryString("updateSite"))

                oClient = oClientDAO.GetClientById(iClientID)

                tbClientNom.Text = CStr(oClient.ClientNom)
                cbClientActif.Checked = oClient.ClientActif
                tbClientAdresse.Text = CStr(oClient.ClientAdresse)
                If CInt(oClient.ClientCP) = 0 Then
                    tbClientCP.Text = ""
                Else
                    tbClientCP.Text = CStr(CInt(oClient.ClientCP))
                End If
                tbClientVille.Text = CStr(oClient.ClientVille)
                tbClientMail.Text = CStr(oClient.ClientMail)
                tbFacturationComplement.Text = CStr(oClient.ClientFacturationComplement)
                If oClient.ClientFacturationNumTVA <> "" Then
                    Dim sNumTVA As String = oClient.ClientFacturationNumTVA.ToString()
                    If Len(oClient.ClientFacturationNumTVA) = 17 Then
                        sNumTVA = oClient.ClientFacturationNumTVA.ToString().Replace(" ", "")
                    End If

                    tbFacturationNumTVAFR.Text = sNumTVA.Substring(0, 2)
                    tbFacturationNumTVA2.Text = sNumTVA.Substring(2, 2)
                    tbFacturationNumTVA9.Text = sNumTVA.Substring(4, 9)
                Else
                    tbFacturationNumTVAFR.Text = ""
                    tbFacturationNumTVA2.Text = ""
                    tbFacturationNumTVA9.Text = ""
                End If

                lbllisteSite.Visible = False
                gvClientSite.Visible = False

                gvClientAffaire.DataSource = oClientDAO.GetListeAffaireByClientID(iClientID)
                gvClientAffaire.DataBind()
                bListeClients.Visible = False
                bFicheClient.Visible = True

            End If

            If Not IsNothing(Request.QueryString("newSite")) Then
                lblTitre.Text = "Nouveau site"
                lblLegende.Text = "Informations du site"
                bListeClients.Visible = False
                bFicheClient.Visible = True
            End If

        End If
        'else: la page est vide pour creer un nouveau client ou un nouveau client site

    End Sub

    ''' <summary>
    ''' control du bouton enregistrer modification information
    ''' </summary>
    Protected Sub BoutonModifier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bEnregistrerModifClient.Click
        If Page.IsPostBack And Page.IsValid Then

            If IsNothing(Request.QueryString("updateClient")) And IsNothing(Request.QueryString("newSite")) And IsNothing(Request.QueryString("updateSite")) Then
                ' -----------------------------------------------
                ' Création d'un nouveau client
                ' -----------------------------------------------
                Dim oClientDAO As CClientDAO = New CClientDAO
                Dim oClient As CClient = New CClient


                If oClientDAO.ClientExiste(tbClientNom.Text) Then
                    ' Si ce nouveau client existe déjà en base, on ne l'enregistre pas pour éviter les doublons
                    lMsg.Text = "Enregistrement impossible, ce client existe déjà."
                Else
                    ' Si ce nouveau client n'existe pas en base, on l'enregistre
                    oClient.ClientActif = cbClientActif.Checked
                    oClient.ClientNom = tbClientNom.Text
                    oClient.ClientAdresse = tbClientAdresse.Text
                    oClient.ClientCP = CInt(tbClientCP.Text)
                    oClient.ClientVille = tbClientVille.Text
                    oClient.ClientMail = tbClientMail.Text
                    oClient.ClientFacturationComplement = tbFacturationComplement.Text
                    oClient.ClientFacturationNumTVA = tbFacturationNumTVAFR.Text & tbFacturationNumTVA2.Text & tbFacturationNumTVA9.Text

                    oClientDAO.InsertClient(oClient)
                    Response.Redirect("ClientListe.aspx")
                    lbllisteSite.Visible = False
                End If



            ElseIf Not IsNothing(Request.QueryString("newSite")) Then
                ' -----------------------------------------------
                ' Création d'un nouveau site client
                ' -----------------------------------------------
                Dim oClientDAO As CClientDAO = New CClientDAO
                Dim oClient As CClient = New CClient

                If oClientDAO.SiteExiste(CInt(Request.QueryString("newSite")), tbClientNom.Text) Then
                    ' Si ce nouveau client existe déjà en base, on ne l'enregistre pas pour éviter les doublons
                    lMsg.Text = "Enregistrement impossible, ce site existe déjà pour ce client."
                Else
                    oClient.ClientActif = cbClientActif.Checked
                    oClient.ClientNom = tbClientNom.Text
                    oClient.ClientAdresse = tbClientAdresse.Text
                    oClient.ClientCP = CInt(tbClientCP.Text)
                    oClient.ClientVille = tbClientVille.Text
                    oClient.ClientMail = tbClientMail.Text
                    oClient.ClientFacturationComplement = tbFacturationComplement.Text
                    oClient.ClientFacturationNumTVA = tbFacturationNumTVAFR.Text & tbFacturationNumTVA2.Text & tbFacturationNumTVA9.Text
                    oClient.ClientReferenceSite = CInt(Request.QueryString("newSite"))

                    oClientDAO.InsertClientSite(oClient)
                    Response.Redirect("ClientModification.aspx?updateClient=" & CInt(Request.QueryString("newSite")))
                    lbllisteSite.Visible = False
                End If

            ElseIf Not IsNothing(Request.QueryString("updateClient")) And IsNothing(Request.QueryString("newSite")) Then
                ' -----------------------------------------------
                ' Modifier les informations d'un client
                ' -----------------------------------------------
                Dim oClientDAO As CClientDAO = New CClientDAO
                Dim oClient As CClient = New CClient
                Dim dsClient As DataSet
                Dim sNomClientEnBase As String
                Dim bEnregistrementPossible As Boolean = True

                ' Récupération du nom de client enregistré en base
                dsClient = oClientDAO.LoadOne(CInt(Request.QueryString("updateClient")))
                If dsClient.Tables(0).Rows.Count > 0 Then
                    sNomClientEnBase = Trim(dsClient.Tables(0).Rows(0)(Client.ClientNom).ToString)
                Else
                    ' Logiquement, on ne devrait jamais rencontrer cette erreur
                    lMsg.Text = "Erreur, l'identifiant de client passé en paramètre n'existe plus "
                    Exit Sub
                End If

                ' Si on modifie le nom du client, alors on teste si ce nouveau nom existe en base
                If sNomClientEnBase <> Trim(tbClientNom.Text) Then
                    If oClientDAO.ClientExiste(tbClientNom.Text) Then
                        ' Si le nouveau nom de client exsite déjà en base, alors on n'enregistre pas
                        bEnregistrementPossible = False
                        lMsg.Text = "Enregistrement impossible, ce client existe déjà."
                    Else
                        ' Si le nouveau nom de client n'existe pas en base, alors on peut l'enregistrer
                        bEnregistrementPossible = True
                    End If
                Else
                    bEnregistrementPossible = True
                End If

                If bEnregistrementPossible Then
                    oClient.ClientID = CInt(Request.QueryString("updateClient"))
                    oClient.ClientActif = cbClientActif.Checked
                    oClient.ClientNom = tbClientNom.Text
                    oClient.ClientAdresse = tbClientAdresse.Text
                    oClient.ClientCP = CInt(tbClientCP.Text)
                    oClient.ClientVille = tbClientVille.Text
                    oClient.ClientMail = tbClientMail.Text
                    oClient.ClientFacturationComplement = tbFacturationComplement.Text
                    oClient.ClientFacturationNumTVA = tbFacturationNumTVAFR.Text & tbFacturationNumTVA2.Text & tbFacturationNumTVA9.Text

                    oClientDAO.UpdateClient(oClient)

                    Response.Redirect("ClientListe.aspx")
                End If


            ElseIf Not IsNothing(Request.QueryString("updateSite")) And IsNothing(Request.QueryString("newSite")) Then
                ' -----------------------------------------------
                ' Modifier les informations d'un site
                ' -----------------------------------------------
                Dim oClientDAO As CClientDAO = New CClientDAO
                Dim oClient As CClient = New CClient
                Dim iClientReferenceSite As Integer = -1
                Dim ds As DataSet
                Dim sNomSiteEnBase As String = ""
                Dim bEnregistrementPossible As Boolean = True

                ds = oClientDAO.LoadOne(CInt(Request.QueryString("updateSite")))

                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)(Client.ClientReferenceSite)) Then
                        iClientReferenceSite = CInt(ds.Tables(0).Rows(0)(Client.ClientReferenceSite))
                    End If
                    sNomSiteEnBase = Trim(ds.Tables(0).Rows(0)(Client.ClientNom).ToString)
                End If

                ' Si on modifie le nom du site, alors on teste si ce nouveau nom existe déjà pour ce client
                If sNomSiteEnBase <> Trim(tbClientNom.Text) Then
                    If oClientDAO.SiteExiste(iClientReferenceSite, tbClientNom.Text) Then
                        ' Si le nouveau nom de site existe déjà pour ce client, alors on n'enregistre pas
                        bEnregistrementPossible = False
                        lMsg.Text = "Enregistrement impossible, ce site existe déjà pour ce client."
                    Else
                        ' Si le nouveau nom de site n'existe pas ce client, alors on peut l'enregistrer
                        bEnregistrementPossible = True
                    End If
                Else
                    bEnregistrementPossible = True
                End If

                If bEnregistrementPossible Then
                    oClient.ClientID = CInt(Request.QueryString("updateSite"))
                    oClient.ClientActif = cbClientActif.Checked
                    oClient.ClientNom = tbClientNom.Text
                    oClient.ClientAdresse = tbClientAdresse.Text
                    oClient.ClientCP = CInt(tbClientCP.Text)
                    oClient.ClientVille = tbClientVille.Text
                    oClient.ClientMail = tbClientMail.Text
                    oClient.ClientFacturationComplement = tbFacturationComplement.Text
                    oClient.ClientFacturationNumTVA = tbFacturationNumTVAFR.Text & tbFacturationNumTVA2.Text & tbFacturationNumTVA9.Text
                    oClient.ClientReferenceSite = iClientReferenceSite

                    oClientDAO.UpdateClient(oClient)

                    Response.Redirect("ClientModification.aspx?updateClient=" & iClientReferenceSite)
                    lbllisteSite.Visible = False
                End If

            End If
        End If
    End Sub

    ''' <summary>
    ''' control du bouton revenir sur la liste des clients
    ''' </summary>
    Protected Sub btListeClient_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bListeClients.Click
        Response.Redirect("ClientListe.aspx")
    End Sub

    ''' <summary>
    ''' control pour modifier client site/ supprimer client site
    ''' </summary>
    Protected Sub gvClientSite_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName = "ModifierClientSite" Then
            Response.Redirect("ClientModification.aspx?updateSite=" & e.CommandArgument.ToString)

        ElseIf e.CommandName = "AjouterClientSite" Then
            Response.Redirect("ClientModification.aspx?newSite=" & CInt(Request.QueryString("updateClient")))

        ElseIf e.CommandName = "SupprimerClientSite" Then
            Dim ds As New DataSet
            Dim oClientDAO As CClientDAO = New CClientDAO
            Dim oClient As CClient = oClientDAO.GetClientById(CInt(e.CommandArgument.ToString))
            Dim iDelete As Integer = oClientDAO.DeleteClient(oClient)

            ds = oClientDAO.GetClientSiteByID(CInt(Request.QueryString("updateClient")))
            gvClientSite.DataSource = ds
            gvClientSite.DataBind()

            If iDelete = 0 Then
                lMsg.Text = "Suppression du site effectuée!"
            ElseIf iDelete = 1 Then
                lMsg.Text = "Suppression échouée, ce client site est lié à au moins une affaire ou à au moins un produit."
            End If

        End If
    End Sub

    Protected Sub gvClientAffaire_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvClientAffaire.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim decBudget As Decimal = CDec(e.Row.Cells(3).Text)
            Dim decBudgetReel As Decimal = CDec(e.Row.Cells(4).Text)
            If decBudgetReel > decBudget Then
                e.Row.Cells(4).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub bFicheClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bFicheClient.Click
        Response.Redirect("ClientModification.aspx?updateClient=" & Request.QueryString("newSite"))
    End Sub
End Class