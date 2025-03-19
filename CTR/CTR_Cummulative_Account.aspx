<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CTR_Cummulative_Account.aspx.cs"
    Inherits="CTR_Cummulative_Account" %>

<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC - CTR System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="../../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }


        function Customer(hno) {
            open_popup('../HelpForms/TF_CustomerLookUp2.aspx?hNo=' + hno, 450, 450, 'Customer');
            return false;
        }

        function OpenCustomerList(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnAcNo').click();
            }
        }

        function selectCustomer(id, hNo) {
            if (hNo == 1) {
                document.getElementById('txtAccountNo').value = id;
            }
            __doPostBack('txtAccountNo', '');
        }

//        function Accounthelp() {
//            var txtBranch = document.getElementById('txtBranch');
//            open_popup('../HelpForms/Help_AccountNo.aspx?branch=' + txtBranch.value, 450, 400, 'Branch');
//            return false;
//        }

//        function OpenAccountList(event) {
//            var key = event.keyCode;

//            if (key == 113 && key != 13) {
//                document.getElementById('btnAccount').click();
//            }
//        }

//        function selectAccount(id) {

//            document.getElementById('txtAccountNo').value = id;
//            __doPostBack('txtAccountNo', '');
//        }

        function validate_save() {
            var txtCummCredit = document.getElementById('txtCummCredit');
            var txtCummDebit = document.getElementById('txtCummDebit');
            var txtCummCashDeposit = document.getElementById('txtCummCashDeposit');
            var txtCummCashWithdrawal = document.getElementById('txtCummCashWithdrawal');
            var txtAccountNo = document.getElementById('txtAccountNo');
            var txtMonthYear=document.getElementById('txtMonthYear');

            if (txtAccountNo.value == '') {
                alert('Enter Account Number');
                txtAccountNo.focus();
                return false;
            }

            if (txtMonthYear.value == '') {
                alert('Enter Month Year');
                txtMonthYear.focus();
                return false;
            }

            if (txtCummCredit.value == '') {
                alert('Enter Cummulative Credit');
                txtCummCredit.focus();
                return false;
            }

            if (txtCummDebit.value == '') {
                alert('Enter Cummulative Debit');
                txtCummDebit.focus();
                return false;
            }

            if (txtCummCashDeposit.value == '') {
                alert('Enter Cummulative Case Deposit');
                txtCummCashDeposit.focus();
                return false;
            }

            if (txtCummCashWithdrawal.value == '') {
                alert('Enter Cummulative Cash Withdrawal');
                txtCummCashWithdrawal.focus();
                return false;
            }
            return true;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="left" colspan="2">
                            <span class="pageLabel"><strong>Cummulative Total Data Entry </strong></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 200px">
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Account Number :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAccountNo" runat="server" CssClass="textBox" OnTextChanged="txtAccountNo_TextChanged" Width=100px AutoPostBack=true MaxLength="20"></asp:TextBox>
                            <asp:Button ID="btnAcNo" runat="server" CssClass="btnHelp_enabled" OnClientClick="return Customer(1);" />
                            <asp:Label ID="lblCustName" CssClass="elementLabel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Month/ Year :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMonthYear" runat="server" CssClass="textBox" Width="50px"></asp:TextBox>
                            
                                    </td>

                            <%-- <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />--%>
                    </tr>
                    <%--  <tr>
                        <td align="right">
                            <span class="elementLabel">Account Number :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textBox"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Cummulative Credit :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCummCredit" runat="server" CssClass="textBoxRight" Width=80px></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Cummulative Debit :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCummDebit" runat="server" CssClass="textBoxRight" Width=80px></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Cummulative Cash Deposit :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCummCashDeposit" runat="server" CssClass="textBoxRight" Width=80px></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap>
                            <span class="mandatoryField">*</span>
                            <span class="elementLabel">Cummulative Cash Withdrawal :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCummCashWithdrawal" runat="server" CssClass="textBoxRight" Width=80px></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                            <td align="right">
                                <span class="elementLabel">Reporting Type : </span>
                            </td>
                            <td align="left" colspan="2">
                                 <asp:RadioButton ID="CTR" runat="server" CssClass="elementLabel" Checked="true"
                                    Text="CTR" GroupName="STR" TabIndex="6" />
                                <asp:RadioButton ID="STR" runat="server" CssClass="elementLabel" Text="STR"
                                    GroupName="STR" TabIndex="6" />
                            </td>
                        </tr>
                    <tr>
                        <td align="right" nowrap>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" Text="Cancel"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
