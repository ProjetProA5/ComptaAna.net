Imports ComptaAna.Business
Imports Obout.ComboBox
Imports System.Drawing
Imports GemBox.Spreadsheet

Public Class Droit
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' permet de vérifier si un module iModuleID apparait dans la table des droits lDroit
    ''' </summary>
    Public Shared Function verifDroit(ByVal lDroit As Hashtable, ByVal iModuleID As Integer) As Boolean
        'Dim lDroit As New ArrayList
        Dim bool As Boolean = False

        For Each iDroit As String In lDroit.Keys
            If iDroit = iModuleID.ToString Then bool = True
        Next
        'CType(lDroit.Keys, ArrayList).Contains(iModuleID)

        Return bool
    End Function

    ''' <summary>
    ''' Permet de vérifier si une table de modules est incluse dans une table de droits
    ''' </summary>
    Public Shared Function verifListeDroits(ByVal lDroit As Hashtable, ByVal lModules As ArrayList) As Boolean

        Dim bool As Boolean
        bool = True


        For Each imodule As Integer In lModules
            bool = verifDroit(lDroit, imodule)
        Next

        Return bool
    End Function

    ''' <summary>
    ''' Permet de dire si au moins un droit de lDroitAVerif apparait dans la table lDroit
    ''' </summary>
    Public Shared Function existeUnDroit(ByVal lDroit As Hashtable, ByVal lDroitAVerif As ArrayList) As Boolean
        Dim bool As Boolean
        bool = False


        For Each iDroit As Integer In lDroitAVerif
            If verifDroit(lDroit, iDroit) Then bool = True
        Next

        Return bool
    End Function
End Class
