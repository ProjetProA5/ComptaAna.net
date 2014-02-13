Public Class RechercheAffaire

    Private cbbAffaireTri As Integer = 0
    Private rbAffaireFiltre As Integer = 4
    Private cbbChargerAffaire As Integer
    Private sChargeAffaireID As String
    Private tbDateDeb As String
    Private tbDateFin As String


    Property RechercheAffaireTri() As Integer
        Get
            Return cbbAffaireTri
        End Get
        Set(ByVal Value As Integer)
            cbbAffaireTri = Value
        End Set
    End Property

    Property RechercheAffaireFiltre() As Integer
        Get
            Return rbAffaireFiltre
        End Get
        Set(ByVal Value As Integer)
            rbAffaireFiltre = Value
        End Set
    End Property

    Property RechercheAffaireCharge() As Integer
        Get
            Return cbbChargerAffaire
        End Get
        Set(ByVal Value As Integer)
            cbbChargerAffaire = Value
        End Set
    End Property
    Property RechercheAffaireChargeID() As String
        Get
            Return sChargeAffaireID
        End Get
        Set(ByVal Value As String)
            sChargeAffaireID = Value
        End Set
    End Property

    Property RechercheAffaireDeb() As String
        Get
            Return tbDateDeb
        End Get
        Set(ByVal Value As String)
            tbDateDeb = Value
        End Set
    End Property


    Property RechercheAffaireFin() As String
        Get
            Return tbDateFin
        End Get
        Set(ByVal Value As String)
            tbDateFin = Value
        End Set
    End Property
    Public Sub New()

    End Sub
    Public Sub SaveRecherche(ByVal cbbAffaireTri As Integer, ByVal rbAffaireFiltre As Integer, ByVal cbbChargerAffaire As Integer, ByVal sChargeAffaireID As String, ByVal tbDateDeb As String, ByVal tbDateFin As String)
        RechercheAffaireTri = cbbAffaireTri
        RechercheAffaireFiltre = rbAffaireFiltre
        RechercheAffaireCharge = cbbChargerAffaire
        RechercheAffaireChargeID = sChargeAffaireID
        RechercheAffaireDeb = tbDateDeb
        RechercheAffaireFin = tbDateFin
    End Sub

    Public Sub RestaureRecherche(ByRef cbbTri As Obout.ComboBox.ComboBox, ByRef rbFiltre As RadioButtonList, ByRef cbbCharge As Obout.ComboBox.ComboBox, ByRef tbDeb As TextBox, ByRef tbFin As TextBox)
        cbbTri.SelectedIndex = RechercheAffaireTri
        rbFiltre.SelectedIndex = RechercheAffaireFiltre
        cbbCharge.SelectedIndex = RechercheAffaireCharge
        cbbCharge.SelectedValue = RechercheAffaireChargeID
        tbDeb.Text = RechercheAffaireDeb
        tbFin.Text = RechercheAffaireFin
    End Sub

End Class
