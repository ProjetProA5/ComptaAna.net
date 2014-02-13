Imports GemBox.Spreadsheet
Imports a6tm.Framework.Mail
Imports a6tm.Framework.Web

Public Class YB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oEmployeDAO As New Business.CEmployeDAO
        Dim oEmploye As Business.CEmploye
        oEmploye = oEmployeDAO.getEmployeFromLoginAndPassword("lamare", "delphps")

        'Dim sTheme As String = CType(ConfigurationManager.GetSection("system.web/pages"), System.Web.Configuration.PagesSection).Theme

        Session("employe") = oEmploye
        Session("Connecter") = True

        Response.Redirect("~/Gestion/Affaire/AffaireProduits.aspx?id=3226")

        'Dim oProduitDAO As New Business.CProduitDAO
        'oProduitDAO.GetAllProduits_Javascript()

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


    Private Sub lbEnvoiMail_Click(sender As Object, e As System.EventArgs) Handles lbEnvoiMail.Click
        Dim oMail As CMail
        oMail = New CMail()
        oMail.SMTPServer = "smtp.axe-environnement.fr"
        oMail.From = "y.bleuzen@axe-environnement.fr"

        oMail.Subject = "Test envoi mail depuis la compta"
        oMail.Body = "Ceci est un test d'envoi de mail depuis la compta"

        oMail.To = "y.bleuzen@axe-environnement.fr"
        oMail.Cc = ""
        oMail.BCc = ""

        oMail.Send()

    End Sub
End Class