<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ebrc_GenerateXML.aspx.cs" Inherits="EBR_Ebrc_GenerateXML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Menu/Menu.ascx" TagName="Menu" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link id="Link1" runat="server" rel="shortcut icon" href="~/Images/favicon.ico" type="image/x-icon" />
    <link id="Link2" runat="server" rel="icon" href="~/Images/favicon.ico" type="image/ico" />
    <link href="~/Style/Style.css" rel="stylesheet" type="text/css" />
     <link href="~/Style/style_new.css" rel="Stylesheet" type="text/css" media="screen">
    <script src="~/Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
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
            function Custhelp() {
                var fromdate = document.getElementById('txtfromDate');
                var toDate = document.getElementById('txtToDate');

                if (fromdate.value == '') {
                    alert('Select From Date.');
                    fromdate.focus();
                    return false;
                }
                if (toDate.value == '') {
                    alert('Select To Date.');
                    toDate.focus();
                    return false;
                }
                popup = window.open('../TF_CustomerLookUp1.aspx', 'helpCustId', 'height=520,  width=520,status= no, resizable= no, scrollbars=yes, toolbar=no,location=center,menubar=no, top=100, left=300');
                common = "helpCustId"
                return false;
            }
            function CustId(event) {
                var key = event.keyCode;
                if (key == 113 && key != 13) {
                    document.getElementById('btCustList').click();
                }
            }
            function sss() {
                var s = popup.document.getElementById('txtcell1').value;

                if (common == "helpCustId") {
                    document.getElementById('txtCustomer').value = s;
                }
            }

        </script>
        <script language="javascript" type="text/javascript">
            function toogleDisplay() {
                var rdbAllCustomer = document.getElementById('rdbAllCustomer');
                var rdbSelectedCustomer = document.getElementById('rdbSelectedCustomer');
                var divCutomer = document.getElementById('divCutomer');
                var txtCustomer = document.getElementById('txtCustomer');
                var lblCustomerName = document.getElementById('lblCustomerName');
                var rdbFresh = document.getElementById('rdbFresh');
                var rdbCancel = document.getElementById('rdbCancel');
                 

                if (rdbAllCustomer.checked == true) {

                    divCutomer.style.display = 'none';
                    txtCustomer.value = '';
                    lblCustomerName.innerHTML = '';
                  
                }
                else {
                    
                    divCutomer.style.display = 'block';
                    
                }
                return true;
            }
            function Generate() {
                var ddlBranch = document.getElementById('ddlBranch').value;
                var txtfromDate = document.getElementById('txtfromDate');
                var txtToDate = document.getElementById('txtToDate');

                var rdbAllCustomer = document.getElementById('rdbAllCustomer');
                var rdbSelectedCustomer = document.getElementById('rdbSelectedCustomer');
                var txtCustomer = document.getElementById('txtCustomer');
                var Report = document.getElementById("PageHeader").innerHTML;

                
                if (ddlBranch == 0) {
                    alert('Select Branch Name');
                    ddlBranch.focus();
                    return false;
                }
                if (txtfromDate.value == '') {
                    alert('Select From Date.');
                    txtfromDate.focus();
                    return false;
                }
                if (txtToDate.value == '') {
                    alert('Select To Date.');
                    txtToDate.focus();
                    return false;
                }
                if (document.getElementById('rdbSelectedCustomer').checked == true) {
                    if (txtCustomer.value == '') {
                        alert('Select Customer A/C No.')
                        txtCustomer.focus();
                        return false;
                    }
                }

//                var Customer
//                if (rdbAllCustomer.checked == true)
//                    Customer = "All"
//                else
//                    Customer = txtCustomer.value;

//                var winname = window.open('View_XOS_rptEBWRegister.aspx?frm=' + txtfromDate.value + '&to=' + txtToDate.value + '&Branch=' + ddlBranch + '&Customer=' + Customer + '&Report=' + Report, '', 'scrollbars=yes,top=50,left=0,status=0,menubar=0,width=1100,height=550');
//                winname.focus();
//                return false;
            }
        </script>
</head>
<body>
    <form id="form2" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManagerMain" runat="server">
    </asp:ScriptManager>
    <script src="~/Scripts/Enable_Disable_Opener.js" type="text/javascript"></script>
    <script src="~/Scripts/InitEndRequest.js" type="text/javascript"></script>
    <asp:UpdateProgress ID="updateProgress" runat="server" DynamicLayout="true">
        <ProgressTemplate>
            <div id="progressBackgroundMain" class="progressBackground">
                <div id="processMessage" class="progressimageposition">
                    <img src="~/Images/ajax-loader.gif" style="border: 0px" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <center>
            <uc2:Menu ID="Menu1" runat="server" />
            <br />
            <br />
            <asp:UpdatePanel ID="UpdatePanelMain" runat="server">
                <ContentTemplate>
                    <table cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left" style="width: 50%" valign="bottom">
                                <asp:Label ID="PageHeader" CssClass="pageLabel" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 50%" valign="top">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%" valign="top">
                                <asp:Label ID="lblmessage" runat="server" CssClass="mandatoryField"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%; border: 1px solid #49A3FF" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0" width="1000px" style="line-height: 150%">
                                    <tr>
                                        <td width="150px" align="right" nowrap>
                                            <span class="mandatoryField">*</span><span class="elementLabel">Branch :</span>
                                        </td>
                                        <td align="left" nowrap>
                                            <asp:DropDownList ID="ddlBranch" CssClass="dropdownList" TabIndex="1" AutoPostBack="true"
                                                Width="100px" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 150px">
                                            <span class="mandatoryField">*</span><span class="elementLabel">From Realised Date :</span>
                                        </td>
                                        <td align="left" style="width: 800px">
                                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="textBox" MaxLength="10" ValidationGroup="dtval"
                                                Width="70" TabIndex="2"></asp:TextBox>
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
                                            <%-- <asp:Button ID="btnChangeDate" runat="server" OnClick="btnChangeDate_Click" />--%>
                                            &nbsp; <span class="mandatoryField">*</span><span class="elementLabel">To Realised Date :</span>
                                            &nbsp;
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="textBox" MaxLength="10" Width="70"
                                                TabIndex="3"></asp:TextBox>
                                            <asp:Button ID="btncalendar_ToDate" runat="server" CssClass="btncalendar_enabled" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                                runat="server" TargetControlID="txtToDate" InputDirection="RightToLeft" AcceptNegative="Left"
                                                ErrorTooltipEnabled="true" CultureName="en-GB" DisplayMoney="Left" PromptCharacter="_">
                                            </ajaxToolkit:MaskedEditExtender>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="btncalendar_ToDate">
                                            </ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ValidationGroup="dtVal" ControlToValidate="txtToDate" EmptyValueMessage="Enter Date Value"
                                                InvalidValueBlurredMessage="Date is invalid" EmptyValueBlurredText="*">
                                            </ajaxToolkit:MaskedEditValidator>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rdbAllCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="4" Text="All Customers" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbSelectedCustomer" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data2" TabIndex="5" Text="Selected Customer" />
                                        </td>
                                    </tr>
                                </table>
                              
                                     <div id="divCutomer">
                                    <table id="Table1" runat="server">
                                        <tr>
                                       
                                            <td align="right" style="font-weight: medium; color: #000000; font-size: small;" width="200px">
                                                <span style="color: Red">*</span>Customer ID. :&nbsp;
                                            </td>
                                            <td align="left" width="0px" style="font-weight: bold; color: #000000;">
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="textBox" MaxLength="40" TabIndex="6"
                                                    Width="100px" AutoPostBack="true" ontextchanged="txtCustomer_TextChanged"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btCustList" runat="server" CssClass="btnHelp_enabled" />
                                                <asp:Label ID="lblCustomerName" runat="server" CssClass="elementLabel" ></asp:Label>
                                          
                                            </td>
                                           
                                        </tr>
                                    </table>
                                    </div>
                                    <table cellspacing="0" border="0">
                                    <tr>
                                        <td nowrap width = "100%">
                                            <asp:RadioButton ID="rdbFresh" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" TabIndex="7" Text="Fresh" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbCancel" runat="server" AutoPostBack="true" CssClass="elementLabel"
                                                GroupName="Data3" TabIndex="8" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                                     
                          
                                <table>
                                    <tr valign="bottom">
                                        <td align="right" style="width: 80px">
                                        </td>
                                        <td align="left" style="width: 700px; padding-top: 10px; padding-bottom: 10px" valign="bottom">
                                            &nbsp;
                                            <asp:Button ID="btnSave" runat="server" CssClass="buttonDefault" Text="Genarate XML File"
                                                ToolTip="Genarate XML File" TabIndex="9" width = "120" 
                                                onclick="btnSave_Click"/>
                                            <asp:Button ID="btnSearchCriteria" runat="server" ClientIDMode="Static" OnClientClick="sss()"
                                                Style="visibility: hidden" Text="Button" UseSubmitBehavior="false" />
                                        </td>
                                    </tr>
                                </table>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            toogleDisplay();
        }
    </script>
</body>
</html>
