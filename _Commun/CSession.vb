Imports System.Configuration
Imports a6tm.Framework.CConversion
Imports System.Web.HttpContext
Imports ComptaAna.Business

Public Class CSession
    Inherits a6tm.Framework.Web.CConfigurationBase

    Public Shared ReadOnly Property EmployeID() As Integer
        Get
            Return CType(Current.Session("Employe"), CEmploye).EmployeID
        End Get
    End Property

    Public Shared ReadOnly Property EmployeDroit() As Hashtable
        Get
            Return CType(Current.Session("droit"), CEmployeModuleDAO).GetDroitsEmploye(EmployeID)
        End Get
    End Property

    Public Shared Property RechercheAffaire() As RechercheAffaire
        Get
            Return CType(Current.Session("recherche"), RechercheAffaire)
        End Get
        Set(ByVal value As RechercheAffaire)
            Current.Session("recherche") = value
        End Set
    End Property
    Public Shared Property RechercheGraphe() As RechercheGraphPremPge
        Get
            Return CType(Current.Session("PremPassage"), RechercheGraphPremPge)
        End Get
        Set(ByVal value As RechercheGraphPremPge)
            Current.Session("PremPassage") = value
        End Set
    End Property
    Public Shared Property RechercheExportRex() As RechercheExportRex
        Get
            Return CType(Current.Session("ExportRex"), RechercheExportRex)
        End Get
        Set(ByVal value As RechercheExportRex)
            Current.Session("ExportRex") = value
        End Set
    End Property

End Class
