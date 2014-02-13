<%@ Page Title="StatEmployeServiceV2" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteMaster.Master" CodeBehind="StatEmployeServiceV2.aspx.vb" Inherits="ComptaAna.net.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 19px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<fieldset>
<table  cellspacing="0" cellpadding="0">
<asp:TextBox runat="server" ID="tbDateDeb"  ReadOnly="true" Visible="false" />
<asp:TextBox runat="server" ID="tbDateFin"  ReadOnly="true" Visible="false" />
<tr>
<td valign="bottom">

<table class="ExportREXGAUCHEBIS">
        <tr>
            <td class="tdInvisible" >
                Calcul de centre de profit veille
            </td>
        </tr>
        <tr>
        <td  class="tdInvisible"> '</td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRDEB">
                
                <asp:TextBox runat="server" ID="tbPourcentageDuCAHT"  Text="%age du CAHT Prévu" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
               
                <asp:TextBox runat="server" ID="tbCAAnnuelPrevi"  Text="CA ANNUEL PREVI" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRGROSSEBORDURE">
                 <asp:TextBox runat="server" ID="tbCAGlobal"  Text="CA Global" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
                <asp:TextBox runat="server" ID="tbImpotEtTaxesPourcent" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRGROSSEBORDURE">
               
                <asp:TextBox runat="server" ID="tbSalaireChargerBU"  Text="Salaires chargés/BU" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
                <asp:TextBox runat="server" ID="tbFraisStructurePoucent" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRFIN">
                
                <asp:TextBox runat="server" ID="tbEBE"  Text="EBE" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
    </table>


</td>
<td valign="bottom">


    <table class="ExportREXSTJ">
        <tr>
            <td  >
                STJ
            </td>
             <td >
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif STJ
            </td>
            <td class="ExportREXTD">
                Réalisé STJ
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD" align="center">
                
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserPourcentageSTJ"  ReadOnly="true" CssClass="tbSTJ" /> 
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifAnnuelleSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
              
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifCAGlobalSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureSTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureSTJ"  ReadOnly="true" CssClass="tbSTJ" />
                </td>
        </tr>
         <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBESTJ"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserEBESTJ"  ReadOnly="true" CssClass="tbSTJ" />
                </td>
        </tr>
    </table>



</td>
<td valign="bottom">


<table class="ExportREXICPE">
        <tr >
            <td>
                ICPE
            </td>
            <td >
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif ICPE
            </td>
            <td class="ExportREXTD">
                Réalisé ICPE
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifAnnuelleICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserCAGlobalICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifSalaireChargerICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureICPE" ReadOnly="true" CssClass="tbICPE" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBEICPE" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserEBEICPE" ReadOnly="true" CssClass="tbICPE" />
               </td>
        </tr>
    </table>


</td>
<td valign="bottom">


<table class="ExportREXMDP">
        <tr class="ExportREXtd">
            <td >
                MDP
            </td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif MDP
            </td>
            <td class="ExportREXTD">
                Réalisé MDP
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
              
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifAnnuelleMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifCAGlobalMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifSalaireChargerMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserSalaireChargerMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureMDP"  ReadOnly="true" CssClass="tbMDP" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifEBEMDP"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBEMDP"  ReadOnly="true" CssClass="tbMDP" />
               </td>
        </tr>
    </table>


</td>
<td valign="bottom">


<table class="ExportREXPORZO">
        <tr>
            <td >
                PORZO
            </td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif PORZO
            </td>
            <td class="ExportREXTD">
                Réalisé PORZO
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentagePORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifAnnuellePORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructurePORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructurePORZO"  ReadOnly="true" CssClass="tbPORZO" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifEBEPORZO"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserEBEPORZO"  ReadOnly="true" CssClass="tbPORZO" />
                </td>
        </tr>
    </table>



</td>
<td valign="bottom">
    <table class="ExportREXTOTAL">
        <tr>
            <td>
                TOTAL
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Obj AXE
            </td>
            <td class="ExportREXTD">
                Réa AXE
            </td>
            <td class="ExportREXTD">
                Réa AXE N-1
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1PourcentageTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifAnnuelleTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserCAGlobalTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1CAGlobalTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1ImpotEtTaxesTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifSalaireChargerTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1SalaireChargerTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1FraisDeStructureTOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBETOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBETOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1EBETOTAL"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
        </tr>
   
    </table>

</td>
<td valign="bottom">

<table class="ExportREXCONSOLIDE">
        <tr>
            <td>
                CONSOLIDE
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Obj Consolidé
            </td>
            <td class="ExportREXTD">
                Réa Conso
            </td>
            <td class="ExportREXTD">
                Réa Conso N-1
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserPourcentageCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1PourcentageCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifAnnuelleCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1CAGlobalCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />

            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1ImpotEtTaxesCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1SalaireChargerCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1FraisDeStructureCONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBECONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBECONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserN1EBECONSOLIDE"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
        </tr>
   
    </table>


</td>
</tr>
</table >
<br />
<br />
Paramétrage
<table class="ExportTable">
<tr>
    <td class="ExportREXTD">MOIS</td>
</tr>
<tr>
    <td class="ExportREXTD">
    <asp:TextBox runat="server" ID="tbNbMois"  ReadOnly="true" CssClass="tbClassique" />
    </td>
</tr>
<tr>
    <td class="ExportREXTD">Impôts et taxes (en %)</td>
</tr>
<tr>
    <td class="ExportREXTD">
    <asp:TextBox runat="server" ID="tbImpotEtTaxes"  ReadOnly="true" CssClass="tbClassique" />
    </td>
</tr>
<tr>
    <td class="ExportREXTD">Frais de structure (en %)</td>
</tr>
<tr>
    <td class="ExportREXTD">
    <asp:TextBox runat="server" ID="tbFraisStruct"  ReadOnly="true" CssClass="tbClassique" />
    </td>
</tr>

</table>



<br />
<br />
Ratios
<table cellspacing="0" cellpadding="0">
    <tr>
        <td  valign="bottom">

        <table class="ExportREXGAUCHEBORDURE">
        <tr>
        <td class="ExportREXTDPAIRBIS">

        <asp:TextBox runat="server" ID="tbMasseSalarialeCA"  Text="Masse salariale/CA HT <42%" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIRBIS">
       
         <asp:TextBox runat="server" ID="tbEBECA"  Text="EBE / CA >25 à 30%" ReadOnly="true" CssClass="tbImpairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIRBIS">
        
         <asp:TextBox runat="server" ID="tbOutilsAnnee"  Text="OUTILS année en cours" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIRBIS">
        
         <asp:TextBox runat="server" ID="tbCAHTHorsOutilsAnnee"  Text="CAHT hors outils année en cours" ReadOnly="true" CssClass="tbImpairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIRBIS">
        
         <asp:TextBox runat="server" ID="tbOutilsAnneeN1"  Text="OUTILS année n-1" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIRBIS">
        
         <asp:TextBox runat="server" ID="tbCAHTHorsOutilsAnneeN1"  Text="CAHT hors outils année n-1" ReadOnly="true" CssClass="tbImpairBis" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIRBIS">
        
         <asp:TextBox runat="server" ID="tbEBE2012"  Text="EBE 2012" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        </table>


        </td>
   
        <td  valign="bottom">

         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCASTJ"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCASTJ"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECASTJ"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECASTJ"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeSTJ"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeSTJ"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1STJ"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1STJ"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserEBEN1STJ"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        </table>

        </td>
    
        <td  valign="bottom">
         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAICPE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAICPE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAICPE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAICPE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeICPE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeICPE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1ICPE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
      
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1ICPE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBEN1ICPE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        </table>

        </td>
    
        <td  valign="bottom">
         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAMDP"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAMDP"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAMDP"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAMDP"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeMDP"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
      
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeMDP"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1MDP"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1MDP"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBEN1MDP"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        </table>

        </td>
   
        <td  valign="bottom">

         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAPORZO"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAPORZO"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAPORZO"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAPORZO"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
       
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneePORZO"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneePORZO"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1PORZO"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1PORZO"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBEN1PORZO"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        </table>

        </td>
    
        <td  valign="bottom">
        <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCATOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCATOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1MasseSalarialeCATOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbObjectifEBECATOTAL"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBECATOTAL"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1EBECATOTAL"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeTOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1OutilsAnneeTOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeTOTAL"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1CAHTHorsOutilsAnneeTOTAL"  ReadOnly="true" CssClass="tbImpair" /></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR"></td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1TOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR"></td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1TOTAL"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR"></td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBEN1TOTAL"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
        </table>
        </td>
    
        <td  valign="bottom">
             <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCACONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCACONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1MasseSalarialeCACONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbObjectifEBECACONSOLIDE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBECACONSOLIDE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserN1EBECACONSOLIDE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeCONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeCONSOLIDE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR"></td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeN1CONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR"></td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeN1CONSOLIDE"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR"></td>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBEN1CONSOLIDE"  ReadOnly="true" CssClass="tbPair" />
        
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
        </table>
        </td>
    </tr>
</table>
<br />
<br />

<table  cellspacing="0" cellpadding="0">

<tr>
<td valign="bottom">

<table class="ExportREXGAUCHEBIS">
        <tr>
            <td class="tdInvisible" >
                Calcul de centre de profit veille
            </td>
        </tr>
        <tr>
        <td class="tdInvisible"> '</td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRDEB">
                
                <asp:TextBox runat="server" ID="tbPourcentageCAHTN1"  Text="%age du CAHT Prévu" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
               
                <asp:TextBox runat="server" ID="tbCAAnnuelPreviN1"  Text="CA ANNUEL PREVI" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRGROSSEBORDURE">
                 <asp:TextBox runat="server" ID="tbCAGlobalN1"  Text="CA Global" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
                <asp:TextBox runat="server" ID="tbImpotEtTaxesN1" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRGROSSEBORDURE">
               
                <asp:TextBox runat="server" ID="tbSalaireChargerBUN1"  Text="Salaires chargés/BU" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDIMPAIRGROSSEBORDURE">
                <asp:TextBox runat="server" ID="tbFraisDeStructureN1" ReadOnly="true" CssClass="tbImpairBis" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTDPAIRFIN">
                
                <asp:TextBox runat="server" ID="tbEBEN1"  Text="EBE" ReadOnly="true" CssClass="tbPairBis" />
            </td>
        </tr>
    </table>


</td>
<td valign="bottom">


    <table class="ExportREXSTJ">
        <tr>
            <td  >
                STJ
            </td>
             <td >
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif STJ
            </td>
            <td class="ExportREXTD">
                Réalisé STJ
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserPourcentageSTJN1"  ReadOnly="true" CssClass="tbSTJ" /> 
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifAnnuelleSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
              
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifCAGlobalSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureSTJN1"  ReadOnly="true" CssClass="tbSTJ" />
                </td>
        </tr>
         <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBESTJN1"  ReadOnly="true" CssClass="tbSTJ" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserEBESTJN1"  ReadOnly="true" CssClass="tbSTJ" />
                </td>
        </tr>
    </table>



</td>
<td valign="bottom">


<table class="ExportREXICPE">
        <tr >
            <td>
                ICPE
            </td>
            <td >
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif ICPE
            </td>
            <td class="ExportREXTD">
                Réalisé ICPE
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifAnnuelleICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserCAGlobalICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifSalaireChargerICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureICPEN1" ReadOnly="true" CssClass="tbICPE" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBEICPEN1" ReadOnly="true" CssClass="tbICPE" />
            </td>
            <td class="ExportREXTD">
             <asp:TextBox runat="server" ID="tbRealiserEBEICPEN1" ReadOnly="true" CssClass="tbICPE" />
               </td>
        </tr>
    </table>


</td>
<td valign="bottom">


<table class="ExportREXMDP">
        <tr class="ExportREXtd">
            <td >
                MDP
            </td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif MDP
            </td>
            <td class="ExportREXTD">
                Réalisé MDP
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
              
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifAnnuelleMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifCAGlobalMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifSalaireChargerMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserSalaireChargerMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureMDPN1"  ReadOnly="true" CssClass="tbMDP" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbObjectifEBEMDPN1"  ReadOnly="true" CssClass="tbMDP" />
            </td>
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBEMDPN1"  ReadOnly="true" CssClass="tbMDP" />
               </td>
        </tr>
    </table>


</td>
<td valign="bottom">


<table class="ExportREXPORZO">
        <tr>
            <td >
                PORZO
            </td>
            <td >
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Objectif PORZO
            </td>
            <td class="ExportREXTD">
                Réalisé PORZO
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentagePORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifAnnuellePORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructurePORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserFraisDeStructurePORZON1"  ReadOnly="true" CssClass="tbPORZO" />
                </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifEBEPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
            </td>
            <td class="ExportREXTD">
            <asp:TextBox runat="server" ID="tbRealiserEBEPORZON1"  ReadOnly="true" CssClass="tbPORZO" />
                </td>
        </tr>
    </table>



</td>
<td valign="bottom">
    <table class="ExportREXTOTAL">
        <tr>
            <td>
                TOTAL
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Obj AXE
            </td>
            <td class="ExportREXTD">
                Réa AXE
            </td>
            <td class="ExportREXTD">
                Réa AXE N-2
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserPourcentageTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifAnnuelleTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserCAGlobalTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifSalaireChargerTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureTOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBETOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBETOTALN1"  ReadOnly="true" CssClass="tbTOTAL" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
   
    </table>

</td>
<td valign="bottom">

<table class="ExportREXCONSOLIDE">
        <tr>
            <td>
                CONSOLIDE
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                Obj Consolidé
            </td>
            <td class="ExportREXTD">
                Réa Conso
            </td>
            <td class="ExportREXTD">
                Réa Conso N-2
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserPourcentageCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifAnnuelleCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifCAGlobalCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserCAGlobalCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifImpotEtTaxesCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserImpotEtTaxesCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifSalaireChargerCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserSalaireChargerCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
       <tr>
            <td class="ExportREXTD">
                 <asp:TextBox runat="server" ID="tbObjectifFraisDeStructureCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbRealiserFraisDeStructureCONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
        <tr>
            <td class="ExportREXTD">
                <asp:TextBox runat="server" ID="tbObjectifEBECONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td >
            <td class="ExportREXTD">
               <asp:TextBox runat="server" ID="tbRealiserEBECONSOLIDEN1"  ReadOnly="true" CssClass="tbCONSOLIDE" />
            </td>
            <td class="ExportREXTD">
                
            </td>
        </tr>
   
    </table>


</td>
</tr>
</table >


<br />
<br />
Ratios
<table cellspacing="0" cellpadding="0">
    <tr>
        <td  valign="bottom">

        <table class="ExportREXGAUCHEBORDURE">
        <tr>
        <td class="ExportREXTDPAIRBIS">

        <asp:TextBox runat="server" ID="b31"  Text="Masse salariale/CA HT <42%" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIRBIS">
       
         <asp:TextBox runat="server" ID="b32"  Text="EBE / CA >25 à 30%" ReadOnly="true" CssClass="tbImpairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIRBIS">
        
         <asp:TextBox runat="server" ID="b33"  Text="OUTILS année en cours" ReadOnly="true" CssClass="tbPairBis" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIRBIS">
        
         <asp:TextBox runat="server" ID="b34"  Text="CAHT hors outils année en cours" ReadOnly="true" CssClass="tbImpairBis" />
        </td>
        </tr>
        </table>


        </td>
   
        <td  valign="bottom">

         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCASTJN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCASTJN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECASTJN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECASTJN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeSTJN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeSTJN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
   
        </table>

        </td>
    
        <td  valign="bottom">
         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAICPEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAICPEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAICPEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAICPEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeICPEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeICPEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
    
        </table>

        </td>
    
        <td  valign="bottom">
         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
        <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAMDPN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAMDPN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAMDPN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAMDPN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeMDPN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
      
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeMDPN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
   
        </table>

        </td>
   
        <td  valign="bottom">

         <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCAPORZON1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCAPORZON1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbObjectifEBECAPORZON1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserEBECAPORZON1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
        <tr>
        <td class="ExportREXTDPAIR">
       
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneePORZON1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        </tr>
       <tr>
        <td class="ExportREXTDIMPAIR">
       
        </td>
        <td class="ExportREXTDIMPAIR">
       <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneePORZON1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        </tr>
   
        </table>

        </td>
    
        <td  valign="bottom">
        <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCATOTALN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCATOTALN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbObjectifEBECATOTALN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBECATOTALN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeTOTALN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeTOTALN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
   
        </table>
        </td>
    
        <td  valign="bottom">
             <table class="ExportREXGAUCHE">
        <tr>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbObjectifMasseSalarialeCACONSOLIDEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserMasseSalarialeCACONSOLIDEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbObjectifEBECACONSOLIDEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserEBECACONSOLIDEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDPAIR">
        
        </td>
        <td class="ExportREXTDPAIR">
         <asp:TextBox runat="server" ID="tbRealiserOutilsAnneeCONSOLIDEN1"  ReadOnly="true" CssClass="tbPair" />
        </td>
        <td class="ExportREXTDPAIR"></td>
        </tr>
         <tr>
        <td class="ExportREXTDIMPAIR">
        
        </td>
        <td class="ExportREXTDIMPAIR">
        <asp:TextBox runat="server" ID="tbRealiserCAHTHorsOutilsAnneeCONSOLIDEN1"  ReadOnly="true" CssClass="tbImpair" />
        </td>
        <td class="ExportREXTDIMPAIR"></td>
        </tr>
      
        </table>
        </td>
    </tr>
</table>
<br />
<br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnCalcRent" runat="server" CssClass="btn175" Text="Calcul et export du REX"
                    ValidationGroup="vgRechercher" />
            </td>
        </tr>
    </table>

</fieldset>

</asp:Content>
