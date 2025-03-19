<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EBRC_CancellationDataEntry.aspx.cs"
    Inherits="EBR_EBRC_CancellationDataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC- EBRC System</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function OpenCustomerCodeList(e) {
            var keycode;
            var txtCustAcNo;
            var Branch = document.getElementById('ddlBranch').value;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtCustAcNo = document.getElementById('txtCustAcNo').value;
                open_popup('EBR_CustomerHelp.aspx?CustID=' + txtCustAcNo + '&Branch=' + Branch, 450, 450, 'CustomerCodeList');
                return false;
            }
        }
        function selectCustomer(selectedID) {

            var id = selectedID;
            document.getElementById('hdnCustomerCode').value = id;
            document.getElementById('btnCustomerCode').click();
        }

        function OpenEBRCNoList(e) {
            var keycode;
            var txtCustAcNo;
            var Branch = document.getElementById('ddlBranch').value;
            var Year = document.getElementById('txtYear').value;

            if (window.event) keycode = window.event.keyCode;
            if (keycode == 113 || e == 'mouseClick') {
                txtCustAcNo = document.getElementById('txtCustAcNo').value;
                open_popup('Help_EBRC_EBRCNo.aspx?CustID=' + txtCustAcNo + '&Branch=' + Branch +'&Year=' +Year, 400, 750, 'EBRCNoList');
                return false;
            }

        }
        function selectEBRCNo(selectedID) {

            var id = selectedID;
            document.getElementById('hdnEBRCNo').value = id;
            document.getElementById('btnBRCNumber').click();
        }

        //        function ConfirmSave() {
        //           
        //            var Ok = confirm('Are you sure want to save the changes?');
        //            if (Ok)
        //                return true;
        //            else
        //                return false;
        //        }

        function toUpper_Case() {
            document.getElementById('txtStatus').value = (document.getElementById('txtStatus').value).toUpperCase();
        }

        function ValidateSave() {

            var Branch = document.getElementById('ddlBranch');

            if (Branch.value == "0") {
                alert('Select Branch Name.');
                Branch.focus();
                return false;
            }
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var txtEBRCNo = document.getElementById('txtEBRCNo');
            var txtStatus = document.getElementById('txtStatus');

            if (txtCustAcNo.value == '') {
                alert('Please Enter Customer A/C No.');
                txtCustAcNo.focus();
                return false;
            }

            if (txtEBRCNo.value == '') {
                alert('Please Enter EBRC No.');
                txtEBRCNo.focus();
                return false;
            }

            if (txtStatus.value == '' || txtStatus.value !== 'C') {
                alert('Please enter Status C for cancellation');
                txtStatus.focus();
                return false;
            }

            var Ok = confirm('Are you sure you want to cancel this E-BRC Certificate?');
            if (Ok) {
                return true;
            }
            else {
                return false;
            }
            return true;
        }

        function numberFormat() {
            var txtRealisedAmt = document.getElementById('txtRealisedAmt');

            if (txtRealisedAmt.value == '')
                txtRealisedAmt.value = 0;
            txtRealisedAmt.value = parseFloat(txtRealisedAmt.value).toFixed(2);

            var txtAmtinINR = document.getElementById('txtAmtinINR');
            if (txtAmtinINR.value == '')
                txtAmtinINR.value = 0;
            txtAmtinINR.value = parseFloat(txtAmtinINR.value).toFixed(2);
        }
       
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
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
    <div align="left">
        <center>
            <uc1:Menu ID="Menu1" runat="server" />
            <br />
        </center>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left" style="width: 50%" valign="bottom">
                            <span class="pageLabel">E-BRC Cancellation Data Entry</span>
                        </td>
                        <td align="right" style="width: 50%" nowrap>
                         <asp:Label runat="server" ID="lblSupervisormsg" Text="Role : Supervisor - ONLY VIEW THE DATA  " 
                                    style="color:red" Visible="false" ></asp:Label>&nbsp;

                            <%--<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                TabIndex="62" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <input type="hidden" id="hdnCustomerCode" runat="server" />
                            <asp:Button ID="btnCustomerCode" Style="display: none;" runat="server" OnClick="btnCustomerCode_Click" />
                            <input type="hidden" id="hdnEBRCNo" runat="server" />
                            <asp:Button ID="btnBRCNumber" Style="display: none;" runat="server" OnClick="btnBRCNumber_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%;" valign="top" colspan="2">
                            <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0" border="0" width="45%">
                    <tr align="left">
                        <td rowspan="14" width="40%">
                        &nbsp;
                        </td>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Branch :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" AutoPostBack="true" runat="server"
                                TabIndex="1" Width="100px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Customer ID :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtCustAcNo" runat="server" MaxLength="12" TabIndex="2" Width="100px"
                                CssClass="textBox" ToolTip="Press F2 for Help" OnTextChanged="txtCustAcNo_TextChanged"
                                onkeydown="OpenCustomerCodeList(this);" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btnCustAcNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label ID="lblCustName" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Year :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="50px" CssClass="textBox" TabIndex="3"
                               ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">E-BRC No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtEBRCNo" runat="server" MaxLength="20" TabIndex="4" Width="150px"
                                CssClass="textBox" AutoPostBack="true" OnTextChanged="txtEBRCNo_TextChanged"
                                onkeydown="OpenEBRCNoList(this);"></asp:TextBox>
                            <asp:Button ID="btnEBRCNo" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            <asp:Label ID="lblEBRCNum" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Sr No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtSrNo" runat="server" MaxLength="6" Width="50px" CssClass="textBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Document No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDocNo" runat="server" MaxLength="20" Width="150px" CssClass="textBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td width="20%" align="right" nowrap bgcolor="#b0b0ff">
                            <span class="elementLabel">Realization Date :</span>
                        </td>
                        <td align="left" nowrap  bgcolor="#b0b0ff">
                            <asp:TextBox ID="txtDocumentDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap  bgcolor="#b0b0ff">
                            <span class="elementLabel">Currency :</span>
                        </td>
                        <td align="left" nowrap  bgcolor="#b0b0ff">
                            <asp:TextBox ID="txtCurrency" runat="server" CssClass="textBox" MaxLength="3" Width="50px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Realised Amount(FC) :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtRealisedAmt" runat="server" CssClass="textBox" MaxLength="20"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Amount in INR :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtAmtinINR" runat="server" CssClass="textBox" MaxLength="20" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Shipping Bill No :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtShippingBillNo" runat="server" CssClass="textBox" MaxLength="7"
                                Width="100px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Shipping Bill Date :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtShippingBillDate" runat="server" CssClass="textBox" MaxLength="10"
                                ValidationGroup="dtVal" Width="70px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                     <tr bgcolor="#b0b0ff">
                        <td width="20%" align="right" nowrap>
                            <span class="elementLabel">Port Code :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtPortCode" runat="server" CssClass="textBox" MaxLength="6"
                                ValidationGroup="dtVal" Width="70px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%" align="right" nowrap height="30px">
                            <span class="elementLabel">Enter C for Cancellation :</span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtStatus" runat="server" MaxLength="4" Width="20px" CssClass="textBox"
                                TabIndex="4"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" border="0" width="100%">
                    <tr>
                        <td width="15%">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonDefault" ToolTip="Save"
                                TabIndex="10" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonDefault"
                                ToolTip="Cancel" TabIndex="11" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script language="javascript" type="text/javascript">
        window.onload = function () {

            numberFormat();


        }
    </script>
</body>
</html>
