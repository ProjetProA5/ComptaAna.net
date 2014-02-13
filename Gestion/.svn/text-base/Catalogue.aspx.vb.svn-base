Imports ComptaAna.Business
Imports ComptaAna.net.Droit

''' <summary>
''' Classe permettant de gerer l'ecoute de la page Catalogue.aspx
''' </summary>
''' <remarks></remarks>
Public Class Catalogue
    Inherits System.Web.UI.Page
    Dim chargerListe As Boolean

    ''' <summary>
    ''' Au chargement de la page
    ''' </summary>
    ''' <param name="sender"> un objet </param>
    ''' <param name="e"> un evenement </param>
    ''' <remarks>Si l'id = 0, on affiche les champs vide. Si on a selectionne un id dans le treeview et que l'id est initialise, on charge le produit correspondant</remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Try
            '    If CType(Session("Employe"), CEmploye).ProfilID = 1 Then
            '        Response.Redirect("~/Login.aspx?Erreur=403")
            '    End If
            'Catch ex As NullReferenceException
            '    Response.Redirect("~/Login.aspx?Erreur=401")
            'End Try

            Dim lDroit As Hashtable = CType(Session("droit"), Hashtable)
            Try
                If Not verifDroit(lDroit, eModule.AccesCatalogue) Then
                    Response.Redirect("~/Login.aspx?Erreur=403")

                End If
            Catch ex As NullReferenceException
                Response.Redirect("~/Login.aspx?Erreur=401")
            End Try


            LoadTreeView()
            chargerListe = True
        End If
        If Not rbRefac.Items(0).Selected And Not rbRefac.Items(1).Selected Then
            rbRefac.Items(0).Selected = True
        End If
        If chargerListe Then
            ChargerListeTva()
            ChargerListeType()
        End If
    End Sub

    ''' <summary>
    ''' Charge le treeview dans la page
    ''' </summary>
    Private Sub LoadTreeView()
        Dim oProduitDAO As New CProduitDAO
        Dim dsProduit As DataSet

        Dim oTypeProduitDAO As New CTypeProduitDAO
        Dim dsTypeProduit As DataSet

        Dim tnProduit As Obout.Ajax.UI.TreeView.Node
        Dim tnTypeProduit As Obout.Ajax.UI.TreeView.Node

        dsTypeProduit = oTypeProduitDAO.GetTypeProduitRelieProduit

        If Not IsCallback Then
            tvCatalogue.Nodes.Clear()
        End If

        'branche parent
        For i = 0 To dsTypeProduit.Tables(0).Rows.Count - 1
            tnTypeProduit = New Obout.Ajax.UI.TreeView.Node(CStr(dsTypeProduit.Tables(0).Rows(i)("TypeProduitLibelle")))

            dsProduit = oProduitDAO.GetAllProduitByTypeProduitID(CInt(dsTypeProduit.Tables(0).Rows(i)("TypeProduitID")))

            'branche enfant
            For j = 0 To dsProduit.Tables(0).Rows.Count - 1
                tnProduit = New Obout.Ajax.UI.TreeView.Node()
                tnProduit.Value = CStr(dsProduit.Tables(0).Rows(j)("ProduitID"))
                tnProduit.Text = CStr(dsProduit.Tables(0).Rows(j)("ProduitRef"))

                tnTypeProduit.ChildNodes.Add(tnProduit)
                tnTypeProduit.SelectMode = Obout.Ajax.UI.TreeView.NodeSelectMode.Expand
            Next
            tvCatalogue.Nodes.Add(tnTypeProduit)
        Next
    End Sub

    ''' <summary>
    ''' Charge un produit lorque qu'il est selectionne dans le treeView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tvCatalogue_SelectedTreeNodeChanged(ByVal sender As Object, ByVal e As Obout.Ajax.UI.TreeView.NodeEventArgs) Handles tvCatalogue.SelectedTreeNodeChanged
        lblMsgSupp.Text = ""
        LoadProduit(CInt(tvCatalogue.SelectedNode.Value))

        fsDetailsProduit.Visible = True
        pDetailsProduit.Enabled = False
        listeType.Enabled = False
        listeTva.Enabled = False
        btnModifier.Visible = True
        btnSupprimer.Visible = True
        btnEnregistrer.Visible = False
        hCreationNewProduit.Visible = False
    End Sub

    ''' <summary>
    ''' Charge un produit selectionne dans le treeview dans la partie gauche de la page
    ''' </summary>
    ''' <param name="idProduit"> l'id du produit </param>
    Private Sub LoadProduit(ByVal idProduit As Integer)
        lblMsgSupp.Text = ""
        If idProduit <> 0 Then
            'MAJ des textbox et radios
            Dim oProduitDAO As New CProduitDAO
            Dim oProduit As CProduit

            oProduit = oProduitDAO.GetProduit(idProduit)
            tbReference.Text = oProduit.ProduitRef
            tbLibelle.Text = oProduit.ProduitLibelle

            listeType.SelectedValue = oProduit.TypeProduitID.ToString()
            listeTva.SelectedValue = oProduit.TvaID.ToString

            If CBool(oProduit.ProduitRefac) Then
                rbRefac.Items(1).Selected = True
                rbRefac.Items(0).Selected = False
            Else
                rbRefac.Items(0).Selected = True
                rbRefac.Items(1).Selected = False
            End If
            'MAJ des caracteristiques du produit
            affichageCaracProduit(oProduit.TypeProduitID)
        End If

    End Sub

    ''' <summary>
    ''' Ecouteur du bouton btnSupprimer
    ''' </summary>
    ''' <param name="sender"> un objet </param>
    ''' <param name="e"> un evenement </param>
    ''' <remarks>Supprime dans la BDD le produit correspondant a celui dont l'id est passe en parametre</remarks>
    Protected Sub btnSupprimer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSupprimer.Click
        Try
            Dim oProduitDAO As CProduitDAO = New CProduitDAO
            Dim iDelete As Integer = oProduitDAO.SupprimerProduit(CInt(tvCatalogue.SelectedNode.Value))

            If iDelete = 0 Then
                lblMsgSupp.Text = "Suppression effectuée!"

                fsDetailsProduit.Visible = False

            ElseIf iDelete = 1 Then
                lblMsgSupp.Text = "Suppression echouée, ce type de produit est lie à au moins un produitAffaire. "
            End If

            tbReference.Text = ""
            tbLibelle.Text = ""
            rbRefac.Items(0).Selected = True
            rbRefac.Items(1).Selected = False
            LoadTreeView()
            ChargerListeTva()
            ChargerListeType()
        Catch ex As NullReferenceException
            lblMsgSupp.Text = "Vous n'avez pas encore sélectionné de produit."
        End Try
    End Sub

    ''' <summary>
    ''' Ecouteur du bouton btnEnregistrer
    ''' </summary>
    ''' <remarks>Modifie dans la BDD le produit correspondant a celui dont l'id est passe en parametre</remarks>
    Protected Sub btnEnregistrer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnregistrer.Click
        Dim oProduitDAO As CProduitDAO = New CProduitDAO
        Dim oProduit As CProduit = New CProduit
        Dim resultat As Integer
        Dim checkRefac As Boolean
        'determine si ProduitRefac est a true ou a false en fonction du bouton radio coche
        'si ht (items(0).selected) => true
        'si ttc (items(1).selected) => false
        If rbRefac.Items(0).Selected Then
            checkRefac = False
        Else
            checkRefac = True
        End If

        oProduit.TypeProduitID = CInt(listeType.SelectedValue)
        oProduit.TvaID = CInt(listeTva.SelectedValue)
        oProduit.ProduitLibelle = tbLibelle.Text
        oProduit.ProduitRef = tbReference.Text
        oProduit.ProduitRefac = checkRefac

        'si un noeud du treeview n'a pas ete selectionne
        If tvCatalogue.SelectedNode Is Nothing Then
            'on l'ajoute dans la bdd
            resultat = oProduitDAO.InsererProduit(oProduit)
            'si un noeud est selectione on verifie que le produit existe
        ElseIf oProduitDAO.ProduitExiste(CInt(tvCatalogue.SelectedNode.Value)) Then
            'et on le met a jour
            oProduit.ProduitID = CInt(tvCatalogue.SelectedNode.Value)
            resultat = oProduitDAO.ModifierProduit(oProduit)
        End If
        If resultat = 1 Then
            lblMsgSupp.Text = "Le produit a bien été enregistré."

            tbReference.Text = ""
            tbLibelle.Text = ""
            rbRefac.Items(0).Selected = True
            rbRefac.Items(1).Selected = False
            LoadTreeView()
            ChargerListeTva()
            ChargerListeType()

            pDetailsProduit.Enabled = False
            listeTva.Enabled = False
            listeType.Enabled = False
            btnEnregistrer.Visible = False
            btnModifier.Visible = True
            btnSupprimer.Visible = True

            ' à cause du treeview
            fsDetailsProduit.Visible = False
        Else
            lblMsgSupp.Text = "Erreur : Le produit n'a pas pu être enregistré."

            tbReference.Text = ""
            tbLibelle.Text = ""
            rbRefac.Items(0).Selected = True
            rbRefac.Items(1).Selected = False
            LoadTreeView()
            ChargerListeTva()
            ChargerListeType()
        End If
    End Sub

    ''' <summary>
    ''' Ecouteur du bouton btnNouveau
    ''' </summary>
    ''' <param name="sender"> un objet </param>
    ''' <param name="e"> un evenement </param>
    ''' <remarks>Recharge la page du catalogue, remet les champs a zero pour creer un nouveau produit</remarks>
    Protected Sub btnNouveau_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNouveau.Click
        'Response.Redirect("Catalogue.aspx")
        LoadTreeView()
        tbLibelle.Text = ""
        tbReference.Text = ""

        listeType.SelectedIndex = -1
        listeTva.SelectedIndex = -1

        rbRefac.SelectedIndex = 0
        tpAffaire.ImageUrl = ""
        tpBudgetAffaire.ImageUrl = ""
        tpCAAxe.ImageUrl = ""
        tpCAEmploye.ImageUrl = ""
        tpFacInter.ImageUrl = ""
        tpFactQualif.ImageUrl = ""
        tpJournee.ImageUrl = ""

        lblMsgSupp.Text = ""
        fsDetailsProduit.Visible = True
        pDetailsProduit.Enabled = True
        listeType.Enabled = True
        listeTva.Enabled = True
        btnModifier.Visible = False
        btnEnregistrer.Visible = True
        btnSupprimer.Visible = False
        hCreationNewProduit.Visible = True
    End Sub

    ''' <summary>
    ''' Ecouteur de la liste listeType, la liste des types de produits
    ''' </summary>
    ''' <remarks>Met a jour l'encadre concernant les caracteristiques du produit dans la page Catalogue.aspx</remarks>
    Protected Sub listeType_Click(ByVal sender As Object, ByVal e As EventArgs) Handles listeType.SelectedIndexChanged
        If tvCatalogue.SelectedNode Is Nothing Then
            chargerListe = False

            tbReference.Text = listeType.SelectedText
            tbLibelle.Text = listeType.SelectedText

            Dim oTypeProduitDAO As CTypeProduitDAO = New CTypeProduitDAO
            Dim index As Integer
            index = CInt(listeType.SelectedIndex) + 1
            'MAJ des caracteristiques du produit
            affichageCaracProduit(index)
        End If
    End Sub

    ''' <summary>
    ''' Met a jour l'affichage des caracteristiques du produit
    ''' </summary>
    ''' <param name="idTypeProduit"> l'id d'un type de produit </param>
    Protected Sub affichageCaracProduit(ByVal idTypeProduit As Integer)
        Dim bTypeProduitAffaire As Boolean
        Dim bTypeProduitJournee As Boolean
        Dim bTypeProduitCA As Boolean
        Dim bTypeProduitBudgetAffaire As Boolean
        Dim bTypeProduitCAAxe As Boolean
        Dim bTypeProduitFacInter As Boolean
        Dim bTypeProduitFacQualif As Boolean

        Dim oTypeProduit As New CTypeProduit
        Dim oTypeProduitDAO As New CTypeProduitDAO
        oTypeProduit = oTypeProduitDAO.GetCaracTypeProduit(idTypeProduit)

        bTypeProduitAffaire = CBool(oTypeProduit.TypeProduitAffaire)
        bTypeProduitBudgetAffaire = CBool(oTypeProduit.TypeProduitBudgetAffaire)
        bTypeProduitJournee = CBool(oTypeProduit.TypeProduitJournee)
        bTypeProduitCA = CBool(oTypeProduit.TypeProduitCA)
        bTypeProduitCAAxe = CBool(oTypeProduit.TypeProduitCAAxe)
        bTypeProduitFacInter = CBool(oTypeProduit.TypeProduitFactInterne)
        bTypeProduitFacQualif = CBool(oTypeProduit.TypeProduitFactQualif)

        If bTypeProduitAffaire Then
            tpAffaire.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpAffaire.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitBudgetAffaire Then
            tpBudgetAffaire.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpBudgetAffaire.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitJournee Then
            tpJournee.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpJournee.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitCA Then
            tpCAEmploye.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpCAEmploye.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitCAAxe Then
            tpCAAxe.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpCAAxe.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitFacQualif Then
            tpFactQualif.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpFactQualif.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If

        If bTypeProduitFacInter Then
            tpFacInter.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_tick.png"
        Else
            tpFacInter.ImageUrl = "../App_Themes/ComptaAna/Design/Icon_cross.png"
        End If
    End Sub

    ''' <summary>
    ''' Charge la liste de tva
    ''' </summary>
    Protected Sub ChargerListeTva()
        listeTva.Items.Clear()
        Dim oTvaDAO As New CTVADAO
        Dim dsTva As DataSet

        dsTva = oTvaDAO.GetAllTva

        listeTva.EmptyText = "Choisir une TVA"
        listeTva.DataSource = dsTva
        listeTva.DataBind()
    End Sub

    ''' <summary>
    ''' Charge la liste de type
    ''' </summary>
    Protected Sub ChargerListeType()
        listeType.Items.Clear()
        Dim oTypeProduitDAO As New CTypeProduitDAO
        Dim dsTypeProduit As DataSet
        dsTypeProduit = oTypeProduitDAO.GetAllTypeProduit
        listeType.EmptyText = "Choisir un type"
        listeType.DataSource = dsTypeProduit
        listeType.DataBind()
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As System.EventArgs) Handles btnModifier.Click
        pDetailsProduit.Enabled = True
        lblMsgSupp.Text = ""
        listeType.Enabled = True
        listeTva.Enabled = True
        btnModifier.Visible = False
        btnEnregistrer.Visible = True
    End Sub
End Class