<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STRAccountFile.aspx.cs" Inherits="CTR_STRAccountFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org3/1999/xhtml">
<head id="header1" runat="server">
    <title>LMCC - STR System</title>
    <link id="Link1" runat="server" rel="Shortcut Icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="Stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript">
        //        var today = new Date();
        //        var dd = today.getDate();
        //        var mm = today.getMonth() + 1; //January is 0! 
        //        var yyyy = today.getFullYear();
        //        if (dd < 10) { dd = '0' + dd }
        //        if (mm < 10) { mm = '0' + mm }
        //        function toDate() {

        //            if (document.getElementById('txtfromdate').value != "__/__/____") {

        //                var toDate;
        //                toDate = dd + '/' + mm + '/' + yyyy;
        //                document.getElementById('txttodate').value = toDate;
        //            }
        //        }


        function val_MonthYear() {
            var txtMonth = document.getElementById('txtMonth');
            // alert(1);
            if (txtMonth.value == '__/____') {

                alert('Enter Month Year');
                txtMonth.focus();
                return false;
            }

            if (parseFloat(txtMonth.value.substring(0, 2)) > '12' || parseFloat(txtMonth.value.substring(0, 2)) <= '00') {

                alert("Invalid Month");
                txtMonth.focus();
                return false;
            }
            return true;
        }

        function ReasonHelp() {

            open_popup('../HelpForms/HelpReason_For_Replacement.aspx', 400, 500, "ReasonHelp");
            return false;
        }

        function ReasonHelp_Key(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                open_popup('../HelpForms/HelpReason_For_Replacement.aspx', 400, 500, "ReasonHelpKey");
            }
        }

        function selectCust(Reason, Desc) {
            document.getElementById('txtReasonForReplace').value = Reason;
            document.getElementById('lblReason').innerHTML = Desc;

            document.getElementById('txtReasonForReplace').focus();
        }


        function POHelp() {
            //var Branch = document.getElementById('ddlBranch').value;

            open_popup('../HelpForms/HelpPrincipal_Officer.aspx?Branch=', 400, 500, "POHelp");
            return false;
        }


        function POHelp_Key(event) {

            var key = event.keyCode;
            //var Branch = document.getElementById('ddlBranch').value;

            if (key == 113 && key != 13) {
                open_popup('../HelpForms/HelpPrincipal_Officer.aspx?Branch=', 400, 500, "POHelp");
            }

        }

        function selectPO_Off(id, Desc) {
            document.getElementById('txtPrincOffNo').value = id;
            document.getElementById('lblPO').innerHTML = Desc;
            document.getElementById('txtPrincOffNo').focus();

        }


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

        function save_validate() {
            var txtfromdate = document.getElementById('txtfromdate');


            var txttodate = document.getElementById('txttodate');
            var txtReportNo = document.getElementById('txtReportNo');
            var txtMonth = document.getElementById('txtMonth');
            var txtPrincOffNo = document.getElementById('txtPrincOffNo');
            var txtDataStruVer = document.getElementById('txtDataStruVer');
            var txtNoofBranches = document.getElementById('txtNoofBranches');
            var txtBranchesSubmitted = document.getElementById('txtBranchesSubmitted');
            var txtTotalBranches = document.getElementById('txtTotalBranches');
            var txtReasonForReplace = document.getElementById('txtReasonForReplace');
            var rdbNewReport = document.getElementById('rdbNewReport');
            var rdbReplaceReport = document.getElementById('rdbReplaceReport');


            if (txtfromdate.value == '') {
                alert('Enter From Date');
                txtfromdate.focus();
                return false;

            }

            if (txttodate.value == '') {
                alert('Enter To Date');
                txttodate.focus();
                return false;

            }

            if (txtReportNo.value == '') {
                alert('Enter Report No');
                txtReportNo.focus();
                return false;
            }

            if (txtMonth.value == '') {
                alert('Enter Month Year');
                txtMonth.focus();
                return false;
            }
            if (rdbReplaceReport.checked == true) {
                if (txtReasonForReplace.value == '') {
                    alert('Enter Reason For Replace');
                    txtReasonForReplace.focus();
                    return false;
                }
            }

            if (txtPrincOffNo.value == '') {
                alert('Enter Principle Office Number');
                txtPrincOffNo.focus();
                return false;
            }
            if (txtDataStruVer.value == '') {
                alert('Enter Data structure Version')
                txtDataStruVer.focus();
                return false;
            }

            //            if (txtTotalBranches.value == '') {
            //                alert('Enter Total Branches');
            //                txtTotalBranches.focus();
            //                return false;
            //            }

            if (txtNoofBranches.value == '') {
                alert('Enter No of Branches Sent Report');
                txtNoofBranches.focus();
                return false;
            }
            if (txtBranchesSubmitted.value == '') {
                alert('Enter No of Branches Submitted STR');
                txtBranchesSubmitted.focus();
                return false;
            }
            return true;
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
                            <td align="left" valign="bottom" colspan="4">
                                <span class="pageLabel" style="font-weight: bold">Generation of STR File</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top" colspan="4">
                                <asp:Label ID="LabelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right" width="5%">
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlbranch" runat="server" CssClass="dropdownList" Width="100px"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right" width="15%">
                                <span class="elementLabel">From Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtfromdate" runat="server" CssClass="textBox" Width="70px" TabIndex=1></asp:TextBox>
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
                          <td></td>
                            <td align=center><span class=elementLabel>Details Of Last Report</span></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">To Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txttodate" runat="server" CssClass="textBox" Width="70px" MaxLength=2></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_DOI" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txttodate" ErrorTooltipEnabled="True" CultureName="en-GB" CultureAMPMPlaceholder="AM;PM"
                                    CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY" CultureDatePlaceholder="/"
                                    CultureDecimalPlaceholder="." CultureThousandsPlaceholder="," CultureTimePlaceholder=":"
                                    Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnDOI" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txttodate" PopupButtonID="btnDOI" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="ME_DOI"
                                    ValidationGroup="dtVal" ControlToValidate="txttodate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false"></ajaxToolkit:MaskedEditValidator>
                            </td>
                            <td></td>
                            <td align=Center><span class=elementLabel>Report Serial No :</span>
                            <asp:Label ID=lblLastSerialNo runat=server CssClass=elementLabel Width=60px > </asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Report Serial No :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtReportNo" runat="server" CssClass="textBox" Width="60px" 
                                    ontextchanged="txtReportNo_TextChanged" AutoPostBack=true TabIndex=3 MaxLength=8></asp:TextBox>
                            </td>
                           <td></td>
                            <td align=center><span class=elementLabel>Month & Year of Report : </span>
                            <asp:Label ID=lblLastMonthYear CssClass=elementLabel runat=server Width=40px></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Month/Year :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtMonth" runat="server" CssClass="textBox" Width="50px" TabIndex=4></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdApplicationDate" runat="server" ClearMaskOnLostFocus="false"
                                    CultureDateFormat="MY" Enabled="true" ErrorTooltipEnabled="True" Mask="99/9999"
                                    MaskType="None" TargetControlID="txtMonth">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:RadioButton ID="rdbTestMode" runat="server" CssClass="elementLabel" Text="Test Mode"
                                    GroupName="Mode" Style="forecolor: #000000; font-weight: bold" TabIndex="5" Checked="true" />
                                <asp:RadioButton ID="rdbProdMode" runat="server" CssClass="elementLabel" Text="Production Mode"
                                    GroupName="Mode" Style="forecolor: #000000; font-weight: bold" TabIndex="5" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:RadioButton ID="rdbNewReport" runat="server" CssClass="elementLabel" Text="New Report"
                                    GroupName="Report" Style="forecolor: #000000; font-weight: bold" AutoPostBack="true"
                                    TabIndex="6" Checked="true" 
                                    oncheckedchanged="rdbNewReport_CheckedChanged" />
                                <asp:RadioButton ID="rdbReplaceReport" runat="server" CssClass="elementLabel" Text="Replacement Report"
                                    GroupName="Report" Style="forecolor: #000000; font-weight: bold" AutoPostBack="true"
                                    TabIndex="6" oncheckedchanged="rdbReplaceReport_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="right">
                                <span class="elementLabel">Reason For Replacement :</span>
                            </td>
                            <td align="left" colspan=3>
                                <asp:TextBox ID="txtReasonForReplace" runat="server" CssClass="textBox" MaxLength="1"
                                    Width="28px" TabIndex="7" AutoPostBack="true" ToolTip="F2 For Help"></asp:TextBox>
                                &nbsp;<asp:Button ID="btnHelpReason" runat="server" CssClass="btnHelp_enabled" />
                                <asp:Label ID="lblReason" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="right">
                                <span class="elementLabel">Og. Report Serial Number :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtOgReportSerialNo" runat="server" CssClass="textBox" MaxLength="8"
                                    TabIndex="8" Width="60px"> </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td nowrap align="right">
                                <span class="elementLabel">Principal Officer :</span>
                            </td>
                            <td align="left" colspan=3>
                                <asp:TextBox ID="txtPrincOffNo" runat="server" CssClass="textBox" MaxLength="2" AutoPostBack="true"
                                    TabIndex="9" Width="28px" ToolTip="F2 For Help"> </asp:TextBox>
                                &nbsp;<asp:Button ID="btnHelpPO" runat="server" CssClass="btnHelp_enabled" />
                                <asp:Label ID="lblPO" runat="server" CssClass="elementLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Data Structure Version : </span>
                            </td>
                            <td align="left" width="10%">
                                <asp:TextBox ID="txtDataStruVer" runat="server" CssClass="textBox" MaxLength="1"
                                    TabIndex="10" Width="28px" Text="2"> </asp:TextBox>
                            </td>
                            <td align="right" width="15%" nowrap>
                                <span class="elementLabel">No. of STRs :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNoofCTR" CssClass="textBox" runat="server" Width="50px" Enabled=false ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Total Branches : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTotalBranches" runat="server" CssClass="textBox" 
                                    Width="50px"  TabIndex=11></asp:TextBox>
                            </td>
                            <td align="right" nowrap>
                                <span class="elementLabel">No of Transaction : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNoofTransaction" runat="server" CssClass="textBox" Width="50px" Enabled=false></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">No of Branches Sent Reports : (Including NIL Report) </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNoofBranches" runat="server" CssClass="textBox" Width="50px" TabIndex=12 MaxLength=8></asp:TextBox>
                            </td>
                            <td align="right">
                                <span class="elementLabel">No of Individual Person :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIndividual" CssClass="textBox" runat="server" Width="50px" Enabled=false></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">No of Branches Submitted STR : (Excluding NIL Report)</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBranchesSubmitted" runat="server" CssClass="textBox" Width="50px" TabIndex=13 MaxLength=8></asp:TextBox>
                            </td>
                            <td align="right">
                                <span class="elementLabel">No of Legal Person/Entity : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLegalPerson" CssClass="textBox" runat="server" Width="50px" Enabled=false></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" height="60px">
                                <asp:Button ID="btnGenerate" CssClass="buttonDefault" Text="Generate" runat="server"
                                    OnClick="btnGenerate_Click" TabIndex=14/>
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
