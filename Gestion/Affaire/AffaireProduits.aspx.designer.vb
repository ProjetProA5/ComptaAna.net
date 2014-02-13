'------------------------------------------------------------------------------
' <généré automatiquement>
'     Ce code a été généré par un outil.
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </généré automatiquement>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class AffaireProduits

    '''<summary>
    '''Contrôle mMenuAffaireModif.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents mMenuAffaireModif As Global.System.Web.UI.WebControls.Menu

    '''<summary>
    '''Contrôle lblNomAffaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblNomAffaire As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle lblNoProduits.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblNoProduits As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle divRestrictions.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents divRestrictions As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Contrôle ddlEmployes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlEmployes As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle ddlTypes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlTypes As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle ddlBU.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlBU As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle LabelTri.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents LabelTri As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbDateDeb.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbDateDeb As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle cCalendrierDebut.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cCalendrierDebut As Global.OboutInc.Calendar2.Calendar

    '''<summary>
    '''Contrôle tbDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbDateFin As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle cCalendrierFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cCalendrierFin As Global.OboutInc.Calendar2.Calendar

    '''<summary>
    '''Contrôle ibValider.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ibValider As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''Contrôle ibExporter.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ibExporter As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''Contrôle cvDateDebut.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvDateDebut As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle cvDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvDateFin As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle cvPeriode.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvPeriode As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle rfvDateDeb.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvDateDeb As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle rfvDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvDateFin As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle lblMsg.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblMsg As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle gvProduitAffaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents gvProduitAffaire As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Contrôle pPopupAjoutProduit.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pPopupAjoutProduit As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Contrôle divReleve.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents divReleve As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Contrôle lblDate.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblDate As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbDate.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbDate As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle cCalendrier.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cCalendrier As Global.OboutInc.Calendar2.Calendar

    '''<summary>
    '''Contrôle ddlEmployeConcerne.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlEmployeConcerne As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblAffaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblAffaire As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbAffaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbAffaire As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle tbAffaireID.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbAffaireID As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblSite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblSite As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlSite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlSite As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblQualification.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblQualification As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlQualification.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlQualification As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle tdDonneurOrdreLabel.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tdDonneurOrdreLabel As Global.System.Web.UI.HtmlControls.HtmlTableCell

    '''<summary>
    '''Contrôle lblDonneurOrdre.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblDonneurOrdre As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tdDonneurOrdreTextBox.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tdDonneurOrdreTextBox As Global.System.Web.UI.HtmlControls.HtmlTableCell

    '''<summary>
    '''Contrôle tbDonneurOrdre.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbDonneurOrdre As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblService.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblService As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlService.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlService As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblType.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblType As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlType.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlType As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblReference.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblReference As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlProduit.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlProduit As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblLibelle.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblLibelle As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbLibelle.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbLibelle As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblQuantite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblQuantite As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbQuantite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbQuantite As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblPU.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblPU As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbPrixUnit.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbPrixUnit As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblWarningTTC.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblWarningTTC As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle lblTVA.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblTVA As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle ddlTVA.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlTVA As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Contrôle lblTotal.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblTotal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbTotal.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbTotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle bNouveau.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents bNouveau As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Contrôle lblErreur.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblErreur As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle rfvPU.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvPU As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle cvPU.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvPU As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle cvDate.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvDate As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle cvDateMin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvDateMin As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle cvDateMax.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cvDateMax As Global.System.Web.UI.WebControls.CompareValidator

    '''<summary>
    '''Contrôle rfvDate.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvDate As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle ctmDate.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ctmDate As Global.System.Web.UI.WebControls.CustomValidator

    '''<summary>
    '''Contrôle RfvType.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents RfvType As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle rfvProduit.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvProduit As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle rfvQuantite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvQuantite As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle revTbQuantite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents revTbQuantite As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Contrôle revPrixunit.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents revPrixunit As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Contrôle rfvSite.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvSite As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle lbQteErreur.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lbQteErreur As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle lblQteNulle.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblQteNulle As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle lblPrixNull.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblPrixNull As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle btnQuitter.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnQuitter As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Contrôle btnRetourFiche.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnRetourFiche As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Contrôle btnRetourListe.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnRetourListe As Global.System.Web.UI.WebControls.Button
End Class
