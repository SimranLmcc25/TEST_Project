<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CBWT_AddEdit_TransactionFile.aspx.cs"  Inherits="CBWT_AddEdit_TransactionFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>LMCC- CBWT System</title>
    <link href="../Style/Style.css" rel="Stylesheet" type="text/css" />
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script language="javascript" type="text/javascript">
        function CountryHelp(hNo) {
            open_popup('../HelpForms/TF_CountryLookUp1.aspx?hNo=' + hNo, 450, 400, 'Country');
            return false;
        }

        function selectCountry(selectedID, hNo) {
            var id = selectedID;
            if (hNo == 1) {
                document.getElementById('txtTranCountry').value = id;
                __doPostBack('txtTranCountry', '');
            }
            else if (hNo == 2) {
                document.getElementById('txtInstCountry').value = id;
                __doPostBack('txtInstCountry', '');
            }
            else if (hNo == 3) {
                document.getElementById('txtsecondLagTransCountry').value = id;
                __doPostBack('txtsecondLagTransCountry', '');
            }
        }

        function PuposeCode() {
            open_popup('../HelpForms/TF_PurposeCodeLookUp2.aspx?hNo=1', 450, 400, 'PuposeCode');
            return false;
        }

        function OpenPuposecodeList(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnPuposeCode').click();
            }
        }

        function selectPurpose(id, hNo) {
            document.getElementById('txtPuposeCode').value = id;
            __doPostBack('txtPuposeCode', '');
        }

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


        function State() {
            open_popup('../HelpForms/HelpStateMaster.aspx?', 450, 300, 'State');
            return false;
        }

        function OpenStateList(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnState').click();
            }
        }

        function selectState(id) {
            document.getElementById('txtStateCode').value = id;
            __doPostBack('txtStateCode', '');
        }

        function Customer(hno) {
            open_popup('../HelpForms/TF_CustomerLookUp2.aspx?hNo=' + hno, 450, 450, 'Customer');
            return false;
        }

        function OpenCustomerList(event) {
            var key = event.keyCode;
            if (key == 113 && key != 13) {
                document.getElementById('btnCustomer').click();
            }
        }

        function selectCustomer(id, hNo) {
            if (hNo == 1) {
                document.getElementById('txtCustACNo').value = id;
            } else
                document.getElementById('txtBeneficiaryID').value = id;
            __doPostBack('txtCustACNo', '');
        }

        function validate_Number(evnt) {
            var charCode = (evnt.which) ? evnt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 8 && charCode != 46 && charCode != 16 && charCode != 37 && charCode != 39 && charCode != 46 && charCode != 110 && charCode != 190 && charCode != 144 && (charCode < 96 || charCode > 105))
                return false;
            else
                return true;
        }

        function validate_Save() {
            var txtTranRefNo = document.getElementById('txtTranRefNo');
            var txtTranDate = document.getElementById('txtTranDate');
            var txtCustACNo = document.getElementById('txtCustACNo');
            var txtCustName = document.getElementById('txtCustName');
            var txtAntFC = document.getElementById('txtAntFC');
            var txtAmtINR = document.getElementById('txtAmtINR');           
            var txtBeneficiaryID = document.getElementById('txtBeneficiaryID');
            var txtBenName = document.getElementById('txtBenName');
            var txtBenAddress = document.getElementById('txtBenAddress');            
            var txtPuposeCode = document.getElementById('txtPuposeCode');
            var txtCurrency = document.getElementById('txtCurrency');
            var txtStateCode = document.getElementById('txtStateCode');
            var txtTranCountry = document.getElementById('txtTranCountry');
            var txtInstCountry = document.getElementById('txtInstCountry');
            var ddlTransactionType = document.getElementById('ddlTransactionType');
            var ddlInstType = document.getElementById('ddlInstType');
            var txtRemAdd = document.getElementById('txtRemAdd');
            var txtInstituteRefNo = document.getElementById('txtInstituteRefNo');
            var txtInstituteName = document.getElementById('txtInstituteName');
            var txtsecondLagTransCountry = document.getElementById('txtsecondLagTransCountry');
            if (txtTranRefNo.value == '') {
                alert("Enter Transaction Reference No");
                txtTranRefNo.focus();
                return false;
            }

            if (txtTranDate.value == '') {
                alert('Enter Transaction Date');
                txtTranDate.focus();
                return false;
            }

            if (txtCustACNo.value == '') {
                if (txtCustName.value == '') {
                    alert('Enter Remitter Name');
                    txtCustName.focus();
                    return false;
                }

                if (txtRemAdd.value == '') {
                    alert('Enter Remitter Address');
                    txtRemAdd.focus();
                    return false
                }

                if (txtRemAdd.value.length < 8) {
                    alert('Enter Remitter Address atleast 8 characters');
                    txtRemAdd.focus();
                    return false
                }
            }

            if (txtBeneficiaryID.value == '') {
                if (txtBenName.value == '') {
                    alert('Enter Beneficiary Name');
                    txtBenName.focus();
                    return false;
                }

                if (txtBenAddress.value == '') {
                    alert('Enter Beneficiary Address');
                    txtBenAddress.focus();
                    return false;
                }

                if (txtBenAddress.value.length < 8) {
                    alert('Enter Beneficiary Address atleast 8 characters ');
                    txtBenAddress.focus();
                    return false;
                }

            }

            if (txtPuposeCode.value == '') {
                alert('Enter Purpose Code');
                txtPuposeCode.focus();
                return false;
            }

            if (txtCurrency.value == '') {
                alert('Enter Currency');
                txtCurrency.focus();
                return false;
            }

            if (txtAntFC.value == '') {
                alert('Enter Amount In FC');
                txtAntFC.focus();
                return false;
            }

            if (txtAmtINR.value == '') {
                alert('Enter Amount In INR');
                txtAmtINR.focus();
                return false;
            }

            if (txtStateCode.value == '') {
                alert('Enter State Code');
                txtStateCode.focus();
                return false;
            }

            if (txtTranCountry.value == '') {
                alert('Enter Transaction Country');
                txtTranCountry.focus();
                return false;
            }

            if (txtTranCountry.value == '') {
                alert('Enter Transaction Country');
                txtTranCountry.focus();
                return false;
            }

            if (txtsecondLagTransCountry.value == '') {
                alert('Enter Second Lag Transaction Country');
                txtsecondLagTransCountry.focus();
                return false;
            }

            if (ddlTransactionType.value == '') {
                alert('Enter Transaction Type');
                ddlTransactionType.focus();
                return false;
            }

            if (ddlInstType.value == '') {
                alert('Enter Instrument Type');
                ddlInstType.focus();
                return false;
            }

            if (txtInstituteName.value == '') {
                alert('Enter Institute Name');
                txtInstituteName.focus();
                return false;
            }
            if (txtInstituteRefNo.value == '') {
                alert('Enter Institute Ref No');
                txtInstituteRefNo.focus();
                return false;
            }
            return true;
        }

        function calculate_AmtINR() {
            var txtAntFC = document.getElementById('txtAntFC');
            var txtAmtINR = document.getElementById('txtAmtINR');
            var txtExRate = document.getElementById('txtExRate');
            var amt = "";
            amt = (parseFloat(txtAntFC.value) * parseFloat(txtExRate.value));
            txtAmtINR.value = amt;
            if (txtExRate.value == '') {
                txtAmtINR.value = "0";
                txtAmtINR.focus();
            }
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


        function validRemAdd() {
            if (document.getElementById('txtRemAdd').value != '') {
                if (document.getElementById('txtRemAdd').value.length < 15) {
                    alert('Address must be of 15 Character');
                    document.getElementById('txtRemAdd').focus();
                    return false;
                }
            }
        }


        function validBenAdd() {
            if (document.getElementById('txtBenAddress').value != '') {
                if (document.getElementById('txtBenAddress').value.length < 15) {
                    alert('Address must be of 15 Character');
                    document.getElementById('txtBenAddress').focus();
                    return false;
                }
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
                                <span class="pageLabel" style="font-weight: bold">Transaction File Data Entry</span>
                            </td>
                            <td align="right" style="width: 50%">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="buttonDefault" ToolTip="Back"
                                    OnClick="btnBack_Click" TabIndex="29" />
                            </td>
                        </tr>
                        <%-- <tr>
                        <td align="left" style="width: 100%" valign="top" colspan="2">
                                <hr />
                                <input type="hidden" id="hdnCountryHelpNo" runat="server" />
                                <input type="hidden" id="hdnCountry" runat="server" />
                                <asp:Button ID="btnCountry" Style="display: none;" runat="server" OnClick="btnCountry_Click" />
                                </td>
                        </tr>--%>
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <%-- These section to handle audit log  --%>
                        <input type="hidden" runat="server" id="hdnTransRefNo" />
                        <input type="hidden" runat="server" id="hdnTransDate" />
                        <input type="hidden" runat="server" id="hdnRemitterID" />
                        <input type="hidden" runat="server" id="hdnRemitterName" />
                        <input type="hidden" runat="server" id="hdnBeneID" />
                        <input type="hidden" runat="server" id="hdnBeneName" />
                        <input type="hidden" runat="server" id="hdnPuposeCode" />
                        <input type="hidden" runat="server" id="hdnCurrency" />
                        <input type="hidden" runat="server" id="hdnAmtFC" />
                        <input type="hidden" runat="server" id="hdnINR" />
                        <input type="hidden" runat="server" id="hdnState" />
                        <input type="hidden" runat="server" id="hdnCountry" />
                        <input type="hidden" runat="server" id="hdnICountry" />
                        <input type="hidden" runat="server" id="hdntransType" />
                        <input type="hidden" runat="server" id="hdnInstType" />
                        <input type="hidden" runat="server" id="hdnRisk" />
                        <%-- End of These section to handle audit log  --%>
                        <tr>
                            <td align="right" valign="top" colspan="2">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Ref Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtADCode" runat="server" MaxLength="7" Width="60px" CssClass="textBox"
                                    Enabled="false" OnTextChanged="txtADCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblBranchName" runat="server" CssClass="elementLabel" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Month Year : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtYearMonth" runat="server" MaxLength="7" Width="50px" CssClass="textBox"
                                    Enabled="false"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="mdApplicationDate" runat="server" ClearMaskOnLostFocus="false"
                                    CultureDateFormat="MY" Enabled="true" ErrorTooltipEnabled="True" Mask="99/9999"
                                    MaskType="None" TargetControlID="txtYearMonth">
                                </ajaxToolkit:MaskedEditExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Sr No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtSrNo" runat="server" Width="20px" CssClass="textBox" OnTextChanged="txtSrNo_TextChanged"
                                    AutoPostBack="true" TabIndex="1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="elementLabel">Transaction Ref No : </span>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtTranRefNo" runat="server" CssClass="textBox" TabIndex="2" MaxLength="20"
                                    Width="80px"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp;<span class="mandatoryField">*</span> <span class="elementLabel">Transaction Date : </span>
                                <asp:TextBox ID="txtTranDate" runat="server" CssClass="textBox" Width="70px" TabIndex="3"
                                    MaxLength="10"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="ME_DOI" Mask="99/99/9999" MaskType="Date" runat="server"
                                    TargetControlID="txtTranDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                    CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                    CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                    CultureTimePlaceholder=":" Enabled="True">
                                </ajaxToolkit:MaskedEditExtender>
                                <asp:Button ID="btnDOI" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtTranDate" PopupButtonID="btnDOI" Enabled="True">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="ME_DOI"
                                    ValidationGroup="dtVal" ControlToValidate="txtTranDate" EmptyValueMessage="Enter Date Value"
                                    InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"
                                    Enabled="false"></ajaxToolkit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                 <span class="elementLabel">Remitter A/C No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCustACNo" runat="server" CssClass="textBox" MaxLength="20" TabIndex="4"
                                    OnTextChanged="txtCustACNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btnCustomer" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                    OnClientClick="return Customer('1');" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Name : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCustName" runat="server" CssClass="textBox" Width="100%" TabIndex="5"
                                    MaxLength="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Address : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemAdd" runat="server" CssClass="textBox" TabIndex="6" Width="100%"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="visibility:hidden" >
                            <td align="right">
                                <span class="elementLabel">Remitter A/C No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemAcNo" runat="server" CssClass="textBox" TabIndex="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Beneficiary A/C No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBeneficiaryID" runat="server" CssClass="textBox" TabIndex="8"
                                    MaxLength="20" OnTextChanged="txtBeneficiaryID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btnBenID" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                    OnClientClick="return Customer('2');" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Beneficiary Name : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBenName" runat="server" CssClass="textBox" Width="100%" TabIndex="9"
                                    MaxLength="80"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span> <span class="elementLabel">Beneficiary Address : </span>
                            </td>
                            <td align="left" height="30%">
                                <asp:TextBox ID="txtBenAddress" runat="server" CssClass="textBox" Width="100%" TextMode="MultiLine"
                                    TabIndex="10" MaxLength="225"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="visibility:hidden">
                            <td align="right">
                                <span class="elementLabel">Beneficiary A/C No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtBenACNo" runat="server" CssClass="textBox" TabIndex="11" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td colspan="2" align="left">
                                <asp:RadioButton ID="rbtExport" runat="server" CssClass="elementLabel" GroupName="Category"
                                    Text="Export" Checked="true" TabIndex="12" />
                                <asp:RadioButton ID="rbtImport" runat="server" CssClass="elementLabel" GroupName="Category"
                                    Text="Import" TabIndex="12" />
                                <asp:RadioButton ID="rbtInward" runat="server" CssClass="elementLabel" GroupName="Category"
                                    Text="Inward" TabIndex="12" />
                                <asp:RadioButton ID="rbtOutward" runat="server" CssClass="elementLabel" GroupName="Category"
                                    Text="Outward" TabIndex="12" />
                                <asp:RadioButton ID="rbtOther" runat="server" CssClass="elementLabel" GroupName="Category"
                                    Text="Other" TabIndex="12" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Purpose Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPuposeCode" runat="server" CssClass="textBox" TabIndex="13" MaxLength="5"
                                    Width="50px" OnTextChanged="txtPuposeCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btnPuposeCode" runat="server" CssClass="btnHelp_enabled" Width="16px"
                                    OnClientClick="return PuposeCode();" />
                                <asp:Label ID="lblPurpose" runat="server" CssClass="elementLabel" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Currency : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCurrency" CssClass="textBox" runat="server" TabIndex="14" MaxLength="3"
                                    AutoPostBack="true" OnTextChanged="txtCurrency_TextChanged" Width="40px"></asp:TextBox>
                                <asp:Button ID="btnCurrency" runat="server" CssClass="btnHelp_enabled" OnClientClick="return Currency();"
                                    Width="16px" />
                                <asp:Label ID="lblCurrency" runat="server" CssClass="elementLabel" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Amount In FC : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAntFC" runat="server" CssClass="textBox" MaxLength="12" TabIndex="15"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp; <span class="elementLabel">Exch Rate (INR) : </span>
                                <asp:TextBox ID="txtExRate" runat="server" CssClass="textBox" MaxLength="12" TabIndex="16"
                                    Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Amount In INR : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmtINR" runat="server" CssClass="textBox" MaxLength="12" TabIndex="17"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">State Code : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtStateCode" runat="server" AutoPostBack="true" CssClass="textBox"
                                    MaxLength="2" OnTextChanged="txtStateCode_TextChanged" TabIndex="18" Width="30px"></asp:TextBox>
                                <asp:Button ID="btnState" runat="server" CssClass="btnHelp_enabled" OnClientClick="return State();"
                                    Width="16px" />
                                <asp:Label ID="lblState" runat="server" CssClass="elementLabel" Width="250px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span> <span class="elementLabel">Transaction Country Code : </span>
                            </td>
                            <td align="left" colspan="2">
                                <asp:TextBox ID="txtTranCountry" runat="server" AutoPostBack="true" CssClass="textBox"
                                    MaxLength="2" OnTextChanged="txtTranCountry_TextChanged" TabIndex="19" Width="30px"></asp:TextBox>
                                <asp:Button ID="btnCountryList" runat="server" CssClass="btnHelp_enabled" OnClientClick="CountryHelp('1');"
                                    Width="16px" />
                                <asp:Label ID="lblCountryName" runat="server" CssClass="elementLabel" Width="150px"></asp:Label>
                                
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap>
                                <span class="mandatoryField">*</span> <span class="elementLabel">Instrument Country Code : </span>
                            </td>
                            <td align="left" style="white-space:nowrap">
                                <asp:TextBox ID="txtInstCountry" runat="server" AutoPostBack="true" CssClass="textBox"
                                    MaxLength="2" OnTextChanged="txtInstCountry_TextChanged" TabIndex="20" Width="30px"></asp:TextBox>
                                <asp:Button ID="btnInstrumentalC" runat="server" CssClass="btnHelp_enabled" OnClientClick="CountryHelp('2');"
                                    Width="16px" />
                                <asp:Label ID="lblcountryN" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
<span class="mandatoryField">*</span> <span class="elementLabel">Second Lag Transaction Country Code :</span>
                                <asp:TextBox ID="txtsecondLagTransCountry" runat="server" CssClass="textBox" AutoPostBack="true"
                                    OnTextChanged="txtsecondLagTransCountry_TextChanged" Width="30px" TabIndex="21"></asp:TextBox>
                                <asp:Button ID="btnSecondLagTrsnsCountry" runat="server" CssClass="btnHelp_enabled"
                                    OnClientClick="CountryHelp('3');" Width="16px" />
                                <asp:Label ID="lblSecondLagTrsnsCountry" runat="server" CssClass="elementLabel" Width="200px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Transaction Type : </span>
                            </td>
                            <td align="left" nowrap>
                                <asp:DropDownList ID="ddlTransactionType" runat="server" CssClass="dropdownList"
                                    TabIndex="22" Width="60">
                                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                    <asp:ListItem Text="P" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="R" Value="R"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; &nbsp; &nbsp; <span class="mandatoryField">*</span> <span class="elementLabel">Instrument Type : </span>
                                <asp:DropDownList ID="ddlInstType" runat="server" CssClass="dropdownList" TabIndex="23"
                                    Width="150px">
                                    <asp:ListItem Text="----Select----" Value=""></asp:ListItem>
                                    <asp:ListItem Text="A-Currency Note" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="B-Traveler Cheque" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="C-Demand Draft/Pay Order" Value="C"></asp:ListItem>
                                    <asp:ListItem Text="D-Money Order" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="E-Wire Transfer/TT" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="F-Money Transfer" Value="F"></asp:ListItem>
                                    <asp:ListItem Text="G-Credit Card" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="H-Debit Card" Value="H"></asp:ListItem>
                                    <asp:ListItem Text="I-Smart Card" Value="I"></asp:ListItem>
                                    <asp:ListItem Text="J-Prepaid Card" Value="J"></asp:ListItem>
                                    <asp:ListItem Text="K-Gift Card" Value="K"></asp:ListItem>
                                    <asp:ListItem Text="L-Cheque" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="Z-Others" Value="Z"></asp:ListItem>
                                    <asp:ListItem Text="X-Not categorised" Value="X"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Risk Category : </span>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlRiskCategory" runat="server" CssClass="dropdownList" TabIndex="24"
                                    Width="200px">
                                    <asp:ListItem Text="T3-Low Risk Transaction" Value="T3"></asp:ListItem>
                                    <asp:ListItem Text="T2-Medium Risk Transaction" Value="T2"></asp:ListItem>
                                    <asp:ListItem Text="T1-High Risk Transaction" Value="T1"></asp:ListItem>
                                    <asp:ListItem Text="XX - Not categorised" Value="XX"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Institute Name : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInstituteName" runat="server" CssClass="textBox" MaxLength="80"
                                    TabIndex="25" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="mandatoryField">*</span> <span class="elementLabel">Institute Ref No : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInstituteRefNo" runat="server" CssClass="textBox" MaxLength="20"
                                    TabIndex="26" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <span class="elementLabel">Remark : </span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="textBox" MaxLength="50" TabIndex="27"
                                    Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td align="left" height="50px">
                                <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" OnClick="btnSave_Click"
                                    TabIndex="28" Text="Save" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="buttonDefault" OnClick="btnCancel_Click"
                                    TabIndex="29" Text="Cancel" />
                                <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                    Style="visibility: hidden" Text="Button" UseSubmitBehavior="False" />
                            </td>
                        </tr>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
