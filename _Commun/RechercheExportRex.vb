Public Class RechercheExportRex
    'varaible de sauvegarde premier passage
    Private DateDeb As New Date
    Private DateFin As New Date
    Property RechercheDateDeb() As Date
        Get
            Return DateDeb
        End Get
        Set(ByVal value As Date)
            DateDeb = value
        End Set
    End Property
    Property RechercheDateFin() As Date
        Get
            Return DateFin
        End Get
        Set(ByVal value As Date)
            DateFin = value
        End Set
    End Property
    Public Sub SaveRecherche(ByVal DateDeb As Date, ByVal DateFin As Date)
        RechercheDateDeb = DateDeb
        RechercheDateFin = DateFin
    End Sub

    Public Sub RestaureRecherche(ByVal DateDeb As System.Web.UI.WebControls.TextBox, ByVal DateFin As System.Web.UI.WebControls.TextBox)
        DateDeb.Text = CStr(RechercheDateDeb)
        DateFin.Text = CStr(RechercheDateFin)
    End Sub
End Class
