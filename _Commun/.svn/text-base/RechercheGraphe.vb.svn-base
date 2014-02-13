Imports Telerik.Charting
' Classe pour la sauvegarde des graphes.
Public Class RechercheGraphe
   
    'variable de sauvegarde de date
    Private tbDateDeb As String
    Private tbDateFin As String
    'variable de sauvegarde du graphe taux de factu
    Private cbbTauxFactu As Integer
    Private cbbTauxFactuValue As String
    ' variable de sauvegarde du graphe CA par Service
    Private cbCAServiceCheck As New List(Of Boolean)
    Private cbCAServiceValue As New List(Of String)
    Private cbCAServicePourcentage As Boolean
    Private bCAServiceTypeGraphe As Boolean
    ' variable de sauvegarde du graphe CA par Service
    Private cbCAEmployeCheck As New List(Of Boolean)
    Private cbCAEmployeValue As New List(Of String)
    Private cbCAEmployePourcentage As Boolean
    'variable de sauvegarde du graphe Masse salariale par Service
    Private cbbMasseSalariale As Integer
    Private cbbMasseSalarialeValue As String
    Private bMasseSalarialeTypeGraphe As Boolean
    Private cbMasseSalarialePourcentage As Boolean
    'variable de sauvegarde du graphe EBE par Service
    Private bEBEServiceTypeGraphe As Boolean
    Private cbEBEServicePourcentage As Boolean
    'variable de sauvegarde du graphe Outils par Service
    Private bOutilsServiceTypeGraphe As Boolean
    Private cbOutilsServicePourcentage As Boolean
    ' variable de sauvegarde du graphe Montant Formation
    Private cbMontantFormationServicePourcentage As Boolean
    'Variable pour les graphes n-1
    Private cbNmoinUn As Boolean
   
    Property RecherchecbNmoinUn() As Boolean
        Get
            Return cbNmoinUn
        End Get
        Set(ByVal value As Boolean)
            cbNmoinUn = value
        End Set
    End Property
    Property RechercheMontantFormationServicePourcentage() As Boolean
        Get
            Return cbMontantFormationServicePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbMontantFormationServicePourcentage = value
        End Set
    End Property
    Property RechercheOutilsServiceTypeGraphe() As Boolean
        Get
            Return bOutilsServiceTypeGraphe
        End Get
        Set(ByVal value As Boolean)
            bOutilsServiceTypeGraphe = value
        End Set
    End Property

    Property RechercheOutilsServicePourcentage() As Boolean
        Get
            Return cbOutilsServicePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbOutilsServicePourcentage = value
        End Set
    End Property
    Property RechercheEBEServiceTypeGraphe() As Boolean
        Get
            Return bEBEServiceTypeGraphe
        End Get
        Set(ByVal value As Boolean)
            bEBEServiceTypeGraphe = value
        End Set
    End Property

    Property RechercheEBEServicePourcentage() As Boolean
        Get
            Return cbEBEServicePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbEBEServicePourcentage = value
        End Set
    End Property
    Property RecherchecbbMasseSalariale() As Integer
        Get
            Return cbbMasseSalariale
        End Get
        Set(ByVal Value As Integer)
            cbbMasseSalariale = Value
        End Set
    End Property
    Property RecherchecbbMasseSalarialeValue() As String
        Get
            Return cbbMasseSalarialeValue
        End Get
        Set(ByVal Value As String)
            cbbMasseSalarialeValue = Value
        End Set
    End Property
    Property RechercheMasseSalarialeTypeGraphe() As Boolean
        Get
            Return bMasseSalarialeTypeGraphe
        End Get
        Set(ByVal value As Boolean)
            bMasseSalarialeTypeGraphe = value
        End Set
    End Property

    Property RechercheMasseSalarialePourcentage() As Boolean
        Get
            Return cbMasseSalarialePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbMasseSalarialePourcentage = value
        End Set
    End Property

    Property RechercheCAEmployePourcentage() As Boolean
        Get
            Return cbCAEmployePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbCAEmployePourcentage = value
        End Set
    End Property

    Property RechercheCAEmployeValue() As List(Of String)
        Get
            Return cbCAEmployeValue
        End Get
        Set(ByVal Value As List(Of String))
            Dim i As Integer = 0
            If Not cbCAEmployeValue.Count = 0 Then
                cbCAEmployeValue.Clear()
            End If
            For i = 0 To Value.Count - 1
                cbCAEmployeValue.Add(Value(i))
            Next
        End Set
    End Property
    Property RechercheCAEmployeCheck() As List(Of Boolean)
        Get
            Return cbCAEmployeCheck
        End Get
        Set(ByVal Value As List(Of Boolean))
            Dim i As Integer = 0
            If Not cbCAEmployeCheck.Count = 0 Then
                cbCAEmployeCheck.Clear()
            End If

            For i = 0 To Value.Count - 1
                cbCAEmployeCheck.Add(Value(i))
            Next
        End Set
    End Property
    Property RechercheCAServiceTypeGraphe() As Boolean
        Get
            Return bCAServiceTypeGraphe
        End Get
        Set(ByVal value As Boolean)
            bCAServiceTypeGraphe = value
        End Set
    End Property

    Property RechercheCAServicePourcentage() As Boolean
        Get
            Return cbCAServicePourcentage
        End Get
        Set(ByVal value As Boolean)
            cbCAServicePourcentage = value
        End Set
    End Property

    Property RechercheCAServiceValue() As List(Of String)
        Get
            Return cbCAServiceValue
        End Get
        Set(ByVal Value As List(Of String))
            Dim i As Integer = 0
            If Not cbCAServiceValue.Count = 0 Then
                cbCAServiceValue.Clear()
            End If
            For i = 0 To Value.Count - 1
                cbCAServiceValue.Add(Value(i))
            Next
        End Set
    End Property
    Property RechercheCAServiceCheck() As List(Of Boolean)
        Get
            Return cbCAServiceCheck
        End Get
        Set(ByVal Value As List(Of Boolean))
            Dim i As Integer = 0
            If Not cbCAServiceCheck.Count = 0 Then
                cbCAServiceCheck.Clear()
            End If

            For i = 0 To Value.Count - 1
                cbCAServiceCheck.Add(Value(i))
            Next
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
    Property RecherchecbbTauxFactu() As Integer
        Get
            Return cbbTauxFactu
        End Get
        Set(ByVal Value As Integer)
            cbbTauxFactu = Value
        End Set
    End Property
    Property RecherchecbbTauxFactuValue() As String
        Get
            Return cbbTauxFactuValue
        End Get
        Set(ByVal Value As String)
            cbbTauxFactuValue = Value
        End Set
    End Property
    Public Sub SaveRecherche(ByVal cbNmoinUn As Boolean, ByVal cbMontantFormationServicePourcentag As Boolean, ByVal cbOutilsServiceTypeGraph As Boolean, ByVal cbOutilsServicePourcentag As Boolean, ByVal cbEBEServiceTypeGraph As Boolean, ByVal cbEBEServicePourcentag As Boolean, ByVal cbMasseSalarialeTypeGraph As Boolean, ByVal cbMasseSalarialePourcentag As Boolean, ByVal cbbMasseSalarialeValue As String, ByVal cbbMasseSalarialeIndex As Integer, ByVal cbCAEmployePourcentag As Boolean, ByVal cbCAEmployeValue As List(Of String), ByVal cbCAEmployeCheck As List(Of Boolean), ByVal bCAServiceTypeGraph As Boolean, ByVal cbCAServicePourcentag As Boolean, ByVal cbCAServiceValue As List(Of String), ByVal cbCAServiceCheck As List(Of Boolean), ByVal cbbTauxFactIndex As Integer, ByVal cbbTauxFactValue As String, ByVal tbDateDeb As String, ByVal tbDateFin As String)
        RechercheAffaireDeb = tbDateDeb
        RechercheAffaireFin = tbDateFin
        'Graphe taux factu
        RecherchecbbTauxFactu = cbbTauxFactIndex
        RecherchecbbTauxFactuValue = cbbTauxFactValue
        'Graphe CAService
        RechercheCAServiceCheck = cbCAServiceCheck
        RechercheCAServiceValue = cbCAServiceValue
        RechercheCAServicePourcentage = cbCAServicePourcentag
        bCAServiceTypeGraphe = bCAServiceTypeGraph
        'Graphe CAEmploye
        RechercheCAEmployeCheck = cbCAEmployeCheck
        RechercheCAEmployeValue = cbCAEmployeValue
        RechercheCAEmployePourcentage = cbCAEmployePourcentag
        'Graphe MasseSalariale
        RecherchecbbMasseSalariale = cbbMasseSalarialeIndex
        RecherchecbbMasseSalarialeValue = cbbMasseSalarialeValue
        RechercheMasseSalarialePourcentage = cbMasseSalarialePourcentag
        bMasseSalarialeTypeGraphe = cbMasseSalarialeTypeGraph
        'Graphe EBEService
        RechercheEBEServicePourcentage = cbEBEServicePourcentag
        RechercheEBEServiceTypeGraphe = cbEBEServiceTypeGraph
        'Graphe Outils Par Service
        RechercheOutilsServicePourcentage = cbOutilsServicePourcentag
        RechercheOutilsServiceTypeGraphe = cbOutilsServiceTypeGraph
        'Graphe Montant Formation
        RechercheMontantFormationServicePourcentage = cbMontantFormationServicePourcentag
        'graphe n-1
        RecherchecbNmoinUn = cbNmoinUn

    End Sub

    Public Sub RestaureRecherche(ByRef cbn1 As CheckBox, ByRef cbPourcentageFormation As CheckBox, ByRef rcOutilsGraphe As Telerik.Web.UI.RadChart, ByRef rcOutilsGrapheNMoinsUn As Telerik.Web.UI.RadChart, ByRef cbOutils As CheckBox, ByRef rcRexGraphe As Telerik.Web.UI.RadChart, ByRef rcRexGrapheNMoinsUn As Telerik.Web.UI.RadChart, ByRef cbEBEService As CheckBox, ByRef rcPanelGraphe3 As Telerik.Web.UI.RadChart, ByRef rcMasseSalarialeNMoinsUn As Telerik.Web.UI.RadChart, ByRef cbMasseSalariale As CheckBox, ByRef cbbMasseSalariale As Obout.ComboBox.ComboBox, ByRef cbCAEmploye As CheckBox, ByRef cblCAEmploye As CheckBoxList, ByRef rcPanelGraphe As Telerik.Web.UI.RadChart, ByRef rcCAServiceNMoinsUn As Telerik.Web.UI.RadChart, ByRef cbCAService As CheckBox, ByRef cblCAService As CheckBoxList, ByRef cbbTauxFact As Obout.ComboBox.ComboBox, ByRef tbDeb As TextBox, ByRef tbFin As TextBox)
        tbDeb.Text = RechercheAffaireDeb
        tbFin.Text = RechercheAffaireFin
        'Graphe tauxFactu
        cbbTauxFact.SelectedIndex = RecherchecbbTauxFactu
        cbbTauxFact.SelectedValue = RecherchecbbTauxFactuValue
        'Graphe CAService
        For i = 0 To cblCAService.Items.Count - 1
            'en cour de modification
            cblCAService.Items(i).Selected = cbCAServiceCheck.Item(i)
            cblCAService.Items(i).Value = cbCAServiceValue.Item(i)
        Next
        If bCAServiceTypeGraphe Then
            rcPanelGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcPanelGraphe.Chart.Series.GetSeries(1).Type = ChartSeriesType.Bar
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(1).Type = ChartSeriesType.Bar

        Else
            rcPanelGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcPanelGraphe.Chart.Series.GetSeries(1).Type = ChartSeriesType.Pie
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcCAServiceNMoinsUn.Chart.Series.GetSeries(1).Type = ChartSeriesType.Pie
        End If
        cbCAService.Checked = RechercheCAServicePourcentage

        'Graphe CAEmploye
        For i = 0 To cblCAEmploye.Items.Count - 1
            'en cour de modification
            cblCAEmploye.Items(i).Selected = cbCAEmployeCheck.Item(i)
            cblCAEmploye.Items(i).Value = cbCAEmployeValue.Item(i)
        Next
        cbCAEmploye.Checked = RechercheCAEmployePourcentage
        'Graphe MassseSalariale
        cbbMasseSalariale.SelectedIndex = RecherchecbbMasseSalariale
        cbbMasseSalariale.SelectedValue = RecherchecbbMasseSalarialeValue
        cbMasseSalariale.Checked = RechercheMasseSalarialePourcentage
        If bMasseSalarialeTypeGraphe Then
            rcPanelGraphe3.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcMasseSalarialeNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
        Else
            rcPanelGraphe3.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcMasseSalarialeNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
        End If
        'Graphe EBEService
        cbEBEService.Checked = RechercheEBEServicePourcentage

        rcRexGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
        rcRexGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
       
        'Graphe OutilsService
        cbOutils.Checked = RechercheOutilsServicePourcentage
        If RechercheOutilsServiceTypeGraphe Then
            rcOutilsGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
            rcOutilsGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Bar
        Else
            rcOutilsGraphe.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
            rcOutilsGrapheNMoinsUn.Chart.Series.GetSeries(0).Type = ChartSeriesType.Pie
        End If
        'Graphe Montant Formation
        cbPourcentageFormation.Checked = RechercheMontantFormationServicePourcentage
        'Graphe n-1
        cbn1.Checked = RecherchecbNmoinUn
    End Sub

End Class
