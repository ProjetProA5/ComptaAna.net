Imports GemBox.Spreadsheet

Public Class MG
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oEmployeDAO As New Business.CEmployeDAO
        Dim oEmploye As Business.CEmploye
        Dim oEmployeModule As New Business.CEmployeModuleDAO
        Dim lDroit As Hashtable

        oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("lamare", "delphps")
        'oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("bleuzen", "yoann")
        'oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("gruel", "annie")
        'oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("piau", "matthieu")
        'oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("kraeutler", "laurent")
        'oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("favris", "laurence")

        lDroit = oEmployeModule.GetDroitsEmploye(oEmploye.EmployeID)

        Session("employe") = oEmploye
        Session("droit") = lDroit
        Session("Connecter") = True

        'Response.Redirect("http://compta.local/Statistiques/StatTypeProduit.aspx")
        'Response.Redirect("http://compta.local/Statistiques/StatEmployeService.aspx")
        'Response.Redirect("http://compta.local/Gestion/Client/ClientListe.aspx")
        'Response.Redirect("http://compta.local/Gestion/Catalogue.aspx")
        'Response.Redirect("http://compta.local/Gestion/Affaire/AffaireProduits.aspx?id=3226")
        'Response.Redirect("http://compta.local/Gestion/Affaire/AffaireLister.aspx")
        ' Response.Redirect("http://compta.local/Gestion/Affaire/AffaireFacturation.aspx?id=3614")
        'Response.Redirect("http://compta.local/Gestion/Affaire/AffaireFactureDetailsV2.aspx?id=118")
        Response.Redirect("http://compta.local/Gestion/Employe/EmployeCout.aspx?id=53")


        ' A passer ce soir (18/02/2013)
        'Dim oAffaireDAO As New Business.CAffaireDAO
        'Dim dDebut As Date = Now
        'oAffaireDAO.UpdateProduitsDepassementAll()
        'Response.Write("Mise à jour OK ; durée = " & DateDiff(DateInterval.Second, Now, dDebut) & " s")
        ' ---------------------------------------------------


        'Dim oProduitDAO As New Business.CProduitDAO
        'oProduitDAO.GetAllProduits_Javascript()




        'Response.Write("Dépassement (affaireid=3138) = " & oAffaireDAO.MontantDepassement(3138))

        '        Dim sw As IO.StreamWriter
        '       sw = IO.File.AppendText("D:\titi\aaa.txt")
        '      sw.WriteLine("aaa")
        '     sw.Close()

    End Sub

    Public Sub ExportExcelAllEmploye()
        ' Détermination des constantes de lignes, colonnes 
        Dim iLigneDebut As Integer = 5
        Dim iColPrenom As Integer = 0
        Dim iColNom As Integer = 1
        Dim iColQualificationLibelle As Integer = 2

        ' Chargement des données
        Dim ds As DataSet = (New Business.CEmployeDAO).GetAllEmploye()

        ' Initialisation de l'objet Excel
        SpreadsheetInfo.SetLicense("E9RB-D9QG-XTV6-HFOG")
        Dim ef As ExcelFile = New ExcelFile
        Dim ws As ExcelWorksheet = ef.Worksheets.Add("DataSheet")

        ' En-tête du fichier
        ws.Cells(0, 0).Value = "Extraction de tous les employés"
        ws.Cells(2, 0).Value = "Date extraction : " & Now

        ' Fusion des 3 premières cellules de la première ligne, écriture en gras
        ws.Cells.GetSubrangeAbsolute(0, 0, 0, 2).Merged = True
        ws.Cells.GetSubrangeAbsolute(0, 0, 0, 2).Style.Font.Weight = ExcelFont.BoldWeight

        ' Ecriture du titre des colonnes + bordures
        ws.Cells(iLigneDebut, iColPrenom).Value = "Prénom"
        ws.Cells(iLigneDebut, iColNom).Value = "Nom"
        ws.Cells(iLigneDebut, iColQualificationLibelle).Value = "Qualification"
        ws.Cells(iLigneDebut, iColPrenom).SetBorders(CType(MultipleBorders.Horizontal + MultipleBorders.Vertical, GemBox.Spreadsheet.MultipleBorders), Drawing.Color.Black, LineStyle.Thin)
        ws.Cells(iLigneDebut, iColNom).SetBorders(CType(MultipleBorders.Horizontal + MultipleBorders.Vertical, GemBox.Spreadsheet.MultipleBorders), Drawing.Color.Black, LineStyle.Thin)
        ws.Cells(iLigneDebut, iColQualificationLibelle).SetBorders(CType(MultipleBorders.Horizontal + MultipleBorders.Vertical, GemBox.Spreadsheet.MultipleBorders), Drawing.Color.Black, LineStyle.Thin)

        ' Parcours du dataset et remplissage du fichier Excel
        With ds.Tables(0)
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                ws.Cells(i + iLigneDebut + 1, iColPrenom).Value = .Rows(i)("EmployePrenom")
                ws.Cells(i + iLigneDebut + 1, iColNom).Value = .Rows(i)("EmployeNom")
                ws.Cells(i + iLigneDebut + 1, iColQualificationLibelle).Value = .Rows(i)("QualificationLibelle")
            Next

        End With

        ' Taille des colonnes
        ws.Columns(iColPrenom).Width = 15 * 256
        ws.Columns(iColNom).Width = 15 * 256
        ws.Columns(iColQualificationLibelle).Width = 20 * 256


        Response.Clear()

        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=" + "TexteClient" & Guid.NewGuid.ToString & ".xls")

        ef.SaveXls(Response.OutputStream)
    End Sub


End Class