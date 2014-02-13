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


Partial Public Class StatPrimesAugmentations

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
    '''Contrôle cDateDeb.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cDateDeb As Global.OboutInc.Calendar2.Calendar

    '''<summary>
    '''Contrôle tbDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbDateFin As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle cDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cDateFin As Global.OboutInc.Calendar2.Calendar

    '''<summary>
    '''Contrôle lblRestriction.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblRestriction As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle cbbFiltreStatGeneral.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cbbFiltreStatGeneral As Global.Obout.ComboBox.ComboBox

    '''<summary>
    '''Contrôle cbbItemToutEmploye.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cbbItemToutEmploye As Global.Obout.ComboBox.ComboBoxItem

    '''<summary>
    '''Contrôle cbbItemToutService.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cbbItemToutService As Global.Obout.ComboBox.ComboBoxItem

    '''<summary>
    '''Contrôle cbbFiltreStatGalDetail.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents cbbFiltreStatGalDetail As Global.Obout.ComboBox.ComboBox

    '''<summary>
    '''Contrôle btnRechercheStat.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnRechercheStat As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''Contrôle btnExporter.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnExporter As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''Contrôle btnEXportPrimeAugmentation.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnEXportPrimeAugmentation As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Contrôle rfvTbDateDeb.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvTbDateDeb As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle rfvDateFin.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvDateFin As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle rfvCbbFiltre1.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents rfvCbbFiltre1 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Contrôle lblPrime.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblPrime As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Contrôle gvPrime.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents gvPrime As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Contrôle lblTotalPrimes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblTotalPrimes As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbTotalPrimes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbTotalPrimes As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle lblAug.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblAug As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Contrôle gvAugmentation.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents gvAugmentation As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Contrôle lblTxMoyen.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents lblTxMoyen As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Contrôle tbTxMoyen.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents tbTxMoyen As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Contrôle Div1.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents Div1 As Global.System.Web.UI.HtmlControls.HtmlGenericControl
End Class
