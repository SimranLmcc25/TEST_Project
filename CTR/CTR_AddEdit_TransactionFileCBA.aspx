<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTR_AddEdit_TransactionFileCBA.aspx.cs"
    Inherits="CTR_CTR_AddEdit_TransactionFileCBA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title>LMCC-CTR System</title>
    <link href="../Style/Style.css" rel="Stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">

        function Currency() {
            open_popup('../HelpForms/TF_CurrencyLookUp1.aspx?', 450, 300, 'Currency');
            return false;
        }

        function OpenCurrencyList(event) {
            var key = event.keyCode;

            if (key == 113 && key != 13) {
                document.getElementById('btnCurrency').click();
            }
        }

        function selectCurrency(id) {

            document.getElementById('txtCurrency').value = id;
            __doPostBack('txtCurrency', '');
        }


        function amt() {
            var txtAmt = document.getElementById('txtAmt');
            var txtrate = document.getElementById('txtrate');
            var amtINR = document.getElementById('amtINR');
            var txtAmtINR = document.getElementById('txtAmtINR')
            // var txtCurrency = document.getElementById('txtCurrency');

            amtINR = parseFloat(txtAmt.value) * parseFloat(txtrate.value);
            txtAmtINR.value = amtINR;


            if (txtrate.value == '' || txtAmt.value == '') {
                txtAmtINR.value = "0";

            }
            return true;
        }

        function rate() {
            var txtrate = document.getElementById('txtrate');
            var txtCurrency = document.getElementById('txtCurrency');
            var txtAmt = document.getElementById('txtAmt');

            if (txtCurrency.value == 'INR') {
                txtrate.value = "1";
                txtAmt.focus();
                return false;
            }
            return true;
        }

        function validate_Save() {
            var txtTransactionDate = document.getElementById('txtTransactionDate');
            var txtAccountNo = document.getElementById('txtAccountNo');
            var txtCurrency = document.getElementById('txtCurrency');
            var txtAmt = document.getElementById('txtAmt');
            var txtrate = document.getElementById('txtrate');
            var txtAmtINR = document.getElementById('txtAmtINR');
            var txtFund = document.getElementById('txtFund');

            if (txtTransactionDate.value == '') {
                alert('Enter Transaction Date');
                txtTransactionDate.focus();
                return false;
            }

            if (txtAccountNo.value == '') {
                alert('Enter Account No.');
                txtAccountNo.focus();
                return false;
            }

            if (txtCurrency.value == '') {
                alert('Enter Currency');
                txtCurrency.focus();
                return false;
            }

            if (txtAmt.value == '') {
                alert('Enter Amount');
                txtAmt.focus();
                return false;
            }
            if (txtrate.value == '') {
                txtrate.value = '0';
                return false;
            }
            if (txtFund.value == '') {
                txtFund.value = 'X';
                return false;
            }
        }

        function Accounthelp() {
            var txtBranch = document.getElementById('txtBranch');
            open_popup('../HelpForms/Help_AccountNo.aspx?branch=' + txtBranch.value, 450, 400, 'Branch');
            return false;
        }

        function OpenAccountList(event) {
            var key = event.keyCode;

            if (key == 113 && key != 13) {
                document.getElementById('btnAccount').click();
            }
        }

        function selectAccount(id) {

            document.getElementById('txtAccountNo').value = id;
            __doPostBack('txtAccountNo', '');
        }

        function isValidDate(controlID, CName) {
            var obj = controlID;

            if (controlID.value != "__/__/____") {
                var day = obj.value.split("/")[0];
                var month = obj.value.split("/")[1];
                var year = obj.value.split("/")[2];
                var today = new Date();

                if (day == "__") {
                    day = today.getDay();
                }
                if (month == "__") {
                    month = today.getMonth() + 1;
                }
                if (year == "____") {
                    year = today.getFullYear();
                }
                var dt = new Date(year, month - 1, day);

                if ((dt.getDate() != day) || (dt.getMonth() != month - 1) || (dt.getFullYear() != year) || (dt > today)) {

                    alert('Invalid ' + CName);
                    controlID.focus();
                    return false;
                }
            }
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
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
    <asp:UpdateProgress DynamicLayout="true" runat="server" ID="updateProgress">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <uc2:Menu ID="Menu1" runat="server" />
        <br />
        <center>
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="left" valign="bottom">
                                <span class="pageLabel" style="font-weight: bold">CBA Transaction File Data Entry</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" TabIndex="12" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Branch : </span>
                            </td>
                            <td align="left" width="10%">
                                <asp:TextBox ID="txtBranch" CssClass="textBox" runat="server" Enabled="false" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                           <tr>
                            <td align="right">
                                <span class="elementLabel">Reporting Type : </span>
                            </td>
                            <td align="left" colspan="2">
                                 <asp:RadioButton ID="CTR" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="CTR" GroupName="STR" TabIndex="6" 
                                     oncheckedchanged="CTR_CheckedChanged" AutoPostBack="true"  />
                                <asp:RadioButton ID="STR" runat="server" CssClass="elementLabel" Text="STR"
                                    GroupName="STR" TabIndex="6" oncheckedchanged="STR_CheckedChanged" AutoPostBack="true"  />
                            </td>
                        </tr>

                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Transaction ID : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox CssClass="textBox" runat="server" ID="txttransactionID" OnTextChanged="txttransactionID_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Transaction Date : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="textBox" Width="70px"
                                    TabIndex="1"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_DOI" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtTransactionDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnDOI" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtTransactionDate" PopupButtonID="btnDOI" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="ME_DOI"
                                    ValidationGroup="dtVal" ControlToValidate="txtTransactionDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Account No : </span>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textBox" OnTextChanged="txtAccountNo_TextChanged"
                                    AutoPostBack="true" TabIndex="2" MaxLength="20"></asp:TextBox>
                                <asp:Button ID="btnAccount" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                    OnClientClick="return Accounthelp();" />
                                <asp:Label ID="lblAccount" runat="server" CssClass="elementLabel" Width="400px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Currency of Transaction : </span>
                            </td>
                            <td align="left" width="5%" colspan="2">
                                <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" MaxLength="3" Width="30px"
                                    OnTextChanged="txtCurrency_TextChanged" AutoPostBack="true" TabIndex="3" Text="INR"></asp:TextBox>
                                <asp:Button ID="btnCurrency" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                    OnClientClick="return Currency();" />
                                <asp:Label ID="lblCurrency" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Amount : </span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox ID="txtAmt" CssClass="textBoxRight" runat="server" Width="80px" TabIndex="4"
                                    MaxLength="20"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rbtcash" runat="server" CssClass="elementLabel" Text="Cash"
                                    Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Conversion Rate : </span>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:TextBox ID="txtrate" CssClass="textBoxRight" runat="server" Width="60px" TabIndex="5"
                                    MaxLength="20"></asp:TextBox>
                                &nbsp;&nbsp;
                                <asp:RadioButton ID="rbtCeadit" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="Credit" GroupName="Credit" TabIndex="6" />
                                <asp:RadioButton ID="rbtdebit" runat="server" CssClass="elementLabel" Text="Debit"
                                    GroupName="Credit" TabIndex="6" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span>
                                <span class="elementLabel">Amount INR : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmtINR" runat="server" CssClass="textBoxRight" Width="80px" TabIndex="7"
                                    MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Disposition of Fund : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtFund" CssClass="textBox" runat="server" Width="20px" Text="X"
                                    TabIndex="8" MaxLength="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Remark : </span>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtRemark" CssClass="textBox" runat="server" Width="250px" TabIndex="9"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                      

                        <tr>
                            <td>
                            </td>
                            <td align="left" style="white-space: nowrap">
                                <asp:Button ID="btnSave" CssClass="buttonDefault" runat="server" Text="Save" OnClick="btnSave_Click"
                                    TabIndex="10" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" CssClass="buttonDefault" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" TabIndex="11" />
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
