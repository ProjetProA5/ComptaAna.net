Imports System.Configuration
Imports a6tm.Framework.CConversion

Public Class CConfiguration
    Inherits a6tm.Framework.Web.CConfigurationBase

    Public Shared ReadOnly Property ModePreprod() As Boolean
        Get
            Return CLng(ConfigurationManager.AppSettings("ModePreprod")) = 1
        End Get
    End Property

    Public Shared ReadOnly Property NouvelleVersion() As Boolean
        Get
            Return CLng(ConfigurationManager.AppSettings("NouvelleVersion")) = 1
        End Get
    End Property

    Public Shared ReadOnly Property ErreurXmlPath() As String
        Get
            Return ConfigurationManager.AppSettings("ErreurXmlPath")
        End Get    
    End Property

    Public Shared ReadOnly Property UploadPhysicalPath() As String
        Get
            Return ConfigurationManager.AppSettings("UploadPhysicalPath")
        End Get
    End Property

    Public Shared ReadOnly Property UploadVirtualPath() As String
        Get
            Return ConfigurationManager.AppSettings("UploadVirtualPath")
        End Get
    End Property
End Class
