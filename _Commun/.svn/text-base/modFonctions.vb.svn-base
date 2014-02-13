Imports ComptaAna.Business

Module modFonctions

    ''' <summary>
    ''' Ajoute un "0" devant un chiffre de manière à afficher ce chiffre sur 2 caractères
    ''' </summary>
    ''' <param name="iChiffre"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AjouterZero(iChiffre As Integer) As String
        If iChiffre >= 0 And iChiffre < 10 Then
            Return "0" & iChiffre.ToString()
        Else
            Return iChiffre.ToString()
        End If
    End Function

    ''' <summary>
    ''' Transforme la date du jour en string, sous format américain (utilisé dans les noms de fichier par exemple)
    ''' Ex : 31/01/2013 --> 20130131
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DateCouranteToString() As String
        Return Year(Now) & AjouterZero(Month(Now)) & AjouterZero(Day(Now))
    End Function

    ''' <summary>
    ''' Transforme la date du jour et l'heure en string, sous format américain (utilisé dans les noms de fichier par exemple)
    ''' Ex : 31/01/2013 à 10H14m34s --> 20130131_10h14m34s
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DateEtHeureCouranteToString() As String
        Return Year(Now) & AjouterZero(Month(Now)) & AjouterZero(Day(Now)) & "_" & AjouterZero(Hour(Now)) & "h" & AjouterZero(Minute(Now)) & "m" & AjouterZero(Second(Now)) & "s"
    End Function

    Public Sub LogErreur(sEmploye As String, sDescriptionErreur As String)

        Dim oXml As New System.Xml.XmlDocument
        Dim oRoot As System.Xml.XmlElement
        Dim oJour As System.Xml.XmlNode
        Dim oErreur As System.Xml.XmlNode
        Dim oAtt As System.Xml.XmlAttribute
        Dim sLogFile As String

        sLogFile = CConfiguration.ErreurXmlPath

        Try
            oXml.Load(sLogFile)
        Catch ex As IO.FileNotFoundException
            Exit Sub
        End Try

        oRoot = oXml.DocumentElement

        oJour = oRoot.Item("J" & DateCouranteToString())
        If oJour Is Nothing Then
            oJour = oXml.CreateNode(System.Xml.XmlNodeType.Element, "J" & DateCouranteToString(), "")
            oRoot.AppendChild(oJour)
        End If

        oErreur = oJour.Item("Erreur")

        oErreur = oXml.CreateNode(System.Xml.XmlNodeType.Element, "Erreur", "")
        'Ajout d'attributs
        oAtt = oXml.CreateAttribute("Heure")
        oAtt.Value = Date.Now.ToLongTimeString
        oErreur.Attributes.Append(oAtt)

        oAtt = oXml.CreateAttribute("EmployeID")
        oAtt.Value = sEmploye.ToString
        oErreur.Attributes.Append(oAtt)

        oErreur.InnerText = sDescriptionErreur
        oJour.AppendChild(oErreur)


        oXml.Save(sLogFile)

    End Sub

    ''' <summary>
    ''' Renvoit le montant TTC à partir du montant PU HT et de la TVA
    ''' </summary>
    ''' <param name="dMontantHT"></param>
    ''' <param name="dTvaMontant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMontantTTC(dMontantHT As Double, dTvaMontant As Double) As Double
        Dim dNouveauMontant As Double

        Try
            dNouveauMontant = (1 + dTvaMontant / 100) * dMontantHT
        Catch e As Exception
            LogErreur(CType(HttpContext.Current.Session("Employe"), CEmploye).EmployeNom, e.Message)
        End Try

        Return dNouveauMontant
    End Function

    ''' <summary>
    ''' Renvoit le montant HT à partir d'un montant TTC et de la TVA
    ''' </summary>
    ''' <param name="dMontantTTC"></param>
    ''' <param name="dTvaMontant"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMontantHT(dMontantTTC As Double, dTvaMontant As Double) As Double
        Dim dNouveauMontant As Double

        Try
            dNouveauMontant = dMontantTTC / (1 + dTvaMontant / 100)
        Catch e As Exception
            LogErreur(CType(HttpContext.Current.Session("Employe"), CEmploye).EmployeNom, e.Message)
        End Try

        Return dNouveauMontant
    End Function
    ''' <summary>
    ''' donne l'url d'une facture
    ''' </summary>
    ''' <param name="iAffaireID"></param>
    ''' <param name="iFactureID"></param>
    ''' <param name="iDossier">1 -> dossier Factures | 2 -> dossier Avoir</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetURLFacture(ByVal iAffaireID As Integer, ByVal iFactureID As Integer) As String
        Dim sURL As String = ""
        Dim sFileName As String = ""

        Dim sPathAffaire As String = ""
        Dim oFactuDAO As New CFacturationAffaireDAO
        Dim oFactu As CFacturationAffaire = oFactuDAO.GetFacturationAffaireByFactureID(iFactureID)


                sPathAffaire = CConfiguration.UploadPhysicalPath & "\Factures\" & iAffaireID
         


        If System.IO.Directory.Exists(sPathAffaire) Then

            For Each sPath As String In System.IO.Directory.GetFiles(sPathAffaire)
                If IO.Path.GetFileNameWithoutExtension(sPath) = iFactureID.ToString() Then
                    sFileName = System.IO.Path.GetFileName(sPath)
                  
                            sURL = CConfiguration.UploadVirtualPath & "/Factures/" & iAffaireID & "/" & sFileName

                    Exit For
                End If
            Next
        End If

        Return sURL

    End Function
End Module
