Public Class RechercheGraphPremPge
    'varaible de sauvegarde premier passage
    Private bPassage As New Boolean
    Property RecherchecbPassage() As Boolean
        Get
            Return bPassage
        End Get
        Set(ByVal value As Boolean)
            bPassage = value
        End Set
    End Property
    Public Sub SaveRecherche(ByVal bPremPassage As Boolean)
        RecherchecbPassage = bPremPassage

    End Sub

    Public Sub RestaureRecherche(ByRef bPremPassage As Boolean)
        bPremPassage = RecherchecbPassage
    End Sub
End Class
