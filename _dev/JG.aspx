<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="YV.aspx.vb" Inherits="ComptaAna.net.JL" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<telerik:RadChart EnableViewState="false" ID="chartConformiteReg" runat="server" style="margin-left:10px;"
            DataGroupColumn="Name" AutoTextWrap="true" Width="1024" Skin="Web20">
            <Legend>
                <Appearance GroupNameFormat="#VALUE" Position-Auto="false" Position-X="360" Position-Y="100">
                </Appearance>
            </Legend>
              
            <PlotArea>
                <XAxis DataLabelsColumn="AffaireLibelle">
                </XAxis>
             <YAxis>
                    </YAxis>
</PlotArea>
              <ChartTitle>
                <TextBlock Text="DataTable">
                </TextBlock>
            </ChartTitle>

</telerik:RadChart>--%>
        <telerik:RadChart ID="rcRexGraphe" runat="server" AutoLayout="false" AutoTextWrap="false"
            EnableHandlerDetection="false" Width="600" OnItemDataBound="rcRexGraphe_ItemDataBound">
            <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="EBE par service"
                Appearance-Position-AlignedPosition="Top">
            </ChartTitle>
            <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                Appearance-FillStyle-MainColor="#ffffff">
            </PlotArea>
            <Series>
                <telerik:ChartSeries Type="Pie" DataYColumn="Rex">
                    <Appearance LegendDisplayMode="ItemLabels">
                        <TextAppearance TextProperties-Color="#6788be">
                        </TextAppearance>
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
        </telerik:RadChart>

        <telerik:RadChart ID="rcOutilsGraphe" runat="server" AutoLayout="false" AutoTextWrap="false"
            EnableHandlerDetection="false" Width="600" OnItemDataBound="rcOutilsGraphe_ItemDataBound">
            <ChartTitle TextBlock-Appearance-Position-AlignedPosition="Center" TextBlock-Text="Outils par service"
                Appearance-Position-AlignedPosition="Top">
            </ChartTitle>
            <PlotArea Appearance-Border-Visible="False" Appearance-FillStyle-FillType="Solid"
                Appearance-FillStyle-MainColor="#ffffff">
            </PlotArea>
            <Series>
                <telerik:ChartSeries Type="Pie" DataYColumn="Outils">
                    <Appearance LegendDisplayMode="ItemLabels">
                        <TextAppearance TextProperties-Color="#6788be">
                        </TextAppearance>
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
        </telerik:RadChart>

        <telerik:RadChart ID="rcCAEmploye" runat="server" Width="700"
            Visible="true" SeriesOrientation="Horizontal" AutoLayout="True" AutoTextWrap="false"
            Skin="Web20" EnableHandlerDetection="false">
            <Legend Visible="false">
                <Appearance Position-Auto="True">
                </Appearance>
            </Legend>
            <PlotArea>
                <XAxis DataLabelsColumn="Nom">
                </XAxis>
                
            </PlotArea>
            <Series>
                <telerik:ChartSeries Type="Bar" DataYColumn="CA">
                    <Appearance LegendDisplayMode="ItemLabels">
                        <TextAppearance TextProperties-Color="#6788be">
                        </TextAppearance>
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <ChartTitle>
                <TextBlock Text="Le Chiffre d'Affaire par employé">
                </TextBlock>
            </ChartTitle>
        </telerik:RadChart>

        <telerik:RadChart ID="rcFormationGraphe" runat="server" Width="700" Height="900" 
            Visible="true" SeriesOrientation="Horizontal" AutoLayout="True" AutoTextWrap="false"
            Skin="Web20" EnableHandlerDetection="false">
            <Legend Visible="false">
                <Appearance Position-Auto="True">
                </Appearance>
            </Legend>
            <PlotArea>
                <XAxis DataLabelsColumn="Nom" AutoScale="true" AutoShrink="true">
                </XAxis>
            </PlotArea>
            <Series>
                <telerik:ChartSeries Type="Bar" DataYColumn="Cout">
                    <Appearance LegendDisplayMode="ItemLabels">
                        <TextAppearance TextProperties-Color="#6788be">
                        </TextAppearance>
                    </Appearance>
                </telerik:ChartSeries>
            </Series>
            <ChartTitle>
                <TextBlock Text="Le Montant de la Formation Prévu par employé">
                </TextBlock>
            </ChartTitle>
        </telerik:RadChart>
    </div>
    </form>
</body>
</html>
