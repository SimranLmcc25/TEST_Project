<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EBRC_Checker_ORM_DataEntry.aspx.cs" Inherits="EBR_EBRC_Checker_ORM_DataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LMCC-EBRC SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />

   <%-- <link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />--%>
   <%-- <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquerynew.min.js" language="javascript" type="text/javascript"></script>
    <link href="../Scripts/JueryUI.css" rel="Stylesheet" type="text/css" media="screen" />
    <script src="../Scripts/jquery-ui.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/MyJquery1.js" language="javascript" type="text/javascript"></script>
    <script src="../Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>--%>

 <%# "<script type='text/javascript' src='" + ResolveClientUrl("~/Scripts/jquery-1.4.1.min.js") + "'></script>" %>
<%# "<script src='" + ResolveClientUrl("~/Scripts/jquerynew.min.js") + "' language='javascript' type='text/javascript'></script>" %>
<%# "<link href='" + ResolveClientUrl("~/Scripts/JueryUI.css") + "' rel='stylesheet' type='text/css' media='screen' />" %>
<%# "<script src='" + ResolveClientUrl("~/Scripts/jquery-ui.js") + "' language='javascript' type='text/javascript'></script>" %>
<%# "<script src='" + ResolveClientUrl("~/Scripts/MyJquery1.js") + "' language='javascript' type='text/javascript'></script>" %>
<%# "<link href='" + ResolveClientUrl("~/Style/style_new.css") + "' rel='stylesheet' type='text/css' media='screen' />"%>
<%# "<link href='" + ResolveClientUrl("~/Style/Style.css") + "' rel='stylesheet' type='text/css' media='screen' />"%>



  <%--  <script language="javascript" type="text/javascript">
        function openPurposeCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_PurposeCodeLookUp2.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectPurpose(id, hNo) {
            var purposeid = document.getElementById('txtPurposeCode');
            if (hNo == '1') {
                purposeid.value = id;
                __doPostBack('purposeid', '');
                return true;
            }
        }
        //        function openCustAcNo(e, hNo) {
        //            var keycode;
        //            var txtBranchCode = document.getElementById('txtBranchCode');
        //            if (keycode == 113 || e == 'mouseClick') {
        //                open_popup('../TF_CustomerLookUp3.aspx?branchCode=' + txtBranchCode.value, 500, 500, 'CustAcNo');
        //                return false;
        //            }
        //            return true;
        //        }

        //        function selectCustomer(id) {
        //            var txtCustAcNo = document.getElementById('txtCustAcNo');
        //            txtCustAcNo.value = id;
        //            __doPostBack('txtCustAcNo');
        //            return true;
        //        }
        function Cust_Help() {
            var cusid = document.getElementById('txtCustAcNo').value
            open_popup('../TF_CustomerLookUp.aspx?CustID=' + cusid, 400, 400, "Cust");
            return true;
        }

        function selectCustomer(custid) {
            document.getElementById('txtCustAcNo').value = custid;
            document.getElementById('txtCustAcNo').focus();
            __doPostBack('txtCustAcNo', '');
        }

        function openCountryCode(e, hNo) {
            var keycode;
            if (keycode == 113 || e == 'mouseClick') {
                open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                return false;
            }
            return true;
        }

        function selectCountry(id, hNo) {
            var txtRemitterCountry = document.getElementById('txtRemitterCountry');
            var txtRemBankCountry = document.getElementById('txtRemBankCountry');

            if (hNo == '1') {
                txtRemitterCountry.value = id;
                __doPostBack('txtRemitterCountry', '');
                return true;
            }
            if (hNo == '2') {
                txtRemBankCountry.value = id;
                __doPostBack('txtRemBankCountry', '');
                return true;
            }
        }

        function ComputeAmtInINR() {
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            if (txtExchangeRate.value == '')
                txtExchangeRate.value = 0;

            document.getElementById('txtExchangeRate').value = parseFloat(txtExchangeRate.value).toFixed(10);
            var inramt = parseFloat((txtAmount.value) * (txtExchangeRate.value)).toFixed(0);
            if (isNaN(inramt) == false)
                txtAmtInINR.value = parseFloat(inramt).toFixed(2);
            else
                txtAmtInINR.value = 0;
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

        function validateSave() {
            //var txtdocSrNo = document.getElementById('txtdocSrNo');
            var txtDocDate = document.getElementById('txtDocDate');
            var txtCustAcNo = document.getElementById('txtCustAcNo');
            var txtPurposeCode = document.getElementById('txtPurposeCode');
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');
            var txtFIRCNo = document.getElementById('txtFIRCNo');
            var txtvalueDate = document.getElementById('txtvalueDate');
            var ddlCurrency = document.getElementById('ddlCurrency');
            if (ddlCurrency.value == '0') {
                alert('Select Currency.');
                ddlCurrency.focus();
                return false;
            }
            //            if (txtdocSrNo.value == '') {
            //                alert('Enter Document No.');
            //                txtdocSrNo.focus();
            //                return false;
            //            }
            if (txtDocDate.value == '') {
                alert('Enter Document Date.');
                txtDocDate.focus();
                return false;
            }
            if (txtCustAcNo.value == '') {
                alert('Enter Customer A/C No.');
                txtCustAcNo.focus();
                return false;
            }
            if (txtPurposeCode.value == '') {
                alert('Enter Purpose Code.');
                txtPurposeCode.focus();
                return false;
            }
            // if (txtAmount.value != '' || txtAmount.value != 0) {
            //  alert('Invalid Amount In FC');
            // txtAmount.focus();
            //return false;

            //  if (txtExchangeRate.value == '') {
            //      alert('Enter Exchange Rate.');
            //      txtExchangeRate.focus();
            //       return false;
            //    }
            // }
            if (txtvalueDate.value == '') {
                alert('Enter Value Date.');
                txtvalueDate.focus();
                return false;
            }
            //            if (txtAmtInINR.value == '' || txtAmtInINR.value == 0) {
            //                alert('Invalid Amount In INR');
            //                txtAmtInINR.focus();
            //                return false;
            //            }
        }
        //        function chkFIRCADCode() {
        //            var txtADCode = document.getElementById('txtADCode');
        //            if (txtADCode.value.length < 7) {
        //                alert('FIRC AD Code Should Be 7 Digit.');
        //                txtADCode.focus();
        //                return false;
        //            }
        //        }
    </script>
--%>

  
  <script language="javascript" type="text/javascript">
   function DialogAlert()
   {
  
    var divO = { id: "divO", css: { "width": "100%" } };
    var divA = {id: "dialog",};
    var para = {id: "Paragraph"};
    var table1 = { id: "tblDialog", css: { "width": "100%" } };
    var Para1 = $("<p>", para);
    var div1 = $("<div>", divO);
    var dialog = $("<div>", divA);
    var tablea = $("<table>", table1);
    var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

    $("body").append(div1);
    $("#divO").append(dialog);
    $("#dialog").append(Para1);
    $("#dialog").append(tablea);
    $("#tblDialog").append(tr1);

    var _ddlApproveReject = $("#ddlApproveReject");
    if (_ddlApproveReject.val() == '0') {
        alert('Select Reject or Approve.');
        _ddlApproveReject.focus();
        return false;
    }
    if (_ddlApproveReject.val() == '1') {
        $("#txtRejectReason").hide();
        $("#spnDialog").hide();
        $("#Paragraph").text("Do you want to approve transaction?");
        $("#lblReason").hide();
        $("#dialog").dialog({
            title: "Confirm",
            width: 400,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
               //icon: "ui-icon-heart",
                  click: function () {
                    $(this).dialog("close");
                    document.getElementById('btnSave').click();
                   //alert('This Trasnscation is Approve and json file created successfully.');
              }

            },

            {
                text: "No", //"✖"
               //   icon: "ui-icon-heart",
                   click: function () {
                    $(this).dialog("close");
                   // document.getElementById('btnSave').click();
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    if (_ddlApproveReject.val() == '2') {
        $("#txtRejectReason").val($("#hdnRejectReason").val());
        $("#txtRejectReason").show();
        $("#spnDialog").show();
        $("#Paragraph").text("Are you sure you want to reject transaction?");
        $("#lblReason").show();
        $("#dialog").dialog({
            title: "Confirm",
            width: 500,
            modal: true,
            closeOnEscape: true,
            dialogClass: "AlertJqueryDisplay",
            hide: { effect: "explode", duration: 400 },
            buttons: [
            {
                text: "Yes", //"✔"
             //icon: "ui-icon-heart",
                click: function () {
                    if ($("#txtRejectReason").val().trim() != '') {
                        $(this).dialog("close");
                        $("#hdnRejectReason").val($("#txtRejectReason").val());
                        $("#txtRejectReason").val('');
                        document.getElementById('btnSave').click();
                   
                    }
                    else  {
                     alert("Reject reason can not be blank.");
                        $("#txtRejectReason").focus();
                       return false;
                    }
                }
            },
            {
                text: "No", //"✖"
               // icon: "ui-icon-heart",
                click: function () {
                    $(this).dialog("close");
                    $("#txtRejectReason").remove();
                    $("#ddlApproveReject").val('-Select-')
                    return false;
                }
            }
            ]
        });
        $('.ui-dialog :button').blur();
    }
    return true;
}
    </script>

 <%--  <script language="javascript" type="text/javascript">

        function DialogAlert() {
            var divO = { id: "divO", css: { "width": "100%"} };
            var divA = { id: "dialog" };
            var para = { id: "Paragraph" };
            var table1 = { id: "tblDialog", css: { "width": "100%"} };
            var Para1 = $("<p>", para).text("Do you want to approve transaction?");
            var div1 = $("<div>", divO);
            var dialog = $("<div>", divA);
            var tablea = $("<table>", table1);
            var tr1 = "<tr><td width='20%' align='right'><span id='lblReason' class='elementLabel' style='font-weight:bold'>Reason: </span></td><td width='80%'><textarea id='txtRejectReason' cols='40' rows='4' maxlength='200'></textarea></td></tr><tr><td colspan='2' align='left'><span class='mandatoryField' id='spnDialog'>Max 200 char</span></td><tr>";

            $("body").append(div1);
            $("#divO").append(dialog);
            $("#dialog").append(Para1);
            $("#dialog").append(tablea);
            $("#tblDialog").append(tr1);

            var _ddlApproveReject = $("#ddlApproveReject");
            var _txtRejectReason = $("#txtRejectReason");

            if (_ddlApproveReject.val() == '0') {
                alert('Select Reject or Approve.');
                _ddlApproveReject.focus();
                return false;
            }
            if (_ddlApproveReject.val() == '1') {
                _txtRejectReason.hide();
                $("#spnDialog").hide();
                $("#lblReason").hide();
            }
            if (_ddlApproveReject.val() == '2') {
                _txtRejectReason.val($("#hdnRejectReason").val());
                _txtRejectReason.show();
                $("#spnDialog").show();
                $("#lblReason").show();
            }

            $("#dialog").dialog({
                title: "Confirm",
                width: _ddlApproveReject.val() == '1' ? 400 : 500,
                modal: true,
                closeOnEscape: true,
                dialogClass: "AlertJqueryDisplay",
                hide: { effect: "explode", duration: 400 },
                buttons: [
            {
                text: "Yes",
                click: function () {
                    if (_ddlApproveReject.val() == '2' && _txtRejectReason.val().trim() == '') {
                        alert("Reject reason cannot be blank.");
                        _txtRejectReason.focus();
                        return false;
                    }

                    $(this).dialog("close");
                    if (_ddlApproveReject.val() == '2') {
                        $("#hdnRejectReason").val(_txtRejectReason.val());
                        _txtRejectReason.val('');
                    }
                    document.getElementById('btnSave').click();
                }
            },
            {
                text: "No",
                click: function () {
                    $(this).dialog("close");
                    if (_ddlApproveReject.val() == '2') {
                        _txtRejectReason.remove();
                    }
                    _ddlApproveReject.val('0');
                    return false;
                }
            }
        ]
            });
            $('.ui-dialog :button').blur();

            return true;
        }
  </script>--%>

   
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div>
        <uc1:Menu ID="Menu1" runat="server" />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
        </asp:ScriptManager>
       <%-- <script language="javascript" type="text/javascript" src="../Scripts/InitEndRequest.js"></script>
        <script language="javascript" type="text/javascript" src="../Scripts/Enable_Disable_Opener.js"></script>--%>
         <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/InitEndRequest.js") %>'></script>
        <script src='<%=ResolveClientUrl("~/Scripts/Enable_Disable_Opener.js") %>' type="text/javascript"></script>
        <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
            <ProgressTemplate>
                <div id="progressBackgroundMain" class="progressBackground">
                    <div id="processMessage" class="progressimageposition">
                        <%--<img src="../Images/ajax-loader.gif" style="border: 0px" alt="" />--%>
                         <img src='<%=ResolveClientUrl("~/Images/ajax-loader.gif") %>' style="border: 0px"
                        alt="" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
          <Triggers>
                <asp:PostBackTrigger ControlID="btnBack" />
            </Triggers>
            <ContentTemplate>
                <table width="100%" cellpadding="1">
                    <tr>
                        <td colspan="4" align="left">
                            <span class="pageLabel"><strong>EBRC ORM Data Entry View- Checker </strong></span>
                        </td>

                        <td align="right" style="width: 50%" valign="bottom">
                                <asp:Button ID="btnBack" CssClass="buttonDefault" Text="Back" runat="server" onclick="btnBack_Click" />
                            </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                       <td align="left" valign="top" colspan="4">
                                <asp:Label ID="labelMessage" runat="server" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnRejectReason" runat="server" />
                            </td>
                        </tr>
                      
                    <tr>
                        <td colspan="4" valign="top" align="left">
                            <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td style="text-align: right" width="5%">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Branch ID : </span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="textBox" Style="width: 40px"
                                Enabled="false" TabIndex="1"></asp:TextBox>
                            <asp:Label ID="lblBranchName" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">ORM No : </span>
                        </td>
                        <td align="left" width="3%">
                            <asp:TextBox ID="txtormNo" runat="server" CssClass="textBox" AutoPostBack="true"
                                Width="260px" Enabled="false"></asp:TextBox>
                            
                        </td>
                        <td style="text-align: right; width: 5%; white-space: nowrap;">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Payment Date :
                            </span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtpaymentDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtpaymentDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtpaymentDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtpaymentDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
          
                    <tr>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Purpose Code :
                            </span>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtPurposeCode" runat="server" CssClass="textBox" Style="width: 50px"
                                AutoPostBack="true" MaxLength="8" TabIndex="6"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">BeneficiaryName:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtbeneficiaryName" runat="server" Width="280px" CssClass="textBox"
                                MaxLength="50" TabIndex="7"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap;">
                            <span class="mandatoryField">*</span><span class="elementLabel">BeneficiaryCountry:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtbeneficiaryCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                AutoPostBack="true"  TabIndex="8"></asp:TextBox>
                            <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1" />
                            &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                      <td style="text-align: right">
                            <span class="elementLabel">IEC Code : </span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtIECcode" runat="server" CssClass="textBoxRight" 
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>
                        <%-- <td style="text-align: right">
                            <span class="elementLabel">ADCode : </span>
                        </td>--%>
                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">AD Code :</span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtADCode" runat="server" CssClass="textBoxRight" 
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>
                    </tr>

   
                    <tr>
                       <%-- <td style="text-align: right">
                            <span class="elementLabel">ORN FCC : </span>
                        </td>--%>
                         <td style="text-align: right" class="style3">
                            <span class="mandatoryField">*</span> <span class="elementLabel">ORN FC : </span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtornFCC" runat="server" CssClass="textBoxRight" 
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>

                     <%--   <td style="text-align: right">
                            <span class="elementLabel">ORNFCAmount : </span>
                        </td>--%>
                        <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span> <span class="elementLabel">ORNFCAmount :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textBoxRight" Style="width: 120px"
                                 TabIndex="14"></asp:TextBox>
                        </td>
                    </tr>
                             <tr>
                        <%--<td style="text-align: right">
                            <span class="elementLabel">BankTransactionId: </span>
                        </td>--%>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span><span class="elementLabel">BankTransctionId:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBankuniqueTextId" runat="server" CssClass="textBoxRight" 
                                TabIndex="16" Style="width: 220px" 
                                ontextchanged="txtBankuniqueTextId_TextChanged"></asp:TextBox>
                        </td>
                        <%--<td style="text-align: right">
                            <span class="elementLabel">ORMIssueDate : </span>
                        </td>--%>
                         <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">ORMIssueDate :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtORMissuedt" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="17"></asp:TextBox>
                                   <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtORMissuedt" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button7" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtORMissuedt" PopupButtonID="Button6" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                       
                        <td style="text-align: right">
                            <span class="elementLabel">INRPayableAmount: </span>
                        </td> 
                       
                        <td align="left">
                            <asp:TextBox ID="txtornINRAmt" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="17"></asp:TextBox>
                        </td>
                        <%--<td style="text-align: right">
                            <span class="elementLabel">IFSC Code : </span>
                        </td>--%>
                         <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">IFSC Code :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIFSCcode" runat="server" CssClass="textBox" Style="width: 100px"
                                TabIndex="17"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <%--<td style="text-align: right">
                            <span class="elementLabel">PanNumber : </span>
                        </td>--%>
                         <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">PanNumber :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="textBoxRight" 
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>

                         <%--<td align="right" nowrap>
                           <span class="elementLabel">Reference IRM :</span>
                            </td>--%>
                             <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">Reference IRM :</span>
                        </td>
                          
                                <td align="left">
                              <asp:TextBox ID="txtrefIRM" runat="server" CssClass="textBox" Style="width: 200px"
                                TabIndex="17"></asp:TextBox>
                             </td>
                    
                    </tr>
             
                      <tr>
                         <%-- <td align="right">
                              <span class="elementLabel">Approve / Reject :</span>
                          </td>--%>
                          <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">Approve / Reject :</span>
                        </td>
                          <td align="left" colspan="1">
                              <asp:DropDownList ID="ddlApproveReject" runat="server" CssClass="dropdownList" 
                                  TabIndex="256" 
                                  AutoPostBack="true">
                                  <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="Approve" Value="1"></asp:ListItem>
                                  <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                              </asp:DropDownList>
                               <asp:Button ID="btnSave" runat="server" 
                             onclick="btnSave_Click" Style="visibility: hidden" TabIndex="256" Text="Save" 
                                  ToolTip="Save"/>
                        <%--  </td>
                            <td align="right" nowrap>
                         <span class="elementLabel">ORM Status :</span>
                         </td> --%>
                             <td style="text-align: right;white-space:nowrap;" >
                          <span class="mandatoryField">*</span><span class="elementLabel">ORM Status :</span>
                        </td>
                           <td align="left">
                              <asp:DropDownList ID="ddlORMstatus" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                    <asp:ListItem  Value="0">-Select-</asp:ListItem>
                                     <asp:ListItem Value="1">F</asp:ListItem>
                                     <asp:ListItem Value="2">A</asp:ListItem>
                                     <asp:ListItem Value="3">C</asp:ListItem>
                          </asp:DropDownList>                  
                     </td>
                      
                     
                      </tr>
                </tr>
                    
                </table>
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
  
</body>
</html>
