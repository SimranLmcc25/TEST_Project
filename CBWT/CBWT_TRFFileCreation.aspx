<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_TRFFileCreation.aspx.cs"
    Inherits="INW_INWBranchFileCreation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC- CBWT System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script type="text/javascript">
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0! 
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        function toDate() {

            if (document.getElementById('txtFromDate').value != "__/__/____") {

                var toDate;
                toDate = dd + '/' + mm + '/' + yyyy;
                document.getElementById('txtToDate').value = toDate;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">

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
            open_popup('../HelpForms/HelpPrincipal_Officer.aspx?Branch=', 400, 500, "POHelp");
            return false;
        }
        function POHelp_Key(event) {
            var key = event.keyCode;
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

            var fromdate = document.getElementById('txtFromDate');
            var toDate = document.getElementById('txtToDate');

            var txtBatchDate = document.getElementById('txtBatchDate');

            var fromdateyyyy = fromdate.value.split("/")[2];
            var fromdatemm = fromdate.value.split("/")[1];
            var fromdatedd = fromdate.value.split("/")[0];

            if (fromdate.value == '' || fromdate.value == '__/__/____') {
                alert('Enter From Date.');
                document.getElementById('txtFromDate').focus();
                return false;
            }
            else {
                var calDt = new Date(parseFloat(fromdateyyyy), parseFloat(fromdatemm), 0);
                toDate.value = calDt.format("dd/MM/yyyy");
                txtBatchDate.value = toDate.value;
            }
        }

        function validateSave() {
            var txtBatchNo = document.getElementById('txtBatchNo');
            var txtBatchDate = document.getElementById('txtBatchDate');
            var txtPrincOffNo = document.getElementById('txtPrincOffNo');
            if (txtBatchNo.value == '') {
                alert('Enter Batch No.');
                txtBatchNo.focus();
                return false;
            }

            if (txtBatchDate.value == '') {
                alert('Enter Batch Date');
                txtBatchDate.focus();
                return false;
            }

            if (txtPrincOffNo.value == '') {
                alert('Enter Principal Officer ID.');
                txtPrincOffNo.focus();
                return false;
            }
        }

        function formatBatchNo() {
            var txtBatchNo = document.getElementById('txtBatchNo');
            txtBatchNo.value = "00000000" + txtBatchNo.value;

            txtBatchNo.value = txtBatchNo.value.substring(txtBatchNo.value.length - 8, txtBatchNo.value.length);

            if (parseFloat(txtBatchNo.value) == 0)
                txtBatchNo.value = "";
        }
    </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="../Scripts/InitEndRequest.js" type="text/javascript"></script>
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
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <%--  <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>--%>
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <span class="elementLabel">TRF File Creation</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="800px" style="line-height: 150%">
                                    <%--<tr>
                                        <td width="10%" align="right" nowrap>
                                            <span class="mandatoryField">* </span><span class="elementLabel">Ref No :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            &nbsp;<asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" 
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnPurCodeList" runat="server" CssClass="btnHelp_enabled" 
                                                    TabIndex="-1" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Date :</span>
                                        </td>
                                        <td align="left" style="width: 700px">
                                            &nbsp;<asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="2"></asp:TextBox>
                                            <asp:Button ID="btncalendar_FromDate" runat="server" CssClass="btncalendar_enabled"
                                                TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="mdfdate" Mask="99/99/9999" MaskType="Date" runat="server"
                                                TargetControlID="txtFromDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="btncalendar_FromDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtFromDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                            &nbsp;&nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Date:</span>
                                            &nbsp;<asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70" TabIndex="3" Enabled="false"></asp:TextBox>
                                            <%--<asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="Button2">
                                            </ajaxToolkit:CalendarExtender>--%>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="mdfdate"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                            <%--<asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Batch Number :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtBatchNo" runat="server" CssClass="textBox" MaxLength="8"
                                                TabIndex="4" onblur="formatBatchNo();" Width="80px"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Report Serial Number
                                                :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtReportSerialNumber" runat="server" CssClass="textBox" MaxLength="8"
                                                TabIndex="5" AutoPostBack="true" Width="80px"> </asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Batch Date :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtBatchDate" runat="server" CssClass="textBox" MaxLength="10"
                                                ValidationGroup="dtVal" Width="70px" TabIndex="6"></asp:TextBox>
                                            <asp:Button ID="btn_Batch_calender" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtBatchDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtBatchDate" PopupButtonID="btn_Batch_calender">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtBatchDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbTestMode" runat="server" CssClass="elementLabel" Text="Test Mode"
                                                GroupName="Mode" Style="forecolor: #000000; font-weight: bold" TabIndex="7" Checked="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbProdMode" runat="server" CssClass="elementLabel" Text="Production Mode"
                                                GroupName="Mode" Style="forecolor: #000000; font-weight: bold" TabIndex="8" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbNewReport" runat="server" CssClass="elementLabel" Text="New Report"
                                                GroupName="Report" Style="forecolor: #000000; font-weight: bold" AutoPostBack="true"
                                                OnCheckedChanged="rdbNewReport_CheckedChanged" TabIndex="8" Checked="true" />
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbReplaceReport" runat="server" CssClass="elementLabel" Text="Replacement Report"
                                                GroupName="Report" Style="forecolor: #000000; font-weight: bold" AutoPostBack="true"
                                                OnCheckedChanged="rdbReplaceReport_CheckedChanged" TabIndex="9" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Reason For Replacement
                                                :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtReasonForReplace" runat="server" CssClass="textBox" MaxLength="1"
                                                Width="28px" TabIndex="10" OnTextChanged="txtReasonForReplace_TextChanged" AutoPostBack="true"
                                                ToolTip="F2 For Help"></asp:TextBox>
                                            &nbsp;<asp:Button ID="btnHelpReason" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="lblReason" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Og. Batch Number :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtOgBatchNo" runat="server" CssClass="textBox" MaxLength="8"
                                                TabIndex="11" Width="80px"> </asp:TextBox>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Og. Report Serial Number
                                                :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtOgReportSerialNo" runat="server" CssClass="textBox" MaxLength="8"
                                                TabIndex="11" Width="80px"> </asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td nowrap align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Principal Officer :</span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtPrincOffNo" runat="server" CssClass="textBox" MaxLength="2"
                                                AutoPostBack="true" TabIndex="12" Width="28px" OnTextChanged="txtPrincOffNo_TextChanged"
                                                ToolTip="F2 For Help"> </asp:TextBox>
                                            &nbsp;<asp:Button ID="btnHelpPO" runat="server" CssClass="btnHelp_enabled" />
                                            <asp:Label ID="lblPO" runat="server" CssClass="elementLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <span class="mandatoryField">*</span> <span class="elementLabel">Data Structure Version
                                                : </span>
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtDataStruVer" runat="server" CssClass="textBox" MaxLength="1"
                                                TabIndex="13" Width="28px" Text="2"> </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 120px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Generate"
                                                ToolTip="Generate" TabIndex="14" OnClick="btnSave_Click" />
                                        </td>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" runat="server" id="hdnFromDate" />
                    <input type="hidden" runat="server" id="hdnToDate" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            GetToDate();
        }
    </script>
</body>
</html>
