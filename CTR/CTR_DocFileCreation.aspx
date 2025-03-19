<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTR_DocFileCreation.aspx.cs"
    Inherits="CTR_CTR_DocFileCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org3/1999/xhtml">
<head id="header1" runat="server">
    <title>LMCC - CTR System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="Stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">

        function GetToDate() {

            var fromdate = document.getElementById('txtfromdate');
            var toDate = document.getElementById('txttodate');

            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];

            if (fromdate.value == '' || fromdate.value == '__/__/____') {
                alert('Enter From Date.');
                document.getElementById('txtfromdate').focus();
                return false;
            }
            else {

                var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);

                toDate.value = calDt.format("dd/MM/yyyy");
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="0" border="0">
                        <tr>
                            <td align="left" valign="bottom" colspan="2">
                                <span class="pageLabel" style="font-weight: bold">Generation of CTR Doc File</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="2">
                                <asp:Label ID="LabelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlbranch" runat="server" CssClass="dropdownList" Width="100px" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">
                                <span class="elementLabel">From Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="textBox" Width="70px" TabIndex="1"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_DOI1" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtfromdate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnDOI1" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtfromdate" PopupButtonID="btnDOI1" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="ME_DOI1"
                                    ValidationGroup="dtVal" ControlToValidate="txtfromdate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">To Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txttodate" runat="server" CssClass="textBox" Width="70px" MaxLength="10"
                                    Enabled="false"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_DOI" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txttodate" ErrorTooltipEnabled="True" CultureName="en-GB" CultureAMPMPlaceholder="AM;PM"
                                    CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                    CultureDecimalPlaceholder="." CultureThousandsPlaceholder="," CultureTimePlaceholder=":"
                                    Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <%--<asp:Button ID="btnDOI" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txttodate" PopupButtonID="btnDOI" Enabled="True">
                                </ajaxToolkit:CalendarExtender>--%>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="ME_DOI"
                                    ValidationGroup="dtVal" ControlToValidate="txttodate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" height="60px">
                                <asp:Button ID="btnGenerate" CssClass="buttonDefault" Text="Generate" runat="server"
                                    TabIndex="3" OnClick="btnGenerate_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
