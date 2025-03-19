<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EBRC_Maker_DataEntry.aspx.cs"
    Inherits="EBR_EBRC_Maker_DataEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LMCC-EBRC SYSTEM</title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />

    <%--<link href="../Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Style/style_new.css" rel="Stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
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

 <script language="javascript" type="text/javascript">

         function openPurposeCode(e, hNo) {
             // var encodedHNo = encodeURIComponent(hNo);
             var keycode;
             if (keycode == 113 || e == 'mouseClick') {
                 // open_popup('../TF_PurposeCodeLookUp2.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                //open_popup(getRootURL() + 'GqKEieT5FcdkRVVIniYfHA==' + hNo, 500, 500, 'purposeid');
                 open_popup(getRootURL() + 'GqKEieT5FcdkRVVIniYfHA/' + hNo, 500, 500, 'purposeid');
                 // var url = getRootURL() + 'Mvm3dLe9kIaC0KNSTuy2Gc4h/upEu+J4Q+12saDLsYk=' + '&hNo=' + encodedHNo;
                 //open_popup(url, 500, 500, 'purposeid');
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
                 //open_popup('../TF_CountryLookUp1.aspx?hNo=' + hNo, 500, 500, 'purposeid');
                 open_popup(getRootURL() + 'qfs8xAfzdKvFL8ROgVVDaXumKlQsn4Fa3JDMP0jGPg/' + hNo, 500, 500, 'purposeid');

                 return false;
             }
             return true;
         }

         function selectCountry(id, hNo) {
             var txtRemitterCountry = document.getElementById('txtRemitterCountry');
             var txtRemitterCountry = document.getElementById('txtRemitterCountry');

             if (hNo == '1') {
                 txtRemitterCountry.value = id;
                 __doPostBack('txtRemitterCountry', '');
                 return true;
             }
             if (hNo == '2') {
                 txtRemitterCountry.value = id;
                 __doPostBack('txtRemitterCountry', '');
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
 <script type="text/javascript">
      function functionvalidateSave() {
          var txtremitdate = document.getElementById('txtDocDate');
          var txtissuedate = document.getElementById('txtirmIssueDate');
          var txtirmnumber = document.getElementById('txtDocNo');
          var txtpurposecode = document.getElementById('txtPurposeCode');
          var txtremmitername = document.getElementById('txtRemitterName');
          var txtieccode = document.getElementById('txtIECcode');
          var txtremmiteradcode = document.getElementById('txtRemittanceADCode');
          var txtremiiterfc = document.getElementById('txtremittanceFCC');
          var txtremitcountry = document.getElementById('txtRemitterCountry');
          var txtremiiterfcamount = document.getElementById('txtAmount');
          var txtinramount = document.getElementById('txtinrCreditAmount');
          var txtifsccode = document.getElementById('txtIFSCcode');
          var txtpannumber = document.getElementById('txtPanNumber');
          var txtbkrefnumber = document.getElementById('txtbanrefnumber');
          var txtbkacnumber = document.getElementById('txtbanaccnumber');
          var ddlirmstatus = document.getElementById('ddlIRMstatus');
          //irm status
          if (ddlirmstatus.value == '0') {
              alert('Select IRM status.');
              ddlirmstatus.focus();
              return false;
          }

          //bank ac no
          if (txtbkacnumber.value == '') {
              alert('Enter Bank Account No.');
              txtbkacnumber.focus();
              return false;
          }

          var count = txtbkacnumber.value.length;
          if (count > 25) {
              alert('Invalid Bank Account No.');
              txtbkacnumber.focus();
              return false;
          }

          //bank ref no
//          if (txtbkrefnumber.value == '') {
//              alert('Enter Bank Reference No.');
//              txtbkrefnumber.focus();
//              return false;
//          }

          if (txtbkrefnumber.value !== '' && txtbkrefnumber.value.length >20) {
              alert('Invalid Bank Reference No.');
              txtbkrefnumber.focus();
              return false;
          }

          if (txtieccode.value !== '' && txtieccode.value.length < 10) {
              alert('Invalid IEcode.');
              txtieccode.focus();
              return false;
          }

          //Pan no
          if (txtpannumber.value == '') {
              alert('Enter Pan No.');
              txtpannumber.focus();
              return false;
          }

          var panRegex = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;
          if (!panRegex.test(txtpannumber.value)) {
              alert('Invalid Pan No.');
              txtpannumber.focus();
              return false;
          }

          //AD Code
          if (txtremmiteradcode.value.length < 7) {
              alert('Invalid Remittance AD Code.');
              txtremmiteradcode.focus();
              return false;
          }

          //IFSC Code
          if (txtifsccode.value == '') {
              alert('Enter IFSC Code.');
              txtifsccode.focus();
              return false;
          }

          var count = txtifsccode.value.length;
          if (count < 11) {
              alert('Invalid IFSC Code.');
              txtifsccode.focus();
              return false;
          }

          //INR Amt
          if (txtinramount.value == '') {
              alert('Enter INR Amount.');
              txtinramount.focus();
              return false;
          }

          // remmitance date
          var pattern = /^(\d{2})\/(\d{2})\/(\d{4})$/;
          var match = txtremitdate.value.match(pattern);
          var day = parseInt(match[1]);
          var month = parseInt(match[2]) - 1; // Months are zero-based (0-11)
          var year = parseInt(match[3]);
          // Create a Date object from the input date
          var inputDate = new Date(year, month, day);

          var match2 = txtissuedate.value.match(pattern);
          var day1 = parseInt(match2[1]);
          var month1 = parseInt(match2[2]) - 1; // Months are zero-based (0-11)
          var year1 = parseInt(match2[3]);
          // Create a Date object from the input date
          var inputDate2 = new Date(year1, month1, day1);


          // Get the current date
          var currentDate = new Date();
          if (inputDate > currentDate) {
              alert('Remmittance date should not be greater than Current date.');
              txtremitdate.focus();
              return false;
          }

          if (inputDate > inputDate2) {
              alert('Remmittance date should not be greater than IRM Issue date.');
              txtremitdate.focus();
              return false;
          }



          //irm issue date
          if (inputDate2 > currentDate) {
              alert('IRM issue date should not be greater than current date.');
              txtissuedate.focus();
              return false;
          }

          //Remmiter FC Amt
          if (txtremiiterfcamount.value == '') {
              alert('Enter Remmiter FC Amount.');
              txtremiiterfcamount.focus();
              return false;
          }

          //Remmiter FC
          if (txtremiiterfc.value == '') {
              alert('Enter Remmiter FC .');
              txtremiiterfc.focus();
              return false;
          }

          if (txtremiiterfc.value.length < 3) {
              alert('Invalid Remmiter FC .');
              txtremiiterfc.focus();
              return false;
          }

          //Remmitter Country
          if (txtremitcountry.value == '') {
              alert('Enter Remmiter Country .');
              txtremitcountry.focus();
              return false;
          }

          if (txtremitcountry.value.length < 2) {
              alert('Invalid Remmiter Country .');
              txtremitcountry.focus();
              return false;
          }

          //IRM No
          if (txtirmnumber.value == '') {
              alert('Enter IRM No .');
              txtirmnumber.focus();
              return false;
          }



          //Remmiter Name
          if (txtremmitername.value == '') {
              alert('Enter Remmiter Name .');
              txtremmitername.focus();
              return false;
          }

          //Purpose Code
          if (txtpurposecode.value == '') {
              alert('Enter Purpose Code.');
              txtpurposecode.focus();
              return false;
          }

          var txtpurposecode = document.getElementById('txtPurposeCode');
          if (txtpurposecode.value != '') {
              if (txtpurposecode.value != ('P0101') && txtpurposecode.value != ('P0102') && txtpurposecode.value != ('P0103') && txtpurposecode.value != ('P0104') && txtpurposecode.value != ('P0108') && txtpurposecode.value != ('P0109')
                && txtpurposecode.value != ('P0201') && txtpurposecode.value != ('P0202') && txtpurposecode.value != ('P0205') && txtpurposecode.value != ('P0207') && txtpurposecode.value != ('P0208') && txtpurposecode.value != ('P0211') && txtpurposecode.value != ('P0214') && txtpurposecode.value != ('P0215') && txtpurposecode.value != ('P0216') && txtpurposecode.value != ('P0217') && txtpurposecode.value != ('P0218') && txtpurposecode.value != ('P0219') && txtpurposecode.value != ('P0220') && txtpurposecode.value != ('P0221') && txtpurposecode.value != ('P0222') && txtpurposecode.value != ('P0223') && txtpurposecode.value != ('P0224') && txtpurposecode.value != ('P0225') && txtpurposecode.value != ('P0226')
                && txtpurposecode.value != ('P0301') && txtpurposecode.value != ('P0302') && txtpurposecode.value != ('P0304') && txtpurposecode.value != ('P0305') && txtpurposecode.value != ('P0306') && txtpurposecode.value != ('P0308')
                && txtpurposecode.value != ('P0501') && txtpurposecode.value != ('P0502')
                && txtpurposecode.value != ('P0601') && txtpurposecode.value != ('P0602') && txtpurposecode.value != ('P0603') && txtpurposecode.value != ('P0605') && txtpurposecode.value != ('P0607') && txtpurposecode.value != ('P0608') && txtpurposecode.value != ('P0609') && txtpurposecode.value != ('P0610') && txtpurposecode.value != ('P0611') && txtpurposecode.value != ('P0612')
                && txtpurposecode.value != ('P0701') && txtpurposecode.value != ('P0702') && txtpurposecode.value != ('P0703')
                && txtpurposecode.value != ('P0801') && txtpurposecode.value != ('P0802') && txtpurposecode.value != ('P0803') && txtpurposecode.value != ('P0804') && txtpurposecode.value != ('P0805') && txtpurposecode.value != ('P0806') && txtpurposecode.value != ('P0807') && txtpurposecode.value != ('P0808') && txtpurposecode.value != ('P0809')
                && txtpurposecode.value != ('P0901') && txtpurposecode.value != ('P0902')
                && txtpurposecode.value != ('P1002') && txtpurposecode.value != ('P1003') && txtpurposecode.value != ('P1004') && txtpurposecode.value != ('P1005') && txtpurposecode.value != ('P1006') && txtpurposecode.value != ('P1007') && txtpurposecode.value != ('P1008') && txtpurposecode.value != ('P1009') && txtpurposecode.value != ('P1010') && txtpurposecode.value != ('P1011') && txtpurposecode.value != ('P1013') && txtpurposecode.value != ('P1014') && txtpurposecode.value != ('P1015') && txtpurposecode.value != ('P1016') && txtpurposecode.value != ('P1017') && txtpurposecode.value != ('P1018') && txtpurposecode.value != ('P1019') && txtpurposecode.value != ('P1020') && txtpurposecode.value != ('P1021') && txtpurposecode.value != ('P1022') && txtpurposecode.value != ('P1099')
                && txtpurposecode.value != ('P1101') && txtpurposecode.value != ('P1103') && txtpurposecode.value != ('P1104') && txtpurposecode.value != ('P1105') && txtpurposecode.value != ('P1106') && txtpurposecode.value != ('P1107') && txtpurposecode.value != ('P1108') && txtpurposecode.value != ('P1109')
                && txtpurposecode.value != ('P1201') && txtpurposecode.value != ('P1203')
                && txtpurposecode.value != ('P1505')
                && txtpurposecode.value != ('P1601') && txtpurposecode.value != ('P1602')
                && txtpurposecode.value != ('P1701')) {
                  alert('Invalid Purpose Code.');
                  txtpurposecode.focus();
                  return false;
              }

          }


      }

      function validateAmt() {
          var irmfcamt = document.getElementById('txtAmount').value;
          if (irmfcamt == '') {
              document.getElementById('txtAmount').value = '';
              return false;
          }
          else {
              var f = (Math.round(irmfcamt * 100) / 100);
              document.getElementById('txtAmount').value = formatAmt(f);
          }

          var INRCCREADITAMT = document.getElementById('txtinrCreditAmount').value;
          if (INRCCREADITAMT == '') {
              document.getElementById('txtinrCreditAmount').value = '';
              return false;
          }
          else {
              var f = (Math.round(INRCCREADITAMT * 100) / 100);
              document.getElementById('txtinrCreditAmount').value = formatAmt(f);
          }


      }

      function CheckLength() {
          var txtirmno = document.getElementById('txtDocNo');
          if (txtirmno.value.length > 20) {
              alert('IRM No Should Not Be greater Than 20 Characters.');
          }
      }
    </script>

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
                            <span class="pageLabel"><strong>EBRC IRM Data Entry View - Maker</strong></span>
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
                       <td align="left" style="width: 50%;" valign="top" colspan="4">
                                <asp:Label ID="labelMessage" runat="server"  Font-Bold="True" CssClass="mandatoryField"></asp:Label>
                                <input type="hidden" id="hdnRejectReason" runat="server" />
                                <br />
                            </td>
                        </tr>
                      
                    <tr>
                        <td colspan="4" align="left">
                            <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
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
                            <span class="mandatoryField">*</span> <span class="elementLabel">IRM No : </span>
                        </td>
                        <td align="left" width="3%">
                            <asp:TextBox ID="txtDocNo" runat="server" CssClass="textBox" AutoPostBack="true"
                                Width="260px" Enabled="false"></asp:TextBox>
                            <%--<asp:TextBox ID="txtdocSrNo" Style="width: 45px; height: 17px;" runat="server" CssClass="textBox"
                                MaxLength="6" OnTextChanged="txtdocSrNo_TextChanged" AutoPostBack="true" 
                                TabIndex="2"> </asp:TextBox>--%>
                        </td>
                        <td style="text-align: right; width: 5%; white-space: nowrap;">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Remittance Date :
                            </span>
                        </td>
                        <td align="left" nowrap>
                            <asp:TextBox ID="txtDocDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="3"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="mdDocdate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtDocDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="btncalendar_DocDate" runat="server" CssClass="btncalendar_enabled"
                                TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="calendarFromDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtDocDate" PopupButtonID="btncalendar_DocDate" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="mdDocdate"
                                ValidationGroup="dtVal" ControlToValidate="txtDocDate" EmptyValueMessage="Enter Date Value"
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
                                AutoPostBack="true" MaxLength="8" TabIndex="6" 
                                ontextchanged="txtPurposeCode_TextChanged"></asp:TextBox>
                          <asp:Button ID="Button1" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                OnClientClick="return openPurposeCode('mouseClick','1');" />
                            &nbsp;<asp:Label ID="lblpurposeCode" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                       <%-- <td align="left">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Swift Code :</span>
                            <asp:TextBox ID="txtswift" runat="server" CssClass="textBox" MaxLength="30" Width="200px"
                                TabIndex="6"></asp:TextBox>
                        </td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Name :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemitterName" runat="server" Width="280px" CssClass="textBox"
                                MaxLength="50" TabIndex="7"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap;">
                            <span class="mandatoryField">*</span> <span class="elementLabel">Remitter Country :</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemitterCountry" runat="server" CssClass="textBox" Style="width: 30px"
                                AutoPostBack="true" MaxLength="2" TabIndex="8" 
                                ontextchanged="txtRemitterCountry_TextChanged"></asp:TextBox>
                           <asp:Button ID="Button4" runat="server" CssClass="btnHelp_enabled" TabIndex="-1"
                                OnClientClick="return openCountryCode('mouseClick','1');" />
                            &nbsp;<asp:Label ID="lblCountryDesc" runat="server" CssClass="elementLabel"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                      <td style="text-align: right">
                            <span class="elementLabel">IEC Code : </span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtIECcode" runat="server" CssClass="textBoxRight" MaxLength="10"
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>
                         <td style="text-align: right">
                          <span class="mandatoryField">*</span>   <span class="elementLabel">RemittanceADCode : </span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtRemittanceADCode" runat="server" CssClass="textBoxRight" MaxLength="50"
                                TabIndex="16" Style="width: 120px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right">
                          <span class="mandatoryField">*</span>   <span class="elementLabel">Remittance FC : </span>
                        </td>
                         <td align="left">
                            <asp:TextBox ID="txtremittanceFCC" runat="server" CssClass="textBoxRight" MaxLength="20"
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>

                        <td style="text-align: right">
                         <span class="mandatoryField">*</span><span class="elementLabel">RemittanceFCCAmount:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAmount" runat="server" CssClass="textBoxRight" Style="width: 120px"
                                MaxLength="20" TabIndex="14"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                     <%--   <td style="text-align: right">
                            <span class="elementLabel">Exchange Rate : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="textBoxRight" TabIndex="15"
                                Style="width: 120px"></asp:TextBox>--%>
                        </td>
                    </tr>
               <%--     <tr>
                        <td style="text-align: right">
                            <span class="elementLabel">Amount In INR : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAmtInINR" runat="server" CssClass="textBoxRight" MaxLength="20"
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>
                    </tr>--%>
                <%--         <tr>
                        <td style="text-align: right">
                            <span class="elementLabel">FIRC NO : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFIRCNo" runat="server" CssClass="textBox" MaxLength="15" TabIndex="17"
                                Style="width: 120px"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <span class="elementLabel">FIRC Date : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFIRCDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="17"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="meFircDate" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtFIRCDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button2" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtFIRCDate" PopupButtonID="Button2" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meFircDate"
                                ValidationGroup="dtVal" ControlToValidate="txtFIRCDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                            <span class="elementLabel">FIRC AD Code : </span>
                            <asp:TextBox ID="txtADCode" runat="server" CssClass="textBox" Style="width: 55px"
                                MaxLength="7" TabIndex="19"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span class="elementLabel">Remarks : </span>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtremarks" runat="server" CssClass="textBox" MaxLength="100" Width="500px"
                                TabIndex="19"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right">
                           <span class="mandatoryField">*</span>  <span class="elementLabel">BankTransactionId: </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtBankuniqueTextId" runat="server" CssClass="textBoxRight" MaxLength="25"
                                TabIndex="16" Enabled="false" Style="width: 220px"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                           <span class="mandatoryField">*</span>  <span class="elementLabel">IRMIssueDate : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtirmIssueDate" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="17"></asp:TextBox>
                                   <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                runat="server" TargetControlID="txtirmIssueDate" ErrorTooltipEnabled="True" CultureName="en-GB"
                                CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="£" CultureDateFormat="DMY"
                                CultureDatePlaceholder="/" CultureDecimalPlaceholder="." CultureThousandsPlaceholder=","
                                CultureTimePlaceholder=":" Enabled="True">
                            </ajaxToolkit:MaskedEditExtender>
                            <asp:Button ID="Button7" runat="server" CssClass="btncalendar_enabled" TabIndex="-1" />
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtirmIssueDate" PopupButtonID="Button7" Enabled="True">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2"
                                ValidationGroup="dtVal" ControlToValidate="txtirmIssueDate" EmptyValueMessage="Enter Date Value"
                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*" ErrorMessage="*"></ajaxToolkit:MaskedEditValidator>
                        </td>
                    </tr>
                    <tr>
                        <%--<td style="text-align: right">
                            <span class="elementLabel">Remittance FC : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtremittanceFCC" runat="server" CssClass="textBoxRight" MaxLength="20"
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>--%>
                        <td style="text-align: right">
                            <span class="elementLabel">INRCreditAmount : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtinrCreditAmount" runat="server" CssClass="textBox" Style="width: 70px"
                                TabIndex="17"></asp:TextBox>
                        </td>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span><span class="elementLabel">IFSC Code : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIFSCcode" runat="server" CssClass="textBox" Style="width: 120px"
                                TabIndex="17" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <span class="mandatoryField">*</span> <span class="elementLabel">PanNumber : </span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPanNumber" runat="server" CssClass="textBoxRight" MaxLength="20"
                                TabIndex="16" Style="width: 120px"></asp:TextBox>
                        </td>

                         <td align="right" nowrap>
                           <span class="elementLabel">Bank Reference number :</span>
                            </td>
                          
                                <td align="left">
                              <asp:TextBox ID="txtbanrefnumber" runat="server" CssClass="textBox" Style="width: 200px"
                                TabIndex="17"></asp:TextBox>
                             </td>
                            
                    </tr>
                         <tr>
                       
                        <td align="right" nowrap>
                         <span class="mandatoryField">*</span> <span class="elementLabel">Bank Account Number :</span>
                         </td>
                         <td align="left">
                          
                              <asp:TextBox ID="txtbanaccnumber" runat="server" CssClass="textBox" Style="width: 170px"
                                 TabIndex="17"></asp:TextBox>       
                     </td>

                      <td align="right" nowrap>
                           <span class="mandatoryField">*</span><span class="elementLabel">IRM Status :</span>
                            </td>
                            <td align="left">
                                 &nbsp;<asp:DropDownList ID="ddlIRMstatus" CssClass="dropdownList" AutoPostBack="true"
                                    runat="server" TabIndex="1">
                                     <asp:ListItem Value="0">-Select-</asp:ListItem>
                                     <asp:ListItem Value="1">F</asp:ListItem>
                                     <asp:ListItem Value="2">A</asp:ListItem>
                                     <asp:ListItem Value="3">C</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                     
                    </tr>
                 
                </tr>
                  <tr>
                <td></td>
                   <td align="left" colspan="3">
                    <asp:Button ID="btnSave" runat="server" Text="Send To Checker" 
                        CssClass="buttonDefault" onclick="btnSave_Click" />
                  &nbsp;
          
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                        CssClass="buttonDefault"/>
                </td>
                </tr>
                    
                </table>
              
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <%--<script language="javascript" type="text/javascript">
        window.onload = function () {
            var txtAmount = document.getElementById('txtAmount');
            var txtExchangeRate = document.getElementById('txtExchangeRate');
            var txtAmtInINR = document.getElementById('txtAmtInINR');

            if (txtAmount.value == '')
                txtAmount.value = 0;
            txtAmount.value = parseFloat(txtAmount.value).toFixed(2);

            if (txtExchangeRate.value == '')
                txtExchangeRate.value = 0;
            txtExchangeRate.value = parseFloat(txtExchangeRate.value).toFixed(10);

            if (txtAmtInINR.value == '')
                txtAmtInINR.value = 0;
            txtAmtInINR.value = parseFloat(txtAmtInINR.value).toFixed(2);
        } //function
    </script>--%>
</body>


</html>
